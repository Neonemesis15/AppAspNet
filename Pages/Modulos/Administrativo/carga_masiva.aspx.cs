using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Lucky.Data;
using System.Data.OleDb;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;
using Lucky.Business.Common.Application;

namespace SIGE.Pages.Modulos.Administrativo
{
    public partial class carga_masiva : System.Web.UI.Page
    {


        #region excel
        private void CreaExcel(DataSet ds,string nombre_Archivo)
        {
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbooks oLibros = default(Microsoft.Office.Interop.Excel.Workbooks);
            Microsoft.Office.Interop.Excel.Workbook oLibro = default(Microsoft.Office.Interop.Excel.Workbook);

            Microsoft.Office.Interop.Excel.Sheets oHojas = default(Microsoft.Office.Interop.Excel.Sheets);

            Microsoft.Office.Interop.Excel.Worksheet oHoja = default(Microsoft.Office.Interop.Excel.Worksheet);


            Microsoft.Office.Interop.Excel.Range oCeldas = default(Microsoft.Office.Interop.Excel.Range);

            try
            {


                string sFile = null;
                string sTemplate = null;

                // Usamos una plantilla para crear el nuevo excel

                sFile = Server.MapPath("Archivos") + "\\" +nombre_Archivo+ ".xls";

                sTemplate = Server.MapPath("Archivos") + "\\" + "Plantilla.xls";

                oExcel.Visible = false;

                oExcel.DisplayAlerts = false;

                // Abrimos un nuevo libro

                oLibros = oExcel.Workbooks;

                oLibros.Open(sTemplate);

                oLibro = oLibros.Item[1];

                oHojas = oLibro.Worksheets;


                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    oHoja = (Microsoft.Office.Interop.Excel.Worksheet)oHojas.Item[i + 1];

                    oHoja.Name = "Hoja" + (i + 1);

                    oCeldas = oHoja.Cells;
                    oHoja.Range["B2"].Interior.Color = 0;
                    oHoja.Range["B2"].Font.Color = 16777215;
                    oHoja.Range["A2"].Interior.Color = 0;
                    oHoja.Range["A2"].Font.Color = 16777215;


                    oHoja.Range["B2"].Font.Bold = true;
                    oHoja.Range["A2"].Font.Bold = true;

                    oHoja.Range["A2", "B" + (ds.Tables[i].Rows.Count + 2).ToString()].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDash;
                    oHoja.Range["A2", "B" + (ds.Tables[i].Rows.Count + 2).ToString()].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;




                    VuelcaDatos(ds.Tables[i], oCeldas);


                }



                oHoja.SaveAs(sFile);

                oLibro.Close();

                // Eliminamos lo que hemos creado

                oExcel.Quit();

                oExcel = null;

                oLibros = null;

                oLibro = null;

                oHojas = null;

                oHoja = null;

                oCeldas = null;

                System.GC.Collect();

            }
            catch
            {
                oLibro.Close();
                oExcel.Quit();

                Pmensaje.CssClass = "MensajesSupervisor";
                lblencabezado.Text = "Sr. Usuario";
                lblmensajegeneral.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                Mensajes_Usuario();
            }

        }

        private void VuelcaDatos(DataTable tabla, Microsoft.Office.Interop.Excel.Range oCells)
        {

            DataRow dr = null;
            object[] ary = null;

            int iRow = 0;
            int iCol = 0;

            // Sacamos las cabeceras


            for (iCol = 0; iCol <= tabla.Columns.Count - 1; iCol++)
            {
                oCells[2, iCol + 1] = tabla.Columns[iCol].ToString();


            }


            // Sacamos los datos


            for (iRow = 0; iRow <= tabla.Rows.Count - 1; iRow++)
            {
                dr = tabla.Rows[iRow];

                ary = dr.ItemArray;


                for (iCol = 0; iCol <= ary.GetUpperBound(0); iCol++)
                {
                    oCells[iRow + 3, iCol + 1] = ary[iCol].ToString();

                }

            }



        }

        #endregion

        Conexion oCoon = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.Session["TipoCarga"].ToString() == "PtoVentas")
                {
                    try
                    {
                        tblPtoVenta.Visible = true;
                        btnloadptoventa.Visible = true;
                        DataSet ds = oCoon.ejecutarDataSet("AD_CARGAMASIVA_PTOVENTA_DATOS");
                        CreaExcel(ds, "DATOS_CARGA_PTOVENTA");
                    }
                    catch
                    {
 
                    }
                }
                if (this.Session["TipoCarga"].ToString() == "PtoVentaCliente")
                {
                    try
                    {
                        tblPtoVentaCliente.Visible = true;
                        btnloadptoventaCliente.Visible = true;
                        DataSet ds = oCoon.ejecutarDataSet("AD_CARGAMASIVA_PTOVENTACLIENTE_DATOS");
                        CreaExcel(ds, "DATOS_CARGA_PTOVENTA_CLIENTE");
                    }
                    catch
                    {
 
                    }
                }
            }
        }

        private void Mensajes_Usuario()
        {
            ModalPopupMensaje.Show();
        }


        protected void btnloadproduct_Click(object sender, EventArgs e)
        {
                if ((FileUpProducto.PostedFile != null) && (FileUpProducto.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpProducto.PostedFile.FileName);

                    string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;
                    try
                    {
                        FileUpProducto.PostedFile.SaveAs(SaveLocation);
                        DataSet ds = new DataSet();
                        DataTable sourceData = new DataTable();
                        if (SaveLocation != string.Empty)
                        {
                            ds.ReadXml(SaveLocation);
                            FileUpProducto.PostedFile.SaveAs(MapPath("../../Modulos/Administrativo/Busquedas/" + FileUpProducto.FileName));
                            sourceData = ds.Tables[0];
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oCoon.GetConnection()))
                            {
                                bulkCopy.ColumnMappings.Clear();
                                foreach (DataColumn dc in sourceData.Columns)
                                {
                                    bulkCopy.ColumnMappings.Add(dc.ColumnName.Trim(), dc.ColumnName.Trim());
                                }
                                bulkCopy.DestinationTableName = "Products";
                                bulkCopy.WriteToServer(sourceData, DataRowState.Added);
                            }
                            LblVacio.Text = "Ahora debe cargar Empaquetamientos para estos productos";
                            LblCargarArchivo.Visible = false;
                            FileUpProducto.Visible = false;
                            btnloadproduct.Visible = false;
                            LblCargarArchivo0.Visible = true;
                            FileUpProducto0.Visible = true;
                            btnloadproduct0.Visible = true;                                                   
                        }
                        else
                        {
                            LblVacio.Text = "Seleccione Archivo con PRODUCTOS.";
                            LblCargarArchivo.Visible = true;
                            FileUpProducto.Visible = true;
                            btnloadproduct.Visible = true;
                            LblCargarArchivo0.Visible = false;
                            FileUpProducto0.Visible = false;
                            btnloadproduct0.Visible = false;
                        }
                    }

                    catch (Exception ioex)
                    {
                        string errMessage = "";
                        Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ioex);
                        exs.Country = "SIGE(" + ConfigurationManager.AppSettings["COUNTRY"] + ") - Usuario " + this.Session["sUser"].ToString();
                        errMessage = Convert.ToString(ioex);
                        exs.errorWebsite(this.Session["scountry"].ToString().Trim());
                        errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ioex.Message);
                        LblVacio.Text = "Se ha producido un error en la carga del Archivo.";
                        LblCargarArchivo.Visible = true;
                        FileUpProducto.Visible = true;
                        btnloadproduct.Visible = true;
                        LblCargarArchivo0.Visible = false;
                        FileUpProducto0.Visible = false;
                        btnloadproduct0.Visible = false;   
                    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Response.Write("Error: " + ex.Message);
                    //    //Nota: Exception.Message devuelve un mensaje detallado que describe la excepción actual. 
                    //    //Por motivos de seguridad, no se recomienda devolver Exception.Message a los usuarios finales de 
                    //    //entornos de producción. Sería más aconsejable poner un mensaje de error genérico. 
                    //}
                }
                else
                {
                    LblVacio.Text="Seleccione Archivo con PRODUCTOS.";
                    LblCargarArchivo.Visible = true;
                    FileUpProducto.Visible = true;
                    btnloadproduct.Visible = true;
                    LblCargarArchivo0.Visible = false;
                    FileUpProducto0.Visible = false;
                    btnloadproduct0.Visible = false;
                }
            
            //try
            //{                
            //    string extension;
            //    DataSet ds = new DataSet();
            //    DataTable sourceData = new DataTable();
                

            //    if (FileUpProducto.PostedFile.FileName != string.Empty)
            //    {  
             
            //        ds.ReadXml(FileUpProducto.PostedFile.FileName);
            //        extension = Path.GetFileName(FileUpProducto.PostedFile.FileName);
            //            //Path.GetExtension(FileUpProducto.FileName);
            //        FileUpProducto.PostedFile.SaveAs(MapPath("~/Pruebas_Lucky/" + FileUpProducto.FileName));

            //        //FileUpProducto.SaveAs(MapPath("../../Pages/Carga/" + FileUpProducto.FileName));
            //        sourceData = ds.Tables[0];
            //        // open the connection
            //        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oCoon.GetConnection()))
            //        {
            //            // column mapeadas
            //            bulkCopy.ColumnMappings.Clear();
            //            foreach (DataColumn dc in sourceData.Columns)
            //            {
            //                bulkCopy.ColumnMappings.Add(dc.ColumnName.Trim(), dc.ColumnName.Trim());
            //            }
            //            bulkCopy.DestinationTableName = "Products";
            //            bulkCopy.WriteToServer(sourceData, DataRowState.Added);
            //        }
            //        LblVacio.Text = "Se han Cargado Correctamente los Datos.";
            //        //Response.Write("Se han Cargado Correctamente los Datos.");

            //    }
            //    else
            //    {
            //        LblVacio.Text = "Debe seleccionar Archivo.";
            //        //Response.Write("Debe seleccionar Archivo.");
            //    }

            //}
            //catch (Exception ioex)
            //{                
            //    string errMessage = "";
            //    Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ioex);
            //    exs.Country = "SIGE(" + ConfigurationManager.AppSettings["COUNTRY"] + ") - Usuario " + this.Session["sUser"].ToString();
            //    errMessage = Convert.ToString(ioex);
            //    exs.errorWebsite(this.Session["scountry"].ToString().Trim());
            //    errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ioex.Message);
            //}           
        }

        protected void btnloadproduct0_Click(object sender, EventArgs e)
        {
            if ((FileUpProducto0.PostedFile != null) && (FileUpProducto0.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(FileUpProducto0.PostedFile.FileName);

                string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;
                try
                {
                    FileUpProducto0.PostedFile.SaveAs(SaveLocation);
                    DataSet ds = new DataSet();
                    DataTable sourceData = new DataTable();
                    if (SaveLocation != string.Empty)
                    {
                        ds.ReadXml(SaveLocation);
                        FileUpProducto0.PostedFile.SaveAs(MapPath("../../Modulos/Administrativo/Busquedas/" + FileUpProducto0.FileName));
                        sourceData = ds.Tables[0];
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oCoon.GetConnection()))
                        {
                            bulkCopy.ColumnMappings.Clear();
                            foreach (DataColumn dc in sourceData.Columns)
                            {
                                bulkCopy.ColumnMappings.Add(dc.ColumnName.Trim(), dc.ColumnName.Trim());
                            }
                            bulkCopy.DestinationTableName = "Product_Packing";
                            bulkCopy.WriteToServer(sourceData, DataRowState.Added);
                        }
                        LblVacio.Text = "Los productos y empaquetamientos estan almacenados";
                        LblCargarArchivo.Visible = true;
                        FileUpProducto.Visible = true;
                        btnloadproduct.Visible = true;
                        LblCargarArchivo0.Visible = false;
                        FileUpProducto0.Visible = false;
                        btnloadproduct0.Visible = false;
                       

                    }
                    else
                    {
                        LblVacio.Text = "Seleccione Archivo con EMPAQUETAMIENTO DE PRODUCTOS.";
                        LblCargarArchivo.Visible = false;
                        FileUpProducto.Visible = false;
                        btnloadproduct.Visible = false;
                        LblCargarArchivo0.Visible = true;
                        FileUpProducto0.Visible = true;
                        btnloadproduct0.Visible = true;   
                    }


                }

                catch (Exception ioex)
                {
                    string errMessage = "";
                    Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ioex);
                    exs.Country = "SIGE(" + ConfigurationManager.AppSettings["COUNTRY"] + ") - Usuario " + this.Session["sUser"].ToString();
                    errMessage = Convert.ToString(ioex);
                    exs.errorWebsite(this.Session["scountry"].ToString().Trim());
                    errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ioex.Message);
                    LblVacio.Text = "Se ha producido un error en la carga del Archivo.";
                    LblCargarArchivo.Visible = false;
                    FileUpProducto.Visible = false;
                    btnloadproduct.Visible = false;
                    LblCargarArchivo0.Visible = true;
                    FileUpProducto0.Visible = true;
                    btnloadproduct0.Visible = true;   
                }
                //}
                //catch (Exception ex)
                //{
                //    Response.Write("Error: " + ex.Message);
                //    //Nota: Exception.Message devuelve un mensaje detallado que describe la excepción actual. 
                //    //Por motivos de seguridad, no se recomienda devolver Exception.Message a los usuarios finales de 
                //    //entornos de producción. Sería más aconsejable poner un mensaje de error genérico. 
                //}
            }
            else
            {
                LblVacio.Text = "Seleccione Archivo con EMPAQUETAMIENTO DE PRODUCTOS.";
                LblCargarArchivo.Visible = false;
                FileUpProducto.Visible = false;
                btnloadproduct.Visible = false;
                LblCargarArchivo0.Visible = true;
                FileUpProducto0.Visible = true;
                btnloadproduct0.Visible = true;   
            }

        }

        protected void btnloadptoventa_Click(object sender, EventArgs e)
        {
            if ((FileUpPtoVenta.PostedFile != null) && (FileUpPtoVenta.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(FileUpPtoVenta.PostedFile.FileName);
                string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;

                if (SaveLocation != string.Empty)
                {
                    if (FileUpPtoVenta.FileName.ToLower().EndsWith(".xls"))
                    {
                        // string Destino = Server.MapPath(null) + "\\PDV_Planning\\" + Path.GetFileName(FileUpPDV.PostedFile.FileName);
                        OleDbConnection oConn1 = new OleDbConnection();
                        OleDbCommand oCmd = new OleDbCommand();
                        OleDbDataAdapter oDa = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();
                        DataTable dt = new DataTable();

                        FileUpPtoVenta.PostedFile.SaveAs(SaveLocation);

                        // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                        oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                        oConn1.Open();
                        oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Pla_Pto_Venta"];
                        oCmd.Connection = oConn1;
                        oDa.SelectCommand = oCmd;

                        try
                        {
                            if (this.Session["scountry"].ToString() != null)
                            {
                                oDa.Fill(oDs);


                                dt = oDs.Tables[0];
                                int numcol = 13; //determina el número de columnas para el datatable
                                if (dt.Columns.Count == numcol)
                                {
                                    dt.Columns[0].ColumnName = "Cód Tipo de Documento";
                                    dt.Columns[1].ColumnName = "Identificación";
                                    dt.Columns[2].ColumnName = "Razón Social";
                                    dt.Columns[3].ColumnName = "Nombre de Punto de Venta";
                                    dt.Columns[4].ColumnName = "País";
                                    dt.Columns[5].ColumnName = "Departamento";
                                    dt.Columns[6].ColumnName = "Provincia";
                                    dt.Columns[7].ColumnName = "Distrito";
                                    dt.Columns[8].ColumnName = "Dirección";
                                    dt.Columns[9].ColumnName = "Canal";
                                    dt.Columns[10].ColumnName = "Tipo de Agrupación Comercial";
                                    dt.Columns[11].ColumnName = "Nombre de Agrupación comercial";
                                    dt.Columns[12].ColumnName = "Segmento";


                                    int cargados = 0;
                                    int duplicados = 0;
                                    

                                    string cod_tipo_Documento;
                                    string Identificación;
                                    string razon_social;
                                    string Nombre_pto_Venta;
                                    string pais;
                                    string Departamento;
                                    string Provincia;
                                    string Distrito;
                                    string Dirección;
                                    string Canal;
                                    string Tipo_Agrupacion;
                                    string NNombre_Agrupacion;
                                    string Segmento;

                                    DataSet ds = new DataSet();

                                    PuntosDV oPuntosDV = new PuntosDV();
                                    EPuntosDV oEPuntosDV = new EPuntosDV();


                                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                    {


                                        cod_tipo_Documento = dt.Rows[i]["Cód Tipo de Documento"].ToString().Trim();
                                        Identificación = dt.Rows[i]["Identificación"].ToString().Trim();
                                        razon_social = dt.Rows[i]["Razón Social"].ToString().Trim();
                                        Nombre_pto_Venta = dt.Rows[i]["Nombre de Punto de Venta"].ToString().Trim();
                                        pais = dt.Rows[i]["País"].ToString().Trim();
                                        Departamento = dt.Rows[i]["Departamento"].ToString().Trim();
                                        Provincia = dt.Rows[i]["Provincia"].ToString().Trim();
                                        Distrito = dt.Rows[i]["Distrito"].ToString().Trim();
                                        Dirección = dt.Rows[i]["Dirección"].ToString().Trim();
                                        Canal = dt.Rows[i]["Canal"].ToString().Trim();
                                        Tipo_Agrupacion = dt.Rows[i]["Tipo de Agrupación Comercial"].ToString().Trim();
                                        NNombre_Agrupacion = dt.Rows[i]["Nombre de Agrupación comercial"].ToString().Trim();
                                        Segmento = dt.Rows[i]["Segmento"].ToString().Trim();



                                            Conexion cn = new Conexion();
                                           ds =cn.ejecutarDataSet("AD_CONSULTA_PTOVENTA",Identificación);

                                        if(ds.Tables[0].Rows.Count == 0)
                                        {
                                            EPuntosDV oePuntosDV = oPuntosDV.RegistrarPDV(cod_tipo_Documento, Identificación, "", "", "", "", razon_social, Nombre_pto_Venta, "", "", "",
                                            pais, Departamento, Provincia, Distrito, null, Dirección,
                                            "", Canal, Convert.ToInt32(Tipo_Agrupacion), NNombre_Agrupacion, Convert.ToInt32(Segmento), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                                            cargados = cargados + 1;
                                        }
                                        else
                                        {
                                            duplicados = duplicados + 1;
                                        }
                                        
                                    }


                                    Pmensaje.CssClass = "MensajesSupervisor";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "Numero de puntos de ventas Cargados:" + cargados + ", numero de Puntos de ventas no cargados:" + duplicados;
                                    Mensajes_Usuario();




                                }
                                else
                                {
                                    Pmensaje.CssClass = "MensajesSupervisor";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "Por favor verifique. La información";
                                    Mensajes_Usuario();
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            //Pmensaje.CssClass = "MensajesSupervisor";
                            //lblencabezado.Text = "Sr. Usuario";
                            //lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de puntos de venta válido. Por favor verifique que el nombre de la hoja donde estan los datos sea Puntos_Venta";
                            //Mensajes_Usuario();
                        }
                        oConn1.Close();
                    }
                    else
                    {
                        //Pmensaje.CssClass = "MensajesSupervisor";
                        //lblencabezado.Text = "Sr. Usuario";
                        //lblmensajegeneral.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                        //Mensajes_Usuario();
                    }
                }
            }
             else
             {
                 //Pmensaje.CssClass = "MensajesSupervisor";
                 //lblencabezado.Text = "Sr. Usuario";
                 //lblmensajegeneral.Text = "Es indispensable seleccionar un presupuesto y un archivo.";
                 //Mensajes_Usuario();
             }
        }

        protected void btnloadptoventaCliente_Click(object sender, EventArgs e)
        {
            if ((FileUpPtoVentaCliente.PostedFile != null) && (FileUpPtoVentaCliente.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(FileUpPtoVentaCliente.PostedFile.FileName);
                string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;

                if (SaveLocation != string.Empty)
                {
                    if (FileUpPtoVentaCliente.FileName.ToLower().EndsWith(".xls"))
                    {
                        // string Destino = Server.MapPath(null) + "\\PDV_Planning\\" + Path.GetFileName(FileUpPDV.PostedFile.FileName);
                        OleDbConnection oConn1 = new OleDbConnection();
                        OleDbCommand oCmd = new OleDbCommand();
                        OleDbDataAdapter oDa = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();
                        DataTable dt = new DataTable();

                        FileUpPtoVentaCliente.PostedFile.SaveAs(SaveLocation);

                        // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                        oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                        oConn1.Open();
                        oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_AD_Pto_Venta_Cliente"];
                        oCmd.Connection = oConn1;
                        oDa.SelectCommand = oCmd;

                        try
                        {
                            if (this.Session["scountry"].ToString() != null)
                            {
                                oDa.Fill(oDs);


                                dt = oDs.Tables[0];
                                int numcol = 6; //determina el número de columnas para el datatable
                                if (dt.Columns.Count == numcol)
                                {
                                    dt.Columns[0].ColumnName = "cod_Company";
                                    dt.Columns[1].ColumnName = "id_PtoVenta";
                                    dt.Columns[2].ColumnName = "cod_Ptoventa";
                                    dt.Columns[3].ColumnName = "cod_sector";
                                    dt.Columns[4].ColumnName = "cod_Oficina";
                                    dt.Columns[5].ColumnName = "alias";
                                   


                                    int cargados = 0;
                                    int duplicados = 0;


                                    string cod_Company;
                                    string id_PtoVenta;
                                    string cod_Ptoventa;
                                    string cod_sector;
                                    string cod_Oficina;
                                    string alias;
                                    

                                    DataSet ds = new DataSet();

                                    PuntosDV oPuntosDV = new PuntosDV();
                                    EPuntosDV oEPuntosDV = new EPuntosDV();


                                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                    {


                                        cod_Company = dt.Rows[i]["cod_Company"].ToString().Trim();
                                        id_PtoVenta = dt.Rows[i]["id_PtoVenta"].ToString().Trim();
                                        cod_Ptoventa = dt.Rows[i]["cod_Ptoventa"].ToString().Trim();
                                        cod_sector = dt.Rows[i]["cod_sector"].ToString().Trim();
                                        cod_Oficina = dt.Rows[i]["cod_Oficina"].ToString().Trim();
                                        alias = dt.Rows[i]["alias"].ToString().Trim();
                                       



                                        Conexion cn = new Conexion();
                                        ds = cn.ejecutarDataSet("AD_CONSULTA_PTOVENTACLIENTE", cod_Company, id_PtoVenta);

                                        if (ds.Tables[0].Rows.Count == 0)
                                        {

                                            EPuntosDV oePDVCliente = oPuntosDV.RegistrarClientPDV(Convert.ToInt32(cod_Company), Convert.ToInt32(id_PtoVenta), cod_Ptoventa, Convert.ToInt32(cod_sector), Convert.ToInt32(cod_Oficina), Convert.ToInt32(null), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now, alias);

                                            EPuntosDV oePDVClientetmp = oPuntosDV.RegistrarClientPDVTMP();

                                            cargados = cargados + 1;
                                        }
                                        else
                                        {
                                            duplicados = duplicados + 1;
                                        }




                                    }


                                    Pmensaje.CssClass = "MensajesSupervisor";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "Numero de puntos de ventas Cargados:" + cargados + ", numero de Puntos de ventas no cargados:" + duplicados;
                                    Mensajes_Usuario();




                                }
                                else
                                {
                                    //Pmensaje.CssClass = "MensajesSupervisor";
                                    //lblencabezado.Text = "Sr. Usuario";
                                    //lblmensajegeneral.Text = "El archivo debe contener 29 campos. Por favor verifique.";
                                    //Mensajes_Usuario();
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            //Pmensaje.CssClass = "MensajesSupervisor";
                            //lblencabezado.Text = "Sr. Usuario";
                            //lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de puntos de venta válido. Por favor verifique que el nombre de la hoja donde estan los datos sea Puntos_Venta";
                            //Mensajes_Usuario();
                        }
                        oConn1.Close();
                    }
                    else
                    {
                        //Pmensaje.CssClass = "MensajesSupervisor";
                        //lblencabezado.Text = "Sr. Usuario";
                        //lblmensajegeneral.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                        //Mensajes_Usuario();
                    }
                }
            }
            else
            {
                //Pmensaje.CssClass = "MensajesSupervisor";
                //lblencabezado.Text = "Sr. Usuario";
                //lblmensajegeneral.Text = "Es indispensable seleccionar un presupuesto y un archivo.";
                //Mensajes_Usuario();
            }
        }
    
    
    
    }
}
                
              