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

namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_Impulso_SF_M : System.Web.UI.Page
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
            cargarGrilla_Impulso();
        }
        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //cargarGrilla_Impulso_to_Excel();
                gv_impulsoToExcel.Visible = true;
                GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Stock", this.gv_impulsoToExcel);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void cargarGrilla_Impulso()
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

                string iidcorporacion = cmbcorporacion.SelectedValue;
                if (iidcorporacion == "")
                    iidcorporacion = "0";

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


                    Conexion Ocoon = new Conexion();

                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_INGRESOS", dfecha_inicio, dfecha_fin, sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidmarca, sidcategoriaProducto, iidperson);
                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_INGRESOS3", dfecha_inicio, dfecha_fin, sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidmarca, sidcategoriaProducto, iidperson);
                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_IMPULSO_SANFERNANDO_MODERNO", sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, dfecha_inicio, dfecha_fin);
                    //UP_WEBXPLORA_OPE_CONSULTA_IMPULSO_SANFERNANDO_MODERNO_ModiBy_pSalas
                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_IMPULSO_SANFERNANDO_MODERNO", sidchannel, sidplanning, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, dfecha_inicio, dfecha_fin, 0);
                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_IMPULSO_SANFERNANDO_MODERNO_ModiBy_pSalas", sidplanning,  0, 0, 0, "0", 0, 0, dfecha_inicio, dfecha_fin, "0");



                    if (dt.Rows.Count > 0)
                    {
                        gv_impulso.DataSource = dt;
                        gv_impulso.DataBind();


                        gv_impulsoToExcel.DataSource = dt;
                        gv_impulsoToExcel.DataBind();

                        lblmensaje.ForeColor = System.Drawing.Color.Green;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Blue;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                        gv_impulso.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
                gv_impulso.DataBind();

                //System.Threading.Thread.Sleep(8000);
                //Response.Redirect("~/err_mensaje_seccion.aspx", true);

            }

        }
        protected void cargarGrilla_Impulso_to_Excel()
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

                string iidcorporacion = cmbcorporacion.SelectedValue;
                if (iidcorporacion == "")
                    iidcorporacion = "0";



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
                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_IMPULSO_SANFERNANDO_MODERNO_TO_EXCEL", sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, dfecha_inicio, dfecha_fin);
                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_INGRESOS_TO_EXCEL", dfecha_inicio, dfecha_fin, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca);


                    if (dt.Rows.Count > 0)
                    {
                        //gv_stock.DataSource = dt;
                        //gv_stock.DataBind();


                        gv_impulsoToExcel.DataSource = dt;
                        gv_impulsoToExcel.DataBind();

                        lblmensaje.ForeColor = System.Drawing.Color.Green;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Blue;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                        gv_impulso.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
                gv_impulso.DataBind();

                //System.Threading.Thread.Sleep(8000);
                //Response.Redirect("~/err_mensaje_seccion.aspx", true);

            }

        }

        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox cb_all = (CheckBox)gv_ventas.FindControl("cb_all");
            bool validar = cb_all.Checked;

            for (int i = 0; i < gv_impulso.Items.Count; i++)
            {
                GridItem item = gv_impulso.Items[i];
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

        protected void gv_precios_CancelCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            cargarGrilla_Impulso();
        }
        protected void gv_precios_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            cargarGrilla_Impulso();
        }
        protected void gv_precios_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            cargarGrilla_Impulso();
        }
        protected void gv_precios_PageSizeChanged(object source, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            cargarGrilla_Impulso();
        }
        protected void gv_precios_PdfExporting(object source, Telerik.Web.UI.GridPdfExportingArgs e)
        {

        }

        protected void update_impulso_detalle_validado(int idrimpulsodet, bool validar)
        {
            try
            {
                Conexion OCoon = new Conexion();
                OCoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_IMPULSO_DETALLE_VALIDADO2", idrimpulsodet, validar);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();

            }


        }
        protected void btn_validar_Click_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gv_impulso.Items.Count; i++)
            {
                GridItem item = gv_impulso.Items[i];
                // if (item.ItemType == GridItemType.Item)
                //{

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");
                Label lbl_validar = (Label)item.FindControl("lbl_validar");
                Label lbl_id_rimpulsodet = (Label)item.FindControl("lblrimpulsodet");
                //Label lbl_id_IngresosDetalle = (Label)item.FindControl("lblregingresodet");


                if (cb_validar.Enabled == true)
                {

                    int idregimpulsodet = Convert.ToInt32(lbl_id_rimpulsodet.Text);
                    
                    bool validar = cb_validar.Checked;

                    if (validar == true)
                    {
                        update_impulso_detalle_validado(idregimpulsodet, validar);

                        lbl_validar.Text = "valido";
                        lbl_validar.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        update_impulso_detalle_validado(idregimpulsodet, validar);

                        lbl_validar.Text = "invalidado";
                        lbl_validar.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
        }
        protected void gv_precios_DataBound(object sender, EventArgs e)
        {
            if (gv_impulso.Items.Count > 0)
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

                GridItem item = gv_impulso.Items[e.Item.ItemIndex];

                Label lbl_id_rimpulsodet = (Label)item.FindControl("lblrimpulsodet");
                int iid_det = Convert.ToInt32(lbl_id_rimpulsodet.Text.Trim());
                

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

                string ingreso = ArrayEditorValue[0].ToString();
                string stockfinal = ArrayEditorValue[1].ToString();
                DateTime Fec_reg_bd = Convert.ToDateTime((ArrayEditorValue[2] as RadDateTimePicker).SelectedDate);
                
                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_IMPULSO_SANFERNANDO", iid_det, ingreso,stockfinal, Fec_reg_bd, Session["sUser"].ToString(), DateTime.Now, ckvalidado.Checked);
                cargarGrilla_Impulso();

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
        protected void cargarCombo_corporaciones(string sid_planning)
        {

            cmbcorporacion.Items.Clear();
            DataTable dt = new DataTable();
            //UP_WEBXPLORA_AD_OBTENER_CORPORACION_BY_idPlanning
            Conexion Ocoon = new Conexion();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_CORPORACION_BY_idPlanning", sid_planning);
            if (dt.Rows.Count > 0)
            {
                cmbcorporacion.DataSource = dt;
                cmbcorporacion.DataValueField = "corp_id";
                cmbcorporacion.DataTextField = "corp_name";
                cmbcorporacion.DataBind();
                cmbcorporacion.Items.Insert(0, new ListItem("---Todas---", "0"));
            }

        }

        //protected void cargarCombo_CategoriaDeproducto(string sidplanning)
        //{
        //    DataTable dt = null;
        //    Conexion Ocoon = new Conexion();

        //    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_COMPETENCIA", sidplanning);
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

        protected void cargarCombo_CategoriaDeproducto(int compañia)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_BY_COMPANY_ID", compañia);
            cmbcategoria_producto.Enabled = false;
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
        protected void cmbfamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        protected void cmbcorporacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbNodeComercial.Items.Clear();
            DataTable dt = new DataTable();
            Conexion Ocoon = new Conexion();
            string sidcorporacion = cmbcorporacion.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_NODOCOMERCIAL_BY_CORPORACION_ID", sidcorporacion);
            if (dt.Rows.Count > 0)
            {
                cmbNodeComercial.DataSource = dt;
                cmbNodeComercial.DataValueField = "id_NodeCommercial";
                cmbNodeComercial.DataTextField = "commercialNodeName";
                cmbNodeComercial.DataBind();
                cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbNodeComercial.Enabled = true;
            }
        }
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

                //------llamado al metodo cargar categoria de producto
                cargarCombo_Oficina();
                cargarCombo_NodeComercial(sidplanning);
                cargarCombo_CategoriaDeproducto(compañia);
                cargarCombo_corporaciones(sidplanning);
                //----------------------------------------------------

            }
            else
            {
                cmbcategoria_producto.Items.Clear();
                cmbcategoria_producto.Enabled = false;
              
                cmbperson.Items.Clear();
                cmbperson.Enabled = false;

                cmbOficina.Items.Clear();
                cmbOficina.Enabled = false;
                cmbNodeComercial.Items.Clear();
                cmbNodeComercial.Enabled = false;
                cmbPuntoDeVenta.Items.Clear();
                cmbPuntoDeVenta.Enabled = false;
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
      
    }
}