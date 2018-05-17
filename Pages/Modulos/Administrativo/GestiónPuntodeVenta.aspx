<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GestiónPuntodeVenta.aspx.cs" Inherits="SIGE.Pages.Modulos.Administrativo.GestiónPuntodeVenta" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestión Puntos de venta</title>
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
        .style53
        {
            width: 127px;
        }
        .style54
        {
            width: 236px;
        }
        </style>
    <link href="../../css/StiloAdministrativo.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: transparent;" >
    <form id="form1" runat="server" >
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="release"> 
    </cc1:ToolkitScriptManager>
    <!-- se agrego el parametro ScriptMode=release a ToolkitScriptManager soluciona problema de event handler exception - 25/05/2011 Angel Ortiz -->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
    
            <cc2:ModalUpdateProgress ID="ModalUpdateProgress1" runat="server"
                    AssociatedUpdatePanelID="UpdatePanel1" BackgroundCssClass="modalProgressGreyBackground">
                <ProgressTemplate>
                    <div class="modalPopup">
                        <div>
                            Cargando...
                        </div>
                        <br />
                        <div>
                            <img alt="Procesando" src="../../images/loading5.gif" style="vertical-align: middle" />
                        </div>
                    </div>
                </ProgressTemplate>
          </cc2:ModalUpdateProgress>
            <div style="height: 526px" >
                <cc1:TabContainer ID="TabAdministradorpdv" runat="server" ActiveTabIndex="7" Width="100%"
                    Height="460px" Font-Names="Verdana" allowtransparency="true" > <!---->
                    <cc1:TabPanel runat="server" HeaderText="País" ID="TabPanelSegmentos" allowtransparency="true">
                        <HeaderTemplate>
                            Segmentos
                        </HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblTitSegmentos" runat="server" CssClass="labelsTit2" Text="Administración de Segmentos"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <fieldset id="FlInfBasicSegmento" runat="server">
                                            <legend style="">Tipo de Segmentación</legend>
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblCodSegmento" runat="server" CssClass="labels" Text="Código *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtCodSegmento" runat="server" BackColor="#DDDDDD" Enabled="False"
                                                            ReadOnly="True" Width="80px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblTipoSegmento" runat="server" CssClass="labels" Text="Tipo *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtTipoSegmento" runat="server" MaxLength="50" Width="190px" Enabled="False"></asp:TextBox>
                                                       
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <table align="center">
                                <tr>
                                    <td valign="top">
                                        <fieldset id="FlAgregaSegmento" runat="server">
                                            <legend style="">Descripción del Tipo de Segmento</legend>
                                            <table align="center">
                                                <tr style="height: 154px;" valign="top">
                                                    <td>
                                                        <asp:TextBox ID="TxtDescSegmento" runat="server" Height="150px" MaxLength="255" TextMode="MultiLine"
                                                            Width="250px" Enabled="False"></asp:TextBox>
                                                       
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td valign="top">
                                        <fieldset id="FlSegmentoAgregado" runat="server">
                                            <legend>Segmentos Agregados</legend>
                                            <table align="center">
                                                <tr style="height: 154px;" valign="top">
                                                    <td>
                                                        <asp:Label ID="LblSegmento" runat="server" CssClass="labels" Text="Nombre *"></asp:Label>
                                                        <asp:TextBox ID="TxtSegmento" runat="server" MaxLength="25" Width="190px" Enabled="False"></asp:TextBox>
                                                        <asp:Button ID="BtnAgrSegmento" runat="server" Text="&gt;&gt;" OnClick="BtnAgrSegmento_Click" Enabled="False" />
                                                        <div>
                                                            <br />
                                                            <br />
                                                            <asp:CheckBoxList ID="ChkSegmento" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblEstadoSegmento" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RbtnEstadoSegmento" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnCrearSegmento" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" OnClick="BtnCrearSegmento_Click" />
                                        <asp:Button ID="BtnsaveSegmento" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnsaveSegmento_Click" />
                                        <asp:Button ID="BtnconsultaSegmento" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" />
                                        <asp:Button ID="BtneditSegmento" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="BtneditSegmento_Click" />
                                        <asp:Button ID="BtnActualizaSegmento" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActualizaSegmento_Click" />
                                        <asp:Button ID="BtncancelSegmento" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtncancelSegmento_Click" />
                                        <asp:Button ID="PregSegmento" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" onclick="PregSegmento_Click" />
                                        <asp:Button ID="AregSegmento" runat="server" CssClass="buttonPlan" Text="&lt;&lt;"
                                            Visible="False" onclick="AregSegmento_Click" />
                                        <asp:Button ID="SregSegmento" runat="server" CssClass="buttonPlan" Text="&gt;&gt;"
                                            Visible="False" onclick="SregSegmento_Click" />
                                        <asp:Button ID="UregSegmento" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False" onclick="UregSegmento_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarSegmentsType" runat="server" CssClass="busqueda" DefaultButton="btnBuscarSegmento"
                                Height="202px" Width="343px" Style="display: none">
                                <br />
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblTitBSegmentsType" runat="server" CssClass="labelsTit2" Text="Buscar Segmentos" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBSegmentsType" runat="server" CssClass="labels" Text="Tipo de Segmento:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtBTipoSegmento" runat="server" MaxLength="50" Width="190px"></asp:TextBox>
                                            
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />                             
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnBuscarSegmento" runat="server" CssClass="buttonPlan" Text="Buscar"
                                                Width="80px" OnClick="btnBuscarSegmento_Click" />
                                            <asp:Button ID="btnCancelarbsegmento" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                                Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnSegmento_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btnCancelarbsegmento" PopupControlID="BuscarSegmentsType"
                                TargetControlID="BtnconsultaSegmento" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="País" ID="TabPanelTipoAgrupación" allowtransparency="true">
                        <HeaderTemplate>
                            Tipo de Agrupación
                        </HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblTitAdmintiponodo" runat="server" CssClass="labelsTit2" Text="Gestión de Tipos de Agrupación comercial"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <fieldset id="FlTipnodo" runat="server">
                                            <legend style="">Tipo de Agrupación Comercial</legend>
                                            <br />
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblCodtipnodo" runat="server" CssClass="labels" Text="Código*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtCodtipnodo" runat="server" BackColor="#DDDDDD" Enabled="False"
                                                            ReadOnly="True" Width="190px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblNomTiponodo" runat="server" CssClass="labels" Text="Nombre*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNomtiponodo" runat="server" MaxLength="50" Width="190px" Enabled="False"></asp:TextBox>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblEstadoTiponodon" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RbtnEstadoTiponodo" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnCrearTipoNodo" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" OnClick="BtnCrearTipoNodo_Click" />
                                        <asp:Button ID="BtnsaveTipoNodo" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnsaveTipoNodo_Click" />
                                        <asp:Button ID="BtnconsultaTipoNodo" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" />
                                        <asp:Button ID="BtneditTipoNodo" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="BtneditTipoNodo_Click" />
                                        <asp:Button ID="BtnActualizaTiponNodo" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActualizaTiponNodo_Click" />
                                        <asp:Button ID="BtncancelTipoNodo" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtncancelTipoNodo_Click" />
                                        <asp:Button ID="PregTipoNodo" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" onclick="PregTipoNodo_Click" />
                                        <asp:Button ID="AregTipoNodo" runat="server" CssClass="buttonPlan" Text="&lt;&lt;"
                                            Visible="False" onclick="AregTipoNodo_Click" />
                                        <asp:Button ID="SregTipoNodo" runat="server" CssClass="buttonPlan" Text="&gt;&gt;"
                                            Visible="False" onclick="SregTipoNodo_Click" />
                                        <asp:Button ID="UregTipoNodo" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False" onclick="UregTipoNodo_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarTipoNodo" runat="server" CssClass="busqueda" DefaultButton="btnBuscarTipoNodo"
                                Style="display: none" Height="202px" Width="343px">
                                <br />
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="LbltitBTipoNodo" runat="server" CssClass="labelsTit2" Text="Buscar Tipo de Agrupación Comercial" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBNomTipoNodo" runat="server" CssClass="labels" Text="Nombre:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtBNomTipoNodo" runat="server" MaxLength="50" Width="120px"></asp:TextBox>                                         
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnBuscarTipoNodo" runat="server" CssClass="buttonPlan" Text="Buscar"
                                    Width="80px" OnClick="btnBuscarTipoNodo_Click" />
                                <asp:Button ID="btncancelarbTipoNodo" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" />
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnTipoNodo_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="btncancelarbtipoNodo" PopupControlID="BuscarTipoNodo"
                                TargetControlID="BtnconsultaTipoNodo" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>       
                    <cc1:TabPanel runat="server" HeaderText="País" ID="TabPanelAgrupaciónComercial" allowtransparency="true" >
                        <HeaderTemplate>
                            Agrupación Comercial
                        </HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            
                            <table align="center" >
                                <tr>
                                    <td>
                                        <asp:Label ID="LblTitAdminnodo" runat="server" CssClass="labelsTit2" Text="Gestión de Agrupación comercial"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                                               

                            <table  align="center" style="width: 429px">
                                <tr>
                                    <td>
                                        <fieldset id="Flnodo" runat="server">
                                            <legend style="">Agrupación Comercial</legend>                                         
                                            <table align="center" style="width: 406px">
                                                <tr>
                                                    <td class="style53">
                                                        <asp:Label ID="LblCodnodo" runat="server" CssClass="labels" Text="Código*"></asp:Label>
                                                    </td>
                                                    <td class="style54">
                                                        <asp:TextBox ID="TxtCodnodo" runat="server" BackColor="#DDDDDD" ReadOnly="True" Width="190px"></asp:TextBox>
                                                    </td>
                                                </tr>                                                
                                           
                                                <tr>
                                                    <td class="style53">
                                                        <asp:Label ID="LblSelTipNodo" runat="server" CssClass="labels" Text="Tipo de Agrupación comercial*"></asp:Label>
                                                    </td>
                                                    <td class="style54">
                                                        <asp:DropDownList ID="CmbSelTipNodo" runat="server" MaxLength="50" Width="198px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style53">
                                                        <asp:Label ID="LblNomnodo" runat="server" CssClass="labels" Text="Nombre *"></asp:Label>
                                                    </td>
                                                    <td class="style54">
                                                        <asp:TextBox ID="txtNomnodo" runat="server" MaxLength="50" Width="190px" Enabled="False"></asp:TextBox>
                                                     
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style53">
                                                        <span class="labels">Dirección: </span>
                                                    </td>
                                                    <td class="style54">
                                                        <asp:TextBox ID="txtdireccion" runat="server" MaxLength="50" Width="190px" Enabled="False"></asp:TextBox>                                                     
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style53">
                                                        <span class="labels">Corporación: </span>
                                                    </td>
                                                    <td class="style54">
                                                        <asp:DropDownList ID="cmbCorporacion" runat="server" MaxLength="50" Width="198px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>                                    
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <div align="center">
                            <fieldset id="Fieldset4" runat="server" align="center" style="width: 429px">
                                            <legend style="">Ubicación Geográfica</legend>
                            <table align="center" style="width: 429px">
                            <tr>
                                <td>País*</td>
                                <td><asp:DropDownList  runat="server" AutoPostBack="True" CausesValidation="True" 
                                        TabIndex="12" Width="166px" Enabled="False" 
                                        OnSelectedIndexChanged="ddlSelCountry_SelectedIndexChanged" ID="ddlSelCountry"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>Departamento</td>
                                <td><asp:DropDownList  runat="server" AutoPostBack="True" CausesValidation="True"
                                                            TabIndex="13" Width="166px" Enabled="False" 
                                        ID="ddlDpto" onselectedindexchanged="ddlDpto_SelectedIndexChanged" >
                                                        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>Provincia</td>
                                <td><asp:DropDownList  runat="server" AutoPostBack="True" CausesValidation="True"
                                                            TabIndex="14" Height="22px" Width="166px" 
                                        Enabled="False" ID="ddlProv" 
                                        onselectedindexchanged="ddlProv_SelectedIndexChanged" >
                                                        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>Distrito</td>
                                <td><asp:DropDownList runat="server" AutoPostBack="True" CausesValidation="True"
                                                            TabIndex="15" Height="22px" Width="166px" 
                                        Enabled="False" ID="ddlDist" 
                                        onselectedindexchanged="ddlDist_SelectedIndexChanged">
                                                        </asp:DropDownList></td>
                            </tr>
                              <tr>
                                <td>Barrio</td>
                                <td><asp:DropDownList ID="ddlBarrio" runat="server" TabIndex="16" Height="22px"
                                                            Width="166px" Enabled="False">
                                                        </asp:DropDownList></td>
                            </tr>
                            </table>
                            </fieldset>
                            </div>
                            <br />
                            <!-- Inicio de GridView -->
                            <asp:Panel ID="AgrupComGVCons" runat="server" Style="display: block"  >                        
                                        <div class="centrar centrarcontenido">                                
                                        <div class="p" style="width:780px; height: 315px; background-color: #FFFFFF; padding: 2px 2px; font-size: 10pt;
	                                        font-family : arial, Helvetica, sans-serif;"> 
                                            <asp:GridView ID="GVConsAgrupCom" runat="server" AutoGenerateColumns="False"  
                                                Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                                Width="100%" onpageindexchanging="GVConsAgrupCom_PageIndexChanging" 
                                                onrowcancelingedit="GVConsAgrupCom_RowCancelingEdit" 
                                                onrowediting="GVConsAgrupCom_RowEditing" onrowupdating="GVConsAgrupCom_RowUpdating" 
                                                >                                               
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Cod.Agrupación">
                                                        <EditItemTemplate>
                                                                <asp:Label ID="LblCodAgruCom" runat="server"  Text='<%# Bind("NodeCommercial") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCodAgruCom" runat="server" Text='<%# Bind("NodeCommercial") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nombre">
                                                        <EditItemTemplate>
                                                        <asp:TextBox ID="TxtNomAgrupCom" runat="server" Width="150px" >       
                                                            </asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblNomAgrupCom" runat="server"  Width="150px" Text='<%# Bind("commercialNodeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tipo" Visible="False">
                                                        <EditItemTemplate>
                                                                <asp:Label ID="LblNodeType" runat="server"  Text='<%# Bind("idNodeComType") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblNodeType" runat="server" Text='<%# Bind("idNodeComType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tipo">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="cmb_Node_Type" runat="server" Width="150px" >       
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblNodeTypeName" runat="server" Width="150px" Text='<%# Bind("NodeComType_name") %>'>></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Dirección">
                                                        <EditItemTemplate>
                                                        <asp:TextBox ID="TxtAgrupDirec" runat="server" Width="150px" >       
                                                            </asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblAgrupDirec" runat="server"  Width="150px" Text='<%# Bind("NodeCommercial_Direccion") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cod.Corporación" Visible="False">
                                                        <EditItemTemplate>
                                                                <asp:Label ID="LblCodCorp" runat="server"  Text='<%# Bind("NodeCommercial_corp_id") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCodCorp" runat="server" Text='<%# Bind("NodeCommercial_corp_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Corporación">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="cmbCorpo_Edit" runat="server" Width="150px" >       
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCorpName" runat="server" Width="150px" Text='<%# Bind("corp_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Estado" >
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="CheckAgrupCom" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckAgrupCom" runat="server"  Enabled="false" Checked ='<%# Bind("NodeCommercial_Status") %>'  ></asp:CheckBox> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pais" Visible="False">
                                                        <EditItemTemplate>
                                                                <asp:Label ID="LblCodPais" runat="server"  Text='<%# Bind("NodeCommercial_codCountry") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCodPais" runat="server" Text='<%# Bind("NodeCommercial_codCountry") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pais">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlPais_edit" runat="server" Width="150px"   OnSelectedIndexChanged="ddlPais_edit_SelectedIndexChanged" AutoPostBack="True" CausesValidation="True">       
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblPaisNombre" runat="server" Width="150px" Text='<%# Bind("Name_Country") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Departamento" Visible="False">
                                                        <EditItemTemplate>
                                                                <asp:Label ID="LblCodDepartamento" runat="server"  Text='<%# Bind("NodeCommercial_codDepartment") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCodDepartamento" runat="server" Text='<%# Bind("NodeCommercial_codDepartment") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Departamento">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlDepartamento_edit" runat="server" Width="150px" AutoPostBack="True" CausesValidation="True"  OnSelectedIndexChanged="ddlDepartamento_edit_SelectedIndexChanged">       
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblDepartamentoNombre" runat="server" Width="150px" Text='<%# Bind("Name_dpto") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Provincia" Visible="False">
                                                        <EditItemTemplate>
                                                                <asp:Label ID="LblCodProvincia" runat="server"  Text='<%# Bind("NodeCommercial_codProvince") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCodProvincia" runat="server" Text='<%# Bind("NodeCommercial_codProvince") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Provincia">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlProvincia_edit" runat="server" Width="150px" AutoPostBack="True" CausesValidation="True" OnSelectedIndexChanged="ddlProvincia_edit_SelectedIndexChanged">       
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblProvinciaNombre" runat="server" Width="150px" Text='<%# Bind("Name_City") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Distrito" Visible="False">
                                                        <EditItemTemplate>
                                                                <asp:Label ID="LblCodDistrito" runat="server"  Text='<%# Bind("NodeCommercial_codDistrict") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCodDistrito" runat="server" Text='<%# Bind("NodeCommercial_codDistrict") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Distrito">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlDistrito_edit" runat="server" Width="150px" AutoPostBack="True" CausesValidation="True" OnSelectedIndexChanged="ddlDistrito_edit_SelectedIndexChanged">       
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblDistritoNombre" runat="server" Width="150px" Text='<%# Bind("Name_Local") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                     <asp:TemplateField HeaderText="Barrio" Visible="False">
                                                        <EditItemTemplate>
                                                                <asp:Label ID="LblCodBarrio" runat="server"  Text='<%# Bind("NodeCommercial_codCommunity") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCodBarrio" runat="server" Text='<%# Bind("NodeCommercial_codCommunity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Barrio">
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlBarrio_edit" runat="server" Width="150px" AutoPostBack="True" CausesValidation="True" Enabled="False" OnSelectedIndexChanged="ddlBarrio_edit_SelectedIndexChanged">       
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblBarrioNombre" runat="server" Width="150px" Text='<%# Bind("Name_Community") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:CommandField ShowEditButton="True" />
                                                </Columns>                                                
                                            </asp:GridView>
                                            <br />                                                 
                                                <div class="centrar">
                                                    <div class="centrar centrarcontenido">
                                                    <asp:Label ID="Lblcateeexc" runat="server" CssClass="labels" Text="Exportar a Excel "></asp:Label>
                                                    </div>
                                                    <iframe id="iframeexcelCategoria" runat="server" src="pruebaexcel.aspx" frameborder="0"  width="64px" height="64px"  ></iframe>
                                                    <div class="centrar centrarcontenido">                                       
                                                        <asp:Button ID="btnCanAgrupCom" runat="server" CssClass="buttonPlan" 
                                                            Text="Cancelar" Width="80px" onclick="btnCancelCategoria_Click" />
                                                    </div> 
                                                </div>                                                               
                                            </div>
                                    </div> 
                            </asp:Panel>
                            <!-- Fin de GriedView -->
                            <cc1:ModalPopupExtender ID="MopopConsulAgrupCom" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True"  PopupControlID="AgrupComGVCons"
                                TargetControlID="btnPopupGVAgrupCom" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="btnPopupGVAgrupCom" runat="server" CssClass="alertas" Width="0px" />
                            <!---->
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblEstadonodo" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RbtnEstadonodo" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <br />

                            

                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnCrearNodo" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px"
                                            OnClick="BtnCrearNodo_Click" />
                                        <asp:Button ID="BtnsaveNodo" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnsaveNodo_Click" />
                                        <asp:Button ID="BtnconsultaNodo" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" />
                                        <asp:Button ID="BtncancelNodo" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtncancelNodo_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarNodo" runat="server" CssClass="busqueda" DefaultButton="BtnBuscarNodo"
                                Style="display: none" Height="202px" Width="343px">                                
                                <div class="titleposition"><span class="spanlabelstit2">Buscar Agrupación Comercial</span></div>
                                <table align="center">
                                    <!-- Begin Seleccionar Cliente -->
                                        <tr>
                                            <td>
                                                <span class="spanlabels">Tipo:</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_bnc_tipo" runat="server" MaxLength="50" 
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                     </tr><!-- End Seleccionar Cliente -->
                                     <tr>
                                        <td>
                                            <asp:Label ID="LblBNomNodo" runat="server" CssClass="labels" Text="Nombre:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtBNomNodo" runat="server" MaxLength="50" Width="146px"></asp:TextBox>
                                          
                                        </td>
                                    </tr>
                                    <tr>
                                            <td>
                                                <span class="spanlabels">Corporación:</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_corporacion" runat="server" MaxLength="50" 
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                </table>
                                <div id="botonerabnode">
                                <asp:Button ID="BtnBuscarNodo" runat="server" CssClass="buttonPlan" Text="Buscar" Width="80px" OnClick="BtnBuscarNodo_Click" />
                                <asp:Button ID="BtnCancelBNodo" runat="server" CssClass="buttonPlan" Text="Cancelar" Width="80px" />
                                </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnNodo_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnCancelBNodo" PopupControlID="BuscarNodo"
                                TargetControlID="BtnconsultaNodo" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                            
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="Panel_ChannelXACommercial" runat="server" HeaderText="Agrupación Comercial x Canal">
                        <HeaderTemplate>
                            Agrupación Comercial x Canal
                        </HeaderTemplate>
                        <ContentTemplate>
                        <br />
                        <br />
                            <div class="centrarcontenido">
                                <span class="labelsTit2">Gestión de Agrupación Comercial por Canales</span>
                            </div>
                        <br />
                        <br />
                            <div class="centrar">  
                                   <div class="tabla centrar">                                                                      
                                        <fieldset>
                                            <legend>Agrupación Comercial x Canal</legend>
                                                
                                                    
                                    
                                                <div class="fila">
                                                    <div class="celda">
                                                    </div>
                                                </div>
                                                <div class="fila">
                                                    <div class="celda">
                                                        <span class="labels">Tipo Agrupación Comercial*</span>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="ddl_typenode_chxnodecom" runat="server" Width="195px"
                                                            Enabled="False" 
                                                            onselectedindexchanged="ddl_typenode_chxnodecom_SelectedIndexChanged" 
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="fila">
                                                    <div class="celda">
                                                        <span class="labels">Agrupación Comercial*</span>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="ddl_nodecom_chxnodecom" runat="server" Width="195px" 
                                                            Enabled="False" AutoPostBack="True" 
                                                            onselectedindexchanged="ddl_nodecom_chxnodecom_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="fila">
                                                        <div class="celda">
                                                            <span class="labels">Cliente *</span>
                                                        </div>
                                                         <div class="celda">
                                                            <asp:DropDownList ID="ddlgpdvcliente" runat="server"
                                                                Enabled="False" MaxLength="50" Width="195px" AutoPostBack="True" 
                                                                 onselectedindexchanged="ddlgpdvcliente_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="fila">
                                                        <div class="celda">
                                                            <span class="labels">Canal *</span>
                                                            </div>
                                                            <div class="celda">
                                                            </div>
                                                        </div>                                                       
                                                       
                                                        <div style=" width: 156px; margin:auto">
                                                                <asp:CheckBoxList ID="cbxlnodecanal" runat="server" BorderColor="#CCCCCC" 
                                                                    BorderStyle="Solid" BorderWidth="1px" Height="24px" Visible="False" 
                                                                    Width="156px" Enabled="False">
                                                                </asp:CheckBoxList>
                                                                <asp:Label ID="lbl_nochannel" runat="server" Text="Seleccione Cliente"></asp:Label>
                                                        </div>

                                        </fieldset>
                                   </div>                                   
                            </div>
                            <br /><br />

                            <!-- Inicio de GridView -->

                            <!-- Fin deGriedView -->

                            <div class="centrarcontenido">
                                <asp:Button runat="server" Text="Editar" CssClass="buttonPlan" Width="95px" 
                                    ID="btn_editachannelxnode" onclick="btn_editachannelxnode_Click"></asp:Button>
                                <asp:Button runat="server" Text="Actualizar" CssClass="buttonPlan" Width="120px" ID="btn_guardarchannelxnode" Visible="False" onclick="btn_guardarchannelxnode_Click"></asp:Button>
                                <asp:Button runat="server" Text="Cancelar" CssClass="buttonPlan" Width="95px" 
                                    ID="btn_cancelarchannelxnode" OnClick="btn_cancelarchannelxnode_Click"></asp:Button>                                                    
                            </div>

                           


                        </ContentTemplate>
                    </cc1:TabPanel>


                    <cc1:TabPanel runat="server" HeaderText="Mallas" ID="Panel_Mallas">
                        <HeaderTemplate>
                            Mallas
                        </HeaderTemplate>
                        <ContentTemplate>            
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblTitAdminmallas" runat="server" CssClass="labelsTit2" Text="Gestión de Mallas"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <br />
                                        <fieldset id="FlMallas" runat="server">
                                            <legend style="">Mallas</legend>
                                            <br />
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblCodMallas" runat="server" CssClass="labels" Text="Código*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtCodMallas" runat="server" BackColor="#DDDDDD" ReadOnly="True"
                                                            Width="80px" Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LabelClientemallas" runat="server" CssClass="labels" Text="Cliente*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbClienteMallas" runat="server" Height="21px" Width="195px"
                                                            Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblNomMallas" runat="server" CssClass="labels" Text="Nombre: *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtNomallas" runat="server" MaxLength="50" Width="190px" Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblmallaStatus" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RbtnmallasStatus" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnCrearmallas" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" OnClick="BtnCrearmallas_Click" />
                                        <asp:Button ID="BtnSavemalla" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnSavemalla_Click" />
                                        <asp:Button ID="BtnConsultamallas" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" />
                                        <asp:Button ID="BtnEditmallas" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="BtnEditmallas_Click" />
                                        <asp:Button ID="BtnActualizamallas" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActualizamallas_Click" />
                                        <asp:Button ID="BtnCancelmalla" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtnCancelmalla_Click" />
                                        <asp:Button ID="PregMalla" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" onclick="PregMalla_Click" />
                                        <asp:Button ID="AregMalla" runat="server" CssClass="buttonPlan" Text="&lt;&lt;" 
                                            Visible="False" onclick="AregMalla_Click" />
                                        <asp:Button ID="SregMalla" runat="server" CssClass="buttonPlan" Text="&gt;&gt;" 
                                            Visible="False" onclick="SregMalla_Click" />
                                        <asp:Button ID="UregMalla" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False" onclick="UregMalla_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarMalla" runat="server" CssClass="busqueda" DefaultButton="BtnBmalla" Style="display: none"
                                Height="202px" Width="343px">
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBmalla" runat="server" CssClass="labelsTit2" Text="Buscar Malla" />
                                        </td>
                                    </tr>
                                     
                                </table>
                                <br />
                                <br />
                                <table align="center">                                 
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblBNombMalla" runat="server" CssClass="labelsTit2" 
                                                Text="Malla:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtBNommalla" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                   
                                </table>
                                <br />
                                <br />
                                <br />
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="BtnBmalla" runat="server" CssClass="buttonPlan" Text="Buscar" Width="80px"
                                    OnClick="BtnBmalla_Click" />
                                <asp:Button ID="BtnCancelBmalla" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" />
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnMalla" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" PopupControlID="BuscarMalla" TargetControlID="BtnConsultamallas"
                                DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="Sector" ID="Panel_Sector">
                        <HeaderTemplate>
                            Sector
                        </HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblTitSector" runat="server" CssClass="labelsTit2" Text="Gestión Sector"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <br />
                                        <br />
                                        <fieldset id="FlSector" runat="server">
                                            <legend style="">Sector</legend>
                                            <br />
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblCodSector" runat="server" CssClass="labels" Text="Código*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtCodSector" runat="server" BackColor="#DDDDDD" ReadOnly="True"
                                                            Width="80px" Enabled="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblClienteSector" runat="server" CssClass="labels" Text="Cliente *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbCliente" runat="server" Width="255px" Enabled="False" 
                                                            onselectedindexchanged="CmbCliente_SelectedIndexChanged" 
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblSelmalla" runat="server" CssClass="labels" Text="Malla *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbmallaSector" runat="server" Width="255px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblNomSector" runat="server" CssClass="labels" Text="Nombre *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtNomSector" runat="server" MaxLength="50" Width="250px" Enabled="False"></asp:TextBox>
                                                      
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                            <br />
                                            <br />
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblStatusSector" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RbtnStatusSector" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnCrearSector" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" OnClick="BtnCrearSector_Click" />
                                        <asp:Button ID="BtnsaveSector" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnsaveSector_Click" />
                                        <asp:Button ID="BtnSearchSector" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" />
                                        <asp:Button ID="BtnEditSector" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="BtnEditSector_Click" />
                                        <asp:Button ID="BtnActualizaSector" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActualizaSector_Click" />
                                        <asp:Button ID="BtnCancelSector" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtnCancelSector_Click" />
                                        <asp:Button ID="PregSector" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" Height="21px" Width="27px" onclick="PregSector_Click" />
                                        <asp:Button ID="AregSector" runat="server" CssClass="buttonPlan" Text="&lt;&lt;"
                                            Visible="False" onclick="AregSector_Click" />
                                        <asp:Button ID="SregSector" runat="server" CssClass="buttonPlan" Text="&gt;&gt;"
                                            Visible="False" onclick="SregSector_Click" />
                                        <asp:Button ID="UregSector" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False" onclick="UregSector_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarSector" runat="server" CssClass="busqueda" DefaultButton="BtnBSector"
                                 Height="202px" Width="343px" Style="display: none;">
                                <br />
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblTitBSector" runat="server" CssClass="labelsTit2" Text="Buscar Sector" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <table align="center">
                                <tr>
                                            <td>
                                                <asp:Label ID="lblBClienteSector" runat="server" CssClass="labels" Text="Cliente:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbBClienteSector" runat="server" Width="215px" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="cmbBClienteSector_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                <tr>
                                            <td>
                                                <asp:Label ID="LblBSector" runat="server" CssClass="labels" Text="Malla:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="CmbBmallaSector" runat="server" Width="215px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBNomSector" runat="server" CssClass="labels" Text="Nombre:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtBNomSector" runat="server" MaxLength="50" Width="210px"></asp:TextBox>
                                      
                                        </td>
                                        
                                    </tr>
                                </table>
                                <br />
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="BtnBSector" runat="server" CssClass="buttonPlan" Text="Buscar" Width="80px"
                                    OnClick="BtnBSector_Click" />
                                <asp:Button ID="BtnCancelBSector" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" />
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnSector" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnCancelBSector" PopupControlID="BuscarSector"
                                TargetControlID="BtnSearchSector" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="País" ID="TabDistribuidora" allowtransparency="true">
                        <HeaderTemplate>
                            Distribuidora
                        </HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblDistribuidor" runat="server" CssClass="labelsTit2" Text="Gestión de Distribuidora"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <br />
                                        <br />
                                        <fieldset id="Fieldset3" runat="server">
                                            <legend style="">Distribuidora</legend>
                                            <br />
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Lablcod" runat="server" CssClass="labels" Text="Código*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Texcodigod" runat="server" BackColor="#DDDDDD" Enabled="False"
                                                            ReadOnly="True" Width="190px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Lblndis" runat="server" CssClass="labels" Text="Distribuidora *"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextDistribuidora" runat="server" MaxLength="50" Width="190px" Enabled="False"></asp:TextBox>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                        </fieldset>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="Lblestadod" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="Rbtestadodistribuidora" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="creardistrib" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" onclick="creardistrib_Click"  />
                                        <asp:Button ID="Guardardistri" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" onclick="Guardardistri_Click"  />
                                        <asp:Button ID="ConsultarDistr" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" />
                                        <asp:Button ID="editardistribuidor" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" onclick="editardistribuidor_Click"  />
                                        <asp:Button ID="ActualizarDistri" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" onclick="ActualizarDistri_Click" />
                                        <asp:Button ID="cancelardistrib" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" onclick="cancelardistrib_Click" />
                                        <asp:Button ID="primero" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" onclick="primero_Click"  />
                                        <asp:Button ID="antes" runat="server" CssClass="buttonPlan" Text="&lt;&lt;"
                                            Visible="False" onclick="antes_Click"  />
                                        <asp:Button ID="siguiente" runat="server" CssClass="buttonPlan" Text="&gt;&gt;"
                                            Visible="False" onclick="siguiente_Click" />
                                        <asp:Button ID="ultimo" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False"  />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnBuscarDistribuidora" runat="server" CssClass="busqueda" DefaultButton="BtnBucarDistribuidora"
                                Style="display: none" Height="202px" Width="343px">
                                <br />
                                <br />
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblbD" runat="server" CssClass="labelsTit2" Text="Buscar Distribuidora" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBdistribuidora" runat="server" CssClass="labels" Text="Nombre:" />
                                        </td>
                                        <td>                                            
                                          <asp:DropDownList ID="cmbBDistribuidora" runat="server" Height="21px" Width="195px" Enabled="true">
                                          </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                            <div align="center">
                                <asp:Button ID="BtnBucarDistribuidora" runat="server" CssClass="buttonPlan" Text="Buscar"
                                    Width="80px" onclick="BtnBucarDistribuidora_Click"  />
                                <asp:Button ID="BtnCancelarDistri" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                    Width="80px" />
                            </div>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="modalBusDistribuidora" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnCancelarDistri" PopupControlID="pnBuscarDistribuidora"
                                TargetControlID="ConsultarDistr" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="PDV" ID="TabPanelPDV" allowtransparency="true">
                        <HeaderTemplate>
                            Puntos de Venta
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" CssClass="labelsTit2" Text="Administración de Puntos de Venta"></asp:Label>
                                    </td>
                                </tr>
                            </table>



                            <table align="center">
                                <tr>
                                    <td>
                                        <fieldset id="FSInfBasica" runat="server">
                                            <legend style="">Información Básica</legend>
                                                <div id="main_basic" class="tabla"> <!-- inicio tabla -->
                                                    <div class="fila">
                                                        <div class="celda"><span class="labels">Código Interno*</span></div>
                                                        <div class="celda"><asp:TextBox ID="TxtCodPos" runat="server" BackColor="#DDDDDD" ReadOnly="True" Width="60px" Enabled="False"></asp:TextBox></div>
                                                        <div class="celda"></div>
                                                        <div class="celda"></div>
                                                    </div>
                                                    <div class="fila">
                                                        <div class="celda"><span class="labels">Razón Social*</span></div>
                                                        <div class="celda"><asp:TextBox ID="TxtRSocPos" runat="server" Enabled="False" MaxLength="50" TabIndex="3" Width="225px"></asp:TextBox></div>
                                                        <div class="celda"><span class="labels">Nombre Comercial*</span></div>
                                                        <div class="celda"><asp:TextBox ID="TxtNComPos" runat="server" MaxLength="50" TabIndex="4" Width="225px" Enabled="False"></asp:TextBox></div>
                                                    </div>
                                                    <div class="fila">
                                                        <div class="celda"><span class="labels">Tipo de Documento*</span></div>
                                                        <div class="celda"><asp:DropDownList ID="cmbTipDocPDV" runat="server" TabIndex="1" Width="166px" Enabled="False"></asp:DropDownList></div>
                                                        <div class="celda"><span class="labels">Teléfono</span></div>
                                                        <div class="celda"><asp:TextBox ID="TxtTelPos" runat="server" MaxLength="12" TabIndex="5" Width="119px" Enabled="False"></asp:TextBox></div>
                                                    </div>
                                                    <div class="fila">
                                                        <div class="celda"><span class="labels">Número de Documento*</span></div>
                                                        <div class="celda"><asp:TextBox runat="server" MaxLength="11" Enabled="False" TabIndex="2" Width="160px" ID="TxtNumdocPDV"></asp:TextBox></div>
                                                        <div class="celda"><span class="labels">Fax</span></div>
                                                        <div class="celda"><asp:TextBox runat="server" MaxLength="12" Enabled="False" TabIndex="7" Width="120px" ID="TxtFaxPos"></asp:TextBox></div>
                                                    </div>
                                                    <div class="fila">
                                                        <div class="celda"><span class="labels">Email</span></div>
                                                        <div class="celda"><asp:TextBox runat="server" Enabled="False" TabIndex="18" Width="160px" ID="TxtMailPos"></asp:TextBox></div>
                                                        <div class="celda"><span class="labels">Anexo</span></div>
                                                        <div class="celda"><asp:TextBox runat="server" MaxLength="12" Enabled="False" TabIndex="7" Width="120px" ID="TxtanexPos"></asp:TextBox> </div>
                                                    </div>
                                                    <div class="fila">
                                                        <div class="celda"><span class="labels">Nombre de Contacto 1</span></div>
                                                        <div class="celda"><asp:TextBox runat="server" MaxLength="50" Enabled="False" TabIndex="8" Width="160px" ID="TxtcontacPos"></asp:TextBox></div>
                                                        <div class="celda"><span class="labels">Cargo 1</span></div>
                                                        <div class="celda"><asp:TextBox runat="server" MaxLength="50" Enabled="False" TabIndex="9" Width="121px" ID="TxtCargo1"></asp:TextBox></div>
                                                    </div>
                                                    <div class="fila">
                                                        <div class="celda"><span class="labels">Nombre de Contacto 2</span></div>
                                                        <div class="celda"><asp:TextBox runat="server" MaxLength="50" Enabled="False" TabIndex="10" Width="160px" ID="TxtcontacPos2"></asp:TextBox></div>
                                                        <div class="celda"><span class="labels">Cargo 2</span></div>
                                                        <div class="celda"><asp:TextBox runat="server" MaxLength="50" Enabled="False" TabIndex="11" Width="121px" ID="TxtCargo2"></asp:TextBox></div>
                                                    </div>                                                    
                                                </div> <!-- end tabla -->
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <fieldset id="Fieldset2" runat="server">
                                            <legend style="">Ubicación Geográfica</legend>
                                                <div id="main_geo" class="tabla"> <!-- inicio tabla -->
                                                    <div class="fila">
                                                    <div class="celda"><span class="labels">País*</span></div>
                                                    <div class="celda"><asp:DropDownList ID="cmbSelCountry" runat="server" AutoPostBack="True" CausesValidation="True" TabIndex="12" Width="166px" Enabled="False" OnSelectedIndexChanged="cmbSelCountry_SelectedIndexChanged"></asp:DropDownList></div>
                                                    <div class="celda"><span class="labels">Canal*</span></div>
                                                    <div class="celda"><asp:DropDownList ID="cmbSelCanal" runat="server" TabIndex="19" Width="166px" Enabled="False"></asp:DropDownList></div>
                                                </div>
                                                    <div class="fila">
                                                    <div class="celda">
                                                        <span class="labels">Departamento</span>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="cmbSelDpto" runat="server" AutoPostBack="True" CausesValidation="True"
                                                            TabIndex="13" Width="166px" Enabled="False" OnSelectedIndexChanged="cmbSelDpto_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="celda">
                                                        <span class="labels">Tipo de agrupación comercial*</span>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="CmbTipMerc" runat="server" AutoPostBack="True" CausesValidation="True"
                                                            TabIndex="20" Width="166px" Enabled="False" OnSelectedIndexChanged="CmbTipMerc_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                    <div class="fila">
                                                    <div class="celda">
                                                        <span class="labels">Provincia</span>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="cmbSelProvince" runat="server" AutoPostBack="True" CausesValidation="True"
                                                            TabIndex="14" Height="22px" Width="166px" Enabled="False" OnSelectedIndexChanged="cmbSelProvince_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="celda">
                                                        <span class="labels">Agrupación comercial*</span>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="cmbNodoCom" runat="server" TabIndex="21" Height="22px" Width="166px"
                                                            Enabled="False">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                    <div class="fila">
                                                    <div class="celda">
                                                        <span class="labels">Distrito</span>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="cmbSelDistrict" runat="server" AutoPostBack="True" CausesValidation="True"
                                                            TabIndex="15" Height="22px" Width="166px" Enabled="False" OnSelectedIndexChanged="cmbSelDistrict_SelectedIndexChanged1">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="celda">
                                                        <span class="labels">Segmento*</span>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="CmbSelSegPDV" runat="server" TabIndex="22" Height="22px" Width="166px"
                                                            Enabled="False">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                    <div class="fila">
                                                    <div class="celda">
                                                        <asp:Label ID="LblSelComunity" runat="server" CssClass="labels" Text="Barrio"></asp:Label>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:DropDownList ID="cmbSelComunity" runat="server" TabIndex="16" Height="22px"
                                                            Width="166px" Enabled="False">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:Label ID="LblDirPos" runat="server" CssClass="labels" Text="Dirección*"></asp:Label>
                                                    </div>
                                                    <div class="celda">
                                                        <asp:TextBox ID="TxtDirPos" runat="server" MaxLength="200" TabIndex="17" Width="161px"></asp:TextBox>
                                                    </div>                                             
                                                </div>
                                                </div> <!-- fin tabla -->
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="LblStatusPos" runat="server" CssClass="labels" Text="Estado"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RBtnListStatusPos" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Black" RepeatDirection="Horizontal" TabIndex="26"
                                            Enabled="False">
                                            <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                            <asp:ListItem>Deshabilitado</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCrearPos" runat="server" CssClass="buttonPlan" TabIndex="27" Text="Crear"
                                            Width="95px" OnClick="btnCrearPos_Click" />
                                        <asp:Button ID="btnsavePos" runat="server" CssClass="buttonPlan" TabIndex="28" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="btnsavePos_Click" />
                                        <asp:Button ID="btnConsultarPos" runat="server" CssClass="buttonPlan" TabIndex="29"
                                            Text="Consultar" Width="95px" />
                                        <asp:Button ID="btnEditPDV" runat="server" CssClass="buttonPlan" TabIndex="30" Text="Editar"
                                            Visible="False" Width="95px" OnClick="btnEditPDV_Click" />
                                        <asp:Button ID="btnActualizarPos" runat="server" CssClass="buttonPlan" TabIndex="31"
                                            Text="Actualizar" Visible="False" Width="95px" OnClick="btnActualizarPos_Click" />
                                        <asp:Button ID="btnCancelPos" runat="server" CssClass="buttonPlan" TabIndex="32"
                                            Text="Cancelar" Width="95px" OnClick="btnCancelPos_Click" />
                                        <asp:Button ID="btnPtoVentaMasivo" runat="server" CssClass="buttonPlan" TabIndex="32"
                                            Text="Carga Masiva" Width="95px" OnClick="btnPtoVentaMasivo_Click" />
                                        <asp:Button ID="btnPreg7" runat="server" CssClass="buttonPlan" TabIndex="33" Text="|&lt;&lt;"
                                            Visible="False" OnClick="btnPreg7_Click" />
                                        <asp:Button ID="btnAreg7" runat="server" CssClass="buttonPlan" TabIndex="34" Text="&lt;&lt;"
                                            Visible="False" OnClick="btnAreg7_Click" Height="21px" Width="24px" />
                                        <asp:Button ID="btnSreg7" runat="server" CssClass="buttonPlan" TabIndex="35" Text="&gt;&gt;"
                                            Visible="False" OnClick="btnSreg7_Click" />
                                        <asp:Button ID="btnUreg7" runat="server" CssClass="buttonPlan" TabIndex="36" Text="&gt;&gt;|"
                                            Visible="False" OnClick="btnUreg7_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarPDV" runat="server" CssClass="busqueda"  Style="display: none" DefaultButton="BtnBuscarPdv" Height="202px" Width="363px">
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Lblbpdv" runat="server" CssClass="labelsTit2" Text="Buscar Puntos de Venta" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <table align="center">
                                <tr>
                                        <td>
                                            <asp:Label ID="Lblbidenpdv" runat="server" CssClass="labels" Text="RUC / NIT:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtbidpdv" runat="server" MaxLength="12" Width="100px"></asp:TextBox>
                                            
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <asp:Label ID="LblCanal" runat="server" CssClass="labels" Text="Canal:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbCanalBPDV" runat="server" Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblbProvPdv" runat="server" CssClass="labels" Text="Provincia / Ciudad:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbbProvpdv" runat="server" Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                   
                                    
                                    <tr>
                                        <td>
                                            <asp:Label ID="Lblbnompdv" runat="server" CssClass="labels" Text="Nombre Comercial:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Txtbnompdv" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                           
                                        </td>
                                    </tr>
                                </table>
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnBuscarPdv" runat="server" CssClass="buttonPlan" Text="Buscar"
                                                Width="80px" OnClick="BtnBuscarPdv_Click" />
                                            <asp:Button ID="BtnCancelarpdv" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                                Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="IbtnPdv_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnCancelarpdv" PopupControlID="BuscarPDV"
                                TargetControlID="btnConsultarPos" DynamicServicePath="">
                            </cc1:ModalPopupExtender>

                             <asp:Panel ID="CargaMasivaPtoVenta" runat="server" Style="vertical-align: middle; " BackColor="#7F99CC" BorderColor="#7F99CC"
                            BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt">
                           
                            <div style="position:absolute; z-index:1;  width:100%;" align="right" > 
                            <asp:ImageButton ID="BtnCerrarProducto" runat="server" AlternateText="Cerrar Ventana"
                                                ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px"  
                                        />
                            </div>
                                    <div  align="center">                                   
                                        <iframe id="IframeCargarPtoVenta" runat="server" height="230px" src="" width="500px">
                                        </iframe>                                       
                                    </div>                                                           
                                </asp:Panel>
                                <cc1:ModalPopupExtender ID="ModalPCargaPtoVenta" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True"  PopupControlID="CargaMasivaPtoVenta"
                                TargetControlID="btnPopupPtoventa" DynamicServicePath="">
                                </cc1:ModalPopupExtender>
                                 <asp:Button ID="btnPopupPtoventa" runat="server" CssClass="alertas"
                                Width="0px" />


                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel runat="server" HeaderText="PDV_Cliente" ID="PanelPDVCliente">
                        <HeaderTemplate>
                            PDV VS Cliente
                        </HeaderTemplate>
                        <ContentTemplate>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" CssClass="labelsTit2" Text="Gestión Puntos de Venta VS Cliente"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <fieldset id="Fieldset1" runat="server">
                                            <legend style="">PDV_Cliente</legend>
                                            <table align="center">
                                                <tr>
                                                  <td>
                                                        <asp:Label ID="LblPaísPDV" runat="server" CssClass="labels" Text="País:*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbPaísPDVC" runat="server" Enabled="False" Width="200px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="CmbPaísPDVC_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                              
                                                     <td>
                                                        <asp:Label ID="lblClientePDVC" runat="server" CssClass="labels" Text="Cliente:*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbClientePDVC" runat="server" Enabled="False" 
                                                            Width="200px" AutoPostBack="True" 
                                                            onselectedindexchanged="cmbClientePDVC_SelectedIndexChanged" >
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblTACPDVC" runat="server" CssClass="labels" Text="Tipo Agrupación:*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbTACPDVC" runat="server" AutoPostBack="True" Enabled="False"
                                                            OnSelectedIndexChanged="cmbTACPDVC_SelectedIndexChanged" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="LblCanalPDVC" runat="server" CssClass="labels" Text="Canal:*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="CmbCanalPDVC" runat="server" AutoPostBack="True" Enabled="False"
                                                            OnSelectedIndexChanged="CmbCanalPDVC_SelectedIndexChanged" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LblAgruCPDVC" runat="server" CssClass="labels" Text="Agrupación Comercial:*"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cmbAgruCPDVC" runat="server" Enabled="False" Width="200px"
                                                            AutoPostBack="True" OnSelectedIndexChanged="cmbAgruCPDVC_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <div class="p" style="width: 850px; height: 193px;">
                                            <asp:GridView ID="GvPDV" runat="server" AutoGenerateColumns="False" 
                                                Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                                Width="100%" onrowcancelingedit="GvPDV_RowCancelingEdit" 
                                                onrowediting="GvPDV_RowEditing" onrowupdating="GvPDV_RowUpdating">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Cod.PDV">
                                                        <EditItemTemplate>
                                                             <asp:Label ID="Label1" runat="server" Text='<%# Bind("id_PointOfsale") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("id_PointOfsale") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Punto de Venta">
                                                        <EditItemTemplate>
                                                          <asp:Label ID="LblPDVName" runat="server" Text='<%# Bind("pdv_Name") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblNamePDV" runat="server" Text='<%# Bind("pdv_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Código">
                                                    <EditItemTemplate>
                                                         <asp:TextBox ID="TxtCodPDVC" runat="server" Width="80px"></asp:TextBox>
                                                     </EditItemTemplate>
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblcodPDVC" runat="server"  Width="80px" Text='<%# Bind("ClientPDV_Code") %>'></asp:Label>                                                      
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Alias" > 
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TxtAliasPDVC" runat="server" Width="80px"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblAliasPDVC" runat="server"   ></asp:Label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Oficina">
                                                          <EditItemTemplate>                                                   
                                                            <asp:DropDownList ID="cmbOficinaPDVC" runat="server" Width="120px" 
                                                                AutoPostBack="True" 
                                                                onselectedindexchanged="cmbOficinaPDVC_SelectedIndexChanged"  >
                                                            </asp:DropDownList>
                                                              </EditItemTemplate>
                                                       <ItemTemplate>
                                                       <asp:Label ID="lblcodOficina" runat="server" Width="120px"  Text='<%# Bind("Name_Oficina") %>'></asp:Label>                                                                                
                                                       </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Región">
                                                      <EditItemTemplate> 
                                                            <asp:DropDownList ID="cmbMallaPDVC" runat="server" Width="120px" 
                                                                AutoPostBack="True" onselectedindexchanged="cmbMallaPDVC_SelectedIndexChanged" >
                                                            </asp:DropDownList>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                       <asp:Label ID="lblRegion" runat="server" Width="120px"  Text='<%# Bind("malla") %>'></asp:Label>                                                                                
                                                       </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Zona">
                                                      <EditItemTemplate> 
                                                            <asp:DropDownList ID="cmbSectorPDVC" runat="server" Width="120px" >                                                            
                                                            </asp:DropDownList>
                                                       </EditItemTemplate>
                                                        <ItemTemplate>
                                                          <asp:Label ID="lblZona" runat="server" Width="120px"  Text='<%# Bind("Sector") %>'></asp:Label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Distribuidora">
                                                      <EditItemTemplate> 
                                                            <asp:DropDownList ID="cmbDEX" runat="server" Width="120px" >                                                            
                                                            </asp:DropDownList>
                                                       </EditItemTemplate>
                                                        <ItemTemplate>
                                                          <asp:Label ID="lbldex" runat="server" Width="120px"  Text='<%# Bind("Dex_Name") %>'></asp:Label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Estado" >
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server"  Enabled="false"    ></asp:CheckBox> 
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server"  Enabled="false" ></asp:CheckBox> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowEditButton="True" UpdateText="Guardar" 
                                                        EditText="Agregar" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="GVPDVConsulta" runat="server" AutoGenerateColumns="False" 
                                                Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                                Width="100%" onrowcancelingedit="GVPDVConsulta_RowCancelingEdit" 
                                                onrowediting="GVPDVConsulta_RowEditing" 
                                                onrowupdating="GVPDVConsulta_RowUpdating">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Cod.PDV">
                                                        <EditItemTemplate>
                                                             <asp:Label ID="Label1" runat="server" Text='<%# Bind("id_PointOfsale") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("id_PointOfsale") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Punto de Venta">
                                                        <EditItemTemplate>
                                                          <asp:Label ID="LblPDVName" runat="server" Text='<%# Bind("pdv_Name") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblNamePDV" runat="server" Text='<%# Bind("pdv_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Código">
                                                    <EditItemTemplate>
                                                         <asp:TextBox ID="TxtCodPDVC" runat="server" Width="80px" Enabled="False"></asp:TextBox>
                                                     </EditItemTemplate>
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblcodPDVC" runat="server"  Width="80px" Text='<%# Bind("ClientPDV_Code") %>'></asp:Label>                                                      
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Oficina">
                                                          <EditItemTemplate>
                                                            &nbsp;&nbsp;                                                   
                                                            <asp:DropDownList ID="cmbOficinaconsultaPDVC" runat="server" Width="120px" 
                                                                AutoPostBack="True" 
                                                                onselectedindexchanged="cmbOficinaConsultaPDVC_SelectedIndexChanged"  >
                                                            </asp:DropDownList>
                                                              </EditItemTemplate>
                                                       <ItemTemplate>
                                                       <asp:Label ID="lblcodOficina" runat="server" Width="120px"  Text='<%# Bind("Name_Oficina") %>'></asp:Label>                                                                                
                                                       </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Región">
                                                      <EditItemTemplate> 
                                                            <asp:DropDownList ID="cmbMallaConsultaPDVC" runat="server" Width="100px" 
                                                                AutoPostBack="True" onselectedindexchanged="cmbMallaConsultaPDVC_SelectedIndexChanged" >
                                                            </asp:DropDownList>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                       <asp:Label ID="lblRegion" runat="server" Width="100px"  Text='<%# Bind("malla") %>'></asp:Label>                                                                                
                                                       </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Zona">
                                                      <EditItemTemplate> 
                                                            <asp:DropDownList ID="cmbSectorPDVC" runat="server" Width="100px" >                                                            
                                                            </asp:DropDownList>
                                                       </EditItemTemplate>
                                                        <ItemTemplate>
                                                          <asp:Label ID="lblZona" runat="server" Width="100px"  Text='<%# Bind("Sector") %>'></asp:Label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Distribuidora">
                                                      <EditItemTemplate> 
                                                            <asp:DropDownList ID="cmbDEX" runat="server" Width="100px" >                                                            
                                                            </asp:DropDownList>
                                                       </EditItemTemplate>
                                                        <ItemTemplate>
                                                          <asp:Label ID="lbldex" runat="server" Width="100px"  Text='<%# Bind("Dex_Name") %>'></asp:Label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Estado" >
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server"  Enabled="true" ></asp:CheckBox> 
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server"  Enabled="false" Checked ='<%# Bind("ClientPDV_Status") %>'  ></asp:CheckBox> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowEditButton="True" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnCrearPDVC" runat="server" CssClass="buttonPlan" Text="Crear" Width="95px"
                                            OnClick="BtnCrearPDVC_Click" />
                                        <asp:Button ID="BtnGuardarPDVC" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnGuardarPDVC_Click" />
                                        <asp:Button ID="BtnConsultarPDVC" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" />
                                        <asp:Button ID="BtnEditarPDVC" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px" OnClick="BtnEditarPDVC_Click" />
                                        <asp:Button ID="BtnActualizarPDVC" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px" OnClick="BtnActualizarPDVC_Click" />
                                        <asp:Button ID="BtnCancelarPDVC" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtnCancelarPDVC_Click" />
                                            <asp:Button ID="btnCargamasivaPDVC" runat="server" CssClass="buttonPlan" TabIndex="32"
                                            Text="Carga Masiva" Width="95px" OnClick="btnCargamasivaPDVC_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="BuscarPDVCliente" runat="server" CssClass="busqueda"  Style="display: none"
                                DefaultButton="BtnBuscarPDVCliente" Height="202px" Width="400px">
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblTituloBuscar" runat="server" CssClass="labelsTit2" Text="Buscar PDV a Cliente" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table align="center">
                                          <tr>
                                        <td>
                                            <asp:Label ID="LblPaisPDVC" runat="server" CssClass="labels" Text="País:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbBPaísPDVC" runat="server" MaxLength="50" Width="193px" 
                                                AutoPostBack="True" onselectedindexchanged="cmbBPaísPDVC_SelectedIndexChanged" 
                                               >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCliente" runat="server" CssClass="labels" Text="Cliente:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbBClientePDVC" runat="server" MaxLength="50" 
                                                Width="193px" AutoPostBack="True" 
                                                onselectedindexchanged="cmbBClientePDVC_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                          
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblBCanal" runat="server" CssClass="labels" Text="Canal:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbBCanalPDVC" runat="server" MaxLength="50" 
                                                Width="193px" AutoPostBack="True" 
                                                onselectedindexchanged="cmbBCanalPDVC_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                        <tr>
                                        <td>
                                            <asp:Label ID="LlbTipoAgrupación" runat="server" CssClass="labels" Text="Tipo Agrupación:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbBTipoAgrupacion" runat="server" MaxLength="50" 
                                                 Width="193px" AutoPostBack="True" onselectedindexchanged="cmbBTipoAgrupacion_SelectedIndexChanged" 
                                                >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                        <tr>
                                        <td>
                                            <asp:Label ID="lblAgrupacionC" runat="server" CssClass="labels" Text="Agrupación Comercial:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbBAgrupacionC" runat="server" MaxLength="50" 
                                                 Width="193px" 
                                                >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                               <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnBuscarPDVCliente" runat="server" CssClass="buttonPlan" Text="Buscar"
                                                Width="80px" OnClick="BtnBuscarPDVCliente_Click" />
                                            <asp:Button ID="BtnBCancelarPDVC" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                                Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalPopupPDVCliente" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnBCancelarPDVC" PopupControlID="BuscarPDVCliente"
                                TargetControlID="BtnConsultarPDVC" DynamicServicePath="">
                            </cc1:ModalPopupExtender>


                            <asp:Panel ID="CargaMasivaPtoVentaClienete" runat="server" Style="vertical-align: middle; " BackColor="#7F99CC" BorderColor="#7F99CC"
                            BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt">
                           
                            <div style="position:absolute; z-index:1;  width:100%;" align="right" > 
                            <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Cerrar Ventana"
                                                ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px"  
                                        />
                            </div>
                                    <div  align="center">                                   
                                        <iframe id="Iframe1" runat="server" height="230px" src="" width="500px">
                                        </iframe>                                       
                                    </div>                                                           
                                </asp:Panel>
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True"  PopupControlID="CargaMasivaPtoVentaClienete"
                                TargetControlID="btnPopupPtoventaCliente" DynamicServicePath="">
                                </cc1:ModalPopupExtender>
                                 <asp:Button ID="btnPopupPtoventaCliente" runat="server" CssClass="alertas"
                                Width="0px" />




                        </ContentTemplate>
                    </cc1:TabPanel>
                    
                </cc1:TabContainer>
            </div>
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
            <asp:Button ID="Btndisparaalertas" runat="server" CssClass="alertas" Text="" Visible="true"
                Width="0px" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
