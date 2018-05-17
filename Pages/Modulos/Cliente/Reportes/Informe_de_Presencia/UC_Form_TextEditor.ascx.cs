using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Data;
using Telerik.Web.UI;
using System.Data;
using Lucky.CFG.Util;

namespace SIGE.Pages.Modulos.Cliente.Reportes.Informe_de_Presencia
{
    public partial class UC_Form_TextEditor : System.Web.UI.UserControl
    {
        private Int32 iidcompany;
        private String sidcanal;
        private String siduser;
        private Int32 Report;
        private Int32 iservicio;

        private static string año;
        private static string mes;
        private static string periodo;

        Conexion oCoon = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                iservicio = Convert.ToInt32(this.Session["Service"]);              
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();

                Periodo P = new Periodo();
                P.Servicio = iservicio;
                P.Canal = sidcanal;
                P.Cliente = iidcompany;
                P.SetPeriodoInicial_Presencia();
                año = P.Año;
                mes = P.Mes;
                periodo = P.PeriodoId;
                cmb_año.SelectedIndex = cmb_año.Items.FindItemIndexByValue(año);
                cmb_mes.SelectedIndex = cmb_mes.Items.FindItemIndexByValue(mes);
                cargarPeriodo();
                cmb_periodo.SelectedIndex = cmb_periodo.Items.FindItemIndexByValue(periodo);

                ResumenEjecutivo();
            }
        }

        public void llenaAñosUC(DataTable dty)
        {
            cmb_año.DataSource = dty;
            cmb_año.DataValueField = "Years_Number";
            cmb_año.DataTextField = "Years_Number";
            cmb_año.DataBind();
            cmb_año.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));

            
           
        }
        public void llenaMesUC(DataTable dtm)
        {

            cmb_mes.DataSource = dtm;
            cmb_mes.DataValueField = "codmes";
            cmb_mes.DataTextField = "namemes";
            cmb_mes.DataBind();
            cmb_mes.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));

           
        }

        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cargarPeriodo();
            
        }
        public void cargarPeriodo()
        {
           
            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            cmb_periodo.Items.Clear();
            cmb_periodo.Enabled = true;
            DataTable dtp = null;

            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PERIODOS_POR_MES", iservicio, sidcanal, iidcompany, Report, cmb_año.SelectedValue, cmb_mes.SelectedValue);

            cmb_periodo.DataSource = dtp;
            cmb_periodo.DataValueField = "ReportsPlanning_Periodo";
            cmb_periodo.DataTextField = "Descripcion";
            cmb_periodo.DataBind();

            cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                iservicio = Convert.ToInt32(this.Session["Service"]);
                Report = Convert.ToInt32(this.Session["Reporte"]);
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();
                siduser = this.Session["sUser"].ToString();

                string observacion =RadEditor_ResumenEjecutivo.Content;
                oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_INSERTAR_RESUMEN_EJECUTIVO", iservicio, sidcanal, iidcompany, Report, año, mes,periodo, observacion, siduser, DateTime.Now, siduser, DateTime.Now);
            }
            catch
            {

            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            ResumenEjecutivo();
        }
        protected void ResumenEjecutivo()
        {
            try
            {
                iservicio = Convert.ToInt32(this.Session["Service"]);
                Report = Convert.ToInt32(this.Session["Reporte"]);
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();

                DataTable dtre = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_RESUMEN_EJECUTIVO", iservicio, sidcanal, iidcompany, Report, cmb_año.SelectedValue,cmb_mes.SelectedValue,cmb_periodo.SelectedValue);

                RadEditor_ResumenEjecutivo.Content = dtre.Rows[0]["Observación"].ToString();

            }
            catch
            {

            }

        }
    }
}