using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace SIGE.Pages.Modulos.Cliente
{
    public partial class Pruebas_reportes : System.Web.UI.Page
    {
        private void LlenaCategorias() {
            DataSet ds = null;
            Conexion Ocoon = new Conexion();
            ds = Ocoon.ejecutarDataSet("UP_WEBXPLORA_MOVIL_OBTENERCATEGORIAS");
            if (ds.Tables[0].Rows.Count > 0) {
                cmbcategorias.DataSource = ds.Tables[0];
                cmbcategorias.DataValueField = "id_catego";
                cmbcategorias.DataTextField = "Name_Category";
                cmbcategorias.DataBind();

            
            
            
            
            
            }
        
        
        
        
        
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LlenaCategorias();
            String strConnection = ConfigurationSettings.AppSettings["SERVIDOR_REPORTING_SERVICES"];
            reportPrueba.ServerReport.ReportServerUrl = new Uri(strConnection);


        }

        protected void btngenerar_Click(object sender, EventArgs e)
        {
            reportPrueba.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
            reportPrueba.ServerReport.ReportPath = "/Reportes_Clientes/Panel_Precio";
           
            //Array de parametros

            List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
            parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria",cmbcategorias.SelectedValue));
            //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODOINI", txtfecini.Text));
            //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODOFIN", txtfecfin.Text));
            //reportPrueba.ServerReport.SetParameters(parametros);
            //Obtener la coleccion de parametros
            Microsoft.Reporting.WebForms.ReportParameterInfoCollection parametrosr = reportPrueba.ServerReport.GetParameters();




        }
    }
}
