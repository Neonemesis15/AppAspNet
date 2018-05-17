using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using Lucky.Data;
using Lucky.Business;
using Lucky.Data.Common.Application;
using Lucky.Data.Common.Security;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Application.Security;
using Lucky.Entity.Common.Security;
using Lucky.Business.Common.Application;
using Lucky.Business.Common.Security;
using Lucky.CFG.Messenger;
using Lucky.CFG.Util;
using SIGE.Facade_Procesos_Administrativos;
using System.Net;
using System.Net.NetworkInformation;

namespace SIGE
{
    /// <summary>
    /// Pagina Inicio SIGE Contiene el formulario de Logueo.
    /// Creado por: Ing. Carlos Alberto Hernandez R
    /// Fecha creacion: 22/04/2009
    /// Requerimiento:
    /// </summary>
    public partial class loginV2 : System.Web.UI.Page
    {
        #region declaracion de variables
        private Usuario oUsuario;
        EntrySeccion oSeccion = new EntrySeccion();

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
        Facade_Proceso_Planning.Facade_Proceso_Planning Planning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos ProcesoAdmin = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Conexion oCoon = new Conexion();





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
                                            oMail.From = oeEmail.MailFrom;
                                            oMail.To = dtpersonalenvio.Rows[ipersonalenvio]["Person_Email"].ToString().Trim();
                                            //oMail.To = "sgs_mauricio@hotmail.com";                                            
                                            oMail.Subject = "Lucky SAC : Informes Cargados en Portal Xplora";
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
                            catch (Exception ex)
                            {
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
                //string sObjeto = "";
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

        #endregion



        #region region Eventos de los Controles
        protected void btningreso_Click(object sender, EventArgs e)
        {
            string sPagina = "~/";

            try
            {
                UpdateProg1.Visible = true;
                PProgresso.Style.Value = "Display:block;";

                DateTime fFecha = Convert.ToDateTime(Session["fFecha"]);


                sUser = txtuser.Text.ToLower();

                sPassw = txtpassw.Text;
                //Se deshabilita la verificacion de sesion
                //DataTable dtsesionuser = null;
                //dtsesionuser = ProcesoAdmin.Get_User_Sesion_Conteo(sUser);

                //if (dtsesionuser.Rows.Count > 0)
                //{
                //    this.Session["encabemensa"] = "Error de Autenticacion";
                //    this.Session["mensaje"] = "El Usuario" + " " + sUser + " " + "ya tiene" + "<br>" + "una sesion SIGE Activa";
                //    this.Session["cssclass"] = "MensajesSupervisor";
                //    Mensajes_Usuario();
                //    return;


                //}


                //ProcesoAdmin.Get_Obtener_User_Sesion(sUser);

                //Guardar las variables de sesión

                this.Session["sUser"] = sUser;
                this.Session["sPassw"] = sPassw;

                this.Session["fFecha"] = fFecha;
                //Session["Service"] = 254;
                //Session["Canal"]="1000";


                EUsuario oeUsuario = oUsuario.obtener(sUser, sPassw);


                sCoutry = oeUsuario.codCountry;
                sDepartament = oeUsuario.coddepartam;
                scity = oeUsuario.codcity;
                smail = oeUsuario.PersonEmail;
                this.Session["smail"] = smail;
                this.Session["companyid"] = oeUsuario.companyid;
                this.Session["fotocomany"] = oeUsuario.fotocompany;

                sNombre = oeUsuario.companyName;



                this.Session["sNombre"] = sNombre;

                snameuser = oeUsuario.PersonFirtsname + " " + oeUsuario.PersonSurname;
                this.Session["nameuser"] = snameuser;

                smodul = oeUsuario.Moduloid;
                this.Session["scountry"] = sCoutry;
                this.Session["scity"] = scity;
                this.Session["personid"] = oeUsuario.Personid;
                this.Session["smodul"] = smodul;
                idnivel = oeUsuario.idlevel;
                this.Session["idnivel"] = idnivel;

                snamenivel = oeUsuario.leveldescription;
                this.Session["namenivel"] = snamenivel;
                this.Session["Perfilid"] = oeUsuario.Perfilid;
                this.Session["nameperfil"] = oeUsuario.NamePerfil;
                this.Session["Service"] = 254;
                this.Session["Canal"] = "0";
                this.Session["Nivel"] = 0;

                Sesion_Users su = new Sesion_Users();
                //string HostName =System.Net.Dns.GetHostByAddress(Request.UserHostAddress).HostName;
                //string RemoteHost = HttpContext.Current.Request.UserHostAddress;
                //string RemoteHost = HttpContext.Current.Request.ServerVariables["HTTP_USER_ADDR"];


                string RemoteHost = Request.ServerVariables["REMOTE_ADDR"];



                su.Registrar_Auditoria(this.Session["sUser"].ToString(), Convert.ToInt32(this.Session["companyid"]), RemoteHost, DateTime.Now);


                ObtenerDatosEnvioMail();

                if (oeUsuario != null)
                {



                    UsuarioAcceso oUsuarioAcceso = new UsuarioAcceso();
                    EUsuarioAcceso oeUsuarioAcceso = new EUsuarioAcceso();
                    oeUsuarioAcceso = oUsuarioAcceso.obtenerAleatorioxUsuario(sUser, sPassw);
                    UniqueLogin(sUser);
                    AplicacionWeb oAplicacionWeb = new AplicacionWeb();
                    EAplicacionWeb oeAplicacionWeb = oAplicacionWeb.obtenerAplicacion(sCoutry, smodul);
                    this.Session["oeAplicacionWeb"] = oeAplicacionWeb;
                    this.Session["cod_applucky"] = oeAplicacionWeb.codapplucky;
                    this.Session["abr_app"] = oeAplicacionWeb.abrapp;
                    this.Session["app_url"] = oeAplicacionWeb.appurl;
                    sPagina = oeAplicacionWeb.HomePage;
                    oeUsuarioAcceso = null;
                    oeAplicacionWeb = null;
                    oAplicacionWeb = null;
                }
                //PProgresso.Style.Value = "Display:none";
                //PProgresso_ModalPopupExtender.Hide();


            }





            catch (Exception ex)
            {
                Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                string errMessage = "";
                if (ex.Message.Substring(0, 20) == "Error en la Autenticación de Usuario" ||
                    ex.Message.Substring(0, 20) == "La Clave es Errrada o Usuario no Existe")
                {
                    //errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);


                    //this.Response.Redirect("~/err_mensaje.aspx?msg=" + errMessage, true);
                    this.Session["encabemensa"] = "Error de Autenticación";
                    this.Session["mensaje"] = "Usuario y/o Clave Erradas";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensajes_Usuario();
                    return;


                }
                else
                {
                    //Enviar error a fin de evitar que este se pierda con el redirect de página.

                    exs.Country = "SIGE(" + ConfigurationManager.AppSettings["COUNTRY"] + ") - Usuario " + this.Session["sUser"].ToString();
                    string sCountry = ConfigurationManager.AppSettings["COUNTRY"];
                    //exs.errorWebsite(sCountry);
                    //errMessage += new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                    errMessage = "Error de Autenticacion para " + ' ' + sUser + ' ' + "Clave errada o Usuario Inactivo";
                    this.Session["errMessage"] = errMessage;
                    //this.Response.Redirect("~/err_mensaje.aspx?msg=" + errMessage, true);
                    this.Session["encabemensa"] = "Error de Autenticacion";
                    this.Session["mensaje"] = "Usuario y/o Clave Erradas";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    ProcesoAdmin.Get_Delete_Sesion_User(sUser);
                    Mensajes_Usuario();
                    return;
                }
            }

            EEntrySeccion oeSeccion = oSeccion.PrimerAcceso(sUser);
            if (oeSeccion.seccionname == "1")
            {


                Response.Redirect("Cambio_pswd.aspx", true);

            }
            //if (Request.Cookies["SIGE_URLRedirect"] == null)
            //{


            //    this.Response.Redirect("~/" + sPagina, true);



            //}
            else
            {

                //HttpCookie hcURLRedirect = Request.Cookies["SIGE_URLRedirect"];
                this.Response.Redirect("~/" + sPagina, true);
            }



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

        protected void btnenvio_Click(object sender, EventArgs e)
        {
            if (cmbpaisolv.Text != "0" && txtusaolvi.Text != "" && txtfrom.Text != "")
            {
                Enviomail oEnviomail = new Enviomail();
                EEnviomail oeEmail = oEnviomail.Envio_mails(cmbpaisolv.SelectedValue, "Solicitud_Clave");
                Mails oMail = new Mails();
                oMail.Server = oeEmail.MailServer;
                oMail.From = txtfrom.Text;

                oMail.To = oeEmail.MailTo;
                oMail.Subject = oeEmail.Subject + "'   '" + txtusaolvi.Text;
                string[] textArray1 = new string[] { "Usuario:" + ' ' + txtusaolvi.Text };

                oMail.Body = oeEmail.Body + string.Concat(textArray1);
                oMail.CC = "mortiz.col@lucky.com.pe";

                oMail.BodyFormat = "HTML";


                //Envio Automativo de Clave Generada
                Enviomail oEnvioUserMail = new Enviomail();
                EEnviomail oeMailuser = oEnviomail.Envio_mails(cmbpaisolv.SelectedValue, "Solicitud_Clave");
                DataSet dsClave = ProcesoAdmin.Get_GenerarPasswordOlvido(txtfrom.Text, txtusaolvi.Text);
                DataTable dtclave = new DataTable();
                dtclave = dsClave.Tables[0];
                if (dtclave.Rows.Count > 0)
                {
                    // envia al administrador alerta.
                    oMail.send();

                    // envia informacion nueva al usuario solicitante
                    Mails omailenvio = new Mails();
                    omailenvio.Server = oeMailuser.MailServer;
                    omailenvio.From = "AdminXplora@lucky.com.pe";
                    omailenvio.CC = "chernandez.col@lucky.com.pe";
                    omailenvio.BCC = "mjimenez.col@lucky.com.pe";
                    omailenvio.To = txtfrom.Text;
                    omailenvio.Subject = "Nueva Clave";
                    omailenvio.Body = "Señor Usuario(a): <br> Su nueva Clave es" + ' ' + dtclave.Rows[0]["Clave"].ToString().Trim();
                    omailenvio.BodyFormat = "HTML";
                    omailenvio.send();
                    oMail = null;
                    oeEmail = null;
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
                //lblsend.Text = "Se ha enviado su Nueva Clave al Correo registrado en el Sistema";
                //this.recordatorio.Visible = false;

            }
            else
            {
                ModalPopupExtender1.Show();
            }

        }

        protected void btnaceptar_Click(object sender, EventArgs e)
        {

        }

        protected void btnacepmensaje2_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Show();
        }

        protected void ImgEnviarMail_Click(object sender, EventArgs e)
        {
            try
            {
                Enviomail oEnviomail = new Enviomail();
                EEnviomail oeEmail = oEnviomail.Envio_mails("589", "Solicitud_Operativo");
                Mails oMail = new Mails();
                oMail.Server = "mail.lucky.com.pe";
                //oeEmail.MailServer; 



                    oMail.From = TxtSolicitante.Text;
                    oMail.To = "sgs_mauricio@hotmail.com";
                    oMail.Subject = TxtMotivo.Text;
                    oMail.Body = TxtSolicitante.Text + " " + TxtMensaje.Text;
                    oMail.CC = "sgs_mauricio@hotmail.com";
                    oMail.BodyFormat = "HTML";
                    oMail.send();
                    oMail = null;
                    // oeEmail = null;
                    oEnviomail = null;
                    TxtSolicitante.Text = "";
                    TxtMotivo.Text = "";
                    TxtMensaje.Text = "";
                    
                
            }
            catch (Exception ex)
            {
                //Alertas.CssClass = "MensajesSupervisor";
                //LblAlert.Text = "Envio Solicitudes";
                //LblFaltantes.Text = "Sr. Usuario, se presentó un error inesperado al momento de enviar el correo. Por favor intentelo nuevamente o consulte al Administrador de la aplicación";
                //PopupMensajes();
                return;
            }
        }
    }
}


