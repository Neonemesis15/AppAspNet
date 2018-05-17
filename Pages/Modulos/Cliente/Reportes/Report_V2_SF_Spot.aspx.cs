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


namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Report_V2_SF_Spot : System.Web.UI.Page
    {
        private int compañia;
        private string pais;
        Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Conexion oCoon = new Conexion();
        private string sUser;
        private string sPassw;
        private string sNameUser;
        private int icompany;
        private int iservicio;
        private string canal;
        private int Report;

        private string sidaño;
        private string sidmes;
        private string sidperiodo;
        private string snode;
        private string sidcategoria;
        private string scompetencia;
        private string smarca;
        private string sfamilia;
        private string sactividad;
        private string fini;
        private string ffin;

        private void _AsignarVariables()
        {
            sidaño = cmbAnio.SelectedValue;
            sidmes = cmbMes.SelectedValue;
            sidperiodo = cmbPeriodo.SelectedValue;

            snode = cmbCadena.SelectedValue;
            sidcategoria = cmbCategoria.SelectedValue;
            scompetencia = cmbCompetencia.SelectedValue;
            smarca = cmbMarca.SelectedValue;
            sfamilia = cmbFamilia.SelectedValue;
            sactividad = cmbActividad.SelectedValue;
            fini = txt_fecha_inicio.SelectedDate.ToString();
            ffin = txt_fecha_fin.SelectedDate.ToString();

            if (fini.Equals("")) fini = "0";
            if (ffin.Equals("")) ffin = "0";

            string sidciudad = "0";
            string sidsub_categoria = "0";
            string sidsub_marca = "0";
            string sidsku = "0";
            string sidpuntoventa = "0";

            string sidperdil = this.Session["Perfilid"].ToString();
            if (cmbAnio.SelectedValue == "0" && cmbMes.SelectedValue == "0" && cmbPeriodo.SelectedValue == "0")
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    icompany = Convert.ToInt32(this.Session["companyid"]);
                    iservicio = Convert.ToInt32(this.Session["Service"]);
                    canal = this.Session["Canal"].ToString().Trim();
                    Report = Convert.ToInt32(this.Session["Reporte"]);
                    Periodo p = new Periodo(Report, sidciudad, sidcategoria, sidsub_categoria, smarca, sidsub_marca, sidsku, sidpuntoventa, canal, icompany, iservicio);
                    p.Set_PeriodoConDataValidada_New();

                    sidaño = p.Año;
                    sidmes = p.Mes;
                    sidperiodo = p.PeriodoId;
                    if (sidperiodo == null)
                        sidperiodo = "0";
                    GetPeridForAnalist();

                }
                else if (sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteGeneral"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado1"])
                {
                    GetPeriodForClient();
                }
            }
            else
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    GetPeridForAnalist();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session["Ciudad"] = "0";
            this.Session["catego"] = "0";
            this.Session["subcatego"] = "0";
            this.Session["pdv"] = "0";
            this.Session["Año"] = "0";
            this.Session["Mes"] = "0";

            sUser = this.Session["sUser"].ToString();
            sPassw = this.Session["sPassw"].ToString();
            sNameUser = this.Session["nameuser"].ToString();
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();

            if (!Page.IsPostBack)
            {
                cmbAnio.DataBind();
                cmbAnio.Items.Insert(0, new ListItem("--Todos--", "0"));
                cmbMes.DataBind();
                cmbMes.Items.Insert(0, new ListItem("--Todos--", "0"));
                cmbPeriodo.DataBind();
                cmbPeriodo.Items.Insert(0, new ListItem("--Todos--", "0"));
                cmbCadena.DataBind();
                cmbCadena.Items.Insert(0, new ListItem("--Todos--", "0"));
                cmbCategoria.DataBind();
                cmbCategoria.Items.Insert(0, new ListItem("--Todos--", "0"));
                cmbCompetencia.DataBind();
                cmbCompetencia.Items.Insert(0, new ListItem("--Todos--", "0"));
                cmbMarca.DataBind();
                cmbMarca.Items.Insert(0, new ListItem("--Todos--", "0"));
                cmbMarca.DataBind();
                cmbMarca.Items.Insert(0, new ListItem("--Todos--", "0"));
                cmbFamilia.DataBind();
                cmbFamilia.Items.Insert(0, new ListItem("--Todos--", "0"));
                cmbActividad.DataBind();
                cmbActividad.Items.Insert(0, new ListItem("--Todos--", "0"));

                _AsignarVariables();
                llenarreporteSpot();
                llenarreporteMarcaMes();
                llenarreporteDistribucionSpot();
                llenareportePorFamilia();
                llenarreportePromocion();
                cargarParametrosdeXml();

                Años();
                CargarCombo_Channel();
                cargarCadena();
                CargarCategoria();
                llenarMes();
                CargarMarca();
                CargarCompetencia();
                CargarFamilia();
            }
            else
            {
                MyAccordion.SelectedIndex = 1;
                TabContainer_Reporte_Precio.ActiveTabIndex = 1;
            }
        }

        private void llenarreporteSpot()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {
                //ventas por distribuidora
                rpt_spot_publicitario.Visible = true;
                rpt_spot_publicitario.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_spot_publicitario.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_SF_SPOT_PUBLICITARIO";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_spot_publicitario.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_spot_publicitario.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("compania", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("añio", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Mes", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("node", snode));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("competencia", scompetencia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Marca", smarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familia", sfamilia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Actividad", sactividad));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("FechaIni", fini));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("FechaFin", ffin));

                rpt_spot_publicitario.ServerReport.SetParameters(parametros);

            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }
        }

        private void llenarreporteMarcaMes()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {
                //ventas por distribuidora
                rpt_spot_marca_meses.Visible = true;
                rpt_spot_marca_meses.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_spot_marca_meses.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_SF_SPOT_MARCA_MES";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_spot_marca_meses.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_spot_marca_meses.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("compania", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("anio", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Mes", sidmes));                
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("node", snode));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("competencia", scompetencia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Marca", smarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familia", sfamilia));

                rpt_spot_marca_meses.ServerReport.SetParameters(parametros);

            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }
        }

        private void llenarreporteDistribucionSpot()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {
                //ventas por distribuidora
                rpt_spot_distri.Visible = true;
                rpt_spot_distri.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_spot_distri.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_SF_SPOT_DISTRIBUCION";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_spot_distri.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_spot_distri.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("compania", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("añio", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Mes", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("node", snode));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("competencia", scompetencia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Marca", smarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familia", sfamilia));

                rpt_spot_distri.ServerReport.SetParameters(parametros);

            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }
        }

        private void llenareportePorFamilia()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {
                //ventas por distribuidora
                rpt_spot_familia.Visible = true;
                rpt_spot_familia.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_spot_familia.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_SF_SPOT_FAMILIA";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_spot_familia.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_spot_familia.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("compania", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("añio", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Mes", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("node", snode));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("competencia", scompetencia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Marca", smarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familia", sfamilia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Actividad", sactividad));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("FechaIni", fini));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("FechaFin", ffin));

                rpt_spot_familia.ServerReport.SetParameters(parametros);

            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }
        }

        private void llenarreportePromocion()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {
                //ventas por distribuidora
                rpt_spot_promo.Visible = true;
                rpt_spot_promo.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_spot_promo.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_SF_SPOT_PROMOCION";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_spot_promo.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_spot_promo.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("compania", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("añio", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Mes", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("node", snode));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("competencia", scompetencia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Marca", smarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familia", sfamilia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Actividad", sactividad));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("FechaIni", fini));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("FechaFin", ffin));

                rpt_spot_promo.ServerReport.SetParameters(parametros);

            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }
        }

        private void Años()
        {

            DataTable dty = null;
            dty = Facd_ProcAdmin.Get_ObtenerYears();
            if (dty.Rows.Count > 0)
            {
                cmbAnio.DataSource = dty;
                cmbAnio.DataValueField = "Years_Number";
                cmbAnio.DataTextField = "Years_Number";
                cmbAnio.DataBind();





                cmbAnio.DataSource = dty;
                cmbAnio.DataValueField = "Years_Number";
                cmbAnio.DataTextField = "Years_Number";
                cmbAnio.DataBind();
                cmbAnio.Items.Insert(0, new ListItem("--Todos--", "0"));
            }
            else
            {

                dty = null;



            }

        }

        private void Llenar_Periodos()
        {
            DataTable dtp = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", canal, icompany, Report, cmbMes.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                cmbPeriodo.DataSource = dtp;
                cmbPeriodo.DataValueField = "id_periodo";
                cmbPeriodo.DataTextField = "Periodo";
                cmbPeriodo.DataBind();

                cmbPeriodo.Items.Insert(0, new ListItem("--Todos--", "0"));


            }
            else
            {

                dtp = null;

            }



        }

        protected void cargarCadena()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_NODECOMMERCIALXTIPO", "24");
            
            if (dt.Rows.Count > 0)
            {

                cmbCadena.DataSource = dt;
                cmbCadena.DataValueField = "NodeCommercial";
                cmbCadena.DataTextField = "commercialNodeName";
                cmbCadena.DataBind();
                cmbCadena.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbCadena.Enabled = true;

            }

        }

        protected void CargarTipoActividad()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();





            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_TIPOACTIVIDADXPLANNING", cmbplanning.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                cmbActividad.DataSource = dt;
                cmbActividad.DataValueField = "id_Tipo_Act";
                cmbActividad.DataTextField = "descripcion";
                cmbActividad.DataBind();
                cmbActividad.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbActividad.Enabled = true;
            }
        }

        protected void CargarCompetencia()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);



            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_CompetenciaXCliente", compañia.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbCompetencia.DataSource = dt;
                cmbCompetencia.DataValueField = "Company_id";
                cmbCompetencia.DataTextField = "Company_Name";
                cmbCompetencia.DataBind();
                cmbCompetencia.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbCompetencia.Enabled = true;
            }
        }




        protected void CargarMarca()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);


            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_MarcaXCompany", compañia.ToString(), "");
            if (dt.Rows.Count > 0)
            {
                cmbMarca.DataSource = dt;
                cmbMarca.DataValueField = "id_Brand";
                cmbMarca.DataTextField = "Name_Brand";
                cmbMarca.DataBind();
                cmbMarca.Items.Insert(0, new ListItem("---Todos---", "0"));
            }
        }
        protected void CargarCategoria()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            pais = this.Session["scountry"].ToString();


            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_Product_CategoryXCompany_id", compañia.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbCategoria.DataSource = dt;
                cmbCategoria.DataValueField = "id_ProductCategory";
                cmbCategoria.DataTextField = "Product_Category";
                cmbCategoria.DataBind();
                cmbCategoria.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbCategoria.Enabled = true;


            }
        }

        protected void CargarFamilia()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();



            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK", cmbCategoria.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                cmbFamilia.DataSource = dt;
                cmbFamilia.DataValueField = "id_ProductFamily";
                cmbFamilia.DataTextField = "name_Family";
                cmbFamilia.DataBind();
                cmbFamilia.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbFamilia.Enabled = true;
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
                cmbcanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }
        }

        protected void cmbcanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            //tdcampaña.Visible = true;
            cmbplanning.Visible = true;
            string sidchannel = cmbcanal.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);


            cmbplanning.Items.Clear();
            //cmbcategoria_producto.Items.Clear();
            //cmbcategoria_producto.Enabled = false;
            //cmbmarca.Items.Clear();
            //cmbmarca.Enabled = false;


            //cmbOficina.Items.Clear();
            //cmbOficina.Enabled = false;
            //cmbNodeComercial.Items.Clear();
            //cmbNodeComercial.Enabled = false;
            //cmbPuntoDeVenta.Items.Clear();
            //cmbPuntoDeVenta.Enabled = false;
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

        protected void cmbplanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            string sidplanning = cmbplanning.SelectedValue;

            if (cmbplanning.SelectedIndex != 0)
            {
                CargarTipoActividad();
                //------llamado al metodo cargar categoria de producto
                //string iischannel = cmbcanal.SelectedValue;


                //----------------------------------------------------
            }
            else
            {

            }
        }



        protected void llenarMes()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();



            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_MONTH");

            cmbMes.Items.Clear();
            cmbMes.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                cmbMes.DataSource = dt;
                cmbMes.DataValueField = "id_Month";
                cmbMes.DataTextField = "Month_name";
                cmbMes.DataBind();
                cmbMes.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbMes.Enabled = true;
            }

        }
                

        protected void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFamilia();
        }

        protected void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Llenar_Periodos();
        }

        protected void GetPeridForAnalist()
        {//se obtiene el estado de un Reporte en un Año, mes y periodo especifico.Y otros datos adicionales del periodo obtenido

            Report = Convert.ToInt32(this.Session["Reporte"]);
            DataTable dt = null;
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO", canal, Report, sidaño, sidmes, sidperiodo);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    div_Validar.Visible = true;
                    sidaño = dt.Rows[0]["id_Year"].ToString();
                    sidmes = dt.Rows[0]["id_Month"].ToString();
                    sidperiodo = dt.Rows[0]["periodo"].ToString();
                    bool valid_analist = Convert.ToBoolean(dt.Rows[0]["ReportsPlanning_ValidacionAnalista"]);

                    lbl_año_value.Text = sidaño;
                    lbl_mes_value.Text = sidmes;
                    lbl_periodo_value.Text = sidperiodo;


                    if (valid_analist)
                        chkb_validar.Checked = valid_analist;
                    else
                        chkb_invalidar.Checked = true;

                    lbl_validacion.Text = sidaño + "-" + dt.Rows[0]["Month_name"].ToString() + " " + sidperiodo;

                }
            }
        }

        protected void GetPeriodForClient()
        { //se obtiene el ultimo años mes y perido validado por el analista, para que el cliente pueda ver dicho reporte
            DataTable dt = null;

            Report = Convert.ToInt32(this.Session["Reporte"]);
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_MAX_PERIODO_VALIDADO", Report);


            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    sidaño = dt.Rows[0]["id_Year"].ToString();//con estos datos se debe hacer la carga
                    sidmes = dt.Rows[0]["id_Month"].ToString();
                    sidperiodo = dt.Rows[0]["periodo"].ToString();
                }

            }
        }

        protected void cargarParametrosdeXml()
        {
            string path = Server.MapPath("~/parametros.xml");

            if (System.IO.File.Exists(path))
            {
                Reportes_parametros oReportes_parametros = new Reportes_parametros();


                oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
                oReportes_parametros.Id_user = this.Session["sUser"].ToString();
                oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
                oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
                oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

                Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

                RadGrid_parametros.DataSource = oReportes_parametrosToXml.Get_Parametros(oReportes_parametros, path);
                RadGrid_parametros.DataBind();
            }
        }
        protected void chkb_invalidar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkb_invalidar.Checked)
            {
                chkb_validar.Checked = false;
            }
            lbl_msj_validar.Text = "¿ Esta seguro que desea continuar?";
            btn_aceptar2.Visible = false;
            btn_aceptar.Visible = true;
            btn_cancelar.Visible = true;

            ModalPopupExtender_ValidationAnalyst.Show();
        }
        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            btn_aceptar2.Visible = true;
            btn_aceptar.Visible = false;
            btn_cancelar.Visible = false;
            try
            {
                Report = Convert.ToInt32(this.Session["Reporte"]);
                oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_REPORT_PLANNING_ACTUALIZAR_VALIDACION", Report, lbl_año_value.Text.Trim(), lbl_mes_value.Text.Trim(), lbl_periodo_value.Text.Trim(), chkb_validar.Checked, Session["sUser"].ToString(), DateTime.Now);

                ModalPopupExtender_ValidationAnalyst.Show();
                lbl_msj_validar.Text = "El cambio se realizo con exito.";
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {

            if (chkb_validar.Checked == true)
            {
                chkb_validar.Checked = false;
                chkb_invalidar.Checked = true;
            }
            else
            {
                chkb_validar.Checked = true;
                chkb_invalidar.Checked = false;
            }
        }

        protected void chkb_validar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkb_validar.Checked)
            {
                chkb_invalidar.Checked = false;
            }
            lbl_msj_validar.Text = "¿ Esta seguro que desea continuar?";
            btn_aceptar2.Visible = false;
            btn_aceptar.Visible = true;
            btn_cancelar.Visible = true;

            ModalPopupExtender_ValidationAnalyst.Show();
        }



        protected void btngnerar_Click(object sender, EventArgs e)
        {
            _AsignarVariables();

            llenarreporteSpot();
            llenarreporteMarcaMes();
            llenarreporteDistribucionSpot();
            llenareportePorFamilia();
            llenarreportePromocion();
        }


        /*
        Conexion oCoon = new Conexion();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();

        #region Funciones Especiales
               
        

        
        protected void btn_imb_tab_Click(object sender, ImageClickEventArgs e)
        {
            TabContainer_filtros.ActiveTabIndex = 0;
        }

        
        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/parametros.xml");
            Reportes_parametros oReportes_parametros = new Reportes_parametros();

            oReportes_parametros.Id = Convert.ToInt32(Session["idxml"].ToString());
            oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
            oReportes_parametros.Id_user = this.Session["sUser"].ToString();
            oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
            oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
            oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

            oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_ciudad.SelectedValue);
            oReportes_parametros.Id_punto_venta = "0";
            oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
            oReportes_parametros.Id_subCategoria = cmb_subCategoria.SelectedValue;
            string sidmarca = cmb_marca.SelectedValue;
            if (sidmarca == "")
                sidmarca = "0";
            oReportes_parametros.Id_producto_marca = sidmarca;
            oReportes_parametros.Id_subMarca = cmb_subMarca.SelectedValue;
            oReportes_parametros.SkuProducto = cmbCheckBox_skuProducto.SelectedValue;
            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            oReportes_parametros.Id_periodo = Convert.ToInt32(cmb_periodo.SelectedValue);

            oReportes_parametros.Descripcion = txt_pop_actualiza.Text.Trim();

            Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

            if (System.IO.File.Exists(path))
            {
                if (oReportes_parametrosToXml.DeleteElement(oReportes_parametros, path))
                {
                    oReportes_parametrosToXml.addToXml(oReportes_parametros, path);
                    cargarParametrosdeXml();
                }

            }

            lbl_saveconsulta.Visible = true;
            btn_img_add.Visible = true;

            lbl_updateconsulta.Visible = false;
            btn_img_actualizar.Visible = false;

            TabContainer_filtros.ActiveTabIndex = 1;
        }
        #region Logica de la Validación del Reporte
       
        
        #endregion 

        */
    }
}