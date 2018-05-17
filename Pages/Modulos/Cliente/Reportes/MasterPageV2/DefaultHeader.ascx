<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefaultHeader.ascx.cs"
    Inherits="SIGE.Pages.Modulos.Cliente.Reportes.MasterPageV2.DefaultHeader" %>
<div class="art-logo">
    
    <table>
    <tr>
    <td colspan="2" align="center">
    <h1 id="name-text" class="art-logo-name">
            <asp:Image ID="img_cliente" runat="server" Height="20px" 
            ImageUrl="~/Pages/ImgBooom/logo_lucky.png" Width="129px" /></h1>
            <asp:Label ID="lblUsuario" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="#114092"></asp:Label>
     <asp:Label ID="usersession" runat="server" Visible="False" Font-Names="Verdana" Font-Size="9pt"
            ForeColor="#114092"></asp:Label>
            <br />
        <asp:Label ID="lblcompany" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="#114092"></asp:Label>
        <br />
    </td>
    </tr>
 
    </table>

             

        
    <%--<div id="slogan-text" class="art-logo-text">
        Marketing Promocional</div>--%>
</div>
