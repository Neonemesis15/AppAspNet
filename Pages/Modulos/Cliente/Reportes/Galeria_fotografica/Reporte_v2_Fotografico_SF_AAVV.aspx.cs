using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using System.Data;
using Lucky.Data;
using Lucky.CFG.Util;
using Telerik.Web.UI;
using System.IO;
using System.Collections;

namespace SIGE.Pages.Modulos.Cliente.Reportes.Galeria_fotografica
{
    public partial class Reporte_v2_Fotografico_SF_AAVV : System.Web.UI.Page
    {
        int icompany;
        private string sidcanal;
        private int Report;
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Conexion oCoon = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("cache-control", "private");

            Response.AddHeader("pragma", "no-cache");

            Response.AddHeader("Cache-Control", "must-revalidate");

            Response.AddHeader("Cache-Control", "no-cache");

            //Response.AppendHeader("Cache-Control", "no-cache");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.Now);
            //Response.Cache.SetNoServerCaching();
            //Response.Cache.SetNoStore();
            //Response.AddHeader("Pragma", "no-cache");

            if (!Page.IsPostBack)
            {
                calendar_day.Enabled = false;
                //cargarCobertura();
                Llena_Años();
                Llena_Meses();
                cargarGaleriaFotos();
            }
            else {
                MyAccordion.SelectedIndex = 1;
            }
        }

        #region Llenado Datos

        private void Llena_Años()
        {

            DataTable dty = null;
            dty = Get_Administrativo.Get_ObtenerYears();
            if (dty.Rows.Count > 0)
            {
                cmb_año.DataSource = dty;
                cmb_año.DataValueField = "Years_Number";
                cmb_año.DataTextField = "Years_Number";
                cmb_año.DataBind();
                cmb_año.DataSource = dty;
                cmb_año.DataValueField = "Years_Number";
                cmb_año.DataTextField = "Years_Number";
                cmb_año.DataBind();
                cmb_año.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            }
            else
            {
                dty = null;
            }
        }

        private void Llena_Meses()
        {
            DataTable dtm = Get_Administrativo.Get_ObtenerMeses();

            if (dtm.Rows.Count > 0)
            {
                cmb_mes.DataSource = dtm;
                cmb_mes.DataValueField = "codmes";
                cmb_mes.DataTextField = "namemes";
                cmb_mes.DataBind();


                cmb_mes.DataSource = dtm;
                cmb_mes.DataValueField = "codmes";
                cmb_mes.DataTextField = "namemes";
                cmb_mes.DataBind();
                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            }
            else
            {
                dtm = null;

            }

        }

        private void Llenar_Periodos()
        {
            DataTable dtp = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            sidcanal = this.Session["Canal"].ToString().Trim();
            if (Report == 0)
                Report = 23;
            sidcanal = "1025";
            icompany = 1572;
            //Report = 19;
            //sidcanal = "1025";
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", sidcanal, icompany, Report, cmb_mes.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                cmb_periodo.DataSource = dtp;
                cmb_periodo.DataValueField = "id_periodo";
                cmb_periodo.DataTextField = "Periodo";
                cmb_periodo.DataBind();

                //cmb_periodo.DataSource = dtp;
                //cmb_periodo.DataValueField = "id_periodo";
                //cmb_periodo.DataTextField = "Periodo";
                //cmb_periodo.DataBind();
                cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            }
            else
            {
                dtp = null;
            }
        }

        private void LLenarDiasxPerido()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            sidcanal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);
            calendar_day.Enabled = true;
            calendar_day.DateInput.Text = "";
            DataTable dtp = null;
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_DIAS_DEL_PERIODOS", sidcanal, icompany, Report, cmb_año.SelectedValue, cmb_mes.SelectedValue, Convert.ToInt32(cmb_periodo.SelectedValue));
            if (dtp.Rows.Count > 0)
            {

                calendar_day.Calendar.RangeMinDate = Convert.ToDateTime(dtp.Rows[0]["fechaInicial"]);
                calendar_day.Calendar.RangeMaxDate = Convert.ToDateTime(dtp.Rows[0]["fechaFinal"]);
                calendar_day.Calendar.OutOfRangeDayStyle.BackColor = System.Drawing.Color.Magenta;
                calendar_day.EnableTyping = false;

            }
            else
            {
                dtp = null;
            }
        }

        #endregion

        //protected void btn_ocultar_Click(object sender, EventArgs e)
        //{
        //    if (Div_filtros.Visible == false)
        //    {

        //        Div_filtros.Visible = true;
        //        btn_ocultar.Text = "Ocultar";
        //    }
        //    else if (Div_filtros.Visible == true)
        //    {
        //        Div_filtros.Visible = false;
        //        btn_ocultar.Text = "Filtros";
        //    }
        //}

        protected void cargarGaleriaFotos()
        {
            try
            {
                icompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();

                //string sidtiporeporte = cmb_tipoReporte.SelectedValue;
                //string sidcity = cmb_ciudad.SelectedValue;
                string sidano = cmb_año.SelectedValue;
                string sidmes = cmb_mes.SelectedValue;
                string sidperiodo = cmb_periodo.SelectedValue;
                string siddia = calendar_day.Calendar.SelectedDate.Day.ToString();
                string siddiaText = calendar_day.DateInput.Text;
                if (sidano == "")
                    sidano = "0";
                if (sidmes == "")
                    sidmes = "0";
                if (sidperiodo == "")
                    sidperiodo = "0";
                if (siddiaText == "")
                    siddia = "0";
                string siddistribuidora = "0";

                crearXML(icompany, sidcanal, siddistribuidora, sidano, sidmes, sidperiodo, siddia);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        public void crearXML(int iidcompany, string sidcanal, string siddistribuidora, string ano, string mes, string sidperiodo, string dia)
        {
            try
            {
                DataTable dt = null;

                

                string nameFile = "Data.xml";
                string SaveLocation = Server.MapPath(@"~\Pages\Modulos\Cliente\Reportes\Galeria_fotografica") + "\\" + nameFile;

                //File.Delete(SaveLocation);
                int iservicio = Convert.ToInt32(this.Session["Service"]);
                dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_REPORTE_FOTOGRAFICO_SF_AAVV", iidcompany, iservicio, sidcanal, siddistribuidora, ano, mes, sidperiodo, dia);


                XmlTextWriter writer = new XmlTextWriter(SaveLocation, Encoding.UTF8);

                //Usa indentación por legibilidad
                writer.Formatting = Formatting.Indented;

                //Escribe la declaración del XML
                writer.WriteStartDocument();

                //Escribe el elemento raiz

                writer.WriteStartElement("data");
                writer.WriteAttributeString("transition", "CrossFadeTransition");

                string valor = "";
                string valorSiguiente = "";
                string valorSubSiguiente = "";

                int cont = 0;
                int flag = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (i < dt.Rows.Count)
                        valor = dt.Rows[i]["Id_Dex"].ToString();

                    if (i < dt.Rows.Count - 1)
                        valorSiguiente = dt.Rows[i + 1]["Id_Dex"].ToString();

                    if (i < dt.Rows.Count - 2)
                        valorSubSiguiente = dt.Rows[i + 2]["Id_Dex"].ToString();

                    if (valor != valorSiguiente)
                        flag = 1;
                    if ((valor == valorSiguiente && i < dt.Rows.Count) || (valor != valorSubSiguiente && cont == 0 && i < dt.Rows.Count) || (flag == 1))
                    {
                        if (cont == 0)
                        {
                            string tituloAlbum =/* "Mercado :" + */dt.Rows[i]["Dex_Name"].ToString();
                            string descripcionAlbum = "Distrito :" + dt.Rows[i]["Name_Local"].ToString();
                            string imagenAlbum = "Fotos/" + dt.Rows[i]["Url_foto"].ToString();

                            writer.WriteStartElement("album");
                            writer.WriteAttributeString("title", tituloAlbum);
                            writer.WriteAttributeString("description", descripcionAlbum);
                            writer.WriteAttributeString("image", imagenAlbum);
                        }

                        string tituloFoto = "Cliente: " + dt.Rows[i]["pdv_Name"].ToString() + ",  Nivel :" + dt.Rows[i]["Segment_Name"].ToString().ToUpper()
                        + ",  Mercado :" + dt.Rows[i]["commercialNodeName"].ToString().ToUpper() + ",  Distribuidora :" + dt.Rows[i]["Dex_Name"].ToString().ToUpper()
                        + ",  Distrito :" + dt.Rows[i]["Name_Local"].ToString().ToUpper();

                        string descripcionFoto = "Tipo de Incidencia:" + dt.Rows[i]["Tipo de Incidencia"].ToString() + ",  Incidencia:" + dt.Rows[i]["Subtipo Incidencia"].ToString()
                        + ",  Detalle:" + dt.Rows[i]["Comentario"].ToString();
                        //string Nivel = "Nivel:" + dt.Rows[i]["Segment_Name"].ToString();
                        //string Mercado = "Mercado:" + dt.Rows[i]["Segment_Name"].ToString();

                        string imagenFoto = "Fotos/" + dt.Rows[i]["Url_foto"].ToString();
                        string thumbnailFoto = "Fotos/" + dt.Rows[i]["Url_foto"].ToString();

                        writer.WriteStartElement("slide");
                        writer.WriteAttributeString("title", tituloFoto);
                        writer.WriteAttributeString("description", descripcionFoto);
                        writer.WriteAttributeString("image", imagenFoto);
                        writer.WriteAttributeString("thumbnail", thumbnailFoto);

                        //Escribe los elementos dentro de sus etiquetas

                        //writer.WriteElementString("dificultad", "media");
                        //writer.WriteElementString("organizacion", "www.aguilarweb.com");

                        writer.WriteEndElement();


                        cont = cont + 1;
                    }
                    if (valor != valorSiguiente && flag == 1 && i < dt.Rows.Count)
                    {
                        writer.WriteEndElement();
                        cont = 0;
                        flag = 0;
                    }
                }
                writer.WriteEndElement();

                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void cargarParametrosdeXml()
        {
            string path = Server.MapPath("~/parametros.xml");

            if (System.IO.File.Exists(path))
            {
                Reportes_parametros oReportes_parametros = new Reportes_parametros();


                oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
                oReportes_parametros.Id_user = this.Session["sUser"].ToString();
                oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
                oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
                oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

                Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

                //RadGrid_parametros.DataSource = oReportes_parametrosToXml.Get_Parametros(oReportes_parametros, path);
                //RadGrid_parametros.DataBind();
            }
        }

        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Llenar_Periodos();
        }

        protected void cmb_periodo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LLenarDiasxPerido();
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            if (cmb_año.SelectedIndex == 0)
                cmb_año.SelectedIndex = 1;
            cargarGaleriaFotos();
        } 
    }
}