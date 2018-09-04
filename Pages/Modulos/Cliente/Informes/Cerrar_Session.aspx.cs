using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Xml;
using System.Globalization;
using Lucky.Entity.Common.Security;
using Lucky.Entity.Common.Application.Security;
using Lucky.Business.Common.Security;
using Lucky.Business.Common.Application;
using Lucky.CFG;
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.CFG.Tools;
using SIGE.Facade_Procesos_Administrativos;

namespace SIGE.Pages.Modulos.Cliente.Informes
{
    public partial class Cerrar_Session : System.Web.UI.Page
    {
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos PdAmon = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private void LimpiarCokkies()
        {
            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(aCookie);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            PdAmon.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
            /* colocar label de session user */
            this.Session.Abandon();
            LimpiarCokkies();

            

            
        }
    }
}
