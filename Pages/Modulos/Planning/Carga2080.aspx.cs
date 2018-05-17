using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

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


namespace SIGE.Pages.Modulos.Planning
{
    public partial class Carga2080 : System.Web.UI.Page
    {
        private void Mensajes_Usuario()
        {
            ModalPopupMensaje.Show();
        }

        private void Mensaje_Confirmacion()
        {
            ModalConfirmacion.Show(); 
        }

        Conexion oCoon = new Conexion();

        protected void Page_Load(object sender, EventArgs e)
        {

            BtnCargaArchivo.Attributes.Add("onclick", "javascript:document.getElementById('"

+ BtnCargaArchivo.ClientID + "').disabled=true;" + "javascript:document.getElementById('"

+ BtnCargaArchivo.ClientID + "').value='Cargando...';" + this.GetPostBackEventReference(BtnCargaArchivo));


//            BtnCargaArchivo.Attributes.Add("onclick", "javascript:document.getElementById('"

//+ FileUp2080.ClientID + "').disabled=true;" + this.GetPostBackEventReference(FileUp2080));

            
            if (!IsPostBack)
            {
                try
                {
                    LblPresupuestoPDV.Text = this.Session["Presupuestoreportes"].ToString().Trim();
                    LblPlanning.Text =    this.Session["id_planningReportes"].ToString().Trim();

                    DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAREPORTESPLANNINGPRECIOS", LblPlanning.Text, 19);
                    if (DT != null)
                    {
                        if (DT.Rows.Count > 0)
                        {
                            GvRep.DataSource = DT;
                            GvRep.DataBind();
                        }
                        else
                        {
                            Pmensaje.CssClass = "MensajesSupervisor";
                            lblencabezado.Text = "Sr. Usuario";
                            lblmensajegeneral.Text = "Ha seleccionado una campaña a la cual no se le han creado periodos válidos en el reporte de precios.";
                            Mensajes_Usuario();
                        }
                    }
                   

                    if (this.Session["2080"].ToString().Trim() == "80")
                    {
                        LblTitCargarArchivo.Text = "Carga de archivo 80";
                    }
                    if (this.Session["2080"].ToString().Trim() == "20")
                    {
                        LblTitCargarArchivo.Text = "Carga de archivo 20";
                    }
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

        protected void BtnCargaArchivo_Click(object sender, EventArgs e)
        {

 

            if ((FileUp2080.PostedFile != null) && (FileUp2080.PostedFile.ContentLength > 0))
            {
                if (GvRep.SelectedIndex != -1)
                {
                    if (this.Session["2080"].ToString().Trim() == "20")
                    {

                        if ((FileUp2080.PostedFile != null) && (FileUp2080.PostedFile.ContentLength > 0))
                        {
                            string fn = System.IO.Path.GetFileName(FileUp2080.PostedFile.FileName);
                            string SaveLocation = Server.MapPath("PDV_Planning") + "\\" + fn;

                            if (SaveLocation != string.Empty)
                            {
                                if (FileUp2080.FileName.ToLower().EndsWith(".xls"))
                                {
                                    OleDbConnection oConn1 = new OleDbConnection();
                                    OleDbCommand oCmd = new OleDbCommand();
                                    OleDbDataAdapter oDa = new OleDbDataAdapter();
                                    DataSet oDs = new DataSet();

                                    DataTable dt = new DataTable();


                                    FileUp2080.PostedFile.SaveAs(SaveLocation);

                                    // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                                    oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                                    oConn1.Open();
                                    oCmd.CommandText = ConfigurationManager.AppSettings["Archivo20"];
                                    oCmd.Connection = oConn1;
                                    oDa.SelectCommand = oCmd;
                                    try
                                    {

                                        oDa.Fill(oDs);
                                        dt = oDs.Tables[0];
                                        if (dt.Columns.Count == 23)
                                        {
                                            dt.Columns[0].ColumnName = "Fecha";
                                            dt.Columns[1].ColumnName = "Tipo";
                                            dt.Columns[2].ColumnName = "Org";
                                            dt.Columns[3].ColumnName = "Can";
                                            dt.Columns[4].ColumnName = "Ofic";
                                            dt.Columns[5].ColumnName = "Esq.Cli";
                                            dt.Columns[6].ColumnName = "Segmento";
                                            dt.Columns[7].ColumnName = "Material";
                                            dt.Columns[8].ColumnName = "Descripción";
                                            dt.Columns[9].ColumnName = "Contado";
                                            dt.Columns[10].ColumnName = "Crédito";
                                            dt.Columns[11].ColumnName = "Gr.M";
                                            dt.Columns[12].ColumnName = "Cat";
                                            dt.Columns[13].ColumnName = "Mar";
                                            dt.Columns[14].ColumnName = "Fam";
                                            dt.Columns[15].ColumnName = "Var";
                                            dt.Columns[16].ColumnName = "Lin";
                                            dt.Columns[17].ColumnName = "Desc.Oficina";
                                            dt.Columns[18].ColumnName = "Desc.Categoría";
                                            dt.Columns[19].ColumnName = "Desc.Marca";
                                            dt.Columns[20].ColumnName = "Desc.Grupo de Materiales";
                                            dt.Columns[21].ColumnName = "Desc.Jerarquía";
                                            dt.Columns[22].ColumnName = "Cond. Credito";



                                            GvlogArchivo2080.DataSource = dt;
                                            GvlogArchivo2080.DataBind();


                                            for (int i = 0; i <= GvlogArchivo2080.Rows.Count - 1; i++)
                                            {
                                                for (int j = 0; j <= 22; j++)
                                                {
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&nbsp;", "");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#160;", "");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#193;", "á");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#201;", "é");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#205;", "í");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#211;", "ó");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#218;", "ú");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#225;", "á");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#233;", "é");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#237;", "í");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#243;", "ó");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#250;", "ú");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#209;", "ñ");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#241;", "ñ");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&amp;", "&");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#176;", "o");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#186;", "o");

                                                }
                                            }

                                            for (int i = 0; i <= GvlogArchivo2080.Rows.Count - 1; i++)
                                            {

                                                oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_insertarInterfaceAlicorp_MPreMay_cambiosTmp",
                                                    Convert.ToDateTime(TxtFechaini.Text),
                                                      Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[1].Text),
                                                     Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[2].Text),
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[3].Text),
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[4].Text),
                                                   Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[5].Text),
                                                    GvlogArchivo2080.Rows[i].Cells[6].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[7].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[8].Text,
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[9].Text),
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[10].Text),
                                                    GvlogArchivo2080.Rows[i].Cells[11].Text,
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[12].Text),
                                                    GvlogArchivo2080.Rows[i].Cells[13].Text,
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[14].Text),
                                                    GvlogArchivo2080.Rows[i].Cells[15].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[16].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[17].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[18].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[19].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[20].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[21].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[22].Text);
                                            }

                                            DataTable dtLogFinal = new DataTable();
                                            dtLogFinal.Columns.Add("Fecha");
                                            dtLogFinal.Columns.Add("Tipo");
                                            dtLogFinal.Columns.Add("Org");
                                            dtLogFinal.Columns.Add("Can");
                                            dtLogFinal.Columns.Add("Ofic");
                                            dtLogFinal.Columns.Add("EsqCli");
                                            dtLogFinal.Columns.Add("Segmento");
                                            dtLogFinal.Columns.Add("Material");
                                            dtLogFinal.Columns.Add("Descripción");
                                            dtLogFinal.Columns.Add("Contado");
                                            dtLogFinal.Columns.Add("Crédito");
                                            dtLogFinal.Columns.Add("GrM");
                                            dtLogFinal.Columns.Add("Cat");
                                            dtLogFinal.Columns.Add("Mar");
                                            dtLogFinal.Columns.Add("Fam");
                                            dtLogFinal.Columns.Add("Var");
                                            dtLogFinal.Columns.Add("Lin");
                                            dtLogFinal.Columns.Add("DescOficina");
                                            dtLogFinal.Columns.Add("DescCategoría");
                                            dtLogFinal.Columns.Add("DescMarca");
                                            dtLogFinal.Columns.Add("DescGrupodeMateriales");
                                            dtLogFinal.Columns.Add("DescJerarquía");
                                            dtLogFinal.Columns.Add("CondCredito");
                                            dtLogFinal.Columns.Add("Detalle_error");




                                            DataTable dtExisteProd = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAinterfaceAlicorp_MPreMay", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "Log_Productos");
                                            if (dtExisteProd != null)
                                            {
                                                if (dtExisteProd.Rows.Count > 0)
                                                {
                                                    for (int i = 0; i < dtExisteProd.Rows.Count; i++)
                                                    {
                                                        DataRow dr = dtLogFinal.NewRow();
                                                        dr["Fecha"] = dtExisteProd.Rows[i][0].ToString().Trim();
                                                        dr["Tipo"] = dtExisteProd.Rows[i][1].ToString().Trim();
                                                        dr["Org"] = dtExisteProd.Rows[i][2].ToString().Trim();
                                                        dr["Can"] = dtExisteProd.Rows[i][3].ToString().Trim();
                                                        dr["Ofic"] = dtExisteProd.Rows[i][4].ToString().Trim();
                                                        dr["EsqCli"] = dtExisteProd.Rows[i][5].ToString().Trim();
                                                        dr["Segmento"] = dtExisteProd.Rows[i][6].ToString().Trim();
                                                        dr["Material"] = dtExisteProd.Rows[i][7].ToString().Trim();
                                                        dr["Descripción"] = dtExisteProd.Rows[i][8].ToString().Trim();
                                                        dr["Contado"] = dtExisteProd.Rows[i][9].ToString().Trim();
                                                        dr["Crédito"] = dtExisteProd.Rows[i][10].ToString().Trim();
                                                        dr["GrM"] = dtExisteProd.Rows[i][11].ToString().Trim();
                                                        dr["Cat"] = dtExisteProd.Rows[i][12].ToString().Trim();
                                                        dr["Mar"] = dtExisteProd.Rows[i][13].ToString().Trim();
                                                        dr["Fam"] = dtExisteProd.Rows[i][14].ToString().Trim();
                                                        dr["Var"] = dtExisteProd.Rows[i][15].ToString().Trim();
                                                        dr["Lin"] = dtExisteProd.Rows[i][16].ToString().Trim();
                                                        dr["DescOficina"] = dtExisteProd.Rows[i][17].ToString().Trim();
                                                        dr["DescCategoría"] = dtExisteProd.Rows[i][18].ToString().Trim();
                                                        dr["DescMarca"] = dtExisteProd.Rows[i][19].ToString().Trim();
                                                        dr["DescGrupodeMateriales"] = dtExisteProd.Rows[i][20].ToString().Trim();
                                                        dr["DescJerarquía"] = dtExisteProd.Rows[i][21].ToString().Trim();
                                                        dr["CondCredito"] = dtExisteProd.Rows[i][22].ToString().Trim();
                                                        dr["Detalle_error"] = "Producto inexistente el maestro de productos";
                                                        dtLogFinal.Rows.Add(dr);
                                                    }
                                                    DataTable dtDelnoExisteProd = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAinterfaceAlicorp_MPreMay", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "DelLog_Productos");
                                                }
                                            }

                                            DataTable dtExisteOficina = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAinterfaceAlicorp_MPreMay", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "Log_oficinas");
                                            if (dtExisteOficina != null)
                                            {
                                                if (dtExisteOficina.Rows.Count > 0)
                                                {
                                                    for (int i = 0; i < dtExisteOficina.Rows.Count; i++)
                                                    {
                                                        DataRow dr = dtLogFinal.NewRow();
                                                        dr["Fecha"] = dtExisteOficina.Rows[i][0].ToString().Trim();
                                                        dr["Tipo"] = dtExisteOficina.Rows[i][1].ToString().Trim();
                                                        dr["Org"] = dtExisteOficina.Rows[i][2].ToString().Trim();
                                                        dr["Can"] = dtExisteOficina.Rows[i][3].ToString().Trim();
                                                        dr["Ofic"] = dtExisteOficina.Rows[i][4].ToString().Trim();
                                                        dr["EsqCli"] = dtExisteOficina.Rows[i][5].ToString().Trim();
                                                        dr["Segmento"] = dtExisteOficina.Rows[i][6].ToString().Trim();
                                                        dr["Material"] = dtExisteOficina.Rows[i][7].ToString().Trim();
                                                        dr["Descripción"] = dtExisteOficina.Rows[i][8].ToString().Trim();
                                                        dr["Contado"] = dtExisteOficina.Rows[i][9].ToString().Trim();
                                                        dr["Crédito"] = dtExisteOficina.Rows[i][10].ToString().Trim();
                                                        dr["GrM"] = dtExisteOficina.Rows[i][11].ToString().Trim();
                                                        dr["Cat"] = dtExisteOficina.Rows[i][12].ToString().Trim();
                                                        dr["Mar"] = dtExisteOficina.Rows[i][13].ToString().Trim();
                                                        dr["Fam"] = dtExisteOficina.Rows[i][14].ToString().Trim();
                                                        dr["Var"] = dtExisteOficina.Rows[i][15].ToString().Trim();
                                                        dr["Lin"] = dtExisteOficina.Rows[i][16].ToString().Trim();
                                                        dr["DescOficina"] = dtExisteOficina.Rows[i][17].ToString().Trim();
                                                        dr["DescCategoría"] = dtExisteOficina.Rows[i][18].ToString().Trim();
                                                        dr["DescMarca"] = dtExisteOficina.Rows[i][19].ToString().Trim();
                                                        dr["DescGrupodeMateriales"] = dtExisteOficina.Rows[i][20].ToString().Trim();
                                                        dr["DescJerarquía"] = dtExisteOficina.Rows[i][21].ToString().Trim();
                                                        dr["CondCredito"] = dtExisteOficina.Rows[i][22].ToString().Trim();
                                                        dr["Detalle_error"] = "Oficina inexistente el maestro de Oficinas";
                                                        dtLogFinal.Rows.Add(dr);
                                                    }
                                                    DataTable dtDelnoExisteProd = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAinterfaceAlicorp_MPreMay", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "DelLog_oficinas");
                                                }
                                            }

                                            if (dtExisteOficina.Rows.Count > 0 || dtExisteProd.Rows.Count > 0)
                                            {
                                                string fnn = "Log_Archivo20_.xls";
                                                string pathlog = Server.MapPath("PDV_Planning") + "\\" + fnn;
                                                if (ExportarExcelDataTable(dtLogFinal, pathlog))
                                                {


                                                    //Se envia el correo con el Log generado Ing. Carlos Alberto Hernández Rincón 30/01/2011

                                                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                                    System.Net.Mail.Attachment file = new System.Net.Mail.Attachment(pathlog);//Adjunta el Log Generado
                                                    correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                                    correo.To.Add(this.Session["smail"].ToString());
                                                    correo.Subject = "Errores en archivo 20";
                                                    correo.Attachments.Add(file);
                                                    correo.IsBodyHtml = true;
                                                    correo.Priority = System.Net.Mail.MailPriority.Low;
                                                    string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                                        this.Session["nameuser"].ToString() + "<br/>" + "<br/>" + 
                                                        "El archivo 20 que usted seleccionó para la carga contiene información no válida ." + "<br/>" +
                                                         "<br/>" +  "<br/>" +                                           
                                                        "Por favor verifique el archivo adjunto a este correo , el cual contiene información importante para usted" + "<br/>" +                                                                                    
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

                                            DataTable dtCargaFinal = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAinterfaceAlicorp_MPreMay", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "Carga_final");
                                            DataTable dtexistentes = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAinterfaceAlicorp_MPreMay", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "NO");
                                            DataTable dtdeltmp = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAinterfaceAlicorp_MPreMay", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "DeleteTmp");

                                            int countLog = dtExisteOficina.Rows.Count + dtExisteProd.Rows.Count;
                                            string Msjcorreo = "";
                                            if (countLog > 0)
                                            {
                                                Msjcorreo = " Verifique su correo";
                                            }
                                            Pmensaje.CssClass = "MensajesSupConfirm";
                                            lblencabezado.Text = "Sr. Usuario";
                                            lblmensajegeneral.Text = "Se ha cargado con éxito el archivo 20 para la campaña : " + LblPresupuestoPDV.Text.ToUpper() + ". <br/> cargados : " + dtexistentes.Rows.Count + " No cargados: " + countLog + Msjcorreo;

                                            Mensajes_Usuario();
                                            dtExisteOficina = null;
                                            dtExisteProd = null;
                                            dtCargaFinal = null;
                                            dtexistentes = null;
                                            dtdeltmp = null;
                                        }
                                        else
                                        {
                                            GvRep.DataBind();


                                            Pmensaje.CssClass = "MensajesSupervisor";
                                            lblencabezado.Text = "Sr. Usuario";
                                            lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo 20 válido. Por favor verifique la estructura que fue enviada a su correo.";


                                            Mensajes_Usuario();

                                            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                            correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                            correo.To.Add(this.Session["smail"].ToString());
                                            correo.Subject = "Errores en archivo 20";
                                            correo.IsBodyHtml = true;
                                            correo.Priority = System.Net.Mail.MailPriority.Normal;
                                            string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                            this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                   
                                             

                                            "El archivo 20 que usted seleccionó para la carga no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga las siguientes columnas" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique la columna de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                             "Columna 1  : Fecha" + "<br/>" + 
                                             "Columna 2  : Tipo" + "<br/>" + 
                                             "Columna 3  : Org" + "<br/>" + 
                                             "Columna 4  : Can" + "<br/>" + 
                                             "Columna 5  : Ofic" + "<br/>" + 
                                             "Columna 6  : Esq.Cli" + "<br/>" + 
                                             "Columna 7  : Segmento" + "<br/>" + 
                                             "Columna 8  : Material" + "<br/>" + 
                                             "Columna 9  : Descripción" + "<br/>" + 
                                             "Columna 10  : Contado" + "<br/>" + 
                                             "Columna 11  : Crédito" + "<br/>" + 
                                             "Columna 12  : Gr.M" + "<br/>" + 
                                             "Columna 13  : Cat" + "<br/>" + 
                                             "Columna 14  : Mar" + "<br/>" + 
                                             "Columna 15  : Fam" + "<br/>" + 
                                             "Columna 16  : Var" + "<br/>" + 
                                             "Columna 17  : Lin" + "<br/>" + 
                                             "Columna 18  : Desc.Oficina" + "<br/>" + 
                                             "Columna 19  : Desc.Categoría" + "<br/>" + 
                                             "Columna 20  : Desc.Marca" + "<br/>" + 
                                             "Columna 21  : Desc.Grupo de Materiales" + "<br/>" + 
                                             "Columna 22  : Desc.Jerarquía" + "<br/>" +  
                                             "Columna 23  : Cond. Credito" + "<br/>" +                                          
                                            "Nota:  No es indispensable que la columna se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizarlo a su gusto ." +
                                            "Pero tenga en cuenta que debe ingresar la información del archivo 20 en ese orden expuesto anteriormente ." + "<br/>" + "<br/>" + "<br/>" +
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
                                        lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo válido. Por favor verifique que el nombre de la hoja sea Archivo20";

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
                    if (this.Session["2080"].ToString().Trim() == "80")
                    {
                        if ((FileUp2080.PostedFile != null) && (FileUp2080.PostedFile.ContentLength > 0))
                        {
                            string fn = System.IO.Path.GetFileName(FileUp2080.PostedFile.FileName);
                            string SaveLocation = Server.MapPath("PDV_Planning") + "\\" + fn;

                            if (SaveLocation != string.Empty)
                            {
                                if (FileUp2080.FileName.ToLower().EndsWith(".xls"))
                                {
                                    OleDbConnection oConn1 = new OleDbConnection();
                                    OleDbCommand oCmd = new OleDbCommand();
                                    OleDbDataAdapter oDa = new OleDbDataAdapter();
                                    DataSet oDs = new DataSet();

                                    DataTable dt = new DataTable();


                                    FileUp2080.PostedFile.SaveAs(SaveLocation);

                                    // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                                    oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                                    oConn1.Open();
                                    oCmd.CommandText = ConfigurationManager.AppSettings["Archivo80"];
                                    oCmd.Connection = oConn1;
                                    oDa.SelectCommand = oCmd;
                                    try
                                    {
                                        oDa.Fill(oDs);
                                        dt = oDs.Tables[0];
                                        if (dt.Columns.Count == 23)
                                        {
                                            dt.Columns[0].ColumnName = "Fecha";
                                            dt.Columns[1].ColumnName = "Tipo";
                                            dt.Columns[2].ColumnName = "Org";
                                            dt.Columns[3].ColumnName = "Can";
                                            dt.Columns[4].ColumnName = "Ofic";
                                            dt.Columns[5].ColumnName = "Esq.Cli";
                                            dt.Columns[6].ColumnName = "Segmento";
                                            dt.Columns[7].ColumnName = "Material";
                                            dt.Columns[8].ColumnName = "Descripción";
                                            dt.Columns[9].ColumnName = "Contado";
                                            dt.Columns[10].ColumnName = "Crédito";
                                            dt.Columns[11].ColumnName = "Gr.M";
                                            dt.Columns[12].ColumnName = "Cat";
                                            dt.Columns[13].ColumnName = "Mar";
                                            dt.Columns[14].ColumnName = "Fam";
                                            dt.Columns[15].ColumnName = "Var";
                                            dt.Columns[16].ColumnName = "Lin";
                                            dt.Columns[17].ColumnName = "Desc.Oficina";
                                            dt.Columns[18].ColumnName = "Desc.Categoría";
                                            dt.Columns[19].ColumnName = "Desc.Marca";
                                            dt.Columns[20].ColumnName = "Desc.Grupo de Materiales";
                                            dt.Columns[21].ColumnName = "Desc.Jerarquía";
                                            dt.Columns[22].ColumnName = "Cond. Credito";



                                            GvlogArchivo2080.DataSource = dt;
                                            GvlogArchivo2080.DataBind();


                                            for (int i = 0; i <= GvlogArchivo2080.Rows.Count - 1; i++)
                                            {
                                                for (int j = 0; j <= 22; j++)
                                                {
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&nbsp;", "");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#160;", "");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#193;", "á");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#201;", "é");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#205;", "í");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#211;", "ó");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#218;", "ú");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#225;", "á");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#233;", "é");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#237;", "í");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#243;", "ó");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#250;", "ú");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#209;", "ñ");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#241;", "ñ");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&amp;", "&");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#176;", "o");
                                                    GvlogArchivo2080.Rows[i].Cells[j].Text = GvlogArchivo2080.Rows[i].Cells[j].Text.Replace("&#186;", "o");

                                                }
                                            }

                                            for (int i = 0; i <= GvlogArchivo2080.Rows.Count - 1; i++)
                                            {

                                                oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_insertarInterfaceAlicorp_PreDex_cambiosTmp",
                                                    Convert.ToDateTime(TxtFechaini.Text),
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[1].Text),
                                                     Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[2].Text),
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[3].Text),
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[4].Text),
                                                   Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[5].Text),
                                                    GvlogArchivo2080.Rows[i].Cells[6].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[7].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[8].Text,
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[9].Text),
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[10].Text),
                                                    GvlogArchivo2080.Rows[i].Cells[11].Text,
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[12].Text),
                                                    GvlogArchivo2080.Rows[i].Cells[13].Text,
                                                    Convert.ToDouble(GvlogArchivo2080.Rows[i].Cells[14].Text),
                                                    GvlogArchivo2080.Rows[i].Cells[15].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[16].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[17].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[18].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[19].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[20].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[21].Text,
                                                    GvlogArchivo2080.Rows[i].Cells[22].Text);
                                            }

                                            DataTable dtLogFinal = new DataTable();
                                            dtLogFinal.Columns.Add("Fecha");
                                            dtLogFinal.Columns.Add("Tipo");
                                            dtLogFinal.Columns.Add("Org");
                                            dtLogFinal.Columns.Add("Can");
                                            dtLogFinal.Columns.Add("Ofic");
                                            dtLogFinal.Columns.Add("EsqCli");
                                            dtLogFinal.Columns.Add("Segmento");
                                            dtLogFinal.Columns.Add("Material");
                                            dtLogFinal.Columns.Add("Descripción");
                                            dtLogFinal.Columns.Add("Contado");
                                            dtLogFinal.Columns.Add("Crédito");
                                            dtLogFinal.Columns.Add("GrM");
                                            dtLogFinal.Columns.Add("Cat");
                                            dtLogFinal.Columns.Add("Mar");
                                            dtLogFinal.Columns.Add("Fam");
                                            dtLogFinal.Columns.Add("Var");
                                            dtLogFinal.Columns.Add("Lin");
                                            dtLogFinal.Columns.Add("DescOficina");
                                            dtLogFinal.Columns.Add("DescCategoría");
                                            dtLogFinal.Columns.Add("DescMarca");
                                            dtLogFinal.Columns.Add("DescGrupodeMateriales");
                                            dtLogFinal.Columns.Add("DescJerarquía");
                                            dtLogFinal.Columns.Add("CondCredito");
                                            dtLogFinal.Columns.Add("Detalle_error");




                                            DataTable dtExisteProd = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAInterfaceAlicorp_PreDex", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "Log_Productos");
                                            if (dtExisteProd != null)
                                            {
                                                if (dtExisteProd.Rows.Count > 0)
                                                {
                                                    for (int i = 0; i < dtExisteProd.Rows.Count; i++)
                                                    {
                                                        DataRow dr = dtLogFinal.NewRow();
                                                        dr["Fecha"] = dtExisteProd.Rows[i][0].ToString().Trim();
                                                        dr["Tipo"] = dtExisteProd.Rows[i][1].ToString().Trim();
                                                        dr["Org"] = dtExisteProd.Rows[i][2].ToString().Trim();
                                                        dr["Can"] = dtExisteProd.Rows[i][3].ToString().Trim();
                                                        dr["Ofic"] = dtExisteProd.Rows[i][4].ToString().Trim();
                                                        dr["EsqCli"] = dtExisteProd.Rows[i][5].ToString().Trim();
                                                        dr["Segmento"] = dtExisteProd.Rows[i][6].ToString().Trim();
                                                        dr["Material"] = dtExisteProd.Rows[i][7].ToString().Trim();
                                                        dr["Descripción"] = dtExisteProd.Rows[i][8].ToString().Trim();
                                                        dr["Contado"] = dtExisteProd.Rows[i][9].ToString().Trim();
                                                        dr["Crédito"] = dtExisteProd.Rows[i][10].ToString().Trim();
                                                        dr["GrM"] = dtExisteProd.Rows[i][11].ToString().Trim();
                                                        dr["Cat"] = dtExisteProd.Rows[i][12].ToString().Trim();
                                                        dr["Mar"] = dtExisteProd.Rows[i][13].ToString().Trim();
                                                        dr["Fam"] = dtExisteProd.Rows[i][14].ToString().Trim();
                                                        dr["Var"] = dtExisteProd.Rows[i][15].ToString().Trim();
                                                        dr["Lin"] = dtExisteProd.Rows[i][16].ToString().Trim();
                                                        dr["DescOficina"] = dtExisteProd.Rows[i][17].ToString().Trim();
                                                        dr["DescCategoría"] = dtExisteProd.Rows[i][18].ToString().Trim();
                                                        dr["DescMarca"] = dtExisteProd.Rows[i][19].ToString().Trim();
                                                        dr["DescGrupodeMateriales"] = dtExisteProd.Rows[i][20].ToString().Trim();
                                                        dr["DescJerarquía"] = dtExisteProd.Rows[i][21].ToString().Trim();
                                                        dr["CondCredito"] = dtExisteProd.Rows[i][22].ToString().Trim();
                                                        dr["Detalle_error"] = "Producto inexistente el maestro de productos";
                                                        dtLogFinal.Rows.Add(dr);
                                                    }

                                                    DataTable dtDelnoExisteProd = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAInterfaceAlicorp_PreDex", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "DelLog_Productos");
                                                }
                                            }

                                            DataTable dtExisteOficina = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAInterfaceAlicorp_PreDex", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "Log_oficinas");
                                            if (dtExisteOficina != null)
                                            {
                                                if (dtExisteOficina.Rows.Count > 0)
                                                {
                                                    for (int i = 0; i < dtExisteOficina.Rows.Count; i++)
                                                    {
                                                        DataRow dr = dtLogFinal.NewRow();
                                                        dr["Fecha"] = dtExisteOficina.Rows[i][0].ToString().Trim();
                                                        dr["Tipo"] = dtExisteOficina.Rows[i][1].ToString().Trim();
                                                        dr["Org"] = dtExisteOficina.Rows[i][2].ToString().Trim();
                                                        dr["Can"] = dtExisteOficina.Rows[i][3].ToString().Trim();
                                                        dr["Ofic"] = dtExisteOficina.Rows[i][4].ToString().Trim();
                                                        dr["EsqCli"] = dtExisteOficina.Rows[i][5].ToString().Trim();
                                                        dr["Segmento"] = dtExisteOficina.Rows[i][6].ToString().Trim();
                                                        dr["Material"] = dtExisteOficina.Rows[i][7].ToString().Trim();
                                                        dr["Descripción"] = dtExisteOficina.Rows[i][8].ToString().Trim();
                                                        dr["Contado"] = dtExisteOficina.Rows[i][9].ToString().Trim();
                                                        dr["Crédito"] = dtExisteOficina.Rows[i][10].ToString().Trim();
                                                        dr["GrM"] = dtExisteOficina.Rows[i][11].ToString().Trim();
                                                        dr["Cat"] = dtExisteOficina.Rows[i][12].ToString().Trim();
                                                        dr["Mar"] = dtExisteOficina.Rows[i][13].ToString().Trim();
                                                        dr["Fam"] = dtExisteOficina.Rows[i][14].ToString().Trim();
                                                        dr["Var"] = dtExisteOficina.Rows[i][15].ToString().Trim();
                                                        dr["Lin"] = dtExisteOficina.Rows[i][16].ToString().Trim();
                                                        dr["DescOficina"] = dtExisteOficina.Rows[i][17].ToString().Trim();
                                                        dr["DescCategoría"] = dtExisteOficina.Rows[i][18].ToString().Trim();
                                                        dr["DescMarca"] = dtExisteOficina.Rows[i][19].ToString().Trim();
                                                        dr["DescGrupodeMateriales"] = dtExisteOficina.Rows[i][20].ToString().Trim();
                                                        dr["DescJerarquía"] = dtExisteOficina.Rows[i][21].ToString().Trim();
                                                        dr["CondCredito"] = dtExisteOficina.Rows[i][22].ToString().Trim();
                                                        dr["Detalle_error"] = "Oficina inexistente el maestro de Oficinas";
                                                        dtLogFinal.Rows.Add(dr);
                                                    }
                                                    DataTable dtDelnoExisteProd = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAInterfaceAlicorp_PreDex", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "DelLog_oficinas");
                                                }
                                            }

                                            if (dtExisteOficina.Rows.Count > 0 || dtExisteProd.Rows.Count > 0)
                                            {
                                                string fnn = "Log_Archivo80_.xls";
                                                string pathlog = Server.MapPath("PDV_Planning") + "\\" + fnn;
                                                if (ExportarExcelDataTable(dtLogFinal, pathlog))
                                                {


                                                    //Se envia el correo con el Log generado Ing. Carlos Alberto Hernández Rincón 30/01/2011

                                                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                                    System.Net.Mail.Attachment file = new System.Net.Mail.Attachment(pathlog);//Adjunta el Log Generado
                                                    correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                                    correo.To.Add(this.Session["smail"].ToString());
                                                    correo.Subject = "Errores en archivo 80";
                                                    correo.Attachments.Add(file);
                                                    correo.IsBodyHtml = true;
                                                    correo.Priority = System.Net.Mail.MailPriority.Low;
                                                    string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                                        this.Session["nameuser"].ToString() + "<br/>" + "<br/>" + 
                                                        "El archivo 80 que usted seleccionó para la carga contiene información no válida ." + "<br/>" +
                                                         "<br/>" +  "<br/>" +                                           
                                                        "Por favor verifique el archivo adjunto a este correo , el cual contiene información importante para usted" + "<br/>" +                                                                                    
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

                                            DataTable dtCargaFinal = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAInterfaceAlicorp_PreDex", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "Carga_final");
                                            DataTable dtexistentes = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAInterfaceAlicorp_PreDex", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "NO");
                                            DataTable dtdeltmp = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAInterfaceAlicorp_PreDex", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "DeleteTmp");

                                            int countLog = dtExisteOficina.Rows.Count + dtExisteProd.Rows.Count;
                                            string Msjcorreo = "";
                                            if (countLog > 0)
                                            {
                                                Msjcorreo = " Verifique su correo";
                                            }
                                            Pmensaje.CssClass = "MensajesSupConfirm";
                                            lblencabezado.Text = "Sr. Usuario";
                                            lblmensajegeneral.Text = "Se ha cargado con éxito el archivo 80 para la campaña : " + LblPresupuestoPDV.Text.ToUpper() + ". <br/> cargados : " + dtexistentes.Rows.Count + " No cargados: " + countLog + Msjcorreo;

                                            Mensajes_Usuario();
                                            dtExisteOficina = null;
                                            dtExisteProd = null;
                                            dtCargaFinal = null;
                                            dtexistentes = null;
                                            dtdeltmp = null;
                                        }
                                        else
                                        {
                                            GvRep.DataBind();


                                            Pmensaje.CssClass = "MensajesSupervisor";
                                            lblencabezado.Text = "Sr. Usuario";
                                            lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo 80 válido. Por favor verifique la estructura que fue enviada a su correo.";


                                            Mensajes_Usuario();

                                            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                            correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                            correo.To.Add(this.Session["smail"].ToString());
                                            correo.Subject = "Errores en archivo 80";
                                            correo.IsBodyHtml = true;
                                            correo.Priority = System.Net.Mail.MailPriority.Normal;
                                            string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                            this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                   
                                             

                                            "El archivo 80 que usted seleccionó para la carga no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga las siguientes columnas" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique la columna de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                             "Columna 1  : Fecha" + "<br/>" + 
                                             "Columna 2  : Tipo" + "<br/>" + 
                                             "Columna 3  : Org" + "<br/>" + 
                                             "Columna 4  : Can" + "<br/>" + 
                                             "Columna 5  : Ofic" + "<br/>" + 
                                             "Columna 6  : Esq.Cli" + "<br/>" + 
                                             "Columna 7  : Segmento" + "<br/>" + 
                                             "Columna 8  : Material" + "<br/>" + 
                                             "Columna 9  : Descripción" + "<br/>" + 
                                             "Columna 10  : Contado" + "<br/>" + 
                                             "Columna 11  : Crédito" + "<br/>" + 
                                             "Columna 12  : Gr.M" + "<br/>" + 
                                             "Columna 13  : Cat" + "<br/>" + 
                                             "Columna 14  : Mar" + "<br/>" + 
                                             "Columna 15  : Fam" + "<br/>" + 
                                             "Columna 16  : Var" + "<br/>" + 
                                             "Columna 17  : Lin" + "<br/>" + 
                                             "Columna 18  : Desc.Oficina" + "<br/>" + 
                                             "Columna 19  : Desc.Categoría" + "<br/>" + 
                                             "Columna 20  : Desc.Marca" + "<br/>" + 
                                             "Columna 21  : Desc.Grupo de Materiales" + "<br/>" + 
                                             "Columna 22  : Desc.Jerarquía" + "<br/>" +  
                                             "Columna 23  : Cond. Credito" + "<br/>" +                                          
                                            "Nota:  No es indispensable que la columna se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizarlo a su gusto ." +
                                            "Pero tenga en cuenta que debe ingresar la información del archivo 80 en ese orden expuesto anteriormente ." + "<br/>" + "<br/>" + "<br/>" +
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
                                        lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo válido. Por favor verifique que el nombre de la hoja sea Archivo80";

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
                else
                {
                    Pmensaje.CssClass = "MensajesSupervisor";
                    lblencabezado.Text = "Sr. Usuario";
                    lblmensajegeneral.Text = "Por favor seleccione un elemento de la lista superior para asignar fechas de archivo";
                    Mensajes_Usuario();
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

        protected void GvRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblCargarArchivo.Visible = true;
            FileUp2080.Visible = true;
            BtnCargaArchivo.Visible = true;
           // tbFile.Visible = true;
            BtnCargaArchivo.Visible = true;
            TxtAño.Text = GvRep.SelectedRow.Cells[1].Text.ToString().Trim();
            TxtMes.Text = GvRep.SelectedRow.Cells[3].Text.ToString().Trim();
            TxtPeriodo.Text = GvRep.SelectedRow.Cells[4].Text.ToString().Trim();
            TxtFechaini.Text = GvRep.SelectedRow.Cells[5].Text.ToString().Trim();
            TxtFechaFin.Text = GvRep.SelectedRow.Cells[6].Text.ToString().Trim();

            if (this.Session["2080"].ToString().Trim() == "20")
            {
                DataTable dtexistentes = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAinterfaceAlicorp_MPreMay", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "NO");
                if (dtexistentes != null)
                {
                    if (dtexistentes.Rows.Count > 0)
                    {
                        Pmensaje.CssClass = "MensajesSupervisor";
                        LblSrUsuario.Text = "Sr. Usuario";
                        LblMensajeConfirm.Text = "Existen registros creados dentro de ese rango de fechas . Desea eliminarlos y cargar nuevo archivo";
                        Mensaje_Confirmacion();
                    }
                    else
                    {
                        Pmensaje.CssClass = "MensajesSupConfirm";
                        lblencabezado.Text = "Sr. Usuario";
                        lblmensajegeneral.Text = "Seleccione un archivo y oprima el botón Cargar archivo";
                        Mensajes_Usuario();
                    }
                }
            }

            if (this.Session["2080"].ToString().Trim() == "80")
            {
                DataTable dtexistentes = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAInterfaceAlicorp_PreDex", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "NO");
                if (dtexistentes != null)
                {
                    if (dtexistentes.Rows.Count > 0)
                    {
                        Pmensaje.CssClass = "MensajesSupervisor";
                        LblSrUsuario.Text = "Sr. Usuario";
                        LblMensajeConfirm.Text = "Existen registros creados dentro de ese rango de fechas . Desea eliminarlos y cargar nuevo archivo";
                        Mensaje_Confirmacion();
                    }
                    else
                    {
                        Pmensaje.CssClass = "MensajesSupConfirm";
                        lblencabezado.Text = "Sr. Usuario";
                        lblmensajegeneral.Text = "Seleccione un archivo y oprima el botón Cargar archivo";
                        Mensajes_Usuario();
                    }
                }
            }

        }

        protected void BtnSiConfirma_Click(object sender, EventArgs e)
        {
            if (this.Session["2080"].ToString().Trim() == "20")
            {
                oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAinterfaceAlicorp_MPreMay", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "SI");
            }
            if (this.Session["2080"].ToString().Trim() == "80")
            {
                oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAInterfaceAlicorp_PreDex", Convert.ToDateTime(TxtFechaini.Text), Convert.ToDateTime(TxtFechaFin.Text), "SI");
            }

            Pmensaje.CssClass = "MensajesSupConfirm";
            lblencabezado.Text = "Sr. Usuario";
            lblmensajegeneral.Text = "ha eliminado los registros satisfactoriamente, ahora seleccione el nuevo archivo a cargar";
            Mensajes_Usuario();
        }

        protected void BtnNoConfirma_Click(object sender, EventArgs e)
        {
            GvRep.SelectedIndex = -1;
            LblCargarArchivo.Visible = false;
            FileUp2080.Visible = false;
            BtnCargaArchivo.Visible = false;
            //tbFile.Visible = false;

            BtnCargaArchivo.Visible = false;
            TxtAño.Text = "";
            TxtMes.Text = "";
            TxtPeriodo.Text = "";
            TxtFechaini.Text = "";
            TxtFechaFin.Text = "";            
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

    }
}