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
using System.Configuration;

namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_SF_Tra_ExaTda : System.Web.UI.Page
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
                CargarCanal();
            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {

            cargarGrilla_Act_Datos();
        }
        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GridView1.Visible = true;
                GridViewExportUtil.ExportToExcelMethodTwo("Reporte Examen de Tienda - SF - Tradicional", this.GridView1);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void cargarGrilla_Act_Datos()
        {
            lblmensaje.Text = "";
            try
            {

                Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();

                DataTable dt = null;

             
                //int iidperson = 0;
                //if (cmbGenerador.SelectedIndex >= 0)
                //    iidperson = Convert.ToInt32(cmbGenerador.SelectedValue);
             
                string sidplanning = cmbplanning.SelectedValue;
                string sidchannel = cmbcanal.SelectedValue;

                string iidDistribuidora = "0";
                if (cmbNodeComercial.SelectedIndex >= 0)
                    iidDistribuidora = cmbNodeComercial.SelectedValue.ToString();
                string iidDistrito = "0";
                if (cmbDistrito.SelectedIndex >= 0)
                    iidDistrito = cmbDistrito.SelectedValue.ToString();
                string iidSupervisor = "0";
                if (cmbSupervisor.SelectedIndex >= 0)
                    iidSupervisor = cmbSupervisor.SelectedValue.ToString();
                string iidGenerador = "0";
                if (cmbGenerador.SelectedIndex >= 0)
                    iidGenerador = cmbGenerador.SelectedValue.ToString();

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

                    Conexion Ocoon = new Conexion();
                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_SF_TRA_ACTDATOS_OBTENER_New", dfecha_inicio, dfecha_fin, sidplanning, sidchannel, iidDistribuidora, iidDistrito, iidSupervisor, iidGenerador);
               
                    if (dt.Rows.Count > 0)
                    {
                        gv_ExamenTienda.DataSource = dt;
                        gv_ExamenTienda.DataBind();
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        lblmensaje.ForeColor = System.Drawing.Color.Green;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";

                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Blue;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                        gv_ExamenTienda.DataBind();
                    }
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
                gv_ExamenTienda.DataBind();

            }

        }

        #region
        //Disabled
        protected void cargarGrilla_Act_Datostoexcel()
        {
            lblmensaje.Text = "";
            try
            {

                Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();

                DataTable dt = null;

                int iidperson = 0;
                if (cmbGenerador.SelectedIndex >= 0)
                    iidperson = Convert.ToInt32(cmbGenerador.SelectedValue);

                string sidplanning = cmbplanning.SelectedValue;
                string sidchannel = cmbcanal.SelectedValue;

                //int icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);

                //int iidNodeComercial = Convert.ToInt32(cmbNodeComercial.SelectedValue);

                //string sidPDV = cmbPuntoDeVenta.SelectedValue;
                //if (sidPDV == "")
                //    sidPDV = "0";

                //string sidcategoriaProducto = cmbcategoria_producto.SelectedValue;
                //if (sidcategoriaProducto == "")
                //    sidcategoriaProducto = "0";

                //string sidmarca = cmbmarca.SelectedValue;
                //if (sidmarca == "")
                //    sidmarca = "0";

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
                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_INGRESOS_TO_EXCEL4", dfecha_inicio, dfecha_fin, sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidmarca, sidcategoriaProducto, iidperson);

                    if (dt.Rows.Count > 0)
                    {
                        //gv_ExamenTienda.DataSource = dt;
                        //gv_ExamenTienda.DataBind();


                        gv_ExamenTiendaToExcel.DataSource = dt;
                        gv_ExamenTiendaToExcel.DataBind();

                        lblmensaje.ForeColor = System.Drawing.Color.Green;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Blue;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                        gv_ExamenTienda.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
                gv_ExamenTienda.DataBind();

                //System.Threading.Thread.Sleep(8000);
                //Response.Redirect("~/err_mensaje_seccion.aspx", true);

            }

        }



        protected void gv_precios_CancelCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            cargarGrilla_Act_Datos();
        }
        protected void gv_precios_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            cargarGrilla_Act_Datos();
        }
        protected void gv_precios_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            cargarGrilla_Act_Datos();
        }
        protected void gv_precios_PageSizeChanged(object source, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            cargarGrilla_Act_Datos();
        }
        protected void gv_precios_PdfExporting(object source, Telerik.Web.UI.GridPdfExportingArgs e)
        {

        }

        
        protected void update_ExaTda_detalle_Validado(int idregActDatosDet, bool validar)
        {
            try
            {
                Conexion OCoon = new Conexion();
                OCoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_ACTDATOS_DETALLE_VALIDADO", idregActDatosDet, validar, Session["sUser"].ToString(), DateTime.Now);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();

            }


        }
        protected void btn_validar_Click_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gv_ExamenTienda.Items.Count; i++)
            {
                GridItem item = gv_ExamenTienda.Items[i];


                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");
                Label lbl_validar = (Label)item.FindControl("lbl_validar");
                Label lbl_id_RegAct_Datos_Detalle = (Label)item.FindControl("lblregActDatosDetalle");


                if (cb_validar.Enabled == true)
                {

                    int idregActDatosDet = Convert.ToInt32(lbl_id_RegAct_Datos_Detalle.Text);
                    bool validar = cb_validar.Checked;

                    if (validar == true)
                    {
                        update_ExaTda_detalle_Validado(idregActDatosDet, validar);

                        lbl_validar.Text = "valido";
                        lbl_validar.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        update_ExaTda_detalle_Validado(idregActDatosDet, validar);

                        lbl_validar.Text = "invalidado";
                        lbl_validar.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
        }
        protected void gv_precios_DataBound(object sender, EventArgs e)
        {
            if (gv_ExamenTienda.Items.Count > 0)
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

                GridItem item = gv_ExamenTienda.Items[e.Item.ItemIndex];

                Label lbl_idActTiendaDetalle = (Label)item.FindControl("lblregActDatosDetalle");
                int iid_det = Convert.ToInt32(lbl_idActTiendaDetalle.Text.Trim());
                Label lbl_idActTienda = (Label)item.FindControl("lblregActDatos");
                int iid_cab = Convert.ToInt32(lbl_idActTienda.Text.Trim());
                
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

                            if (editor is GridTextColumnEditor)
                            {
                                editorText = (editor as GridTextColumnEditor).Text;
                                editorValue = (editor as GridTextColumnEditor).Text;
                                ArrayEditorValue.Add(editorValue);
                            }

                            if (editor is GridDropDownListColumnEditor) {
                                editorText = (editor as GridDropDownColumnEditor).SelectedText;
                                editorValue = (editor as GridDropDownColumnEditor).SelectedValue;
                                ArrayEditorValue.Add(editorValue);
                            }

                        }
                    }
                }

                string horarioAtencion_01 = ArrayEditorValue[0].ToString();
                string horarioAtencion_02 = ArrayEditorValue[1].ToString();
                string diaDescanso = ArrayEditorValue[2].ToString();
                string giroPdv = ArrayEditorValue[3].ToString();
                string ubicacionPdv = ArrayEditorValue[4].ToString();
                string ptoConsumo = ArrayEditorValue[5].ToString();
                string sizePdv = ArrayEditorValue[6].ToString();
                string horneaPavo = ArrayEditorValue[7].ToString();

                //string ingreso = ArrayEditorValue[0].ToString();
                //string pedido = ArrayEditorValue[1].ToString();
                //DateTime Fec_reg_bd = Convert.ToDateTime((ArrayEditorValue[2] as RadDateTimePicker).SelectedDate);

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_ACTDATOS_SF_TRA", iid_cab, iid_det, horarioAtencion_01, horarioAtencion_02, diaDescanso, giroPdv, ubicacionPdv, ptoConsumo, sizePdv, horneaPavo, Session["sUser"].ToString(), DateTime.Now, ckvalidado.Checked);
                cargarGrilla_Act_Datos();

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
        protected void cargarCombo_Oficina()
        {
            //try
            //{
            //    Conexion Ocoon = new Conexion();

            //    if (this.Session["companyid"] != null)
            //    //if (this.compañia != null)
            //    {
            //        compañia = Convert.ToInt32(this.Session["companyid"]);
            //        DataTable dtofi = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENEROFICINAS", compañia);

            //        if (dtofi.Rows.Count > 0)
            //        {
            //            cmbOficina.Enabled = true;
            //            cmbOficina.DataSource = dtofi;
            //            cmbOficina.DataTextField = "Name_Oficina";
            //            cmbOficina.DataValueField = "cod_Oficina";
            //            cmbOficina.DataBind();

            //            cmbOficina.Items.Insert(0, new ListItem("---Todas---", "0"));
            //        }
            //    }
            //    else
            //    {
            //        Response.Redirect("~/err_mensaje_seccion.aspx", true);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }
        protected void cargarCombo_NodeComercial(string sid_planning)
        {
            //try
            //{
            //    #region
            //    //cmbNodeComercial.Items.Clear();
            //    //Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(cmbplanning.SelectedValue);

            //    //if (oListNodeComercial.Length > 0)
            //    //{
            //    //    cmbNodeComercial.Enabled = true;
            //    //    cmbNodeComercial.DataSource = oListNodeComercial;
            //    //    cmbNodeComercial.DataTextField = "commercialNodeName";
            //    //    cmbNodeComercial.DataValueField = "NodeCommercial";
            //    //    cmbNodeComercial.DataBind();

            //    //    cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
            //    //}
            //    #endregion
            //    cmbNodeComercial.Items.Clear();
            //    DataTable dt = new DataTable();
            //    Conexion Ocoon = new Conexion();
            //    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_NODECOMERCIAL_BY_idPlanning", sid_planning);
            //    if (dt.Rows.Count > 0)
            //    {
            //        cmbNodeComercial.Enabled = true;
            //        cmbNodeComercial.DataSource = dt;
            //        //cmbNodeComercial.DataTextField = "commercialNodeName";
            //        //cmbNodeComercial.DataValueField = "NodeCommercial";
            //        cmbNodeComercial.DataTextField = "commercialNodeName";
            //        cmbNodeComercial.DataValueField = "id_NodeCommercial";
            //        cmbNodeComercial.DataBind();
            //        cmbNodeComercial.Items.Insert(0, new ListItem("--Todas--", "0"));
            //    }

            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }
        protected void cargarCombo_corporaciones(string sid_planning)
        {

            //cmbcorporacion.Items.Clear();
            //DataTable dt = new DataTable();
            ////UP_WEBXPLORA_AD_OBTENER_CORPORACION_BY_idPlanning
            //Conexion Ocoon = new Conexion();
            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_CORPORACION_BY_idPlanning", sid_planning);
            //if (dt.Rows.Count > 0)
            //{
            //    cmbcorporacion.DataSource = dt;
            //    cmbcorporacion.DataValueField = "corp_id";
            //    cmbcorporacion.DataTextField = "corp_name";
            //    cmbcorporacion.DataBind();
            //    cmbcorporacion.Items.Insert(0, new ListItem("---Todas---", "0"));
            //}

        }
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
            //DataTable dt = null;
            //Conexion Ocoon = new Conexion();

            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_STOCK", sidplanning);
            //cmbcategoria_producto.Enabled = false;
            //cmbfamilia.Enabled = false;
            //if (dt.Rows.Count > 0)
            //{
            //    cmbcategoria_producto.DataSource = dt;
            //    cmbcategoria_producto.DataValueField = "id_ProductCategory";
            //    cmbcategoria_producto.DataTextField = "Product_Category";
            //    cmbcategoria_producto.DataBind();
            //    cmbcategoria_producto.Items.Insert(0, new ListItem("---Todas---", "0"));
            //    cmbcategoria_producto.Enabled = true;
            //}
        }


        protected void cmbOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    Conexion Ocoon = new Conexion();

            //    cmbPuntoDeVenta.Items.Clear();
            //    cmbPuntoDeVenta.Enabled = false;
            //    if (cmbplanning.SelectedIndex > 0 && cmbOficina.SelectedIndex > 0)
            //    {
            //        DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", cmbplanning.SelectedValue, Convert.ToInt32(cmbOficina.SelectedValue));

            //        if (dtPdv.Rows.Count > 0)
            //        {
            //            cmbPuntoDeVenta.DataSource = dtPdv;
            //            cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
            //            cmbPuntoDeVenta.DataTextField = "pdv_Name";
            //            cmbPuntoDeVenta.DataBind();

            //            cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));

            //            cmbPuntoDeVenta.Enabled = true;
            //        }

            //        DataTable dtNodeCom = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_NODECOMERCIAL_BY_idPlanning_and_codOficina", cmbplanning.SelectedValue, Convert.ToInt32(cmbOficina.SelectedValue));

            //        //if (dtNodeCom.Rows.Count > 0)
            //        //{
            //        //    cmbNodeComercial.Enabled = true;
            //        //    cmbNodeComercial.Items.Clear();
            //        //    cmbNodeComercial.DataSource = dtNodeCom;
            //        //    cmbNodeComercial.DataTextField = "commercialNodeName";
            //        //    cmbNodeComercial.DataValueField = "id_NodeCommercial";
            //        //    cmbNodeComercial.DataBind();

            //        //    cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
            //        //}
            //        //else
            //        //{
            //        //    cmbNodeComercial.Enabled = false;
            //        //    cmbNodeComercial.Items.Clear();
            //        //}
            //    }
            //    if (cmbOficina.SelectedIndex == 0 && cmbplanning.SelectedIndex > 0)
            //    {
            //        cargarCombo_NodeComercial(cmbplanning.SelectedValue);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        protected void cmbNodeComercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    Conexion Ocoon = new Conexion();
            //    cmbPuntoDeVenta.Items.Clear();
            //    cmbPuntoDeVenta.Enabled = false;
            //    if (cmbplanning.SelectedIndex > 0 && cmbNodeComercial.SelectedIndex > 0)
            //    {
            //        DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_and_idNodeComercial", cmbplanning.SelectedValue, Convert.ToInt32(cmbNodeComercial.SelectedValue));

            //        if (dtPdv.Rows.Count > 0)
            //        {
            //            cmbPuntoDeVenta.DataSource = dtPdv;
            //            cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
            //            cmbPuntoDeVenta.DataTextField = "pdv_Name";
            //            cmbPuntoDeVenta.DataBind();

            //            cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));

            //            cmbPuntoDeVenta.Enabled = true;
            //        }
            //    }

            //    if (cmbNodeComercial.SelectedIndex == 0)
            //    {
            //        DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", cmbplanning.SelectedValue, Convert.ToInt32(cmbOficina.SelectedValue));

            //        if (dtPdv.Rows.Count > 0)
            //        {
            //            cmbPuntoDeVenta.DataSource = dtPdv;
            //            cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
            //            cmbPuntoDeVenta.DataTextField = "pdv_Name";
            //            cmbPuntoDeVenta.DataBind();

            //            cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));

            //            cmbPuntoDeVenta.Enabled = true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        protected void cmbcategoria_producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt = null;
            //Conexion Ocoon = new Conexion();

            ////string iischannel = cmbcanal.SelectedValue;
            ////string sidplanning = cmbplanning.SelectedValue;
            //string siproductCategory = cmbcategoria_producto.SelectedValue;
            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_BY_CATEGORIA_ID", siproductCategory);

            //cmbmarca.Items.Clear();
            //cmbmarca.Enabled = false;

            //if (dt.Rows.Count > 0)
            //{
            //    cmbmarca.DataSource = dt;
            //    cmbmarca.DataValueField = "id_Brand";
            //    cmbmarca.DataTextField = "Name_Brand";
            //    cmbmarca.DataBind();
            //    cmbmarca.Items.Insert(0, new ListItem("---Todas---", "0"));
            //    cmbmarca.Enabled = true;
            //}
            //// cargarGrilla_Precio();
        }
        protected void cmbfamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt = null;
            //Conexion Ocoon = new Conexion();

            //string sidproductFamily = cmbfamilia.SelectedValue;
            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", sidproductFamily);


            //cmbsubfamilia.Items.Clear();
            //cmbsubfamilia.Enabled = false;

            //if (dt.Rows.Count > 0)
            //{
            //    cmbsubfamilia.DataSource = dt;
            //    cmbsubfamilia.DataValueField = "id_ProductSubFamily";
            //    cmbsubfamilia.DataTextField = "subfam_nombre";
            //    cmbsubfamilia.DataBind();
            //    cmbsubfamilia.Items.Insert(0, new ListItem("---Todas---", "0"));
            //    cmbsubfamilia.Enabled = true;
            //}
            ////cargarGrilla_Stock();
        }
        //protected void cmbcorporacion_SelectedIndexChanged(object sender, EventArgs e)
        //{
            
        //}
        
        protected void cmbplanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region
            //DataTable dt = null;
            //Conexion Ocoon = new Conexion();
            //string sidplanning = cmbplanning.SelectedValue;

            //if (cmbplanning.SelectedIndex != 0)
            //{
            //    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);
            //    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUPERVISOR", sidplanning);
            //    cmbcategoria_producto.Items.Clear();
            //    cmbcategoria_producto.Enabled = false;
            //    cmbfamilia.Items.Clear();
            //    cmbfamilia.Enabled = false;
            //    cmbGenerador.Items.Clear();
            //    cmbGenerador.Enabled = false;

            //    if (dt.Rows.Count > 0)
            //    {
            //        cmbGenerador.DataSource = dt;
            //        cmbGenerador.DataValueField = "Person_id";
            //        cmbGenerador.DataTextField = "Person_NameComplet";
            //        cmbGenerador.DataBind();
            //        cmbGenerador.Items.Insert(0, new ListItem("---Todos---", "0"));
            //        cmbGenerador.Enabled = true;
            //    }

            //    //------llamado al metodo cargar categoria de producto
            //    cargarCombo_Oficina();
            //    cargarCombo_NodeComercial(sidplanning);
            //    cargarCombo_CategoriaDeproducto(sidplanning);
            //    cargarCombo_corporaciones(sidplanning);
            //    //----------------------------------------------------

            //}
            //else
            //{
            //    cmbcategoria_producto.Items.Clear();
            //    cmbcategoria_producto.Enabled = false;
            //    cmbfamilia.Items.Clear();
            //    cmbfamilia.Enabled = false;
            //    cmbGenerador.Items.Clear();
            //    cmbGenerador.Enabled = false;

            //    cmbOficina.Items.Clear();
            //    cmbOficina.Enabled = false;
            //    cmbNodeComercial.Items.Clear();
            //    cmbNodeComercial.Enabled = false;
            //    cmbPuntoDeVenta.Items.Clear();
            //    cmbPuntoDeVenta.Enabled = false;
            //}


            //if (cmbplanning.SelectedIndex != 0)
            //{

                
            //    cmbNodeComercial.Enabled = false;
            //    cmbDistrito.Enabled = false;
            //    cmbGenerador.Enabled = false;
            //    cmbSupervisor.Enabled = false;

            #endregion

            string sidplanning = cmbplanning.SelectedValue;
            if (cmbplanning.SelectedIndex != 0)
            {
                cargarComboDistribuidorByPlanning();
                cargarComboDistritoByPlanning();
                cargarComboGeneradorByPlanning();
                cargarComboSupervidoresByPlanning();
            }
            else
            {
                cmbNodeComercial.Items.Clear();
                cmbNodeComercial.Enabled = false;
                cmbDistrito.Items.Clear();
                cmbDistrito.Enabled = false;
                cmbSupervisor.Items.Clear();
                cmbSupervisor.Enabled = false;
                cmbGenerador.Items.Clear();
                cmbGenerador.Enabled = false;
            }

        }

        public void cargarComboDistribuidorByPlanning()
        {
            //DistribuidoraBy_Planning
            cmbNodeComercial.Items.Clear();
            DataTable dt = new DataTable();
            Conexion Ocoon = new Conexion();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_NODOCOMERCIAL_BY_ID_PLANNING", cmbplanning.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbNodeComercial.DataSource = dt;
                cmbNodeComercial.DataValueField = "NodeCommercial";
                cmbNodeComercial.DataTextField = "commercialNodeName";
                cmbNodeComercial.DataBind();
                cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbNodeComercial.Enabled = true;
            }
        }
        public void cargarComboDistritoByPlanning()
        {
            //DistritosBy_Planning
            Conexion Ocoon = new Conexion();
            cmbDistrito.Items.Clear();
            DataTable dt = new DataTable();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DISTRITOS_BY_ID_PLANNING", cmbplanning.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbDistrito.DataSource = dt;
                cmbDistrito.DataValueField = "cod_District";
                cmbDistrito.DataTextField = "Name_Local";
                cmbDistrito.DataBind();
                cmbDistrito.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbDistrito.Enabled = true;
            }

        }
        public void cargarComboGeneradorByPlanning()
        {
            //GeneradoresByPlanning
            //UP_WEBXPLORA_OPE_GENERADORES_BY_ID_PLANNING
            Conexion Ocoon = new Conexion();
            cmbGenerador.Items.Clear();
            DataTable dt = new DataTable();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_GENERADORES_BY_ID_PLANNING", cmbplanning.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbGenerador.DataSource = dt;
                cmbGenerador.DataValueField = "Person_id";
                cmbGenerador.DataTextField = "Generador";
                cmbGenerador.DataBind();
                cmbGenerador.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbGenerador.Enabled = true;
            }
        }
        public void cargarComboSupervidoresByPlanning()
        {

            //SupervisoresByPlanning
            Conexion Ocoon = new Conexion();
            cmbSupervisor.Items.Clear();
            DataTable dt = new DataTable();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_SUPERVISORES_BY_ID_PLANNING", cmbplanning.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbSupervisor.DataSource = dt;
                cmbSupervisor.DataValueField = "Person_idSupe";
                cmbSupervisor.DataTextField = "Supervisor";
                cmbSupervisor.DataBind();
                cmbSupervisor.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbSupervisor.Enabled = true;
            }
        }
       

        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            bool validar = cb_all.Checked;

            for (int i = 0; i < gv_ExamenTienda.Items.Count; i++)
            {
                GridItem item = gv_ExamenTienda.Items[i];
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

        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            //MopoReporStock.Show();
        }

        protected void btnMasiva_Click(object sender, EventArgs e)
        {
            //mopoCargaMasiva.Show();
        }

        #endregion

        #region
        protected void CargarCanal()
        {
            //DataTable dt = null;
            //Conexion Ocoon = new Conexion();

            //compañia = Convert.ToInt32(this.Session["companyid"]);
            //pais = this.Session["scountry"].ToString();


            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CHANNEL", pais, compañia);
            //if (dt.Rows.Count > 0)
            //{
            //    ddlCanal.DataSource = dt;
            //    ddlCanal.DataValueField = "cod_Channel";
            //    ddlCanal.DataTextField = "Channel_Name";
            //    ddlCanal.DataBind();
            //    ddlCanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            //}

        }


        protected void ddlCanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt = null;
            //Conexion Ocoon = new Conexion();
            //compañia = Convert.ToInt32(this.Session["companyid"]);

            //string sidchannel = ddlCanal.SelectedValue;

            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);

            //ddlCampana.Items.Clear();
            //ddlCategoria.Items.Clear();
            //ddlCategoria.Enabled = false;
            //ddlFamilia.Items.Clear();
            //ddlFamilia.Enabled = false;
            //ddlMercaderista.Items.Clear();
            //ddlMercaderista.Enabled = false;

            //ddlOficina.Items.Clear();
            //ddlOficina.Enabled = false;
            //ddlNodeComercial.Items.Clear();
            //ddlNodeComercial.Enabled = false;
            //ddlPuntoVenta.Items.Clear();
            //ddlPuntoVenta.Enabled = false;

            //if (dt.Rows.Count > 0)
            //{
            //    ddlCampana.DataSource = dt;
            //    ddlCampana.DataValueField = "id_planning";
            //    ddlCampana.DataTextField = "Planning_Name";
            //    ddlCampana.DataBind();
            //    ddlCampana.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            //    ddlCampana.Enabled = true;
            //}
            //MopoReporStock.Show();
        }

        protected void ddlCampana_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt = null;
            //Conexion Ocoon = new Conexion();
            //string sidplanning = ddlCampana.SelectedValue;

            //if (ddlCampana.SelectedIndex != 0)
            //{
            //    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);

            //    ddlCategoria.Items.Clear();
            //    ddlCategoria.Enabled = false;
            //    ddlFamilia.Items.Clear();
            //    ddlFamilia.Enabled = false;
            //    ddlMercaderista.Items.Clear();
            //    ddlMercaderista.Enabled = false;

            //    if (dt.Rows.Count > 0)
            //    {
            //        ddlMercaderista.DataSource = dt;
            //        ddlMercaderista.DataValueField = "Person_id";
            //        ddlMercaderista.DataTextField = "Person_NameComplet";
            //        ddlMercaderista.DataBind();
            //        ddlMercaderista.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            //        ddlMercaderista.Enabled = true;
            //    }

            //    //------llamado al metodo cargar categoria de producto
            //    cargarOficina();
            //    cargarNodeComercial(sidplanning);
            //    cargarCategoria(sidplanning);
            //    //----------------------------------------------------

            //}
            //else
            //{
            //    ddlCategoria.Items.Clear();
            //    ddlCategoria.Enabled = false;
            //    ddlFamilia.Items.Clear();
            //    ddlFamilia.Enabled = false;
            //    ddlMercaderista.Items.Clear();
            //    ddlMercaderista.Enabled = false;

            //    ddlOficina.Items.Clear();
            //    ddlOficina.Enabled = false;
            //    ddlNodeComercial.Items.Clear();
            //    ddlNodeComercial.Enabled = false;
            //    ddlPuntoVenta.Items.Clear();
            //    ddlPuntoVenta.Enabled = false;
            //}
            //MopoReporStock.Show();
        }

        public void cargarOficina()
        {
            //try
            //{
            //    Conexion Ocoon = new Conexion();

            //    if (this.Session["companyid"] != null)
            //    {
            //        compañia = Convert.ToInt32(this.Session["companyid"]);
            //        DataTable dtofi = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENEROFICINAS", compañia);

            //        if (dtofi.Rows.Count > 0)
            //        {
            //            ddlOficina.Enabled = true;
            //            ddlOficina.DataSource = dtofi;
            //            ddlOficina.DataTextField = "Name_Oficina";
            //            ddlOficina.DataValueField = "cod_Oficina";
            //            ddlOficina.DataBind();

            //            ddlOficina.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            //        }
            //    }
            //    else
            //    {
            //        Response.Redirect("~/err_mensaje_seccion.aspx", true);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}

        }


        protected void cargarNodeComercial(string sid_planning)
        {
            //try
            //{
            //    cmbNodeComercial.Items.Clear();
            //    Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(sid_planning);

            //    if (oListNodeComercial.Length > 0)
            //    {
            //        ddlNodeComercial.Enabled = true;
            //        ddlNodeComercial.DataSource = oListNodeComercial;
            //        ddlNodeComercial.DataTextField = "commercialNodeName";
            //        ddlNodeComercial.DataValueField = "NodeCommercial";
            //        ddlNodeComercial.DataBind();

            //        ddlNodeComercial.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            //        MopoReporStock.Show();
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }

        protected void cargarCategoria(string sidplanning)
        {
            //DataTable dt = null;
            //Conexion Ocoon = new Conexion();

            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_STOCK", sidplanning);
            //ddlCategoria.Enabled = false;
            //ddlFamilia.Enabled = false;
            //if (dt.Rows.Count > 0)
            //{
            //    ddlCategoria.DataSource = dt;
            //    ddlCategoria.DataValueField = "id_ProductCategory";
            //    ddlCategoria.DataTextField = "Product_Category";
            //    ddlCategoria.DataBind();
            //    ddlCategoria.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            //    ddlCategoria.Enabled = true;
            //}
        }

        protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    Conexion Ocoon = new Conexion();

            //    ddlPuntoVenta.Items.Clear();
            //    ddlPuntoVenta.Enabled = false;
            //    if (ddlCampana.SelectedIndex > 0 && ddlOficina.SelectedIndex > 0)
            //    {
            //        DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", ddlCampana.SelectedValue, Convert.ToInt32(ddlOficina.SelectedValue));

            //        if (dtPdv.Rows.Count > 0)
            //        {
            //            ddlPuntoVenta.DataSource = dtPdv;
            //            ddlPuntoVenta.DataValueField = "ClientPDV_Code";
            //            ddlPuntoVenta.DataTextField = "pdv_Name";
            //            ddlPuntoVenta.DataBind();

            //            ddlPuntoVenta.Items.Insert(0, new ListItem("---Seleccione---", "0"));

            //            ddlPuntoVenta.Enabled = true;
            //        }

            //        DataTable dtNodeCom = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_NODECOMERCIAL_BY_idPlanning_and_codOficina", ddlCampana.SelectedValue, Convert.ToInt32(ddlOficina.SelectedValue));

            //        if (dtNodeCom.Rows.Count > 0)
            //        {
            //            ddlNodeComercial.Enabled = true;
            //            ddlNodeComercial.Items.Clear();
            //            ddlNodeComercial.DataSource = dtNodeCom;
            //            ddlNodeComercial.DataTextField = "commercialNodeName";
            //            ddlNodeComercial.DataValueField = "id_NodeCommercial";
            //            ddlNodeComercial.DataBind();

            //            ddlNodeComercial.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            //        }
            //        else
            //        {
            //            ddlNodeComercial.Enabled = false;
            //            ddlNodeComercial.Items.Clear();
            //        }
            //    }
            //    if (ddlOficina.SelectedIndex == 0 && ddlCampana.SelectedIndex > 0)
            //    {
            //        cargarCombo_NodeComercial(ddlCampana.SelectedValue);
            //    }
            //    MopoReporStock.Show();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void ddlNodeComercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    Conexion Ocoon = new Conexion();


            //    if (ddlCampana.SelectedIndex > 0 && ddlNodeComercial.SelectedIndex > 0)
            //    {
            //        DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_and_idNodeComercial", ddlCampana.SelectedValue, Convert.ToInt32(ddlNodeComercial.SelectedValue));

            //        if (dtPdv.Rows.Count > 0)
            //        {
            //            ddlPuntoVenta.DataSource = dtPdv;
            //            ddlPuntoVenta.DataValueField = "ClientPDV_Code";
            //            ddlPuntoVenta.DataTextField = "pdv_Name";
            //            ddlPuntoVenta.DataBind();

            //            ddlPuntoVenta.Items.Insert(0, new ListItem("---Seleccione---", "0"));

            //            ddlPuntoVenta.Enabled = true;
            //        }
            //    }
            //    if (ddlNodeComercial.SelectedIndex == 0)
            //    {
            //        DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", ddlCampana.SelectedValue, Convert.ToInt32(ddlOficina.SelectedValue));

            //        if (dtPdv.Rows.Count > 0)
            //        {
            //            ddlPuntoVenta.DataSource = dtPdv;
            //            ddlPuntoVenta.DataValueField = "ClientPDV_Code";
            //            ddlPuntoVenta.DataTextField = "pdv_Name";
            //            ddlPuntoVenta.DataBind();

            //            ddlPuntoVenta.Items.Insert(0, new ListItem("---Seleccione---", "0"));

            //            ddlPuntoVenta.Enabled = true;
            //        }
            //    }
            //    MopoReporStock.Show();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt = null;
            //Conexion Ocoon = new Conexion();

            //string sidproductCategory = ddlCategoria.SelectedValue;
            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK", sidproductCategory);

            //ddlFamilia.Items.Clear();
            //ddlFamilia.Enabled = false;

            //if (dt.Rows.Count > 0)
            //{
            //    ddlFamilia.DataSource = dt;
            //    ddlFamilia.DataValueField = "id_ProductFamily";
            //    ddlFamilia.DataTextField = "name_Family";
            //    ddlFamilia.DataBind();
            //    ddlFamilia.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            //    ddlFamilia.Enabled = true;
            //}
            //MopoReporStock.Show();
        }

        void LimpiarControles()
        {
            //ddlCanal.SelectedValue = "0";
            //ddlCampana.Items.Clear();
            //ddlCategoria.Items.Clear();
            //ddlFamilia.Items.Clear();
            //ddlMercaderista.Items.Clear();
            //ddlNodeComercial.Items.Clear();
            //ddlOficina.Items.Clear();
            //ddlPuntoVenta.Items.Clear();

            //txtCantidad.Text = "";
            //txtObservacion.Text = "";
        }

        #endregion

        #region
        protected void btnGuardarReportStock_Click(object sender, EventArgs e)
        {
    //        try
    //        {
    //            if (ddlCanal.Text == "0" || ddlCampana.Text == "0" || ddlMercaderista.Text == "0"
    //|| ddlNodeComercial.Text == "0" || ddlPuntoVenta.Text == "0" || ddlCategoria.Text == "0"
    //|| txtCantidad.Text == "")
    //            {
    //                return;
    //            }

    //            Lucky.Business.Common.Application.OPE_Reporte_Stock oOPE_Reporte_Stock = new Lucky.Business.Common.Application.OPE_Reporte_Stock();


    //            DataTable dt = null;
    //            DataTable dtl = null;
    //            Conexion Ocoon = new Conexion();
    //            Conexion con = new Conexion(2);
    //            dt = Ocoon.ejecutarDataTable("UP_WEB_SEARCH_USER", "", Convert.ToInt32(ddlMercaderista.SelectedValue));
    //            string idperfil = dt.Rows[0][14].ToString();

    //            dtl = con.ejecutarDataTable("STP_JVM_LISTAR_CANALES", ddlCanal.SelectedValue);
    //            string tipocanal = dtl.Rows[0]["CAN_TIPO"].ToString();


    //            Lucky.Entity.Common.Application.EOPE_Reporte_Stock oEOPE_Reporte_Stock = oOPE_Reporte_Stock.RegistrarReporteStock(Convert.ToInt32(ddlMercaderista.SelectedValue),
    //                                                        idperfil, ddlCampana.SelectedValue, this.Session["companyid"].ToString(),
    //                                                        ddlPuntoVenta.SelectedValue, ddlCategoria.SelectedValue, null, null,
    //                                                        null, DateTime.Now.ToShortDateString(), "0", "0", "0");




    //            oOPE_Reporte_Stock.RegistrarReporteStock_Detalle(Convert.ToInt32(oEOPE_Reporte_Stock.ID_REG_STOCK), ddlFamilia.SelectedValue, txtCantidad.Text, null, null, null, null, txtObservacion.Text);

    //            LimpiarControles();
    //            lblmensaje.Visible = true;
    //            lblmensaje.Text = "Fue registrado con exito el reporte de Stock";
    //            lblmensaje.ForeColor = System.Drawing.Color.Blue;
    //        }

    //        catch (Exception ex)
    //        {
    //            string error = "";
    //            string mensaje = "";
    //            error = Convert.ToString(ex.Message);
    //            mensaje = ConfigurationManager.AppSettings["ErrorConection"];
    //            if (error == mensaje)
    //            {
    //                Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
    //                string errMessage = "";
    //                errMessage = mensaje;
    //                errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
    //                this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
    //            }
    //            else
    //            {
    //                this.Session.Abandon();
    //                Response.Redirect("~/err_mensaje_seccion.aspx", true);
    //            }
    //        }
        }
        #endregion

        protected void OnItemDataBoundHandler(object sender, GridItemEventArgs e)
        {
            string tableName = e.Item.OwnerTableView.Name.ToString();
            if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditManager editMan = editedItem.EditManager;
                DataTable dt = null;
                Conexion Ocoon = new Conexion();
                
                //LlenarDropDownList_DiaDeDescanso
                dt = Ocoon.ejecutarDataTable("UP_WEBPLORA_OPE_ObtenerPointOfSaleGenerales","DD");
                GridDropDownListColumnEditor ddlDiaDescanso = editMan.GetColumnEditor("ddlDiaDescanso") as GridDropDownListColumnEditor;
                ddlDiaDescanso.DataSource = dt;
                ddlDiaDescanso.DataValueField = "id_gen";
                ddlDiaDescanso.DataTextField  = "gen_descripcion";
                ddlDiaDescanso.DataBind();

                ///LlenarDropDownList_PointOfSaleGiro
                dt = Ocoon.ejecutarDataTable("UP_WEBPLORA_OPE_ObtenerPointOfSaleGiro");
                GridDropDownListColumnEditor ddlDescripcionGiro = editMan.GetColumnEditor("ddldescripcion_Giro") as GridDropDownListColumnEditor;
                ddlDescripcionGiro.DataSource = dt;
                ddlDescripcionGiro.DataValueField = "id_PointOfSale_Giro";
                ddlDescripcionGiro.DataTextField = "descripcion_Giro";
                ddlDescripcionGiro.DataBind();

                //LlenarDropDownList_UbicacionPtoVenta
                dt = Ocoon.ejecutarDataTable("UP_WEBPLORA_OPE_ObtenerPointOfSaleLocation");
                GridDropDownListColumnEditor ddlUbicacion = editMan.GetColumnEditor("ddlUbicacion") as GridDropDownListColumnEditor;
                ddlUbicacion.DataSource = dt;
                ddlUbicacion.DataValueField = "id_PointOfSale_Location";
                ddlUbicacion.DataTextField = "descripcion_Location";
                ddlUbicacion.DataBind();


                //LlenarDropDownList_EsPtoConsumo
                dt = Ocoon.ejecutarDataTable("UP_WEBPLORA_OPE_ObtenerPointOfSaleGenerales", "PC");
                GridDropDownListColumnEditor ddlPtoConsumo = editMan.GetColumnEditor("ddlPtoConsumo") as GridDropDownListColumnEditor;
                ddlPtoConsumo.DataSource = dt;
                ddlPtoConsumo.DataValueField = "id_gen";
                ddlPtoConsumo.DataTextField = "gen_descripcion";
                ddlPtoConsumo.DataBind();

               
                
                //LlenarDropDownList_SizePtoVenta
                dt = Ocoon.ejecutarDataTable("UP_WEBPLORA_OPE_ObtenerPointOfSaleSize");
                GridDropDownListColumnEditor ddlSizePtoVenta = editMan.GetColumnEditor("ddlSizePtoConsumo") as GridDropDownListColumnEditor;
                ddlSizePtoVenta.DataSource = dt;
                ddlSizePtoVenta.DataValueField = "id_PointOfSale_Size";
                ddlSizePtoVenta.DataTextField = "descripcion_Size";
                ddlSizePtoVenta.DataBind();



                //LlenarDropDownList_HorneaPavo
                dt = Ocoon.ejecutarDataTable("UP_WEBPLORA_OPE_ObtenerPointOfSaleGenerales","HP");
                GridDropDownListColumnEditor ddlHorneaPavo = editMan.GetColumnEditor("ddlHorneaPavo") as GridDropDownListColumnEditor;
                ddlHorneaPavo.DataSource = dt;
                ddlHorneaPavo.DataValueField = "id_gen";
                ddlHorneaPavo.DataTextField = "gen_descripcion";
                ddlHorneaPavo.DataBind();





            }

                #region
            //if (e.Item.IsInEditMode)
            //{
            //    GridEditableItem item = (GridEditableItem)e.Item;

            //    if (!(e.Item is IGridInsertItem))
            //    {
            //        RadComboBox combo = (RadComboBox)item.FindControl("rcbGiroPdv");
            //        RadComboBoxItem selectedItem = new RadComboBoxItem();
            //        selectedItem.Text = ((DataRowView)e.Item.DataItem)["descripcion_Giro"].ToString();
            //        combo.Items.Add(selectedItem);
            //        selectedItem.DataBind();

            //        //Conexion oCoon = new Conexion();
            //        //DataTable dt = oCoon.ejecutarDataTable("UP_WEBPLORA_OPE_ObtenerPointOfSaleGiro");
            //        //combo.Items.Clear();
            //        //combo.DataSource = dt;
            //        //combo.DataTextField = "descripcion_Giro";
            //        //combo.DataValueField = "id_PointOfSale_Giro";
            //        //combo.DataBind();
            //        //foreach (DataRow row in dt.Rows)
            //        //{
            //        //    RadComboBoxItem item2 = new RadComboBoxItem();
            //        //    item2.Text = row["id_PointOfSale_Giro"].ToString();
            //        //    item2.Value = row["descripcion_Giro"].ToString();
            //        //    //item.Attributes.Add("descripcion_Giro", row["descripcion_Giro"].ToString());

            //        //    combo.Items.Add(item2);
                        
            //        //    item2.DataBind();
            //        //}

            //    }
#endregion
                #region
                //selectedItem.Value = ((DataRowView)e.Item.DataItem)["id_PointOfSale_Giro"].ToString();
                //selectedItem.Attributes.Add("ContactName", ((DataRowView)e.Item.DataItem)["ContactName"].ToString());
                //Session["SupplierID"] = selectedItem.Value;

                //Conexion oCoon = new Conexion();
                //DataTable dt = oCoon.ejecutarDataTable("UP_WEBPLORA_OPE_ObtenerPointOfSaleGiro");
                //GridEditableItem item = (GridEditableItem)e.Item;
                //RadComboBox combo = (RadComboBox)item.FindControl("rcbGiroPdv");
                //RadComboBoxItem selectedItem = new RadComboBoxItem();
                //combo.DataSource = dt;
                //combo.DataTextField = "descripcion_Giro";
                //combo.DataValueField = "id_PointOfSale_Giro";
                //combo.DataBind();
                #endregion
                //}
        }

        protected void rcbGiroPdv_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
            {
           Conexion oCoon = new Conexion();
           #region
           ////DataTable dt = oCoon.ejecutarDataTable("UP_WEBPLORA_OPE_ObtenerPointOfSaleGiro");
                //GridEditableItem item = (GridEditableItem)e.Item;
                //RadComboBox combo = (RadComboBox)item.FindControl("rcbGiroPdv");
                //RadComboBoxItem selectedItem = new RadComboBoxItem();
                //combo.DataSource = dt;
                //combo.DataTextField = "descripcion_Giro";
                //combo.DataValueField = "id_PointOfSale_Giro";
           //combo.DataBind();
           #endregion
           DataTable dt = oCoon.ejecutarDataTable("UP_WEBPLORA_OPE_ObtenerPointOfSaleGiro");
            RadComboBox comboBox = (RadComboBox)sender;
            // Clear the default Item that has been re-created from ViewState at this point.
            comboBox.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["id_PointOfSale_Giro"].ToString();
                item.Value = row["descripcion_Giro"].ToString();
                //item.Attributes.Add("descripcion_Giro", row["descripcion_Giro"].ToString());

                comboBox.Items.Add(item);

                item.DataBind();
            }

            

            //if (e.Item.IsInEditMode) {
            //    GridEditableItem item = (GridEditableItem)e.Item;
            //    if (!(e.Item is IGridInsertItem))
            //    {
            //        RadComboBox combo = (RadComboBox)item.FindControl("rcbGiroPdv");
            //        RadComboBoxItem selectedItem = new RadComboBoxItem();
            //        combo.DataSource = dt;
            //        combo.DataTextField = "descripcion_Giro";
            //        combo.DataValueField = "id_PointOfSale_Giro";
            //        combo.DataBind();
            //    }
            //}
        }

        protected void OnSelectedIndexChangedHandler(object o, RadComboBoxSelectedIndexChangedEventArgs e)
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
    }
}