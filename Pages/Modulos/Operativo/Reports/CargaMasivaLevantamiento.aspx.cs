using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
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
using System.Collections.Generic;
using System.Data.OleDb;
using Lucky.Entity;
using SIGE.Facade_Proceso_Planning;


namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class CargaMasivaLevantamiento : System.Web.UI.Page
    {
        private int compañia;
        Conexion oConn = new Lucky.Data.Conexion();
        OPE_REPORTE_PUBLICACION oLPublicacion = new OPE_REPORTE_PUBLICACION();
        OPE_REPORTE_EXHB_IMPULSO olExhibiImpulso = new OPE_REPORTE_EXHB_IMPULSO();
        OPE_REPORTE_MATERIAL_POP olMaterialPOP = new OPE_REPORTE_MATERIAL_POP();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region Funciones

        private void consultaUltimoIdmarca()
        {
            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_CONSULTA_ID_BRANDCOMPANYTMP");
            //llena el ultimo id de marca para insertar en bd intemedia  16/02/2011 Magaly Jiménez
            TxtCodBrand.Text = dt.Rows[0]["id_Brand"].ToString().Trim();
            dt = null;
        }
        private void consultaUltimoIdfamilia()
        {
            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_CONSULTA_ID_FAMILYTMP");
            //llena el ultimo id de marca para insertar en bd intemedia  7/04/2011 Magaly Jiménez

            this.Session["codigofamily"] = dt.Rows[0]["id_ProductFamily"].ToString().Trim();
            dt = null;
        }
        private void MensajeAlerta()
        {
            ModalPopupAlertas.Show();
            BtnAceptarAlert.Focus();
            //ScriptManager.RegisterStartupScript(
            //    this, this.GetType(), "myscript", "alert('Debe ingresar todos los parametros con (*)');", true);
        }
        private void llenaplanningAASS()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            compañia = Convert.ToInt32(this.Session["companyid"]);
            //string sidchannel = cmbcanal.SelectedValue;

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", 1241, compañia);

            cmbplanning.Items.Clear();


            if (dt.Rows.Count > 0)
            {
                cmbplanning.DataSource = dt;
                cmbplanning.DataValueField = "id_planning";
                cmbplanning.DataTextField = "Planning_Name";
                cmbplanning.DataBind();
                cmbplanning.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                cmbplanning.Enabled = true;
            }
        }
        #endregion

        protected void btnCargaArchivo_Click(object sender, EventArgs e)
        {
            if (this.Session["TipoCarga"].ToString().Trim() == "Carga Levantamiento Publicaciones")
            {
                if ((FileUpCMasivaMArca.PostedFile != null) && (FileUpCMasivaMArca.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpCMasivaMArca.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpCMasivaMArca.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();

                            DataTable dt = new DataTable();


                            FileUpCMasivaMArca.PostedFile.SaveAs(SaveLocation);

                            // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_LevantaPublicaciones"];
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            try
                            {
                                if (this.Session["scountry"].ToString() != null)
                                {
                                    oDa.Fill(oDs);
                                    dt = oDs.Tables[0];
                                    if (dt.Columns.Count == 9)
                                    {

                                        dt.Columns[0].ColumnName = "SKU";
                                        dt.Columns[1].ColumnName = "Familia";
                                        dt.Columns[2].ColumnName = "Promoción Puntual";
                                        dt.Columns[3].ColumnName = "PVP";
                                        dt.Columns[4].ColumnName = "Oferta";
                                        dt.Columns[5].ColumnName = "Inicio Actividad";
                                        dt.Columns[6].ColumnName = "Fin Actividad";
                                        dt.Columns[7].ColumnName = "Cadena";
                                        dt.Columns[8].ColumnName = "Tipo Publicación";
                             


                                        GvCargaArchivo.DataSource = dt;
                                        GvCargaArchivo.DataBind();

                                        for (int i = 0; i <= GvCargaArchivo.Rows.Count - 1; i++)
                                        {
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#186;", "o");

                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("  ", " ");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#186;", "o");

                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#186;", "o");



                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#186;", "o");


                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#186;", "o");


                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#186;", "o");


                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[6].Text = GvCargaArchivo.Rows[i].Cells[6].Text.Replace("&#186;", "o");

                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[7].Text = GvCargaArchivo.Rows[i].Cells[7].Text.Replace("&#186;", "o");

                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[8].Text = GvCargaArchivo.Rows[i].Cells[8].Text.Replace("&#186;", "o");


                                           
                               


                                        }

                                        bool sigue = true;
                                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                        {
                                            if (GvCargaArchivo.Rows[i].Cells[0].Text == "" && GvCargaArchivo.Rows[i].Cells[1].Text == "")
                                                if (this.Session["RecogerPor"].ToString().Trim() == "1")
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "Debe ingresar por SKU  para el levantamiento de información";
                                                    MensajeAlerta();

                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                            if (GvCargaArchivo.Rows[i].Cells[0].Text == "" && GvCargaArchivo.Rows[i].Cells[1].Text == "") 
                                                {
                                                    if (this.Session["RecogerPor"].ToString().Trim() == "2")
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "Debe ingresar por Familia para levantamiento de información";
                                                        MensajeAlerta();

                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                }
                                            if (GvCargaArchivo.Rows[i].Cells[0].Text != "" && GvCargaArchivo.Rows[i].Cells[1].Text != "")
                                            {
                                                string dato;
                                                dato = this.Session["RecogerPor"].ToString().Trim();
                                                if (this.Session["RecogerPor"].ToString().Trim() == "1")
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "El Levantamiento de Publicaciones para este caso se realiza por SKU  por favor Verifique";
                                                    MensajeAlerta();

                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                            }

                                            if (GvCargaArchivo.Rows[i].Cells[0].Text != "" && GvCargaArchivo.Rows[i].Cells[1].Text != "")
                                            {
                                                if (this.Session["RecogerPor"].ToString().Trim() == "2")
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "El Levantamiento de Publicaciones para este caso se realiza por Familia  por favor Verifique";
                                                    MensajeAlerta();

                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                            }
                                            DataSet oebranid = oLPublicacion.ObteneridsLevantamientoP(GvCargaArchivo.Rows[i].Cells[0].Text, GvCargaArchivo.Rows[i].Cells[1].Text, GvCargaArchivo.Rows[i].Cells[7].Text, GvCargaArchivo.Rows[i].Cells[8].Text);
                                            if (oebranid != null)
                                            {
                                                if (GvCargaArchivo.Rows[i].Cells[0].Text != "")
                                                {
                                                    if (oebranid.Tables[0].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "El SKU " + GvCargaArchivo.Rows[i].Cells[0].Text + ". No es válido ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[0].Text = oebranid.Tables[0].Rows[0][0].ToString().Trim();
                                                    }
                                                }
                                                if (GvCargaArchivo.Rows[i].Cells[1].Text != "")
                                                {
                                                    if (oebranid.Tables[1].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "La Familia " + GvCargaArchivo.Rows[i].Cells[1].Text + ". No es válida ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[1].Text = oebranid.Tables[1].Rows[0][0].ToString().Trim();
                                                    }
                                                }


                                                if (GvCargaArchivo.Rows[i].Cells[7].Text != "")
                                                {
                                                    if (oebranid.Tables[2].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "La Cadena " + GvCargaArchivo.Rows[i].Cells[7].Text + ". No es válida ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[7].Text = oebranid.Tables[2].Rows[0][0].ToString().Trim();
                                                    }
                                                }

                                                if (GvCargaArchivo.Rows[i].Cells[8].Text != "")
                                                {
                                                    if (oebranid.Tables[3].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "El tipo de Publicación " + GvCargaArchivo.Rows[i].Cells[8].Text + ". No es válida ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[8].Text = oebranid.Tables[3].Rows[0][0].ToString().Trim();
                                                    }
                                                }
                                            }
                                           

                                        }
                                        if (sigue)
                                        {
                                            for (int i = 0; i <= GvCargaArchivo.Rows.Count - 1; i++)
                                            {
                                                try
                                                {
                                                 
                                                    if (GvCargaArchivo.Rows[i].Cells[8].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[8].Text="0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[7].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[7].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[0].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[0].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[1].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[1].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[2].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[2].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[6].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[6].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[5].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[5].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[4].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[4].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[3].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[3].Text = "0";
                                                    }
                                                    EOPE_REPORTE_PUBLICACION oeLevantaP = oLPublicacion.RegistrarINFOLevantaPublicacion(Convert.ToInt32(this.Session["personid"]), this.Session["Planning"].ToString().Trim(), Convert.ToInt32(this.Session["companyid"]), Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[7].Text), GvCargaArchivo.Rows[i].Cells[1].Text, GvCargaArchivo.Rows[i].Cells[0].Text,
                                                     Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[8].Text), Convert.ToDateTime(GvCargaArchivo.Rows[i].Cells[5].Text), Convert.ToDateTime(GvCargaArchivo.Rows[i].Cells[6].Text), DateTime.Now, Convert.ToDateTime(this.Session["Fecha"].ToString().Trim()), Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now, true, GvCargaArchivo.Rows[i].Cells[2].Text,
                                                    Convert.ToDecimal(GvCargaArchivo.Rows[i].Cells[3].Text), Convert.ToDecimal(GvCargaArchivo.Rows[i].Cells[4].Text));

                                                    

                                                }


                                                catch (Exception ex)
                                                {
                                                    string error = "";
                                                    string mensaje = "";
                                                    error = Convert.ToString(ex.Message);
                                                    mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                                    if (error == mensaje)
                                                    {
                                                        Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                                        string errMessage = "";
                                                        errMessage = mensaje;
                                                        errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                                        this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                                    }
                                                    else
                                                    {
                                                        this.Session.Abandon();
                                                        Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                                    }
                                                }

                                            }
                                            Alertas.CssClass = "MensajesCorrecto";
                                            LblFaltantes.Text = GvCargaArchivo.Rows.Count + "  Registros fueron cargadas con exito" + this.Session["sBrand"] + ", provenientes del Archivo  " + FileUpCMasivaMArca.FileName.ToLower();
                                            MensajeAlerta();

                                        }
                                    }
                                    else
                                    {

                                        GvCargaArchivo.DataBind();
                                        Alertas.CssClass = "MensajesError";
                                        LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de asignación de puntos de venta válido. Por favor verifique la estructura que fue enviada a su correo.";
                                        MensajeAlerta();


                                        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                        correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                        correo.To.Add(this.Session["smail"].ToString());
                                        correo.Subject = "Errores en archivo de creación de Marcas";
                                        correo.IsBodyHtml = true;
                                        correo.Priority = System.Net.Mail.MailPriority.Normal;
                                        string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                        this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                   
                                             

                                            "El archivo que usted seleccionó para la carga de asignación de puntos de venta no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga  columnas" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                            "Columna 0  : SKU" + "<br/>" + 
                                            "Columna 1  : Familia" + "<br/>" + 
                                            "Columna 2  : Promoción Puntual" + "<br/>" + 
                                            "Columna 3  : PVP" + "<br/>" + 
                                            "Columna 4  : Oferta" + "<br/>" + 
                                            "Columna 5  : Inicio Actividad" + "<br/>" + 
                                            "Columna 6  : Fin Actividad" + "<br/>" + 
                                            "Columna 7  : Cadena" + "<br/>" + 
                                            "Columna 8  : Tipo Publicación" + "<br/>" +                                     
                                            "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizar los nombres de las columnas del archivo ." +
                                            "Pero tenga en cuenta que debe ingresar la información de las Marcas en ese orden." + "<br/>" + "<br/>" + "<br/>" +
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
                                else
                                {
                                    Alertas.CssClass = "MensajesError";
                                    LblFaltantes.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                                    MensajeAlerta();

                                }
                            }
                            catch (Exception ex)
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Levantamiento Publicaciones válido. Por favor verifique que el nombre de la hoja donde estan los datos sea Marcas";
                                MensajeAlerta();

                            }
                            oConn1.Close();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            MensajeAlerta();

                        }
                    }
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Es indispensable seleccionar un archivo.";
                    MensajeAlerta();

                }


            }

          
            
            ///carga masiva Levantamiento Exhibición e impulso
            ///mayo 9 del 2011 Magaly Jiménez
            

            if (this.Session["TipoCarga"].ToString().Trim() == "Carga Levantamiento ExhibicImpulso")
            {
                if ((FileUpCMasivaMArca.PostedFile != null) && (FileUpCMasivaMArca.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpCMasivaMArca.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpCMasivaMArca.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();

                            DataTable dt = new DataTable();


                            FileUpCMasivaMArca.PostedFile.SaveAs(SaveLocation);

                            // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_LevantaExhibiImpulso"];
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            try
                            {
                                if (this.Session["scountry"].ToString() != null)
                                {
                                    oDa.Fill(oDs);
                                    dt = oDs.Tables[0];
                                    if (dt.Columns.Count == 6)
                                    {

                                        dt.Columns[0].ColumnName = "SKU";
                                        dt.Columns[1].ColumnName = "Familia";
                                        dt.Columns[2].ColumnName = "Inicio Actividad";
                                        dt.Columns[3].ColumnName = "Fin Actividad";
                                        dt.Columns[4].ColumnName = "PDV";
                                        dt.Columns[5].ColumnName = "Tipo Actividad";



                                        GvCargaArchivo.DataSource = dt;
                                        GvCargaArchivo.DataBind();

                                        for (int i = 0; i <= GvCargaArchivo.Rows.Count - 1; i++)
                                        {
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#186;", "o");

                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("  ", " ");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#186;", "o");

                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#186;", "o");



                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#186;", "o");


                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#186;", "o");


                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#186;", "o");


                                            

                                        }

                                        bool sigue = true;
                                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                        {
                                            if (GvCargaArchivo.Rows[i].Cells[0].Text == "" && GvCargaArchivo.Rows[i].Cells[1].Text == "")
                                                if (this.Session["RecogerPor"].ToString().Trim() == "1")
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "Debe ingresar por SKU  para el levantamiento de información";
                                                    MensajeAlerta();

                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                            if (GvCargaArchivo.Rows[i].Cells[0].Text == "" && GvCargaArchivo.Rows[i].Cells[1].Text == "")
                                            {
                                                if (this.Session["RecogerPor"].ToString().Trim() == "2")
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "Debe ingresar por Familia para levantamiento de información";
                                                    MensajeAlerta();

                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                            }
                                            if (GvCargaArchivo.Rows[i].Cells[0].Text != "" && GvCargaArchivo.Rows[i].Cells[1].Text != "")
                                            {
                                                string dato;
                                                dato = this.Session["RecogerPor"].ToString().Trim();
                                                if (this.Session["RecogerPor"].ToString().Trim() == "1")
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "El Levantamiento de Exhibición e impulso para este caso se realiza por SKU  por favor Verifique";
                                                    MensajeAlerta();

                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                            }

                                            if (GvCargaArchivo.Rows[i].Cells[0].Text != "" && GvCargaArchivo.Rows[i].Cells[1].Text != "")
                                            {
                                                if (this.Session["RecogerPor"].ToString().Trim() == "2")
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "El Levantamiento de Exhibición e impulso para este caso se realiza por Familia  por favor Verifique";
                                                    MensajeAlerta();

                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                            }
                                            DataSet oebranid = olExhibiImpulso.ObteneridsLevantamientoEI(GvCargaArchivo.Rows[i].Cells[0].Text, GvCargaArchivo.Rows[i].Cells[1].Text, GvCargaArchivo.Rows[i].Cells[4].Text, GvCargaArchivo.Rows[i].Cells[5].Text);
                                            if (oebranid != null)
                                            {
                                                if (GvCargaArchivo.Rows[i].Cells[0].Text != "")
                                                {
                                                    if (oebranid.Tables[0].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "El SKU " + GvCargaArchivo.Rows[i].Cells[0].Text + ". No es válido ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[0].Text = oebranid.Tables[0].Rows[0][0].ToString().Trim();
                                                    }
                                                }
                                                if (GvCargaArchivo.Rows[i].Cells[1].Text != "")
                                                {
                                                    if (oebranid.Tables[1].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "La Familia " + GvCargaArchivo.Rows[i].Cells[1].Text + ". No es válida ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[1].Text = oebranid.Tables[1].Rows[0][0].ToString().Trim();
                                                    }
                                                }


                                                if (GvCargaArchivo.Rows[i].Cells[4].Text != "")
                                                {
                                                    if (oebranid.Tables[2].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "El PDV " + GvCargaArchivo.Rows[i].Cells[4].Text + ". No es válida ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[4].Text = oebranid.Tables[2].Rows[0][0].ToString().Trim();
                                                    }
                                                }

                                                if (GvCargaArchivo.Rows[i].Cells[5].Text != "")
                                                {
                                                    if (oebranid.Tables[3].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "El tipo de Publicación " + GvCargaArchivo.Rows[i].Cells[5].Text + ". No es válida ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[5].Text = oebranid.Tables[3].Rows[0][0].ToString().Trim();
                                                    }
                                                }
                                            }


                                        }
                                        if (sigue)
                                        {
                                            for (int i = 0; i <= GvCargaArchivo.Rows.Count - 1; i++)
                                            {
                                                try
                                                {

                                                                                                       
                                                    if (GvCargaArchivo.Rows[i].Cells[0].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[0].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[1].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[1].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[2].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[2].Text = "0";
                                                    }                                                  
                                                    if (GvCargaArchivo.Rows[i].Cells[5].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[5].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[4].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[4].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[3].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[3].Text = "0";
                                                    }
                                                  
                                                    EOPE_REPORTE_EXHB_IMPULSO oeLevanExhiImpulso = olExhibiImpulso.RegistrarINFOLevantaexhiimpulso(Convert.ToInt32(this.Session["personid"]), this.Session["Planning"].ToString().Trim(), Convert.ToInt32(this.Session["companyid"]), GvCargaArchivo.Rows[i].Cells[4].Text, GvCargaArchivo.Rows[i].Cells[1].Text, GvCargaArchivo.Rows[i].Cells[0].Text,
                                                    GvCargaArchivo.Rows[i].Cells[5].Text, Convert.ToDateTime(GvCargaArchivo.Rows[i].Cells[2].Text), Convert.ToDateTime(GvCargaArchivo.Rows[i].Cells[3].Text), DateTime.Now, Convert.ToDateTime(this.Session["Fecha"].ToString().Trim()), Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now, true);

                                                }


                                                catch (Exception ex)
                                                {
                                                    string error = "";
                                                    string mensaje = "";
                                                    error = Convert.ToString(ex.Message);
                                                    mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                                    if (error == mensaje)
                                                    {
                                                        Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                                        string errMessage = "";
                                                        errMessage = mensaje;
                                                        errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                                        this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                                    }
                                                    else
                                                    {
                                                        this.Session.Abandon();
                                                        Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                                    }
                                                }

                                            }
                                            Alertas.CssClass = "MensajesCorrecto";
                                            LblFaltantes.Text = GvCargaArchivo.Rows.Count + "  Registros fueron cargadas con exito" + this.Session["sBrand"] + ", provenientes del Archivo  " + FileUpCMasivaMArca.FileName.ToLower();
                                            MensajeAlerta();

                                        }
                                    }
                                    else
                                    {

                                        GvCargaArchivo.DataBind();
                                        Alertas.CssClass = "MensajesError";
                                        LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de asignación Levantamiento de Exhibición de Impulso válido. Por favor verifique la estructura que fue enviada a su correo.";
                                        MensajeAlerta();


                                        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                        correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                        correo.To.Add(this.Session["smail"].ToString());
                                        correo.Subject = "Errores en archivo de creación de Marcas";
                                        correo.IsBodyHtml = true;
                                        correo.Priority = System.Net.Mail.MailPriority.Normal;
                                        string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                            this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                   
                                             

                                            "El archivo que usted seleccionó para la carga de asignación de puntos de venta no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga  columnas" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                            "Columna 1  : SKU" + "<br/>" +
                                            "Columna 2  : Familia"+ "<br/>" +
                                            "Columna 3  : Inicio Actividad" + "<br/>" +     
                                            "Columna 4  : Fin Actividad" + "<br/>" +   
                                            "Columna 5  : PDV" + "<br/>" +  
                                            "Columna 6  : Tipo Actividad" + "<br/>" +   
                                            "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizar los nombres de las columnas del archivo ." +
                                            "Pero tenga en cuenta que debe ingresar la información de las Marcas en ese orden." + "<br/>" + "<br/>" + "<br/>" +
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
                                else
                                {
                                    Alertas.CssClass = "MensajesError";
                                    LblFaltantes.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                                    MensajeAlerta();

                                }
                            }
                            catch (Exception ex)
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Marcas válido. Por favor verifique que el nombre de la hoja donde estan los datos sea ExhibiImpulso";
                                MensajeAlerta();

                            }
                            oConn1.Close();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            MensajeAlerta();

                        }
                    }
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Es indispensable seleccionar un archivo.";
                    MensajeAlerta();

                }


            }



            ///carga Masiva de Levantamiento Material POP
            ///Mayo
            ///


            if (this.Session["TipoCarga"].ToString().Trim() == "Carga Levantamiento MaterialPOP")
            {
                if ((FileUpCMasivaMArca.PostedFile != null) && (FileUpCMasivaMArca.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpCMasivaMArca.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpCMasivaMArca.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();

                            DataTable dt = new DataTable();


                            FileUpCMasivaMArca.PostedFile.SaveAs(SaveLocation);

                            // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_LevantaMaterialPOP"];
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            try
                            {
                                if (this.Session["scountry"].ToString() != null)
                                {
                                    oDa.Fill(oDs);
                                    dt = oDs.Tables[0];
                                    if (dt.Columns.Count == 6)
                                    {

                                        dt.Columns[0].ColumnName = "Marca";
                                        dt.Columns[1].ColumnName = "promoción";
                                        dt.Columns[2].ColumnName = "Tipo POP";
                                        dt.Columns[3].ColumnName = "Inicio Actividad";
                                        dt.Columns[4].ColumnName = "Fin Actividad";
                                        dt.Columns[5].ColumnName = "PDV";
                             



                                        GvCargaArchivo.DataSource = dt;
                                        GvCargaArchivo.DataBind();

                                        for (int i = 0; i <= GvCargaArchivo.Rows.Count - 1; i++)
                                        {
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[0].Text = GvCargaArchivo.Rows[i].Cells[0].Text.Replace("&#186;", "o");

                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("  ", " ");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[1].Text = GvCargaArchivo.Rows[i].Cells[1].Text.Replace("&#186;", "o");

                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[2].Text = GvCargaArchivo.Rows[i].Cells[2].Text.Replace("&#186;", "o");



                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[3].Text = GvCargaArchivo.Rows[i].Cells[3].Text.Replace("&#186;", "o");


                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[4].Text = GvCargaArchivo.Rows[i].Cells[4].Text.Replace("&#186;", "o");


                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&nbsp;", "");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#160;", "");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#193;", "Á");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#201;", "É");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#205;", "Í");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#211;", "Ó");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#218;", "Ú");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#225;", "á");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#233;", "é");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#237;", "í");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#243;", "ó");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#250;", "ú");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#209;", "Ñ");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#241;", "ñ");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&amp;", "&");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#176;", "o");
                                            GvCargaArchivo.Rows[i].Cells[5].Text = GvCargaArchivo.Rows[i].Cells[5].Text.Replace("&#186;", "o");

                                          


                                        }

                                        bool sigue = true;
                                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                        {
                                            if (GvCargaArchivo.Rows[i].Cells[0].Text == "")
                                            {
                                                Alertas.CssClass = "MensajesError";
                                                LblFaltantes.Text = "Debe ingresar por lo menos Marca  para el levantamiento de información";
                                                MensajeAlerta();

                                                sigue = false;
                                                i = dt.Rows.Count - 1;


                                            }


                                             DataSet oebranid = olMaterialPOP.ObteneridsLevantamientoMaterialPOP(GvCargaArchivo.Rows[i].Cells[0].Text, GvCargaArchivo.Rows[i].Cells[5].Text, GvCargaArchivo.Rows[i].Cells[1].Text, GvCargaArchivo.Rows[i].Cells[2].Text);
                                            if (oebranid != null)
                                            {
                                                if (GvCargaArchivo.Rows[i].Cells[0].Text != "")
                                                {
                                                    if (oebranid.Tables[0].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "La Marca " + GvCargaArchivo.Rows[i].Cells[0].Text + ". No es válido ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[0].Text = oebranid.Tables[0].Rows[0][0].ToString().Trim();
                                                    }
                                                }
                                                if (GvCargaArchivo.Rows[i].Cells[1].Text != "")
                                                {
                                                    if (oebranid.Tables[1].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "La Promocion " + GvCargaArchivo.Rows[i].Cells[1].Text + ". No es válida ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[1].Text = oebranid.Tables[1].Rows[0][0].ToString().Trim();
                                                    }
                                                }


                                                if (GvCargaArchivo.Rows[i].Cells[2].Text != "")
                                                {
                                                    if (oebranid.Tables[2].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "El Tipo POP " + GvCargaArchivo.Rows[i].Cells[2].Text + ". No es válida ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[2].Text = oebranid.Tables[2].Rows[0][0].ToString().Trim();
                                                    }
                                                }

                                                if (GvCargaArchivo.Rows[i].Cells[5].Text != "")
                                                {
                                                    if (oebranid.Tables[3].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "El PDV " + GvCargaArchivo.Rows[i].Cells[5].Text + ". No es válida ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[5].Text = oebranid.Tables[3].Rows[0][0].ToString().Trim();
                                                    }
                                                }
                                            }


                                        }
                                        if (sigue)
                                        {
                                            for (int i = 0; i <= GvCargaArchivo.Rows.Count - 1; i++)
                                            {
                                                try
                                                {

                                                    if (GvCargaArchivo.Rows[i].Cells[1].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[1].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[2].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[2].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[3].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[3].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[4].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[4].Text = "0";
                                                    }
                                                    if (GvCargaArchivo.Rows[i].Cells[5].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[5].Text = "0";
                                                    }
                                               
                                                    

                                                    EOPE_REPORTE_MATERIAL_POP oeLevanPublicaciones = olMaterialPOP.RegistrarINFOLevantaMaterialPOP(Convert.ToInt32(this.Session["personid"]), this.Session["Planning"].ToString().Trim(), Convert.ToInt32(this.Session["companyid"]), GvCargaArchivo.Rows[i].Cells[5].Text, Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[0].Text), Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[2].Text),
                                                    GvCargaArchivo.Rows[i].Cells[1].Text, Convert.ToDateTime(GvCargaArchivo.Rows[i].Cells[3].Text), Convert.ToDateTime(GvCargaArchivo.Rows[i].Cells[4].Text), DateTime.Now, Convert.ToDateTime(this.Session["Fecha"].ToString().Trim()), Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now, true);

                                                }


                                                catch (Exception ex)
                                                {
                                                    string error = "";
                                                    string mensaje = "";
                                                    error = Convert.ToString(ex.Message);
                                                    mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                                    if (error == mensaje)
                                                    {
                                                        Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                                        string errMessage = "";
                                                        errMessage = mensaje;
                                                        errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                                        this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                                    }
                                                    else
                                                    {
                                                        this.Session.Abandon();
                                                        Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                                    }
                                                }

                                            }
                                            Alertas.CssClass = "MensajesCorrecto";
                                            LblFaltantes.Text = GvCargaArchivo.Rows.Count + "  Registros fueron cargadas con exito" + this.Session["sBrand"] + ", provenientes del Archivo  " + FileUpCMasivaMArca.FileName.ToLower();
                                            MensajeAlerta();

                                        }
                                    }
                                    else
                                    {

                                        GvCargaArchivo.DataBind();
                                        Alertas.CssClass = "MensajesError";
                                        LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de asignación de Levantamiento de Material POP no es  válido. Por favor verifique la estructura que fue enviada a su correo.";
                                        MensajeAlerta();


                                        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                        correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                        correo.To.Add(this.Session["smail"].ToString());
                                        correo.Subject = "Errores en archivo de creación de Marcas";
                                        correo.IsBodyHtml = true;
                                        correo.Priority = System.Net.Mail.MailPriority.Normal;
                                        string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                        this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                   
                                             

                                            "El archivo que usted seleccionó para la carga de asignación de puntos de venta no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga  columnas" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                            "Columna 1  : Marca" + "<br/>" +
                                            "Columna 2  : Promoción"+ "<br/>" +
                                            "Columna 3  : Tipo POP" + "<br/>" +  
                                            "Columna 4  : Inicio Actividad" + "<br/>" + 
                                            "Columna 3  : Fin Actividad" + "<br/>" + 
                                            "Columna 3  : PDV" + "<br/>" + 
                                            "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizar los nombres de las columnas del archivo ." +
                                            "Pero tenga en cuenta que debe ingresar la información de las Marcas en ese orden." + "<br/>" + "<br/>" + "<br/>" +
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
                                else
                                {
                                    Alertas.CssClass = "MensajesError";
                                    LblFaltantes.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                                    MensajeAlerta();

                                }
                            }
                            catch (Exception ex)
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Marcas válido. Por favor verifique que el nombre de la hoja donde estan los datos sea Marcas";
                                MensajeAlerta();

                            }
                            oConn1.Close();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            MensajeAlerta();

                        }
                    }
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Es indispensable seleccionar un archivo.";
                    MensajeAlerta();

                }


            }

        


        }
    }
}