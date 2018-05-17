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


namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_v2_SOD : System.Web.UI.Page
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
        string sidmallas;
        ReportViewer rpesod;
        ReportViewer rpesodxpdv;
        ReportViewer rpcuobje;

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
      
        private void _AsignarVariables()
        {
            sidaño = cmb_año.SelectedValue;
            sidmes = cmb_mes.SelectedValue;
   
            string sidperdil = this.Session["Perfilid"].ToString();

            sidciudad = cmb_ciudad.SelectedValue;
            sidcategoria = cmb_categoria.SelectedValue;
            sidmarca = cmb_marca.SelectedValue;
            sidsku = cmb_skuProducto.SelectedValue;
            sidpuntoventa = cmb_punto_de_venta.SelectedValue;
            sidmallas = cmb_region.SelectedValue;

            if (sidcategoria == "0")
            {
                sidcategoria = "12";
                cmb_categoria.SelectedIndex = cmb_categoria.Items.FindItemIndexByValue(sidcategoria);
            }
            if (sidmallas == "0")
            {
                sidmallas = "2"; //Se Cambia esta asignacion pues tenia por defecto 2 Ing. Carlos Hernandez Rincon 04/07/2011
                cmb_region.SelectedIndex = cmb_region.Items.FindItemIndexByValue(sidmallas);
            }
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

                    p.SetPeriodoInicial_SOD();

                    sidaño = p.Año;
                    sidmes = p.Mes;
                    //sidperiodo ="2";
                    GetPeridForAnalist();
                    ValidAnalyst = 0;

                    cmb_año.SelectedIndex = cmb_año.Items.FindItemIndexByValue(sidaño);
                    cmb_mes.SelectedIndex = cmb_mes.Items.FindItemIndexByValue(sidmes);
                }
                else if (sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteGeneral"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado1"]) 
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
                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            }
            else
            {
                dtm = null;

            }

        }

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

                progress.CurrentOperationText = "SOD Canal Mayorista";

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

        //private void ObtenerNegocio() {

        //    DataTable dtn = null;
        //    dtn = oCoon.ejecutarDataTable("UP_WEBXPLORA_SOD_OBTENER_NEGOCIO");

        //    if (dtn.Rows.Count > 0) {

        //        cmb_Negocio.DataSource = dtn;
        //        cmb_Negocio.DataValueField = "Company_id";
        //        cmb_Negocio.DataTextField = "Company_Name";
        //        cmb_Negocio.DataBind();
        //        cmb_Negocio.Items.Insert(0, new ListItem("--Todos--", "0"));
            
            
        //    }
        
        
        
        //}

        private void Cobertura()
        {
            DataTable dtc = null;
            icompany = Convert.ToInt32(this.Session["companyid"]);
            canal = this.Session["Canal"].ToString().Trim();
            dtc = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_OFICINAS_BY_CANAL_COMPANY", icompany, canal);
            if (dtc.Rows.Count > 0)
            {
                cmb_ciudad.DataSource = dtc;
                cmb_ciudad.DataValueField = "cod_Oficina";
                cmb_ciudad.DataTextField = "Name_Oficina";
                cmb_ciudad.DataBind();
                cmb_ciudad.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
            }
            else
            {
                dtc = null;
            }
        }
        private void CoberturaPorRegion()
        {
            DataTable dtc = null;
            cmb_ciudad.Items.Clear();
            dtc = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_OFICINAS_BY_MALLA_CANAL_COMPANY", icompany, canal,cmb_region.SelectedValue);
            if (dtc.Rows.Count > 0)
            {
                cmb_ciudad.DataSource = dtc;
                cmb_ciudad.DataValueField = "cod_Oficina";
                cmb_ciudad.DataTextField = "Name_Oficina";
                cmb_ciudad.DataBind();
                cmb_ciudad.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
            }
            else
            {
                dtc = null;
            }
        }

        private void Categorias()
        {
            DataTable dtcatego = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            dtcatego = Get_DataClientes.Get_Obtenerinfocombos_F(icompany, canal,Report, "0", 2);
            if (dtcatego.Rows.Count > 0)
            {

                cmb_categoria.DataSource = dtcatego;
                cmb_categoria.DataValueField = "cod_catego";
                cmb_categoria.DataTextField = "Name_Catego";
                cmb_categoria.DataBind();
                cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));

            }
            else
            {
                dtcatego = null;
            }
        }

        //private void Subcategorias()
        //{
        //    DataTable dtscat = null;

        //    dtscat = Get_DataClientes.Get_Obtenerinfocombos(0, "", cmb_categoria.SelectedValue, 3);

        //    if (dtscat.Rows.Count > 0)
        //    {
    
        //        cmb_subCategoria.DataSource = dtscat;
        //        cmb_subCategoria.DataValueField = "cod_subcatego";
        //        cmb_subCategoria.DataTextField = "Name_Subcatego";
        //        cmb_subCategoria.DataBind();
        //        cmb_subCategoria.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
        //    }
        //    else
        //    {
        //        dtscat = null;
        //        cmb_subCategoria.Enabled = false;
        //    }
        //}

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
                cmb_marca.Enabled = true;

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

        protected void Cargar_Region()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            canal = this.Session["Canal"].ToString().Trim();

            DataTable dtr = oCoon.ejecutarDataTable("UP_WEBXPLORA_OBTENER_MALLA_BY_CHANNEL_AND_CLIENTE", canal, icompany);
            if (dtr.Rows.Count > 0)
            {
                cmb_region.DataSource = dtr;
                cmb_region.DataValueField = "id_malla";
                cmb_region.DataTextField = "malla";
                cmb_region.DataBind();
                cmb_region.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
            }
            else
            {
                cmb_region.Items.Insert(0, new RadComboBoxItem("--No hay datos--", "0"));
            }
        }
        //private void Submarcas()
        //{
        //    DataTable dtsm = null;
        //    dtsm = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTNERSUBMARCAS", Convert.ToInt32(cmb_marca.SelectedValue));
        //    if (dtsm.Rows.Count > 0)
        //    {

        //        cmb_subMarca.DataSource = dtsm;
        //        cmb_subMarca.DataValueField = "id_SubBrand";
        //        cmb_subMarca.DataTextField = "Name_SubBrand";
        //        cmb_subMarca.DataBind();

        //        cmb_subMarca.DataSource = dtsm;
        //        cmb_subMarca.DataValueField = "id_SubBrand";
        //        cmb_subMarca.DataTextField = "Name_SubBrand";
        //        cmb_subMarca.DataBind();
        //        cmb_subMarca.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));




        //    }
        //    else
        //    {

        //        dtsm = null;
        //        cmb_subMarca.Enabled = false;
        //        //cmb_subMarca.Items.Clear();

        //    }



        //}

        //private void Puntos_Venta()
        //{

        //    DataTable dtpdv = null;
        //    dtpdv = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PDV_PRECIOS", Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), cmb_ciudad.SelectedValue);
        //    if (dtpdv.Rows.Count > 0)
        //    {
        //        cmb_punto_de_venta.DataSource = dtpdv;
        //        cmb_punto_de_venta.DataValueField = "pdv_code";
        //        cmb_punto_de_venta.DataTextField = "pdv_Name";
        //        cmb_punto_de_venta.DataBind();

        //        cmb_punto_de_venta.DataSource = dtpdv;
        //        cmb_punto_de_venta.DataValueField = "pdv_code";
        //        cmb_punto_de_venta.DataTextField = "pdv_Name";
        //        cmb_punto_de_venta.DataBind();
        //        cmb_punto_de_venta.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));




        //    }
        //    else
        //    {
        //        dtpdv = null;
        //    }




        //}

        //private void Productos()
        //{
        //    icompany = Convert.ToInt32(this.Session["companyid"]);
        //    DataTable dtpdt = null;


        //    if (cmb_subCategoria.SelectedValue == "" && cmb_marca.SelectedValue == "" && cmb_subMarca.SelectedValue == "")
        //    {



        //        dtpdt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PRODUCTOS_PRECIOS", icompany, cmb_categoria.SelectedValue, "0", 0, 0);

        //    }
        //    else
        //    {
        //        if (cmb_subMarca.SelectedValue == "")
        //        {



        //            dtpdt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PRODUCTOS_PRECIOS", icompany, cmb_categoria.SelectedValue, cmb_subCategoria.SelectedValue, cmb_marca.SelectedValue, 0);
        //        }
        //        else
        //        {

        //            dtpdt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PRODUCTOS_PRECIOS", icompany, cmb_categoria.SelectedValue, cmb_subCategoria.SelectedValue, cmb_marca.SelectedValue, cmb_subMarca.SelectedValue);




        //        }
        //    }



        //    if (dtpdt.Rows.Count > 0)
        //    {

        //        cmb_skuProducto.DataSource = dtpdt;
        //        cmb_skuProducto.DataValueField = "cod_Product";
        //        cmb_skuProducto.DataTextField = "Name_Product";
        //        cmb_skuProducto.DataBind();
        //        cmb_skuProducto.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));


        //    }
        //    else
        //    {
        //        dtpdt = null;
        //       // cmb_subCategoria.Items.Clear();
        //       // cmb_skuProducto.Items.Clear();



        //    }

        //}


        #endregion

        #region Llenado de Informes

        private void llenarreporteEvolucionSODMay()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);

            try
            {

                rpesod = (ReportViewer)(Reporte_v2_SOD_EvolucionSODMayorista.FindControl("Reportevosod"));
                rpesod.Visible = true;
                //rpesodxpdv.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                rpesod.ServerReport.ReportPath = "/Reporte_Precios_V1/Evolucion_SOD_Mayorista";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpesod.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpesod.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", cmb_periodo.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("OFICINA", sidciudad));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBCATEGO", sidsub_categoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MARCA", sidmarca));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBMARCA",sidsub_marca));
              
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PDV", sidpuntoventa));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("ValidAnalyst", ValidAnalyst.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_mallas", sidmallas));
                
              

                rpesod.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);


            }

        }

        private void llenarreporteEvolucionSODMayxPDV()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);

            try
            {

               
                rpesodxpdv = (ReportViewer)(Reporte_v2_SOD_SODporPDVMayorista.FindControl("Reportevosodxpdv"));
                rpesodxpdv.Visible = true;
                //rpesodxpdv.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;
                rpesodxpdv.ServerReport.ReportPath = "/Reporte_Precios_V1/SOD_Por_PDV_Mayorista";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpesodxpdv.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpesodxpdv.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES",sidmes));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", cmb_periodo.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("OFICINA", sidciudad));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
               // parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBCATEGO", sidsub_categoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MARCA", sidmarca));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBMARCA", sidsub_marca));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PRODUCTOS", cmb_skuProducto.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PDV",sidpuntoventa));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("ValidAnalyst", "0"));



                rpesodxpdv.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);


            }

        }

        private void llenarreporteCumpliIbjetivos()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {


                rpcuobje = (ReportViewer)(Reporte_v2_SOD_CumplimientoDeObjetivosMayorista.FindControl("Reporcuobj"));
                rpcuobje.Visible = true;
               // rpcuobje.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                rpcuobje.ServerReport.ReportPath = "/Reporte_Precios_V1/Objetivos_Marca_vs_SOD_RealizadoV2";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpcuobje.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpcuobje.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("NEGOCIO", "0"));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PDV",sidpuntoventa));




                rpcuobje.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);


            }

        }






        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {

            this.Session["Ciudad"] = "0";
            this.Session["catego"] = "0";
            this.Session["subcatego"] = "0";
            this.Session["pdv"] = "0";
            this.Session["Año"] = "0";
            this.Session["Mes"] = "0";

            sUser = this.Session["sUser"].ToString();
            sPassw = this.Session["sPassw"].ToString();
            sNameUser = this.Session["nameuser"].ToString();
            icompany = Convert.ToInt32(this.Session["companyid"]);
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();
            MyAccordion.SelectedIndex = 1;
            TabContainer_filtros.ActiveTabIndex = 0;
            //UpdateProgressContext2();
            if (!IsPostBack)
            {
                try
                {

                   

                    cmb_año.DataBind();
                    cmb_año.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_mes.DataBind();
                    cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    


                    cmb_categoria.DataBind();
                    cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_subCategoria.DataBind();
                    cmb_subCategoria.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_ciudad.DataBind();
                    cmb_ciudad.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_punto_de_venta.DataBind();
                    cmb_punto_de_venta.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_marca.DataBind();
                    cmb_marca.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_subMarca.DataBind();
                    cmb_subMarca.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_skuProducto.DataBind();
                    cmb_skuProducto.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

                    Años();
                    Llena_Meses();
                    
                    Categorias();
                    Cargar_Region();
                    Cobertura();
                    //Productos();

                    _AsignarVariables();
                    llenarreporteEvolucionSODMay();
                    llenarreporteEvolucionSODMayxPDV();
                    llenarreporteCumpliIbjetivos();

                    cargarParametrosdeXml();

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
        protected void cmb_region_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CoberturaPorRegion();
        }

        protected void cmb_categoria_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //cmb_marca.Items.Clear();
            //cmb_subMarca.Items.Clear();
            //cmb_subCategoria.Items.Clear();
            cmb_subCategoria.Enabled = true;
            //Subcategorias();
            cmb_marca.Enabled = true;
            Marcas();
            //Productos();
            
          
        }

        protected void cmb_subCategoria_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();
            Marcas();
            //Productos();
         
        }

        protected void cmb_marca_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmb_subMarca.Enabled = true;
            //Submarcas();
            //Productos();
           
        }

        protected void cmb_ciudad_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //Puntos_Venta();
           
        }

        protected void cmb_subMarca_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //Productos();
        }

        protected void cmb_punto_de_venta_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
          
        }


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
        //    }
        //}

        protected void btngnerar_Click(object sender, EventArgs e)
        {


                TabContainer_Reporte_Precio.ActiveTabIndex = 1;
                TabContainer_filtros.ActiveTabIndex = 0;
                _AsignarVariables();
                llenarreporteEvolucionSODMay();
                llenarreporteEvolucionSODMayxPDV();
                llenarreporteCumpliIbjetivos();
          

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

            oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_ciudad.SelectedValue);
            oReportes_parametros.Id_punto_venta = cmb_punto_de_venta.SelectedValue;
            oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
            oReportes_parametros.Id_subCategoria = cmb_subCategoria.SelectedValue;
            string sidmarca = cmb_marca.SelectedValue;
            if (sidmarca == "")
                sidmarca = "0";
            oReportes_parametros.Id_producto_marca = sidmarca;
            oReportes_parametros.Id_subMarca = cmb_subMarca.SelectedValue;
            oReportes_parametros.SkuProducto = cmb_skuProducto.SelectedValue;
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
            TabContainer_filtros.ActiveTabIndex = 0;
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
                Label lbl_id_malla = (Label)e.Item.FindControl("lbl_id_malla");

                sidaño = lbl_id_año.Text.Trim();
                sidmes = lbl_id_mes.Text.Trim();
                sidciudad = lbl_id_oficina.Text.Trim();
                sidcategoria = lbl_id_categoria.Text.Trim();
                sidsub_categoria = lbl_id_subCategoria.Text.Trim();
                sidmarca = lbl_id_marca.Text.Trim();
                sidsub_marca = lbl_id_subMarca.Text.Trim();
                sidpuntoventa = lbl_id_pdv.Text.Trim();
                sidmallas = lbl_id_malla.Text.Trim();

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

                cmb_año.SelectedValue = lbl_id_año.Text.Trim();
                cmb_mes.SelectedValue = lbl_id_mes.Text.Trim();
                cmb_categoria.SelectedValue = lbl_id_categoria.Text.Trim();
                cmb_marca.SelectedValue = lbl_id_marca.Text.Trim();
                cmb_region.SelectedValue = lbl_id_malla.Text.Trim();
                cmb_ciudad.SelectedValue = lbl_id_oficina.Text.Trim();
                    //lbl_id_año.Text.Trim()
                TabContainer_Reporte_Precio.ActiveTabIndex = 1;
                llenarreporteEvolucionSODMay();
                llenarreporteEvolucionSODMayxPDV();
                llenarreporteCumpliIbjetivos();

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

                cmb_ciudad.SelectedIndex = cmb_ciudad.Items.FindItemByValue(lbl_id_oficina.Text).Index;
                cmb_punto_de_venta.SelectedIndex = cmb_punto_de_venta.Items.FindItemByValue(lbl_id_pdv.Text).Index;
                cmb_categoria.SelectedIndex = cmb_categoria.Items.FindItemByValue(lbl_id_categoria.Text).Index;
                //Subcategorias();
                //cmb_subCategoria.SelectedIndex = cmb_subCategoria.Items.FindItemByValue(lbl_id_subCategoria.Text).Index;
                Marcas();
                cmb_marca.SelectedIndex = cmb_marca.Items.FindItemByValue(lbl_id_marca.Text).Index;
                cmb_subMarca.SelectedIndex = cmb_subMarca.Items.FindItemByValue(lbl_id_subMarca.Text).Index;

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

            oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_ciudad.SelectedValue);
            oReportes_parametros.Id_punto_venta = cmb_punto_de_venta.SelectedValue;
            oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
            oReportes_parametros.Id_subCategoria = cmb_subCategoria.SelectedValue;
            string sidmarca = cmb_marca.SelectedValue;
            if (sidmarca == "")
                sidmarca = "0";
            oReportes_parametros.Id_producto_marca = sidmarca;
            oReportes_parametros.Id_subMarca = cmb_subMarca.SelectedValue;
            oReportes_parametros.SkuProducto = cmb_skuProducto.SelectedValue;
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

            TabContainer_filtros.ActiveTabIndex = 0;
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

            if (sidmes!="0")
            {
                dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO",canal,Report, sidaño, sidmes, 1);


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


                        chkb_validar.Checked=false;//inicializamos los checkbox en false
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
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
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