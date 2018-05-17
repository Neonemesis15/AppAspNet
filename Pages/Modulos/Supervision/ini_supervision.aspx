<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ini_supervision.aspx.cs" Inherits="SIGE.Pages.Modulos.Supervision.ini_supervision" Culture="auto" UICulture="auto"  %>

<%@ Register assembly="eWorld.UI.Compatibility" namespace="eWorld.UI.Compatibility" tagprefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">    
    <title>Xplora - Supervisión</title>
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/stilo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/Menu.css" rel="stylesheet" type="text/css" />
    <style type="text/css">            
       
        .prueba
        {        
        	background-image:"~/Pages/images/calendario.JPG" 
        }       
     
        .style3
        {
            width: 278px;
        }
        .style4
        {
            width: 80px;
        }
        #iframePrevFormat
        {
            top: 226px;
            left: 163px;
            height: 450px;
        }
         .style13
        {
            width: 402px;
        }       
        .style15
        {
            width: 73px;
        }
        
     
       
     
       
        
     
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True"  ></asp:ScriptManager>
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
        <asp:UpdatePanel ID="updatepanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Panel ID="panel1" runat="server">                                                
                        <table align="center">                        
                            <tr>
                                <td>
                                     <div>
                                        <asp:Button ID="Btndisparaalertas" runat="server" Text="" Visible="true"
                                        Width="0px" CssClass="alertas"/>
                                        <asp:Button ID="BtndisparaConfirmSI" runat="server" Text="" Visible="true"
                                        Width="0px" CssClass="alertas"/>                                
                                        <asp:Menu id="MenuSupervisor" Runat="server" CssSelectorClass="menu" 
                                            Orientation="Horizontal" onmenuitemclick="MenuSupervisor_MenuItemClick" 
                                             DynamicHorizontalOffset="10">                                            
                                            <Items>
                                                <asp:MenuItem Text="Distribución de Tareas a Operativos" Value="0" />                                                
                                                <asp:MenuItem Text="Análisis de Resultados" Value="1" />                                                
                                            </Items>                                          
                                        </asp:Menu>
                                    </div>   
                                </td>
                            </tr>
                        </table>
                        <table align="center">
                            <tr>
                                <td>                        
                                    <asp:MultiView ID="Multiview" runat="server">
                                        <br />
                                        <asp:View ID="VTareas" runat="server">                                            
                                            <table align="center" width="100%">
                                                <tr> 
                                                    <td>
                                                        <fieldset id="Contenformatos" runat="server" 
                                                            style="border-color:White; border-width:1px; " 
                                                            visible="True" >
                                                            <legend style="color:Fuchsia; font-weight:bold ">Datos del Planning</legend> 
                                                            <br />                                                            
                                                            <br />                                                            
                                                            <asp:GridView ID="gvformatos" runat="server" BackColor="White" 
                                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                                                Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" 
                                                                onselectedindexchanged="gvformatos_SelectedIndexChanged" 
                                                                AutoGenerateSelectButton="True" GridLines="Vertical">
                                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                                            </asp:GridView>
                                                            <br />
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnPrevForm" runat="server" CssClass="button" 
                                                                            Text="Previsualizar formato" Visible="False" onclick="btnPrevForm_Click" />
                                                                        <asp:Button ID="BtnEnviarForm" runat="server" CssClass="button" 
                                                                            Text="Enviar formato" Visible="False" />
                                                                        <asp:Button ID="BtnEnviarPDV" runat="server" CssClass="button" 
                                                                            Text="Enviar PDV" Visible="False" />
                                                                        <asp:Button ID="btnGenForm" runat="server" CssClass="button" 
                                                                            Text="Generar formato" Visible="False" />
                                                                        <asp:Button ID="BtnConfirmEnvia" runat="server" CssClass="button" 
                                                                            Text="Confirmar envio" Visible="False" />
                                                                        <asp:Button ID="btnPreviInf" runat="server" CssClass="button" 
                                                                            Text="Previsualizar informes" Visible="False" />
                                                                        <asp:Button ID="BtnEnviarInfor" runat="server" CssClass="button" 
                                                                            Text="Enviar Informes" Visible="False" />
                                                                        <asp:Button ID="btnCancelVista" runat="server" CssClass="button" 
                                                                            Text="Cancelar" onclick="btnCancelVista_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table align="center">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="LblSeloperativo" runat="server" CssClass="labels" 
                                                                            Text="Personal Operativo" Visible="False"></asp:Label>
                                                                        <br />
                                                                        
                                                                        <asp:CheckBoxList ID="ckbSelOperativos" runat="server" 
                                                                            Height="220px" RepeatLayout="Flow" Width="320px">
                                                                        </asp:CheckBoxList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblSelpdv" runat="server" CssClass="labels" 
                                                                            Text="Puntos a Visitar" Visible="False"></asp:Label>
                                                                        <br />
                                                                        <asp:CheckBoxList ID="CkbSelpdv" runat="server" Height="220px" 
                                                                            onselectedindexchanged="CkbSelpdv_SelectedIndexChanged" RepeatLayout="Flow" 
                                                                            Width="320px">
                                                                        </asp:CheckBoxList>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblSelFormatos" runat="server" CssClass="labels" 
                                                                            Text="Formatos de levantamiento de información:" Visible="False"></asp:Label>
                                                                        <br />
                                                                        <asp:CheckBoxList ID="CkbSelFormatos" runat="server" AutoPostBack="True" 
                                                                             Height="220px" 
                                                                            onselectedindexchanged="CkbSelFormatos_SelectedIndexChanged" 
                                                                            RepeatLayout="Flow" Width="320px">
                                                                        </asp:CheckBoxList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                           <%-- <asp:FormView ID="FormView1" runat="server" AllowPaging="True" CellPadding="4" 
                                                                DataSourceID="SqlDataSource1" DefaultMode="Edit" ForeColor="#333333" 
                                                                Width="446px">
                                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="#EFF3FB" />
                                                                <EditItemTemplate>
                                                                    <table align="center">
                                                                        <tr>
                                                                            <td>
                                                                            Cod Punto de venta:
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="id_PointOfSaleTextBox" runat="server" 
                                                                                Text='<%# Bind("id_PointOfSale") %>' />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                cod_Point:
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="cod_PointTextBox" runat="server" 
                                                                                Text='<%# Bind("cod_Point") %>' />
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                    </table>
                                                                   
                                                                    
                                                                    <br />
                                                                    
                                                                    
                                                                    <br />
                                                                    Item_DetDescription:
                                                                    <asp:TextBox ID="Item_DetDescriptionTextBox" runat="server" 
                                                                        Text='<%# Bind("Item_DetDescription") %>' />
                                                                    <br />
                                                                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CssClass="button"
                                                                        CommandName="Update" Text="Agregar" />
                                                                    &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CssClass="button" 
                                                                        CausesValidation="False" CommandName="Cancel" Text="Enviar" />
                                                                        &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" 
                                                                        CausesValidation="False" CommandName="Cancel" Text="Cancelar" />
                                                                        
                                                                </EditItemTemplate>
                                                                <InsertItemTemplate>
                                                                    id_PointOfSale:
                                                                    <asp:TextBox ID="id_PointOfSaleTextBox" runat="server" 
                                                                        Text='<%# Bind("id_PointOfSale") %>' />
                                                                    <br />
                                                                    cod_Point:
                                                                    <asp:TextBox ID="cod_PointTextBox" runat="server" 
                                                                        Text='<%# Bind("cod_Point") %>' />
                                                                    <br />
                                                                    Item_DetDescription:
                                                                    <asp:TextBox ID="Item_DetDescriptionTextBox" runat="server" 
                                                                        Text='<%# Bind("Item_DetDescription") %>' />
                                                                    <br />
                                                                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                                                                        CommandName="Insert" Text="Insertar" />
                                                                    &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                                                                        CausesValidation="False" CommandName="Cancel" Text="Cancelar" />
                                                                </InsertItemTemplate>
                                                                <ItemTemplate>
                                                                    Código de punto de venta:
                                                                    <asp:Label ID="id_PointOfSaleLabel" runat="server" 
                                                                        Text='<%# Bind("id_PointOfSale") %>' />
                                                                    <br />
                                                                    cod_Point:
                                                                    <asp:Label ID="cod_PointLabel" runat="server" Text='<%# Bind("cod_Point") %>' />
                                                                    <br />
                                                                    Item_DetDescription:
                                                                    <asp:Label ID="Item_DetDescriptionLabel" runat="server" 
                                                                        Text='<%# Bind("Item_DetDescription") %>' />
                                                                    <br />
                                                                </ItemTemplate>
                                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                <EditRowStyle BackColor="#2461BF" />
                                                            </asp:FormView>
                                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:ConectaDBLucky %>" 
                                                                SelectCommand="SELECT PointOfSale_Planning.id_PointOfSale, Item_Points.cod_Point, Item_Point_Detalle.Item_DetDescription FROM Item_Point_Detalle INNER JOIN Item_Points ON Item_Point_Detalle.cod_item = Item_Points.cod_Item CROSS JOIN PointOfSale_Planning">
                                                            </asp:SqlDataSource>--%>
                                                            <br />
                                                            <br />
                                                            <table align="center">                                                                                                                       
                                                            </table>                                                            
                                                            <br />
                                                            <br />
                                                            <br />
                                                        </fieldset>
                                                    </td>                                    
                                                </tr>
                                            </table>
                                            <br />                                
                                        </asp:View>
                                      <%--  <asp:View ID="VResultados" runat="server">
                                            <fieldset style="border-color:White; border-width:1px; width: 505px;" id="Contenformatos0" runat="server">
                                                <legend style="color:White; font-weight:bold ">Datos del Planning</legend> 
                                               <asp:GridView ID="gvformatos0" runat="server" BackColor="White" 
                                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                                    GridLines="Horizontal" Width="490px" Height="109px">
                                                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                                </asp:GridView>
                                                <br />
                                                <table align="center">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblNomPlan0" runat="server" CssClass="labels" Text="Planning"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Nomplan0" runat="server" Width="258px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblElabPlan" runat="server" CssClass="labels" Text="Fecha de elaboración"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtElabPlan" runat="server" Width="188px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblNomejecut" runat="server" CssClass="labels" Text="Ejecutivo de Cuenta"></asp:Label>                                                
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtNomeject" runat="server" Width="188px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="gvformatos2" runat="server" BackColor="White" 
                                                                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                                                GridLines="Horizontal" Width="490px" Height="109px">
                                                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>                                            
                                                </table>
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <table align="center">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnPrevPlanning" runat="server"  Text="Previsualizar Planning" CssClass="button"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </asp:View>--%>
                                    </asp:MultiView>                       
                                    <br />
                                </td> 
                            </tr>                
                        </table>
                    </asp:Panel>
                   
                    <br />
                    <br />
                </div>
                <asp:Panel ID="Alertas" runat="server" Width="375px" CssClass="MensajesSupervisor" 
                    Height="215px" DefaultButton="BtnAceptarAlert"  style="display:none;">
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
                <cc1:ModalPopupExtender ID="ModalPopupAlertas" runat="server" 
                    DropShadow="True" Enabled="True" 
                    OkControlID="BtnAceptarAlert" PopupControlID="Alertas" 
                    TargetControlID="Btndisparaalertas" 
                    BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                
                <asp:Panel ID="Confirmacion" runat="server" Width="375px" CssClass="MensajesSupConfirm" 
                    Height="215px" style="display:none;" >
                    <table align="center" style="height: 164px">
                        <tr>
                            <td class="style4">
                            
                            </td>
                            <td class="style3">                                
                                <br />
                                <asp:Label ID="Label2" runat="server" Text="El formato previsualizado cumple con las especificaciones para el levantamiento de información ?" CssClass="labels"></asp:Label>                                
                                <br />
                                <br />
                             </td>
                        </tr>                                            
                    </table>
                    <table align="center">
                        <tr>                                                    
                            <td>
                                <asp:Button ID="BtnCofirmarSI" runat="server" CssClass="button" 
                                Text="SI" onclick="BtnCofirmarSI_Click" />
                            </td>
                            <td>
                                <asp:Button ID="BtnCofirmarNO" runat="server" CssClass="button" 
                                Text="NO"/>                            
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupConfirm" runat="server" 
                    DropShadow="True" Enabled="True" 
                    CancelControlID="BtnCofirmarNO" PopupControlID="Confirmacion" 
                    TargetControlID="BtnEnviarForm" 
                    BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                
                <asp:Panel ID="ConfirmaSI" runat="server" Width="375px" CssClass="MensajesSupConfirm" 
                    Height="215px" style="display:none;" >
                    <table align="center" style="height: 164px">
                        <tr>
                            <td class="style4">
                            
                            </td>
                            <td class="style3" align="center">                                
                                <br />
                                <asp:Label ID="Label1" runat="server" Text="Solicitud enviada con exito" CssClass="labels"></asp:Label>                                
                                <br />
                                <br />
                             </td>
                        </tr>                                            
                    </table>
                    <table align="center">
                        <tr>                                                    
                            <td>
                                <asp:Button ID="BtnAceptaSI" runat="server" CssClass="button" 
                                Text="Aceptar" />
                            </td>                            
                        </tr>
                    </table>
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupConfirmSI" runat="server" 
                    DropShadow="True" Enabled="True" 
                    OkControlID="BtnAceptaSI" PopupControlID="ConfirmaSI" 
                    TargetControlID="BtndisparaConfirmSI" 
                    BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                
                    <asp:Panel ID="PSolicitudes" runat="server" Width="593px"  
            CssClass="busqueda" Height="397px"  >
            <br />
            <table align="center" style="width: 528px">
                <tr>
                    <td>
                        <asp:Image ID="ImgMail" runat="server" 
                        ImageUrl="~/Pages/images/mailreminder.png" Height="62px" Width="65px" />
                    </td>
                    <td align="center" class="style13">
                        <asp:Label ID="LblTitEnvioMail" runat="server" Text="Solicitudes SIGE" CssClass="labelsTit"></asp:Label></td></tr></table><br />
            <table align="center">
                <tr>
                    <td valign="top" class="style15">
                        <asp:Label ID="LblMailSolicitante" runat="server" Text="De" CssClass="labels"></asp:Label></td><td>
                        <asp:TextBox ID="TxtSolicitante" runat="server" Width="400px" 
                            CssClass="modalBackground" Enabled="False"></asp:TextBox></td></tr><tr>
                    <td valign="top" class="style15">
                        <asp:Label ID="LblMailPara" runat="server" Text="Para" CssClass="labels"></asp:Label></td><td>
                        <asp:TextBox ID="TxtEmail" runat="server" Width="400px" 
                            CssClass="modalBackground" Enabled="False"></asp:TextBox></td></tr><tr>
                    <td valign="top" class="style15">
                        <asp:Label ID="LblMotivo" runat="server" Text="Asunto" CssClass="labels"></asp:Label></td><td>
                        <asp:TextBox ID="TxtMotivo" runat="server" Width="400px"></asp:TextBox></td></tr><tr>
                    <td valign="top" class="style15">
                        <asp:Label ID="LblMensaje" runat="server" Text="Mensaje" CssClass="labels"></asp:Label><br />
                        <br />
                        &nbsp;&nbsp;&nbsp;
                        </td>
                    <td>
                        <asp:TextBox ID="TxtMensaje" runat="server" TextMode="MultiLine" Width="400px" 
                            Height="113px"></asp:TextBox></td></tr></table><br />
            <table align="center">
                <tr>
                    <td>
                        <asp:Button ID="btnEnviarPSolicitud" runat="server" CssClass="button" Text="Enviar" 
                            Width="80px"/>
                        <asp:Button ID="btnCancelarPSolicitudes" runat="server" CssClass="button" Text="Cancelar" Width="80px" />
                    </td>
                </tr>
            </table>
          </asp:Panel>
                 <cc1:ModalPopupExtender ID="ModalPSolicitud" runat="server" 
                    DropShadow="True" Enabled="True" 
                    PopupControlID="PSolicitudes" 
                    TargetControlID="BtnCofirmarNO" 
                    BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                
                
                        
                        
              <%--<asp:Panel ID="BuscarPlanning" runat="server" Width="459px" 
                    CssClass="buscarplanning" Height="236px" style="display:none;"> 
                    
                <br />
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="LblbuscarPlanning" runat="server" CssClass="labelsTit" Text="Buscar Planning" />
                        </td>
                    </tr>
                </table>
                <br />
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="LblBCodPlanning" runat="server" CssClass="labels" Text="Código:" />                            
                        </td>
                        <td>
                            <asp:TextBox ID="TxtBCodPlanning" runat="server" Width="80px"></asp:TextBox>
                        </td>
                    </tr>                
                    <tr>
                        <td >
                            <asp:Label ID="LblBNomPlanning" runat="server" CssClass="labels" Text="Planning" />                        
                        </td>
                        <td>
                            <asp:TextBox ID="TxtBNomPlanning" runat="server" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                    <table align="center">                                        
                    <tr>
                        <td >                        
                            <asp:Label ID="LblFechaIni" runat="server" CssClass="labels" Text="Inicio Actividad"></asp:Label>
                        </td>
                        <td>
                        <asp:TextBox ID="TxtFecIni" runat="server" Width="80" ReadOnly="true"></asp:TextBox>
                        <img id="ImgFecIni" src="../../images/calendario.JPG" alt="" align="middle" />
                        <cc1:CalendarExtender ID="CEFecIni" runat="server" PopupButtonID="ImgFecIni"
                        TargetControlID="TxtFecIni">
                        </cc1:CalendarExtender>
                        </td>                   
                        <td>                        
                            <asp:Label ID="LblFechafin" runat="server" CssClass="labels" Text="Fin Actividad"></asp:Label>
                        </td>
                        <td>                        
                            <asp:TextBox ID="TxtFecFin" runat="server" Width="80" ReadOnly="true"></asp:TextBox>
                            <img id="ImgFecFin" src="../../images/calendario.JPG" alt="" align="middle" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImgFecFin"
                            TargetControlID="TxtFecFin">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                </table>                 
                <br />
                <table align="center"> 
                    <tr>
                        <td>
                            <asp:Button ID="BtnBPlanning" runat="server" CssClass="button" Text="Buscar" 
                                Width="80px" />
                            <asp:Button ID="BtnCancelBPlanning" runat="server" CssClass="button" Text="Cancelar" Width="80px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>       --%>   
                        
               <%-- <asp:Panel ID="BuscarPlanning0" runat="server" Width="462px" CssClass="busqueda" Height="236px" style="display:none;">
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
            <asp:Label ID="LblbuscarPlanning0" runat="server" CssClass="labelsTit" 
            Text="Buscar Planning" />
            <br />
            <br />
            <table style="width: 453px;">
                <tr>
                    <td>
                        <asp:Label ID="LblBCodPlanning0" runat="server" CssClass="labels" 
                        Text="Código:" />
                        &nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="TxtBNomRol1" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 452px;">
                <tr>
                    <td>
                        <asp:Label ID="LblBNomPlanning0" runat="server" CssClass="labels" 
                        Text="Planning" />
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="TxtBNomRol2" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<br />
                        <asp:Label ID="LblFechaIni0" runat="server" CssClass="labels" 
                            Text="Inicio Actividad"></asp:Label>--%>
                        <%--<cc1:CalendarPopup ID="CalendarPopup3" runat="server" BorderStyle="None" 
                            ControlDisplay="TextBoxImage" ImageUrl="~/Pages/images/calendario.JPG" 
                            Text="" Width="80px">
                            <buttonstyle font-bold="True" font-names="Arial" font-size="10pt" 
                                width="22px" />
                        </cc1:CalendarPopup>--%>
                    <%--</td>
                    <td class="style2">
                        <br />
                        <asp:Label ID="LblFechafin0" runat="server" CssClass="labels" 
                            Text="Fin de Actividad"></asp:Label>--%>
                        <%--<cc1:CalendarPopup ID="CalendarPopup4" runat="server" BorderStyle="None" 
                            ControlDisplay="TextBoxImage" ImageUrl="~/Pages/images/calendario.JPG" 
                            PadSingleDigits="True" Text="" Width="80px">
                            <buttonstyle font-bold="True" font-names="Arial" font-size="10pt" 
                                width="22px" />
                        </cc1:CalendarPopup>--%>
                    <%--</td>
                </tr>
            </table>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;        
            <asp:Button ID="BtnBRol0" 
            runat="server" CssClass="button" Text="Buscar" 
            Width="80px" />
            <asp:Button ID="btnCancelarRol0" runat="server" 
            CssClass="button" Text="Cancelar" Width="80px" />
        </asp:Panel>--%>
            </ContentTemplate>
        </asp:UpdatePanel> 
       
    </form>
</body>
</html>
