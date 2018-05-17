﻿using System;
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


namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_Presencia_Colg : System.Web.UI.Page
    {

        #region Declaracion de Campañas
        private int compañia;
        private string pais;
        private static string static_channel;
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        #endregion 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                CargarCombo_Channel();

            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {

            cargarGrilla_Stock_Ingresos();
        }
        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //Modificado Por: Pablo Salas. Fecha:05/09/2011. Resumen: Se inhabilita el metodo cargarGrilla_Stock_Ingresostoexcel() para cargar todos los registros indiferente si son validados o invalidos.
                //cargarGrilla_Stock_Ingresostoexcel();
                //cargarGrilla_Stock_Ingresos();
                GridView1.Visible = true;
                //gv_stockToExcel.Visible = true;
                //GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Stock", this.gv_stockToExcel);
                GridViewExportUtil.ExportToExcelMethodTwo("Reporte Stock-Ingresos", this.GridView1);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void cargarGrilla_Stock_Ingresos()
        {
            lblmensaje.Text = "";
            try
            {

                Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();

                DataTable dt = null;

                #region person
                int iidperson = 0;
                if (cmbperson.SelectedIndex >= 0)
                    iidperson = Convert.ToInt32(cmbperson.SelectedValue);
                #endregion

                int iidmercaderista = 0;
                if (cmbMercaderista.SelectedIndex >= 0)
                    iidmercaderista = Convert.ToInt32(cmbMercaderista.SelectedValue);


                string sidplanning = cmbplanning.SelectedValue;
                string sidchannel = cmbcanal.SelectedValue;
                int icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);
                int iidNodeComercial = Convert.ToInt32(cmbNodeComercial.SelectedValue);
                #region pdv
                string sidPDV = cmbPuntoDeVenta.SelectedValue;
                if (sidPDV == "")
                    sidPDV = "0";
                #endregion
                #region categoria
                string sidcategoriaProducto = cmbcategoria_producto.SelectedValue;
                if (sidcategoriaProducto == "")
                    sidcategoriaProducto = "0";
                #endregion
                #region marca
                string sidmarca = cmbmarca.SelectedValue;
                if (sidmarca == "")
                    sidmarca = "0";
                #endregion

                //string iidcorporacion = cmbcorporacion.SelectedValue;
                //if (iidcorporacion == "")
                //    iidcorporacion = "0";

                string sidTipoReporte = cmbTipo_reporte.SelectedValue;
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
                else
                {
                    //Ing Ditmar Estrada 20/01/2011
                    //dt = obj_Facade_Proceso_Operativo.Get_ReportePrecio(iidperson, sidplanning, sidchannel, sidcategoriaProducto, sidmarca, scodproducto, dfecha_inicio, dfecha_fin);
                    //este metodo se suspendio temporalmente hasta corregir la data de caracteres especiales, por miestras 
                    //se usara el metodo ejecutarDataTable que se muestra acontinuacion.

                    Conexion Ocoon = new Conexion();

                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_INGRESOS", dfecha_inicio, dfecha_fin, sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidmarca, sidcategoriaProducto, iidperson);
                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_INGRESOS3", dfecha_inicio, dfecha_fin, sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidmarca, sidcategoriaProducto, iidperson);
                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_INGRESOS4", dfecha_inicio, dfecha_fin, sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidmarca, sidcategoriaProducto, iidperson);

                    //******Modificado por: Pablo Salas*****///
                    //******Fecha:07/09/2011******////
                    //******Resumen:Muestra el Reporte diario de las Ventas dentro de un rango de Fechas
                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_PRESENCIA", sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, "0", "0", iidperson, iidmercaderista, sidTipoReporte, dfecha_inicio, dfecha_fin);// sidcategoriaProducto, sidmarca, iidperson);
                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_PRESENCIA", "813622482010", 1000, 9, 4475, 0, 0, 0, 8762, 9087, "05", 10/08/2011, 11/08/2011);// sidcategoriaProducto, sidmarca, iidperson);


                    if (dt.Rows.Count > 0)
                    {
                        gv_stock.DataSource = dt;
                        gv_stock.DataBind();

                        //Modificado por: Pablo Salas. Fecha:05/09/2011. Resumen: Se exporta todos los registros sean los validados e invalidados.
                        //gv_stockToExcel.DataSource = dt;
                        //gv_stockToExcel.DataBind();
                        GridView1.DataSource = dt;
                        GridView1.DataBind();

                        lblmensaje.ForeColor = System.Drawing.Color.Green;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";


                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Blue;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                        gv_stock.DataBind();
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
                gv_stock.DataBind();

                //System.Threading.Thread.Sleep(8000);
                //Response.Redirect("~/err_mensaje_seccion.aspx", true);

            }

        }

        //Pablo Salas.
        //Fecha:06/09/2011
        //Resumen: Exporta a Excel sólo los registros validos.
        //No se esta usando ya que se solicito que exporte todos los registros.
        protected void cargarGrilla_Stock_Ingresostoexcel()
        {
            lblmensaje.Text = "";
            try
            {

                Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();

                DataTable dt = null;

                int iidperson = 0;
                if (cmbperson.SelectedIndex >= 0)
                    iidperson = Convert.ToInt32(cmbperson.SelectedValue);

                string sidplanning = cmbplanning.SelectedValue;
                string sidchannel = cmbcanal.SelectedValue;

                int icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);

                int iidNodeComercial = Convert.ToInt32(cmbNodeComercial.SelectedValue);

                string sidPDV = cmbPuntoDeVenta.SelectedValue;
                if (sidPDV == "")
                    sidPDV = "0";

                string sidcategoriaProducto = cmbcategoria_producto.SelectedValue;
                if (sidcategoriaProducto == "")
                    sidcategoriaProducto = "0";

                string sidmarca = cmbmarca.SelectedValue;
                if (sidmarca == "")
                    sidmarca = "0";

                //string iidcorporacion = cmbcorporacion.SelectedValue;
                //if (iidcorporacion == "")
                //    iidcorporacion = "0";



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
                    //Ing Ditmar Estrada 20/01/2011
                    //dt = obj_Facade_Proceso_Operativo.Get_ReportePrecio(iidperson, sidplanning, sidchannel, sidcategoriaProducto, sidmarca, scodproducto, dfecha_inicio, dfecha_fin);
                    //este metodo se suspendio temporalmente hasta corregir la data de caracteres especiales, por miestras 
                    //se usara el metodo ejecutarDataTable que se muestra acontinuacion.

                    Conexion Ocoon = new Conexion();


                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_INGRESOS_TO_EXCEL", dfecha_inicio, dfecha_fin, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca);
                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_INGRESOS_TO_EXCEL3", dfecha_inicio, dfecha_fin, sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidmarca, sidcategoriaProducto, iidperson);
                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_INGRESOS_TO_EXCEL4", dfecha_inicio, dfecha_fin, sidplanning, sidchannel, "", icod_oficina, iidNodeComercial, sidPDV, sidmarca, sidcategoriaProducto, iidperson);

                    if (dt.Rows.Count > 0)
                    {
                        //gv_stock.DataSource = dt;
                        //gv_stock.DataBind();


                        gv_stockToExcel.DataSource = dt;
                        gv_stockToExcel.DataBind();

                        lblmensaje.ForeColor = System.Drawing.Color.Green;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Blue;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                        gv_stock.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
                gv_stock.DataBind();

                //System.Threading.Thread.Sleep(8000);
                //Response.Redirect("~/err_mensaje_seccion.aspx", true);

            }

        }



        protected void gv_precios_CancelCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            cargarGrilla_Stock_Ingresos();
        }
        protected void gv_precios_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            cargarGrilla_Stock_Ingresos();
        }
        protected void gv_precios_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            cargarGrilla_Stock_Ingresos();
        }
        protected void gv_precios_PageSizeChanged(object source, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            cargarGrilla_Stock_Ingresos();
        }
        protected void gv_precios_PdfExporting(object source, Telerik.Web.UI.GridPdfExportingArgs e)
        {

        }

        //psalas, 16/08/2011 se quita el parametro idregingresodet de la funcion update_stock_detalle_validado
        protected void update_stock_detalle_validado(int idregstockdet, bool validar, string opcionreporte)
        {
            try
            {
                Conexion OCoon = new Conexion();
                //psalas, 16/08/2011, se quita el valor idregingresodet, xq ahora se guarda en una sola tabla
                //OCoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_STOCK_DETALLE_VALIDADO2", idregstockdet, idregingresodet, validar);
                OCoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_PRESENCIA_VALIDADO_COLGATE", idregstockdet, validar, opcionreporte, Session["sUser"].ToString(), DateTime.Now);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();

            }


        }
        protected void btn_validar_Click_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gv_stock.Items.Count; i++)
            {
                GridItem item = gv_stock.Items[i];
                // if (item.ItemType == GridItemType.Item)
                //{

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");
                Label lbl_validar = (Label)item.FindControl("lbl_validar");
                Label lbl_id_presencia_detalle = (Label)item.FindControl("lbl_id_presencial_det");
                //Label lbl_id_IngresosDetalle = (Label)item.FindControl("lblregingresodet");

                Label lbl_opcion_reporte = (Label)item.FindControl("lbl_opcion_reporte");
               
                string str_opcion_reporte = lbl_opcion_reporte.Text;


                if (cb_validar.Enabled == true)
                {

                    int idreg_presencia_det = Convert.ToInt32(lbl_id_presencia_detalle.Text);
                   
                    bool validar = cb_validar.Checked;

                    if (validar == true)
                    {
                        //psalas, 16/8/2011, actualizacion del metodo se quita un parametro idregingresodet
                        update_stock_detalle_validado(idreg_presencia_det, validar, str_opcion_reporte);

                        lbl_validar.Text = "valido";
                        lbl_validar.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        //psalas, 16/8/2011, actualizacion del metodo se quita un parametro idregingresodet
                        update_stock_detalle_validado(idreg_presencia_det, validar, str_opcion_reporte);

                        lbl_validar.Text = "invalidado";
                        lbl_validar.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
        }
        protected void gv_precios_DataBound(object sender, EventArgs e)
        {
            if (gv_stock.Items.Count > 0)
            {
                cb_all.Visible = true;
                lbl_cb_all.Visible = true;

            }
            else
            {
                cb_all.Visible = false;
                lbl_cb_all.Visible = false;
            }
        }
        protected void gv_precios_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                lblmensaje.Text = "";
                Conexion Ocoon = new Conexion();

                GridItem item = gv_stock.Items[e.Item.ItemIndex];

                //Label lbl_id_StockDetalle = (Label)item.FindControl("lblregstockfinaldet");
                Label lbl_id_detalle_presencia = (Label)item.FindControl("lbl_id_presencial_det");
                Label lbl_opcion_reporte = (Label)item.FindControl("lbl_opcion_reporte");
                int ii_id_detalle_presencia = Convert.ToInt32(lbl_id_detalle_presencia.Text);
                string str_opcion_reporte = lbl_opcion_reporte.Text;
                //Label lbl_id_IngresoDetalle = (Label)item.FindControl("lblregingresodet");
                //int iid_det = Convert.ToInt32(lbl_id_StockDetalle.Text.Trim());
                //int iid_detingreso = Convert.ToInt32(lbl_id_IngresoDetalle.Text.Trim());

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

                string valor = ArrayEditorValue[0].ToString();
                //string pedido = ArrayEditorValue[1].ToString();
                DateTime Fec_reg_bd = Convert.ToDateTime((ArrayEditorValue[1] as RadDateTimePicker).SelectedDate);

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_PRESENCIA_COLGATE", ii_id_detalle_presencia, str_opcion_reporte, valor, Fec_reg_bd, Session["sUser"].ToString(), DateTime.Now, ckvalidado.Checked);
                cargarGrilla_Stock_Ingresos();

            }
            catch (Exception ex)
            {
                lblmensaje.Text = ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
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
                cmbcanal.Items.Insert(0, new ListItem("---Todas---", "0"));
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
                cmbTipo_reporte.DataSource = dt;
                cmbTipo_reporte.DataValueField = "ID_OPCIONPRESENCIA";
                cmbTipo_reporte.DataTextField = "TipoReporte_Descripcion";
                cmbTipo_reporte.DataBind();
                cmbTipo_reporte.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbTipo_reporte.Enabled = true;
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
                //if (this.compañia != null)
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
        protected void cargarCombo_NodeComercial(string sid_planning)
        {
            try
            {
                #region
                //cmbNodeComercial.Items.Clear();
                //Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(cmbplanning.SelectedValue);

                //if (oListNodeComercial.Length > 0)
                //{
                //    cmbNodeComercial.Enabled = true;
                //    cmbNodeComercial.DataSource = oListNodeComercial;
                //    cmbNodeComercial.DataTextField = "commercialNodeName";
                //    cmbNodeComercial.DataValueField = "NodeCommercial";
                //    cmbNodeComercial.DataBind();

                //    cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                //}
                #endregion
                cmbNodeComercial.Items.Clear();
                DataTable dt = new DataTable();
                Conexion Ocoon = new Conexion();
                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_NODECOMERCIAL_BY_idPlanning", sid_planning);
                if (dt.Rows.Count > 0)
                {
                    cmbNodeComercial.Enabled = true;
                    cmbNodeComercial.DataSource = dt;
                    //cmbNodeComercial.DataTextField = "commercialNodeName";
                    //cmbNodeComercial.DataValueField = "NodeCommercial";
                    cmbNodeComercial.DataTextField = "commercialNodeName";
                    cmbNodeComercial.DataValueField = "id_NodeCommercial";
                    cmbNodeComercial.DataBind();
                    cmbNodeComercial.Items.Insert(0, new ListItem("--Todas--", "0"));
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //protected void cargarCombo_corporaciones(string sid_planning)
        //{

        //    cmbcorporacion.Items.Clear();
        //    DataTable dt = new DataTable();
        //    //UP_WEBXPLORA_AD_OBTENER_CORPORACION_BY_idPlanning
        //    Conexion Ocoon = new Conexion();
        //    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_CORPORACION_BY_idPlanning", sid_planning);
        //    if (dt.Rows.Count > 0)
        //    {
        //        cmbcorporacion.DataSource = dt;
        //        cmbcorporacion.DataValueField = "corp_id";
        //        cmbcorporacion.DataTextField = "corp_name";
        //        cmbcorporacion.DataBind();
        //        cmbcorporacion.Items.Insert(0, new ListItem("---Todas---", "0"));
        //    }

        //}
        //protected void cargarCombo_CategoriaDeproducto(int compañia)
        //{
        //    DataTable dt = null;
        //    Conexion Ocoon = new Conexion();

        //    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_BY_COMPANY_ID", compañia);
        //    cmbcategoria_producto.Enabled = false;
        //    if (dt.Rows.Count > 0)
        //    {
        //        cmbcategoria_producto.DataSource = dt;
        //        cmbcategoria_producto.DataValueField = "id_ProductCategory";
        //        cmbcategoria_producto.DataTextField = "Product_Category";
        //        cmbcategoria_producto.DataBind();
        //        cmbcategoria_producto.Items.Insert(0, new ListItem("---Todas---", "0"));
        //        cmbcategoria_producto.Enabled = true;
        //    }
        //}
        protected void cargarCombo_CategoriaDeproducto(string sidplanning)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_STOCK", sidplanning);
            cmbcategoria_producto.Enabled = false;
            //cmbfamilia.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                cmbcategoria_producto.DataSource = dt;
                cmbcategoria_producto.DataValueField = "id_ProductCategory";
                cmbcategoria_producto.DataTextField = "Product_Category";
                cmbcategoria_producto.DataBind();
                cmbcategoria_producto.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbcategoria_producto.Enabled = true;
            }
        }

        protected void cmbOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Conexion Ocoon = new Conexion();

                cmbPuntoDeVenta.Items.Clear();
                cmbPuntoDeVenta.Enabled = false;
                if (cmbplanning.SelectedIndex > 0 && cmbOficina.SelectedIndex > 0)
                {
                    DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", cmbplanning.SelectedValue, Convert.ToInt32(cmbOficina.SelectedValue));

                    if (dtPdv.Rows.Count > 0)
                    {
                        cmbPuntoDeVenta.DataSource = dtPdv;
                        cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
                        cmbPuntoDeVenta.DataTextField = "pdv_Name";
                        cmbPuntoDeVenta.DataBind();

                        cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));

                        cmbPuntoDeVenta.Enabled = true;
                    }

                    DataTable dtNodeCom = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_NODECOMERCIAL_BY_idPlanning_and_codOficina", cmbplanning.SelectedValue, Convert.ToInt32(cmbOficina.SelectedValue));

                    if (dtNodeCom.Rows.Count > 0)
                    {
                        cmbNodeComercial.Enabled = true;
                        cmbNodeComercial.Items.Clear();
                        cmbNodeComercial.DataSource = dtNodeCom;
                        cmbNodeComercial.DataTextField = "commercialNodeName";
                        cmbNodeComercial.DataValueField = "id_NodeCommercial";
                        cmbNodeComercial.DataBind();

                        cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                    }
                    else
                    {
                        cmbNodeComercial.Enabled = false;
                        cmbNodeComercial.Items.Clear();
                    }
                }
                if (cmbOficina.SelectedIndex == 0 && cmbplanning.SelectedIndex > 0)
                {
                    cargarCombo_NodeComercial(cmbplanning.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbNodeComercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Conexion Ocoon = new Conexion();
                cmbPuntoDeVenta.Items.Clear();
                cmbPuntoDeVenta.Enabled = false;
                if (cmbplanning.SelectedIndex > 0 && cmbNodeComercial.SelectedIndex > 0)
                {
                    DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_and_idNodeComercial", cmbplanning.SelectedValue, Convert.ToInt32(cmbNodeComercial.SelectedValue));

                    if (dtPdv.Rows.Count > 0)
                    {
                        cmbPuntoDeVenta.DataSource = dtPdv;
                        cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
                        cmbPuntoDeVenta.DataTextField = "pdv_Name";
                        cmbPuntoDeVenta.DataBind();

                        cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));

                        cmbPuntoDeVenta.Enabled = true;
                    }
                }

                if (cmbNodeComercial.SelectedIndex == 0)
                {
                    DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", cmbplanning.SelectedValue, Convert.ToInt32(cmbOficina.SelectedValue));

                    if (dtPdv.Rows.Count > 0)
                    {
                        cmbPuntoDeVenta.DataSource = dtPdv;
                        cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
                        cmbPuntoDeVenta.DataTextField = "pdv_Name";
                        cmbPuntoDeVenta.DataBind();

                        cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));

                        cmbPuntoDeVenta.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cmbcategoria_producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            //string iischannel = cmbcanal.SelectedValue;
            //string sidplanning = cmbplanning.SelectedValue;
            string siproductCategory = cmbcategoria_producto.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_BY_CATEGORIA_ID", siproductCategory);

            cmbmarca.Items.Clear();
            cmbmarca.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                cmbmarca.DataSource = dt;
                cmbmarca.DataValueField = "id_Brand";
                cmbmarca.DataTextField = "Name_Brand";
                cmbmarca.DataBind();
                cmbmarca.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbmarca.Enabled = true;
            }
            // cargarGrilla_Precio();
        }
        //protected void cmbfamilia_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataTable dt = null;
        //    Conexion Ocoon = new Conexion();

        //    string sidproductFamily = cmbfamilia.SelectedValue;
        //    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", sidproductFamily);


        //    cmbsubfamilia.Items.Clear();
        //    cmbsubfamilia.Enabled = false;

        //    if (dt.Rows.Count > 0)
        //    {
        //        cmbsubfamilia.DataSource = dt;
        //        cmbsubfamilia.DataValueField = "id_ProductSubFamily";
        //        cmbsubfamilia.DataTextField = "subfam_nombre";
        //        cmbsubfamilia.DataBind();
        //        cmbsubfamilia.Items.Insert(0, new ListItem("---Todas---", "0"));
        //        cmbsubfamilia.Enabled = true;
        //    }
        //    //cargarGrilla_Stock();
        //}
        //protected void cmbcorporacion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cmbNodeComercial.Items.Clear();
        //    DataTable dt = new DataTable();
        //    Conexion Ocoon = new Conexion();
        //    string sidcorporacion = cmbcorporacion.SelectedValue;
        //    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_NODOCOMERCIAL_BY_CORPORACION_ID", sidcorporacion);
        //    if (dt.Rows.Count > 0)
        //    {
        //        cmbNodeComercial.DataSource = dt;
        //        cmbNodeComercial.DataValueField = "id_NodeCommercial";
        //        cmbNodeComercial.DataTextField = "commercialNodeName";
        //        cmbNodeComercial.DataBind();
        //        cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
        //        cmbNodeComercial.Enabled = true;
        //    }
        //}
        protected void cmbplanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            string sidplanning = cmbplanning.SelectedValue;

            if (cmbplanning.SelectedIndex != 0)
            {
                //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);
                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUPERVISOR", sidplanning);
                cmbcategoria_producto.Items.Clear();
                cmbcategoria_producto.Enabled = false;
                //cmbfamilia.Items.Clear();
                //cmbfamilia.Enabled = false;
                cmbperson.Items.Clear();
                cmbperson.Enabled = false;

                if (dt.Rows.Count > 0)
                {
                    cmbperson.DataSource = dt;
                    cmbperson.DataValueField = "Person_id";
                    cmbperson.DataTextField = "Person_NameComplet";
                    cmbperson.DataBind();
                    cmbperson.Items.Insert(0, new ListItem("---Todos---", "0"));
                    cmbperson.Enabled = true;
                }
                cmbMercaderista.Items.Clear();
                cmbTipo_reporte.Items.Clear();

                cmbMercaderista.Enabled = true;
                cmbTipo_reporte.Enabled = true;
                CargarCombo_Mercaderistas(sidplanning);
                CargarCombo_TipoReporte(sidplanning);
              
                
                //------llamado al metodo cargar categoria de producto
                cargarCombo_Oficina();
                cargarCombo_NodeComercial(sidplanning);
                cargarCombo_CategoriaDeproducto(sidplanning);
                //cargarCombo_corporaciones(sidplanning);
                //----------------------------------------------------

            }
            else
            {
                cmbcategoria_producto.Items.Clear();
                cmbcategoria_producto.Enabled = false;
                //cmbfamilia.Items.Clear();
                //cmbfamilia.Enabled = false;
                cmbperson.Items.Clear();
                cmbperson.Enabled = false;

                cmbOficina.Items.Clear();
                cmbOficina.Enabled = false;
                cmbNodeComercial.Items.Clear();
                cmbNodeComercial.Enabled = false;
                cmbPuntoDeVenta.Items.Clear();
                cmbPuntoDeVenta.Enabled = false;

                cmbTipo_reporte.Items.Clear();
                cmbTipo_reporte.Enabled = false;
                cmbMercaderista.Items.Clear();
                cmbMercaderista.Enabled = false;
            }
        }
        protected void cmbsku_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void cmbcanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            compañia = Convert.ToInt32(this.Session["companyid"]);

            string sidchannel = cmbcanal.SelectedValue;

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);

            cmbplanning.Items.Clear();
            cmbcategoria_producto.Items.Clear();
            cmbcategoria_producto.Enabled = false;
            //cmbfamilia.Items.Clear();
            //cmbfamilia.Enabled = false;
            cmbperson.Items.Clear();
            cmbperson.Enabled = false;

            cmbOficina.Items.Clear();
            cmbOficina.Enabled = false;
            cmbNodeComercial.Items.Clear();
            cmbNodeComercial.Enabled = false;
            cmbPuntoDeVenta.Items.Clear();
            cmbPuntoDeVenta.Enabled = false;

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

        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            bool validar = cb_all.Checked;

            for (int i = 0; i < gv_stock.Items.Count; i++)
            {
                GridItem item = gv_stock.Items[i];
                //if (item.ItemType == GridItemType.Item)
                //{

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");

                if (cb_validar.Enabled == true)
                {
                    if (validar == true)
                    {
                        cb_validar.Checked = true;
                    }
                    else if (validar == false)
                    {
                        cb_validar.Checked = false;
                    }
                }

            }
        }
    }
}