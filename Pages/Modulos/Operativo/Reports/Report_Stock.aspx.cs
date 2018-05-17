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
    public partial class Report_Stock : System.Web.UI.Page
    {
        #region Declaracion de variables generales
        private int compañia;
        private string pais;
        Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
 
        int iidperson;
        int icod_oficina;
        int iidNodeComercial;
        string sidPDV;
        string sidplanning;
        string sidchannel;
        string sidcategoriaProducto;
        string sidfamilia;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                CargarCombo_Channel();
                CargarCanal();
                Años();
                Llena_Meses();
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

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel,compañia);

            cmbplanning.Items.Clear();
            cmbcategoria_producto.Items.Clear();
            cmbcategoria_producto.Enabled = false;
            cmbfamilia.Items.Clear();
            cmbfamilia.Enabled = false;
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
                cmbcategoria_producto.Enabled = false;
                cmbfamilia.Items.Clear();
                cmbfamilia.Enabled = false;
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
                cmbfamilia.Items.Clear();
                cmbfamilia.Enabled = false;
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

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_STOCK", sidplanning);
            cmbcategoria_producto.Enabled = false;
            cmbfamilia.Enabled = false;
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

            string sidproductCategory = cmbcategoria_producto.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK", sidproductCategory);

            cmbfamilia.Items.Clear();
            cmbfamilia.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                cmbfamilia.DataSource = dt;
                cmbfamilia.DataValueField = "id_ProductFamily";
                cmbfamilia.DataTextField = "name_Family";
                cmbfamilia.DataBind();
                cmbfamilia.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbfamilia.Enabled = true;
            }
            //cargarGrilla_Stock();
        }
        
        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrilla_Stock();
        }
        protected void cargarGrilla_Stock()
        {
            try
            {

                DataTable dt = null;

                iidperson = Convert.ToInt32(cmbperson.SelectedValue);

                icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);
                iidNodeComercial = Convert.ToInt32(cmbNodeComercial.SelectedValue);
                sidPDV = cmbPuntoDeVenta.SelectedValue;
                if (sidPDV == "")
                    sidPDV = "0";
                sidplanning = cmbplanning.SelectedValue;
                sidchannel = cmbcanal.SelectedValue;
                sidcategoriaProducto = cmbcategoria_producto.SelectedValue;
                if (sidcategoriaProducto == "")
                    sidcategoriaProducto = "0";
                sidfamilia = cmbfamilia.SelectedValue;
                if (sidfamilia == "")
                    sidfamilia = "0";


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
                    dt = obj_Facade_Proceso_Operativo.Get_ReporteStock(iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidfamilia, dfecha_inicio, dfecha_fin);


                    gv_stock.DataSource = dt;
                    gv_stock.DataBind();
                    gv_stock.Visible = true;
                    gv_stockToExcel.DataSource = dt;
                    gv_stockToExcel.DataBind();
                    gv_faltantes.Visible = false;
                    //controlesvalidez.Style.Value = "display:block";
                    //GridView2.Visible = false;
                    lblmensaje.Text = "Se encontro " + dt.Rows.Count + " resultados";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                gv_stock.DataBind();
                lblmensaje.Text = "";
                if (cmbplanning.SelectedIndex > 0)
                    lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";

            }

        }
        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try{

            gv_stockToExcel.Visible = true;
            GridViewExportUtil.ExportToExcelMethodTwo("Reporte_stock", this.gv_stockToExcel);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        protected void gv_stock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_stock.PageIndex = e.NewPageIndex;
            cargarGrilla_Stock();
        }
        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb_all = (CheckBox)gv_stock.HeaderRow.FindControl("cb_all");
            bool validar = cb_all.Checked;

            for (int i = 0; i < gv_stock.Rows.Count; i++)
            {
                GridViewRow row = gv_stock.Rows[i];
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

            for (int i = 0; i < gv_stock.Rows.Count; i++)
            {
                GridViewRow row = gv_stock.Rows[i];
                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox cb_validar = (CheckBox)row.FindControl("cb_validar");

                    Label lbl_validar = (Label)row.FindControl("lbl_validar");
                    Label lbl_dlbl_id_StockDetalle = (Label)row.FindControl("lbl_id_StockDetalle");

                    int iid_StockDetalle = Convert.ToInt32(lbl_dlbl_id_StockDetalle.Text);
                    bool validar = cb_validar.Checked;
                    update_stock_detalle_validado(iid_StockDetalle, validar);
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
        }
        protected void update_stock_detalle_validado(int id, bool validar)
        {

            try
            {
                Conexion Ocoon = new Conexion();

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_STOCK_DETALLE_VALIDADO", id, validar, Session["sUser"].ToString(), DateTime.Now);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void gv_stock_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_stock.EditIndex = e.NewEditIndex;

            cargarGrilla_Stock();
            
            GridViewRow row = gv_stock.Rows[gv_stock.EditIndex];

            Label lbl_fec_Reg = row.FindControl("lbl_fec_Reg") as Label;
            RadNumericTextBox txt_gvs_cantidad = (RadNumericTextBox)row.FindControl("txt_gvs_cantidad");
            txt_gvs_cantidad.Enabled = true;
            RadDateTimePicker RadDateTimePicker_fec_reg = (RadDateTimePicker)row.FindControl("RadDateTimePicker_fec_reg");
            RadDateTimePicker_fec_reg.Visible = true;
            RadDateTimePicker_fec_reg.DbSelectedDate =Convert.ToDateTime(lbl_fec_Reg.Text);
            
            lbl_fec_Reg.Visible = false;
        }
        protected void gv_stock_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Conexion Ocoon = new Conexion();

            try
            {
                GridViewRow row = gv_stock.Rows[gv_stock.EditIndex];

                Label lbl_dlbl_id_StockDetalle = (Label)row.FindControl("lbl_id_StockDetalle");

                RadNumericTextBox txt_gvs_cantidad = (RadNumericTextBox)row.FindControl("txt_gvs_cantidad");
                RadDateTimePicker RadDateTimePicker_fec_reg = (RadDateTimePicker)row.FindControl("RadDateTimePicker_fec_reg");

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_STOCK_DETALLE", lbl_dlbl_id_StockDetalle.Text.Trim(), txt_gvs_cantidad.Text.Trim(),RadDateTimePicker_fec_reg.DbSelectedDate ,Session["sUser"].ToString(), DateTime.Now);

                gv_stock.EditIndex = -1;
                cargarGrilla_Stock();
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_stock_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_stock.EditIndex = -1;
            cargarGrilla_Stock();
        }

        protected void btn_newRegister_Click(object sender, ImageClickEventArgs e)
        {
            Conexion oCoonToBbId2 = new Conexion(2);
            DataTable dt = null;
            try
            {
               dt=oCoonToBbId2.ejecutarDataTable("STP_JVM_INSERTAR_STOCK", 8500, "003", "813711382010", "1562", "106700", "28", DateTime.Now, DateTime.Now, "0", "0", "L");
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }


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
            ddlFamilia.Items.Clear();
            ddlFamilia.Enabled = false;
            ddlMercaderista.Items.Clear();
            ddlMercaderista.Enabled = false;

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
            MopoReporStock.Show();
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
                    MopoReporStock.Show();
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
            ddlFamilia.Enabled = false;
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
                ddlFamilia.Items.Clear();
                ddlFamilia.Enabled = false;
                ddlMercaderista.Items.Clear();
                ddlMercaderista.Enabled = false;

                if (dt.Rows.Count > 0)
                {
                    ddlMercaderista.DataSource = dt;
                    ddlMercaderista.DataValueField = "Person_id";
                    ddlMercaderista.DataTextField = "Person_NameComplet";
                    ddlMercaderista.DataBind();
                    ddlMercaderista.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                    ddlMercaderista.Enabled = true;
                }

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
                ddlFamilia.Items.Clear();
                ddlFamilia.Enabled = false;
                ddlMercaderista.Items.Clear();
                ddlMercaderista.Enabled = false;

                ddlOficina.Items.Clear();
                ddlOficina.Enabled = false;
                ddlNodeComercial.Items.Clear();
                ddlNodeComercial.Enabled = false;
                ddlPuntoVenta.Items.Clear();
                ddlPuntoVenta.Enabled = false;
            }
            MopoReporStock.Show();
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
                MopoReporStock.Show();
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
                MopoReporStock.Show();
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

            string sidproductCategory = ddlCategoria.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK", sidproductCategory);

            ddlFamilia.Items.Clear();
            ddlFamilia.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                ddlFamilia.DataSource = dt;
                ddlFamilia.DataValueField = "id_ProductFamily";
                ddlFamilia.DataTextField = "name_Family";
                ddlFamilia.DataBind();
                ddlFamilia.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlFamilia.Enabled = true;
            }
            MopoReporStock.Show();
        }

        void LimpiarControles()
        {
            ddlCanal.SelectedValue = "0";
            ddlCampana.Items.Clear();
            ddlCategoria.Items.Clear();
            ddlFamilia.Items.Clear();
            ddlMercaderista.Items.Clear();
            ddlNodeComercial.Items.Clear();
            ddlOficina.Items.Clear();
            ddlPuntoVenta.Items.Clear();

            txtCantidad.Text = "";
            txtObservacion.Text = "";

            ddlCanalCargaMasiva.Items.Clear();
            ddlCampañaCargaMasiva.Items.Clear();

            CargarCanal();
        }

        protected void btnGuardarReportStock_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCanal.Text == "0" || ddlCampana.Text == "0" || ddlMercaderista.Text == "0"
    || ddlNodeComercial.Text == "0" || ddlPuntoVenta.Text == "0" || ddlCategoria.Text == "0"
    || txtCantidad.Text == "" )
                {
                    return;
                }

                Lucky.Business.Common.Application.OPE_Reporte_Stock oOPE_Reporte_Stock = new Lucky.Business.Common.Application.OPE_Reporte_Stock();
               

                DataTable dt = null;
                DataTable dtl = null;
                Conexion Ocoon = new Conexion();
                Conexion con = new Conexion(2);
                dt = Ocoon.ejecutarDataTable("UP_WEB_SEARCH_USER", "", Convert.ToInt32(ddlMercaderista.SelectedValue));
                string idperfil = dt.Rows[0][14].ToString();

                dtl = con.ejecutarDataTable("STP_JVM_LISTAR_CANALES", ddlCanal.SelectedValue);
                string tipocanal = dtl.Rows[0]["CAN_TIPO"].ToString();


                Lucky.Entity.Common.Application.EOPE_Reporte_Stock oEOPE_Reporte_Stock = oOPE_Reporte_Stock.RegistrarReporteStock(Convert.ToInt32(ddlMercaderista.SelectedValue),
                                                            idperfil, ddlCampana.SelectedValue, this.Session["companyid"].ToString(),
                                                            ddlPuntoVenta.SelectedValue, ddlCategoria.SelectedValue, null, null,
                                                            null, DateTime.Now.ToShortDateString(), "0", "0", "0");




                oOPE_Reporte_Stock.RegistrarReporteStock_Detalle(Convert.ToInt32(oEOPE_Reporte_Stock.ID_REG_STOCK),ddlFamilia.SelectedValue, txtCantidad.Text, null,null,null,null,txtObservacion.Text);

                LimpiarControles();
                lblmensaje.Visible = true;
                lblmensaje.Text = "Fue registrado con exito el reporte de Stock";
                lblmensaje.ForeColor = System.Drawing.Color.Blue;
            }

            catch (Exception ex)
            {
                string error = "";
                string mensaje = "";
                error = Convert.ToString(ex.Message);
                mensaje = ConfigurationManager.AppSettings["ErrorConection"];
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
        protected void btn_verificar_Click(object sender, EventArgs e)
        {

        }
        protected void btn_verfecha_Click(object sender, EventArgs e)
        {
            Conexion Ocoon = new Conexion();

            iidperson = Convert.ToInt32(cmbperson.SelectedValue);

            icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);
            iidNodeComercial = Convert.ToInt32(cmbNodeComercial.SelectedValue);
            sidPDV = cmbPuntoDeVenta.SelectedValue;
            if (sidPDV == "")
                sidPDV = "0";
            sidplanning = cmbplanning.SelectedValue;
            sidchannel = cmbcanal.SelectedValue;
            sidcategoriaProducto = cmbcategoria_producto.SelectedValue;
            if (sidcategoriaProducto == "")
                sidcategoriaProducto = "0";
            sidfamilia = cmbfamilia.SelectedValue;
            if (sidfamilia == "")
                sidfamilia = "0";


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

                DataTable registros = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_FUERA_RANGO", iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidfamilia, dfecha_inicio, dfecha_fin);

                gv_stock.DataSource = registros;
                gv_stock.DataBind();
                gv_stockToExcel.DataSource = registros;
                gv_stockToExcel.DataBind();
                gv_stock.Visible = true;
                gv_faltantes.Visible = false;
                lbl_mensaje_verifica.Text = "Se encontró " + registros.Rows.Count + " registros fuera de período.";
            }
        }

        private void Años()
        {
            DataTable dty = null;
            dty = Facd_ProcAdmin.Get_ObtenerYears();

            cmb_año.DataSource = dty;
            cmb_año.DataValueField = "Years_Number";
            cmb_año.DataTextField = "Years_Number";
            cmb_año.DataBind();
            cmb_año.Items.Insert(0, new ListItem("--Selecione--", "0"));
        }
        private void Llena_Meses()
        {
            DataTable dtm = Facd_ProcAdmin.Get_ObtenerMeses();


            cmb_mes.DataSource = dtm;
            cmb_mes.DataValueField = "codmes";
            cmb_mes.DataTextField = "namemes";
            cmb_mes.DataBind();
            cmb_mes.Items.Insert(0, new ListItem("--Selecione--", "0"));

            cmb_periodo.Items.Insert(0, new ListItem("--Selecione--", "0"));
            cmb_periodo.Enabled = false;
        }

        protected void cmb_mes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_periodo.Items.Clear();
          
            Llenar_Periodos();
            ModalPopupExtender_verDuplicados.Show();
        }
        private void Llenar_Periodos()
        {
            Conexion Ocoon = new Conexion();
            cmb_periodo.Items.Clear();
            cmb_periodo.Enabled = true;
            DataTable dtp = null;
            int icompany = Convert.ToInt32(this.Session["companyid"]);

            dtp = Ocoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS_POR_PLANNING", cmbplanning.SelectedValue, icompany, 28,cmb_año.SelectedValue, cmb_mes.SelectedValue);

            cmb_periodo.DataSource = dtp;
            cmb_periodo.DataValueField = "id_ReportsPlanning";
            cmb_periodo.DataTextField = "Periodo";
            cmb_periodo.DataBind();
            cmb_periodo.Items.Insert(0, new ListItem("--Selecione--", "0"));

        }

        protected void btn_verDuplicado_Click(object sender, EventArgs e)
        {
            Conexion Ocoon = new Conexion();

            iidperson = Convert.ToInt32(cmbperson.SelectedValue);

            icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);
            iidNodeComercial = Convert.ToInt32(cmbNodeComercial.SelectedValue);
            sidPDV = cmbPuntoDeVenta.SelectedValue;
            if (sidPDV == "")
                sidPDV = "0";
            sidplanning = cmbplanning.SelectedValue;
            sidchannel = cmbcanal.SelectedValue;
            sidcategoriaProducto = cmbcategoria_producto.SelectedValue;
            if (sidcategoriaProducto == "")
                sidcategoriaProducto = "0";
            sidfamilia = cmbfamilia.SelectedValue;
            if (sidfamilia == "")
                sidfamilia = "0";


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

                DataTable registros = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_STOCK_DUPLICADA", iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidfamilia,Convert.ToInt32(cmb_periodo.SelectedValue));

                gv_stock.DataSource = registros;
                gv_stock.DataBind();
                gv_stockToExcel.DataSource = registros;
                gv_stockToExcel.DataBind();
                gv_stock.Visible = true;
                gv_faltantes.Visible = false;
                lbl_mensaje_verifica.Text = "Se encontró " + registros.Rows.Count + " registros duplicados para el periodo :" + cmb_periodo.SelectedItem +" - "+cmb_año.SelectedItem;
            }
        }
        protected void btn_exportarexcel_Click(object sender, EventArgs e)
        {
            try
            {
                gv_stockToExcel.Visible = true;
                GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Stock", this.gv_stockToExcel);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btn_verfaltante_Click(object sender, EventArgs e)
        {
            Conexion Ocoon = new Conexion();
            DateTime fechaini;
            //DateTime fechafin;
            fechaini = txt_fecha_inicio.SelectedDate.Value;
            DataTable registros = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_LISTA_FAMILIAS_X_PDV_FALTANTES", cmbplanning.SelectedValue, fechaini.Year, fechaini.Month);
            //dgv_faltantes
            gv_faltantes.DataSource = registros;
            gv_faltantes.DataBind();
            gv_faltantes.PageIndex = 1;
            gv_faltantes.Visible = true;
            gv_stock.Visible = false;            
            lbl_mensaje_verifica.Text = "Se encontró " + registros.Rows.Count + " registros faltantes por relevar.";
        }



        protected void ddlCanalCargaMasiva_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            sidchannel = ddlCanalCargaMasiva.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);

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
                        oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Ope_Stock"];
                        oCmd.Connection = oConn1;
                        oDa.SelectCommand = oCmd;

                        try
                        {

                            oDa.Fill(oDs);


                            dt = oDs.Tables[0];
                            int numcol = 7; //determina el número de columnas para el datatable
                            if (dt.Columns.Count == numcol)
                            {

                                dt.Columns[0].ColumnName = "cod_pdv";
                                dt.Columns[1].ColumnName = "cod_categoria";
                                dt.Columns[2].ColumnName = "cod_marca";
                                dt.Columns[3].ColumnName = "Observacion";
                                dt.Columns[4].ColumnName = "cod_periodo";
                                dt.Columns[5].ColumnName = "cod_familia";
                                dt.Columns[6].ColumnName = "Cantidad";



                                string idPlanning = ddlCampañaCargaMasiva.SelectedValue;
                                string companyid = this.Session["companyid"].ToString();
                                string usuario = this.Session["sUser"].ToString().Trim();

                               // cargarCategoria(idPlanning);
                                //llenarPuntoVenta1(idPlanning, 0);

                                OPE_REPORTE_PRECIO oOPE_REPORTE_PRECIO = new OPE_REPORTE_PRECIO();
                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    string cod_pdv = dt.Rows[i]["cod_pdv"].ToString();
                                    string cod_categoria = dt.Rows[i]["cod_categoria"].ToString();
                                    string cod_marca = dt.Rows[i]["cod_marca"].ToString();
                                    string Observacion = dt.Rows[i]["Observacion"].ToString();
                                    string cod_periodo = dt.Rows[i]["cod_periodo"].ToString();

                                    string cod_familia = dt.Rows[i]["cod_familia"].ToString();
                                    string Cantidad = dt.Rows[i]["Cantidad"].ToString();
                                    

                                   // llenarMarca1(id_Categoria);
                                   // llenaProducto(Convert.ToInt32(companyid), id_Categoria, Convert.ToInt32(id_Marca));

                                    //ddlPuntoVenta.Items.FindByValue(id_ClientPDV).Selected = true;
                                    //ddlCategoria.Items.FindByValue(id_Categoria).Selected = true;
                                  //  ddlMarca.Items.FindByValue(id_Marca).Selected = true;



                                    Lucky.Business.Common.Application.OPE_Reporte_Stock oOPE_Reporte_Stock = new Lucky.Business.Common.Application.OPE_Reporte_Stock();


                                    //DataTable dt = null;
                                    //DataTable dtl = null;
                                    Conexion Ocoon = new Conexion();
                                    Conexion con = new Conexion(2);
                                    dt = Ocoon.ejecutarDataTable("UP_WEB_SEARCH_USER", "", Convert.ToInt32(this.Session["personid"].ToString()));
                                    string idperfil = dt.Rows[0]["Perfil_id"].ToString();

                                   // dtl = con.ejecutarDataTable("STP_JVM_LISTAR_CANALES", ddlCanal.SelectedValue);
                                   // string tipocanal = dtl.Rows[0]["CAN_TIPO"].ToString();


                                    Lucky.Entity.Common.Application.EOPE_Reporte_Stock oEOPE_Reporte_Stock = oOPE_Reporte_Stock.RegistrarReporteStock(Convert.ToInt32(this.Session["personid"].ToString()),
                                                                                idperfil, ddlCampañaCargaMasiva.SelectedValue, this.Session["companyid"].ToString(),
                                                                                cod_pdv, cod_categoria, cod_marca, null,
                                                                                null, DateTime.Now.ToShortDateString(), "0", "0", "0");




                                   oOPE_Reporte_Stock.RegistrarReporteStock_Detalle(Convert.ToInt32(oEOPE_Reporte_Stock.ID_REG_STOCK), cod_familia, Cantidad, null, null, null, null, Observacion);





                                }

                                LimpiarControles();
                                lblmensaje.Visible = true;
                                lblmensaje.Text = "Fue registrado con exito el reporte de precio";
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
                            lbl_mensaje_verifica.Text = "Por favor revise la consistencia de la información";
                        }
                    }
                }
            }
        }


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

                sFile = Server.MapPath("masivo") + "\\" + "DATOS_CARGA_REPORTE_STOCK.xls";

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
                            col = "A";
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
                    oHoja.Range["A2", col + "2"].Interior.Color = 0;
                    oHoja.Range["A2", col + "2"].Font.Color = 16777215;


                    oHoja.Range["A2", col + "2"].Font.Bold = true;
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


        protected void ddlCampañaCargaMasiva_SelectedIndexChanged(object sender, EventArgs e)
        {
            Conexion cn = new Conexion();
            DataSet ds = cn.ejecutarDataSet("OPE_CARGAMASIVA_REPORTE_STOCK_DATOS", ddlCampañaCargaMasiva.SelectedValue,"28");
            CreaExcel(ds);
            Datos.Visible = true;
            MopoReporMasivo.Show();
        }


    }
}

