using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.Pages.Modulos.Cliente.Reportes.MasterPageV2
{
    public partial class DefaultHeader : System.Web.UI.UserControl
    {
        string url_foto;
        string sUser;
        string company;
        string sNameUser;


        private void ObrenerDatosUsuario()
        {
            if (this.Session["sUser"] == null || this.Session["sNombre"] == null || this.Session["nameuser"] == null)
            {


                usersession.Text = "";
                lblUsuario.Text = "";
                lblcompany.Text = "";

            }
            else {

                sUser = this.Session["sUser"].ToString();
                company = this.Session["sNombre"].ToString();
                sNameUser = this.Session["nameuser"].ToString();
                usersession.Text = sUser;
                lblUsuario.Text = sNameUser;
                lblcompany.Text = company;
            
            
            
            
            }



        }
        protected void Page_Load(object sender, EventArgs e)
        {


           


           
            if (!IsPostBack)
            {
                ObrenerDatosUsuario();
            
                if (this.Session["fotocomany"] == null )
                    url_foto = "~/Pages/ImgBooom/logo_lucky.png";
                    
                     
               
                else
                    url_foto = this.Session["fotocomany"].ToString();
                

                img_cliente.ImageUrl = url_foto;
                 
            }
        }
    }
}