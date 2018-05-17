using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using Lucky.Business.Common.Application;
using Lucky.Business.Common.Security;
using Lucky.CFG.Messenger;
using Lucky.CFG.Util;
using Lucky.Data;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Application.Security;
using Lucky.Entity.Common.Security;

namespace SIGE
{
    /// <summary>
    /// Pagina Inicio SIGE Contiene el formulario de Logueo.
    /// Creado por: Ing. Carlos Alberto Hernandez R
    /// Fecha creacion: 22/04/2009
    /// Requerimiento:
    /// </summary>
    public partial class login : System.Web.UI.Page
    {
        #region declaracion de variables
        private Usuario oUsuario;
        private EntrySeccion oSeccion = new EntrySeccion();
        private string sUser;
        private string sPassw;
        private string sCoutry;
        private string sDepartament;
        private string scity;
        private string smodul;
        private string sNombre;
        private string smail;
        private string idnivel;
        private string snamenivel;
        private string snameuser;
        private Facade_Proceso_Planning.Facade_Proceso_Planning Planning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos ProcesoAdmin = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private Conexion oCoon = new Conexion();
        #endregion

        #region Mensajes

        private void Mensajes_Usuario()
        {
            //imagenalert.Src = "../../images/save_file.jpg";
            //Se obtine el consecutivo del ultimo planning creado
            PMensajes.CssClass = this.Session["cssclass"].ToString();
            lblencabezado.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneral.Text = this.Session["mensaje"].ToString();
            ModalPopupCanal.Show();
        }
        private void Mensajes_Usuario2()
        {
            Pmensaje2.CssClass = this.Session["cssclass"].ToString();
            LblEncabezado2.Text = this.Session["encabemensa"].ToString();
            LblMesanje2.Text = this.Session["mensaje"].ToString();
            ModalPopupExtender2.Show();
        }

        #endregion

        #region Eventos de Componentes de Negocio

        private bool UniqueLogin(string user)
        {
            string myUser = Convert.ToString(Cache[user]);
            // Si el usuario es nulo o cadena vacía...
            if (myUser.ToString() == String.Empty)
            {
                TimeSpan SessTimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
                HttpContext.Current.Cache.Insert(user, user, null, DateTime.MaxValue, SessTimeOut,System.Web.Caching.CacheItemPriority.NotRemovable, null);
                Session["user"] = user;
                return true;
            }
            else
            {
                return false;
            }
        }
        override protected void OnInit(EventArgs e)
        {
            oUsuario = new Usuario();
            //oUsuario.PrimerAcceso += new Lucky.Business.SIGEEventHandler(oUsuario_PrimerAcceso);
            base.OnInit(e);
        }
        private void oUsuario_PrimerAcceso()
        {
            this.Session["sCoutry"] = "";
            this.Session["abr_app"] = "";
            this.Session["app_url"] = "";
            string sPagina = "~/Cambio_pswd.aspx";
            Response.Redirect(sPagina, true);
        }
        private void ObtenerDatosEnvioMail()
        {
            int horalocal = DateTime.Now.Hour;
            string dia = Convert.ToString(DateTime.Now.DayOfWeek);
            if (dia == Convert.ToString(DayOfWeek.Friday) && horalocal >= 10)
            {
                string fechavalida = Convert.ToString(DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " 10:01:00");
                DataSet dsenvio = null;
                dsenvio = oCoon.ejecutarDataSet("UP_WEBXPLORA_AD_OBTENERDIASENVIO");

                // validar existencia de informes para enviar por correo
                // recupera servicio y cliente
                if (dsenvio != null)
                {
                    if (dsenvio.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dsenvio.Tables[0].Rows.Count - 1; i++)
                        {
                            //Por Solicitud de Alicorp se inactiva el envio de correos hasta nueva orden Ing. Carlos Hernandez
                            //envio de correo al cliente con información de los reportes cargados
                            try
                            {
                                //De Acuerdo a Requerimiento de5 Alicorp solicito que los correos se enviaran x Canal, Servicio Ing. Carlos Alberto Hernández Rincón
                                DataTable dtpersonalenvio = null;

                                if (Convert.ToInt32(dsenvio.Tables[0].Rows[i][1].ToString().Trim()) != 1561)
                                {
                                    // recuperar listado general de personas a las cuales se les enviará correo
                                    dtpersonalenvio = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLANNING_OBTENEREMAILUSERS", 0, Convert.ToInt32(dsenvio.Tables[0].Rows[i][1].ToString().Trim()), 0, "none", Convert.ToInt32(dsenvio.Tables[0].Rows[i][0].ToString().Trim()), "none");
                                    //Planning.Get_Obtener_Datos_Cliente(0, Convert.ToInt32(dsenvio.Tables[0].Rows[i][1].ToString().Trim()), 0, "none", Convert.ToInt32(dsenvio.Tables[0].Rows[i][0].ToString().Trim()),"");
                                    if (dtpersonalenvio != null)
                                    {
                                        if (dtpersonalenvio.Rows.Count > 0)
                                        {
                                            for (int ipersonalenvio = 0; ipersonalenvio <= dtpersonalenvio.Rows.Count - 1; ipersonalenvio++)
                                            //USAR PARA PRUEBAS ESTE FOR Y COMENTARIAR EL DE ARRIBA -----> 
                                            //for (int ipersonalenvio = 1; ipersonalenvio <= 1; ipersonalenvio++)
                                            {
                                                Enviomail oEnviomail = new Enviomail();
                                                EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Solicitud_Clave");
                                                Mails oMail = new Mails();
                                                oMail.Server = oeEmail.MailServer;
                                                oMail.Puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"]); //Se Agrega Puerto Ing.CarlosH 30/11/2011
                                                oMail.MCifrado = true;//Se Agrega Cifrado Ing.CarlosH 30/11/2011
                                                oMail.DatosUsuario = new System.Net.NetworkCredential();//Se Agrega Credenciales Ing.CarlosH 30/11/2011
                                                oMail.From = oeEmail.MailFrom;
                                                oMail.To = dtpersonalenvio.Rows[ipersonalenvio]["Person_Email"].ToString().Trim();
                                                //oMail.To = "sgs_mauricio@hotmail.com";                                            
                                                oMail.Subject = "Lucky  : Informes Cargados en Portal Xplora";
                                                string tabla = "<table style=" + '"' + "font-family: verdana; font-size: 11px; color: #0000FF;" + '"';
                                                string tablaclose = "</table><br>";
                                                string informacion = "";
                                                for (int icanales = 0; icanales <= dsenvio.Tables[1].Rows.Count - 1; icanales++)
                                                {
                                                    if (dsenvio.Tables[1].Rows[icanales]["Company_id"].ToString().Trim() == dsenvio.Tables[0].Rows[i]["Company_id"].ToString().Trim())
                                                    {
                                                        // (Obtener informes A enviar dependiendo usuario de carga , servicio canal y reportes por usuario 
                                                        DataTable dtasociacionpersonal = null;
                                                        dtasociacionpersonal = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLANNING_OBTENEREMAILUSERS", 1, Convert.ToInt32(dsenvio.Tables[0].Rows[i][1].ToString().Trim()), Convert.ToInt32(dtpersonalenvio.Rows[ipersonalenvio]["Person_id"].ToString().Trim()), dsenvio.Tables[1].Rows[icanales]["cod_Channel"].ToString().Trim(), Convert.ToInt32(dsenvio.Tables[0].Rows[i][0].ToString().Trim()), fechavalida);
                                                        // Planning.Get_Obtener_Datos_Cliente(1, Convert.ToInt32(dsenvio.Tables[0].Rows[i][1].ToString().Trim()), Convert.ToInt32(dtpersonalenvio.Rows[ipersonalenvio]["Person_id"].ToString().Trim()), dsenvio.Tables[1].Rows[icanales]["cod_Channel"].ToString().Trim(), Convert.ToInt32(dsenvio.Tables[0].Rows[i][0].ToString().Trim()), this.Session["sUser"].ToString().Trim());

                                                        if (dtasociacionpersonal != null)
                                                        {
                                                            if (dtasociacionpersonal.Rows.Count > 0)
                                                            {
                                                                informacion = informacion + "<div style=" + '"' + "font-weight: bold;" + '"' + ">" + dsenvio.Tables[1].Rows[icanales][1].ToString().Trim() + "</div>";
                                                                informacion = informacion + tabla;
                                                                for (int j = 0; j <= dtasociacionpersonal.Rows.Count - 1; j++)
                                                                {
                                                                    if (Convert.ToDateTime(dtasociacionpersonal.Rows[j]["fecha_Registro"].ToString().Trim()) <= Convert.ToDateTime(fechavalida))
                                                                    {
                                                                        informacion = informacion + "<tr><td style=" + '"' + "width: 400px;" + '"' + ">" + dtasociacionpersonal.Rows[j]["informe"].ToString().Trim() + "</td>" + "<td style=" + '"' + "width: 200px;" + '"' + ">" + dtasociacionpersonal.Rows[j]["fecha"].ToString().Trim() + "</td>" + "<td style=" + '"' + "width: 200px;" + '"' + ">" + dtasociacionpersonal.Rows[j]["reporte"].ToString().Trim() + "</td></tr>";
                                                                    }
                                                                }
                                                                informacion = informacion + tablaclose;
                                                            }
                                                        }
                                                    }
                                                }
                                                if (informacion != "")
                                                {
                                                    string imagencorreo = "<img  src=" + '"' + "http://sige.lucky.com.pe/pages/images/LinkXplora.png" + '"' + " width=" + '"' + "59px" + '"' + "height=" + '"' + "15px" + '"' + " GALLERYIMG=" + '"' + "no" + '"' + "/>";
                                                    string[] textArray2 = new string[] { };
                                                    string[] textArray1 = new string[] { "<div style=" + '"' + "font-family: verdana; font-size: 11px; color: #0000FF;" + '"' +">Señor(a) " , dtpersonalenvio.Rows[ipersonalenvio]["Nombres"].ToString().Trim(), "<br>" ,
                                            "Lo invitamos a acceder al sistema de consultas en línea al cual puede acceder a traves del siguiente link: ",
                                            "<a href=" + "http://sige.lucky.com.pe" + ">" + imagencorreo + "</a>" ,"<br><br>" ,
                                            
                                            "Encontrará a su disposición la siguiente información:" , "<br><br>" ,
                                              informacion,                                       "<br><br>" ,                                            
                                            "Para información adicional comuníquese con nosotros. Quedamos atentos a sus comentarios", "<br><br>" ,"<br><br>" ,                                            
                                            "Cordialmente", "<br>", "Administrador Xplora </div>"
                                        };

                                                    oMail.Body = string.Concat(textArray1);
                                                    oMail.BodyFormat = "HTML";
                                                    oMail.send();
                                                    oMail = null;
                                                    oeEmail = null;
                                                    oEnviomail = null;
                                                }
                                            }
                                        }
                                    }

                                }
                                else {

                                    dsenvio = null;
                                
                                }
                            }
                            catch (Exception ex)
                            {
                                Exception exmen = ex;
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["mensaje"] = "Se creo el reporte pero no fue posible enviar aviso al cliente.";
                                Mensajes_Usuario();
                            }
                        }
                    }
                }
                Planning.Get_Actualiza_EstadoEnvioMail(fechavalida, this.Session["sUser"].ToString().Trim(), DateTime.Now);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtuser.Focus();          
                this.Session.RemoveAll();
                //Definir tipo de aplicación
                AppDomain.CurrentDomain.SetData("ApplicationType", "Web");
                //Definir URL de Aplicación Web
                string sPort = "";
                if (Request.Url.Port == 80 || Request.Url.Port == 443) sPort = "";
                else sPort = ":" + Request.Url.Port;
                string sApplicationPath = "";
                if (Request.ApplicationPath == "/")
                    sApplicationPath = "";
                else sApplicationPath = Request.ApplicationPath;
                this.Session["WebRoot"] = Request.Url.Scheme + "://" + Request.Url.Host + sPort + sApplicationPath + "/";
                //this.recordatorio.Visible = false;
                cmbpaisolvido();
            }
        }

        
        #region region Eventos de los Controles
        protected void btningreso_Click(object sender, EventArgs e)
        {
            string sPagina = "~/";

            try
            {

                sUser = txtuser.Text.ToLower();
                sPassw = txtpassw.Text;
                txtuser.Enabled = false;

                this.Session["sUser"] = sUser;
                this.Session["sPassw"] = sPassw;

                DataTable dt = oCoon.ejecutarDataTable("PA_WEB_ACCEDER", sUser, sPassw);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        this.Session["codUsuario"] = Convert.ToInt32(dt.Rows[0]["CODIGO"].ToString().Trim());
                        this.Session["nameuser"] = dt.Rows[0]["NOMBRE"].ToString().Trim();
                        this.Session["Perfilid"] = dt.Rows[0]["COD_PERFIL"].ToString().Trim();
                    }

                }

                if (this.Session["codUsuario"] != null)
                {
                    /*AplicacionWeb oAplicacionWeb = new AplicacionWeb();
                    EAplicacionWeb oeAplicacionWeb = oAplicacionWeb.obtenerAplicacion(sCoutry, smodul);
                    this.Session["oeAplicacionWeb"] = oeAplicacionWeb;
                    this.Session["cod_applucky"] = oeAplicacionWeb.codapplucky;
                    this.Session["abr_app"] = oeAplicacionWeb.abrapp;
                    this.Session["app_url"] = oeAplicacionWeb.appurl;
                    sPagina = oeAplicacionWeb.HomePage;
                    oeAplicacionWeb = null;
                    oAplicacionWeb = null;*/
                    sPagina = "Pages/Modulos/Planning/Menu_Planning.aspx";
                }
            }
            catch (Exception ex){
                Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                string errMessage = "";
                if (ex.Message.Substring(0, 20) == "Error en la Autenticación de Usuario" ||
                    ex.Message.Substring(0, 20) == "La Clave es Errrada o Usuario no Existe")
                {
                    errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                    this.Session["encabemensa"] = "Error de Autenticación";
                    this.Session["mensaje"] = "Usuario y/o Clave Erradas";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensajes_Usuario();
                    return;
                }
                else
                {
                    exs.Country = "SIGE(" + ConfigurationManager.AppSettings["COUNTRY"] + ") - Usuario " + this.Session["sUser"].ToString();
                    string sCountry = ConfigurationManager.AppSettings["COUNTRY"];
                    errMessage = "Error de Autenticacion para " + ' ' + sUser + ' ' + "Clave errada o Usuario Inactivo";
                    this.Session["errMessage"] = errMessage;
                    this.Session["encabemensa"] = "Error de Autenticacion";
                    this.Session["mensaje"] = "Usuario y/o Clave Erradas";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    ProcesoAdmin.Get_Delete_Sesion_User(sUser);
                    Mensajes_Usuario();
                    return;
                }
            }

            this.Response.Redirect("~/" + sPagina, true);
        }

        protected void SetAppSession(EUsuario oeUsuario)
        {
            Response.Cookies["companyid"].Value = oeUsuario.companyid;
            Response.Cookies["companyid"].Expires = DateTime.Now.AddMinutes(5);

            Response.Cookies["fotocomany"].Value = oeUsuario.fotocompany;
            Response.Cookies["fotocomany"].Expires = DateTime.Now.AddMinutes(5);

            Response.Cookies["sNombre"].Value = oeUsuario.companyName;
            Response.Cookies["sNombre"].Expires = DateTime.Now.AddMinutes(5);

            snameuser = oeUsuario.PersonFirtsname + " " + oeUsuario.PersonSurname;
            Response.Cookies["nameuser"].Value = snameuser;
            Response.Cookies["nameuser"].Expires = DateTime.Now.AddMinutes(5);

            Response.Cookies["sUser"].Value = sUser;
            Response.Cookies["sUser"].Expires = DateTime.Now.AddMinutes(5);

            Response.Cookies["Perfilid"].Value = oeUsuario.Perfilid;
            Response.Cookies["Perfilid"].Expires = DateTime.Now.AddMinutes(5);

            Response.Cookies["Personid"].Value = oeUsuario.Personid.ToString();
            Response.Cookies["Personid"].Expires = DateTime.Now.AddMinutes(5);
        }
        protected void BtnCOlv_Click(object sender, ImageClickEventArgs e)
        {
            //UpdateProg1.Visible = false;            
            PRecordatorio.Style.Value = "Display:none;";
        }

        #endregion

        private void cmbpaisolvido()
        {
            DataSet ds = null;
            ds = oCoon.ejecutarDataSet("UP_WEB_LLENACOMBOS", 2);
            cmbpaisolv.DataSource = ds;
            cmbpaisolv.DataValueField = "cod_Country";
            cmbpaisolv.DataTextField = "Name_Country";
            cmbpaisolv.DataBind();
        }

        /// <summary>
        /// Se Crea este Metodo para Autogerar clave de usuario ante Olvido de la misma
        /// Ing. CarlosH 30/11/2011
        /// </summary>

        private void GenerarNuevaClave()
        {
            Enviomail oEnviomail = new Enviomail();
            Enviomail oemail = new Enviomail();
            EEnviomail oeMailuser = oEnviomail.Envio_mails(cmbpaisolv.SelectedValue, "Solicitud_Clave");
            DataSet dsClave = ProcesoAdmin.Get_GenerarPasswordOlvido(txtfrom.Text, txtusaolvi.Text);
            DataTable dtclave = new DataTable();

            try
            {
                dtclave = dsClave.Tables[0];

                if (dtclave.Rows.Count > 0)
                {
                    Mails omailenvio = new Mails();

                    omailenvio.Credenciales = true;
                    omailenvio.Puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"]);
                    omailenvio.Server = oeMailuser.MailServer;
                    omailenvio.MCifrado = true;
                    omailenvio.DatosUsuario = new System.Net.NetworkCredential();
                    omailenvio.From = oeMailuser.MailFrom;
                    // "adminxplora@lucky.com.pe";
                    omailenvio.BCC = "chernandez.col@lucky.com.pe";

                    omailenvio.To = txtfrom.Text;
                    omailenvio.Subject = "Nueva Clave";
                    omailenvio.Body = "Señor Usuario(a): <br> Su nueva Clave es" + ' ' + dtclave.Rows[0]["Clave"].ToString().Trim();
                    omailenvio.BodyFormat = "HTML";
                    omailenvio.send();

                    oEnviomail = null;
                    omailenvio = null;
                    oeMailuser = null;
                    cmbpaisolv.SelectedValue = "0";
                    txtfrom.Text = "";
                    txtusaolvi.Text = "";
                    this.Session["encabemensa"] = "Sr. Usuario ";
                    this.Session["mensaje"] = "Los nuevos datos de autenticación se han enviado a su correo";

                    this.Session["cssclass"] = "MensajesSupConfirm";
                    Mensajes_Usuario();
                }
                else
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["mensaje"] = "Información de solicitud no válida. Verifique o consulte con el Administrador Xplora";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensajes_Usuario2();
                    ModalPopupExtender1.Show();
                }
            }

            catch(Exception ex) {

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);

                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);            
            }
        }
        
        protected void btnenvio_Click(object sender, EventArgs e)
        {
            GenerarNuevaClave();
        }

        protected void btnaceptar_Click(object sender, EventArgs e)
        {

        }

        protected void btnacepmensaje2_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }
    }
}