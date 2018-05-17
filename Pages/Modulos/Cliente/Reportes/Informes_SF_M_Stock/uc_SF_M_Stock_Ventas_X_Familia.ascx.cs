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


namespace SIGE.Pages.Modulos.Cliente.Reportes.Informes_SF_M_Stock
{
    public partial class uc_SF_M_Stock_Ventas_X_Familia : System.Web.UI.UserControl
    {
        private Conexion oCoon = new Conexion();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();

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
        private string siddia;
        private string sidciudad;
        private string sidcategoria;
        private string sidsub_categoria;
        private string sidmarca;
        private string sidsub_marca;
        private string sidsku;
        private string sidpuntoventa;
        private string sidmercaderista;
        private string sidfamilia;
        private string sidsub_familia;
        private string sidproducto;
        


        protected void Page_Load(object sender, EventArgs e)
        {
            sUser = this.Session["sUser"].ToString();
            sPassw = this.Session["sPassw"].ToString();
            sNameUser = this.Session["nameuser"].ToString();
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();



            if (!IsPostBack)
            {
                try
                {
                    iniciarcombos();
                    _AsignarVariables();
                    llenarreporteInicial();
                    Años();
                    Llena_Meses();
                    llenaoficina();
                    llenacategoria();
                    llenafuerzav();
                    llenasupervisores();

                }
                catch (Exception ex)
                {
                    Exception mensaje = ex;
                    this.Session.Abandon();
                    
                }
            }
        }
        private void iniciarcombos()
        {
            ddl_Year.DataBind();
            ddl_Year.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            ddl_Month.DataBind();
            ddl_Month.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            //ddl_Month.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            ddl_Periodo.DataBind();
            ddl_Periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            ddl_Dia.DataBind();
            //ddl_Dia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            ddl_Oficina.DataBind();
            ddl_Oficina.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            ddl_Corporacion.DataBind();
            ddl_Corporacion.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            ddl_NodoComercial.DataBind();
            ddl_NodoComercial.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            ddl_PuntoDeVenta.DataBind();
            ddl_PuntoDeVenta.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            ddl_Supervisor.DataBind();
            ddl_Supervisor.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            ddl_FuerzaDeVenta.DataBind();
            ddl_FuerzaDeVenta.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            ddl_Categoria.DataBind();
            ddl_Categoria.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            ddl_Marca.DataBind();
            ddl_Marca.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            ddl_Familia.DataBind();
            ddl_Familia.Items.Insert(0, new ListItem("--Todos--", "0"));
            //ddl_Familia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            ddl_Subfamilia.DataBind();
            ddl_Subfamilia.Items.Insert(0, new ListItem("--Todos--", "0"));
            //ddl_Subfamilia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
        }
        private void _AsignarVariables()
        {
            sidaño = ddl_Year.SelectedValue;
            sidmes = ddl_Month.SelectedValue;
            sidperiodo = ddl_Periodo.SelectedValue;
            siddia = ddl_Dia.SelectedValue;


            string sidperdil = this.Session["Perfilid"].ToString();

            if (ddl_Year.SelectedValue == "0" && ddl_Month.SelectedValue == "0" && ddl_Periodo.SelectedValue == "0")
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {

                    icompany = Convert.ToInt32(this.Session["companyid"]);
                    iservicio = Convert.ToInt32(this.Session["Service"]);
                    canal = this.Session["Canal"].ToString().Trim();
                    Report = Convert.ToInt32(this.Session["Reporte"]);
                    int cadena = Convert.ToInt32(ddl_NodoComercial.SelectedItem.Value);
                    string categoria = ddl_Categoria.SelectedItem.Value;

                    Periodo p = new Periodo(Report, cadena, categoria, canal, icompany, iservicio);

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
        {
            Report = Convert.ToInt32(this.Session["Reporte"]);
            DataTable dt = null;
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO", canal, Report, sidaño, sidmes, sidperiodo);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    
                    sidaño = dt.Rows[0]["id_Year"].ToString();
                    sidmes = dt.Rows[0]["id_Month"].ToString();
                    sidperiodo = dt.Rows[0]["periodo"].ToString();
                    bool valid_analist = Convert.ToBoolean(dt.Rows[0]["ReportsPlanning_ValidacionAnalista"]);

                   }
            }
        }
        protected void GetPeriodForClient()
        { 
            DataTable dt = null;

            Report = Convert.ToInt32(this.Session["Reporte"]);
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_MAX_PERIODO_VALIDADO", Report);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    sidaño = dt.Rows[0]["id_Year"].ToString();
                    sidmes = dt.Rows[0]["id_Month"].ToString();
                    sidperiodo = dt.Rows[0]["periodo"].ToString();
                }
            }
        }
        
        private void llenarreporteInicial()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);


            string iidFamilia = "0";
            string familia = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {

                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if(ddl_Familia.Items[i].Selected) {
                        familia = familia + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                iidFamilia = familia.Substring(0, familia.Length - 1);

            }

            string iidSubfamilia = "0";
            string ck_subfamilia = "";
            if (ddl_Subfamilia.SelectedIndex >= 0) {

                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++) {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        ck_subfamilia = ck_subfamilia + ddl_Subfamilia.Items[i].Value + ",";
                    }
                }
                iidSubfamilia = ck_subfamilia.Substring(0, ck_subfamilia.Length - 1);
            }

            string iidProducto = "0";
            string ck_Productos = "";
            if (ddl_Producto.SelectedIndex >= 0) {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++) {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        ck_Productos = ck_Productos + ddl_Producto.Items[i].Value + ",";
                    }
                }
                iidProducto = ck_Productos.Substring(0, ck_Productos.Length - 1);
            }

            string iidPeriodo = "0";
            if (ddl_Periodo.SelectedIndex >= 0) {
                iidPeriodo = ddl_Periodo.SelectedValue;
                }

            string iidDia = "0";
            string ck_Dia = "";
            if (ddl_Dia.SelectedIndex >= 0) {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++) {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        ck_Dia = ck_Dia + ddl_Dia.Items[i].Value + ",";
                    }
                }
                iidDia = ck_Dia.Substring(0, ck_Dia.Length - 1);
            }

            try
            {
                
                Reporte_SF_M_Stock_Ventas_X_Familia.Visible = true;
                Reporte_SF_M_Stock_Ventas_X_Familia.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;

                Reporte_SF_M_Stock_Ventas_X_Familia.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Ventas_X_Familia";
                                                                                                  
                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                Reporte_SF_M_Stock_Ventas_X_Familia.ServerReport.ReportServerUrl = new Uri(strConnection);
                Reporte_SF_M_Stock_Ventas_X_Familia.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", ddl_Corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", ddl_Oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", ddl_NodoComercial.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", ddl_PuntoDeVenta.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", ddl_Categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", ddl_Marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", iidFamilia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", iidSubfamilia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", ddl_Supervisor.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", ddl_FuerzaDeVenta.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iidPeriodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", "0"));

                Reporte_SF_M_Stock_Ventas_X_Familia.ServerReport.SetParameters(parametros);

                
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
            dty = Get_Administrativo.Get_ObtenerYears();
            if (dty.Rows.Count > 0)
            {
                ddl_Year.DataSource = dty;
                ddl_Year.DataValueField = "Years_Number";
                ddl_Year.DataTextField = "Years_Number";
                ddl_Year.DataBind();
                ddl_Year.DataSource = dty;
                ddl_Year.DataValueField = "Years_Number";
                ddl_Year.DataTextField = "Years_Number";
                ddl_Year.DataBind();
                ddl_Year.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
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
                ddl_Month.DataSource = dtm;
                ddl_Month.DataValueField = "codmes";
                ddl_Month.DataTextField = "namemes";
                ddl_Month.DataBind();

                ddl_Month.DataSource = dtm;
                ddl_Month.DataValueField = "codmes";
                ddl_Month.DataTextField = "namemes";
                ddl_Month.DataBind();
                //ddl_Month.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
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
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", canal, icompany, Report, ddl_Month.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                ddl_Periodo.DataSource = dtp;
                ddl_Periodo.DataValueField = "id_periodo";
                ddl_Periodo.DataTextField = "Periodo";
                ddl_Periodo.DataBind();

                //ddl_Periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            }
            else
            {
                dtp = null;
            }
        }
        private void LLenarDiasxPerido()
        {

            DataTable dtDias = oCoon.ejecutarDataTable("UP_WEBXPLORA_OBTENERDIASXPERIODO_SF_MODERNO",icompany,canal,28,ddl_Year.SelectedValue,ddl_Month.SelectedValue,ddl_Periodo.SelectedValue);
            if (dtDias.Rows.Count > 0)
            {
                ddl_Dia.DataSource = dtDias;
                ddl_Dia.DataValueField = "dia";
                ddl_Dia.DataTextField = "dia";
                ddl_Dia.DataBind();
                //ddl_Dia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                ddl_Dia.Enabled = true;
            }
            else {
                dtDias = null;
            }
        }

        private void llenacorporacion()
        {
            try
            {
                DataTable dtcorp = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CORPORACION", ddl_Oficina.SelectedItem.Value);

                if (dtcorp.Rows.Count > 0)
                {
                    ddl_Corporacion.Enabled = true;
                    ddl_Corporacion.DataSource = dtcorp;
                    ddl_Corporacion.DataTextField = "corp_name";
                    ddl_Corporacion.DataValueField = "corp_id";
                    ddl_Corporacion.DataBind();

                    ddl_Corporacion.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenaoficina()
        {
            if (this.Session["companyid"] != null)
            {
                int compañia = Convert.ToInt32(this.Session["companyid"]);
                DataTable dtofi = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENEROFICINAS", compañia);

                if (dtofi.Rows.Count > 0)
                {
                    ddl_Oficina.Enabled = true;
                    ddl_Oficina.DataSource = dtofi;
                    ddl_Oficina.DataTextField = "Name_Oficina";
                    ddl_Oficina.DataValueField = "cod_Oficina";
                    ddl_Oficina.DataBind();

                    ddl_Oficina.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            else
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenacadena()
        {
            //UP_WEBXPLORA_OPE_COMBO_CADENA
            try
            {
                DataTable dtcad = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CADENA", ddl_Corporacion.SelectedItem.Value);

                if (dtcad.Rows.Count > 0)
                {
                    ddl_NodoComercial.Enabled = true;
                    ddl_NodoComercial.DataSource = dtcad;
                    ddl_NodoComercial.DataTextField = "commercialNodeName";
                    ddl_NodoComercial.DataValueField = "NodeCommercial";
                    ddl_NodoComercial.DataBind();

                    ddl_NodoComercial.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenapuntoventa()
        {
            //UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA
            try
            {
                DataTable dtpventa = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA", ddl_NodoComercial.SelectedItem.Value);

                if (dtpventa.Rows.Count > 0)
                {
                    ddl_PuntoDeVenta.Enabled = true;
                    ddl_PuntoDeVenta.DataSource = dtpventa;
                    ddl_PuntoDeVenta.DataTextField = "pdv_Name";
                    ddl_PuntoDeVenta.DataValueField = "id_PointOfsale";
                    ddl_PuntoDeVenta.DataBind();

                    ddl_PuntoDeVenta.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenacategoria()
        {
            //UP_WEBXPLORA_OPE_COMBO_CATEGORIA
            try
            {
                DataTable dtcategoria = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA", icompany, canal);

                if (dtcategoria.Rows.Count > 0)
                {
                    ddl_Categoria.Enabled = true;
                    ddl_Categoria.DataSource = dtcategoria;
                    ddl_Categoria.DataTextField = "Product_Category";
                    ddl_Categoria.DataValueField = "id_ProductCategory";
                    ddl_Categoria.DataBind();

                    ddl_Categoria.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenamarca()
        {
            //UP_WEBXPLORA_OPE_COMBO_CATEGORIA
            try
            {
                DataTable dtmarca = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_BY_CATEGORIA_ID", ddl_Categoria.SelectedItem.Value);

                if (dtmarca.Rows.Count > 0)
                {
                    ddl_Marca.Enabled = true;
                    ddl_Marca.DataSource = dtmarca;
                    ddl_Marca.DataTextField = "Name_Brand";
                    ddl_Marca.DataValueField = "id_Brand";
                    ddl_Marca.DataBind();

                    ddl_Marca.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
                else
                {
                    ddl_Marca.Items.Insert(0, new RadComboBoxItem("---No tiene marcas asociadas---", "0"));
                    //ddl_Familia.Items.Insert(0, new RadComboBoxItem("---No tiene familias asociadas---", "0"));
                    //ddl_Subfamilia.Items.Insert(0, new RadComboBoxItem("---No tiene subfamilias asociadas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenafamilia()
        {
            //UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK
            try
            {
                DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK", ddl_Categoria.SelectedItem.Value);

                if (dtfamilia.Rows.Count > 0)
                {
                    ddl_Familia.Enabled = true;
                    ddl_Familia.DataSource = dtfamilia;
                    ddl_Familia.DataTextField = "name_Family";
                    ddl_Familia.DataValueField = "id_ProductFamily";
                    ddl_Familia.DataBind();

                    //ddl_Familia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
                else
                {
                    //ddl_Familia.Items.Insert(0, new RadComboBoxItem("---No tiene familias asociadas---", "0"));
                    //ddl_Subfamilia.Items.Insert(0, new RadComboBoxItem("---No tiene subfamilias asociadas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenasubfamilia()
        {
            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", sidproductFamily);
            try
            {
                DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", ddl_Familia.SelectedItem.Value);

                if (dtfamilia.Rows.Count > 0)
                {
                    ddl_Subfamilia.Enabled = true;
                    ddl_Subfamilia.DataSource = dtfamilia;
                    ddl_Subfamilia.DataTextField = "subfam_nombre";
                    ddl_Subfamilia.DataValueField = "id_ProductSubFamily";
                    ddl_Subfamilia.DataBind();

                    //ddl_Subfamilia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenasupervisores()
        {
            //UP_WEBXPLORA_OPE_COMBO_SUPERVISOR_CANAL            
            try
            {
                DataTable dtsupervisor = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUPERVISOR_CANAL", icompany, canal);

                if (dtsupervisor.Rows.Count > 0)
                {
                    ddl_Supervisor.Enabled = true;
                    ddl_Supervisor.DataSource = dtsupervisor;
                    ddl_Supervisor.DataTextField = "Person_NameComplet";
                    ddl_Supervisor.DataValueField = "Person_id";
                    ddl_Supervisor.DataBind();

                    ddl_Supervisor.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenafuerzav()
        {
            try
            {
                DataTable dtfuerzav = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FUERZA_VENTA", icompany, canal);

                if (dtfuerzav.Rows.Count > 0)
                {
                    ddl_FuerzaDeVenta.Enabled = true;
                    ddl_FuerzaDeVenta.DataSource = dtfuerzav;
                    ddl_FuerzaDeVenta.DataTextField = "pdv_contact_name";
                    ddl_FuerzaDeVenta.DataValueField = "id_PointOfSale_Contact";
                    ddl_FuerzaDeVenta.DataBind();

                    ddl_FuerzaDeVenta.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        private void ConsultarImpulso_SF_M()
        {
            //icompany = Convert.ToInt32(this.Session["companyid"]);
            //iservicio = Convert.ToInt32(this.Session["Service"]);
            //canal = this.Session["Canal"].ToString().Trim();

            try
            {
                //Impulso SF_Moderno
                Reporte_SF_M_Stock_Ventas_X_Familia.Visible = true;
                Reporte_SF_M_Stock_Ventas_X_Familia.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;

                Reporte_SF_M_Stock_Ventas_X_Familia.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_Impulso_SF_Moderno";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                Reporte_SF_M_Stock_Ventas_X_Familia.ServerReport.ReportServerUrl = new Uri(strConnection);
                Reporte_SF_M_Stock_Ventas_X_Familia.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", ddl_Corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", ddl_Oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodocomercial", ddl_NodoComercial.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", ddl_PuntoDeVenta.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductocategoria", ddl_Categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idmarca", ddl_Marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductfamily", ddl_Familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", ddl_Subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idsupervisor", ddl_Supervisor.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idffvv", ddl_FuerzaDeVenta.SelectedItem.Value));


                Reporte_SF_M_Stock_Ventas_X_Familia.ServerReport.SetParameters(parametros);


            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }


        }

        protected void ddl_Oficina_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenacorporacion();
        }

        protected void ddl_Corporacion_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenacorporacion();
        }

        protected void ddl_NodoComercial_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenapuntoventa();
        }

        protected void ddl_Categoria_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenamarca();
        }

        protected void ddl_Marca_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenafamilia();
        }

        //protected void ddl_Familia_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    llenasubfamilia();
        //}

        protected void ddl_Periodo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LLenarDiasxPerido();
        }

        protected void ddl_Month_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Llenar_Periodos();
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            _AsignarVariables();
            llenarreporteInicial();
        }


        protected void ddl_Familia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Familia.SelectedIndex >= 0)
            {
                /// Obtener id del person y anidarlos en una cadena------------
                /// Ditmar Estrada
                string cadenaIdFamilia = "";

                for (int i = 0; i < ddl_Familia.Items.Count; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        cadenaIdFamilia = cadenaIdFamilia + ddl_Familia.Items[i].Value + ",";
                    }
                }
                sidfamilia = cadenaIdFamilia.Substring(0, cadenaIdFamilia.Length - 1);
            }
            llenasubfamilia_check(sidfamilia);
        }

        private void llenasubfamilia_check(String familias)
        {
            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", sidproductFamily);
            try
            {
                DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK_CHECK", familias);

                if (dtfamilia.Rows.Count > 0)
                {
                    ddl_Subfamilia.Enabled = true;
                    ddl_Subfamilia.DataSource = dtfamilia;
                    ddl_Subfamilia.DataTextField = "subfam_nombre";
                    ddl_Subfamilia.DataValueField = "id_ProductSubFamily";
                    ddl_Subfamilia.DataBind();

                    //ddl_Subfamilia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void ddl_Subfamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Subfamilia.SelectedIndex >= 0) {
                string cadenaSubFamilia = "";
                for (int i = 0; i < ddl_Subfamilia.Items.Count; i++) {
                    if (ddl_Subfamilia.Items[i].Selected) {
                        cadenaSubFamilia = cadenaSubFamilia + ddl_Subfamilia.Items[i].Value + ",";
                    }
                }
                sidsub_familia = cadenaSubFamilia.Substring(0, cadenaSubFamilia.Length - 1);
            }
            llenarproductos_check(sidsub_familia);
        }

        private void llenarproductos_check(String subfamilias) {
            try {
                DataTable dtprodutos = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SKU_PRODUCTO_REPORT_STOCK", sidsub_familia);
                if (dtprodutos.Rows.Count > 0) {
                    ddl_Producto.Enabled = true;
                    ddl_Producto.DataSource = dtprodutos;
                    ddl_Producto.DataTextField = "productoNombre";
                    ddl_Producto.DataValueField = "cod_Product";
                    ddl_Producto.DataBind();
                }
            }
            catch(Exception ex) {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void ddl_Producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Producto.SelectedIndex >= 0) {
                String cadenaProductos = "";
                for (int i = 0; i < ddl_Producto.Items.Count; i++) {
                    if (ddl_Producto.Items[i].Selected) {
                        cadenaProductos = cadenaProductos + ddl_Producto.Items[i].Value + ",";
                    }
                }
                sidproducto = cadenaProductos.Substring(0, cadenaProductos.Length - 1);
            }
        }

        protected void ddl_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Month.SelectedIndex >= 0) {
                string cadenaMonth = "";
                for (int i = 0; i < ddl_Month.Items.Count; i++) {
                    if (ddl_Month.Items[i].Selected) {
                        cadenaMonth = cadenaMonth + ddl_Month.Items[i].Value + ",";
                    }
                }
                sidmes = cadenaMonth.Substring(0, cadenaMonth.Length - 1);
            }

        }

        private void Llenar_Periodos_check(String months)
        {
            DataTable dtp = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", canal, icompany, Report, ddl_Month.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                ddl_Periodo.DataSource = dtp;
                ddl_Periodo.DataValueField = "id_periodo";
                ddl_Periodo.DataTextField = "Periodo";
                ddl_Periodo.DataBind();

                //ddl_Periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            }
            else
            {
                dtp = null;
            }
        }

        protected void ddl_Periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtDias = oCoon.ejecutarDataTable("UP_WEBXPLORA_OBTENERDIASXPERIODO_SF_MODERNO", icompany, canal, 28, ddl_Year.SelectedValue, ddl_Month.SelectedValue,ddl_Periodo.SelectedValue );
            if (dtDias.Rows.Count > 0)
            {
                ddl_Dia.DataSource = dtDias;
                ddl_Dia.DataValueField = "dia";
                ddl_Dia.DataTextField = "dia";
                ddl_Dia.DataBind();
                //ddl_Dia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                ddl_Dia.Enabled = true;
            }
            else
            {
                dtDias = null;
            }

        }

        
    }
}