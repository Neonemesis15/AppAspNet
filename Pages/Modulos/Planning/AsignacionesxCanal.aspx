<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsignacionesxCanal.aspx.cs" Inherits="SIGE.Pages.Modulos.Planning.AsignacionesxCanal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI" tagprefix="ew" %>
<%@ Register assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI.Compatibility" tagprefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function pageLoad() {
        }
    </script>

    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
    <link href="../../css/luckysige.css" rel="stylesheet" type="text/css" />
    <link href="../../css/Menu.css" rel="stylesheet" type="text/css" />
    <link href="../../css/stilo.css" rel="stylesheet" type="text/css" />
    <%--<link href="../../css/ie6.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
               
        #Lucky
        {
            height: 61px;
            width: 281px;
        }
        .style49
            {   
                width: 213px;
                height: 74px;
            }      
        .style50
            {
                height: 74px;
                width: 80px;
            }
       
        </style>
</head>
<body class="CargaArchivos">
    <form id="form1" runat="server">
    <asp:UpdatePanel ID="upgeneral" runat="server">
  <ContentTemplate>
    
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
    </div>
    <table>
        <tr>
            <td style="width:1024px">
                <asp:Label ID="lbltitlepla" runat="server" Font-Names="Bauhaus 93" 
                            Font-Size="18pt" Text="Asignaciones por Canal" Width="357px"></asp:Label>
                            <asp:Button ID="BtndisparaalertasCiudadPrincipal" runat="server" CssClass="alertas" 
                                    Text="" Visible="true" Width="0px" />
                            <asp:Button ID="Btndisparaalertacambiocateg" runat="server" CssClass="alertas" 
                                    Text="" Visible="true" Width="0px" />
                                    
            </td>
            <td align="right" style="width:1024px">                        
                <object id="Lucky" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" 
                    codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0">
                    <param name="allowScriptAccess" value="sameDomain" />
                    <param name="movie" value="../../images/SIGEV2.swf" />
                    <param name="loop" value="false" />
                    <param name="quality" value="high" />
                    <param name="wmode" value="transparent" />
                    <param name="bgcolor" value="#ffffff" />
                    <embed align="middle" allowscriptaccess="sameDomain" bgcolor="#ffffff" 
                    loop="false" name="../../images/SIGEV2.swf" 
                    pluginspage="http://www.macromedia.com/go/getflashplayer" quality="high" 
                    src="../../images/SIGEV2.swf" style="height: 88px; width: 283px" 
                    type="application/x-shockwave-flash" wmode="transparent" />
                </object>
            </td>
        </tr>
    </table>
    <table id="tcontenido" runat="server" align="center">            
        <tr>
            <td valign="top">
                <asp:Label ID="lblcliente" runat="server" Text="Cliente" CssClass="labelsN"></asp:Label>   
            </td>
            <td valign="top">
                <asp:DropDownList ID="cmbcliente" runat="server" Width="300px"
                    AutoPostBack="True" 
                    onselectedindexchanged="cmbcliente_SelectedIndexChanged" 
                    CausesValidation="True">
                </asp:DropDownList>      
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="lblcanal" runat="server" Text="Canal" CssClass="labelsN"></asp:Label>
            </td>
            <td valign="top">
                <asp:DropDownList ID="cmbcanal" runat="server" Width="300px" 
                    onselectedindexchanged="cmbcanal_SelectedIndexChanged" AutoPostBack="True" 
                    CausesValidation="True">
                </asp:DropDownList>                      
            </td>
        </tr>
    </table>
    <table align="center">
        <tr>
            <td>
                <asp:Button ID="btninimenu" runat="server"  Text="Ir al Menu" 
                    Width="113px" CssClass="button" 
                    PostBackUrl="~/Pages/Modulos/Planning/Menu_Planning.aspx" />
            </td>
            <td>                
                <asp:Button ID="btnsesion" runat="server" CausesValidation="False" 
                    CssClass="button" onclick="btnsesion_Click" Text="Cerrar Sesión" 
                    Width="113px" />
            </td>
        </tr>
    </table>
    <br />
    
    <cc1:TabContainer ID="TabContainer1" runat="server" Visible="false" 
          ActiveTabIndex="1" Width="1400px">
        <cc1:TabPanel runat="server" HeaderText="Ciudad Principal" ID="TabPanel0"> 
            <HeaderTemplate>Ciudad Principal</HeaderTemplate>
            <ContentTemplate>
                <table align="center">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="upcityprincipal" runat="server">
                                <ContentTemplate>
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblasigcityp" runat="server" CssClass="labelsN" Text="Asignación Ciudad Principal"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="RblcityPrincipal" runat="server" Height="62px" 
                                                    Width="255px" Enabled="False">
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btncitypri" runat="server" CssClass="boton2" Height="29px" 
                                                onclick="btncitypri_Click" Text="Asignar Ciudad" Width="181px" 
                                                Enabled="False" />
                                            </td>
                                        </tr>
                                    </table>                                
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>        
        </cc1:TabPanel>
       
        <cc1:TabPanel runat="server" HeaderText="Plan de Ventas" ID="TabPanel1">
            <HeaderTemplate>Plan de Ventas</HeaderTemplate>
            <ContentTemplate>
                <div>
                    <asp:UpdatePanel ID="upasignpla" runat="server">
                        <ContentTemplate>
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblasigna" runat="server" CssClass="labelsN" 
                                        Text="Asignación Plan de Ventas"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblcitypri" runat="server" CssClass="labelsN" 
                                        Text="Ciudad Principal : "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblcodcitypriasignada" runat="server" CssClass="labelsN" 
                                        Text=""></asp:Label>                                            
                                        -<asp:Label ID="lblcitypriasignada" runat="server" CssClass="labelsN" 
                                        Text=""></asp:Label>                                                                            
                                    </td>
                                </tr>
                            </table>
                            <br />                            
                            <br />
                            <table align="center">                                                                
                                <tr>                                    
                                    <td>
                                        <asp:Label ID="lblaño" runat="server" CssClass="labelsN" Text="Año"></asp:Label>
                                        <asp:DropDownList ID="cmbaño" runat="server" AutoPostBack="True" 
                                            Enabled="False" onselectedindexchanged="cmbaño_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblmes" runat="server" CssClass="labelsN" Text="Mes"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbmes" runat="server" AutoPostBack="True" 
                                            Enabled="False" onselectedindexchanged="cmbmes_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMedida" runat="server" CssClass="labelsN" 
                                            Text="Unida de Medida"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rblistipo" runat="server" Enabled="False" 
                                            onselectedindexchanged="rblistipo_SelectedIndexChanged" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text="TM" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="$" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>                            
                            <br />                            
                            <table align="center">
                                <tr> 
                                    <td>
                                        <asp:Label ID="lblPlventaCitypri" runat="server" CssClass="labelsN" 
                                            Text="Plan Ventas Ciudad Principal"></asp:Label>
                                    </td>                                    
                                    <td>
                                        <asp:TextBox ID="txtplan" runat="server" Enabled="False" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RegularExpressionValidator ID="ReqPlan" runat="server" 
                                            ControlToValidate="txtplan" 
                                            ErrorMessage="El valor debe ser numérico con máximo 2 decimales ej:(99,99)" 
                                            ValidationExpression="^[0-9]*(\,[0-9]+)?$">
                                        &nbsp;
                                        <cc1:ValidatorCalloutExtender ID="validaexpreplaprov" runat="server" 
                                            Enabled="True" TargetControlID="ReqPlanProv"></cc1:ValidatorCalloutExtender>
                                        </asp:RegularExpressionValidator>
                                    </td>                                    
                                    <td>
                                        <asp:Label ID="lblPlventaPais" runat="server" CssClass="labelsN" 
                                            Text="Plan Ventas Provincias"></asp:Label>
                                    </td>
                                    <td>                                    
                                        <asp:TextBox ID="txtplanNal" runat="server" Enabled="False" Width="200px"></asp:TextBox>
                                    </td>                                    
                                    <td>
                                        <asp:RegularExpressionValidator ID="ReqPlanProv" runat="server" 
                                            ControlToValidate="txtplanNal" 
                                            ErrorMessage="El valor debe ser numérico con máximo 2 decimales ej:(99,99)" 
                                            ValidationExpression="^[0-9]*(\,[0-9]+)?$">
                                        &nbsp;
                                        <cc1:ValidatorCalloutExtender ID="validaexprepla" runat="server" Enabled="True" 
                                            TargetControlID="ReqPlan"></cc1:ValidatorCalloutExtender>
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                                <tr>                                
                                    <td>
                                        <asp:Button ID="btnNuevo" runat="server" CssClass="boton" 
                                            onclick="btnNuevo_Click" Text="Nuevo Plan" />
                                        <asp:Button ID="btnagreplan" runat="server" CssClass="boton" 
                                            onclick="btnagreplan_Click" Text="Agregar" Visible="False" />
                                        <cc1:ConfirmButtonExtender ID="btnagreplan_ConfirmButtonExtender" 
                                            runat="server" ConfirmText="Desea Agregar El nuevo Plan de Ventas" 
                                            Enabled="True" TargetControlID="btnagreplan">
                                        </cc1:ConfirmButtonExtender>
                                    </td>                                    
                                </tr>
                            </table>
                            <br />
                            <table align="center">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvplan" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" CellPadding="3" 
                                        DataKeyNames="id_plan" 
                                        onpageindexchanging="gvplan_PageIndexChanging" 
                                        onrowcancelingedit="gvplan_RowCancelingEdit" onrowcommand="gvplan_RowCommand" 
                                        onrowcreated="gvplan_RowCreated" onrowdatabound="gvplan_RowDataBound" 
                                        onrowediting="gvplan_RowEditing" onrowupdating="gvplan_RowUpdating" 
                                        PageSize="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                                        BorderWidth="1px">
                                        <RowStyle CssClass="altrow" ForeColor="#000066" />
                                        <Columns>
                                            <asp:BoundField DataField="id_plan" HeaderStyle-ForeColor="Black" HeaderText="id_plan" 
                                                ReadOnly="True">
                                                <HeaderStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderStyle-ForeColor="White" 
                                                HeaderText="Plan_Ventas_Ciudad_Principal">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblplan" runat="server" Text='<%# Bind("Valor_Plan_city") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPlanNal" runat="server" AutoPostBack="True" MaxLength="50" 
                                                        Text='<%# Bind("Valor_Plan_city") %>' Width="290px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFV_txtPlanNal" runat="server" 
                                                        ControlToValidate="txtPlanNal" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <HeaderStyle ForeColor="White" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-ForeColor="White" 
                                                HeaderText="Plan_Ventas_Provincias">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("Value_plan_Nal") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPlan" runat="server" AutoPostBack="True" MaxLength="50" 
                                                        Text='<%# Bind("Value_plan_Nal") %>' Width="300px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFV_txtPlan" runat="server" 
                                                        ControlToValidate="txtPlan" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <HeaderStyle ForeColor="White" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-ForeColor="White" 
                                            HeaderText="Unidad">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblunidad" runat="server" Text='<%# Bind("Unidades") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>                                                
                                                <asp:TextBox ID="txtunidad" runat="server" AutoPostBack="True" MaxLength="50" 
                                                        Text='<%# Bind("Unidades") %>' Width="30px"></asp:TextBox>
                                                                                                        
                                                    <asp:RequiredFieldValidator ID="RFV_txtunidad" runat="server" 
                                                        ControlToValidate="txtunidad" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                                        
                                                </EditItemTemplate>                                                
                                                <HeaderStyle ForeColor="White" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Año"  HeaderText="Año" 
                                                ReadOnly="True">
                                                <HeaderStyle ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Mes" HeaderStyle-ForeColor="Black" 
                                                HeaderText="Mes" ReadOnly="True">
                                                <HeaderStyle ForeColor="White" Wrap="True" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="Button3" runat="server" CausesValidation="False" 
                                                        CommandName="Edit" CssClass="boton" Text="Editar"  />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="btnupdate0" runat="server" CausesValidation="True" 
                                                        CommandName="Update" CssClass="boton" Text="Actualizar"  />
                                                        &nbsp;<asp:Button ID="btncancel0" runat="server" CausesValidation="False" 
                                                        CommandName="Cancel" CssClass="boton" Text="Cancelar" />
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                </FooterTemplate>
                                                <HeaderStyle ForeColor="White" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            </table>                            
                            <br />
                            <asp:Button ID="btndipararalerta" CssClass="alertas" Enabled="false" runat="server" Height="0px" Text="" 
                                Width="0px"  />
                            <cc1:ModalPopupExtender ID="ModalPopupCanal" runat="server" 
                                BackgroundCssClass="modalBackground" Enabled="True" PopupControlID="PMensajes" 
                                TargetControlID="btndipararalerta">
                            </cc1:ModalPopupExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        
        <cc1:TabPanel runat="server" HeaderText="Asignación de Categorias/Presentaciones" ID="TabPanel6">
            <HeaderTemplate>Asignación de Categorias/Presentaciones</HeaderTemplate>
            <ContentTemplate>
                <div>
                    <asp:UpdatePanel ID="Upasigna" runat="server">
                        <ContentTemplate>
                            <table id ="tasignac" runat ="server" align="center" >
                                <tr >
                                    <td valign="top">                                    
                                        <asp:Label ID="lblCategoria" runat="server" CssClass="labelsN" Text="Categorias"></asp:Label>
                                        <br />
                                        <asp:RadioButtonList ID="RlistCategorias" runat="server" 
                                            CssClass="p" 
                                            RepeatLayout="Flow" Width="300px" AutoPostBack="True" 
                                            onselectedindexchanged="RlistCategorias_SelectedIndexChanged" 
                                            Height="200px">
                                        </asp:RadioButtonList>
                                    </td>
                                    <td width="305" valign="top">                                    
                                        <asp:Label ID="lblpresenta" runat="server" CssClass="labelsN" Text="Presentaciones"></asp:Label>
                                        <br />
                                        <asp:CheckBoxList ID="lisPresentaciones" runat="server" CssClass="p" 
                                            RepeatLayout="Flow" Width="300px" 
                                            onselectedindexchanged="lisPresentaciones_SelectedIndexChanged" 
                                            AutoPostBack="True" Height="200px">
                                        </asp:CheckBoxList>
                                    </td>
                                    <td width="305" valign="top">
                                        <asp:Label ID="lblpresenta1" runat="server" CssClass="labelsN" Text="Presentacion Principal"></asp:Label>
                                        <br />
                                        <asp:RadioButtonList ID="rblistpresntaprincipal" runat="server" 
                                            AutoPostBack="True" CssClass="p" Height="200px" RepeatLayout="Flow" 
                                            Width="300px" 
                                            onselectedindexchanged="rblistpresntaprincipal_SelectedIndexChanged">
                                        </asp:RadioButtonList>
                                    </td>
                                    <td width="305" valign="top">
                                        <asp:Label ID="Lblpresentcom" runat="server" CssClass="labelsN" Text="Presentacion Competidor"></asp:Label>
                                        <br />
                                        <asp:CheckBoxList ID="ChkPresentCompe" runat="server" CssClass="p" 
                                            RepeatLayout="Flow" Width="300px" Height="200px" AutoPostBack="True" 
                                            onselectedindexchanged="ChkPresentCompe_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </td>
                                    
                                    <%--<td width="305">
                                        <asp:Label ID="Label5" runat="server" CssClass="labelsN" Text="Presentaciones de Actividades en el comercio" Width="200px"></asp:Label>
                                        <br />                                        
                                        <asp:Label ID="Label7" runat="server" CssClass="labelsN" Text="Presentacion1" 
                                        Width="100px"></asp:Label>
                                        <asp:TextBox ID="txtpresentacomp" runat="server" Height="21px" 
                                        Width="183px" MaxLength="50"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Reqprrecompe" 
                                        runat="server" ControlToValidate="txtpresentacomp" Display="none" 
                                        ErrorMessage="No ingrese caracteres especiales" 
                                        ValidationExpression="([a-zA-Z][\w\sñÑáéíóúÁÉÍÓÚ0-9.]{0,49})"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender ID="recacomp" runat="server" 
                                        Enabled="True" TargetControlID="Reqprrecompe"></cc1:ValidatorCalloutExtender>
                                        <br />
                                        <asp:Label ID="Label8" runat="server" CssClass="labelsN" Text="Presentacion2" 
                                        Width="100px"></asp:Label>
                                        <asp:TextBox ID="txtpresentacomp1" runat="server" Height="21px" Width="183px" MaxLength="50"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Reqcompe1" runat="server" 
                                        ControlToValidate="txtpresentacomp1" Display="none" 
                                        ErrorMessage="No ingrese caracteres especiales " 
                                        ValidationExpression="([a-zA-Z][\w\sñÑáéíóúÁÉÍÓÚ0-9.]{0,49})"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                                        Enabled="True" TargetControlID="Reqcompe1"></cc1:ValidatorCalloutExtender>
                                        <br />
                                        <asp:Label ID="Label9" runat="server" CssClass="labelsN" Text="Presentacion3" 
                                        Width="100px"></asp:Label>
                                        <asp:TextBox ID="txtpresentacomp2" runat="server" Height="21px" Width="183px" MaxLength="50"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Reqcompe2" runat="server" 
                                        ControlToValidate="txtpresentacomp2" Display="none" 
                                        ErrorMessage="No ingrese caracteres especiales " 
                                        ValidationExpression="([a-zA-Z][\w\sñÑáéíóúÁÉÍÓÚ0-9.]{0,49})"></asp:RegularExpressionValidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" 
                                        Enabled="True" TargetControlID="Reqcompe2"></cc1:ValidatorCalloutExtender>
                                    </td>--%>
                                </tr>
                            </table>
                            <table id ="consultaCateg" runat="server" align="center" frame="box"
                            style="display:none;">
                                <tr style="border-width: 1px; border-style: solid">
                                    <td>
                                        <asp:Label ID="lblCategoria0" runat="server" CssClass="labelsN" 
                                            Text="Categoría que desea deshabilitar :" Visible="False"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="CmbCategXClienXCanal" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="CmbCategXClienXCanal_SelectedIndexChanged" 
                                            Visible="False" Width="180px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCategoriainfo" runat="server" CssClass="labelsN" 
                                            Text="Presentaciones asociadas:" Visible="False"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="CmbProdPSelec" runat="server" AutoPostBack="True"                                            
                                            Visible="False">
                                        </asp:DropDownList>                                    
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPresPrincipalReg" runat="server" CssClass="labelsN" 
                                            Text="Presentación principal:" Visible="False"></asp:Label>
                                        <br />
                                        <asp:Label ID="TxtPresPrincipalReg" runat="server" CssClass="labelsN" 
                                            Text="" Visible="False"></asp:Label>                                        
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCategoriainfocompe" runat="server" CssClass="labelsN" 
                                            Text="Presentaciones de la competencia:" Visible="False"></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="CmbProdPCompeSelec" runat="server" AutoPostBack="True"                                            
                                            Visible="False">
                                        </asp:DropDownList>                                    
                                    </td>
                                    <td>                                        
                                        <br /> 
                                        <asp:Button ID="BtnDeshabilitareg" runat="server" 
                                            Text="Deshabilitar Categoría" Visible="false" 
                                            onclick="BtnDeshabilitareg_Click" />                                        
                                    </td>
                                </tr>
                            </table>
                            <table align="center">
                                <tr>
                                    <td>                                       
                                        <asp:Button ID="btnaddpresnta" runat="server" CssClass="boton2" Text="Agregar" 
                                        onclick="btnaddpresnta_Click"/>                           
                                    </td>
                                    <td>
                                        <asp:Button ID="btnconsultapresnta" runat="server" CssClass="boton2" 
                                            Text="Consultar" onclick="btnconsultapresnta_Click"/>                           
                                    </td>
                                    <td>
                                        <asp:Button ID="btncancelpresn" runat="server" CssClass="boton2" 
                                        Text="Cancelar" onclick="btncancelpresn_Click" />                           
                                    </td>
                                </tr>                                
                           </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel17" runat="server" HeaderText="Seleccionar Reporte Fotografico" Enabled="true">
       <ContentTemplate>
       <asp:UpdatePanel ID= "upphoto" runat="server">
       <ContentTemplate>
        <asp:Panel ID="PSearchfotos" runat="server" 
                     BorderStyle="None" GroupingText="Parámetros de consulta">
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>          
                                <asp:Label ID="lblfecha" runat="server" Text="Desde" CssClass="labelsN"
                                     ToolTip="Ingrese la fecha en la cual se tomó la fotografía" ></asp:Label>
                            </td> 
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtfecha" runat="server" Width="70px" AutoPostBack="True" 
                                                CausesValidation="True" CssClass="StiloCombo" 
                                                ontextchanged="txtfecha_TextChanged"></asp:TextBox>
                                            
                                            <cc1:MaskedEditExtender ID="txtfecha_MaskedEditExtender" runat="server" 
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtfecha" 
                                                UserDateFormat="DayMonthYear"></cc1:MaskedEditExtender>
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="imgcalendar" runat="server" Height="21px" 
                                            ImageUrl="~/Pages/images/calendario.JPG" Width="21px" />                            
                                            <asp:Label ID="LblFechainiObl" runat="server" Text="*" Font-Bold="True" 
                                            Font-Size="10px" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>                                       
                                <cc1:CalendarExtender ID="TxtFechaFoto_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgcalendar" 
                                    TargetControlID="txtfecha"></cc1:CalendarExtender>                    
                            </td>
                            <td>                 
                                <asp:Label ID="lblcampa" runat="server" CssClass="labelsN" Text="Campaña"></asp:Label>
                            </td>
                            <td>                 
                                <asp:DropDownList ID="cmbcampa" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="cmbcampa_SelectedIndexChanged" Width="400px">
                                </asp:DropDownList>
                            </td>   
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblfecha2" runat="server" Text="Hasta" CssClass="labelsN"
                                     ToolTip="Ingrese la fecha en la cual se tomó la fotografía" ></asp:Label>                
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtfecha2" runat="server" Width="70px" AutoPostBack="True" 
                                                CausesValidation="True" CssClass="StiloCombo" 
                                                ontextchanged="txtfecha2_TextChanged"></asp:TextBox>
                                            
                                            <cc1:MaskedEditExtender ID="txtfecha2_MaskedEditExtender" runat="server" 
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtfecha2" 
                                                UserDateFormat="DayMonthYear"></cc1:MaskedEditExtender>
                                        </td>
                                        <td align="center">
                                            <asp:ImageButton ID="imgcalendar2" runat="server" Height="21px" 
                                            ImageUrl="~/Pages/images/calendario.JPG" Width="21px" />                            
                                            <asp:Label ID="LblFechafinObl" runat="server" Text="*" Font-Bold="True" 
                                            Font-Size="10px" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>                                       
                                <cc1:CalendarExtender ID="TxtFechaFoto2_CalendarExtender" runat="server" 
                                    Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgcalendar2" 
                                    TargetControlID="txtfecha2"></cc1:CalendarExtender>                    
                            </td>
                            <td>                   
                                <asp:Label ID="lbllugar" runat="server" CssClass="labelsN" Text="Lugar"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmblugar" runat="server" Width="400px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnloadphoto" runat="server" CssClass="boton2" Height="16px" 
                                    onclick="btnloadphoto_Click" Text="Consultar" Width="114px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="fngenDet" runat="server" 
                                    Text="Cantidad de fotografias disponibles para seleccionar" CssClass="labelsN"></asp:Label>
                                <br />
                                <asp:Label ID="Fngeneral" runat="server" Text="General:" CssClass="labelsN"></asp:Label>
                                <asp:Label ID="CountFngeneral" runat="server" CssClass="labelsN"></asp:Label>
                                <br />
                                <asp:Label ID="Fndetallado" runat="server" Text="Detallado:" CssClass="labelsN"></asp:Label>
                                <asp:Label ID="CountFnDetallado" runat="server" CssClass="labelsN"></asp:Label>
                            </td>                
                        </tr>            
                    </table>                
                </td>
            </tr>
        </table> 
               
        </asp:Panel>
       <br />             
        <table align="center">            
            <tr>
                <td>
                        <asp:GridView ID="gvphotogra" runat="server" AutoGenerateColumns="False" 
                             Width="334px" Height="367px" ShowFooter="True" BackColor="White" 
                            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
                            GridLines="Horizontal" EmptyDataText="Sr. Usuario no existen registros para esta consulta.">
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="id_Photographs" HeaderText="Cod_Foto" >
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Foto">
                                    <ItemTemplate>
                                        <asp:Image ID="ImgPhoto" runat="server" CssClass="imagenstrech" Height="152px" 
                                           ImageAlign="Middle" Width="226px"  />
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comentario">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtcomentario" runat="server" Height="152px" 
                                            TextMode="MultiLine" Width="500px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo de Vista">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width:120px">
                                                    <asp:CheckBox ID ="ChkGeneral" runat="server" Text="General" />
                                                </td>
                                                <td style="width:120px">
                                                    <asp:CheckBox ID ="ChkDetallado" runat="server" Text ="Detallado" />
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        
                                       
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnagregafotos" runat="server" CssClass="boton2" 
                                            EnableTheming="False" onclick="btnagregafotos_Click" Text="Agregar" />
                                    </FooterTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>                                
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>                              
                </td>
            </tr>        
        </table>       
       </ContentTemplate>       
       </asp:UpdatePanel>
       
       </ContentTemplate>
       
       
       </cc1:TabPanel>
       
        <cc1:TabPanel ID="TabPanel3" runat="server" 
            HeaderText="Seleccion Actividades en el Comercio">
            <HeaderTemplate>
                Seleccion Actividades en el Comercio
            </HeaderTemplate>
        <ContentTemplate>
        <div>
        <asp:UpdatePanel ID="upacticomercio" runat="server">
        <ContentTemplate>
        <br />
        <table align="center">
            <tr>
                <td>                
                    <asp:Label ID="LblActividad" runat="server" CssClass="labelsN" Text="Seleccione las tres principales Actividades del comercio, cambie la observación de la actividad y escriba los comentarios de las fotografías si así lo requiere."></asp:Label>
                </td>
            </tr>
        </table>                
        <table align="center">        
            <tr>
                <td valign="top">            
                    <div class="ScrollActividades">
                       
                        <asp:GridView ID="GVActividades" runat="server" AutoGenerateColumns="False" 
                            ShowFooter="True" BackColor="White" 
                            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
                            GridLines="Horizontal">
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkSelActividad" runat="server"/>                                        
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>                                
                                                               
                                <asp:BoundField DataField="id_cinfo" HeaderText="Cod." 
                                    FooterStyle-VerticalAlign="Top"  >
                                    
                                    <FooterStyle VerticalAlign="Top" />
                                </asp:BoundField>
                                    
                                <asp:BoundField DataField="cinfo_Name_Activity" HeaderText="Actividad" 
                                    ControlStyle-Width="100" >                                
                                    <ControlStyle Width="100px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Observación">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtObservacion" runat="server" Height="70px" 
                                            TextMode="MultiLine" Width="220px"></asp:TextBox>                                        
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>                                
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </div> 
                    
                </td>
                <td valign="top">
                      <div class="ScrollActividadesphoto">
                        <asp:GridView ID="gvactivi" runat="server" AutoGenerateColumns="False" 
                              Height="324px" ShowFooter="True" BackColor="White" 
                              BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
                              GridLines="Horizontal">
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <Columns>
                                <asp:BoundField DataField="id_PhotoCI" HeaderText="Cod_Foto" />
                                <asp:BoundField DataField="id_cinfo" HeaderText="Cod_Actividad" />
                                <asp:BoundField DataField="cinfo_Name_Activity" HeaderText="Actividad"  
                                    ControlStyle-Width="100" >                                
                                    <ControlStyle Width="100px" />
                                </asp:BoundField>                                
                                <asp:TemplateField HeaderText="Foto">
                                    <ItemTemplate>
                                        <asp:Image ID="ImgPhotoa" runat="server" Height="152px" 
                                           ImageAlign="Middle" Width="226px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comentario">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtcomentario" runat="server" Height="152px" 
                                            TextMode="MultiLine" Width="240px"></asp:TextBox>                                        
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:TemplateField>                                
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
        <table align="center">
            <tr>
                <td>
                      <asp:Button ID="btnUpdateActCom" runat="server" CssClass="boton2" 
                            Text="Actualizar" onclick="btnUpdateActCom_Click" />
                </td>
            </tr>
        </table>
        
            
        
        </ContentTemplate>
        
        
        
        
        </asp:UpdatePanel>
        
        
        </div>
        
        </ContentTemplate>
        </cc1:TabPanel>
        
    </cc1:TabContainer>
      
     
        
          <asp:Panel ID="PMensajes" runat="server" 
                        Height="169px" Width="332px" style="display:none;">
                        <table align="center">
                            <tr>
                                <td align="center" class="style50" valign="top">
                                    <br />
                                </td>
                                <td class="style49" valign="top">
                                    <br />
                                    <asp:Label ID="lblencabezado" runat="server" CssClass="labelsTit"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="lblmensajegeneral" runat="server" CssClass="labels"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table align="center">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnaceptar" runat="server" CssClass="button" 
                                        Text="Aceptar" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="AlertCambioCiudad" runat="server" CssClass="MensajesFotosConfirm" 
                        Height="169px" style="display:none;"  Width="332px">
                        <table align="center">
                            <tr>
                                <td align="center" class="style50" valign="top">
                                    <br />
                                </td>
                                <td class="style49" valign="top">
                                    <br />
                                    <asp:Label ID="Label1" runat="server" CssClass="labelsTit" 
                                        Text="Ciudad Principal"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="LblChangeCiudadPricipal" runat="server" CssClass="labels" 
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table align="center">
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="ImgBtnSI" runat="server" 
                                        ImageUrl="~/Pages/images/BtnSI.png"
                                        onmouseout="this.src = '../../images/BtnSI.png'" 
                                        onmouseover="this.src = '../../images/BtnSIDown.png'" 
                                        onclick="ImgBtnSI_Click" />
                                    <asp:ImageButton ID="ImgBtnNO" runat="server" 
                                        ImageUrl="~/Pages/images/BtnNO.png"
                                        onmouseout="this.src = '../../images/BtnNO.png'" 
                                        onmouseover="this.src = '../../images/BtnNODown.png'" />                                    
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalPopupciudadprincipal" runat="server" 
                        BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" 
                        OkControlID="ImgBtnNO" PopupControlID="AlertCambioCiudad" 
                        TargetControlID="BtndisparaalertasCiudadPrincipal">
                    </cc1:ModalPopupExtender>
                    
                     <asp:Panel ID="AlertCambioCategorias" runat="server" CssClass="MensajesFotosConfirm" 
                        Height="169px" style="display:none;"  Width="332px">
                        <table align="center">
                            <tr>
                                <td align="center" class="style50" valign="top">
                                    <br />
                                </td>
                                <td class="style49" valign="top">
                                    <br />
                                    <asp:Label ID="lblChangecateg" runat="server" CssClass="labelsTit" 
                                        Text="Cambio Asignación"></asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="LblChangeAsignacion" runat="server" CssClass="labels" 
                                        Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table align="center">
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="BtnSICateg" runat="server" 
                                        ImageUrl="~/Pages/images/BtnSI.png"
                                        onmouseout="this.src = '../../images/BtnSI.png'" 
                                        onmouseover="this.src = '../../images/BtnSIDown.png'" onclick="BtnSICateg_Click" 
                                        />
                                    <asp:ImageButton ID="BtnNOCateg" runat="server" 
                                        ImageUrl="~/Pages/images/BtnNO.png"
                                        onmouseout="this.src = '../../images/BtnNO.png'" 
                                        onmouseover="this.src = '../../images/BtnNODown.png'" />                                    
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="ModalPopupCambioCategorias" runat="server" 
                        BackgroundCssClass="modalBackground" DropShadow="True" Enabled="True" 
                        OkControlID="BtnNOCateg" PopupControlID="AlertCambioCategorias" 
                        TargetControlID="Btndisparaalertacambiocateg">
                    </cc1:ModalPopupExtender>
                
         
        </ContentTemplate></asp:UpdatePanel> 
         
         
        
         
         
    </form>
    </body>
</html>
