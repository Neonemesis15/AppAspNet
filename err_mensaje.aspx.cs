using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Data.Common.Application;
using Lucky.Data;
using System.Data;
using Lucky.Entity.Common.Application;
using Lucky.Business.Common.Application;

namespace SIGE
{
    public partial class err_mensaje : System.Web.UI.Page
    {
        Conexion oCoon = new Conexion();
     
        protected void Page_Load(object sender, EventArgs e)
        {

            lblerror.Text = "Se ha perdido la conexion con SIGE. por favor intentelo más tarde";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/login.aspx");
        }
      


        
  
    }
}
