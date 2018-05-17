using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;


namespace SIGE.Pages.Modulos.Planning.Reports
{

   
    public partial class Brief_Campaña : System.Web.UI.Page
    {
        #region Declaracion de Varibles
       int  idplanning;
        
       
        #endregion
       protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                //this.Session["idplanning"] = "";
                idplanning = Convert.ToInt32(this.Session["iplanning"]);
             

                ReportBrief.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;


                ReportBrief.ServerReport.ReportPath = "/Developer_Report/BRIEF DE CAMPAÑA";
                //FMARTINEZ SE DEJA LA RUTA DESDE EL WEB CONFIG
                //ReportBrief.ServerReport.ReportServerUrl = new Uri("http://LUCKYDC/ReportServer");
                
                String strConnection = ConfigurationSettings.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                ReportBrief.ServerReport.ReportServerUrl = new Uri(strConnection);

               


                //Array de parametros

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();


                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("IDPLANNING",  Convert.ToString(idplanning)));

                //Se añadedn los parametros al reportViewer
                
                ReportBrief.ServerReport.SetParameters(parametros);

                //Obtener la coleccion de parametros
                Microsoft.Reporting.WebForms.ReportParameterInfoCollection parametrosr = ReportBrief.ServerReport.GetParameters();
            }

        }
    }
}
