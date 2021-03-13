using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Telerik.Web.UI;

namespace SIGE.Pages.Modulos.Operativo.Reports.MasterPage
{
    public partial class DefaultMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //string sidperdil = this.Session["Perfilid"].ToString();

                if (!Page.IsPostBack){
                   
                    if (ConfigurationManager.
                        AppSettings["PerfilAnalista"].
                        Equals(this.Session["Perfilid"].ToString())){

                        cargarMenuHeaderPlanning();

                    }

                    cargarXploramaps();
                    
                    if (Session["companyid"].ToString().Equals("1562")){
                        cargarDataValidada();
                    }

                    cargarMenu();
                    
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
                throw ex;
            }
        }
        /// <summary>
        /// Add Telerik RadMenu, Option for Close 
        /// </summary>
        protected void cargarMenu()
        {
            RadMenuItem rmitem = new RadMenuItem();

            rmitem.Text = "Cerrar sesión";
            rmitem.NavigateUrl = "~/Pages/Modulos/Cliente/Reportes/MasterPageV2/CerrarSession.aspx";

            RadMenuDataMercaderista.Items.Add(rmitem);
        }
        /// <summary>
        /// Add Telerik RadMenu, Option for to back Menu Planning.
        /// </summary>
        protected void cargarMenuHeaderPlanning()
        {
            RadMenuItem rmitem = new RadMenuItem();

            rmitem.Text = "Menu Planning";
            rmitem.NavigateUrl = "~/Pages/Modulos/Planning/Menu_Planning.aspx";

            RadMenuDataMercaderista.Items.Add(rmitem);
        }
        /// <summary>
        /// Add in Telerik RadMenu 'RadMenuDataMercaderista' the item
        /// relationated with Xplora Maps
        /// </summary>
        protected void cargarXploramaps()
        {
            RadMenuItem rmitem = new RadMenuItem();

            rmitem.Text = "Xplora Maps";
            rmitem.NavigateUrl = "http://sige.lucky.com.pe:8081";

            RadMenuDataMercaderista.Items.Add(rmitem);
        }
        /// <summary>
        /// Add Item Telerik RadMenu for DataValidation
        /// </summary>
        protected void cargarDataValidada()
        {
            RadMenuItem rmitem = new RadMenuItem();

            rmitem.Text = "Reporte de Validación";
            rmitem.NavigateUrl = "~/Pages/Modulos/Operativo/Reports/Report_Alicorp_DataValidada.aspx";

            RadMenuDataMercaderista.Items.Add(rmitem);
        }
    }
}