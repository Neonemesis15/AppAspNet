using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Lucky.Data;
using Lucky.Business.Common.Application;
using System.Text;
using System.IO;
using Telerik.Web.UI;
using System.Threading;
using Lucky.CFG.Util;
using Lucky.Entity.Common.Security;
namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_Alicorp_DataValidada : System.Web.UI.Page
    {

        #region Declaracion de variables generales

        private int compañia;
        private string pais;

        Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                CargarCombo_Channel();
               
                cargarAño();
                cargarMes();
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
            compañia = Convert.ToInt32(this.Session["companyid"]);
            string sidchannel = cmbcanal.SelectedValue;

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);

            cmbplanning.Items.Clear();

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


        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }
        protected void BtnDetalle_Click(object sender, EventArgs e)
        {
            DataSet ds;
            Conexion cn = new Conexion();
            ds = cn.ejecutarDataSet("SP_REPORTE_MOD_VALIDADO", cmbplanning.SelectedValue, cmbPeriodo.SelectedValue);

            gvValidado.DataSource = ds.Tables[0];
            gvValidado.DataBind();
        }
        

        protected void cargarGrilla()
        {
            DataSet ds;
           Conexion cn= new Conexion();
           ds = cn.ejecutarDataSet("SP_REPORTE_MOD_VALIDADO",cmbplanning.SelectedValue,cmbPeriodo.SelectedValue);

           gvValidado.DataSource = ds.Tables[1];
           gvValidado.DataBind();
            
        }


        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                gv_layoutToExcel.Visible = true;
                GridViewExportUtil.ExportToExcelMethodTwo("Resporte_Layout", this.gv_layoutToExcel);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }









        #region cargamasiva



        public void cargarAño()
        {
            DataTable dty = null;
            dty = Facd_ProcAdmin.Get_ObtenerYears();
            if (dty.Rows.Count > 0)
            {
                cmbAño.DataSource = dty;
                cmbAño.DataValueField = "Years_Number";
                cmbAño.DataTextField = "Years_Number";
                cmbAño.DataBind();

            }
            else
            {

                dty = null;

            }
        }

        public void cargarMes()
        {
            DataTable dtm = Facd_ProcAdmin.Get_ObtenerMeses();

            if (dtm.Rows.Count > 0)
            {
                cmbMes.DataSource = dtm;
                cmbMes.DataValueField = "codmes";
                cmbMes.DataTextField = "namemes";
                cmbMes.DataBind();

            }
            else
            {
                dtm = null;

            }
        }




        protected void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt;

           Conexion cn= new Conexion();
           dt = cn.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS_POR_PLANNING", cmbplanning.SelectedValue, 1562, 28,cmbAño.SelectedValue, cmbMes.SelectedValue);

         if (dt.Rows.Count > 0)
            {
                cmbPeriodo.DataSource = dt;
                cmbPeriodo.DataValueField = "id_ReportsPlanning";
                cmbPeriodo.DataTextField = "Periodo";
                cmbPeriodo.DataBind();

                //cmbPeriodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));


            }
        }

        #endregion

        #region excel
        private void CreaExcel(DataSet ds)
        {
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbooks oLibros = default(Microsoft.Office.Interop.Excel.Workbooks);
            Microsoft.Office.Interop.Excel.Workbook oLibro = default(Microsoft.Office.Interop.Excel.Workbook);

            Microsoft.Office.Interop.Excel.Sheets oHojas = default(Microsoft.Office.Interop.Excel.Sheets);

            Microsoft.Office.Interop.Excel.Worksheet oHoja = default(Microsoft.Office.Interop.Excel.Worksheet);


            Microsoft.Office.Interop.Excel.Range oCeldas = default(Microsoft.Office.Interop.Excel.Range);

            try
            {


                string sFile = null;
                string sTemplate = null;

                // Usamos una plantilla para crear el nuevo excel

                sFile = Server.MapPath("masivo") + "\\" + "DATOS_CARGA_REPORTE_LAYOUT.xls";

                sTemplate = Server.MapPath("masivo/Template.xls");

                lblmensaje.Visible = true;
                lblmensaje.Text = sTemplate;
                lblmensaje.ForeColor = System.Drawing.Color.Blue;

                //oExcel.Visible = false;

                //oExcel.DisplayAlerts = false;

                //// Abrimos un nuevo libro

                //oLibros = oExcel.Workbooks;

                //oLibros.Open(sTemplate);

                //oLibro = oLibros.Item[1];

                //oHojas = oLibro.Worksheets;


                //for (int i = 0; i < ds.Tables.Count; i++)
                //{
                //    oHoja = (Microsoft.Office.Interop.Excel.Worksheet)oHojas.Item[i + 1];

                //    oHoja.Name = "Hoja" + (i + 1);


                //    string col = "";
                //    int columnas = ds.Tables[i].Columns.Count;
                //    switch (columnas)
                //    {
                //        case 1:
                //            col = "A";
                //            break;
                //        case 2:
                //            col = "B";
                //            break;
                //        case 3:
                //            col = "C";
                //            break;
                //        case 4:
                //            col = "D";
                //            break;
                //        case 5:
                //            col = "E";
                //            break;
                //        case 6:
                //            col = "F";
                //            break;

                //    }



                //    oCeldas = oHoja.Cells;
                //    //oHoja.Range["B2"].Interior.Color = 0;
                //    //oHoja.Range["B2"].Font.Color = 16777215;
                //    oHoja.Range["A2", col + "2"].Interior.Color = 0;
                //    oHoja.Range["A2", col + "2"].Font.Color = 16777215;


                //    oHoja.Range["A2", col + "2"].Font.Bold = true;
                //    //oHoja.Range["A2"].Font.Bold = true;

                //    oHoja.Range["A2", col + (ds.Tables[i].Rows.Count + 2).ToString()].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDash;
                //    oHoja.Range["A2", col + (ds.Tables[i].Rows.Count + 2).ToString()].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;




                //    VuelcaDatos(ds.Tables[i], oCeldas);


                //}



                //oHoja.SaveAs(sFile);

                //oLibro.Close();

                //// Eliminamos lo que hemos creado

                //oExcel.Quit();

                //oExcel = null;

                //oLibros = null;

                //oLibro = null;

                //oHojas = null;

                //oHoja = null;

                //oCeldas = null;

                //System.GC.Collect();

            }
            catch
            {
                //oLibro.Close();
                //oExcel.Quit();

                // lblmensaje.Text = "se cargo correctamente";
                //Pmensaje.CssClass = "MensajesSupervisor";
                //lblencabezado.Text = "Sr. Usuario";
                //lblmensajegeneral.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                //Mensajes_Usuario();
            }

        }

        private void VuelcaDatos(DataTable tabla, Microsoft.Office.Interop.Excel.Range oCells)
        {

            DataRow dr = null;
            object[] ary = null;

            int iRow = 0;
            int iCol = 0;

            // Sacamos las cabeceras


            for (iCol = 0; iCol <= tabla.Columns.Count - 1; iCol++)
            {
                oCells[2, iCol + 1] = tabla.Columns[iCol].ToString();


            }


            // Sacamos los datos


            for (iRow = 0; iRow <= tabla.Rows.Count - 1; iRow++)
            {
                dr = tabla.Rows[iRow];

                ary = dr.ItemArray;


                for (iCol = 0; iCol <= ary.GetUpperBound(0); iCol++)
                {
                    oCells[iRow + 3, iCol + 1] = ary[iCol].ToString();

                }

            }

        }





        #endregion




    }
}