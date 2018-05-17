using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Application.Interfaces;
using Lucky.Business;
using Lucky.Business.Common.Application;


namespace SIGE.Pages.Modulos.Operativo
{
    public partial class CargaFotosCom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //controla las sesiones de usuario 
                try
                {
                    string sUser = this.Session["sUser"].ToString();
                    string sPassw = this.Session["sPassw"].ToString();
                    if (sUser != null && sPassw != null)
                    {
                                     
                    }
                }
                catch (Exception ex)
                {
                    this.Session.Abandon();
                    string errmensajeseccion = Convert.ToString(ex);
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }


            }
        }

        #region Zona de Declaración de Variables Generales      
        
        int consecutivo;
        private Competition__Information oCompetition__Information = new Competition__Information();
        Facade_Proceso_Operativo.Facade_Proceso_Operativo Get_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Conexion oConn = new Lucky.Data.Conexion();
        
        #endregion

        private void PopupMensajes()
        {
            ModalPopupAlertas.Show();
        }

        protected void BtnCargarFotos_Click(object sender, EventArgs e)
        {
            if (this.Session["nuevoreg"].ToString().Trim() == "1")
            {
                consecutivo = Convert.ToInt32(this.Session["CONSECUTIVO"]) + 1;
                this.Session["CONSECUTIVO"] = consecutivo;
                if ((FileUpFotosCom.PostedFile != null) && (FileUpFotosCom.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpFotosCom.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("PictureComercio") + "\\" + fn;
                    try
                    {
                        if (SaveLocation != string.Empty)
                        {
                            if (FileUpFotosCom.FileName.EndsWith(".jpg") || FileUpFotosCom.FileName.EndsWith(".png") || FileUpFotosCom.FileName.EndsWith(".JPG") || FileUpFotosCom.FileName.EndsWith(".PNG"))
                            {
                                DataTable dt = Get_Operativo.Get_idCompetition_Information(Convert.ToString(this.Session["sUser"]));
                                int cinfo = Convert.ToInt32(dt.Rows[0]["id_cinfo"].ToString().Trim());
                                FileUpFotosCom.PostedFile.SaveAs(MapPath("../../Modulos/Operativo/PictureComercio/" + FileUpFotosCom.FileName));
                                string sPhotoPathName = FileUpFotosCom.FileName;
                                ECompetition__Information ActividadCom = oCompetition__Information.RegistrarPhotoActividadCom(Convert.ToInt32(dt.Rows[0]["id_cinfo"].ToString().Trim()), sPhotoPathName, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                Alertas.CssClass = "MensajesSupConfirm";
                                LblFaltantes.Text = "Sr. Usuario.";
                                LblAlert.Text = "La fotografía ha sido cargada con exito";
                                PopupMensajes();
                                return;
                            }
                            else
                            {
                                Alertas.CssClass = "MensajesformatoFotos";
                                LblFaltantes.Text = "Sr. Usuario, el formato de la imagen que desea cargar no esta permitido por la aplicación. Intentelo nuevamente";
                                LblAlert.Text = "Formatos validos *.jpg *.png";
                                PopupMensajes();
                                return;
                            }
                        }
                    }

                    catch (Exception ioex)
                    {

                    }

                }
                else
                {
                    Alertas.CssClass = "MensajesformatoFotos";
                    LblFaltantes.Text = "Sr. Usuario, debe seleccionar alguna fotografia para cargar";
                    LblAlert.Text = "Formatos validos *.jpg *.png";
                    PopupMensajes();
                    return;
                }
            }
            else
            {
                consecutivo = Convert.ToInt32(this.Session["CONSECUTIVO"]) + 1;
                this.Session["CONSECUTIVO"] = consecutivo;
                if ((FileUpFotosCom.PostedFile != null) && (FileUpFotosCom.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpFotosCom.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("PictureComercio") + "\\" + fn;
                    try
                    {
                        if (SaveLocation != string.Empty)
                        {
                            if (FileUpFotosCom.FileName.EndsWith(".jpg") || FileUpFotosCom.FileName.EndsWith(".png") || FileUpFotosCom.FileName.EndsWith(".JPG") || FileUpFotosCom.FileName.EndsWith(".PNG"))
                            {
                                int cinfo = Convert.ToInt32(this.Session["cinfo"].ToString().Trim());
                                FileUpFotosCom.PostedFile.SaveAs(MapPath("../../Modulos/Operativo/PictureComercio/" + FileUpFotosCom.FileName));
                                string sPhotoPathName = FileUpFotosCom.FileName;
                                ECompetition__Information ActividadCom = oCompetition__Information.RegistrarPhotoActividadCom(cinfo, sPhotoPathName, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                Alertas.CssClass = "MensajesSupConfirm";
                                LblFaltantes.Text = "Sr. Usuario.";
                                LblAlert.Text = "La fotografía ha sido cargada con exito";
                                PopupMensajes();
                                return;
                            }
                            else
                            {
                                Alertas.CssClass = "MensajesformatoFotos";
                                LblFaltantes.Text = "Sr. Usuario, el formato de la imagen que desea cargar no esta permitido por la aplicación. Intentelo nuevamente";
                                LblAlert.Text = "Formatos validos *.jpg *.png";
                                PopupMensajes();
                                return;
                            }
                        }
                    }

                    catch (Exception ioex)
                    {

                    }

                }
                else
                {
                    Alertas.CssClass = "MensajesformatoFotos";
                    LblFaltantes.Text = "Sr. Usuario, debe seleccionar alguna fotografia para cargar";
                    LblAlert.Text = "Formatos validos *.jpg *.png";
                    PopupMensajes();
                    return;
                }

            }
        }

        protected void BtncancelCargarFotos_Click(object sender, EventArgs e)
        {

        }
    }
}
