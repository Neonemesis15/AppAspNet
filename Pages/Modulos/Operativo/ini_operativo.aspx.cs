using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Text;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Application.Interfaces;
using Lucky.Business;
using Lucky.Business.Common.Application;
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;
using Lucky.CFG.Tools;




namespace SIGE.Pages.Modulos.Operativo
{
    //-- =============================================
    //-- Author:		<Ing. Mauricio Ortiz>
    //-- Create date:   <02/09/2009>
    //-- Description:	<Permite operar los requerimientos del módulo operativo>
    //-- Requerimiento No. <>
    //-- =============================================

    public partial class ini_operativo : System.Web.UI.Page
    {
        #region Zona de Declaración de Variables Generales
        private Competition__Information oCompetition__Information = new Competition__Information();
        Conexion oConn = new Lucky.Data.Conexion();
        AlmacenDetalle_Planning AlmacenDetalle_Planning = new AlmacenDetalle_Planning();
        Facade_Proceso_Operativo.Facade_Proceso_Operativo Get_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Facade_Proceso_Planning.Facade_Proceso_Planning Get_Planning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        DateTime FechaEjec;
        DateTime cinfo_FeciniPromo;
        DateTime cinfo_FecfinPromo;
        DateTime FecOperaDigita;
        string svigencia;
        bool bStatusActCom;
        bool bStatusActPropia;
        string cinfo_CodStrategy;
        string cinfo_Cod_Channel;
        string cinfo_id_ProductCategory;
        string dupFechaEjec;
        string dupnameCompany;
        string dupName_Activity;
        string Query;
        string personalExel;
        string PDVVsPersonalExcel;
        string ProductoXPDVVsPersonalExcel;
        string MarcasXPDVVsPersonalExcel;
        string FormatoEncExcel;
        string FormatoPieExcel;
        int conteo;
        #endregion

        #region Funciones comunes
        //Limpiar controles
        private void limpiarControles(Control parent)
        {
            TextBox t;
            DropDownList d;

            foreach (Control Txt in parent.Controls)
            {
                t = Txt as TextBox;
                if (t != null)
                {
                    t.Text = "";
                }
                if (Txt.Controls.Count > 0)
                {
                    limpiarControles(Txt);
                }
            }
            foreach (Control Cmb in parent.Controls)
            {
                d = Cmb as DropDownList;
                if (d != null)
                {
                    try
                    {
                        d.Text = "0";
                    }
                    catch
                    {
                    }
                }
                if (Cmb.Controls.Count > 0)
                {
                    limpiarControles(Cmb);
                }
            }
        }
        private void ActivarControles(Control parent)
        {
            TextBox t;
            DropDownList d;

            foreach (Control Txt in parent.Controls)
            {
                t = Txt as TextBox;
                if (t != null)
                {
                    t.Enabled = true;
                }
                if (Txt.Controls.Count > 0)
                {
                    ActivarControles(Txt);
                }
            }
            foreach (Control Cmb in parent.Controls)
            {
                d = Cmb as DropDownList;
                if (d != null)
                {
                    try
                    {
                        d.Enabled = true;
                    }
                    catch
                    {
                    }
                }
                if (Cmb.Controls.Count > 0)
                {
                    ActivarControles(Cmb);
                }
            }

            ChkCinfo_id_MPointOfPurchase.Enabled = true;
            RBtncinfo_Vigente.Enabled = true;
            ImgCalendar.Enabled = true;
            ImgCalendarFI.Enabled = true;
            ImgCalendarFF.Enabled = true;
            ImgBtnFecOperaDigita.Enabled = true;



        }
        private void InActivarControles(Control parent)
        {
            ChkCinfo_id_MPointOfPurchase.Enabled = false;
            RBtncinfo_Vigente.Enabled = false;
            RbtnStatusActCom.Enabled = false;
            RbtnStatusActPropia.Enabled = false;
            ImgCalendar.Enabled = false;
            ImgCalendarFI.Enabled = false;
            ImgCalendarFF.Enabled = false;
            ImgBtnFecOperaDigita.Enabled = false;
            TxtMotivo.Enabled = true;
            TxtMensaje.Enabled = true;

            //TextBox t;
            //DropDownList d;

            //foreach (Control Txt in parent.Controls)
            //{
            //    t = Txt as TextBox;
            //    if (t != null)
            //    {
            //        t.Enabled = false;
            //    }
            //    if (Txt.Controls.Count > 0)
            //    {
            //        InActivarControles(Txt);
            //    }
            //}
            //foreach (Control Cmb in parent.Controls)
            //{
            //    d = Cmb as DropDownList;
            //    if (d != null)
            //    {
            //        try
            //        {
            //            d.Enabled = false;
            //        }
            //        catch
            //        {
            //        }
            //    }
            //    if (Cmb.Controls.Count > 0)
            //    {
            //        InActivarControles(Cmb);
            //    }
            //}

        }

        //Llenar objetos
        private void llena_planning()
        {
            DataTable dt = new DataTable();
            dt = Get_Operativo.Get_Planning();
            //Se alimenta la grilla del menú Asignaciones pendientes            
            GVPlanning.DataSource = dt;
            GVPlanning.DataBind();

            //Se alimenta el combo del menú Impresión formatos
            CmbPlanningForm.DataSource = dt;
            CmbPlanningForm.DataValueField = "Código";
            CmbPlanningForm.DataTextField = "Planning";
            CmbPlanningForm.DataBind();
            if (dt.Rows.Count == 0)
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Resultado de la consulta";
                LblFaltantes.Text = "Sr. Usuario, la consulta realizada no arrojó ninguna respuesta";
                PopupMensajes();
            }
            dt = null;
        }
        private void llena_FormatosPropios()
        {
            //Se alimenta la grilla del menú Impresión formatos
            DataTable dt = new DataTable();
            dt = Get_Operativo.Get_FormatosPlanning(Convert.ToInt32(CmbPlanningForm.Text));
            GVFormaPropio.DataSource = dt;
            GVFormaPropio.DataBind();

            dt = null;

            //se alimenta checkboxlist de reportes a digitar
            DataTable dt1 = oConn.ejecutarDataTable("UP_WEBSIGE_OPERATIVO_OBTENERREPORTESPLANNING", Convert.ToInt32(CmbPlanningForm.Text));
            ChkTipDigitacion.DataSource = dt1;
            ChkTipDigitacion.DataTextField = "Report_nameReport";
            ChkTipDigitacion.DataValueField = "report_id";
            ChkTipDigitacion.DataBind();
            dt1 = null;
        }
        private void llena_StaffPlanning()
        {
            DataTable dt = new DataTable();
            dt = Get_Operativo.Get_StaffPlanning(Convert.ToInt32(CmbPlanningForm.Text));
            //Se alimenta el listbox del personal operativo asignado al planning

            LstPlanningOp.DataSource = dt;
            LstPlanningOp.DataTextField = "Nombre";
            LstPlanningOp.DataValueField = "Person_id";
            LstPlanningOp.DataBind();
            dt = null;

        }
        private void llena_FormatoActCom()
        {
            //Se alimenta la grilla de formatos de activiades en el comercio del planning seleccionado
            DataTable dt = new DataTable();
            dt = Get_Operativo.Get_ActivComercio(Convert.ToInt32(CmbPlanningForm.Text));
            GVFormaCompetencia.DataSource = dt;
            GVFormaCompetencia.DataBind();
            dt = null;


        }
        private void llena_Servicios()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOSERVICIOSCONPLANNING", Convert.ToInt32(GVPlanning.SelectedRow.Cells[1].Text));
            //se llena combo servicios
            Cmbcinfo_CodStrategy.DataSource = ds;
            Cmbcinfo_CodStrategy.DataTextField = "Strategy_Name";
            Cmbcinfo_CodStrategy.DataValueField = "cod_Strategy";
            Cmbcinfo_CodStrategy.DataBind();
            ds = null;
        }
        private void llena_POP()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 52);
            //se llena checkboxlist material POP
            ChkCinfo_id_MPointOfPurchase.DataSource = ds;
            ChkCinfo_id_MPointOfPurchase.DataTextField = "POP_name";
            ChkCinfo_id_MPointOfPurchase.DataValueField = "id_MPointOfPurchase";
            ChkCinfo_id_MPointOfPurchase.DataBind();
            ds = null;
        }
        private void llena_Channel()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 53);
            //se llena combo Canales (GRUPOS OBJETIVOS)
            Cmbcinfo_Cod_Channel.DataSource = ds;
            Cmbcinfo_Cod_Channel.DataTextField = "TargetGroup";
            Cmbcinfo_Cod_Channel.DataValueField = "id_TargetGroup";
            Cmbcinfo_Cod_Channel.DataBind();
            ds = null;
        }
        private void llena_Category()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 13);
            //se llena combo Categorias
            Cmbcinfo_id_ProductCategory.DataSource = ds;
            Cmbcinfo_id_ProductCategory.DataTextField = "Product_Category";
            Cmbcinfo_id_ProductCategory.DataValueField = "id_ProductCategory";
            Cmbcinfo_id_ProductCategory.DataBind();
            ds = null;
        }
        private void llena_PDV()
        {
            DataTable dt = Get_Operativo.Get_PdvXOperativoSel(Convert.ToInt32(CmbOperaDigita.SelectedValue));
            CmbPdvDigita.DataSource = dt;
            CmbPdvDigita.DataValueField = "id_MPOSPlanning";
            CmbPdvDigita.DataTextField = "pdv_Name";
            CmbPdvDigita.DataBind();
            CmbPdvDigita.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            dt = null;
        }
        private void llena_Products()
        {
            DataTable dt = Get_Operativo.Get_ProductosXPdvXOperativoSel(Convert.ToInt32(CmbOperaDigita.SelectedValue));
            CmbProdDigita.DataSource = dt;
            CmbProdDigita.DataValueField = "id_ProductsPlanning";
            CmbProdDigita.DataTextField = "Product_Name";
            CmbProdDigita.DataBind();
            CmbProdDigita.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            dt = null;

        }
        private void llena_ProductsCompe()
        {
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_OPERATIVO_PRODUCTOCOMPETIDORXPRODUCTOPROPIOSEL", Convert.ToInt32(CmbProdDigita.Text));
            this.Session["prodcompetenciadigitar"] = dt;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    DataTable dtmar = oConn.ejecutarDataTable("UP_WEBSIGE_OPERATIVO_MARCAPRODUCTOCOMPETIDORXPRODUCTOCOMPEXOPESEL", Convert.ToInt32(dt.Rows[i][0]).ToString().Trim());
                    if (i == 0)
                    {
                        lblTxtMarcaProdCompeSel.Text = dtmar.Rows[0]["Marca Competidor"].ToString().Trim();
                    }
                    if (i == 1)
                    {
                        lblTxtMarcaProdCompeSel1.Text = dtmar.Rows[0]["Marca Competidor"].ToString().Trim();
                    }
                    if (i == 2)
                    {
                        lblTxtMarcaProdCompeSel2.Text = dtmar.Rows[0]["Marca Competidor"].ToString().Trim();
                    }
                }
            }
            GvVentasCompe1.DataBind();
            GvVentasCompe2.DataBind();
            GvVentasCompe3.DataBind();
            GvPreciosCompe1.DataBind();
            GvPreciosCompe2.DataBind();
            GvPreciosCompe3.DataBind();
            GvCoberturaCompe1.DataBind();
            GvCoberturaCompe2.DataBind();
            GvCoberturaCompe3.DataBind();
            GvSODCompe1.DataBind();
            GvSODCompe2.DataBind();
            GvSODCompe3.DataBind();
            DataTable dt2 = oConn.ejecutarDataTable("UP_WEBSIGE_OPERATIVO_MARCAPRODUCTOSXPRODUCTOXOPESEL", Convert.ToInt32(CmbProdDigita.SelectedValue), Convert.ToInt32(CmbOperaDigita.Text));
            if (dt2.Rows.Count > 0)
            {
                lblTxtMarcaProdSel.Text = dt2.Rows[0]["Name_Brand"].ToString().Trim();
                GvDatosIndicadorSOD.Caption = dt2.Rows[0]["Name_Brand"].ToString().Trim();
            }
            dt2 = null;

            for (int formato = 0; formato <= GVFormaPropio.Rows.Count - 1; formato++)
            {

                DataTable dt1 = Get_Operativo.Get_SearchContenidoFormatosPropiosIndicador(Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text));


                if (Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text) == 17)
                {
                    for (int y = 0; y <= dt.Rows.Count - 1; y++)
                    {
                        if (y == 0)
                        {
                            GvVentasCompe1.DataSource = dt1;
                            GvVentasCompe1.DataBind();
                            GvVentasCompe1.Caption = dt.Rows[y][1].ToString().Trim();
                            GvVentasCompe1.CaptionAlign = TableCaptionAlign.Left;
                            this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text + y] = dt1;
                            for (int j = 0; j <= GvVentasCompe1.Rows.Count - 1; j++)
                            {
                                ((Label)GvVentasCompe1.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                            }
                        }
                        if (y == 1)
                        {
                            GvVentasCompe2.DataSource = dt1;
                            GvVentasCompe2.DataBind();
                            GvVentasCompe2.Caption = dt.Rows[y][1].ToString().Trim();
                            GvVentasCompe2.CaptionAlign = TableCaptionAlign.Left;
                            this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text + y] = dt1;
                            for (int j = 0; j <= GvVentasCompe2.Rows.Count - 1; j++)
                            {
                                ((Label)GvVentasCompe2.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                            }
                        }
                        if (y == 2)
                        {
                            GvVentasCompe3.DataSource = dt1;
                            GvVentasCompe3.DataBind();
                            GvVentasCompe3.Caption = dt.Rows[y][1].ToString().Trim();
                            GvVentasCompe3.CaptionAlign = TableCaptionAlign.Left;
                            this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text + y] = dt1;
                            for (int j = 0; j <= GvVentasCompe3.Rows.Count - 1; j++)
                            {
                                ((Label)GvVentasCompe3.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                            }
                        }
                    }
                }
                if (Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text) == 19)
                {
                    for (int y = 0; y <= dt.Rows.Count - 1; y++)
                    {
                        if (y == 0)
                        {

                            GvPreciosCompe1.DataSource = dt1;
                            GvPreciosCompe1.DataBind();
                            GvPreciosCompe1.Caption = dt.Rows[y][1].ToString().Trim();
                            GvPreciosCompe1.CaptionAlign = TableCaptionAlign.Left;
                            this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text + y] = dt1;
                            for (int j = 0; j <= GvPreciosCompe1.Rows.Count - 1; j++)
                            {
                                ((Label)GvPreciosCompe1.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                            }
                        }
                        if (y == 1)
                        {
                            GvPreciosCompe2.DataSource = dt1;
                            GvPreciosCompe2.DataBind();
                            GvPreciosCompe2.Caption = dt.Rows[y][1].ToString().Trim();
                            GvPreciosCompe2.CaptionAlign = TableCaptionAlign.Left;
                            this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text + y] = dt1;
                            for (int j = 0; j <= GvPreciosCompe2.Rows.Count - 1; j++)
                            {
                                ((Label)GvPreciosCompe2.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                            }
                        }
                        if (y == 2)
                        {
                            GvPreciosCompe3.DataSource = dt1;
                            GvPreciosCompe3.DataBind();
                            GvPreciosCompe3.Caption = dt.Rows[y][1].ToString().Trim();
                            GvPreciosCompe3.CaptionAlign = TableCaptionAlign.Left;
                            this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text + y] = dt1;
                            for (int j = 0; j <= GvPreciosCompe3.Rows.Count - 1; j++)
                            {
                                ((Label)GvPreciosCompe3.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                            }
                        }
                    }
                }
                if (Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text) == 22)
                {
                    for (int y = 0; y <= dt.Rows.Count - 1; y++)
                    {
                        if (y == 0)
                        {
                            GvCoberturaCompe1.DataSource = dt1;
                            GvCoberturaCompe1.DataBind();
                            GvCoberturaCompe1.Caption = dt.Rows[y][1].ToString().Trim();
                            GvCoberturaCompe1.CaptionAlign = TableCaptionAlign.Left;
                            this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text + y] = dt1;
                            for (int j = 0; j <= GvCoberturaCompe1.Rows.Count - 1; j++)
                            {
                                ((Label)GvCoberturaCompe1.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                            }
                        }
                        if (y == 1)
                        {
                            GvCoberturaCompe2.DataSource = dt1;
                            GvCoberturaCompe2.DataBind();
                            GvCoberturaCompe2.Caption = dt.Rows[y][1].ToString().Trim();
                            GvCoberturaCompe2.CaptionAlign = TableCaptionAlign.Left;
                            this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text + y] = dt1;
                            for (int j = 0; j <= GvCoberturaCompe2.Rows.Count - 1; j++)
                            {
                                ((Label)GvCoberturaCompe2.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                            }
                        }
                        if (y == 2)
                        {
                            GvCoberturaCompe3.DataSource = dt1;
                            GvCoberturaCompe3.DataBind();
                            GvCoberturaCompe3.Caption = dt.Rows[y][1].ToString().Trim();
                            GvCoberturaCompe3.CaptionAlign = TableCaptionAlign.Left;
                            this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text + y] = dt1;
                            for (int j = 0; j <= GvCoberturaCompe3.Rows.Count - 1; j++)
                            {
                                ((Label)GvCoberturaCompe3.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                            }
                        }
                    }
                }
                if (Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text) == 21)
                {
                    for (int y = 0; y <= dt.Rows.Count - 1; y++)
                    {
                        if (y == 0)
                        {
                            GvSODCompe1.DataSource = dt1;
                            GvSODCompe1.DataBind();
                            GvSODCompe1.Caption = dt.Rows[y][2].ToString().Trim();
                            GvSODCompe1.CaptionAlign = TableCaptionAlign.Left;
                            this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text + y] = dt1;
                            for (int j = 0; j <= GvSODCompe1.Rows.Count - 1; j++)
                            {
                                ((Label)GvSODCompe1.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                            }
                        }
                        if (y == 1)
                        {
                            GvSODCompe2.DataSource = dt1;
                            GvSODCompe2.DataBind();
                            GvSODCompe2.Caption = dt.Rows[y][2].ToString().Trim();
                            GvSODCompe2.CaptionAlign = TableCaptionAlign.Left;
                            this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text + y] = dt1;
                            for (int j = 0; j <= GvSODCompe2.Rows.Count - 1; j++)
                            {
                                ((Label)GvSODCompe2.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                            }
                        }
                        if (y == 2)
                        {
                            GvSODCompe3.DataSource = dt1;
                            GvSODCompe3.DataBind();
                            GvSODCompe3.Caption = dt.Rows[y][2].ToString().Trim();
                            GvSODCompe3.CaptionAlign = TableCaptionAlign.Left;
                            this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text + y] = dt1;
                            for (int j = 0; j <= GvSODCompe3.Rows.Count - 1; j++)
                            {
                                ((Label)GvSODCompe3.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                            }
                        }
                    }
                }
            }
        }
        private void llena_ConsultarACTCom()
        {
            DataTable dt = Get_Operativo.Get_SearchCompetition_Information(Convert.ToInt32(this.Session["id_Planning"].ToString().Trim()));
            GVConsultaActCom.DataSource = dt;
            GVConsultaActCom.DataBind();
        }
        private void llena_ConsultarACTLevPropio()
        {
            DataTable dt = Get_Operativo.Get_SearchActivLevantPropio(Convert.ToInt32(this.Session["id_Planning"].ToString().Trim()));
            GVConsultaActLevPropia.DataSource = dt;
            GVConsultaActLevPropia.DataBind();
        }

        //Ventanas de mensaje de usuario
        private void PopupMensajes()
        {
            ModalPopupAlertas.Show();
        }
        private void PopupFotos()
        {
            ModalPopupFotos.Show();
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //controla las sesiones de usuario 
                try
                {
                    this.Session["id_Planning"] = "";
                    this.Session["cinfo"] = "";
                    this.Session["CONSECUTIVO"] = 0;
                    string sUser = this.Session["sUser"].ToString();
                    usersession.Text = sUser;
                    string sPassw = this.Session["sPassw"].ToString();
                    if (sUser != null && sPassw != null)
                    {
                        llena_planning();


                        TabCOperativo.Tabs[0].Enabled = true;
                        TabCOperativo.Tabs[1].Enabled = false;
                        TabCOperativo.Tabs[2].Enabled = false;
                        TabCOperativo.Tabs[3].Enabled = false;
                        TabCOperativo.Tabs[4].Enabled = false;
                        TabCOperativo.Tabs[5].Enabled = false;
                        TabCOperativo.Tabs[6].Enabled = false;
                        TabCOperativo.Tabs[7].Enabled = false;
                        TabCOperativo.Tabs[8].Enabled = true;

                        //td en Tab 4(Digitación)
                        tdfotos.Style.Value = "Display:none;";
                        ifcarga.Style.Value = "Display:none;";

                        //poner esta funcion en evento ActiveTabChange  del tabcontainer e independizar
                        InActivarControles(this);

                        //poner en evento activetabChange del tabontainer en tab TabPPrevPropios
                        LkbPuntosDeVenta.Visible = false;
                        LkbEncabezado.Visible = false;
                        LkbPie.Visible = false;
                        LkbProductos.Visible = false;
                        LkbMarcas.Visible = false;

                        OpenExcel.Visible = false;


                    }
                }
                catch (Exception ex)
                {
                    Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                    this.Session.Abandon();
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }

            tdfotos.Style.Value = "Display:none;";
            ifcarga.Style.Value = "Display:none;";

        }

        //-- Description: Permite navegar entre paginas en la grilla
        protected void GVPlanning_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVPlanning.PageIndex = e.NewPageIndex;
            llena_planning();
        }
        protected void GVFormaPropio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVFormaPropio.PageIndex = e.NewPageIndex;
            llena_FormatosPropios();
        }
        protected void GVConsultaActCom_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVConsultaActCom.PageIndex = e.NewPageIndex;
            llena_ConsultarACTCom();
            ModalPopupBuscarActCom.Show();
        }
        protected void GVConsultaActLevPropia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVConsultaActLevPropia.PageIndex = e.NewPageIndex;
            llena_ConsultarACTLevPropio();
            ModalPopupBuscarActPropia.Show();
        }

        //-- Description: ejecuta acciones al seleccionar en el objeto
        protected void GVPlanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                llena_Servicios();
                TabCOperativo.ActiveTabIndex = 1;
                TabCOperativo.Tabs[0].Enabled = true;
                TabCOperativo.Tabs[1].Enabled = true;
                TabCOperativo.Tabs[2].Enabled = false;
                TabCOperativo.Tabs[3].Enabled = false;
                TabCOperativo.Tabs[4].Enabled = false;
                TabCOperativo.Tabs[5].Enabled = false;
                TabCOperativo.Tabs[6].Enabled = false;
                TabCOperativo.Tabs[7].Enabled = true;
                TabCOperativo.Tabs[8].Enabled = true;

                CmbPlanningForm.Text = GVPlanning.SelectedRow.Cells[1].Text;
                this.Session["id_Planning"] = CmbPlanningForm.Text;
                this.Session["namePlanning"] = GVPlanning.SelectedRow.Cells[2].Text;
                this.Session["fechainiplanning"] = Convert.ToDateTime(GVPlanning.SelectedRow.Cells[4].Text);
                this.Session["company_id"] = Convert.ToInt32(GVPlanning.SelectedRow.Cells[6].Text);
                TxtSolicitante.Text = this.Session["smail"].ToString().Trim();
                DataTable dtSupervisores = Get_Operativo.Get_StaffSupervisorPlanning(Convert.ToInt32(this.Session["id_Planning"]));
                string sTxtEmail = "";
                if (dtSupervisores != null)
                {
                    for (int i = 0; i <= dtSupervisores.Rows.Count - 1; i++)
                    {
                        sTxtEmail = sTxtEmail + dtSupervisores.Rows[i]["Person_Email"].ToString().Trim();
                        if (i < dtSupervisores.Rows.Count - 1)
                        {
                            sTxtEmail = sTxtEmail + ";";
                        }
                    }
                    this.Session["TxtEmail"] = sTxtEmail;
                    TxtEmail.Text = this.Session["TxtEmail"].ToString().Trim();

                }

                llena_FormatosPropios();
                ChkAll.Checked = false;
                llena_StaffPlanning();


                if (LstPlanningOp.Items.Count == 0)
                {
                    ChkAll.Enabled = false;
                }
                else
                {
                    ChkAll.Enabled = true;
                }

                llena_FormatoActCom();
                llena_ConsultarACTCom();
                llena_ConsultarACTLevPropio();
                LkbPuntosDeVenta.Visible = false;
                LkbEncabezado.Visible = false;
                LkbPie.Visible = false;
                LkbProductos.Visible = false;
                LkbMarcas.Visible = false;
                OpenExcel.Visible = false;
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        protected void GVFormaCompetencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabCOperativo.ActiveTabIndex = 3;
            TabCOperativo.Tabs[0].Enabled = true;
            TabCOperativo.Tabs[1].Enabled = true;
            TabCOperativo.Tabs[2].Enabled = false;
            TabCOperativo.Tabs[3].Enabled = true;
            TabCOperativo.Tabs[4].Enabled = true;
            TabCOperativo.Tabs[5].Enabled = false;
            TabCOperativo.Tabs[6].Enabled = false;
            TabCOperativo.Tabs[7].Enabled = true;
            TabCOperativo.Tabs[8].Enabled = true;

            LblTitFormato.Text = GVFormaCompetencia.SelectedRow.Cells[1].Text;
            LblName_planning.Text = GVPlanning.SelectedRow.Cells[2].Text;
            DataTable dtformacom = Get_Operativo.Get_FormatoActivComercio();
            GVFormatoCompetencia.DataSource = dtformacom;
            GVFormatoCompetencia.DataBind();
            //GVFormatoCompetencia.Visible = true;
            IframeCompe.Attributes["src"] = "RVCompetencia.aspx";
            ifcarga.Attributes["src"] = "CargaFotosCom.aspx";
        }
        protected void GVConsultaActCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = Get_Operativo.Get_SearchInfoCompetition_Information(Convert.ToInt32(GVConsultaActCom.SelectedRow.Cells[1].Text));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    this.Session["cinfo"] = dt.Rows[0]["id_cinfo"].ToString().Trim();
                    TxtFechaEjec.Text = dt.Rows[0]["cinfo_DateExecution"].ToString().Trim();
                    Txtcinfo_nameCompany.Text = dt.Rows[0]["cinfo_nameCompany"].ToString().Trim();
                    Txtcinfo_Brand.Text = dt.Rows[0]["cinfo_Brand"].ToString().Trim();
                    Txtcinfo_Name_Activity.Text = dt.Rows[0]["cinfo_Name_Activity"].ToString().Trim();
                    Txtcinfo_FeciniPromo.Text = dt.Rows[0]["cinfo_FeciniPromo"].ToString().Trim();
                    if (dt.Rows[0]["cinfo_Vigente"].ToString().Trim() == "Vigente")
                    {
                        RBtncinfo_Vigente.Items[0].Selected = true;
                    }
                    else
                    {
                        if (dt.Rows[0]["cinfo_Vigente"].ToString().Trim() == "No Vigente")
                        {
                            RBtncinfo_Vigente.Items[1].Selected = true;
                        }
                        else
                        {
                            RBtncinfo_Vigente.Items[0].Selected = false;
                            RBtncinfo_Vigente.Items[1].Selected = false;
                        }
                    }
                    Txtcinfo_Mecanica.Text = dt.Rows[0]["Mecanica"].ToString().Trim();
                    Txtcinfo_Comment_Observa.Text = dt.Rows[0]["cinfo_Comment_Observa"].ToString().Trim();
                    Cmbcinfo_CodStrategy.SelectedValue = dt.Rows[0]["cod_Strategy"].ToString().Trim();
                    Cmbcinfo_Cod_Channel.SelectedValue = dt.Rows[0]["cod_Channel"].ToString().Trim();
                    Txtcinfo_FecfinPromo.Text = dt.Rows[0]["cinfo_FecfinPromo"].ToString().Trim();
                    Txtcinfo_PersonnelCantid.Text = dt.Rows[0]["cinfo_PersonnelCatid"].ToString().Trim();
                    Cmbcinfo_id_ProductCategory.SelectedValue = dt.Rows[0]["id_ProductCategory"].ToString().Trim();
                    if (dt.Rows[0]["cinfo_Status"].ToString().Trim() == "True")
                    {
                        RbtnStatusActCom.Items[0].Selected = true;
                        RbtnStatusActCom.Items[1].Selected = false;
                    }
                    else
                    {
                        RbtnStatusActCom.Items[0].Selected = false;
                        RbtnStatusActCom.Items[1].Selected = true;
                    }
                    DataTable dt1 = Get_Operativo.Get_SearchInfoPOPCompetition_Information(Convert.ToInt32(GVConsultaActCom.SelectedRow.Cells[1].Text));
                    if (dt1 != null)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                            {
                                for (int j = 0; j <= ChkCinfo_id_MPointOfPurchase.Items.Count - 1; j++)
                                {
                                    if (ChkCinfo_id_MPointOfPurchase.Items[j].Value == dt1.Rows[i]["id_MPointOfPurchase"].ToString().Trim())
                                    {
                                        ChkCinfo_id_MPointOfPurchase.Items[j].Selected = Convert.ToBoolean(dt1.Rows[i]["CI_POP_Status"].ToString().Trim());
                                        j = ChkCinfo_id_MPointOfPurchase.Items.Count - 1;
                                    }
                                }
                            }
                        }
                    }
                    dt1 = null;

                    DataTable dt2 = Get_Operativo.Get_FotosCompetition_Information(Convert.ToInt32(this.Session["cinfo"].ToString().Trim()));
                    GVFotografias.DataSource = dt2;
                    GVFotografias.DataBind();

                    if (dt2 != null)
                    {
                        if (dt2.Rows.Count > 0)
                        {
                            for (int j = 0; j <= GVFotografias.Rows.Count - 1; j++)
                            {
                                string fn = System.IO.Path.GetFileName(dt2.Rows[j]["PhotoCI_PathName"].ToString().Trim());
                                ((CheckBox)GVFotografias.Rows[j].Cells[0].FindControl("CheckBox1")).Checked = Convert.ToBoolean(dt2.Rows[j]["PhotoCI_Status"].ToString().Trim());
                                ((Image)GVFotografias.Rows[j].Cells[0].FindControl("Image1")).ImageUrl = "~/Pages/Modulos/Operativo/PictureComercio/" + fn;
                            }
                        }
                    }
                    dt2 = null;
                }
            }
            dt = null;

            TxtFechaEjec.Enabled = false;
            Txtcinfo_nameCompany.Enabled = false;
            Txtcinfo_Brand.Enabled = false;
            Txtcinfo_Name_Activity.Enabled = false;
            Txtcinfo_FeciniPromo.Enabled = false;
            RBtncinfo_Vigente.Enabled = false;
            Txtcinfo_Mecanica.Enabled = false;
            Txtcinfo_Comment_Observa.Enabled = false;
            Cmbcinfo_CodStrategy.Enabled = false;
            Cmbcinfo_Cod_Channel.Enabled = false;
            Txtcinfo_FecfinPromo.Enabled = false;
            Txtcinfo_PersonnelCantid.Enabled = false;
            Cmbcinfo_id_ProductCategory.Enabled = false;
            ChkCinfo_id_MPointOfPurchase.Enabled = false;
            ImgCalendar.Enabled = false;
            ImgCalendarFF.Enabled = false;
            ImgCalendarFI.Enabled = false;


            IbtnCrearOpe.Visible = false;
            IbtnSaveOpe.Visible = false;
            IbtnSearchOpe.Visible = true;
            IbtnEditOpe.Visible = true;
            IbtnActualizaOpe.Visible = false;
            BtnImgTerminarCarga.Visible = false;
            tdfotos.Style.Value = "Display:block;";
            GVFotografias.Enabled = false;
            ImgBtnFileUpload.Visible = false;
            ImgBtnFileUploadSearch.Visible = false;
        }
        protected void GVConsultaActLevPropia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["id_Planning"].ToString() != "")
            {
                DataTable dt = Get_Operativo.Get_SearchInfoLevantamientoPropia(Convert.ToInt32(this.Session["id_Planning"]), GVConsultaActLevPropia.SelectedRow.Cells[3].Text.ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[5].Text.ToString().Trim());
                this.Session["Get_SearchInfoLevantamientoPropia"] = dt;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            CmbOperaDigita.Text = dt.Rows[0]["Person_id"].ToString().Trim();
                        }
                        catch
                        {
                            Alertas.CssClass = "MensajesSupervisor";
                            LblAlert.Text = "Selección de Operativo";
                            LblFaltantes.Text = "Sr. Usuario, este registro corresponde a un operativo el cual no fue seleccionado en la pestaña formato, por favor seleccionelo e intente nuevamente";
                            PopupMensajes();
                            return;

                        }
                        llena_PDV();
                        llena_Products();
                        CmbPdvDigita.Text = dt.Rows[0]["id_MPOSPlanning"].ToString().Trim();
                        CmbProdDigita.Text = dt.Rows[0]["id_ProductsPlanning"].ToString().Trim();

                        llena_ProductsCompe();
                        GvDatosIndicadorVentas.Caption = CmbProdDigita.SelectedItem.ToString().Trim();
                        GvDatosIndicadorPrecios.Caption = CmbProdDigita.SelectedItem.ToString().Trim();
                        GvDatosIndicadorCobertura.Caption = CmbProdDigita.SelectedItem.ToString().Trim();
                        //GvDatosIndicadorSOD.Caption = CmbProdDigita.SelectedItem.ToString().Trim();

                        TxtFecOperaDigita.Text = dt.Rows[0]["dato_Date"].ToString().Trim();
                        FecOperaDigita = Convert.ToDateTime(TxtFecOperaDigita.Text.ToString());
                        System.Globalization.CultureInfo norwCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es");
                        System.Globalization.Calendar cal = norwCulture.Calendar;
                        int weekNo = cal.GetWeekOfYear(FecOperaDigita, norwCulture.DateTimeFormat.CalendarWeekRule, norwCulture.DateTimeFormat.FirstDayOfWeek);
                        this.Session["NumSemana"] = weekNo.ToString();




                        if (dt.Rows[0]["almacenDetalle_Status"].ToString().Trim() == "True")
                        {
                            RbtnStatusActPropia.Items[0].Selected = true;
                            RbtnStatusActPropia.Items[1].Selected = false;
                        }
                        else
                        {
                            RbtnStatusActPropia.Items[0].Selected = false;
                            RbtnStatusActPropia.Items[1].Selected = true;
                        }

                        for (int j = 0; j <= GvAlmacenPlanning.Rows.Count - 1; j++)
                        {
                            ((TextBox)GvAlmacenPlanning.Rows[j].Cells[0].FindControl("TxtCaptura")).Text = dt.Rows[j]["datoAlmacenado"].ToString().Trim();
                        }
                        Paneldigitacion.Style.Value = "display:block;";
                        PanelTipoDigitacion.Style.Value = "display:none;";
                        if (GvDatosIndicadorVentas.Rows.Count > 0)
                        {
                            TabVentas.Visible = true;

                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador17"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicador(17, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, GVConsultaActLevPropia.SelectedRow.Cells[5].Text.ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador17"] = dt2;
                            for (int j = 0; j <= GvDatosIndicadorVentas.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvDatosIndicadorVentas.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        else
                        {
                            TabVentas.Visible = false;
                        }

                        DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                        if (GvVentasCompe1.Rows.Count > 0)
                        {
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador170"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicadorCompe(17, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, prodcompetenciadigitar.Rows[0][1].ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador170"] = dt2;
                            for (int j = 0; j <= GvVentasCompe1.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvVentasCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        if (GvVentasCompe2.Rows.Count > 0)
                        {
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador171"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicadorCompe(17, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, prodcompetenciadigitar.Rows[1][1].ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador171"] = dt2;
                            for (int j = 0; j <= GvVentasCompe2.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvVentasCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        if (GvVentasCompe3.Rows.Count > 0)
                        {
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador172"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicadorCompe(17, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, prodcompetenciadigitar.Rows[2][1].ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador172"] = dt2;
                            for (int j = 0; j <= GvVentasCompe3.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvVentasCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        if (GvDatosIndicadorPrecios.Rows.Count > 0)
                        {
                            TabPrecios.Visible = true;

                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador19"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicador(19, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, GVConsultaActLevPropia.SelectedRow.Cells[5].Text.ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador19"] = dt2;
                            for (int j = 0; j <= GvDatosIndicadorPrecios.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvDatosIndicadorPrecios.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        else
                        {
                            TabPrecios.Visible = false;
                        }
                        if (GvPreciosCompe1.Rows.Count > 0)
                        {
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador190"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicadorCompe(19, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, prodcompetenciadigitar.Rows[0][1].ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador190"] = dt2;
                            for (int j = 0; j <= GvPreciosCompe1.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvPreciosCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        if (GvPreciosCompe2.Rows.Count > 0)
                        {
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador191"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicadorCompe(19, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, prodcompetenciadigitar.Rows[1][1].ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador191"] = dt2;
                            for (int j = 0; j <= GvPreciosCompe2.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvPreciosCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        if (GvPreciosCompe3.Rows.Count > 0)
                        {
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador192"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicadorCompe(19, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, prodcompetenciadigitar.Rows[2][1].ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador192"] = dt2;
                            for (int j = 0; j <= GvPreciosCompe3.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvPreciosCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        if (GvDatosIndicadorCobertura.Rows.Count > 0)
                        {
                            TabCobertura.Visible = true;
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador22"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicador(22, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, GVConsultaActLevPropia.SelectedRow.Cells[5].Text.ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador22"] = dt2;
                            for (int j = 0; j <= GvDatosIndicadorCobertura.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvDatosIndicadorCobertura.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        else
                        {
                            TabCobertura.Visible = false;
                        }
                        if (GvCoberturaCompe1.Rows.Count > 0)
                        {
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador220"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicadorCompe(22, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, prodcompetenciadigitar.Rows[0][1].ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador220"] = dt2;
                            for (int j = 0; j <= GvCoberturaCompe1.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvCoberturaCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        if (GvCoberturaCompe2.Rows.Count > 0)
                        {
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador221"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicadorCompe(22, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, prodcompetenciadigitar.Rows[1][1].ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador221"] = dt2;
                            for (int j = 0; j <= GvCoberturaCompe2.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvCoberturaCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        if (GvCoberturaCompe3.Rows.Count > 0)
                        {
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador222"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicadorCompe(22, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, prodcompetenciadigitar.Rows[2][1].ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador222"] = dt2;
                            for (int j = 0; j <= GvCoberturaCompe3.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvCoberturaCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        if (GvDatosIndicadorSOD.Rows.Count > 0)
                        {
                            TabSOD.Visible = true;
                            lblMarcaProdSel.Visible = true;
                            lblTxtMarcaProdSel.Visible = true;
                            LblMarcaProdCompeSel.Visible = true;
                            lblTxtMarcaProdCompeSel.Visible = true;
                            LblMarcaProdCompeSel1.Visible = true;
                            lblTxtMarcaProdCompeSel1.Visible = true;
                            LblMarcaProdCompeSel2.Visible = true;
                            lblTxtMarcaProdCompeSel2.Visible = true;
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador21"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicador(21, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, GVConsultaActLevPropia.SelectedRow.Cells[5].Text.ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador21"] = dt2;
                            for (int j = 0; j <= GvDatosIndicadorSOD.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvDatosIndicadorSOD.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        else
                        {
                            TabSOD.Visible = false;
                        }
                        if (GvSODCompe1.Rows.Count > 0)
                        {
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador210"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicadorCompe(21, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, prodcompetenciadigitar.Rows[0][1].ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador210"] = dt2;
                            for (int j = 0; j <= GvSODCompe1.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvSODCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        if (GvSODCompe2.Rows.Count > 0)
                        {
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador211"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicadorCompe(21, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, prodcompetenciadigitar.Rows[1][1].ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador211"] = dt2;
                            for (int j = 0; j <= GvSODCompe2.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvSODCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                        if (GvSODCompe3.Rows.Count > 0)
                        {
                            DataTable dt1 = (DataTable)this.Session["dtformatospropiosindicador212"];
                            DataTable dt2 = Get_Operativo.Get_SearchInfoContenidoFormatosPropiosIndicadorCompe(21, Convert.ToInt32(this.Session["id_Planning"]), TxtFecOperaDigita.Text, prodcompetenciadigitar.Rows[2][1].ToString().Trim(), GVConsultaActLevPropia.SelectedRow.Cells[4].Text.ToString().Trim());
                            this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador212"] = dt2;
                            for (int j = 0; j <= GvSODCompe3.Rows.Count - 1; j++)
                            {
                                ((TextBox)GvSODCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = dt2.Rows[j][dt1.Rows[j][2].ToString().Trim()].ToString().Trim();
                            }
                        }
                    }
                }
                IbtnCrearCaptura.Visible = false;
                IbtnSaveCaptura.Visible = false;
                IbtnSearchCaptura.Visible = true;
                IbtnEditCaptura.Visible = true;
                IbtnActualizaCaptura.Visible = false;

                CmbOperaDigita.Enabled = false;
                TxtFecOperaDigita.Enabled = false;
                ImgBtnFecOperaDigita.Enabled = false;
                CmbPdvDigita.Enabled = false;
                CmbProdDigita.Enabled = false;
                RbtnStatusActPropia.Enabled = false;
                GvAlmacenPlanning.Enabled = false;
                GvDatosIndicadorVentas.Enabled = false;
                GvDatosIndicadorPrecios.Enabled = false;
                GvDatosIndicadorSOD.Enabled = false;
                GvDatosIndicadorCobertura.Enabled = false;
                GvVentasCompe1.Enabled = false;
                GvVentasCompe2.Enabled = false;
                GvVentasCompe3.Enabled = false;
                GvPreciosCompe1.Enabled = false;
                GvPreciosCompe2.Enabled = false;
                GvPreciosCompe3.Enabled = false;
                GvCoberturaCompe1.Enabled = false;
                GvCoberturaCompe2.Enabled = false;
                GvCoberturaCompe3.Enabled = false;
                GvSODCompe1.Enabled = false;
                GvSODCompe2.Enabled = false;
                GvSODCompe3.Enabled = false;
            }
            else
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAll.Checked == true)
            {
                for (int i = 0; i <= LstPlanningOp.Items.Count - 1; i++)
                {
                    LstPlanningOp.Items[i].Selected = true;
                }
            }
            else
            {
                for (int i = 0; i <= LstPlanningOp.Items.Count - 1; i++)
                {
                    LstPlanningOp.Items[i].Selected = false;
                }
            }
        }

        //-- Description: Llenar la grilla con fotografias almacenadas para la actividad registrada
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            tdfotos.Style.Value = "Display:block;";
            ifcarga.Style.Value = "Display:block;";
            GVFotografias.Enabled = false;
            DataTable dt = Get_Operativo.Get_idCompetition_Information(Convert.ToString(this.Session["sUser"]));
            DataTable dt1 = Get_Operativo.Get_FotosCompetition_Information(Convert.ToInt32(dt.Rows[0]["id_cinfo"].ToString().Trim()));
            GVFotografias.DataSource = dt1;
            GVFotografias.DataBind();

            if (dt1 != null)
            {
                if (dt1.Rows.Count > 0)
                {
                    for (int j = 0; j <= GVFotografias.Rows.Count - 1; j++)
                    {
                        string fn = System.IO.Path.GetFileName(dt1.Rows[j]["PhotoCI_PathName"].ToString().Trim());
                        ((CheckBox)GVFotografias.Rows[j].Cells[0].FindControl("CheckBox1")).Checked = Convert.ToBoolean(dt1.Rows[j]["PhotoCI_Status"].ToString().Trim());
                        ((Image)GVFotografias.Rows[j].Cells[0].FindControl("Image1")).ImageUrl = "~/Pages/Modulos/Operativo/PictureComercio/" + fn;
                    }
                }
            }
        }

        protected void ImageButtonSearch_Click(object sender, ImageClickEventArgs e)
        {
            tdfotos.Style.Value = "Display:block;";
            ifcarga.Style.Value = "Display:block;";
            DataTable dt1 = Get_Operativo.Get_FotosCompetition_Information(Convert.ToInt32(this.Session["cinfo"].ToString().Trim()));
            GVFotografias.DataSource = dt1;
            GVFotografias.DataBind();

            if (dt1 != null)
            {
                if (dt1.Rows.Count > 0)
                {
                    for (int j = 0; j <= GVFotografias.Rows.Count - 1; j++)
                    {
                        string fn = System.IO.Path.GetFileName(dt1.Rows[j]["PhotoCI_PathName"].ToString().Trim());
                        ((CheckBox)GVFotografias.Rows[j].Cells[0].FindControl("CheckBox1")).Checked = Convert.ToBoolean(dt1.Rows[j]["PhotoCI_Status"].ToString().Trim());
                        ((Image)GVFotografias.Rows[j].Cells[0].FindControl("Image1")).ImageUrl = "~/Pages/Modulos/Operativo/PictureComercio/" + fn;
                    }
                }
            }
        }

        //-- Description:       <Permite Crear .... Ing. Mauricio Ortiz>
        //-- Requerimiento No.  <>
        //-- =============================================
        protected void IbtnCrearOpe_Click(object sender, ImageClickEventArgs e)
        {
            this.Session["CONSECUTIVO"] = 0;
            TxtFechaEjec.Focus();
            IbtnCrearOpe.Visible = false;
            IbtnSaveOpe.Visible = true;
            IbtnSaveOpe.Enabled = true;
            IbtnSearchOpe.Visible = false;
            IbtnEditOpe.Visible = false;
            IbtnActualizaOpe.Visible = false;
            BtnImgTerminarCarga.Visible = true;

            TxtFechaEjec.Text = "";
            Txtcinfo_nameCompany.Text = "";
            Txtcinfo_Brand.Text = "";
            Txtcinfo_Name_Activity.Text = "";
            Txtcinfo_FeciniPromo.Text = "";
            Txtcinfo_Mecanica.Text = "";
            Txtcinfo_Comment_Observa.Text = "";
            Cmbcinfo_CodStrategy.Text = "0";
            Cmbcinfo_Cod_Channel.Text = "0";
            Txtcinfo_FecfinPromo.Text = "";
            Txtcinfo_PersonnelCantid.Text = "";
            Cmbcinfo_id_ProductCategory.Text = "0";

            for (int i = 0; i <= ChkCinfo_id_MPointOfPurchase.Items.Count - 1; i++)
            {
                ChkCinfo_id_MPointOfPurchase.Items[i].Selected = false;
            }


            for (int i = 0; i <= RBtncinfo_Vigente.Items.Count - 1; i++)
            {
                RBtncinfo_Vigente.Items[i].Selected = false;

            }

            TxtFechaEjec.Enabled = true;
            Txtcinfo_nameCompany.Enabled = true;
            Txtcinfo_Brand.Enabled = true;
            Txtcinfo_Name_Activity.Enabled = true;
            Txtcinfo_FeciniPromo.Enabled = true;
            RBtncinfo_Vigente.Enabled = true;
            Txtcinfo_Mecanica.Enabled = true;
            Txtcinfo_Comment_Observa.Enabled = true;
            Cmbcinfo_CodStrategy.Enabled = true;
            Cmbcinfo_Cod_Channel.Enabled = true;
            Txtcinfo_FecfinPromo.Enabled = true;
            Txtcinfo_PersonnelCantid.Enabled = true;
            Cmbcinfo_id_ProductCategory.Enabled = true;
            ChkCinfo_id_MPointOfPurchase.Enabled = true;
            ImgCalendar.Enabled = true;
            ImgCalendarFF.Enabled = true;
            ImgCalendarFI.Enabled = true;
            RbtnStatusActCom.Enabled = false;

            TabCOperativo.Tabs[0].Enabled = false;
            TabCOperativo.Tabs[1].Enabled = false;
            TabCOperativo.Tabs[2].Enabled = false;
            TabCOperativo.Tabs[3].Enabled = false;
            TabCOperativo.Tabs[4].Enabled = true;
            TabCOperativo.Tabs[5].Enabled = false;
            TabCOperativo.Tabs[6].Enabled = false;
            TabCOperativo.Tabs[7].Enabled = false;
            TabCOperativo.Tabs[8].Enabled = false;

            GVFotografias.DataBind();
        }
        protected void IbtnSaveOpe_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (TxtFechaEjec.Text != "")
                {
                    FechaEjec = Convert.ToDateTime(TxtFechaEjec.Text.ToString());
                }

                if (Txtcinfo_FeciniPromo.Text != "")
                {
                    cinfo_FeciniPromo = Convert.ToDateTime(Txtcinfo_FeciniPromo.Text.ToString());
                }

                if (Txtcinfo_FecfinPromo.Text != "")
                {
                    cinfo_FecfinPromo = Convert.ToDateTime(Txtcinfo_FecfinPromo.Text.ToString());
                }

            }
            catch
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Parametros Incorrectos";
                LblFaltantes.Text = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
                PopupMensajes();
                return;
            }
            TxtFechaEjec.Text = TxtFechaEjec.Text.TrimStart();
            Txtcinfo_nameCompany.Text = Txtcinfo_nameCompany.Text.TrimStart();
            Txtcinfo_Brand.Text = Txtcinfo_Brand.Text.TrimStart();
            Txtcinfo_Name_Activity.Text = Txtcinfo_Name_Activity.Text.TrimStart();
            Txtcinfo_Mecanica.Text = Txtcinfo_Mecanica.Text.TrimStart();
            Txtcinfo_Comment_Observa.Text = Txtcinfo_Comment_Observa.Text.TrimStart();

            if (TxtFechaEjec.Text == "" || Txtcinfo_nameCompany.Text == "" || Txtcinfo_Brand.Text == "" || Txtcinfo_Name_Activity.Text == "" || Txtcinfo_Mecanica.Text == ""
                || Txtcinfo_Comment_Observa.Text == "" || Cmbcinfo_CodStrategy.SelectedValue == "0")
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Parámetros obligatorio (*)";
                LblFaltantes.Text = "Todos los campos marcados con (*) deben estar digitados. Por favor verifique";
                PopupMensajes();
                return;
            }

            if (RBtncinfo_Vigente.Items[0].Selected == false && RBtncinfo_Vigente.Items[1].Selected == false)
            {
                svigencia = "No Sabe";
            }
            else
            {
                if (RBtncinfo_Vigente.Items[0].Selected == true)
                {
                    svigencia = "Vigente";
                }
                else
                {
                    svigencia = "No Vigente";
                }
            }
            if (Txtcinfo_FeciniPromo.Text == "" || Txtcinfo_FecfinPromo.Text == "")
            {
                if (Txtcinfo_FeciniPromo.Text == "")
                {
                    cinfo_FeciniPromo = Convert.ToDateTime("01/01/1900");
                }
                if (Txtcinfo_FecfinPromo.Text == "")
                {
                    cinfo_FecfinPromo = Convert.ToDateTime("01/01/1900");
                }
            }
            cinfo_CodStrategy = Cmbcinfo_CodStrategy.SelectedValue;
            cinfo_Cod_Channel = Cmbcinfo_Cod_Channel.SelectedValue;
            cinfo_id_ProductCategory = Cmbcinfo_id_ProductCategory.SelectedValue;

            if (Cmbcinfo_CodStrategy.SelectedValue == "0" || Cmbcinfo_Cod_Channel.SelectedValue == "0" || Cmbcinfo_id_ProductCategory.SelectedValue == "0")
            {
                if (Cmbcinfo_CodStrategy.SelectedValue == "0")
                {
                    cinfo_CodStrategy = "0";
                }
                else
                {
                    cinfo_CodStrategy = Cmbcinfo_CodStrategy.SelectedValue;
                }
                if (Cmbcinfo_Cod_Channel.SelectedValue == "0")
                {
                    cinfo_Cod_Channel = "0";

                }
                else
                {
                    cinfo_Cod_Channel = Cmbcinfo_Cod_Channel.SelectedValue;
                }
                if (Cmbcinfo_id_ProductCategory.SelectedValue == "0")
                {
                    cinfo_id_ProductCategory = "0";
                }
                else
                {
                    cinfo_id_ProductCategory = Cmbcinfo_id_ProductCategory.SelectedValue;
                }
            }
            if (Txtcinfo_PersonnelCantid.Text == "")
            {
                Txtcinfo_PersonnelCantid.Text = "0";
            }

            DataTable DtDuplicados = Get_Operativo.Get_DuplicadosCompetition_Information("[Competition_ Information]", FechaEjec, Txtcinfo_nameCompany.Text, Txtcinfo_Name_Activity.Text, this.Session["id_Planning"].ToString().Trim());
            if (DtDuplicados != null)
            {
                if (DtDuplicados.Rows.Count > 0)
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Registro Duplicado";
                    LblFaltantes.Text = "Sr. Usuario, la actividad registrada ya existe en este planning. Por favor verifique fecha de ejecución, empresa  y actividad.";
                    PopupMensajes();
                    return;
                }
                else
                {
                    ECompetition__Information ActividadCom = oCompetition__Information.RegistrarActividadCom(CmbPlanningForm.SelectedValue,
                        FechaEjec, Txtcinfo_nameCompany.Text, Txtcinfo_Brand.Text, Convert.ToInt32(cinfo_CodStrategy), Txtcinfo_Name_Activity.Text,
                        cinfo_Cod_Channel, cinfo_FeciniPromo, cinfo_FecfinPromo, svigencia, Convert.ToInt32(Txtcinfo_PersonnelCantid.Text), Txtcinfo_Mecanica.Text,
                        cinfo_id_ProductCategory, Txtcinfo_Comment_Observa.Text, true,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    DataTable dt = Get_Operativo.Get_idCompetition_Information(Convert.ToString(this.Session["sUser"]));
                    int cinfo = Convert.ToInt32(dt.Rows[0]["id_cinfo"].ToString().Trim());

                    for (int i = 0; i <= ChkCinfo_id_MPointOfPurchase.Items.Count - 1; i++)
                    {
                        if (ChkCinfo_id_MPointOfPurchase.Items[i].Selected == true)
                        {
                            ECompetition__Information POPActividadCom = oCompetition__Information.RegistrarPOPActividadCom(cinfo, Convert.ToInt32(ChkCinfo_id_MPointOfPurchase.Items[i].Value),
                                ChkCinfo_id_MPointOfPurchase.Items[i].Selected, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        }
                    }
                    IbtnCrearOpe.Visible = false;
                    IbtnSaveOpe.Visible = false;
                    IbtnSearchOpe.Visible = false;
                    IbtnEditOpe.Visible = false;
                    IbtnActualizaOpe.Visible = false;
                    IbtnCancelReg.Visible = false;
                    TxtFechaEjec.Enabled = false;
                    Txtcinfo_nameCompany.Enabled = false;
                    Txtcinfo_Brand.Enabled = false;
                    Txtcinfo_Name_Activity.Enabled = false;
                    Txtcinfo_FeciniPromo.Enabled = false;
                    RBtncinfo_Vigente.Enabled = false;
                    Txtcinfo_Mecanica.Enabled = false;
                    Txtcinfo_Comment_Observa.Enabled = false;
                    Cmbcinfo_CodStrategy.Enabled = false;
                    Cmbcinfo_Cod_Channel.Enabled = false;
                    Txtcinfo_FecfinPromo.Enabled = false;
                    Txtcinfo_PersonnelCantid.Enabled = false;
                    Cmbcinfo_id_ProductCategory.Enabled = false;
                    ChkCinfo_id_MPointOfPurchase.Enabled = false;
                    ImgCalendar.Enabled = false;
                    ImgCalendarFF.Enabled = false;
                    ImgCalendarFI.Enabled = false;
                    PopupFotos();
                }
            }
            DtDuplicados = null;
            llena_ConsultarACTCom();
        }

        //-- Description: Finalizar la carga de las fotografias de las actividades de la competencia
        protected void BtnImgTerminarCarga_Click(object sender, ImageClickEventArgs e)
        {
            IbtnCrearOpe.Visible = true;
            IbtnSaveOpe.Visible = false;
            IbtnSearchOpe.Visible = true;
            IbtnEditOpe.Visible = false;
            IbtnActualizaOpe.Visible = false;
            IbtnCancelReg.Visible = true;
            tdfotos.Style.Value = "Display:none;";
            ifcarga.Style.Value = "Display:none;";
            TxtFechaEjec.Text = "";
            Txtcinfo_nameCompany.Text = "";
            Txtcinfo_Brand.Text = "";
            Txtcinfo_Name_Activity.Text = "";
            Txtcinfo_FeciniPromo.Text = "";
            Txtcinfo_Mecanica.Text = "";
            Txtcinfo_Comment_Observa.Text = "";
            Cmbcinfo_CodStrategy.Text = "0";
            Cmbcinfo_Cod_Channel.Text = "0";
            Txtcinfo_FecfinPromo.Text = "";
            Txtcinfo_PersonnelCantid.Text = "";
            Cmbcinfo_id_ProductCategory.Text = "0";

            for (int i = 0; i <= ChkCinfo_id_MPointOfPurchase.Items.Count - 1; i++)
            {
                ChkCinfo_id_MPointOfPurchase.Items[i].Selected = false;
            }


            for (int i = 0; i <= RBtncinfo_Vigente.Items.Count - 1; i++)
            {
                RBtncinfo_Vigente.Items[i].Selected = false;

            }
            Alertas.CssClass = "MensajesSupConfirm";
            LblAlert.Text = "Actividad del comercio";
            LblFaltantes.Text = "Sr. Usuario, el registro se ha guardado con éxito";
            PopupMensajes();

        }

        //-- Description: habilita o deshabilita el iframe para cargar las fotografias de las actividades de la competencia
        protected void ImgBtnSI_Click(object sender, ImageClickEventArgs e)
        {
            this.Session["nuevoreg"] = "1";
            tdfotos.Style.Value = "Display:block;";
            ifcarga.Style.Value = "Display:block;";
            ImgBtnFileUpload.Visible = true;
            ImgBtnFileUploadSearch.Visible = false;
            BtnImgTerminarCarga.Enabled = true;
            ifcarga.Attributes["src"] = "CargaFotosCom.aspx";

        }
        protected void ImgBtnNO_Click(object sender, ImageClickEventArgs e)
        {
            IbtnCrearOpe.Visible = true;
            IbtnSaveOpe.Visible = false;
            IbtnSearchOpe.Visible = true;
            IbtnEditOpe.Visible = false;
            IbtnActualizaOpe.Visible = false;
            IbtnCancelReg.Visible = true;
            tdfotos.Style.Value = "Display:none;";
            ifcarga.Style.Value = "Display:none;";
            TxtFechaEjec.Text = "";
            Txtcinfo_nameCompany.Text = "";
            Txtcinfo_Brand.Text = "";
            Txtcinfo_Name_Activity.Text = "";
            Txtcinfo_FeciniPromo.Text = "";
            Txtcinfo_Mecanica.Text = "";
            Txtcinfo_Comment_Observa.Text = "";
            Cmbcinfo_CodStrategy.Text = "0";
            Cmbcinfo_Cod_Channel.Text = "0";
            Txtcinfo_FecfinPromo.Text = "";
            Txtcinfo_PersonnelCantid.Text = "";
            Cmbcinfo_id_ProductCategory.Text = "0";

            for (int i = 0; i <= ChkCinfo_id_MPointOfPurchase.Items.Count - 1; i++)
            {
                ChkCinfo_id_MPointOfPurchase.Items[i].Selected = false;
            }


            for (int i = 0; i <= RBtncinfo_Vigente.Items.Count - 1; i++)
            {
                RBtncinfo_Vigente.Items[i].Selected = false;

            }
            Alertas.CssClass = "MensajesSupConfirm";
            LblAlert.Text = "Actividad del comercio";
            LblFaltantes.Text = "Sr. Usuario, el registro se ha guardado con éxito";
            PopupMensajes();
            BtnImgTerminarCarga.Enabled = false;
        }

        //-- Description:       <Permite Cancelar acciones del maestro>
        protected void IbtnCancelReg_Click(object sender, ImageClickEventArgs e)
        {
            IbtnCrearOpe.Visible = true;
            IbtnSaveOpe.Visible = false;
            IbtnSearchOpe.Visible = true;
            IbtnEditOpe.Visible = false;
            IbtnActualizaOpe.Visible = false;
            IbtnCancelReg.Visible = true;

            TxtFechaEjec.Enabled = false;
            Txtcinfo_nameCompany.Enabled = false;
            Txtcinfo_Brand.Enabled = false;
            Txtcinfo_Name_Activity.Enabled = false;
            Txtcinfo_FeciniPromo.Enabled = false;
            RBtncinfo_Vigente.Enabled = false;
            Txtcinfo_Mecanica.Enabled = false;
            Txtcinfo_Comment_Observa.Enabled = false;
            Cmbcinfo_CodStrategy.Enabled = false;
            Cmbcinfo_Cod_Channel.Enabled = false;
            Txtcinfo_FecfinPromo.Enabled = false;
            Txtcinfo_PersonnelCantid.Enabled = false;
            Cmbcinfo_id_ProductCategory.Enabled = false;
            ChkCinfo_id_MPointOfPurchase.Enabled = false;
            RbtnStatusActCom.Enabled = false;
            ImgCalendar.Enabled = false;
            ImgCalendarFF.Enabled = false;
            ImgCalendarFI.Enabled = false;

            TxtFechaEjec.Text = "";
            Txtcinfo_nameCompany.Text = "";
            Txtcinfo_Brand.Text = "";
            Txtcinfo_Name_Activity.Text = "";
            Txtcinfo_FeciniPromo.Text = "";
            Txtcinfo_Mecanica.Text = "";
            Txtcinfo_Comment_Observa.Text = "";
            Cmbcinfo_CodStrategy.Text = "0";
            Cmbcinfo_Cod_Channel.Text = "0";
            Txtcinfo_FecfinPromo.Text = "";
            Txtcinfo_PersonnelCantid.Text = "";
            Cmbcinfo_id_ProductCategory.Text = "0";

            TabCOperativo.Tabs[0].Enabled = true;
            TabCOperativo.Tabs[1].Enabled = true;
            TabCOperativo.Tabs[2].Enabled = false;
            TabCOperativo.Tabs[3].Enabled = false;
            TabCOperativo.Tabs[4].Enabled = false;
            TabCOperativo.Tabs[5].Enabled = false;
            TabCOperativo.Tabs[6].Enabled = false;
            TabCOperativo.Tabs[7].Enabled = true;
            TabCOperativo.Tabs[8].Enabled = true;
            TabCOperativo.ActiveTabIndex = 1;
        }

        //-- Description: validación de fechas
        protected void TxtFechaEjec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TxtFechaEjec.Text != "")
                {
                    FechaEjec = Convert.ToDateTime(TxtFechaEjec.Text.ToString());
                    if (Convert.ToDateTime(TxtFechaEjec.Text.ToString()) > DateTime.Today || Convert.ToDateTime(TxtFechaEjec.Text.ToString()) < Convert.ToDateTime(GVPlanning.SelectedRow.Cells[4].Text))
                    {
                        TxtFechaEjec.Focus();
                        TxtFechaEjec.Text = "";
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parametros Incorrectos";
                        LblFaltantes.Text = "Sr. Usuario, la fecha ingresada no puede ser mayor a la fecha actual ni menor a la fecha inicial del planning. Por favor verifiquelo";
                        PopupMensajes();
                        return;
                    }
                    else
                    {
                        Txtcinfo_nameCompany.Focus();
                    }
                }
                else
                {
                    TxtFechaEjec.Focus();
                    TxtFechaEjec.Text = "";
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Fecha de Ejecución.";
                    PopupMensajes();
                    return;
                }
            }
            catch
            {
                TxtFechaEjec.Focus();
                TxtFechaEjec.Text = "";
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Parametros Incorrectos";
                LblFaltantes.Text = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
                PopupMensajes();
                return;
            }
        }
        protected void Txtcinfo_FeciniPromo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Txtcinfo_FeciniPromo.Text.ToString().Trim() != "")
                {
                    cinfo_FeciniPromo = Convert.ToDateTime(Txtcinfo_FeciniPromo.Text.ToString());
                    if (Txtcinfo_FeciniPromo.Text.ToString().Trim() != "" && Txtcinfo_FecfinPromo.Text.ToString().Trim() != "")
                    {
                        if (Convert.ToDateTime(Txtcinfo_FeciniPromo.Text.ToString().Trim()) > Convert.ToDateTime(Txtcinfo_FecfinPromo.Text.ToString().Trim()))
                        {
                            Txtcinfo_FeciniPromo.Focus();
                            Txtcinfo_FeciniPromo.Text = "";
                            Alertas.CssClass = "MensajesSupervisor";
                            LblAlert.Text = "Parametros Incorrectos";
                            LblFaltantes.Text = "Sr. Usuario, la fecha inicial no puede ser mayor a la fecha final. Por favor verifiquelo";
                            PopupMensajes();
                            return;
                        }
                        if (Convert.ToDateTime(Txtcinfo_FecfinPromo.Text.ToString()) >= DateTime.Today)
                        {
                            RBtncinfo_Vigente.Items[0].Selected = true;
                            RBtncinfo_Vigente.Enabled = false;
                            Txtcinfo_PersonnelCantid.Focus();
                        }
                        else
                        {
                            RBtncinfo_Vigente.Items[1].Selected = true;
                            RBtncinfo_Vigente.Enabled = false;
                            Txtcinfo_PersonnelCantid.Focus();
                        }

                    }
                }
                Txtcinfo_FecfinPromo.Focus();
            }
            catch
            {
                Txtcinfo_FeciniPromo.Focus();
                Txtcinfo_FeciniPromo.Text = "";
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Parametros Incorrectos";
                LblFaltantes.Text = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
                PopupMensajes();
                return;
            }
        }
        protected void Txtcinfo_FecfinPromo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Txtcinfo_FecfinPromo.Text.ToString().Trim() != "")
                {
                    cinfo_FecfinPromo = Convert.ToDateTime(Txtcinfo_FecfinPromo.Text.ToString());
                    if (Txtcinfo_FeciniPromo.Text.ToString().Trim() != "" && Txtcinfo_FecfinPromo.Text.ToString().Trim() != "")
                    {
                        if (Convert.ToDateTime(Txtcinfo_FecfinPromo.Text.ToString().Trim()) < Convert.ToDateTime(Txtcinfo_FeciniPromo.Text.ToString().Trim()))
                        {
                            Txtcinfo_FecfinPromo.Focus();
                            Txtcinfo_FecfinPromo.Text = "";
                            Alertas.CssClass = "MensajesSupervisor";
                            LblAlert.Text = "Parametros Incorrectos";
                            LblFaltantes.Text = "Sr. Usuario, la fecha final no puede ser menor a la fecha inicial. Por favor verifiquelo";
                            PopupMensajes();
                            return;
                        }
                        if (Convert.ToDateTime(Txtcinfo_FecfinPromo.Text.ToString()) >= DateTime.Today)
                        {
                            RBtncinfo_Vigente.Items[0].Selected = true;
                            RBtncinfo_Vigente.Enabled = false;
                            Txtcinfo_PersonnelCantid.Focus();
                        }
                        else
                        {
                            RBtncinfo_Vigente.Items[1].Selected = true;
                            RBtncinfo_Vigente.Enabled = false;
                            Txtcinfo_PersonnelCantid.Focus();
                        }
                    }
                    if (Convert.ToDateTime(Txtcinfo_FecfinPromo.Text.ToString().Trim()) >= DateTime.Today)
                    {
                        RBtncinfo_Vigente.Items[0].Selected = true;
                        RBtncinfo_Vigente.Enabled = false;
                        Txtcinfo_PersonnelCantid.Focus();
                    }
                    else
                    {
                        RBtncinfo_Vigente.Items[1].Selected = true;
                        RBtncinfo_Vigente.Enabled = false;
                        Txtcinfo_PersonnelCantid.Focus();
                    }

                }
                else
                {
                    RBtncinfo_Vigente.Focus();
                    RBtncinfo_Vigente.Items[0].Selected = false;
                    RBtncinfo_Vigente.Items[1].Selected = false;
                    RBtncinfo_Vigente.Enabled = true;
                }
            }
            catch
            {
                Txtcinfo_FecfinPromo.Focus();
                Txtcinfo_FecfinPromo.Text = "";
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Parametros Incorrectos";
                LblFaltantes.Text = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
                PopupMensajes();
                return;
            }

        }
        protected void TxtFecOperaDigita_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TxtFecOperaDigita.Text != "")
                {
                    FecOperaDigita = Convert.ToDateTime(TxtFecOperaDigita.Text.ToString());
                    System.Globalization.CultureInfo norwCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es");
                    System.Globalization.Calendar cal = norwCulture.Calendar;
                    int weekNo = cal.GetWeekOfYear(FecOperaDigita, norwCulture.DateTimeFormat.CalendarWeekRule, norwCulture.DateTimeFormat.FirstDayOfWeek);
                    this.Session["NumSemana"] = weekNo.ToString();

                    if (Convert.ToDateTime(TxtFecOperaDigita.Text.ToString()) > DateTime.Today || Convert.ToDateTime(TxtFecOperaDigita.Text.ToString()) < Convert.ToDateTime(GVPlanning.SelectedRow.Cells[4].Text))
                    {
                        TxtFecOperaDigita.Focus();
                        TxtFecOperaDigita.Text = "";
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parametros Incorrectos";
                        LblFaltantes.Text = "Sr. Usuario, la fecha ingresada no puede ser mayor a la fecha actual ni menor a la fecha inicial del planning. Por favor verifiquelo";
                        PopupMensajes();
                        return;
                    }

                }
                else
                {
                    TxtFecOperaDigita.Focus();
                    TxtFecOperaDigita.Text = "";
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Fecha de Ejecución.";
                    PopupMensajes();
                    return;
                }
            }
            catch
            {
                TxtFecOperaDigita.Focus();
                TxtFecOperaDigita.Text = "";
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Parametros Incorrectos";
                LblFaltantes.Text = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
                PopupMensajes();
                return;
            }
            llena_PDV();
            llena_Products();
        }

        protected void IbtnEditOpe_Click(object sender, ImageClickEventArgs e)
        {
            this.Session["nuevoreg"] = "0";
            IbtnEditOpe.Visible = false;
            IbtnActualizaOpe.Visible = true;

            TxtFechaEjec.Enabled = true;
            Txtcinfo_nameCompany.Enabled = true;
            Txtcinfo_Brand.Enabled = true;
            Txtcinfo_Name_Activity.Enabled = true;
            Txtcinfo_FeciniPromo.Enabled = true;
            RBtncinfo_Vigente.Enabled = true;
            Txtcinfo_Mecanica.Enabled = true;
            Txtcinfo_Comment_Observa.Enabled = true;
            Cmbcinfo_CodStrategy.Enabled = true;
            Cmbcinfo_Cod_Channel.Enabled = true;
            Txtcinfo_FecfinPromo.Enabled = true;
            Txtcinfo_PersonnelCantid.Enabled = true;
            Cmbcinfo_id_ProductCategory.Enabled = true;
            ChkCinfo_id_MPointOfPurchase.Enabled = true;
            ImgCalendar.Enabled = true;
            ImgCalendarFF.Enabled = true;
            ImgCalendarFI.Enabled = true;

            RbtnStatusActCom.Enabled = true;
            tdfotos.Style.Value = "Display:block;";
            ifcarga.Style.Value = "Display:block;";
            ifcarga.Attributes["src"] = "CargaFotosCom.aspx";

            GVFotografias.Enabled = true;
            ImgBtnFileUpload.Visible = false;
            ImgBtnFileUploadSearch.Visible = true;
            this.Session["FechaEjec"] = TxtFechaEjec.Text;
            this.Session["nameCompany"] = Txtcinfo_nameCompany.Text;
            this.Session["Name_Activity"] = Txtcinfo_Name_Activity.Text;
        }

        protected void IbtnActualizaOpe_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (TxtFechaEjec.Text != "")
                {
                    FechaEjec = Convert.ToDateTime(TxtFechaEjec.Text.ToString());
                }

                if (Txtcinfo_FeciniPromo.Text != "")
                {
                    cinfo_FeciniPromo = Convert.ToDateTime(Txtcinfo_FeciniPromo.Text.ToString());
                }

                if (Txtcinfo_FecfinPromo.Text != "")
                {
                    cinfo_FecfinPromo = Convert.ToDateTime(Txtcinfo_FecfinPromo.Text.ToString());
                }
            }
            catch
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Parametros Incorrectos";
                LblFaltantes.Text = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
                PopupMensajes();
                return;
            }

            TxtFechaEjec.Text = TxtFechaEjec.Text.TrimStart();
            Txtcinfo_nameCompany.Text = Txtcinfo_nameCompany.Text.TrimStart();
            Txtcinfo_Brand.Text = Txtcinfo_Brand.Text.TrimStart();
            Txtcinfo_Name_Activity.Text = Txtcinfo_Name_Activity.Text.TrimStart();
            Txtcinfo_Mecanica.Text = Txtcinfo_Mecanica.Text.TrimStart();
            Txtcinfo_Comment_Observa.Text = Txtcinfo_Comment_Observa.Text.TrimStart();

            if (TxtFechaEjec.Text == "" || Txtcinfo_nameCompany.Text == "" || Txtcinfo_Brand.Text == "" || Txtcinfo_Name_Activity.Text == "" || Txtcinfo_Mecanica.Text == ""
                || Txtcinfo_Comment_Observa.Text == "" || Cmbcinfo_CodStrategy.SelectedValue == "0")
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Parámetros obligatorio (*)";
                LblFaltantes.Text = "Todos los campos marcados con (*) deben estar digitados. Por favor verifique";
                PopupMensajes();
                return;
            }

            if (RBtncinfo_Vigente.Items[0].Selected == false && RBtncinfo_Vigente.Items[1].Selected == false)
            {
                svigencia = "No Sabe";
            }
            else
            {
                if (RBtncinfo_Vigente.Items[0].Selected == true)
                {
                    svigencia = "Vigente";
                }
                else
                {
                    svigencia = "No Vigente";
                }
            }
            if (Txtcinfo_FeciniPromo.Text == "" || Txtcinfo_FecfinPromo.Text == "")
            {
                if (Txtcinfo_FeciniPromo.Text == "")
                {
                    cinfo_FeciniPromo = Convert.ToDateTime("01/01/1900");
                }
                if (Txtcinfo_FecfinPromo.Text == "")
                {
                    cinfo_FecfinPromo = Convert.ToDateTime("01/01/1900");
                }
            }
            cinfo_CodStrategy = Cmbcinfo_CodStrategy.SelectedValue;
            cinfo_Cod_Channel = Cmbcinfo_Cod_Channel.SelectedValue;
            cinfo_id_ProductCategory = Cmbcinfo_id_ProductCategory.SelectedValue;
            if (RbtnStatusActCom.Items[0].Selected == true)
            {
                bStatusActCom = true;

            }
            else
            {
                bStatusActCom = false;
            }
            if (Cmbcinfo_CodStrategy.SelectedValue == "0" || Cmbcinfo_Cod_Channel.SelectedValue == "0" || Cmbcinfo_id_ProductCategory.SelectedValue == "0")
            {
                if (Cmbcinfo_CodStrategy.SelectedValue == "0")
                {
                    cinfo_CodStrategy = "0";
                }
                else
                {
                    cinfo_CodStrategy = Cmbcinfo_CodStrategy.SelectedValue;
                }
                if (Cmbcinfo_Cod_Channel.SelectedValue == "0")
                {
                    cinfo_Cod_Channel = "0";

                }
                else
                {
                    cinfo_Cod_Channel = Cmbcinfo_Cod_Channel.SelectedValue;
                }
                if (Cmbcinfo_id_ProductCategory.SelectedValue == "0")
                {
                    cinfo_id_ProductCategory = "0";
                }
                else
                {
                    cinfo_id_ProductCategory = Cmbcinfo_id_ProductCategory.SelectedValue;
                }
            }
            if (Txtcinfo_PersonnelCantid.Text == "")
            {
                Txtcinfo_PersonnelCantid.Text = "0";
            }

            dupFechaEjec = this.Session["FechaEjec"].ToString().Trim();
            dupnameCompany = this.Session["nameCompany"].ToString().Trim();
            dupName_Activity = this.Session["Name_Activity"].ToString().Trim();

            if (dupFechaEjec != TxtFechaEjec.Text || dupnameCompany != Txtcinfo_nameCompany.Text ||
                dupName_Activity != Txtcinfo_Name_Activity.Text)
            {
                FechaEjec = Convert.ToDateTime(TxtFechaEjec.Text);
                DataTable DtDuplicados = Get_Operativo.Get_DuplicadosCompetition_Information("[Competition_ Information]", FechaEjec, Txtcinfo_nameCompany.Text, Txtcinfo_Name_Activity.Text, this.Session["id_Planning"].ToString().Trim());
                if (DtDuplicados != null)
                {
                    if (DtDuplicados.Rows.Count > 0)
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Registro Duplicado";
                        LblFaltantes.Text = "Sr. Usuario, la actividad registrada ya existe en este planning. Por favor verifique fecha de ejecución, empresa  y actividad.";
                        PopupMensajes();
                        return;
                    }
                    else
                    {
                        ECompetition__Information actividadCom = oCompetition__Information.ActualizarActividadCom(Convert.ToInt32(this.Session["cinfo"].ToString().Trim()), FechaEjec,
                            Txtcinfo_nameCompany.Text, Txtcinfo_Brand.Text, Convert.ToInt32(cinfo_CodStrategy), Txtcinfo_Name_Activity.Text,
                            cinfo_Cod_Channel, cinfo_FeciniPromo, cinfo_FecfinPromo, svigencia, Convert.ToInt32(Txtcinfo_PersonnelCantid.Text), Txtcinfo_Mecanica.Text,
                            cinfo_id_ProductCategory, Txtcinfo_Comment_Observa.Text, bStatusActCom, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        for (int i = 0; i <= ChkCinfo_id_MPointOfPurchase.Items.Count - 1; i++)
                        {
                            if (ChkCinfo_id_MPointOfPurchase.Items[i].Selected == true)
                            {
                                DataTable dt = Get_Operativo.Get_SearchInfoPOPActualCompetition_Information(Convert.ToInt32(this.Session["cinfo"].ToString().Trim()), Convert.ToInt32(ChkCinfo_id_MPointOfPurchase.Items[i].Value));
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        ECompetition__Information POPActividadCom = oCompetition__Information.ActualizarPOPActividadCom(Convert.ToInt32(this.Session["cinfo"].ToString().Trim()), Convert.ToInt32(ChkCinfo_id_MPointOfPurchase.Items[i].Value),
                                            ChkCinfo_id_MPointOfPurchase.Items[i].Selected, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                    }
                                    else
                                    {
                                        ECompetition__Information POPActividadCom = oCompetition__Information.RegistrarPOPActividadCom(Convert.ToInt32(this.Session["cinfo"].ToString().Trim()), Convert.ToInt32(ChkCinfo_id_MPointOfPurchase.Items[i].Value),
                                            ChkCinfo_id_MPointOfPurchase.Items[i].Selected, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                    }
                                }
                                dt = null;
                            }
                        }
                        for (int j = 0; j <= GVFotografias.Rows.Count - 1; j++)
                        {
                            string fn = System.IO.Path.GetFileName(((Image)GVFotografias.Rows[j].Cells[0].FindControl("Image1")).ImageUrl);

                            ECompetition__Information PhotoActividadCom = oCompetition__Information.ActualizarPhotoActividadCom(Convert.ToInt32(this.Session["cinfo"].ToString().Trim()), fn,
                                ((CheckBox)GVFotografias.Rows[j].Cells[0].FindControl("CheckBox1")).Checked, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        }
                    }
                }
                DtDuplicados = null;
            }
            else
            {
                FechaEjec = Convert.ToDateTime(TxtFechaEjec.Text);
                ECompetition__Information actividadCom = oCompetition__Information.ActualizarActividadCom(Convert.ToInt32(this.Session["cinfo"].ToString().Trim()), FechaEjec,
                            Txtcinfo_nameCompany.Text, Txtcinfo_Brand.Text, Convert.ToInt32(cinfo_CodStrategy), Txtcinfo_Name_Activity.Text,
                            cinfo_Cod_Channel, cinfo_FeciniPromo, cinfo_FecfinPromo, svigencia, Convert.ToInt32(Txtcinfo_PersonnelCantid.Text), Txtcinfo_Mecanica.Text,
                            cinfo_id_ProductCategory, Txtcinfo_Comment_Observa.Text, bStatusActCom, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                for (int i = 0; i <= ChkCinfo_id_MPointOfPurchase.Items.Count - 1; i++)
                {
                    DataTable dt = Get_Operativo.Get_SearchInfoPOPActualCompetition_Information(Convert.ToInt32(this.Session["cinfo"].ToString().Trim()), Convert.ToInt32(ChkCinfo_id_MPointOfPurchase.Items[i].Value));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            ECompetition__Information POPActividadCom = oCompetition__Information.ActualizarPOPActividadCom(Convert.ToInt32(this.Session["cinfo"].ToString().Trim()), Convert.ToInt32(ChkCinfo_id_MPointOfPurchase.Items[i].Value),
                                ChkCinfo_id_MPointOfPurchase.Items[i].Selected, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        }
                        else
                        {
                            ECompetition__Information POPActividadCom = oCompetition__Information.RegistrarPOPActividadCom(Convert.ToInt32(this.Session["cinfo"].ToString().Trim()), Convert.ToInt32(ChkCinfo_id_MPointOfPurchase.Items[i].Value),
                                ChkCinfo_id_MPointOfPurchase.Items[i].Selected, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        }
                    }
                    dt = null;
                }
                for (int j = 0; j <= GVFotografias.Rows.Count - 1; j++)
                {

                    string fn = System.IO.Path.GetFileName(((Image)GVFotografias.Rows[j].Cells[0].FindControl("Image1")).ImageUrl);

                    ECompetition__Information PhotoActividadCom = oCompetition__Information.ActualizarPhotoActividadCom(Convert.ToInt32(this.Session["cinfo"].ToString().Trim()), fn,
                        ((CheckBox)GVFotografias.Rows[j].Cells[0].FindControl("CheckBox1")).Checked, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                }
            }
            llena_ConsultarACTCom();

            TxtFechaEjec.Text = "";
            Txtcinfo_nameCompany.Text = "";
            Txtcinfo_Brand.Text = "";
            Txtcinfo_Name_Activity.Text = "";
            Txtcinfo_FeciniPromo.Text = "";
            Txtcinfo_Mecanica.Text = "";
            Txtcinfo_Comment_Observa.Text = "";
            Cmbcinfo_CodStrategy.Text = "0";
            Cmbcinfo_Cod_Channel.Text = "0";
            Txtcinfo_FecfinPromo.Text = "";
            Txtcinfo_PersonnelCantid.Text = "";
            Cmbcinfo_id_ProductCategory.Text = "0";

            for (int i = 0; i <= ChkCinfo_id_MPointOfPurchase.Items.Count - 1; i++)
            {
                ChkCinfo_id_MPointOfPurchase.Items[i].Selected = false;
            }


            for (int i = 0; i <= RBtncinfo_Vigente.Items.Count - 1; i++)
            {
                RBtncinfo_Vigente.Items[i].Selected = false;

            }

            TxtFechaEjec.Enabled = false;
            Txtcinfo_nameCompany.Enabled = false;
            Txtcinfo_Brand.Enabled = false;
            Txtcinfo_Name_Activity.Enabled = false;
            Txtcinfo_FeciniPromo.Enabled = false;
            RBtncinfo_Vigente.Enabled = false;
            Txtcinfo_Mecanica.Enabled = false;
            Txtcinfo_Comment_Observa.Enabled = false;
            Cmbcinfo_CodStrategy.Enabled = false;
            Cmbcinfo_Cod_Channel.Enabled = false;
            Txtcinfo_FecfinPromo.Enabled = false;
            Txtcinfo_PersonnelCantid.Enabled = false;
            Cmbcinfo_id_ProductCategory.Enabled = false;
            ChkCinfo_id_MPointOfPurchase.Enabled = false;
            RbtnStatusActCom.Enabled = false;
            ImgCalendar.Enabled = false;
            ImgCalendarFF.Enabled = false;
            ImgCalendarFI.Enabled = false;

            Alertas.CssClass = "MensajesSupConfirm";
            LblAlert.Text = "Actividad del comercio";
            LblFaltantes.Text = "Sr. Usuario, el registro se ha actualizado con éxito";
            PopupMensajes();

            ImgBtnFileUpload.Visible = true;
            ImgBtnFileUploadSearch.Visible = false;
            RbtnStatusActCom.Items[0].Selected = true;
            RbtnStatusActCom.Items[1].Selected = false;
            IbtnCrearOpe.Visible = true;
            IbtnSaveOpe.Visible = false;
            IbtnSearchOpe.Visible = true;
            IbtnEditOpe.Visible = false;
            IbtnActualizaOpe.Visible = false;
            IbtnCancelReg.Visible = true;
        }

        protected void ImgInfoFotos_Click(object sender, ImageClickEventArgs e)
        {
            TabCOperativo.Tabs[0].Enabled = true;
            TabCOperativo.Tabs[1].Enabled = true;
            TabCOperativo.Tabs[2].Enabled = true;
            TabCOperativo.Tabs[3].Enabled = false;
            TabCOperativo.Tabs[4].Enabled = false;
            TabCOperativo.Tabs[5].Enabled = false;
            TabCOperativo.Tabs[6].Enabled = false;
            TabCOperativo.Tabs[7].Enabled = true;
            TabCOperativo.Tabs[8].Enabled = true;
            TabCOperativo.ActiveTabIndex = 2;

            ifFotosActividad.Attributes["src"] = "CargaFotosAct.aspx";
        }





        //protected void GVFormaPropio_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    TabCOperativo.Tabs[0].Enabled = true;
        //    TabCOperativo.Tabs[1].Enabled = true;
        //    TabCOperativo.Tabs[2].Enabled = false;
        //    TabCOperativo.Tabs[3].Enabled = false;
        //    TabCOperativo.Tabs[4].Enabled = false;
        //    TabCOperativo.Tabs[5].Enabled = true;
        //    TabCOperativo.Tabs[6].Enabled = true;
        //    TabCOperativo.Tabs[7].Enabled = true;
        //    TabCOperativo.Tabs[8].Enabled = true;
        //    TabCOperativo.ActiveTabIndex = 5;

        //    // Formulario de captura de información propia
        //    LbltitDigita.Text = GVFormaPropio.SelectedRow.Cells[2].Text;
        //    DataTable dt = Get_Operativo.Get_SearchContenidoFormatosPropios(Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.SelectedRow.Cells[1].Text));
        //    this.Session["dtformatospropios"] = dt;

        //    DataTable dt1 = Get_Operativo.Get_SearchContenidoFormatosPropiosIndicador(Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.SelectedRow.Cells[3].Text));
        //    this.Session["dtformatospropiosindicador"] = dt1;
        //    GvAlmacenPlanning.DataSource = dt;
        //    GvAlmacenPlanning.DataBind();
        //    GvDatosIndicador.DataSource = dt1;
        //    GvDatosIndicador.DataBind();

        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int j = 0; j <= GvAlmacenPlanning.Rows.Count - 1; j++)
        //            {
        //                ((Label)GvAlmacenPlanning.Rows[j].Cells[0].FindControl("LblCaptura")).Text = dt.Rows[j]["Información Solicitada"].ToString().Trim().ToUpper();
        //            }
        //        }
        //    }
        //    if (dt1 != null)
        //    {
        //        if (dt1.Rows.Count > 0)
        //        {
        //            for (int j = 0; j <= GvDatosIndicador.Rows.Count - 1; j++)
        //            {
        //                ((Label)GvDatosIndicador.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
        //            }
        //        }
        //    }

        //    //creación de formulario de formatos propios
        //    for (int recorre = 0; recorre <= LstPlanningOp.Items.Count - 1; recorre++)
        //    {
        //        if (LstPlanningOp.Items[recorre].Selected == true)
        //        {
        //            conteo = conteo + 1;
        //        }
        //    }

        //    if (conteo > 0)
        //    {
        //        Label mylabelformato = new Label();
        //        mylabelformato.ID = "mylabelformato";
        //        mylabelformato.Text = GVFormaPropio.SelectedRow.Cells[2].Text;
        //        mylabelformato.Font.Bold = true;
        //        GridViewPlaceHolder.Controls.Add(mylabelformato);
        //        GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //        GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //        Label mylabelcampaña = new Label();
        //        mylabelcampaña.ID = "mylabelcampaña";
        //        mylabelcampaña.Text = GVPlanning.SelectedRow.Cells[2].Text;
        //        mylabelcampaña.Font.Bold = true;
        //        GridViewPlaceHolder.Controls.Add(mylabelcampaña);
        //        GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //        GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));

        //        for (int recorre = 0; recorre <= LstPlanningOp.Items.Count - 1; recorre++)
        //        {
        //            if (LstPlanningOp.Items[recorre].Selected == true)
        //            {
        //                Label mylabelopera = new Label();
        //                mylabelopera.ID = "MyLabelopera" + LstPlanningOp.Items[recorre].Value;
        //                mylabelopera.Text = "Mercaderista / Promotor : ";
        //                mylabelopera.Font.Bold = true;
        //                GridViewPlaceHolder.Controls.Add(mylabelopera);
        //                Label myLabel = new Label();
        //                myLabel.ID = "MyLabel" + LstPlanningOp.Items[recorre].Value;
        //                myLabel.Text = LstPlanningOp.Items[recorre].Text.ToUpper();
        //                myLabel.Font.Bold = true;
        //                GridViewPlaceHolder.Controls.Add(myLabel);
        //                GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //                DataTable dt2 = Get_Operativo.Get_FormatoActivPropiaEncabezado(Convert.ToInt32(this.Session["id_Planning"]));
        //                GridView myGrid = new GridView();
        //                myGrid.ID = "MyGrid" + LstPlanningOp.Items[recorre].Value;
        //                myGrid.Attributes.Add("runat", "server");
        //                myGrid.AutoGenerateColumns = true;
        //                myGrid.DataSource = dt2;
        //                myGrid.DataBind();
        //                myGrid.HeaderRow.Cells[0].Width = 300;
        //                myGrid.HeaderRow.Cells[1].Width = 700;
        //                GridViewPlaceHolder.Controls.Add(myGrid);
        //                GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));

        //                Table MyTable = new Table();
        //                MyTable.ID = "Mytable" + LstPlanningOp.Items[recorre].Value;
        //                TableRow tr = new TableRow();
        //                MyTable.Controls.Add(tr);
        //                DataTable dt5 = new DataTable();
        //                dt5 = oConn.ejecutarDataTable("UP_WEBSIGE_OPERATIVO_PDVXOPERATIVO", Convert.ToInt32(this.Session["id_Planning"]), LstPlanningOp.Items[recorre].Value);
        //                for (int i = 0; i <= dt5.Rows.Count - 1; i++)
        //                {
        //                    DataTable dt3 = Get_Operativo.Get_FormatoActivPropiaDetalle(Convert.ToInt32(GVFormaPropio.SelectedRow.Cells[4].Text), Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.SelectedRow.Cells[3].Text));
        //                    GridView myGrid1 = new GridView();
        //                    myGrid1.ID = "MyGrid1" + LstPlanningOp.Items[recorre].Value + i;
        //                    myGrid1.Caption = dt5.Rows[i]["pdv_Name"].ToString().Trim();
        //                    myGrid1.CaptionAlign = TableCaptionAlign.Left;
        //                    myGrid1.Font.Name = "Verdana";
        //                    myGrid1.Font.Size = 7;
        //                    myGrid1.RowStyle.Font.Name = "Verdana";
        //                    myGrid1.RowStyle.Font.Size = 7;
        //                    myGrid1.HeaderStyle.Font.Name = "Verdana";
        //                    myGrid1.HeaderStyle.Font.Size = 7;
        //                    myGrid1.HeaderStyle.Font.Bold = true;
        //                    myGrid1.Attributes.Add("runat", "server");
        //                    myGrid1.AutoGenerateColumns = true;

        //                    if (ocultar > 0)
        //                    {
        //                        dt3.Columns.Remove("Producto");
        //                    }
        //                    myGrid1.DataSource = dt3;
        //                    myGrid1.DataBind();
        //                    if (ocultar == 0)
        //                    {
        //                        myGrid1.Rows[0].Cells[0].Width = 400;
        //                    }
        //                    ocultar = ocultar + 1;
        //                    TableCell tc = new TableCell();
        //                    tc.VerticalAlign = VerticalAlign.Top;
        //                    tr.Controls.Add(tc);
        //                    tc.Controls.Add(myGrid1);
        //                    //myGrid1.Width = 1005;
        //                    ////GridViewPlaceHolder.Controls.Add(myGrid1);
        //                    GridViewPlaceHolder.Controls.Add(MyTable);
        //                }
        //                ocultar = 0;
        //                GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //                DataTable dt4 = Get_Operativo.Get_FormatoActivPropiaPie(Convert.ToInt32(this.Session["id_Planning"]));
        //                GridView myGrid2 = new GridView();
        //                myGrid2.ID = "MyGrid2" + LstPlanningOp.Items[recorre].Value;
        //                myGrid2.Attributes.Add("runat", "server");
        //                myGrid2.AutoGenerateColumns = true;
        //                myGrid2.DataSource = dt4;
        //                myGrid2.DataBind();
        //                myGrid2.HeaderRow.Cells[0].Width = 300;
        //                myGrid2.HeaderRow.Cells[1].Width = 700;
        //                GridViewPlaceHolder.Controls.Add(myGrid2);
        //                GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //                //TabCOperativo.Height = GridViewPlaceHolder.Height;
        //                this.Session["GridViewPlaceHolder"] = GridViewPlaceHolder;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //Label mylabelformato = new Label();
        //        //mylabelformato.ID = "mylabelformato";
        //        //mylabelformato.Text = GVFormaPropio.SelectedRow.Cells[2].Text;
        //        //mylabelformato.Font.Bold = true;
        //        //GridViewPlaceHolder.Controls.Add(mylabelformato);
        //        //GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //        //GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //        //Label mylabelcampaña = new Label();
        //        //mylabelcampaña.ID = "mylabelcampaña";
        //        //mylabelcampaña.Text = GVPlanning.SelectedRow.Cells[2].Text;
        //        //mylabelcampaña.Font.Bold = true;
        //        //GridViewPlaceHolder.Controls.Add(mylabelcampaña);
        //        //GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //        //GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //        //Label mylabelopera = new Label();
        //        //mylabelopera.ID = "MyLabelopera";
        //        //mylabelopera.Text = "Mercaderista / Promotor : ";
        //        //mylabelopera.Font.Bold = true;
        //        //GridViewPlaceHolder.Controls.Add(mylabelopera);
        //        //GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //        //DataTable dt2 = Get_Operativo.Get_FormatoActivPropiaEncabezado(Convert.ToInt32(this.Session["id_Planning"]));
        //        //GridView myGrid = new GridView();
        //        //myGrid.ID = "MyGrid";
        //        //myGrid.Attributes.Add("runat", "server");
        //        //myGrid.AutoGenerateColumns = true;
        //        //myGrid.DataSource = dt2;
        //        //myGrid.DataBind();
        //        //myGrid.HeaderRow.Cells[0].Width = 300;
        //        //myGrid.HeaderRow.Cells[1].Width = 700;
        //        //GridViewPlaceHolder.Controls.Add(myGrid);
        //        //GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //        //Table MyTable = new Table();
        //        //MyTable.ID = "Mytable";
        //        //TableRow tr = new TableRow();
        //        //MyTable.Controls.Add(tr);
        //        //DataTable dt3 = Get_Operativo.Get_FormatoActivPropiaDetalle(Convert.ToInt32(GVFormaPropio.SelectedRow.Cells[4].Text), Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.SelectedRow.Cells[3].Text));
        //        //GridView myGrid1 = new GridView();
        //        //myGrid1.ID = "MyGrid1";
        //        //myGrid1.Font.Name = "Verdana";
        //        //myGrid1.Font.Size = 7;
        //        //myGrid1.RowStyle.Font.Name = "Verdana";
        //        //myGrid1.RowStyle.Font.Size = 7;
        //        //myGrid1.HeaderStyle.Font.Name = "Verdana";
        //        //myGrid1.HeaderStyle.Font.Size = 7;
        //        //myGrid1.HeaderStyle.Font.Bold = true;
        //        //myGrid1.Attributes.Add("runat", "server");
        //        //myGrid1.AutoGenerateColumns = true;
        //        //myGrid1.DataSource = dt3;
        //        //myGrid1.DataBind();
        //        //myGrid1.Rows[0].Cells[0].Width = 400;
        //        //TableCell tc = new TableCell();
        //        //tc.VerticalAlign = VerticalAlign.Top;
        //        //tr.Controls.Add(tc);
        //        //tc.Controls.Add(myGrid1);
        //        //GridViewPlaceHolder.Controls.Add(MyTable);
        //        //GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //        //DataTable dt4 = Get_Operativo.Get_FormatoActivPropiaPie(Convert.ToInt32(this.Session["id_Planning"]));
        //        //GridView myGrid2 = new GridView();
        //        //myGrid2.ID = "MyGrid2";
        //        //myGrid2.Attributes.Add("runat", "server");
        //        //myGrid2.AutoGenerateColumns = true;
        //        //myGrid2.DataSource = dt4;
        //        //myGrid2.DataBind();
        //        //myGrid2.HeaderRow.Cells[0].Width = 300;
        //        //myGrid2.HeaderRow.Cells[1].Width = 700;
        //        //GridViewPlaceHolder.Controls.Add(myGrid2);
        //        //GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
        //        //this.Session["GridViewPlaceHolder"] = GridViewPlaceHolder;
        //    }


        //    //TabCOperativo.Tabs[0].Enabled = true;
        //    //TabCOperativo.Tabs[1].Enabled = true;
        //    //TabCOperativo.Tabs[2].Enabled = false;
        //    //TabCOperativo.Tabs[3].Enabled = false;
        //    //TabCOperativo.Tabs[4].Enabled = false;
        //    //TabCOperativo.Tabs[5].Enabled = true;
        //    //TabCOperativo.Tabs[6].Enabled = true;
        //    //TabCOperativo.Tabs[7].Enabled = true;
        //    //TabCOperativo.Tabs[8].Enabled = true;
        //    //TabCOperativo.ActiveTabIndex = 5;

        //    //LbltitDigita.Text = GVFormaPropio.SelectedRow.Cells[2].Text;
        //    //DataTable dt = Get_Operativo.Get_SearchContenidoFormatosPropios(Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.SelectedRow.Cells[1].Text));
        //    //this.Session["dtformatospropios"] = dt;

        //    //DataTable dt1 = Get_Operativo.Get_SearchContenidoFormatosPropiosIndicador(Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.SelectedRow.Cells[3].Text));
        //    //this.Session["dtformatospropiosindicador"] = dt1;
        //    //GvAlmacenPlanning.DataSource = dt;
        //    //GvAlmacenPlanning.DataBind();
        //    //GvDatosIndicador.DataSource = dt1;
        //    //GvDatosIndicador.DataBind();

        //    //if (dt != null)
        //    //{
        //    //    if (dt.Rows.Count > 0)
        //    //    {
        //    //        for (int j = 0; j <= GvAlmacenPlanning.Rows.Count - 1; j++)
        //    //        {
        //    //            ((Label)GvAlmacenPlanning.Rows[j].Cells[0].FindControl("LblCaptura")).Text = dt.Rows[j]["Información Solicitada"].ToString().Trim().ToUpper();
        //    //        }
        //    //    }
        //    //}
        //    //if (dt1 != null)
        //    //{
        //    //    if (dt1.Rows.Count > 0)
        //    //    {
        //    //        for (int j = 0; j <= GvDatosIndicador.Rows.Count - 1; j++)
        //    //        {
        //    //            ((Label)GvDatosIndicador.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
        //    //        }
        //    //    }
        //    //}

        //    ////Se alimenta la grilla de formato propia del planning
        //    //DataTable operativos = oConn.ejecutarDataTable("UP_WEBSIGE_OPERATIVO_OBTENERSTAFFPLANNINGSEL", Convert.ToInt32(CmbPlanningForm.Text));
        //    //GVFormatoGeneral.DataSource = operativos;
        //    //GVFormatoGeneral.DataBind();
        //    //operativos = null;




        //    //for (int recorre = 0; recorre <= LstPlanningOp.Items.Count - 1; recorre++)
        //    //{
        //    //    if (LstPlanningOp.Items[recorre].Selected == true)
        //    //    {
        //    //        conteo = conteo + 1;

        //    //    }
        //    //}

        //    //if (conteo > 0)
        //    //{
        //    //    for (int recorre = 0; recorre <= LstPlanningOp.Items.Count - 1; recorre++)
        //    //    {
        //    //        if (LstPlanningOp.Items[recorre].Selected == true)
        //    //        {

        //    //            ((Label)GVFormatoGeneral.Rows[recorre].Cells[0].FindControl("lblopform")).Text = LstPlanningOp.Items[recorre].Text.ToUpper();
        //    //            DataTable dt2 = Get_Operativo.Get_FormatoActivPropiaEncabezado(Convert.ToInt32(this.Session["id_Planning"]));
        //    //            ((GridView)GVFormatoGeneral.Rows[recorre].Cells[0].FindControl("GVEncabezado")).DataSource = dt2;
        //    //            ((GridView)GVFormatoGeneral.Rows[recorre].Cells[0].FindControl("GVEncabezado")).DataBind();
        //    //            //GVEncabezado.DataSource = dt2;
        //    //            //GVEncabezado.DataBind();
        //    //            if (dt2 != null)
        //    //            {
        //    //                if (dt2.Rows.Count > 0)
        //    //                {
        //    //                    for (int j = 0; j <= ((GridView)GVFormatoGeneral.Rows[recorre].Cells[0].FindControl("GVEncabezado")).Rows.Count - 1; j++)
        //    //                    {
        //    //                        ((Label)((GridView)GVFormatoGeneral.Rows[recorre].Cells[0].FindControl("GVEncabezado")).Rows[j].Cells[0].FindControl("LblItemEncabezado")).Text = dt2.Rows[j]["Item_Description"].ToString().Trim().ToUpper();
        //    //                        //((Label)GVEncabezado.Rows[j].Cells[0].FindControl("LblItemEncabezado")).Text = dt2.Rows[j]["Item_Description"].ToString().Trim().ToUpper();
        //    //                        string x = dt2.Rows[j]["Item_Description"].ToString().Trim().ToUpper();
        //    //                    }
        //    //                }
        //    //            }
        //    //            DataTable dt3 = Get_Operativo.Get_FormatoActivPropiaDetalle(Convert.ToInt32(GVFormaPropio.SelectedRow.Cells[4].Text), Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.SelectedRow.Cells[3].Text));
        //    //            ((GridView)GVFormatoGeneral.Rows[recorre].Cells[0].FindControl("GVDetalle")).DataSource = dt3;
        //    //            ((GridView)GVFormatoGeneral.Rows[recorre].Cells[0].FindControl("GVDetalle")).DataBind();
        //    //            //GVDetalle.DataSource = dt3;
        //    //            //GVDetalle.DataBind();

        //    //            DataTable dt4 = Get_Operativo.Get_FormatoActivPropiaPie(Convert.ToInt32(this.Session["id_Planning"]));
        //    //            ((GridView)GVFormatoGeneral.Rows[recorre].Cells[0].FindControl("GvPie")).DataSource = dt4;
        //    //            ((GridView)GVFormatoGeneral.Rows[recorre].Cells[0].FindControl("GvPie")).DataBind();
        //    //            //GvPie.DataSource = dt4;
        //    //            //GvPie.DataBind();
        //    //            if (dt4 != null)
        //    //            {
        //    //                if (dt4.Rows.Count > 0)
        //    //                {
        //    //                    for (int j = 0; j <= ((GridView)GVFormatoGeneral.Rows[recorre].Cells[0].FindControl("GvPie")).Rows.Count - 1; j++)
        //    //                    {
        //    //                        ((Label)((GridView)GVFormatoGeneral.Rows[recorre].Cells[0].FindControl("GvPie")).Rows[j].Cells[0].FindControl("LblItemPie")).Text = dt4.Rows[j]["Item_Description"].ToString().Trim().ToUpper();
        //    //                        //((Label)GvPie.Rows[j].Cells[0].FindControl("LblItemPie")).Text = dt4.Rows[j]["Item_Description"].ToString().Trim().ToUpper();
        //    //                    }
        //    //                }
        //    //            }
        //    //            dt2 = null;
        //    //            dt3 = null;
        //    //            dt4 = null;
        //    //        }
        //    //        else
        //    //        {
        //    //            GVFormatoGeneral.Rows[recorre].Cells[0].Visible = false;
        //    //            ((Label)GVFormatoGeneral.Rows[recorre].Cells[0].FindControl("LblNamePromotor")).Text = "";

        //    //        }


        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    ((Label)GVFormatoGeneral.Rows[0].Cells[0].FindControl("lblopform")).Text = "";
        //    //    DataTable dt2 = Get_Operativo.Get_FormatoActivPropiaEncabezado(Convert.ToInt32(this.Session["id_Planning"]));
        //    //    ((GridView)GVFormatoGeneral.Rows[0].Cells[0].FindControl("GVEncabezado")).DataSource = dt2;
        //    //    ((GridView)GVFormatoGeneral.Rows[0].Cells[0].FindControl("GVEncabezado")).DataBind();
        //    //    //GVEncabezado.DataSource = dt2;
        //    //    //GVEncabezado.DataBind();
        //    //    if (dt2 != null)
        //    //    {
        //    //        if (dt2.Rows.Count > 0)
        //    //        {
        //    //            for (int j = 0; j <= ((GridView)GVFormatoGeneral.Rows[0].Cells[0].FindControl("GVEncabezado")).Rows.Count - 1; j++)
        //    //            {
        //    //                ((Label)((GridView)GVFormatoGeneral.Rows[0].Cells[0].FindControl("GVEncabezado")).Rows[j].Cells[0].FindControl("LblItemEncabezado")).Text = dt2.Rows[j]["Item_Description"].ToString().Trim().ToUpper();
        //    //                //((Label)GVEncabezado.Rows[j].Cells[0].FindControl("LblItemEncabezado")).Text = dt2.Rows[j]["Item_Description"].ToString().Trim().ToUpper();
        //    //                string x = dt2.Rows[j]["Item_Description"].ToString().Trim().ToUpper();
        //    //            }
        //    //        }
        //    //    }
        //    //    DataTable dt3 = Get_Operativo.Get_FormatoActivPropiaDetalle(Convert.ToInt32(GVFormaPropio.SelectedRow.Cells[4].Text), Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.SelectedRow.Cells[3].Text));
        //    //    ((GridView)GVFormatoGeneral.Rows[0].Cells[0].FindControl("GVDetalle")).DataSource = dt3;
        //    //    ((GridView)GVFormatoGeneral.Rows[0].Cells[0].FindControl("GVDetalle")).DataBind();
        //    //    //GVDetalle.DataSource = dt3;
        //    //    //GVDetalle.DataBind();

        //    //    DataTable dt4 = Get_Operativo.Get_FormatoActivPropiaPie(Convert.ToInt32(this.Session["id_Planning"]));
        //    //    ((GridView)GVFormatoGeneral.Rows[0].Cells[0].FindControl("GvPie")).DataSource = dt4;
        //    //    ((GridView)GVFormatoGeneral.Rows[0].Cells[0].FindControl("GvPie")).DataBind();
        //    //    //GvPie.DataSource = dt4;
        //    //    //GvPie.DataBind();
        //    //    if (dt4 != null)
        //    //    {
        //    //        if (dt4.Rows.Count > 0)
        //    //        {
        //    //            for (int j = 0; j <= ((GridView)GVFormatoGeneral.Rows[0].Cells[0].FindControl("GvPie")).Rows.Count - 1; j++)
        //    //            {
        //    //                ((Label)((GridView)GVFormatoGeneral.Rows[0].Cells[0].FindControl("GvPie")).Rows[j].Cells[0].FindControl("LblItemPie")).Text = dt4.Rows[j]["Item_Description"].ToString().Trim().ToUpper();
        //    //                //((Label)GvPie.Rows[j].Cells[0].FindControl("LblItemPie")).Text = dt4.Rows[j]["Item_Description"].ToString().Trim().ToUpper();
        //    //            }
        //    //        }
        //    //    }
        //    //    dt2 = null;
        //    //    dt3 = null;
        //    //    dt4 = null;
        //    //    for (int recorre = 1; recorre <= LstPlanningOp.Items.Count; recorre++)
        //    //    {
        //    //        GVFormatoGeneral.Rows[recorre].Cells[0].Visible = false;
        //    //    }
        //    //}

        //    ////opeform = null;
        //    //TabCOperativo.Height = 0;
        //    ////TabCOperativo.Height = (GVEncabezado.Rows.Count * 30) + (GVDetalle.Rows.Count * 30) + (GvPie.Rows.Count * 30) + 400;
        //    //dt = null;
        //    //dt1 = null;   
        //}

        //Exportar informacion a excel 
        protected void ImgExpExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DirectoryInfo DIR = new DirectoryInfo(Server.MapPath("PictureComercio" + "\\" + this.Session["sUser"].ToString()));

                this.Session["DIR"] = DIR;
                if (!DIR.Exists)
                {
                    DIR.Create();
                }
                for (int i = 0; i <= GvPersonalExcel.Rows.Count - 1; i++)
                {
                    personalExel = personalExel + GvPersonalExcel.Rows[i].Cells[0].Text + ";";
                    personalExel = personalExel + GvPersonalExcel.Rows[i].Cells[1].Text + "\r\n";

                }
                personalExel = personalExel.Replace("&#209;", "n");
                personalExel = personalExel.Replace("&#193;", "a");
                //  const string ope = @"\Operativos.csv";
                string ope = DIR + "\\" + "Operativos.csv";

                FileCSV filecsvope = new FileCSV();
                filecsvope.crearArchivo(ope, personalExel);

                //string remoteuri = Server.MapPath("PictureComercio") + "\\" + "Operativos.csv";                
                //string filename = "Operativos.csv";
                //WebClient Client = new WebClient();
                //Client.DownloadFile(remoteuri, filename);
                //          String dlDir = @"PictureComercio/";
                //String path =  Server.MapPath(dlDir + filename); 
                //System.IO.FileInfo toDownload =
                //             new System.IO.FileInfo(path); 
                //if (toDownload.Exists)
                //{
                //   Response.Clear(); 
                //   Response.AddHeader("Content-Disposition",
                //              "attachment; filename=" + toDownload.Name);
                //   Response.AddHeader("Content-Length", 
                //              toDownload.Length.ToString());
                //   Response.ContentType = "text/HTML";
                //   Response.WriteFile(dlDir + filename);
                //   Response.End();
                //}    

                for (int i = 0; i <= GvPDVVsPersonalExcel.Rows.Count - 1; i++)
                {
                    PDVVsPersonalExcel = PDVVsPersonalExcel + GvPDVVsPersonalExcel.Rows[i].Cells[0].Text + ";";
                    PDVVsPersonalExcel = PDVVsPersonalExcel + GvPDVVsPersonalExcel.Rows[i].Cells[1].Text + ";";
                    PDVVsPersonalExcel = PDVVsPersonalExcel + GvPDVVsPersonalExcel.Rows[i].Cells[2].Text + "\r\n";

                }
                PDVVsPersonalExcel = PDVVsPersonalExcel.Replace("&#193;", "a");
                PDVVsPersonalExcel = PDVVsPersonalExcel.Replace("&#201;", "e");
                PDVVsPersonalExcel = PDVVsPersonalExcel.Replace("&#205;", "i");
                PDVVsPersonalExcel = PDVVsPersonalExcel.Replace("&#211;", "o");
                PDVVsPersonalExcel = PDVVsPersonalExcel.Replace("&#218;", "u");
                PDVVsPersonalExcel = PDVVsPersonalExcel.Replace("&#225;", "a");
                PDVVsPersonalExcel = PDVVsPersonalExcel.Replace("&#233;", "e");
                PDVVsPersonalExcel = PDVVsPersonalExcel.Replace("&#237;", "i");
                PDVVsPersonalExcel = PDVVsPersonalExcel.Replace("&#243;", "o");
                PDVVsPersonalExcel = PDVVsPersonalExcel.Replace("&#250;", "u");
                PDVVsPersonalExcel = PDVVsPersonalExcel.Replace("&#209;", "n");
                PDVVsPersonalExcel = PDVVsPersonalExcel.Replace("&#241;", "n");
                string pdv = DIR + "\\" + "PDV.csv";
                //const string pdv = @"c:\fli\PDV.csv";
                FileCSV filecsvpdv = new FileCSV();
                filecsvpdv.crearArchivo(pdv, PDVVsPersonalExcel);

                for (int i = 0; i <= GvProductoXPDVVsPersonalExcel.Rows.Count - 1; i++)
                {
                    ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel + GvProductoXPDVVsPersonalExcel.Rows[i].Cells[0].Text + ";";
                    ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel + GvProductoXPDVVsPersonalExcel.Rows[i].Cells[1].Text + ";";
                    ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel + GvProductoXPDVVsPersonalExcel.Rows[i].Cells[3].Text + "\r\n";
                    DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_OPERATIVO_PRODUCTOCOMPETIDORXPRODUCTOPROPIOSEL", Convert.ToInt32(GvProductoXPDVVsPersonalExcel.Rows[i].Cells[1].Text));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int j = 0; j <= dt.Rows.Count - 1; j++)
                            {
                                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel + GvProductoXPDVVsPersonalExcel.Rows[i].Cells[0].Text + ";";
                                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel + GvProductoXPDVVsPersonalExcel.Rows[i].Cells[1].Text + "-" + dt.Rows[j][0].ToString().Trim() + ";";
                                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel + dt.Rows[j][1].ToString().Trim() + "\r\n";
                            }
                        }
                    }

                }
                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel.Replace("&#193;", "a");
                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel.Replace("&#201;", "e");
                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel.Replace("&#205;", "i");
                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel.Replace("&#211;", "o");
                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel.Replace("&#218;", "u");
                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel.Replace("&#225;", "a");
                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel.Replace("&#233;", "e");
                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel.Replace("&#237;", "i");
                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel.Replace("&#243;", "o");
                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel.Replace("&#250;", "u");
                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel.Replace("&#209;", "n");
                ProductoXPDVVsPersonalExcel = ProductoXPDVVsPersonalExcel.Replace("&#241;", "n");

                string productos = DIR + "\\" + "Product.csv";
                //const string productos = @"c:\fli\Product.csv";
                FileCSV filecsvprod = new FileCSV();
                filecsvprod.crearArchivo(productos, ProductoXPDVVsPersonalExcel);

                for (int i = 0; i <= GvPersonalExcel.Rows.Count - 1; i++)
                {
                    DataTable dtmarcas = oConn.ejecutarDataTable("UP_WEBSIGE_OPERATIVO_MARCASPRODUCTOSXPDVXOPERATIVOSEL", Convert.ToInt32(GvPersonalExcel.Rows[i].Cells[0].Text));
                    if (dtmarcas != null)
                    {
                        if (dtmarcas.Rows.Count > 0)
                        {
                            for (int j = 0; j <= dtmarcas.Rows.Count - 1; j++)
                            {
                                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel + GvPersonalExcel.Rows[i].Cells[0].Text + ";";
                                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel + dtmarcas.Rows[j]["id_Brand"].ToString().Trim() + ";";
                                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel + dtmarcas.Rows[j]["Name_Brand"].ToString().Trim() + "\r\n";
                                DataTable dtmarcascomp = oConn.ejecutarDataTable("UP_WEBSIGE_OPERATIVO_MARCASCOMPEPRODUCTOSXPDVXOPERATIVOSEL", Convert.ToInt32(GvPersonalExcel.Rows[i].Cells[0].Text), Convert.ToInt32(dtmarcas.Rows[j]["id_Brand"].ToString().Trim()));

                                if (dtmarcascomp != null)
                                {
                                    if (dtmarcascomp.Rows.Count > 0)
                                    {
                                        for (int k = 0; k <= dtmarcascomp.Rows.Count - 1; k++)
                                        {
                                            MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel + GvPersonalExcel.Rows[i].Cells[0].Text + ";";
                                            MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel + dtmarcas.Rows[j]["id_Brand"].ToString().Trim() + "-C" + dtmarcascomp.Rows[k]["id_producCompe"].ToString().Trim() + ";";
                                            MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel + dtmarcascomp.Rows[k]["Brand_Compe"].ToString().Trim() + "\r\n";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel.Replace("&#193;", "a");
                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel.Replace("&#201;", "e");
                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel.Replace("&#205;", "i");
                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel.Replace("&#211;", "o");
                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel.Replace("&#218;", "u");
                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel.Replace("&#225;", "a");
                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel.Replace("&#233;", "e");
                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel.Replace("&#237;", "i");
                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel.Replace("&#243;", "o");
                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel.Replace("&#250;", "u");
                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel.Replace("&#209;", "n");
                MarcasXPDVVsPersonalExcel = MarcasXPDVVsPersonalExcel.Replace("&#241;", "n");
                string marcas = DIR + "\\" + "Marca.csv";
                //const string marcas = @"c:\fli\Marca.csv";
                FileCSV filecsvmarcas = new FileCSV();
                filecsvprod.crearArchivo(marcas, MarcasXPDVVsPersonalExcel);

                for (int i = 0; i <= GVFormaPropio.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= GvPersonalExcel.Rows.Count - 1; j++)
                    {
                        DataTable dtcanal = Get_Operativo.Get_CanalXFormato(GVPlanning.SelectedRow.Cells[8].Text);

                        string treporte = GVFormaPropio.Rows[i].Cells[2].Text + dtcanal.Rows[0][0].ToString().Trim();
                        if (treporte == "171241" || treporte == "211000" || treporte == "211023" || treporte == "211241" || treporte == "221000" || treporte == "221023" || treporte == "221241")
                        {
                            FormatoEncExcel = FormatoEncExcel + GVFormaPropio.Rows[i].Cells[2].Text + dtcanal.Rows[0][0].ToString().Trim() + ";";
                            FormatoEncExcel = FormatoEncExcel + GvPersonalExcel.Rows[j].Cells[0].Text + ";";
                            FormatoEncExcel = FormatoEncExcel + GVPlanning.SelectedRow.Cells[2].Text + ";";
                            FormatoEncExcel = FormatoEncExcel + GvPersonalExcel.Rows[j].Cells[1].Text + ";";
                            for (int k = 0; k <= GvFormatoEncExcel.Rows.Count - 1; k++)
                            {
                                FormatoEncExcel = FormatoEncExcel + GvFormatoEncExcel.Rows[k].Cells[1].Text + ";";
                            }
                            int completarenc = 8 - (GvFormatoEncExcel.Rows.Count - 1);
                            for (int p = 0; p < completarenc - 1; p++)
                            {
                                FormatoEncExcel = FormatoEncExcel + " ";
                                if (p < completarenc - 2)
                                {
                                    FormatoEncExcel = FormatoEncExcel + ";";
                                }
                            }

                            FormatoEncExcel = FormatoEncExcel + "\r\n";


                        }

                    }
                }
                FormatoEncExcel = FormatoEncExcel.Replace("&#193;", "a");
                FormatoEncExcel = FormatoEncExcel.Replace("&#201;", "e");
                FormatoEncExcel = FormatoEncExcel.Replace("&#205;", "i");
                FormatoEncExcel = FormatoEncExcel.Replace("&#211;", "o");
                FormatoEncExcel = FormatoEncExcel.Replace("&#218;", "u");
                FormatoEncExcel = FormatoEncExcel.Replace("&#225;", "a");
                FormatoEncExcel = FormatoEncExcel.Replace("&#233;", "e");
                FormatoEncExcel = FormatoEncExcel.Replace("&#237;", "i");
                FormatoEncExcel = FormatoEncExcel.Replace("&#243;", "o");
                FormatoEncExcel = FormatoEncExcel.Replace("&#250;", "u");
                FormatoEncExcel = FormatoEncExcel.Replace("&#209;", "n");
                FormatoEncExcel = FormatoEncExcel.Replace("&#241;", "n");

                FormatoEncExcel = FormatoEncExcel.Remove(FormatoEncExcel.Length - 3, 3);



                string encabezado = DIR + "\\" + "encabezado.csv";
                //const string encabezado = @"c:\fli\encabezado.csv";
                FileCSV filecsvenc = new FileCSV();
                filecsvenc.crearArchivo(encabezado, FormatoEncExcel);

                for (int i = 0; i <= GVFormaPropio.Rows.Count - 1; i++)
                {
                    DataTable dtcanal = Get_Operativo.Get_CanalXFormato(GVPlanning.SelectedRow.Cells[8].Text);
                    string preporte = GVFormaPropio.Rows[i].Cells[2].Text + dtcanal.Rows[0][0].ToString().Trim();
                    if (preporte == "171241" || preporte == "211000" || preporte == "211023" || preporte == "211241" || preporte == "221000" || preporte == "221023" || preporte == "221241")
                    {
                        FormatoPieExcel = FormatoPieExcel + GVFormaPropio.Rows[i].Cells[2].Text + dtcanal.Rows[0][0].ToString().Trim() + ";";


                        for (int k = 0; k <= GvFormatoPieExcel.Rows.Count - 1; k++)
                        {
                            FormatoPieExcel = FormatoPieExcel + GvFormatoPieExcel.Rows[k].Cells[1].Text + ";";
                        }
                        int completar = 10 - (GvFormatoPieExcel.Rows.Count - 1);
                        for (int j = 0; j < completar - 1; j++)
                        {
                            FormatoPieExcel = FormatoPieExcel + " ";
                            if (j < completar - 2)
                            {
                                FormatoPieExcel = FormatoPieExcel + ";";
                            }
                        }
                        FormatoPieExcel = FormatoPieExcel + "\r\n";


                    }
                }
                FormatoPieExcel = FormatoPieExcel.Replace("&#193;", "a");
                FormatoPieExcel = FormatoPieExcel.Replace("&#201;", "e");
                FormatoPieExcel = FormatoPieExcel.Replace("&#205;", "i");
                FormatoPieExcel = FormatoPieExcel.Replace("&#211;", "o");
                FormatoPieExcel = FormatoPieExcel.Replace("&#218;", "u");
                FormatoPieExcel = FormatoPieExcel.Replace("&#225;", "a");
                FormatoPieExcel = FormatoPieExcel.Replace("&#233;", "e");
                FormatoPieExcel = FormatoPieExcel.Replace("&#237;", "i");
                FormatoPieExcel = FormatoPieExcel.Replace("&#243;", "o");
                FormatoPieExcel = FormatoPieExcel.Replace("&#250;", "u");
                FormatoPieExcel = FormatoPieExcel.Replace("&#209;", "n");
                FormatoPieExcel = FormatoPieExcel.Replace("&#241;", "n");
                string pie = DIR + "\\" + "pie.csv";
                //const string pie = @"c:\fli\pie.csv";
                FileCSV filecsvpie = new FileCSV();
                filecsvpie.crearArchivo(pie, FormatoPieExcel);

                LkbPuntosDeVenta.Visible = true;
                LkbEncabezado.Visible = true;
                LkbPie.Visible = true;
                LkbProductos.Visible = true;
                LkbMarcas.Visible = true;

                //Downloadenc.Visible = true;
                //Downloadpie.Visible = true;
                //Downloadpro.Visible = true;
                //Downloadpdv.Visible = true;
                //Downloadmar.Visible = true;
                OpenExcel.Visible = true;


                Alertas.CssClass = "MensajesSupConfirm";
                LblAlert.Text = "OpenExcel !!";
                LblFaltantes.Text = "Sr. Usuario, descarge los archivos para la construcción de los formatos e inmediatamente despues ejecute la aplicación OpenExcel";
                PopupMensajes();

                //abrir una aplicacion de escritorio 
                //try
                //{
                //   // string Progfilename = @"C:\Archivos de programa\Proyecto1\ProtoCliente.exe";
                //    string Progfilename = Server.MapPath("Archivos de programa") + (@"\ProtoCliente.exe");
                //    Process proc = new Process();
                //    proc.EnableRaisingEvents = false;
                //    proc.StartInfo.FileName = Progfilename;
                //    proc.Start();
                //}
                //catch(Exception ex)
                //{
                //    Alertas.CssClass = "MensajesSupervisor";
                //    LblAlert.Text = "OpenExcel !!";
                //    LblFaltantes.Text = "Sr. Usuario, esta intentando enlazar con OpenExcel pero no esta instalado. Por favor consulte con el Administrador de la aplicación" + ex;
                //    PopupMensajes();
                //}
            }

            catch (Exception ex)
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Error inesperado!!";
                LblFaltantes.Text = "Sr. Usuario. no fue posible generar los ficheros por falta de información. Por favor consulte al administrador de la aplicación";
                PopupMensajes();
            }


            //Este codigo es para abrir un archivo de excel con el contenido deseado

            //StringBuilder sb = new StringBuilder();
            //StringWriter sw = new StringWriter(sb);
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //Page pagina = new Page();
            //HtmlForm form = new HtmlForm();
            //string prueba = this.Session["GridViewPlaceHolder"].ToString();
            //form.Controls.Add((Panel)this.Session["GridViewPlaceHolder"]);
            //pagina.EnableEventValidation = false;
            //pagina.DesignerInitialize();
            //pagina.Controls.Add(form);
            //pagina.RenderControl(hw);
            //Response.Clear();
            //Response.Buffer = true;

            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            //Response.Charset = "UTF-8";

            //Response.ContentEncoding = Encoding.Default;
            //Response.Write(sb.ToString());
            //Response.End();
        }

        //-- Description:       <Permite Crear una Nueva actividad propia Pais Ing. Mauricio Ortiz>
        //-- Requerimiento No.  <>
        //-- =============================================
        protected void BtnContinuarTip_Click(object sender, EventArgs e)
        {
            TabVentas.Visible = false;
            TabPrecios.Visible = false;
            TabCobertura.Visible = false;
            TabSOD.Visible = false;

            IbtnCrearCaptura.Visible = false;
            IbtnSaveCaptura.Visible = true;
            IbtnSaveCaptura.Enabled = true;
            IbtnSearchCaptura.Visible = false;
            IbtnEditCaptura.Visible = false;
            IbtnActualizaCaptura.Visible = false;

            CmbOperaDigita.Enabled = true;
            TxtFecOperaDigita.Enabled = true;
            ImgBtnFecOperaDigita.Enabled = true;
            CmbPdvDigita.Enabled = true;
            CmbProdDigita.Enabled = true;

           

            TabCOperativo.Tabs[0].Enabled = false;
            TabCOperativo.Tabs[1].Enabled = false;
            TabCOperativo.Tabs[2].Enabled = false;
            TabCOperativo.Tabs[3].Enabled = false;
            TabCOperativo.Tabs[4].Enabled = false;
            TabCOperativo.Tabs[5].Enabled = false;
            TabCOperativo.Tabs[6].Enabled = true;
            TabCOperativo.Tabs[7].Enabled = false;
            TabCOperativo.Tabs[8].Enabled = false;
            Paneldigitacion.Style.Value = "display:block;";
            PanelTipoDigitacion.Style.Value = "display:none;";

            GvAlmacenPlanning.Enabled = true;
            GvDatosIndicadorVentas.Enabled = true;
            GvDatosIndicadorPrecios.Enabled = true;
            GvDatosIndicadorSOD.Enabled = true;
            GvDatosIndicadorCobertura.Enabled = true;

            GvVentasCompe1.Enabled = true;
            GvVentasCompe2.Enabled = true;
            GvVentasCompe3.Enabled = true;
            GvPreciosCompe1.Enabled = true;
            GvPreciosCompe2.Enabled = true;
            GvPreciosCompe3.Enabled = true;
            GvCoberturaCompe1.Enabled = true;
            GvCoberturaCompe2.Enabled = true;
            GvCoberturaCompe3.Enabled = true;
            GvSODCompe1.Enabled = true;
            GvSODCompe2.Enabled = true;
            GvSODCompe3.Enabled = true;

            for (int i = 0; i <= ChkTipDigitacion.Items.Count - 1; i++)
            {
                if (ChkTipDigitacion.Items[i].Value == "17" && ChkTipDigitacion.Items[i].Selected == true)
                {
                    TabVentas.Visible = true;
                }
                if (ChkTipDigitacion.Items[i].Value == "19" && ChkTipDigitacion.Items[i].Selected == true)
                {
                    TabPrecios.Visible = true;
                }
                if (ChkTipDigitacion.Items[i].Value == "22" && ChkTipDigitacion.Items[i].Selected == true)
                {
                    TabCobertura.Visible = true;
                }
                if (ChkTipDigitacion.Items[i].Value == "21" && ChkTipDigitacion.Items[i].Selected == true)
                {
                    TabSOD.Visible = true;
                }
            }
        }

        protected void IbtnSaveCaptura_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (TxtFecOperaDigita.Text != "")
                {
                    FecOperaDigita = Convert.ToDateTime(TxtFecOperaDigita.Text.ToString());
                }
            }
            catch
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Parametros Incorrectos";
                LblFaltantes.Text = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
                PopupMensajes();
                return;
            }

            if (TxtFecOperaDigita.Text == "" || CmbOperaDigita.Text == "0" || CmbPdvDigita.Text == "0" || CmbProdDigita.Text == "0" || CmbOperaDigita.Text == "" || CmbPdvDigita.Text == "" || CmbProdDigita.Text == "")
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Parámetros obligatorio (*)";
                LblFaltantes.Text = "Todos los campos marcados con (*) deben estar digitados. Por favor verifique";
                PopupMensajes();
                return;
            }

            for (int j = 0; j <= GvAlmacenPlanning.Rows.Count - 1; j++)
            {
                if (((TextBox)GvAlmacenPlanning.Rows[j].Cells[0].FindControl("TxtCaptura")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la Información General";
                    PopupMensajes();
                    return;
                }
            }

            if (TabVentas.Visible == true)
            {
                for (int j = 0; j <= GvDatosIndicadorVentas.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvDatosIndicadorVentas.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en Ventas";
                        PopupMensajes();
                        return;
                    }
                }

                for (int j = 0; j <= GvVentasCompe1.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvVentasCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en Ventas";
                        PopupMensajes();
                        return;
                    }
                }
                for (int j = 0; j <= GvVentasCompe2.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvVentasCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en Ventas";
                        PopupMensajes();
                        return;
                    }
                }

                for (int j = 0; j <= GvVentasCompe3.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvVentasCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en Ventas";
                        PopupMensajes();
                        return;
                    }
                }
            }



            if (TabPrecios.Visible == true)
            {


                for (int j = 0; j <= GvDatosIndicadorPrecios.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvDatosIndicadorPrecios.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en Precios";
                        PopupMensajes();
                        return;
                    }
                }

                for (int j = 0; j <= GvPreciosCompe1.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvPreciosCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en Precios";
                        PopupMensajes();
                        return;
                    }
                }

                for (int j = 0; j <= GvPreciosCompe2.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvPreciosCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en Precios";
                        PopupMensajes();
                        return;
                    }
                }

                for (int j = 0; j <= GvPreciosCompe3.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvPreciosCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en Precios";
                        PopupMensajes();
                        return;
                    }
                }
            }


            if (TabSOD.Visible == true)
            {
                for (int j = 0; j <= GvDatosIndicadorSOD.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvDatosIndicadorSOD.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en SOD";
                        PopupMensajes();
                        return;
                    }
                }

                for (int j = 0; j <= GvSODCompe1.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvSODCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en SOD";
                        PopupMensajes();
                        return;
                    }
                }
                for (int j = 0; j <= GvSODCompe2.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvSODCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en SOD";
                        PopupMensajes();
                        return;
                    }
                }
                for (int j = 0; j <= GvSODCompe3.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvSODCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en SOD";
                        PopupMensajes();
                        return;
                    }
                }
            }


            if (TabCobertura.Visible == true)
            {


                for (int j = 0; j <= GvDatosIndicadorCobertura.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvDatosIndicadorCobertura.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en Disponibilidad";
                        PopupMensajes();
                        return;
                    }
                }

                for (int j = 0; j <= GvCoberturaCompe1.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvCoberturaCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en Disponibilidad";
                        PopupMensajes();
                        return;
                    }
                }

                for (int j = 0; j <= GvCoberturaCompe2.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvCoberturaCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en Disponibilidad";
                        PopupMensajes();
                        return;
                    }
                }

                for (int j = 0; j <= GvCoberturaCompe3.Rows.Count - 1; j++)
                {
                    if (((TextBox)GvCoberturaCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                    {
                        Alertas.CssClass = "MensajesSupervisor";
                        LblAlert.Text = "Parámetros obligatorio (*)";
                        LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información en Disponibilidad";
                        PopupMensajes();
                        return;
                    }
                }
            }

            ///verificar duplicidad 
            DataTable dtduplicado = oConn.ejecutarDataTable("UP_WEBSIGE_OPERATIVO_SEARCHDUPLICADOINFOLEVANTAMIENTOPROPIA", Convert.ToInt32(this.Session["id_Planning"]), Convert.ToString(FecOperaDigita), CmbPdvDigita.SelectedItem.ToString(), CmbProdDigita.SelectedItem.ToString());
            if (dtduplicado.Rows.Count - 1 > 0)
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Registro Duplicado";
                LblFaltantes.Text = "Sr. Usuario, ya existen un registro de este producto en este punto de venta para el operativo seleccionado. Por favor verifique";
                PopupMensajes();
                return;

            }
            dtduplicado = null;

            if (GvAlmacenPlanning.Rows.Count > 0)
            {
                for (int j = 0; j <= GvAlmacenPlanning.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropios"];
                    EAlmacenDetalle_Planning oeAlmacenDetalle_Planning = AlmacenDetalle_Planning.RegistrarDatosCalle(Convert.ToInt32(dt.Rows[j][0].ToString().Trim()), this.Session["id_Planning"].ToString().Trim(), Convert.ToInt32(CmbOperaDigita.Text),
                        Convert.ToInt32(CmbPdvDigita.Text), Convert.ToInt32(CmbProdDigita.Text), FecOperaDigita,
                        ((TextBox)GvAlmacenPlanning.Rows[j].Cells[0].FindControl("TxtCaptura")).Text, Convert.ToInt32(this.Session["NumSemana"].ToString().Trim()), true,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    dt = null;
                }
            }

            if (TabVentas.Visible == true)
            {



                if (GvDatosIndicadorVentas.Rows.Count > 0)
                {
                    Query = "INSERT INTO [Sales_Planning]([id_Planning],";
                    for (int j = 0; j <= GvDatosIndicadorVentas.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador17"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metasales],[Person_id],[id_MPOSPlanning],[id_ProductsPlanning],[sales_Date],[weekNo],[sales_CreateBy],[sales_DateBy],[sales_ModiBy],[sales_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvDatosIndicadorVentas.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(CmbProdDigita.Text) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [Sales_Planning]([id_Planning],";
                    }
                }

                if (GvVentasCompe1.Rows.Count > 0)
                {
                    DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                    Query = "INSERT INTO [Sales_Planning]([id_Planning],";
                    for (int j = 0; j <= GvVentasCompe1.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador17"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metasales],[Person_id],[id_MPOSPlanning],[id_producCompe],[sales_Date],[weekNo],[sales_CreateBy],[sales_DateBy],[sales_ModiBy],[sales_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvVentasCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(prodcompetenciadigitar.Rows[0][0].ToString().Trim()) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [Sales_Planning]([id_Planning],";
                    }
                    prodcompetenciadigitar = null;
                }

                if (GvVentasCompe2.Rows.Count > 0)
                {
                    DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                    Query = "INSERT INTO [Sales_Planning]([id_Planning],";
                    for (int j = 0; j <= GvVentasCompe2.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador17"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metasales],[Person_id],[id_MPOSPlanning],[id_producCompe],[sales_Date],[weekNo],[sales_CreateBy],[sales_DateBy],[sales_ModiBy],[sales_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvVentasCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(prodcompetenciadigitar.Rows[1][0].ToString().Trim()) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [Sales_Planning]([id_Planning],";
                    }
                    prodcompetenciadigitar = null;
                }

                if (GvVentasCompe3.Rows.Count > 0)
                {
                    DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                    Query = "INSERT INTO [Sales_Planning]([id_Planning],";
                    for (int j = 0; j <= GvVentasCompe3.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador17"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metasales],[Person_id],[id_MPOSPlanning],[id_producCompe],[sales_Date], [weekNo],[sales_CreateBy],[sales_DateBy],[sales_ModiBy],[sales_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvVentasCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(prodcompetenciadigitar.Rows[2][0].ToString().Trim()) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [Sales_Planning]([id_Planning],";
                    }
                    prodcompetenciadigitar = null;
                }
            }

            if (TabPrecios.Visible == true)
            {

                if (GvDatosIndicadorPrecios.Rows.Count > 0)
                {
                    Query = "INSERT INTO [Prices_Planning]([id_Planning],";
                    for (int j = 0; j <= GvDatosIndicadorPrecios.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador19"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metaprices],[Person_id],[id_MPOSPlanning],[id_ProductsPlanning],[price_Date],[weekNo],[prices_CreateBy],[prices_DateBy],[prices_ModiBy],[prices_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvDatosIndicadorPrecios.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(CmbProdDigita.Text) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [Prices_Planning]([id_Planning],";
                    }
                }

                if (GvPreciosCompe1.Rows.Count > 0)
                {
                    DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                    Query = "INSERT INTO [Prices_Planning]([id_Planning],";
                    for (int j = 0; j <= GvPreciosCompe1.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador19"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metaprices],[Person_id],[id_MPOSPlanning],[id_producCompe],[price_Date],[weekNo],[prices_CreateBy],[prices_DateBy],[prices_ModiBy],[prices_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvPreciosCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(prodcompetenciadigitar.Rows[0][0].ToString().Trim()) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [Prices_Planning]([id_Planning],";
                    }
                    prodcompetenciadigitar = null;
                }

                if (GvPreciosCompe2.Rows.Count > 0)
                {
                    DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                    Query = "INSERT INTO [Prices_Planning]([id_Planning],";
                    for (int j = 0; j <= GvPreciosCompe2.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador19"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metaprices],[Person_id],[id_MPOSPlanning],[id_producCompe],[price_Date],[weekNo],[prices_CreateBy],[prices_DateBy],[prices_ModiBy],[prices_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvPreciosCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(prodcompetenciadigitar.Rows[1][0].ToString().Trim()) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [Prices_Planning]([id_Planning],";
                    }
                    prodcompetenciadigitar = null;
                }

                if (GvPreciosCompe3.Rows.Count > 0)
                {
                    DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                    Query = "INSERT INTO [Prices_Planning]([id_Planning],";
                    for (int j = 0; j <= GvPreciosCompe3.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador19"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metaprices],[Person_id],[id_MPOSPlanning],[id_producCompe],[price_Date],[weekNo],[prices_CreateBy],[prices_DateBy],[prices_ModiBy],[prices_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvPreciosCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(prodcompetenciadigitar.Rows[3][0].ToString().Trim()) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [Prices_Planning]([id_Planning],";
                    }
                    prodcompetenciadigitar = null;
                }
            }

            if (TabSOD.Visible == true)
            {
                if (GvDatosIndicadorSOD.Rows.Count > 0)
                {
                    Query = "INSERT INTO [SpaceMeasurement_Planning]([id_Planning],";
                    for (int j = 0; j <= GvDatosIndicadorSOD.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador21"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metaspace],[Person_id],[id_MPOSPlanning],[id_ProductsPlanning],[SpaceMeasurement_Date],[weekNo],[SpaceMeasurement_CreateBy],[SpaceMeasurement_DateBy],[SpaceMeasurement_ModiBy],[SpaceMeasurement_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvDatosIndicadorSOD.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(CmbProdDigita.Text) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [SpaceMeasurement_Planning]([id_Planning],";
                    }
                }

                if (GvSODCompe1.Rows.Count > 0)
                {
                    DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                    Query = "INSERT INTO [SpaceMeasurement_Planning]([id_Planning],";
                    for (int j = 0; j <= GvSODCompe1.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador21"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metaspace],[Person_id],[id_MPOSPlanning],[id_producCompe],[SpaceMeasurement_Date],[weekNo],[SpaceMeasurement_CreateBy],[SpaceMeasurement_DateBy],[SpaceMeasurement_ModiBy],[SpaceMeasurement_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvSODCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(prodcompetenciadigitar.Rows[0][0].ToString().Trim()) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [SpaceMeasurement_Planning]([id_Planning],";
                    }
                    prodcompetenciadigitar = null;
                }

                if (GvSODCompe2.Rows.Count > 0)
                {
                    DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                    Query = "INSERT INTO [SpaceMeasurement_Planning]([id_Planning],";
                    for (int j = 0; j <= GvSODCompe2.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador21"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metaspace],[Person_id],[id_MPOSPlanning],[id_producCompe],[SpaceMeasurement_Date], [weekNo],[SpaceMeasurement_CreateBy],[SpaceMeasurement_DateBy],[SpaceMeasurement_ModiBy],[SpaceMeasurement_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvSODCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(prodcompetenciadigitar.Rows[1][0].ToString().Trim()) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [SpaceMeasurement_Planning]([id_Planning],";
                    }
                    prodcompetenciadigitar = null;
                }

                if (GvSODCompe3.Rows.Count > 0)
                {
                    DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                    Query = "INSERT INTO [SpaceMeasurement_Planning]([id_Planning],";
                    for (int j = 0; j <= GvSODCompe3.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador21"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metaspace],[Person_id],[id_MPOSPlanning],[id_producCompe],[SpaceMeasurement_Date],[weekNo],[SpaceMeasurement_CreateBy],[SpaceMeasurement_DateBy],[SpaceMeasurement_ModiBy],[SpaceMeasurement_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvSODCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(prodcompetenciadigitar.Rows[2][0].ToString().Trim()) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [SpaceMeasurement_Planning]([id_Planning],";
                    }
                    prodcompetenciadigitar = null;
                }
            }

            if (TabCobertura.Visible == true)
            {
                if (GvDatosIndicadorCobertura.Rows.Count > 0)
                {
                    Query = "INSERT INTO [Coverage_Planning]([id_Planning],";
                    for (int j = 0; j <= GvDatosIndicadorCobertura.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador22"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metacoverage],[Person_id],[id_MPOSPlanning],[id_ProductsPlanning],[Coverage_Date],[weekNo],[Coverage_CreateBy],[Coverage_DateBy],[Coverage_ModiBy],[Coverage_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvDatosIndicadorCobertura.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(CmbProdDigita.Text) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [Coverage_Planning]([id_Planning],";
                    }
                }

                if (GvCoberturaCompe1.Rows.Count > 0)
                {
                    DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                    Query = "INSERT INTO [Coverage_Planning]([id_Planning],";
                    for (int j = 0; j <= GvCoberturaCompe1.Rows.Count - 1; j++)
                    {

                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador22"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metacoverage],[Person_id],[id_MPOSPlanning],[id_producCompe],[Coverage_Date], [weekNo],[Coverage_CreateBy],[Coverage_DateBy],[Coverage_ModiBy],[Coverage_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvCoberturaCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(prodcompetenciadigitar.Rows[0][0].ToString().Trim()) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [Coverage_Planning]([id_Planning],";
                    }
                    prodcompetenciadigitar = null;
                }

                if (GvCoberturaCompe2.Rows.Count > 0)
                {
                    DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                    Query = "INSERT INTO [Coverage_Planning]([id_Planning],";
                    for (int j = 0; j <= GvCoberturaCompe2.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador22"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metacoverage],[Person_id],[id_MPOSPlanning],[id_producCompe],[Coverage_Date],[weekNo],[Coverage_CreateBy],[Coverage_DateBy],[Coverage_ModiBy],[Coverage_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvCoberturaCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(prodcompetenciadigitar.Rows[1][0].ToString().Trim()) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [Coverage_Planning]([id_Planning],";
                    }
                    prodcompetenciadigitar = null;
                }

                if (GvCoberturaCompe3.Rows.Count > 0)
                {
                    DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
                    Query = "INSERT INTO [Coverage_Planning]([id_Planning],";
                    for (int j = 0; j <= GvCoberturaCompe3.Rows.Count - 1; j++)
                    {
                        DataTable dt = (DataTable)this.Session["dtformatospropiosindicador22"];
                        Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "]" + ",[id_metacoverage],[Person_id],[id_MPOSPlanning],[id_producCompe],[Coverage_Date], [weekNo],[Coverage_CreateBy],[Coverage_DateBy],[Coverage_ModiBy],[Coverage_DateModiBy]) VALUES (" +
                            Convert.ToInt32(this.Session["id_Planning"]) + "," + "'" + ((TextBox)GvCoberturaCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'" + "," + Convert.ToInt32(dt.Rows[j][0].ToString().Trim()) + "," + Convert.ToInt32(CmbOperaDigita.Text) + "," +
                            Convert.ToInt32(CmbPdvDigita.Text) + "," + Convert.ToInt32(prodcompetenciadigitar.Rows[2][0].ToString().Trim()) + "," + "'" + TxtFecOperaDigita.Text + "'" + "," + "'" + Convert.ToInt32(this.Session["NumSemana"]) + "'" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + "," + "'" + Convert.ToString(this.Session["sUser"]) + "'" + "," + "getdate()" + ")";
                        oConn.ejecutarQuery(Query);
                        Query = "INSERT INTO [Coverage_Planning]([id_Planning],";
                    }
                    prodcompetenciadigitar = null;
                }
            }

            Alertas.CssClass = "MensajesSupConfirm";
            LblAlert.Text = LbltitDigita.Text;
            LblFaltantes.Text = "Sr. Usuario, el registro se ha guardado con éxito";
            PopupMensajes();
            llena_ConsultarACTLevPropio();
            IbtnCrearCaptura.Visible = true;
            IbtnSaveCaptura.Visible = false;
            IbtnSearchCaptura.Visible = true;
            IbtnEditCaptura.Visible = false;
            IbtnActualizaCaptura.Visible = false;
            IbtnCancelCaptura.Visible = true;


            CmbOperaDigita.Enabled = false;
            TxtFecOperaDigita.Enabled = false;
            ImgBtnFecOperaDigita.Enabled = false;
            CmbPdvDigita.Enabled = false;
            CmbProdDigita.Enabled = false;
            CmbProdDigita.Items.Remove(CmbProdDigita.SelectedItem);

            for (int j = 0; j <= GvAlmacenPlanning.Rows.Count - 1; j++)
            {
                ((TextBox)GvAlmacenPlanning.Rows[j].Cells[0].FindControl("TxtCaptura")).Text = "";
            }

            for (int j = 0; j <= GvDatosIndicadorVentas.Rows.Count - 1; j++)
            {
                ((TextBox)GvDatosIndicadorVentas.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvVentasCompe1.Rows.Count - 1; j++)
            {
                ((TextBox)GvVentasCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }
            for (int j = 0; j <= GvVentasCompe2.Rows.Count - 1; j++)
            {
                ((TextBox)GvVentasCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvVentasCompe3.Rows.Count - 1; j++)
            {
                ((TextBox)GvVentasCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvDatosIndicadorPrecios.Rows.Count - 1; j++)
            {
                ((TextBox)GvDatosIndicadorPrecios.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvPreciosCompe1.Rows.Count - 1; j++)
            {
                ((TextBox)GvPreciosCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvPreciosCompe2.Rows.Count - 1; j++)
            {
                ((TextBox)GvPreciosCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvPreciosCompe3.Rows.Count - 1; j++)
            {
                ((TextBox)GvPreciosCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvDatosIndicadorSOD.Rows.Count - 1; j++)
            {
                ((TextBox)GvDatosIndicadorSOD.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvSODCompe1.Rows.Count - 1; j++)
            {
                ((TextBox)GvSODCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }
            for (int j = 0; j <= GvSODCompe2.Rows.Count - 1; j++)
            {
                ((TextBox)GvSODCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }
            for (int j = 0; j <= GvSODCompe3.Rows.Count - 1; j++)
            {
                ((TextBox)GvSODCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvDatosIndicadorCobertura.Rows.Count - 1; j++)
            {
                ((TextBox)GvDatosIndicadorCobertura.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvCoberturaCompe1.Rows.Count - 1; j++)
            {
                ((TextBox)GvCoberturaCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvCoberturaCompe2.Rows.Count - 1; j++)
            {
                ((TextBox)GvCoberturaCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvCoberturaCompe3.Rows.Count - 1; j++)
            {
                ((TextBox)GvCoberturaCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            GvAlmacenPlanning.Enabled = false;
            GvDatosIndicadorVentas.Enabled = false;
            GvDatosIndicadorPrecios.Enabled = false;
            GvDatosIndicadorSOD.Enabled = false;
            GvDatosIndicadorCobertura.Enabled = false;
            GvVentasCompe1.Enabled = false;
            GvVentasCompe2.Enabled = false;
            GvVentasCompe3.Enabled = false;
            GvPreciosCompe1.Enabled = false;
            GvPreciosCompe2.Enabled = false;
            GvPreciosCompe3.Enabled = false;
            GvCoberturaCompe1.Enabled = false;
            GvCoberturaCompe2.Enabled = false;
            GvCoberturaCompe3.Enabled = false;
            GvSODCompe1.Enabled = false;
            GvSODCompe2.Enabled = false;
            GvSODCompe3.Enabled = false;

        }


        protected void IbtnEditCaptura_Click(object sender, ImageClickEventArgs e)
        {
            IbtnEditCaptura.Visible = false;
            IbtnActualizaCaptura.Visible = true;

            CmbOperaDigita.Enabled = false;
            TxtFecOperaDigita.Enabled = false;
            ImgBtnFecOperaDigita.Enabled = false;
            CmbPdvDigita.Enabled = false;
            CmbProdDigita.Enabled = false;
            RbtnStatusActPropia.Enabled = true;

            GvAlmacenPlanning.Enabled = true;
            GvDatosIndicadorVentas.Enabled = true;
            GvDatosIndicadorPrecios.Enabled = true;
            GvDatosIndicadorSOD.Enabled = true;
            GvDatosIndicadorCobertura.Enabled = true;

            GvVentasCompe1.Enabled = true;
            GvVentasCompe2.Enabled = true;
            GvVentasCompe3.Enabled = true;
            GvPreciosCompe1.Enabled = true;
            GvPreciosCompe2.Enabled = true;
            GvPreciosCompe3.Enabled = true;
            GvCoberturaCompe1.Enabled = true;
            GvCoberturaCompe2.Enabled = true;
            GvCoberturaCompe3.Enabled = true;
            GvSODCompe1.Enabled = true;
            GvSODCompe2.Enabled = true;
            GvSODCompe3.Enabled = true;

        }
        protected void IbtnActualizaCaptura_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (TxtFecOperaDigita.Text != "")
                {
                    FecOperaDigita = Convert.ToDateTime(TxtFecOperaDigita.Text.ToString());
                }
            }
            catch
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Parametros Incorrectos";
                LblFaltantes.Text = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
                PopupMensajes();
                return;
            }

            if (TxtFecOperaDigita.Text == "" || CmbOperaDigita.Text == "0" || CmbPdvDigita.Text == "0" || CmbOperaDigita.Text == "" || CmbPdvDigita.Text == "")
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Parámetros obligatorio (*)";
                LblFaltantes.Text = "Todos los campos marcados con (*) deben estar digitados. Por favor verifique";
                PopupMensajes();
                return;
            }


            for (int j = 0; j <= GvAlmacenPlanning.Rows.Count - 1; j++)
            {
                if (((TextBox)GvAlmacenPlanning.Rows[j].Cells[0].FindControl("TxtCaptura")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada";
                    PopupMensajes();
                    return;

                }
            }


            for (int j = 0; j <= GvDatosIndicadorVentas.Rows.Count - 1; j++)
            {
                if (((TextBox)GvDatosIndicadorVentas.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Ventas";
                    PopupMensajes();
                    return;
                }
            }

            for (int j = 0; j <= GvVentasCompe1.Rows.Count - 1; j++)
            {
                if (((TextBox)GvVentasCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Ventas";
                    PopupMensajes();
                    return;
                }
            }

            for (int j = 0; j <= GvVentasCompe2.Rows.Count - 1; j++)
            {
                if (((TextBox)GvVentasCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Ventas";
                    PopupMensajes();
                    return;
                }
            }

            for (int j = 0; j <= GvVentasCompe3.Rows.Count - 1; j++)
            {
                if (((TextBox)GvVentasCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Ventas";
                    PopupMensajes();
                    return;
                }
            }

            for (int j = 0; j <= GvDatosIndicadorPrecios.Rows.Count - 1; j++)
            {
                if (((TextBox)GvDatosIndicadorPrecios.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Precios";
                    PopupMensajes();
                    return;
                }
            }
            for (int j = 0; j <= GvPreciosCompe1.Rows.Count - 1; j++)
            {
                if (((TextBox)GvPreciosCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Precios";
                    PopupMensajes();
                    return;
                }
            }
            for (int j = 0; j <= GvPreciosCompe2.Rows.Count - 1; j++)
            {
                if (((TextBox)GvPreciosCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Precios";
                    PopupMensajes();
                    return;
                }
            }
            for (int j = 0; j <= GvPreciosCompe3.Rows.Count - 1; j++)
            {
                if (((TextBox)GvPreciosCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Precios";
                    PopupMensajes();
                    return;
                }
            }
            for (int j = 0; j <= GvDatosIndicadorCobertura.Rows.Count - 1; j++)
            {
                if (((TextBox)GvDatosIndicadorCobertura.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Disponibilidad";
                    PopupMensajes();
                    return;
                }
            }
            for (int j = 0; j <= GvCoberturaCompe1.Rows.Count - 1; j++)
            {
                if (((TextBox)GvCoberturaCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Precios";
                    PopupMensajes();
                    return;
                }
            }
            for (int j = 0; j <= GvCoberturaCompe2.Rows.Count - 1; j++)
            {
                if (((TextBox)GvCoberturaCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Precios";
                    PopupMensajes();
                    return;
                }
            }
            for (int j = 0; j <= GvCoberturaCompe2.Rows.Count - 1; j++)
            {
                if (((TextBox)GvCoberturaCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Precios";
                    PopupMensajes();
                    return;
                }
            }

            for (int j = 0; j <= GvDatosIndicadorSOD.Rows.Count - 1; j++)
            {
                if (((TextBox)GvDatosIndicadorSOD.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de SOD";
                    PopupMensajes();
                    return;
                }
            }
            for (int j = 0; j <= GvSODCompe1.Rows.Count - 1; j++)
            {
                if (((TextBox)GvSODCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Precios";
                    PopupMensajes();
                    return;
                }
            }
            for (int j = 0; j <= GvSODCompe2.Rows.Count - 1; j++)
            {
                if (((TextBox)GvSODCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Precios";
                    PopupMensajes();
                    return;
                }
            }
            for (int j = 0; j <= GvSODCompe3.Rows.Count - 1; j++)
            {
                if (((TextBox)GvSODCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text == "")
                {
                    Alertas.CssClass = "MensajesSupervisor";
                    LblAlert.Text = "Parámetros obligatorio (*)";
                    LblFaltantes.Text = "Sr. Usuario, debe ingresar toda la información solicitada de Precios";
                    PopupMensajes();
                    return;
                }
            }

            if (RbtnStatusActPropia.Items[0].Selected == true)
            {
                bStatusActPropia = true;

            }
            else
            {
                bStatusActPropia = false;
            }

            for (int j = 0; j <= GvAlmacenPlanning.Rows.Count - 1; j++)
            {
                DataTable dt = (DataTable)this.Session["Get_SearchInfoLevantamientoPropia"];

                EAlmacenDetalle_Planning oeAlmacenDetalle_Planning = AlmacenDetalle_Planning.ActualizarDatosCalle(Convert.ToInt32(dt.Rows[j]["id_almacenDetalle"].ToString().Trim()), Convert.ToInt32(CmbOperaDigita.Text),
                    Convert.ToInt32(CmbPdvDigita.Text), Convert.ToInt32(CmbProdDigita.Text), FecOperaDigita, ((TextBox)GvAlmacenPlanning.Rows[j].Cells[0].FindControl("TxtCaptura")).Text, Convert.ToInt32(this.Session["NumSemana"]), bStatusActPropia, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                dt = null;
            }

            DataTable prodcompetenciadigitar = (DataTable)this.Session["prodcompetenciadigitar"];
            if (GvDatosIndicadorVentas.Rows.Count > 0)
            {
                //17
                Query = "UPDATE [Sales_Planning] SET ";
                for (int j = 0; j <= GvDatosIndicadorVentas.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador17"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador17"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvDatosIndicadorVentas.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_ProductsPlanning] = '" + CmbProdDigita.Text +
                        "', [sales_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [sales_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [sales_DateModiBy] = " +
                        "getdate() WHERE (id_sales = " + dt2.Rows[j]["id_sales"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [Sales_Planning] SET ";
                }
            }

            if (GvVentasCompe1.Rows.Count > 0)
            {
                //17
                Query = "UPDATE [Sales_Planning] SET ";
                for (int j = 0; j <= GvVentasCompe1.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador170"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador170"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvVentasCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_producCompe] = '" + prodcompetenciadigitar.Rows[0][0].ToString().Trim() +
                        "', [sales_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [sales_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [sales_DateModiBy] = " +
                        "getdate() WHERE (id_sales = " + dt2.Rows[j]["id_sales"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [Sales_Planning] SET ";
                }
            }

            if (GvVentasCompe2.Rows.Count > 0)
            {
                //17
                Query = "UPDATE [Sales_Planning] SET ";
                for (int j = 0; j <= GvVentasCompe2.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador171"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador171"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvVentasCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_producCompe] = '" + prodcompetenciadigitar.Rows[1][0].ToString().Trim() +
                        "', [sales_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [sales_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [sales_DateModiBy] = " +
                        "getdate() WHERE (id_sales = " + dt2.Rows[j]["id_sales"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [Sales_Planning] SET ";
                }
            }

            if (GvVentasCompe3.Rows.Count > 0)
            {
                //17
                Query = "UPDATE [Sales_Planning] SET ";
                for (int j = 0; j <= GvVentasCompe3.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador172"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador172"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvVentasCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_producCompe] = '" + prodcompetenciadigitar.Rows[2][0].ToString().Trim() +
                        "', [sales_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [sales_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [sales_DateModiBy] = " +
                        "getdate() WHERE (id_sales = " + dt2.Rows[j]["id_sales"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [Sales_Planning] SET ";
                }
            }


            if (GvDatosIndicadorPrecios.Rows.Count > 0)
            {
                //19
                Query = "UPDATE [Prices_Planning] SET ";
                for (int j = 0; j <= GvDatosIndicadorPrecios.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador19"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador19"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvDatosIndicadorPrecios.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_ProductsPlanning] = '" + CmbProdDigita.Text +
                        "', [price_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [prices_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [prices_DateModiBy] = " +
                        "getdate() WHERE (id_prices = " + dt2.Rows[j]["id_prices"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [Prices_Planning] SET ";
                }
            }

            if (GvPreciosCompe1.Rows.Count > 0)
            {
                //19
                Query = "UPDATE [Prices_Planning] SET ";
                for (int j = 0; j <= GvPreciosCompe1.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador190"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador190"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvPreciosCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_producCompe] = '" + prodcompetenciadigitar.Rows[0][0].ToString().Trim() +
                        "', [price_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [prices_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [prices_DateModiBy] = " +
                        "getdate() WHERE (id_prices = " + dt2.Rows[j]["id_prices"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [Prices_Planning] SET ";
                }
            }

            if (GvPreciosCompe2.Rows.Count > 0)
            {
                //19
                Query = "UPDATE [Prices_Planning] SET ";
                for (int j = 0; j <= GvPreciosCompe2.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador191"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador191"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvPreciosCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_producCompe] = '" + prodcompetenciadigitar.Rows[1][0].ToString().Trim() +
                        "', [price_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [prices_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [prices_DateModiBy] = " +
                        "getdate() WHERE (id_prices = " + dt2.Rows[j]["id_prices"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [Prices_Planning] SET ";
                }
            }

            if (GvPreciosCompe3.Rows.Count > 0)
            {
                //19
                Query = "UPDATE [Prices_Planning] SET ";
                for (int j = 0; j <= GvPreciosCompe3.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador192"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador192"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvPreciosCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_producCompe] = '" + prodcompetenciadigitar.Rows[2][0].ToString().Trim() +
                        "', [price_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [prices_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [prices_DateModiBy] = " +
                        "getdate() WHERE (id_prices = " + dt2.Rows[j]["id_prices"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [Prices_Planning] SET ";
                }
            }

            if (GvDatosIndicadorSOD.Rows.Count > 0)
            {
                //21
                Query = "UPDATE [SpaceMeasurement_Planning] SET ";
                for (int j = 0; j <= GvDatosIndicadorSOD.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador21"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador21"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvDatosIndicadorSOD.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_ProductsPlanning] = '" + CmbProdDigita.Text +
                        "', [SpaceMeasurement_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [SpaceMeasurement_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [SpaceMeasurement_DateModiBy] = " +
                        "getdate() WHERE (id_SpaceMeasurement = " + dt2.Rows[j]["id_SpaceMeasurement"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [SpaceMeasurement_Planning] SET ";
                }
            }

            if (GvSODCompe1.Rows.Count > 0)
            {
                //21
                Query = "UPDATE [SpaceMeasurement_Planning] SET ";
                for (int j = 0; j <= GvSODCompe1.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador210"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador210"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvSODCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_producCompe] = '" + prodcompetenciadigitar.Rows[0][0].ToString().Trim() +
                        "', [SpaceMeasurement_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [SpaceMeasurement_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [SpaceMeasurement_DateModiBy] = " +
                        "getdate() WHERE (id_SpaceMeasurement = " + dt2.Rows[j]["id_SpaceMeasurement"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [SpaceMeasurement_Planning] SET ";
                }
            }

            if (GvSODCompe2.Rows.Count > 0)
            {
                //21
                Query = "UPDATE [SpaceMeasurement_Planning] SET ";
                for (int j = 0; j <= GvSODCompe2.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador211"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador211"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvSODCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_producCompe] = '" + prodcompetenciadigitar.Rows[1][0].ToString().Trim() +
                        "', [SpaceMeasurement_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [SpaceMeasurement_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [SpaceMeasurement_DateModiBy] = " +
                        "getdate() WHERE (id_SpaceMeasurement = " + dt2.Rows[j]["id_SpaceMeasurement"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [SpaceMeasurement_Planning] SET ";
                }
            }

            if (GvSODCompe3.Rows.Count > 0)
            {
                //21
                Query = "UPDATE [SpaceMeasurement_Planning] SET ";
                for (int j = 0; j <= GvSODCompe3.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador212"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador212"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvSODCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_producCompe] = '" + prodcompetenciadigitar.Rows[2][0].ToString().Trim() +
                        "', [SpaceMeasurement_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [SpaceMeasurement_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [SpaceMeasurement_DateModiBy] = " +
                        "getdate() WHERE (id_SpaceMeasurement = " + dt2.Rows[j]["id_SpaceMeasurement"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [SpaceMeasurement_Planning] SET ";
                }
            }

            if (GvDatosIndicadorCobertura.Rows.Count > 0)
            {
                //22
                Query = "UPDATE [Coverage_Planning] SET ";
                for (int j = 0; j <= GvDatosIndicadorCobertura.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador22"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador22"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvDatosIndicadorCobertura.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_ProductsPlanning] = '" + CmbProdDigita.Text +
                        "', [Coverage_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [Coverage_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [Coverage_DateModiBy] = " +
                        "getdate() WHERE (id_Coverage = " + dt2.Rows[j]["id_Coverage"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [Coverage_Planning] SET ";
                }
            }

            if (GvCoberturaCompe1.Rows.Count > 0)
            {
                //22
                Query = "UPDATE [Coverage_Planning] SET ";
                for (int j = 0; j <= GvCoberturaCompe1.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador220"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador220"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvCoberturaCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_producCompe] = '" + prodcompetenciadigitar.Rows[0][0].ToString().Trim() +
                        "', [Coverage_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [Coverage_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [Coverage_DateModiBy] = " +
                        "getdate() WHERE (id_Coverage = " + dt2.Rows[j]["id_Coverage"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [Coverage_Planning] SET ";
                }
            }

            if (GvCoberturaCompe2.Rows.Count > 0)
            {
                //22
                Query = "UPDATE [Coverage_Planning] SET ";
                for (int j = 0; j <= GvCoberturaCompe2.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador221"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador221"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvCoberturaCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_producCompe] = '" + prodcompetenciadigitar.Rows[1][0].ToString().Trim() +
                        "', [Coverage_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [Coverage_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [Coverage_DateModiBy] = " +
                        "getdate() WHERE (id_Coverage = " + dt2.Rows[j]["id_Coverage"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [Coverage_Planning] SET ";
                }
            }

            if (GvCoberturaCompe3.Rows.Count > 0)
            {
                //22
                Query = "UPDATE [Coverage_Planning] SET ";
                for (int j = 0; j <= GvCoberturaCompe3.Rows.Count - 1; j++)
                {
                    DataTable dt = (DataTable)this.Session["dtformatospropiosindicador222"];
                    DataTable dt2 = (DataTable)this.Session["Get_SearchInfoContenidoFormatosPropiosIndicador222"];
                    Query = Query + "[" + dt.Rows[j][2].ToString().Trim() + "] = " + "'" + ((TextBox)GvCoberturaCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text + "'," +
                        "[Person_id] = '" + CmbOperaDigita.Text + "', [id_MPOSPlanning] = '" + CmbPdvDigita.Text + "', [id_producCompe] = '" + prodcompetenciadigitar.Rows[2][0].ToString().Trim() +
                        "', [Coverage_Date] = '" + TxtFecOperaDigita.Text + "', [weekNo] = '" + Convert.ToInt32(this.Session["NumSemana"]) + "', [Coverage_ModiBy] = '" + Convert.ToString(this.Session["sUser"]) + "', [Coverage_DateModiBy] = " +
                        "getdate() WHERE (id_Coverage = " + dt2.Rows[j]["id_Coverage"].ToString().Trim() + ")";
                    oConn.ejecutarQuery(Query);
                    Query = "UPDATE [Coverage_Planning] SET ";
                }
            }


            llena_ConsultarACTLevPropio();

            CmbOperaDigita.Enabled = false;
            CmbOperaDigita.ClearSelection();
            TxtFecOperaDigita.Enabled = false;
            TxtFecOperaDigita.Text = "";
            ImgBtnFecOperaDigita.Enabled = false;
            CmbPdvDigita.Enabled = false;
            CmbPdvDigita.Items.Clear();
            CmbProdDigita.Enabled = false;
            CmbProdDigita.Items.Clear();

            for (int j = 0; j <= GvAlmacenPlanning.Rows.Count - 1; j++)
            {
                ((TextBox)GvAlmacenPlanning.Rows[j].Cells[0].FindControl("TxtCaptura")).Text = "";
            }

            for (int j = 0; j <= GvDatosIndicadorVentas.Rows.Count - 1; j++)
            {
                ((TextBox)GvDatosIndicadorVentas.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvVentasCompe1.Rows.Count - 1; j++)
            {
                ((TextBox)GvVentasCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }
            for (int j = 0; j <= GvVentasCompe2.Rows.Count - 1; j++)
            {
                ((TextBox)GvVentasCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvVentasCompe3.Rows.Count - 1; j++)
            {
                ((TextBox)GvVentasCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvDatosIndicadorPrecios.Rows.Count - 1; j++)
            {
                ((TextBox)GvDatosIndicadorPrecios.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvPreciosCompe1.Rows.Count - 1; j++)
            {
                ((TextBox)GvPreciosCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvPreciosCompe2.Rows.Count - 1; j++)
            {
                ((TextBox)GvPreciosCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvPreciosCompe3.Rows.Count - 1; j++)
            {
                ((TextBox)GvPreciosCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvDatosIndicadorSOD.Rows.Count - 1; j++)
            {
                ((TextBox)GvDatosIndicadorSOD.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvSODCompe1.Rows.Count - 1; j++)
            {
                ((TextBox)GvSODCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }
            for (int j = 0; j <= GvSODCompe2.Rows.Count - 1; j++)
            {
                ((TextBox)GvSODCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }
            for (int j = 0; j <= GvSODCompe3.Rows.Count - 1; j++)
            {
                ((TextBox)GvSODCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvDatosIndicadorCobertura.Rows.Count - 1; j++)
            {
                ((TextBox)GvDatosIndicadorCobertura.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvCoberturaCompe1.Rows.Count - 1; j++)
            {
                ((TextBox)GvCoberturaCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvCoberturaCompe2.Rows.Count - 1; j++)
            {
                ((TextBox)GvCoberturaCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvCoberturaCompe3.Rows.Count - 1; j++)
            {
                ((TextBox)GvCoberturaCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            RbtnStatusActPropia.Items[0].Selected = true;
            RbtnStatusActPropia.Items[1].Selected = false;
            Alertas.CssClass = "MensajesSupConfirm";
            LblAlert.Text = LbltitDigita.Text;
            LblFaltantes.Text = "Sr. Usuario, el registro se ha actualizado con éxito";
            PopupMensajes();
            IbtnCrearCaptura.Visible = true;
            IbtnSaveCaptura.Visible = false;
            IbtnSearchCaptura.Visible = true;
            IbtnEditCaptura.Visible = false;
            IbtnActualizaCaptura.Visible = false;
            IbtnCancelCaptura.Visible = true;
        }

        protected void IbtnCancelCaptura_Click(object sender, ImageClickEventArgs e)
        {
            IbtnCrearCaptura.Visible = true;
            IbtnSaveCaptura.Visible = false;
            IbtnSearchCaptura.Visible = true;
            IbtnEditCaptura.Visible = false;
            IbtnActualizaCaptura.Visible = false;
            IbtnCancelCaptura.Visible = true;

            CmbOperaDigita.Enabled = false;
            CmbOperaDigita.ClearSelection();
            TxtFecOperaDigita.Enabled = false;
            TxtFecOperaDigita.Text = "";
            ImgBtnFecOperaDigita.Enabled = false;
            CmbPdvDigita.Enabled = false;
            CmbPdvDigita.Items.Clear();
            CmbProdDigita.Enabled = false;
            CmbProdDigita.Items.Clear();

            PanelTipoDigitacion.Style.Value = "display:none;";
            ChkTipDigitacion.SelectedIndex = -1;
            Paneldigitacion.Style.Value = "display:none;";
            GvAlmacenPlanning.Enabled = false;
            GvDatosIndicadorVentas.Enabled = false;
            GvDatosIndicadorPrecios.Enabled = false;
            GvDatosIndicadorSOD.Enabled = false;
            GvDatosIndicadorCobertura.Enabled = false;
            GvVentasCompe1.Enabled = false;
            GvVentasCompe2.Enabled = false;
            GvVentasCompe3.Enabled = false;
            GvPreciosCompe1.Enabled = false;
            GvPreciosCompe2.Enabled = false;
            GvPreciosCompe3.Enabled = false;
            GvCoberturaCompe1.Enabled = false;
            GvCoberturaCompe2.Enabled = false;
            GvCoberturaCompe3.Enabled = false;
            GvSODCompe1.Enabled = false;
            GvSODCompe2.Enabled = false;
            GvSODCompe3.Enabled = false;

            for (int j = 0; j <= GvAlmacenPlanning.Rows.Count - 1; j++)
            {
                ((TextBox)GvAlmacenPlanning.Rows[j].Cells[0].FindControl("TxtCaptura")).Text = "";
            }

            for (int j = 0; j <= GvDatosIndicadorVentas.Rows.Count - 1; j++)
            {
                ((TextBox)GvDatosIndicadorVentas.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvVentasCompe1.Rows.Count - 1; j++)
            {
                ((TextBox)GvVentasCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }
            for (int j = 0; j <= GvVentasCompe2.Rows.Count - 1; j++)
            {
                ((TextBox)GvVentasCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvVentasCompe3.Rows.Count - 1; j++)
            {
                ((TextBox)GvVentasCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvDatosIndicadorPrecios.Rows.Count - 1; j++)
            {
                ((TextBox)GvDatosIndicadorPrecios.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvPreciosCompe1.Rows.Count - 1; j++)
            {
                ((TextBox)GvPreciosCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvPreciosCompe2.Rows.Count - 1; j++)
            {
                ((TextBox)GvPreciosCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvPreciosCompe3.Rows.Count - 1; j++)
            {
                ((TextBox)GvPreciosCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvDatosIndicadorSOD.Rows.Count - 1; j++)
            {
                ((TextBox)GvDatosIndicadorSOD.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvSODCompe1.Rows.Count - 1; j++)
            {
                ((TextBox)GvSODCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }
            for (int j = 0; j <= GvSODCompe2.Rows.Count - 1; j++)
            {
                ((TextBox)GvSODCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }
            for (int j = 0; j <= GvSODCompe3.Rows.Count - 1; j++)
            {
                ((TextBox)GvSODCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvDatosIndicadorCobertura.Rows.Count - 1; j++)
            {
                ((TextBox)GvDatosIndicadorCobertura.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvCoberturaCompe1.Rows.Count - 1; j++)
            {
                ((TextBox)GvCoberturaCompe1.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvCoberturaCompe2.Rows.Count - 1; j++)
            {
                ((TextBox)GvCoberturaCompe2.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }

            for (int j = 0; j <= GvCoberturaCompe3.Rows.Count - 1; j++)
            {
                ((TextBox)GvCoberturaCompe3.Rows[j].Cells[0].FindControl("TxtCapturaIndicador")).Text = "";
            }
            RbtnStatusActPropia.Items[0].Selected = true;
            RbtnStatusActPropia.Items[1].Selected = false;

            TabCOperativo.Tabs[0].Enabled = true;
            TabCOperativo.Tabs[1].Enabled = true;
            TabCOperativo.Tabs[2].Enabled = false;
            TabCOperativo.Tabs[3].Enabled = false;
            TabCOperativo.Tabs[4].Enabled = false;
            TabCOperativo.Tabs[5].Enabled = false;
            TabCOperativo.Tabs[6].Enabled = false;
            TabCOperativo.Tabs[7].Enabled = true;
            TabCOperativo.Tabs[8].Enabled = true;
            TabCOperativo.ActiveTabIndex = 1;
        }
        protected void ImgCancelMail_Click(object sender, ImageClickEventArgs e)
        {
            TxtMotivo.Text = "";
            TxtMensaje.Text = "";
        }

        protected void ImgEnviarMail_Click(object sender, ImageClickEventArgs e)
        {
            if (TxtMotivo.Text == "" || TxtMensaje.Text == "")
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Envio Solicitudes";
                LblFaltantes.Text = "Sr. Usuario, es necesario que ingrese información en el asunto y en el mensaje ";
                PopupMensajes();
                return;
            }
            try
            {
                Enviomail oEnviomail = new Enviomail();
                EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Solicitud_Operativo");
                Mails oMail = new Mails();
                oMail.Server = "mail.lucky.com.pe";
                //oeEmail.MailServer;
                oMail.From = TxtSolicitante.Text;
                oMail.To = this.Session["TxtEmail"].ToString().Trim();
                oMail.Subject = TxtMotivo.Text;
                oMail.Body = TxtMensaje.Text;
                oMail.CC = "sgs_mauricio@hotmail.com";
                oMail.BodyFormat = "HTML";
                oMail.send();
                oMail = null;
                // oeEmail = null;
                oEnviomail = null;
                TxtMotivo.Text = "";
                TxtMensaje.Text = "";
                Alertas.CssClass = "MensajesSupConfirm";
                LblAlert.Text = "Envio Solicitudes";
                LblFaltantes.Text = "Sr. Usuario, el mensaje fue enviado correctamente";
                PopupMensajes();
            }
            catch (Exception ex)
            {
                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "Envio Solicitudes";
                LblFaltantes.Text = "Sr. Usuario, se presentó un error inesperado al momento de enviar el correo. Por favor intentelo nuevamente o consulte al Administrador de la aplicación";
                PopupMensajes();
                return;
            }
        }

        protected void ImgBtnFormatoPropio_Click(object sender, ImageClickEventArgs e)
        {
            TabCOperativo.Tabs[0].Enabled = true;
            TabCOperativo.Tabs[1].Enabled = true;
            TabCOperativo.Tabs[2].Enabled = false;
            TabCOperativo.Tabs[3].Enabled = false;
            TabCOperativo.Tabs[4].Enabled = false;
            TabCOperativo.Tabs[5].Enabled = true;
            TabCOperativo.Tabs[6].Enabled = true;
            TabCOperativo.Tabs[7].Enabled = true;
            TabCOperativo.Tabs[8].Enabled = true;
            TabCOperativo.ActiveTabIndex = 5;
            DataTable personalexcel = new DataTable("PersonalExcel");
            DataColumn personid = personalexcel.Columns.Add("Código", typeof(Int32));
            personalexcel.Columns.Add("Nombre", typeof(String));


            for (int i = 0; i <= LstPlanningOp.Items.Count - 1; i++)
            {
                if (LstPlanningOp.Items[i].Selected == true)
                {
                    DataRow dr = personalexcel.NewRow();
                    dr["Código"] = LstPlanningOp.Items[i].Value;
                    dr["Nombre"] = LstPlanningOp.Items[i].Text;
                    personalexcel.Rows.Add(dr);
                }
            }
            GvPersonalExcel.DataSource = personalexcel;
            GvPersonalExcel.DataBind();


            CmbOperaDigita.DataSource = personalexcel;
            CmbOperaDigita.DataTextField = "Nombre";
            CmbOperaDigita.DataValueField = "Código";
            CmbOperaDigita.DataBind();
            CmbOperaDigita.Items.Insert(0, new ListItem("<Seleccione...>", "0"));

            DataTable pdvxoperativosel = new DataTable("pdvxoperativosel");
            DataColumn person_id = pdvxoperativosel.Columns.Add("Código", typeof(Int32));
            pdvxoperativosel.Columns.Add("Cód_PDV", typeof(String));
            pdvxoperativosel.Columns.Add("Punto de Venta", typeof(String));
            pdvxoperativosel.Columns.Add("Nombre Operativo", typeof(String));
            DataTable dtpdv = new DataTable();
            for (int i = 0; i <= GvPersonalExcel.Rows.Count - 1; i++)
            {
                dtpdv = Get_Operativo.Get_PdvXOperativoSel(Convert.ToInt32(GvPersonalExcel.Rows[i].Cells[0].Text));
                if (dtpdv != null)
                {
                    if (dtpdv.Rows.Count > 0)
                    {
                        for (int j = 0; j <= dtpdv.Rows.Count - 1; j++)
                        {
                            DataRow dr = pdvxoperativosel.NewRow();
                            dr["Código"] = dtpdv.Rows[j][0].ToString().Trim();
                            dr["Cód_PDV"] = dtpdv.Rows[j][1].ToString().Trim();
                            dr["Punto de Venta"] = dtpdv.Rows[j][2].ToString().Trim();
                            dr["Nombre Operativo"] = dtpdv.Rows[j][3].ToString().Trim();
                            pdvxoperativosel.Rows.Add(dr);
                        }
                    }
                }
                dtpdv = null;
            }
            GvPDVVsPersonalExcel.DataSource = pdvxoperativosel;
            GvPDVVsPersonalExcel.DataBind();

            DataTable productxpdvxoperativosel = new DataTable("productxpdvxoperativosel");
            DataColumn persona_id = productxpdvxoperativosel.Columns.Add("Código", typeof(Int32));
            productxpdvxoperativosel.Columns.Add("Cód_Producto", typeof(String));
            productxpdvxoperativosel.Columns.Add("Marca", typeof(String));
            productxpdvxoperativosel.Columns.Add("Producto", typeof(String));
            productxpdvxoperativosel.Columns.Add("Nombre Operativo", typeof(String));
            DataTable dtproduct = new DataTable();
            for (int i = 0; i <= GvPersonalExcel.Rows.Count - 1; i++)
            {

                dtproduct = Get_Operativo.Get_ProductosXPdvXOperativoSel(Convert.ToInt32(GvPersonalExcel.Rows[i].Cells[0].Text));
                if (dtproduct != null)
                {
                    if (dtproduct.Rows.Count > 0)
                    {
                        for (int j = 0; j <= dtproduct.Rows.Count - 1; j++)
                        {
                            DataRow dr = productxpdvxoperativosel.NewRow();
                            dr["Código"] = dtproduct.Rows[j][0].ToString().Trim();
                            dr["Cód_Producto"] = dtproduct.Rows[j][1].ToString().Trim();
                            dr["Marca"] = dtproduct.Rows[j][4].ToString().Trim();
                            dr["Producto"] = dtproduct.Rows[j][2].ToString().Trim();
                            dr["Nombre Operativo"] = dtproduct.Rows[j][3].ToString().Trim();
                            productxpdvxoperativosel.Rows.Add(dr);
                        }
                    }
                }
                dtproduct = null;
            }
            GvProductoXPDVVsPersonalExcel.DataSource = productxpdvxoperativosel;
            GvProductoXPDVVsPersonalExcel.DataBind();

            DataTable compe = new DataTable();
            compe = Get_Operativo.Get_ProductoCompetidorXProducto(Convert.ToInt32(this.Session["id_Planning"]));
            if (compe != null)
            {
                if (compe.Rows.Count > 0)
                {
                    GvProductoCompeExcel.DataSource = compe;
                    GvProductoCompeExcel.DataBind();
                }
            }

            LbltitFormatoExcel.Text = "FORMATO DE LEVANTAMIENTO DE INFORMACION";
            LblCampañaExcel.Text = GVPlanning.SelectedRow.Cells[2].Text;


            for (int j = 0; j <= GVFormaPropio.PageCount - 1; j++)
            {
                for (int i = 0; i <= GVFormaPropio.Rows.Count - 1; i++)
                {
                    DataTable dtcanal = Get_Operativo.Get_CanalXFormato(GVPlanning.SelectedRow.Cells[8].Text);

                    string reporte = GVFormaPropio.Rows[i].Cells[2].Text + dtcanal.Rows[0][0].ToString().Trim();
                    if (reporte == "171241" || reporte == "211000" || reporte == "211023" || reporte == "211241" || reporte == "221000" || reporte == "221023" || reporte == "221241")
                    {
                        DataTable dt2 = Get_Operativo.Get_FormatoActivPropiaEncabezado(Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.Rows[i].Cells[2].Text));
                        GvFormatoEncExcel.DataSource = dt2;
                        GvFormatoEncExcel.DataBind();
                        DataTable dt4 = Get_Operativo.Get_FormatoActivPropiaPie(Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.Rows[i].Cells[2].Text));
                        GvFormatoPieExcel.DataSource = dt4;
                        GvFormatoPieExcel.DataBind();
                    }
                }
                GVFormaPropio.PageIndex = j + 1;
                llena_FormatosPropios();
            }

            GVFormaPropio.PageIndex = 0;
            llena_FormatosPropios();


            for (int recorre = 0; recorre <= LstPlanningOp.Items.Count - 1; recorre++)
            {
                if (LstPlanningOp.Items[recorre].Selected == true)
                {
                    conteo = conteo + 1;
                    recorre = LstPlanningOp.Items.Count;
                }
            }

            if (conteo > 0)
            {
                ImgExpExcel.Enabled = true;
            }
            else
            {
                ImgExpExcel.Enabled = false;

                Alertas.CssClass = "MensajesSupervisor";
                LblAlert.Text = "";
                LblFaltantes.Text = "Sr. Usuario, No seleccionó ningún operativo de la lista en la pestaña Formatos.";
                PopupMensajes();
                return;

            }

            // Formulario de captura de información propia
            LbltitDigita.Text = "LEVANTAMIENTO DE INFORMACION : " + GVPlanning.SelectedRow.Cells[2].Text;

            for (int k = 0; k <= GVFormaPropio.PageCount - 1; k++)
            {
                for (int formato = 0; formato <= GVFormaPropio.Rows.Count - 1; formato++)
                {
                    DataTable dtcanal = Get_Operativo.Get_CanalXFormato(GVPlanning.SelectedRow.Cells[8].Text);
                    string reporte = GVFormaPropio.Rows[formato].Cells[2].Text + dtcanal.Rows[0][0].ToString().Trim();
                    if (reporte == "171241" || reporte == "211000" || reporte == "211023" || reporte == "211241" || reporte == "221000" || reporte == "221023" || reporte == "221241")
                    {
                        DataTable dt = Get_Operativo.Get_SearchContenidoFormatosPropios(Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[0].Text), Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text));
                        this.Session["dtformatospropios"] = dt;
                        GvAlmacenPlanning.DataSource = dt;
                        GvAlmacenPlanning.DataBind();
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                for (int j = 0; j <= GvAlmacenPlanning.Rows.Count - 1; j++)
                                {
                                    ((Label)GvAlmacenPlanning.Rows[j].Cells[0].FindControl("LblCaptura")).Text = dt.Rows[j]["Información Solicitada"].ToString().Trim().ToUpper();
                                }
                            }
                        }
                    }
                    DataTable dt1 = Get_Operativo.Get_SearchContenidoFormatosPropiosIndicador(Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text));
                    this.Session["dtformatospropiosindicador" + GVFormaPropio.Rows[formato].Cells[2].Text] = dt1;


                    if (Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text) == 17)
                    {
                        GvDatosIndicadorVentas.DataSource = dt1;
                        GvDatosIndicadorVentas.DataBind();
                    }
                    if (Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text) == 19)
                    {
                        GvDatosIndicadorPrecios.DataSource = dt1;
                        GvDatosIndicadorPrecios.DataBind();
                    }
                    if (Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text) == 21)
                    {
                        GvDatosIndicadorSOD.DataSource = dt1;
                        GvDatosIndicadorSOD.DataBind();
                    }
                    if (Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text) == 22)
                    {
                        GvDatosIndicadorCobertura.DataSource = dt1;
                        GvDatosIndicadorCobertura.DataBind();
                    }

                    if (dt1 != null)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text) == 17)
                            {
                                for (int j = 0; j <= GvDatosIndicadorVentas.Rows.Count - 1; j++)
                                {
                                    ((Label)GvDatosIndicadorVentas.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                                }
                            }
                            if (Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text) == 19)
                            {
                                for (int j = 0; j <= GvDatosIndicadorPrecios.Rows.Count - 1; j++)
                                {
                                    ((Label)GvDatosIndicadorPrecios.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                                }
                            }
                            if (Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text) == 21)
                            {
                                for (int j = 0; j <= GvDatosIndicadorSOD.Rows.Count - 1; j++)
                                {
                                    ((Label)GvDatosIndicadorSOD.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                                }
                            }
                            if (Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text) == 22)
                            {
                                for (int j = 0; j <= GvDatosIndicadorCobertura.Rows.Count - 1; j++)
                                {
                                    ((Label)GvDatosIndicadorCobertura.Rows[j].Cells[0].FindControl("LblCapturaIndicador")).Text = dt1.Rows[j]["Symbol_Name"].ToString().Trim().ToUpper();
                                }
                            }
                        }
                    }

                    //////creación de formulario de formatos propios
                    ////for (int recorre = 0; recorre <= LstPlanningOp.Items.Count - 1; recorre++)
                    ////{
                    ////    if (LstPlanningOp.Items[recorre].Selected == true)
                    ////    {
                    ////        conteo = conteo + 1;
                    ////    }
                    ////}

                    ////if (conteo > 0)
                    ////{
                    ////    Label mylabelformato = new Label();
                    ////    mylabelformato.ID = "mylabelformato";
                    ////    mylabelformato.Text = GVFormaPropio.Rows[formato].Cells[1].Text;
                    ////    mylabelformato.Font.Bold = true;
                    ////    GridViewPlaceHolder.Controls.Add(mylabelformato);
                    ////    GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////    GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////    Label mylabelcampaña = new Label();
                    ////    mylabelcampaña.ID = "mylabelcampaña";
                    ////    mylabelcampaña.Text = GVPlanning.SelectedRow.Cells[2].Text;
                    ////    mylabelcampaña.Font.Bold = true;
                    ////    GridViewPlaceHolder.Controls.Add(mylabelcampaña);
                    ////    GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////    GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));

                    ////    for (int recorre = 0; recorre <= LstPlanningOp.Items.Count - 1; recorre++)
                    ////    {
                    ////        if (LstPlanningOp.Items[recorre].Selected == true)
                    ////        {
                    ////            Label mylabelopera = new Label();
                    ////            mylabelopera.ID = "MyLabelopera" + LstPlanningOp.Items[recorre].Value;
                    ////            mylabelopera.Text = "Mercaderista / Promotor : ";
                    ////            mylabelopera.Font.Bold = true;
                    ////            GridViewPlaceHolder.Controls.Add(mylabelopera);
                    ////            Label myLabel = new Label();
                    ////            myLabel.ID = "MyLabel" + LstPlanningOp.Items[recorre].Value;
                    ////            myLabel.Text = LstPlanningOp.Items[recorre].Text.ToUpper();
                    ////            myLabel.Font.Bold = true;
                    ////            GridViewPlaceHolder.Controls.Add(myLabel);
                    ////            GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////            DataTable dt2 = Get_Operativo.Get_FormatoActivPropiaEncabezado(Convert.ToInt32(this.Session["id_Planning"]));
                    ////            GridView myGrid = new GridView();
                    ////            myGrid.ID = "MyGrid" + LstPlanningOp.Items[recorre].Value;
                    ////            myGrid.Attributes.Add("runat", "server");
                    ////            myGrid.AutoGenerateColumns = true;
                    ////            myGrid.DataSource = dt2;
                    ////            myGrid.DataBind();
                    ////            myGrid.HeaderRow.Cells[0].Width = 300;
                    ////            myGrid.HeaderRow.Cells[1].Width = 700;
                    ////            GridViewPlaceHolder.Controls.Add(myGrid);
                    ////            GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));

                    ////            Table MyTable = new Table();
                    ////            MyTable.ID = "Mytable" + LstPlanningOp.Items[recorre].Value;
                    ////            TableRow tr = new TableRow();
                    ////            MyTable.Controls.Add(tr);
                    ////            DataTable dt5 = new DataTable();
                    ////            dt5 = oConn.ejecutarDataTable("UP_WEBSIGE_OPERATIVO_PDVXOPERATIVO", Convert.ToInt32(this.Session["id_Planning"]), LstPlanningOp.Items[recorre].Value);
                    ////            for (int i = 0; i <= dt5.Rows.Count - 1; i++)
                    ////            {
                    ////                DataTable dt3 = Get_Operativo.Get_FormatoActivPropiaDetalle(Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[3].Text), Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text));
                    ////                GridView myGrid1 = new GridView();
                    ////                myGrid1.ID = "MyGrid1" + LstPlanningOp.Items[recorre].Value + i;
                    ////                myGrid1.Caption = dt5.Rows[i]["pdv_Name"].ToString().Trim();
                    ////                myGrid1.CaptionAlign = TableCaptionAlign.Left;
                    ////                myGrid1.Font.Name = "Verdana";
                    ////                myGrid1.Font.Size = 7;
                    ////                myGrid1.RowStyle.Font.Name = "Verdana";
                    ////                myGrid1.RowStyle.Font.Size = 7;
                    ////                myGrid1.HeaderStyle.Font.Name = "Verdana";
                    ////                myGrid1.HeaderStyle.Font.Size = 7;
                    ////                myGrid1.HeaderStyle.Font.Bold = true;
                    ////                myGrid1.Attributes.Add("runat", "server");
                    ////                myGrid1.AutoGenerateColumns = true;

                    ////                if (ocultar > 0)
                    ////                {
                    ////                    dt3.Columns.Remove("Producto");
                    ////                }
                    ////                myGrid1.DataSource = dt3;
                    ////                myGrid1.DataBind();
                    ////                if (ocultar == 0)
                    ////                {
                    ////                    myGrid1.Rows[0].Cells[0].Width = 400;
                    ////                }
                    ////                ocultar = ocultar + 1;
                    ////                TableCell tc = new TableCell();
                    ////                tc.VerticalAlign = VerticalAlign.Top;
                    ////                tr.Controls.Add(tc);
                    ////                tc.Controls.Add(myGrid1);
                    ////                //myGrid1.Width = 1005;
                    ////                ////GridViewPlaceHolder.Controls.Add(myGrid1);
                    ////                GridViewPlaceHolder.Controls.Add(MyTable);
                    ////            }
                    ////            ocultar = 0;
                    ////            GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////            DataTable dt4 = Get_Operativo.Get_FormatoActivPropiaPie(Convert.ToInt32(this.Session["id_Planning"]));
                    ////            GridView myGrid2 = new GridView();
                    ////            myGrid2.ID = "MyGrid2" + LstPlanningOp.Items[recorre].Value;
                    ////            myGrid2.Attributes.Add("runat", "server");
                    ////            myGrid2.AutoGenerateColumns = true;
                    ////            myGrid2.DataSource = dt4;
                    ////            myGrid2.DataBind();
                    ////            myGrid2.HeaderRow.Cells[0].Width = 300;
                    ////            myGrid2.HeaderRow.Cells[1].Width = 700;
                    ////            GridViewPlaceHolder.Controls.Add(myGrid2);
                    ////            GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////            //TabCOperativo.Height = GridViewPlaceHolder.Height;
                    ////            this.Session["GridViewPlaceHolder"] = GridViewPlaceHolder;
                    ////        }
                    ////    }
                    ////}
                    ////else
                    ////{
                    ////    Label mylabelformato = new Label();
                    ////    mylabelformato.ID = "mylabelformato";
                    ////    mylabelformato.Text = GVFormaPropio.Rows[formato].Cells[1].Text;
                    ////    mylabelformato.Font.Bold = true;
                    ////    GridViewPlaceHolder.Controls.Add(mylabelformato);
                    ////    GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////    GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////    Label mylabelcampaña = new Label();
                    ////    mylabelcampaña.ID = "mylabelcampaña";
                    ////    mylabelcampaña.Text = GVPlanning.SelectedRow.Cells[2].Text;
                    ////    mylabelcampaña.Font.Bold = true;
                    ////    GridViewPlaceHolder.Controls.Add(mylabelcampaña);
                    ////    GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////    GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////    Label mylabelopera = new Label();
                    ////    mylabelopera.ID = "MyLabelopera";
                    ////    mylabelopera.Text = "Mercaderista / Promotor : ";
                    ////    mylabelopera.Font.Bold = true;
                    ////    GridViewPlaceHolder.Controls.Add(mylabelopera);
                    ////    GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////    DataTable dt2 = Get_Operativo.Get_FormatoActivPropiaEncabezado(Convert.ToInt32(this.Session["id_Planning"]));
                    ////    GridView myGrid = new GridView();
                    ////    myGrid.ID = "MyGrid";
                    ////    myGrid.Attributes.Add("runat", "server");
                    ////    myGrid.AutoGenerateColumns = true;
                    ////    myGrid.DataSource = dt2;
                    ////    myGrid.DataBind();
                    ////    myGrid.HeaderRow.Cells[0].Width = 300;
                    ////    myGrid.HeaderRow.Cells[1].Width = 700;
                    ////    GridViewPlaceHolder.Controls.Add(myGrid);
                    ////    GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////    Table MyTable = new Table();
                    ////    MyTable.ID = "Mytable";
                    ////    TableRow tr = new TableRow();
                    ////    MyTable.Controls.Add(tr);
                    ////    DataTable dt3 = Get_Operativo.Get_FormatoActivPropiaDetalle(Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[3].Text), Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text));
                    ////    GridView myGrid1 = new GridView();
                    ////    myGrid1.ID = "MyGrid1";
                    ////    myGrid1.Font.Name = "Verdana";
                    ////    myGrid1.Font.Size = 7;
                    ////    myGrid1.RowStyle.Font.Name = "Verdana";
                    ////    myGrid1.RowStyle.Font.Size = 7;
                    ////    myGrid1.HeaderStyle.Font.Name = "Verdana";
                    ////    myGrid1.HeaderStyle.Font.Size = 7;
                    ////    myGrid1.HeaderStyle.Font.Bold = true;
                    ////    myGrid1.Attributes.Add("runat", "server");
                    ////    myGrid1.AutoGenerateColumns = true;
                    ////    myGrid1.DataSource = dt3;
                    ////    myGrid1.DataBind();
                    ////    myGrid1.Rows[0].Cells[0].Width = 400;
                    ////    TableCell tc = new TableCell();
                    ////    tc.VerticalAlign = VerticalAlign.Top;
                    ////    tr.Controls.Add(tc);
                    ////    tc.Controls.Add(myGrid1);
                    ////    GridViewPlaceHolder.Controls.Add(MyTable);
                    ////    GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////    DataTable dt4 = Get_Operativo.Get_FormatoActivPropiaPie(Convert.ToInt32(this.Session["id_Planning"]));
                    ////    GridView myGrid2 = new GridView();
                    ////    myGrid2.ID = "MyGrid2";
                    ////    myGrid2.Attributes.Add("runat", "server");
                    ////    myGrid2.AutoGenerateColumns = true;
                    ////    myGrid2.DataSource = dt4;
                    ////    myGrid2.DataBind();
                    ////    myGrid2.HeaderRow.Cells[0].Width = 300;
                    ////    myGrid2.HeaderRow.Cells[1].Width = 700;
                    ////    GridViewPlaceHolder.Controls.Add(myGrid2);
                    ////    GridViewPlaceHolder.Controls.Add(new LiteralControl("<br />"));
                    ////    this.Session["GridViewPlaceHolder"] = GridViewPlaceHolder;
                    ////}


                    ////if (conteo > 0)
                    ////{

                    //Table MyTable = new Table();
                    //MyTable.ID = "Mytable";
                    //TableRow tr = new TableRow();
                    //MyTable.Controls.Add(tr);
                    //for (int formato = 0; formato <= GVFormaPropio.Rows.Count - 1; formato++)
                    //{
                    //    DataTable dt3 = Get_Operativo.Get_FormatoActivPropiaDetalle(Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[3].Text), Convert.ToInt32(this.Session["id_Planning"]), Convert.ToInt32(GVFormaPropio.Rows[formato].Cells[2].Text));
                    //    GridView myGrid1 = new GridView();
                    //    myGrid1.ID = "MyGrid1";
                    //    myGrid1.Font.Name = "Verdana";
                    //    myGrid1.Font.Size = 7;
                    //    myGrid1.RowStyle.Font.Name = "Verdana";
                    //    myGrid1.RowStyle.Font.Size = 7;
                    //    myGrid1.HeaderStyle.Font.Name = "Verdana";
                    //    myGrid1.HeaderStyle.Font.Size = 7;
                    //    myGrid1.HeaderStyle.Font.Bold = true;
                    //    myGrid1.Attributes.Add("runat", "server");
                    //    myGrid1.AutoGenerateColumns = true;
                    //    myGrid1.DataSource = dt3;
                    //    myGrid1.DataBind();
                    //    myGrid1.Rows[0].Cells[0].Width = 400;
                    //    TableCell tc = new TableCell();
                    //    tc.VerticalAlign = VerticalAlign.Top;
                    //    tr.Controls.Add(tc);
                    //    tc.Controls.Add(myGrid1);
                    //    PanelDinamico.Controls.Add(MyTable);
                    //    PanelDinamico.Controls.Add(new LiteralControl("<br />"));

                    //    PanelDinamico.Controls.Add(myGrid2);
                    //    PanelDinamico.Controls.Add(new LiteralControl("<br />"));
                    //    this.Session["GridViewPlaceHolder"] = PanelDinamico;
                    //}
                }
                GVFormaPropio.PageIndex = k + 1;
                llena_FormatosPropios();
            }
            GVFormaPropio.PageIndex = 0;
            llena_FormatosPropios();
        }

        protected void CmbOperaDigita_SelectedIndexChanged(object sender, EventArgs e)
        {
            llena_PDV();
            llena_Products();
        }

        protected void CmbPdvDigita_SelectedIndexChanged(object sender, EventArgs e)
        {
            llena_Products();
        }

        protected void CmbProdDigita_SelectedIndexChanged(object sender, EventArgs e)
        {
            llena_ProductsCompe();
            GvDatosIndicadorVentas.Caption = CmbProdDigita.SelectedItem.ToString().Trim();
            GvDatosIndicadorPrecios.Caption = CmbProdDigita.SelectedItem.ToString().Trim();
            GvDatosIndicadorCobertura.Caption = CmbProdDigita.SelectedItem.ToString().Trim();
            //GvDatosIndicadorSOD.Caption = CmbProdDigita.SelectedItem.ToString().Trim();
        }

        protected void LkbPuntosDeVenta_Click(object sender, EventArgs e)
        {
            this.Session["ArchivoCSV"] = "PDV.csv";
            Response.Redirect("~/Pages/Modulos/Operativo/filedownload.aspx", true);

        }

        protected void LkbProductos_Click(object sender, EventArgs e)
        {
            this.Session["ArchivoCSV"] = "Product.csv";
            Response.Redirect("~/Pages/Modulos/Operativo/filedownload.aspx", true);
        }

        protected void LkbMarcas_Click(object sender, EventArgs e)
        {
            this.Session["ArchivoCSV"] = "Marca.csv";
            Response.Redirect("~/Pages/Modulos/Operativo/filedownload.aspx", true);
        }

        protected void LkbEncabezado_Click(object sender, EventArgs e)
        {
            this.Session["ArchivoCSV"] = "encabezado.csv";
            Response.Redirect("~/Pages/Modulos/Operativo/filedownload.aspx", true);
        }

        protected void LkbPie_Click(object sender, EventArgs e)
        {
            this.Session["ArchivoCSV"] = "pie.csv";
            Response.Redirect("~/Pages/Modulos/Operativo/filedownload.aspx", true);
        }

        protected void ChkTipDigitacion_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            if ((ChkTipDigitacion.SelectedItem.Text == "VENTAS" || ChkTipDigitacion.SelectedItem.Text == "PRECIOS" || ChkTipDigitacion.SelectedItem.Text == "DISPONIBILIDAD") && ChkTipDigitacion.SelectedItem.Selected == true)
            {
                for (int i = 0; i <= ChkTipDigitacion.Items.Count - 1; i++)
                {
                    if (ChkTipDigitacion.Items[i].Text == "VENTAS" || ChkTipDigitacion.Items[i].Text == "PRECIOS" || ChkTipDigitacion.Items[i].Text == "DISPONIBILIDAD")
                    {
                        ChkTipDigitacion.Items[i].Selected = true;
                    }
                }
            }
            if (ChkTipDigitacion.SelectedItem.Text == "SOD" && ChkTipDigitacion.SelectedItem.Selected == true)
            {
                for (int i = 0; i <= ChkTipDigitacion.Items.Count - 1; i++)
                {
                    ChkTipDigitacion.Items[i].Selected = true;
                }
            }

            ModalPopupTipDigitacion.Show();
        }

        protected void TabCOperativo_ActiveTabChanged(object sender, EventArgs e)
        {
            ifFotosActividad.Attributes["src"] = "";
            IframeCompe.Attributes["src"] = "";
            ifcarga.Attributes["src"] = "";

            if (TabCOperativo.ActiveTabIndex == 0)
            {
                TabCOperativo.Tabs[0].Enabled = true;
                TabCOperativo.Tabs[1].Enabled = false;
                TabCOperativo.Tabs[2].Enabled = false;
                TabCOperativo.Tabs[3].Enabled = false;
                TabCOperativo.Tabs[4].Enabled = false;
                TabCOperativo.Tabs[5].Enabled = false;
                TabCOperativo.Tabs[6].Enabled = false;
                TabCOperativo.Tabs[7].Enabled = false;
                TabCOperativo.Tabs[8].Enabled = true;
            }
            if (TabCOperativo.ActiveTabIndex == 1)
            {
                TabCOperativo.Tabs[2].Enabled = false;
                TabCOperativo.Tabs[3].Enabled = false;
                TabCOperativo.Tabs[4].Enabled = false;
                TabCOperativo.Tabs[5].Enabled = false;
                TabCOperativo.Tabs[6].Enabled = false;
            }

            if (TabCOperativo.ActiveTabIndex == 2)
            {
                ifFotosActividad.Attributes["src"] = "CargaFotosAct.aspx";
            }

            if (TabCOperativo.ActiveTabIndex == 3)
            {
                IframeCompe.Attributes["src"] = "RVCompetencia.aspx";
                ifcarga.Attributes["src"] = "CargaFotosCom.aspx";
            }

            if (TabCOperativo.ActiveTabIndex == 4)
            {
                llena_POP();
                llena_Channel();
                llena_Category();
            }

            if (TabCOperativo.ActiveTabIndex == 8)
            {
                try
                {
                    Get_Administrativo.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                    this.Session.Clear();
                    Response.Redirect("~/login.aspx",true);
                }
                catch (Exception ex)
                {
                    Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                    this.Session.Clear();
                    Response.Redirect("~/login.aspx", true);
                }
            }
        }
    }
}