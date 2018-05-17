using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Lucky.CFG.JavaMovil;
using Lucky.CFG.Util;
//using LuckyMer.Contracts.DataContract;
using System.Configuration;
using System.Data;
using Lucky.Data;

namespace SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_SOD
{
    public partial class Reporte_v2_SOD_EvolucionSOD : System.Web.UI.Page
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
                CargarCategorias();
                CargarMarcas();
                CargarRegion();
                
                cargarReportSOD();
            }
        }
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
        private void CargarCategorias()
        {
            RadComboBox cmb_categoria = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_categoria") as RadComboBox;

            DataTable dtcatego = null;

            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            dtcatego = Get_DataClientes.Get_Obtenerinfocombos_F(iidcompany, sidcanal, Report, "0", 2);

            cmb_categoria.DataSource = dtcatego;
            cmb_categoria.DataValueField = "cod_catego";
            cmb_categoria.DataTextField = "Name_Catego";
            cmb_categoria.DataBind();
            cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));

        }
        protected void cmb_categoria_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox cmb_marca = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_marca") as RadComboBox;
            RadComboBox cmb_categoria = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_categoria") as RadComboBox;
            if (cmb_categoria.SelectedIndex > 0)
            {
                CargarMarcas();
            }
            else
            {
                cmb_marca.Items.Clear();
                cmb_marca.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
            }
        }
        private void CargarMarcas()
        {
            RadComboBox cmb_marca = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_marca") as RadComboBox;
            RadComboBox cmb_categoria = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_categoria") as RadComboBox;

            DataTable dtm = null;
            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());

            dtm = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIEN_V2_LLENARMARCAS_PRECIOS",cmb_categoria.SelectedValue, 0, iidcompany);


            cmb_marca.DataSource = dtm;
            cmb_marca.DataValueField = "id_Brand";
            cmb_marca.DataTextField = "Name_Brand";
            cmb_marca.DataBind();
            cmb_marca.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
        }
        protected void CargarRegion()
        {
            RadComboBox cmb_region = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_region") as RadComboBox;

            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();

            DataTable dtr = oCoon.ejecutarDataTable("UP_WEBXPLORA_OBTENER_MALLA_BY_CHANNEL_AND_CLIENTE", sidcanal, iidcompany);

            cmb_region.DataSource = dtr;
            cmb_region.DataValueField = "id_malla";
            cmb_region.DataTextField = "malla";
            cmb_region.DataBind();
            cmb_region.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));

        }
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
        protected void btn_generar_Click(object sender, EventArgs e)
        {
            cargarReportSOD();
        }
        private void cargarReportSOD()
        {         
            RadComboBox cmb_año = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_año") as RadComboBox;
            RadComboBox cmb_mes = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_mes") as RadComboBox;
            RadComboBox cmb_ciudad = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_ciudad") as RadComboBox;
            RadComboBox cmb_marca = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_marca") as RadComboBox;
            RadComboBox cmb_categoria = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_categoria") as RadComboBox;
            RadComboBox cmb_region = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_region") as RadComboBox;

            Informes_validacion.UC_ValidarPeriodos UC_ValidarPeriodos1 = RadPanelBar_menu.FindChildByValue<RadPanelItem>("validar").FindControl("UC_ValidarPeriodos1") as Informes_validacion.UC_ValidarPeriodos;

            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            string sidperdil = this.Session["Perfilid"].ToString();

            string año, mes, periodo;
            string validacion;

            año = cmb_año.SelectedValue;
            mes = cmb_mes.SelectedValue;

            if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0")
            {
                P.Servicio = iservicio;
                P.Canal = sidcanal;
                P.Cliente = iidcompany;
                P.SetPeriodoInicial_SOD();

                año = P.Año;
                mes = P.Mes;


                cmb_año.SelectedIndex = cmb_año.Items.FindItemIndexByValue(año);
                cmb_mes.SelectedIndex = cmb_mes.Items.FindItemIndexByValue(mes);

            }

            if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
            {
                RadPanelBar_menu.FindChildByValue<RadPanelItem>("itemValidacion").Visible = true;
                UC_ValidarPeriodos1.SetValidacion(iservicio, sidcanal, iidcompany, Report, año, mes, "1");
                validacion = "0";
            }
            else
            {
                RadPanelBar_menu.FindChildByValue<RadPanelItem>("itemValidacion").Visible = false;
                validacion = "1";
            }

            Reporte_contenido.Visible = true;

            Reporte_contenido.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            Reporte_contenido.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
            Reporte_contenido.ServerReport.ReportPath = "/Reporte_Precios_V1/Evolucion_SOD_Mayorista";

            String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

            Reporte_contenido.ServerReport.ReportServerUrl = new Uri(strConnection);
            Reporte_contenido.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
            List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", cmb_año.SelectedValue));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", cmb_mes.SelectedValue));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("OFICINA", "0"));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", (cmb_categoria.SelectedValue == "0" ? "12" : cmb_categoria.SelectedValue)));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MARCA", cmb_marca.SelectedValue));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PDV", "0"));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", sidcanal));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", iidcompany.ToString()));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", iservicio.ToString()));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("ValidAnalyst", validacion));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_mallas", (cmb_region.SelectedValue == "0" ? "2" : cmb_region.SelectedValue)));

            Reporte_contenido.ServerReport.SetParameters(parametros);
        }

       
    }
}