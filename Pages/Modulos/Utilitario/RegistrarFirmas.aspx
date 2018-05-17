<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Utilitario/MasterPage/design/MasterPage.master" AutoEventWireup="true" CodeBehind="RegistrarFirmas.aspx.cs" Inherits="SIGE.Pages.Modulos.Utilitario.RegistrarFirmas" %>

<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultMenu" Src="~/Pages/Modulos/Utilitario/MasterPage/DefaultMenu.ascx" %>
<%@ Register TagPrefix="art" TagName="DefaultHeader" Src="~/Pages/Modulos/Utilitario/MasterPage/DefaultHeader.ascx" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultSidebar1" Src="~/Pages/Modulos/Utilitario/MasterPage/DefaultSidebar1.ascx" %>
<%@ Register Assembly="obout_Show_Net" Namespace="OboutInc.Show" TagPrefix="obshow" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControls" Namespace="AjaxControls" TagPrefix="cc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    <art:defaultheader ID="DefaultHeader" runat="server" />
            <style type="text/css">
        .style1
        {
            width: 128px;
        }
    </style>
</asp:Content>
<asp:Content ID="MenuContent" ContentPlaceHolderID="MenuContentPlaceHolder" runat="Server">
    <art:defaultmenu ID="DefaultMenuContent" runat="server" />
</asp:Content>
<asp:Content ID="SheetContent" ContentPlaceHolderID="SheetContentPlaceHolder" runat="Server">
    <div align="center" >
    <br />
    <fieldset    style="width:850px"  >
            <legend  > Registro de datos</legend>

            <table align="center">
             <tr>
             <td  align="right">Nombre :</td> <td class="style1"> 
             <asp:TextBox runat="server"  ID="txtNombre" style="border-width:1px;height:18px" 
                     BorderStyle="Solid" ></asp:TextBox>
            

                 </td>
           <td  align="right">Anexo :</td>
           <td>
          
               <telerik:RadNumericTextBox ID="txtAnexo" runat="server" Culture="es-PE" 
                                EmptyMessage="Solo Numeros" 
                                IncrementSettings-InterceptMouseWheel="False" MaxLength="4"
                                 ShowSpinButtons="false" Skin="Telerik" >
<IncrementSettings InterceptMouseWheel="False"></IncrementSettings>
                            <NumberFormat DecimalDigits="0" GroupSeparator="" GroupSizes="9" />
                            </telerik:RadNumericTextBox>
           </td>
            </tr>
             <tr>
           <td  align="right"> Apellido Paterno:</td>
           <td class="style1">
           <asp:TextBox ID="txtApePaterno" runat="server" style="border-width:1px;height:18px" 
                     BorderStyle="Solid"  > </asp:TextBox>
               <script type="text/javascript">
                   function ValidNum(e) {
                       var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
                       return ((tecla > 47 && tecla < 58) || tecla == 46);
                   }

                   //validar solo letras
                   function validar(e,txt) {
                       tecla = (document.all) ? e.keyCode : e.which;
                       if (tecla == 8) return true;
                       patron = /[A-Za-zñÑ.\s]/;
                      
                       if ((tecla > 47 && tecla < 58) || tecla == 46) {
                           var txtNombre = document.getElementById(txt);
                           txtNombre.style.borderColor = "red";
//                           var txtApePaterno = document.getElementById('ctl00_SheetContentPlaceHolder_txtApePaterno');
//                           txtApePaterno.style.borderColor = "red";
//                           var txtApeMaterno = document.getElementById('ctl00_SheetContentPlaceHolder_txtApeMaterno');
//                           txtApeMaterno.style.borderColor = "red";
//                           var txtCargo = document.getElementById('ctl00_SheetContentPlaceHolder_txtCargo');
//                           txtCargo.style.borderColor = "red";
                       }
                       else {
                           var txtNombre = document.getElementById(txt);
                           txtNombre.style.borderColor = "green";
//                           var txtApePaterno = document.getElementById('ctl00_SheetContentPlaceHolder_txtApePaterno');
//                           txtApePaterno.style.borderColor = "green";
//                           var txtApeMaterno = document.getElementById('ctl00_SheetContentPlaceHolder_txtApeMaterno');
//                           txtApeMaterno.style.borderColor = "green";
//                           var txtCargo = document.getElementById('ctl00_SheetContentPlaceHolder_txtCargo');
//                           txtCargo.style.borderColor = "green";
                       }

                       te = String.fromCharCode(tecla);

                       return patron.test(te);
                   }
                   function txt(e,txt) {

                       var txtNombre = document.getElementById(txt);
                       txtNombre.style.borderColor = "green";
//                       var txtApePaterno = document.getElementById('ctl00_SheetContentPlaceHolder_txtApePaterno');
//                       txtApePaterno.style.borderColor = "green";
//                       var txtApeMaterno = document.getElementById('ctl00_SheetContentPlaceHolder_txtApeMaterno');
//                       txtApeMaterno.style.borderColor = "green";
//                       var txtCargo = document.getElementById('ctl00_SheetContentPlaceHolder_txtCargo');
//                       txtCargo.style.borderColor = "green";

                   }
                   function txt1(e, txt) {

                       var txtNombre = document.getElementById(txt);
                       txtNombre.style.borderColor = "black";
                       //                       var txtApePaterno = document.getElementById('ctl00_SheetContentPlaceHolder_txtApePaterno');
                       //                       txtApePaterno.style.borderColor = "green";
                       //                       var txtApeMaterno = document.getElementById('ctl00_SheetContentPlaceHolder_txtApeMaterno');
                       //                       txtApeMaterno.style.borderColor = "green";
                       //                       var txtCargo = document.getElementById('ctl00_SheetContentPlaceHolder_txtCargo');
                       //                       txtCargo.style.borderColor = "green";

                   }
</script>
                 </td>
           <td  align="right">RPC :</td>
           <td>
          
               <telerik:RadNumericTextBox ID="txtCelular" runat="server" Culture="es-PE" 
                                EmptyMessage="Solo Numeros" 
                                IncrementSettings-InterceptMouseWheel="False" MaxLength="9"
                                 ShowSpinButtons="false" Skin="Telerik" 
                   DataType="System.String" >
<IncrementSettings InterceptMouseWheel="False"></IncrementSettings>
                            <NumberFormat DecimalDigits="0" GroupSeparator="" GroupSizes="9" />
                            </telerik:RadNumericTextBox>
           </td>
            </tr>
             <tr>
           <td  align="right">&nbsp;Apellido Materno:</td>
           <td class="style1">
           <asp:TextBox ID="txtApeMaterno" runat="server" style="border-width:1px;height:18px" 
                     BorderStyle="Solid"   ></asp:TextBox>
                 </td>
           <td  align="right">RPM :</td>
           <td>
           

                            <asp:TextBox ID="txtCelularRPM" runat="server"  style="border-width:1px;height:18px" 
                     BorderStyle="Solid"   ></asp:TextBox>
           </td>
            </tr>
             <tr>
           <td  align="right">Cargo:</td>
           <td class="style1">
           <asp:TextBox ID="txtCargo" runat="server"  style="border-width:1px;height:18px" 
                     BorderStyle="Solid"   ></asp:TextBox>
              
                 </td>
           <td  align="right">Nextel :</td>
           <td>
             <asp:TextBox ID="txtCelularNextel" runat="server"  style="border-width:1px;height:18px" 
                     BorderStyle="Solid"   ></asp:TextBox>
                 </td>
            </tr>
                         <tr>
           <td  align="right">Telefono:</td>
           <td class="style1">
                <telerik:RadNumericTextBox ID="txtTelefono" runat="server" Culture="es-PE" 
                                EmptyMessage="Solo Numeros" 
                                IncrementSettings-InterceptMouseWheel="False" MaxLength="7"
                                 ShowSpinButtons="false" Skin="Telerik">
<IncrementSettings InterceptMouseWheel="False"></IncrementSettings>
                            <NumberFormat DecimalDigits="0" GroupSeparator="" GroupSizes="9" />
                            </telerik:RadNumericTextBox>
              
                 </td>
           <td  align="right">&nbsp;</td>
           <td>
               &nbsp;</td>
            </tr>

             <tr>
           <td colspan="4" align="center">
           <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" 
                                Height="25px" Width="164px" onclick="btnRegistrar_Click" />
                                 <asp:Button ID="btnConsultar" runat="server" Text="Consultar" 
                                Height="25px" Width="164px" onclick="btnConsultar_Click"  />
                                 <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                                Height="25px" Width="164px" Visible="false" 
                   onclick="btnCancelar_Click"  />
           </td>
            </tr>
             <tr>
           <td colspan="4">

               <asp:Label ID="lblmensaje" runat="server" Style="text-align: left"></asp:Label></td>
            </tr>

            </table>

            </fieldset>

            
    </div>
    <div align="center">

    <asp:Image runat="server" ID="iFirma" Visible="false" />

    </div>
</asp:Content>
