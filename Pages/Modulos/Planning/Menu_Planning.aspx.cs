using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Business.Common.Application;
using Lucky.CFG.Messenger;
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
                        lblUsuario.Text = "Bienvenido " + sNameUser;
                        //TxtSolicitante.Text = this.Session["smail"].ToString();
                        //TxtEmail.Text = "AdminXplora@lucky.com.pe";
                        DataTable dt = oCoon.ejecutarDataTable("PA_GET_ModulosByCodUsuario", this.Session["codUsuario"].ToString());
                        
                        if(dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                SelModulo.Visible = true;
                                cmbselModulos.Visible = true;
                                SelCliente.Visible = false;
                                GO.Visible = false;
                                cmbcliente.Enabled = true;                                
                                cmbselModulos.DataSource = dt;
                                cmbselModulos.DataValueField = "CODIGO";
                                cmbselModulos.DataTextField = "NOMBRE";
                                cmbselModulos.DataBind();
                                cmbselModulos.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                                //cmbselModulos.Items.Remove(cmbselModulos.Items.FindByValue(this.Session["smodul"].ToString().Trim()));
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
                oMail.Server = "mail.lucky.com.pe";                
                oMail.From = TxtSolicitante.Text;
                oMail.To = "AdminXplora@lucky.com.pe";
                oMail.Subject = TxtMotivo.Text;
                oMail.Body = TxtMensaje.Text;
                oMail.CC = "sgs_mauricio@hotmail.com";
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

        }

        protected void cmbselModulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sPagina = "";
            if (cmbselModulos.Text != "0" && cmbcliente.Text != "0")
            {
                DataTable dt = oCoon.ejecutarDataTable("PA_GET_ModulosByCodModulo", cmbselModulos.SelectedItem.Value);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        sPagina = dt.Rows[0][1].ToString().Trim();
                    }
                }
            }
            this.Response.Redirect("~/" + sPagina, true);

            #region Inservible
            /*
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
             */
            #endregion
        }
    }
}