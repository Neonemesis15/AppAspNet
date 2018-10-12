using System;
using System.Configuration;
using System.Data;
using Lucky.Business.Common.Application;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;


namespace SIGE.Pages.Modulos.Administrativo
{
    public partial class GestiónPaís : System.Web.UI.Page
    {
        private DataTable barrio = null;
        private string parametro = "";
        private string parametro2 = "";
        private string parametro3 = "";
        private string duplicado = "";
        private bool estado;
        private string snamecountry = "";
        private string repetido1 = "";
        private string repetido = "";
        private string scodcountry = "";
        private string scodpto = "";
        private string snamedpto = "";
        private string scodcity = "";
        private string snamecity = "";
        private string scodto = "";
        private string snamedto = "";
        private string scodbarrio = "";
        private string snamebarrio = "";
        private string sNomBarrio = "";
        private string sDeptoVacio, sCiudadVacio, sDistritoVacio;
        private int recsearch;
        private DataTable recorrido = null;

        private Conexion oConn = new Conexion();
        private Barrio oBarrio = new Barrio();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {                  
                   llenaComboPaisBuscarDpto();                  
                   llenar_comboPaísBuscarCiudad();               
                   llenar_comboPaísbuscarDistrito();                 
                   llenar_combopaísBuscarBarrio();
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
        private void activarControlesPais()
        {
            TxtNomPais.Enabled = true;
            cmbidioma.Enabled = true;
            chkDepto.Enabled = true;
            chkciudad.Enabled = true;
            chkdistrito.Enabled = true;
            chkbarrio.Enabled = true;
            RBtnListStatusPais.Enabled = false;
            TabPanelDepartamento.Enabled = false;
            TabPanelCiudad.Enabled = false;
            TabPanelDistrito.Enabled = false;
            Panel_Barrio.Enabled = false;
        }

        private void desactivarControlesPais()
        {
            TxtNomPais.Enabled = false;
            cmbidioma.Enabled = false;
            chkDepto.Enabled = false;
            chkciudad.Enabled = false;
            chkdistrito.Enabled = false;
            chkbarrio.Enabled = false;
            RBtnListStatusPais.Enabled = false;
            TabPanelDepartamento.Enabled = true;
            TabPanelCiudad.Enabled = true;
            TabPanelDistrito.Enabled = true;
            Panel_Barrio.Enabled = true;
        }

        private void crearActivarbotonesPais()
        {
            btnCrearPais.Visible = false;
            btnsavePais.Visible = true;
            btnConsultarPais.Visible = false;
            btnEditPais.Visible = false;
            btnActualizarPais.Visible = false;
            btnCancelPais.Visible = true;
            btnPreg19.Visible = false;
            btnAreg19.Visible = false;
            btnSreg19.Visible = false;
            btnUreg19.Visible = false;
        }

        private void SavelimpiarcontrolesPais()
        {
            TxtCodPais.Text = "";
            TxtNomPais.Text = "";
            cmbidioma.Text = "0";
            chkDepto.Checked = false;
            chkciudad.Checked = false;
            chkdistrito.Checked = false;
            chkbarrio.Checked = false;
            RBtnListStatusPais.Items[0].Selected = true;
            RBtnListStatusPais.Items[1].Selected = false;
            TxtBcodPais.Text = "";
            TxtBNomPais.Text = "";
        }

        private void saveActivarbotonesPais()
        {
            btnCrearPais.Visible = true;
            btnsavePais.Visible = false;
            btnConsultarPais.Visible = true;
            btnEditPais.Visible = false;
            btnActualizarPais.Visible = false;
            btnCancelPais.Visible = true;
            btnPreg19.Visible = false;
            btnAreg19.Visible = false;
            btnSreg19.Visible = false;
            btnUreg19.Visible = false;
        }

        private void EditarActivarbotonesPais()
        {
            btnCrearPais.Visible = true;
            btnsavePais.Visible = false;
            btnConsultarPais.Visible = true;
            btnEditPais.Visible = false;
            btnActualizarPais.Visible = true;
            btnCancelPais.Visible = true;
            btnPreg19.Visible = false;
            btnAreg19.Visible = false;
            btnSreg19.Visible = false;
            btnUreg19.Visible = false;
        }

        private void EditarActivarControlesPais()
        {
            cmbidioma.Enabled = true;
            RBtnListStatusPais.Enabled = true;
            TxtCodPais.Enabled = false;
            TxtNomPais.Enabled = true;
            chkDepto.Enabled = true;
            chkciudad.Enabled = true;
            chkdistrito.Enabled = true;
            chkbarrio.Enabled = true;
        }

        private void BuscarActivarbotnesPais() 
        {

            btnCrearPais.Visible = false;
            btnsavePais.Visible = false;
            btnConsultarPais.Visible = true;
            btnEditPais.Visible = true;
            btnActualizarPais.Visible = false;
            btnCancelPais.Visible = true;
        }

        private void llenar_comboIdioma()
        {           
            DataSet dscomboIdioma = new DataSet();
            dscomboIdioma = get_Administrativo.Get_Llenar_Combos("25");

            cmbidioma.DataSource = dscomboIdioma;
            cmbidioma.DataValueField = "cod_Lenguaje";
            cmbidioma.DataTextField = "Name_Lenguaje";
            cmbidioma.DataBind();
        }       

        private void activarControlesDpto()
        {
            cmbSDivPais.Enabled = true;
            TxtDivCodDept.Enabled = false;
            TxtDivNomDept.Enabled = true;
            RBtnDivEstDept.Enabled = false;
            TabPanelPaís.Enabled = false;
            TabPanelDepartamento.Enabled = true;
            TabPanelCiudad.Enabled = false;
            TabPanelDistrito.Enabled = false;
            Panel_Barrio.Enabled = false;
        }

        private void desactivarControlesDpto()
        {
            cmbSDivPais.Enabled = false;
            TxtDivCodDept.Enabled = false;
            TxtDivNomDept.Enabled = false;
            RBtnDivEstDept.Enabled = false;
            TabPanelPaís.Enabled = true;
            TabPanelCiudad.Enabled = true;
            TabPanelDistrito.Enabled = true;
            Panel_Barrio.Enabled = true;
        }

        private void crearActivarbotonesDpto()
        {
            BtnCrearDivDept.Visible = false;
            BtnSaveDivDept.Visible = true;
            BtnConsulDivDept.Visible = false;
            BtnEditDivDept.Visible = false;
            BtnActualizarDivDept.Visible = false;
            BtnCancelDivDept.Visible = true;
            pregdept.Visible = false;
            aregdept.Visible = false;
            sregdept.Visible = false;
            uregdept.Visible = false;
        }

        private void SavelimpiarcontrolesDpto()
        {
            cmbSDivPais.Text = "0";
            TxtDivCodDept.Text = "";
            TxtDivNomDept.Text = "";
            RBtnDivEstDept.Items[0].Selected = true;
            RBtnDivEstDept.Items[1].Selected = false;

            CmbSelPaisDept.Text = "0";
            TxtbCodDepto.Text = "";
            TxtbNomDepto.Text = "";
        }

        private void saveActivarbotonesDpto()
        {
            BtnCrearDivDept.Visible = true;
            BtnSaveDivDept.Visible = false;
            BtnConsulDivDept.Visible = true;
            BtnEditDivDept.Visible = false;
            BtnActualizarDivDept.Visible = false;
            BtnCancelDivDept.Visible = true;
            pregdept.Visible = false;
            aregdept.Visible = false;
            sregdept.Visible = false;
            uregdept.Visible = false;
        }

        private void EditarActivarbotonesDpto()
        {
            BtnCrearDivDept.Visible = true;
            BtnSaveDivDept.Visible = false;
            BtnConsulDivDept.Visible = true;
            BtnEditDivDept.Visible = false;
            BtnActualizarDivDept.Visible = true;
            BtnCancelDivDept.Visible = true;
            pregdept.Visible = false;
            aregdept.Visible = false;
            sregdept.Visible = false;
            uregdept.Visible = false;
        }

        private void EditarActivarControlesDpto()
        {
            cmbSDivPais.Enabled = true;
            TxtDivCodDept.Enabled = false;
            TxtDivNomDept.Enabled = true;
            RBtnDivEstDept.Enabled = true;
            TabPanelPaís.Enabled = false;
            TabPanelCiudad.Enabled = false;
            TabPanelDistrito.Enabled = false;
            Panel_Barrio.Enabled = false;
        }

        private void BuscarActivarbotnesDpto()
        {
            BtnCrearDivDept.Visible = false;
            BtnSaveDivDept.Visible = false;
            BtnConsulDivDept.Visible = true;
            BtnEditDivDept.Visible = true;
            BtnActualizarDivDept.Visible = false;
            BtnCancelDivDept.Visible = true;
            pregdept.Visible = false;
            aregdept.Visible = false;
            sregdept.Visible = false;
            uregdept.Visible = false;
        }

        private void llenar_comboPaísDpto()
        {
            DataSet dscomboPais = new DataSet();
            dscomboPais = get_Administrativo.Get_Llenar_Combos("26");

            cmbSDivPais.DataSource = dscomboPais.Tables[0];
            cmbSDivPais.DataTextField = "Name_Country";
            cmbSDivPais.DataValueField = "cod_Country";
            cmbSDivPais.DataBind();
        }

        private void llenaComboPaisBuscarDpto() 
        {
            /*
            DataSet ds3 = new DataSet();
            ds3 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 37);
            //se llena Paises en departamento en buscar deptos
            CmbSelPaisDept.DataSource = ds3;
            CmbSelPaisDept.DataTextField = "Name_Country";
            CmbSelPaisDept.DataValueField = "cod_Country";
            CmbSelPaisDept.DataBind();
            */
        }

        private void activarControlesCdad()
        {
            CmbSSDivPais.Enabled = true;
            CmbSSDivDept.Enabled = true;
            RbtnEstdivCiudad.Enabled = false;
            TxtDivcodCiudad.Enabled = false;
            TxtDivNomCiudad.Enabled = true;
            TabPanelPaís.Enabled = false;
            TabPanelDepartamento.Enabled = false;
            TabPanelCiudad.Enabled = true;
            TabPanelDistrito.Enabled = false;
            Panel_Barrio.Enabled = false;
        }

        private void desactivarControlesCdad()
        {
            CmbSSDivPais.Enabled = false;
            CmbSSDivDept.Enabled = false;
            TxtDivcodCiudad.Enabled = false;
            TxtDivNomCiudad.Enabled = false;
            RbtnEstdivCiudad.Enabled = false;
            TabPanelPaís.Enabled = true;
            TabPanelCiudad.Enabled = true;
            TabPanelDistrito.Enabled = true;
            TabPanelDepartamento.Enabled = true;
            Panel_Barrio.Enabled = true;
        }

        private void crearActivarbotonesCdad()
        {
            BtnCrearDivCiudad.Visible = false;
            BtnSaveDivCiudad.Visible = true;
            BtnConsultarDivCiudad.Visible = false;
            BtnEditDivCiudad.Visible = false;
            BtnActualizarDivCiudad.Visible = false;
            BtnCancelDivCiudad.Visible = true;
            PregCiudad.Visible = false;
            AregCiudad.Visible = false;
            SregCiudad.Visible = false;
            UregCiudad.Visible = false;
        }

        private void SavelimpiarcontrolesCdad()
        {
            TxtDivcodCiudad.Text = "";
            TxtDivNomCiudad.Text = "";
            CmbSSDivPais.Text = "0";
            CmbSSDivDept.Items.Clear();
            RbtnEstdivCiudad.Items[0].Selected = true;
            RbtnEstdivCiudad.Items[1].Selected = false;
            CmbSelPaisCiudad.Text = "0";
            CmbSelDeptoCiudad.Items.Clear();
            TxtbCodciud.Text = "";
            TxtbNomCiud.Text = "";
        }

        private void saveActivarbotonesCdad()
        {
            BtnCrearDivCiudad.Visible = true;
            BtnSaveDivCiudad.Visible = false;
            BtnConsultarDivCiudad.Visible = true;
            BtnEditDivCiudad.Visible = false;
            BtnActualizarDivCiudad.Visible = false;
            BtnCancelDivCiudad.Visible = true;
            PregCiudad.Visible = false;
            AregCiudad.Visible = false;
            SregCiudad.Visible = false;
            UregCiudad.Visible = false;
        }

        private void EditarActivarbotonesCdad()
        {
            BtnCrearDivCiudad.Visible = true;
            BtnSaveDivCiudad.Visible = false;
            BtnConsultarDivCiudad.Visible = true;
            BtnEditDivCiudad.Visible = false;
            BtnActualizarDivCiudad.Visible = true;
            BtnCancelDivCiudad.Visible = true;
            PregCiudad.Visible = false;
            AregCiudad.Visible = false;
            SregCiudad.Visible = false;
            UregCiudad.Visible = false;
        }

        private void EditarActivarControlesCdad()
        {
            cmbSDivPais.Enabled = true;
            CmbSSDivDept.Enabled = true;
            TxtDivcodCiudad.Enabled = false;
            TxtDivNomCiudad.Enabled = true;
            RbtnEstdivCiudad.Enabled = true;
            TabPanelPaís.Enabled = false;
            TabPanelDepartamento.Enabled = false;
            TabPanelCiudad.Enabled = false;
            TabPanelDistrito.Enabled = false;
            Panel_Barrio.Enabled = false;
        }

        private void BuscarActivarbotonesCdad()    
        {
            BtnCrearDivCiudad.Visible = false;
            BtnSaveDivCiudad.Visible = false;
            BtnConsultarDivCiudad.Visible = true;
            BtnEditDivCiudad.Visible = true;
            BtnActualizarDivCiudad.Visible = false;
            BtnCancelDivCiudad.Visible = true;
        }

        private void llenar_combopaísCdad() 
        {
            DataSet dscomboPais = new DataSet();
            dscomboPais = get_Administrativo.Get_Llenar_Combos("38");
            CmbSSDivPais.DataSource = dscomboPais.Tables[0];
            CmbSSDivPais.DataTextField = "Name_Country";
            CmbSSDivPais.DataValueField = "cod_Country";
            CmbSSDivPais.DataBind();
        }

        private void llenar_comboPaísBuscarCiudad()
        { 
            //se llena Paises en buscar ciudades
            CmbSelPaisCiudad.DataSource = get_Administrativo.Get_Llenar_Combos("57");
            CmbSelPaisCiudad.DataTextField = "Name_Country";
            CmbSelPaisCiudad.DataValueField = "cod_Country";
            CmbSelPaisCiudad.DataBind();
        }

        private void Get_DivPolitica()
        {
            DataSet dsDivPolitica = new DataSet();
            dsDivPolitica = get_Administrativo.Get_DivPolitica(this.Session["codPais"].ToString().Trim());

            ECountry oescountry = new ECountry();
            if (dsDivPolitica != null)
            {
                if (dsDivPolitica.Tables[0].Rows.Count > 0)
                {
                    oescountry.CountryDpto = Convert.ToBoolean(dsDivPolitica.Tables[0].Rows[0]["Country_Dpto"].ToString().Trim());
                    oescountry.CountryCiudad = Convert.ToBoolean(dsDivPolitica.Tables[0].Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dsDivPolitica.Tables[0].Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dsDivPolitica.Tables[0].Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }
            if (oescountry.CountryDpto == true)
            {
                Get_llenar_ComboDptoCdad();
            }
            else
            {
                duplicado = "2";
                parametro = "";
                CmbSSDivDept.Items.Clear();
                CmbSSDivDept.Enabled = false;
                this.Session["parametro"] = parametro;
            }
            dsDivPolitica = null;
            oescountry = null;
            this.Session["duplicado"] = duplicado;
        }

        private void Get_llenar_ComboDptoCdad()
        {
            //en view ciudades
            DataTable dtcountry = new DataTable();
            dtcountry = get_Administrativo.Get_llenar_ComboDpto(CmbSSDivPais.SelectedValue);

            if (dtcountry != null)
            {
                if (dtcountry.Rows.Count > 1)
                {
                    CmbSSDivDept.DataSource = dtcountry;
                    CmbSSDivDept.DataTextField = "Name_dpto";
                    CmbSSDivDept.DataValueField = "cod_dpto";
                    CmbSSDivDept.DataBind();
                }
                else
                {
                    // FaltaDivPolDepto();
                    CmbSSDivDept.DataSource = dtcountry;
                    CmbSSDivDept.DataTextField = "Name_dpto";
                    CmbSSDivDept.DataValueField = "cod_dpto";
                    CmbSSDivDept.DataBind();
                }
                duplicado = "1";
                parametro = CmbSSDivDept.Text;
                CmbSSDivDept.Enabled = true;
                this.Session["parametro"] = parametro;
            }
        }                    

        private void activarControlesDistrito()
        {
            CmbSDDivPais.Enabled = true;
            CmbSDDept.Enabled = true;
            CmbSDCiudad.Enabled = true;
            TxtCodDistrito.Enabled = false;
            TxtNomDistrito.Enabled = true;
            RBtnEstadoDistrito.Enabled = false;
            TabPanelPaís.Enabled = false;
            TabPanelDepartamento.Enabled = false;
            TabPanelCiudad.Enabled = false;
            TabPanelDistrito.Enabled = true;
            Panel_Barrio.Enabled = false;
        }

        private void desactivarControlesDistrito()
        {
            CmbSDDivPais.Enabled = false;
            CmbSDDept.Enabled = false;
            CmbSDCiudad.Enabled = false;
            TxtCodDistrito.Enabled = false;
            TxtNomDistrito.Enabled = false;
            RBtnEstadoDistrito.Enabled = false;
            TabPanelPaís.Enabled = true;
            TabPanelDepartamento.Enabled = true;
            TabPanelCiudad.Enabled = true;
            TabPanelDistrito.Enabled = true;
            Panel_Barrio.Enabled = true;
        }

        private void crearActivarbotonesDistrito()
        {
            BtnCrearDistrito.Visible = false;
            BtnSaveDistrito.Visible = true;
            BtnconsulDistrito.Visible = false;
            BtnEditDistrito.Visible = false;
            BtnActualizaDistrito.Visible = false;
            BtnCancelDistrito.Visible = true;
            PregDistri.Visible = false;
            AregDistri.Visible = false;
            SregDistri.Visible = false;
            UregDistri.Visible = false;
        }

        private void SavelimpiarcontrolesDistrito()
        {
            TxtCodDistrito.Text = "";
            TxtNomDistrito.Text = "";
            CmbSDDept.Text = "0";
            CmbSDCiudad.Text = "0";
            CmbSDDivPais.Text = "0";
            RBtnEstadoDistrito.Items[0].Selected = true;
            RBtnEstadoDistrito.Items[1].Selected = false;
            TxtbNomDistrito.Text = "";
            CmbSelPaisDistrito.Text = "0";
            CmbSelDeptoDistrito.Items.Clear();
            CmbSelCiudadDistrito.Items.Clear();
            TxtbCodDistr.Text = "";
            TxtbNomDistrito.Text = "";
        }

        private void saveActivarbotonesDistrito()
        {
            BtnCrearDistrito.Visible = true;
            BtnSaveDistrito.Visible = false;
            BtnconsulDistrito.Visible = true;
            BtnEditDistrito.Visible = false;
            BtnActualizaDistrito.Visible = false;
            BtnCancelDistrito.Visible = true;
            PregDistri.Visible = false;
            AregDistri.Visible = false;
            SregDistri.Visible = false;
            UregDistri.Visible = false;
        }

        private void EditarActivarbotonesDistrito()
        {
            BtnCrearDistrito.Visible = true;
            BtnSaveDistrito.Visible = false;
            BtnconsulDistrito.Visible = true;
            BtnEditDistrito.Visible = false;
            BtnActualizaDistrito.Visible = true;
            BtnCancelDistrito.Visible = true;
            PregDistri.Visible = false;
            AregDistri.Visible = false;
            SregDistri.Visible = false;
            UregDistri.Visible = false;
        }

        private void EditarActivarControlesDistrito()
        {
            CmbSDDivPais.Enabled = true;
            CmbSDDept.Enabled = true;
            CmbSDCiudad.Enabled = true;
            TxtCodDistrito.Enabled = false;
            TxtNomDistrito.Enabled = true;
            RBtnEstadoDistrito.Enabled = true;
            TabPanelPaís.Enabled = false;
            TabPanelDepartamento.Enabled = false;
            TabPanelCiudad.Enabled = false;
            TabPanelDistrito.Enabled = true;
            Panel_Barrio.Enabled = false;
        }

        private void buscaractivarbotonesDistrito()
        {
            BtnCrearDistrito.Visible = false;
            BtnSaveDistrito.Visible = false;
            BtnconsulDistrito.Visible = true;
            BtnEditDistrito.Visible = true;
            BtnActualizaDistrito.Visible = false;
            BtnCancelDistrito.Visible = true;
        }

        private void llenar_combopaísDistrito()
        {
            /*
            DataSet ds5 = new DataSet();
            ds5 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 39);
            //se llena Paises distritos            
            CmbSDDivPais.DataSource = ds5;
            CmbSDDivPais.DataTextField = "Name_Country";
            CmbSDDivPais.DataValueField = "cod_Country";
            CmbSDDivPais.DataBind();
            */
        }

        private void llenar_comboPaísbuscarDistrito()
        {
            /*
            //se llena Paises distritos search 
            CmbSelPaisDistrito.DataSource = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 55);
            CmbSelPaisDistrito.DataTextField = "Name_Country";
            CmbSelPaisDistrito.DataValueField = "cod_Country";
            CmbSelPaisDistrito.DataBind();
            */
        }

        private void activarControlesBarrio()
        {
            CmbSBpais.Enabled = true;
            CmbSBDept.Enabled = true;
            CmbSBciudad.Enabled = true;
            CmbSBdistr.Enabled = true;
            TxtCodBarr.Enabled = false;
            TxtNomBarr.Enabled = true;
            RbtnEstadoBarrios.Enabled = false;
            TabPanelPaís.Enabled = false;
            TabPanelDepartamento.Enabled = false;
            TabPanelCiudad.Enabled = false;
            TabPanelDistrito.Enabled = false;
            Panel_Barrio.Enabled = true;
        }

        private void desactivarControlesBarrio()
        {
            CmbSBpais.Enabled = false;
            CmbSBDept.Enabled = false;
            CmbSBciudad.Enabled = false;
            CmbSBdistr.Enabled = false;
            TxtCodBarr.Enabled = false;
            TxtNomBarr.Enabled = false;
            RbtnEstadoBarrios.Enabled = false;
            TabPanelPaís.Enabled = true;
            TabPanelDepartamento.Enabled = true;
            TabPanelCiudad.Enabled = true;
            TabPanelDistrito.Enabled = true;
            Panel_Barrio.Enabled = true;
        }

        private void crearActivarbotonesBarrio()
        {
            BtnCrearBarrios.Visible = false;
            BtnSaveBarrios.Visible = true;
            BtnConsulBarrios.Visible = false;
            BtnEditBarrios.Visible = false;
            BtnActualizaBarrios.Visible = false;
            BtncancelBarrios.Visible = true;
            PregBarrio.Visible = false;
            AregBarrio.Visible = false;
            SregBarrio.Visible = false;
            UregBarrio.Visible = false;
         }

        private void SavelimpiarcontrolesBarrio()
        {
            CmbSBpais.Text = "0";
            CmbSBDept.Text = "0";
            CmbSBciudad.Text = "0";
            CmbSBdistr.Text = "0";
            TxtCodBarr.Text = "";
            TxtNomBarr.Text = "";
            RbtnEstadoBarrios.Items[0].Selected = true;
            RbtnEstadoBarrios.Items[1].Selected = false;

            CmbSelPaisBarrio.Text = "0";
            CmbSelDeptoBarrio.Items.Clear();
            CmbSelCiudadBarrio.Items.Clear();
            CmbSelDistritoBarrio.Items.Clear();
            TxtbNomBarrio.Text = "";
        }

        private void saveActivarbotonesBarrio()
        {
            BtnCrearBarrios.Visible = true;
            BtnSaveBarrios.Visible = false;
            BtnConsulBarrios.Visible = true;
            BtnEditBarrios.Visible = false;
            BtnActualizaBarrios.Visible = false;
            BtncancelBarrios.Visible = true;
            PregBarrio.Visible = false;
            AregBarrio.Visible = false;
            SregBarrio.Visible = false;
            UregBarrio.Visible = false;
        }

        private void EditarActivarbotonesBarrio()
        {
            BtnCrearBarrios.Visible = true;
            BtnSaveBarrios.Visible = false;
            BtnConsulBarrios.Visible = true;
            BtnEditBarrios.Visible = false;
            BtnActualizaBarrios.Visible = true;
            BtncancelBarrios.Visible = true;
            PregBarrio.Visible = false;
            AregBarrio.Visible = false;
            SregBarrio.Visible = false;
            UregBarrio.Visible = false;
        }

        private void EditarActivarControlesBarrio()
        {
            TabPanelPaís.Enabled = false;
            TabPanelDepartamento.Enabled = false;
            TabPanelCiudad.Enabled = false;
            TabPanelDistrito.Enabled = false;
            Panel_Barrio.Enabled = true;
            CmbSBpais.Enabled = true;
            CmbSBDept.Enabled = true;
            CmbSBciudad.Enabled = true;
            CmbSBdistr.Enabled = true;
            RbtnEstadoBarrios.Enabled = true;
            TxtCodBarr.Enabled = false;
            TxtNomBarr.Enabled = true;
        }

        private void BuscarActivarbotonesBarrio() 
        {
            BtnCrearBarrios.Visible = false;
            BtnSaveBarrios.Visible = false;
            BtnConsulBarrios.Visible = true;
            BtnEditBarrios.Visible = true;
            BtnActualizaBarrios.Visible = false;
            BtncancelBarrios.Visible = true;
        }

        private void llenar_CombopaisBarrio()
        {
            /*DataSet ds2 = new DataSet();
            ds2 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 29);
            //se llena Paises en barrios
            CmbSBpais.DataSource = ds2;
            CmbSBpais.DataTextField = "Name_Country";
            CmbSBpais.DataValueField = "cod_Country";
            CmbSBpais.DataBind();
            */
        }

        private void llenar_combopaísBuscarBarrio() 
        {
            /*
            //se llena Paises en buscar barrios
            CmbSelPaisBarrio.DataSource = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 56);
            CmbSelPaisBarrio.DataTextField = "Name_Country";
            CmbSelPaisBarrio.DataValueField = "cod_Country";
            CmbSelPaisBarrio.DataBind();
            */
        }

        private void MensajeAlerta()
        {
            ModalPopupAlertas.Show();
            BtnAceptarAlert.Focus();
            //ScriptManager.RegisterStartupScript(
            //    this, this.GetType(), "myscript", "alert('Debe ingresar todos los parametros con (*)');", true);
        }

        #endregion

        #region Eventos Pais
        protected void btnCrearPais_Click(object sender, EventArgs e)
        {
            //llama la función que Activa controles 
            llenar_comboIdioma();
            activarControlesPais();
            crearActivarbotonesPais();
        }

        protected void btnsavePais_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomPais.Text = TxtNomPais.Text.TrimStart();
            if (cmbidioma.Text == "0" || TxtNomPais.Text == "")
            {
                if (cmbidioma.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Idioma";
                }
                if (TxtNomPais.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Pais";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            if (chkDepto.Checked == false && chkciudad.Checked == false && chkdistrito.Checked == false && chkbarrio.Checked == false)
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar por lo menos una división politica";
                MensajeAlerta();
                return;
            }
            try
            {

                DAplicacion odconsucliente = new DAplicacion();
                DataTable dtconsulta = odconsucliente.ConsultaDuplicados(ConfigurationManager.AppSettings["Paises"], TxtCodPais.Text, TxtNomPais.Text, null);
                if (dtconsulta == null)
                {
                    Country oCountry = new Country();
                    ECountry oecountry = oCountry.RegisterCountry(TxtCodPais.Text, TxtNomPais.Text, cmbidioma.SelectedValue.ToString(), Convert.ToBoolean(chkDepto.Checked), Convert.ToBoolean(chkciudad.Checked),
                    Convert.ToBoolean(chkdistrito.Checked), Convert.ToBoolean(chkbarrio.Checked), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sCountry = "";
                    sCountry = TxtNomPais.Text;
                    this.Session["sCountry"] = sCountry;
                    llenar_comboPaísDpto();
                    llenaComboPaisBuscarDpto();
                    llenar_combopaísCdad();
                    llenar_comboPaísBuscarCiudad();
                    llenar_combopaísDistrito();
                    llenar_comboPaísbuscarDistrito();
                    llenar_CombopaisBarrio();
                    llenar_combopaísBuscarBarrio();
                    SavelimpiarcontrolesPais();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El país " + this.Session["sCountry"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesPais();
                    desactivarControlesPais();
                }
                else
                {
                    string sCountry = "";
                    sCountry = TxtNomPais.Text;
                    this.Session["sCountry"] = sCountry;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El país " + this.Session["sCountry"] + " Ya Existe";
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

        protected void btnBuscarDivPol_Click(object sender, EventArgs e)
        {
            desactivarControlesPais();
            LblFaltantes.Text = "";
            TxtBNomPais.Text = TxtBNomPais.Text.TrimStart();
            TxtBcodPais.Text = TxtBcodPais.Text.TrimStart();
            if (TxtBNomPais.Text == "" && TxtBcodPais.Text == "")
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnDivPol_ModalPopupExtender.Show();
                return;
            }
            BuscarActivarbotnesPais();
            //btnCrearPais.Visible = true;
            scodcountry = TxtBcodPais.Text;
            snamecountry = TxtBNomPais.Text;
            TxtBNomPais.Text = "";
            TxtBcodPais.Text = "";
            DataTable dtcountry = new DataTable();
            Country oCountry = new Country();
            dtcountry = oCountry.ConsultarCountry(scodcountry, snamecountry);
            
            if (dtcountry != null)
            {
                if (dtcountry.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtcountry.Rows.Count - 1; i++)
                    {
                        TxtCodPais.Text = dtcountry.Rows[0]["cod_Country"].ToString().Trim();
                        TxtNomPais.Text = dtcountry.Rows[0]["Name_Country"].ToString().Trim();
                        llenar_comboIdioma();
                        cmbidioma.Text = dtcountry.Rows[0]["cod_Lenguaje"].ToString().Trim();
                        chkDepto.Checked = Convert.ToBoolean(dtcountry.Rows[0]["Country_Dpto"].ToString().Trim());
                        chkciudad.Checked = Convert.ToBoolean(dtcountry.Rows[0]["Country_Ciudad"].ToString().Trim());
                        chkdistrito.Checked = Convert.ToBoolean(dtcountry.Rows[0]["Country_Distrito"].ToString().Trim());
                        chkbarrio.Checked = Convert.ToBoolean(dtcountry.Rows[0]["Country_Barrio"].ToString().Trim());

                        estado = Convert.ToBoolean(dtcountry.Rows[0]["Country_status"].ToString().Trim());
                        if (estado == true)
                        {
                            RBtnListStatusPais.Items[0].Selected = true;
                            RBtnListStatusPais.Items[1].Selected = false;
                        }
                        else
                        {
                            RBtnListStatusPais.Items[0].Selected = false;
                            RBtnListStatusPais.Items[1].Selected = true;
                        }
                        this.Session["tcountry"] = dtcountry;
                        this.Session["i"] = 0;
                    }
                    if (dtcountry.Rows.Count == 1)
                    {
                        btnPreg19.Visible = false;
                        btnAreg19.Visible = false;
                        btnSreg19.Visible = false;
                        btnUreg19.Visible = false;
                    }
                    else
                    {
                        btnPreg19.Visible = true;
                        btnAreg19.Visible = true;
                        btnSreg19.Visible = true;
                        btnUreg19.Visible = true;
                    }
                }
                else
                {
                    llenar_comboPaísDpto();
                    llenaComboPaisBuscarDpto();
                    llenar_combopaísCdad();
                    llenar_comboPaísBuscarCiudad();
                    llenar_combopaísDistrito();
                    llenar_comboPaísbuscarDistrito();
                    llenar_CombopaisBarrio();
                    llenar_combopaísBuscarBarrio();
                    SavelimpiarcontrolesPais();
                    SavelimpiarcontrolesPais();
                    saveActivarbotonesPais();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnDivPol_ModalPopupExtender.Show();
                }
            }
        }

        protected void btnEditPais_Click(object sender, EventArgs e)
        {
            btnActualizarPais.Visible = true;
            btnEditPais.Visible = false;
            EditarActivarControlesPais();
            this.Session["rept"] = TxtCodPais.Text;
            this.Session["rept1"] = TxtNomPais.Text;
            DAplicacion oddeshabdivpol = new DAplicacion();

            DataTable dtdepto = oddeshabdivpol.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryDepartament"], TxtCodPais.Text);
            if (dtdepto != null)
            {
                chkDepto.Enabled = false;
            }
            else
            {
                chkDepto.Enabled = true;
            }

            DataTable dtcity = oddeshabdivpol.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryCity"], TxtCodPais.Text);
            if (dtcity != null)
            {
                chkciudad.Enabled = false;
            }
            else
            {
                chkciudad.Enabled = true;
            }

            DataTable dtdistrito = oddeshabdivpol.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryDistrict"], TxtCodPais.Text);
            if (dtdistrito != null)
            {
                chkdistrito.Enabled = false;
            }
            else
            {
                chkdistrito.Enabled = true;
            }

            DataTable dtbarrio = oddeshabdivpol.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryCommunity"], TxtCodPais.Text);
            if (dtbarrio != null)
            {
                chkbarrio.Enabled = false;
            }
            else
            {
                chkbarrio.Enabled = true;
            }

            dtdepto = null;
            dtcity = null;
            dtdistrito = null;
            dtbarrio = null;
        }

        protected void btnActualizarPais_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomPais.Text = TxtNomPais.Text.TrimStart();
            if (cmbidioma.Text == "0" || TxtNomPais.Text == "")
            {
                if (cmbidioma.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Idioma";
                }
                if (TxtNomPais.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Pais";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            if (chkDepto.Checked == false && chkciudad.Checked == false && chkdistrito.Checked == false && chkbarrio.Checked == false)
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar por lo menos una Disvisión Politica";
                MensajeAlerta();
                return;
            }
            try
            {
                if (RBtnListStatusPais.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabCountry = new DAplicacion();
                    DataTable dt = oddeshabCountry.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryappLucky"], TxtCodPais.Text);
                    if (dt != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en la tabla appLucky, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt1 = oddeshabCountry.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryCity"], TxtCodPais.Text);

                    if (dt1 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Ciudad, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt2 = oddeshabCountry.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryCommunity"], TxtCodPais.Text);
                    if (dt2 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Barrio, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt3 = oddeshabCountry.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryCompany"], TxtCodPais.Text);
                    if (dt3 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Cliente, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt4 = oddeshabCountry.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryDepartament"], TxtCodPais.Text);
                    if (dt4 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Dpto., por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt5 = oddeshabCountry.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryDistrict"], TxtCodPais.Text);
                    if (dt5 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Distrito, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt7 = oddeshabCountry.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryPerson"], TxtCodPais.Text);
                    if (dt7 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Usuario, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt8 = oddeshabCountry.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryPointOfSale"], TxtCodPais.Text);
                    if (dt8 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Puntos de Venta, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt10 = oddeshabCountry.PermitirDeshabilitar(ConfigurationManager.AppSettings["CountryStrategies"], TxtCodPais.Text);
                    if (dt10 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Servicios, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                }

                repetido1 = Convert.ToString(this.Session["rept1"]);
                if (repetido1 != TxtNomPais.Text)
                {
                    DAplicacion odconsuclientes = new DAplicacion();
                    DataTable dtconsulta = odconsuclientes.ConsultaDuplicados(ConfigurationManager.AppSettings["Paises"], null, TxtNomPais.Text, null);

                    if (dtconsulta == null)
                    {
                        Country oCountry = new Country();
                        ECountry oecountry = oCountry.ActualizarCountry(TxtCodPais.Text, TxtNomPais.Text, cmbidioma.SelectedValue.ToString(), Convert.ToBoolean(chkDepto.Checked), Convert.ToBoolean(chkciudad.Checked),
                        Convert.ToBoolean(chkdistrito.Checked), Convert.ToBoolean(chkbarrio.Checked), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        snamecountry = TxtNomPais.Text;
                        this.Session["sCountry"] = snamecountry;
                        llenar_comboPaísDpto();
                        llenaComboPaisBuscarDpto();
                        llenar_combopaísCdad();
                        llenar_comboPaísBuscarCiudad();
                        llenar_combopaísDistrito();
                        llenar_comboPaísbuscarDistrito();
                        llenar_CombopaisBarrio();
                        llenar_combopaísBuscarBarrio();
                        SavelimpiarcontrolesPais();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El País : " + this.Session["sCountry"] + " Se Actualizo Corecctamente";
                        MensajeAlerta();
                        saveActivarbotonesPais();
                    }
                    else
                    {
                        snamecountry = TxtNomPais.Text;
                        this.Session["sCountry"] = snamecountry;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = this.Session["sCountry"] + " No se puede Actualizar este registro ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    Country oCountry = new Country();
                    ECountry oecountry = oCountry.ActualizarCountry(TxtCodPais.Text, TxtNomPais.Text, cmbidioma.SelectedValue.ToString(), Convert.ToBoolean(chkDepto.Checked), Convert.ToBoolean(chkciudad.Checked),
                    Convert.ToBoolean(chkdistrito.Checked), Convert.ToBoolean(chkbarrio.Checked), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    snamecountry = TxtNomPais.Text;
                    this.Session["sCountry"] = snamecountry;
                    llenar_comboPaísDpto();
                    llenaComboPaisBuscarDpto();
                    llenar_combopaísCdad();
                    llenar_comboPaísBuscarCiudad();
                    llenar_combopaísDistrito();
                    llenar_comboPaísbuscarDistrito();
                    llenar_CombopaisBarrio();
                    llenar_combopaísBuscarBarrio();
                    SavelimpiarcontrolesPais();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El País : " + this.Session["sCountry"] + " Se Actualizo con Exito";
                    MensajeAlerta();
                    saveActivarbotonesPais();
                    desactivarControlesPais();
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

        protected void btnCancelPais_Click(object sender, EventArgs e)
        {
            SavelimpiarcontrolesPais();
            saveActivarbotonesPais();
            desactivarControlesPais();
        }

        private void MostrarDatosPaises()
        {
            recorrido = (DataTable)this.Session["tcountry"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodPais.Text = recorrido.Rows[recsearch]["cod_Country"].ToString().Trim();
                    TxtNomPais.Text = recorrido.Rows[recsearch]["Name_Country"].ToString().Trim();
                    cmbidioma.Text = recorrido.Rows[recsearch]["cod_Lenguaje"].ToString().Trim();
                    chkDepto.Checked = Convert.ToBoolean(recorrido.Rows[recsearch]["Country_Dpto"].ToString().Trim());
                    chkciudad.Checked = Convert.ToBoolean(recorrido.Rows[recsearch]["Country_Ciudad"].ToString().Trim());
                    chkdistrito.Checked = Convert.ToBoolean(recorrido.Rows[recsearch]["Country_Distrito"].ToString().Trim());
                    chkbarrio.Checked = Convert.ToBoolean(recorrido.Rows[recsearch]["Country_Barrio"].ToString().Trim());
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Country_status"].ToString().Trim());

                    if (estado == true)
                    {
                        RBtnListStatusPais.Items[0].Selected = true;
                        RBtnListStatusPais.Items[1].Selected = false;
                    }
                    else
                    {
                        RBtnListStatusPais.Items[0].Selected = false;
                        RBtnListStatusPais.Items[1].Selected = true;
                    }
                }
            }
        }

        protected void btnPreg19_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tCountry"];
            MostrarDatosPaises();
        }

        protected void btnAreg19_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosPaises();
            }
        }

        protected void btnSreg19_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tCountry"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosPaises();
            }
        }

        protected void btnUreg19_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tCountry"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosPaises();
        }

        #endregion

        #region Eventos Departamento
        protected void BtnCrearDivDept_Click(object sender, EventArgs e)
        {
            llenar_comboPaísDpto();
            activarControlesDpto();
            crearActivarbotonesDpto();
        }

        protected void BtnSaveDivDept_Click(object sender, EventArgs e)
        {            
            LblFaltantes.Text = "";
            TxtDivNomDept.Text = TxtDivNomDept.Text.TrimStart();
            if (cmbSDivPais.Text == "0" || TxtDivNomDept.Text == "")
            {
                if (cmbSDivPais.Text == "0")
                {
                    LblFaltantes.Text = ". " + "País";
                }
                if (TxtDivNomDept.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Departamento";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
                DAplicacion odconsucliente = new DAplicacion();
                DataTable dtconsulta = odconsucliente.ConsultaDuplicados(ConfigurationManager.AppSettings["Departamento"], cmbSDivPais.Text, TxtDivNomDept.Text, null);
                if (dtconsulta == null)
                {
                    Departamento oddepartamento = new Departamento();
                    EDepartamento oedepartamento = oddepartamento.RegisterDepartamento(TxtDivCodDept.Text, cmbSDivPais.SelectedValue.ToString(), TxtDivNomDept.Text, true,
                    Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    string sdpto = "";
                    sdpto = TxtDivNomDept.Text;
                    this.Session["sdpto"] = sdpto;
                    SavelimpiarcontrolesDpto();
                    llenaComboPaisBuscarDpto();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Departamento: " + this.Session["sdpto"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesDpto();
                    desactivarControlesDpto();
                }
                else
                {
                    string sdpto = "";
                    sdpto = TxtDivNomDept.Text;
                    this.Session["sdpto"] = sdpto;
                    this.Session["mensajealert"] = this.Session["sdpto"];
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Departamento: " + this.Session["sdpto"] + " Ya Existe";
                    MensajeAlerta(); ;
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

        protected void btnBuscardepto_Click(object sender, EventArgs e)
        {
            desactivarControlesDpto();
            IbtnDeptos_ModalPopupExtender.Hide();
            LblFaltantes.Text = "";
            TxtbCodDepto.Text = TxtbCodDepto.Text.TrimStart();
            TxtbNomDepto.Text = TxtbNomDepto.Text.TrimStart();
            if (CmbSelPaisDept.Text == "0" && TxtbCodDepto.Text == "" && TxtbNomDepto.Text == "")
            {
                this.Session["mensajealert"] = "Código de país , Código de Departamento y/o Nombre de Departamento";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnDeptos_ModalPopupExtender.Show();
                return;
            }
            BuscarActivarbotnesDpto();
            //BtnCrearDivDept.Visible = true;
            scodpto = TxtbCodDepto.Text;
            snamedpto = TxtbNomDepto.Text;
            scodcountry = CmbSelPaisDept.Text;
            TxtDivCodDept.Text = "";
            TxtbCodDepto.Text = "";
            TxtbNomDepto.Text = "";
            TxtDivNomDept.Text = "";
            CmbSelPaisDept.Text = "0";

            DataTable dtdpto = new DataTable();
            Departamento odpto = new Departamento();
            dtdpto = odpto.ConsultaDepartamento(scodpto, scodcountry, snamedpto);
            if (dtdpto != null)
            {
                if (dtdpto.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtdpto.Rows.Count - 1; i++)
                    {
                        llenar_comboPaísDpto();
                        cmbSDivPais.SelectedValue = dtdpto.Rows[0]["cod_Country"].ToString().Trim();
                        TxtDivCodDept.Text = dtdpto.Rows[0]["cod_dpto"].ToString().Trim();
                        TxtDivNomDept.Text = dtdpto.Rows[0]["Name_dpto"].ToString().Trim();
                        estado = Convert.ToBoolean(dtdpto.Rows[0]["Dpto_Status"].ToString().Trim());

                        if (estado == true)
                        {
                            RBtnDivEstDept.Items[0].Selected = true;
                            RBtnDivEstDept.Items[1].Selected = false;
                        }
                        else
                        {
                            RBtnDivEstDept.Items[0].Selected = false;
                            RBtnDivEstDept.Items[1].Selected = true;
                        }
                        this.Session["tdpto"] = dtdpto;
                        this.Session["i"] = 0;
                    }
                    if (dtdpto.Rows.Count == 1)
                    {
                        pregdept.Visible = false;
                        aregdept.Visible = false;
                        sregdept.Visible = false;
                        uregdept.Visible = false;
                    }
                    else
                    {
                        pregdept.Visible = true;
                        aregdept.Visible = true;
                        sregdept.Visible = true;
                        uregdept.Visible = true;
                    }
                }
                else
                {
                    SavelimpiarcontrolesDpto();
                    BuscarActivarbotnesDpto();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnDeptos_ModalPopupExtender.Show();
                }
            }
        }

        protected void BtnEditDivDept_Click(object sender, EventArgs e)
        {
            //EditarActivarbotonesDpto();
            BtnActualizarDivDept.Visible = true;
            BtnEditDivDept.Visible = false;
            EditarActivarControlesDpto();
            TxtDivCodDept.Enabled = false;
            this.Session["rept"] = TxtDivCodDept.Text;
            this.Session["rept1"] = TxtDivNomDept.Text;
        }

        protected void BtnActualizarDivDept_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtDivCodDept.Text = TxtDivCodDept.Text.TrimStart();
            TxtDivNomDept.Text = TxtDivNomDept.Text.TrimStart();
            if (cmbSDivPais.Text == "0" || TxtDivNomDept.Text == "")
            {
                if (cmbSDivPais.Text == "0")
                {
                    LblFaltantes.Text = ". " + "País";
                }
                if (TxtDivNomDept.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Departamento";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            else
            {
                try
                {
                    if (RBtnDivEstDept.Items[0].Selected == true)
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
                        DAplicacion oddeshabdepto = new DAplicacion();
                        DataTable dt = oddeshabdepto.PermitirDeshabilitar(ConfigurationManager.AppSettings["CompanyBudget"], TxtDivCodDept.Text);

                        if (dt != null)
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en la tabla appLucky, por favor Verifique";
                            MensajeAlerta();
                            return;
                        }
                        DataTable dt1 = oddeshabdepto.PermitirDeshabilitar(ConfigurationManager.AppSettings["DepartamentCommunity"], TxtDivCodDept.Text);
                        if (dt1 != null)
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Barrio, por favor Verifique";
                            MensajeAlerta();
                            return;
                        }
                        DataTable dt2 = oddeshabdepto.PermitirDeshabilitar(ConfigurationManager.AppSettings["DepartamentDistrict"], TxtDivCodDept.Text);
                        if (dt2 != null)
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en la Maestro de distrito, por favor Verifique";
                            MensajeAlerta();
                            return;
                        }
                        DataTable dt3 = oddeshabdepto.PermitirDeshabilitar(ConfigurationManager.AppSettings["DepartamentPointOfSale"], TxtDivCodDept.Text);
                        if (dt3 != null)
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Puntos de Venta, por favor Verifique";
                            MensajeAlerta();
                            return;
                        }
                    }
                    repetido = Convert.ToString(this.Session["rept"]);
                    repetido1 = Convert.ToString(this.Session["rept1"]);

                    if (repetido != TxtDivCodDept.Text || repetido1 != TxtDivNomDept.Text)
                    {
                        DAplicacion oddpto = new DAplicacion();
                        DataTable dtconsulta = oddpto.ConsultaDuplicados(ConfigurationManager.AppSettings["Departamento"], cmbSDivPais.Text, TxtDivNomDept.Text, null);
                        Departamento oDepto = new Departamento();

                        if (dtconsulta == null)
                        {
                            Departamento oddepartamento = new Departamento();
                            EDepartamento oedepartamento = oddepartamento.ActualizarDepartamento(TxtDivCodDept.Text, cmbSDivPais.SelectedValue.ToString(), TxtDivNomDept.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                            snamedpto = TxtDivNomDept.Text;
                            this.Session["sdpto"] = snamedpto;
                            llenaComboPaisBuscarDpto();
                            SavelimpiarcontrolesDpto();                            
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "El Departamento : " + this.Session["sdpto"] + " Se Actualizo con Exito";
                            MensajeAlerta();
                            saveActivarbotonesDpto();
                            desactivarControlesDpto();
                        }
                        else
                        {
                            snamedpto = TxtDivNomDept.Text;
                            this.Session["sdpto"] = snamedpto;
                            this.Session["mensajealert"] = this.Session["sdpto"];
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Departamento : " + this.Session["sdpto"] + " No se puede Actualizar este registro ya Existe";
                            MensajeAlerta();
                        }
                    }
                    else
                    {
                        Departamento oddepartamento = new Departamento();
                        EDepartamento oedepartamento = oddepartamento.ActualizarDepartamento(TxtDivCodDept.Text, cmbSDivPais.SelectedValue.ToString(), TxtDivNomDept.Text, estado,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        snamedpto = TxtDivNomDept.Text;
                        this.Session["sdpto"] = snamedpto;
                        SavelimpiarcontrolesDpto();
                        llenaComboPaisBuscarDpto();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Departamento : " + this.Session["sdpto"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesDpto();
                        desactivarControlesDpto();
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
        }

        protected void BtnCancelDivDept_Click(object sender, EventArgs e)
        {
            SavelimpiarcontrolesDpto();
            saveActivarbotonesDpto();
            desactivarControlesDpto();
        }

        private void MostrarDatosDeptos()
        {
            recorrido = (DataTable)this.Session["tdpto"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    cmbSDivPais.SelectedValue = recorrido.Rows[recsearch]["cod_Country"].ToString().Trim();
                    TxtDivCodDept.Text = recorrido.Rows[recsearch]["cod_dpto"].ToString().Trim();
                    TxtDivNomDept.Text = recorrido.Rows[recsearch]["Name_dpto"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Dpto_Status"].ToString().Trim());
                    if (estado == true)
                    {
                        RBtnDivEstDept.Items[0].Selected = true;
                        RBtnDivEstDept.Items[1].Selected = false;
                    }
                    else
                    {
                        RBtnDivEstDept.Items[0].Selected = false;
                        RBtnDivEstDept.Items[1].Selected = true;
                    }
                }
            }
        }

        protected void pregdept_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tdpto"];
            MostrarDatosDeptos();
        }
        protected void aregdept_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosDeptos();
            }
        }
        protected void sregdept_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tdpto"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosDeptos();
            }
        }
        protected void uregdept_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tdpto"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosDeptos();
        }        

        #endregion

        #region Eventos Ciudad
        protected void BtnCrearDivCiudad_Click(object sender, EventArgs e)
        {
            llenar_combopaísCdad();
            activarControlesCdad();
            crearActivarbotonesCdad();
        }

        protected void BtnSaveDivCiudad_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtDivcodCiudad.Text = TxtDivcodCiudad.Text.TrimStart();
            TxtDivNomCiudad.Text = TxtDivNomCiudad.Text.TrimStart();
            parametro = CmbSSDivDept.Text;
            this.Session["parametro"] = parametro;
            if (CmbSSDivPais.Text == "0" || parametro == "0" || TxtDivNomCiudad.Text == "")
            {
                if (CmbSSDivPais.Text == "0")
                {
                    LblFaltantes.Text = ". " + "País";
                }
                if (parametro == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Departamento";
                }
                if (TxtDivNomCiudad.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Ciudad";
                }

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
                DAplicacion odconsultaciudad = new DAplicacion();
                if (this.Session["duplicado"] == "1")
                {
                    DataTable dtconsulta = odconsultaciudad.ConsultaDuplicados(ConfigurationManager.AppSettings["Ciudad"], CmbSSDivPais.Text, TxtDivNomCiudad.Text, null);
                    if (dtconsulta == null)
                    {
                        City oCity = new City();
                        ECity oecity = oCity.RegisterCity(TxtDivcodCiudad.Text, parametro, CmbSSDivPais.SelectedValue.ToString(), TxtDivNomCiudad.Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        string sCity = "";
                        sCity = TxtDivNomCiudad.Text;
                        this.Session["sCity"] = sCity;
                        llenar_combopaísCdad();
                        llenar_comboPaísBuscarCiudad();
                        llenar_combopaísDistrito();
                        llenar_comboPaísbuscarDistrito();
                        llenar_CombopaisBarrio();
                        llenar_combopaísBuscarBarrio();
                        SavelimpiarcontrolesCdad();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Ciudad " + this.Session["sCity"] + " fue creado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesCdad();
                        desactivarControlesCdad();
                    }
                    else
                    {
                        string sCity = "";
                        sCity = TxtDivNomCiudad.Text;
                        this.Session["sCity"] = sCity;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La Ciudad " + this.Session["sCity"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                if (this.Session["duplicado"] == "2")
                {
                    DataTable dtconsulta = odconsultaciudad.ConsultaDuplicados(ConfigurationManager.AppSettings["CiudadDto"], CmbSSDivDept.Text, TxtDivNomCiudad.Text, null);
                    if (dtconsulta == null)
                    {
                        City oCity = new City();
                        ECity oecity = oCity.RegisterCity(TxtDivcodCiudad.Text, parametro, CmbSSDivPais.SelectedValue.ToString(), TxtDivNomCiudad.Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        string sCity = "";
                        sCity = TxtDivNomCiudad.Text;
                        this.Session["sCity"] = sCity;                        
                        llenar_comboPaísBuscarCiudad();
                        llenar_comboPaísbuscarDistrito();                      
                        llenar_combopaísBuscarBarrio();
                        SavelimpiarcontrolesCdad();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Ciudad " + this.Session["sCity"] + " fue creado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesCdad();
                        desactivarControlesCdad();
                    }
                    else
                    {
                        string sCity = "";
                        sCity = TxtDivNomCiudad.Text;
                        this.Session["sCity"] = sCity;
                        this.Session["mensajealert"] = this.Session["sCity"];
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La Ciudad " + this.Session["sCity"] + " Ya Existe";
                        MensajeAlerta();
                    }
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

        protected void btnBuscarCiudad_Click(object sender, EventArgs e)
        {
            desactivarControlesCdad();
            IbtnCiudad_ModalPopupExtender.Hide();
            LblFaltantes.Text = "";
            TxtbCodciud.Text = TxtbCodciud.Text.TrimStart();
            TxtbNomCiud.Text = TxtbNomCiud.Text.TrimStart();
            if (CmbSelPaisCiudad.Text == "0" && (CmbSelDeptoCiudad.Text == "0" || CmbSelDeptoCiudad.Text == "") && TxtbCodciud.Text == "" && TxtbNomCiud.Text == "")
            {
                this.Session["mensajealert"] = "País, Departamento, Ciudad y/o Nombre de Ciudad";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnCiudad_ModalPopupExtender.Show();
                return;
            }
            BuscarActivarbotonesCdad();
            //BtnCrearDivCiudad.Visible = true;
            scodcity = TxtbCodciud.Text;
            snamecity = TxtbNomCiud.Text;
            scodpto = CmbSelDeptoCiudad.Text;
            scodcountry = CmbSelPaisCiudad.Text;
            TxtbCodciud.Text = "";
            TxtbNomCiud.Text = "";
            CmbSelPaisCiudad.Text = "0";
            CmbSelDeptoCiudad.Items.Clear();

            DataTable dtcity = new DataTable();
            City ocity = new City();

            dtcity = ocity.ConsultarCity(scodcountry, scodpto, scodcity, snamecity);


            if (dtcity != null)
            {
                if (dtcity.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtcity.Rows.Count - 1; i++)
                    {
                        llenar_combopaísCdad();
                        CmbSSDivPais.SelectedValue = dtcity.Rows[0]["cod_country"].ToString().Trim();
                        DataTable dtcountry = new DataTable();                      
                        dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSSDivPais.Text);
                        CmbSSDivDept.DataSource = dtcountry;
                        CmbSSDivDept.DataTextField = "Name_dpto";
                        CmbSSDivDept.DataValueField = "cod_dpto";
                        CmbSSDivDept.DataBind();
                        try
                        {
                            CmbSSDivDept.SelectedValue = dtcity.Rows[0]["cod_dpto"].ToString().Trim();
                        }
                        catch
                        {
                            CmbSSDivDept.Items.Clear();
                        }

                        TxtDivcodCiudad.Text = dtcity.Rows[0]["cod_City"].ToString().Trim();
                        TxtDivNomCiudad.Text = dtcity.Rows[0]["Name_City"].ToString().Trim();

                        estado = Convert.ToBoolean(dtcity.Rows[0]["City_Status"].ToString().Trim());
                        if (estado == true)
                        {
                            RbtnEstdivCiudad.Items[0].Selected = true;
                            RbtnEstdivCiudad.Items[1].Selected = false;

                        }
                        else
                        {
                            RbtnEstdivCiudad.Items[0].Selected = false;
                            RbtnEstdivCiudad.Items[1].Selected = true;
                        }

                        this.Session["tcity"] = dtcity;
                        this.Session["i"] = 0;
                    }
                    if (dtcity.Rows.Count == 1)
                    {
                        PregCiudad.Visible = false;
                        AregCiudad.Visible = false;
                        SregCiudad.Visible = false;
                        UregCiudad.Visible = false;
                    }
                    else
                    {
                        PregCiudad.Visible = true;
                        AregCiudad.Visible = true;
                        SregCiudad.Visible = true;
                        UregCiudad.Visible = true;
                    }
                }
                else
                {
                    SavelimpiarcontrolesCdad();
                    llenar_comboPaísbuscarDistrito();
                    SavelimpiarcontrolesPais();
                    TxtbNomCiud.Focus();
                    BuscarActivarbotonesCdad();
                    IbtnCiudad_ModalPopupExtender.Hide();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnCiudad_ModalPopupExtender.Show();
                }
            }
        }

        protected void BtnEditDivCiudad_Click(object sender, EventArgs e)
        {
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", CmbSSDivPais.Text);
            ECountry oescountry = new ECountry();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryDpto = Convert.ToBoolean(dt.Rows[0]["Country_Dpto"].ToString().Trim());
                    oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }
            if (oescountry.CountryDpto == true)
            {
                duplicado = "1";
                parametro = CmbSSDivDept.Text;
                this.Session["parametro"] = parametro;
            }
            else
            {
                duplicado = "2";
                parametro = "";
                this.Session["parametro"] = parametro;
            }
            dt = null;
            oescountry = null;
            this.Session["duplicado"] = duplicado;

            BtnActualizarDivCiudad.Visible = true;
            BtnEditDivCiudad.Visible = false;
            activarControlesCdad();
            TxtCodDistrito.Enabled = false;
            this.Session["rept"] = TxtDivNomCiudad.Text;
        }

        protected void BtnActualizarDivCiudad_Click(object sender, EventArgs e)
        {            
            LblFaltantes.Text = "";
            TxtDivcodCiudad.Text = TxtDivcodCiudad.Text.TrimStart();
            TxtDivNomCiudad.Text = TxtDivNomCiudad.Text.TrimStart();
            if (CmbSSDivPais.Text == "0" || CmbSSDivDept.Text == "0" || TxtDivNomCiudad.Text == "")
            {
                if (CmbSSDivPais.Text == "0")
                {
                    LblFaltantes.Text = ". " + "País";
                }
                if (CmbSSDivDept.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Departamento";
                }
                if (TxtDivNomCiudad.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Ciudad";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
                if (RbtnEstdivCiudad.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabcity = new DAplicacion();
                    DataTable dt = oddeshabcity.PermitirDeshabilitar(ConfigurationManager.AppSettings["CityCity_Planning"], TxtDivcodCiudad.Text);
                    if (dt != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de planning, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt1 = oddeshabcity.PermitirDeshabilitar(ConfigurationManager.AppSettings["CityCommunity"], TxtDivcodCiudad.Text);
                    if (dt1 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Barrio, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt2 = oddeshabcity.PermitirDeshabilitar(ConfigurationManager.AppSettings["CityDistrict"], TxtDivcodCiudad.Text);
                    if (dt2 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Distrito, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt3 = oddeshabcity.PermitirDeshabilitar(ConfigurationManager.AppSettings["CityPointOfSale"], TxtDivcodCiudad.Text);
                    if (dt3 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Puntos de Venta, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                }
                repetido = Convert.ToString(this.Session["rept"]);
                if (this.Session["duplicado"] == "1")
                {
                    if (repetido != TxtDivNomCiudad.Text)
                    {
                        DAplicacion odcity = new DAplicacion();
                        DataTable dtconsulta = odcity.ConsultaDuplicados(ConfigurationManager.AppSettings["Ciudad"], CmbSSDivPais.Text, TxtDivNomCiudad.Text, null);

                        if (dtconsulta == null)
                        {
                            City oCity = new City();
                            ECity oecity = oCity.ActualizarCity(TxtDivcodCiudad.Text, CmbSSDivDept.SelectedValue.ToString(), CmbSSDivPais.SelectedValue.ToString(), TxtDivNomCiudad.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                            snamecity = TxtDivNomCiudad.Text;
                            this.Session["scity"] = snamecity;
                            llenar_comboPaísDpto();
                            SavelimpiarcontrolesCdad();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "La Ciudad : " + this.Session["scity"] + " Se Actualizo con Exito";
                            MensajeAlerta();
                            saveActivarbotonesCdad();
                            desactivarControlesCdad();
                        }
                        else
                        {
                            snamecity = TxtDivNomCiudad.Text;
                            this.Session["scity"] = snamecity;
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "La Ciudad : " + this.Session["scity"] + " No se puede Actualizar este registro ya Existe";
                            MensajeAlerta();
                        }
                    }
                    else
                    {
                        City oCity = new City();

                        ECity oecity = oCity.ActualizarCity(TxtDivcodCiudad.Text, CmbSSDivDept.SelectedValue.ToString(), CmbSSDivPais.SelectedValue.ToString(), TxtDivNomCiudad.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        snamecity = TxtDivNomCiudad.Text;
                        this.Session["scity"] = snamecity;
                        llenar_comboPaísDpto();
                        SavelimpiarcontrolesCdad();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Ciudad : " + this.Session["scity"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesCdad();
                        desactivarControlesCdad();
                    }
                }
                if (this.Session["duplicado"] == "2")
                {

                    if (repetido != TxtDivNomCiudad.Text)
                    {
                        DAplicacion odcity = new DAplicacion();
                        DataTable dtconsulta = odcity.ConsultaDuplicados(ConfigurationManager.AppSettings["CiudadDto"], CmbSSDivDept.Text, TxtDivNomCiudad.Text, null);


                        if (dtconsulta == null)
                        {
                            City oCity = new City();
                            ECity oecity = oCity.ActualizarCity(TxtDivcodCiudad.Text, CmbSSDivDept.SelectedValue.ToString(), CmbSSDivPais.SelectedValue.ToString(), TxtDivNomCiudad.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                            snamecity = TxtDivNomCiudad.Text;
                            this.Session["scity"] = snamecity;
                            llenar_comboPaísDpto();
                            SavelimpiarcontrolesCdad();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "La Ciudad : " + this.Session["scity"] + " Se Actualizo con Exito";
                            MensajeAlerta();
                            saveActivarbotonesCdad();
                            desactivarControlesCdad();
                        }

                        else
                        {
                            snamecity = TxtDivNomCiudad.Text;
                            this.Session["scity"] = snamecity;
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "La Ciudad : " + this.Session["scity"] + " No se puede Actualizar este registro ya Existe";
                            MensajeAlerta();
                        }
                    }
                    else
                    {
                        City oCity = new City();
                        ECity oecity = oCity.ActualizarCity(TxtDivcodCiudad.Text, CmbSSDivDept.SelectedValue.ToString(), CmbSSDivPais.SelectedValue.ToString(), TxtDivNomCiudad.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        snamecity = TxtDivNomCiudad.Text;
                        this.Session["scity"] = snamecity;
                        llenar_comboPaísDpto();
                        SavelimpiarcontrolesCdad();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Ciudad : " + this.Session["scity"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesCdad();
                        desactivarControlesCdad();
                    }
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

        protected void BtnCancelDivCiudad_Click(object sender, EventArgs e)
        {
            SavelimpiarcontrolesCdad();
            saveActivarbotonesCdad();
            desactivarControlesCdad();
        }

        protected void CmbSSDivPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["codPais"] = CmbSSDivPais.Text;
            Get_DivPolitica();
        }

        protected void CmbSelPaisCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //en buscar ciudades
            DataTable dtcountry = new DataTable();
            dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSelPaisCiudad.Text);
            CmbSelDeptoCiudad.DataSource = dtcountry;
            CmbSelDeptoCiudad.DataTextField = "Name_dpto";
            CmbSelDeptoCiudad.DataValueField = "cod_dpto";
            CmbSelDeptoCiudad.DataBind();
            IbtnCiudad_ModalPopupExtender.Show();
        }

        private void MostrarDatosCiudades()
        {
            recorrido = (DataTable)this.Session["tcity"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    CmbSSDivPais.SelectedValue = recorrido.Rows[recsearch]["cod_country"].ToString().Trim();
                    DataTable dtcountry = new DataTable();
                    dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSSDivPais.Text);
                    CmbSSDivDept.DataSource = dtcountry;
                    CmbSSDivDept.DataTextField = "Name_dpto";
                    CmbSSDivDept.DataValueField = "cod_dpto";
                    CmbSSDivDept.DataBind();
                    try
                    {
                        CmbSSDivDept.SelectedValue = recorrido.Rows[recsearch]["cod_dpto"].ToString().Trim();
                    }
                    catch
                    {
                        CmbSSDivDept.Items.Clear();
                    }

                    TxtDivcodCiudad.Text = recorrido.Rows[recsearch]["cod_City"].ToString().Trim();
                    TxtDivNomCiudad.Text = recorrido.Rows[recsearch]["Name_City"].ToString().Trim();

                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["City_Status"].ToString().Trim());
                    if (estado == true)
                    {
                        RbtnEstdivCiudad.Items[0].Selected = true;
                        RbtnEstdivCiudad.Items[1].Selected = false;
                    }
                    else
                    {
                        RbtnEstdivCiudad.Items[0].Selected = false;
                        RbtnEstdivCiudad.Items[1].Selected = true;
                    }
                }
            }
        }

        protected void PregCiudad_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tcity"];
            MostrarDatosCiudades();
        }

        protected void AregCiudad_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosCiudades();
            }
        }

        protected void UregCiudad_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tcity"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosCiudades();
        }

        protected void SregCiudad_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tcity"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosCiudades();
            }
        }

        #endregion

        #region Eventos Distrito  
        protected void BtnCrearDistrito_Click(object sender, EventArgs e)
        {
            llenar_combopaísDistrito();
            activarControlesDistrito();
            crearActivarbotonesDistrito();
        }

        protected void BtnSaveDistrito_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomDistrito.Text = TxtNomDistrito.Text.TrimStart();
            parametro = CmbSDDept.Text;
            parametro2 = CmbSDCiudad.Text;

            if (CmbSDDivPais.Text == "0" || parametro == "0" || parametro2 == "0" || TxtNomDistrito.Text == "")
            {
                if (CmbSDDivPais.Text == "0")
                {
                    LblFaltantes.Text = ". " + "País";
                }
                if (parametro == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Departamento";
                }
                if (parametro2 == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Ciudad";
                }
                if (TxtNomDistrito.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre Distrito";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {                
                DAplicacion oddistrito = new DAplicacion();
                if (this.Session["duplicado"] == "3" || this.Session["duplicado"] == "5")
                {
                    DataTable dtconsulta = oddistrito.ConsultaDuplicados(ConfigurationManager.AppSettings["DistritoCity"], CmbSDCiudad.Text, TxtNomDistrito.Text, null);
                    if (dtconsulta == null)
                    {
                        Distrito oDistrito = new Distrito();
                        EDistrito oedistrito = oDistrito.RegistrarDistrito(TxtCodDistrito.Text, CmbSDDivPais.SelectedValue.ToString(), CmbSDDept.SelectedValue.ToString(), CmbSDCiudad.SelectedValue.ToString(), true, TxtNomDistrito.Text, Convert.ToString(this.Session["sUser"]), DateTime.Now,
                             Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        string sdistrito = "";
                        sdistrito = TxtNomDistrito.Text;
                        this.Session["sdistrito"] = sdistrito;                       
                        llenar_comboPaísbuscarDistrito();
                        llenar_combopaísBuscarBarrio();
                        SavelimpiarcontrolesDistrito();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Distrito " + this.Session["sdistrito"] + " fue creado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesDistrito();
                        desactivarControlesDistrito();
                    }
                    else
                    {
                        string sdistrito = "";
                        sdistrito = TxtNomDistrito.Text;
                        this.Session["sdistrito"] = sdistrito;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Distrito " + this.Session["sdistrito"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                if (this.Session["duplicado"] == "4")
                {
                    DataTable dtconsulta = oddistrito.ConsultaDuplicados(ConfigurationManager.AppSettings["DistritoCountry"], CmbSDDivPais.Text, TxtNomDistrito.Text, null);
                    if (dtconsulta == null)
                    {
                        Distrito oDistrito = new Distrito();
                        EDistrito oedistrito = oDistrito.RegistrarDistrito(TxtCodDistrito.Text, CmbSDDivPais.SelectedValue.ToString(), CmbSDDept.SelectedValue.ToString(), CmbSDCiudad.SelectedValue.ToString(), true, TxtNomDistrito.Text, Convert.ToString(this.Session["sUser"]), DateTime.Now,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        string sdistrito = "";
                        sdistrito = TxtNomDistrito.Text;
                        this.Session["sdistrito"] = sdistrito;
                        llenar_combopaísDistrito();
                        llenar_comboPaísbuscarDistrito();
                        llenar_CombopaisBarrio();
                        llenar_combopaísBuscarBarrio();
                        SavelimpiarcontrolesDistrito();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Distrito " + this.Session["sdistrito"] + " fue creado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesDistrito();
                        desactivarControlesDistrito();
                    }
                    else
                    {
                        string sdistrito = "";
                        sdistrito = TxtNomDistrito.Text;
                        this.Session["sdistrito"] = sdistrito;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Distrito " + this.Session["sdistrito"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                if (this.Session["duplicado"] == "6")
                {
                    DataTable dtconsulta = oddistrito.ConsultaDuplicados(ConfigurationManager.AppSettings["DistritoDepto"], CmbSDDept.Text, TxtNomDistrito.Text, null);
                    if (dtconsulta == null)
                    {
                        Distrito oDistrito = new Distrito();
                        EDistrito oedistrito = oDistrito.RegistrarDistrito(TxtCodDistrito.Text, CmbSDDivPais.SelectedValue.ToString(), CmbSDDept.SelectedValue.ToString(), CmbSDCiudad.SelectedValue.ToString(), estado, TxtNomDistrito.Text, Convert.ToString(this.Session["sUser"]), DateTime.Now,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        string sdistrito = "";
                        sdistrito = TxtNomDistrito.Text;
                        this.Session["sdistrito"] = sdistrito;
                        SavelimpiarcontrolesDistrito();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Distrito " + this.Session["sdistrito"] + " fue creado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesDistrito();
                        desactivarControlesDistrito();
                    }
                    else
                    {
                        string sdistrito = "";
                        sdistrito = TxtNomDistrito.Text;
                        this.Session["sdistrito"] = sdistrito;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Distrito " + this.Session["sdistrito"] + " Ya Existe";
                        MensajeAlerta();
                    }
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

        protected void btnBuscarDistrito_Click(object sender, EventArgs e)
        {
            desactivarControlesDistrito();
            IbtnDistrito_ModalPopupExtender.Hide();
            LblFaltantes.Text = "";
            TxtbNomDistrito.Text = TxtbNomDistrito.Text.TrimStart();

            if (CmbSelPaisDistrito.Text == "0" && CmbSelDeptoDistrito.Text == "0" && CmbSelCiudadDistrito.Text == "" && TxtbCodDistr.Text == "" && TxtbNomDistrito.Text == "")
            {
                this.Session["mensajealert"] = "País, Departamento, Ciudad, Código y/o Nombre de Distrito";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnDistrito_ModalPopupExtender.Show();
                return;
            }

            buscaractivarbotonesDistrito();
            //BtnCrearDistrito.Visible = true;            
            scodto = TxtbCodDistr.Text;
            snamecity = CmbSelCiudadDistrito.Text;
            scodpto = CmbSelDeptoDistrito.Text;
            scodcountry = CmbSelPaisDistrito.Text;
            snamedto = TxtbNomDistrito.Text;
            TxtbNomDistrito.Text = "";
            CmbSelPaisDistrito.Text = "0";
            CmbSelDeptoDistrito.Items.Clear();
            CmbSelCiudadDistrito.Items.Clear();
            TxtbCodDistr.Text = "";
            TxtbNomDistrito.Text = "";
            
            DataTable dto = new DataTable();
            Distrito oDTO = new Distrito(); ;

            dto = oDTO.ConsultarDistrito(scodto, scodcountry, scodpto, snamecity, snamedto);
            if (dto != null)
            {
                if (dto.Rows.Count > 0)
                {
                    llenar_combopaísDistrito();
                    CmbSDDivPais.SelectedValue = dto.Rows[0]["cod_country"].ToString().Trim();
                    DataTable dtcountry = new DataTable();
                    dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSDDivPais.Text);
                    CmbSDDept.DataSource = dtcountry;
                    CmbSDDept.DataTextField = "Name_dpto";
                    CmbSDDept.DataValueField = "cod_dpto";
                    CmbSDDept.DataBind();
                    try
                    {
                        CmbSDDept.SelectedValue = dto.Rows[0]["cod_dpto"].ToString().Trim();
                    }
                    catch
                    {
                        CmbSDDept.Items.Clear();
                    }
                    DataTable dtdepto = new DataTable();
                    dtdepto = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITY", CmbSDDept.Text);
                    CmbSDCiudad.DataSource = dtdepto;
                    CmbSDCiudad.DataTextField = "Name_City";
                    CmbSDCiudad.DataValueField = "cod_City";
                    CmbSDCiudad.DataBind();
                    try
                    {
                        CmbSDCiudad.SelectedValue = dto.Rows[0]["cod_City"].ToString().Trim();
                    }
                    catch
                    {
                        DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITYCONPAIS", CmbSDDivPais.Text);
                        if (dt.Rows.Count > 1)
                        {
                            CmbSDCiudad.DataSource = dt;
                            CmbSDCiudad.DataTextField = "Name_City";
                            CmbSDCiudad.DataValueField = "cod_City";
                            CmbSDCiudad.DataBind();
                            CmbSDCiudad.SelectedValue = dto.Rows[0]["cod_City"].ToString().Trim();
                        }
                        else
                        {
                            CmbSDCiudad.Items.Clear();
                        }
                    }
                    TxtCodDistrito.Text = dto.Rows[0]["cod_District"].ToString().Trim();
                    TxtNomDistrito.Text = dto.Rows[0]["Name_Local"].ToString().Trim();
                    estado = Convert.ToBoolean(dto.Rows[0]["District_Status"].ToString().Trim());
                    if (estado == true)
                    {
                        RBtnEstadoDistrito.Items[0].Selected = true;
                        RBtnEstadoDistrito.Items[1].Selected = false;
                    }
                    else
                    {
                        RBtnEstadoDistrito.Items[0].Selected = false;
                        RBtnEstadoDistrito.Items[1].Selected = true;
                    }
                    this.Session["tdto"] = dto;
                    this.Session["i"] = 0;

                    if (dto.Rows.Count == 1)
                    {
                        PregDistri.Visible = false;
                        AregDistri.Visible = false;
                        SregDistri.Visible = false;
                        UregDistri.Visible = false;
                    }
                    else
                    {
                        PregDistri.Visible = true;
                        AregDistri.Visible = true;
                        SregDistri.Visible = true;
                        UregDistri.Visible = true;
                    }
                }

                else
                {
                    SavelimpiarcontrolesDistrito();
                    llenar_comboPaísDpto();
                    saveActivarbotonesDistrito();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnDistrito_ModalPopupExtender.Show();
                }

            }

        }
        protected void BtnEditDistrito_Click(object sender, EventArgs e)
        {
            //EditarActivarbotonesDistrito();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", CmbSDDivPais.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryDpto = Convert.ToBoolean(dt.Rows[0]["Country_Dpto"].ToString().Trim());
                    oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }

            if (oescountry.CountryDpto == true)
            {
                duplicado = "1";
                parametro = CmbSDDept.Text;
                this.Session["parametro"] = parametro;
                if (oescountry.CountryCiudad == true)
                {
                    duplicado = "5";
                    parametro2 = CmbSDCiudad.Text;
                    this.Session["parametro2"] = parametro2;
                }
                else
                {
                    duplicado = "6";
                    parametro2 = "";
                    this.Session["parametro2"] = parametro2;
                }
            }
            else
            {
                duplicado = "2";
                parametro = "";
                this.Session["parametro"] = parametro;
                if (oescountry.CountryCiudad == true)
                {
                    duplicado = "3";
                    parametro2 = CmbSDCiudad.Text;
                    this.Session["parametro2"] = parametro2;
                }
                else
                {
                    duplicado = "4";
                    parametro2 = "";
                    this.Session["parametro2"] = parametro2;
                }
            }

            dt = null;
            oescountry = null;
            this.Session["duplicado"] = duplicado;
            BtnActualizaDistrito.Visible = true;
            BtnEditDistrito.Visible = false;
            EditarActivarControlesDistrito();
            // aqui van los controles q no se deben habilitar para edicion 
            TxtCodDistrito.Enabled = false;
            this.Session["rept"] = TxtNomDistrito.Text;
        }

        protected void BtnActualizaDistrito_Click(object sender, EventArgs e)
        {                      
            desactivarControlesDistrito();
            LblFaltantes.Text = "";
            TxtCodDistrito.Text = TxtCodDistrito.Text.TrimStart();
            TxtNomDistrito.Text = TxtNomDistrito.Text.TrimStart();

            if (CmbSDDivPais.Text == "0" || parametro == "0" || parametro2 == "0" || TxtNomDistrito.Text == "")
            {
                if (CmbSDDivPais.Text == "0")
                {
                    LblFaltantes.Text = ". " + "País";
                }
                if (parametro == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Departamento";
                }
                if (parametro2 == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Ciudad";
                }
                if (TxtNomDistrito.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre Distrito";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {
                if (RBtnEstadoDistrito.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabdistrito = new DAplicacion();
                    DataTable dt = oddeshabdistrito.PermitirDeshabilitar(ConfigurationManager.AppSettings["DistrictCommunity"], TxtCodDistrito.Text);
                    if (dt != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Barrio, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt1 = oddeshabdistrito.PermitirDeshabilitar(ConfigurationManager.AppSettings["DistrictPointOfSale"], TxtCodDistrito.Text);
                    if (dt1 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Puntos de Venta, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                }
                repetido = Convert.ToString(this.Session["rept"]);
                if (this.Session["duplicado"] == "3" || this.Session["duplicado"] == "5")
                {
                    if (repetido != TxtNomDistrito.Text)
                    {
                        DAplicacion odcity = new DAplicacion();
                        DataTable dtconsulta = odcity.ConsultaDuplicados(ConfigurationManager.AppSettings["DistritoCity"], CmbSDCiudad.Text, TxtNomDistrito.Text, null);

                        if (dtconsulta == null)
                        {
                            Distrito oDistrito = new Distrito();
                            EDistrito oedistrito = oDistrito.ActualizarDistrito(TxtCodDistrito.Text, CmbSDDivPais.SelectedValue.ToString(), CmbSDDept.SelectedValue.ToString(), CmbSDCiudad.SelectedValue.ToString(), estado, TxtNomDistrito.Text, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            
                            string sdistrito = "";
                            sdistrito = TxtNomDistrito.Text;
                            this.Session["sdistrito"] = sdistrito;
                            SavelimpiarcontrolesDistrito();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "El Distrito : " + this.Session["sdistrito"] + " Se Actualizo con Exito";
                            MensajeAlerta();
                            saveActivarbotonesDistrito();
                        }

                        else
                        {
                            string sdistrito = "";
                            sdistrito = TxtNomDistrito.Text;
                            this.Session["sdistrito"] = sdistrito;
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Distrito : " + this.Session["sdistrito"] + " No se puede Actualizar este registro ya Existe";
                            MensajeAlerta();
                        }
                    }
                    else
                    {
                        Distrito oDistrito = new Distrito();
                        EDistrito oedistrito = oDistrito.ActualizarDistrito(TxtCodDistrito.Text, CmbSDDivPais.SelectedValue.ToString(), CmbSDDept.SelectedValue.ToString(), CmbSDCiudad.SelectedValue.ToString(), estado, TxtNomDistrito.Text, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        string sdistrito = "";
                        sdistrito = TxtNomDistrito.Text;
                        this.Session["sdistrito"] = sdistrito;
                        SavelimpiarcontrolesDistrito();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Distrito : " + this.Session["sdistrito"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesDistrito();                        
                    }
                }
                if (this.Session["duplicado"] == "4")
                {
                    if (repetido != TxtNomDistrito.Text)
                    {
                        DAplicacion odcity = new DAplicacion();
                        DataTable dtconsulta = odcity.ConsultaDuplicados(ConfigurationManager.AppSettings["DistritoCountry"], CmbSDDivPais.Text, TxtNomDistrito.Text, null);


                        if (dtconsulta == null)
                        {
                            Distrito oDistrito = new Distrito();
                            EDistrito oedistrito = oDistrito.ActualizarDistrito(TxtCodDistrito.Text, CmbSDDivPais.SelectedValue.ToString(), CmbSDDept.SelectedValue.ToString(), CmbSDCiudad.SelectedValue.ToString(), estado, TxtNomDistrito.Text, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                            string sdistrito = "";
                            sdistrito = TxtNomDistrito.Text;
                            this.Session["sdistrito"] = sdistrito;
                            SavelimpiarcontrolesDistrito();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "El Distrito : " + this.Session["sdistrito"] + " Se Actualizo con Exito";
                            MensajeAlerta();
                            saveActivarbotonesDistrito();                            
                        }

                        else
                        {
                            string sdistrito = "";
                            sdistrito = TxtNomDistrito.Text;
                            this.Session["sdistrito"] = sdistrito;
                            this.Session["mensajealert"] = "El Distrito : " + this.Session["sdistrito"];
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Distrito : " + this.Session["sdistrito"] + " No se puede Actualizar este registro ya Existe";
                            MensajeAlerta();
                        }
                    }
                    else
                    {
                        Distrito oDistrito = new Distrito();
                        EDistrito oedistrito = oDistrito.ActualizarDistrito(TxtCodDistrito.Text, CmbSDDivPais.SelectedValue.ToString(), CmbSDDept.SelectedValue.ToString(), CmbSDCiudad.SelectedValue.ToString(), estado, TxtNomDistrito.Text, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        string sdistrito = "";
                        sdistrito = TxtNomDistrito.Text;
                        this.Session["sdistrito"] = sdistrito;
                        SavelimpiarcontrolesDistrito();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Distrito : " + this.Session["sdistrito"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesDistrito(); 
                    }
                }
                if (this.Session["duplicado"] == "6")
                {
                    if (repetido != TxtNomDistrito.Text)
                    {
                        DAplicacion odcity = new DAplicacion();
                        DataTable dtconsulta = odcity.ConsultaDuplicados(ConfigurationManager.AppSettings["DistritoDepto"], CmbSDDept.Text, TxtNomDistrito.Text, null);

                        if (dtconsulta == null)
                        {
                            Distrito oDistrito = new Distrito();
                            EDistrito oedistrito = oDistrito.ActualizarDistrito(TxtCodDistrito.Text, CmbSDDivPais.SelectedValue.ToString(), CmbSDDept.SelectedValue.ToString(), CmbSDCiudad.SelectedValue.ToString(), estado, TxtNomDistrito.Text, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                            string sdistrito = "";
                            sdistrito = TxtNomDistrito.Text;
                            this.Session["sdistrito"] = sdistrito;
                            SavelimpiarcontrolesDistrito();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "El Distrito : " + this.Session["sdistrito"] + " Se Actualizo con Exito";
                            MensajeAlerta();
                            saveActivarbotonesDistrito();                            
                        }
                        else
                        {
                            string sdistrito = "";
                            sdistrito = TxtNomDistrito.Text;
                            this.Session["sdistrito"] = sdistrito;
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Distrito : " + this.Session["sdistrito"] + " No se puede Actualizar este registro ya Existe";
                            MensajeAlerta();
                        }
                    }
                    else
                    {
                        Distrito oDistrito = new Distrito();
                        EDistrito oedistrito = oDistrito.ActualizarDistrito(TxtCodDistrito.Text, CmbSDDivPais.SelectedValue.ToString(), CmbSDDept.SelectedValue.ToString(), CmbSDCiudad.SelectedValue.ToString(), estado, TxtNomDistrito.Text, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        string sdistrito = "";
                        sdistrito = TxtNomDistrito.Text;
                        this.Session["sdistrito"] = sdistrito;
                        SavelimpiarcontrolesDistrito();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Distrito : " + this.Session["sdistrito"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesDistrito();                        
                    }
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

        protected void BtnCancelDistrito_Click(object sender, EventArgs e)
        {
            SavelimpiarcontrolesDistrito();
            saveActivarbotonesDistrito();
            desactivarControlesDistrito();
        }        

        protected void CmbSDDivPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbSDDept.Items.Clear();
            CmbSDCiudad.Items.Clear();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", CmbSDDivPais.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryDpto = Convert.ToBoolean(dt.Rows[0]["Country_Dpto"].ToString().Trim());
                    oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }
            if (oescountry.CountryDpto == true)
            {
                //en view distritos
                DataTable dtcountry = new DataTable();
                dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSDDivPais.Text);
                if (dtcountry != null)
                {
                    if (dtcountry.Rows.Count > 1)
                    {
                        CmbSDDept.DataSource = dtcountry;
                        CmbSDDept.DataTextField = "Name_dpto";
                        CmbSDDept.DataValueField = "cod_dpto";
                        CmbSDDept.DataBind();
                    }

                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Distrito para este país. Falta crear el(los) Departamentos.";
                        MensajeAlerta();
                        CmbSDDept.DataSource = dtcountry;
                        CmbSDDept.DataTextField = "Name_dpto";
                        CmbSDDept.DataValueField = "cod_dpto";
                        CmbSDDept.DataBind();
                    }
                    duplicado = "1";
                    parametro = CmbSDDept.Text;
                    //CmbSDDept.Enabled = false;
                    this.Session["parametro"] = parametro;
                }
                dtcountry = null;
            }
            else
            {
                duplicado = "2";
                parametro = "";
                CmbSDDept.Items.Clear();
                //CmbSDDept.Enabled = true;
                this.Session["parametro"] = parametro;
                if (oescountry.CountryCiudad == true)
                {
                    //en view distritos
                    DataTable dtcity = new DataTable();
                    dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITYCONPAIS", CmbSDDivPais.Text);
                    if (dtcity.Rows.Count > 1)
                    {
                        CmbSDCiudad.DataSource = dtcity;
                        CmbSDCiudad.DataTextField = "Name_City";
                        CmbSDCiudad.DataValueField = "cod_City";
                        CmbSDCiudad.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Distrito para este país. Falta crear el(los) Departamentos.";
                        MensajeAlerta();
                        CmbSDCiudad.DataSource = dtcity;
                        CmbSDCiudad.DataTextField = "Name_City";
                        CmbSDCiudad.DataValueField = "cod_City";
                        CmbSDCiudad.DataBind();
                    }
                    duplicado = "3";
                    parametro2 = CmbSDCiudad.Text;                    
                    this.Session["parametro2"] = parametro2;
                    dtcity = null;
                }
                else
                {
                    duplicado = "4";
                    parametro2 = "";
                    CmbSDCiudad.Items.Clear();                    
                    this.Session["parametro2"] = parametro2;
                }
            }
            dt = null;
            oescountry = null;
            this.Session["duplicado"] = duplicado;
        }

        protected void CmbSDDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbSDCiudad.Items.Clear();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", CmbSDDivPais.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryDpto = Convert.ToBoolean(dt.Rows[0]["Country_Dpto"].ToString().Trim());
                    oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }
            if (oescountry.CountryCiudad == true)
            {
                //en view distritos
                DataTable dtcountry = new DataTable();
                dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITY", CmbSDDept.Text);
                if (dtcountry != null)
                {
                    if (dtcountry.Rows.Count > 1)
                    {
                        CmbSDCiudad.DataSource = dtcountry;
                        CmbSDCiudad.DataTextField = "Name_City";
                        CmbSDCiudad.DataValueField = "cod_City";
                        CmbSDCiudad.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Distrito para este país. Falta crear el(las) Ciudades.";
                        MensajeAlerta();
                        CmbSDCiudad.DataSource = dtcountry;
                        CmbSDCiudad.DataTextField = "Name_City";
                        CmbSDCiudad.DataValueField = "cod_City";
                        CmbSDCiudad.DataBind();
                    }
                    duplicado = "5";
                    parametro = CmbSDDept.Text;
                    parametro2 = CmbSDCiudad.Text;
                    this.Session["parametro"] = parametro;
                    this.Session["parametro2"] = parametro2;
                }
            }
            else
            {
                duplicado = "6";
                parametro = CmbSDDept.Text;
                parametro2 = "";
                CmbSDCiudad.Items.Clear();
                this.Session["parametro"] = parametro;
                this.Session["parametro2"] = parametro2;
            }

            dt = null;
            oescountry = null;
            this.Session["duplicado"] = duplicado;
        }

        protected void CmbSelPaisDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbSelDeptoDistrito.Items.Clear();
            CmbSelCiudadDistrito.Items.Clear();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", CmbSelPaisDistrito.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryDpto = Convert.ToBoolean(dt.Rows[0]["Country_Dpto"].ToString().Trim());
                    oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }
            if (oescountry.CountryDpto == true)
            {
                //en buscar distritos
                DataTable dtcountry = new DataTable();
                dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSelPaisDistrito.Text);
                if (dtcountry != null)
                {
                    if (dtcountry.Rows.Count > 1)
                    {
                        CmbSelDeptoDistrito.DataSource = dtcountry;
                        CmbSelDeptoDistrito.DataTextField = "Name_dpto";
                        CmbSelDeptoDistrito.DataValueField = "cod_dpto";
                        CmbSelDeptoDistrito.DataBind();
                    }

                    //else
                    //{
                    //    Alertas.CssClass = "MensajesError";
                    //    LblFaltantes.Text = "No es posible crear Ciudad para este país. Falta crear el(los) Departamentos.";
                    //    MensajeAlerta();
                    //    CmbSelDeptoDistrito.DataSource = dtcountry;
                    //    CmbSelDeptoDistrito.DataTextField = "Name_dpto";
                    //    CmbSelDeptoDistrito.DataValueField = "cod_dpto";
                    //    CmbSelDeptoDistrito.DataBind();
                    //}
                    duplicado = "1";
                    parametro = CmbSelDeptoDistrito.Text;
                    this.Session["parametro"] = parametro;
                }
                dtcountry = null;
            }
            else
            {
                duplicado = "2";
                parametro = "";
                CmbSelDeptoDistrito.Items.Clear();
                this.Session["parametro"] = parametro;
                if (oescountry.CountryCiudad == true)
                {
                    //en view distritos
                    DataTable dtcity = new DataTable();
                    dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITYCONPAIS", CmbSelPaisDistrito.Text);
                    if (dtcity.Rows.Count > 1)
                    {
                        CmbSelCiudadDistrito.DataSource = dtcity;
                        CmbSelCiudadDistrito.DataTextField = "Name_City";
                        CmbSelCiudadDistrito.DataValueField = "cod_City";
                        CmbSelCiudadDistrito.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Distrito para este país. Falta crear el(las) Ciudades.";
                        MensajeAlerta();
                        CmbSelCiudadDistrito.DataSource = dtcity;
                        CmbSelCiudadDistrito.DataTextField = "Name_City";
                        CmbSelCiudadDistrito.DataValueField = "cod_City";
                        CmbSelCiudadDistrito.DataBind();
                    }
                    duplicado = "3";
                    parametro2 = CmbSelCiudadDistrito.Text;
                    this.Session["parametro2"] = parametro2;
                    dtcity = null;
                }
                else
                {
                    duplicado = "4";
                    parametro2 = "";
                    CmbSelCiudadDistrito.Items.Clear();
                    this.Session["parametro2"] = parametro2;
                }
            }
            dt = null;
            oescountry = null;
            this.Session["duplicado"] = duplicado;
            IbtnDistrito_ModalPopupExtender.Show();
        }

        protected void CmbSelDeptoDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbSelCiudadDistrito.Items.Clear();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", CmbSelPaisDistrito.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryDpto = Convert.ToBoolean(dt.Rows[0]["Country_Dpto"].ToString().Trim());
                    oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }

            if (oescountry.CountryCiudad == true)
            {
                //en buscar distritos
                DataTable dtcountry = new DataTable();
                dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITY", CmbSelDeptoDistrito.Text);
                if (dtcountry != null)
                {
                    if (dtcountry.Rows.Count > 1)
                    {
                        CmbSelCiudadDistrito.DataSource = dtcountry;
                        CmbSelCiudadDistrito.DataTextField = "Name_City";
                        CmbSelCiudadDistrito.DataValueField = "cod_City";
                        CmbSelCiudadDistrito.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Distrito para este país. Falta crear el(las) Ciudades.";
                        MensajeAlerta();
                        CmbSelCiudadDistrito.DataSource = dtcountry;
                        CmbSelCiudadDistrito.DataTextField = "Name_City";
                        CmbSelCiudadDistrito.DataValueField = "cod_City";
                        CmbSelCiudadDistrito.DataBind();
                    }
                    duplicado = "5";
                    parametro = CmbSelDeptoDistrito.Text;
                    parametro2 = CmbSelCiudadDistrito.Text;
                    this.Session["parametro"] = parametro;
                    this.Session["parametro2"] = parametro2;
                }
            }
            else
            {
                duplicado = "6";
                parametro = CmbSelDeptoDistrito.Text;
                parametro2 = "";
                CmbSelCiudadDistrito.Items.Clear();
                this.Session["parametro"] = parametro;
                this.Session["parametro2"] = parametro2;
            }

            dt = null;
            oescountry = null;
            this.Session["duplicado"] = duplicado;
            IbtnDistrito_ModalPopupExtender.Show();

        }
        private void MostrarDatosDIstrito()
        {
            recorrido = (DataTable)this.Session["tdto"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    CmbSDDivPais.SelectedValue = recorrido.Rows[recsearch]["cod_country"].ToString().Trim();
                    DataTable dtcountry = new DataTable();
                    dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSDDivPais.Text);
                    CmbSDDept.DataSource = dtcountry;
                    CmbSDDept.DataTextField = "Name_dpto";
                    CmbSDDept.DataValueField = "cod_dpto";
                    CmbSDDept.DataBind();
                    try
                    {
                        CmbSDDept.SelectedValue = recorrido.Rows[recsearch]["cod_dpto"].ToString().Trim();
                    }
                    catch
                    {
                        CmbSDDept.Items.Clear();
                    }
                    DataTable dtdepto = new DataTable();
                    dtdepto = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITY", CmbSDDept.Text);
                    CmbSDCiudad.DataSource = dtdepto;
                    CmbSDCiudad.DataTextField = "Name_City";
                    CmbSDCiudad.DataValueField = "cod_City";
                    CmbSDCiudad.DataBind();
                    try
                    {
                        CmbSDCiudad.SelectedValue = recorrido.Rows[recsearch]["cod_City"].ToString().Trim();
                    }
                    catch
                    {
                        CmbSDCiudad.Items.Clear();
                    }
                    TxtCodDistrito.Text = recorrido.Rows[recsearch]["cod_District"].ToString().Trim();
                    TxtNomDistrito.Text = recorrido.Rows[recsearch]["Name_Local"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["District_Status"].ToString().Trim());
                    if (estado == true)
                    {
                        RBtnEstadoDistrito.Items[0].Selected = true;
                        RBtnEstadoDistrito.Items[1].Selected = false;
                    }
                    else
                    {
                        RBtnEstadoDistrito.Items[0].Selected = false;
                        RBtnEstadoDistrito.Items[1].Selected = true;
                    }
                }
            }
        }

        protected void PregDistri_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tdto"];
            MostrarDatosDIstrito();
        }

        protected void AregDistri_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosDIstrito();
            }
        }

        protected void SregDistri_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tdto"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosDIstrito();
            }
        }

        protected void UregDistri_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tdto"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosDIstrito();
        }

        #endregion

        #region Eventos Barrios
        protected void BtnCrearBarrios_Click(object sender, EventArgs e)
        {
            llenar_CombopaisBarrio();
            activarControlesBarrio();
            crearActivarbotonesBarrio();
        }

        protected void BtnSaveBarrios_Click(object sender, EventArgs e)
        {        
            desactivarControlesBarrio();
            LblFaltantes.Text = "";
            TxtNomBarr.Text = TxtNomBarr.Text.TrimStart();
            parametro = CmbSBDept.Text;
            parametro2 = CmbSBciudad.Text;
            parametro3 = CmbSBdistr.Text;

            if (CmbSBpais.Text == "0" || parametro == "0" || parametro2 == "0" || parametro3 == "0" || TxtNomBarr.Text == "")
            {
                if (CmbSBpais.Text == "0")
                {
                    LblFaltantes.Text = ". " + "País";
                }

                if (parametro == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Departamento";
                }

                if (parametro2 == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Ciudad";
                }

                if (parametro3 == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Distrito";
                }

                if (TxtNomBarr.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Barrio";
                }

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
               
                DAplicacion odbarrio = new DAplicacion();
                if (this.Session["duplicado"] == "5" || this.Session["duplicado"] == "9" || this.Session["duplicado"] == "11")
                {
                    DataTable dtconsulta = odbarrio.ConsultaDuplicados(ConfigurationManager.AppSettings["BarrioDistrict"], CmbSBdistr.Text, TxtNomBarr.Text, null);
                    if (dtconsulta == null)
                    {
                        //if (CmbSBDept.Text == "0" || CmbSBDept.Text == "")
                        //{
                        //    sDeptoVacio = "0";
                        //}
                        //else
                        //{
                        //    sDeptoVacio = CmbSBDept.Text;
                        //}
                        //if (CmbSBciudad.Text == "0" || CmbSBciudad.Text == "")
                        //{
                        //    sCiudadVacio = "0";
                        //}
                        //else
                        //{
                        //    sCiudadVacio = CmbSBciudad.Text;
                        //}
                        //if (CmbSBdistr.Text == "0" || CmbSBdistr.Text == "")
                        //{
                        //    sDistritoVacio = "0";
                        //}
                        //else
                        //{
                        //    sDistritoVacio = CmbSBdistr.Text;
                        //}

                        EBarrio oebarrio = oBarrio.RegistrarBarrio(TxtCodBarr.Text, CmbSBpais.Text, CmbSBDept.Text, CmbSBciudad.Text,
                        CmbSBdistr.Text, TxtNomBarr.Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        string sbarrio = "";
                        sbarrio = TxtNomBarr.Text;
                        this.Session["sbarrio"] = sbarrio;
                        llenar_combopaísBuscarBarrio();
                        SavelimpiarcontrolesBarrio();
                        //DataSet dscountry = new DataSet();
                        //dscountry = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSBpais.Text);
                        //CmbSBDept.DataSource = dscountry;
                        //CmbSBDept.DataTextField = "Name_dpto";
                        //CmbSBDept.DataValueField = "cod_dpto";
                        //CmbSBDept.DataBind();
                        //dscountry = null;
                        //DataSet dscity = new DataSet();
                        //dscity = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCITY", CmbSBDept.Text);
                        //CmbSBciudad.DataSource = dscity;
                        //CmbSBciudad.DataTextField = "Name_City";
                        //CmbSBciudad.DataValueField = "cod_City";
                        //CmbSBciudad.DataBind();
                        //dscity = null;
                        //DataSet dsDistrict = new DataSet();
                        //dsDistrict = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSDISTRITO", CmbSBciudad.Text);
                        //CmbSBdistr.DataSource = dsDistrict;
                        //CmbSBdistr.DataTextField = "Name_Local";
                        //CmbSBdistr.DataValueField = "cod_District";
                        //CmbSBdistr.DataBind();
                        //dsDistrict = null;
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Barrio " + this.Session["sbarrio"] + " fue creado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesBarrio();
                    }
                    else
                    {
                        string sbarrio = "";
                        sbarrio = TxtNomBarr.Text;
                        this.Session["sbarrio"] = sbarrio;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Barrio " + this.Session["sbarrio"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                if (this.Session["duplicado"] == "12")
                {
                    DataTable dtconsulta = odbarrio.ConsultaDuplicados(ConfigurationManager.AppSettings["BarrioCity"], CmbSBciudad.Text, TxtNomBarr.Text, null);
                    if (dtconsulta == null)
                    {
                        //if (CmbSBDept.Text == "0" || CmbSBDept.Text == "")
                        //{
                        //    sDeptoVacio = "0";
                        //}
                        //else
                        //{
                        //    sDeptoVacio = CmbSBDept.Text;
                        //}
                        //if (CmbSBciudad.Text == "0" || CmbSBciudad.Text == "")
                        //{
                        //    sCiudadVacio = "0";
                        //}
                        //else
                        //{
                        //    sCiudadVacio = CmbSBciudad.Text;
                        //}
                        //if (CmbSBdistr.Text == "0" || CmbSBdistr.Text == "")
                        //{
                        //    sDistritoVacio = "0";
                        //}
                        //else
                        //{
                        //    sDistritoVacio = CmbSBdistr.Text;
                        //}

                        EBarrio oebarrio = oBarrio.RegistrarBarrio(TxtCodBarr.Text, CmbSBpais.Text, CmbSBDept.Text, CmbSBciudad.Text,
                        CmbSBdistr.Text, TxtNomBarr.Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        string sbarrio = "";
                        sbarrio = TxtNomBarr.Text;
                        this.Session["sbarrio"] = sbarrio;
                        llenar_combopaísBuscarBarrio();
                        SavelimpiarcontrolesBarrio();
                        //DataSet dscountry = new DataSet();
                        //dscountry = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSBpais.Text);
                        //CmbSBDept.DataSource = dscountry;
                        //CmbSBDept.DataTextField = "Name_dpto";
                        //CmbSBDept.DataValueField = "cod_dpto";
                        //CmbSBDept.DataBind();
                        //dscountry = null;
                        //DataSet dscity = new DataSet();
                        //dscity = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCITY", CmbSBDept.Text);
                        //CmbSBciudad.DataSource = dscity;
                        //CmbSBciudad.DataTextField = "Name_City";
                        //CmbSBciudad.DataValueField = "cod_City";
                        //CmbSBciudad.DataBind();
                        //dscity = null;
                        //DataSet dsDistrict = new DataSet();
                        //dsDistrict = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSDISTRITO", CmbSBciudad.Text);
                        //CmbSBdistr.DataSource = dsDistrict;
                        //CmbSBdistr.DataTextField = "Name_Local";
                        //CmbSBdistr.DataValueField = "cod_District";
                        //CmbSBdistr.DataBind();
                        //dsDistrict = null;
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Barrio " + this.Session["sbarrio"] + " fue creado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesBarrio();
                    }
                    else
                    {
                        string sbarrio = "";
                        sbarrio = TxtNomBarr.Text;
                        this.Session["sbarrio"] = sbarrio;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Barrio " + this.Session["sbarrio"] + " Ya Existe";
                        MensajeAlerta();
                    }

                }
                if (this.Session["duplicado"] == "10")
                {
                    DataTable dtconsulta = odbarrio.ConsultaDuplicados(ConfigurationManager.AppSettings["BarrioDpto"], CmbSBDept.Text, TxtNomBarr.Text, null);
                    if (dtconsulta == null)
                    {
                        //if (CmbSBDept.Text == "0" || CmbSBDept.Text == "")
                        //{
                        //    sDeptoVacio = "0";
                        //}
                        //else
                        //{
                        //    sDeptoVacio = CmbSBDept.Text;
                        //}
                        //if (CmbSBciudad.Text == "0" || CmbSBciudad.Text == "")
                        //{
                        //    sCiudadVacio = "0";
                        //}
                        //else
                        //{
                        //    sCiudadVacio = CmbSBciudad.Text;
                        //}
                        //if (CmbSBdistr.Text == "0" || CmbSBdistr.Text == "")
                        //{
                        //    sDistritoVacio = "0";
                        //}
                        //else
                        //{
                        //    sDistritoVacio = CmbSBdistr.Text;
                        //}

                        EBarrio oebarrio = oBarrio.RegistrarBarrio(TxtCodBarr.Text, CmbSBpais.Text, CmbSBDept.Text, CmbSBciudad.Text,
                        CmbSBdistr.Text, TxtNomBarr.Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        string sbarrio = "";
                        sbarrio = TxtNomBarr.Text;
                        this.Session["sbarrio"] = sbarrio;
                        llenar_combopaísBuscarBarrio();
                        SavelimpiarcontrolesBarrio();
                        //DataSet dscountry = new DataSet();
                        //dscountry = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSBpais.Text);
                        //CmbSBDept.DataSource = dscountry;
                        //CmbSBDept.DataTextField = "Name_dpto";
                        //CmbSBDept.DataValueField = "cod_dpto";
                        //CmbSBDept.DataBind();
                        //dscountry = null;
                        //DataSet dscity = new DataSet();
                        //dscity = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCITY", CmbSBDept.Text);
                        //CmbSBciudad.DataSource = dscity;
                        //CmbSBciudad.DataTextField = "Name_City";
                        //CmbSBciudad.DataValueField = "cod_City";
                        //CmbSBciudad.DataBind();
                        //dscity = null;
                        //DataSet dsDistrict = new DataSet();
                        //dsDistrict = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSDISTRITO", CmbSBciudad.Text);
                        //CmbSBdistr.DataSource = dsDistrict;
                        //CmbSBdistr.DataTextField = "Name_Local";
                        //CmbSBdistr.DataValueField = "cod_District";
                        //CmbSBdistr.DataBind();
                        //dsDistrict = null;
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Barrio " + this.Session["sbarrio"] + " fue creado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesBarrio();
                    }
                    else
                    {
                        string sbarrio = "";
                        sbarrio = TxtNomBarr.Text;
                        this.Session["sbarrio"] = sbarrio;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Barrio " + this.Session["sbarrio"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                if (this.Session["duplicado"] == "6")
                {
                    DataTable dtconsulta = odbarrio.ConsultaDuplicados(ConfigurationManager.AppSettings["BarrioCountry"], CmbSBpais.Text, TxtNomBarr.Text, null);
                    if (dtconsulta == null)
                    {
                        //if (CmbSBDept.Text == "0" || CmbSBDept.Text == "")
                        //{
                        //    sDeptoVacio = "0";
                        //}
                        //else
                        //{
                        //    sDeptoVacio = CmbSBDept.Text;
                        //}
                        //if (CmbSBciudad.Text == "0" || CmbSBciudad.Text == "")
                        //{
                        //    sCiudadVacio = "0";
                        //}
                        //else
                        //{
                        //    sCiudadVacio = CmbSBciudad.Text;
                        //}
                        //if (CmbSBdistr.Text == "0" || CmbSBdistr.Text == "")
                        //{
                        //    sDistritoVacio = "0";
                        //}
                        //else
                        //{
                        //    sDistritoVacio = CmbSBdistr.Text;
                        //}

                        EBarrio oebarrio = oBarrio.RegistrarBarrio(TxtCodBarr.Text, CmbSBpais.Text, CmbSBDept.Text, CmbSBciudad.Text,
                        CmbSBdistr.Text, TxtNomBarr.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        string sbarrio = "";
                        sbarrio = TxtNomBarr.Text;
                        this.Session["sbarrio"] = sbarrio;
                        llenar_combopaísBuscarBarrio();
                        SavelimpiarcontrolesBarrio();
                        //DataSet dscountry = new DataSet();
                        //dscountry = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSBpais.Text);
                        //CmbSBDept.DataSource = dscountry;
                        //CmbSBDept.DataTextField = "Name_dpto";
                        //CmbSBDept.DataValueField = "cod_dpto";
                        //CmbSBDept.DataBind();
                        //dscountry = null;
                        //DataSet dscity = new DataSet();
                        //dscity = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCITY", CmbSBDept.Text);
                        //CmbSBciudad.DataSource = dscity;
                        //CmbSBciudad.DataTextField = "Name_City";
                        //CmbSBciudad.DataValueField = "cod_City";
                        //CmbSBciudad.DataBind();
                        //dscity = null;
                        //DataSet dsDistrict = new DataSet();
                        //dsDistrict = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSDISTRITO", CmbSBciudad.Text);
                        //CmbSBdistr.DataSource = dsDistrict;
                        //CmbSBdistr.DataTextField = "Name_Local";
                        //CmbSBdistr.DataValueField = "cod_District";
                        //CmbSBdistr.DataBind();
                        //dsDistrict = null;
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Barrio " + this.Session["sbarrio"] + " fue creado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesBarrio();
                    }
                    else
                    {
                        string sbarrio = "";
                        sbarrio = TxtNomBarr.Text;
                        this.Session["sbarrio"] = sbarrio;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Barrio " + this.Session["sbarrio"] + " Ya Existe";
                        MensajeAlerta();
                    }
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

        protected void btnBuscarBarrio_Click(object sender, EventArgs e)
        {
            desactivarControlesBarrio();
            IbtnBarrio_ModalPopupExtender.Hide();
            LblFaltantes.Text = "";
            TxtbNomBarrio.Text = TxtbNomBarrio.Text.TrimStart();
            if (CmbSelPaisBarrio.Text == "0" && TxtbNomBarrio.Text == "")
            {
                this.Session["mensajealert"] = "Mínimo debe tener Código de país y Nombre de Barrio";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnBarrio_ModalPopupExtender.Show();
                return;
            }
            BuscarActivarbotonesBarrio();
            //BtnCrearBarrios.Visible = true;
            scodcountry = CmbSelPaisBarrio.Text;
            if (CmbSelDeptoBarrio.Text == "0" || CmbSelDeptoBarrio.Text == "")
            {
                sDeptoVacio = "0";
            }
            else
            {
                sDeptoVacio = CmbSelDeptoBarrio.Text;
            }
            if (CmbSelCiudadBarrio.Text == "0" || CmbSelCiudadBarrio.Text == "")
            {
                sCiudadVacio = "0";
            }
            else
            {
                sCiudadVacio = CmbSelCiudadBarrio.Text;
            }
            if (CmbSelDistritoBarrio.Text == "0" || CmbSelDistritoBarrio.Text == "")
            {
                sDistritoVacio = "0";
            }
            else
            {
                sDistritoVacio = CmbSelDistritoBarrio.Text;
            }
            scodbarrio = TxtCodBarr.Text;
            snamebarrio = TxtbNomBarrio.Text;
            CmbSelPaisBarrio.Text = "0";
            CmbSelDeptoBarrio.Items.Clear();
            CmbSelCiudadBarrio.Items.Clear();
            CmbSelDistritoBarrio.Items.Clear();
            TxtbNomBarrio.Text = "";

            Barrio oBarrio = new Barrio();
            barrio = oBarrio.ConsultarBarrio(scodcountry, sDeptoVacio, sCiudadVacio, sDistritoVacio, snamebarrio);
            if (barrio != null)
            {
                if (barrio.Rows.Count > 0)
                {
                    llenar_CombopaisBarrio();
                    CmbSBpais.SelectedValue = barrio.Rows[0]["cod_country"].ToString().Trim();
                    DataSet dscountry = new DataSet();
                    dscountry = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSBpais.Text);

                    CmbSBDept.DataSource = dscountry;
                    CmbSBDept.DataTextField = "Name_dpto";
                    CmbSBDept.DataValueField = "cod_dpto";
                    CmbSBDept.DataBind();
                    dscountry = null;

                    CmbSBDept.SelectedValue = barrio.Rows[0]["cod_dpto"].ToString().Trim();
                    DataSet dscity = new DataSet();
                    dscity = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCITY", CmbSBDept.Text);

                    CmbSBciudad.DataSource = dscity;
                    CmbSBciudad.DataTextField = "Name_City";
                    CmbSBciudad.DataValueField = "cod_City";
                    CmbSBciudad.DataBind();
                    dscity = null;

                    CmbSBciudad.SelectedValue = barrio.Rows[0]["cod_City"].ToString().Trim();
                    DataSet dsDistrict = new DataSet();
                    dsDistrict = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSDISTRITO", CmbSBciudad.Text);
                    CmbSBdistr.DataSource = dsDistrict;
                    CmbSBdistr.DataTextField = "Name_Local";
                    CmbSBdistr.DataValueField = "cod_District";
                    CmbSBdistr.DataBind();
                    dsDistrict = null;

                    CmbSBdistr.SelectedValue = barrio.Rows[0]["cod_District"].ToString().Trim();
               
                    TxtCodBarr.Text = barrio.Rows[0]["cod_Community"].ToString().Trim();
                    TxtNomBarr.Text = barrio.Rows[0]["Name_Community"].ToString().Trim();
                    
                    estado = Convert.ToBoolean(barrio.Rows[0]["Comunity_Status"].ToString().Trim());
                    if (estado == true)
                    {
                        RbtnEstadoBarrios.Items[0].Selected = true;
                        RbtnEstadoBarrios.Items[1].Selected = false;
                    }
                    else
                    {
                        RbtnEstadoBarrios.Items[0].Selected = false;
                        RbtnEstadoBarrios.Items[1].Selected = true;
                    }
                    this.Session["tbarrio"] = barrio;
                    this.Session["i"] = 0;

                    if (barrio.Rows.Count == 1)
                    {
                        PregBarrio.Visible = false;
                        AregBarrio.Visible = false;
                        SregBarrio.Visible = false;
                        UregBarrio.Visible = false;
                    }
                    else
                    {
                        PregBarrio.Visible = true;
                        AregBarrio.Visible = true;
                        SregBarrio.Visible = true;
                        UregBarrio.Visible = true;
                    }
                }
                else
                {
                    SavelimpiarcontrolesBarrio();                    
                    saveActivarbotonesBarrio();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnBarrio_ModalPopupExtender.Show();
                }
            }
        }

        protected void BtnEditBarrios_Click(object sender, EventArgs e)
        {            
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", CmbSBpais.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryDpto = Convert.ToBoolean(dt.Rows[0]["Country_Dpto"].ToString().Trim());
                    oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }
            if (oescountry.CountryDpto == true)
            {
                duplicado = "1";
                parametro = CmbSBDept.Text;
                this.Session["parametro"] = parametro;
                if (oescountry.CountryCiudad == true)
                {
                    duplicado = "7";
                    parametro2 = CmbSBciudad.Text;
                    this.Session["parametro2"] = parametro2;
                    if (oescountry.CountryDistrito == true)
                    {
                        duplicado = "11";
                        parametro3 = CmbSBdistr.Text;
                        this.Session["parametro3"] = parametro3;
                    }
                    else
                    {
                        duplicado = "12";
                        parametro3 = CmbSBdistr.Text;
                        this.Session["parametro3"] = parametro3;
                    }
                }
                else
                {
                    duplicado = "8";
                    parametro2 = "";
                    this.Session["parametro2"] = parametro2;
                    if (oescountry.CountryDistrito == true)
                    {
                        duplicado = "9";
                        parametro3 = CmbSBdistr.Text;
                        this.Session["parametro3"] = parametro3;
                    }
                    else
                    {
                        duplicado = "10";
                        parametro3 = "";
                        this.Session["parametro3"] = parametro3;
                    }
                }
            }
            else
            {
                duplicado = "2";
                parametro = "";
                this.Session["parametro"] = parametro;
                if (oescountry.CountryCiudad == true)
                {
                    duplicado = "3";
                    parametro2 = CmbSBciudad.Text;
                    this.Session["parametro2"] = parametro2;
                    if (oescountry.CountryDistrito == true)
                    {
                        duplicado = "11";
                        parametro3 = CmbSBdistr.Text;
                        this.Session["parametro3"] = parametro3;
                    }
                    else
                    {
                        duplicado = "12";
                        parametro3 = "";
                        this.Session["parametro3"] = parametro3;
                    }
                }
                else
                {
                    duplicado = "4";
                    parametro2 = "";
                    this.Session["parametro2"] = parametro2;
                    if (oescountry.CountryDistrito == true)
                    {
                        duplicado = "5";
                        parametro3 = CmbSBdistr.Text;
                        this.Session["parametro3"] = parametro3;
                    }
                    else
                    {
                        duplicado = "6";
                        parametro3 = "";
                        this.Session["parametro3"] = parametro3;
                    }
                }
            }
            dt = null;
            oescountry = null;
            this.Session["duplicado"] = duplicado;
            BtnActualizaBarrios.Visible = true;
            BtnEditBarrios.Visible = false;
            EditarActivarControlesBarrio();
            TxtCodBarr.Enabled = false;
            this.Session["rept"] = TxtNomBarr.Text;                
        }

        protected void BtnActualizaBarrios_Click(object sender, EventArgs e)
        {      
            desactivarControlesBarrio();
            LblFaltantes.Text = "";
            TxtCodBarr.Text = TxtCodBarr.Text.TrimStart();
            TxtNomBarr.Text = TxtNomBarr.Text.TrimStart();
            if (CmbSBpais.Text == "0" || parametro == "0" || parametro2 == "0" || parametro3 == "0" || TxtNomBarr.Text == "")
            {
                if (CmbSBpais.Text == "0")
                {
                    LblFaltantes.Text = ". " + "País";
                }
                if (parametro == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Departamento";
                }
                if (parametro2 == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Ciudad";
                }
                if (parametro3 == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Distrito";
                }

                if (TxtNomBarr.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Barrio";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {

                if (RbtnEstadoBarrios.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabbarrio = new DAplicacion();
                    DataTable dt = oddeshabbarrio.PermitirDeshabilitar(ConfigurationManager.AppSettings["CommunityPointOfSale"], TxtCodBarr.Text);
                    if (dt != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Puntos de Venta, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }


                }
                repetido = Convert.ToString(this.Session["rept"]);
                if (this.Session["duplicado"] == "5" || this.Session["duplicado"] == "9" || this.Session["duplicado"] == "11")
                {
                    if (repetido != TxtNomBarr.Text)
                    {

                        DAplicacion odconsubarrio = new DAplicacion();
                        DataTable dtconsulta = odconsubarrio.ConsultaDuplicados(ConfigurationManager.AppSettings["BarrioDistrict"], CmbSBdistr.Text, TxtNomBarr.Text, null);
                        if (dtconsulta == null)
                        {
                            //if (CmbSBDept.Text == "0" || CmbSBDept.Text == "")
                            //{
                            //    sDeptoVacio = "0";
                            //}
                            //else
                            //{
                            //    sDeptoVacio = CmbSBDept.Text;
                            //}
                            //if (CmbSBciudad.Text == "0" || CmbSBciudad.Text == "")
                            //{
                            //    sCiudadVacio = "0";
                            //}
                            //else
                            //{
                            //    sCiudadVacio = CmbSBciudad.Text;
                            //}
                            //if (CmbSBdistr.Text == "0" || CmbSBdistr.Text == "")
                            //{
                            //    sDistritoVacio = "0";
                            //}
                            //else
                            //{
                            //    sDistritoVacio = CmbSBdistr.Text;
                            //}

                            EBarrio oeaBarrio = oBarrio.ActualizarBarrio(TxtCodBarr.Text, CmbSBpais.Text, CmbSBDept.Text, CmbSBciudad.Text, CmbSBdistr.Text, TxtNomBarr.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            sNomBarrio = TxtNomBarr.Text;
                            this.Session["sNomBarrio"] = sNomBarrio;
                            SavelimpiarcontrolesBarrio();
                            //llenarcombos();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "El Barrio : " + this.Session["sNomBarrio"] + " Se Actualizo con Exito";
                            MensajeAlerta();
                            saveActivarbotonesBarrio();
                        }
                        else
                        {
                            sNomBarrio = TxtNomBarr.Text;
                            this.Session["sNomBarrio"] = sNomBarrio;
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Barrio : " + this.Session["sNomBarrio"] + " No se puede Actualizar este registro ya Existe";
                            MensajeAlerta();
                        }
                    }
                    else
                    {
                        EBarrio oeaBarrio = oBarrio.ActualizarBarrio(TxtCodBarr.Text, CmbSBpais.Text, CmbSBDept.Text, CmbSBciudad.Text, CmbSBdistr.Text, TxtNomBarr.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        sNomBarrio = TxtNomBarr.Text;
                        this.Session["sNomBarrio"] = sNomBarrio;
                        SavelimpiarcontrolesBarrio();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Barrio : " + this.Session["sNomBarrio"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesBarrio();
                    }
                }
                if (this.Session["duplicado"] == "12")
                {
                    if (repetido != TxtNomBarr.Text)
                    {

                        DAplicacion odconsubarrio = new DAplicacion();
                        DataTable dtconsulta = odconsubarrio.ConsultaDuplicados(ConfigurationManager.AppSettings["BarrioCity"], CmbSBciudad.Text, TxtNomBarr.Text, null);
                        if (dtconsulta == null)
                        {
                            //if (CmbSBDept.Text == "0" || CmbSBDept.Text == "")
                            //{
                            //    sDeptoVacio = "0";
                            //}
                            //else
                            //{
                            //    sDeptoVacio = CmbSBDept.Text;
                            //}
                            //if (CmbSBciudad.Text == "0" || CmbSBciudad.Text == "")
                            //{
                            //    sCiudadVacio = "0";
                            //}
                            //else
                            //{
                            //    sCiudadVacio = CmbSBciudad.Text;
                            //}
                            //if (CmbSBdistr.Text == "0" || CmbSBdistr.Text == "")
                            //{
                            //    sDistritoVacio = "0";
                            //}
                            //else
                            //{
                            //    sDistritoVacio = CmbSBdistr.Text;
                            //}

                            EBarrio oeaBarrio = oBarrio.ActualizarBarrio(TxtCodBarr.Text, CmbSBpais.Text, CmbSBDept.Text, CmbSBciudad.Text, CmbSBdistr.Text, TxtNomBarr.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            sNomBarrio = TxtNomBarr.Text;
                            this.Session["sNomBarrio"] = sNomBarrio;
                            SavelimpiarcontrolesBarrio();
                            //llenarcombos();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "El Barrio : " + this.Session["sNomBarrio"] + " Se Actualizo con Exito";
                            MensajeAlerta();
                            saveActivarbotonesBarrio();
                        }
                        else
                        {
                            sNomBarrio = TxtNomBarr.Text;
                            this.Session["sNomBarrio"] = sNomBarrio;
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Barrio : " + this.Session["sNomBarrio"] + " No se puede Actualizar este registro ya Existe";
                            MensajeAlerta();
                        }
                    }
                    else
                    {
                        EBarrio oeaBarrio = oBarrio.ActualizarBarrio(TxtCodBarr.Text, CmbSBpais.Text, CmbSBDept.Text, CmbSBciudad.Text, CmbSBdistr.Text, TxtNomBarr.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        sNomBarrio = TxtNomBarr.Text;
                        this.Session["sNomBarrio"] = sNomBarrio;
                        SavelimpiarcontrolesBarrio();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Barrio : " + this.Session["sNomBarrio"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesBarrio();
                    }

                }
                if (this.Session["duplicado"] == "10")
                {
                    if (repetido != TxtNomBarr.Text)
                    {

                        DAplicacion odconsubarrio = new DAplicacion();
                        DataTable dtconsulta = odconsubarrio.ConsultaDuplicados(ConfigurationManager.AppSettings["BarrioDpto"], CmbSBDept.Text, TxtNomBarr.Text, null);
                        if (dtconsulta == null)
                        {
                            //if (CmbSBDept.Text == "0" || CmbSBDept.Text == "")
                            //{
                            //    sDeptoVacio = "0";
                            //}
                            //else
                            //{
                            //    sDeptoVacio = CmbSBDept.Text;
                            //}
                            //if (CmbSBciudad.Text == "0" || CmbSBciudad.Text == "")
                            //{
                            //    sCiudadVacio = "0";
                            //}
                            //else
                            //{
                            //    sCiudadVacio = CmbSBciudad.Text;
                            //}
                            //if (CmbSBdistr.Text == "0" || CmbSBdistr.Text == "")
                            //{
                            //    sDistritoVacio = "0";
                            //}
                            //else
                            //{
                            //    sDistritoVacio = CmbSBdistr.Text;
                            //}

                            EBarrio oeaBarrio = oBarrio.ActualizarBarrio(TxtCodBarr.Text, CmbSBpais.Text, CmbSBDept.Text, CmbSBciudad.Text, CmbSBdistr.Text, TxtNomBarr.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            sNomBarrio = TxtNomBarr.Text;
                            this.Session["sNomBarrio"] = sNomBarrio;
                            SavelimpiarcontrolesBarrio();
                            //llenarcombos();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "El Barrio : " + this.Session["sNomBarrio"] + " Se Actualizo con Exito";
                            MensajeAlerta();
                            saveActivarbotonesBarrio();
                        }
                        else
                        {
                            sNomBarrio = TxtNomBarr.Text;
                            this.Session["sNomBarrio"] = sNomBarrio;
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Barrio : " + this.Session["sNomBarrio"] + " No se puede Actualizar este registro ya Existe";
                            MensajeAlerta();
                        }
                    }
                    else
                    {
                        EBarrio oeaBarrio = oBarrio.ActualizarBarrio(TxtCodBarr.Text, CmbSBpais.Text, CmbSBDept.Text, CmbSBciudad.Text, CmbSBdistr.Text, TxtNomBarr.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        sNomBarrio = TxtNomBarr.Text;
                        this.Session["sNomBarrio"] = sNomBarrio;
                        SavelimpiarcontrolesBarrio();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Barrio : " + this.Session["sNomBarrio"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesBarrio();
                    }
                }
                if (this.Session["duplicado"] == "6")
                {
                    if (repetido != TxtNomBarr.Text)
                    {

                        DAplicacion odconsubarrio = new DAplicacion();
                        DataTable dtconsulta = odconsubarrio.ConsultaDuplicados(ConfigurationManager.AppSettings["BarrioCountry"], CmbSBpais.Text, TxtNomBarr.Text, null);
                        if (dtconsulta == null)
                        {
                            //if (CmbSBDept.Text == "0" || CmbSBDept.Text == "")
                            //{
                            //    sDeptoVacio = "0";
                            //}
                            //else
                            //{
                            //    sDeptoVacio = CmbSBDept.Text;
                            //}
                            //if (CmbSBciudad.Text == "0" || CmbSBciudad.Text == "")
                            //{
                            //    sCiudadVacio = "0";
                            //}
                            //else
                            //{
                            //    sCiudadVacio = CmbSBciudad.Text;
                            //}
                            //if (CmbSBdistr.Text == "0" || CmbSBdistr.Text == "")
                            //{
                            //    sDistritoVacio = "0";
                            //}
                            //else
                            //{
                            //    sDistritoVacio = CmbSBdistr.Text;
                            //}

                            EBarrio oeaBarrio = oBarrio.ActualizarBarrio(TxtCodBarr.Text, CmbSBpais.Text, CmbSBDept.Text, CmbSBciudad.Text, CmbSBdistr.Text, TxtNomBarr.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            sNomBarrio = TxtNomBarr.Text;
                            this.Session["sNomBarrio"] = sNomBarrio;
                            SavelimpiarcontrolesBarrio();
                            //llenarcombos();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "El Barrio : " + this.Session["sNomBarrio"] + " Se Actualizo con Exito";
                            MensajeAlerta();
                            saveActivarbotonesBarrio();
                        }
                        else
                        {
                            sNomBarrio = TxtNomBarr.Text;
                            this.Session["sNomBarrio"] = sNomBarrio;
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Barrio : " + this.Session["sNomBarrio"] + " No se puede Actualizar este registro ya Existe";
                            MensajeAlerta();
                        }
                    }
                    else
                    {
                        EBarrio oeaBarrio = oBarrio.ActualizarBarrio(TxtCodBarr.Text, CmbSBpais.Text, CmbSBDept.Text, CmbSBciudad.Text, CmbSBdistr.Text, TxtNomBarr.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        sNomBarrio = TxtNomBarr.Text;
                        this.Session["sNomBarrio"] = sNomBarrio;
                        SavelimpiarcontrolesBarrio();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Barrio : " + this.Session["sNomBarrio"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesBarrio();
                    }
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
        protected void BtncancelBarrios_Click(object sender, EventArgs e)
        {
            SavelimpiarcontrolesBarrio();
            saveActivarbotonesBarrio();
            desactivarControlesBarrio();
        }       
        protected void CmbSBpais_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbSBDept.Items.Clear();
            CmbSBciudad.Items.Clear();
            CmbSBdistr.Items.Clear();

            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", CmbSBpais.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryDpto = Convert.ToBoolean(dt.Rows[0]["Country_Dpto"].ToString().Trim());
                    oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }
            if (oescountry.CountryDpto == true)
            {
                // en view barrios
                DataTable dtcountry = new DataTable();
                dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSBpais.Text);
                if (dtcountry != null)
                {
                    if (dtcountry.Rows.Count > 1)
                    {
                        CmbSBDept.DataSource = dtcountry;
                        CmbSBDept.DataTextField = "Name_dpto";
                        CmbSBDept.DataValueField = "cod_dpto";
                        CmbSBDept.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Barrios para este país. Falta crear el(los) Departamentos.";
                        MensajeAlerta();
                        CmbSBDept.DataSource = dtcountry;
                        CmbSBDept.DataTextField = "Name_dpto";
                        CmbSBDept.DataValueField = "cod_dpto";
                        CmbSBDept.DataBind();
                    }
                    duplicado = "1";
                    parametro = CmbSBDept.Text;
                    this.Session["parametro"] = parametro;
                }
                dtcountry = null;
            }
            else
            {
                duplicado = "2";
                parametro = "";
                CmbSBDept.Items.Clear();
                this.Session["parametro"] = parametro;
                if (oescountry.CountryCiudad == true)
                {
                    //en view distritos
                    DataTable dtcity = new DataTable();
                    dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITYCONPAIS", CmbSBpais.Text);
                    if (dtcity.Rows.Count > 1)
                    {
                        CmbSBciudad.DataSource = dtcity;
                        CmbSBciudad.DataTextField = "Name_City";
                        CmbSBciudad.DataValueField = "cod_City";
                        CmbSBciudad.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Barrios para este país. Falta crear el(las) Ciudades.";
                        MensajeAlerta();
                        CmbSBciudad.DataSource = dtcity;
                        CmbSBciudad.DataTextField = "Name_City";
                        CmbSBciudad.DataValueField = "cod_City";
                        CmbSBciudad.DataBind();
                    }
                    duplicado = "3";
                    parametro2 = CmbSBciudad.Text;
                    this.Session["parametro2"] = parametro2;
                    dtcity = null;
                }
                else
                {
                    duplicado = "4";
                    parametro2 = "";
                    CmbSBciudad.Items.Clear();
                    this.Session["parametro2"] = parametro2;
                    if (oescountry.CountryDistrito == true)
                    {
                        //en view distritos
                        DataTable dtdistrito = new DataTable();
                        dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONPAIS", CmbSBpais.Text);
                        if (dtdistrito.Rows.Count > 1)
                        {

                            CmbSBdistr.DataSource = dtdistrito;
                            CmbSBdistr.DataTextField = "Name_Local";
                            CmbSBdistr.DataValueField = "cod_District";
                            CmbSBdistr.DataBind();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "No es posible crear Barrios para este país. Falta crear el(los) Distritos.";
                            MensajeAlerta();
                            CmbSBdistr.DataSource = dtdistrito;
                            CmbSBdistr.DataTextField = "Name_Local";
                            CmbSBdistr.DataValueField = "cod_District";
                            CmbSBdistr.DataBind();
                        }
                        duplicado = "5";
                        parametro3 = CmbSBdistr.Text;
                        this.Session["parametro3"] = parametro3;
                        dtdistrito = null;
                    }
                    else
                    {
                        duplicado = "6";
                        parametro3 = "";
                        CmbSBdistr.Items.Clear();
                        this.Session["parametro3"] = parametro3;
                    }
                }
            }
            dt = null;
            oescountry = null;
            this.Session["duplicado"] = duplicado;
        }
        protected void CmbSBDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbSBciudad.Items.Clear();
            CmbSBdistr.Items.Clear();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", CmbSBpais.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryDpto = Convert.ToBoolean(dt.Rows[0]["Country_Dpto"].ToString().Trim());
                    oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }

            if (oescountry.CountryCiudad == true)
            {
                //en view distritos
                DataTable dtcity = new DataTable();
                dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITY", CmbSBDept.Text);
                if (dtcity.Rows.Count > 1)
                {
                    CmbSBciudad.DataSource = dtcity;
                    CmbSBciudad.DataTextField = "Name_City";
                    CmbSBciudad.DataValueField = "cod_City";
                    CmbSBciudad.DataBind();
                    dtcity = null;
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No es posible crear Barrios para este país. Falta crear el(las) Ciudades.";
                    MensajeAlerta();
                    CmbSBciudad.DataSource = dtcity;
                    CmbSBciudad.DataTextField = "Name_City";
                    CmbSBciudad.DataValueField = "cod_City";
                    CmbSBciudad.DataBind();
                }
                duplicado = "7";
                parametro = CmbSBDept.Text;
                parametro2 = CmbSBciudad.Text;
                this.Session["parametro"] = parametro;
                this.Session["parametro2"] = parametro2;
                dtcity = null;
            }
            else
            {
                duplicado = "8";
                parametro = CmbSBDept.Text;
                parametro2 = "";
                CmbSBciudad.Items.Clear();
                this.Session["parametro"] = parametro;
                this.Session["parametro2"] = parametro2;
                if (oescountry.CountryDistrito == true)
                {
                    //en view distritos
                    DataTable dtdistrito = new DataTable();
                    dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONDEPTO", CmbSBDept.Text);
                    if (dtdistrito.Rows.Count > 1)
                    {
                        CmbSBdistr.DataSource = dtdistrito;
                        CmbSBdistr.DataTextField = "Name_Local";
                        CmbSBdistr.DataValueField = "cod_District";
                        CmbSBdistr.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Barrios para este país. Falta crear el(los) Distritos.";
                        MensajeAlerta();
                        CmbSBdistr.DataSource = dtdistrito;
                        CmbSBdistr.DataTextField = "Name_Local";
                        CmbSBdistr.DataValueField = "cod_District";
                        CmbSBdistr.DataBind();
                    }
                    duplicado = "9";
                    parametro3 = CmbSBdistr.Text;
                    this.Session["parametro3"] = parametro3;
                    dtdistrito = null;
                }
                else
                {
                    duplicado = "10";
                    parametro3 = "";
                    CmbSBdistr.Items.Clear();
                    this.Session["parametro3"] = parametro3;
                }
            }

            dt = null;
            oescountry = null;
            this.Session["duplicado"] = duplicado;
        }
        protected void CmbSBciudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbSBdistr.Items.Clear();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", CmbSBpais.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryDpto = Convert.ToBoolean(dt.Rows[0]["Country_Dpto"].ToString().Trim());
                    oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }

            if (oescountry.CountryCiudad == true)
            {
                //en view distritos

                if (oescountry.CountryDistrito == true)
                {
                    //en view distritos
                    DataTable dtdistrito = new DataTable();
                    dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITO", CmbSBciudad.Text);
                    if (dtdistrito.Rows.Count > 1)
                    {
                        CmbSBdistr.DataSource = dtdistrito;
                        CmbSBdistr.DataTextField = "Name_Local";
                        CmbSBdistr.DataValueField = "cod_District";
                        CmbSBdistr.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Barrios para este país. Falta crear el(los) Distritos.";
                        MensajeAlerta();
                        CmbSBdistr.DataSource = dtdistrito;
                        CmbSBdistr.DataTextField = "Name_Local";
                        CmbSBdistr.DataValueField = "cod_District";
                        CmbSBdistr.DataBind();
                    }
                    duplicado = "11";
                    parametro3 = CmbSBdistr.Text;
                    this.Session["parametro3"] = parametro3;
                    dtdistrito = null;
                }
                else
                {
                    duplicado = "12";
                    parametro3 = "";
                    CmbSBdistr.Items.Clear();
                    this.Session["parametro3"] = parametro3;
                }
            }

            dt = null;
            oescountry = null;
            this.Session["duplicado"] = duplicado;
        }
        protected void CmbSelPaisBarrio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbSelDeptoBarrio.Items.Clear();
            CmbSelCiudadBarrio.Items.Clear();
            CmbSelDistritoBarrio.Items.Clear();
            //llena combo deptos en buscar barrio
            DataSet dsdptos = new DataSet();
            dsdptos = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSelPaisBarrio.Text);

            CmbSelDeptoBarrio.DataSource = dsdptos;
            CmbSelDeptoBarrio.DataTextField = "Name_dpto";
            CmbSelDeptoBarrio.DataValueField = "cod_dpto";
            CmbSelDeptoBarrio.DataBind();
            IbtnBarrio_ModalPopupExtender.Show();


        }       
        protected void CmbSelCiudadBarrio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbSelDistritoBarrio.Items.Clear();

            //llena combo distritos  en buscar barrio
            DataSet dsdistrito = new DataSet();
            dsdistrito = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSDISTRITO", CmbSelCiudadBarrio.Text);

            CmbSelDistritoBarrio.DataSource = dsdistrito;
            CmbSelDistritoBarrio.DataTextField = "Name_Local";
            CmbSelDistritoBarrio.DataValueField = "cod_District";
            CmbSelDistritoBarrio.DataBind();

            IbtnBarrio_ModalPopupExtender.Show();
        }
        protected void CmbSelDeptoBarrio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbSelCiudadBarrio.Items.Clear();
            CmbSelDistritoBarrio.Items.Clear();

            //llena combo ciudades en buscar barrio
            DataSet dscity = new DataSet();
            dscity = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCITY", CmbSelDeptoBarrio.Text);

            CmbSelCiudadBarrio.DataSource = dscity;
            CmbSelCiudadBarrio.DataTextField = "Name_City";
            CmbSelCiudadBarrio.DataValueField = "cod_City";
            CmbSelCiudadBarrio.DataBind();

            IbtnBarrio_ModalPopupExtender.Show();
        }
        private void MostrarDatosBarrios()
        {
            recorrido = (DataTable)this.Session["tbarrio"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    CmbSBpais.SelectedValue = recorrido.Rows[recsearch]["cod_country"].ToString().Trim();

                    DataSet dscountry = new DataSet();
                    dscountry = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCOUNTRY", CmbSBpais.Text);

                    CmbSBDept.DataSource = dscountry;
                    CmbSBDept.DataTextField = "Name_dpto";
                    CmbSBDept.DataValueField = "cod_dpto";
                    CmbSBDept.DataBind();
                    dscountry = null;
                    try
                    {
                        CmbSBDept.SelectedValue = recorrido.Rows[recsearch]["cod_dpto"].ToString().Trim();
                    }
                    catch
                    {
                        CmbSBDept.Items.Clear();

                    }

                    DataSet dscity = new DataSet();
                    dscity = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSCITY", CmbSBDept.Text);

                    CmbSBciudad.DataSource = dscity;
                    CmbSBciudad.DataTextField = "Name_City";
                    CmbSBciudad.DataValueField = "cod_City";
                    CmbSBciudad.DataBind();
                    dscity = null;

                    try
                    {
                        CmbSBciudad.SelectedValue = recorrido.Rows[recsearch]["cod_City"].ToString().Trim();
                    }
                    catch
                    {
                        CmbSBciudad.Items.Clear();

                    }

                    DataSet dsDistrict = new DataSet();
                    dsDistrict = oConn.ejecutarDataSet("UP_WEBSIGE_LLENACOMBOSDISTRITO", CmbSBciudad.Text);
                    CmbSBdistr.DataSource = dsDistrict;
                    CmbSBdistr.DataTextField = "Name_Local";
                    CmbSBdistr.DataValueField = "cod_District";
                    CmbSBdistr.DataBind();
                    dsDistrict = null;

                    try
                    {
                        CmbSBdistr.SelectedValue = recorrido.Rows[recsearch]["cod_District"].ToString().Trim();
                    }
                    catch
                    {
                        CmbSBdistr.Items.Clear();
                    }

                    TxtCodBarr.Text = recorrido.Rows[recsearch]["cod_Community"].ToString().Trim();
                    TxtNomBarr.Text = recorrido.Rows[recsearch]["Name_Community"].ToString().Trim();


                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Comunity_Status"].ToString().Trim());
                    if (estado == true)
                    {
                        RbtnEstadoBarrios.Items[0].Selected = true;
                        RbtnEstadoBarrios.Items[1].Selected = false;

                    }
                    else
                    {
                        RbtnEstadoBarrios.Items[0].Selected = false;
                        RbtnEstadoBarrios.Items[1].Selected = true;
                    }


                }
            }
        }
        protected void PregBarrio_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tbarrio"];
            MostrarDatosBarrios();
        }
        protected void AregBarrio_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosBarrios();
            }
        }
        protected void SregBarrio_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tbarrio"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosBarrios();
            }
        }
        protected void UregBarrio_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tbarrio"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosBarrios();

        }

        #endregion
    }
}