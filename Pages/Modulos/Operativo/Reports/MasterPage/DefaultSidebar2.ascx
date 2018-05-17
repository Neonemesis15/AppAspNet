<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefaultSidebar2.ascx.cs" Inherits="SIGE.Pages.Modulos.Operativo.Reports.MasterPage.DefaultSidebar2" %>

<%@ Import Namespace="Artisteer" %>
<%@ Register TagPrefix="artisteer" Namespace="Artisteer" %>
<%@ Register TagPrefix="art" TagName="DefaultVerticalMenu" Src="DefaultVerticalMenu.ascx" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<art:DefaultVerticalMenu ID="DefaultVMenuContent" runat="server" />
<artisteer:Block ID="HighlightsBlock" Caption="Novedades" runat="server">
    <ContentTemplate>
        <div>
            <%string fecha = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year; %>
            <ul>
                <li><b>
                    <% Response.Write(fecha); %></b>
                    <p>
                        Data Mercadersitas.<br />
                        <a href="#">Leer...</a>
                    </p>
                </li>
            </ul>
            <ul>
                <li><b>Servicios</b>
                    <p>
                        Reportes .<br />
                        <a href="#">Leer...</a>
                    </p>
                </li>
            </ul>
        </div>
    </ContentTemplate>
</artisteer:Block>
