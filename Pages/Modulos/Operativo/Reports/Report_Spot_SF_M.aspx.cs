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
using System.Drawing.Imaging;
using System.Drawing;
using Lucky.CFG.Tools;
using Lucky.Business.Common.Application;
using Lucky.Entity.Common.Security;
using System.Data.OleDb;

namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_Spot_SF_M : System.Web.UI.Page
    {

        #region Declaracion de variables generales
        private int compañia;
        private string pais;
        Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private static DataTable dt_Foto;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["companyid"]) == 1592)
            {
                
                lblComentario.Visible = true;
            }


            if (!Page.IsPostBack)
            {
                CargarCombo_Channel();
                CargarCanal();
                cargarCadena();
                CargarCategoria();
                llenarMes();
                CargarMarca();
                CargarCompetencia();
                CargarCanal_CargaMasiva();
    

             
             
                RadProgressArea1.ProgressIndicators &= ~ProgressIndicators.SelectedFilesCount;


            }
        }
        

     protected void CargarTipoActividad()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();





            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_TIPOACTIVIDADXPLANNING", cmbplanning.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                cmbActividad.DataSource = dt;
                cmbActividad.DataValueField = "id_Tipo_Act";
                cmbActividad.DataTextField = "descripcion";
                cmbActividad.DataBind();
                cmbActividad.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbActividad.Enabled = true;
            }
        }

       protected void CargarCompetencia()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);



            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_CompetenciaXCliente", compañia.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbCompetencia.DataSource = dt;
                cmbCompetencia.DataValueField = "Company_id";
                cmbCompetencia.DataTextField = "Company_Name";
                cmbCompetencia.DataBind();
                cmbCompetencia.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbCompetencia.Enabled = true;
            }
        }


        

      protected void CargarMarca()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);


            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_MarcaXCompany", compañia.ToString(),"");
            if (dt.Rows.Count > 0)
            {
                cmbMarca.DataSource = dt;
                cmbMarca.DataValueField = "id_Brand";
                cmbMarca.DataTextField = "Name_Brand";
                cmbMarca.DataBind();
                cmbMarca.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbMarca.Enabled = true;
            }
        }
        protected void CargarCategoria()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            pais = this.Session["scountry"].ToString();


            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_Product_CategoryXCompany_id", compañia.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbCategoria.DataSource = dt;
                cmbCategoria.DataValueField = "id_ProductCategory";
                cmbCategoria.DataTextField = "Product_Category";
                cmbCategoria.DataBind();
                cmbCategoria.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbCategoria.Enabled = true;


            }
        }

        protected void CargarFamilia()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();



            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_Product_FamilyXCategoria", cmbCategoria.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                cmbFamilia.DataSource = dt;
                cmbFamilia.DataValueField = "id_ProductFamily";
                cmbFamilia.DataTextField = "name_Family";
                cmbFamilia.DataBind();
                cmbFamilia.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbFamilia.Enabled = true;
            }
            else
            {
                cmbFamilia.Items.Clear();
                cmbFamilia.Enabled = false;
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
                lblmensaje.Text = "";
            }
        }

        protected void cmbplanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            string sidplanning = cmbplanning.SelectedValue;

            if (cmbplanning.SelectedIndex != 0)
            {
                CargarTipoActividad();
                //------llamado al metodo cargar categoria de producto
                //string iischannel = cmbcanal.SelectedValue;
               
                
                //----------------------------------------------------
            }
            else
            {
                
            }
        }



        protected void  llenarMes()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();



            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_MONTH");

            cmbMes.Items.Clear();
            cmbMes.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                cmbMes.DataSource = dt;
                cmbMes.DataValueField = "id_Month";
                cmbMes.DataTextField = "Month_name";
                cmbMes.DataBind();
                cmbMes.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbMes.Enabled = true;
            }
      
        }


        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrilla_ReporteFotografico();
        }
        public DataTable Get_ReporteFotografico(int iidperson, string sidplanning, string sidchanel, int cod_oficina, int id_NodeComercial, string ClientPDV_Code, string sidtiporeporte, string sid_categoriaproducto, string sidbrand, string stipreport, DateTime dfecha_inicio, DateTime dfecha_fin, int icompanyid)
        {
            Conexion oCoon = new Conexion();
            DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REPORTE_FOTOGRAFICO", iidperson, sidplanning, sidchanel, cod_oficina, id_NodeComercial, ClientPDV_Code, sidtiporeporte, sid_categoriaproducto, sidbrand, stipreport, dfecha_inicio, dfecha_fin, icompanyid);
            return dt;
        }
        protected void cargarGrilla_ReporteFotografico()
        {
            try
            {


                //string sidtipoReport = cmbtiporeporte.SelectedValue;
                //int iidperson = Convert.ToInt32(cmbperson.SelectedValue);

                //int icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);
                string sidplanning = cmbplanning.SelectedValue;
                string sidchannel = cmbcanal.SelectedValue;
                //int iidNodeComercial = Convert.ToInt32(cmbNodeComercial.SelectedValue);
                //string sidPDV = cmbPuntoDeVenta.SelectedValue;
                //if (sidPDV == "")
                //    sidPDV = "0";
                //string sidcategoriaProducto = cmbcategoria_producto.SelectedValue;
                //if (sidcategoriaProducto == "")
                //    sidcategoriaProducto = "0";
                string sidFamilia = cmbFamilia.SelectedValue;
                if (sidFamilia == "")
                    sidFamilia = "0";




                DateTime dfecha_inicio, dfecha_fin;
                if (txt_fecha_inicio.SelectedDate.ToString() == "" || txt_fecha_inicio.SelectedDate.ToString() == "0" || txt_fecha_inicio.SelectedDate == null)
                    dfecha_inicio = Convert.ToDateTime("01/01/1900");
                else dfecha_inicio = txt_fecha_inicio.SelectedDate.Value;


                if (txt_fecha_fin.SelectedDate.ToString() == "" || txt_fecha_fin.SelectedDate.ToString() == "0" || txt_fecha_fin.SelectedDate == null)
                    dfecha_fin = Convert.ToDateTime("01/01/1900");
                else dfecha_fin = txt_fecha_fin.SelectedDate.Value;

                if (DateTime.Compare(dfecha_inicio, dfecha_fin) == 1)
                {
                    lblmensaje.Visible = true;
                    lblmensaje.Text = "verifique si La fecha de inicio debe ser menor o igual a la fecha fin";
                    lblmensaje.ForeColor = System.Drawing.Color.Red;

                    return;
                }

                if (cmbcanal.SelectedValue == "0" || cmbplanning.SelectedValue == "0")
                {
                    lblmensaje.Visible = true;
                    lblmensaje.Text = "seleccione un canal y una campaña";
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {

                    compañia = Convert.ToInt32(this.Session["companyid"]);
                    //dt_Foto = Get_ReporteFotografico(iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, "R", sidcategoriaProducto, sidmarca, sidtipoReport, dfecha_inicio, dfecha_fin, compañia);
                    Conexion oCoon = new Conexion();
               
                        //es momentaneo cambiar el store para que sea uno solo
                    dt_Foto = oCoon.ejecutarDataTable("UP_WEBSIGE_OPE_REPORTE_SPOT_SF", sidplanning, sidchannel, cmbMes.SelectedValue, cmbCadena.SelectedValue, cmbCategoria.SelectedValue, sidFamilia, cmbCompetencia.SelectedValue, cmbMarca.SelectedValue, cmbActividad.SelectedValue, txt_fecha_inicio.SelectedDate, txt_fecha_fin.SelectedDate);
                    
 

                    gv_Foto.DataSource = dt_Foto;
                    gv_Foto.DataBind();

                    lblmensaje.Text = "Se encontro " + dt_Foto.Rows.Count + " resultados";
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
        protected void gv_Foto_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = gv_Foto.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                gv_Foto.DataSource = dataView;
                gv_Foto.DataBind();
            }
        }
        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            // GridHeaderItem header = "";

            //CheckBox cb_all = (CheckBox)gv_Foto.FindControl("cb_all");
            bool validar = cb_all.Checked;

            for (int i = 0; i < gv_Foto.Items.Count; i++)
            {
                GridItem item = gv_Foto.Items[i];
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

            for (int i = 0; i < gv_Foto.Items.Count; i++)
            {
                GridItem item = gv_Foto.Items[i];

                this.Session["foto_Chk"] = item;


                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");
                Label lbl_validar = (Label)item.FindControl("lbl_validar");
                Label lbl_Id_Reg = (Label)item.FindControl("lbl_Id_Rep");


                int id = Convert.ToInt32(lbl_Id_Reg.Text.Trim());
                bool validar = cb_validar.Checked;

                update_validado(id, validar);
                if (validar == true)
                {


                    lbl_validar.Text = "validado";
                    lbl_validar.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    lbl_validar.Text = "sin validar";
                    lbl_validar.ForeColor = System.Drawing.Color.Red;
                }

            }
            //cargarGrilla_Competencias();
        }
        protected void update_validado(int id, bool validar)
        {
            try
            {
                Conexion Ocoon = new Conexion();
                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_SPOT_VALIDADO", id, validar);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
        protected void gv_Foto_CancelCommand(object source, GridCommandEventArgs e)
        {

            try
            {
                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_EditCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                gv_Foto.CurrentPageIndex = e.NewPageIndex;
                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            try
            {
                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                lblmensaje.Text = "";
                Conexion Ocoon = new Conexion();

                GridItem item = gv_Foto.Items[e.Item.ItemIndex];

                Label lbl_Id_Reg_Fotogr = (Label)item.FindControl("lbl_Id_Reg_Fotogr");
                int iid_ReportFotog = Convert.ToInt32(lbl_Id_Reg_Fotogr.Text.Trim());

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");



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

                            if (editor is GridTextColumnEditor)
                            {
                                editorText = (editor as GridTextColumnEditor).Text;
                                editorValue = (editor as GridTextColumnEditor).Text;
                                ArrayEditorValue.Add(editorValue);
                            }
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

                string comentario = ArrayEditorValue[0].ToString();
                DateTime Fec_reg_bd = Convert.ToDateTime((ArrayEditorValue[2] as RadDateTimePicker).SelectedDate);

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_FOTOGRAFICO", cb_validar.Checked, comentario, iid_ReportFotog, Fec_reg_bd, Session["sUser"].ToString(), DateTime.Now);

                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                lblmensaje.Text = ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_DataBound(object sender, EventArgs e)
        {

            if (gv_Foto.Items.Count > 0)
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
        protected void buttonSubmit_Click(object sender, System.EventArgs e)
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

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REG_FOTO", iidregft, Id_repft, foto, Session["sUser"].ToString(), DateTime.Now);
                RadBinaryImage_fotoBig.Visible = false;
                cargarGrilla_ReporteFotografico();

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

        /// <summary>
        /// Verificar este metodo Ing. Carlo  Hernandez ---Preguntar a Ditmar Estrada
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void gv_Foto_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                panelEdit.DataBind();
                RadBinaryImage_fotoBig.DataBind();
                RadBinaryImage_fotoBig.Visible = false;
                //GridItem item = gv_Foto.Items[e.Item.ItemIndex];
                GridItem item = gv_Foto.Items[e.Item.ItemIndex];






                Label lbl_id_reg_foto = (Label)item.FindControl("lbl_id_reg_foto");

                Label lbl_Id_Reg_Fotogr = (Label)item.FindControl("lbl_Id_Reg_Fotogr");
                if (e.CommandName == "VERFOTO")
                {
                    Session["iidregft"] = Convert.ToInt32(lbl_id_reg_foto.Text);
                    Session["Id_repft"] = Convert.ToInt32(lbl_Id_Reg_Fotogr.Text);

                    DataTable dt = null;
                    Conexion Ocoon = new Conexion();
                    //string sidregft = ((LinkButton)sender).CommandArgument;
                    int iidregft = Convert.ToInt32(lbl_id_reg_foto.Text);

                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REG_FOTO", iidregft);
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
                    if (Int32.Equals(iidregft, 0))
                    {
                        Response.Redirect("/login.aspx");
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

                byte[] foto = (byte[])Session["array"];

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REG_FOTO", iidregft, Id_repft, foto, Session["sUser"].ToString(), DateTime.Now);
                RadBinaryImage_fotoBig.Visible = false;
                cargarGrilla_ReporteFotografico();

            }
            catch (Exception ex)
            {
                ex.ToString();
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
      

            if (dt.Rows.Count > 0)
            {
                ddlCampana.DataSource = dt;
                ddlCampana.DataValueField = "id_planning";
                ddlCampana.DataTextField = "Planning_Name";
                ddlCampana.DataBind();
                ddlCampana.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlCampana.Enabled = true;
            }

            llenatipoGrupoObjetivo();
            llenaCategoria();

            MopoReport.Show();
        }


        protected void llenaCategoria()
        {


            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            pais = this.Session["scountry"].ToString();


            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_Product_CategoryXCompany_id", compañia.ToString());
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

        protected void cargarCadena()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_NODECOMMERCIALXTIPO", "24");
            ddlCadena.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                ddlCadena.DataSource = dt;
                ddlCadena.DataValueField = "NodeCommercial";
                ddlCadena.DataTextField = "commercialNodeName";
                ddlCadena.DataBind();
                ddlCadena.Items.Insert(0, new ListItem("---Sleccionar---", "0"));
                ddlCadena.Enabled = true;

                cmbCadena.DataSource = dt;
                cmbCadena.DataValueField = "NodeCommercial";
                cmbCadena.DataTextField = "commercialNodeName";
                cmbCadena.DataBind();
                cmbCadena.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbCadena.Enabled = true;

            }



            


        }


        



        protected void ddlCampana_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenatipoActividad();



            MopoReport.Show();
        }





        void LimpiarControles()
        {
            ddlCanal.SelectedValue = "0";
            ddlCampana.Items.Clear();
            ddlCategoria.Items.Clear();
            ddlMarca.Items.Clear();
   


        }

        protected void btnGuardarReporteFotografico_Click(object sender, EventArgs e)
        {
     

           


                try
                {
                    if (ddlCanal.Text == "0" || ddlCampana.Text == "0" || ddlCadena.Text == "0"
        || ddlPDV.Text == "0" || ddlCategoria.Text == "0" || ddlFamilia.Text == "0"  || ddlSubfamilia.Text=="0"  || ddlTipoEmpresa.Text=="0" || ddlEmpresa.Text=="0"
                   || ddlMarca.Text=="0" || ddlTipoActividad.Text=="0" || ddlGrupoObjetivo.Text=="0"    
                        )
                    {
                        lblmensaje.Visible = true;
                        lblmensaje.Text = "No se pudo registrar faltan datos";
                        lblmensaje.ForeColor = System.Drawing.Color.Red;
                        return;
                    }


                    DataTable dt = null;
                    Conexion Ocoon = new Conexion();
                    string person=this.Session["personid"].ToString();

                    dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_REGISTRAR_REPORTE_SPOT", person, ddlCampana.SelectedValue, this.Session["companyid"].ToString(),ddlCadena.SelectedValue,ddlPDV.SelectedValue,ddlCategoria.SelectedValue
                        , DateTime.Now.Month,true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now
                        ,ddlFamilia.SelectedValue,ddlSubfamilia.SelectedValue, ddlTipoEmpresa.SelectedValue,ddlEmpresa.SelectedValue,ddlMarca.SelectedValue,ddlTipoActividad.SelectedValue
                        , ddlGrupoObjetivo.SelectedValue, ddlCanal.SelectedValue, rdtFechaini.SelectedDate, rdtFechafin.SelectedDate, txtMecanica.Text, Convert.ToDouble(txtPrecioNormal.Text), Convert.ToDouble(txtPrecioOferta.Text));


                    LimpiarControles();
                    lblmensaje.Visible = true;
                    lblmensaje.Text = "Fue registrado con exito el reporte Spot";
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

        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            MopoReport.Show();
        }

 
        protected void cargarPDV()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_POINTOFSALEXNODECOMMERCIAL", ddlCadena.SelectedValue);
            ddlPDV.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                ddlPDV.DataSource = dt;
                ddlPDV.DataValueField = "ClientPDV_Code";
                ddlPDV.DataTextField = "pdv_Name";
                ddlPDV.DataBind();
                ddlPDV.Items.Insert(0, new ListItem("---Todos---", "0"));
                ddlPDV.Enabled = true;
            }
        }

        protected void ddlCadena_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarPDV();
            MopoReport.Show();
        }

        protected void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFamilia();
        }

        protected void llenaMarca()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);


            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_MarcaXCompany1", ddlEmpresa.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                ddlMarca.DataSource = dt;
                ddlMarca.DataValueField = "id_Brand";
                ddlMarca.DataTextField = "Name_Brand";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("---Seleccionar---", "0"));
            }
        }
        protected void llenaSubfamilia()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();




            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_SubfamiliaXFamilia", ddlFamilia.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                ddlSubfamilia.DataSource = dt;
                ddlSubfamilia.DataValueField = "id_ProductSubFamily";
                ddlSubfamilia.DataTextField = "subfam_nombre";
                ddlSubfamilia.DataBind();
                ddlSubfamilia.Items.Insert(0, new ListItem("---Seleccionar---", "0"));
                ddlSubfamilia.Enabled = true;
            }
        }
        protected void llenaFamilia()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();



            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_Product_FamilyXCategoria", ddlCategoria.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                ddlFamilia.DataSource = dt;
                ddlFamilia.DataValueField = "id_ProductFamily";
                ddlFamilia.DataTextField = "name_Family";
                ddlFamilia.DataBind();
                ddlFamilia.Items.Insert(0, new ListItem("---Seleccionar---", "0"));
                ddlFamilia.Enabled = true;
            }
            else
            {
                ddlFamilia.Items.Clear();
                ddlFamilia.Enabled = false;
            }

            
        }

        protected void llenatipoActividad()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();



            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_TIPOACTIVIDADXPLANNING", ddlCampana.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                ddlTipoActividad.DataSource = dt;
                ddlTipoActividad.DataValueField = "id_Tipo_Act";
                ddlTipoActividad.DataTextField = "descripcion";
                ddlTipoActividad.DataBind();
                ddlTipoActividad.Items.Insert(0, new ListItem("---Seleccionar---", "0"));
                ddlTipoActividad.Enabled = true;
            }


        }

    protected void llenatipoGrupoObjetivo()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();



            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_GRUPOOBJETIVOXCANAL", ddlCanal.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                ddlGrupoObjetivo.DataSource = dt;
                ddlGrupoObjetivo.DataValueField = "id_TargetGroup";
                ddlGrupoObjetivo.DataTextField = "TargetGroup";
                ddlGrupoObjetivo.DataBind();
                ddlGrupoObjetivo.Items.Insert(0, new ListItem("---Seleccionar---", "0"));
                ddlGrupoObjetivo.Enabled = true;
            }


        }
        

      protected void llenaEmpresa()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();



            dt = Ocoon.ejecutarDataTable("UP_WEBSIGE_OPE_SELECCIONAR_CompanyXTipo_SF", ddlTipoEmpresa.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                ddlEmpresa.DataSource = dt;
                ddlEmpresa.DataValueField = "Company_id";
                ddlEmpresa.DataTextField = "Company_Name";
                ddlEmpresa.DataBind();
                ddlEmpresa.Items.Insert(0, new ListItem("---Seleccionar---", "0"));
                ddlEmpresa.Enabled = true;
            }

            
        }

        

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaFamilia();
            MopoReport.Show();
        }

        protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaSubfamilia();
            MopoReport.Show();
        }

        protected void ddlTipoEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaEmpresa();
            MopoReport.Show();

        }

        protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaMarca();
            MopoReport.Show();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if ((FileUpReport.PostedFile != null) && (FileUpReport.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(FileUpReport.PostedFile.FileName);
                string SaveLocation = Server.MapPath("masivo") + "\\" + fn;

                if (SaveLocation != string.Empty)
                {
                    if (FileUpReport.FileName.ToLower().EndsWith(".xls"))
                    {
                        // string Destino = Server.MapPath(null) + "\\PDV_Planning\\" + Path.GetFileName(FileUpPDV.PostedFile.FileName);
                        OleDbConnection oConn1 = new OleDbConnection();
                        OleDbCommand oCmd = new OleDbCommand();
                        OleDbDataAdapter oDa = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();
                        DataTable dt = new DataTable();

                        FileUpReport.PostedFile.SaveAs(SaveLocation);

                        // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                        oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                        oConn1.Open();
                        oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_ope_Reporte_Spot"];
                        oCmd.Connection = oConn1;
                        oDa.SelectCommand = oCmd;

                        try
                        {
                            //if (this.Session["scountry"].ToString() != null)
                            //{
                            oDa.Fill(oDs);

                            //DataTable dtdivpol = oCoon.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", this.Session["scountry"].ToString());
                            //ECountry oescountry = new ECountry();
                            //if (dtdivpol != null)
                            //{
                            //    if (dtdivpol.Rows.Count > 0)
                            //    {
                            //        oescountry.CountryDpto = Convert.ToBoolean(dtdivpol.Rows[0]["Country_Dpto"].ToString().Trim());
                            //        oescountry.CountryCiudad = Convert.ToBoolean(dtdivpol.Rows[0]["Country_Ciudad"].ToString().Trim());
                            //        oescountry.CountryDistrito = Convert.ToBoolean(dtdivpol.Rows[0]["Country_Distrito"].ToString().Trim());
                            //        oescountry.CountryBarrio = Convert.ToBoolean(dtdivpol.Rows[0]["Country_Barrio"].ToString().Trim());
                            //    }
                            //}

                            dt = oDs.Tables[0];
                            int numcol = 16; //determina el número de columnas para el datatable
                            if (dt.Columns.Count == numcol)
                            {

                                dt.Columns[0].ColumnName = "cod_mes";
                                dt.Columns[1].ColumnName = "cod_cadena";
                                dt.Columns[2].ColumnName = "cod_tienda";
                                dt.Columns[3].ColumnName = "cod_categoria";
                                dt.Columns[4].ColumnName = "cod_familia";
                                dt.Columns[5].ColumnName = "cod_variedad";
                                dt.Columns[6].ColumnName = "cod_competencia";
                                dt.Columns[7].ColumnName = "cod_empresa";
                                dt.Columns[8].ColumnName = "cod_marca";
                                dt.Columns[9].ColumnName = "cod_actividad";
                                dt.Columns[10].ColumnName = "cod_grupo_Objetivo";
                                dt.Columns[11].ColumnName = "INICIO";
                                dt.Columns[12].ColumnName = "TERMINO";
                                dt.Columns[13].ColumnName = "MECANICA";
                                dt.Columns[14].ColumnName = "NORMAL";
                                dt.Columns[15].ColumnName = "OFERTA";


                                

                                

                                

                                ConnectionStringSettings settingconection;
                                settingconection = ConfigurationManager.ConnectionStrings["ConectaDBLucky"];
                                string oSqlConnIN = settingconection.ConnectionString;

                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oSqlConnIN))
                                {
                                    bulkCopy.DestinationTableName = "OPE_REPORTE_SPOT_TMP";
                                    //carga los SKU's temporalmente para hacer el procedimiento a través de un SP
                                    bulkCopy.WriteToServer(dt);
                                }

                                string usuario = this.Session["sUser"].ToString().Trim();

                                //realiza las validaciones y carga los productos a planning.
                                Conexion cn = new Conexion();
                                //DataTable dtl = cn.ejecutarDataTable("UP_WEBXPLORA_OPE_CARGAMASIVA_OPE_REPORTE_SPOT_TMP",
                                //this.Session["personid"].ToString(),ddlCargaMasiva_Canal.SelectedValue ,ddlCargaMasiva_Campaña.SelectedValue,
                                // this.Session["companyid"].ToString(),
                                //usuario, DateTime.Now,
                                //usuario, DateTime.Now);

                                int cont = 0;

                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    string MES = dt.Rows[i]["cod_mes"].ToString();
                                    string CADENA = dt.Rows[i]["cod_cadena"].ToString();
                                    string TIENDAS = dt.Rows[i]["cod_tienda"].ToString();
                                    string categoria = dt.Rows[i]["cod_categoria"].ToString();
                                    string FAMILIA = dt.Rows[i]["cod_familia"].ToString();
                                    string VARIEDAD = dt.Rows[i]["cod_variedad"].ToString();
                                    string COMPETENCIA = dt.Rows[i]["cod_competencia"].ToString();
                                    string EMPRESA = dt.Rows[i]["cod_empresa"].ToString();
                                    string MARCA = dt.Rows[i]["cod_marca"].ToString();
                                    string ACTIVIDAD = dt.Rows[i]["cod_actividad"].ToString();
                                    string GRUPO_OBJETIVO = dt.Rows[i]["cod_grupo_Objetivo"].ToString();
                                    string INICIO = dt.Rows[i]["INICIO"].ToString();
                                    string TERMINO = dt.Rows[i]["TERMINO"].ToString();
                                    string MECANICA = dt.Rows[i]["MECANICA"].ToString();
                                    string NORMAL = dt.Rows[i]["NORMAL"].ToString();
                                    string OFERTA = dt.Rows[i]["OFERTA"].ToString();


                                    cn.ejecutarDataTable("UP_WEBSIGE_OPE_REGISTRAR_REPORTE_SPOT", this.Session["personid"].ToString(), ddlCargaMasiva_Campaña.SelectedValue, this.Session["companyid"].ToString(), CADENA, TIENDAS, categoria
                        , MES, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now
                        , FAMILIA, VARIEDAD, COMPETENCIA, EMPRESA, MARCA, ACTIVIDAD
                        , GRUPO_OBJETIVO, ddlCargaMasiva_Canal.SelectedValue, Convert.ToDateTime(INICIO), Convert.ToDateTime(TERMINO), MECANICA, Convert.ToDouble(NORMAL), Convert.ToDouble(OFERTA));

                                    cont = cont + 1;

                                }

                                lblmensaje.Visible = true;
                                lblmensaje.Text = "Fueron registrados " + cont + " datos con existo";
                            lblmensaje.ForeColor = System.Drawing.Color.Blue;
                            }

                           
                        }
                        catch
                        {

                            //lblencabezado.Text = "INFORMACION";
                            //lblmensajegeneral.Text = "Tiene que seleccionar un archivo";
                            //ImgMensaje.ImageUrl = "~/Pages/images/Mensajes/warning_blue.png";
                            //divMensaje.Style.Value = "border-width:10px;border-style:solid;border-color:#53A2FF; height:169px;background-color:#9FCBFF";
                            //ModalPopupMensaje.Show();


                        }
                    }
                }
            }
        }

        protected void btnMasiva_Click(object sender, EventArgs e)
        {
            mopoCargaMasiva.Show();
        }
        protected void CargarCanal_CargaMasiva()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            pais = this.Session["scountry"].ToString();


            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CHANNEL", pais, compañia);
            if (dt.Rows.Count > 0)
            {
                ddlCargaMasiva_Canal.DataSource = dt;
                ddlCargaMasiva_Canal.DataValueField = "cod_Channel";
                ddlCargaMasiva_Canal.DataTextField = "Channel_Name";
                ddlCargaMasiva_Canal.DataBind();
                ddlCargaMasiva_Canal.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }

        }

        protected void CargarCampaña_CargaMasiva()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            //tdcampaña.Visible = true;
            ddlCargaMasiva_Campaña.Visible = true;
            string sidchannel = ddlCargaMasiva_Canal.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);


            ddlCargaMasiva_Campaña.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                ddlCargaMasiva_Campaña.DataSource = dt;
                ddlCargaMasiva_Campaña.DataValueField = "id_planning";
                ddlCargaMasiva_Campaña.DataTextField = "Planning_Name";
                ddlCargaMasiva_Campaña.DataBind();
                ddlCargaMasiva_Campaña.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlCargaMasiva_Campaña.Enabled = true;
            }
        }

        protected void ddlCargaMasiva_Canal_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCampaña_CargaMasiva();
            mopoCargaMasiva.Show();
        }

    }
}