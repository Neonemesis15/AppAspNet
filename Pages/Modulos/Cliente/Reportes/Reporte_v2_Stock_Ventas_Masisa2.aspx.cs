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
    public partial class Reporte_v2_Stock_Ventas_Masisa2 : System.Web.UI.Page
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


        string year ;
        string month ;
        int iperiodo ;
        
               


        #endregion


        private string sidaño;
        private string sidmes;
        private string sidperiodo;
        private string sidcategoria;


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


            if (!IsPostBack)
            {
                try
                {



                    cmb_año.DataBind();
                    cmb_año.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_mes.DataBind();
                    cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_periodo.DataBind();
                    cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));


                    cmb_categoria.DataBind();
                    cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));


                    //cmb_Dias.DataBind();
                    //cmb_Dias.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));



                    llenar_Años();
                    llenar_Meses();
                    llenameses_2();
                    llenar_Dia();
                    llenar_puntoVenta();


                    llenarCategoria();
                    llenarSubcategoria();

                    llenarProducto();

                    _AsignarVariables();
                    ObtenerInformePrecio();
                    ObtenerInformeStock_VentasXSemana();
                    ObtenerInformeStock_VentasXDia();
                    EvoluciondeVentasSemanalesXLinea();


                    cargarParametrosdeXml();

                }

                catch (Exception ex)
                {

                    Exception mensaje = ex;
                    this.Session.Abandon();
                    //Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
        }



        #region Llenado Datos

        private void llenar_Años()
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
        private void llenar_Dia()
        {
            cmb_Dia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_Dia.Items.Insert(1, new RadComboBoxItem("Lunes", "1"));
            cmb_Dia.Items.Insert(2, new RadComboBoxItem("Martes", "2"));
            cmb_Dia.Items.Insert(3, new RadComboBoxItem("Miercoles", "3"));
            cmb_Dia.Items.Insert(4, new RadComboBoxItem("Jueves", "4"));
            cmb_Dia.Items.Insert(5, new RadComboBoxItem("Viernes", "5"));
            cmb_Dia.Items.Insert(6, new RadComboBoxItem("Sabado", "6"));
            cmb_Dia.Items.Insert(7, new RadComboBoxItem("Domingo", "7"));

        }
        private void llenar_Meses()
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
                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "-1"));
            }
            else
            {
                dtm = null;

            }

        }

        private void llenameses_2()
        {
            //UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA
            try
            {
                DataTable dtm = Get_Administrativo.Get_ObtenerMeses();

                if (dtm.Rows.Count > 0)
                {
                    ddl_mes.Enabled = true;
                    ddl_mes.DataSource = dtm;
                    ddl_mes.DataTextField = "namemes";
                    ddl_mes.DataValueField = "codmes";
                    ddl_mes.DataBind();

                    //cmb_pventa.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        private void llenar_puntoVenta()
        {

            DataTable dtp = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();
            dtp = oCoon.ejecutarDataTable("WEB_XPLORA_CLIEV2_LLENAPTOVENTACOMPANY", icompany);
            if (dtp.Rows.Count > 0)
            {
                cmb_PtoVenta.DataSource = dtp;
                cmb_PtoVenta.DataValueField = "id_PointOfsale";
                cmb_PtoVenta.DataTextField = "pdv_Name";
                cmb_PtoVenta.DataBind();

                cmb_PtoVenta.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));


            }
            else
            {

                dtp = null;

            }
        }

        private void llenar_Periodos()
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

                cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));


            }
            else
            {

                dtp = null;
                cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            }
        }

        private void llenarSubcategoria()
        {


            DataTable dt = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            icompany = Convert.ToInt32(this.Session["companyid"].ToString());
            canal = this.Session["Canal"].ToString();
            dt = oCoon.ejecutarDataTable("WEB_XPLORA_CLIEV2_LLENASUBCATECOMPANY", icompany);

            if (dt.Rows.Count > 0)
            {
                cmb_SubCategoria.DataSource = dt;
                cmb_SubCategoria.DataValueField = "id_Subcategory";
                cmb_SubCategoria.DataTextField = "Name_Subcategory";
                cmb_SubCategoria.DataBind();
                cmb_SubCategoria.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
                cmb_SubCategoria.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "-1"));
            }
        }


        protected void llenarCategoria()
        {
            DataTable dt = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            icompany = Convert.ToInt32(this.Session["companyid"].ToString());
            canal = this.Session["Canal"].ToString();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERCATEGORIA", icompany, canal, Report);

            if (dt.Rows.Count > 0)
            {
                cmb_categoria.DataSource = dt;
                cmb_categoria.DataValueField = "id_ProductCategory";
                cmb_categoria.DataTextField = "Product_Category";
                cmb_categoria.DataBind();
                cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
                cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "-1"));
            }

        }

        protected void llenarProducto()
        {
            DataTable dt = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            icompany = Convert.ToInt32(this.Session["companyid"].ToString());
            canal = this.Session["Canal"].ToString();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPRODUCTOS_Clientes", icompany);

            if (dt.Rows.Count > 0)
            {
                cmbProducto.DataSource = dt;
                cmbProducto.DataValueField = "id_Product";
                cmbProducto.DataTextField = "Product_Name";
                cmbProducto.DataBind();
                cmbProducto.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
            }

        }



        private void ObtenerInformeStock_VentasXDia()
        {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);



            try
            {


                EvoluciondeVentasporSemana.Visible = true;
                EvoluciondeVentasporSemana.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                EvoluciondeVentasporSemana.ServerReport.ReportPath = "/Reporte_Precios_V1/Masisa_Reporte_Stock_VentasXSemana2";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                EvoluciondeVentasporSemana.ServerReport.ReportServerUrl = new Uri(strConnection);
                EvoluciondeVentasporSemana.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("companyid", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("codstrategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Codchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", cmb_año.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", cmb_mes.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", cmb_periodo.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("dia", cmb_Dia.SelectedValue));


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ptoventa", Convert.ToString(Convert.ToInt32(cmb_PtoVenta.SelectedValue))));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", cmb_categoria.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SubCategoria", cmb_SubCategoria.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Producto", cmbProducto.SelectedValue));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("DIA", sdias));







                EvoluciondeVentasporSemana.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


            }
        }


        private void EvoluciondeVentasSemanalesXLinea()
        {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);



            try
            {


                EvoluciondeVentasSemanalesporLinea.Visible = true;
                EvoluciondeVentasSemanalesporLinea.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                EvoluciondeVentasSemanalesporLinea.ServerReport.ReportPath = "/Reporte_Precios_V1/Masisa_Reporte_Stock_Ventas_XLineasXSemana";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                EvoluciondeVentasSemanalesporLinea.ServerReport.ReportServerUrl = new Uri(strConnection);
                EvoluciondeVentasSemanalesporLinea.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("companyid", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("codstrategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Codchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", cmb_año.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", cmb_mes.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", cmb_periodo.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("dia", cmb_Dia.SelectedValue));


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ptoventa", Convert.ToString(Convert.ToInt32(cmb_PtoVenta.SelectedValue))));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", cmb_categoria.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SubCategoria", cmb_SubCategoria.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Producto", cmbProducto.SelectedValue));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("DIA", sdias));







                EvoluciondeVentasSemanalesporLinea.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


            }
        }


        private void ObtenerInformeStock_VentasXSemana()
        {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);


            try
            {


                EvoluciondeVentasMesualporLinea.Visible = true;
                EvoluciondeVentasMesualporLinea.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                EvoluciondeVentasMesualporLinea.ServerReport.ReportPath = "/Reporte_Precios_V1/Masisa_Reporte_Stock_Ventas_XLineasXMes";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                EvoluciondeVentasMesualporLinea.ServerReport.ReportServerUrl = new Uri(strConnection);
                EvoluciondeVentasMesualporLinea.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("companyid", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("codstrategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Codchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", cmb_año.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", cmb_mes.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", cmb_periodo.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("dia", cmb_Dia.SelectedValue));


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ptoventa", Convert.ToString(Convert.ToInt32(cmb_PtoVenta.SelectedValue))));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", cmb_categoria.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SubCategoria", cmb_SubCategoria.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Producto", cmbProducto.SelectedValue));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("DIA", sdias));


                EvoluciondeVentasMesualporLinea.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


            }
        }


        private void ObtenerInformePrecio()
        {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);



            try
            {


                EvolucionMensualdeVentas.Visible = true;
                EvolucionMensualdeVentas.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                EvolucionMensualdeVentas.ServerReport.ReportPath = "/Reporte_Precios_V1/Masisa_Reporte_Stock_VentasXMes2";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                EvolucionMensualdeVentas.ServerReport.ReportServerUrl = new Uri(strConnection);
                EvolucionMensualdeVentas.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("companyid", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("codstrategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Codchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", cmb_año.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", cmb_mes.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", cmb_periodo.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("dia", cmb_Dia.SelectedValue));


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ptoventa", Convert.ToString(Convert.ToInt32(cmb_PtoVenta.SelectedValue))));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", cmb_categoria.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SubCategoria", cmb_SubCategoria.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Producto", cmbProducto.SelectedValue));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("DIA", sdias));







                EvolucionMensualdeVentas.ServerReport.SetParameters(parametros);
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

        string inicial = "";

        //protected void btn_ocultar_Click(object sender, EventArgs e)
        //{
        //    if (Div_filtros.Visible == false)
        //    {

        //        Div_filtros.Visible = true;

        //        btn_ocultar.Text = "Ocultar";
        //        btngnerar.Visible = true;
        //    }
        //    else if (Div_filtros.Visible == true)
        //    {
        //        Div_filtros.Visible = false;
        //        btn_ocultar.Text = "Filtros";
        //        btngnerar.Visible = false;
        //    }

        //    inicial = "1";

        //}

        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            cmb_periodo.Items.Clear();
            llenar_Periodos();

        }
        protected void btn_imb_tab_Click(object sender, ImageClickEventArgs e)
        {
            
            TabContainer_filtros.ActiveTabIndex = 0;
            UpReportStock.Update();
           
        }

        protected void btngnerar_Click(object sender, EventArgs e)
        {
            _AsignarVariables();
            ObtenerInformePrecio();
            ObtenerInformeStock_VentasXSemana();
            ObtenerInformeStock_VentasXDia();
            EvoluciondeVentasSemanalesXLinea();

        }
        private void _AsignarVariables()
        {
            sidaño = cmb_año.SelectedValue;
            sidmes = cmb_mes.SelectedValue;
            sidperiodo = cmb_periodo.SelectedValue;

            sidcategoria = cmb_categoria.SelectedValue;
            //sdias = cmb_Dias.SelectedValue;



            string sidperdil = this.Session["Perfilid"].ToString();
            if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0" && cmb_periodo.SelectedValue == "0")
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    //aca debe ir la carga inical para el analista

                    //aca debe ir la carga inical para el analista
                    icompany = Convert.ToInt32(this.Session["companyid"]);
                    iservicio = Convert.ToInt32(this.Session["Service"]);
                    canal = this.Session["Canal"].ToString().Trim();
                    Report = Convert.ToInt32(this.Session["Reporte"]);
                    //Periodo p = new Periodo(Report, icadena, sidcategoria, canal, icompany, iservicio);

                    //p.Set_PeriodoConDataValidada();

                    //sidaño = p.Año;
                    //sidmes = p.Mes;
                    //sidperiodo = p.PeriodoId;

                    // GetPeridForAnalist();

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
                        chkb_validar.Items[0].Selected = valid_analist;
                    else
                        chkb_validar.Items[1].Selected = true;

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

        protected void cmbFamilia_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenarCategoria();
        }

        #region Logica de la Validación del Reporte

        protected void chkb_validar_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkb_validar.Items[0].Selected == true)
            {
                chkb_validar.Items[1].Selected = false;
            }
            else
            {
                chkb_validar.Items[0].Selected = false;
            }
            lbl_msj_validar.Text = "¿ Esta seguro que desea continuar?";
            btn_aceptar2.Visible = false;
            btn_aceptar.Visible = true;
            btn_cancelar.Visible = true;

            ModalPopupExtender_ValidationAnalyst.Show();

            UpdatePanel_validacion.Update();
        }
      
        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            btn_aceptar2.Visible = true;
            btn_aceptar.Visible = false;
            btn_cancelar.Visible = false;
            try
            {
                Report = Convert.ToInt32(this.Session["Reporte"]);
                oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_REPORT_PLANNING_ACTUALIZAR_VALIDACION", Report, lbl_año_value.Text.Trim(), lbl_mes_value.Text.Trim(), lbl_periodo_value.Text.Trim(), chkb_validar.Items[0].Selected, Session["sUser"].ToString(), DateTime.Now);

                ModalPopupExtender_ValidationAnalyst.Show();
                lbl_msj_validar.Text = "El cambio se realizo con exito.";
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            UpdatePanel_validacion.Update();

        }
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {

            chkb_validar.Items[0].Selected = false;
            chkb_validar.Items[1].Selected = false;

            UpdatePanel_validacion.Update();
        }
        #endregion



        #region mis favoritos 

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

           oReportes_parametros.Id_punto_venta = cmb_PtoVenta.SelectedValue;
            oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
            oReportes_parametros.Id_subCategoria = cmb_SubCategoria.SelectedValue;
            oReportes_parametros.SkuProducto = cmbProducto.SelectedValue;
          
            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            oReportes_parametros.Id_periodo = Convert.ToInt32(cmb_periodo.SelectedValue);
            oReportes_parametros.id_dia = cmb_Dia.SelectedValue;


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
            TabContainer_filtros.ActiveTabIndex = 0;
            up_favoritos.Update();
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

      
            oReportes_parametros.Id_punto_venta = cmb_PtoVenta.SelectedValue;
            oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
            oReportes_parametros.Id_subCategoria = cmb_SubCategoria.SelectedValue;
            oReportes_parametros.SkuProducto = cmbProducto.SelectedValue;
          
            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            oReportes_parametros.Id_periodo = Convert.ToInt32(cmb_periodo.SelectedValue);
            oReportes_parametros.id_dia = cmb_Dia.SelectedValue;

           

            oReportes_parametros.Descripcion = txt_descripcion_parametros.Text.Trim();

            Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

            if (!System.IO.File.Exists(path))
                oReportes_parametrosToXml.createXml(oReportes_parametros, path);
            else
                oReportes_parametrosToXml.addToXml(oReportes_parametros, path);

            
            cargarParametrosdeXml();
            txt_descripcion_parametros.Text = "";
            TabContainer_filtros.ActiveTabIndex = 0;
            up_favoritos.Update();
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

            if (e.CommandName == "BUSCAR")
            {
                string sidperdil = this.Session["Perfilid"].ToString();

                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_periodo = (Label)e.Item.FindControl("lbl_id_periodo");
                Label lbl_id_dia = (Label)e.Item.FindControl("lbl_id_dia");
                Label lbl_id_pdv = (Label)e.Item.FindControl("lbl_id_pdv");
                Label lbl_id_categoria = (Label)e.Item.FindControl("lbl_id_categoria");
                Label lbl_id_subcategoria = (Label)e.Item.FindControl("lbl_id_subcategoria");
                Label lbl_id_producto = (Label)e.Item.FindControl("lbl_id_producto");



                cmb_año.SelectedValue = lbl_id_año.Text.Trim();
                cmb_mes.SelectedValue = lbl_id_mes.Text.Trim();
                cmb_periodo.SelectedValue = lbl_id_periodo.Text.Trim();
                cmb_Dia.SelectedValue = lbl_id_dia.Text;
                llenar_puntoVenta();
                cmb_PtoVenta.SelectedValue = lbl_id_pdv.Text;
                llenarCategoria();
                cmb_categoria.SelectedValue = lbl_id_categoria.Text;
                llenarSubcategoria();
                cmb_SubCategoria.SelectedValue = lbl_id_subcategoria.Text;
                llenarProducto();
                cmbProducto.SelectedValue = lbl_id_producto.Text;

                TabContainer_filtros.ActiveTabIndex = 0;


                cargarParametrosdeXml();
                ObtenerInformePrecio();
                ObtenerInformeStock_VentasXSemana();
                ObtenerInformeStock_VentasXDia();
                EvoluciondeVentasSemanalesXLinea();

                
                UpReportStock.Update();
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
                Session["idxml"] = lbl_id.Text;

                string a = Session["idxml"].ToString();
                string sidperdil = this.Session["Perfilid"].ToString();

                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_periodo = (Label)e.Item.FindControl("lbl_id_periodo");
                Label lbl_id_dia = (Label)e.Item.FindControl("lbl_id_dia");
                Label lbl_id_pdv = (Label)e.Item.FindControl("lbl_id_pdv");
                Label lbl_id_categoria = (Label)e.Item.FindControl("lbl_id_categoria");
                Label lbl_id_subcategoria = (Label)e.Item.FindControl("lbl_id_subcategoria");
                Label lbl_id_producto = (Label)e.Item.FindControl("lbl_id_producto");



                cmb_año.SelectedValue = lbl_id_año.Text.Trim();
                cmb_mes.SelectedValue = lbl_id_mes.Text.Trim();
                cmb_periodo.SelectedValue = lbl_id_periodo.Text.Trim();
                cmb_Dia.SelectedValue = lbl_id_dia.Text;
                llenar_puntoVenta();
                cmb_PtoVenta.SelectedValue = lbl_id_pdv.Text;
                llenarCategoria();
                cmb_categoria.SelectedValue = lbl_id_categoria.Text;
                llenarSubcategoria();
                cmb_SubCategoria.SelectedValue = lbl_id_subcategoria.Text;
                llenarProducto();
                cmbProducto.SelectedValue = lbl_id_producto.Text;

                TabContainer_filtros.ActiveTabIndex = 0;


                TabContainer_filtros.ActiveTabIndex = 0;
                lbl_updateconsulta.Visible = true;
                btn_img_actualizar.Visible = true;

                lbl_saveconsulta.Visible = false;
                btn_img_add.Visible = false;

                UpReportStock.Update();
            }
        }

        #endregion

    }
}