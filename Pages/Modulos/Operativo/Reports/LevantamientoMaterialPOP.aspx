<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LevantamientoMaterialPOP.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.LevantamientoMaterialPOP" %>
    <%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Levantamiento de Material POP</title>
 <link href="../../../css/StiloAdministrativo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                            <img alt="Procesando" src="../../../images/loading5.gif" style="vertical-align: middle" />
                        </div>
                    </div>
                </ProgressTemplate>
          </cc2:ModalUpdateProgress> 
          
                 <table align="center">
                           <caption>
                               <br />
                               <br />
                               <tr>
                                   <td>
                                       <asp:Label ID="Label1" runat="server" Text="Levantamiento de Material POP"></asp:Label>
                                   </td>
                               </tr>
                           </caption>
                            </table>
                  
                            <table align="center">
                                <tr>
                                    <td>                                  
                                           
                                            <br />
                                            <div>
                                                <table align="center">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="LblCanal" runat="server" Text="Canal* " Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="cmbcanal" runat="server" Enabled="False" Height="20px" Visible="false"
                                                                Width="300px" AutoPostBack="True" 
>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                         <tr>
                                                        <td>
                                                            <asp:Label ID="Labfecha" runat="server" Text="fecha* "></asp:Label>
                                                        </td>
                                                        <td>
                                                      <asp:TextBox ID="txt_fechaActual" runat="server"  Enabled="false" Width="100px"
                                                                       ></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="txt_fechaActual_MaskedEditExtender" runat="server"
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fechaActual"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </cc1:MaskedEditExtender>
                                                                   <%-- <cc1:CalendarExtender ID="txt_fechaActual_CalendarExtender" runat="server" Enabled="True"
                                                                        FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal3" PopupPosition="TopLeft"
                                                                        TargetControlID="txt_fechaActual">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:ImageButton ID="ImageButtonCal3" runat="server" Enabled="true" Height="16px"
                                                                        ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />   --%>
                                                          </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblplanning" runat="server" Text="Campaña* "></asp:Label>
                                                        </td>
                                                        <td>
                                                           <asp:DropDownList ID="cmbplanning" runat="server"  Enabled="False"
                                                                Height="20px"  Width="300px"   AutoPostBack="True" OnSelectedIndexChanged="cmbplanning_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>                                                
                                                     <%--<tr>
                                                        <td>
                                                            <asp:Label ID="LblAño" runat="server" Text="Año* " Visible ="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                           <asp:DropDownList ID="cmbAño" runat="server" AutoPostBack="True" Enabled="False" Visible="false"
                                                                Height="20px"  Width="300px" >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                        <td>
                                                            <asp:Label ID="lblMes" runat="server" Text="Mes* " Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                           <asp:DropDownList ID="cmbmes" runat="server" AutoPostBack="True" Enabled="False" Visible="false"
                                                                Height="20px"  Width="300px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>--%>
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="LblRecoger" runat="server" Text="Recoger Por* " Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                           <asp:DropDownList ID="cmbRecoger" runat="server" AutoPostBack="True" Enabled="False" Visible="false"
                                                                Height="20px"  Width="300px" OnSelectedIndexChanged="cmbRecoger_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <br />
                                 <div align="center">
                                <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnCrearUsuInfo" runat="server" CssClass="buttonPlan" Text="Crear"
                                            Width="95px" OnClick="BtnCrearUsuInfo_Click"  />
                                        <asp:Button ID="BtnGuardarUsuInfo" runat="server" CssClass="buttonPlan" Text="Guardar"
                                            Visible="False" Width="95px" OnClick="BtnGuardarUsuInfo_Click"  />
                                        <asp:Button ID="BtnConsultarUsuInfo" runat="server" CssClass="buttonPlan" Text="Consultar"
                                            Width="95px" />
                                        <asp:Button ID="BtnEditarUsuInfo" runat="server" CssClass="buttonPlan" Text="Editar"
                                            Visible="False" Width="95px"  />
                                        <asp:Button ID="BtnActuUsuInfo" runat="server" CssClass="buttonPlan" Text="Actualizar"
                                            Visible="False" Width="95px"  OnClick="BtnActuUsuInfo_Click"  />
                                        <asp:Button ID="BtnCancelUsuInfo" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                            Width="95px" OnClick="BtnCancelUsuInfo_Click"  />
                                        <asp:Button ID="BtnCargaMasivaLP" runat="server" 
                                           CssClass="buttonPlan"    Text="Carga Masiva" Width="95px" onclick="BtnCargaMasivaLP_Click" />
                                       <asp:Button ID="PregUsuInfo" runat="server" CssClass="buttonPlan" Text="|&lt;&lt;"
                                            Visible="False" ></asp:Button>
                                        <asp:Button ID="AregUsuInfo" runat="server" CssClass="buttonPlan" Text="&lt;&lt;"
                                            Visible="False" Width="24px"></asp:Button>
                                        <asp:Button ID="SregUsuInfo" runat="server" CssClass="buttonPlan" Text="&gt;&gt;"
                                            Visible="False" ></asp:Button>
                                        <asp:Button ID="UregUsuInfo" runat="server" CssClass="buttonPlan" Text="&gt;&gt;|"
                                            Visible="False"></asp:Button>  
                                       
                                    </td>
                                </tr>
                            </table>
                            </div>

                                           <%-- <table style="border: 1; border-style: solid;">--%>
                                                <tr>
                                                    <td>
                                             <%-- <div class="p" style="width: 100%; height: 193px;">--%>
                                               <asp:GridView ID="GvLPublicaciones" runat="server" AutoGenerateColumns="False" 
                                                Font-Names="Verdana" Font-Size="8pt" EnableModelValidation="True" 
                                                Width="100%" onselectedindexchanged="GvLPublicaciones_SelectedIndexChanged">                                           
                                                 
                                                <Columns>
                                                 <asp:TemplateField HeaderText="codregistro"  Visible="false">
                                                        <EditItemTemplate>
                                                             <asp:label ID="lblcodregistro" runat="server" Width="250px"   Text='<%# Bind("id_rpteMatPOP") %>'>       
                                                            </asp:label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                           <asp:label ID="lblcodregistro" runat="server" Width="250px"  >       
                                                            </asp:label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Marca" >
                                                        <EditItemTemplate>
                                                             <asp:DropDownList ID="cmbMarca" runat="server" Width="280px"   Text='<%# Bind("Id_brand") %>'>       
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                           <asp:DropDownList ID="cmbMarca" runat="server" Width="280px"  >       
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Promoción">
                                                          <EditItemTemplate>                                                   
                                                           <asp:DropDownList ID="cmbPromocion" runat="server" Width="150px" Text='<%# Bind("id_Tipo_Prom") %>' >       
                                                            </asp:DropDownList>
                                                              </EditItemTemplate>
                                                       <ItemTemplate>
                                                      <asp:DropDownList ID="cmbPromocion" runat="server" Width="120px"  >       
                                                            </asp:DropDownList>                                                                               
                                                       </ItemTemplate>
                                                    </asp:TemplateField>  
                                                     <asp:TemplateField HeaderText="Tipo POP">
                                                          <EditItemTemplate>                                                   
                                                           <asp:DropDownList ID="cmbtipoPOP" runat="server" Width="150px" Text='<%# Bind("id_MPointOfPurchase") %>' >       
                                                            </asp:DropDownList>
                                                              </EditItemTemplate>
                                                       <ItemTemplate>
                                                      <asp:DropDownList ID="cmbtipoPOP" runat="server" Width="120px"  >       
                                                            </asp:DropDownList>                                                                               
                                                       </ItemTemplate>
                                                    </asp:TemplateField>  
                                                                                               
                                                   <asp:TemplateField HeaderText="Inicio Actividad">    
                                                       <EditItemTemplate>                                                   
                                                           <asp:TextBox ID="txt_Inicio_Actividad" runat="server"  Enabled="true" Width="100px" Text='<%# Bind("fec_ini_act") %>'
                                                                       ></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="txt_Inicio_Actividad_MaskedEditExtender" runat="server"
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Inicio_Actividad"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </cc1:MaskedEditExtender>
                                                              </EditItemTemplate>                                                      
                                                       <ItemTemplate>
                                                       <asp:TextBox ID="txt_Inicio_Actividad" runat="server"  Enabled="true" Width="100px" 
                                                                       ></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="txt_Inicio_Actividad_MaskedEditExtender" runat="server"
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Inicio_Actividad"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </cc1:MaskedEditExtender>
                                                                   <%-- <cc1:CalendarExtender ID="txt_Inicio_Actividad_CalendarExtender" runat="server" Enabled="True"
                                                                        FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal" PopupPosition="TopLeft"
                                                                        TargetControlID="txt_Inicio_Actividad">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:ImageButton ID="ImageButtonCal" runat="server" Enabled="true" Height="16px"
                                                                        ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />                 --%>                                                              
                                                       </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Fin Actividad">     
                                                    <EditItemTemplate>
                                                     <asp:TextBox ID="txt_Fin_Actividad" runat="server"  Enabled="true" Width="100px" Text='<%# Bind("fec_fin_act") %>'
                                                                        ></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="txt_Fin_Actividad_MaskedEditExtender" runat="server"
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Fin_Actividad"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </cc1:MaskedEditExtender>
                                                    </EditItemTemplate>                                                     
                                                       <ItemTemplate>
                                                       <asp:TextBox ID="txt_Fin_Actividad" runat="server"  Enabled="true" Width="100px" 
                                                                        ></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="txt_Fin_Actividad_MaskedEditExtender" runat="server"
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Fin_Actividad"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </cc1:MaskedEditExtender>
                                                                   <%-- <cc1:CalendarExtender ID="txt_Fin_Actividad_CalendarExtender" runat="server" Enabled="True"
                                                                        FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal2" PopupPosition="TopLeft"
                                                                        TargetControlID="txt_Fin_Actividad">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:ImageButton ID="ImageButtonCal2" runat="server" Enabled="true" Height="16px"
                                                                        ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />           --%>                                                            
                                                       </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="PDV">
                                                          <EditItemTemplate>                                                   
                                                             <asp:DropDownList ID="cmbCadena" runat="server" Width="250px" Text='<%# Bind("ClientPDV_code") %>' >       
                                                             </asp:DropDownList>
                                                          </EditItemTemplate>
                                                       <ItemTemplate>
                                                       <asp:DropDownList ID="cmbCadena" runat="server" Width="250px"  >       
                                                            </asp:DropDownList>                                                                               
                                                       </ItemTemplate>
                                                    </asp:TemplateField>                                                                
                                                                                   
                                             
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/Pages/images/delete.png" 
                                                        ShowSelectButton="True" visible="false" />
                                                                                   
                                             
                                                </Columns>                                                
                                            </asp:GridView>
                                            <%--    </div>--%>
                                                    </td>
                                                
                                                </tr>
                                         <%--   </table>--%>                                           
                                            <caption>
                                                <br />
                                            </caption>
                                   
                                    </td>
                                </tr>
                            </table>
                           


                              <asp:Panel ID="BuscarLevaP" runat="server" CssClass="busqueda"  Style="display: none"
                                DefaultButton="BtnBuscarLevaP" Height="202px" Width="363px">
                                <table align="center">
                                <br />
                                <br />
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblTituloBuscar" runat="server" CssClass="labelsTit2" Text="Buscar Levantamiento Material POP" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table align="center">

                                 <tr>
                                                        <td>
                                                            <asp:Label ID="LblBfecha" runat="server" Text="fecha "></asp:Label>
                                                        </td>
                                                        <td>
                                                      <asp:TextBox ID="TexBFecha" runat="server"  Enabled="true" Width="100px"
                                                                       ></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                        CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="TexBFecha"
                                                                        UserDateFormat="DayMonthYear">
                                                                    </cc1:MaskedEditExtender>
                                                                   <%-- <cc1:CalendarExtender ID="txt_fechaActual_CalendarExtender" runat="server" Enabled="True"
                                                                        FirstDayOfWeek="Monday" PopupButtonID="ImageButtonCal3" PopupPosition="TopLeft"
                                                                        TargetControlID="txt_fechaActual">
                                                                    </cc1:CalendarExtender>
                                                                    <asp:ImageButton ID="ImageButtonCal3" runat="server" Enabled="true" Height="16px"
                                                                        ImageUrl="~/Pages/images/calendario.JPG" Width="16px" />   --%>
                                                          </td>
                                                    </tr>

                                          <tr>
                                        <td>
                                            <asp:Label ID="lblBplanning" runat="server" CssClass="labels" Text="Campaña:" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbBCampaña" runat="server" MaxLength="50" Width="193px" 
                                                AutoPostBack="True" OnSelectedIndexChanged="cmbBCampaña_SelectedIndexChanged"
                                               >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblBrecogido" runat="server" CssClass="labels" Text="Recogido Por:" visible="false" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cmbBRecogidoPof" runat="server" MaxLength="50"  visible="false"
                                                Width="193px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>                      
                                 
                                     
                                </table>
                                <br />
                                    <table align="center">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnBuscarLevaP" runat="server" CssClass="buttonPlan" Text="Buscar"
                                                Width="80px" OnClick="BtnBuscarLevaP_Click" />
                                            <asp:Button ID="BtnBCancelarPDVC" runat="server" CssClass="buttonPlan" Text="Cancelar"
                                                Width="80px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalPopupLevantaPublicacion" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True" OkControlID="BtnBCancelarPDVC" PopupControlID="BuscarLevaP"
                                TargetControlID="BtnConsultarUsuInfo" DynamicServicePath="">
                            </cc1:ModalPopupExtender>


                             <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="BtnConfirmacionProd" runat="server" Text="" Height="0px" CssClass="alertas"
                            Width="0" Enabled="false" />
                        <asp:Panel ID="PanelConfirmaProd" runat="server" Width="332px" CssClass="altoverow"
                            Style="display: none;">
                            <table align="center" style="width: 95%;">
                                <tr>
                                    <td align="center" valign="top">
                                        <br />
                                        <asp:Label ID="LblSrUsuarioProd" runat="server" Text="Sr. Usuario"></asp:Label>
                                        <br />
                                        <asp:Label ID="LblMensajeConfirProd" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <table align="center">
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="BtnSiConfirmaProd" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                                        Text="SI" OnClick="BtnSiConfirmaProd_Click" />
                                                </td>
                                                <td align="center">
                                                    <asp:Button ID="BtnNoConfirmaProd" BorderStyle="Solid" runat="server" CssClass="buttonPlan"
                                                        Text="NO" OnClick="BtnNoConfirmaProd_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <cc1:ModalPopupExtender ID="ModalConfirmaProd" runat="server" Enabled="True" TargetControlID="BtnConfirmacionProd"
                            PopupControlID="PanelConfirmaProd" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>


                        
                          <asp:Panel ID="CargaMasiva" runat="server" Style="vertical-align: middle; " BackColor="#7F99CC" BorderColor="#7F99CC"
                            BorderStyle="Solid" BorderWidth="6px" Font-Names="Verdana" Font-Size="10pt">
                           
                           <div style="position:absolute; z-index:1;  width:100%;" align="right" > 
                           <asp:ImageButton ID="BtnCargaMAsivaMarca" runat="server" AlternateText="Cerrar Ventana"
                                                ToolTip="Cerrar Ventana" BackColor="Transparent" Height="22px" ImageAlign="Right"
                                                ImageUrl="~/Pages/ImgBooom/salir.png" Width="23px"  
                                      />
                           </div>
                                 <div  align="center">                                   
                                       <iframe id="IfCargaMasivaGProductos" runat="server" height="230px" src="" width="500px">
                                       </iframe>                                       
                                 </div>                                                           
                             </asp:Panel>
                               <cc1:ModalPopupExtender ID="ModalCMasiva" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="True" Enabled="True"  PopupControlID="CargaMasiva"
                                TargetControlID="btnPopupMarcas" DynamicServicePath="">
                            </cc1:ModalPopupExtender>
                              <asp:Button ID="btnPopupMarcas" runat="server" CssClass="alertas"
                               Width="0px" />

                    </ContentTemplate>
                </asp:UpdatePanel>




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
    </div>
    </form>
</body>
</html>
