﻿using System;
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
    public partial class Reporte_v2_Competencia_3M : System.Web.UI.Page
    {
        #region Declaración de Asignaciones Generales
        Conexion oCoon = new Conexion();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();

        string sUser;
        string sPassw;
        string sNameUser;
        int icompany;
        int iservicio;
        string canal;
        string quiebre;
        int Report;


        #endregion


        private string sidaño;
        private string sidmes;
        private string sidperiodo;
        private string sidcategoria;
        private string sdias;
        private int icadena;

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Session["catego"] = "0";
            this.Session["subcatego"] = "0";

            this.Session["Año"] = "0";
            this.Session["Mes"] = "0";

            sUser = this.Session["sUser"].ToString();
            sPassw = this.Session["sPassw"].ToString();
            sNameUser = this.Session["nameuser"].ToString();
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();
            //Delay();
            UpdateProgressContext2();

            if (!IsPostBack)
            {
                try
                {







                    cmb_cadena.DataBind();
                    cmb_cadena.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

                    cmbTienda.DataBind();
                    cmbTienda.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));



                    llenarCadena();
                    llenarPuntoVenta();

                    _AsignarVariables();
                    ObtenerInformeCompetencia();



                }

                catch (Exception ex)
                {

                    Exception mensaje = ex;
                    this.Session.Abandon();
                    //Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
        }

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

                progress.CurrentOperationText = "Reporte De Disponibilidad";

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

        #region Llenado Datos

        protected void llenarPuntoVenta()
        {
            DataTable dt = null;
            icompany = Convert.ToInt32(this.Session["companyid"].ToString());
            canal = this.Session["Canal"].ToString();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPUNTOSDEVENTA_CLIENTE_3M", icompany, cmb_cadena.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                cmbTienda.DataSource = dt;
                cmbTienda.DataValueField = "id_PointOfsale";
                cmbTienda.DataTextField = "pdv_Name";
                cmbTienda.DataBind();
                cmbTienda.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
            }

        }



        protected void llenarCadena()
        {
            DataTable dt = null;
            icompany = Convert.ToInt32(this.Session["companyid"].ToString());
            canal = this.Session["Canal"].ToString();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERCADENAS", icompany, canal);

            if (dt.Rows.Count > 0)
            {
                cmb_cadena.DataSource = dt;
                cmb_cadena.DataValueField = "id_cadena";
                cmb_cadena.DataTextField = "Cadena";
                cmb_cadena.DataBind();

                cmb_cadena.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
            }

        }



        private void ObtenerInformeCompetencia()
        {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);


            try
            {

                ReporPrecio.Visible = true;
                ReporPrecio.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                ReporPrecio.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_Competencia_3M";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                ReporPrecio.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReporPrecio.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("companyid", Convert.ToString(icompany)));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("codstrategy", Convert.ToString(iservicio)));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Codchannel", canal));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", cmb_año.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("desde", dtpDesde.Calendar.SelectedDate.ToShortDateString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("hasta", dtpHasta.Calendar.SelectedDate.ToShortDateString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Cadena", cmb_cadena.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Tienda", cmbTienda.SelectedValue));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familias", cmbFamilia.SelectedValue));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Producto", cmbProducto.SelectedValue));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("departamento", cmbDepartamento.SelectedValue));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("DIA", sdias));





                ReporPrecio.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


            }
        }

        #endregion

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

        protected void cmb_periodo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // cmb_Dias.Items.Clear();
        }
        protected void btn_imb_tab_Click(object sender, ImageClickEventArgs e)
        {
            TabContainer_filtros.ActiveTabIndex = 0;
        }
        protected void btngnerar_Click(object sender, EventArgs e)
        {
            _AsignarVariables();
            ObtenerInformeCompetencia();

        }
        private void _AsignarVariables()
        {

            icadena = Convert.ToInt32(cmb_cadena.SelectedValue);

            //sdias = cmb_Dias.SelectedValue;



            string sidperdil = this.Session["Perfilid"].ToString();
            if (dtpDesde.Calendar.SelectedDate.Day.ToString() == "" && dtpHasta.Calendar.SelectedDate.Day.ToString() == "")
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    //aca debe ir la carga inical para el analista

                    //aca debe ir la carga inical para el analista
                    icompany = Convert.ToInt32(this.Session["companyid"]);
                    iservicio = Convert.ToInt32(this.Session["Service"]);
                    canal = this.Session["Canal"].ToString().Trim();
                    Report = Convert.ToInt32(this.Session["Reporte"]);
                    Periodo p = new Periodo(Report, icadena, sidcategoria, canal, icompany, iservicio);

                    p.Set_PeriodoConDataValidada();

                    sidaño = p.Año;
                    sidmes = p.Mes;
                    sidperiodo = p.PeriodoId;

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

    }
}