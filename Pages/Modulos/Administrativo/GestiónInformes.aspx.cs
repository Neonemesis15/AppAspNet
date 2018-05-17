using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Lucky.Business.Common.Application;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;

namespace SIGE.Pages.Modulos.Administrativo
{

    //-- =============================================
    //-- Author:		    <Ing. Magaly Jiménez>
    //-- Create date:       <30/07/2010>
    //-- Description:       <Permite al actor Administrador de SIGE realizar todos los procesos para la administracion 
    //--                    de Gestión De Informes>
    //-- Requerimiento No.  
    //-- =============================================
    public partial class GestiónInformes : System.Web.UI.Page
    {
        private bool estado;
        private string sReportid;
        private string sNomInf;
        private string repetido = "";
        private int icodStrategy;
        private string sNomInfvsServ, sNomRepCanal;
        private bool continuar = true;
        private bool bContinuar = true;
        //bool bcontinuar1 = true;
        private int recsearch;
        private string scodChannel;
        private bool dato;
        //private long iid_City_UserRepor;
        //int iid_userinforme;
        private string sbcliente, sUsuario, sbCanal, sBServicio;
        //private string sbclientecity, sUsuariocity, sbCanalcity, sBServiciocity, sBReportCity;
        private DataTable recorrido = null;

        //private int iid_TypeReport, iModulo_id, ReportId, CompanyId, ReportIdC;
        private int ReportId, CompanyId, ReportIdC;
        private Reports oReports = new Reports();
        private ReportChannel oReportesCanal = new ReportChannel();
        private AD_GestionProductosXReporte oRG = new AD_GestionProductosXReporte();
        private InfoaUsuario oinfousuario = new InfoaUsuario();
        private AD_GestionProductosXReporte oParametroReporte = new AD_GestionProductosXReporte();
        private InfoaUsuario odactInfoaUsu = new InfoaUsuario();
        private City_UserReport oerCityUserReport = new City_UserReport();
        private AD_ReportOficina oerROficina = new AD_ReportOficina();
        private Conexion oConn = new Lucky.Data.Conexion();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos owsadministrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    LlenaComboInforme();
                    //LlenaCmbModulo();                  
                    ComboBuscarserviciosInfvsServ();
                    comboclienteBuscarenInfoUser();
                    llenacomboBuscarclienteRC();
                    ComboBuscarClienteCity();
                    llenacomboBuscarClienteRO();
                    LlenaComboPaisInfoServ();
                    LlenaComboReporserviinfo();
                    comboclienteBuscarenGR();
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
        }

        #region Funciones
        private void SavelimpiarControlesInforme()
        {

            //Controles ViewReportes
            TxtCodReport.Text = "";
            txtNomReport.Text = "";
            //ChkTipInf.Items.Clear();
            //ChkSelModulo.Items.Clear();
            TxtDescReport.Text = "";
            RBtnListStatusReport.Items[0].Selected = true;
            RBtnListStatusReport.Items[1].Selected = false;

            CmbNameReporte.Text = "0";
            //CmbNameReporte.Text = "0";
            //for (int i = 0; i <= ChkTipInf.Items.Count - 1; i++)
            //{
            //    ChkTipInf.Items[i].Selected = false;
            //}
            //for (int i = 0; i <= ChkSelModulo.Items.Count - 1; i++)
            //{
            //    ChkSelModulo.Items[i].Selected = false;
            //}
        }
        private void activarControlesInforme()
        {
            //Controles ViewReportes
            //ChkTipInf.Enabled = true;
            //ChkSelModulo.Enabled = true;
            RBtnListStatusReport.Enabled = true;
            TxtCodReport.Enabled = false;
            txtNomReport.Enabled = true;
            TxtDescReport.Enabled = true;

            //navegadores          
            btnPreg9.Visible = false;
            btnAreg9.Visible = false;
            btnSreg9.Visible = false;
            btnUreg9.Visible = false;
            Panel_informe.Enabled = true;
            Panel_info_Servicio.Enabled = false;
            TabGestionReporte.Enabled = false;
            PanelUsuariovsInforme.Enabled = false;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Report_Channel.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;

        }
        private void desactivarControlesInforme()
        {
            //Controles ViewReportes
            //ChkTipInf.Enabled = false;
            //cmbSIndicador.Enabled = false;
            //ChkSelModulo.Enabled = false;
            RBtnListStatusReport.Enabled = false;
            TxtCodReport.Enabled = false;
            txtNomReport.Enabled = false;
            //TxtNomReport.Enabled = false;
            TxtDescReport.Enabled = false;
            Panel_informe.Enabled = true;
            Panel_info_Servicio.Enabled = true;
            PanelUsuariovsInforme.Enabled = true;
            TabAsignaciónCobertura.Enabled =true;
            Tab_Report_Channel.Enabled = true;
            Tab_Reporte_Oficina.Enabled = true;
            TabGestionReporte.Enabled = true;
        }
        private void crearActivarbotonesInforme()
        {
            btnCrearReporte.Visible = false;
            BtnGuardarReporte.Visible = true;
            btnConsultarReporte.Visible = false;
            btnEditReport.Visible = false;
            RBtnListStatusReport.Enabled = false;
            btnActualizarReporte.Visible = false;
            btnCancelarReporte.Visible = true;
            btnPreg9.Visible = false;
            btnAreg9.Visible = false;
            btnSreg9.Visible = false;
            btnUreg9.Visible = false;


        }
        private void saveActivarbotonesInforme()
        {
            btnCrearReporte.Visible = true;
            BtnGuardarReporte.Visible = false;
            btnConsultarReporte.Visible = true;
            btnEditReport.Visible = false;
            btnActualizarReporte.Visible = false;
            btnCancelarReporte.Visible = true;
            btnPreg9.Visible = false;
            btnAreg9.Visible = false;
            btnSreg9.Visible = false;
            btnUreg9.Visible = false;

        }
        private void EditarActivarbotonesInforme()
        {
            btnCrearReporte.Visible = false;
            BtnGuardarReporte.Visible = false;
            btnConsultarReporte.Visible = true;
            btnEditReport.Visible = false;
            btnActualizarReporte.Visible = true;
            btnCancelarReporte.Visible = true;
            btnPreg9.Visible = false;
            btnAreg9.Visible = false;
            btnSreg9.Visible = false;
            btnUreg9.Visible = false;

        }
        private void EditarActivarControlesInforme()
        {

            //Controles ViewReportes
            //ChkTipInf.Enabled = true;
            ////ChkSelModulo.Enabled = true;
            RBtnListStatusReport.Enabled = true;
            TxtCodReport.Enabled = false;
            txtNomReport.Enabled = true;
            TxtDescReport.Enabled = true;

            //navegadores          
            btnPreg9.Visible = false;
            btnAreg9.Visible = false;
            btnSreg9.Visible = false;
            btnUreg9.Visible = false;
            Panel_informe.Enabled = true;
            Panel_info_Servicio.Enabled = false;
            PanelUsuariovsInforme.Enabled = false;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Report_Channel.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;
        }
        private void BuscarActivarbotnesInforme()
        {

            btnCrearReporte.Visible = false;
            BtnGuardarReporte.Visible = false;
            btnConsultarReporte.Visible = true;
            btnEditReport.Visible = true;
            btnActualizarReporte.Visible = false;
            btnCancelarReporte.Visible = true;

        }
        private void LlenaComboInforme()
        {
            DataSet ds1 = new DataSet();
            ds1 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 40);
            //se llena reportes en buscar reportes
            CmbNameReporte.DataSource = ds1;
            CmbNameReporte.DataTextField = "Report_NameReport";
            CmbNameReporte.DataValueField = "Report_Id";
            CmbNameReporte.DataBind();

            //se llena informes en informes
            //txtNomReport.DataSource = ds1;
            //CmbNomReport.DataTextField = "Report_NameReport";
            //CmbNomReport.DataValueField = "Report_Id";
            //CmbNomReport.DataBind();
        }
        //private void llenaCmbTipoInfo()
        //{
        //    DataSet ds = new DataSet();
        //    ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 18);
        //    //se llena tipo de informes  en Informes
        //    ChkTipInf.DataSource = ds;
        //    ChkTipInf.DataTextField = "TypeReport_Name";
        //    ChkTipInf.DataValueField = "id_TypeReport";
        //    ChkTipInf.DataBind();
        //    ds = null;
        //}
        //private void LlenaCmbModulo()
        //{
        //    DataSet ds1 = new DataSet();
        //    ds1 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 51);
        //    ChkSelModulo.DataSource = ds1;
        //    ChkSelModulo.DataTextField = "Modulo_Name";
        //    ChkSelModulo.DataValueField = "Modulo_id";
        //    ChkSelModulo.DataBind();
        //}

        private void SavelimpiarControlesInfoServ()
        {
            TxtAsociaRpvsEs.Text = "";
            CmbSelPaisSer.Text = "0";
            cmbSelServicio.Items.Clear();
            cmbSelReporte.Text = "0";

            RBtnListStatusAsociarRpvsEs.Items[0].Selected = true;
            RBtnListStatusAsociarRpvsEs.Items[1].Selected = false;

            cmbClienteBUI.Text = "0";
            cmbUsuarioBAIU.Text = "0";
            cmbBCanalUI.Text = "0";
            cmbServicioBAIU.Text = "0";
        }
        private void activarControlesInfoServ()
        {
            CmbSelPaisSer.Enabled = true;
            cmbSelServicio.Enabled = true;
            cmbSelReporte.Enabled = true;
            RBtnListStatusAsociarRpvsEs.Enabled = false;
            TxtAsociaRpvsEs.Enabled = false;
            Panel_informe.Enabled = false;
            Panel_info_Servicio.Enabled = true;
            PanelUsuariovsInforme.Enabled = false;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Report_Channel.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;
            TabGestionReporte.Enabled = false;
        }
        private void desactivarControlesInfoServ()
        {
            CmbSelPaisSer.Enabled = false;
            cmbSelServicio.Enabled = false;
            cmbSelReporte.Enabled = false;
            RBtnListStatusAsociarRpvsEs.Enabled = false;
            TxtAsociaRpvsEs.Enabled = false;
            Panel_informe.Enabled = true;
            Panel_info_Servicio.Enabled = true;
            PanelUsuariovsInforme.Enabled = true;
            TabGestionReporte.Enabled = true;
            TabAsignaciónCobertura.Enabled = true;
            Tab_Report_Channel.Enabled = true;
            Tab_Reporte_Oficina.Enabled = true;
        }
        private void crearActivarbotonesInfoServ()
        {
            BtnCrearARpvsEs.Visible = false;
            BtnSaveARpvsEs.Visible = true;
            BtnConsultaARpvsEs.Visible = false;
            btnEditRepVSSer.Visible = false;
            BtnActualizaARpvsEs.Visible = false;
            BtnCancelARpvsEs.Visible = true;
            btnPreg10.Visible = false;
            btnAreg10.Visible = false;
            btnSreg10.Visible = false;
            btnUreg10.Visible = false;

        }
        private void saveActivarbotonesInfoServ()
        {
            BtnCrearARpvsEs.Visible = true;
            BtnSaveARpvsEs.Visible = false;
            BtnConsultaARpvsEs.Visible = true;
            btnEditRepVSSer.Visible = false;
            BtnActualizaARpvsEs.Visible = false;
            BtnCancelARpvsEs.Visible = true;
            btnPreg10.Visible = false;
            btnAreg10.Visible = false;
            btnSreg10.Visible = false;
            btnUreg10.Visible = false;
        }
        private void EditarActivarbotonesInfoServ()
        {
            BtnCrearARpvsEs.Visible = false;
            BtnSaveARpvsEs.Visible = false;
            BtnConsultaARpvsEs.Visible = true;
            btnEditRepVSSer.Visible = false;
            BtnActualizaARpvsEs.Visible = true;
            BtnCancelARpvsEs.Visible = true;
            btnPreg10.Visible = false;
            btnAreg10.Visible = false;
            btnSreg10.Visible = false;
            btnUreg10.Visible = false;
        }
        private void EditarActivarControlesInfoServ()
        {
            CmbSelPaisSer.Enabled = true;
            cmbSelServicio.Enabled = true;
            cmbSelReporte.Enabled = true;
            RBtnListStatusAsociarRpvsEs.Enabled = true;
            TxtAsociaRpvsEs.Enabled = false;
            Panel_informe.Enabled = false;
            Panel_info_Servicio.Enabled = true;
            PanelUsuariovsInforme.Enabled = false;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Report_Channel.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;
        }
        private void BuscarActivarbotnesInfoServ()
        {
            BtnCrearARpvsEs.Visible = false;
            BtnSaveARpvsEs.Visible = false;
            BtnConsultaARpvsEs.Visible = true;
            btnEditRepVSSer.Visible = true;
            BtnActualizaARpvsEs.Visible = false;
            BtnCancelARpvsEs.Visible = true;
        }
        private void LlenaComboPaisInfoServ()
        {
            //llena combo de pais 
            DataSet ds7 = new DataSet();
            ds7 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 42);
            CmbSelPaisSer.DataSource = ds7;
            CmbSelPaisSer.DataTextField = "Name_Country";
            CmbSelPaisSer.DataValueField = "cod_Country";
            CmbSelPaisSer.DataBind();


            CmbBSelPaisSvsInf.DataSource = ds7;
            CmbBSelPaisSvsInf.DataTextField = "Name_Country";
            CmbBSelPaisSvsInf.DataValueField = "cod_Country";
            CmbBSelPaisSvsInf.DataBind();
        }
        private void LlenaComboServiInfoServ()
        {
            DataSet ds3 = new DataSet();
            ds3 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOSERVICIOS", CmbSelPaisSer.Text);
            //se llena servicios en items de servicios
            cmbSelServicio.DataSource = ds3;
            cmbSelServicio.DataTextField = "Strategy_Name";
            cmbSelServicio.DataValueField = "cod_Strategy";
            cmbSelServicio.DataBind();
            ds3 = null;
        }
        private void LlenaComboReporserviinfo()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 20);
            //se llena reportes en reportes vs servicios
            cmbSelReporte.DataSource = ds;
            cmbSelReporte.DataTextField = "Report_NameReport";
            cmbSelReporte.DataValueField = "Report_Id";
            cmbSelReporte.DataBind();
            // llena reportes en busqueda reportes vs Servicio
            cmbSnomRep.DataSource = ds;
            cmbSnomRep.DataTextField = "Report_NameReport";
            cmbSnomRep.DataValueField = "Report_Id";
            cmbSnomRep.DataBind();
        }
        private void ComboBuscarserviciosInfvsServ()
        {
            DataSet ds3 = new DataSet();
            ds3 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOSERVICIOS", CmbBSelPaisSvsInf.Text);
            //se llena servicios en buscar reportes por servicio
            cmbSelSer.DataSource = ds3;
            cmbSelSer.DataTextField = "Strategy_Name";
            cmbSelSer.DataValueField = "cod_Strategy";
            cmbSelSer.DataBind();
            ds3 = null;
        }

        //private void ComboBuscarReporteServ()
        //{
        //    DataSet ds3 = new DataSet();
        //    ds3 = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_OBTENER_REPORT_POR_CHANNEL", cmbSelSer.Text);
        //    //se llena servicios en buscar reportes por servicio
        //    cmbSnomRep.DataSource = ds3;
        //    cmbSnomRep.DataTextField = "NameReport";
        //    cmbSnomRep.DataValueField = "Report_id";
        //    cmbSnomRep.DataBind();
        //    ds3 = null;
        //}

        private void SavelimpiarControlesReportChannel()
        {
            TxtCodAC.Text = "";
            txtAlias.Text = "";
            CmbCienteRC.Text = "0";
            cmbCanalRC.Text = "0";
            cmbReportesRC.Text = "0";

            RBTEsatadoRC.Items[0].Selected = true;
            RBTEsatadoRC.Items[1].Selected = false;

            cmbBuscarClienteRC.Text = "0";
            cmbBuscarCanal.Text = "0";
            cmbBuscarReportRC.Text = "0";

        }
        private void activarControlesReportChannel()
        {
            CmbCienteRC.Enabled = true;
            cmbCanalRC.Enabled = true;
            txtAlias.Enabled = true;
            cmbReportesRC.Enabled = true;
            RBTEsatadoRC.Enabled = false;
            Panel_informe.Enabled = false;
            Panel_info_Servicio.Enabled = false;
            PanelUsuariovsInforme.Enabled = false;
            Tab_Report_Channel.Enabled = true;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;
            TabGestionReporte.Enabled = false;         
        }

        private void desactivarControlesReportChannel()
        {
            CmbCienteRC.Enabled = false;
            cmbCanalRC.Enabled = false;
            cmbReportesRC.Enabled = false;
            txtAlias.Enabled = false;
            RBTEsatadoRC.Enabled = false;
            Panel_informe.Enabled = true;
            Panel_info_Servicio.Enabled = true;
            PanelUsuariovsInforme.Enabled = true;
            Tab_Report_Channel.Enabled = true;
            TabAsignaciónCobertura.Enabled = true;
            Tab_Reporte_Oficina.Enabled = true;
            TabGestionReporte.Enabled = true;
        }
        private void crearActivarbotonesReportChannel()
        {
            BtnCrearRC.Visible = false;
            BtnGuardarRC.Visible = true;
            BtnConsultarRC.Visible = false;
            BtnEditarRC.Visible = false;
            BtnActualizarRC.Visible = false;
            BtnCancelar.Visible = true;
            BtnPriRC.Visible = false;
            BtnAnRC.Visible = false;
            BtnSigRC.Visible = false;
            BtnUlRC.Visible = false;

        }
        private void saveActivarbotonesReportChannel()
        {
            BtnCrearRC.Visible = true;
            BtnGuardarRC.Visible = false;
            BtnConsultarRC.Visible = true;
            BtnEditarRC.Visible = false;
            BtnActualizarRC.Visible = false;
            BtnCancelar.Visible = true;
            BtnPriRC.Visible = false;
            BtnAnRC.Visible = false;
            BtnSigRC.Visible = false;
            BtnUlRC.Visible = false;

        }
        private void EditarActivarbotonesReportChannel()
        {
            BtnCrearRC.Visible = false;
            BtnGuardarRC.Visible = false;
            BtnConsultarRC.Visible = true;
            BtnEditarRC.Visible = false;
            BtnActualizarRC.Visible = true;
            BtnCancelar.Visible = true;
            BtnPriRC.Visible = false;
            BtnAnRC.Visible = false;
            BtnSigRC.Visible = false;
            BtnUlRC.Visible = false;

        }
        private void EditarActivarControlesReportChannel()
        {
            CmbCienteRC.Enabled = false;
            cmbCanalRC.Enabled = false;
            cmbReportesRC.Enabled = false;
            RBTEsatadoRC.Enabled = true;
            Panel_informe.Enabled = false;
            Panel_info_Servicio.Enabled = false;
            PanelUsuariovsInforme.Enabled = false;
            Tab_Report_Channel.Enabled = true;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;
         

        }
        private void BuscarActivarbotnesReportChannel()
        {
            BtnCrearRC.Visible = false;
            BtnGuardarRC.Visible = false;
            BtnConsultarRC.Visible = true;
            BtnEditarRC.Visible = true;
            BtnActualizarRC.Visible = false;
            BtnCancelar.Visible = true;

        }
        private void llenacomboclienteRC()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSREPORCHANNEL", 0);
            CmbCienteRC.DataSource = ds.Tables[0];
            CmbCienteRC.DataTextField = "Company_Name";
            CmbCienteRC.DataValueField = "Company_id";
            CmbCienteRC.DataBind();

        }
        private void llenacombocCanalRC()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSREPORCHANNEL", Convert.ToInt32(CmbCienteRC.SelectedValue));
            cmbCanalRC.DataSource = ds.Tables[1];
            cmbCanalRC.DataTextField = "Channel_Name";
            cmbCanalRC.DataValueField = "cod_Channel";
            cmbCanalRC.DataBind();


        }
        private void llenacomboReportesRC()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSREPORCHANNEL", 0);
            cmbReportesRC.DataSource = ds.Tables[2];
            cmbReportesRC.DataTextField = "Report_NameReport";
            cmbReportesRC.DataValueField = "Report_Id";
            cmbReportesRC.DataBind();

        }
        private void llenacomboBuscarclienteRC()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSCONSULTAREPORCHANNEL", 0, 0);
            cmbBuscarClienteRC.DataSource = ds.Tables[0];
            cmbBuscarClienteRC.DataTextField = "Company_Name";
            cmbBuscarClienteRC.DataValueField = "Company_id";
            cmbBuscarClienteRC.DataBind();

        }
        private void llenacomboBuscarCanalRC()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSCONSULTAREPORCHANNEL", Convert.ToInt32(cmbBuscarClienteRC.SelectedValue), 0);
            cmbBuscarCanal.DataSource = ds.Tables[1];
            cmbBuscarCanal.DataTextField = "Channel_Name";
            cmbBuscarCanal.DataValueField = "cod_Channel";
            cmbBuscarCanal.DataBind();


        }
        private void llenacomboBuscarReportesRC()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSCONSULTAREPORCHANNEL", Convert.ToInt32(cmbBuscarClienteRC.SelectedValue), Convert.ToInt32(cmbBuscarCanal.SelectedValue));
            cmbBuscarReportRC.DataSource = ds.Tables[2];
            cmbBuscarReportRC.DataTextField = "Report_NameReport";
            cmbBuscarReportRC.DataValueField = "Report_Id";
            cmbBuscarReportRC.DataBind();

        }


        private void SavelimpiarControlesUsuarioInfo()
        {
            TextCodUI.Text = "";
            cmbUsuarioInfo.Text = "0";
            cmbClienteAcceder.Text = "0";
            //cmbCanalUsu.Text = "0";
            //cmbServicioUsu.Text = "0";
            cmbCanalUsu.Items.Clear();
            cmbSubCanalUsu.Items.Clear();
            cmbSubNivel.Items.Clear();
            cmbServicioUsu.Items.Clear();
            CmbClienteUI.Text = "0";
            Checkinforme.Items.Clear();

            //for (int i = 0; i <= Checkinforme.Items.Count - 1; i++)
            //{
            //    Checkinforme.Items[i].Selected = false;
            //}      

            RadioInfoUsu.Items[0].Selected = true;
            RadioInfoUsu.Items[1].Selected = false;


            cmbClienteBUI.Text = "0";
            cmbUsuarioBAIU.Items.Clear();
            cmbBCanalUI.Items.Clear();
            cmbServicioBAIU.Items.Clear();
        }
        private void activarControlesInfoUsuarioInfo()
        {
            TextCodUI.Enabled = false;
            cmbUsuarioInfo.Enabled = true;
            cmbCanalUsu.Enabled = true;
            cmbSubCanalUsu.Enabled = true;
            cmbSubNivel.Enabled = true;
            cmbClienteAcceder.Enabled = true;
            cmbServicioUsu.Enabled = true;
            CheckCiudades.Enabled = true;
            CmbClienteUI.Enabled = true;
            Checkinforme.Enabled = true;
            RadioInfoUsu.Enabled= false;
            Panel_informe.Enabled = false;
            Panel_info_Servicio.Enabled = false;
            PanelUsuariovsInforme.Enabled = true;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Report_Channel.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;
            TabGestionReporte.Enabled = false;
        }
        private void desactivarControlesUsuarioInfo()
        {
            TextCodUI.Enabled = false;
            cmbUsuarioInfo.Enabled = false;
            cmbCanalUsu.Enabled = false;
            cmbSubCanalUsu.Enabled = false;
            cmbSubNivel.Enabled = false;
            cmbClienteAcceder.Enabled = false;
            cmbServicioUsu.Enabled = false;
            CmbClienteUI.Enabled = false;
            CheckCiudades.Enabled = false;
            Checkinforme.Enabled = false;
            RadioInfoUsu.Items[0].Selected = false;
            Panel_informe.Enabled = true;
            Panel_info_Servicio.Enabled = true;
            PanelUsuariovsInforme.Enabled = true;
            TabAsignaciónCobertura.Enabled = true;
            Tab_Report_Channel.Enabled = true;
            Tab_Reporte_Oficina.Enabled = true;
            TabGestionReporte.Enabled = true;
        }
        private void crearActivarbotonesUsuarioInfo()
        {
            BtnCrearUsuInfo.Visible = false;
            BtnGuardarUsuInfo.Visible = true;
            BtnConsultarUsuInfo.Visible = false;
            BtnEditarUsuInfo.Visible = false;
            BtnActuUsuInfo.Visible = false;
            BtnCancelUsuInfo.Visible = true;
            //Button7.Visible = false;
            //Button8.Visible = false;
            //Button9.Visible = false;
            //Button10.Visible = false;

        }
        private void saveActivarbotonesUsuarioInfo()
        {
            BtnCrearUsuInfo.Visible = true;
            BtnGuardarUsuInfo.Visible = false;
            BtnConsultarUsuInfo.Visible = true;
            BtnEditarUsuInfo.Visible = false;
            BtnActuUsuInfo.Visible = false;
            BtnCancelUsuInfo.Visible = true;
            //Button7.Visible = false;
            //Button8.Visible = false;
            //Button9.Visible = false;
            //Button10.Visible = false;

        }
        private void EditarActivarbotonesUsuarioInfo()
        {
            BtnCrearUsuInfo.Visible = false;
            BtnGuardarUsuInfo.Visible = false;
            BtnConsultarUsuInfo.Visible = true;
            BtnEditarUsuInfo.Visible = false;
            BtnActuUsuInfo.Visible = true;
            BtnCancelUsuInfo.Visible = true;
            //Button7.Visible = false;
            //Button8.Visible = false;
            //Button9.Visible = false;
            //Button10.Visible = false;
        }
        private void EditarActivarControlesUsuarioInfo()
        {
            TextCodUI.Enabled = false;
            cmbUsuarioInfo.Enabled = false;
            cmbCanalUsu.Enabled = false;
            cmbSubCanalUsu.Enabled = false;
            cmbSubNivel.Enabled = false;
            cmbServicioUsu.Enabled = false;
            cmbClienteAcceder.Enabled = false;
            Checkinforme.Enabled = true;
            CmbClienteUI.Enabled = false;
            CheckCiudades.Enabled = true;
            RadioInfoUsu.Enabled= false;
            Panel_informe.Enabled = false;
            Panel_info_Servicio.Enabled = false;
            PanelUsuariovsInforme.Enabled = true;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Report_Channel.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;
        }
        private void BuscarActivarbotnesUsuarioInfo()
        {
            BtnCrearUsuInfo.Visible = false;
            BtnGuardarUsuInfo.Visible = false;
            BtnConsultarUsuInfo.Visible = true;
            BtnEditarUsuInfo.Visible = true;
            BtnActuUsuInfo.Visible = false;
            BtnCancelUsuInfo.Visible = true;
        }
        private void comboclienteenInfoUser()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en en maestro de Asignación de informes a usuario
            CmbClienteUI.DataSource = ds;
            CmbClienteUI.DataTextField = "Company_Name";
            CmbClienteUI.DataValueField = "Company_id";
            CmbClienteUI.DataBind();


        }
        private void comboclienteenInfoUserAcceso()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en en maestro de Asignación de informes a usuario
            cmbClienteAcceder.DataSource = ds;
            cmbClienteAcceder.DataTextField = "Company_Name";
            cmbClienteAcceder.DataValueField = "Company_id";
            cmbClienteAcceder.DataBind();


        }
        private void comboclienteBuscarenInfoUser()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosConsultaInfoUser(0, 0, "0");
            //se llena combo cliente buscar asignación de informes a usuarios
            cmbClienteBUI.DataSource = ds.Tables[0];
            cmbClienteBUI.DataTextField = "Company_Name";
            cmbClienteBUI.DataValueField = "Company_id";
            cmbClienteBUI.DataBind();
            ds = null;

        }
        private void combousuarios()
        {
            DataTable dt = new DataTable();
            //llena combo usuarios en Asignación de informe a Ususario por Cliente seleccionado
            dt = owsadministrativo.LlenaComboUsuarioXCliente(Convert.ToInt32(CmbClienteUI.SelectedValue));
            cmbUsuarioInfo.DataSource = dt;
            cmbUsuarioInfo.DataTextField = "name_user";
            cmbUsuarioInfo.DataValueField = "Person_id";
            cmbUsuarioInfo.DataBind();
            dt = null;
        }
        private void comboBuscarusuarios()
        {

            DataSet ds = new DataSet();
            //llena combo Buscar usuarios en Asignación de informe a Ususario por cliente seleccioando
            ds = owsadministrativo.llenaCombosConsultaInfoUser(Convert.ToInt32(cmbClienteBUI.SelectedValue), 0, "0");
            cmbUsuarioBAIU.DataSource = ds.Tables[1];
            cmbUsuarioBAIU.DataTextField = "name_user";
            cmbUsuarioBAIU.DataValueField = "Person_id";
            cmbUsuarioBAIU.DataBind();
            ds = null;
        }

        private void ComboCanalXCliente()
        {
            DataTable dt = new DataTable();
            dt = owsadministrativo.LLenacomboCanalporCliente(Convert.ToInt32(cmbClienteAcceder.SelectedValue));
            //se llena combo canales en Asignación de informe a Ususario por Cliente seleccionado
            cmbCanalUsu.DataSource = dt;
            cmbCanalUsu.DataTextField = "Channel_Name";
            cmbCanalUsu.DataValueField = "cod_Channel";
            cmbCanalUsu.DataBind();
            cmbCanalUsu.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            dt = null;
        }
        private void ComboCanalBuscarXCliente()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosConsultaInfoUser(Convert.ToInt32(cmbClienteBUI.SelectedValue), Convert.ToInt32(cmbUsuarioBAIU.SelectedValue), "0");
            //se llena combo buescar canales en Asignación de informe a Ususario  por usario seleccionado
            cmbBCanalUI.DataSource = ds.Tables[2];
            cmbBCanalUI.DataTextField = "Channel_Name";
            cmbBCanalUI.DataValueField = "cod_Channel";
            cmbBCanalUI.DataBind();
            ds = null;
        }
        private void llenacomboServicioUsu()
        {
            DataTable dt1 = new DataTable();
            //consulta pais de usuario seleccioando.
            dt1 = owsadministrativo.ConsultaPaísdeUsuario(Convert.ToInt32(cmbUsuarioInfo.SelectedValue));
            if (dt1 != null)
            {
                if (dt1.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = owsadministrativo.LlenaComServicio(dt1.Rows[0]["cod_Country"].ToString().Trim());
                    //se llena Combo de servicio segun usuario seleccionado por pais
                    cmbServicioUsu.DataSource = dt;
                    cmbServicioUsu.DataTextField = "Strategy_Name";
                    cmbServicioUsu.DataValueField = "cod_Strategy";
                    cmbServicioUsu.DataBind();
                    cmbServicioUsu.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                    dt = null;
                }
                else
                {
                    cmbServicioUsu.Items.Clear();

                }
            }
            Checkinforme.Items.Clear();

           
        }
        private void llenacomboBuscarServicioUsu()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosConsultaInfoUser(Convert.ToInt32(cmbClienteBUI.SelectedValue), Convert.ToInt32(cmbUsuarioBAIU.SelectedValue), cmbBCanalUI.SelectedValue);
            //se llena Combo de servicio segun Canal seleccionado
            cmbServicioBAIU.DataSource = ds.Tables[3];
            cmbServicioBAIU.DataTextField = "Strategy_Name";
            cmbServicioBAIU.DataValueField = "cod_Strategy";
            cmbServicioBAIU.DataBind();
            ds = null;
        }
        private void LLenainformeporServicio()
        {
            
                    // llena Checkinforme al seleccionar el servicio por cliente, canal y servicio
                    DataSet ds = new DataSet();
                    ds = owsadministrativo.llenaCheckInformes(Convert.ToInt32(cmbClienteAcceder.SelectedValue), cmbCanalUsu.SelectedValue, Convert.ToInt32(cmbServicioUsu.SelectedValue));
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            Checkinforme.DataSource = ds;
                            Checkinforme.DataTextField = "Report_NameReport";
                            Checkinforme.DataValueField = "Report_Id";
                            Checkinforme.DataBind();
                           // Checkinforme.Items.Remove(Checkinforme.Items[0]);
                            ds = null;
                        }
                        else
                        {
                            Checkinforme.Items.Clear();
                        }
                    }
                  
        }
        //private void llenacomboUsuarioConsulta()
        //{
        //    DataSet ds = new DataSet();
        //    ds = oinfousuario.ConsultarInfoaUsuario(0, 0, 0);
        //    //se llena combo de Usuario en la consulta
        //    CmbBuscarAsignación.DataSource = ds.Tables[0];
        //    CmbBuscarAsignación.DataTextField = "name_user";
        //    CmbBuscarAsignación.DataValueField = "Person_id";
        //    CmbBuscarAsignación.DataBind();
        //    CmbBuscarAsignación.Items.Insert(0, new ListItem("<Todos..>", "0"));
        //    ds = null;
        //}
        
    
        //private void llenacomboServicioConsulta()
        //{
        //    DataSet ds = new DataSet();
        //    ds = oinfousuario.ConsultarInfoaUsuario(0, 0, 1);
        //    //se llena combo de Servicio en la consulta
        //    CmbBuscarServicio.DataSource = ds.Tables[0];
        //    CmbBuscarServicio.DataTextField = "Strategy_Name";
        //    CmbBuscarServicio.DataValueField = "cod_Strategy";
        //    CmbBuscarServicio.DataBind();
        //    CmbBuscarServicio.Items.Insert(0, new ListItem("<Todos..>", "0"));
        //    ds = null;
        //}
        //private void llenaGrillaConsulta()
        //{
        //    DataSet ds = new DataSet();
        //    if (CmbBuscarAsignación.SelectedValue == "0" && CmbBuscarServicio.SelectedValue == "0")
        //    {
        //        ds = oinfousuario.ConsultarInfoaUsuario(0, 0, 2);
        //        //se llena Grilla en la consulta
        //        InfoaUsuarios.DataSource = ds.Tables[0];
        //        InfoaUsuarios.DataBind();

        //    }

        //    if (CmbBuscarAsignación.SelectedValue != "0" && CmbBuscarServicio.SelectedValue == "0")
        //    {
        //        ds = oinfousuario.ConsultarInfoaUsuario(Convert.ToInt32(CmbBuscarAsignación.SelectedValue), Convert.ToInt32(CmbBuscarServicio.SelectedValue), 3);
        //        //se llena Grilla en la consulta
        //        InfoaUsuarios.DataSource = ds.Tables[0];
        //        InfoaUsuarios.DataBind();

        //    }
        //    if (CmbBuscarAsignación.SelectedValue != "0" && CmbBuscarServicio.SelectedValue != "0")
        //    {
        //        ds = oinfousuario.ConsultarInfoaUsuario(Convert.ToInt32(CmbBuscarAsignación.SelectedValue), Convert.ToInt32(CmbBuscarServicio.SelectedValue), 4);
        //        //se llena Grilla en la consulta
        //        InfoaUsuarios.DataSource = ds.Tables[0];
        //        InfoaUsuarios.DataBind();

        //    }
        //    if (CmbBuscarAsignación.SelectedValue == "0" && CmbBuscarServicio.SelectedValue != "0")
        //    {
        //        ds = oinfousuario.ConsultarInfoaUsuario(Convert.ToInt32(CmbBuscarAsignación.SelectedValue), Convert.ToInt32(CmbBuscarServicio.SelectedValue), 5);
        //        //se llena Grilla en la consulta
        //        InfoaUsuarios.DataSource = ds.Tables[0];
        //        InfoaUsuarios.DataBind();

        //    }
        //   this.Session["dsinfousuarios"] = ds.Tables[0];
        //    ds = null;

        //}
        private void comboSubcanalXcanal()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSUBCANALXCANAL", cmbCanalUsu.SelectedValue);
            //Se llena submarcas de productos en productos
            if (ds.Tables[0].Rows.Count == 1)
            {
                cmbSubCanalUsu.DataSource = ds;
                cmbSubCanalUsu.DataTextField = "Name_subchannel";
                cmbSubCanalUsu.DataValueField = "cod_subchannel";
                cmbSubCanalUsu.DataBind();
                cmbSubNivel.Items.Insert(0, new ListItem("<No aplica ...>", "0"));
            }
            else {
                cmbSubCanalUsu.DataSource = ds;
                cmbSubCanalUsu.DataTextField = "Name_subchannel";
                cmbSubCanalUsu.DataValueField = "cod_subchannel";
                cmbSubCanalUsu.DataBind();
            }
            ds = null;

        }
        private void llenaconsulCheckinforme()
        {
            DataSet ds = new DataSet();
            /// consulta los informes que se encuentran en el checkinforme y los que fueron creados para ese usuario de ese cliente, canal y servicio
            ds = owsadministrativo.ConsultaInformedeInfoUsu(Convert.ToInt32(sbcliente), Convert.ToInt32(sUsuario), sbCanal, Convert.ToInt32(sBServicio), 0);
            //ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_CONSULTAINFORMES_CLIE_USERS_REPORTS", Convert.ToInt32(sbcliente), Convert.ToInt32(sUsuario), sbCanal, Convert.ToInt32(sBServicio));
            for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                for (int j = 0; j <= Checkinforme.Items.Count - 1; j++)
                {
                    string tabla = ds.Tables[0].Rows[i][0].ToString().Trim();
                    string ch = Checkinforme.Items[j].Value;
                    string estado = ds.Tables[0].Rows[i][1].ToString().Trim();
                    if (ds.Tables[0].Rows[i][0].ToString().Trim() == Checkinforme.Items[j].Value)
                    {  ///compara los que se encuentran en el check y los de la tabla si son iguales los chulea si no los deja en false
                        if (ds.Tables[0].Rows[i][1].ToString().Trim() == "True")
                        {
                            Checkinforme.Items[j].Selected = true;
                        }
                        else
                        {
                            Checkinforme.Items[j].Selected = false;
                        }
                    }
                }
            }
            this.Session["dsinformesxusuario"] = ds;
            ds = null;
        }
        private void llenaconsulCheckCiudades()
        {
            /// consulta los informes que se encuentran en el checkCiudades y los que fueron creados para ese id_userinforme y debe traer las ciudades de la tabla City_User_Reports al registro consultado de Clie_User_Reports
            DataSet ds1 = new DataSet();
            //ds1 = owsadministrativo.ConsultaInformedeInfoUsu(Convert.ToInt32(sbcliente), Convert.ToInt32(sUsuario), sbCanal, Convert.ToInt32(sBServicio), 0);
            ds1 = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_CONSULTACIUDADES_CITY_USERS_REPORTS", Convert.ToInt32(cmbBCliCity.SelectedValue), Convert.ToInt32(cmbBUSUCity.SelectedValue), cmbBCanalCity.SelectedValue, Convert.ToInt32(cmbBservicioCity.SelectedValue), Convert.ToInt32(cmbBreport.SelectedValue));

            for (int i = 0; i <= ds1.Tables[0].Rows.Count - 1; i++)
            {
                for (int j = 0; j <= CheckCiudades.Items.Count - 1; j++)
                {

                    string tabla1 = ds1.Tables[0].Rows[i][0].ToString().Trim();
                    string ch1 = CheckCiudades.Items[j].Value;
                    string estado1 = ds1.Tables[0].Rows[i][1].ToString().Trim();
                    if (ds1.Tables[0].Rows[i][0].ToString().Trim() == CheckCiudades.Items[j].Value)
                    { ///compara los que se encuentran en el check y los de la tabla si son iguales los chulea si no los deja en false(Nunca entra por aca, nunca chulea nada)
                        if (ds1.Tables[0].Rows[i][1].ToString().Trim() == "True")
                        {
                            CheckCiudades.Items[j].Selected = true;
                            j = CheckCiudades.Items.Count - 1;
                        }
                        else
                        {
                            CheckCiudades.Items[j].Selected = false;
                        }
                    }
                }
            }
            this.Session["dsCiudadesxusuario"] = ds1;
            ds1 = null;
        }
        private void consultaUltimoIdUserreport()
        {
            //Consulta ultimo regitro de la tabla Clie_User_Reports al que se va a insertar las ciudades seleccionadas.
            DataTable dt = new DataTable();
            dt = owsadministrativo.ConsultaUltimoiddeClieInfoUser();
            this.Session["id_userinforme"] = dt.Rows[0]["id_userinforme"].ToString().Trim();
            dt = null;
        }
        private void comboSubNivelXSubcanal()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSUBNIVELXSUBCANAL", cmbSubCanalUsu.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                //Se llena submarcas de productos en productos
                cmbSubNivel.DataSource = ds;
                cmbSubNivel.DataTextField = "name_Subnivel";
                cmbSubNivel.DataValueField = "cod_Subnivel";
                cmbSubNivel.DataBind();
            }
            else
                cmbSubNivel.Items.Insert(0, new ListItem("<No aplica ...>", "0"));
            ds = null;

        }


        private void SavelimpiarControlesReportOficina()
        {
            //TxtCodROf.Text = "";
            cmbClienteRO.Text = "0";
            CmbReporteOficina.Text = "0";
            ChekROficinas.Items.Clear();
            RbtERficina.Items[0].Selected = true;
            RbtERficina.Items[1].Selected = false;

            cmbBCliRO.Text = "0";
            cmbBReporRO.Text = "0";            

        }
        private void activarControlesReportOficina()
        {
            //TxtCodROf.Enabled = false;
            cmbClienteRO.Enabled = true;
            CmbReporteOficina.Enabled = true;
            ChekROficinas.Enabled = true;
            RbtERficina.Enabled = false;
            Panel_informe.Enabled = false;
            Panel_info_Servicio.Enabled = false;
            PanelUsuariovsInforme.Enabled = false;
            Tab_Report_Channel.Enabled = false;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Reporte_Oficina.Enabled = true;
            TabGestionReporte.Enabled = false;

        }
        private void desactivarControlesReportOficina()
        {
            //TxtCodROf.Enabled = false;
            cmbClienteRO.Enabled = false;
            CmbReporteOficina.Enabled = false;
            ChekROficinas.Enabled = false;
            RbtERficina.Enabled = false;
            Tab_Reporte_Oficina.Enabled = true;
            Panel_informe.Enabled = true;
            Panel_info_Servicio.Enabled = true;
            PanelUsuariovsInforme.Enabled = true;
            Tab_Report_Channel.Enabled = true;
            TabAsignaciónCobertura.Enabled = true;
            TabGestionReporte.Enabled = true;
        }
        private void crearActivarbotonesReportOficina()
        {
            CrearRO.Visible = false;
            GuardarRO.Visible = true;
            ConsultarRO.Visible = false;
            EditarRO.Visible = false;
            ActualizarRO.Visible = false;
            CancelarRO.Visible = true;


        }
        private void saveActivarbotonesReportOficina()
        {
           
            CrearRO.Visible = true;
            GuardarRO.Visible = false;
            ConsultarRO.Visible = true;
            EditarRO.Visible = false;
            ActualizarRO.Visible = false;
            CancelarRO.Visible = true;

        }
        private void EditarActivarbotonesReportOficina()
        {
            CrearRO.Visible = false;
            GuardarRO.Visible = false;
            ConsultarRO.Visible =true;
            EditarRO.Visible = false;
            ActualizarRO.Visible = true;
            CancelarRO.Visible = true;         

        }
        private void EditarActivarControlesReportOficina()
        {
            //TxtCodROf.Enabled = false;
            cmbClienteRO.Enabled = false;
            CmbReporteOficina.Enabled = false;
            ChekROficinas.Enabled = true;
            RbtERficina.Enabled = false;
            Panel_informe.Enabled = false;
            Panel_info_Servicio.Enabled = false;
            PanelUsuariovsInforme.Enabled = false;
            Tab_Report_Channel.Enabled = false;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Reporte_Oficina.Enabled = true;


        }
        private void BuscarActivarbotnesReportOficina()
        {
     
            CrearRO.Visible = false;
            GuardarRO.Visible = false;
            ConsultarRO.Visible = true;
            EditarRO.Visible = true;
            ActualizarRO.Visible = false;
            CancelarRO.Visible = true;
            
        }
        private void llenacomboClienteRO()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosAsignacionReportOficina(0);
            //se llena Combo de servicio segun Canal seleccionado
            cmbClienteRO.DataSource = ds.Tables[0];
            cmbClienteRO.DataTextField = "Company_Name";
            cmbClienteRO.DataValueField = "Company_id";
            cmbClienteRO.DataBind();
            cmbClienteRO.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void llenacomboReportXClienteRO()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosAsignacionReportOficina(Convert.ToInt32(cmbClienteRO.SelectedValue));
            //se llena Combo de servicio segun Canal seleccionado
            CmbReporteOficina.DataSource = ds.Tables[1];
            CmbReporteOficina.DataTextField = "Report_NameReport";
            CmbReporteOficina.DataValueField = "Report_Id";
            CmbReporteOficina.DataBind();
            CmbReporteOficina.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void llenacheckOfcinasXClienteRO()
        {

            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosAsignacionReportOficina(Convert.ToInt32(cmbClienteRO.SelectedValue));
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ChekROficinas.DataSource = ds.Tables[2];
                    ChekROficinas.DataTextField = "Name_Oficina";
                    ChekROficinas.DataValueField = "cod_Oficina";
                    ChekROficinas.DataBind();
                    //ChekROficinas.Items.Remove(ChekROficinas.Items[0]);
                    ds = null;
                }
                else
                {
                    ChekROficinas.Items.Clear();
                }
            }
                    
        }
        private void llenacomboBuscarClienteRO()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosConsultaAsignacionReportOficina(0);
            //se llena Combo de servicio segun Canal seleccionado
            cmbBCliRO.DataSource = ds.Tables[0];
            cmbBCliRO.DataTextField = "Company_Name";
            cmbBCliRO.DataValueField = "Company_id";
            cmbBCliRO.DataBind();
            cmbBCliRO.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void llenacomboBuscarReportXClienteRO()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosConsultaAsignacionReportOficina(Convert.ToInt32(cmbBCliRO.SelectedValue));

            cmbBReporRO.DataSource = ds.Tables[1];
            cmbBReporRO.DataTextField = "Report_NameReport";
            cmbBReporRO.DataValueField = "Report_Id";
            cmbBReporRO.DataBind();
            cmbBReporRO.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void llenaconsulCheckReportOficinas()
        {
            DataSet ds1 = new DataSet();
            ds1 = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_CONSULTAREPORT_OFICINA", Convert.ToInt32(cmbBCliRO.SelectedValue), Convert.ToInt32(cmbBReporRO.SelectedValue));

            for (int i = 0; i <= ds1.Tables[0].Rows.Count - 1; i++)
            {
                for (int j = 0; j <= ChekROficinas.Items.Count - 1; j++)
                {

                    string tabla1 = ds1.Tables[0].Rows[i][0].ToString().Trim();
                    string ch1 = ChekROficinas.Items[j].Value;
                    string estado1 = ds1.Tables[0].Rows[i][1].ToString().Trim();
                    if (ds1.Tables[0].Rows[i][0].ToString().Trim() == ChekROficinas.Items[j].Value)
                    { 
                        if (ds1.Tables[0].Rows[i][1].ToString().Trim() == "True")
                        {
                            ChekROficinas.Items[j].Selected = true;
                            j = ChekROficinas.Items.Count - 1;
                        }
                        else
                        {
                            ChekROficinas.Items[j].Selected = false;
                        }
                    }
                }
            }
            this.Session["dsOficinasXReporte"] = ds1;
            ds1 = null;
        }
        private void DefinicionEstadoReporteOficina()
        {

            if (ChekROficinas.SelectedIndex != -1)
            {
                // habilita estado (Activado)
                RbtERficina.Items[0].Selected = true;
                // deshabilita estado (Deshabilitado)
                RbtERficina.Items[1].Selected = false;

            }

            else
            {
                RbtERficina.Items[0].Selected = false;
                RbtERficina.Items[1].Selected = true;

            }

        }
        private void llenaOficinaXClienteyReportRO()
        {

            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENEROFICINAS", Convert.ToInt32(cmbClienteRO.SelectedValue));
           //Convert.ToInt32(CmbReporteOficina.SelectedValue)
            if (dt.Rows.Count > 0)
            {
                dato = true;
                //se carga check de ciudades segun usuario seleccionado por pais
                ChekROficinas.DataSource = dt;
                ChekROficinas.DataValueField = "cod_Oficina";
                ChekROficinas.DataTextField = "Name_Oficina";
                ChekROficinas.DataBind();

            }
            else
            {
                dato = false;
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "no existe Asociación de oficinas para este Reporte";
                MensajeAlerta();
                //TxtCodROf.Text = "";
                cmbClienteRO.Text = "0";
                CmbReporteOficina.Items.Clear();
                ChekROficinas.Items.Clear();
            }
        }
     
        

        private void SavelimpiarControlesACity()
        {
            TxtcodigoClieUR.Text = "";
            cmbClientecity.Text = "0";
            cmbUsuarioCity.Text = "0";
            cmbCanalCity.Text="0";
            cmbServicioCity.Text = "0";
            cmbReporteCity.Text = "0";
            CheckCiudades.Items.Clear();
            
            //for (int i = 0; i <= CheckCiudades.Items.Count - 1; i++)
            //{
            //    CheckCiudades.Items[i].Selected = false;
            //}
            
            RBTasigCity.Items[0].Selected = true;
            RBTasigCity.Items[1].Selected = false;
            
            cmbBCliCity.Text = "0";
            cmbBUSUCity.Items.Clear();
            cmbBCanalCity.Items.Clear();
            cmbBservicioCity.Items.Clear();
            cmbBreport.Items.Clear();
        }
        private void activarControlesACity()
        {
            cmbClientecity.Enabled = true;
            cmbUsuarioCity.Enabled = true;
            cmbReporteCity.Enabled = true;
            cmbCanalCity.Enabled = true;
            cmbServicioCity.Enabled = true;
            CheckCiudades.Enabled = true;
            chkTodos.Enabled = true;
            RBTasigCity.Enabled = false;
            Panel_informe.Enabled = false;
            Panel_info_Servicio.Enabled = false;
            PanelUsuariovsInforme.Enabled = false;
            TabAsignaciónCobertura.Enabled=true;
            Tab_Report_Channel.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;
            TabGestionReporte.Enabled = false;
        }
        private void desactivarControlesACity()
        {
            cmbClientecity.Enabled = false;
            cmbUsuarioCity.Enabled = false;
            cmbCanalCity.Enabled = false;
            cmbServicioCity.Enabled = false;
            cmbReporteCity.Enabled = false;
            CheckCiudades.Enabled = false;
            chkTodos.Enabled = false;
            RBTasigCity.Items[0].Selected = false;
            Panel_informe.Enabled = true;
            Panel_info_Servicio.Enabled = true;
            PanelUsuariovsInforme.Enabled = true;
            TabAsignaciónCobertura.Enabled = true;
            Tab_Report_Channel.Enabled = true;
            Tab_Reporte_Oficina.Enabled = true;
            TabGestionReporte.Enabled = true;
        }
        private void crearActivarbotonesACity()
        {

            btnCrearCity.Visible=false;
            BtnGuardarcity.Visible=true;
            BtnConsultarCity.Visible=false;
            BtnEditarCity.Visible=false;
            BtnActualizarCity.Visible=false;
            BtnCancelarCity.Visible = true;


        }
        private void saveActivarbotonesACity()
        {

            btnCrearCity.Visible = true;
            BtnGuardarcity.Visible = false;
            BtnConsultarCity.Visible = true;
            BtnEditarCity.Visible = false;
            BtnActualizarCity.Visible = false;
            BtnCancelarCity.Visible = true;

           
        }
        private void EditarActivarbotonesACity()
        {

            btnCrearCity.Visible = false;
            BtnGuardarcity.Visible = false;
            BtnConsultarCity.Visible = true;
            BtnEditarCity.Visible = false;
            BtnActualizarCity.Visible = true;
            BtnCancelarCity.Visible = true;

         
        }
        private void EditarActivarControlesACity()
        {
            cmbClientecity.Enabled = false;
            cmbUsuarioCity.Enabled = false;
            cmbCanalCity.Enabled = false;
            cmbServicioCity.Enabled = false;
            cmbReporteCity.Enabled =false;
            CheckCiudades.Enabled = true;
            chkTodos.Enabled = true;
            RBTasigCity.Enabled = false;
            Panel_informe.Enabled = false;
            Panel_info_Servicio.Enabled = false;
            PanelUsuariovsInforme.Enabled = false;
            TabAsignaciónCobertura.Enabled = true;
            Tab_Report_Channel.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;
        }
        private void BuscarActivarbotnesACity()
        {
            btnCrearCity.Visible = false;
            BtnGuardarcity.Visible = false;
            BtnConsultarCity.Visible = true;
            BtnEditarCity.Visible = true;
            BtnActualizarCity.Visible = false;
            BtnCancelarCity.Visible = true;

        }
        private void ComboClienteCity()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosAsignacionCobertura(0, 0,"0", 0, 0);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //se llena combo canales en Asignación de informe a Ususario por Cliente seleccionado
                    cmbClientecity.DataSource = ds.Tables[0];
                    cmbClientecity.DataTextField = "Company_Name";
                    cmbClientecity.DataValueField = "company_id";
                    cmbClientecity.DataBind();
                    cmbClientecity.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                    ds = null;
                }
                else
                {
                    cmbClientecity.Items.Clear(); 
                }
            }
        }
        private void ComboUsuarioCity()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosAsignacionCobertura(Convert.ToInt32(cmbClientecity.SelectedValue), 0, "0", 0, 0);
            if (ds != null)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    //se llena combo usuarios en asignación de cobertura
                    cmbUsuarioCity.DataSource = ds.Tables[1];
                    cmbUsuarioCity.DataTextField = "name_user";
                    cmbUsuarioCity.DataValueField = "Person_id";
                    cmbUsuarioCity.DataBind();
                    cmbUsuarioCity.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                    ds = null;
                }
                else
                { cmbUsuarioCity.Items.Clear();
                }
            }
        }
        private void ComboCanalxUsuCity()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosAsignacionCobertura(Convert.ToInt32(cmbClientecity.SelectedValue), Convert.ToInt32(cmbUsuarioCity.SelectedValue), "0", 0, 0);
            if (ds != null)
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    //se llena combo canales en Asignación de cobertura
                    cmbCanalCity.DataSource = ds.Tables[2];
                    cmbCanalCity.DataTextField = "Channel_Name";
                    cmbCanalCity.DataValueField = "cod_Channel";
                    cmbCanalCity.DataBind();
                    cmbCanalCity.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                    ds = null;
                }
                else {
                    cmbCanalCity.Items.Clear();
                }
            }
        }
        private void ComboServicioxCanalCity()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosAsignacionCobertura(Convert.ToInt32(cmbClientecity.SelectedValue), Convert.ToInt32(cmbUsuarioCity.SelectedValue), cmbCanalCity.SelectedValue, 0, 0);
            if (ds != null)
            {
                if (ds.Tables[3].Rows.Count > 0)
                {
                    //se llena combo canales en Asignación de cobertura
                    cmbServicioCity.DataSource = ds.Tables[3];
                    cmbServicioCity.DataTextField = "Strategy_Name";
                    cmbServicioCity.DataValueField = "cod_Strategy";
                    cmbServicioCity.DataBind();
                    cmbServicioCity.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                    ds = null;
                }
                else 
                { 
                    cmbServicioCity.Items.Clear();
                }
            }
        }
        private void ComboReportCity()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosAsignacionCobertura(Convert.ToInt32(cmbClientecity.SelectedValue), Convert.ToInt32(cmbUsuarioCity.SelectedValue), cmbCanalCity.SelectedValue, Convert.ToInt32(cmbServicioCity.SelectedValue),0);
            if (ds != null)
            {
                if (ds.Tables[4].Rows.Count > 0)
                {
                    //se llena combo canales en Asignación de informe a Ususario por Cliente seleccionado
                    cmbReporteCity.DataSource = ds.Tables[4];
                    cmbReporteCity.DataTextField = "Report_NameReport";
                    cmbReporteCity.DataValueField = "Report_Id";
                    cmbReporteCity.DataBind();
                    cmbReporteCity.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                    ds = null;
                }
                else
                {
                    cmbReporteCity.Items.Clear();
                }
            }
        }
        private void Combotextid_userinforme()
        {
            DataSet ds = new DataSet();
          
                    ds = owsadministrativo.llenaCombosAsignacionCobertura(Convert.ToInt32(cmbClientecity.SelectedValue), Convert.ToInt32(cmbUsuarioCity.SelectedValue), cmbCanalCity.SelectedValue, Convert.ToInt32(cmbServicioCity.SelectedValue), Convert.ToInt32(cmbReporteCity.SelectedValue));
                    TxtcodigoClieUR.Text = ds.Tables[5].Rows[0]["id_userinforme"].ToString().Trim();            
            
            
            ds = null;
        }
        private void consultapaisXCliente()
        {
            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_CONSULTAPAISXCLIENTE", cmbClientecity.SelectedValue);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    this.Session["id_Pais"] = dt.Rows[0]["cod_Country"].ToString().Trim();
                    dt = null;
                }
            }
        }
        private void llenaOficinaXClienteyReport()
        {
           
            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENERCITY", Convert.ToInt32(cmbClientecity.SelectedValue),Convert.ToInt32(cmbReporteCity.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                dato = true;
                //se carga check de ciudades segun usuario seleccionado por pais
                CheckCiudades.DataSource = dt;
                CheckCiudades.DataValueField = "cod_Oficina";
                CheckCiudades.DataTextField = "Name_Oficina";
                CheckCiudades.DataBind();
                
            }
            else
            {
                dato = false;
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "no existe Asignación de este reporte a oficinas. Porfavor ingrese a pestaña de Asignación de Reporte a Oficinas y realicela Asignación.";
                MensajeAlerta();
                cmbClientecity.Text = "0";
                cmbUsuarioCity.Items.Clear();
                cmbCanalCity.Items.Clear();
                cmbReporteCity.Items.Clear();
                cmbServicioCity.Items.Clear();
                CheckCiudades.Items.Clear();
            }
        }
        private void ComboBuscarClienteCity()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosConsultaAsignacionCobertura(0, 0, "0", 0, 0);

            //se llena combo canales en Asignación de informe a Ususario por Cliente seleccionado
            cmbBCliCity.DataSource = ds.Tables[0];
            cmbBCliCity.DataTextField = "Company_Name";
            cmbBCliCity.DataValueField = "company_id";
            cmbBCliCity.DataBind();
            cmbBCliCity.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void ComboBuscarUsuarioCity()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosConsultaAsignacionCobertura(Convert.ToInt32(cmbBCliCity.SelectedValue), 0, "0", 0, 0);

            //se llena combo usuarios en asignación de cobertura
            cmbBUSUCity.DataSource = ds.Tables[1];
            cmbBUSUCity.DataTextField = "name_user";
            cmbBUSUCity.DataValueField = "Person_id";
            cmbBUSUCity.DataBind();
            cmbBUSUCity.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void ComboBuscarCanalxUsuCity()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosConsultaAsignacionCobertura(Convert.ToInt32(cmbBCliCity.SelectedValue), Convert.ToInt32(cmbBUSUCity.SelectedValue), "0", 0, 0);

            //se llena combo canales en Asignación de cobertura
            cmbBCanalCity.DataSource = ds.Tables[2];
            cmbBCanalCity.DataTextField = "Channel_Name";
            cmbBCanalCity.DataValueField = "cod_Channel";
            cmbBCanalCity.DataBind();
            cmbBCanalCity.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void ComboBuscarServicioxCanalCity()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosConsultaAsignacionCobertura(Convert.ToInt32(cmbBCliCity.SelectedValue), Convert.ToInt32(cmbBUSUCity.SelectedValue), cmbBCanalCity.SelectedValue, 0, 0);

            //se llena combo canales en Asignación de cobertura
            cmbBservicioCity.DataSource = ds.Tables[3];
            cmbBservicioCity.DataTextField = "Strategy_Name";
            cmbBservicioCity.DataValueField = "cod_Strategy";
            cmbBservicioCity.DataBind();
            cmbBservicioCity.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void ComboBReportBuscarCity()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosConsultaAsignacionCobertura(Convert.ToInt32(cmbBCliCity.SelectedValue), Convert.ToInt32(cmbBUSUCity.SelectedValue), cmbBCanalCity.SelectedValue, Convert.ToInt32(cmbBservicioCity.SelectedValue), 0);

            //se llena combo canales en Asignación de informe a Ususario por Cliente seleccionado
            cmbBreport.DataSource = ds.Tables[4];
            cmbBreport.DataTextField = "Report_NameReport";
            cmbBreport.DataValueField = "Report_Id";
            cmbBreport.DataBind();
            cmbBreport.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void DefinicionEstado()
        {

            if (Checkinforme.SelectedIndex != -1)
            {
                // habilita estado (Activado)
                RadioInfoUsu.Items[0].Selected = true;
                // deshabilita estado (Deshabilitado)
                RadioInfoUsu.Items[1].Selected = false;

            }

            else
            {
                RadioInfoUsu.Items[0].Selected = false;
                RadioInfoUsu.Items[1].Selected = true;

            }

        }
        private void DefinicionEstadoOficina()
        {

            if (CheckCiudades.SelectedIndex != -1)
            {
                // habilita estado (Activado)
                RBTasigCity.Items[0].Selected = true;
                // deshabilita estado (Deshabilitado)
                RBTasigCity.Items[1].Selected = false;

            }

            else
            {
                RBTasigCity.Items[0].Selected = false;
                RBTasigCity.Items[1].Selected = true;

            }

        }





        private void SavelimpiarControlesGR()
        {

            TxtCodgoGR.Text = "";
            cmbCompanyGR.Text = "0";
            CmbCanalGR.Text = "0";
            cmbReportGv.Text = "0";
            CmbtipoReporte.Text = "0";

            RadioButtonGR.Items[0].Selected = true;
            RadioButtonGR.Items[1].Selected = false;
            
            chkVCategory.Checked = false;
            chkVMarca.Checked = false;
            chkVSubMarca.Checked = false;
            chkVFamilia.Checked = false;
            chkVProducto.Checked = false;

            CMBBClienteRG.Text = "0";
            CmbCanalBRG.Items.Clear();
            CMBReporRG.Items.Clear();

        }
        private void activarControlesGR()
        {
            cmbCompanyGR.Enabled = true;
            CmbCanalGR.Enabled = true;
            cmbReportGv.Enabled = true;
            CmbtipoReporte.Enabled = true;
            chkVCategory.Enabled = true;
            chkVMarca.Enabled = true;
            chkVSubMarca.Enabled = true;
            chkVFamilia.Enabled = true;
            chkVSubFamilia.Enabled = true;
            chkVProducto.Enabled = true;
            Panel_informe.Enabled = false;
            TabGestionReporte.Enabled = true;
            Panel_info_Servicio.Enabled = false;
            PanelUsuariovsInforme.Enabled = false;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Report_Channel.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;
        }
        private void desactivarControlesGR()
        {
            cmbCompanyGR.Enabled = false;
            CmbCanalGR.Enabled = false;
            cmbReportGv.Enabled = false;
            CmbtipoReporte.Enabled = false;
            chkVCategory.Enabled = false;
            chkVMarca.Enabled = false;
            chkVSubMarca.Enabled = false;
            chkVFamilia.Enabled = false;
            chkVSubFamilia.Enabled = false;
            chkVProducto.Enabled = false;
            Panel_informe.Enabled = true;
            Panel_info_Servicio.Enabled = true;
            TabGestionReporte.Enabled = true;
            PanelUsuariovsInforme.Enabled = true;
            TabAsignaciónCobertura.Enabled = true;
            Tab_Report_Channel.Enabled = true;
            Tab_Reporte_Oficina.Enabled = true;
        }

        private void crearActivarbotonesGR()
        {

            BtnCrearGR.Visible = false;
            BtnGuardarGR.Visible = true;
            BtnConsultarGR.Visible = false;
            BtnEditarGR.Visible = false;
            BtnActualizarGR.Visible = false;
            BtnCancelarGR.Visible = true;
            primero.Visible = false;
            sig.Visible = false;
            ant.Visible = false;
            ultimo.Visible = false;


        }
        private void saveActivarbotonesGR()
        {
          
            BtnCrearGR.Visible = true;
            BtnGuardarGR.Visible = false;
            BtnConsultarGR.Visible = true;
            BtnEditarGR.Visible = false;
            BtnActualizarGR.Visible = false;
            BtnCancelarGR.Visible = true;
            primero.Visible = false;
            sig.Visible = false;
            ant.Visible = false;
            ultimo.Visible = false;
            btneliminar.Visible = false;

        }
        private void EditarActivarbotonesGR()
        {
       
            BtnCrearGR.Visible = false;
            BtnGuardarGR.Visible = false;
            BtnConsultarGR.Visible = true;
            BtnEditarGR.Visible = false;
            BtnActualizarGR.Visible = true;
            BtnCancelarGR.Visible = true;
            primero.Visible = false;
            sig.Visible = false;
            ant.Visible = false;
            ultimo.Visible = false;
        }
        private void EditarActivarControleGR()
        {

            TxtCodgoGR.Enabled = false;
            cmbCompanyGR.Enabled = false;
            CmbCanalGR.Enabled = false;
            cmbReportGv.Enabled = false;
            CmbtipoReporte.Enabled = false;
            chkVCategory.Enabled = true;
            chkVMarca.Enabled = true;
            chkVSubMarca.Enabled = true;
            chkVFamilia.Enabled = true;
            chkVProducto.Enabled = true;
            RadioButtonGR.Enabled = true;
            TabGestionReporte.Enabled = true;
            Panel_informe.Enabled = false;
            Panel_info_Servicio.Enabled = false;
            PanelUsuariovsInforme.Enabled = false;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Report_Channel.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;
        }
        private void BuscarActivarbotnesGR()
        {
      
            BtnCrearGR.Visible = false;
            BtnGuardarGR.Visible = false;
            BtnConsultarGR.Visible = true;
            BtnEditarGR.Visible = true;
            BtnActualizarGR.Visible = false;
            BtnCancelarGR.Visible = true;
            primero.Visible = false;
            sig.Visible = false;
            ant.Visible = false;
            ultimo.Visible = false;
            btneliminar.Visible = true;           
        }

        /**Método para eliminar  un registro de la tabla
         * 30/05/2011 - Angel Ortiz
         * */

        private void eliminarparamxreport(int code) 
        {
            //int result = 0;
            try
            {
                oConn.ejecutarSinRetorno("UP_WEBXPLORA_AD_ELIMINAREPORTEPARAMETRIZADO", code);
                Alertas.CssClass = "MensajesCorrecto";
                LblFaltantes.Text = "Se ha eliminado el registro " + code + " correctamente.";
                MensajeAlerta();
                return;
            }
            catch(Exception  e) {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "No se puede eliminar el registro";
                MensajeAlerta();
            }           
   
        }

        private void comboclienteenGR(DropDownList ddlCliente)
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en en maestro de Asignación de informes a usuario
            ddlCliente.DataSource = ds;
            ddlCliente.DataTextField = "Company_Name";
            ddlCliente.DataValueField = "Company_id";
            ddlCliente.DataBind();


        }
        private void ComboCanalXClienteGR(DropDownList ddlCliente, DropDownList ddlCanal)
        {
            DataTable dt = new DataTable();
            dt = owsadministrativo.LLenacomboCanalporCliente(Convert.ToInt32(ddlCliente.SelectedValue));
            //se llena combo canales en Asignación de informe a Ususario por Cliente seleccionado
            ddlCanal.DataSource = dt;
            ddlCanal.DataTextField = "Channel_Name";
            ddlCanal.DataValueField = "cod_Channel";
            ddlCanal.DataBind();
            ddlCanal.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
          
            dt = null;
        }
        private void LlenacomboReporteGR(DropDownList ddlCliente, DropDownList ddlCanal, DropDownList ddlReporte)
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENAINFORMEGR", Convert.ToInt32(ddlCliente.SelectedValue), ddlCanal.SelectedValue);
            //se llena cliente en en maestro de Asignación de informes a usuario
            ddlReporte.DataSource = ds;
            ddlReporte.DataTextField = "Report_NameReport";
            ddlReporte.DataValueField = "Report_Id";
            ddlReporte.DataBind();
        }

        //inicio carga de Combos de busqueda de informe x servicio
        private void comboclienteBuscarenGR()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet ("UP_WEBXPLORA_AD_LLENACOMBOSCONSULTASGPR", 0, "0");        
            CMBBClienteRG.DataSource = ds.Tables[0];
            CMBBClienteRG.DataTextField = "Company_Name";
            CMBBClienteRG.DataValueField = "Company_id";
            CMBBClienteRG.DataBind();
        }

        private void ComboCanalXClienteBuscarGR()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSCONSULTASGPR",Convert.ToInt32(CMBBClienteRG.SelectedValue), "0");            
            CmbCanalBRG.DataSource = ds.Tables[1];
            CmbCanalBRG.DataTextField = "Channel_Name";
            CmbCanalBRG.DataValueField = "cod_Channel";
            CmbCanalBRG.DataBind();
            ds = null;
        }

        private void LlenacomboReporteBuscarGR()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSCONSULTASGPR", Convert.ToInt32(CMBBClienteRG.SelectedValue), CmbCanalBRG.SelectedValue);
            //se llena cliente en en maestro de Asignación de informes a usuario
            CMBReporRG.DataSource = ds.Tables[2];
            CMBReporRG.DataTextField = "Report_NameReport";
            CMBReporRG.DataValueField = "Report_Id";
            CMBReporRG.DataBind();
            ds = null;
        }

        // fin carga de Combos de busqueda de informe x servicio

        private void LlenacomboTipoReporteGR(DropDownList ddlCliente, DropDownList ddlTipoReporte)
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOTYPOREPORTEGR", Convert.ToInt32(ddlCliente.SelectedValue));
            //se llena cliente en en maestro de Asignación de informes a usuario
            ddlTipoReporte.DataSource = ds;
            ddlTipoReporte.DataTextField = "TipoReporte_Descripcion";
            ddlTipoReporte.DataValueField = "id_Tipo_Reporte";
            ddlTipoReporte.DataBind();
        }

        private void MensajeConfirmacion()
        {
            ModalPopupConfirm.Show();
            btnCancel.Focus();
        }
   

        private void MensajeAlerta()
        {
            ModalPopupAlertas.Show();
            BtnAceptarAlert.Focus();
            //ScriptManager.RegisterStartupScript(
            //    this, this.GetType(), "myscript", "alert('Debe ingresar todos los parametros con (*)');", true);
        }

        #endregion

        #region Informes
        protected void btnCrearReporte_Click(object sender, EventArgs e)
        {
        
            activarControlesInforme();
            crearActivarbotonesInforme();
            SavelimpiarControlesInforme();

        }
        protected void BtnGuardarReporte_Click(object sender, EventArgs e)
        {
            this.Session["Nota"] = "";
            this.Session["Nota1"] = "";
            LblFaltantes.Text = "";
            txtNomReport.Text = txtNomReport.Text.TrimStart();
            TxtDescReport.Text = TxtDescReport.Text.TrimStart();
            if (txtNomReport.Text == "0" || TxtDescReport.Text == "")
            {
                if (txtNomReport.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Nombre informe";
                }
                if (TxtDescReport.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            //for (int i = 0; i <= ChkTipInf.Items.Count - 1; i++)
            //{
            //    if (ChkTipInf.Items[i].Selected == false)
            //    {
            //        iid_TypeReport = 0;
            //    }
            //    else
            //    {
            //        iid_TypeReport = 1;
            //        i = ChkTipInf.Items.Count - 1;
            //    }
            //}

            //if (iid_TypeReport == 0)
            //{
            //    //imagenalert.Src = "../../images/alert1.gif";
            //    //LblAlert.Text = "Sr. Usuario";
            //    //LblFaltantes.Text = "Debe habilitar al menos un tipo de informe";
            //    Alertas.CssClass = "MensajesError";
            //    LblFaltantes.Text = "Debe habilitar al menos un tipo de informe";
            //    MensajeAlerta();
            //    ModalPopupAlertas.Show();
            //    BtnAceptarAlert.Focus();
            //    return;
            //}


            //for (int i = 0; i <= ChkSelModulo.Items.Count - 1; i++)
            //{
            //    if (ChkSelModulo.Items[i].Selected == false)
            //    {
            //        iModulo_id = 0;
            //    }
            //    else
            //    {
            //        iModulo_id = 1;
            //        i = ChkSelModulo.Items.Count - 1;
            //    }
            //}
            //if (iModulo_id == 0)
            //{
            //    //imagenalert.Src = "../../images/alert1.gif";
            //    //LblAlert.Text = "Sr. Usuario";
            //    //LblFaltantes.Text = "Debe habilitar al menos un Módulo";
            //    Alertas.CssClass = "MensajesError";
            //    LblFaltantes.Text = "Deb;e habilital por lo menos un Modulo";
            //    MensajeAlerta();
            //    ModalPopupAlertas.Show();
            //    BtnAceptarAlert.Focus();
            //    return;
            //}

            try
            {

                estado = true;

                DAplicacion odconsureport = new DAplicacion();
                DataTable dtconsulta = odconsureport.ConsultaDuplicados(ConfigurationManager.AppSettings["Reports"], txtNomReport.Text, null, null);
                if (dtconsulta == null)
                {
                    if (txtOrderReport.Text == "")
                    {
                        txtOrderReport.Text = "0";
                    }
                    EReports oeReportes = oReports.RegistrarReportes(Convert.ToInt32(txtOrderReport.Text), txtNomReport.Text, TxtDescReport.Text.ToUpper(), estado,
                       Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    char estadoTMP;
                    if(estado == true)
                    {
                        estadoTMP = '1';
                    }
                    else
                    {
                        estadoTMP = '0';
                    }
                    oReports.registrarReportesTMP(oeReportes.reportId.ToString(), oeReportes.ReportNameReport, estadoTMP);

                    //for (int i = 0; i <= ChkTipInf.Items.Count - 1; i++)
                    //{
                    //    DataTable dtconsDupInfTipoInf = odconsureport.ConsultaDuplicados(ConfigurationManager.AppSettings["ReportestipoInf"], TxtCodReport.Text, ChkTipInf.Items[i].Value, null);
                    //    if (dtconsDupInfTipoInf == null)
                    //    {
                    //        if (ChkTipInf.Items[i].Selected == true)
                    //        {
                    //            EReports oeReportesModulo = oReports.RegistrarReportesTiposInf((Convert.ToInt32(TxtCodReport.Text)), Convert.ToInt32(ChkTipInf.Items[i].Value), estado,
                    //              Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //sReportTipInf = sReportTipInf + " ," + ChkTipInf.Items[i].Text;
                    //        //this.Session["sReportTipInf"] = sReportTipInf;
                    //        //this.Session["Nota"] = "NOTA IMPORTANTE !! : Los Tipos de Informe: " + this.Session["sReportTipInf"] + " ya se encontraban registrados en la aplicación ";
                    //    }
                    //}

                    //for (int i = 0; i <= ChkSelModulo.Items.Count - 1; i++)
                    //{
                    //DataTable dtconsDupInfMod = odconsureport.ConsultaDuplicados(ConfigurationManager.AppSettings["ReportesModulo"], TxtCodReport.Text, ChkSelModulo.Items[i].Value, null);
                    //if (dtconsDupInfMod == null)
                    //{
                    //    //if (ChkSelModulo.Items[i].Selected == true)
                    //    //{
                    //        EReports oeReportesModulo = oReports.RegistrarReportesModulos(Convert.ToInt32(TxtCodReport.Text), ChkSelModulo.Items[i].Value, estado,
                    //            Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    //    //}
                    //}
                    //else
                    //{
                    //sReportModulo = sReportModulo + " ," + ChkSelModulo.Items[i].Text;
                    //this.Session["sReportModulo"] = sReportModulo;
                    //this.Session["Nota1"] = "Los Módulos: " + this.Session["sReportModulo"] + " ya se encontraban registrados en la aplicación ";
                    //}
                    //}
                    string sInforme = "";
                    sInforme = txtNomReport.Text;
                    this.Session["sInforme"] = sInforme;
                    SavelimpiarControlesInforme();
                    LlenaComboInforme();
                    //llenaCmbTipoInfo();
                    //LlenaCmbModulo();
                    //LlenaComboInforme();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Informe " + this.Session["sInforme"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesInforme();
                    desactivarControlesInforme();
                }
                else
                {
                    string sInforme = "";
                    sInforme = txtNomReport.Text;
                    this.Session["sInforme"] = sInforme;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El informe  " + this.Session["sInforme"] + " Ya Existe";
                    MensajeAlerta();
                }
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
        //protected void CmbNomReport_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    DataTable dt = owsadministrativo.Get_ObtenerReport_Description(Convert.ToInt32(txtNomReport.Text));
        //    TxtDescReport.Text = dt.Rows[0]["Report_Description"].ToString().Trim();
        //    TxtCodReport.Text = dt.Rows[0]["Report_Id"].ToString().Trim();
        //    dt = null;

        //    DataTable oeInformesmodulos = oReports.BuscarInformeModulo(Convert.ToInt32(TxtCodReport.Text));
        //    if (oeInformesmodulos != null)
        //    {
        //        if (oeInformesmodulos.Rows.Count > 0)
        //        {
        //            for (int j = 0; j <= oeInformesmodulos.Rows.Count - 1; j++)
        //            {
        //                for (int k = 0; k <= ChkSelModulo.Items.Count - 1; k++)
        //                {
        //                    if (ChkSelModulo.Items[k].Value == oeInformesmodulos.Rows[j]["Modulo_id"].ToString().Trim())
        //                    {
        //                        ChkSelModulo.Items[k].Selected = Convert.ToBoolean(oeInformesmodulos.Rows[j]["ReportModulo_Status"].ToString().Trim());
        //                        k = ChkSelModulo.Items.Count - 1;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    oeInformesmodulos = null;

        //    DataTable oeInformesTipInf = oReports.BuscarInformetipoInf(Convert.ToInt32(TxtCodReport.Text));
        //    //if (oeInformesTipInf != null)
        //    //{
        //    //    if (oeInformesTipInf.Rows.Count > 0)
        //    //    {
        //    //        for (int j = 0; j <= oeInformesTipInf.Rows.Count - 1; j++)
        //    //        {
        //    //            for (int k = 0; k <= ChkTipInf.Items.Count - 1; k++)
        //    //            {
        //    //                if (ChkTipInf.Items[k].Value == oeInformesTipInf.Rows[j]["id_TypeReport"].ToString().Trim())
        //    //                {
        //    //                    ChkTipInf.Items[k].Selected = Convert.ToBoolean(oeInformesTipInf.Rows[j]["ReportTypeReport_Status"].ToString().Trim());
        //    //                    k = ChkTipInf.Items.Count - 1;
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    oeInformesTipInf = null;
        //}
        protected void BtnBReportes_Click(object sender, EventArgs e)
        {
            desactivarControlesInforme();
            LblFaltantes.Text = "";
            if (CmbNameReporte.Text == "0")
            {
                this.Session["mensajealert"] = "nombre de informe";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnReportes_ModalPopupExtender.Show();
                return;
            }
            BuscarActivarbotnesInforme();
            sReportid = CmbNameReporte.SelectedValue;

            DataTable oeInforme = oReports.BuscarInforme(sReportid);

            if (oeInforme != null)
            {
                if (oeInforme.Rows.Count > 0)
                {
                    CmbNameReporte.Text = "0";
                    for (int i = 0; i <= oeInforme.Rows.Count - 1; i++)
                    {
                        //LlenaComboInforme();
                        TxtCodReport.Text = oeInforme.Rows[0]["Report_Id"].ToString().Trim();
                        txtNomReport.Text = oeInforme.Rows[0]["Report_NameReport"].ToString().Trim();
                        TxtDescReport.Text = oeInforme.Rows[0]["Report_Description"].ToString().Trim();
                        estado = Convert.ToBoolean(oeInforme.Rows[0]["Report_Status"].ToString().Trim());
                        //DataTable oeInformesmodulos = oReports.BuscarInformeModulo(Convert.ToInt32(TxtCodReport.Text));
                        //if (oeInformesmodulos != null)
                        //{
                        //    if (oeInformesmodulos.Rows.Count > 0)
                        //    {
                        //        for (int j = 0; j <= oeInformesmodulos.Rows.Count - 1; j++)
                        //        {
                        //            for (int k = 0; k <= ChkSelModulo.Items.Count - 1; k++)
                        //            {
                        //                if (ChkSelModulo.Items[k].Value == oeInformesmodulos.Rows[j]["Modulo_id"].ToString().Trim())
                        //                {
                        //                    ChkSelModulo.Items[k].Selected = Convert.ToBoolean(oeInformesmodulos.Rows[j]["ReportModulo_Status"].ToString().Trim());
                        //                    k = ChkSelModulo.Items.Count - 1;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //DataTable oeInformesTipInf = oReports.BuscarInformetipoInf(Convert.ToInt32(TxtCodReport.Text));
                        //if (oeInformesTipInf != null)
                        //{
                        //    if (oeInformesTipInf.Rows.Count > 0)
                        //    {
                        //        for (int j = 0; j <= oeInformesTipInf.Rows.Count - 1; j++)
                        //        {
                        //            for (int k = 0; k <= ChkTipInf.Items.Count - 1; k++)
                        //            {
                        //                if (ChkTipInf.Items[k].Value == oeInformesTipInf.Rows[j]["id_TypeReport"].ToString().Trim())
                        //                {
                        //                    ChkTipInf.Items[k].Selected = Convert.ToBoolean(oeInformesTipInf.Rows[j]["ReportTypeReport_Status"].ToString().Trim());
                        //                    k = ChkTipInf.Items.Count - 1;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}


                        if (estado == true)
                        {
                            RBtnListStatusReport.Items[0].Selected = true;
                            RBtnListStatusReport.Items[1].Selected = false;
                        }
                        else
                        {
                            RBtnListStatusReport.Items[0].Selected = false;
                            RBtnListStatusReport.Items[1].Selected = true;
                        }
                        this.Session["tinformes"] = oeInforme;
                        this.Session["i"] = 0;
                    }
                    if (oeInforme.Rows.Count == 1)
                    {
                        btnPreg9.Visible = false;
                        btnUreg9.Visible = false;
                        btnAreg9.Visible = false;
                        btnSreg9.Visible = false;
                    }
                    else
                    {
                        btnPreg9.Visible = true;
                        btnUreg9.Visible = true;
                        btnAreg9.Visible = true;
                        btnSreg9.Visible = true;
                    }

                }
                else
                {
                    SavelimpiarControlesInforme();
                    saveActivarbotonesInforme();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnReportes_ModalPopupExtender.Show();
                }
            }
        }
        protected void btnEditReport_Click(object sender, EventArgs e)
        {
            btnEditReport.Visible = false;
            btnActualizarReporte.Visible = true;
            activarControlesInforme();
            this.Session["rept"] = txtNomReport.Text;
            
        }
        protected void btnActualizarReporte_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtDescReport.Text = TxtDescReport.Text.TrimStart();

            if (TxtDescReport.Text == "")
            {
                if (TxtDescReport.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {
                char estadotmp;
            if (RBtnListStatusReport.Items[0].Selected == true)
            {
                estado = true;
                estadotmp = '1';
            }
            else
            {
                estado = false;
                estadotmp = '0';
                DAplicacion odReports = new DAplicacion();
                DataTable dt = odReports.PermitirDeshabilitar(ConfigurationManager.AppSettings["ReportsReports_Strategit"], TxtCodReport.Text);
                if (dt != null)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No se puede Deshabilitar este registro ya que esta relacionado con el maestro de Informe Vs Servicio, por favor Verifique";
                    MensajeAlerta();
                    return;
                }
            }

               repetido = Convert.ToString(this.Session["rept"]);
               if (repetido != txtNomReport.Text)
               {
                   DAplicacion odconsureport = new DAplicacion();
                   DataTable dtconsulta = odconsureport.ConsultaDuplicados(ConfigurationManager.AppSettings["Reports"], txtNomReport.Text, null, null);
                   if (dtconsulta == null)
                   {
                       if (txtOrderReport.Text == "")
                       {
                           txtOrderReport.Text = "0";
                       }

                       EReports oeainformes = oReports.Actualizar_Reporte(Convert.ToInt32(TxtCodReport.Text.ToUpper()), Convert.ToInt32(txtOrderReport.Text), txtNomReport.Text, TxtDescReport.Text.ToUpper(), estado,
                       Convert.ToString(this.Session["sUser"]), DateTime.Now);
                       oReports.Actualizar_informeTMP(TxtCodReport.Text, txtNomReport.Text, estadotmp);
                       sNomInf = txtNomReport.Text;
                       this.Session["sNomInf"] = sNomInf;
                       SavelimpiarControlesInforme();
                       //llenaCmbTipoInfo();
                       // LlenaCmbModulo();
                       LlenaComboInforme();
                       this.Session["mensajealert"] = "El Informe : " + this.Session["sNomInf"];
                       Alertas.CssClass = "MensajesCorrecto";
                       LblFaltantes.Text = "El Canal : " + this.Session["sNomCanal"] + " Se Actualizo Corecctamente";
                       MensajeAlerta();
                       saveActivarbotonesInforme();
                       desactivarControlesInforme();

                   }
                   else
                   {
                       string sInforme = "";
                       sInforme = txtNomReport.Text;
                       this.Session["sInforme"] = sInforme;
                       Alertas.CssClass = "MensajesError";
                       LblFaltantes.Text = "El informe  " + this.Session["sInforme"] + " Ya Existe";
                       MensajeAlerta();
                   
                   }
               }

               else
               {

                   if (txtOrderReport.Text == "")
                   {
                       txtOrderReport.Text = "0";
                   }

                   EReports oeainformes = oReports.Actualizar_Reporte(Convert.ToInt32(TxtCodReport.Text.ToUpper()), Convert.ToInt32(txtOrderReport.Text), txtNomReport.Text, TxtDescReport.Text.ToUpper(), estado,
                   Convert.ToString(this.Session["sUser"]), DateTime.Now);
                   sNomInf = txtNomReport.Text;
                   this.Session["sNomInf"] = sNomInf;
                   SavelimpiarControlesInforme();
                   //llenaCmbTipoInfo();
                   // LlenaCmbModulo();
                   LlenaComboInforme();
                   this.Session["mensajealert"] = "El Informe : " + this.Session["sNomInf"];
                   Alertas.CssClass = "MensajesCorrecto";
                   LblFaltantes.Text = "El Canal : " + this.Session["sNomCanal"] + " Se Actualizo Corecctamente";
                   MensajeAlerta();
                   saveActivarbotonesInforme();
                   desactivarControlesInforme();

               }
               
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
        protected void btnCancelarReporte_Click(object sender, EventArgs e)
        {
            SavelimpiarControlesInforme();
            saveActivarbotonesInforme();
            desactivarControlesInforme();

        }
        private void MostrarDatosInforme()
        {
            recorrido = (DataTable)this.Session["tinformes"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodReport.Text = recorrido.Rows[recsearch]["Report_Id"].ToString().Trim();
                    txtNomReport.Text = recorrido.Rows[recsearch]["Report_NameReport"].ToString().Trim();
                    TxtDescReport.Text = recorrido.Rows[recsearch]["Report_Description"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Report_Status"].ToString().Trim());

                    if (estado == true)
                    {
                        RBtnListStatusReport.Items[0].Selected = true;
                        RBtnListStatusReport.Items[1].Selected = false;
                    }
                    else
                    {
                        RBtnListStatusReport.Items[0].Selected = false;
                        RBtnListStatusReport.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void btnPreg9_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tinformes"];
            MostrarDatosInforme();
        }
        protected void btnAreg9_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosInforme();
            }
        }
        protected void btnSreg9_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tinformes"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosInforme();
            }
        }
        protected void btnUreg9_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tinformes"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosInforme();
        }
        #endregion

        #region Informes vs Servicios
        protected void BtnCrearARpvsEs_Click(object sender, EventArgs e)
        {
            LlenaComboPaisInfoServ();
            LlenaComboReporserviinfo();
            crearActivarbotonesInfoServ();
            activarControlesInfoServ();

        }
        protected void BtnSaveARpvsEs_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            if (CmbSelPaisSer.Text == "0" || cmbSelServicio.Text == "0" || cmbSelServicio.Text == "" || cmbSelReporte.Text == "0")
            {
                if (CmbSelPaisSer.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "País";
                }

                if (cmbSelServicio.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Servicio";
                }
                if (cmbSelReporte.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Informe";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {
                DAplicacion odconsuARpvsSer = new DAplicacion();
                DataTable dtconsulta = odconsuARpvsSer.ConsultaDuplicados(ConfigurationManager.AppSettings["ReportesVsServ"], cmbSelReporte.Text, cmbSelServicio.Text, null);
                if (dtconsulta == null)
                {


                    EReportsStrategit oeRepVsServ = oReports.RegistrarRepVsSer(Convert.ToInt32(cmbSelServicio.SelectedValue), Convert.ToInt32(cmbSelReporte.SelectedValue), true,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    SavelimpiarControlesInfoServ();
                    //llenarcombos();


                    this.Session["mensajealert"] = "La Asociación del servicio con el informe ";
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Informe " + this.Session["sInforme"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesInfoServ();
                    desactivarControlesInfoServ();
                }
                else
                {
                    this.Session["mensajealert"] = "Asignacion de informe a servicio no se puede cambiar";
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Asignacion de informe a servicio no se puede cambiar" + " Ya Existe";
                    MensajeAlerta();
                }
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
        protected void CmbSelPaisSer_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaComboServiInfoServ();
        }
        protected void btnBuscarRepxser_Click(object sender, EventArgs e)
        {
            desactivarControlesInfoServ();
            LblFaltantes.Text = "";
            if ((cmbSelSer.Text == "0" || cmbSelSer.Text == "") || cmbSnomRep.Text == "0")
            {
                this.Session["mensajealert"] = "servicio e informe";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = " Porfavor Ingrese todos los parametros de consulta";
                MensajeAlerta();
                IbtnSerVsrepor_ModalPopupExtender.Show();
                return;
            }

            BuscarActivarbotnesInfoServ();
            try
            {
                icodStrategy = Convert.ToInt32(cmbSelSer.SelectedValue);
            }
            catch
            {
                icodStrategy = 0;
            }
            ReportId = Convert.ToInt32(cmbSnomRep.SelectedValue);
            cmbSelSer.Text = "0";
            cmbSnomRep.Text = "0";

            DataTable oeRepVsServ = oReports.BuscarRepVsServ(icodStrategy, ReportId);
            if (oeRepVsServ != null)
            {
                if (oeRepVsServ.Rows.Count > 0)
                {
                    for (int i = 0; i <= oeRepVsServ.Rows.Count - 1; i++)
                    {

                        TxtAsociaRpvsEs.Text = oeRepVsServ.Rows[0]["ReportSt_id"].ToString().Trim();
                      
                        CmbSelPaisSer.Text = oeRepVsServ.Rows[0]["cod_Country"].ToString().Trim();
                        LlenaComboServiInfoServ();
                        cmbSelServicio.Text = oeRepVsServ.Rows[0]["cod_Strategy"].ToString().Trim();
                        LlenaComboReporserviinfo();
                        cmbSelReporte.Text = oeRepVsServ.Rows[0]["Report_id"].ToString().Trim();
                        estado = Convert.ToBoolean(oeRepVsServ.Rows[0]["ReportSt_Status"].ToString().Trim());

                        if (estado == true)
                        {
                            RBtnListStatusAsociarRpvsEs.Items[0].Selected = true;
                            RBtnListStatusAsociarRpvsEs.Items[1].Selected = false;
                        }
                        else
                        {
                            RBtnListStatusAsociarRpvsEs.Items[0].Selected = false;
                            RBtnListStatusAsociarRpvsEs.Items[1].Selected = true;
                        }
                        this.Session["trepvsserv"] = oeRepVsServ;
                        this.Session["i"] = 0;
                    }
                    if (oeRepVsServ.Rows.Count == 1)
                    {
                        btnPreg10.Visible = false;
                        btnUreg10.Visible = false;
                        btnAreg10.Visible = false;
                        btnSreg10.Visible = false;
                    }
                    else
                    {
                        btnPreg10.Visible = true;
                        btnUreg10.Visible = true;
                        btnAreg10.Visible = true;
                        btnSreg10.Visible = true;
                    }

                }
                else
                {
                    SavelimpiarControlesInfoServ();
                    saveActivarbotonesInfoServ();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnSerVsrepor_ModalPopupExtender.Show();
                }
            }

        }
        protected void CmbBSelPaisSvsInf_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBuscarserviciosInfvsServ();
            IbtnSerVsrepor_ModalPopupExtender.Show();
        }

        protected void cmbSelSer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBuscarReporteServ();
            LlenaComboReporserviinfo();
            IbtnSerVsrepor_ModalPopupExtender.Show();
        }

        protected void btnEditRepVSSer_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesInfoServ();
            EditarActivarControlesInfoServ();
        }
        protected void BtnActualizaARpvsEs_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";

            try
            {
                if (RBtnListStatusAsociarRpvsEs.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabreportservice = new DAplicacion();
                    DataTable dt = oddeshabreportservice.PermitirDeshabilitar(ConfigurationManager.AppSettings["Reports_StrategitReports_Planning"], TxtAsociaRpvsEs.Text);
                    DataTable dt1 = oddeshabreportservice.PermitirDeshabilitarRepVSServ(ConfigurationManager.AppSettings["Reports_StrategitIndicadores"], cmbSelReporte.Text, cmbSelServicio.Text);
                    if (dt != null || dt1 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }
                }
                EReportsStrategit oeaInfVsServ = oReports.Actualizar_InformeVsServ(Convert.ToInt32(TxtAsociaRpvsEs.Text), Convert.ToInt32(cmbSelServicio.Text), estado,
                    Convert.ToString(this.Session["sUser"]), DateTime.Now);


                sNomInfvsServ = cmbSelServicio.SelectedValue + "/" + cmbSelReporte.SelectedValue;
                this.Session["sNomInfvsServ"] = sNomInfvsServ;
                SavelimpiarControlesInfoServ();
                //llenarcombos();
                this.Session["mensajealert"] = "La asociación de informe con servicio";
                Alertas.CssClass = "MensajesCorrecto";
                LblFaltantes.Text = "La asociación de informe con servicio" + " Se Actualizo Corecctamente";
                MensajeAlerta();
                saveActivarbotonesInfoServ();
                desactivarControlesInfoServ();
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
        protected void BtnCancelARpvsEs_Click(object sender, EventArgs e)
        {
            saveActivarbotonesInfoServ();
            desactivarControlesInfoServ();
            SavelimpiarControlesInfoServ();
        }
        private void MostrarDatosRepVsServ()
        {
            recorrido = (DataTable)this.Session["trepvsserv"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtAsociaRpvsEs.Text = recorrido.Rows[recsearch]["ReportSt_id"].ToString().Trim();
                    CmbSelPaisSer.Text = recorrido.Rows[recsearch]["cod_Country"].ToString().Trim();
                    LlenaComboServiInfoServ();
                    cmbSelServicio.Text = recorrido.Rows[recsearch]["cod_Strategy"].ToString().Trim();
                    cmbSelReporte.Text = recorrido.Rows[recsearch]["Report_id"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["ReportSt_Status"].ToString().Trim());

                    if (estado == true)
                    {
                        RBtnListStatusAsociarRpvsEs.Items[0].Selected = true;
                        RBtnListStatusAsociarRpvsEs.Items[1].Selected = false;
                    }
                    else
                    {
                        RBtnListStatusAsociarRpvsEs.Items[0].Selected = false;
                        RBtnListStatusAsociarRpvsEs.Items[1].Selected = true;
                    }
                }
            }

        }
        protected void btnPreg10_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["trepvsserv"];
            MostrarDatosRepVsServ();

        }
        protected void btnAreg10_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosRepVsServ();
            }
        }
        protected void btnSreg10_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["trepvsserv"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosRepVsServ();
            }
        }
        protected void btnUreg10_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["trepvsserv"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosRepVsServ();

        }

        #endregion

        #region Asignación Reportes a Canal
        protected void CmbCienteRC_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbCanalRC.Text = "0";
            llenacombocCanalRC();
            cmbReportesRC.Text = "0";
        }
        protected void BtnCrearRC_Click(object sender, EventArgs e)
        {
            llenacomboclienteRC();
            llenacomboReportesRC();
            crearActivarbotonesReportChannel();
            activarControlesReportChannel();
            cmbBuscarReportRC.Text = "0";
        }
        protected void cmbBuscarClienteRC_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenacomboBuscarCanalRC();
            cmbBuscarReportRC.Text = "0";
            IbtnReporteChannel_ModalPopupExtender.Show();
        }
        protected void cmbBuscarCanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenacomboBuscarReportesRC();
            IbtnReporteChannel_ModalPopupExtender.Show();
        }

        protected void BtnGuardarRC_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            if (CmbCienteRC.Text == "0" || cmbCanalRC.Text == "0" || cmbCanalRC.Text == "" || cmbReportesRC.Text == "0")
            {
                if (CmbCienteRC.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "País";
                }

                if (cmbCanalRC.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Servicio";
                }
                if (cmbReportesRC.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Informe";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {
                string tiporeporte = "";
                if (txtAlias.Text == "")
                {
                    txtAlias.Text = cmbReportesRC.Text;
                }
                if (cmb_TipoReporte.SelectedValue == "0")
                {

                }
                else
                {
                    tiporeporte = cmb_TipoReporte.SelectedValue;
                }

                DAplicacion odDuplicaReportChannel = new DAplicacion();
                DataTable dtconsulta = odDuplicaReportChannel.ConsultaDuplicados(ConfigurationManager.AppSettings["ReportChannel"], CmbCienteRC.Text, cmbCanalRC.Text, cmbReportesRC.Text);
                if (dtconsulta == null)
                {


                    EReportChannel oeReportesCanal = oReportesCanal.RegistrarReportesCanal(Convert.ToInt32(cmbReportesRC.SelectedValue), Convert.ToInt32(CmbCienteRC.SelectedValue), cmbCanalRC.SelectedValue, true,
                     Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now, txtAlias.Text, tiporeporte);

                    DataTable dt = oReportesCanal.RegistrarRepVsCanalTMP(oeReportesCanal.Report_id.ToString(), txtAlias.Text, oeReportesCanal.Company_id.ToString(), oeReportesCanal.cod_Channel, '1', tiporeporte);

                    SavelimpiarControlesReportChannel();
                    //llenarcombos();
                    this.Session["mensajealert"] = "La Asociación del Reporte con Canal ";
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Asociación " + this.Session["sInforme"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesReportChannel();
                    desactivarControlesReportChannel();
                }
                else
                {
                    this.Session["mensajealert"] = "Asignacion de reporte a Canal no se puede cambiar";
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Asignacion de Reporte a Canal no se puede cambiar" + " Ya Existe";
                    MensajeAlerta();
                }
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
        protected void cmbReportesRC_SelectedIndexChanged(object sender, EventArgs e)
        {
            Conexion oCoon= new Conexion();
            DataTable tipo_Reporte = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENATIPOSDEREPORTES", cmbReportesRC.SelectedValue, CmbCienteRC.SelectedValue);
            if (tipo_Reporte.Rows.Count > 0)
            {
                cmb_TipoReporte.DataSource = tipo_Reporte;
                cmb_TipoReporte.DataValueField = "id_Tipo_Reporte";
                cmb_TipoReporte.DataTextField = "TipoReporte_Descripcion";
                cmb_TipoReporte.DataBind();
              

                cmb_TipoReporte.Enabled = true;
            }

            cmb_TipoReporte.Items.Insert(0, new ListItem("Seleccione...", "0"));


        }
        protected void BtnBuscarRC_Click(object sender, EventArgs e)
        {
            desactivarControlesReportChannel();
            LblFaltantes.Text = "";
            if ((cmbBuscarClienteRC.Text == "0" || cmbBuscarCanal.Text == "") || cmbBuscarReportRC.Text == "0")
            {
                this.Session["mensajealert"] = "servicio e informe";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = " Porfavor Ingrese todos los parametros de consulta";
                MensajeAlerta();
                IbtnReporteChannel_ModalPopupExtender.Show();
                return;
            }

            BuscarActivarbotnesReportChannel();
            CompanyId = Convert.ToInt32(cmbBuscarClienteRC.SelectedValue);
            try
            {
                scodChannel = cmbBuscarCanal.SelectedValue;
            }
            catch
            {
                scodChannel = "0";
            }

            try
            {
                ReportIdC = Convert.ToInt32(cmbBuscarReportRC.SelectedValue);
            }
            catch
            {
                ReportIdC = 0;
            }
            cmbBuscarClienteRC.Text = "0";
            cmbBuscarCanal.Text = "0";
            cmbBuscarReportRC.Text = "0";

            DataTable oeReportesCanales = oReportesCanal.ConsultarReporteCanal(CompanyId, scodChannel, ReportIdC);

            if (oeReportesCanales != null)
            {
                if (oeReportesCanales.Rows.Count > 0)
                {
                    for (int i = 0; i <= oeReportesCanales.Rows.Count - 1; i++)
                    {
                        TxtCodAC.Text = oeReportesCanales.Rows[0]["ReportCanal_id"].ToString().Trim();
                        llenacomboclienteRC();
                        CmbCienteRC.Text = oeReportesCanales.Rows[0]["Company_id"].ToString().Trim();
                        llenacombocCanalRC();
                        cmbCanalRC.Text = oeReportesCanales.Rows[0]["cod_Channel"].ToString().Trim();
                        llenacomboReportesRC();
                        cmbReportesRC.Text = oeReportesCanales.Rows[0]["Report_id"].ToString().Trim();
                        estado = Convert.ToBoolean(oeReportesCanales.Rows[0]["ReportCanal_Status"].ToString().Trim());

                        if (estado == true)
                        {
                            RBTEsatadoRC.Items[0].Selected = true;
                            RBTEsatadoRC.Items[1].Selected = false;
                        }
                        else
                        {
                            RBTEsatadoRC.Items[0].Selected = false;
                            RBTEsatadoRC.Items[1].Selected = true;
                        }
                        this.Session["tRepCanal"] = oeReportesCanales;
                        this.Session["i"] = 0;
                    }
                    if (oeReportesCanales.Rows.Count == 1)
                    {
                        btnPreg10.Visible = false;
                        btnUreg10.Visible = false;
                        btnAreg10.Visible = false;
                        btnSreg10.Visible = false;
                    }
                    else
                    {
                        btnPreg10.Visible = true;
                        btnUreg10.Visible = true;
                        btnAreg10.Visible = true;
                        btnSreg10.Visible = true;
                    }

                }
                else
                {
                    SavelimpiarControlesInfoServ();
                    saveActivarbotonesInfoServ();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnReporteChannel_ModalPopupExtender.Show();
                }
            }
        }
        protected void BtnEditarRC_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesReportChannel();
            EditarActivarControlesReportChannel();

        }
        protected void BtnActualizarRC_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";

            try
            {
                if (RBTEsatadoRC.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshaReporChanel = new DAplicacion();
                    DataTable dt = oddeshaReporChanel.PermitirDeshabilitar(ConfigurationManager.AppSettings["Reports_Channels"], TxtCodAC.Text);
                    if (dt != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }
                }
                EReportChannel oeaReVSCanal = oReportesCanal.Actualizar_ReportesCanal(Convert.ToInt32(TxtCodAC.Text), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);


                sNomRepCanal = cmbCanalRC.SelectedValue + "/" + cmbReportesRC.SelectedValue;
                this.Session["sNomRepCanal"] = sNomRepCanal;
                SavelimpiarControlesReportChannel();
                //llenarcombos();
                this.Session["mensajealert"] = "La asociación de informe con servicio";
                Alertas.CssClass = "MensajesCorrecto";
                LblFaltantes.Text = "La asociación de Reporte con Canal" + " Se Actualizo Corecctamente";
                MensajeAlerta();
                saveActivarbotonesReportChannel();
                desactivarControlesReportChannel();
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
        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            saveActivarbotonesReportChannel();
            desactivarControlesReportChannel();
            SavelimpiarControlesReportChannel();

        }
        #endregion
     
        #region Asignación Informe a Usuario
        protected void BtnCrearUsuInfo_Click(object sender, EventArgs e)
        {
            comboclienteenInfoUser();
            crearActivarbotonesUsuarioInfo();
            activarControlesInfoUsuarioInfo();
        }
        protected void CmbClienteUI_SelectedIndexChanged(object sender, EventArgs e)
        {
            combousuarios();
            cmbClienteAcceder.Items.Clear();
            cmbServicioUsu.Items.Clear();
            Checkinforme.Items.Clear();

        }
        protected void cmbClienteAcceder_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboCanalXCliente();
            //cmbServicioUsu.Items.Clear();
            Checkinforme.Items.Clear();
        }
        protected void cmbUsuarioInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenacomboServicioUsu();
            comboclienteenInfoUserAcceso();
            cmbCanalUsu.Text = "0";
            Checkinforme.Items.Clear();
           
        }
        protected void cmbCanalUsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbServicioUsu.Text = "0";
            comboSubcanalXcanal();
            Checkinforme.Items.Clear();
        }
        protected void cmbSubCanalUsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbServicioUsu.Text = "0";
            comboSubNivelXSubcanal();
            Checkinforme.Items.Clear();
        }
        protected void cmbServicioUsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            LLenainformeporServicio();
            Informe.Visible = true;
            Checkinforme.Visible = true;      

        }
        protected void BtnGuardarUsuInfo_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            if (cmbUsuarioInfo.Text == "0" || Checkinforme.SelectedIndex == -1 || cmbServicioUsu.Text == "0" || cmbCanalUsu.Text == "0" || CmbClienteUI.Text == "0" || CheckCiudades.Text == "0" || cmbClienteAcceder.Text=="0")
            {
                if (cmbUsuarioInfo.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Usuario";
                }

                if (Checkinforme.SelectedIndex == -1)
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Informe";
                }
                if (cmbServicioUsu.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Servicio";
                }
                if (cmbCanalUsu.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                if (CmbClienteUI.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }
                if (CheckCiudades.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cobertura";
                }
                if (cmbClienteAcceder.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Clientes a Acceder";
                }
                
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            //if (cmbClienteAcceder.Text == "1572" ) 
            //{
            //    if (cmbSubCanalUsu.Text == "0" || cmbSubNivel.Text == "0" || cmbSubNivel.Text == "")
            //    {
            //        if (cmbSubCanalUsu.Text == "0")
            //        {
            //            LblFaltantes.Text = LblFaltantes.Text + ". " + "SubCanal";
            //        }
            //        if (cmbSubNivel.Text == "0")
            //        {
            //            LblFaltantes.Text = LblFaltantes.Text + ". " + "SubNivel";
            //        }
            //        Alertas.CssClass = "MensajesError";
            //        LblFaltantes.Text = "Debe ingresar SubCanal y SubNivel para acceder a informes de San Fernando";
            //        MensajeAlerta();
            //        return;
            //    }
              
            //}

            try
            {
                for (int i = 0; i <= Checkinforme.Items.Count - 1; i++)
                {
                    if (Checkinforme.Items[i].Selected == true)
                    {///verifica  que no haya duplicados usuario, canal, servicio e informes
                        DAplicacion oedInfoaUsu = new DAplicacion();
                        DataTable dtconsulta = oedInfoaUsu.DuplicadosInfoaUsuarios(ConfigurationManager.AppSettings["AsignaciónInfoaUsuario"], cmbUsuarioInfo.SelectedValue, Checkinforme.Items[i].Value, cmbServicioUsu.SelectedValue, cmbCanalUsu.SelectedValue, cmbClienteAcceder.SelectedValue, cmbSubCanalUsu.SelectedValue, cmbSubNivel.SelectedValue);
                        if (dtconsulta != null)
                        {
                            this.Session["mensajealert"] = "La Asociación del servicio con el informe ";
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El informe " + Checkinforme.Items[i].Text + " para el usuario " + cmbUsuarioInfo.SelectedItem.Text +
                                " en el Servicio " + cmbServicioUsu.SelectedItem.Text + " y Canal " + cmbCanalUsu.SelectedItem.Text + " Ya Existe " + "quitelo e intentelo de nuevo ";
                            MensajeAlerta();
                            i = Checkinforme.Items.Count - 1;
                            continuar = false;
                        }

                    }
                }
                if (continuar)
                {
                    for (int i = 0; i <= Checkinforme.Items.Count - 1; i++)
                    {
                        if (Checkinforme.Items[i].Selected == true)
                        {//inserta registro en Clie_User_Report
                            EInfoaUsaurio oeinfoUsuario = oinfousuario.InsertarInfoaUsuario(Convert.ToInt32(cmbUsuarioInfo.SelectedValue.ToString().Trim()), Convert.ToInt32(Checkinforme.Items[i].Value), Convert.ToInt32(cmbServicioUsu.SelectedValue.ToString().Trim()), cmbCanalUsu.SelectedValue.ToString().Trim(), cmbSubCanalUsu.Text.ToString().Trim(),  cmbSubNivel.Text.ToString().Trim(), Convert.ToInt32(cmbClienteAcceder.SelectedValue.ToString().Trim()), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                                     

                        }
                    }

                    SavelimpiarControlesUsuarioInfo();
                    comboclienteBuscarenInfoUser();
                    this.Session["mensajealert"] = "La Asociación del servicio con el informe ";
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "Asignación realizada con Exito";
                    MensajeAlerta();
                    saveActivarbotonesUsuarioInfo();
                    desactivarControlesUsuarioInfo();
                }
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
        protected void BtnBuscarAInformesUsers_Click(object sender, EventArgs e)
        {
            desactivarControlesUsuarioInfo();
            LblFaltantes.Text = "";
            if (cmbClienteBUI.Text == "0" || cmbUsuarioBAIU.Text == "0" || cmbBCanalUI.Text == "0" || cmbServicioBAIU.Text == "0")
            {
                if (cmbClienteBUI.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }

                if (cmbUsuarioBAIU.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Usuario";
                }
                if (cmbBCanalUI.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                if (cmbServicioBAIU.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Servicio";
                }

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese todos los parametros de consulta";
                MensajeAlerta();
                IbtnModalPopupAsignacionReportUser.Show();
                return;
            }

            BuscarActivarbotnesUsuarioInfo();
            sbcliente = cmbClienteBUI.Text;
            sUsuario = cmbUsuarioBAIU.Text;
            sbCanal = cmbBCanalUI.Text;
            sBServicio = cmbServicioBAIU.Text;
            //cmbClienteBUI.Text = "0";
            cmbUsuarioBAIU.Text = "0";
            cmbBCanalUI.Text = "0";
            cmbServicioBAIU.Text = "0";
            ///consulta resgistro de tabla Clie_User_Reports
            DataTable oeinfoUser = oinfousuario.ConsultarInfoaUsuario(Convert.ToInt32(sbcliente), Convert.ToInt32(sUsuario), sbCanal, Convert.ToInt32(sBServicio));

            if (oeinfoUser != null)
            {
                if (oeinfoUser.Rows.Count > 0)
                {/// llena objetos con la consulta
                    DataSet ds = (DataSet)this.Session["dsinfousuarios"];
                    TextCodUI.Text = oeinfoUser.Rows[0]["id_userinforme"].ToString().Trim();
                    comboclienteenInfoUser();
                    CmbClienteUI.Text = oeinfoUser.Rows[0]["Company_Person"].ToString().Trim();
                    //CmbClienteUI.Text = cmbClienteBUI.Text; 
                    combousuarios();
                    cmbUsuarioInfo.Text = oeinfoUser.Rows[0]["Person_id"].ToString().Trim();
                    comboclienteenInfoUserAcceso();
                    cmbClienteAcceder.Text = oeinfoUser.Rows[0]["Company_id"].ToString().Trim();
                    ComboCanalXCliente();
                    cmbCanalUsu.Text = oeinfoUser.Rows[0]["cod_Channel"].ToString().Trim();
                    comboSubcanalXcanal();
                    cmbSubCanalUsu.Text = oeinfoUser.Rows[0]["cod_subchannel"].ToString().Trim();
                    comboSubNivelXSubcanal();
                    cmbSubNivel.Text = oeinfoUser.Rows[0]["cod_Subnivel"].ToString().Trim();
                    llenacomboServicioUsu();
                    cmbServicioUsu.Text = oeinfoUser.Rows[0]["cod_Strategy"].ToString().Trim();
                    LLenainformeporServicio();
                    Informe.Visible = true;
                    Checkinforme.Visible = true;
                    llenaconsulCheckinforme();
                    BuscarActivarbotnesUsuarioInfo();
                    DefinicionEstado();
                    estado = Convert.ToBoolean(oeinfoUser.Rows[0]["userinfo_status"].ToString().Trim());

                    if (estado == true)
                    {
                        RadioInfoUsu.Items[0].Selected = true;
                        RadioInfoUsu.Items[1].Selected = false;
                    }
                    else
                    {
                        RadioInfoUsu.Items[0].Selected = false;
                        RadioInfoUsu.Items[1].Selected = true;
                    }
                    this.Session["tInfoUser"] = oeinfoUser;
                    this.Session["i"] = 0;

                    if (oeinfoUser.Rows.Count == 1)
                    {
                        PregUsuInfo.Visible = false;
                        AregUsuInfo.Visible = false;
                        SregUsuInfo.Visible = false;
                        UregUsuInfo.Visible = false;
                    }
                    else
                    {
                        PregUsuInfo.Visible = true;
                        AregUsuInfo.Visible = true;
                        SregUsuInfo.Visible = true;
                        UregUsuInfo.Visible = true;
                    }
                }
                else
                {
                    SavelimpiarControlesUsuarioInfo();
                    saveActivarbotonesUsuarioInfo();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnModalPopupAsignacionReportUser.Show();
                }
                
            }
        }
        protected void BtnCancelUsuInfo_Click(object sender, EventArgs e)
        {
            saveActivarbotonesUsuarioInfo();
            desactivarControlesUsuarioInfo();
            SavelimpiarControlesUsuarioInfo();
        }
        protected void cmbClienteBUI_SelectedIndexChanged(object sender, EventArgs e)
        {///llena combo de buscar usuarios al selecconar el cliente
            comboBuscarusuarios();
            cmbBCanalUI.Items.Clear();
            cmbServicioBAIU.Items.Clear();
            IbtnModalPopupAsignacionReportUser.Show();

        }
        protected void cmbUsuarioBAIU_SelectedIndexChanged(object sender, EventArgs e)
        {///llena combo de buscar canal al seleccionar usuarios
            ComboCanalBuscarXCliente();
            cmbServicioBAIU.Items.Clear();
            IbtnModalPopupAsignacionReportUser.Show();
        }
        protected void cmbBCanalUI_SelectedIndexChanged(object sender, EventArgs e)
        {///llena  combo de buscar servcio al seleccionar el canal
            llenacomboBuscarServicioUsu();
            IbtnModalPopupAsignacionReportUser.Show();
        }
        //todo lo que se encuentra comentariado es por los cambios que se hizo en este maestro en la consulta que anteriormente era en una grilla

        //protected void CmbBuscarAsignación_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    llenaGrillaConsulta();
        //    ModalBuscar.Show();
        //}
        //protected void CmbBuscarServicio_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    llenaGrillaConsulta();
        //    ModalBuscar.Show();
        //}
        //protected void InfoaUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    InfoaUsuarios.PageIndex = e.NewPageIndex;
        //    llenaGrillaConsulta();
        //    ModalBuscar.Show();
        //}
        //protected void InfoaUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataTable ds = (DataTable)this.Session["dsinfousuarios"];
        //    cmbUsuarioInfo.Text = ds.Rows[InfoaUsuarios.SelectedRow.DataItemIndex]["Person_id"].ToString().Trim();
        //    cmbCanalUsu.Text = ds.Rows[InfoaUsuarios.SelectedRow.DataItemIndex]["cod_Channel"].ToString().Trim();
        //    llenacomboServicioUsu();
        //    cmbServicioUsu.Text = ds.Rows[InfoaUsuarios.SelectedRow.DataItemIndex]["cod_Strategy"].ToString().Trim();
        //    LLenainformeporServicio();
        //    Informe.Visible = true;
        //    Checkinforme.Visible = true;
        //    llenaconsulCheckinforme();
        //    BuscarActivarbotnesUsuarioInfo();
        //    DefinicionEstado();
        //    InfoaUsuarios.SelectedIndex = -1;
        //    llenacomboUsuarioConsulta();
        //    llenacomboServicioConsulta();
        //    llenaGrillaConsulta();

        //}
        protected void BtnEditarUsuInfo_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesUsuarioInfo();
            EditarActivarControlesUsuarioInfo();

        }       
        protected void BtnActuUsuInfo_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DataSet ds = (DataSet)this.Session["dsinformesxusuario"];
                    for (int j = 0; j <= Checkinforme.Items.Count - 1; j++)
                    {
                        bContinuar = true;
                        for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                        {
                            if (Checkinforme.Items[j].Value == ds.Tables[0].Rows[i][0].ToString().Trim())
                            {
                                EInfoaUsaurio oeactInfoaUsu = odactInfoaUsu.Actualizar_AsignaciónInfoaUsu
                                 (Convert.ToInt32(cmbUsuarioInfo.Text), Convert.ToInt32(Checkinforme.Items[j].Value), Convert.ToInt32(cmbServicioUsu.Text), cmbCanalUsu.Text, cmbSubCanalUsu.Text, cmbSubNivel.Text, Checkinforme.Items[j].Selected,
                                Convert.ToString(this.Session["sUser"]), DateTime.Now);

                                i = ds.Tables[0].Rows.Count - 1;
                                bContinuar = false;
                            }
                        }
                        if (bContinuar)
                        {
                            if (Checkinforme.Items[j].Selected == true)
                            {
                                EInfoaUsaurio oeinfoUsuario = oinfousuario.InsertarInfoaUsuario(Convert.ToInt32(cmbUsuarioInfo.SelectedValue.ToString().Trim()), Convert.ToInt32(Checkinforme.Items[j].Value), Convert.ToInt32(cmbServicioUsu.SelectedValue.ToString().Trim()), cmbCanalUsu.SelectedValue.ToString().Trim(), cmbSubCanalUsu.SelectedValue.ToString().Trim(), cmbSubNivel.Text.ToString().Trim(), Convert.ToInt32(cmbClienteAcceder.SelectedValue.ToString().Trim()), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            }
                        }
                    }
                    SavelimpiarControlesUsuarioInfo();
                    //llenacomboUsuarioConsulta();
                    this.Session["mensajealert"] = "La asociación de Informes a Usuario";
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La asociación de Informes con Usuario" + " Se Actualizo Corecctamente";
                    MensajeAlerta();
                    saveActivarbotonesUsuarioInfo();
                    desactivarControlesUsuarioInfo();
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

               
            }
        private void MostrarDatosUsuInfo()
        {
            recorrido = (DataTable)this.Session["tInfoUser"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                   

                    DataSet ds = (DataSet)this.Session["dsinfousuarios"];
                    TextCodUI.Text = recorrido.Rows[recsearch]["id_userinforme"].ToString().Trim();
                    comboclienteenInfoUser();
                    CmbClienteUI.Text = recorrido.Rows[recsearch]["Company_Person"].ToString().Trim();
                    //CmbClienteUI.Text = cmbClienteBUI.Text; 
                    combousuarios();
                    cmbUsuarioInfo.Text = recorrido.Rows[recsearch]["Person_id"].ToString().Trim();
                    comboclienteenInfoUserAcceso();
                    cmbClienteAcceder.Text = recorrido.Rows[recsearch]["Company_id"].ToString().Trim();
                    ComboCanalXCliente();
                    cmbCanalUsu.Text = recorrido.Rows[recsearch]["cod_Channel"].ToString().Trim();
                    comboSubcanalXcanal();
                    cmbSubCanalUsu.Text = recorrido.Rows[recsearch]["cod_subchannel"].ToString().Trim();
                    comboSubNivelXSubcanal();
                    cmbSubNivel.Text = recorrido.Rows[recsearch]["cod_Subnivel"].ToString().Trim();
                    llenacomboServicioUsu();
                    cmbServicioUsu.Text = recorrido.Rows[recsearch]["cod_Strategy"].ToString().Trim();
                    LLenainformeporServicio();
                    Informe.Visible = true;
                    Checkinforme.Visible = true;
                    llenaconsulCheckinforme();
                    BuscarActivarbotnesUsuarioInfo();
                    DefinicionEstado();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["userinfo_status"].ToString().Trim());

                    if (estado == true)
                    {
                        RadioInfoUsu.Items[0].Selected = true;
                        RadioInfoUsu.Items[1].Selected = false;
                    }
                    else
                    {
                        RadioInfoUsu.Items[0].Selected = false;
                        RadioInfoUsu.Items[1].Selected = true;
                    }
                }
            }
            
        }
        protected void PregUsuInfo_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tInfoUser"];
            MostrarDatosUsuInfo();
        }
        protected void AregUsuInfo_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosUsuInfo();
            }
        }
        protected void SregUsuInfo_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tInfoUser"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosUsuInfo();
            }
        }
        protected void UregUsuInfo_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tInfoUser"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosUsuInfo();
        }
        #endregion      
        
        #region maestro de asignación Oficina
        protected void btnCrearCity_Click(object sender, EventArgs e)
        {
            ComboClienteCity();
            crearActivarbotonesACity();
            activarControlesACity();
        }
        protected void cmbClientecity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboUsuarioCity();
            consultapaisXCliente();
            cmbCanalCity.Items.Clear();
            cmbReporteCity.Items.Clear();
            cmbServicioCity.Items.Clear();
            CheckCiudades.Items.Clear();
            
        }
        protected void cmbUsuarioCity_SelectedIndexChanged(object sender, EventArgs e)
        {
           ComboCanalxUsuCity();
           cmbReporteCity.Items.Clear();
           cmbServicioCity.Items.Clear();
           CheckCiudades.Items.Clear();
        }
        protected void cmbCanalCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboServicioxCanalCity();
            cmbReporteCity.Items.Clear();
            CheckCiudades.Items.Clear();
        }
        protected void cmbServicioCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboReportCity();
            CheckCiudades.Items.Clear();
        }
        protected void cmbReporteCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaOficinaXClienteyReport();
            if (dato)
            {
                Combotextid_userinforme();
            }
        }
        protected void BtnGuardarcity_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            if (cmbClientecity.Text == "0" || cmbUsuarioCity.Text == "0" || cmbCanalCity.Text == "0" || cmbServicioCity.Text == "0" || cmbReporteCity.Text == "0" || CheckCiudades.SelectedIndex == -1)
            {
                if (cmbClientecity.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }

                if (cmbUsuarioCity.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Usuario";
                }
                if (cmbCanalCity.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                if (cmbServicioCity.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Servicio";
                }
                if (cmbReporteCity.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Reporte";
                }
                if (CheckCiudades.SelectedIndex == -1)
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cobertura";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {
                for (int i = 0; i <= CheckCiudades.Items.Count - 1; i++)
                {
                    if (CheckCiudades.Items[i].Selected == true)
                    {///verifica  que no haya duplicados usuario, canal, servicio,informes Ciudades
                        DAplicacion oedUCity = new DAplicacion();
                        DataTable dtconsulta = oedUCity.ConsultaDuplicados(ConfigurationManager.AppSettings["City_UserRepor"], TxtcodigoClieUR.Text, CheckCiudades.Items[i].Value, null);
                        if (dtconsulta != null)
                        {
                            this.Session["mensajealert"] = "La Asociación de Cobertura";
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "las Ciudades " + CheckCiudades.Items[i].Text + " para el usuario " + cmbUsuarioCity.SelectedItem.Text +
                                " en el Servicio " + cmbServicioCity.SelectedItem.Text + " y Canal" + cmbCanalCity.SelectedItem.Text + " Ya Existe " + "quitelo e intentelo de nuevo ";
                            MensajeAlerta();
                            i = CheckCiudades.Items.Count - 1;
                            continuar = false;
                        }

                    }
                }
                if (continuar)
                {
                    for (int i = 0; i <= CheckCiudades.Items.Count - 1; i++)
                    {
                        if (CheckCiudades.Items[i].Selected == true)
                        {//inserta registro en City_User_Report
                            ECity_UserReport oeiCityUR = oerCityUserReport.RegistrarCityUserReport(Convert.ToInt32(TxtcodigoClieUR.Text), Convert.ToInt64( CheckCiudades.Items[i].Value), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now); 

                        }
                    }

                    SavelimpiarControlesACity();
                   // ComboClienteCity();
                    this.Session["mensajealert"] = "La Asociación de Cobertura ";
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "Asignación realizada con Exito";
                    MensajeAlerta();
                    saveActivarbotonesACity();
                    desactivarControlesACity();
                }
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
        protected void cmbBCliCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBuscarUsuarioCity();
            cmbBCanalCity.Items.Clear();
            cmbBservicioCity.Items.Clear();
            cmbBreport.Items.Clear();
            ModalPopupCity.Show();
        }
        protected void cmbBUSUCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBuscarCanalxUsuCity();
            cmbBservicioCity.Items.Clear();
            cmbBreport.Items.Clear();
            ModalPopupCity.Show();
        }
        protected void cmbBCanalCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBuscarServicioxCanalCity();
            cmbBreport.Items.Clear();
            ModalPopupCity.Show();
        }
        protected void cmbBservicioCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBReportBuscarCity();
            ModalPopupCity.Show();
        }
        protected void BtnEditarCity_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesACity();
            EditarActivarControlesACity();
        }
        protected void BtnBuscarCity_Click(object sender, EventArgs e)
        {
    
            desactivarControlesACity();
            LblFaltantes.Text = "";
            if (cmbBCliCity.Text == "0" || cmbBUSUCity.Text == "0" || cmbBCanalCity.Text == "0" || cmbBservicioCity.Text == "0" || cmbBreport.Text == "0")
            {
                if (cmbBCliCity.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }

                if (cmbBUSUCity.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Usuario";
                }
                if (cmbBCanalCity.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                if (cmbBservicioCity.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Servicio";
                }
                if (cmbBreport.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Reporte";
                }

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese todos los parametros de consulta";
                MensajeAlerta();
                ModalPopupCity.Show();
                return;
            }

           
            ComboClienteCity();
            cmbClientecity.Text = cmbBCliCity.Text;        
            ComboUsuarioCity();
            consultapaisXCliente();
            cmbUsuarioCity.Text=cmbBUSUCity.Text;
            ComboCanalxUsuCity();
            cmbCanalCity.Text=cmbBCanalCity.Text;
            ComboServicioxCanalCity();
            cmbServicioCity.Text = cmbBservicioCity.Text;
            ComboReportCity();
            cmbReporteCity.Text = cmbBreport.Text;
            llenaOficinaXClienteyReport();
            if (dato)
            {
                Combotextid_userinforme();
                llenaconsulCheckCiudades();
                DefinicionEstadoOficina();
                BuscarActivarbotnesACity();
            }
                cmbBCliCity.Text = "0";
                cmbBUSUCity.Items.Clear();
                cmbBCanalCity.Items.Clear();
                cmbBservicioCity.Items.Clear();
                cmbBreport.Items.Clear();
                
            

        }
        protected void BtnActualizarCity_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DataSet ds1 = (DataSet)this.Session["dsCiudadesxusuario"];
                    for (int j = 0; j <= CheckCiudades.Items.Count - 1; j++)
                    {
                        bContinuar = true;
                        for (int i = 0; i <= ds1.Tables[0].Rows.Count - 1; i++)
                        {
                            string dato;
                            dato = ds1.Tables[0].Rows[i][0].ToString().Trim();
                            if (CheckCiudades.Items[j].Value == ds1.Tables[0].Rows[i][0].ToString().Trim())
                            {
                                ECity_UserReport oeactCityUsR = oerCityUserReport.ActualizarCityUR(Convert.ToInt32(TxtcodigoClieUR.Text), Convert.ToInt64(CheckCiudades.Items[j].Value), CheckCiudades.Items[j].Selected,
                                Convert.ToString(this.Session["sUser"]), DateTime.Now);
                               

                                i = ds1.Tables[0].Rows.Count - 1;
                                bContinuar = false;
                            }
                        }
                        if (bContinuar)
                        {
                            if (CheckCiudades.Items[j].Selected == true)
                            {
                                ECity_UserReport oeiCityUR = oerCityUserReport.RegistrarCityUserReport(Convert.ToInt32(TxtcodigoClieUR.Text), Convert.ToInt64(CheckCiudades.Items[j].Value), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now); 
                            }
                        }
                    }
                    SavelimpiarControlesACity();
                    //llenacomboUsuarioConsulta();
                    this.Session["mensajealert"] = "La asociación de Informes a Usuario";
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La asociación de Informes con Usuario" + " Se Actualizo Corecctamente";
                    MensajeAlerta();
                    saveActivarbotonesACity();
                    desactivarControlesACity();
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


        }
        protected void BtnCancelarCity_Click(object sender, EventArgs e)
        {
            saveActivarbotonesACity();
            desactivarControlesACity();
            SavelimpiarControlesACity();
        }
        #endregion

        #region Asignación de Reporte a Oficinas
        protected void cmbClienteRO_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenacomboReportXClienteRO();
            ChekROficinas.Items.Clear();
        }
        protected void CmbReporteOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            //llenacheckOfcinasXClienteRO();
            llenaOficinaXClienteyReportRO();
        }
        protected void CrearRO_Click(object sender, EventArgs e)
        {
            llenacomboClienteRO();
            crearActivarbotonesReportOficina();
            activarControlesReportOficina();

        }
        protected void cmbBCliRO_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenacomboBuscarReportXClienteRO();
            Popup_ReportOficina.Show();
        }
        protected void GuardarRO_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            if (cmbClienteRO.Text == "0" || CmbReporteOficina.Text == "0" || ChekROficinas.SelectedIndex == -1)
            {
                if (cmbClienteRO.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }

                if (CmbReporteOficina.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Usuario";
                }

                if (ChekROficinas.SelectedIndex == -1)
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cobertura";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {
                for (int i = 0; i <= ChekROficinas.Items.Count - 1; i++)
                {
                    if (ChekROficinas.Items[i].Selected == true)
                    {///verifica  que no haya duplicados usuario, canal, servicio,informes Ciudades
                        DAplicacion oedRO = new DAplicacion();
                        DataTable dtconsulta = oedRO.ConsultaDuplicados(ConfigurationManager.AppSettings["AD_ReportOficina"], CmbReporteOficina.Text, ChekROficinas.Items[i].Value, null);
                        if (dtconsulta != null)
                        {
                            this.Session["mensajealert"] = "La Asociación de Oficinas a Reporte";
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "las Oficinas " + ChekROficinas.Items[i].Text + " para el Reporte " + CmbReporteOficina.SelectedItem.Text  + " Ya Existe " + "quitelo e intentelo de nuevo ";
                            MensajeAlerta();
                            i = ChekROficinas.Items.Count - 1;
                            continuar = false;
                        }

                    }
                }
                if (continuar)
                {
                    for (int i = 0; i <= ChekROficinas.Items.Count - 1; i++)
                    {
                        if (ChekROficinas.Items[i].Selected == true)
                        {
                            EAD_Report_Oficina oeiRO = oerROficina.RegistrarReportOficinas(Convert.ToInt32(CmbReporteOficina.Text), Convert.ToInt64( ChekROficinas.Items[i].Value), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                       
                        }
                    }

                    SavelimpiarControlesReportOficina();
                    // ComboClienteCity();
                    this.Session["mensajealert"] = "La Asociación de Oficinas a Reporte";
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "Asignación realizada con Exito";
                    MensajeAlerta();
                    saveActivarbotonesReportOficina();
                    desactivarControlesReportOficina();
                }
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
        protected void BucarRO_Click(object sender, EventArgs e)
        {
            desactivarControlesReportOficina();
            LblFaltantes.Text = "";
            if (cmbBCliRO.Text == "0" || cmbBReporRO.Text == "0")
            {
                if (cmbBCliRO.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }

                if (cmbBReporRO.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Usuario";
                }
             
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese todos los parametros de consulta";
                MensajeAlerta();
                Popup_ReportOficina.Show();
                return;
            }


            llenacomboClienteRO();
            cmbClienteRO.Text = cmbBCliRO.Text;
            llenacomboReportXClienteRO();
            CmbReporteOficina.Text = cmbBReporRO.Text;
            llenaOficinaXClienteyReportRO();
            //llenacheckOfcinasXClienteRO();
            //llenaOficinaXClienteyReportRO();
            if (dato)
            {       
            llenaconsulCheckReportOficinas();
            DefinicionEstadoReporteOficina();
            BuscarActivarbotnesReportOficina();
            }
            cmbBCliRO.Text = "0";
            cmbBReporRO.Items.Clear();

        }
        protected void EditarRO_Click(object sender, EventArgs e)
        {
             EditarActivarbotonesReportOficina();
             EditarActivarControlesReportOficina();

        }
        protected void ActualizarRO_Click(object sender, EventArgs e)
        {
            
                try
                {
                    DataSet ds1 = (DataSet)this.Session["dsOficinasXReporte"];
                    for (int j = 0; j <= ChekROficinas.Items.Count - 1; j++)
                    {
                        bContinuar = true;
                        for (int i = 0; i <= ds1.Tables[0].Rows.Count - 1; i++)
                        {
                            string dato;
                            dato = ds1.Tables[0].Rows[i][0].ToString().Trim();
                            if (ChekROficinas.Items[j].Value == ds1.Tables[0].Rows[i][0].ToString().Trim())
                            {
                                EAD_Report_Oficina oeaReOf = oerROficina.ActualizarReOficina(Convert.ToInt32(CmbReporteOficina.Text), Convert.ToInt64(ChekROficinas.Items[j].Value), ChekROficinas.Items[j].Selected,
                                 Convert.ToString(this.Session["sUser"]), DateTime.Now); 
                             
                                i = ds1.Tables[0].Rows.Count - 1;
                                bContinuar = false;
                            }
                        }
                        if (bContinuar)
                        {
                            if (ChekROficinas.Items[j].Selected == true)
                            {
                                EAD_Report_Oficina oeiRO = oerROficina.RegistrarReportOficinas(Convert.ToInt32(CmbReporteOficina.Text), Convert.ToInt64(ChekROficinas.Items[j].Value), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            }
                        }
                    }
                    SavelimpiarControlesReportOficina();
                    //llenacomboUsuarioConsulta();
                    this.Session["mensajealert"] = "La asociación de Informes a Usuario";
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La asociación de Oficinas a Reporte" + " Se Actualizo Corecctamente";
                    MensajeAlerta();
                    saveActivarbotonesReportOficina();
                    desactivarControlesReportOficina();
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
        protected void CancelarRO_Click(object sender, EventArgs e)
        {
            saveActivarbotonesReportOficina();
            desactivarControlesReportOficina();
            SavelimpiarControlesReportOficina();
        }
        #endregion

        #region Parametrización por Reporte
        protected void BtnCrearGR_Click(object sender, EventArgs e)
        {
            crearActivarbotonesGR();
            activarControlesGR();
            comboclienteenGR(cmbCompanyGR);
           
        }
        protected void cmbCompanyGR_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboCanalXClienteGR(cmbCompanyGR,CmbCanalGR);
        }
        protected void CmbCanalGR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenacomboReporteGR(cmbCompanyGR, CmbCanalGR, cmbReportGv);
        }
        protected void cmbReportGv_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenacomboTipoReporteGR(cmbCompanyGR, CmbtipoReporte);
        }
        protected void BtnGuardarGR_Click(object sender, EventArgs e)
        {
            

            LblFaltantes.Text = "";
            if (cmbCompanyGR.Text == "0" || CmbCanalGR.Text == "0" || cmbReportGv.Text == "0" )
            {
                if (cmbCompanyGR.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }

                if (CmbCanalGR.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                if (cmbReportGv.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Reporte";
                }
               
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            /** if (chkVCategory.Checked == false && chkVMarca.Checked == false && chkVSubMarca.Checked == false && chkVFamilia.Checked == false && chkVProducto.Checked==false)
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar por lo menos una vista";
                MensajeAlerta();
                return;
            }**/

            try
            {
                DAplicacion odconsuARpvsSer = new DAplicacion();
                DataTable dtconsulta = odconsuARpvsSer.DuplicadosInfoaUsuarios(ConfigurationManager.AppSettings["Parametros_Reports"], cmbCompanyGR.Text, CmbCanalGR.Text, cmbReportGv.Text, CmbtipoReporte.Text, null, null, null);
                if (dtconsulta == null)
                {


                    EAD_GestionProductosXReporte oeGestioReporte = oRG.RegistrarGR(Convert.ToInt32(cmbCompanyGR.SelectedValue), CmbCanalGR.SelectedValue, Convert.ToInt32(cmbReportGv.SelectedValue), Convert.ToBoolean(chkVCategory.Checked), Convert.ToBoolean(chkVMarca.Checked), Convert.ToBoolean(chkVSubMarca.Checked), Convert.ToBoolean(chkVFamilia.Checked),Convert.ToBoolean(chkVSubFamilia.Checked), Convert.ToBoolean(chkVProducto.Checked), CmbtipoReporte.SelectedValue, true,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    /* se cambia el metodo de insercion y se habilita el paso de parametros para ejecutaar el SP directamente en base intermedia
                     * Angel Ortiz - 03/08/2011
                     * **/
                    EAD_GestionProductosXReporte oegestionRTMP = oRG.RegistrarGRtmp(oeGestioReporte.id_RelInfo_prodcutos, oeGestioReporte.Company_id, oeGestioReporte.cod_Channel, oeGestioReporte.Report_id, oeGestioReporte.Vista_Categoria, 
                                                                  oeGestioReporte.Vista_Marca, oeGestioReporte.Vista_SubMarca,oeGestioReporte.Vista_Familia, oeGestioReporte.Vista_Producto,oeGestioReporte.id_Tipo_Reporte);                    
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "Parametrización por reporte " + cmbReportGv.SelectedItem.ToString() + " fue creada con éxito";
                    SavelimpiarControlesGR();
                    desactivarControlesGR();
                    MensajeAlerta();
                    saveActivarbotonesGR();                   
                }
                else
                {
                   
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La parametrización por reporte no se puede crear, ya existe";
                    MensajeAlerta();
                }
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
        protected void CMBBClienteRG_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboCanalXClienteBuscarGR();
            ModalBuscarGR.Show();
        }
        protected void CmbCanalBRG_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenacomboReporteBuscarGR();
            ModalBuscarGR.Show();
        }
        private void llenaGrilla(DataTable oDataTable)
        {
            GVGestionProdXReport.EditIndex = -1;
            GVGestionProdXReport.DataSource = null;
            GVGestionProdXReport.DataSource = oDataTable;
            GVGestionProdXReport.DataBind();
            MopopGrillaParametrosXReporte.Show();
        }
        protected void BuscarGRI_Click(object sender, EventArgs e) 
        {
            LblFaltantes.Text = "";
            if (CMBBClienteRG.Text == "0" || CmbCanalBRG.Text=="0")
            {               

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos Cliente y Canal";
                MensajeAlerta();
                ModalBuscarGR.Show();
                return;
            }

            DataTable odt = oParametroReporte.ConsultarGR(Convert.ToInt32(CMBBClienteRG.SelectedValue), CmbCanalBRG.SelectedValue, Convert.ToInt32(CMBReporRG.SelectedValue));
            this.Session["tParaReport"] = odt;

            if (odt != null)
            {
                if (odt.Rows.Count > 0)
                {
                    llenaGrilla(odt);
                }

                else
                {
                    saveActivarbotonesGR();
                    desactivarControlesGR();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La consulta realizada no devolvió ninguna respuesta";
                    MensajeAlerta();
                    ModalBuscarGR.Show();
                }
            }

            this.Session["Exportar_Excel"] = "Exportar_Para_Report";

            DataTable dtPara_Report = new DataTable();
            dtPara_Report.Columns.Add("Codigo", typeof(String));
            dtPara_Report.Columns.Add("Cliente", typeof(String));
            dtPara_Report.Columns.Add("Canal", typeof(String));
            dtPara_Report.Columns.Add("Reporte", typeof(String));
            dtPara_Report.Columns.Add("Tipo_Reporte", typeof(String));
            dtPara_Report.Columns.Add("Vista_Categoria", typeof(String));
            dtPara_Report.Columns.Add("Vista_Marca", typeof(String));
            dtPara_Report.Columns.Add("Vista_SubMarca", typeof(String));
            dtPara_Report.Columns.Add("Vista_Familia", typeof(String));
            dtPara_Report.Columns.Add("Vista_SubFamilia", typeof(String));
            dtPara_Report.Columns.Add("Vista_Producto", typeof(String));

            for (int i = 0; i <= GVGestionProdXReport.Rows.Count - 1; i++)
            {
                DataRow dr = dtPara_Report.NewRow();
                dr["Codigo"] = ((Label)GVGestionProdXReport.Rows[i].Cells[0].FindControl("Lblid_RelInfo_prodcutos")).Text;
                dr["Cliente"] = ((Label)GVGestionProdXReport.Rows[i].Cells[0].FindControl("LblCompany_Name")).Text;
                dr["Canal"] = ((Label)GVGestionProdXReport.Rows[i].Cells[0].FindControl("LblChannel_Name")).Text;
                dr["Reporte"] = ((Label)GVGestionProdXReport.Rows[i].Cells[0].FindControl("LblReport_NameReport")).Text;
                dr["Tipo_Reporte"] = ((Label)GVGestionProdXReport.Rows[i].Cells[0].FindControl("LblTypeReport_Name")).Text;
                dr["Vista_Categoria"] = ((CheckBox)GVGestionProdXReport.Rows[i].Cells[0].FindControl("CheckPXRVista_Categoria")).Checked;
                dr["Vista_Marca"] = ((CheckBox)GVGestionProdXReport.Rows[i].Cells[0].FindControl("CheckPXRVista_Marca")).Checked;
                dr["Vista_SubMarca"] = ((CheckBox)GVGestionProdXReport.Rows[i].Cells[0].FindControl("CheckPXRVista_SubMarca")).Checked;
                dr["Vista_Familia"] = ((CheckBox)GVGestionProdXReport.Rows[i].Cells[0].FindControl("CheckPXRVista_Familia")).Checked;
                dr["Vista_SubFamilia"] = ((CheckBox)GVGestionProdXReport.Rows[i].Cells[0].FindControl("CheckPXRVista_SubFamilia")).Checked;
                dr["Vista_Producto"] = ((CheckBox)GVGestionProdXReport.Rows[i].Cells[0].FindControl("CheckPXRVista_Producto")).Checked;
                dtPara_Report.Rows.Add(dr);
            }

            this.Session["CExporPara_Report"] = dtPara_Report;
        
        }
        private void MostrarDatosParametrosReport()
        {
            recorrido = (DataTable)this.Session["tparaXReport"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodgoGR.Text = recorrido.Rows[recsearch]["id_RelInfo_prodcutos"].ToString().Trim();
                    comboclienteenGR(cmbCompanyGR);
                    cmbCompanyGR.Text = recorrido.Rows[recsearch]["Company_id"].ToString().Trim();
                    //CmbClienteUI.Text = cmbClienteBUI.Text; 
                    ComboCanalXClienteGR(cmbCompanyGR, CmbCanalBRG);
                    CmbCanalGR.Text = recorrido.Rows[recsearch]["cod_Channel"].ToString().Trim();
                    LlenacomboReporteGR(cmbCompanyGR, CmbCanalBRG, CMBReporRG);
                    cmbReportGv.Text = recorrido.Rows[recsearch]["Report_Id"].ToString().Trim();
                    LlenacomboTipoReporteGR(cmbCompanyGR,CmbtipoReporte);
                    if (recorrido.Rows[recsearch]["id_Tipo_Reporte"].ToString().Trim() == "")
                    {
                        CmbtipoReporte.Text = "0";
                    }
                    else
                    {
                        CmbtipoReporte.Text = recorrido.Rows[recsearch]["id_Tipo_Reporte"].ToString().Trim();
                    }
                    chkVCategory.Checked = Convert.ToBoolean(recorrido.Rows[recsearch]["Vista_Categoria"].ToString().Trim());
                    chkVMarca.Checked = Convert.ToBoolean(recorrido.Rows[recsearch]["Vista_Marca"].ToString().Trim());
                    chkVSubMarca.Checked = Convert.ToBoolean(recorrido.Rows[recsearch]["Vista_SubMarca"].ToString().Trim());
                    chkVFamilia.Checked = Convert.ToBoolean(recorrido.Rows[recsearch]["Vista_Familia"].ToString().Trim());
                    chkVProducto.Checked = Convert.ToBoolean(recorrido.Rows[recsearch]["Vista_Producto"].ToString().Trim());
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["RelInfo_Status"].ToString().Trim());                                                   
                    
              

                    if (estado == true)
                    {
                        RadioButtonGR.Items[0].Selected = true;
                        RadioButtonGR.Items[1].Selected = false;
                    }
                    else
                    {
                        RadioButtonGR.Items[0].Selected = false;
                        RadioButtonGR.Items[1].Selected = true;
                    }
                }
            }

        }

        /**Eliminar un reporte parametrizado
         * 30/05/2011 Angel Ortiz
         * */

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            activarControlesGR();
            ConfirmDialog.CssClass = "ConfirmDialog";
            lblMessage.Text = "¿Está seguro de eliminar el registro: " + TxtCodgoGR.Text + " ?";
            MensajeConfirmacion();
        }

        protected void btnAcept_Click(object sender, EventArgs e)
        {
            int code = Convert.ToInt32(TxtCodgoGR.Text);
            eliminarparamxreport(code);   
            saveActivarbotonesGR();
            desactivarControlesGR();
            SavelimpiarControlesGR();
        }
        /** fin eliminar**/

        protected void primero_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tparaXReport"];
            MostrarDatosParametrosReport();

        }
        protected void sig_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosParametrosReport();
            }
        }
        protected void ant_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tparaXReport"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosParametrosReport();
            }
        }
        protected void ultimo_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tparaXReport"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosParametrosReport();

        }
        protected void BtnActualizarGR_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            try
            {
                if (RadioButtonGR.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    //DAplicacion oddeshaReporChanel = new DAplicacion();
                    //DataTable dt = oddeshaReporChanel.PermitirDeshabilitar(ConfigurationManager.AppSettings["Reports_Channels"], TxtCodAC.Text);
                    //if (dt != null)
                    //{
                    //    Alertas.CssClass = "MensajesError";
                    //    LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                    //    MensajeAlerta();
                    //    return;
                    //}
                }
               // EAD_GestionProductosXReporte oeaParametroReport = oParametroReporte.Actualizar_ParametroReport(Convert.ToInt32(TxtCodgoGR.Text), Convert.ToBoolean(chkVCategory.Checked), Convert.ToBoolean(chkVMarca.Checked), Convert.ToBoolean(chkVSubMarca.Checked), Convert.ToBoolean(chkVFamilia.Checked), Convert.ToBoolean(chkVProducto.Checked), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                //sNomRepCanal = cmbCanalRC.SelectedValue + "/" + cmbReportesRC.SelectedValue;
                //this.Session["sNomRepCanal"] = sNomRepCanal;
                SavelimpiarControlesGR();
                //llenarcombos();
                //this.Session["mensajealert"] = "La asociación de informe con servicio";
                Alertas.CssClass = "MensajesCorrecto";
                LblFaltantes.Text = "La parametrización por Reporte se actualizó correctamente";
                MensajeAlerta();
                saveActivarbotonesGR();
                desactivarControlesGR();
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
        protected void BtnEditarGR_Click(object sender, EventArgs e)
        {
            EditarActivarControleGR();
            EditarActivarbotonesGR();
        }
        protected void BtnCancelarGR_Click(object sender, EventArgs e)
        {
            saveActivarbotonesGR();
            desactivarControlesGR();
            SavelimpiarControlesGR();
        }
        
        #endregion

        protected void GVGestionProdXReport_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVGestionProdXReport.EditIndex = -1;
            llenaGrilla((DataTable)this.Session["tParaReport"]);
            btnCanGvProdXReport.Visible = true;
            MopopGrillaParametrosXReporte.Show();
        }

        protected void GVGestionProdXReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVGestionProdXReport.PageIndex = e.NewPageIndex;
            GVGestionProdXReport.DataSource = (DataTable)this.Session["tParaReport"];
            GVGestionProdXReport.DataBind();
            MopopGrillaParametrosXReporte.Show();
        }

        protected void GVGestionProdXReport_RowEditing(object sender, GridViewEditEventArgs e)
        {
            btnCanGvProdXReport.Visible = false;
            MopopGrillaParametrosXReporte.Show();
            GVGestionProdXReport.EditIndex = e.NewEditIndex;
            string Codigo, nombre, tipo, direc, corpo, pais, departamento, provincia, distrito, barrio, idpais, iddepartamento, idprovincia, iddistrito;
            bool VistaCategoria, VistaMarca, VistaSubMarca, VistaProducto, VistaFamila, VistaSubFamilia, estado;

            //Codigo = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodAgruCom")).Text;
            //nombre = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblNomAgrupCom")).Text;
            //tipo = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblNodeTypeName")).Text;
            //direc = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblAgrupDirec")).Text;
            //corpo = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCorpName")).Text;
            //estado = ((CheckBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("CheckAgrupCom")).Checked;
            VistaCategoria=((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[0].FindControl("CheckPXRVista_Categoria")).Checked;
            VistaMarca = ((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[0].FindControl("CheckPXRVista_Marca")).Checked;
            VistaSubMarca=((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[0].FindControl("CheckPXRVista_SubMarca")).Checked; 
            VistaProducto=((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[0].FindControl("CheckPXRVista_Familia")).Checked ;
            VistaFamila=((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[0].FindControl("CheckPXRVista_SubFamilia")).Checked; 
            VistaSubFamilia=((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[0].FindControl("CheckPXRVista_Producto")).Checked ;
            estado = ((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[0].FindControl("CheckPXREstado")).Checked;
            //pais = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblPaisNombre")).Text;
            //departamento = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblDepartamentoNombre")).Text;
            //provincia = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblProvinciaNombre")).Text;
            //distrito = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblDistritoNombre")).Text;
            //barrio = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblBarrioNombre")).Text;
            //idpais = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodPais")).Text;
            //iddepartamento = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodDepartamento")).Text;
            //idprovincia = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodProvincia")).Text;
            //iddistrito = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodDistrito")).Text;



            GVGestionProdXReport.DataSource = (DataTable)this.Session["tParaReport"];
            GVGestionProdXReport.DataBind();
            //if (tipo.Equals(""))
            //    tipo = "<Seleccione...>";
            //if (corpo.Equals(""))
            //    corpo = "<Seleccione...>";
            //((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodAgruCom")).Text = Codigo;
            //((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text = nombre;
            //LlenacomboConsultaTypeMode();
            //((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[2].FindControl("cmb_Node_Type")).Items.FindByText(tipo).Selected = true;
            //((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text = direc;
            //LlenacomboConsultaCorpo();
            //((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).Items.FindByText(corpo).Selected = true;
            ((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[5].FindControl("CheckPXRVista_Categoria")).Checked = VistaCategoria;
            ((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[6].FindControl("CheckPXRVista_Marca")).Checked = VistaMarca;
            ((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[7].FindControl("CheckPXRVista_SubMarca")).Checked = VistaSubMarca;
            ((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[8].FindControl("CheckPXRVista_Familia")).Checked = VistaProducto;
            ((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[9].FindControl("CheckPXRVista_SubFamilia")).Checked = VistaFamila;
            ((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[10].FindControl("CheckPXRVista_Producto")).Checked = VistaSubFamilia;
            ((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[10].FindControl("CheckPXREstado")).Checked = estado;

            //((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text.TrimStart();
            //((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text.TrimEnd();
            //((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text.TrimStart();
            //((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text.TrimEnd();

            //this.Session["rept"] = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text;
            //this.Session["rept1"] = ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[2].FindControl("cmb_Node_Type")).Text;
            //this.Session["rept2"] = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text;
            //this.Session["rept3"] = ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).Text;
        }

        protected void GVGestionProdXReport_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            LblFaltantes.Text = "";

            this.Session["sCompany_id"] = ((Label)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[0].FindControl("LblCompany_id")).Text;
            this.Session["scod_Channel"] = ((Label)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[0].FindControl("Lblcod_Channel")).Text;
            this.Session["sReport_id"] = ((Label)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[0].FindControl("LblReport_id")).Text;
            
            try
            {


                EAD_GestionProductosXReporte oeaParametroReport = oParametroReporte.Actualizar_ParametroReport(Convert.ToInt32(((Label)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[0].FindControl("Lblid_RelInfo_prodcutos")).Text), Convert.ToBoolean(((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[5].FindControl("CheckPXRVista_Categoria")).Checked), Convert.ToBoolean(((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[6].FindControl("CheckPXRVista_Marca")).Checked), Convert.ToBoolean(((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[7].FindControl("CheckPXRVista_SubMarca")).Checked), Convert.ToBoolean(((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[8].FindControl("CheckPXRVista_Familia")).Checked), Convert.ToBoolean(((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[9].FindControl("CheckPXRVista_SubFamilia")).Checked), Convert.ToBoolean(((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[10].FindControl("CheckPXRVista_Producto")).Checked), Convert.ToBoolean(((CheckBox)GVGestionProdXReport.Rows[GVGestionProdXReport.EditIndex].Cells[11].FindControl("CheckPXREstado")).Checked), Convert.ToString(this.Session["sUser"]), DateTime.Now);

                //sNomRepCanal = cmbCanalRC.SelectedValue + "/" + cmbReportesRC.SelectedValue;
                //this.Session["sNomRepCanal"] = sNomRepCanal;

                SavelimpiarControlesGR();
                GVGestionProdXReport.EditIndex = -1;
                DataTable oDataTable = oParametroReporte.ConsultarGR(Convert.ToInt32(this.Session["sCompany_id"].ToString().Trim()), this.Session["scod_Channel"].ToString().Trim(),Convert.ToInt32(this.Session["sReport_id"].ToString().Trim()));
                this.Session["tParaReport"] = oDataTable;
                if (oDataTable != null)
                {
                    if (oDataTable.Rows.Count > 0)
                    {
                        llenaGrilla(oDataTable);
                    }
                }
                btnCanGvProdXReport.Visible = true;
                MopopGrillaParametrosXReporte.Show();
                Alertas.CssClass = "MensajesCorrecto";
                LblFaltantes.Text = "La parametrización por Reporte se actualizó correctamente";
                MensajeAlerta();
                saveActivarbotonesGR();
                desactivarControlesGR();


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


        #region TipoReporte

        private void activarControlesTipoReporte()
        {

            txtCodigoTipoReporte.Enabled = false;
            txtNombreTipoReporte.Enabled = true;
            rbtEstadoTiposReporte.Enabled = true;

            //navegadores          
            btnPreg9.Visible = false;
            btnAreg9.Visible = false;
            btnSreg9.Visible = false;
            btnUreg9.Visible = false;
            Panel_informe.Enabled = false;
            Panel_info_Servicio.Enabled = false;
            TabGestionReporte.Enabled = false;
            PanelUsuariovsInforme.Enabled = false;
            TabAsignaciónCobertura.Enabled = false;
            Tab_Report_Channel.Enabled = false;
            Tab_Reporte_Oficina.Enabled = false;
            Panel_TipoReporte.Enabled = true;

        }
        private void crearActivarbotonesTiposReporte()
        {
            BtnCrearTiposReporte.Visible = false;
            BtnGuardarTiposReporte.Visible = true;
            BtnConsultarTiposReporte.Visible = false;
            BtnEditarTiposReporte.Visible = false;
            rbtEstadoTiposReporte.Enabled = false;
            BtnActualizarTiposReporte.Visible = false;
            BtnCancelarTiposReporte.Visible = true;
        }

        private void SavelimpiarControlesTiposReporte()
        {

         
            txtCodigoTipoReporte.Text = "";
            txtNombreTipoReporte.Text = "";

            rbtEstadoTiposReporte.Items[0].Selected = true;
            rbtEstadoTiposReporte.Items[1].Selected = false;

            ddlTiposReporte.Text = "0";
        }

        private void saveActivarbotonesTiposReporte()
        {
            BtnCrearTiposReporte.Visible = true;
            BtnGuardarTiposReporte.Visible = false;
            BtnConsultarTiposReporte.Visible = true;
            BtnEditarTiposReporte.Visible = false;
            BtnActualizarTiposReporte.Visible = false;
            BtnCancelarTiposReporte.Visible = true;

        }

        private void desactivarControlesTiposReporte()
        {

            rbtEstadoTiposReporte.Enabled = false;
            txtCodigoTipoReporte.Enabled = false;
            txtNombreTipoReporte.Enabled = false;

            TxtDescReport.Enabled = false;
            Panel_informe.Enabled = true;
            Panel_info_Servicio.Enabled = true;
            PanelUsuariovsInforme.Enabled = true;
            TabAsignaciónCobertura.Enabled = true;
            Tab_Report_Channel.Enabled = true;
            Tab_Reporte_Oficina.Enabled = true;
            TabGestionReporte.Enabled = true;
            Panel_TipoReporte.Enabled = true;
        }

        private void BuscarActivarbotnesTiposReporte()
        {

            BtnCrearTiposReporte.Visible = false;
            BtnGuardarTiposReporte.Visible = false;
            BtnConsultarTiposReporte.Visible = true;
            BtnEditarTiposReporte.Visible = true;
            BtnActualizarTiposReporte.Visible = false;
            BtnCancelarTiposReporte.Visible = true;

        }

        protected void BtnCrearTiposReporte_Click(object sender, EventArgs e)
        {
            activarControlesTipoReporte();
            crearActivarbotonesTiposReporte();
            SavelimpiarControlesTiposReporte();
        }

        protected void BtnCancelarTiposReporte_Click(object sender, EventArgs e)
        {
            SavelimpiarControlesTiposReporte();
            saveActivarbotonesTiposReporte();
            desactivarControlesTiposReporte();
        }

        protected void BtnEditarTiposReporte_Click(object sender, EventArgs e)
        {
            BtnEditarTiposReporte.Visible = false;
            BtnActualizarTiposReporte.Visible = true;
            activarControlesTipoReporte();
            this.Session["rept"] = txtNombreTipoReporte.Text;
        }

        protected void BtnBuscarTiposReportePopup_Click(object sender, EventArgs e)
        {
            desactivarControlesTiposReporte();
            LblFaltantes.Text = "";
            if (ddlTiposReporte.Text == "0" )
            {
                this.Session["mensajealert"] = "servicio e informe";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = " Porfavor Ingrese todos los parametros de consulta";
                MensajeAlerta();
                mopoTiposReporte.Show();
                return;
            }

            BuscarActivarbotnesTiposReporte();
            sReportid = ddlTiposReporte.SelectedValue;

            DataTable oeInforme = oReports.BuscarInforme(sReportid);

            if (oeInforme != null)
            {
                if (oeInforme.Rows.Count > 0)
                {
                    CmbNameReporte.Text = "0";
                    for (int i = 0; i <= oeInforme.Rows.Count - 1; i++)
                    {
                        //LlenaComboInforme();
                        TxtCodReport.Text = oeInforme.Rows[0]["Report_Id"].ToString().Trim();
                        txtNomReport.Text = oeInforme.Rows[0]["Report_NameReport"].ToString().Trim();
                        TxtDescReport.Text = oeInforme.Rows[0]["Report_Description"].ToString().Trim();
                        estado = Convert.ToBoolean(oeInforme.Rows[0]["Report_Status"].ToString().Trim());
                        //DataTable oeInformesmodulos = oReports.BuscarInformeModulo(Convert.ToInt32(TxtCodReport.Text));
                        //if (oeInformesmodulos != null)
                        //{
                        //    if (oeInformesmodulos.Rows.Count > 0)
                        //    {
                        //        for (int j = 0; j <= oeInformesmodulos.Rows.Count - 1; j++)
                        //        {
                        //            for (int k = 0; k <= ChkSelModulo.Items.Count - 1; k++)
                        //            {
                        //                if (ChkSelModulo.Items[k].Value == oeInformesmodulos.Rows[j]["Modulo_id"].ToString().Trim())
                        //                {
                        //                    ChkSelModulo.Items[k].Selected = Convert.ToBoolean(oeInformesmodulos.Rows[j]["ReportModulo_Status"].ToString().Trim());
                        //                    k = ChkSelModulo.Items.Count - 1;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //DataTable oeInformesTipInf = oReports.BuscarInformetipoInf(Convert.ToInt32(TxtCodReport.Text));
                        //if (oeInformesTipInf != null)
                        //{
                        //    if (oeInformesTipInf.Rows.Count > 0)
                        //    {
                        //        for (int j = 0; j <= oeInformesTipInf.Rows.Count - 1; j++)
                        //        {
                        //            for (int k = 0; k <= ChkTipInf.Items.Count - 1; k++)
                        //            {
                        //                if (ChkTipInf.Items[k].Value == oeInformesTipInf.Rows[j]["id_TypeReport"].ToString().Trim())
                        //                {
                        //                    ChkTipInf.Items[k].Selected = Convert.ToBoolean(oeInformesTipInf.Rows[j]["ReportTypeReport_Status"].ToString().Trim());
                        //                    k = ChkTipInf.Items.Count - 1;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}


                        if (estado == true)
                        {
                            RBtnListStatusReport.Items[0].Selected = true;
                            RBtnListStatusReport.Items[1].Selected = false;
                        }
                        else
                        {
                            RBtnListStatusReport.Items[0].Selected = false;
                            RBtnListStatusReport.Items[1].Selected = true;
                        }
                        this.Session["tinformes"] = oeInforme;
                        this.Session["i"] = 0;
                    }
                    if (oeInforme.Rows.Count == 1)
                    {
                        btnPreg9.Visible = false;
                        btnUreg9.Visible = false;
                        btnAreg9.Visible = false;
                        btnSreg9.Visible = false;
                    }
                    else
                    {
                        btnPreg9.Visible = true;
                        btnUreg9.Visible = true;
                        btnAreg9.Visible = true;
                        btnSreg9.Visible = true;
                    }

                }
                else
                {
                    SavelimpiarControlesInforme();
                    saveActivarbotonesInforme();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnReportes_ModalPopupExtender.Show();
                }
            }
        }

        #endregion

        protected void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            bool seleccion = true;
             if (chkTodos.Text == "Seleccionar Todos")
                {
                    chkTodos.Text = "Deseleccionar Todos";
                    seleccion = true;
                }
             else
             {
                 chkTodos.Text = "Seleccionar Todos";
                 seleccion = false;
             }
            for (int j = 0; j <= CheckCiudades.Items.Count - 1; j++)
            {


                CheckCiudades.Items[j].Selected = seleccion;
               
                    
               
              


               

                //if (ds1.Tables[0].Rows[i][0].ToString().Trim() == CheckCiudades.Items[j].Value)
                //{ ///compara los que se encuentran en el check y los de la tabla si son iguales los chulea si no los deja en false(Nunca entra por aca, nunca chulea nada)
                    //if (ds1.Tables[0].Rows[i][1].ToString().Trim() == "True")
                    //{
                    //    CheckCiudades.Items[j].Selected = true;
                    //    j = CheckCiudades.Items.Count - 1;
                    //}
                    //else
                    //{
                    //    CheckCiudades.Items[j].Selected = false;
                    //}
                //}
            }
        }








    }    
}
    
