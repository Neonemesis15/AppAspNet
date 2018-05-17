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
using AjaxControlToolkit;

namespace SIGE.Pages.Modulos.Cliente.Reportes.Informe_de_Presencia.Informe_Minoristas
{
    public partial class Reporte_v2_PresenciaColgate_Menor_ES : System.Web.UI.Page
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
                Ciudad();
                cargarCadena();
                cargarReporte();
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
            cmb_cobertura.Items.Insert(0, new RadComboBoxItem("--Nacional--", "0"));
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

        protected void cmb_cobertura_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox cmb_cobertura = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_cobertura") as RadComboBox;
            RadComboBox cmb_ciudad = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_ciudad") as RadComboBox;
            RadComboBox cmb_cadena = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_cadena") as RadComboBox;
            Label lbl_ciudad = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("lbl_ciudad") as Label;
            Label lbl_cadena = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("lbl_cadena") as Label;

            if (cmb_cobertura.SelectedValue == "2")
            {
                lbl_ciudad.Visible = true;
                cmb_ciudad.Visible = true;
            }
            else
            {
                cmb_ciudad.SelectedIndex = 0;
                lbl_ciudad.Visible = false;
                cmb_ciudad.Visible = false;

                //---
                cmb_cadena.Visible = false;
                lbl_cadena.Visible = false;

                cargarCadena();
            }
        }
        protected void cmb_ciudad_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cargarCadena();
        }        
        protected void cargarCadena()
        {
            RadComboBox cmb_cobertura = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_cobertura") as RadComboBox;
            RadComboBox cmb_cadena = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_cadena") as RadComboBox;
            RadComboBox cmb_ciudad = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_ciudad") as RadComboBox;
            Label lbl_cadena = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("lbl_cadena") as Label;

            cmb_cadena.Visible = false;
            lbl_cadena.Visible = false;

            DataTable dt = null;

            iservicio = Convert.ToInt32(this.Session["Service"]);
            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();

            if (cmb_cobertura.SelectedValue == "1" || cmb_ciudad.SelectedIndex > 0)
            {
                cmb_cadena.Visible = true;
                lbl_cadena.Visible = true;
                if (cmb_cobertura.SelectedValue == "1")
                    cmb_ciudad.SelectedValue = "9";                
            }
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_NODECOMERCIAL_BY_cod_oficina", iservicio, sidcanal, iidcompany, cmb_ciudad.SelectedValue);

            cmb_cadena.DataSource = dt;
            cmb_cadena.DataValueField = "id_NodeCommercial";
            cmb_cadena.DataTextField = "commercialNodeName";
            cmb_cadena.DataBind();

            cmb_cadena.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
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
            cargarReporte();
        }
        #endregion

        #region GenerarReportes
        private void cargarReporte()
        {
            RadComboBox cmb_periodo = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_periodo") as RadComboBox;
            RadComboBox cmb_año = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_año") as RadComboBox;
            RadComboBox cmb_mes = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_mes") as RadComboBox;
            RadComboBox cmb_cadena = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_cadena") as RadComboBox;
            RadComboBox cmb_ciudad = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_ciudad") as RadComboBox;
            RadComboBox cmb_cobertura = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_cobertura") as RadComboBox;
            Informes_validacion.UC_ValidarPeriodos UC_ValidarPeriodos1 = RadPanelBar_menu.FindChildByValue<RadPanelItem>("validar").FindControl("UC_ValidarPeriodos1") as Informes_validacion.UC_ValidarPeriodos;


            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            string sidperdil = this.Session["Perfilid"].ToString();

            string  año, mes, periodo;
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
                RadPanelBar_menu.FindChildByValue<RadPanelItem>("configuracion").Visible = true;
                UC_ValidarPeriodos1.SetValidacion(iservicio, sidcanal, iidcompany, Report, año, mes, periodo);
                validacion = false;
            }
            else
            {
                RadPanelBar_menu.FindChildByValue<RadPanelItem>("itemValidacion").Visible = false;
                RadPanelBar_menu.FindChildByValue<RadPanelItem>("configuracion").Visible = false;
                validacion = true;
            }

            Reporte.Visible = true;

            Reporte.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            Reporte.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
            Reporte.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_PresenciaMinColgateGraficos";

            string strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

            Reporte.ServerReport.ReportServerUrl = new Uri(strConnection);
            Reporte.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
            List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", año));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", mes));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", periodo));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", cmb_ciudad.SelectedValue));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA",cmb_cadena.SelectedValue ));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("COBERTURA", cmb_cobertura.SelectedValue));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", sidcanal));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", iidcompany.ToString()));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", iservicio.ToString()));
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("VALIDANALYST", validacion.ToString()));

            Reporte.ServerReport.SetParameters(parametros);
        }
        #endregion

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
            oReportes_parametros.Id_TipoCiudad = Convert.ToInt32(cmb_cobertura.SelectedValue);
            oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_ciudad.SelectedValue);
            oReportes_parametros.Icadena = (cmb_cadena.SelectedValue == "") ? 0 : Convert.ToInt32(cmb_cadena.SelectedValue);
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
                cmb_cobertura.SelectedIndex = cmb_cobertura.Items.FindItemIndexByValue(lbl_id_tipoCiudad.Text);
                cmb_ciudad.SelectedIndex = cmb_ciudad.Items.FindItemIndexByValue(lbl_id_oficina.Text);
                cmb_cadena.SelectedIndex = cmb_cadena.Items.FindItemIndexByValue(lbl_id_cadena.Text);

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