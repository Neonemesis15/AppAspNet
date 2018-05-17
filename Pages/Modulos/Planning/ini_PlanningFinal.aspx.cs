using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Business.Common.Application;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;

namespace SIGE.Pages.Modulos.Planning
{
    public partial class ini_PlanningFinal : System.Web.UI.Page
    {
        private string sUser;
        private string sPassw;
        private string sNameUser;
        private bool sigue = true;
        private bool asignacionsup = false;
        private int j = 0;

        private DateTime fechaSolicitudP;
        private DateTime fechaFinalP;
        private DateTime fechaIniPreP;
        private DateTime fechaIniPre;
        private DateTime fechaFinPreP;
        private DateTime fechaIniPlaP;
        private DateTime fechaPlaFinP;

        private Conexion oCoon = new Conexion();
        private Staff_Planning Staff_Planning = new Staff_Planning();
        private PointOfSale_PlanningOper PointOfSale_PlanningOper = new PointOfSale_PlanningOper();

        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE Presupuesto = new SIGE.Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE();
        private Facade_Menu_strategy.Facade_MPlanning menu = new SIGE.Facade_Menu_strategy.Facade_MPlanning();
        private Facade_Proceso_Planning.Facade_Proceso_Planning wsPlanning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        private Facade_Proceso_Cliente.Facade_Proceso_Cliente wsCliente = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();
        private int level_carga;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    sUser = this.Session["sUser"].ToString();
                    sPassw = this.Session["sPassw"].ToString();
                    sNameUser = this.Session["nameuser"].ToString();
                    usersession.Text = sUser;
                    lblUsuario.Text = sNameUser;
                    if (sUser != null && sPassw != null)
                    {
                        InicializarPaneles();
                        // LlenaPresupuestosAsignados();
                    }
                    Años(ddlAño);
                    Años(ddlAño2);
                }
                catch (Exception ex)
                {
                    Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                    this.Session.Abandon();
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
        }

        #region Funciones
        private void StiloBotonSeleccionado()
        {
            BtnAsignaPresupuesto.CssClass = "buttonNewPlan";
            BtnDescripcionCampaña.CssClass = "buttonNewPlan";
            BtnResponsables.CssClass = "buttonNewPlan";
            BtnAsignaPersonal.CssClass = "buttonNewPlan";
            BtnPDV.CssClass = "buttonNewPlan";
            BtnPaneles.CssClass = "buttonNewPlan";
            BtnProductos.CssClass = "buttonNewPlan";
            BtnAsignaPDVaOpe.CssClass = "buttonNewPlan";
            BtnReportes.CssClass = "buttonNewPlan";
            btnPanelPtoVenta.CssClass = "buttonNewPlan";
            btnPanelMaterialPOP.CssClass = "buttonNewPlan";
        }
        private void StiloBotonTipoProdSeleccionado()
        {
            BtnProdPropio.CssClass = "buttonNewPlan";
            BtnProdCompe.CssClass = "buttonNewPlan";
        }

        //private void LlenarFrecuencias() {
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OBTENERFRECUENCIAS");
        //        if(dt.Rows.Count>0){                    
        //            ChklstFrecuencia.DataSource = dt;
        //            ChklstFrecuencia.DataValueField = "id_Frecuencia";
        //            ChklstFrecuencia.DataTextField = "Txt_Frecuencia";
        //            ChklstFrecuencia.DataBind();                
        //        }        
        //}

        private void Habilitabotones()
        {
            BtnAsignaPresupuesto.Enabled = true;
            BtnDescripcionCampaña.Enabled = true;
            BtnResponsables.Enabled = true;
            BtnAsignaPersonal.Enabled = true;
            BtnPDV.Enabled = true;
            BtnPaneles.Enabled = true;
            BtnProductos.Enabled = true;
            BtnAsignaPDVaOpe.Enabled = true;
            BtnReportes.Enabled = true;
            btnPanelPtoVenta.Enabled = true;
            btnPanelMaterialPOP.Enabled = true;
            
        }
        private void HabilitabotonesTipoProd()
        {
            BtnProdPropio.Enabled = true;
            BtnProdCompe.Enabled = true;
        }
        private void InicializarPaneles()
        {
            PanelASignaPresupuesto.Style.Value = "Display:none;";
            PanelDescCampaña.Style.Value = "Display:none;";
            PanelResponsablesCampaña.Style.Value = "Display:none;";
            PanelAsignaPersonal.Style.Value = "Display:none;";
            PanelAsignaPDV.Style.Value = "Display:none;";
            PanelPanelesPlanning.Style.Value = "Display:none;";
            PanelAsignaProductos.Style.Value = "Display:none;";
            PanelCargaMasivaProductos.Style.Value = "Display:none;";
            PanelAsignacionPDVaoper.Style.Value = "Display:none;";
            PanelCargaMasivaAsignapdv.Style.Value = "Display:none;";
            PanelReportesCampaña.Style.Value = "Display:none;";
            PanelCarga2080.Style.Value = "Display:none;";
            panelPuntoVenta.Style.Value = "Display:none;";
            PanelPtoVenta_Masivo.Style.Value = "Display:none;";
            Panel_AsignacionMaterial_POP.Style.Value = "Display:none;";
        }
        private bool LlenaPresupuestos()
        {
            bool Continuar = true;
            try
            {
                DataTable dt = new DataTable(); ;
                dt = Presupuesto.Presupuesto(this.Session["scountry"].ToString(), Convert.ToInt32(this.Session["companyid"].ToString().Trim()));

                if (dt != null)
                {
                    if (dt.Rows.Count > 1)
                    {
                        cmbpresupuesto.DataSource = dt;
                        cmbpresupuesto.DataValueField = "Numero_Presupuesto";
                        cmbpresupuesto.DataTextField = "Nombre";
                        cmbpresupuesto.DataBind();
                        this.Session["NameCountry"] = dt.Rows[1]["Name_Country"].ToString().Trim();
                        dt = null;
                    }
                    else
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        //Francisco Martinez: Se acomoda el mensaje - 25/03/2010
                        //this.Session["mensaje"] = "No Existen Presupuesto Disponibles";
                        this.Session["mensaje"] = "No hay presupuesto disponible";
                        Mensajes_Usuario();
                        InicializarPaneles();
                        Continuar = false;
                    }
                }
                return Continuar;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
                Continuar = false;
                return Continuar;
            }
        }
        private void LlenaPresupuestosAsignados()
        {
            try
            {
                DataTable dt = new DataTable(); ;
                dt = Presupuesto.Presupuesto_Search(Convert.ToInt32(this.Session["companyid"].ToString().Trim()));

                if (dt != null)
                {
                    if (dt.Rows.Count > 1)
                    {
                        CmbSelPresupuestoDesc.DataSource = dt;
                        CmbSelPresupuestoDesc.DataValueField = "Numero_Presupuesto";
                        CmbSelPresupuestoDesc.DataTextField = "Nombre";
                        CmbSelPresupuestoDesc.DataBind();

                        CmbSelPresupuestoRes.DataSource = dt;
                        CmbSelPresupuestoRes.DataValueField = "Numero_Presupuesto";
                        CmbSelPresupuestoRes.DataTextField = "Nombre";
                        CmbSelPresupuestoRes.DataBind();

                        CmbSelPresupuestoAsig.DataSource = dt;
                        CmbSelPresupuestoAsig.DataValueField = "Numero_Presupuesto";
                        CmbSelPresupuestoAsig.DataTextField = "Nombre";
                        CmbSelPresupuestoAsig.DataBind();

                        CmbSelPresupuestoPanel.DataSource = dt;
                        CmbSelPresupuestoPanel.DataValueField = "Numero_Presupuesto";
                        CmbSelPresupuestoPanel.DataTextField = "Nombre";
                        CmbSelPresupuestoPanel.DataBind();

                        CmbSelPresupuestoAsigProd.DataSource = dt;
                        CmbSelPresupuestoAsigProd.DataValueField = "Numero_Presupuesto";
                        CmbSelPresupuestoAsigProd.DataTextField = "Nombre";
                        CmbSelPresupuestoAsigProd.DataBind();

                        CmbSelPresupuestoAsigPDVOPE.DataSource = dt;
                        CmbSelPresupuestoAsigPDVOPE.DataValueField = "Numero_Presupuesto";
                        CmbSelPresupuestoAsigPDVOPE.DataTextField = "Nombre";
                        CmbSelPresupuestoAsigPDVOPE.DataBind();

                        CmbSelPresupuestoReportes.DataSource = dt;
                        CmbSelPresupuestoReportes.DataValueField = "Numero_Presupuesto";
                        CmbSelPresupuestoReportes.DataTextField = "Nombre";
                        CmbSelPresupuestoReportes.DataBind();


                        
                        CmbSelPresupuestoPanelesPtoVenta.DataSource = dt;
                        CmbSelPresupuestoPanelesPtoVenta.DataValueField = "Numero_Presupuesto";
                        CmbSelPresupuestoPanelesPtoVenta.DataTextField = "Nombre";
                        CmbSelPresupuestoPanelesPtoVenta.DataBind();


                        ddlAsignasionMatePOP.DataSource = dt;
                        ddlAsignasionMatePOP.DataValueField = "Numero_Presupuesto";
                        ddlAsignasionMatePOP.DataTextField = "Nombre";
                        ddlAsignasionMatePOP.DataBind();

                    }
                    else
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "Usted pertenece a una compañia que no tiene campañas creadas. Por favor consulte con el Administrador Xplora";
                        Mensajes_Usuario();
                        InicializarPaneles();
                    }

                }
                dt = null;
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void LlenaCanales()
        {
            DataTable dt = new DataTable();
            dt = wsCliente.Get_ObtenerCanalesxCliente(Convert.ToInt32(this.Session["company_id"].ToString().Trim()));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    RbtnCanal.DataSource = dt;
                    RbtnCanal.DataValueField = "codigo_canal";
                    RbtnCanal.DataTextField = "nombre_canal".ToLower();
                    RbtnCanal.DataBind();
                }
            }
            dt = null;
        }
        private void llenaEjecutivos()
        {
            DataTable dt = wsPlanning.ObtenerEjecutivos(Convert.ToInt32(this.Session["company_id"]));

            ChkSelEjecutivos.DataSource = dt;
            ChkSelEjecutivos.DataTextField = "name_user";
            ChkSelEjecutivos.DataValueField = "Person_id";
            ChkSelEjecutivos.DataBind();
            ChkSelEjecutivos.Items.Remove(ChkSelEjecutivos.Items[0]);
        }
        private void llenaNuevosSupervisores()
        {
            DataSet dsSupervisor = (DataSet)this.ViewState["ds_Personal"];
            if (dsSupervisor != null)
            {
                if (dsSupervisor.Tables[1].Rows.Count > 0)
                {
                    LstNewSupervisor.DataSource = dsSupervisor.Tables[1];
                    LstNewSupervisor.DataTextField = "name_user";
                    LstNewSupervisor.DataValueField = "Person_id";
                    LstNewSupervisor.DataBind();
                }
            }
            dsSupervisor = null;
        }
        private void llenaNuevosMercaderistas()
        {
            DataSet dsMercaderista = (DataSet)this.ViewState["ds_Personal"];
            if (dsMercaderista != null)
            {
                if (dsMercaderista.Tables[3].Rows.Count > 0)
                {
                    LstNewMercaderista.DataSource = dsMercaderista.Tables[3];
                    LstNewMercaderista.DataTextField = "name_user";
                    LstNewMercaderista.DataValueField = "Person_id";
                    LstNewMercaderista.DataBind();
                }
            }
            dsMercaderista = null;
        }
        private void llenaStaffplanning()
        {
            DataSet dsStaffPlanning = wsPlanning.Get_Staff_Planning(TxtPlanningAsig.Text);
            if (dsStaffPlanning != null)
            {
                if (dsStaffPlanning.Tables[0].Rows.Count > 0 && dsStaffPlanning.Tables[1].Rows.Count > 0)
                {
                    Lisboxsupervi.DataSource = dsStaffPlanning.Tables[0];
                    Lisboxsupervi.DataTextField = "name_user";
                    Lisboxsupervi.DataValueField = "Person_id";
                    Lisboxsupervi.DataBind();

                    LstBoxMercaderistas.DataSource = dsStaffPlanning.Tables[1];
                    LstBoxMercaderistas.DataTextField = "name_user";
                    LstBoxMercaderistas.DataValueField = "Person_id";
                    LstBoxMercaderistas.DataBind();
                    sigue = true;
                }
                else
                {
                    sigue = false;
                }
            }
        }

        private void llenaOperativosAsignaPDVOPE()
        {
            DataSet dsStaffPlanning = wsPlanning.Get_Staff_Planning(TxtPlanningAsigPDVOPE.Text);
            if (dsStaffPlanning != null)
            {
                if (dsStaffPlanning.Tables[1].Rows.Count > 0)
                {
                    CmbSelOpePlanning.DataSource = dsStaffPlanning.Tables[1];
                    CmbSelOpePlanning.DataTextField = "name_user";
                    CmbSelOpePlanning.DataValueField = "Person_id";
                    CmbSelOpePlanning.DataBind();
                    CmbSelOpePlanning.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
                    sigue = true;
                }
                else
                {
                    sigue = false;
                }
            }
        }

        private void llenacompetidores()
        {
            DataTable dtCompetidores = wsPlanning.Get_ObtenerCompetidoresCliente(Convert.ToInt32(this.Session["company_id"]));
            CmbCompetidores.DataSource = dtCompetidores;
            CmbCompetidores.DataTextField = "Company_Name";
            CmbCompetidores.DataValueField = "Compay_idCompe";
            CmbCompetidores.DataBind();
            CmbCompetidores.Items[0].Text = "<Seleccione...>";
        }


        private void LlenaPeriodos(int iaño, int imes)
        {
            DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENERPREIODOS", iaño, imes);
            ChklstFrecuencia.DataSource = dt;
            ChklstFrecuencia.DataTextField = "dia";
            ChklstFrecuencia.DataValueField = "dia";
            ChklstFrecuencia.DataBind();
        }
        
        private void llenaciudades()
        {

            DataSet ds = wsPlanning.Get_cityPointofsalePlanning(TxtPlanningAsigPDVOPE.Text, "0", 0, "0", 0, 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                CmbSelCity.DataSource = ds.Tables[0];

                CmbSelCity.DataValueField = "cod_city";
                CmbSelCity.DataTextField = "name_city";
                CmbSelCity.DataBind();
                CmbSelCity.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            }
            else
            {
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "No se han creado puntos de venta para la campaña: " + CmbSelPresupuestoAsigPDVOPE.SelectedItem.Text.ToUpper();
                Mensajes_Usuario();

                BtnSaveAsigPDVOPE.Enabled = false;
                Inactivar_AsignacionPDVOPE();
            }
            ds = null;
            CmbSelTipoAgrup.Items.Clear();
            CmbSelAgrup.Items.Clear();
            CmbSelOficina.Items.Clear();
            CmbSelMalla.Items.Clear();


        }
        private void llenaTipoAgrup()
        {
            DataSet ds = wsPlanning.Get_cityPointofsalePlanning(TxtPlanningAsigPDVOPE.Text, CmbSelCity.Text, 0, "0", 0, 0);

            CmbSelTipoAgrup.DataSource = ds.Tables[1];

            CmbSelTipoAgrup.DataValueField = "idNodeComType";
            CmbSelTipoAgrup.DataTextField = "NodeComType_name";
            CmbSelTipoAgrup.DataBind();
            CmbSelTipoAgrup.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            ds = null;
            CmbSelAgrup.Items.Clear();
            CmbSelOficina.Items.Clear();
            CmbSelMalla.Items.Clear();
            CmbSelSector.Items.Clear();
            ChkListPDV.Items.Clear();

        }
        private void llenaAgrup()
        {
            DataSet ds = wsPlanning.Get_cityPointofsalePlanning(TxtPlanningAsigPDVOPE.Text, CmbSelCity.Text, Convert.ToInt32(CmbSelTipoAgrup.Text), "0", 0, 0);

            CmbSelAgrup.DataSource = ds.Tables[2];

            CmbSelAgrup.DataValueField = "NodeCommercial";
            CmbSelAgrup.DataTextField = "commercialNodeName";
            CmbSelAgrup.DataBind();
            CmbSelAgrup.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            ds = null;
            CmbSelOficina.Items.Clear();
            CmbSelMalla.Items.Clear();
            CmbSelSector.Items.Clear();
            ChkListPDV.Items.Clear();
        }
        private void llenaOficinas()
        {
            DataSet ds = wsPlanning.Get_cityPointofsalePlanning(TxtPlanningAsigPDVOPE.Text, CmbSelCity.Text, Convert.ToInt32(CmbSelTipoAgrup.Text), CmbSelAgrup.Text, 0, 0);

            CmbSelOficina.DataSource = ds.Tables[3];

            CmbSelOficina.DataValueField = "cod_Oficina";
            CmbSelOficina.DataTextField = "Name_Oficina";
            CmbSelOficina.DataBind();
            CmbSelOficina.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            ds = null;
            CmbSelMalla.Items.Clear();
            CmbSelSector.Items.Clear();
            ChkListPDV.Items.Clear();
        }
        private void Llenamallas()
        {

            DataSet ds = wsPlanning.Get_cityPointofsalePlanning(TxtPlanningAsigPDVOPE.Text, CmbSelCity.Text, Convert.ToInt32(CmbSelTipoAgrup.Text), CmbSelAgrup.Text, Convert.ToInt64(CmbSelOficina.Text), 0);

            CmbSelMalla.DataSource = ds.Tables[4];

            CmbSelMalla.DataValueField = "id_malla";
            CmbSelMalla.DataTextField = "malla";
            CmbSelMalla.DataBind();
            CmbSelMalla.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            ds = null;
            CmbSelSector.Items.Clear();
        }
        private void Llenasector()
        {
            DataSet ds = wsPlanning.Get_cityPointofsalePlanning(TxtPlanningAsigPDVOPE.Text, CmbSelCity.Text, Convert.ToInt32(CmbSelTipoAgrup.Text), CmbSelAgrup.Text, Convert.ToInt64(CmbSelOficina.Text), Convert.ToInt32(CmbSelMalla.Text));

            CmbSelSector.DataSource = ds.Tables[5];

            CmbSelSector.DataValueField = "id_sector";
            CmbSelSector.DataTextField = "Sector";
            CmbSelSector.DataBind();
            CmbSelSector.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void LlenaPDVPlanning()
        {
            DataTable dt = PointOfSale_PlanningOper.Consultar_PDVPlanning(TxtPlanningAsigPDVOPE.Text, CmbSelCity.Text, Convert.ToInt32(CmbSelTipoAgrup.Text), CmbSelAgrup.Text, Convert.ToInt64(CmbSelOficina.Text), Convert.ToInt32(CmbSelMalla.Text), Convert.ToInt32(CmbSelSector.Text));
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ChkListPDV.DataSource = dt;
                    ChkListPDV.DataTextField = "Nombre";
                    ChkListPDV.DataValueField = "id_MPOSPlanning";
                    ChkListPDV.DataBind();
                    BtnAllPDV.Visible = true;
                    BtnNonePDV.Visible = true;
                }
                else
                {
                    ChkListPDV.Items.Clear();
                    BtnAllPDV.Visible = false;
                    BtnNonePDV.Visible = false;
                }
            }
        }
        private void llenameses()
        {
            DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CONSULTA_INFORMESYMESES_PLANNING", TxtPlanningReportes.Text, 0, "NONE", Convert.ToInt32(CmbSelAñoCampaña.SelectedItem.Text));


            RbtnListmeses.DataSource = ds.Tables[0];
            RbtnListmeses.DataValueField = "id_Month";
            RbtnListmeses.DataTextField = "Month_name";
            RbtnListmeses.DataBind();

            //ChkListMeses.DataSource = ds.Tables[0];
            //ChkListMeses.DataValueField = "id_Month";
            //ChkListMeses.DataTextField = "Month_name";
            //ChkListMeses.DataBind();
            ds = null;
        }
        private void llenaaños()
        {
            DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CONSULTA_INFORMESYMESES_PLANNING", TxtPlanningReportes.Text, 0, "NONE", 0);
            CmbSelAñoCampaña.DataSource = ds.Tables[2];
            CmbSelAñoCampaña.DataValueField = "Years_id";
            CmbSelAñoCampaña.DataTextField = "Years_Number";
            CmbSelAñoCampaña.DataBind();
            CmbSelAñoCampaña.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void llenainformes()
        {
            try
            {
                DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CONSULTA_INFORMESYMESES_PLANNING", TxtPlanningReportes.Text, Convert.ToInt32(this.Session["company_id"]), this.Session["Planning_CodChannel"].ToString().Trim(), 0);
                RBtnListInformes.DataSource = ds.Tables[1];
                RBtnListInformes.DataValueField = "Report_id";
                RBtnListInformes.DataTextField = "Report_NameReport";
                RBtnListInformes.DataBind();
                ds = null;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenainformesProd()
        {
            try
            {
                DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CONSULTA_INFORMESYMESES_PLANNING", TxtPlanningAsigProd.Text, Convert.ToInt32(this.Session["company_id"]), this.Session["Planning_CodChannel"].ToString().Trim(), 0);
                RbtnListInfProd.DataSource = ds.Tables[1];
                RbtnListInfProd.DataValueField = "Report_id";
                RbtnListInfProd.DataTextField = "Report_NameReport";
                RbtnListInfProd.DataBind();
                ds = null;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenainformesPaneles()
        {
            DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CONSULTA_INFORMESYMESES_PLANNING", TxtCodPlanningPanel.Text, Convert.ToInt32(this.Session["company_id"]), this.Session["Planning_CodChannel"].ToString().Trim(), 0);
            RbtnListReportPanel.DataSource = ds.Tables[1];
            RbtnListReportPanel.DataValueField = "Report_id";
            RbtnListReportPanel.DataTextField = "Report_NameReport";
            RbtnListReportPanel.DataBind();
            ds = null;
        }
        private void llenaGrillaPDVPaneles()
        {
            DataTable dtpdvplanning = null;
            dtpdvplanning = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTARPDVPLANNING_PANELES", TxtCodPlanningPanel.Text, RbtnListReportPanel.SelectedItem.Value,ddlCiudad.SelectedValue,ddlMercado.SelectedValue);

            if (dtpdvplanning != null)
            {
                if (dtpdvplanning.Rows.Count > 0)
                {
                    GvPDVPaneles.DataSource = dtpdvplanning;
                    GvPDVPaneles.DataBind();
                    dtpdvplanning = null;

                    LblSelRapida.Visible = true;
                    TxtCodigoPDV.Visible = true;
                    TxtCodigoPDV.Text = "";
                    ImgSelRapida.Visible = true;
                }
                else
                {
                    GvPDVPaneles.DataSource = null;
                    GvPDVPaneles.DataBind();
                }
            }
        }
        private void Limpiar_InformacionBasica()
        {
            txtnumpla.Text = "";
            Rblisstatus.Items[0].Selected = true;
            Rblisstatus.Items[1].Selected = false;
            txtnamepresu.Text = "";
            txtcliente.Text = "";
            txtservice.Text = "";
            txt_FechaSolicitud.Text = "";
            txt_FechainiPre.Text = "";
            txt_FechainiPla.Text = "";
            txt_FechaEntrega.Text = "";
            txt_Fechafinpre.Text = "";
            txt_FechaPlafin.Text = "";
            TxtDuracion.Text = "";
            RbtnCanal.SelectedIndex = -1;
        }
        private void Limpiar_InformacionDescripcion()
        {
            TxtCodPlanningDesc.Text = "";
            CmbSelPresupuestoDesc.Text = "0";
            txtobj.Text = "";
            txtmanda.Text = "";
            Txtmeca.Text = "";
            txtcontacto.Text = "";
            txtarea.Text = "";
        }
        private void limpiar_InformacionResponsables()
        {
            CmbSelPresupuestoRes.Text = "0";
            TxtPlanningRes.Text = "";
            ChkSelEjecutivos.Items.Clear();
            ImgButtonAddSupervisores.Visible = false;
            ImgButtonAddMercaderistas.Visible = false;
            ChkListMercaderistas.Items.Clear();
            ChkListSupervisores.Items.Clear();
            BtnNoneOpe.Visible = false;
            BtnAllOpe.Visible = false;
            BtnNoneSup.Visible = false;
            BtnAllSup.Visible = false;
        }
        private void Limpiar_InformacionAsignacion()
        {
            TxtPlanningAsig.Text = "";
            CmbSelPresupuestoAsig.Text = "0";
            Lisboxsupervi.Items.Clear();
            LstBoxMercaderistas.Items.Clear();
            GvAsignados.DataBind();
            BtnSaveAsig.Enabled = false;
            //ImgAdverirSup.Visible = true;
            //LblAdvertirsup.Visible = true;
            //ImgOkSup.Visible = false;
            //LblOksup.Visible = false;

            //ImgAdverirOpe.Visible = true;
            //LblAdvertirOpe.Visible = true;
            //ImgOkOpe.Visible = false;
            //LblOkOpe.Visible = false;
        }
        private void limpiar_InformacionProductos()
        {
            rbliscatego.Items.Clear();
            Chklistcatego.Items.Clear();
            Chklistmarca.Items.Clear();
            rblmarca.Items.Clear();
            rblsubmarca.Items.Clear();
            ChkListFamilias.Items.Clear();
            ChkProductos.Items.Clear();
            submarcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
        }
        private void limpiar_InformacionPDVOPE()
        {
            TxtPlanningAsigPDVOPE.Text = "";
            CmbSelPresupuestoAsigPDVOPE.Text = "0";
            CmbSelOpePlanning.Items.Clear();
            TxtF_iniPDVOPE.Text = "";
            TxtF_finPDVOPE.Text = "";
            CmbSelCity.Items.Clear();
            CmbSelTipoAgrup.Items.Clear();
            CmbSelAgrup.Items.Clear();
            CmbSelOficina.Items.Clear();
            CmbSelMalla.Items.Clear();
            CmbSelSector.Items.Clear();
            ChkListPDV.Items.Clear();
            GvAsignaPDVOPE.DataBind();
        }
        private void limpiar_InformacionReportPlanning()
        {
            // limpiar controles 
            TxtPlanningReportes.Text = "";
            CmbSelPresupuestoReportes.Text = "0";
            RBtnListInformes.Items.Clear();
            CmbSelAñoCampaña.Items.Clear();
            RbtnListmeses.Items.Clear();
            //  ChkListMeses.Items.Clear();
            ChklstFrecuencia.Items.Clear();
            GVFrecuencias.DataBind();
        }
        private void Activar_InformacionBasica()
        {
            BtnSavePlanning.Enabled = true;
            txtnamepresu.Enabled = false;
            txtcliente.Enabled = false;
            txtservice.Enabled = false;
            txt_FechaSolicitud.Enabled = true;
            txt_FechainiPre.Enabled = true;
            txt_FechainiPla.Enabled = true;
            txt_FechaEntrega.Enabled = true;
            txt_Fechafinpre.Enabled = true;
            txt_FechaPlafin.Enabled = true;
            TxtDuracion.Enabled = true;
            RbtnCanal.Enabled = true;
            ImageButtonCal.Enabled = true;
            ImageButtonCal3.Enabled = true;
            ImageButtonCal5.Enabled = true;
            ImageButtonCal2.Enabled = true;
            ImageButtonCal4.Enabled = true;
            ImageButtonCal6.Enabled = true;
        }
        private void Inactivar_InformacionBasica()
        {
            BtnSavePlanning.Enabled = false;
            txtnamepresu.Enabled = false;
            txtcliente.Enabled = false;
            txtservice.Enabled = false;
            txt_FechaSolicitud.Enabled = false;
            txt_FechainiPre.Enabled = false;
            txt_FechainiPla.Enabled = false;
            txt_FechaEntrega.Enabled = false;
            txt_Fechafinpre.Enabled = false;
            txt_FechaPlafin.Enabled = false;
            TxtDuracion.Enabled = false;
            RbtnCanal.Enabled = false;
            ImageButtonCal.Enabled = false;
            ImageButtonCal3.Enabled = false;
            ImageButtonCal5.Enabled = false;
            ImageButtonCal2.Enabled = false;
            ImageButtonCal4.Enabled = false;
            ImageButtonCal6.Enabled = false;
        }
        private void Activar_AsignacionPDVOPE()
        {
            CmbSelOpePlanning.Enabled = true;
            TxtF_iniPDVOPE.Enabled = true;
            TxtF_finPDVOPE.Enabled = true;
            BtnCalF_iniPDVOPE.Enabled = true;
            BtnCalF_finPDVOPE.Enabled = true;
            CmbSelCity.Enabled = true;
            CmbSelTipoAgrup.Enabled = true;
            CmbSelAgrup.Enabled = true;
            CmbSelOficina.Enabled = true;
            CmbSelMalla.Enabled = true;
            CmbSelSector.Enabled = true;
            ChkListPDV.Enabled = true;
            BtnAsigPDVOPE.Enabled = true;

        }
        private void Inactivar_AsignacionPDVOPE()
        {
            CmbSelOpePlanning.Enabled = false;
            TxtF_iniPDVOPE.Enabled = false;
            TxtF_finPDVOPE.Enabled = false;
            BtnCalF_iniPDVOPE.Enabled = false;
            BtnCalF_finPDVOPE.Enabled = false;
            CmbSelCity.Enabled = false;
            CmbSelTipoAgrup.Enabled = false;
            CmbSelAgrup.Enabled = false;
            CmbSelOficina.Enabled = false;
            CmbSelMalla.Enabled = false;
            CmbSelSector.Enabled = false;
            ChkListPDV.Enabled = false;
            BtnAsigPDVOPE.Enabled = false;
        }

        private bool datoscompletosInformacionBasica()
        {
            if (cmbpresupuesto.Text == "0" || txt_FechaSolicitud.Text == "" ||
                txt_FechaEntrega.Text == "" || txt_FechainiPre.Text == "" || txt_Fechafinpre.Text == "" ||
                txt_FechainiPla.Text == "" || txt_FechaPlafin.Text == "" || TxtDuracion.Text == "" || RbtnCanal.SelectedIndex == -1)
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";

                if (RbtnCanal.SelectedIndex == -1)
                { this.Session["mensaje"] = "Debe seleccionar el Canal"; }
                if (TxtDuracion.Text == "")
                { this.Session["mensaje"] = "Debe llenar la Duración"; }
                if (txt_FechaPlafin.Text == "")
                { this.Session["mensaje"] = "Debe llenar fecha final de ejecución"; }
                if (txt_FechainiPla.Text == "")
                { this.Session["mensaje"] = "Debe llenar fecha inicial de ejecución"; }
                if (txt_Fechafinpre.Text == "")
                { this.Session["mensaje"] = "Debe llenar fecha final de preproducción"; }
                if (txt_FechainiPre.Text == "")
                { this.Session["mensaje"] = "Debe llenar fecha inicial de preproducción"; }
                if (txt_FechaEntrega.Text == "")
                { this.Session["mensaje"] = "Debe llenar fecha de entrega final"; }
                if (txt_FechaSolicitud.Text == "")
                { this.Session["mensaje"] = "Debe llenar fecha de solicitud"; }
                if (cmbpresupuesto.Text == "0")
                { this.Session["mensaje"] = "Debe seleccionar un presupuesto"; }

                Mensajes_Usuario();
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool datoscompletosDescripcionCampaña()
        {
            txtobj.Text = txtobj.Text.TrimStart();
            txtmanda.Text = txtmanda.Text.TrimStart();
            Txtmeca.Text = Txtmeca.Text.TrimStart();
            txtcontacto.Text = txtcontacto.Text.TrimStart();
            txtarea.Text = txtarea.Text.TrimStart();

            if (CmbSelPresupuestoDesc.SelectedValue == "0" || txtobj.Text == "" || txtmanda.Text == "" ||
               Txtmeca.Text == "" || txtcontacto.Text == "" || txtarea.Text == "")
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                if (txtarea.Text == "")
                { this.Session["mensaje"] = "Debe ingresar el área involucrada"; }
                if (txtcontacto.Text == "")
                { this.Session["mensaje"] = "Debe ingresar el nombre del contacto"; }
                if (Txtmeca.Text == "")
                { this.Session["mensaje"] = "Debe ingresar la mecanica de la campaña"; }
                if (txtmanda.Text == "")
                { this.Session["mensaje"] = "Debe ingresar los mandatorios de la campaña"; }
                if (txtobj.Text == "")
                { this.Session["mensaje"] = "Debe ingresar el objetivo de la campaña"; }
                if (CmbSelPresupuestoDesc.SelectedValue == "0")
                { this.Session["mensaje"] = "Debe seleccionar un presupuesto"; }
                Mensajes_Usuario();
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool datoscompletosResponsablesCampaña()
        {
            if (CmbSelPresupuestoRes.SelectedValue.Equals("0") || (ChkSelEjecutivos.SelectedIndex == -1 &&
                ChkListSupervisores.SelectedIndex == -1 && ChkListMercaderistas.SelectedIndex == -1))
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                if (ChkListMercaderistas.SelectedIndex == -1 && ChkListSupervisores.SelectedIndex == -1 &&
                    ChkSelEjecutivos.SelectedIndex == -1)
                { this.Session["mensaje"] = "Debe seleccionar Ejecutivo , Supervisor y/o Mercaderista para continuar"; }
                if (CmbSelPresupuestoRes.SelectedValue == "0")
                { this.Session["mensaje"] = "Debe seleccionar un presupuesto"; }
                Mensajes_Usuario();
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool datoscompletosProductos()
        {
            bool sigue = true;
            if (CmbSelPresupuestoAsigProd.SelectedValue.Equals("0"))
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Debe seleccionar un presupuesto";
                Mensajes_Usuario();
                sigue = false;
            }
            if (sigue)
            {
                string vista = this.Session["vista_final"].ToString().Trim();

                if (vista.Equals("Producto"))
                {
                    if (ChkProductos.SelectedIndex == -1)
                    {
                        this.Session["mensaje"] = "Debe seleccionar por lo menos un producto";
                        sigue = false;
                    }
                }

                if (vista.Equals("Marca"))
                {
                    if (Chklistmarca.SelectedIndex == -1)
                    {
                        this.Session["mensaje"] = "Debe seleccionar por lo menos una marca";
                        sigue = false;
                    }
                }

                if (vista.Equals("Familia"))
                {
                    if (ChkListFamilias.SelectedIndex == -1)
                    {
                        this.Session["mensaje"] = "Debe seleccionar por lo menos una familia";
                        sigue = false;
                    }
                }

                // agregamos la verificación de subfamilias
                if (vista.Equals("Subfamilia"))
                {
                    if (Chklistcatego.SelectedIndex == -1)
                    {
                        this.Session["mensaje"] = "Debe seleccionar por lo menos una SubFamilia";
                        sigue = false;
                    }
                }

                if (vista.Equals("Categoria"))
                {
                    if (Chklistcatego.SelectedIndex == -1)
                    {
                        this.Session["mensaje"] = "Debe seleccionar por lo menos una Categoría";
                        sigue = false;
                    }
                }
            }
            if (sigue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // valiaciones de fecha 
        private bool Validad_FechaSolicitud()
        {
            //@FechaPA
            bool validar = true;
            try
            {
                fechaSolicitudP = DateTime.Parse(txt_FechaSolicitud.Text);
                fechaFinalP = DateTime.Parse(txt_FechaEntrega.Text);
                fechaIniPreP = DateTime.Parse(txt_FechainiPre.Text);
                fechaIniPlaP = DateTime.Parse(txt_FechainiPla.Text);
                
                if (fechaSolicitudP > fechaIniPreP || fechaSolicitudP > fechaIniPlaP || fechaSolicitudP > fechaFinalP)
                {
                    validar = false;
                    if (fechaSolicitudP > fechaIniPreP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de solicitud de la campaña  no puede ser mayor a la fecha de inicio de Preproducción";
                        Mensajes_Usuario();
                    }
                    if (fechaSolicitudP > fechaIniPlaP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La Fecha de solicitud de la campaña  no puede ser mayor a la fecha de inicio de ejecución";
                        Mensajes_Usuario();

                    }
                    else if (fechaSolicitudP > fechaFinalP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de solicitud de la campaña  no puede ser mayor a la fecha de entrega final";
                        Mensajes_Usuario();
                    }
                }

                return validar;
            }
            catch
            {
                validar = false;
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
                Mensajes_Usuario();
                return validar;
            }
        }

        private bool Validar_Fecha_Entrega_Final()
        {
            //FechaPa
            bool valido = true;
            try
            {
                fechaSolicitudP = DateTime.Parse(txt_FechaSolicitud.Text);
                fechaFinalP = DateTime.Parse(txt_FechaEntrega.Text);
                fechaIniPreP = DateTime.Parse(txt_FechainiPre.Text);
                fechaFinPreP = DateTime.Parse(txt_Fechafinpre.Text);
                fechaIniPlaP = DateTime.Parse(txt_FechainiPla.Text);
                fechaPlaFinP = DateTime.Parse(txt_FechaPlafin.Text);
                //if (fechaFinalP < DateTime.Today)
                //{
                //    valido = false;
                //    this.Session["encabemensa"] = "Señor Usuario";
                //    this.Session["cssclass"] = "MensajesSupervisor";
                //    this.Session["mensaje"] = "La fecha de entrega final no puede ser menor que la actual";
                //    Mensajes_AsignacionPresupuesto();
                //}               
                if (fechaFinalP < fechaSolicitudP)
                {
                    valido = false;
                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "La fecha de entrega final no pude ser menor a la fecha de solicitud";
                    Mensajes_Usuario();

                }
                else if (fechaFinalP < fechaIniPreP)
                {
                    valido = false;
                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";

                    this.Session["mensaje"] = "La fecha de entrega final no pude ser menor a la fecha de inicio de Preproducción";
                    Mensajes_Usuario();

                }
                else
                    if (fechaFinalP < fechaFinPreP)
                    {
                        valido = false;
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de entrega final no pude ser menor  a la fecha de fin de Preproducción";
                        Mensajes_Usuario();

                    }
                    else if (fechaFinalP < fechaIniPlaP)
                    {
                        valido = false;
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de entrega final no pude ser menor a la fecha de inicio de ejecución";
                        Mensajes_Usuario();
                    }
                    else
                        if (fechaFinalP < fechaPlaFinP)
                        {
                            valido = false;
                            this.Session["encabemensa"] = "Señor Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";

                            this.Session["mensaje"] = "La fecha de entrega final no pude ser menor a la fecha de fin de ejecución";
                            Mensajes_Usuario();
                        }
                return valido;
            }
            catch
            {
                valido = false;
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
                Mensajes_Usuario();
                return valido;
            }
        }
        private bool Validad_Fecha_InicioPreproducción()
        {
            bool sigue = true;
            try
            {
                fechaFinalP = DateTime.Parse(txt_FechaEntrega.Text);
                fechaIniPreP = DateTime.Parse(txt_FechainiPre.Text);
                fechaFinPreP = DateTime.Parse(txt_Fechafinpre.Text);
                fechaIniPlaP = DateTime.Parse(txt_FechainiPla.Text);

                if (fechaIniPreP > fechaFinPreP || fechaIniPreP > fechaIniPlaP || fechaIniPreP > fechaFinalP)
                {
                    //if (fechaIniPreP < DateTime.Today)
                    //{
                    //    this.Session["encabemensa"] = "Señor Usuario";
                    //    this.Session["cssclass"] = "MensajesSupervisor";

                    //    this.Session["mensaje"] = "La fecha de inicio de Preproducción no puede ser menor de  la fecha actual";
                    //    Mensajes_AsignacionPresupuesto();
                    //    return false;
                    //}
                    if (fechaIniPreP > fechaFinPreP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La fecha de inicio de Preproducción no puede ser mayor que la fecha de fin de Preproducción";
                        Mensajes_Usuario();
                        return false;
                    }

                    if (fechaIniPreP > fechaIniPlaP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La Fecha de inicio de Preproducción no puede ser mayor que la fecha de inicio del Plannning";
                        Mensajes_Usuario();
                        return false;

                    }
                    if (fechaIniPreP > fechaFinalP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de inicio de Preproducción no puede ser mayor que la fecha de entrega final";
                        Mensajes_Usuario();
                        return false;
                    }
                }
                return sigue;
            }
            catch
            {
                sigue = false;
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
                Mensajes_Usuario();
                return sigue;
            }
        }
        private bool Validad_Fecha_finPreproducción()
        {
            //FechaPa          
            try
            {
                fechaSolicitudP = DateTime.Parse(txt_FechaSolicitud.Text);
                fechaFinalP = DateTime.Parse(txt_FechaEntrega.Text);
                fechaIniPre = DateTime.Parse(txt_FechainiPre.Text);
                fechaFinPreP = DateTime.Parse(txt_Fechafinpre.Text);
                fechaIniPlaP = DateTime.Parse(txt_FechainiPla.Text);
                fechaPlaFinP = DateTime.Parse(txt_FechaPlafin.Text);
                if (fechaFinPreP < fechaSolicitudP || fechaFinPreP > fechaIniPlaP || fechaFinPreP > fechaPlaFinP || fechaFinPreP > fechaFinalP)
                {
                    //if (fechaIniPre < DateTime.Today)
                    //{
                    //    this.Session["encabemensa"] = "Señor Usuario";
                    //    this.Session["cssclass"] = "MensajesSupervisor";

                    //    this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser menor de  la fecha actual";
                    //    Mensajes_AsignacionPresupuesto();
                    //    return false;
                    //}
                    if (fechaFinPreP < fechaSolicitudP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser mayor que la fecha de solicitud";
                        Mensajes_Usuario();
                        return false;
                    }

                    if (fechaFinPreP > fechaIniPlaP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser mayor que la fecha de inicio del Plannning";
                        Mensajes_Usuario();
                        return false;

                    }
                    if (fechaFinPreP > fechaFinalP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser mayor quela fecha de entrega final";
                        Mensajes_Usuario();
                        return false;

                    }

                    if (fechaFinPreP > fechaPlaFinP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser mayor quela fecha fin de ejecución";
                        Mensajes_Usuario();
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
                Mensajes_Usuario();
                return false;
            }
        }
        private bool Validar_fechas()
        {
            //FechaPA
            try
            {
                fechaIniPlaP = DateTime.Parse(txt_FechainiPla.Text);
                fechaPlaFinP = DateTime.Parse(txt_FechaPlafin.Text);
                if (fechaIniPlaP > fechaPlaFinP)
                {

                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";

                    //Francisco Martinez: Se cambia el mensaje - 25/03/2010
                    //this.Session["mensaje"] = this.Session["mensaje"] + " " + "La Fecha de Inicio de Ejecucion no puede ser mayor y/o Igual que la de Finalización";
                    this.Session["mensaje"] = "La fecha de inicio de ejecución no puede ser mayor que la de finalización";
                    Mensajes_Usuario();
                    return false;
                }
                return true;
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
                Mensajes_Usuario();
                return false;
            }
        }
        private bool Validar_fechas_Menor()
        {
            //FechaPA
            try
            {
                fechaIniPlaP = DateTime.Parse(txt_FechainiPla.Text);
                fechaPlaFinP = DateTime.Parse(txt_FechaPlafin.Text);
                if (fechaPlaFinP < fechaIniPlaP)
                {
                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "La fecha de fin de la campaña no puede ser menor que la de inicio";
                    Mensajes_Usuario();
                    return false;
                }
                //if (fechaIniPlaP < DateTime.Today)
                //{
                //    this.Session["encabemensa"] = "Señor Usuario";
                //    this.Session["cssclass"] = "MensajesSupervisor";
                //    this.Session["mensaje"] = "La fecha de inicio de ejecución no puede ser menor que la actual";
                //    Mensajes_AsignacionPresupuesto();
                //    return false;
                //}
                //if (fechaPlaFinP < DateTime.Today)
                //{
                //    this.Session["encabemensa"] = "Señor Usuario";
                //    this.Session["cssclass"] = "MensajesSupervisor";
                //    this.Session["mensaje"] = "La fecha fin de ejecución no puede ser menor que la actual";
                //    Mensajes_AsignacionPresupuesto();
                //    return false;
                //}
                return true;
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
                Mensajes_Usuario();
                return false;
            }
        }

        private void Mensajes_Usuario()
        {
            PCanal.CssClass = this.Session["cssclass"].ToString();
            lblencabezado.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneral.Text = this.Session["mensaje"].ToString();
            ModalPopupCanal.Show();
        }
        private void Mensajes_UsuarioValidacionVistas()
        {
            PMensajeVista.CssClass = this.Session["cssclass"].ToString();
            LblTitMensajeVista.Text = this.Session["encabemensa"].ToString();
            LblMsjMensajeVista.Text = this.Session["mensaje"].ToString();
            ModalMensajeVista.Show();
        } 
        #endregion
                
        protected void ImgCloseSession_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Get_Administrativo.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                this.Session.Abandon();
                Response.Redirect("~/login.aspx", true);
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/login.aspx", true);
            }
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Modulos/Planning/Menu_Planning.aspx", true);
        }

        #region Menú vertical
        protected void BtnAsignaPresupuesto_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            Limpiar_InformacionBasica();
            PanelASignaPresupuesto.Style.Value = "Display:Block;";
            StiloBotonSeleccionado();
            BtnAsignaPresupuesto.CssClass = "buttonNewPlanSel";
            Habilitabotones();
            BtnAsignaPresupuesto.Enabled = false;
            LlenaPresupuestos();
        }

        protected void BtnDescripcionCampaña_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            Limpiar_InformacionDescripcion();
            PanelDescCampaña.Style.Value = "Display:Block;";
            StiloBotonSeleccionado();
            BtnDescripcionCampaña.CssClass = "buttonNewPlanSel";
            Habilitabotones();
            BtnDescripcionCampaña.Enabled = false;
            LlenaPresupuestosAsignados();
        }

        protected void BtnResponsables_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            limpiar_InformacionResponsables();
            PanelResponsablesCampaña.Style.Value = "Display:Block;";
            StiloBotonSeleccionado();
            BtnResponsables.CssClass = "buttonNewPlanSel";
            Habilitabotones();
            BtnResponsables.Enabled = false;
            LlenaPresupuestosAsignados();
        }

        protected void BtnAsignaPersonal_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            Limpiar_InformacionAsignacion();
            PanelAsignaPersonal.Style.Value = "Display:Block;";
            StiloBotonSeleccionado();
            BtnAsignaPersonal.CssClass = "buttonNewPlanSel";
            Habilitabotones();
            BtnAsignaPersonal.Enabled = false;
            LlenaPresupuestosAsignados();
        }
        protected void BtnPDV_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            PanelAsignaPDV.Style.Value = "Display:Block;";
            StiloBotonSeleccionado();
            BtnPDV.CssClass = "buttonNewPlanSel";
            Habilitabotones();
            BtnPDV.Enabled = false;
            ifcarga.Attributes["src"] = "carga_PDV.aspx";
            LlenaPresupuestosAsignados();
        }
        protected void BtnPaneles_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            PanelPanelesPlanning.Style.Value = "Display:Block;";
            StiloBotonSeleccionado();
            BtnPaneles.CssClass = "buttonNewPlanSel";
            Habilitabotones();
            BtnPaneles.Enabled = false;
            LlenaPresupuestosAsignados();
            
        }
        protected void BtnProductos_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            limpiar_InformacionProductos();
            PanelAsignaProductos.Style.Value = "Display:Block;";
            StiloBotonSeleccionado();
            BtnProductos.CssClass = "buttonNewPlanSel";
            Habilitabotones();
            BtnProductos.Enabled = false;
            LlenaPresupuestosAsignados();
        }
        protected void BtnAsignaPDVaOpe_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            limpiar_InformacionPDVOPE();
            PanelAsignacionPDVaoper.Style.Value = "Display:Block;";
            StiloBotonSeleccionado();
            BtnAsignaPDVaOpe.CssClass = "buttonNewPlanSel";
            Habilitabotones();
            BtnAsignaPDVaOpe.Enabled = false;
            BtnSaveAsigPDVOPE.Enabled = false;
            BtnClearAsigPDVOPE.Enabled = false;
            BtnCargaPDVOPE.Enabled = false;
            Inactivar_AsignacionPDVOPE();
            LlenaPresupuestosAsignados();
        }
        protected void BtnReportes_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            limpiar_InformacionReportPlanning();
            PanelReportesCampaña.Style.Value = "Display:Block;";
            StiloBotonSeleccionado();
            BtnReportes.CssClass = "buttonNewPlanSel";
            Habilitabotones();
            BtnReportes.Enabled = false;
            LlenaPresupuestosAsignados();
        }

        #endregion

        #region Asignar presupuesto
        protected void cmbpresupuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Limpiar_InformacionBasica();
            if (!cmbpresupuesto.SelectedValue.Equals("0"))
            {
                DataTable dtnamePresupuesto = Presupuesto.Get_NamePlanning(cmbpresupuesto.SelectedValue);
                txtnamepresu.Text = dtnamePresupuesto.Rows[0]["namepreu"].ToString().ToUpperInvariant();
                txtnamepresu.ToolTip = dtnamePresupuesto.Rows[0]["namepreu"].ToString().ToUpperInvariant();
                dtnamePresupuesto = null;

                //se llena el control con el nombre del cliente asociado al presupuesto seleccionado - Ing. Mauricio Ortiz
                DataTable dtCliente = Presupuesto.Get_ObtenerClientes(cmbpresupuesto.SelectedValue, 1);
                this.Session["company_id"] = dtCliente.Rows[0]["Company_id"].ToString().Trim();
                LlenaCanales();
                txtcliente.Text = dtCliente.Rows[0]["Company_Name"].ToString().ToUpperInvariant();
                txtcliente.ToolTip = dtCliente.Rows[0]["Company_Name"].ToString().ToUpperInvariant();
                dtCliente = null;

                //se llena el control con el nombre del servicio asociado al Presupuesto
                DataTable dtservicio = menu.Menu(cmbpresupuesto.SelectedValue);
                this.Session["cod_strategy"] = dtservicio.Rows[0]["cod_Strategy"].ToString().Trim();
                txtservice.Text = dtservicio.Rows[0]["Strategy_Name"].ToString().ToUpperInvariant();
                txtservice.ToolTip = dtservicio.Rows[0]["Strategy_Name"].ToString().ToUpperInvariant();
                dtservicio = null;

                //se llena controles de fecha de ejecución (inicio y fin) las cuales llegan por interface con EasyWin)
                DataTable dtfechas = Presupuesto.Get_OtenerFechasPlanning(cmbpresupuesto.SelectedValue, 1);
                txt_FechainiPla.Text = dtfechas.Rows[0]["Fec_iniPlanning"].ToString().Trim();
                txt_FechaPlafin.Text = dtfechas.Rows[0]["Fec_FinPlanning"].ToString().Trim();
                txt_FechaSolicitud.Text = Convert.ToString(DateTime.Today.AddDays(-1));
                txt_FechaEntrega.Text = Convert.ToString(Convert.ToDateTime(txt_FechaPlafin.Text).AddDays(1));
                txt_FechainiPre.Text = Convert.ToString(DateTime.Today);
                txt_Fechafinpre.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPla.Text).AddDays(-1));
                txt_FechaEntrega.Text = Convert.ToString(Convert.ToDateTime(txt_FechaPlafin.Text).AddDays(1));
                dtfechas = null;
                Activar_InformacionBasica();
            }
            else
            {
                RbtnCanal.Items.Clear();
                Inactivar_InformacionBasica();
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario seleccionar un presupuesto";
                Mensajes_Usuario();
            }
        }
        protected void txt_FechaSolicitud_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_FechaSolicitud.Text != "__/__/____" && txt_FechaSolicitud.Text != "")
                {
                    DateTime t = Convert.ToDateTime(txt_FechaSolicitud.Text);
                }
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                txt_FechaSolicitud.Text = "";
                Mensajes_Usuario();
            }
        }
        protected void txt_FechaEntrega_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_FechaEntrega.Text != "__/__/____" && txt_FechaEntrega.Text != "")
                {
                    DateTime t = Convert.ToDateTime(txt_FechaEntrega.Text);
                }
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                txt_FechaEntrega.Text = "";
                Mensajes_Usuario();
            }

        }
        protected void txt_FechainiPre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_FechainiPre.Text != "__/__/____" && txt_FechainiPre.Text != "")
                {
                    DateTime t = Convert.ToDateTime(txt_FechainiPre.Text);
                    txt_Fechafinpre.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPre.Text));
                    txt_FechaSolicitud.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPre.Text).AddDays(-1));
                }
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                txt_FechainiPre.Text = "";
                Mensajes_Usuario();
            }
        }
        protected void txt_Fechafinpre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_Fechafinpre.Text != "__/__/____" && txt_Fechafinpre.Text != "")
                {
                    DateTime t = Convert.ToDateTime(txt_Fechafinpre.Text);
                    txt_FechainiPla.Text = Convert.ToString(Convert.ToDateTime(txt_Fechafinpre.Text).AddDays(1));
                }
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                txt_Fechafinpre.Text = "";
                Mensajes_Usuario();
            }
        }
        protected void txt_FechainiPla_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_FechainiPla.Text != "__/__/____" && txt_FechainiPla.Text != "")
                {
                    DateTime t = Convert.ToDateTime(txt_FechainiPla.Text);
                    txt_FechaSolicitud.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPla.Text).AddDays(-2));
                    txt_FechainiPre.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPla.Text).AddDays(-1));
                    txt_Fechafinpre.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPla.Text).AddDays(-1));
                    if (txt_FechaPlafin.Text != "__/__/____" && txt_FechaPlafin.Text != "")
                    {
                        txt_FechaSolicitud.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPla.Text).AddDays(-2));
                        txt_FechainiPre.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPla.Text).AddDays(-1));
                        txt_Fechafinpre.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPla.Text).AddDays(-1));
                        txt_FechaEntrega.Text = Convert.ToString(Convert.ToDateTime(txt_FechaPlafin.Text).AddDays(1));                        
                    }
                }
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                txt_FechainiPla.Text = "";
                Mensajes_Usuario();
            }
        }
        protected void txt_FechaPlafin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_FechaPlafin.Text != "__/__/____" && txt_FechaPlafin.Text != "")
                {
                    DateTime t = Convert.ToDateTime(txt_FechaPlafin.Text);
                    txt_Fechafinpre.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPla.Text).AddDays(-1));
                    txt_FechaEntrega.Text = Convert.ToString(Convert.ToDateTime(txt_FechaPlafin.Text).AddDays(1));
                }
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                txt_FechaPlafin.Text = "";
                Mensajes_Usuario();
            }
        }
        protected void BtnSavePlanning_Click(object sender, EventArgs e)
        {
            Boolean Continuar = datoscompletosInformacionBasica();
            if (Continuar)
            {
                //@FechaPA - Francisco Martinez - 26/03/2010
                //Se modifica la logica para habilitar o deshabilitar el canal.
                Boolean sigue = Validad_FechaSolicitud();

                if (sigue)
                {
                    sigue = Validar_Fecha_Entrega_Final();

                    if (sigue)
                    {
                        sigue = Validad_Fecha_InicioPreproducción();

                        if (sigue)
                        {
                            sigue = Validad_Fecha_finPreproducción();

                            if (sigue)
                            {
                                sigue = Validar_fechas();

                                if (sigue)
                                {
                                    sigue = Validar_fechas_Menor();

                                    if (sigue)
                                    {
                                        int icod_Strategy = Convert.ToInt32(this.Session["cod_strategy"]);

                                        // ejecutar metodo para insertar registro del planning . Ing. Mauricio Ortiz
                                        // 30/07/2010 se adiciona id_planning concatenando número de presupuesto y fecha actual. Ing. Mauricio Ortiz  
                                        wsPlanning.Save_Planning(cmbpresupuesto.SelectedValue + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year, txtnamepresu.Text, icod_Strategy, RbtnCanal.SelectedValue,
                                        Convert.ToDateTime(txt_FechainiPla.Text), Convert.ToDateTime(txt_FechaPlafin.Text), Convert.ToDateTime(txt_FechaSolicitud.Text), Convert.ToDateTime(txt_FechainiPre.Text),
                                        Convert.ToDateTime(txt_Fechafinpre.Text), TxtDuracion.Text, Convert.ToDateTime(txt_FechaEntrega.Text), TxtDuracion.Text, cmbpresupuesto.SelectedValue,
                                        true, 1, this.Session["sUser"].ToString().Trim(),
                                        DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                                        //Ejecutar método para actualizar el número de planning generado en presupuesto (Budget) 
                                        wsPlanning.obtenerPlanning(cmbpresupuesto.SelectedValue);

                                        //metodo para hacer el save en base de datos DB_LUCKY_TMP
                                        //Ing. Mauricio Ortiz 14/02/2011
                                        wsPlanning.Save_PlanningDB_LUCKY_TMP(cmbpresupuesto.SelectedValue + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year, txtnamepresu.Text, RbtnCanal.SelectedValue);




                                        //ejecutar método para obtener id del planning generado 
                                        DataTable dt = wsPlanning.ObtenerIdPlanning(cmbpresupuesto.SelectedValue);
                                        this.Session["presupuesto"] = cmbpresupuesto.SelectedValue;
                                        string splanning = (dt.Rows[0]["Planning"].ToString().Trim());
                                        txtnumpla.Text = dt.Rows[0]["Planning"].ToString().Trim();
                                        this.Session["encabemensa"] = "Sr. Usuario";
                                        this.Session["cssclass"] = "MensajesSupConfirm";
                                        this.Session["mensaje"] = "La campaña : " + txtnamepresu.Text.ToUpper() + " Se ha creado con éxito";
                                        Mensajes_Usuario();
                                        LlenaPresupuestos();
                                        LlenaPresupuestosAsignados();
                                        Limpiar_InformacionBasica();

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void btncancelcara_Click(object sender, EventArgs e)
        {
            cmbpresupuesto.SelectedValue = "0";
            Limpiar_InformacionBasica();
            Inactivar_InformacionBasica();
        }
        #endregion

        #region Descripción de la campaña
        protected void CmbSelPresupuestoDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbSelPresupuestoDesc.SelectedValue != "0")
            {
                //ejecutar método para obtener id del planning generado 
                DataTable dt = wsPlanning.ObtenerIdPlanning(CmbSelPresupuestoDesc.SelectedValue);
                TxtCodPlanningDesc.Text = dt.Rows[0]["Planning"].ToString().Trim();

                DataSet ds = new DataSet();
                ds = wsPlanning.Get_PlanningCreados(dt.Rows[0]["Planning"].ToString().Trim());
                if (ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0 && ds.Tables[3].Rows.Count > 0)
                {
                    BtnSaveDescCampaña.Enabled = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Ya existe descripción para la campaña : " + CmbSelPresupuestoDesc.SelectedItem.Text.ToUpper();
                    Mensajes_Usuario();
                    Limpiar_InformacionDescripcion();
                }
                else
                {
                    BtnSaveDescCampaña.Enabled = true;
                }
            }
            else
            {
                TxtCodPlanningDesc.Text = "";
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario seleccionar un presupuesto";
                Mensajes_Usuario();
            }
        }
        protected void BtnSaveDescCampaña_Click(object sender, EventArgs e)
        {
            Boolean Continuar = datoscompletosDescripcionCampaña();
            if (Continuar)
            {
                //Ejecutar Método para almacenar los objetivos de la Campaña. Ing. Mauricio Ortiz
                wsPlanning.Get_RegisterObjPlanning(TxtCodPlanningDesc.Text, txtobj.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                //Ejecutar Método para almacenar los Mandatorios de la Campaña. Ing. Mauricio Ortiz
                wsPlanning.Get_RegisterMandatoryPlanning(TxtCodPlanningDesc.Text, txtmanda.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                //Ejecutar Método para almacenar la Mecanica de la Actividad. Ing. Mauricio Ortiz
                wsPlanning.Get_RegisterMecanicaPlanning(TxtCodPlanningDesc.Text, Txtmeca.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                //Ejecutar Método para actualizar contacto del planning y area involucrada del planning
                wsPlanning.ActualizaContactoyarea(TxtCodPlanningDesc.Text, txtarea.Text, txtcontacto.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now);


                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupConfirm";
                this.Session["mensaje"] = "Se ha creado con éxito la descripción para la campaña : " + CmbSelPresupuestoDesc.SelectedItem.Text.ToUpper();
                Mensajes_Usuario();

                Limpiar_InformacionDescripcion();

            }
        }
        protected void BtnClearDescrip_Click(object sender, EventArgs e)
        {
            Limpiar_InformacionDescripcion();
        }
        #endregion

        #region Responsables
        protected void CmbSelPresupuestoRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbSelPresupuestoRes.SelectedValue != "0")
            {
                //ejecutar método para obtener id del planning generado 
                DataTable dt = wsPlanning.ObtenerIdPlanning(CmbSelPresupuestoRes.SelectedValue);
                TxtPlanningRes.Text = dt.Rows[0]["Planning"].ToString().Trim();
                this.Session["company_id"] = dt.Rows[0]["Company_id"].ToString().Trim();
                ImgButtonAddSupervisores.Visible = true;
                ImgButtonAddMercaderistas.Visible = true;
                //ejecutar método para obtener los supervisores y mercadersitas. Guardado en ViewState.
                DataSet dsPersonal = wsPlanning.ObtenerPersonal(TxtPlanningRes.Text);
                this.ViewState["ds_Personal"] = dsPersonal;
                if (dsPersonal != null)
                {
                    if (dsPersonal.Tables[0].Rows.Count > 0)
                    {
                        ChkListSupervisores.DataSource = dsPersonal.Tables[0];
                        ChkListSupervisores.DataTextField = "name_user";
                        ChkListSupervisores.DataValueField = "Person_id";
                        ChkListSupervisores.DataBind();
                    }
                    else
                    {
                        ChkListSupervisores.DataSource = dsPersonal.Tables[1];
                        ChkListSupervisores.DataTextField = "name_user";
                        ChkListSupervisores.DataValueField = "Person_id";
                        ChkListSupervisores.DataBind();
                    }
                    if (dsPersonal.Tables[2].Rows.Count > 0)
                    {
                        ChkListMercaderistas.DataSource = dsPersonal.Tables[2];
                        ChkListMercaderistas.DataTextField = "name_user";
                        ChkListMercaderistas.DataValueField = "Person_id";
                        ChkListMercaderistas.DataBind();
                    }
                    else
                    {
                        ChkListMercaderistas.DataSource = dsPersonal.Tables[3];
                        ChkListMercaderistas.DataTextField = "name_user";
                        ChkListMercaderistas.DataValueField = "Person_id";
                        ChkListMercaderistas.DataBind();
                    }
                }
                llenaEjecutivos();
                llenaNuevosSupervisores();
                llenaNuevosMercaderistas();
                BtnNoneOpe.Visible = true;
                BtnAllOpe.Visible = true;
                BtnNoneSup.Visible = true;
                BtnAllSup.Visible = true;
                dsPersonal = null;
                BtnSaveRespons.Enabled = true;

                if (ChkListMercaderistas.Items.Count <= 0)
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "No existen mercaderistas disponibles con un perfil apropiado para la campaña: " + CmbSelPresupuestoRes.SelectedItem.Text + ".";
                    Mensajes_Usuario();
                    return;
                }
                //07/09/2010 [Ing. Mauricio Ortiz se comentarea codigo para permitir seguir ingresando responsables 
                //            al planning seleccionado]
                //DataSet ds = new DataSet();
                //ds = wsPlanning.Get_PlanningCreados(dt.Rows[0]["Planning"].ToString().Trim());
                //if (ds.Tables[4].Rows.Count > 0)
                //{
                //    BtnSaveRespons.Enabled = false;
                //    this.Session["encabemensa"] = "Sr. Usuario";
                //    this.Session["cssclass"] = "MensajesSupervisor";
                //    this.Session["mensaje"] = "Ya existe Responsables para la campaña : " + CmbSelPresupuestoRes.SelectedItem.Text.ToUpper();
                //    Mensajes_Usuario();
                //    limpiar_InformacionResponsables();                   
                //}
                //else
                //{
                //   BtnSaveRespons.Enabled = true;                    
                //}              
            }
            else
            {
                limpiar_InformacionResponsables();
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario seleccionar un presupuesto.";
                Mensajes_Usuario();
            }
        }
        protected void BtnAllSup_Click(object sender, EventArgs e)
        {
            foreach(ListItem supervisor in ChkListSupervisores.Items)
                supervisor.Selected = true;
        }
        protected void BtnNoneSup_Click(object sender, EventArgs e)
        {
            ChkListSupervisores.ClearSelection();
        }
        protected void BtnAllOpe_Click(object sender, EventArgs e)
        {
            foreach(ListItem mercaderista in ChkListMercaderistas.Items)  
                mercaderista.Selected = true; 
        }
        protected void BtnNoneOpe_Click(object sender, EventArgs e)
        {
            ChkListMercaderistas.ClearSelection();
        }
        protected void btnAddSupervisores_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= LstNewSupervisor.Items.Count - 1; i++)
            {
                if (LstNewSupervisor.Items[i].Selected == true)
                {

                    bool continuar = true;
                    for (j = 0; j <= ChkListSupervisores.Items.Count - 1; j++)
                    {
                        if (ChkListSupervisores.Items[j].Value == LstNewSupervisor.Items[i].Value)
                        {
                            continuar = false;
                            j = ChkListSupervisores.Items.Count - 1;
                        }
                    }
                    if (continuar)
                    {
                        ChkListSupervisores.Items.Insert(0, new ListItem(LstNewSupervisor.Items[i].Text, LstNewSupervisor.Items[i].Value));
                    }
                }
            }
            LstNewSupervisor.SelectedIndex = -1;

        }
        protected void btnAddMercaderistas_Click(object sender, EventArgs e)
        {

            for (int i = 0; i <= LstNewMercaderista.Items.Count - 1; i++)
            {
                if (LstNewMercaderista.Items[i].Selected == true)
                {
                    bool continuar = true;
                    for (j = 0; j <= ChkListMercaderistas.Items.Count - 1; j++)
                    {
                        if (ChkListMercaderistas.Items[j].Value == LstNewMercaderista.Items[i].Value)
                        {
                            continuar = false;
                            j = ChkListMercaderistas.Items.Count - 1;
                        }
                    }
                    if (continuar)
                    {
                        ChkListMercaderistas.Items.Insert(0, new ListItem(LstNewMercaderista.Items[i].Text, LstNewMercaderista.Items[i].Value));
                    }
                }
            }
            LstNewMercaderista.SelectedIndex = -1;
        }
        protected void BtnclosePanel2_Click(object sender, ImageClickEventArgs e)
        {
            LstNewMercaderista.SelectedIndex = -1;
        }
        protected void BtnclosePanel1_Click(object sender, ImageClickEventArgs e)
        {
            LstNewSupervisor.SelectedIndex = -1;
        }
        protected void BtnSaveRespons_Click(object sender, EventArgs e)
        {
            Boolean Continuar = datoscompletosResponsablesCampaña();
            if (Continuar)
            {
                // regsitrar el ejecutivo de cuenta
                DAplicacion odDuplicado = new DAplicacion();
                //ListItemCollection listaejecutivos = new ListItemCollection();

                //Registra ejecutivos
                foreach (ListItem ejecutivo in ChkSelEjecutivos.Items)
                {
                    if (ejecutivo.Selected)
                    {
                        DataTable dtDuplicadoEjecutivo = odDuplicado.ConsultaDuplicados(ConfigurationManager.AppSettings["Staff_Planning"], TxtPlanningRes.Text, ejecutivo.Value, null);
                        if (dtDuplicadoEjecutivo == null)
                            Staff_Planning.RegistrarPersonal(TxtPlanningRes.Text, Convert.ToInt32(ejecutivo.Value), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                    }
                }

                // registrar supervisores seleccionados
                foreach (ListItem supervisor in ChkListSupervisores.Items)
                {
                    if (supervisor.Selected)
                    {
                        DataTable dtDuplicadoSupervisor = odDuplicado.ConsultaDuplicados(ConfigurationManager.AppSettings["Staff_Planning"], TxtPlanningRes.Text, supervisor.Value, null);
                        if (dtDuplicadoSupervisor == null)
                            Staff_Planning.RegistrarPersonal(TxtPlanningRes.Text, Convert.ToInt32(supervisor.Value), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                    }
                }
                // registrar mercaderistas seleccionados
                foreach (ListItem mercaderista in ChkListMercaderistas.Items)
                {
                    if (mercaderista.Selected)
                    {
                        DataTable dtDuplicadoMercaderista = odDuplicado.ConsultaDuplicados(ConfigurationManager.AppSettings["Staff_Planning"], TxtPlanningRes.Text, mercaderista.Value, null);
                        if (dtDuplicadoMercaderista == null)
                        {
                            Staff_Planning.RegistrarPersonal(TxtPlanningRes.Text, Convert.ToInt32(mercaderista.Value), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                            try
                            {
                                Staff_Planning.RegistrarTBL_PERFIL(Convert.ToInt32(mercaderista.Value), TxtPlanningRes.Text, this.Session["company_id"].ToString().Trim());
            
                            }
                            catch (Exception ex)
                            {
                                // el planning no esta registrado en la base de datos intermedia por lo cual no insertará el perfil en tbl_perfil
                            }
                        }
                    }
                }
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupConfirm";
                this.Session["mensaje"] = "Se ha creado con éxito los Responsables de la campaña : " + CmbSelPresupuestoRes.SelectedItem.Text.ToUpper();
                Mensajes_Usuario();
                limpiar_InformacionResponsables();
            }
        }
        protected void BtnClearRespons_Click(object sender, EventArgs e)
        {
            limpiar_InformacionResponsables();
        }
        #endregion

        #region Asignacion de personal
        protected void CmbSelPresupuestoAsig_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtAsigna_OP_A_SUPERV_Temp = new DataTable();
            dtAsigna_OP_A_SUPERV_Temp.Columns.Add("Cod_Supervisor", typeof(Int32));
            dtAsigna_OP_A_SUPERV_Temp.Columns.Add("Nombre_Supervisor", typeof(String));
            dtAsigna_OP_A_SUPERV_Temp.Columns.Add("Cod_Mercaderista", typeof(String));
            dtAsigna_OP_A_SUPERV_Temp.Columns.Add("Nombre_Mercaderista", typeof(String));
            GvAsignados.DataSource = dtAsigna_OP_A_SUPERV_Temp;
            GvAsignados.DataBind();
            this.Session["dtAsigna_OP_A_SUPERV_Temp"] = dtAsigna_OP_A_SUPERV_Temp;

            if (!CmbSelPresupuestoAsig.SelectedValue.Equals("0"))
            {
                //ejecutar método para obtener id del planning generado 
                DataTable dt = wsPlanning.ObtenerIdPlanning(CmbSelPresupuestoAsig.SelectedValue);
                TxtPlanningAsig.Text = dt.Rows[0]["Planning"].ToString().Trim();
                llenaStaffplanning();
                if (sigue)
                {
                    DataSet ds = new DataSet();
                    ds = wsPlanning.Get_PlanningCreados(dt.Rows[0]["Planning"].ToString().Trim());
                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        for (int i = 0; i <= LstBoxMercaderistas.Items.Count - 1; i++)
                        {
                            DataTable dtverificacion = wsPlanning.Get_VerficaAsignaOperativo(TxtPlanningAsig.Text, Convert.ToInt32(LstBoxMercaderistas.Items[i].Value));
                            if (dtverificacion.Rows.Count > 0)
                            {
                                LstBoxMercaderistas.Items.Remove(LstBoxMercaderistas.Items[i]);
                                i--;
                            }

                        }
                        if (LstBoxMercaderistas.Items.Count <= 0)
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "Actualmente no hay Mercaderistas por asignar a Supervisor en la campaña " + CmbSelPresupuestoAsig.SelectedItem.Text.ToUpper();
                            Mensajes_Usuario();
                            Limpiar_InformacionAsignacion();
                        }
                        //07/09/2010 [Ing. Mauricio Ortiz se comentarea codigo para permitir seguir ingresando asignacion de personal 
                        //            al planning seleccionado]
                        //BtnSaveAsig.Enabled = false;
                        //this.Session["encabemensa"] = "Sr. Usuario";
                        //this.Session["cssclass"] = "MensajesSupervisor";
                        //this.Session["mensaje"] = "Ya existe Asignación de Personal para la campaña : " + CmbSelPresupuestoAsig.SelectedItem.Text.ToUpper();
                        //Mensajes_Usuario();
                        //Limpiar_InformacionAsignacion();
                    }   
                }
                else
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es indispensable crear primero los Responsables (Supervisores y Mercaderistas) para la campaña: " + CmbSelPresupuestoAsig.SelectedItem.Text.ToUpper();
                    Mensajes_Usuario();
                    BtnSaveAsig.Enabled = false;
                    Limpiar_InformacionAsignacion();
                }
            }
            else
            {
                BtnSaveAsig.Enabled = false;
                Limpiar_InformacionAsignacion();
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario seleccionar un presupuesto";
                Mensajes_Usuario();
            }
        }
        protected void BtnMasAsing_Click(object sender, EventArgs e)
        {
            DataTable dtAsigna_OP_A_SUPERV_Temp = (DataTable)this.Session["dtAsigna_OP_A_SUPERV_Temp"];

            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupervisor";

            if (Lisboxsupervi.SelectedValue == "")
            {
                this.Session["mensaje"] = "Debe Seleccionar un Supervisor";
                Mensajes_Usuario();
                return;
            }

            if (LstBoxMercaderistas.SelectedValue == "")
            {
                this.Session["mensaje"] = "Debe Seleccionar por lo menos un Operativo";
                Mensajes_Usuario();
                return;
            }

            try
            {
                for (int i = 0; i <= LstBoxMercaderistas.Items.Count - 1; i++)
                {
                    if (LstBoxMercaderistas.Items[i].Selected)
                    {
                        DataRow dr = dtAsigna_OP_A_SUPERV_Temp.NewRow();
                        dr["Cod_Supervisor"] = Convert.ToInt32(Lisboxsupervi.SelectedItem.Value);
                        dr["Nombre_Supervisor"] = Lisboxsupervi.SelectedItem.Text;
                        dr["Cod_Mercaderista"] = LstBoxMercaderistas.Items[i].Value;
                        dr["Nombre_Mercaderista"] = LstBoxMercaderistas.Items[i].Text;
                        dtAsigna_OP_A_SUPERV_Temp.Rows.Add(dr);
                        this.Session["dtAsigna_OP_A_SUPERV_Temp"] = dtAsigna_OP_A_SUPERV_Temp;
                    }
                }

                GvAsignados.DataSource = dtAsigna_OP_A_SUPERV_Temp;
                GvAsignados.DataBind();

                Lisboxsupervi.SelectedItem.Selected = false;


                for (int i = 0; i <= LstBoxMercaderistas.Items.Count - 1; i++)
                {
                    if (LstBoxMercaderistas.Items[i].Selected)
                    {
                        LstBoxMercaderistas.Items.Remove(LstBoxMercaderistas.Items[i]);
                        i--;
                    }
                }


                for (int i = 0; i <= Lisboxsupervi.Items.Count - 1; i++)
                {
                    for (int k = 0; k <= GvAsignados.PageCount - 1; k++)
                    {
                        for (int j = 0; j <= GvAsignados.Rows.Count - 1; j++)
                        {
                            if (Lisboxsupervi.Items[i].Value == GvAsignados.Rows[j].Cells[1].Text)
                            {
                                j = GvAsignados.Rows.Count;
                                asignacionsup = true;
                            }
                            else
                            {
                                asignacionsup = false;
                            }
                        }
                        if (asignacionsup == false)
                        {
                            GvAsignados.PageIndex = k + 1;
                            GvAsignados.DataSource = dtAsigna_OP_A_SUPERV_Temp;
                            GvAsignados.DataBind();
                        }
                        else
                        {
                            k = GvAsignados.PageCount;
                        }
                    }
                    if (asignacionsup == false)
                    {
                        i = Lisboxsupervi.Items.Count;
                    }
                    GvAsignados.PageIndex = 0;
                    GvAsignados.DataSource = dtAsigna_OP_A_SUPERV_Temp;
                    GvAsignados.DataBind();
                }

                //if (asignacionsup == true)
                //{
                //    ImgAdverirSup.Visible = false;
                //    LblAdvertirsup.Visible = false;
                //    ImgOkSup.Visible = true;
                //    LblOksup.Visible = true;
                //}
                //else
                //{
                //    ImgAdverirSup.Visible = true;
                //    LblAdvertirsup.Visible = true;
                //    ImgOkSup.Visible = false;
                //    LblOksup.Visible = false;
                //}

                //if (LstBoxMercaderistas.Items.Count == 0)
                //{
                //    ImgAdverirOpe.Visible = false;
                //    LblAdvertirOpe.Visible = false;
                //    ImgOkOpe.Visible = true;
                //    LblOkOpe.Visible = true;
                //}
                //else
                //{
                //    ImgAdverirOpe.Visible = true;
                //    LblAdvertirOpe.Visible = true;
                //    ImgOkOpe.Visible = false;
                //    LblOkOpe.Visible = false;
                //}

                //if (ImgOkOpe.Visible == true && ImgOkSup.Visible == true)
                //{
                //    BtnSaveAsig.Enabled = true;                    
                //}
                //else
                //{
                //    BtnSaveAsig.Enabled = false;                    
                //}
                if (GvAsignados.Rows.Count > 0)
                {
                    BtnSaveAsig.Enabled = true;
                }
                else
                {
                    BtnSaveAsig.Enabled = false;
                }


            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        protected void GvAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListItem newList = new ListItem();
            newList.Value = GvAsignados.SelectedRow.Cells[3].Text.ToString().Trim();
            newList.Text = GvAsignados.SelectedRow.Cells[4].Text.ToString().Trim();

            newList.Text = newList.Text.Replace("&#193;", "á");
            newList.Text = newList.Text.Replace("&#201;", "é");
            newList.Text = newList.Text.Replace("&#205;", "í");
            newList.Text = newList.Text.Replace("&#211;", "ó");
            newList.Text = newList.Text.Replace("&#218;", "ú");
            newList.Text = newList.Text.Replace("&#225;", "á");
            newList.Text = newList.Text.Replace("&#233;", "é");
            newList.Text = newList.Text.Replace("&#237;", "í");
            newList.Text = newList.Text.Replace("&#243;", "ó");
            newList.Text = newList.Text.Replace("&#250;", "ú");
            newList.Text = newList.Text.Replace("&#209;", "ñ");
            newList.Text = newList.Text.Replace("&#241;", "ñ");

            newList.Selected = false;
            LstBoxMercaderistas.Items.Add(newList);

            try
            {
                GridViewRow row = GvAsignados.SelectedRow;
                DataTable dtdel = (DataTable)this.Session["dtAsigna_OP_A_SUPERV_Temp"];
                dtdel.Rows[row.RowIndex].Delete();
                this.Session["dtAsigna_OP_A_SUPERV_Temp"] = dtdel;
                GvAsignados.DataSource = (DataTable)this.Session["dtAsigna_OP_A_SUPERV_Temp"];
                GvAsignados.DataBind();

                asignacionsup = false;

                for (int i = 0; i <= Lisboxsupervi.Items.Count - 1; i++)
                {
                    for (int k = 0; k <= GvAsignados.PageCount - 1; k++)
                    {
                        for (int j = 0; j <= GvAsignados.Rows.Count - 1; j++)
                        {
                            if (Lisboxsupervi.Items[i].Value == GvAsignados.Rows[j].Cells[1].Text)
                            {
                                j = GvAsignados.Rows.Count;
                                asignacionsup = true;
                            }
                            else
                            {
                                asignacionsup = false;
                            }
                        }
                        if (asignacionsup == false)
                        {
                            GvAsignados.PageIndex = k + 1;
                            GvAsignados.DataSource = dtdel;
                            GvAsignados.DataBind();
                        }
                        else
                        {
                            k = GvAsignados.PageCount;
                        }
                    }
                    if (asignacionsup == false)
                    {
                        i = Lisboxsupervi.Items.Count;
                    }
                    GvAsignados.PageIndex = 0;
                    GvAsignados.DataSource = dtdel;
                    GvAsignados.DataBind();
                }

                //if (asignacionsup == true)
                //{
                //    ImgAdverirSup.Visible = false;
                //    LblAdvertirsup.Visible = false;
                //    ImgOkSup.Visible = true;
                //    LblOksup.Visible = true;
                //}
                //else
                //{
                //    ImgAdverirSup.Visible = true;
                //    LblAdvertirsup.Visible = true;
                //    ImgOkSup.Visible = false;
                //    LblOksup.Visible = false;
                //}

                //if (LstBoxMercaderistas.Items.Count == 0)
                //{
                //    ImgAdverirOpe.Visible = false;
                //    LblAdvertirOpe.Visible = false;
                //    ImgOkOpe.Visible = true;
                //    LblOkOpe.Visible = true;
                //}
                //else
                //{
                //    ImgAdverirOpe.Visible = true;
                //    LblAdvertirOpe.Visible = true;
                //    ImgOkOpe.Visible = false;
                //    LblOkOpe.Visible = false;
                //}
                //if (ImgOkOpe.Visible == true && ImgOkSup.Visible == true)
                //{
                //    BtnSaveAsig.Enabled = true;         
                //}
                //else
                //{
                //    BtnSaveAsig.Enabled = false;         
                //}
                if (GvAsignados.Rows.Count > 0)
                {
                    BtnSaveAsig.Enabled = true;
                }
                else
                {
                    BtnSaveAsig.Enabled = false;
                }
            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

        }
        protected void BtnSaveAsig_Click(object sender, EventArgs e)
        {
            Conexion cn = new Conexion(2);
            //Ejecutar Método para almancenar la asignación de mercadersitas a supervisores. Ing. Mauricio Ortiz
            for (int i = 0; i <= GvAsignados.Rows.Count - 1; i++)
            {
                wsPlanning.Get_Register_OperativosxSupervisor(TxtPlanningAsig.Text, Convert.ToInt32(GvAsignados.Rows[i].Cells[1].Text), Convert.ToInt32(GvAsignados.Rows[i].Cells[3].Text), true, this.Session["sUser"].ToString(), DateTime.Now, this.Session["sUser"].ToString(), DateTime.Now);

            }




            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupConfirm";
            this.Session["mensaje"] = "Se ha creado con éxito la asignación de operativos a supervisores de la campaña : " + CmbSelPresupuestoAsig.SelectedItem.Text.ToUpper();
            Mensajes_Usuario();
            Limpiar_InformacionAsignacion();
        }
        protected void BtnClearAsig_Click(object sender, EventArgs e)
        {

            Limpiar_InformacionAsignacion();
        }
        #endregion

        #region Puntos de Venta
        // El evento del boton BtnPDV  BtnPDV_Click hace el response redirect a la pagina que gestiona esta parte
        #endregion

        #region Paneles
        protected void CmbSelPresupuestoPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbSelPresupuestoPanel.SelectedValue != "0")
            {
                //ejecutar método para obtener id del planning generado 
                DataTable dt = wsPlanning.ObtenerIdPlanning(CmbSelPresupuestoPanel.SelectedValue);
                TxtCodPlanningPanel.Text = dt.Rows[0]["Planning"].ToString().Trim();
                this.Session["company_id"] = dt.Rows[0]["Company_id"].ToString().Trim();
                this.Session["Planning_CodChannel"] = dt.Rows[0]["Planning_CodChannel"].ToString().Trim();
                llenainformesPaneles();
                if (this.Session["company_id"].ToString() == "1561")
                {
                    llenaTipoPanel();
                }

                GvPDVPaneles.Visible = true;
                BtnSavePanel.Enabled = true;
                BtnClearPanel.Enabled = true;

            }
            else
            {
                RbtnListReportPanel.Items.Clear();
                TxtCodPlanningPanel.Text = "";
                GvPDVPaneles.Visible = false;
                GvPDVPaneles.DataBind();
                BtnSavePanel.Enabled = false;
                BtnClearPanel.Enabled = false;
                LblSelRapida.Visible = false;
                TxtCodigoPDV.Visible = false;
                TxtCodigoPDV.Text = "";
                ImgSelRapida.Visible = false;
            }
        }
        protected void RbtnListReportPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaNodeComercial();
            llenaCiudad();
            llenaGrillaPDVPaneles();
            parametros.Visible = true;
            

        }
        protected void ddlCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaGrillaPDVPaneles();
        }
        protected void ddlMercado_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaGrillaPDVPaneles();
        }

        void llenaTipoPanel()
        {
            Conexion cn = new Conexion();

            DataTable dt = cn.ejecutarDataTable("LLENA_TIPO_PANEL");

            ddlTipoPanel.DataSource = dt;
            ddlTipoPanel.DataTextField = "nombre";
            ddlTipoPanel.DataValueField = "id";
            ddlTipoPanel.DataBind();
            ddlTipoPanel.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            dt = null;
            colgate.Visible = true;
        }
        void llenaCiudad()
        {
            Conexion cn = new Conexion();


            DataTable dt = cn.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTARCITY_PANELES", this.Session["company_id"].ToString(), TxtCodPlanningPanel.Text);

            ddlCiudad.DataSource = dt;
            ddlCiudad.DataTextField = "Name_City";
            ddlCiudad.DataValueField = "cod_City";
            ddlCiudad.DataBind();
            ddlCiudad.Items.Insert(0, new ListItem("---Todas---", "0"));
            dt = null;
            
        }
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        void llenaNodeComercial()
        {
            Conexion cn = new Conexion();

            Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(TxtCodPlanningPanel.Text);

            if (oListNodeComercial.Length > 0)
            {
                ddlMercado.Enabled = true;
                ddlMercado.DataSource = oListNodeComercial;
                ddlMercado.DataTextField = "commercialNodeName";
                ddlMercado.DataValueField = "NodeCommercial";
                ddlMercado.DataBind();
                ddlMercado.Items.Insert(0, new ListItem("---Todas---", "0"));
            }
        }

        protected void btnguardarPDV_Click(object sender, EventArgs e)
        {
            Conexion cn = new Conexion();

          

            try
            {

                if (ddlperiodo2.SelectedValue == "" || ddlPeriodo.SelectedValue == "")
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Seleccione un periodo";
                    Mensajes_Usuario();
                    return;
                }

                DataTable dtDuplicado = cn.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAR_PANELES_PLANNING", TxtCodPlanningPanel.Text, Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value), Convert.ToInt32(ddlPeriodo.SelectedValue));


                if (dtDuplicado.Rows.Count == 0)
                {

                    DataTable dt = cn.ejecutarDataTable("INSERTARPANELESANTERIORES", TxtCodPlanningPanel.Text,
                                 RbtnListReportPanel.SelectedItem.Value, ddlperiodo2.SelectedValue, ddlPeriodo.SelectedValue,
                                 this.Session["sUser"].ToString().Trim(), DateTime.Now,
                                 this.Session["sUser"].ToString().Trim(), DateTime.Now);

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Los puntos de ventas fueron Copiados dos exito";
                    Mensajes_Usuario();
                }
                else
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "El punto de venta  ya esta asignado para el panel del reporte " + RbtnListReportPanel.SelectedItem.Text;
                    Mensajes_Usuario();
                    return;
                }
            }
            catch
            {
                
            }


        }

        protected void RbtnSelTipoCarga_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (RbtnSelTipoCarga.Items[0].Selected)
            {
                copiarPDV.Visible = true;
                copiarPDVG.Visible = true;
                BtnSavePanel.Enabled = false;
            }
            if (RbtnSelTipoCarga.Items[1].Selected)
            {
                copiarPDV.Visible = false;
                copiarPDVG.Visible = false;
                BtnSavePanel.Enabled = true;
            }
        }


        protected void BtnSavePanel_Click(object sender, EventArgs e)
        {
            Conexion cn = new Conexion();
            bool sigue = true;
            int numreg = 0;
            DAplicacion odDuplicado = new DAplicacion();
            if (RbtnListReportPanel.SelectedIndex == -1)
            {
                sigue = false;
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario seleccionar un reporte";
                Mensajes_Usuario();
            }

            if (sigue)
            {
                for (int i = 0; i <= GvPDVPaneles.Rows.Count - 1; i++)
                {
                    if (((CheckBox)GvPDVPaneles.Rows[i].Cells[0].FindControl("CheckBox1")).Checked == true)
                    {
                        DataTable dtDuplicado = cn.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAR_PANELES_PLANNING", TxtCodPlanningPanel.Text, Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value),Convert.ToInt32(ddlPeriodo.SelectedValue));
                        //DataTable dtDuplicado = odDuplicado.ConsultaDuplicados(ConfigurationManager.AppSettings["PLA_Panel_Planning"], TxtCodPlanningPanel.Text,
                        //          ((Label)GvPDVPaneles.Rows[i].Cells[0].FindControl("LblNo")).Text,
                        //          RbtnListReportPanel.SelectedItem.Value);
                        if (dtDuplicado.Rows.Count == 0)
                        {
                            sigue = true;
                        }
                        else
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "El punto de venta : " +
                                ((Label)GvPDVPaneles.Rows[i].Cells[0].FindControl("Labelnompdv")).Text +
                                " ya esta asignado para el panel del reporte " + RbtnListReportPanel.SelectedItem.Text;
                            Mensajes_Usuario();
                            sigue = false;
                            i = GvPDVPaneles.Rows.Count - 1;
                        }
                    }
                }
            }
            if (sigue)
            {
                // invoca método para guardar en PLA_Panel_Planning
                for (int i = 0; i <= GvPDVPaneles.Rows.Count - 1; i++)
                {
                    if (((CheckBox)GvPDVPaneles.Rows[i].Cells[0].FindControl("CheckBox1")).Checked == true)
                    {

                        if (this.Session["company_id"].ToString() == "1561")
                        {
                            cn.ejecutarDataTable("UP_WEBXPLORA_PLA_REGISTRAR_PLA_Panel_Planning", TxtCodPlanningPanel.Text,
                           Convert.ToInt32(((Label)GvPDVPaneles.Rows[i].Cells[0].FindControl("LblNo")).Text),
                           ((Label)GvPDVPaneles.Rows[i].Cells[0].FindControl("Labelcodpdv")).Text,
                           Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value), true,
                           this.Session["sUser"].ToString().Trim(), DateTime.Now,
                           this.Session["sUser"].ToString().Trim(), DateTime.Now, Convert.ToInt32(ddlPeriodo.SelectedValue), Convert.ToInt32(ddlTipoPanel.SelectedValue));
                        }
                        else
                        {
                            cn.ejecutarDataTable("UP_WEBXPLORA_PLA_REGISTRAR_PLA_Panel_Planning", TxtCodPlanningPanel.Text,
                                Convert.ToInt32(((Label)GvPDVPaneles.Rows[i].Cells[0].FindControl("LblNo")).Text),
                                ((Label)GvPDVPaneles.Rows[i].Cells[0].FindControl("Labelcodpdv")).Text,
                                Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value), true,
                                this.Session["sUser"].ToString().Trim(), DateTime.Now,
                                this.Session["sUser"].ToString().Trim(), DateTime.Now, Convert.ToInt32(ddlPeriodo.SelectedValue), null);
                        }

                        //wsPlanning.Registrar_PLA_Panel_Planning(TxtCodPlanningPanel.Text,
                        //    Convert.ToInt32(((Label)GvPDVPaneles.Rows[i].Cells[0].FindControl("LblNo")).Text),
                        //    ((Label)GvPDVPaneles.Rows[i].Cells[0].FindControl("Labelcodpdv")).Text,
                        //    Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value), true,
                        //    this.Session["sUser"].ToString().Trim(), DateTime.Now,
                        //    this.Session["sUser"].ToString().Trim(), DateTime.Now);

                        ((CheckBox)GvPDVPaneles.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = false;
                        numreg = numreg + 1;
                    }
                }
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupConfirm";
                this.Session["mensaje"] = "Se ha configurado correctamente (" + numreg + ") puntos de venta para el panel del reporte " + RbtnListReportPanel.SelectedItem.Text;
                Mensajes_Usuario();
                RbtnListReportPanel.SelectedIndex = -1;
                GvPDVPaneles.DataBind();


            }
        }

        protected void BtnClearPanel_Click(object sender, EventArgs e)
        {
            RbtnListReportPanel.Items.Clear();
            TxtCodPlanningPanel.Text = "";
            CmbSelPresupuestoPanel.Text = "0";
            GvPDVPaneles.Visible = false;
            GvPDVPaneles.DataBind();
            BtnSavePanel.Enabled = false;
            BtnClearPanel.Enabled = false;
            ddlPeriodo.Items.Clear();
            ddlperiodo2.Items.Clear();
            parametros.Visible = false;

        }
        protected void ImgSelRapida_Click(object sender, ImageClickEventArgs e)
        {
            bool sigue = false;
            for (int i = 0; i <= GvPDVPaneles.Rows.Count - 1; i++)
            {
                if (((Label)GvPDVPaneles.Rows[i].FindControl("Labelcodpdv")).Text == TxtCodigoPDV.Text)
                {
                    ((CheckBox)GvPDVPaneles.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = true;
                    i = GvPDVPaneles.Rows.Count - 1;
                    TxtCodigoPDV.Text = "";
                    sigue = true;
                }
            }
            if (sigue)
            {
                Lblmsj.ForeColor = System.Drawing.Color.Blue;
                Lblmsj.Text = "Registro marcado !";
            }
            else
            {
                Lblmsj.ForeColor = System.Drawing.Color.Red;
                Lblmsj.Text = "Registro no encontrado !";
            }
        }

        public void llenarPeriodos(string planning, int report, string año, string mes)
        {

           // string a = Session["idPlanning"].ToString();
            //string b = Session["id_report"].ToString();
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_OBTENERPERIODOS_xPlanningReporteAñoMes", planning, report, año, mes);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlPeriodo.DataSource = dt;
                    ddlPeriodo.DataValueField = "id_ReportsPlanning";
                    ddlPeriodo.DataTextField = "Perido";
                    ddlPeriodo.DataBind();
                }
                else
                {
                    ddlPeriodo.Items.Clear();
                }
            }
       
            dt = null;

        }
        public void llenarPeriodos2(string planning, int report, string año, string mes)
        {

            // string a = Session["idPlanning"].ToString();
            //string b = Session["id_report"].ToString();
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_OBTENERPERIODOS_xPlanningReporteAñoMes", planning, report, año, mes);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlperiodo2.DataSource = dt;
                    ddlperiodo2.DataValueField = "id_ReportsPlanning";
                    ddlperiodo2.DataTextField = "Perido";
                    ddlperiodo2.DataBind();
                }
                else
                {
                    ddlperiodo2.Items.Clear();
                }
            }

            dt = null;

        }
        private void Años(DropDownList ddl)
        {

            DataTable dty = null;
            dty = Get_Administrativo.Get_ObtenerYears();
            if (dty.Rows.Count > 0)
            {
                ddl.DataSource = dty;
                ddl.DataValueField = "Years_Number";
                ddl.DataTextField = "Years_Number";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Selecione--", "0"));
            }

        }
        protected void ddlmes_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarPeriodos(TxtCodPlanningPanel.Text, Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value),ddlAño.SelectedValue,ddlmes.SelectedValue);
        }
        protected void ddlmes2_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarPeriodos2(TxtCodPlanningPanel.Text, Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value),ddlAño2.SelectedValue,ddlmes2.SelectedValue);
        }
        

        #endregion

        #region Levantamiento de información
        protected void CmbSelPresupuestoAsigProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LblReporteAsociado.Visible = false;
                LblReporteAsociado.Text = "";
                divselproductos.Style.Value = "display:none;";
                div_masopciones.Style.Value = "display:none;";
                StiloBotonTipoProdSeleccionado();

                if (CmbSelPresupuestoAsigProd.SelectedValue != "0")
                {
                    if (this.Session["companyid"].ToString() == "1609")
                    {
                        btnCargaPrecio.Visible = true;
                    }
                    if (this.Session["companyid"].ToString() == "1561")
                    {
                        btnPanelMaterialPOP.Visible = true;
                    }



                    ModalPanelreporteproducto.Show();
                    
                    //ejecutar método para obtener id del planning generado 
                    DataTable dt = wsPlanning.ObtenerIdPlanning(CmbSelPresupuestoAsigProd.SelectedValue);
                    TxtPlanningAsigProd.Text = dt.Rows[0]["Planning"].ToString().Trim();
                    this.Session["company_id"] = dt.Rows[0]["Company_id"].ToString().Trim();
                    this.Session["Planning_CodChannel"] = dt.Rows[0]["Planning_CodChannel"].ToString().Trim();
                    this.Session["id_planningProductos"] = TxtPlanningAsigProd.Text;
                    this.Session["PresupuestoProductos"] = CmbSelPresupuestoAsigProd.SelectedItem.Text;
                    Session["idPlanning"] = TxtPlanningAsigProd.Text;
                    llenainformesProd();
                    llenacompetidores();

                    BtnProdCompe.Enabled = true;
                    BtnProdPropio.Enabled = true;                    
                    CmbCompetidores.Visible = false;
                    LblSelCompetidor.Visible = false;
                    BtnSaveProd.Enabled = true;
                    BtnClearProd.Enabled = true;
                }
                else
                {                    
                    TxtPlanningAsigProd.Text = "";
                    BtnProdCompe.Enabled = false;
                    BtnProdPropio.Enabled = false;                    
                    CmbCompetidores.Visible = false;
                    LblSelCompetidor.Visible = false;
                    BtnSaveProd.Enabled = false;
                    BtnClearProd.Enabled = false;
                }
                limpiarlistas();
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        //Angel Ortiz -  Se agrega metodo para limpiar listas de clasficicacion de productos.
        private void limpiarlistas() 
        {
            rbliscatego.Items.Clear();
            Chklistcatego.Items.Clear();
            rblmarca.Items.Clear();
            Chklistmarca.Items.Clear();
            rblsubmarca.Items.Clear();
            Chklistmarca.Items.Clear();
            rblfamilia.Items.Clear();
            ChkListFamilias.Items.Clear();
            rblsubfamilia.Items.Clear();
            ChkListSubFamilias.Items.Clear();
            ChkProductos.Items.Clear();
        }

        protected void BtnSelInfProd_Click(object sender, EventArgs e)
        {
            try
            {
                BtnProdCompe.Enabled = true;
                BtnProdPropio.Enabled = true;
                CmbCompetidores.Visible = false;
                LblSelCompetidor.Visible = false;
                BtnSaveProd.Enabled = false;
                BtnClearProd.Enabled = true;

                llenacompetidores();
                StiloBotonTipoProdSeleccionado();
                RbtMasopciones.Items.Clear();
                limpiarlistas();

                LblReporteAsociado.Visible = true;
                LblReporteAsociado.Text = "Reporte " + RbtnListInfProd.SelectedItem.Text;
                this.Session["RbtnListInfProd"] = RbtnListInfProd.SelectedItem.Value;        
        
                int id_company = Convert.ToInt32(this.Session["company_id"].ToString().Trim());

                string cod_channel = this.Session["Planning_CodChannel"].ToString().Trim();
                int id_report = Convert.ToInt32(RbtnListInfProd.SelectedItem.Value);

                Session["id_report"] = id_report;

                DPlanning dplanning = new DPlanning();
                DataTable vistas = dplanning.ValidaTipoGestion(id_company, cod_channel, id_report); //obtiene los valores booleanos para las vistas de paneles
                this.Session["vistas"] = vistas;
                DataTable tipo_reporte = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_TIPO_REPORTE_LISTAR", id_company, id_report); //obtiene lista de subreportes en caso las tenga

                if (tipo_reporte.Rows.Count > 0) //si tiene subreportes
                {
                    divselproductos.Style.Value = "display:none;";
                    div_masopciones.Style.Value = "display:block;";
                    RbtMasopciones.DataSource = vistas;
                    RbtMasopciones.DataTextField = "TipoReporte_Descripcion";
                    RbtMasopciones.DataValueField = "id_Tipo_Reporte";
                    RbtMasopciones.DataBind();
                }
                else // si no tiene sub_reportes
                {
                    opcionesvistareporte();
                }
            }
            catch (Exception ex)
            {
                ModalPanelreporteproducto.Show();
                divselproductos.Style.Value = "display:none;";
            }
        }

        // Angel Ortiz - Activa la visibilidad de los paneles y los botones de acuerdo 
        // al tipo de levantamiento(categorias, marcas, familias, etc).
        private void opcionesvistareporte()
        {            
            DataTable vistas = ((DataTable)this.Session["vistas"]);
            string rep = this.Session["RbtnListInfProd"].ToString();
            string tipo_rep = RbtMasopciones.SelectedValue;
            this.Session["tipo_rep"] = tipo_rep;
            
            int row = 0;
            if (!tipo_rep.Equals(""))
            {
                if (rep.Equals("23"))//FOTOGRAFICO
                {
                    if (RbtMasopciones.Text.Equals("02"))//tipo exhib. visib.
                        row = 1;
                }
                else if (rep.Equals("57"))//FOTOGRAFICO
                {
                    if (RbtMasopciones.Text.Equals("02"))//tipo exhib. visib.
                        row = 1;
                }
                else if (rep.Equals("58"))//PRESENCIA
                {
                    if (RbtMasopciones.Text.Equals("04"))//tipo exhib. visib.
                        row = 1;
                    if (RbtMasopciones.Text.Equals("05"))//tipo exhib. visib.
                        row = 2;
                    if (RbtMasopciones.Text.Equals("06"))//tipo exhib. visib.
                        row = 3;
                    if (RbtMasopciones.Text.Equals("07"))//tipo exhib. visib.
                        row = 4;
                    if (RbtMasopciones.Text.Equals("08"))//tipo exhib. visib.
                        row = 5;
                }
            }

            int id_company = Convert.ToInt32(this.Session["company_id"].ToString().Trim());

            if (tipo_rep == "03" && rep == "58" && id_company==1561)//elementos de visibilidad....Carlos Marin
            {

                div_Elementos.Style.Value = "display:block;";
                div_masopciones.Style.Value = "display:none;";


                Conexion cn = new Conexion();
                DataTable dt =cn.ejecutarDataTable("PLA_LISTAR_MPointOfPurchaseXTipoMaterial");


                    BtnSaveProd.Enabled = true;
                    chklist.DataSource = dt;
                    chklist.DataValueField = "id_MPointOfPurchase";
                    chklist.DataTextField = "POP_name";
                    chklist.DataBind();
                    rbliscatego.Items.Clear();
                
            }

            else if (tipo_rep == "04" && rep == "58" && id_company == 1561)//Pres.Colgate......Carlos Marin
            {
                string vista_final = "";

                vista_final = "Producto";

                        //carga array booleano para activar las vistas
                       
                            bool[] s_vistas = new bool[6];

                            s_vistas[0] = true;
                            s_vistas[1] = false;
                            s_vistas[2] = false;
                            s_vistas[3] = false;
                            s_vistas[4] = false;
                            s_vistas[5] = true;

                            this.Session["s_vistas"] = s_vistas;
                        

                        this.Session["vista_final"] = "Producto";

                        divselproductos.Style.Value = "display:block;";
                        div_masopciones.Style.Value = "display:none;";
                        preparacontroles(vista_final);
                    
                  
                
    
            }

            else if (tipo_rep == "05" && rep == "58" && id_company == 1561)//Pres.Competencia......Carlos Marin
            {
                string vista_final = "";

                vista_final = "Producto";

                //carga array booleano para activar las vistas

                bool[] s_vistas = new bool[6];

                s_vistas[0] = true;
                s_vistas[1] = false;
                s_vistas[2] = false;
                s_vistas[3] = false;
                s_vistas[4] = false;
                s_vistas[5] = true;

                this.Session["s_vistas"] = s_vistas;


                this.Session["vista_final"] = "Producto";

                divselproductos.Style.Value = "display:block;";
                div_masopciones.Style.Value = "display:none;";
                preparacontroles(vista_final);




            }
            //else if (tipo_rep == "05" && rep == "58" && id_company == 1561)//Pres.Competencia.....Carlos Marin
            //{

            //} 
            else if (tipo_rep == "06" && rep == "58" && id_company == 1561)//Pres.Competencia.....Carlos Marin
            {


                string vista_final = "";

                vista_final = "Producto";

                //carga array booleano para activar las vistas

                bool[] s_vistas = new bool[6];

                s_vistas[0] = false;
                s_vistas[1] = false;
                s_vistas[2] = false;
                s_vistas[3] = false;
                s_vistas[4] = false;
                s_vistas[5] = true;

                this.Session["s_vistas"] = s_vistas;


                this.Session["vista_final"] = "Producto";

                divselproductos.Style.Value = "display:block;";
                div_masopciones.Style.Value = "display:none;";
                preparacontroles(vista_final);
            }
            else if (tipo_rep == "07" && rep == "58" && id_company == 1561)
            {
                div_Observaciones.Style.Value = "display:block;";
                div_masopciones.Style.Value = "display:none;";


                Conexion cn = new Conexion();
                DataTable dt = cn.ejecutarDataTable("PLA_LISTAR_OBSERVACIONES");


                chklist_Observaciones.DataSource = dt;
                chklist_Observaciones.DataValueField = "id_Observaciones";
                chklist_Observaciones.DataTextField = "nombre_Observaciones";
                chklist_Observaciones.DataBind();





            }
            else
            {

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

                        divselproductos.Style.Value = "display:block;";
                        div_masopciones.Style.Value = "display:none;";
                        preparacontroles(vista_final);
                    }
                    else
                    {
                        divselproductos.Style.Value = "display:none;";
                        div_masopciones.Style.Value = "display:none;";
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No existe parametrización de esta vista. Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente.";
                        Mensajes_UsuarioValidacionVistas();
                    }
                }
                else
                {
                    divselproductos.Style.Value = "display:none;";
                    div_masopciones.Style.Value = "display:none;";
                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "No existe parametrización de esta vista. Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente.";
                    Mensajes_UsuarioValidacionVistas();
                }

            }
        }

        private void preparacontroles(string vista_final)
        {
            if (vista_final.Equals("Categoria"))
            {
                BtnProdPropio.Text = "Categorías Propias";
                BtnProdCompe.Text = "Categorías Competidor";
                BtnProdPropio.Visible = true;
                BtnProdCompe.Visible = true;
            }

            if (vista_final.Equals("Marca"))
            {
                BtnProdPropio.Text = "Marcas Propias";
                BtnProdCompe.Text = "Marcas Competidor";
                BtnProdPropio.Visible = true;
                BtnProdCompe.Visible = true;
            }

            if (vista_final.Equals("SubMarca"))
            {
                BtnProdPropio.Text = "SubMarcas Propias";
                BtnProdCompe.Text = "SubMarcas Competidor";
                BtnProdPropio.Visible = true;
                BtnProdCompe.Visible = true;
            }

            if (vista_final.Equals("Familia"))
            {
                BtnProdPropio.Text = "Familias Propias";
                BtnProdCompe.Text = "Familias Competidor";
                BtnProdPropio.Visible = true;
                BtnProdCompe.Visible = true;
            }

            if (vista_final.Equals("SubFamilia"))
            {
                BtnProdPropio.Text = "SubFamilias Propias";
                BtnProdCompe.Text = "SubFamilias Competidor";
                BtnProdPropio.Visible = true;
                BtnProdCompe.Visible = true;
            }

            int id_company = Convert.ToInt32(this.Session["company_id"].ToString().Trim());
            string tipo_rep = this.Session["tipo_rep"].ToString();

            if (vista_final.Equals("Producto"))
            {
                BtnProdPropio.Text = "Productos Propios";
                BtnProdCompe.Text = "Productos Competidor";
                bool estado_propio = true;
                bool estado_compe = true;
                if (id_company == 1561)
                {
                    if (tipo_rep.Equals("05"))
                        estado_propio = false;
                    else if (tipo_rep.Equals("04"))
                        estado_compe = false;
                }
                BtnProdPropio.Visible = estado_propio;
                BtnProdCompe.Visible = estado_compe;
            }
            //muestra los paneles
            showpanels();
        }
        /**
         * void showpanels
         * Se agrega este método para la gestión de visibilidad de paneles de levantamiento de información.
         * Angel Ortiz - 31/08/2011
         **/
        private void showpanels()
        {            
            try
            {
                bool[] s_vistas = new bool[6];
                s_vistas = (bool[])(this.Session["s_vistas"]);
                categorias.Visible = s_vistas[0];
                Marcas.Visible = s_vistas[1];
                submarcas.Visible = s_vistas[2];
                Familias.Visible = s_vistas[3];
                SubFamilias.Visible = s_vistas[4];
                productos.Visible = s_vistas[5];
            }
            catch (Exception ex)
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "No existe parametrización de esta vista. Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente.";
                Mensajes_UsuarioValidacionVistas();
            }
        }

        private void cargarlistas()
        {
            bool[] s_vistas = (bool[])this.Session["s_vistas"];
            string vistafinal = this.Session["vista_final"].ToString();

            int company_id = 0;
            int id_categoria = 0;

            if (!CmbCompetidores.Text.Equals("0"))
                company_id = Convert.ToInt32(CmbCompetidores.Text);
            else
                company_id = Convert.ToInt32(this.Session["company_id"].ToString().Trim());

            #region Lista Categorías
            if (s_vistas[0]==true && level_carga == 0)//Si categorias esta habilitado
            {
                DataTable dtcatego = wsPlanning.Get_ObtenerCategoriasPlanning(company_id);
                if (this.Session["vista_final"].ToString().Trim().Equals("Categoria")) // verifica si la vista final es categoría
                {
                    BtnSaveProd.Enabled = true;
                    Chklistcatego.DataSource = dtcatego;
                    Chklistcatego.DataValueField = "id_ProductCategory";
                    Chklistcatego.DataTextField = "Product_Category";
                    Chklistcatego.DataBind();
                    rbliscatego.Items.Clear();
                }
                else
                {
                    rbliscatego.DataSource = dtcatego;
                    rbliscatego.DataValueField = "id_ProductCategory";
                    rbliscatego.DataTextField = "Product_Category";
                    rbliscatego.DataBind();
                    Chklistcatego.Items.Clear();
                }
            }
            #endregion

            try
            {
                id_categoria = Convert.ToInt32(rbliscatego.SelectedValue);
            }
            catch (Exception ex)
            {
                id_categoria = 0;
            }

            #region Lista Marcas
            if(s_vistas[1] == true)
            {
            if ( level_carga > 5 || level_carga == 0)//Si marcas esta habilitado
            {
                DataTable dtmarca = wsPlanning.Get_ObtenerMarcas(id_categoria, company_id);

                if (dtmarca.Rows.Count > 0)
                {
                    if (!vistafinal.Equals("Marca"))//si no es la vista final
                    {
                        rblmarca.DataSource = dtmarca;
                        rblmarca.DataValueField = "id_Brand";
                        rblmarca.DataTextField = "Name_Brand";
                        rblmarca.DataBind();
                        Chklistmarca.Items.Clear();
                    }
                    else
                    {
                        BtnSaveProd.Enabled = true;
                        Chklistmarca.DataSource = dtmarca;
                        Chklistmarca.DataValueField = "id_Brand";
                        Chklistmarca.DataTextField = "Name_Brand";
                        Chklistmarca.DataBind();
                        rblmarca.Items.Clear();
                    }
                }
                dtmarca = null;
            }
            }
            #endregion

            #region Lista SubMarcas

            if(s_vistas[2] == true)
            {

            if ( level_carga > 4 || level_carga == 0)//Si submarcas esta habilitado
            {
                int id_marca = 0;

                if (rblmarca.Items.Count > 0 && rblmarca.SelectedIndex != -1)// si existen marcas y se seleccionó alguna
                    id_marca = Convert.ToInt32(rblmarca.SelectedValue);

                DataTable dtsubmarca = wsPlanning.Get_ObtenerSubMarcas(id_categoria.ToString(), id_marca, company_id);
                if (rblsubmarca.Items.Count > 0)
                {
                    if (!vistafinal.Equals("SubMarca"))
                    {
                        rblsubmarca.DataSource = dtsubmarca;
                        rblsubmarca.DataValueField = "cod_smarca";
                        rblsubmarca.DataTextField = "name_marca";
                        rblsubmarca.DataBind();
                    }
                }
                dtsubmarca = null;
            }
            }
            #endregion

            #region Lista Familias
            if(s_vistas[3] == true)
            {

            if ( level_carga > 3 || level_carga == 0)//si familias está habilitado
            {
                int id_marca = 0;
                int id_submarca = 0;

                if (rblmarca.Items.Count != 0 && rblmarca.SelectedIndex != -1)//si existen marcas y se ha seleccionado alguna
                    id_marca = Convert.ToInt32(rblmarca.SelectedValue);
                if (rblsubmarca.Items.Count != 0 && rblsubmarca.SelectedIndex != -1)//si existen submarcas y se ha seleccionado alguna
                    id_submarca = Convert.ToInt32(rblsubmarca.SelectedValue);

                DataTable dtFamilias = wsPlanning.Get_Obtener_Familias(id_marca, id_submarca, company_id);

                if (!vistafinal.Equals("Familia"))
                {
                    if (dtFamilias != null)
                    {
                        if (dtFamilias.Rows.Count > 0)
                        {
                            rblfamilia.DataSource = dtFamilias;
                            rblfamilia.DataTextField = "name_Family";
                            rblfamilia.DataValueField = "id_ProductFamily";
                            rblfamilia.DataBind();
                        }
                        else
                        {
                            this.Session["encabemensa"] = "Señor Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existen familias para la marca seleccionada.";
                            Mensajes_Usuario();
                        }
                    }
                }
                else
                {
                    if (dtFamilias != null)
                    {
                        if (dtFamilias.Rows.Count > 0)
                        {
                            ChkListFamilias.DataSource = dtFamilias;
                            ChkListFamilias.DataTextField = "name_Family";
                            ChkListFamilias.DataValueField = "id_ProductFamily";
                            ChkListFamilias.DataBind();
                        }
                        else
                        {
                            this.Session["encabemensa"] = "Señor Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existen familias para la marca seleccionada.";
                            Mensajes_Usuario();
                        }
                    }
                }
                dtFamilias = null;
            }

            }
            #endregion

            #region Lista SubFamilias
            if (s_vistas[4] == true)
            {
                if (level_carga > 2 || level_carga == 0)//si subfamilias está habilitado
                {
                    string id_familia = "0";

                    if (rblfamilia.Items.Count > 0 && rblfamilia.SelectedIndex != -1)// si existen familias y se seleccionó alguna.
                        id_familia = rblfamilia.SelectedValue;

                    DataTable dtSubFamilias = wsPlanning.Get_Obtener_SubFamilias(id_familia, company_id);

                    if (!vistafinal.Equals("Familia"))
                    {
                        if (dtSubFamilias != null)
                        {
                            if (dtSubFamilias.Rows.Count > 0)
                            {
                                rblsubfamilia.DataSource = dtSubFamilias;
                                rblsubfamilia.DataTextField = "subfam_name";
                                rblsubfamilia.DataValueField = "id_ProductSubFamily";
                                rblsubfamilia.DataBind();
                            }
                            else
                            {
                                this.Session["encabemensa"] = "Señor Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "No existen familias para la marca seleccionada.";
                                Mensajes_Usuario();
                            }
                        }
                    }
                    else
                    {
                        if (dtSubFamilias != null)
                        {
                            if (dtSubFamilias.Rows.Count > 0)
                            {
                                ChkListSubFamilias.DataSource = dtSubFamilias;
                                ChkListSubFamilias.DataTextField = "name_Family";
                                ChkListSubFamilias.DataValueField = "id_ProductFamily";
                                ChkListSubFamilias.DataBind();
                            }
                            else
                            {
                                this.Session["encabemensa"] = "Señor Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "No existen familias para la marca seleccionada.";
                                Mensajes_Usuario();
                            }
                        }
                    }
                    dtSubFamilias = null;
                }
            }
            #endregion

            #region Lista Productos
            if (s_vistas[5] == true)//si productos está habilitado
            {
                int id_marca = 0;
                string id_familia = "0";
                string id_subfamilia = "0";

                if (rblmarca.Items.Count != 0 && rblmarca.SelectedIndex != -1)// si existen marcas y se ha seleccionado alguna
                    id_marca = Convert.ToInt32(rblmarca.SelectedValue);

                if (rblfamilia.Items.Count > 0 && rblfamilia.SelectedIndex != -1)// si existen familias y se seleccionó alguna.
                    id_familia = rblfamilia.SelectedValue;

                if (rblfamilia.Items.Count > 0 && rblfamilia.SelectedIndex != -1)// si existen familias y se seleccionó alguna.
                    id_familia = rblfamilia.SelectedValue;


                DataTable dtproducto;
                Conexion cn = new Conexion();


                if (this.Session["tipo_rep"].ToString() == "06" && this.Session["RbtnListInfProd"].ToString() == "58" && this.Session["company_id"].ToString() == "1561")
                {
                    dtproducto = cn.ejecutarDataTable("PLA_CONSULTA_MISILES");
                }
                else
                {
                    dtproducto = wsPlanning.Get_Obtener_Productos(id_categoria.ToString(), id_marca.ToString(), id_familia, id_subfamilia, company_id);

                  
                }
                

                if (dtproducto != null)
                {
                    if (dtproducto.Rows.Count > 0)
                    {
                        ChkProductos.DataSource = dtproducto;
                        ChkProductos.DataValueField = "id_product";
                        ChkProductos.DataTextField = "Product_Name";
                        ChkProductos.DataBind();
                    }
                    else
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No existen  productos o ya todos fueron agregados a la campaña.";
                        Mensajes_Usuario();
                    }
                }
                dtproducto = null;
            }
            #endregion

            level_carga = 0;
        }

        protected void RbtMasopciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblReporteAsociado.Text = LblReporteAsociado.Text + "_" + RbtMasopciones.SelectedItem.Text;
            opcionesvistareporte();
            //divselproductos.Style.Value = "display:block;";
            //div_masopciones.Style.Value = "display:none;";
            #region comentado visibilidad
            //DataTable validacion = (DataTable)this.Session["vistas"];

            //if (validacion != null)
            //{
            //    if (validacion.Rows.Count > 0)
            //    {
            //        for (int i = 0; i <= validacion.Rows.Count - 1; i++)
            //        {
            //            if (validacion.Rows[i]["TipoReporte_Descripcion"].ToString().Trim().Equals(RbtMasopciones.SelectedItem.Text))
            //            {
            //                divselproductos.Style.Value = "display:block;";
            //                div_masopciones.Style.Value = "display:none;";

            //                DataTable vistas = (DataTable)this.Session["vistas"];
            //                string tipo;
            //                string vista_final="";
            //                int row = 0;

            //                if (RbtMasopciones.Text.Equals("02"))
            //                    row = 1;

            //                if (vistas.Rows.Count > 0)
            //                {
            //                    bool[] s_vistas = new bool[6];

            //                    s_vistas[0] = Convert.ToBoolean(vistas.Rows[row]["Vista_Categoria"]);
            //                    s_vistas[1] = Convert.ToBoolean(vistas.Rows[row]["Vista_Marca"]);
            //                    s_vistas[2] = Convert.ToBoolean(vistas.Rows[row]["Vista_SubMarca"]);
            //                    s_vistas[3] = Convert.ToBoolean(vistas.Rows[row]["Vista_Familia"]);
            //                    s_vistas[4] = Convert.ToBoolean(vistas.Rows[row]["Vista_SubFamilia"]);
            //                    s_vistas[5] = Convert.ToBoolean(vistas.Rows[row]["Vista_Producto"]);

            //                    this.Session["s_vistas"] = s_vistas;

            //                    tipo = validacion.Rows[row]["id_Tipo_Reporte"].ToString().Trim();
            //                    vista_final = validacion.Rows[row]["vista_final"].ToString().Trim();
            //                }

            //                this.Session["vistas"] = vistas;

            //                if (validacion.Rows[i]["vista_final"].ToString().Trim().Equals("Categoria"))
            //                {                                
            //                    BtnProdPropio.Text = "Categorías Propias";
            //                    BtnProdCompe.Text = "Categorias Competidor";
            //                    BtnProdPropio.Visible = false;
            //                    BtnProdCompe.Visible = false;
            //                    showpanels();
            //                }
            //                if (validacion.Rows[i]["vista_final"].ToString().Trim().Equals("Marca"))
            //                {
            //                    BtnProdPropio.Text = "Marcas Propias";
            //                    BtnProdCompe.Text = "Marcas Competidor";
            //                    BtnProdPropio.Visible = true;
            //                    BtnProdCompe.Visible = true;
            //                    showpanels();
            //                }
            //                if (validacion.Rows[i]["vista_final"].ToString().Trim().Equals("SubMarca"))
            //                {
            //                    BtnProdPropio.Text = "SubMarcas Propias";
            //                    BtnProdCompe.Text = "SubMarcas Competidor";
            //                    BtnProdPropio.Visible = true;
            //                    BtnProdCompe.Visible = true;
            //                    showpanels();
            //                }
            //                if (validacion.Rows[i]["vista_final"].ToString().Trim().Equals("Familia"))
            //                {
            //                    BtnProdPropio.Text = "Familias Propias";
            //                    BtnProdCompe.Text = "Familias Competidor";
            //                    BtnProdPropio.Visible = true;
            //                    BtnProdCompe.Visible = true;
            //                    showpanels();
            //                }
            //                if (validacion.Rows[i]["vista_final"].ToString().Trim().Equals("SubFamilia"))
            //                {
            //                    BtnProdPropio.Text = "Familias Propias";
            //                    BtnProdCompe.Text = "Familias Competidor";
            //                    BtnProdPropio.Visible = true;
            //                    BtnProdCompe.Visible = true;
            //                    showpanels();
            //                }
            //                if (validacion.Rows[i]["vista_final"].ToString().Trim().Equals("Producto"))
            //                {
            //                    BtnProdPropio.Text = "Productos Propios";
            //                    BtnProdCompe.Text = "Productos Competidor";
            //                    BtnProdPropio.Visible = true;
            //                    BtnProdCompe.Visible = true;
            //                    showpanels();
            //                }
            //                if (validacion.Rows[i]["vista_final"].ToString().Trim().Equals(""))
            //                {
            //                    divselproductos.Style.Value = "display:none;";
            //                    this.Session["encabemensa"] = "Señor Usuario";
            //                    this.Session["cssclass"] = "MensajesSupervisor";
            //                    this.Session["mensaje"] = "No existe parametrización de esta vista . Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente";
            //                    Mensajes_UsuarioValidacionVistas();
            //                    BtnProdPropio.Visible = false;
            //                    BtnProdCompe.Visible = false;
            //                }
            //                //this.Session["Vista"] = validacion.Rows[i]["vista_final"].ToString().Trim();
            //                i = validacion.Rows.Count - 1;
            //            }
            //        }
            //    }
            //} 
            #endregion
        }

        protected void BtnAceptaMensajeVista_Click(object sender, EventArgs e)
        {
            ModalPanelreporteproducto.Show();
            divselproductos.Style.Value = "display:none;";
        }
        protected void ImgCloseVistas_Click(object sender, ImageClickEventArgs e)
        {
            CmbSelPresupuestoAsigProd.SelectedValue = "0";
            LblReporteAsociado.Visible = false;
        }

        protected void BtnProdPropio_Click(object sender, EventArgs e)
        {
            this.Session["TipoProducto"] = "P";
            StiloBotonTipoProdSeleccionado();
            BtnProdPropio.CssClass = "buttonNewPlanSel";
            HabilitabotonesTipoProd();
            BtnProdPropio.Enabled = false;
            BtnSaveProd.Enabled = true;
            CmbCompetidores.Visible = false;
            LblSelCompetidor.Visible = false;
            CmbCompetidores.Text = "0";
            limpiarlistas();
            level_carga = 0;
            cargarlistas();
        }

        protected void BtnProdCompe_Click(object sender, EventArgs e)
        {
            if (CmbCompetidores.Items.Count == 1)
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Actualmente el cliente no tiene ningun competidor asignado . Por favor solicite esta información al administrador de Xplora";
                Mensajes_Usuario();
            }
            else
            {
                this.Session["TipoProducto"] = "C";
                StiloBotonTipoProdSeleccionado();
                BtnProdCompe.CssClass = "buttonNewPlanSel";
                HabilitabotonesTipoProd();
                BtnProdCompe.Enabled = false;
                BtnSaveProd.Enabled = true;
                CmbCompetidores.Visible = true;
                LblSelCompetidor.Visible = true;
                CmbCompetidores.Text = "0";
                limpiarlistas();
                //level_carga = 0;
                //cargarlistas();
            }
        }
        protected void CmbCompetidores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CmbCompetidores.SelectedItem.Value.Equals("0"))
            {
                BtnSaveProd.Enabled = true;
                limpiarlistas();
                level_carga = 0;
                cargarlistas();
            }
            else
            {
                BtnSaveProd.Enabled = false;
                limpiarlistas();
            }
        }
        protected void rbliscatego_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BtnSaveProd.Enabled = true;                
                Chklistmarca.Items.Clear();
                rblmarca.Items.Clear();                
                rblsubmarca.Items.Clear();
                ChkListFamilias.Items.Clear();
                rblfamilia.Items.Clear();
                ChkListSubFamilias.Items.Clear();
                rblsubfamilia.Items.Clear();
                ChkProductos.Items.Clear();
                level_carga = 6; 
                cargarlistas();                
            }
            catch(Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void rblmarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rblsubmarca.Items.Clear();
                ChkListFamilias.Items.Clear();
                rblfamilia.Items.Clear();
                ChkListSubFamilias.Items.Clear();
                rblsubfamilia.Items.Clear();
                ChkProductos.Items.Clear();
                //Se llena lisbox de productos filtrados por marcas 
                level_carga = 5;
                cargarlistas();
            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void rblsubmarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChkListFamilias.Items.Clear();
                rblfamilia.Items.Clear();
                ChkListSubFamilias.Items.Clear();
                rblsubfamilia.Items.Clear();
                ChkProductos.Items.Clear();
                level_carga = 4;
                cargarlistas();
            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void rblfamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChkListSubFamilias.Items.Clear();
                rblsubfamilia.Items.Clear();
                ChkProductos.Items.Clear();
                level_carga = 3;
                cargarlistas();
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void rblsubfamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChkProductos.Items.Clear();
                level_carga = 2;
                cargarlistas();
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        

        protected void BtnSaveProd_Click(object sender, EventArgs e)
        {
            Boolean continuar = datoscompletosProductos();
            DAplicacion Dduplicidad = new DAplicacion();
            string sTIPO_LEVANTAMIENTO;

            if (continuar)
            {
                bool sigue = true;
                sTIPO_LEVANTAMIENTO = this.Session["vista_final"].ToString().Trim();

                #region Levantamiento por PRODUCTOS
                if (sTIPO_LEVANTAMIENTO.Equals("Producto"))
                {
                    // Se crea una lista en memoria con los productos a cargar
                    ListItemCollection listaproductos = new ListItemCollection();

                    foreach (ListItem producto in ChkProductos.Items)
                    {
                        if (producto.Selected)
                        {
                            listaproductos.Add(producto);
                        }
                    }
                    Conexion cn = new Conexion();
                    foreach(ListItem producto in listaproductos)
                    {
                        DataTable dtconsulta = cn.ejecutarDataTable("PLA_CONSULTA_PRODUCT_PLANNING",producto.Value,TxtPlanningAsigProd.Text,RbtnListInfProd.SelectedItem.Value);
                        if (dtconsulta.Rows.Count != 0)
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "El producto " + producto.Text + ", ya existe para esta Campaña";
                            Mensajes_Usuario();
                            sigue = false;
                            break;
                        }
                    }

                    //if (sigue)
                    //{
                    //    foreach (ListItem producto in listaproductos)
                    //    {
                    //        DataTable dtbrandysubbrand = wsPlanning.Get_Obtener_BrandySubbrandxProducto(Convert.ToInt32(producto.Value));
                    //        if (dtbrandysubbrand != null)
                    //        {
                    //            if (dtbrandysubbrand.Rows.Count > 0)
                    //            {
                    //                this.Session["brand"] = dtbrandysubbrand.Rows[0]["id_Brand"].ToString().Trim();
                    //                this.Session["sub_brand"] = dtbrandysubbrand.Rows[0]["id_SubBrand"].ToString().Trim();
                    //            }
                    //        }
                    //        DPlanning PermitirGuardar = new DPlanning();
                    //        DataTable dtpermitir = PermitirGuardar.Permitir_GuardarLevantamiento(sTIPO_LEVANTAMIENTO, Convert.ToInt64(producto.Value), rbliscatego.SelectedValue, Convert.ToInt32(this.Session["brand"].ToString().Trim()), null);
                    //        if (dtpermitir != null)
                    //        {
                    //            if (dtpermitir.Rows[0]["CONTINUAR"].ToString().Trim().Equals("CONTINUAR"))
                    //            {
                    //                sigue = true;
                    //            }
                    //            else
                    //            {
                    //                this.Session["encabemensa"] = "Sr. Usuario";
                    //                this.Session["cssclass"] = "MensajesSupervisor";
                    //                this.Session["mensaje"] = "El producto " + producto.Text + " no se puede almacenar por falta de información en Categorías, Marcas y Productos. Consulte con el Administrador Xplora";
                    //                Mensajes_Usuario();
                    //                sigue = false;
                    //                break;
                    //            }
                    //        }                            
                    //    }
                    //}

                    DPlanning Dplanning = new DPlanning();
                    if (sigue)
                    {
                        //Ejecutar Método para almacenar los productos seleccionados para el planning. Ing. Mauricio Ortiz  
                        foreach (ListItem producto in listaproductos)
                        {
                            DataTable dtconsulta = cn.ejecutarDataTable("PLA_CONSULTA_PRODUCT_PLANNING", producto.Value, TxtPlanningAsigProd.Text, RbtnListInfProd.SelectedItem.Value);
                            if (dtconsulta.Rows.Count != 0)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "El producto " + producto.Text + ", ya existe para esta Campaña";
                                Mensajes_Usuario();
                                sigue = false;
                                break;
                            }
                            else
                            {


                                DataTable dt = cn.ejecutarDataTable("PLA_CONSULTA_PRODUCTXID", producto.Value);
                                DataTable productos = new DataTable();
                                
                                  
                                 int ProductsPlanningInitialStock = 0;
                                 int Report_Id = Convert.ToInt32(RbtnListInfProd.SelectedItem.Value);
                                 bool Status = true;

                                 string id_company = this.Session["company_id"].ToString().Trim();
                                 string cod_channel = this.Session["Planning_CodChannel"].ToString().Trim();

                                 string id_planning = TxtPlanningAsigProd.Text;
                                 long id_Product = Convert.ToInt64(producto.Value);

                                 string idcategoria = dt.Rows[0]["id_ProductCategory"].ToString().Trim();
                                 int idMarca =Convert.ToInt32(dt.Rows[0]["id_Brand"].ToString().Trim());
                                 string idSubMarcar = dt.Rows[0]["id_SubBrand"].ToString().Trim();
                                  string idFamilia = dt.Rows[0]["id_ProductFamily"].ToString().Trim();
                                 string idSubFamilia = dt.Rows[0]["id_ProductSubFamily"].ToString().Trim();

                                 string Produc_Caracteristicas = dt.Rows[0]["Produc_Caracteristicas"].ToString().Trim();
                                 string Produc_Beneficios = dt.Rows[0]["Produc_Beneficios"].ToString().Trim();

                                 string usuario = this.Session["sUser"].ToString().Trim();
                                string tipo_prod = this.Session["TipoProducto"].ToString().Trim();

                                 productos = Dplanning.Crear_Productos_Planning(id_planning, id_Product, idcategoria, idMarca, idSubMarcar, idFamilia, idSubFamilia, Produc_Caracteristicas, Produc_Beneficios, ProductsPlanningInitialStock, Report_Id, Status, usuario, DateTime.Now, usuario, DateTime.Now);


                                 string opcionesreporte;
                                 string mar_propio = "S";

                                 try
                                 {
                                     opcionesreporte = RbtMasopciones.SelectedItem.Text;
                                     if (this.Session["TipoProducto"].Equals("C")) // 
                                         mar_propio = "N"; //obtenemos si la marca es propia 'S' o 'N'
                                 }
                                 catch (Exception ex)
                                 {
                                     opcionesreporte = "";
                                 }

                                 string cod_producto = productos.Rows[0]["cod_producto"].ToString();
                                 string categoria = productos.Rows[0]["categoria"].ToString();
                                 string id_eq_fam = productos.Rows[0]["equipo_familia"].ToString();
                                 string familia = productos.Rows[0]["familia"].ToString();
                                 string id_eq_subfam = productos.Rows[0]["equipo_subfamilia"].ToString();
                                 string subfamilia = productos.Rows[0]["subfamilia"].ToString();
                                 string marca = productos.Rows[0]["marca"].ToString();

                                 //Agrega el producto a tbl_equipo_productos(Base Intermedia)
                                 Dplanning.Registrar_TBL_EQUIPO_PRODUCTOS(id_planning, id_Product, cod_producto, idcategoria, categoria, idMarca,
                                     idSubMarcar, id_eq_fam, idFamilia, familia, id_eq_subfam, idSubFamilia, subfamilia, marca, mar_propio,
                                     Report_Id, tipo_prod, opcionesreporte, id_company, cod_channel);

                            }
                        }
                        //Ejecutar Método para almacenar los productos seleccionados para el planning. Ing. Mauricio Ortiz 
                        // Se cambia el metodo utilizando lista temporal de productos seleccionados. Angel Ortiz 08/2011
                        //foreach (ListItem producto in listaproductos)
                        //{
                        //    //ejecutar metodo para consultar datos de producto 
                        //    DataTable dtbrandysubbrand = wsPlanning.Get_Obtener_BrandySubbrandxProducto(Convert.ToInt32(producto.Value));
                        //    if (dtbrandysubbrand != null)
                        //    {
                        //        if (dtbrandysubbrand.Rows.Count > 0)
                        //        {
                        //            this.Session["brand"] = dtbrandysubbrand.Rows[0]["id_Brand"].ToString().Trim();
                        //            this.Session["sub_brand"] = dtbrandysubbrand.Rows[0]["id_SubBrand"].ToString().Trim();
                        //        }
                        //    }

                        //    //DataTable dtconsulta = Dduplicidad.ConsultaDuplicados(ConfigurationManager.AppSettings["Product_planning"], TxtPlanningAsigProd.Text, ChkProductos.Items[i].Value, null);
                        //    DataTable dtconsulta = cn.ejecutarDataTable("PLA_CONSULTA_PRODUCT_PLANNING", producto.Value, TxtPlanningAsigProd.Text, RbtnListInfProd.SelectedItem.Value);
                        //if (dtconsulta.Rows.Count == 0)
                        //    {
                        //        // Se comenta registro que utiliza el webservice.
                        //        // wsPlanning.Get_Regitration_ProductosPlanning(TxtPlanningAsigProd.Text, Convert.ToInt64(ChkProductos.Items[i].Value), rbliscatego.SelectedItem.Value, Convert.ToInt32(this.Session["brand"].ToString().Trim()), this.Session["sub_brand"].ToString().Trim(), "", "", 0, Convert.ToInt32(RbtnListInfProd.SelectedItem.Value), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                        //        DPlanning Dplanning = new DPlanning();

                        //        //Objeto para obtener parametros necesarios para la insercion en DB Intermedia:
                        //        DataTable productos = new DataTable();

                                
                        //         string id_company = this.Session["company_id"].ToString().Trim();
                        //         string cod_channel = this.Session["Planning_CodChannel"].ToString().Trim();

                        //        string id_planning = TxtPlanningAsigProd.Text;
                        //        long id_Product = Convert.ToInt64(producto.Value);
                        //        string idProductCategory = rbliscatego.SelectedValue;
                        //        int id_Brand = Convert.ToInt32(this.Session["brand"].ToString().Trim());
                        //        string idSubBrand = this.Session["sub_brand"].ToString().Trim();
                        //        string id_familia;
                        //        string id_subfamilia;

                        //        try { id_familia = rblfamilia.SelectedItem.Value; }
                        //        catch { id_familia = "0"; }
                        //        try { id_subfamilia = rblsubfamilia.SelectedItem.Value; }
                        //        catch { id_subfamilia = "0"; }

                        //        string ProducCarac = "";
                        //        string ProducBeni = "";
                        //        int ProductsPlanningInitialStock = 0;
                        //        int Report_Id = Convert.ToInt32(RbtnListInfProd.SelectedItem.Value);
                        //        bool Status = true;
                        //        string usuario = this.Session["sUser"].ToString().Trim();
                        //        string tipo_prod = this.Session["TipoProducto"].ToString().Trim();

                        //        //se agrega el producto al planning en base Xplora
                        //        productos = Dplanning.Crear_Productos_Planning(id_planning, id_Product, idProductCategory, id_Brand, idSubBrand, id_familia, id_subfamilia, ProducCarac, ProducBeni, ProductsPlanningInitialStock, Report_Id, Status, usuario, DateTime.Now, usuario, DateTime.Now);

                        //        string opcionesreporte;
                        //        string mar_propio = "S";

                        //        try
                        //        {
                        //            opcionesreporte = RbtMasopciones.SelectedItem.Text;
                        //            if (this.Session["TipoProducto"].Equals("C")) // 
                        //                mar_propio = "N"; //obtenemos si la marca es propia 'S' o 'N'
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            opcionesreporte = "";
                        //        }

                        //        string cod_producto = productos.Rows[0]["cod_producto"].ToString();
                        //        string categoria = productos.Rows[0]["categoria"].ToString();
                        //        string id_eq_fam = productos.Rows[0]["equipo_familia"].ToString();
                        //        string familia = productos.Rows[0]["familia"].ToString();
                        //        string id_eq_subfam = productos.Rows[0]["equipo_subfamilia"].ToString();
                        //        string subfamilia = productos.Rows[0]["subfamilia"].ToString();
                        //        string marca = productos.Rows[0]["marca"].ToString();

                        //        //Agrega el producto a tbl_equipo_productos(Base Intermedia)
                        //        Dplanning.Registrar_TBL_EQUIPO_PRODUCTOS(id_planning, id_Product, cod_producto, idProductCategory, categoria, id_Brand,
                        //            idSubBrand, id_eq_fam, id_familia, familia, id_eq_subfam, id_subfamilia, subfamilia, marca, mar_propio,
                        //            Report_Id, tipo_prod, opcionesreporte, id_company, cod_channel);

                        //        #region Insercion TBL_PRODUCTO_CADENA
                        //        // SE COMENTA CODIGO PARA LA INSERCION DE TBL_PRODUCTO_CADENA
                        //        //////////////////////////////////////////////////////////////////
                        //        //try
                        //        //{
                        //        //    DataTable dtRegsitrarTBLPROD = Dplanning.Registrar_TBL_PRODUCTO_CADENA("1", this.Session["TipoProducto"].ToString().Trim(),
                        //        //            Convert.ToInt64(producto.Value), TxtPlanningAsigProd.Text, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");

                        //        //    if (dtRegsitrarTBLPROD != null)
                        //        //    {
                        //        //        if (dtRegsitrarTBLPROD.Rows.Count > 0)
                        //        //        {
                        //        //            for (int ir = 0; ir <= dtRegsitrarTBLPROD.Rows.Count - 1; ir++)
                        //        //            {
                        //        //                try
                        //        //                {
                        //        //                    DataTable dtRegsitrarTBLPRODUCTOS = Dplanning.Registrar_TBL_PRODUCTO_CADENA("2", this.Session["TipoProducto"].ToString().Trim(),
                        //        //                                                                Convert.ToInt64(producto.Value), TxtPlanningAsigProd.Text,
                        //        //                                                                dtRegsitrarTBLPROD.Rows[ir][0].ToString().Trim(),
                        //        //                        dtRegsitrarTBLPROD.Rows[ir][1].ToString().Trim(),
                        //        //                        dtRegsitrarTBLPROD.Rows[ir][2].ToString().Trim(),
                        //        //                        dtRegsitrarTBLPROD.Rows[ir][3].ToString().Trim(),
                        //        //                        dtRegsitrarTBLPROD.Rows[ir][4].ToString().Trim(),
                        //        //                        dtRegsitrarTBLPROD.Rows[ir][5].ToString().Trim(),
                        //        //                        dtRegsitrarTBLPROD.Rows[ir][6].ToString().Trim(),
                        //        //                        dtRegsitrarTBLPROD.Rows[ir][7].ToString().Trim(),
                        //        //                        dtRegsitrarTBLPROD.Rows[ir][8].ToString().Trim(),
                        //        //                        dtRegsitrarTBLPROD.Rows[ir][9].ToString().Trim());
                        //        //                }
                        //        //                catch (Exception ex)
                        //        //                {
                        //        //                    // no inserta en tbl_producto_cadena nada porq ya existe en esa tabla
                        //        //                }
                        //        //            }
                        //        //        }
                        //        //    }
                        //        //    dtRegsitrarTBLPROD = null;
                        //        //}
                        //        //catch
                        //        //{

                        //        //} 
                        //        #endregion

                        //        this.Session["encabemensa"] = "Sr. Usuario";
                        //        this.Session["cssclass"] = "MensajesSupConfirm";
                        //        this.Session["mensaje"] = "Se ha creado con éxito los productos para la campaña: " + CmbSelPresupuestoAsigProd.SelectedItem.Text.ToUpper();
                        //        Mensajes_Usuario();
                        //    }
                        //    dtconsulta = null;
                        //    dtbrandysubbrand = null;
                        //}

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se ha creado con éxito los productos para la campaña: " + CmbSelPresupuestoAsigProd.SelectedItem.Text.ToUpper();
                        Mensajes_Usuario();


                        BtnSaveProd.Enabled = false;
                        Chklistmarca.Items.Clear();
                        rblmarca.Items.Clear();
                        Chklistmarca.Items.Clear();
                        rblsubmarca.Items.Clear();
                        ChkListFamilias.Items.Clear();
                        ChkProductos.Items.Clear();
                    }
                } 
                #endregion

                else
                {
                    #region Levantamiento por MARCA
                    if (sTIPO_LEVANTAMIENTO.Equals("Marca"))
                    {
                        ListItemCollection listamarcas = new ListItemCollection();

                        foreach (ListItem marca in Chklistmarca.Items)
                        {
                            if (marca.Selected)
                                listamarcas.Add(marca);
                        }


                        foreach (ListItem marca in listamarcas)
                        {
                            DataTable dtconsulta = Dduplicidad.ConsultaDuplicadosNew(ConfigurationManager.AppSettings["PLA_Brand_Planning"], TxtPlanningAsigProd.Text, marca.Value, RbtnListInfProd.SelectedItem.Value, null);
                            if (dtconsulta != null)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "La marca " + marca.Text + ", ya existe para esta Campaña";
                                Mensajes_Usuario();
                                sigue = false;
                                break;
                            }
                        }

                        if (sigue)
                        {
                            foreach (ListItem marca in listamarcas)
                            {
                                DPlanning PermitirGuardar = new DPlanning();
                                DataTable dtpermitir = PermitirGuardar.Permitir_GuardarLevantamiento(sTIPO_LEVANTAMIENTO, 0, rbliscatego.SelectedItem.Value, Convert.ToInt32(marca.Value), null);
                                if (dtpermitir != null)
                                {
                                    if (dtpermitir.Rows[0]["CONTINUAR"].ToString().Trim().Equals("CONTINUAR"))
                                    {
                                        sigue = true;
                                    }
                                    else
                                    {
                                        this.Session["encabemensa"] = "Sr. Usuario";
                                        this.Session["cssclass"] = "MensajesSupervisor";
                                        this.Session["mensaje"] = "La Marca " + marca.Text + " no se puede almacenar por falta de información en Categorías, Marcas. Consulte con el Administrador Xplora";
                                        Mensajes_Usuario();
                                        sigue = false;
                                        break;
                                    }
                                }
                            }
                        }

                        if (sigue)
                        {
                            //Ejecutar Método para almacenar las marcas seleccionados para el planning. Ing. Mauricio Ortiz  
                            foreach (ListItem marca in listamarcas)
                            {
                                DataTable dtconsulta = Dduplicidad.ConsultaDuplicadosNew(ConfigurationManager.AppSettings["PLA_Brand_Planning"], TxtPlanningAsigProd.Text, marca.Value, RbtnListInfProd.SelectedItem.Value, null);
                                if (dtconsulta == null)
                                {
                                    string id_planning = TxtPlanningAsigProd.Text;
                                    string id_ProductCategory = rbliscatego.SelectedItem.Value;
                                    int id_Brand = Convert.ToInt32(marca.Value);
                                    int Report_Id = Convert.ToInt32(RbtnListInfProd.SelectedItem.Value);
                                    bool Status = true;
                                    string usuario = this.Session["sUser"].ToString().Trim();
                                    DPlanning Dplanning = new DPlanning();
                                    DataTable dt_marcas = new DataTable();
                                    dt_marcas = Dplanning.Crear_Marcas_Planning(id_planning, id_ProductCategory, id_Brand, Report_Id, Status, usuario, DateTime.Now, usuario, DateTime.Now);
                                    
                                    try
                                    {
                                        string id_brand_pla = dt_marcas.Rows[0]["id_brand_pla"].ToString();
                                        string categoria = dt_marcas.Rows[0]["categoria"].ToString();
                                        string id_eq_cat = dt_marcas.Rows[0]["id_eq_cat"].ToString();                                        
                                        Dplanning.Registrar_TBL_EQUIPO_MARCAS(id_brand_pla, id_planning,id_ProductCategory,categoria,id_eq_cat,id_Brand.ToString(),Report_Id.ToString(),"1");
                                    }
                                    catch (Exception ex)
                                    {
                                    }

                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupConfirm";
                                    this.Session["mensaje"] = "Se ha creado con éxito las marcas para la campaña : " + CmbSelPresupuestoAsigProd.SelectedItem.Text.ToUpper();
                                    Mensajes_Usuario();
                                }
                                dtconsulta = null;
                            }
                            BtnSaveProd.Enabled = false;
                            Chklistmarca.Items.Clear();
                            rblmarca.Items.Clear();
                            Chklistmarca.Items.Clear();
                            rblsubmarca.Items.Clear();
                            ChkListFamilias.Items.Clear();
                            ChkProductos.Items.Clear();
                        }
                    }
                    #endregion
                    else
                    {
                        #region Levantamiento por FAMILIA
                        if (sTIPO_LEVANTAMIENTO.Equals("Familia"))
                        {
                            // Se crea una lista en memoria con los productos a cargar
                            ListItemCollection listafamilias = new ListItemCollection();

                            foreach (ListItem familia in ChkListFamilias.Items)
                            {
                                if (familia.Selected)
                                {
                                    listafamilias.Add(familia);
                                }
                            }

                            foreach (ListItem familia in listafamilias)
                            {
                                DataTable dtconsulta = Dduplicidad.ConsultaDuplicados(ConfigurationManager.AppSettings["PLA_Family_Planning"], TxtPlanningAsigProd.Text, familia.Value, RbtnListInfProd.SelectedItem.Value);
                                if (dtconsulta != null)
                                {
                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "La Familia  " + familia.Text + " Ya exite para esta Campaña";
                                    Mensajes_Usuario();
                                    sigue = false;
                                    break;
                                }
                            }

                            if (sigue)
                            {
                                foreach (ListItem familia in listafamilias)
                                {
                                    DPlanning PermitirGuardar = new DPlanning();
                                    DataTable dtpermitir = PermitirGuardar.Permitir_GuardarLevantamiento(sTIPO_LEVANTAMIENTO, 0, rbliscatego.SelectedItem.Value, 0, familia.Value);
                                    if (dtpermitir != null)
                                    {
                                        if (dtpermitir.Rows[0]["CONTINUAR"].ToString().Trim().Equals("CONTINUAR"))
                                        {
                                            sigue = true;
                                        }
                                        else
                                        {
                                            this.Session["encabemensa"] = "Sr. Usuario";
                                            this.Session["cssclass"] = "MensajesSupervisor";
                                            this.Session["mensaje"] = "La familia " + familia.Text + ", no se puede almacenar por falta de información en Categorías , Marcas y Familias . Consulte con el Administrador Xplora";
                                            Mensajes_Usuario();
                                            sigue = false;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (sigue)
                            {
                                //Ejecutar Método para almacenar las familias seleccionadas para el planning. Ing. Mauricio Ortiz  
                                foreach (ListItem familia in listafamilias)
                                {
                                    DataTable dtconsulta = Dduplicidad.ConsultaDuplicados(ConfigurationManager.AppSettings["PLA_Family_Planning"], TxtPlanningAsigProd.Text, familia.Value, RbtnListInfProd.SelectedItem.Value);
                                    if (dtconsulta == null)
                                    {                                        
                                        string id_equipo = TxtPlanningAsigProd.Text;
                                        string id_categoria = rbliscatego.SelectedItem.Value;                                        
                                        int id_marca = Convert.ToInt32(rblmarca.SelectedValue);                                        
                                        string id_familia = familia.Value;
                                        int id_reporte = Convert.ToInt32(RbtnListInfProd.SelectedItem.Value);
                                        string usuario =  this.Session["sUser"].ToString().Trim();

                                        //DataTable dt_levfamilias = wsPlanning.Get_Registrar_FamiliasPlanning(id_equipo, id_categoria, id_marca, id_familia, id_reporte, true, usuario, DateTime.Now, usuario, DateTime.Now);
                                        DPlanning Dplanning = new DPlanning();

                                        //string id_eqcategoria = dt_levfamilias.Rows[0]["id_eqcategoria"].ToString();
                                        //string id_eqfamilia = dt_levfamilias.Rows[0]["id_eqfamilia"].ToString();
                                        //string id_eqmarca = dt_levfamilias.Rows[0]["id_eqmarca"].ToString();
                                        //try
                                        //{
                                        //    DataTable dtRegsitrarTBL_EQUIPO_FAMILIAS = Dplanning.Registrar_TBL_EQUIPO_FAMILIAS(id_eqfamilia, id_equipo, id_categoria,
                                        //        id_eqcategoria, id_marca.ToString(), id_eqmarca, id_familia, id_reporte.ToString());
                                        //}
                                        //catch (Exception ex)
                                        //{

                                        //}

                                        this.Session["encabemensa"] = "Sr. Usuario";
                                        this.Session["cssclass"] = "MensajesSupConfirm";
                                        this.Session["mensaje"] = "Se ha creado con éxito las familias para la campaña : " + CmbSelPresupuestoAsigProd.SelectedItem.Text.ToUpper();
                                        Mensajes_Usuario();
                                    }
                                }
                                BtnSaveProd.Enabled = false;
                                Chklistmarca.Items.Clear();
                                rblmarca.Items.Clear();
                                Chklistmarca.Items.Clear();
                                rblsubmarca.Items.Clear();
                                ChkListFamilias.Items.Clear();
                                ChkProductos.Items.Clear();
                            }
                        } 
                        #endregion
                        else
                        {
                            #region Levantamiento por CATEGORIA
                            if (sTIPO_LEVANTAMIENTO.Equals("Categoria"))
                            {
                                // Se crea una lista en memoria con los productos a cargar
                                ListItemCollection listacategorias = new ListItemCollection();

                                foreach (ListItem categoria in Chklistcatego.Items)
                                {
                                    if (categoria.Selected)
                                    {
                                        listacategorias.Add(categoria);
                                    }
                                }

                                foreach (ListItem categoria in listacategorias)
                                {
                                    DataTable dtconsulta = Dduplicidad.DuplicadosCategoriasPlanning(ConfigurationManager.AppSettings["PLA_Category_Planning"], TxtPlanningAsigProd.Text, categoria.Value, RbtnListInfProd.SelectedItem.Value);
                                    if (dtconsulta != null)
                                    {
                                        this.Session["encabemensa"] = "Sr. Usuario";
                                        this.Session["cssclass"] = "MensajesSupervisor";
                                        this.Session["mensaje"] = "La Categoría  " + categoria.Text + ", ya existe para esta Campaña";
                                        Mensajes_Usuario();
                                        sigue = false;
                                        break;
                                    }
                                }

                                if (sigue)
                                {
                                    foreach (ListItem categoria in listacategorias)
                                    {
                                        DPlanning PermitirGuardar = new DPlanning();
                                        DataTable dtpermitir = PermitirGuardar.Permitir_GuardarLevantamiento(sTIPO_LEVANTAMIENTO, 0, categoria.Value, 0, null);
                                        if (dtpermitir != null)
                                        {
                                            if (dtpermitir.Rows[0]["CONTINUAR"].ToString().Trim().Equals("CONTINUAR"))
                                            {
                                                sigue = true;
                                            }
                                            else
                                            {
                                                this.Session["encabemensa"] = "Sr. Usuario";
                                                this.Session["cssclass"] = "MensajesSupervisor";
                                                this.Session["mensaje"] = "La Categoría " + categoria.Text + ", no se puede almacenar por falta de información en Categorías. Consulte con el Administrador Xplora";
                                                Mensajes_Usuario();
                                                sigue = false;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (sigue)
                                {
                                    //Ejecutar Método para almacenar las categorias seleccionadas para el planning. Ing. Mauricio Ortiz  
                                    foreach (ListItem categoria in listacategorias)
                                    {
                                        DataTable dtconsulta = Dduplicidad.DuplicadosCategoriasPlanning(ConfigurationManager.AppSettings["PLA_Category_Planning"], TxtPlanningAsigProd.Text, categoria.Value, RbtnListInfProd.SelectedItem.Value);
                                        if (dtconsulta == null)
                                        {
                                            string id_planning = TxtPlanningAsigProd.Text;
                                            string id_categoria = categoria.Value;
                                            int report_id = Convert.ToInt32(RbtnListInfProd.SelectedItem.Value);
                                            bool status = true;
                                            string usuario = this.Session["sUser"].ToString().Trim();

                                            DPlanning dregistrarCateg = new DPlanning();
                                            DataTable dtcategorias = dregistrarCateg.Crear_CategoriasPlanning(id_planning,id_categoria,report_id,status,usuario,DateTime.Now,usuario,DateTime.Now);

                                            try
                                            {
                                                DataTable dtRegistrarTBL_EQUIPO_CATEGORIAS = dregistrarCateg.Registrar_TBL_EQUIPO_CATEGORIAS(dtcategorias.Rows[0]["id_CategoryPlanning"].ToString(), id_planning, id_categoria, report_id.ToString(), "1");
                                            }
                                            catch (Exception ex)
                                            {

                                            }

                                            this.Session["encabemensa"] = "Sr. Usuario";
                                            this.Session["cssclass"] = "MensajesSupConfirm";
                                            this.Session["mensaje"] = "Se ha creado con éxito las categorías para la campaña : " + CmbSelPresupuestoAsigProd.SelectedItem.Text.ToUpper();
                                            Mensajes_Usuario();
                                        }
                                    }
                                    Chklistmarca.Items.Clear();
                                    rblmarca.Items.Clear();
                                    rblsubmarca.Items.Clear();
                                    ChkListFamilias.Items.Clear();
                                    ChkProductos.Items.Clear();
                                }
                            } 
                            #endregion
                            else
                            {
                                this.Session["encabemensa"] = "Señor Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "Este reporte seleccionado no tiene disponible esta funcionalidad. Consulte con el Administrador Xplora";
                                Mensajes_Usuario();
                            }
                        }
                    }
                }
            }
            else
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensajes_Usuario();
            }
        }
        protected void BtnClearProd_Click(object sender, EventArgs e)
        {
            //CmbSelPresupuestoAsigProd.Text = "0";
            //TxtPlanningAsigProd.Text = "";
            BtnProdCompe.Enabled = false;
            BtnProdPropio.Enabled = false;
            StiloBotonTipoProdSeleccionado();
            CmbCompetidores.Visible = false;
            LblSelCompetidor.Visible = false;
            BtnSaveProd.Enabled = false;
            BtnClearProd.Enabled = true;
            rbliscatego.Items.Clear();
            Chklistcatego.Items.Clear();
            rblmarca.Items.Clear();
            rblsubmarca.Items.Clear();
            ChkProductos.Items.Clear();
            divselproductos.Style.Value = "display:none;";
            ModalPanelreporteproducto.Show();
            llenainformesProd();
        }
        protected void BtnCargaLevanInform_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            PanelCargaMasivaProductos.Style.Value = "Display:Block;";

            IframeCargaMasivaProd.Attributes["src"] = "CargaLevInformacion.aspx";
        }
        protected void btnCargaPrecio_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            PanelCargaMasivaProductos.Style.Value = "Display:Block;";

            IframeCargaMasivaProd.Attributes["src"] = "Carga_Precio.aspx";
        }
        protected void BtnCloseMasivaProd_Click(object sender, ImageClickEventArgs e)
        {
            InicializarPaneles();
            PanelAsignaProductos.Style.Value = "Display:Block;";

            IframeCargaMasivaProd.Attributes["src"] = "";
        }

        #endregion

        #region Rutas
        protected void CmbSelPresupuestoAsigPDVOPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbSelPresupuestoAsigPDVOPE.SelectedValue != "0")
            {
                DataTable dtAsigna_PDV_A_OPE_Temp = new DataTable();
                dtAsigna_PDV_A_OPE_Temp.Columns.Add("Cod_", typeof(Int32));
                dtAsigna_PDV_A_OPE_Temp.Columns.Add("Mercaderista", typeof(String));
                dtAsigna_PDV_A_OPE_Temp.Columns.Add("Cod.", typeof(Int32));
                dtAsigna_PDV_A_OPE_Temp.Columns.Add("Punto_de_Venta", typeof(String));
                dtAsigna_PDV_A_OPE_Temp.Columns.Add("Desde", typeof(String));
                dtAsigna_PDV_A_OPE_Temp.Columns.Add("Hasta", typeof(String));
                GvAsignaPDVOPE.DataSource = dtAsigna_PDV_A_OPE_Temp;
                GvAsignaPDVOPE.DataBind();
                this.Session["dtAsigna_PDV_A_OPE_Temp"] = dtAsigna_PDV_A_OPE_Temp;
                BtnClearAsigPDVOPE.Enabled = true;
                BtnCargaPDVOPE.Enabled = true;
                //ejecutar método para obtener id del planning generado 
                DataTable dt = wsPlanning.ObtenerIdPlanning(CmbSelPresupuestoAsigPDVOPE.SelectedValue);
                TxtPlanningAsigPDVOPE.Text = dt.Rows[0]["Planning"].ToString().Trim();

                this.Session["id_planningPDVOPE"] = TxtPlanningAsigPDVOPE.Text;
                this.Session["PresupuestoPDVOPE"] = CmbSelPresupuestoAsigPDVOPE.SelectedItem.Text;

                this.Session["company_id"] = dt.Rows[0]["Company_id"].ToString().Trim();
                this.Session["Fechainicial"] = dt.Rows[0]["Planning_StartActivity"].ToString().Trim();
                this.Session["Fechafinal"] = dt.Rows[0]["Planning_EndActivity"].ToString().Trim();

                BtnSaveAsigPDVOPE.Enabled = false;

                Activar_AsignacionPDVOPE();
                llenaciudades();
                llenaOperativosAsignaPDVOPE();
                if (CmbSelOpePlanning.Items.Count > 0)
                {
                    //Llenamallas();
                    //Llenasector();
                    ChkListPDV.Items.Clear();
                }
                else
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "No se ha seleccionado el personal de la campaña: " + CmbSelPresupuestoAsigPDVOPE.SelectedItem.Text.ToUpper();
                    Mensajes_Usuario();
                    limpiar_InformacionPDVOPE();
                    BtnSaveAsigPDVOPE.Enabled = false;
                    Inactivar_AsignacionPDVOPE();
                }

            }

            else
            {
                limpiar_InformacionPDVOPE();
                BtnSaveAsigPDVOPE.Enabled = false;
                BtnClearAsigPDVOPE.Enabled = false;
                BtnCargaPDVOPE.Enabled = false;
                Inactivar_AsignacionPDVOPE();
            }
        }
        protected void CmbSelCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaTipoAgrup();
        }
        protected void CmbSelTipoAgrup_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaAgrup();
        }
        protected void CmbSelAgrup_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaOficinas();
        }
        protected void CmbSelOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            Llenamallas();
            Llenasector();
            LlenaPDVPlanning();
            //LlenarFrecuencias();

        }
        protected void CmbSelMalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            Llenasector();
            LlenaPDVPlanning();
        }
        protected void CmbSelSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaPDVPlanning();
        }

        protected void BtnCargaPDVOPE_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            PanelCargaMasivaAsignapdv.Style.Value = "Display:Block;";

            IframeMasivaPDVOpe.Attributes["src"] = "Carga_PDVOPE.aspx";
        }

        protected void BtnCloseMasivaPDVOPE_Click(object sender, ImageClickEventArgs e)
        {
            InicializarPaneles();
            PanelAsignacionPDVaoper.Style.Value = "Display:Block;";
        }


        protected void BtnAllPDV_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= ChkListPDV.Items.Count - 1; i++)
            {
                ChkListPDV.Items[i].Selected = true;
            }
        }
        protected void BtnNonePDV_Click(object sender, EventArgs e)
        {
            ChkListPDV.ClearSelection();
        }
        protected void BtnAsigPDVOPE_Click(object sender, EventArgs e)
        {
            DataTable dtAsigna_PDV_A_OPE_Temp = (DataTable)this.Session["dtAsigna_PDV_A_OPE_Temp"];

            bool seguir = true;
            if (CmbSelOpePlanning.Text == "0" || TxtF_iniPDVOPE.Text == "" || TxtF_finPDVOPE.Text == "")
            {
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                if (TxtF_finPDVOPE.Text == "")
                {
                    this.Session["mensaje"] = "Es indispensable ingresar la fecha final";
                }
                if (TxtF_iniPDVOPE.Text == "")
                {
                    this.Session["mensaje"] = "Es indispensable ingresar la fecha inicial";
                }
                if (CmbSelOpePlanning.Text == "0")
                {
                    this.Session["mensaje"] = "Es indispensable seleccionar un Operativo";
                }
                Mensajes_Usuario();
                seguir = false;
            }
            if (seguir)
            {
                try
                {
                    DateTime FechaInicial = DateTime.Parse(TxtF_iniPDVOPE.Text);
                    DateTime Fechafinal = DateTime.Parse(TxtF_finPDVOPE.Text);
                    DateTime FechainicialPlanning = DateTime.Parse(this.Session["Fechainicial"].ToString().Trim());
                    DateTime FechafinalPlanning = DateTime.Parse(this.Session["Fechafinal"].ToString().Trim());

                    if (FechaInicial > Fechafinal)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La fecha inicial no puede ser mayor a la fecha final";
                        Mensajes_Usuario();
                        seguir = false;
                    }
                    if (FechaInicial < FechainicialPlanning || Fechafinal > FechafinalPlanning)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "Las fechas deben estar dentro del rango : " + FechainicialPlanning.ToShortDateString() + " y " + FechafinalPlanning.ToShortDateString() + " que corresponden a las fechas de ejecución de la Campaña";
                        Mensajes_Usuario();
                        seguir = false;
                    }
                    if (FechaInicial < DateTime.Today)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La fecha inicial debe ser igual o superior a la fecha actual";
                        Mensajes_Usuario();
                        seguir = false;
                    }

                }
                catch
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Formato de fecha no valido. Por favor verifique (dd/mm/aaaa)";
                    Mensajes_Usuario();
                }
            }

            if (seguir)
            {
                for (int i = 0; i <= ChkListPDV.Items.Count - 1; i++)
                {
                    if (ChkListPDV.Items[i].Selected == true)
                    {
                        DataTable dtconsulta = wsPlanning.Get_AsignacionDuplicadaPDV(Convert.ToInt32(ChkListPDV.Items[i].Value), Convert.ToInt32(CmbSelOpePlanning.SelectedItem.Value), TxtPlanningAsigPDVOPE.Text);
                        if (dtconsulta != null)
                        {
                            if (dtconsulta.Rows.Count == 0)
                            {
                                seguir = true;
                            }
                            // Se elimina funcionalidad : solo debe permitir guardar una vez un punto de venta por operativo sin importar la fecha. 
                            // 24/01/2011 Ing. Mauricio Ortiz 
                            else
                            {
                                // valida rango de fechas 
                                //for (int k = 0; k <= dtconsulta.Rows.Count - 1; k++)
                                //{
                                //    if (Convert.ToDateTime(TxtF_iniPDVOPE.Text) >= Convert.ToDateTime(dtconsulta.Rows[k][3].ToString().Trim()) && Convert.ToDateTime(TxtF_iniPDVOPE.Text) <= Convert.ToDateTime(dtconsulta.Rows[k][4].ToString().Trim()))
                                //    {

                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "El punto de venta : " + ChkListPDV.Items[i].Text + " ya esta asignado al Mercaderista seleccionado. Por favor verifique";
                                Mensajes_Usuario();
                                //k = dtconsulta.Rows.Count - 1;
                                i = ChkListPDV.Items.Count - 1;
                                seguir = false;
                                //    }
                                //    else
                                //    {
                                //        seguir = true;
                                //    }
                                //}
                            }
                        }
                        if (seguir)
                        {
                            #region paso
                            if (GvAsignaPDVOPE.Rows.Count > 0)
                            {
                                for (int j = 0; j <= GvAsignaPDVOPE.Rows.Count - 1; j++)
                                {
                                    if (CmbSelOpePlanning.SelectedItem.Value == GvAsignaPDVOPE.Rows[j].Cells[1].Text &&
                                        ChkListPDV.Items[i].Value == GvAsignaPDVOPE.Rows[j].Cells[3].Text)
                                    //&& (Convert.ToDateTime(TxtF_iniPDVOPE.Text) >= Convert.ToDateTime(GvAsignaPDVOPE.Rows[j].Cells[5].Text) && Convert.ToDateTime(TxtF_iniPDVOPE.Text) <= Convert.ToDateTime(GvAsignaPDVOPE.Rows[j].Cells[6].Text))
                                    {
                                        this.Session["encabemensa"] = "Sr. Usuario";
                                        this.Session["cssclass"] = "MensajesSupervisor";
                                        this.Session["mensaje"] = "El punto de venta : " + ChkListPDV.Items[i].Text + " ya esta asignado al Mercaderista seleccionado. Por favor verifique";
                                        Mensajes_Usuario();
                                        j = GvAsignaPDVOPE.Rows.Count - 1;
                                        i = ChkListPDV.Items.Count - 1;
                                        seguir = false;
                                    }
                                    else
                                    {
                                        seguir = true;
                                    }
                                }
                                if (seguir)
                                {
                                    DataRow dr = dtAsigna_PDV_A_OPE_Temp.NewRow();
                                    dr["Cod_"] = Convert.ToInt32(CmbSelOpePlanning.SelectedItem.Value);
                                    dr["Mercaderista"] = CmbSelOpePlanning.SelectedItem.Text;
                                    dr["Cod."] = ChkListPDV.Items[i].Value;
                                    dr["Punto_de_Venta"] = ChkListPDV.Items[i].Text;
                                    dr["Desde"] = TxtF_iniPDVOPE.Text;
                                    dr["Hasta"] = TxtF_finPDVOPE.Text;
                                    dtAsigna_PDV_A_OPE_Temp.Rows.Add(dr);
                                    this.Session["dtAsigna_PDV_A_OPE_Temp"] = dtAsigna_PDV_A_OPE_Temp;
                                }
                            }
                            else
                            {
                                DataRow dr = dtAsigna_PDV_A_OPE_Temp.NewRow();
                                dr["Cod_"] = Convert.ToInt32(CmbSelOpePlanning.SelectedItem.Value);
                                dr["Mercaderista"] = CmbSelOpePlanning.SelectedItem.Text;
                                dr["Cod."] = ChkListPDV.Items[i].Value;
                                dr["Punto_de_Venta"] = ChkListPDV.Items[i].Text;
                                dr["Desde"] = TxtF_iniPDVOPE.Text;
                                dr["Hasta"] = TxtF_finPDVOPE.Text;
                                dtAsigna_PDV_A_OPE_Temp.Rows.Add(dr);
                                this.Session["dtAsigna_PDV_A_OPE_Temp"] = dtAsigna_PDV_A_OPE_Temp;
                            }
                            #endregion
                        }
                    }
                }
                GvAsignaPDVOPE.DataSource = dtAsigna_PDV_A_OPE_Temp;
                GvAsignaPDVOPE.DataBind();
                ChkListPDV.ClearSelection();
                if (GvAsignaPDVOPE.Rows.Count > 0)
                {
                    BtnSaveAsigPDVOPE.Enabled = true;
                }
                else
                {
                    BtnSaveAsigPDVOPE.Enabled = false;
                }
            }
        }
        protected void GvAsignaPDVOPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GvAsignaPDVOPE.SelectedRow;
            DataTable dtdel = (DataTable)this.Session["dtAsigna_PDV_A_OPE_Temp"];
            dtdel.Rows[row.RowIndex].Delete();
            this.Session["dtAsigna_PDV_A_OPE_Temp"] = dtdel;
            GvAsignaPDVOPE.DataSource = (DataTable)this.Session["dtAsigna_PDV_A_OPE_Temp"];
            GvAsignaPDVOPE.DataBind();
            if (GvAsignaPDVOPE.Rows.Count > 0)
            {
                BtnSaveAsigPDVOPE.Enabled = true;
            }
            else
            {
                BtnSaveAsigPDVOPE.Enabled = false;
            }
        }
        protected void BtnSaveAsigPDVOPE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= GvAsignaPDVOPE.Rows.Count - 1; i++)
            {
                EPointOfSale_PlanningOper RegistrarPointOfSale_PlanningOper = PointOfSale_PlanningOper.RegistrarAsignPDVaOperativo(Convert.ToInt32(GvAsignaPDVOPE.Rows[i].Cells[3].Text), TxtPlanningAsigPDVOPE.Text, Convert.ToInt32(GvAsignaPDVOPE.Rows[i].Cells[1].Text), Convert.ToDateTime(GvAsignaPDVOPE.Rows[i].Cells[5].Text + " 01:00:00.000"), Convert.ToDateTime(GvAsignaPDVOPE.Rows[i].Cells[6].Text + " 23:59:00.000"),0, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                PointOfSale_PlanningOper RegistrarTBL_EQUIPO_PTO_VENTA = new PointOfSale_PlanningOper();
                RegistrarTBL_EQUIPO_PTO_VENTA.RegistrarTBL_EQUIPO_PTO_VENTA(Convert.ToInt32(GvAsignaPDVOPE.Rows[i].Cells[3].Text), TxtPlanningAsigPDVOPE.Text, Convert.ToInt32(GvAsignaPDVOPE.Rows[i].Cells[1].Text), Convert.ToDateTime(GvAsignaPDVOPE.Rows[i].Cells[5].Text + " 01:00:00.000"), Convert.ToDateTime(GvAsignaPDVOPE.Rows[i].Cells[6].Text + " 23:59:00.000"));

            }
            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupConfirm";
            this.Session["mensaje"] = "Se ha asignado con éxito los puntos de venta para la campaña : " + CmbSelPresupuestoAsigPDVOPE.SelectedItem.Text.ToUpper();
            Mensajes_Usuario();
            limpiar_InformacionPDVOPE();
        }
        protected void BtnClearAsigPDVOPE_Click(object sender, EventArgs e)
        {
            limpiar_InformacionPDVOPE();
        }



        #endregion

        #region Informes Campaña
        protected void CmbSelPresupuestoReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnCarga20.Visible = false;
            BtnCarga80.Visible = false;
            DataTable dtAsigna_reportplanning = new DataTable();
            dtAsigna_reportplanning.Columns.Add("Cod_", typeof(Int32));
            dtAsigna_reportplanning.Columns.Add("Informe", typeof(String));
            dtAsigna_reportplanning.Columns.Add("Mes_", typeof(String));
            dtAsigna_reportplanning.Columns.Add("Asignado", typeof(String));
            dtAsigna_reportplanning.Columns.Add("Periodos", typeof(String));
            dtAsigna_reportplanning.Columns.Add("Año", typeof(String));
            GVFrecuencias.DataSource = dtAsigna_reportplanning;
            GVFrecuencias.DataBind();
            this.Session["dtAsigna_reportplanning"] = dtAsigna_reportplanning;

            BtnSaveReportes.Enabled = false;

            if (CmbSelPresupuestoReportes.SelectedValue != "0")
            {
                //ejecutar método para obtener id del planning generado 
                DataTable dt = wsPlanning.ObtenerIdPlanning(CmbSelPresupuestoReportes.SelectedValue);
                try
                {
                    TxtPlanningReportes.Text = dt.Rows[0]["Planning"].ToString().Trim();
                    this.Session["company_id"] = dt.Rows[0]["Company_id"].ToString().Trim();
                    this.Session["Planning_CodChannel"] = dt.Rows[0]["Planning_CodChannel"].ToString().Trim();
                    //llenameses(); esta función de dispara ahora desde la seleccion de un año. Ing. Mauricio Ortiz
                    this.Session["id_planningReportes"] = TxtPlanningReportes.Text;
                    this.Session["Presupuestoreportes"] = CmbSelPresupuestoReportes.SelectedItem.Text;
                    llenaaños();
                    llenainformes();
                    if (dt.Rows[0]["Company_id"].ToString().Trim() == "1562" && dt.Rows[0]["Planning_CodChannel"].ToString().Trim() == "1000")
                    {
                        BtnCarga20.Visible = true;
                        BtnCarga80.Visible = true;
                    }

                }
                catch
                {
                    limpiar_InformacionReportPlanning();

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Este presupuesto corresponde a pruebas y no es válido. seleccione otro por favor";
                    Mensajes_Usuario();
                }
            }
            else
            {
                limpiar_InformacionReportPlanning();
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario seleccionar un presupuesto";
                Mensajes_Usuario();
            }

        }
        protected void BtnAddFrecuencia_Click(object sender, EventArgs e)
        {
            DAplicacion odDuplicado = new DAplicacion();
            DataTable dtAsigna_reportplanning = (DataTable)this.Session["dtAsigna_reportplanning"];

            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupervisor";

            if (RBtnListInformes.SelectedValue == "")
            {
                this.Session["mensaje"] = "Debe Seleccionar un Informe";
                Mensajes_Usuario();
                return;
            }

            if (RbtnListmeses.SelectedValue == "")
           // if (ChkListMeses.SelectedValue == "")
            {
                this.Session["mensaje"] = "Debe Seleccionar el mes";
                Mensajes_Usuario();
                return;
            }

            if (ChklstFrecuencia.SelectedValue == "")
            {
                this.Session["mensaje"] = "Debe Seleccionar el periodo";
                Mensajes_Usuario();
                return;
            }

            try
            {
                bool continuar = true;
                //for (int i = 0; i <= ChkListMeses.Items.Count - 1; i++)
                //{
                    //if (ChkListMeses.Items[i].Selected)
                    //{
                        for (int frecuencias = 0; frecuencias <= ChklstFrecuencia.Items.Count - 1; frecuencias++)
                        {
                            if (ChklstFrecuencia.Items[frecuencias].Selected == true)
                            {
                                for (int valida = 0; valida <= dtAsigna_reportplanning.Rows.Count - 1; valida++)
                                {
                                    if (RBtnListInformes.SelectedItem.Value == dtAsigna_reportplanning.Rows[valida]["Cod_"].ToString().Trim() &&
                                       RbtnListmeses.SelectedValue == dtAsigna_reportplanning.Rows[valida]["Mes_"].ToString().Trim() &&
                                        ChklstFrecuencia.Items[frecuencias].Value == dtAsigna_reportplanning.Rows[valida]["Periodos"].ToString().Trim())
                                    {
                                        this.Session["mensaje"] = "El informe " + RBtnListInformes.SelectedItem.Text + " para el mes " + RbtnListmeses.SelectedItem.Text + " y periodo " + ChklstFrecuencia.Items[frecuencias].Value + " Ya está en la lista de asignaciones";
                                        Mensajes_Usuario();
                                        //i = ChkListMeses.Items.Count - 1;
                                        valida = dtAsigna_reportplanning.Rows.Count - 1;
                                        frecuencias = ChklstFrecuencia.Items.Count - 1;
                                        continuar = false;
                                    }
                                }
                            }
                        }
                    //}
                //}

                if (continuar)
                {
                    //for (int i = 0; i <= ChkListMeses.Items.Count - 1; i++)
                    //{
                        //if (ChkListMeses.Items[i].Selected)
                        //{
                            for (int frecuencias = 0; frecuencias <= ChklstFrecuencia.Items.Count - 1; frecuencias++)
                            {
                                if (ChklstFrecuencia.Items[frecuencias].Selected == true)
                                {
                                    DataTable dtDuplicado = odDuplicado.ConsultaDuplicadoReportPlanning(ConfigurationManager.AppSettings["Reports_Planning"], TxtPlanningReportes.Text, RBtnListInformes.SelectedItem.Value,RbtnListmeses.SelectedValue, ChklstFrecuencia.Items[frecuencias].Value);
                                    if (dtDuplicado != null)
                                    {
                                        this.Session["mensaje"] = "El informe " + RBtnListInformes.SelectedItem.Text + " para el mes " + RbtnListmeses.SelectedItem.Text + " y periodo " + ChklstFrecuencia.Items[frecuencias].Value + " Ya esta creado en la base de datos";
                                        Mensajes_Usuario();
                                        //i = ChkListMeses.Items.Count - 1;
                                        frecuencias = ChklstFrecuencia.Items.Count - 1;
                                        continuar = false;
                                    }
                                }
                            }
                        //}
                    //}

                    if (continuar)
                    {
                        //for (int i = 0; i <= ChkListMeses.Items.Count - 1; i++)
                        //{
                            //if (ChkListMeses.Items[i].Selected)
                            //{
                                for (int frecuencias = 0; frecuencias <= ChklstFrecuencia.Items.Count - 1; frecuencias++)
                                {
                                    if (ChklstFrecuencia.Items[frecuencias].Selected == true)
                                    {
                                        DataRow dr = dtAsigna_reportplanning.NewRow();
                                        dr["Cod_"] = Convert.ToInt32(RBtnListInformes.SelectedItem.Value);
                                        dr["Informe"] = RBtnListInformes.SelectedItem.Text;
                                        dr["Mes_"] = RbtnListmeses.SelectedValue;
                                        dr["Asignado"] = RbtnListmeses.SelectedItem.Text;
                                        dr["Periodos"] = ChklstFrecuencia.Items[frecuencias].Value;
                                        dr["Año"] = CmbSelAñoCampaña.SelectedItem.Text;
                                        dtAsigna_reportplanning.Rows.Add(dr);
                                        this.Session["dtAsigna_reportplanning"] = dtAsigna_reportplanning;
                                    }
                                }
                            //}
                        //}
                        GVFrecuencias.DataSource = dtAsigna_reportplanning;
                        GVFrecuencias.DataBind();

                        RBtnListInformes.SelectedIndex = -1;
                        // ChkListMeses.SelectedIndex = -1;
                        RbtnListmeses.SelectedIndex = -1;

                        ChklstFrecuencia.Items.Clear();

                        if (GVFrecuencias.Rows.Count > 0)
                        {
                            BtnSaveReportes.Enabled = true;
                        }
                        else
                        {
                            BtnSaveReportes.Enabled = false;
                        }
                    }
                }
            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

        }
        protected void GVFrecuencias_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                GridViewRow row = GVFrecuencias.SelectedRow;
                DataTable dtdel = (DataTable)this.Session["dtAsigna_reportplanning"];
                dtdel.Rows[row.RowIndex].Delete();
                this.Session["dtAsigna_reportplanning"] = dtdel;

                GVFrecuencias.DataSource = (DataTable)this.Session["dtAsigna_reportplanning"];
                GVFrecuencias.DataBind();


                if (GVFrecuencias.Rows.Count > 0)
                {
                    BtnSaveReportes.Enabled = true;
                }
                else
                {
                    BtnSaveReportes.Enabled = false;
                }
            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        protected void BtnSaveReportes_Click(object sender, EventArgs e)
        {
            bool seguir = true;
            for (int i = 0; i <= GVFrecuencias.Rows.Count - 1; i++)
            {
                if (((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechaini")).Text == "" ||
                    ((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechafin")).Text == "")
                {
                    seguir = false;
                    i = GVFrecuencias.Rows.Count;
                }

                if (seguir)
                {
                    try
                    {
                        DateTime FechaInicial = Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechaini")).Text + " 01:00:00.000");
                        DateTime Fechafinal = Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000");
                        if (FechaInicial > Fechafinal)
                        {
                            this.Session["mensaje"] = "La fecha inicial no puede ser mayor a la fecha final.";
                            Mensajes_Usuario();
                            seguir = false;
                            i = GVFrecuencias.Rows.Count;
                            return;
                        }

                        if (seguir)
                        {
                            if (Convert.ToString(FechaInicial.Year) != GVFrecuencias.Rows[i].Cells[5].Text ||
                                Convert.ToString(Fechafinal.Year) != GVFrecuencias.Rows[i].Cells[5].Text )
                            //|| Convert.ToInt32(Fechafinal.Month) != Convert.ToInt32(GVFrecuencias.Rows[i].Cells[3].Text)) -- Comentado para habilitar final de periodo en el mes siguiente
                            // || Convert.ToInt32(FechaInicial.Month) != Convert.ToInt32(GVFrecuencias.Rows[i].Cells[3].Text) )
                            {
                                this.Session["mensaje"] = "El reporte : " + GVFrecuencias.Rows[i].Cells[2].Text +
                                    " es de " + GVFrecuencias.Rows[i].Cells[4].Text + " de " +
                                     GVFrecuencias.Rows[i].Cells[5].Text + ". deben ser del mismo año. Por favor verifique";

                                Mensajes_Usuario();
                                seguir = false;
                                i = GVFrecuencias.Rows.Count;
                                return;
                            }
                            if (seguir)
                            {
                                int cuenta = 0;
                                for (int k = 0; k <= GVFrecuencias.Rows.Count - 1; k++)
                                {


                                    //if (GVFrecuencias.Rows[k].Cells[1].Text == GVFrecuencias.Rows[i].Cells[1].Text &&
                                    //    GVFrecuencias.Rows[k].Cells[3].Text == GVFrecuencias.Rows[i].Cells[3].Text)
                                    //{
                                    if (Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[k].Cells[0].FindControl("TxtFechaini")).Text + " 01:00:00.000") >= Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechaini")).Text + " 01:00:00.000")
                                            && Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[k].Cells[0].FindControl("TxtFechaini")).Text + " 01:00:00.000") <= Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000")
                                             )
                                    {
                                        cuenta = cuenta + 1;
                                    }
                                    if (cuenta > 1)
                                    {

                                        this.Session["mensaje"] = "Los rangos de fechas por reporte y periodo no deben coincidir. Por favor verifique";
                                        Mensajes_Usuario();
                                        seguir = false;
                                        k = GVFrecuencias.Rows.Count;
                                        i = GVFrecuencias.Rows.Count;
                                        return;
                                    }

                                    //}

                                }
                            }
                        }
                    }
                    catch
                    {
                        this.Session["mensaje"] = "Debe incluir todas las fechas con formato (dd/mm/aaaa).";
                        Mensajes_Usuario();
                        seguir = false;
                        i = GVFrecuencias.Rows.Count;
                        return;
                    }
                }

            }
            if (seguir)
            {

                DAplicacion odDuplicado = new DAplicacion();
                for (int j = 0; j <= GVFrecuencias.Rows.Count - 1; j++)
                {
                    DataTable dtvalidarango = odDuplicado.ConsultaRangos(TxtPlanningReportes.Text, Convert.ToInt32(GVFrecuencias.Rows[j].Cells[1].Text),
                        Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[j].Cells[0].FindControl("TxtFechaini")).Text + " 01:00:00.000"),
                        Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[j].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000"),
                        Convert.ToInt32(GVFrecuencias.Rows[j].Cells[5].Text));
                    if (dtvalidarango.Rows.Count > 0)
                    {
                        seguir = false;
                        j = GVFrecuencias.Rows.Count;
                        this.Session["mensaje"] = "Ya existe un periodo para ese reporte el cual contiene el Rango de fechas ingresado.";
                        Mensajes_Usuario();
                        return;
                    }
                }

                if (seguir)
                {
                    for (int i = 0; i <= GVFrecuencias.Rows.Count - 1; i++)
                    {
                        DataTable dtDuplicado = odDuplicado.ConsultaDuplicadoReportPlanning(ConfigurationManager.AppSettings["Reports_Planning"], TxtPlanningReportes.Text, GVFrecuencias.Rows[i].Cells[1].Text, GVFrecuencias.Rows[i].Cells[3].Text, GVFrecuencias.Rows[i].Cells[5].Text);
                        if (dtDuplicado == null)
                        {
                            wsPlanning.Get_InsertaReportesPlanning(TxtPlanningReportes.Text, Convert.ToInt32(GVFrecuencias.Rows[i].Cells[1].Text), Convert.ToInt32(GVFrecuencias.Rows[i].Cells[5].Text), GVFrecuencias.Rows[i].Cells[3].Text, Convert.ToInt32(GVFrecuencias.Rows[i].Cells[6].Text), Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechaini")).Text + " 01:00:00.000"), Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000"), true, this.Session["sUser"].ToString(), DateTime.Now, this.Session["sUser"].ToString(), DateTime.Now);
                        }
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se ha creado con éxito los Informes de la campaña : " + CmbSelPresupuestoReportes.SelectedItem.Text.ToUpper();
                    Mensajes_Usuario();
                    limpiar_InformacionReportPlanning();
                }
            }
            else
            {
                this.Session["mensaje"] = "Ingrese las fechas de inicio y fin por cada asignación.";
                Mensajes_Usuario();
            }
        }
        protected void BtnClearReportes_Click(object sender, EventArgs e)
        {
            limpiar_InformacionReportPlanning();
        }
        protected void CmbSelAñoCampaña_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbSelAñoCampaña.Text != "0")
            {
                llenameses();
            }
            else
            {
                RbtnListmeses.Items.Clear();
                ChklstFrecuencia.Items.Clear();
                //ChkListMeses.Items.Clear();
            }
        }
        protected void BtnCarga20_Click(object sender, ImageClickEventArgs e)
        {
            InicializarPaneles();
            this.Session["2080"] = "20";
            PanelCarga2080.Style.Value = "Display:Block;";
            IframeMasiva2080.Attributes["src"] = "Carga2080.aspx";
        }
        protected void BtnCarga80_Click(object sender, ImageClickEventArgs e)
        {

            InicializarPaneles();
            this.Session["2080"] = "80";
            PanelCarga2080.Style.Value = "Display:Block;";
            IframeMasiva2080.Attributes["src"] = "Carga2080.aspx";
        }
        protected void BtnCloseMasiva2080_Click(object sender, ImageClickEventArgs e)
        {
            InicializarPaneles();
            PanelReportesCampaña.Style.Value = "Display:Block;";
        }
        protected void RbtnListmeses_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaPeriodos(Convert.ToInt32(CmbSelAñoCampaña.SelectedItem.Text), Convert.ToInt32(RbtnListmeses.SelectedValue));
        }
        protected void BtnAllPer_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= ChklstFrecuencia.Items.Count - 1; i++)
            {
                ChklstFrecuencia.Items[i].Selected = true;
            }
        }
        protected void BtnNonePer_Click(object sender, EventArgs e)
        {
            ChklstFrecuencia.ClearSelection();
        }
        #endregion


        #region paneles punto de venta

        protected void btnPanelPtoVenta_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            //limpiar_InformacionReportPlanning();
            panelPuntoVenta.Style.Value = "Display:Block;";
            StiloBotonSeleccionado();
            btnPanelPtoVenta.CssClass = "buttonNewPlanSel";
            Habilitabotones();
            btnPanelPtoVenta.Enabled = false;
            LlenaPresupuestosAsignados();
        }
        private void llenainformesPanelPtoVenta()
        {
            try
            {
                DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CONSULTA_INFORMESYMESES_PLANNING", TxtPlanningPanelPuntoVenta.Text, Convert.ToInt32(this.Session["company_id"]), this.Session["Planning_CodChannel"].ToString().Trim(), 0);
                RBtnListPanelPtoVenta.DataSource = ds.Tables[1];
                RBtnListPanelPtoVenta.DataValueField = "Report_id";
                RBtnListPanelPtoVenta.DataTextField = "Report_NameReport";
                RBtnListPanelPtoVenta.DataBind();
                RBtnListPanelPtoVenta.Items.Insert(0, new ListItem("--Selecione--", "0"));
                ds = null;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        protected void CmbSelPresupuestoPanelesPtoVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnCarga20.Visible = false;
            BtnCarga80.Visible = false;
            DataTable dtAsigna_reportplanning = new DataTable();
            dtAsigna_reportplanning.Columns.Add("Cod_", typeof(Int32));
            dtAsigna_reportplanning.Columns.Add("Informe", typeof(String));
            dtAsigna_reportplanning.Columns.Add("Mes_", typeof(String));
            dtAsigna_reportplanning.Columns.Add("Asignado", typeof(String));
            dtAsigna_reportplanning.Columns.Add("Periodos", typeof(String));
            dtAsigna_reportplanning.Columns.Add("Año", typeof(String));
            GVFrecuencias.DataSource = dtAsigna_reportplanning;
            GVFrecuencias.DataBind();
            this.Session["dtAsigna_reportplanning"] = dtAsigna_reportplanning;

            BtnSaveReportes.Enabled = false;

            if (CmbSelPresupuestoPanelesPtoVenta.SelectedValue != "0")
            {
                //ejecutar método para obtener id del planning generado 
                DataTable dt = wsPlanning.ObtenerIdPlanning(CmbSelPresupuestoPanelesPtoVenta.SelectedValue);
                try
                {
                    TxtPlanningPanelPuntoVenta.Text = dt.Rows[0]["Planning"].ToString().Trim();
                    this.Session["Planning"] = TxtPlanningPanelPuntoVenta.Text;
                    this.Session["company_id"] = dt.Rows[0]["Company_id"].ToString().Trim();
                    this.Session["Planning_CodChannel"] = dt.Rows[0]["Planning_CodChannel"].ToString().Trim();
                    //llenameses(); esta función de dispara ahora desde la seleccion de un año. Ing. Mauricio Ortiz
                    this.Session["id_planningReportes"] = TxtPlanningReportes.Text;
                    this.Session["Presupuestoreportes"] = CmbSelPresupuestoPanelesPtoVenta.SelectedItem.Text;
                    llenainformesPanelPtoVenta();
                    llenar_Años();
                    llenar_Meses();
                    if (dt.Rows[0]["Company_id"].ToString().Trim() == "1562" && dt.Rows[0]["Planning_CodChannel"].ToString().Trim() == "1000")
                    {
                        BtnCarga20.Visible = true;
                        BtnCarga80.Visible = true;
                    }

                }
                catch
                {
                    limpiar_InformacionReportPlanning();

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Este presupuesto corresponde a pruebas y no es válido. seleccione otro por favor";
                    Mensajes_Usuario();
                }
            }
            else
            {
                limpiar_InformacionReportPlanning();
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario seleccionar un presupuesto";
                Mensajes_Usuario();
            }

        }

        private void llenaPtoVenta()
        {
            DataTable dtpdvplanning = null;
            dtpdvplanning = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTARPDVPLANNING1", TxtPlanningPanelPuntoVenta.Text);

            if (dtpdvplanning != null)
            {
                if (dtpdvplanning.Rows.Count > 0)
                {
                    ddlPanelPtoventa.DataSource = dtpdvplanning;
                    ddlPanelPtoventa.DataValueField = "Código";
                    ddlPanelPtoventa.DataTextField = "Nombre";
                    ddlPanelPtoventa.DataBind();
                    ddlPanelPtoventa.Items.Insert(0, new ListItem("--Selecione--", "0"));
                    dtpdvplanning = null;

                    LblSelRapida.Visible = true;
                    TxtCodigoPDV.Visible = true;
                    TxtCodigoPDV.Text = "";
                    ImgSelRapida.Visible = true;
                }
                else
                {
                    ddlPanelPtoventa.DataSource = null;
                    ddlPanelPtoventa.DataBind();
                }
            }
        }

        protected void RBtnListPanelPtoVenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaPtoVenta();
            llenaPanelptoVenta_cattegoria();

            int id_company = Convert.ToInt32(this.Session["company_id"].ToString().Trim());
            string cod_channel = this.Session["Planning_CodChannel"].ToString().Trim();
            int id_report = Convert.ToInt32(RBtnListPanelPtoVenta.SelectedValue);

            Session["id_report"] = id_report;

            DPlanning dplanning = new DPlanning();
            DataTable vistas = dplanning.ValidaTipoGestion(id_company, cod_channel, id_report); //obtiene los valores booleanos para las vistas de paneles
            this.Session["vistas"] = vistas;

            DataTable tipo_reporte = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_TIPO_REPORTE_LISTAR", id_company, id_report); //obtiene lista de subreportes en caso las tenga

            if (tipo_reporte.Rows.Count > 0) //si tiene subreportes
            {
                divselproductos.Style.Value = "display:none;";
                div_masopciones1.Style.Value = "display:block;";
                RbtMasopciones1.DataSource = vistas;
                RbtMasopciones1.DataTextField = "TipoReporte_Descripcion";
                RbtMasopciones1.DataValueField = "id_Tipo_Reporte";
                RbtMasopciones1.DataBind();
            }
            else // si no tiene sub_reportes
            {
                CargarOpciones1();
            }

            BtnMasivoPanelPtoVenta.Enabled = true;
            
            

        }

        void llenaPanelptoVenta_cattegoria()
        {
            //DataTable dtcatego = wsPlanning.Get_ObtenerCategoriasPlanning(Convert.ToInt32(this.Session["company_id"].ToString()));

            //ddlPanelPtoventa_Categoria.DataSource = dtcatego;
            //ddlPanelPtoventa_Categoria.DataValueField = "id_ProductCategory";
            //ddlPanelPtoventa_Categoria.DataTextField = "Product_Category";
            //ddlPanelPtoventa_Categoria.DataBind();

            //dtcatego = null;

        }

        void llenaPanelPtoVenta_Marca()
        {
            //DataTable dtmarca = wsPlanning.Get_ObtenerMarcas(Convert.ToInt32(ddlPanelPtoventa_Categoria.SelectedValue), Convert.ToInt32(this.Session["company_id"].ToString()));

            //ddlPanelPtoventa_Marca.DataSource = dtmarca;
            //ddlPanelPtoventa_Marca.DataValueField = "id_Brand";
            //ddlPanelPtoventa_Marca.DataTextField = "Name_Brand";
            //ddlPanelPtoventa_Marca.DataBind();
            //dtmarca = null;

        }

        protected void ddlPanelPtoventa_Categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaPanelPtoVenta_Marca();
        }

        void llenaPanelPtoVenta_Familia()
        {
            //DataTable dtFamilias = wsPlanning.Get_Obtener_Familias(Convert.ToInt32(ddlPanelPtoventa_Marca.SelectedValue), 0, Convert.ToInt32(this.Session["company_id"].ToString()));

            //ddlPanelPtoventa_Familia.DataSource = dtFamilias;
            //ddlPanelPtoventa_Familia.DataTextField = "name_Family";
            //ddlPanelPtoventa_Familia.DataValueField = "id_ProductFamily";
            //ddlPanelPtoventa_Familia.DataBind();
        }

        protected void ddlPanelPtoventa_Marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaPanelPtoVenta_Familia();
        }


        void llenaPanelPtoVenta_Produto()
        {
            //DataTable dtproducto = wsPlanning.Get_Obtener_Productos(ddlPanelPtoventa_Categoria.SelectedValue, ddlPanelPtoventa_Marca.SelectedValue, ddlPanelPtoventa_Familia.SelectedValue, "0", Convert.ToInt32(this.Session["company_id"].ToString()));


            //chkPanelPtoVenta_Produtos.DataSource = dtproducto;
            //chkPanelPtoVenta_Produtos.DataValueField = "id_product";
            //chkPanelPtoVenta_Produtos.DataTextField = "Product_Name";
            //chkPanelPtoVenta_Produtos.DataBind();
            
        }

        protected void ddlPanelPtoventa_Familia_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaPanelPtoVenta_Produto();
        }

        private void llenar_Años()
        {

            DataTable dty = null;
            dty = Get_Administrativo.Get_ObtenerYears();
            if (dty.Rows.Count > 0)
            {
                ddlPanelPtoventa_Año.DataSource = dty;
                ddlPanelPtoventa_Año.DataValueField = "Years_Number";
                ddlPanelPtoventa_Año.DataTextField = "Years_Number";
                ddlPanelPtoventa_Año.DataBind();
            }
            else
            {

                dty = null;

            }
        }

        private void llenar_Meses()
        {
            DataTable dtm = Get_Administrativo.Get_ObtenerMeses();

            if (dtm.Rows.Count > 0)
            {
                ddlPanelPtoventa_Mes.DataSource = dtm;
                ddlPanelPtoventa_Mes.DataValueField = "codmes";
                ddlPanelPtoventa_Mes.DataTextField = "namemes";
                ddlPanelPtoventa_Mes.DataBind();
                ddlPanelPtoventa_Mes.Items.Insert(0, new ListItem("--Seleccione--", "0"));

            }
            else
            {
                dtm = null;

            }

        }

        private void llenar_Periodos()
        {
            int Report;
            string canal;
            DataTable dtp = null;
            Report = Convert.ToInt32(RBtnListPanelPtoVenta.SelectedValue);
            canal = this.Session["Planning_CodChannel"].ToString().Trim();
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS_2", canal, this.Session["company_id"].ToString(), Report, ddlPanelPtoventa_Mes.SelectedValue, ddlPanelPtoventa_Año.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                ddlPanelPtoventa_Periodo.DataSource = dtp;
                ddlPanelPtoventa_Periodo.DataValueField = "id_periodo";
                ddlPanelPtoventa_Periodo.DataTextField = "Periodo";
                ddlPanelPtoventa_Periodo.DataBind();

            }
            else
            {
                dtp = null;
            }
        }

        protected void ddlPanelPtoventa_Mes_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenar_Periodos();
        }


        private void cargarlistas1()
        {
            bool[] s_vistas = (bool[])this.Session["s_vistas"];
            string vistafinal = this.Session["vista_final"].ToString();

            int company_id = 0;
            int id_categoria = 0;

                company_id = Convert.ToInt32(this.Session["company_id"].ToString().Trim());



            #region Lista Categorías
            if (s_vistas[0] && level_carga == 0)//Si categorias esta habilitado
            {
                DataTable dtcatego = wsPlanning.Get_ObtenerCategoriasPlanning(company_id);
                if (this.Session["vista_final"].ToString().Trim().Equals("Categoria")) // verifica si la vista final es categoría
                {
                    BtnSaveProd.Enabled = true;
                    Chklistcatego1.DataSource = dtcatego;
                    Chklistcatego1.DataValueField = "id_ProductCategory";
                    Chklistcatego1.DataTextField = "Product_Category";
                    Chklistcatego1.DataBind();
                    rbliscatego1.Items.Clear();
                }
                else
                {
                    rbliscatego1.DataSource = dtcatego;
                    rbliscatego1.DataValueField = "id_ProductCategory";
                    rbliscatego1.DataTextField = "Product_Category";
                    rbliscatego1.DataBind();
                    Chklistcatego1.Items.Clear();
                }
            }
            #endregion

            try
            {
                id_categoria = Convert.ToInt32(rbliscatego1.SelectedValue);
            }
            catch (Exception ex)
            {
                id_categoria = 0;
            }

            #region Lista Marcas
            if (s_vistas[1] && level_carga != 2)//Si marcas esta habilitado
            {
                DataTable dtmarca = wsPlanning.Get_ObtenerMarcas(id_categoria, company_id);

                if (dtmarca.Rows.Count > 0)
                {
                    if (!vistafinal.Equals("Marca"))//si no es la vista final
                    {
                        rblmarca1.DataSource = dtmarca;
                        rblmarca1.DataValueField = "id_Brand";
                        rblmarca1.DataTextField = "Name_Brand";
                        rblmarca1.DataBind();
                        Chklistmarca1.Items.Clear();
                    }
                    else
                    {
                        Chklistmarca1.DataSource = dtmarca;
                        Chklistmarca1.DataValueField = "id_Brand";
                        Chklistmarca1.DataTextField = "Name_Brand";
                        Chklistmarca1.DataBind();
                        rblmarca1.Items.Clear();
                    }
                }
                dtmarca = null;
            }
            #endregion

            #region Lista SubMarcas
            if (s_vistas[2] && level_carga != 3)//Si submarcas esta habilitado
            {
                int id_marca = 0;

                if (rblmarca1.Items.Count > 0 && rblmarca1.SelectedIndex != -1)// si existen marcas y se seleccionó alguna
                    id_marca = Convert.ToInt32(rblmarca1.SelectedValue);

                DataTable dtsubmarca = wsPlanning.Get_ObtenerSubMarcas(id_categoria.ToString(), id_marca, company_id);
                if (rblsubmarca1.Items.Count > 0)
                {
                    if (!vistafinal.Equals("SubMarca"))
                    {
                        rblsubmarca1.DataSource = dtsubmarca;
                        rblsubmarca1.DataValueField = "cod_smarca";
                        rblsubmarca1.DataTextField = "name_marca";
                        rblsubmarca1.DataBind();
                    }
                }
                dtsubmarca = null;
            }
            #endregion

            #region Lista Familias
            if (s_vistas[3] && level_carga != 4)//si familias está habilitado
            {
                int id_marca = 0;
                int id_submarca = 0;

                if (rblmarca1.Items.Count != 0 && rblmarca1.SelectedIndex != -1)//si existen marcas y se ha seleccionado alguna
                    id_marca = Convert.ToInt32(rblmarca1.SelectedValue);
                if (rblsubmarca1.Items.Count != 0 && rblsubmarca1.SelectedIndex != -1)//si existen submarcas y se ha seleccionado alguna
                    id_submarca = Convert.ToInt32(rblsubmarca1.SelectedValue);

                DataTable dtFamilias = wsPlanning.Get_Obtener_Familias(id_marca, id_submarca, company_id);

                if (!vistafinal.Equals("Familia"))
                {
                    if (dtFamilias != null)
                    {
                        if (dtFamilias.Rows.Count > 0)
                        {
                            rblfamilia1.DataSource = dtFamilias;
                            rblfamilia1.DataTextField = "name_Family";
                            rblfamilia1.DataValueField = "id_ProductFamily";
                            rblfamilia1.DataBind();
                        }
                        else
                        {
                            this.Session["encabemensa"] = "Señor Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existen familias para la marca seleccionada.";
                            Mensajes_Usuario();
                        }
                    }
                }
                else
                {
                    if (dtFamilias != null)
                    {
                        if (dtFamilias.Rows.Count > 0)
                        {
                            ChkListFamilias1.DataSource = dtFamilias;
                            ChkListFamilias1.DataTextField = "name_Family";
                            ChkListFamilias1.DataValueField = "id_ProductFamily";
                            ChkListFamilias1.DataBind();
                        }
                        else
                        {
                            this.Session["encabemensa"] = "Señor Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existen familias para la marca seleccionada.";
                            Mensajes_Usuario();
                        }
                    }
                }
                dtFamilias = null;
            }
            #endregion

            #region Lista SubFamilias
            if (s_vistas[4] && level_carga != 5)//si subfamilias está habilitado
            {
                string id_familia = "0";

                if (rblfamilia1.Items.Count > 0 && rblfamilia1.SelectedIndex != -1)// si existen familias y se seleccionó alguna.
                    id_familia = rblfamilia1.SelectedValue;

                DataTable dtSubFamilias = wsPlanning.Get_Obtener_SubFamilias(id_familia, company_id);

                if (!vistafinal.Equals("Familia"))
                {
                    if (dtSubFamilias != null)
                    {
                        if (dtSubFamilias.Rows.Count > 0)
                        {
                            rblsubfamilia1.DataSource = dtSubFamilias;
                            rblsubfamilia1.DataTextField = "name_Family";
                            rblsubfamilia1.DataValueField = "id_ProductFamily";
                            rblsubfamilia1.DataBind();
                        }
                        else
                        {
                            this.Session["encabemensa"] = "Señor Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existen familias para la marca seleccionada.";
                            Mensajes_Usuario();
                        }
                    }
                }
                else
                {
                    if (dtSubFamilias != null)
                    {
                        if (dtSubFamilias.Rows.Count > 0)
                        {
                            ChkListSubFamilias1.DataSource = dtSubFamilias;
                            ChkListSubFamilias1.DataTextField = "name_Family";
                            ChkListSubFamilias1.DataValueField = "id_ProductFamily";
                            ChkListSubFamilias1.DataBind();
                        }
                        else
                        {
                            this.Session["encabemensa"] = "Señor Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existen familias para la marca seleccionada.";
                            Mensajes_Usuario();
                        }
                    }
                }
                dtSubFamilias = null;
            }
            #endregion

            #region Lista Productos
            if (s_vistas[5])//si productos está habilitado
            {
                int id_marca = 0;
                string id_familia = "0";
                string id_subfamilia = "0";
                
                if (rblmarca1.Items.Count != 0 && rblmarca1.SelectedIndex != -1)// si existen marcas y se ha seleccionado alguna
                    id_marca = Convert.ToInt32(rblmarca1.SelectedValue);

                if (rblfamilia1.Items.Count > 0 && rblfamilia1.SelectedIndex != -1)// si existen familias y se seleccionó alguna.
                    id_familia = rblfamilia1.SelectedValue;

                if (rblsubfamilia1.Items.Count > 0 && rblsubfamilia1.SelectedIndex != -1)// si existen subfamilias y se seleccionó alguna.
                    id_subfamilia = rblsubfamilia1.SelectedValue;

                DataTable dtproducto = wsPlanning.Get_Obtener_Productos(id_categoria.ToString(), id_marca.ToString(), id_familia, id_subfamilia, company_id);

                if (dtproducto != null)
                {
                    if (dtproducto.Rows.Count > 0)
                    {
                        ChkProductos1.DataSource = dtproducto;
                        ChkProductos1.DataValueField = "cod_Product";
                        ChkProductos1.DataTextField = "Product_Name";
                        ChkProductos1.DataBind();
                    }
                    else
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No existen productos o ya todos fueron agregados a la campaña.";
                        Mensajes_Usuario();
                    }
                }
                dtproducto = null;
            }
            #endregion

            level_carga = 0;

            BtnSavePanelPtoVenta.Enabled = true;
            
        }


        void CargarOpciones1()
        {
          

           DataTable vistas = ((DataTable)this.Session["vistas"]);
           string rep = Session["id_report"].ToString();
            string tipo_rep =RbtMasopciones1.SelectedValue;
            this.Session["tipo_rep"] = tipo_rep;

            int row = 0;
            if (!tipo_rep.Equals(""))
            {
                if (rep.Equals("23"))//FOTOGRAFICO
                {
                    if (RbtMasopciones1.Text.Equals("02"))//tipo exhib. visib.
                        row = 1;
                }
                else if (rep.Equals("57"))//FOTOGRAFICO
                {
                    if (RbtMasopciones1.Text.Equals("02"))//tipo exhib. visib.
                        row = 1;
                }
                else if (rep.Equals("58"))//PRESENCIA
                {
                    if (RbtMasopciones1.Text.Equals("04"))//tipo exhib. visib.
                        row = 1;
                    if (RbtMasopciones1.Text.Equals("05"))//tipo exhib. visib.
                        row = 2;
                    if (RbtMasopciones1.Text.Equals("06"))//tipo exhib. visib.
                        row = 3;
                    if (RbtMasopciones1.Text.Equals("07"))//tipo exhib. visib.
                        row = 4;
                    if (RbtMasopciones1.Text.Equals("08"))//tipo exhib. visib.
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

                    divselproductos.Style.Value = "display:block;";
                    div_masopciones.Style.Value = "display:none;";
                    preparacontroles(vista_final);
                    showpanels1();

                    limpiarlistas1();
                    cargarlistas1();
                }
                else
                {
                    divselproductos.Style.Value = "display:none;";
                    div_masopciones.Style.Value = "display:none;";
                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "No existe parametrización de esta vista. Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente.";
                    Mensajes_UsuarioValidacionVistas();
                }
            }
            else
            {
                divselproductos.Style.Value = "display:none;";
                div_masopciones.Style.Value = "display:none;";
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "No existe parametrización de esta vista. Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente.";
                Mensajes_UsuarioValidacionVistas();
            }
        
        }


        private void showpanels1()
        {
            try
            {
                bool[] s_vistas = new bool[6];
                s_vistas = (bool[])(this.Session["s_vistas"]);
                categorias1.Visible = s_vistas[0];
                Marcas1.Visible = s_vistas[1];
                submarcas1.Visible = s_vistas[2];
                Familias1.Visible = s_vistas[3];
                SubFamilias1.Visible = s_vistas[4];
                productos1.Visible = s_vistas[5];
            }
            catch (Exception ex)
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "No existe parametrización de esta vista. Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente.";
                Mensajes_UsuarioValidacionVistas();
            }
        }



        private void limpiarlistas1()
        {
            rbliscatego1.Items.Clear();
            Chklistcatego1.Items.Clear();
            rblmarca1.Items.Clear();
            Chklistmarca1.Items.Clear();
            rblsubmarca1.Items.Clear();
            Chklistmarca1.Items.Clear();
            rblfamilia1.Items.Clear();
            ChkListFamilias1.Items.Clear();
            rblsubfamilia1.Items.Clear();
            ChkListSubFamilias1.Items.Clear();
            ChkProductos1.Items.Clear();
        }


        protected void rbliscatego1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BtnSavePanelPtoVenta.Enabled = true;
                Chklistmarca1.Items.Clear();
                rblmarca1.Items.Clear();
                rblsubmarca1.Items.Clear();
                ChkListFamilias1.Items.Clear();
                rblfamilia1.Items.Clear();
                ChkListSubFamilias1.Items.Clear();
                rblsubfamilia1.Items.Clear();
                ChkProductos1.Items.Clear();
                level_carga = 1;
                cargarlistas1();
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void rblmarca1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                rblsubmarca1.Items.Clear();
                ChkListFamilias1.Items.Clear();
                rblfamilia1.Items.Clear();
                ChkListSubFamilias1.Items.Clear();
                rblsubfamilia1.Items.Clear();
                ChkProductos1.Items.Clear();
                //Se llena lisbox de productos filtrados por marcas 
                level_carga = 2;
                cargarlistas1();
            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }


        protected void rblsubmarca1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChkListFamilias1.Items.Clear();
                rblfamilia1.Items.Clear();
                ChkListSubFamilias1.Items.Clear();
                rblsubfamilia1.Items.Clear();
                ChkProductos1.Items.Clear();
                level_carga = 3;
                cargarlistas1();
            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }


        protected void rblfamilia1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChkListSubFamilias1.Items.Clear();
                rblsubfamilia1.Items.Clear();
                ChkProductos1.Items.Clear();
                level_carga = 4;
                cargarlistas1();
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }


        protected void rblsubfamilia1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChkProductos1.Items.Clear();
                level_carga = 5;
                cargarlistas1();
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }


        protected void RbtMasopciones1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LblReporteAsociado.Text = LblReporteAsociado.Text + "_" + RbtMasopciones.SelectedItem.Text;
            CargarOpciones1();
            divselproductos.Style.Value = "display:block;";
            div_masopciones1.Style.Value = "display:none;";

        }

        protected void BtnSavePanelPtoVenta_Click(object sender, EventArgs e)
        {
            Conexion cn = new Conexion();
            try
            {
                DataTable dt;
                string vista_final = this.Session["vista_final"].ToString();

                if (ddlPanelPtoventa_Periodo.SelectedValue != null)
                {




                    if (this.Session["vista_final"].ToString() == "Producto")
                    {
                        foreach (ListItem producto in ChkProductos1.Items)
                        {
                            if (producto.Selected)
                            {
                                dt = cn.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAR_PANEL_PTOVENTA", ddlPanelPtoventa.SelectedValue, RBtnListPanelPtoVenta.SelectedItem.Value, RbtMasopciones1.SelectedValue, ddlPanelPtoventa_Periodo.SelectedValue, producto.Value, "", "", "", TxtPlanningPanelPuntoVenta.Text);

                                if (dt.Rows.Count == 0)
                                {
                                    cn.ejecutarDataTable("UP_WEB_REGISTERPANELPUNTOVENTA", TxtPlanningPanelPuntoVenta.Text, ddlPanelPtoventa.SelectedValue, RBtnListPanelPtoVenta.SelectedItem.Value, RbtMasopciones1.SelectedValue, ddlPanelPtoventa_Periodo.SelectedValue, producto.Value, "", "", "", true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                }
                                else
                                {
                                    this.Session["encabemensa"] = "Señor Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "El producto " + producto.Value + " Ya ha sido agregado a la campaña.";
                                    Mensajes_Usuario();
                                    return;
                                }

                            }
                        }

                    }
                    if (this.Session["vista_final"].ToString() == "Marca")
                    {
                        foreach (ListItem Marca in ChkProductos1.Items)
                        {
                            if (Marca.Selected)
                            {
                                dt = cn.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAR_PANEL_PTOVENTA", ddlPanelPtoventa.SelectedValue, RBtnListPanelPtoVenta.SelectedItem.Value, RbtMasopciones1.SelectedValue, ddlPanelPtoventa_Periodo.SelectedValue, "", Marca.Value, "", "", TxtPlanningPanelPuntoVenta.Text);
                                if (dt.Rows.Count == 0)
                                {
                                    cn.ejecutarDataTable("UP_WEB_REGISTERPANELPUNTOVENTA", TxtPlanningPanelPuntoVenta.Text, ddlPanelPtoventa.SelectedValue, RBtnListPanelPtoVenta.SelectedItem.Value, RbtMasopciones1.SelectedValue, ddlPanelPtoventa_Periodo.SelectedValue, "", Marca.Value, "", "", true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                }
                                else
                                {
                                    this.Session["encabemensa"] = "Señor Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "La Marca " + Marca.Value + " Ya ha sido agregado a la campaña.";
                                    Mensajes_Usuario();
                                    return;
                                }
                            }
                        }
                    }
                    if (this.Session["vista_final"].ToString() == "Categoria")
                    {
                        foreach (ListItem Categoria in ChkProductos1.Items)
                        {
                            if (Categoria.Selected)
                            {
                                dt = cn.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAR_PANEL_PTOVENTA", ddlPanelPtoventa.SelectedValue, RBtnListPanelPtoVenta.SelectedItem.Value, RbtMasopciones1.SelectedValue, ddlPanelPtoventa_Periodo.SelectedValue, "", "", "", Categoria.Value, TxtPlanningPanelPuntoVenta.Text);
                                if (dt.Rows.Count == 0)
                                {
                                    cn.ejecutarDataTable("UP_WEB_REGISTERPANELPUNTOVENTA", TxtPlanningPanelPuntoVenta.Text, ddlPanelPtoventa.SelectedValue, RBtnListPanelPtoVenta.SelectedItem.Value, RbtMasopciones1.SelectedValue, ddlPanelPtoventa_Periodo.SelectedValue, "", "", "", Categoria.Value, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                }
                                else
                                {
                                    this.Session["encabemensa"] = "Señor Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "La Marca " + Categoria.Value + " Ya ha sido agregado a la campaña.";
                                    Mensajes_Usuario();
                                    return;
                                }

                            }
                        }
                    }
                    if (this.Session["vista_final"].ToString() == "Familia")
                    {
                        foreach (ListItem Familia in ChkListFamilias1.Items)
                        {
                            if (Familia.Selected)
                            {
                                dt = cn.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTAR_PANEL_PTOVENTA", ddlPanelPtoventa.SelectedValue, RBtnListPanelPtoVenta.SelectedItem.Value, RbtMasopciones1.SelectedValue, ddlPanelPtoventa_Periodo.SelectedValue, "", "", Familia.Value, "", TxtPlanningPanelPuntoVenta.Text);
                                if (dt.Rows.Count == 0)
                                {
                                    cn.ejecutarDataTable("UP_WEB_REGISTERPANELPUNTOVENTA", TxtPlanningPanelPuntoVenta.Text, ddlPanelPtoventa.SelectedValue, RBtnListPanelPtoVenta.SelectedItem.Value, RbtMasopciones1.SelectedValue, ddlPanelPtoventa_Periodo.SelectedValue, "", "", Familia.Value, "", true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                }
                                else
                                {
                                    this.Session["encabemensa"] = "Señor Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "La Marca " + Familia.Value + " Ya ha sido agregado a la campaña.";
                                    Mensajes_Usuario();
                                    return;
                                }
                            }
                        }
                    }

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se ha creado con éxito a la campaña ";
                    Mensajes_Usuario();
                }

                else
                {
                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Seleccione un Periodo";
                    Mensajes_Usuario();
                    return;
                }
            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void BtnMasivoPanelPtoVenta_Click(object sender, EventArgs e)
        {
            InicializarPaneles();

            PanelPtoVenta_Masivo.Style.Value = "Display:Block;";

            IframePanelPtoVenta_Masiva.Attributes["src"] = "carga_Panel_PDV.aspx";
        }

        protected void BtnCloseMasivaPanelPtoVenta_Click(object sender, ImageClickEventArgs e)
        {
            InicializarPaneles();
            panelPuntoVenta.Style.Value = "Display:Block;";
        }


        #endregion


        #region Asignacion de Material POP

        protected void btnPanelMaterialPOP_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            //limpiar_InformacionReportPlanning();
            Panel_AsignacionMaterial_POP.Style.Value = "Display:Block;";
            StiloBotonSeleccionado();
            btnPanelMaterialPOP.CssClass = "buttonNewPlanSel";
            Habilitabotones();
            btnPanelMaterialPOP.Enabled = false;
            LlenaPresupuestosAsignados();
        }

        void llenarTipoMPOP()
        {
            if (ddlAsignasionMatePOP.SelectedValue != "0")
            {

                DataTable dt = wsPlanning.ObtenerIdPlanning(ddlAsignasionMatePOP.SelectedValue);
                try
                {
                    txtAsignasionMatePOP.Text = dt.Rows[0]["Planning"].ToString().Trim();
                    this.Session["Planning"] = txtAsignasionMatePOP.Text;
                    this.Session["company_id"] = dt.Rows[0]["Company_id"].ToString().Trim();
                    this.Session["Planning_CodChannel"] = dt.Rows[0]["Planning_CodChannel"].ToString().Trim();

                    this.Session["id_planningReportes"] = TxtPlanningReportes.Text;
                    this.Session["Presupuestoreportes"] = CmbSelPresupuestoPanelesPtoVenta.SelectedItem.Text;
                }
                catch
                {
 
                }
            }
            Conexion cn = new Conexion();

            DataTable dtl = cn.ejecutarDataTable("PLA_LLENATIPOMATERIALPOP");
            rblAsignasionMatePOP.DataSource = dtl;
            rblAsignasionMatePOP.DataValueField = "idtipoMa";
            rblAsignasionMatePOP.DataTextField = "TipoMaDescripcion";
            rblAsignasionMatePOP.DataBind();
        }

        void llenarMaterialPOP()
        {
             Conexion cn = new Conexion();

             DataTable dt = cn.ejecutarDataTable("PLA_LLENAMATERIALPOP", rblAsignasionMatePOP.SelectedValue);


            chkAsignasionMatePOP.DataSource = dt;
            chkAsignasionMatePOP.DataValueField = "id_MPointOfPurchase";
            chkAsignasionMatePOP.DataTextField = "POP_name";
            chkAsignasionMatePOP.DataBind();
            
        }

        protected void ddlAsignasionMatePOP_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarTipoMPOP();
            btnGuardarAsignasionMatePOP.Enabled = true;
        }

        protected void rblAsignasionMatePOP_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarMaterialPOP();
        }


        protected void btnGuardarAsignasionMatePOP_Click(object sender, EventArgs e)
        {
            Conexion cn = new Conexion();
            Conexion con = new Conexion(2);
            try
            {
                DataTable dt;
                DataTable dtl;

                foreach (ListItem TIPOMATPOP in rblAsignasionMatePOP.Items)
                {

                    if (TIPOMATPOP.Selected)
                    {

                    foreach (ListItem MATPOP in chkAsignasionMatePOP.Items)
                    {
                        if (MATPOP.Selected)
                        {
                            dtl = cn.ejecutarDataTable("UP_PLA_CONSULTAR_MPointOfPurchase_Planning", this.Session["Planning"].ToString(), MATPOP.Value);

                            if (dtl.Rows.Count == 0)
                            {
                            cn.ejecutarDataTable("UP_PLA_CREARPLANNINGXMPOP", this.Session["Planning"].ToString(), MATPOP.Value, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                           dt= con.ejecutarDataTable("STP_JVM_CONSULTAR_TBL_TIPMATERIAL", this.Session["Planning"].ToString(), TIPOMATPOP.Value);
                           if (dt.Rows.Count == 0)
                           {
                               con.ejecutarDataTable("STP_JVM_INSERTAR_TBL_TIPMATERIAL", this.Session["Planning"].ToString(), TIPOMATPOP.Value, TIPOMATPOP.Text);
                           }
                           con.ejecutarDataTable("STP_JVM_INSERTAR_TBL_EQUIPO_POP", this.Session["idPlanning"].ToString(), null, MATPOP.Value, 1, true, null);

                            }
                            else
                            {
                                this.Session["encabemensa"] = "Señor Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "El Material POP " + MATPOP.Text + " Ya ha sido agregado a la campaña.";
                                Mensajes_Usuario();
                                return;
                            }

                        }
                    }
                    }
                }


                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se ha creado con éxito el Material POP a la campaña ";
                    Mensajes_Usuario();
                    limpiar_InformacionAsignacionMatPOP();
                }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        private void limpiar_InformacionAsignacionMatPOP()
        {
            // limpiar controles 
            txtAsignasionMatePOP.Text = "";
            ddlAsignasionMatePOP.Text = "0";


            rblAsignasionMatePOP.Items.Clear();
            chkAsignasionMatePOP.Items.Clear();

        }

        protected void btnLimpiarAsignasionMatePOP_Click(object sender, EventArgs e)
        {
            limpiar_InformacionAsignacionMatPOP();
        }

        

        #endregion


        #region Elementos de visibilidad
        protected void btnsave_Elemento_Click(object sender, EventArgs e)
        {
            DataTable dt;
            DataTable dtl;

            string tipo_rep = this.Session["tipo_rep"].ToString();

            Conexion cn = new Conexion();
            Conexion con = new Conexion(2);
            foreach (ListItem ELEM_VISI in chklist.Items)
            {
                if (ELEM_VISI.Selected)
                {
                    dtl = cn.ejecutarDataTable("UP_PLA_CONSULTAR_MPointOfPurchase_Planning", Session["idPlanning"].ToString(), ELEM_VISI.Value);

                    if (dtl.Rows.Count == 0)
                    {
                        cn.ejecutarDataTable("UP_PLA_CREARPLANNINGXMPOP", this.Session["idPlanning"].ToString(), ELEM_VISI.Value, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                        //dt = con.ejecutarDataTable("STP_JVM_CONSULTAR_TBL_TIPMATERIAL", this.Session["Planning"].ToString(), "02");
                        //if (dt.Rows.Count == 0)
                        //{
                        //    con.ejecutarDataTable("STP_JVM_INSERTAR_TBL_TIPMATERIAL", this.Session["Planning"].ToString(), TIPOMATPOP.Value, TIPOMATPOP.Text);
                        //}

   

                        con.ejecutarDataTable("STP_JVM_INSERTAR_TBL_EQUIPO_POP", this.Session["idPlanning"].ToString(),null, ELEM_VISI.Value, null, true, null);

                    }
                    else
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "El Material POP " + ELEM_VISI.Text + " Ya ha sido agregado a la campaña.";
                        Mensajes_Usuario();
                        return;
                    }

                }
            }

            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupConfirm";
            this.Session["mensaje"] = "Se ha creado con éxito el Material POP a la campaña ";
            Mensajes_Usuario();

            chklist.Items.Clear();
            div_Elementos.Style.Value = "display:none";

        }
        #endregion


        #region Observacion
        protected void btnObservaciones_Click(object sender, EventArgs e)
        {
             DataTable dt;
            DataTable dtl;

            Conexion cn = new Conexion();
            Conexion con = new Conexion(2);
            foreach (ListItem OBSERVACION in chklist_Observaciones.Items)
            {
                if (OBSERVACION.Selected)
                {
                    dtl = cn.ejecutarDataTable("PLA_CONSULTAR_PLANNING_OBSERVACION", Session["idPlanning"].ToString(),Convert.ToInt32(OBSERVACION.Value),Convert.ToInt32(this.Session["RbtnListInfProd"].ToString()));

                    if (dtl.Rows.Count == 0)
                    {
                        cn.ejecutarDataTable("PLA_INSERTAR_PLANNING_OBSERVACION", this.Session["idPlanning"].ToString(), Convert.ToInt32(OBSERVACION.Value), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now, Convert.ToInt32(this.Session["RbtnListInfProd"].ToString()));

                        //dt = con.ejecutarDataTable("STP_JVM_CONSULTAR_TBL_TIPMATERIAL", this.Session["Planning"].ToString(), "02");
                        //if (dt.Rows.Count == 0)
                        //{
                        //    con.ejecutarDataTable("STP_JVM_INSERTAR_TBL_TIPMATERIAL", this.Session["Planning"].ToString(), TIPOMATPOP.Value, TIPOMATPOP.Text);
                        //}


                        if (OBSERVACION.Value.ToString().Length==1)
                        {
                            OBSERVACION.Value = "0" + OBSERVACION.Value;
                        }

                        con.ejecutarDataTable("STP_JVM_INSERTAR_TBL_EQUIPO_OBSERVACION", this.Session["idPlanning"].ToString(), OBSERVACION.Value, true);

                    }
                    else
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La Observación " + OBSERVACION.Text + " Ya ha sido agregado a la campaña.";
                        Mensajes_Usuario();
                        return;
                    }

                }
            }

            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupConfirm";
            this.Session["mensaje"] = "Se ha creado con éxito el Material POP a la campaña ";
            Mensajes_Usuario();

            chklist.Items.Clear();
            div_Elementos.Style.Value = "display:none";
        }
        #endregion





    }
}