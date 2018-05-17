using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.Pages.Modulos.Supervision
{
    public partial class PrevFormatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            

        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Modulos/Supervision/ini_supervision.aspx", true);
        }

       
    }
}
