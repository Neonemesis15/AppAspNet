using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Lucky.Data;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;
using Lucky.CFG.Util;
using System.Net;


namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_v2_Competencia_SF_M : System.Web.UI.Page
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

                    /*
                     check para mostrar si el reporte está validado o nó por el analista
                     */
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

        private void _AsignarVariables()
        {
            sidaño = cmb_año.SelectedValue;
            sidmes = cmb_mes.SelectedValue;
            sidperiodo = cmb_periodo.SelectedValue;
            //siddia = cmb_dia.SelectedValue;
            //sidcategoria = "9463";

            string sidperdil = this.Session["Perfilid"].ToString();
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
                    Periodo p = new Periodo(Report, cadena, categoria, canal, icompany, iservicio);

                    p.Set_PeriodoConDataValidada_New();
                    //datos quemados temporalmente
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
                    //cmb_familia.Items.Insert(0, new RadComboBoxItem("---No tiene familias asociadas---", "0"));
                    //cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---No tiene subfamilias asociadas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        //private void llenafamilia()
        //{
        //    //UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK
        //    try
        //    {
        //        DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK", cmb_categoria.SelectedItem.Value);

        //        if (dtfamilia.Rows.Count > 0)
        //        {
        //            cmb_familia.Enabled = true;
        //            cmb_familia.DataSource = dtfamilia;
        //            cmb_familia.DataTextField = "name_Family";
        //            cmb_familia.DataValueField = "id_ProductFamily";
        //            cmb_familia.DataBind();

        //            cmb_familia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
        //        }
        //        else
        //        {
        //            cmb_familia.Items.Insert(0, new RadComboBoxItem("---No tiene familias asociadas---", "0"));
        //            //cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---No tiene subfamilias asociadas---", "0"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Redirect("~/err_mensaje_seccion.aspx", true);
        //    }
        //}

        //private void llenasubfamilia()
        //{
        //    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", sidproductFamily);
        //    try
        //    {
        //        DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", cmb_familia.SelectedItem.Value);

        //        if (dtfamilia.Rows.Count > 0)
        //        {
        //            cmb_subfamilia.Enabled = true;
        //            cmb_subfamilia.DataSource = dtfamilia;
        //            cmb_subfamilia.DataTextField = "subfam_nombre";
        //            cmb_subfamilia.DataValueField = "id_ProductSubFamily";
        //            cmb_subfamilia.DataBind();

        //            cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Redirect("~/err_mensaje_seccion.aspx", true);
        //    }
        //}

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

            MyAccordion.SelectedIndex = 1;
            //TabContainer_filtros.ActiveTabIndex = 1;
            TabContainer_Reporte_Ventas_Distribuidora_AAVV.ActiveTabIndex = 0;

            if (!IsPostBack)
            {
                try
                {
                    iniciarcombos();
                    _AsignarVariables();
                    llenarreporteInicial();
                    llenarActComp_inicial();
                    llenarActCompXPtoVenta_inicial();
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
                    llenaTargetGroup_ini();         //Add 11/12/2011
                    llenaEmpresaCompetidora_ini();  //Add 11/12/2011
                    llenaMarcaCompetidora_ini();    //Add 11/12/2011
                    llenaTipoActividad_ini();       //Add 11/12/2011

                    //llenaFamilia_ini();
                    //llenaSubFamilia_ini();

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
            //cmb_familia.DataBind();
            //cmb_familia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            //cmb_subfamilia.DataBind();
            //cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            cmb_targetGroup.DataBind();
            cmb_targetGroup.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            
            cmb_companyCompetidora.DataBind();
            cmb_companyCompetidora.Items.Insert(0, new RadComboBoxItem("--Todos--","0"));
            
            cmb_marca.DataBind();
            cmb_marca.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            
            cmb_tipoActividad.DataBind();
            cmb_tipoActividad.Items.Insert(0, new RadComboBoxItem("--Todos--","0"));

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
                //btn_ocultar.Text = "Ocultar";
                btngnerar.Visible = true;
                
            }
            else if (Div_filtros.Visible == true)
            {

                
                Div_filtros.Visible = false;
                //btn_ocultar.Text = "Filtros";
                btngnerar.Visible = false;
            }
        }

        protected void btngnerar_Click(object sender, EventArgs e)
        {
            chkb_invalidar.Checked = false;
            chkb_validar.Checked = false;
            MyAccordion.SelectedIndex = 1;
            _AsignarVariables();
            cargar_rgv_competencia();
            llenarActComp();
            llenarActCompXPtoVenta();
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

                //RadGrid_parametros.DataSource = oReportes_parametrosToXml.Get_Parametros(oReportes_parametros, path);
                //RadGrid_parametros.DataBind();
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

                //TabContainer_filtros.ActiveTabIndex = 0;
                //lbl_updateconsulta.Visible = true;
                //btn_img_actualizar.Visible = true;
                //lbl_saveconsulta.Visible = false;
                //btn_img_add.Visible = false;
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
            //string path = Server.MapPath("~/parametros.xml");
            //Reportes_parametros oReportes_parametros = new Reportes_parametros();

            //oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
            //oReportes_parametros.Id_user = this.Session["sUser"].ToString();
            //oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
            //oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
            //oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

            //oReportes_parametros.Id_año = cmb_año.SelectedValue;
            //oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            //oReportes_parametros.Id_periodo = Convert.ToInt32(cmb_periodo.SelectedValue);
            //oReportes_parametros.Descripcion = txt_descripcion_parametros.Text.Trim();

            //Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

            //if (!System.IO.File.Exists(path))
            //    oReportes_parametrosToXml.createXml(oReportes_parametros, path);
            //else
            //    oReportes_parametrosToXml.addToXml(oReportes_parametros, path);

            //cargarParametrosdeXml();
            //txt_descripcion_parametros.Text = "";
            //TabContainer_filtros.ActiveTabIndex = 1;
        }
        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
        }
      

        protected void cmb_oficina_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenacorporacion();
        }
    
       
        protected void cmb_categoria_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenamarca();
        }
        protected void cmb_marca_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //llenafamilia();
        }
        protected void cmb_familia_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //llenasubfamilia();
        }



        /*  Metodo para Cargar las imagenes     */
        private DataTable load_images_rgv_competencia(DataTable source)
        {
            /*
             DataTable source
             * 1.-Creamos un DataTable fotos
             * 2.-Conexión OConn--selecciondo Tipo de conexion 1
             */

            DataTable fotos = new DataTable();
            Conexion OConn = new Conexion(1);


            /*
             Agregamos en el datatable dos columnas
             * 1.-identifica el id_regft
             * 2.-identifica la foto
             */
            source.Columns.Add("Id_regft", typeof(Int64));
            source.Columns.Add("foto", typeof(Byte[]));

            /*
             creamos un for
             * fotos es una DataTable
             */

            for (int i = 0; i < source.Rows.Count; i++)
            {
                fotos = OConn.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_REPORTE_COMPETENCIA_FOTO", source.Rows[i]["Id_rcompe"].ToString());
                if (fotos.Rows.Count > 0 && fotos.Rows[0]["foto"] != System.DBNull.Value)
                {
                    source.Rows[i]["Id_regft"] = Convert.ToInt64(fotos.Rows[0]["Id_regft"]);
                    source.Rows[i]["foto"] = (Byte[])(fotos.Rows[0]["foto"]);
                }
                else
                {
                    source.Rows[i]["Id_regft"] = 0;
                    source.Rows[i]["foto"] = getBytesNoImage();//Este método trae una imagen del servidor que indica que no hay imagen para este registro
                }
            }
            return source;
        }

        /*  Bytes de No Image - Este método recupera una imagen en caso no encuentra imagen    */
        #region BytesNoImagen
        private Byte[] getBytesNoImage()
        {
            WebResponse result = null;
            byte[] rBytes = null;

            try
            {
                //hace una solicitud al servidor de una imagen
                WebRequest request = WebRequest.Create("http://sige.lucky.com.pe/Pages/Modulos/Cliente/Reportes/Galeria_fotografica/Fotos/sin_url_imagen.jpg");

                // Get the content
                result = request.GetResponse();
                Stream rStream = result.GetResponseStream();

                // Bytes from address
                using (BinaryReader br = new BinaryReader(rStream))
                {
                    // Ask for bytes bigger than the actual stream
                    rBytes = br.ReadBytes(1000000);
                    br.Close();
                }
                // close down the web response object
                result.Close();
            }
            catch (Exception ex)
            {

            }
            return rBytes;
        }
        #endregion

        /*  pSalas. 
         * 16/11/2011
         * Método que sirbe para enviar dos commandName si es VERFOTO ó EDITFOTO     */
        protected void rgv_competencia_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                panelEdit.DataBind();
                RadBinaryImage_fotoBig.DataBind();
                RadBinaryImage_fotoBig.Visible = false;
                GridItem item = rgv_competencia.Items[e.Item.ItemIndex];
                Label lbl_id_reg_foto = (Label)item.FindControl("lbl_id_reg_foto");
                Label lbl_id_reg_competencia = (Label)item.FindControl("lbl_id_reg_competencia");

                if (e.CommandName == "VERFOTO")
                {
                    int iidregft = Convert.ToInt32(lbl_id_reg_foto.Text);
                    byte[] byteArrayIn;
                    if (iidregft != 0)
                    {
                        DataTable dt = null;
                        Conexion Ocoon = new Conexion();
                        dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REG_FOTO", iidregft);
                        byteArrayIn = (byte[])dt.Rows[0]["foto"];
                        dt = null;
                        Ocoon = null;
                    }
                    else
                        byteArrayIn = getBytesNoImage();

                    RadBinaryImage_viewFoto.DataValue = byteArrayIn;
                    ModalPopupExtender_viewfoto.Show();
                }
                //else if (e.CommandName == "EDITFOTO")
                //{
                //    int iidregft = Convert.ToInt32(lbl_id_reg_foto.Text);
                //    int Id_rcompe = Convert.ToInt32(lbl_id_reg_competencia.Text);
                //    RadBinaryImage imageBinary = (RadBinaryImage)item.FindControl("RadBinaryImage_foto");

                //    RadBinaryImage_fotoBig.DataValue = imageBinary.DataValue;
                //    RadBinaryImage_fotoBig.Visible = false;
                //    Session["iidregft"] = iidregft;
                //    Session["Id_rcompe"] = Id_rcompe;

                //    ModalPopup_Edit.Show();
                //}
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }


        #region Commandos para editar el GridView
        protected void rgv_competencia_DataBound(object sender, EventArgs e)
        {

        }
        //protected void rgv_competencia_ItemCommand(object source, GridCommandEventArgs e)
        //{

        //}
        protected void rgv_competencia_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {

        }
        protected void rgv_competencia_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {

        }
        protected void rgv_competencia_UpdateCommand(object source, GridCommandEventArgs e)
        {

        }
        protected void rgv_competencia_CancelCommand(object source, GridCommandEventArgs e)
        {

        }
        protected void rgv_competencia_EditCommand(object source, GridCommandEventArgs e)
        {

        }
        #endregion

        protected void BtnclosePanel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RadBinaryImage_fotoBig.DataBind();
                RadBinaryImage_fotoBig.Visible = false;
                ModalPopup_Edit.Hide();
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void imgbtn_save_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgbtn_cancel_Click(object sender, ImageClickEventArgs e)
        {

        }
        
        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
        //    gv_competenciaToExcel.Visible = true;
        //    GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Comeptencia", this.gv_competenciaToExcel);

        }
        protected void btn_img_toExcel_Click(object sender, ImageClickEventArgs e)
        {
            gv_competenciaToExcel.Visible = true;
            GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Competencia", this.gv_competenciaToExcel);

            //gv_competenciaToExcel.Visible = true;
            //ExportToExcel("Reporte_Comeptencia");
        }
        //private void ExportToExcel(string strFileName)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    System.IO.StringWriter sw = new System.IO.StringWriter(sb);
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);

        //    Page page = new Page();
        //    HtmlForm form = new HtmlForm();

        //    //gv_competencia.EnableViewState = false;
        //    //gv_competencia.AllowPaging = false;
        //    //gv.DataBind();

        //    page.EnableEventValidation = false;
        //    page.DesignerInitialize();
        //    page.Controls.Add(form);
        //    form.Controls.Add(gv_competenciaToExcel);
        //    page.RenderControl(htw);

        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ContentType = "application/ms-excel";// vnd.ms-excel";
        //    Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName + ".xls");
        //    Response.Charset = "UTF-8";

        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentEncoding = System.Text.Encoding.Default;
        //    Response.Write(sb.ToString());
        //    Response.End();
        //}
        private void llenarreporteInicial()
        {

            try
            {
                /*
                 Cambiar la lógica para la primera carga y a continuación y luego invocar al método para cargar
                 * psalas 17/11/2011
                 */
                //cargar_rgv_competencia();

                /*
                 psalas realizar la primera carga.
                 */
                DataTable dt = null;

                //int iidperson = Convert.ToInt32(cmbperson.SelectedValue);
                //string sidplanning = cmbplanning.SelectedValue;
                int iiCompany_id = Convert.ToInt32(this.Session["companyid"]);
                int iiservicio = Convert.ToInt32(this.Session["Service"]);
                string sidchannel = this.Session["Canal"].ToString().Trim();

                int icod_oficina = Convert.ToInt32(cmb_oficina.SelectedValue);

                int iidNodeComercial = Convert.ToInt32(cmb_nodocomercial.SelectedValue);

                //string sidPDV = cmb_pventa.SelectedValue;
                //if (sidPDV == "")
                //    sidPDV = "0";

                string sidcategoriaProducto = cmb_categoria.SelectedValue;
                if (sidcategoriaProducto == "")
                    sidcategoriaProducto = "0";

                string sidmarca = cmb_marca.SelectedValue;
                if (sidmarca == "")
                    sidmarca = "0";
                string iidcorporacion = cmb_corporacion.SelectedValue;
                if (iidcorporacion == "")
                    iidcorporacion = "0";

                //Add 11/12/2011
                string sidtargetGroup = cmb_targetGroup.SelectedValue;
                if (sidtargetGroup == "")
                    sidtargetGroup = "0";
                //Add 11/12/2011
                string sidtipoActividad = cmb_tipoActividad.SelectedValue;
                if (sidtipoActividad == "")
                    sidtipoActividad = "0";
                //Add 11/12/2011
                string sidcompanyCompetidora = cmb_companyCompetidora.SelectedValue;
                if (sidcompanyCompetidora == "")
                    sidcompanyCompetidora = "0";

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


                //string iiaño = cmb_año.SelectedValue;
                //if (iiaño == "")
                //    iiaño = "0";

                //string iimes = cmb_mes.SelectedValue;
                //if (iimes == "")
                //    iimes = "0";

                //string iiperiodo = cmb_periodo.SelectedValue;
                //if (iiperiodo == "")
                //    iiperiodo = "0";


                Conexion Ocoon = new Conexion();

                //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_COMPETENCIA_SF_MODERNO", sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, iitargetGroup, iitipoActividad, iiempresaCompetidora, dfecha_inicio, dfecha_fin);
                //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_COMPETENCIA_SF_MODERNO", "004552352011", 1003, 0, 0, 0, 0, 0, 0, 0, 0, 0, "14/11/2011", "21/11/2011");
                //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_COMPETENCIA_SF_MODERNO", iiCompany_id, iiservicio, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, 0, 0, 0, sidaño, sidmes, sidperiodo); //pendiente filtro de GrupoObjetivo, TipoDeActividad, IdEmpresaCompetidora 17/11/2011
                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_COMPETENCIA_SF_MODERNO", iiCompany_id, iiservicio, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, id_PuntosDeVenta, sidcategoriaProducto, sidmarca, sidtargetGroup, sidtipoActividad, sidcompanyCompetidora, sidaño, sidmes, sidperiodo);

                //[UP_WEBXPLORA_CLIE_V2_COMPETENCIA_SF_MODERNO]

                DataTable source = load_images_rgv_competencia(dt);

                rgv_competencia.DataSource = source;
                rgv_competencia.DataBind();
                btn_img_toExcel.Enabled = true;

                gv_competenciaToExcel.DataSource = dt;
                gv_competenciaToExcel.DataBind();

                //lblmensaje.Visible = true;
                //lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";

                //}

            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }
        }
        /*psalas 16/11/2011 */
        //metodo para cargar la grilla en el radTelerikGridView datos  y aparte invoca un método para obtener la imagen
        private void cargar_rgv_competencia()
        {
            MyAccordion.SelectedIndex = 1;
            try
            {
                DataTable dt = null;

                //int iidperson = Convert.ToInt32(cmbperson.SelectedValue);
                //string sidplanning = cmbplanning.SelectedValue;
                int iiCompany_id = Convert.ToInt32(this.Session["companyid"]);
                int iiservicio = Convert.ToInt32(this.Session["Service"]);
                string sidchannel = this.Session["Canal"].ToString().Trim();

                int icod_oficina = Convert.ToInt32(cmb_oficina.SelectedValue);

                int iidNodeComercial = Convert.ToInt32(cmb_nodocomercial.SelectedValue);

                //string sidPDV = cmb_pventa.SelectedValue;
                //if (sidPDV == "")
                //    sidPDV = "0";


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


                string sidcategoriaProducto = cmb_categoria.SelectedValue;
                if (sidcategoriaProducto == "")
                    sidcategoriaProducto = "0";

                string sidmarca = cmb_marca.SelectedValue;
                if (sidmarca == "")
                    sidmarca = "0";
                string iidcorporacion = cmb_corporacion.SelectedValue;
                if (iidcorporacion == "")
                    iidcorporacion = "0";

                string iiaño = cmb_año.SelectedValue;
                if (iiaño == "")
                    iiaño = "0";

                string iimes = cmb_mes.SelectedValue;
                if (iimes == "")
                    iimes = "0";

                string iiperiodo = cmb_periodo.SelectedValue;
                if (iiperiodo == "")
                    iiperiodo = "0";

                //Add 11/12/2011
                string sidtargetGroup = cmb_targetGroup.SelectedValue;
                if (sidtargetGroup == "")
                    sidtargetGroup = "0";
                //Add 11/12/2011
                string sidtipoActividad = cmb_tipoActividad.SelectedValue;
                if (sidtipoActividad == "")
                    sidtipoActividad = "0";
                //Add 11/12/2011
                string sidcompanyCompetidora = cmb_companyCompetidora.SelectedValue;
                if (sidcompanyCompetidora == "")
                    sidcompanyCompetidora = "0";

                Conexion Ocoon = new Conexion();

                //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_COMPETENCIA_SF_MODERNO", sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, iitargetGroup, iitipoActividad, iiempresaCompetidora, dfecha_inicio, dfecha_fin);
                //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_COMPETENCIA_SF_MODERNO", "004552352011", 1003, 0, 0, 0, 0, 0, 0, 0, 0, 0, "14/11/2011", "21/11/2011");
                //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_COMPETENCIA_SF_MODERNO", iiCompany_id, iiservicio, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, 0, 0, 0, iiaño, iimes, iiperiodo); //pendiente filtro de GrupoObjetivo, TipoDeActividad, IdEmpresaCompetidora 17/11/2011
                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_COMPETENCIA_SF_MODERNO", iiCompany_id, iiservicio, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, id_PuntosDeVenta, sidcategoriaProducto, sidmarca, sidtargetGroup, sidtipoActividad, sidcompanyCompetidora, iiaño, iimes, iiperiodo);


                //[UP_WEBXPLORA_CLIE_V2_COMPETENCIA_SF_MODERNO]

                DataTable source = load_images_rgv_competencia(dt);

                rgv_competencia.DataSource = source;
                rgv_competencia.DataBind();
                btn_img_toExcel.Enabled = true;

                gv_competenciaToExcel.DataSource = dt;
                gv_competenciaToExcel.DataBind();

                //lblmensaje.Visible = true;
                //lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";

                //}
            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                rgv_competencia.DataBind();
                //    lblmensaje.Text = "";
                //    if (cmbplanning.SelectedIndex > 0)
                //        lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
            }
        }
        private void llenarActComp_inicial()
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
                rpt_ActComp.Visible = true;
                rpt_ActComp.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_ActComp.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Competencia_Actividad_Competencia";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_ActComp.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_ActComp.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodocomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", cmb_pventa.SelectedItem.Value));id_PuntosDeVenta
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductocategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idmarca", cmb_marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idtargetgroup", cmb_targetGroup.SelectedItem.Value));          //Modified 11/12/2011
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idtipoactividad", cmb_tipoActividad.SelectedItem.Value));      //Modified 11/12/2011
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idempresacompetidora", cmb_companyCompetidora.SelectedValue));         //Modified 11/12/2011
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_ActComp.ServerReport.SetParameters(parametros);


            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }


        }
        private void llenarActComp()
        {
            MyAccordion.SelectedIndex = 1;
            //icompany = Convert.ToInt32(this.Session["companyid"]);
            //iservicio = Convert.ToInt32(this.Session["Service"]);
            //canal = this.Session["Canal"].ToString().Trim();
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
                rpt_ActComp.Visible = true;
                rpt_ActComp.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_ActComp.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Competencia_Actividad_Competencia";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_ActComp.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_ActComp.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodocomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", cmb_pventa.SelectedItem.Value));id_PuntosDeVenta
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientePDVcodigo", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductocategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idmarca", cmb_marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idtargetgroup", cmb_targetGroup.SelectedItem.Value));//Modified 11/12/2011
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idtipoactividad", cmb_tipoActividad.SelectedItem.Value));//Modified 11/12/2011
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idempresacompetidora", cmb_companyCompetidora.SelectedValue));//Modified 11/12/2011
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_ActComp.ServerReport.SetParameters(parametros);


            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }


        }
        private void llenarActCompXPtoVenta_inicial()
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
                rpt_ActComp_X_Tda.Visible = true;
                rpt_ActComp_X_Tda.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_ActComp_X_Tda.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Competencia_Actividades_X_PtoVenta";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_ActComp_X_Tda.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_ActComp_X_Tda.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
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
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idtargetgroup", cmb_targetGroup.SelectedItem.Value));//Modified 11/12/2011
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idtipoactividad", cmb_tipoActividad.SelectedItem.Value));//Modified 11/12/2011
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idempresacompetidora", cmb_companyCompetidora.SelectedValue));//Modified 11/12/2011
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_ActComp_X_Tda.ServerReport.SetParameters(parametros);


            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }


        }
        private void llenarActCompXPtoVenta()
        {
            MyAccordion.SelectedIndex = 1;
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
                rpt_ActComp_X_Tda.Visible = true;
                rpt_ActComp_X_Tda.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_ActComp_X_Tda.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Competencia_Actividades_X_PtoVenta";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_ActComp_X_Tda.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_ActComp_X_Tda.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
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
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idtargetgroup", cmb_targetGroup.SelectedItem.Value));//Modified 11/12/2011
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idtipoactividad", cmb_tipoActividad.SelectedItem.Value));//Modified 11/12/2011
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idempresacompetidora", cmb_companyCompetidora.SelectedValue));//Modified 11/12/2011
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_ActComp_X_Tda.ServerReport.SetParameters(parametros);


            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }


        }

        protected void btnActComp_Click(object sender, EventArgs e)
        {
            _AsignarVariables();
            llenarActComp();
        }
        protected void btnActCompPtoVenta_Click(object sender, EventArgs e)
        {
            _AsignarVariables();
            llenarActCompXPtoVenta();
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

        protected void cmb_periodo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LLenarDiasxPerido();
            LLenarDiasxPeriodo_2();
        }

        private void LLenarDiasxPeriodo_2()
        {

            //DataTable dtDias = oCoon.ejecutarDataTable("UP_WEBXPLORA_OBTENERDIASXPERIODO_SF_MODERNO", idCompany, idCanal, idReporte, idYear, idMonth, idPeriodo);
            DataTable dtDias = oCoon.ejecutarDataTable("UP_WEBXPLORA_OBTENERDIASXPERIODO_SF_MODERNO", icompany, canal, 28, cmb_año.SelectedValue, cmb_mes.SelectedValue, cmb_periodo.SelectedValue);


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

        private void llenaTargetGroup_ini()
        {
            //Corporacion.corp_id, Corporacion.corp_name
            try
            {
                DataTable dtTargetGroup = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_TargetGroup",canal);

                if (dtTargetGroup.Rows.Count > 0)
                {
                    cmb_targetGroup.Enabled = true;
                    cmb_targetGroup.DataSource = dtTargetGroup;
                    cmb_targetGroup.DataTextField = "TargetGroup";
                    cmb_targetGroup.DataValueField = "id_TargetGroup";
                    cmb_targetGroup.DataBind();

                    cmb_targetGroup.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }


        private void llenaEmpresaCompetidora_ini()
        {
            //Corporacion.corp_id, Corporacion.corp_name
            try
            {
                DataTable dtCompanyCompetidora = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_CompanyCompetidora");

                if (dtCompanyCompetidora.Rows.Count > 0)
                {
                    cmb_companyCompetidora.Enabled = true;
                    cmb_companyCompetidora.DataSource = dtCompanyCompetidora;
                    cmb_companyCompetidora.DataTextField = "Company_Name";
                    cmb_companyCompetidora.DataValueField = "Company_id";
                    cmb_companyCompetidora.DataBind();

                    cmb_companyCompetidora.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }


        private void llenaMarcaCompetidora_ini()
        {
            //Corporacion.corp_id, Corporacion.corp_name
            try
            {
                DataTable dtMarcaCompetidora = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_MarcaCompetidora_ini");

                if (dtMarcaCompetidora.Rows.Count > 0)
                {
                    cmb_marca.Enabled = true;
                    cmb_marca.DataSource = dtMarcaCompetidora;
                    cmb_marca.DataTextField = "Name_Brand";
                    cmb_marca.DataValueField = "id_Brand";
                    cmb_marca.DataBind();

                    cmb_marca.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        private void llenaMarcaCompetidora()
        {
            //Corporacion.corp_id, Corporacion.corp_name
            try
            {
                DataTable dtMarcaCompetidora = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_MarcaCompetidora",cmb_companyCompetidora.SelectedValue);

                if (dtMarcaCompetidora.Rows.Count > 0)
                {
                    cmb_marca.Enabled = true;
                    cmb_marca.DataSource = dtMarcaCompetidora;
                    cmb_marca.DataTextField = "Name_Brand";
                    cmb_marca.DataValueField = "id_Brand";
                    cmb_marca.DataBind();

                    cmb_marca.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        private void llenaTipoActividad_ini()
        {
            //Corporacion.corp_id, Corporacion.corp_name
            try
            {
                DataTable dtTipoActividad = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_TipoActividad", icompany, iservicio,canal);

                if (dtTipoActividad.Rows.Count > 0)
                {
                    cmb_tipoActividad.Enabled = true;
                    cmb_tipoActividad.DataSource = dtTipoActividad;
                    cmb_tipoActividad.DataTextField = "descripcion";
                    cmb_tipoActividad.DataValueField = "id_Tipo_Act";
                    cmb_tipoActividad.DataBind();

                    cmb_tipoActividad.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void cmb_companyCompetidora_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if(cmb_companyCompetidora.SelectedIndex>0)
                llenaMarcaCompetidora();
            if (cmb_companyCompetidora.SelectedIndex == 0)
                llenaMarcaCompetidora_ini();
        }



        //private void llenaFamilia_ini()
        //{
        //    //UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK
        //    try
        //    {
        //        DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_FAMILIA_INI", icompany, canal, Report);

        //        if (dtfamilia.Rows.Count > 0)
        //        {
        //            cmb_familia.Enabled = true;
        //            cmb_familia.DataSource = dtfamilia;
        //            cmb_familia.DataTextField = "name_Family";
        //            cmb_familia.DataValueField = "id_ProductFamily";
        //            cmb_familia.DataBind();

        //            cmb_familia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
        //        }
        //        else
        //        {
        //            //ddl_Familia.Items.Insert(0, new RadComboBoxItem("---No tiene familias asociadas---", "0"));
        //            //ddl_Subfamilia.Items.Insert(0, new RadComboBoxItem("---No tiene subfamilias asociadas---", "0"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Redirect("~/err_mensaje_seccion.aspx", true);
        //    }
        //}

        //private void llenaSubFamilia_ini()
        //{
        //    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", sidproductFamily);
        //    try
        //    {
        //        DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_SUBFAMILIA_INI", icompany, canal, Report);

        //        if (dtfamilia.Rows.Count > 0)
        //        {
        //            cmb_subfamilia.Enabled = true;
        //            cmb_subfamilia.DataSource = dtfamilia;
        //            cmb_subfamilia.DataTextField = "subfam_nombre";
        //            cmb_subfamilia.DataValueField = "id_ProductSubFamily";
        //            cmb_subfamilia.DataBind();

        //            cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Redirect("~/err_mensaje_seccion.aspx", true);
        //    }
        //}
    }
}