using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE
{
    public partial class err_mensaje_seccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblerrorseccion.Text = "Su sesión ha expirado. Si desea ingresar nuevamente por favor haga clic en ir a login";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/login.aspx");
        }
    }
}
