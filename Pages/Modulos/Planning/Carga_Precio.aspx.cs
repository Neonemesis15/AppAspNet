using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Data.SqlClient;
using Lucky.Business.Common.Application;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.Pages.Modulos.Planning
{
    public partial class Carga_Precio : System.Web.UI.Page
    {
         private Conexion oCoon = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    LblPresupuestoPDV.Text = this.Session["PresupuestoProductos"].ToString().Trim();
                    LblPlanning.Text = this.Session["id_planningProductos"].ToString().Trim();
                    Log(LblPlanning.Text);
                    llenarPeriodos();
                    llenarProductos();
                    llenarPtoVenta();
                   
                }
                catch
                {
                    Pmensaje.CssClass = "MensajesSupervisor";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "Es indispensable que cierre sesión e inicie nuevamente. Su sesión expiró.";
                    Mensajes_Usuario();
                }
            }
        }
        private void Mensajes_Usuario()
        {
            ModalPopupMensaje.Show();
        }

        protected void Log(string idPlannig)
        {

            // dt_rptstock = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_EXPORTAEXCEL_PRECIO_PLANNING", idPlannig);

            DataTable dtDGbyCatego = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_EXPORTAEXCEL_PRECIO_PLANNING", idPlannig);


            //dt_Log.Columns.Add("Código Punto de Venta");
            //dt_Log.Columns.Add("Nombre Punto de Venta");
            //dt_Log.Columns.Add("SKU");
            //dt_Log.Columns.Add("Nombre del Producto");
            //dt_Log.Columns.Add("Precio");

            if (ExportarExcelDataTable(dtDGbyCatego, Server.MapPath("PDV_Planning/FORMATO_PLANNIG_CARGAMASIVA_PRECIO.xls")))
            {
                //Label lbl_mensaje = (Label)Panel_popupmensaje.TemplateControl.FindControl("lbl_msj_popup");
                //lbl_msj_popup.Text = "";
                //string mensaje = "Sr Usuario: <br/> Se han presentado registros con errores en la información procesada, dichos errores se enviaron a su correo electronico.";
                //lbl_msj_popup.Text = mensaje;
                //ModalPopupExtender_mensaje.Show();
                //smailadmon = "AdministradorXplora@lucky.com.pe";

                //Se envia el correo con el Log generado Ing. Carlos Alberto Hernández Rincón 30/01/2011

                //System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                //System.Net.Mail.Attachment file = new System.Net.Mail.Attachment(path);//Adjunta el Log Generado
                //correo.From = new System.Net.Mail.MailAddress(smailadmon);

                //correo.To.Add(semail);
                //correo.Subject = "Log Stock" + " " + month + " " + year + " " + "Periodo:" + " " + speriodo;
                //correo.Attachments.Add(file);
                //correo.IsBodyHtml = false;
                //correo.Priority = System.Net.Mail.MailPriority.Low;
                //string[] txtbody = new string[] { "Sr Usuario: Se han presentado " + contador + " registros con errores de un total de " + dt_rptstock.Rows.Count + " de la información procesada" };

                //correo.Body = string.Concat(txtbody);

                //System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                //cliente.Host = "mail.lucky.com.pe";

            }

        }
        public Boolean ExportarExcelDataTable(DataTable dt, string RutaExcel)
        {
            try
            {
                const string FIELDSEPARATOR = "\t";
                const string ROWSEPARATOR = "\n";
                System.Text.StringBuilder output = new System.Text.StringBuilder();
                // Escribir encabezados    
                foreach (DataColumn dc in dt.Columns)
                {
                    output.Append(dc.ColumnName);
                    output.Append(FIELDSEPARATOR);
                }
                output.Append(ROWSEPARATOR);
                foreach (DataRow item in dt.Rows)
                {
                    foreach (object value in item.ItemArray)
                    {
                        output.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' ').Replace('.', ','));
                        output.Append(FIELDSEPARATOR);
                    }
                    // Escribir una línea de registro        
                    output.Append(ROWSEPARATOR);
                }
                // Valor de retorno    
                // output.ToString();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(RutaExcel);
                sw.Write(output.ToString());
                sw.Close();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }

        public void llenarPeriodos()
        {

            string a = Session["idPlanning"].ToString();
            string b = Session["id_report"].ToString();
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_OBTENERPERIODOS_xPlanningReporte", Session["idPlanning"].ToString(), Convert.ToInt32(Session["id_report"].ToString()));

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlPeriodo.DataSource = dt;
                    ddlPeriodo.DataValueField = "id_ReportsPlanning";
                    ddlPeriodo.DataTextField = "fecha";
                    ddlPeriodo.DataBind();
                }
            }
            dt = null;
            
        }

        public void llenarProductos()
        {
             string a = Session["idPlanning"].ToString();
            string b = Session["id_report"].ToString();
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_OBTENERPRODUCTOS_xPlanningReporte", Session["idPlanning"].ToString(), Convert.ToInt32(Session["id_report"].ToString()));

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlProducto.DataSource = dt;
                    ddlProducto.DataValueField = "id_ProductsPlanning";
                    ddlProducto.DataTextField = "Product_Name";
                    ddlProducto.DataBind();
                }
            }
            dt = null;  
        }


        public void llenarPtoVenta()
        {
            string a = Session["idPlanning"].ToString();
            string b = Session["id_report"].ToString();
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_OBTENERPTOVENTAS_xPlanning", Session["idPlanning"].ToString());

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlPtoVenta.DataSource = dt;
                    ddlPtoVenta.DataValueField = "id_MPOSPlanning";
                    ddlPtoVenta.DataTextField = "pdv_Name";
                    ddlPtoVenta.DataBind();
                }
            }
            dt = null;
        }
        

        protected void BtnCargaArchivo_Click(object sender, EventArgs e)
        {


            if ((FileUpPrecio.PostedFile != null) && (FileUpPrecio.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(FileUpPrecio.PostedFile.FileName);
                string SaveLocation = Server.MapPath("PDV_Planning") + "\\" + fn;

                if (SaveLocation != string.Empty)
                {
                    if (FileUpPrecio.FileName.ToLower().EndsWith(".xls"))
                    {
                        // string Destino = Server.MapPath(null) + "\\PDV_Planning\\" + Path.GetFileName(FileUpPDV.PostedFile.FileName);
                        OleDbConnection oConn1 = new OleDbConnection();
                        OleDbCommand oCmd = new OleDbCommand();
                        OleDbDataAdapter oDa = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();
                        DataTable dt = new DataTable();

                        FileUpPrecio.PostedFile.SaveAs(SaveLocation);

                        // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                        oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                        oConn1.Open();
                        oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Pla_Precio"];
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
                                int numcol = 5; //determina el número de columnas para el datatable
                                if (dt.Columns.Count == numcol)
                                {
                                    
                                    dt.Columns[0].ColumnName = "id_ClientPDV";
                                    dt.Columns[1].ColumnName = "pdv_Name";
                                    dt.Columns[2].ColumnName = "cod_Product";
                                    dt.Columns[3].ColumnName = "Product_Name";
                                    dt.Columns[4].ColumnName = "Precio";

                                    ConnectionStringSettings settingconection;
                                    settingconection = ConfigurationManager.ConnectionStrings["ConectaDBLucky"];
                                    string oSqlConnIN = settingconection.ConnectionString;

                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oSqlConnIN))
                                    {
                                        bulkCopy.DestinationTableName = "Products_Price_Planning_TMP";
                                        //carga los SKU's temporalmente para hacer el procedimiento a través de un SP
                                        bulkCopy.WriteToServer(dt);
                                    }
                                    

                                     string idPlanning = Session["idPlanning"].ToString();
                                     string id_ReportsPlanning = ddlPeriodo.SelectedValue;
                                    string usuario = this.Session["sUser"].ToString().Trim();

                                    //realiza las validaciones y carga los productos a planning.
                                    DataSet dsCargar = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CARGAMASIVA_PRECIO_PLANNING_TMP",
                                    idPlanning,
                                    Convert.ToInt32(id_ReportsPlanning),
                                    usuario, DateTime.Now,
                                    usuario, DateTime.Now);

                                    //for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                    //{
                                    //    string id_ClientPDV = dt.Rows[i]["id_ClientPDV"].ToString();
                                    //    string cod_Product = dt.Rows[i]["cod_Product"].ToString();
                                    //    string id_report = Session["id_report"].ToString();
                                    //    string idPlanning = Session["idPlanning"].ToString();
                                    //    string id_ReportsPlanning = ddlPeriodo.SelectedValue;
                                    //    string Precio = dt.Rows[i]["Precio"].ToString();

                                    //}
                                }
                            //}
                        }
                        catch
                        {
                            //
                        }
                    }
                }
            }
                                       

        }

        protected void RbtnSelTipoCarga_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (RbtnSelTipoCarga.Items[0].Selected)
            {
                OpcUnoAUno.Style.Value = "Display:block;";
                OpcMasiva.Style.Value = "Display:none;";
                td_download.Style.Value = "display=none";
            }
            if (RbtnSelTipoCarga.Items[1].Selected)
            {
                OpcUnoAUno.Style.Value = "Display:none;";
                OpcMasiva.Style.Value = "Display:block;";
                BtnCargaUnoaUno.Visible = false;
                td_download.Style.Value = "display=block";
            }
        }

        protected void BtnCargaUnoaUno_Click(object sender, EventArgs e)
        {

            string mensaje="";
            if (ddlPeriodo.SelectedValue == "0" || ddlProducto.SelectedValue == "0" || ddlPtoVenta.SelectedValue == "0" || txtPrecio.Text == "")
            {//genera mensaje de alerta al faltar ingresar compo requerido
                if (ddlPeriodo.SelectedValue == "0")
                {
                    mensaje = ". " + "Seleccione un perido.";
                }
                if (ddlProducto.SelectedValue == "0")
                {
                    mensaje = mensaje  + "Seleccione un perido.";
                }
                if (ddlPtoVenta.SelectedValue == "0")
                {
                    mensaje = mensaje  + "Seleccione un Pto de Venta.";
                }
                if (txtPrecio.Text == "")
                {
                    mensaje = mensaje  + "Ingrese un Precio.";
                }
                lblencabezado.Text = "INFORMACION";
                lblmensajegeneral.Text = mensaje;
                ImgMensaje.ImageUrl = "~/Pages/images/Mensajes/warning_blue.png";
                divMensaje.Style.Value="border-width:10px;border-style:solid;border-color:#53A2FF; height:169px;background-color:#9FCBFF";
                ModalPopupMensaje.Show();
                return;
            }

            try
            {

                string usuario = this.Session["sUser"].ToString().Trim();

                DataSet dsCargar = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_INSERTAR_PRECIO_PLANNING",
                                        Convert.ToInt32(ddlPeriodo.SelectedValue),
                                        Convert.ToInt32(ddlPtoVenta.SelectedValue),
                                        Convert.ToInt32(ddlProducto.SelectedValue),
                                        Convert.ToDouble(txtPrecio.Text),
                                        true,
                                        usuario, DateTime.Now,
                                        usuario, DateTime.Now);

                lblencabezado.Text = "INFORMACION";
                lblmensajegeneral.Text = "Se registro correctamente";
                 Pmensaje.Style.Value = "background-color: Green;";
                ModalPopupMensaje.Show();

            }
            catch 
            {
                lblencabezado.Text = "INFORMACION";
                lblmensajegeneral.Text = "ya existe ese registro";
                ImgMensaje.ImageUrl = "~/Pages/images/Mensajes/warning_blue.png";
                divMensaje.Style.Value = "border-width:10px;border-style:solid;border-color:#53A2FF; height:169px;background-color:#9FCBFF";
                ModalPopupMensaje.Show();

               // Response.Write("<script>prompt('" + mensaje + "');</script>");
            }
  
        }
    }
}