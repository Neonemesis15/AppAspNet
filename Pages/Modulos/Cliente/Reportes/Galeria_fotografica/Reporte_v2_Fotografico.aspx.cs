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

namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_v2_Fotografico : System.Web.UI.Page
    {
        private int iidcompany;
        private string sidcanal;

        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Conexion oCoon = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!Page.IsPostBack)
            {
                cargarCobertura();
                cargarAños();
                cargarMes();
                cargarGaleriaFotos();
            }
            
        }
        protected void btn_ocultar_Click(object sender, EventArgs e)
        {
            if (Div_filtros.Visible == false)
            {

                Div_filtros.Visible = true;
                btn_ocultar.Text = "Ocultar";
            }
            else if (Div_filtros.Visible == true)
            {
                Div_filtros.Visible = false;
                btn_ocultar.Text = "Filtros";
            }
        }

        protected void cargarCobertura()
        {
            DataTable dt = null;
            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            dt = oCoon.ejecutarDataTable("UP_WEXPLORA_CLIEN_V2_LLENACOMBOS",iidcompany,sidcanal,"",1);

            if (dt.Rows.Count > 0)
            {
                cmb_ciudad.DataSource = dt;
                cmb_ciudad.DataValueField = "cod_city";
                cmb_ciudad.DataTextField = "name_city";
                cmb_ciudad.DataBind();

                cmb_ciudad.Items.Insert(0, new ListItem("--Todos--", "0"));
            }

        }
        private void cargarAños()
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
                cmb_año.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            }
            else
            {
                dty = null;
            }
        }
        private void cargarMes()
        {
            DataTable dtm = Get_Administrativo.Get_ObtenerMeses();

            if (dtm.Rows.Count > 0)
            {
                cmb_mes.DataSource = dtm;
                cmb_mes.DataValueField = "codmes";
                cmb_mes.DataTextField = "namemes";
                cmb_mes.DataBind();
                cmb_mes.Items.Insert(0, new ListItem("--Todos--", "0"));
                
            }
        }
        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            if (cmb_año.SelectedIndex == 0)
                cmb_año.SelectedIndex = 1;
            cargarGaleriaFotos();
        } 
        protected void cargarGaleriaFotos()
        {
            try
            {
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();

                string sidtiporeporte = cmb_tipoReporte.SelectedValue;
                string sidcity = cmb_ciudad.SelectedValue;
                string sidano = cmb_año.SelectedValue;
                string sidmes = cmb_mes.SelectedValue;
                if (sidmes == "")
                    sidmes = "0";

                crearXML(iidcompany, sidcanal, sidtiporeporte, sidcity, sidano, sidmes);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        public void crearXML(int iidcompany, string sidcanal, string sidtipoReporte, string sidciudad, string ano, string mes)
        {
            try
            {
                DataTable dt = null;

                string nameFile = "Data.xml";
                string SaveLocation = Server.MapPath(@"~\Pages\Modulos\Cliente\Reportes\Galeria_fotografica") + "\\" + nameFile;
                int iservicio = Convert.ToInt32(this.Session["Service"]);
                dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_REPORTE_FOTOGRAFICO",iidcompany,iservicio,sidcanal, sidtipoReporte, sidciudad, ano, mes);


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
                int flag=0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (i < dt.Rows.Count)
                        valor = dt.Rows[i]["ClientPDV_Code"].ToString();

                    if (i < dt.Rows.Count - 1)
                        valorSiguiente = dt.Rows[i + 1]["ClientPDV_Code"].ToString();

                    if (i < dt.Rows.Count - 2)
                        valorSubSiguiente = dt.Rows[i + 2]["ClientPDV_Code"].ToString();

                    if (valor != valorSiguiente)
                        flag = 1;
                    if ((valor == valorSiguiente && i < dt.Rows.Count) || (valor != valorSubSiguiente && cont == 0 && i < dt.Rows.Count)|| (flag==1))
                    {
                        if (cont == 0)
                        {
                            string tituloAlbum = "Oficina:" + dt.Rows[i]["Name_Oficina"].ToString();
                            string descripcionAlbum = "Punto de venta:" + dt.Rows[i]["pdv_Name"].ToString();
                            string imagenAlbum = "Fotos/" + dt.Rows[i]["Url_foto"].ToString();

                            writer.WriteStartElement("album");
                            writer.WriteAttributeString("title", tituloAlbum);
                            writer.WriteAttributeString("description", descripcionAlbum);
                            writer.WriteAttributeString("image", imagenAlbum);
                        }

                        string tituloFoto = "Punto de Venta: " + dt.Rows[i]["pdv_Name"].ToString() + ",  Tipo de Reporte:" + dt.Rows[i]["TipoReporte_Descripcion"].ToString().ToUpper();
                        string descripcionFoto = "Comentario:" + dt.Rows[i]["Comentario"].ToString() + ",  Fecha de Toma:" + dt.Rows[i]["Fec_Reg_Cel"].ToString();
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
                    if (valor != valorSiguiente && flag==1 && i < dt.Rows.Count)
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
        protected void buttonGuardar_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/parametros.xml");
            Reportes_parametros oReportes_parametros = new Reportes_parametros();


            oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
            oReportes_parametros.Id_user = this.Session["sUser"].ToString();
            oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
            oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
            oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

            oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_ciudad.SelectedValue);

            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            oReportes_parametros.Id_tipoReporte = cmb_tipoReporte.SelectedValue;

            string periodo = cmb_periodo.SelectedValue;
            if (periodo == "")
                periodo = "0";
            oReportes_parametros.Id_periodo = Convert.ToInt32(periodo);

            oReportes_parametros.Descripcion = txt_descripcion_parametros.Text.Trim();

            Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

            if (!System.IO.File.Exists(path))
                oReportes_parametrosToXml.createXml(oReportes_parametros, path);
            else
                oReportes_parametrosToXml.addToXml(oReportes_parametros, path);


            cargarParametrosdeXml();
            txt_descripcion_parametros.Text = "";
            TabContainer_filtros.ActiveTabIndex = 1;
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

                RadGrid_parametros.DataSource = oReportes_parametrosToXml.Get_Parametros(oReportes_parametros, path);
                RadGrid_parametros.DataBind();
            }
        }

        protected void btn_imb_tab_Click(object sender, ImageClickEventArgs e)
        {
            TabContainer_filtros.ActiveTabIndex = 0;
        }

        protected void RadGrid_parametros_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            if (e.CommandName == "BUSCAR")
            {
                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_periodo = (Label)e.Item.FindControl("lbl_id_periodo");
                Label lbl_id_oficina = (Label)e.Item.FindControl("lbl_id_oficina");
                Label lbl_id_tipoReporte = (Label)e.Item.FindControl("lbl_id_tipoReporte");


                crearXML(iidcompany, sidcanal, lbl_id_tipoReporte.Text.Trim(), lbl_id_oficina.Text.Trim(), lbl_id_año.Text.Trim(), lbl_id_mes.Text.Trim());

            }
            if (e.CommandName == "ELIMINAR")
            {
                Label lbl_id = (Label)e.Item.FindControl("lbl_id");

                string path = Server.MapPath("~/parametros.xml");
                Reportes_parametros oReportes_parametros = new Reportes_parametros();
                oReportes_parametros.Id = Convert.ToInt32(lbl_id.Text);



                Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

                oReportes_parametrosToXml.DeleteElement(oReportes_parametros, path);
            }
        }

        //[System.Web.Services.WebMethod]
        // public  static string UserID()
        // {
        //    // rutaConfigurationData = "Configuration_Data" +sUserStatic + ".xml ";
        //     return rutaConfigurationData;
        // }
        #region METODO POSTERGADO PARA SU FUTURO USO
        //protected void crearConfiguration_DataXML()//METODO POSTERGADO PARA SU FUTURO USO
        //{
        //    //primero creamos Solo el DataXml para luego combinarlo con la configuracion inicial
        //    string nameFile = "Configuration_Data"+sUser+".xml";
        //    string SaveLocation = Server.MapPath(@"~\Pages\Modulos\Cliente\Reportes\Galeria_fotografica") + "\\" + nameFile;

        //    using (XmlTextWriter writer = new XmlTextWriter(SaveLocation, Encoding.UTF8))
        //    {
        //        //Usa indentación por legibilidad
        //        writer.Formatting = Formatting.Indented;

        //        //Escribe la declaración del XML
        //        writer.WriteStartDocument();


        //        writer.WriteStartElement("configuration");
        //        writer.WriteAttributeString("width", "1000");
        //        writer.WriteAttributeString("height", "400");

        //            writer.WriteStartElement("scripts");//start scripts
        //                writer.WriteStartElement("script");
        //                writer.WriteAttributeString("key", "AlbumButton");
        //                writer.WriteAttributeString("url", "../../Galeria_fotografica/Debug/AlbumButton.js");
        //                writer.WriteAttributeString("extendsClass", "Button");
        //                writer.WriteEndElement();
        //            writer.WriteEndElement();//End scripts


        //            writer.WriteStartElement("modules");//start modules
        //                writer.WriteStartElement("module");
        //                writer.WriteAttributeString("type", "SlideViewer");
        //                writer.WriteEndElement();

        //                writer.WriteStartElement("module");
        //                writer.WriteAttributeString("type", "ProgressBar");
        //                writer.WriteEndElement();

        //                writer.WriteStartElement("module");
        //                writer.WriteAttributeString("type", "SlideDescription");
        //                writer.WriteEndElement();

        //                writer.WriteStartElement("module");
        //                writer.WriteAttributeString("type", "NavigationTray");
        //                    writer.WriteStartElement("option");
        //                    writer.WriteAttributeString("name","initialAlbumView");
        //                    writer.WriteAttributeString("value","true");
        //                    writer.WriteEndElement();
        //                writer.WriteEndElement();
        //            writer.WriteEndElement();//end modules

        //            writer.WriteStartElement("transitions");//start transitions
        //            writer.WriteStartElement("transition");
        //            writer.WriteAttributeString("type", "FadeTransition");
        //            writer.WriteAttributeString("name", "CrossFadeTransition");
        //            writer.WriteEndElement();

        //            writer.WriteStartElement("transition");
        //            writer.WriteAttributeString("type", "ShapeTransition");
        //            writer.WriteAttributeString("name", "CircleOutTransition");
        //            writer.WriteEndElement();

        //            writer.WriteStartElement("transition");
        //            writer.WriteAttributeString("type", "SlideTransition");
        //            writer.WriteAttributeString("name", "SlideLeftTransition");
        //            writer.WriteEndElement();

        //            writer.WriteStartElement("transition");
        //            writer.WriteAttributeString("type", "SlideTransition");
        //            writer.WriteAttributeString("name", "SlideDownTransition");
        //            writer.WriteStartElement("option");
        //            writer.WriteAttributeString("name", "direction");
        //            writer.WriteAttributeString("value", "Down");
        //            writer.WriteEndElement();
        //            writer.WriteEndElement();
        //            writer.WriteStartElement("transition");
        //            writer.WriteAttributeString("type", "WipeTransition");
        //            writer.WriteAttributeString("name", "WipeRightTransition");
        //            writer.WriteStartElement("option");
        //            writer.WriteAttributeString("name", "direction");
        //            writer.WriteAttributeString("value", "Right");
        //            writer.WriteEndElement();
        //            writer.WriteEndElement();
        //            writer.WriteEndElement();//end transitions


        //            writer.WriteStartElement("dataProvider");
        //            writer.WriteAttributeString("type", "XmlDataProvider");
        //            writer.WriteStartElement("option");
        //            writer.WriteAttributeString("name", "url");
        //            writer.WriteAttributeString("value","Data_" + sUser + ".xml");
        //            writer.WriteEndElement();
        //            writer.WriteEndElement();
        //       writer.WriteEndElement();

        //       writer.WriteEndDocument();

        //       writer.Close();

        //    }

        //}
        #endregion
    }
}