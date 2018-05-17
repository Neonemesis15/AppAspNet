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
    //-- Author:		    <Ing. Magaly Jimenez>
    //-- Create date:       <09/09/2010>
    //-- Description:       <Permite al actor Administrador de xplora realizar todos los procesos para la administracion 
    //--                    de gestión puntos de venta en la aplicacion xplora>
    //-- Requerimiento No.  <Módelo de requerimientos funcionales Módulo Administrativo >
    //-- =============================================
    public partial class GestiónPuntodeVenta : System.Web.UI.Page
    {

        private bool estado;
        private string sSector = "";
        //int iid_malla;
        //int iid_ClientPDV;
        //bool bContinuar = true;
        //bool bContinuar = true;
        private string sCodMalla = "0";
        private int iCompany_id;
        private int iid_segmentsType;
        private string repetido = "";
        private string repetido1 = "";
        private string repetido2 = "";
        private string repetido3 = "";
        private string sSegmentsType, sSegment, sNodeComType_name, sdex,  sNodeType, sNodeComname, sMalla, sNode;
        private int RUC;//, CODPDV;
        private int recsearch;
        private DataTable recorrido = null;
        private string sPdvCodProvince, sPdvRegTax, sPdvName, sPdvChannel;

        private string   spais, sCanal, sAgrupacion ;
        private int iCliente, itipoAgrupacion;

        private Segments_Type oSegmentstype = new Segments_Type();
        private Segments oSegments = new Segments();
        private NodeType oTypeNode = new NodeType();
        private AD_Distribuidora oDex = new AD_Distribuidora();
        private NodeComercial dnode = new NodeComercial();
        private Malla oMalla = new Malla();
        private Sector oSector = new Sector();
        private PuntosDV oPDV = new PuntosDV();

        private Conexion oConn = new Lucky.Data.Conexion();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos owsadministrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos obtenerid = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                try
                {
                   
                    LlenacomboconsultaDistribuidora();
                    LlenacomboBuscarClienteSector();
                    comboCiudadesBuscaPDV();
                    LLenaComboBuscarPaisPDVC();
                    comboBCanalesPDV();
                    combotiponodo();
                    cargarCorporacion();
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
        private void activarControlesSegmentos()
        {
            TxtCodSegmento.Enabled = false;
            TxtTipoSegmento.Enabled = true;
            TxtSegmento.Enabled = true;
            TxtDescSegmento.Enabled = true;
            ChkSegmento.Enabled = true;
            RbtnEstadoSegmento.Enabled = false;
            BtnAgrSegmento.Enabled = true;
            TabPanelSegmentos.Enabled =true;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = false;
            TabDistribuidora.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void desactivarControlesSegmentos()
        {
            TxtCodSegmento.Enabled = false;
            TxtTipoSegmento.Enabled = false;
            TxtSegmento.Enabled = false;
            TxtDescSegmento.Enabled = false;
            ChkSegmento.Enabled = false;
            RbtnEstadoSegmento.Enabled = false;
            BtnAgrSegmento.Enabled = false;
            TabPanelSegmentos.Enabled = true;
            TabPanelTipoAgrupación.Enabled = true;
            TabPanelAgrupaciónComercial.Enabled = true;
            Panel_Mallas.Enabled = true;
            Panel_Sector.Enabled = true;
            TabPanelPDV.Enabled = true;
            PanelPDVCliente.Enabled = true;
            TabDistribuidora.Enabled = true;
            Panel_ChannelXACommercial.Enabled = true;
        }
        private void crearActivarbotonesSegmentos()
        {
            BtnCrearSegmento.Visible = false;
            BtnsaveSegmento.Visible = true;
            BtnconsultaSegmento.Visible = false;
            BtneditSegmento.Visible = false;
            BtnActualizaSegmento.Visible = false;
            BtncancelSegmento.Visible = true;
            PregSegmento.Visible = false;
            AregSegmento.Visible = false;
            SregSegmento.Visible = false;
            UregSegmento.Visible = false;
            BtnAgrSegmento.Enabled = true;
        }
        private void SavelimpiarcontrolesSegmentos()
        {
            TxtCodSegmento.Text = "";
            TxtTipoSegmento.Text = "";
            TxtSegmento.Text = "";
            TxtDescSegmento.Text = "";
            ChkSegmento.Items.Clear();
            RbtnEstadoSegmento.Items[0].Selected = true;
            RbtnEstadoSegmento.Items[1].Selected = false;
            TxtBTipoSegmento.Text = "";
        }
        private void saveActivarbotonesSegmentos()
        {
            BtnCrearSegmento.Visible = true;
            BtnsaveSegmento.Visible = false;
            BtnconsultaSegmento.Visible = true;
            BtneditSegmento.Visible = false;
            BtnActualizaSegmento.Visible = false;
            BtncancelSegmento.Visible = true;
            PregSegmento.Visible = false;
            AregSegmento.Visible = false;
            SregSegmento.Visible = false;
            UregSegmento.Visible = false;
        }
        private void EditarActivarbotonesSegmentos()
        {
            BtnCrearSegmento.Visible = false;
            BtnsaveSegmento.Visible = false;
            BtnconsultaSegmento.Visible = true;
            BtneditSegmento.Visible = false;
            BtnActualizaSegmento.Visible = true;
            BtncancelSegmento.Visible = true;
            PregSegmento.Visible = false;
            AregSegmento.Visible = false;
            SregSegmento.Visible = false;
            UregSegmento.Visible = false;
        }
        private void EditarActivarControlesSegmentos()
        {
            TxtCodSegmento.Enabled = false;
            TxtTipoSegmento.Enabled = true;
            TxtSegmento.Enabled = true;
            TxtDescSegmento.Enabled = true;
            ChkSegmento.Enabled = true;
            RbtnEstadoSegmento.Enabled = true;
            BtnAgrSegmento.Enabled = true;
            TabPanelSegmentos.Enabled = true;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            TabDistribuidora.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
        }
        private void BuscarActivarbotnesSegmentos()
        {
            BtnCrearSegmento.Visible = false;
            BtnsaveSegmento.Visible = false;
            BtnconsultaSegmento.Visible = true;
            BtneditSegmento.Visible = true;
            BtncancelSegmento.Visible = true;
        }
        private void activarControlesTipoAgrupación()
        {
            RbtnEstadoTiponodo.Enabled = false;
            TxtCodtipnodo.Enabled = false;
            txtNomtiponodo.Enabled = true;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = true;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
            TabDistribuidora.Enabled = false;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void desactivarControlesTipoAgrupación()
        {
            RbtnEstadoTiponodo.Enabled = false;
            TxtCodtipnodo.Enabled = false;
            txtNomtiponodo.Enabled = false;
            TabPanelSegmentos.Enabled = true;
            TabPanelTipoAgrupación.Enabled = true;
            TabPanelAgrupaciónComercial.Enabled = true;
            Panel_Mallas.Enabled = true;
            Panel_Sector.Enabled = true;
            TabPanelPDV.Enabled = true;
            TabDistribuidora.Enabled = true;
            PanelPDVCliente.Enabled = true;
            Panel_ChannelXACommercial.Enabled = true;
        }
        private void crearActivarbotonesTipoAgrupación()
        {
            BtnCrearTipoNodo.Visible = false;
            BtnsaveTipoNodo.Visible = true;
            BtnconsultaTipoNodo.Visible = false;
            BtneditTipoNodo.Visible = false;
            BtnActualizaTiponNodo.Visible = false;
            BtncancelTipoNodo.Visible = true;
            PregTipoNodo.Visible = false;
            AregTipoNodo.Visible = false;
            SregTipoNodo.Visible = false;
            UregTipoNodo.Visible = false;
        }
        private void SavelimpiarcontrolesTipoAgrupación()
        {
            TxtCodtipnodo.Text = "";
            txtNomtiponodo.Text = "";
            RbtnEstadoTiponodo.Items[0].Selected = true;
            RbtnEstadoTiponodo.Items[1].Selected = false;
            TxtBNomTipoNodo.Text = "";
        }
        private void saveActivarbotonesTipoAgrupación()
        {
            BtnCrearTipoNodo.Visible = true;
            BtnsaveTipoNodo.Visible = false;
            BtnconsultaTipoNodo.Visible = true;
            BtneditTipoNodo.Visible = false;
            BtnActualizaTiponNodo.Visible = false;
            BtncancelTipoNodo.Visible = true;
            PregTipoNodo.Visible = false;
            AregTipoNodo.Visible = false;
            SregTipoNodo.Visible = false;
            UregTipoNodo.Visible = false;
        }
        private void EditarActivarbotonesTipoAgrupación()
        {
            BtnCrearTipoNodo.Visible = false;
            BtnsaveTipoNodo.Visible = false;
            BtnconsultaTipoNodo.Visible = true;
            BtneditTipoNodo.Visible = false;
            BtnActualizaTiponNodo.Visible = true;
            BtncancelTipoNodo.Visible = true;
            PregTipoNodo.Visible = false;
            AregTipoNodo.Visible = false;
            SregTipoNodo.Visible = false;
            UregTipoNodo.Visible = false;
        }
        private void EditarActivarControlesTipoAgrupación()
        {
            RbtnEstadoTiponodo.Enabled = true;
            TxtCodtipnodo.Enabled = false;
            txtNomtiponodo.Enabled = true;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = true;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = false;
            TabDistribuidora.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void BuscarActivarbotnesTipoAgrupación()
        {
            BtnCrearTipoNodo.Visible = false;
            BtnsaveTipoNodo.Visible = false;
            BtnconsultaTipoNodo.Visible = true;
            BtneditTipoNodo.Visible = true;
            BtnActualizaTiponNodo.Visible = false;
            BtncancelTipoNodo.Visible = true;
        }
        private void combotiponodo()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 28);
            //se llena Tipo de nodos en Nodo comercial
            CmbSelTipNodo.DataSource = ds;
            CmbSelTipNodo.DataTextField = "NodeComType_name";
            CmbSelTipNodo.DataValueField = "idNodeComType";
            CmbSelTipNodo.DataBind();
            ds = null;
            foreach (ListItem li in CmbSelTipNodo.Items)
            {
                ddl_bnc_tipo.Items.Add(li);
            }

        }
        private void activarControlesAgrupación()
        {
            RbtnEstadonodo.Enabled = false;
            TxtCodnodo.Enabled = false;
            txtNomnodo.Enabled = true;
            CmbSelTipNodo.Enabled = true;
            cmbCorporacion.Enabled = true;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = true;
            Panel_Mallas.Enabled =false;
            Panel_Sector.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
            TabDistribuidora.Enabled = false;
            ddlgpdvcliente.Enabled = true;
            txtdireccion.Enabled = true;
            cbxlnodecanal.Enabled = true;
            ddlSelCountry.Enabled = true;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void desactivarControlesAgrupación()
        {
            RbtnEstadonodo.Enabled = false;
            TxtCodnodo.Enabled = false;
            txtNomnodo.Enabled = false;
            CmbSelTipNodo.Enabled = false;
            cmbCorporacion.Enabled = false;
            ddlSelCountry.Enabled = false;
            ddlDpto.Enabled = false;
            ddlProv.Enabled = false;
            ddlDist.Enabled = false;
            ddlBarrio.Enabled = false;
            TabPanelSegmentos.Enabled = true;
            TabPanelTipoAgrupación.Enabled = true;
            TabPanelAgrupaciónComercial.Enabled = true;
            Panel_Mallas.Enabled = true;
            Panel_Sector.Enabled = true;
            TabPanelPDV.Enabled = true;
            PanelPDVCliente.Enabled =true;
            TabDistribuidora.Enabled = true;
            txtdireccion.Enabled = false;
            cbxlnodecanal.Enabled = false;
            ddlgpdvcliente.Enabled = false;            
            cbxlnodecanal.Visible = true;
            Panel_ChannelXACommercial.Enabled = true;
        }
        private void crearActivarbotonesAgrupación()
        {
            BtnCrearNodo.Visible = false;
            BtnsaveNodo.Visible = true;
            BtnconsultaNodo.Visible = false;
            //BtneditNodo.Visible = false;
            //BtnActualizaNodo.Visible = false;
            BtncancelNodo.Visible = true;
            //PregNodo.Visible = false;
            //AregNodo.Visible = false;
            //SregNodo.Visible = false;
            //UregNodo.Visible = false;
        }
        private void SavelimpiarcontrolesAgrupación()
        {
            TxtCodnodo.Text = "";
            txtNomnodo.Text = "";
            CmbSelTipNodo.Text = "0";
            cmbCorporacion.Text = "0";
            RbtnEstadonodo.Items[0].Selected = true;
            RbtnEstadonodo.Items[1].Selected = false;
            cbxlnodecanal.Items.Clear();
            if(ddlgpdvcliente.Items.Count > 0)
                ddlgpdvcliente.SelectedIndex = 0;
            TxtBNomNodo.Text = "";
            txtdireccion.Text = "";
            ddlSelCountry.Text = "0";

            ddlDpto.Items.Clear();
            ddlProv.Items.Clear();
            ddlDist.Items.Clear();
            ddlBarrio.Items.Clear();
            ddlDpto.CssClass = null;
            ddlProv.CssClass = null;
            ddlDist.CssClass = null;
            ddlBarrio.CssClass = null;
        }
        private void saveActivarbotonesAgrupación()
        {
            BtnCrearNodo.Visible = true;
            BtnsaveNodo.Visible = false;
            BtnconsultaNodo.Visible = true;
            //BtneditNodo.Visible = false;
            //BtnActualizaNodo.Visible = false;
            BtncancelNodo.Visible = true;
            //PregNodo.Visible = false;
            //AregNodo.Visible = false;
            //SregNodo.Visible = false;
            //UregNodo.Visible = false;
        }
        private void EditarActivarbotonesAgrupación()
        {
            BtnCrearNodo.Visible = false;
            BtnsaveNodo.Visible = false;
            BtnconsultaNodo.Visible = true;
            //BtneditNodo.Visible = false;
            //BtnActualizaNodo.Visible = true;
            BtncancelNodo.Visible = true;
            //PregNodo.Visible = false;
            //AregNodo.Visible = false;
            //SregNodo.Visible = false;
            //UregNodo.Visible = false;
        }
        private void EditarActivarControlesAgrupación()
        {
            RbtnEstadonodo.Enabled = true;
            TxtCodnodo.Enabled = false;
            txtNomnodo.Enabled = true;
            CmbSelTipNodo.Enabled = true;
            cmbCorporacion.Enabled = true;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = true;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
            TabDistribuidora.Enabled = false;
            if (ddlgpdvcliente.Text == "0")
                ddlgpdvcliente.Enabled = true;
            txtdireccion.Enabled = true;
            cbxlnodecanal.Enabled = true;
        }
        private void BuscarActivarbotnesAgrupación()
        {
            BtnCrearNodo.Visible = false;
            BtnsaveNodo.Visible = false;
            BtnconsultaNodo.Visible = true;
            //BtneditNodo.Visible = true;
            //BtnActualizaNodo.Visible = false;
            BtncancelNodo.Visible = true;
        }
        private void activarControlesmalla()
        {
            TxtCodMallas.Enabled = false;
            cmbClienteMallas.Enabled = true;
            TxtNomallas.Enabled = true;
            RbtnmallasStatus.Enabled = false;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = true;
            Panel_Sector.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
            TabDistribuidora.Enabled = false;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void desactivarControlesmalla()
        {
            TxtCodMallas.Enabled = false;
            cmbClienteMallas.Enabled = false;
            TxtNomallas.Enabled = false;
            RbtnmallasStatus.Enabled = false;
            TabPanelSegmentos.Enabled = true;
            TabPanelTipoAgrupación.Enabled = true;
            TabPanelAgrupaciónComercial.Enabled = true;
            Panel_Mallas.Enabled = true;
            Panel_Sector.Enabled =true;
            TabPanelPDV.Enabled = true;
            PanelPDVCliente.Enabled = true;
            TabDistribuidora.Enabled = true;
            Panel_ChannelXACommercial.Enabled = true;
        }
        private void crearActivarbotonesmalla()
        {
            BtnCrearmallas.Visible = false;
            BtnSavemalla.Visible = true;
            BtnConsultamallas.Visible = false;
            BtnEditmallas.Visible = false;
            BtnActualizamallas.Visible = false;
            BtnCancelmalla.Visible = true;
        }
        private void Savelimpiarcontrolesmalla()
        {
            TxtCodMallas.Text = "";
            cmbClienteMallas.Text = "0";
            TxtNomallas.Text = "";
            RbtnmallasStatus.Items[0].Selected = true;
            RbtnmallasStatus.Items[1].Selected = false;
            TxtBNommalla.Text = "";
        }
        private void saveActivarbotonesmalla()
        {
            BtnCrearmallas.Visible = true;
            BtnSavemalla.Visible = false;
            BtnConsultamallas.Visible = true;
            BtnEditmallas.Visible = false;
            BtnActualizamallas.Visible = false;
            BtnCancelmalla.Visible = true;
        }
        private void EditarActivarbotonesmalla()
        {
            BtnCrearmallas.Visible = false;
            BtnSavemalla.Visible = false;
            BtnConsultamallas.Visible = true;
            BtnEditmallas.Visible = false;
            BtnActualizamallas.Visible = true;
            BtnCancelmalla.Visible = true;
        }
        private void EditarActivarControlesmalla()
        {
            TxtCodMallas.Enabled = false;
            cmbClienteMallas.Enabled = true;
            TxtNomallas.Enabled = true;
            RbtnmallasStatus.Enabled = true;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = true;
            Panel_Sector.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
            TabDistribuidora.Enabled = false;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void BuscarActivarbotnesmalla()
        {
            BtnCrearmallas.Visible = false;
            BtnSavemalla.Visible = false;
            BtnConsultamallas.Visible = true;
            BtnEditmallas.Visible = true;
            BtnActualizamallas.Visible = false;
            BtnCancelmalla.Visible = true;
        }
        private void comboclienteenMalla()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en Usuarios
            cmbClienteMallas.DataSource = ds;
            cmbClienteMallas.DataTextField = "Company_Name";
            cmbClienteMallas.DataValueField = "Company_id";
            cmbClienteMallas.DataBind();
        }
        private void activarControlesSector()
        {
            TxtCodSector.Enabled = false;
            TxtNomSector.Enabled = true;
            CmbmallaSector.Enabled = true;
            CmbCliente.Enabled = true;
            RbtnStatusSector.Enabled = false;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = true;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
            TabDistribuidora.Enabled = false;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void desactivarControleSector()
        {
            TxtCodSector.Enabled = false;
            TxtNomSector.Enabled = false;
            CmbmallaSector.Enabled = false;
            CmbCliente.Enabled = false;
            RbtnStatusSector.Enabled = false;
            TabPanelSegmentos.Enabled = true;
            TabPanelTipoAgrupación.Enabled = true;
            TabPanelAgrupaciónComercial.Enabled = true;
            Panel_Mallas.Enabled =true;
            Panel_Sector.Enabled = true;
            TabPanelPDV.Enabled = true;
            PanelPDVCliente.Enabled =true;
            TabDistribuidora.Enabled = true;
            Panel_ChannelXACommercial.Enabled = true;
        }
        private void crearActivarbotonesSector()
        {
            BtnCrearSector.Visible = false;
            BtnsaveSector.Visible = true;
            BtnSearchSector.Visible = false;
            BtnEditSector.Visible = false;
            BtnActualizaSector.Visible = false;
            BtnCancelSector.Visible = true;
        }
        private void SavelimpiarcontrolesSector()
        {
            TxtCodSector.Text = "";
            TxtNomSector.Text = "";
            CmbmallaSector.Text = "0";
            CmbCliente.Text = "0";
            RbtnStatusSector.Items[0].Selected = true;
            RbtnStatusSector.Items[1].Selected = false;
            CmbBmallaSector.Text = "0";
            TxtBNomSector.Text = "";
        }
        private void saveActivarbotonesSector()
        {
            BtnCrearSector.Visible = true;
            BtnsaveSector.Visible = false;
            BtnSearchSector.Visible = true;
            BtnEditSector.Visible = false;
            BtnActualizaSector.Visible = false;
            BtnCancelSector.Visible = true;
        }
        private void EditarActivarbotonesSector()
        {
            BtnCrearSector.Visible = false;
            BtnsaveSector.Visible = false;
            BtnSearchSector.Visible = true;
            BtnEditSector.Visible = false;
            BtnActualizaSector.Visible = true;
            BtnCancelSector.Visible = true;
            PregSector.Visible = false;
            AregSector.Visible = false;
            SregSector.Visible = false;
            UregSector.Visible = false;
        }
        private void EditarActivarControlesSector()
        {
            TxtCodSector.Enabled = false;
            TxtNomSector.Enabled = true;
            CmbmallaSector.Enabled = true;
            CmbCliente.Enabled = true;
            RbtnStatusSector.Enabled = true;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = true;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
            TabDistribuidora.Enabled = false;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void BuscarActivarbotnesSector()
        {
            BtnCrearSector.Visible = false;
            BtnsaveSector.Visible = false;
            BtnSearchSector.Visible = true;
            BtnEditSector.Visible = true;
            BtnActualizaSector.Visible = false;
            BtnCancelSector.Visible = true;
        }
        private void LlenacomboClienteSector()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosSector(0);
            //se llena cliente producto en Sector
            CmbCliente.DataSource = ds.Tables[2];
            CmbCliente.DataTextField = "Company_Name";
            CmbCliente.DataValueField = "Company_id";
            CmbCliente.DataBind();
            CmbCliente.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
        }
        private void LlenacomboBuscarClienteSector()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosSector(0);
            //se llena cliente producto en Sector
            cmbBClienteSector.DataSource = ds.Tables[2];
            cmbBClienteSector.DataTextField = "Company_Name";
            cmbBClienteSector.DataValueField = "Company_id";
            cmbBClienteSector.DataBind();
            cmbBClienteSector.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
        }
        private void LlenacomboMallasenSector()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosSector(Convert.ToInt32(CmbCliente.SelectedValue));
            //se llena mallas producto en Sector
            CmbmallaSector.DataSource = ds.Tables[1];
            CmbmallaSector.DataTextField = "malla";
            CmbmallaSector.DataValueField = "id_malla";
            CmbmallaSector.DataBind();
            CmbmallaSector.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
        }
        private void LlenacomboBuscarMallasenSector()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosSector(Convert.ToInt32(cmbBClienteSector.SelectedValue));   
            //se llena mallas producto en consultar Sector
            CmbBmallaSector.DataSource = ds.Tables[0];
            CmbBmallaSector.DataTextField = "malla";
            CmbBmallaSector.DataValueField = "id_malla";
            CmbBmallaSector.DataBind();
           // CmbBmallaSector.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void activarControlesDistribuidora()
        {
            Rbtestadodistribuidora.Enabled = false;
            Texcodigod.Enabled = false;
            TextDistribuidora.Enabled = true;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
            TabDistribuidora.Enabled = true;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void desactivarControlesDistribuidora()
        {
            Rbtestadodistribuidora.Enabled = false;
            Texcodigod.Enabled = false;
            TextDistribuidora.Enabled = false;
            TabPanelSegmentos.Enabled = true;
            TabPanelTipoAgrupación.Enabled = true;
            TabPanelAgrupaciónComercial.Enabled = true;
            Panel_Mallas.Enabled = true;
            Panel_Sector.Enabled = true;
            TabPanelPDV.Enabled = true;
            PanelPDVCliente.Enabled = true;
            TabDistribuidora.Enabled = true;
            Panel_ChannelXACommercial.Enabled = true;
        }
        private void crearActivarbotonesDistribuidora()
        {
            creardistrib.Visible = false;
            Guardardistri.Visible = true;
            ConsultarDistr.Visible = false;
            editardistribuidor.Visible = false;
            ActualizarDistri.Visible = false;
            cancelardistrib.Visible = true;
            primero.Visible = false;
            antes.Visible = false;
            siguiente.Visible = false;
            ultimo.Visible = false;
        }
        private void SavelimpiarcontrolesDistribuidora()
        {
            Texcodigod.Text = "";
            TextDistribuidora.Text = "";
            Rbtestadodistribuidora.Items[0].Selected = true;
            Rbtestadodistribuidora.Items[1].Selected = false;
            cmbBDistribuidora.Text = "0";
        }
        private void saveActivarbotonesDistribuidora()
        {
            creardistrib.Visible = true;
            Guardardistri.Visible = false;
            ConsultarDistr.Visible = true;
            editardistribuidor.Visible = false;
            ActualizarDistri.Visible = false;
            cancelardistrib.Visible = true;
            primero.Visible = false;
            antes.Visible = false;
            siguiente.Visible = false;
            ultimo.Visible = false;
        }
        private void EditarActivarbotonesDistribuidora()
        {
            creardistrib.Visible = false;
            Guardardistri.Visible = false;
            ConsultarDistr.Visible = true;
            editardistribuidor.Visible = false;
            ActualizarDistri.Visible = true;
            cancelardistrib.Visible = true;
            primero.Visible = false;
            antes.Visible = false;
            siguiente.Visible = false;
            ultimo.Visible = false;
        }
        private void EditarActivarControlesDistribuidora()
        {
            Rbtestadodistribuidora.Enabled = true;
            Texcodigod.Enabled = false;
            TextDistribuidora.Enabled = true;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
            TabDistribuidora.Enabled = true;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void BuscarActivarbotnesDistribuidora()
        {
            creardistrib.Visible = false;
            Guardardistri.Visible = false;
            ConsultarDistr.Visible = true;
            editardistribuidor.Visible = true;
            ActualizarDistri.Visible = false;
            cancelardistrib.Visible = true;
        }
        private void LlenacomboconsultaDistribuidora()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOCONSULTADISTRIBUIDORA");
            //se llena mallas producto en Sector
            cmbBDistribuidora.DataSource = ds;
            cmbBDistribuidora.DataTextField = "Dex_Name";
            cmbBDistribuidora.DataValueField = "Id_Dex";
            cmbBDistribuidora.DataBind();           
        }    
        private void activarControlesPDV()
        {
            TxtCodPos.Enabled = false;
            cmbTipDocPDV.Enabled = true;
            TxtNumdocPDV.Enabled = true;
            TxtcontacPos.Enabled = true;
            TxtCargo1.Enabled = true;
            TxtcontacPos2.Enabled = true;
            TxtCargo2.Enabled = true;
            TxtRSocPos.Enabled = true;
            TxtNComPos.Enabled = true;
            TxtTelPos.Enabled = true;
            TxtanexPos.Enabled = true;
            TxtFaxPos.Enabled = true;
            cmbSelCountry.Enabled = true;
            cmbSelDpto.Enabled = true;
            cmbSelProvince.Enabled = true;
            cmbSelDistrict.Enabled = true;
            cmbSelComunity.Enabled = true;
            TxtDirPos.Enabled = true;
            TxtMailPos.Enabled = true;
            cmbSelCanal.Enabled = true;
            CmbTipMerc.Enabled = true;
            cmbNodoCom.Enabled = true;
            CmbSelSegPDV.Enabled = true;
            RBtnListStatusPos.Enabled = false;
            btnPreg7.Visible = false;
            btnAreg7.Visible = false;
            btnSreg7.Visible = false;
            btnUreg7.Visible = false;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = false;
            TabPanelPDV.Enabled = true;
            PanelPDVCliente.Enabled = false;
            TabDistribuidora.Enabled = false;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void desactivarControlePDV()
        {
            TxtCodPos.Enabled = false;
            cmbTipDocPDV.Enabled = false;
            TxtNumdocPDV.Enabled = false;
            TxtcontacPos.Enabled = false;
            TxtCargo1.Enabled = false;
            TxtcontacPos2.Enabled = false;
            TxtCargo2.Enabled = false;
            TxtRSocPos.Enabled = false;
            TxtNComPos.Enabled = false;
            TxtTelPos.Enabled = false;
            TxtanexPos.Enabled = false;
            TxtFaxPos.Enabled = false;
            //cmbDexpdv.Enabled = false;
            cmbSelCountry.Enabled = false;
            cmbSelDpto.Enabled = false;
            cmbSelProvince.Enabled = false;
            cmbSelDistrict.Enabled = false;
            cmbSelComunity.Enabled = false;
            TxtDirPos.Enabled = false;
            TxtMailPos.Enabled = false;
            cmbSelCanal.Enabled = false;
            CmbTipMerc.Enabled = false;
            cmbNodoCom.Enabled = false;
            CmbSelSegPDV.Enabled = false;
            ////cmbSelCadena.Enabled = false;
            RBtnListStatusPos.Enabled = false;
            TabPanelSegmentos.Enabled = true;
            TabPanelTipoAgrupación.Enabled = true;
            TabPanelAgrupaciónComercial.Enabled = true;
            Panel_Mallas.Enabled = true;
            Panel_Sector.Enabled = true;
            TabPanelPDV.Enabled = true;
            PanelPDVCliente.Enabled = true;
            TabDistribuidora.Enabled = true;
            Panel_ChannelXACommercial.Enabled = true;
        }
        private void crearActivarbotonesPDV()
        {
            btnCrearPos.Visible = false;
            btnsavePos.Visible = true;
            btnConsultarPos.Visible = false;
            btnEditPDV.Visible = false;
            btnActualizarPos.Visible = false;
            btnCancelPos.Visible = true;
            btnPreg7.Visible = false;
            btnAreg7.Visible = false;
            btnSreg7.Visible = false;
            btnUreg7.Visible = false;
        }
        private void SavelimpiarcontrolesPDV()
        {
            TxtCodPos.Text = "";
            cmbTipDocPDV.Text = "0";
            TxtNumdocPDV.Text = "";
            TxtcontacPos.Text = "";
            TxtCargo1.Text = "";
            TxtcontacPos2.Text = "";
            TxtCargo2.Text = "";
            TxtRSocPos.Text = "";
            TxtNComPos.Text = "";
            TxtTelPos.Text = "";
            TxtanexPos.Text = "";
            TxtFaxPos.Text = "";
            cmbSelCountry.Text = "0";
            //cmbDexpdv.Text = "0";
            cmbSelDpto.Items.Clear();
            cmbSelProvince.Items.Clear();
            cmbSelDistrict.Items.Clear();
            cmbSelComunity.Items.Clear();
            cmbSelDpto.CssClass = null;
            cmbSelProvince.CssClass = null;
            cmbSelDistrict.CssClass = null;
            cmbSelComunity.CssClass = null;
            TxtDirPos.Text = "";
            TxtMailPos.Text = "";
            cmbSelCanal.Text = "0";
            CmbTipMerc.Text = "0";
            cmbNodoCom.Items.Clear();
            CmbSelSegPDV.Text = "0";


            RBtnListStatusPos.Items[0].Selected = true;
            RBtnListStatusPos.Items[1].Selected = false;

         
            cmbbProvpdv.Text = "0";
            txtbidpdv.Text = "";
            Txtbnompdv.Text = "";
            cmbCanalBPDV.Text = "0";


        }
        private void saveActivarbotonesPDV()
        {
            btnCrearPos.Visible = true;
            btnsavePos.Visible = false;
            btnConsultarPos.Visible = true;
            btnEditPDV.Visible = false;
            btnActualizarPos.Visible = false;
            btnCancelPos.Visible = true;
            btnPreg7.Visible = false;
            btnAreg7.Visible = false;
            btnSreg7.Visible = false;
            btnUreg7.Visible = false;
        }
        private void EditarActivarbotonesPDV()
        {
            btnCrearPos.Visible = false;
            btnsavePos.Visible = false;
            btnConsultarPos.Visible = true;
            btnEditPDV.Visible = false;
            btnActualizarPos.Visible = true;
            btnCancelPos.Visible = true;
            btnPreg7.Visible = false;
            btnAreg7.Visible = false;
            btnSreg7.Visible = false;
            btnUreg7.Visible = false;
        }
        private void EditarActivarControlesPDV()
        {
            TxtCodPos.Enabled = false;
            cmbTipDocPDV.Enabled = true;
            TxtNumdocPDV.Enabled = true;
            TxtcontacPos.Enabled = true;
            TxtCargo1.Enabled = true;
            TxtcontacPos2.Enabled = true;
            TxtCargo2.Enabled = true;
            TxtRSocPos.Enabled = true;
            TxtNComPos.Enabled = true;
            TxtTelPos.Enabled = true;
            TxtanexPos.Enabled = true;
            TxtFaxPos.Enabled = true;
            cmbSelCountry.Enabled = true;
            //cmbDexpdv.Enabled = true;
            cmbSelDpto.Enabled = true;
            cmbSelProvince.Enabled = true;
            cmbSelDistrict.Enabled = true;
            cmbSelComunity.Enabled = true;
            TxtDirPos.Enabled = true;
            TxtMailPos.Enabled = true;
            cmbSelCanal.Enabled = true;
            CmbTipMerc.Enabled = true;
            cmbNodoCom.Enabled = true;
            CmbSelSegPDV.Enabled = true;
            RBtnListStatusPos.Enabled = true;
            btnPreg7.Visible = false;
            btnAreg7.Visible = false;
            btnSreg7.Visible = false;
            btnUreg7.Visible = false;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = false;
            TabPanelPDV.Enabled = true;
            TabDistribuidora.Enabled = false;
            Panel_ChannelXACommercial.Enabled = false;
            PanelPDVCliente.Enabled = false;
        }
        private void BuscarActivarbotnesPDV()
        {
            btnCrearPos.Visible = false;
            btnsavePos.Visible = false;
            btnConsultarPos.Visible = true;
            btnEditPDV.Visible = true;
            btnActualizarPos.Visible = false;
            btnCancelPos.Visible = true;
        }
        private void LlenacomboMallasenPDV()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosSector(Convert.ToInt32(cmbClientePDVC.SelectedValue));
            //se llena mallas producto en Sector
            CmbmallaSector.DataSource = ds.Tables[1];
            CmbmallaSector.DataTextField = "malla";
            CmbmallaSector.DataValueField = "id_malla";
            CmbmallaSector.DataBind();
            CmbmallaSector.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            //se llena mallas producto en consultar Sector
            CmbBmallaSector.DataSource = ds.Tables[0];
            CmbBmallaSector.DataTextField = "malla";
            CmbBmallaSector.DataValueField = "id_malla";
            CmbBmallaSector.DataBind();
            CmbBmallaSector.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void comboDoc()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 1);
            cmbTipDocPDV.DataSource = ds;
            cmbTipDocPDV.DataTextField = "Type_documento";
            cmbTipDocPDV.DataValueField = "id_typeDocument";
            cmbTipDocPDV.DataBind();
            ds = null;
        }
        private void llenaPaisPDV()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 26);
            cmbSelCountry.DataSource = ds;
            cmbSelCountry.DataTextField = "Name_Country";
            cmbSelCountry.DataValueField = "cod_Country";
            cmbSelCountry.DataBind();
            ds = null;
        }
        private void comboCanales()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 9);
            //se llena canales en PDV
            cmbSelCanal.DataSource = ds;
            cmbSelCanal.DataTextField = "Channel_Name";
            cmbSelCanal.DataValueField = "cod_Channel";
            cmbSelCanal.DataBind();
            cmbSelCanal.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
        }
        private void comboBCanalesPDV()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 9);
            //se llena canales en PDV
            cmbCanalBPDV.DataSource = ds;
            cmbCanalBPDV.DataTextField = "Channel_Name";
            cmbCanalBPDV.DataValueField = "cod_Channel";
            cmbCanalBPDV.DataBind();
            cmbCanalBPDV.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
        }
        private void comboTipoMercado()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 11);
            //se llena tipo de nodo comercial en PDV
            CmbTipMerc.DataSource = ds;
            CmbTipMerc.DataTextField = "NodeComType_name";
            CmbTipMerc.DataValueField = "idNodeComType";
            CmbTipMerc.DataBind();
            ds = null;
        }
        private void comboNodos()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_COMBONODOS", CmbTipMerc.SelectedValue);
            //se llena nodos en PDV
            cmbNodoCom.DataSource = ds;
            cmbNodoCom.DataTextField = "commercialNodeName";
            cmbNodoCom.DataValueField = "NodeCommercial";
            cmbNodoCom.DataBind();
            ds = null;
        }
        private void combosegmenpdv()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 50);
            //se llena segmentos en PDV
            CmbSelSegPDV.DataSource = ds;
            CmbSelSegPDV.DataTextField = "Segment_Name";
            CmbSelSegPDV.DataValueField = "id_Segment";
            CmbSelSegPDV.DataBind();
            ds = null;
        }
        private void comboCiudadesBuscaPDV()
        {
            DataSet ds1 = new DataSet();
            ds1 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 36);
            //se llena ciudades en buscar PDV

            cmbbProvpdv.DataSource = ds1;
            cmbbProvpdv.DataTextField = "Name_City";
            cmbbProvpdv.DataValueField = "cod_City";
            cmbbProvpdv.DataBind();
            ds1 = null;
        }
        private void activarControlesPDVCli()
        {
            cmbClientePDVC.Enabled = true;
            CmbPaísPDVC.Enabled = true;
            CmbCanalPDVC.Enabled = true;
            cmbTACPDVC.Enabled = true;
            cmbAgruCPDVC.Enabled = true;
            GvPDV.Enabled = true;
            //RBTPDVC.Enabled = false;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = true;
            TabDistribuidora.Enabled = false;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void desactivarControlesPDVCli()
        {
            cmbClientePDVC.Enabled = false;
            CmbPaísPDVC.Enabled = false;
            CmbCanalPDVC.Enabled = false;
            cmbTACPDVC.Enabled = false;
            cmbAgruCPDVC.Enabled = false;
            GvPDV.Enabled = false;
            //RBTPDVC.Enabled = false;
            TabPanelSegmentos.Enabled = true;
            TabPanelTipoAgrupación.Enabled = true;
            TabPanelAgrupaciónComercial.Enabled = true;
            Panel_Mallas.Enabled = true;
            Panel_Sector.Enabled = true;
            TabPanelPDV.Enabled = true;
            PanelPDVCliente.Enabled = true;
            TabDistribuidora.Enabled = true;
            Panel_ChannelXACommercial.Enabled = true;
        }
        private void crearActivarbotonesPDVCli()
        {
            BtnCrearPDVC.Visible = false;
            BtnGuardarPDVC.Visible = false;
            BtnConsultarPDVC.Visible = false;
            BtnEditarPDVC.Visible = false;
            BtnActualizarPDVC.Visible = false;
            BtnCancelarPDVC.Visible = true;
            //BtnAnt1PDVC.Visible = false;
            //BtnAntPDVC.Visible = false;
            //BtnSigPDVC.Visible = false;
            //BtnSig1PDVC.Visible = false;
        }
        private void SavelimpiarcontrolesPDVCli()
        {
            cmbClientePDVC.Text = "0";
            CmbPaísPDVC.Text = "0";
            CmbCanalPDVC.Text = "0";
            cmbTACPDVC.Text = "0";
            cmbAgruCPDVC.Text = "0";
            GvPDV.DataBind();
            //RBTPDVC.Items[0].Selected = true;
            //RBTPDVC.Items[1].Selected = false;
            cmbBClientePDVC.Text = "0";
            cmbBPaísPDVC.Text = "0";
            cmbBCanalPDVC.Text = "0";
            cmbBTipoAgrupacion.Text = "0";
            cmbBAgrupacionC.Text = "0";
        }
        private void saveActivarbotonesPDVCli()
        {
            BtnCrearPDVC.Visible = true;
            BtnGuardarPDVC.Visible = false;
            BtnConsultarPDVC.Visible = true;
            BtnEditarPDVC.Visible = false;
            BtnActualizarPDVC.Visible = false;
            BtnCancelarPDVC.Visible = true;
            //BtnAnt1PDVC.Visible = false;
            //BtnAntPDVC.Visible = false;
            //BtnSigPDVC.Visible = false;
            //BtnSig1PDVC.Visible = false;
        }
        private void EditarActivarbotonesPDVCli()
        {
            BtnCrearPDVC.Visible = false;
            BtnGuardarPDVC.Visible = false;
            BtnConsultarPDVC.Visible = true;
            BtnEditarPDVC.Visible = false;
            BtnActualizarPDVC.Visible = true;
            BtnCancelarPDVC.Visible = true;
            //BtnAnt1PDVC.Visible = false;
            //BtnAntPDVC.Visible = false;
            //BtnSigPDVC.Visible = false;
            //BtnSig1PDVC.Visible = false;
        }
        private void EditarActivarControlesPDVCli()
        {
            cmbClientePDVC.Enabled = false;
            CmbPaísPDVC.Enabled = false;
            CmbCanalPDVC.Enabled = false;
            cmbTACPDVC.Enabled = false;
            cmbAgruCPDVC.Enabled =false;
            //RBTPDVC.Enabled = true;
            GvPDV.Enabled = true;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = true;
            TabDistribuidora.Enabled = false;
            Panel_ChannelXACommercial.Enabled = false;
        }
        private void BuscarActivarbotnesPDVCli()
        {
            BtnCrearPDVC.Visible = false;
            BtnGuardarPDVC.Visible = false;
            BtnConsultarPDVC.Visible = true;
            BtnEditarPDVC.Visible = false;
            BtnActualizarPDVC.Visible = false;
            BtnCancelarPDVC.Visible = true;
        }      
        private void LLenaComboPaisPDVC()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPDVCliente(0, "0", "0", 0, "0");
            //se llena cliente en producto Ancla
            CmbPaísPDVC.DataSource = ds.Tables[0];
            CmbPaísPDVC.DataTextField = "name_country";
            CmbPaísPDVC.DataValueField = "pdv_codCountry";
            CmbPaísPDVC.DataBind();
            CmbPaísPDVC.Items.Insert(0, new ListItem("<Seleccione..>", "0"));    
            ds = null;
        }
        private void LLenaComboBuscarPaisPDVC()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPDVCliente(0, "0", "0", 0, "0");
            cmbBPaísPDVC.DataSource = ds.Tables[0];
            cmbBPaísPDVC.DataTextField = "name_country";
            cmbBPaísPDVC.DataValueField = "pdv_codCountry";
            cmbBPaísPDVC.DataBind();
            cmbBPaísPDVC.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void comboclienteenPDVCli()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPDVCliente(0, CmbPaísPDVC.SelectedValue, "0", 0, "0");
            //se llena cliente en PDVCliente
            cmbClientePDVC.DataSource = ds.Tables[1];
            cmbClientePDVC.DataTextField = "Company_Name";
            cmbClientePDVC.DataValueField = "Company_id";
            cmbClientePDVC.DataBind();
            cmbClientePDVC.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
        }
        private void comboBuscarclienteenPDVCli()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPDVCliente(0, cmbBPaísPDVC.SelectedValue, "0", 0, "0");
            //se llena cliente en PDVCliente
            cmbBClientePDVC.DataSource = ds.Tables[1];
            cmbBClientePDVC.DataTextField = "Company_Name";
            cmbBClientePDVC.DataValueField = "Company_id";
            cmbBClientePDVC.DataBind();
            cmbBClientePDVC.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
        } 
        private void LLenaComboCanalxPaisPDVC()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPDVCliente(Convert.ToInt32( cmbClientePDVC.SelectedValue), CmbPaísPDVC.SelectedValue, "0", 0, "0");          
            CmbCanalPDVC.DataSource = ds.Tables[2];
            CmbCanalPDVC.DataTextField = "Channel_Name";
            CmbCanalPDVC.DataValueField = "pdv_codChannel";
            CmbCanalPDVC.DataBind();
            CmbCanalPDVC.Items.Insert(0, new ListItem("<Seleccione..>", "0"));          
            ds = null;          
        }
        private void LLenaComboBuscarCanalxPaisPDVC()
        {          
            DataSet ds1 = null;
            ds1 = owsadministrativo.llenaCombosPDVCliente(Convert.ToInt32(cmbBClientePDVC.SelectedValue), cmbBPaísPDVC.SelectedValue, "0", 0, "0");      
            cmbBCanalPDVC.DataSource = ds1.Tables[2];
            cmbBCanalPDVC.DataTextField = "Channel_Name";
            cmbBCanalPDVC.DataValueField = "pdv_codChannel";
            cmbBCanalPDVC.DataBind();
            cmbBCanalPDVC.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            ds1 = null;
        }
        private void LLenaComboTACxCanalPDVC()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPDVCliente(0,CmbPaísPDVC.SelectedValue, CmbCanalPDVC.SelectedValue, 0, "0");            
            //se llena Canal  en Punto de Venta A clinete
            cmbTACPDVC.DataSource = ds.Tables[3];
            cmbTACPDVC.DataTextField = "NodeComType_name";
            cmbTACPDVC.DataValueField = "idNodeComType";
            cmbTACPDVC.DataBind();
            cmbTACPDVC.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void LLenaComboBuscarTACxCanalPDVC()
        {      
            DataSet ds1 = null;
            ds1 = owsadministrativo.llenaCombosPDVCliente(0,cmbBPaísPDVC.SelectedValue, cmbBCanalPDVC.SelectedValue, 0, "0");
            cmbBTipoAgrupacion.DataSource = ds1.Tables[3];
            cmbBTipoAgrupacion.DataTextField = "NodeComType_name";
            cmbBTipoAgrupacion.DataValueField = "idNodeComType";
            cmbBTipoAgrupacion.DataBind();
            cmbBTipoAgrupacion.Items.Insert(0, new ListItem("<Seleccione..>", "0"));          
            ds1 = null;
        }
        private void LLenaComboACxTACPDVC()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPDVCliente(0, CmbPaísPDVC.SelectedValue, CmbCanalPDVC.SelectedValue, Convert.ToInt32(cmbTACPDVC.SelectedValue), "0");
            //se llena Canal  en Punto de Venta A clinete
            cmbAgruCPDVC.DataSource = ds.Tables[4];
            cmbAgruCPDVC.DataTextField = "commercialNodeName";
            cmbAgruCPDVC.DataValueField = "NodeCommercial";
            cmbAgruCPDVC.DataBind();
            cmbAgruCPDVC.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;            
        }
        private void LLenaComboBuscarACxTACPDVC()
        {           
            DataSet ds1 = null;
            ds1 = owsadministrativo.llenaCombosPDVCliente(0, cmbBPaísPDVC.SelectedValue, cmbBCanalPDVC.SelectedValue, Convert.ToInt32(cmbBTipoAgrupacion.SelectedValue), "0");
            cmbBAgrupacionC.DataSource = ds1.Tables[4];
            cmbBAgrupacionC.DataTextField = "commercialNodeName";
            cmbBAgrupacionC.DataValueField = "NodeCommercial";
            cmbBAgrupacionC.DataBind();
            cmbBAgrupacionC.Items.Insert(0, new ListItem("<Seleccione...>", "0"));            
            ds1 = null;
        }
        private void LLenaComboPDVxACPDVC()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPDVCliente(0, CmbPaísPDVC.SelectedValue, CmbCanalPDVC.SelectedValue, Convert.ToInt32(cmbTACPDVC.SelectedValue), cmbAgruCPDVC.SelectedValue);
            if (ds != null)
            {
                if (ds.Tables[5].Rows.Count > 0)
                {
                    //se llena Canal  en Punto de Venta A clinete
                    GvPDV.DataSource = ds.Tables[5];
                    GvPDV.DataBind();
                }
            
                else
                {
                    GvPDV.DataBind();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Esta Agrupación comercial no tiene PDV disponibles para relacionar";
                    MensajeAlerta();
                }
            }
            ds = null;
        }
        private void LLenaPDVxConsulta()
        {
            try
            {
                DataTable dt = oPDV.ConsultarPDVClientGrilla(Convert.ToInt32(cmbClientePDVC.SelectedValue), CmbPaísPDVC.SelectedValue, CmbCanalPDVC.SelectedValue, Convert.ToInt32(cmbTACPDVC.SelectedValue), cmbAgruCPDVC.SelectedValue);
                //DataSet ds = null;
                //ds = owsadministrativo.llenaPDVClienteConsulta(0, cmbBPaísPDVC.SelectedValue, cmbBCanalPDVC.SelectedValue, Convert.ToInt32(cmbBTipoAgrupacion.SelectedValue), cmbBAgrupacionC.SelectedValue);
                //se llena Canal  en Punto de Venta A clinete                
                GVPDVConsulta.DataSource = dt;
                GVPDVConsulta.DataBind();               
                dt = null;
            }
            catch (Exception ex)
            {             
            }       
        }
        private void LlenacomboConsultaMallasenPDVC()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosSector(Convert.ToInt32(cmbClientePDVC.SelectedValue));
            //se llena mallas PDVC

            if (ds.Tables[1].Rows.Count > 0)
            {
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).DataSource = ds.Tables[1];
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).DataTextField = "malla";
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).DataValueField = "id_malla";
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).DataBind();
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).Items.Clear();
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).Items.Insert(0, new ListItem("No aplica", "No aplica"));
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
        }
        private void LlenacomboConsultaTypeMode()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 28);
            DropDownList ddlType = GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[2].FindControl("cmb_Node_Type") as DropDownList;
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlType.DataSource = ds;
                ddlType.DataTextField = "NodeComType_name";
                ddlType.DataValueField = "idNodeComType";
                ddlType.DataBind();
            }
        }        
        private void LlenacomboConsultaCorpo()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_OBTENER_CORPORACION");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).DataSource = ds;
                ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).DataTextField = "corp_name";
                ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).DataValueField = "corp_id";
                ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).DataBind();                
            }
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).Items.Insert(0, new ListItem("<Seleccione...>", "0"));
        }
        private void LlenacomboConsultaOficinaenPDVC()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosSector(Convert.ToInt32(cmbClientePDVC.SelectedValue));
            //se llena mallas PDVC          

            if (ds.Tables[3].Rows.Count > 0)
            {
                DropDownList ddloficina = GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[3].FindControl("cmbOficinaconsultaPDVC") as DropDownList;
                ddloficina.DataSource = ds.Tables[3];
                ddloficina.DataTextField = "Name_Oficina";
                ddloficina.DataValueField = "cod_Oficina";
                ddloficina.DataBind();
                ddloficina.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {
                DropDownList ddloficina = GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[3].FindControl("cmbOficinaconsultaPDVC") as DropDownList;
                ddloficina.Items.Clear();
                ddloficina.Items.Insert(0, new ListItem("No aplica", "No aplica"));
                ddloficina.Items.Insert(0, new ListItem("<Seleccione..>", "0"));              
            }
        }
        private void LlenacomboMallasenPDVC()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosSector(Convert.ToInt32(cmbClientePDVC.SelectedValue));
            //se llena mallas PDVC
           
                if (ds.Tables[1].Rows.Count > 0)
                {
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[4].FindControl("cmbMallaPDVC")).DataSource = ds.Tables[1];
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[4].FindControl("cmbMallaPDVC")).DataTextField = "malla";
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[4].FindControl("cmbMallaPDVC")).DataValueField = "id_malla";
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[4].FindControl("cmbMallaPDVC")).DataBind();
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[4].FindControl("cmbMallaPDVC")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                }
                else
                {
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[4].FindControl("cmbMallaPDVC")).Items.Clear();
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[4].FindControl("cmbMallaPDVC")).Items.Insert(0, new ListItem("No aplica", "No aplica"));
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[4].FindControl("cmbMallaPDVC")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                }
        }
        private void LlenacomboOficinaenPDVC()
        {
            DataSet ds = new DataSet();
            ds = owsadministrativo.llenaCombosSector(Convert.ToInt32(cmbClientePDVC.SelectedValue));
            //se llena mallas PDVC          
           
                if (ds.Tables[3].Rows.Count > 0)
                {
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[3].FindControl("cmbOficinaPDVC")).DataSource = ds.Tables[3];
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[3].FindControl("cmbOficinaPDVC")).DataTextField = "Name_Oficina";
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[3].FindControl("cmbOficinaPDVC")).DataValueField = "cod_Oficina";
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[3].FindControl("cmbOficinaPDVC")).DataBind();
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[3].FindControl("cmbOficinaPDVC")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                }
                else
                {
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[3].FindControl("cmbOficinaPDVC")).Items.Clear();
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[3].FindControl("cmbOficinaPDVC")).Items.Insert(0, new ListItem("No aplica", "No aplica"));
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[3].FindControl("cmbOficinaPDVC")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                }
        }
        private void LlenaEstadoOficinaenPDVC()
        {
                string Oficina;
                Oficina = ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[3].FindControl("cmbOficinaPDVC")).Text;
                if (Oficina != "0")
                {
                    ((CheckBox)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("CheckBox1")).Checked = true;
                }
                else
                {
                    ((CheckBox)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("CheckBox1")).Checked = false;
                }
        }
        private void LlenaEstadoConsultaOficinaenPDVC()
        {

            string Oficina;
            Oficina = ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[3].FindControl("cmbOficinaconsultaPDVC")).Text;
            if (Oficina != "0")
            {
                ((CheckBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("CheckBox1")).Checked = true;
            }
            else
            {
                ((CheckBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("CheckBox1")).Checked = false;
            }

        }
        private void LlenacomboSectorxMallaPDVC()
        {          
            DataTable dt = new DataTable();
            if (((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[4].FindControl("cmbMallaPDVC")).Text != "No aplica")
                {     
                dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOSECTORPORMALLAPDVC", ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[4].FindControl("cmbMallaPDVC")).Text);
                         ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).DataSource = dt;
                        ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).DataTextField = "Sector";
                        ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).DataValueField = "id_sector";
                        ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).DataBind();
                        ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                   }
            else
            {
                ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Items.Clear();
                ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Items.Insert(0, new ListItem("No aplica", "No aplica"));
                ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }                    
                
               dt = null;
            
        }
        private void LlenacomboConsultaSectorxMallaPDVC()
        {
            DataTable dt = new DataTable();
            if (((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).Text != "No aplica")
            {
                dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOSECTORPORMALLAPDVC", ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).Text);
                
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).DataSource = dt;
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).DataTextField = "Sector";
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).DataValueField = "id_sector";
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).DataBind();
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {
                ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Items.Clear();
                ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Items.Insert(0, new ListItem("No aplica", "No aplica"));
                ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            dt = null;
        }

        private void comboDistribuidora()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEB_LLENACOMBOS", 62);
            //se llena canales en PDV

                                 
           
                if (dt.Rows.Count > 0)
                {
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("cmbDEX")).DataSource = dt;
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("cmbDEX")).DataTextField = "Dex_Name";
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("cmbDEX")).DataValueField = "Id_Dex";
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("cmbDEX")).DataBind();
                    //((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("cmbDEX")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                }
                else
                {
                //    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("cmbDEX")).Items.Clear();
                //    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("cmbDEX")).Items.Insert(0, new ListItem("No aplica", "No aplica"));
                //    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("cmbDEX")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                }
           
           
        }
        private void comboConsultaDistribuidora()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEB_LLENACOMBOS", 62);
            //se llena canales en PDV

            if (dt.Rows.Count > 0)
            {
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("cmbDEX")).DataSource = dt;
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("cmbDEX")).DataTextField = "Dex_Name";
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("cmbDEX")).DataValueField = "Id_Dex";
                ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("cmbDEX")).DataBind();
                //((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("cmbDEX")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {
                //((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("cmbDEX")).Items.Clear();
                //((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("cmbDEX")).Items.Insert(0, new ListItem("No aplica", "No aplica"));
                ////((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("cmbDEX")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
        }

        private void MensajeAlerta()
        {

            ModalPopupAlertas.Show();
            BtnAceptarAlert.Focus();
            //ScriptManager.RegisterStartupScript(
            //this, this.GetType(), "myscript", "alert('Debe ingresar todos los parametros con (*)');", true);
        }

        private void cargarddlgpdvcliente() 
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_CLIENTES_EXTERNOS");
            //se llena clientes en GPDV

            if (dt.Rows.Count > 0)
            {
                ddlgpdvcliente.DataSource = dt;
                ddlgpdvcliente.DataTextField = "Company_Name"; 
                ddlgpdvcliente.DataValueField = "Company_id";
                ddlgpdvcliente.DataBind();
                ddlgpdvcliente.Items.Insert(0, new ListItem("<Selecccione ...>", "0"));
            }
            else
            {
            }
        }

        private void cargarCorporacion()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_CORPORACION");
            if (dt.Rows.Count > 0)
            {
                cmbCorporacion.DataSource = dt;
                cmbCorporacion.DataTextField = "corp_name";
                cmbCorporacion.DataValueField = "corp_id";
                cmbCorporacion.DataBind();
                cmbCorporacion.Items.Insert(0, new ListItem("<Selecccione ...>", "0"));
            }
            foreach (ListItem li in cmbCorporacion.Items)
            {
                ddl_corporacion.Items.Add(li);
            } 
        }


        #endregion
        
        #region Eventos Segmentos
        protected void BtnCrearSegmento_Click(object sender, EventArgs e)
        {
            crearActivarbotonesSegmentos();
            activarControlesSegmentos();
            RbtnEstadoSegmento.Enabled = false;
        }
        protected void BtnAgrSegmento_Click(object sender, EventArgs e)
        {
            if (TxtSegmento.Text == "")
            {
                LblAlert.Text = "Sr. Usuario";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Primero ingrese la información para ser adicionada ";
                MensajeAlerta();
                ModalPopupAlertas.Show();
                BtnAceptarAlert.Focus();
                return;
            }
            else
            {
                ChkSegmento.Items.Add(TxtSegmento.Text.ToLowerInvariant());
                ChkSegmento.Items[ChkSegmento.Items.Count - 1].Selected = true;
                TxtSegmento.Text = "";
                TxtSegmento.Focus();
            }

        }
        protected void BtnsaveSegmento_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            if (ChkSegmento.Items.Count == 0)
            {

                LblAlert.Text = "Sr. Usuario";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar al menos un segmento para el tipo : " + TxtTipoSegmento.Text;
                MensajeAlerta();
                ModalPopupAlertas.Show();
                BtnAceptarAlert.Focus();
                return;
            }
            else
            {
                for (int i = 0; i <= ChkSegmento.Items.Count - 1; i++)
                {
                    if (ChkSegmento.Items[i].Selected == false)
                    {
                        LblAlert.Text = "Sr. Usuario";
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El check debe estar habilitado para el segmento : " + ChkSegmento.Items[i].Text + " Por favor habilítelo";
                        MensajeAlerta();
                        ModalPopupAlertas.Show();
                        BtnAceptarAlert.Focus();
                        return;
                    }
                }
            }

            TxtTipoSegmento.Text = TxtTipoSegmento.Text.TrimStart();
            TxtDescSegmento.Text = TxtDescSegmento.Text.TrimStart();
            if (TxtTipoSegmento.Text == "" || TxtDescSegmento.Text == "")
            {
                if (TxtTipoSegmento.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo de segmento";
                }
                if (TxtDescSegmento.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción del tipo de segmento";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                return;
            }
            try
            {
                estado = true;
                DAplicacion odconsulSegmento = new DAplicacion();
                //DataTable dtconsulta = odconsulSegmento.ConsultaDuplicados(ConfigurationManager.AppSettings["Segments_Type"], TxtTipoSegmento.Text, null, null);
                DataTable dtconsulta = odconsulSegmento.ConsultaDuplicados("Segments_Type", TxtTipoSegmento.Text, null, null);
                if (dtconsulta == null)
                {
                    ESegments_Type oeSegmentsType = oSegmentstype.RegistrarSegmentsType(TxtTipoSegmento.Text, TxtDescSegmento.Text, estado,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    //dt = oConn.ejecutarDataTable("UP_WEBSIGE_ADMIN_OBTENER_IDSEGMENTSTYPE", TxtTipoSegmento.Text, Convert.ToString(this.Session["sUser"]));
                    DataTable dtidSegmentsType = obtenerid.Get_ObteneridSegmentsType(TxtTipoSegmento.Text, Convert.ToString(this.Session["sUser"]));

                    iid_segmentsType = Convert.ToInt32(dtidSegmentsType.Rows[0]["id_SegmentsType"].ToString().Trim());
                    for (int i = 0; i <= ChkSegmento.Items.Count - 1; i++)
                    {
                        DataTable dtconsul = odconsulSegmento.ConsultaDuplicados(ConfigurationManager.AppSettings["Segments"], ChkSegmento.Items[i].Text, iid_segmentsType.ToString(), null);
                        //DataTable dtconsul = odconsulSegmento.ConsultaDuplicados("Segments", ChkSegmento.Items[i].Text, iid_segmentsType.ToString(), null);
                        if (dtconsul == null)
                        {
                            ESegments oeSegments = oSegments.RegistrarSegments(ChkSegmento.Items[i].Text, iid_segmentsType, ChkSegmento.Items[i].Selected,
                                Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        }
                        else
                        {
                            sSegment = sSegment + " " + ChkSegmento.Items[i].Text;
                            this.Session["sSegment"] = sSegment;
                            this.Session["Nota"] = "NOTA IMPORTANTE !! : Los Segmentos: " + this.Session["sSegment"] + " aparecían más de una vez en la lista . Solo se almacenó un registro por c/u de ellos para evitar duplicidad";
                        }
                    }
                    string sSegmentsType = "";
                    sSegmentsType = TxtTipoSegmento.Text;
                    this.Session["sSegmentsType"] = sSegmentsType;
                    SavelimpiarcontrolesSegmentos();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El tipo de segmento " + this.Session["sSegmentsType"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesSegmentos();
                }
                else
                {
                    string sSegmentType = "";
                    sSegmentType = "El tipo de segmento : " + TxtTipoSegmento.Text;
                    this.Session["sSegmentType"] = sSegmentType;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El país " + this.Session["sSegmentType"] + " Ya Existe";
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
        protected void btnBuscarSegmento_Click(object sender, EventArgs e)
        {
            desactivarControlesSegmentos();
            LblFaltantes.Text = "";
            TxtBTipoSegmento.Text = TxtBTipoSegmento.Text.TrimStart();

            if (TxtBTipoSegmento.Text == "")
            {
                this.Session["mensajealert"] = "Nombre de tipo de segmento";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                SavelimpiarcontrolesSegmentos();
                IbtnSegmento_ModalPopupExtender.Show();
                return;
            }
            sSegmentsType = TxtBTipoSegmento.Text;

            DataTable oeSegmentsType = oSegmentstype.BuscarSegmentsType(sSegmentsType);

            BuscarActivarbotnesSegmentos();
            TxtBTipoSegmento.Text = "";
            ChkSegmento.Items.Clear();

            if (oeSegmentsType != null)
            {
                if (oeSegmentsType.Rows.Count > 0)
                {
                    TxtCodSegmento.Text = oeSegmentsType.Rows[0]["id_SegmentsType"].ToString().Trim();
                    TxtTipoSegmento.Text = oeSegmentsType.Rows[0]["Segment_Type"].ToString().Trim();
                    TxtDescSegmento.Text = oeSegmentsType.Rows[0]["Segment_Description"].ToString().Trim();
                    estado = Convert.ToBoolean(oeSegmentsType.Rows[0]["SegmentType_Status"].ToString().Trim());

                    if (estado == true)
                    {
                        RbtnEstadoSegmento.Items[0].Selected = true;
                        RbtnEstadoSegmento.Items[1].Selected = false;
                    }
                    else
                    {
                        RbtnEstadoSegmento.Items[0].Selected = false;
                        RbtnEstadoSegmento.Items[1].Selected = true;
                    }
                    this.Session["tSegmentsType"] = oeSegmentsType;
                    this.Session["i"] = 0;

                    if (oeSegmentsType.Rows.Count == 1)
                    {
                        PregSegmento.Visible = false;
                        AregSegmento.Visible = false;
                        SregSegmento.Visible = false;
                        UregSegmento.Visible = false;
                    }
                    else
                    {
                        PregSegmento.Visible = true;
                        AregSegmento.Visible = true;
                        SregSegmento.Visible = true;
                        UregSegmento.Visible = true;
                    }

                    DataTable oeSegments = oSegments.BuscarSegments(Convert.ToInt32(TxtCodSegmento.Text));
                    if (oeSegments != null)
                    {
                        if (oeSegments.Rows.Count > 0)
                        {
                            for (int i = 0; i <= oeSegments.Rows.Count - 1; i++)
                            {
                                ChkSegmento.Items.Add(oeSegments.Rows[i]["Segment_Name"].ToString().Trim());
                                ChkSegmento.Items[i].Selected = Convert.ToBoolean(oeSegments.Rows[i]["Segment_Status"].ToString().Trim());
                            }
                        }
                    }
                    this.Session["tSegments"] = oeSegments;
                }
                else
                {
                    SavelimpiarcontrolesSegmentos();
                    //llenarcombos();
                    saveActivarbotonesSegmentos();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnSegmento_ModalPopupExtender.Show();
                }
            }
        }
        protected void BtneditSegmento_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesSegmentos();
            EditarActivarControlesSegmentos();
            activarControlesSegmentos();
            this.Session["rept"] = TxtTipoSegmento.Text;
        }
        protected void BtnActualizaSegmento_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtTipoSegmento.Text = TxtTipoSegmento.Text.TrimStart();
            TxtDescSegmento.Text = TxtDescSegmento.Text.TrimStart();
            if (TxtTipoSegmento.Text == "" || TxtDescSegmento.Text == "")
            {
                if (TxtTipoSegmento.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo de segmento";
                }
                if (TxtDescSegmento.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción del tipo de segmento";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                return;
            }
            try
            {
                if (RbtnEstadoSegmento.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabSegmentsType = new DAplicacion();
                    DataTable dt = oddeshabSegmentsType.PermitirDeshabilitar(ConfigurationManager.AppSettings["Segments_TypeSegments"], TxtCodSegmento.Text);
                    DataTable dt1 = oddeshabSegmentsType.PermitirDeshabilitar(ConfigurationManager.AppSettings["Segments_TypePointOfSale"], TxtCodSegmento.Text);
                    if (dt != null || dt1 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }
                }
                repetido = Convert.ToString(this.Session["rept"]);
                if (repetido != TxtTipoSegmento.Text)
                {
                    DAplicacion odconsulSegmento = new DAplicacion();
                    DataTable dtconsulta = odconsulSegmento.ConsultaDuplicados("Segments_Type", TxtTipoSegmento.Text, null, null);

                    if (dtconsulta == null)
                    {
                        ESegments_Type oeSegmentsType = oSegmentstype.Actualizar_SegmentsType(Convert.ToInt32(TxtCodSegmento.Text), TxtTipoSegmento.Text, TxtDescSegmento.Text, estado,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        sSegmentsType = TxtTipoSegmento.Text;
                        this.Session["sSegmentsType"] = sSegmentsType;
                        for (int i = 0; i <= ChkSegmento.Items.Count - 1; i++)
                        {
                            DataTable dtActualizar = oSegments.BuscarSegmentsActual(ChkSegmento.Items[i].Text, Convert.ToInt32(TxtCodSegmento.Text));
                            if (dtActualizar != null)
                            {
                                if (dtActualizar.Rows.Count > 0)
                                {
                                    ESegments oeaSegments = oSegments.Actualizar_Segments(Convert.ToInt32(dtActualizar.Rows[0]["id_Segment"].ToString().Trim()), Convert.ToInt32(TxtCodSegmento.Text), ChkSegmento.Items[i].Selected, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                }

                                else
                                {
                                    DataTable dtconsul = odconsulSegmento.ConsultaDuplicados(ConfigurationManager.AppSettings["Segments"], ChkSegmento.Items[i].Text, iid_segmentsType.ToString(), null);
                                    if (dtconsul == null)
                                    {
                                        if (ChkSegmento.Items[i].Selected == false)
                                        {
                                            Alertas.CssClass = "MensajesError";
                                            LblFaltantes.Text = "El check debe estar habilitado para el segmento : " + ChkSegmento.Items[i].Text + " Por favor habilítelo";
                                            MensajeAlerta();
                                            ModalPopupAlertas.Show();
                                            BtnAceptarAlert.Focus();
                                            return;
                                        }
                                        else
                                        {
                                            ESegments oeSegments = oSegments.RegistrarSegments(ChkSegmento.Items[i].Text, Convert.ToInt32(TxtCodSegmento.Text), ChkSegmento.Items[i].Selected,
                                                Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                        }
                                    }
                                    else
                                    {
                                        sSegment = sSegment + " " + ChkSegmento.Items[i].Text;
                                        this.Session["sSegment"] = sSegment;
                                        this.Session["Nota"] = "NOTA IMPORTANTE !! : Los Segmentos: " + this.Session["sSegment"] + " aparecían más de una vez en la lista y/o ya estaban creados . Solo se almacenó un registro por c/u de ellos para evitar duplicidad";
                                    }
                                }
                            }
                        }
                        SavelimpiarcontrolesSegmentos();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El tipo de segmento " + this.Session["sSegmentsType"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesSegmentos();
                    }
                    else
                    {
                        string sSegmentType = "";
                        sSegmentType = "El tipo de segmento : " + TxtTipoSegmento.Text;
                        this.Session["sSegmentType"] = sSegmentType;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Tipo de Segmento : " + this.Session["sSegmentType"] + " No se puede Actualizar este registro ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    ESegments_Type oeSegmentsType = oSegmentstype.Actualizar_SegmentsType(Convert.ToInt32(TxtCodSegmento.Text), TxtTipoSegmento.Text, TxtDescSegmento.Text, estado,
                           Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    sSegmentsType = TxtTipoSegmento.Text;
                    this.Session["sSegmentsType"] = sSegmentsType;
                    for (int i = 0; i <= ChkSegmento.Items.Count - 1; i++)
                    {
                        DataTable dtActualizar = oSegments.BuscarSegmentsActual(ChkSegmento.Items[i].Text, Convert.ToInt32(TxtCodSegmento.Text));
                        if (dtActualizar != null)
                        {
                            if (dtActualizar.Rows.Count > 0)
                            {
                                ESegments oeaSegments = oSegments.Actualizar_Segments(Convert.ToInt32(dtActualizar.Rows[0]["id_Segment"].ToString().Trim()), Convert.ToInt32(TxtCodSegmento.Text), ChkSegmento.Items[i].Selected, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            }
                            else
                            {
                                DAplicacion odconsulSegmento = new DAplicacion();
                                DataTable dtconsul = odconsulSegmento.ConsultaDuplicados(ConfigurationManager.AppSettings["Segments"], ChkSegmento.Items[i].Text, iid_segmentsType.ToString(), null);
                                if (dtconsul == null)
                                {
                                    if (ChkSegmento.Items[i].Selected == false)
                                    {
                                        Alertas.CssClass = "MensajesError";
                                        LblFaltantes.Text = "El check debe estar habilitado para el segmento : " + ChkSegmento.Items[i].Text + " Por favor habilítelo";
                                        MensajeAlerta();
                                        ModalPopupAlertas.Show();
                                        BtnAceptarAlert.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        ESegments oeSegments = oSegments.RegistrarSegments(ChkSegmento.Items[i].Text, Convert.ToInt32(TxtCodSegmento.Text), ChkSegmento.Items[i].Selected,
                                            Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                    }
                                }
                                else
                                {
                                    sSegment = sSegment + " " + ChkSegmento.Items[i].Text;
                                    this.Session["sSegment"] = sSegment;
                                    this.Session["Nota"] = "NOTA IMPORTANTE !! : Los Segmentos: " + this.Session["sSegment"] + " aparecían más de una vez en la lista y/o ya estaban creados . Solo se almacenó un registro por c/u de ellos para evitar duplicidad";
                                }
                            }
                        }
                    }
                    SavelimpiarcontrolesSegmentos();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El tipo de segmento " + this.Session["sSegmentsType"] + " Se Actualizo con Exito";
                    MensajeAlerta();
                    saveActivarbotonesSegmentos();
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
        protected void BtncancelSegmento_Click(object sender, EventArgs e)
        {
            SavelimpiarcontrolesSegmentos();
            saveActivarbotonesSegmentos();
            desactivarControlesSegmentos();
        }
        private void MostrarDatosSegmentos()
        {
            SavelimpiarcontrolesSegmentos();
            recorrido = (DataTable)this.Session["tSegmentsType"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodSegmento.Text = recorrido.Rows[recsearch]["id_SegmentsType"].ToString().Trim();
                    TxtTipoSegmento.Text = recorrido.Rows[recsearch]["Segment_Type"].ToString().Trim();
                    TxtDescSegmento.Text = recorrido.Rows[recsearch]["Segment_Description"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["SegmentType_Status"].ToString().Trim());
                    if (estado == true)
                    {
                        RbtnEstadoSegmento.Items[0].Selected = true;
                        RbtnEstadoSegmento.Items[1].Selected = false;
                    }
                    else
                    {
                        RbtnEstadoSegmento.Items[0].Selected = false;
                        RbtnEstadoSegmento.Items[1].Selected = true;
                    }
                    DataTable oeSegments = oSegments.BuscarSegments(Convert.ToInt32(TxtCodSegmento.Text));
                    if (oeSegments != null)
                    {
                        if (oeSegments.Rows.Count > 0)
                        {
                            for (int i = 0; i <= oeSegments.Rows.Count - 1; i++)
                            {
                                ChkSegmento.Items.Add(oeSegments.Rows[i]["Segment_Name"].ToString().Trim());
                                ChkSegmento.Items[i].Selected = Convert.ToBoolean(oeSegments.Rows[i]["Segment_Status"].ToString().Trim());
                            }
                        }
                    }
                }
            }
        }
        protected void PregSegmento_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tSegmentsType"];
            MostrarDatosSegmentos();

        }
        protected void AregSegmento_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosSegmentos();
            }
        }
        protected void SregSegmento_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tSegmentsType"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosSegmentos();
            }
        }
        protected void UregSegmento_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tSegmentsType"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosSegmentos();
        }
        #endregion

        #region Eventos Tipo Agrupación Comercial

        protected void BtnCrearTipoNodo_Click(object sender, EventArgs e)
        {
            crearActivarbotonesTipoAgrupación();
            activarControlesTipoAgrupación();
        }
        protected void BtnsaveTipoNodo_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            txtNomtiponodo.Text = txtNomtiponodo.Text.TrimStart();
            txtNomtiponodo.Text = txtNomtiponodo.Text.TrimEnd();
            if (txtNomtiponodo.Text == "")
            {
                if (txtNomtiponodo.Text == "")
                {
                    LblFaltantes.Text += ("Nombre tipo de Agrupación comercial" + " . ");
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                return;



            }

            try
            {
                if (RbtnEstadoTiponodo.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }
                DAplicacion odconsultipnodo = new DAplicacion();
                DataTable dtconsulta = odconsultipnodo.ConsultaDuplicados(ConfigurationManager.AppSettings["TipoNodo"], txtNomtiponodo.Text, null, null);
                if (dtconsulta == null)
                {
                    ENodeType oeNodeType = oTypeNode.RegistrarTypeNode(txtNomtiponodo.Text, estado,
                                            Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sNodeType = "";
                    sNodeType = txtNomtiponodo.Text;
                    this.Session["sNodeType"] = sNodeType;
                    SavelimpiarcontrolesTipoAgrupación();
                    combotiponodo();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Tipo de Agrupación Comercial  " + this.Session["sNodeType"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesTipoAgrupación();
                }
                else
                {
                    string sNodeType = "";
                    sNodeType = txtNomtiponodo.Text;
                    this.Session["sNodeType"] = sNodeType;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Tipo de Agrupación Comercial  " + this.Session["sNodeType"] + " Ya Existe";
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
        protected void btnBuscarTipoNodo_Click(object sender, EventArgs e)
        {
            desactivarControlesTipoAgrupación();
            LblFaltantes.Text = "";
            TxtBNomTipoNodo.Text = TxtBNomTipoNodo.Text.TrimStart();
            if (TxtBNomTipoNodo.Text == "")
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnTipoNodo_ModalPopupExtender.Show();
                return;

            }

            BuscarActivarbotnesTipoAgrupación();
            sNodeComType_name = TxtBNomTipoNodo.Text;
            TxtBNomTipoNodo.Text = "";
            DataTable oeTypeNode = oTypeNode.ConsultarTypeNode(sNodeComType_name);

            if (oeTypeNode != null)
            {
                if (oeTypeNode.Rows.Count > 0)
                {
                    for (int i = 0; i <= oeTypeNode.Rows.Count - 1; i++)
                    {
                        TxtCodtipnodo.Text = oeTypeNode.Rows[0]["idNodeComType"].ToString().Trim();
                        txtNomtiponodo.Text = oeTypeNode.Rows[0]["NodeComType_name"].ToString().Trim();
                        estado = Convert.ToBoolean(oeTypeNode.Rows[0]["NodeComType_Status"].ToString().Trim());

                        if (estado == true)
                        {
                            RbtnEstadoTiponodo.Items[0].Selected = true;
                            RbtnEstadoTiponodo.Items[1].Selected = false;
                        }
                        else
                        {
                            RbtnEstadoTiponodo.Items[0].Selected = false;
                            RbtnEstadoTiponodo.Items[1].Selected = true;
                        }
                        this.Session["ttypenode"] = oeTypeNode;
                        this.Session["i"] = 0;

                    }

                    if (oeTypeNode.Rows.Count == 1)
                    {
                        PregTipoNodo.Visible = false;
                        AregTipoNodo.Visible = false;
                        SregTipoNodo.Visible = false;
                        UregTipoNodo.Visible = false;
                    }
                    else
                    {
                        PregTipoNodo.Visible = true;
                        AregTipoNodo.Visible = true;
                        SregTipoNodo.Visible = true;
                        UregTipoNodo.Visible = true;
                    }

                }
                else
                {
                    SavelimpiarcontrolesTipoAgrupación();
                    saveActivarbotonesTipoAgrupación();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnTipoNodo_ModalPopupExtender.Show();
                }
            }

        }
        protected void BtneditTipoNodo_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesTipoAgrupación();
            EditarActivarControlesTipoAgrupación();
            this.Session["rept"] = txtNomtiponodo.Text;

        }
        protected void BtnActualizaTiponNodo_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            txtNomtiponodo.Text = txtNomtiponodo.Text.TrimStart();
            txtNomtiponodo.Text = txtNomtiponodo.Text.TrimEnd();
            if (txtNomtiponodo.Text == "")
            {
                if (txtNomtiponodo.Text == "")
                {
                    LblFaltantes.Text = ". " + "Nombre tipo de Agrupación comercial";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                return;

            }
            try
            {

                if (RbtnEstadoTiponodo.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabtiponodo = new DAplicacion();
                    DataTable dt1 = oddeshabtiponodo.PermitirDeshabilitar(ConfigurationManager.AppSettings["NodeComercial_TypePointOfSale"], TxtCodtipnodo.Text);
                    DataTable dt2 = oddeshabtiponodo.PermitirDeshabilitar(ConfigurationManager.AppSettings["NodeComercial_TypeNodeCommercial"], TxtCodtipnodo.Text);

                    if (dt1 != null || dt2 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }
                }
                repetido = Convert.ToString(this.Session["rept"]);
                if (repetido != txtNomtiponodo.Text)
                {

                    DAplicacion odconsultipnodo = new DAplicacion();
                    DataTable dtconsulta = odconsultipnodo.ConsultaDuplicados(ConfigurationManager.AppSettings["TipoNodo"], txtNomtiponodo.Text, null, null);
                    if (dtconsulta == null)
                    {
                        ENodeType oeaTypeNode = oTypeNode.ActualizarTypeNode(Convert.ToInt32(TxtCodtipnodo.Text), txtNomtiponodo.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                        sNodeType = txtNomtiponodo.Text;
                        this.Session["sNodeType"] = sNodeType;
                        SavelimpiarcontrolesTipoAgrupación();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Tipo de Agrupación Comercial : " + this.Session["sNodeType"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesTipoAgrupación();
                        desactivarControlesTipoAgrupación();
                    }
                    else
                    {
                        sNodeType = txtNomtiponodo.Text;
                        this.Session["sNodeType"] = sNodeType;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Tipo de Agrupación Comercial : " + this.Session["sNodeType"] + " No se puede Actualizar este registro ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    ENodeType oeaTypeNode = oTypeNode.ActualizarTypeNode(Convert.ToInt32(TxtCodtipnodo.Text), txtNomtiponodo.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    sNodeType = txtNomtiponodo.Text;
                    this.Session["sNodeType"] = sNodeType;
                    SavelimpiarcontrolesTipoAgrupación();
                    combotiponodo();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Tipo de Agrupación Comercial : " + this.Session["sNodeType"] + " Se Actualizo con Exito";
                    MensajeAlerta();
                    saveActivarbotonesTipoAgrupación();
                    desactivarControlesTipoAgrupación();

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
        protected void BtncancelTipoNodo_Click(object sender, EventArgs e)
        {
            SavelimpiarcontrolesTipoAgrupación();
            saveActivarbotonesTipoAgrupación();
            desactivarControlesTipoAgrupación();

        }
        private void MostrarDatosTypeNode()
        {
            recorrido = (DataTable)this.Session["ttypenode"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodtipnodo.Text = recorrido.Rows[recsearch]["idNodeComType"].ToString().Trim();
                    txtNomtiponodo.Text = recorrido.Rows[recsearch]["NodeComType_name"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["NodeComType_Status"].ToString().Trim());

                    if (estado == true)
                    {
                        RbtnEstadoTiponodo.Items[0].Selected = true;
                        RbtnEstadoTiponodo.Items[1].Selected = false;
                    }
                    else
                    {
                        RbtnEstadoTiponodo.Items[0].Selected = false;
                        RbtnEstadoTiponodo.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void PregTipoNodo_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["ttypenode"];
            MostrarDatosTypeNode();
        }
        protected void AregTipoNodo_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosTypeNode();
            }
        }
        protected void SregTipoNodo_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["ttypenode"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosTypeNode();
            }
        }
        protected void UregTipoNodo_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["ttypenode"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosTypeNode();
        }
        #endregion

        #region Eventos  Agrupación Comercial
        
        protected void BtnCrearNodo_Click(object sender, EventArgs e)
        {
            crearActivarbotonesAgrupación();
            activarControlesAgrupación();
            llenaPaisAgrupComercial(ddlSelCountry);
        }
        protected void BtnsaveNodo_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            txtNomnodo.Text = txtNomnodo.Text.TrimStart();
            txtNomnodo.Text = txtNomnodo.Text.TrimEnd();
            if (txtNomnodo.Text == "" || CmbSelTipNodo.Text == "0" || ddlSelCountry.Text == "0")
            {
                LblFaltantes.Text = "Ingrese los valores marcados con *:";
                if (txtNomnodo.Text == "")
                {
                    LblFaltantes.Text += ("Agrupación Comercial" + ". ");
                }
                if (CmbSelTipNodo.Text == "0")
                {
                    LblFaltantes.Text += ("Tipo de Agrupación Comercial" + ". ");
                }
                if (ddlSelCountry.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + "País.";
                }
                Alertas.CssClass = "MensajesError";
                MensajeAlerta();
                return;
            }
            if (ddlDpto.Items.Count > 0 && ddlDpto.Text == "0")
            {
                this.Session["mensajealert"] = " Departamento";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Departamento";
                MensajeAlerta();
                return;
            }
            if (ddlProv.Items.Count > 0 && ddlProv.Text == "0")
            {
                this.Session["mensajealert"] = " Ciudad/Provincia";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta fatla Ciudad/Provincia";
                MensajeAlerta();
                return;
            }
            if (ddlDist.Items.Count > 0 && ddlDist.Text == "0")
            {
                this.Session["mensajealert"] = " Distrito";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Distrito";
                MensajeAlerta();
                return;
            }
            if (ddlBarrio.Items.Count > 0 && ddlBarrio.Text == "0")
            {
                this.Session["mensajealert"] = " Barrio";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Barrio";
                MensajeAlerta();
                return;
            }

            try
            {
                DAplicacion odconsulnodo = new DAplicacion();
                DataTable dtconsulta = odconsulnodo.ConsultaDuplicados(ConfigurationManager.AppSettings["Nodo"], txtNomnodo.Text, null, null);
                if (dtconsulta == null)
                {
                    estado = true;
                    /** Inserta un nuevo Nodo Comercial **/
                    ENodeComercial oeNode = new ENodeComercial();
                    NodeComercial dnode = new NodeComercial();
                    oeNode = dnode.RegistrarNodeComercial(txtNomnodo.Text, Convert.ToInt32(CmbSelTipNodo.Text), Convert.ToInt32(cmbCorporacion.Text), txtdireccion.Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now, ddlSelCountry.SelectedValue,ddlDpto.SelectedValue, ddlProv.SelectedValue,ddlDist.SelectedValue, ddlBarrio.SelectedValue);
                    /** Fin inserta un nuevo Nodo Comercial **/

                    /** Inserta el registro en la DB Intermedia **/
                    int st_tmp = 0;
                    st_tmp = dnode.RegistrarNodeComercialTMP(oeNode.NodeCommercial, txtNomnodo.Text, estado);
                    /** Fin insercion en DB Intermedia **/

                    string sNode = "";
                    sNode = txtNomnodo.Text;
                    this.Session["sNode"] = sNode;
                    SavelimpiarcontrolesAgrupación();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Agrupación Comercial " + this.Session["sNode"] + " fue creada con éxito";
                    MensajeAlerta();
                    CancelarAgrupCome();
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La Agrupación Comercial " + txtNomnodo.Text + " ya existe";
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
        private void CancelarAgrupCome()
        {
            saveActivarbotonesAgrupación();
            desactivarControlesAgrupación();
        }
        protected void BtncancelNodo_Click(object sender, EventArgs e)
        {
            saveActivarbotonesAgrupación();
            SavelimpiarcontrolesAgrupación();
            desactivarControlesAgrupación();
        }
        protected void BtnBuscarNodo_Click(object sender, EventArgs e)
        {
            desactivarControlesAgrupación();
            LblFaltantes.Text = "";
            TxtBNomNodo.Text = TxtBNomNodo.Text.TrimStart();
            int idTipoNodo = Convert.ToInt32(ddl_bnc_tipo.Text.ToString().Trim());
            int idCorporacion = Convert.ToInt32(ddl_corporacion.Text.ToString().Trim());

            if (ddl_bnc_tipo.Text == "0" && TxtBNomNodo.Text == "" && ddl_corporacion.Text == "0")
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese al menos un parámetro de consulta";
                MensajeAlerta();
                IbtnNodo_ModalPopupExtender.Show();
                return;
            }

            BuscarActivarbotnesAgrupación();
            sNodeComname = TxtBNomNodo.Text;
            this.Session["iNodeType_id"] = idTipoNodo;
            this.Session["sNodename"] = sNodeComname;
            this.Session["iIdCorporacion"] = idCorporacion;
            TxtBNomNodo.Text = "";
            ddl_bnc_tipo.Text = "0";
            ddl_corporacion.Text = "0";
            DataTable oeNode = dnode.ConsultarNodeComercial(sNodeComname, idTipoNodo, idCorporacion);
            this.Session["tAgrupCome"] = oeNode;

            if (oeNode != null)
            {
                if (oeNode.Rows.Count > 0)
                {
                    gridAgrupComer(oeNode);
                }

                else
                {
                    CancelarAgrupCome();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La consulta realizada no devolvió ninguna respuesta";
                    MensajeAlerta();
                    IbtnNodo_ModalPopupExtender.Show();
                }
            }

            this.Session["Exportar_Excel"] = "Exportar_Agrup_Come";

            DataTable dtAgrupCome = new DataTable();
            dtAgrupCome.Columns.Add("Cod.Agrupación", typeof(String));
            dtAgrupCome.Columns.Add("Nombre", typeof(String));
            dtAgrupCome.Columns.Add("Tipo", typeof(String));
            dtAgrupCome.Columns.Add("Dirección", typeof(String));
            dtAgrupCome.Columns.Add("Corporación", typeof(String));
            dtAgrupCome.Columns.Add("Estado", typeof(String));

            for (int i = 0; i <= GVConsAgrupCom.Rows.Count - 1; i++)
            {
                DataRow dr = dtAgrupCome.NewRow();
                dr["Cod.Agrupación"] = ((Label)GVConsAgrupCom.Rows[i].Cells[0].FindControl("LblCodAgruCom")).Text;
                dr["Nombre"] = ((Label)GVConsAgrupCom.Rows[i].Cells[0].FindControl("LblNomAgrupCom")).Text;
                dr["Tipo"] = ((Label)GVConsAgrupCom.Rows[i].Cells[0].FindControl("LblNodeTypeName")).Text;
                dr["Dirección"] = ((Label)GVConsAgrupCom.Rows[i].Cells[0].FindControl("LblAgrupDirec")).Text;
                dr["Corporación"] = ((Label)GVConsAgrupCom.Rows[i].Cells[0].FindControl("LblCorpName")).Text;
                dr["Estado"] = ((CheckBox)GVConsAgrupCom.Rows[i].Cells[0].FindControl("CheckAgrupCom")).Checked;
                dtAgrupCome.Rows.Add(dr);
            }

            this.Session["CExporAgrup"] = dtAgrupCome;
        }
        protected void btnCancelCategoria_Click(object sender, EventArgs e)
        {
            CancelarAgrupCome();
            SavelimpiarcontrolesAgrupación();
        }
        protected void GVConsAgrupCom_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVConsAgrupCom.PageIndex = e.NewPageIndex;
            GVConsAgrupCom.DataSource = (DataTable)this.Session["tAgrupCome"];
            GVConsAgrupCom.DataBind();
            MopopConsulAgrupCom.Show();
        }
        private void gridAgrupComer(DataTable oeNode)
        {
            GVConsAgrupCom.EditIndex = -1;
            GVConsAgrupCom.DataSource = null;
            GVConsAgrupCom.DataSource = oeNode;
            GVConsAgrupCom.DataBind();
            MopopConsulAgrupCom.Show();
        }
        protected void GVConsAgrupCom_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVConsAgrupCom.EditIndex = -1;
            gridAgrupComer((DataTable)this.Session["tAgrupCome"]);
            btnCanAgrupCom.Visible = true;
            MopopConsulAgrupCom.Show();
        }
        protected void GVConsAgrupCom_RowEditing(object sender, GridViewEditEventArgs e)
        {
            btnCanAgrupCom.Visible = false;
            MopopConsulAgrupCom.Show();
            GVConsAgrupCom.EditIndex = e.NewEditIndex;
            string Codigo, nombre, tipo, direc, corpo, pais, departamento, provincia, distrito, barrio, idpais, iddepartamento, idprovincia, iddistrito;
            bool estado;

            Codigo = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodAgruCom")).Text;
            nombre = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblNomAgrupCom")).Text;
            tipo = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblNodeTypeName")).Text;
            direc = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblAgrupDirec")).Text;
            corpo = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCorpName")).Text;
            estado = ((CheckBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("CheckAgrupCom")).Checked;
            pais = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblPaisNombre")).Text;
            departamento = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblDepartamentoNombre")).Text;
            provincia = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblProvinciaNombre")).Text;
            distrito = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblDistritoNombre")).Text;
            barrio = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblBarrioNombre")).Text;
            idpais = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodPais")).Text;
            iddepartamento = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodDepartamento")).Text;
            idprovincia = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodProvincia")).Text;
            iddistrito = ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodDistrito")).Text;
            

            
            GVConsAgrupCom.DataSource = (DataTable)this.Session["tAgrupCome"];
            GVConsAgrupCom.DataBind();
            if (tipo.Equals(""))
                tipo = "<Seleccione...>";
            if (corpo.Equals(""))
                corpo = "<Seleccione...>";
            ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodAgruCom")).Text = Codigo;
            ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text = nombre;
            LlenacomboConsultaTypeMode();
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[2].FindControl("cmb_Node_Type")).Items.FindByText(tipo).Selected = true;
            ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text = direc;
            LlenacomboConsultaCorpo();
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).Items.FindByText(corpo).Selected = true;
            ((CheckBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[5].FindControl("CheckAgrupCom")).Checked = estado;
            LlenacomboConsultaCorpo();
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).Items.FindByText(corpo).Selected = true;
            //llenos pais
            llenaPaisAgrupComercial(((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[6].FindControl("ddlPais_edit")));
            if(pais!="")
            {
             ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[6].FindControl("ddlPais_edit")).Items.FindByText(pais).Selected = true;
            }

            //lleno departamento
            DataTable dtcountry = new DataTable();
            dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", idpais);

            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[7].FindControl("ddlDepartamento_edit")).DataSource = dtcountry;
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[7].FindControl("ddlDepartamento_edit")).DataTextField = "Name_dpto";
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[7].FindControl("ddlDepartamento_edit")).DataValueField = "cod_dpto";
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[7].FindControl("ddlDepartamento_edit")).DataBind();
            if (departamento != "")
            {
                ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[7].FindControl("ddlDepartamento_edit")).Items.FindByText(departamento).Selected = true;
            }
            //lleno provincias
            DataTable dtcity = new DataTable();
            dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITY", iddepartamento);
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[8].FindControl("ddlProvincia_edit")).DataSource = dtcity;
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[8].FindControl("ddlProvincia_edit")).DataTextField = "Name_City";
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[8].FindControl("ddlProvincia_edit")).DataValueField = "cod_City";
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[8].FindControl("ddlProvincia_edit")).DataBind();
            if (provincia != "")
            {
                ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[8].FindControl("ddlProvincia_edit")).Items.FindByText(provincia).Selected = true;
            }
            //leno distrito
            DataTable dtdistrito = new DataTable();
            dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITO", idprovincia);
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[9].FindControl("ddlDistrito_edit")).DataSource = dtdistrito;
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[9].FindControl("ddlDistrito_edit")).DataTextField = "Name_Local";
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[9].FindControl("ddlDistrito_edit")).DataValueField = "cod_District";
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[9].FindControl("ddlDistrito_edit")).DataBind();

            if (distrito != "")
            {
                ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[9].FindControl("ddlDistrito_edit")).Items.FindByText(distrito).Selected = true;
            }
            //leno barrio
            DataTable dte = null;
            dte = oConn.ejecutarDataTable("UP_WEB_COMBOCOMUNITY", idpais, iddepartamento, idprovincia, iddistrito);
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[10].FindControl("ddlBarrio_edit")).DataSource = dte;
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[10].FindControl("ddlBarrio_edit")).DataTextField = "Name_Community";
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[10].FindControl("ddlBarrio_edit")).DataValueField = "cod_Community";
            ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[10].FindControl("ddlBarrio_edit")).DataBind();
            if (barrio != "")
            {
                ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[11].FindControl("ddlBarrio_edit")).Items.FindByText(barrio).Selected = true;
            }


            ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text.TrimStart();
            ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text.TrimEnd();
            ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text.TrimStart();
            ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text.TrimEnd();
            this.Session["rept"] = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text;
            this.Session["rept1"] = ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[2].FindControl("cmb_Node_Type")).Text;
            this.Session["rept2"] = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text;
            this.Session["rept3"] = ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).Text;
        }
        protected void GVConsAgrupCom_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            LblFaltantes.Text = "";
            ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text.TrimStart();
            ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text.TrimEnd();
            ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text.TrimStart();
            ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text.TrimEnd();
            this.Session["sAgrupName"] = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text;
            this.Session["iNodeType"] = ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[2].FindControl("cmb_Node_Type")).Text.Trim();
            this.Session["iCorp"] = ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).Text.Trim();
            if (((CheckBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[5].FindControl("CheckAgrupCom")).Checked != false)
            {
                estado = true;
            }
            else
            {
                estado = false;
                DAplicacion oddeshabnodo = new DAplicacion();
                DataTable dt = oddeshabnodo.PermitirDeshabilitar(ConfigurationManager.AppSettings["NodeCommercialPointOfSale"], ((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodAgruCom")).Text);
                if (dt != null)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                    MensajeAlerta();
                    return;
                }
            }
            if (((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text == "" || ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[2].FindControl("cmb_Node_Type")).Text == "0" || ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[6].FindControl("ddlPais_edit")).Text == "0")
            {
                LblFaltantes.Text = "Debe ingresar los campos: ";
                if (((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[2].FindControl("cmb_Node_Type")).Text == "0")
                {
                    LblFaltantes.Text += ("Tipo de Agrupación" + " . ");
                }
                if (((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text == "")
                {
                    LblFaltantes.Text += ("Nombre de Agrupación" + " . ");
                }
                if (((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[6].FindControl("ddlPais_edit")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + "País.";
                }
                Alertas.CssClass = "MensajesError";
                CancelarAgrupCome();
                MensajeAlerta();
                return;
            }
            if (((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[7].FindControl("ddlDepartamento_edit")).Items.Count > 0 && ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[7].FindControl("ddlDepartamento_edit")).Text == "0")
            {
                this.Session["mensajealert"] = " Departamento";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Departamento";
                MensajeAlerta();
                return;
            }
            if (((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[8].FindControl("ddlProvincia_edit")).Items.Count > 0 && ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[8].FindControl("ddlProvincia_edit")).Text == "0")
            {
                this.Session["mensajealert"] = " Ciudad/Provincia";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta fatla Ciudad/Provincia";
                MensajeAlerta();
                return;
            }
            if (((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[9].FindControl("ddlDistrito_edit")).Items.Count > 0 && ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[9].FindControl("ddlDistrito_edit")).Text == "0")
            {
                this.Session["mensajealert"] = " Distrito";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Distrito";
                MensajeAlerta();
                return;
            }
            if (((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[10].FindControl("ddlBarrio_edit")).Items.Count > 0 && ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[10].FindControl("ddlBarrio_edit")).Text == "0")
            {
                this.Session["mensajealert"] = " Barrio";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Barrio";
                MensajeAlerta();
                return;
            }

            try
            {
                repetido = Convert.ToString(this.Session["rept"]);
                repetido1 = Convert.ToString(this.Session["rept1"]);
                repetido2 = Convert.ToString(this.Session["rept2"]);
                repetido3 = Convert.ToString(this.Session["rept3"]);
                if (repetido != ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text || repetido1 != ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[2].FindControl("cmb_Node_Type")).Text || repetido2 != ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[3].FindControl("TxtAgrupDirec")).Text || repetido3 != ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).Text)
                {
                    DAplicacion odconsulnodo = new DAplicacion();
                    DataTable dtconsulta = odconsulnodo.ConsultaDuplicados(ConfigurationManager.AppSettings["Nodo"], ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("TxtNomAgrupCom")).Text, ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[2].FindControl("cmb_Node_Type")).Text, ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[4].FindControl("cmbCorpo_Edit")).Text);
                    if (dtconsulta == null)
                    {
                        ENodeComercial oeaNode = dnode.ActualizarNodeComercial(Convert.ToInt32(((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodAgruCom")).Text), ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("TxtNomAgrupCom")).Text, Convert.ToInt32(((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("cmb_Node_Type")).Text), Convert.ToInt32(((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("cmbCorpo_Edit")).Text), ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("TxtAgrupDirec")).Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now, ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("ddlPais_edit")).Text, ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("ddlDepartamento_edit")).Text, ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("ddlProvincia_edit")).Text, ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("ddlDistrito_edit")).Text, ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("ddlBarrio_edit")).Text);
                        ENodeComercial oeacadena = dnode.ActualizarNodeComercialTMP(Convert.ToInt32(((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodAgruCom")).Text), ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("TxtNomAgrupCom")).Text, estado);
                        GVConsAgrupCom.EditIndex = -1;
                        DataTable oeNode = dnode.ConsultarNodeComercial(this.Session["sAgrupName"].ToString().Trim(), Convert.ToInt32(this.Session["iNodeType"].ToString().Trim()), Convert.ToInt32(this.Session["iCorp"].ToString().Trim()));
                        this.Session["tAgrupCome"] = oeNode;
                        if (oeNode != null)
                        {//  tNodeComme
                            if (oeNode.Rows.Count > 0)
                            {
                                gridAgrupComer(oeNode);
                            }
                        }
                        //MopopConsulAgrupCom.Hide();
                        btnCanAgrupCom.Visible = true;
                        MopopConsulAgrupCom.Show();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La categoría de Producto : " + this.Session["sProductType"] + " fue actualizada con éxito";
                        MensajeAlerta();
                        activarControlesAgrupación();
                    }
                    else
                    {
                        this.Session["sNode"] = ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[1].FindControl("TxtNomAgrupCom")).Text;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La Agrupación Comercial : " + this.Session["sNode"] + " No se puede Actualizar este registro ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    ENodeComercial oeaNode = dnode.ActualizarNodeComercial(Convert.ToInt32(((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodAgruCom")).Text), ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("TxtNomAgrupCom")).Text, Convert.ToInt32(((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("cmb_Node_Type")).Text), Convert.ToInt32(((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("cmbCorpo_Edit")).Text), ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("TxtAgrupDirec")).Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now, ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("ddlPais_edit")).Text, ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("ddlDepartamento_edit")).Text, ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("ddlProvincia_edit")).Text, ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("ddlDistrito_edit")).Text, ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("ddlBarrio_edit")).Text);
                    ENodeComercial oeacadena = dnode.ActualizarNodeComercialTMP(Convert.ToInt32(((Label)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("LblCodAgruCom")).Text), ((TextBox)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[0].FindControl("TxtNomAgrupCom")).Text, estado);
                    SavelimpiarcontrolesAgrupación();
                    GVConsAgrupCom.EditIndex = -1;
                    DataTable oeNode = dnode.ConsultarNodeComercial(this.Session["sAgrupName"].ToString().Trim(), Convert.ToInt32(this.Session["iNodeType"].ToString().Trim()), Convert.ToInt32(this.Session["iCorp"].ToString().Trim()));
                    this.Session["tAgrupCome"] = oeNode;
                    if (oeNode != null)
                    {
                        if (oeNode.Rows.Count > 0)
                        {
                           // gridAgrupComer(oeNode);
                            GVConsAgrupCom.DataSource = oeNode;
                            GVConsAgrupCom.DataBind();
                        }
                    }
                    btnCanAgrupCom.Visible = true;
                    MopopConsulAgrupCom.Show();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La categoría de Producto : " + this.Session["sProductType"] + " fue actualizada con éxito";
                    MensajeAlerta();
                    activarControlesAgrupación();
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

        //protected void BtnActualizaNodo_Click(object sender, EventArgs e)
        //{
        //    LblFaltantes.Text = "";
        //    txtNomnodo.Text = txtNomnodo.Text.TrimStart();
        //    txtNomnodo.Text = txtNomnodo.Text.TrimEnd();
        //    if (txtNomnodo.Text == "" || CmbSelTipNodo.Text == "0")
        //    {
        //        if (txtNomnodo.Text == "")
        //        {
        //            LblFaltantes.Text = ". " + "Agrupación Comercial";
        //        }
        //        if (CmbSelTipNodo.Text == "0")
        //        {
        //            LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo de Agrupación Comercial";
        //        }
        //        Alertas.CssClass = "MensajesError";
        //        LblFaltantes.Text = "Ingrese toda la Información";
        //        MensajeAlerta();
        //        return;
        //    }
        //    try
        //    {

        //        if (RbtnEstadonodo.Items[0].Selected == true)
        //        {
        //            estado = true;
        //        }
        //        else
        //        {
        //            estado = false;
        //            DAplicacion oddeshabnodo = new DAplicacion();
        //            DataTable dt = oddeshabnodo.PermitirDeshabilitar(ConfigurationManager.AppSettings["NodeCommercialPointOfSale"], TxtCodnodo.Text);
        //            if (dt != null)
        //            {
        //                Alertas.CssClass = "MensajesError";
        //                LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
        //                MensajeAlerta();
        //                return;
        //            }
        //        }
        //        repetido = Convert.ToString(this.Session["rept"]);
        //        if (repetido != txtNomnodo.Text)
        //        {
        //            DAplicacion odconsulnodo = new DAplicacion();
        //            DataTable dtconsulta = odconsulnodo.ConsultaDuplicados(ConfigurationManager.AppSettings["Nodo"], txtNomnodo.Text, null, null);
        //            if (dtconsulta == null)
        //            {
        //                ENodeComercial oeaNode = dnode.ActualizarNodeComercial(Convert.ToInt32(TxtCodnodo.Text), txtNomnodo.Text, Convert.ToInt32(CmbSelTipNodo.Text), txtdireccion.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
        //                ENodeComercial oeacadena = dnode.ActualizarNodeComercialTMP(Convert.ToInt32(TxtCodnodo.Text), txtNomnodo.Text, estado);


        //                sNode = txtNomnodo.Text;
        //                this.Session["sNode"] = sNode;
        //                SavelimpiarcontrolesAgrupación();
        //                //llenarcombos();
        //                Alertas.CssClass = "MensajesCorrecto";
        //                LblFaltantes.Text = "La Agrupación Comercial : " + this.Session["sNode"] + " se actualizó con éxito";
        //                MensajeAlerta();
        //                saveActivarbotonesAgrupación();
        //                desactivarControlesAgrupación();
        //            }
        //            else
        //            {
        //                sNode = txtNomnodo.Text;
        //                this.Session["sNode"] = sNode;
        //                Alertas.CssClass = "MensajesError";
        //                LblFaltantes.Text = "La Agrupación Comercial : " + this.Session["sNode"] + " No se puede Actualizar este registro ya Existe";
        //                MensajeAlerta();
        //            }
        //        }
        //        else
        //        {
        //            ENodeComercial oeaNode = dnode.ActualizarNodeComercial(Convert.ToInt32(TxtCodnodo.Text), txtNomnodo.Text, Convert.ToInt32(CmbSelTipNodo.Text), txtdireccion.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
        //            ENodeComercial oeacadena = dnode.ActualizarNodeComercialTMP(Convert.ToInt32(TxtCodnodo.Text), txtNomnodo.Text,  estado);



        //            sNode = txtNomnodo.Text;
        //            this.Session["sNode"] = sNode;
        //            SavelimpiarcontrolesAgrupación();
        //            //llenarcombos();
        //            Alertas.CssClass = "MensajesCorrecto";
        //            LblFaltantes.Text = "La Agrupación Comercial : " + this.Session["sNode"] + " Se Actualizo con Exito";
        //            MensajeAlerta();
        //            saveActivarbotonesAgrupación();
        //            desactivarControlesAgrupación();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "";
        //        string mensaje = "";
        //        error = Convert.ToString(ex.Message);
        //        mensaje = ConfigurationManager.AppSettings["ErrorConection"];
        //        if (error == mensaje)
        //        {
        //            Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
        //            string errMessage = "";
        //            errMessage = mensaje;
        //            errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
        //            this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
        //        }
        //        else
        //        {
        //            this.Session.Abandon();
        //            Response.Redirect("~/err_mensaje_seccion.aspx", true);
        //        }
        //    }


        //}
       
        #endregion

        #region Canales x NodoCommercial

       
        protected void btn_editachannelxnode_Click(object sender, EventArgs e)
        {
            cargarddlgpdvcliente();
            cargarddl_typenode_chxnodecom();
            activarcontrolesncxc_crear();
        }

        private void cargarddl_typenode_chxnodecom()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 28);
            //se llena Tipo de nodos en Nodo comercial
            ddl_typenode_chxnodecom.DataSource = ds;
            ddl_typenode_chxnodecom.DataTextField = "NodeComType_name";
            ddl_typenode_chxnodecom.DataValueField = "idNodeComType";
            ddl_typenode_chxnodecom.DataBind();
            ds = null;
        }

        private void activarcontrolesncxc_crear()
        {
            ddl_typenode_chxnodecom.Enabled = true;
            ddl_nodecom_chxnodecom.Enabled = true;
            ddlgpdvcliente.Enabled = true;
            cbxlnodecanal.Enabled = true;           
            btn_guardarchannelxnode.Visible = true;
            btn_editachannelxnode.Visible = false;
            TabPanelSegmentos.Enabled = false;
            TabPanelTipoAgrupación.Enabled = false;
            TabPanelAgrupaciónComercial.Enabled = false;
            Panel_Mallas.Enabled = false;
            Panel_Sector.Enabled = false;
            TabDistribuidora.Enabled = false;
            TabPanelPDV.Enabled = false;
            PanelPDVCliente.Enabled = false;
        }

        private void activarcontrolesncxchannel()
        {
            ddl_typenode_chxnodecom.Enabled = false;
            ddl_nodecom_chxnodecom.Enabled = false;
            ddlgpdvcliente.Enabled = false;
            cbxlnodecanal.Enabled = false;
            btn_guardarchannelxnode.Visible = false;
            btn_editachannelxnode.Visible = true;
            TabPanelSegmentos.Enabled = true;
            TabPanelTipoAgrupación.Enabled = true;
            TabPanelAgrupaciónComercial.Enabled = true;
            Panel_Mallas.Enabled = true;
            Panel_Sector.Enabled = true;
            TabDistribuidora.Enabled = true;
            TabPanelPDV.Enabled = true;
            PanelPDVCliente.Enabled = true;
        }

        protected void ddl_nodecom_chxnodecom_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenacheckboxlistdecanales();
        }

        /** llena checkbox list con canales disponibles por cliente **/
        protected void ddlgpdvcliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenacheckboxlistdecanales();
        }

        private void llenacheckboxlistdecanales() {
            int clientcode = Convert.ToInt32(ddlgpdvcliente.SelectedValue);
            int nodecode = Convert.ToInt32(ddl_nodecom_chxnodecom.Text);
            if (cbxlnodecanal.Visible != true)
                cbxlnodecanal.Visible = true;
            //LLenaCheckBoxListCanal(clientcode);
            activa_opciones_canales(clientcode, nodecode);
            lbl_nochannel.Visible = false;
        }

        private void activa_opciones_canales(int clientcode, int nodecode)
        {
            DataTable oeNodeXChannel = dnode.ConsultarNodeComercialXChannel(clientcode, nodecode);
            ddlgpdvcliente.Text = clientcode.ToString();
            LLenaCheckBoxListCanal(clientcode);
            ////////////////////
            foreach (ListItem item in cbxlnodecanal.Items)
            {
                int itemvalue = Convert.ToInt32(item.Value);
                for (int x = 0; x < oeNodeXChannel.Rows.Count; x++)
                {
                    if (item.Value == oeNodeXChannel.Rows[x]["cod_Channel"].ToString())
                        item.Selected = Convert.ToBoolean(oeNodeXChannel.Rows[x]["ncxchannel_status"]);
                }
            }
        }        

        private void LLenaCheckBoxListCanal(int clientcode)
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACANALXCLIENTE", clientcode);
            cbxlnodecanal.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                cbxlnodecanal.DataSource = dt;
                cbxlnodecanal.DataTextField = "Channel_Name";
                cbxlnodecanal.DataValueField = "cod_Channel";
                cbxlnodecanal.DataBind();
            }
        }
        /** fin llenado checkboxlist **/

        protected void btn_guardarchannelxnode_Click(object sender, EventArgs e)
        {
            if (ddlgpdvcliente.SelectedIndex != 0 || ddl_nodecom_chxnodecom.SelectedIndex != 0 || ddl_typenode_chxnodecom.SelectedIndex != 0)
            {
                try
                {
                    /** Actualiza cada una de las relaciones del Nodo para los canales seleccionados - Angel Ortiz - 30/06/2011 **/
                    int st = 0;
                    int aux = 0;
                    int idcliente = Convert.ToInt32(ddlgpdvcliente.SelectedValue);
                    foreach (ListItem item in cbxlnodecanal.Items)
                    {
                        int itemvalue = Convert.ToInt32(item.Value);
                        bool nodoxcanal_estado = item.Selected;
                        //{
                        st = dnode.ActualizarNodeComercialXChannel(Convert.ToInt32(ddl_nodecom_chxnodecom.Text), itemvalue, idcliente, nodoxcanal_estado, Convert.ToString(this.Session["sUser"]));
                        /** actuliza la relacion Node_X_Canal en la DB Intermedia **/
                        aux = dnode.ActualizarNodeComercialXChannelTMP(idcliente, itemvalue, Convert.ToInt32(ddl_nodecom_chxnodecom.Text), nodoxcanal_estado);
                        /** Fin actualizacion en DB Intermedia **/
                        //}
                    }
                    /** fin de actualizacion de Nodo por Canal Seleccionado **/
                    Alertas.CssClass = "MensajesCorrecto";
                    limpiacontrolesncxchannel();
                    activarcontrolesncxchannel();
                    LblFaltantes.Text = "Las asignaciones de Agrupación Comercial con Canales se actualizaron con éxito";
                    MensajeAlerta();
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
            else {                
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese todos los parámetro de selección";
                MensajeAlerta();
            }
        }


        private void limpiacontrolesncxchannel() {

            ddl_typenode_chxnodecom.Items.Clear();
            ddl_nodecom_chxnodecom.Items.Clear();
            ddlgpdvcliente.Items.Clear();
            cbxlnodecanal.Items.Clear();
            
        }
        protected void btn_cancelarchannelxnode_Click(object sender, EventArgs e)
        {
            limpiacontrolesncxchannel();
            activarcontrolesncxchannel();
        }

     
        
        protected void ddl_typenode_chxnodecom_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable oeNode = dnode.ConsultarNodeComercial("", Convert.ToInt32(ddl_typenode_chxnodecom.Text), 0);
            ddl_nodecom_chxnodecom.DataSource = oeNode;
            ddl_nodecom_chxnodecom.DataTextField = "commercialNodeName";
            ddl_nodecom_chxnodecom.DataValueField = "NodeCommercial";
            ddl_nodecom_chxnodecom.DataBind();
            ddl_nodecom_chxnodecom.Items.Insert(0, new ListItem("<Selecccione ...>", "0"));  
        }
        
        protected void btn_bcancel_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Eventos Malla
        protected void BtnCrearmallas_Click(object sender, EventArgs e)
        {
            comboclienteenMalla(); 
            crearActivarbotonesmalla();
            activarControlesmalla();
        }
        protected void BtnSavemalla_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomallas.Text = TxtNomallas.Text.TrimStart();
            TxtNomallas.Text = TxtNomallas.Text.TrimEnd();
            if (TxtNomallas.Text == "" || cmbClienteMallas.Text == "0")
            {
                if (TxtNomallas.Text == "")
                {
                    LblFaltantes.Text = ". " + "Nombre Malla";
                }
                if (cmbClienteMallas.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {

                DAplicacion odconsulMalla = new DAplicacion();
                DataTable dtconsulta = odconsulMalla.ConsultaDuplicados(ConfigurationManager.AppSettings["Mallas"], cmbClienteMallas.Text, TxtNomallas.Text, null);
                if (dtconsulta == null)
                {
                    EMalla oeMalla = oMalla.RegistrarMallas(TxtNomallas.Text, Convert.ToInt32(cmbClienteMallas.Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sMalla = "";
                    sMalla = TxtNomallas.Text;
                    this.Session["sMalla"] = sMalla;
                    Savelimpiarcontrolesmalla();
                    //LlenacomboMarcaensubMarca();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Malla de Punto de venta " + this.Session["sMalla"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesmalla();
                    desactivarControlesmalla();

                }
                else
                {
                    string sMalla = "";
                    sMalla = TxtNomallas.Text;
                    this.Session["sMalla"] = sMalla;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La Malla de Punto de venta " + this.Session["sMalla"] + " Ya Existe";
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
        protected void BtnBmalla_Click(object sender, EventArgs e)
        {
            desactivarControlesmalla();
            LblFaltantes.Text = "";
            TxtBNommalla.Text = TxtBNommalla.Text.TrimStart();


            if (TxtBNommalla.Text == "")
            {
                this.Session["mensajealert"] = "Código y/o Nombre de Marca de producto";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnMalla.Show();
                return;
            }
            BuscarActivarbotnesmalla();
            sMalla = TxtBNommalla.Text;
            TxtBNommalla.Text = "";
            DataTable oeMalla = oMalla.ConsultarMalla(sMalla);
            if (oeMalla != null)
            {
                if (oeMalla.Rows.Count > 0)
                {
                    for (int i = 0; i <= oeMalla.Rows.Count - 1; i++)
                    {
                        TxtCodMallas.Text = oeMalla.Rows[0]["id_malla"].ToString().Trim();
                        comboclienteenMalla(); 
                        cmbClienteMallas.Text = oeMalla.Rows[0]["Company_id"].ToString().Trim();
                        TxtNomallas.Text = oeMalla.Rows[0]["malla"].ToString().Trim();
                        estado = Convert.ToBoolean(oeMalla.Rows[0]["malla_Status"].ToString().Trim());

                        if (estado == true)
                        {
                            RbtnmallasStatus.Items[0].Selected = true;
                            RbtnmallasStatus.Items[1].Selected = false;
                        }
                        else
                        {
                            RbtnmallasStatus.Items[0].Selected = false;
                            RbtnmallasStatus.Items[1].Selected = true;
                        }
                        this.Session["tMalla"] = oeMalla;
                        this.Session["i"] = 0;

                    }

                    if (oeMalla.Rows.Count == 1)
                    {
                        PregMalla.Visible = false;
                        AregMalla.Visible = false;
                        SregMalla.Visible = false;
                        UregMalla.Visible = false;
                    }
                    else
                    {
                        PregMalla.Visible = true;
                        AregMalla.Visible = true;
                        SregMalla.Visible = true;
                        UregMalla.Visible = true;
                    }

                }
                else
                {
                    Savelimpiarcontrolesmalla();
                    saveActivarbotonesmalla();
                    comboclienteenMalla();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnMalla.Show();
                }
            }
        }
        protected void BtnEditmallas_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesmalla();
            EditarActivarControlesmalla();
            this.Session["rept"] = TxtNomallas.Text;

        }
        protected void BtnActualizamallas_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomallas.Text = TxtNomallas.Text.TrimStart();
            TxtNomallas.Text = TxtNomallas.Text.TrimEnd();
            if (TxtNomallas.Text == "" || cmbClienteMallas.Text == "0")
            {
                if (TxtNomallas.Text == "")
                {
                    LblFaltantes.Text = ". " + "Nombre Malla";
                }
                if (cmbClienteMallas.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
                if (RbtnmallasStatus.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }

                repetido = Convert.ToString(this.Session["rept"]);
                if (repetido != TxtNomallas.Text)
                {
                    DAplicacion odconsulMalla = new DAplicacion();
                    DataTable dtconsulta = odconsulMalla.ConsultaDuplicados(ConfigurationManager.AppSettings["Mallas"], cmbClienteMallas.Text, TxtNomallas.Text, null);
                    if (dtconsulta == null)
                    {
                        EMalla oeMalla = oMalla.Actualizar_Malla(Convert.ToInt32(TxtCodMallas.Text), Convert.ToInt32(cmbClienteMallas.Text), TxtNomallas.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        sMalla = TxtNomallas.Text;
                        this.Session["sMalla"] = sMalla;
                        Savelimpiarcontrolesmalla();
                        //LlenacomboMarcaensubMarca();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Malla de Punto de Venta : " + this.Session["sMalla"] + " Se Actualizo Corecctamente";
                        MensajeAlerta();
                        saveActivarbotonesmalla();
                        desactivarControlesmalla();
                    }
                    else
                    {
                        sMalla = TxtNomallas.Text;
                        this.Session["sMalla"] = sMalla;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La Malla de Punto de Venta : " + this.Session["sMalla"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {

                    EMalla oeMalla = oMalla.Actualizar_Malla(Convert.ToInt32(TxtCodMallas.Text), Convert.ToInt32(cmbClienteMallas.Text), TxtNomallas.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    sMalla = TxtNomallas.Text;
                    this.Session["sMalla"] = sMalla;
                    Savelimpiarcontrolesmalla();
                    //LlenacomboMarcaensubMarca();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Malla de Punto de Venta : " + this.Session["sMalla"] + " Se Actualizo Corecctamente";
                    MensajeAlerta();
                    saveActivarbotonesmalla();
                    desactivarControlesmalla();
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
        protected void BtnCancelmalla_Click(object sender, EventArgs e)
        {
            Savelimpiarcontrolesmalla();
            saveActivarbotonesmalla();
            desactivarControlesmalla();

        }
        private void MostrarDatosMalla()
        {
            recorrido = (DataTable)this.Session["tMalla"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodMallas.Text = recorrido.Rows[recsearch]["id_malla"].ToString().Trim();
                    cmbClienteMallas.Text = recorrido.Rows[recsearch]["Company_id"].ToString().Trim();
                    TxtNomallas.Text = recorrido.Rows[recsearch]["malla"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["malla_Status"].ToString().Trim());
                    
                    if (estado == true)
                    {
                        RbtnmallasStatus.Items[0].Selected = true;
                        RbtnmallasStatus.Items[1].Selected = false;
                    }
                    else
                    {
                        RbtnmallasStatus.Items[0].Selected = false;
                        RbtnmallasStatus.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void PregMalla_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tMalla"];
            MostrarDatosMalla();
        }
        protected void AregMalla_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosMalla();
            }
        }
        protected void SregMalla_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tMalla"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosMalla();
            }
        }
        protected void UregMalla_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tMalla"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosMalla();
        }
        #endregion

        #region Eventos Sector
        protected void BtnCrearSector_Click(object sender, EventArgs e)
        {
            LlenacomboClienteSector();
            crearActivarbotonesSector();
            activarControlesSector();
        }
        protected void CmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenacomboMallasenSector();
        }
        protected void BtnsaveSector_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomSector.Text = TxtNomSector.Text.TrimStart();
            TxtNomSector.Text = TxtNomSector.Text.TrimEnd();
            string sidMalla = CmbmallaSector.Text;
            if (TxtNomSector.Text == "" || CmbCliente.Text == "0" || CmbmallaSector.Text == "0")
            {
                if (TxtNomSector.Text == "")
                {
                    LblFaltantes.Text = ". " + "Sector Punto de Venta";
                }
                if (CmbCliente.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }
                if (CmbmallaSector.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Malla de Punto de Ventaxs";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {

                DAplicacion odconsulSector = new DAplicacion();
                DataTable dtconsulta = odconsulSector.ConsultaDuplicados(ConfigurationManager.AppSettings["Sector"], TxtNomSector.Text, CmbmallaSector.Text, null);
                if (dtconsulta == null)
                {
                    ESector oeSector = oSector.RegistrarSector(TxtNomSector.Text, Convert.ToInt32(CmbmallaSector.Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sSector = "";
                    sSector = CmbmallaSector.SelectedItem.Text + " " + TxtNomSector.Text;
                    this.Session["sSector"] = sSector;
                    LlenacomboMallasenSector();
                    SavelimpiarcontrolesSector();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Sector de Puntos de Venta " + this.Session["sSector"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesSector();
                    desactivarControleSector();
                }
                else
                {
                    string sSector = "";
                    sSector = CmbmallaSector.SelectedItem.Text + " " + TxtNomSector.Text;
                    this.Session["sSector"] = sSector;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Sector de Puntos de Venta " + this.Session["sSector"] + " Ya Existe";
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
        protected void BtnBSector_Click(object sender, EventArgs e)
        {
            desactivarControleSector();
            LblFaltantes.Text = "";
            TxtBNomSector.Text = TxtBNomSector.Text.TrimStart();


            if (TxtBNomSector.Text == "" && CmbBmallaSector.Text == "0" && cmbBClienteSector.Text == "0")
            {
                this.Session["mensajealert"] = "Nombre de SubSybCategoria y/o Categoria de Producto";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnSector.Show();
                return;
            }

            BuscarActivarbotnesSector();
            sSector = TxtBNomSector.Text;
            if (CmbBmallaSector.Text != "")
            {
                sCodMalla = CmbBmallaSector.Text;
            }
            iCompany_id = Convert.ToInt32(cmbBClienteSector.Text);
            TxtBNomSector.Text = "";
            CmbBmallaSector.Text = "0";
            cmbBClienteSector.Text = "0";

            DataTable oeSector = oSector.ConsultarSector(Convert.ToInt32(sCodMalla), sSector, iCompany_id);


            if (oeSector != null)
            {
                if (oeSector.Rows.Count > 0)
                {
                    TxtCodSector.Text = oeSector.Rows[0]["id_sector"].ToString().Trim();
                    LlenacomboClienteSector();
                    CmbCliente.Text = oeSector.Rows[0]["Company_id"].ToString().Trim();
                    LlenacomboMallasenSector();
                    CmbmallaSector.Text = oeSector.Rows[0]["id_malla"].ToString().Trim();
                    TxtNomSector.Text = oeSector.Rows[0]["Sector"].ToString().Trim();
                    estado = Convert.ToBoolean(oeSector.Rows[0]["Sector_Status"].ToString().Trim());

                    if (estado == true)
                    {
                        RbtnStatusSector.Items[0].Selected = true;
                        RbtnStatusSector.Items[1].Selected = false;
                    }
                    else
                    {
                        RbtnStatusSector.Items[0].Selected = false;
                        RbtnStatusSector.Items[1].Selected = true;
                    }
                    this.Session["teSector"] = oeSector;
                    this.Session["i"] = 0;

                    if (oeSector.Rows.Count == 1)
                    {
                        PregSector.Visible = false;
                        AregSector.Visible = false;
                        SregSector.Visible = false;
                        UregSector.Visible = false;
                    }
                    else
                    {
                        PregSector.Visible = true;
                        AregSector.Visible = true;
                        SregSector.Visible = true;
                        UregSector.Visible = true;
                    }
                }
                else
                {
                    SavelimpiarcontrolesSector();
                    saveActivarbotonesSector();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnSector.Show();
                }
            }
        }
        protected void cmbBClienteSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenacomboBuscarMallasenSector();
            IbtnSector.Show();
        }
        protected void BtnEditSector_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesSector();
            EditarActivarControlesSector();
            this.Session["rept"] = TxtNomSector.Text;
            this.Session["rept1"] = CmbmallaSector.Text;
         

        }
        protected void BtnActualizaSector_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomSector.Text = TxtNomSector.Text.TrimStart();
            TxtNomSector.Text = TxtNomSector.Text.TrimEnd();
            string sidMalla = CmbmallaSector.Text;
            if (TxtNomSector.Text == "" || CmbCliente.Text == "0" || CmbmallaSector.Text == "0")
            {
                if (TxtNomSector.Text == "")
                {
                    LblFaltantes.Text = ". " + "Sector Punto de Venta";
                }
                if (CmbmallaSector.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Malla de Punto de Ventaxs";
                }
                if (CmbCliente.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Malla de Punto de Ventaxs";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
                if (RbtnStatusSector.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    //DAplicacion oddeshabSubBrand = new DAplicacion();
                    //DataTable dt = oddeshabSubBrand.PermitirDeshabilitar(ConfigurationManager.AppSettings["SubBrandProducts"], TxtCodSubMarca.Text);
                    //if (dt != null)
                    //{

                    //    Alertas.CssClass = "MensajesError";
                    //    LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Perfil, por favor Verifique";
                    //    MensajeAlerta();
                    //    return;
                    //}
                }
                repetido = Convert.ToString(this.Session["rept"]);
                repetido1 = Convert.ToString(this.Session["rept1"]);
                if (repetido != TxtNomSector.Text || repetido1 != CmbmallaSector.Text)
                {

                    DAplicacion odconsulSector = new DAplicacion();
                    DataTable dtconsulta = odconsulSector.ConsultaDuplicados(ConfigurationManager.AppSettings["Sector"], TxtNomSector.Text, CmbmallaSector.Text, null);
                    if (dtconsulta == null)
                    {
                        ESector oeSector = oSector.Actualizar_Sector(Convert.ToInt32(TxtCodSector.Text), Convert.ToInt32(CmbmallaSector.Text), TxtNomSector.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        sSector = CmbmallaSector.SelectedItem.Text + " " + TxtNomSector.Text;
                        this.Session["sSector"] = sSector;
                        LlenacomboMallasenSector();
                        SavelimpiarcontrolesSector();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Sector de Puntos de Venta " + this.Session["sSector"] + " fue Actualizado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesSector();
                        desactivarControleSector();

                    }
                    else
                    {
                        sSector = CmbmallaSector.SelectedItem.Text + " " + TxtNomSector.Text;
                        this.Session["sSector"] = sSector;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Sector de Puntos de Venta " + this.Session["sSector"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    ESector oeSector = oSector.Actualizar_Sector(Convert.ToInt32(TxtCodSector.Text), Convert.ToInt32(CmbmallaSector.Text), TxtNomSector.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    sSector = CmbmallaSector.SelectedItem.Text + " " + TxtNomSector.Text;
                    this.Session["sSector"] = sSector;
                    LlenacomboMallasenSector();
                    SavelimpiarcontrolesSector();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Sector de Puntos de Venta " + this.Session["sSector"] + " fue Actualizado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesSector();
                    desactivarControleSector();
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
        protected void BtnCancelSector_Click(object sender, EventArgs e)
        {
            SavelimpiarcontrolesSector();
            saveActivarbotonesSector();
            desactivarControleSector();

        }
        private void MostrarDatosSector()
        {
            recorrido = (DataTable)this.Session["teSector"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodSector.Text = recorrido.Rows[recsearch]["id_sector"].ToString().Trim();
                    CmbCliente.Text = recorrido.Rows[recsearch]["Company_id"].ToString().Trim();
                    CmbmallaSector.Text = recorrido.Rows[recsearch]["id_malla"].ToString().Trim();
                    TxtNomSector.Text = recorrido.Rows[recsearch]["Sector"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Sector_Status"].ToString().Trim());

                    if (estado == true)
                    {
                        RbtnStatusSector.Items[0].Selected = true;
                        RbtnStatusSector.Items[1].Selected = false;
                    }
                    else
                    {
                        RbtnStatusSector.Items[0].Selected = false;
                        RbtnStatusSector.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void PregSector_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["teSector"];
            MostrarDatosSector();
        }
        protected void AregSector_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosSector();
            }
        }
        protected void SregSector_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["teSector"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosSector();
            }
        }
        protected void UregSector_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["teSector"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosSector();
        }

        #endregion

        #region Eventos Distribuidora
        protected void creardistrib_Click(object sender, EventArgs e)
        {
            crearActivarbotonesDistribuidora();
            activarControlesDistribuidora();

        }
        protected void Guardardistri_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TextDistribuidora.Text = TextDistribuidora.Text.TrimStart();
            TextDistribuidora.Text = TextDistribuidora.Text.TrimEnd();
      
            if (TextDistribuidora.Text == "")
            {
                if (TextDistribuidora.Text == "")
                {
                    LblFaltantes.Text = ". " + "Nombre de Distribuidora";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese el parametro de Consulta";
                MensajeAlerta();
                return;

            }

            try
            {
                if (Rbtestadodistribuidora.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }
                DAplicacion odconsultipnodo = new DAplicacion();
                DataTable dtconsulta = odconsultipnodo.ConsultaDuplicados(ConfigurationManager.AppSettings["Distribuidora"], TextDistribuidora.Text, null, null);
                if (dtconsulta == null)
                {
                    EAD_Distribuidora oeDex = oDex.RegistrarDex(TextDistribuidora.Text, estado,
                                            Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                   
                    string sDex = "";
                    sDex = TextDistribuidora.Text;
                    this.Session["sDex"] = sDex;
                    SavelimpiarcontrolesDistribuidora();
                    desactivarControlesDistribuidora();
                    //combotiponodo();
                    //comboDistribuidora();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Distribuidora  " + this.Session["sDex"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesDistribuidora();
                }
                else
                {
                    string sDex = "";
                    sDex = TextDistribuidora.Text;
                    this.Session["sDex"] = sDex;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La Distribuidora  " + this.Session["sDex"] + " Ya Existe";
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
        protected void BtnBucarDistribuidora_Click(object sender, EventArgs e)
        {

            desactivarControlesDistribuidora();
            LblFaltantes.Text = "";
            //cmbBDistribuidora.Text = txtBDistribuidora.Text.TrimStart();
            if (cmbBDistribuidora.Text == "0")
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese el parametro de consulta";
                MensajeAlerta();
                modalBusDistribuidora.Show();
                return;

            }

            BuscarActivarbotnesDistribuidora();
            sdex = cmbBDistribuidora.Text;
            cmbBDistribuidora.Text = "0";
            DataTable oedex = oDex.Consultardex(Convert.ToInt32(sdex));

            if (oedex != null)
            {
                if (oedex.Rows.Count > 0)
                {
                    for (int i = 0; i <= oedex.Rows.Count - 1; i++)
                    {
                        Texcodigod.Text = oedex.Rows[0]["Id_Dex"].ToString().Trim();
                        TextDistribuidora.Text = oedex.Rows[0]["Dex_Name"].ToString().Trim();
                        estado = Convert.ToBoolean(oedex.Rows[0]["Dex_Status"].ToString().Trim());

                        if (estado == true)
                        {
                            Rbtestadodistribuidora.Items[0].Selected = true;
                            Rbtestadodistribuidora.Items[1].Selected = false;
                        }
                        else
                        {
                            Rbtestadodistribuidora.Items[0].Selected = false;
                            Rbtestadodistribuidora.Items[1].Selected = true;
                        }
                        this.Session["tdex"] = oedex;
                        this.Session["i"] = 0;

                    }

                    if (oedex.Rows.Count == 1)
                    {


                        primero.Visible = false;
                        antes.Visible = false;
                        siguiente.Visible = false;
                        ultimo.Visible = false;
                    }
                    else
                    {
                        primero.Visible = true;
                        antes.Visible = true;
                        siguiente.Visible = true;
                        ultimo.Visible = true;
                    }

                }
                else
                {
                    SavelimpiarcontrolesDistribuidora();
                    saveActivarbotonesDistribuidora();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    modalBusDistribuidora.Show();
                }
            }

        }
        protected void editardistribuidor_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesDistribuidora();
            EditarActivarControlesDistribuidora();
            this.Session["rept"] = TextDistribuidora.Text;

        }
        protected void ActualizarDistri_Click(object sender, EventArgs e)
        {

            LblFaltantes.Text = "";
            TextDistribuidora.Text = TextDistribuidora.Text.TrimEnd();
            TextDistribuidora.Text = TextDistribuidora.Text.TrimStart();
            if (TextDistribuidora.Text == "")
            {
                if (txtNomtiponodo.Text == "")
                {
                    LblFaltantes.Text = ". " + "Distribuidora";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese el parametro de consulta";
                MensajeAlerta();
                return;

            }
            try
            {

                if (Rbtestadodistribuidora.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabtiponodo = new DAplicacion();
                    DataTable dt1 = oddeshabtiponodo.PermitirDeshabilitar(ConfigurationManager.AppSettings["Dex"], Texcodigod.Text);
                  
                    if (dt1 != null )
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en Punntos de Venta. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }
                }
                repetido = Convert.ToString(this.Session["rept"]);
                if (repetido != TextDistribuidora.Text)
                {

                    DAplicacion odconsultipnodo = new DAplicacion();
                    DataTable dtconsulta = odconsultipnodo.ConsultaDuplicados(ConfigurationManager.AppSettings["Distribuidora"], TextDistribuidora.Text, null, null);
                    if (dtconsulta == null)
                    {
                        EAD_Distribuidora oeaDex = oDex.ActualizarDex(Convert.ToInt32(Texcodigod.Text), TextDistribuidora.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);                            
                        
                        sdex = TextDistribuidora.Text;
                        this.Session["sdex"] = sdex;
                        SavelimpiarcontrolesDistribuidora();
                        LlenacomboconsultaDistribuidora();
                       // comboDistribuidora();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Distribuidora : " + this.Session["sdex"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesDistribuidora();
                        desactivarControlesDistribuidora();
                    }
                    else
                    {
                        sdex = TextDistribuidora.Text;
                        this.Session["sdex"] = sdex;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La Distribuidora : " + this.Session["sdex"] + " No se puede Actualizar este registro ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    EAD_Distribuidora oeaDex = oDex.ActualizarDex(Convert.ToInt32(Texcodigod.Text), TextDistribuidora.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now); 

                    sdex = TextDistribuidora.Text;
                    this.Session["sdex"] = sdex;
                    SavelimpiarcontrolesDistribuidora();
                    LlenacomboconsultaDistribuidora();
                    //comboDistribuidora();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Distribuidora : " + this.Session["sdex"] + " Se Actualizo con Exito";
                    MensajeAlerta();
                    saveActivarbotonesDistribuidora();
                    desactivarControlesDistribuidora();

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
        protected void cancelardistrib_Click(object sender, EventArgs e)
        {
            SavelimpiarcontrolesDistribuidora();
            saveActivarbotonesDistribuidora();
            desactivarControlesDistribuidora();
        }
        private void MostrarDatosDex()
        {
            recorrido = (DataTable)this.Session["tdex"]; 
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    Texcodigod.Text = recorrido.Rows[recsearch]["Id_Dex"].ToString().Trim();
                    TextDistribuidora.Text = recorrido.Rows[recsearch]["Dex_Name"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Dex_Status"].ToString().Trim());                                     
          

                    if (estado == true)
                    {
                        Rbtestadodistribuidora.Items[0].Selected = true;
                        Rbtestadodistribuidora.Items[1].Selected = false;
                    }
                    else
                    {
                        Rbtestadodistribuidora.Items[0].Selected = false;
                        Rbtestadodistribuidora.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void primero_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tdex"];
            MostrarDatosDex();
        }
        protected void antes_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosDex();
            }
        }
        protected void siguiente_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tdex"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosDex();
            }
        }
        protected void ultimo_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tdex"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosDex();

        }
     
        #endregion

        #region Eventos de Puntos de Venta
        protected void btnCrearPos_Click(object sender, EventArgs e)
        {
            comboDoc();
            llenaPaisPDV();
            comboCanales();
            comboTipoMercado();
            combosegmenpdv();
            crearActivarbotonesPDV();
            activarControlesPDV();
        }
        protected void CmbTipMerc_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboNodos();
        }

        protected void btnPtoVentaMasivo_Click(object sender, System.EventArgs e)
        {
            this.Session["TipoCarga"] = "PtoVentas";
            IframeCargarPtoVenta.Attributes["src"] = "carga_masiva.aspx";
            ModalPCargaPtoVenta.Show();
        }
        
        protected void cmbSelCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSelDpto.CssClass = null;
            cmbSelProvince.CssClass = null;
            cmbSelDistrict.CssClass = null;
            cmbSelComunity.CssClass = null;
            cmbSelDpto.Enabled = true;
            cmbSelProvince.Enabled = true;
            cmbSelDistrict.Enabled = true;
            cmbSelComunity.Enabled = true;
            cmbSelDpto.Items.Clear();
            cmbSelProvince.Items.Clear();
            cmbSelDistrict.Items.Clear();
            cmbSelComunity.Items.Clear();

            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", cmbSelCountry.Text);
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

            if (oescountry.CountryDpto == false)
            {
                cmbSelDpto.Enabled = false;
                cmbSelDpto.CssClass = "divipol";
                cmbSelDpto.Items.Clear();
            }

            if (oescountry.CountryCiudad == false)
            {
                cmbSelProvince.Enabled = false;
                cmbSelProvince.CssClass = "divipol";
                cmbSelProvince.Items.Clear();
            }
            if (oescountry.CountryDistrito == false)
            {
                cmbSelDistrict.Enabled = false;
                cmbSelDistrict.CssClass = "divipol";
                cmbSelDistrict.Items.Clear();
            }
            if (oescountry.CountryBarrio == false)
            {
                cmbSelComunity.Enabled = false;
                cmbSelComunity.CssClass = "divipol";
                cmbSelComunity.Items.Clear();
            }


            if (oescountry.CountryDpto == true)
            {
                cmbSelDpto.Enabled = true;
                cmbSelDpto.CssClass = null;
                DataTable dtcountry = new DataTable();
                dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", cmbSelCountry.Text);
                if (dtcountry != null)
                {
                    if (dtcountry.Rows.Count > 1)
                    {
                        cmbSelDpto.DataSource = dtcountry;
                        cmbSelDpto.DataTextField = "Name_dpto";
                        cmbSelDpto.DataValueField = "cod_dpto";
                        cmbSelDpto.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Punto de Venta para este país. Falta crear el(los) Departamentos.";
                        MensajeAlerta();
                        cmbSelDpto.DataSource = dtcountry;
                        cmbSelDpto.DataTextField = "Name_dpto";
                        cmbSelDpto.DataValueField = "cod_dpto";
                        cmbSelDpto.DataBind();
                    }
                }
                dtcountry = null;
            }
            else
            {
                cmbSelDpto.Enabled = false;
                cmbSelDpto.CssClass = "divipol";
                cmbSelDpto.Items.Clear();
                if (oescountry.CountryCiudad == true)
                {
                    DataTable dtcity = new DataTable();
                    dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITYCONPAIS", cmbSelCountry.Text);
                    if (dtcity.Rows.Count > 1)
                    {
                        cmbSelProvince.DataSource = dtcity;
                        cmbSelProvince.DataTextField = "Name_City";
                        cmbSelProvince.DataValueField = "cod_City";
                        cmbSelProvince.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Punto de Venta para este país. Falta crear la(s) Ciudades.";
                        MensajeAlerta();
                        cmbSelProvince.DataSource = dtcity;
                        cmbSelProvince.DataTextField = "Name_City";
                        cmbSelProvince.DataValueField = "cod_City";
                        cmbSelProvince.DataBind();
                    }
                    dtcity = null;
                }
                else
                {
                    cmbSelProvince.Enabled = false;
                    cmbSelProvince.CssClass = "divipol";
                    cmbSelProvince.Items.Clear();
                    if (oescountry.CountryDistrito == true)
                    {
                        DataTable dtdistrito = new DataTable();
                        dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONPAIS", cmbSelCountry.Text);
                        if (dtdistrito.Rows.Count > 1)
                        {
                            cmbSelDistrict.DataSource = dtdistrito;
                            cmbSelDistrict.DataTextField = "Name_Local";
                            cmbSelDistrict.DataValueField = "cod_District";
                            cmbSelDistrict.DataBind();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "No es posible crear Punto de Venta para este país. Falta crear la(s) Distritos.";
                            MensajeAlerta();
                            cmbSelDistrict.DataSource = dtdistrito;
                            cmbSelDistrict.DataTextField = "Name_Local";
                            cmbSelDistrict.DataValueField = "cod_District";
                            cmbSelDistrict.DataBind();
                        }
                        dtdistrito = null;
                    }
                    else
                    {
                        cmbSelDistrict.Enabled = false;
                        cmbSelDistrict.CssClass = "divipol";
                        cmbSelDistrict.Items.Clear();
                        if (oescountry.CountryBarrio == true)
                        {

                            DataTable dtbarrio = new DataTable();

                            dtbarrio = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONPAIS", cmbSelCountry.Text);
                            if (dtbarrio.Rows.Count > 1)
                            {
                                cmbSelComunity.DataSource = dtbarrio;
                                cmbSelComunity.DataTextField = "Name_Community";
                                cmbSelComunity.DataValueField = "cod_Community";
                                cmbSelComunity.DataBind();
                            }
                            else
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "No es posible crear Punto de Venta para este país. Falta crear la(s) Barrios.";
                                MensajeAlerta();
                                cmbSelComunity.DataSource = dtbarrio;
                                cmbSelComunity.DataTextField = "Name_Community";
                                cmbSelComunity.DataValueField = "cod_Community";
                                cmbSelComunity.DataBind();
                            }
                            dtbarrio = null;
                        }
                        else
                        {
                            cmbSelComunity.Enabled = false;
                            cmbSelComunity.CssClass = "divipol";
                            cmbSelComunity.Items.Clear();
                        }
                    }
                }
            }
            dt = null;
            oescountry = null;

        }
        protected void cmbSelDpto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSelProvince.CssClass = null;
            cmbSelDistrict.CssClass = null;
            cmbSelComunity.CssClass = null;
            cmbSelProvince.Enabled = true;
            cmbSelDistrict.Enabled = true;
            cmbSelComunity.Enabled = true;
            cmbSelProvince.Items.Clear();
            cmbSelDistrict.Items.Clear();
            cmbSelComunity.Items.Clear();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", cmbSelCountry.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }

            if (oescountry.CountryCiudad == false)
            {
                cmbSelProvince.Enabled = false;
                cmbSelProvince.CssClass = "divipol";
                cmbSelProvince.Items.Clear();
            }

            if (oescountry.CountryDistrito == false)
            {
                cmbSelDistrict.Enabled = false;
                cmbSelDistrict.CssClass = "divipol";
                cmbSelDistrict.Items.Clear();

            }
            if (oescountry.CountryBarrio == false)
            {
                cmbSelComunity.Enabled = false;
                cmbSelComunity.CssClass = "divipol";
                cmbSelComunity.Items.Clear();
            }

            if (oescountry.CountryCiudad == true)
            {
                cmbSelProvince.Enabled = true;
                cmbSelProvince.CssClass = null;
                DataTable dtcity = new DataTable();
                dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITY", cmbSelDpto.Text);
                if (dtcity.Rows.Count > 1)
                {
                    cmbSelProvince.DataSource = dtcity;
                    cmbSelProvince.DataTextField = "Name_City";
                    cmbSelProvince.DataValueField = "cod_City";
                    cmbSelProvince.DataBind();
                    dtcity = null;
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No es posible crear Punto de Venta para este país. Falta crear el(los) Ciudad.";
                    MensajeAlerta();
                    cmbSelProvince.DataSource = dtcity;
                    cmbSelProvince.DataTextField = "Name_City";
                    cmbSelProvince.DataValueField = "cod_City";
                    cmbSelProvince.DataBind();
                }
                dtcity = null;
            }
            else
            {
                cmbSelProvince.Enabled = false;
                cmbSelProvince.CssClass = "divipol";
                cmbSelProvince.Items.Clear();
                if (oescountry.CountryDistrito == true)
                {
                    cmbSelDistrict.Enabled = true;
                    cmbSelDistrict.CssClass = null;
                    DataTable dtdistrito = new DataTable();
                    dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONDEPTO", cmbSelDpto.Text);
                    if (dtdistrito.Rows.Count > 1)
                    {
                        cmbSelDistrict.DataSource = dtdistrito;
                        cmbSelDistrict.DataTextField = "Name_Local";
                        cmbSelDistrict.DataValueField = "cod_District";
                        cmbSelDistrict.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Punto de Venta para este país. Falta crear el(los) Distrito.";
                        MensajeAlerta();
                        cmbSelDistrict.DataSource = dtdistrito;
                        cmbSelDistrict.DataTextField = "Name_Local";
                        cmbSelDistrict.DataValueField = "cod_District";
                        cmbSelDistrict.DataBind();
                    }
                    dtdistrito = null;
                }
                else
                {
                    cmbSelDistrict.Enabled = false;
                    cmbSelDistrict.CssClass = "divipol";
                    cmbSelDistrict.Items.Clear();
                    if (oescountry.CountryBarrio == true)
                    {
                        cmbSelComunity.Enabled = true;
                        cmbSelComunity.CssClass = null;
                        DataTable dtbarrio = new DataTable();
                        dtbarrio = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONDPTO", cmbSelDpto.Text);
                        if (dtbarrio.Rows.Count > 1)
                        {
                            cmbSelComunity.DataSource = dtbarrio;
                            cmbSelComunity.DataTextField = "Name_Community";
                            cmbSelComunity.DataValueField = "cod_Community";
                            cmbSelComunity.DataBind();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "No es posible crear Punto de Venta para este país. Falta crear el(los) Barrios.";
                            MensajeAlerta();
                            cmbSelComunity.DataSource = dtbarrio;
                            cmbSelComunity.DataTextField = "Name_Community";
                            cmbSelComunity.DataValueField = "cod_Community";
                            cmbSelComunity.DataBind();
                        }
                        dtbarrio = null;
                    }
                    else
                    {
                        cmbSelComunity.Enabled = false;
                        cmbSelComunity.CssClass = "divipol";
                        cmbSelComunity.Items.Clear();
                    }

                }
            }

            dt = null;
            oescountry = null;

        }
        protected void cmbSelProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSelDistrict.CssClass = null;
            cmbSelComunity.CssClass = null;
            cmbSelDistrict.Enabled = true;
            cmbSelComunity.Enabled = true;
            cmbSelDistrict.Items.Clear();
            cmbSelComunity.Items.Clear();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", cmbSelCountry.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }

            if (oescountry.CountryDistrito == false)
            {
                cmbSelDistrict.Enabled = false;
                cmbSelDistrict.CssClass = "divipol";
                cmbSelDistrict.Items.Clear();
            }
            if (oescountry.CountryBarrio == false)
            {
                cmbSelComunity.Enabled = false;
                cmbSelComunity.CssClass = "divipol";
                cmbSelComunity.Items.Clear();
            }


            if (oescountry.CountryDistrito == true)
            {
                cmbSelDistrict.Enabled = true;
                cmbSelDistrict.CssClass = null;
                DataTable dtdistrito = new DataTable();
                dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITO", cmbSelProvince.Text);
                if (dtdistrito.Rows.Count > 1)
                {
                    cmbSelDistrict.DataSource = dtdistrito;
                    cmbSelDistrict.DataTextField = "Name_Local";
                    cmbSelDistrict.DataValueField = "cod_District";
                    cmbSelDistrict.DataBind();
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No es posible crear Punto de Venta para este país. Falta crear el(los) Distrito.";
                    MensajeAlerta();
                    cmbSelDistrict.DataSource = dtdistrito;
                    cmbSelDistrict.DataTextField = "Name_Local";
                    cmbSelDistrict.DataValueField = "cod_District";
                    cmbSelDistrict.DataBind();
                }
                dtdistrito = null;
            }
            else
            {
                cmbSelDistrict.Enabled = false;
                cmbSelDistrict.CssClass = "divipol";
                cmbSelDistrict.Items.Clear();
                if (oescountry.CountryBarrio == true)
                {
                    cmbSelComunity.Enabled = true;
                    cmbSelComunity.CssClass = null;
                    DataTable dtbarrio = new DataTable();

                    dtbarrio = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONCITY", cmbSelProvince.Text);
                    if (dtbarrio.Rows.Count > 1)
                    {
                        cmbSelComunity.DataSource = dtbarrio;
                        cmbSelComunity.DataTextField = "Name_Community";
                        cmbSelComunity.DataValueField = "cod_Community";
                        cmbSelComunity.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear Punto de Venta para este país. Falta crear el(los) Barrio.";
                        MensajeAlerta();
                        cmbSelComunity.DataSource = dtbarrio;
                        cmbSelComunity.DataTextField = "Name_Community";
                        cmbSelComunity.DataValueField = "cod_Community";
                        cmbSelComunity.DataBind();
                    }
                    dtbarrio = null;
                }
                else
                {
                    cmbSelComunity.Enabled = false;
                    cmbSelComunity.CssClass = "divipol";
                    cmbSelComunity.Items.Clear();
                }
            }

            dt = null;
            oescountry = null;

        }
        protected void cmbSelDistrict_SelectedIndexChanged1(object sender, EventArgs e)
        {
            cmbSelComunity.CssClass = null;
            cmbSelComunity.Enabled = true;
            cmbSelComunity.Items.Clear();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", cmbSelCountry.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }
            if (oescountry.CountryBarrio == false)
            {
                cmbSelComunity.Enabled = false;
                cmbSelComunity.CssClass = "divipol";
                cmbSelComunity.Items.Clear();
            }
            else
            {
                cmbSelComunity.Enabled = true;
                cmbSelComunity.CssClass = null;
                comboComunidad();
            }

        }
        private void comboComunidad()
        {
            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEB_COMBOCOMUNITY", cmbSelCountry.SelectedValue, cmbSelDpto.SelectedValue, cmbSelProvince.SelectedValue, cmbSelDistrict.SelectedValue);
            if (dt.Rows.Count > 1)
            {
                cmbSelComunity.DataSource = dt;
                cmbSelComunity.DataTextField = "Name_Community";
                cmbSelComunity.DataValueField = "cod_Community";
                cmbSelComunity.DataBind();
            }
            else
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "No es posible crear Punto de venta para este país. Falta crear el(los) Distritos.";
                MensajeAlerta();
                cmbSelComunity.DataSource = dt;
                cmbSelComunity.DataTextField = "Name_Community";
                cmbSelComunity.DataValueField = "cod_Community";
                cmbSelComunity.DataBind();
            }
            dt = null;
        }
        protected void btnsavePos_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNumdocPDV.Text = TxtNumdocPDV.Text.TrimStart();
            TxtRSocPos.Text = TxtRSocPos.Text.TrimStart();
            TxtNComPos.Text = TxtNComPos.Text.TrimStart();
            TxtDirPos.Text = TxtDirPos.Text.TrimStart();
            TxtNumdocPDV.Text = TxtNumdocPDV.Text.TrimEnd();
            TxtRSocPos.Text = TxtRSocPos.Text.TrimEnd();
            TxtNComPos.Text = TxtNComPos.Text.TrimEnd();
            TxtDirPos.Text = TxtDirPos.Text.TrimEnd();

            if (cmbTipDocPDV.Text == "0" || TxtNumdocPDV.Text == "" || TxtRSocPos.Text == "" || TxtNComPos.Text == "" ||
                cmbSelCountry.Text == "0" || TxtDirPos.Text == "" || cmbSelCanal.Text == "0" || CmbTipMerc.Text == "0" || cmbNodoCom.Text == "0" || cmbNodoCom.Text == "" ||
                CmbSelSegPDV.Text == "0")
            {

                if (cmbTipDocPDV.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo de documento";
                }
                if (TxtNumdocPDV.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Número de documento";
                }
                if (TxtRSocPos.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Razón Social";
                }
                if (TxtNComPos.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre comercial";
                }
                if (cmbSelCountry.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "País";
                }
                if (TxtDirPos.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Dirección";
                }
                if (cmbSelCanal.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                if (CmbTipMerc.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo Agrupación Comercial";
                }
                if (cmbNodoCom.Text == "0" || cmbNodoCom.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Agrupación Comercial";
                }
                if (CmbSelSegPDV.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Segmento";
                }


                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            if (cmbSelDpto.Items.Count > 0 && cmbSelDpto.Text == "0")
            {
                this.Session["mensajealert"] = " Departamento";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Departamento";
                MensajeAlerta();
                return;
            }
            if (cmbSelProvince.Items.Count > 0 && cmbSelProvince.Text == "0")
            {
                this.Session["mensajealert"] = " Ciudad/Provincia";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta fatla Ciudad/Provincia";
                MensajeAlerta();
                return;
            }
            if (cmbSelDistrict.Items.Count > 0 && cmbSelDistrict.Text == "0")
            {
                this.Session["mensajealert"] = " Distrito";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Distrito";
                MensajeAlerta();
                return;
            }
            if (cmbSelComunity.Items.Count > 0 && cmbSelComunity.Text == "0")
            {
                this.Session["mensajealert"] = " Barrio";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Barrio";
                MensajeAlerta();
                return;
            }
            //if (cmbSelCanal.Text == "1023" && cmbDexpdv.Text == "0")
            //{
            //    Alertas.CssClass = "MensajesError";
            //    LblFaltantes.Text = "En Canal Minorista el campo Disbtribuidora es Obligatorio";
            //    MensajeAlerta();
            //    return;
            //}


            try
            {
                RUC = Convert.ToInt32(TxtNumdocPDV.Text);
            }
            catch
            {
                RUC = 1;
            }


            if (RUC == 0)
            {
                Alertas.CssClass = "MensajesCorrecto";
                LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                MensajeAlerta();
                return;
            }

            try
            {

                DAplicacion odconsupdv = new DAplicacion();
                DataTable dtconsulta1 = odconsupdv.ConsultaDuplicados(ConfigurationManager.AppSettings["PDVdoc"], TxtNumdocPDV.Text, null, null);
                if (dtconsulta1 != null)
                {
                    if (TxtRSocPos.Text == dtconsulta1.Rows[0]["pdv_RazónSocial"].ToString().Trim())
                    {
                        DataTable dtconsulta = odconsupdv.ConsultaDuplicados(ConfigurationManager.AppSettings["PDV"], "0", TxtNComPos.Text, TxtDirPos.Text);
                        if (dtconsulta == null)
                        {
                            // se quita TxtCodPOSC.Text ya que el campo fue eliminado de la tabla y pasa a ser de la tabla PointOfSale_Client . Ing. Mauricio Ortiz
                            EPuntosDV oePuntosDV = oPDV.RegistrarPDV(cmbTipDocPDV.SelectedValue, TxtNumdocPDV.Text, TxtcontacPos.Text, TxtCargo1.Text, TxtcontacPos2.Text, TxtCargo2.Text, TxtRSocPos.Text, TxtNComPos.Text, TxtTelPos.Text, TxtanexPos.Text, TxtFaxPos.Text,
                                cmbSelCountry.SelectedValue, cmbSelDpto.SelectedValue, cmbSelProvince.SelectedValue, cmbSelDistrict.SelectedValue, cmbSelComunity.SelectedValue, TxtDirPos.Text,
                                TxtMailPos.Text, cmbSelCanal.SelectedValue, Convert.ToInt32(CmbTipMerc.SelectedValue), cmbNodoCom.SelectedValue, Convert.ToInt32(CmbSelSegPDV.SelectedValue),  estado, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            DataTable dt= oPDV.ObtenerCanedatmp(cmbNodoCom.SelectedValue);
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    EPuntosDV oeCadenaTMP = oPDV.Actualizar_Cadena(cmbSelCanal.SelectedValue,  cmbNodoCom.SelectedValue);
                                }
                            }
                            else
                            {

                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El Nodo Comercial " + " Ya Existe";
                                MensajeAlerta();
                            }

                            

                            string sPDV = "";
                            sPDV = TxtRSocPos.Text + "-" + TxtNComPos.Text;
                            this.Session["sPDV"] = sPDV;
                            SavelimpiarcontrolesPDV();
                            //llenarcombos();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "El Punto de venta " + this.Session["sPDV"] + " fue creado con Exito";
                            MensajeAlerta();
                            saveActivarbotonesPDV();
                            desactivarControlePDV();
                        }
                        else
                        {

                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Punto de venta " + this.Session["sPDV"] + " Ya Existe";
                            MensajeAlerta();
                        }
                    }
                    else
                    {
                        string sPDV = "";
                        sPDV = TxtNumdocPDV.Text;
                        this.Session["sPDV"] = sPDV;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "Debe cambiar el documento " + this.Session["sPDV"] + " ya que esta asignado a la razon social : " + dtconsulta1.Rows[0]["pdv_RazónSocial"].ToString().Trim().ToUpper() + " Ya Existe";
                        MensajeAlerta();
                        return;
                    }
                }
                else
                {
                    DataTable dtconsulta = odconsupdv.ConsultaDuplicados(ConfigurationManager.AppSettings["PDV"], "0", TxtNComPos.Text, TxtDirPos.Text);
                    if (dtconsulta == null)
                    {
                        // se quita TxtCodPOSC.Text ya que el campo fue eliminado de la tabla y pasa a ser de la tabla PointOfSale_Client . Ing. Mauricio Ortiz
                        EPuntosDV oePuntosDV = oPDV.RegistrarPDV(cmbTipDocPDV.SelectedValue, TxtNumdocPDV.Text, TxtcontacPos.Text, TxtCargo1.Text, TxtcontacPos2.Text, TxtCargo2.Text, TxtRSocPos.Text, TxtNComPos.Text, TxtTelPos.Text, TxtanexPos.Text, TxtFaxPos.Text,
                            cmbSelCountry.SelectedValue, cmbSelDpto.SelectedValue, cmbSelProvince.SelectedValue, cmbSelDistrict.SelectedValue, cmbSelComunity.SelectedValue, TxtDirPos.Text,
                            TxtMailPos.Text, cmbSelCanal.SelectedValue, Convert.ToInt32(CmbTipMerc.SelectedValue), cmbNodoCom.SelectedValue, Convert.ToInt32(CmbSelSegPDV.SelectedValue), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        
                        DataTable dt = oPDV.ObtenerCanedatmp(cmbNodoCom.SelectedValue);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                EPuntosDV oeCadenaTMP = oPDV.Actualizar_Cadena(cmbSelCanal.SelectedValue, cmbNodoCom.SelectedValue);
                            }
                        }
                        else
                        {

                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Nodo Comercial " + " Ya Existe";
                            MensajeAlerta();
                        }

                        string sPDV = "";
                        sPDV = TxtRSocPos.Text + "-" + TxtNComPos.Text;
                        this.Session["sPDV"] = sPDV;
                        SavelimpiarcontrolesPDV();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Punto de venta " + this.Session["sPDV"] + " fue creado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesPDV();
                        desactivarControlePDV();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear el punto de venta: " + TxtNComPos.Text.ToUpper() + " Por favor verifique que el código , el nombre comercial y/o la dirección del punto de venta sean correctos" + " Ya Existe";
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
        private void MostrarDatosPDV()
        {
            recorrido = (DataTable)this.Session["tPDV"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);

                    TxtCodPos.Text = recorrido.Rows[recsearch]["id_PointOfsale"].ToString().Trim();
                    cmbTipDocPDV.Text = recorrido.Rows[recsearch]["id_typeDocument"].ToString().Trim();
                    TxtNumdocPDV.Text = recorrido.Rows[recsearch]["pdv_RegTax"].ToString().Trim();
                    TxtcontacPos.Text = recorrido.Rows[recsearch]["pdv_contact1"].ToString().Trim();
                    TxtCargo1.Text = recorrido.Rows[recsearch]["pdv_position1"].ToString().Trim();
                    TxtcontacPos2.Text = recorrido.Rows[recsearch]["pdv_contact2"].ToString().Trim();
                    TxtCargo2.Text = recorrido.Rows[recsearch]["pdv_position2"].ToString().Trim();
                    TxtRSocPos.Text = recorrido.Rows[recsearch]["pdv_RazónSocial"].ToString().Trim();
                    TxtNComPos.Text = recorrido.Rows[recsearch]["pdv_Name"].ToString().Trim();
                    TxtTelPos.Text = recorrido.Rows[recsearch]["pdv_Phone"].ToString().Trim();
                    TxtanexPos.Text = recorrido.Rows[recsearch]["pdv_Anexo"].ToString().Trim();
                    TxtFaxPos.Text = recorrido.Rows[recsearch]["pdv_Fax"].ToString().Trim();
                    cmbSelCountry.Text = recorrido.Rows[recsearch]["pdv_codCountry"].ToString().Trim();
                    cmbSelDpto.CssClass = null;
                    cmbSelProvince.CssClass = null;
                    cmbSelDistrict.CssClass = null;
                    cmbSelComunity.CssClass = null;
                    cmbSelDpto.Items.Clear();
                    cmbSelProvince.Items.Clear();
                    cmbSelDistrict.Items.Clear();
                    cmbSelComunity.Items.Clear();

                    DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", cmbSelCountry.Text);
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
                        cmbSelDpto.CssClass = null;
                        DataTable dtcountry = new DataTable();
                        dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", cmbSelCountry.Text);
                        if (dtcountry != null)
                        {
                            if (dtcountry.Rows.Count > 1)
                            {
                                cmbSelDpto.DataSource = dtcountry;
                                cmbSelDpto.DataTextField = "Name_dpto";
                                cmbSelDpto.DataValueField = "cod_dpto";
                                cmbSelDpto.DataBind();
                                cmbSelDpto.Text = recorrido.Rows[recsearch]["pdv_codDepartment"].ToString().Trim();
                            }
                        }
                        dtcountry = null;
                    }
                    else
                    {
                        cmbSelDpto.CssClass = "divipol";
                        cmbSelDpto.Items.Clear();
                    }


                    if (cmbSelDpto.Text != "")
                    {
                        if (oescountry.CountryCiudad == true)
                        {
                            DataTable dtcity = new DataTable();
                            dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITY", cmbSelDpto.Text);
                            if (dtcity.Rows.Count > 1)
                            {
                                cmbSelProvince.DataSource = dtcity;
                                cmbSelProvince.DataTextField = "Name_City";
                                cmbSelProvince.DataValueField = "cod_City";
                                cmbSelProvince.DataBind();
                                dtcity = null;
                                cmbSelProvince.Text = recorrido.Rows[recsearch]["pdv_codProvince"].ToString().Trim();
                            }
                            dtcity = null;
                        }
                        else
                        {
                            cmbSelProvince.CssClass = "divipol";
                            cmbSelProvince.Items.Clear();
                        }
                    }
                    else
                    {
                        if (oescountry.CountryCiudad == true)
                        {
                            DataTable dtcity = new DataTable();
                            dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITYCONPAIS", cmbSelCountry.Text);
                            if (dtcity.Rows.Count > 1)
                            {
                                cmbSelProvince.DataSource = dtcity;
                                cmbSelProvince.DataTextField = "Name_City";
                                cmbSelProvince.DataValueField = "cod_City";
                                cmbSelProvince.DataBind();
                                dtcity = null;
                                cmbSelProvince.Text = recorrido.Rows[recsearch]["pdv_codProvince"].ToString().Trim();
                            }
                            dtcity = null;
                        }
                        else
                        {
                            cmbSelProvince.CssClass = "divipol";
                            cmbSelProvince.Items.Clear();
                        }
                    }

                    if (cmbSelProvince.Text != "")
                    {
                        if (oescountry.CountryDistrito == true)
                        {
                            DataTable dtdistrito = new DataTable();
                            dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITO", cmbSelProvince.Text);
                            if (dtdistrito.Rows.Count > 1)
                            {
                                cmbSelDistrict.DataSource = dtdistrito;
                                cmbSelDistrict.DataTextField = "Name_Local";
                                cmbSelDistrict.DataValueField = "cod_District";
                                cmbSelDistrict.DataBind();
                                cmbSelDistrict.Text = recorrido.Rows[recsearch]["pdv_codDistrict"].ToString().Trim();
                            }
                            dtdistrito = null;
                        }
                        else
                        {
                            cmbSelDistrict.CssClass = "divipol";
                            cmbSelDistrict.Items.Clear();
                        }
                    }
                    else
                    {
                        if (cmbSelDpto.Text != "")
                        {
                            DataTable dtdistrito = new DataTable();
                            dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONDEPTO", cmbSelDpto.Text);
                            if (dtdistrito.Rows.Count > 1)
                            {
                                cmbSelDistrict.DataSource = dtdistrito;
                                cmbSelDistrict.DataTextField = "Name_Local";
                                cmbSelDistrict.DataValueField = "cod_District";
                                cmbSelDistrict.DataBind();
                                cmbSelDistrict.Text = recorrido.Rows[recsearch]["pdv_codDistrict"].ToString().Trim();
                            }
                            dtdistrito = null;
                        }
                        else
                        {
                            DataTable dtdistrito = new DataTable();
                            dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONPAIS", cmbSelCountry.Text);
                            if (dtdistrito.Rows.Count > 1)
                            {
                                cmbSelDistrict.DataSource = dtdistrito;
                                cmbSelDistrict.DataTextField = "Name_Local";
                                cmbSelDistrict.DataValueField = "cod_District";
                                cmbSelDistrict.DataBind();
                                cmbSelDistrict.Text = recorrido.Rows[recsearch]["pdv_codDistrict"].ToString().Trim();
                            }
                            dtdistrito = null;
                        }

                    }

                    DataTable dtComunity = new DataTable();
                    dtComunity = oConn.ejecutarDataTable("UP_WEB_COMBOCOMUNITY", cmbSelCountry.SelectedValue, cmbSelDpto.SelectedValue, cmbSelProvince.SelectedValue, cmbSelDistrict.SelectedValue);
                    if (dtComunity.Rows.Count > 1)
                    {
                        cmbSelComunity.DataSource = dtComunity;
                        cmbSelComunity.DataTextField = "Name_Community";
                        cmbSelComunity.DataValueField = "cod_Community";
                        cmbSelComunity.DataBind();
                    }
                    else
                    {
                        cmbSelComunity.CssClass = "divipol";
                        cmbSelComunity.Items.Clear();
                    }
                    dtComunity = null;

                    dt = null;
                    oescountry = null;
                    cmbSelComunity.Text = recorrido.Rows[recsearch]["pdv_codCommunity"].ToString().Trim();
                    TxtDirPos.Text = recorrido.Rows[recsearch]["pdv_Address"].ToString().Trim();
                    TxtMailPos.Text = recorrido.Rows[recsearch]["pdv_email"].ToString().Trim();
                    cmbSelCanal.Text = recorrido.Rows[recsearch]["pdv_codChannel"].ToString().Trim();
                    comboTipoMercado();
                    CmbTipMerc.Text = recorrido.Rows[recsearch]["idNodeComType"].ToString().Trim();
                    comboNodos();
                    try
                    {
                        cmbNodoCom.Text = recorrido.Rows[recsearch]["NodeCommercial"].ToString().Trim();
                    }
                    catch
                    {
                        cmbNodoCom.Items.Clear();
                    }
                    CmbSelSegPDV.Text = recorrido.Rows[recsearch]["id_Segment"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["pdv_status"].ToString().Trim());





                    if (estado == true)
                    {
                        RBtnListStatusPos.Items[0].Selected = true;
                        RBtnListStatusPos.Items[1].Selected = false;
                    }
                    else
                    {
                        RBtnListStatusPos.Items[0].Selected = false;
                        RBtnListStatusPos.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void BtnBuscarPdv_Click(object sender, EventArgs e)
        {
            desactivarControlePDV();
            LblFaltantes.Text = "";
            txtbidpdv.Text = txtbidpdv.Text.TrimStart();
            Txtbnompdv.Text = Txtbnompdv.Text.TrimStart();


            if (cmbbProvpdv.Text == "0" && txtbidpdv.Text == "" && Txtbnompdv.Text == "" && cmbCanalBPDV.Text == "0")
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "RUC/NIT, Canal, Provincia y/o nombre de punto de venta";
                MensajeAlerta();
                IbtnPdv_ModalPopupExtender.Show();
                return;
            }

            try
            {
                RUC = Convert.ToInt32(txtbidpdv.Text);
            }
            catch
            {
                RUC = 1;
            }

            if (RUC == 0)
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                MensajeAlerta();
                return;
            }

            BuscarActivarbotnesPDV();
            //btnCrearPos.Visible = true;
            sPdvCodProvince = cmbbProvpdv.Text;
            sPdvRegTax = txtbidpdv.Text;
            sPdvName = Txtbnompdv.Text;
            sPdvChannel = cmbCanalBPDV.Text;
            cmbbProvpdv.Text = "0";
            txtbidpdv.Text = "";
            Txtbnompdv.Text = "";
            cmbCanalBPDV.Text = "0";
            DataTable oePDV = oPDV.BuscarPDV(sPdvCodProvince, sPdvRegTax, sPdvName, sPdvChannel);

            if (oePDV != null)
            {
                if (oePDV.Rows.Count > 0)
                {
              
                    TxtCodPos.Text = oePDV.Rows[0]["id_PointOfsale"].ToString().Trim();
                    cmbTipDocPDV.Text = oePDV.Rows[0]["id_typeDocument"].ToString().Trim();
                    TxtNumdocPDV.Text = oePDV.Rows[0]["pdv_RegTax"].ToString().Trim();
                    TxtcontacPos.Text = oePDV.Rows[0]["pdv_contact1"].ToString().Trim();
                    TxtCargo1.Text = oePDV.Rows[0]["pdv_position1"].ToString().Trim();
                    TxtcontacPos2.Text = oePDV.Rows[0]["pdv_contact2"].ToString().Trim();
                    TxtCargo2.Text = oePDV.Rows[0]["pdv_position2"].ToString().Trim();
                    TxtRSocPos.Text = oePDV.Rows[0]["pdv_RazónSocial"].ToString().Trim();
                    TxtNComPos.Text = oePDV.Rows[0]["pdv_Name"].ToString().Trim();
                    TxtTelPos.Text = oePDV.Rows[0]["pdv_Phone"].ToString().Trim();
                    TxtanexPos.Text = oePDV.Rows[0]["pdv_Anexo"].ToString().Trim();
                    TxtFaxPos.Text = oePDV.Rows[0]["pdv_Fax"].ToString().Trim();
                    comboDoc();
                    llenaPaisPDV();
                    comboCanales();
                    comboTipoMercado();
                    combosegmenpdv();
                    cmbSelCountry.Text = oePDV.Rows[0]["pdv_codCountry"].ToString().Trim();
                    cmbSelDpto.CssClass = null;
                    cmbSelProvince.CssClass = null;
                    cmbSelDistrict.CssClass = null;
                    cmbSelComunity.CssClass = null;
                    cmbSelDpto.Items.Clear();
                    cmbSelProvince.Items.Clear();
                    cmbSelDistrict.Items.Clear();
                    cmbSelComunity.Items.Clear();

                    DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", cmbSelCountry.Text);
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
                        cmbSelDpto.CssClass = null;
                        DataTable dtcountry = new DataTable();
                        dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", cmbSelCountry.Text);
                        if (dtcountry != null)
                        {
                            if (dtcountry.Rows.Count > 1)
                            {
                                cmbSelDpto.DataSource = dtcountry;
                                cmbSelDpto.DataTextField = "Name_dpto";
                                cmbSelDpto.DataValueField = "cod_dpto";
                                cmbSelDpto.DataBind();
                                cmbSelDpto.Text = oePDV.Rows[0]["pdv_codDepartment"].ToString().Trim();
                            }
                        }
                        dtcountry = null;
                    }
                    else
                    {
                        cmbSelDpto.CssClass = "divipol";
                        cmbSelDpto.Items.Clear();
                    }


                    if (cmbSelDpto.Text != "")
                    {
                        if (oescountry.CountryCiudad == true)
                        {
                            DataTable dtcity = new DataTable();
                            dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITY", cmbSelDpto.Text);
                            if (dtcity.Rows.Count > 1)
                            {
                                cmbSelProvince.DataSource = dtcity;
                                cmbSelProvince.DataTextField = "Name_City";
                                cmbSelProvince.DataValueField = "cod_City";
                                cmbSelProvince.DataBind();
                                dtcity = null;
                                cmbSelProvince.Text = oePDV.Rows[0]["pdv_codProvince"].ToString().Trim();
                            }
                            dtcity = null;
                        }
                        else
                        {
                            cmbSelProvince.CssClass = "divipol";
                            cmbSelProvince.Items.Clear();
                        }
                    }
                    else
                    {
                        if (oescountry.CountryCiudad == true)
                        {
                            DataTable dtcity = new DataTable();
                            dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITYCONPAIS", cmbSelCountry.Text);
                            if (dtcity.Rows.Count > 1)
                            {
                                cmbSelProvince.DataSource = dtcity;
                                cmbSelProvince.DataTextField = "Name_City";
                                cmbSelProvince.DataValueField = "cod_City";
                                cmbSelProvince.DataBind();
                                dtcity = null;
                                cmbSelProvince.Text = oePDV.Rows[0]["pdv_codProvince"].ToString().Trim();
                            }
                            dtcity = null;
                        }
                        else
                        {
                            cmbSelProvince.CssClass = "divipol";
                            cmbSelProvince.Items.Clear();
                        }
                    }

                    if (cmbSelProvince.Text != "")
                    {
                        if (oescountry.CountryDistrito == true)
                        {
                            DataTable dtdistrito = new DataTable();
                            dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITO", cmbSelProvince.Text);
                            if (dtdistrito.Rows.Count > 1)
                            {
                                cmbSelDistrict.DataSource = dtdistrito;
                                cmbSelDistrict.DataTextField = "Name_Local";
                                cmbSelDistrict.DataValueField = "cod_District";
                                cmbSelDistrict.DataBind();
                                cmbSelDistrict.Text = oePDV.Rows[0]["pdv_codDistrict"].ToString().Trim();
                            }
                            dtdistrito = null;
                        }
                        else
                        {
                            cmbSelDistrict.CssClass = "divipol";
                            cmbSelDistrict.Items.Clear();
                        }
                    }
                    else
                    {
                        if (cmbSelDpto.Text != "")
                        {
                            DataTable dtdistrito = new DataTable();
                            dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONDEPTO", cmbSelDpto.Text);
                            if (dtdistrito.Rows.Count > 1)
                            {
                                cmbSelDistrict.DataSource = dtdistrito;
                                cmbSelDistrict.DataTextField = "Name_Local";
                                cmbSelDistrict.DataValueField = "cod_District";
                                cmbSelDistrict.DataBind();
                                cmbSelDistrict.Text = oePDV.Rows[0]["pdv_codDistrict"].ToString().Trim();
                            }
                            dtdistrito = null;
                        }
                        else
                        {
                            DataTable dtdistrito = new DataTable();
                            dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONPAIS", cmbSelCountry.Text);
                            if (dtdistrito.Rows.Count > 1)
                            {
                                cmbSelDistrict.DataSource = dtdistrito;
                                cmbSelDistrict.DataTextField = "Name_Local";
                                cmbSelDistrict.DataValueField = "cod_District";
                                cmbSelDistrict.DataBind();
                                cmbSelDistrict.Text = oePDV.Rows[0]["pdv_codDistrict"].ToString().Trim();
                            }
                            dtdistrito = null;
                        }

                    }

                    DataTable dtComunity = new DataTable();
                    dtComunity = oConn.ejecutarDataTable("UP_WEB_COMBOCOMUNITY", cmbSelCountry.SelectedValue, cmbSelDpto.SelectedValue, cmbSelProvince.SelectedValue, cmbSelDistrict.SelectedValue);
                    if (dtComunity.Rows.Count > 1)
                    {
                        cmbSelComunity.DataSource = dtComunity;
                        cmbSelComunity.DataTextField = "Name_Community";
                        cmbSelComunity.DataValueField = "cod_Community";
                        cmbSelComunity.DataBind();
                    }
                    else
                    {
                        cmbSelComunity.CssClass = "divipol";
                        cmbSelComunity.Items.Clear();
                    }
                    dtComunity = null;

                    dt = null;
                    oescountry = null;
                    cmbSelComunity.Text = oePDV.Rows[0]["pdv_codCommunity"].ToString().Trim();
                    TxtDirPos.Text = oePDV.Rows[0]["pdv_Address"].ToString().Trim();
                    TxtMailPos.Text = oePDV.Rows[0]["pdv_email"].ToString().Trim();
                    cmbSelCanal.Text = oePDV.Rows[0]["pdv_codChannel"].ToString().Trim();
                    comboTipoMercado();
                    CmbTipMerc.Text = oePDV.Rows[0]["idNodeComType"].ToString().Trim();
                    comboNodos();
                    try
                    {
                        cmbNodoCom.Text = oePDV.Rows[0]["NodeCommercial"].ToString().Trim();
                    }
                    catch
                    {
                        cmbNodoCom.Items.Clear();
                    }
                    CmbSelSegPDV.Text = oePDV.Rows[0]["id_Segment"].ToString().Trim();
                    estado = Convert.ToBoolean(oePDV.Rows[0]["pdv_status"].ToString().Trim());

                    if (estado == true)
                    {
                        RBtnListStatusPos.Items[0].Selected = true;
                        RBtnListStatusPos.Items[1].Selected = false;
                    }
                    else
                    {
                        RBtnListStatusPos.Items[0].Selected = false;
                        RBtnListStatusPos.Items[1].Selected = true;
                    }
                    this.Session["tPDV"] = oePDV;
                    this.Session["i"] = 0;

                    if (oePDV.Rows.Count == 1)
                    {
                        btnPreg7.Visible = false;
                        btnUreg7.Visible = false;
                        btnAreg7.Visible = false;
                        btnSreg7.Visible = false;
                    }
                    else
                    {
                        btnPreg7.Visible = true;
                        btnUreg7.Visible = true;
                        btnAreg7.Visible = true;
                        btnSreg7.Visible = true;
                    }
                }
                else
                {
                    SavelimpiarcontrolesPDV();
                    saveActivarbotonesPDV();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnPdv_ModalPopupExtender.Show();
                }
            }

        }
        protected void btnEditPDV_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesPDV();
            EditarActivarControlesPDV();
            this.Session["rept1"] = TxtNComPos.Text;
            this.Session["rept2"] = TxtDirPos.Text;

        }
        protected void btnActualizarPos_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNumdocPDV.Text = TxtNumdocPDV.Text.TrimStart();
            TxtRSocPos.Text = TxtRSocPos.Text.TrimStart();
            TxtNComPos.Text = TxtNComPos.Text.TrimStart();
            TxtDirPos.Text = TxtDirPos.Text.TrimStart();
            TxtNumdocPDV.Text = TxtNumdocPDV.Text.TrimEnd();
            TxtRSocPos.Text = TxtRSocPos.Text.TrimEnd();
            TxtNComPos.Text = TxtNComPos.Text.TrimEnd();
            TxtDirPos.Text = TxtDirPos.Text.TrimEnd();

            if (cmbTipDocPDV.Text == "0" || TxtNumdocPDV.Text == "" || TxtRSocPos.Text == "" || TxtNComPos.Text == "" ||
                cmbSelCountry.Text == "0" || TxtDirPos.Text == "" || cmbSelCanal.Text == "0" || CmbTipMerc.Text == "0" || cmbNodoCom.Text == "0" || cmbNodoCom.Text == "" ||
                CmbSelSegPDV.Text == "0")
            {

                if (cmbTipDocPDV.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo de documento";
                }
                if (TxtNumdocPDV.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Número de documento";
                }
                if (TxtRSocPos.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Razón Social";
                }
                if (TxtNComPos.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre comercial";
                }
                if (cmbSelCountry.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "País";
                }
                if (TxtDirPos.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Dirección";
                }
                if (cmbSelCanal.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                if (CmbTipMerc.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo Agrupación Comercial";
                }
                if (cmbNodoCom.Text == "0" || cmbNodoCom.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Agrupación Comercial";
                }
                if (CmbSelSegPDV.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Segmento";
                }


                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            if (cmbSelDpto.Items.Count > 0 && cmbSelDpto.Text == "0")
            {
                this.Session["mensajealert"] = " Departamento";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Departamento";
                MensajeAlerta();
                return;
            }
            if (cmbSelProvince.Items.Count > 0 && cmbSelProvince.Text == "0")
            {
                this.Session["mensajealert"] = " Ciudad/Provincia";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Provincia";
                MensajeAlerta();
                return;
            }
            if (cmbSelDistrict.Items.Count > 0 && cmbSelDistrict.Text == "0")
            {
                this.Session["mensajealert"] = " Distrito";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Distrito";
                MensajeAlerta();
                return;
            }
            if (cmbSelComunity.Items.Count > 0 && cmbSelComunity.Text == "0")
            {
                this.Session["mensajealert"] = " Barrio";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "División Politica incompleta falta Barrio";
                MensajeAlerta();
                return;
            }
            try
            {
                RUC = Convert.ToInt32(TxtNumdocPDV.Text);
            }
            catch
            {
                RUC = 1;
            }
            if (RUC == 0)
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                MensajeAlerta();
                return;
            }
            try
            {
                if (RBtnListStatusPos.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabPdv = new DAplicacion();
                    DataTable dt = oddeshabPdv.PermitirDeshabilitar(ConfigurationManager.AppSettings["PointOfSalePointOfSale_Planning"], TxtCodPos.Text);
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
               if (repetido1 != TxtNComPos.Text || repetido != TxtDirPos.Text)
                {

                    //if (repetido1 == TxtNComPos.Text)
                    //{
                    //    repetido1 = "";
                    //}
                    //else
                    //{
                    //    repetido1 = TxtNComPos.Text;
                    //}
                    //if (repetido == TxtDirPos.Text)
                    //{
                    //    repetido = "";
                    //}
                    //else
                    //{
                    //    repetido = TxtDirPos.Text;
                    //}
                    DAplicacion odconsupdv = new DAplicacion();
                    DataTable dtconsulta = null;
                        //odconsupdv.ConsultaDuplicados(ConfigurationManager.AppSettings["PDV"], repetido, repetido1, null); Se debe cambiar esta logica para evaluar duplicados en actualizacion
                        //Ing. Carlos Hernandez 22/03/2012
                    if (dtconsulta == null)
                    {
                        // 25/08/2010 se quita TxtCodPOSC.Text ya que en la tabla ya no existe este campo , se paso para la tabla PointOfSale_Client . Ing. Mauricio Ortiz

                        EPuntosDV oePuntosDV = oPDV.Actualizar_PDV(Convert.ToInt32(TxtCodPos.Text), cmbTipDocPDV.SelectedValue, TxtNumdocPDV.Text, TxtcontacPos.Text, TxtCargo1.Text, TxtcontacPos2.Text, TxtCargo2.Text,
                            TxtRSocPos.Text, TxtNComPos.Text, TxtTelPos.Text, TxtanexPos.Text, TxtFaxPos.Text, cmbSelCountry.SelectedValue, cmbSelDpto.SelectedValue, cmbSelProvince.SelectedValue, cmbSelDistrict.SelectedValue,
                            cmbSelComunity.SelectedValue, TxtDirPos.Text, TxtMailPos.Text, cmbSelCanal.SelectedValue, Convert.ToInt32(CmbTipMerc.SelectedValue), cmbNodoCom.SelectedValue, Convert.ToInt32(CmbSelSegPDV.SelectedValue),  estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        EPuntosDV oenodePuntosdv = oPDV.Actualizar_NodePlaPDV(Convert.ToInt32(TxtCodPos.Text),cmbSelProvince.SelectedValue, Convert.ToInt32(CmbTipMerc.SelectedValue), cmbNodoCom.SelectedValue);

                        string sPDV = "";
                        sPDV = TxtRSocPos.Text;
                        this.Session["sPDV"] = sPDV;
                        SavelimpiarcontrolesPDV();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Punto de venta : " + this.Session["sPDV"] + " Se Actualizo Corecctamente";
                        MensajeAlerta();
                        saveActivarbotonesPDV();
                        desactivarControlePDV();
                    }
                    else
                    {
                        string sPDV = "";
                        sPDV = TxtRSocPos.Text;
                        this.Session["sPDV"] = sPDV;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Punto de venta : " + this.Session["sPDV"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    // 25/08/2010 se quita TxtCodPOSC.Text ya que en la tabla ya no existe este campo , se paso para la tabla PointOfSale_Client . Ing. Mauricio Ortiz
                    EPuntosDV oePuntosDV = oPDV.Actualizar_PDV(Convert.ToInt32(TxtCodPos.Text), cmbTipDocPDV.SelectedValue, TxtNumdocPDV.Text, TxtcontacPos.Text, TxtCargo1.Text, TxtcontacPos2.Text, TxtCargo2.Text,
                            TxtRSocPos.Text, TxtNComPos.Text, TxtTelPos.Text, TxtanexPos.Text, TxtFaxPos.Text, cmbSelCountry.SelectedValue, cmbSelDpto.SelectedValue, cmbSelProvince.SelectedValue, cmbSelDistrict.SelectedValue,
                            cmbSelComunity.SelectedValue, TxtDirPos.Text, TxtMailPos.Text, cmbSelCanal.SelectedValue, Convert.ToInt32(CmbTipMerc.SelectedValue), cmbNodoCom.SelectedValue, Convert.ToInt32(CmbSelSegPDV.SelectedValue),  estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    EPuntosDV oenodePuntosdv = oPDV.Actualizar_NodePlaPDV(Convert.ToInt32(TxtCodPos.Text), cmbSelProvince.SelectedValue, Convert.ToInt32(CmbTipMerc.SelectedValue), cmbNodoCom.SelectedValue);


                    string sPDV = "";
                    sPDV = TxtRSocPos.Text;
                    this.Session["sPDV"] = sPDV;
                    SavelimpiarcontrolesPDV();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Punto de venta : " + this.Session["sPDV"] + " Se Actualizo Corecctamente";
                    MensajeAlerta();
                    saveActivarbotonesPDV();
                    desactivarControlePDV();
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
        protected void btnCancelPos_Click(object sender, EventArgs e)
        {
            SavelimpiarcontrolesPDV();
            saveActivarbotonesPDV();
            desactivarControlePDV();

        }
        protected void btnPreg7_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tPDV"];
            MostrarDatosPDV();
        }
        protected void btnAreg7_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosPDV();
            }
        }
        protected void btnSreg7_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tPDV"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosPDV();
            }
        }
        protected void btnUreg7_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tPDV"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosPDV();
        }
        #endregion

        #region Eventos PDV vs Cliente
        protected void BtnCrearPDVC_Click(object sender, EventArgs e)
        {
            LLenaComboPaisPDVC();
            crearActivarbotonesPDVCli();
            activarControlesPDVCli();
            GVPDVConsulta.DataBind();
        }

        protected void btnCargamasivaPDVC_Click(object sender, System.EventArgs e)
        {
            this.Session["TipoCarga"] = "PtoVentaCliente";
            Iframe1.Attributes["src"] = "carga_masiva.aspx";
            ModalPopupExtender1.Show();
        }
        protected void CmbPaísPDVC_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboclienteenPDVCli();
            CmbCanalPDVC.Items.Clear();
            cmbTACPDVC.Items.Clear();
            cmbAgruCPDVC.Items.Clear();
            GvPDV.DataBind();
        }
        protected void cmbClientePDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LLenaComboCanalxPaisPDVC();
            cmbTACPDVC.Items.Clear();
            cmbAgruCPDVC.Items.Clear();
            GvPDV.DataBind();
        }
        protected void CmbCanalPDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LLenaComboTACxCanalPDVC();
            cmbAgruCPDVC.Items.Clear();
            GvPDV.DataBind();
        }
        protected void cmbTACPDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LLenaComboACxTACPDVC();
            GvPDV.DataBind();
        }
        protected void cmbAgruCPDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LLenaComboPDVxACPDVC();
           // LlenacomboOficinaenPDVC();
            //LlenacomboMallasenPDVC();

        }
        protected void cmbOficinaPDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaEstadoOficinaenPDVC();
        }
        protected void cmbOficinaConsultaPDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaEstadoConsultaOficinaenPDVC();
        }
        protected void cmbMallaPDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenacomboSectorxMallaPDVC();
           
        }
        protected void cmbMallaConsultaPDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenacomboConsultaSectorxMallaPDVC();
           
        }      
        protected void cmbBPaísPDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBuscarclienteenPDVCli();
            cmbBCanalPDVC.Items.Clear();
            cmbBTipoAgrupacion.Items.Clear();
            cmbBAgrupacionC.Items.Clear();
            ModalPopupPDVCliente.Show();
        
        }
        protected void cmbBClientePDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LLenaComboBuscarCanalxPaisPDVC();
            cmbBTipoAgrupacion.Items.Clear();
            cmbBAgrupacionC.Items.Clear();
            ModalPopupPDVCliente.Show();
        }
        protected void cmbBCanalPDVC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LLenaComboBuscarTACxCanalPDVC();
            cmbBAgrupacionC.Items.Clear();
            ModalPopupPDVCliente.Show();
        }
        protected void cmbBTipoAgrupacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            LLenaComboBuscarACxTACPDVC();
            ModalPopupPDVCliente.Show();
        }
        protected void BtnGuardarPDVC_Click(object sender, EventArgs e)
        {
            bool continuar = false;
            LblFaltantes.Text = "";
            ((TextBox)GvPDV.Rows[GvPDV.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text = ((TextBox)GvPDV.Rows[GvPDV.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text.TrimStart();
            ((TextBox)GvPDV.Rows[GvPDV.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text = ((TextBox)GvPDV.Rows[GvPDV.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text.TrimEnd();
            if (cmbClientePDVC.Text == "0" || CmbPaísPDVC.Text == "0" || CmbCanalPDVC.Text == "0"
                || cmbTACPDVC.Text == "0" || cmbAgruCPDVC.Text == "0" || CmbPaísPDVC.Text == ""
                || CmbCanalPDVC.Text == "" || cmbTACPDVC.Text == "" || cmbAgruCPDVC.Text == "")
            {
                if (cmbClientePDVC.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Cliente";
                }
                if (CmbPaísPDVC.Text == "0" || CmbPaísPDVC.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "País";
                }
                if (CmbCanalPDVC.Text == "0" || CmbCanalPDVC.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                if (cmbTACPDVC.Text == "0" || cmbTACPDVC.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo de Agrupación Comercial";
                }
                if (cmbAgruCPDVC.Text == "0" || cmbAgruCPDVC.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Agrupación Comercial";
                }
                Alertas.CssClass = "MensajesError";
                MensajeAlerta();
                return;
            }

            for (int i = 0; i <= GvPDV.Rows.Count - 1; i++)
            {

                if (((CheckBox)GvPDV.Rows[i].Cells[7].FindControl("CheckBox1")).Checked != false)
                {
                    continuar = true;
                    i = GvPDV.Rows.Count - 1;
                }
            }

            if (continuar)
            {
                for (int i = 0; i <= GvPDV.Rows.Count - 1; i++)
                {
                    if (((CheckBox)GvPDV.Rows[i].Cells[7].FindControl("CheckBox1")).Checked == true)
                    {
                        if (((TextBox)GvPDV.Rows[i].Cells[2].FindControl("TxtCodPDVC")).Text == "" ||
                            ((DropDownList)GvPDV.Rows[i].Cells[3].FindControl("cmbOficinaPDVC")).Text == "0"
                            ||
                            ((DropDownList)GvPDV.Rows[i].Cells[4].FindControl("cmbMallaPDVC")).Text == "0" ||
                            ((DropDownList)GvPDV.Rows[i].Cells[5].FindControl("cmbSectorPDVC")).Text == "0")
                        {
                            i = GvPDV.Rows.Count - 1;
                            continuar = false;
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Debe ingresar Información del Punto de Venta";
                            MensajeAlerta();

                        }
                        if (CmbCanalPDVC.Text == "1023" && ((DropDownList)GvPDV.Rows[i].Cells[6].FindControl("cmbDEX")).Text == "0")
                            
                           {
                            continuar = false;
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Para Canal Minorista la Distribuidora es Obligatoria";
                            MensajeAlerta();
                            
                            }
                        else
                        {
                            continuar = true;
                        }
                    }
                }
            }
            else
            {
                continuar = false;
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar información de mínimo un Punto de Venta";
                MensajeAlerta();
            }


            if (continuar)
            {
                try
                {
                    DAplicacion odconsulPDVC = new DAplicacion();
                    for (int i = 0; i <= GvPDV.Rows.Count - 1; i++)
                    {
                        if (((CheckBox)GvPDV.Rows[i].Cells[7].FindControl("CheckBox1")).Checked == true)
                        {
                            DataTable dtconsulta = odconsulPDVC.ConsultaDuplicados(ConfigurationManager.AppSettings["PointOfSale_Client"], cmbClientePDVC.Text, GvPDV.Rows[i].Cells[0].Text, null);
                            if (dtconsulta == null)
                            {
                                continuar = true;
                            }
                            else
                            {
                                i = GvPDV.Rows.Count - 1;
                                continuar = false;

                                string sPDVCliente = "";
                                sPDVCliente = "punto de venta" + " " + cmbClientePDVC.SelectedItem.Text;
                                this.Session["sPDVCliente"] = sPDVCliente;
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "La Relación de PDV y Cliente " + this.Session["sPDVCliente"] + " Ya Existe";
                                MensajeAlerta();


                            }
                        }
                    }
                    if (continuar)
                    {
                        for (int i = 0; i <= GvPDV.Rows.Count - 1; i++)
                        {
                            if (((CheckBox)GvPDV.Rows[i].Cells[7].FindControl("CheckBox1")).Checked == true)
                            {
                                string sector;
                                if (((DropDownList)GvPDV.Rows[i].Cells[5].FindControl("cmbSectorPDVC")).Text != "No aplica")
                                {
                                    sector = ((DropDownList)GvPDV.Rows[i].Cells[5].FindControl("cmbSectorPDVC")).Text;
                                }
                                else
                                { sector = "0"; }
                                //AGREGAR SOPORTE PARA EL ALIAS
                                EPuntosDV oePDVCliente = oPDV.RegistrarClientPDV(Convert.ToInt32(cmbClientePDVC.Text), Convert.ToInt32(GvPDV.Rows[i].Cells[0].Text), ((TextBox)GvPDV.Rows[i].Cells[2].FindControl("TxtCodPDVC")).Text, Convert.ToInt32(sector), Convert.ToInt32(((DropDownList)GvPDV.Rows[i].Cells[3].FindControl("cmbOficinaPDVC")).Text), Convert.ToInt32(((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("cmbDEX")).Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now, "");
                            }
                        }
                        string sPDVCliente = "";
                        sPDVCliente = "punto de venta" + " " + cmbClientePDVC.SelectedItem.Text;
                        this.Session["sPDVCliente"] = sPDVCliente;
                        //LlenacomboMallasenSector();
                        SavelimpiarcontrolesPDVCli();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Relación de PDV y Cliente " + this.Session["sPDVCliente"] + " fue creado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesPDVCli();
                        desactivarControlesPDVCli();



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
        protected void BtnBuscarPDVCliente_Click(object sender, EventArgs e)
        {
            GvPDV.DataBind();
            desactivarControlesPDVCli();
            LblFaltantes.Text = "";
            //TxtBCodigoPDVC.Text = TxtBCodigoPDVC.Text.TrimStart();


            if (cmbBClientePDVC.Text == "0" || cmbBPaísPDVC.Text == "0" || cmbBCanalPDVC.Text == "0"
                || cmbBTipoAgrupacion.Text == "0" || cmbBAgrupacionC.Text == "0" || cmbBPaísPDVC.Text == ""
                || cmbBCanalPDVC.Text == "" || cmbBTipoAgrupacion.Text == "" || cmbBAgrupacionC.Text == "")
            {
                if (cmbBClientePDVC.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Cliente";
                }
                if (cmbBPaísPDVC.Text == "0" || cmbBPaísPDVC.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "País";
                }
                if (cmbBCanalPDVC.Text == "0" || cmbBCanalPDVC.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                if (cmbBTipoAgrupacion.Text == "0" || cmbBTipoAgrupacion.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo de Agrupación Comercial";
                }
                if (cmbBAgrupacionC.Text == "0" || cmbBAgrupacionC.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Agrupación Comercial";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese todos los parametros de consulta";
                MensajeAlerta();
                ModalPopupPDVCliente.Show();
                return;
            }
            
            LLenaComboPaisPDVC();
            CmbPaísPDVC.Text = cmbBPaísPDVC.Text;
            comboclienteenPDVCli();
            cmbClientePDVC.Text = cmbBClientePDVC.Text ;          
            LLenaComboCanalxPaisPDVC();
            CmbCanalPDVC.Text= cmbBCanalPDVC.Text ;
            LLenaComboTACxCanalPDVC();
            cmbTACPDVC.Text = cmbBTipoAgrupacion.Text;
            LLenaComboACxTACPDVC();
            cmbAgruCPDVC.Text = cmbBAgrupacionC.Text ;
             //LLenaComboPDVxACPDVC();
             LLenaPDVxConsulta();         
            //LlenacomboOficinaenPDVC();
            //LlenacomboMallasenPDVC();      
            BuscarActivarbotnesPDVCli();
            iCliente = Convert.ToInt32(cmbBClientePDVC.Text);
            spais=cmbBPaísPDVC.Text;
            sCanal=cmbBCanalPDVC.Text;
            itipoAgrupacion=Convert.ToInt32(cmbBTipoAgrupacion.Text);
            sAgrupacion = cmbBAgrupacionC.Text;

            cmbBClientePDVC.Items.Clear();
            cmbBPaísPDVC.Text = "0";
            cmbBCanalPDVC.Items.Clear();
            cmbBTipoAgrupacion.Items.Clear();
            cmbBAgrupacionC.Items.Clear();
          
           // cmbBClientePDVC.Text = "0";


            //DataTable oePDVC = oPDV.ConsultarPDVClient(iCliente, spais, sCanal, itipoAgrupacion, sAgrupacion);

            //if (oePDVC != null)
            //{
            //    if (oePDVC.Rows.Count > 0)
            //    {
            //        GvPDV.Enabled = false;
                    
            //        //for (int j = 0; j <= oePDVC.Rows.Count - 1; j++)
                  
            //        //{
            //            //for (int i = 0; i <= GvPDV.Rows.Count - 1; i++)
                   
            //            //{
            //            //    if (GvPDV.Rows[i].Cells[0].Text == oePDVC.Rows[j]["id_PointOfsale"].ToString().Trim())
            //            //    {
            //            //        this.Session["id_ClientPDV"] = Convert.ToInt64(oePDVC.Rows[j]["id_ClientPDV"].ToString().Trim());
            //            //        iid_ClientPDV = Convert.ToInt32(this.Session["id_ClientPDV"]);
            //            //        ((TextBox)GvPDV.Rows[i].Cells[2].FindControl("TxtCodPDVC")).Text = oePDVC.Rows[j]["ClientPDV_Code"].ToString().Trim();
            //            //        //LlenacomboOficinaenPDVC();
            //            //        ((DropDownList)GvPDV.Rows[i].Cells[3].FindControl("cmbOficinaPDVC")).Text = oePDVC.Rows[j]["cod_Oficina"].ToString().Trim();
            //            //        //LlenacomboMallasenPDVC();
            //            //        if (oePDVC.Rows[j]["id_malla"].ToString().Trim() == "")
            //            //        {
            //            //            ((DropDownList)GvPDV.Rows[i].Cells[4].FindControl("cmbMallaPDVC")).Text = "No aplica";
            //            //        }
            //            //        else
            //            //        {
            //            //            ((DropDownList)GvPDV.Rows[i].Cells[4].FindControl("cmbMallaPDVC")).Text = oePDVC.Rows[j]["id_malla"].ToString().Trim();
            //            //        }
            //            //        LlenacomboSectorxMallaPDVC();
            //            //        if (oePDVC.Rows[j]["id_sector"].ToString().Trim() == "0")
            //            //        {
            //            //            ((DropDownList)GvPDV.Rows[i].Cells[5].FindControl("cmbSectorPDVC")).Text = "No aplica";
            //            //        }
            //            //        else
            //            //        {
            //            //            ((DropDownList)GvPDV.Rows[i].Cells[5].FindControl("cmbSectorPDVC")).Text = oePDVC.Rows[j]["id_sector"].ToString().Trim();
            //            //        }
                                
            //            //        ((CheckBox)GvPDV.Rows[i].Cells[6].FindControl("CheckBox1")).Checked = Convert.ToBoolean(oePDVC.Rows[j]["ClientPDV_Status"].ToString().Trim());
                                
            //            //      i = GvPDV.Rows.Count - 1; 
            //            //    }
            //            //}
            //        //}
            //    }
            //    else
            //    {
            //        SavelimpiarcontrolesPDVCli();
            //        saveActivarbotonesPDVCli();
            //        Alertas.CssClass = "MensajesError";
            //        LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
            //        MensajeAlerta();
            //        ModalPopupPDVCliente.Show();
            //    }
            //    this.Session["oePDVC"] = oePDVC;
            //}
        }
        //private void MostrarDatosPDVCliente()
        //{
        //    recorrido = (DataTable)this.Session["tePDVC"];
        //    if (recorrido != null)
        //    {
        //        if (recorrido.Rows.Count > 0)
        //        {
        //            recsearch = Convert.ToInt32(this.Session["i"]);

        //            this.Session["id_ClientPDV"] = Convert.ToInt64(recorrido.Rows[recsearch]["id_ClientPDV"].ToString().Trim());
        //            iid_ClientPDV = Convert.ToInt32(this.Session["id_ClientPDV"]);
        //            cmbClientePDVC.Text = recorrido.Rows[recsearch]["Company_id"].ToString().Trim();
        //            ////cmbPDVCliente.Text = recorrido.Rows[recsearch]["id_PointOfsale"].ToString().Trim();
        //            ////TxtCodPDVC.Text = recorrido.Rows[recsearch]["ClientPDV_Code"].ToString().Trim();
        //            LlenacomboMallasenPDVC();
        //            ////cmbMallaPDVC.Text = recorrido.Rows[recsearch]["id_malla"].ToString().Trim();
        //            LlenacomboSectorxMallaPDVC();
        //            ////cmbSectorPDVC.Text = recorrido.Rows[recsearch]["id_sector"].ToString().Trim();
        //            estado = Convert.ToBoolean(recorrido.Rows[recsearch]["ClientPDV_Status"].ToString().Trim());
        //            //if (estado == true)
        //            //{
        //            //    RBTPDVC.Items[0].Selected = true;
        //            //    RBTPDVC.Items[1].Selected = false;
        //            //}
        //            //else
        //            //{
        //            //    RBTPDVC.Items[0].Selected = false;
        //            //    RBTPDVC.Items[1].Selected = true;
        //            //}
        //        }
        //    }
        //}
        protected void BtnEditarPDVC_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesPDVCli();
            EditarActivarControlesPDVCli();
            ////this.Session["rept"] = cmbPDVCliente.Text;
            this.Session["rept"] = cmbClientePDVC.Text;
            this.Session["rept1"] = GvPDV.Rows[0].Cells[0].Text;
        }

        protected void BtnActualizarPDVC_Click(object sender, EventArgs e)
        {
            bool continuar = false;
            LblFaltantes.Text = "";

            if (((CheckBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[7].FindControl("CheckBox1")).Checked != false)
            {
                continuar = true;
            }
            else
            {
                ((CheckBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[7].FindControl("CheckBox1")).Checked = false;
                continuar = true;
                DAplicacion oddeshabnodo = new DAplicacion();
                DataTable dt = oddeshabnodo.PermitirDeshabilitar(ConfigurationManager.AppSettings["NodeCommercialPointOfSale"], TxtCodnodo.Text);
                if (dt != null)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                    MensajeAlerta();
                    return;
                }
            }

            if (continuar)
            {


                if (((TextBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text == "" ||
                    ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[3].FindControl("cmbOficinaconsultaPDVC")).Text == "0"
                    ||
                    ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).Text == "0" ||
                    ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Text == "0")
                {

                    continuar = false;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Debe ingresar Información del Punto de Venta";
                    MensajeAlerta();

                }
                else
                {
                    continuar = true;
                }

            }
            else
            {
                continuar = false;
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar información de mínimo un Punto de Venta";
                MensajeAlerta();
            }


            if (continuar)
            {
                try
                {
                    DAplicacion odconsulPDVC = new DAplicacion();

                    DataTable dtconsulta = odconsulPDVC.ConsultaDuplicados(ConfigurationManager.AppSettings["PointOfSale_Client"], cmbClientePDVC.Text, GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].Text, null);
                    if (dtconsulta == null)
                    {
                        continuar = true;
                    }
                    else
                    {

                        continuar = false;

                        string sPDVCliente = "";
                        sPDVCliente = "punto de venta" + " " + cmbClientePDVC.SelectedItem.Text;
                        this.Session["sPDVCliente"] = sPDVCliente;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La Relación de PDV y Cliente " + this.Session["sPDVCliente"] + " Ya Existe";
                        MensajeAlerta();


                    }


                    if (continuar)
                    {


                        string sector;
                        if (((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Text != "No aplica")
                        {
                            sector = ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Text;
                        }
                        else
                        { sector = "0"; }
                        try
                        {
                            EPuntosDV oePDVCliente = oPDV.Actualizar_PDVClient(Convert.ToInt32(cmbBClientePDVC.Text), Convert.ToInt32(((Label)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("Label1")).Text), ((TextBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text, Convert.ToInt32(sector), Convert.ToInt64(((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[3].FindControl("cmbOficinaconsultaPDVC")).Text), Convert.ToInt32(((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("cmbDEX")).Text), ((CheckBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[7].FindControl("CheckBox1")).Checked, Convert.ToString(this.Session["sUser"]), DateTime.Now,"");
                            string sPDVCliente = "";
                            sPDVCliente = "punto de venta" + " " + cmbClientePDVC.SelectedItem.Text;
                            this.Session["sPDVCliente"] = sPDVCliente;
                            //LlenacomboMallasenSector();
                            //SavelimpiarcontrolesPDVCli();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "La Relación de PDV y Cliente " + this.Session["sPDVCliente"] + " fue Actualizada con Exito";
                            MensajeAlerta();
                            //saveActivarbotonesPDVCli();
                            //desactivarControlesPDVCli();
                            GVPDVConsulta.EditIndex = -1;
                            if (GVPDVConsulta.Rows.Count > 1)
                            {
                                LLenaPDVxConsulta();
                            }
                            else
                            {
                                GVPDVConsulta.DataBind();
                                cmbAgruCPDVC.Text = "0";

                            }
                        }
                        catch
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Código de la Relación de PDV y Cliente " + this.Session["sPDVCliente"] + " Ya Existe";
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
 
        }            
        protected void BtnCancelarPDVC_Click(object sender, EventArgs e)
        {
            SavelimpiarcontrolesPDVCli();
            saveActivarbotonesPDVCli();
            desactivarControlesPDVCli();
            GvPDV.DataBind();
            GVPDVConsulta.DataBind();
        }
        protected void GvPDV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GvPDV.EditIndex = e.NewEditIndex;
            LLenaComboPDVxACPDVC();
            LlenacomboOficinaenPDVC();
            LlenacomboMallasenPDVC();
            comboDistribuidora();
            BtnCancelarPDVC.Visible = false;
            BtnConsultarPDVC.Visible = false;
        }

        protected void GvPDV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GvPDV.EditIndex = -1;
            LLenaComboPDVxACPDVC();
            BtnCancelarPDVC.Visible = true;
            BtnConsultarPDVC.Visible = true;

        }
        protected void GvPDV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            bool continuar = false;
            LblFaltantes.Text = "";

            ((TextBox)GvPDV.Rows[GvPDV.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text = ((TextBox)GvPDV.Rows[GvPDV.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text.TrimStart();
            ((TextBox)GvPDV.Rows[GvPDV.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text = ((TextBox)GvPDV.Rows[GvPDV.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text.TrimEnd();

            if (((CheckBox)GvPDV.Rows[GvPDV.EditIndex].Cells[7].FindControl("CheckBox1")).Checked != false)
            {
                continuar = true;
            }
            
            if (continuar)
            {
                if (((TextBox)GvPDV.Rows[GvPDV.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text == "" ||
                    ((TextBox)GvPDV.Rows[GvPDV.EditIndex].Cells[2].FindControl("TxtAliasPDVC")).Text == "" || //agregando el campo de Alias
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[3].FindControl("cmbOficinaPDVC")).Text == "0" ||
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[4].FindControl("cmbMallaPDVC")).Text == "0" ||
                    ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Text == "0")
                {
                    continuar = false;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Debe ingresar Información del Punto de Venta";
                    MensajeAlerta();
                }
                if (CmbCanalPDVC.Text == "1023" && ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("cmbDEX")).Text == "0")
                {
                    continuar = false;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Para Canal Minorista se debe seleccionar Distribuidora";
                    MensajeAlerta();
                }
                else
                {
                    continuar = true;
                }
            }
            else
            {
                continuar = false;
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar información de mínimo un Punto de Venta";
                MensajeAlerta();
            }


            if (continuar)
            {
                try
                {
                    DAplicacion odconsulPDVC = new DAplicacion();

                    DataTable dtconsulta = odconsulPDVC.ConsultaDuplicados(ConfigurationManager.AppSettings["PointOfSale_Client"], cmbClientePDVC.Text, GvPDV.Rows[GvPDV.EditIndex].Cells[0].Text, null);
                    if (dtconsulta == null)
                    {
                        continuar = true;
                    }
                    else
                    {
                        continuar = false;

                        string sPDVCliente = "";
                        sPDVCliente = "punto de venta" + " " + cmbClientePDVC.SelectedItem.Text;
                        this.Session["sPDVCliente"] = sPDVCliente;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La Relación de PDV y Cliente " + this.Session["sPDVCliente"] + " Ya Existe";
                        MensajeAlerta();
                    }


                    if (continuar)
                    {


                        string sector;
                        if (((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Text != "No aplica")
                        {
                            sector = ((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Text;
                        }
                        else
                        { sector = "0"; }
                        try
                        {
                            //AGREGAR FUNCION PARA AGREGAR ALIAS
                            EPuntosDV oePDVCliente = oPDV.RegistrarClientPDV(Convert.ToInt32(cmbClientePDVC.Text), Convert.ToInt32(((Label)GvPDV.Rows[GvPDV.EditIndex].Cells[0].FindControl("Label1")).Text), ((TextBox)GvPDV.Rows[GvPDV.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text, Convert.ToInt32(sector), Convert.ToInt32(((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[3].FindControl("cmbOficinaPDVC")).Text), Convert.ToInt32(((DropDownList)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("cmbDEX")).Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now, ((TextBox)GvPDV.Rows[GvPDV.EditIndex].Cells[6].FindControl("TxtAliasPDVC")).Text);
                            EPuntosDV oePDVClientetmp = oPDV.RegistrarClientPDVTMP();

                            string sPDVCliente = "";
                            sPDVCliente = "punto de venta" + " " + cmbClientePDVC.SelectedItem.Text;
                            this.Session["sPDVCliente"] = sPDVCliente;
                            //LlenacomboMallasenSector();
                            //SavelimpiarcontrolesPDVCli();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "La Relación de PDV y Cliente " + this.Session["sPDVCliente"] + " fue creado con Exito";
                            MensajeAlerta();
                            saveActivarbotonesPDVCli();
                            //desactivarControlesPDVCli();
                            GvPDV.EditIndex = -1;
                            if (GvPDV.Rows.Count > 1)
                            {
                                LLenaComboPDVxACPDVC();
                            }
                            else
                            {
                                GvPDV.DataBind();
                                cmbAgruCPDVC.Text = "0";
                            }
                        }
                        catch
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Código de la Relación de PDV y Cliente " + this.Session["sPDVCliente"] + " Ya Existe";
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
        }
        protected void GVPDVConsulta_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVPDVConsulta.EditIndex = -1;
            LLenaPDVxConsulta();
            BtnCancelarPDVC.Visible = true;
            BtnConsultarPDVC.Visible = true;
        }

        protected void GVPDVConsulta_RowEditing(object sender, GridViewEditEventArgs e)
        {
            BtnCancelarPDVC.Visible = false;
            BtnConsultarPDVC.Visible = false;
            GVPDVConsulta.EditIndex = e.NewEditIndex;
            string Codigo, alias, oficina, malla, sector, Distribuidora;
            bool estado;
            Codigo = ((Label)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("lblcodPDVC")).Text;
            if (GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("LblAliasPDVC") == null)
            {
                alias = "";
            }
            else
            {
                 alias = ((Label)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("LblAliasPDVC")).Text;
            }
           
            oficina = ((Label)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("lblcodOficina")).Text;
            malla = ((Label)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("lblRegion")).Text;
            sector = ((Label)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("lblZona")).Text;
            Distribuidora = ((Label)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("lbldex")).Text;
            if (Distribuidora == "")
            {
                Distribuidora = "<Seleccione...>";
            }
            estado = ((CheckBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("CheckBox1")).Checked;
            LLenaPDVxConsulta();

            //se activan los controles de edicion
            comboConsultaDistribuidora();
            ((TextBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("TxtCodPDVC")).Text = Codigo;
            //((TextBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("TxtAliasPDVC")).Text = alias;
            LlenacomboConsultaOficinaenPDVC();
            ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("cmbOficinaconsultaPDVC")).Items.FindByText(oficina).Selected = true;
            LlenacomboConsultaMallasenPDVC();
            ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("cmbMallaConsultaPDVC")).Items.FindByText(malla).Selected = true;
            LlenacomboConsultaSectorxMallaPDVC();
            ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("cmbSectorPDVC")).Items.FindByText(sector).Selected = true;
            ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("cmbDEX")).Items.FindByText(Distribuidora).Selected = true;      
            ((CheckBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("CheckBox1")).Checked = estado;         
            
        } 
        protected void GVPDVConsulta_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {         
            LblFaltantes.Text = "";
            //((CheckBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("CheckBox1")).Checked = estado;

            if (((CheckBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[7].FindControl("CheckBox1")).Checked != false)          
                estado = true;
            else
            {
               estado  = false;

               DAplicacion oddeshabPDVCliente = new DAplicacion();
               DataTable dt = oddeshabPDVCliente.PermitirDeshabilitar(ConfigurationManager.AppSettings["PDVCliente_OPE_REPORTE_COMPETENCIA"], ((TextBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text);
               DataTable dt1 = oddeshabPDVCliente.PermitirDeshabilitar(ConfigurationManager.AppSettings["PDVCliente_OPE_REPORTE_EXHIBICION"], ((TextBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text);
               DataTable dt2 = oddeshabPDVCliente.PermitirDeshabilitar(ConfigurationManager.AppSettings["PDVCliente_OPE_REPORTE_PRECIO"], ((TextBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text);
               DataTable dt3 = oddeshabPDVCliente.PermitirDeshabilitar(ConfigurationManager.AppSettings["PDVCliente_OPE_REPORTE_SOD"], ((TextBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text);
               DataTable dt4 = oddeshabPDVCliente.PermitirDeshabilitar(ConfigurationManager.AppSettings["PDVCliente_OPE_REPORTE_STOCK"], ((TextBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text);
               DataTable dt5 = oddeshabPDVCliente.PermitirDeshabilitar(ConfigurationManager.AppSettings["PDVCliente_OPE_REPORTE_FOTOGRAFICO"], ((TextBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text);

               if (dt != null || dt1 != null || dt2 != null || dt3 != null || dt4 != null || dt5 != null)
               {

                   Alertas.CssClass = "MensajesError";
                   LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                   MensajeAlerta();
                   return;
               }
            }

              if (((TextBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text == "" ||
                    ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[3].FindControl("cmbOficinaconsultaPDVC")).Text == "0"
                    ||
                    ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).Text == "0" ||
                    ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Text == "0")
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Debe ingresar Información del Punto de Venta";
                    MensajeAlerta();
                }

              if (CmbCanalPDVC.Text == "1023" && ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("cmbDEX")).Text == "0")
              {
                  Alertas.CssClass = "MensajesError";
                  LblFaltantes.Text = "Para Canal Minorista se debe seleccionar Distribuidora";
                  MensajeAlerta();

              }
                try
                {
                    DAplicacion odconsulPDVC = new DAplicacion();

                    DataTable dtconsulta = odconsulPDVC.ConsultaDuplicados(ConfigurationManager.AppSettings["PointOfSale_Client"], cmbClientePDVC.Text, GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].Text, null);
                    if (dtconsulta == null)
                    {
                        string sector;
                        if (((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Text != "No aplica")
                        {
                            sector = ((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Text;
                        }
                        else
                        { sector = "0"; }
                        try
                        {
                            EPuntosDV oePDVCliente = oPDV.Actualizar_PDVClient(Convert.ToInt32(cmbBClientePDVC.Text), Convert.ToInt32(((Label)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("Label1")).Text), ((TextBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[2].FindControl("TxtCodPDVC")).Text, Convert.ToInt32(sector), Convert.ToInt64(((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[3].FindControl("cmbOficinaconsultaPDVC")).Text), Convert.ToInt32(((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("cmbDEX")).Text), ((CheckBox)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[6].FindControl("CheckBox1")).Checked, Convert.ToString(this.Session["sUser"]), DateTime.Now,"");
                            EPuntosDV oePlaPDVCliente = oPDV.Actualizar_PDVPlanningPDVClient(Convert.ToInt32(((Label)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[0].FindControl("Label1")).Text), Convert.ToInt64(((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[3].FindControl("cmbOficinaconsultaPDVC")).Text), Convert.ToInt32(((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[4].FindControl("cmbMallaConsultaPDVC")).Text), Convert.ToInt32(((DropDownList)GVPDVConsulta.Rows[GVPDVConsulta.EditIndex].Cells[5].FindControl("cmbSectorPDVC")).Text), "");
                            string sPDVCliente = "";
                            sPDVCliente = "punto de venta" + " " + cmbClientePDVC.SelectedItem.Text;
                            this.Session["sPDVCliente"] = sPDVCliente;
                            //LlenacomboMallasenSector();
                            //SavelimpiarcontrolesPDVCli();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "La relación de PDV y Cliente " + this.Session["sPDVCliente"] + " fue actualizada con éxito";
                            MensajeAlerta();
                            saveActivarbotonesPDVCli();
                            //desactivarControlesPDVCli();
                            GVPDVConsulta.EditIndex = -1;
                            if (GVPDVConsulta.Rows.Count > 1)
                            {
                                LLenaPDVxConsulta();                               
                            }
                            else
                            {
                                GVPDVConsulta.DataBind();
                                cmbAgruCPDVC.Text = "0";
                            }
                        }
                        catch
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El código de la relación de PDV y Cliente " + this.Session["sPDVCliente"] + " ya existe";
                            MensajeAlerta();
                        }
                    }
                    else
                    {
                        string sPDVCliente = "";
                        sPDVCliente = "punto de venta" + " " + cmbClientePDVC.SelectedItem.Text;
                        this.Session["sPDVCliente"] = sPDVCliente;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La relación de PDV y Cliente " + this.Session["sPDVCliente"] + " ya existe";
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
      
        #endregion        

        private void llenaPaisAgrupComercial(DropDownList ddlpais)
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 26);
            ddlpais.DataSource = ds;
            ddlpais.DataTextField = "Name_Country";
            ddlpais.DataValueField = "cod_Country";
            ddlpais.DataBind();
            ds = null;
        }
        private void RestriccionPais(DropDownList ddlpais, DropDownList ddlDepartamento, DropDownList ddlProvincia, DropDownList ddlDistrito, DropDownList ddlBarrio, string smaestro)
        {

            ddlDepartamento.CssClass = null;
            ddlProvincia.CssClass = null;
            ddlDistrito.CssClass = null;
            ddlBarrio.CssClass = null;
            ddlDepartamento.Enabled = true;
            ddlProvincia.Enabled = true;
            ddlDistrito.Enabled = true;
            ddlBarrio.Enabled = true;
            ddlDepartamento.Items.Clear();
            ddlProvincia.Items.Clear();
            ddlDistrito.Items.Clear();
            ddlBarrio.Items.Clear();

            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", ddlpais.Text);
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

            if (oescountry.CountryDpto == false)
            {
                ddlDepartamento.Enabled = false;
                ddlDepartamento.CssClass = "divipol";
                ddlDepartamento.Items.Clear();
            }

            if (oescountry.CountryCiudad == false)
            {
                ddlProvincia.Enabled = false;
                ddlProvincia.CssClass = "divipol";
                ddlProvincia.Items.Clear();
            }
            if (oescountry.CountryDistrito == false)
            {
                ddlDistrito.Enabled = false;
                ddlDistrito.CssClass = "divipol";
                ddlDistrito.Items.Clear();
            }
            if (oescountry.CountryBarrio == false)
            {
                ddlBarrio.Enabled = false;
                ddlBarrio.CssClass = "divipol";
                ddlBarrio.Items.Clear();
            }


            if (oescountry.CountryDpto == true)
            {
                ddlDepartamento.Enabled = true;
                ddlDepartamento.CssClass = null;
                DataTable dtcountry = new DataTable();
                dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", ddlpais.Text);
                if (dtcountry != null)
                {
                    if (dtcountry.Rows.Count > 1)
                    {
                        ddlDepartamento.DataSource = dtcountry;
                        ddlDepartamento.DataTextField = "Name_dpto";
                        ddlDepartamento.DataValueField = "cod_dpto";
                        ddlDepartamento.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear " + smaestro + " para este país. Falta crear el(los) Departamentos.";
                        MensajeAlerta();
                        ddlDepartamento.DataSource = dtcountry;
                        ddlDepartamento.DataTextField = "Name_dpto";
                        ddlDepartamento.DataValueField = "cod_dpto";
                        ddlDepartamento.DataBind();
                    }
                }
                dtcountry = null;
            }
            else
            {
                ddlDepartamento.Enabled = false;
                ddlDepartamento.CssClass = "divipol";
                ddlDepartamento.Items.Clear();
                if (oescountry.CountryCiudad == true)
                {
                    DataTable dtcity = new DataTable();
                    dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITYCONPAIS", ddlpais.Text);
                    if (dtcity.Rows.Count > 1)
                    {
                        ddlProvincia.DataSource = dtcity;
                        ddlProvincia.DataTextField = "Name_City";
                        ddlProvincia.DataValueField = "cod_City";
                        ddlProvincia.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear " + smaestro + " para este país. Falta crear la(s) Ciudades.";
                        MensajeAlerta();
                        ddlProvincia.DataSource = dtcity;
                        ddlProvincia.DataTextField = "Name_City";
                        ddlProvincia.DataValueField = "cod_City";
                        ddlProvincia.DataBind();
                    }
                    dtcity = null;
                }
                else
                {
                    ddlProvincia.Enabled = false;
                    ddlProvincia.CssClass = "divipol";
                    ddlProvincia.Items.Clear();
                    if (oescountry.CountryDistrito == true)
                    {
                        DataTable dtdistrito = new DataTable();
                        dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONPAIS", ddlpais.Text);
                        if (dtdistrito.Rows.Count > 1)
                        {
                            ddlDistrito.DataSource = dtdistrito;
                            ddlDistrito.DataTextField = "Name_Local";
                            ddlDistrito.DataValueField = "cod_District";
                            ddlDistrito.DataBind();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "No es posible crear " + smaestro + " para este país. Falta crear la(s) Distritos.";
                            MensajeAlerta();
                            ddlDistrito.DataSource = dtdistrito;
                            ddlDistrito.DataTextField = "Name_Local";
                            ddlDistrito.DataValueField = "cod_District";
                            ddlDistrito.DataBind();
                        }
                        dtdistrito = null;
                    }
                    else
                    {
                        ddlDistrito.Enabled = false;
                        ddlDistrito.CssClass = "divipol";
                        ddlDistrito.Items.Clear();
                        if (oescountry.CountryBarrio == true)
                        {

                            DataTable dtbarrio = new DataTable();

                            dtbarrio = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONPAIS", ddlpais.Text);
                            if (dtbarrio.Rows.Count > 1)
                            {
                                ddlBarrio.DataSource = dtbarrio;
                                ddlBarrio.DataTextField = "Name_Community";
                                ddlBarrio.DataValueField = "cod_Community";
                                ddlBarrio.DataBind();
                            }
                            else
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "No es posible crear " + smaestro + " para este país. Falta crear la(s) Barrios.";
                                MensajeAlerta();
                                ddlBarrio.DataSource = dtbarrio;
                                ddlBarrio.DataTextField = "Name_Community";
                                ddlBarrio.DataValueField = "cod_Community";
                                ddlBarrio.DataBind();
                            }
                            dtbarrio = null;
                        }
                        else
                        {
                            ddlBarrio.Enabled = false;
                            ddlBarrio.CssClass = "divipol";
                            ddlBarrio.Items.Clear();
                        }
                    }
                }
            }
            dt = null;
            oescountry = null;
        }

        private void RestriccionDepartamento(DropDownList ddlpais, DropDownList ddlDepartamento, DropDownList ddlProvincia, DropDownList ddlDistrito, DropDownList ddlBarrio, string smaestro)
        {
            ddlProvincia.CssClass = null;
            ddlDistrito.CssClass = null;
            ddlBarrio.CssClass = null;
            ddlProvincia.Enabled = true;
            ddlDistrito.Enabled = true;
            ddlBarrio.Enabled = true;
            ddlProvincia.Items.Clear();
            ddlDistrito.Items.Clear();
            ddlBarrio.Items.Clear();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", ddlpais.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }

            if (oescountry.CountryCiudad == false)
            {
                ddlProvincia.Enabled = false;
                ddlProvincia.CssClass = "divipol";
                ddlProvincia.Items.Clear();
            }

            if (oescountry.CountryDistrito == false)
            {
                ddlDistrito.Enabled = false;
                ddlDistrito.CssClass = "divipol";
                ddlDistrito.Items.Clear();

            }
            if (oescountry.CountryBarrio == false)
            {
                ddlBarrio.Enabled = false;
                ddlBarrio.CssClass = "divipol";
                ddlBarrio.Items.Clear();
            }

            if (oescountry.CountryCiudad == true)
            {
                ddlProvincia.Enabled = true;
                ddlProvincia.CssClass = null;
                DataTable dtcity = new DataTable();
                dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITY", ddlDepartamento.Text);
                if (dtcity.Rows.Count > 1)
                {
                    ddlProvincia.DataSource = dtcity;
                    ddlProvincia.DataTextField = "Name_City";
                    ddlProvincia.DataValueField = "cod_City";
                    ddlProvincia.DataBind();
                    dtcity = null;
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No es posible crear " + smaestro + " para este país. Falta crear el(los) Ciudad.";
                    MensajeAlerta();
                    ddlProvincia.DataSource = dtcity;
                    ddlProvincia.DataTextField = "Name_City";
                    ddlProvincia.DataValueField = "cod_City";
                    ddlProvincia.DataBind();
                }
                dtcity = null;
            }
            else
            {
                ddlProvincia.Enabled = false;
                ddlProvincia.CssClass = "divipol";
                ddlProvincia.Items.Clear();
                if (oescountry.CountryDistrito == true)
                {
                    ddlDistrito.Enabled = true;
                    ddlDistrito.CssClass = null;
                    DataTable dtdistrito = new DataTable();
                    dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONDEPTO", ddlDepartamento.Text);
                    if (dtdistrito.Rows.Count > 1)
                    {
                        ddlDistrito.DataSource = dtdistrito;
                        ddlDistrito.DataTextField = "Name_Local";
                        ddlDistrito.DataValueField = "cod_District";
                        ddlDistrito.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear " + smaestro + " para este país. Falta crear el(los) Distrito.";
                        MensajeAlerta();
                        ddlDistrito.DataSource = dtdistrito;
                        ddlDistrito.DataTextField = "Name_Local";
                        ddlDistrito.DataValueField = "cod_District";
                        ddlDistrito.DataBind();
                    }
                    dtdistrito = null;
                }
                else
                {
                    ddlDistrito.Enabled = false;
                    ddlDistrito.CssClass = "divipol";
                    ddlDistrito.Items.Clear();
                    if (oescountry.CountryBarrio == true)
                    {
                        ddlBarrio.Enabled = true;
                        ddlBarrio.CssClass = null;
                        DataTable dtbarrio = new DataTable();
                        dtbarrio = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONDPTO", ddlDepartamento.Text);
                        if (dtbarrio.Rows.Count > 1)
                        {
                            ddlBarrio.DataSource = dtbarrio;
                            ddlBarrio.DataTextField = "Name_Community";
                            ddlBarrio.DataValueField = "cod_Community";
                            ddlBarrio.DataBind();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "No es posible crear " + smaestro + " para este país. Falta crear el(los) Barrios.";
                            MensajeAlerta();
                            ddlBarrio.DataSource = dtbarrio;
                            ddlBarrio.DataTextField = "Name_Community";
                            ddlBarrio.DataValueField = "cod_Community";
                            ddlBarrio.DataBind();
                        }
                        dtbarrio = null;
                    }
                    else
                    {
                        ddlBarrio.Enabled = false;
                        ddlBarrio.CssClass = "divipol";
                        ddlBarrio.Items.Clear();
                    }

                }
            }

            dt = null;
            oescountry = null;
        }
        private void RestriccionProvincia(DropDownList ddlpais, DropDownList ddlDepartamento, DropDownList ddlProvincia, DropDownList ddlDistrito, DropDownList ddlBarrio, string smaestro)
        {
            ddlDistrito.CssClass = null;
            ddlBarrio.CssClass = null;
            ddlDistrito.Enabled = true;
            ddlBarrio.Enabled = true;
            ddlDistrito.Items.Clear();
            ddlBarrio.Items.Clear();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", ddlpais.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }

            if (oescountry.CountryDistrito == false)
            {
                ddlDistrito.Enabled = false;
                ddlDistrito.CssClass = "divipol";
                ddlDistrito.Items.Clear();
            }
            if (oescountry.CountryBarrio == false)
            {
                ddlBarrio.Enabled = false;
                ddlBarrio.CssClass = "divipol";
                ddlBarrio.Items.Clear();
            }


            if (oescountry.CountryDistrito == true)
            {
                ddlDistrito.Enabled = true;
                ddlDistrito.CssClass = null;
                DataTable dtdistrito = new DataTable();
                dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITO", ddlProvincia.Text);
                if (dtdistrito.Rows.Count > 1)
                {
                    ddlDistrito.DataSource = dtdistrito;
                    ddlDistrito.DataTextField = "Name_Local";
                    ddlDistrito.DataValueField = "cod_District";
                    ddlDistrito.DataBind();
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No es posible crear " + smaestro + " para este país. Falta crear el(los) Distrito.";
                    MensajeAlerta();
                    ddlDistrito.DataSource = dtdistrito;
                    ddlDistrito.DataTextField = "Name_Local";
                    ddlDistrito.DataValueField = "cod_District";
                    ddlDistrito.DataBind();
                }
                dtdistrito = null;
            }
            else
            {
                ddlDistrito.Enabled = false;
                ddlDistrito.CssClass = "divipol";
                ddlDistrito.Items.Clear();
                if (oescountry.CountryBarrio == true)
                {
                    ddlBarrio.Enabled = true;
                    ddlBarrio.CssClass = null;
                    DataTable dtbarrio = new DataTable();

                    dtbarrio = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONCITY", ddlProvincia.Text);
                    if (dtbarrio.Rows.Count > 1)
                    {
                        ddlBarrio.DataSource = dtbarrio;
                        ddlBarrio.DataTextField = "Name_Community";
                        ddlBarrio.DataValueField = "cod_Community";
                        ddlBarrio.DataBind();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No es posible crear " + smaestro + " para este país. Falta crear el(los) Barrio.";
                        MensajeAlerta();
                        ddlBarrio.DataSource = dtbarrio;
                        ddlBarrio.DataTextField = "Name_Community";
                        ddlBarrio.DataValueField = "cod_Community";
                        ddlBarrio.DataBind();
                    }
                    dtbarrio = null;
                }
                else
                {
                    ddlBarrio.Enabled = false;
                    ddlBarrio.CssClass = "divipol";
                    ddlBarrio.Items.Clear();
                }
            }

            dt = null;
            oescountry = null;
        }

        private void RestriccionDistrito(DropDownList ddlpais, DropDownList ddlDepartamento, DropDownList ddlProvincia, DropDownList ddlDistrito, DropDownList ddlBarrio, string smaestro)
        {
            ddlBarrio.CssClass = null;
            ddlBarrio.Enabled = true;
            ddlBarrio.Items.Clear();
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", ddlpais.Text);
            ECountry oescountry = new ECountry();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
                }
            }
            if (oescountry.CountryBarrio == false)
            {
                ddlBarrio.Enabled = false;
                ddlBarrio.CssClass = "divipol";
                ddlBarrio.Items.Clear();
            }
            else
            {
                ddlBarrio.Enabled = true;
                ddlBarrio.CssClass = null;
                DataTable dte = null;
                dte = oConn.ejecutarDataTable("UP_WEB_COMBOCOMUNITY", ddlpais.SelectedValue, ddlDepartamento.SelectedValue, ddlProvincia.SelectedValue, ddlDistrito.SelectedValue);
                if (dte.Rows.Count > 1)
                {
                    ddlBarrio.DataSource = dte;
                    ddlBarrio.DataTextField = "Name_Community";
                    ddlBarrio.DataValueField = "cod_Community";
                    ddlBarrio.DataBind();
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No es posible crear "+smaestro+" para este país. Falta crear el(los) Distritos.";
                    MensajeAlerta();
                    ddlBarrio.DataSource = dte;
                    ddlBarrio.DataTextField = "Name_Community";
                    ddlBarrio.DataValueField = "cod_Community";
                    ddlBarrio.DataBind();
                }
                dte = null;
            }
        }
        protected void ddlPais_edit_SelectedIndexChanged(object sender, EventArgs e)
        {
            MopopConsulAgrupCom.Show();
            RestriccionPais(((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[7].FindControl("ddlPais_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[8].FindControl("ddlDepartamento_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[9].FindControl("ddlProvincia_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[10].FindControl("ddlDistrito_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[11].FindControl("ddlBarrio_edit")), "Agrupación Comercial");
           
        }

        protected void ddlDepartamento_edit_SelectedIndexChanged(object sender, EventArgs e)
        {
            MopopConsulAgrupCom.Show();
            RestriccionDepartamento(((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[7].FindControl("ddlPais_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[8].FindControl("ddlDepartamento_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[9].FindControl("ddlProvincia_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[10].FindControl("ddlDistrito_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[11].FindControl("ddlBarrio_edit")), "Agrupación Comercial");
           
        }


        protected void ddlProvincia_edit_SelectedIndexChanged(object sender, EventArgs e)
        {
            MopopConsulAgrupCom.Show();
            RestriccionProvincia(((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[7].FindControl("ddlPais_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[8].FindControl("ddlDepartamento_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[9].FindControl("ddlProvincia_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[10].FindControl("ddlDistrito_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[11].FindControl("ddlBarrio_edit")), "Agrupación Comercial");
           
        }
        protected void ddlDistrito_edit_SelectedIndexChanged(object sender, EventArgs e)
        {
            MopopConsulAgrupCom.Show();
            RestriccionDistrito(((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[7].FindControl("ddlPais_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[8].FindControl("ddlDepartamento_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[9].FindControl("ddlProvincia_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[10].FindControl("ddlDistrito_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[11].FindControl("ddlBarrio_edit")), "Agrupación Comercial");
           
        }
        protected void ddlBarrio_edit_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MopopConsulAgrupCom.Show();
            //Restriccionb(((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[11].FindControl("ddlBarrio_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[8].FindControl("ddlDepartamento_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[9].FindControl("ddlProvincia_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[10].FindControl("ddlDistrito_edit")), ((DropDownList)GVConsAgrupCom.Rows[GVConsAgrupCom.EditIndex].Cells[11].FindControl("ddlBarrio_edit")), "Agrupación Comercial");
           
        }
        protected void ddlSelCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            RestriccionPais(ddlSelCountry, ddlDpto, ddlProv, ddlDist, ddlBarrio, "Agrupación Comercial");

            #region comentario
            //ddlDpto.CssClass = null;
            //ddlProv.CssClass = null;
            //ddlDist.CssClass = null;
            //ddlBarrio.CssClass = null;
            //ddlDpto.Enabled = true;
            //ddlProv.Enabled = true;
            //ddlDist.Enabled = true;
            //ddlBarrio.Enabled = true;
            //ddlDpto.Items.Clear();
            //ddlProv.Items.Clear();
            //ddlDist.Items.Clear();
            //ddlBarrio.Items.Clear();

            //DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", ddlSelCountry.Text);
            //ECountry oescountry = new ECountry();
            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        oescountry.CountryDpto = Convert.ToBoolean(dt.Rows[0]["Country_Dpto"].ToString().Trim());
            //        oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
            //        oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
            //        oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
            //    }
            //}

            //if (oescountry.CountryDpto == false)
            //{
            //    ddlDpto.Enabled = false;
            //    ddlDpto.CssClass = "divipol";
            //    ddlDpto.Items.Clear();
            //}

            //if (oescountry.CountryCiudad == false)
            //{
            //    ddlProv.Enabled = false;
            //    ddlProv.CssClass = "divipol";
            //    ddlProv.Items.Clear();
            //}
            //if (oescountry.CountryDistrito == false)
            //{
            //    ddlDist.Enabled = false;
            //    ddlDist.CssClass = "divipol";
            //    ddlDist.Items.Clear();
            //}
            //if (oescountry.CountryBarrio == false)
            //{
            //    ddlBarrio.Enabled = false;
            //    ddlBarrio.CssClass = "divipol";
            //    ddlBarrio.Items.Clear();
            //}


            //if (oescountry.CountryDpto == true)
            //{
            //    ddlDpto.Enabled = true;
            //    ddlDpto.CssClass = null;
            //    DataTable dtcountry = new DataTable();
            //    dtcountry = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCOUNTRY", ddlSelCountry.Text);
            //    if (dtcountry != null)
            //    {
            //        if (dtcountry.Rows.Count > 1)
            //        {
            //            ddlDpto.DataSource = dtcountry;
            //            ddlDpto.DataTextField = "Name_dpto";
            //            ddlDpto.DataValueField = "cod_dpto";
            //            ddlDpto.DataBind();
            //        }
            //        else
            //        {
            //            Alertas.CssClass = "MensajesError";
            //            LblFaltantes.Text = "No es posible crear Agrupacion Comercial para este país. Falta crear el(los) Departamentos.";
            //            MensajeAlerta();
            //            ddlDpto.DataSource = dtcountry;
            //            ddlDpto.DataTextField = "Name_dpto";
            //            ddlDpto.DataValueField = "cod_dpto";
            //            ddlDpto.DataBind();
            //        }
            //    }
            //    dtcountry = null;
            //}
            //else
            //{
            //    ddlDpto.Enabled = false;
            //    ddlDpto.CssClass = "divipol";
            //    ddlDpto.Items.Clear();
            //    if (oescountry.CountryCiudad == true)
            //    {
            //        DataTable dtcity = new DataTable();
            //        dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITYCONPAIS", ddlSelCountry.Text);
            //        if (dtcity.Rows.Count > 1)
            //        {
            //            ddlProv.DataSource = dtcity;
            //            ddlProv.DataTextField = "Name_City";
            //            ddlProv.DataValueField = "cod_City";
            //            ddlProv.DataBind();
            //        }
            //        else
            //        {
            //            Alertas.CssClass = "MensajesError";
            //            LblFaltantes.Text = "No es posible crear Agrupacion Comercial para este país. Falta crear la(s) Ciudades.";
            //            MensajeAlerta();
            //            ddlProv.DataSource = dtcity;
            //            ddlProv.DataTextField = "Name_City";
            //            ddlProv.DataValueField = "cod_City";
            //            ddlProv.DataBind();
            //        }
            //        dtcity = null;
            //    }
            //    else
            //    {
            //        ddlProv.Enabled = false;
            //        ddlProv.CssClass = "divipol";
            //        ddlProv.Items.Clear();
            //        if (oescountry.CountryDistrito == true)
            //        {
            //            DataTable dtdistrito = new DataTable();
            //            dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONPAIS", ddlSelCountry.Text);
            //            if (dtdistrito.Rows.Count > 1)
            //            {
            //                ddlDist.DataSource = dtdistrito;
            //                ddlDist.DataTextField = "Name_Local";
            //                ddlDist.DataValueField = "cod_District";
            //                ddlDist.DataBind();
            //            }
            //            else
            //            {
            //                Alertas.CssClass = "MensajesError";
            //                LblFaltantes.Text = "No es posible crear Agrupacion Comercial para este país. Falta crear la(s) Distritos.";
            //                MensajeAlerta();
            //                ddlDist.DataSource = dtdistrito;
            //                ddlDist.DataTextField = "Name_Local";
            //                ddlDist.DataValueField = "cod_District";
            //                ddlDist.DataBind();
            //            }
            //            dtdistrito = null;
            //        }
            //        else
            //        {
            //            ddlDist.Enabled = false;
            //            ddlDist.CssClass = "divipol";
            //            ddlDist.Items.Clear();
            //            if (oescountry.CountryBarrio == true)
            //            {

            //                DataTable dtbarrio = new DataTable();

            //                dtbarrio = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONPAIS", ddlSelCountry.Text);
            //                if (dtbarrio.Rows.Count > 1)
            //                {
            //                    ddlBarrio.DataSource = dtbarrio;
            //                    ddlBarrio.DataTextField = "Name_Community";
            //                    ddlBarrio.DataValueField = "cod_Community";
            //                    ddlBarrio.DataBind();
            //                }
            //                else
            //                {
            //                    Alertas.CssClass = "MensajesError";
            //                    LblFaltantes.Text = "No es posible crear Agrupacion Comercial para este país. Falta crear la(s) Barrios.";
            //                    MensajeAlerta();
            //                    ddlBarrio.DataSource = dtbarrio;
            //                    ddlBarrio.DataTextField = "Name_Community";
            //                    ddlBarrio.DataValueField = "cod_Community";
            //                    ddlBarrio.DataBind();
            //                }
            //                dtbarrio = null;
            //            }
            //            else
            //            {
            //                ddlBarrio.Enabled = false;
            //                ddlBarrio.CssClass = "divipol";
            //                ddlBarrio.Items.Clear();
            //            }
            //        }
            //    }
            //}
            //dt = null;
            //oescountry = null;
            #endregion
        }

        protected void ddlDpto_SelectedIndexChanged(object sender, EventArgs e)
        {
            RestriccionDepartamento(ddlSelCountry, ddlDpto, ddlProv, ddlDist, ddlBarrio, "Agrupación Comercial");

            #region comentario
            //ddlProv.CssClass = null;
            //ddlDist.CssClass = null;
            //ddlBarrio.CssClass = null;
            //ddlProv.Enabled = true;
            //ddlDist.Enabled = true;
            //ddlBarrio.Enabled = true;
            //ddlProv.Items.Clear();
            //ddlDist.Items.Clear();
            //ddlBarrio.Items.Clear();
            //DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", ddlSelCountry.Text);
            //ECountry oescountry = new ECountry();

            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        oescountry.CountryCiudad = Convert.ToBoolean(dt.Rows[0]["Country_Ciudad"].ToString().Trim());
            //        oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
            //        oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
            //    }
            //}

            //if (oescountry.CountryCiudad == false)
            //{
            //    ddlProv.Enabled = false;
            //    ddlProv.CssClass = "divipol";
            //    ddlProv.Items.Clear();
            //}

            //if (oescountry.CountryDistrito == false)
            //{
            //    ddlDist.Enabled = false;
            //    ddlDist.CssClass = "divipol";
            //    ddlDist.Items.Clear();

            //}
            //if (oescountry.CountryBarrio == false)
            //{
            //    ddlBarrio.Enabled = false;
            //    ddlBarrio.CssClass = "divipol";
            //    ddlBarrio.Items.Clear();
            //}

            //if (oescountry.CountryCiudad == true)
            //{
            //    ddlProv.Enabled = true;
            //    ddlProv.CssClass = null;
            //    DataTable dtcity = new DataTable();
            //    dtcity = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSCITY", ddlDpto.Text);
            //    if (dtcity.Rows.Count > 1)
            //    {
            //        ddlProv.DataSource = dtcity;
            //        ddlProv.DataTextField = "Name_City";
            //        ddlProv.DataValueField = "cod_City";
            //        ddlProv.DataBind();
            //        dtcity = null;
            //    }
            //    else
            //    {
            //        Alertas.CssClass = "MensajesError";
            //        LblFaltantes.Text = "No es posible crear Agrupacion Comercial para este país. Falta crear el(los) Ciudad.";
            //        MensajeAlerta();
            //        ddlProv.DataSource = dtcity;
            //        ddlProv.DataTextField = "Name_City";
            //        ddlProv.DataValueField = "cod_City";
            //        ddlProv.DataBind();
            //    }
            //    dtcity = null;
            //}
            //else
            //{
            //    ddlProv.Enabled = false;
            //    ddlProv.CssClass = "divipol";
            //    ddlProv.Items.Clear();
            //    if (oescountry.CountryDistrito == true)
            //    {
            //        ddlDist.Enabled = true;
            //        ddlDist.CssClass = null;
            //        DataTable dtdistrito = new DataTable();
            //        dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITOCONDEPTO", ddlDpto.Text);
            //        if (dtdistrito.Rows.Count > 1)
            //        {
            //            ddlDist.DataSource = dtdistrito;
            //            ddlDist.DataTextField = "Name_Local";
            //            ddlDist.DataValueField = "cod_District";
            //            ddlDist.DataBind();
            //        }
            //        else
            //        {
            //            Alertas.CssClass = "MensajesError";
            //            LblFaltantes.Text = "No es posible crear Agrupacion Comercial para este país. Falta crear el(los) Distrito.";
            //            MensajeAlerta();
            //            ddlDist.DataSource = dtdistrito;
            //            ddlDist.DataTextField = "Name_Local";
            //            ddlDist.DataValueField = "cod_District";
            //            ddlDist.DataBind();
            //        }
            //        dtdistrito = null;
            //    }
            //    else
            //    {
            //        ddlDist.Enabled = false;
            //        ddlDist.CssClass = "divipol";
            //        ddlDist.Items.Clear();
            //        if (oescountry.CountryBarrio == true)
            //        {
            //            ddlBarrio.Enabled = true;
            //            ddlBarrio.CssClass = null;
            //            DataTable dtbarrio = new DataTable();
            //            dtbarrio = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONDPTO", ddlDpto.Text);
            //            if (dtbarrio.Rows.Count > 1)
            //            {
            //                ddlBarrio.DataSource = dtbarrio;
            //                ddlBarrio.DataTextField = "Name_Community";
            //                ddlBarrio.DataValueField = "cod_Community";
            //                ddlBarrio.DataBind();
            //            }
            //            else
            //            {
            //                Alertas.CssClass = "MensajesError";
            //                LblFaltantes.Text = "No es posible crear Agrupacion Comercial para este país. Falta crear el(los) Barrios.";
            //                MensajeAlerta();
            //                ddlBarrio.DataSource = dtbarrio;
            //                ddlBarrio.DataTextField = "Name_Community";
            //                ddlBarrio.DataValueField = "cod_Community";
            //                ddlBarrio.DataBind();
            //            }
            //            dtbarrio = null;
            //        }
            //        else
            //        {
            //            ddlBarrio.Enabled = false;
            //            ddlBarrio.CssClass = "divipol";
            //            ddlBarrio.Items.Clear();
            //        }

            //    }
            //}

            //dt = null;
            //oescountry = null;
            #endregion
        }

        protected void ddlProv_SelectedIndexChanged(object sender, EventArgs e)
        {
            RestriccionProvincia(ddlSelCountry, ddlDpto, ddlProv, ddlDist, ddlBarrio, "Agrupación Comercial");

            #region comentario
            //ddlDist.CssClass = null;
            //ddlBarrio.CssClass = null;
            //ddlDist.Enabled = true;
            //ddlBarrio.Enabled = true;
            //ddlDist.Items.Clear();
            //ddlBarrio.Items.Clear();
            //DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", ddlSelCountry.Text);
            //ECountry oescountry = new ECountry();

            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        oescountry.CountryDistrito = Convert.ToBoolean(dt.Rows[0]["Country_Distrito"].ToString().Trim());
            //        oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
            //    }
            //}

            //if (oescountry.CountryDistrito == false)
            //{
            //    ddlDist.Enabled = false;
            //    ddlDist.CssClass = "divipol";
            //    ddlDist.Items.Clear();
            //}
            //if (oescountry.CountryBarrio == false)
            //{
            //    ddlBarrio.Enabled = false;
            //    ddlBarrio.CssClass = "divipol";
            //    ddlBarrio.Items.Clear();
            //}


            //if (oescountry.CountryDistrito == true)
            //{
            //    ddlDist.Enabled = true;
            //    ddlDist.CssClass = null;
            //    DataTable dtdistrito = new DataTable();
            //    dtdistrito = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSDISTRITO", ddlProv.Text);
            //    if (dtdistrito.Rows.Count > 1)
            //    {
            //        ddlDist.DataSource = dtdistrito;
            //        ddlDist.DataTextField = "Name_Local";
            //        ddlDist.DataValueField = "cod_District";
            //        ddlDist.DataBind();
            //    }
            //    else
            //    {
            //        Alertas.CssClass = "MensajesError";
            //        LblFaltantes.Text = "No es posible crear Agrupacion Comercial para este país. Falta crear el(los) Distrito.";
            //        MensajeAlerta();
            //        ddlDist.DataSource = dtdistrito;
            //        ddlDist.DataTextField = "Name_Local";
            //        ddlDist.DataValueField = "cod_District";
            //        ddlDist.DataBind();
            //    }
            //    dtdistrito = null;
            //}
            //else
            //{
            //    ddlDist.Enabled = false;
            //    ddlDist.CssClass = "divipol";
            //    ddlDist.Items.Clear();
            //    if (oescountry.CountryBarrio == true)
            //    {
            //        ddlBarrio.Enabled = true;
            //        ddlBarrio.CssClass = null;
            //        DataTable dtbarrio = new DataTable();

            //        dtbarrio = oConn.ejecutarDataTable("UP_WEBSIGE_LLENACOMBOSBARRIOSCONCITY", ddlProv.Text);
            //        if (dtbarrio.Rows.Count > 1)
            //        {
            //            ddlBarrio.DataSource = dtbarrio;
            //            ddlBarrio.DataTextField = "Name_Community";
            //            ddlBarrio.DataValueField = "cod_Community";
            //            ddlBarrio.DataBind();
            //        }
            //        else
            //        {
            //            Alertas.CssClass = "MensajesError";
            //            LblFaltantes.Text = "No es posible crear Agrupacion Comercial para este país. Falta crear el(los) Barrio.";
            //            MensajeAlerta();
            //            ddlBarrio.DataSource = dtbarrio;
            //            ddlBarrio.DataTextField = "Name_Community";
            //            ddlBarrio.DataValueField = "cod_Community";
            //            ddlBarrio.DataBind();
            //        }
            //        dtbarrio = null;
            //    }
            //    else
            //    {
            //        ddlBarrio.Enabled = false;
            //        ddlBarrio.CssClass = "divipol";
            //        ddlBarrio.Items.Clear();
            //    }
            //}

            //dt = null;
            //oescountry = null;
            #endregion
        }

        protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
        {
            RestriccionDistrito(ddlSelCountry, ddlDpto, ddlProv, ddlDist, ddlBarrio, "Agrupación Comercial");

            //ddlBarrio.CssClass = null;
            //ddlBarrio.Enabled = true;
            //ddlBarrio.Items.Clear();
            //DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_DIVISIONCOUNTRY", ddlSelCountry.Text);
            //ECountry oescountry = new ECountry();

            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        oescountry.CountryBarrio = Convert.ToBoolean(dt.Rows[0]["Country_Barrio"].ToString().Trim());
            //    }
            //}
            //if (oescountry.CountryBarrio == false)
            //{
            //    ddlBarrio.Enabled = false;
            //    ddlBarrio.CssClass = "divipol";
            //    ddlBarrio.Items.Clear();
            //}
            //else
            //{
            //    ddlBarrio.Enabled = true;
            //    ddlBarrio.CssClass = null;
            //    comboBarrio();
            //}
        }
          private void comboBarrio()
        {
            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEB_COMBOCOMUNITY", ddlSelCountry.SelectedValue, ddlDpto.SelectedValue, ddlProv.SelectedValue, ddlDist.SelectedValue);
            if (dt.Rows.Count > 1)
            {
                ddlBarrio.DataSource = dt;
                ddlBarrio.DataTextField = "Name_Community";
                ddlBarrio.DataValueField = "cod_Community";
                ddlBarrio.DataBind();
            }
            else
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "No es posible crear Agrupacion Comercial para este país. Falta crear el(los) Distritos.";
                MensajeAlerta();
                ddlBarrio.DataSource = dt;
                ddlBarrio.DataTextField = "Name_Community";
                ddlBarrio.DataValueField = "cod_Community";
                ddlBarrio.DataBind();
            }
            dt = null;
        }
                       
   }
}