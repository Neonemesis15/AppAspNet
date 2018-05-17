using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Lucky.Data;
using System.Configuration;
using System.Data;
using Lucky.CFG.Util;
using Telerik.Web.UI;

namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_v2_PresenciaColgate_Bodega : System.Web.UI.Page
    {
        private int iidcompany;
        private string sidcanal;

        private static string sidaño;
        private static string sidmes;
        private static string sidperiodo;
        private string sidcobertura;
        private string sicadena = "0";
        private string sioficina;
        delegate string DType(string input);
        int iservicio;
        string canal;
        int Report;
        private bool bvalidanalyst;
        ReportViewer reporteHistorical;
        ReportViewer reporteExecutiveSumary;
        ReportViewer reporteIndexPrice;
        ReportViewer reporteIndexPriceDetail;


        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();
        Conexion oCoon = new Conexion();



        #region Generacion Informes de Gestión


        private void ReportHistorical()
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                reporteHistorical = (ReportViewer)(Reporte_v2_HistoricalTables1.FindControl("ReportHistoricalTables"));
                reporteHistorical.Visible = true;
                reporteHistorical.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                reporteHistorical.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_PresenciaHistoricoBodegaColgate";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                reporteHistorical.ServerReport.ReportServerUrl = new Uri(strConnection);
                reporteHistorical.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(iidcompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", sicadena));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("COBERTURA", sidcobertura));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sioficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("VALIDANALYST", bvalidanalyst.ToString()));

                reporteHistorical.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
            }

        }

        private void ReportExecutiveSummary()
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                reporteExecutiveSumary = (ReportViewer)(Reporte_v2_Wholesalers1.FindControl("ReportWholessalersGrafics"));
                reporteExecutiveSumary.Visible = true;
                reporteExecutiveSumary.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                reporteExecutiveSumary.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_PresenciaBodegaColgateGraficos";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                reporteExecutiveSumary.ServerReport.ReportServerUrl = new Uri(strConnection);
                reporteExecutiveSumary.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(iidcompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", sicadena));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("COBERTURA", sidcobertura));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sioficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("VALIDANALYST", bvalidanalyst.ToString()));

                reporteExecutiveSumary.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


            }
        }
        private void ReportIndexPrice()
        {

            iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                reporteIndexPrice = (ReportViewer)(Reporte_v2_IndexPrice1.FindControl("ReportIndexPrice"));
                reporteIndexPrice.Visible = true;
                reporteIndexPrice.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                reporteIndexPrice.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_IndexPriceResumenColgateBodega";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                reporteIndexPrice.ServerReport.ReportServerUrl = new Uri(strConnection);
                reporteIndexPrice.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(iidcompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", sicadena));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("TIPOCIUDAD", sidcobertura));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sioficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("VALIDANALYST", bvalidanalyst.ToString()));

                reporteIndexPrice.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
            }
        }
        private void ReportIndexPriceDetail()
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                reporteIndexPriceDetail = (ReportViewer)(Reporte_v2_IndexPriceDetail1.FindControl("ReportIndexPriceDetail"));
                reporteIndexPriceDetail.Visible = true;
                reporteIndexPriceDetail.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                reporteIndexPriceDetail.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_IndexPriceDetailBodega";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                reporteIndexPriceDetail.ServerReport.ReportServerUrl = new Uri(strConnection);
                reporteIndexPriceDetail.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(iidcompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", sicadena));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("TIPOCIUDAD", sidcobertura));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sioficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("VALIDANALYST", bvalidanalyst.ToString()));

                reporteIndexPriceDetail.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


            }

        }
        protected void GetPeriodForClient()
        { //se obtiene el ultimo años mes y perido validado por el analista, para que el cliente pueda ver dicho reporte
            DataTable dt = null;

            iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_MAX_PERIODO_VALIDADO_FINAL", Report, iservicio, canal, iidcompany);


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


                    chkb_validar.Checked = valid_analist;
                    chkb_invalidar.Checked = !valid_analist;

                    lbl_validacion.Text = sidaño + "-" + dt.Rows[0]["Month_name"].ToString() + " " + sidperiodo;

                    UpdatePanel_validacion.Update();
                }
            }
        }


        #endregion


        private void _AsignarVariables()
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);


            sidaño = cmb_año.SelectedValue;
            sidmes = cmb_mes.SelectedValue;
            sidperiodo = cmb_periodo.SelectedValue;

            sidcobertura = cmb_cobertura.SelectedValue;

            if (cmb_cadena.SelectedIndex > 0)
                sicadena = cmb_cadena.SelectedValue;

            // inegocio = Convert.ToInt32(cmb_negocio.SelectedValue);


            sioficina = cmb_oficina.SelectedValue;
            bvalidanalyst = true;


            if (sioficina == "")
                sioficina = "0";

            string sidperdil = this.Session["Perfilid"].ToString();
            if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0")
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    //aca debe ir la carga inical para el analista

                    Periodo p = new Periodo();

                    p.Reportid = Report;
                    p.Cliente = iidcompany;
                    p.Canal = canal;
                    p.Servicio = iservicio;


                    p.Set_PeriodoConDataValidada_New();

                    sidaño = p.Año;
                    cmb_año.SelectedValue = sidaño;

                    if (p.Mes.Length == 1)
                        sidmes = "0" + p.Mes;
                    else
                        sidmes = p.Mes;

                    cmb_mes.SelectedValue = sidmes;

                    sidperiodo = p.PeriodoId;

                    cmb_periodo.Visible = true;
                    lbl_periodo.Visible = true;
                    cmb_periodo.Items.Clear();
                    Llenar_Periodos();

                    bvalidanalyst = false;


                    //rcmb_añoPr.SelectedIndex=rcmb_añoPr.Items.FindItemIndexByValue(sidaño);dejo de usarse
                    //rcmb_mesPr.SelectedIndex = rcmb_mesPr.Items.FindItemIndexByValue(sidmes);dejo de usarse
                    rcmb_grafico.SelectedIndex = 1;

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
                    bvalidanalyst = false;
                    GetPeridForAnalist();
                }
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //fin de la session 
                iidcompany = Convert.ToInt32(this.Session["companyid"]);
                if (!Page.IsPostBack)
                {
                    cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));


                    canal = this.Session["Canal"].ToString().Trim();
                    Cobertura();
                    Ciudad();
                    //cargarCadena();
                    cargarAños();
                    cargarMes();

                    cargarCategorias();
                    cargarListaGraficos();
                    _AsignarVariables();

                    cargarProductos();
                    ReportExecutiveSummary();
                    ReportHistorical();
                    ReportIndexPrice();
                    ReportIndexPriceDetail();

                    ResumenEjecutivo();

                    string sidperdil = this.Session["Perfilid"].ToString();
                    if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                    {
                        TabPanel_config.HeaderText = "Configuración";
                        div_config.Visible = true;

                    }
                    else
                    {
                        TabPanel_config.HeaderText = "Resumen Ejecutivo";
                        TabPanel_config.Visible = true;
                        div_config.Visible = false;
                        TabContainer_Reporte_Presencia.ActiveTabIndex = 1;
                    }

                }



            }
            catch
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

        }


        private void Ciudad()
        {
            DataTable dtc = null;
            dtc = Get_DataClientes.Get_Obtenerinfocombos(iidcompany, canal, "0", 1);
            if (dtc.Rows.Count > 0)
            {

                cmb_oficina.DataSource = dtc;
                cmb_oficina.DataValueField = "cod_city";
                cmb_oficina.DataTextField = "name_city";
                cmb_oficina.DataBind();
                cmb_oficina.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
                cmb_oficina.Items.Remove(cmb_oficina.Items.FindItemIndexByValue("9"));
                //Reporte_v2_Stock_DiasGiroPorMarcaYFamilia.Cobertura(dtc);//llena conbos del user control
            }
            else
            {
                cmb_oficina.DataBind();
            }
        }

        private void Cobertura()
        {

            DataTable dtCobertura = oCoon.ejecutarDataTable("UP_WEBXPLORA_CARGARCOBERTURA_COLGATE", iidcompany, canal);

            if (dtCobertura.Rows.Count > 0)
            {
                cmb_cobertura.DataSource = dtCobertura;
                cmb_cobertura.DataValueField = "cod_Agrupacion";
                cmb_cobertura.DataTextField = "Oficina_descripcion";
                cmb_cobertura.DataBind();

                //cmb_cobertura.Items.Insert(0, new RadComboBoxItem("Nacional", "0"));

            }
            else
            {
                cmb_cobertura.DataBind();
            }

        }
        protected void cargarCadena()
        {
            cmb_cadena.Visible = false;
            lbl_cadena.Visible = false;


            DataTable dt = null;

            iservicio = Convert.ToInt32(this.Session["Service"]);
            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();

            string codOficina = "0";
            if (cmb_oficina.SelectedIndex > 0)
                codOficina = cmb_oficina.SelectedValue;

            if (cmb_cobertura.SelectedValue == "1" || cmb_oficina.SelectedIndex > 0)
            {
                cmb_cadena.Visible = true;
                lbl_cadena.Visible = true;
                if (cmb_cobertura.SelectedValue == "1")
                {
                    codOficina = "9";
                }
            }

            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_NODECOMERCIAL_BY_cod_oficina", iservicio, sidcanal, iidcompany, codOficina);


            cmb_cadena.DataSource = dt;
            cmb_cadena.DataValueField = "id_NodeCommercial";
            cmb_cadena.DataTextField = "commercialNodeName";
            cmb_cadena.DataBind();

            cmb_cadena.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));

        }
        private void cargarAños()
        {
            DataTable dty = null;
            dty = Get_Administrativo.Get_ObtenerYears();
            if (dty.Rows.Count > 0)
            {

                cmb_año.DataSource = dty;
                cmb_año.DataValueField = "Years_Number";
                cmb_año.DataTextField = "Years_Number";
                cmb_año.DataBind();
                cmb_año.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));

                string sidperdil = this.Session["Perfilid"].ToString();
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    Form_Presencia_Objetivos1.llenaAñosUC(dty);
                    Form_Presencia_PrecSugeridos1.llenaAñosUC(dty);
                    //rcmb_añoPr.DataSource = dty; ya no se usara
                    //rcmb_añoPr.DataValueField = "Years_Number";
                    //rcmb_añoPr.DataTextField = "Years_Number";
                    //rcmb_añoPr.DataBind();
                    //rcmb_añoPr.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
                }
            }
            else
            {
                dty = null;
            }
        }
        private void cargarMes()
        {
            DataTable dtm = Get_Administrativo.Get_ObtenerMeses();

            if (dtm.Rows.Count > 0)
            {
                cmb_mes.DataSource = dtm;
                cmb_mes.DataValueField = "codmes";
                cmb_mes.DataTextField = "namemes";
                cmb_mes.DataBind();
                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));


                string sidperdil = this.Session["Perfilid"].ToString();
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    Form_Presencia_Objetivos1.llenaMesUC(dtm);
                    Form_Presencia_PrecSugeridos1.llenaMesUC(dtm);
                    //rcmb_mesPr.DataSource = dtm;ya no se usara
                    //rcmb_mesPr.DataValueField = "codmes";
                    //rcmb_mesPr.DataTextField = "namemes";
                    //rcmb_mesPr.DataBind();
                    //rcmb_mesPr.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
                }
            }
        }

        private void cargarCategorias()
        {
            string sidperdil = this.Session["Perfilid"].ToString();

            if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
            {
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();
                Report = Convert.ToInt32(this.Session["Reporte"]);
                iservicio = Convert.ToInt32(this.Session["Service"]);

                DataTable dtcatego = Get_DataClientes.Get_Obtenerinfocombos_F(iidcompany, sidcanal, Report, "", 2);

                rlb_category.DataSource = dtcatego;
                rlb_category.DataValueField = "cod_catego";
                rlb_category.DataTextField = "Name_Catego";
                rlb_category.DataBind();
                rlb_category.Items.Insert(0, new RadListBoxItem("--Todos--", "0"));
            }
        }
        private static DataTable sdtProduct;
        protected void rlb_category_SelectedIndexChanged(object o, EventArgs e)
        {
            if (rlb_category.SelectedValue != "0")
            {
                DataTable dtFiltrado = new DataTable();

                dtFiltrado.Columns.Add("cod_Product");
                dtFiltrado.Columns.Add("Product_Name");

                string expression = "id_ProductCategory=" + rlb_category.SelectedValue;
                string sortOrder = "Product_Name ASC";

                DataRow[] foundRows;

                foundRows = sdtProduct.Select(expression, sortOrder);

                for (int i = 0; i < foundRows.Length; i++)
                {
                    DataRow dr = dtFiltrado.NewRow();

                    dr["cod_Product"] = foundRows[i]["cod_Product"];
                    dr["Product_Name"] = foundRows[i]["Product_Name"];

                    dtFiltrado.Rows.Add(dr);
                }

                rlb_productos.DataSource = dtFiltrado;
                rlb_productos.DataValueField = "cod_Product";
                rlb_productos.DataTextField = "Product_Name";
                rlb_productos.DataBind();
            }
            else
            {
                rlb_productos.DataSource = sdtProduct;
                rlb_productos.DataValueField = "cod_Product";
                rlb_productos.DataTextField = "Product_Name";
                rlb_productos.DataBind();
            }

        }
        private void cargarProductos()
        {
            string sidperdil = this.Session["Perfilid"].ToString();
            if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
            {
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();
                Report = Convert.ToInt32(this.Session["Reporte"]);
                iservicio = Convert.ToInt32(this.Session["Service"]);

                //cambiar paso del código de planning
                //string año = rcmb_añoPr.SelectedValue.ToString(); Se quito debido a k la gestión de gráficos ya no sera por año y mes 
                //string mes = rcmb_mesPr.SelectedValue.ToString();
                string cod_grafico = rcmb_grafico.SelectedValue.ToString();
                lbl_mensaje.ForeColor = System.Drawing.Color.Red;
                DataSet dtm = oCoon.ejecutarDataSet("UP_WEBXPLORA_PRESENCIA_OBTENER_PRODUCTOS_X_PLANNING", sidcanal, iidcompany, Report, iservicio, cod_grafico);

                sdtProduct = dtm.Tables[0];



                if (!cod_grafico.Equals("0"))
                {

                    rlb_productos.DataSource = dtm.Tables[0];
                    rlb_productos.DataValueField = "cod_Product";
                    rlb_productos.DataTextField = "Product_Name";
                    rlb_productos.DataBind();

                    rlb_category.SelectedIndex = 0;

                    rlb_lstproductos.DataSource = dtm.Tables[1];
                    rlb_lstproductos.DataValueField = "cod_Product";
                    rlb_lstproductos.DataTextField = "Product_Name";
                    rlb_lstproductos.DataBind();

                    lbl_grafico_text.Text = "Gráfico " + rcmb_grafico.SelectedItem.Text;
                    lbl_grafico_text.ForeColor = System.Drawing.Color.Green;
                    lbl_mensaje.Text = "";

                }
                else
                {
                    lbl_mensaje.Text = "Sr. usuario, debe seleccionar año, mes y gráfico.";
                }
            }
        }

        private void cargarListaGraficos()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?> " +
            "<Items>" +
                "<Item Text=\"--Seleccione--\" Value=\"0\" />" +
                "<Item Text=\"Executive Summary - 2\" Value=\"graf01\" />" +
                "<Item Text=\"Executive Summary - 2.1\" Value=\"graf02\" />" +
                "<Item Text=\"Executive Summary - 2.2\" Value=\"graf03\" />" +
                "<Item Text=\"Executive Summary - 2.3\" Value=\"graf04\" />" +
                "<Item Text=\"Executive Summary - 2.4\" Value=\"graf05\" />" +
                "<Item Text=\"Index Price - 1\" Value=\"graf11\" />" +
                "<Item Text=\"Index Price - 2\" Value=\"graf06\" />" +
                "<Item Text=\"Index Price - 2.1\" Value=\"graf07\" />" +
                "<Item Text=\"Index Price - 2.2\" Value=\"graf08\" />" +
                "<Item Text=\"Index Price - 3\" Value=\"graf09\" />" +
                "<Item Text=\"Index Price - 4\" Value=\"graf10\" />" +

            "</Items>";
            rcmb_grafico.LoadXml(xml);
        }

        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmb_mes.SelectedValue == "0")
            {
                cmb_periodo.Visible = false;
                lbl_periodo.Visible = false;
            }
            else
            {
                cmb_periodo.Visible = true;
                lbl_periodo.Visible = true;
                cmb_periodo.Items.Clear();
                Llenar_Periodos();
            }
            UpdatePanel_filtros.Update();
        }
        private void Llenar_Periodos()
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            cmb_periodo.Items.Clear();
            cmb_periodo.Enabled = true;
            DataTable dtp = null;
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PERIODOS_POR_MES", iservicio, sidcanal, iidcompany, Report, cmb_año.SelectedValue, cmb_mes.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                cmb_periodo.DataSource = dtp;
                cmb_periodo.DataValueField = "ReportsPlanning_Periodo";
                cmb_periodo.DataTextField = "Descripcion";
                cmb_periodo.DataBind();
                cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));

            }
            else
            {
                cmb_periodo.DataBind();
            }
        }
        protected void btngnerar_Click(object sender, EventArgs e)
        {
            TabContainer_Reporte_Presencia.ActiveTabIndex = 1;
            MyAccordion.SelectedIndex = 1;
            _AsignarVariables();
            ResumenEjecutivo();
            ReportHistorical();
            ReportExecutiveSummary();
            ReportIndexPrice();
            ReportIndexPriceDetail();

            TabContainer_filtros.ActiveTabIndex = 1;
        }
        protected void buttonGuardar_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/parametros.xml");
            Reportes_parametros oReportes_parametros = new Reportes_parametros();


            oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
            oReportes_parametros.Id_user = this.Session["sUser"].ToString();
            oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
            oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
            oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

            oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_oficina.SelectedValue);

            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            oReportes_parametros.Icadena = (cmb_cadena.SelectedValue == "") ? 0 : Convert.ToInt32(cmb_cadena.SelectedValue);


            oReportes_parametros.Descripcion = txt_descripcion_parametros.Text.Trim();

            Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

            if (!System.IO.File.Exists(path))
                oReportes_parametrosToXml.createXml(oReportes_parametros, path);
            else
                oReportes_parametrosToXml.addToXml(oReportes_parametros, path);


            cargarParametrosdeXml();
            txt_descripcion_parametros.Text = "";
            TabContainer_filtros.ActiveTabIndex = 1;
            up_MisFavoritos.Update();
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

        protected void btn_imb_tab_Click(object sender, ImageClickEventArgs e)
        {
            TabContainer_filtros.ActiveTabIndex = 0;
            up_saveConsulta.Update();
        }



        protected void chkb_validar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkb_validar.Checked)
                chkb_invalidar.Checked = false;
            else
                chkb_invalidar.Checked = true;

            lbl_msj_validar.Text = "¿ Esta seguro que desea continuar?";
            btn_aceptar2.Visible = false;
            btn_aceptar.Visible = true;
            btn_cancelar.Visible = true;
            UpdatePanel_validacion.Update();
            ModalPopupExtender_ValidationAnalyst.Show();
        }
        protected void chkb_invalidar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkb_invalidar.Checked)
                chkb_validar.Checked = false;
            else
                chkb_validar.Checked = true;

            lbl_msj_validar.Text = "¿ Esta seguro que desea continuar?";
            btn_aceptar2.Visible = false;
            btn_aceptar.Visible = true;
            btn_cancelar.Visible = true;

            UpdatePanel_validacion.Update();
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
            UpdatePanel_validacion.Update();
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
            UpdatePanel_validacion.Update();
        }

        protected void btn_guardar_params_Click(object sender, EventArgs e)
        {
            sidcanal = this.Session["Canal"].ToString();
            //string año = rcmb_añoPr.SelectedValue.ToString();
            // string mes = rcmb_mesPr.SelectedValue.ToString();
            string cod_grafico = rcmb_grafico.SelectedValue.ToString();
            string grafico = rcmb_grafico.Text.ToString();
            string cod_productos = "";

            lbl_mensaje.ForeColor = System.Drawing.Color.Red;

            if (!cod_grafico.Equals("0"))
            {
                if (rlb_lstproductos.Items.Count != 0)
                {
                    foreach (RadListBoxItem item in rlb_lstproductos.Items)
                        cod_productos = cod_productos + item.Value + ",";
                    //cod_productos = cod_productos + "'" + item.Value + "',";

                    cod_productos = cod_productos.Substring(0, cod_productos.Length - 1);

                    try
                    {
                        DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PRESENCIA_REGISTRAR_PANEL_PROD", sidcanal, cod_grafico, grafico, cod_productos);


                        rlb_productos.Items.Clear();
                        rlb_lstproductos.Items.Clear();
                        cargarProductos();

                        lbl_mensaje.Text = "Registro efectuado correctamente.";
                        lbl_mensaje.ForeColor = System.Drawing.Color.Green;
                    }
                    catch (Exception)
                    {
                        lbl_mensaje.Text = "Lo sentimos, no se pudo completar el registro.";
                    }
                }
                else
                {
                    lbl_mensaje.Text = "Sr. usuario, no ha seleccionado productos.";
                }
            }
            else
            {
                lbl_mensaje.Text = "Sr. usuario, debe seleccionar año, mes y gráfico.";
            }
        }

        protected void rcmb_grafico_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cargarProductos();
        }

        protected void RadGrid_parametros_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            if (e.CommandName == "BUSCAR")
            {
                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_periodo = (Label)e.Item.FindControl("lbl_id_periodo");
                Label lbl_id_tipoCiudad = (Label)e.Item.FindControl("lbl_id_tipoCiudad");
                Label lbl_id_oficina = (Label)e.Item.FindControl("lbl_id_oficina");
                Label lbl_id_cadena = (Label)e.Item.FindControl("lbl_id_cadena");
                ImageButton imgBtn_buscar = (ImageButton)e.Item.FindControl("btn_img_buscar");

                sidaño = lbl_id_año.Text;
                sidmes = lbl_id_mes.Text;
                sidperiodo = lbl_id_periodo.Text;
                sidcobertura = lbl_id_tipoCiudad.Text;
                sioficina = lbl_id_oficina.Text;
                sicadena = lbl_id_cadena.Text;

                cmb_año.SelectedIndex = cmb_año.Items.FindItemIndexByValue(sidaño);
                cmb_mes.SelectedIndex = cmb_mes.Items.FindItemIndexByValue(sidmes);
                Llenar_Periodos();
                cmb_periodo.SelectedIndex = cmb_periodo.Items.FindItemIndexByValue(sidperiodo);
                cmb_cobertura.SelectedIndex = cmb_cobertura.Items.FindItemIndexByValue(sidcobertura);
                cmb_oficina.SelectedIndex = cmb_cobertura.Items.FindItemIndexByValue(sioficina);
                cmb_cadena.SelectedIndex = cmb_cadena.Items.FindItemIndexByValue(sicadena);

                TabContainer_filtros.ActiveTabIndex = 0;

            }
            if (e.CommandName == "ELIMINAR")
            {
                Label lbl_id = (Label)e.Item.FindControl("lbl_id");

                string path = Server.MapPath("~/parametros.xml");
                Reportes_parametros oReportes_parametros = new Reportes_parametros();
                oReportes_parametros.Id = Convert.ToInt32(lbl_id.Text);



                Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

                oReportes_parametrosToXml.DeleteElement(oReportes_parametros, path);
                cargarParametrosdeXml();
            }
        }
        protected void cmb_cobertura_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmb_cobertura.SelectedValue == "2")
            {
                lbl_ciudad.Visible = true;
                cmb_oficina.Visible = true;
            }
            else
            {
                cmb_oficina.SelectedIndex = 0;
                lbl_ciudad.Visible = false;
                cmb_oficina.Visible = false;

                //---
                cmb_cadena.SelectedIndex = 0;
                cmb_cadena.Visible = false;
                lbl_cadena.Visible = false;

                cargarCadena();

            }
            UpdatePanel_filtros.Update();
        }
        protected void cmb_oficina_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            cargarCadena();

        }
        protected void btn_editorPopUp_Click(object sender, EventArgs e)
        {
            //UpdatePanel_editor.Update();
            //ModalPopupExtender_Editor.Show();
            string urlSource = Request.Url.PathAndQuery;
            Response.Redirect("~/Pages/Modulos/Cliente/Reportes/Informe_de_Presencia/Page_ResumenEjecutivo.aspx?año=" + sidaño + "&mes=" + sidmes + "&periodo=" + sidperiodo + "&urlSource=" + urlSource, true);

        }
        protected void ResumenEjecutivo()
        {
            try
            {
                iservicio = Convert.ToInt32(this.Session["Service"]);
                Report = Convert.ToInt32(this.Session["Reporte"]);
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();

                DataTable dtre = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_RESUMEN_EJECUTIVO", iservicio, sidcanal, iidcompany, Report, sidaño, sidmes, sidperiodo);

                string sidperdil = this.Session["Perfilid"].ToString();
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    if (dtre.Rows.Count > 0)
                        div_viewHtmlFormat.InnerHtml = dtre.Rows[0]["Observación"].ToString();
                    else
                        div_viewHtmlFormat.InnerHtml = "";

                    btn_editorPopUp.Visible = true;
                }
                else
                {
                    div_RE_cliente.InnerHtml = dtre.Rows[0]["Observación"].ToString();
                }

            }
            catch
            {

            }

        }
    }
}