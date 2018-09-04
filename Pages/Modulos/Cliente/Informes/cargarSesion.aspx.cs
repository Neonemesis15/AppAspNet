using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.Pages.Modulos.Cliente.Informes
{
    public partial class cargarSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string a, b, c;
            //if (Request.QueryString.Equals("idcanal"))
            //{
            //    Session["Canal"] = Request.QueryString["idcanal"];
            //    b = Session["Canal"].ToString();
            //}
            
            this.Session["Service"]  = Request.QueryString["servicio"];
            this.Session["Canal"] = Request.QueryString["idcanal"];
            this.Session["Nivel"] = Request.QueryString["nivel"];
            a = Session["Service"].ToString();
            b = Session["Canal"].ToString();
            c = Session["Nivel"].ToString();

            

           
        }
    }
}
