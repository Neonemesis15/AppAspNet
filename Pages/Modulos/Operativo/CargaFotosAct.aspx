<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargaFotosAct.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.CargaFotosAct" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Fotografías de la actividad</title>
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
     <style type="text/css"> 
        .style49
            {   
                width: 213px;
                height: 74px;
            }      
        .style50
            {
                height: 74px;
                width: 120px;
            }          
        </style>
</head>
<body class="backcolor1">
    <form id="form1" runat="server">    
     <div style="border-color:Maroon; border-width:2px;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <asp:Button ID="Btndisparaalertas" runat="server" CssClass="alertas" Text=""                                 
                                Visible="true" Width="0px" />                
                <table bgcolor="#7F99CC" >                    
                </table>
                <table>
                    <tr valign="top">
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblPlanning" runat="server" Text="Planning" 
                                            CssClass="labelsTitN"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblNamePlanning" runat="server" Text="" CssClass="boton"></asp:Label>
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
                            <div class="ScrollFotos"> 
                                <asp:GridView ID="GVFotografiasAct" runat="server" AutoGenerateColumns="False" 
                                    PageSize="1" Width="272px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Fotografías">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                                                CausesValidation="True" oncheckedchanged="CheckBox1_CheckedChanged" />
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="Image1" runat="server" CssClass="imagenstrech" Height="152" 
                                                                ImageAlign="Middle" Width="226px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                        <td valign="top"> 
                            <table>                                
                                 <tr>
                                    <td>
                                        <asp:Label ID="LblFechaTomaFoto" runat="server" CssClass="labelsN" 
                                            Text="Fecha de Fotografía:" 
                                            ToolTip="Ingrese la fecha en la cual se tomó la fotografía"></asp:Label>
                                    </td>                                     
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TxtFechaFoto" runat="server" Width="70px" AutoPostBack="True" 
                                                        CausesValidation="True" ontextchanged="TxtFechaFoto_TextChanged" 
                                                        CssClass="StiloCombo"></asp:TextBox>                                                    
                                                    <cc1:MaskedEditExtender ID="TxtFechaFoto_MaskedEditExtender" runat="server" 
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                        Mask="99/99/9999" MaskType="Date" TargetControlID="TxtFechaFoto" 
                                                        UserDateFormat="DayMonthYear">
                                                    </cc1:MaskedEditExtender>
                                                </td>
                                                <td align="center">
                                                    <asp:ImageButton ID="ImgFotoCalendar" runat="server"
                                                        ImageUrl="~/Pages/images/calendario.JPG" />
                                                    <asp:Label ID="LblFechaObl" runat="server" Text="*" Font-Bold="True" 
                                                        Font-Size="10px" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <cc1:CalendarExtender ID="TxtFechaFoto_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd/MM/yyyy" PopupButtonID="ImgFotoCalendar" 
                                            TargetControlID="TxtFechaFoto">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblPDVPlanning" runat="server" Text="Punto de venta" 
                                            CssClass="labelsN"></asp:Label>
                                    </td>                                    
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="CmbPDVPlanning" runat="server" Width="244px" 
                                                        CssClass="StiloCombo">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="LblPDVObl" runat="server" Text="*" Font-Bold="True" 
                                                        Font-Size="10px" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblCatgProd" runat="server" Text="Categoría de producto" 
                                            CssClass="labelsN"></asp:Label>
                                    </td>                                    
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="CmbCatgProd" runat="server" Width="244px" 
                                                        CssClass="StiloCombo">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="LblCatgObl" runat="server" Text="*" Font-Bold="True" 
                                                        Font-Size="10px" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>                                       
                                    </td>
                                </tr>  
                                 <tr>
                                    <td align="left" colspan="2"> 
                                    <br />
                                      <br />                                                     
                                </td>            
                            </tr>       
                            <tr>
                                <td  align="center" colspan="2">
                                    <asp:Label ID="LblTitCargarArchivo" runat="server" CssClass="labelsN"  
                                    Text="Cargar Fotografías" ></asp:Label>                                                                         
                                </td>
                            </tr>
                            <tr>                        
                                <td align="center" colspan="2">
                                    <asp:FileUpload ID="FileUpFotosAct" runat="server" Width="237px" />                                                                                
                                </td>
                            </tr> 
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="BtnCargarFotosAct" runat="server" CssClass="alertas" Text=""                                 
                                    Visible="true" Width="0px" />                                  
                                </td>
                            </tr>                                  
                            <tr>
                                <td align="center" colspan="2">
                                    <table>
                                        <tr>
                                            <td> 
                                                <asp:Label ID="StatusActPropia" runat="server" Text="Estado" Font-Bold="True" 
                                                Font-Names="Arial" Font-Size="10pt" ForeColor="Black"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="RbtnStatusActPropia" runat="server" CssClass="StiloCombo"
                                                RepeatDirection="Horizontal" Enabled="False">
                                                <asp:ListItem Selected="True">Habilitado</asp:ListItem>
                                                <asp:ListItem>Deshabilitado</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>                                                                                            
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <asp:Label ID="LblComentarFotos" runat="server" Text="Comentarios" CssClass="labelsN"></asp:Label>                            
                        <br />
                        <asp:TextBox ID="TxtObsActPropia" runat="server" Height="199px" TextMode="MultiLine" 
                        Width="259px" MaxLength="255"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="ReqObsActPropia" runat="server"
                            ControlToValidate="TxtObsActPropia" Display="None"
                            ErrorMessage="El comentario no debe empezar por espacio en blanco ni valor numérico, Ingrese máximo 255 caracteres"
                            ValidationExpression="([a-zA-Z][\W\w\sñÑáéíóúÁÉÍÓÚ0-9]{0,254})">
                        </asp:RegularExpressionValidator>
                        <cc1:ValidatorCalloutExtender ID="VCEReqObsActPropia" runat="server" 
                            Enabled="True" TargetControlID="ReqObsActPropia">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />                
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="IbtnCrearInfFoto" runat="server"
                            AlternateText="Nuevo registro" BorderStyle="None"
                            ImageUrl="~/Pages/images/BtnNewReg.png"  
                            onmouseout="this.src = '../../images/BtnNewReg.png'" 
                            onmouseover="this.src = '../../images/BtnNewRegDown.png'" 
                            style="margin-left: 0px" onclick="IbtnCrearInfFoto_Click" />
                        <asp:ImageButton ID="IbtnSaveInfFoto" runat="server" 
                            AlternateText="Guardar registro" BorderStyle="None" 
                            ImageUrl="../../images/BtnSaveReg.png"
                            onmouseout="this.src = '../../images/BtnSaveReg.png'" 
                            onmouseover="this.src = '../../images/BtnSaveRegDown.png'" 
                            style="margin-left: 0px" Visible="False" onclick="IbtnSaveInfFoto_Click" />
                        <asp:ImageButton ID="IbtnSearchInfFoto" runat="server"
                            AlternateText="Consultar registro" BorderStyle="None"
                            ImageUrl="~/Pages/images/BtnSearchReg.png" 
                            onmouseout="this.src = '../../images/BtnSearchReg.png'" 
                            onmouseover="this.src = '../../images/BtnSearchDownReg.png'" 
                            style="margin-left: 0px"   />
                        <asp:ImageButton ID="IbtnEditInfFoto" runat="server"
                            AlternateText="Editar registro" BorderStyle="None"
                            ImageUrl="~/Pages/images/BtnEditReg.png"  
                            onmouseout="this.src = '../../images/BtnEditReg.png'" 
                            onmouseover="this.src = '../../images/BtnEditRegDown.png'" 
                            style="margin-left: 0px" Visible="False" onclick="IbtnEditInfFoto_Click"/>
                        <asp:ImageButton ID="IbtnActualizaInfFoto" runat="server"
                            AlternateText="Actualizar registro" BorderStyle="None"
                            ImageUrl="~/Pages/images/BtnUpdateReg.png"  
                            onmouseout="this.src = '../../images/BtnUpdateReg.png'" 
                            onmouseover="this.src = '../../images/BtnUpdateRegDown.png'" 
                            style="margin-left: 0px" Visible="False" onclick="IbtnActualizaInfFoto_Click"/>                                                        
                        <asp:ImageButton ID="IbtnCancelInfFoto" runat="server" AlternateText="Cancelar" 
                            BorderStyle="None" ImageUrl="../../images/BtnCancelReg.png"                             
                            onmouseout="this.src = '../../images/BtnCancelReg.png'" 
                            onmouseover="this.src = '../../images/BtncancelRegDown.png'" 
                            style="margin-left: 0px" onclick="IbtnCancelInfFoto_Click" />
                    </td>
                </tr>
            </table>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <asp:Panel ID="Alertas" runat="server" 
                DefaultButton="BtnAceptarAlert" style="display:none;"  Height="136px" Width="250px" >
                <br />
                <table align="center">
                    <tr>
                        <td align="center" class="style50" valign="top">
                            <br />
                        </td>
                        <td class="style49" valign="top">                                
                                <asp:Label ID="LblAlert" runat="server" Font-Bold="True" Font-Names="Arial" 
                                    Font-Size="8pt" ForeColor="White"></asp:Label>  
                                <br />                                    
                                <asp:Label ID="LblFaltantes" runat="server" Font-Bold="True" Font-Names="Arial" 
                                    Font-Size="8pt" ForeColor="White"></asp:Label>                               
                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td align="center">
                                <asp:Button ID="BtnAceptarAlert" runat="server" CssClass="button" 
                                    Text="Aceptar" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                
                <cc1:ModalPopupExtender ID="ModalPopupAlertas" runat="server" 
                    BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" 
                    OkControlID="BtnAceptarAlert" PopupControlID="Alertas" 
                    TargetControlID="Btndisparaalertas">
                </cc1:ModalPopupExtender>
                
                    <asp:Panel ID="BuscarActPropia" runat="server" CssClass="CargaArchivos" 
                        Width="750px" Height="330px" style="display:none;" >
                        <div dir="rtl">
                            <asp:ImageButton ID="ImgCloseSearchActPropia" runat="server" ImageUrl="../../images/Exitiframe.gif" Height="19px" Width="19px" ToolTip="Cerrar Ventana" />
                        </div>                       
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Label ID="LblTitBActPropia" runat="server" CssClass="labelsTitN" 
                                        Text="Buscar registro fotográfico" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:GridView ID="GVConsultaActPropia" runat="server" AllowPaging="True" PageSize="8" 
                                        AutoGenerateSelectButton="True" BackColor="White" BorderColor="#CCCCCC" 
                                        BorderStyle="None" BorderWidth="1px"
                                        CaptionAlign="Left" CellPadding="3" 
                                        EmptyDataText="Sr. Usuario , actualmente este planning no tiene registros fotográficos"                                                                 
                                        Width="717px" onpageindexchanging="GVConsultaActPropia_PageIndexChanging" 
                                        onselectedindexchanged="GVConsultaActPropia_SelectedIndexChanged" >
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle BackColor="White" Font-Names="Arial" Font-Size="9px" 
                                            ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="9px" ForeColor="White" />
                                        <PagerSettings Mode="NextPreviousFirstLast" NextPageText="Siguiente" 
                                            PreviousPageText="Anterior" />
                                        <PagerStyle BackColor="White" Font-Names="Arial" Font-Size="9px" 
                                            ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle Font-Names="Arial" Font-Size="9px" ForeColor="#000066" />
                                        <SelectedRowStyle Font-Names="Arial" 
                                            Font-Size="9px" ForeColor="#000066" />
                                    </asp:GridView>                                                            
                                </td>                                                       
                            </tr>                        
                        </table>                        
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalPopupBuscarActPropia" runat="server" 
                                                BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" 
                                                OkControlID="ImgCloseSearchActPropia" PopupControlID="BuscarActPropia" 
                                                TargetControlID="IbtnSearchInfFoto" DynamicServicePath="">
                                            </cc1:ModalPopupExtender>                                            
                
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="IbtnSaveInfFoto" />
<asp:PostBackTrigger ControlID="IbtnActualizaInfFoto"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="IbtnActualizaInfFoto"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="IbtnActualizaInfFoto"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="IbtnActualizaInfFoto"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="IbtnActualizaInfFoto"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="IbtnActualizaInfFoto"></asp:PostBackTrigger>
            </Triggers>
            <Triggers>
                <asp:PostBackTrigger ControlID="IbtnActualizaInfFoto" />
            </Triggers>
            
        </asp:UpdatePanel>            
    
    
    </div>
    </form>
</body>
</html>
