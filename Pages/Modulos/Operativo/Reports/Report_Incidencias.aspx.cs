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
    public partial class Report_Incidencias : System.Web.UI.Page
    {

   #region Declaracion de variables generales

        private static DataTable dt_Foto;
        private int compañia;
        private string pais;
        Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();


        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            btn_buscar.Attributes.Add("onclick", "javascript:document.getElementById('"

+ btn_buscar.ClientID + "').disabled=true;" +"javascript:document.getElementById('"

+ btn_buscar.ClientID + "').value='Cargando...';" + this.GetPostBackEventReference(btn_buscar));


            if (!Page.IsPostBack)
            {

                CargarCombo_Channel();
                CargarCombo_TipoReporte();
            }
        }

        protected void CargarCombo_TipoReporte()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
             compañia = Convert.ToInt32(this.Session["companyid"]);

             dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_TIPO_REPORTE_CLIENTE_REPORTEID", compañia, 57);
            if (dt.Rows.Count > 0)
            {
                cmbtiporeporte.DataSource = dt;
                cmbtiporeporte.DataValueField = "id_Tipo_Reporte";
                cmbtiporeporte.DataTextField = "TipoReporte_Descripcion";
                cmbtiporeporte.DataBind();
                cmbtiporeporte.Items.Insert(0, new ListItem("---Todos---", "0"));
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
            //tdcampaña.Visible = true;
            cmbplanning.Visible = true;
            string sidchannel = cmbcanal.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel,compañia);


            cmbplanning.Items.Clear();
            cmbcategoria_producto.Items.Clear();
            cmbcategoria_producto.Enabled = false;
            cmbmarca.Items.Clear();
            cmbmarca.Enabled = false;
            cmbperson.Items.Clear();
            cmbperson.Enabled = false;
            cmbSupervisor.Items.Clear();
            cmbSupervisor.Enabled = false;

            cmbOficina.Items.Clear();
            cmbOficina.Enabled = false;
            cmbSector.Items.Clear();
            cmbSector.Enabled = false;
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
                lblmensaje.Text = "";
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
                cmbperson.Items.Clear();
                cmbperson.Enabled = false;

                if (dt.Rows.Count > 0)
                {
                    cmbperson.DataSource = dt;
                    cmbperson.DataValueField = "Person_id";
                    cmbperson.DataTextField = "Person_NameComplet";
                    cmbperson.DataBind();
                    //cmbperson.Items.Insert(0, new ListItem("---Todos---", "0"));
                    cmbperson.Enabled = true;
                }
                else
                {
                    lblmensaje.Text = "No se encontraron resultados.";
                    gv_incidencias.DataBind();
                }
                //------llamado al metodo cargar categoria de producto
                //string iischannel = cmbcanal.SelectedValue;
                cargarCombo_Oficina();
                cargarCombo_NodeComercial(sidplanning);
                cargarCombo_CategoriaDeproducto(compañia);
                cargarCombo_Sector(compañia);
                cargarCombo_Supervisores(sidplanning);
                //----------------------------------------------------
            }
            else
            {
                cmbcategoria_producto.Items.Clear();
                cmbcategoria_producto.Enabled = false;
                cmbmarca.Items.Clear();
                cmbmarca.Enabled = false;

                cmbSupervisor.Items.Clear();
                cmbSupervisor.Enabled = false;

                cmbperson.Items.Clear();
                cmbperson.Enabled = false;

                cmbOficina.Items.Clear();
                cmbOficina.Enabled = false;
                cmbSector.Items.Clear();
                cmbSector.Enabled = false;
                cmbNodeComercial.Items.Clear();
                cmbNodeComercial.Enabled = false;
                cmbPuntoDeVenta.Items.Clear();
                cmbPuntoDeVenta.Enabled = false;
            }
        }
        protected void cargarCombo_Supervisores(string sidplanning)
        {
            Conexion Ocoon = new Conexion();
            DataTable dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUPERVISOR", sidplanning);

            cmbSupervisor.DataSource = dt;
            cmbSupervisor.DataValueField = "Person_id";
            cmbSupervisor.DataTextField = "Person_NameComplet";
            cmbSupervisor.DataBind();
            cmbSupervisor.Items.Insert(0, new ListItem("---Todos---", "0"));
            cmbSupervisor.Enabled = true;
        }
        protected void cmbSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Conexion Ocoon = new Conexion();
            DataTable dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON_POR_SUPERVISOR", cmbplanning.SelectedValue, cmbSupervisor.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                cmbperson.DataSource = dt;
                cmbperson.DataValueField = "Person_id";
                cmbperson.DataTextField = "Person_NameComplet";
                cmbperson.DataBind();
                //cmbperson.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbperson.Enabled = true;
            }
            else
            {
                cmbplanning_SelectedIndexChanged(sender, e);
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

                    DataTable dtNodeCom = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_MALLA_BY_idPlanning_and_codOficina", cmbplanning.SelectedValue, Convert.ToInt32(cmbOficina.SelectedValue));

                    if (dtNodeCom.Rows.Count > 0)
                    {
                        cmbNodeComercial.Enabled = true;
                        cmbNodeComercial.Items.Clear();
                        cmbNodeComercial.DataSource = dtNodeCom;
                        cmbNodeComercial.DataTextField = "malla";
                        cmbNodeComercial.DataValueField = "id_malla";
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
        protected void cargarCombo_Sector(int company_id)
        {
            try
            {
                Conexion Ocoon = new Conexion();
                cmbSector.Items.Clear();
                DataTable dtNodeCom = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_MALLAS_POR_CLIENTE", company_id);

                if (dtNodeCom.Rows.Count > 0)
                {
                    cmbSector.Enabled = true;
                    cmbSector.DataSource = dtNodeCom;
                    cmbSector.DataTextField = "malla";
                    cmbSector.DataValueField = "id_malla";
                    cmbSector.DataBind();

                    cmbSector.Items.Insert(0, new ListItem("---Todos---", "0"));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void cmbSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Conexion Ocoon = new Conexion();
                DataTable dtNodeCom = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_NODECOMERCIAL_BY_idPlanning_and_idMalla", cmbplanning.SelectedValue, Convert.ToInt32(cmbSector.SelectedValue));

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


            string sidcategoriaproducto = cmbcategoria_producto.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_BY_CATEGORIA_ID", sidcategoriaproducto);

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
           // cargarGrilla_ReporteFotografico();
        }
        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrilla_ReporteIncidencias();
        }
        protected void cargarGrilla_ReporteIncidencias()
        {
            try
            {
                DataTable dt = null;
                //------------------------volver a deshabilitgar las columnas--------------------------
                gv_incidencias.Columns[6].Visible = false;

                //-----------------//-----------------//-----------------//-----------------//-----------------

                string sidtipoReport = cmbtiporeporte.SelectedValue;
                string iidpersons = "0";
                if (cmbperson.SelectedIndex >= 0)
                {
                    /// Obtener id del person y anidarlos en una cadena------------
                    /// Ditmar Estrada

                    string cadenaIdPersons = "";

                    for (int i = 0; i < cmbperson.Items.Count; i++)
                    {
                        if (cmbperson.Items[i].Selected)
                        {
                            cadenaIdPersons = cadenaIdPersons + cmbperson.Items[i].Value + ",";
                        }
                    }
                    iidpersons = cadenaIdPersons.Substring(0, cadenaIdPersons.Length - 1);

                    //----
                }

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
                string sidmarca = cmbmarca.SelectedValue;
                if (sidmarca == "")
                    sidmarca = "0";
                int iidsupervisor = Convert.ToInt32(cmbSupervisor.SelectedValue);
                int iidsector = Convert.ToInt32(cmbSector.SelectedValue);

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

                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REPORTE_INCIDENCIAS_BORRAR", iidsupervisor, iidpersons, sidplanning, sidchannel, icod_oficina, iidsector, iidNodeComercial, sidPDV, sidcategoriaProducto, sidmarca, sidtipoReport, dfecha_inicio, dfecha_fin);
                    dt_Foto = load_images_rgv_Incidencias(dt);
                    
                    
                    if (dt.Rows.Count > 0)
                    {
                        gv_incidencias.DataSource = dt_Foto;
                        gv_incidencias.DataBind();


                        gv_incidenciasToExcel.DataSource = dt_Foto;
                        gv_incidenciasToExcel.DataBind();

                        lblmensaje.ForeColor = System.Drawing.Color.Green;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Blue;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                        gv_incidencias.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.Text = "";
                if (cmbplanning.SelectedIndex > 0)
                    lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
            }
        }
        private DataTable load_images_rgv_Incidencias(DataTable source)
        {
            double menor=0;
            int index=0;
            DataTable fotos = new DataTable();
            Conexion OConn = new Conexion(1);

            source.Columns.Add("Id_regft", typeof(Int64));
            source.Columns.Add("foto", typeof(Byte[]));

            for (int i = 0; i < source.Rows.Count; i++)
            {
                fotos = OConn.ejecutarDataTable("UP_WEBXPLORA_OPE_REPORTE_INCIDENCIA_FOTO", Convert.ToInt64(source.Rows[i]["Id_incidencia"].ToString()));
                if (fotos.Rows.Count > 0 && fotos.Rows[0]["foto"] != System.DBNull.Value)
                {
                    for (int j = 0; j < fotos.Rows.Count; j++)
                    {

                        double time = ((DateTime)fotos.Rows[j]["Fecha_Foto"] - (DateTime)fotos.Rows[j]["Fecha_Repor"]).TotalSeconds;

                        if (time < 0)
                        {
                            time = time * (-1);
                        }

                        if(menor==0)
                        {
                            menor=time;
                            index = j;
                        }
                        else
                        {

                            if (time > menor)
                            {
                                menor = menor + 0;
                            
                            }
                            else
                            {
                                menor = time;
                                index = j;
                            }
                            
                        }

                    }
                    menor = 0;
                    source.Rows[i]["Id_regft"] = Convert.ToInt64(fotos.Rows[index]["Id_regft"]);
                    source.Rows[i]["foto"] = (Byte[])(fotos.Rows[index]["foto"]);
                }
                else
                {
                    source.Rows[i]["Id_regft"] = 0;
                    source.Rows[i]["foto"] = getBytesNoImage();
                }
            }
            return source;
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
        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                gv_incidenciasToExcel.Visible = true;
                GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Incidencias", this.gv_incidenciasToExcel);
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
            // GridHeaderItem header = "";

            //CheckBox cb_all = (CheckBox)gv_incidencias.FindControl("cb_all");
            bool validar = cb_all.Checked;

            for (int i = 0; i < gv_incidencias.Items.Count; i++)
            {
                GridItem item = gv_incidencias.Items[i];
                //if (item.ItemType == GridItemType.Item)
                //{

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");

                if (cb_validar.Enabled == true)
                {
                    if (validar == true)
                    {
                        cb_validar.Checked = true;
                        lbl_cb_all.Text = "Deseleccionar todos";
                    }
                    else if (validar == false)
                    {
                        cb_validar.Checked = false;
                        lbl_cb_all.Text = "Seleccionar todos";
                    }
                }

            }
        }
        protected void btn_validar_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < gv_incidencias.Items.Count; i++)
            {
                GridItem item = gv_incidencias.Items[i];
                // if (item.ItemType == GridItemType.Item)
                //{

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");

                Label lbl_validar = (Label)item.FindControl("lbl_validar");
                Label Id_incidenciaDetalle = (Label)item.FindControl("Id_incidenciaDetalle");


                int id = Convert.ToInt32(Id_incidenciaDetalle.Text);
                bool validar = cb_validar.Checked;
                update_incidencias_detalle_validado(id, validar);
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

            //cargarGrilla_Competencias();
        }
        protected void update_incidencias_detalle_validado(int id, bool validar)
        {

            try
            {
                Conexion Ocoon = new Conexion();

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_INCIDENCIAS_DETALLE_VALIDADO", id, Session["sUser"].ToString(), DateTime.Now, validar);

            }
            catch (Exception ex)
            {
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
        protected void gv_incidencias_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = gv_incidencias.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                gv_incidencias.DataSource = dataView;
                gv_incidencias.DataBind();
            }

        }

        protected void gv_incidencias_EditCommand(object source, GridCommandEventArgs e)
        {
            cargarGrilla_ReporteIncidencias();
        }

        protected void gv_incidencias_CancelCommand(object source, GridCommandEventArgs e)
        {
            cargarGrilla_ReporteIncidencias();
        }

        protected void gv_incidencias_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
         
            try
            {
                cargarGrilla_ReporteIncidencias();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_incidencias_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                lblmensaje.Text = "";
                Conexion Ocoon = new Conexion();

                GridItem item = gv_incidencias.Items[e.Item.ItemIndex];

                Label Id_incidenciaDetalle = (Label)item.FindControl("Id_incidenciaDetalle");
                int iid_det = Convert.ToInt32(Id_incidenciaDetalle.Text.Trim());

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

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_INCIDENCIAS", iid_det, Fec_reg_bd, Session["sUser"].ToString(), DateTime.Now, ckvalidado.Checked);

                cargarGrilla_ReporteIncidencias();
            }
            catch (Exception ex)
            {
                lblmensaje.Text = ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_incidencias_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
            gv_incidencias.CurrentPageIndex = e.NewPageIndex;
            cargarGrilla_ReporteIncidencias();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

        }

        protected void gv_incidencias_DataBound(object sender, EventArgs e)
        {
            if(cmbtiporeporte.SelectedValue=="01")
            {
                gv_incidencias.Columns[6].Visible = true;
                gv_incidencias.Columns[7].Visible = true;
                gv_incidencias.Columns[8].Visible = false;
                gv_incidencias.Columns[9].Visible = false;
            }
            else if (cmbtiporeporte.SelectedValue == "02")
            {
                gv_incidencias.Columns[6].Visible = false;
                gv_incidencias.Columns[7].Visible = false;
                gv_incidencias.Columns[8].Visible = true;
                gv_incidencias.Columns[9].Visible = true;
            }

            if (gv_incidencias.Items.Count > 0)
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
        protected void cmbperson_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel_mercaderistas.Controls.Clear();
            Panel_mercaderistas.Controls.Add(new Literal() { Text = "<b>Las personas seleccionadas son:</b>" + "<br/>" });
            foreach (ListItem item in (sender as ListControl).Items)
            {
                if (item.Selected)
                {

                    Panel_mercaderistas.Controls.Add(new Literal() { Text = item.Text + "<br/>" });

                }
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

                progress.CurrentOperationText = file.GetName() + " Empezando a procesar...";

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
                Int32 Id_repft = Convert.ToInt32(Session["Id_repft"]);

                Conexion Ocoon = new Conexion();

                byte[] foto = (byte[])Session["byteArrayIn"];

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REG_FOTO_INCIDENCIA", iidregft, Id_repft, foto, Session["sUser"].ToString(), DateTime.Now);
                RadBinaryImage_fotoBig.Visible = false;
                cargarGrilla_ReporteIncidencias();

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

        protected void gv_incidencias_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                panelEdit.DataBind();
                RadBinaryImage_fotoBig.DataBind();
                RadBinaryImage_fotoBig.Visible = false;
                //GridItem item = gv_Foto.Items[e.Item.ItemIndex];
                int a = e.Item.ItemIndex;
                if (a == -1)
                    return;
                GridItem item = gv_incidencias.Items[e.Item.ItemIndex];






                Label lbl_id_reg_foto = (Label)item.FindControl("lbl_id_reg_foto");

                Label lbl_Id_Reg_Fotogr = (Label)item.FindControl("Id_incidencia");
                if (e.CommandName == "VERFOTO")
                {
                    Session["iidregft"] = Convert.ToInt32(lbl_id_reg_foto.Text);
                    Session["Id_repft"] = Convert.ToInt32(lbl_Id_Reg_Fotogr.Text);

                    DataTable dt = null;
                    Conexion Ocoon = new Conexion();
                    //string sidregft = ((LinkButton)sender).CommandArgument;
                    int iidregft = Convert.ToInt32(lbl_id_reg_foto.Text);

                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REG_FOTO", iidregft);
                    if (dt.Rows.Count == 0)
                    {
                        lblencabezado.Text = "INFORMACION";
                        lblmensajegeneral.Text = "La Foto Seleccionada no existe";
                        ImgMensaje.ImageUrl = "~/Pages/images/Mensajes/warning_blue.png";
                        divMensaje.Style.Value = "border-width:10px;border-style:solid;border-color:#53A2FF; height:169px;background-color:#9FCBFF";
                        ModalPopupMensaje.Show();
                        return ;
                    }
                    byte[] byteArrayIn = (byte[])dt.Rows[0]["foto"];

                    Session["array"] = byteArrayIn;
                    RadBinaryImage_viewFoto.DataValue = byteArrayIn;
                    ModalPopupExtender_viewfoto.Show();

                    //Response.Redirect("verFoto.aspx?iidregft=" + iidregft, true);
                }
                else if (e.CommandName == "EDITFOTO")
                {

                    //string sidregft = ((LinkButton)senser).CommandArgument;
                    int iidregft = Convert.ToInt32(lbl_id_reg_foto.Text);
                    int Id_repft = Convert.ToInt32(lbl_Id_Reg_Fotogr.Text);
                    if (iidregft== 0)
                    {
                        lblencabezado.Text = "INFORMACION";
                        lblmensajegeneral.Text = "La Foto Seleccionada no existe";
                        ImgMensaje.ImageUrl = "~/Pages/images/Mensajes/warning_blue.png";
                        divMensaje.Style.Value = "border-width:10px;border-style:solid;border-color:#53A2FF; height:169px;background-color:#9FCBFF";
                        ModalPopupMensaje.Show();

                        return;
                    }
                    else
                    {
                        RadBinaryImage imageBinary = (RadBinaryImage)item.FindControl("RadBinaryImage_foto");

                        RadBinaryImage_fotoBig.DataValue = imageBinary.DataValue;
                        RadBinaryImage_fotoBig.Visible = false;
                        Session["iidregft"] = iidregft;
                        Session["Id_repft"] = Id_repft;
                    }
                    ModalPopup_Edit.Show();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
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
                Int32 Id_repft = Convert.ToInt32(Session["Id_repft"]);

                Conexion Ocoon = new Conexion();

                if (chkValidar.Checked == true)
                {
                    update_urlFoto(iidregft);
                }
                byte[] foto = (byte[])Session["array"];

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REG_FOTO_INCIDENCIA", iidregft, Id_repft, foto, Session["sUser"].ToString(), DateTime.Now);
                RadBinaryImage_fotoBig.Visible = false;
                cargarGrilla_ReporteIncidencias();

            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        protected void update_urlFoto(int iidregft)
        {
            try
            {
                System.Drawing.Image newImage;

               

                for (int i = 0; i < dt_Foto.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt_Foto.Rows[i]["Id_regft"]) == iidregft)
                    {

                        string PhotoNameAsUrlInDb = iidregft + ".jpg";
                        byte[] byteArrayIn = (byte[])dt_Foto.Rows[i]["foto"];

                        string strFileName = Server.MapPath(@"~\Pages\Modulos\Cliente\Reportes\Galeria_fotografica\Fotos") + "\\" + PhotoNameAsUrlInDb;
                        if (byteArrayIn != null)
                        {
                            using (MemoryStream stream = new MemoryStream(byteArrayIn))
                            {
                                newImage = System.Drawing.Image.FromStream(stream);

                                newImage.Save(strFileName);
                                //miImagen.Attributes.Add("src", strFileName);
                            }
                        }
                        Conexion Ocoon = new Conexion();
                        Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REG_FOTO_FOTOGRAFICO_URLFOTO", iidregft, this.Session["sUser"], DateTime.Now, PhotoNameAsUrlInDb);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        } 

    }
}
