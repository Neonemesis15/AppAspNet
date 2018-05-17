using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Data;
using Telerik.Web.UI;

namespace SIGE.Pages.Modulos.Cliente.Reportes.MasterPage
{
    public partial class Reportes_V2 : System.Web.UI.MasterPage
    {
        Conexion oCoon = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarMenuReports();
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
            cargarMenuInfoGerencial();
        }

        protected void RadMenu_reportes_ItemClick(object sender, RadMenuEventArgs e)
        {

            int servicio;
            string Canal;


            RadMenuItem miItem = new RadMenuItem();
            miItem = (RadMenuItem)e.Item;

            DataTable dt = null;
            if (miItem.Level == 3)
            {
                int id_menu =Convert.ToInt32(RadMenu_reportes.SelectedValue.ToString());

                dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_GEN_MENU_OBTENER_MENU_BY_IDMENU", id_menu);

                servicio = Convert.ToInt32(dt.Rows[0]["cod_Strategy"].ToString());
                Canal = dt.Rows[0]["cod_Channel"].ToString();

                this.Session["Service"] = servicio;
                this.Session["Canal"] = Canal;

                string url = dt.Rows[0]["url"].ToString();

                Response.Redirect(url,true);
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
            menuItem2.NavigateUrl = "~/Pages/Modulos/Cliente/GaleryofBooksOnline/assets/index.html";


            menuItem1.Items.Add(menuItem2);
        }
    }
}
