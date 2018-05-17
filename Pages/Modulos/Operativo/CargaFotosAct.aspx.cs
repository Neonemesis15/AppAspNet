//using System;
//using System.Collections;
//using System.Configuration;
//using System.Data;
//using System.Linq;
//using System.IO;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
//using Lucky.Data;
//using Lucky.Data.Common.Application;
//using Lucky.Entity.Common.Application;
//using Lucky.Entity.Common.Application.Interfaces;
//using Lucky.Business;
//using Lucky.Business.Common.Application;

//namespace SIGE.Pages.Modulos.Operativo
//{
//    public partial class CargaFotosAct : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                //controla las sesiones de usuario 
//                try
//                {
//                    string sUser = this.Session["sUser"].ToString();
//                    string sPassw = this.Session["sPassw"].ToString();
//                    if (sUser != null && sPassw != null)
//                    {
//                        try
//                        {
//                            llena_ConsultarACTPropia();
//                        }
//                        catch (Exception ex)
//                        {
//                        }
//                        LblNamePlanning.Text = Convert.ToString(this.Session["namePlanning"]);
//                        InActivarControles(this);
//                    }
//                }
//                catch (Exception ex)
//                {                    
//                       this.Session.Abandon();
//                    Response.Write("<script>window.close();</script>");
//                    Alertas.CssClass = "MensajesSupervisor";
//                    LblAlert.Text = "Sessión Caducada";
//                    LblFaltantes.Text = "Sr. Usuario, la sesión ha expirado. Por favor cierre la aplicación e ingrese nuevamente";
//                    PopupMensajes();
//                }
//            }
//        }

//        #region Zona de Declaración de Variables Generales

//        int consecutivo;
//        bool StatusFotosActPropia;        
//        DateTime FechaFoto;
//        DateTime FechaIniPlanning;
//        Conexion oConn = new Lucky.Data.Conexion();
//        Facade_Proceso_Operativo.Facade_Proceso_Operativo Get_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
//        Facade_Proceso_Planning.Facade_Proceso_Planning Get_Planning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
//        private Photographs_Service oPhotographs_Service = new Photographs_Service();

//        #endregion

//        #region Funciones comunes
//        //Limpiar controles
//        private void limpiarControles(Control parent)
//        {
//            TextBox t;
//            DropDownList d;

//            foreach (Control Txt in parent.Controls)
//            {
//                t = Txt as TextBox;
//                if (t != null)
//                {
//                    t.Text = "";
//                }
//                if (Txt.Controls.Count > 0)
//                {
//                    limpiarControles(Txt);
//                }
//            }
//            foreach (Control Cmb in parent.Controls)
//            {
//                d = Cmb as DropDownList;
//                if (d != null)
//                {
//                    try
//                    {
//                        d.Text = "0";
//                    }
//                    catch
//                    {
//                    }
//                }
//                if (Cmb.Controls.Count > 0)
//                {
//                    limpiarControles(Cmb);
//                }
//            }

//            GVFotografiasAct.DataBind();
//        }
//        //Activar controles
//        private void ActivarControles(Control parent)
//        {
//            TextBox t;
//            DropDownList d;

//            foreach (Control Txt in parent.Controls)
//            {
//                t = Txt as TextBox;
//                if (t != null)
//                {
//                    t.Enabled = true;
//                }
//                if (Txt.Controls.Count > 0)
//                {
//                    ActivarControles(Txt);
//                }
//            }
//            foreach (Control Cmb in parent.Controls)
//            {
//                d = Cmb as DropDownList;
//                if (d != null)
//                {
//                    try
//                    {
//                        d.Enabled = true;
//                    }
//                    catch
//                    {
//                    }
//                }
//                if (Cmb.Controls.Count > 0)
//                {
//                    ActivarControles(Cmb);
//                }
//            }
//            ImgFotoCalendar.Enabled = true;
//            FileUpFotosAct.Enabled = true;


//        }
//        //Inactivar controles
//        private void InActivarControles(Control parent)
//        {
//            TextBox t;
//            DropDownList d;

//            foreach (Control Txt in parent.Controls)
//            {
//                t = Txt as TextBox;
//                if (t != null)
//                {
//                    t.Enabled = false;
//                }
//                if (Txt.Controls.Count > 0)
//                {
//                    InActivarControles(Txt);
//                }
//            }
//            foreach (Control Cmb in parent.Controls)
//            {
//                d = Cmb as DropDownList;
//                if (d != null)
//                {
//                    try
//                    {
//                        d.Enabled = false;
//                    }
//                    catch
//                    {
//                    }
//                }
//                if (Cmb.Controls.Count > 0)
//                {
//                    InActivarControles(Cmb);
//                }
//            }
//            ImgFotoCalendar.Enabled = false;
//            FileUpFotosAct.Enabled = false;



//        }
//        //Llenar objetos 
//        private void llena_PDV()
//        {
//            try
//            {
//                DataTable dt = Get_Planning.Get_ObtenerPDVPlanning(Convert.ToInt32(this.Session["id_Planning"].ToString().Trim()));
//                if (dt != null)
//                {
//                    if (dt.Rows.Count - 1 > 0)
//                    {
//                        CmbPDVPlanning.DataSource = dt;
//                        CmbPDVPlanning.DataValueField = "id_MPOSPlanning";
//                        CmbPDVPlanning.DataTextField = "pdv_Name";
//                        CmbPDVPlanning.DataBind();
//                    }
//                    else
//                    {
//                        Alertas.CssClass = "MensajesSupervisor";
//                        LblAlert.Text = "Puntos de venta:";
//                        LblFaltantes.Text = "Sr. Usuario, el planning seleccionado no tiene puntos de venta creados. No es posible crear informe fotográfico";
//                        InActivarControles(this);
//                        PopupMensajes();
//                        return;
//                    }
//                }
//                dt = null;
//            }
//            catch
//            {
//                this.Session.Abandon();
//                Alertas.CssClass = "MensajesSupervisor";
//                LblAlert.Text = "Sessión Caducada";
//                LblFaltantes.Text = "Sr. Usuario, la sesión ha expirado. Por favor cierre la aplicación e ingrese nuevamente";
//                PopupMensajes();
//            }
//        }
//        private void llena_Category()
//        {
//            try
//            {
//                int prueba = Convert.ToInt32(this.Session["id_Planning"].ToString().Trim());
//                DataTable dt = Get_Operativo.Get_SearchcategoryProductPlanning(prueba);
//                if (dt != null)
//                {
//                    if (dt.Rows.Count - 1 > 0)
//                    {
//                        CmbCatgProd.DataSource = dt;
//                        CmbCatgProd.DataValueField = "id_ProductCategory";
//                        CmbCatgProd.DataTextField = "Product_Category";
//                        CmbCatgProd.DataBind();
//                    }
//                    else
//                    {
//                        Alertas.CssClass = "MensajesSupervisor";
//                        LblAlert.Text = "Puntos de venta:";
//                        LblFaltantes.Text = "Sr. Usuario, el planning seleccionado no tiene categorías de producto. No es posible crear informe fotográfico";
//                        InActivarControles(this);
//                        PopupMensajes();
//                        return;
//                    }
//                }
//                dt = null;
//            }
//            catch
//            {
//                this.Session.Abandon();
//                Alertas.CssClass = "MensajesSupervisor";
//                LblAlert.Text = "Sessión Caducada";
//                LblFaltantes.Text = "Sr. Usuario, la sesión ha expirado. Por favor cierre la aplicación e ingrese nuevamente";
//                PopupMensajes();
//            }
//        }
//        private void llena_ConsultarACTPropia()
//        {            
//                int planning = Convert.ToInt32(this.Session["id_Planning"]);
//                DataTable dt = Get_Operativo.Get_SearchPhotographs_Service(planning);
//                GVConsultaActPropia.DataSource = dt;
//                GVConsultaActPropia.DataBind();
           
//        }
//        //Ventanas de mensaje de usuario
//        private void PopupMensajes()
//        {
//            ModalPopupAlertas.Show();
//        }
//        #endregion


//        //-- Description: validación de fechas
//        protected void TxtFechaFoto_TextChanged(object sender, EventArgs e)
//        {
//            try
//            {
//                if (TxtFechaFoto.Text != "")
//                {
//                    FechaFoto = Convert.ToDateTime(TxtFechaFoto.Text.ToString());
//                    FechaIniPlanning = Convert.ToDateTime(this.Session["fechainiplanning"]);
//                    if (Convert.ToDateTime(TxtFechaFoto.Text.ToString()) > DateTime.Today || Convert.ToDateTime(TxtFechaFoto.Text.ToString()) < FechaIniPlanning)
//                    {
//                        TxtFechaFoto.Focus();
//                        TxtFechaFoto.Text = "";
//                        Alertas.CssClass = "MensajesSupervisor";
//                        LblAlert.Text = "Parametros Incorrectos";
//                        LblFaltantes.Text = "Sr. Usuario, la fecha no puede ser mayor a la fecha actual ni menor a la fecha inicial del planning. Por favor verifiquelo";
//                        PopupMensajes();
//                        return;
//                    }
//                    else
//                    {
//                        CmbPDVPlanning.Focus();
//                    }
//                }
//                else
//                {
//                    TxtFechaFoto.Focus();
//                    TxtFechaFoto.Text = "";
//                    Alertas.CssClass = "MensajesSupervisor";
//                    LblAlert.Text = "Parámetros obligatorio (*)";
//                    LblFaltantes.Text = "Fecha de fotografía.";
//                    PopupMensajes();
//                    return;
//                }
//            }
//            catch
//            {
//                TxtFechaFoto.Focus();
//                TxtFechaFoto.Text = "";
//                Alertas.CssClass = "MensajesSupervisor";
//                LblAlert.Text = "Parametros Incorrectos";
//                LblFaltantes.Text = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
//                PopupMensajes();
//                return;
//            }
//        }

//        //-- Description:       <Permite Crear un Nuevo registro fotográfico de  actividades propias Ing. Mauricio Ortiz>
//        //-- Requerimiento No.  <>
//        //-- =============================================
//        protected void IbtnCrearInfFoto_Click(object sender, ImageClickEventArgs e)
//        {
//            TxtFechaFoto.Focus();
//            IbtnCrearInfFoto.Visible = false;
//            IbtnSaveInfFoto.Visible = true;
//            IbtnSearchInfFoto.Visible = false;
//            IbtnEditInfFoto.Visible = false;
//            IbtnActualizaInfFoto.Visible = false;
//            limpiarControles(this);
//            ActivarControles(this);
//            llena_PDV();
//            llena_Category();
//            LblTitCargarArchivo.Text = "Cargar Fotografías";
//        }
//        protected void IbtnSaveInfFoto_Click(object sender, ImageClickEventArgs e)
//        {
//            try
//            {
//                if (TxtFechaFoto.Text != "")
//                {
//                    FechaFoto = Convert.ToDateTime(TxtFechaFoto.Text.ToString());
//                }
//            }
//            catch
//            {
//                Alertas.CssClass = "MensajesSupervisor";
//                LblAlert.Text = "Parametros Incorrectos";
//                LblFaltantes.Text = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
//                PopupMensajes();
//                return;
//            }

//            if (TxtFechaFoto.Text == "" || CmbPDVPlanning.Text == "" || CmbPDVPlanning.Text == "0" || CmbCatgProd.Text == "" ||
//                CmbCatgProd.Text == "0")
//            {
//                Alertas.CssClass = "MensajesSupervisor";
//                LblAlert.Text = "Parámetros obligatorio (*)";
//                LblFaltantes.Text = "Todos los campos marcados con (*) deben estar digitados y debe haber seleccionado una fotografía. Por favor verifique";
//                PopupMensajes();
//                return;
//            }
//            else
//            {
//                consecutivo = Convert.ToInt32(this.Session["CONSECUTIVO"]) + 1;
//                this.Session["CONSECUTIVO"] = consecutivo;
//                if ((FileUpFotosAct.PostedFile != null) && (FileUpFotosAct.PostedFile.ContentLength > 0))
//                {
//                    string fn = System.IO.Path.GetFileName(FileUpFotosAct.PostedFile.FileName);
//                    string SaveLocation = Server.MapPath("PictureActividad") + "\\" + fn;
//                    try
//                    {
//                        if (SaveLocation != string.Empty)
//                        {
//                            if (FileUpFotosAct.FileName.ToLower().EndsWith(".jpg") || FileUpFotosAct.FileName.ToLower().EndsWith(".png"))
//                            {
//                                int fileSize = FileUpFotosAct.PostedFile.ContentLength;
//                                // Allow only files less than 1.050.000 bytes (approximately 1 MB) to be uploaded.
//                                if (fileSize < 1050000)
//                                {
//                                    FechaFoto = Convert.ToDateTime(TxtFechaFoto.Text.ToString());
//                                    FileUpFotosAct.PostedFile.SaveAs(MapPath("../../Modulos/Operativo/PictureActividad/" + FileUpFotosAct.FileName));
//                                    string sPhotoPathName = FileUpFotosAct.FileName;
//                                    EPhotographs_Service FotoActividadPropia = oPhotographs_Service.RegistrarFotosActividadPropia(this.Session["id_Planning"].ToString().Trim(), Convert.ToInt32(CmbPDVPlanning.SelectedValue), FechaFoto, CmbCatgProd.SelectedValue, "1",
//                                        sPhotoPathName, TxtObsActPropia.Text, Convert.ToInt32(this.Session["Company_id"]), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
//                                    DataTable dt = Get_Operativo.Get_idPhotographs_Service(Convert.ToString(this.Session["sUser"]));
//                                    DataTable dt1 = Get_Operativo.Get_FotosPhotographs_Service(Convert.ToInt32(dt.Rows[0]["id_Photographs"].ToString().Trim()));
//                                    GVFotografiasAct.DataSource = dt1;
//                                    GVFotografiasAct.DataBind();
//                                    if (dt1 != null)
//                                    {
//                                        if (dt1.Rows.Count > 0)
//                                        {
//                                            for (int j = 0; j <= GVFotografiasAct.Rows.Count - 1; j++)
//                                            {
//                                                string filename = System.IO.Path.GetFileName(dt1.Rows[j]["Photo_Directory"].ToString().Trim());
//                                                ((CheckBox)GVFotografiasAct.Rows[j].Cells[0].FindControl("CheckBox1")).Checked = Convert.ToBoolean(dt1.Rows[j]["Photo_Status"].ToString().Trim());
//                                                ((Image)GVFotografiasAct.Rows[j].Cells[0].FindControl("Image1")).ImageUrl = "~/Pages/Modulos/Operativo/PictureActividad/" + filename;
//                                            }
//                                        }
//                                    }
//                                    IbtnCrearInfFoto.Visible = true;
//                                    IbtnSaveInfFoto.Visible = false;
//                                    IbtnSearchInfFoto.Visible = true;
//                                    IbtnEditInfFoto.Visible = false;
//                                    IbtnActualizaInfFoto.Visible = false;
//                                    IbtnCancelInfFoto.Visible = true;
//                                    InActivarControles(this);
//                                    llena_ConsultarACTPropia();
//                                    FileUpFotosAct.Enabled = false;
//                                    GVFotografiasAct.Enabled = false;
//                                    Alertas.CssClass = "MensajesSupConfirm";
//                                    LblAlert.Text = "Informe Fotográfico";
//                                    LblFaltantes.Text = "Sr. Usuario, el registro se ha guardado con éxito";
//                                    PopupMensajes();
//                                }
//                                else
//                                {
//                                    Alertas.CssClass = "MensajesformatoFotos";
//                                    LblFaltantes.Text = "Sr. Usuario, verifique que la fotografía no exceda el tamaño máximo permitido ";
//                                    LblAlert.Text = "Tamaño máximo 2 MB";
//                                    PopupMensajes();
//                                    return;
//                                }
//                            }
//                            else
//                            {
//                                Alertas.CssClass = "MensajesformatoFotos";
//                                LblFaltantes.Text = "Sr. Usuario, el formato de la imagen que desea cargar no esta permitido por la aplicación. Intentelo nuevamente";
//                                LblAlert.Text = "Formatos validos *.jpg *.png";
//                                PopupMensajes();
//                                return;
//                            }
//                        }
//                    }

//                    catch (Exception ioex)
//                    {
//                        this.Session.Abandon();
//                        Alertas.CssClass = "MensajesSupervisor";
//                        LblAlert.Text = "Sessión Caducada";
//                        LblFaltantes.Text = "Sr. Usuario, la sesión ha expirado. Por favor cierre la aplicación e ingrese nuevamente";
//                        PopupMensajes();
//                    }

//                }
//                else
//                {
//                    Alertas.CssClass = "MensajesformatoFotos";
//                    LblFaltantes.Text = "Sr. Usuario, debe seleccionar alguna fotografia para cargar";
//                    LblAlert.Text = "Formatos validos *.jpg *.png";
//                    PopupMensajes();
//                    return;
//                }
//            }
//        }

//        //-- Description:       <Permite consultar un registro fotográfico de actividades propias Ing. Mauricio Ortiz>
//        //-- Requerimiento No.  <>
//        //-- =============================================
//        protected void GVConsultaActPropia_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            DataTable dt = Get_Operativo.Get_SearchInfoPhotographs_Service((Convert.ToInt32(GVConsultaActPropia.SelectedRow.Cells[1].Text)));
//            if (dt != null)
//            {
//                if (dt.Rows.Count > 0)
//                {
//                    this.Session["id_Photographs"] = dt.Rows[0]["id_Photographs"].ToString().Trim();
//                    TxtFechaFoto.Text = dt.Rows[0]["Photo_Date"].ToString().Trim();
//                    llena_PDV();
//                    CmbPDVPlanning.Text = dt.Rows[0]["id_MPOSPlanning"].ToString().Trim();
//                    llena_Category();
//                    CmbCatgProd.Text = dt.Rows[0]["id_ProductCategory"].ToString().Trim();
//                    TxtObsActPropia.Text = dt.Rows[0]["Photo_Comment_Observa"].ToString().Trim();
//                    if (Convert.ToBoolean(dt.Rows[0]["Photo_Status"].ToString().Trim()) == true)
//                    {
//                        RbtnStatusActPropia.Items[0].Selected = true;
//                        RbtnStatusActPropia.Items[1].Selected = false;
//                    }
//                    else
//                    {
//                        RbtnStatusActPropia.Items[0].Selected = false;
//                        RbtnStatusActPropia.Items[1].Selected = true;
//                    }


//                    DataTable dt2 = Get_Operativo.Get_FotosPhotographs_Service(Convert.ToInt32(this.Session["id_Photographs"].ToString().Trim()));
//                    GVFotografiasAct.DataSource = dt2;
//                    GVFotografiasAct.DataBind();

//                    if (dt2 != null)
//                    {
//                        if (dt2.Rows.Count > 0)
//                        {
//                            for (int j = 0; j <= GVFotografiasAct.Rows.Count - 1; j++)
//                            {
//                                string fn = System.IO.Path.GetFileName(dt2.Rows[j]["Photo_Directory"].ToString().Trim());
//                                ((CheckBox)GVFotografiasAct.Rows[j].Cells[0].FindControl("CheckBox1")).Checked = true;
//                                ((Image)GVFotografiasAct.Rows[j].Cells[0].FindControl("Image1")).ImageUrl = "~/Pages/Modulos/Operativo/PictureActividad/" + fn;
//                                this.Session["fn"] = fn;
//                            }
//                        }
//                    }
//                    dt2 = null;
//                }
//            }
//            dt = null;
//            InActivarControles(this);
//            IbtnCrearInfFoto.Visible = false;
//            IbtnSaveInfFoto.Visible = false;
//            IbtnSearchInfFoto.Visible = true;
//            IbtnEditInfFoto.Visible = true;
//            IbtnActualizaInfFoto.Visible = false;
//            GVFotografiasAct.Enabled = false;
//        }

//        //-- Description: Permite navegar entre paginas en la grilla
//        protected void GVConsultaActPropia_PageIndexChanging(object sender, GridViewPageEventArgs e)
//        {
//            GVConsultaActPropia.PageIndex = e.NewPageIndex;
//            llena_ConsultarACTPropia();
//            ModalPopupBuscarActPropia.Show();
//        }

//        //-- Description:       <Permite actualizar un registro fotográfico de  actividades propias Ing. Mauricio Ortiz>
//        //-- Requerimiento No.  <>
//        //-- =============================================
//        protected void IbtnEditInfFoto_Click(object sender, ImageClickEventArgs e)
//        {
//            IbtnEditInfFoto.Visible = false;
//            IbtnActualizaInfFoto.Visible = true;
//            ActivarControles(this);
//            llena_ConsultarACTPropia();
//            TxtFechaFoto.Focus();
//            GVFotografiasAct.Enabled = true;
//            LblTitCargarArchivo.Text = "Cambiar Fotografía";
//            FileUpFotosAct.Enabled = false;
//            RbtnStatusActPropia.Enabled = true;
//        }
//        protected void IbtnActualizaInfFoto_Click(object sender, ImageClickEventArgs e)
//        {
//            try
//            {
//                if (TxtFechaFoto.Text != "")
//                {
//                    FechaFoto = Convert.ToDateTime(TxtFechaFoto.Text.ToString());
//                }
//            }
//            catch
//            {
//                Alertas.CssClass = "MensajesSupervisor";
//                LblAlert.Text = "Parametros Incorrectos";
//                LblFaltantes.Text = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
//                PopupMensajes();
//                return;
//            }
//            if (RbtnStatusActPropia.Items[0].Selected == true)
//            {
//                StatusFotosActPropia = true;
//            }
//            else
//            {
//                StatusFotosActPropia = false;
//            }

//            if (TxtFechaFoto.Text == "" || CmbPDVPlanning.Text == "" || CmbPDVPlanning.Text == "0" || CmbCatgProd.Text == "" ||
//                CmbCatgProd.Text == "0")
//            {
//                Alertas.CssClass = "MensajesSupervisor";
//                LblAlert.Text = "Parámetros obligatorio (*)";
//                LblFaltantes.Text = "Todos los campos marcados con (*) deben estar digitados. Por favor verifique";
//                PopupMensajes();
//                return;
//            }
//            else
//            {

//                if ((FileUpFotosAct.PostedFile != null))
//                {

//                    if ((FileUpFotosAct.PostedFile != null) && (FileUpFotosAct.PostedFile.ContentLength > 0))
//                    {
//                        string fn = System.IO.Path.GetFileName(FileUpFotosAct.PostedFile.FileName);
//                        string SaveLocation = Server.MapPath("PictureActividad") + "\\" + fn;
//                        try
//                        {
//                            if (SaveLocation != string.Empty)
//                            {
//                                if (FileUpFotosAct.FileName.EndsWith(".jpg") || FileUpFotosAct.FileName.EndsWith(".png") || FileUpFotosAct.FileName.EndsWith(".JPG") || FileUpFotosAct.FileName.EndsWith(".PNG"))
//                                {

//                                    FechaFoto = Convert.ToDateTime(TxtFechaFoto.Text.ToString());
//                                    FileUpFotosAct.PostedFile.SaveAs(MapPath("../../Modulos/Operativo/PictureActividad/" + FileUpFotosAct.FileName));
//                                    string sPhotoPathName = FileUpFotosAct.FileName;
//                                    File.Delete(MapPath("../../Modulos/Operativo/PictureActividad/" + this.Session["fn1"].ToString().Trim()));

//                                    EPhotographs_Service FotoActividadPropia = oPhotographs_Service.ActualizarFotosActividadPropia(Convert.ToInt32(this.Session["id_Photographs"].ToString().Trim()), Convert.ToInt32(CmbPDVPlanning.SelectedValue), FechaFoto, CmbCatgProd.SelectedValue,
//                                        sPhotoPathName, TxtObsActPropia.Text, StatusFotosActPropia, Convert.ToString(this.Session["sUser"]), DateTime.Now);
//                                    DataTable dt1 = Get_Operativo.Get_FotosPhotographs_Service(Convert.ToInt32(this.Session["id_Photographs"].ToString().Trim()));
//                                    GVFotografiasAct.DataSource = dt1;
//                                    GVFotografiasAct.DataBind();
//                                    if (dt1 != null)
//                                    {
//                                        if (dt1.Rows.Count > 0)
//                                        {
//                                            for (int j = 0; j <= GVFotografiasAct.Rows.Count - 1; j++)
//                                            {
//                                                string filename = System.IO.Path.GetFileName(dt1.Rows[j]["Photo_Directory"].ToString().Trim());
//                                                ((CheckBox)GVFotografiasAct.Rows[j].Cells[0].FindControl("CheckBox1")).Checked = true;
//                                                ((Image)GVFotografiasAct.Rows[j].Cells[0].FindControl("Image1")).ImageUrl = "~/Pages/Modulos/Operativo/PictureActividad/" + filename;
//                                            }
//                                        }
//                                    }
//                                    llena_ConsultarACTPropia();
//                                    InActivarControles(this);
//                                    RbtnStatusActPropia.Enabled = false;
//                                    IbtnCrearInfFoto.Visible = true;
//                                    IbtnSaveInfFoto.Visible = false;
//                                    IbtnSearchInfFoto.Visible = true;
//                                    IbtnEditInfFoto.Visible = false;
//                                    IbtnActualizaInfFoto.Visible = false;
//                                    IbtnCancelInfFoto.Visible = true;
//                                    FileUpFotosAct.Enabled = false;
//                                    GVFotografiasAct.Enabled = false;
//                                    Alertas.CssClass = "MensajesSupConfirm";
//                                    LblAlert.Text = "Informe Fotográfico";
//                                    LblFaltantes.Text = "Sr. Usuario, el registro se ha actualizado con éxito";
//                                    PopupMensajes();
//                                }
//                                else
//                                {
//                                    Alertas.CssClass = "MensajesformatoFotos";
//                                    LblFaltantes.Text = "Sr. Usuario, el formato de la imagen que desea cargar no esta permitido por la aplicación. Intentelo nuevamente";
//                                    LblAlert.Text = "Formatos validos *.jpg *.png";
//                                    PopupMensajes();
//                                    return;
//                                }
//                            }
//                        }

//                        catch (Exception ioex)
//                        {
//                            this.Session.Abandon();
//                            Alertas.CssClass = "MensajesSupervisor";
//                            LblAlert.Text = "Sessión Caducada";
//                            LblFaltantes.Text = "Sr. Usuario, la sesión ha expirado. Por favor cierre la aplicación e ingrese nuevamente";
//                            PopupMensajes();
//                        }

//                    }
//                    else
//                    {
//                        Alertas.CssClass = "MensajesformatoFotos";
//                        LblFaltantes.Text = "Sr. Usuario, debe seleccionar alguna fotografia para cargar";
//                        LblAlert.Text = "Formatos validos *.jpg *.png";
//                        PopupMensajes();
//                        return;
//                    }
//                }
//                else
//                {
//                    try
//                    {
//                        FechaFoto = Convert.ToDateTime(TxtFechaFoto.Text.ToString());
//                        DataTable dt1 = Get_Operativo.Get_FotosPhotographs_Service(Convert.ToInt32(this.Session["id_Photographs"].ToString().Trim()));
//                        GVFotografiasAct.DataSource = dt1;
//                        GVFotografiasAct.DataBind();
//                        if (dt1 != null)
//                        {
//                            if (dt1.Rows.Count > 0)
//                            {
//                                for (int j = 0; j <= GVFotografiasAct.Rows.Count - 1; j++)
//                                {
//                                    string filename = System.IO.Path.GetFileName(dt1.Rows[j]["Photo_Directory"].ToString().Trim());
//                                    ((CheckBox)GVFotografiasAct.Rows[j].Cells[0].FindControl("CheckBox1")).Checked = true;
//                                    ((Image)GVFotografiasAct.Rows[j].Cells[0].FindControl("Image1")).ImageUrl = "~/Pages/Modulos/Operativo/PictureActividad/" + filename;
//                                    this.Session["fotografia"] = filename;
//                                }
//                            }
//                        }

//                        string sPhoto = this.Session["fotografia"].ToString().Trim();
//                        EPhotographs_Service FotoActividadPropia = oPhotographs_Service.ActualizarFotosActividadPropia(Convert.ToInt32(this.Session["id_Photographs"].ToString().Trim()), Convert.ToInt32(CmbPDVPlanning.SelectedValue), FechaFoto, CmbCatgProd.SelectedValue,
//                            sPhoto, TxtObsActPropia.Text, StatusFotosActPropia, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                         
//                        llena_ConsultarACTPropia();
//                        InActivarControles(this);
//                        RbtnStatusActPropia.Enabled = false;
//                        IbtnCrearInfFoto.Visible = true;
//                        IbtnSaveInfFoto.Visible = false;
//                        IbtnSearchInfFoto.Visible = true;
//                        IbtnEditInfFoto.Visible = false;
//                        IbtnActualizaInfFoto.Visible = false;
//                        IbtnCancelInfFoto.Visible = true;
//                        FileUpFotosAct.Enabled = false;
//                        GVFotografiasAct.Enabled = false;
//                        Alertas.CssClass = "MensajesSupConfirm";
//                        LblAlert.Text = "Informe Fotográfico";
//                        LblFaltantes.Text = "Sr. Usuario, el registro se ha actualizado con éxito";
//                        PopupMensajes();
//                    }
//                    catch (Exception ioex)
//                    {
//                        this.Session.Abandon();
//                        Alertas.CssClass = "MensajesSupervisor";
//                        LblAlert.Text = "Sessión Caducada";
//                        LblFaltantes.Text = "Sr. Usuario, la sesión ha expirado. Por favor cierre la aplicación e ingrese nuevamente";
//                        PopupMensajes();
//                    }
//                }
//            }
//        }
//        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
//        {
//            if (((CheckBox)GVFotografiasAct.Rows[0].Cells[0].FindControl("CheckBox1")).Checked == true)
//            {
//                FileUpFotosAct.Enabled = false;
//                this.Session["fn1"] = "";
//                string fn1 = this.Session["fn1"].ToString().Trim();
//            }
//            else
//            {
//                FileUpFotosAct.Enabled = true;
//                this.Session["fn1"] = this.Session["fn"];
//                string fn1 = this.Session["fn"].ToString().Trim();
            
//                Alertas.CssClass = "MensajesSupConfirm";
//                LblAlert.Text = "Cambio de Fotografía";
//                LblFaltantes.Text = "Sr. Usuario, usted ha decidido cambiar la fotografía. ";                
//                PopupMensajes();
//                return;                
//            }
//        }

//        //-- Description:       <Permite Cancelar acciones del maestro>
//        protected void IbtnCancelInfFoto_Click(object sender, ImageClickEventArgs e)
//        {
//            IbtnCrearInfFoto.Visible = true;
//            IbtnSaveInfFoto.Visible = false;
//            IbtnSearchInfFoto.Visible = true;
//            IbtnEditInfFoto.Visible = false;
//            IbtnActualizaInfFoto.Visible = false;
//            IbtnCancelInfFoto.Visible = true;
//            InActivarControles(this);
//            limpiarControles(this);
//            RbtnStatusActPropia.Enabled = false;
//            RbtnStatusActPropia.Items[0].Selected = true;
//            RbtnStatusActPropia.Items[1].Selected = false;           
//        }
//    }
//}

