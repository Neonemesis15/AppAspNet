<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargaFotosCom.aspx.cs" Inherits="SIGE.Pages.Modulos.Operativo.CargaFotosCom" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css"> 
        .style49
            {   
                width: 238px;
                height: 67px;
            }      
        .style50
            {
                height: 67px;
                width: 79px;
            }          
        </style>
</head>
<body class="CargaArchivos">
    <form id="form1" runat="server">
    <div style="border-color:Maroon; border-width:2px; width: 237px; height:40px ">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="Btndisparaalertas" runat="server" CssClass="alertas" Text=""                                 
                                Visible="true" Width="0px" />
                <table align="center" bgcolor="#7F99CC" style="width: 237px; height:40px">
                    <tr>
                        <td align="left">                            
                            <asp:Label ID="LblTitCargarArchivo" runat="server" CssClass="labels"  
                            Text="Cargar Fotografías" ></asp:Label>
                        </td>            
                    </tr>        
                </table>
                <table align="center" style="width: 237px ;height:40px" >
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUpFotosCom" runat="server" Width="237px" />                                                                            
                        </td>
                    </tr>
                    <tr>                        
                        <td align="center">
                            <asp:ImageButton ID="BtnCargarFotos" runat="server" 
                             AlternateText="Cargar fotografía" BorderStyle="None" 
                             ImageUrl="../../images/BtnCargarFoto.png" 
                             onmouseout="this.src = '../../images/BtnCargarFoto.png'" 
                             onmouseover="this.src = '../../images/BtnCargarFotoDown.png'" 
                             style="margin-left: 0px" onclick="BtnCargarFotos_Click"   />                            
                             
                                                       
                        </td>
                    </tr>
                </table>
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                 <asp:Panel ID="Alertas" runat="server" 
                    DefaultButton="BtnAceptarAlert" style="display:none;" Height="122px" Width="238px" >
                    <table align="center">
                        <tr>
                            <td align="center" class="style50" valign="top">
                                <br />
                            </td>
                            <td class="style49" valign="top">                                
                                <asp:Label ID="LblFaltantes" runat="server" Font-Bold="True" Font-Names="Arial" 
                                    Font-Size="8pt" ForeColor="White"></asp:Label>                           
                                <br />
                                <asp:Label ID="LblAlert" runat="server" Font-Bold="True" Font-Names="Arial" 
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
                
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnCargarFotos" />
            </Triggers>
        </asp:UpdatePanel>            
    </div>                
                
    </form>
</body>
</html>
