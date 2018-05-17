using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
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
using Microsoft.Reporting.WebForms;
using System.Configuration;
using Telerik.Web.UI;
using System.Timers;


namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_v2_Pedido_SF : System.Web.UI.Page
    {


        Conexion oCoon = new Conexion();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();

        string sUser;
        string sPassw;
        string sNameUser;
        int icompany;
        int iservicio;
        string canal;
        int Report;



        #region Funciones Especiales

        private string sidaño;
        private string sidmes;
        private string sidperiodo;
        private string siddia;
        private string sidciudad;
        private string sidproductcategory;
        private string sidsub_categoria;
        private string sidmarca;
        private string sidsub_marca;
        private string sidsku;
        private string sidpuntoventa;
       

        private void _AsignarVariables()
        {
            sidaño = cmb_año.SelectedValue;
            sidmes = cmb_mes.SelectedValue;
            sidperiodo = cmb_periodo.SelectedValue;
            siddia =calendar_day.Calendar.SelectedDate.Day.ToString();
            sidproductcategory = cmb_categoria.SelectedValue;

            //if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0" && cmb_periodo.SelectedValue == "0")
            //{
            //    sidaño = DateTime.Now.Year.ToString();
            //    sidmes = DateTime.Now.Month.ToString();
            //    siddia = "0";
            //    sidperiodo ="0";
            //    sidproductcategory = "9463";
            //}
            string sidperdil = this.Session["Perfilid"].ToString();
            if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0" && cmb_periodo.SelectedValue == "0")
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    //aca debe ir la carga inical para el analista

                    //aca debe ir la carga inical para el analista
                    icompany = Convert.ToInt32(this.Session["companyid"]);
                    iservicio = Convert.ToInt32(this.Session["Service"]);
                    canal = this.Session["Canal"].ToString().Trim();
                    Report = Convert.ToInt32(this.Session["Reporte"]);
                    Periodo p = new Periodo(Report, sidciudad, sidproductcategory, sidsub_categoria, sidmarca, sidsub_marca, sidsku, sidpuntoventa, canal, icompany, iservicio);

                    p.Set_PeriodoConDataValidada();

                    sidaño = p.Año;
                    sidmes = p.Mes;
                    sidperiodo = p.PeriodoId;
                    siddia = DateTime.Now.Day.ToString();
                    sidproductcategory = "9463";
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

        private void  LlenarReportDistribuidoras()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            //sidproductcategory = "9463";
            try
            {
                //String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                ////Pedidos vs ingresos
                //rpt_pvi_dist.Visible = true;
                //rpt_pvi_dist.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                //rpt_pvi_dist.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_PEDIDO_INGRESO_DISTRIBUIDORA_SF_AAVV";
                //rpt_pvi_dist.ServerReport.ReportServerUrl = new Uri(strConnection);
                //rpt_pvi_dist.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                //List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("productcategory", sidproductcategory));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("company", icompany.ToString()));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", "2011"));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", "10"));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", "9"));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", "0"));

                //rpt_pvi_dist.ServerReport.SetParameters(parametros);

                ////puntos blancos vs puntos azules
                //rpt_bva_dist.Visible = true;
                //rpt_bva_dist.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                //rpt_bva_dist.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_PEDIDO_INGRESO_DISTRIBUIDORA_AZ_BL_SF_AAVV";
                //rpt_bva_dist.ServerReport.ReportServerUrl = new Uri(strConnection);
                //rpt_bva_dist.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                //List<Microsoft.Reporting.WebForms.ReportParameter> parametros1 = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("productcategory", sidproductcategory));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("company", icompany.ToString()));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", "2011"));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", "10"));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", "9"));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", "0"));

                //rpt_bva_dist.ServerReport.SetParameters(parametros1);

                ////pedidos vs ingresos por nivel
                //rpt_pvi_nivel.Visible = true;
                //rpt_pvi_nivel.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                //rpt_pvi_nivel.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_PEDIDO_INGRESO_NIVEL_SF_AAVV";
                //rpt_pvi_nivel.ServerReport.ReportServerUrl = new Uri(strConnection);
                //rpt_pvi_nivel.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                //List<Microsoft.Reporting.WebForms.ReportParameter> parametros2 = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("productcategory", sidproductcategory));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("company", icompany.ToString()));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", "2011"));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", "10"));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", "9"));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", "0"));

                //rpt_pvi_nivel.ServerReport.SetParameters(parametros2);

                ////pedidos vs ingresos por marca
                //rpt_pvi_marca.Visible = true;
                //rpt_pvi_marca.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                //rpt_pvi_marca.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_PEDIDO_INGRESO_MARCAS_SF_AAVV";
                //rpt_pvi_marca.ServerReport.ReportServerUrl = new Uri(strConnection);
                //rpt_pvi_marca.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                //List<Microsoft.Reporting.WebForms.ReportParameter> parametros3 = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                //parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                //parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                //parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("productcategory", sidproductcategory));
                //parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("company", icompany.ToString()));
                //parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", "2011"));
                //parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", "10"));
                //parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", "9"));
                //parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", "0"));

                //rpt_pvi_marca.ServerReport.SetParameters(parametros3);

                ////rpt_bva_marca
                ////pedidos vs ingresos por marca
                //rpt_bva_marca.Visible = true;
                //rpt_bva_marca.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                //rpt_bva_marca.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_PEDIDO_INGRESO_MARCA_AZ_BL_SF_AAVV";
                //rpt_bva_marca.ServerReport.ReportServerUrl = new Uri(strConnection);
                //rpt_bva_marca.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                //List<Microsoft.Reporting.WebForms.ReportParameter> parametros4 = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                //parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                //parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                //parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("productcategory", sidproductcategory));
                //parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("company", icompany.ToString()));
                //parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", "2011"));
                //parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", "10"));
                //parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", "9"));
                //parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", "0"));

                //rpt_bva_marca.ServerReport.SetParameters(parametros4);


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                //Pedidos vs ingresos
                rpt_pvi_dist.Visible = true;
                rpt_pvi_dist.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                rpt_pvi_dist.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_PEDIDO_INGRESO_DISTRIBUIDORA_SF_AAVV";
                rpt_pvi_dist.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_pvi_dist.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("productcategory", sidproductcategory));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("company", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", siddia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", sidperiodo));

                rpt_pvi_dist.ServerReport.SetParameters(parametros);

                //puntos blancos vs puntos azules
                rpt_bva_dist.Visible = true;
                rpt_bva_dist.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                rpt_bva_dist.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_PEDIDO_INGRESO_DISTRIBUIDORA_AZ_BL_SF_AAVV";
                rpt_bva_dist.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_bva_dist.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros1 = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("productcategory", sidproductcategory));
                parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("company", icompany.ToString()));
                parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", siddia));
                parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", sidperiodo));

                rpt_bva_dist.ServerReport.SetParameters(parametros1);

                //pedidos vs ingresos por nivel
                rpt_pvi_nivel.Visible = true;
                rpt_pvi_nivel.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                rpt_pvi_nivel.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_PEDIDO_INGRESO_NIVEL_SF_AAVV";
                rpt_pvi_nivel.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_pvi_nivel.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros2 = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("productcategory", sidproductcategory));
                parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("company", icompany.ToString()));
                parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", siddia));
                parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", sidperiodo));

                rpt_pvi_nivel.ServerReport.SetParameters(parametros2);

                //pedidos vs ingresos por marca
                rpt_pvi_marca.Visible = true;
                rpt_pvi_marca.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                rpt_pvi_marca.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_PEDIDO_INGRESO_MARCAS_SF_AAVV";
                rpt_pvi_marca.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_pvi_marca.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros3 = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("productcategory", sidproductcategory));
                parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", siddia));
                parametros3.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", sidperiodo));

                rpt_pvi_marca.ServerReport.SetParameters(parametros3);

                //rpt_bva_marca
                //pedidos vs ingresos por marca
                rpt_bva_marca.Visible = true;
                rpt_bva_marca.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                rpt_bva_marca.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_PEDIDO_INGRESO_MARCA_AZ_BL_SF_AAVV";
                rpt_bva_marca.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_bva_marca.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros4 = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("productcategory", sidproductcategory));
                parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("company", icompany.ToString()));
                parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", siddia));
                parametros4.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", sidperiodo));

                rpt_bva_marca.ServerReport.SetParameters(parametros4);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }
        }

        #endregion

        #region Llenado Datos

        private void Años()
        {

            DataTable dty = null;
            dty = Get_Administrativo.Get_ObtenerYears();
            if (dty.Rows.Count > 0)
            {
                cmb_año.DataSource = dty;
                cmb_año.DataValueField = "Years_Number";
                cmb_año.DataTextField = "Years_Number";
                cmb_año.DataBind();
                cmb_año.DataSource = dty;
                cmb_año.DataValueField = "Years_Number";
                cmb_año.DataTextField = "Years_Number";
                cmb_año.DataBind();
                cmb_año.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            }
            else
            {
                dty = null;
            }
        }

        private void Llena_Meses()
        {
            DataTable dtm = Get_Administrativo.Get_ObtenerMeses();

            if (dtm.Rows.Count > 0)
            {
                cmb_mes.DataSource = dtm;
                cmb_mes.DataValueField = "codmes";
                cmb_mes.DataTextField = "namemes";
                cmb_mes.DataBind();


                cmb_mes.DataSource = dtm;
                cmb_mes.DataValueField = "codmes";
                cmb_mes.DataTextField = "namemes";
                cmb_mes.DataBind();
                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            }
            else
            {
                dtm = null;

            }

        }

        private void Llenar_Periodos()
        {
            DataTable dtp = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = 28;
            canal = "1025";
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", canal, icompany, Report, cmb_mes.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                cmb_periodo.DataSource = dtp;
                cmb_periodo.DataValueField = "id_periodo";
                cmb_periodo.DataTextField = "Periodo";
                cmb_periodo.DataBind();

                //cmb_periodo.DataSource = dtp;
                //cmb_periodo.DataValueField = "id_periodo";
                //cmb_periodo.DataTextField = "Periodo";
                //cmb_periodo.DataBind();

               

                cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            }
            else
            {
                dtp = null;
            }
        }

        private void LLenarDiasxPerido()
        {
            calendar_day.Enabled = true;
            DataTable dtp = null;
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_DIAS_DEL_PERIODOS",canal, icompany, Report, cmb_año.SelectedValue, cmb_mes.SelectedValue, Convert.ToInt32(cmb_periodo.SelectedValue));
            if (dtp.Rows.Count > 0)
            {
                
                calendar_day.Calendar.RangeMinDate = Convert.ToDateTime(dtp.Rows[0]["fechaInicial"]);
                calendar_day.Calendar.RangeMaxDate = Convert.ToDateTime(dtp.Rows[0]["fechaFinal"]);
                calendar_day.Calendar.OutOfRangeDayStyle.BackColor = System.Drawing.Color.Magenta;
                calendar_day.EnableTyping = false;
                
            }
            else
            {
                dtp = null;
            }
        }

        private void Categorias()
        {
            DataTable dtcatego = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            dtcatego = Get_DataClientes.Get_Obtenerinfocombos(icompany,canal,"",2);

            if (dtcatego.Rows.Count > 0)
            {

                cmb_categoria.DataSource = dtcatego;
                cmb_categoria.DataValueField = "cod_catego";
                cmb_categoria.DataTextField = "Name_Catego";
                cmb_categoria.DataBind();
                cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));

            }
            else
            {
                cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
                dtcatego = null;
            }
        }

        #endregion





        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Session["channel"] = "1025";
            //this.Session["strategy"] = "254";
            //this.Session["product"] = "1885";
            //this.Session["Año"] = "";
            //this.Session["Mes"] = "";
            //this.Session["Dia"] = "";
            //this.Session["Periodo"] = "";


            sUser = this.Session["sUser"].ToString();
            sPassw = this.Session["sPassw"].ToString();
            sNameUser = this.Session["nameuser"].ToString();
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();
            //Delay();
        
            /// <summary>
            /// Metodo UpdateProgressContext2()
            /// Descripcion: Ejecuta el ProgressBar 
            /// Por: Pablo Salas A.
            /// Fecha:18/06/2011
            /// </summary>

            //UpdateProgressContext2();

            if (!IsPostBack)
            {
                try
                {
                    cmb_año.DataBind();
                    cmb_año.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_mes.DataBind();
                    cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_periodo.DataBind();
                    cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    Categorias();
                    //cmb_dia.DataBind();
                    //cmb_dia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

                    _AsignarVariables();
                    LlenarReportDistribuidoras();

                    cargarParametrosdeXml();

                    Años();
                    Llena_Meses();
                    calendar_day.Enabled = false;

            
                }

                catch (Exception ex)
                {

                    Exception mensaje = ex;
                    this.Session.Abandon();
                    //Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }

        }



        /// <summary>
        /// Metodo UpdateProgressContext2()
        /// Descripcion: Ejecuta el ProgressBar 
        /// Por: Pablo Salas A.
        /// Fecha:18/06/2011
        /// </summary>
        //private void UpdateProgressContext2()
        //{
        //    RadProgressContext context = RadProgressContext.Current;
        //    context.SecondaryTotal = "100";
        //    for (int i = 1; i < 100; i++)
        //    {
        //        context.SecondaryValue = i.ToString();
        //        context.SecondaryPercent = i.ToString();
        //        context.CurrentOperationText = "Procesando " + i.ToString() + "%";
        //        context.CurrentOperationText = " Cargando Informes de Precios...";

        //        if (!Response.IsClientConnected)
        //        {
        //            //Cancel button was clicked or the browser was closed, so stop processing
        //            break;
        //        }
        //        // simulate a long time performing the current step
        //        System.Threading.Thread.Sleep(100);
        //    }
        //}


        private void UpdateProgressContext2()
        {
            const int total = 100;

            RadProgressContext progress = RadProgressContext.Current;

            for (int i = 0; i < total; i++)
            {

                progress.PrimaryTotal = 1;
                progress.PrimaryValue = 1;
                progress.PrimaryPercent = 100;

                progress.SecondaryTotal = total;
                progress.SecondaryValue = i;
                progress.SecondaryPercent = i;

                progress.CurrentOperationText = "Precios Canal Mayorista";

                if (!Response.IsClientConnected)
                {
                    //Cancel button was clicked or the browser was closed, so stop processing
                    break;
                }
                progress.Speed = i;
                //Stall the current thread for 0.1 seconds
                System.Threading.Thread.Sleep(100);


            }
        }
        //private void Delay()
        //{


        //    for (int i = 0; i < 100; i++)
        //    {
        //        lbltime.Text = i.ToString();




        //    }


        //}

        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Llenar_Periodos();
        }

        protected void btn_ocultar_Click(object sender, EventArgs e)
        {
            if (Div_filtros.Visible == false)
            {

                Div_filtros.Visible = true;

                btn_ocultar.Text = "Ocultar";
                btngnerar.Visible = true;
            }
            else if (Div_filtros.Visible == true)
            {
                Div_filtros.Visible = false;
                btn_ocultar.Text = "Filtros";
                btngnerar.Visible = false;
            }
        }

        protected void btngnerar_Click(object sender, EventArgs e)
        {
            _AsignarVariables();
            LlenarReportDistribuidoras();
        }

        //protected void buttonGuardar_Click(object sender, EventArgs e)
        //{
        //    string path = Server.MapPath("~/parametros.xml");
        //    Reportes_parametros oReportes_parametros = new Reportes_parametros();


        //    oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
        //    oReportes_parametros.Id_user = this.Session["sUser"].ToString();
        //    oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
        //    oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
        //    oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

        //    oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_ciudad.SelectedValue);
        //    oReportes_parametros.Id_punto_venta = cmb_punto_de_venta.SelectedValue;
        //    oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
        //    oReportes_parametros.Id_subCategoria = cmb_subCategoria.SelectedValue;
        //    string sidmarca = cmb_marca.SelectedValue;
        //    if (sidmarca == "")
        //        sidmarca = "0";
        //    oReportes_parametros.Id_producto_marca = sidmarca;
        //    oReportes_parametros.Id_subMarca = cmb_subMarca.SelectedValue;
        //    oReportes_parametros.SkuProducto = cmb_skuProducto.SelectedValue;
        //    oReportes_parametros.Id_año = cmb_año.SelectedValue;
        //    oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
        //    oReportes_parametros.Id_periodo = Convert.ToInt32(cmb_periodo.SelectedValue);

        //    //string descripcion = cmb_año.SelectedItem  + "-" + cmb_periodo.SelectedItem + "-" +
        //    //   cmb_oficina.SelectedItem + "-" + cmb_punto_de_venta.SelectedItem + "-" + cmb_categoria.SelectedItem + "-" +
        //    // cmb_marca.SelectedItem + "-" + cmb_familia.SelectedItem;

        //    oReportes_parametros.Descripcion = txt_descripcion_parametros.Text.Trim();

        //    Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

        //    if (!System.IO.File.Exists(path))
        //        oReportes_parametrosToXml.createXml(oReportes_parametros, path);
        //    else
        //        oReportes_parametrosToXml.addToXml(oReportes_parametros, path);


        //    cargarParametrosdeXml();
        //    txt_descripcion_parametros.Text = "";
        //    TabContainer_filtros.ActiveTabIndex = 1;
        //}

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
        }

        protected void RadGrid_parametros_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "BUSCAR")
            {
                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_periodo = (Label)e.Item.FindControl("lbl_id_periodo");
                Label lbl_id_oficina = (Label)e.Item.FindControl("lbl_id_oficina");
                Label lbl_id_pdv = (Label)e.Item.FindControl("lbl_id_pdv");
                Label lbl_id_categoria = (Label)e.Item.FindControl("lbl_id_categoria");
                Label lbl_id_subCategoria = (Label)e.Item.FindControl("lbl_id_subCategoria");
                Label lbl_id_marca = (Label)e.Item.FindControl("lbl_id_marca");
                Label lbl_id_subMarca = (Label)e.Item.FindControl("lbl_id_subMarca");
                Label lbl_skuProducto = (Label)e.Item.FindControl("lbl_skuProducto");

                sidaño = lbl_id_año.Text.Trim();
                sidmes = lbl_id_mes.Text.Trim();
                sidperiodo = lbl_id_periodo.Text.Trim();
                sidciudad = lbl_id_oficina.Text.Trim();
                sidproductcategory = lbl_id_categoria.Text.Trim();
                sidsub_categoria = lbl_id_subCategoria.Text.Trim();
                sidmarca = lbl_id_marca.Text.Trim();
                sidsub_marca = lbl_id_subMarca.Text.Trim();
                sidsku = lbl_skuProducto.Text.Trim();
                sidpuntoventa = lbl_id_pdv.Text.Trim();


                LlenarReportDistribuidoras();

                //llenarreporteCompa();
                //llenarreporteCompaciuda();
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
            if (e.CommandName == "EDITAR")
            {
                Label lbl_id = (Label)e.Item.FindControl("lbl_id");
                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_periodo = (Label)e.Item.FindControl("lbl_id_periodo");
                Label lbl_id_oficina = (Label)e.Item.FindControl("lbl_id_oficina");
                Label lbl_id_pdv = (Label)e.Item.FindControl("lbl_id_pdv");
                Label lbl_id_categoria = (Label)e.Item.FindControl("lbl_id_categoria");
                Label lbl_id_subCategoria = (Label)e.Item.FindControl("lbl_id_subCategoria");
                Label lbl_id_marca = (Label)e.Item.FindControl("lbl_id_marca");
                Label lbl_id_subMarca = (Label)e.Item.FindControl("lbl_id_subMarca");
                Label lbl_skuProducto = (Label)e.Item.FindControl("lbl_skuProducto");

                Session["idxml"] = lbl_id.Text.Trim();
                cmb_año.SelectedIndex = cmb_año.Items.FindItemByValue(lbl_id_año.Text).Index;
                cmb_mes.SelectedIndex = cmb_mes.Items.FindItemByValue(lbl_id_mes.Text).Index;
                Llenar_Periodos();
                cmb_periodo.SelectedIndex = cmb_periodo.FindItemByValue(lbl_id_periodo.Text).Index;
                //cmb_ciudad.SelectedIndex = cmb_ciudad.Items.FindItemByValue(lbl_id_oficina.Text).Index;
                //cmb_punto_de_venta.SelectedIndex = cmb_punto_de_venta.Items.FindItemByValue(lbl_id_pdv.Text).Index;
                //cmb_categoria.SelectedIndex = cmb_categoria.Items.FindItemByValue(lbl_id_categoria.Text).Index;
                //Subcategorias();
                //cmb_subCategoria.SelectedIndex=cmb_subCategoria.Items.FindItemByValue(lbl_id_subCategoria.Text).Index;
                //cmb_marca.SelectedIndex = cmb_marca.Items.FindItemByValue(lbl_id_marca.Text).Index;
                //cmb_subMarca.SelectedIndex = cmb_subMarca.Items.FindItemByValue(lbl_id_subMarca.Text).Index;
                //cmb_skuProducto.SelectedIndex = cmb_skuProducto.Items.FindItemByValue(lbl_skuProducto.Text).Index;

                TabContainer_filtros.ActiveTabIndex = 0;
                lbl_updateconsulta.Visible = true;
                btn_img_actualizar.Visible = true;

                lbl_saveconsulta.Visible = false;
                btn_img_add.Visible = false;
            }
        }
        //protected void btn_actualizar_Click(object sender, EventArgs e)
        //{
        //    string path = Server.MapPath("~/parametros.xml");
        //    Reportes_parametros oReportes_parametros = new Reportes_parametros();

        //    oReportes_parametros.Id = Convert.ToInt32(Session["idxml"].ToString());
        //    oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
        //    oReportes_parametros.Id_user = this.Session["sUser"].ToString();
        //    oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
        //    oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
        //    oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

        //    oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_ciudad.SelectedValue);
        //    oReportes_parametros.Id_punto_venta = cmb_punto_de_venta.SelectedValue;
        //    oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
        //    oReportes_parametros.Id_subCategoria = cmb_subCategoria.SelectedValue;
        //    string sidmarca = cmb_marca.SelectedValue;
        //    if (sidmarca == "")
        //        sidmarca = "0";
        //    oReportes_parametros.Id_producto_marca = sidmarca;
        //    oReportes_parametros.Id_subMarca = cmb_subMarca.SelectedValue;
        //    oReportes_parametros.SkuProducto = cmb_skuProducto.SelectedValue;
        //    oReportes_parametros.Id_año = cmb_año.SelectedValue;
        //    oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
        //    oReportes_parametros.Id_periodo = Convert.ToInt32(cmb_periodo.SelectedValue);

        //    oReportes_parametros.Descripcion = txt_pop_actualiza.Text.Trim();

        //    Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

        //    if (System.IO.File.Exists(path))
        //    {
        //        if (oReportes_parametrosToXml.DeleteElement(oReportes_parametros, path))
        //        {
        //            oReportes_parametrosToXml.addToXml(oReportes_parametros, path);
        //            cargarParametrosdeXml();
        //        }

        //    }

        //    lbl_saveconsulta.Visible = true;
        //    btn_img_add.Visible = true;

        //    lbl_updateconsulta.Visible = false;
        //    btn_img_actualizar.Visible = false;

        //    TabContainer_filtros.ActiveTabIndex = 1;
        //}
        #region Logica de la Validación del Reporte

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
        #endregion
        protected void buttonGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_img_add_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
        }

        protected void cmb_periodo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LLenarDiasxPerido();
            
        }

    }
}