using System;
using System.Data;
using Lucky.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class verFoto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Int32 iidregft = Convert.ToInt32(Request.QueryString["iidregft"] == null ? "0" : Request.QueryString["iidregft"]);

                if (Int32.Equals(iidregft, 0))
                {
                    Response.Redirect("/login.aspx");
                }
                else
                {
                    DataTable dt = null;
                    Conexion Ocoon = new Conexion();


                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REG_FOTO", iidregft);
                    byte[] byteArrayIn = (byte[])dt.Rows[0]["foto"];


                    Response.OutputStream.Write(byteArrayIn, 0, byteArrayIn.Length);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
    }
}
