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
    public partial class Reporte_v2_Precio_VariacionQuincenal : System.Web.UI.UserControl
    {

        //string canal;
        //int icompanyid;
        //string sciudad;
        //string catego;
        //string subcatego;
        //string pdv;
        //string año;
        //string mes;

        //private void llenarreporteInicial()
        //{
        //    reportvquincenal.Visible = true;
        //    reportvquincenal.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
        //    reportvquincenal.ServerReport.ReportPath = "/Reporte_Precios_V1/Variacion_Quincenal";


        //    String strConnection = ConfigurationSettings.AppSettings["SERVIDOR_REPORTING_SERVICES"];
        //    reportvquincenal.ServerReport.ReportServerUrl = new Uri(strConnection);

        //    List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

        //    parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", año));
        //    parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", mes));
        //    parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sciudad));
        //    parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGO", catego));
        //    parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SUBCATEGO", subcatego));
        //    parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PDV", pdv));
        //    parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
        //    parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(icompanyid)));


        //    reportvquincenal.ServerReport.SetParameters(parametros);















        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            //canal = this.Session["Canal"].ToString().Trim();
            //icompanyid = Convert.ToInt32(this.Session["companyid"]);
            //sciudad = this.Session["Ciudad"].ToString().Trim();
            //catego = this.Session["catego"].ToString().Trim();
            //subcatego = this.Session["subcatego"].ToString().Trim();
            //año = this.Session["Año"].ToString().Trim();
            //mes = this.Session["Mes"].ToString().Trim();
            //pdv = this.Session["pdv"].ToString().Trim();

            

        }
      


    }
}