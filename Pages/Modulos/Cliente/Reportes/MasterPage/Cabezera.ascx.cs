using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Data;
using Telerik.Web.UI;

namespace SIGE.Pages.Modulos.Cliente.Reportes.MasterPage
{
    public partial class Cabezera : System.Web.UI.UserControl
    {
        string url_foto;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                url_foto = this.Session["fotocomany"].ToString();
                Imgcliente.ImageUrl = url_foto;
                if (url_foto == null)
                {
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
                
            }
            catch(Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);

                ex.Message.ToString();
            }

            
        }
        
    }
    
}