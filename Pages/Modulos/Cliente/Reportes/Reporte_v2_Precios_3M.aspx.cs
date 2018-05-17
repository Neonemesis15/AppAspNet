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
using System.Xml;
using System.Text;

namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_v2_Precios_3M : System.Web.UI.Page
    {
        private int iidcompany;
        private string sidcanal;

        string sUser;
        string sPassw;
        string sNameUser;
        int iservicio;
        string canal;
        int Report;
        ReportViewer reporte1;
        ReportViewer repordet;
        ReportViewer repvquincenal;
        ReportViewer repbrecmarg;
        ReportViewer repIndice;
        ReportViewer repcompa;
        ReportViewer repcompaciu;
        ReportViewer reppanel;
        ReportViewer repCumpliLayout;


        private string sidaño;
        private string sidmes;
        private string sidperiodo;
        private int icadena;

        private string sidcategoria;
        private int inegocio;



        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();
        Facade_Proceso_Planning.Facade_Proceso_Planning Get_Proceso_Planning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        Conexion oCoon = new Conexion();

        protected void Page_Load(object sender, EventArgs e)
        {
            //UpdateProgressContext2();
            if (!Page.IsPostBack)
            {                
                cargarCadena();
                cargarAños();
                cargarMes();
                cargarCategorias();
                cargarSemana();
                _AsignarVariables();                
                GenerarReportePrecios();
            }
        }

        private void GenerarReportePrecios()
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {
                repordet = (ReportViewer)(Reporte_v2_Precio_InformePrecio_3M1.FindControl("reportePrecios"));
                repordet.Visible = true;
                repordet.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                repordet.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_PRECIO_3M";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                repordet.ServerReport.ReportServerUrl = new Uri(strConnection);
                repordet.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", "1562"));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", "1000"));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA","74"));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", "07"));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SEMANA", "1"));
               // parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("POINTOFSALE", "0"));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("OFICINA", "30"));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("FAMILIA", "F0061"));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PRODUCTO", "0"));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MPointOfPurchase", "0"));
                repordet.ServerReport.SetParameters(parametros);

            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
            }
        }

        private void cargarSemana()
        {            
            cmb_Semana.Items.Insert(0, new RadComboBoxItem("1", "1"));
            cmb_Semana.Items.Insert(1, new RadComboBoxItem("2", "2"));
            cmb_Semana.Items.Insert(2, new RadComboBoxItem("3", "3"));
            cmb_Semana.Items.Insert(3, new RadComboBoxItem("4", "4"));
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

                progress.CurrentOperationText = "Exh. Adicionales Canal AASS";

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
        private void UpdateProgressContext3()
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
                
                progress.CurrentOperationText = "Cargando Puntos de Ventas";

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
        protected void cargarCadena()
        {
            DataTable dt = null;
            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERCADENAS", iidcompany, sidcanal);

            if (dt.Rows.Count > 0)
            {
                cmb_cadena.DataSource = dt;
                cmb_cadena.DataValueField = "id_cadena";
                cmb_cadena.DataTextField = "Cadena";
                cmb_cadena.DataBind();

                cmb_cadena.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
            }
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


                cmb_año.DataSource = dty;
                cmb_año.DataValueField = "Years_Number";
                cmb_año.DataTextField = "Years_Number";
                cmb_año.DataBind();
                cmb_año.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
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
                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            }
        }
        private void cargarCategorias()
        {
            DataTable dtcatego = null;
            dtcatego = Get_DataClientes.Get_Obtenerinfocombos(iidcompany, sidcanal, "", 2);
            if (dtcatego.Rows.Count > 0)
            {
                cmb_categoria.DataSource = dtcatego;
                cmb_categoria.DataValueField = "cod_catego";
                cmb_categoria.DataTextField = "Name_Catego";
                cmb_categoria.DataBind();
                cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));

            }
            else
            {
                dtcatego = null;
            }
        }
        private void _AsignarVariables()
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);


            sidaño = cmb_año.SelectedValue;
            sidmes = cmb_mes.SelectedValue;

            ///sidperiodo = cmb_periodo.SelectedValue;
            icadena = Convert.ToInt32(cmb_cadena.SelectedValue);


            sidcategoria = cmb_categoria.SelectedValue;
            // inegocio = Convert.ToInt32(cmb_negocio.SelectedValue);



            string sidperdil = this.Session["Perfilid"].ToString();
            if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0")
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {

                    Periodo p = new Periodo(Report, icadena, sidcategoria, canal, iidcompany, iservicio);
                    p.Set_PeriodoConDataValidada();
                    sidaño = p.Año;
                    sidmes = p.Mes;
                    sidperiodo = p.PeriodoId;

                    cmb_año.SelectedIndex = cmb_año.Items.FindItemIndexByValue(DateTime.Now.Year.ToString());
                    GetPeridForAnalist();

                }
                else if (sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteGeneral"])
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
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO", canal, Report, sidaño, sidmes, 1);

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
        protected void cmb_categoria_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            DataTable dtFamilia = null;
            dtFamilia = Get_DataClientes.Get_Obtenerinfocombos(iidcompany, sidcanal, cmb_categoria.SelectedValue, 4);

            if (dtFamilia != null)
            {
                cmb_categoria.DataSource = dtFamilia;
                cmb_categoria.DataValueField = "name_Family";
                cmb_categoria.DataTextField = "id_ProductFamily";
                cmb_categoria.DataBind();
                cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));

            }
            else
            {
                dtFamilia = null;
            }
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

            oReportes_parametros.Id_producto_familia = (cmb_Familia.SelectedValue);
            oReportes_parametros.Id_producto_categoria = (cmb_categoria.SelectedValue);
            oReportes_parametros.Id_Semana = Convert.ToInt32(cmb_Semana.SelectedValue);
            oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_cadena.SelectedValue);

            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            //oReportes_parametros.Id_tipoReporte = cmb_tipoExhibicion.SelectedValue;


            oReportes_parametros.Descripcion = txt_descripcion_parametros.Text.Trim();

            Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

            if (!System.IO.File.Exists(path))
                oReportes_parametrosToXml.createXml(oReportes_parametros, path);
            else
                oReportes_parametrosToXml.addToXml(oReportes_parametros, path);


            cargarParametrosdeXml();
            txt_descripcion_parametros.Text = "";
            TabContainer_filtros.ActiveTabIndex = 1;
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

        protected void RadGrid_parametros_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            if (e.CommandName == "BUSCAR")
            {
                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_periodo = (Label)e.Item.FindControl("lbl_id_periodo");
                Label lbl_id_oficina = (Label)e.Item.FindControl("lbl_id_oficina");
                Label lbl_id_tipoReporte = (Label)e.Item.FindControl("lbl_id_tipoReporte");
                //crearXML(iidcompany, sidcanal, lbl_id_tipoReporte.Text.Trim(), lbl_id_oficina.Text.Trim(), lbl_id_año.Text.Trim(), lbl_id_mes.Text.Trim());
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
    }
}