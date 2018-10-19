using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Business.Common.Application;
using Lucky.CFG.Messenger;
using System.Configuration;
using Lucky.Data;
using Lucky.Entity.Common.Application;


namespace SIGE.Pages.Modulos.Planning
{
    public partial class Menu_Planning : System.Web.UI.Page
    {

        Conexion oCoon = new Conexion();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos PAdmin = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();

        //Ventanas de mensaje de usuario
        private void PopupMensajes()
        {
            ModalPopupAlertas.Show();
            ModalPanelSolicitud.Show();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string sUser = this.Session["sUser"].ToString();
                    string sPassw = this.Session["sPassw"].ToString();
                    string sNameUser = this.Session["nameuser"].ToString();

                    if (sUser != null && sPassw != null)
                    {
                        lblUsuario.Text = sNameUser;
                        TxtSolicitante.Text = this.Session["smail"].ToString();
                        TxtEmail.Text = "AdminXplora@lucky.com.pe";
                        DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_MODULOSALTERNOS", this.Session["idnivel"].ToString().Trim());

                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                SelModulo.Visible = true;
                                cmbselModulos.Visible = true;
                                SelCliente.Visible = false;
                                //cmbcliente.Visible = false;
                                GO.Visible = true;
                                cmbcliente.Enabled = true;
                                cmbselModulos.DataSource = dt;
                                cmbselModulos.DataTextField = "Modulo_Name";
                                cmbselModulos.DataValueField = "Modulo_id";
                                cmbselModulos.DataBind();
                                cmbselModulos.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                                cmbselModulos.Items.Remove(cmbselModulos.Items.FindByValue(this.Session["smodul"].ToString().Trim()));
                            }
                            else
                            {
                                SelModulo.Visible = false;
                                cmbselModulos.Visible = false;
                                SelCliente.Visible = false;
                                cmbcliente.Visible = false;
                                GO.Visible = false;
                                cmbselModulos.Items.Clear();
                                cmbcliente.Items.Clear();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Session.Abandon();
                    string errmensajeseccion = Convert.ToString(ex);
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
        }

        protected void btncontruplanning_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Modulos/Planning/Mod_Planning.aspx", true);
            //para pruebas en lima
            //Response.Redirect("~/Pages/Modulos/Planning/ini_planning.aspx", true);         
        }

        protected void ImgCloseSession_Click(object sender, ImageClickEventArgs e)
        {
            PAdmin.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
            this.Session.Abandon();
            Response.Redirect("~/login.aspx");
        }

        protected void btnasignaporcanal_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Modulos/Planning/AsignacionesxCanal.aspx", true);
        }

        // seguimiento de la construccion de los planning . Ing mauricio Ortiz
        protected void BtnConsultas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Modulos/Planning/Seguimiento_Planning.aspx", true);
        }

        protected void ImgEnviarMail_Click(object sender, ImageClickEventArgs e)
        {
            if (TxtMotivo.Text == "" || TxtMensaje.Text == "")
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Envio Solicitudes";
                LblFaltantes.Text = "Sr. Usuario, es necesario que ingrese información en el asunto y en el mensaje ";
                PopupMensajes();
                return;
            }
            try
            {
                Enviomail oEnviomail = new Enviomail();
                EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Solicitud_Planning");
                Mails oMail = new Mails();
                oMail.Server = ConfigurationManager.AppSettings["ServerMail"];
                oMail.Puerto = 587;
                oMail.MCifrado = true;
                oMail.DatosUsuario = new System.Net.NetworkCredential();
                oMail.From = TxtSolicitante.Text;
                oMail.To = ConfigurationManager.AppSettings["User"];
                oMail.Subject = TxtMotivo.Text;
                oMail.Body = TxtMensaje.Text;
                oMail.CC = "adminxplora@lucky.com.pe";
                oMail.BodyFormat = "HTML";
                oMail.send();
                oMail = null;
                oEnviomail = null;
                TxtMotivo.Text = "";
                TxtMensaje.Text = "";
                Alertas.CssClass = "MensajesSupConfirm";
                LblAlert.Text = "Envio Solicitudes";
                LblFaltantes.Text = "Sr. Usuario, el mensaje fue enviado correctamente";
                PopupMensajes();
            }
            catch (Exception ex)
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Envio Solicitudes";
                LblFaltantes.Text = "Sr. Usuario, se presentó un error inesperado al momento de enviar el correo. Por favor inténtelo nuevamente o consulte al Administrador de la aplicación";
                PopupMensajes();
                return;
            }
        }

        protected void BtnCloseSolicitudes_Click(object sender, ImageClickEventArgs e)
        {
            TxtMotivo.Text = "";
            TxtMensaje.Text = "";
        }

        protected void BtnInforComun_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Modulos/Planning/Mod_Planning.aspx", true);
        }

        protected void BtnNuevasCamp_Click(object sender, EventArgs e)
        {
            this.Session["InsertaConsultaPDV"] = "";
            Response.Redirect("~/Pages/Modulos/Planning/ini_PlanningFinal.aspx", true);
        }

        protected void GO_Click(object sender, EventArgs e)
        {
            if (cmbselModulos.Text != "0" && cmbcliente.Text != "0")
            {
                AplicacionWeb oAplicacionWeb = new AplicacionWeb();
                EAplicacionWeb oeAplicacionWeb = oAplicacionWeb.obtenerAplicacion(this.Session["scountry"].ToString().Trim(), cmbselModulos.SelectedItem.Value);
                this.Session["oeAplicacionWeb"] = oeAplicacionWeb;
                this.Session["cod_applucky"] = oeAplicacionWeb.codapplucky;
                this.Session["abr_app"] = oeAplicacionWeb.abrapp;
                this.Session["app_url"] = oeAplicacionWeb.appurl;
                this.Session["companyid"] = cmbcliente.SelectedItem.Value;
                DataTable dturllogo = oCoon.ejecutarDataTable("UP_WEBXPLORA_GEN_LOGOCLIENT", Convert.ToInt32(cmbcliente.Text));
                this.Session["fotocomany"] = dturllogo.Rows[0][0].ToString().Trim();
                this.Session["sNombre"] = cmbcliente.SelectedItem.Text;
                string sPagina = oeAplicacionWeb.HomePage;
                oeAplicacionWeb = null;
                oAplicacionWeb = null;
                this.Response.Redirect("~/" + sPagina, true);
                //if (this.Session["scountry"].ToString() == "589" && cmbselModulos.SelectedItem.Value == "MOVIL")
                //{
                //Response.Redirect("http://localhost:61260/?data=" + Lucky.CFG.Util.Encriptacion.QueryStringEncode(this.Session["sUser"].ToString() + "/" + this.Session["companyid"].ToString() + "/" + this.Session["sNombre"].ToString(), "usr"));

                //    //Response.Redirect("http://localhost:61260 <http://localhost:61260/> ", true); 
                //}
                //else
                //{
                //    this.Response.Redirect("~/" + sPagina, true);
                //}


            }
        }

        protected void cmbselModulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbselModulos.SelectedItem.Value == "0")
            {
                cmbcliente.Enabled = false;
                SelCliente.Visible = false;
                cmbcliente.Visible = false;
            }
            else
            {
                DataTable dtClientes = oCoon.ejecutarDataTable("UP_WEBXPLORA_GEN_CONSULTACLIENTES", Convert.ToInt32(this.Session["personid"].ToString().Trim()));
                cmbcliente.DataSource = dtClientes;
                cmbcliente.DataTextField = "Company_Name";
                cmbcliente.DataValueField = "Company_id";
                cmbcliente.DataBind();
                cmbcliente.Items.Insert(cmbcliente.Items.Count, new ListItem("Lucky SAC", "1478"));
                cmbcliente.Visible = true;

                if (dtClientes.Rows.Count == 1 && this.Session["Perfilid"].ToString().Trim() != "0090")
                {
                    cmbcliente.Visible = false;
                    SelCliente.Visible = false;
                    ScriptManager.RegisterStartupScript(
                    this, this.GetType(), "myscript", "alert('No tiene permisos para visualizar información de ningún Cliente. Por favor solicite al Administrador los permisos necesarios.');", true);
                }
                else
                {
                    if (cmbselModulos.SelectedItem.Value == "AD" || cmbselModulos.SelectedItem.Value == "PLA")
                    {
                        if (this.Session["Perfilid"].ToString().Trim() == "0090")
                        {
                            this.Session["AdmProd"] = "SI";
                            cmbcliente.Items.Insert(cmbcliente.Items.Count, new ListItem("Adm_Prod", this.Session["companyid"].ToString().Trim()));
                            cmbcliente.Text = this.Session["companyid"].ToString().Trim();
                            cmbcliente.Visible = false;
                            SelCliente.Visible = false;
                        }
                        else
                        {
                            cmbcliente.Text = "1478";
                            cmbcliente.Visible = false;
                            SelCliente.Visible = false;
                        }
                    }
                    else
                    {
                        cmbcliente.Items.Remove(cmbcliente.Items.FindByValue("1478"));
                        if (dtClientes.Rows.Count == 2)
                        {
                            cmbcliente.SelectedIndex = 1;
                        }
                        else
                        {
                            cmbcliente.Text = "0";
                            if (cmbselModulos.SelectedItem.Value == "MOVIL")
                            {
                                cmbcliente.Visible = true;
                                SelCliente.Visible = true;
                                cmbcliente.Enabled = true;
                            }
                        }
                    }
                }
            }
        }
    }
}