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
    public partial class Reporte_v2_Stock : System.Web.UI.Page
    {
        private Conexion oCoon = new Conexion();

        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();

        private string sUser;
        private string sPassw;
        private string sNameUser;
        private string semail;
        private string smailadmon;
        private int icompany;
        private string canal;
        private int Report;
        private int iservicio;
        private int iclient;

        private ReportViewer ReportStock_DiasGiroOficina;
        private ReportViewer ReportStock_DiasGiroOCategoria;
        private ReportViewer ReportStock_DiasGiroOMarcaFamilia;
        private ReportViewer ReportStock_DiasGiroOPDV;
        private ReportViewer ReportStock_DiasGiroOEvolucion;
        private ReportViewer Reporte_v2_Stock_RSellin1;
        private ReportViewer ReportStock;
        private ReportViewer RepSOficina;
        private ReportViewer RepRlev;

        private string year = String.Empty;
        private string month = String.Empty;
        private int iperiodo = 0;
        private string sidmarca = String.Empty;
        private string id_ProductCategory = String.Empty;
        private string cod_oficina = String.Empty;
        private string id_family = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                sUser = this.Session["sUser"].ToString();
                sPassw = this.Session["sPassw"].ToString();
                sNameUser = this.Session["nameuser"].ToString();
                icompany = Convert.ToInt32(this.Session["companyid"]);
                semail = this.Session["smail"].ToString().Trim();
                Report = Convert.ToInt32(this.Session["Reporte"]);
                canal = this.Session["Canal"].ToString().Trim();
                iservicio = Convert.ToInt32(this.Session["Service"]);
                string sidperdil = this.Session["Perfilid"].ToString();
                //UpdateProgressContext2();

                this.Session["Ciudad"] = "0";
                this.Session["catego"] = "0";
                this.Session["subcatego"] = "0";
                this.Session["pdv"] = "0";
                this.Session["Año"] = "0";
                this.Session["Mes"] = "0";
                MyAccordion.SelectedIndex = 1;
                TabContainer_filtros.ActiveTabIndex = 0;

                if (!IsPostBack)
                {
                    try
                    {
                        cmb_año.DataBind();
                        cmb_año.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
                        cmb_mes.DataBind();
                        cmb_mes.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
                        cmb_periodo.DataBind();
                        cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
                        
                        cmb_categoria.DataBind();
                        cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                        cmb_subCategoria.DataBind();
                        cmb_subCategoria.Items.Insert(0, new ListItem("--Todos--", "0"));
                        cmb_oficina.DataBind();
                        cmb_oficina.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                        cmb_punto_de_venta.DataBind();
                        cmb_punto_de_venta.Items.Insert(0, new ListItem("--Todos--", "0"));
                        cmb_marca.DataBind();
                        cmb_marca.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                        cmb_subMarca.DataBind();
                        cmb_subMarca.Items.Insert(0, new ListItem("--Todos--", "0"));
                        cmb_skuProducto.DataBind();
                        cmb_skuProducto.Items.Insert(0, new ListItem("--Todos--", "0"));
                        cmb_familia.DataBind();
                        cmb_familia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

                        Años();
                        Llena_Meses();
                        Cobertura();
                        Categorias();
                        SetVariables();

                        cargarInforme_EvolucionDiasGiro(year, cod_oficina, id_ProductCategory, sidmarca, id_family, month, iperiodo.ToString());
                        cargarInforme_DiasGiroOficina(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
                        cargarInforme_RangoSellin(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
                        cargarInforme_RangoStock(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
                        cargarInforme_RangoSellOficina(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
                        //cargarInforme_DiasGiroCategoria(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
                        //cargarInforme_DiasGiroMarcaFamilia(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
                        //cargarInforme_DiasGiroDetalleOficina(year, month, iperiodo.ToString(), "0", id_ProductCategory, sidmarca, id_family);
                        cargarInforme_DiasGiroPDV(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
                        cargarInforme_RelevosOficina(year, month, iperiodo.ToString());
                        cargarParametrosdeXml();
                        if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                        {
                            div_configuration.Visible = true;
                            //Log(year, month, iperiodo.ToString(), "0", "0", "0", "0"); ///Revizar hay un error Ing. Carlos Hernandez 08/09/2011
                            TabPanel_Configuracion.HeaderText = "Configuración";
                            Reporte_v2_Stock_ResumenEjecutivo.Visible = true;
                            Form_Stock_RangoDiasGiro1.Visible = true;
                        }
                        else
                        {
                            //TabPanel_Configuracion.HeaderText = "Resumen Ejecutivo";
                            //Div_ResumenEjecutivo.Visible = false;
                            //Div_ResumenEjecutivo.Style.Value = "display:none;";
                            TabPanel_Configuracion.Enabled = false;
                            TabPanel_Configuracion.Visible = false;
                        }
                    }

                    catch (Exception ex)
                    {
                        Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                        mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
                        this.Session.Abandon();
                        Response.Redirect("~/err_mensaje_seccion.aspx", true);
                    }
                }
            }
            catch
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

     
        protected void SetVariables()
        {
            string sidperdil = this.Session["Perfilid"].ToString();


            //---cargar parametros iniciales 
            sidmarca = cmb_marca.SelectedValue;
            if (sidmarca == "")
                sidmarca = "0";

            year = cmb_año.SelectedValue;
            month = cmb_mes.SelectedValue;

            id_ProductCategory = cmb_categoria.SelectedValue;
            if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0" && id_ProductCategory == "0")
                id_ProductCategory = "12";

            cod_oficina = cmb_oficina.SelectedValue.Trim();
            if (cod_oficina == "")
                cod_oficina = "0";

            id_family = cmb_familia.SelectedValue.Trim();
            if (id_family == "")
                id_family = "0";

            if (iperiodo == 0 && cmb_periodo.SelectedIndex == 0 && year == "0" && month == "0")
            {
                

                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    //aca debe ir la carga inical para el analista
                    iclient = 0;
                    icompany = Convert.ToInt32(this.Session["companyid"]);
                    canal = this.Session["Canal"].ToString().Trim();
                    Report = Convert.ToInt32(this.Session["Reporte"]);
                    Periodo p = new Periodo(Report, cod_oficina, id_ProductCategory, sidmarca, id_family, canal, icompany, iservicio);

                    //p.Set_PeriodoConDataValidada_New(); //revizar este metodo esta mal Ing. Carlos Hernandez
                    p.Set_PeriodoConDataValidada();//Este metodo recupera el periodo actual Ing. Carlos Hernandez
                    year = p.Año;
                    month = p.Mes;
                    iperiodo = Convert.ToInt32(p.PeriodoId);
                    cmb_año.SelectedIndex = cmb_año.Items.FindItemByValue(year).Index;
                    cmb_mes.SelectedIndex = cmb_mes.Items.FindItemByValue(month).Index;
                    rcmb_año.SelectedIndex = rcmb_año.Items.FindItemByValue(year).Index;
                    rcmb_mes.SelectedIndex = rcmb_mes.Items.FindItemByValue(month).Index;
                    rcmb_año2.SelectedIndex = rcmb_año2.Items.FindItemByValue(year).Index;
                    rcmb_mes2.SelectedIndex = rcmb_mes2.Items.FindItemByValue(month).Index;

                    Llenar_Periodos();
                    cmb_periodo.SelectedIndex = cmb_periodo.Items.FindItemByValue(iperiodo.ToString()).Index;
                    rcmb_periodo.SelectedIndex = rcmb_periodo.Items.FindItemByValue(iperiodo.ToString()).Index;
                    rcmb_periodo2.SelectedIndex = rcmb_periodo2.Items.FindItemByValue(iperiodo.ToString()).Index;

                    //rcmb_año2.Items.AddRange(cmb_año.Items);
                    //rcmb_mes2.Items.AddRange(cmb_mes.Items);
                    //rcmb_periodo2.Items.AddRange(cmb_periodo.Items);
                                        
                    GetPeridForAnalist();
                }
                else if (sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteGeneral"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado1"])
                {
                    iclient = 1;
                    GetPeriodForClient();
                    cmb_año.SelectedIndex = cmb_año.Items.FindItemByValue(year).Index;
                    cmb_mes.SelectedIndex = cmb_mes.Items.FindItemByValue(month).Index;
                    Llenar_Periodos();
                    cmb_periodo.SelectedIndex = cmb_periodo.Items.FindItemByValue(iperiodo.ToString()).Index;                    
                }
            }
            else
            {
                    iperiodo = Convert.ToInt32(cmb_periodo.SelectedValue);

                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    cmb_año.SelectedIndex = cmb_año.Items.FindItemByValue(year).Index;
                    cmb_mes.SelectedIndex = cmb_mes.Items.FindItemByValue(month).Index;
                    Llenar_Periodos();
                    cmb_periodo.SelectedIndex = cmb_periodo.Items.FindItemByValue(iperiodo.ToString()).Index;
                    GetPeridForAnalist();
                }
            }

            /////////////////////////////

            
            //cmb_categoria.SelectedIndex = cmb_categoria.Items.FindItemByValue(id_ProductCategory).Index;



            //--end cargar parametros iniciales
        }
        //protected void btn_ocultar_Click(object sender, EventArgs e)
        //{
        //    if (Div_filtros.Visible == true)
        //    {

        //        Div_filtros.Visible = false;
        //        btn_ocultar.Text = "Filtros";
        //    }
        //    else if (Div_filtros.Visible == false)
        //    {
        //        Div_filtros.Visible = true;
        //        btn_ocultar.Text = "Ocultar";
        //    }
        //}

        # region Llenar Datos

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

                cmb_año.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));

                //Reporte_v2_Stock_DiasGiroPorMarcaYFamilia.Años(dty);//llena conbos del user control

                string sidperdil = this.Session["Perfilid"].ToString();
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    Form_Stock_RangoDiasGiro1.Años(dty);

                    rcmb_año.DataSource = dty;
                    rcmb_año.DataValueField = "Years_Number";
                    rcmb_año.DataTextField = "Years_Number";
                    rcmb_año.DataBind();

                    rcmb_año.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));



                    rcmb_año2.DataSource = dty;
                    rcmb_año2.DataValueField = "Years_Number";
                    rcmb_año2.DataTextField = "Years_Number";
                    rcmb_año2.DataBind();

                    rcmb_año2.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));
                }
            }
            else
            {
                cmb_año.DataBind();
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
                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));

                //Reporte_v2_Stock_DiasGiroPorMarcaYFamilia.Llena_Meses(dtm);//llena conbos del user control

                string sidperdil = this.Session["Perfilid"].ToString();
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    Form_Stock_RangoDiasGiro1.Llena_Meses(dtm);

                    rcmb_mes.DataSource = dtm;
                    rcmb_mes.DataValueField = "codmes";
                    rcmb_mes.DataTextField = "namemes";
                    rcmb_mes.DataBind();
                    rcmb_mes.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));


                    rcmb_mes2.DataSource = dtm;
                    rcmb_mes2.DataValueField = "codmes";
                    rcmb_mes2.DataTextField = "namemes";
                    rcmb_mes2.DataBind();
                    rcmb_mes2.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));
                }
            }
            else
            {
                cmb_mes.DataBind();
            }
        }

        private void Llenar_Periodos()
        {
            cmb_periodo.Items.Clear();
            cmb_periodo.Enabled = true;
            DataTable dtp = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", canal, icompany, Report, cmb_mes.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                cmb_periodo.DataSource = dtp;
                cmb_periodo.DataValueField = "id_periodo";
                cmb_periodo.DataTextField = "Periodo";
                cmb_periodo.DataBind();
                cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));

                string sidperdil = this.Session["Perfilid"].ToString();
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    rcmb_periodo.DataSource = dtp;
                    rcmb_periodo.DataValueField = "id_periodo";
                    rcmb_periodo.DataTextField = "Periodo";
                    rcmb_periodo.DataBind();
                    rcmb_periodo.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));


                    rcmb_periodo2.DataSource = dtp;
                    rcmb_periodo2.DataValueField = "id_periodo";
                    rcmb_periodo2.DataTextField = "Periodo";
                    rcmb_periodo2.DataBind();
                    rcmb_periodo2.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));
                }
            }
            else
            {
                cmb_periodo.DataBind();
            }
        }

        private void Cobertura()
        {
            DataTable dtc = null;
            dtc = Get_DataClientes.Get_Obtenerinfocombos(icompany, canal, "0", 1);
            if (dtc.Rows.Count > 0)
            {

                cmb_oficina.DataSource = dtc;
                cmb_oficina.DataValueField = "cod_city";
                cmb_oficina.DataTextField = "name_city";
                cmb_oficina.DataBind();

                cmb_oficina.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));

                //Reporte_v2_Stock_DiasGiroPorMarcaYFamilia.Cobertura(dtc);//llena conbos del user control
            }
            else
            {
                cmb_oficina.DataBind();
            }

        }

        //private void Categorias()
        //{
        //    DataTable dtcatego = null;
        //    dtcatego = Get_DataClientes.Get_Obtenerinfocombos(icompany, "0", "", 2);
        //    if (dtcatego.Rows.Count > 0)
        //    {
        //        cmb_marca.Enabled = true;
        //        cmb_categoria.DataSource = dtcatego;
        //        cmb_categoria.DataValueField = "cod_catego";
        //        cmb_categoria.DataTextField = "Name_Catego";
        //        cmb_categoria.DataBind();

        //        cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));

        //        Reporte_v2_Stock_DiasGiroPorMarcaYFamilia.Categorias(dtcatego);//llena conbos del user control

        //        string sidperdil = this.Session["Perfilid"].ToString();
        //        if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
        //        {
        //            Form_Stock_RangoDiasGiro1.Categorias(dtcatego);
        //        }
        //    }
        //    else
        //    {
        //        cmb_categoria.DataBind();
        //    }

        //}

        private void Categorias()
        {
            DataTable dtcatego = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            dtcatego = Get_DataClientes.Get_Obtenerinfocombos_F(icompany, canal, Report, "0", 2);
            if (dtcatego.Rows.Count > 0)
            {

                cmb_categoria.DataSource = dtcatego;
                cmb_categoria.DataValueField = "cod_catego";
                cmb_categoria.DataTextField = "Name_Catego";
                cmb_categoria.DataBind();
                cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));

                //Reporte_v2_Stock_DiasGiroPorMarcaYFamilia.Categorias(dtcatego);//llena conbos del user control

                string sidperdil = this.Session["Perfilid"].ToString();
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    Form_Stock_RangoDiasGiro1.Categorias(dtcatego);
                }
            }
            else
            {
                cmb_categoria.DataBind();
                dtcatego = null;
            }
        }

        private void Subcategorias()
        {
            DataTable dtscat = null;
            dtscat = Get_DataClientes.Get_Obtenerinfocombos(0, "", cmb_categoria.SelectedValue, 3);
            if (dtscat.Rows.Count > 0)
            {
                cmb_subCategoria.DataSource = dtscat;
                cmb_subCategoria.DataValueField = "cod_subcatego";
                cmb_subCategoria.DataTextField = "Name_Subcatego";
                cmb_subCategoria.DataBind();


                cmb_subCategoria.Items.Insert(0, new ListItem("--Todas--", "0"));

            }
            else
            {
                cmb_subCategoria.DataBind();
                cmb_subCategoria.Enabled = false;
            }

        }

        private void Marcas()
        {
            DataTable dtm = null;
            icompany = Convert.ToInt32(this.Session["companyid"]);
            if (cmb_subCategoria.SelectedValue == "")
            {

                dtm = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIEN_V2_LLENARMARCAS_PRECIOS", cmb_categoria.SelectedValue, 0, icompany);



            }
            else
            {

                dtm = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIEN_V2_LLENARMARCAS_PRECIOS", cmb_categoria.SelectedValue, cmb_subCategoria.SelectedValue, icompany);

            }
            if (dtm.Rows.Count > 0)
            {

                cmb_marca.DataSource = dtm;
                cmb_marca.DataValueField = "id_Brand";
                cmb_marca.DataTextField = "Name_Brand";
                cmb_marca.DataBind();

                cmb_marca.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));

            }
            else
            {

                dtm = null;
                cmb_marca.Enabled = false;
                //cmb_subCategoria.Items.Clear();
            }
        }

        private void Submarcas()
        {
            DataTable dtsm = null;
            dtsm = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTNERSUBMARCAS", Convert.ToInt32(cmb_marca.SelectedValue));
            if (dtsm.Rows.Count > 0)
            {

                cmb_subMarca.DataSource = dtsm;
                cmb_subMarca.DataValueField = "id_SubBrand";
                cmb_subMarca.DataTextField = "Name_SubBrand";
                cmb_subMarca.DataBind();

                cmb_subMarca.Items.Insert(0, new ListItem("--Todas--", "0"));

            }
            else
            {
                dtsm = null;
                cmb_subMarca.Enabled = false;
                cmb_subMarca.Items.Clear();

            }

        }
        private void Puntos_Venta()
        {

            DataTable dtpdv = null;
            dtpdv = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PDV_PRECIOS", Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), cmb_oficina.SelectedValue);
            if (dtpdv.Rows.Count > 0)
            {
                cmb_punto_de_venta.DataSource = dtpdv;
                cmb_punto_de_venta.DataValueField = "pdv_code";
                cmb_punto_de_venta.DataTextField = "pdv_Name";
                cmb_punto_de_venta.DataBind();

                cmb_punto_de_venta.Items.Insert(0, new ListItem("--Todos--", "0"));

            }
            else
            {
                dtpdv = null;
            }
        }
        private void Familias()
        {

            DataTable dtf = null;
            dtf = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_COMBO_FAMILIA_BY_IDCATEGORIA", cmb_categoria.SelectedValue.Trim());

            if (dtf.Rows.Count > 0)
            {
                cmb_familia.Enabled = true;
                cmb_familia.DataSource = dtf;
                cmb_familia.DataValueField = "id_ProductFamily";
                cmb_familia.DataTextField = "name_Family";
                cmb_familia.DataBind();

                cmb_familia.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
            }
            else
            {
                cmb_familia.DataBind();
                cmb_familia.Enabled = false;
            }
        }
      

        # endregion

        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmb_periodo.Items.Clear();
            Llenar_Periodos();
        }
        protected void cmb_oficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Puntos_Venta();

        }
        protected void cmb_categoria_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            cmb_marca.Items.Clear();
            cmb_subMarca.Items.Clear();
            cmb_subCategoria.Items.Clear();
            cmb_subCategoria.Enabled = true;
            Subcategorias();
            cmb_marca.Enabled = true;
            Marcas();
            Familias();
            //Productos();
        }

        protected void cmb_marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_subMarca.Enabled = true;
            //Submarcas();
            //Productos();

            //cargarInforme_DiasGiroOficina();
            //cargarInforme_DiasGiroCategoria();
            //cargarInforme_DiasGiroMarcaFamilia();
            //cargarInforme_DiasGiroPDV();
        }

        protected void cmb_subMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Productos();
        }


        protected void cargarInforme_RangoSellin(string year, string month, string speriodo, string cod_oficina, string id_ProductCategory, string sidmarca, string id_familia)
        {


            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);
           

            try
            {

                Reporte_v2_Stock_RSellin1 = (ReportViewer)Reporte_v2_Stock_RSellin.FindControl("ReportstockRSellin");
                Reporte_v2_Stock_RSellin1.Visible = true;
                Reporte_v2_Stock_RSellin1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                Reporte_v2_Stock_RSellin1.ServerReport.ReportPath = "/Reporte_Precios_V1/Cliente_DRango";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                Reporte_v2_Stock_RSellin1.ServerReport.ReportServerUrl = new Uri(strConnection);
                Reporte_v2_Stock_RSellin1.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Company_id", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Strategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Planning_CodChannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Oficina", cod_oficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductCategory", id_ProductCategory));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_Brand", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductFamily", id_familia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", year));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", month));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", speriodo));

                Reporte_v2_Stock_RSellin1.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }

        protected void cargarInforme_RangoStock(string year, string month, string speriodo, string cod_oficina, string id_ProductCategory, string sidmarca, string id_familia)
        {


            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);
           

            try
            {

                ReportStock = (ReportViewer)Reporte_v2_Stock_RStock.FindControl("RStockn");
                ReportStock.Visible = true;
                ReportStock.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                ReportStock.ServerReport.ReportPath = "/Reporte_Precios_V1/CLieStocRango";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                ReportStock.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReportStock.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Company_id", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Strategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Planning_CodChannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Oficina", cod_oficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductCategory", id_ProductCategory));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_Brand", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductFamily", id_familia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", year));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", month));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", speriodo));

                ReportStock.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }

        protected void cargarInforme_RangoSellOficina(string year, string month, string speriodo, string cod_oficina, string id_ProductCategory, string sidmarca, string id_familia)
        {


            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);
      

            try
            {

                RepSOficina = (ReportViewer)Reporte_v2_Stock_RSOficina.FindControl("RSOficina");
                RepSOficina.Visible = true;
                RepSOficina.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                RepSOficina.ServerReport.ReportPath = "/Reporte_Precios_V1/RangoOfPDV";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                RepSOficina.ServerReport.ReportServerUrl = new Uri(strConnection);
                RepSOficina.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Company_id", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Strategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Planning_CodChannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Oficina", cod_oficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductCategory", id_ProductCategory));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_Brand", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductFamily", id_familia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", year));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", month));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", speriodo));

                RepSOficina.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }

        protected void cargarInforme_RelevosOficina(string year, string month, string speriodo)
        {


            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);
       

            try
            {

                RepRlev = (ReportViewer)Reporte_v2_Stock_RptPdvRelevados.FindControl("RReleva");
                RepRlev.Visible = true;
                RepRlev.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                RepRlev.ServerReport.ReportPath = "/Reporte_Precios_V1/RptPdvRelevados";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                RepRlev.ServerReport.ReportServerUrl = new Uri(strConnection);
                RepRlev.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Company", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Service", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Channel", canal));
             
       
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Año", year));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Mes", month));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", speriodo));

                RepRlev.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }



        protected void cargarInforme_DiasGiroOficina(string year, string month, string speriodo, string cod_oficina, string id_ProductCategory, string sidmarca, string id_familia)
        {


            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                ReportStock_DiasGiroOficina = (ReportViewer)Reporte_v2_Stock_TotalDiasGiroOficina.FindControl("ReportstockdiagiroOfic");
                ReportStock_DiasGiroOficina.Visible = true;
                ReportStock_DiasGiroOficina.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                ReportStock_DiasGiroOficina.ServerReport.ReportPath = "/Reporte_Precios_V1/ReporteStock_TotalDiasGiroOficina";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                ReportStock_DiasGiroOficina.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReportStock_DiasGiroOficina.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Company_id", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Strategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Planning_CodChannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Oficina", cod_oficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductCategory", id_ProductCategory));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_Brand", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductFamily", id_familia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", year));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", month));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", speriodo));

                ReportStock_DiasGiroOficina.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }

        protected void cargarInforme_DiasGiroCategoria(string year, string month, string speriodo, string cod_oficina, string id_ProductCategory, string sidmarca, string id_familia)
        {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);



            //if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0" && cmb_categoria.SelectedValue == "0")
            id_ProductCategory = "0";
            sidmarca = "0";
            try
            {

               // ReportStock_DiasGiroOCategoria = (ReportViewer)Reporte_v2_Stock_TotalDiasGiroCategoria.FindControl("ReportstockdiagiroCatego");
                ReportStock_DiasGiroOCategoria.Visible = true;
                ReportStock_DiasGiroOCategoria.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                ReportStock_DiasGiroOCategoria.ServerReport.ReportPath = "/Reporte_Precios_V1/ReporteStock_TotalDiasGiroCategoria";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                ReportStock_DiasGiroOCategoria.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReportStock_DiasGiroOCategoria.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Company_id", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Strategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Planning_CodChannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Oficina", cod_oficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductCategory", id_ProductCategory));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_Brand", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductFamily", id_familia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", year));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", month));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", speriodo));

                ReportStock_DiasGiroOCategoria.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }

        protected void cargarInforme_DiasGiroMarcaFamilia(string year, string month, string speriodo, string cod_oficina, string id_ProductCategory, string sidmarca, string id_familia)
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                //ReportStock_DiasGiroOMarcaFamilia = (ReportViewer)Reporte_v2_Stock_DiasGiroPorMarcaYFamilia.FindControl("ReportstockdiagiroMarcaFamilia");
                ReportStock_DiasGiroOMarcaFamilia.Visible = true;
                ReportStock_DiasGiroOMarcaFamilia.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                ReportStock_DiasGiroOMarcaFamilia.ServerReport.ReportPath = "/Reporte_Precios_V1/ReporteStock_TotalDiasGiroPorMarcaYFamilia";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                ReportStock_DiasGiroOMarcaFamilia.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReportStock_DiasGiroOMarcaFamilia.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Company_id", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Strategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Planning_CodChannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Oficina", cod_oficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductCategory", id_ProductCategory));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_Brand", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductFamily", id_familia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", year));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", month));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", speriodo));

                ReportStock_DiasGiroOMarcaFamilia.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }
        protected void cargarInforme_DiasGiroDetalleOficina(string year, string month, string speriodo, string cod_oficina, string id_ProductCategory, string sidmarca, string id_familia)
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

               // ReportStock_DiasGiroOMarcaFamilia = (ReportViewer)Reporte_v2_Stock_DetalleOficina1.FindControl("ReportstockDetalleOfic");
                ReportStock_DiasGiroOMarcaFamilia.Visible = true;
                ReportStock_DiasGiroOMarcaFamilia.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                ReportStock_DiasGiroOMarcaFamilia.ServerReport.ReportPath = "/Reporte_Precios_V1/ReporteStock_DG_MarcaDetalleOficina";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                ReportStock_DiasGiroOMarcaFamilia.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReportStock_DiasGiroOMarcaFamilia.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Company_id", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Strategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Planning_CodChannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Oficina", cod_oficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductCategory", id_ProductCategory));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_Brand", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductFamily", id_familia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", year));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", month));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", speriodo));

                ReportStock_DiasGiroOMarcaFamilia.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }
        protected void cargarInforme_DiasGiroPDV(string year, string month, string speriodo, string cod_oficina, string id_ProductCategory, string sidmarca, string id_familia)
        {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                ReportStock_DiasGiroOPDV = (ReportViewer)Reporte_v2_Stock_DiasGiroPorPDV.FindControl("ReportstockdiagiroPDV");
                ReportStock_DiasGiroOPDV.Visible = true;
                ReportStock_DiasGiroOPDV.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                ReportStock_DiasGiroOPDV.ServerReport.ReportPath = "/Reporte_Precios_V1/ReporteStock_TotalDiasGiroYsellOut_porPDV";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                ReportStock_DiasGiroOPDV.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReportStock_DiasGiroOPDV.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Company_id", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Strategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Planning_CodChannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Oficina", cod_oficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductCategory", id_ProductCategory));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_Brand", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductFamily", id_familia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", year));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", month));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", speriodo));

                ReportStock_DiasGiroOPDV.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }
        //agregado month y periodo - Angel Ortiz
        protected void cargarInforme_EvolucionDiasGiro(string year, string cod_oficina, string id_ProductCategory, string sidmarca, string id_familia, string month, string speriodo)
        {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                ReportStock_DiasGiroOEvolucion = (ReportViewer)Reporte_V2_Stock_EvolucionTotalDiasGiroPorPeriodo.FindControl("ReportViewer_evolucion_DG");
                ReportStock_DiasGiroOEvolucion.Visible = true;
                ReportStock_DiasGiroOEvolucion.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                ReportStock_DiasGiroOEvolucion.ServerReport.ReportPath = "/Reporte_Precios_V1/ReporteStock_EvolucionTotalDiasGiroPorPeriodo";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                ReportStock_DiasGiroOEvolucion.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReportStock_DiasGiroOEvolucion.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Company_id", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Strategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Planning_CodChannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Oficina", cod_oficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductCategory", id_ProductCategory));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_Brand", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductFamily", id_familia));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", year));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", month));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", speriodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("client", iclient.ToString()));

                ReportStock_DiasGiroOEvolucion.ServerReport.SetParameters(parametros);

                
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }

        protected void btngnerar_Click(object sender, EventArgs e)
        {
            string sidperdil = this.Session["Perfilid"].ToString();
            TabContainer_filtros.ActiveTabIndex = 0;
            SetVariables();
            //Div_filtros.Visible = false;
            //btn_ocultar.Text = "Filtros";
            cargarInforme_EvolucionDiasGiro(year, cod_oficina, id_ProductCategory, sidmarca, id_family, month, iperiodo.ToString());
            cargarInforme_DiasGiroOficina(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
            //cargarInforme_DiasGiroCategoria(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
            //cargarInforme_DiasGiroMarcaFamilia(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
            //cargarInforme_DiasGiroDetalleOficina(year, month, iperiodo.ToString(), "0", id_ProductCategory, sidmarca, id_family);
            cargarInforme_DiasGiroPDV(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
         
            cargarInforme_RangoSellin(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
            cargarInforme_RangoStock(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
            cargarInforme_RangoSellOficina(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
            cargarInforme_RelevosOficina(year, month, iperiodo.ToString());
            //if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
            //    Log(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
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
            oReportes_parametros.Id_punto_venta = cmb_punto_de_venta.SelectedValue;
            oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
            string sidmarca = cmb_marca.SelectedValue;
            if (sidmarca == "")
                sidmarca = "0";
            oReportes_parametros.Id_producto_marca = sidmarca;
            oReportes_parametros.Id_producto_familia = cmb_familia.SelectedValue;
            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            oReportes_parametros.Id_periodo = Convert.ToInt32(cmb_periodo.SelectedValue);

            //string descripcion = cmb_año.SelectedItem  + "-" + cmb_periodo.SelectedItem + "-" +
            //   cmb_oficina.SelectedItem + "-" + cmb_punto_de_venta.SelectedItem + "-" + cmb_categoria.SelectedItem + "-" +
            // cmb_marca.SelectedItem + "-" + cmb_familia.SelectedItem;

            oReportes_parametros.Descripcion = txt_descripcion_parametros.Text.Trim();

            Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

            if (!System.IO.File.Exists(path))
                oReportes_parametrosToXml.createXml(oReportes_parametros, path);
            else
                oReportes_parametrosToXml.addToXml(oReportes_parametros, path);


            cargarParametrosdeXml();
            txt_descripcion_parametros.Text = "";
            TabContainer_filtros.ActiveTabIndex = 1;
            UpReportStock.Update();
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
        }

        protected void RadGrid_parametros_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.CommandName == "BUSCAR")
            {
                string sidperdil = this.Session["Perfilid"].ToString();

                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_periodo = (Label)e.Item.FindControl("lbl_id_periodo");
                Label lbl_id_oficina = (Label)e.Item.FindControl("lbl_id_oficina");
                Label lbl_id_categoria = (Label)e.Item.FindControl("lbl_id_categoria");
                Label lbl_id_marca = (Label)e.Item.FindControl("lbl_id_marca");
                Label lbl_id_familia = (Label)e.Item.FindControl("lbl_id_familia");

                year = lbl_id_año.Text.Trim();
                month = lbl_id_mes.Text.Trim();
                iperiodo = Convert.ToInt32(lbl_id_periodo.Text.Trim());
                cod_oficina = lbl_id_oficina.Text.Trim();
                id_ProductCategory = lbl_id_categoria.Text.Trim();
                sidmarca = lbl_id_marca.Text.Trim();
                id_family = lbl_id_familia.Text.Trim();

                cmb_año.SelectedIndex = cmb_año.Items.FindItemByValue(year).Index;
                cmb_mes.SelectedIndex = cmb_mes.Items.FindItemByValue(month).Index;
                Llenar_Periodos();
                cmb_periodo.SelectedIndex = cmb_periodo.FindItemByValue(iperiodo.ToString()).Index;
                cmb_oficina.SelectedIndex = cmb_oficina.Items.FindItemByValue(cod_oficina).Index;
                cmb_categoria.SelectedIndex = cmb_categoria.Items.FindItemByValue(id_ProductCategory).Index;
                cmb_marca.SelectedIndex = cmb_marca.Items.FindItemByValue(sidmarca).Index;
                cmb_familia.SelectedIndex = cmb_familia.Items.FindItemByValue(id_family).Index;

                TabContainer_filtros.ActiveTabIndex = 0;

                //__________cargarInforme_DiasGiroOficina(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
                //cargarInforme_DiasGiroCategoria(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
                //cargarInforme_DiasGiroMarcaFamilia(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
                //__________cargarInforme_DiasGiroPDV(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
                //__________cargarInforme_EvolucionDiasGiro(year, cod_oficina, id_ProductCategory, sidmarca, id_family, month, iperiodo.ToString());
                //if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                //    Log(year, month, iperiodo.ToString(), cod_oficina, id_ProductCategory, sidmarca, id_family);
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
                UpReportStock.Update();
            }
            if (e.CommandName == "EDITAR")
            {
                Label lbl_id = (Label)e.Item.FindControl("lbl_id");
                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_periodo = (Label)e.Item.FindControl("lbl_id_periodo");
                Label lbl_id_oficina = (Label)e.Item.FindControl("lbl_id_oficina");
                Label lbl_id_categoria = (Label)e.Item.FindControl("lbl_id_categoria");
                Label lbl_id_marca = (Label)e.Item.FindControl("lbl_id_marca");
                Label lbl_id_familia = (Label)e.Item.FindControl("lbl_id_familia");

                Session["idxml"] = lbl_id.Text.Trim();
                cmb_año.SelectedIndex = cmb_año.Items.FindItemByValue(lbl_id_año.Text).Index;
                cmb_mes.SelectedIndex = cmb_mes.Items.FindItemByValue(lbl_id_mes.Text).Index;
                Llenar_Periodos();
                cmb_periodo.SelectedIndex = cmb_periodo.FindItemByValue(lbl_id_periodo.Text).Index;
                cmb_oficina.SelectedIndex = cmb_oficina.Items.FindItemByValue(lbl_id_oficina.Text).Index;
                cmb_categoria.SelectedIndex = cmb_categoria.Items.FindItemByValue(lbl_id_categoria.Text).Index;
                cmb_marca.SelectedIndex = cmb_marca.Items.FindItemByValue(lbl_id_marca.Text).Index;
                cmb_familia.SelectedIndex = cmb_familia.Items.FindItemByValue(lbl_id_familia.Text).Index;

                TabContainer_filtros.ActiveTabIndex = 0;
                lbl_updateconsulta.Visible = true;
                btn_img_actualizar.Visible = true;

                lbl_saveconsulta.Visible = false;
                btn_img_add.Visible = false;

                UpReportStock.Update();
            }
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

            oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_oficina.SelectedValue);
            oReportes_parametros.Id_punto_venta = cmb_punto_de_venta.SelectedValue;
            oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
            string sidmarca = cmb_marca.SelectedValue;
            if (sidmarca == "")
                sidmarca = "0";
            oReportes_parametros.Id_producto_marca = sidmarca;
            oReportes_parametros.Id_producto_familia = cmb_familia.SelectedValue;
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
            UpReportStock.Update();
        }
        protected void Log(string year, string month, string speriodo, string cod_oficina, string id_ProductCategory, string sidmarca, string id_familia)
        {
            lbl_msj_popup.Text = "";
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            semail = this.Session["smail"].ToString().Trim();


            DataTable dt_rptstock = null;
            dt_rptstock = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_STOCK_REPORTE_Log", icompany, iservicio, canal, cod_oficina, id_ProductCategory, sidmarca, id_familia, year, month, speriodo);

            DataTable dtDGbyCatego = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_GET_ProductCategoryXCompany_BYPeriodo", icompany, year, month, speriodo);
            DataTable dt_Log = new DataTable();

            dt_Log.Columns.Add("Periodo");
            dt_Log.Columns.Add("Cod_Oficina");
            dt_Log.Columns.Add("Oficina");
            dt_Log.Columns.Add("Cod_PDV");
            dt_Log.Columns.Add("PDV");
            dt_Log.Columns.Add("Cod_Categoria");
            dt_Log.Columns.Add("Categoria");
            //dt_Log.Columns.Add("Cod_Marca");
            //dt_Log.Columns.Add("Marca");
            //dt_Log.Columns.Add("Cod_Familia");
            //dt_Log.Columns.Add("Familia");
            //dt_Log.Columns.Add("Descripcion");
            dt_Log.Columns.Add("Peso");
            dt_Log.Columns.Add("Fecha_Incial");
            dt_Log.Columns.Add("Stock_Inicial");
            dt_Log.Columns.Add("Fecha_Final");
            dt_Log.Columns.Add("Stock_Final");
            dt_Log.Columns.Add("Sell_In");
            dt_Log.Columns.Add("Rango_Dias");
            dt_Log.Columns.Add("Sell_Out");
            dt_Log.Columns.Add("Sell_Out_Toneladas");
            dt_Log.Columns.Add("Dias_Giro");
            dt_Log.Columns.Add("MIN_DG");
            dt_Log.Columns.Add("MAX_DG");
            dt_Log.Columns.Add("Tipo_Error");

            int contador = 0;
            for (int i = 0; i < dt_rptstock.Rows.Count; i++)
            {
                DataRow dr = dt_Log.NewRow();

                int cod_categoria = 0;
                double DiasGiro = 0;
                try
                {
                    dr["Periodo"] = year + "-" + month + "-" + speriodo;
                    dr["Cod_Oficina"] = dt_rptstock.Rows[i]["cod_Oficina"].ToString();
                    dr["Oficina"] = dt_rptstock.Rows[i]["Name_Oficina"].ToString();
                    dr["Cod_PDV"] = dt_rptstock.Rows[i]["ClientPDV_code"].ToString();
                    dr["PDV"] = dt_rptstock.Rows[i]["pdv_Name"].ToString();
                    cod_categoria = Convert.ToInt32(dt_rptstock.Rows[i]["id_ProductCategory"].ToString());
                    dr["Cod_Categoria"] = cod_categoria;
                    dr["Categoria"] = dt_rptstock.Rows[i]["Product_Category"].ToString();
                    //dr["Cod_Marca"] = dt_rptstock.Rows[i]["id_Brand"].ToString();
                    //dr["Marca"] = dt_rptstock.Rows[i]["Name_Brand"].ToString();
                    //dr["Cod_Familia"] = dt_rptstock.Rows[i]["id_ProductFamily"].ToString();
                    //dr["Familia"] = dt_rptstock.Rows[i]["family_Descripcion"].ToString();
                    //dr["Descripcion"] = dt_rptstock.Rows[i]["name_Family"].ToString();
                    dr["Fecha_Incial"] = dt_rptstock.Rows[i]["Fecha_Incial"].ToString();
                    dr["Fecha_Final"] = dt_rptstock.Rows[i]["Fecha_Final"].ToString();
                    double peso = Convert.ToDouble(dt_rptstock.Rows[i]["family_Peso"]);
                    dr["Peso"] = peso;
                    int stock_final = Convert.ToInt32(dt_rptstock.Rows[i]["Stock_Final"]);
                    dr["Stock_Final"] = stock_final;
                    int stock_inicial = Convert.ToInt32(dt_rptstock.Rows[i]["Stock_Inicial"]);
                    dr["Stock_Inicial"] = stock_inicial;
                    int sell_in = Convert.ToInt32(dt_rptstock.Rows[i]["Sell_in"].ToString());
                    dr["Sell_In"] = sell_in;
                    Double rango_dias = Convert.ToInt32(dt_rptstock.Rows[i]["Rango_Dias"].ToString());
                    dr["Rango_Dias"] = rango_dias;
                    Double sell_out = stock_inicial + sell_in - stock_final;
                    dr["Sell_Out"] = sell_out;
                    dr["Sell_Out_Toneladas"] = Math.Round((sell_out * peso) / 1000, 2);
                    DiasGiro = Math.Round((stock_final / (sell_out / rango_dias)), 0);
                    dr["Dias_Giro"] = DiasGiro;

                }
                catch (DivideByZeroException)
                {
                    dr["Dias_Giro"] = 0;
                }
                dr["Tipo_Error"] = "DG Fuera de Rango";

                int MIN_DG = 0, MAX_DG = 0;
                for (int j = 0; i < dtDGbyCatego.Rows.Count; j++)
                {
                    if (cod_categoria == Convert.ToInt32(dtDGbyCatego.Rows[j]["id_ProductCategory"].ToString().Trim()))
                    {
                        MIN_DG = Convert.ToInt32(dtDGbyCatego.Rows[j]["DiasGiro_Min_xplora"].ToString().Trim());
                        MAX_DG = Convert.ToInt32(dtDGbyCatego.Rows[j]["DiasGiro_Max_xplora"].ToString().Trim());

                        if (!(DiasGiro >= MIN_DG && DiasGiro <= MAX_DG))
                        {
                            dr["MIN_DG"] = MIN_DG;
                            dr["MAX_DG"] = MAX_DG;
                            dt_Log.Rows.Add(dr);
                            contador += 1;
                        }

                        break;
                    }
                }

            }
            gv_LogErrores.DataSource = dt_Log;
            gv_LogErrores.DataBind();

            if (contador > 0)
            {
                string fn = "Log_Stock_" + speriodo + "-" + month + "-" + year + ".xls";
                string path = Server.MapPath("~/Pages/Modulos/Cliente/Reportes/" + fn);
                if (ExportarExcelDataTable(dt_Log, path))
                {
                    //Label lbl_mensaje = (Label)Panel_popupmensaje.TemplateControl.FindControl("lbl_msj_popup");
                    lbl_msj_popup.Text = "";
                    string mensaje = "Sr Usuario: <br/> Se han presentado registros con errores en la información procesada, dichos errores se enviaron a su correo electronico.";
                    lbl_msj_popup.Text = mensaje;
                    ModalPopupExtender_mensaje.Show();
                    smailadmon = "AdministradorXplora@lucky.com.pe";

                    //Se envia el correo con el Log generado Ing. Carlos Alberto Hernández Rincón 30/01/2011

                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                    System.Net.Mail.Attachment file = new System.Net.Mail.Attachment(path);//Adjunta el Log Generado
                    correo.From = new System.Net.Mail.MailAddress(smailadmon);

                    correo.To.Add(semail);
                    correo.Subject = "Log Stock" + " " + month + " " + year + " " + "Periodo:" + " " + speriodo;
                    correo.Attachments.Add(file);
                    correo.IsBodyHtml = false;
                    correo.Priority = System.Net.Mail.MailPriority.Low;
                    string[] txtbody = new string[] { "Sr Usuario: Se han presentado " + contador + " registros con errores de un total de " + dt_rptstock.Rows.Count + " de la información procesada" };

                    correo.Body = string.Concat(txtbody);

                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                    cliente.Host = "mail.lucky.com.pe";

                    try
                    {
                        cliente.Send(correo);
                        contador = 0;
                        dt_rptstock = null;
                    }
                    catch (Exception ex)
                    {
                        Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                        mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
                    }
                }
            }
        }
        public Boolean ExportarExcelDataTable(DataTable dt, string RutaExcel)
        {
            try
            {
                const string FIELDSEPARATOR = "\t";
                const string ROWSEPARATOR = "\n";
                System.Text.StringBuilder output = new System.Text.StringBuilder();
                // Escribir encabezados    
                foreach (DataColumn dc in dt.Columns)
                {
                    output.Append(dc.ColumnName);
                    output.Append(FIELDSEPARATOR);
                }
                output.Append(ROWSEPARATOR);
                foreach (DataRow item in dt.Rows)
                {
                    foreach (object value in item.ItemArray)
                    {
                        output.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' ').Replace('.', ','));
                        output.Append(FIELDSEPARATOR);
                    }
                    // Escribir una línea de registro        
                    output.Append(ROWSEPARATOR);
                }
                // Valor de retorno    
                // output.ToString();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(RutaExcel);
                sw.Write(output.ToString());
                sw.Close();
                return true;
            }
            catch (Exception ex)
            {
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
                return false;
            }
        }

        #region Logica de la Validación del Reporte
        protected void GetPeriodForClient()
        {
            DataTable dt = null;

            Report = Convert.ToInt32(this.Session["Reporte"]);
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_MAX_PERIODO_VALIDADO_FINAL", Report,iservicio,canal,icompany);

            //div_Validar.Visible = true;
            //lbl_año_value.Text = year;
            //lbl_mes_value.Text = month;
            //lbl_periodo_value.Text = iperiodo.ToString();
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    year = dt.Rows[0]["id_Year"].ToString();
                    month = dt.Rows[0]["id_Month"].ToString();
                    iperiodo = Convert.ToInt32(dt.Rows[0]["periodo"]);
                }

            }
        }
        protected void GetPeridForAnalist()
        {

            Report = Convert.ToInt32(this.Session["Reporte"]);
            DataTable dt = null;
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO", canal, Report, year, month, iperiodo);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    div_Validar.Visible = true;
                    year = dt.Rows[0]["id_Year"].ToString();
                    month = dt.Rows[0]["id_Month"].ToString();
                    iperiodo = Convert.ToInt32(dt.Rows[0]["periodo"]);
                    bool valid_analist = Convert.ToBoolean(dt.Rows[0]["ReportsPlanning_ValidacionAnalista"]);

                    lbl_año_value.Text = year;
                    lbl_mes_value.Text = month;
                    lbl_periodo_value.Text = iperiodo.ToString();

                    chkb_validar.Checked = false;//inicializamos los checkbox en false
                    chkb_invalidar.Checked = false;
                    if (valid_analist)
                        chkb_validar.Checked = valid_analist;
                    else
                        chkb_invalidar.Checked = true;

                    lbl_validacion.Text = year + "-" + dt.Rows[0]["Month_name"].ToString() + " " + iperiodo;

                }
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
        protected void rcmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcmb_periodo.Items.Clear();
            rcmb_periodo.Enabled = true;
            DataTable dtp = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", canal, icompany, Report, rcmb_mes.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                rcmb_periodo.DataSource = dtp;
                rcmb_periodo.DataValueField = "id_periodo";
                rcmb_periodo.DataTextField = "Periodo";
                rcmb_periodo.DataBind();

                rcmb_periodo.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));
            }
            else
            {
                rcmb_periodo.DataBind();
            }
        }

        protected void btn_exportRangos_Click(object sender, EventArgs e)
        {
            try
            {
                icompany = Convert.ToInt32(this.Session["companyid"]);
                iservicio = Convert.ToInt32(this.Session["Service"]);
                canal = this.Session["Canal"].ToString().Trim();

                DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_STOCK_REPORTE_temporal", icompany, iservicio, canal, "0", "0", "0", "0", rcmb_año.SelectedValue, rcmb_mes.SelectedValue, rcmb_periodo.SelectedValue);

                string file_name = "RangoStock" + rcmb_periodo.SelectedValue + "-" + rcmb_mes.SelectedValue + "-" + rcmb_año.SelectedValue;

                Session["DataTableRangosStock"] = dt;
                Response.Redirect("ExportPage.aspx?file_name=" + file_name);

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        protected void gv_LogErrores_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                icompany = Convert.ToInt32(this.Session["companyid"]);
                iservicio = Convert.ToInt32(this.Session["Service"]);
                canal = this.Session["Canal"].ToString().Trim();

                GridItem item = gv_LogErrores.Items[e.Item.ItemIndex];

                if (e.CommandName == "CALCULAR")
                {
                    RadNumericTextBox txt_rangoDias = item.FindControl("txt_RangoDias") as RadNumericTextBox;
                    RadNumericTextBox txt_rsellIn = item.FindControl("txt_sellIn") as RadNumericTextBox;
                    RadNumericTextBox txt_stockIni = item.FindControl("txt_stockIni") as RadNumericTextBox;
                    RadNumericTextBox txt_stockFin = item.FindControl("txt_stockFin") as RadNumericTextBox;

                    double rangoDias = Convert.ToDouble(txt_rangoDias.DbValue);
                    double sellIn = Convert.ToDouble(txt_rsellIn.DbValue);
                    double stockIni = Convert.ToDouble(txt_stockIni.DbValue);
                    double stockFin = Convert.ToDouble(txt_stockFin.DbValue);
                    double sellOut = sellIn + stockIni - stockFin;

                    double DiasGiro = stockFin / (sellOut / rangoDias);

                    RadNumericTextBox txt_DG = item.FindControl("txt_DG") as RadNumericTextBox;

                    txt_DG.DbValue = DiasGiro;

                }
            }
            catch (Exception ex)
            {

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
              
            }
        }

        protected void btn_generarErrores_Click(object sender, EventArgs e)
        {
            try
            {
                Log(cmb_año.SelectedValue, cmb_mes.SelectedValue, cmb_periodo.SelectedValue, "0", "0", "0", "0");
            }
            catch (Exception ex)
            {
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
                lbl_msjRadView.Text = "Seleccione año, mes y periodo en los filtros.";
                ex.Message.ToString();
            }
        }

        protected void btnImg_save_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                for (int i = 0; i < gv_LogErrores.Items.Count; i++)
                {
                    GridItem item = gv_LogErrores.Items[i];

                    Label lbl_cod_PDV, lbl_cod_Categoria, lbl_cod_Familia;

                    lbl_cod_PDV = item.FindControl("lbl_cod_PDV") as Label;
                    lbl_cod_Categoria = item.FindControl("lbl_cod_Categoria") as Label;
                    lbl_cod_Familia = item.FindControl("lbl_cod_Familia") as Label;
                    RadDateTimePicker rad_fecha;
                    rad_fecha = item.FindControl("rad_fecha") as RadDateTimePicker;
                    RadNumericTextBox txt_stockFin;
                    txt_stockFin = item.FindControl("txt_stockFin") as RadNumericTextBox;


                    oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_STOCK_REPORTE_ACTUALIZAR_CANTIDAD", icompany, iservicio, canal, lbl_cod_PDV.Text.Trim(), lbl_cod_Categoria.Text.Trim(),
                        lbl_cod_Familia.Text.Trim(), rad_fecha.DbSelectedDate, txt_stockFin.DbValue, Session["user"].ToString(), DateTime.Now);


                }
                Log(cmb_año.SelectedValue, cmb_mes.SelectedValue, cmb_periodo.SelectedValue, "0", "0", "0", "0");
            }
            catch (Exception ex)
            {
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
                ex.Message.ToString();
            }
        }

        protected void gv_LogErrores_ItemDataBound(object sender, GridItemEventArgs e)
        {
             lbl_msjRadView.Text="";
             if (gv_LogErrores.Items.Count <= 0)
             {
                 lbl_msjRadView.Text = "No se generó ningun  error";
             }
             else
             {
                 lbl_msjRadView.Text = "Se generó " + gv_LogErrores.Items.Count+" errores";
             }
        }
        protected void btn_recalcularDg_Click(object sender, EventArgs e)
        {
             icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            try
            {
                DataTable dt;
                dt=oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_STOCK_REPORTE-tmp1", icompany, iservicio, canal, "0", "0", "0", "0", rcmb_año2.SelectedValue, rcmb_mes2.SelectedValue, rcmb_periodo2.SelectedValue);

                if (dt.Rows.Count > 0)
                {
                    lbl_msj_recalcular.Text = "recalculo exitoso";
                    lbl_msj_recalcular.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lbl_msj_recalcular.Text = "Insumos insuficientes";
                    lbl_msj_recalcular.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
                lbl_msj_recalcular.Text=ex.Message.ToString();
                lbl_msj_recalcular.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void rcmb_mes2_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lbl_msj_recalcular.Text = "";
            rcmb_periodo2.Items.Clear();
            rcmb_periodo2.Enabled = true;
            DataTable dtp = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", canal, icompany, Report, rcmb_mes2.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                rcmb_periodo2.DataSource = dtp;
                rcmb_periodo2.DataValueField = "id_periodo";
                rcmb_periodo2.DataTextField = "Periodo";
                rcmb_periodo2.DataBind();

                rcmb_periodo2.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));
            }
            else
            {
                rcmb_periodo2.DataBind();
            }
        }


    }
}
