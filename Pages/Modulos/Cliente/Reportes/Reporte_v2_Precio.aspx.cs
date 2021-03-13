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
    public partial class Reporte_v2_Precio : System.Web.UI.Page
    {
        private Conexion oCoon = new Conexion();
        
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = 
            new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        
        private Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = 
            new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();

        private string sUser;
        private string sPassw;
        private string sNameUser;
        private int icompany;
        private int iservicio;
        private string canal;
        private int Report;
        private ReportViewer reporte1;
        private ReportViewer reporteinfeje;
        private ReportViewer repvquincenal;
        private ReportViewer repbrecmarg;
        private ReportViewer repIndice;
        private ReportViewer repcompa;
        private ReportViewer repcompaciu;
        private ReportViewer reppanel;
        
        #region Funciones Especiales

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

        private void _AsignarVariables()
        {
            sidaño = cmb_año.SelectedValue;
            sidmes = cmb_mes.SelectedValue;
            sidperiodo = cmb_periodo.SelectedValue;
            sidciudad = cmb_ciudad.SelectedValue;
            sidcategoria = cmb_categoria.SelectedValue;
            sidsub_categoria = cmb_subCategoria.SelectedValue;
            sidmarca = cmb_marca.SelectedValue;
            sidsub_marca = cmb_subMarca.SelectedValue;

            sidsku = "0";

            //para control saplin previo
            //if (cmbCheckBox_skuProducto.SelectedIndex >= 0)
            //{
            //    string cadenaIdProducts = ""; 

            //    for (int i = 0; i < cmbCheckBox_skuProducto.Items.Count; i++)
            //    {
            //        if (cmbCheckBox_skuProducto.Items[i].Selected)
            //        {
            //            cadenaIdProducts = cadenaIdProducts + cmbCheckBox_skuProducto.Items[i].Value + ",";
            //        }
            //    }
            //    sidsku = cadenaIdProducts.Substring(0, cadenaIdProducts.Length - 1);
            //}

            string cadenaIdProducts = "";

            foreach (RadComboBoxItem item in cmb_skuProducto.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chk1");
                if (chk.Checked)
                    cadenaIdProducts += item.Value + ",";
            }

            if (cadenaIdProducts.Length != 0)
                sidsku = cadenaIdProducts.Substring(0, cadenaIdProducts.Length - 1);
            else
                sidsku = "0";

            string sidperdil = this.Session["Perfilid"].ToString();

            if (cmb_año.SelectedValue == "0" && 
                cmb_mes.SelectedValue == "0" && 
                cmb_periodo.SelectedValue == "0"){
                
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"]){
                    //aca debe ir la carga inical para el analista
                    icompany = Convert.ToInt32(this.Session["companyid"]);
                    iservicio = Convert.ToInt32(this.Session["Service"]);
                    canal = this.Session["Canal"].ToString().Trim();
                    Report = Convert.ToInt32(this.Session["Reporte"]);
                    
                    Periodo p = new Periodo(Report, 
                                            sidciudad, 
                                            sidcategoria,
                                            sidsub_categoria, 
                                            sidmarca,
                                            sidsub_marca,
                                            sidsku, 
                                            canal, 
                                            icompany, 
                                            iservicio);

                    p.Set_PeriodoConDataValidada();

                    sidaño = p.Año;
                    sidmes = p.Mes;
                    sidperiodo = p.PeriodoId;

                    GetPeridForAnalist();
                }
                else if (sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado"] || 
                         sidperdil == ConfigurationManager.AppSettings["PerfilClienteGeneral"] || 
                         sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado1"])
                {
                    GetPeriodForClient();
                }
            } else { if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"]) { GetPeridForAnalist(); }
            }
        }

        private void llenarreporteInicial()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                reporte1 = (ReportViewer)(Reporte_v2_Precio_InformePrecio.FindControl("reportinPrecios"));
                reporte1.Visible = true;
                reporte1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                reporte1.ServerReport.ReportPath = "/Reporte_Precios_V1/Informe_de_Precios";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                reporte1.ServerReport.ReportServerUrl = new Uri(strConnection);
                reporte1.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sidciudad));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGO", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBCATEGO", sidsub_categoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MARCA", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBMARCA", sidsub_marca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PRODUCTOS", sidsku));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PDV", sidpuntoventa));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));

                reporte1.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
            }
        }

        private void llenarreporteInformeEjecutivo()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            canal = this.Session["Canal"].ToString().Trim();


            try
            {

                reporteinfeje = (ReportViewer)(Reporte_v2_Precio_ResumenEjecutivo.FindControl("Reportresumen"));
                reporteinfeje.Visible = true;
                reporteinfeje.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                reporteinfeje.ServerReport.ReportPath = "/Reporte_Precios_V1/Resumen_Ejecutivo";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                reporteinfeje.ServerReport.ReportServerUrl = new Uri(strConnection);
                reporteinfeje.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                reporteinfeje.ServerReport.SetParameters(parametros);
            }

            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
            }
        }

        private void llenarreporteQuincenal()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                repvquincenal = (ReportViewer)(Reporte_v2_Precio_VariacionQuincenal.FindControl("reportvquincenal"));
                repvquincenal.Visible = true;
                repvquincenal.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                repvquincenal.ServerReport.ReportPath = "/Reporte_Precios_V1/Variacion_Quincenal";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                repvquincenal.ServerReport.ReportServerUrl = new Uri(strConnection);

                repvquincenal.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = 
                    new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sidciudad));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGO", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBCATEGO", sidsub_categoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MARCA", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBMARCA", sidsub_marca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PRODUCTOS", sidsku));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PDV", sidpuntoventa));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));

                repvquincenal.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
            }
        }

        private void llenaGraficaMargenesBre()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {
                repbrecmarg = (ReportViewer)(Reporte_v2_Precio_MargenesYBrechas.FindControl("ReportBrechas"));
                repbrecmarg.Visible = true;
                repbrecmarg.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                repbrecmarg.ServerReport.ReportPath = "/Reporte_Precios_V1/Margenes_y_Brechas";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                repbrecmarg.ServerReport.ReportServerUrl = new Uri(strConnection);
                repbrecmarg.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sidciudad));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGO", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBCATEGO", sidsub_categoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MARCA", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBMARCA", sidsub_marca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PRODUCTOS", sidsku));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PDV", sidpuntoventa));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));

                repbrecmarg.ServerReport.SetParameters(parametros);
            }

            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
            }
        }

        private void llenarreporteIndiceM()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                repIndice = (ReportViewer)(Reporte_v2_Precio_IndiceMayoristas.FindControl("Reportindice"));
                repIndice.Visible = true;
                repIndice.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                repIndice.ServerReport.ReportPath = "/Reporte_Precios_V1/Indices_Mayoristas";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                repIndice.ServerReport.ReportServerUrl = new Uri(strConnection);
                repIndice.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sidciudad));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGO", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBCATEGO", sidsub_categoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MARCA", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBMARCA", sidsub_marca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PRODUCTOS", sidsku));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PDV", sidpuntoventa));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));

                repIndice.ServerReport.SetParameters(parametros);
            }

            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);

            }

        }

        private void llenarreporteCompa()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                repcompa = (ReportViewer)(Reporte_v2_Precio_ComparativoDePrecios.FindControl("ReportCompa"));
                repcompa.Visible = true;
                repcompa.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                repcompa.ServerReport.ReportPath = "/Reporte_Precios_V1/Comparativo_Precios";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                repcompa.ServerReport.ReportServerUrl = new Uri(strConnection);
                repcompa.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sidciudad));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGO", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBCATEGO", sidsub_categoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MARCA", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBMARCA", sidsub_marca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PRODUCTOS", sidsku));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PDV", sidpuntoventa));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));

                repcompa.ServerReport.SetParameters(parametros);
            }

            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
            }
        }

        private void llenarreporteCompaciuda()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            
            try
            {
                repcompaciu = (ReportViewer)(Reporte_v2_Precio_ComparativoPrecioEnCiudades.FindControl("ReportCompaCi"));
                repcompaciu.Visible = true;
                repcompaciu.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                repcompaciu.ServerReport.ReportPath = "/Reporte_Precios_V1/Comparativo_Precios_Ciudades";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                repcompaciu.ServerReport.ReportServerUrl = new Uri(strConnection);
                repcompaciu.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sidciudad));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGO", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBCATEGO", sidsub_categoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MARCA", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBMARCA", sidsub_marca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PRODUCTOS", sidsku));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PDV", sidpuntoventa));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));

                repcompaciu.ServerReport.SetParameters(parametros);
            }

            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
            }
        }

        private void llenarPanelClientes()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            canal = this.Session["Canal"].ToString().Trim();

            try
            {
                reppanel = (ReportViewer)(Reporte_v2_Precio_PanelDeCliente.FindControl("ReportPanel"));
                reppanel.Visible = true;
                reppanel.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                reppanel.ServerReport.ReportPath = "/Reporte_Precios_V1/Panel_de_Clientes";
                
                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                reppanel.ServerReport.ReportServerUrl = new Uri(strConnection);

                reppanel.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                
                reppanel.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
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
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", 
                                          canal, 
                                          icompany, 
                                          Report, 
                                          cmb_mes.SelectedValue);
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

        private void Cobertura()
        {
            DataTable dtc = null;
            dtc = Get_DataClientes.Get_Obtenerinfocombos(icompany, canal, "0", 1);
            if (dtc.Rows.Count > 0)
            {
                cmb_ciudad.DataSource = dtc;
                cmb_ciudad.DataValueField = "cod_city";
                cmb_ciudad.DataTextField = "name_city";
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
            dtcatego = Get_DataClientes.Get_Obtenerinfocombos_F(icompany, canal, Report, "0", 2);
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
                cmb_subCategoria.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
            }
            else
            {
                dtscat = null;
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
                cmb_subMarca.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
            }
            else
            {
                dtsm = null;
                cmb_subMarca.Enabled = false;
                //cmb_subMarca.Items.Clear();
            }
        }

        private void Productos()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            DataTable dtpdt = null;

            dtpdt = Get_DataClientes.Get_ObtenerProductos_Precios(sidaño, sidmes, Convert.ToInt32(sidperiodo), sidciudad, icompany, cmb_categoria.SelectedValue, Convert.ToInt64(cmb_subCategoria.SelectedValue), Convert.ToInt32(cmb_marca.SelectedValue), Convert.ToInt32(cmb_subMarca.SelectedValue));
            //dtpdt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PRODUCTOS_PRECIOS", sidaño, sidmes, sidperiodo, sidciudad, icompany, cmb_categoria.SelectedValue, cmb_subCategoria.SelectedValue, cmb_marca.SelectedValue, cmb_subMarca.SelectedValue);

            if (dtpdt.Rows.Count > 0)
            {               
                cmb_skuProducto.DataSource = dtpdt;
                cmb_skuProducto.DataValueField = "cod_Product";
                cmb_skuProducto.DataTextField = "Name_Product";
                cmb_skuProducto.DataBind();
                
                //item para seleccionar todos
                RadComboBoxItem radComboBoxItem = new RadComboBoxItem();
                radComboBoxItem.Text = "Seleccionar todos";
                radComboBoxItem.Value = "Seleccionar todos";
                cmb_skuProducto.Items.Insert(0, radComboBoxItem);
                radComboBoxItem.DataBind();
                
            }
            else
            {
                dtpdt = null;
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
            iservicio = Convert.ToInt32(this.Session["Service"]);
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();
            TabContainer_filtros.ActiveTabIndex = 0; //Se Agrega para inicializar el tab de filtros Ing.CarlosH 28/11/2011
            TabContainer_Reporte_Precio.ActiveTabIndex = 1; //iniciar el tab 1 de pestañas en reporte. Angel Ortiz 16/12/2011
            MyAccordion.SelectedIndex = 1;


            /// <summary>
            /// Metodo UpdateProgressContext2()
            /// Descripcion: Ejecuta el ProgressBar 
            /// Por: Pablo Salas A.
            /// Fecha:18/06/2011
            /// </summary>

           // UpdateProgressContext2();

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
                    cmb_subCategoria.DataBind();
                    cmb_subCategoria.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_ciudad.DataBind();
                    cmb_ciudad.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

                    cmb_marca.DataBind();
                    cmb_marca.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                    cmb_subMarca.DataBind();
                    cmb_subMarca.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

                    _AsignarVariables();
                    llenarreporteInformeEjecutivo();
                    llenarreporteInicial();
                    llenarreporteQuincenal();
                    llenaGraficaMargenesBre();
                    llenarreporteIndiceM();
                    llenarreporteCompa();
                    llenarreporteCompaciuda();
                    llenarPanelClientes();
                    cargarParametrosdeXml();

                    Años();

                    Llena_Meses();
                    Cobertura();
                    Categorias();
                    Productos();
                    cmb_año.SelectedIndex = cmb_año.Items.FindItemByValue(sidaño).Index;
                    cmb_mes.SelectedIndex = cmb_mes.Items.FindItemByValue(sidmes).Index;
                    Llenar_Periodos();
                    cmb_periodo.SelectedIndex = cmb_periodo.Items.FindItemByValue(sidperiodo.ToString()).Index;
                    //cmb_categoria.SelectedIndex = cmb_categoria.Items.FindItemByValue(id_ProductCategory).Index;
                }

                catch (Exception ex)
                {

                    Exception mensaje = ex;
                    this.Session.Abandon();
                    //Response.Redirect("~/err_mensaje_seccion.aspx", true);
                    Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                    mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
                }
            }
        }



        /// <summary>
        /// Metodo UpdateProgressContext2()
        /// Descripcion: Ejecuta el ProgressBar 
        /// Por: Pablo Salas A.
        /// Fecha:18/06/2011
        /// </summary>
        //private void UpdateProgressContext2()
        //{
        //    RadProgressContext context = RadProgressContext.Current;
        //    context.SecondaryTotal = "100";
        //    for (int i = 1; i < 100; i++)
        //    {
        //        context.SecondaryValue = i.ToString();
        //        context.SecondaryPercent = i.ToString();
        //        context.CurrentOperationText = "Procesando " + i.ToString() + "%";
        //        context.CurrentOperationText = " Cargando Informes de Precios...";

        //        if (!Response.IsClientConnected)
        //        {
        //            //Cancel button was clicked or the browser was closed, so stop processing
        //            break;
        //        }
        //        // simulate a long time performing the current step
        //        System.Threading.Thread.Sleep(100);
        //    }
        //}

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

                progress.CurrentOperationText = "Precios Canal Mayorista";

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
        //private void Delay()
        //{


        //    for (int i = 0; i < 100; i++)
        //    {
        //        lbltime.Text = i.ToString();




        //    }


        //}
        protected void cmb_categoria_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //cmb_marca.Items.Clear();
            //cmb_subMarca.Items.Clear();
            //cmb_subCategoria.Items.Clear();
            cmb_subCategoria.Enabled = true;
            Subcategorias();
            cmb_marca.Enabled = true;
            Marcas();
            Productos();
            //this.Session["catego"] = cmb_categoria.SelectedValue;
            //llenarreporteInicial();
            //llenarreporteQuincenal();
            //llenaGraficaMargenesBre();
            //llenarreporteIndiceM();
            //llenarreporteCompa();
            //llenarreporteCompaciuda();
        }

        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Llenar_Periodos();
            //llenarreporteInicial();
            //llenarreporteQuincenal();
            //llenaGraficaMargenesBre();
            //llenarreporteIndiceM();
            //llenarreporteCompa();
            //llenarreporteCompaciuda();
            //this.Session["Mes"] = cmb_mes.SelectedValue;
        }

        protected void cmb_skuProducto_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //llenarreporteInicial();
            //llenarreporteQuincenal();
            //llenaGraficaMargenesBre();
            //llenarreporteIndiceM();
            //llenarreporteCompa();
            //llenarreporteCompaciuda();
            //this.Session["Mes"] = cmb_mes.SelectedValue;
        }

        protected void cmb_subCategoria_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();
            Marcas();
            Productos();
            //this.Session["subcatego"] = cmb_subCategoria.SelectedValue;

        }

        protected void cmb_marca_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmb_subMarca.Enabled = true;
           // llenarreporteInicial();
            Submarcas();
            Productos();
        }

        protected void cmb_subMarca_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Productos();
            //llenarreporteInicial();
        }

        protected void cmb_punto_de_venta_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //this.Session["pdv"] = cmb_punto_de_venta.SelectedValue;

            //llenarreporteInicial();
            //llenarreporteQuincenal();
            //llenaGraficaMargenesBre();
            //llenarreporteCompa();
            //llenarreporteCompaciuda();
        }
        //protected void btn_ocultar_Click(object sender, EventArgs e)
        //{
        //    if (Div_filtros.Visible == false)
        //    {

        //        Div_filtros.Visible = true;
        //        btn_ocultar.Text = "Ocultar";
        //        btngnerar.Visible = true;
        //        cmbCheckBox_skuProducto.Visible = true;

        //    }
        //    else if (Div_filtros.Visible == true)
        //    {
                
        //        Div_filtros.Visible = false;
        //        btn_ocultar.Text = "Filtros";
        //        btngnerar.Visible = false;
        //        cmbCheckBox_skuProducto.Visible = false;

        //    }
        //}

        protected void btngnerar_Click(object sender, EventArgs e)
        {
            TabContainer_filtros.ActiveTabIndex = 0; //Se Agrega para inicializar el tab de filtros Ing.CarlosH 28/11/2011
            _AsignarVariables();
            llenarreporteInformeEjecutivo();
            llenarreporteInicial();

            llenaGraficaMargenesBre();
            llenarreporteIndiceM();
            llenarreporteCompa();
            llenarreporteCompaciuda();
            llenarreporteQuincenal();
            //llenarPanelClientes();

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
            oReportes_parametros.Id_punto_venta = "0";
            oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
            oReportes_parametros.Id_subCategoria = cmb_subCategoria.SelectedValue;
            string sidmarca = cmb_marca.SelectedValue;
            if (sidmarca == "")
                sidmarca = "0";
            oReportes_parametros.Id_producto_marca = sidmarca;
            oReportes_parametros.Id_subMarca = cmb_subMarca.SelectedValue;

            if (cmb_skuProducto.SelectedIndex >= 0)
            {
                string cadenaIdProducts = "";

                //for (int i = 0; i < cmbCheckBox_skuProducto.Items.Count; i++)
                //{
                //    if (cmbCheckBox_skuProducto.Items[i].Selected)
                //    {
                //        cadenaIdProducts = cadenaIdProducts + cmbCheckBox_skuProducto.Items[i].Value + ",";
                //    }
                //}                
                foreach (RadComboBoxItem item in cmb_skuProducto.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chk1");
                    if (chk.Checked)
                        cadenaIdProducts += item.Value + ",";
                }
                sidsku = cadenaIdProducts.Substring(0, cadenaIdProducts.Length - 1);
            }

            if (sidsku == null)
                sidsku = "0";
            else if (sidsku == "")
                sidsku = "0";

            oReportes_parametros.SkuProducto = sidsku;
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

            up_favoritos.Update();
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

                ///////////
                cmb_año.SelectedValue = lbl_id_año.Text;
                cmb_mes.SelectedValue = lbl_id_mes.Text;
                Llenar_Periodos();
                cmb_periodo.SelectedValue = lbl_id_periodo.Text;
                cmb_ciudad.SelectedValue = lbl_id_oficina.Text;
                cmb_categoria.SelectedValue = lbl_id_categoria.Text;
                Subcategorias();
                cmb_subCategoria.SelectedValue = lbl_id_subCategoria.Text;
                cmb_marca.SelectedValue = lbl_id_marca.Text;
                cmb_subMarca.SelectedValue = lbl_id_subMarca.Text;
                Productos();
                if (lbl_skuProducto.Text != "" && lbl_skuProducto.Text != "0")
                {
                    string cad_sku = lbl_skuProducto.Text;
                    string[] arr_sku = cad_sku.Split(',');

                    foreach (RadComboBoxItem item in cmb_skuProducto.Items)
                    {
                        for (int i = 0; i < arr_sku.Length; i++)
                        {
                            if (item.Value == arr_sku[i])
                            {
                                CheckBox chk = (CheckBox)item.FindControl("chk1");
                                chk.Checked = true;
                            }
                        }
                    }
                }
                else
                    sidsku = "0";

                llenarreporteInformeEjecutivo();
                llenarreporteInicial();
                llenaGraficaMargenesBre();
                llenarreporteIndiceM();
                llenarreporteCompa();
                llenarreporteCompaciuda();
                llenarreporteQuincenal();
                //llenarPanelClientes();
                UpFiltrosPrecios.Update();
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
                cmb_año.SelectedValue = lbl_id_año.Text;
                cmb_mes.SelectedValue = lbl_id_mes.Text;
                Llenar_Periodos();
                cmb_periodo.SelectedValue = lbl_id_periodo.Text;
                cmb_ciudad.SelectedValue = lbl_id_oficina.Text;
                cmb_categoria.SelectedValue = lbl_id_categoria.Text;
                Subcategorias();
                cmb_subCategoria.SelectedValue = lbl_id_subCategoria.Text;
                cmb_marca.SelectedValue = lbl_id_marca.Text;
                cmb_subMarca.SelectedValue = lbl_id_subMarca.Text;
                Productos();
                if (lbl_skuProducto.Text != "" && lbl_skuProducto.Text != "0")
                {
                    string cad_sku = lbl_skuProducto.Text;
                    string[] arr_sku = cad_sku.Split(',');

                    foreach (RadComboBoxItem item in cmb_skuProducto.Items)
                    {
                        for (int i = 0; i < arr_sku.Length; i++)
                        {
                            if (item.Value == arr_sku[i])
                            {
                                CheckBox chk = (CheckBox)item.FindControl("chk1");
                                chk.Checked = true;
                            }
                        }
                    }
                }
                else
                    sidsku = "0";

                TabContainer_filtros.ActiveTabIndex = 0;
                lbl_updateconsulta.Visible = true;
                btn_img_actualizar.Visible = true;

                lbl_saveconsulta.Visible = false;
                btn_img_add.Visible = false;
                UpFiltrosPrecios.Update();
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
            oReportes_parametros.Id_punto_venta = "0";
            oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
            oReportes_parametros.Id_subCategoria = cmb_subCategoria.SelectedValue;
            string sidmarca = cmb_marca.SelectedValue;
            if (sidmarca == "")
                sidmarca = "0";
            oReportes_parametros.Id_producto_marca = sidmarca;
            oReportes_parametros.Id_subMarca = cmb_subMarca.SelectedValue;
            //oReportes_parametros.SkuProducto = cmbCheckBox_skuProducto.SelectedValue;
            if (cmb_skuProducto.Items.Count > 0)
            {
                string cadenaIdProducts = "";

                foreach (RadComboBoxItem item in cmb_skuProducto.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chk1");
                    if (chk.Checked)
                        cadenaIdProducts += item.Value + ",";
                }
                sidsku = cadenaIdProducts.Substring(0, cadenaIdProducts.Length - 1);
            }

            if (sidsku == null)
                sidsku = "0";
            else if (sidsku == "")
                sidsku = "0";

            oReportes_parametros.SkuProducto = sidsku;
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
            TabContainer_filtros.ActiveTabIndex = 0;
            up_favoritos.Update();
        }
        #region Logica de la Validación del Reporte
        protected void GetPeriodForClient()
        { //se obtiene el ultimo años mes y perido validado por el analista, para que el cliente pueda ver dicho reporte
            DataTable dt = null;

            Report = Convert.ToInt32(this.Session["Reporte"]);
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_MAX_PERIODO_VALIDADO_FINAL", Report, iservicio, canal, icompany);
                       
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

            Report = Convert.ToInt32(this.Session["Reporte"]);
            DataTable dt = null;
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO",canal ,Report, sidaño,sidmes,sidperiodo);

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
                    lbl_periodo_value.Text =sidperiodo;


                    if (valid_analist)
                        chkb_validar.Checked = valid_analist;
                    else
                        chkb_invalidar.Checked = true;

                    lbl_validacion.Text = sidaño + "-" + dt.Rows[0]["Month_name"].ToString() + " " + sidperiodo;

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

    }
}
