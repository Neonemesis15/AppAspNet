using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.CFG.Util;
using Lucky.Data;
using Microsoft.Reporting.WebForms;
using Telerik.Web.UI;

namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_SF_Tra_ExaTdaConsolidado : System.Web.UI.Page
    {
        private Conexion oCoon = new Conexion();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();

        #region Declaracion de Campañas
        private int compañia;
        private string pais;
        private static string static_channel;
        private string idplanning;
        private string idcanal;
    
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        #endregion


        private string sUser;
        private string sPassw;
        private string sNameUser;
        private int icompany;
        private int iservicio;
        private string canal;
        private int Report;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region
            //sUser = this.Session["sUser"].ToString();
            //sPassw = this.Session["sPassw"].ToString();
            //sNameUser = this.Session["nameuser"].ToString();
            //icompany = Convert.ToInt32(this.Session["companyid"]);
            //iservicio = Convert.ToInt32(this.Session["Service"]);
            //Report = Convert.ToInt32(this.Session["Reporte"]);
            //canal = this.Session["Canal"].ToString().Trim();
            //MyAccordion.SelectedIndex = 1;

            //if (!IsPostBack)
            //{

                //try
                //{
                //    //iniciarcombos();

                //    //_AsignarVariables();
                //    //llenarreporteInicial();
                //    //cargarParametrosdeXml();
                //    //Años();
                //    //Llena_Meses();
                //    //llenaoficina();
                //    //llenacategoria();
                //    //llenafuerzav();
                //    //llenasupervisores();

                //    //llenaCorporacion_ini();
                //    //llenaNodoComercial_ini();
                //    //llenaPuntoDeVenta_ini();
                //    //llenaFamilia_ini();
                //    //llenaSubFamilia_ini();

                //}
                //catch (Exception ex)
                //{
                //    //Exception mensaje = ex;
                //    //this.Session.Abandon();
                //    //Response.Redirect("~/err_mensaje_seccion.aspx", true);
                //}

            //}
            #endregion

            if (!Page.IsPostBack)
            {
                CargarCombo_Channel();
                CargarCanal();
            }

        }

        protected void CargarCanal()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            pais = this.Session["scountry"].ToString();


            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CHANNEL", pais, compañia);
            if (dt.Rows.Count > 0)
            {
                cmbcanal.DataSource = dt;
                cmbcanal.DataValueField = "cod_Channel";
                cmbcanal.DataTextField = "Channel_Name";
                cmbcanal.DataBind();
                cmbcanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }

        }

        private void ConsultarExamenTda_SF_Tra()
        {
            //icompany = Convert.ToInt32(this.Session["companyid"]);
            //iservicio = Convert.ToInt32(this.Session["Service"]);
            
            idplanning = cmbplanning.SelectedValue.ToString();
            idcanal = cmbcanal.SelectedValue.ToString();

            DateTime dfecha_inicio, dfecha_fin;
            if (txt_fecha_inicio.SelectedDate.ToString() == "" || txt_fecha_inicio.SelectedDate.ToString() == "0" || txt_fecha_inicio.SelectedDate == null)
                dfecha_inicio = Convert.ToDateTime("01/01/1900");
            else dfecha_inicio = Convert.ToDateTime(txt_fecha_inicio.SelectedDate.Value);


            if (txt_fecha_fin.SelectedDate.ToString() == "" || txt_fecha_fin.SelectedDate.ToString() == "0" || txt_fecha_fin.SelectedDate == null)
                dfecha_fin = Convert.ToDateTime("01/01/1900");
            else dfecha_fin = Convert.ToDateTime(txt_fecha_fin.SelectedDate.Value);

            DateTime dfecha_inicio_aux;
            dfecha_inicio_aux=dfecha_inicio;
            DateTime dfecha_fin_aux;
            dfecha_fin_aux = dfecha_fin;

            string dfecha_ini_reporte;
            string dfecha_fin_reporte;

            dfecha_ini_reporte = dfecha_inicio_aux.Day + "/" + dfecha_inicio_aux.Month+"/"+dfecha_inicio_aux.Year;
            dfecha_fin_reporte = dfecha_fin_aux.Day + "/" + dfecha_fin_aux.Month + "/" + dfecha_fin_aux.Year;

            if (DateTime.Compare(dfecha_inicio, dfecha_fin) == 1)
            {
                lblmensaje.Visible = true;
                lblmensaje.Text = "verifique si La fecha de inicio debe ser menor o igual a la fecha fin";
                lblmensaje.ForeColor = System.Drawing.Color.Red;

                return;
            }
            else {
                lblmensaje.Visible = false;
            }

            string iidDistribuidora = "0";
            if (cmbNodeComercial.SelectedIndex >= 0)
                iidDistribuidora = cmbNodeComercial.SelectedValue.ToString();

            string iidDistrito = "0";
                if(cmbDistrito.SelectedIndex>=0)
                    iidDistrito=cmbDistrito.SelectedValue.ToString();
            
            string iidSupervidor ="0";
            if(cmbSupervisor.SelectedIndex>=0)
                iidSupervidor=cmbSupervisor.SelectedValue.ToString();

            string iidGenerador = "0";
            if(cmbGenerador.SelectedIndex>=0)
                iidGenerador = cmbGenerador.SelectedValue.ToString();
                

            try
            {
                //Precio Moderno
                rpt_SF_Tra_ExaTda.Visible = true;
                rpt_SF_Tra_ExaTda.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_SF_Tra_ExaTda.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_TRA_ExamenTienda_New";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_SF_Tra_ExaTda.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_SF_Tra_ExaTda.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idplanning", idplanning.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("fechaini", dfecha_ini_reporte));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("fechafin", dfecha_fin_reporte));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcanal", idcanal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("coddistribuidora", iidDistribuidora));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("coddistrito", iidDistrito));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("codsupervisor", iidSupervidor));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("codgenerador", iidGenerador));
                

                rpt_SF_Tra_ExaTda.ServerReport.SetParameters(parametros);


            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }
        }

        protected void CargarCombo_Channel()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            pais = this.Session["scountry"].ToString();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CHANNEL", pais, compañia);
            if (dt.Rows.Count > 0)
            {
                cmbcanal.DataSource = dt;
                cmbcanal.DataValueField = "cod_Channel";
                cmbcanal.DataTextField = "Channel_Name";
                cmbcanal.DataBind();
                cmbcanal.Items.Insert(0, new ListItem("---Todas---", "0"));
            }
        }

        protected void cmbcanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            compañia = Convert.ToInt32(this.Session["companyid"]);

            string sidchannel = cmbcanal.SelectedValue;

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);

            cmbplanning.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                cmbplanning.DataSource = dt;
                cmbplanning.DataValueField = "id_planning";
                cmbplanning.DataTextField = "Planning_Name";
                cmbplanning.DataBind();
                cmbplanning.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                cmbplanning.Enabled = true;
            }

        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            ConsultarExamenTda_SF_Tra();
        }

        protected void cmbplanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sidplanning = cmbplanning.SelectedValue;
            if (cmbplanning.SelectedIndex != 0)
            {
                cargarComboDistribuidorByPlanning();
                cargarComboDistritoByPlanning();
                cargarComboGeneradorByPlanning();
                cargarComboSupervidoresByPlanning();
            }
            else {
                cmbNodeComercial.Items.Clear();
                cmbNodeComercial.Enabled = false;
                cmbDistrito.Items.Clear();
                cmbDistrito.Enabled = false;
                cmbSupervisor.Items.Clear();
                cmbSupervisor.Enabled = false;
                cmbGenerador.Items.Clear();
                cmbGenerador.Enabled = false;
            }
        }

        public void cargarComboDistribuidorByPlanning() {
            //DistribuidoraBy_Planning
            cmbNodeComercial.Items.Clear();
            DataTable dt = new DataTable();
            Conexion Ocoon = new Conexion();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_NODOCOMERCIAL_BY_ID_PLANNING", cmbplanning.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbNodeComercial.DataSource = dt;
                cmbNodeComercial.DataValueField = "NodeCommercial";
                cmbNodeComercial.DataTextField = "commercialNodeName";
                cmbNodeComercial.DataBind();
                cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbNodeComercial.Enabled = true;
            }
        }

        public void cargarComboDistritoByPlanning() {
            //DistritosBy_Planning
            Conexion Ocoon = new Conexion();
            cmbDistrito.Items.Clear();
            DataTable dt = new DataTable();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DISTRITOS_BY_ID_PLANNING", cmbplanning.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbDistrito.DataSource = dt;
                cmbDistrito.DataValueField = "cod_District";
                cmbDistrito.DataTextField = "Name_Local";
                cmbDistrito.DataBind();
                cmbDistrito.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbDistrito.Enabled = true;
            }
        
        }

        public void cargarComboGeneradorByPlanning() {
            //GeneradoresByPlanning
            //UP_WEBXPLORA_OPE_GENERADORES_BY_ID_PLANNING
            Conexion Ocoon = new Conexion();
            cmbGenerador.Items.Clear();
            DataTable dt = new DataTable();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_GENERADORES_BY_ID_PLANNING", cmbplanning.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbGenerador.DataSource = dt;
                cmbGenerador.DataValueField = "Person_id";
                cmbGenerador.DataTextField = "Generador";
                cmbGenerador.DataBind();
                cmbGenerador.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbGenerador.Enabled = true;
            }
        }

        public void cargarComboSupervidoresByPlanning() {

            //SupervisoresByPlanning
            Conexion Ocoon = new Conexion();
            cmbSupervisor.Items.Clear();
            DataTable dt = new DataTable();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_SUPERVISORES_BY_ID_PLANNING", cmbplanning.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbSupervisor.DataSource = dt;
                cmbSupervisor.DataValueField = "Person_idSupe";
                cmbSupervisor.DataTextField = "Supervisor";
                cmbSupervisor.DataBind();
                cmbSupervisor.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbSupervisor.Enabled = true;
            }
        }
       

    }
}