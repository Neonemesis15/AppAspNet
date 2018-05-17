using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Lucky.Data;

namespace SIGE.Pages.Modulos.Cliente.Reportes.MasterPage
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarMenuReports();
        }
        protected void cargarMenuReports()
        {
            Conexion oCoon = new Conexion();

            DataSet ds = new DataSet();

            ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_GEN_MENU_OBTENER", "589", 1562);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                RadMenuItem menuItem = new RadMenuItem();

                menuItem.Value = ds.Tables[0].Rows[i]["cod_Strategy"].ToString();
                menuItem.Text = ds.Tables[0].Rows[i]["Strategy_Name"].ToString();
               // menuItem.NavigateUrl ="http://www.hotmail.com"; //ds.Tables[0].Rows[i][""].ToString();
                //menuItem.ImageUrl = "~/img/admin_16.png";
                RadMenu1.Items.Add(menuItem);

                for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                {
                    if (ds.Tables[0].Rows[i]["cod_Strategy"].ToString().Trim() == ds.Tables[1].Rows[j]["cod_Strategy"].ToString().Trim())
                    {
                        RadMenuItem menuItem2 = new RadMenuItem();

                        menuItem2.Value = ds.Tables[1].Rows[j]["cod_Channel"].ToString();
                        menuItem2.Text = ds.Tables[1].Rows[j]["Channel_Name"].ToString();
                       // menuItem2.NavigateUrl = "http://www.hotmail.com"; //ds.Tables[0].Rows[i][""].ToString();
                        //menuItem.ImageUrl = "~/img/admin_16.png";

                        menuItem.Items.Add(menuItem2);


                        for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                        {
                            if (ds.Tables[1].Rows[j]["cod_Channel"].ToString().Trim() == ds.Tables[2].Rows[k]["cod_Channel"].ToString().Trim())
                            {
                            RadMenuItem menuItem3 = new RadMenuItem();

                            menuItem3.Value = ds.Tables[2].Rows[k]["Report_Id"].ToString();
                            menuItem3.Text = ds.Tables[2].Rows[k]["Report_NameReport"].ToString();
                           // menuItem3.NavigateUrl = "http://www.hotmail.com"; //ds.Tables[0].Rows[i][""].ToString();
                            //menuItem.ImageUrl = "~/img/admin_16.png";

                            menuItem2.Items.Add(menuItem3);
                            }
                        }
                    }


                }

            }
        }
        protected void cargarmenu()
        {
            string[] valorsmenu = new string[5];

            valorsmenu[0] = "Reporte Precios";
            valorsmenu[1] = "Reporte Competencia";

            valorsmenu[2] = "Reporte SOD";
            valorsmenu[3] = "Reporte Quiebre";
            valorsmenu[4] = "Reporte Stock";


            for (int i = 0; i < valorsmenu.Length; i++)
            {
                //MenuItem menuItem = new MenuItem();
                RadMenuItem menuItem = new RadMenuItem();

                menuItem.Value = "" + valorsmenu[i];
                menuItem.Text = "" + valorsmenu[i];
                menuItem.NavigateUrl = "" + valorsmenu[i];
                //menuItem.ImageUrl = "~/img/admin_16.png";
                RadMenu1.Items.Add(menuItem);
            }
        }
    }
}