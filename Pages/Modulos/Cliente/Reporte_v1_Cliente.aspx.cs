using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Business.Common.Application;
using Lucky.Business.Common.Security;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Data.Common.Security;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Security;
using Lucky.Entity.Common.Application.Security;
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;
using Lucky.CFG.Tools;

namespace SIGE.Pages.Modulos.Cliente
{
    public partial class Reporte_v1_Cliente : System.Web.UI.Page
    {

        Conexion oCoon = new Conexion();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();

        string sUser;
        string sPassw;
        string sNameUser;



        private bool Validar_fechas()
        {
            DateTime Fechaini, Fechafin;
            try
            {
                Fechaini = DateTime.Parse(txtfecha.Text);
                Fechafin = DateTime.Parse(txtfechafin.Text);
                if (Fechaini >= Fechafin)
                {

                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";


                    //this.Session["mensaje"] = this.Session["mensaje"] + " " + "La Fecha de Inicio de Ejecucion no puede ser mayor y/o Igual que la de Finalización";
                    this.Session["mensaje"] = "La fecha de inicio de ejecución no puede ser mayor y/o igual que la de finalización";
                    Mensajes_Usuario();
                    return false;
                }
                return true;
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
                Mensajes_Usuario();
                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    sUser = this.Session["sUser"].ToString();
                    sPassw = this.Session["sPassw"].ToString();
                    sNameUser = this.Session["nameuser"].ToString();
                    TxtSolicitante.Text = this.Session["smail"].ToString().Trim();
                    //url_foto = this.Session["fotocomany"].ToString();
                      //Imgcliente.ImageUrl = url_foto;
                    TxtEmail.Text = "mortiz.col@lucky.com.pe";
                      //TablaInformes.Style.Value = "display:none;";
                      PanelCorreos.Style.Value = "display:none;";
                      PanelInfo.Style.Value = "display:none;";
                    //usersession.Text = sUser;
                    //lblUsuario.Text = sNameUser;
                    //lblcompany.Text = this.Session["sNombre"].ToString();
                    if (sUser != null && sPassw != null)
                    {
                        //BotonPOP();
                        //ValidaBotonInfGerencial();
                        //llenacanales();
                        //for (int i = 0; i <= ListViewCanales.Items.Count - 1; i++)
                        //{
                        //    Label lbCodCanal = new Label();
                        //    lbCodCanal = ((Label)ListViewCanales.Items[i].FindControl("CodCanal"));
                        //    if (this.Session["Canal"].ToString().Trim() == lbCodCanal.Text)
                        //    {
                        //        string NameCanal = this.Session["NameCanal"].ToString().Trim();

                        //        ((LinkButton)ListViewCanales.Items[i].FindControl("SelectCanalesButton")).CssClass = "p" + NameCanal + "Seleccionado";
                        //        ((LinkButton)ListViewCanales.Items[i].FindControl("SelectCanalesButton")).Enabled = false;

                        //    }
                        //}
                        llenareportes();
                    }
                }
                catch (Exception ex)
                {
                    Get_Administrativo.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                    Exception mensaje = ex;
                    this.Session.Abandon();
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                   
                }
            }
            
            iframeExcel.Attributes["src"] = "";
        }


        public void llenareportes()
        {
            try
            {
                string scity = this.Session["scity"].ToString().Trim();
                DataTable dtReportes = Get_DataClientes.Get_Obtener_reporteXCliente_Canal(Convert.ToInt32(this.Session["personid"]), this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["companyid"]));

                if (dtReportes.Rows.Count > 0)
                {
                    btnnext.Visible = true; //Se agrego al aspx un id a los botones del carrusel Ing. Carlos Hernandez 09/11/2010
                    btnprev.Visible = true;

                    // se quita el if ya que hace exactamente lo mismo por los dos caminos. 16/11/2010 Ing. Mauricio Ortiz
                    //if (scity == "")
                    //{
                        ListViewReportes.DataSource = dtReportes;
                        ListViewReportes.DataBind();
                    //}
                    //else
                    //{
                    //    ListViewReportes.DataSource = dtReportes;
                    //    ListViewReportes.DataBind();

                    //}



                }
                    //Se vuelve a Colocar esta validación porque se elimino en el ultimo ajuste Ing. Carlos Hernandez 09/11/2010
                else 
                {
                
                    ListViewReportes.DataBind();//Se agrega limpiado del control cuando no hay datos y mensaje Ing. Carlos Hernandez 
                    btnnext.Visible = false;
                    btnprev.Visible = false;
                    ScriptManager.RegisterStartupScript(
                                    this, this.GetType(), "myscript", "alert('Señor Usuario: Su Perfil no tiene Permisos para visualizar Reportes en este Canal');", true);

                }
            }
            catch (Exception ex)
            {
                Exception mensaje = ex;

                Get_Administrativo.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }


        //public void BotonPOP()
        //{
        //    int iperonid;
        //    iperonid = Convert.ToInt32(this.Session["personid"]);
        //    DataTable dt = null;
        //    dt = Get_DataClientes.Get_obtnerBotonBlioteca(iperonid, Convert.ToInt32(this.Session["companyid"]));

        //    if (dt.Rows.Count > 0)
        //    {
        //        Biblioteca.DataSource = dt;
        //        Biblioteca.DataBind();


        //    }
        //    else
        //    {

        //        dt = null;
        //        Biblioteca.Visible = false;
        //    }


        //}

        protected void ListViewReportes_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)ListViewReportes.Items[e.NewSelectedIndex];
            this.Session["ReporteSeleccionado"] = e.NewSelectedIndex;
            iframeExcel.Attributes["src"] = "";

        }
        protected void ListViewReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;

            try
            {


                int item = Convert.ToInt32(this.Session["ReporteSeleccionado"]);
                Label lbCodReporte = new Label();
                lbCodReporte = ((Label)ListViewReportes.Items[item].FindControl("CodReporte"));
                this.Session["Reporte"] = lbCodReporte.Text;
                //Permite validar Reportes No COntratados por el Cliente Ing.Carlos Hernández
                dt = Get_DataClientes.Get_ObtenerReportesNoActivos(Convert.ToInt32(lbCodReporte.Text), Convert.ToInt32(this.Session["companyid"]));
                if (dt.Rows.Count > 0)
                {
                    bool Estado = Convert.ToBoolean(dt.Rows[0]["ReportCanal_Status"]);
                    if (Estado == false)
                    {
                        ScriptManager.RegisterStartupScript(
                              this, this.GetType(), "myscript", "alert('Reporte no Contratado');", true);



                    }
                }



                // TablaInformes.Style.Value = "display:block;";
                PanelCorreos.Style.Value = "display:none;";
                iframeExcel.Attributes["src"] = "";
                gvLink_informes.DataBind();
                DataTable dturl = null;
                int personinfor;
                personinfor = Convert.ToInt32(this.Session["personid"]);
                dturl = Get_DataClientes.Get_ObtenerurlInformes(this.Session["scountry"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["Reporte"]), personinfor);
                if (dturl != null)
                {
                    if (dturl.Rows.Count > 0)
                    {
                        PanelInfo.Style.Value = "display:Block;";
                        gvLink_informes.DataSource = dturl;
                        gvLink_informes.DataBind();

                        gvLink_informes_invisible.DataSource = dturl;
                        gvLink_informes_invisible.DataBind();

                        gvLink_informes.SelectedIndex = -1;
                    }
                    else
                    {
                        PanelInfo.Style.Value = "display:none;";
                        ScriptManager.RegisterStartupScript(
                        this, this.GetType(), "myscript", "alert('No existe información disponible para este tipo de reporte');", true);


                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "No existe información disponible para este tipo de reporte";
                        Mensajes_Usuario();
                    }
                }
            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Get_Administrativo.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

        }
        protected void ListViewReportes_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            // Clear selection. 
            iframeExcel.Attributes["src"] = "";
            ListViewReportes.SelectedIndex = -1;
        }
        private void Mensajes_Usuario()
        {
            PCanal.CssClass = this.Session["cssclass"].ToString();
            lblencabezado.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneral.Text = this.Session["mensaje"].ToString();
            ModalPopupCanal.Show();
        }
        //public void llenacanales()
        //{
        //    TablaInformes.Style.Value = "display:none;";

        //    iframeExcel.Attributes["src"] = "";
        //    gvLink_informes.DataBind();
        //    DataTable dtcanales = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENERCNALXSERVICIO", this.Session["Cod_ServicioSel"].ToString().Trim(), Convert.ToInt32(this.Session["companyid"]));

        //    MenuCanales.Items.Clear();
        //    if (dtcanales != null)
        //    {
        //        if (dtcanales.Rows.Count > 0)
        //        {
        //            for (int i = 0; i <= dtcanales.Rows.Count - 1; i++)
        //            {
        //                MenuItem item = new MenuItem();
        //                item.Text = dtcanales.Rows[i]["nombre_canal"].ToString().Trim();
        //                item.Value = dtcanales.Rows[i]["codigo_canal"].ToString().Trim();
        //                MenuCanales.Items.Add(item);
        //            }
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnAnfitrionaje_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnAnfitrionaje.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnBlitz_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnBlitz.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnBtl_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnBtl.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }


        //}

        //protected void BtnCanje_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnCanje.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnDegustacion_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnDegustacion.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnImpulso_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnImpulso.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }

        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnMercadersimo_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnMercadersimo.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }

        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnSampling_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnSampling.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }

        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnSellSampling_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnSellSampling.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }

        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void NtnVisibilidad_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", NtnVisibilidad.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }

        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void MenuCanales_MenuItemClick(object sender, MenuEventArgs e)
        //{
        //    TablaInformes.Style.Value = "display:block;";
        //    PanelCorreos.Style.Value = "display:none;";
        //    iframeExcel.Attributes["src"] = "";
        //    gvLink_informes.DataBind();
        //    DataTable dturl = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_URLINFORMES", this.Session["scountry"].ToString(), Convert.ToInt32(this.Session["companyid"]), Convert.ToInt32(this.Session["Cod_ServicioSel"].ToString().Trim()), MenuCanales.SelectedItem.Value);
        //    gvLink_informes.DataSource = dturl;
        //    gvLink_informes.DataBind();

        //    gvLink_informes_invisible.DataSource = dturl;
        //    gvLink_informes_invisible.DataBind();

        //    gvLink_informes.SelectedIndex = -1;
        //}

        protected void gvLink_informes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string RemoteHost = Request.ServerVariables["REMOTE_ADDR"]; 

            
            iframeExcel.Attributes["src"] = "";
            string fn = System.IO.Path.GetFileName(gvLink_informes_invisible.Rows[gvLink_informes.SelectedRow.DataItemIndex].Cells[1].Text.ToString().Trim());
            iframeExcel.Attributes["src"] = "Informes/" + fn;

            Sesion_Users su = new Sesion_Users();

            DataTable dtDuplicado = su.Duplicado_Auditoria_Download(this.Session["sUser"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["Reporte"]), RemoteHost, fn, DateTime.Now.Hour.ToString().Trim() + ":" + DateTime.Now.Minute.ToString().Trim());
            if (dtDuplicado != null)
            {
                if (dtDuplicado.Rows.Count == 0)
                {
                    su.Registrar_Auditoria_Download(this.Session["sUser"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["Reporte"]), RemoteHost, fn, DateTime.Now, DateTime.Now.Hour.ToString().Trim() + ":" + DateTime.Now.Minute.ToString().Trim());
                }
            } 
        }

        

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //TablaInformes.Style.Value = "display:none;";
            PanelCorreos.Style.Value = "display:block;";
            //MenuCanales.Items.Clear();
            iframeExcel.Attributes["src"] = "";
        }

        protected void ImgCancelMail_Click(object sender, ImageClickEventArgs e)
        {
            PanelCorreos.Style.Value = "display:none;";
        }

        protected void ImgEnviarMail_Click(object sender, ImageClickEventArgs e)
        {
            if (TxtMotivo.Text == "" || TxtMensaje.Text == "")
            {
                this.Session["encabemensa"] = "Envío de Solicitudes";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Sr. Usuario, es necesario que ingrese información en el asunto y en el mensaje";
                Mensajes_Usuario();
                return;
            }
            try
            {
                Enviomail oEnviomail = new Enviomail();
                EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Solicitud_Operativo");
                Mails oMail = new Mails();
                oMail.Server = "mail.lucky.com.pe";
                //oeEmail.MailServer;
                oMail.From = TxtSolicitante.Text;
                oMail.To = TxtEmail.Text;
                oMail.Subject = TxtMotivo.Text;
                oMail.Body = TxtMensaje.Text;
                oMail.CC = "chernandez.col@lucky.com.pe";
                oMail.BodyFormat = "HTML";
                oMail.send();
                oMail = null;
                // oeEmail = null;
                oEnviomail = null;
                TxtMotivo.Text = "";
                TxtMensaje.Text = "";
                PanelCorreos.Style.Value = "display:none;";
                this.Session["encabemensa"] = "Envío de Solicitudes";
                this.Session["cssclass"] = "MensajesSupConfirm";
                this.Session["mensaje"] = "Sr. Usuario, el mensaje fue enviado correctamente";
                Mensajes_Usuario();
            }
            catch (Exception ex)
            {
                this.Session["encabemensa"] = "Envío de Solicitudes";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Sr. Usuario, se presentó un error inesperado al momento de enviar el correo. Por favor intentelo nuevamente o consulte al Administrador de la aplicación";
                Mensajes_Usuario();
            }
        }

        /// <summary>
        /// Se ajusta este Metodo agregando el metodo de WS y controlando la no existencia de retorno de Datos y Limoiando la grilla
        /// Modificado por: Ing. Carlos Hernandez
        /// Fecha: 11/11/2010
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLink_informes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                iframeExcel.Attributes["src"] = "";
                gvLink_informes.PageIndex = e.NewPageIndex;
                 DataTable dturl;
                 int personinfor; 
                personinfor = Convert.ToInt32(this.Session["personid"]);
                dturl = Get_DataClientes.Get_ObtenerurlInformes(this.Session["scountry"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["Reporte"]), personinfor);

                if (dturl.Rows.Count > 0)
                {

                    ///oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_URLINFORMES", this.Session["scountry"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), this.Session["Reporte"]);
                    gvLink_informes.DataSource = dturl;
                    gvLink_informes.DataBind();
                }
                else {

                    gvLink_informes.DataBind();
                
                }
            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void btnsearchr_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtsr = null;
            if (txtfecha.Text != "" && txtfechafin.Text != "")
            {
                dtsr = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_URLINFORMES_TMP", this.Session["scountry"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["Reporte"]), txtfecha.Text, txtfechafin.Text);
                if (dtsr.Rows.Count > 0)
                {
                    gvLink_informes.DataSource = dtsr;
                    gvLink_informes.DataBind();
                    gvLink_informes_invisible.DataBind();
                    txtfecha.Text = "";
                    txtfechafin.Text = "";



                }
                else
                {

                    gvLink_informes.DataBind();
                    txtfecha.Text = "";
                    txtfechafin.Text = "";


                    Validar_fechas();

                }


            }
            else
            {
                PanelInfo.Style.Value = "display:none;";
                ScriptManager.RegisterStartupScript(
                this, this.GetType(), "myscript", "alert('Debe Seleccionar Fechas para Busqueda');", true);




            }
        }

        protected void txtfecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtfecha.Text != "__/__/____" && txtfecha.Text != "")
                {
                    DateTime t = Convert.ToDateTime(txtfecha.Text);
                }
                Validar_fechas();
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                txtfecha.Text = "";
                Mensajes_Usuario();
            }
        }

        protected void txtfechafin_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (txtfechafin.Text != "__/__/____" && txtfecha.Text != "")
                {
                    DateTime t = Convert.ToDateTime(txtfechafin.Text);
                }
                Validar_fechas();
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                txtfechafin.Text = "";
                Mensajes_Usuario();
            }

        }
    }
}