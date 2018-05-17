using System;
using System.Configuration;
using System.Web.UI;

namespace SIGE.Pages.Modulos.Administrativo
{
    public partial class MOD_Admin : System.Web.UI.Page
    {
        private string planningADM;
        //private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos PAdmin = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();

        private void inicializa_botones()
        {
            ImgBtnPais.Enabled = true;
            ImgBtnUsuario.Enabled = true;
            ImgBtnProducto.Enabled = true;
            ImgBtnPdv.Enabled = true;
            ImgBtnInformes.Enabled = true;
            ImgBtnGeneral.Enabled = true;
        }
        protected void ImgCloseSession_Click(object sender, ImageClickEventArgs e)
        {
            //PAdmin.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
            this.Session.Abandon();
            Response.Redirect("~/login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    try
                    {
                         //planningADM = this.Session["AdmProd"].ToString().Trim();
                    }
                    catch 
                    { 
                    }
                        /*string sUser = this.Session["sUser"].ToString();
                        string sPassw = this.Session["sPassw"].ToString();
                        string sNameUser = this.Session["nameuser"].ToString();*/
                        
                        string sUser = "sUser";
                        string sPassw = "sPassw";
                        string sNameUser = "nameuser";
                        planningADM = "NO";

                        if (sUser != null && sPassw != null)
                        {
                            lblUsuario.Text = sNameUser;
                            if (planningADM == "SI")
                            {
                                ImgBtnPais.Visible = false;
                                ImgBtnUsuario.Visible = false;
                                ImgBtnProducto.Visible = false;
                                ImgBtnPdv.Visible = false;
                                ImgBtnInformes.Visible = false;
                                ImgBtnGeneral.Visible = false;
                                Iframe.Attributes["src"] = "GestiónProducto.aspx";
                                //inicializa_botones();
                                //ImgBtnProducto.Enabled = false; 
                            }
                            else 
                            {
                                inicializa_botones();
                            }
                            // TxtSolicitante.Text = this.Session["smail"].ToString();
                            //TxtEmail.Text = "AdminXplora@lucky.com.pe";
                        }
                }
                catch (Exception ex)
                {
                    string error = "";
                    string mensaje = "";
                    error = Convert.ToString(ex.Message);
                    mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                    if (error == mensaje)
                    {
                        Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                        string errMessage = "";
                        errMessage = mensaje;
                        errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                        this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                    }
                    else
                    {
                        this.Session.Abandon();
                        Response.Redirect("~/err_mensaje_seccion.aspx", true);
                    }
                }
            }
        }

        protected void ImgBtnPais_Click(object sender, EventArgs e)
        {            
            Iframe.Attributes["src"] = "GestiónPaís.aspx";            
            inicializa_botones();
            ImgBtnPais.Enabled = false;           
        }

        protected void ImgBtnUsuario_Click(object sender, EventArgs e)
        {
            Iframe.Attributes["src"] = "GestiónUsuario.aspx";
            inicializa_botones();
            ImgBtnUsuario.Enabled = false;           
        }

        protected void ImgBtnProducto_Click(object sender, EventArgs e)
        {
            Iframe.Attributes["src"] = "GestiónProducto.aspx";            
            inicializa_botones();
            ImgBtnProducto.Enabled = false;           
        }

        protected void ImgBtnPdv_Click(object sender, EventArgs e)
        {
            Iframe.Attributes["src"] = "GestiónPuntodeVenta.aspx";
            inicializa_botones();
            ImgBtnPdv.Enabled = false;           
        }

        protected void ImgBtnInformes_Click(object sender, EventArgs e)
        {
            Iframe.Attributes["src"] = "GestiónInformes.aspx";
            inicializa_botones();
            ImgBtnInformes.Enabled = false;           
        }

        protected void ImgBtnGeneral_Click(object sender, EventArgs e)
        {
            Iframe.Attributes["src"] = "GestiónGeneral.aspx";
            inicializa_botones();
            ImgBtnGeneral.Enabled = false;
        }
    }       
}