using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Test : System.Web.UI.Page
    {
        private int iidcompany;
        private string sidcanal;

        private string sidaño="2011";
        private string sidmes="10";
        private string sidperiodo="0";
        private int icadena=0;
        private string sioficina="0";
        private string sidcategoria="0";
        private int inegocio=0;
        delegate string DType(string input);
        string sUser;
        string sPassw;
        string sNameUser;
        int iservicio;
        string canal;
        int Report;
        //ReportViewer reporteHistorical;
        ReportViewer reporteExecutiveSumary;
        ReportViewer reporteIndexPrice;
        ReportViewer reporteIndexPriceDetail;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Page.ClientScript.Scro
               // ReportExecutiveSummary();
                //ReportIndexPrice();
                ReportHistorical();
            }

        }
        private void ReportHistorical()
        {
            iidcompany = 1561;
            iservicio = 254;
            canal = "1000";
            Report = 58;

            try
            {

               
                reporteHistorical.Visible = true;
                //reporteHistorical.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                reporteHistorical.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_PresenciaMayMinColgate";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                reporteHistorical.ServerReport.ReportServerUrl = new Uri(strConnection);
                reporteHistorical.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(iidcompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", icadena.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("COBERTURA", "0"));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", "0"));

                reporteHistorical.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
            }

        }
        private void ReportIndexPrice()
        {

            iidcompany = 1561;
            iservicio = 254;
            canal = "1000";
            Report = 58;
            try
            {

               // reporteIndexPrice = (ReportViewer)(Reporte_v2_IndexPrice1.FindControl("ReportIndexPrice"));
                reporteIndexPrice.Visible = true;
                reporteIndexPrice.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                reporteIndexPrice.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_IndexPriceResumen";
                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                reporteIndexPrice.ServerReport.ReportServerUrl = new Uri(strConnection);
                reporteIndexPrice.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(iidcompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", icadena.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sioficina));
                reporteIndexPrice.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";
            }
        }

        private void ReportExecutiveSummary()
        {
            //iidcompany = Convert.ToInt32(this.Session["companyid"]);
            //iservicio = Convert.ToInt32(this.Session["Service"]);
            //canal = this.Session["Canal"].ToString().Trim();
            //Report = Convert.ToInt32(this.Session["Reporte"]);
            iidcompany = 1561;
            iservicio = 254;
            canal = "1000";
            Report = 58;

            try
            {

               // reporteExecutiveSumary = (ReportViewer)(Reporte_v2_Wholesalers1.FindControl("ReportWholessalersGrafics"));
                reporteExecutiveSumary.Visible = true;
                reporteExecutiveSumary.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                reporteExecutiveSumary.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_PresenciaMayColgateGraficos";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                reporteExecutiveSumary.ServerReport.ReportServerUrl = new Uri(strConnection);
                reporteExecutiveSumary.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(iidcompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", icadena.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", ));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sioficina));

                reporteExecutiveSumary.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


            }
        }
    }
}