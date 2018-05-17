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
using SIGE.Facade_Procesos_Administrativos;
using Lucky.CFG.Messenger;
using Lucky.CFG.Util;

namespace SIGE.Pages
{
    /// <summary>
    /// Pagina:Cambio de Clave
    /// Creada Por:Ing.Carlos Alberto Hernandez
    /// Fecha de Creacion:05/05/2009
    /// Descripcion:Permite hacer el cambio de clave cuando los eventos del negociolo requieran
    /// Reuqerimiento No:
    /// </summary>

    public partial class Cambio_pswd : System.Web.UI.Page
    {
        #region Inicializacion de WS
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos FAdmin = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                TxtSolicitante.Enabled = false;
                TxtSolicitante.Text = this.Session["sUser"].ToString().Trim();
                TxtPassNuevo.Text = "";
                TxtPassNew.Text = "";                
            }
        }

        #region Funciones Comunes
        public void limpiarcontroles()
        {
            TxtSolicitante.Text = "";
            TxtPassNuevo.Text = "";
            TxtPassNew.Text = "";
        }
        #endregion       

        protected void btnEnviarNewPass_Click(object sender, EventArgs e)
        {
            if ((TxtPassNuevo.Text == TxtPassNew.Text) && (TxtPassNuevo.Text != "" && TxtPassNew.Text!= ""))
            {
                Usuario ocambiopsw = new Usuario();
                string Key;
                Key = ConfigurationManager.AppSettings["TamperProofKey"];

                EUsuario oecambiopasw = ocambiopsw.cambiarContrasena(TxtSolicitante.Text,Encriptacion.Codificar(TxtPassNew.Text,Key), Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));
                EntrySeccion oSeccion = new EntrySeccion();
                EEntrySeccion oeseccion = oSeccion.Register_PrimerSeccion(Convert.ToString(this.Session["sUser"]), Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now), Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));

                limpiarcontroles();
                FAdmin.Get_Delete_Sesion_User(this.Session["sUser"].ToString().Trim());
                //realiza el envio de Email confirmatorio del cambio de clave
                Enviomail oEnviomail = new Enviomail();
                EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Solicitud_Clave");
                Mails oMail = new Mails();
                oMail.Credenciales = true;
                oMail.Puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"]);  //Se agrega Puerto Ing. CarlosH 30/11/2011
                oMail.Server = oeEmail.MailServer;
                oMail.MCifrado = true; //Se agrega Cifrado Ing. CarlosH 30/11/2011
                oMail.DatosUsuario = new System.Net.NetworkCredential(); //Se agrega Credenciales Ing. CarlosH 30/11/2011
                oMail.From = oeEmail.MailFrom;
                 oMail.To = this.Session["smail"].ToString().Trim();
                 oMail.BCC = "chernandez.col@lucky.com.pe";
               
                oMail.Subject = "Acuse de Cambio de Clave" + "'   '" + "Usuario" + "'  '" + this.Session["sUser"].ToString().Trim();
                string[] textArray1 = new string[] { "Señor usuario:" + ' ' + "Su Clave se ha modificado Correctamente", "<br><br>", "Atentamente", "<br>", "Administrador Xplora", };

                oMail.Body = string.Concat(textArray1);


                oMail.BodyFormat = "HTML";
                oMail.send();

                oMail = null;
                oeEmail = null;
                oEnviomail = null;
                this.Session["mensaje"] = "Su clave ha sido cambiada correctamente ver confirmación en su correo";
                this.Session["cssclass"] = "MensajesSupConfirm";
                PMensajeClave.CssClass = this.Session["cssclass"].ToString();
                lblpasw.Text = this.Session["mensaje"].ToString();
                ModalPopupExtender.Show();
            }
            else
            {

            }

        }

        protected void btningresonew_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

     

    

     

     
          


            
          
          
        

        
       

        

        
    }
}
