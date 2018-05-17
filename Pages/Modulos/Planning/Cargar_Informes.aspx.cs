using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
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

namespace SIGE.Pages.Modulos.Planning
{
    public partial class Cargar_Informes : System.Web.UI.Page
    {
        //DateTime fechaSolicitudP;
        //DateTime fechaFinalP;
        //DateTime fechaIniPreP;
        //DateTime fechaIniPre;
        //DateTime fechaFinPreP;
        //DateTime fechaIniPlaP;
        //DateTime fechaPlaFinP;
        //string splanning;
        //string snameChannel;

        string sUser;
        string sPassw;
        string sCod_channel; 
        int ipersonid;

        Conexion oCoon = new Conexion();
        Info_Planning_City oInfo_Planning_City = new Info_Planning_City();

        Facade_Menu_strategy.Facade_MPlanning menu = new SIGE.Facade_Menu_strategy.Facade_MPlanning();
        Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE Presupuesto = new SIGE.Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE();
        Facade_Proceso_Planning.Facade_Proceso_Planning Planning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    sUser = this.Session["sUser"].ToString();
                    sPassw = this.Session["sPassw"].ToString();
                    ipersonid = Convert.ToInt32(this.Session["personid"]);
                    usersession.Text = sUser;
                    if (sUser != null && sPassw != null)
                    {
                        PConsulta.Style.Value = "Display:Block;";
                        LblTitCargarArchivo.Text = "Crear nuevas Campañas";
                        btnSerch.Enabled = false;
                        btnSerch.Style.Value = "background-image: url('../../images/bg_btn_ahover.png');";
                        BtnCrearPlanning.Enabled = false;
                        LblTitCargarArchivo.Text = "Agregar informes a planning existentes";
                        Obtener_CampañasxUsuario();

                        //08-10-2010 ing. mauricio ortiz se cambia forma de llenar reportes, canales y servicio  
                        //por lo cual ya no se llena desde aqui sino desde el evento cmbPlanning_SelectedIndexChanged.
                        //LLenar_Reporte();
                        //Llena_Canal();
                        //Llenar_Servicios();

                        //TabContainerPlanning.Tabs[0].Enabled = false;
                        //TabContainerPlanning.Tabs[1].Enabled = true;
                        Llena_Meses();
                        Llena_años();
                        Llena_CanalBuscar();
                        ObtenerClientesBuscar();
                        llenaConsultaInformesTotal();

                        ///////////ObtenerClientes();

                        //ObtenerDatosEnvioMail();
                        //Limpiar_InformacionBasica();
                        //cmbpresupuesto.Text = "0";

                        //BtnSavePlanning.Visible = false;
                        //InfoPlanningBasico.Style.Value = "Display:none;";
                        PConsulta.Style.Value = "Display:Block;";

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

        private void Obtener_CampañasxUsuario()
        {
            //08-10-2010 Ing. Mauricio Ortiz se modifica [UP_WEBXPLORA_OBTENERCAMPAÑASXCLIETE]
            //se modifica la consulta para q muestre unicamente las campañas 
            //a las cuales el usuario que se logeó tiene ingerencia y se cambia parametro @COMPANYID INT por poerson_id
            // DataTable dt = Planning.Get_ObtenerCampañasxCliente(Convert.ToInt32(this.Session["personid"].ToString().Trim()));
            //07/09/2011 - Se agrega el parametro company_id adicional al Person apra la consulta de campañas.
            int company_id = Convert.ToInt32(this.Session["companyid"].ToString().Trim());
            DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OBTENERCAMPAÑASXCLIETE", ipersonid, company_id);
            //Planning.Get_ObtenerCampañasxCliente(ipersonid);
            this.Session["CampañasXusuario"] = dt;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    cmbPlanning.DataSource = dt;
                    cmbPlanning.DataValueField = "id_planning";
                    cmbPlanning.DataTextField = "Planning_name";
                    cmbPlanning.DataBind();
                    cmbPlanning.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                }
                else
                {
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["mensaje"] = "Usted no ha sido asignado aún a ninguna campaña.";
                    Mensajes_Usuario();
                }
            }
        }
        /// <summary>
        /// Metodo para Llenar ciudades donde aplica el informe Ing. Carlos Hernandez 05/10/2010
        /// //Se cambia el sp y se agrega el parametro de cliente las ciudades se obrtendran de City_userreport Ing. Carlos Hernandez
        /// </summary>
        private void llenarCiudades()
        {
            DataTable dtc = null;
            dtc = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENERCITY", Convert.ToInt32(RbtnCliente.SelectedValue), Convert.ToInt32(RbtnListReport.SelectedItem.Value));
            if (dtc.Rows.Count > 0)
            {
                Chkcityorig.DataSource = dtc;
                Chkcityorig.DataValueField = "cod_Oficina";
                Chkcityorig.DataTextField = "Name_Oficina";
                Chkcityorig.DataBind();
                //Chkcityorig.Items.Remove(Chkcityorig.Items[0]);//Se modifica porque estaba eliminando un item Ing. Carlos Hernandez
                //Ya no es necesario debido a que se cambio el store Ing. Mauricio Ortiz
                Chkcityorig.Enabled = true;
                BtnAllCobertura.Visible = true;
                BtnNone.Visible = true;
            }
            else
            {
                Chkcityorig.Enabled = true;
                Chkcityorig.Items.Clear();
                BtnAllCobertura.Visible = false;
                BtnNone.Visible = false;
            }
        }

        // 20/10/2010 Ing. Mauricio Ortiz esta funcionalidad pasa al login de Xplora 
        //private void ObtenerDatosEnvioMail()
        //{
        //    string dia = Convert.ToString(DateTime.Now.DayOfWeek);        
        //    if (dia == Convert.ToString(DayOfWeek.Friday))
        //    {
        //        string fechavalida = Convert.ToString(DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " 10:01:00");
        //        DataSet dsenvio = null;
        //        dsenvio = oCoon.ejecutarDataSet("UP_WEBXPLORA_AD_OBTENERDIASENVIO");

        //        // validar existencia de informes para enviar por correo
        //        // recupera servicio y cliente
        //        if (dsenvio != null)
        //        {
        //            if (dsenvio.Tables[0].Rows.Count > 0)
        //            {
        //                for (int i = 0; i <= dsenvio.Tables[0].Rows.Count - 1; i++)
        //                {
        //                    //Por Solicitud de Alicorp se inactiva el envio de correos hasta nueva orden Ing. Carlos Hernandez

        //                    //envio de correo al cliente con información de los reportes cargados
        //                    try
        //                    {
        //                        //De Acuerdo a Requerimiento de5 Alicorp solicito que los correos se enviaran x Canal, Servicio Ing. Carlos Alberto Hernández Rincón
        //                        DataTable dtpersonalenvio = null;

        //                        // recuperar listado general de personas a las cuales se les enviará correo
        //                        dtpersonalenvio = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLANNING_OBTENEREMAILUSERS", 0, Convert.ToInt32(dsenvio.Tables[0].Rows[i][1].ToString().Trim()), 0, "none", Convert.ToInt32(dsenvio.Tables[0].Rows[i][0].ToString().Trim()),"none");
        //                        //Planning.Get_Obtener_Datos_Cliente(0, Convert.ToInt32(dsenvio.Tables[0].Rows[i][1].ToString().Trim()), 0, "none", Convert.ToInt32(dsenvio.Tables[0].Rows[i][0].ToString().Trim()),"");
        //                        if (dtpersonalenvio != null)
        //                        {
        //                            if (dtpersonalenvio.Rows.Count > 0)
        //                            {
        //                                for (int ipersonalenvio = 0; ipersonalenvio <= dtpersonalenvio.Rows.Count - 1; ipersonalenvio++)
        //                                //USAR PARA PRUEBAS ESTE FOR Y COMENTARIAR EL DE ARRIBA -----> 
        //                                //for (int ipersonalenvio = 1; ipersonalenvio <= 1; ipersonalenvio++)
        //                                {
        //                                    Enviomail oEnviomail = new Enviomail();
        //                                    EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Solicitud_Clave");
        //                                    Mails oMail = new Mails();
        //                                    oMail.Server = oeEmail.MailServer;
        //                                    oMail.From = oeEmail.MailFrom;
        //                                    oMail.To = dtpersonalenvio.Rows[ipersonalenvio]["Person_Email"].ToString().Trim();
        //                                    //oMail.To = "sgs_mauricio@hotmail.com";
        //                                    oMail.Subject = "Lucky SAC : Informes Cargados en Portal Xplora";
        //                                    string tabla = "<table style=" + '"' + "font-family: verdana; font-size: 11px; color: #0000FF;" + '"';
        //                                    string tablaclose = "</table><br>";
        //                                    string informacion = "";
        //                                    for (int icanales = 0; icanales <= dsenvio.Tables[1].Rows.Count - 1; icanales++)
        //                                    {
        //                                        if (dsenvio.Tables[1].Rows[icanales]["Company_id"].ToString().Trim() == dsenvio.Tables[0].Rows[i]["Company_id"].ToString().Trim())
        //                                        {
        //                                            // (Obtener informes A enviar dependiendo usuario de carga , servicio canal y reportes por usuario 
        //                                            DataTable dtasociacionpersonal = null;
        //                                            dtasociacionpersonal = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLANNING_OBTENEREMAILUSERS", 1, Convert.ToInt32(dsenvio.Tables[0].Rows[i][1].ToString().Trim()), Convert.ToInt32(dtpersonalenvio.Rows[ipersonalenvio]["Person_id"].ToString().Trim()), dsenvio.Tables[1].Rows[icanales]["cod_Channel"].ToString().Trim(), Convert.ToInt32(dsenvio.Tables[0].Rows[i][0].ToString().Trim()),fechavalida);
        //                                            // Planning.Get_Obtener_Datos_Cliente(1, Convert.ToInt32(dsenvio.Tables[0].Rows[i][1].ToString().Trim()), Convert.ToInt32(dtpersonalenvio.Rows[ipersonalenvio]["Person_id"].ToString().Trim()), dsenvio.Tables[1].Rows[icanales]["cod_Channel"].ToString().Trim(), Convert.ToInt32(dsenvio.Tables[0].Rows[i][0].ToString().Trim()), this.Session["sUser"].ToString().Trim());

        //                                            if (dtasociacionpersonal != null)
        //                                            {
        //                                                if (dtasociacionpersonal.Rows.Count > 0)
        //                                                {
        //                                                    informacion = informacion + "<div style=" + '"' + "font-weight: bold;" + '"' + ">" + dsenvio.Tables[1].Rows[icanales][1].ToString().Trim() + "</div>";
        //                                                    informacion = informacion + tabla;
        //                                                    for (int j = 0; j <= dtasociacionpersonal.Rows.Count - 1; j++)
        //                                                    {
        //                                                        if (Convert.ToDateTime(dtasociacionpersonal.Rows[j]["fecha_Registro"].ToString().Trim()) <= Convert.ToDateTime(fechavalida))
        //                                                            {
        //                                                                informacion = informacion + "<tr><td style=" + '"' + "width: 400px;" + '"' + ">" + dtasociacionpersonal.Rows[j]["informe"].ToString().Trim() + "</td>" + "<td style=" + '"' + "width: 200px;" + '"' + ">" + dtasociacionpersonal.Rows[j]["fecha"].ToString().Trim() + "</td>" + "<td style=" + '"' + "width: 200px;" + '"' + ">" + dtasociacionpersonal.Rows[j]["reporte"].ToString().Trim() + "</td></tr>";
        //                                                            }
                                                                
        //                                                    }
        //                                                    informacion = informacion + tablaclose;
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                    if (informacion != "")
        //                                    {
        //                                        string imagencorreo = "<img  src=" + '"' + "http://sige.lucky.com.pe/pages/images/LinkXplora.png" + '"' + " width=" + '"' + "59px" + '"' + "height=" + '"' + "15px" + '"' + " GALLERYIMG=" + '"' + "no" + '"' + "/>";
        //                                        string[] textArray2 = new string[] { };
        //                                        string[] textArray1 = new string[] { "<div style=" + '"' + "font-family: verdana; font-size: 11px; color: #0000FF;" + '"' +">Señor(a) " , dtpersonalenvio.Rows[ipersonalenvio]["Nombres"].ToString().Trim(), "<br>" ,
        //                                    "Lo invitamos a acceder al sistema de consultas en línea al cual puede acceder a traves del siguiente link: ",
        //                                    "<a href=" + "http://sige.lucky.com.pe" + ">" + imagencorreo + "</a>" ,"<br><br>" ,
                                            
        //                                    "Encontrará a su disposición la siguiente información:" , "<br><br>" ,
        //                                      informacion,                                       "<br><br>" ,                                            
        //                                    "Para información adicional comuníquese con nosotros. Quedamos atentos a sus comentarios", "<br><br>" ,"<br><br>" ,                                            
        //                                    "Cordialmente", "<br>", "Administrador Xplora </div>"
        //                                };

        //                                        oMail.Body = string.Concat(textArray1);
        //                                        oMail.BodyFormat = "HTML";
        //                                        oMail.send();
        //                                        oMail = null;
        //                                        oeEmail = null;
        //                                        oEnviomail = null;
        //                                    }
        //                                }
        //                            }
        //                        }
                              
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        this.Session["cssclass"] = "MensajesSupervisor";
        //                        this.Session["encabemensa"] = "Sr. Usuario";
        //                        this.Session["mensaje"] = "Se creo el reporte pero no fue posible enviar aviso al cliente.";
        //                        Mensajes_Usuario();
        //                    }
        //                }
        //            }
        //        }
        //        Planning.Get_Actualiza_EstadoEnvioMail(fechavalida);
        //    }
        //}

        private void llenarsubcanales()
        {
            DataSet ds;
            int company;
            string canal;
            canal = ChkListCanal.Items[0].Value; // this.Session["Canal"].ToString().Trim();
            company = Convert.ToInt32(RbtnCliente.Items[0].Value); //Convert.ToInt32(this.Session["companyid"])

            ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_OBTENERSUBCANALES", canal, company);
            RbtnSubcanales.Items.Clear();
            LblSubCanal.Visible = false;
            RbtnSubcanales.Visible = false;

            if (ds.Tables[0].Rows.Count > 0)
            {
                LblSubCanal.Visible = true;
                RbtnSubcanales.Visible = true;
                RbtnSubcanales.DataSource = ds.Tables[0];
                RbtnSubcanales.DataValueField = "id_sucanal";
                RbtnSubcanales.DataTextField = "Subcanal";
                RbtnSubcanales.DataBind();
            }
           
                ds = null;
            
        }

        private void llenarsubniveles()
        {
            DataSet ds;
          
            string subcanal;            
            subcanal = RbtnSubcanales.SelectedItem.Value;

            ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_OBTENERSUBNIVELES", subcanal);
            RbtnSubNivel.Items.Clear();
            LblSubnivel.Visible = false;
            RbtnSubNivel.Visible = false;

            if (ds.Tables[0].Rows.Count > 0)
            {
                LblSubnivel.Visible = true;
                RbtnSubNivel.Visible = true;
                RbtnSubNivel.DataSource = ds.Tables[0];
                RbtnSubNivel.DataValueField = "id_Subnivel";
                RbtnSubNivel.DataTextField = "SubNivel";
                RbtnSubNivel.DataBind();
            }

            if (RbtnSubNivel.Items.Count == 1)
            {
                RbtnSubNivel.Items[0].Selected = true;
                RbtnSubNivel.Visible = false;
                LblSubnivel.Visible = false;
                LLenar_Reporte();
            }
            ds = null;

        }

        /// <summary>
        /// Se Crea Esta función para obtenern los Servicios para suplir requerimiento Alicorp Ing. Carlos Alberto Hernández Rincón
        /// </summary>
        //private void Llenar_Servicios()
        //{
        //    DataTable dtservice = null;
        //    dtservice = Planning.Get_ObtenerServices(this.Session["scountry"].ToString());
        //    if (dtservice.Rows.Count > 0)
        //    {
        //        rblservice.DataSource = dtservice;
        //        rblservice.DataValueField = "cod_Strategy";
        //        rblservice.DataTextField = "Servicio";
        //        rblservice.DataBind();
        //    }
        //}
        private void Llena_CanalBuscar()
        {
            DataTable dt = new DataTable();

           int compañia = Convert.ToInt32(this.Session["companyid"]);
           string pais = this.Session["scountry"].ToString();

           dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CHANNEL", pais, compañia);
           // dt = Planning.Get_ObtenerCanales();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    CmbCanal.DataSource = dt;
                    CmbCanal.DataValueField = "cod_Channel";
                    CmbCanal.DataTextField = "Channel_Name";
                    CmbCanal.DataBind();
                    CmbCanal.Items.Insert(0, new ListItem("--Todos--", "0"));
                    dt = null;
                }
            }
        }
        private void Llena_Meses()
        {
            DataTable dt = Get_Administrativo.Get_ObtenerMeses();
            CmbSelMes.DataSource = dt;
            CmbSelMes.DataValueField = "codmes";
            CmbSelMes.DataTextField = "namemes";
            CmbSelMes.DataBind();
            CmbSelMes.Items.Insert(0, new ListItem("--Seleccione--", "0"));

            CmbBuscarSelMes.DataSource = dt;
            CmbBuscarSelMes.DataValueField = "codmes";
            CmbBuscarSelMes.DataTextField = "namemes";
            CmbBuscarSelMes.DataBind();
            CmbBuscarSelMes.Items.Insert(0, new ListItem("--Todos--", "0"));

        }
        private void Llena_años()
        {
            DataTable dt = Get_Administrativo.Get_ObtenerYears();


            CmbBuscarSelAño.DataSource = dt;
            CmbBuscarSelAño.DataValueField = "Years_id";
            CmbBuscarSelAño.DataTextField = "Years_Number";
            CmbBuscarSelAño.DataBind();
            CmbBuscarSelAño.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        }
        private void ObtenerClientesBuscar()
        {
            DataTable dt = Planning.Get_ObtenerClienteconPlanning(this.Session["scountry"].ToString(),
                this.Session["idnivel"].ToString(), Convert.ToInt32(this.Session["companyid"].ToString().Trim()));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    CmbClienteBuscar.DataSource = dt;
                    CmbClienteBuscar.DataValueField = "Company_id";
                    CmbClienteBuscar.DataTextField = "Company_Name";
                    CmbClienteBuscar.DataBind();
                    if (dt.Rows.Count != 1)
                    {
                        CmbClienteBuscar.Items[0].Text = "--Todos--";
                    }
                }

                CmbClienteBuscar.SelectedValue=this.Session["companyid"].ToString();
                         
            }
        }
        private void llenaConsultaInformes()
        {
            try
            {

                DataTable dturl = null;
                string slevel;
                slevel = this.Session["idnivel"].ToString();
                gvLink_informes.EmptyDataText = "...La consulta no produjo ningún resultado...";
                if (slevel == "0001" || slevel == "0037" || slevel == "0090")
                {
                    if (CmbClienteBuscar.Text == "0" && CmbCanal.Text == "0" && CmbBuscarSelMes.Text == "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(0, "0", "0", this.Session["sUser"].ToString().Trim(), 1, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text != "0" && CmbCanal.Text == "0" && CmbBuscarSelMes.Text == "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(Convert.ToInt32(CmbClienteBuscar.Text), "0", "0", this.Session["sUser"].ToString().Trim(), 2, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text == "0" && CmbCanal.Text != "0" && CmbBuscarSelMes.Text == "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(0, CmbCanal.Text, "0", this.Session["sUser"].ToString().Trim(), 3, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text != "0" && CmbCanal.Text != "0" && CmbBuscarSelMes.Text == "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(Convert.ToInt32(CmbClienteBuscar.Text), CmbCanal.Text, "0", this.Session["sUser"].ToString().Trim(), 4, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text == "0" && CmbCanal.Text == "0" && CmbBuscarSelMes.Text != "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(0, "0", CmbBuscarSelMes.Text + "/" + CmbBuscarSelAño.SelectedItem.Text, this.Session["sUser"].ToString().Trim(), 9, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text != "0" && CmbCanal.Text == "0" && CmbBuscarSelMes.Text != "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(Convert.ToInt32(CmbClienteBuscar.Text), "0", CmbBuscarSelMes.Text + "/" + CmbBuscarSelAño.SelectedItem.Text, this.Session["sUser"].ToString().Trim(), 10, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text == "0" && CmbCanal.Text != "0" && CmbBuscarSelMes.Text != "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(0, CmbCanal.Text, CmbBuscarSelMes.Text + "/" + CmbBuscarSelAño.SelectedItem.Text, this.Session["sUser"].ToString().Trim(), 11, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text != "0" && CmbCanal.Text != "0" && CmbBuscarSelMes.Text != "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(Convert.ToInt32(CmbClienteBuscar.Text), CmbCanal.Text, CmbBuscarSelMes.Text + "/" + CmbBuscarSelAño.SelectedItem.Text, this.Session["sUser"].ToString().Trim(), 12, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }




                }
                if (slevel == "0030")
                {
                    if (CmbClienteBuscar.Text == "0" && CmbCanal.Text == "0" && CmbBuscarSelMes.Text == "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(0, "0", "0", "0", 5, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text != "0" && CmbCanal.Text == "0" && CmbBuscarSelMes.Text == "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(Convert.ToInt32(CmbClienteBuscar.Text), "0", "0", "0", 6, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text == "0" && CmbCanal.Text != "0" && CmbBuscarSelMes.Text == "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(0, CmbCanal.Text, "0", "0", 7, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text != "0" && CmbCanal.Text != "0" && CmbBuscarSelMes.Text == "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(Convert.ToInt32(CmbClienteBuscar.Text), CmbCanal.Text, "0", "0", 8, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }

                    if (CmbClienteBuscar.Text == "0" && CmbCanal.Text == "0" && CmbBuscarSelMes.Text != "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(0, "0", CmbBuscarSelMes.Text + "/" + CmbBuscarSelAño.SelectedItem.Text, "0", 13, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text != "0" && CmbCanal.Text == "0" && CmbBuscarSelMes.Text != "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(Convert.ToInt32(CmbClienteBuscar.Text), "0", CmbBuscarSelMes.Text + "/" + CmbBuscarSelAño.SelectedItem.Text, "0", 14, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text == "0" && CmbCanal.Text != "0" && CmbBuscarSelMes.Text != "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(0, CmbCanal.Text, CmbBuscarSelMes.Text + "/" + CmbBuscarSelAño.SelectedItem.Text, "0", 15, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                    if (CmbClienteBuscar.Text != "0" && CmbCanal.Text != "0" && CmbBuscarSelMes.Text != "0")
                    {
                        dturl = Planning.Get_ConsultainformesCargados(Convert.ToInt32(CmbClienteBuscar.Text), CmbCanal.Text, CmbBuscarSelMes.Text + "/" + CmbBuscarSelAño.SelectedItem.Text, "0", 16, slevel);
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                }
                dturl = null;
            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenaConsultaInformesTotal()
        {
            string slevel;
            gvLink_informes.EmptyDataText = "...La consulta no produjo ningún resultado...";

            slevel = this.Session["idnivel"].ToString();
            if (slevel == "0001" || slevel == "0037" || slevel == "0090")
            {

                DataTable dturl = Planning.Get_ConsultainformesCargados(0, "0", "0", this.Session["sUser"].ToString().Trim(), 1, slevel);
                if (dturl != null)
                {
                    if (dturl.Rows.Count >= 0)
                    {
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                }
            }

            if (slevel == "0030")
            {

                DataTable dturl = Planning.Get_ConsultainformesCargados(Convert.ToInt32(this.Session["companyid"].ToString().Trim()), "0", "0", "0", 6, slevel);
                if (dturl != null)
                {
                    if (dturl.Rows.Count >= 0)
                    {
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();
                        gvLink_informes.SelectedIndex = -1;
                    }
                }
            }
        }

        //private void Llena_Canal()
        //{
        //    DataTable dt = new DataTable();
        //    dt = Planning.Get_ObtenerCanales();
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            ChkListCanal.DataSource = dt;
        //            ChkListCanal.DataValueField = "cod_Channel";
        //            ChkListCanal.DataTextField = "Channel_Name";
        //            ChkListCanal.DataBind();
        //            dt = null;
        //        }
        //    }
        //}
        private void LLenar_Reporte()
        {
            //08-10-2010 Ing. Mauricio Ortiz se modifica forma de llenar los reportes , ahora es por cliente , canal y servicio 
            DataSet ds = new DataSet();
            ds = Get_Administrativo.llenaCheckInformes(Convert.ToInt32(RbtnCliente.Items[0].Value), ChkListCanal.Items[0].Value, Convert.ToInt32(rblservice.Items[0].Value));

            if (ds.Tables[0].Rows.Count > 0)
            {
                RbtnListReport.DataSource = ds.Tables[0];
                RbtnListReport.DataValueField = "Report_Id";
                RbtnListReport.DataTextField = "Report_NameReport";
                RbtnListReport.DataBind();
            }
        }
        //private bool LlenaPresupuesto()
        //{
        //    bool Continuar = true;
        //    DataTable dt = new DataTable(); ;
        //    dt = Presupuesto.Presupuesto(this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            cmbpresupuesto.DataSource = dt;
        //            cmbpresupuesto.DataValueField = "Numero_Presupuesto";
        //            cmbpresupuesto.DataTextField = "Nombre";
        //            cmbpresupuesto.DataBind();
        //            this.Session["NameCountry"] = dt.Rows[1]["Name_Country"].ToString().Trim();
        //            //Se destruye el Datatable una vez consultado
        //            dt = null;
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Sr. Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";
        //            this.Session["mensaje"] = "No hay presupuesto disponible";
        //            Mensajes_Usuario();
        //            Continuar = false;
        //        }
        //    }
        //    return Continuar;
        //}

        //private void Limpiar_InformacionBasica()
        //{
        //    txtnamepresu.Text = "";
        //    txtcliente.Text = "";
        //    txtservice.Text = "";
        //    TxtFechaSolicitud.Text = "";
        //    TxtFechaEntrega.Text = "";
        //    TxtFechainipre.Text = "";
        //    TxtFechainiPla.Text = "";
        //    TxtFechafinpre.Text = "";
        //    TxtFechaplanfin.Text = "";
        //    txtcontacto.Text = "";
        //    txtmail.Text = "";
        //    txtarea.Text = "";
        //    txtnumpla.Text = "";
        //}
        //private void Activar_InformacionBasica()
        //{
        //    txtnamepresu.Enabled = true;
        //    txtcliente.Enabled = true;
        //    txtservice.Enabled = true;
        //    TxtFechaSolicitud.Enabled = true;
        //    TxtFechaEntrega.Enabled = true;

        //    TxtFechainipre.Enabled = true;
        //    TxtFechainiPla.Enabled = true;

        //    TxtFechafinpre.Enabled = true;
        //    TxtFechaplanfin.Enabled = true;
        //    txtcontacto.Enabled = true;
        //    txtmail.Enabled = true;
        //    txtarea.Enabled = true;
        //    ImgBtnCal.Enabled = true;
        //    ImgBtnCal1.Enabled = true;
        //    ImgBtnCal2.Enabled = true;
        //    ImgBtnCal3.Enabled = true;
        //    ImgBtnCal4.Enabled = true;
        //    ImgBtnCal5.Enabled = true;
        //}
        //private void Inactivar_InformacionBasica()
        //{
        //    txtnamepresu.Enabled = false;
        //    txtcliente.Enabled = false;
        //    txtservice.Enabled = false;
        //    TxtFechaSolicitud.Enabled = false;
        //    TxtFechaEntrega.Enabled = false;
        //    TxtFechainipre.Enabled = false;
        //    TxtFechainiPla.Enabled = false;
        //    TxtFechafinpre.Enabled = false;
        //    TxtFechaplanfin.Enabled = false;
        //    txtcontacto.Enabled = false;
        //    txtmail.Enabled = false;
        //    txtarea.Enabled = false;
        //    ImgBtnCal.Enabled = false;
        //    ImgBtnCal1.Enabled = false;
        //    ImgBtnCal2.Enabled = false;
        //    ImgBtnCal3.Enabled = false;
        //    ImgBtnCal4.Enabled = false;
        //    ImgBtnCal5.Enabled = false;
        //}

        //private bool Validad_FechaSolicitud()
        //{
        //    //@FechaPA
        //    bool validar = true;
        //    try
        //    {
        //        fechaSolicitudP = DateTime.Parse(TxtFechaSolicitud.Text);
        //        fechaFinalP = DateTime.Parse(TxtFechaEntrega.Text);
        //        fechaIniPreP = DateTime.Parse(TxtFechainipre.Text);
        //        fechaIniPlaP = DateTime.Parse(TxtFechainiPla.Text);

        //        if (fechaSolicitudP >= fechaIniPreP || fechaSolicitudP >= fechaIniPlaP || fechaSolicitudP > fechaFinalP)
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            if (fechaSolicitudP >= fechaIniPreP)
        //            {
        //                this.Session["mensaje"] = "La fecha de solicitud de la campaña no puede ser mayor y /o igual a la fecha de inicio de Preproducción";
        //            }
        //            else if (fechaSolicitudP >= fechaIniPlaP)
        //            {
        //                this.Session["mensaje"] = "La Fecha de solicitud de la campaña no puede ser mayor y o igual a la fecha de inicio de ejecución";
        //            }
        //            else if (fechaSolicitudP > fechaFinalP)
        //            {
        //                this.Session["mensaje"] = "La fecha de solicitud de la campaña no puede ser mayor a la fecha de entrega final";
        //            }
        //            validar = false;
        //            Mensajes_Usuario();
        //        }
        //        return validar;
        //    }
        //    catch
        //    {
        //        validar = false;
        //        this.Session["encabemensa"] = "Señor Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
        //        Mensajes_Usuario();
        //        return validar;
        //    }
        //}
        //private bool Validar_Fecha_Entrega_Final()
        //{
        //    //FechaPa
        //    bool valido = true;
        //    try
        //    {
        //        fechaSolicitudP = DateTime.Parse(TxtFechaSolicitud.Text);
        //        fechaFinalP = DateTime.Parse(TxtFechaEntrega.Text);
        //        fechaIniPreP = DateTime.Parse(TxtFechainipre.Text);
        //        fechaFinPreP = DateTime.Parse(TxtFechafinpre.Text);
        //        fechaIniPlaP = DateTime.Parse(TxtFechainiPla.Text);
        //        fechaPlaFinP = DateTime.Parse(TxtFechaplanfin.Text);
        //        if (fechaFinalP < DateTime.Today)
        //        {
        //            valido = false;
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";
        //            this.Session["mensaje"] = "La fecha de entrega final no puede ser menor que la actual";
        //            Mensajes_Usuario();
        //        }
        //        else
        //            if (fechaFinalP <= fechaSolicitudP)
        //            {
        //                valido = false;
        //                this.Session["encabemensa"] = "Señor Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";
        //                this.Session["mensaje"] = "La fecha de entrega final no pude ser menor y/o igual a la fecha de solicitud";
        //                Mensajes_Usuario();

        //            }
        //            else if (fechaFinalP <= fechaIniPreP)
        //            {
        //                valido = false;
        //                this.Session["encabemensa"] = "Señor Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";

        //                this.Session["mensaje"] = "La fecha de entrega final no pude ser menor y/o igual a la fecha de inicio de Preproducción";
        //                Mensajes_Usuario();

        //            }
        //            else
        //                if (fechaFinalP <= fechaFinPreP)
        //                {
        //                    valido = false;
        //                    this.Session["encabemensa"] = "Señor Usuario";
        //                    this.Session["cssclass"] = "MensajesSupervisor";

        //                    this.Session["mensaje"] = "La fecha de entrega final no pude ser menor y/o igual a la fecha de fin de Preproducción";
        //                    Mensajes_Usuario();

        //                }
        //                else if (fechaFinalP <= fechaIniPlaP)
        //                {
        //                    valido = false;
        //                    this.Session["encabemensa"] = "Señor Usuario";
        //                    this.Session["cssclass"] = "MensajesSupervisor";

        //                    this.Session["mensaje"] = "La fecha de entrega final no pude ser menor y/o igual a la fecha de inicio de ejecución";
        //                    Mensajes_Usuario();
        //                }
        //                else
        //                    if (fechaFinalP <= fechaPlaFinP)
        //                    {
        //                        valido = false;
        //                        this.Session["encabemensa"] = "Señor Usuario";
        //                        this.Session["cssclass"] = "MensajesSupervisor";

        //                        this.Session["mensaje"] = "La fecha de entrega final no pude ser menor y/o igual a la fecha de fin de ejecución";
        //                        Mensajes_Usuario();
        //                    }
        //        return valido;
        //    }
        //    catch
        //    {
        //        valido = false;
        //        this.Session["encabemensa"] = "Señor Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
        //        Mensajes_Usuario();
        //        return valido;
        //    }
        //}
        //private bool Validad_Fecha_InicioPreproducción()
        //{
        //    bool sigue = true;
        //    try
        //    {
        //        fechaFinalP = DateTime.Parse(TxtFechaEntrega.Text);
        //        fechaIniPreP = DateTime.Parse(TxtFechainipre.Text);
        //        fechaFinPreP = DateTime.Parse(TxtFechafinpre.Text);
        //        fechaIniPlaP = DateTime.Parse(TxtFechainiPla.Text);

        //        if (fechaIniPreP < DateTime.Today || fechaIniPreP > fechaFinPreP || fechaIniPreP > fechaIniPlaP || fechaIniPreP > fechaFinalP)
        //        {
        //            if (fechaIniPreP < DateTime.Today)
        //            {
        //                this.Session["encabemensa"] = "Señor Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";

        //                this.Session["mensaje"] = "La fecha de inicio de Preproducción no puede ser menor de  la fecha actual";
        //                Mensajes_Usuario();
        //                return false;
        //            }
        //            if (fechaIniPreP > fechaFinPreP)
        //            {
        //                this.Session["encabemensa"] = "Señor Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";
        //                this.Session["mensaje"] = "La fecha de inicio de Preproducción no puede ser mayor que la fecha de fin de Preproducción";
        //                Mensajes_Usuario();
        //                return false;
        //            }

        //            if (fechaIniPreP > fechaIniPlaP)
        //            {
        //                this.Session["encabemensa"] = "Señor Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";

        //                this.Session["mensaje"] = "La Fecha de inicio de Preproducción no puede ser mayor que la fecha de inicio del Plannning";
        //                Mensajes_Usuario();
        //                return false;

        //            }
        //            if (fechaIniPreP > fechaFinalP)
        //            {
        //                this.Session["encabemensa"] = "Señor Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";

        //                this.Session["mensaje"] = "La fecha de inicio de Preproducción no puede ser mayor quela fecha de entrega final";
        //                Mensajes_Usuario();
        //                return false;
        //            }
        //        }
        //        return sigue;
        //    }
        //    catch
        //    {
        //        sigue = false;
        //        this.Session["encabemensa"] = "Señor Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
        //        Mensajes_Usuario();
        //        return sigue;
        //    }
        //}
        //private bool Validad_Fecha_finPreproducción()
        //{
        //    //FechaPa          
        //    try
        //    {
        //        fechaSolicitudP = DateTime.Parse(TxtFechaSolicitud.Text);
        //        fechaFinalP = DateTime.Parse(TxtFechaEntrega.Text);
        //        fechaIniPre = DateTime.Parse(TxtFechainipre.Text);
        //        fechaFinPreP = DateTime.Parse(TxtFechafinpre.Text);
        //        fechaIniPlaP = DateTime.Parse(TxtFechainiPla.Text);
        //        fechaPlaFinP = DateTime.Parse(TxtFechaplanfin.Text);
        //        if (fechaFinPreP < DateTime.Today || fechaFinPreP < fechaSolicitudP || fechaFinPreP > fechaIniPlaP || fechaFinPreP > fechaPlaFinP || fechaFinPreP > fechaFinalP)
        //        {
        //            if (fechaIniPre < DateTime.Today)
        //            {
        //                this.Session["encabemensa"] = "Señor Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";

        //                this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser menor de  la fecha actual";
        //                Mensajes_Usuario();
        //                return false;
        //            }
        //            if (fechaFinPreP < fechaSolicitudP)
        //            {
        //                this.Session["encabemensa"] = "Señor Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";

        //                this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser mayor que la fecha de solicitud";
        //                Mensajes_Usuario();
        //                return false;
        //            }

        //            if (fechaFinPreP > fechaIniPlaP)
        //            {
        //                this.Session["encabemensa"] = "Señor Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";

        //                this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser mayor que la fecha de inicio del Plannning";
        //                Mensajes_Usuario();
        //                return false;

        //            }
        //            if (fechaFinPreP > fechaFinalP)
        //            {
        //                this.Session["encabemensa"] = "Señor Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";

        //                this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser mayor quela fecha de entrega final";
        //                Mensajes_Usuario();
        //                return false;

        //            }

        //            if (fechaFinPreP > fechaPlaFinP)
        //            {
        //                this.Session["encabemensa"] = "Señor Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";

        //                this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser mayor quela fecha fin de ejecución";
        //                Mensajes_Usuario();
        //                return false;
        //            }
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        this.Session["encabemensa"] = "Señor Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
        //        Mensajes_Usuario();
        //        return false;
        //    }
        //}
        //private bool Validar_fechas()
        //{
        //    //FechaPA
        //    try
        //    {
        //        fechaIniPlaP = DateTime.Parse(TxtFechainiPla.Text);
        //        fechaPlaFinP = DateTime.Parse(TxtFechaplanfin.Text);
        //        if (fechaIniPlaP >= fechaPlaFinP)
        //        {

        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            //Francisco Martinez: Se cambia el mensaje - 25/03/2010
        //            //this.Session["mensaje"] = this.Session["mensaje"] + " " + "La Fecha de Inicio de Ejecucion no puede ser mayor y/o Igual que la de Finalización";
        //            this.Session["mensaje"] = "La fecha de inicio de ejecución no puede ser mayor y/o igual que la de finalización";
        //            Mensajes_Usuario();
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        this.Session["encabemensa"] = "Señor Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
        //        Mensajes_Usuario();
        //        return false;
        //    }
        //}
        //private bool Validar_fechas_Menor()
        //{
        //    //FechaPA
        //    try
        //    {
        //        fechaIniPlaP = DateTime.Parse(TxtFechainiPla.Text);
        //        fechaPlaFinP = DateTime.Parse(TxtFechaplanfin.Text);
        //        if (fechaPlaFinP <= fechaIniPlaP)
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";
        //            this.Session["mensaje"] = "La fecha de fin de la campaña no puede ser menor y/o igual que la de inicio";
        //            Mensajes_Usuario();
        //            return false;
        //        }
        //        if (fechaIniPlaP < DateTime.Today)
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";
        //            this.Session["mensaje"] = "La fecha de inicio de ejecución no puede ser menor que la actual";
        //            Mensajes_Usuario();
        //            return false;
        //        }
        //        if (fechaPlaFinP < DateTime.Today)
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";
        //            this.Session["mensaje"] = "La fecha fin de ejecución no puede ser menor que la actual";
        //            Mensajes_Usuario();
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        this.Session["encabemensa"] = "Señor Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
        //        Mensajes_Usuario();
        //        return false;
        //    }
        //}

        //private void ObtenerClientes()
        //{
        //    DataTable dt = Planning.Get_ObtenerClienteconPlanning(this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            CmbClientes.DataSource = dt;
        //            CmbClientes.DataValueField = "Company_id";
        //            CmbClientes.DataTextField = "Company_Name";
        //            CmbClientes.DataBind();
        //        }
        //        else
        //        {
        //            this.Session["cssclass"] = "MensajesSupervisor";
        //            this.Session["encabemensa"] = "Sr. Usuario";
        //            this.Session["mensaje"] = "No existen Clientes actualmente con campañas activas.";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        private void Mensajes_Usuario()
        {
            PanelMensajesUsuario.CssClass = this.Session["cssclass"].ToString();
            Label1.Text = this.Session["encabemensa"].ToString();
            Label2.Text = this.Session["mensaje"].ToString();
            ModalPopupCanal.Show();
        }

        private void Mensajes_UsuarioElimina()
        {
            PanelMensajeUsuarioElimina.CssClass = this.Session["cssclass"].ToString();
            Label3.Text = this.Session["encabemensa"].ToString();
            Label4.Text = this.Session["mensaje"].ToString();
            ModalPopupMensajeEliminar.Show();
        }
        protected void BtnAceptaMensajeElimina_Click(object sender, EventArgs e)
        {
            ModalBuscar.Show();
        }

        //protected void cmbpresupuesto_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Limpiar_InformacionBasica();
        //    if (cmbpresupuesto.SelectedValue != "0")
        //    {
        //        BtnSavePlanning.Visible = true;
        //        DataTable dtnamePresupuesto = Presupuesto.Get_NamePlanning(cmbpresupuesto.SelectedValue);
        //        txtnamepresu.Text = dtnamePresupuesto.Rows[0]["namepreu"].ToString().ToUpperInvariant();
        //        //LblTxtCamp.Text = dtnamePresupuesto.Rows[0]["namepreu"].ToString().ToUpperInvariant();
        //        dtnamePresupuesto = null;

        //        //se llena el control con el nombre del cliente asociado al presupuesto seleccionado - Ing. Mauricio Ortiz
        //        DataTable dtCliente = Presupuesto.Get_ObtenerClientes(cmbpresupuesto.SelectedValue, 1);
        //        this.Session["company_id"] = dtCliente.Rows[0]["Company_id"].ToString().Trim();
        //        txtcliente.Text = dtCliente.Rows[0]["Company_Name"].ToString().ToUpperInvariant();
        //        //LblTxtClient.Text = dtCliente.Rows[0]["Company_Name"].ToString().ToUpperInvariant();
        //        dtCliente = null;

        //        //se llena el control con el nombre del servicio asociado al Presupuesto
        //        DataTable dtservicio = menu.Menu(cmbpresupuesto.SelectedValue);
        //        this.Session["cod_strategy"] = dtservicio.Rows[0]["cod_Strategy"].ToString().Trim();
        //        txtservice.Text = dtservicio.Rows[0]["Strategy_Name"].ToString().ToUpperInvariant();
        //        //LblTxtServicio.Text = dtservicio.Rows[0]["Strategy_Name"].ToString().ToUpperInvariant();
        //        dtservicio = null;

        //        //se llena controles de fecha de ejecución (inicio y fin) las cuales llegan por interface con EasyWin)
        //        DataTable dtfechas = Presupuesto.Get_OtenerFechasPlanning(cmbpresupuesto.SelectedValue, 1);
        //        TxtFechainiPla.Text = dtfechas.Rows[0]["Fec_iniPlanning"].ToString().Trim();
        //        TxtFechaplanfin.Text = dtfechas.Rows[0]["Fec_FinPlanning"].ToString().Trim();
        //        TxtFechaSolicitud.Text = Convert.ToString(DateTime.Today.AddDays(-1));
        //        TxtFechaEntrega.Text = Convert.ToString(Convert.ToDateTime(TxtFechaplanfin.Text).AddDays(1));
        //        TxtFechainipre.Text = Convert.ToString(DateTime.Today);
        //        TxtFechafinpre.Text = Convert.ToString(Convert.ToDateTime(TxtFechainiPla.Text).AddDays(-1));
        //        TxtFechaEntrega.Text = Convert.ToString(Convert.ToDateTime(TxtFechaplanfin.Text).AddDays(1));
        //        dtfechas = null;
        //        Activar_InformacionBasica();
        //    }
        //    else
        //    {
        //        BtnSavePlanning.Visible = false;
        //        Inactivar_InformacionBasica();
        //    }
        //}

        ////protected void TxtFechaSolicitud_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DateTime t = Convert.ToDateTime(TxtFechaSolicitud.Text);

        //    }
        //    catch
        //    {
        //        this.Session["encabemensa"] = "Señor Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
        //        TxtFechaSolicitud.Text = "";
        //        Mensajes_Usuario();
        //    }
        //}

        //protected void TxtFechaEntrega_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (TxtFechaEntrega.Text != "" && TxtFechaEntrega.Text != "__/__/____")
        //        {
        //            DateTime t = Convert.ToDateTime(TxtFechaEntrega.Text);

        //        }
        //    }
        //    catch
        //    {
        //        this.Session["encabemensa"] = "Señor Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
        //        TxtFechaEntrega.Text = "";
        //        Mensajes_Usuario();
        //    }
        //}

        //protected void TxtFechainipre_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (TxtFechainipre.Text != "" && TxtFechainipre.Text != "__/__/____")
        //        {
        //            DateTime t = Convert.ToDateTime(TxtFechainipre.Text);
        //        }
        //    }
        //    catch
        //    {
        //        this.Session["encabemensa"] = "Señor Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
        //        TxtFechainipre.Text = "";
        //        Mensajes_Usuario();
        //    }
        //}

        //protected void TxtFechafinpre_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (TxtFechafinpre.Text != "" && TxtFechafinpre.Text != "__/__/____")
        //        {
        //            DateTime t = Convert.ToDateTime(TxtFechafinpre.Text);

        //        }
        //    }
        //    catch
        //    {
        //        this.Session["encabemensa"] = "Señor Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
        //        TxtFechafinpre.Text = "";
        //        Mensajes_Usuario();
        //    }
        //}

        //protected void TxtFechainiPla_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (TxtFechainiPla.Text != "" && TxtFechainiPla.Text != "__/__/____")
        //        {
        //            DateTime t = Convert.ToDateTime(TxtFechainiPla.Text);
        //            TxtFechafinpre.Text = Convert.ToString(Convert.ToDateTime(TxtFechainiPla.Text).AddDays(-1));
        //            TxtFechaEntrega.Text = Convert.ToString(Convert.ToDateTime(TxtFechaplanfin.Text).AddDays(1));
        //        }
        //    }
        //    catch
        //    {
        //        this.Session["encabemensa"] = "Señor Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
        //        TxtFechainiPla.Text = "";
        //        Mensajes_Usuario();
        //    }
        //}

        //protected void TxtFechaplanfin_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (TxtFechaplanfin.Text != "" && TxtFechaplanfin.Text != "__/__/____")
        //        {
        //            DateTime t = Convert.ToDateTime(TxtFechaplanfin.Text);
        //            TxtFechafinpre.Text = Convert.ToString(Convert.ToDateTime(TxtFechainiPla.Text).AddDays(-1));
        //            TxtFechaEntrega.Text = Convert.ToString(Convert.ToDateTime(TxtFechaplanfin.Text).AddDays(1));
        //        }
        //    }
        //    catch
        //    {
        //        this.Session["encabemensa"] = "Señor Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
        //        TxtFechaplanfin.Text = "";
        //        Mensajes_Usuario();
        //    }
        //}


        //protected void BtnFinalizarPlanning_Click(object sender, EventArgs e)
        //{

        //    txtobj.Text = txtobj.Text.TrimStart();
        //    txtmanda.Text = txtmanda.Text.TrimStart();
        //    Ttxtmeca.Text = Ttxtmeca.Text.TrimStart();
        //    bool Continua = true;
        //    if (txtobj.Text == "" || txtmanda.Text == "" || Ttxtmeca.Text == "")
        //    {
        //        Continua = false;
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["encabemensa"] = "Sr. Usuario";
        //        this.Session["mensaje"] = "Es indispensable que ingresar toda la información (*)";
        //        Mensajes_Usuario();

        //    }
        //    if (Continua)
        //    {
        //        //Ejecutar Método para almacenar los objetivos de la Campaña. Ing. Mauricio Ortiz
        //        Planning.Get_RegisterObjPlanning(this.Session["id_planning"].ToString().Trim(), txtobj.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

        //        //Ejecutar Método para almacenar los Mandatorios de la Campaña. Ing. Mauricio Ortiz
        //        Planning.Get_RegisterMandatoryPlanning(this.Session["id_planning"].ToString().Trim(), txtmanda.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

        //        //Ejecutar Método para almacenar la Mecanica de la Actividad. Ing. Mauricio Ortiz
        //        Planning.Get_RegisterMecanicaPlanning(this.Session["id_planning"].ToString().Trim(), Ttxtmeca.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

        //        txtobj.Text = "";
        //        txtmanda.Text = "";
        //        Ttxtmeca.Text = "";



        //        TabContainerPlanning.ActiveTabIndex = 0;
        //        TabContainerPlanning.Tabs[0].Enabled = true;
        //        TabContainerPlanning.Tabs[1].Enabled = false;



        //        this.Session["encabemensa"] = "Creación de Planning";
        //        this.Session["cssclass"] = "MensajesSupConfirm";
        //        this.Session["mensaje"] = "Sr. Usuario, la campaña " + txtnamepresu.Text + " del cliente " + txtcliente.Text + " fue creada con éxito";
        //        Mensajes_Usuario();
        //        LlenaPresupuesto();
        //        Llena_Canal();
        //        Limpiar_InformacionBasica();
        //        btnSerch.Enabled = true;
        //    }
        //}

        //protected void BtnSavePlanning_Click(object sender, EventArgs e)
        //{
        //    btnSerch.Enabled = false;
        //    TxtFechaSolicitud.Text = TxtFechaSolicitud.Text.TrimStart();
        //    TxtFechaSolicitud.Text = TxtFechaSolicitud.Text.TrimStart();
        //    TxtFechaEntrega.Text = TxtFechaEntrega.Text.TrimStart();
        //    TxtFechainipre.Text = TxtFechainipre.Text.TrimStart();
        //    TxtFechafinpre.Text = TxtFechafinpre.Text.TrimStart();
        //    TxtFechainiPla.Text = TxtFechainiPla.Text.TrimStart();
        //    TxtFechaplanfin.Text = TxtFechaplanfin.Text.TrimStart();
        //    txtcontacto.Text = txtcontacto.Text.TrimStart();
        //    txtarea.Text = txtarea.Text.TrimStart();
        //    txtmail.Text = txtmail.Text.TrimStart();

        //    //////////////////
        //    int icod_Strategy = Convert.ToInt32(this.Session["cod_strategy"]);
        //    int personid = Convert.ToInt32(this.Session["personid"]);
        //    bool formato_compe = true;
        //    //if (rblformalevan.Items[0].Selected == true)
        //    //{
        //    //    formato_compe = false;
        //    //}
        //    //else
        //    //{
        //    //    formato_compe = true;
        //    //}

        //    bool Continua = true;
        //    if (cmbpresupuesto.SelectedValue == "0" || TxtFechaSolicitud.Text == "" || TxtFechaSolicitud.Text == "__/__/____" ||
        //        TxtFechaEntrega.Text == "" || TxtFechaEntrega.Text == "__/__/____" || TxtFechainipre.Text == "" ||
        //        TxtFechainipre.Text == "__/__/____" || TxtFechafinpre.Text == "" || TxtFechafinpre.Text == "__/__/____" ||
        //        TxtFechainiPla.Text == "" || TxtFechainiPla.Text == "__/__/____" || TxtFechaplanfin.Text == "" ||
        //        TxtFechaplanfin.Text == "__/__/____" || txtcontacto.Text == "" || txtarea.Text == "" || txtmail.Text == "")
        //    {
        //        Continua = false;
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["encabemensa"] = "Sr. Usuario";
        //        this.Session["mensaje"] = "Es indispensable que ingresar toda la información (*)";
        //        Mensajes_Usuario();
        //    }
        //    if (Continua)
        //    {
        //        Boolean sigue = Validad_FechaSolicitud();

        //        if (sigue)
        //        {
        //            sigue = Validar_Fecha_Entrega_Final();

        //            if (sigue)
        //            {
        //                sigue = Validad_Fecha_InicioPreproducción();

        //                if (sigue)
        //                {
        //                    sigue = Validad_Fecha_finPreproducción();

        //                    if (sigue)
        //                    {
        //                        sigue = Validar_fechas();

        //                        if (sigue)
        //                        {
        //                            sigue = Validar_fechas_Menor();

        //                            if (sigue)
        //                            {

        //                                // ejecutar metodo para insertar registro del planning . Ing. Mauricio Ortiz
        //                                // 30/07/2010 se adiciona id_planning concatenando numero de presupuesto y fecha actual. Ing. Mauricio Ortiz  
        //                                Planning.Save_Planning(cmbpresupuesto.SelectedValue + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year, txtnamepresu.Text, icod_Strategy, "No aplica",
        //                                   Convert.ToDateTime(TxtFechainiPla.Text), Convert.ToDateTime(TxtFechaplanfin.Text), Convert.ToDateTime(TxtFechaSolicitud.Text), Convert.ToDateTime(TxtFechainipre.Text),
        //                                Convert.ToDateTime(TxtFechafinpre.Text), "No aplica", Convert.ToDateTime(TxtFechaEntrega.Text), "No aplica", cmbpresupuesto.SelectedValue,
        //                                true, 1, this.Session["sUser"].ToString().Trim(),
        //                                DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);


        //                                Planning.obtenerPlanning(cmbpresupuesto.SelectedValue);
        //                                //Ejecutar método para obtener el número de planning generado 
        //                                DataTable dt = Planning.ObtenerIdPlanning(cmbpresupuesto.SelectedValue);
        //                                this.Session["presupuesto"] = cmbpresupuesto.SelectedValue;
        //                                splanning = dt.Rows[0]["Planning"].ToString().Trim();
        //                                this.Session["id_planning"] = dt.Rows[0]["Planning"].ToString().Trim();
        //                                txtnumpla.Text = dt.Rows[0]["Planning"].ToString().Trim();


        //                                ////Ejecutar Método para almancenar la asignación de mercadersitas a supervisores. Ing. Mauricio Ortiz
        //                                //for (int i = 0; i <= GvAsignados.Rows.Count - 1; i++)
        //                                //{
        //                                //    Planning.Get_Register_OperativosxSupervisor(iplanning,Convert.ToInt32(GvAsignados.Rows[i].Cells[1].Text),Convert.ToInt32(GvAsignados.Rows[i].Cells[3].Text), true, this.Session["sUser"].ToString(), DateTime.Now, this.Session["sUser"].ToString(), DateTime.Now);
        //                                //}

        //                                ////Ejecutar Método para almacenar los productos seleccionados para el planning. Ing. Mauricio Ortiz  
        //                                //for(int i=0; i<= GvAsignacionProductos.Rows.Count-1; i++)
        //                                //{
        //                                //    Planning.Get_Regitration_ProductosPlanning(iplanning, Convert.ToInt32(GvAsignacionProductos.Rows[i].Cells[1].Text),GvAsignacionProductos.Rows[i].Cells[2].Text,Convert.ToInt32(GvAsignacionProductos.Rows[i].Cells[3].Text),GvAsignacionProductos.Rows[i].Cells[4].Text,GvAsignacionProductos.Rows[i].Cells[5].Text,GvAsignacionProductos.Rows[i].Cells[6].Text,0, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
        //                                //}

        //                                ////Ejecutar Método para almancenar los productos del competidor para cada producto seleccionado. Ing. Mauricio Ortiz
        //                                //for (int i = 0; i <= GvAsignacionProducCompe.Rows.Count - 1; i++)
        //                                //{
        //                                //    Planning.Get_Register_Product_Compe(iplanning, Convert.ToInt32(GvAsignacionProducCompe.Rows[i].Cells[1].Text), GvAsignacionProducCompe.Rows[i].Cells[2].Text, GvAsignacionProducCompe.Rows[i].Cells[3].Text, GvAsignacionProducCompe.Rows[i].Cells[4].Text, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
        //                                //}

        //                                ////Ejecutar Método para almacenar las ciudades en las cuales se realizará el planning. Ing. Mauricio Ortiz
        //                                //for (int i = 0; i <= ListCity.Items.Count - 1; i++)
        //                                //{
        //                                //    if (ListCity.Items[i].Selected == true)
        //                                //    {
        //                                //        Planning.CrearCityPlanning(iplanning, ListCity.Items[i].Text, Convert.ToInt32(this.Session["company_id"]), "0", true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
        //                                //    }
        //                                //}

        //                                ////Ejecutar Método para almacenar los puntos de venta seleccionados para el planning. Ing. Mauricio Ortiz

        //                                //for (int i = 0; i <= GVPDV.Rows.Count - 1; i++)
        //                                //{
        //                                //    DataTable dtCodPDVExiste = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_SEARCH_PDV_MASIVO", 2, GVPDV.Rows[i].Cells[2].Text, GVPDV.Rows[i].Cells[7].Text, GVPDV.Rows[i].Cells[8].Text);
        //                                //    Planning.Get_registrarPDVPlanning(Convert.ToInt32(dtCodPDVExiste.Rows[0][0].ToString().Trim()), iplanning, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
        //                                //}

        //                                ////Ejecutar Método para insertar información de los formatos e indicadores que se usarán en el planning
        //                                ////encabezado
        //                                //for (int i = 0; i <= GvEncabezadoFormOculto.Rows.Count - 1; i++)
        //                                //{
        //                                //    Planning.Get_RegisterItemFormato(iplanning, Convert.ToInt32(GvEncabezadoFormOculto.Rows[i].Cells[4].Text), Convert.ToInt32(GvEncabezadoFormOculto.Rows[i].Cells[0].Text),1,5, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
        //                                //}
        //                                ////pie
        //                                //for (int i = 0; i <= GVPieFormOculto.Rows.Count - 1; i++)
        //                                //{
        //                                //    Planning.Get_RegisterItemFormato(iplanning, Convert.ToInt32(GVPieFormOculto.Rows[i].Cells[4].Text), Convert.ToInt32(GVPieFormOculto.Rows[i].Cells[0].Text), 3, 5, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
        //                                //}
        //                                //indicadores
        //                                //for (int k = 0; k <= gvindicadoresVentas.Rows.Count - 1; k++)
        //                                //{
        //                                //    int convert;
        //                                //    try
        //                                //    {
        //                                //        convert = Convert.ToInt32(gvindicadoresVentas.Rows[k].Cells[4].Text);
        //                                //    }
        //                                //    catch
        //                                //    {

        //                                //    }
        //                                //}

        //                                TabContainerPlanning.ActiveTabIndex = 1;
        //                                TabContainerPlanning.Tabs[0].Enabled = false;
        //                                TabContainerPlanning.Tabs[1].Enabled = true;
        //                                ObtenerClientes();




        //                                //cmbpresupuesto.SelectedValue = "0";
        //                                //BotonesCancelar();
        //                                //Limpiar_InformacionBasica();
        //                                //Inactivar_InformacionBasica();

        //                                //DataTable dtAsigna_Productos_Temp = new DataTable();
        //                                //dtAsigna_Productos_Temp.Columns.Add("id_Producto", typeof(Int32));
        //                                //dtAsigna_Productos_Temp.Columns.Add("id_ProductCategory", typeof(Int32));
        //                                //dtAsigna_Productos_Temp.Columns.Add("id_Brand", typeof(Int32));
        //                                //dtAsigna_Productos_Temp.Columns.Add("id_SubBrand", typeof(Int32));
        //                                //dtAsigna_Productos_Temp.Columns.Add("Product_caracte", typeof(String));
        //                                //dtAsigna_Productos_Temp.Columns.Add("Product_benefic", typeof(String));
        //                                //this.Session["dtAsigna_Productos_Temp"] = dtAsigna_Productos_Temp;

        //                                //GvAsignacionProductos.DataSource = dtAsigna_Productos_Temp;
        //                                //GvAsignacionProductos.DataBind();

        //                                //DataTable dtAsigna_ProductosCompe_Temp = new DataTable();
        //                                //dtAsigna_ProductosCompe_Temp.Columns.Add("id_Producto", typeof(Int32));
        //                                //dtAsigna_ProductosCompe_Temp.Columns.Add("name_producCompe", typeof(String));
        //                                //dtAsigna_ProductosCompe_Temp.Columns.Add("Brand_Compe", typeof(String));
        //                                //dtAsigna_ProductosCompe_Temp.Columns.Add("ProductCompe_manufacturer", typeof(String));
        //                                //this.Session["dtAsigna_ProductosCompe_Temp"] = dtAsigna_ProductosCompe_Temp;

        //                                //GvAsignacionProducCompe.DataSource = dtAsigna_ProductosCompe_Temp;
        //                                //GvAsignacionProducCompe.DataBind();

        //                                //TabContainerPlanning.Tabs[0].Enabled = true;
        //                                //TabContainerPlanning.Tabs[1].Enabled = false;
        //                                //TabContainerPlanning.Tabs[2].Enabled = false;
        //                                //TabContainerPlanning.Tabs[3].Enabled = false;
        //                                //TabContainerPlanning.Tabs[4].Enabled = false;
        //                                //TabContainerPlanning.Tabs[5].Enabled = false;

        //                                //TabContainerPlanning.ActiveTabIndex = 0;

        //                                //ifcarga.Attributes["src"] = "";


        //                                // enviar correo a todos los supervisores del presupuesto seleccionado informando
        //                            }
        //                            //////////////////////////////
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        protected void BtnSaveInfo_Click(object sender, EventArgs e)
        {

            bool Continua = true;
            if (cmbPlanning.SelectedValue == "0" ||
                FileUpPDV.PostedFile == null || FileUpPDV.PostedFile.ContentLength <= 0 ||
                ChkListCanal.SelectedValue == "" || RbtnListReport.SelectedIndex == -1 || CmbSelMes.Text == "0")
            {
                Continua = false;
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["mensaje"] = "Debe registrar toda la información para continuar";
                Mensajes_Usuario();

            }
            if (Continua)
            {
                if ((FileUpPDV.PostedFile != null) && (FileUpPDV.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpPDV.PostedFile.FileName);
                    
                    fn = fn.Replace("á", "a");
                    fn = fn.Replace("é", "e");
                    fn = fn.Replace("í", "i");
                    fn = fn.Replace("ó", "o");
                    fn = fn.Replace("ú", "u");
                    fn = fn.Replace("Á", "A");
                    fn = fn.Replace("É", "E");
                    fn = fn.Replace("Í", "I");
                    fn = fn.Replace("Ó", "O");
                    fn = fn.Replace("Ú", "U");

                    string extension = System.IO.Path.GetExtension(FileUpPDV.PostedFile.FileName);
                    string SaveLocation = Server.MapPath(@"~\Pages\Modulos\Cliente\Informes") + "\\" +
                       RbtnCliente.Items[0].Text + "_" + fn;
                    this.Session["SaveLocation"] = SaveLocation;
                    this.Session["FileName"] = fn;

                    string sinExternsion = fn;
                    sinExternsion = sinExternsion.Replace(extension, "");
                    this.Session["FileNamesinEXT"] = sinExternsion;
                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpPDV.FileName.ToLower().EndsWith(".xls") || FileUpPDV.FileName.ToLower().EndsWith(".xlsx") ||
                            FileUpPDV.FileName.ToLower().EndsWith(".ppt") || FileUpPDV.FileName.ToLower().EndsWith(".pptx") ||
                            FileUpPDV.FileName.ToLower().EndsWith(".doc") || FileUpPDV.FileName.ToLower().EndsWith(".docx") ||
                            FileUpPDV.FileName.ToLower().EndsWith(".pdf") || FileUpPDV.FileName.ToLower().EndsWith(".pps") ||
                            FileUpPDV.FileName.ToLower().EndsWith(".ppsx"))
                        {
                            if (System.IO.File.Exists(Server.MapPath(@"~\Pages\Modulos\Cliente\Informes") + "\\" +
                               RbtnCliente.Items[0].Text + "_" + this.Session["FileName"]))
                            {
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["mensaje"] = "El archivo que intenta grabar ya existe, por favor verifique";
                                Mensajes_Usuario();

                            }
                            else
                            {
                                FileUpPDV.PostedFile.SaveAs(SaveLocation);
                                this.Session["Canales"] = "";
                                TxtNameReport.Text = SaveLocation;
                                for (int i = 0; i <= ChkListCanal.Items.Count - 1; i++)
                                {
                                    if (ChkListCanal.Items[i].Selected == true)
                                    {
                                        this.Session["Canales"] = this.Session["Canales"] + " " + ChkListCanal.Items[i].Text;
                                    }
                                }
                                LblMensajeConfirm.Text = "Realmente desea almacenar el informe " + this.Session["FileName"] +
                                    " en el canal : " + this.Session["Canales"].ToString().Trim() + " en el reporte : " +
                                    RbtnListReport.SelectedItem.Text + "?";
                                ModalConfirmacion.Show();
                            }
                        }
                        else
                        {
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["mensaje"] = "Solo se permite cargar archivos para Excel, Word, PowerPoint y pdf";
                            Mensajes_Usuario();
                        }
                    }
                }
            }
        }
        protected void BtnSiConfirma_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(Server.MapPath(@"~\Pages\Modulos\Cliente\Informes") + "\\" +
                         RbtnCliente.Items[0].Text + "_" + this.Session["FileName"]))
                {
                    this.Session["encabemensa"] = "Carga de Archivo";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "El Archivo : " + this.Session["FileName"].ToString().Trim() + " Se ha guardado con éxito";
                    Mensajes_Usuario();

                    this.Session["Canales"] = "";
                    TxtNameReport.Text = this.Session["SaveLocation"].ToString().Trim();



                    for (int i = 0; i <= ChkListCanal.Items.Count - 1; i++)
                    {
                        if (ChkListCanal.Items[i].Selected == true)
                        {

                            DataTable dtDuplicado = null;
                            if (RbtnSubcanales.Items.Count == 0)
                            {
                                 dtDuplicado = Planning.Get_DuplicadosinfoPlanning(ChkListCanal.Items[i].Value, RbtnCliente.Items[0].Text + "_" + this.Session["FileName"].ToString().Trim());
                            }
                            else
                            {
                                  dtDuplicado = Planning.Get_DuplicadosinfoPlanning_consubcanal(ChkListCanal.Items[i].Value, RbtnCliente.Items[0].Text + "_" + this.Session["FileName"].ToString().Trim(),RbtnSubcanales.SelectedItem.Value);
                            }

                            
                            // ejecuta método para controlar que no se duplique el reporte por canal el oprimir el boton SI
                           
                            this.Session["canal"] = ChkListCanal.Items[i].Value;
                            if (dtDuplicado != null)
                            {
                                if (dtDuplicado.Rows.Count == 0)
                                {

                                    this.Session["Canales"] = this.Session["Canales"] + " " + ChkListCanal.Items[i].Text;
                                    if (RbtnSubcanales.Items.Count == 0)
                                    {
                                        Planning.Get_InsertaInfoReportes("sin_subcanal",cmbPlanning.SelectedValue,
                                            Convert.ToInt32(RbtnCliente.Items[0].Value),
                                            Convert.ToInt32(RbtnListReport.SelectedValue),
                                            ChkListCanal.Items[i].Value,"0","0",Convert.ToInt32(rblservice.Items[0].Value),
                                            this.Session["pais"].ToString().Trim(),
                                            "No aplica",
                                            this.Session["FileNamesinEXT"].ToString().Trim(),
                                           RbtnCliente.Items[0].Text + "_" + this.Session["FileName"].ToString().Trim(), CmbSelMes.Text + "/" + DateTime.Now.Year, true, this.Session["sUser"].ToString().Trim(),
                                            DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                    }
                                    else
                                    {
                                        string ValSubnivel = "";
                                        if (RbtnSubNivel.Items.Count > 0)
                                        {
                                            ValSubnivel = RbtnSubNivel.SelectedItem.Value;
                                        }
                                        else
                                        {
                                            ValSubnivel = "0";
                                        }


                                       // oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CARGAREPORTESCAMPAÑAS", "con_subcanal", cmbPlanning.SelectedValue, Convert.ToInt32(RbtnCliente.Items[0].Value),  Convert.ToInt32(RbtnListReport.SelectedValue), ChkListCanal.Items[i].Value, RbtnSubcanales.SelectedItem.Value, ValSubnivel, Convert.ToInt32(rblservice.Items[0].Value),  this.Session["pais"].ToString().Trim(), "No aplica", this.Session["FileNamesinEXT"].ToString().Trim(),  RbtnCliente.Items[0].Text + "_" + this.Session["FileName"].ToString().Trim(), CmbSelMes.Text + "/" + DateTime.Now.Year, true,  this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                                        Planning.Get_InsertaInfoReportes("con_subcanal", cmbPlanning.SelectedValue,
                                           Convert.ToInt32(RbtnCliente.Items[0].Value),
                                           Convert.ToInt32(RbtnListReport.SelectedValue),
                                           ChkListCanal.Items[i].Value, RbtnSubcanales.SelectedItem.Value, ValSubnivel, Convert.ToInt32(rblservice.Items[0].Value),
                                           this.Session["pais"].ToString().Trim(),
                                           "No aplica",
                                           this.Session["FileNamesinEXT"].ToString().Trim(),
                                          RbtnCliente.Items[0].Text + "_" + this.Session["FileName"].ToString().Trim(), CmbSelMes.Text + "/" + DateTime.Now.Year, true, this.Session["sUser"].ToString().Trim(),
                                           DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                    }
                                    
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_INSERTARREPORTXMAIL", this.Session["sUser"].ToString().Trim());



                                    if (Chkcityorig.SelectedIndex != -1)
                                    {
                                        DataTable dtidinfoplanning = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENERIDMAXINFOPLANNING");
                                        int id_infoplanningmax = Convert.ToInt32(dtidinfoplanning.Rows[0]["id_infoplanning"].ToString().Trim());

                                        for (int registracity = 0; registracity <= Chkcityorig.Items.Count - 1; registracity++)
                                        {
                                            if (Chkcityorig.Items[registracity].Selected == true)
                                            {
                                                oInfo_Planning_City.RegistrarCityInfoPlanning(id_infoplanningmax, Convert.ToInt64(Chkcityorig.Items[registracity].Value), Chkcityorig.Items[registracity].Value, true, this.Session["sUser"].ToString().Trim(),
                                            DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    llenaConsultaInformesTotal();
                    CmbSelMes.Text = "0";

                    #region codigo comentariado pasa a funcion
                    //Por Solicitud de Alicorp se inactiva el envio de correos hasta nueva orden Ing. Carlos Hernandez
                    //envio de correo al cliente con información de los reportes cargados
                    //try
                    //{
                    //    //De Acuerdo a Requerimiento de5 Alicorp solicito que los correos se enviaran x Canal, Servicio Ing. Carlos Alberto Hernández Rincón
                    //    DataTable dtbodymail = null;
                    //    dtbodymail = (DataTable)this.Session["Dtmail"];




                    //    DataTable dtcorreo = Planning.Get_Obtener_Datos_Cliente(Convert.ToInt32(CmbClientes.SelectedItem.Value), this.Session["canal"].ToString(), Convert.ToInt32(rblservice.SelectedValue));
                    //    if (dtbodymail.Rows.Count > 0)
                    //        for (int j = 0; j <= dtbodymail.Rows.Count - 1; j++)
                    //        {
                    //    {
                    //    if (dtcorreo != null)
                    //    {



                    //        if (dtcorreo.Rows.Count > 0)
                    //        {
                    //            for (int envio = 0; envio <= dtcorreo.Rows.Count - 1; envio++)
                    //            {
                    //                Enviomail oEnviomail = new Enviomail();
                    //                EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Solicitud_Clave");
                    //                Mails oMail = new Mails();
                    //                oMail.Server = oeEmail.MailServer;
                    //                oMail.From = oeEmail.MailFrom;
                    //                oMail.To = "chernandez.col@lucky.com.pe";
                    //                    //dtcorreo.Rows[envio]["Person_Email"].ToString().Trim();
                    //                Label lblcanal = new Label();
                    //                GridView gvbody = new GridView();
                    //                gvbody.Visible = true;
                    //                lblcanal.Visible = true;
                    //                oMail.Subject = "Lucky SAC _ Informe : " + this.Session["FileNamesinEXT"].ToString().Trim();
                    //                string[] textArray1 = new string[] { "Señor(a) " , dtcorreo.Rows[envio]["Nombres"].ToString().Trim(), "<br>" ,
                    //                        "Lo invitamos a acceder al sistema de consultas en línea . ",
                    //                        lblcanal.Text="Canal :" + dtbodymail.Rows[j]["Canales"].ToString().Trim(),
                    //                        "al cual puede acceder a traves del siguiente link: ",
                    //                        "<a href=" + "http://sige.lucky.com.pe" + ">..::Xplora</a>" ,"<br><br>" ,
                    //                        "Encontrará a su disposición la siguiente información:" , "<br><br>" ,
                    //                        "Tipo de Reporte       : " , RbtnListReport.SelectedItem.Text, "<br><br>" ,                                            
                    //                        "Canales de aplicación : " , this.Session["Canales"].ToString().Trim() ,  "<br><br>" ,                                            
                    //                        "Nombre de Archivo     : " , this.Session["FileName"].ToString().Trim(), "<br>" ,
                    //                        "Para información adicional comuniquese con nosotros. Quedamos atentos a sus comentarios", "<br><br>" ,"<br><br>" ,                                            
                    //                        "Cordialmente", "<br>", "Administrador Xplora"


                    //                };







                    //                        oMail.Body = string.Concat(textArray1); 
                    //                oMail.BodyFormat = "HTML";
                    //                oMail.send();
                    //                oMail = null;
                    //                oeEmail = null;
                    //                oEnviomail = null;
                    //            }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    this.Session["cssclass"] = "MensajesSupervisor";
                    //    this.Session["encabemensa"] = "Sr. Usuario";
                    //    this.Session["mensaje"] = "Se creo el reporte pero no fue posible enviar aviso al cliente.";
                    //    Mensajes_Usuario();
                    //}
                    #endregion
                }
            }
            catch(Exception ex)
            {
                System.IO.File.Delete(Server.MapPath(@"~\Pages\Modulos\Cliente\Informes") + "\\" +
                     RbtnCliente.Items[0].Text + "_" + this.Session["FileName"]);

                DataSet dsEliminar = Planning.Get_EliminarInfoPlanning(RbtnCliente.Items[0].Text + "_" + this.Session["FileName"].ToString().Trim(),
               this.Session["canal"].ToString());


                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        protected void BtnNoConfirma_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(Server.MapPath(@"~\Pages\Modulos\Cliente\Informes") + "\\" +
                   RbtnCliente.Items[0].Text + "_" + this.Session["FileName"]))
            {
                System.IO.File.Delete(Server.MapPath(@"~\Pages\Modulos\Cliente\Informes") + "\\" +
                      RbtnCliente.Items[0].Text + "_" + this.Session["FileName"]);
            }

        }

        //protected void BtnCaract_Click(object sender, EventArgs e)
        //{
        //    txtobj.Text = txtobj.Text.TrimStart();
        //    txtmanda.Text = txtmanda.Text.TrimStart();
        //    Ttxtmeca.Text = Ttxtmeca.Text.TrimStart();
        //    bool Continua = true;
        //    if (txtobj.Text == "" || txtmanda.Text == "" || Ttxtmeca.Text == "")
        //    {
        //        Continua = false;
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["encabemensa"] = "Sr. Usuario";
        //        this.Session["mensaje"] = "Es indispensable que ingresar toda la información (*)";
        //        Mensajes_Usuario();

        //    }
        //    if (Continua)
        //    {
        //        //Ejecutar Método para almacenar los objetivos de la Campaña. Ing. Mauricio Ortiz
        //        Planning.Get_RegisterObjPlanning(this.Session["id_planning"].ToString().Trim(), txtobj.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

        //        //Ejecutar Método para almacenar los Mandatorios de la Campaña. Ing. Mauricio Ortiz
        //        Planning.Get_RegisterMandatoryPlanning(this.Session["id_planning"].ToString().Trim(), txtmanda.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

        //        //Ejecutar Método para almacenar la Mecanica de la Actividad. Ing. Mauricio Ortiz
        //        Planning.Get_RegisterMecanicaPlanning(this.Session["id_planning"].ToString().Trim(), Ttxtmeca.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

        //        //TabContainerPlanning.ActiveTabIndex = 2;
        //        //TabContainerPlanning.Tabs[0].Enabled = false;
        //        //TabContainerPlanning.Tabs[1].Enabled = false;
        //        //TabContainerPlanning.Tabs[2].Enabled = true;
        //        //BtnSaveInfo.Visible = true;
        //        txtobj.Text = "";
        //        txtmanda.Text = "";
        //        Ttxtmeca.Text = "";
        //    }
        //}

        protected void BtnCrearPlanning_Click(object sender, EventArgs e)
        {
            LblTitCargarArchivo.Text = "Crear nuevas Campañas";
            //InfoPlanningBasico.Style.Value = "Display:Block;";
            PConsulta.Style.Value = "Display:none;";

            btnSerch.CssClass = "buttonPlan";
            btnSerch.Style.Value = "background-image: url('../../images/bg_btn_a.png');";
            btnSerch.Enabled = true;
            BtnCrearPlanning.Enabled = false;
            //BtnCrearPlanning.Style.Value = "background-image: url('../../images/bg_btn_ahover.png');";
        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            Obtener_CampañasxUsuario();

            //Limpiar_InformacionBasica();
            //cmbpresupuesto.Text = "0";

            //BtnSavePlanning.Visible = false;            
            //InfoPlanningBasico.Style.Value = "Display:none;";
            PConsulta.Style.Value = "Display:Block;";

            //btnSerch.CssClass = "buttonPlan";
            //btnSerch.Style.Value = "background-image: url('../../images/bg_btn_a.png');";
            //BtnCrearPlanning.Enabled = true;

        }

        // ing. Mauricio Ortiz -- 08/10/2010 ya no aplica de acuerdo al nuevo planteamiento para la seleccion de la campaña
        //protected void CmbClientes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataTable dt = Planning.Get_ObtenerCampañasxCliente(Convert.ToInt32(CmbClientes.SelectedValue));

        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            cmbPlanning.DataSource = dt;
        //            cmbPlanning.DataValueField = "id_planning";
        //            cmbPlanning.DataTextField = "Planning_name";

        //            cmbPlanning.DataBind();
        //            cmbPlanning.Items.Insert(0, new ListItem("--Seleccione--", "0"));
        //        }
        //        else
        //        {
        //            this.Session["cssclass"] = "MensajesSupervisor";
        //            this.Session["encabemensa"] = "Sr. Usuario";
        //            this.Session["mensaje"] = "No existen campañas activas para el cliente seleccionado.";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        protected void gvLink_informes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvLink_informes.PageIndex = e.NewPageIndex;
                llenaConsultaInformes();
                ModalBuscar.Show();
            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void CmbClienteBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblBuscarSelAño.Visible = false;
            CmbBuscarSelAño.Visible = false;
            CmbBuscarSelMes.Text = "0";
            CmbBuscarSelAño.Text = "0";
            llenaConsultaInformes();
            ModalBuscar.Show();
        }
        protected void CmbCanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblBuscarSelAño.Visible = false;
            CmbBuscarSelAño.Visible = false;
            CmbBuscarSelMes.Text = "0";
            CmbBuscarSelAño.Text = "0";
            llenaConsultaInformes();
            ModalBuscar.Show();
        }
        protected void CmbBuscarSelMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbBuscarSelAño.Text = "0";
            if (CmbBuscarSelMes.Text != "0")
            {
                LblBuscarSelAño.Visible = true;
                CmbBuscarSelAño.Visible = true;
            }
            else
            {
                LblBuscarSelAño.Visible = false;
                CmbBuscarSelAño.Visible = false;
            }
            gvLink_informes.EmptyDataText = "Seleccione el año para ver resultados";
            gvLink_informes.DataBind();
            ModalBuscar.Show();
        }

        protected void CmbBuscarSelAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaConsultaInformes();
            ModalBuscar.Show();
        }
        protected void gvLink_informes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblTxtConfirmaElimina.Text = "Realmente desea eliminar el informe " + gvLink_informes.SelectedRow.Cells[2].Text.ToString().Trim();
            ModalConfirmaElimina.Show();



        }
        /// <summary>
        /// Funcion para Elinar Informes Cargados
        /// Creada por: Ing. Mauricio Ortiz
        /// Fecha:?
        /// Modificada por: Ing. Carlos Alberto Hernández Rincón
        /// Fecha Modificación: 19/08/2010
        /// Descripción Modificación: Segun requerimiento de Operaciones por el cual no se estaban eliminando registros 
        ///   se verifico la funcion y el problema estaba en las variables de seccion para obtener canal se ajusto esta parte y se
        ///   creo un metodo para obtrener el nombre del del informe y eliminarlo fisicamente del servidor.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void BtnSiConfirmaElimina_Click(object sender, EventArgs e)
        {
            DataTable dtefile = null;
            string file_inserver;
            string ruta;

            for (int i = 0; i <= CmbCanal.Items.Count - 1; i++)
            {
                if (CmbCanal.Items[i].Text == gvLink_informes.SelectedRow.Cells[1].Text.ToString().Trim())
                {
                    sCod_channel = CmbCanal.Items[i].Value;
                    i = CmbCanal.Items.Count - 1;
                }
            }


            if (CmbCanal.SelectedValue != "0" && CmbClienteBuscar.SelectedValue != "0") //Verificamos que seleccione un cliente y un Canal Ing. Carlos Hernandez
            {
                //Invocamos metodo para buscar archivo a Eliminar fisicamente) Ing. Carlos Hernández
                dtefile = Planning.Get_EliminarArchivoServer(CmbCanal.SelectedValue, gvLink_informes.SelectedRow.Cells[3].Text.ToString().Trim(), Convert.ToInt32(CmbClienteBuscar.SelectedValue));
            }
            else
            {
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["encabemensa"] = "Señor Usuario";
                Label1.Text = this.Session["encabemensa"].ToString();

                this.Session["mensaje"] = "Debe seleccionar un Cliente y un Canal para poder eliminar el informe";
                Mensajes_UsuarioElimina();
                ObtenerClientesBuscar();
                Llena_CanalBuscar();
                llenaConsultaInformes();
                return;



            }
            if (dtefile.Rows.Count > 0)
            {


                file_inserver = dtefile.Rows[0]["Archivo"].ToString(); //Obtengo Nombre del archivo de la BD  Ing. Carlos Hernández
                ruta = Server.MapPath(@"~\Pages\Modulos\Cliente\Informes"); //Ruta
                File.Delete(ruta + "\\" + file_inserver);//Eliminamos archivo del Servidor Ing. Carlos Hernández

                ModalBuscar.Hide();

                DataSet dsEliminar = Planning.Get_EliminarInfoPlanning(gvLink_informes.SelectedRow.Cells[3].Text.ToString().Trim(),
               CmbCanal.SelectedValue);
                this.Session["encabemensa"] = "Señor Usuario";
                Label1.Text = this.Session["encabemensa"].ToString();
                this.Session["cssclass"] = "MensajesSupConfirm";
                this.Session["mensaje"] = "Se ha Eliminado el Informe " + " " + gvLink_informes.SelectedRow.Cells[3].Text.ToString().Trim() + " " + "con Exito";
                Mensajes_Usuario();
                ObtenerClientesBuscar();
                Llena_CanalBuscar();
                llenaConsultaInformes();


            }
            else
            {
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["encabemensa"] = "Señor Usuario";
                Label1.Text = this.Session["encabemensa"].ToString();

                this.Session["mensaje"] = "El archivo tiene caracteres especiales.....contacte con el area de TI";
                Mensajes_UsuarioElimina();
                ObtenerClientesBuscar();
                Llena_CanalBuscar();
                llenaConsultaInformes();
                return;
            }
            



        }

        protected void BtnNoConfirmaElimina_Click(object sender, EventArgs e)
        {
            ModalBuscar.Show();
        }

        protected void BtnCOlv_Click(object sender, ImageClickEventArgs e)
        {
            CmbClienteBuscar.SelectedIndex = 0;
            CmbCanal.Text = "0";
            llenaConsultaInformesTotal();
        }

        protected void cmbPlanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlanning.SelectedValue != "0")
            {
                DataTable dt = (DataTable)this.Session["CampañasXusuario"];
                string idcompañia = dt.Rows[(Convert.ToInt32(cmbPlanning.SelectedIndex.ToString().Trim()) - 1)]["Company_id"].ToString().Trim();
                string compañia = dt.Rows[(Convert.ToInt32(cmbPlanning.SelectedIndex.ToString().Trim()) - 1)]["Company_Name"].ToString().Trim();
                string idservicio = dt.Rows[(Convert.ToInt32(cmbPlanning.SelectedIndex.ToString().Trim()) - 1)]["cod_Strategy"].ToString().Trim();
                string servicio = dt.Rows[(Convert.ToInt32(cmbPlanning.SelectedIndex.ToString().Trim()) - 1)]["Strategy_Name"].ToString().Trim();
                string idcanal = dt.Rows[(Convert.ToInt32(cmbPlanning.SelectedIndex.ToString().Trim()) - 1)]["Planning_CodChannel"].ToString().Trim();
                string canal = dt.Rows[(Convert.ToInt32(cmbPlanning.SelectedIndex.ToString().Trim()) - 1)]["Channel_Name"].ToString().Trim();
                this.Session["pais"] = dt.Rows[(Convert.ToInt32(cmbPlanning.SelectedIndex.ToString().Trim()) - 1)]["cod_country"].ToString().Trim();

                RbtnCliente.Items.Clear();
                RbtnCliente.Items.Insert(0, new ListItem(compañia, idcompañia));
                RbtnCliente.Items[0].Selected = true;


                rblservice.Items.Clear();
                rblservice.Items.Insert(0, new ListItem(servicio, idservicio));
                rblservice.Items[0].Selected = true;

                ChkListCanal.Items.Clear();
                ChkListCanal.Items.Insert(0, new ListItem(canal, idcanal));
                ChkListCanal.Items[0].Selected = true;

                RbtnListReport.Items.Clear();
                //se llena ahora desde el seleccionar reporte . Ing. Mauricio Ortiz 28/10/2010
                //llenarCiudades(); 
                llenarsubcanales();
                RbtnSubNivel.Items.Clear();
                LblSubnivel.Visible = false;
                if (RbtnSubcanales.Items.Count == 0)
                {
                    LLenar_Reporte();
                }
               
                Chkcityorig.Items.Clear();
                Chkcityorig.Enabled = true;
                BtnAllCobertura.Visible = false;
                BtnNone.Visible = false;
                CmbSelMes.SelectedIndex = -1;
                

            }
            else
            {
                RbtnCliente.Items.Clear();
                rblservice.Items.Clear();
                ChkListCanal.Items.Clear();
                RbtnListReport.Items.Clear();
                Chkcityorig.Items.Clear();
                Chkcityorig.Enabled = true;
                BtnAllCobertura.Visible = false;
                BtnNone.Visible = false;
                CmbSelMes.SelectedIndex = -1;
                RbtnSubcanales.Items.Clear();
                LblSubCanal.Visible = false;
                RbtnSubNivel.Items.Clear();
                LblSubnivel.Visible = false;
            }



        }

        protected void RbtnListReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarCiudades();
            
            
        }

        protected void BtnAllCobertura_Click(object sender, EventArgs e)
        {
             for (int i = 0; i <= Chkcityorig.Items.Count - 1; i++)
            {
                Chkcityorig.Items[i].Selected = true;
                Chkcityorig.Enabled = false;

            }            
        }

        protected void BtnNone_Click(object sender, EventArgs e)
        {
            Chkcityorig.ClearSelection();
            Chkcityorig.Enabled = true;
        }

        protected void RbtnSubcanales_SelectedIndexChanged(object sender, EventArgs e)
        {
            RbtnListReport.Items.Clear();
            llenarsubniveles();
            if (RbtnSubNivel.Items.Count == 0)
            {
                LLenar_Reporte();
            }           
        }

        protected void RbtnSubNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LLenar_Reporte();
        }
    }
}

