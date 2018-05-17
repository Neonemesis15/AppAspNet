using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
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

namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_V2_EfectividadMIN : System.Web.UI.Page
    {
        Conexion oCoon = new Conexion();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();

        string sUser;
        string sPassw;
        string sNameUser;
        int icompany;
        string canal;
        int Report;
        int iservicio;
        ReportViewer ReportEfectividad;
        ReportViewer ReportEvolucionClientes;

        private string sidaño;
        private string sidmes;
        private string sidperiodo;
        private string sidciudad;
        private string sidcategoria;
        private string sidsub_categoria;
        private string sidmarca;
        private string sidsub_marca;
        private string sidsku;
        private string sidpuntoventa;
        private int ValidAnalyst;

        private void UpdateProgressContext2()
        {
            const int total = 100;

            RadProgressContext progress = RadProgressContext.Current;

            for (int i = 0; i < total; i++)
            {

                progress.PrimaryTotal = 1;
                progress.PrimaryValue = 1;
                progress.PrimaryPercent = 100;

                progress.SecondaryTotal = total;
                progress.SecondaryValue = i;
                progress.SecondaryPercent = i;

                progress.CurrentOperationText = "Efectividad Canal AASS";

                if (!Response.IsClientConnected)
                {
                    //Cancel button was clicked or the browser was closed, so stop processing
                    break;
                }
                progress.Speed = i;
                //Stall the current thread for 0.1 seconds
                System.Threading.Thread.Sleep(100);


            }
        }

        private void _AsignarVariables()
        {
            sidaño = cmb_año.SelectedValue;
            sidmes = cmb_mes.SelectedValue;

            string sidperdil = this.Session["Perfilid"].ToString();

            //sidciudad = cmb_ciudad.SelectedValue;
            //sidcategoria = cmb_ditribuidor.SelectedValue;
            //sidmarca = cmb_marca.SelectedValue;
            //sidsku = cmb_skuProducto.SelectedValue;
            //sidpuntoventa = cmb_punto_de_venta.SelectedValue;

            if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0")
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    //aca debe ir la carga inical para el analista
                    icompany = Convert.ToInt32(this.Session["companyid"]);
                    iservicio = Convert.ToInt32(this.Session["Service"]);
                    canal = this.Session["Canal"].ToString().Trim();
                    Report = Convert.ToInt32(this.Session["Reporte"]);
                    Periodo p = new Periodo(1, Report, sidciudad, sidcategoria, sidmarca, sidpuntoventa, canal, icompany, iservicio);

                    //p.Set_PeriodoConDataValidada();

                    sidaño = DateTime.Now.Year.ToString();
                    sidmes = "0";// p.Mes;
                    //sidperiodo ="2";
                    //GetPeridForAnalist();
                    ValidAnalyst = 0;
                }
                else if (sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteGeneral"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado1"])
                {
                    //GetPeriodForClient();
                    ValidAnalyst = 1;
                }
            }
            else
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    //GetPeridForAnalist();
                    ValidAnalyst = 0;
                }
            }
        }

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

        private void Distribuidora()
        {
            DataTable dt_dex = null;
            dt_dex = oCoon.ejecutarDataTable("UP_WEB_LLENACOMBOS", 62);
            if (dt_dex.Rows.Count > 0)
            {

                cmb_ditribuidor.DataSource = dt_dex;
                cmb_ditribuidor.DataValueField = "Id_Dex";
                cmb_ditribuidor.DataTextField = "Dex_Name";
                cmb_ditribuidor.DataBind();
                cmb_ditribuidor.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));

            }
            else
            {
                dt_dex = null;

            }
        }
        protected void LlenarTipoZona()
        {
            DataTable dt_Tnoza = null;
            dt_Tnoza = oCoon.ejecutarDataTable("UP_WEB_LLENACOMBOS", 11);
            if (dt_Tnoza.Rows.Count > 0)
            {

                cmb_tipoCadena.DataSource = dt_Tnoza;
                cmb_tipoCadena.DataValueField = "idNodeComType";
                cmb_tipoCadena.DataTextField = "NodeComType_name";
                cmb_tipoCadena.DataBind();
                cmb_tipoCadena.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                cmb_tipoCadena.Items.Remove(1);
            }
            else
            {
                dt_Tnoza = null;

            }
        }
        protected void LlenarTipoNoVisita()
        {
            DataTable dt_Tnoza = null;
            dt_Tnoza = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_TIPONOVISITA",icompany);
            if (dt_Tnoza.Rows.Count > 0)
            {

                cmb_noVisita.DataSource = dt_Tnoza;
                cmb_noVisita.DataValueField = "ID_TNOV";
                cmb_noVisita.DataTextField = "TNOV_DESCIPCION";
                cmb_noVisita.DataBind();
                cmb_noVisita.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            }
            else
            {
                dt_Tnoza = null;

            }
        }



        #endregion

        #region Llenado de Informes

        private void llenarreporteComparativoEfectividad()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);

            try
            {

                ReportEfectividad = (ReportViewer)(Reporte_v2_ComparativoEfectividad1.FindControl("ComparativoEfectividad"));
                ReportEfectividad.Visible = true;
                ReportEfectividad.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                ReportEfectividad.ServerReport.ReportPath = "/Reporte_Precios_V1/ReporteEfectividad_Efectividad";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                ReportEfectividad.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReportEfectividad.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("DEX", cmb_ditribuidor.SelectedValue));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("TIPOCADENA", cmb_tipoCadena.SelectedValue));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("TipoNoVisita", cmb_noVisita.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));




                ReportEfectividad.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


            }

        }

        private void llenarreporteEvolucionClientes()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);

            try
            {


                ReportEvolucionClientes = (ReportViewer)(Reporte_v2_EfectividadEvolucionClientes1.FindControl("EvlucionClientes"));
                ReportEvolucionClientes.Visible = true;
                ReportEvolucionClientes.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                ReportEvolucionClientes.ServerReport.ReportPath = "/Reporte_Precios_V1/ReporteEfectividad_EvolucionCliente";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                ReportEvolucionClientes.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReportEvolucionClientes.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
    
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("DEX", cmb_ditribuidor.SelectedValue));
           
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("TIPOCADENA", cmb_tipoCadena.SelectedValue));
            
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("TipoNoVisita",cmb_noVisita.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));

                ReportEvolucionClientes.ServerReport.SetParameters(parametros);
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


        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session["Año"] = "0";
            this.Session["Mes"] = "0";

            sUser = this.Session["sUser"].ToString();
            sPassw = this.Session["sPassw"].ToString();
            sNameUser = this.Session["nameuser"].ToString();
            icompany = Convert.ToInt32(this.Session["companyid"]);
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();
            UpdateProgressContext2();
            if (!IsPostBack)
            {
                try
                {


                    Años();

                    Llena_Meses();
                    Distribuidora();
                    LlenarTipoZona();
                    LlenarTipoNoVisita();
                    _AsignarVariables();
                    llenarreporteComparativoEfectividad();
                    llenarreporteEvolucionClientes();

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
        /*
        protected void btn_ocultar_Click(object sender, EventArgs e)
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
            }
        }
        */
        protected void btngnerar_Click(object sender, EventArgs e)
        {
            _AsignarVariables();
            llenarreporteComparativoEfectividad();
            llenarreporteEvolucionClientes();


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

            //oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_ciudad.SelectedValue);
            //oReportes_parametros.Id_punto_venta = cmb_punto_de_venta.SelectedValue;
            oReportes_parametros.Id_producto_categoria = cmb_ditribuidor.SelectedValue;
            //oReportes_parametros.Id_subCategoria = cmb_subCategoria.SelectedValue;
     
            oReportes_parametros.Id_producto_marca = sidmarca;
            //oReportes_parametros.Id_subMarca = cmb_subMarca.SelectedValue;
            //oReportes_parametros.SkuProducto = cmb_skuProducto.SelectedValue;
            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;


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
                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_oficina = (Label)e.Item.FindControl("lbl_id_oficina");
                Label lbl_id_pdv = (Label)e.Item.FindControl("lbl_id_pdv");
                Label lbl_id_categoria = (Label)e.Item.FindControl("lbl_id_categoria");
                Label lbl_id_subCategoria = (Label)e.Item.FindControl("lbl_id_subCategoria");
                Label lbl_id_marca = (Label)e.Item.FindControl("lbl_id_marca");
                Label lbl_id_subMarca = (Label)e.Item.FindControl("lbl_id_subMarca");
                Label lbl_skuProducto = (Label)e.Item.FindControl("lbl_skuProducto");

                sidaño = lbl_id_año.Text.Trim();
                sidmes = lbl_id_mes.Text.Trim();
                sidciudad = lbl_id_oficina.Text.Trim();
                sidcategoria = lbl_id_categoria.Text.Trim();
                sidsub_categoria = lbl_id_subCategoria.Text.Trim();
                sidmarca = lbl_id_marca.Text.Trim();
                sidsub_marca = lbl_id_subMarca.Text.Trim();
                sidpuntoventa = lbl_id_pdv.Text.Trim();

                string sidperdil = this.Session["Perfilid"].ToString();
                if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0")
                {
                    if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                    {
                        GetPeridForAnalist();
                        ValidAnalyst = 0;
                    }
                    else if (sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteGeneral"])
                    {
                        GetPeriodForClient();
                        ValidAnalyst = 1;
                    }
                }
                else
                {
                    if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                    {
                        GetPeridForAnalist();
                        ValidAnalyst = 0;
                    }
                }

                llenarreporteComparativoEfectividad();
                llenarreporteEvolucionClientes();
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

                Session["idxml"] = lbl_id.Text.Trim();
                cmb_año.SelectedIndex = cmb_año.Items.FindItemByValue(lbl_id_año.Text).Index;
                cmb_mes.SelectedIndex = cmb_mes.Items.FindItemByValue(lbl_id_mes.Text).Index;

                //cmb_ciudad.SelectedIndex = cmb_ciudad.Items.FindItemByValue(lbl_id_oficina.Text).Index;
                
                //cmb_punto_de_venta.SelectedIndex = cmb_punto_de_venta.Items.FindItemByValue(lbl_id_pdv.Text).Index;
                //cmb_categoria.SelectedIndex = cmb_categoria.Items.FindItemByValue(lbl_id_categoria.Text).Index;
                //Subcategorias();
                //cmb_subCategoria.SelectedIndex = cmb_subCategoria.Items.FindItemByValue(lbl_id_subCategoria.Text).Index;
                
                //cmb_marca.SelectedIndex = cmb_marca.Items.FindItemByValue(lbl_id_marca.Text).Index;
                //cmb_subMarca.SelectedIndex = cmb_subMarca.Items.FindItemByValue(lbl_id_subMarca.Text).Index;

                TabContainer_filtros.ActiveTabIndex = 0;
                lbl_updateconsulta.Visible = true;
                btn_img_actualizar.Visible = true;

                lbl_saveconsulta.Visible = false;
                btn_img_add.Visible = false;
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

            //oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_ciudad.SelectedValue);
            //oReportes_parametros.Id_punto_venta = cmb_punto_de_venta.SelectedValue;
            //oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
            //oReportes_parametros.Id_subCategoria = cmb_subCategoria.SelectedValue;
            
            oReportes_parametros.Id_producto_marca = sidmarca;
            //oReportes_parametros.Id_subMarca = cmb_subMarca.SelectedValue;
            //oReportes_parametros.SkuProducto = cmb_skuProducto.SelectedValue;
            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;

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
        }
        protected void cmb_Negocio_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }
        #region Logica de la Validación del Reporte
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
        protected void GetPeridForAnalist()
        {//se obtiene el estado de un Reporte en un Año, mes y periodo especifico.Y otros datos adicionales del periodo obtenido
            lbl_msj_allMonth.Visible = false;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            DataTable dt = null;

            if (sidmes != "0")
            {
                dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO", canal, Report, sidaño, sidmes, 1);


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


                        chkb_validar.Checked = false;//inicializamos los checkbox en false
                        chkb_invalidar.Checked = false;
                        if (valid_analist)
                            chkb_validar.Checked = valid_analist;
                        else
                            chkb_invalidar.Checked = true;

                        lbl_validacion.Text = sidaño + "-" + dt.Rows[0]["Month_name"].ToString();

                    }
                }
            }
            else
            {
                div_Validar.Visible = false;
                lbl_msj_allMonth.Text = "Para porder hacer validaciones filtre por almenos un mes.";
                lbl_msj_allMonth.Visible = true;
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
    }
}