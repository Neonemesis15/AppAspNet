using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Data;
using Lucky.CFG.Util;
using Telerik.Web.UI;
using System.Data;
//using LuckyMer.Contracts.DataContract;
using Lucky.CFG.JavaMovil;
using System.Configuration;

namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_v2_PresenciaColgate_Bodegas : System.Web.UI.Page
    {
        private int iidcompany;
        private string sidcanal;
        private int iservicio;
        private int Report;

        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();
        Conexion oCoon = new Conexion();
        Periodo P = new Periodo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarMenu();
                cargarAño();
                cargarMes();
                cargarPeriodo();
                Cobertura();

                //cargarReportProduct();
            }
        }

    #region LLenar Combos
        public void cargarAño()
        {
            RadComboBox cmb_año = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_año") as RadComboBox;


            DataTable dty = null;
            dty = Get_Administrativo.Get_ObtenerYears();

            cmb_año.DataSource = dty;
            cmb_año.DataValueField = "Years_Number";
            cmb_año.DataTextField = "Years_Number";
            cmb_año.DataBind();

            cmb_año.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
        }

        public void cargarMes()
        {
            RadComboBox cmb_mes = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_mes") as RadComboBox;


            DataTable dtm = Get_Administrativo.Get_ObtenerMeses();

            cmb_mes.DataSource = dtm;
            cmb_mes.DataValueField = "codmes";
            cmb_mes.DataTextField = "namemes";
            cmb_mes.DataBind();
            cmb_mes.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
        }

        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            UpdatePanel up_filtros = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("up_filtros") as UpdatePanel;

            cargarPeriodo();
            up_filtros.Update();
        }

        public void cargarPeriodo()
        {
            RadComboBox cmb_periodo = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_periodo") as RadComboBox;
            RadComboBox cmb_año = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_año") as RadComboBox;
            RadComboBox cmb_mes = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_mes") as RadComboBox;

            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            cmb_periodo.Items.Clear();
            cmb_periodo.Enabled = true;
            DataTable dtp = null;

            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PERIODOS_POR_MES", iservicio, sidcanal, iidcompany, Report, cmb_año.SelectedValue, cmb_mes.SelectedValue);

            cmb_periodo.DataSource = dtp;
            cmb_periodo.DataValueField = "ReportsPlanning_Periodo";
            cmb_periodo.DataTextField = "Descripcion";
            cmb_periodo.DataBind();

            cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
        }
        
        private void Cobertura()
        {
            string canal = this.Session["Canal"].ToString().Trim();

            RadComboBox cmb_cobertura = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_cobertura") as RadComboBox;

            DataTable dtc = null;
            dtc = oCoon.ejecutarDataTable("UP_WEBXPLORA_CARGARCOBERTURA_COLGATE", iidcompany, canal);

            cmb_cobertura.DataSource = dtc;
            cmb_cobertura.DataValueField = "cod_Agrupacion";
            cmb_cobertura.DataTextField = "Oficina_descripcion";
            cmb_cobertura.DataBind();
        }
    #endregion

    #region Cargar Menu 
        public void cargarMenu()
        {
            MenuService1.MenuServiceClient client = new MenuService1.MenuServiceClient("BasicHttpBinding_IMenuService");


            RadMenu rad_menu = RadPanelBar_menu.FindChildByValue<RadPanelItem>("menu").FindControl("rad_menu") as RadMenu;

            string dataJson;
            string request;

            request = "{'i':'" + Session["id_menu"].ToString() + "'}";
            dataJson = client.ObtenerMenuDetalle(request);
            //MenuServiceResponse menuServiceResponse = HelperJson.Deserialize<MenuServiceResponse>(dataJson);

            MenuLoadUtil oLoadMenu = new MenuLoadUtil();
            //rad_menu = oLoadMenu.LoadRadMenu(rad_menu, menuServiceResponse);

        }
    #endregion

    #region GenerarInforme
        protected void btn_generar_Click(object sender, EventArgs e)
        {
            cargarReportProduct();
        }
    #endregion

    #region Eventos del Menu
        protected void RadMenu_ItemClick(object sender, RadMenuEventArgs e)
        {
            int i = 0;
            string a = e.Item.Value.ToString();
        } 
    #endregion

    #region GenerarReportes
        private void cargarReportProduct()
        {
            RadComboBox cmb_periodo = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_periodo") as RadComboBox;
            RadComboBox cmb_año = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_año") as RadComboBox;
            RadComboBox cmb_mes = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_mes") as RadComboBox;
            RadComboBox cmb_cobertura = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_cobertura") as RadComboBox;
            Informes_validacion.UC_ValidarPeriodos UC_ValidarPeriodos1 = RadPanelBar_menu.FindChildByValue<RadPanelItem>("validar").FindControl("UC_ValidarPeriodos1") as Informes_validacion.UC_ValidarPeriodos;


            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            string sidperdil = this.Session["Perfilid"].ToString();

            string id_categoria, año, mes, periodo;
            bool validacion;
            id_categoria = Request.QueryString["cod"];

            año = cmb_año.SelectedValue;
            mes = cmb_mes.SelectedValue;
            periodo = cmb_periodo.SelectedValue;

            if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0")
            {

                P.Servicio = iservicio;
                P.Canal = sidcanal;
                P.Cliente = iidcompany;
                P.SetPeriodoInicial_Presencia();

                año = P.Año;
                mes = P.Mes;
                periodo = P.PeriodoId;

                cmb_año.SelectedIndex = cmb_año.Items.FindItemIndexByValue(año);
                cmb_mes.SelectedIndex = cmb_mes.Items.FindItemIndexByValue(mes);
                cargarPeriodo();
                cmb_periodo.SelectedIndex = cmb_periodo.Items.FindItemIndexByValue(periodo);
            }

            if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
            {
                RadPanelBar_menu.FindChildByValue<RadPanelItem>("itemValidacion").Visible = true;
                UC_ValidarPeriodos1.SetValidacion(iservicio, sidcanal, iidcompany, Report, año, mes, periodo);
                validacion = false;
            }
            else
            {
                RadPanelBar_menu.FindChildByValue<RadPanelItem>("itemValidacion").Visible = false;
                validacion = true;
            }










            ReportProductos.Visible = true;

            ReportProductos.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportProductos.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
            ReportProductos.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_Presencia_Productos_FarmDT";

            string strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

            ReportProductos.ServerReport.ReportServerUrl = new Uri(strConnection);
            ReportProductos.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
            List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", año));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", mes));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", periodo));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", cmb_cobertura.SelectedValue));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", (id_categoria == null ? "9994" : id_categoria)));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", sidcanal));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", iidcompany.ToString()));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", iservicio.ToString()));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("VALIDANALYST", validacion.ToString()));

            ReportProductos.ServerReport.SetParameters(parametros);


        }
    #endregion
    }
}