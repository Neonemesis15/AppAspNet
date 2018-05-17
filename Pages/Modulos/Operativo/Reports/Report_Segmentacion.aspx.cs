using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Lucky.Data;
using Telerik.Web.UI;
using Lucky.CFG.Util;

namespace SIGE.Pages.Modulos.Operativo.Reports
{
	public partial class Report_Segmentacion : System.Web.UI.Page
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
                cargarCombo_CategoriaDeproducto(sidplanning);
            }
            else
            {
                cmbcategoria_producto.Items.Clear();
                cmbcategoria_producto.Enabled = false;
                cmbperson.Items.Clear();
                cmbperson.Enabled = false;
                cmbmarca.Items.Clear();
                cmbmarca.Enabled = false;
                cmbsku.Items.Clear();
                cmbsku.Enabled = false;


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
                cmbmarca.Items.Insert(0, new ListItem("---Todas---", "0"));
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
           // cargarGrilla_Quiebre();
        }
        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrilla_Quiebre();
        }
        protected void cargarGrilla_Quiebre()
        {
            try
            {

                DataTable dt = null;

                int iidperson = Convert.ToInt32(cmbperson.SelectedValue);
                int icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);
                string sidplanning = cmbplanning.SelectedValue;
                string sidchannel = cmbcanal.SelectedValue;
                int iidNodeComercial = Convert.ToInt32(cmbNodeComercial.SelectedValue);
                string sidPDV = cmbPuntoDeVenta.SelectedValue;
                if (sidPDV == "")
                    sidPDV = "0";
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



                    dt = obj_Facade_Proceso_Operativo.Get_ReporteQuiebre(iidperson, sidplanning, sidchannel,icod_oficina,iidNodeComercial,sidPDV, sidcategoriaProducto, iidmarca, scodproducto, dfecha_inicio, dfecha_fin, compañia);
                    //Conexion Ocoon = new Conexion();
                    //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_QUIEBRE", iidperson, sidplanning, sidchannel, sidcategoriaProducto, iidmarca, scodproducto, sfecha_inicio, sfecha_fin);
                    gv_quibre.DataSource = dt;
                    gv_quibre.DataBind();
                    gv_quiebreToExcel.DataSource = dt;
                    gv_quiebreToExcel.DataBind();

                    //GridView2.Visible = false;
                    lblmensaje.Visible = true;
                    lblmensaje.Text = "Se encontro " + dt.Rows.Count + " resultados";
                    lblmensaje.ForeColor = System.Drawing.Color.Green;
                }
            }

            catch (Exception ex)
            {
                Exception mensaje = ex;
                lblmensaje.Text = "";
                gv_quibre.DataBind();
                if (cmbplanning.SelectedIndex > 0)
                    lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
            }
        }


        protected void gv_quibre_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_quibre.PageIndex = e.NewPageIndex;
            cargarGrilla_Quiebre();
        }

        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                gv_quiebreToExcel.Visible = true;
                GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Quiebre", this.gv_quiebreToExcel);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //}
        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb_all = (CheckBox)gv_quibre.HeaderRow.FindControl("cb_all");
            bool validar = cb_all.Checked;

            for (int i = 0; i < gv_quibre.Rows.Count; i++)
            {
                GridViewRow row = gv_quibre.Rows[i];
                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox cb_validar = (CheckBox)row.FindControl("cb_validar");

                    if (validar == true && cb_validar.Enabled == true)
                    {
                        cb_validar.Checked = true;
                    }
                    else if (validar == false && cb_validar.Enabled == true)
                    {
                        cb_validar.Checked = false;
                    }

                }
            }
        }
        protected void btn_validar_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < gv_quibre.Rows.Count; i++)
            {
                GridViewRow row = gv_quibre.Rows[i];
                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox cb_validar = (CheckBox)row.FindControl("cb_validar");
                    Label lbl_validar = (Label)row.FindControl("lbl_validar");
                    Label lbl_Id_Quiebre_Detall = (Label)row.FindControl("lbl_Id_Quiebre_Detall");


                    int id = Convert.ToInt32(lbl_Id_Quiebre_Detall.Text.Trim());
                    bool validar = cb_validar.Checked;

                    update_quiebre_detalle_validado(id, validar);
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
            //cargarGrilla_Quiebre();
        }
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

        protected void gv_quiebre_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_quibre.EditIndex = e.NewEditIndex;

            cargarGrilla_Quiebre();

            GridViewRow row = gv_quibre.Rows[gv_quibre.EditIndex];

            Label lbl_fec_Reg = row.FindControl("lbl_fec_Reg") as Label;

            RadDateTimePicker RadDateTimePicker_fec_reg = (RadDateTimePicker)row.FindControl("RadDateTimePicker_fec_reg");
            RadDateTimePicker_fec_reg.Visible = true;
            RadDateTimePicker_fec_reg.DbSelectedDate = Convert.ToDateTime(lbl_fec_Reg.Text);

            lbl_fec_Reg.Visible = false;
        }
        protected void gv_quiebre_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Conexion Ocoon = new Conexion();

            try
            {
                GridViewRow row = gv_quibre.Rows[gv_quibre.EditIndex];

                Label lbl_Id_Quiebre_Detall = (Label)row.FindControl("lbl_Id_Quiebre_Detall");

                RadDateTimePicker RadDateTimePicker_fec_reg = (RadDateTimePicker)row.FindControl("RadDateTimePicker_fec_reg");

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_QUIEBRE_DETALLE", lbl_Id_Quiebre_Detall.Text.Trim(), RadDateTimePicker_fec_reg.DbSelectedDate, Session["sUser"].ToString(), DateTime.Now);

                gv_quibre.EditIndex = -1;
                cargarGrilla_Quiebre();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void gv_quiebre_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_quibre.EditIndex = -1;
            cargarGrilla_Quiebre();
        }



    }
}
