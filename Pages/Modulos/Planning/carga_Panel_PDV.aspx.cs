using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity;
using Lucky.Business.Common.Application;
using Lucky.Entity.Common.Application;
using Lucky.CFG.Tools;
using SIGE.Facade_Proceso_Planning;
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;

using Microsoft.Office.Interop;

namespace SIGE.Pages.Modulos.Planning
{
    public partial class carga_Panel_PDV : System.Web.UI.Page
    {

        Facade_Proceso_Planning.Facade_Proceso_Planning wsPlanning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        Conexion oCoon = new Conexion();
        //PointOfSale_PlanningOper PointOfSale_PlanningOper = new PointOfSale_PlanningOper(); 

        private void Mensajes_Usuario()
        {
            ModalPopupMensaje.Show();
        }

        private void CreaExcel(DataSet ds)
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

                sFile = Server.MapPath("PDV_Planning") + "\\" + "Datos_Panel_ptoVenta2.xls";

                sTemplate = Server.MapPath("PDV_Planning") + "\\" + "Datos_Panel_ptoVenta1.xls";

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



        private void llenaOperativosAsignaPDVOPE()
        {
            DataTable dtStaffPlanning = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_VALIDACIONES_CARGAMASIVAPDVOPE", "Operativo", LblPlanning.Text, "none");


            if (dtStaffPlanning != null)
            {
                if (dtStaffPlanning.Rows.Count > 0)
                {
                    CmbOpePlanning.DataSource = dtStaffPlanning;
                    CmbOpePlanning.DataTextField = "name_user";
                    CmbOpePlanning.DataValueField = "Person_id";
                    CmbOpePlanning.DataBind();
                }
                else
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "No se ha seleccionado el personal de la campaña: " + LblPresupuestoPDV.Text;
                    Mensajes_Usuario();
                }
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    LblPresupuestoPDV.Text = this.Session["Presupuestoreportes"].ToString().Trim();
                    LblPlanning.Text = this.Session["Planning"].ToString().Trim();
                   // Log(LblPlanning.Text);

                    DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_EXPORTAEXCEL_PANEL_PTO_VENTA", LblPlanning.Text);
                    CreaExcel(ds);

                }
                catch
                {
                    Pmensaje.CssClass = "MensajesSupervisor";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                    Mensajes_Usuario();
                }
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

        

        public void insertar(string ptoventa, string cod_reporte, string cod_SubReporte, string cod_perido, string planning, string cod_Producto, string cod_Marca,
                              string cod_Familia, string cod_Categoria)
        {
            
            Conexion cn = new Conexion();
            try
            {
                DataTable dt = cn.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAR_PANEL_PTOVENTA", ptoventa, cod_reporte, cod_SubReporte, cod_perido, cod_Producto, cod_Marca, cod_Familia, cod_Categoria, planning);

                if (dt.Rows.Count == 0)
                {


                    string a = this.Session["vista_final"].ToString();

                    if (this.Session["vista_final"].ToString() == "Producto" && cod_Producto!="")
                    {

                        cn.ejecutarDataTable("UP_WEB_REGISTERPANELPUNTOVENTA", planning, ptoventa, cod_reporte, cod_SubReporte, cod_perido, cod_Producto, "", "", "", true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                    }
                    if (this.Session["vista_final"].ToString() == "Marca" && cod_Marca!="")
                    {

                        cn.ejecutarDataTable("UP_WEB_REGISTERPANELPUNTOVENTA", planning, ptoventa, cod_reporte, cod_SubReporte, cod_perido, "", cod_Marca, "", "", true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                    }
                    if (this.Session["vista_final"].ToString() == "Categoria" && cod_Categoria!="")
                    {

                        cn.ejecutarDataTable("UP_WEB_REGISTERPANELPUNTOVENTA", planning, ptoventa, cod_reporte, cod_SubReporte, cod_perido, "", "", "", cod_Categoria, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                        
                    }
                    if (this.Session["vista_final"].ToString() == "Familia"  && cod_Familia!="" )
                    {

                        cn.ejecutarDataTable("UP_WEB_REGISTERPANELPUNTOVENTA", planning, ptoventa, cod_reporte, cod_SubReporte, cod_perido, "", "", cod_Familia, "", true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                    }

                    Pmensaje.CssClass = "MensajesSupConfirm";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "Se ha creado con éxito a la campaña ";
                    Mensajes_Usuario();



                }

                else
                {

                    Pmensaje.CssClass = "MensajesSupervisor";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "Ya ha sido agregado a la campaña.";
                    Mensajes_Usuario();

                }

            }
            catch
            {

            }
        }


        void CargarOpciones1()
        {


            DataTable vistas = ((DataTable)this.Session["vistas"]);
            string rep = Session["id_report"].ToString();
            string tipo_rep = this.Session["cod_SubReporte"].ToString();
            this.Session["tipo_rep"] = tipo_rep;

            int row = 0;
            if (!tipo_rep.Equals(""))
            {
                if (rep.Equals("23"))//FOTOGRAFICO
                {
                    if (tipo_rep.Equals("02"))//tipo exhib. visib.
                        row = 1;
                }
                else if (rep.Equals("57"))//FOTOGRAFICO
                {
                    if (tipo_rep.Equals("02"))//tipo exhib. visib.
                        row = 1;
                }
                else if (rep.Equals("58"))//PRESENCIA
                {
                    if (tipo_rep.Equals("04"))//tipo exhib. visib.
                        row = 1;
                    if (tipo_rep.Equals("05"))//tipo exhib. visib.
                        row = 2;
                    if (tipo_rep.Equals("06"))//tipo exhib. visib.
                        row = 3;
                    if (tipo_rep.Equals("07"))//tipo exhib. visib.
                        row = 4;
                    if (tipo_rep.Equals("08"))//tipo exhib. visib.
                        row = 5;
                }
            }

            string vista_final = "";

            //verifica que el objeto vistas retornado en store no sea nulo ademas de verificar el revistro de la vista categoria
            //que en algunos casos devolvia rows con valores nulos. Angel Ortiz 26/09/2011
            if (vistas != null && vistas.Rows[row]["Vista_Categoria"] != System.DBNull.Value)
            {
                if (vistas.Rows.Count > 0)
                {
                    vista_final = vistas.Rows[row]["vista_final"].ToString().Trim();

                    //carga array booleano para activar las vistas
                    if (vistas.Rows.Count > 0)
                    {
                        bool[] s_vistas = new bool[6];

                        s_vistas[0] = Convert.ToBoolean(vistas.Rows[row]["Vista_Categoria"]);
                        s_vistas[1] = Convert.ToBoolean(vistas.Rows[row]["Vista_Marca"]);
                        s_vistas[2] = Convert.ToBoolean(vistas.Rows[row]["Vista_SubMarca"]);
                        s_vistas[3] = Convert.ToBoolean(vistas.Rows[row]["Vista_Familia"]);
                        s_vistas[4] = Convert.ToBoolean(vistas.Rows[row]["Vista_SubFamilia"]);
                        s_vistas[5] = Convert.ToBoolean(vistas.Rows[row]["Vista_Producto"]);

                        this.Session["s_vistas"] = s_vistas;
                    }

                    this.Session["vista_final"] = vista_final;


                }
                else
                {


                    Pmensaje.CssClass = "MensajesSupervisor";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "No existe parametrización de esta vista. Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente.";
                    Mensajes_Usuario();
                    return;

                }
            }
            else
            {

                Pmensaje.CssClass = "MensajesSupervisor";
                lblencabezado.Text = "Sr. Usuario";
                lblmensajegeneral.Text = "No existe parametrización de esta vista. Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente.";
                Mensajes_Usuario();
                return;
            }

        }



        protected void BtnCargaArchivo_Click(object sender, EventArgs e)
        {

            if ((FileUpPDV.PostedFile != null) && (FileUpPDV.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(FileUpPDV.PostedFile.FileName);
                string SaveLocation = Server.MapPath("PDV_Planning") + "\\" + fn;

                if (SaveLocation != string.Empty)
                {
                    if (FileUpPDV.FileName.ToLower().EndsWith(".xls"))
                    {
                        OleDbConnection oConn1 = new OleDbConnection();
                        OleDbCommand oCmd = new OleDbCommand();
                        OleDbDataAdapter oDa = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();

                        DataTable dt = new DataTable();


                        FileUpPDV.PostedFile.SaveAs(SaveLocation);

                        // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                        oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                        oConn1.Open();
                        oCmd.CommandText = "SELECT * FROM [Hoja1$]";
                        oCmd.Connection = oConn1;
                        oDa.SelectCommand = oCmd;
                        try
                        {
                            if (this.Session["scountry"].ToString() != null)
                            {
                                oDa.Fill(oDs);
                                dt = oDs.Tables[0];
                                if (dt.Columns.Count == 8)
                                {

                                    dt.Columns[0].ColumnName = "cod_PDV";
                                    dt.Columns[1].ColumnName = "cod_Reporte";
                                    dt.Columns[2].ColumnName = "cod_SubReporte";
                                    dt.Columns[3].ColumnName = "cod_Periodo";
                                    dt.Columns[4].ColumnName = "cod_Producto";
                                    dt.Columns[5].ColumnName = "cod_Marca";
                                    dt.Columns[6].ColumnName = "cod_Familia";
                                    dt.Columns[7].ColumnName = "cod_Categoria";

                                    Gvlog.DataSource = dt;
                                    Gvlog.DataBind();

                                    for (int i = 0; i <= Gvlog.Rows.Count - 1; i++)
                                    {
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&nbsp;", "");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#160;", "");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#193;", "Á");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#201;", "É");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#205;", "Í");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#211;", "Ó");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#218;", "Ú");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#225;", "á");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#233;", "é");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#237;", "í");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#243;", "ó");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#250;", "ú");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#209;", "Ñ");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#241;", "ñ");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&amp;", "&");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#176;", "o");
                                        Gvlog.Rows[i].Cells[0].Text = Gvlog.Rows[i].Cells[0].Text.Replace("&#186;", "o");

                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&nbsp;", "");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("  ", " ");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#160;", "");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#193;", "Á");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#201;", "É");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#205;", "Í");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#211;", "Ó");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#218;", "Ú");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#225;", "á");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#233;", "é");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#237;", "í");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#243;", "ó");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#250;", "ú");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#209;", "Ñ");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#241;", "ñ");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&amp;", "&");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#176;", "o");
                                        Gvlog.Rows[i].Cells[1].Text = Gvlog.Rows[i].Cells[1].Text.Replace("&#186;", "o");


                                    }

                                    DPlanning dplanning = new DPlanning();
                                    string cod_PDV, cod_Reporte, cod_SubReporte, cod_Periodo, cod_Producto, cod_Marca, cod_Familia, cod_Categoria;
                                    Conexion cn = new Conexion();

                                    bool sigue = true;
                                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                    {                                       // llenaOperativosAsignaPDVOPE();


                                    cod_PDV = dt.Rows[i][0].ToString().Trim();
                                    cod_Reporte = dt.Rows[i][1].ToString().Trim();
                                    cod_SubReporte = dt.Rows[i][2].ToString().Trim();
                                    cod_Periodo = dt.Rows[i][3].ToString().Trim();
                                    cod_Producto = dt.Rows[i][4].ToString().Trim();
                                    cod_Marca = dt.Rows[i][5].ToString().Trim();
                                    cod_Familia = dt.Rows[i][6].ToString().Trim();
                                    cod_Categoria = dt.Rows[i][7].ToString().Trim();

                                   

                                    DataSet ds = oCoon.ejecutarDataSet("UP_WEB_CONSULTAR_Existencia_Panel_ptoVenta", cod_PDV, cod_Reporte,cod_Periodo, LblPlanning.Text);

                                       if (ds.Tables[0].Rows.Count > 0)
                                       {

                                           if (ds.Tables[1].Rows.Count > 0)
                                           {
                                               DataTable tipo_reporte = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_TIPO_REPORTE_LISTAR", Convert.ToInt32(this.Session["company_id"].ToString().Trim()), cod_Reporte);

                                               if (tipo_reporte.Rows.Count > 0 && cod_SubReporte!= "")//tiene subreporte
                                               {
                                                  //pasa

                                                   this.Session["cod_SubReporte"] = cod_SubReporte;

                                                   DataTable vistas = dplanning.ValidaTipoGestion(Convert.ToInt32(this.Session["company_id"].ToString().Trim()), this.Session["Planning_CodChannel"].ToString().Trim(), Convert.ToInt32(cod_Reporte)); //obtiene los valores booleanos para las vistas de paneles
                                                   this.Session["vistas"] = vistas;

                                                   CargarOpciones1();
                                                   insertar(cod_PDV, cod_Reporte,cod_SubReporte, cod_Periodo, LblPlanning.Text,cod_Producto,cod_Marca,cod_Familia,cod_Categoria);


                                               }
                                               else if (tipo_reporte.Rows.Count == 0 && cod_SubReporte == "")//no tiene subreporte
                                               {
                                                   //pasa

                                                   this.Session["cod_SubReporte"] = cod_SubReporte;

                                                   DataTable vistas = dplanning.ValidaTipoGestion(Convert.ToInt32(this.Session["company_id"].ToString().Trim()), this.Session["Planning_CodChannel"].ToString().Trim(), Convert.ToInt32(cod_Reporte)); //obtiene los valores booleanos para las vistas de paneles
                                                   this.Session["vistas"] = vistas;

                                                   CargarOpciones1();
                                                   insertar(cod_PDV, cod_Reporte, cod_SubReporte, cod_Periodo, LblPlanning.Text, cod_Producto, cod_Marca, cod_Familia, cod_Categoria);

                                               }
                                               else
                                               {
                                                   Pmensaje.CssClass = "MensajesSupervisor";
                                                   lblencabezado.Text = "Sr. Usuario";
                                                   lblmensajegeneral.Text = "El Reporte " + cod_Reporte + " no concuerda con el subreporte " + cod_SubReporte ;
                                                   Mensajes_Usuario();
                                                   return;
                                               }
                                           }
                                           else
                                           {
                                               Pmensaje.CssClass = "MensajesSupervisor";
                                               lblencabezado.Text = "Sr. Usuario";
                                               lblmensajegeneral.Text = "El Reporte " + cod_Reporte + "o el periodo "+cod_Periodo+ ". No es válido o no ha sido asignado a la campaña";
                                               Mensajes_Usuario();
                                               return;
                                           }
                                       }
                                       else
                                       {
                                           Pmensaje.CssClass = "MensajesSupervisor";
                                           lblencabezado.Text = "Sr. Usuario";
                                           lblmensajegeneral.Text = "El punto de venta " + cod_PDV + ". No es válido o no ha sido asignado a la campaña";
                                           Mensajes_Usuario();
                                           return;
                                       }

                               
                                    }
                             
                                }
                                else
                                {
                                    Gvlog.DataBind();
                                    Pmensaje.CssClass = "MensajesSupervisor";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de panel de puntos de venta válido. Por favor verifique la estructura que fue enviada a su correo.";
                                    Mensajes_Usuario();

                                    //System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                    //correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                    //correo.To.Add(this.Session["smail"].ToString());
                                    //correo.Subject = "Errores en archivo de asignación de puntos de venta";
                                    //correo.IsBodyHtml = true;
                                    //correo.Priority = System.Net.Mail.MailPriority.Normal;
                                    //string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                    //        this.Session["nameuser"].ToString() + "<br/>" + "<br/>" + 
                     
                                             

                                    //        "El archivo que usted seleccionó para la carga de asignación de puntos de venta no cumple con una estructura válida." + "<br/>" +
                                    //        "Por favor verifique que tenga 4 columnas" + "<br/>" +  "<br/>" +
                                    //        "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                    //        "Columna 1  : Nombre de Punto de Venta" + "<br/>" +
                                    //        "Columna 2  : Mercaderista"+ "<br/>" +
                                    //        "Columna 3  : Fecha inicio" + "<br/>" +                                            
                                    //        "Columna 4  : Fecha fin" + "<br/>" + "<br/>" + "<br/>" +
                                    //        "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizar los nombres de las columnas del archivo ." +
                                    //        "Pero tenga en cuenta que debe ingresar la información de los puntos de venta en ese orden." + "<br/>" + "<br/>" + "<br/>" +
                                    //        "Cordial Saludo" + "<br/>" + "Administrador Xplora" };

                                    //correo.Body = string.Concat(txtbody);

                                    //System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                    //cliente.Host = "mail.lucky.com.pe";

                                    //try
                                    //{
                                    //    cliente.Send(correo);
                                    //}
                                    //catch (System.Net.Mail.SmtpException)
                                    //{
                                    //}
                                }
                            }
                            else
                            {
                                Pmensaje.CssClass = "MensajesSupervisor";
                                lblencabezado.Text = "Sr. Usuario";
                                lblmensajegeneral.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                                Mensajes_Usuario();
                            }
                        }
                        catch (Exception ex)
                        {
                            Pmensaje.CssClass = "MensajesSupervisor";
                            lblencabezado.Text = "Sr. Usuario";
                            lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de panel de punto de venta. Por favor verifique que el nombre de la hoja donde estan los datos sea Hoja1";
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
    }
}