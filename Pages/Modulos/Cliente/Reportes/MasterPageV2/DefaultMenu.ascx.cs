using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Lucky.Data;
using Telerik.Web.UI;

namespace SIGE.Pages.Modulos.Cliente.Reportes.MasterPageV2
{
    public partial class DefaultMenu : System.Web.UI.UserControl
    {
        Conexion oCoon = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Page.Request.Url.AbsolutePath == "")
            //{
             
            //}
            //else
            //{

            //}
            if (!Page.IsPostBack)
            {
                try
                {
                    cargarMenuReports();
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                    this.Session.Abandon();
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
        }

        protected void cargarMenuReports()
        {

            DataSet ds = new DataSet();

            string sidcountry = this.Session["scountry"].ToString();
            int sidcompany = Convert.ToInt32(this.Session["companyid"].ToString());

            ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_GEN_MENU_OBTENER", sidcountry, sidcompany);

            RadMenuItem menuItem = new RadMenuItem();

            menuItem.Value = "1";
            menuItem.Text = "SERVICIOS";
            menuItem.SkinID = "Forest";

            RadMenu_reportes.Items.Add(menuItem);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                RadMenuItem menuItem1 = new RadMenuItem();

                menuItem1.Value = ds.Tables[0].Rows[i]["cod_Strategy"].ToString();
                menuItem1.Text = ds.Tables[0].Rows[i]["Strategy_Name"].ToString();
                menuItem1.NavigateUrl = "";//ds.Tables[0].Rows[i][""].ToString();
                //menuItem.ImageUrl = "~/img/admin_16.png";


                menuItem.Items.Add(menuItem1);

                for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                {
                    if (ds.Tables[0].Rows[i]["cod_Strategy"].ToString().Trim() == ds.Tables[1].Rows[j]["cod_Strategy"].ToString().Trim())
                    {
                        RadMenuItem menuItem2 = new RadMenuItem();


                        menuItem2.Value = ds.Tables[1].Rows[j]["cod_Channel"].ToString();
                        menuItem2.Text = ds.Tables[1].Rows[j]["Channel_Name"].ToString();
                        menuItem2.NavigateUrl = ""; //ds.Tables[0].Rows[i][""].ToString();
                        //menuItem.ImageUrl = ds.Tables[1].Rows[j]["url_imagen"].ToString();

                        menuItem1.Items.Add(menuItem2);


                        for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                        {
                            if (ds.Tables[1].Rows[j]["cod_Channel"].ToString().Trim() == ds.Tables[2].Rows[k]["cod_Channel"].ToString().Trim())
                            {
                                RadMenuItem menuItem3 = new RadMenuItem();

                                menuItem3.Value = ds.Tables[2].Rows[k]["id_Menu"].ToString();
                                menuItem3.Text = ds.Tables[2].Rows[k]["Report_NameReport"].ToString();
                                // menuItem3.NavigateUrl = ds.Tables[2].Rows[k]["url"].ToString();

                                menuItem3.ImageUrl = ds.Tables[2].Rows[k]["url_imagen"].ToString();

                                menuItem2.Items.Add(menuItem3);

                            }
                        }
                    }
                }
            }
            if (sidcompany == 1562)
                cargarMenuInfoGerencial();
            else if (sidcompany == 1572)
                cargarMenuReportes();



            string sidperdil = this.Session["Perfilid"].ToString();
            if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
            {
                RadMenuItem IrMenu = new RadMenuItem();
                IrMenu.Value = "2";
                IrMenu.Text = " IR A MODULOS";
                //IrMenu.ImageUrl = "~/Pages/images/BackModulo.png";
                IrMenu.NavigateUrl = "~/Pages/Modulos/Planning/Menu_Planning.aspx";
                RadMenu_reportes.Items.Add(IrMenu);
            }

            //Aplica en perfil clientte Ing. Carlos H 13/06/2012
            if (sidperdil == ConfigurationManager.AppSettings["PerfilClienteGeneral"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado1"])
            {
                RadMenuItem IrMenuc = new RadMenuItem();
                IrMenuc.Value = "3";
                IrMenuc.Text = " IR A VERSION";
                //IrMenu.ImageUrl = "~/Pages/images/BackModulo.png";
                IrMenuc.NavigateUrl = "~/Pages/Modulos/Cliente/Mod_Cliente_Canales.aspx";
                RadMenu_reportes.Items.Add(IrMenuc);
            }

            //Xplora Maps
            if (sidcompany == 1561)
            {
                RadMenuItem XploraMaps = new RadMenuItem();
                XploraMaps.Value = "4";
                XploraMaps.Text = "XPLORA MAPS";
                XploraMaps.NavigateUrl = "http://services.lucky.com.pe:8181/";

                RadMenu_reportes.Items.Add(XploraMaps);
            }

            //menu para cerrar session
            RadMenuItem ItemCerrarSession = new RadMenuItem();
            ItemCerrarSession.Value = "5";
            ItemCerrarSession.Text = "CERRAR SESION";
            ItemCerrarSession.NavigateUrl = "~/Pages/Modulos/Cliente/Reportes/MasterPageV2/CerrarSession.aspx";

            RadMenu_reportes.Items.Add(ItemCerrarSession);

        }
        protected void RadMenu_reportes_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
        {
             int servicio;
            string Canal;
            string Reporteid;


            RadMenuItem miItem = new RadMenuItem();
            miItem = (RadMenuItem)e.Item;

            DataTable dt = null;
            if (miItem.Level == 3)
            {
                int id_menu = Convert.ToInt32(RadMenu_reportes.SelectedValue.ToString());

                dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_GEN_MENU_OBTENER_MENU_BY_IDMENU", id_menu);

                servicio = Convert.ToInt32(dt.Rows[0]["cod_Strategy"].ToString());
                Canal = dt.Rows[0]["cod_Channel"].ToString();
                Reporteid = dt.Rows[0]["Report_id"].ToString();
                string url = dt.Rows[0]["url"].ToString();


                this.Session["Service"] = servicio;
                this.Session["Canal"] = Canal;
                this.Session["Reporte"] = Reporteid;
                this.Session["id_menu"] = id_menu;

                Response.Redirect(url, true);
            }
            else if (miItem.Level == 2 && (RadMenu_reportes.SelectedValue.ToString() == "1023" || RadMenu_reportes.SelectedValue.ToString() == "1241"))
            {
                this.Session["Canal"] = RadMenu_reportes.SelectedValue.ToString();
                string url = "~/Pages/Modulos/Cliente/Reporte_v1_Cliente.aspx";
                Response.Redirect(url, true);
            }
        }
        protected void cargarMenuInfoGerencial()
        {
            RadMenuItem menuItem = new RadMenuItem();

            menuItem.Value = "1";
            menuItem.Text = "INFORMACION GERENCIAL";
            //menuItem.NavigateUrl = "";

            RadMenu_reportes.Items.Add(menuItem);

            RadMenuItem menuItem1 = new RadMenuItem();

            menuItem1.Value = "1";
            menuItem1.Text = "MANUALES";
            //menuItem1.NavigateUrl = "";

            menuItem.Items.Add(menuItem1);

            RadMenuItem menuItem2 = new RadMenuItem();

            menuItem2.Value = "1";
            menuItem2.Text = "MERCADERISMO";
            menuItem2.NavigateUrl = "~/Pages/Modulos/Cliente/GaleryofBooksOnline/index.html";
            menuItem2.ImageUrl = "~/Pages/Modulos/Cliente/imgs/manual.png";

            menuItem1.Items.Add(menuItem2);

        }
        protected void cargarMenuReportes()
        {
            RadMenuItem menuItem = new RadMenuItem();

            menuItem.Value = "1";
            menuItem.Text = "DEFINICIÓN REPORTES";
            //menuItem.NavigateUrl = "";

            RadMenu_reportes.Items.Add(menuItem);

            RadMenuItem menuItem1 = new RadMenuItem();

            menuItem1.Value = "1";
            menuItem1.Text = "MANUALES";
            //menuItem1.NavigateUrl = "";

            menuItem.Items.Add(menuItem1);

            RadMenuItem menuItem2 = new RadMenuItem();

            menuItem2.Value = "1";
            menuItem2.Text = "Reporte";
            menuItem2.NavigateUrl = "~/Pages/Modulos/Cliente/BookAAVV/index.html";
            menuItem2.ImageUrl = "~/Pages/Modulos/Cliente/imgs/manual.png";

            menuItem1.Items.Add(menuItem2);

        }
    }
}