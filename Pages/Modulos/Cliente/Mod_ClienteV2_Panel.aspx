<%@ Page Language="C#" MasterPageFile="~/Pages/Modulos/Cliente/Reportes/MasterPage/Reportes_V2.Master" AutoEventWireup="true" CodeBehind="Mod_ClienteV2_Panel.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Mod_ClienteV2_Panel" %>

<%-- Referencias cabecera--%>
<%@ Register TagPrefix="art" TagName="Cabezera" Src="~/Pages/Modulos/Cliente/Reportes/MasterPage/Cabezera.ascx" %>

<%-- footer--%>
<%@ Register TagPrefix="art" TagName="pie" Src="~/Pages/Modulos/Cliente/Reportes/MasterPage/pie.ascx" %>
<%-- end footer--%>
<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolder_head" runat="server">
    <art:Cabezera ID="Cabezera" runat="server" />
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="ContentPlaceHolder_body" runat="server">

Contenido de la pagina

</asp:Content>