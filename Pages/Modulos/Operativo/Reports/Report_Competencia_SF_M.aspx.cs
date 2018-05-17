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
using System.IO;
using Lucky.CFG.Util;
using Telerik.Web.UI;
using System.Net;
using System.Threading;


namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_Competencia_SF : System.Web.UI.Page
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

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);

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
                cargarCombo_corporaciones(sidplanning);
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
            cargarGrilla_Competencias();
        }
        GridView gv = new GridView();
        protected void cargarGrilla_Competencias()
        {
            try
            {

                DataTable dt = null;

                int iidperson = Convert.ToInt32(cmbperson.SelectedValue);
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

                    Conexion Ocoon = new Conexion();
                    //dt = obj_Facade_Proceso_Operativo.Get_ReporteCompetencias(iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, dfecha_inicio, dfecha_fin);
                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_COMPETENCIA_2", sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, dfecha_inicio, dfecha_fin);

                    
                    gv_competencia.DataSource = dt;
                    gv_competencia.DataBind();
                    btn_img_exporttoexcel.Enabled = true;

                    //gv_competenciaToExcel.DataSource = dt;
                    //gv_competenciaToExcel.DataBind();

                    lblmensaje.Visible = true;
                    lblmensaje.Text = "Se encontro " + dt.Rows.Count + " resultados";

                }
            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                gv_competencia.DataBind();
                lblmensaje.Text = "";
                if (cmbplanning.SelectedIndex > 0)
                    lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
            }

        }

        protected void cargarGrilla_Competencias_toExcel()
        {
            try
            {

                DataTable dt = null;

                int iidperson = Convert.ToInt32(cmbperson.SelectedValue);
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

                    Conexion Ocoon = new Conexion();
                    //dt = obj_Facade_Proceso_Operativo.Get_ReporteCompetencias(iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, dfecha_inicio, dfecha_fin);
                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_COMPETENCIA_2_TO_EXCEL", sidplanning, sidchannel, iidcorporacion, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, dfecha_inicio, dfecha_fin);


                    //gv_competencia.DataSource = dt;
                    //gv_competencia.DataBind();
                    btn_img_exporttoexcel.Enabled = true;

                    gv_competenciaToExcel.DataSource = dt;
                    gv_competenciaToExcel.DataBind();

                    lblmensaje.Visible = true;
                    lblmensaje.Text = "Se encontro " + dt.Rows.Count + " resultados";

                }
            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                gv_competencia.DataBind();
                lblmensaje.Text = "";
                if (cmbplanning.SelectedIndex > 0)
                    lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
            }

        }
        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                cargarGrilla_Competencias_toExcel();
                gv_competenciaToExcel.Visible = true;
                GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Competencia", this.gv_competenciaToExcel);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //}

        protected void gv_competencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            cargarGrilla_Competencias();

        }
        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            bool validar = cb_all.Checked;
            for (int i = 0; i < gv_competencia.Items.Count; i++)
            {
                GridItem item = gv_competencia.Items[i];
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
        protected void btn_validar_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < gv_competencia.Items.Count; i++)
            {
                GridItem item = gv_competencia.Items[i];
                // if (item.ItemType == GridItemType.Item)
                //{

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");

                Label lbl_validar = (Label)item.FindControl("lbl_validar");
                Label lbl_Id_regcompetencia = (Label)item.FindControl("lblregcompetencia");


                int id = Convert.ToInt32(lbl_Id_regcompetencia.Text);
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
        protected void update_competencia_detalle_validado(int id_compet, bool validar)
        {

            try
            {
                Conexion Ocoon = new Conexion();

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_COMPETENCIA_DETALLE_VALIDADO_SF_MODERNO", id_compet, validar, this.Session["sUser"], DateTime.Now);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void btn_img_buscar_detalle_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Conexion Ocoon = new Conexion();

                string sidcompetencia = ((ImageButton)sender).CommandArgument;
                int iidcompetencia = Convert.ToInt32(sidcompetencia);

                DataTable dt2 = null;
                dt2 = Ocoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_REPORTE_COMPETENCIA_FOTO", iidcompetencia);

                if (dt2.Rows.Count > 0)
                {
                    string photoName = dt2.Rows[0]["Id_regft"].ToString() + ".jpg"; ;
                    byte[] byteArrayIn = (byte[])dt2.Rows[0]["foto"];

                    //string strFileName = Server.MapPath(@"~\Pages\Modulos\Cliente\Reportes\Galeria_fotografica\Fotos") + "\\" + photoName;

                    Save_photo(photoName, byteArrayIn);

                    Image foto_url = Panel_DetalleCompetencia.FindControl("foto_url") as Image;
                    foto_url.ImageUrl = "~/Pages/Modulos/Cliente/Reportes/Galeria_fotografica/Fotos/" + photoName;

                    ModalPopupExtender_detalle.Show();
                }
                else
                {
                    foto_url.ImageUrl = "~/Pages/Modulos/Cliente/Reportes/Galeria_fotografica/Fotos/sin_url_imagen.jpg";
                    ModalPopupExtender_detalle.Show();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

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
                int iidregft = 0;
                if (dt_miFoto.Rows.Count > 0)
                {
                    iidregft = Convert.ToInt32(dt_miFoto.Rows[0]["Id_regft"].ToString());
                    PhotoNameAsUrlInDb = iidregft + ".jpg";
                    byte[] byteArrayIn = (byte[])dt_miFoto.Rows[0]["foto"];

                    string strFileName = Server.MapPath(@"~\Pages\Modulos\Cliente\Reportes\Informe_de_Competencia\Fotos") + "\\" + PhotoNameAsUrlInDb;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream stream = new MemoryStream(byteArrayIn))
                        {
                            newImage = System.Drawing.Image.FromStream(stream);

                            newImage.Save(strFileName);
                            //miImagen.Attributes.Add("src", strFileName);
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
      
        protected void gv_competencia_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Conexion Ocoon = new Conexion();

            //try
            //{
            //    GridViewRow row = gv_competencia.Rows[gv_competencia.EditIndex];

            //    Label oLabel_id_cabz_comp = (Label)row.FindControl("lbl_id_reg_competencia");
            //    int iid_Cabz_compet = Convert.ToInt32(oLabel_id_cabz_comp.Text.Trim());

            //    Label lbl_id_reg_comp_detalle = (Label)row.FindControl("lbl_id_reg_comp_detalle");
            //    int id_compDetll = Convert.ToInt32(lbl_id_reg_comp_detalle.Text.Trim());

            //    Label lbl_tipoReg = (Label)row.FindControl("lbl_tipoReg");
            //    char tipoReg = Convert.ToChar(lbl_tipoReg.Text.Trim());

            //    RadDateTimePicker RadDateTimePicker_fec_reg = (RadDateTimePicker)row.FindControl("RadDateTimePicker_fec_reg");
            //    Label lblobs = row.FindControl("lblobs") as Label;

            //    TextBox txtobs = row.FindControl("txtobs") as TextBox;

            //    CheckBox cb_validar = (CheckBox)row.FindControl("cb_validar");

            //    string Precio_Costo = ((TextBox)(row.Cells[8].Controls[0])).Text;
            //    string Precio_PVP = ((TextBox)(row.Cells[9].Controls[0])).Text;
            //    string Fec_Ini_Act = ((TextBox)(row.Cells[10].Controls[0])).Text;
            //    string Fec_Fin_Act = ((TextBox)(row.Cells[11].Controls[0])).Text;

            //    string Cant_Personal = ((TextBox)(row.Cells[12].Controls[0])).Text;
            //    string Premio = ((TextBox)(row.Cells[13].Controls[0])).Text;
            //    string Mecanica = ((TextBox)(row.Cells[14].Controls[0])).Text;
            //    //string observacion = ((TextBox)(row.Cells[15].Controls[0])).Text;
            //    string Txt_Grupo_Obj = ((TextBox)(row.Cells[18].Controls[0])).Text;
            //    string Mat_Apoyo = ((TextBox)(row.Cells[21].Controls[0])).Text;

            //    Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_COMPETENCIA", iid_Cabz_compet, id_compDetll, tipoReg, Precio_Costo, Precio_PVP, Fec_Ini_Act, Fec_Fin_Act, Cant_Personal, Premio, Mecanica, Txt_Grupo_Obj, Mat_Apoyo, RadDateTimePicker_fec_reg.DbSelectedDate, Session["sUser"].ToString(), DateTime.Now, txtobs.Text, cb_validar.Checked);

            //    gv_competencia.EditIndex = -1;
            //    cargarGrilla_Competencias();
            //}
            //catch (Exception ex)
            //{
            //    ex.Message.ToString();
            //    Response.Redirect("~/err_mensaje_seccion.aspx", true);
            //}
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

        protected void gv_competencia_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            cargarGrilla_Competencias();
        }

        protected void gv_competencia_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            cargarGrilla_Competencias();
        }

        protected void gv_competencia_PdfExporting(object source, GridPdfExportingArgs e)
        {

        }

        protected void gv_competencia_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                lblmensaje.Text = "";
                Conexion Ocoon = new Conexion();

                GridItem item = gv_competencia.Items[e.Item.ItemIndex];

                Label lbl_id_regcompetencia = (Label)item.FindControl("lblregcompetencia");
                int iid_regcompetencia = Convert.ToInt32(lbl_id_regcompetencia.Text.Trim());
                

                CheckBox ckvalidado = (CheckBox)item.FindControl("cb_validar");
                //psalas, 16/08/2011, se agrega esta logica porque en la tabla ope_reporte_competencia,
                //los validados se consideran como 0 y los invalidados como 1
                if (ckvalidado.Checked == true)
                {
                    ckvalidado.Checked = false;
                }
                else {
                    ckvalidado.Checked = true;
                }


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

                DateTime promocionini = Convert.ToDateTime((ArrayEditorValue[0] as RadDateTimePicker).SelectedDate);
                DateTime promocionfin = Convert.ToDateTime((ArrayEditorValue[1] as RadDateTimePicker).SelectedDate);
                string precioregular = ArrayEditorValue[2].ToString();
                //psalas. 16/08/2011. se agrega preciooferta por requerimiento san fernando
                string preciooferta = ArrayEditorValue[3].ToString();

                string strpromocionini = Convert.ToString(promocionini);
                string strpromocionfin = Convert.ToString(promocionfin);

                
                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_COMPETENCIA_SF_MODERNO", iid_regcompetencia, precioregular,preciooferta, strpromocionini, strpromocionfin, Session["sUser"].ToString(), DateTime.Now, ckvalidado.Checked);
                cargarGrilla_Competencias();
                

            }
            catch (Exception ex)
            {
                lblmensaje.Text = ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

        }

        protected void gv_competencia_EditCommand(object source, GridCommandEventArgs e)
        {
            cargarGrilla_Competencias();
        }

        protected void gv_competencia_DataBound(object sender, EventArgs e)
        {
            if (gv_competencia.Items.Count > 0)
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

        protected void gv_competencia_CancelCommand(object source, GridCommandEventArgs e)
        {
            cargarGrilla_Competencias();
        }

        protected void btn_validar_Click_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gv_competencia.Items.Count; i++)
            {
                GridItem item = gv_competencia.Items[i];
                // if (item.ItemType == GridItemType.Item)
                //{

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");
                Label lbl_validar = (Label)item.FindControl("lbl_validar");
                Label lbl_id_regcompetencia = (Label)item.FindControl("lblregcompetencia");
                


                if (cb_validar.Enabled == true)
                {

                    int idregcompetencia = Convert.ToInt32(lbl_id_regcompetencia.Text);
                   
                    bool validar = cb_validar.Checked;
                    //psalas, 16/08/2011, se agrega esta logica porque en la tabla ope_reporte_competencia,
                    //los registros validados tiene valor 0 y los invalidados 1
                    if (validar == true)
                        validar = false;
                    else
                        validar = true;

                    if (validar == true)
                    {
                        update_competencia_detalle_validado(idregcompetencia, validar);

                        //psalas, 16/08/2011 se cambia la logica: los validados son 0 y los invalidados 1
                        //lbl_validar.Text = "valido";
                        //lbl_validar.ForeColor = System.Drawing.Color.Green;
                        lbl_validar.Text = "invalido";
                        lbl_validar.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        update_competencia_detalle_validado(idregcompetencia, validar);
                        //psalas, 16/08/2011 se cambia la logica: los validados son 0 y los invalidados 1
                        //lbl_validar.Text = "invalidado";
                        //lbl_validar.ForeColor = System.Drawing.Color.Red;
                        lbl_validar.Text = "validado";
                        lbl_validar.ForeColor = System.Drawing.Color.Green;
                    }
                }

            }

        }


    }
}