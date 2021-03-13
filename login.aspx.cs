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
using Lucky.Entity.Common.Application.Response;


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
        // Se inicializa el ilog
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Global));

        /// <summary>
        /// Constructor de la Clase login
        /// </summary>
        public login()
        {
            //Log.Info("Startup application.");
        }

        #region declaracion de variables
        private Usuario oUsuario;
        
        // Variable para almacenar el idUsuario
        private string sUser;
        // Variable para almacenar el password
        private string sPassw;
        //private string sCoutry;
        //private string sDepartament;
        //private string scity;
        //private string smodul;
        //private string sNombre;
        //private string smail;
        //private string idnivel;
        //private string snamenivel;
        //private string snameuser;
        private Facade_Proceso_Planning.Facade_Proceso_Planning Planning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos ProcesoAdmin = 
            new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private Conexion oCoon = new Conexion();

        // Variable para almacenar los mensajes de Error
        private String messages = "";
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
       
        /// <summary>
        /// Funcion para Verificar que solo una persona se haya logueado al Sistema.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool UniqueLogin(string user)
        {
            string myUser = Convert.ToString(Cache[user]);
            // Si el usuario es nulo o cadena vacía...
            if (myUser.ToString() == String.Empty)
            {
                TimeSpan SessTimeOut = new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0);
                HttpContext.Current.Cache.Insert(user, user, null, DateTime.MaxValue, SessTimeOut,
                System.Web.Caching.CacheItemPriority.NotRemovable, null);
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
                                    dtpersonalenvio = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLANNING_OBTENEREMAILUSERS", 
                                        0, 
                                        Convert.ToInt32(dsenvio.Tables[0].Rows[i][1].ToString().Trim()), 
                                        0, 
                                        "none", 
                                        Convert.ToInt32(dsenvio.Tables[0].Rows[i][0].ToString().Trim()), 
                                        "none");

                                    Planning.Get_Obtener_Datos_Cliente(
                                        0, 
                                        Convert.ToInt32(dsenvio.Tables[0].Rows[i][1].ToString().Trim()), 
                                        0, 
                                        "none", 
                                        Convert.ToInt32(dsenvio.Tables[0].Rows[i][0].ToString().Trim()), 
                                        "");

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
                                                        Planning.Get_Obtener_Datos_Cliente(1, Convert.ToInt32(dsenvio.Tables[0].Rows[i][1].ToString().Trim()), Convert.ToInt32(dtpersonalenvio.Rows[ipersonalenvio]["Person_id"].ToString().Trim()), dsenvio.Tables[1].Rows[icanales]["cod_Channel"].ToString().Trim(), Convert.ToInt32(dsenvio.Tables[0].Rows[i][0].ToString().Trim()), this.Session["sUser"].ToString().Trim());

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
                                else
                                {

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

        #region Evento Page Load
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

                if (Request.Url.Port == 80 || 
                    Request.Url.Port == 443) 
                    sPort = "";
                else 
                    sPort = ":" + Request.Url.Port;

                string sApplicationPath = "";

                if (Request.ApplicationPath == "/")
                    sApplicationPath = "";
                else 
                    sApplicationPath = Request.ApplicationPath;

                this.Session["WebRoot"] = Request.Url.Scheme + 
                    "://" + 
                    Request.Url.Host + 
                    sPort +
                    sApplicationPath + 
                    "/";
                //this.recordatorio.Visible = false;
                cmbpaisolvido();
            }
        }

        #endregion

        /// <summary>
        /// Evento Click del Boton de Ingreso "btningreso"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btningreso_Click(object sender, EventArgs e)
        {
            //string sPagina = "~/";
            // Variable para almacenar la fecha
            // DateTime fFecha = Convert.ToDateTime(Session["fFecha"]);
            // Obtener el nombre de Usuario del Formulario
            sUser = txtuser.Text.ToLower();
            // Obtener el password del Usuario del Formulario
            sPassw = txtpassw.Text;
            // Deshabilitar el AspControl TextBox 'txtUser'
            txtuser.Enabled = false;
            //UpdateProg1.Visible = true;
            //PProgresso.Style.Value = "Display:block;";
            // Se crean Sessiones para sUser, sPassw y fFecha
            this.Session["sUser"] = sUser;
            this.Session["sPassw"] = sPassw;
            this.Session["fFecha"] = Convert.ToDateTime(Session["fFecha"]);

            LoginResponse rLoginResponse = new LoginResponse();
            //try
            //{
                rLoginResponse = orqLogin(sUser, 
                                          sPassw,
                                          Request.ServerVariables["REMOTE_ADDR"],
                                          DateTime.Now);

                if (getMessages().Equals(""))
                {
                    //EUsuario oEUsuario = rLoginResponse.OEUsuario;
                    fncSetSessionObjectByEUsuario(rLoginResponse.OEUsuario);
                    if (getMessages().Equals(""))
                    {
                        Response.Redirect(rLoginResponse.UrlPage);
                    }
                    //else {
                    //    messages = getMessages();
                    //}
                }
                //else {
                //    messages = getMessages();
                //}
            //}
            //catch (Exception ex)
            //{
            //    messages = "Error: " + ex.Message.ToString();
            //}



            /*
            // Variable spasEncriptado, encriptar el password digitado por WebSite.
            string spasEncriptado = fncEncriptar(sPassw);
                //Lucky.CFG.Util.Encriptacion.Codificar( 
                //sPassw,
                //Key);

            if (getMessages().Equals("")) {
                //DataTable dtc = fncGetPassword(sUser);
                string sclvr = fncGetPassword(sUser).Rows[0]["User_Password"].ToString();
                    
                if (getMessages().Equals("")) {
                    if (sclvr != spasEncriptado && 
                        sclvr.Length < 15){
                        //if (sclvr.Length < 15){
                        //Valida si la Clave no esta Enciptada y la encripta Ing. CarlosH 30/11/2011
                        //spasEncriptado = Lucky.CFG.Util.Encriptacion.Codificar(sclvr, Key);
                        //spasEncriptado = fncEncriptar(sclvr);
                        //Actualiza la Clave encriptada Ing. CarlosH 30/11/2011
                        fncUpdatePasswordEncriptado(fncEncriptar(sclvr), sUser);
                        //oCoon.ejecutarDataReader("UP_WEBXPLORA_UPDATEPSWENCRIPTA", spasEncriptado, sUser);
                        //}
                        if (getMessages().Equals("")){
                            EUsuario oeUsuario = fncSetSessionObjectPerson(sUser, spasEncriptado);
                            if (getMessages().Equals("")){
                                fncInsertAuditoría();
                                if (getMessages().Equals("") && oeUsuario != null){
                                    SetAppSession(oeUsuario);
                                    if (getMessages().Equals("")){
                                        //sPagina = fncGetDataAplicationWeb();
                                        fncRedireccionar(fncGetDataAplicationWeb());
                                    }
                                }
                            }
                            //if ()
                            //{
                                //UsuarioAcceso oUsuarioAcceso = new UsuarioAcceso();
                                //EUsuarioAcceso oeUsuarioAcceso = new EUsuarioAcceso();
                                ///oeUsuarioAcceso = oUsuarioAcceso.obtenerAleatorioxUsuario(sUser, sPassw);
                                //UniqueLogin(sUser);
                            //}
                            //PProgresso.Style.Value = "Display:none";
                            //PProgresso_ModalPopupExtender.Hide();
                        }
                    }    
                }
            }
            */
            // Si existen errores se mostrará un PopUp
            if (!getMessages().Equals("")) {
                
                MensajeSeguimiento.CssClass = "MensajesSupervisor";
                lblencabezadoSeguimiento.Text = "Error";
                lblmensajegeneralSeguimiento.Text = "Error: " + getMessages(); //oAplicacionWeb.getMessage();
                ProcesoAdmin.Get_Delete_Sesion_User(sUser);
                MPMensajeSeguimiento.Show();
            }

            //dtc = oCoon.ejecutarDataTable("UP_WEBXPLORAGEN_PASSUSER", sUser);
               
            // Obtener Password Encriptado de Base de Datos por idUsuario
            //DataTable dtc = oCoon.ejecutarDataTable("UP_WEBXPLORAGEN_PASSUSER", sUser);
        }

        /// <summary>
        /// Almacenar en Cookies atributos que se utilizarán a lo largo de la WebSite. 
        /// </summary>
        /// <param name="oeUsuario"></param>
        protected void SetAppSession(EUsuario oeUsuario)
        {
            try
            {
                Response.Cookies["companyid"].Value = oeUsuario.companyid;
                Response.Cookies["companyid"].Expires = DateTime.Now.AddMinutes(50);

                Response.Cookies["fotocomany"].Value = oeUsuario.fotocompany;
                Response.Cookies["fotocomany"].Expires = DateTime.Now.AddMinutes(50);

                Response.Cookies["sNombre"].Value = oeUsuario.companyName;
                Response.Cookies["sNombre"].Expires = DateTime.Now.AddMinutes(50);

                //snameuser = oeUsuario.PersonFirtsname + " " + oeUsuario.PersonSurname;
                Response.Cookies["nameuser"].Value = oeUsuario.PersonFirtsname + " " + oeUsuario.PersonSurname;
                Response.Cookies["nameuser"].Expires = DateTime.Now.AddMinutes(50);

                Response.Cookies["sUser"].Value = sUser;
                Response.Cookies["sUser"].Expires = DateTime.Now.AddMinutes(50);

                Response.Cookies["Perfilid"].Value = oeUsuario.Perfilid;
                Response.Cookies["Perfilid"].Expires = DateTime.Now.AddMinutes(50);

                Response.Cookies["Personid"].Value = oeUsuario.Personid.ToString();
                Response.Cookies["Personid"].Expires = DateTime.Now.AddMinutes(50);
            }
            catch (Exception ex) {
                messages = "Error: " + ex.Message.ToString();
            }
        }

        #region Funciones Generales
        private void cmbpaisolvido()
        {
            try{
                DataSet ds = null;
                ds = oCoon.ejecutarDataSet("UP_WEB_LLENACOMBOS", 2);
                cmbpaisolv.DataSource = ds;
                cmbpaisolv.DataValueField = "cod_Country";
                cmbpaisolv.DataTextField = "Name_Country";
                cmbpaisolv.DataBind();
            }
            catch (Exception ex){
                MensajeSeguimiento.CssClass = "MensajesSupervisor";
                lblencabezadoSeguimiento.Text = "Error";
                lblmensajegeneralSeguimiento.Text = "Error: " + ex.ToString().Substring(0, 100) + " ...";
                MPMensajeSeguimiento.Show();
            }
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

            catch (Exception ex)
            {

                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["mensaje"] = "Error: " + ex.ToString().Substring(0, 100) + "...";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensajes_Usuario2();
                ModalPopupExtender1.Show();

                //Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                //mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
            }
        }
        #endregion

        #region Eventos Click

        protected void BtnCOlv_Click(object sender, ImageClickEventArgs e)
        {
            //UpdateProg1.Visible = false;            
            PRecordatorio.Style.Value = "Display:none;";
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
        #endregion

        #region Funciones Auxiliares

        /// <summary>
        /// Función para Encriptar Passwords
        /// </summary>
        /// <param name="password"></param>
        private String fncEncriptar(String password) {
            String result = "";
            try{
                result = Lucky.CFG.Util.Encriptacion.Codificar(password, ConfigurationManager.AppSettings["TamperProofKey"]);
            }
            catch (Exception ex) {
                messages = "Ocurrio un Error: " + ex.Message.ToString();
            }
            return result;
        }

        /// <summary>
        /// Función para obtener el Password de la Base de Datos,
        /// por idUser.
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        private DataTable fncGetPassword(String idUser) {
            DataTable dt = new DataTable();
            try {
                dt = oCoon.ejecutarDataTable("UP_WEBXPLORAGEN_PASSUSER", idUser);
            }
            catch (Exception ex) {
                messages = "Ocurrio un Error: " + ex.Message.ToString();
            }
            return dt;
        }
        
        /// <summary>
        /// Si devuelve vacio (""), no se presentaron errores,
        /// caso contrario muestra el error ocurrido.
        /// </summary>
        /// <returns></returns>
        private String getMessages() {
            return messages;
        }
        
        /// <summary>
        /// Función para Actualizar la Encriptación del Password en Base de Datos
        /// En caso el password no se haya almacenado Encriptado.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="idUser"></param>
        private void fncUpdatePasswordEncriptado(String password, String idUser) {
            try {
                oCoon.ejecutarDataReader("UP_WEBXPLORA_UPDATEPSWENCRIPTA", password, idUser);
            }
            catch (Exception ex) {
                messages = "Error: " + ex.Message.ToString();
            }
        }

        /// <summary>
        /// Obtener información del usuario y 
        /// Establecer en Objetos Session los Atributos del Objeto 'Person'.
        /// </summary>
        /// <param name="passEncriptado"></param>
        private EUsuario fncSetSessionObjectPerson(String idUser, String passEncriptado){

            // Declaramos la variable oeUsuario
            EUsuario oeUsuario = new EUsuario();

            try{
                oeUsuario = oUsuario.obtener(idUser, passEncriptado);

                if (oUsuario.getMessages().Equals("") && oeUsuario != null)
                {
                    //sCoutry = oeUsuario.codCountry;
                    //sDepartament = oeUsuario.coddepartam;
                    //scity = oeUsuario.codcity;
                    //smail = oeUsuario.PersonEmail;
                    this.Session["smail"] = oeUsuario.PersonEmail;
                    this.Session["companyid"] = oeUsuario.companyid;
                    this.Session["fotocomany"] = oeUsuario.fotocompany;
                    //sNombre = oeUsuario.companyName;
                    this.Session["sNombre"] = oeUsuario.companyName;
                    //snameuser = oeUsuario.PersonFirtsname + " " + oeUsuario.PersonSurname;
                    this.Session["nameuser"] = oeUsuario.PersonFirtsname + " " + oeUsuario.PersonSurname;
                    //smodul = oeUsuario.Moduloid;
                    this.Session["scountry"] = oeUsuario.codCountry;
                    this.Session["scity"] = oeUsuario.codcity;
                    this.Session["personid"] = oeUsuario.Personid;
                    this.Session["smodul"] = oeUsuario.Moduloid;
                    //idnivel = oeUsuario.idlevel;
                    this.Session["idnivel"] = oeUsuario.idlevel;
                    //snamenivel = oeUsuario.leveldescription;
                    this.Session["namenivel"] = oeUsuario.leveldescription;
                    this.Session["Perfilid"] = oeUsuario.Perfilid;
                    this.Session["nameperfil"] = oeUsuario.NamePerfil;
                    this.Session["Canal"] = "0";
                    this.Session["Nivel"] = 0;

                }
                else {
                    messages = oUsuario.getMessages();
                }               
            }
            catch (Exception ex) {
                messages = "Error: " + ex.Message.ToString();
            }
            return oeUsuario;
        }

        /// <summary>
        /// Insertar registros de Auditoría en la Base de Datos.
        /// </summary>
        private void fncInsertAuditoría() {
            Sesion_Users su = new Sesion_Users();
            //string HostName =System.Net.Dns.GetHostByAddress(Request.UserHostAddress).HostName;
            //string RemoteHost = HttpContext.Current.Request.UserHostAddress;
            //string RemoteHost = HttpContext.Current.Request.ServerVariables["HTTP_USER_ADDR"];
            string RemoteHost = Request.ServerVariables["REMOTE_ADDR"];

            try{
                su.Registrar_Auditoria(
                    this.Session["sUser"].ToString(),
                    Convert.ToInt32(this.Session["companyid"]),
                    RemoteHost,
                    DateTime.Now);
            }
            catch (Exception ex) {
                messages = "Error: " + ex.Message.ToString();
            }
            //ObtenerDatosEnvioMail();
        }

        /// <summary>
        /// Retorna la Pagina a la que debe ser redireccionado el usuario despues de loguearse.
        /// </summary>
        private String fncGetDataAplicationWeb() {
            
            String pagina = "";

            EAplicacionWeb oeAplicacionWeb = new EAplicacionWeb();
            AplicacionWeb oAplicacionWeb = new AplicacionWeb();

            try
            {
                oeAplicacionWeb = oAplicacionWeb.obtenerAplicacion(this.Session["scountry"].ToString(), this.Session["smodul"].ToString());
                // Verifica que no haya Errores
                if (oAplicacionWeb.getMessage().Equals(""))
                {
                    this.Session["oeAplicacionWeb"] = oeAplicacionWeb;
                    this.Session["cod_applucky"] = oeAplicacionWeb.codapplucky;
                    this.Session["abr_app"] = oeAplicacionWeb.abrapp;
                    this.Session["app_url"] = oeAplicacionWeb.appurl;

                    pagina = oeAplicacionWeb.HomePage;

                    //oeUsuarioAcceso = null;
                    //oeAplicacionWeb = null;
                    //oAplicacionWeb = null;
                }
                else {
                    messages = "Error: " + oAplicacionWeb.getMessage();
                }
            }
            catch (Exception ex) {
                messages = "Error: " + ex.Message.ToString();
            }

            return pagina;
        }
        
        /// <summary>
        /// Redireccionar a la Pagina Web Correspondiente Según Perfil de Usuario, si 
        /// se redirecciona a Xplora Maps, Actualizar Password o la Página Normal.
        /// </summary>
        /// <param name="paginaUrl"></param>
        private void fncRedireccionar(String paginaUrl) {

            EntrySeccion oSeccion = new EntrySeccion();
            EEntrySeccion oeSeccion = new EEntrySeccion();
   
            try{
                oeSeccion = oSeccion.PrimerAcceso(sUser);
                if (oSeccion.getMessages().Equals("")){
                    if (oeSeccion.seccionname == "1"){
                        Response.Redirect("Cambio_pswd.aspx", true);
                    }
                    //if (Request.Cookies["SIGE_URLRedirect"] == null) 
                    //    this.Response.Redirect("~/" + sPagina, true);
                    else{
                        //HttpCookie hcURLRedirect = Request.Cookies["SIGE_URLRedirect"];
                        // Xplora Maps Alicorp
                        if (this.Session["companyid"].ToString() == "1562"
                            && (this.Session["Perfilid"].ToString() == "6001"
                            || this.Session["Perfilid"].ToString() == "4512")){
                            Response.Redirect("http://sige.lucky.com.pe:8081/");
                        } // Xplora Maps Colgate
                        else if (this.Session["companyid"].ToString() == "1561"
                                 && this.Session["Perfilid"].ToString() == "4512"){
                            Response.Redirect("http://sige.lucky.com.pe:8282");
                        } // WebSite Operativo y Administrativo
                        else{
                            //this.Response.Redirect("~/" + sPagina, true);
                            this.Response.Redirect("~/" + paginaUrl, true);
                        }
                    }
                }
                else {
                    messages = "Error: " + oSeccion.getMessages();
                }
            }catch (Exception ex) {
                messages = "Error: " + ex.Message.ToString();
            }
        }
        
        #endregion

        /// <summary>
        /// Orquestador Login, Response para la función Login.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <param name="idMachine"></param>
        /// <param name="dateBy"></param>
        private LoginResponse orqLogin(String userName, 
                                       String userPassword, 
                                       String idMachine, 
                                       DateTime dateBy) {

            Usuario blUsuario = new Usuario();
            LoginResponse rLoginResponse = new LoginResponse();
            
            try {
                rLoginResponse = blUsuario.fncLogin(userName, 
                                                    userPassword, 
                                                    idMachine, 
                                                    dateBy);
                
                if (!blUsuario.getMessages().Equals("")) {
                    messages = blUsuario.getMessages();
                }
            }
            catch (Exception ex) {
                messages = ex.Message.ToString();
            }
            
            return rLoginResponse;
        }

        /// <summary>
        /// Setear una Session con los valores del Object Person
        /// </summary>
        /// <param name="oeUsuario"></param>
        private void fncSetSessionObjectByEUsuario(EUsuario oeUsuario)
        {
            try
            {
                this.Session["smail"] = oeUsuario.PersonEmail;
                this.Session["companyid"] = oeUsuario.companyid;
                this.Session["fotocomany"] = oeUsuario.fotocompany;
                this.Session["sNombre"] = oeUsuario.companyName;
                this.Session["nameuser"] = oeUsuario.PersonFirtsname + " " + oeUsuario.PersonSurname;
                this.Session["scountry"] = oeUsuario.codCountry;
                this.Session["scity"] = oeUsuario.codcity;
                this.Session["personid"] = oeUsuario.Personid;
                this.Session["smodul"] = oeUsuario.Moduloid;
                this.Session["idnivel"] = oeUsuario.idlevel;
                this.Session["namenivel"] = oeUsuario.leveldescription;
                this.Session["Perfilid"] = oeUsuario.Perfilid;
                this.Session["nameperfil"] = oeUsuario.NamePerfil;
                this.Session["Canal"] = "0";
                this.Session["Nivel"] = 0;
            }
            catch (Exception ex)
            {
                messages = "Ocurrio un Error: " + ex.Message.ToString();
            }
        }
    }
}