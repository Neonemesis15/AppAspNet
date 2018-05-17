using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Business.Common.Application;
using Lucky.Business.Common.Security;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Data.Common.Security;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Security;
using Lucky.Entity.Common.Application.Security;
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;
using Lucky.CFG.Tools;



namespace SIGE.Pages.Modulos.Supervision
{
    public partial class ini_supervision : System.Web.UI.Page
    {
        #region Zona de Declaración de Variables Generales
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Conexion oConn = new Conexion();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        int budget=0;
        int planning = 0;
        string planningName = "";

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    string sUser = this.Session["sUser"].ToString();
                    string sPassw = this.Session["sPassw"].ToString();                    
                    usersession.Text = sUser;

                    if (sUser != null && sPassw != null)
                    {
                        llenargrilla();
                    }
                }
                catch (Exception ex)
                {
                    Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                    this.Session.Abandon();
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);

                   
                }
            }
        }
        #region Funciones Comunes
        private void Mensajes()
        {
            LblAlert.Text = this.Session["Encabezado"].ToString().Trim();
            LblFaltantes.Text = this.Session["alertas"].ToString().Trim();
            ModalPopupAlertas.Show();
        }

        protected void ImgCloseSession_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                    Get_Administrativo.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                    this.Session.Abandon();
                    Response.Redirect("~/login.aspx", true);               
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/login.aspx", true);
            }
        }

       
        private void Cancelvista()
        {
            
        }
        #endregion 

        #region DISTRIBUCION TAREAS A OPERATIVOS
        private void llenargrilla()        
        {

            dt = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARPLANNINGSUPERVISOR", this.Session["sUser"].ToString());
               
                //TxtEmail.Text= 
                if (dt.Rows.Count == 0)
                {
                    this.Session["Encabezado"] = "Asignación de Servicio";
                    this.Session["alertas"] = "Sr. Usuario. Actualmente no tiene ninguna asignación de servicio. Por favor intentelo más tarde";
                    Mensajes();
                    MenuSupervisor.Items[0].Enabled = false;
                }
                else
                { 
                    this.Session["EMailSup"] = dt.Rows[0]["Email"].ToString().Trim();
                    TxtSolicitante.Text = this.Session["EMailSup"].ToString();
                    //se llena grilla plannings
                    gvformatos.DataSource = dt;
                    gvformatos.DataBind();
                }
                dt = null;
           
        }        
        #endregion 

    

      

        

       

        protected void gvformatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["alertas"] = "";
            budget = Convert.ToInt32(gvformatos.SelectedRow.Cells[5].Text);
            planning = Convert.ToInt32(gvformatos.SelectedRow.Cells[1].Text);
            this.Session["Planning"] = planning;
            planningName = gvformatos.SelectedRow.Cells[2].Text;
            this.Session["PlanningName"] = planningName;
            this.Session["budget"] = budget;
            ckbSelOperativos.Items.Clear();
            CkbSelpdv.Items.Clear();
            CkbSelFormatos.Items.Clear();

            //se llena combo supervisores    
            dt = oConn.ejecutarDataTable("UP_WEB_CONSULTARPLANNINGSTAFF", budget);
            
            //OJO::: esto se modificio para el modulo operativo .. tener en cuenta para cambiar cuando inicie en el de supervisor
            dt1 = oConn.ejecutarDataTable("UP_WEB_CONSULTARPLANNINGFORMATOS", planning);
            
            
            dt2 = oConn.ejecutarDataTable("UP_WEB_CONSULTARPLANNINGPDV", planning);

            if (dt.Rows.Count == 0)
            {
                this.Session["Encabezado"] = "Sr Usuario:";
                this.Session["alertas"] = this.Session["alertas"] + "No tiene personal operativo asignado. ";
                LblSeloperativo.Visible = false;
            }
            else
            {
                ckbSelOperativos.DataSource = dt;
                ckbSelOperativos.DataTextField = "Nombre";
                ckbSelOperativos.DataValueField = "Person_id";
                ckbSelOperativos.DataBind();
                LblSeloperativo.Visible = true;                
            }
            if (dt1.Rows.Count == 0)
            {
                this.Session["Encabezado"] = "Sr Usuario:";
                this.Session["alertas"] = this.Session["alertas"] + "No tiene Formatos de levantamiento de información. ";
                LblSelFormatos.Visible = false;
            }
            else
            {
                //se llena combo formatos
                CkbSelFormatos.DataSource = dt1;
                CkbSelFormatos.DataTextField = "Formato";
                CkbSelFormatos.DataValueField = "Código";
                CkbSelFormatos.DataBind();
                LblSelFormatos.Visible = true;
            }
            if (dt2.Rows.Count == 0)
            {
                this.Session["Encabezado"] = "Sr Usuario:";
                this.Session["alertas"] = this.Session["alertas"] + "Fue creado sin Puntos de Venta ";
                LblSelpdv.Visible = false;
            }
            else
            {
                //se llena combo puntos de venta
                CkbSelpdv.DataSource = dt2;
                CkbSelpdv.DataTextField = "Nombre";
                CkbSelpdv.DataValueField = "Codigo";
                CkbSelpdv.DataBind();
                LblSelpdv.Visible = true;                
            }
            if (this.Session["alertas"].ToString() != "")
            {
                Mensajes();
            }
            dt = null;
            dt1 = null;
            dt2 = null;
        }
        protected void CkbSelFormatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPrevForm.Visible = true;
            BtnEnviarForm.Visible = true;           
            this.Session["id_cod_point"] = CkbSelFormatos.SelectedValue;            
        }
        protected void CkbSelpdv_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnEnviarPDV.Visible = true;
        }
        protected void btnPrevForm_Click(object sender, EventArgs e)
        {            
            Response.Redirect("~/Pages/Modulos/Supervision/PrevFormatos.aspx",false);
        }

        protected void MenuSupervisor_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MenuSupervisor.Enabled = false;
            Multiview.ActiveViewIndex = index;
            this.Session["view"] = index;
        }

        protected void BtnCofirmarSI_Click(object sender, EventArgs e)
        {
            budget = Convert.ToInt32(this.Session["budget"]);
            dt = oConn.ejecutarDataTable("UP_WEB_CONSULTARPLANNINGSTAFF", budget);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    string scountry = this.Session["scountry"].ToString();
                    ckbSelOperativos.DataSource = dt;
                    ckbSelOperativos.DataTextField = "Nombre";
                    ckbSelOperativos.DataValueField = "Person_id";
                    ckbSelOperativos.DataBind();
                    //Envio Automativo de informacion a los operativos
                    Enviomail oEnviomail = new Enviomail();                    
                    EEnviomail oeMailuser = oEnviomail.Envio_mails(scountry, "Solicitud_Clave");
                    Mails omailenvio = new Mails();
                    omailenvio.Server = oeMailuser.MailServer;
                    omailenvio.From = "mortiz.col@lucky.com.pe";
                    omailenvio.To = dt.Rows[i]["Email"].ToString().Trim();
                    omailenvio.Subject = "Asignación de Actividad Lucky SAC";
                    omailenvio.Body = "Señor: " + dt.Rows[i]["nombre"].ToString().Trim() + "<br /><br />Usted a sido asignado para una actividad de "
                        + dt.Rows[i]["Servicio"].ToString().Trim() + "<br /><br />Por favor contacte al supervisor y solicite información al respecto";
                    omailenvio.BodyFormat = "HTML";
                    omailenvio.send();
                    oEnviomail = null;
                    omailenvio = null;
                    oeMailuser = null;
                }
                ModalPopupConfirmSI.Show();               
            }
            else
            {
                 this.Session["Encabezado"] = "Sr Usuario:";
                this.Session["alertas"] = "No tiene personal operativo asignado. no se puede enviar correos";
                Mensajes();
            }
        }     

        protected void btnCancelVista_Click(object sender, EventArgs e)
        {
            MenuSupervisor.Enabled = true;
            
        }

       

        

       

      

     

       
      



          
           




      
    }

       
}
