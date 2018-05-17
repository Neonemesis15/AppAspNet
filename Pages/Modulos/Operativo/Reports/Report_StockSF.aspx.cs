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
    public partial class Report_StockSF : System.Web.UI.Page
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

            static_channel = cmbcanal.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", static_channel, compañia);

            cmbplanning.Items.Clear();
            cmbcategoria_producto.Items.Clear();
            cmbcategoria_producto.Enabled = false;
            cmbmarca.Items.Clear();
            cmbmarca.Enabled = false;
            cmbsku.Items.Clear();
            cmbsku.Enabled = false;
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

        protected void cmbplanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            string sidplanning = cmbplanning.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);
            if (cmbplanning.SelectedIndex != 0)
            {
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
                    cmbperson.Items.Insert(0, new ListItem("---Todos---", "0"));
                    cmbperson.Enabled = true;
                }

                //------llamado al metodo cargar categoria de producto
                cargarCombo_Oficina();
                cargarCombo_NodeComercial(sidplanning);
                cargarCombo_CategoriaDeproducto(compañia);
                //----------------------------------------------------
            }
            else
            {
                cmbcategoria_producto.Items.Clear();
                cmbcategoria_producto.Enabled = false;
                cmbmarca.Items.Clear();
                cmbmarca.Enabled = false;
                cmbsku.Items.Clear();
                cmbsku.Enabled = false;

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

        protected void cargarCombo_NodeComercial(string sid_planning)
        {
            try
            {
                cmbNodeComercial.Items.Clear();
                Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(cmbplanning.SelectedValue);

                if (oListNodeComercial.Length > 0)
                {
                    cmbNodeComercial.Enabled = true;
                    cmbNodeComercial.DataSource = oListNodeComercial;
                    cmbNodeComercial.DataTextField = "commercialNodeName";
                    cmbNodeComercial.DataValueField = "NodeCommercial";
                    cmbNodeComercial.DataBind();

                    cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void cmbNodeComercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Conexion Ocoon = new Conexion();


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
            cmbsku.Items.Clear();
            cmbsku.Enabled = false;
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

        protected void cmbmarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            //string iischannel = cmbcanal.SelectedValue;
            //string sidplanning = cmbplanning.SelectedValue;
            string sidmarca = cmbmarca.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SKU_PRODUCTO_BY_BRAND_ID", sidmarca);

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
            //cargarGrilla_Precio();
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrilla_Precio();
        }

        protected void cargarGrilla_Precio()
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
                string sidmarca = cmbmarca.SelectedValue;
                if (sidcategoriaProducto == "")
                    sidcategoriaProducto = "0";
                string scodproducto = cmbsku.SelectedValue;
                if (scodproducto == "")
                    scodproducto = "0";
                if (sidmarca == "")
                    sidmarca = "0";


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

                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_PRECIO", iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, scodproducto, dfecha_inicio, dfecha_fin);
                    //dt = Ocoon.ejecutarDataTable("prueba2", iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, scodproducto, dfecha_inicio, dfecha_fin);
                    //dt = Ocoon.ejecutarDataTable("prueba2", 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_POR_DIA", iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, scodproducto, dfecha_inicio);


                    if (dt.Rows.Count > 0)
                    {
                        gv_precios.DataSource = dt;
                        gv_precios.DataBind();


                        gv_precioToExcel.DataSource = dt;
                        gv_precioToExcel.DataBind();

                        lblmensaje.ForeColor = System.Drawing.Color.Green;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Blue;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                        gv_precios.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
                gv_precios.DataBind();

                //System.Threading.Thread.Sleep(8000);
                //Response.Redirect("~/err_mensaje_seccion.aspx", true);

            }

        }
        
        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                
                gv_precioToExcel.Visible = true;
                GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Stock_por_Dia", this.gv_precioToExcel);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void cmbsku_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_validar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gv_precios.Items.Count; i++)
            {
                GridItem item = gv_precios.Items[i];
                // if (item.ItemType == GridItemType.Item)
                //{

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");

                Label lbl_validar = (Label)item.FindControl("lbl_validar");
                Label lbl_Id_Detalle_Precio = (Label)item.FindControl("lbl_Id_Detalle_Precio");


                int id = Convert.ToInt32(lbl_Id_Detalle_Precio.Text);
                bool validar = cb_validar.Checked;
                //update_precio_detalle_validado(id, validar);
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
        }

        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox cb_all = (CheckBox)gv_ventas.FindControl("cb_all");
            bool validar = cb_all.Checked;

            for (int i = 0; i < gv_precios.Items.Count; i++)
            {
                GridItem item = gv_precios.Items[i];
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

        //protected void update_precio_detalle_validado(int id, bool validar)
        //{

        //    try
        //    {
        //        Conexion Ocoon = new Conexion();

        //        Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_PRECIO_DETALLE_VALIDADO", id, validar);

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //    }
        //}

        protected void update_stock_detalle_validado(int id, bool validar) {
            try
            {
                Conexion OCoon = new Conexion();
                OCoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_STOCK_DETALLE_VALIDADO", id, validar);
            }
            catch(Exception ex) {
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

        protected void gv_precios_CancelCommand(object source, GridCommandEventArgs e)
        {
            cargarGrilla_Precio();
        }

        protected void gv_precios_DataBound(object sender, EventArgs e)
        {
            if (gv_precios.Items.Count > 0)
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

        protected void gv_precios_EditCommand(object source, GridCommandEventArgs e)
        {
            cargarGrilla_Precio();
        }

        protected void gv_precios_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            gv_precios.CurrentPageIndex = e.NewPageIndex;
            cargarGrilla_Precio();
           
        }

        protected void gv_precios_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            gv_precios.CurrentPageIndex =e.NewPageSize;
            cargarGrilla_Precio();
        }

        protected void gv_precios_PdfExporting(object source, GridPdfExportingArgs e)
        {

        }

        protected void btn_validar_Click_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gv_precios.Items.Count; i++)
            {
                GridItem item = gv_precios.Items[i];
                // if (item.ItemType == GridItemType.Item)
                //{

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");

                Label lbl_validar = (Label)item.FindControl("lbl_validar");
                Label lbl_id_StockDetalle = (Label)item.FindControl("lbl_id_StockDetalle");


                if (cb_validar.Enabled == true)
                {

                    int id = Convert.ToInt32(lbl_id_StockDetalle.Text);
                    bool validar = cb_validar.Checked;

                    if (validar == true)
                    {
                        update_stock_detalle_validado(id, validar);
                     
                        lbl_validar.Text = "valido";
                        lbl_validar.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        update_stock_detalle_validado(id, validar);
                       
                        lbl_validar.Text = "invalidado";
                        lbl_validar.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
        }

        protected void gv_precios_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                lblmensaje.Text = "";
                Conexion Ocoon = new Conexion();

                GridItem item = gv_precios.Items[e.Item.ItemIndex];

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
                DateTime Fec_reg_bd = Convert.ToDateTime((ArrayEditorValue[0] as RadDateTimePicker).SelectedDate);

               
                string ingreso = ArrayEditorValue[1].ToString();
                string pedido = ArrayEditorValue[2].ToString();


                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_STOCK_SANFERNDO", iid_det, pedido, ingreso, Fec_reg_bd, Session["sUser"].ToString(), DateTime.Now, ckvalidado.Checked);
                
                cargarGrilla_Precio();
            }
            catch (Exception ex)
            {
                lblmensaje.Text = ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

    }

       
}
