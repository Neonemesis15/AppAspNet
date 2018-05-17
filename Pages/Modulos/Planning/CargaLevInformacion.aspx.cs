﻿using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Lucky.Business.Common.Application;
using Lucky.Data;


namespace SIGE.Pages.Modulos.Planning
{
    public partial class CargaLevInformacion : System.Web.UI.Page
    {
        private Productos oProductos = new Productos();
        private Brand oBrand = new Brand();
        private BProduct_Family oProductFamily = new BProduct_Family();
        private Conexion oCoon = new Conexion();
        
        private Facade_Proceso_Planning.Facade_Proceso_Planning wsPlanning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    LblPresupuestoPDV.Text = this.Session["PresupuestoProductos"].ToString().Trim();
                    LblPlanning.Text = this.Session["id_planningProductos"].ToString().Trim();

                    if (this.Session["vista_final"].ToString().Trim() == "Producto")
                    {
                        formato_Producto.Visible = true;
                    }
                    if (this.Session["vista_final"].ToString().Trim() == "Marca")
                    {
                        formato_Marca.Visible = true;
                    }
                    if (this.Session["vista_final"].ToString().Trim() == "Familia")
                    {
                        formato_Familia.Visible = true;
                    }
                    if (this.Session["vista_final"].ToString().Trim() == "Categoria")
                    {
                        formato_Categoria.Visible = true;
                    }
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
      
        protected void BtnCargaArchivo_Click(object sender, EventArgs e)
        {
            string sTIPO_LEVANTAMIENTO;
            sTIPO_LEVANTAMIENTO = this.Session["vista_final"].ToString().Trim();

            #region levantamiento por productos
            if (sTIPO_LEVANTAMIENTO.Equals("Producto"))
            {
                if ((FileUpLevInformacion.PostedFile != null) && (FileUpLevInformacion.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpLevInformacion.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("PDV_Planning") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpLevInformacion.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();
                            DataTable dt = new DataTable();

                            FileUpLevInformacion.PostedFile.SaveAs(SaveLocation);

                            //oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            //oConn1.ConnectionString = ("Provider=Microsoft.ACE.OLEDB.12.0;" + ("Data Source=" + (SaveLocation + ";Extended Properties=\"Excel 12.0;HDR=YES\"")));
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Pla_Productos"];
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;

                            try
                            {
                                oDa.Fill(oDs);
                                dt = oDs.Tables[0];

                                if (dt.Columns.Count == 1)
                                {
                                    dt.Columns[0].ColumnName = "SKU_Producto";                                

                                    // Bulk Copy to SQL Server
                                    ConnectionStringSettings settingconection;
                                    settingconection = ConfigurationManager.ConnectionStrings["ConectaDBLucky"];
                                    string oSqlConnIN = settingconection.ConnectionString;

                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oSqlConnIN))
                                    {
                                        bulkCopy.DestinationTableName = "Products_Planning_TMP";
                                        //carga los SKU's temporalmente para hacer el procedimiento a través de un SP
                                        bulkCopy.WriteToServer(dt);
                                    }
                                                                        
                                    int id_company = Convert.ToInt32(this.Session["company_id"].ToString().Trim());
                                    string planning = LblPlanning.Text;
                                    int listinfprod = Convert.ToInt32(this.Session["RbtnListInfProd"].ToString().Trim());
                                    string usuario = this.Session["sUser"].ToString().Trim();

                                    //realiza las validaciones y carga los productos a planning.
                                    DataSet dsCargar = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CARGAMASIVA_PRODUCT_PLANNING_TMP",
                                    id_company,
                                    planning,
                                    listinfprod,
                                    usuario, DateTime.Now,
                                    usuario, DateTime.Now);

                                    #region codigo comentariado sirve para insertar sin bulkcopy 
                                    ////GvProductosPlanning.DataSource = dt;
                                    ////GvProductosPlanning.DataBind();
                                    //for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                    //{

                                    //    DataTable dtconsulta = oProductos.BuscarProductos(0, "0", 0, dt.Rows[i][0].ToString().Trim());
                                    //    if (dtconsulta != null)
                                    //    {
                                    //        if (dtconsulta.Rows.Count == 0)
                                    //        {
                                    //            Pmensaje.CssClass = "MensajesSupervisor";
                                    //            lblencabezado.Text = "Sr. Usuario";
                                    //            lblmensajegeneral.Text = "El SKU " + dt.Rows[i][0].ToString().Trim() + " No existe ";
                                    //            Mensajes_Usuario();
                                    //            sigue = false;
                                    //            i = dt.Rows.Count - 1;
                                    //        }
                                    //        else
                                    //        {
                                    //            if (dtconsulta.Rows[0][6].ToString().Trim() != this.Session["company_id"].ToString().Trim())
                                    //            {
                                    //                DataTable dtCompetencia = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENERCOMPETIDORESCLIENTE", Convert.ToInt32(this.Session["company_id"].ToString().Trim()));
                                    //                if (dtCompetencia != null)
                                    //                {
                                    //                    if (dtCompetencia.Rows.Count <= 0)
                                    //                    {
                                    //                        Pmensaje.CssClass = "MensajesSupervisor";
                                    //                        lblencabezado.Text = "Sr. Usuario";
                                    //                        lblmensajegeneral.Text = "El SKU " + dt.Rows[i][0].ToString().Trim() + " es de un competidor no válido para la campaña";
                                    //                        Mensajes_Usuario();
                                    //                        sigue = false;
                                    //                        i = dt.Rows.Count - 1;
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        sigue = false;
                                    //                        for (int j = 0; j <= dtCompetencia.Rows.Count - 1; j++)
                                    //                        {
                                    //                            if (dtconsulta.Rows[0][6].ToString().Trim() == dtCompetencia.Rows[j][2].ToString().Trim())
                                    //                            {
                                    //                                j = dtCompetencia.Rows.Count - 1;
                                    //                                sigue = true;
                                    //                            }
                                    //                        }
                                    //                        if (sigue == false)
                                    //                        {
                                    //                            Pmensaje.CssClass = "MensajesSupervisor";
                                    //                            lblencabezado.Text = "Sr. Usuario";
                                    //                            lblmensajegeneral.Text = "El SKU " + dt.Rows[i][0].ToString().Trim() + " es de un competidor no válido para la campaña";
                                    //                            Mensajes_Usuario();
                                    //                            sigue = false;
                                    //                            i = dt.Rows.Count - 1;
                                    //                        }

                                    //                    }

                                    //                }
                                    //            }
                                    //        }
                                    //    }

                                    //}

                                    //if (sigue)
                                    //{
                                    //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                    //    {
                                    //        string prueba = dt.Rows[i][0].ToString().Trim();

                                    //        DataTable dtconsulta = oProductos.BuscarProductos(0, "0", 0, dt.Rows[i][0].ToString().Trim());
                                    //        DataTable dtbrandysubbrand = wsPlanning.Get_Obtener_BrandySubbrandxProducto(Convert.ToInt32(dtconsulta.Rows[0][0].ToString().Trim()));
                                    //        if (dtbrandysubbrand != null)
                                    //        {
                                    //            if (dtbrandysubbrand.Rows.Count > 0)
                                    //            {
                                    //                this.Session["brand"] = dtbrandysubbrand.Rows[0]["id_Brand"].ToString().Trim();
                                    //                this.Session["sub_brand"] = dtbrandysubbrand.Rows[0]["id_SubBrand"].ToString().Trim();
                                    //            }
                                    //        }
                                    //        DPlanning PermitirGuardar = new DPlanning();
                                    //        DataTable dtpermitir = PermitirGuardar.Permitir_GuardarLevantamiento(sTIPO_LEVANTAMIENTO, Convert.ToInt64(dtconsulta.Rows[0][0].ToString().Trim()), dtconsulta.Rows[0][7].ToString().Trim(), Convert.ToInt32(this.Session["brand"].ToString().Trim()), null);
                                    //        if (dtpermitir != null)
                                    //        {
                                    //            if (dtpermitir.Rows[0]["CONTINUAR"].ToString().Trim() == "CONTINUAR")
                                    //            {
                                    //                sigue = true;
                                    //            }
                                    //            else
                                    //            {
                                    //                Pmensaje.CssClass = "MensajesSupervisor";
                                    //                lblencabezado.Text = "Sr. Usuario";
                                    //                lblmensajegeneral.Text = "El producto " + dt.Rows[i][0].ToString().Trim() + " No se permite almacenar por falta de información en Categorías , Marcas y Productos. Consulte con el Administrador Xplora";
                                    //                sigue = false;
                                    //                i = dt.Rows.Count - 1;
                                    //                Mensajes_Usuario();
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    //if (sigue)
                                    //{
                                    //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                    //    {
                                    //        try
                                    //        {
                                    //            string prueba = dt.Rows[i][0].ToString().Trim();

                                    //            DataTable dtconsulta = oProductos.BuscarProductos(0, "0", 0, dt.Rows[i][0].ToString().Trim());

                                    //            //ejecutar metodo para consultar datos de producto 
                                    //            DataTable dtbrandysubbrand = wsPlanning.Get_Obtener_BrandySubbrandxProducto(Convert.ToInt32(dtconsulta.Rows[0][0].ToString().Trim()));
                                    //            if (dtbrandysubbrand != null)
                                    //            {
                                    //                if (dtbrandysubbrand.Rows.Count > 0)
                                    //                    {
                                    //                    this.Session["brand"] = dtbrandysubbrand.Rows[0]["id_Brand"].ToString().Trim();
                                    //                    this.Session["sub_brand"] = dtbrandysubbrand.Rows[0]["id_SubBrand"].ToString().Trim();
                                    //                }
                                    //            }
                                    //            DAplicacion ValidaDuplicidad = new DAplicacion();
                                    //            DataTable dtDuplicados = ValidaDuplicidad.ConsultaDuplicados(ConfigurationManager.AppSettings["Product_planning"], LblPlanning.Text, dtconsulta.Rows[0][0].ToString().Trim(), null);
                                    //            if (dtDuplicados == null)
                                    //            {
                                    //                wsPlanning.Get_Regitration_ProductosPlanning(LblPlanning.Text, Convert.ToInt64(dtconsulta.Rows[0][0].ToString().Trim()), dtconsulta.Rows[0][7].ToString().Trim(), Convert.ToInt32(this.Session["brand"].ToString().Trim()), this.Session["sub_brand"].ToString().Trim(), "", "", 0, Convert.ToInt32(this.Session["RbtnListInfProd"].ToString().Trim()), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                    //                DPlanning Dplanning = new DPlanning();



                                    //                try
                                    //                {
                                    //                    if (dtconsulta.Rows[0][6].ToString().Trim() == this.Session["company_id"].ToString().Trim())
                                    //                    {
                                    //                        this.Session["TipoProducto"] = "P";
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        this.Session["TipoProducto"] = "C";
                                    //                    }

                                    //                    Dplanning.Registrar_TBL_EQUIPO_PRODUCTOS(this.Session["TipoProducto"].ToString().Trim());

                                    //                    DataTable dtRegsitrarTBLPROD = Dplanning.Registrar_TBL_PRODUCTO_CADENA("1", this.Session["TipoProducto"].ToString().Trim(),
                                    //                        Convert.ToInt64(dtconsulta.Rows[0][0].ToString().Trim()), LblPlanning.Text, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");

                                    //                    if (dtRegsitrarTBLPROD != null)
                                    //                    {
                                    //                        if (dtRegsitrarTBLPROD.Rows.Count > 0)
                                    //                        {
                                    //                            for (int ir = 0; ir <= dtRegsitrarTBLPROD.Rows.Count - 1; ir++)
                                    //                            {
                                    //                                try
                                    //                                {
                                    //                                    DataTable dtRegsitrarTBLPRODUCTOS = Dplanning.Registrar_TBL_PRODUCTO_CADENA("2", this.Session["TipoProducto"].ToString().Trim(),
                                    //                                        Convert.ToInt64(dtconsulta.Rows[0][0].ToString().Trim()), LblPlanning.Text,
                                    //                                        dtRegsitrarTBLPROD.Rows[ir][0].ToString().Trim(),
                                    //                                     dtRegsitrarTBLPROD.Rows[ir][1].ToString().Trim(),
                                    //                                     dtRegsitrarTBLPROD.Rows[ir][2].ToString().Trim(),
                                    //                                     dtRegsitrarTBLPROD.Rows[ir][3].ToString().Trim(),
                                    //                                     dtRegsitrarTBLPROD.Rows[ir][4].ToString().Trim(),
                                    //                                     dtRegsitrarTBLPROD.Rows[ir][5].ToString().Trim(),
                                    //                                     dtRegsitrarTBLPROD.Rows[ir][6].ToString().Trim(),
                                    //                                     dtRegsitrarTBLPROD.Rows[ir][7].ToString().Trim(),
                                    //                                     dtRegsitrarTBLPROD.Rows[ir][8].ToString().Trim(),
                                    //                                     dtRegsitrarTBLPROD.Rows[ir][9].ToString().Trim());
                                    //                                }
                                    //                                catch (Exception ex)
                                    //                                {
                                    //                                    // no inserta en tbl_producto_cadena nada porq ya existe en esa tabla
                                    //                                }
                                    //                            }
                                    //                        }
                                    //                    }
                                    //                }
                                    //                catch
                                    //                {

                                    //                }
                                    //            }

                                    //        }


                                    //        catch (Exception ex)
                                    //        {
                                    //            string error = "";
                                    //            string mensaje = "";
                                    //            error = Convert.ToString(ex.Message);
                                    //            mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                    //            if (error == mensaje)
                                    //            {
                                    //                Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                    //                string errMessage = "";
                                    //                errMessage = mensaje;
                                    //                errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                    //                this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                    //            }
                                    //            else
                                    //            {
                                    //                this.Session.Abandon();
                                    //                Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                    //            }
                                    //        }

                                    //    }
                                    //    Pmensaje.CssClass = "MensajesSupConfirm";
                                    //    lblencabezado.Text = "Sr. Usuario";
                                    //    lblmensajegeneral.Text = "Se ha creado con éxito los productos para la campaña : " + LblPresupuestoPDV.Text.ToUpper();

                                    //    Mensajes_Usuario();



                                    //}
                                    #endregion

                                    Pmensaje.CssClass = "MensajesSupConfirm";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "Se ha realizado la carga de productos para la campaña: " + LblPresupuestoPDV.Text.ToUpper() + " Revisar el log enviado a su correo ";

                                    Mensajes_Usuario();

                                    if (dsCargar.Tables[0].Rows.Count > 0 )
                                    {
                                        string fnn = "Log_ArchivoProductos.xls";
                                        string pathlog = Server.MapPath("PDV_Planning") + "\\" + fnn;
                                        //genera un archivo Excel con los campos
                                        if (ExportarExcelDataTable(dsCargar.Tables[0], pathlog))
                                        {
                                            //Se envia el correo con el Log generado 

                                            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                            System.Net.Mail.Attachment file = new System.Net.Mail.Attachment(pathlog);//Adjunta el Log Generado
                                            correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                            correo.To.Add(this.Session["smail"].ToString());
                                            correo.Subject = "Log de registro de carga masiva de productos para la campaña";
                                            correo.Attachments.Add(file);
                                            correo.IsBodyHtml = true;
                                            correo.Priority = System.Net.Mail.MailPriority.Low;
                                            string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                            this.Session["nameuser"].ToString() + "<br/>" + "<br/>" + 
                                            "El archivo de carga masiva de productos que usted seleccionó para la carga, generó el siguiente registro." + "<br/>" +
                                            "<br/>" +  "<br/>" +                                           
                                            "Por favor verifique el archivo adjunto a este correo, el cual contiene información importante para usted." + "<br/>" +
                                            "<br/>" +  "<br/>" +     
                                            "En dicho archivo se le indica qué productos fueron cargados con éxito, cuáles ya existían y no fue necesario cargar nuevamente, cuáles corresponden a un competidor no válido para el cliente de la campaña y cuáles estan pendientes debido a que no están creados en el maestro de productos de Xplora." +
                                            "<br/>" +  "<br/>" + 
                                            "Cualquier inquietud que pueda tener consultarlo con el Administrador Xplora" +
                                            "<br/>" +  "<br/>" + 
                                            "<br/>" +  "<br/>" + 
                                            "Cordial Saludo." + "<br/>"  };

                                            correo.Body = string.Concat(txtbody);

                                            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                            cliente.Host = "mail.lucky.com.pe";

                                            try
                                            {
                                                cliente.Send(correo);
                                                dt = null;
                                            }
                                            catch (System.Net.Mail.SmtpException)
                                            {
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // GvProductosPlanning.DataBind();
                                    Pmensaje.CssClass = "MensajesSupervisor";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de productos válido. Por favor verifique la estructura que fue enviada a su correo.";

                                    Mensajes_Usuario();

                                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                    correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                    correo.To.Add(this.Session["smail"].ToString());
                                    correo.Subject = "Errores en archivo de creación de Productos";
                                    correo.IsBodyHtml = true;
                                    correo.Priority = System.Net.Mail.MailPriority.Normal;
                                    string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                    this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +
                                    "El archivo que usted seleccionó para la carga de productos para la campaña no cumple con una estructura válida." + "<br/>" +
                                    "Por favor verifique que tenga 1 columna" + "<br/>" +  "<br/>" +
                                    "Sugerencia : identifique la columna de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                    "Columna 1  : SKU_Producto" + "<br/>" +                                                                                    
                                    "Nota:  No es indispensable que la columna se identifiquen de la misma manera como se describió anteriormente, usted puede personalizarlo a su gusto." +
                                    "Pero tenga en cuenta que debe ingresar la información de los Productos en la columna 1 ." + "<br/>" + "<br/>" + "<br/>" +
                                    "Cordial Saludo." + "<br/>" + "Administrador Xplora." };

                                    correo.Body = string.Concat(txtbody);

                                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                    cliente.Host = "mail.lucky.com.pe";

                                    try
                                    {
                                        cliente.Send(correo);
                                    }
                                    catch (System.Net.Mail.SmtpException)
                                    {
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] =
                                Pmensaje.CssClass = "MensajesSupervisor";
                                lblencabezado.Text = "Sr. Usuario";
                                lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de Productos válido. Por favor verifique que el nombre de la hoja sea Productos_Planning";

                                Mensajes_Usuario();
                            }
                            oConn1.Close();
                        }
                        else
                        {
                            Pmensaje.CssClass = "MensajesSupervisor";
                            lblencabezado.Text = "Sr. Usuario";
                            lblmensajegeneral.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            Mensajes_Usuario();
                        }
                    }
                }
                else
                {
                    Pmensaje.CssClass = "MensajesSupervisor";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "Es indispensable seleccionar un archivo.";
                    Mensajes_Usuario();
                }
            }
            #endregion

            #region levantamiento por marca
            if (sTIPO_LEVANTAMIENTO== "Marca")
            {
                if ((FileUpLevInformacion.PostedFile != null) && (FileUpLevInformacion.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpLevInformacion.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("PDV_Planning") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpLevInformacion.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();

                            DataTable dt = new DataTable();
                            
                            FileUpLevInformacion.PostedFile.SaveAs(SaveLocation);

                            //oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            //oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Pla_Marcas"];
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            try
                            {
                                oDa.Fill(oDs);
                                dt = oDs.Tables[0];
                                if (dt.Columns.Count == 2)
                                {
                                    dt.Columns[0].ColumnName = "Categoria";
                                    dt.Columns[0].ColumnName = "Marca";

                                    // Bulk Copy to SQL Server
                                    ConnectionStringSettings settingconection;
                                    settingconection = ConfigurationManager.ConnectionStrings["ConectaDBLucky"];
                                    string oSqlConnIN = settingconection.ConnectionString;

                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oSqlConnIN))
                                    {
                                        bulkCopy.DestinationTableName = "PLA_Brand_Planning_TMP";
                                        bulkCopy.WriteToServer(dt);
                                    }

                                    DataSet dsCargar = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CARGAMASIVA_BRAND_PLANNING",
                                        Convert.ToInt32(this.Session["company_id"].ToString().Trim()),
                                        LblPlanning.Text,
                                        Convert.ToInt32(this.Session["RbtnListInfProd"].ToString().Trim()),
                                         this.Session["sUser"].ToString().Trim(), DateTime.Now,
                                         this.Session["sUser"].ToString().Trim(), DateTime.Now);

                                    #region codigo comentariado sirve para insertar sin bulkcopy
                                    //GvProductosPlanning.DataSource = dt;
                                    //GvProductosPlanning.DataBind();
                                    //bool sigue = true;
                                    //for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                    //{

                                    //    DataTable dtconsulta = oBrand.ObtenerDatosMarca(dt.Rows[i][0].ToString().Trim(), dt.Rows[i][1].ToString().Trim());
                                    //    if (dtconsulta != null)
                                    //    {
                                    //        if (dtconsulta.Rows.Count <= 0)
                                    //        {
                                    //            Pmensaje.CssClass = "MensajesSupervisor";
                                    //            lblencabezado.Text = "Sr. Usuario";
                                    //            lblmensajegeneral.Text = "La Marca " + dt.Rows[i][1].ToString().Trim() + " No existe ";
                                    //            Mensajes_Usuario();
                                    //            sigue = false;
                                    //            i = dt.Rows.Count - 1;
                                    //        }
                                    //        else
                                    //        {
                                    //            if (dtconsulta.Rows[0][1].ToString().Trim() != this.Session["company_id"].ToString().Trim())
                                    //            {
                                    //                DataTable dtCompetencia = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENERCOMPETIDORESCLIENTE", Convert.ToInt32(this.Session["company_id"].ToString().Trim()));
                                    //                if (dtCompetencia != null)
                                    //                {
                                    //                    if (dtCompetencia.Rows.Count <= 0)
                                    //                    {
                                    //                        Pmensaje.CssClass = "MensajesSupervisor";
                                    //                        lblencabezado.Text = "Sr. Usuario";
                                    //                        lblmensajegeneral.Text = "La marca " + dt.Rows[i][1].ToString().Trim() + " es de un competidor no válido para la campaña";
                                    //                        Mensajes_Usuario();
                                    //                        sigue = false;
                                    //                        i = dt.Rows.Count - 1;
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        sigue = false;
                                    //                        for (int j = 0; j <= dtCompetencia.Rows.Count - 1; j++)
                                    //                        {
                                    //                            if (dtconsulta.Rows[0][1].ToString().Trim() == dtCompetencia.Rows[j][2].ToString().Trim())
                                    //                            {
                                    //                                j = dtCompetencia.Rows.Count - 1;
                                    //                                sigue = true;
                                    //                            }
                                    //                        }
                                    //                        if (sigue == false)
                                    //                        {
                                    //                            Pmensaje.CssClass = "MensajesSupervisor";
                                    //                            lblencabezado.Text = "Sr. Usuario";
                                    //                            lblmensajegeneral.Text = "La marca " + dt.Rows[i][1].ToString().Trim() + " es de un competidor no válido para la campaña";
                                    //                            Mensajes_Usuario();
                                    //                            sigue = false;
                                    //                            i = dt.Rows.Count - 1;
                                    //                        }

                                    //                    }

                                    //                }
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    //if (sigue)
                                    //{
                                    //    for (int i = 0; i <= GvProductosPlanning.Rows.Count - 1; i++)
                                    //    {
                                    //        try
                                    //        {

                                    //            //Ejecutar Método para almacenar las marcas seleccionados para el planning. Ing. Mauricio Ortiz  
                                    //            DataTable dtconsulta = oBrand.ObtenerDatosMarca(dt.Rows[i][0].ToString().Trim(), dt.Rows[i][1].ToString().Trim());
                                    //            DAplicacion ValidaDuplicidad = new DAplicacion();

                                    //            DataTable dtDatos = ValidaDuplicidad.ConsultaDuplicados(ConfigurationManager.AppSettings["PLA_Brand_Planning"], LblPlanning.Text, dtconsulta.Rows[0][1].ToString().Trim(), null);
                                    //            if (dtDatos == null)
                                    //            {
                                    //                wsPlanning.Get_Registrar_MarcasPlanning(LblPlanning.Text, dtconsulta.Rows[0][2].ToString().Trim(), Convert.ToInt32(dtconsulta.Rows[0][0].ToString().Trim()), Convert.ToInt32(this.Session["RbtnListInfProd"].ToString().Trim()), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                    //                DPlanning Dplanning = new DPlanning();
                                    //                try
                                    //                {
                                    //                    Dplanning.Registrar_TBL_EQUIPO_MARCAS();
                                    //                }
                                    //                catch (Exception ex)
                                    //                {

                                    //                }
                                    //            }

                                    //        }
                                    //        catch (Exception ex)
                                    //        {
                                    //            string error = "";
                                    //            string mensaje = "";
                                    //            error = Convert.ToString(ex.Message);
                                    //            mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                    //            if (error == mensaje)
                                    //            {
                                    //                Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                    //                string errMessage = "";
                                    //                errMessage = mensaje;
                                    //                errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                    //                this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                    //            }
                                    //            else
                                    //            {
                                    //                this.Session.Abandon();
                                    //                Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                    //            }
                                    //        }

                                    //    }

                                    //}
                                    #endregion

                                    Pmensaje.CssClass = "MensajesSupConfirm";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "Se ha realizado la carga de marcas para la campaña : " + LblPresupuestoPDV.Text.ToUpper() + " Revisar el log enviado a su correo ";

                                    Mensajes_Usuario();

                                    if (dsCargar.Tables[0].Rows.Count > 0)
                                    {
                                        string fnn = "Log_ArchivoMarcas.xls";
                                        string pathlog = Server.MapPath("PDV_Planning") + "\\" + fnn;
                                        if (ExportarExcelDataTable(dsCargar.Tables[0], pathlog))
                                        {
                                            //Se envia el correo con el Log generado 

                                            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                            System.Net.Mail.Attachment file = new System.Net.Mail.Attachment(pathlog);//Adjunta el Log Generado
                                            correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                            correo.To.Add(this.Session["smail"].ToString());
                                            correo.Subject = "Log de registro de carga masiva de marcas para la campaña";
                                            correo.Attachments.Add(file);
                                            correo.IsBodyHtml = true;
                                            correo.Priority = System.Net.Mail.MailPriority.Low;
                                            string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                                        this.Session["nameuser"].ToString() + "<br/>" + "<br/>" + 
                                                        "El archivo de carga masiva de marcas que usted seleccionó para la carga Generó el siguiente registro ." + "<br/>" +
                                                         "<br/>" +  "<br/>" +                                           
                                                        "Por favor verifique el archivo adjunto a este correo , el cual contiene información importante para usted" + "<br/>" +                                                                                    
                                                        "<br/>" +  "<br/>" +     
                                                        "En Dicho archivo se le indica que marcas fueron cargadas con éxito , cuales ya existian y no fue necesario cargar nuevamente, cuales marcas corresponden a un competidor no valido para el cliente de la campaña y cuales estan pendientes por motivo que no están creados en el maestro de marcas de Xplora" +
                                                        "<br/>" +  "<br/>" + 
                                                        "Cualquier inquietud que pueda tener consultarlo con el Administrador Xplora" +
                                                        "<br/>" +  "<br/>" + 
                                                        "<br/>" +  "<br/>" + 
                                                        "Cordial Saludo" + "<br/>"  };

                                            correo.Body = string.Concat(txtbody);

                                            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                            cliente.Host = "mail.lucky.com.pe";

                                            try
                                            {
                                                cliente.Send(correo);
                                                dt = null;
                                            }
                                            catch (System.Net.Mail.SmtpException)
                                            {
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //GvProductosPlanning.DataBind();
                                    Pmensaje.CssClass = "MensajesSupervisor";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de Marcas válido. Por favor verifique la estructura que fue enviada a su correo.";

                                    Mensajes_Usuario();

                                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                    correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                    correo.To.Add(this.Session["smail"].ToString());
                                    correo.Subject = "Errores en archivo de creación de Marcas";
                                    correo.IsBodyHtml = true;
                                    correo.Priority = System.Net.Mail.MailPriority.Normal;
                                    string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                            this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +
                                            "El archivo que usted seleccionó para la carga de Marcas para la campaña no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga 2 columnas" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique la columna de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                            "Columna 1  : Categoria" + "<br/>" +  
                                            "Columna 1  : Marcas" + "<br/>" +                                                                                    
                                            "Nota:  No es indispensable que la columna se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizarlo a su gusto ." +
                                            "Pero tenga en cuenta el orden especificado ." + "<br/>" + "<br/>" + "<br/>" +
                                            "Cordial Saludo" + "<br/>" + "Administrador Xplora" };

                                    correo.Body = string.Concat(txtbody);

                                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                    cliente.Host = "mail.lucky.com.pe";

                                    try
                                    {
                                        cliente.Send(correo);
                                    }
                                    catch (System.Net.Mail.SmtpException)
                                    {
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] =
                                Pmensaje.CssClass = "MensajesSupervisor";
                                lblencabezado.Text = "Sr. Usuario";
                                lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de Marcas válido. Por favor verifique que el nombre de la hoja sea Marcas_Planning";
                                Mensajes_Usuario();
                            }
                            oConn1.Close();
                        }
                        else
                        {
                            Pmensaje.CssClass = "MensajesSupervisor";
                            lblencabezado.Text = "Sr. Usuario";
                            lblmensajegeneral.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            Mensajes_Usuario();
                        }
                    }
                }
                else
                {
                    Pmensaje.CssClass = "MensajesSupervisor";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "Es indispensable seleccionar un archivo.";
                    Mensajes_Usuario();
                }
            }
            #endregion

            #region levantamiento por familia
            if (sTIPO_LEVANTAMIENTO == "Familia")
            {
                if ((FileUpLevInformacion.PostedFile != null) && (FileUpLevInformacion.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpLevInformacion.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("PDV_Planning") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpLevInformacion.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();

                            DataTable dt = new DataTable();


                            FileUpLevInformacion.PostedFile.SaveAs(SaveLocation);

                            // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            //oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Pla_Familias"];
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            try
                            {

                                oDa.Fill(oDs);
                                dt = oDs.Tables[0];
                                if (dt.Columns.Count == 1)
                                {

                                    dt.Columns[0].ColumnName = "Familia";

                                    // Bulk Copy to SQL Server
                                    ConnectionStringSettings settingconection;
                                    settingconection =
                                          ConfigurationManager.ConnectionStrings["ConectaDBLucky"];
                                    string oSqlConnIN = settingconection.ConnectionString;

                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oSqlConnIN))
                                    {
                                        bulkCopy.DestinationTableName = "PLA_Family_Planning_TMP";
                                        bulkCopy.WriteToServer(dt);
                                    }

                                    
                                    ///cambiar por metodo de familias
                                    DataSet dsCargar = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CARGAMASIVA_FAMILY_PLANNING",
                                        Convert.ToInt32(this.Session["company_id"].ToString().Trim()),
                                        LblPlanning.Text,
                                        Convert.ToInt32(this.Session["RbtnListInfProd"].ToString().Trim()),
                                         this.Session["sUser"].ToString().Trim(), DateTime.Now,
                                         this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                    
                                    #region codigo comentariado sirve para insertar sin bulkcopy
                                    //GvProductosPlanning.DataSource = dt;
                                    //GvProductosPlanning.DataBind();
                                    //bool sigue = true;
                                    //for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                    //{

                                    //    DataTable dtconsulta = oProductFamily.ObtenerDatosFamilia(dt.Rows[i][0].ToString().Trim());
                                    //    if (dtconsulta != null)
                                    //    {
                                    //        if (dtconsulta.Rows.Count <= 0)
                                    //        {
                                    //            Pmensaje.CssClass = "MensajesSupervisor";
                                    //            lblencabezado.Text = "Sr. Usuario";
                                    //            lblmensajegeneral.Text = "La Familia " + dt.Rows[i][0].ToString().Trim() + " No existe ";
                                    //            Mensajes_Usuario();
                                    //            sigue = false;
                                    //            i = dt.Rows.Count - 1;
                                    //        }
                                    //        else
                                    //        {
                                    //            if (dtconsulta.Rows[0][13].ToString().Trim() != this.Session["company_id"].ToString().Trim())
                                    //            {
                                    //                DataTable dtCompetencia = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENERCOMPETIDORESCLIENTE", Convert.ToInt32(this.Session["company_id"].ToString().Trim()));
                                    //                if (dtCompetencia != null)
                                    //                {
                                    //                    if (dtCompetencia.Rows.Count <= 0)
                                    //                    {
                                    //                        Pmensaje.CssClass = "MensajesSupervisor";
                                    //                        lblencabezado.Text = "Sr. Usuario";
                                    //                        lblmensajegeneral.Text = "La familia " + dt.Rows[i][0].ToString().Trim() + " es de un competidor no válido para la campaña";
                                    //                        Mensajes_Usuario();
                                    //                        sigue = false;
                                    //                        i = dt.Rows.Count - 1;
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        sigue = false;
                                    //                        for (int j = 0; j <= dtCompetencia.Rows.Count - 1; j++)
                                    //                        {
                                    //                            if (dtconsulta.Rows[0][13].ToString().Trim() == dtCompetencia.Rows[j][2].ToString().Trim())
                                    //                            {
                                    //                                j = dtCompetencia.Rows.Count - 1;
                                    //                                sigue = true;
                                    //                            }
                                    //                        }
                                    //                        if (sigue == false)
                                    //                        {
                                    //                            Pmensaje.CssClass = "MensajesSupervisor";
                                    //                            lblencabezado.Text = "Sr. Usuario";
                                    //                            lblmensajegeneral.Text = "La Familia " + dt.Rows[i][0].ToString().Trim() + " es de un competidor no válido para la campaña";
                                    //                            Mensajes_Usuario();
                                    //                            sigue = false;
                                    //                            i = dt.Rows.Count - 1;
                                    //                        }

                                    //                    }

                                    //                }
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    //if (sigue)
                                    //{
                                    //    for (int i = 0; i <= GvProductosPlanning.Rows.Count - 1; i++)
                                    //    {
                                    //        try
                                    //        {
                                    //            DataTable dtconsulta = oProductFamily.ObtenerDatosFamilia(dt.Rows[i][0].ToString().Trim());
                                    //            DAplicacion ValidaDuplicidad = new DAplicacion();
                                    //            DataTable dtduplicadoFamilia = ValidaDuplicidad.ConsultaDuplicados(ConfigurationManager.AppSettings["PLA_Family_Planning"], LblPlanning.Text, dtconsulta.Rows[0][0].ToString().Trim(), this.Session["RbtnListInfProd"].ToString().Trim());
                                    //            if (dtduplicadoFamilia == null)
                                    //            {
                                    //                wsPlanning.Get_Registrar_FamiliasPlanning(LblPlanning.Text, dtconsulta.Rows[0][1].ToString().Trim(), Convert.ToInt32(dtconsulta.Rows[0][3].ToString().Trim()), dtconsulta.Rows[0][0].ToString().Trim(), Convert.ToInt32(this.Session["RbtnListInfProd"].ToString().Trim()), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                    //                DPlanning Dplanning = new DPlanning();
                                    //                try
                                    //                {
                                    //                    DataTable dtRegsitrarTBL_EQUIPO_FAMILIAS = Dplanning.Registrar_TBL_EQUIPO_FAMILIAS();
                                    //                }
                                    //                catch (Exception ex)
                                    //                {

                                    //                }                                                   
                                    //            }

                                    //        }
                                    //        catch (Exception ex)
                                    //        {
                                    //            string error = "";
                                    //            string mensaje = "";
                                    //            error = Convert.ToString(ex.Message);
                                    //            mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                    //            if (error == mensaje)
                                    //            {
                                    //                Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                    //                string errMessage = "";
                                    //                errMessage = mensaje;
                                    //                errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                    //                this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                    //            }
                                    //            else
                                    //            {
                                    //                this.Session.Abandon();
                                    //                Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                    //            }
                                    //        }

                                    //    }                                    
                                    //}
                                    #endregion
                                    Pmensaje.CssClass = "MensajesSupConfirm";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "Se ha realizado la carga de familias para la campaña : " + LblPresupuestoPDV.Text.ToUpper() + " Revisar el log enviado a su correo ";

                                    Mensajes_Usuario();

                                    if (dsCargar.Tables[0].Rows.Count > 0)
                                    {
                                        string fnn = "Log_ArchivoFamilias.xls";
                                        string pathlog = Server.MapPath("PDV_Planning") + "\\" + fnn;
                                        if (ExportarExcelDataTable(dsCargar.Tables[0], pathlog))
                                        {
                                            //Se envia el correo con el Log generado 

                                            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                            System.Net.Mail.Attachment file = new System.Net.Mail.Attachment(pathlog);//Adjunta el Log Generado
                                            correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                            correo.To.Add(this.Session["smail"].ToString());
                                            correo.Subject = "Log de registro de carga masiva de familias para la campaña";
                                            correo.Attachments.Add(file);
                                            correo.IsBodyHtml = true;
                                            correo.Priority = System.Net.Mail.MailPriority.Low;
                                            string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                                        this.Session["nameuser"].ToString() + "<br/>" + "<br/>" + 
                                                        "El archivo de carga masiva de marcas que usted seleccionó para la carga Generó el siguiente registro ." + "<br/>" +
                                                         "<br/>" +  "<br/>" +                                           
                                                        "Por favor verifique el archivo adjunto a este correo , el cual contiene información importante para usted" + "<br/>" +                                                                                    
                                                        "<br/>" +  "<br/>" +     
                                                        "En Dicho archivo se le indica que marcas fueron cargadas con éxito , cuales ya existian y no fue necesario cargar nuevamente, cuales marcas corresponden a un competidor no valido para el cliente de la campaña y cuales estan pendientes por motivo que no están creados en el maestro de marcas de Xplora" +
                                                        "<br/>" +  "<br/>" + 
                                                        "Cualquier inquietud que pueda tener consultarlo con el Administrador Xplora" +
                                                        "<br/>" +  "<br/>" + 
                                                        "<br/>" +  "<br/>" + 
                                                        "Cordial Saludo" + "<br/>"  };

                                            correo.Body = string.Concat(txtbody);

                                            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                            cliente.Host = "mail.lucky.com.pe";

                                            try
                                            {
                                                cliente.Send(correo);
                                                dt = null;
                                            }
                                            catch (System.Net.Mail.SmtpException)
                                            {
                                            }
                                        }
                                    }
                                }
                                else
                                {

                                    //GvProductosPlanning.DataBind();


                                    Pmensaje.CssClass = "MensajesSupervisor";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de Familias válido. Por favor verifique la estructura que fue enviada a su correo.";


                                    Mensajes_Usuario();

                                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                    correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                    correo.To.Add(this.Session["smail"].ToString());
                                    correo.Subject = "Errores en archivo de creación de Familias";
                                    correo.IsBodyHtml = true;
                                    correo.Priority = System.Net.Mail.MailPriority.Normal;
                                    string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                            this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                   
                                             

                                            "El archivo que usted seleccionó para la carga de Familias para la campaña no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga 1 columna" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique la columna de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                            "Columna 1  : Familias" + "<br/>" +                                                                                    
                                            "Nota:  No es indispensable que la columna se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizarlo a su gusto ." +
                                            "Pero tenga en cuenta que debe ingresar la información de las Familias en la columna 1 ." + "<br/>" + "<br/>" + "<br/>" +
                                            "Cordial Saludo" + "<br/>" + "Administrador Xplora" };

                                    correo.Body = string.Concat(txtbody);

                                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                    cliente.Host = "mail.lucky.com.pe";

                                    try
                                    {
                                        cliente.Send(correo);
                                    }
                                    catch (System.Net.Mail.SmtpException)
                                    {
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] =
                                Pmensaje.CssClass = "MensajesSupervisor";
                                lblencabezado.Text = "Sr. Usuario";
                                lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de Familias válido. Por favor verifique que el nombre de la hoja sea Familias_Planning";
                                Mensajes_Usuario();



                            }
                            oConn1.Close();
                        }
                        else
                        {


                            Pmensaje.CssClass = "MensajesSupervisor";
                            lblencabezado.Text = "Sr. Usuario";
                            lblmensajegeneral.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            Mensajes_Usuario();



                        }
                    }
                }
                else
                {

                    Pmensaje.CssClass = "MensajesSupervisor";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "Es indispensable seleccionar un archivo.";

                    Mensajes_Usuario();



                }
            }
            #endregion 

            #region levantamiento por categoria
            if (sTIPO_LEVANTAMIENTO == "Categoria")
            {
                if ((FileUpLevInformacion.PostedFile != null) && (FileUpLevInformacion.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpLevInformacion.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("PDV_Planning") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpLevInformacion.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();
                            DataTable dt = new DataTable();

                            FileUpLevInformacion.PostedFile.SaveAs(SaveLocation);

                            // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            //oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Pla_Categorias"];
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            try
                            {
                                oDa.Fill(oDs);
                                dt = oDs.Tables[0];
                                if (dt.Columns.Count == 1)
                                {
                                    dt.Columns[0].ColumnName = "Categoria";                                    

                                    // Bulk Copy to SQL Server
                                    ConnectionStringSettings settingconection;
                                    settingconection = ConfigurationManager.ConnectionStrings["ConectaDBLucky"];
                                    string oSqlConnIN = settingconection.ConnectionString;

                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oSqlConnIN))
                                    {
                                        bulkCopy.DestinationTableName = "PLA_Category_Planning_TMP";
                                        bulkCopy.WriteToServer(dt);
                                    }

                                    DataSet dsCargar = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CARGAMASIVA_CATEGORY_PLANNING",
                                    Convert.ToInt32(this.Session["company_id"].ToString().Trim()),
                                    LblPlanning.Text,
                                    Convert.ToInt32(this.Session["RbtnListInfProd"].ToString().Trim()),
                                    this.Session["sUser"].ToString().Trim(), DateTime.Now,
                                    this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                    

                                    Pmensaje.CssClass = "MensajesSupConfirm";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "Se ha realizado la carga de categorias para la campaña : " + LblPresupuestoPDV.Text.ToUpper() + " Revisar el log enviado a su correo ";

                                    Mensajes_Usuario();

                                    if (dsCargar.Tables[0].Rows.Count > 0)
                                    {
                                        string fnn = "Log_ArchivoCategorias.xls";
                                        string pathlog = Server.MapPath("PDV_Planning") + "\\" + fnn;
                                        if (ExportarExcelDataTable(dsCargar.Tables[0], pathlog))
                                        {
                                            //Se envia el correo con el Log generado 

                                            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                            System.Net.Mail.Attachment file = new System.Net.Mail.Attachment(pathlog);//Adjunta el Log Generado
                                            correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                            correo.To.Add(this.Session["smail"].ToString());
                                            correo.Subject = "Log de registro de carga masiva de Categorias para la campaña";
                                            correo.Attachments.Add(file);
                                            correo.IsBodyHtml = true;
                                            correo.Priority = System.Net.Mail.MailPriority.Low;
                                            string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                                        this.Session["nameuser"].ToString() + "<br/>" + "<br/>" + 
                                                        "El archivo de carga masiva de Categorias que usted seleccionó para la carga Generó el siguiente registro ." + "<br/>" +
                                                         "<br/>" +  "<br/>" +                                           
                                                        "Por favor verifique el archivo adjunto a este correo , el cual contiene información importante para usted" + "<br/>" +                                                                                    
                                                        "<br/>" +  "<br/>" +     
                                                        "En Dicho archivo se le indica que Categorias fueron cargadas con éxito , cuales ya existian y no fue necesario cargar nuevamente, cuales Categorias corresponden a un competidor no valido para el cliente de la campaña y cuales estan pendientes por motivo que no están creados en el maestro de categorias de Xplora" +
                                                        "<br/>" +  "<br/>" + 
                                                        "Cualquier inquietud que pueda tener consultarlo con el Administrador Xplora" +
                                                        "<br/>" +  "<br/>" + 
                                                        "<br/>" +  "<br/>" + 
                                                        "Cordial Saludo" + "<br/>"  };

                                            correo.Body = string.Concat(txtbody);

                                            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                            cliente.Host = "mail.lucky.com.pe";

                                            try
                                            {
                                                cliente.Send(correo);
                                                dt = null;
                                            }
                                            catch (System.Net.Mail.SmtpException)
                                            {
                                            }
                                        }
                                    }
                                }
                                else
                                {

                                    //GvProductosPlanning.DataBind();


                                    Pmensaje.CssClass = "MensajesSupervisor";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de Categorias válido. Por favor verifique la estructura que fue enviada a su correo.";


                                    Mensajes_Usuario();

                                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                    correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                    correo.To.Add(this.Session["smail"].ToString());
                                    correo.Subject = "Errores en archivo de creación de Categorias";
                                    correo.IsBodyHtml = true;
                                    correo.Priority = System.Net.Mail.MailPriority.Normal;
                                    string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                            this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                   
                                             

                                            "El archivo que usted seleccionó para la carga de Categorias para la campaña no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga 1 columna" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique la columna de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                            "Columna 1  : Categoria" + "<br/>" +                                                                                                                             
                                            "Nota:  No es indispensable que la columna se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizarlo a su gusto ." +
                                            "<br/>" + "<br/>" + "<br/>" +
                                            "Cordial Saludo" + "<br/>" + "Administrador Xplora" };

                                    correo.Body = string.Concat(txtbody);

                                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                    cliente.Host = "mail.lucky.com.pe";

                                    try
                                    {
                                        cliente.Send(correo);
                                    }
                                    catch (System.Net.Mail.SmtpException)
                                    {
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] =
                                Pmensaje.CssClass = "MensajesSupervisor";
                                lblencabezado.Text = "Sr. Usuario";
                                lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de Categorias válido. Por favor verifique que el nombre de la hoja sea Categorias_Planning";
                                Mensajes_Usuario();



                            }
                            oConn1.Close();
                        }
                        else
                        {


                            Pmensaje.CssClass = "MensajesSupervisor";
                            lblencabezado.Text = "Sr. Usuario";
                            lblmensajegeneral.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            Mensajes_Usuario();



                        }
                    }
                }
                else
                {

                    Pmensaje.CssClass = "MensajesSupervisor";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "Es indispensable seleccionar un archivo.";

                    Mensajes_Usuario();



                }
            }
            #endregion

        }
    }
}