using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_Principal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string sidperdil = this.Session["Perfilid"].ToString();
                if (!Page.IsPostBack)
                {

                    if (ConfigurationManager.AppSettings["PerfilAnalista"] == sidperdil || sidperdil == "0033")
                    {
                        //ir_inicioplan.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
                throw ex;
            }
        }

        protected void link_btn_atras_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Principal.aspx");
        }
        protected void ir_inicioplan_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Modulos/Planning/Menu_Planning.aspx", true);
        }
    }
}
