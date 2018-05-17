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
    public partial class Reporte_v2_Stock_Ventas_Masisa : System.Web.UI.Page
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

            MyAccordion.SelectedIndex = 1;
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
                    llenar_Dia();
                    llenar_puntoVenta();

                    
                    llenarCategoria();
                    llenarSubcategoria();

                    llenarProducto();

                    _AsignarVariables();
                    ObtenerInformePrecio();
                    ObtenerInformeStock_VentasXSemana();
                    ObtenerInformeStock_VentasXDia();
                    ObtenerInformeStock_SplitVentas();




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
                cmb_categoria.DataValueField = "Product_Category";
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

        

       private void ObtenerInformeStock_SplitVentas()
        {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);



            try
            {


                ReporSplitVentas.Visible = true;
                ReporSplitVentas.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                ReporSplitVentas.ServerReport.ReportPath = "/Reporte_Precios_V1/Masisa_Reporte_Split_de_Ventas";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                ReporSplitVentas.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReporSplitVentas.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
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







                ReporSplitVentas.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


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


                ReporStock_VentasXDia.Visible = true;
                ReporStock_VentasXDia.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                ReporStock_VentasXDia.ServerReport.ReportPath = "/Reporte_Precios_V1/Masisa_Reporte_Stock_VentasXDia";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                ReporStock_VentasXDia.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReporStock_VentasXDia.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
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







                ReporStock_VentasXDia.ServerReport.SetParameters(parametros);
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


                ReporStock_VentasXSemana.Visible = true;
                ReporStock_VentasXSemana.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                ReporStock_VentasXSemana.ServerReport.ReportPath = "/Reporte_Precios_V1/Masisa_Reporte_Stock_VentasXSemana";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                ReporStock_VentasXSemana.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReporStock_VentasXSemana.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
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







                ReporStock_VentasXSemana.ServerReport.SetParameters(parametros);
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


                ReporPrecio.Visible = true;
                ReporPrecio.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                ReporPrecio.ServerReport.ReportPath = "/Reporte_Precios_V1/Masisa_Reporte_Stock_VentasXMes";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                ReporPrecio.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReporPrecio.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
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







                ReporPrecio.ServerReport.SetParameters(parametros);
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

        string inicial="";

        /*protected void btn_ocultar_Click(object sender, EventArgs e)
        {
            if (Div_filtros.Visible == false)
            {

                Div_filtros.Visible = true;

                btn_ocultar.Text = "Ocultar";
                btngnerar.Visible = true;
            }
            else if (Div_filtros.Visible == true)
            {
                Div_filtros.Visible = false;
                btn_ocultar.Text = "Filtros";
                btngnerar.Visible = false;
            }

            inicial = "1";

        }*/

        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            cmb_periodo.Items.Clear();
            llenar_Periodos();

        }
        protected void btn_imb_tab_Click(object sender, ImageClickEventArgs e)
        {
            TabContainer_filtros.ActiveTabIndex = 0;
        }

        protected void btngnerar_Click(object sender, EventArgs e)
        {
            _AsignarVariables();
            ObtenerInformePrecio();
            ObtenerInformeStock_VentasXSemana();
            ObtenerInformeStock_VentasXDia();
            ObtenerInformeStock_SplitVentas();

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
                   // GetPeridForAnalist();
                }
            }
        }
        //protected void GetPeridForAnalist()
        //{//se obtiene el estado de un Reporte en un Año, mes y periodo especifico.Y otros datos adicionales del periodo obtenido

        //    Report = Convert.ToInt32(this.Session["Reporte"]);
        //    DataTable dt = null;
        //    dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO", canal, Report, sidaño, sidmes, sidperiodo);

        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count == 1)
        //        {
        //            div_Validar.Visible = true;
        //            sidaño = dt.Rows[0]["id_Year"].ToString();
        //            sidmes = dt.Rows[0]["id_Month"].ToString();
        //            sidperiodo = dt.Rows[0]["periodo"].ToString();
        //            bool valid_analist = Convert.ToBoolean(dt.Rows[0]["ReportsPlanning_ValidacionAnalista"]);

        //            lbl_año_value.Text = sidaño;
        //            lbl_mes_value.Text = sidmes;
        //            lbl_periodo_value.Text = sidperiodo;

                    
        //            if (valid_analist)
        //                chkb_validar.Checked = valid_analist;
        //            else
        //                chkb_invalidar.Checked = true;

        //            lbl_validacion.Text = sidaño + "-" + dt.Rows[0]["Month_name"].ToString() + " " + sidperiodo;

        //        }
        //    }
        //}
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
    }
}