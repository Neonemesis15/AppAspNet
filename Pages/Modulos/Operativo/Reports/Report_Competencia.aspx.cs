using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Lucky.Data;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;
using Lucky.CFG.Util;
using System.Net;
using System.Drawing.Imaging;
using System.Drawing;
using Lucky.CFG.Tools;

namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_Competencia : System.Web.UI.Page
    {

        #region Declaracion de variables generales
        Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private int compañia;
        private string pais;
        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarCombo_Channel();
                RadProgressArea1.ProgressIndicators &= ~ProgressIndicators.SelectedFilesCount;
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

                ddlCanal.DataSource = dt;
                ddlCanal.DataValueField = "cod_Channel";
                ddlCanal.DataTextField = "Channel_Name";
                ddlCanal.DataBind();
                ddlCanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));
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
            cmbmarca.Items.Clear();
            cmbmarca.Enabled = false;
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

        #region BytesNoImagen
        private Byte[] getBytesNoImage()
        {            
            WebResponse result = null;
            byte[] rBytes = null;

            try
            {
                WebRequest request = WebRequest.Create("http://sige.lucky.com.pe/Pages/Modulos/Cliente/Reportes/Galeria_fotografica/Fotos/sin_url_imagen.jpg");
                
                // Get the content
                result = request.GetResponse();
                Stream rStream = result.GetResponseStream();

                // Bytes from address
                using (BinaryReader br = new BinaryReader(rStream))
                {
                    // Ask for bytes bigger than the actual stream
                    rBytes = br.ReadBytes(1000000);
                    br.Close();
                }
                // close down the web response object
                result.Close();                
            }
            catch (Exception ex) 
            {

            }
            return rBytes;
        } 
        #endregion

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

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_COMPETENCIA", sidplanning);
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

            string sidcategoriaproducto = cmbcategoria_producto.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_REPORT_COMPETENCIA", sidcategoriaproducto);

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
            //cargarGrilla_Competencias();
        }
        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargar_rgv_competencia();
        }
        GridView gv = new GridView();
        //protected void cargarGrilla_Competencias()
        //{
        //    try
        //    {
                
        //        DataTable dt = null;

        //        int iidperson = Convert.ToInt32(cmbperson.SelectedValue);
        //        string sidplanning = cmbplanning.SelectedValue;
        //        string sidchannel = cmbcanal.SelectedValue;

        //        int icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);
        //        int iidNodeComercial = Convert.ToInt32(cmbNodeComercial.SelectedValue);
        //        string sidPDV = cmbPuntoDeVenta.SelectedValue;
        //        if (sidPDV == "")
        //            sidPDV = "0";

        //        string sidcategoriaProducto = cmbcategoria_producto.SelectedValue;
        //        if (sidcategoriaProducto == "")
        //            sidcategoriaProducto = "0";
        //        string sidmarca = cmbmarca.SelectedValue;
        //        if (sidmarca == "")
        //            sidmarca = "0";


        //         DateTime dfecha_inicio;
        //        DateTime dfecha_fin;

        //        if (txt_fecha_inicio.SelectedDate.ToString() == "" || txt_fecha_inicio.SelectedDate.ToString() == "0" || txt_fecha_inicio.SelectedDate == null)
        //            dfecha_inicio = txt_fecha_inicio.MinDate;
        //        else dfecha_inicio = txt_fecha_inicio.SelectedDate.Value;


        //        if (txt_fecha_fin.SelectedDate.ToString() == "" || txt_fecha_fin.SelectedDate.ToString() == "0" || txt_fecha_fin.SelectedDate == null)
        //            dfecha_fin = txt_fecha_fin.MaxDate;
        //        else dfecha_fin = txt_fecha_fin.SelectedDate.Value;

        //        if (DateTime.Compare(dfecha_inicio, dfecha_fin) == 1)
        //        {
        //            lblmensaje.Visible = true;
        //            lblmensaje.Text = "La fecha de inicio debe ser menor o igual a la fecha fin";
        //            lblmensaje.ForeColor = System.Drawing.Color.Red;
        //        }
        //        else
        //        {
        //            dt = obj_Facade_Proceso_Operativo.Get_ReporteCompetencias(iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, dfecha_inicio, dfecha_fin);



        //            gv_competencia.DataSource = dt;
        //            gv_competencia.DataBind();
        //            btn_img_exporttoexcel.Enabled = true;

        //            gv_competenciaToExcel.DataSource = dt;
        //            gv_competenciaToExcel.DataBind();

        //            lblmensaje.Visible = true;
        //            lblmensaje.Text = "Se encontro " + dt.Rows.Count + " resultados";

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception mensaje = ex;
        //        gv_competencia.DataBind();
        //        lblmensaje.Text = "";
        //        if (cmbplanning.SelectedIndex > 0)
        //            lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
        //    }

        //}

        private void cargar_rgv_competencia() 
        {
            try
            {
                DataTable dt = null;

                int iidperson = Convert.ToInt32(cmbperson.SelectedValue);
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
                    dt = obj_Facade_Proceso_Operativo.Get_ReporteCompetencias(iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, dfecha_inicio, dfecha_fin);
                    DataTable source = load_images_rgv_competencia(dt);

                    rgv_competencia.DataSource = source;
                    rgv_competencia.DataBind();
                    btn_img_exporttoexcel.Enabled = true;

                    gv_competenciaToExcel.DataSource = dt;
                    gv_competenciaToExcel.DataBind();

                    lblmensaje.Visible = true;
                    lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                    lblmensaje.ForeColor = System.Drawing.Color.Blue;
                    
                }
            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                rgv_competencia.DataBind();
                lblmensaje.Text = "";
                if (cmbplanning.SelectedIndex > 0)
                    lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
            }
        }
        
        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            gv_competenciaToExcel.Visible = true;
            GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Comeptencia",this.gv_competenciaToExcel);
            
        }
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //}

        //protected void gv_competencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
           
        //    gv_competencia.PageIndex = e.NewPageIndex;
        //    cargarGrilla_Competencias();
            
        //}
        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            bool validar = cb_all.Checked;

            foreach (GridItem item in rgv_competencia.Items)
            {
                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");

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
        //protected void btn_validar_Click(object sender, EventArgs e)
        //{
            
        //    for (int i = 0; i < gv_competencia.Rows.Count; i++)
        //    {
        //        GridViewRow row = gv_competencia.Rows[i];
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {


        //            CheckBox cb_validar = (CheckBox)row.FindControl("cb_validar");
        //            Label lbl = (Label)row.FindControl("lbl_validar");
                   
        //            //gv_competencia.Columns[15].Visible = true;
        //            //int id = Convert.ToInt32(row.Cells[15].Text);
        //            Label lbl_id_reg_comp_detalle = (Label)row.FindControl("lbl_id_reg_comp_detalle");
        //            int id_compDetll =Convert.ToInt32(lbl_id_reg_comp_detalle.Text.Trim());

        //            bool validar = cb_validar.Checked;

        //            Label oLabel_id_cabz_comp = (Label)row.FindControl("lbl_id_reg_competencia");
        //            int iid_Cabz_compet = Convert.ToInt32(oLabel_id_cabz_comp.Text.Trim());

        //            Label lbl_tipoReg = (Label)row.FindControl("lbl_tipoReg");
        //            char tipoReg = Convert.ToChar(lbl_tipoReg.Text.Trim());

        //            update_competencia_detalle_validado(iid_Cabz_compet, id_compDetll, validar, tipoReg);
        //            if (validar == true)
        //            {
   
        //                lbl.Text = "validado";
        //                lbl.ForeColor = System.Drawing.Color.Green;

        //                update_urlFoto(iid_Cabz_compet);
        //            }
        //            else
        //            {
        //                lbl.Text = "sin validar";
        //                lbl.ForeColor = System.Drawing.Color.Red;
        //            }  
        //        }

        //    }

        //    //gv_competencia.Columns[15].Visible = false;
        //}

        //Codigo para Ordenar y Paginar la grilla "grid_detalle_foto"------------------
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

        protected void update_competencia_detalle_validado(int id_compet, int id_compDetall, bool validar,char tipoReg)
        {
            try
            {
                Conexion Ocoon = new Conexion();
                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_COMPETENCIA_DETALLE_VALIDADO", id_compet, id_compDetall, validar, tipoReg, this.Session["sUser"],DateTime.Now);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void btn_img_buscar_detalle_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{
            //    Conexion Ocoon = new Conexion();

            //    string sidcompetencia = ((ImageButton)sender).CommandArgument;
            //    int iidcompetencia = Convert.ToInt32(sidcompetencia);

            //    DataTable dt2 = null;
            //    dt2 = Ocoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_REPORTE_COMPETENCIA_FOTO", iidcompetencia);

            //    if (dt2.Rows.Count > 0)
            //    {
            //        string photoName = dt2.Rows[0]["Id_regft"].ToString() + ".jpg"; ;
            //        byte[] byteArrayIn = (byte[])dt2.Rows[0]["foto"];

            //        //string strFileName = Server.MapPath(@"~\Pages\Modulos\Cliente\Reportes\Galeria_fotografica\Fotos") + "\\" + photoName;

            //        Save_photo(photoName, byteArrayIn);

            //        Image foto_url = Panel_DetalleCompetencia.FindControl("foto_url") as Image;
            //        foto_url.ImageUrl = "~/Pages/Modulos/Cliente/Reportes/Galeria_fotografica/Fotos/" + photoName;

            //        ModalPopupExtender_detalle.Show();
            //    }
            //    else
            //    {
            //        foto_url.ImageUrl = "~/Pages/Modulos/Cliente/Reportes/Galeria_fotografica/Fotos/sin_url_imagen.jpg";
            //        ModalPopupExtender_detalle.Show();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.Message.ToString();
            //}
            
        }
        protected void Save_photo(string photoName, byte[] byteArrayIn)
        {
            try
            {
                System.Drawing.Image newImage;

                // iidregft = dt_Foto.Rows[i]["iidregft"].ToString();
                string PhotoNameAsUrlInDb = photoName;
                // byte[] byteArrayIn = (byte[])dt_Foto.Rows[i]["foto"];

                string strFileName = Server.MapPath(@"~\Pages\Modulos\Cliente\Reportes\Galeria_fotografica\Fotos") + "\\" + PhotoNameAsUrlInDb;

                if (!System.IO.File.Exists(strFileName))
                {
                    if (byteArrayIn != null)
                    {
                        using (System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArrayIn))
                        {
                            newImage = System.Drawing.Image.FromStream(stream);
                            newImage.Save(strFileName);
                            //miImagen.Attributes.Add("src", strFileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void update_urlFoto(int iidreg_competencia)
        {
            try
            {
                System.Drawing.Image newImage;

                DataTable dt_miFoto = null;
                Conexion Ocoon = new Conexion();
                dt_miFoto = Ocoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_REPORTE_COMPETENCIA_FOTO", iidreg_competencia);
                
                string PhotoNameAsUrlInDb;
                int iidregft=0;
                if (dt_miFoto.Rows.Count > 0)
                {
                    iidregft =Convert.ToInt32(dt_miFoto.Rows[0]["Id_regft"].ToString());
                    PhotoNameAsUrlInDb = iidregft + ".jpg";
                    byte[] byteArrayIn = (byte[])dt_miFoto.Rows[0]["foto"];

                    string strFileName = Server.MapPath(@"~\Pages\Modulos\Cliente\Reportes\Informe_de_Competencia\Fotos") + "\\" + PhotoNameAsUrlInDb;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream stream = new MemoryStream(byteArrayIn))
                        {
                            newImage = System.Drawing.Image.FromStream(stream);
                            newImage.Save(strFileName);
                        }
                    }
                }
                else
                {
                    PhotoNameAsUrlInDb = "sin_url_imagen.jpg";
                }
                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REG_FOTO_FOTOGRAFICO_URLFOTO", iidregft, this.Session["sUser"], DateTime.Now, PhotoNameAsUrlInDb);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        //protected void gv_competencia_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    gv_competencia.EditIndex = e.NewEditIndex;

        //    cargarGrilla_Competencias();

        //    GridViewRow row = gv_competencia.Rows[gv_competencia.EditIndex];

        //    Label lbl_fec_Reg = row.FindControl("lbl_fec_Reg") as Label;
        //    Label lblobs= row.FindControl("lblobs") as Label;

        //    TextBox txtobs = row.FindControl("txtobs") as TextBox;
        //    txtobs.Visible = true;
        //    lblobs.Visible = false;
        //    txtobs.Text = lblobs.Text;

        //    RadDateTimePicker RadDateTimePicker_fec_reg = (RadDateTimePicker)row.FindControl("RadDateTimePicker_fec_reg");
        //    RadDateTimePicker_fec_reg.Visible = true;
        //    RadDateTimePicker_fec_reg.DbSelectedDate = Convert.ToDateTime(lbl_fec_Reg.Text);
        //    CheckBox cb_validar = (CheckBox)row.FindControl("cb_validar");
        //    cb_validar.Enabled = true;

        //    lbl_fec_Reg.Visible = false;
        //}
        //protected void gv_competencia_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    Conexion Ocoon = new Conexion();

        //    try
        //    {
        //        GridViewRow row = gv_competencia.Rows[gv_competencia.EditIndex];

        //        Label oLabel_id_cabz_comp = (Label)row.FindControl("lbl_id_reg_competencia");
        //        int iid_Cabz_compet = Convert.ToInt32(oLabel_id_cabz_comp.Text.Trim());

        //        Label lbl_id_reg_comp_detalle = (Label)row.FindControl("lbl_id_reg_comp_detalle");
        //        int id_compDetll = Convert.ToInt32(lbl_id_reg_comp_detalle.Text.Trim());

        //        Label lbl_tipoReg = (Label)row.FindControl("lbl_tipoReg");
        //        char tipoReg = Convert.ToChar(lbl_tipoReg.Text.Trim());

        //        RadDateTimePicker RadDateTimePicker_fec_reg = (RadDateTimePicker)row.FindControl("RadDateTimePicker_fec_reg");
        //        Label lblobs = row.FindControl("lblobs") as Label;

        //        TextBox txtobs = row.FindControl("txtobs") as TextBox;

        //        CheckBox cb_validar = (CheckBox)row.FindControl("cb_validar");

        //        string Precio_Costo=((TextBox)(row.Cells[8].Controls[0])).Text;
        //        string Precio_PVP = ((TextBox)(row.Cells[9].Controls[0])).Text;
        //        string Fec_Ini_Act = ((TextBox)(row.Cells[10].Controls[0])).Text;
        //        string Fec_Fin_Act = ((TextBox)(row.Cells[11].Controls[0])).Text;

        //        string Cant_Personal = ((TextBox)(row.Cells[12].Controls[0])).Text;
        //        string Premio = ((TextBox)(row.Cells[13].Controls[0])).Text;
        //        string Mecanica = ((TextBox)(row.Cells[14].Controls[0])).Text;
        //        //string observacion = ((TextBox)(row.Cells[15].Controls[0])).Text;
        //        string Txt_Grupo_Obj = ((TextBox)(row.Cells[18].Controls[0])).Text;
        //        string Mat_Apoyo = ((TextBox)(row.Cells[21].Controls[0])).Text;

        //        Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_COMPETENCIA", iid_Cabz_compet,id_compDetll,tipoReg,Precio_Costo,Precio_PVP,Fec_Ini_Act,Fec_Fin_Act,Cant_Personal,Premio,Mecanica,Txt_Grupo_Obj,Mat_Apoyo, RadDateTimePicker_fec_reg.DbSelectedDate, Session["sUser"].ToString(), DateTime.Now, txtobs.Text, cb_validar.Checked);

        //        gv_competencia.EditIndex = -1;
        //        cargarGrilla_Competencias();
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        Response.Redirect("~/err_mensaje_seccion.aspx", true);
        //    }
        //}

        //protected void gv_competencia_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    gv_competencia.EditIndex = -1;
        //    cargarGrilla_Competencias();
        //}

        protected void btn_validar_Click(object sender, EventArgs e)
        {  
            foreach (GridItem item in rgv_competencia.Items)
            {
                    this.Session["foto_Chk"] = item;


                    CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");
                    Label lbl_validar = (Label)item.FindControl("lbl_validar");

                    Label lbl_id_reg_comp_detalle = (Label)item.FindControl("lbl_id_reg_comp_detalle");
                    int id_compDetll = Convert.ToInt32(lbl_id_reg_comp_detalle.Text.Trim());

                    bool validar = cb_validar.Checked;

                    Label oLabel_id_cabz_comp = (Label)item.FindControl("lbl_id_reg_competencia");
                    int iid_Cabz_compet = Convert.ToInt32(oLabel_id_cabz_comp.Text.Trim());

                    Label lbl_tipoReg = (Label)item.FindControl("lbl_tipoReg");
                    char tipoReg = Convert.ToChar(lbl_tipoReg.Text.Trim());

                    update_competencia_detalle_validado(iid_Cabz_compet, id_compDetll, validar, tipoReg);

                    if (validar == true)
                    {
                        lbl_validar.Text = "validado";
                        lbl_validar.ForeColor = System.Drawing.Color.Green;
                        update_urlFoto(iid_Cabz_compet);
                    }
                    else
                    {
                        lbl_validar.Text = "sin validar";
                        lbl_validar.ForeColor = System.Drawing.Color.Red;
                    }
            }
        }

        protected void rgv_competencia_DataBound(object sender, EventArgs e)
        {
            if (rgv_competencia.Items.Count > 0)
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

        private DataTable load_images_rgv_competencia(DataTable source) 
        {
            DataTable fotos = new DataTable();
            Conexion OConn = new Conexion(1);

            source.Columns.Add("Id_regft", typeof(Int64));
            source.Columns.Add("foto", typeof(Byte[]));

            for (int i = 0; i < source.Rows.Count; i++) 
            {
                fotos = OConn.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_REPORTE_COMPETENCIA_FOTO", source.Rows[i]["Id_rcompe"].ToString());
                if (fotos.Rows.Count > 0 && fotos.Rows[0]["foto"] != System.DBNull.Value)
                {
                    source.Rows[i]["Id_regft"] = Convert.ToInt64(fotos.Rows[0]["Id_regft"]);
                    source.Rows[i]["foto"] = (Byte[])(fotos.Rows[0]["foto"]);
                }
                else 
                {
                    source.Rows[i]["Id_regft"] = 0;
                    source.Rows[i]["foto"] = getBytesNoImage();                    
                }
            }
            return source;
        }

        protected void rgv_competencia_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                panelEdit.DataBind();
                RadBinaryImage_fotoBig.DataBind();
                RadBinaryImage_fotoBig.Visible = false;
                GridItem item = rgv_competencia.Items[e.Item.ItemIndex];                                
                Label lbl_id_reg_foto = (Label)item.FindControl("lbl_id_reg_foto");
                Label lbl_id_reg_competencia = (Label)item.FindControl("lbl_id_reg_competencia");

                if (e.CommandName == "VERFOTO")
                {
                    int iidregft = Convert.ToInt32(lbl_id_reg_foto.Text);
                    byte[] byteArrayIn;
                    if (iidregft != 0)
                    {
                        DataTable dt = null;
                        Conexion Ocoon = new Conexion();
                        dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REG_FOTO", iidregft);
                        byteArrayIn = (byte[])dt.Rows[0]["foto"];
                        dt = null;
                        Ocoon = null;
                    }
                    else
                        byteArrayIn = getBytesNoImage();

                    RadBinaryImage_viewFoto.DataValue = byteArrayIn;
                    Session["array"] = byteArrayIn;
                    Session["iidregft"] = Convert.ToInt32(lbl_id_reg_foto.Text);
                    Session["Id_rcompe"] = Convert.ToInt32(lbl_id_reg_competencia.Text);
                    ModalPopupExtender_viewfoto.Show();
                }
                else if (e.CommandName == "EDITFOTO")
                {
                    int iidregft = Convert.ToInt32(lbl_id_reg_foto.Text);
                    int Id_rcompe = Convert.ToInt32(lbl_id_reg_competencia.Text);
                    RadBinaryImage imageBinary = (RadBinaryImage)item.FindControl("RadBinaryImage_foto");

                    RadBinaryImage_fotoBig.DataValue = imageBinary.DataValue;
                    RadBinaryImage_fotoBig.Visible = false;
                    Session["iidregft"] = iidregft;
                    Session["Id_rcompe"] = Id_rcompe;

                    ModalPopup_Edit.Show();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            UploadedFile file = UploadedFile.FromHttpPostedFile(Request.Files[inputFile.UniqueID]);

            if (!Object.Equals(file, null))
            {
                LooongMethodWhichUpdatesTheProgressContext(file);
                RadBinaryImage_fotoBig.Visible = true;

                Int32 tamaño = Convert.ToInt32(file.InputStream.Length);
                byte[] byteArrayIn = new byte[tamaño + 1];

                file.InputStream.Read(byteArrayIn, 0, tamaño);
                RadBinaryImage_fotoBig.DataValue = byteArrayIn;
                Session["byteArrayIn"] = byteArrayIn;
            }

            ModalPopup_Edit.Show();
        }

        private void LooongMethodWhichUpdatesTheProgressContext(UploadedFile file)
        {
            const int total = 100;

            RadProgressContext progress = RadProgressContext.Current;

            for (int i = 0; i < total; i++)
            {
                progress.PrimaryTotal = 1;
                progress.PrimaryValue = 1;
                progress.PrimaryPercent = 100;

                progress.SecondaryTotal = total;
                progress.SecondaryValue = i;
                progress.SecondaryPercent = i;
                string img_name;
                try
                {
                    img_name = file.GetName();
                }
                catch (Exception e)
                {
                    img_name = "Imágen";
                }
                progress.CurrentOperationText = img_name  + " Empezando a procesar...";

                if (!Response.IsClientConnected)
                {
                    //Cancel button was clicked or the browser was closed, so stop processing
                    break;
                }

                //Stall the current thread for 0.1 seconds
                System.Threading.Thread.Sleep(100);
            }
        }

        protected void imgbtn_save_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Int32 iidregft = Convert.ToInt32(Session["iidregft"]);
                Int32 Id_rcompe = Convert.ToInt32(Session["Id_rcompe"]);

                Conexion Ocoon = new Conexion();

                byte[] foto = (byte[])Session["byteArrayIn"];

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REG_COMPE_FOTO", iidregft, Id_rcompe, foto, Session["sUser"].ToString(), DateTime.Now);
                RadBinaryImage_fotoBig.Visible = false;
                cargar_rgv_competencia();

            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void imgbtn_cancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RadBinaryImage_fotoBig.DataBind();
                RadBinaryImage_fotoBig.Visible = false;
                ModalPopup_Edit.Hide();
            }
            catch (Exception ex)
            {
                //comentario
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void rgv_competencia_EditCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                cargar_rgv_competencia();
                //load_images_rgv_competencia();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void rgv_competencia_UpdateCommand(object source, GridCommandEventArgs e)
        {           
            try
            {
                lblmensaje.Text = "";
                Conexion Ocoon = new Conexion();

                GridEditableItem row = rgv_competencia.Items[e.Item.ItemIndex];

                Label oLabel_id_cabz_comp = row.FindControl("lbl_id_reg_competencia") as Label;
                int iid_Cabz_compet = Convert.ToInt32(oLabel_id_cabz_comp.Text.Trim());

                Label lbl_id_reg_comp_detalle = row.FindControl("lbl_id_reg_comp_detalle") as Label;
                int id_compDetll = Convert.ToInt32(lbl_id_reg_comp_detalle.Text.Trim());

                Label lbl_tipoReg = row.FindControl("lbl_tipoReg") as Label;
                char tipoReg = Convert.ToChar(lbl_tipoReg.Text.Trim());

                CheckBox cb_validar = row.FindControl("cb_validar") as CheckBox;

                GridEditableItem item = e.Item as GridEditableItem;
                
                List<object> ArrayEditorValue = new List<object>();

                GridEditManager editMan = item.EditManager;

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

                            if (editor is GridTextColumnEditor)
                            {
                                editorText = (editor as GridTextColumnEditor).Text;
                                editorValue = (editor as GridTextColumnEditor).Text;
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
                string Precio_Costo = ArrayEditorValue[0].ToString();
                string Precio_PVP = ArrayEditorValue[1].ToString();
                string Fec_Ini_Act = (ArrayEditorValue[3] as RadDateTimePicker).SelectedDate.ToString();
                string Fec_Fin_Act = (ArrayEditorValue[5] as RadDateTimePicker).SelectedDate.ToString();           
                string Cant_Personal = ArrayEditorValue[6].ToString();
                string Premio = ArrayEditorValue[7].ToString();
                string Mecanica = ArrayEditorValue[8].ToString();
                string observaciones = ArrayEditorValue[9].ToString();
                string Txt_Grupo_Obj = ArrayEditorValue[10].ToString();
                string Mat_Apoyo = ArrayEditorValue[11].ToString();
                DateTime fec_reg = Convert.ToDateTime((ArrayEditorValue[13] as RadDateTimePicker).SelectedDate);    
                
                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_COMPETENCIA", iid_Cabz_compet, id_compDetll, tipoReg, Precio_Costo, Precio_PVP, Fec_Ini_Act, Fec_Fin_Act, Cant_Personal, Premio, Mecanica, Txt_Grupo_Obj, Mat_Apoyo, fec_reg, Session["sUser"].ToString(), DateTime.Now, observaciones, cb_validar.Checked);
                cargar_rgv_competencia();
 
            }
            catch (Exception ex)
            {
                lblmensaje.Text = ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void rgv_competencia_CancelCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                cargar_rgv_competencia();
            }
            catch(Exception ex)
            {
                Exception mensaje = ex;
                rgv_competencia.DataBind();
                lblmensaje.Text = "";
                if (cmbplanning.SelectedIndex > 0)
                    lblmensaje.Text = "Ocurrió un error inesperado, Por favor inténtelo más tarde o comuníquese con el área de Tecnologías de Información.";
            }
        }

        protected void rgv_competencia_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                rgv_competencia.CurrentPageIndex = e.NewPageIndex;
                cargar_rgv_competencia();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void rgv_competencia_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            try
            {
                cargar_rgv_competencia();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void BtnclosePanel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RadBinaryImage_fotoBig.DataBind();
                RadBinaryImage_fotoBig.Visible = false;
                ModalPopup_Edit.Hide();
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void ddlCanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dte = null;
            DataTable dtl = null;
            DataTable d = null;
            Conexion Ocoon = new Conexion();
            compañia = Convert.ToInt32(this.Session["companyid"]);
            string sidchannel = ddlCanal.SelectedValue;

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);
          

            ddlCampana.Items.Clear();
            ddlCategoria.Items.Clear();
            ddlCategoria.Enabled = false;
            ddlMarca.Items.Clear();
            ddlMarca.Enabled = false;
            ddlMercaderista.Items.Clear();
            ddlMercaderista.Enabled = false;


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

            dte = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_TIPO_PROMOCION");

            if (dte.Rows.Count > 0)
            {
                ddlTipoProm.DataSource = dte;
                ddlTipoProm.DataValueField = "id_Tipo_Prom";
                ddlTipoProm.DataTextField = "descripcion";
                ddlTipoProm.DataBind();
                ddlTipoProm.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlTipoProm.Enabled = true;
            }

            dtl = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_GrupoObjetivo", ddlCanal.SelectedValue);

            if (dtl.Rows.Count > 0)
            {
                ddlGrupoObj.DataSource = dtl;
                ddlGrupoObj.DataValueField = "id_TargetGroup";
                ddlGrupoObj.DataTextField = "TargetGroup";
                ddlGrupoObj.DataBind();
                ddlGrupoObj.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlGrupoObj.Enabled = true;
            }
            Conexion ocon = new Conexion(2);
            d = ocon.ejecutarDataTable("STP_JVM_LISTAR_COMPETIDORAxCLIENTE", this.Session["companyid"].ToString());
            if (d.Rows.Count > 0)
            {
                ddlEmpresa.DataSource = d;
                ddlEmpresa.DataValueField = "ID_COMPETIDORA";
                ddlEmpresa.DataTextField = "COM_NOMBRE";
                ddlEmpresa.DataBind();
                ddlEmpresa.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlEmpresa.Enabled = true;
            }

            MopoReporCompetencia.Show();
        }


        public void cargaNodeComercial(string idPlanning , DropDownList Nodecomercial)
        {
            try
            {
                Nodecomercial.Items.Clear();
                Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(idPlanning);

                if (oListNodeComercial.Length > 0)
                {
                    Nodecomercial.Enabled = true;
                    Nodecomercial.DataSource = oListNodeComercial;
                    Nodecomercial.DataTextField = "commercialNodeName";
                    Nodecomercial.DataValueField = "NodeCommercial";
                    Nodecomercial.DataBind();

                    Nodecomercial.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void cargarCategoria(string idPlanning, DropDownList Categoria)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_COMPETENCIA", idPlanning);
            Categoria.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                Categoria.DataSource = dt;
                Categoria.DataValueField = "id_ProductCategory";
                Categoria.DataTextField = "Product_Category";
                Categoria.DataBind();
                Categoria.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                Categoria.Enabled = true;
            }
        }
        public void cargarOficina(DropDownList Oficina)
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
                        Oficina.Enabled = true;
                        Oficina.DataSource = dtofi;
                        Oficina.DataTextField = "Name_Oficina";
                        Oficina.DataValueField = "cod_Oficina";
                        Oficina.DataBind();

                        Oficina.Items.Insert(0, new ListItem("---Seleccione---", "0"));
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

        public void Limpiar()
        {
            ddlCanal.SelectedValue = "0";
            ddlCampana.Items.Clear();
            ddlMercaderista.Items.Clear();
            ddlOficina.Items.Clear();
            ddlNodeComercial.Items.Clear();
            ddlPuntoVenta.Items.Clear();
            ddlCategoria.Items.Clear();
            ddlMarca.Items.Clear();
            ddlTipoProm.Items.Clear();
            ddlTipoAct.Items.Clear();
            ddlPop.Items.Clear();
            ddlEmpresa.Items.Clear();

            txtFecIniActividad.Clear();
            txtFecFinActividad.Clear();
            txtFecComunicacion.Clear();
            ddlGrupoObj.Items.Clear();
            txtPrecioCosto.Text = "";
            txtPrecioPVP.Text = "";
            txtGrupObjComen.Text = "";
            txtCantPersonal.Text = "";
            txtPremio.Text = "";
            txtMecanica.Text = "";
            txtMatApoyo.Text = "";
            txtObservacion.Text = "";



        }

        protected void ddlCampana_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dte = null;
            DataTable dtl = null;
            Conexion Ocoon = new Conexion();
            string sidplanning = ddlCampana.SelectedValue;

            if (ddlCampana.SelectedIndex != 0)
            {

                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);

                ddlCategoria.Items.Clear();
                ddlMarca.Items.Clear();
                ddlMarca.Enabled = false;
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

                dte = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_TIPO_ACTIVIDADxPLANNING", sidplanning);

                if (dt.Rows.Count > 0)
                {
                    ddlTipoAct.DataSource = dte;
                    ddlTipoAct.DataValueField = "id_Tipo_Act";
                    ddlTipoAct.DataTextField = "descripcion";
                    ddlTipoAct.DataBind();
                    ddlTipoAct.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                    ddlTipoAct.Enabled = true;
                }


                Conexion ocon = new Conexion(2);
                dtl = ocon.ejecutarDataTable("STP_JVM_LISTAR_POPxPlanning", sidplanning);

                if (dt.Rows.Count > 0)
                {
                    ddlPop.DataSource = dtl;
                    ddlPop.DataValueField = "ID_POP";
                    ddlPop.DataTextField = "POP_DESCRIPCION";
                    ddlPop.DataBind();
                    ddlPop.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                    ddlPop.Enabled = true;
                }



                cargarOficina(ddlOficina);
                cargaNodeComercial(sidplanning,ddlNodeComercial);
                cargarCategoria(sidplanning,ddlCategoria);
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


                ddlMercaderista.Items.Clear();
                ddlMercaderista.Enabled = false;

                ddlNodeComercial.Items.Clear();
                ddlNodeComercial.Enabled = false;
                ddlPuntoVenta.Items.Clear();
                ddlPuntoVenta.Enabled = false;
            }
            MopoReporCompetencia.Show();
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
                MopoReporCompetencia.Show();
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
                    DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", ddlCampana.SelectedValue, Convert.ToInt32(ddlNodeComercial.SelectedValue));

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
                MopoReporCompetencia.Show();
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
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_REPORT_COMPETENCIA", sidcategoriaproducto);

            ddlMarca.Items.Clear();
            ddlMarca.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                ddlMarca.DataSource = dt;
                ddlMarca.DataValueField = "id_Brand";
                ddlMarca.DataTextField = "Name_Brand";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlMarca.Enabled = true;
            }
            MopoReporCompetencia.Show();
        }

        protected void btnGuardarReportCompetencia_Click(object sender, EventArgs e)
        {


                try
                {
                    if (ddlCanal.Text == "0" || ddlCampana.Text == "0" || ddlMercaderista.Text == "0"
                        || ddlNodeComercial.Text == "0" || ddlPuntoVenta.Text == "0" || ddlCategoria.Text == "0"
                        ||  txtFecIniActividad.SelectedDate.ToString()=="" || txtFecFinActividad.SelectedDate.ToString()=="")
                    {
                        lblmensaje.Visible = true;
                        lblmensaje.Text = "Ingrese campos requeridos";
                        lblmensaje.ForeColor = System.Drawing.Color.Red;
                        return;
                      
                    }

                    if (txtFecIniActividad.SelectedDate > txtFecFinActividad.SelectedDate)
                    {
                        lblmensaje.Visible = true;
                        lblmensaje.Text = "La fecha de inicio no puede ser mayor que la fecha de Fin";
                        lblmensaje.ForeColor = System.Drawing.Color.Red;
                        return;
                      
                    }

                    Lucky.Business.Common.Application.OPE_REPORTE_COMPETENCIA oOPE_REPORTE_COMPETENCIA = new Lucky.Business.Common.Application.OPE_REPORTE_COMPETENCIA();
                    Lucky.Entity.Common.Application.EOPE_REPORTE_COMPETENCIA oEOPE_REPORTE_COMPETENCIA = new Lucky.Entity.Common.Application.EOPE_REPORTE_COMPETENCIA();

                    DataTable dt = null;
                    Conexion Ocoon = new Conexion();
                    dt = Ocoon.ejecutarDataTable("UP_WEB_SEARCH_USER", "", Convert.ToInt32(ddlMercaderista.SelectedValue));
                    string idperfil = dt.Rows[0]["Perfil_id"].ToString();

                    oEOPE_REPORTE_COMPETENCIA = oOPE_REPORTE_COMPETENCIA.RegistrarReporteCompetencia(Convert.ToInt32(ddlMercaderista.SelectedValue), idperfil, ddlCampana.SelectedValue, this.Session["companyid"].ToString(), ddlPuntoVenta.SelectedValue,
                                                                         ddlCategoria.SelectedValue, ddlMarca.SelectedValue, ddlTipoProm.SelectedValue, ddlTipoAct.SelectedValue, ddlGrupoObj.SelectedValue,
                                                                         txtPrecioCosto.Text, txtPrecioPVP.Text, txtFecIniActividad.SelectedDate.ToString(), txtFecFinActividad.SelectedDate.ToString(),
                                                                         txtGrupObjComen.Text, txtCantPersonal.Text, txtPremio.Text, txtMecanica.Text, txtMatApoyo.Text, txtObservacion.Text, DateTime.Now.ToShortDateString(),
                                                                         "0", "0", "0", txtFecComunicacion.SelectedDate.ToString(), ddlEmpresa.SelectedValue);


                    oOPE_REPORTE_COMPETENCIA.RegistrarReporteCompetencia_Detalle(Convert.ToInt32(oEOPE_REPORTE_COMPETENCIA.ID_COMPETENCIA), ddlPop.SelectedValue);

                    Limpiar();
                    lblmensaje.Visible = true;
                    lblmensaje.Text = "La fecha de inicio no puede ser mayor que la fecha de Fin";
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

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string a = Server.MapPath("temp.jpg");
            Session["path"] = a;
            MyImage FImage;
            FImage = new MyImage(RadBinaryImage_viewFoto.ImageUrl, (byte[])Session["array"]);
            FImage.Rotate("90", Server.MapPath("temp.jpg"));
            //Image1.ImageUrl = "temp.jpg";
            RadBinaryImage_viewFoto.DataValue = FImage.imageToByteArray((System.Drawing.Image)Session["imagenabyte"]);
 

            FImage = null;

            ModalPopupExtender_viewfoto.Show();
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            string a = Server.MapPath("temp.jpg");
            Session["path"] = a;
            MyImage FImage;
           
            FImage = new MyImage(RadBinaryImage_viewFoto.ImageUrl, (byte[])Session["array"]);
            FImage.Rotate("270", Server.MapPath("temp.jpg"));
            //Image1.ImageUrl = "temp.jpg";
            RadBinaryImage_viewFoto.DataValue = FImage.imageToByteArray((System.Drawing.Image)Session["imagenabyte"]);


            FImage = null;

            ModalPopupExtender_viewfoto.Show();
        }

        protected void ibtnGuardarImagen_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Int32 iidregft = Convert.ToInt32(Session["iidregft"]);
                Int32 Id_rcompe = Convert.ToInt32(Session["Id_rcompe"]);

                Conexion Ocoon = new Conexion();

                byte[] foto = (byte[])Session["array"];

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REG_COMPE_FOTO", iidregft, Id_rcompe, foto, Session["sUser"].ToString(), DateTime.Now);
                RadBinaryImage_fotoBig.Visible = false;
                cargar_rgv_competencia();

            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
    }

}
