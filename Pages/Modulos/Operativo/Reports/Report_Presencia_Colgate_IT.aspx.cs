using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lucky.Data;
using Telerik.Web.UI;
using Lucky.CFG.Util;
using System.Threading;
using System.Net;
using Lucky.Business.Common.Application;
using Lucky.Entity.Common.Application;

namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_Presencia_Colgate_IT : System.Web.UI.Page
    {

        #region Declaracion de Variables
        public  DataSet dsElemento = null; 
        private int compañia;
        private string pais;
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        public DataTable Productos
        {
            get
            {
                if (Session["Productos"] == null)
                    return null;
                else
                    return (DataTable)Session["Productos"];
            }
            set
            {
                Session["Productos"] = value;
            }
        }
        public string TipoReporte
        {
            get
            {
                if (Session["TipoReporte"] == null)
                    return "";
                else
                    return Session["TipoReporte"].ToString();
            }
            set
            {
                Session["TipoReporte"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarCombo_Channel();
            }
        }

        void llenaNodeComercial()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_NODECOMERCIAL", cmbplanning.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                cmbCadena.DataSource = dt;
                cmbCadena.DataValueField = "NodeCommercial";
                cmbCadena.DataTextField = "commercialNodeName";
                cmbCadena.DataBind();
                cmbCadena.Items.Insert(0, new ListItem("---Todas---", "0"));

                cmbCadena.Enabled = true;
            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrilla_Reporte_Presencia();
        }

        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridView1.Visible = true;
                GridView2.Visible = true;
                GridViewExportUtil.ExportToExcelMethodThree("Reporte Presencia", this.GridView2, this.GridView1);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void cargarGrilla_Reporte_Presencia()
        {
            lblmensaje.Text = "";
            try
            {
                Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();

                DataSet ds = null;

                compañia = Convert.ToInt32(this.Session["companyid"]);

                string sidplanning = cmbplanning.SelectedValue;
                if (sidplanning == "")
                    sidplanning = "0";

                string sidchannel = cmbcanal.SelectedValue;
                if (sidchannel == "")
                    sidchannel = "0";

                int iidmercaderista = Convert.ToInt32(cmbMercaderista.SelectedValue);

                string cadena = cmbCadena.SelectedValue;
                int icod_malla;
                if (cmbMalla.Enabled)
                    icod_malla = Convert.ToInt32(cmbMalla.SelectedValue);
                else
                    icod_malla = 0;

                string sidPDV = "";
                if (sidPDV == "")
                    sidPDV = "0";

                string sidTipoReporte = cmbTipoReporte.SelectedValue;
                if (sidTipoReporte == "")
                    sidTipoReporte = "0";
                TipoReporte = sidTipoReporte;

                DateTime dfecha_inicio;
                DateTime dfecha_fin;

                #region validacion fecha if{...}
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
                #endregion
                //else if (sidTipoReporte.Equals("0"))
                //{
                //    lblmensaje.Visible = true;
                //    lblmensaje.Text = "Debe seleccionar un Tipo de Reporte.";
                //    lblmensaje.ForeColor = System.Drawing.Color.Red;
                //}
                else
                {

                    Conexion Ocoon = new Conexion();


                    if (sidTipoReporte == "03")
                    {
                        dsElemento = Ocoon.ejecutarDataSet("UP_WEBXPLORA_OPE_CONSULTA_ELEMENTOSDEVISIBILIDAD_WITH_PIVOT_IT", compañia, sidplanning, sidchannel, iidmercaderista, cadena, icod_malla, sidPDV, sidTipoReporte, dfecha_inicio, dfecha_fin);
                        gv_presencia.DataSource = dsElemento.Tables[1];
                        gv_presencia.DataBind();
                        gv_Calculos.DataSource = dsElemento.Tables[2];
                        gv_Calculos.DataBind();

                        return;
                    }
                    else
                    {


                        ds = Ocoon.ejecutarDataSet("UP_WEBXPLORA_OPE_CONSULTA_PRESENCIA_WITH_PIVOT_IT", compañia, sidplanning, sidchannel, iidmercaderista, cadena, icod_malla, sidPDV, sidTipoReporte, dfecha_inicio, dfecha_fin);
                    }

   



                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            Productos = ds.Tables[0];
                            DataTable Datos = ds.Tables[1];
                            Session["gvPresen"] = Datos.Rows.Count;
                            if (Datos.Rows.Count > 0)
                            {

                                if (TipoReporte.Equals("04") || TipoReporte.Equals("0") || TipoReporte.Equals("05"))
                                {
                                    if (ds.Tables[2] != null)
                                    {
                                        gv_Calculos.DataSource = ds.Tables[2];
                                        gv_Calculos.DataBind();
                                        GridView2.DataSource = ds.Tables[2];
                                        GridView2.DataBind();
                                    }
                                }
                                else
                                {
                                    gv_Calculos.DataSource = null;
                                    gv_Calculos.DataBind();
                                    GridView2.DataSource = null;
                                    GridView2.DataBind();
                                }



                                gv_presencia.DataSource = Datos;
                                gv_presencia.DataBind();

                                GridView1.DataSource = Datos;

                                //GridView1.DataBind();
                                gv_Presencia_DataBind();
                                lblmensaje.ForeColor = System.Drawing.Color.Green;
                                lblmensaje.Text = "Se encontró " + Datos.Rows.Count + " resultados";


                            }
                            else
                            {
                                lblmensaje.ForeColor = System.Drawing.Color.Blue;
                                lblmensaje.Text = "Se encontró " + Datos.Rows.Count + " resultados";
                            }




                        }
                        else
                        {
                            lblmensaje.ForeColor = System.Drawing.Color.Blue;
                            lblmensaje.Text = "No se encontraron resultados";
                            gv_Presencia_DataBind();
                            gv_Calculos.DataBind();
                            GridView1.DataBind();
                        }
                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Blue;
                        lblmensaje.Text = "No se encontraron resultados";
                        gv_Presencia_DataBind();
                        gv_Calculos.DataBind();
                        GridView1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
                gv_Presencia_DataBind();
                gv_Calculos.DataBind();
                GridView1.DataBind();
            }
        }

        protected void gv_Presencia_DataBind()
        {
            gv_presencia.DataBind();
            GridView1.DataBind();
        }

        protected void cargarGrilla_Presencia_toexcel()
        {
            lblmensaje.Text = "";
            try
            {
                Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();

                DataSet ds = null;

                compañia = Convert.ToInt32(this.Session["companyid"]);

                string sidplanning = cmbplanning.SelectedValue;
                if (sidplanning == "")
                    sidplanning = "0";

                string sidchannel = cmbcanal.SelectedValue;
                if (sidchannel == "")
                    sidchannel = "0";

                int iidmercaderista = Convert.ToInt32(cmbMercaderista.SelectedValue);
                int icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);

                int icod_malla;
                if (cmbMalla.Enabled)
                    icod_malla = Convert.ToInt32(cmbMalla.SelectedValue);
                else
                    icod_malla = 0;

                string sidPDV = "";
                if (sidPDV == "")
                    sidPDV = "0";

                string sidTipoReporte = "";
                if (sidTipoReporte == "")
                    sidTipoReporte = "0";

                DateTime dfecha_inicio;
                DateTime dfecha_fin;

                #region validacion fecha if{...}
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
                #endregion
                //else if (sidTipoReporte.Equals("0"))
                //{
                //    lblmensaje.Visible = true;
                //    lblmensaje.Text = "Debe seleccionar un Tipo de Reporte.";
                //    lblmensaje.ForeColor = System.Drawing.Color.Red;
                //}
                else
                {
                    Conexion Ocoon = new Conexion();
                    ds = Ocoon.ejecutarDataSet("UP_WEBXPLORA_OPE_CONSULTA_PRESENCIA_WITH_PIVOT", compañia, sidplanning, sidchannel, iidmercaderista, icod_oficina, icod_malla, sidPDV, sidTipoReporte, dfecha_inicio, dfecha_fin);
                    Productos = ds.Tables[0];
                    DataTable dt = ds.Tables[1];

                    if (dt.Rows.Count > 0)
                    {
                        gv_PresenciaToExcel.DataSource = ds;
                        //gv_PresenciaToExcel.DataSource = Productos;

                        gv_PresenciaToExcel.DataBind();
                        lblmensaje.ForeColor = System.Drawing.Color.Green;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Blue;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                        gv_PresenciaToExcel.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
                gv_presencia.DataBind();
            }
        }

        #region Eventos RadGrid

        protected void gv_presencia_CancelCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            cargarGrilla_Reporte_Presencia();
        }

        protected void gv_presencia_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            cargarGrilla_Reporte_Presencia();
        }

        protected void gv_presencia_DataBound(object sender, EventArgs e)
        {
            if (gv_presencia.Items.Count > 0)
            {
                cb_all.Visible = true;


                //lbl_cb_all.Visible = true;
            }
            else
            {
                cb_all.Visible = false;
                //lbl_cb_all.Visible = false;
            }

            //gv_presencia.Items.Count
            int col = 0;
            int col1 = 0;


            double valor = 0;
            for (int i = 0; i < gv_presencia.Items.Count; i++)
            {

                GridItem item2 = gv_presencia.Items[i];


                //a = item2.Cells[21].Text;
                //a = item2.Cells[22].Text;
                //a = item2.Cells[23].Text;
                //a = item2.Cells[24].Text;

                Boolean soles = true;
                for (int k = 1; k < (item2.Cells.Count - 20); k++)
                {
                    col = k + 20;
                    col1 = k + 3;
                    GridItem item3 = gv_Calculos.Items[0];

                    double max = 0;
                    double min = 0;

                    // item3.Cells[3].Text = item3.Cells[3].Text.Substring(1);


                   


                    //  item3.Cells[3].Text = item3.Cells[3].Text.Substring(1);
                    try
                    {
                        item3 = gv_Calculos.Items[1];
                        max = double.Parse(item3.Cells[col1].Text);
                        item3 = gv_Calculos.Items[2];
                        min = double.Parse(item3.Cells[col1].Text);
                    }
                    catch
                    {
                        max = min = 0;
                    }


                   

                    //  item3.Cells[3].Text = item3.Cells[3].Text.Substring(1);
                   



                    int tt = item3.Cells.Count;
                    int tt1 = gv_Calculos.Items.Count;


                    try
                    {

                        valor = double.Parse(item2.Cells[col].Text);

                    }
                    catch
                    {
                        valor = 0;
                    }


                    if (valor == max)
                    {
                        if (soles == true)
                        {
                            item2.Cells[col].Text = "S/." + item2.Cells[col].Text;
                        }

                        item2.Cells[col].BackColor = System.Drawing.Color.Green;
                        item2.Cells[col].ForeColor = System.Drawing.Color.White;
                        item2.Cells[col].Font.Size = 13;
                    }
                    else if (valor == min)
                    {
                        if (soles == true)
                        {
                            item2.Cells[col].Text = "S/." + item2.Cells[col].Text;
                        }


                        item2.Cells[col].BackColor = System.Drawing.Color.Red;
                        item2.Cells[col].ForeColor = System.Drawing.Color.White;
                        item2.Cells[col].Font.Size = 13;
                    }

                    if (soles == true)
                    {
                        soles = false;
                    }
                    else
                    {
                        soles = true;
                    }
                }



                // int c = item2.Cells.Count;




            }

        }

        protected void gv_presencia_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                lblmensaje.Text = "";
                Conexion Ocoon = new Conexion();
                DateTime Fec_reg_bd = DateTime.Today;

                GridItem item = gv_presencia.Items[e.Item.ItemIndex];

                CheckBox cb_validar_presencia = (CheckBox)item.FindControl("cb_validar_presencia");
                Label lbl_validar_presencia = (Label)item.FindControl("lbl_validar_presencia");
                Label lbl_id_cliente = (Label)item.FindControl("lbl_id_cliente");
                Label lbl_id_node = (Label)item.FindControl("lbl_id_node");
                Label lbl_fecha = (Label)item.FindControl("lbl_fecha");
                Label lbl_id_mercaderista = (Label)item.FindControl("lbl_id_mercaderista");

                int node = Int32.Parse(lbl_id_node.Text);
                string cliente = lbl_id_cliente.Text;
                string fecha = lbl_fecha.Text;
                int id_mercaderista = Int32.Parse(lbl_id_mercaderista.Text);
                bool validado = cb_validar_presencia.Checked;
                string Message = "";

                List<object[]> ArrayEditorValue = new List<object[]>();

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
                                object[] EditValue = new object[2];
                                EditValue[0] = column.HeaderText;
                                EditValue[1] = editorText;
                                ArrayEditorValue.Add(EditValue);
                            }
                            if (editor is GridDateTimeColumnEditor)
                            {
                                editorText = (editor as GridDateTimeColumnEditor).Text;
                                Fec_reg_bd = Convert.ToDateTime(editorText);
                            }
                        }
                    }
                }
                string selectDatosRegistro = String.Format("nodocomercial={0} and PDV_Client='{1}' and fec_reg_cel='{2}' and id_mercaderista={3}", node, cliente, fecha, id_mercaderista);
                DataRow[] Datos = Productos.Select(selectDatosRegistro);
                if (Datos.Count() > 0)
                {
                    string sId_Reg_Presencia = Datos[0]["id_detalle_presencia"].ToString();
                    Datos = null;

                    foreach (object[] value in ArrayEditorValue)
                    {
                        string updatValor = "";
                        int count = 0;
                        string cabecera = value[0].ToString();
                        string valor = value[1].ToString();

                        if (!valor.Equals(""))
                        {
                            string select = String.Format("nodocomercial={0} and PDV_Client='{1}' and fec_reg_cel='{2}' and id_mercaderista={3} and dato='{4}'", node, cliente, fecha, id_mercaderista, cabecera);
                            DataRow[] filasActualizar = Productos.Select(select);

                            if (filasActualizar.Count() == 0)
                            { //Crear el registro 
                                //string selectProducto = String.Format("dato='{4}'", node, cliente, fecha, id_mercaderista, cabecera);
                                //DataRow[] CodProducto = Productos.Select(selectProducto);
                                //string sId_Dato = CodProducto[0]["cod_dato"].ToString();
                                //CodProducto = null;
                                //OPE_Reporte_Presencia oOPE_Reporte_Presencia = new OPE_Reporte_Presencia();
                                //EOPE_Reporte_Presencia oEOPE_Reporte_Presencia = oOPE_Reporte_Presencia.RegistrarReportePresencia_Pivot(sId_Dato, sId_Reg_Presencia, "");
                                //oOPE_Reporte_Presencia.RegistrarReportePresenciaDetalle_Pivot(Convert.ToInt64(oEOPE_Reporte_Presencia.ID), sId_Dato, valor);
                            }
                            else
                            {//Actualizar el registro
                                string NomProduct = "";
                                double valors = double.Parse(valor);
                                double valorActual = 0;
                                double valorTotal = 0;
                                Array.ForEach(filasActualizar, delegate(DataRow fila)
                                {
                                    updatValor += (fila["id_detalle_presencia"].ToString() + ",");
                                    fila["validado"] = validado;
                                    valorActual = double.Parse(fila["valor"].ToString());
                                    NomProduct = fila["dato"].ToString();
                                    count++;
                                    TipoReporte = fila["opcion_reporte"].ToString();
                                    valorTotal += double.Parse(fila["valor"].ToString());
                                });
                                updatValor = updatValor.Remove(updatValor.Length - 1, 1);
                                if (valors != valorTotal)
                                {
                                    Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_PRESENCIA_COLGATE_PIVOT_DT", updatValor, TipoReporte, valor, Fec_reg_bd, Session["sUser"].ToString(), DateTime.Now, validado);
                                }
                                if (filasActualizar.Count() > 1)
                                {
                                    Message += string.Format("El producto {0} contiene {1} registros, debe invalidar algunos de ellos.<br />", NomProduct, count);
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmensaje.ForeColor = System.Drawing.Color.Blue;
                    lblmensaje.Text = "No es posible actualizar.";
                }
                cargarGrilla_Reporte_Presencia();
                lblmensaje.ForeColor = System.Drawing.Color.Blue;
                lblmensaje.Text = Message; lblmensaje.Text = Message;
            }
            catch (Exception ex)
            {
                lblmensaje.Text = ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_presencia_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            if (e.Column.UniqueName == "dateby")
            {
                GridDateTimeColumn boundColumn = e.Column as GridDateTimeColumn;
                boundColumn.ReadOnly = true;
                if (boundColumn != null)
                    boundColumn.DataFormatString = "{0:dd/MM/yy}";
            }

            else if (e.Column.UniqueName == "Place")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }



            else if (e.Column.UniqueName == "Supervisoras")
            {
                e.Column.Visible = false;
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = false;
            }
            else if (e.Column.UniqueName == "Merchandising")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "nodocomercial")
            {
                e.Column.Visible = false;
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
                //boundColumn.DataFormatString = "{0:d}";
            }
            else if (e.Column.UniqueName == "PDV_Client")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "Address")
            {
                e.Column.Visible = true;
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "Ciudad")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "Distrito")
            {
                e.Column.Visible = true;
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "Distribuidora")
            {
                e.Column.Visible = true;
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "Telefono")
            {
                e.Column.Visible = false;
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }

            else if (e.Column.UniqueName == "Dni/Ruc")
            {
                e.Column.Visible = false;
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }


            else if (e.Column.UniqueName == "CodDistrib")
            {
                e.Column.Visible = false;
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "opreport")
            {
                e.Column.Visible = false;
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "id_mercaderista")
            {
                e.Column.Visible = false;
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }

            else if (e.Column.UniqueName == "Total Dis")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "% Distb")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
                //if (boundColumn != null)
                //    boundColumn.DataFormatString = "{0:#%}";
            }
            else if (e.Column.UniqueName == "01 A 07 SKUs")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "08 A 13 SKUs")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "14 A 17 SKUs")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "18 A 20 SKUs")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "Mercado")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "PDV")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "cod_pdv")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "createby")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }

            else if (e.Column.UniqueName == "modiby")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "datemodiby")
            {
                GridDateTimeColumn boundColumn = e.Column as GridDateTimeColumn;
                boundColumn.ReadOnly = true;
                if (boundColumn != null)
                    boundColumn.DataFormatString = "{0:dd/MM/yy}";
            }
            else if (e.Column.UniqueName == "validado")
            {
                e.Column.Visible = false;
                GridCheckBoxColumn boundColumn = e.Column as GridCheckBoxColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "Periodo")
            {
                GridDateTimeColumn boundColumn = e.Column as GridDateTimeColumn;
                if (boundColumn != null)
                    boundColumn.DataFormatString = "{0:dd/MM/yy}";

            }


            else if (e.Column.UniqueName == "fecha")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }

            else if (e.Column.UniqueName == "cod_pdv")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }

            else if (e.Column.UniqueName == "pdv_nombre")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }

            else if (e.Column.UniqueName == "cadena")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }

            else if (e.Column.UniqueName == "direccion")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }

            else if (e.Column.UniqueName == "distrito")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "ciudad")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }
            else if (e.Column.UniqueName == "validado")
            {
                GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                boundColumn.ReadOnly = true;
            }   

                


            else
            {
                //GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                //if (boundColumn != null && TipoReporte.Equals("04"))
                //    boundColumn.DataFormatString = "{0:C}";
                //if (boundColumn != null && TipoReporte.Equals("05"))
                //{
                //    boundColumn.DataFormatString = "{0:C}";
                //}



                //boundColumn.DataFormatString = "{0:S/.####.##}";
            }
            #region Presencia_Colgate
            if (TipoReporte.Equals("04") || TipoReporte.Equals("0"))
            {
                if (e.Column.UniqueName == "nodocomercial_Calc")
                {
                    e.Column.Visible = false;
                    GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                    boundColumn.ReadOnly = true;
                }
                else if (e.Column.UniqueName == "PDV_Client_Calc")
                {
                    e.Column.Visible = false;
                    GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                    boundColumn.ReadOnly = true;
                }
                else if (e.Column.UniqueName == "fec_reg_cel_Calc")
                {
                    e.Column.Visible = false;
                    GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                    boundColumn.ReadOnly = true;
                }
                else if (e.Column.UniqueName == "id_mercaderista_Calc")
                {
                    e.Column.Visible = false;
                    GridBoundColumn boundColumn = e.Column as GridBoundColumn;
                    boundColumn.ReadOnly = true;
                }
            }
            #endregion
            //else if (e.Column.UniqueName == "createby")
            //{
            //    e.Column.OrderIndex = 1;
            //}
            //else if (e.Column.UniqueName == "Colgate Ultra 12 x 14")
            //{
            //    GridBoundColumn colTelephone =
            //     gv_presencia.Columns.FindByUniqueName("Colgate Ultra 12 x 14") as GridBoundColumn;
            //    colTelephone.DataFormatString = "{0:$####.##}";
            //}

            //else if (e.Column.UniqueName == "createby")
            //{

            //    "{0:$####.##}"
            //}
            ////DataFormatString="{0:C}"
        }

        protected void gv_Calculos_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            if (e.Column.UniqueName == "en_blanco0")
            {
                GridDateTimeColumn boundColumn = e.Column as GridDateTimeColumn;
                //e.Column.ItemStyle.Width = Unit.Pixel(40);
                e.Column.ItemStyle.Width = Unit.Percentage(40);
                e.Column.HeaderStyle.Width = Unit.Percentage(40);
                //e.Column.HeaderStyle.Width = Unit.Pixel(40);                
            }
            if (e.Column.UniqueName == "en_blanco1")
            {
                GridDateTimeColumn boundColumn = e.Column as GridDateTimeColumn;
                e.Column.ItemStyle.Width = Unit.Pixel(40);
                e.Column.HeaderStyle.Width = Unit.Pixel(40);
            }
            //if (e.Column.UniqueName == "Tipo")
            //{
            //    GridDateTimeColumn boundColumn = e.Column as GridDateTimeColumn;
            //}
            //else
            //{
            //    GridBoundColumn boundColumn = e.Column as GridBoundColumn;
            //    if (boundColumn != null && TipoReporte.Equals("04"))
            //    {
            //        boundColumn.DataFormatString = "{0:C}";
            //    }
            //    if (boundColumn != null && TipoReporte.Equals("05"))
            //    {
            //        boundColumn.DataFormatString = "{0:C}";
            //    }
            //}
        }

        protected void gv_presencia_ItemDataBound(object sender, GridItemEventArgs e)
        {


            if (e.Item is GridDataItem)
            {
                GridItem item = e.Item;
                CheckBox cb_validar_presencia = (CheckBox)item.FindControl("cb_validar_presencia");
                Label lbl_validar_presencia = (Label)item.FindControl("lbl_validar_presencia");
                bool valido = cb_validar_presencia.Checked;
                if (valido)
                {
                    lbl_validar_presencia.Text = "Valido";
                    lbl_validar_presencia.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lbl_validar_presencia.Text = "Invalidado";
                    lbl_validar_presencia.ForeColor = System.Drawing.Color.Red;
                }


                //e.Item.Cells[14].BackColor = System.Drawing.Color.Green;

                //if (TipoReporte.Equals("04") || TipoReporte.Equals("0"))
                //{
                //    GridDataItem itemData = (GridDataItem)e.Item;
                //    TableCell cell = itemData["% Distb"];
                //    if (cell != null && !cell.Text.Equals("&nbsp;"))
                //        cell.Text = String.Format("{0} %", Math.Round(Convert.ToDouble(cell.Text) * 100, 2));
                //    //cell.Text = String.Format("S/. {0}", cell.Text);
                //}
            }
        }

        #endregion

        #region Llena Combos

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
                cmbcanal.Items.Insert(0, new ListItem("---Todas---", "0"));
            }
        }

        protected void CargarCombo_Malla()
        {
            try
            {
                DataTable dt = null;
                Conexion Ocoon = new Conexion();

                compañia = Convert.ToInt32(this.Session["companyid"]);

                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OBTENER_MALLA_BY_CHANNEL_AND_CLIENTE", cmbcanal.SelectedValue, compañia);
                if (dt.Rows.Count > 0)
                {
                    cmbMalla.Enabled = true;
                    cmbMalla.DataSource = dt;
                    cmbMalla.DataValueField = "id_malla";
                    cmbMalla.DataTextField = "malla";
                    cmbMalla.DataBind();
                    cmbMalla.Items.Insert(0, new ListItem("---Todas---", "0"));
                }
            }
            catch (Exception)
            {
            }
        }



        protected void CargarCombo_Mercaderistas(string sidplanning)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);

            cmbMercaderista.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                cmbMercaderista.DataSource = dt;
                cmbMercaderista.DataValueField = "Person_id";
                cmbMercaderista.DataTextField = "Person_NameComplet";
                cmbMercaderista.DataBind();
                cmbMercaderista.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbMercaderista.Enabled = true;
            }
        }

        protected void cargarCombo_Oficina()
        {
            try
            {
                Conexion Ocoon = new Conexion();

                if (this.Session["companyid"] != null)
                {
                    compañia = Convert.ToInt32(this.Session["companyid"]);
                    DataTable dtofi = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENEROFICINAS", compañia);

                    if (dtofi.Rows.Count > 0)
                    {
                        cmbOficina.Enabled = true;
                        cmbOficina.DataSource = dtofi;
                        cmbOficina.DataTextField = "Name_Oficina";
                        cmbOficina.DataValueField = "cod_Oficina";
                        cmbOficina.DataBind();

                        cmbOficina.Items.Insert(0, new ListItem("---Todas---", "0"));
                    }
                }
                else
                {
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Eventos de Combos


        protected void cmbplanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMercaderista.Items.Clear();
            cmbMercaderista.Enabled = false;

            cmbOficina.Items.Clear();
            cmbOficina.Enabled = false;

            cmbMalla.Items.Clear();
            cmbMalla.Enabled = false;



            string sidplanning = cmbplanning.SelectedValue;
            CargarCombo_Mercaderistas(sidplanning);
            cargarCombo_Oficina();
            llenaNodeComercial();
            CargarCombo_Malla();
            CargarCombo_TipoReporte(sidplanning);

        }

        protected void cmbcanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            compañia = Convert.ToInt32(this.Session["companyid"]);

            string sidchannel = cmbcanal.SelectedValue;

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);

            cmbMercaderista.Items.Clear();
            cmbMercaderista.Enabled = false;

            cmbOficina.Items.Clear();
            cmbOficina.Enabled = false;

            cmbMalla.Items.Clear();
            cmbMalla.Enabled = false;



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


        #endregion

        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            bool validar = cb_all.Checked;

            for (int i = 0; i < gv_presencia.Items.Count; i++)
            {
                GridItem item = gv_presencia.Items[i];
                CheckBox cb_validar_presencia = (CheckBox)item.FindControl("cb_validar_presencia");
                Label lbl_validar_presencia = (Label)item.FindControl("lbl_validar_presencia");
                lbl_validar_presencia.Text = "";
                if (cb_validar_presencia.Enabled == true)
                {
                    if (validar == true)
                    {
                        cb_validar_presencia.Checked = true;
                        //lbl_validar_presencia.Text = "Valido";
                        //lbl_validar_presencia.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (validar == false)
                    {
                        cb_validar_presencia.Checked = false;
                        //lbl_validar_presencia.Text = "Invalidado";
                        //lbl_validar_presencia.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void update_stock_detalle_validado_masa(string idregPre, bool validar, string opcionreporte)
        {
            try
            {
                Conexion OCoon = new Conexion();
                OCoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_PRESENCIA_VARIOS_COLGATE", idregPre, validar, opcionreporte, Session["sUser"].ToString(), DateTime.Now);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btn_validar_Pres_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gv_presencia.Items.Count; i++)
            {
                GridItem item = gv_presencia.Items[i];

                CheckBox cb_validar_presencia = (CheckBox)item.FindControl("cb_validar_presencia");
                Label lbl_validar_presencia = (Label)item.FindControl("lbl_validar_presencia");
                Label lbl_id_cliente = (Label)item.FindControl("lbl_id_cliente");
                Label lbl_id_node = (Label)item.FindControl("lbl_id_node");
                Label lbl_fecha = (Label)item.FindControl("lbl_fecha");
                Label lbl_id_mercaderista = (Label)item.FindControl("lbl_id_mercaderista");

                int node = Int32.Parse(lbl_id_node.Text);
                string cliente = lbl_id_cliente.Text;
                string fecha = lbl_fecha.Text;
                int id_mercaderista = Int32.Parse(lbl_id_mercaderista.Text);

                string select = String.Format("nodocomercial={0} and PDV_Client='{1}'  and fec_reg_cel='{2}' and id_mercaderista={3}", node, cliente, fecha, id_mercaderista);

                DataRow[] filasActualizar = Productos.Select(select);

                if (cb_validar_presencia.Enabled == true && filasActualizar != null && filasActualizar.Length > 0)
                {
                    bool validar = cb_validar_presencia.Checked;
                    if (validar != Convert.ToBoolean(filasActualizar[0]["validado"].ToString()))
                    {
                        string UpdateMasa = "";
                        Array.ForEach(filasActualizar, delegate(DataRow fila)
                        {
                            UpdateMasa += (fila["id_detalle_presencia"].ToString() + ",");
                            fila["validado"] = validar;
                        });
                        UpdateMasa = UpdateMasa.Remove(UpdateMasa.Length - 1, 1);
                        update_stock_detalle_validado_masa(UpdateMasa, validar, "");
                    }
                    if (validar)
                    {
                        lbl_validar_presencia.Text = "Valido";
                        lbl_validar_presencia.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_validar_presencia.Text = "Invalidado";
                        lbl_validar_presencia.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
        }

        protected void gv_presencia_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

        }

        protected void gv_Calculos_DataBound(object sender, EventArgs e)
        {

            if (TipoReporte != "03")
            {
                Boolean status = true;

                for (int i = 0; i < gv_Calculos.Items.Count; i++)
                {

                    GridItem item = gv_Calculos.Items[i];

                    //item.Cells[3].Text = item.Cells[3].Text.Substring(1);



                    if (status == false)
                    {
                        for (int j = 5; j < item.Cells.Count; j++)
                        {
                            item.Cells[j].Text = "";

                            j = j + 1;

                        }

                    }

                    status = false;
                }
            }

        }

        protected void CargarCombo_TipoReporte(string sidplanning)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_TIPO_REPORTE_REPORT_PRESENCIA", sidplanning);
            //cmbcategoria_producto.Enabled = false;
            //cmbfamilia.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                cmbTipoReporte.DataSource = dt;
                cmbTipoReporte.DataValueField = "ID_OPCIONPRESENCIA";
                cmbTipoReporte.DataTextField = "TipoReporte_Descripcion";
                cmbTipoReporte.DataBind();
                cmbTipoReporte.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbTipoReporte.Enabled = true;
            }
        }
    }
}