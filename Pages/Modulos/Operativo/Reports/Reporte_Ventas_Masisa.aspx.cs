using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Data;
using Lucky.Business.Common.Application;
using System.Text;
using System.IO;
using Telerik.Web.UI;
using System.Threading;
using Lucky.CFG.Util;

namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_Ventas_Masisa : System.Web.UI.Page
    {
        #region Declaracion de Campañas
        private int compañia;
        private string pais;
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Años();
                CargarCombo_Channel();
                cmb_mes.Enabled = false;

                calendar_day.Enabled = false;
                cmb_periodo.Enabled = false;
            }
        }

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

                cmb_año.DataSource = dty;
                cmb_año.DataValueField = "Years_Number";
                cmb_año.DataTextField = "Years_Number";
                cmb_año.DataBind();
                cmb_año.Items.Insert(0, new ListItem("--Todos--", "0"));
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
                cmb_mes.Items.Insert(0, new ListItem("--Todos--", "0"));
                cmb_mes.Enabled = true;
            }
            else
            {
                dtm = null;

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
                //cmbcanal.SelectedIndex=1;
            }            
        }

        protected void CargarCombo_Pdv()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            string scanal = cmbcanal.SelectedValue;
                if (scanal == "")
                    scanal = "0";
                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idchannel", scanal);
            if (dt.Rows.Count > 0)
            {
                cmb_pdv.DataSource = dt;
                cmb_pdv.DataValueField = "ClientPDV_Code";
                cmb_pdv.DataTextField = "pdv_Name"; 
                cmb_pdv.DataBind();
                cmb_pdv.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                cmb_pdv.Enabled = true;
            }
        }

        private void Llenar_Periodos()
        {
            Conexion Ocoon = new Conexion();
            DataTable dtp = null;
            int company = Convert.ToInt32(this.Session["companyid"]);
            string scanal = cmbcanal.SelectedValue;
            if (scanal == "")
                scanal = "0";
            dtp = Ocoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS_Masisa", scanal, company, 28, cmb_mes.SelectedValue);
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
                cmb_periodo.Items.Insert(0, new ListItem("--Todos--", "0"));
                cmb_periodo.Enabled = true;
            }
            else
            {
                dtp = null;
            }
        }

        private void LLenarDiasxPerido()
        {
            Conexion oCoon = new Conexion();
            DataTable dtp = null;
            compañia = Convert.ToInt32(this.Session["companyid"]);
            string scanal = cmbcanal.SelectedValue;
            if (scanal == "")
                scanal = "0";
           // dtp = Get_Administrativo.Get_obtener_Dias_Periodo(compañia, scanal, 28, cmb_año.SelectedValue, cmb_mes.SelectedValue, Convert.ToInt32(cmb_periodo.SelectedValue));
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_DIAS_DEL_PERIODOS_Masisa", scanal, compañia, 28, cmb_año.SelectedValue, cmb_mes.SelectedValue, Convert.ToInt32(cmb_periodo.SelectedValue));
            if (dtp.Rows.Count > 0)
            {
                calendar_day.Enabled = true;
                calendar_day.Calendar.RangeMinDate = Convert.ToDateTime(dtp.Rows[0]["fechaInicial"]);
                calendar_day.Calendar.RangeMaxDate = Convert.ToDateTime(dtp.Rows[0]["fechaFinal"]);
                calendar_day.Calendar.OutOfRangeDayStyle.BackColor = System.Drawing.Color.Magenta;
                //calendar_day.EnableTyping = false;

            }
            else
            {
                dtp = null;
            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrilla_Ventas();
        }
        protected void cargarGrilla_Ventas()
        {
            lblmensaje.Text = "";
            try
            {

                Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();

                DataTable dt = null;
                compañia = Convert.ToInt32(this.Session["companyid"]);
                string sAño = cmb_año.SelectedValue;
                if (sAño == "")
                    sAño = "0";
                string sMes = cmb_mes.SelectedValue;
                if (sMes == "")
                    sMes = "0";
                string sDia =    calendar_day.SelectedDate.ToString();
                if (sDia == "")
                    sDia = "0";
                string sPeriodo = cmb_periodo.SelectedValue;
                if (sPeriodo == "")
                    sPeriodo = "0";
                string scanal = cmbcanal.SelectedValue;
                if (scanal == "")
                    scanal = "0";
                string sPdv = cmb_pdv.SelectedValue;
                if (sPdv == "")
                    sPdv = "0";



                    Conexion Ocoon = new Conexion();

                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REPORTE_VENTA_MASISA", scanal, "254", compañia, sPdv, sAño, sMes, sDia, sPeriodo);

                    if (dt.Rows.Count > 0)
                    {
                        gv_ventas.DataSource = dt;
                        gv_ventas.DataBind();


                        gv_stockToExcel.DataSource = dt;
                        gv_stockToExcel.DataBind();

                        lblmensaje.ForeColor = System.Drawing.Color.Green;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Blue;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                        gv_ventas.DataBind();
                    }
                

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
                gv_ventas.DataBind();

                //System.Threading.Thread.Sleep(8000);
                //Response.Redirect("~/err_mensaje_seccion.aspx", true);

            }

        }
        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                gv_stockToExcel.Visible = true;
                GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Ventas", this.gv_stockToExcel);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        
        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            //// GridHeaderItem header = "";

            ////CheckBox cb_all = (CheckBox)gv_ventas.FindControl("cb_all");
            //bool validar = cb_all.Checked;

            //for (int i = 0; i < gv_ventas.Items.Count; i++)
            //{
            //    GridItem item = gv_ventas.Items[i];
            //    //if (item.ItemType == GridItemType.Item)
            //    //{

            //    CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");

            //    if (cb_validar.Enabled == true)
            //    {
            //        if (validar == true)
            //        {
            //            cb_validar.Checked = true;
            //        }
            //        else if (validar == false)
            //        {
            //            cb_validar.Checked = false;
            //        }
            //    }

            //}
        }
        protected void btn_validar_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < gv_ventas.Items.Count; i++)
            {
                GridItem item = gv_ventas.Items[i];
                // if (item.ItemType == GridItemType.Item)
                //{

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");

                Label lbl_validar = (Label)item.FindControl("lbl_validar");
                Label lbl_id_StockDetalle = (Label)item.FindControl("lbl_id_StockDetalle");


                int id = Convert.ToInt32(lbl_id_StockDetalle.Text);
                bool validar = cb_validar.Checked;

                update_stock_detalle_validado(id, validar);
                if (validar == true)
                {
                    lbl_validar.Text = "valido";
                    lbl_validar.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lbl_validar.Text = "invalidado";
                    lbl_validar.ForeColor = System.Drawing.Color.Red;
                }

            }

            //cargarGrilla_Competencias();
        }
        protected void update_stock_detalle_validado(int id, bool validar)
        {

            try
            {
                Conexion Ocoon = new Conexion();

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_STOCK_DETALLE_VALIDADO", id, validar);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        //Codigo para Ordenar y Paginar la grilla de "precios"------------------
        public string SortExpression
        {
            get { return (ViewState["SortExpression"] == null ? string.Empty : ViewState["SortExpression"].ToString()); }
            set { ViewState["SortExpression"] = value; }
        }

        public string SortDirection
        {
            get { return (ViewState["SortDirection"] == null ? string.Empty : ViewState["SortDirection"].ToString()); }
            set { ViewState["SortDirection"] = value; }
        }

        private string GetSortDirection(string sortExpression)
        {
            if (SortExpression == sortExpression)
            {
                if (SortDirection == "ASC")
                    SortDirection = "DESC";
                else if (SortDirection == "DESC")
                    SortDirection = "ASC";
                return SortDirection;
            }
            else
            {
                SortExpression = sortExpression;
                SortDirection = "ASC";
                return SortDirection;
            }
        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = gv_ventas.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                gv_ventas.DataSource = dataView;
                gv_ventas.DataBind();
            }

        }
                
        protected void gv_ventas_EditCommand(object source, GridCommandEventArgs e)
        {
            cargarGrilla_Ventas();
        }

        protected void gv_ventas_CancelCommand(object source, GridCommandEventArgs e)
        {
            cargarGrilla_Ventas();
        }

        protected void gv_ventas_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            cargarGrilla_Ventas();
        }

        protected void gv_ventas_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                lblmensaje.Text = "";
                Conexion Ocoon = new Conexion();

                GridItem item = gv_ventas.Items[e.Item.ItemIndex];

                Label lbl_id_StockDetalle = (Label)item.FindControl("lbl_id_StockDetalle");
                int iid_det = Convert.ToInt32(lbl_id_StockDetalle.Text.Trim());

                CheckBox ckvalidado = (CheckBox)item.FindControl("cb_validar");



                List<object> ArrayEditorValue = new List<object>();

                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditManager editMan = editedItem.EditManager;

                foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
                {
                    if (column is IGridEditableColumn)
                    {
                        IGridEditableColumn editableCol = (column as IGridEditableColumn);
                        if (editableCol.IsEditable)
                        {
                            IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);

                            string editorType = editor.ToString();
                            string editorText = "unknown";
                            object editorValue = null;

                            if (editor is GridNumericColumnEditor)
                            {
                                editorText = (editor as GridNumericColumnEditor).Text;
                                editorValue = (editor as GridNumericColumnEditor).NumericTextBox.DbValue;
                                ArrayEditorValue.Add(editorValue);
                            }

                            if (editor is GridDateTimeColumnEditor)
                            {
                                editorText = (editor as GridDateTimeColumnEditor).Text;
                                editorValue = (editor as GridDateTimeColumnEditor).PickerControl;
                                ArrayEditorValue.Add(editorValue);
                            }
                        }
                    }
                }

                string venta = ArrayEditorValue[0].ToString();
                DateTime Fec_reg_bd = Convert.ToDateTime((ArrayEditorValue[1] as RadDateTimePicker).SelectedDate);

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_VENTAS_MASISA", iid_det, venta, Fec_reg_bd, Session["sUser"].ToString(), DateTime.Now, ckvalidado.Checked);

                cargarGrilla_Ventas();
            }
            catch (Exception ex)
            {
                lblmensaje.Text = ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_ventas_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            gv_ventas.CurrentPageIndex = e.NewPageIndex;
            cargarGrilla_Ventas();
        }
        
        protected void gv_ventas_DataBound(object sender, EventArgs e)
        {
            //if (gv_ventas.Items.Count > 0)
            //{
            //    cb_all.Visible = true;
            //    lbl_cb_all.Visible = true;

            //}
            //else
            //{
            //    cb_all.Visible = false;
            //    lbl_cb_all.Visible = false;
            //}
        }

        protected void cmb_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LLenarDiasxPerido();
        }

        protected void cmbPeriodoxMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Llenar_Periodos();
        }

        protected void cmb_año_SelectedIndexChanged(object sender, EventArgs e)
        {
            Llena_Meses();
        }

        protected void cmbcanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCombo_Pdv();
        }
    }
}