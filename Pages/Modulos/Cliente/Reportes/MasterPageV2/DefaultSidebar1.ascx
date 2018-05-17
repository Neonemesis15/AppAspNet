<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DefaultSidebar1.ascx.cs"
    Inherits="Sidebar1" %>
<%@ Register Assembly="Lucky.CFG" Namespace="Artisteer" TagPrefix="artisteer" %>
<%@ Register Assembly="SIGE" Namespace="Artisteer" TagPrefix="artisteer" %>
<%--<%@ Import Namespace="Artisteer"%>--%>
<%--<%@ Register TagPrefix="art" TagName="DefaultVerticalMenu" Src="~/Pages/Modulos/Cliente/Reportes/MasterPageV2/DefaultVerticalMenu.ascx" %>--%>
<%--<artisteer:Block ID="NewsletterBlock" Caption="Newsletter" runat="server">
    <ContentTemplate>
        <div>
            <input type="text" value="" name="email" id="s" style="width: 95%;" />
            <span class="art-button-wrapper"><span class="l"></span><span class="r"></span>
                <input class="art-button" type="submit" name="search" value="Subscribe" />
            </span>
        </div>
    </ContentTemplate>
</artisteer:Block>--%>
<!-- este es el menu vertical si se require para un proximo diseño -->
<%--<art:DefaultVerticalMenu ID="DefaultVMenuContent" runat="server" />--%>
<artisteer:Block ID="HighlightsBlock" Caption="Servicios" runat="server">
    <ContentTemplate>
        <div>
            <ul>
                <li><%int icompany = Convert.ToInt32(this.Session["companyid"]);
                      if (icompany == 1572)
                          Response.Write("<b>Generador de Negocio</b>"); 
                      else
                          Response.Write("<b>Mercaderismo</b>"); 
                          %><%--<b>Mercaderismo</b>--%>
                    <p>
                        Son un conjunto de acciones en el Punto de Venta destinadas a aumentar la 
                        rentabilidad. Colocando el producto en el lugar, durante el tiempo, en la forma, 
                        al precio y en la cantidad más conveniente.<br />
                       <%-- <a href="#">Read more...</a>--%>
                    </p>
                </li>
            </ul>
        </div>
    </ContentTemplate>
</artisteer:Block>
<artisteer:Block ID="HighlightsBlock1" Caption="Novedades" runat="server">
    <ContentTemplate>
  <%--      <div>
            <ul>
                <li><b>Reporte de Precios</b>
                    <p>
                        
                        
                    </p>
                </li>
            </ul>
            <ul>
                <li><b>Reporte de Stock</b>
                    <p>
                     
                    </p>
                </li>
            </ul>
        </div>--%>
    </ContentTemplate>
</artisteer:Block>
<artisteer:Block ID="ContactInformationBlock" Caption="Información de contacto" runat="server">
    <ContentTemplate>
        <div>
            <img src="images/contact.jpg" alt="an image" style="margin: 0 auto; display: block;
                width: 95%" />
            <br />
            <b>Lucky</b><br />
            Jr. General Manuel de Mendiburu 1230 Miraflores, Lima-Perú<br />
            Email: <a href="mailto:mktpromo@lucky.com.pe">mktpromo@lucky.com.pe</a><br />
            <br />
            Central: (511) 610-7400
            <br />
            Fax: (511) 610-7424
        </div>
    </ContentTemplate>
</artisteer:Block>
