using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using Lucky.Data;

namespace SIGE.Pages.Modulos.Operativo.Reports.MasterPage
{
    public partial class DefaultVerticalMenu : System.Web.UI.UserControl
    {

        SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_Cliente = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();
        //onexion oConn = new Lucky.Data.Conexion();
        int compañia;
        private Conexion oCoon = new Conexion();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                string sCodUsuario = this.Session["codUsuario"].ToString();
                if (sCodUsuario != null)
                {
                    if (!Page.IsPostBack)
                    {
                        CargarMenu();
                    }

                }
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                // Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void CargarMenu()
        {
            RadPanelItem PItemN1 = new RadPanelItem();

            DataTable dt = oCoon.ejecutarDataTable("PA_GET_PlanningByCodUsuario", this.Session["codUsuario"].ToString());
            /*if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        //oecDex.Id_Dex = Convert.ToInt32(dt.Rows[i]["Id_Dex"].ToString().Trim());
                        //oecDex.Dex_Name = dt.Rows[i]["Dex_Name"].ToString().Trim();
                        //oecDex.Dex_Status = Convert.ToBoolean(dt.Rows[i]["Dex_Status"].ToString().Trim());
                        //oecDex.Dex_CreateBy = dt.Rows[i]["Dex_CreateBy"].ToString().Trim();
                        //oecDex.Dex_DateBy = Convert.ToDateTime(dt.Rows[i]["Dex_DateBy"].ToString().Trim());
                        //oecDex.Dex_ModiBy = dt.Rows[i]["Dex_ModiBy"].ToString().Trim();
                        //oecDex.Dex_DateModiBy = Convert.ToDateTime(dt.Rows[i]["Dex_DateModiBy"].ToString().Trim());
                    }
                }
            }*/

            #region reportes
            PItemN1.Text = "Reportes";
            PItemN1.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasks.gif";
            PItemN1.Expanded = true;
            
            //*******************************************************************************
            //reportes para san fernando moderno
            //agregado por:pablo salas
            //02/08/2011

            RadPanelItem PItemN2_reporteIncioFIn = new RadPanelItem();
            PItemN2_reporteIncioFIn.Text = "R.Inicio_FIn_Dia";
            PItemN2_reporteIncioFIn.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporteIncioFIn.NavigateUrl = "../Rpt_Inicio_Fin.aspx";

            #region repetitivo
            RadPanelItem PItemN2_reporteImpulsoFM = new RadPanelItem();
            PItemN2_reporteImpulsoFM.Text = "R. Impulso Moderno";
            PItemN2_reporteImpulsoFM.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporteImpulsoFM.NavigateUrl = "../Report_Impulso_SF_M.aspx";

            RadPanelItem PItemN2_reporteCompetenciaFM = new RadPanelItem();
            PItemN2_reporteCompetenciaFM.Text = "R. Competencia Moderno";
            PItemN2_reporteCompetenciaFM.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporteCompetenciaFM.NavigateUrl = "../Report_Competencia_SF_M.aspx";
            
            RadPanelItem PItemN2_reportePrecioSFM = new RadPanelItem();
            PItemN2_reportePrecioSFM.Text = "R. Precios Moderno";
            PItemN2_reportePrecioSFM.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportePrecioSFM.NavigateUrl = "../Report_Precios_SF_M.aspx";


            RadPanelItem PItemN2_reporteStockIngresoPorDia = new RadPanelItem();
            PItemN2_reporteStockIngresoPorDia.Text = "R. Stock-Ingresos Moderno";
            PItemN2_reporteStockIngresoPorDia.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporteStockIngresoPorDia.NavigateUrl = "../Report_Ingresos-Stock_SF_M.aspx";
            //*******************************************************************************

            //creado: 13/06/2011. Por: Pablo Salas
            RadPanelItem PItemN2_reportStockPorDia = new RadPanelItem();
            PItemN2_reportStockPorDia.Text = "Reporte de Stock San Fernando";
            PItemN2_reportStockPorDia.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportStockPorDia.NavigateUrl = "../Report_StockSF.aspx";
            /////
            RadPanelItem PItemN2_OpeDigitacion = new RadPanelItem();
            PItemN2_OpeDigitacion.Text = "Operativo Digitacion";
            PItemN2_OpeDigitacion.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_OpeDigitacion.NavigateUrl = "../../OPE_Digitacion.aspx";

            RadPanelItem PItemN2_reportPrecio = new RadPanelItem();
            PItemN2_reportPrecio.Text = "Reporte de Precios";
            PItemN2_reportPrecio.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportPrecio.NavigateUrl = "../Report_Precio.aspx";

            RadPanelItem PItemN2_reportCompetencia = new RadPanelItem();
            PItemN2_reportCompetencia.Text = "Reporte de Competencia";
            PItemN2_reportCompetencia.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportCompetencia.NavigateUrl = "../Report_Competencia.aspx";

            RadPanelItem PItemN2_reportExhibicion = new RadPanelItem();
            PItemN2_reportExhibicion.Text = "Reporte de Exhibición";
            PItemN2_reportExhibicion.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportExhibicion.NavigateUrl = "../Report_Exhibicion.aspx";

            RadPanelItem PItemN2_reportLayout = new RadPanelItem();
            PItemN2_reportLayout.Text = "Reporte de Layout";
            PItemN2_reportLayout.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportLayout.NavigateUrl = "../Report_Layout.aspx";

            RadPanelItem PItemN2_reportQuiebre = new RadPanelItem();
            PItemN2_reportQuiebre.Text = "Reporte de Quiebre";
            PItemN2_reportQuiebre.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportQuiebre.NavigateUrl = "../Report_Quiebre.aspx";

            RadPanelItem PItemN2_reportSegementacion = new RadPanelItem();
            PItemN2_reportSegementacion.Text = "Reporte de Segmentación";
            PItemN2_reportSegementacion.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportSegementacion.NavigateUrl = "../Report_Segmentacion.aspx";

            RadPanelItem PItemN2_reportSOD = new RadPanelItem();
            PItemN2_reportSOD.Text = "Reporte de SOD";
            PItemN2_reportSOD.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportSOD.NavigateUrl = "../Report_SOD.aspx";

            RadPanelItem PItemN2_reportStock = new RadPanelItem();
            PItemN2_reportStock.Text = "Reporte de Stock";
            PItemN2_reportStock.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportStock.NavigateUrl = "../Report_Stock.aspx";

            RadPanelItem PItemN2_reportFotografico = new RadPanelItem();
            PItemN2_reportFotografico.Text = "Reporte de Fotografico";
            PItemN2_reportFotografico.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportFotografico.NavigateUrl = "../ReportFotografico.aspx";

            RadPanelItem PItemN2_reportRptSegementacion = new RadPanelItem();
            PItemN2_reportRptSegementacion.Text = "Consolidado de segmentacion";
            PItemN2_reportRptSegementacion.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportRptSegementacion.NavigateUrl = "../Rpt_Segmentacion.aspx";

            RadPanelItem PItemN2_SegNov = new RadPanelItem();
            PItemN2_SegNov.Text = "Seguimiento de visitas no efectivas";
            PItemN2_SegNov.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_SegNov.NavigateUrl = "../Rpt_SegNov.aspx";


            RadPanelItem PItemN2_LevantamientoPublicacion= new RadPanelItem();
            PItemN2_LevantamientoPublicacion.Text = "Levantamiento de Publicaciones";
            PItemN2_LevantamientoPublicacion.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_LevantamientoPublicacion.NavigateUrl = "../LevantamientoPublicaciones.aspx";


            RadPanelItem PItemN2_LevantamientoExhImpul = new RadPanelItem();
            PItemN2_LevantamientoExhImpul.Text = "Levantamiento Exhi. e Impulso";
            PItemN2_LevantamientoExhImpul.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_LevantamientoExhImpul.NavigateUrl = "../LevantamientoExhImpuls.aspx";


            RadPanelItem PItemN2_LevantamientoMaterialPOP = new RadPanelItem();
            PItemN2_LevantamientoMaterialPOP.Text = "Levantamiento Material POP";
            PItemN2_LevantamientoMaterialPOP.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_LevantamientoMaterialPOP.NavigateUrl = "../LevantamientoMaterialPOP.aspx";

            RadPanelItem PItemN2_Rpt_SegIngre = new RadPanelItem();
            PItemN2_Rpt_SegIngre.Text = "Ingreso de Supervisores Xplora";
            PItemN2_Rpt_SegIngre.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_Rpt_SegIngre.NavigateUrl = "../Rpt_SegIngre.aspx";


            RadPanelItem PItemN2_reportPrecioSF = new RadPanelItem();
            PItemN2_reportPrecioSF.Text = "Reporte de Precios";
            PItemN2_reportPrecioSF.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportPrecioSF.NavigateUrl = "../Report_PrecioSF.aspx";

            RadPanelItem PItemN2_reportVentas = new RadPanelItem();
            PItemN2_reportVentas.Text = "Reporte de Ventas";
            PItemN2_reportVentas.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportVentas.NavigateUrl = "../Report_Ventas.aspx";

            RadPanelItem PItemN2_reportIncidencias = new RadPanelItem();
            PItemN2_reportIncidencias.Text = "Reporte de Incidencias";
            PItemN2_reportIncidencias.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportIncidencias.NavigateUrl = "../Report_Incidencias.aspx";

            RadPanelItem PItemN2_reportQuiebres3M = new RadPanelItem();
            PItemN2_reportQuiebres3M.Text = "Reporte de Presencia";
            PItemN2_reportQuiebres3M.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportQuiebres3M.NavigateUrl = "../Report_Quiebres3M.aspx";


            RadPanelItem PItemN2_Rpt_SegIngreC = new RadPanelItem();
            PItemN2_Rpt_SegIngreC.Text = "Ingreso de Ejecutivos Xplora";
            PItemN2_Rpt_SegIngreC.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_Rpt_SegIngreC.NavigateUrl = "../Rpt_SegIngreEC.aspx";


            //Masisa Joseph Gonzales
            RadPanelItem PItemN2_reporte_Venta_Masisa = new RadPanelItem();
            PItemN2_reporte_Venta_Masisa.Text = "R. Ventas Especializado";
            PItemN2_reporte_Venta_Masisa.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_Venta_Masisa.NavigateUrl = "../Reporte_Ventas_Masisa.aspx";

            RadPanelItem PItemN2_reporte_Fotografico_Masisa = new RadPanelItem();
            PItemN2_reporte_Fotografico_Masisa.Text = "R. Fotografico Especializado";
            PItemN2_reporte_Fotografico_Masisa.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_Fotografico_Masisa.NavigateUrl = "../ReportFotografico.aspx";
            
            //pSalas. 05/10/2011 - Colgate:  Reporte de Presencia
            RadPanelItem PItemN2_reportePresencia = new RadPanelItem();
            PItemN2_reportePresencia.Text = "Reporte de Presencia";
            PItemN2_reportePresencia.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportePresencia.NavigateUrl = "../Report_Presencia_Colg.aspx";

            //pSalas. 05/10/2011 - Colgate:  Reporte de Presencia
            RadPanelItem PItemN2_reportePresenciaConsolidado = new RadPanelItem();
            PItemN2_reportePresenciaConsolidado.Text = "Reporte de Presencia Consolidado";
            PItemN2_reportePresenciaConsolidado.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reportePresenciaConsolidado.NavigateUrl = "../Report_Presencia_Colgate.aspx";

            //pSalas. 13/10/2011 - Colgate:  Reporte de Competencia V2
            RadPanelItem PItemN2_reporteCompetenciaV2_SFM = new RadPanelItem();
            PItemN2_reporteCompetenciaV2_SFM.Text = "R. Competencia Moderno ";
            PItemN2_reporteCompetenciaV2_SFM.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporteCompetenciaV2_SFM.NavigateUrl = "../Report_Competencia_SF_M_V_1_1.aspx";

            /////*******************************/////
            /////*******************************/////
            /////******CANAL TRADICIONAL********/////
            /////*******************************/////
            /////*******************************/////

            //pSalas. 10/01/2012 - San Fernando - Tradicional:  Reporte de Examen de Tienda
            RadPanelItem PItemN2_reporte_SF_Tra_ActDatos = new RadPanelItem();
            PItemN2_reporte_SF_Tra_ActDatos.Text = "R. Examen de Tienda - Tradicional";
            PItemN2_reporte_SF_Tra_ActDatos.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_SF_Tra_ActDatos.NavigateUrl = "../Report_SF_Tra_ExaTda.aspx";


            //pSalas. 10/01/2012 - San Fernando - Tradicional:  Reporte de Disponibilidad
            RadPanelItem PItemN2_reporte_SF_Tra_Disponibilidad = new RadPanelItem();
            PItemN2_reporte_SF_Tra_Disponibilidad.Text = "R. Disponibilidad - Tradicional";
            PItemN2_reporte_SF_Tra_Disponibilidad.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_SF_Tra_Disponibilidad.NavigateUrl = "../Report_SF_Tra_Disponibilidad.aspx";

            //Report_SF_Tra_ExaTdaConsolidado.aspx pSalas 16/01/2012 - Reporte Examen de Tienda Consolidado - se usa ReportingServices
            RadPanelItem PItemN2_reporte_SF_Tra_ExaTdaConsolidado = new RadPanelItem();
            PItemN2_reporte_SF_Tra_ExaTdaConsolidado.Text = "R. Exa.Tda Consolidado - Tradicional";
            PItemN2_reporte_SF_Tra_ExaTdaConsolidado.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_SF_Tra_ExaTdaConsolidado.NavigateUrl = "../Report_SF_Tra_ExaTdaConsolidado.aspx";



            //////////////////////////////////////////////

            RadPanelItem PItemN2_reporte_Precios_Cementos = new RadPanelItem();
            PItemN2_reporte_Precios_Cementos.Text = "R.Precios";
            PItemN2_reporte_Precios_Cementos.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_Precios_Cementos.NavigateUrl = "../Report_Precio_Cementos.aspx";

            RadPanelItem PItemN2_reporte_Ventas_Cementos = new RadPanelItem();
            PItemN2_reporte_Ventas_Cementos.Text = "R.Ventas";
            PItemN2_reporte_Ventas_Cementos.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_Ventas_Cementos.NavigateUrl = "../Report_Ventas_Cementos.aspx";

            RadPanelItem PItemN2_reporte_Levantami = new RadPanelItem();
            PItemN2_reporte_Levantami.Text = "R.Lev_Información";
            PItemN2_reporte_Levantami.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_Levantami.NavigateUrl = "../Rpt_levinfo.aspx";

            RadPanelItem PItemN2_reporte_Fachada_Cementos = new RadPanelItem();
            PItemN2_reporte_Fachada_Cementos.Text = "R.Fachada";
            PItemN2_reporte_Fachada_Cementos.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_Fachada_Cementos.NavigateUrl = "../Report_Fachada_Cementos1.aspx";

            RadPanelItem PItemN2_reporte_Inflable_Cementos = new RadPanelItem();
            PItemN2_reporte_Inflable_Cementos.Text = "R.Inflable";
            PItemN2_reporte_Inflable_Cementos.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_Inflable_Cementos.NavigateUrl = "../Report_Inflable_Cementos.aspx";

            RadPanelItem PItemN2_reporte_Competencia_Cementos = new RadPanelItem();
            PItemN2_reporte_Competencia_Cementos.Text = "R.Competencia";
            PItemN2_reporte_Competencia_Cementos.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_Competencia_Cementos.NavigateUrl = "../Report_Competencia_Cementos.aspx";

            
            RadPanelItem PItemN2_reporte_Spor_SF_M = new RadPanelItem();
            PItemN2_reporte_Spor_SF_M.Text = "R.Spot";
            PItemN2_reporte_Spor_SF_M.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_Spor_SF_M.NavigateUrl = "../Report_Spot_SF_M.aspx";


            //Colgate --Canal DT
            RadPanelItem PItemN2_reporte_presencia_DT = new RadPanelItem();
            PItemN2_reporte_presencia_DT.Text = "Report. DT";
            PItemN2_reporte_presencia_DT.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_presencia_DT.NavigateUrl = "../Report_Presencia_Colgate_DT.aspx";

            RadPanelItem PItemN2_reporte_presencia_DT_pop = new RadPanelItem();
            PItemN2_reporte_presencia_DT_pop.Text = "Reporte Pop";
            PItemN2_reporte_presencia_DT_pop.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_presencia_DT_pop.NavigateUrl = "../Reporte_Pop/Reporte_PopFotos.aspx";

            RadPanelItem PItemN2_reporte_promocion_DT = new RadPanelItem();
            PItemN2_reporte_promocion_DT.Text = "Reporte Promoción";
            PItemN2_reporte_promocion_DT.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_promocion_DT.NavigateUrl = "../Reporte_Promocion/Reporte_Promocion.aspx";

            //Colgate --Canal IT
            RadPanelItem PItemN2_reporte_presencia_IT = new RadPanelItem();
            PItemN2_reporte_presencia_IT.Text = "Report. IT";
            PItemN2_reporte_presencia_IT.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_presencia_IT.NavigateUrl = "../Report_Presencia_Colgate_IT.aspx";


            //Colgate --Canal BODEGA
            RadPanelItem PItemN2_reporte_presencia_BODEGA = new RadPanelItem();
            PItemN2_reporte_presencia_BODEGA.Text = "Report. BODEGA";
            PItemN2_reporte_presencia_BODEGA.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_presencia_BODEGA.NavigateUrl = "../Report_Presencia_Colgate_BODEGA.aspx";

            //Reporte Fotografico New
            //Fecha: 21 /07/2012
            //Add : Pablo Salas Alvarez.
            RadPanelItem PItemN2_reporte_Fotografico_New = new RadPanelItem();
            PItemN2_reporte_Fotografico_New.Text = "Report. Fotografico";
            PItemN2_reporte_Fotografico_New.ImageUrl = "~/Pages/Modulos/Operativo/Reports/Image/tasksItems.gif";
            PItemN2_reporte_Fotografico_New.NavigateUrl = "../Reporte_Fotografico_New.aspx";
            #endregion


            #endregion

            //DataTable dt = null;
            compañia = Convert.ToInt32(this.Session["companyid"]);
            dt = Get_Cliente.Get_ObtenerCanalesxCliente(compañia);
            string sUser = this.Session["sUser"].ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (compañia == 1562 && Equals(dt.Rows[i]["codigo_canal"].ToString(), "1000"))
                {
                    //mayoristas
                    PItemN1.Items.Add(PItemN2_reportCompetencia);
                    PItemN1.Items.Add(PItemN2_reportExhibicion);
                    PItemN1.Items.Add(PItemN2_reportFotografico);
                    PItemN1.Items.Add(PItemN2_reportLayout);
                    PItemN1.Items.Add(PItemN2_reportQuiebre);
                    PItemN1.Items.Add(PItemN2_reportStock);
                    PItemN1.Items.Add(PItemN2_reportSOD);
                    PItemN1.Items.Add(PItemN2_reportPrecio);
                    PItemN1.Items.Add(PItemN2_LevantamientoPublicacion);
                    PItemN1.Items.Add(PItemN2_LevantamientoExhImpul);
                    PItemN1.Items.Add(PItemN2_LevantamientoMaterialPOP);
                    PItemN1.Items.Add(PItemN2_Rpt_SegIngre);
                    PItemN1.Items.Add(PItemN2_OpeDigitacion);
  
                    break;
                }
                if (compañia == 1562 && Equals(dt.Rows[i]["codigo_canal"].ToString(), "1023"))
                {
                    //minoristas

                    PItemN1.Items.Add(PItemN2_reportCompetencia);
                    PItemN1.Items.Add(PItemN2_reportFotografico);
                    PItemN1.Items.Add(PItemN2_reportExhibicion);
                    PItemN1.Items.Add(PItemN2_reportLayout);
                    PItemN1.Items.Add(PItemN2_reportStock);
                    PItemN1.Items.Add(PItemN2_reportSOD);
                    PItemN1.Items.Add(PItemN2_reportPrecio);
                    PItemN1.Items.Add(PItemN2_Rpt_SegIngre);
                    break;
                }
                if (compañia == 1562 && Equals(dt.Rows[i]["codigo_canal"].ToString(), "1241"))
                {
                    //autoservicios

                    PItemN1.Items.Add(PItemN2_reportCompetencia);
                    PItemN1.Items.Add(PItemN2_reportFotografico);
                    PItemN1.Items.Add(PItemN2_reportQuiebre);
                    PItemN1.Items.Add(PItemN2_reportLayout);
                    PItemN1.Items.Add(PItemN2_reportExhibicion);

                    PItemN1.Items.Add(PItemN2_reportPrecio);

                    PItemN1.Items.Add(PItemN2_SegNov);
                    PItemN1.Items.Add(PItemN2_LevantamientoPublicacion);
                    PItemN1.Items.Add(PItemN2_LevantamientoExhImpul);
                    PItemN1.Items.Add(PItemN2_LevantamientoMaterialPOP);
                    PItemN1.Items.Add(PItemN2_Rpt_SegIngre);

                    break;
                }
                if (compañia==1592 &&  Equals(dt.Rows[i]["codigo_canal"].ToString(), "1241"))
                {
                    //autoservicios
                    PItemN1.Items.Add(PItemN2_reportQuiebres3M);
                    PItemN1.Items.Add(PItemN2_reportFotografico);
                    break;
                }

                //Se Agrega el link para Cliente Cementos Lima Canal Progresol
                if (compañia == 1560 && Equals(dt.Rows[i]["codigo_canal"].ToString(), "1001"))
                {
                    //Progresol
                    
                   // PItemN1.Items.Add(PItemN2_reportFotografico);
                    PItemN1.Items.Add(PItemN2_reporte_Precios_Cementos);// Carlos Marin 23/11/2011
                    PItemN1.Items.Add(PItemN2_reporte_Ventas_Cementos);// Carlos Marin 23/11/2011
                    PItemN1.Items.Add(PItemN2_reporte_Fachada_Cementos);// Carlos Marin 23/11/2011
                    PItemN1.Items.Add(PItemN2_reporte_Inflable_Cementos);// Carlos Marin 23/11/2011
                    PItemN1.Items.Add(PItemN2_reporte_Competencia_Cementos);// Carlos Marin 23/11/2011
                    break;
                }

                if (compañia == 1561 && Equals(dt.Rows[i]["codigo_canal"].ToString(), "1000"))
                {
                    //autoservicios
                    PItemN1.Items.Add(PItemN2_Rpt_SegIngreC);
                    //PItemN1.Items.Add(PItemN2_reportFotografico);
                    PItemN1.Items.Add(PItemN2_reporte_Fotografico_New);//Pablo Salas A. 21/07/2012
                    PItemN1.Items.Add(PItemN2_reportePresencia);//pSalas . 05/10/2011 Reporte Presencia
                    PItemN1.Items.Add(PItemN2_reportePresenciaConsolidado);//jGonzales . 03/11/2011 Reporte Presencia
                    PItemN1.Items.Add(PItemN2_reporte_presencia_DT);//cmarin . 29/2/2012
                    PItemN1.Items.Add(PItemN2_reporte_presencia_IT);//cmarin . 12/3/2012
                    PItemN1.Items.Add(PItemN2_reporte_presencia_BODEGA); //cmarin . 17/4/2012
                    PItemN1.Items.Add(PItemN2_reporte_presencia_DT_pop);//Destrada 27/04/2012
                    PItemN1.Items.Add(PItemN2_reporte_promocion_DT);//Destrada 27/04/2012

                    break;
                }

                if (compañia == 1572 && Equals(dt.Rows[i]["codigo_canal"].ToString(), "1002") )
                {
                    //TRADICIONAL SF

                    PItemN1.Items.Add(PItemN2_reportFotografico);
                    PItemN1.Items.Add(PItemN2_reportSegementacion);
                    PItemN1.Items.Add(PItemN2_reportRptSegementacion);
                    PItemN1.Items.Add(PItemN2_SegNov);
                    PItemN1.Items.Add(PItemN2_Rpt_SegIngre);
                    PItemN1.Items.Add(PItemN2_reportPrecioSF);
                    PItemN1.Items.Add(PItemN2_reportVentas);
                    PItemN1.Items.Add(PItemN2_reportIncidencias);
                    PItemN1.Items.Add(PItemN2_reporteIncioFIn);
                    PItemN1.Items.Add(PItemN2_reporte_Levantami);//Se Agrega Reporte de Status de Levantamiento Info AAVV
                                                                 //Ing. CarlosH 24/11/2011   
                }
                if (compañia == 1572 && Equals(dt.Rows[i]["codigo_canal"].ToString(), "1003"))
                {

                    //PARA SAN FERNANDO CANAL MODERNO - Por: Pablo - Fecha:02/08/2011 - Resumen:modulos para sanfernando moderno
                    PItemN1.Items.Add(PItemN2_reporteStockIngresoPorDia);
                    PItemN1.Items.Add(PItemN2_reportePrecioSFM);
                    //PItemN1.Items.Add(PItemN2_reporteCompetenciaFM);
                    PItemN1.Items.Add(PItemN2_reporteCompetenciaV2_SFM);
                    PItemN1.Items.Add(PItemN2_reporteImpulsoFM);
                    PItemN1.Items.Add(PItemN2_reporte_Spor_SF_M);
                    
                    //PItemN1.Items.Add(PItemN2_reporteStock_Seguimiento_SFM);

                    //////////*************SAN FERNANDO************//////////
                    //////////**********CANAL TRADICIONAL************//////////

                    PItemN1.Items.Add(PItemN2_reporte_SF_Tra_ActDatos); //Add 10/01/2012.  pSalas - Tradicional
                    PItemN1.Items.Add(PItemN2_reporte_SF_Tra_Disponibilidad); //Add 10/01/2012.  pSalas - Tradicional
                    PItemN1.Items.Add(PItemN2_reporte_SF_Tra_ExaTdaConsolidado);//Add 16/01/2012.  pSalas - Tradicional
                }

                if (compañia == 1609 && Equals(dt.Rows[i]["codigo_canal"].ToString(), "2005"))
                {

                    //PARA MASISA CANAL ESPECIALIZADO - Por: Joseph Gonzales - Fecha:04/10/2011 - Resumen:modulos masisa
                    PItemN1.Items.Add(PItemN2_reporte_Venta_Masisa);
                    PItemN1.Items.Add(PItemN2_reporte_Fotografico_Masisa);
                }
            }
            RadPanelBar_menu.Items.Add(PItemN1);
        }
    }
}