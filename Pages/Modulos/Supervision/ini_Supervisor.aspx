<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ini_Supervisor.aspx.cs" Inherits="SIGE.Pages.Modulos.Supervision.ini_Supervisor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Xplora - Supervisión de Actividades</title>
    <link href="../../css/Menu.css" rel="stylesheet" type="text/css" />   
    <link href="../../css/stilo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
   
    <style type="text/css">
        .style3
            {
                width: 278px;
            }
        .style4
            {
                width: 80px;
            }        
        
        </style>
</head>
<body>    
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <table width="90%" align="center" >
        <tr>            
            <td align="right">
                <asp:Label ID="lblUsuario" runat="server" Text="" CssClass="texto" ></asp:Label>
                <asp:Label ID="usersession" runat="server"></asp:Label>
                <br />
                <asp:ImageButton ID="ImgCloseSession" runat="server" Height="16px" 
                ImageUrl="~/Pages/images/SesionClose.png" Width="84px" 
                    onclick="ImgCloseSession_Click" />
            </td>            
        </tr>
    </table>
    <br />
    <asp:UpdatePanel ID="UpPanelSupervisor" runat="server">
        <ContentTemplate>            
            <table align="center">
                <tr>
                    <td>
                        <asp:Menu id="MenuSupervisor" Runat="server" CssSelectorClass="menu"
                            Orientation="Horizontal" onmenuitemclick="MenuSupervisor_MenuItemClick">                                            
                            <Items>
                                <asp:MenuItem Text="Distribución de Tareas a Operativos" Value="0" />                                                
                                <asp:MenuItem Text="Auditoria de Información" Value="1" />                                                
                            </Items>                                          
                        </asp:Menu>
                    </td>
                </tr>
            </table>                
            <asp:Panel ID="Pasignaciones" runat="server">            
                <table align="center">
                    <tr>
                        <td>
                            <fieldset id="ContenPlan" runat="server" 
                                style="border-color:#000000; border-width:1px;">
                                <legend style="color:#CC3399; font-weight:bold ;margin-left:10px" >Distribución de Tareas</legend>
                                <br />
                                <asp:GridView ID="gvplanning" runat="server" 
                                    onselectedindexchanged="gvplanning_SelectedIndexChanged" 
                                    AutoGenerateSelectButton="True" GridLines="Horizontal" 
                                    BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                                    CellPadding="4">
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="Silver" ForeColor="Black" />
                                </asp:GridView>    
                                <br />
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <br />                 
                <table align="center">
                    <tr> 
                        <td>
                            <cc1:TabContainer ID="TbCSupervisor" runat="server" ActiveTabIndex="0" Font-Size="9pt">
                               
                                <cc1:TabPanel ID="TabPDV" runat="server" HeaderText="Asignación de Puntos de Venta">
                                    <ContentTemplate>
                                        <br />
                                        <br />
                                        <asp:Panel ID="panelAsignaPDV" runat="server" >
                                            <table align="center">
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Label ID="LblSeloperativo" runat="server" Font-Bold="True" 
                                                            Text="Personal Operativo"></asp:Label>
                                                        <br />
                                                        <asp:ListBox ID="LstBoxSelOperativos" runat="server" Font-Names="Verdana" 
                                                            Font-Size="9pt" Height="140px" Width="300px">
                                                        </asp:ListBox>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Label ID="LblSelpdv" runat="server" 
                                                        Text="Puntos a Visitar" Font-Bold="True" ></asp:Label>
                                                        <br />
                                                        <asp:ListBox ID="LstBoxPdv" runat="server" Width="300px" Height="140px" 
                                                        SelectionMode="Multiple" CausesValidation="True" Font-Names="Verdana" 
                                                        Font-Size="9pt"></asp:ListBox>
                                                    </td>
                                                    <td valign="top" align="center" width="80px" rowspan="3">
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <asp:Button ID="BtnMasAsing" runat="server" CssClass="pagnext" 
                                                            onclick="BtnMasAsing_Click" Text=" ." ToolTip="Asignar" Width="85px" />
                                                        <br />
                                                        <br />
                                                    </td>
                                                    <td valign="top" rowspan="3">
                                                        <asp:Label ID="LblAsignacion" runat="server" 
                                                        Text="Asignación realizada" Font-Bold="True" ></asp:Label>
                                                        <br />
                                                        <asp:GridView ID="GvAsignados" runat="server" AllowPaging="True" 
                                                        GridLines="None" PageSize="15" 
                                                        onpageindexchanging="GvAsignados_PageIndexChanging" 
                                                        onselectedindexchanged="GvAsignados_SelectedIndexChanged"
                                                        Font-Names="Verdana" Font-Size="8pt">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" 
                                                                SelectText="" ShowSelectButton="True" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Image ID="ImgAdverirPDV" runat="server" 
                                                            ImageUrl="~/Pages/images/alert1.gif" Visible="False" />
                                                        <asp:Label ID="LblAdvertirPDV" runat="server" 
                                                            Text="No todo el Personal tiene asignación de Puntos de Venta" 
                                                            Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:Label>
                                                        
                                                        <asp:Image ID="ImgOkPdv" runat="server" ImageUrl="~/Pages/images/alert2.gif" 
                                                            Visible="False" />
                                                        <asp:Label ID="LblOkPdv" runat="server" 
                                                            Text="Todo el Personal tiene al menos un punto de venta asignado" 
                                                            Font-Names="Arial" Font-Size="8pt"  Visible="False"></asp:Label>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Image ID="ImgAdverirPDV0" runat="server" 
                                                            ImageUrl="~/Pages/images/alert1.gif" Visible="False" />
                                                        <asp:Label ID="LblAdvertirPDV0" runat="server" Font-Names="Arial" 
                                                            Font-Size="8pt" 
                                                            Text="Falta Puntos de Venta por asignar a Personal" 
                                                            Visible="False"></asp:Label>
                                                        
                                                        <asp:Image ID="ImgOkPdv0" runat="server" ImageUrl="~/Pages/images/alert2.gif" 
                                                            Visible="False" />
                                                        <asp:Label ID="LblOkPdv0" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                            Text="Todos los punto de venta están asignados por lo menos a un Personal" 
                                                            Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="panelConsultaPDV" runat="server">
                                            <asp:Label ID="LblSelBuscar" runat="server" Text="Selecione un Operativo de la lista para visualizar la asignación existente..."></asp:Label>
                                            <br />
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Label ID="LblSelBuscaroperativo" runat="server" 
                                                        Text="Personal Operativo" Font-Bold="True" ></asp:Label>
                                                        <br />
                                                        <asp:ListBox ID="LstBoxSelBuscarOperativos" runat="server" Width="300px" 
                                                        Height="140px" Font-Names="Verdana" 
                                                        Font-Size="9pt" AutoPostBack="True" CausesValidation="True" 
                                                        onselectedindexchanged="LstBoxSelBuscarOperativos_SelectedIndexChanged">
                                                        </asp:ListBox>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Label ID="LblBuscarAsignacion" runat="server" 
                                                        Text="Asignación de Puntos de venta  realizada" Font-Bold="True" ></asp:Label>
                                                        <br />
                                                        <asp:GridView ID="GvBuscarAsignados" runat="server" AllowPaging="True" 
                                                        GridLines="None" PageSize="5"
                                                        Font-Names="Verdana" Font-Size="8pt" 
                                                        ToolTip="Oprima el botón rojo para quitar asignación a este Operativo" 
                                                        onpageindexchanging="GvBuscarAsignados_PageIndexChanging" 
                                                        onselectedindexchanged="GvBuscarAsignados_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png"  
                                                                SelectText="" ShowSelectButton="True" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="NewAsign" runat="server" 
                                                        Text="Nueva Asignación" CssClass="button" Width="130px" 
                                                        onclick="NewAsign_Click"/>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnAsignarPDV" runat="server" 
                                                        Text="Asignar" CssClass="button" Width="84px" 
                                                        onclick="BtnAsignarPDV_Click" Visible="False" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnBuscarAsignacionPDV" runat="server" 
                                                        Text="Consultar" CssClass="button" Width="84px" onclick="BtnBuscarAsignacionPDV_Click"/>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnCancelVistaPDV" runat="server" 
                                                        Text="Cancelar" CssClass="button" Width="84px" 
                                                         onclick="BtnCancelVistaPDV_Click" Visible="False"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>                                                                
                                <cc1:TabPanel ID="TabProductos" runat="server" HeaderText="Asignación de Productos">
                                    <HeaderTemplate>Asignación de Productos</HeaderTemplate>
                                    <ContentTemplate>
                                        <br />
                                        <br />
                                        <asp:Panel ID="panelAsignaProductos" runat="server">
                                            <table align="center">
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Label ID="LblSeloperativoProd" runat="server" 
                                                        Text="Personal Operativo" Font-Bold="True" ></asp:Label>
                                                        <br />
                                                        <asp:ListBox ID="LstBoxSelOperativoProd" runat="server" Width="300px" 
                                                        Height="140px" Font-Names="Verdana" 
                                                        Font-Size="9pt"></asp:ListBox>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Label ID="LblSelProd" runat="server" 
                                                        Text="Productos a asignar" Font-Bold="True" ></asp:Label>
                                                        <br />
                                                        <asp:ListBox ID="LstBoxProd" runat="server" Width="300px" Height="140px" 
                                                        SelectionMode="Multiple" CausesValidation="True" Font-Names="Verdana" 
                                                        Font-Size="9pt">
                                                        </asp:ListBox>
                                                    </td>
                                                    <td width="80px" align="center" valign="top">
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <asp:Button ID="BtnMasAsingProd" runat="server" Text=" ." Width="85px" CssClass="pagnext" 
                                                            ToolTip="Asignar" OnClick="BtnMasAsingProd_Click" />
                                                        <br />
                                                        <br />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Label ID="LblAsignacionProd" runat="server" Text="Asignación realizada" Font-Bold="True" >
                                                        </asp:Label>
                                                        <br />
                                                        <asp:GridView ID="GvAsignadosProd" runat="server" AllowPaging="True" 
                                                        GridLines="None" PageSize="15" 
                                                        OnPageIndexChanging="GvAsignadosProd_PageIndexChanging" 
                                                        OnSelectedIndexChanged="GvAsignadosProd_SelectedIndexChanged" 
                                                        Font-Names="Verdana" Font-Size="8pt">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" 
                                                                SelectText="" ShowSelectButton="True" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="panelConsultaPRODUCTOS" runat="server">
                                            <asp:Label ID="LblSelBuscarProd" runat="server" Text="Selecione un Operativo de la lista para visualizar la asignación existente..."></asp:Label>
                                            <br />
                                            <br />
                                            <table align="center">
                                                <tr>
                                                    <td valign="top">
                                                        <asp:Label ID="LblSelBuscaroperativoProd" runat="server" 
                                                        Text="Personal Operativo" Font-Bold="True" ></asp:Label>
                                                        <br />
                                                        <asp:ListBox ID="LstBoxSelBuscarOperativosProd" runat="server" Width="300px" 
                                                        Height="140px" Font-Names="Verdana" 
                                                        Font-Size="9pt" AutoPostBack="True" CausesValidation="True" 
                                                        onselectedindexchanged="LstBoxSelBuscarOperativosProd_SelectedIndexChanged"></asp:ListBox>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Label ID="LblBuscarAsignacionProd" runat="server" 
                                                        Text="Asignación de Productos realizada" Font-Bold="True" ></asp:Label>
                                                        <br />
                                                        <asp:GridView ID="GvBuscarAsignadosProd" runat="server" AllowPaging="True" 
                                                        GridLines="None" PageSize="5"
                                                        Font-Names="Verdana" Font-Size="8pt" 
                                                        ToolTip="Oprima el botón rojo para quitar asignación a este Operativo" 
                                                        onpageindexchanging="GvBuscarAsignadosProd_PageIndexChanging" 
                                                        onselectedindexchanged="GvBuscarAsignadosProd_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png"  
                                                                SelectText="" ShowSelectButton="True" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <table align="center">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="NewAsignProd" runat="server" 
                                                        Text="Nueva Asignación" CssClass="button" Width="130px" 
                                                        OnClick="NewAsignProd_Click"/>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnAsignarProd" runat="server" 
                                                        Text="Asignar" CssClass="button" onclick="BtnAsignarProd_Click" 
                                                        Visible="False" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnBuscarAsignacionPRODUCTO" runat="server" 
                                                        Text="Consultar" CssClass="button" Width="84px" 
                                                        onclick="BtnBuscarAsignacionPRODUCTO_Click"/>
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnCancelVistaPRODUCTO" runat="server" 
                                                        Text="Cancelar" CssClass="button" Width="84px" Visible="False" 
                                                         onclick="BtnCancelVistaPRODUCTO_Click"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PAnalisis" runat="server">
                <table align="center">
                    <tr>                        
                        <td>
                            <fieldset id="ContenPlanAnalisis" runat="server" 
                                style="border-color:#000000; border-width:1px;">
                                <legend style="color:#CC3399; font-weight:bold; margin-left:10px " >Auditoría de Información</legend>
                                <br />
                                <asp:GridView ID="gvplanningAnalisis" runat="server"                                     
                                    AutoGenerateSelectButton="True" GridLines="Horizontal"
                                    CellPadding="4" 
                                    onselectedindexchanged="gvplanningAnalisis_SelectedIndexChanged" 
                                    CssClass="altoverow" Font-Names="Verdana" Font-Size="9pt" 
                                    ForeColor="Black">                                   
                                    <SelectedRowStyle CssClass="altoverow" Font-Bold="False" />
                                    <HeaderStyle BackColor="#E9ECF0" Font-Size="9pt" ForeColor="Black" 
                                        HorizontalAlign="Center" VerticalAlign="Middle" />                                   
                                </asp:GridView>    
                                <br />
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <hr  align="center" style="background-color:#D2E0FF; height: 3px;" 
                    width="90%" />               
                <br />
                <table width="100%" align="center" >
                    <tr>
                        <td>
                             <asp:UpdatePanel ID="updatepanel1" runat="server" >
                                <ContentTemplate>
                                    <asp:Panel ID="PanelVistaAnalisis" runat="server">
                                        <table align="center"  >
                                            <tr>
                                                <td>
                                                    <div id="mainContent" >
                                                        <!-- Contenido de pagina -->                                                    
                                                        <div id="leftSide" >                                                        
                                                            <p class="altrow">
                                                                &nbsp;</p>
                                                            <asp:Label ID="LblTip" runat="server" Text="Informes" CssClass="selectedrow"></asp:Label>
                                                            <asp:ImageButton ID="BtnAdd" runat="server" ImageUrl="~/Pages/images/add.png" 
                                                                onclick="BtnAdd_Click" Visible="False"/>
                                                            <asp:ImageButton ID="BtnRest" runat="server" 
                                                                ImageUrl="~/Pages/images/delete.png" onclick="BtnRest_Click" />
                                                            <br />                                                     
                                                            <br />
                                                            <asp:Menu ID="MenuDinamico" runat="server" Orientation="Vertical"                 
                                                                CssSelectorClass="menu" 
                                                                onmenuitemclick="MenuDinamico_MenuItemClick">                                                            
                                                            </asp:Menu>
                                                            <p class="altrow">
                                                                &nbsp;</p>                             
                                                            <asp:Label ID="LblSolicitudes" runat="server" Text="Solicitudes" 
                                                                CssClass="selectedrow"></asp:Label>
                                                            <br />
                                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                                                ImageUrl="~/Pages/images/mailreminder.png" Height="58px" 
                                                                Width="83px"  />
                                                                <asp:Panel ID="PanelCorreos" runat="server" style="display:none;" >
                                                                    <table align="center" border="0" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <img alt="sup1" height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup1.png" width="6"></img> 
                                                                            </td>
                                                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                                                <img height="6" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="1"></img> 
                                                                            </td>
                                                                            <td>
                                                                                <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_sup3.png" width="6"></img> 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                                                <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6"></img> 
                                                                            </td>
                                                                            <td bgcolor="#CCD4E1" valign="top">
                                                                                
                                                                                    <br />
                                                                                    <table align="center">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Image ID="ImgMail" runat="server" Height="62px" 
                                                                                                    ImageUrl="~/Pages/images/mailreminder.png" Width="83px" />
                                                                                            </td>
                                                                                            <td align="center">
                                                                                                <asp:Label ID="LblTitEnvioMail" runat="server" CssClass="labelsTitN" 
                                                                                                    Text="Novedades / Solicitudes"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    <table align="center">
                                                                                        <tr>
                                                                                            <td valign="top">
                                                                                                <asp:Label ID="LblMailSolicitante" runat="server" CssClass="labelsN" Text="De"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TxtSolicitante" runat="server" Enabled="False" Width="400px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td valign="top">
                                                                                                <asp:Label ID="LblSelPara" runat="server" CssClass="labelsN" Text="Dirigido a:"></asp:Label>                                                                                            
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:RadioButtonList ID="RbtnSelPara" runat="server" 
                                                                                                    RepeatDirection="Horizontal" 
                                                                                                    onselectedindexchanged="RbtnSelPara_SelectedIndexChanged" 
                                                                                                    AutoPostBack="True">
                                                                                                    <asp:ListItem Text="BackOffice" Value="0"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Ejecutivo de Cuenta" Value="1"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Director de Cuenta" Value="2"></asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td valign="top">
                                                                                                <asp:Label ID="LblMailPara" runat="server" CssClass="labelsN" Text="Para"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TxtEmail" runat="server" Enabled="False" Width="400px" 
                                                                                                    ></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td valign="top">
                                                                                                <asp:Label ID="LblMotivo" runat="server" CssClass="labelsN" Text="Asunto"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TxtMotivo" runat="server" MaxLength="50" Width="400px"></asp:TextBox>
                                                                                                <asp:RegularExpressionValidator ID="Reqmotivo" runat="server" ControlToValidate="TxtMotivo" Display="None"
                                                                                                    ErrorMessage="Sr. Usuario, por favor no ingrese caracteres especiales y recuerde que no debe inicial con número"
                                                                                                    ValidationExpression="([a-zA-Z][a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]{0,49})">
                                                                                                </asp:RegularExpressionValidator>
                                                                                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="Reqmotivo">
                                                                                                </cc1:ValidatorCalloutExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td valign="top">
                                                                                                <asp:Label ID="LblMensaje" runat="server" CssClass="labelsN" Text="Mensaje"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TxtMensaje" runat="server" Height="113px" TextMode="MultiLine" Width="400px"></asp:TextBox>
                                                                                                <asp:RegularExpressionValidator ID="ReqMensaje" runat="server" 
                                                                                                    ControlToValidate="TxtMensaje" Display="None"
                                                                                                    ErrorMessage="Por favor no exceda 500 caracteres" 
                                                                                                    ValidationExpression="([\w\W]{0,500})">
                                                                                                </asp:RegularExpressionValidator>                                                                                                
                                                                                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="ReqMensaje">
                                                                                                </cc1:ValidatorCalloutExtender>                                                
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <br />
                                                                                    <table align="center">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:ImageButton ID="ImgEnviarMail" runat="server" AlternateText="Enviar" 
                                                                                                    BorderStyle="None" ImageUrl="../../images/BtnEnviarMail.png"
                                                                                                    onmouseout="this.src = '../../images/BtnEnviarMail.png'" onmouseover="this.src = '../../images/BtnEnviarMailDown.png'"
                                                                                                    style="margin-left: 0px" Height="26px" 
                                                                                                    Width="53px" onclick="ImgEnviarMail_Click" />
                                                                                                <asp:ImageButton ID="ImgCancelMail" runat="server" AlternateText="Cancelar" BorderStyle="None" ImageUrl="../../images/BtnCancelReg.png"
                                                                                                    onmouseout="this.src = '../../images/BtnCancelReg.png'" onmouseover="this.src = '../../images/BtncancelRegDown.png'" 
                                                                                                    style="margin-left: 0px" Height="26px" 
                                                                                                    Width="53px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                               
                                                                            </td>
                                                                            <td background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                                                <img height="1" src="../../images/tablas_genericas/bg_gris_tablasgene.jpg" width="6"></img> 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png" width="6"></img>
                                                                            </td>
                                                                            <td align="center" background="../../images/tablas_genericas/bg_gris_tablasgene.jpg">
                                                                                <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf1.png" width="1"></img>
                                                                            </td>
                                                                            <td>
                                                                                <img height="6" src="../../images/tablas_genericas/bordegris_tablasgene_inf3.png" width="6"></img>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    </asp:Panel>
                                                                    <asp:Button ID="Btndisparapanelcorreo" runat="server" Text="" Visible="true" Width="0px" CssClass="alertas"/>            
                                                                     <cc1:ModalPopupExtender ID="ModalPopupCorreos" runat="server" Enabled="True" 
                                                                        OkControlID="ImgCancelMail" PopupControlID="PanelCorreos" 
                                                                        TargetControlID="ImageButton1" 
                                                                        BackgroundCssClass="modalBackground" DropShadow="True">
                                                                    </cc1:ModalPopupExtender>
                                                        </div>
                                                        <!-- Contenedor principal -->
                                                        <div id="contenido" >
                                                            <table align="center">
                                                                <tr>
                                                                    <td>                                                                    
                                                                        <asp:Panel ID="panelcontenido" runat="server" >
                                                                            <div>
                                                                                <br />
                                                                                &nbsp;&nbsp;&nbsp;<asp:Label ID="Lbltitulo" runat="server" Font-Bold="True"></asp:Label>
                                                                            </div>
                                                                            <br />
                                                                            <table>
                                                                                <tr>
                                                                                    <td> 
                                                                                        <asp:Panel ID="panelActPropia" runat="server">                                                      
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td valign="top">
                                                                                                        <table class="altoverow" >
                                                                                                            <tr>
                                                                                                                <td valign="top">                                                                                                                                                                                                                                                                                                                                                
                                                                                                                    <asp:Label ID="Label1" runat="server" Text="Desde"></asp:Label>                                                                                                                                                                                                                                                                                                                                                
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="TxtFechaEjec" runat="server" Width="70px" AutoPostBack="True" 
                                                                                                                        CausesValidation="True" ontextchanged="TxtFechaEjec_TextChanged">
                                                                                                                    </asp:TextBox>                                                                                
                                                                                                                    <cc1:MaskedEditExtender ID="TxtFechaEjec_MaskedEditExtender" runat="server"
                                                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechaEjec"
                                                                                                                        UserDateFormat="DayMonthYear">
                                                                                                                    </cc1:MaskedEditExtender>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:ImageButton ID="ImgCalendar" runat="server" 
                                                                                                                    ImageUrl="~/Pages/images/calendario.JPG" />
                                                                                                                    <asp:Label ID="LblFIObl" runat="server" Text="*" Font-Bold="True" 
                                                                                                                        Font-Size="10px" ForeColor="Red"></asp:Label>
                                                                                                                </td>                                                                                                                                
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td valign="top">                                                                                                                                                                                                                                                                                                                                                
                                                                                                                    <asp:Label ID="Label2" runat="server" Text="Hasta"></asp:Label>                                                                                                                                                                                                                                                                                                                                                
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="TxtFechaFinEjec" runat="server" Width="70px" 
                                                                                                                        AutoPostBack="True" CausesValidation="True" Enabled="false" 
                                                                                                                        ontextchanged="TxtFechaFinEjec_TextChanged">
                                                                                                                    </asp:TextBox>
                                                                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                                                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                                                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                                                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechaFinEjec"
                                                                                                                        UserDateFormat="DayMonthYear">
                                                                                                                    </cc1:MaskedEditExtender>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:ImageButton ID="ImgCalendar2" runat="server" Enabled="false"
                                                                                                                    ImageUrl="~/Pages/images/calendario.JPG" />
                                                                                                                    <asp:Label ID="LblFFObl" runat="server" Text="*" Font-Bold="True" 
                                                                                                                        Font-Size="10px" ForeColor="Red"></asp:Label>                                                                                
                                                                                                                </td>                                                                                                                                
                                                                                                            </tr>
                                                                                                        </table>  
                                                                                                        <br />
                                                                                                        <br />
                                                                                                        <table align="center">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:Button ID="BtnConsultarAnalisis" runat="server" CssClass="button" 
                                                                                                                    Text="Ejecutar Consulta" onclick="BtnConsultarAnalisis_Click" /> 
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                        <cc1:CalendarExtender ID="TxtFechaEjec_CalendarExtender" runat="server" 
                                                                                                            Format="dd/MM/yyyy" PopupButtonID="ImgCalendar" 
                                                                                                            TargetControlID="TxtFechaEjec">
                                                                                                        </cc1:CalendarExtender> 
                                                                                                        <cc1:CalendarExtender ID="TxtFechaFinEjec_CalendarExtender" runat="server" 
                                                                                                            Format="dd/MM/yyyy" PopupButtonID="ImgCalendar2" 
                                                                                                            TargetControlID="TxtFechaFinEjec">
                                                                                                        </cc1:CalendarExtender>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <table class="altoverow" >
                                                                                                            <tr>
                                                                                                                <td valign="top">                                                                                                                                                                                                                                                                                                                                                
                                                                                                                    <asp:Label ID="LblOperativoAnalisis" runat="server" Text="Operativo"></asp:Label>                                                                                                                                                                                                                                                                                                                                                
                                                                                                                    <asp:Label ID="LblOpeObl" runat="server" Text="*" Font-Bold="True" 
                                                                                                                        Font-Size="10px" ForeColor="Red"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <div class="ScrollOpeAnalisis">
                                                                                                                        <asp:CheckBox ID="ChkAllOperativos" runat="server" AutoPostBack="True" Font-Names="Arial" 
                                                                                                                            Font-Size="8pt" ForeColor="Black" 
                                                                                                                            Text="Todos" oncheckedchanged="ChkAllOperativos_CheckedChanged"/>
                                                                                                                        <br />                                                                        
                                                                                                                        <asp:RadioButtonList ID="ChkLstOpeAnalisis" runat="server" Font-Names="Arial" 
                                                                                                                            Font-Size="8pt" AutoPostBack="True" 
                                                                                                                            CausesValidation="True" 
                                                                                                                            onselectedindexchanged="ChkLstOpeAnalisis_SelectedIndexChanged">
                                                                                                                        </asp:RadioButtonList>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                                <td valign="top">                                                                                                                                                                                                                                                                                                                                                
                                                                                                                    <asp:Label ID="LblPuntoVenta" runat="server" Text="Punto de Venta"></asp:Label>                                                                                                                                                                                                                                                                                                                                                
                                                                                                                    <asp:Label ID="LblPdvObl" runat="server" Font-Bold="True" Font-Size="10px" 
                                                                                                                        ForeColor="Red" Text="*"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <div class="ScrollpdvAnalisis">
                                                                                                                        <asp:CheckBox ID="ChkAllPDV" runat="server" AutoPostBack="True" Font-Names="Arial" 
                                                                                                                            Font-Size="8pt" ForeColor="Black" 
                                                                                                                            Text="Todos" oncheckedchanged="ChkAllPDV_CheckedChanged" Visible="False"/>
                                                                                                                        <br />
                                                                                                                        <asp:RadioButtonList ID="RbtnLstPDVAnalisis" runat="server" Font-Names="Arial"
                                                                                                                             Font-Size="8pt" AutoPostBack="true"
                                                                                                                             CausesValidation="true" OnSelectedIndexChanged="RbtnLstPDVAnalisis_SelectedIndexChanged">
                                                                                                                        </asp:RadioButtonList>                                                                                                                                                                      
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                                <td valign="top">                                                                                                                                                                                                                                                                                                                                                
                                                                                                                    <asp:Label ID="LblProductos" runat="server" Text="Producto"></asp:Label>                                                                                                                                                                                                                                                                                                                                                
                                                                                                                    <asp:Label ID="LblProdObl" runat="server" Font-Bold="True" Font-Size="10px" 
                                                                                                                        ForeColor="Red" Text="*"></asp:Label>
                                                                                                                    <br />
                                                                                                                    <div class="ScrollprodAnalisis">
                                                                                                                        <asp:CheckBox ID="ChkAllPRODUCTO" runat="server" AutoPostBack="True" Font-Names="Arial" 
                                                                                                                            Font-Size="8pt" ForeColor="Black" 
                                                                                                                            Text="Todos" oncheckedchanged="ChkAllPRODUCTO_CheckedChanged" 
                                                                                                                            Visible="False"/>
                                                                                                                        <br />
                                                                                                                        <asp:CheckBoxList ID="ChkLstProdAnalisis" runat="server" Font-Names="Arial" 
                                                                                                                            Font-Size="8pt" RepeatLayout="Flow">
                                                                                                                        </asp:CheckBoxList>
                                                                                                                    </div>                                                                                                                                               
                                                                                                                </td>    
                                                                                                            </tr>
                                                                                                        </table>   
                                                                                                    </td>                                                                
                                                                                                </tr>
                                                                                            </table>
                                                                                            <br />
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:GridView ID="GvDatosAnalisis" runat="server" CssClass="altoverow" 
                                                                                                            EmptyDataText="Sr. Usuario el filtro de consulta no arrojo ninguna respuesta">
                                                                                                        </asp:GridView> 
                                                                                                        
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Panel ID="panelActComercio" runat="server">
                                                                                            <table align="center">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:GridView ID="GVActComercio" runat="server" CssClass="altoverow" Width="100%"                                                                                                 
                                                                                                            EmptyDataText="Sr. Usuario este planning no posee información de actividades en el comercio" 
                                                                                                            onselectedindexchanged="GVActComercio_SelectedIndexChanged"> 
                                                                                                             <Columns>
                                                                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/ViewDetails.png"
                                                                                                                SelectText="" ShowSelectButton="True"  />
                                                                                                            </Columns>
                                                                                                        </asp:GridView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                               
                                                                                            </table>
                                                                                        </asp:Panel>                                                                            
                                                                                    </td>
                                                                                </tr>
                                                                                <tr >
                                                                                    <td >
                                                                                        <asp:Panel ID="panelActPhotoPropia" runat="server">
                                                                                            <table align="center">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:GridView ID="GVPhotoActPropia" runat="server" CssClass="altoverow" Width="100%"
                                                                                                            
                                                                                                            EmptyDataText="Sr. Usuario este planning no posee registros fotográficos de la actividad." 
                                                                                                            onselectedindexchanged="GVPhotoActPropia_SelectedIndexChanged"> 
                                                                                                             <Columns>
                                                                                                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/ViewDetails.png"
                                                                                                                SelectText="" ShowSelectButton="True"  />
                                                                                                            </Columns>
                                                                                                        </asp:GridView>
                                                                                                    </td>
                                                                                                </tr>
                                                                                               
                                                                                            </table>
                                                                                        </asp:Panel>                                                                            
                                                                                    </td>
                                                                                </tr>
                                                                            </table>                                                              
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Alertas" runat="server" Width="375px" CssClass="MensajesSupervisor" Height="215px" DefaultButton="BtnAceptarAlert"  style="display:none;">
                <table align="center" style="height: 164px">
                    <tr>
                        <td class="style4">                        
                        </td>
                        <td class="style3">
                            <br />
                            <asp:Label ID="LblAlert" runat="server" CssClass="labelsTit"></asp:Label>
                            <br />
                            <br />
                            <br />
                            <asp:Label ID="LblFaltantes" runat="server" CssClass="labels"></asp:Label>                                
                            <br />
                            <br />
                        </td>
                    </tr>                                            
                </table>
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button ID="BtnAceptarAlert" runat="server" CssClass="button" 
                            Text="Aceptar" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="Btndisparaalertas" runat="server" Text="" Visible="true" Width="0px" CssClass="alertas"/>            
            <cc1:ModalPopupExtender ID="ModalPopupAlertas" runat="server" 
                DropShadow="True" Enabled="True" 
                OkControlID="BtnAceptarAlert" PopupControlID="Alertas" 
                TargetControlID="Btndisparaalertas" 
                BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>   
            
             <asp:Panel ID="PanelFotosComercio" runat="server" style="display:none;" 
                CssClass="CargaArchivos" Height="447px" Width="432px">                
                <div class="ScrollActividadesphotoSupervisor">
                    <asp:GridView ID="gvactivi" runat="server" AutoGenerateColumns="False"                                                                         
                        EmptyDataText="No existen registros fotográficos de esta actividad del comercio.">                                                                                                   
                        <Columns>                                                                                                                      
                            <asp:TemplateField HeaderText="Foto">
                                <ItemTemplate>
                                    <asp:Image ID="ImgPhotoa" runat="server" Height="300px" 
                                       ImageAlign="Middle" Width="400px" ToolTip="Ingrese Comentarios de la fotografía si lo requiere" />
                                    <br />
                                    <br />
                                    <asp:Label ID="LblObsFoto" runat="server" Text="Observación" Font-Bold="True"></asp:Label>
                                    <asp:Label ID="LblNumFoto" runat="server" Text=""  Visible="false"></asp:Label>                                    
                                    <br />
                                    <asp:TextBox ID="txtcomentario" runat="server" Height="50px" 
                                    TextMode="MultiLine" Width="400px" ToolTip="Ingrese Comentarios de la fotografía si lo requiere"></asp:TextBox>         
                                </ItemTemplate>
                            </asp:TemplateField>                                                                     
                        </Columns>                                   
                    </asp:GridView>
                </div>                                                    
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button ID="ClosePActividad" runat="server" CssClass="button" 
                            Text="Cerrar" />
                        </td>
                        <td>
                            <asp:Button ID="BtnUpdateComment" runat="server" CssClass="button" 
                            Text="Actualizar" onclick="BtnUpdateComment_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="Btndisparapanelfotos" runat="server" Text="" Visible="true" Width="0px" CssClass="alertas"/>            
            <cc1:ModalPopupExtender ID="ModalPopupFotosComercio" runat="server" Enabled="True" 
                OkControlID="ClosePActividad" PopupControlID="PanelFotosComercio" 
                TargetControlID="Btndisparapanelfotos" 
                BackgroundCssClass="modalBackground" DropShadow="True">
            </cc1:ModalPopupExtender>
            
              <asp:Panel ID="PanelFotosPropias" runat="server" style="display:none;"  
                CssClass="CargaArchivos">                
                <div class="ScrollActividadesphotopropSupervisor">
                    <table>
                        <tr>
                            <td>                           
                                <asp:GridView ID="gvactivPropia" runat="server" AutoGenerateColumns="False"                                                                         
                                    
                                    EmptyDataText="No existen registros fotográficos de esta actividad del comercio.">                                                                                                   
                                    <Columns>                                                                                                                      
                                        <asp:TemplateField HeaderText="Foto">
                                            <ItemTemplate>
                                                <asp:Image ID="ImgPhotoap" runat="server" Height="300px" 
                                                   ImageAlign="Middle" Width="400px" ToolTip="Ingrese Comentarios de la fotografía si lo requiere" />
                                                <br />
                                                <br />
                                                <asp:Label ID="LblObsFoto" runat="server" Text="Observación" Font-Bold="True"></asp:Label>
                                                <asp:Label ID="LblNumFoto" runat="server" Text=""  Visible="false"></asp:Label>                                    
                                                <br />                                                                            
                                                <asp:TextBox ID="txtcomentario" runat="server" Height="50px" 
                                                TextMode="MultiLine" Width="400px" ToolTip="Ingrese Comentarios de la fotografía si lo requiere"></asp:TextBox>         
                                                <br />
                                                <asp:RegularExpressionValidator ID="ReqTxtComment" runat="server" 
                                                    ControlToValidate="txtcomentario" Display="Dynamic"
                                                    ErrorMessage="Por favor no exceda 255 caracteres" 
                                                    ValidationExpression="([\w\W]{0,255})">
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                     
                                    </Columns>                                   
                                </asp:GridView> 
                            </td>
                        </tr>
                    </table>
                </div>                                                    
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button ID="ClosePActivPropia" runat="server" CssClass="button" 
                            Text="Cerrar" />
                        </td>
                        <td>
                            <asp:Button ID="BtnUpdateCommenPropia" runat="server" CssClass="button" 
                            Text="Actualizar" onclick="BtnUpdateCommenPropia_Click"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Button ID="BtndisparapanelfotosPropia" runat="server" Text="" Visible="true" Width="0px" CssClass="alertas"/>            
            <cc1:ModalPopupExtender ID="ModalPopupFotosPropia" runat="server" Enabled="True" 
                OkControlID="ClosePActivPropia" PopupControlID="PanelFotosPropias" 
                TargetControlID="BtndisparapanelfotosPropia" 
                BackgroundCssClass="modalBackground" DropShadow="True">
            </cc1:ModalPopupExtender>
            
           
            
            
            
                                  
            <div id="footer" align="center" ></div>
        </ContentTemplate>    
    </asp:UpdatePanel>    
    </form>    
</body>
</html>