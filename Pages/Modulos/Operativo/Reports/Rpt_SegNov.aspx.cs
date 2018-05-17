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

namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Rpt_SegNov : System.Web.UI.Page
    {
        #region Declaracion de variables generales

        private int compañia;
        private string pais;


        Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                CargarCombo_Channel();
                Cargar_Motivos();
            }
        }

        protected void CargarCombo_Channel()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            pais = this.Session["scountry"].ToString();


            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CHANNEL", pais, compañia);
            if (dt.Rows.Count > 0)
            {
                cmbcanal.DataSource = dt;
                cmbcanal.DataValueField = "cod_Channel";
                cmbcanal.DataTextField = "Channel_Name";
                cmbcanal.DataBind();
                cmbcanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }

        }

        protected void Cargar_Motivos() {


            DataTable dt;
            Conexion Ocoon = new Conexion();
            compañia = Convert.ToInt32(this.Session["companyid"]);
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OBTENERMOTIVOS", compañia);


            if (dt.Rows.Count > 0)
            {

                cmbmotivos.DataSource = dt;
                cmbmotivos.DataValueField = "id_motivo";
                cmbmotivos.DataTextField = "Descripcion";
                cmbmotivos.DataBind();
                cmbmotivos.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                cmbmotivos.Enabled = true;
            }
 
        }

        protected void cmbcanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            string sidchannel = cmbcanal.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);

            cmbplanning.Items.Clear();

            //cmbmotivos.Items.Clear();
            //cmbmotivos.Enabled = false;
            
            cmbperson.Items.Clear();
            cmbperson.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                cmbplanning.DataSource = dt;
                cmbplanning.DataValueField = "id_planning";
                cmbplanning.DataTextField = "Planning_Name";
                cmbplanning.DataBind();
                cmbplanning.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                cmbplanning.Enabled = true;
            }
        }

        protected void cmbplanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            //-------
            lblmensaje.Visible = true;
            //lblmensaje.Text = "Cargando...";
            //-------
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            string sidplanning = cmbplanning.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);


            //cmbmotivos.Items.Clear();
            //cmbmotivos.Enabled = false;
          
            cmbperson.Items.Clear();
            cmbperson.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                cmbperson.DataSource = dt;
                cmbperson.DataValueField = "Person_id";
                cmbperson.DataTextField = "Person_NameComplet";
                cmbperson.DataBind();
                cmbperson.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                cmbperson.Enabled = true;
            }

            //------llamado al metodo cargar categoria de producto

         
            //cargarGrilla_Quiebre();
        }


       
        //protected void cmbsku_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cargarGrilla_Quiebre();
        //}

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrilla_Quiebre();
        }
        protected void cargarGrilla_Quiebre()
        {
            try
            {
                int iidperson = Convert.ToInt32(cmbperson.SelectedValue);
                string sidplanning = cmbplanning.SelectedValue;
                string sidchannel = cmbcanal.SelectedValue;


                string simotivo = cmbmotivos.SelectedValue;
                if (simotivo == "")
                    simotivo = "0";
                //int iidmarca = Convert.ToInt32(sidmarca);

                compañia = Convert.ToInt32(this.Session["companyid"]);

                DateTime dfecha_inicio;
                DateTime dfecha_fin;

                if (txt_fecha_inicio.SelectedDate.ToString() == "" || txt_fecha_inicio.SelectedDate.ToString() == "0" || txt_fecha_inicio.SelectedDate == null)
                    dfecha_inicio = txt_fecha_inicio.MinDate;
                else dfecha_inicio = txt_fecha_inicio.SelectedDate.Value;


                if (txt_fecha_fin.SelectedDate.ToString() == "" || txt_fecha_fin.SelectedDate.ToString() == "0" || txt_fecha_fin.SelectedDate == null)
                    dfecha_fin = txt_fecha_fin.MaxDate;
                else dfecha_fin = txt_fecha_fin.SelectedDate.Value;

                if (DateTime.Compare(dfecha_inicio, dfecha_fin) == 1)
                {
                    lblmensaje.Visible = true;
                    lblmensaje.Text = "La fecha de inicio debe ser menor o igual a la fecha fin";
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                }
                else
                {



                    //dt = obj_Facade_Proceso_Operativo.Get_ReporteQuiebre(iidperson, sidplanning, sidchannel, sidcategoriaProducto, iidmarca, scodproducto, dfecha_inicio, dfecha_fin, compañia);
                    ////Conexion Ocoon = new Conexion();
                    ////dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_QUIEBRE", iidperson, sidplanning, sidchannel, sidcategoriaProducto, iidmarca, scodproducto, sfecha_inicio, sfecha_fin);
                    //gv_quibre.DataSource = dt;
                    //gv_quibre.DataBind();
                    //gv_quiebreToExcel.DataSource = dt;
                    //gv_quiebreToExcel.DataBind();

                    ////GridView2.Visible = false;
                    //lblmensaje.Visible = true;
                    //lblmensaje.Text = "Se encontro " + dt.Rows.Count + " resultados";
                    //lblmensaje.ForeColor = System.Drawing.Color.Green;

                    //icompany = Convert.ToInt32(this.Session["companyid"]);
                    //iservicio = Convert.ToInt32(this.Session["Service"]);
                    //canal = this.Session["Canal"].ToString().Trim();
                    //Report = Convert.ToInt32(this.Session["Reporte"]);

                    try
                    {


                        Rptsegnv.Visible = true;

                        Rptsegnv.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                        Rptsegnv.ServerReport.ReportPath = "/Reporte_Precios_V1/Rpt_Seguimiento_NV_SF";


                        String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                        Rptsegnv.ServerReport.ReportServerUrl = new Uri(strConnection);
                        Rptsegnv.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                        List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Personid", Convert.ToString(iidperson)));
                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idplanning", sidplanning));
                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("canal", sidchannel));

                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MOTIVONV", simotivo));
                       // parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Product", scodproducto));
                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("fec_ini", Convert.ToString(dfecha_inicio)));
                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("fec_fin", Convert.ToString(dfecha_fin)));
                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("company_id", Convert.ToString(compañia)));



                        Rptsegnv.ServerReport.SetParameters(parametros);
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

            catch (Exception ex)
            {
                Exception mensaje = ex;
                lblmensaje.Text = "";
                if (cmbplanning.SelectedIndex > 0)
                    lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
            }
        }


   

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        //private void ExportToExcel(string strFileName)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    System.IO.StringWriter sw = new System.IO.StringWriter(sb);
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);

        //    Page page = new Page();
        //    HtmlForm form = new HtmlForm();

        //    //GridView1.EnableViewState = false;
        //    //GridView1.AllowPaging = false;
        //    //gv.DataBind();

        //    page.EnableEventValidation = false;
        //    page.DesignerInitialize();
        //    page.Controls.Add(form);
        //    form.Controls.Add(gv_quiebreToExcel);
        //    page.RenderControl(htw);

        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.ContentType = "application/ms-excel";// vnd.ms-excel";
        //    Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName + ".xls");
        //    Response.Charset = "UTF-8";

        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentEncoding = System.Text.Encoding.Default;
        //    Response.Write(sb.ToString());
        //    Response.End();
        //}
        //protected void cb_all_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox cb_all = (CheckBox)gv_quibre.HeaderRow.FindControl("cb_all");
        //    bool validar = cb_all.Checked;

        //    for (int i = 0; i < gv_quibre.Rows.Count; i++)
        //    {
        //        GridViewRow row = gv_quibre.Rows[i];
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {

        //            CheckBox cb_validar = (CheckBox)row.FindControl("cb_validar");

        //            if (validar == true && cb_validar.Enabled == true)
        //            {
        //                cb_validar.Checked = true;
        //            }
        //            else if (validar == false && cb_validar.Enabled == true)
        //            {
        //                cb_validar.Checked = false;
        //            }

        //        }
        //    }
        //}
        //protected void btn_validar_Click(object sender, EventArgs e)
        //{

        //    for (int i = 0; i < gv_quibre.Rows.Count; i++)
        //    {
        //        GridViewRow row = gv_quibre.Rows[i];
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {

        //            CheckBox cb_validar = (CheckBox)row.FindControl("cb_validar");
        //            Label lbl = (Label)row.FindControl("lbl_validar");

        //            row.Cells[9].Visible = true;
        //            int id = Convert.ToInt32(row.Cells[9].Text);
        //            bool validar = cb_validar.Checked;


        //            if (validar == true)
        //            {

        //                cb_validar.Enabled = true;
        //                lbl.Text = "valido";
        //                lbl.ForeColor = System.Drawing.Color.Green;
        //            }
        //            else
        //            {
        //                update_quiebre_detalle_validado(id, validar);
        //                cb_validar.Enabled = false;
        //                lbl.Text = "invalidado";
        //                lbl.ForeColor = System.Drawing.Color.Red;
        //            }

        //        }
        //    }
        //    //cargarGrilla_Quiebre();
        //}
        protected void update_quiebre_detalle_validado(int id, bool validar)
        {

            try
            {
                Conexion Ocoon = new Conexion();
                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_QUIEBRE_DETALLE_VALIDADO", id, validar);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }





    }
}
