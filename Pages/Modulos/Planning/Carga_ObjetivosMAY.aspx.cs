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
    public partial class Carga_ObjetivosMAY : System.Web.UI.Page
    {
        private Conexion oCoon = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                     //this.Session["PresupuestoProductos"].ToString().Trim();
                    LblPlanning.Text = this.Session["id_planning"].ToString().Trim();
                    LblCanal.Text= this.Session["Planning_CodChannel"].ToString().Trim();
                    lblCompany.Text = this.Session["company_id"].ToString().Trim();     
                    llenaReportesObjetivoSODMAY();


                }
                catch
                {
                    PmensajeCargaMasiva.CssClass = "MensajesSupervisor";
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
        private void llenaReportesObjetivoSODMAY()
        {
            DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CONSULTA_INFORMESYMESES_PLANNING", this.Session["id_planning"].ToString().Trim(), Convert.ToInt32(this.Session["company_id"]), this.Session["Planning_CodChannel"].ToString().Trim(), 0);
            ddlReporteObjetivoSODMAY.DataSource = ds.Tables[1];
            ddlReporteObjetivoSODMAY.DataValueField = "Report_id";
            ddlReporteObjetivoSODMAY.DataTextField = "Report_NameReport";
            ddlReporteObjetivoSODMAY.DataBind();
            ds = null;
        }
        public void llenarPeriodos(DropDownList ddl, string planning, int report, string año, string mes)
        {

            // string a = Session["idPlanning"].ToString();
            //string b = Session["id_report"].ToString();
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_OBTENERPERIODOS_xPlanningReporteAñoMes", planning, report, año, mes);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataValueField = "id_ReportsPlanning";
                    ddl.DataTextField = "Perido";
                    ddl.DataBind();
                }
                else
                {
                    ddl.Items.Clear();
                }
            }

            dt = null;

        }
        protected void ddlMesObjetivosSODMAY_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarPeriodos(dllPeridoObjetivosSODMAY, this.Session["id_planning"].ToString().Trim(), Convert.ToInt32(ddlReporteObjetivoSODMAY.SelectedValue), ddlAñoObjetivosSODMAY.SelectedValue, ddlMesObjetivosSODMAY.SelectedValue);
           
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

            string mensaje = "";
            if (dllPeridoObjetivosSODMAY.SelectedValue == "")
            {//genera mensaje de alerta al faltar ingresar compo requerido
                if (dllPeridoObjetivosSODMAY.SelectedValue == "")
                {
                    mensaje = ". " + "Seleccione un perido.";
                }

                lblencabezado.Text = "INFORMACION";
                lblmensajegeneral.Text = mensaje;
                ImgMensaje.ImageUrl = "~/Pages/images/Mensajes/warning_blue.png";
                divMensaje.Style.Value = "border-width:10px;border-style:solid;border-color:#53A2FF; height:169px;background-color:#9FCBFF";
                ModalPopupMensaje.Show();
                return;
            }


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
                        oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Pla_ObjetivosMay"];
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

                                dt.Columns[0].ColumnName = "Region";
                                dt.Columns[1].ColumnName = "Categoria";
                                dt.Columns[2].ColumnName = "Marca";
                                dt.Columns[3].ColumnName = "Obj. Marca";
                                dt.Columns[4].ColumnName = "Obj. Categoria";

                                ConnectionStringSettings settingconection;
                                settingconection = ConfigurationManager.ConnectionStrings["ConectaDBLucky"];
                                string oSqlConnIN = settingconection.ConnectionString;

                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oSqlConnIN))
                                {
                                    bulkCopy.DestinationTableName = "SUPE_OBJETIVOS_SOD_MAY_TMP";
                                    //carga los SKU's temporalmente para hacer el procedimiento a través de un SP
                                    bulkCopy.WriteToServer(dt);
                                }


                                string idPlanning = LblPlanning.Text;
                                string id_ReportsPlanning = dllPeridoObjetivosSODMAY.SelectedValue;
                                string usuario = this.Session["sUser"].ToString().Trim();

                                //realiza las validaciones y carga los productos a planning.
                                DataSet dsCargar = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CARGAMASIVA_ObjetivosMay_PLANNING_TMP",
                                Convert.ToInt32(lblCompany.Text), LblCanal.Text,
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

                            lblencabezado.Text = "INFORMACION";
                            lblmensajegeneral.Text = "Los Objetivos Fueron Cargados Correctamente";
                            ImgMensaje.ImageUrl = "~/Pages/images/Mensajes/Correct_1.png";
                            divMensaje.Style.Value = "border-width:10px;border-style:solid;border-color:#83D447; height:169px;background-color:#FFFFFF";
                            ModalPopupMensaje.Show();
                            //}
                        }
                        catch
                        {

                            lblencabezado.Text = "INFORMACION";
                            lblmensajegeneral.Text = "Tiene que seleccionar un archivo";
                            ImgMensaje.ImageUrl = "~/Pages/images/Mensajes/warning_blue.png";
                            divMensaje.Style.Value = "border-width:10px;border-style:solid;border-color:#53A2FF; height:169px;background-color:#9FCBFF";
                            ModalPopupMensaje.Show();


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

            string mensaje = "";
            if (dllPeridoObjetivosSODMAY.SelectedValue == "" || ddlProducto.SelectedValue == "0" || ddlPtoVenta.SelectedValue == "0" || txtPrecio.Text == "")
            {//genera mensaje de alerta al faltar ingresar compo requerido
                if (dllPeridoObjetivosSODMAY.SelectedValue == "")
                {
                    mensaje = ". " + "Seleccione un perido.";
                }
                if (ddlProducto.SelectedValue == "0")
                {
                    mensaje = mensaje + "Seleccione un perido.";
                }
                if (ddlPtoVenta.SelectedValue == "0")
                {
                    mensaje = mensaje + "Seleccione un Pto de Venta.";
                }
                if (txtPrecio.Text == "")
                {
                    mensaje = mensaje + "Ingrese un Precio.";
                }
                lblencabezado.Text = "INFORMACION";
                lblmensajegeneral.Text = mensaje;
                ImgMensaje.ImageUrl = "~/Pages/images/Mensajes/warning_blue.png";
                divMensaje.Style.Value = "border-width:10px;border-style:solid;border-color:#53A2FF; height:169px;background-color:#9FCBFF";
                ModalPopupMensaje.Show();
                return;
            }

            try
            {

                string usuario = this.Session["sUser"].ToString().Trim();

                DataSet dsCargar = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_INSERTAR_PRECIO_PLANNING",
                                        Convert.ToInt32(dllPeridoObjetivosSODMAY.SelectedValue),
                                        Convert.ToInt32(ddlPtoVenta.SelectedValue),
                                        Convert.ToInt32(ddlProducto.SelectedValue),
                                        Convert.ToDouble(txtPrecio.Text),
                                        true,
                                        usuario, DateTime.Now,
                                        usuario, DateTime.Now);

                lblencabezado.Text = "INFORMACION";
                lblmensajegeneral.Text = "Se registro correctamente";
                PmensajeCargaMasiva.Style.Value = "background-color: Green;";
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