using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Lucky.Business.Common.Application;
using Lucky.CFG;
using Lucky.CFG.Messenger;
using Lucky.Data;
using System.Data.SqlClient; 
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Application.Interfaces;
using SIGE.Facade_Interface_EasyWin;
using SIGE.Facade_Menu_strategy;
using SIGE.Facade_Proceso_Planning;
using SIGE.Facade_Procesos_Administrativos;
using SIGE.Facade_Search;


namespace SIGE.Pages.Modulos.Planning
{
    public partial class AsignacionesxCanal : System.Web.UI.Page
    {
        #region Zona de Declaracion de Variables
        
        DataSet ds = null;
        Conexion oCoon = new Conexion();
        public string servicio;
        public string itemser;
        bool estado;
        string estadorblistipo;
        int idpoint;
        int ContadorGeneral = 0;
        int ContadorDetallado = 0;
        int contadorPhotoCom = 0;
        int icodstrategy;
        int istrategy;
        int mostrarnota=0;        
        DateTime FechaFoto;
        int maxcountcompe = 0;
        private Photographs_Service oPhotographs_Service = new Photographs_Service();
        private Competition__Information oCompetition__Information = new Competition__Information();
        Facade_Menu_strategy.Facade_MPlanning menu = new Facade_MPlanning();
        Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE Presupuesto = new SIGE.Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE();
        Facade_Proceso_Planning.Facade_Proceso_Planning Planning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        Facade_Search.Facade_Search Busquedas = new SIGE.Facade_Search.Facade_Search();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos PAmin = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Operativo.Facade_Proceso_Operativo Operaciones = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();

        #endregion
        
        #region Inicializacion de controles
        private void Inicializar_Radibutonlist()
        {
            rblistipo.Items[0].Selected = true;
            rblistipo.Items[1].Selected = false;
        }
        private void InicializarTabs()
        {
            TabContainer1.Visible = true;
            TabContainer1.ActiveTabIndex = 0;            
            BinData();
            CampañasCliente();
            LugaresFotos();
            cmbactividades();
            Photoactividades();
        }
        #endregion

        # region Zona de LLenado de combos        
        private void cmbclientes()
        {
            DataTable dtcliente = null;
            dtcliente = Planning.Get_ObtenerClientesxcampañasActivas();
            cmbcliente.DataSource = dtcliente;
            cmbcliente.DataValueField = "comapny";
            cmbcliente.DataTextField = "name_company";
            cmbcliente.DataBind();
            this.Session["dtClientesxcampañasActivas"] = dtcliente;
        }
        private void CampañasCliente()
        {
            DataTable dtcampa = null;
            dtcampa = Planning.Get_ObtenerCampañasCliente(Convert.ToInt32(cmbcliente.SelectedValue), Convert.ToInt32(cmbcanal.SelectedValue));
            this.Session["ipplanning"] = dtcampa.Rows[0]["id_planning"].ToString();
            cmbcampa.DataSource = dtcampa;
            cmbcampa.DataValueField = "id_planning";
            cmbcampa.DataTextField = "Planning_Name";
            cmbcampa.DataBind();
        }
        private void cmbactividades()
        {
            DataTable dtactivi = Planning.Get_Obtener_Actividades_Comercio(Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.SelectedValue);            
            GVActividades.DataSource = dtactivi;
            GVActividades.DataBind();
            if (dtactivi != null)
            {
                if (dtactivi.Rows.Count > 0)
                {
                    for (int i = 0; i <= GVActividades.Rows.Count - 1; i++)
                    {
                        ((CheckBox)GVActividades.Rows[i].Cells[0].FindControl("ChkSelActividad")).Checked = Convert.ToBoolean(dtactivi.Rows[i]["cinfo_Principal"].ToString().Trim());
                        ((TextBox)GVActividades.Rows[i].Cells[0].FindControl("TxtObservacion")).Text = dtactivi.Rows[i]["cinfo_Comment_Observa"].ToString().Trim();                        
                    }
                }
            }
        }
        private void Photoactividades()
        {
            DataTable dtactivi = Planning.Get_Obtener_PhotoActividades_Comercio(Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.SelectedValue);
            
            gvactivi.DataSource = dtactivi;
            gvactivi.DataBind();
            if (dtactivi != null)
            {
                if (dtactivi.Rows.Count > 0)
                {
                    for (int i = 0; i <= gvactivi.Rows.Count - 1; i++)
                    {
                        string fn = System.IO.Path.GetFileName(dtactivi.Rows[i]["PhotoCI_PathName"].ToString().Trim());
                        ((Image)gvactivi.Rows[i].Cells[0].FindControl("ImgPhotoa")).ImageUrl = "~/Pages/Modulos/Operativo/PictureComercio/" + fn;                        
                        ((TextBox)gvactivi.Rows[i].Cells[0].FindControl("txtcomentario")).Text = dtactivi.Rows[i]["PhotoCI_Observacion"].ToString().Trim();
                    }
                }
            }
        }
        private void LugaresFotos()
        {
            //21/10/2010 Ing. Mauricio Ortiz se modifica temporalmente para no generar errores en seguimiento_planning.aspx
            // Esta funcionalidad no aplica por el momento 
            DataTable dtlufotos = null;           
            //dtlufotos = Planning.Get_ObtenerPDVPlanning(Convert.ToInt32(cmbcampa.SelectedValue));
            dtlufotos = Planning.Get_ObtenerPDVPlanning(cmbcampa.SelectedValue,0,0);
            cmblugar.DataSource = dtlufotos;
            cmblugar.DataValueField = "id_MPOSPlanning";
            cmblugar.DataTextField = "pdv_Name";
            cmblugar.DataBind();
        }
        private void lisboxCategorias()
        {
            DataTable dtliscatego = Planning.Get_ObtenerCategoriasPla(Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.SelectedValue);
            RlistCategorias.DataSource = dtliscatego;
            RlistCategorias.DataValueField = "id_ProductCategory";
            RlistCategorias.DataTextField = "Product_Category";
            RlistCategorias.DataBind();
            lisPresentaciones.Items.Clear();
            rblistpresntaprincipal.Items.Clear(); 
            dtliscatego = null;
            
        }  
        #endregion 

        #region Mensajes
        private void Mensaje_Usuario()
        {
            PMensajes.CssClass = this.Session["cssclass"].ToString();
            //PCanal.Width = 350;
            //PCanal.Height = 140;
            lblencabezado.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneral.Text = this.Session["mensaje"].ToString();
            ModalPopupCanal.Show();
        }
        private void Mensaje_CiudadPricipal()
        {
            ModalPopupciudadprincipal.Show();            
        }
        private void Mensaje_asignacionCategoria()
        {
            ModalPopupCambioCategorias.Show();            
        }
        #endregion
        
        #region Bindings
        public void BinData()
        {
            DataTable dtplan = null;
            this.Session["canal"] = cmbcanal.SelectedValue;
            dtplan = Planning.Get_ObtenerPlanVentas(this.Session["canal"].ToString(), Convert.ToInt32(cmbcliente.SelectedValue));
            Session["DataPlan"] = dtplan;
            gvplan.DataSource = dtplan;
            gvplan.DataBind();
        }
        private void Años()
        {
            DataTable dtaño = null;
            dtaño = PAmin.Get_ObtenerYears();
            cmbaño.DataSource = dtaño;
            cmbaño.DataValueField = "Years_id";
            cmbaño.DataTextField = "Years_Number";
            cmbaño.DataBind();
            cmbaño.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
        }
        private void CityPrincipal()
        {
            DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACION_CANAL_CONSULTACIUDADPRINCIPAL", cmbcanal.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    lblcodcitypriasignada.Text = dt.Rows[0]["cod_City"].ToString().Trim();
                    lblcitypriasignada.Text = dt.Rows[0]["Name_City"].ToString().Trim().ToUpper();
                    this.Session["CityPrincipal"] = dt;
                }
                else
                {
                    lblcodcitypriasignada.Text = "";
                    lblcitypriasignada.Text = "<< Sin Asignación >>";
                }
            }

        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    string sUser = this.Session["sUser"].ToString();
                    string sPassw = this.Session["sPassw"].ToString();
                    if (sUser != null && sPassw != null)
                    {                        
                        cmbclientes();
                        Años();                        
                        Inicializar_Radibutonlist();                        
                        this.Session["añoactual"] = DateTime.Now.Year;
                        this.Session["mesactual"] = DateTime.Now.Month;                        
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
        protected void btnsesion_Click(object sender, EventArgs e)
        {
            PAmin.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
            this.Session.Abandon();
            Response.Redirect("~/login.aspx");
        }
        protected void cmbcliente_SelectedIndexChanged(object sender, EventArgs e)
        {         
            btnaceptar.Enabled = true;            
            RlistCategorias.Items.Clear();
            lisPresentaciones.Items.Clear();
            rblistpresntaprincipal.Items.Clear();            
            DataTable dtchannel = null;
            DataTable dtClixcampAct = (DataTable)this.Session["dtClientesxcampañasActivas"];
            
            //istrategy = Convert.ToInt32(dtClixcampAct.Rows[Convert.ToInt32(cmbcliente.SelectedIndex)]["cod_Strategy"].ToString().Trim());
            //this.Session["codstrategy"] = istrategy;

            dtchannel = Planning.Get_ObtenerChannelxServicio(Convert.ToInt32(cmbcliente.SelectedValue));
            cmbcanal.DataSource = dtchannel;
            cmbcanal.DataValueField = "cod_Channel";
            cmbcanal.DataTextField = "Channel_Name";
            cmbcanal.DataBind();

            if (cmbcanal.SelectedValue == "0" || cmbcanal.SelectedValue =="")
            {
                TabContainer1.Visible = false;

            }

            
        }
        protected void cmbcanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbcanal.SelectedValue == "0")
            {
                TabContainer1.Visible = false;                
            }
            else
            {
                gvphotogra.DataBind();
                DataTable general = Planning.Get_Obtener_Photos_general(Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.SelectedValue);
                DataTable detallado = Planning.Get_Obtener_Photos_detallado(Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.SelectedValue);
                if (general != null)
                {
                    if (detallado.Rows.Count > 0)
                    {
                        int restaGeneral = 3 - Convert.ToInt32(general.Rows[0][0].ToString().Trim());
                        this.Session["restageneral"] = restaGeneral;
                        CountFngeneral.Text = restaGeneral.ToString().Trim()+"/"+ restaGeneral.ToString().Trim();

                    }
                }
                if (detallado != null)
                {
                    if (detallado.Rows.Count > 0)
                    {
                        int restaDetallado = 6 - Convert.ToInt32(detallado.Rows[0][0].ToString().Trim());
                        this.Session["restadetallado"] = restaDetallado;
                        CountFnDetallado.Text = restaDetallado.ToString().Trim() + "/" + restaDetallado.ToString().Trim();
                    }
                }

                InicializarTabs();
                RblcityPrincipal.Enabled = true;
                btncitypri.Enabled = true;
                DataTable dtcity = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACIONXCANAL_OBTENERCYTYPLA", Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.Text);
                RblcityPrincipal.DataSource = dtcity;
                RblcityPrincipal.DataValueField = "cod_City";
                RblcityPrincipal.DataTextField = "Name_City";
                RblcityPrincipal.DataBind();
                CityPrincipal();
                lisboxCategorias();

                DataTable dtcategoriasexistentes = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACION_CANAL_CONSULTACANTIDADCATEGORIASASIGNADAS", cmbcanal.Text, Convert.ToInt32(cmbcliente.SelectedValue));
                CmbCategXClienXCanal.DataSource = dtcategoriasexistentes;
                CmbCategXClienXCanal.DataValueField = "id_ProductCategory";
                CmbCategXClienXCanal.DataTextField = "Product_Category";
                CmbCategXClienXCanal.DataBind();
                CmbCategXClienXCanal.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
                dtcategoriasexistentes = null;
                lblCategoria0.Visible = false;
                CmbCategXClienXCanal.Visible = false;
                lblCategoriainfo.Visible = false;
                CmbProdPSelec.Items.Clear();
                CmbProdPSelec.Visible = false;
                lblPresPrincipalReg.Visible = false;
                TxtPresPrincipalReg.Visible = false;
                lblCategoriainfocompe.Visible = false;
                CmbProdPCompeSelec.Visible = false;
                BtnDeshabilitareg.Visible = false;

            }
        }
        protected void btncitypri_Click(object sender, EventArgs e)
        {
            if (RblcityPrincipal.SelectedValue == "")
            {
                this.Session["encabemensa"] = "Parametros Incorrectos";
                this.Session["mensaje"] = "Sr. Usuario, Debe Seleccionar una Ciudad";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensaje_Usuario();
                return;
            }
            DataTable dtduplica = oCoon.ejecutarDataTable("UP_WEB_SEARCH_DUPLICACITYPRINCIPAL", cmbcanal.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue));
            if (dtduplica != null)
            {
                if (dtduplica.Rows.Count > 0)
                {
                    LblChangeCiudadPricipal.Text = "Sr. Usuario, actualmente este cliente tiene asignada la ciudad principal : " + dtduplica.Rows[0][2].ToString().Trim().ToUpper() + " Desea Cambiarla ?";
                    Mensaje_CiudadPricipal();
                }

                else
                {
                    Planning.Get_Register_City_Principal(RblcityPrincipal.SelectedValue, 0, cmbcanal.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue), true, this.Session["sUser"].ToString(), DateTime.Now, this.Session["sUser"].ToString(), DateTime.Now);                    
                    CityPrincipal();

                    DataTable dtprincipal = (DataTable)this.Session["CityPrincipal"];
                    string mensaje = "Para el cliente: " + cmbcliente.SelectedItem.Text + " en el canal: " + cmbcanal.SelectedItem.Text + " Almaceno la ciudad :" + lblcitypriasignada.Text + " como ciudad principal, se advierte que esta ciudad no está relacionada en la(s) siguiente(s) Campaña(s) :<br /><br />";
                    mostrarnota = 0;

                    for (int i = 0; i <= cmbcampa.Items.Count - 1; i++)
                    {
                        if (cmbcampa.Items[i].Value != "0")
                        {
                            DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACION_CANAL_CONSULTAEXISTCITYPRINCIPALINPLANNING", Convert.ToInt32(cmbcampa.Items[i].Value),dtprincipal.Rows[0]["cod_City"].ToString().Trim());
                            if (dt != null)
                            {
                                if (dt.Rows.Count <= 0)
                                {
                                    //generar string para correo 
                                    mensaje = mensaje + cmbcampa.Items[i].Text + "<br />";
                                    mostrarnota = mostrarnota + 1;
                                }                                
                            }
                        }                    
                    }
                    mensaje = mensaje + "<br />Por lo cual en el módulo de Cliente cuando el cliente despliegue información del canal : " + cmbcanal.SelectedItem.Text + " no incluirá la información recabada ni procesada de esta(s) campaña(s)";
                    this.Session["AdvertenciaCityP"] = mensaje;

                    if (mostrarnota > 0)
                    {
                        this.Session["encabemensa"] = "";
                        this.Session["mensaje"] = "Ha Asignado la Ciudad Principal. Por favor revice su correo para verificar Advertencia generada.";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        Mensaje_Usuario();
                        try
                        {
                            Enviomail oEnviomail = new Enviomail();
                            EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Advertencia_Asignación_ciudad_Principal");
                            Mails oMail = new Mails();
                            oMail.Server = "mail.lucky.com.pe";
                            //oeEmail.MailServer;
                            oMail.From = "wcastillo.col@lucky.com.pe";
                            oMail.To = this.Session["smail"].ToString().Trim() ;
                            oMail.Subject = "Advertencia generada al crear ciudad principal";
                            oMail.Body = this.Session["AdvertenciaCityP"].ToString().Trim();
                            oMail.CC = "wcastillo.col@lucky.com.pe";
                            oMail.BodyFormat = "HTML";
                            oMail.send();
                            oMail = null;
                            // oeEmail = null;
                            oEnviomail = null;                            
                        }
                        catch (Exception ex)
                        {
                            this.Session["encabemensa"] = "Envio Solicitudes";
                            this.Session["mensaje"] = "Sr. Usuario, se presentó un error inesperado al momento de enviar el correo de advertencia al crear ciudad principal. el cual informa : " + this.Session["AdvertenciaCityP"].ToString().Trim();
                            this.Session["cssclass"] = "MensajesSupConfirm";
                            Mensaje_Usuario();                          
                            return;
                        }
                    }
                    else
                    {
                        this.Session["encabemensa"] = "Sr.Usuario";
                        this.Session["mensaje"] = "Ha Asignado la Ciudad Principal";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        Mensaje_Usuario();
                    }
                }
            }
        }
        protected void ImgBtnSI_Click(object sender, ImageClickEventArgs e)
        {            
            Planning.Get_Modify_City_Principal(cmbcanal.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue), false, this.Session["sUser"].ToString(), DateTime.Now);
            Planning.Get_Register_City_Principal(RblcityPrincipal.SelectedValue, 0, cmbcanal.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue), true, this.Session["sUser"].ToString(), DateTime.Now, this.Session["sUser"].ToString(), DateTime.Now);
            CityPrincipal();

            DataTable dtprincipal = (DataTable)this.Session["CityPrincipal"];
            string mensaje = "Para el cliente: " + cmbcliente.SelectedItem.Text + " en el canal: " + cmbcanal.SelectedItem.Text + " Almaceno la ciudad :" + lblcitypriasignada.Text + " como ciudad principal, se advierte que esta ciudad no está relacionada en la(s) siguiente(s) Campaña(s) : <br /><br />";
            mostrarnota = 0;
            for (int i = 0; i <= cmbcampa.Items.Count - 1; i++)
            {
                if (cmbcampa.Items[i].Value != "0")
                {
                    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACION_CANAL_CONSULTAEXISTCITYPRINCIPALINPLANNING", Convert.ToInt32(cmbcampa.Items[i].Value), dtprincipal.Rows[0]["cod_City"].ToString().Trim());
                    if (dt != null)
                    {
                        if (dt.Rows.Count <= 0)
                        {
                            //generar string para correo 
                            mensaje = mensaje + cmbcampa.Items[i].Text + "<br />";
                            mostrarnota = mostrarnota + 1;
                        }
                    }
                }
            }
            mensaje = mensaje + "<br /> Por lo cual en el módulo de Cliente cuando el cliente despliegue información del canal : " + cmbcanal.SelectedItem.Text + " no incluirá la información recabada ni procesada de esta(s) campaña(s)";
            this.Session["AdvertenciaCityP"] = mensaje;

            if (mostrarnota > 0)
            {
                this.Session["encabemensa"] = "";
                this.Session["mensaje"] = "Ha Asignado la Ciudad Principal. Por favor revice su correo para verificar Advertencia generada.";
                this.Session["cssclass"] = "MensajesSupConfirm";
                Mensaje_Usuario();
                try
                {
                    Enviomail oEnviomail = new Enviomail();
                    EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Advertencia_Asignación_ciudad_Principal");
                    Mails oMail = new Mails();
                    oMail.Server = "mail.lucky.com.pe";
                    //oeEmail.MailServer;
                    oMail.From = "wcastillo.col@lucky.com.pe";
                    oMail.To = this.Session["smail"].ToString().Trim();
                    oMail.Subject = "Advertencia generada al crear ciudad principal";
                    oMail.Body = this.Session["AdvertenciaCityP"].ToString().Trim();
                    oMail.CC = "wcastillo.col@lucky.com.pe";
                    oMail.BodyFormat = "HTML";
                    oMail.send();
                    oMail = null;
                    // oeEmail = null;
                    oEnviomail = null;
                }
                catch (Exception ex)
                {
                    this.Session["encabemensa"] = "Envio Solicitudes";
                    this.Session["mensaje"] = "Sr. Usuario, se presentó un error inesperado al momento de enviar el correo de advertencia al crear ciudad principal. el cual informa : " + this.Session["AdvertenciaCityP"].ToString().Trim();
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    Mensaje_Usuario();
                    return;
                }
            }
            else
            {
                this.Session["encabemensa"] = "Sr.Usuario";
                this.Session["mensaje"] = "Se ha cambiado satisfactoriamente la Ciudad Principal";
                this.Session["cssclass"] = "MensajesSupConfirm";
                Mensaje_Usuario();
            }           
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            btnagreplan.Visible = true;
            btnNuevo.Visible = false;
            txtplan.Enabled = true;
            txtplanNal.Enabled = true;
            cmbaño.Enabled = true;
            cmbmes.Enabled = true;
            rblistipo.Enabled = true;
        }
        protected void cmbaño_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (Convert.ToInt32(cmbaño.SelectedItem.Text) < Convert.ToInt32(this.Session["añoactual"]))
            {
                cmbmes.DataBind();
                this.Session["encabemensa"] = "Parametros Incorrectos";
                this.Session["mensaje"] = "Sr. Usuario, El año seleccionado debe ser igual o mayor al actual.";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensaje_Usuario();
                return;
            }
            else
            {
                DataTable dtmes = null;
                dtmes = PAmin.Get_ObtenerMeses();
                cmbmes.DataSource = dtmes;
                cmbmes.DataValueField = "codmes";
                cmbmes.DataTextField = "namemes";
                cmbmes.DataBind();
               
            }
        }
        protected void cmbmes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbmes.SelectedValue) < Convert.ToInt32(this.Session["mesactual"]))
            {                
                this.Session["encabemensa"] = "Parametros Incorrectos";
                this.Session["mensaje"] = "Sr. Usuario, debe seleccionar un mes igual o superior al actual.";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensaje_Usuario();
                return;
            }

        }
        protected void btnagreplan_Click(object sender, EventArgs e)
        {
            if (rblistipo.Items[0].Selected == true)
            {
                estadorblistipo = rblistipo.Items[0].Text;
            }
            else
            {
                estadorblistipo = rblistipo.Items[1].Text; 
            }

            //icodstrategy = Convert.ToInt32(this.Session["codstrategy"]);
            try
            {

                if (cmbaño.SelectedValue== "0")
                {
                    this.Session["encabemensa"] = "Asignación Plan de Ventas";
                    this.Session["mensaje"] = "Sr. Usuario, aún no ha seleccionado el año.";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensaje_Usuario();
                    return;
                }

                if (cmbmes.SelectedValue == "0")
                {
                    this.Session["encabemensa"] = "Asignación Plan de Ventas";
                    this.Session["mensaje"] = "Sr. Usuario, aún no ha seleccionado el Mes.";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensaje_Usuario();
                    return;
                }


                if (lblcodcitypriasignada.Text == "")
                {
                    this.Session["encabemensa"] = "Asignación de Ciudad Principal";
                    this.Session["mensaje"] = "Sr. Usuario, aún no ha asignado la ciudad principal.";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensaje_Usuario();
                    return;
                }

                if (cmbcliente.SelectedValue == "0")
                {
                    this.Session["encabemensa"] = "Sr.Usuario";
                    this.Session["mensaje"] = "Debe Seleccionar Un Cliente";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensaje_Usuario();
                    return;
                }
                if (cmbcanal.SelectedValue == "0")
                {
                    this.Session["encabemensa"] = "Sr.Usuario";
                    this.Session["mensaje"] = "Debe Seleccionar Un Canal";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensaje_Usuario();
                    return;
                }
                if (txtplan.Text == "0")
                {
                    this.Session["encabemensa"] = "Sr.Usuario";
                    this.Session["mensaje"] = "Debe Ingresar un valor mayor de Cero" + "<br>" + "para su Plan de Ventas";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensaje_Usuario();
                    return;
                }
                DataTable dtduplica = oCoon.ejecutarDataTable("UP_WEB_SEARCH_DUPLICAPLANVENTAS", cmbcanal.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue), Convert.ToInt32(cmbmes.SelectedValue), Convert.ToInt32(cmbaño.SelectedValue));
                if (dtduplica != null)
                {
                    if (dtduplica.Rows.Count > 0)
                    {
                        this.Session["encabemensa"] = "Sr.Usuario";
                        this.Session["mensaje"] = "Ya existe un plan de ventas para este cliente, canal, año y mes seleccionados.";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        Mensaje_Usuario();
                        return;
                    }


                    Planning.Get_Register_SalesPlan(0, Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.SelectedValue, Convert.ToInt32(lblcodcitypriasignada.Text), Convert.ToDecimal(txtplan.Text), "0", Convert.ToDecimal(txtplanNal.Text), estadorblistipo, Convert.ToInt32(cmbmes.SelectedValue), Convert.ToInt32(cmbaño.SelectedValue), true, this.Session["sUser"].ToString(), DateTime.Now, this.Session["sUser"].ToString(), DateTime.Now);
                    this.Session["encabemensa"] = "Sr.Usuario";
                    this.Session["mensaje"] = "Se ha Guardado El Nuevo valor del Plan de Ventas";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    Mensaje_Usuario();
                    txtplan.Enabled = false;
                    txtplanNal.Enabled = false;
                    cmbaño.Enabled = false;
                    cmbmes.Enabled = false;
                    Años();
                    cmbmes.Text = "0";
                    rblistipo.Enabled = false;
                    txtplan.Text = "";
                    txtplanNal.Text = "";
                    Inicializar_Radibutonlist();

                    btnNuevo.Visible = true;
                    btnagreplan.Visible = false;
                    BinData();
                }
            }
            catch (Exception ex)
            {
                this.Session["encabemensa"] = "Sr.Usuario";
                this.Session["mensaje"] = "No se ha podido guardar el Plan de Ventas";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensaje_Usuario();
                return;
            }
        }

        protected void gvplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvplan.PageIndex = e.NewPageIndex;
            BinData();
        }
        protected void gvplan_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvplan.EditIndex = -1;
            BinData();

            ////DataBind();
            //DataTable dtplan = null;          
            //dtplan = Planning.Get_ObtenerPlanVentas(this.Session["canal"].ToString());
            //gvplan.DataSource = dtplan;
            //gvplan.DataBind();           
        }
        protected void gvplan_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void gvplan_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvplan_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvplan_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvplan.EditIndex = e.NewEditIndex;
            this.Session["idsaleplan"] = gvplan.Rows[e.NewEditIndex].Cells[0].Text;
            BinData();
        }
        protected void gvplan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            DataTable dt = (DataTable)Session["DataPlan"];

            //Update the values.
            GridViewRow row = gvplan.Rows[e.RowIndex];
            //int istrategy = Convert.ToInt32(this.Session["codstrategy"]);
            //dt.Rows[gvplan.EditIndex]["id_plan"] = ((Label)(row.Cells[0].Controls[3])).Text;
            dt.Rows[gvplan.EditIndex]["Valor_Plan_city"] = Convert.ToDecimal(((TextBox)(row.Cells[1].Controls[1])).Text);
            dt.Rows[gvplan.EditIndex]["Value_plan_Nal"] =  Convert.ToDecimal(((TextBox)(row.Cells[2].Controls[1])).Text);                            
            if (((TextBox)(row.Cells[3].Controls[1])).Text.ToUpper() != "TM" && ((TextBox)(row.Cells[3].Controls[1])).Text.ToUpper() != "$")
            {
                ((TextBox)(row.Cells[3].Controls[1])).Text = "";
                this.Session["encabemensa"] = "Sr.Usuario";
                this.Session["mensaje"] = "En el campo Unidades solo se permite escribir TM ó $ Según sea el caso";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensaje_Usuario();
                return;
            }
            dt.Rows[gvplan.EditIndex]["Unidades"] = ((TextBox)(row.Cells[3].Controls[1])).Text.ToUpper();
            //dt.Rows[gvplan.EditIndex]["Año"] = ((DropDownList)(row.Cells[2].Controls[1])).SelectedValue;
            //dt.Rows[gvplan.EditIndex]["Mes"] = ((DropDownList)(row.Cells[3].Controls[1])).SelectedValue;
            int idsale;
            idsale = Convert.ToInt32(this.Session["idsaleplan"]);

            //int idsale, i=0;
            //idsale = Convert.ToInt32(gvplan.Rows[0].Cells[i].Text);
            //i++;
            Planning.Get_UpdateSalesPlan(idsale, 0, Convert.ToInt32(cmbcliente.SelectedValue), Convert.ToInt32(lblcodcitypriasignada.Text), Convert.ToDecimal(((TextBox)(row.Cells[1].Controls[1])).Text), "0", Convert.ToDecimal(((TextBox)(row.Cells[2].Controls[1])).Text), ((TextBox)(row.Cells[3].Controls[1])).Text.ToUpper(), this.Session["sUser"].ToString(), DateTime.Now);

            //Reset the edit index.
            gvplan.EditIndex = -1;

            this.Session["encabemensa"] = "Sr.Usuario";
            this.Session["mensaje"] = "Se ha cambiado satisfactoriamente el plan de ventas";
            this.Session["cssclass"] = "MensajesSupConfirm";
            Mensaje_Usuario();

            //Bind data to the GridView control.
            BinData();
        }

        protected void RlistCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtpresenta = null;

            lisPresentaciones.Items.Clear();
            rblistpresntaprincipal.Items.Clear();
            ChkPresentCompe.Items.Clear();

            dtpresenta = Planning.Get_ObtenerPresentacionesxCategoriaAsignacionCanal(Convert.ToInt32(RlistCategorias.SelectedValue), Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.SelectedValue);
            lisPresentaciones.DataSource = dtpresenta;
            lisPresentaciones.DataValueField = "idporuct";
            lisPresentaciones.DataTextField = "name_product";
            lisPresentaciones.DataBind();



        }
        //protected void RlistCategorias_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataTable dtpresenta = null;

        //    dtpresenta = Planning.Get_Obtener_PresentacionesxCategoria(Convert.ToInt32(RlistCategorias.SelectedValue));
        //    lisPresentaciones.DataSource = dtpresenta;
        //    lisPresentaciones.DataValueField = "idporuct";
        //    lisPresentaciones.DataTextField = "name_product";
        //    lisPresentaciones.DataBind();


        //     DataTable dtplaventa = null;
        //     dtplaventa = Planning.Get_ObtenerProductPlanventas(Convert.ToInt32(RlistCategorias.SelectedValue));

        //}
        protected void lisPresentaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dtpresenta = null;            
            //dtpresenta = Planning.Get_ObtenerPresentacionesxCategoriaAsignacionCanal(Convert.ToInt32(RlistCategorias.SelectedValue), Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.SelectedValue);
            //rblistpresntaprincipal.DataSource = dtpresenta;
            //rblistpresntaprincipal.DataValueField = "idporuct";
            //rblistpresntaprincipal.DataTextField = "name_product";
            //rblistpresntaprincipal.DataBind();


            //Se modifica forma de llenar el control de las presentaciones que se ha de usar para seleccionar la presentacion principal - principal
            //Ing. Mauricio Ortiz
            //Fecha de modificación: 20/01/2010

            rblistpresntaprincipal.Items.Clear();
            int maxcount = 0;
            for (int i = 0; i <= lisPresentaciones.Items.Count - 1; i++)
            {
                if (lisPresentaciones.Items[i].Selected == true)
                {
                    maxcount = maxcount + 1;
                    if (maxcount > 3)
                    {
                        maxcount = maxcount - 1;
                        lisPresentaciones.Items[i].Selected = false;
                        this.Session["encabemensa"] = "Sr.Usuario";
                        this.Session["mensaje"] = "Solo es permitido seleccionar máximo 3 presentaciones";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        Mensaje_Usuario();
                        return;
                    }
                    else
                    {
                        rblistpresntaprincipal.Items.Insert(rblistpresntaprincipal.Items.Count, new ListItem(lisPresentaciones.Items[i].Text, lisPresentaciones.Items[i].Value));
                    }
                }
            }
        }
        protected void rblistpresntaprincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = Planning.Get_ObtenerPresentCompeXProductPrincipal(Convert.ToInt32(rblistpresntaprincipal.SelectedValue));
            ChkPresentCompe.DataSource = dt;
            ChkPresentCompe.DataValueField = "id_producCompe";
            ChkPresentCompe.DataTextField = "name_producCompe";
            ChkPresentCompe.DataBind();
            dt = null;
        }
        protected void ChkPresentCompe_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           

            for (int i = 0; i <= ChkPresentCompe.Items.Count - 1; i++)
            {
                if (ChkPresentCompe.Items[i].Selected == true)
                {
                    maxcountcompe = maxcountcompe + 1;
                    if (maxcountcompe > 3)
                    {
                        maxcountcompe = maxcountcompe - 1;
                        ChkPresentCompe.Items[i].Selected = false;
                        this.Session["encabemensa"] = "Sr.Usuario";
                        this.Session["mensaje"] = "Solo es permitido seleccionar máximo 3 presentaciones de la competencia";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        Mensaje_Usuario();
                        return;
                    }
                    else
                    {
                        if (maxcountcompe == 1)
                        {
                            this.Session["competidor1"] = ChkPresentCompe.Items[i].Value;
                        }
                        if (maxcountcompe == 2)
                        {
                            this.Session["competidor2"] = ChkPresentCompe.Items[i].Value;
                        }
                        if (maxcountcompe == 3)
                        {
                            this.Session["competidor3"] = ChkPresentCompe.Items[i].Value;
                        }                        
                    }
                }
            }

        }
        protected void btnaddpresnta_Click(object sender, EventArgs e)
        {
            try
            {
                if (RlistCategorias.SelectedValue == "")
                {
                    this.Session["encabemensa"] = "Sr.Usuario";
                    this.Session["mensaje"] = "Debe Seleccionar la Categoría";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensaje_Usuario();                    
                    return;
                }

                if (lisPresentaciones.SelectedValue == "")
                {
                    this.Session["encabemensa"] = "Sr.Usuario";
                    this.Session["mensaje"] = "Debe Seleccionar entre una y tres presentaciones";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensaje_Usuario();                    
                    return; 
                }

                if (rblistpresntaprincipal.SelectedValue == "")
                {

                    this.Session["encabemensa"] = "Sr.Usuario";
                    this.Session["mensaje"] = "Debe seleccionar la presentación principal";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensaje_Usuario();                    
                    return;
                }

                if (ChkPresentCompe.Items.Count == 0)
                {
                    this.Session["encabemensa"] = "Sr.Usuario";
                    this.Session["mensaje"] = "No existen presentaciones competidoras";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensaje_Usuario();
                    return;
                }

                if (ChkPresentCompe.SelectedValue =="")
                {
                    this.Session["encabemensa"] = "Sr.Usuario";
                    this.Session["mensaje"] = "Debe seleccionar entre una y tres presentaciones del competidor";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    Mensaje_Usuario();
                    return;
                }

                maxcountcompe = 0;
                this.Session["competidor1"] = "";
                this.Session["competidor2"] = "";
                this.Session["competidor3"] = "";
                for (int i = 0; i <= ChkPresentCompe.Items.Count - 1; i++)
                {
                    if (ChkPresentCompe.Items[i].Selected == true)
                    {
                        maxcountcompe = maxcountcompe + 1;

                        if (maxcountcompe == 1)
                        {
                            this.Session["competidor1"] = ChkPresentCompe.Items[i].Value;
                        }
                        if (maxcountcompe == 2)
                        {
                            this.Session["competidor2"] = ChkPresentCompe.Items[i].Value;
                        }
                        if (maxcountcompe == 3)
                        {
                            this.Session["competidor3"] = ChkPresentCompe.Items[i].Value;
                        }
                    }
                }

                //icodstrategy = Convert.ToInt32(this.Session["codstrategy"]);

                //verificar si ya hay 3 categorias para ese cliente  en ese canal 
                DataTable dtmaxcatg = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACION_CANAL_CONSULTACANTIDADCATEGORIASASIGNADAS", cmbcanal.Text, Convert.ToInt32(cmbcliente.SelectedValue));
                if (dtmaxcatg != null)
                {
                    if (dtmaxcatg.Rows.Count >= 3)
                    {
                        this.Session["encabemensa"] = "Sr.Usuario";
                        this.Session["mensaje"] = "Ya existen 3 categorías principales para este cliente en este canal";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        Mensaje_Usuario();
                        return;
                    }
                }
                dtmaxcatg = null;


                //verificar si la categoria para ese cliente en ese canal existe
                DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACION_CANAL_CONSULTACANTIDADPRESENTPRINCIPAL", cmbcanal.Text, RlistCategorias.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //verificar cantidad de productos principales creados para esa categoria en ese canal para ese cliente 
                        if (Convert.ToInt32(dt.Rows[0][0].ToString().Trim()) >= 3)
                        {
                            LblChangeAsignacion.Text = "Sr. Usuario, esta categoría ya existe para este cliente en este canal y tiene " + Convert.ToInt32(dt.Rows[0][0].ToString().Trim()) + " producto(s) principal(es) asignado(s), Desea Cambiar esta asignación ?";
                            // decidir si cambiar asignacion o no .
                            Mensaje_asignacionCategoria();
                            return;
                        }
                        else
                        {
                            int cantidadmax = Convert.ToInt32(dt.Rows[0][0].ToString().Trim());
                            this.Session["mensaje"] = "Este cliente tenía  " + Convert.ToInt32(dt.Rows[0][0].ToString().Trim()) + " producto(s) principale(s) asignado(s) a esta categoría. Se ha cambio por la que usted seleccionó en esta oportunidad";

                            foreach (ListItem LstProductos in lisPresentaciones.Items)
                            {
                                if (LstProductos.Selected)
                                {
                                    if (cantidadmax < 3)
                                    {
                                        //verifica si el producto principal de esa categoria de ese cliente en ese canal a registrar ya existe 
                                        DataTable dtduplica = oCoon.ejecutarDataTable("UP_WEB_SEARCH_DUPLICAPRESENTACIONPRINCIPAL", Convert.ToInt32(LstProductos.Value), RlistCategorias.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.Text);
                                        if (dtduplica != null)
                                        {
                                            if (dtduplica.Rows.Count == 0)
                                            {
                                                Planning.Get_Register_Presentaciones_Asignacionesxcanal(RlistCategorias.SelectedValue, Convert.ToInt32(LstProductos.Value), Convert.ToInt32(rblistpresntaprincipal.SelectedValue), Convert.ToInt32(cmbcliente.SelectedValue), 0, this.Session["competidor1"].ToString(), this.Session["competidor2"].ToString(), this.Session["competidor3"].ToString(), cmbcanal.Text, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                                cantidadmax = cantidadmax + 1;
                                            }
                                            else
                                            {
                                                //poner en estado falso la asignacion de esa categoria para ese canal de ese cliente 
                                                Planning.Get_Modify_Assignment__PresentationXCategoria(cmbcanal.Text, RlistCategorias.SelectedValue, Convert.ToInt32(LstProductos.Value), Convert.ToInt32(cmbcliente.SelectedValue), false, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                                //reasignar categoria con presentaciones principales en ese canal para ese cliente 
                                                Planning.Get_Register_Presentaciones_Asignacionesxcanal(RlistCategorias.SelectedValue, Convert.ToInt32(LstProductos.Value), Convert.ToInt32(rblistpresntaprincipal.SelectedValue), Convert.ToInt32(cmbcliente.SelectedValue), icodstrategy, this.Session["competidor1"].ToString(), this.Session["competidor2"].ToString(), this.Session["competidor3"].ToString(), cmbcanal.Text, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);                                                
                                            }
                                        }
                                    }
                                }
                            }

                            for (int i = 0; i <= RlistCategorias.Items.Count - 1; i++)
                            {
                                if (RlistCategorias.Items[i].Selected == true)
                                {
                                    RlistCategorias.Items.Remove(RlistCategorias.Items[i]);
                                }
                            }

                            lisPresentaciones.Items.Clear();
                            rblistpresntaprincipal.Items.Clear();
                            ChkPresentCompe.Items.Clear();
                            this.Session["encabemensa"] = "Sr.Usuario";
                            this.Session["cssclass"] = "MensajesSupConfirm";
                            Mensaje_Usuario();
                        }
                    }
                    else
                    {
                        //registrar categoria con sus productos principales para ese cliente en ese canal 
                        foreach (ListItem LstProductos in lisPresentaciones.Items)
                        {
                            if (LstProductos.Selected)
                            {
                                Planning.Get_Register_Presentaciones_Asignacionesxcanal(RlistCategorias.SelectedValue, Convert.ToInt32(LstProductos.Value), Convert.ToInt32(rblistpresntaprincipal.SelectedValue), Convert.ToInt32(cmbcliente.SelectedValue), icodstrategy, this.Session["competidor1"].ToString(), this.Session["competidor2"].ToString(), this.Session["competidor3"].ToString(), cmbcanal.Text, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                            }
                        }

                        for (int i = 0; i <= RlistCategorias.Items.Count - 1; i++)
                        {
                            if (RlistCategorias.Items[i].Selected == true)
                            {

                                RlistCategorias.Items.Remove(RlistCategorias.Items[i]);
                            }
                        }
                        lisPresentaciones.Items.Clear();
                        rblistpresntaprincipal.Items.Clear();
                        ChkPresentCompe.Items.Clear();
                        this.Session["encabemensa"] = "Sr.Usuario";
                        this.Session["mensaje"] = "Se ha Asignado las Presentaciones principales del cliente en ese canal con la categoría seleccionada";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        Mensaje_Usuario();
                    }
                }

                DataTable dtcategoriasexistentes = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACION_CANAL_CONSULTACANTIDADCATEGORIASASIGNADAS", cmbcanal.Text, Convert.ToInt32(cmbcliente.SelectedValue));
                CmbCategXClienXCanal.DataSource = dtcategoriasexistentes;
                CmbCategXClienXCanal.DataValueField = "id_ProductCategory";
                CmbCategXClienXCanal.DataTextField = "Product_Category";
                CmbCategXClienXCanal.DataBind();
                CmbCategXClienXCanal.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
                dtcategoriasexistentes = null;

            }
            catch (Exception ex)
            {
                this.Session["encabemensa"] = "Sr.Usuario";
                this.Session["mensaje"] = "Ocurrio un error al intentar guardar el registro , por favor intentelo nuevamente";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensaje_Usuario();                
                return;
            }




        }
        protected void BtnSICateg_Click(object sender, ImageClickEventArgs e)
        {
            //poner en estado falso la asignacion de esa categoria para ese canal de ese cliente 
            Planning.Get_Modify_Assignment__Presentations(cmbcanal.Text, RlistCategorias.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue), false, this.Session["sUser"].ToString().Trim(), DateTime.Now);

            try
            {
                icodstrategy = Convert.ToInt32(this.Session["codstrategy"]);
                foreach (ListItem LstProductos in lisPresentaciones.Items)
                {
                    if (LstProductos.Selected)
                    {
                        //reasignar categoria con presentaciones principales en ese canal para ese cliente 
                        Planning.Get_Register_Presentaciones_Asignacionesxcanal(RlistCategorias.SelectedValue, Convert.ToInt32(LstProductos.Value), Convert.ToInt32(rblistpresntaprincipal.SelectedValue), Convert.ToInt32(cmbcliente.SelectedValue), icodstrategy, this.Session["competidor1"].ToString(), this.Session["competidor2"].ToString(), this.Session["competidor3"].ToString(), cmbcanal.Text, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                    }
                }
                for (int i = 0; i <= RlistCategorias.Items.Count - 1; i++)
                {
                    if (RlistCategorias.Items[i].Selected == true)
                    {
                        RlistCategorias.Items.Remove(RlistCategorias.Items[i]);
                    }
                }
                lisPresentaciones.Items.Clear();
                rblistpresntaprincipal.Items.Clear();
                ChkPresentCompe.Items.Clear();
                this.Session["encabemensa"] = "Sr.Usuario";
                this.Session["mensaje"] = "Se ha Reasignado las Presentaciones principales del cliente en ese canal con la categoría seleccionada";
                this.Session["cssclass"] = "MensajesSupConfirm";
                Mensaje_Usuario();

                DataTable dtcategoriasexistentes = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACION_CANAL_CONSULTACANTIDADCATEGORIASASIGNADAS", cmbcanal.Text, Convert.ToInt32(cmbcliente.SelectedValue));
                CmbCategXClienXCanal.DataSource = dtcategoriasexistentes;
                CmbCategXClienXCanal.DataValueField = "id_ProductCategory";
                CmbCategXClienXCanal.DataTextField = "Product_Category";
                CmbCategXClienXCanal.DataBind();
                CmbCategXClienXCanal.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
                dtcategoriasexistentes = null;

            }
            catch (Exception ex)
            {
                this.Session["encabemensa"] = "Sr.Usuario";
                this.Session["mensaje"] = "Ocurrio un error al intentar guardar el registro , por favor intentelo nuevamente";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensaje_Usuario();
                return;
            }
        }
        protected void btnconsultapresnta_Click(object sender, EventArgs e)
        {
            btnaddpresnta.Visible = false;
            btnconsultapresnta.Visible = false;
            lblCategoria0.Visible = true;
            CmbCategXClienXCanal.Visible = true;
            consultaCateg.Style.Value = "display:block;";
            tasignac.Style.Value = "display:none;";

        }
        protected void CmbCategXClienXCanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbCategXClienXCanal.SelectedValue != "0")
            {
                lblCategoriainfo.Visible = true;
                CmbProdPSelec.Visible = true;

                DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACION_CANAL_CONSULTACANTIDADPRESENTPRINCIPAL", cmbcanal.Text, CmbCategXClienXCanal.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue));

                CmbProdPSelec.DataSource = dt;
                CmbProdPSelec.DataValueField = "Product_Name";
                CmbProdPSelec.DataTextField = "Product_Name";
                CmbProdPSelec.DataBind();

                lblPresPrincipalReg.Visible = true;
                TxtPresPrincipalReg.Visible = true;

                DataTable dt1 = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACION_CANAL_CONSULTAPRESENTPRINCIPAL", cmbcanal.Text, CmbCategXClienXCanal.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue));
                if (dt1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        TxtPresPrincipalReg.Text = dt1.Rows[0]["Product_Name"].ToString().Trim();
                    }
                }

                lblCategoriainfocompe.Visible = true;
                CmbProdPCompeSelec.Visible = true;
                DataTable dt2 = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACION_CANAL_CONSULTAPRESENTCOMPEXPRINCIPAL", cmbcanal.Text, CmbCategXClienXCanal.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue));
                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        CmbProdPCompeSelec.DataSource = dt2;
                        CmbProdPCompeSelec.DataValueField = "Presenta_Competition1";
                        CmbProdPCompeSelec.DataTextField = "name_producCompe";
                        CmbProdPCompeSelec.DataBind();
                    }
                }

                BtnDeshabilitareg.Visible = true;
            }
            else
            {
                lblCategoriainfo.Visible = false;
                CmbProdPSelec.Visible = false;
                lblPresPrincipalReg.Visible = false;
                TxtPresPrincipalReg.Visible = false;
                lblCategoriainfocompe.Visible = false;
                CmbProdPCompeSelec.Visible = false;
                BtnDeshabilitareg.Visible = false;

            }

        }
        protected void btncancelpresn_Click(object sender, EventArgs e)
        {
            RlistCategorias.Items.Clear();
            lisPresentaciones.Items.Clear();
            rblistpresntaprincipal.Items.Clear();
            ChkPresentCompe.Items.Clear();
            lisboxCategorias();
            lblCategoria0.Visible = false;
            CmbCategXClienXCanal.Visible = false;
            lblCategoriainfo.Visible = false;
            CmbProdPSelec.Items.Clear();
            CmbProdPSelec.Visible = false;
            lblPresPrincipalReg.Visible = false;
            TxtPresPrincipalReg.Visible = false;
            lblCategoriainfocompe.Visible = false;
            CmbProdPCompeSelec.Visible = false;
            BtnDeshabilitareg.Visible = false;
            consultaCateg.Style.Value = "display:none;";
            tasignac.Style.Value = "display:block;";
            btnaddpresnta.Visible = true;
            btnconsultapresnta.Visible = true;
        }
        protected void BtnDeshabilitareg_Click(object sender, EventArgs e)
        {
            
            //poner en estado falso la asignacion de esa categoria para ese canal de ese cliente 
            Planning.Get_Modify_Assignment__Presentations(cmbcanal.Text, CmbCategXClienXCanal.SelectedValue, Convert.ToInt32(cmbcliente.SelectedValue), false, this.Session["sUser"].ToString().Trim(), DateTime.Now);
            this.Session["encabemensa"] = "Sr.Usuario";
            this.Session["mensaje"] = "Se ha deshabilitado la categoría " + CmbCategXClienXCanal.SelectedItem.Text + " del cliente en ese canal";
            this.Session["cssclass"] = "MensajesSupConfirm";
            Mensaje_Usuario();

            DataTable dtcategoriasexistentes = oCoon.ejecutarDataTable("UP_WEBSIGE_ASIGNACION_CANAL_CONSULTACANTIDADCATEGORIASASIGNADAS", cmbcanal.Text, Convert.ToInt32(cmbcliente.SelectedValue));
            CmbCategXClienXCanal.DataSource = dtcategoriasexistentes;
            CmbCategXClienXCanal.DataValueField = "id_ProductCategory";
            CmbCategXClienXCanal.DataTextField = "Product_Category";
            CmbCategXClienXCanal.DataBind();
            CmbCategXClienXCanal.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            dtcategoriasexistentes = null;
            lblCategoriainfo.Visible = false;
            CmbProdPSelec.Visible = false;
            lblPresPrincipalReg.Visible = false;
            TxtPresPrincipalReg.Visible = false;
            lblCategoriainfocompe.Visible = false;
            CmbProdPCompeSelec.Visible = false;
            BtnDeshabilitareg.Visible = false;
        }

        protected void txtfecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtfecha.Text != "")
                {
                    txtfecha2.Text = "";
                    FechaFoto = Convert.ToDateTime(txtfecha.Text.ToString());
                    if (Convert.ToDateTime(txtfecha.Text.ToString()) > DateTime.Today)
                    {
                        txtfecha.Focus();
                        txtfecha.Text = "";
                        this.Session["encabemensa"] = "Parametros Incorrectos";
                        this.Session["mensaje"] = "Sr. Usuario, la fecha no puede ser mayor a la fecha actual";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        Mensaje_Usuario();
                        return;
                    }
                    else
                    {
                        txtfecha2.Focus();
                    }
                }
            }
            catch
            {
                txtfecha.Focus();
                txtfecha.Text = "";
                this.Session["encabemensa"] = "Parametros Incorrectos";
                this.Session["mensaje"] = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensaje_Usuario();
                return;
            }
        }
        protected void txtfecha2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtfecha2.Text != "")
                {
                    FechaFoto = Convert.ToDateTime(txtfecha2.Text.ToString());
                    if (Convert.ToDateTime(txtfecha2.Text.ToString()) > DateTime.Today || Convert.ToDateTime(txtfecha2.Text.ToString()) < Convert.ToDateTime(txtfecha.Text.ToString()))
                    {
                        txtfecha2.Focus();
                        txtfecha2.Text = "";
                        this.Session["encabemensa"] = "Parametros Incorrectos";
                        this.Session["mensaje"] = "Sr. Usuario, la fecha no puede ser mayor a la fecha actual ni menor a la fecha Desde";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        Mensaje_Usuario();
                        return;
                    }
                    else
                    {
                        cmbcampa.Focus();
                    }
                }
            }
            catch
            {
                txtfecha2.Focus();
                txtfecha2.Text = "";
                this.Session["encabemensa"] = "Parametros Incorrectos";
                this.Session["mensaje"] = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensaje_Usuario();
                return;
            }
        }
        protected void cmbcampa_SelectedIndexChanged(object sender, EventArgs e)
        {
            LugaresFotos();
        }
        protected void btnloadphoto_Click(object sender, EventArgs e)
        {
            if (txtfecha.Text == "" || txtfecha2.Text == "")
            {
                this.Session["encabemensa"] = "Parametros requeridos";
                this.Session["mensaje"] = "Sr. Usuario, debe ingresar las fechas desde hasta para efectuar la consulta";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensaje_Usuario();
                return;

            }
            icodstrategy = Convert.ToInt32(this.Session["codstrategy"]);
            DataTable dt1 = Planning.Get_Obtener_Photos_Asignacionxcanal(Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.SelectedValue, Convert.ToDateTime(txtfecha.Text), Convert.ToDateTime(txtfecha2.Text), Convert.ToInt32(cmbcampa.SelectedValue),
                 Convert.ToInt32(cmblugar.SelectedValue));
            gvphotogra.DataSource = dt1;
            gvphotogra.DataBind();
            this.Session["photoGenDet"] = dt1;
            if (dt1 != null)
            {
                if (dt1.Rows.Count > 0)
                {
                    for (int j = 0; j <= gvphotogra.Rows.Count - 1; j++)
                    {
                        string fn = System.IO.Path.GetFileName(dt1.Rows[j]["Photo_Directory"].ToString().Trim());
                        ((Image)gvphotogra.Rows[j].Cells[0].FindControl("ImgPhoto")).ImageUrl = "~/Pages/Modulos/Operativo/PictureActividad/" + fn;
                        ((TextBox)gvphotogra.Rows[j].Cells[0].FindControl("txtcomentario")).Text = dt1.Rows[j]["Photo_Comment_Observa"].ToString().Trim();
                        ((CheckBox)gvphotogra.Rows[j].Cells[0].FindControl("ChkGeneral")).Checked = Convert.ToBoolean(dt1.Rows[j]["Photo_general"].ToString().Trim());
                        ((CheckBox)gvphotogra.Rows[j].Cells[0].FindControl("ChkDetallado")).Checked = Convert.ToBoolean(dt1.Rows[j]["Photo_detallado"].ToString().Trim());
                    }
                }
            }
        }
        protected void btnagregafotos_Click(object sender, EventArgs e)
        {
            ContadorGeneral = Convert.ToInt32(CountFngeneral.Text);
            ContadorDetallado = Convert.ToInt32(CountFnDetallado.Text);
            DataTable dtphotogendet = (DataTable)this.Session["photoGenDet"];

            for (int j = 0; j <= gvphotogra.Rows.Count - 1; j++)
            {
                if (((CheckBox)gvphotogra.Rows[j].Cells[0].FindControl("ChkGeneral")).Checked == true)
                {
                    if (((CheckBox)gvphotogra.Rows[j].Cells[0].FindControl("ChkGeneral")).Checked != Convert.ToBoolean(dtphotogendet.Rows[j][3].ToString().Trim()))
                    {
                        ContadorGeneral = ContadorGeneral - 1;
                        CountFngeneral.Text = Convert.ToString(Convert.ToInt32(CountFngeneral.Text) - 1);
                        if (Convert.ToInt32(CountFngeneral.Text) < 0)
                        {
                            //  ContadorGeneral = 0;
                            CountFngeneral.Text = this.Session["restageneral"].ToString().Trim();
                            ////((CheckBox)gvphotogra.Rows[j].Cells[0].FindControl("ChkGeneral")).Checked = false;
                            DataTable dt1 = (DataTable)this.Session["photoGenDet"];
                            gvphotogra.DataSource = dt1;
                            gvphotogra.DataBind();
                            if (dt1 != null)
                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= gvphotogra.Rows.Count - 1; i++)
                                    {
                                        string fn = System.IO.Path.GetFileName(dt1.Rows[i]["Photo_Directory"].ToString().Trim());
                                        ((Image)gvphotogra.Rows[i].Cells[0].FindControl("ImgPhoto")).ImageUrl = "~/Pages/Modulos/Operativo/PictureActividad/" + fn;
                                        ((TextBox)gvphotogra.Rows[i].Cells[0].FindControl("txtcomentario")).Text = dt1.Rows[i]["Photo_Comment_Observa"].ToString().Trim();
                                        ((CheckBox)gvphotogra.Rows[i].Cells[0].FindControl("ChkGeneral")).Checked = Convert.ToBoolean(dt1.Rows[i]["Photo_general"].ToString().Trim());
                                        ((CheckBox)gvphotogra.Rows[i].Cells[0].FindControl("ChkDetallado")).Checked = Convert.ToBoolean(dt1.Rows[i]["Photo_detallado"].ToString().Trim());
                                    }
                                }
                            }

                            this.Session["encabemensa"] = "Cantidad no permitida";
                            this.Session["mensaje"] = "Sr. Usuario, la cantidad de fotografías de nivel general fue superada";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            Mensaje_Usuario();
                            return;
                        }
                    }
                }
                else
                {
                    if (((CheckBox)gvphotogra.Rows[j].Cells[0].FindControl("ChkGeneral")).Checked != Convert.ToBoolean(dtphotogendet.Rows[j][3].ToString().Trim()))
                    {
                        ContadorGeneral = ContadorGeneral + 1;
                        CountFngeneral.Text = Convert.ToString(Convert.ToInt32(CountFngeneral.Text) + 1);
                        if (Convert.ToInt32(CountFngeneral.Text) > 3)
                        {
                            CountFngeneral.Text = "3";
                        }

                    }
                }

                if (((CheckBox)gvphotogra.Rows[j].Cells[0].FindControl("ChkDetallado")).Checked == true)
                {
                    if (((CheckBox)gvphotogra.Rows[j].Cells[0].FindControl("ChkDetallado")).Checked != Convert.ToBoolean(dtphotogendet.Rows[j][4].ToString().Trim()))
                    {
                        ContadorDetallado = ContadorDetallado - 1;
                        CountFnDetallado.Text = Convert.ToString(Convert.ToInt32(CountFnDetallado.Text) - 1);
                        if (Convert.ToInt32(CountFnDetallado.Text) < 0)
                        {
                            //ContadorDetallado = 0;
                            CountFnDetallado.Text = this.Session["restadetallado"].ToString().Trim();
                            /////((CheckBox)gvphotogra.Rows[j].Cells[0].FindControl("ChkDetallado")).Checked = false;
                            DataTable dt1 = (DataTable)this.Session["photoGenDet"];
                            gvphotogra.DataSource = dt1;
                            gvphotogra.DataBind();
                            if (dt1 != null)
                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= gvphotogra.Rows.Count - 1; i++)
                                    {
                                        string fn = System.IO.Path.GetFileName(dt1.Rows[i]["Photo_Directory"].ToString().Trim());
                                        ((Image)gvphotogra.Rows[i].Cells[0].FindControl("ImgPhoto")).ImageUrl = "~/Pages/Modulos/Operativo/PictureActividad/" + fn;
                                        ((TextBox)gvphotogra.Rows[i].Cells[0].FindControl("txtcomentario")).Text = dt1.Rows[i]["Photo_Comment_Observa"].ToString().Trim();
                                        ((CheckBox)gvphotogra.Rows[i].Cells[0].FindControl("ChkGeneral")).Checked = Convert.ToBoolean(dt1.Rows[i]["Photo_general"].ToString().Trim());
                                        ((CheckBox)gvphotogra.Rows[i].Cells[0].FindControl("ChkDetallado")).Checked = Convert.ToBoolean(dt1.Rows[i]["Photo_detallado"].ToString().Trim());
                                    }
                                }
                            }
                            this.Session["encabemensa"] = "Cantidad no permitida";
                            this.Session["mensaje"] = "Sr. Usuario, la cantidad de fotografías de nivel detallado fue superada";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            Mensaje_Usuario();
                            return;
                        }
                    }
                }
                else
                {
                    if (((CheckBox)gvphotogra.Rows[j].Cells[0].FindControl("ChkDetallado")).Checked != Convert.ToBoolean(dtphotogendet.Rows[j][4].ToString().Trim()))
                    {
                        ContadorDetallado = ContadorDetallado + 1;
                        CountFnDetallado.Text = Convert.ToString(Convert.ToInt32(CountFnDetallado.Text) + 1);
                        if (Convert.ToInt32(CountFnDetallado.Text) > 3)
                        {
                            CountFnDetallado.Text = "3";
                        }
                    }
                }
            }
            if (ContadorGeneral < 0)
            {
                this.Session["encabemensa"] = "Cantidad no permitida";
                this.Session["mensaje"] = "Sr. Usuario, la cantidad de fotografías de nivel general fue superada";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensaje_Usuario();
                return;
            }
            if (ContadorDetallado < 0)
            {
                this.Session["encabemensa"] = "Cantidad no permitida";
                this.Session["mensaje"] = "Sr. Usuario, la cantidad de fotografías de nivel detallado fue superada";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensaje_Usuario();
                return;
            }

            for (int j = 0; j <= gvphotogra.Rows.Count - 1; j++)
            {
                EPhotographs_Service PhotographsServices = oPhotographs_Service.ActualizarFotosActividadPropiaPlanning(Convert.ToInt32(gvphotogra.Rows[j].Cells[0].Text),
                    ((TextBox)gvphotogra.Rows[j].Cells[0].FindControl("txtcomentario")).Text, ((CheckBox)gvphotogra.Rows[j].Cells[0].FindControl("ChkGeneral")).Checked,
                    ((CheckBox)gvphotogra.Rows[j].Cells[0].FindControl("ChkDetallado")).Checked, Convert.ToString(this.Session["sUser"]), DateTime.Now);
            }
            this.Session["encabemensa"] = "Asignación";
            this.Session["mensaje"] = "Sr. Usuario, fue realizada la asignación de fotográfias correctamente";
            this.Session["cssclass"] = "MensajesSupConfirm";
            Mensaje_Usuario();           
            txtfecha.Text = "";
            txtfecha2.Text = "";
            cmbcampa.Text = "0";
            cmblugar.Text = "0";
            gvphotogra.DataBind();
            CountFngeneral.Text = "";
            CountFnDetallado.Text = "";
            ContadorGeneral = 0;
            ContadorDetallado = 0;
            gvphotogra.EmptyDataText = "";

            DataTable general = Planning.Get_Obtener_Photos_general(Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.SelectedValue);
            DataTable detallado = Planning.Get_Obtener_Photos_detallado(Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.SelectedValue);
            if (general != null)
            {
                if (detallado.Rows.Count > 0)
                {
                    int restaGeneral = 3 - Convert.ToInt32(general.Rows[0][0].ToString().Trim());
                    this.Session["restageneral"] = restaGeneral;
                    CountFngeneral.Text = restaGeneral.ToString().Trim();

                }
            }
            if (detallado != null)
            {
                if (detallado.Rows.Count > 0)
                {
                    int restaDetallado = 6 - Convert.ToInt32(detallado.Rows[0][0].ToString().Trim());
                    this.Session["restadetallado"] = restaDetallado;
                    CountFnDetallado.Text = restaDetallado.ToString().Trim();
                }
            }
        }


        protected void btnUpdateActCom_Click(object sender, EventArgs e)
        {
            for (int j = 0; j <= GVActividades.Rows.Count - 1; j++)
            {
                if (((CheckBox)GVActividades.Rows[j].Cells[0].FindControl("ChkSelActividad")).Checked == true)
                {
                    contadorPhotoCom = contadorPhotoCom + 1;
                    if (contadorPhotoCom > 3)
                    {
                        contadorPhotoCom = 0;
                        this.Session["encabemensa"] = "Cantidad no permitida";
                        this.Session["mensaje"] = "Sr. Usuario, la cantidad de Actividades en el comercio principales fue superada. recuerde que el máximo permitido es 3 por cliente y por canal. ";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        Mensaje_Usuario();
                        return;
                    }
                }
            }

            for (int j = 0; j <= GVActividades.Rows.Count - 1; j++)
            {
                ECompetition__Information ActComPrincipal = oCompetition__Information.ActualizarActComPrincipal(Convert.ToInt32(GVActividades.Rows[j].Cells[1].Text), ((TextBox)GVActividades.Rows[j].Cells[0].FindControl("TxtObservacion")).Text, ((CheckBox)GVActividades.Rows[j].Cells[0].FindControl("ChkSelActividad")).Checked, this.Session["sUser"].ToString().Trim(), DateTime.Now);
            }
            for (int j = 0; j <= gvactivi.Rows.Count - 1; j++)
            {
                ECompetition__Information ObsFotoActCom = oCompetition__Information.ActualizarObsFotoActividadCom(Convert.ToInt32(gvactivi.Rows[j].Cells[0].Text), Convert.ToInt32(gvactivi.Rows[j].Cells[1].Text), ((TextBox)gvactivi.Rows[j].Cells[0].FindControl("txtcomentario")).Text, this.Session["sUser"].ToString().Trim(), DateTime.Now);
            }
            this.Session["encabemensa"] = "Asignación";
            this.Session["mensaje"] = "Sr. Usuario, fue realizada la asignación de las actividades del comercio principales.";
            this.Session["cssclass"] = "MensajesSupConfirm";
            Mensaje_Usuario();
        }





        protected void btnasignarPlan_Click(object sender, EventArgs e)
       {
           //btnasignarPlan.Visible = false;
           //Btnnewplan.Visible = true;
           //btnsarch.Visible = true;
           
         

       }

     

       protected void btnactualiza_Click(object sender, EventArgs e)
       {
           //btnactualiza.Visible = false;
           //btnsarch.Visible= true; 
           //Btnnewplan.Visible = true;

       }


       //protected void Btncerrar_Click(object sender, EventArgs e)
       //{
       //    Imgbtnfoto1.Visible = true;
       //    Imgbtnfoto2.Visible = true;
       //    IImgbtnfoto3.Visible = true;
       //    Lablcomenta.Visible = true;
       //    txtcomenta.Visible = true;
       //    lblcomenta1.Visible = true;
       //    txtcomenta1.Visible = true;
       //    lblcomenta2.Visible = true;
       //    txtcomenta2.Visible = true;
       //    Button1.Visible = true;
       //    Button2.Visible = true;
       //}

       

       protected void txtcomenta3_TextChanged(object sender, EventArgs e)
       {
                  }

    

     

       protected void Gvplan_RowUpdating(object sender, GridViewUpdateEventArgs e)
       {
           


       }

       

       protected void btnasignarPlan_Click1(object sender, EventArgs e)
       {
          
       }

       protected void RegsPag_SelectedIndexChanged(object sender, EventArgs e)
       {
          

       }

       protected void IraPag(object sender, EventArgs e)
       {
           
          
       }

  

     

     
       protected void btnagregar_Click(object sender, EventArgs e)
       {
           
          
       }

      

      

       
    

       

       
       //protected void cmbcampaña_SelectedIndexChanged(object sender, EventArgs e)
       //{
       //    LugaresFotos();
       //}

     

       

     

       protected void rblistipo_SelectedIndexChanged(object sender, EventArgs e)
       {
           string valorreal;
           if (rblistipo.Items[0].Selected)
           {

               
               valorreal = "TM";




           }
           else {
               valorreal = "$";
           
           
           
           
           }
           this.Session["signo"] = valorreal;
       }

      

    
   

     

       protected void btnaceptar_Click(object sender, EventArgs e)
       {

       }

       protected void Button1_Click(object sender, EventArgs e)
       {
           TabContainer1.Tabs[3].Enabled = true;
       }

       protected void gvplan_SelectedIndexChanged(object sender, EventArgs e)
       {
           int idsale = Convert.ToInt32(gvplan.SelectedDataKey.Value);
           this.Session["idsale"] = idsale;
       }

     

       protected void cmbactividade_SelectedIndexChanged(object sender, EventArgs e)
       {

       }

       

      

     

      

       protected void gvactivi_SelectedIndexChanged(object sender, EventArgs e)
       {

       }

       protected void btnagregarcomer_Click(object sender, EventArgs e)
       {

       }

       protected void btncargaractiv_Click(object sender, EventArgs e)
       {
          // icodstrategy = Convert.ToInt32(this.Session["codstrategy"]);
          // //DataTable dt = Operaciones.Get_idCompetition_Information(Convert.ToString(this.Session["sUser"]));
          //// DataTable dt1 = Planning.Get_Obtener_Photos_Asignacionxcanal(Convert.ToInt32(cmbcliente.SelectedValue), cmbcanal.SelectedValue, Convert.ToDateTime(txtfecha.Text), Convert.ToDateTime(txtfecha2.Text),Convert.ToInt32(cmbcampa.SelectedValue), Convert.ToInt32(cmblugar.SelectedValue));
          // //Operaciones.Get_FotosCompetition_Information(Convert.ToInt32(dt.Rows[0]["id_cinfo"].ToString().Trim()));
          // gvactivi.DataSource = dt1;
          // gvactivi.DataBind();

          // if (dt1 != null)
          // {
          //     if (dt1.Rows.Count > 0)
          //     {
          //         for (int j = 0; j <= gvactivi.Rows.Count - 1; j++)
          //         {
          //             string fn = System.IO.Path.GetFileName(dt1.Rows[j]["Photo_Directory"].ToString().Trim());
          //             ((Image)gvactivi.Rows[j].Cells[0].FindControl("ImgPhoto")).ImageUrl = "~/Pages/Modulos/Operativo/PictureActividad/" + fn;
          //         }
          //     }
          // }   
       }

     

      




























    }
}
