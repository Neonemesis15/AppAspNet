using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Lucky.Business.Common.Application;
using Lucky.Business.Common.Security;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Data.Common.Security;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Security;
using Lucky.Entity.Common.Application.Security;
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;
using Lucky.CFG.Tools;

namespace SIGE.Pages.Modulos.Planning
{
    public partial class Mod_Planning : System.Web.UI.Page
    {
        string sUser;
        string sPassw;
        string sNameUser;
       

        int j = 0;

       
      

        Conexion oCoon = new Conexion();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE Presupuesto = new SIGE.Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE();
        Facade_Menu_strategy.Facade_MPlanning menu = new SIGE.Facade_Menu_strategy.Facade_MPlanning();
        Facade_Proceso_Planning.Facade_Proceso_Planning Planning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();

        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    sUser = this.Session["sUser"].ToString();
                    sPassw = this.Session["sPassw"].ToString();
                    sNameUser = this.Session["nameuser"].ToString();

                    usersession.Text = sUser;
                    lblUsuario.Text = sNameUser;
                    if (sUser != null && sPassw != null)
                    {
                        iframeExcel.Attributes["src"] = "Cargar_Informes.aspx";
                    }
                }
                catch (Exception ex)
                {
                    Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                    this.Session.Abandon();
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
        }

        protected void ImgCloseSession_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Get_Administrativo.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                this.Session.Abandon();
                Response.Redirect("~/login.aspx", true);
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/login.aspx", true);
            }
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
       
            Response.Redirect("~/Pages/Modulos/Planning/Menu_Planning.aspx", true);
        
        }

      
    }
}
