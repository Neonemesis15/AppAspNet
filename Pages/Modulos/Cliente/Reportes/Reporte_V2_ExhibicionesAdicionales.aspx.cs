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
using Microsoft.Reporting.WebForms;
using System.Configuration;
using Telerik.Web.UI;
using System.Xml;
using System.Text;


namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_V2_ExhibicionesAdicionales : System.Web.UI.Page
    {
        private int iidcompany;
        private string sidcanal;

        string sUser;
        string sPassw;
        string sNameUser;
        int iservicio;
        string canal;
        int Report;
        ReportViewer reporte1;
        ReportViewer repordet;
        ReportViewer repvquincenal;
        ReportViewer repbrecmarg;
        ReportViewer repIndice;
        ReportViewer repcompa;
        ReportViewer repcompaciu;
        ReportViewer reppanel;
        ReportViewer repCumpliLayout;
        ReportViewer rptGTipo;

        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();
        Conexion oCoon = new Conexion();


        #region Generacion Informes de Gestión


        private void GenerarGestionExh()
        {
             iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                reporte1 = (ReportViewer)(Reporte_V2_GetionDeExibicion1.FindControl("ReporG"));
                reporte1.Visible = true;
                reporte1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                reporte1.ServerReport.ReportPath = "/Reporte_Precios_V1/EvolucionMensualExhAASS";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                reporte1.ServerReport.ReportServerUrl = new Uri(strConnection);
                reporte1.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(iidcompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", icadena.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", cmb_año.SelectedValue));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", cmb_mes.SelectedValue));

             
             

                

                reporte1.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


            }

        }


        private void GenerarDetalleExh()
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                repordet = (ReportViewer)(Reporte_v2_DetalleDeExhibicion1.FindControl("ReporD"));
                repordet.Visible = true;
                repordet.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                repordet.ServerReport.ReportPath = "/Reporte_Precios_V1/Rpt_DExhCadena";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                repordet.ServerReport.ReportServerUrl = new Uri(strConnection);
                repordet.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(iidcompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", icadena.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));






                repordet.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


            }

        }



        private void GenerarDetalleExhTipo()
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {

                rptGTipo = (ReportViewer)(Revporte_v2_GetionDeExibicionD1.FindControl("ReporGT"));
                rptGTipo.Visible = true;
                rptGTipo.ServerReport.ReportPath = "/Reporte_Precios_V1/Rpt_DetalleExhibidor";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rptGTipo.ServerReport.ReportServerUrl = new Uri(strConnection);
                rptGTipo.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(iidcompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", icadena.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                rptGTipo.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

        
                this.Session.Abandon();
                //Response.Redirect("~/err_mensaje_seccion.aspx", true);
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
            


            }

        }
        private void GenerarCumplimientoLayout()
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {
                repCumpliLayout = (ReportViewer)(Reporte_v2_CumplimientoLayout1.FindControl("ReportCumplimientoLayout"));
                
                repCumpliLayout.Visible = true;
                repCumpliLayout.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                repCumpliLayout.ServerReport.ReportPath = "/Reporte_Precios_V1/Informe_Exhibiciones_CumplimientoLayout";


                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                repCumpliLayout.ServerReport.ReportServerUrl = new Uri(strConnection);
                repCumpliLayout.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(iidcompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", icadena.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", cmb_mes.SelectedValue));


                repCumpliLayout.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


            }

        }


        protected void GetPeriodForClient()
        { //se obtiene el ultimo años mes y perido validado por el analista, para que el cliente pueda ver dicho reporte
            DataTable dt = null;

            Report = Convert.ToInt32(this.Session["Reporte"]);
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_MAX_PERIODO_VALIDADO", Report);


            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    sidaño = dt.Rows[0]["id_Year"].ToString();//con estos datos se debe hacer la carga
                    sidmes = dt.Rows[0]["id_Month"].ToString();
                    sidperiodo = dt.Rows[0]["periodo"].ToString();
                }

            }
        }

        protected void GetPeridForAnalist()
        {//se obtiene el estado de un Reporte en un Año, mes y periodo especifico.Y otros datos adicionales del periodo obtenido

            Report = Convert.ToInt32(this.Session["Reporte"]);
            DataTable dt = null;
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO", canal, Report, sidaño, sidmes, 1);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    div_Validar.Visible = true;
                    sidaño = dt.Rows[0]["id_Year"].ToString();
                    sidmes = dt.Rows[0]["id_Month"].ToString();
                    sidperiodo = dt.Rows[0]["periodo"].ToString();
                    bool valid_analist = Convert.ToBoolean(dt.Rows[0]["ReportsPlanning_ValidacionAnalista"]);

                    lbl_año_value.Text = sidaño;
                    lbl_mes_value.Text = sidmes;
                    lbl_periodo_value.Text = sidperiodo;


                    if (valid_analist)
                        chkb_validar.Checked = valid_analist;
                    else
                        chkb_invalidar.Checked = true;

                    lbl_validacion.Text = sidaño + "-" + dt.Rows[0]["Month_name"].ToString() + " " + sidperiodo;

                }
            }
        }


        #endregion

        private string sidaño;
        private string sidmes;
        private string sidperiodo;
        private int icadena;

        private string sidcategoria;
        private int inegocio;

        private void UpdateProgressContext2()
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

                progress.CurrentOperationText = "Exh. Adicionales Canal AASS";

                if (!Response.IsClientConnected)
                {
                    //Cancel button was clicked or the browser was closed, so stop processing
                    break;
                }
                progress.Speed = i;
                //Stall the current thread for 0.1 seconds
                System.Threading.Thread.Sleep(100);


            }
        }

        private void _AsignarVariables()
        {
            iidcompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);


            sidaño = cmb_año.SelectedValue;
            sidmes = cmb_mes.SelectedValue;

            ///sidperiodo = cmb_periodo.SelectedValue;
            icadena = Convert.ToInt32(cmb_cadena.SelectedValue);


            sidcategoria = cmb_categoria.SelectedValue;
           // inegocio = Convert.ToInt32(cmb_negocio.SelectedValue);



            string sidperdil = this.Session["Perfilid"].ToString();
            if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue== "0" )
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                   
                    Periodo p = new Periodo(Report, icadena, sidcategoria,  canal, iidcompany, iservicio);

                    p.Set_PeriodoConDataValidada();

                    sidaño = p.Año;
                    sidmes = p.Mes;
                    sidperiodo = p.PeriodoId;

                    cmb_año.SelectedIndex = cmb_año.Items.FindItemIndexByValue(DateTime.Now.Year.ToString());
                    GetPeridForAnalist();

                }
                else if (sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteGeneral"])
                {
                    GetPeriodForClient();
                }
            }
            else
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    GetPeridForAnalist();
                }
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            MyAccordion.SelectedIndex = 1;
            TabContainer_filtros.ActiveTabIndex = 0;
            if (!Page.IsPostBack)
            {
                cargarCadena();
                cargarAños();
                
                cargarMes();
                cargarCategorias();
                cargaTipoExhibidor();
                cargarGaleriaFotos();
                _AsignarVariables();
                GenerarGestionExh();
                GenerarDetalleExhTipo();
                GenerarDetalleExh();
                GenerarCumplimientoLayout();
            }

        }
     

        protected void cargarCadena()
        {
            DataTable dt = null;
            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERCADENAS", iidcompany, sidcanal);

            if (dt.Rows.Count > 0)
            {
                cmb_cadena.DataSource = dt;
                cmb_cadena.DataValueField = "id_cadena";
                cmb_cadena.DataTextField = "Cadena";
                cmb_cadena.DataBind();

                cmb_cadena.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
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
                cmb_año.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
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
                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            }
        }

        //private void cargarCategorias()
        //{
        //    DataTable dtcatego = null;
        //    Report = Convert.ToInt32(this.Session["Reporte"]);
        //    dtcatego = Get_DataClientes.Get_Obtenerinfocombos_F(iidcompany, canal, Report, "0", 2);
        //    if (dtcatego.Rows.Count > 0)
        //    {

        //        cmb_categoria.DataSource = dtcatego;
        //        cmb_categoria.DataValueField = "cod_catego";
        //        cmb_categoria.DataTextField = "Name_Catego";
        //        cmb_categoria.DataBind();
        //        cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));

        //    }
        //    else
        //    {
        //        dtcatego = null;
        //    }
        //}
        private void cargarCategorias()
        {
            DataTable dtcatego = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
        
            canal = this.Session["Canal"].ToString().Trim();


            dtcatego = Get_DataClientes.Get_Obtenerinfocombos_F(iidcompany, canal, Report, "", 2);

            //dtcatego = Get_DataClientes.Get_Obtenerinfocombos(iidcompany, sidcanal, "", 2);
            if (dtcatego.Rows.Count > 0)
            {
                cmb_categoria.DataSource = dtcatego;
                cmb_categoria.DataValueField = "cod_catego";
                cmb_categoria.DataTextField = "Name_Catego";
                cmb_categoria.DataBind();
                cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));

            }
            else
            {
                dtcatego = null;
            }
        }
        private void cargaTipoExhibidor()
        {
            DataTable dtExhib = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_OBTENER_EXHIBIDORES");

            if (dtExhib.Rows.Count > 0)
            {
                cmb_tipoExhibicion.DataSource = dtExhib;
                cmb_tipoExhibicion.DataValueField = "idTipo";
                cmb_tipoExhibicion.DataTextField = "descripcion";
                cmb_tipoExhibicion.DataBind();
                cmb_tipoExhibicion.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

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
                
                string sidtipoExhib = cmb_tipoExhibicion.SelectedValue;
                int sidCadena =Convert.ToInt32(cmb_cadena.SelectedValue);
                string sidano = cmb_año.SelectedValue;
                string sidmes = cmb_mes.SelectedValue;
                string sidProductCategory=cmb_categoria.SelectedValue;
                if (sidmes == "")
                    sidmes = "0";

                crearXML(iidcompany, sidcanal, sidtipoExhib, sidCadena, sidProductCategory, sidano, sidmes);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        public void crearXML(int iidcompany, string sidcanal, string sidtipoExhibicion, int sidCadena, string sid_ProductCategory,string ano, string mes)
        {
            try
            {
                DataTable dt = null;

                string nameFile = "Data_Exibicion.xml";
                string SaveLocation = Server.MapPath(@"~\Pages\Modulos\Cliente\Reportes\Galeria_fotografica") + "\\" + nameFile;
                int iservicio = Convert.ToInt32(this.Session["Service"]);
                dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_REPORTE_TIPO_EXIBICIONES_ADICIONALES", iidcompany, iservicio, sidcanal, sidtipoExhibicion, sidCadena,sid_ProductCategory, ano, mes);


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
                        valor = dt.Rows[i]["id_NodeCommercial"].ToString();

                    if (i < dt.Rows.Count - 1)
                        valorSiguiente = dt.Rows[i + 1]["id_NodeCommercial"].ToString();

                    if (i < dt.Rows.Count - 2)
                        valorSubSiguiente = dt.Rows[i + 2]["id_NodeCommercial"].ToString();

                    if (valor != valorSiguiente)
                        flag = 1;
                    if ((valor == valorSiguiente && i < dt.Rows.Count) || (valor != valorSubSiguiente && cont == 0 && i < dt.Rows.Count) || (flag == 1))
                    {
                        string photoName = dt.Rows[i]["Id_regft"].ToString() + ".jpg";
                        byte[] byteArrayIn = (byte[])dt.Rows[i]["foto"];

                        Save_photo(photoName, byteArrayIn);
                        if (cont == 0)
                        {
                            string tituloAlbum = "Cadena:" + dt.Rows[i]["commercialNodeName"].ToString();
                            string descripcionAlbum = "Oficina:" + dt.Rows[i]["Name_Oficina"].ToString();
                            string imagenAlbum = "Galeria_fotografica/Fotos/" + photoName;

                            writer.WriteStartElement("album");
                            writer.WriteAttributeString("title", tituloAlbum);
                            writer.WriteAttributeString("description", descripcionAlbum);
                            writer.WriteAttributeString("image", imagenAlbum);
                        }

                        string tituloFoto = "Punto de Venta: " + dt.Rows[i]["pdv_Name"].ToString() + ", Categoria:" + dt.Rows[i]["Product_Category"].ToString() + ",  Tipo de Exhibidor:" + dt.Rows[i]["TipoExibicion_Descripcion"].ToString().ToUpper();
                        string descripcionFoto = "Comentario:" + dt.Rows[i]["Comentario"].ToString() + ",  Fecha de Toma:" + dt.Rows[i]["Fec_Reg_Bd"].ToString();
                        string imagenFoto = "Galeria_fotografica/Fotos/" + photoName;
                        string thumbnailFoto = "Galeria_fotografica/Fotos/" + photoName;

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

        protected void btngnerar_Click(object sender, EventArgs e)
        {
            TabContainer_filtros.ActiveTabIndex = 0;
            cargarGaleriaFotos();
            _AsignarVariables();
            GenerarGestionExh();
            GenerarDetalleExhTipo();
            GenerarDetalleExh();
            GenerarCumplimientoLayout();

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

            oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_cadena.SelectedValue);

            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            oReportes_parametros.Id_tipoReporte = cmb_tipoExhibicion.SelectedValue;


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



        protected void chkb_validar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkb_validar.Checked)
            {
                chkb_invalidar.Checked = false;
            }
            lbl_msj_validar.Text = "¿ Esta seguro que desea continuar?";
            btn_aceptar2.Visible = false;
            btn_aceptar.Visible = true;
            btn_cancelar.Visible = true;

            ModalPopupExtender_ValidationAnalyst.Show();
        }
        protected void chkb_invalidar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkb_invalidar.Checked)
            {
                chkb_validar.Checked = false;
            }
            lbl_msj_validar.Text = "¿ Esta seguro que desea continuar?";
            btn_aceptar2.Visible = false;
            btn_aceptar.Visible = true;
            btn_cancelar.Visible = true;

            ModalPopupExtender_ValidationAnalyst.Show();
        }
        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            btn_aceptar2.Visible = true;
            btn_aceptar.Visible = false;
            btn_cancelar.Visible = false;
            try
            {
                Report = Convert.ToInt32(this.Session["Reporte"]);
                oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_REPORT_PLANNING_ACTUALIZAR_VALIDACION", Report, lbl_año_value.Text.Trim(), lbl_mes_value.Text.Trim(), lbl_periodo_value.Text.Trim(), chkb_validar.Checked, Session["sUser"].ToString(), DateTime.Now);

                ModalPopupExtender_ValidationAnalyst.Show();
                lbl_msj_validar.Text = "El cambio se realizo con exito.";
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {

            if (chkb_validar.Checked == true)
            {
                chkb_validar.Checked = false;
                chkb_invalidar.Checked = true;
            }
            else
            {
                chkb_validar.Checked = true;
                chkb_invalidar.Checked = false;
            }
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


                //crearXML(iidcompany, sidcanal, lbl_id_tipoReporte.Text.Trim(), lbl_id_oficina.Text.Trim(), lbl_id_año.Text.Trim(), lbl_id_mes.Text.Trim());

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
    }
}