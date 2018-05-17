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
using AjaxControlToolkit;


namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_v2_PresenciaColgate_IP_IT : System.Web.UI.Page
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
                Ciudad();

                cargarReportIndexPrice();
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

        private void Ciudad()
        {
            RadComboBox cmb_ciudad = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_ciudad") as RadComboBox;

            DataTable dtc = null;
            dtc = Get_DataClientes.Get_Obtenerinfocombos(iidcompany, sidcanal, "0", 1);

            cmb_ciudad.DataSource = dtc;
            cmb_ciudad.DataValueField = "cod_city";
            cmb_ciudad.DataTextField = "name_city";
            cmb_ciudad.DataBind();
            cmb_ciudad.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
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
            cargarReportIndexPrice();
        }
        private void cargarReportIndexPrice()
        {
            RadComboBox cmb_periodo = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_periodo") as RadComboBox;
            RadComboBox cmb_año = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_año") as RadComboBox;
            RadComboBox cmb_mes = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_mes") as RadComboBox;
            RadComboBox cmb_ciudad = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_ciudad") as RadComboBox;
            Informes_validacion.UC_ValidarPeriodos UC_ValidarPeriodos1 = RadPanelBar_menu.FindChildByValue<RadPanelItem>("validar").FindControl("UC_ValidarPeriodos1") as Informes_validacion.UC_ValidarPeriodos;

            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            string sidperdil = this.Session["Perfilid"].ToString();

            string año, mes, periodo;
            bool validacion;

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

            ReportIndexPrice.Visible = true;

            ReportIndexPrice.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportIndexPrice.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
            ReportIndexPrice.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_IndexPriceResumenIT";

            String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

            ReportIndexPrice.ServerReport.ReportServerUrl = new Uri(strConnection);
            ReportIndexPrice.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
            List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", cmb_año.SelectedValue));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", cmb_mes.SelectedValue));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", cmb_periodo.SelectedValue));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", cmb_ciudad.SelectedValue));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", sidcanal));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", iidcompany.ToString()));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", iservicio.ToString()));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("VALIDANALYST", "false"));


            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", "0"));// siempre se pasa en 0
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("TIPOCIUDAD", "0"));// siempre se pasa en 0

            ReportIndexPrice.ServerReport.SetParameters(parametros);
        }
        #region Gestion consultas

        protected void Click_btn_img_ver(object sender, EventArgs e)
        {
            ModalPopupExtender mpe_ver = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("mpe_ver") as ModalPopupExtender;
            cargarParametrosdeXml();
            mpe_ver.Show();
        }
        protected void cargarParametrosdeXml()
        {
            RadGrid RadGrid_parametros = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("RadGrid_parametros") as RadGrid;
            string path = Server.MapPath("~/parametros.xml");

            if (System.IO.File.Exists(path))
            {
                Reportes_parametros oReportes_parametros = new Reportes_parametros();
                oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
                oReportes_parametros.Id_user = this.Session["sUser"].ToString();
                oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
                oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
                oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();
                oReportes_parametros.Id_tipoReporte = this.Request.Path;

                Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();
                try
                {
                    RadGrid_parametros.DataSource = oReportes_parametrosToXml.Get_Parametros(oReportes_parametros, path);
                    RadGrid_parametros.DataBind();
                }
                catch
                {

                }
            }
        }
        protected void buttonGuardar_Click(object sender, EventArgs e)
        {
            RadComboBox cmb_periodo = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_periodo") as RadComboBox;
            RadComboBox cmb_año = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_año") as RadComboBox;
            RadComboBox cmb_mes = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_mes") as RadComboBox;
            RadComboBox cmb_cadena = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_cadena") as RadComboBox;
            RadComboBox cmb_ciudad = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_ciudad") as RadComboBox;
            RadComboBox cmb_cobertura = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_cobertura") as RadComboBox;
            TextBox txt_descripcion_parametros = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("txt_descripcion_parametros") as TextBox;
            ModalPopupExtender mpe_ver = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("mpe_ver") as ModalPopupExtender;

            string path = Server.MapPath("~/parametros.xml");
            Reportes_parametros oReportes_parametros = new Reportes_parametros();

            oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
            oReportes_parametros.Id_user = this.Session["sUser"].ToString();
            oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
            oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
            oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

            oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_ciudad.SelectedValue);

            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            oReportes_parametros.Id_periodo = Convert.ToInt32(cmb_periodo.SelectedValue);
            oReportes_parametros.Descripcion = txt_descripcion_parametros.Text.Trim();
            oReportes_parametros.Id_tipoReporte = this.Request.Path;

            Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

            if (!System.IO.File.Exists(path))
                oReportes_parametrosToXml.createXml(oReportes_parametros, path);
            else
                oReportes_parametrosToXml.addToXml(oReportes_parametros, path);


            cargarParametrosdeXml();
            mpe_ver.Show();
        }
        protected void RadGrid_parametros_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            RadComboBox cmb_periodo = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_periodo") as RadComboBox;
            RadComboBox cmb_año = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_año") as RadComboBox;
            RadComboBox cmb_mes = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_mes") as RadComboBox;
            RadComboBox cmb_cadena = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_cadena") as RadComboBox;
            RadComboBox cmb_ciudad = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_ciudad") as RadComboBox;
            RadComboBox cmb_cobertura = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_cobertura") as RadComboBox;
            UpdatePanel up_filtros = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("up_filtros") as UpdatePanel;
            ModalPopupExtender mpe_ver = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("mpe_ver") as ModalPopupExtender;

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

                cmb_año.SelectedIndex = cmb_año.Items.FindItemIndexByValue(lbl_id_año.Text);
                cmb_mes.SelectedIndex = cmb_mes.Items.FindItemIndexByValue(lbl_id_mes.Text);
                cargarPeriodo();
                cmb_periodo.SelectedIndex = cmb_periodo.Items.FindItemIndexByValue(lbl_id_periodo.Text);

                cmb_ciudad.SelectedIndex = cmb_ciudad.Items.FindItemIndexByValue(lbl_id_oficina.Text);


                up_filtros.Update();
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
                mpe_ver.Show();
            }
        }
        #endregion
    }
}