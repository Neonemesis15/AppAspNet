using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using Lucky.Business.Common.Application;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


namespace SIGE.Pages.Modulos.Planning
{
    public partial class carga_PDV : System.Web.UI.Page
    {
        private Conexion oCoon = new Conexion();
        private NodeType oTypeNode = new NodeType();
        private PuntosDV oPDV = new PuntosDV();
        private Conexion oConn = new Lucky.Data.Conexion();

        private Facade_Proceso_Planning.Facade_Proceso_Planning Planning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE Presupuesto = new SIGE.Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE();

        private bool DivisiónPolitica = true;
        private bool CanalValido;
        private bool TipoAgrupacion;
        private bool Agrupacion;
        private bool segmento;
        private bool TipoDoc;
        private bool malla;
        private bool sector;
        private bool oficina;
        private bool Distribuidora;
        private bool Pdv_Contact;

        public static void EmbeddedImagesLinked()
        {
            
        } // End EmbeddedImagesLinked
        
        private void Limpiar_InformacionPDVPlanning()
        {
            TxtCodPlanningPDV.Text = "";
            CmbSelPresupuestoPDV.Text = "0";
        }



        #region excel
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

                sFile = Server.MapPath("PDV_Planning") + "\\" + "DATOS_CARGA_PTOVENTA.xls";

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

                    oHoja.Range["A2", "B" + (ds.Tables[i].Rows.Count+2 ).ToString()].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDash;
                    oHoja.Range["A2", "B" + (ds.Tables[i].Rows.Count+2).ToString()].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;

                    


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
            //UpdateProgressContext2(); para el progress

            if (!IsPostBack)
            {
                try
                {
                    LlenaPresupuestosAsignados();
                    Validarobjeto();
                    OpcUnoAUno.Style.Value = "Display:none;";
                    OpcMasiva.Style.Value = "Display:none;";



                }
                catch
                {
                    Pmensaje.CssClass = "MensajesSupervisor";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "Es indispensable que cierre sesión he inicie nuevamente. Ha dejado mucho tiempo inactivo el sistema y se perderá la información hasta ahora ingresada.";
                    Mensajes_Usuario();
                }
            }
        }

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

                progress.CurrentOperationText = "Precios Canal Mayorista";

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
        private void LlenaPresupuestosAsignados()
        {
            DataTable dt = new DataTable();
            dt = Presupuesto.Presupuesto_Search(Convert.ToInt32(this.Session["companyid"].ToString().Trim()));

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    CmbSelPresupuestoPDV.DataSource = dt;
                    CmbSelPresupuestoPDV.DataValueField = "Numero_Presupuesto";
                    CmbSelPresupuestoPDV.DataTextField = "Nombre";
                    CmbSelPresupuestoPDV.DataBind();
                }
            }
            dt = null;
        }
        private void Validarobjeto()
        {
            try
            {
                if (this.Session["InsertaConsultaPDV"].ToString().Trim() == "true")
                {
                    CmbSelPresupuestoPDV.Text = this.Session["InsertaConsultaPDVPresupuesto"].ToString().Trim();
                    CmbSelPresupuestoPDV.Enabled = false;
                    //ejecutar método para obtener id del planning generado 
                    DataTable dt = Planning.ObtenerIdPlanning(CmbSelPresupuestoPDV.SelectedValue);
                    TxtCodPlanningPDV.Text = dt.Rows[0]["Planning"].ToString().Trim();
                    LblCanal.Text = dt.Rows[0]["Channel_Name"].ToString().Trim();
                    LblCountry.Text = dt.Rows[0]["Name_Country"].ToString().Trim();
                    this.Session["company_id"] = dt.Rows[0]["Company_id"].ToString().Trim();
                    this.Session["CodChannel"] = dt.Rows[0]["Planning_CodChannel"].ToString().Trim();
                    RbtnSelTipoCarga.Enabled = true;
                }
            }
            catch
            {
            }
        }

        protected void CmbSelPresupuestoPDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpcUnoAUno.Style.Value = "Display:none;";
            OpcMasiva.Style.Value = "Display:none;";
            TxtCodigoPDV.Enabled = false;
            TxtCodigoPDV.Text = "";
            LblNamePDV.Text = "------------------------------------------------------------------------------------------------";
            LblDirPDV.Text = "------------------------------------------------------------------------------------------------";            
            LblCiudad.Text = "";
            LblTipoAgrupacion.Text = "";
            LblAgrupacion.Text = "";
            LblOficina.Text = "";
            LblMalla.Text = "";
            LblSector.Text = "";
            BtnCargaUnoaUno.Visible = false;
            RbtnSelTipoCarga.Items[0].Selected = false;
            RbtnSelTipoCarga.Items[1].Selected = false;


            if (CmbSelPresupuestoPDV.SelectedValue != "0")
            {
                RbtnSelTipoCarga.Enabled = true;
                //ejecutar método para obtener id del planning generado 
                DataTable dt = Planning.ObtenerIdPlanning(CmbSelPresupuestoPDV.SelectedValue);
                TxtCodPlanningPDV.Text = dt.Rows[0]["Planning"].ToString().Trim();
                LblCanal.Text = dt.Rows[0]["Channel_Name"].ToString().Trim();
                LblCountry.Text = dt.Rows[0]["Name_Country"].ToString().Trim();
                this.Session["company_id"] = dt.Rows[0]["Company_id"].ToString().Trim();
                this.Session["CodChannel"] = dt.Rows[0]["Planning_CodChannel"].ToString().Trim();

            }
            else
            {
                RbtnSelTipoCarga.Enabled = false;
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario seleccionar un presupuesto";

                Pmensaje.CssClass = this.Session["cssclass"].ToString();
                lblencabezado.Text = this.Session["encabemensa"].ToString();
                lblmensajegeneral.Text = this.Session["mensaje"].ToString();

                Mensajes_Usuario();
            }
        }


        private void comboTipoMercado()
        {
            DataSet ds = oCoon.ejecutarDataSet("UP_WEB_LLENACOMBOS", 11);
            //se llena tipo de nodo comercial en cargar PDV masivamente
            CmbTipMerc.DataSource = ds;
            CmbTipMerc.DataTextField = "NodeComType_name";
            CmbTipMerc.DataValueField = "idNodeComType";
            CmbTipMerc.DataBind();
            ds = null;
        }
        private void comboNodos()
        {
            DataSet ds = oCoon.ejecutarDataSet("UP_WEB_COMBONODOS", CmbTipMerc.SelectedValue);
            //se llena nodos en PDV
            cmbNodoCom.DataSource = ds;
            cmbNodoCom.DataTextField = "commercialNodeName";
            cmbNodoCom.DataValueField = "NodeCommercial";
            cmbNodoCom.DataBind();
            ds = null;
        }
        private void combosegmenpdv()
        {
            DataSet ds = oCoon.ejecutarDataSet("UP_WEB_LLENACOMBOS", 50);
            //se llena segmentos en PDV
            CmbSelSegPDV.DataSource = ds;
            CmbSelSegPDV.DataTextField = "Segment_Name";
            CmbSelSegPDV.DataValueField = "id_Segment";
            CmbSelSegPDV.DataBind();
            ds = null;
        }
        //-- Description:       <llena el combo de tipos de documento en el maestro>
        private void combtipDocCli()
        {
            try
            {
                DataSet ds = oCoon.ejecutarDataSet("UP_WEB_LLENACOMBOTypeDocument", this.Session["scountry"].ToString());
                cmbTipDocCli.DataSource = ds;
                cmbTipDocCli.DataTextField = "Type_documento";
                cmbTipDocCli.DataValueField = "id_typeDocument";
                cmbTipDocCli.DataBind();
                ds = null;
            }
            catch
            {
                Pmensaje.CssClass = "MensajesSupervisor";
                lblencabezado.Text = "Sr. Usuario";
                lblmensajegeneral.Text = "Es indispensable que cierre sesión he inicie nuevamente. Ha dejado mucho tiempo inactivo el sistema y se perderá la información hasta ahora ingresada.";
                Mensajes_Usuario();
            }
        }

        private void combomallas()
        {
            try
            {
                DataSet ds = Planning.Get_MallasSector("mallas", Convert.ToInt32(this.Session["company_id"].ToString().Trim()), 0);
                CmbMalla.DataSource = ds;
                CmbMalla.DataTextField = "malla";
                CmbMalla.DataValueField = "id_malla";
                CmbMalla.DataBind();
                ds = null;
            }
            catch
            {
                Pmensaje.CssClass = "MensajesSupervisor";
                lblencabezado.Text = "Sr. Usuario";
                lblmensajegeneral.Text = "Es indispensable que cierre sesión he inicie nuevamente. Ha dejado mucho tiempo inactivo el sistema y se perderá la información hasta ahora ingresada.";
                Mensajes_Usuario();
            }
        }

        private void combosector()
        {
            try
            {
                DataSet ds = Planning.Get_MallasSector("sectores", Convert.ToInt32(this.Session["company_id"].ToString().Trim()), Convert.ToInt32(CmbMalla.SelectedValue));

                CmbSector.DataSource = ds;
                CmbSector.DataTextField = "Sector";
                CmbSector.DataValueField = "id_sector";
                CmbSector.DataBind();
                ds = null;
            }
            catch
            {
                Pmensaje.CssClass = "MensajesSupervisor";
                lblencabezado.Text = "Sr. Usuario";
                lblmensajegeneral.Text = "Es indispensable que cierre sesión he inicie nuevamente. Ha dejado mucho tiempo inactivo el sistema y se perderá la información hasta ahora ingresada.";
                Mensajes_Usuario();
            }
        }

        private void combooficina()
        {
            DataSet ds = Get_Administrativo.llenaCombosSector(Convert.ToInt32(this.Session["company_id"].ToString().Trim()));
            CmbOficina.DataSource = ds.Tables[3];
            CmbOficina.DataTextField = "Name_oficina";
            CmbOficina.DataValueField = "cod_Oficina";
            CmbOficina.DataBind();
        }

        private void comboDistribuidora()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 62);
            //se llena canales en PDV
            cmbDexPDV.DataSource = ds;
            cmbDexPDV.DataTextField = "Dex_Name";
            cmbDexPDV.DataValueField = "Id_Dex";
            cmbDexPDV.DataBind();
            //cmbDexPDV.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
        }

        private void Mensajes_Usuario()
        {
            ModalPopupMensaje.Show();
        }
        protected void BtnCargaArchivo_Click(object sender, EventArgs e)
        {
            if ((FileUpPDV.PostedFile != null) && (FileUpPDV.PostedFile.ContentLength > 0) && (CmbSelPresupuestoPDV.Text != "0"))
            {
                string fn = System.IO.Path.GetFileName(FileUpPDV.PostedFile.FileName);
                string SaveLocation = Server.MapPath("PDV_Planning") + "\\" + fn;

                if (SaveLocation != string.Empty)
                {
                    if (FileUpPDV.FileName.ToLower().EndsWith(".xls"))
                    {
                        // string Destino = Server.MapPath(null) + "\\PDV_Planning\\" + Path.GetFileName(FileUpPDV.PostedFile.FileName);
                        OleDbConnection oConn1 = new OleDbConnection();
                        OleDbCommand oCmd = new OleDbCommand();
                        OleDbDataAdapter oDa = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();
                        DataTable dt = new DataTable();

                        FileUpPDV.PostedFile.SaveAs(SaveLocation);

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

                                DataTable dtdivpol = oCoon.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", this.Session["scountry"].ToString());
                                ECountry oescountry = new ECountry();
                                if (dtdivpol != null)
                                {
                                    if (dtdivpol.Rows.Count > 0)
                                    {
                                        oescountry.CountryDpto = Convert.ToBoolean(dtdivpol.Rows[0]["Country_Dpto"].ToString().Trim());
                                        oescountry.CountryCiudad = Convert.ToBoolean(dtdivpol.Rows[0]["Country_Ciudad"].ToString().Trim());
                                        oescountry.CountryDistrito = Convert.ToBoolean(dtdivpol.Rows[0]["Country_Distrito"].ToString().Trim());
                                        oescountry.CountryBarrio = Convert.ToBoolean(dtdivpol.Rows[0]["Country_Barrio"].ToString().Trim());
                                    }
                                }

                                dt = oDs.Tables[0];
                                int numcol = 1; //determina el número de columnas para el datatable
                                if (dt.Columns.Count == numcol)
                                {
                                    dt.Columns[0].ColumnName = "Código Punto de Venta";


                                    int cargados = 0;
                                    int noexisten = 0;
                                    int duplicados = 0;

                                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                    {

                                        DataTable dtl = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_PDVCODEXCLIENTEYCANAL", this.Session["company_id"].ToString().Trim(), this.Session["CodChannel"].ToString().Trim(), dt.Rows[i]["Código Punto de Venta"].ToString().Trim());
                                        if (dt.Rows.Count > 0)
                                        {
                                            LblNamePDV.Text = dtl.Rows[0]["pdv_Name"].ToString().Trim();
                                            LblDirPDV.Text = dtl.Rows[0]["pdv_Address"].ToString().Trim();
                                            LblCiudad.Text = dtl.Rows[0]["pdv_codProvince"].ToString().Trim();
                                            LblTipoAgrupacion.Text = dtl.Rows[0]["idNodeComType"].ToString().Trim();
                                            LblAgrupacion.Text = dtl.Rows[0]["NodeCommercial"].ToString().Trim();
                                            LblOficina.Text = dtl.Rows[0]["cod_Oficina"].ToString().Trim();
                                            LblMalla.Text = dtl.Rows[0]["id_malla"].ToString().Trim();
                                            LblSector.Text = dtl.Rows[0]["id_sector"].ToString().Trim();



                                            DataTable dtCodPDVExiste = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_SEARCH_PDV_MASIVO", 5, "none", "none", dt.Rows[i]["Código Punto de Venta"].ToString().Trim(), this.Session["company_id"].ToString());
                                            DataTable dtDuplicado = Planning.Get_DuplicadoPDVPlanning(Convert.ToInt32(dtCodPDVExiste.Rows[0][0].ToString().Trim()), TxtCodPlanningPDV.Text);
                                            if (dtDuplicado != null)
                                            {
                                                if (dtDuplicado.Rows.Count == 0)
                                                {


                                                    oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_INSERTARPDVPLANNING",
                                                        Convert.ToInt32(dtCodPDVExiste.Rows[0][0].ToString().Trim()),
                                                        TxtCodPlanningPDV.Text,
                                                        LblCiudad.Text,
                                                        Convert.ToInt32(LblTipoAgrupacion.Text),
                                                        LblAgrupacion.Text,
                                                        Convert.ToInt64(LblOficina.Text),
                                                        0,
                                                        Convert.ToInt32(LblSector.Text),
                                                        true,
                                                        this.Session["sUser"].ToString().Trim(),
                                                        DateTime.Now,
                                                        this.Session["sUser"].ToString().Trim(),
                                                        DateTime.Now,
                                                        "");
                                                    cargados = cargados + 1;
                                                   
                                                  
                                                }
                                                else
                                                {
                                                    duplicados = duplicados + 1;

                                                   
                                                }


                                            }

                                        }
                                        else
                                        {
                                            noexisten = noexisten + 1;
                                        }



                                        #region
                                        //if (dt.Rows[i]["País"].ToString().Equals(LblCountry.Text))
                                        //{
                                        //    dt.Rows[i]["País"] = this.Session["scountry"].ToString();

                                        //    #region Valida Pais
                                        //    //REVISION DE PAIS
                                        //    if (oescountry.CountryDpto)
                                        //    {
                                        //        DataTable dtcountry = new DataTable();
                                        //        dtcountry = oCoon.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", this.Session["scountry"].ToString());
                                        //        if (dtcountry != null)
                                        //        {
                                        //            if (dtcountry.Rows.Count > 1)
                                        //            {
                                        //                cmbSelDpto.DataSource = dtcountry;
                                        //                cmbSelDpto.DataTextField = "Name_dpto";
                                        //                cmbSelDpto.DataValueField = "cod_dpto";
                                        //                cmbSelDpto.DataBind();

                                        //                try
                                        //                {
                                        //                    cmbSelDpto.Items.FindByText(dt.Rows[i]["Departamento"].ToString().Trim()).Selected = true;
                                        //                    dt.Rows[i]["Departamento"] = cmbSelDpto.SelectedItem.Value;
                                        //                }
                                        //                catch
                                        //                {
                                        //                    Pmensaje.CssClass = "MensajesSupervisor";
                                        //                    lblencabezado.Text = "Sr. Usuario";
                                        //                    lblmensajegeneral.Text = "El Departamento: " + dt.Rows[i]["Departamento"].ToString().Trim() + ", no es válido para el país " + LblCountry.Text + ". Por favor verifique";
                                        //                    Mensajes_Usuario();
                                        //                    i = dt.Rows.Count;
                                        //                    DivisiónPolitica = false;
                                        //                }
                                        //            }
                                        //            else
                                        //            {
                                        //                Pmensaje.CssClass = "MensajesSupervisor";
                                        //                lblencabezado.Text = "Sr. Usuario";
                                        //                lblmensajegeneral.Text = "No se han creado Departamentos para el país: " + LblCountry.Text + ". Por favor verifique";
                                        //                Mensajes_Usuario();
                                        //                i = dt.Rows.Count;
                                        //                DivisiónPolitica = false;
                                        //            }
                                        //        }
                                        //        dtcountry = null;
                                        //    }
                                        //    else
                                        //    {
                                        //        cmbSelDpto.Items.Clear();
                                        //        dt.Rows[i]["Departamento"] = "";
                                        //        DivisiónPolitica = true;
                                        //    } 
                                        //    #endregion

                                        //    if (DivisiónPolitica)
                                        //    {
                                        //        if (oescountry.CountryDpto)// si tiene departamento
                                        //        {
                                        //            #region Valida Ciudad
                                        //            if (oescountry.CountryCiudad)// si tiene ciudad
                                        //            {
                                        //                DataTable dtcity = new DataTable();
                                        //                dtcity = oCoon.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITY", cmbSelDpto.Text);
                                        //                if (dtcity.Rows.Count > 1)
                                        //                {
                                        //                    cmbSelProvince.DataSource = dtcity;
                                        //                    cmbSelProvince.DataTextField = "Name_City";
                                        //                    cmbSelProvince.DataValueField = "cod_City";
                                        //                    cmbSelProvince.DataBind();

                                        //                    try
                                        //                    {
                                        //                        cmbSelProvince.Items.FindByText(dt.Rows[i]["Provincia"].ToString().Trim()).Selected = true;
                                        //                        dt.Rows[i]["Provincia"] = cmbSelProvince.SelectedItem.Value;
                                        //                    }
                                        //                    catch
                                        //                    {
                                        //                        Pmensaje.CssClass = "MensajesSupervisor";
                                        //                        lblencabezado.Text = "Sr. Usuario";
                                        //                        lblmensajegeneral.Text = "La Ciudad: " + dt.Rows[i]["Provincia"].ToString().Trim() + ", no es válida para el Departamento: " + cmbSelDpto.SelectedItem.Text + ". Por favor verifique";
                                        //                        Mensajes_Usuario();
                                        //                        i = dt.Rows.Count;
                                        //                        DivisiónPolitica = false;
                                        //                    }
                                        //                }
                                        //                else
                                        //                {
                                        //                    Pmensaje.CssClass = "MensajesSupervisor";
                                        //                    lblencabezado.Text = "Sr. Usuario";
                                        //                    lblmensajegeneral.Text = "No se han creado ciudades para el Departamento: " + cmbSelDpto.SelectedItem.Text + ". Por favor verifique";
                                        //                    Mensajes_Usuario();
                                        //                    i = dt.Rows.Count;
                                        //                    DivisiónPolitica = false;
                                        //                }
                                        //                dtcity = null;
                                        //            }
                                        //            else
                                        //            {
                                        //                DivisiónPolitica = true;
                                        //                cmbSelProvince.Items.Clear();
                                        //                dt.Rows[i]["Provincia"] = "";
                                        //            } 
                                        //            #endregion

                                        //            if (DivisiónPolitica)
                                        //            {
                                        //                if (oescountry.CountryCiudad)// si tiene ciudad
                                        //                {
                                        //                    #region Valida Distrito
                                        //                    if (oescountry.CountryDistrito)// si tiene distrito
                                        //                    {
                                        //                        DataTable dtdistrito = new DataTable();
                                        //                        dtdistrito = oCoon.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITO", cmbSelProvince.Text);
                                        //                        if (dtdistrito.Rows.Count > 1)
                                        //                        {
                                        //                            cmbSelDistrict.DataSource = dtdistrito;
                                        //                            cmbSelDistrict.DataTextField = "Name_Local";
                                        //                            cmbSelDistrict.DataValueField = "cod_District";
                                        //                            cmbSelDistrict.DataBind();
                                        //                            try
                                        //                            {
                                        //                                cmbSelDistrict.Items.FindByText(dt.Rows[i]["Distrito"].ToString().Trim()).Selected = true;
                                        //                                dt.Rows[i]["Distrito"] = cmbSelDistrict.SelectedItem.Value;
                                        //                            }
                                        //                            catch
                                        //                            {
                                        //                                Pmensaje.CssClass = "MensajesSupervisor";
                                        //                                lblencabezado.Text = "Sr. Usuario";
                                        //                                lblmensajegeneral.Text = "El Distrito: " + dt.Rows[i]["Distrito"].ToString().Trim() + ", no es válido para la ciudad " + cmbSelProvince.SelectedItem.Text + ". Por favor verifique";
                                        //                                Mensajes_Usuario();
                                        //                                i = dt.Rows.Count;
                                        //                                DivisiónPolitica = false;
                                        //                            }
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            Pmensaje.CssClass = "MensajesSupervisor";
                                        //                            lblencabezado.Text = "Sr. Usuario";
                                        //                            lblmensajegeneral.Text = "No se han creado Distritos para la ciudad : " + cmbSelProvince.SelectedItem.Text + ". Por favor verifique";
                                        //                            Mensajes_Usuario();
                                        //                            i = dt.Rows.Count;
                                        //                            DivisiónPolitica = false;
                                        //                        }
                                        //                        dtdistrito = null;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        DivisiónPolitica = true;
                                        //                        cmbSelDistrict.Items.Clear();
                                        //                        dt.Rows[i]["Distrito"] = "";
                                        //                    } 
                                        //                    #endregion
                                        //                }
                                        //                else
                                        //                {
                                        //                    #region Combos Distrito con departamento
                                        //                    if (oescountry.CountryDistrito)// si tiene distrito
                                        //                    {
                                        //                        DataTable dtdistrito = new DataTable();
                                        //                        dtdistrito = oCoon.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONDEPTO", cmbSelDpto.Text);
                                        //                        if (dtdistrito.Rows.Count > 1)
                                        //                        {
                                        //                            cmbSelDistrict.DataSource = dtdistrito;
                                        //                            cmbSelDistrict.DataTextField = "Name_Local";
                                        //                            cmbSelDistrict.DataValueField = "cod_District";
                                        //                            cmbSelDistrict.DataBind();
                                        //                            try
                                        //                            {
                                        //                                cmbSelDistrict.Items.FindByText(dt.Rows[i]["Distrito"].ToString().Trim()).Selected = true;
                                        //                                dt.Rows[i]["Distrito"] = cmbSelDistrict.SelectedItem.Value;
                                        //                            }
                                        //                            catch
                                        //                            {
                                        //                                Pmensaje.CssClass = "MensajesSupervisor";
                                        //                                lblencabezado.Text = "Sr. Usuario";
                                        //                                lblmensajegeneral.Text = "El Distrito: " + dt.Rows[i]["Distrito"].ToString().Trim() + ", no es válido para el Dpto. " + cmbSelDpto.SelectedItem.Text + ". Por favor verifique";
                                        //                                Mensajes_Usuario();
                                        //                                i = dt.Rows.Count;
                                        //                                DivisiónPolitica = false;
                                        //                            }
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            Pmensaje.CssClass = "MensajesSupervisor";
                                        //                            lblencabezado.Text = "Sr. Usuario";
                                        //                            lblmensajegeneral.Text = "No se han creado Distritos para el departamento : " + cmbSelDpto.SelectedItem.Text + ". Por favor verifique";
                                        //                            Mensajes_Usuario();
                                        //                            i = dt.Rows.Count;
                                        //                            DivisiónPolitica = false;
                                        //                        }
                                        //                        dtdistrito = null;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        cmbSelDistrict.Items.Clear();
                                        //                        dt.Rows[i]["Distrito"] = "";
                                        //                    } 
                                        //                    #endregion
                                        //                }
                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            #region Valida y llena ciudades con País
                                        //            if (oescountry.CountryCiudad)//si tiene ciudad
                                        //            {
                                        //                DataTable dtcity = new DataTable();
                                        //                dtcity = oCoon.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITYCONPAIS", this.Session["scountry"].ToString());
                                        //                if (dtcity.Rows.Count > 1)
                                        //                {
                                        //                    cmbSelProvince.DataSource = dtcity;
                                        //                    cmbSelProvince.DataTextField = "Name_City";
                                        //                    cmbSelProvince.DataValueField = "cod_City";
                                        //                    cmbSelProvince.DataBind();
                                        //                    try
                                        //                    {
                                        //                        cmbSelProvince.Items.FindByText(dt.Rows[i]["Provincia"].ToString().Trim()).Selected = true;
                                        //                        dt.Rows[i]["Provincia"] = cmbSelProvince.SelectedItem.Value;
                                        //                    }
                                        //                    catch
                                        //                    {
                                        //                        Pmensaje.CssClass = "MensajesSupervisor";
                                        //                        lblencabezado.Text = "Sr. Usuario";
                                        //                        lblmensajegeneral.Text = "La Ciudad: " + dt.Rows[i]["Provincia"].ToString().Trim() + ", no es válida para el País " + LblCountry.Text + ". Por favor verifique";
                                        //                        Mensajes_Usuario();
                                        //                        i = dt.Rows.Count;
                                        //                        DivisiónPolitica = false;
                                        //                    }
                                        //                }
                                        //                else
                                        //                {
                                        //                    Pmensaje.CssClass = "MensajesSupervisor";
                                        //                    lblencabezado.Text = "Sr. Usuario";
                                        //                    lblmensajegeneral.Text = "No se han creado Ciudades para el País: " + LblCountry.Text + ". Por favor verifique";
                                        //                    Mensajes_Usuario();
                                        //                    i = dt.Rows.Count;
                                        //                    DivisiónPolitica = false;
                                        //                }
                                        //                dtcity = null;
                                        //            }
                                        //            else
                                        //            {
                                        //                cmbSelProvince.Items.Clear();
                                        //                dt.Rows[i]["Provincia"] = "";
                                        //            } 
                                        //            #endregion

                                        //            if (DivisiónPolitica)
                                        //            {
                                        //                if (oescountry.CountryCiudad)// si tiene ciudad
                                        //                {
                                        //                    if (oescountry.CountryDistrito)// si tiene distrito
                                        //                    {
                                        //                        DataTable dtdistrito = new DataTable();
                                        //                        dtdistrito = oCoon.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITO", cmbSelProvince.Text);
                                        //                        if (dtdistrito.Rows.Count > 1)
                                        //                        {
                                        //                            cmbSelDistrict.DataSource = dtdistrito;
                                        //                            cmbSelDistrict.DataTextField = "Name_Local";
                                        //                            cmbSelDistrict.DataValueField = "cod_District";
                                        //                            cmbSelDistrict.DataBind();
                                        //                            try
                                        //                            {
                                        //                                cmbSelDistrict.Items.FindByText(dt.Rows[i]["Distrito"].ToString().Trim()).Selected = true;
                                        //                                dt.Rows[i]["Distrito"] = cmbSelDistrict.SelectedItem.Value;
                                        //                            }
                                        //                            catch
                                        //                            {
                                        //                                Pmensaje.CssClass = "MensajesSupervisor";
                                        //                                lblencabezado.Text = "Sr. Usuario";
                                        //                                lblmensajegeneral.Text = "El Distrito: " + dt.Rows[i]["Distrito"].ToString().Trim() + ", no es válido para la ciudad " + cmbSelProvince.SelectedItem.Text + ". Por favor verifique";
                                        //                                Mensajes_Usuario();
                                        //                                i = dt.Rows.Count;
                                        //                                DivisiónPolitica = false;
                                        //                            }
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            Pmensaje.CssClass = "MensajesSupervisor";
                                        //                            lblencabezado.Text = "Sr. Usuario";
                                        //                            lblmensajegeneral.Text = "No se han creado Distritos para la ciudad : " + cmbSelProvince.SelectedItem.Text + ". Por favor verifique";
                                        //                            Mensajes_Usuario();
                                        //                            i = dt.Rows.Count;
                                        //                            DivisiónPolitica = false;
                                        //                        }
                                        //                        dtdistrito = null;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        cmbSelDistrict.Items.Clear();
                                        //                        dt.Rows[i]["Distrito"] = "";
                                        //                    }
                                        //                }
                                        //                else
                                        //                {
                                        //                    if (oescountry.CountryDistrito)
                                        //                    {
                                        //                        DataTable dtdistrito = new DataTable();
                                        //                        dtdistrito = oCoon.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONPAIS", this.Session["scountry"].ToString());
                                        //                        if (dtdistrito.Rows.Count > 1)
                                        //                        {
                                        //                            cmbSelDistrict.DataSource = dtdistrito;
                                        //                            cmbSelDistrict.DataTextField = "Name_Local";
                                        //                            cmbSelDistrict.DataValueField = "cod_District";
                                        //                            cmbSelDistrict.DataBind();
                                        //                            try
                                        //                            {
                                        //                                cmbSelDistrict.Items.FindByText(dt.Rows[i]["Distrito"].ToString().Trim()).Selected = true;
                                        //                                dt.Rows[i]["Distrito"] = cmbSelDistrict.SelectedItem.Value;
                                        //                            }
                                        //                            catch
                                        //                            {
                                        //                                Pmensaje.CssClass = "MensajesSupervisor";
                                        //                                lblencabezado.Text = "Sr. Usuario";
                                        //                                lblmensajegeneral.Text = "El Distrito : " + dt.Rows[i]["Distrito"].ToString().Trim() + " No es válido para el país " + LblCountry.Text + ". Por favor verifique";
                                        //                                Mensajes_Usuario();
                                        //                                i = dt.Rows.Count;
                                        //                                DivisiónPolitica = false;
                                        //                            }
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            Pmensaje.CssClass = "MensajesSupervisor";
                                        //                            lblencabezado.Text = "Sr. Usuario";
                                        //                            lblmensajegeneral.Text = "No se han creado Distritos para el País : " + LblCountry.Text + ". Por favor verifique";
                                        //                            Mensajes_Usuario();
                                        //                            i = dt.Rows.Count;
                                        //                            DivisiónPolitica = false;
                                        //                        }
                                        //                        dtdistrito = null;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        cmbSelDistrict.Items.Clear();
                                        //                        dt.Rows[i]["Distrito"] = "";
                                        //                    }
                                        //                }
                                        //            }
                                        //        }
                                        //    }
                                        //    if (DivisiónPolitica)
                                        //    {
                                        //        if (oescountry.CountryBarrio)
                                        //        {
                                        //            if (oescountry.CountryDistrito)
                                        //            {
                                        //                DataTable dtbarrio = new DataTable();
                                        //                dtbarrio = oCoon.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONDISTRITO", cmbSelDistrict.Text);
                                        //                if (dtbarrio.Rows.Count > 1)
                                        //                {
                                        //                    cmbSelComunity.DataSource = dtbarrio;
                                        //                    cmbSelComunity.DataTextField = "Name_Community";
                                        //                    cmbSelComunity.DataValueField = "cod_Community";
                                        //                    cmbSelComunity.DataBind();
                                        //                    try
                                        //                    {
                                        //                        cmbSelComunity.Items.FindByText(dt.Rows[i]["Barrio"].ToString().Trim()).Selected = true;
                                        //                        dt.Rows[i]["Barrio"] = cmbSelComunity.SelectedItem.Value;
                                        //                    }
                                        //                    catch
                                        //                    {
                                        //                        Pmensaje.CssClass = "MensajesSupervisor";
                                        //                        lblencabezado.Text = "Sr. Usuario";
                                        //                        lblmensajegeneral.Text = "El Barrio : " + dt.Rows[i]["Barrio"].ToString().Trim() + " No es válido para el distrito " + cmbSelDistrict.SelectedItem.Text + ". Por favor verifique";
                                        //                        Mensajes_Usuario();
                                        //                        i = dt.Rows.Count;
                                        //                        DivisiónPolitica = false;
                                        //                    }
                                        //                }
                                        //                else
                                        //                {
                                        //                    Pmensaje.CssClass = "MensajesSupervisor";
                                        //                    lblencabezado.Text = "Sr. Usuario";
                                        //                    lblmensajegeneral.Text = "No se han creado Barrios para el Distrito : " + cmbSelDistrict.SelectedItem.Text + ". Por favor verifique";
                                        //                    Mensajes_Usuario();
                                        //                    i = dt.Rows.Count;
                                        //                    DivisiónPolitica = false;
                                        //                }
                                        //                dtbarrio = null;
                                        //            }
                                        //            else
                                        //            {
                                        //                if (oescountry.CountryCiudad)
                                        //                {
                                        //                    DataTable dtbarrio = new DataTable();
                                        //                    dtbarrio = oCoon.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONCITY", cmbSelProvince.Text);
                                        //                    if (dtbarrio.Rows.Count > 1)
                                        //                    {
                                        //                        cmbSelComunity.DataSource = dtbarrio;
                                        //                        cmbSelComunity.DataTextField = "Name_Community";
                                        //                        cmbSelComunity.DataValueField = "cod_Community";
                                        //                        cmbSelComunity.DataBind();
                                        //                        try
                                        //                        {
                                        //                            cmbSelComunity.Items.FindByText(dt.Rows[i]["Barrio"].ToString().Trim()).Selected = true;
                                        //                            dt.Rows[i]["Barrio"] = cmbSelComunity.SelectedItem.Value;
                                        //                        }
                                        //                        catch
                                        //                        {
                                        //                            Pmensaje.CssClass = "MensajesSupervisor";
                                        //                            lblencabezado.Text = "Sr. Usuario";
                                        //                            lblmensajegeneral.Text = "El Barrio : " + dt.Rows[i]["Barrio"].ToString().Trim() + " No es válido para la ciudad " + cmbSelProvince.SelectedItem.Text + ". Por favor verifique";
                                        //                            Mensajes_Usuario();
                                        //                            i = dt.Rows.Count;
                                        //                            DivisiónPolitica = false;
                                        //                        }
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        Pmensaje.CssClass = "MensajesSupervisor";
                                        //                        lblencabezado.Text = "Sr. Usuario";
                                        //                        lblmensajegeneral.Text = "No se han creado Barrios para la ciudad : " + cmbSelProvince.SelectedItem.Text + ". Por favor verifique";
                                        //                        Mensajes_Usuario();
                                        //                        i = dt.Rows.Count;
                                        //                        DivisiónPolitica = false;
                                        //                    }
                                        //                    dtbarrio = null;
                                        //                }
                                        //                else
                                        //                {
                                        //                    if (oescountry.CountryDpto)
                                        //                    {
                                        //                        DataTable dtbarrio = new DataTable();
                                        //                        dtbarrio = oCoon.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONDPTO", cmbSelDpto.Text);
                                        //                        if (dtbarrio.Rows.Count > 1)
                                        //                        {
                                        //                            cmbSelComunity.DataSource = dtbarrio;
                                        //                            cmbSelComunity.DataTextField = "Name_Community";
                                        //                            cmbSelComunity.DataValueField = "cod_Community";
                                        //                            cmbSelComunity.DataBind();
                                        //                            try
                                        //                            {
                                        //                                cmbSelComunity.Items.FindByText(dt.Rows[i]["Barrio"].ToString().Trim()).Selected = true;
                                        //                                dt.Rows[i]["Barrio"] = cmbSelComunity.SelectedItem.Value;
                                        //                            }
                                        //                            catch
                                        //                            {
                                        //                                Pmensaje.CssClass = "MensajesSupervisor";
                                        //                                lblencabezado.Text = "Sr. Usuario";
                                        //                                lblmensajegeneral.Text = "El Barrio : " + dt.Rows[i]["Barrio"].ToString().Trim() + " No es válido para el departamento " + cmbSelDpto.SelectedItem.Text + ". Por favor verifique";
                                        //                                Mensajes_Usuario();
                                        //                                i = dt.Rows.Count;
                                        //                                DivisiónPolitica = false;
                                        //                            }
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            Pmensaje.CssClass = "MensajesSupervisor";
                                        //                            lblencabezado.Text = "Sr. Usuario";
                                        //                            lblmensajegeneral.Text = "No se han creado Barrios para el departamento : " + cmbSelDpto.SelectedItem.Text + ". Por favor verifique";
                                        //                            Mensajes_Usuario();
                                        //                            i = dt.Rows.Count;
                                        //                            DivisiónPolitica = false;
                                        //                        }
                                        //                        dtbarrio = null;
                                        //                    }
                                        //                    else
                                        //                    {
                                        //                        DataTable dtbarrio = new DataTable();
                                        //                        dtbarrio = oCoon.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONPAIS", this.Session["scountry"].ToString());
                                        //                        if (dtbarrio.Rows.Count > 1)
                                        //                        {
                                        //                            cmbSelComunity.DataSource = dtbarrio;
                                        //                            cmbSelComunity.DataTextField = "Name_Community";
                                        //                            cmbSelComunity.DataValueField = "cod_Community";
                                        //                            cmbSelComunity.DataBind();
                                        //                            try
                                        //                            {
                                        //                                cmbSelComunity.Items.FindByText(dt.Rows[i]["Barrio"].ToString().Trim()).Selected = true;
                                        //                                dt.Rows[i]["Barrio"] = cmbSelComunity.SelectedItem.Value;
                                        //                            }
                                        //                            catch
                                        //                            {
                                        //                                Pmensaje.CssClass = "MensajesSupervisor";
                                        //                                lblencabezado.Text = "Sr. Usuario";
                                        //                                lblmensajegeneral.Text = "El Barrio : " + dt.Rows[i]["Barrio"].ToString().Trim() + " No es válido para el País " + LblCountry.Text + ". Por favor verifique";
                                        //                                Mensajes_Usuario();
                                        //                                i = dt.Rows.Count;
                                        //                                DivisiónPolitica = false;
                                        //                            }
                                        //                        }
                                        //                        else
                                        //                        {
                                        //                            Pmensaje.CssClass = "MensajesSupervisor";
                                        //                            lblencabezado.Text = "Sr. Usuario";
                                        //                            lblmensajegeneral.Text = "No se han creado Barrios para el País : " + LblCountry.Text + ". Por favor verifique";
                                        //                            Mensajes_Usuario();
                                        //                            i = dt.Rows.Count;
                                        //                            DivisiónPolitica = false;
                                        //                        }
                                        //                    }
                                        //                }
                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            cmbSelComunity.Items.Clear();
                                        //            dt.Rows[i]["Barrio"] = "";
                                        //        }
                                        //    }
                                        //    if (DivisiónPolitica)
                                        //    {
                                        //        if (dt.Rows[i]["Canal"].ToString().ToUpper() == LblCanal.Text.ToUpper())
                                        //        {
                                        //            CanalValido = true;
                                        //        }
                                        //        else
                                        //        {
                                        //            CanalValido = false;
                                        //            Pmensaje.CssClass = "MensajesSupervisor";
                                        //            lblencabezado.Text = "Sr. Usuario";
                                        //            lblmensajegeneral.Text = "El canal : " + dt.Rows[i]["Canal"].ToString() + ", no es válido. Sólo se permite cargar puntos de venta para el canal " + LblCanal.Text.ToUpper();
                                        //            Mensajes_Usuario();
                                        //            i = dt.Rows.Count;
                                        //        }
                                        //    }
                                        //    if (DivisiónPolitica && CanalValido)
                                        //    {
                                        //        comboTipoMercado();
                                        //        if (dt.Rows[i]["Tipo de Agrupación Comercial"].ToString().Trim() != "")
                                        //        {
                                        //            try
                                        //            {
                                        //                TipoAgrupacion = true;

                                        //                CmbTipMerc.Items.FindByText(dt.Rows[i]["Tipo de Agrupación Comercial"].ToString().Trim()).Selected = true;
                                        //                dt.Rows[i]["Tipo de Agrupación Comercial"] = CmbTipMerc.SelectedItem.Value;
                                        //                comboNodos();
                                        //                try
                                        //                {
                                        //                    Agrupacion = true;
                                        //                    cmbNodoCom.Items.FindByText(dt.Rows[i]["Nombre de Agrupación comercial"].ToString().Trim()).Selected = true;
                                        //                    dt.Rows[i]["Nombre de Agrupación comercial"] = cmbNodoCom.SelectedItem.Value;
                                        //                }
                                        //                catch (Exception ex)
                                        //                {
                                        //                    Agrupacion = false;
                                        //                    Pmensaje.CssClass = "MensajesSupervisor";
                                        //                    lblencabezado.Text = "Sr. Usuario";
                                        //                    lblmensajegeneral.Text = "La Agrupación comercial " + dt.Rows[i]["Nombre de Agrupación comercial"].ToString().Trim() + " no es válida para el tipo de agrupación :" + CmbTipMerc.SelectedItem.Text + ", por favor verifique";
                                        //                    Mensajes_Usuario();
                                        //                    i = dt.Rows.Count;
                                        //                }
                                        //            }
                                        //            catch (Exception ex)
                                        //            {
                                        //                TipoAgrupacion = false;
                                        //                Pmensaje.CssClass = "MensajesSupervisor";
                                        //                lblencabezado.Text = "Sr. Usuario";
                                        //                lblmensajegeneral.Text = "El Tipo de agrupación comercial " + dt.Rows[i]["Tipo de Agrupación Comercial"].ToString().Trim() + " no es válido por favor verifique";
                                        //                Mensajes_Usuario();
                                        //                i = dt.Rows.Count;
                                        //            }
                                        //        }

                                        //        else
                                        //        {
                                        //            TipoAgrupacion = false;
                                        //            Pmensaje.CssClass = "MensajesSupervisor";
                                        //            lblencabezado.Text = "Sr. Usuario";
                                        //            lblmensajegeneral.Text = "El Tipo de agrupación comercial no puede estar en blanco. Por favor verifique";
                                        //            Mensajes_Usuario();
                                        //            i = dt.Rows.Count;
                                        //        }
                                        //    }
                                        //    if (DivisiónPolitica && CanalValido && TipoAgrupacion && Agrupacion)
                                        //    {
                                        //        combosegmenpdv();

                                        //        try
                                        //        {
                                        //            CmbSelSegPDV.Items.FindByText(dt.Rows[i]["Segmento"].ToString().Trim()).Selected = true;
                                        //            dt.Rows[i]["Segmento"] = CmbSelSegPDV.SelectedItem.Value;
                                        //            segmento = true;
                                        //        }
                                        //        catch
                                        //        {
                                        //            segmento = false;
                                        //            Pmensaje.CssClass = "MensajesSupervisor";
                                        //            lblencabezado.Text = "Sr. Usuario";
                                        //            lblmensajegeneral.Text = "El Segmento " + dt.Rows[i]["Segmento"].ToString().Trim() + ", no es válido por favor verifique";
                                        //            Mensajes_Usuario();
                                        //            i = dt.Rows.Count;
                                        //        }
                                        //    }
                                        //    if (DivisiónPolitica && CanalValido && TipoAgrupacion && Agrupacion && segmento)
                                        //    {

                                        //        combtipDocCli();
                                        //        try
                                        //        {
                                        //            string cod_doc = dt.Rows[i]["Cód Tipo de Documento"].ToString().Trim();
                                        //            if (cod_doc.Length == 1)
                                        //                cod_doc = "0" + cod_doc;
                                        //            cmbTipDocCli.Items.FindByValue(cod_doc).Selected = true;
                                        //            TipoDoc = true;
                                        //        }
                                        //        catch (Exception ex)
                                        //        {
                                        //            TipoDoc = false;
                                        //            Pmensaje.CssClass = "MensajesSupervisor";
                                        //            lblencabezado.Text = "Sr. Usuario";
                                        //            lblmensajegeneral.Text = "El Tipo de documento " + dt.Rows[i]["Cód Tipo de Documento"].ToString().Trim() + ", no es válido por favor verifique";
                                        //            Mensajes_Usuario();
                                        //            i = dt.Rows.Count;
                                        //        }
                                        //    }
                                        //    if (DivisiónPolitica && CanalValido && TipoAgrupacion && Agrupacion && segmento && TipoDoc)
                                        //    {
                                        //        combomallas(); // revisar las mallas (requerido o no)
                                        //        try
                                        //        {
                                        //            if (CmbMalla.Items.Count > 1)
                                        //            {
                                        //                if (dt.Rows[i]["Malla"].ToString().Trim() == "")
                                        //                {
                                        //                    CmbMalla.SelectedItem.Value = "0";
                                        //                    dt.Rows[i]["Malla"] = 0;
                                        //                    malla = true;
                                        //                }
                                        //                else
                                        //                {
                                        //                    CmbMalla.Items.FindByText(dt.Rows[i]["Malla"].ToString().Trim()).Selected = true;
                                        //                    dt.Rows[i]["Malla"] = CmbMalla.SelectedItem.Value;
                                        //                    malla = true;
                                        //                }
                                        //            }
                                        //            else
                                        //            {
                                        //                if (dt.Rows[i]["Malla"].ToString().Trim() == "")
                                        //                {
                                        //                    CmbMalla.SelectedItem.Value = "0";
                                        //                    dt.Rows[i]["Malla"] = 0;
                                        //                    malla = true;
                                        //                }
                                        //                else
                                        //                {
                                        //                    malla = false;
                                        //                    Pmensaje.CssClass = "MensajesSupervisor";
                                        //                    lblencabezado.Text = "Sr. Usuario";
                                        //                    lblmensajegeneral.Text = "La Malla " + dt.Rows[i]["Malla"].ToString().Trim() + ", no es válida por favor verifique";
                                        //                    Mensajes_Usuario();
                                        //                    i = dt.Rows.Count;
                                        //                }
                                        //            }
                                        //        }
                                        //        catch (Exception ex)
                                        //        {
                                        //            malla = false;
                                        //            Pmensaje.CssClass = "MensajesSupervisor";
                                        //            lblencabezado.Text = "Sr. Usuario";
                                        //            lblmensajegeneral.Text = "La Malla " + dt.Rows[i]["Malla"].ToString().Trim() + ", no es válida por favor verifique";
                                        //            Mensajes_Usuario();
                                        //            i = dt.Rows.Count;
                                        //        }
                                        //    }
                                        //    if (DivisiónPolitica && CanalValido && TipoAgrupacion && Agrupacion && segmento && TipoDoc && malla)
                                        //    {
                                        //        try
                                        //        {
                                        //            combosector();
                                        //            if (CmbSector.Items.Count > 1)
                                        //            {
                                        //                CmbSector.Items.FindByText(dt.Rows[i]["Sector"].ToString().Trim()).Selected = true;
                                        //                dt.Rows[i]["Sector"] = CmbSector.SelectedItem.Value;
                                        //                sector = true;
                                        //            }
                                        //            else
                                        //            {
                                        //                if (dt.Rows[i]["Sector"].ToString().Trim() == "")
                                        //                {
                                        //                    CmbSector.SelectedItem.Value = "0";
                                        //                    dt.Rows[i]["Sector"] = 0;
                                        //                    sector = true;
                                        //                }
                                        //                else
                                        //                {
                                        //                    sector = false;
                                        //                    Pmensaje.CssClass = "MensajesSupervisor";
                                        //                    lblencabezado.Text = "Sr. Usuario";
                                        //                    lblmensajegeneral.Text = "El sector " + dt.Rows[i]["Sector"].ToString().Trim() + ", no es valido por favor verifique";
                                        //                    Mensajes_Usuario();
                                        //                    i = dt.Rows.Count;
                                        //                }
                                        //            }
                                        //        }
                                        //        catch
                                        //        {
                                        //            sector = false;
                                        //            Pmensaje.CssClass = "MensajesSupervisor";
                                        //            lblencabezado.Text = "Sr. Usuario";
                                        //            lblmensajegeneral.Text = "El sector " + dt.Rows[i]["Sector"].ToString().Trim() + ", no es válido por favor verifique";
                                        //            Mensajes_Usuario();
                                        //            i = dt.Rows.Count;
                                        //        }
                                        //    }
                                        //    if (DivisiónPolitica && CanalValido && TipoAgrupacion && Agrupacion && segmento && TipoDoc && malla && sector)
                                        //    {
                                        //        combooficina();

                                        //        try
                                        //        {
                                        //            CmbOficina.Items.FindByText(dt.Rows[i]["Oficina"].ToString().Trim()).Selected = true;
                                        //            dt.Rows[i]["Oficina"] = CmbOficina.SelectedItem.Value;
                                        //            oficina = true;
                                        //        }
                                        //        catch (Exception ex)
                                        //        {
                                        //            oficina = false;
                                        //            Pmensaje.CssClass = "MensajesSupervisor";
                                        //            lblencabezado.Text = "Sr. Usuario";
                                        //            lblmensajegeneral.Text = "La oficina " + dt.Rows[i]["Oficina"].ToString().Trim() + ", no es válida por favor verifique";
                                        //            Mensajes_Usuario();
                                        //            i = dt.Rows.Count;
                                        //        }
                                        //    }

                                        //    //Verificación del PDV_Contact - Angel Ortiz - 26/09/2011
                                        //    if (DivisiónPolitica && CanalValido && TipoAgrupacion && Agrupacion && segmento && TipoDoc && malla && sector && oficina)
                                        //    {
                                        //        if (!dt.Rows[i]["Contacto PDV"].ToString().Trim().Equals(""))//Si el campo de contacto no esta vacio
                                        //        {
                                        //            DataTable dt_pdv_contact = oConn.ejecutarDataTable("UP_WEBXPLORA_PLA_VERIFICA_NOMBRE_CONTACTO_PDV", dt.Rows[i]["Contacto PDV"].ToString().Trim());
                                        //            if (dt_pdv_contact != null && dt_pdv_contact.Rows.Count > 0)
                                        //            {
                                        //                Pdv_Contact = true;
                                        //            }
                                        //            else
                                        //            {
                                        //                Pdv_Contact = false;
                                        //                Pmensaje.CssClass = "MensajesSupervisor";
                                        //                lblencabezado.Text = "Sr. Usuario";
                                        //                lblmensajegeneral.Text = "El contacto de pdv: " + dt.Rows[i]["Contacto PDV"].ToString().Trim() + ", no es válido. Por favor verifique.";
                                        //                Mensajes_Usuario();
                                        //                i = dt.Rows.Count;
                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            Pdv_Contact = true;
                                        //        }
                                        //    }

                                        //    //dt.Rows[i]["País"] = this.Session["scountry"].ToString();

                                        //    if (DivisiónPolitica && CanalValido && TipoAgrupacion && Agrupacion && segmento && TipoDoc && malla && sector && oficina && Pdv_Contact)
                                        //    {
                                        //        comboDistribuidora();
                                        //        try
                                        //        {
                                        //            if (cmbDexPDV.Items.Count > 1)
                                        //            {

                                        //                if (LblCanal.Text == "MINORISTA" && dt.Rows[i]["Distribuidora"].ToString().Trim() != "")
                                        //                {
                                        //                    cmbDexPDV.Items.FindByText(dt.Rows[i]["Distribuidora"].ToString().Trim()).Selected = true;
                                        //                    dt.Rows[i]["Distribuidora"] = cmbDexPDV.SelectedItem.Value;
                                        //                    Distribuidora = true;
                                        //                }
                                        //                if (LblCanal.Text != "MINORISTA" && dt.Rows[i]["Distribuidora"].ToString().Trim() != "")
                                        //                {
                                        //                    cmbDexPDV.Items.FindByText(dt.Rows[i]["Distribuidora"].ToString().Trim()).Selected = true;
                                        //                    dt.Rows[i]["Distribuidora"] = cmbDexPDV.SelectedItem.Value;
                                        //                    Distribuidora = true;
                                        //                }
                                        //                if (LblCanal.Text != "MINORISTA" && dt.Rows[i]["Distribuidora"].ToString().Trim() == "")
                                        //                {
                                        //                  cmbDexPDV.SelectedItem.Value="0";
                                        //                  dt.Rows[i]["Distribuidora"] = "0";
                                        //                  Distribuidora = true;
                                        //                }
                                        //                if (LblCanal.Text == "MINORISTA" && dt.Rows[i]["Distribuidora"].ToString().Trim() == "")
                                        //                {
                                        //                    Distribuidora = false;
                                        //                    Pmensaje.CssClass = "MensajesSupervisor";
                                        //                    lblencabezado.Text = "Sr. Usuario";
                                        //                    lblmensajegeneral.Text = "Para el canal Minorista, la Distribuidora es obligatoria.";
                                        //                    Mensajes_Usuario();   
                                        //                    i = dt.Rows.Count;
                                        //                }
                                        //            }
                                        //            else
                                        //            {
                                        //                Pmensaje.CssClass = "MensajesSupervisor";
                                        //                lblencabezado.Text = "Sr. Usuario";
                                        //                lblmensajegeneral.Text = "No existen Distribuidoras";
                                        //                Mensajes_Usuario();   
                                        //            }
                                        //        }
                                        //        catch (Exception ex)
                                        //        {
                                        //            Distribuidora = false;
                                        //            Pmensaje.CssClass = "MensajesSupervisor";
                                        //            lblencabezado.Text = "Sr. Usuario";
                                        //            lblmensajegeneral.Text = "La Distribuidora " + dt.Rows[i]["Distribuidora"].ToString().Trim() + ", no es válida por favor verifique";
                                        //            Mensajes_Usuario();
                                        //            i = dt.Rows.Count;
                                        //        }
                                        //    }
                                        //}

                                        //else
                                        //{
                                        //    DivisiónPolitica = false;
                                        //    Pmensaje.CssClass = "MensajesSupervisor";
                                        //    lblencabezado.Text = "Sr. Usuario";
                                        //    lblmensajegeneral.Text = "El País : " + dt.Rows[i]["País"].ToString() + ", no es válido. Solo se permite cargar puntos de venta para el País " + LblCountry.Text.ToUpper();
                                        //    Mensajes_Usuario();
                                        //    i = dt.Rows.Count;
                                        //}
                                        #endregion




                                    }

                                    Pmensaje.CssClass = "MensajesSupConfirm";
                                    lblencabezado.Text = "Cargar Puntos de venta";
                                    lblmensajegeneral.Text = "Puntos de venta cargados : " + cargados + ", Puntos de ventas que no existen : " + noexisten +", Puntos de ventas que ya estan asignados a la campaña " + CmbSelPresupuestoPDV.SelectedItem.Text +" : "+ duplicados;
                                    Mensajes_Usuario();

                                    //Validarobjeto();




                                }
                                else
                                {
                                    Pmensaje.CssClass = "MensajesSupervisor";
                                    lblencabezado.Text = "Sr. Usuario";
                                    lblmensajegeneral.Text = "El archivo debe contener 29 campos. Por favor verifique.";
                                    Mensajes_Usuario();
                                }
                            }

#region coment

                            //        if (DivisiónPolitica && CanalValido && TipoAgrupacion && Agrupacion && segmento && TipoDoc && malla && sector && oficina && Distribuidora && Pdv_Contact)
                            //        {
                            //            Gvlog.DataSource = dt;
                            //            Gvlog.DataBind();

                            //            foreach (GridViewRow gvr in Gvlog.Rows)
                            //            {
                            //                for (int i = 0; i < numcol; i++)
                            //                {
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&nbsp;", "");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("  ", " ");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#160;", "");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#193;", "Á");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#201;", "É");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#205;", "Í");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#211;", "Ó");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#218;", "Ú");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#225;", "á");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#233;", "é");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#237;", "í");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#243;", "ó");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#250;", "ú");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#209;", "Ñ");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#241;", "ñ");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&amp;", "&");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#176;", "o");
                            //                    gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#186;", "o");
                            //                }
                            //            }                                        
                                       
                            //            bool Continua = true;
                            //            DAplicacion oDConsultaPDV = new DAplicacion();
                            //            for (int i = 0; i <= Gvlog.Rows.Count - 1; i++)
                            //            {
                            //                DAplicacion odconsupdv = new DAplicacion();
                            //                DataTable dtconsulta1 = odconsupdv.ConsultaDuplicados(ConfigurationManager.AppSettings["PDVdoc"], Gvlog.Rows[i].Cells[2].Text, null, null);
                                             
                            //                if (dtconsulta1 != null)
                            //                {
                            //                    if (dtconsulta1.Rows.Count > 0)
                            //                    {
                            //                        if (Gvlog.Rows[i].Cells[7].Text == dtconsulta1.Rows[0]["pdv_RazónSocial"].ToString().Trim())
                            //                        {
                            //                            Continua = true;
                            //                            if (Continua)
                            //                            {
                            //                                DataTable dtPDVExiste = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_SEARCH_PDV_MASIVO", 1, Gvlog.Rows[i].Cells[2].Text, Gvlog.Rows[i].Cells[7].Text, Gvlog.Rows[i].Cells[8].Text);
                            //                                if (dtPDVExiste != null)
                            //                                {
                            //                                    if (dtPDVExiste.Rows.Count == 0)
                            //                                    {
                            //                                        oConn.ejecutarDataTable("UP_WEBXPLORA_AD_REGISTRAR_PUNTOSDEVENTA",Gvlog.Rows[i].Cells[1].Text, Gvlog.Rows[i].Cells[2].Text, Gvlog.Rows[i].Cells[3].Text,
                            //                                        Gvlog.Rows[i].Cells[4].Text, Gvlog.Rows[i].Cells[5].Text, Gvlog.Rows[i].Cells[6].Text, Gvlog.Rows[i].Cells[7].Text, Gvlog.Rows[i].Cells[8].Text,
                            //                                        Gvlog.Rows[i].Cells[9].Text, Gvlog.Rows[i].Cells[10].Text, Gvlog.Rows[i].Cells[11].Text, Gvlog.Rows[i].Cells[12].Text, Gvlog.Rows[i].Cells[13].Text,
                            //                                        Gvlog.Rows[i].Cells[14].Text, Gvlog.Rows[i].Cells[15].Text, Gvlog.Rows[i].Cells[16].Text, Gvlog.Rows[i].Cells[17].Text, Gvlog.Rows[i].Cells[18].Text,
                            //                                        this.Session["CodChannel"].ToString().Trim(), Convert.ToInt32(Gvlog.Rows[i].Cells[20].Text), Gvlog.Rows[i].Cells[21].Text, Convert.ToInt32(Gvlog.Rows[i].Cells[22].Text),
                            //                                        true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            //                                        DataTable dtCodPDVExiste = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_SEARCH_PDV_MASIVO", 2, Gvlog.Rows[i].Cells[2].Text, Gvlog.Rows[i].Cells[7].Text, Gvlog.Rows[i].Cells[8].Text);
                            //                                        try
                            //                                        {
                            //                                            Planning.RegistrarClientPDV(
                            //                                                Convert.ToInt32(this.Session["company_id"]), 
                            //                                                Convert.ToInt32(dtCodPDVExiste.Rows[0][0].ToString().Trim()), 
                            //                                                Gvlog.Rows[i].Cells[0].Text, 
                            //                                                Convert.ToInt32(Gvlog.Rows[i].Cells[24].Text), 
                            //                                                Convert.ToInt64(Gvlog.Rows[i].Cells[25].Text), 
                            //                                                Convert.ToInt32(Gvlog.Rows[i].Cells[26].Text), 
                            //                                                true, 
                            //                                                Convert.ToString(this.Session["sUser"]), 
                            //                                                DateTime.Now, 
                            //                                                Convert.ToString(this.Session["sUser"]), 
                            //                                                DateTime.Now, 
                            //                                                Gvlog.Rows[i].Cells[27].Text);
                            //                                            // método para registrar en la base de datos movil. Ing. Mauricio Ortiz . 14/02/2011
                            //                                            oPDV.RegistrarClientPDVTMP();
                            //                                        }
                            //                                        catch (Exception ex)
                            //                                        {
                            //                                            Continua = false;
                            //                                            Pmensaje.CssClass = "MensajesSupervisor";
                            //                                            lblencabezado.Text = "Sr. Usuario";
                            //                                            lblmensajegeneral.Text = "El código " + Gvlog.Rows[i].Cells[0].Text + " existe para otro punto de venta, debe cambiarlo";
                            //                                            Mensajes_Usuario();
                            //                                            i = dt.Rows.Count;
                            //                                        }
                            //                                    }
                            //                                }
                            //                            }
                            //                        }
                            //                        else
                            //                        {
                            //                            Continua = false;
                            //                            Pmensaje.CssClass = "MensajesSupervisor";
                            //                            lblencabezado.Text = "Sr. Usuario";
                            //                            lblmensajegeneral.Text = "El número de identificación " + Gvlog.Rows[i].Cells[2].Text + "ya está asignado a la razón social " + dtconsulta1.Rows[0]["pdv_RazónSocial"].ToString().Trim();
                            //                            Mensajes_Usuario();
                            //                            i = dt.Rows.Count;
                            //                        }
                            //                    }
                            //                }
                            //                else
                            //                {
                            //                    Continua = true;
                            //                    DataTable dtPDVExiste = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_SEARCH_PDV_MASIVO", 2, Gvlog.Rows[i].Cells[2].Text, Gvlog.Rows[i].Cells[7].Text, Gvlog.Rows[i].Cells[8].Text);
                            //                    if (dtPDVExiste != null)
                            //                    {
                            //                        if (dtPDVExiste.Rows.Count == 0)
                            //                        {
                            //                                oConn.ejecutarDataTable("UP_WEBXPLORA_AD_REGISTRAR_PUNTOSDEVENTA",Gvlog.Rows[i].Cells[1].Text, Gvlog.Rows[i].Cells[2].Text, Gvlog.Rows[i].Cells[3].Text,
                            //                                Gvlog.Rows[i].Cells[4].Text, Gvlog.Rows[i].Cells[5].Text, Gvlog.Rows[i].Cells[6].Text, Gvlog.Rows[i].Cells[7].Text, Gvlog.Rows[i].Cells[8].Text,
                            //                                Gvlog.Rows[i].Cells[9].Text, Gvlog.Rows[i].Cells[10].Text, Gvlog.Rows[i].Cells[11].Text, Gvlog.Rows[i].Cells[12].Text, Gvlog.Rows[i].Cells[13].Text,
                            //                                Gvlog.Rows[i].Cells[14].Text, Gvlog.Rows[i].Cells[15].Text, Gvlog.Rows[i].Cells[16].Text, Gvlog.Rows[i].Cells[17].Text, Gvlog.Rows[i].Cells[18].Text,
                            //                                this.Session["CodChannel"].ToString().Trim(), Convert.ToInt32(Gvlog.Rows[i].Cells[20].Text), Gvlog.Rows[i].Cells[21].Text, Convert.ToInt32(Gvlog.Rows[i].Cells[22].Text),
                            //                                true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                            //                            DataTable dtCodPDVExiste = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_SEARCH_PDV_MASIVO", 2, Gvlog.Rows[i].Cells[2].Text, Gvlog.Rows[i].Cells[7].Text, Gvlog.Rows[i].Cells[8].Text);
                            //                            if (dtCodPDVExiste != null)
                            //                            {
                            //                                if (dtCodPDVExiste.Rows.Count > 0)
                            //                                {
                            //                                    oConn.ejecutarDataTable("UP_WEB_REGISTERPDVCLIENT",
                            //                                        Convert.ToInt32(this.Session["company_id"]), 
                            //                                        Convert.ToInt32(dtCodPDVExiste.Rows[0][0].ToString().Trim()), 
                            //                                        Gvlog.Rows[i].Cells[0].Text, 
                            //                                        Convert.ToInt32(Gvlog.Rows[i].Cells[24].Text), 
                            //                                        Convert.ToInt32(Gvlog.Rows[i].Cells[25].Text),  
                            //                                        Convert.ToInt32(Gvlog.Rows[i].Cells[26].Text),
                            //                                        true, Convert.ToString(this.Session["sUser"]), 
                            //                                        DateTime.Now, 
                            //                                        Convert.ToString(this.Session["sUser"]), 
                            //                                        DateTime.Now, 
                            //                                        Gvlog.Rows[i].Cells[27].Text);//agregando el parametro pdv_alias
                            //                                    // método para registrar en la base de datos movil. Ing. Mauricio Ortiz . 14/02/2011
                            //                                    oPDV.RegistrarClientPDVTMP();
                            //                                }
                            //                            }
                            //                        }
                            //                        else
                            //                        {
                            //                            Continua = false;
                            //                            Pmensaje.CssClass = "MensajesSupervisor";
                            //                            lblencabezado.Text = "Sr. Usuario";
                            //                            lblmensajegeneral.Text = "El punto de venta " + Gvlog.Rows[i].Cells[8].Text + " ya está asignado. Por favor verifique el número de documento o la razón social ";
                            //                            Mensajes_Usuario();
                            //                            i = dt.Rows.Count;
                            //                        }
                            //                    }
                            //                }
                            //            }

                            //            if (Continua)
                            //            {
                            //                //Ejecutar Método para almacenar los puntos de venta seleccionados para el planning. Ing. Mauricio Ortiz

                            //                for (int i = 0; i <= Gvlog.Rows.Count - 1; i++)
                            //                {
                            //                    DataTable dtCodPDVExiste = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_SEARCH_PDV_MASIVO", 3, Gvlog.Rows[i].Cells[2].Text, Gvlog.Rows[i].Cells[7].Text, Gvlog.Rows[i].Cells[8].Text);
                            //                    DataTable dtDuplicado = Planning.Get_DuplicadoPDVPlanning(Convert.ToInt32(dtCodPDVExiste.Rows[0][0].ToString().Trim()), TxtCodPlanningPDV.Text);
                                                
                            //                    if (dtDuplicado != null)
                            //                    {
                            //                        if (dtDuplicado.Rows.Count == 0)
                            //                        {
                            //                            int idclientpdv =Convert. ToInt32(dtCodPDVExiste.Rows[0][0].ToString().Trim());
                            //                            string id_planning = TxtCodPlanningPDV.Text;
                            //                            string cod_city = Gvlog.Rows[i].Cells[14].Text;
                            //                            int id_nodecomtype = Convert.ToInt32(Gvlog.Rows[i].Cells[20].Text);
                            //                            string id_nodocomercial = Gvlog.Rows[i].Cells[21].Text;
                            //                            long doc_oficina = Convert.ToInt64(Gvlog.Rows[i].Cells[25].Text);
                            //                            int id_malla = Convert.ToInt32(Gvlog.Rows[i].Cells[23].Text);
                            //                            int id_sector = Convert.ToInt32(Gvlog.Rows[i].Cells[24].Text);
                            //                            bool status = true;
                            //                            string usuario = this.Session["sUser"].ToString().Trim();
                            //                            DateTime fecha = DateTime.Now;
                            //                            string pdv_contact = Gvlog.Rows[i].Cells[28].Text;
                            //                            DataTable testing = oConn.ejecutarDataTable("UP_WEBSIGE_PLANNING_INSERTARPDVPLANNING", idclientpdv, id_planning, cod_city, id_nodecomtype, id_nodocomercial, doc_oficina, id_malla, id_sector, status, usuario, fecha, usuario, fecha, pdv_contact);
                            //                            //Planning.Get_registrarPDVPlanning(idclientpdv,id_planning,cod_city,id_nodecomtype,id_nodocomercial,doc_oficina,id_malla,id_sector,status,usuario,fecha,usuario,fecha,pdv_contact);
                            //                        }
                            //                    }
                            //                }

                            //                Pmensaje.CssClass = "MensajesSupConfirm";
                            //                lblencabezado.Text = "Cargar Puntos de venta";
                            //                lblmensajegeneral.Text = dt.Rows.Count + " puntos de venta se adicionaron a la Campaña " + CmbSelPresupuestoPDV.SelectedItem.Text + " , provenientes del archivo " + FileUpPDV.FileName.ToLower();
                            //                Mensajes_Usuario();
                            //                Limpiar_InformacionPDVPlanning();
                            //                Validarobjeto();
                            //            }
                            //        }
                            //        else
                            //        {
                            //            Gvlog.DataBind();
                            //        }
                            //    }
                            //    else
                            //    {
                            //        Gvlog.DataBind();
                                    
                            //        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                            //        correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                            //        correo.To.Add(this.Session["smail"].ToString());
                            //        correo.Subject = "Errores en archivo de puntos de venta";
                            //        correo.IsBodyHtml = true;
                            //        correo.Priority = System.Net.Mail.MailPriority.Normal;
                            //        #region Texto de mensaje
                            //        string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                            //                this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                     
                            //                "El archivo que usted seleccionó para la carga de puntos de venta no cumple con una estructura válida." + "<br/>" +
                            //                "Por favor verifique que tenga " + numcol + " columnas" + "<br/>" +  "<br/>" +
                            //                "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                            //                "Columna 1  : Código Punto de Venta" + "<br/>" +
                            //                "Columna 2  : Cód Tipo de Documento"+ "<br/>" +
                            //                "Columna 3  : Identificación" + "<br/>" + 
                            //                "Columna 4  : Nombre Contacto 1" + "<br/>" +
                            //                "Columna 5  : Cargo Contacto 1" + "<br/>" +
                            //                "Columna 6  : Nombre Contacto 2" + "<br/>" +
                            //                "Columna 7  : Cargo Contacto 2" + "<br/>" +
                            //                "Columna 8  : Razón Social" + "<br/>" +
                            //                "Columna 9  : Nombre de Punto de Venta" + "<br/>" +
                            //                "Columna 10 : Teléfono" + "<br/>" +
                            //                "Columna 11 : Anexo" + "<br/>" +
                            //                "Columna 12 : Fax" + "<br/>" +                                            
                            //                "Columna 13 : País" + "<br/>" +                                            
                            //                "Columna 14 : Departamento" + "<br/>" +                                            
                            //                "Columna 15 : Provincia" + "<br/>" +                                           
                            //                "Columna 16 : Distrito" + "<br/>" +                                            
                            //                "Columna 17 : Barrio" + "<br/>" +
                            //                "Columna 18 : Dirección" 	+ "<br/>" +
                            //                "Columna 19 : Email" + "<br/>" +                                            
                            //                "Columna 20 : Canal" + "<br/>" +                                            
                            //                "Columna 21 : Tipo de Agrupación Comercial" + "<br/>" +                                            
                            //                "Columna 22 : Nombre de Agrupación comercial" + "<br/>" +
                            //                "Columna 23 : Segmento" + "<br/>" +     
                            //                "Columna 24 : Malla" + "<br/>" +
                            //                "Columna 25 : Sector" + "<br/>" +
                            //                "Columna 26 : Oficina" + "<br/>" +
                            //                "Columna 27 : Distribuidora" + "<br/>" +
                            //                "Columna 28 : Alias" + "<br/>" +
                            //                "<br/>" + "<br/>" + "<br/>" +
                            //                "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente, usted puede personalizar los nombres de las columnas del archivo ." +
                            //                "Pero tenga en cuenta que debe ingresar la información de los puntos de venta en ese orden." + "<br/>" + "<br/>" + "<br/>" +
                            //                "Cordial Saludo" + "<br/>" + "<br/>" + "Administrador Xplora." }; 
                            //        #endregion
                            //        correo.Body = string.Concat(txtbody);

                            //        System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                            //        cliente.Host = "mail.lucky.com.pe";

                            //        Pmensaje.CssClass = "MensajesSupervisor";
                            //        lblencabezado.Text = "Sr. Usuario";
                            //        lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de puntos de venta válido. Por favor verifique la estructura que fue enviada a su correo.";
                            //        Mensajes_Usuario();

                            //        try
                            //        {
                            //            cliente.Send(correo);
                            //        }
                            //        catch (System.Net.Mail.SmtpException)
                            //        {
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    Pmensaje.CssClass = "MensajesSupervisor";
                            //    lblencabezado.Text = "Sr. Usuario";
                            //    lblmensajegeneral.Text = "Es indispensable que cierre sesión he inicie nuevamente. ha dejado mucho tiempo inactivo el sistema y se perderá la información hasta ahora ingresada.";
                            //    Mensajes_Usuario();
                            //}

#endregion
                        }
                        catch (Exception ex)
                        {
                            Pmensaje.CssClass = "MensajesSupervisor";
                            lblencabezado.Text = "Sr. Usuario";
                            lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de puntos de venta válido. Por favor verifique que el nombre de la hoja donde estan los datos sea Puntos_Venta";
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
                lblmensajegeneral.Text = "Es indispensable seleccionar un presupuesto y un archivo.";
                Mensajes_Usuario();
            }
        }

        protected void RbtnSelTipoCarga_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtCodigoPDV.Text = "";
            LblNamePDV.Text = "------------------------------------------------------------------------------------------------";
            LblDirPDV.Text = "------------------------------------------------------------------------------------------------";
            LblCiudad.Text = "";
            LblTipoAgrupacion.Text = "";
            LblAgrupacion.Text = "";
            LblOficina.Text = "";
            LblMalla.Text = "";
            LblSector.Text = "";          

            if (RbtnSelTipoCarga.Items[0].Selected)
            {
                OpcUnoAUno.Style.Value = "Display:block;";
                OpcMasiva.Style.Value = "Display:none;";
                TxtCodigoPDV.Enabled = true;
                td_download.Style.Value = "display=none";
            }
            if (RbtnSelTipoCarga.Items[1].Selected)
            {
                OpcUnoAUno.Style.Value = "Display:none;";
                OpcMasiva.Style.Value = "Display:block;";
                TxtCodigoPDV.Enabled = false;              
                BtnCargaUnoaUno.Visible = false;
                td_download.Style.Value = "display=block";
                DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_EXPORTAEXCEL_DATOS_PTOS_VENTA", TxtCodPlanningPDV.Text);
                try
                {
                    CreaExcel(ds);
                }
                catch
                { 
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (TxtCodigoPDV.Text.Trim() != "")
            {
                //TxtCodigoPDV.Text = TxtCodigoPDV.Text.Replace(" ", "");
                DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_PDVCODEXCLIENTEYCANAL", this.Session["company_id"].ToString().Trim(), this.Session["CodChannel"].ToString().Trim(), TxtCodigoPDV.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    LblNamePDV.Text = dt.Rows[0]["pdv_Name"].ToString().Trim();
                    LblDirPDV.Text = dt.Rows[0]["pdv_Address"].ToString().Trim();
                    LblCiudad.Text = dt.Rows[0]["pdv_codProvince"].ToString().Trim();
                    LblTipoAgrupacion.Text =dt.Rows[0]["idNodeComType"].ToString().Trim();
                    LblAgrupacion.Text = dt.Rows[0]["NodeCommercial"].ToString().Trim();
                    LblOficina.Text = dt.Rows[0]["cod_Oficina"].ToString().Trim();
                    LblMalla.Text = dt.Rows[0]["id_malla"].ToString().Trim();
                    LblSector.Text = dt.Rows[0]["id_sector"].ToString().Trim();

                    BtnCargaUnoaUno.Visible = true;
                }
                else
                {
                    TxtCodigoPDV.Text = "";
                    LblNamePDV.Text = "------------------------------------------------------------------------------------------------";
                    LblDirPDV.Text = "------------------------------------------------------------------------------------------------";
                    LblCiudad.Text = "";
                    LblTipoAgrupacion.Text = "0";
                    LblAgrupacion.Text = "";
                    LblOficina.Text = "0";
                    LblMalla.Text = "0";
                    LblSector.Text = "0";    
                    BtnCargaUnoaUno.Visible = false;
                    Pmensaje.CssClass = "MensajesSupervisor";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "El codigo de punto de venta no esta asignado al cliente de la campaña o no pertenece al canal " + LblCanal.Text + " Por favor verifique";
                    Mensajes_Usuario();
                }
            }
            else
            {
                TxtCodigoPDV.Text = "";
                LblNamePDV.Text = "------------------------------------------------------------------------------------------------";
                LblDirPDV.Text = "------------------------------------------------------------------------------------------------";
                LblCiudad.Text = "";
                LblTipoAgrupacion.Text = "0";
                LblAgrupacion.Text = "";
                LblOficina.Text = "0";
                LblMalla.Text = "0";
                LblSector.Text = "0"; 
                BtnCargaUnoaUno.Visible = false;
            }
        }

        protected void BtnCargaUnoaUno_Click(object sender, EventArgs e)
        {
            DataTable dtCodPDVExiste = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_SEARCH_PDV_MASIVO", 5, "none", "none", TxtCodigoPDV.Text, this.Session["company_id"].ToString());
            DataTable dtDuplicado = Planning.Get_DuplicadoPDVPlanning(Convert.ToInt32(dtCodPDVExiste.Rows[0][0].ToString().Trim()), TxtCodPlanningPDV.Text);
            if (dtDuplicado != null)
            {
                if (dtDuplicado.Rows.Count == 0)
                {
                    //
                    // AGREGAR SOPORTE PARA INSERCION DE CONTACT
                    //
                    //Planning.Get_registrarPDVPlanning(
                    //    Convert.ToInt32(dtCodPDVExiste.Rows[0][0].ToString().Trim()),
                    //    TxtCodPlanningPDV.Text,
                    //     LblCiudad.Text, Convert.ToInt32(LblTipoAgrupacion.Text), LblAgrupacion.Text,
                    //     Convert.ToInt64(LblOficina.Text), 0, Convert.ToInt32(LblSector.Text),
                    //    true, this.Session["sUser"].ToString().Trim(),
                    //    DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now, "");

                    oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_INSERTARPDVPLANNING", 
                        Convert.ToInt32(dtCodPDVExiste.Rows[0][0].ToString().Trim()),
                        TxtCodPlanningPDV.Text,
                        LblCiudad.Text, 
                        Convert.ToInt32(LblTipoAgrupacion.Text), 
                        LblAgrupacion.Text,
                        Convert.ToInt64(LblOficina.Text), 
                        0, 
                        Convert.ToInt32(LblSector.Text),
                        true, 
                        this.Session["sUser"].ToString().Trim(),
                        DateTime.Now, 
                        this.Session["sUser"].ToString().Trim(), 
                        DateTime.Now, 
                        "");
                    //            (int iid_ClientPDV,
                    //    string sid_Planning, string scod_City, int iidNodeComType, string sNodeCommercial,
                    //long lcod_Oficina, int iid_malla, int iid_Sector, bool bPointOfSalePlanning_Status, string sPointOfSalePlanning_CreateBy,
                    //         DateTime tPointOfSalePlanning_DateBy, string sPointOfSalePlanning_ModiBy, DateTime tPointOfSalePlanning_DateModiBy, string pdv_contact)
                    Pmensaje.CssClass = "MensajesSupConfirm";
                    lblencabezado.Text = "Cargar Puntos de venta";
                    lblmensajegeneral.Text = "El punto de venta '" + LblNamePDV.Text + "', se adicionó a la Campaña " + CmbSelPresupuestoPDV.SelectedItem.Text ;
                    Mensajes_Usuario();
                    TxtCodigoPDV.Text = "";
                    LblNamePDV.Text = "------------------------------------------------------------------------------------------------";
                    LblDirPDV.Text = "------------------------------------------------------------------------------------------------";
                    LblCiudad.Text = "";
                    LblTipoAgrupacion.Text = "";
                    LblAgrupacion.Text = "";
                    LblOficina.Text = "";
                    LblMalla.Text = "";
                    LblSector.Text = "";
                    BtnCargaUnoaUno.Visible = false;
                    Validarobjeto();
                }
                else
                {
                    Pmensaje.CssClass = "MensajesSupervisor";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "El punto de venta '" + LblNamePDV.Text + "', ya se encuentra asignado a esta campaña. Por favor verifique.";
                    Mensajes_Usuario();
                }
            }
        }
    }
}