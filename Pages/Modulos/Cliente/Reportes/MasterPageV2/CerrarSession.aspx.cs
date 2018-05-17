using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.Pages.Modulos.Cliente.Reportes.MasterPageV2
{
    public partial class CerrarSession : System.Web.UI.Page
    {
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Get_Administrativo.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                    this.Session.Abandon();
                    Response.Redirect("~/login.aspx", true);
                }
                catch (Exception ex)
                {
                    //Get_Administrativo.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                    this.Session.Abandon();
                    Response.Redirect("~/login.aspx", true);
                    ex.Message.ToString();
                }
            }
        }
    }
}