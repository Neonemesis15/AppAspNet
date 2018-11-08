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


namespace SIGE.Pages.Modulos.Planning
{
    public partial class Carga_PDVOPE : System.Web.UI.Page
    {
        // Llamada a los Web Services
        Facade_Proceso_Planning.Facade_Proceso_Planning wsPlanning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        
        // Conexión con la Base de Datos
        Conexion oCoon = new Conexion();
        //PointOfSale_PlanningOper PointOfSale_PlanningOper = new PointOfSale_PlanningOper(); 

        /// <summary>
        /// Mostrar el AspControl ModalPopupExtender 'ModalPopupMensaje'
        /// </summary>
        private void Mensajes_Usuario(){
            ModalPopupMensaje.Show();
        }

        /// <summary>
        /// Cargar el AspControl DropDownList 'CmbOpePlanning' los Planning Disponibles
        /// </summary>
        private void llenaOperativosAsignaPDVOPE(){
        
            DataTable dtStaffPlanning = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_VALIDACIONES_CARGAMASIVAPDVOPE", "Operativo", LblPlanning.Text, "none");

            if (dtStaffPlanning != null)
            {
                if (dtStaffPlanning.Rows.Count > 0){
                    
                    CmbOpePlanning.DataSource = dtStaffPlanning;
                    CmbOpePlanning.DataTextField = "Person_id";
                    CmbOpePlanning.DataValueField = "Person_id";
                    CmbOpePlanning.DataBind();                   

                }else{

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "No se ha seleccionado el personal de la campaña: " + LblPresupuestoPDV.Text;
                    Mensajes_Usuario();                   
                }
            }
        }

        #region Gestión y Manejo de Archivos Excel
        
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

                sFile = Server.MapPath("PDV_Planning") + "\\" + "Datos_Rutas.xls";

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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack){
                try{

                    LblPresupuestoPDV.Text = this.Session["PresupuestoPDVOPE"].ToString().Trim();
                    LblPlanning.Text = this.Session["id_planningPDVOPE"].ToString().Trim();
                    DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_EXPORTAEXCEL_DATOS_CARGA_RUTAS", LblPlanning.Text);
                    CreaExcel(ds);

                }catch{
                    //Pmensaje.CssClass = "MensajesSupervisor";
                    //lblencabezado.Text = "Sr. Usuario";
                    //lblmensajegeneral.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                    //Mensajes_Usuario();
                }
            }
        }

        protected void BtnCargaArchivo_Click(object sender, EventArgs e){

            if ((FileUpPDV.PostedFile != null) && (FileUpPDV.PostedFile.ContentLength > 0)){

                string fn = System.IO.Path.GetFileName(FileUpPDV.PostedFile.FileName);
                string SaveLocation = Server.MapPath("PDV_Planning") + "\\" + fn;

                if (SaveLocation != string.Empty){

                    if (FileUpPDV.FileName.ToLower().EndsWith(".xls")){
                       
                        OleDbConnection oConn1 = new OleDbConnection();
                        OleDbCommand oCmd = new OleDbCommand();
                        OleDbDataAdapter oDa = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();

                        DataTable dt = new DataTable();


                        FileUpPDV.PostedFile.SaveAs(SaveLocation);

                        // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                        oConn1.ConnectionString = 
                            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" 
                            + SaveLocation 
                            + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";

                        oConn1.Open();
                        oCmd.CommandText = "SELECT * FROM [Hoja1$]";
                        oCmd.Connection = oConn1;
                        oDa.SelectCommand = oCmd;
                        
                        try{
                            if (this.Session["scountry"].ToString() != null){

                                oDa.Fill(oDs);
                                dt = oDs.Tables[0];

                                if (dt.Columns.Count == 4){

                                    dt.Columns[0].ColumnName = "Cod_PDV";
                                    dt.Columns[1].ColumnName = "Cod_Mercaderista";
                                    dt.Columns[2].ColumnName = "Fecha inicio";
                                    dt.Columns[3].ColumnName = "Fecha fin";

                                    Gvlog.DataSource = dt;
                                    Gvlog.DataBind();

                                    for (int i = 0; i <= Gvlog.Rows.Count - 1; i++){

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

                                    string nombre_pdv;
                                    bool sigue = true;
                                    for (int i = 0; i <= dt.Rows.Count - 1; i++){

                                        llenaOperativosAsignaPDVOPE();
                                        
                                        nombre_pdv = dt.Rows[i][0].ToString().Trim();
                                        
                                        DataTable dtValidaNombre = 
                                            oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_VALIDACIONES_CARGAMASIVAPDVOPE", 
                                            "Nombre", 
                                            LblPlanning.Text, 
                                            dt.Rows[i][0].ToString().Trim());

                                        if (dtValidaNombre.Rows.Count > 0){

                                            CbmPDV.DataSource = dtValidaNombre;
                                            CbmPDV.DataTextField = "pdv_Name";
                                            CbmPDV.DataValueField = "id_MPOSPlanning";
                                            CbmPDV.DataBind();

                                            sigue = true;
                                            Gvlog.Rows[i].Cells[0].Text = dtValidaNombre.Rows[0][0].ToString().Trim();
                                        } else {
                                            Pmensaje.CssClass = "MensajesSupervisor";
                                            lblencabezado.Text = "Sr. Usuario";
                                            lblmensajegeneral.Text = "El punto de venta " + Gvlog.Rows[i].Cells[0].Text + ". No es válido o no ha sido asignado a la campaña";
                                            Mensajes_Usuario();
                                            sigue = false;
                                            i = dt.Rows.Count - 1;

                                        }

                                        if (sigue){
                                            try{
                                                
                                                CmbOpePlanning.Items.FindByText(Gvlog.Rows[i].Cells[1].Text.ToUpper()).Selected = true;
                                                Gvlog.Rows[i].Cells[1].Text = CmbOpePlanning.SelectedItem.Value;

                                            }catch{
                                                
                                                Pmensaje.CssClass = "MensajesSupervisor";
                                                lblencabezado.Text = "Sr. Usuario";
                                                lblmensajegeneral.Text = "El mercaderista " 
                                                    + Gvlog.Rows[i].Cells[1].Text 
                                                    + ". No es válido o no ha sido asignado a la campaña";
                                                Mensajes_Usuario();
                                                sigue = false;
                                                i = dt.Rows.Count - 1;
                                            }
                                        }

                                        if (sigue){
                                            try{
                                                DateTime FechainicialPlanning = DateTime.Parse(this.Session["Fechainicial"].ToString().Trim());
                                                DateTime FechafinalPlanning = DateTime.Parse(this.Session["Fechafinal"].ToString().Trim());
                                                DateTime FechaInicial = DateTime.Parse(Gvlog.Rows[i].Cells[2].Text);
                                                DateTime Fechafinal = DateTime.Parse(Gvlog.Rows[i].Cells[3].Text);
                                                if (FechaInicial > Fechafinal){

                                                    Pmensaje.CssClass = "MensajesSupervisor";
                                                    lblencabezado.Text = "Sr. Usuario";
                                                    lblmensajegeneral.Text = "La fecha inicial no puede ser mayor a la fecha final";
                                                    Mensajes_Usuario();
                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }

                                                if (FechaInicial < FechainicialPlanning || Fechafinal > FechafinalPlanning){
                                                    Pmensaje.CssClass = "MensajesSupervisor";
                                                    lblencabezado.Text = "Sr. Usuario";
                                                    lblmensajegeneral.Text = "Las fechas deben estar dentro del rango : " + FechainicialPlanning.ToShortDateString() + " y " + FechafinalPlanning.ToShortDateString() + " que corresponden a las fechas de ejecución de la Campaña";
                                                    Mensajes_Usuario();
                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }

                                                if (FechaInicial < DateTime.Today){
                                                    Pmensaje.CssClass = "MensajesSupervisor";
                                                    lblencabezado.Text = "Sr. Usuario";
                                                    lblmensajegeneral.Text = "La fecha inicial debe ser igual o superior a la fecha actual";
                                                    Mensajes_Usuario();
                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }

                                                Gvlog.Rows[i].Cells[2].Text = Gvlog.Rows[i].Cells[2].Text + " 01:00:00.000";
                                                Gvlog.Rows[i].Cells[3].Text = Gvlog.Rows[i].Cells[3].Text + " 23:59:00.000";
                                            } catch {
                                                
                                                Pmensaje.CssClass = "MensajesSupervisor";
                                                lblencabezado.Text = "Sr. Usuario";
                                                lblmensajegeneral.Text = "Formato de fecha no valido. Por favor verifique (dd/mm/aaaa)";
                                                Mensajes_Usuario();
                                                sigue = false;
                                                i = dt.Rows.Count - 1;
                                            }
                                        }
                                    }

                                    if (sigue){
                                        for (int i = 0; i <= Gvlog.Rows.Count - 1; i++){
                                            DataTable dtconsulta = 
                                                wsPlanning.Get_AsignacionDuplicadaPDV(
                                                Convert.ToInt32(Gvlog.Rows[i].Cells[0].Text), 
                                                Convert.ToInt32(Gvlog.Rows[i].Cells[1].Text), 
                                                LblPlanning.Text);

                                            if (dtconsulta != null){

                                                if (dtconsulta.Rows.Count == 0) { // 1 > 0

                                                    PointOfSale_PlanningOper obj = new PointOfSale_PlanningOper();
                                                    
                                                    EPointOfSale_PlanningOper RegistrarPointOfSale_PlanningOper = 
                                                        obj.RegistrarAsignPDVaOperativo(
                                                        Convert.ToInt32(Gvlog.Rows[i].Cells[0].Text),
                                                        LblPlanning.Text, 
                                                        Convert.ToInt32(Gvlog.Rows[i].Cells[1].Text), 
                                                        Convert.ToDateTime(Gvlog.Rows[i].Cells[2].Text), 
                                                        Convert.ToDateTime(Gvlog.Rows[i].Cells[3].Text),0,
                                                        true, 
                                                        Convert.ToString(this.Session["sUser"]), 
                                                        DateTime.Now, 
                                                        Convert.ToString(this.Session["sUser"]), 
                                                        DateTime.Now);
                                                   
                                                    obj.RegistrarTBL_EQUIPO_PTO_VENTA(
                                                        Convert.ToInt32(Gvlog.Rows[i].Cells[0].Text), 
                                                        LblPlanning.Text, 
                                                        Convert.ToInt32(Gvlog.Rows[i].Cells[1].Text), 
                                                        Convert.ToDateTime(Gvlog.Rows[i].Cells[2].Text), 
                                                        Convert.ToDateTime(Gvlog.Rows[i].Cells[3].Text));
                                                } else {
                                                    // metodo de actualizacion 
                                                    PointOfSale_PlanningOper obj = new PointOfSale_PlanningOper();
                                                    EPointOfSale_PlanningOper ActualizarPointOfSale_PlanningOper = 
                                                        obj.ActualizarAsignPDVaOperativo(
                                                            Convert.ToInt32(dtconsulta.Rows[0][6].ToString().Trim()),
                                                            Convert.ToDateTime(Gvlog.Rows[i].Cells[2].Text),
                                                            Convert.ToDateTime(Gvlog.Rows[i].Cells[3].Text),
                                                            Convert.ToString(this.Session["sUser"]), 
                                                            DateTime.Now);
                                                    try{
                                                        PointOfSale_PlanningOper ActualizarTBL_EQUIPO_PTO_VENTA = new PointOfSale_PlanningOper();
                                                        ActualizarTBL_EQUIPO_PTO_VENTA.ActualizarTBL_EQUIPO_PTO_VENTA(dtconsulta.Rows[0][5].ToString().Trim(), LblPlanning.Text,
                                                            Convert.ToInt32(Gvlog.Rows[i].Cells[1].Text), Convert.ToDateTime(Gvlog.Rows[i].Cells[2].Text), Convert.ToDateTime(Gvlog.Rows[i].Cells[3].Text));
                                                    }catch (Exception ex){
                                                        Pmensaje.CssClass = "MensajesSupervisor";
                                                        lblencabezado.Text = "Sr. Usuario";
                                                        lblmensajegeneral.Text = "La campaña no existe en la base de datos de Nextel " 
                                                            + ". Consulte con el Administrador Xplora";
                                                        Mensajes_Usuario();
                                                    }
                                                }
                                            }
                                        }

                                        Pmensaje.CssClass = "MensajesSupConfirm";
                                        lblencabezado.Text = "Sr. Usuario";
                                        lblmensajegeneral.Text = "Se ha asignado con éxito los puntos de venta para la campaña : " + LblPresupuestoPDV.Text;
                                        Mensajes_Usuario();
                                    }
                                } else {
                                    Gvlog.DataBind();
                                    Pmensaje.CssClass = "MensajesSupervisor";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de asignación "
                                        +"de puntos de venta válido. Por favor verifique la estructura que fue enviada a su correo.";
                                    Mensajes_Usuario();

                                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                    correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                    correo.To.Add(this.Session["smail"].ToString());
                                    correo.Subject = "Errores en archivo de asignación de puntos de venta";
                                    correo.IsBodyHtml = true;
                                    correo.Priority = System.Net.Mail.MailPriority.Normal;
                                    string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                            this.Session["nameuser"].ToString() 
                                            + "<br/>" 
                                            + "<br/>" 
                                            + "El archivo que usted seleccionó para la carga de asignación de puntos de venta no cumple con una estructura válida." 
                                            + "<br/>" 
                                            + "Por favor verifique que tenga 4 columnas" 
                                            + "<br/>" 
                                            + "<br/>" 
                                            + "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" 
                                            + "<br/>" 
                                            + "<br/>" 
                                            + "Columna 1  : Nombre de Punto de Venta" 
                                            + "<br/>" 
                                            + "Columna 2  : Mercaderista"
                                            + "<br/>" 
                                            + "Columna 3  : Fecha inicio" 
                                            + "<br/>" 
                                            + "Columna 4  : Fecha fin" 
                                            + "<br/>" 
                                            + "<br/>" 
                                            + "<br/>" 
                                            + "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente "
                                            + ", usted puede personalizar los nombres de las columnas del archivo ." 
                                            + "Pero tenga en cuenta que debe ingresar la información de los puntos de venta en ese orden." 
                                            + "<br/>" 
                                            + "<br/>" 
                                            + "<br/>" 
                                            + "Cordial Saludo" + "<br/>" + "Administrador Xplora" };

                                    correo.Body = string.Concat(txtbody);

                                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                    cliente.Host = "mail.lucky.com.pe";

                                    try {
                                        
                                        cliente.Send(correo);

                                    }catch (System.Net.Mail.SmtpException){

                                    }
                                }
                            }else{
                                Pmensaje.CssClass = "MensajesSupervisor";
                                lblencabezado.Text = "Sr. Usuario";
                                lblmensajegeneral.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                                Mensajes_Usuario();
                            }
                        }
                        catch (Exception ex){
                            Pmensaje.CssClass = "MensajesSupervisor";
                            lblencabezado.Text = "Sr. Usuario";
                            lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de puntos de venta válido. "
                                +"Por favor verifique que el nombre de la hoja donde estan los datos sea Hoja1";
                            Mensajes_Usuario();
                        }
                        oConn1.Close();
                    }else{
                        
                        Pmensaje.CssClass = "MensajesSupervisor";
                        lblencabezado.Text = "Sr. Usuario";
                        lblmensajegeneral.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                        Mensajes_Usuario();
                    }
                }
            } else{
                Pmensaje.CssClass = "MensajesSupervisor";
                lblencabezado.Text = "Sr. Usuario";
                lblmensajegeneral.Text = "Es indispensable seleccionar un archivo.";
                Mensajes_Usuario();
            }
        }
    }
}
