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
	public partial class Rpt_Segmentacion : System.Web.UI.Page
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

        protected void cmbcanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            string sidchannel = cmbcanal.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);

            cmbplanning.Items.Clear();
            cmbcategoria_producto.Items.Clear();
            cmbcategoria_producto.Enabled = false;
            cmbmarca.Items.Clear();
            cmbmarca.Enabled = false;
            cmbsku.Items.Clear();
            cmbsku.Enabled = false;
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
            lblmensaje.Text = "Cargando...";
            //-------
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            string sidplanning = cmbplanning.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);

            cmbcategoria_producto.Items.Clear();
            cmbmarca.Items.Clear();
            cmbmarca.Enabled = false;
            cmbsku.Items.Clear();
            cmbsku.Enabled = false;
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

            cargarCombo_CategoriaDeproducto(sidplanning);
            //----------------------------------------------------
            //cargarGrilla_Quiebre();
        }
        protected void cargarCombo_CategoriaDeproducto(string sidplanning)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_QUIEBRE", sidplanning);
            cmbcategoria_producto.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                cmbcategoria_producto.DataSource = dt;
                cmbcategoria_producto.DataValueField = "id_ProductCategory";
                cmbcategoria_producto.DataTextField = "Product_Category";
                cmbcategoria_producto.DataBind();
                cmbcategoria_producto.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                cmbcategoria_producto.Enabled = true;
            }
        }
        protected void cmbcategoria_producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            //string iischannel = cmbcanal.SelectedValue;
            //string sidplanning = cmbplanning.SelectedValue;
            string siproductCategory = cmbcategoria_producto.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_REPORT_QUIEBRE", siproductCategory);

            cmbmarca.Items.Clear();
            cmbmarca.Enabled = false;
            cmbsku.Items.Clear();
            cmbsku.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                cmbmarca.DataSource = dt;
                cmbmarca.DataValueField = "id_Brand";
                cmbmarca.DataTextField = "Name_Brand";
                cmbmarca.DataBind();
                cmbmarca.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbmarca.Enabled = true;
            }
            //cargarGrilla_Quiebre();

        }
        protected void cmbmarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            //string iischannel = cmbcanal.SelectedValue;
            //string sidplanning = cmbplanning.SelectedValue;
            string sidmarca = cmbmarca.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SKU_PRODUCTO_REPORT_QUIEBRE", sidmarca);

            cmbsku.Items.Clear();
            cmbsku.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                cmbsku.DataSource = dt;
                cmbsku.DataValueField = "cod_Product";
                cmbsku.DataTextField = "productoNombre";
                cmbsku.DataBind();
                cmbsku.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbsku.Enabled = true;
            }
            //cargarGrilla_Quiebre();
        }
       
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
                string sidcategoriaProducto = cmbcategoria_producto.SelectedValue;
                if (sidcategoriaProducto == "")
                    sidcategoriaProducto = "0";
                string scodproducto = cmbsku.SelectedValue;
                if (scodproducto == "")
                    scodproducto = "0";
                string sidmarca = cmbmarca.SelectedValue;
                if (sidmarca == "")
                    sidmarca = "0";
                int iidmarca = Convert.ToInt32(sidmarca);

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

                        Rptsegm.Visible = true;

                        Rptsegm.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                        Rptsegm.ServerReport.ReportPath = "/Reporte_Precios_V1/SegmentacionSF";


                        String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                        Rptsegm.ServerReport.ReportServerUrl = new Uri(strConnection);
                        Rptsegm.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                        List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Person_id", Convert.ToString(iidperson)));
                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_Planning", sidplanning));
                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CodChannel", sidchannel));
                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductCategory", sidcategoriaProducto));
                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_Brand", sidmarca));
                       parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Product", scodproducto));
                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("f_inicio",  Convert.ToString(dfecha_inicio)));
                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("f_fin", Convert.ToString(dfecha_fin)));
                        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("company_id",Convert.ToString(compañia)));
                       


                        Rptsegm.ServerReport.SetParameters(parametros);
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


        protected void gv_quibre_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gv_quibre.PageIndex = e.NewPageIndex;
            //cargarGrilla_Quiebre();
        }

        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                //gv_quiebreToExcel.Visible = true;
                //ExportToExcel("Reporte_Quiebre");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
