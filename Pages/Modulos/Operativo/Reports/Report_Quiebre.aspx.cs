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
    public partial class Report_Quiebre : System.Web.UI.Page
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
                CargarCanal();
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

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel,compañia);

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
            //-------
            lblmensaje.Visible = true;
         
            //-------
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



                    dt = obj_Facade_Proceso_Operativo.Get_ReporteQuiebre(iidperson, sidplanning, sidchannel,icod_oficina,iidNodeComercial,sidPDV, sidcategoriaProducto, iidmarca, scodproducto, dfecha_inicio, dfecha_fin,compañia);
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
                gv_quibre.DataBind();
                lblmensaje.Text = "";
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
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
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

                CheckBox ckvalidado = (CheckBox)row.FindControl("cb_validar");

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_QUIEBRE_DETALLE", lbl_Id_Quiebre_Detall.Text.Trim(), RadDateTimePicker_fec_reg.DbSelectedDate, Session["sUser"].ToString(), DateTime.Now, ckvalidado.Checked);

                gv_quibre.EditIndex = -1;
                cargarGrilla_Quiebre();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_quiebre_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_quibre.EditIndex = -1;
            cargarGrilla_Quiebre();
        }

        #region Crear uno a uno
        protected void CargarCanal()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            pais = this.Session["scountry"].ToString();


            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CHANNEL", pais, compañia);
            if (dt.Rows.Count > 0)
            {
                ddlCanal.DataSource = dt;
                ddlCanal.DataValueField = "cod_Channel";
                ddlCanal.DataTextField = "Channel_Name";
                ddlCanal.DataBind();
                ddlCanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));


                ddlCanalCargaMasiva.DataSource = dt;
                ddlCanalCargaMasiva.DataValueField = "cod_Channel";
                ddlCanalCargaMasiva.DataTextField = "Channel_Name";
                ddlCanalCargaMasiva.DataBind();
                ddlCanalCargaMasiva.Items.Insert(0, new ListItem("---Seleccione---", "0"));

            }
        }


        protected void ddlCanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            compañia = Convert.ToInt32(this.Session["companyid"]);

            string sidchannel = ddlCanal.SelectedValue;

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);

            ddlCampana.Items.Clear();
            ddlCategoria.Items.Clear();
            ddlCategoria.Enabled = false;
            ddlMarca.Items.Clear();
            ddlMarca.Enabled = false;


            ddlOficina.Items.Clear();
            ddlOficina.Enabled = false;
            ddlNodeComercial.Items.Clear();
            ddlNodeComercial.Enabled = false;
            ddlPuntoVenta.Items.Clear();
            ddlPuntoVenta.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                ddlCampana.DataSource = dt;
                ddlCampana.DataValueField = "id_planning";
                ddlCampana.DataTextField = "Planning_Name";
                ddlCampana.DataBind();
                ddlCampana.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlCampana.Enabled = true;
            }
            MopoReport.Show();
        }
        public void cargarOficina()
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
                        ddlOficina.Enabled = true;
                        ddlOficina.DataSource = dtofi;
                        ddlOficina.DataTextField = "Name_Oficina";
                        ddlOficina.DataValueField = "cod_Oficina";
                        ddlOficina.DataBind();

                        ddlOficina.Items.Insert(0, new ListItem("---Seleccione---", "0"));
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


        protected void cargarNodeComercial(string sid_planning)
        {
            try
            {
                cmbNodeComercial.Items.Clear();
                Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(sid_planning);

                if (oListNodeComercial.Length > 0)
                {
                    ddlNodeComercial.Enabled = true;
                    ddlNodeComercial.DataSource = oListNodeComercial;
                    ddlNodeComercial.DataTextField = "commercialNodeName";
                    ddlNodeComercial.DataValueField = "NodeCommercial";
                    ddlNodeComercial.DataBind();

                    ddlNodeComercial.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                    MopoReport.Show();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        protected void cargarCategoria(string sidplanning)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_STOCK", sidplanning);
            ddlCategoria.Enabled = false;
            ddlMarca.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                ddlCategoria.DataSource = dt;
                ddlCategoria.DataValueField = "id_ProductCategory";
                ddlCategoria.DataTextField = "Product_Category";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlCategoria.Enabled = true;
            }
        }




        protected void ddlCampana_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            string sidplanning = ddlCampana.SelectedValue;

            if (ddlCampana.SelectedIndex != 0)
            {
                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);

                ddlCategoria.Items.Clear();
                ddlCategoria.Enabled = false;
                ddlMarca.Items.Clear();
                ddlMarca.Enabled = false;


                //------llamado al metodo cargar categoria de producto
                cargarOficina();
                cargarNodeComercial(sidplanning);
                cargarCategoria(sidplanning);
                //----------------------------------------------------

            }
            else
            {
                ddlCategoria.Items.Clear();
                ddlCategoria.Enabled = false;
                ddlMarca.Items.Clear();
                ddlMarca.Enabled = false;


                ddlOficina.Items.Clear();
                ddlOficina.Enabled = false;
                ddlNodeComercial.Items.Clear();
                ddlNodeComercial.Enabled = false;
                ddlPuntoVenta.Items.Clear();
                ddlPuntoVenta.Enabled = false;
            }
            MopoReport.Show();
        }


        protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Conexion Ocoon = new Conexion();

                ddlPuntoVenta.Items.Clear();
                ddlPuntoVenta.Enabled = false;
                if (ddlCampana.SelectedIndex > 0 && ddlOficina.SelectedIndex > 0)
                {
                    DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", ddlCampana.SelectedValue, Convert.ToInt32(ddlOficina.SelectedValue));

                    if (dtPdv.Rows.Count > 0)
                    {
                        ddlPuntoVenta.DataSource = dtPdv;
                        ddlPuntoVenta.DataValueField = "ClientPDV_Code";
                        ddlPuntoVenta.DataTextField = "pdv_Name";
                        ddlPuntoVenta.DataBind();

                        ddlPuntoVenta.Items.Insert(0, new ListItem("---Seleccione---", "0"));

                        ddlPuntoVenta.Enabled = true;
                    }

                    DataTable dtNodeCom = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_NODECOMERCIAL_BY_idPlanning_and_codOficina", ddlCampana.SelectedValue, Convert.ToInt32(ddlOficina.SelectedValue));

                    if (dtNodeCom.Rows.Count > 0)
                    {
                        ddlNodeComercial.Enabled = true;
                        ddlNodeComercial.Items.Clear();
                        ddlNodeComercial.DataSource = dtNodeCom;
                        ddlNodeComercial.DataTextField = "commercialNodeName";
                        ddlNodeComercial.DataValueField = "id_NodeCommercial";
                        ddlNodeComercial.DataBind();

                        ddlNodeComercial.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                    }
                    else
                    {
                        ddlNodeComercial.Enabled = false;
                        ddlNodeComercial.Items.Clear();
                    }
                }
                if (ddlOficina.SelectedIndex == 0 && ddlCampana.SelectedIndex > 0)
                {
                    cargarCombo_NodeComercial(ddlCampana.SelectedValue);
                }
                MopoReport.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlNodeComercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Conexion Ocoon = new Conexion();


                if (ddlCampana.SelectedIndex > 0 && ddlNodeComercial.SelectedIndex > 0)
                {
                    DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_and_idNodeComercial", ddlCampana.SelectedValue, Convert.ToInt32(ddlNodeComercial.SelectedValue));

                    if (dtPdv.Rows.Count > 0)
                    {
                        ddlPuntoVenta.DataSource = dtPdv;
                        ddlPuntoVenta.DataValueField = "ClientPDV_Code";
                        ddlPuntoVenta.DataTextField = "pdv_Name";
                        ddlPuntoVenta.DataBind();

                        ddlPuntoVenta.Items.Insert(0, new ListItem("---Seleccione---", "0"));

                        ddlPuntoVenta.Enabled = true;
                    }
                }
                if (ddlNodeComercial.SelectedIndex == 0)
                {
                    DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", ddlCampana.SelectedValue, Convert.ToInt32(ddlOficina.SelectedValue));

                    if (dtPdv.Rows.Count > 0)
                    {
                        ddlPuntoVenta.DataSource = dtPdv;
                        ddlPuntoVenta.DataValueField = "ClientPDV_Code";
                        ddlPuntoVenta.DataTextField = "pdv_Name";
                        ddlPuntoVenta.DataBind();

                        ddlPuntoVenta.Items.Insert(0, new ListItem("---Seleccione---", "0"));

                        ddlPuntoVenta.Enabled = true;
                    }
                }
                MopoReport.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            string sidcategoriaproducto = ddlCategoria.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_REPORT_SOD", sidcategoriaproducto);

            ddlMarca.Items.Clear();
            ddlMarca.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                ddlMarca.DataSource = dt;
                ddlMarca.DataValueField = "id_Brand";
                ddlMarca.DataTextField = "Name_Brand";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("---Seleccionar---", "0"));
                ddlMarca.Enabled = true;
            }
            MopoReport.Show();
        }

        protected void btnGuardarReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCanal.Text == "0" || ddlCampana.Text == "0"
    || ddlNodeComercial.Text == "0" || ddlPuntoVenta.Text == "0" || ddlCategoria.Text == "0" || ddlProducto.Text == "0")
                {
                    return;
                }

                Lucky.Business.Common.Application.OPE_Reporte_SOD oOPE_Reporte_SOD = new Lucky.Business.Common.Application.OPE_Reporte_SOD();


                DataTable dt = null;
                DataTable dtl = null;
                Conexion Ocoon = new Conexion();
                Conexion con = new Conexion(2);
                dt = Ocoon.ejecutarDataTable("UP_WEB_SEARCH_USER", "", Convert.ToInt32(this.Session["personid"].ToString()));
                string idperfil = dt.Rows[0]["Perfil_id"].ToString();

                dtl = con.ejecutarDataTable("STP_JVM_LISTAR_CANALES", ddlCanal.SelectedValue);
                string tipocanal = dtl.Rows[0]["CAN_TIPO"].ToString();


                string sID_REG_QUIEBRE = "";

                sID_REG_QUIEBRE = con.ejecutarretornodeOUTPUT("STP_JVM_INSERTAR_QUIEBRE", 11, Convert.ToInt32(this.Session["personid"].ToString()), idperfil, ddlCampana.SelectedValue, this.Session["companyid"].ToString(), ddlPuntoVenta.SelectedValue, ddlCategoria.SelectedValue, ddlMarca.SelectedValue, DateTime.Now.ToShortDateString(), "0", "0", "0", sID_REG_QUIEBRE);


                con.ejecutarDataTable("STP_JVM_INSERTAR_QUIEBRE_DETALLE", Convert.ToInt32(sID_REG_QUIEBRE), ddlProducto.SelectedValue,  txtQuiebre.Text.ToUpper());


             

                LimpiarControles();
                lblmensaje.Visible = true;
                lblmensaje.Text = "Fue registrado con exito el reporte Quiebre";
                lblmensaje.ForeColor = System.Drawing.Color.Blue;
            }

            catch (Exception ex)
            {
                string error = "";
                string mensaje = "";
                error = Convert.ToString(ex.Message);
                // mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                if (error == mensaje)
                {
                    Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                    string errMessage = "";
                    errMessage = mensaje;
                    errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                    this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                }
                else
                {
                    this.Session.Abandon();
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            //string iischannel = cmbcanal.SelectedValue;
            //string sidplanning = cmbplanning.SelectedValue;
            string sidmarca = ddlMarca.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SKU_PRODUCTO_REPORT_QUIEBRE", sidmarca);

            ddlProducto.Items.Clear();
            ddlProducto.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                ddlProducto.DataSource = dt;
                ddlProducto.DataValueField = "cod_Product";
                ddlProducto.DataTextField = "productoNombre";
                ddlProducto.DataBind();
                ddlProducto.Items.Insert(0, new ListItem("---Todos---", "0"));
                ddlProducto.Enabled = true;
            }
            MopoReport.Show();
        } 


        void LimpiarControles()
        {

            ddlCanal.Items.Clear();
            ddlCampana.Items.Clear();
            ddlOficina.Items.Clear();
            ddlNodeComercial.Items.Clear();
            ddlPuntoVenta.Items.Clear();
            ddlCategoria.Items.Clear();
            ddlMarca.Items.Clear();
            ddlProducto.Items.Clear();
            txtQuiebre.Text = "";

            ddlCanalCargaMasiva.Items.Clear();
            ddlCampañaCargaMasiva.Items.Clear();

            CargarCanal();
        }

        #endregion



        #region cargamasiva


        protected void ddlCanalCargaMasiva_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();


            compañia = Convert.ToInt32(this.Session["companyid"]);
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", ddlCanalCargaMasiva.SelectedValue, compañia);

            ddlCampañaCargaMasiva.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                ddlCampañaCargaMasiva.DataSource = dt;
                ddlCampañaCargaMasiva.DataValueField = "id_planning";
                ddlCampañaCargaMasiva.DataTextField = "Planning_Name";
                ddlCampañaCargaMasiva.DataBind();
                ddlCampañaCargaMasiva.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlCampañaCargaMasiva.Enabled = true;
            }

            MopoReporMasivo.Show();
        }

        protected void ddlCampañaCargaMasiva_SelectedIndexChanged(object sender, EventArgs e)
        {
            Conexion cn = new Conexion();
            DataSet ds = cn.ejecutarDataSet("OPE_CARGAMASIVA_REPORTE_QUIEBRE_DATOS", ddlCampañaCargaMasiva.SelectedValue, "56");
            CreaExcel(ds);
            Datos.Visible = true;
            MopoReporMasivo.Show();
        }


        protected void btnCargaMasiva_Click(object sender, EventArgs e)
        {
            if ((FileUpCMasivo.PostedFile != null) && (FileUpCMasivo.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(FileUpCMasivo.PostedFile.FileName);
                string SaveLocation = Server.MapPath("masivo") + "\\" + fn;

                if (SaveLocation != string.Empty)
                {
                    if (FileUpCMasivo.FileName.ToLower().EndsWith(".xls"))
                    {
                        // string Destino = Server.MapPath(null) + "\\PDV_Planning\\" + Path.GetFileName(FileUpPDV.PostedFile.FileName);
                        OleDbConnection oConn1 = new OleDbConnection();
                        OleDbCommand oCmd = new OleDbCommand();
                        OleDbDataAdapter oDa = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();
                        DataTable dt = new DataTable();

                        FileUpCMasivo.PostedFile.SaveAs(SaveLocation);

                        // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                        oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                        oConn1.Open();
                        oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Ope_Quiebre"];
                        oCmd.Connection = oConn1;
                        oDa.SelectCommand = oCmd;

                        try
                        {

                            oDa.Fill(oDs);


                            dt = oDs.Tables[0];
                            int numcol = 5; //determina el número de columnas para el datatable
                            if (dt.Columns.Count == numcol)
                            {

                                dt.Columns[0].ColumnName = "cod_pdv";
                                dt.Columns[1].ColumnName = "cod_categoria";
                                dt.Columns[2].ColumnName = "cod_marca";
                                dt.Columns[3].ColumnName = "cod_producto";
                                dt.Columns[4].ColumnName = "quiebre";





                                string idPlanning = ddlCampañaCargaMasiva.SelectedValue;
                                string companyid = this.Session["companyid"].ToString();
                                string usuario = this.Session["sUser"].ToString().Trim();

                                // cargarCategoria(idPlanning);
                                //llenarPuntoVenta1(idPlanning, 0);

                                OPE_Reporte_SOD oOPE_Reporte_SOD = new OPE_Reporte_SOD();
                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    string cod_pdv = dt.Rows[i]["cod_pdv"].ToString();
                                    string cod_categoria = dt.Rows[i]["cod_categoria"].ToString();
                                    string cod_marca = dt.Rows[i]["cod_marca"].ToString();
                                    string cod_producto = dt.Rows[i]["cod_producto"].ToString();
                                    string quiebre = dt.Rows[i]["quiebre"].ToString();



                                    // llenarMarca1(id_Categoria);
                                    // llenaProducto(Convert.ToInt32(companyid), id_Categoria, Convert.ToInt32(id_Marca));

                                    //ddlPuntoVenta.Items.FindByValue(id_ClientPDV).Selected = true;
                                    //ddlCategoria.Items.FindByValue(id_Categoria).Selected = true;
                                    //  ddlMarca.Items.FindByValue(id_Marca).Selected = true;



                                    // Lucky.Business.Common.Application.OPE_Reporte_SOD oOPE_Reporte_SOD = new Lucky.Business.Common.Application.OPE_Reporte_SOD();


                                    //DataTable dt = null;
                                    //DataTable dtl = null;
                                    Conexion Ocoon = new Conexion();
                                    Conexion con = new Conexion(2);
                                    dt = Ocoon.ejecutarDataTable("UP_WEB_SEARCH_USER", "", Convert.ToInt32(this.Session["personid"].ToString()));
                                    string idperfil = dt.Rows[0]["Perfil_id"].ToString();

                                    // dtl = con.ejecutarDataTable("STP_JVM_LISTAR_CANALES", ddlCanal.SelectedValue);
                                    // string tipocanal = dtl.Rows[0]["CAN_TIPO"].ToString();


                                    string sID_REG_QUIEBRE = "";

                                    sID_REG_QUIEBRE = con.ejecutarretornodeOUTPUT("STP_JVM_INSERTAR_QUIEBRE", 11, Convert.ToInt32(this.Session["personid"].ToString()), idperfil, ddlCampañaCargaMasiva.SelectedValue, this.Session["companyid"].ToString(), cod_pdv, cod_categoria, cod_marca, DateTime.Now.ToShortDateString(), "0", "0", "0", sID_REG_QUIEBRE);


                                    con.ejecutarDataTable("STP_JVM_INSERTAR_QUIEBRE_DETALLE", Convert.ToInt32(sID_REG_QUIEBRE), cod_producto, quiebre.ToUpper());


                                }

                                LimpiarControles();
                                lblmensaje.Visible = true;
                                lblmensaje.Text = "Fue registrado con exito el reporte Quiebre";
                                lblmensaje.ForeColor = System.Drawing.Color.Blue;


                                //realiza las validaciones y carga los productos a planning.
                                //DataSet dsCargar = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CARGAMASIVA_PRECIO_PLANNING_TMP",
                                //idPlanning,
                                //Convert.ToInt32(id_ReportsPlanning),
                                //usuario, DateTime.Now,
                                //usuario, DateTime.Now);

                            }
                            //}
                        }
                        catch
                        {
                            lblmensaje.Text = "Por favor revise la consistencia de la información";
                        }
                    }
                }
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

                sFile = Server.MapPath("masivo") + "\\" + "DATOS_CARGA_REPORTE_QUIEBRE.xls";

                sTemplate = Server.MapPath("masivo") + "\\" + "Template.xls";

                oExcel.Visible = false;

                oExcel.DisplayAlerts = false;

                // Abrimos un nuevo libro

                oLibros = oExcel.Workbooks;

                oLibros.Open(sTemplate);

                oLibro = oLibros.Item[1];

                oHojas = oLibro.Worksheets;


                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    oHoja = (Microsoft.Office.Interop.Excel.Worksheet)oHojas.Item[i + 1];

                    oHoja.Name = "Hoja" + (i + 1);


                    string col = "";
                    int columnas = ds.Tables[i].Columns.Count;
                    switch (columnas)
                    {
                        case 1:
                            col="A";
                            break;
                        case 2:
                            col = "B";
                            break;
                        case 3:
                            col = "C";
                            break;
                        case 4:
                            col = "D";
                            break;
                        case 5:
                            col = "E";
                            break;
                        case 6:
                            col = "F";
                            break;
                     
                    }



                    oCeldas = oHoja.Cells;
                    //oHoja.Range["B2"].Interior.Color = 0;
                    //oHoja.Range["B2"].Font.Color = 16777215;
                    oHoja.Range["A2", col+"2"].Interior.Color = 0;
                    oHoja.Range["A2", col+"2"].Font.Color = 16777215;


                    oHoja.Range["A2", col+"2"].Font.Bold = true;
                    //oHoja.Range["A2"].Font.Bold = true;

                    oHoja.Range["A2", col + (ds.Tables[i].Rows.Count + 2).ToString()].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDash;
                    oHoja.Range["A2", col + (ds.Tables[i].Rows.Count + 2).ToString()].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;




                    VuelcaDatos(ds.Tables[i], oCeldas);


                }



                oHoja.SaveAs(sFile);

                oLibro.Close();

                // Eliminamos lo que hemos creado

                oExcel.Quit();

                oExcel = null;

                oLibros = null;

                oLibro = null;

                oHojas = null;

                oHoja = null;

                oCeldas = null;

                System.GC.Collect();

            }
            catch
            {
                oLibro.Close();
                oExcel.Quit();

                lblmensaje.Text = "se cargo correctamente";
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
