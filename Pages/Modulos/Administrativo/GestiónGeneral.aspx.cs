using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using Lucky.Business.Common.Application;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;
using Telerik.Web.UI;

namespace SIGE.Pages.Modulos.Administrativo
{
    //-- =============================================
    //-- Author:		    <Ing. Magaly Jiménez>
    //-- Create date:       <28/07/2010>
    //-- Description:       <Permite al actor Administrador de SIGE realizar todos los procesos para la administracion 
    //--                    de Gestión General>
    //-- Requerimiento No.  
    //-- =============================================
    public partial class GestiónGeneral : System.Web.UI.Page
    {
        private bool estado;
        private string scompanynd = "";
        private string scompayname = "";
        private string scodcountry = "";
        private string sNomClie = "";
        private string repetido = "";
        private string repetido1 = "";
        private string sCodChannel = "";
        private string sChannelName = "";
        private string sNomCanal = "";
        private string sPOPname = "";
        private string sNomPop = "";
        private string sStrategyName = "";
        private string sNomServicio = "";
        private string sOficina = "";
        private string sCorporacion = "";
        private string sSubCanal, sCodCanal;
        private int recsearch;
        private long lcodOficina;
        private long lcodCorporacion;
        private DataTable recorrido = null;

        //private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos owsadministrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        //private Conexion oConn = new Lucky.Data.Conexion();        
        
        private Company oCompany = new Company();
        private Estrategy oEstrategy = new Estrategy();
        private Canales oCanales = new Canales();
        private AD_Subchannel oSubchannel = new AD_Subchannel();
        private MPop oMPOP = new MPop();
        private AD_Oficina oOficinas = new AD_Oficina();
    
       private AD_Corporacion oCorporacion = new AD_Corporacion();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {           
                    llenarpaisBuscarCliente();
                    LlenaPaisConsulta();
                    comboclienteenSubCanaConsultaSubcanal();
                    llenaComboTipoCanal();
                    llenacombotipomaterialpop();
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
        private void SavelimpiarControlesCliente()
        {
            cmbPaisCli.Text = "0";
            cmbTipDocCli.Text="0";
            cmbTipoClie.Text = "0";
            TxtCodCliente.Text = "";
            TxtNumdocCli.Text = "";
            TxtNomClie.Text = "";
            imglogoCliente.ImageUrl = "";
            TxtTelCli.Text = "";
            TxtDirCli.Text = "";
            TxtMailCli.Text = "";
            RBtnListStatusClie.Items[0].Selected = true;
            RBtnListStatusClie.Items[1].Selected = false;
            txtDocCli.Text = "";
            TxtCompañia.Text = "";
            cmbSPais.Text = "0";
        }
        private void activarControlesCliente()
        {
            cmbTipDocCli.Enabled = true;
            cmbTipoClie.Enabled = true;
            cmbPaisCli.Enabled = true;
            RBtnListStatusClie.Enabled = false;
            TxtCodCliente.Enabled = false;
            TxtNumdocCli.Enabled = true;
            TxtNomClie.Enabled = true;
            FileCliente.Enabled = true;
            TxtTelCli.Enabled = true;
            TxtDirCli.Enabled = true;
            TxtMailCli.Enabled = true;
            Cliente.Enabled = true;
            Panel_Canal.Enabled = false;
            Panel_Material_POP.Enabled = false;
            Panel_Servicio.Enabled = false;
            Panel_Oficinas.Enabled = false;
            Panel_SubCanal.Enabled = false;
            Panel_TipoCanal.Enabled = false;
            Panel_Corporacion.Enabled = false;
        }
        private void desactivarControlesCliente()
        {
            cmbTipDocCli.Enabled = false;
            cmbTipoClie.Enabled = false;
            cmbPaisCli.Enabled = false;
            RBtnListStatusClie.Enabled = false;
            TxtCodCliente.Enabled = false;
            TxtNumdocCli.Enabled = false;
            TxtNomClie.Enabled = false;
            FileCliente.Enabled = false;
            TxtTelCli.Enabled = false;
            TxtDirCli.Enabled = false;
            TxtMailCli.Enabled = false;
            Cliente.Enabled = true;
            Panel_Canal.Enabled = true;
            Panel_Material_POP.Enabled = true;
            Panel_Servicio.Enabled = true;
            Panel_Oficinas.Enabled = true;
            Panel_SubCanal.Enabled = true;
            Panel_TipoCanal.Enabled = true;
            Panel_Corporacion.Enabled = true;
        }
        private void crearActivarbotonesCliente()
        {
            btnCrearClie.Visible = false;
            btnsaveCli.Visible = true;
            btnConsultarclie.Visible = false;
            btnEditCli.Visible = false;
            btnActuCli.Visible = false;
            btnCancelClie.Visible = true;
            btnPreg.Visible = false;
            btnAreg.Visible = false;
            btnSreg.Visible = false;
            btnUreg.Visible = false;
        }
        private void saveActivarbotonesCliente()
        {
            btnCrearClie.Visible = true;
            btnsaveCli.Visible = false;
            btnConsultarclie.Visible = true;
            btnEditCli.Visible = false;
            btnActuCli.Visible = false;
            btnCancelClie.Visible = true;
            btnPreg.Visible = false;
            btnAreg.Visible = false;
            btnSreg.Visible = false;
            btnUreg.Visible = false;
        }
        private void EditarActivarbotonesCliente()
        {
            btnCrearClie.Visible = false;
            btnsaveCli.Visible = false;
            btnConsultarclie.Visible = true;
            btnEditCli.Visible = false;
            btnActuCli.Visible = true;
            btnCancelClie.Visible = true;
            btnPreg.Visible = false;
            btnAreg.Visible = false;
            btnSreg.Visible = false;
            btnUreg.Visible = false;
        }
        private void EditarActivarControlesCliente()
        {
            
            cmbTipDocCli.Enabled = true;
            cmbTipoClie.Enabled = true;
            cmbPaisCli.Enabled = true;
            RBtnListStatusClie.Enabled = true;
            TxtCodCliente.Enabled = false;
            TxtNumdocCli.Enabled = true;
            TxtNomClie.Enabled = true;
            TxtTelCli.Enabled = true;
            FileCliente.Enabled = true;
            TxtDirCli.Enabled = true;
            TxtMailCli.Enabled = true;
            Cliente.Enabled = true;
            Panel_Canal.Enabled = false;
            Panel_Material_POP.Enabled = false;
            Panel_Servicio.Enabled = false;
            Panel_Oficinas.Enabled = false;
            Panel_SubCanal.Enabled = false;
        }
        private void BuscarActivarbotnesCliente()
        {
            btnCrearClie.Visible = false;
            btnsaveCli.Visible = false;
            btnConsultarclie.Visible = true;
            btnEditCli.Visible = true;
            btnActuCli.Visible = false;
            btnCancelClie.Visible = true;

        }
        private void llenarComboPaísenCliente()
        {
            /*DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 2);
            cmbPaisCli.DataSource = ds;
            cmbPaisCli.DataTextField = "Name_Country";
            cmbPaisCli.DataValueField = "cod_Country";
            cmbPaisCli.DataBind();*/
            ListItem listItem0 = new ListItem("Pais00", "0");
            ListItem listItem1 = new ListItem("Pais01", "1");
            ListItem listItem2 = new ListItem("Pais02", "2");
            ListItem listItem3 = new ListItem("Pais03", "3");

            cmbPaisCli.Items.Add(listItem0);
            cmbPaisCli.Items.Add(listItem1);
            cmbPaisCli.Items.Add(listItem2);
            cmbPaisCli.Items.Add(listItem3);
			
        }
        private void LlenarDocCliente()
        {
            /*DataSet ds1 = new DataSet();
            ds1 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOTypeDocument", cmbPaisCli.Text);
            cmbTipDocCli.DataSource = ds1;
            cmbTipDocCli.DataTextField = "Type_documento";
            cmbTipDocCli.DataValueField = "id_typeDocument";
            cmbTipDocCli.DataBind();
            ds1 = null;*/

            ListItem listItem0 = new ListItem("TipoDoc00", "0");
            ListItem listItem1 = new ListItem("TipoDoc01", "1");
            ListItem listItem2 = new ListItem("TipoDoc02", "2");
            ListItem listItem3 = new ListItem("TipoDoc03", "3");

            cmbTipDocCli.Items.Add(listItem0);
            cmbTipDocCli.Items.Add(listItem1);
            cmbTipDocCli.Items.Add(listItem2);
            cmbTipDocCli.Items.Add(listItem3);

        }
        private void llenarcomTipoCliente()
        {
            /*DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 58);
            cmbTipoClie.DataSource = ds;
            cmbTipoClie.DataTextField = "Type_Name";
            cmbTipoClie.DataValueField = "id_TypeCompany";
            cmbTipoClie.DataBind();*/
            ListItem listItem0 = new ListItem("TipoCliente00", "0");
            ListItem listItem1 = new ListItem("TipoCliente01", "1");
            ListItem listItem2 = new ListItem("TipoCliente02", "2");
            ListItem listItem3 = new ListItem("TipoCliente03", "3");

            cmbTipoClie.Items.Add(listItem0);
            cmbTipoClie.Items.Add(listItem1);
            cmbTipoClie.Items.Add(listItem2);
            cmbTipoClie.Items.Add(listItem3);

        }
        private void llenarpaisBuscarCliente()
        {
            /*DataSet ds6 = new DataSet();
            ds6 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 41);
            cmbSPais.DataSource = ds6;
            cmbSPais.DataTextField = "Name_Country";
            cmbSPais.DataValueField = "cod_Country";
            cmbSPais.DataBind();*/

            ListItem listItem0 = new ListItem("Pais00", "0");
            ListItem listItem1 = new ListItem("Pais01", "1");
            ListItem listItem2 = new ListItem("Pais02", "2");
            ListItem listItem3 = new ListItem("Pais03", "3");

            cmbSPais.Items.Add(listItem0);
            cmbSPais.Items.Add(listItem1);
            cmbSPais.Items.Add(listItem2);
            cmbSPais.Items.Add(listItem3);
        }
        
        private void SavelimpiarControlesCanal()
        {           
            TxtCodCanal.Text = "";
            TxtNomCanal.Text = "";
            TxtDescCanal.Text = "";
            CmbClienteCanal.Text = "0";
            RBtnListStatusCanal.Items[0].Selected = true;
            RBtnListStatusCanal.Items[1].Selected = false;
            ddl_channel_type.SelectedIndex = 0;
            TxtBCodCanal.Text = "";
            TxtBCanal.Text = "";
        }

        private void activarControlesCanal()
        {
            RBtnListStatusCanal.Enabled = false;
            TxtCodCanal.Enabled = true;
            TxtNomCanal.Enabled = true;
            TxtDescCanal.Enabled = true;
            CmbClienteCanal.Enabled = true;
            ddl_channel_type.Enabled = true;
            Cliente.Enabled = false;
            Panel_Canal.Enabled = true;
            Panel_Material_POP.Enabled = false;
            Panel_Servicio.Enabled = false;
            Panel_Oficinas.Enabled = false;
            Panel_SubCanal.Enabled = false;
            Panel_TipoCanal.Enabled = false;
            Panel_Corporacion.Enabled = false;
        }

        private void desactivarControlesCanal()
        {
            RBtnListStatusCanal.Enabled = false;
            TxtCodCanal.Enabled = false;
            TxtNomCanal.Enabled = false;
            TxtDescCanal.Enabled = false;
            ddl_channel_type.Enabled = false;
            CmbClienteCanal.Enabled = false;
            Cliente.Enabled = true;
            Panel_Canal.Enabled = true;
            Panel_Material_POP.Enabled = true;
            Panel_Servicio.Enabled = true;
            Panel_Oficinas.Enabled = true;
            Panel_SubCanal.Enabled = true;
            Panel_TipoCanal.Enabled = true;
            Panel_Corporacion.Enabled = true;
        }

        private void crearActivarbotonesCanal()
        {
            btnCrearCanal.Visible = false;
            btnsavecanal.Visible = true;
            btnConsultarCanal.Visible = false;
            btnEditCanal.Visible = false;
            btnActualizarCanal.Visible = false;
            btnCancelCanal.Visible = true;
            btnPreg6.Visible = false;
            btnAreg6.Visible = false;
            btnSreg6.Visible = false;
            btnUreg6.Visible = false;
        }

        private void saveActivarbotonesCanal()
        {
            btnCrearCanal.Visible = true;
            btnsavecanal.Visible = false;
            btnConsultarCanal.Visible = true;
            btnEditCanal.Visible = false;
            btnActualizarCanal.Visible = false;
            btnCancelCanal.Visible = true;
            btnPreg6.Visible = false;
            btnAreg6.Visible = false;
            btnSreg6.Visible = false;
            btnUreg6.Visible = false;
        }

        private void EditarActivarbotonesCanal()
        {
            btnCrearCanal.Visible = false;
            btnsavecanal.Visible = false;
            btnConsultarCanal.Visible = true;
            btnEditCanal.Visible = false;
            btnActualizarCanal.Visible = true;
            btnCancelCanal.Visible = true;
            btnPreg6.Visible = false;
            btnAreg6.Visible = false;
            btnSreg6.Visible = false;
            btnUreg6.Visible = false;
        }

        private void EditarActivarControlesCanal()
        {
            RBtnListStatusCanal.Enabled = true;
            TxtCodCanal.Enabled = false;
            TxtNomCanal.Enabled = true;
            TxtDescCanal.Enabled = true;
            CmbClienteCanal.Enabled = true;
            Cliente.Enabled = false;
            ddl_channel_type.Enabled = true;
            Panel_Canal.Enabled = true;
            Panel_Material_POP.Enabled = false;
            Panel_Servicio.Enabled = false;
            Panel_Oficinas.Enabled = false;
            Panel_SubCanal.Enabled = false;
        }

        private void BuscarActivarbotnesCanal()
        {
            btnCrearCanal.Visible = false;
            btnsavecanal.Visible = false;
            btnConsultarCanal.Visible = true;
            btnEditCanal.Visible = true;
            btnActualizarCanal.Visible = false;
            btnCancelCanal.Visible = true;
        }

        private void llenaComboClienteCanal()
        {
            /*DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en Usuarios
            CmbClienteCanal.DataSource = ds;
            CmbClienteCanal.DataTextField = "Company_Name";
            CmbClienteCanal.DataValueField = "Company_id";
            CmbClienteCanal.DataBind();*/

            ListItem listItem0 = new ListItem("Company00", "0");
            ListItem listItem1 = new ListItem("Company01", "1");
            ListItem listItem2 = new ListItem("Company02", "2");
            ListItem listItem3 = new ListItem("Company03", "3");

            CmbClienteCanal.Items.Add(listItem0);
            CmbClienteCanal.Items.Add(listItem1);
            CmbClienteCanal.Items.Add(listItem2);
            CmbClienteCanal.Items.Add(listItem3);
        }

        private void SavelimpiarControlesMaterialPOP()
        {
            TxtCodPOP.Text = "";
            TxtNamePOP.Text = "";
            TxtDescPOP.Text = "";
            RBtnListStatusPOP.Items[0].Selected = true;
            RBtnListStatusPOP.Items[1].Selected = false;
            TxtSMPOP.Text = "";
        }

        private void activarControlesMaterialPOP()
        {
            RBtnListStatusPOP.Enabled = false;
            TxtCodPOP.Enabled = false;
            TxtNamePOP.Enabled = true;
            TxtDescPOP.Enabled = true;
            ddltipomaterial.Enabled = true;
            Cliente.Enabled = false;
            Panel_Canal.Enabled = false;
            Panel_Material_POP.Enabled = true;
            Panel_Servicio.Enabled = false;
            Panel_Oficinas.Enabled = false;
            Panel_SubCanal.Enabled = false;
            Panel_TipoCanal.Enabled = false;
            Panel_Corporacion.Enabled = false;
        }
        private void desactivarControlesMaterialPOP()
        {
            RBtnListStatusPOP.Enabled = false;
            TxtCodPOP.Enabled = false;
            TxtNamePOP.Enabled = false;
            TxtDescPOP.Enabled = false;
            Cliente.Enabled = true;
            Panel_Canal.Enabled = true;
            Panel_Material_POP.Enabled = true;
            Panel_Servicio.Enabled = true;
            Panel_Oficinas.Enabled = true;
            Panel_SubCanal.Enabled = true;
            Panel_TipoCanal.Enabled = true;
            Panel_Corporacion.Enabled = true;
            ddltipomaterial.Enabled = false;
        }
        private void crearActivarbotonesMaterialPOP()
        {
            BtnCrearPOP.Visible = false;
            BtnSavePOP.Visible = true;
            BtnConsultaPOP.Visible = false;
            btnEditMPOP.Visible = false;
            BtnActualizaPOP.Visible = false;
            BtnCancelaPOP.Visible = true;
            btnPreg5.Visible = false;
            btnAreg5.Visible = false;
            btnSreg5.Visible = false;
            btnUreg5.Visible = false;
        }
        private void saveActivarbotonesMaterialPOP()
        {
            BtnCrearPOP.Visible = true;
            BtnSavePOP.Visible = false;
            BtnConsultaPOP.Visible = true;
            btnEditMPOP.Visible = false;
            BtnActualizaPOP.Visible = false;
            BtnCancelaPOP.Visible = true;
            btnPreg5.Visible = false;
            btnAreg5.Visible = false;
            btnSreg5.Visible = false;
            btnUreg5.Visible = false;
        }
        private void EditarActivarbotonesMaterialPOP()
        {
            BtnCrearPOP.Visible = false;
            BtnSavePOP.Visible = false;
            BtnConsultaPOP.Visible = true;
            btnEditMPOP.Visible = false;
            BtnActualizaPOP.Visible = true;
            BtnCancelaPOP.Visible = true;
            btnPreg5.Visible = false;
            btnAreg5.Visible = false;
            btnSreg5.Visible = false;
            btnUreg5.Visible = false;
        }
        private void EditarActivarControlesMaterialPOP()
        {
            RBtnListStatusPOP.Enabled = true;
            TxtCodPOP.Enabled = false;
            TxtNamePOP.Enabled = true;
            TxtDescPOP.Enabled = true;
            Cliente.Enabled = false;
            Panel_Canal.Enabled = false;
            Panel_Material_POP.Enabled = true;
            Cliente.Enabled = false;
            ddltipomaterial.Enabled = true;
            Panel_Canal.Enabled = false;
            Panel_Material_POP.Enabled = true;
            Panel_Servicio.Enabled = false;
            Panel_Oficinas.Enabled = false;
            Panel_SubCanal.Enabled = false;
        }

        private void BuscarActivarbotnesMaterialPOP()
        {
            BtnCrearPOP.Visible = false;
            BtnSavePOP.Visible = false;
            BtnConsultaPOP.Visible = true;
            btnEditMPOP.Visible = true;
            BtnActualizaPOP.Visible = false;
            BtnCancelaPOP.Visible = true;

        }

        private void SavelimpiarControlesServicio()
        {
            TxtCodServ.Text = "";
            TxtNomServ.Text = "";
            TxtDescServ.Text = "";
            cmbcontryServ.Text="0";
            RBtnListStatusServ.Items[0].Selected = true;
            RBtnListStatusServ.Items[1].Selected = false;

            TxtBServicio.Text = "";
            cmbBPais.Text = "0";

        }
        private void activarControlesServicio()
        {
            cmbcontryServ.Enabled = true;
            RBtnListStatusServ.Enabled = false;
            TxtCodServ.Enabled = false;
            TxtNomServ.Enabled = true;
            TxtDescServ.Enabled = true;
            Cliente.Enabled = false;
            Panel_Canal.Enabled = false;
            Panel_Material_POP.Enabled = false;
            Panel_Servicio.Enabled = true;
            Panel_Oficinas.Enabled = false;
            Panel_SubCanal.Enabled = false;
            Panel_TipoCanal.Enabled = false;
            Panel_Corporacion.Enabled = false;
            
        }
        private void desactivarControlesServicio()
        {
            cmbcontryServ.Enabled = false;
            RBtnListStatusServ.Enabled = false;
            TxtCodServ.Enabled = false;
            TxtNomServ.Enabled = false;
            TxtDescServ.Enabled = false;
            Panel_Servicio.Enabled = true;
            Cliente.Enabled = true;
            Panel_Canal.Enabled = true;
            Panel_Material_POP.Enabled = true;
            Panel_Servicio.Enabled = true;
            Panel_Oficinas.Enabled = true;
            Panel_SubCanal.Enabled = true;
            Panel_TipoCanal.Enabled = true;
            Panel_Corporacion.Enabled = true;
        }
        private void crearActivarbotonesServicio()
        {
            
            btnCrearServ.Visible = false;
            btnsaveServ.Visible = true;
            btnConsultarServ.Visible = false;
            btnEditServicios.Visible = false;
            btnActualizarServ.Visible = false;
            btnCancelServ.Visible = true;
            btnPreg3.Visible = false;
            btnAreg3.Visible = false;
            btnSreg3.Visible = false;
            btnUreg3.Visible = false;
        }
        private void saveActivarbotonesServicio()
        {
            btnCrearServ.Visible = true;
            btnsaveServ.Visible = false;
            btnConsultarServ.Visible = true;
            btnEditServicios.Visible = false;
            btnActualizarServ.Visible = false;
            btnCancelServ.Visible = true;
            btnPreg3.Visible = false;
            btnAreg3.Visible = false;
            btnSreg3.Visible = false;
            btnUreg3.Visible = false;

        }
        private void EditarActivarbotonesServicio()
        {
            btnCrearServ.Visible = false;
            btnsaveServ.Visible = false;
            btnConsultarServ.Visible = true;
            btnEditServicios.Visible = false;
            btnActualizarServ.Visible = true;
            btnCancelServ.Visible = true;
            btnPreg3.Visible = false;
            btnAreg3.Visible = false;
            btnSreg3.Visible = false;
            btnUreg3.Visible = false;

        }
        private void EditarActivarControlesServicio()
        {

            cmbcontryServ.Enabled = true;
            RBtnListStatusServ.Enabled = true;
            TxtCodServ.Enabled = false;
            TxtNomServ.Enabled = true;
            TxtDescServ.Enabled = true;
            Panel_SubCanal.Enabled = false;
            Panel_Servicio.Enabled = true;
            Cliente.Enabled = false;
            Panel_Canal.Enabled = false;
            Panel_Material_POP.Enabled = false;
            Panel_Oficinas.Enabled = false;
        }
        private void BuscarActivarbotnesServicio()
        {
            btnCrearServ.Visible = false;
            btnsaveServ.Visible = false;
            btnConsultarServ.Visible = true;
            btnEditServicios.Visible = true;
            btnActualizarServ.Visible = false;
            btnCancelServ.Visible = true;

        }
        private void llacomboPaisServico()
        {
            /*DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 2);
             //se llena paises en servicios
            cmbcontryServ.DataSource = ds;
            cmbcontryServ.DataTextField = "Name_Country";
            cmbcontryServ.DataValueField = "cod_Country";
            cmbcontryServ.DataBind(); */

            ListItem listItem0 = new ListItem("Pais00", "0");
            ListItem listItem1 = new ListItem("Pais01", "1");
            ListItem listItem2 = new ListItem("Pais02", "2");
            ListItem listItem3 = new ListItem("Pais03", "3");

            cmbcontryServ.Items.Add(listItem0);
            cmbcontryServ.Items.Add(listItem1);
            cmbcontryServ.Items.Add(listItem2);
            cmbcontryServ.Items.Add(listItem3);
        }
        private void LlenaPaisConsulta()
        {
            /*
            DataSet ds7 = new DataSet();
            ds7 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 42);
            cmbBPais.DataSource = ds7;
            cmbBPais.DataTextField = "Name_Country";
            cmbBPais.DataValueField = "cod_Country";
            cmbBPais.DataBind();*/

            ListItem listItem0 = new ListItem("Pais00", "0");
            ListItem listItem1 = new ListItem("Pais01", "1");
            ListItem listItem2 = new ListItem("Pais02", "2");
            ListItem listItem3 = new ListItem("Pais03", "3");

            cmbBPais.Items.Add(listItem0);
            cmbBPais.Items.Add(listItem1);
            cmbBPais.Items.Add(listItem2);
            cmbBPais.Items.Add(listItem3);
        }


        private void SavelimpiarControlesOficina()

        {
            TxtCodOficina.Text = "";
            cmbClienteOficina.Text = "0";
            TxtNomOficina.Text = "";
            texAbreviatura.Text="";
            txtOrden.Text = "";
            RbtnOficinaStatus.Items[0].Selected = true;
            RbtnOficinaStatus.Items[1].Selected = false;

            TxtBCodOficina.Text = "";
            TxtBNomOficina.Text = "";

        }
        private void activarControlesOficina()
        {

            TxtCodOficina.Enabled = false;
            cmbClienteOficina.Enabled = true;
            texAbreviatura.Enabled = true;
            txtOrden.Enabled = true;
            TxtNomOficina.Enabled = true;
            RbtnOficinaStatus.Enabled = false;
            Cliente.Enabled = false;
            Panel_Canal.Enabled = false;
            Panel_Material_POP.Enabled = false;
            Panel_Servicio.Enabled = false;
            Panel_Oficinas.Enabled = true;
            Panel_SubCanal.Enabled = false;
            Panel_TipoCanal.Enabled = false;
            Panel_Corporacion.Enabled = false;
        }
        private void desactivarControlesOficina()
        {
            TxtCodOficina.Enabled = false;
            cmbClienteOficina.Enabled = false;
            TxtNomOficina.Enabled = false;
            texAbreviatura.Enabled = false;
            txtOrden.Enabled = false;
            RbtnOficinaStatus.Enabled = false;
            Cliente.Enabled = true;
            Panel_Canal.Enabled = true;
            Panel_Material_POP.Enabled = true;
            Panel_Servicio.Enabled = true;
            Panel_Oficinas.Enabled = true;
            Panel_SubCanal.Enabled = true;
            Panel_TipoCanal.Enabled = true;
            Panel_Corporacion.Enabled = true;
        }
        private void crearActivarbotonesOficina()
        {
            BtnCrearOficina.Visible = false;
            BtnSaveOficina.Visible = true;
            BtnConsultaOficina.Visible = false;
            BtnEditOficina.Visible = false;
            BtnActualizaOficina.Visible = false;
            BtnCancelOficina.Visible = true;
            PregOficina.Visible = false;
            AregOficina.Visible = false; 
            SregOficina.Visible = false;
            UregOficina.Visible = false;


        }
        private void saveActivarbotonesOficina()
        {
            BtnCrearOficina.Visible = true;
            BtnSaveOficina.Visible = false;
            BtnConsultaOficina.Visible = true;
            BtnEditOficina.Visible = false;
            BtnActualizaOficina.Visible = false;
            BtnCancelOficina.Visible = true;
            PregOficina.Visible = false;
            AregOficina.Visible = false;
            SregOficina.Visible = false;
            UregOficina.Visible = false;
         
        }
        private void EditarActivarbotonesOficina()
        {
            BtnCrearOficina.Visible = false;
            BtnSaveOficina.Visible = false;
            BtnConsultaOficina.Visible = true;
            BtnEditOficina.Visible = false;
            BtnActualizaOficina.Visible = true;
            BtnCancelOficina.Visible = true;
            PregOficina.Visible = false;
            AregOficina.Visible = false;
            SregOficina.Visible = false;
            UregOficina.Visible = false;

          
        }
        private void EditarActivarControlesOficina()
        {

            TxtCodOficina.Enabled = false;
            cmbClienteOficina.Enabled = true;
            TxtNomOficina.Enabled = true;
            texAbreviatura.Enabled = true;
            txtOrden.Enabled = true;
            RbtnOficinaStatus.Enabled = true;
            Cliente.Enabled = false;
            Panel_Canal.Enabled = false;
            Panel_Material_POP.Enabled = false;
            Panel_Servicio.Enabled = false;
            Panel_Oficinas.Enabled = true;
            Panel_SubCanal.Enabled = false;
        }
        private void BuscarActivarbotnesOficina()
        {
            BtnCrearOficina.Visible =false;
            BtnSaveOficina.Visible = false;
            BtnConsultaOficina.Visible = true;
            BtnEditOficina.Visible = true;
            BtnActualizaOficina.Visible = false;
            BtnCancelOficina.Visible = true;        

        }
        private void comboclienteenOficina()
        {
            /*DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en Usuarios
            cmbClienteOficina.DataSource = ds;
            cmbClienteOficina.DataTextField = "Company_Name";
            cmbClienteOficina.DataValueField = "Company_id";
            cmbClienteOficina.DataBind();*/

            ListItem listItem0 = new ListItem("Company00", "0");
            ListItem listItem1 = new ListItem("Company01", "1");
            ListItem listItem2 = new ListItem("Company02", "2");
            ListItem listItem3 = new ListItem("Company03", "3");

            cmbClienteOficina.Items.Add(listItem0);
            cmbClienteOficina.Items.Add(listItem1);
            cmbClienteOficina.Items.Add(listItem2);
            cmbClienteOficina.Items.Add(listItem3);
        }
        
        private void SavelimpiarControlesSubCanal()
        {
            TxtCodSubCanal.Text = "";
            TxtNomSubCanal.Text = "";
            cmbClientesubchannel.Text = "0";
            cmbcanalSub.Text = "0";
            RBtSubCanal.Items[0].Selected = true;
            RBtSubCanal.Items[1].Selected = false;

            TxtBNomSubCanal.Text = "";
            cmbBCanalSC.Items.Clear();
            CMBBClienteChannel.Text = "0";
        }

        private void activarControlesSubCanal()
        {         
            TxtCodSubCanal.Enabled = false;
            TxtNomSubCanal.Enabled = true;
            cmbClientesubchannel.Enabled = true;
            cmbcanalSub.Enabled = true;
            RBtSubCanal.Enabled = false;
            Cliente.Enabled = false;
            Panel_Canal.Enabled = false;
            Panel_Material_POP.Enabled = false;
            Panel_Servicio.Enabled = false;
            Panel_Oficinas.Enabled = false;
            Panel_SubCanal.Enabled = true;
            Panel_TipoCanal.Enabled = false;
            Panel_Corporacion.Enabled = false;
        }

        private void desactivarControlesSubCanal()
        {

            TxtCodSubCanal.Enabled = false;
            TxtNomSubCanal.Enabled = false;
            cmbClientesubchannel.Enabled = false;
            cmbcanalSub.Enabled = false;
            RBtSubCanal.Enabled = false;

            Cliente.Enabled = true;
            Panel_Canal.Enabled = true;
            Panel_Material_POP.Enabled = true;
            Panel_Servicio.Enabled = true;
            Panel_Oficinas.Enabled = true;
            Panel_SubCanal.Enabled = true;
            Panel_TipoCanal.Enabled = true;
            Panel_Corporacion.Enabled = true;         
        }

        private void crearActivarbotonesSubCanal()
        {
            BtnCrearSubCanal.Visible = false;
            BtnGuardarSubCanal.Visible = true;
            BtConsultarSubCanal.Visible = false;
            BtnEditarSubCanal.Visible = false;
            BtnActualizarSubCanal.Visible = false;
            BtnCancelarSubCanal.Visible = true;
            BtnPreSubCanal.Visible = false;
            BtnAntSubCanal.Visible = false;
            BtnSigSubCanal.Visible = false;
            BtnUltSubCanal.Visible = false;
        }

        private void saveActivarbotonesSubCanal()
        {
            BtnCrearSubCanal.Visible = true;
            BtnGuardarSubCanal.Visible = false;
            BtConsultarSubCanal.Visible = true;
            BtnEditarSubCanal.Visible = false;
            BtnActualizarSubCanal.Visible = false;
            BtnCancelarSubCanal.Visible = true;
            BtnPreSubCanal.Visible = false;
            BtnAntSubCanal.Visible = false;
            BtnSigSubCanal.Visible = false;
            BtnUltSubCanal.Visible = false;          
        }

        private void EditarActivarbotonesSubCanal()
        {

            BtnCrearSubCanal.Visible = false;
            BtnGuardarSubCanal.Visible = false;
            BtConsultarSubCanal.Visible = false;
            BtnEditarSubCanal.Visible = false;
            BtnActualizarSubCanal.Visible = true;
            BtnCancelarSubCanal.Visible = true;          
        }

        private void EditarActivarControlesSubCanal()
        {
            TxtCodSubCanal.Enabled = false;
            TxtNomSubCanal.Enabled = true;
            cmbClientesubchannel.Enabled = true;
            cmbcanalSub.Enabled = true;
            RBtSubCanal.Enabled = true;
            Cliente.Enabled = false;
            Panel_Canal.Enabled = false;
            Panel_Material_POP.Enabled = false;
            Panel_Servicio.Enabled = false;
            Panel_Oficinas.Enabled = false;
        }

        private void BuscarActivarbotnesSubCanal()
        {

            BtnCrearSubCanal.Visible = false;
            BtnGuardarSubCanal.Visible = false;
            BtConsultarSubCanal.Visible = true;
            BtnEditarSubCanal.Visible = true;
            BtnActualizarSubCanal.Visible = false;
            BtnCancelarSubCanal.Visible = true;
            BtnPreSubCanal.Visible = false;
            BtnAntSubCanal.Visible = false;
            BtnSigSubCanal.Visible = false;
            BtnUltSubCanal.Visible = false;         
        }

        private void comboclienteenSubCanal()
        {
            /*DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en Usuarios
            cmbClientesubchannel.DataSource = ds;
            cmbClientesubchannel.DataTextField = "Company_Name";
            cmbClientesubchannel.DataValueField = "Company_id";
            cmbClientesubchannel.DataBind();*/

            ListItem listItem0 = new ListItem("Company00", "0");
            ListItem listItem1 = new ListItem("Company01", "1");
            ListItem listItem2 = new ListItem("Company02", "2");
            ListItem listItem3 = new ListItem("Company03", "3");

            cmbClientesubchannel.Items.Add(listItem0);
            cmbClientesubchannel.Items.Add(listItem1);
            cmbClientesubchannel.Items.Add(listItem2);
            cmbClientesubchannel.Items.Add(listItem3);
        }

        private void comboclienteenSubCanaConsultaSubcanal()
        {
            /*DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 64);
            //se llena cliente en Usuarios
            CMBBClienteChannel.DataSource = ds;
            CMBBClienteChannel.DataTextField = "Company_Name";
            CMBBClienteChannel.DataValueField = "Company_id";
            CMBBClienteChannel.DataBind();*/
            ListItem listItem0 = new ListItem("Cliente00", "0");
            ListItem listItem1 = new ListItem("Cliente01", "1");
            ListItem listItem2 = new ListItem("Cliente02", "2");
            ListItem listItem3 = new ListItem("Cliente03", "3");

            CMBBClienteChannel.Items.Add(listItem0);
            CMBBClienteChannel.Items.Add(listItem1);
            CMBBClienteChannel.Items.Add(listItem2);
            CMBBClienteChannel.Items.Add(listItem3);
        }        

        private void comboCanalesSubCanal()
        {
            /*DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSREPORCHANNEL", Convert.ToInt32(cmbClientesubchannel.SelectedValue));
            cmbcanalSub.DataSource = ds.Tables[1];
            cmbcanalSub.DataTextField = "Channel_Name";
            cmbcanalSub.DataValueField = "cod_Channel";
            cmbcanalSub.DataBind();*/
            ListItem listItem0 = new ListItem("Canal00", "0");
            ListItem listItem1 = new ListItem("Canal01", "1");
            ListItem listItem2 = new ListItem("Canal02", "2");
            ListItem listItem3 = new ListItem("Canal03", "3");

            cmbcanalSub.Items.Add(listItem0);
            cmbcanalSub.Items.Add(listItem1);
            cmbcanalSub.Items.Add(listItem2);
            cmbcanalSub.Items.Add(listItem3);
        }       

        private void LlenacomboCanalConsulta()
        {
            /*DataSet ds1 = new DataSet();
            ds1 = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSREPORCHANNELSUBCHANEL", Convert.ToInt32(CMBBClienteChannel.SelectedValue));
            //se llena Categorias producto en BuscarSubCategorias
            cmbBCanalSC.DataSource = ds1;
            cmbBCanalSC.DataTextField = "Channel_Name";
            cmbBCanalSC.DataValueField = "cod_Channel";
            cmbBCanalSC.DataBind();
            ds1 = null;*/

            ListItem listItem0 = new ListItem("Canal00", "0");
            ListItem listItem1 = new ListItem("Canal01", "1");
            ListItem listItem2 = new ListItem("Canal02", "2");
            ListItem listItem3 = new ListItem("Canal03", "3");

            cmbBCanalSC.Items.Add(listItem0);
            cmbBCanalSC.Items.Add(listItem1);
            cmbBCanalSC.Items.Add(listItem2);
            cmbBCanalSC.Items.Add(listItem3);
        }

        //private void LlenacomboCanalConsulta()
        //{
        //    DataSet ds1 = new DataSet();
        //    ds1 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 59);
        //    //se llena Categorias producto en BuscarSubCategorias
        //    cmbBCategoriaSC.DataSource = ds1;
        //    cmbBCategoriaSC.DataTextField = "Product_Category";
        //    cmbBCategoriaSC.DataValueField = "id_ProductCategory";
        //    cmbBCategoriaSC.DataBind();
        //    ds1 = null;
        //}
        //private void LlenacomboConsultaCategoriaSubcategoria()
        //{
        //    DataSet ds = null;
        //    ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 13);
        //    //se llena mallas PDVC


        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).DataSource = ds;
        //        ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).DataTextField = "Product_Category";
        //        ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).DataValueField = "id_ProductCategory";
        //        ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).DataBind();

        //    }
        //    else
        //    {

        //    }


        //}


        private void MensajeAlerta()
        {
            ModalPopupAlertas.Show();
            BtnAceptarAlert.Focus();
            //ScriptManager.RegisterStartupScript(
            //    this, this.GetType(), "myscript", "alert('Debe ingresar todos los parametros con (*)');", true);
        }

        private void llenacombotipomaterialpop()
        {
            /*DataTable dt = null;
            dt = owsadministrativo.get_tipo_material_pop();
            ddltipomaterial.DataSource = dt;
            ddltipomaterial.DataTextField = "TipoMaDescripcion";
            ddltipomaterial.DataValueField = "idtipoMa";
            ddltipomaterial.DataBind();
            dt = null; 
            */

            ListItem listItem0 = new ListItem("TipoMaterial00", "0");
            ListItem listItem1 = new ListItem("TipoMaterial01", "1");
            ListItem listItem2 = new ListItem("TipoMaterial02", "2");
            ListItem listItem3 = new ListItem("TipoMaterial03", "3");

            ddltipomaterial.Items.Add(listItem0);
            ddltipomaterial.Items.Add(listItem1);
            ddltipomaterial.Items.Add(listItem2);
            ddltipomaterial.Items.Add(listItem3);

            ddltipomaterial.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            
        }

        private void llenaComboTipoCanal()
        {
            /*
            CanalTipo canal_tipo = new CanalTipo();
            DataTable tipocanal = new DataTable();
            tipocanal = canal_tipo.buscarTipoCanal();
            ddl_channel_type.DataSource = tipocanal;
            ddl_channel_type.DataTextField = "chtype_nombre";
            ddl_channel_type.DataValueField = "chtype_id";
            ddl_channel_type.DataBind();*/

            ListItem listItem0 = new ListItem("TipoCanal00", "0");
            ListItem listItem1 = new ListItem("TipoCanal01", "1");
            ListItem listItem2 = new ListItem("TipoCanal02", "2");
            ListItem listItem3 = new ListItem("TipoCanal03", "3");

            ddl_channel_type.Items.Add(listItem0);
            ddl_channel_type.Items.Add(listItem1);
            ddl_channel_type.Items.Add(listItem2);
            ddl_channel_type.Items.Add(listItem3);
            ddl_channel_type.Items.Insert(0, new ListItem("<Seleccione...>","0"));
        }

        private void activarcontrolestipocanal()
        {
            txt_chtype_nombre.Enabled = true;
            txt_chtype_desc.Enabled = true;
            Cliente.Enabled = false;
            Panel_Canal.Enabled = false;
            Panel_SubCanal.Enabled = false;
            Panel_TipoCanal.Enabled = true;
            Panel_Material_POP.Enabled = false;
            Panel_Servicio.Enabled = false;
            Panel_Oficinas.Enabled = false;
            btn_chtype_crear.Visible = false;
            btn_chtype_guardar.Visible = true;
            btn_chtype_consultar.Visible = false;
        }

        private void desactivarcontrolestipocanal()
        {
            txt_chtype_nombre.Enabled = false;
            txt_chtype_desc.Enabled = false;
            Cliente.Enabled = true;
            Panel_Canal.Enabled = true;
            Panel_Material_POP.Enabled = true;
            Panel_Servicio.Enabled = true;
            Panel_Oficinas.Enabled = true;
            Panel_SubCanal.Enabled = true;
            Panel_TipoCanal.Enabled = true;
            Panel_Corporacion.Enabled = true;
            btn_chtype_crear.Visible = true;
            btn_chtype_guardar.Visible = false;
            btn_chtype_consultar.Visible = true;
        }

        private void limpiarcontroles_tipocanal()
        {
            txt_chtype_nombre.Text = "";
            txt_chtype_desc.Text = "";
            txt_bchtype_descripcion.Text = "";
            txt_bchtype_nombre.Text = "";
        }

        private void cargar_rgv_tipocanal(string nombre, string desc)
        {
            DataTable dt_tipocanal = new DataTable();
            CanalTipo tipocanal = new CanalTipo();
            dt_tipocanal = tipocanal.buscarTipoCanal(nombre, desc);
            this.Session["dt_tipocanal"] = dt_tipocanal;
            rgv_tipocanal.DataSource = dt_tipocanal;
            rgv_tipocanal.DataBind();
            mp_consulta_gvtipocanal.Show();
        }

        #endregion

        /// <summary>
        /// se agrega campo Company_Foto para Guardar, Consultar y Actualizar en Maestro Cliente.
        /// 21 y 22 de Enero por Magaly Andrea Jiménez
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        #region Cliente
        protected void btnCrearClie_Click(object sender, EventArgs e)
        {
            llenarComboPaísenCliente();
            llenarcomTipoCliente();
            crearActivarbotonesCliente();
            activarControlesCliente();
        }
        protected void cmbPaisCli_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarDocCliente();
        }
        protected void btnsaveCli_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNumdocCli.Text = TxtNumdocCli.Text.TrimStart();
            TxtNomClie.Text = TxtNomClie.Text.TrimStart();
            TxtMailCli.Text = TxtMailCli.Text.TrimStart();
            if (cmbTipDocCli.Text == "0" || TxtNumdocCli.Text == "" || cmbTipoClie.Text == "0" || TxtNomClie.Text == "" || cmbPaisCli.Text == "0" || TxtMailCli.Text == "" || FileCliente.PostedFile == null || FileCliente.PostedFile.ContentLength <= 0)
            {
                if (cmbTipDocCli.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Tipo de Documento";
                }
                if (TxtNumdocCli.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Número de documento";
                }
                if (cmbTipoClie.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo Cliente";
                }
                if (TxtNomClie.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre Cliente";
                }
                if (cmbPaisCli.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "País";
                }
                if (TxtMailCli.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Email";
                }
                if (FileCliente.PostedFile == null || FileCliente.PostedFile.ContentLength <= 0)
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Fotografia";
                }
                
              
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;

            }
            try
            {
                if (Convert.ToInt64(TxtNumdocCli.Text) <= 0)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                    MensajeAlerta();
                    return;
                }
            }
            catch
            {
            }
            try
            {
                if (Convert.ToInt64(TxtNomClie.Text) == 0)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                    MensajeAlerta();
                    return;
                }
            }
            catch
            {
            }

            try
            {

                DAplicacion odconsucliente = new DAplicacion();
                DataTable dtconsulta = odconsucliente.ConsultaDuplicados(ConfigurationManager.AppSettings["clientes"], TxtNumdocCli.Text, TxtNomClie.Text, null);
                if (dtconsulta == null)
                {
                       if ((FileCliente.PostedFile != null) && (FileCliente.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileCliente.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("../../Modulos/Cliente/imgs/") + "\\" + fn;
                    try
                    {
                        if (SaveLocation != string.Empty)
                        {
                            if (FileCliente.FileName.ToLower().EndsWith(".jpg") || FileCliente.FileName.ToLower().EndsWith(".png"))
                            {
                                int fileSize = FileCliente.PostedFile.ContentLength;
                                // Allow only files less than 1.050.000 bytes (approximately 1 MB) to be uploaded.

                                //if (System.IO.File.Exists(Server.MapPath("../../Modulos/Cliente/imgs/") + "\\" + "_" + this.Session["FileName"]))
                                //{
                                //    this.Session["cssclass"] = "MensajesError";
                                //    this.Session["encabemensa"] = "Sr. Usuario";
                                //    this.Session["mensaje"] = "El archivo que intenta grabar ya existe, por favor verifique";
                                //    MensajeAlerta();

                                //}                              
                                
                                
                                if (fileSize < 1050000)
                                {
                                    string filenameCliente;
                                    filenameCliente =" ~/Pages/Modulos/Cliente/imgs/" + FileCliente.FileName;
                                     FileCliente.PostedFile.SaveAs(MapPath("../../Modulos/Cliente/imgs/" + FileCliente.FileName));
                                    ECompany oeCompany = oCompany.Register_Company(cmbTipDocCli.SelectedValue.ToString(), cmbTipoClie.SelectedValue, TxtNumdocCli.Text,
                                        TxtNomClie.Text, TxtMailCli.Text, TxtTelCli.Text, TxtDirCli.Text, filenameCliente, cmbPaisCli.SelectedValue.ToString(),
                                        true, Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now), Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));

                                    oCompany.Register_ClienteTMP(oeCompany.Companyid.ToString(), oeCompany.idtypeDocument, oeCompany.Companynd, oeCompany.CompanyName, oeCompany.CompanyAddres, oeCompany.codCountry,Convert.ToInt32(oeCompany.CompanyStatus).ToString());


                                    if (cmbTipoClie.SelectedValue == "3")
                                    {
                                        oCompany.Register_ClienteTMP_Competencia(oeCompany.Companyid.ToString(), oeCompany.CompanyName, Convert.ToInt32(oeCompany.CompanyStatus).ToString());
                                    }


                                    string sCompany = "";
                                    sCompany = TxtNomClie.Text;
                                    this.Session["sCompany"] = sCompany;
                                    Alertas.CssClass = "MensajesCorrecto";
                                    LblFaltantes.Text = "El Cliente " + this.Session["sCompany"] + " fue creado con Exito";
                                    MensajeAlerta();
                                    SavelimpiarControlesCliente();
                                    //llenarcombos();
                                    saveActivarbotonesCliente();
                                    desactivarControlesCliente();
                                }
                                  else
                                {
                                    Alertas.CssClass = "MensajesError";
                                    LblFaltantes.Text = "Sr. Usuario, verifique que la fotografía no exceda el tamaño máximo permitido. Tamaño máximo 2 MB";
                                   
                                    MensajeAlerta();
                                    return;
                                }
                                 }
                            else
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "Sr. Usuario, el formato de la imagen que desea cargar no esta permitido por la aplicación. Intentelo nuevamente. Formatos validos *.jpg *.png";
                               
                                MensajeAlerta();
                                return;
                            }
                        }
                    }
                catch (Exception ex)
                    {
                        this.Session.Abandon();
                        Alertas.CssClass = "MensajesError";
                        LblAlert.Text = "Sessión Caducada. Sr. Usuario, la sesión ha expirado. Por favor cierre la aplicación e ingrese nuevamente";
                        MensajeAlerta();
                    }
                }
                else
                {
                    string sCompany = "";
                    sCompany = TxtNomClie.Text;
                    this.Session["sCompany"] = sCompany;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Cliente " + this.Session["sCompany"] + " Ya Existe";
                    MensajeAlerta();
                }
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Sr. Usuario, debe seleccionar alguna fotografia para cargar. Formatos validos *.jpg *.png";
                    MensajeAlerta();
                    return;
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
        protected void BtnBuscarComp_Click(object sender, EventArgs e)
        {
            desactivarControlesCliente();
            LblFaltantes.Text = "";
            txtDocCli.Text = txtDocCli.Text.TrimStart();
            TxtCompañia.Text = TxtCompañia.Text.TrimStart();
            if (txtDocCli.Text == "" && TxtCompañia.Text == "" && cmbSPais.Text == "0")
            {
                this.Session["mensajealert"] = "Número de documento, Nombre de cliente y/o País";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnComp_ModalPopupExtender.Show();
                return;

            }
            if (txtDocCli.Text != "")
            {
                try
                {
                    if (Convert.ToInt64(txtDocCli.Text) == 0)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                        MensajeAlerta();
                        IbtnComp_ModalPopupExtender.Show();
                        return;
                    }
                }
                catch
                {
                }
            }

            BuscarActivarbotnesCliente();
            //btnCrearClie.Visible = true;
            scompanynd = txtDocCli.Text;
            scompayname = TxtCompañia.Text;
            scodcountry = cmbSPais.Text;
            txtDocCli.Text = "";
            TxtCompañia.Text = "";
            cmbSPais.Text = "0";
            DataTable oeCompañias = new DataTable();
            oeCompañias = null;

            oeCompañias = oCompany.BuscarCompañias(scompanynd, scompayname, scodcountry);

            if (oeCompañias != null)
            {
                if (oeCompañias.Rows.Count > 0)
                {
                    for (int i = 0; i <= oeCompañias.Rows.Count - 1; i++)
                    {
                        TxtCodCliente.Text = oeCompañias.Rows[0]["Company_id"].ToString().Trim();
                        cmbTipoClie.Text = oeCompañias.Rows[0]["Type_Company"].ToString().Trim();
                        llenarComboPaísenCliente();
                        TxtNumdocCli.Text = oeCompañias.Rows[0]["Company_nd"].ToString().Trim();
                        TxtNomClie.Text = oeCompañias.Rows[0]["Company_Name"].ToString().Trim();
                        TxtMailCli.Text = oeCompañias.Rows[0]["Company_Email"].ToString().Trim();
                        TxtTelCli.Text = oeCompañias.Rows[0]["Company_Phone"].ToString().Trim();
                        TxtDirCli.Text = oeCompañias.Rows[0]["Company_Addres"].ToString().Trim();
                        string fn = oeCompañias.Rows[0]["Company_Foto"].ToString().Trim();
                        this.Session["fn"] = fn;
                        imglogoCliente.Visible = true;
                        imglogoCliente.ImageUrl = fn;
                        cmbPaisCli.Text = oeCompañias.Rows[0]["cod_Country"].ToString().Trim();
                        LlenarDocCliente();
                        llenarcomTipoCliente();
                        cmbTipDocCli.Text = oeCompañias.Rows[0]["id_typeDocument"].ToString().Trim();
                        //TxtContac1.Text = oeCompañias.Rows[0]["contac1"].ToString().Trim();
                        //TxtMailContac1.Text = oeCompañias.Rows[0]["email_contac1"].ToString().Trim();
                        //TxtContac2.Text = oeCompañias.Rows[0]["contact2"].ToString().Trim();
                        //TxtMailContac2.Text = oeCompañias.Rows[0]["email_contac2"].ToString().Trim();
                        estado = Convert.ToBoolean(oeCompañias.Rows[0]["Company_Status"].ToString().Trim());
                        if (estado == true)
                        {
                            RBtnListStatusClie.Items[0].Selected = true;
                            RBtnListStatusClie.Items[1].Selected = false;

                        }
                        else
                        {
                            RBtnListStatusClie.Items[0].Selected = false;
                            RBtnListStatusClie.Items[1].Selected = true;
                        }
                        this.Session["tcompañias"] = oeCompañias;
                        this.Session["i"] = 0;
                        
                    }
                    if (oeCompañias.Rows.Count == 1)
                    {
                        btnPreg.Visible = false;
                        btnAreg.Visible = false;
                        btnSreg.Visible = false;
                        btnUreg.Visible = false;
                    }
                    else
                    {
                        btnPreg.Visible = true;
                        btnAreg.Visible = true;
                        btnSreg.Visible = true;
                        btnUreg.Visible = true;
                    }
                }
                else
                {
                    SavelimpiarControlesCliente();
                    //llenarcombos();
                    saveActivarbotonesCliente();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnComp_ModalPopupExtender.Show();
                }

            }
        }
        protected void btnEditCli_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesCliente();
            EditarActivarControlesCliente();
            this.Session["rept"] = TxtNomClie.Text;
        }
        protected void btnActuCli_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNumdocCli.Text = TxtNumdocCli.Text.TrimStart();
            TxtNomClie.Text = TxtNomClie.Text.TrimStart();
            TxtMailCli.Text = TxtMailCli.Text.TrimStart();
            if (cmbTipDocCli.Text == "0" || TxtNumdocCli.Text == "" || cmbTipoClie.Text == "0" || TxtNomClie.Text == "" || cmbPaisCli.Text == "0" || TxtMailCli.Text == "" )
            {
                if (cmbTipDocCli.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Tipo de Documento";
                }
                if (TxtNumdocCli.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Número de documento";
                }
                if (cmbTipoClie.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo Cliente";
                }
                if (TxtNomClie.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre Cliente";
                }
                if (cmbPaisCli.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "País";
                }
                if (TxtMailCli.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Email";
                }
             
                
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
                if (Convert.ToInt64(TxtNumdocCli.Text) <= 0)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                    MensajeAlerta();
                    return;
                }
            }
            catch
            {
            }
            try
            {
                if (Convert.ToInt64(TxtNomClie.Text) == 0)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                    MensajeAlerta();
                    return;
                }
            }
            catch
            {
            }
          
            try
            {
               
              
                if (RBtnListStatusClie.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabclie = new DAplicacion();
                    DataTable dt = oddeshabclie.PermitirDeshabilitar(ConfigurationManager.AppSettings["CompanyBudget"], TxtCodCliente.Text);
                    if (dt != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en la tabla de Planning, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt1 = oddeshabclie.PermitirDeshabilitar(ConfigurationManager.AppSettings["CompanyPerson"], TxtCodCliente.Text);
                    if (dt1 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en Maestro de usuarios, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt2 = oddeshabclie.PermitirDeshabilitar(ConfigurationManager.AppSettings["CompanyProducts"], TxtCodCliente.Text);
                    if (dt2 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Productos, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }


                }
               
                repetido = Convert.ToString(this.Session["rept"]);
                if (repetido != TxtNomClie.Text)
                {
                    DAplicacion odconsuclientes = new DAplicacion();
                    DataTable dtconsulta = odconsuclientes.ConsultaDuplicados(ConfigurationManager.AppSettings["clientes"], null, TxtNomClie.Text, null);
                    
                    if (dtconsulta == null)
                    {
                        string filenameCliente;
                        if (FileCliente.PostedFile == null || FileCliente.PostedFile.ContentLength <= 0)
                        {
                            filenameCliente = this.Session["fn"].ToString().Trim();
                        }
                        else
                        {
                            string Archivo = this.Session["fn"].ToString().Trim();
                            Archivo = Archivo.Replace("~/Pages/Modulos/Cliente/imgs/", "");
                            string ruta = Server.MapPath(@"~\Pages\Modulos\Cliente\imgs"); //Ruta
                            File.Delete(ruta + "\\" + Archivo);
                            filenameCliente = "~/Pages/Modulos/Cliente/imgs/" + FileCliente.FileName;
                            FileCliente.PostedFile.SaveAs(MapPath("~/Pages/Modulos/Cliente/imgs/" + FileCliente.FileName));
                        
                        }
                        ECompany oeaCliente = oCompany.Actualizar_Company(Convert.ToInt32(TxtCodCliente.Text), cmbTipDocCli.SelectedValue.ToString(), cmbTipoClie.SelectedValue, TxtNumdocCli.Text,
                            TxtNomClie.Text, TxtMailCli.Text, TxtTelCli.Text, TxtDirCli.Text, filenameCliente,  cmbPaisCli.SelectedValue.ToString(),
                            estado, Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));

                        sNomClie = TxtNomClie.Text;
                        SavelimpiarControlesCliente();
                        this.Session["sNomClie"] = sNomClie;
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Cliente : " + this.Session["sNomClie"] + " Se Actualizo Corecctamente";
                        MensajeAlerta();
                        imglogoCliente.Visible = false;
                        saveActivarbotonesCliente();
                        desactivarControlesCliente();

                        
                    }

                    else
                    {
                        sNomClie = TxtNomClie.Text;
                        //this.Session["sNomClie"] = sNomClie;
                        //this.Session["mensajealert"] = "El Cliente : " + this.Session["sNomClie"];
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Cliente : " + this.Session["sNomClie"] + " No se puede Actualizar este registro ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    string filenameCliente;
                    if (FileCliente.PostedFile == null || FileCliente.PostedFile.ContentLength <= 0)
                    {
                        filenameCliente = this.Session["fn"].ToString().Trim();
                    }
                    else

                    {
                        string Archivo = this.Session["fn"].ToString().Trim();
                        Archivo = Archivo.Replace("~/Pages/Modulos/Cliente/imgs/", "");
                        string ruta = Server.MapPath(@"~\Pages\Modulos\Cliente\imgs"); //Ruta
                        File.Delete(ruta + "\\" + Archivo);
                        filenameCliente = "~/Pages/Modulos/Cliente/imgs/" + FileCliente.FileName;
                        FileCliente.PostedFile.SaveAs(MapPath("~/Pages/Modulos/Cliente/imgs/" + FileCliente.FileName));

                    }
                   
                    ECompany oeaCliente = oCompany.Actualizar_Company(Convert.ToInt32(TxtCodCliente.Text), cmbTipDocCli.SelectedValue.ToString(), cmbTipoClie.SelectedValue, TxtNumdocCli.Text,
                          TxtNomClie.Text, TxtMailCli.Text, TxtTelCli.Text, TxtDirCli.Text, filenameCliente, cmbPaisCli.SelectedValue.ToString(),
                          estado, Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));

                    sNomClie = TxtNomClie.Text;
                    SavelimpiarControlesCliente();
                    this.Session["sNomClie"] = sNomClie;
                    this.Session["mensajealert"] = "El Cliente : " + this.Session["sNomClie"];
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Cliente : " + this.Session["sNomClie"] + " Se Actualizo Corecctamente";
                    MensajeAlerta();
                    imglogoCliente.Visible = false;
                    saveActivarbotonesCliente();
                    desactivarControlesCliente();
                    
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
        protected void btnCancelClie_Click(object sender, EventArgs e)
        {
            SavelimpiarControlesCliente();
            desactivarControlesCliente();
            saveActivarbotonesCliente();
            imglogoCliente.Visible = false;
        }
        private void MostrarDatosClientes()
        {
            recorrido = (DataTable)this.Session["tcompañias"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    // se adiciona cmbTipoClie.Text para que recorra el tipo de compañia en los navegadores y no muestre mal la colsulta.
                    // Magaly Jimenez   19/10/2010
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodCliente.Text = recorrido.Rows[recsearch]["Company_id"].ToString().Trim();
                    cmbTipoClie.Text = recorrido.Rows[recsearch]["Type_Company"].ToString().Trim();
                    cmbTipDocCli.Text = recorrido.Rows[recsearch]["id_typeDocument"].ToString().Trim();
                    TxtNumdocCli.Text = recorrido.Rows[recsearch]["Company_nd"].ToString().Trim();
                    TxtNomClie.Text = recorrido.Rows[recsearch]["Company_Name"].ToString().Trim();
                    TxtMailCli.Text = recorrido.Rows[recsearch]["Company_Email"].ToString().Trim();
                    TxtTelCli.Text = recorrido.Rows[recsearch]["Company_Phone"].ToString().Trim();
                    TxtDirCli.Text = recorrido.Rows[recsearch]["Company_Addres"].ToString().Trim();
                    string fn = recorrido.Rows[recsearch]["Company_Foto"].ToString().Trim();
                    this.Session["fn"] = fn;
                    imglogoCliente.ImageUrl = fn;
                    cmbPaisCli.Text = recorrido.Rows[recsearch]["cod_Country"].ToString().Trim();
                    //TxtContac1.Text = recorrido.Rows[recsearch]["contac1"].ToString().Trim();
                    //TxtMailContac1.Text = recorrido.Rows[recsearch]["email_contac1"].ToString().Trim();
                    //TxtContac2.Text = recorrido.Rows[recsearch]["contact2"].ToString().Trim();
                    //TxtMailContac2.Text = recorrido.Rows[recsearch]["email_contac2"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Company_Status"].ToString().Trim());
                    if (estado == true)
                    {
                        RBtnListStatusClie.Items[0].Selected = true;
                        RBtnListStatusClie.Items[1].Selected = false;

                    }
                    else
                    {
                        RBtnListStatusClie.Items[0].Selected = false;
                        RBtnListStatusClie.Items[1].Selected = true;
                    }

                }
            }
        }
        protected void btnPreg_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tcompañias"];
            MostrarDatosClientes();
        }
        protected void btnAreg_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosClientes();
            }
        }
        protected void btnSreg_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tcompañias"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosClientes();
            }
        }
        protected void btnUreg_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tcompañias"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosClientes();
        }
        #endregion
              
        #region Canal
        protected void btnCrearCanal_Click(object sender, EventArgs e)
        {
            llenaComboClienteCanal();
            crearActivarbotonesCanal();
            activarControlesCanal();            
        }        
        protected void btnsavecanal_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtCodCanal.Text = TxtCodCanal.Text.TrimStart();
            TxtNomCanal.Text = TxtNomCanal.Text.TrimStart();
            TxtDescCanal.Text = TxtDescCanal.Text.TrimStart();
            if (TxtCodCanal.Text == "" || CmbClienteCanal.SelectedValue == "0" || TxtNomCanal.Text == "" || TxtDescCanal.Text == "")
            {
                if (TxtCodCanal.Text == "")
                {
                    LblFaltantes.Text = ". " + "Código Canal";
                }
                if (CmbClienteCanal.Text == "")
                {
                    LblFaltantes.Text = ". " + "Cliente";
                }
                if (TxtNomCanal.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Canal";
                }
                if (TxtDescCanal.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;

            }
            if (Convert.ToInt32(TxtCodCanal.Text) <= 0)
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                MensajeAlerta();
                return;
            }
            try
            {
                
                DAplicacion odconsucanal = new DAplicacion();
                DataTable dtconsulta = odconsucanal.ConsultaDuplicados(ConfigurationManager.AppSettings["Canales"], TxtNomCanal.Text, CmbClienteCanal.Text, null);
                if (dtconsulta == null)
                {
                    ECanales oeCanales = oCanales.RegistrarCanales(TxtCodCanal.Text, Convert.ToInt32(CmbClienteCanal.Text), TxtNomCanal.Text.ToUpper(), TxtDescCanal.Text.ToUpper(), Convert.ToInt32(ddl_channel_type.Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    ECanales tmpCanales = oCanales.RegistrarCanalesTMP(oeCanales.codChannel,oeCanales.ChannelName, ddl_channel_type.SelectedItem.Text,oeCanales.ChannelStatus);
                    string sCanal = "";
                    sCanal = TxtNomCanal.Text;
                    this.Session["sCanal"] = sCanal;
                    SavelimpiarControlesCanal();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Canal " + this.Session["sCanal"] + ", fue creado con éxito";
                    MensajeAlerta();
                    saveActivarbotonesCanal();
                    desactivarControlesCanal();
                }
                else
                {
                    string sCanal = "";
                    sCanal = TxtNomCanal.Text;
                    this.Session["sCanal"] = sCanal;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Canal " + this.Session["sCanal"] + ", ya existe";
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
        protected void BtnBCanales_Click(object sender, EventArgs e)
        {
            desactivarControlesCanal();
            LblFaltantes.Text = "";
            TxtBCodCanal.Text = TxtBCodCanal.Text.TrimStart();
            TxtBCanal.Text = TxtBCanal.Text.TrimStart();
            if (TxtBCodCanal.Text == "" && TxtBCanal.Text == "")
            {
                this.Session["mensajealert"] = "Código de Canal y/o Nombre de Canal";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnCanales_ModalPopupExtender.Show();
                return;
            }

            BuscarActivarbotnesCanal();
            //btnCrearCanal.Visible = true;
            sCodChannel = TxtBCodCanal.Text;
            sChannelName = TxtBCanal.Text;
            TxtBCodCanal.Text = "";
            TxtBCanal.Text = "";
            DataTable oeCanales = oCanales.BuscarCanales(sCodChannel, sChannelName);
            if (oeCanales != null)
            {
                if (oeCanales.Rows.Count > 0)
                {
                    for (int i = 0; i <= oeCanales.Rows.Count - 1; i++)
                    {
                        TxtCodCanal.Text = oeCanales.Rows[0]["cod_Channel"].ToString().Trim();
                        llenaComboClienteCanal();
                        CmbClienteCanal.Text = oeCanales.Rows[0]["Company_id"].ToString().Trim();
                        TxtNomCanal.Text = oeCanales.Rows[0]["Channel_Name"].ToString().Trim();
                        TxtDescCanal.Text = oeCanales.Rows[0]["Channel_description"].ToString().Trim();
                        ddl_channel_type.Text = oeCanales.Rows[0]["chtype_id"].ToString().Trim();
                        estado = Convert.ToBoolean(oeCanales.Rows[0]["Channel_Status"].ToString().Trim());

                        if (estado == true)
                        {
                            RBtnListStatusCanal.Items[0].Selected = true;
                            RBtnListStatusCanal.Items[1].Selected = false;
                        }
                        else
                        {
                            RBtnListStatusCanal.Items[0].Selected = false;
                            RBtnListStatusCanal.Items[1].Selected = true;
                        }

                        this.Session["tcanales"] = oeCanales;
                        this.Session["i"] = 0;

                    }
                    if (oeCanales.Rows.Count == 1)
                    {
                        btnPreg6.Visible = false;
                        btnUreg6.Visible = false;
                        btnAreg6.Visible = false;
                        btnSreg6.Visible = false;
                    }
                    else
                    {
                        btnPreg6.Visible = true;
                        btnUreg6.Visible = true;
                        btnAreg6.Visible = true;
                        btnSreg6.Visible = true;
                    }
                }
                else
                {
                    SavelimpiarControlesCanal();
                    saveActivarbotonesCanal();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnCanales_ModalPopupExtender.Show();
                }
            }
        }
        protected void btnEditCanal_Click(object sender, EventArgs e)
        {            
            EditarActivarbotonesCanal();           
            EditarActivarControlesCanal();
            this.Session["rept"] = TxtNomCanal.Text;
            
        }
        protected void btnActualizarCanal_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomCanal.Text = TxtNomCanal.Text.TrimStart();
            TxtDescCanal.Text = TxtDescCanal.Text.TrimStart();
            if (TxtNomCanal.Text == "" || TxtDescCanal.Text == "")
            {

                if (TxtNomCanal.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Canal";
                }
                if (TxtDescCanal.Text == "")
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
                if (RBtnListStatusCanal.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabcanal = new DAplicacion();
                    DataTable dt = oddeshabcanal.PermitirDeshabilitar(ConfigurationManager.AppSettings["ChannelPlanning"], TxtCodCanal.Text);
                    if (dt != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en la tabla appLucky, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt1 = oddeshabcanal.PermitirDeshabilitar(ConfigurationManager.AppSettings["ChannelPointOfSale"], TxtCodCanal.Text);
                    if (dt1 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Puntos de Venta, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }


                }
                repetido = Convert.ToString(this.Session["rept"]);
                if (repetido != TxtNomCanal.Text)
                {

                    DAplicacion odconsucanal = new DAplicacion();
                    DataTable dtconsulta = odconsucanal.ConsultaDuplicados(ConfigurationManager.AppSettings["Canales"], TxtNomCanal.Text, null, null);
                    if (dtconsulta == null)
                    {
                        ECanales oeaCanal = oCanales.Actualizar_Canales(TxtCodCanal.Text, Convert.ToInt32(CmbClienteCanal.Text), TxtNomCanal.Text.ToUpper(), TxtDescCanal.Text.ToUpper(), Convert.ToInt32(ddl_channel_type.Text), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        ECanales oeaCanalTMP = oCanales.Actualizar_Canales_TMP(oeaCanal.codChannel, oeaCanal.ChannelName, ddl_channel_type.SelectedItem.Text, oeaCanal.ChannelStatus);
                        sNomCanal = TxtNomCanal.Text;
                        this.Session["sNomCanal"] = sNomCanal;
                        SavelimpiarControlesCanal();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Canal : " + this.Session["sNomCanal"] + ", se actualizó correctamente";
                        MensajeAlerta();
                        saveActivarbotonesCanal();
                        desactivarControlesCanal();
                    }
                    else
                    {
                        sNomCanal = TxtNomCanal.Text;
                        this.Session["sNomCanal"] = sNomCanal;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Canal : " + this.Session["sNomCanal"] + ", no se puede actualizar, el nuevo registro ya existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    ECanales oeaCanal = oCanales.Actualizar_Canales(TxtCodCanal.Text, Convert.ToInt32(CmbClienteCanal.Text), TxtNomCanal.Text.ToUpper(), TxtDescCanal.Text.ToUpper(), Convert.ToInt32(ddl_channel_type.Text), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    sNomCanal = TxtNomCanal.Text;
                    this.Session["sNomCanal"] = sNomCanal;
                    SavelimpiarControlesCanal();
                    //llenarcombos();
                    this.Session["mensajealert"] = "El Canal : " + this.Session["sNomCanal"];
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Canal : " + this.Session["sNomCanal"] + ", se actualizó correctamente";
                    MensajeAlerta();
                    saveActivarbotonesCanal();
                    desactivarControlesCanal();
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
        protected void btnCancelCanal_Click(object sender, EventArgs e)
        {
            SavelimpiarControlesCanal();
            saveActivarbotonesCanal();
            desactivarControlesCanal();
        }
        private void MostrarDatosCanal()
        {
            recorrido = (DataTable)this.Session["tcanales"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodCanal.Text = recorrido.Rows[recsearch]["cod_Channel"].ToString().Trim();
                    llenaComboClienteCanal();
                    CmbClienteCanal.Text = recorrido.Rows[recsearch]["Company_id"].ToString().Trim();
                    TxtNomCanal.Text = recorrido.Rows[recsearch]["Channel_Name"].ToString().Trim();
                    TxtDescCanal.Text = recorrido.Rows[recsearch]["Channel_description"].ToString().Trim();
                    ddl_channel_type.Text = recorrido.Rows[recsearch]["chtype_id"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Channel_Status"].ToString().Trim());

                    if (estado == true)
                    {
                        RBtnListStatusCanal.Items[0].Selected = true;
                        RBtnListStatusCanal.Items[1].Selected = false;
                    }
                    else
                    {
                        RBtnListStatusCanal.Items[0].Selected = false;
                        RBtnListStatusCanal.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void btnPreg6_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tcanales"];
            MostrarDatosCanal();

        }
        protected void btnAreg6_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosCanal();
            }
        }
        protected void btnSreg6_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tcanales"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosCanal();
            }
        }
        protected void btnUreg6_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tcanales"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosCanal();
        }
        #endregion
        
        #region Tipo Canal

        protected void btn_chtype_crear_Click(object sender, EventArgs e)
        {
            activarcontrolestipocanal();
        }

        protected void btn_chtype_guardar_Click(object sender, EventArgs e)
        {
            string nombre = txt_chtype_nombre.Text;
            string desc = txt_chtype_desc.Text;
            string usuario = Convert.ToString(this.Session["sUser"]);
            if (!nombre.Trim().Equals("") && !desc.Trim().Equals(""))
            {
                try
                {
                    DAplicacion odconsucanal = new DAplicacion();
                    DataTable duplicados = odconsucanal.ConsultaDuplicados(ConfigurationManager.AppSettings["TipoCanal"], nombre, desc, null);

                    if (duplicados == null)
                    {
                        CanalTipo tipocanal = new CanalTipo();
                        ECanalTipo oecanaltipo = new ECanalTipo();
                        oecanaltipo = tipocanal.registrartipocanal(nombre, desc, true, usuario);
                        limpiarcontroles_tipocanal();
                        desactivarcontrolestipocanal();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Tipo de canal " + nombre + ", fue creado con éxito";
                        MensajeAlerta();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Tipo de canal " + nombre + ", ya existe";
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
        }

        protected void btn_chtype_cancelar_Click(object sender, EventArgs e)
        {
            desactivarcontrolestipocanal();
        }

        protected void btn_bchtype_buscar_Click(object sender, EventArgs e)
        {
            string nombre = txt_bchtype_nombre.Text.Trim();
            string desc = txt_bchtype_descripcion.Text.Trim();

            if (!nombre.Equals("") || !desc.Equals(""))
            {
                try
                {
                    cargar_rgv_tipocanal(nombre, desc);
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
            else
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Por favor ingrese al menos un parámetro de búsqueda";
                MensajeAlerta();
                Modal_buscartipocanal.Show();
            }
        }

        protected void rgv_tipocanal_EditCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            editedItem.Edit = true;
            rgv_tipocanal.DataSource = (DataTable)(this.Session["dt_tipocanal"]);
            rgv_tipocanal.DataBind();
            mp_consulta_gvtipocanal.Show();
        }

        protected void rgv_tipocanal_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            CanalTipo ocanaltipo = new CanalTipo();
            ECanalTipo oecanaltipo = new ECanalTipo();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            int cod_canaltipo = Convert.ToInt32((editedItem["chtype_id"].Controls[0] as TextBox).Text);
            string nom_canaltipo = (editedItem["chtype_nombre"].Controls[0] as TextBox).Text;
            string desc_canaltipo = (editedItem["chtype_descripcion"].Controls[0] as TextBox).Text;
            bool status_canaltipo = (editedItem["chtype_status"].Controls[0] as CheckBox).Checked;

            string username = "";
            try
            {
                username = Convert.ToString(this.Session["sUser"]);
            }
            catch (Exception ex)
            {
            }

            oecanaltipo = ocanaltipo.actualizartipocanal(cod_canaltipo, nom_canaltipo, desc_canaltipo, status_canaltipo, username);
            //st = ouser.registrarClientesxUsuario(codusuario, codcliente, nodoxcanal_estado, username);
            cargar_rgv_tipocanal(nom_canaltipo, desc_canaltipo);
            limpiarcontroles_tipocanal();
            Alertas.CssClass = "MensajesCorrecto";
            LblFaltantes.Text = "El Tipo de canal " + nom_canaltipo + ", fue actualizado con éxito";
            MensajeAlerta();
        }

        protected void rgv_tipocanal_CancelCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            editedItem.Edit = false;
            rgv_tipocanal.DataSource = (DataTable)(this.Session["dt_tipocanal"]);
            rgv_tipocanal.DataBind();
            mp_consulta_gvtipocanal.Show();
        }
        
        #endregion

        #region SubCanal
        protected void BtnCrearSubCanal_Click(object sender, EventArgs e)
        {
            comboclienteenSubCanal();
            crearActivarbotonesSubCanal();
            activarControlesSubCanal();
        }
        protected void cmbClientesubchannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboCanalesSubCanal();
        }
        protected void CMBBClienteChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenacomboCanalConsulta();
            ModalPopupSubCanal.Show();
        }      
        protected void BtnGuardarSubCanal_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomSubCanal.Text = TxtNomSubCanal.Text.TrimStart();
            string sidCategoira = cmbcanalSub.Text;
            if (TxtNomSubCanal.Text == "" || cmbcanalSub.Text == "0")
            {
                if (TxtNomSubCanal.Text == "")
                {
                    LblFaltantes.Text = ". " + "SubCanal";
                }
                if (cmbcanalSub.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {

                DAplicacion odconsulSubCategoria = new DAplicacion();
                DataTable dtconsulta = odconsulSubCategoria.ConsultaDuplicados(ConfigurationManager.AppSettings["AD_SubCanal"], TxtNomSubCanal.Text, cmbcanalSub.Text, null);
                if (dtconsulta == null)
                {
                    EAD_Subchannel oeSubChannel = oSubchannel.RegistrarSubChanel(TxtCodSubCanal.Text, cmbcanalSub.Text, Convert.ToInt32(cmbClientesubchannel.Text), TxtNomSubCanal.Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sSubCanal = "";
                    sSubCanal = cmbcanalSub.SelectedItem.Text + " " + TxtNomSubCanal.Text;
                    this.Session["sSubCanal"] = sSubCanal;
                    SavelimpiarControlesSubCanal();
                    //LlenacomboCategoConsulta();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El subCanal " + this.Session["sSubCanal"] + " fue creado con éxito";
                    MensajeAlerta();
                    saveActivarbotonesSubCanal();
                    desactivarControlesSubCanal();
                }
                else
                {
                    string sSubCanal = "";
                    sSubCanal = cmbcanalSub.SelectedItem.Text + " " + TxtNomSubCanal.Text;
                    this.Session["sSubCanal"] = sSubCanal;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Subcanal " + this.Session["sSubCanal"] + " ya existe";
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
        protected void BtnBusSubCanal_Click(object sender, EventArgs e)
        {
            int iCodCliente;
            desactivarControlesSubCanal();
            LblFaltantes.Text = "";
            TxtBNomSubCanal.Text = TxtBNomSubCanal.Text.TrimStart();
            
            if (CMBBClienteChannel.Text == "0" || cmbBCanalSC.Text == "0") 
            {
               // this.Session["mensajealert"] = "Nombre de SubCanal y/o Canal";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Seleccione el Canal";
                MensajeAlerta();
                ModalPopupSubCanal.Show();
                return;
            }
            if (TxtBNomSubCanal.Text == "" && cmbBCanalSC.Text == "0")
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Seleccione el algun parametro de consulta";
                MensajeAlerta();
                ModalPopupSubCanal.Show();
                return;
            }
           
            BuscarActivarbotnesSubCanal();
            sSubCanal = TxtBNomSubCanal.Text;
            sCodCanal = cmbBCanalSC.Text;
            iCodCliente = Convert.ToInt32(CMBBClienteChannel.Text);
            TxtBNomSubCanal.Text = "";
            cmbBCanalSC.Text = "0";

            this.Session["sCodCanal"] = sCodCanal;
            this.Session["sSubCanal"] = sSubCanal;
            DataTable oecateSubCanal = oSubchannel.ConsultarSubChannel(iCodCliente,sCodCanal, sSubCanal);
            this.Session["tSubcanal"] = oecateSubCanal;
            if (oecateSubCanal != null)
            {
                if (oecateSubCanal.Rows.Count > 0)
                {
                    for (int i = 0; i <= oecateSubCanal.Rows.Count - 1; i++)
                    {
                        comboclienteenSubCanal();
                        cmbClientesubchannel.Text = oecateSubCanal.Rows[0]["Company_id"].ToString().Trim();
                        TxtCodSubCanal.Text = oecateSubCanal.Rows[0]["cod_subchannel"].ToString().Trim();
                        comboCanalesSubCanal();
                        cmbcanalSub.Text = oecateSubCanal.Rows[0]["cod_Channel"].ToString().Trim();
                        TxtNomSubCanal.Text = oecateSubCanal.Rows[0]["Name_subchannel"].ToString().Trim();
                        estado = Convert.ToBoolean(oecateSubCanal.Rows[0]["Status"].ToString().Trim());

                        RBtSubCanal.Items[0].Selected = estado;
                        RBtSubCanal.Items[1].Selected = !estado;
                        
                        this.Session["tSubcanal"] = oecateSubCanal;
                        this.Session["i"] = 0;

                        if (oecateSubCanal.Rows.Count == 1)
                        {
                            BtnPreSubCanal.Visible = false;
                            BtnAntSubCanal.Visible = false;
                            BtnSigSubCanal.Visible = false;
                            BtnUltSubCanal.Visible = false;
                        }
                        else
                        {
                            BtnPreSubCanal.Visible = true;
                            BtnAntSubCanal.Visible = true;
                            BtnSigSubCanal.Visible = true;
                            BtnUltSubCanal.Visible = true;
                        }
                    }
                }
                else
                {
                    SavelimpiarControlesSubCanal();
                    saveActivarbotonesSubCanal();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La consulta realizada no devolvió ninguna respuesta";
                    MensajeAlerta();
                    ModalPopupSubCanal.Show();
                }                
            }
        }
        protected void BtnEditarSubCanal_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesSubCanal();
            EditarActivarControlesSubCanal();
            this.Session["rept"] = TxtNomSubCanal.Text;
            this.Session["rept1"] = cmbcanalSub.Text;
        }
        protected void BtnActualizarSubCanal_Click1(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomSubCanal.Text = TxtNomSubCanal.Text.TrimStart();

            if (TxtNomSubCanal.Text == "" || cmbcanalSub.Text == "0")
            {
                if (TxtNomSubCanal.Text == "")
                {
                    LblFaltantes.Text = ". " + "SubCanal";
                }
                if (cmbcanalSub.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
                if (RBtSubCanal.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion odconsulSubCategoria = new DAplicacion();
                    DataTable dtconsulta = odconsulSubCategoria.PermitirDeshabilitar(ConfigurationManager.AppSettings["Product_SubCategory"], cmbcanalSub.Text);
                    if (dtconsulta != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Perfil, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                }
                repetido = Convert.ToString(this.Session["rept"]);
                repetido1 = Convert.ToString(this.Session["rept1"]);
                if (repetido != TxtNomSubCanal.Text || repetido1 != cmbcanalSub.Text)
                {
                    DAplicacion odconsulSubCategoria = new DAplicacion();
                    DataTable dtconsulta = odconsulSubCategoria.ConsultaDuplicados(ConfigurationManager.AppSettings["AD_SubCanal"], TxtNomSubCanal.Text, cmbcanalSub.Text, null);
                    if (dtconsulta == null)
                    {
                        EAD_Subchannel oeSubcanal = oSubchannel.Actualizar_SubChannel(TxtCodSubCanal.Text, cmbcanalSub.Text, Convert.ToInt32(cmbClientesubchannel.Text), TxtNomSubCanal.Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        this.Session["sSubCanal"] = sSubCanal;
                        SavelimpiarControlesSubCanal();
                        //LlenacomboCategoConsulta();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La SubCategoria de Producto : " + this.Session["sSubCategoria"] + " fue Actualizado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesSubCanal();
                        desactivarControlesSubCanal();
                    }
                    else
                    {
                        sSubCanal = cmbcanalSub.SelectedItem.Text + " " + TxtNomSubCanal.Text;
                        this.Session["sSubCanal"] = sSubCanal;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La SubCategoria de Producto : " + this.Session["sSubCategoria"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    EAD_Subchannel oeSubcanal = oSubchannel.Actualizar_SubChannel(TxtCodSubCanal.Text, cmbcanalSub.Text, Convert.ToInt32(cmbClientesubchannel.Text), TxtNomSubCanal.Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    sSubCanal = cmbcanalSub.SelectedItem.Text + " " + TxtNomSubCanal.Text;
                    this.Session["sSubCanal"] = sSubCanal;
                    SavelimpiarControlesSubCanal();
                    //LlenacomboCategoConsulta();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La SubCategoría de Producto : " + this.Session["sSubCategoria"] + " fue actualizada con éxito";
                    MensajeAlerta();
                    saveActivarbotonesSubCanal();
                    desactivarControlesSubCanal();
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
        protected void BtnCancelarSubCanal_Click1(object sender, EventArgs e)
        {
            // se crea metodo para habilitar paneles y controles al cancelar operacion
            // 23/05/2011 - Angel Ortiz
            saveActivarbotonesSubCanal();
            desactivarControlesSubCanal();
            SavelimpiarControlesSubCanal();
        }
        private void MostrarDatosSubCanal()
        {
            recorrido = (DataTable)this.Session["tSubcanal"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    comboclienteenSubCanal();
                    cmbClientesubchannel.Text = recorrido.Rows[recsearch]["Company_id"].ToString().Trim();
                    TxtCodSubCanal.Text = recorrido.Rows[recsearch]["cod_subchannel"].ToString().Trim();
                    comboCanalesSubCanal();
                    cmbcanalSub.Text = recorrido.Rows[recsearch]["cod_Channel"].ToString().Trim();
                    TxtNomSubCanal.Text = recorrido.Rows[recsearch]["Name_subchannel"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Status"].ToString().Trim());

                    RBtSubCanal.Items[0].Selected = estado;
                    RBtSubCanal.Items[1].Selected = !estado;
                }
            }
        }
        protected void BtnPreSubCanal_Click1(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tSubcanal"];
            MostrarDatosSubCanal();
        }
        protected void BtnAntSubCanal_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosSubCanal();
            }
        }
        protected void BtnSigSubCanal_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tSubcanal"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosSubCanal();
            }
        }
        protected void BtnUltSubCanal_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tSubcanal"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosSubCanal();
        }
        protected void BtConsultarSubCanal_Click(object sender, EventArgs e)
        { }
        protected void CancelaSubCanal_Click(object sender, EventArgs e)
        { }
        #endregion 

        #region MaterialPOP
        protected void BtnCrearPOP_Click(object sender, EventArgs e)
        {
            crearActivarbotonesMaterialPOP();
            activarControlesMaterialPOP();
        }
        protected void BtnSavePOP_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNamePOP.Text = TxtNamePOP.Text.TrimStart();
            TxtDescPOP.Text = TxtDescPOP.Text.TrimStart();

            if (TxtNamePOP.Text == "" || TxtDescPOP.Text == "" || ddltipomaterial.Text=="0")
            {
                if (TxtNamePOP.Text == "")
                {
                    LblFaltantes.Text = ". " + "Nombre de material POP";
                }
                if (TxtDescPOP.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                if (ddltipomaterial.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo Material";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;

            }
            try
            {
                DAplicacion odconsumpop = new DAplicacion();//cambiar dependiendo las modificaciones de carlos
                DataTable dtconsulta = odconsumpop.ConsultaDuplicados(ConfigurationManager.AppSettings["mpop"], TxtNamePOP.Text, null, null);
                if (dtconsulta == null)
                {
                    EMPop oeMPOP = oMPOP.RegistrarMPOP(ddltipomaterial.Text, TxtNamePOP.Text.ToUpper(), TxtDescPOP.Text.ToUpper(), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sMPOP = "";
                    sMPOP = TxtNamePOP.Text;
                    this.Session["sMPOP"] = sMPOP;
                    SavelimpiarControlesMaterialPOP();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Material POP " + this.Session["sMPOP"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesMaterialPOP();
                    desactivarControlesMaterialPOP();
                }
                else
                {
                    string sMPOP = "";
                    sMPOP = TxtNamePOP.Text;
                    this.Session["sMPOP"] = sMPOP;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Material POP " + this.Session["sMPOP"] + " Ya Existe";
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
        protected void btnBuscarMPOP_Click(object sender, EventArgs e)
        {
            desactivarControlesMaterialPOP();
            LblFaltantes.Text = "";
            TxtSMPOP.Text = TxtSMPOP.Text.TrimStart();

            if (TxtSMPOP.Text == "")
            {
                this.Session["mensajealert"] = "Nombre de Material POP";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parámetro de consulta";
                MensajeAlerta();
                IbtnMPOP_ModalPopupExtender.Show();
                return;
            }

            BuscarActivarbotnesMaterialPOP();
            //BtnCrearPOP.Visible = true;
            sPOPname = TxtSMPOP.Text;
            DataTable oeMPOP = oMPOP.BuscarMPOP(sPOPname);
            
            if (oeMPOP != null)
            {
                if (oeMPOP.Rows.Count > 0)
                {
                    TxtSMPOP.Text = "";
                    for (int i = 0; i <= oeMPOP.Rows.Count - 1; i++)
                    {
                        TxtCodPOP.Text = oeMPOP.Rows[0]["id_MPointOfPurchase"].ToString().Trim();
                        ddltipomaterial.Text = oeMPOP.Rows[0]["idtipoMa"].ToString().Trim();
                        TxtNamePOP.Text = oeMPOP.Rows[0]["POP_name"].ToString().Trim();
                        TxtDescPOP.Text = oeMPOP.Rows[0]["POP_description"].ToString().Trim();
                        estado = Convert.ToBoolean(oeMPOP.Rows[0]["Status"].ToString().Trim());

                        RBtnListStatusPOP.Items[0].Selected = estado;
                        RBtnListStatusPOP.Items[1].Selected = !estado;
                            
                        this.Session["tmpop"] = oeMPOP;
                        this.Session["i"] = 0;

                    }
                    if (oeMPOP.Rows.Count == 1)
                    {
                        btnPreg5.Visible = false;
                        btnUreg5.Visible = false;
                        btnAreg5.Visible = false;
                        btnSreg5.Visible = false;
                    }
                    else
                    {
                        btnPreg5.Visible = true;
                        btnUreg5.Visible = true;
                        btnAreg5.Visible = true;
                        btnSreg5.Visible = true;
                    }
                }
                else
                {
                    SavelimpiarControlesMaterialPOP();
                    saveActivarbotonesMaterialPOP();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La consulta realizada no devolvió ninguna respuesta";
                    MensajeAlerta();
                    IbtnMPOP_ModalPopupExtender.Show();
                }
            }
        }
        protected void btnEditMPOP_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesMaterialPOP();
            EditarActivarControlesMaterialPOP();
            this.Session["rept"] = TxtNamePOP.Text;
        }
        protected void BtnActualizaPOP_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNamePOP.Text = TxtNamePOP.Text.TrimStart();
            TxtDescPOP.Text = TxtDescPOP.Text.TrimStart();
            if (TxtNamePOP.Text == "" || TxtDescPOP.Text == "" || ddltipomaterial.Text=="0")
            {
                if (TxtNamePOP.Text == "")
                {
                    LblFaltantes.Text = ". " + "Nombre de material POP";
                }
                if (TxtDescPOP.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                if (ddltipomaterial.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo de Material POP";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
                if (RBtnListStatusPOP.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabMPOP = new DAplicacion();
                    DataTable dt = oddeshabMPOP.PermitirDeshabilitar(ConfigurationManager.AppSettings["MPointOfPurchaseMPointOfPurchase_Planning"], TxtCodPOP.Text);
                    if (dt != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }


                }
                repetido = Convert.ToString(this.Session["rept"]);
                int codigopop;
                try {
                    codigopop = Convert.ToInt32(TxtCodPOP.Text);
                }
                catch (Exception ex) {
                    codigopop = 0;
                }
                if (repetido != TxtNamePOP.Text)
                {

                    DAplicacion odconsumpop = new DAplicacion();//cambiar dependiendo las modificaciones de carlos
                    DataTable dtconsulta = odconsumpop.ConsultaDuplicados(ConfigurationManager.AppSettings["mpop"], TxtNamePOP.Text, null, null);
                    if (dtconsulta == null)
                    {
                        EMPop oeaMpop = oMPOP.Actualizar_MPOP(codigopop, ddltipomaterial.Text, TxtNamePOP.Text.ToUpper(), TxtDescPOP.Text.ToUpper(), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        sNomPop = TxtNamePOP.Text;
                        this.Session["sNomPop"] = sNomPop;
                        SavelimpiarControlesMaterialPOP();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Material POP : " + this.Session["sNomPop"] + " se actualizó correctamente";
                        MensajeAlerta();
                        saveActivarbotonesMaterialPOP();
                    }
                    else
                    {
                        sNomPop = TxtNamePOP.Text;
                        this.Session["sNomPop"] = sNomPop;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Material POP : " + this.Session["sNomPop"] + " no se puede actualizar, este registro ya existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    EMPop oeaMpop = oMPOP.Actualizar_MPOP(codigopop, ddltipomaterial.Text, TxtNamePOP.Text.ToUpper(), TxtDescPOP.Text.ToUpper(), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    sNomPop = TxtNamePOP.Text;
                    this.Session["sNomPop"] = sNomPop;
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Material POP : " + this.Session["sNomPop"] + " se actualizó correctamente";
                    MensajeAlerta();
                    saveActivarbotonesMaterialPOP();
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
        protected void BtnCancelaPOP_Click(object sender, EventArgs e)
        {
            SavelimpiarControlesMaterialPOP();
            saveActivarbotonesMaterialPOP();
            desactivarControlesMaterialPOP();
        }
        private void MostrarDatosMPOP()
        {
            recorrido = (DataTable)this.Session["tmpop"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodPOP.Text = recorrido.Rows[recsearch]["id_MPointOfPurchase"].ToString().Trim();
                    ddltipomaterial.Text = recorrido.Rows[recsearch]["idtipoMat"].ToString().Trim();
                    TxtNamePOP.Text = recorrido.Rows[recsearch]["POP_name"].ToString().Trim();
                    TxtDescPOP.Text = recorrido.Rows[recsearch]["POP_description"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Status"].ToString().Trim());

                    if (estado == true)
                    {
                        RBtnListStatusPOP.Items[0].Selected = true;
                        RBtnListStatusPOP.Items[1].Selected = false;

                    }
                    else
                    {
                        RBtnListStatusPOP.Items[0].Selected = false;
                        RBtnListStatusPOP.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void btnPreg5_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tmpop"];
            MostrarDatosMPOP();
        }
        protected void btnAreg5_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosMPOP();
            }
        }
        protected void btnSreg5_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tmpop"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosMPOP();
            }
        }
        protected void btnUreg5_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tmpop"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosMPOP();
        }
        #endregion

        #region Servicio
        protected void btnCrearServ_Click(object sender, EventArgs e)
        {
            llacomboPaisServico();
            crearActivarbotonesServicio();
            activarControlesServicio();
        }
        protected void btnsaveServ_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomServ.Text = TxtNomServ.Text.TrimStart();
            TxtDescServ.Text = TxtDescServ.Text.TrimStart();
            if (TxtNomServ.Text == "" || TxtDescServ.Text == "" || cmbcontryServ.Text == "0")
            {
                if (TxtNomServ.Text == "")
                {
                    LblFaltantes.Text = ". " + "Nombre de servicio";
                }
                if (TxtDescServ.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                if (cmbcontryServ.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "País";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;

            }

            try
            {
                DAplicacion odconsuservicios = new DAplicacion();
                DataTable dtconsulta = odconsuservicios.ConsultaDuplicados(ConfigurationManager.AppSettings["Servicios"], TxtNomServ.Text, cmbcontryServ.Text, null);
                if (dtconsulta == null)
                {
                    EEstrategy oeEstrategy = oEstrategy.RegistrarServicios(TxtNomServ.Text.ToUpper(), TxtDescServ.Text, cmbcontryServ.SelectedValue.ToString(), true, Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now), Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));
                    string sServicio = "";
                    sServicio = TxtNomServ.Text;
                    this.Session["sServicio"] = sServicio;
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El servicio: " + this.Session["sServicio"] + " fue creado con Exito";
                    MensajeAlerta();
                    SavelimpiarControlesServicio();
                    saveActivarbotonesServicio();
                    desactivarControlesServicio();
                }
                else
                {
                    string sServicio = "";
                    sServicio = TxtNomServ.Text;
                    this.Session["sServicio"] = sServicio;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text= "El servicio " + this.Session["sServicio"] + " Ya Existe";
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
        protected void BtnBServicio_Click(object sender, EventArgs e)
        {
            desactivarControlesServicio();
            LblFaltantes.Text = "";
            TxtBServicio.Text = TxtBServicio.Text.TrimStart();

            if (TxtBServicio.Text == "" && cmbBPais.Text == "0")
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnServicio_ModalPopupExtender.Show();
                return;
            }
            BuscarActivarbotnesServicio();
            //btnCrearServ.Visible = true;
            sStrategyName = TxtBServicio.Text;
            scodcountry = cmbBPais.SelectedValue;

            DataTable oeServicios = oEstrategy.BuscarServicios(sStrategyName, scodcountry);


            if (oeServicios != null)
            {
                if (oeServicios.Rows.Count > 0)
                {
                    TxtBServicio.Text = "";
                    cmbBPais.Text = "0";
                    for (int i = 0; i <= oeServicios.Rows.Count - 1; i++)
                    {
                        TxtCodServ.Text = oeServicios.Rows[0]["cod_Strategy"].ToString().Trim();
                        TxtNomServ.Text = oeServicios.Rows[0]["Strategy_Name"].ToString().Trim();
                        TxtDescServ.Text = oeServicios.Rows[0]["Strategy_Description"].ToString().Trim();
                        llacomboPaisServico();
                        cmbcontryServ.Text = oeServicios.Rows[0]["cod_Country"].ToString().Trim();
                        estado = Convert.ToBoolean(oeServicios.Rows[0]["Strategy_Status"].ToString().Trim());
                        if (estado == true)
                        {
                            RBtnListStatusServ.Items[0].Selected = true;
                            RBtnListStatusServ.Items[1].Selected = false;

                        }
                        else
                        {
                            RBtnListStatusServ.Items[0].Selected = false;
                            RBtnListStatusServ.Items[1].Selected = true;
                        }
                        this.Session["tservicios"] = oeServicios;
                        this.Session["i"] = 0;
                    }
                    if (oeServicios.Rows.Count == 1)
                    {
                        btnPreg3.Visible = false;
                        btnAreg3.Visible = false;
                        btnSreg3.Visible = false;
                        btnUreg3.Visible = false;
                    }
                    else
                    {
                        btnPreg3.Visible = true;
                        btnAreg3.Visible = true;
                        btnSreg3.Visible = true;
                        btnUreg3.Visible = true;
                    }
                }
                else
                {
                    SavelimpiarControlesServicio();
                    saveActivarbotonesServicio();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnServicio_ModalPopupExtender.Show();
                }
            }

        }
        protected void btnEditServicios_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesServicio();
            EditarActivarControlesServicio();
            this.Session["rept"] = TxtNomServ.Text;
        }
        protected void btnActualizarServ_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomServ.Text = TxtNomServ.Text.TrimStart();
            TxtDescServ.Text = TxtDescServ.Text.TrimStart();

            if (TxtNomServ.Text == "" || TxtDescServ.Text == "" || cmbcontryServ.Text == "0")
            {
                if (TxtNomServ.Text == "")
                {
                    LblFaltantes.Text = ". " + "Nombre de servicio";
                }
                if (TxtDescServ.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                if (cmbcontryServ.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "País";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
                if (RBtnListStatusServ.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabservicio = new DAplicacion();
                    DataTable dt = oddeshabservicio.PermitirDeshabilitar(ConfigurationManager.AppSettings["StrategiesIndicadores"], TxtCodServ.Text);
                    if (dt != null)
                    {
                       
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt1 = oddeshabservicio.PermitirDeshabilitar(ConfigurationManager.AppSettings["StrategiesItemPoints"], TxtCodServ.Text);
                    if (dt1 != null)
                    {
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt2 = oddeshabservicio.PermitirDeshabilitar(ConfigurationManager.AppSettings["StrategiesPlanning"], TxtCodServ.Text);
                    if (dt2 != null)
                    {
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt3 = oddeshabservicio.PermitirDeshabilitar(ConfigurationManager.AppSettings["StrategiesQuestionStrategy"], TxtCodServ.Text);
                    if (dt3 != null)
                    {
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt4 = oddeshabservicio.PermitirDeshabilitar(ConfigurationManager.AppSettings["StrategiesreportStrategy"], TxtCodServ.Text);
                    if (dt4 != null)
                    {
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt5 = oddeshabservicio.PermitirDeshabilitar(ConfigurationManager.AppSettings["StrategiesStPoints"], TxtCodServ.Text);
                    if (dt5 != null)
                    {
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }


                }
                repetido = Convert.ToString(this.Session["rept"]);
                if (repetido != TxtNomServ.Text)
                {

                    DAplicacion odconsuservicios = new DAplicacion();
                    DataTable dtconsulta = odconsuservicios.ConsultaDuplicados(ConfigurationManager.AppSettings["Servicios"], TxtNomServ.Text, cmbcontryServ.Text, null);
                    if (dtconsulta == null)
                    {
                        EEstrategy oeEstrategy = oEstrategy.Actualizar_Servicios(Convert.ToInt32(TxtCodServ.Text), TxtNomServ.Text.ToUpper(), TxtDescServ.Text, cmbcontryServ.SelectedValue.ToString(), estado, Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));
                        sNomServicio = TxtNomServ.Text;
                        this.Session["sNomServicio"] = sNomServicio;
                        SavelimpiarControlesServicio();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Servicio : " + this.Session["sNomServicio"] + " Se Actualizo Corecctamente";
                        MensajeAlerta();
                        saveActivarbotonesServicio();
                        desactivarControlesServicio();
                    }
                    else
                    {
                        sNomServicio = TxtNomServ.Text;
                        this.Session["sNomServicio"] = sNomServicio;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Servicio : " + this.Session["sNomServicio"] + " No se puede Actualizar este registro ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    EEstrategy oeEstrategy = oEstrategy.Actualizar_Servicios(Convert.ToInt32(TxtCodServ.Text), TxtNomServ.Text.ToUpper(), TxtDescServ.Text, cmbcontryServ.SelectedValue.ToString(), estado, Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));
                    sNomServicio = TxtNomServ.Text;
                    this.Session["sNomServicio"] = sNomServicio;
                    SavelimpiarControlesServicio();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Servicio : " + this.Session["sNomServicio"] + " Se Actualizo Corecctamente";
                    MensajeAlerta();
                    saveActivarbotonesServicio();
                    desactivarControlesServicio();
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
        protected void btnCancelServ_Click(object sender, EventArgs e)
        {
            SavelimpiarControlesServicio();
            saveActivarbotonesServicio();
            desactivarControlesServicio();

        }
        private void MostrarDatosServicio()
        {
            recorrido = (DataTable)this.Session["tservicios"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodServ.Text = recorrido.Rows[recsearch]["cod_Strategy"].ToString().Trim();
                    TxtNomServ.Text = recorrido.Rows[recsearch]["Strategy_Name"].ToString().Trim(); ;
                    TxtDescServ.Text = recorrido.Rows[recsearch]["Strategy_Description"].ToString().Trim(); ;
                    llacomboPaisServico();
                    cmbcontryServ.Text = recorrido.Rows[recsearch]["cod_Country"].ToString().Trim(); ;
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Strategy_Status"].ToString().Trim());
                    if (estado == true)
                    {
                        RBtnListStatusServ.Items[0].Selected = true;
                        RBtnListStatusServ.Items[1].Selected = false;

                    }
                    else
                    {
                        RBtnListStatusServ.Items[0].Selected = false;
                        RBtnListStatusServ.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void btnPreg3_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tservicios"];
            MostrarDatosServicio();
        }
        protected void btnAreg3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosServicio();
            }
        }
        protected void btnSreg3_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tservicios"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosServicio();
            }
        }
        protected void btnUreg3_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tservicios"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosServicio();
        }
        #endregion

        #region Eventos Oficina
        protected void BtnCrearOficina_Click(object sender, EventArgs e)
        {
            comboclienteenOficina();
            crearActivarbotonesOficina();
            activarControlesOficina();
        }
        protected void BtnSaveOficina_Click(object sender, EventArgs e)
        {

            LblFaltantes.Text = "";
            TxtNomOficina.Text = TxtNomOficina.Text.TrimStart();
            if (TxtNomOficina.Text == "" || cmbClienteOficina.Text == "")
            {

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {

                DAplicacion odconsulOficina = new DAplicacion();
                DataTable dtconsulta = odconsulOficina.ConsultaDuplicados(ConfigurationManager.AppSettings["AD_Oficina"], TxtNomOficina.Text, cmbClienteOficina.Text, null);
                if (dtconsulta == null)
                {
                    if (texAbreviatura.Text == "")
                    {
                        texAbreviatura.Text = "0";
                    }
                    else
                    {
                        texAbreviatura.Text = texAbreviatura.Text;
                    }
                    if (txtOrden.Text == "")
                    {
                        txtOrden.Text = "0";
                    }
                    else
                    {
                        txtOrden.Text = txtOrden.Text;
                    }
                    EAD_Oficina oeOFicina = oOficinas.RegistrarOficina(Convert.ToInt32(cmbClienteOficina.Text), TxtNomOficina.Text, texAbreviatura.Text, Convert.ToInt32(txtOrden.Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sOficina = "";
                    sOficina = TxtNomOficina.Text;
                    this.Session["sOficina"] = sOficina;
                    SavelimpiarControlesOficina();
                    // LlenacomboMarcaensubMarca();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Oficina" + this.Session["sOficina"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesOficina();
                    desactivarControlesOficina();

                }
                else
                {
                    string sOficina = "";
                    sOficina = TxtNomOficina.Text;
                    this.Session["sOficina"] = sOficina;
                    this.Session["mensajealert"] = "La Oficina " + this.Session["sOficina"];
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La Oficina " + this.Session["sOficina"] + " Ya Existe";
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
        protected void BtnBOficina_Click(object sender, EventArgs e)
        {
            desactivarControlesOficina();
            LblFaltantes.Text = "";
            TxtBCodOficina.Text = TxtBCodOficina.Text.TrimStart();
            TxtBNomOficina.Text = TxtBNomOficina.Text.TrimStart();


            if (TxtBCodOficina.Text == "" && TxtBNomOficina.Text == "")
            {
                this.Session["mensajealert"] = "Código y/o Nombre de Oficina";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnOficina.Show();
                return;
            }
            BuscarActivarbotnesOficina();
            try
            {
                lcodOficina = Convert.ToInt32(TxtBCodOficina.Text);
            }
            catch
            {
            }
            sOficina = TxtBNomOficina.Text;
            TxtBCodOficina.Text = "";
            TxtBNomOficina.Text = "";

            DataTable oeOficina=oOficinas.ConsultarOficinas(lcodOficina, sOficina);
            if (oeOficina != null)
            {
                if (oeOficina.Rows.Count > 0)
                {
                    for (int i = 0; i <= oeOficina.Rows.Count - 1; i++)
                    {
                        TxtCodOficina.Text = oeOficina.Rows[0]["cod_Oficina"].ToString().Trim();
                        TxtNomOficina.Text = oeOficina.Rows[0]["Name_Oficina"].ToString().Trim();
                        texAbreviatura.Text=oeOficina.Rows[0]["Abreviatura"].ToString().Trim();
                        txtOrden.Text = oeOficina.Rows[0]["Orden"].ToString().Trim();
                        comboclienteenOficina();
                        cmbClienteOficina.Text = oeOficina.Rows[0]["Company_id"].ToString().Trim();
                        estado = Convert.ToBoolean(oeOficina.Rows[0]["Oficina_Status"].ToString().Trim());

                        if (estado == true)
                        {
                            RbtnOficinaStatus.Items[0].Selected = true;
                            RbtnOficinaStatus.Items[1].Selected = false;
                        }
                        else
                        {
                            RbtnOficinaStatus.Items[0].Selected = false;
                            RbtnOficinaStatus.Items[1].Selected = true;
                        }
                        this.Session["tOficina"] = oeOficina;
                        this.Session["i"] = 0;

                    }

                    if (oeOficina.Rows.Count == 1)
                    {
                        PregOficina.Visible = false;
                        AregOficina.Visible = false;
                        SregOficina.Visible = false;
                        UregOficina.Visible = false;
                    }
                    else
                    {
                        PregOficina.Visible = true;
                        AregOficina.Visible = true;
                        SregOficina.Visible = true;
                        UregOficina.Visible = true;
                    }

                }
                else
                {
                    SavelimpiarControlesOficina();
                    saveActivarbotonesOficina();
                    comboclienteenOficina();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnOficina.Show();
                }
            }


        }
        protected void BtnEditOficina_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesOficina();
            EditarActivarControlesOficina();
            this.Session["rept"] = TxtNomOficina.Text;
            this.Session["rept1"]=cmbClienteOficina.Text;
         
        }
        protected void BtnActualizaOficina_Click(object sender, EventArgs e)
        {

            LblFaltantes.Text = "";
            TxtNomOficina.Text = TxtNomOficina.Text.TrimStart();
            if (TxtNomOficina.Text == "" || cmbClienteOficina.Text == "")
            {

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {
                if (RbtnOficinaStatus.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabBrand = new DAplicacion();
                    DataTable dt = oddeshabBrand.PermitirDeshabilitar(ConfigurationManager.AppSettings["AD_Report_Oficina"], TxtCodOficina.Text);
                    if (dt != null)
                    {

                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }

                }
                repetido = Convert.ToString(this.Session["rept"]);
                repetido1 = Convert.ToString(this.Session["rept1"]);
                if (repetido != TxtNomOficina.Text || repetido1 != cmbClienteOficina.Text)
                {
                    DAplicacion odconsulOficina = new DAplicacion();
                    DataTable dtconsulta = odconsulOficina.ConsultaDuplicados(ConfigurationManager.AppSettings["AD_Oficina"], TxtNomOficina.Text, cmbClienteOficina.Text, null);
                    if (dtconsulta == null)
                    {
                        EAD_Oficina oeOficina = oOficinas.Actualizar_Oficina(Convert.ToInt64(TxtCodOficina.Text), Convert.ToInt32(cmbClienteOficina.Text), TxtNomOficina.Text, texAbreviatura.Text, Convert.ToInt32(txtOrden.Text), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        string sOficina = "";
                        sOficina = TxtNomOficina.Text;
                        this.Session["sOficina"] = sOficina;
                        SavelimpiarControlesOficina();
                        //LlenacomboMarcaensubMarca();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Oficina : " + this.Session["sOficina"] + " Se Actualizo Corecctamente";
                        MensajeAlerta();
                        saveActivarbotonesOficina();
                        desactivarControlesOficina();
                    }
                    else
                    {
                        string sOficina = "";
                        sOficina = TxtNomOficina.Text;
                        this.Session["sOficina"] = sOficina;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La Oficina : " + this.Session["sOficina"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {

                    EAD_Oficina oeOficina = oOficinas.Actualizar_Oficina(Convert.ToInt64(TxtCodOficina.Text), Convert.ToInt32(cmbClienteOficina.Text), TxtNomOficina.Text, texAbreviatura.Text, Convert.ToInt32(txtOrden.Text), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sOficina = "";
                    sOficina = TxtNomOficina.Text;
                    this.Session["sOficina"] = sOficina;
                    SavelimpiarControlesOficina();
                    //LlenacomboMarcaensubMarca();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Oficina : " + this.Session["sOficina"] + " Se Actualizo Correctamente";
                    MensajeAlerta();
                    saveActivarbotonesOficina();
                    desactivarControlesOficina();
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
        protected void BtnCancelOficina_Click(object sender, EventArgs e)
        {
            saveActivarbotonesOficina();
            desactivarControlesOficina();
            SavelimpiarControlesOficina();
        }
        private void MostrarDatosOficina()
        {
            recorrido = (DataTable)this.Session["tOficina"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodOficina.Text = recorrido.Rows[recsearch]["cod_Oficina"].ToString().Trim();
                    TxtNomOficina.Text = recorrido.Rows[recsearch]["Name_Oficina"].ToString().Trim();
                    comboclienteenOficina();
                    cmbClienteOficina.Text = recorrido.Rows[recsearch]["Company_id"].ToString().Trim();
                    texAbreviatura.Text= recorrido.Rows[recsearch]["Abreviatura"].ToString().Trim();
                    txtOrden.Text = recorrido.Rows[recsearch]["Orden"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Oficina_Status"].ToString().Trim());
                  

                    if (estado == true)
                    {
                        RbtnOficinaStatus.Items[0].Selected = true;
                        RbtnOficinaStatus.Items[1].Selected = false;
                    }
                    else
                    {
                        RbtnOficinaStatus.Items[0].Selected = false;
                        RbtnOficinaStatus.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void PregOficina_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tOficina"];
            MostrarDatosOficina();

        }
        protected void AregOficina_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosOficina();
            }

        }
        protected void SregOficina_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tOficina"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosOficina();
            }
        }
        protected void UregOficina_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tOficina"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosOficina();
        }
        #endregion

        #region Corporacion

        private void crearActivarbotonesCorporacion()
        {
            BtnCrearCorporacion.Visible = false;
            BtnSaveCorporacion.Visible = true;
            BtnConsultaCorporacion.Visible = false;
            BtnActualizaCorporacion.Visible = false;
            BtnCancelCorporacion.Visible = true;
            PregCorporacion.Visible = false;
            AregCorporacion.Visible = false;
            SregCorporacion.Visible = false;
            UregCorporacion.Visible = false;


        }

        private void activarControlesCorporacion()
        {

            TxtCodCorporacion.Enabled = false;    
            TxtNombreCorporacion.Enabled = true;
            RbtnCorporacionStatus.Enabled = false;
            Cliente.Enabled = false;
            Panel_Canal.Enabled = false;
            Panel_Material_POP.Enabled = false;
            Panel_Servicio.Enabled = false;
            Panel_Oficinas.Enabled = false;
            Panel_SubCanal.Enabled = false;
            Panel_TipoCanal.Enabled = false;
            Panel_Corporacion.Enabled = true;
        }
        private void saveActivarbotonesCorporacion()
        {
            BtnCrearCorporacion.Visible = true;
            BtnSaveCorporacion.Visible = false;
            BtnConsultaCorporacion.Visible = true;
            BtnEditCorporacion.Visible = false;
            BtnActualizaCorporacion.Visible = false;
            BtnCancelCorporacion.Visible = true;
            PregCorporacion.Visible = false;
            AregCorporacion.Visible = false;
            SregCorporacion.Visible = false;
            UregCorporacion.Visible = false;
        }

        private void desactivarControlesCorporacion()
        {
            TxtCodCorporacion.Enabled = false;
            TxtNombreCorporacion.Enabled = false;
            RbtnCorporacionStatus.Enabled = false;

            Cliente.Enabled = true;
            Panel_Canal.Enabled = true;
            Panel_Material_POP.Enabled = true;
            Panel_Servicio.Enabled = true;
            Panel_Oficinas.Enabled = true;
            Panel_SubCanal.Enabled = true;
            Panel_TipoCanal.Enabled = true;
            Panel_Corporacion.Enabled = true;
        }

        private void SavelimpiarControlesCorporacion()
        {
            TxtCodCorporacion.Text = "";
            TxtNombreCorporacion.Text = "";
            RbtnOficinaStatus.Items[0].Selected = true;
            RbtnOficinaStatus.Items[1].Selected = false;

            TxtBNombreCorporacion.Text = "";

        }

        private void BuscarActivarbotnesCorporacion()
        {
            BtnCrearCorporacion.Visible = false;
            BtnSaveCorporacion.Visible = false;
            BtnConsultaCorporacion.Visible = true;
            BtnEditCorporacion.Visible = true;
            BtnActualizaCorporacion.Visible = false;
            BtnCancelCorporacion.Visible = true;

        }

        protected void BtnCrearCorporacion_Click(object sender, EventArgs e)
        {
            crearActivarbotonesCorporacion();
            activarControlesCorporacion();
        }

        protected void BtnCancelCorporacion_Click(object sender, EventArgs e)
        {
            saveActivarbotonesCorporacion();
            desactivarControlesCorporacion();
            SavelimpiarControlesCorporacion();
        }
        protected void BtnSaveCorporacion_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNombreCorporacion.Text = TxtNombreCorporacion.Text.TrimStart();
            if (TxtNombreCorporacion.Text == "")
            {

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {

                DAplicacion odconsulCorporacion = new DAplicacion();
                DataTable dtconsulta = odconsulCorporacion.ConsultaDuplicados(ConfigurationManager.AppSettings["AD_Corporacion"], TxtNombreCorporacion.Text, null, null);
                if (dtconsulta == null)
                {


                    EAD_Corporacion oEAD_Corporacion = oCorporacion.RegistrarCorporacion(TxtNombreCorporacion.Text,true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sCorporacion = "";
                    sCorporacion = TxtNombreCorporacion.Text;
                    this.Session["sCorporacion"] = sCorporacion;
                    SavelimpiarControlesCorporacion();
                    
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Corporación" + this.Session["sCorporacion"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesCorporacion();
                    desactivarControlesCorporacion();

                }
                else
                {
                    string sCorporacion = "";
                    sCorporacion = TxtNombreCorporacion.Text;
                    this.Session["sCorporacion"] = sCorporacion;
                    this.Session["mensajealert"] = "La Corporación " + this.Session["sCorporacion"];
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La Corporación " + this.Session["sCorporacion"] + " Ya Existe";
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
        private void desactivarControlesGVCorporacion()
        {
            RbtnCorporacionStatus.Enabled = false;
            TxtCodCorporacion.Enabled = false;
            TxtNombreCorporacion.Enabled = false;

            Panel_Canal.Enabled = true;
            Panel_Material_POP.Enabled = true;
            Panel_Servicio.Enabled = true;
            Panel_Oficinas.Enabled = true;
            Panel_SubCanal.Enabled = true;
            Panel_TipoCanal.Enabled = true;
            Panel_Corporacion.Enabled = true;
            
        }

        private void BuscarActivarbotnesGVCorporacion()
        {
            BtnCrearCorporacion.Visible = false;
            BtnSaveCorporacion.Visible = false;
            BtnConsultaCorporacion.Visible = true;
            //BtneditNodo.Visible = true;
            //BtnActualizaNodo.Visible = false;
            BtnCancelCorporacion.Visible = true;

        }
        private string sNombreCorporacion;

        private void gridCorporacion(DataTable oDataTable)
        {
            GVCorporacion1.EditIndex = -1;
            GVCorporacion1.DataSource = null;
            GVCorporacion1.DataSource = oDataTable;
            GVCorporacion1.DataBind();
            MopopConsulGvCorporacion.Show();
        }
  protected void BtnBCorporacion_Click(object sender, EventArgs e)

        {
            desactivarControlesGVCorporacion();
            LblFaltantes.Text = "";
            TxtBNombreCorporacion.Text = TxtBNombreCorporacion.Text.TrimStart();



            if (TxtBNombreCorporacion.Text == "")
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese al menos un parámetro de consulta";
                MensajeAlerta();
                IbtnCorporacion.Show();
                return;
            }

            BuscarActivarbotnesGVCorporacion();
            sNombreCorporacion = TxtBNombreCorporacion.Text;
            this.Session["scorp_name"] = sNombreCorporacion;
            TxtBNombreCorporacion.Text = "";

            DataTable odt = oCorporacion.ConsultarCorporacionxNombre(sNombreCorporacion);
            this.Session["tCorporacion"] = odt;

            if (odt != null)
            {
                if (odt.Rows.Count > 0)
                {
                    gridCorporacion(odt);
                }

                else
                {
                    saveActivarbotonesCorporacion();
                    desactivarControlesCorporacion();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La consulta realizada no devolvió ninguna respuesta";
                    MensajeAlerta();
                    IbtnCorporacion.Show();
                }
            }

            this.Session["Exportar_Excel"] = "Exportar_Corporacion";

            DataTable dtGvCorpo = new DataTable();
            dtGvCorpo.Columns.Add("Cod.Corporación", typeof(String));
            dtGvCorpo.Columns.Add("Nombre", typeof(String));
            dtGvCorpo.Columns.Add("Estado", typeof(String));

            for (int i = 0; i <= GVCorporacion1.Rows.Count - 1; i++)
            {
                DataRow dr = dtGvCorpo.NewRow();

                string a = ((Label)GVCorporacion1.Rows[i].Cells[0].FindControl("LblCodGvCorporacion")).Text;
                string b= ((Label)GVCorporacion1.Rows[i].Cells[0].FindControl("LblNomGvCorporacion")).Text;
            

                dr["Cod.Corporación"] = ((Label)GVCorporacion1.Rows[i].Cells[0].FindControl("LblCodGvCorporacion")).Text;
                dr["Nombre"] = ((Label)GVCorporacion1.Rows[i].Cells[0].FindControl("LblNomGvCorporacion")).Text;
                dr["Estado"] = ((CheckBox)GVCorporacion1.Rows[i].Cells[0].FindControl("CheckGvCorporacion")).Checked;
                dtGvCorpo.Rows.Add(dr);
            }

            this.Session["CExporCorp"] = dtGvCorpo;














            //desactivarControlesCorporacion();
            //LblFaltantes.Text = "";
            //TxtBCodCorporacion.Text = TxtBCodCorporacion.Text.TrimStart();
            //TxtBNombreCorporacion.Text = TxtBNombreCorporacion.Text.TrimStart();


            //if (TxtBCodCorporacion.Text == "" && TxtBNombreCorporacion.Text == "")
            //{
            //    this.Session["mensajealert"] = "Código y/o Nombre de Corporación";
            //    Alertas.CssClass = "MensajesError";
            //    LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
            //    MensajeAlerta();
            //    IbtnCorporacion.Show();
            //    return;
            //}
            //BuscarActivarbotnesCorporacion();
            //try
            //{
            //    lcodCorporacion = Convert.ToInt32(TxtBCodCorporacion.Text);
            //}
            //catch
            //{
            //}
            //sCorporacion = TxtBNombreCorporacion.Text;
            //TxtBCodCorporacion.Text = "";
            //TxtBNombreCorporacion.Text = "";

            //DataTable oeOficina = oCorporacion.ConsultarCorporacion(Convert.ToInt32(lcodCorporacion));
            //if (oeOficina != null)
            //{
            //    if (oeOficina.Rows.Count > 0)
            //    {
            //        for (int i = 0; i <= oeOficina.Rows.Count - 1; i++)
            //        {
            //            TxtCodOficina.Text = oeOficina.Rows[0]["cod_Oficina"].ToString().Trim();
            //            TxtNomOficina.Text = oeOficina.Rows[0]["Name_Oficina"].ToString().Trim();
            //            texAbreviatura.Text = oeOficina.Rows[0]["Abreviatura"].ToString().Trim();
            //            txtOrden.Text = oeOficina.Rows[0]["Orden"].ToString().Trim();
            //            comboclienteenOficina();
            //            cmbClienteOficina.Text = oeOficina.Rows[0]["Company_id"].ToString().Trim();
            //            estado = Convert.ToBoolean(oeOficina.Rows[0]["Oficina_Status"].ToString().Trim());

            //            if (estado == true)
            //            {
            //                RbtnOficinaStatus.Items[0].Selected = true;
            //                RbtnOficinaStatus.Items[1].Selected = false;
            //            }
            //            else
            //            {
            //                RbtnOficinaStatus.Items[0].Selected = false;
            //                RbtnOficinaStatus.Items[1].Selected = true;
            //            }
            //            this.Session["tOficina"] = oeOficina;
            //            this.Session["i"] = 0;

            //        }

            //        if (oeOficina.Rows.Count == 1)
            //        {
            //            PregOficina.Visible = false;
            //            AregOficina.Visible = false;
            //            SregOficina.Visible = false;
            //            UregOficina.Visible = false;
            //        }
            //        else
            //        {
            //            PregOficina.Visible = true;
            //            AregOficina.Visible = true;
            //            SregOficina.Visible = true;
            //            UregOficina.Visible = true;
            //        }

            //    }
            //    else
            //    {
            //        SavelimpiarControlesOficina();
            //        saveActivarbotonesOficina();
            //        comboclienteenOficina();
            //        Alertas.CssClass = "MensajesError";
            //        LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
            //        MensajeAlerta();
            //        IbtnOficina.Show();
            //    }
            //}
        }

  protected void btnCanCorporacion_Click(object sender, EventArgs e)
  {
      saveActivarbotonesCorporacion();
      desactivarControlesCorporacion();
      SavelimpiarControlesCorporacion();
  }

       

  protected void GVCorporacion1_PageIndexChanging(object sender, GridViewPageEventArgs e)
  {
      GVCorporacion1.PageIndex = e.NewPageIndex;
      GVCorporacion1.DataSource = (DataTable)this.Session["tCorporacion"];
      GVCorporacion1.DataBind();
      MopopConsulGvCorporacion.Show();
  }

  protected void GVCorporacion1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
  {
      GVCorporacion1.EditIndex = -1;
      gridCorporacion((DataTable)this.Session["tCorporacion"]);
      btnCanCorporacion.Visible = true;
      MopopConsulGvCorporacion.Show();
  }

  protected void GVCorporacion1_RowEditing(object sender, GridViewEditEventArgs e)
  {
      btnCanCorporacion.Visible = false;
      MopopConsulGvCorporacion.Show();
      GVCorporacion1.EditIndex = e.NewEditIndex;
      string Codigo, nombre;
      bool estado;

      Codigo = ((Label)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[0].FindControl("LblCodGvCorporacion")).Text;
      nombre = ((Label)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[0].FindControl("LblNomGvCorporacion")).Text;
      estado = ((CheckBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[0].FindControl("CheckGvCorporacion")).Checked;
      GVCorporacion1.DataSource = (DataTable)this.Session["tCorporacion"];
      GVCorporacion1.DataBind();

      ((Label)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[0].FindControl("LblCodGvCorporacion")).Text = Codigo;
      ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text = nombre;
      ((CheckBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[2].FindControl("CheckGvCorporacion")).Checked = estado;


      ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text = ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text.TrimStart();
      ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text = ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text.TrimEnd();



      this.Session["rept"] = ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text;
      //this.Session["rept1"] = ((DropDownList)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[2].FindControl("cmb_Node_Type")).Text;
      //this.Session["rept2"] = ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text;
      //this.Session["rept3"] = ((DropDownList)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).Text;

  }

       

  protected void GVCorporacion1_RowUpdating(object sender, GridViewUpdateEventArgs e)
  {
      LblFaltantes.Text = "";
      ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text = ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text.TrimStart();
      ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text = ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text.TrimEnd();

      this.Session["scorp_name"] = ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text;

      if (((CheckBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[2].FindControl("CheckGvCorporacion")).Checked != false)
      {
          estado = true;
      }
      else
      {
          estado = false;
          DAplicacion oddeshabnodo = new DAplicacion();
          DataTable dt = oddeshabnodo.PermitirDeshabilitar(ConfigurationManager.AppSettings["Corporacion"], ((Label)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[0].FindControl("LblCodGvCorporacion")).Text);
          if (dt != null)
          {
              Alertas.CssClass = "MensajesError";
              LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
              MensajeAlerta();
              return;
          }
      }
      if (((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text == "")
      {
          LblFaltantes.Text = "Debe ingresar los campos: ";
          if (((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text == "")
          {
              LblFaltantes.Text += ("Nombre de Corporación" + " . ");
          }
          Alertas.CssClass = "MensajesError";
          saveActivarbotonesCorporacion();
          desactivarControlesCorporacion();
          MensajeAlerta();
          return;
      }
      try
      {
          repetido = Convert.ToString(this.Session["rept"]);
          //repetido1 = Convert.ToString(this.Session["rept1"]);
          //repetido2 = Convert.ToString(this.Session["rept2"]);
          //repetido3 = Convert.ToString(this.Session["rept3"]);
          if (repetido != ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text )
          {
              DAplicacion odconsulnodo = new DAplicacion();
              DataTable dtconsulta = odconsulnodo.ConsultaDuplicados(ConfigurationManager.AppSettings["AD_Corporacion"], ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[0].FindControl("TxtNomGvCorporacion")).Text,null,null);
              if (dtconsulta == null)
              {

                  int a = Convert.ToInt32(((Label)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[0].FindControl("LblCodGvCorporacion")).Text);
                  string b = ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[0].FindControl("TxtNomGvCorporacion")).Text;
                  string c = Convert.ToString(this.Session["sUser"]);
                  DateTime d = DateTime.Now;


                  EAD_Corporacion oeaNode = oCorporacion.ActualizarCorporacion(Convert.ToInt32(((Label)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[0].FindControl("LblCodGvCorporacion")).Text), ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[0].FindControl("TxtNomGvCorporacion")).Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                 // ENodeComercial oeacadena = dnode.ActualizarNodeComercialTMP(Convert.ToInt32(((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodAgruCom")).Text), ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("TxtNomAgrupCom")).Text, estado);
                  GVCorporacion1.EditIndex = -1;
                  DataTable oDataTable = oCorporacion.ConsultarCorporacionxNombre(this.Session["scorp_name"].ToString().Trim());
                  this.Session["tCorporacion"] = oDataTable;
                  if (oDataTable != null)
                  {//  tNodeComme
                      if (oDataTable.Rows.Count > 0)
                      {
                          gridCorporacion(oDataTable);
                      }
                  }
                  //MopopConsulAgrupCom.Hide();
                  btnCanCorporacion.Visible = true;
                  MopopConsulGvCorporacion.Show();
                  Alertas.CssClass = "MensajesCorrecto";
                  LblFaltantes.Text = "La corporación : " + this.Session["sProductType"] + " fue actualizada con éxito";
                  MensajeAlerta();
                  activarControlesCorporacion();
              }
              else
              {
                  this.Session["sCorporacion"] = ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[1].FindControl("TxtNomGvCorporacion")).Text;
                  Alertas.CssClass = "MensajesError";
                  LblFaltantes.Text = "La Corporacion : " + this.Session["sCorporacion"] + " No se puede Actualizar este registro ya Existe";
                  MensajeAlerta();
              }
          }
          else
          {
              EAD_Corporacion oeaNode = oCorporacion.ActualizarCorporacion(Convert.ToInt32(((Label)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[0].FindControl("LblCodGvCorporacion")).Text), ((TextBox)GVCorporacion1.Rows[GVCorporacion1.EditIndex].Cells[0].FindControl("TxtNomGvCorporacion")).Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
              // ENodeComercial oeacadena = dnode.ActualizarNodeComercialTMP(Convert.ToInt32(((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodAgruCom")).Text), ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("TxtNomAgrupCom")).Text, estado);
              SavelimpiarControlesCorporacion();
              GVCorporacion1.EditIndex = -1;
              DataTable oDataTable = oCorporacion.ConsultarCorporacionxNombre(this.Session["scorp_name"].ToString().Trim());
              this.Session["tCorporacion"] = oDataTable;
              if (oDataTable != null)
              {
                  if (oDataTable.Rows.Count > 0)
                  {
                      gridCorporacion(oDataTable);
                  }
              }
              btnCanCorporacion.Visible = true;
              MopopConsulGvCorporacion.Show();
              Alertas.CssClass = "MensajesCorrecto";
              LblFaltantes.Text = "La categoría de Producto : " + this.Session["sProductType"] + " fue actualizada con éxito";
              MensajeAlerta();
              activarControlesCorporacion();
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
        #endregion

        #region Asignacion Competidores

  private void activarControlesAsignacionCompetidores()
  {

      Panel_Canal.Enabled = false;
      Panel_Material_POP.Enabled = false;
      Panel_Servicio.Enabled = false;
      Panel_Oficinas.Enabled = false;
      Panel_SubCanal.Enabled = false;
      Panel_TipoCanal.Enabled = false;
      Panel_Corporacion.Enabled = false;
      panel_Asignar_Competidires.Enabled = true;

  }

  private void desactivarControlesAsignacionCompetidores()
  {

      Panel_Canal.Enabled = true;
      Panel_Material_POP.Enabled = true;
      Panel_Servicio.Enabled = true;
      Panel_Oficinas.Enabled = true;
      Panel_SubCanal.Enabled = true;
      Panel_TipoCanal.Enabled = true;
      Panel_Corporacion.Enabled = true;
      panel_Asignar_Competidires.Enabled = true;

  }

  public void llenaClientes()
  {

      /*DataTable dt = new DataTable();
       dt = oConn.ejecutarDataTable("UP_WEBXPLORA_CONSULTAS_COMPANYXTIPO", "2");
       ddlAsignacionCompetencia_Cliente.DataSource = dt;
       ddlAsignacionCompetencia_Cliente.DataTextField = "Company_Name";
       ddlAsignacionCompetencia_Cliente.DataValueField = "Company_id";
       ddlAsignacionCompetencia_Cliente.DataBind();*/
      
      ListItem listItem0 = new ListItem("Company00", "0");
      ListItem listItem1 = new ListItem("Company01", "1");
      ListItem listItem2 = new ListItem("Company02", "2");
      ListItem listItem3 = new ListItem("Company03", "3");

      ddlAsignacionCompetencia_Cliente.Items.Add(listItem0);
      ddlAsignacionCompetencia_Cliente.Items.Add(listItem1);
      ddlAsignacionCompetencia_Cliente.Items.Add(listItem2);
      ddlAsignacionCompetencia_Cliente.Items.Add(listItem3);
      
      ddlAsignacionCompetencia_Cliente.Enabled = true;
      
  }

  public void llenaCopetencia()
  {

      /*DataTable dt = new DataTable();
       dt = oConn.ejecutarDataTable("UP_WEBXPLORA_CONSULTAS_COMPANYXTIPO", "3");
       chkAsignacionCompetencia_competidores.DataSource = dt;
       chkAsignacionCompetencia_competidores.DataTextField = "Company_Name";
       chkAsignacionCompetencia_competidores.DataValueField = "Company_id";
       chkAsignacionCompetencia_competidores.DataBind();*/

      ListItem listItem0 = new ListItem("Company00", "0");
      ListItem listItem1 = new ListItem("Company01", "1");
      ListItem listItem2 = new ListItem("Company02", "2");
      ListItem listItem3 = new ListItem("Company03", "3");

      chkAsignacionCompetencia_competidores.Items.Add(listItem0);
      chkAsignacionCompetencia_competidores.Items.Add(listItem1);
      chkAsignacionCompetencia_competidores.Items.Add(listItem2);
      chkAsignacionCompetencia_competidores.Items.Add(listItem3);
      chkAsignacionCompetencia_competidores.Enabled = true;
      
  }

  protected void btnAsignarCompetidores_Crear_Click(object sender, EventArgs e)
  {
      activarControlesAsignacionCompetidores();
      llenaClientes();
      llenaCopetencia();
      btnAsignarCompetidores_Guardar.Visible = true;
      btnAsignarCompetidores_Crear.Visible = false;
      btnAsignarCompetidores_Consultar.Visible = false;
  }

  protected void btnAsignarCompetidores_Guardar_Click(object sender, EventArgs e)
  {
      AD_AsignacionCompetidores oAD_AsignacionCompetidores = new AD_AsignacionCompetidores();
      EAD_AsignacionCompetidores oEAD_AsignacionCompetidores = new EAD_AsignacionCompetidores();

      if (ddlAsignacionCompetencia_Cliente.SelectedIndex != 0 && chkAsignacionCompetencia_competidores.SelectedItem != null)
      {
          Usuario ouser = new Usuario();
          string username = "";
          try
          {
              username = Convert.ToString(this.Session["sUser"]);
          }
          catch (Exception ex)
          {

          }

          int st;
          string cliente = ddlAsignacionCompetencia_Cliente.SelectedValue;

          foreach (ListItem item in chkAsignacionCompetencia_competidores.Items)
          {
              string competidor = item.Value;
              bool nodoxcanal_estado = item.Selected;
              try
              {

                  if (nodoxcanal_estado == true)
                  {
                      DataTable dt = oAD_AsignacionCompetidores.ConsultarAsignacionCompetidores(cliente, competidor);

                      if (dt.Rows.Count == 0)
                      {
                          oEAD_AsignacionCompetidores = oAD_AsignacionCompetidores.RegistrarAsignacionCompetidores(Convert.ToInt32(cliente), Convert.ToInt32(competidor), true, username, DateTime.Now, username, DateTime.Now);

                          oAD_AsignacionCompetidores.RegistrarAsignacionCompetidoresTMP(oEAD_AsignacionCompetidores.AA_idAsignacionCompe.ToString(), oEAD_AsignacionCompetidores.Company_id.ToString(), oEAD_AsignacionCompetidores.Compay_idCompe.ToString(), Convert.ToInt32(oEAD_AsignacionCompetidores.AA_Status).ToString());
                      } 
                      else
                      {
                          Alertas.CssClass = "MensajesError";
                          LblFaltantes.Text = "El Cliente " + dt.Rows[0][4].ToString() + " ya tiene asignado esa Competencaia" + dt.Rows[0][2].ToString();
                          MensajeAlerta();

                          return;

                      }
                  }


              }
              catch (Exception ex) { }
          }

          Alertas.CssClass = "MensajesCorrecto";
          LblFaltantes.Text = "Asignación Cliente a Competidor registrada correctamente.";
          MensajeAlerta();
          ddlAsignacionCompetencia_Cliente.SelectedIndex = 0;
          foreach (ListItem item in chkAsignacionCompetencia_competidores.Items)
          {
              item.Selected = false;
          }
      }
      else
      {
          Alertas.CssClass = "MensajesError";
          LblFaltantes.Text = "Seleccione el Cliente y los Competidores a asociar";
          MensajeAlerta();
      }


    

      
  }



  protected void btnAsignarCompetidores_Cancelar_Click(object sender, EventArgs e)
  {
      desactivarControlesAsignacionCompetidores();
      btnAsignarCompetidores_Crear.Visible = true;
      btnAsignarCompetidores_Consultar.Visible = true;
      btnAsignarCompetidores_Cancelar.Visible = true;
  }

        #endregion

        #region Tipo Material POP

  private void activarControlesTipoMaterialPOP()
  {

      Panel_Canal.Enabled = false;
      Panel_Material_POP.Enabled = false;
      Panel_Servicio.Enabled = false;
      Panel_Oficinas.Enabled = false;
      Panel_SubCanal.Enabled = false;
      Panel_TipoCanal.Enabled = false;
      Panel_Corporacion.Enabled = false;
      panel_Asignar_Competidires.Enabled = false;
      Panel_Material_POP.Enabled = false;
      panel_TipoMPOP.Enabled = true;

  }

  private void desactivarControlesTipoMaterialPOP()
  {

      Panel_Canal.Enabled = true;
      Panel_Material_POP.Enabled = true;
      Panel_Servicio.Enabled = true;
      Panel_Oficinas.Enabled = true;
      Panel_SubCanal.Enabled = true;
      Panel_TipoCanal.Enabled = true;
      Panel_Corporacion.Enabled = true;
      panel_Asignar_Competidires.Enabled = true;
      panel_TipoMPOP.Enabled = true;
      Panel_Material_POP.Enabled = true;

  }

  //private void desactivarControlesMaterialPOP()
  //{
  //    rblEstado_tipoMaterialPOP.Enabled = false;
  //    txtNombre_tipoMaterialPOP.Enabled = false;
  //    Panel_Canal.Enabled = true;
  //    Panel_Material_POP.Enabled = true;
  //    Panel_Servicio.Enabled = true;
  //    Panel_Oficinas.Enabled = true;
  //    Panel_SubCanal.Enabled = true;
  //    Panel_TipoCanal.Enabled = true;
  //    Panel_Corporacion.Enabled = true;

  //}

  protected void btnCrear_TipoMaterialPOP_Crear_Click(object sender, EventArgs e)
  {
      activarControlesAsignacionCompetidores();

      txtNombre_tipoMaterialPOP.Enabled = true;
      btnGuardar_TipoMaterialPOP.Visible = true;
      btnCrear_TipoMaterialPOP.Visible = false;
      btnConsultar_TipoMaterialPOP.Visible = false;

  }


  protected void btnGuardar_TipoMaterialPOP_Click(object sender, EventArgs e)
  {

      Conexion cn = new Conexion();

      if (txtNombre_tipoMaterialPOP.Text !="" )
      {
          DataTable dt = cn.ejecutarDataTable("UP_WEBSIGE_CONSULTAR_AD_TIPOMATERIALPOP", txtNombre_tipoMaterialPOP.Text);

          if (dt.Rows.Count == 0)
          {

              cn.ejecutarDataTable("UP_WEBSIGE_REGISTAR_AD_TIPOMATERIALPOP", txtNombre_tipoMaterialPOP.Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

              Alertas.CssClass = "MensajesCorrecto";
              LblFaltantes.Text = "El tipo de Material POP fue registrado con existo.";
              MensajeAlerta();
              txtNombre_tipoMaterialPOP.Text = "";

          }

          else 
          {
              Alertas.CssClass = "MensajesError";
              LblFaltantes.Text = "El nombre del Tipo de Material POP ya existe en la base de datos";
              MensajeAlerta();
          }

      }
      else
      {
          Alertas.CssClass = "MensajesError";
          LblFaltantes.Text = "El nombre del Tipo de Material POP no debe ser vacio";
          MensajeAlerta();
      }

  }

        void desactivar()
    {
        btnCrear_TipoMaterialPOP.Visible = true;
        btnConsultar_TipoMaterialPOP.Visible = true;
        btnCancelar_TipoMaterialPOP.Visible = true;
        btnGuardar_TipoMaterialPOP.Visible = false;
        btnActualizar_TipoMaterialPOP.Visible = false;
        txtNombre_tipoMaterialPOP.Enabled = false;
        rblEstado_tipoMaterialPOP.Enabled = false;
    }

  protected void btnCancelar_TipoMaterialPOP_Cancelar_Click(object sender, EventArgs e)
  {
      desactivarControlesTipoMaterialPOP();
      desactivar();
     
  }

  protected void btnBuscarTipoMPOP_Click(object sender, EventArgs e)
  {
      desactivarControlesTipoMaterialPOP();
      LblFaltantes.Text = "";
      txtTipoMaterialPOP_Buscar.Text = txtTipoMaterialPOP_Buscar.Text.TrimStart();

      if (txtTipoMaterialPOP_Buscar.Text == "")
      {
          this.Session["mensajealert"] = "Nombre de Material POP";
          Alertas.CssClass = "MensajesError";
          LblFaltantes.Text = "Ingrese el nombre del tipo de material POP";
          MensajeAlerta();
          IbtnMPOP_ModalPopupExtender.Show();
          return;
      }

      BuscarActivarbotnesMaterialPOP();
      //BtnCrearPOP.Visible = true;

      string idtipoMa = "";
      Conexion cn = new Conexion();
      DataTable  dt= cn.ejecutarDataTable("UP_WEBSIGE_CONSULTAR_AD_TIPOMATERIALPOP", txtTipoMaterialPOP_Buscar.Text);

      if (dt != null)
      {
          if (dt.Rows.Count > 0)
          {

              for (int i = 0; i <= dt.Rows.Count - 1; i++)
              {
                  txtNombre_tipoMaterialPOP.Text = dt.Rows[0]["TipoMaDescripcion"].ToString().Trim();

                  this.Session["TipoMaDescripcion"] = txtNombre_tipoMaterialPOP.Text;
                  this.Session["idtipoMa"] = dt.Rows[0]["idtipoMa"].ToString().Trim();

                  estado = Convert.ToBoolean(dt.Rows[0]["TipoMaStatus"].ToString().Trim());

                  rblEstado_tipoMaterialPOP.Items[0].Selected = estado;
                  rblEstado_tipoMaterialPOP.Items[1].Selected = !estado;

                  this.Session["tmpop"] = dt;
                  this.Session["i"] = 0;

              }
              if (dt.Rows.Count == 1)
              {
                  btnPreg5.Visible = false;
                  btnUreg5.Visible = false;
                  btnAreg5.Visible = false;
                  btnSreg5.Visible = false;
              }
              else
              {
                  btnPreg5.Visible = true;
                  btnUreg5.Visible = true;
                  btnAreg5.Visible = true;
                  btnSreg5.Visible = true;
              }
              btnActualizar_TipoMaterialPOP.Visible = true;
              txtNombre_tipoMaterialPOP.Enabled = true;
              rblEstado_tipoMaterialPOP.Enabled = true;

          }
          else
          {
              SavelimpiarControlesMaterialPOP();
              saveActivarbotonesMaterialPOP();
              Alertas.CssClass = "MensajesError";
              LblFaltantes.Text = "La consulta realizada no devolvió ninguna respuesta";
              MensajeAlerta();
              IbtnMPOP_ModalPopupExtender.Show();
          }
      }
  }

  protected void btnActualizar_TipoMaterialPOP_Click(object sender, EventArgs e)
  {
      
          Conexion cn = new Conexion();
      if (txtNombre_tipoMaterialPOP.Text != "")
      {

          DataTable dt = cn.ejecutarDataTable("UP_WEBSIGE_CONSULTAR_AD_TIPOMATERIALPOP", txtNombre_tipoMaterialPOP.Text);

          if (dt.Rows.Count == 0)
          {
              bool estado;

              if (rblEstado_tipoMaterialPOP.SelectedValue == "Habilitado")
              {
                  estado = true;
              }
              else
              {
                  estado = false;
              }


              cn.ejecutarDataTable("UP_WEBSIGE_ACTUALIZAR_AD_TIPOMATERIALPOP", Convert.ToInt32(this.Session["idtipoMa"].ToString()), txtNombre_tipoMaterialPOP.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);


              Alertas.CssClass = "MensajesCorrecto";
              LblFaltantes.Text = "El tipo de Material POP fue Actualizado con existo.";
              MensajeAlerta();
              txtNombre_tipoMaterialPOP.Text = "";
              rblEstado_tipoMaterialPOP.Items[0].Selected = true;
               rblEstado_tipoMaterialPOP.Items[1].Selected = false;
            desactivarControlesTipoMaterialPOP();
             desactivar();
          }

          else
          {
              bool estado;

              if (rblEstado_tipoMaterialPOP.SelectedValue == "Habilitado")
              {
                  estado = true;
              }
              else
              {
                  estado = false;
              }


              cn.ejecutarDataTable("UP_WEBSIGE_ACTUALIZAR_AD_TIPOMATERIALPOP", Convert.ToInt32(this.Session["idtipoMa"].ToString()), this.Session["TipoMaDescripcion"].ToString(), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);


              Alertas.CssClass = "MensajesCorrecto";
              LblFaltantes.Text = "El tipo de Material POP fue Actualizado con existo.";
              MensajeAlerta();
              txtNombre_tipoMaterialPOP.Text = "";
              rblEstado_tipoMaterialPOP.Items[0].Selected = true;
              rblEstado_tipoMaterialPOP.Items[1].Selected = false;
              desactivarControlesTipoMaterialPOP();
              desactivar();
          }

      }
      else
      {
          Alertas.CssClass = "MensajesError";
          LblFaltantes.Text = "El nombre del Tipo de Material POP no debe ser vacio";
          MensajeAlerta();
      }

  }

        #endregion
    }
}
