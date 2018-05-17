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
    public partial class Reporte_v2_Impulso_SF_M : System.Web.UI.Page
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
        //private ReportViewer reporte1;
        //private ReportViewer reportedetalle;

        #region Funciones Especiales

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

        protected void GetPeridForAnalist()
        {//se obtiene el estado de un Reporte en un Año, mes y periodo especifico.Y otros datos adicionales del periodo obtenido
            /*
             pSalas 20/11/2011
             * Metodo Obtener Periodo para el Analista
             * 1.-Obtiene el id_reporte
             * 2.-Ejecuta el procedimiento almacenado: UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO
             *      Este procedimiento devuelve unicamente 1 registro ya solicita como parametro el periodo.
             */     
            Report = Convert.ToInt32(this.Session["Reporte"]);
            DataTable dt = null;
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO", canal, Report, sidaño, sidmes, sidperiodo);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    /*div para validar visible o invisible*/
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
                    /*lbl_validacion para mostrar el mes del periodo seleccionado */
                    lbl_validacion.Text = sidaño + "-" + dt.Rows[0]["Month_name"].ToString() + " " + sidperiodo;
                }
            }
        }

        protected void GetPeriodForClient()
        { //se obtiene el ultimo años mes y perido validado por el analista, para que el cliente pueda ver dicho reporte
            DataTable dt = null;

            Report = Convert.ToInt32(this.Session["Reporte"]);
            icompany = Convert.ToInt32(this.Session["companyid"]);
            canal = this.Session["Canal"].ToString().Trim();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_MAX_PERIODO_VALIDADO_SF", icompany, canal, Report);
            //dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_MAX_PERIODO_VALIDADO", Report);

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

        /*
         pSalas 20/11/2011
         * Asignar Variables Iniciales
         * Setea los valores para el año, mes, periodo y días
         */
        private void _AsignarVariables()
        {
            sidaño = cmb_año.SelectedValue;
            sidmes = cmb_mes.SelectedValue;
            sidperiodo = cmb_periodo.SelectedValue;
            //siddia = cmb_dia.SelectedValue;
            //sidcategoria = "9463";

            string sidperdil = this.Session["Perfilid"].ToString();
            /* En el caso de que el Cbm_Año, el Cmb_Mes y el Cmb_periodo son vacios entonces entra a este if
             * 1.-Dependiendo si el Perfil es de Analista 0090
             * 2.-Obtenemos el id_company, id_service, id_canal, el id_report adicionalmente el id_cadena y el id_categoria
             * 3.-A continuación se invoca una clase Periodo que recibe como parametros (id_report, id_cadena, id_categoria, id_canal, id_company, id_servicio)
             *      para encapsular la data y crear un objeto p del tipo periodo
             * 4.-Este objeto tiene un el método set_PeriodoConDataValidada, que no recibe ningún parámetros
             */
            if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0" && cmb_periodo.SelectedValue == "0")
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    //aca debe ir la carga inical para el analista
                    icompany = Convert.ToInt32(this.Session["companyid"]);
                    iservicio = Convert.ToInt32(this.Session["Service"]);
                    canal = this.Session["Canal"].ToString().Trim();
                    Report = Convert.ToInt32(this.Session["Reporte"]);
                    int cadena = Convert.ToInt32(cmb_nodocomercial.SelectedItem.Value);
                    string categoria = cmb_categoria.SelectedItem.Value;
                    //(int reportid, int icadena, string categoria, string canal, int cliente, int servicio)
                    /*Encapsula los parametros para convertirlo en un objeto*/
                    Periodo p = new Periodo(Report, cadena, categoria, canal, icompany, iservicio);

                    /*Ese objeto invoca un metodo*/
                    p.Set_PeriodoConDataValidada_New();
                    //datos quemados temporalmente
                    /*El año, el mes y el periodo que devuelve son actualmenteÑ
                     El año actual,
                     El mes actual,
                     y el Periodo es: Si la fecha actual se encuentra dentro del rango del periodo entonces la asigna,
                     en caso contrario simplemente cuenta cuantos periodos tiene el Año y Mes Actual*/
                    sidaño = p.Año;
                    sidmes =  p.Mes;
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
            //DataTable dtp = null;
            //dtp = Get_Administrativo.Get_obtener_Dias_Periodo(icompany, canal, Report, cmb_año.SelectedValue, cmb_mes.SelectedValue, Convert.ToInt32(cmb_periodo.SelectedValue));
            //if (dtp.Rows.Count > 0)
            //{
            //    cmb_dia.DataSource = dtp;
            //    cmb_dia.DataValueField = "id_periodo";
            //    cmb_dia.DataTextField = "Dias";
            //    cmb_dia.DataBind();

            //    cmb_dia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            //    cmb_dia.Enabled = true;
            //}
            //else
            //{
            //    dtp = null;
            //}
        }
        //métodos para llenar combos para filtros
        // Angel Ortiz
        // 14/10/2011
        private void llenacorporacion()
        {
            //Corporacion.corp_id, Corporacion.corp_name
            try
            {
                DataTable dtcorp = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CORPORACION", cmb_oficina.SelectedItem.Value);

                if (dtcorp.Rows.Count > 0)
                {
                    cmb_corporacion.Enabled = true;
                    cmb_corporacion.DataSource = dtcorp;
                    cmb_corporacion.DataTextField = "corp_name";
                    cmb_corporacion.DataValueField = "corp_id";
                    cmb_corporacion.DataBind();

                    cmb_corporacion.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
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
            //if (this.compañia != null)
            {
                int compañia = Convert.ToInt32(this.Session["companyid"]);
                DataTable dtofi = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENEROFICINAS", compañia);

                if (dtofi.Rows.Count > 0)
                {
                    cmb_oficina.Enabled = true;
                    cmb_oficina.DataSource = dtofi;
                    cmb_oficina.DataTextField = "Name_Oficina";
                    cmb_oficina.DataValueField = "cod_Oficina";
                    cmb_oficina.DataBind();

                    cmb_oficina.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
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
                DataTable dtcad = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CADENA", cmb_corporacion.SelectedItem.Value);

                if (dtcad.Rows.Count > 0)
                {
                    cmb_nodocomercial.Enabled = true;
                    cmb_nodocomercial.DataSource = dtcad;
                    cmb_nodocomercial.DataTextField = "commercialNodeName";
                    cmb_nodocomercial.DataValueField = "NodeCommercial";
                    cmb_nodocomercial.DataBind();

                    cmb_nodocomercial.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenapuntoventa()
        {
            ////UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA
            //try
            //{
            //    DataTable dtpventa = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA", cmb_nodocomercial.SelectedItem.Value);

            //    if (dtpventa.Rows.Count > 0)
            //    {
            //        cmb_pventa.Enabled = true;
            //        cmb_pventa.DataSource = dtpventa;
            //        cmb_pventa.DataTextField = "pdv_Name";
            //        cmb_pventa.DataValueField = "id_PointOfsale";
            //        cmb_pventa.DataBind();

            //        cmb_pventa.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("~/err_mensaje_seccion.aspx", true);
            //}
        }
        private void llenacategoria()
        {
            //UP_WEBXPLORA_OPE_COMBO_CATEGORIA
            try
            {
                DataTable dtcategoria = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA", icompany, canal);

                if (dtcategoria.Rows.Count > 0)
                {
                    cmb_categoria.Enabled = true;
                    cmb_categoria.DataSource = dtcategoria;
                    cmb_categoria.DataTextField = "Product_Category";
                    cmb_categoria.DataValueField = "id_ProductCategory";
                    cmb_categoria.DataBind();

                    cmb_categoria.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
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
                DataTable dtmarca = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_BY_CATEGORIA_ID", cmb_categoria.SelectedItem.Value);

                if (dtmarca.Rows.Count > 0)
                {
                    cmb_marca.Enabled = true;
                    cmb_marca.DataSource = dtmarca;
                    cmb_marca.DataTextField = "Name_Brand";
                    cmb_marca.DataValueField = "id_Brand";
                    cmb_marca.DataBind();

                    cmb_marca.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
                else
                {
                    cmb_marca.Items.Insert(0, new RadComboBoxItem("---No tiene marcas asociadas---", "0"));
                    cmb_familia.Items.Insert(0, new RadComboBoxItem("---No tiene familias asociadas---", "0"));
                    cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---No tiene subfamilias asociadas---", "0"));
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
                DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK", cmb_categoria.SelectedItem.Value);

                if (dtfamilia.Rows.Count > 0)
                {
                    cmb_familia.Enabled = true;
                    cmb_familia.DataSource = dtfamilia;
                    cmb_familia.DataTextField = "name_Family";
                    cmb_familia.DataValueField = "id_ProductFamily";
                    cmb_familia.DataBind();

                    cmb_familia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
                else
                {
                    cmb_familia.Items.Insert(0, new RadComboBoxItem("---No tiene familias asociadas---", "0"));
                    cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---No tiene subfamilias asociadas---", "0"));
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
                DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", cmb_familia.SelectedItem.Value);

                if (dtfamilia.Rows.Count > 0)
                {
                    cmb_subfamilia.Enabled = true;
                    cmb_subfamilia.DataSource = dtfamilia;
                    cmb_subfamilia.DataTextField = "subfam_nombre";
                    cmb_subfamilia.DataValueField = "id_ProductSubFamily";
                    cmb_subfamilia.DataBind();

                    cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
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
                    cmb_supervisor.Enabled = true;
                    cmb_supervisor.DataSource = dtsupervisor;
                    cmb_supervisor.DataTextField = "Person_NameComplet";
                    cmb_supervisor.DataValueField = "Person_id";
                    cmb_supervisor.DataBind();

                    cmb_supervisor.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenafuerzav()
        {
            //UP_WEBXPLORA_OPE_COMBO_FUERZA_VENTA
            try
            {
                DataTable dtfuerzav = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FUERZA_VENTA", icompany, canal);

                if (dtfuerzav.Rows.Count > 0)
                {
                    cmb_fuerzav.Enabled = true;
                    cmb_fuerzav.DataSource = dtfuerzav;
                    cmb_fuerzav.DataTextField = "pdv_contact_name";
                    cmb_fuerzav.DataValueField = "id_PointOfSale_Contact";
                    cmb_fuerzav.DataBind();

                    cmb_fuerzav.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            sUser = this.Session["sUser"].ToString();
            sPassw = this.Session["sPassw"].ToString();
            sNameUser = this.Session["nameuser"].ToString();
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();

            //TabContainer_filtros.ActiveTabIndex = 1;
            MyAccordion.SelectedIndex = 1;
            TabContainer_filtros.ActiveTabIndex = 0;

            if (!IsPostBack)
            {
                try
                {
                    iniciarcombos();

                    _AsignarVariables();
                    llenarreporteInicial();
                    llenarVentasXProducto_inicial();
                    llenarVentasXPtoVenta_inicial();
                    
                    cargarParametrosdeXml();
                    Años();
                    Llena_Meses();
                    llenaoficina();
                    llenacategoria();
                    llenafuerzav();
                    llenasupervisores();

                    llenaCorporacion_ini();
                    llenaNodoComercial_ini();
                    llenaPuntoDeVenta_ini();
                    llenaFamilia_ini();
                    llenaSubFamilia_ini();
                    llenaproductos_ini();
                    
                                    }
                catch (Exception ex)
                {
                    Exception mensaje = ex;
                    this.Session.Abandon();
                    //Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
        }

        private void iniciarcombos()
        {
            cmb_año.DataBind();
            cmb_año.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_mes.DataBind();
            cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_periodo.DataBind();
            cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            ddl_Dia.DataBind();
            //cmb_dia.DataBind();
            //cmb_dia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            cmb_oficina.DataBind();
            cmb_oficina.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_corporacion.DataBind();
            cmb_corporacion.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_nodocomercial.DataBind();
            cmb_nodocomercial.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            ddl_PuntoDeVenta.DataBind();
            //cmb_pventa.DataBind();
            //cmb_pventa.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            cmb_supervisor.DataBind();
            cmb_supervisor.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_fuerzav.DataBind();
            cmb_fuerzav.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            cmb_categoria.DataBind();
            cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_marca.DataBind();
            cmb_marca.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_familia.DataBind();
            cmb_familia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_subfamilia.DataBind();
            cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
        }

        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Llenar_Periodos();
        }

        protected void btn_ocultar_Click(object sender, EventArgs e)
        {
            if (Div_filtros.Visible == false)
            {
                Div_filtros.Visible = true;
                //btn_ocultar.Text = "Filtros";
                btngnerar.Visible = true;
                btnVentaXProducto.Visible = true;
                btnVentaXPtoVenta.Visible = true;
                
            }
            else if (Div_filtros.Visible == true)
            {

                Div_filtros.Visible = false;
                //btn_ocultar.Text = "Ocultar";
                btngnerar.Visible = false;
                btnVentaXProducto.Visible = false;
                btnVentaXPtoVenta.Visible = false;
                
            }
        }


        protected void cargarParametrosdeXml()
        {
            string path = Server.MapPath("~/parametros.xml");

            if (System.IO.File.Exists(path))
            {
                Reportes_parametros oReportes_parametros = new Reportes_parametros();
                Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

                oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
                oReportes_parametros.Id_user = this.Session["sUser"].ToString();
                oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
                oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
                oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

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
            if (e.CommandName.Equals("BUSCAR"))
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
                sidcategoria = lbl_id_categoria.Text.Trim();
                sidsub_categoria = lbl_id_subCategoria.Text.Trim();
                sidmarca = lbl_id_marca.Text.Trim();
                sidsub_marca = lbl_id_subMarca.Text.Trim();
                sidsku = lbl_skuProducto.Text.Trim();
                sidpuntoventa = lbl_id_pdv.Text.Trim();

                llenarreporteInicial();

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
            string path = Server.MapPath("~/parametros.xml");
            Reportes_parametros oReportes_parametros = new Reportes_parametros();

            oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
            oReportes_parametros.Id_user = this.Session["sUser"].ToString();
            oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
            oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
            oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            oReportes_parametros.Id_periodo = Convert.ToInt32(cmb_periodo.SelectedValue);
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

        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
        }

        private void llenarreporteInicial()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);


            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }


            try
            {
                //Impulso SF_Moderno
                rpt_impulso_sf_m.Visible = true;
                rpt_impulso_sf_m.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_impulso_sf_m.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_Impulso_SF_Moderno";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_impulso_sf_m.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_impulso_sf_m.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodocomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductocategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idmarca", cmb_marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductfamily", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idsupervisor", cmb_supervisor.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idffvv", cmb_fuerzav.SelectedItem.Value));


                rpt_impulso_sf_m.ServerReport.SetParameters(parametros);

                #region masreportes
                ////ventas por nivel
                //rpt_nivel.Visible = true;
                //rpt_nivel.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;

                //rpt_nivel.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_VENTAS_NIVEL_SF_AAVV";

                //String strConnection1 = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                //rpt_nivel.ServerReport.ReportServerUrl = new Uri(strConnection1);
                //rpt_nivel.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                //List<Microsoft.Reporting.WebForms.ReportParameter> parametros1 = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("category", sidcategoria));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", siddia));
                //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", sidperiodo));

                //rpt_nivel.ServerReport.SetParameters(parametros1);

                ////ventas por zona
                //rpt_zona.Visible = true;
                //rpt_zona.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;

                //rpt_zona.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_VENTAS_ZONAS_SF_AAVV";

                //String strConnection2 = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                //rpt_zona.ServerReport.ReportServerUrl = new Uri(strConnection2);
                //rpt_zona.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                //List<Microsoft.Reporting.WebForms.ReportParameter> parametros2 = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("category", sidcategoria));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", siddia));
                //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", sidperiodo));

                //rpt_zona.ServerReport.SetParameters(parametros2); 
                #endregion
            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }
        }
        private void ConsultarImpulso_SF_M()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string iiaño = cmb_año.SelectedValue;
            if (iiaño == "")
                iiaño = "0";

            string iimes = cmb_mes.SelectedValue;
            if (iimes == "")
                iimes = "0";

            string iiperiodo = cmb_periodo.SelectedValue;
            if (iiperiodo == "")
                iiperiodo = "0";

            try
            {
                //Impulso SF_Moderno
                rpt_impulso_sf_m.Visible = true;
                rpt_impulso_sf_m.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_impulso_sf_m.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_Impulso_SF_Moderno";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_impulso_sf_m.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_impulso_sf_m.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodocomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductocategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idmarca", cmb_marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductfamily", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idsupervisor", cmb_supervisor.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idffvv", cmb_fuerzav.SelectedItem.Value));


                rpt_impulso_sf_m.ServerReport.SetParameters(parametros);

                
            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }

         
        }
        private void llenarVentasXProducto_inicial()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }




            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }

            try
            {
                //Impulso SF_Moderno
                rpt_Vta_X_Prod.Visible = true;
                rpt_Vta_X_Prod.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Vta_X_Prod.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Impulso_Ventas_X_Producto";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Vta_X_Prod.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Vta_X_Prod.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodocomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductocategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idmarca", cmb_marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductfamily", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSKU", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idsupervisor", cmb_supervisor.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idffvv", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_Vta_X_Prod.ServerReport.SetParameters(parametros);


            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }


        }
        private void llenarVentasXProducto()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }




            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }

            string iiaño = cmb_año.SelectedValue;
            if (iiaño == "")
                iiaño = "0";

            string iimes = cmb_mes.SelectedValue;
            if (iimes == "")
                iimes = "0";

            string iiperiodo = cmb_periodo.SelectedValue;
            if (iiperiodo == "")
                iiperiodo = "0";

            try
            {
                //Impulso SF_Moderno
                rpt_Vta_X_Prod.Visible = true;
                rpt_Vta_X_Prod.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Vta_X_Prod.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Impulso_Ventas_X_Producto";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Vta_X_Prod.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Vta_X_Prod.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodocomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductocategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idmarca", cmb_marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductfamily", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSKU", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idsupervisor", cmb_supervisor.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idffvv", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_Vta_X_Prod.ServerReport.SetParameters(parametros);


            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }


        }
        private void llenarVentasXPtoVenta_inicial()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }

            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }

            try
            {
                //Impulso SF_Moderno
                rpt_Vta_X_Tda.Visible = true;
                rpt_Vta_X_Tda.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Vta_X_Tda.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Impulso_Ventas_X_PtoVenta";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Vta_X_Tda.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Vta_X_Tda.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodocomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductocategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idmarca", cmb_marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductfamily", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idsupervisor", cmb_supervisor.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSKU", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idffvv", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_Vta_X_Tda.ServerReport.SetParameters(parametros);


            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }


        }
        private void llenarVentasXPtoVenta()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }

            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }

            string iiaño = cmb_año.SelectedValue;
            if (iiaño == "")
                iiaño = "0";

            string iimes = cmb_mes.SelectedValue;
            if (iimes == "")
                iimes = "0";

            string iiperiodo = cmb_periodo.SelectedValue;
            if (iiperiodo == "")
                iiperiodo = "0";


            try
            {
                //Impulso SF_Moderno
                rpt_Vta_X_Tda.Visible = true;
                rpt_Vta_X_Tda.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Vta_X_Tda.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Impulso_Ventas_X_PtoVenta";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Vta_X_Tda.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Vta_X_Tda.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodocomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductocategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idmarca", cmb_marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductfamily", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idsupervisor", cmb_supervisor.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSKU", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idffvv", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_Vta_X_Tda.ServerReport.SetParameters(parametros);


            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }


        }
        
        protected void btngnerar_Click(object sender, EventArgs e)
        {
            MyAccordion.SelectedIndex = 1;
            _AsignarVariables();
            ConsultarImpulso_SF_M();
            llenarVentasXProducto();
            llenarVentasXPtoVenta();
        }

        protected void btnVentaXProducto_Click(object sender, EventArgs e)
        {
            MyAccordion.SelectedIndex = 1;
            _AsignarVariables();
            llenarVentasXProducto();

        }

        protected void btnVentaXPtoVenta_Click(object sender, EventArgs e)
        {
            MyAccordion.SelectedIndex = 1;
            _AsignarVariables();
            llenarVentasXPtoVenta();

        }

        protected void ddl_Familia_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Panel_Familia.Controls.Clear();
            //Panel_Familia.Controls.Add(new Literal() { Text = "<b>Las Familias seleccionadas son:</b>" + "<br/>" });
            //foreach (ListItem item in (sender as ListControl).Items)
            //{
            //    if (item.Selected)
            //    {

            //        Panel_Familia.Controls.Add(new Literal() { Text = item.Text + "<br/>" });

            //    }
            //}

            //string id_Familias = "";
            //if (ddl_Familia.SelectedIndex >= 0)
            //{

            //    string cadenaIdFamilia = "";

            //    for (int i = 0; i < ddl_Familia.Items.Count; i++)
            //    {
            //        if (ddl_Familia.Items[i].Selected)
            //        {
            //            cadenaIdFamilia = cadenaIdFamilia + ddl_Familia.Items[i].Value + ",";
            //        }
            //    }

            //    id_Familias = cadenaIdFamilia.Substring(0, cadenaIdFamilia.Length - 1);
            //}

            //llenasubfamilia_2(id_Familias);

        }

        private void llenasubfamilia_2(String id_familias)
        {
            ////dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", sidproductFamily);
            //try
            //{
            //    DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK_CHECK", id_familias);

            //    if (dtfamilia.Rows.Count > 0)
            //    {
            //        ddl_Subfamilia.Enabled = true;
            //        ddl_Subfamilia.DataSource = dtfamilia;
            //        ddl_Subfamilia.DataTextField = "subfam_nombre";
            //        ddl_Subfamilia.DataValueField = "id_ProductSubFamily";
            //        ddl_Subfamilia.DataBind();

            //        //cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("~/err_mensaje_seccion.aspx", true);
            //}
        }

        protected void ddl_Subfamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string id_SubFamilias = "";
            //try
            //{
            //    if (ddl_Subfamilia.SelectedIndex >= 0)
            //    {

            //        string cadenaIdSubFamilia = "";

            //        for (int i = 0; i < ddl_Subfamilia.Items.Count; i++)
            //        {
            //            if (ddl_Subfamilia.Items[i].Selected)
            //            {
            //                cadenaIdSubFamilia = cadenaIdSubFamilia + ddl_Subfamilia.Items[i].Value + ",";
            //            }
            //        }

            //        id_SubFamilias = cadenaIdSubFamilia.Substring(0, cadenaIdSubFamilia.Length - 1);
            //    }

            //    llenaproductos_2(id_SubFamilias);
            //}
            //catch (Exception ex)
            //{

            //}
        }


        private void llenaproductos_2(String id_SubFamilias)
        {
            try
            {
                DataTable dtprodutos = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SKU_PRODUCTO_REPORT_STOCK", id_SubFamilias);
                if (dtprodutos.Rows.Count > 0)
                {
                    ddl_Producto.Enabled = true;
                    ddl_Producto.DataSource = dtprodutos;
                    ddl_Producto.DataTextField = "productoNombre";
                    ddl_Producto.DataValueField = "cod_Product";
                    ddl_Producto.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        private void llenaproductos_ini()
        {
            try
            {
                DataTable dtprodutos = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_PRODUCTOS_INI", icompany, iservicio, canal, Report);
                if (dtprodutos.Rows.Count > 0)
                {
                    ddl_Producto.Enabled = true;
                    ddl_Producto.DataSource = dtprodutos;
                    ddl_Producto.DataTextField = "productoNombre";
                    ddl_Producto.DataValueField = "cod_Product";
                    ddl_Producto.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        private void llenafamilia_2()
        {
            ////UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK
            //try
            //{
            //    DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK", cmb_categoria.SelectedItem.Value);

            //    if (dtfamilia.Rows.Count > 0)
            //    {
            //        ddl_Familia.Enabled = true;
            //        ddl_Familia.DataSource = dtfamilia;
            //        ddl_Familia.DataTextField = "name_Family";
            //        ddl_Familia.DataValueField = "id_ProductFamily";
            //        ddl_Familia.DataBind();

            //        //ddl_Familia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
            //    }
            //    else
            //    {
            //        //ddl_Familia.Items.Insert(0, new RadComboBoxItem("---No tiene familias asociadas---", "0"));
            //        //ddl_Subfamilia.Items.Insert(0, new RadComboBoxItem("---No tiene subfamilias asociadas---", "0"));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("~/err_mensaje_seccion.aspx", true);
            //}
        }

        protected void ddl_Producto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





        protected void cmb_oficina_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenacorporacion();
        }
        protected void cmb_corporacion_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenacadena();
        }
        protected void cmb_nodocomercial_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenapuntoventa();
            llenapuntoventa_2();
        }

        private void llenapuntoventa_2()
        {
            //UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA
            try
            {
                DataTable dtpventa = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA", cmb_nodocomercial.SelectedItem.Value);

                if (dtpventa.Rows.Count > 0)
                {
                    ddl_PuntoDeVenta.Enabled = true;
                    ddl_PuntoDeVenta.DataSource = dtpventa;
                    ddl_PuntoDeVenta.DataTextField = "pdv_Name";
                    ddl_PuntoDeVenta.DataValueField = "id_PointOfsale";
                    ddl_PuntoDeVenta.DataBind();

                    //cmb_pventa.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void cmb_categoria_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenamarca();
        }
        protected void cmb_marca_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenafamilia();
            llenafamilia_2();
        }
        protected void cmb_familia_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenasubfamilia();
        }

        protected void cmb_periodo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LLenarDiasxPerido();
            LLenarDiasxPeriodo_2();
        }
        private void LLenarDiasxPeriodo_2()
        {

            //DataTable dtDias = oCoon.ejecutarDataTable("UP_WEBXPLORA_OBTENERDIASXPERIODO_SF_MODERNO", idCompany, idCanal, idReporte, idYear, idMonth, idPeriodo);
            DataTable dtDias = oCoon.ejecutarDataTable("UP_WEBXPLORA_OBTENERDIASXPERIODO_SF_MODERNO", icompany, canal, 51, cmb_año.SelectedValue, cmb_mes.SelectedValue, cmb_periodo.SelectedValue);


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

        protected void cmb_subfamilia_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenaproductos_2(cmb_subfamilia.SelectedValue);
        }

        private void llenaPuntoDeVenta_ini()
        {
            //UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA
            try
            {
                DataTable dtpventa = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_PuntoDeVenta_INI", icompany, canal);

                if (dtpventa.Rows.Count > 0)
                {
                    ddl_PuntoDeVenta.Enabled = true;
                    ddl_PuntoDeVenta.DataSource = dtpventa;
                    ddl_PuntoDeVenta.DataTextField = "pdv_Name";
                    ddl_PuntoDeVenta.DataValueField = "id_PointOfsale";
                    ddl_PuntoDeVenta.DataBind();

                    //cmb_pventa.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        private void llenaCorporacion_ini()
        {
            //Corporacion.corp_id, Corporacion.corp_name
            try
            {
                DataTable dtcorp = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CORPORACION_INI");

                if (dtcorp.Rows.Count > 0)
                {
                    cmb_corporacion.Enabled = true;
                    cmb_corporacion.DataSource = dtcorp;
                    cmb_corporacion.DataTextField = "corp_name";
                    cmb_corporacion.DataValueField = "corp_id";
                    cmb_corporacion.DataBind();

                    cmb_corporacion.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        private void llenaNodoComercial_ini()
        {
            //UP_WEBXPLORA_OPE_COMBO_CADENA
            try
            {
                DataTable dtcad = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_NodoComercial_INI", icompany, canal);

                if (dtcad.Rows.Count > 0)
                {
                    cmb_nodocomercial.Enabled = true;
                    cmb_nodocomercial.DataSource = dtcad;
                    cmb_nodocomercial.DataTextField = "commercialNodeName";
                    cmb_nodocomercial.DataValueField = "NodeCommercial";
                    cmb_nodocomercial.DataBind();

                    cmb_nodocomercial.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        private void llenaFamilia_ini()
        {
            //UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK
            try
            {
                DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_FAMILIA_INI", icompany, canal, Report);

                if (dtfamilia.Rows.Count > 0)
                {
                    cmb_familia.Enabled = true;
                    cmb_familia.DataSource = dtfamilia;
                    cmb_familia.DataTextField = "name_Family";
                    cmb_familia.DataValueField = "id_ProductFamily";
                    cmb_familia.DataBind();

                    cmb_familia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
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

        private void llenaSubFamilia_ini()
        {
            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", sidproductFamily);
            try
            {
                DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_SUBFAMILIA_INI", icompany, canal, Report);

                if (dtfamilia.Rows.Count > 0)
                {
                    cmb_subfamilia.Enabled = true;
                    cmb_subfamilia.DataSource = dtfamilia;
                    cmb_subfamilia.DataTextField = "subfam_nombre";
                    cmb_subfamilia.DataValueField = "id_ProductSubFamily";
                    cmb_subfamilia.DataBind();

                    cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
    }
}