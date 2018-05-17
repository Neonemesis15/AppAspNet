using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Lucky.Business.Common.Application;
using Lucky.Business.Common.Security;
using Lucky.CFG.Messenger;
using Lucky.CFG.Util;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Application.Security;
using Telerik.Web.UI;
using System.Collections.Generic;


namespace SIGE.Pages.Modulos.Administrativo
{
    //-- =============================================
    //-- Author:		    <Ing. Magaly Jiménez>
    //-- Create date:       <23/06/2010>
    //-- Description:       <Permite al actor Administrador de SIGE realizar todos los procesos para la administracion 
    //--                    de Gestión Usuarios>
    //-- Requerimiento No.      
    //-- =============================================
    public partial class GestiónUsuario : System.Web.UI.Page
    {
        private bool estado;
        private string sRolname;
        private string repetido;
        private string sNomRol;
        private string sCodNivel;
        private int idChannel;
        private string SNomNivel;
        private string sPerfilName;
        private bool bContinuar = true;
        private string sRolid;
        private string sNomPerfil;
        private string sdoc;
        private string sUsuer;
        private string sNomUsu;
        private string repetido1 = "";
        private string repetido2 = "";
        //string sCountryAsign = "";
        //string sNameDirCuenta = "";
        private int recsearch;
        private DataTable recorrido = null;

        private DataSet ds = null;
        private DataSet ds1 = new DataSet();
        private Conexion oConn = new Conexion();
        private Usuario oUsuario=new Usuario();
        private Person_Asign_Ejec_Direct oPerson_Asign_Ejec_Direct = new Person_Asign_Ejec_Direct();
        private Facade_Search.Facade_Search search = new SIGE.Facade_Search.Facade_Search();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos obtenerid = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {                 
                    comboRolenBuscarperfil();
                    comboCanalenBuscarperfil();
                    comboNivelbuscarPerfil();
                    comboNivelConsultanNivel();
                    combousuarios();
                    cargar_cbxl_cxu_cliente();
                    llenar_ddl_cxub_cliente();
                    llenaTipoReporte_ClienteB();
                    llenaTipoReporte_ReporteB();
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
        private void SavelimpiarControlesRol()
        {
            TxtCodRol.Text = "";
            TxtNomRol.Text = "";
            TxtDescRol.Text = "";
            RBtnListStatusRol.Items[0].Selected = true;
            RBtnListStatusRol.Items[1].Selected = false;

            TxtBNomRol.Text = "";
        }
        private void activarControlesRol() 
        {
            TxtCodRol.Enabled = true;
            TxtNomRol.Enabled = true;
            TxtDescRol.Enabled = true;            
            RBtnListStatusRol.Enabled = false;
            Panel_Roles.Enabled = true;
            Panel_Nivel.Enabled = false;
            Panel_Perfil.Enabled = false;
            Panel_Usuario.Enabled = false;
            //Panel_Asignación.Enabled = false;
        }
        private void desactivarControlesRol()
        {
            TxtCodRol.Enabled = false;
            TxtNomRol.Enabled = false;
            TxtDescRol.Enabled = false;
            RBtnListStatusRol.Enabled = false;
            Panel_Roles.Enabled = true;
            Panel_Nivel.Enabled = true;
            Panel_Perfil.Enabled = true;
            Panel_Usuario.Enabled = true;
            //Panel_Asignación.Enabled = true;
        }        
        private void crearActivarbotonesRol()
        {
            btnCrearRol.Visible = false;
            btnsaveus.Visible = true;
            btnConsultarRol.Visible = false;
            btnEditRol.Visible = false;
            btnActuRol.Visible = false;
            btnCancelRol.Visible = true;
            btnPreg0.Visible = false;
            btnAreg0.Visible = false;
            btnSreg0.Visible = false;
            btnUreg0.Visible = false;
        }       
        private void saveActivarbotonesRol()
        {
            btnCrearRol.Visible = true;
            btnsaveus.Visible = false;
            btnConsultarRol.Visible = true;
            btnEditRol.Visible = false;
            btnActuRol.Visible = false;
            btnCancelRol.Visible = true;
            btnPreg0.Visible = false;
            btnAreg0.Visible = false;
            btnSreg0.Visible = false;
            btnUreg0.Visible = false;
        }
        private void EditarActivarbotonesRol()
        {
            btnCrearRol.Visible = true;
            btnsaveus.Visible = false;
            btnConsultarRol.Visible = true;
            btnEditRol.Visible = false;
            btnActuRol.Visible = true;
            btnCancelRol.Visible = true;
            btnPreg0.Visible = false;
            btnAreg0.Visible = false;
            btnSreg0.Visible = false;
            btnUreg0.Visible = false;

        }
        private void EditarActivarControlesRol()
        {
            TxtCodRol.Enabled = true;
            TxtNomRol.Enabled = true;
            TxtDescRol.Enabled = true;
            RBtnListStatusRol.Enabled = true;
            Panel_Roles.Enabled = true;
            Panel_Nivel.Enabled = false;
            Panel_Perfil.Enabled = false;
            Panel_Usuario.Enabled = false;
            //Panel_Asignación.Enabled = false;
        }
        private void BuscarActivarbotnesRol()
        {
            btnCrearRol.Visible = false;
            btnsaveus.Visible = false;
            btnConsultarRol.Visible = true;
            btnEditRol.Visible = true;
            btnActuRol.Visible = false;
            btnCancelRol.Visible = true;
                      
        }


        private void SavelimpiarControlesNivel()
        {
            txtcodnive.Text = "";
            TxtnomNivel.Text = "";
            CheckModulo.Items.Clear();
            RblistEstnivel.Items[0].Selected = true;
            RblistEstnivel.Items[1].Selected = false;

            TxtBCodNivel.Text = "";
            cmbBuscarNivel.Text = "0";
            //TxtBNomNivel.Text = "";
        }
        private void activarControlesNivel()
        {
            txtcodnive.Enabled = true;
            TxtnomNivel.Enabled = true;
            CheckModulo.Enabled = true;
            RblistEstnivel.Enabled = false;
            Panel_Roles.Enabled = false;
            Panel_Nivel.Enabled = true;
            Panel_Perfil.Enabled = false;
            Panel_Usuario.Enabled = false;
            //Panel_Asignación.Enabled = false;
        }
        private void desactivarControlesNivel()
        {
           
            txtcodnive.Enabled = false;
            TxtnomNivel.Enabled = false;
            CheckModulo.Enabled = false;
            RblistEstnivel.Enabled = false;
            Panel_Roles.Enabled = true;
            Panel_Nivel.Enabled = true;
            Panel_Perfil.Enabled = true;
            Panel_Usuario.Enabled = true;
            //Panel_Asignación.Enabled = true;
        }
        private void crearActivarbotonesNivel()
        {
            btnCrearNive.Visible = false;
            btnSaveNive.Visible = true;
            btnSearchNive.Visible = false;
            btneditNive.Visible = false;
            btnupdNive.Visible = false;
            btnCancelNive.Visible = true;
            btnPregNive.Visible = false;
            btnAregNive.Visible = false;
            btnSregNive.Visible = false;
            btnUregNive.Visible = false;

        }
        private void saveActivarbotonesNivel()
        {
            btnCrearNive.Visible = true;
            btnSaveNive.Visible = false;
            btnSearchNive.Visible = true;
            btneditNive.Visible = false;
            btnupdNive.Visible = false;
            btnCancelNive.Visible = true;
            btnPregNive.Visible = false;
            btnAregNive.Visible = false;
            btnSregNive.Visible = false;
            btnUregNive.Visible = false;
        }
        private void EditarActivarbotonesNivel()
        {
            btnCrearNive.Visible = false;
            btnSaveNive.Visible = false;
            btnSearchNive.Visible = true;
            btneditNive.Visible = false;
            btnupdNive.Visible = true;
            btnCancelNive.Visible = true;
            btnPregNive.Visible = false;
            btnAregNive.Visible = false;
            btnSregNive.Visible = false;
            btnUregNive.Visible = false;
        }
        private void EditarActivarControlesNivel()
        {
            txtcodnive.Enabled = true;
            TxtnomNivel.Enabled = true;
            CheckModulo.Enabled = true;
            RblistEstnivel.Enabled = true;
            Panel_Roles.Enabled = false;
            Panel_Nivel.Enabled = true;
            Panel_Perfil.Enabled = false;
            Panel_Usuario.Enabled = false;          

        }
        private void BuscarActivarbotnesNivel()
        {
            btnCrearNive.Visible = false;
            btnSaveNive.Visible = false;
            btnSearchNive.Visible = true;
            btneditNive.Visible = true;
            btnupdNive.Visible = false;
            btnCancelNive.Visible = true;
        }
        private void LlenaCmbModuloNivel()
        {
            DataSet ds1 = new DataSet();
            ds1 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 51);
            CheckModulo.DataSource = ds1;
            CheckModulo.DataTextField = "Modulo_Name";
            CheckModulo.DataValueField = "Modulo_id";
            CheckModulo.DataBind();
        }
        private void consultaUltimoIdNivel()
        {
            //Consulta ultimo regitro de la tabla Person_Level al que se va a insertar los modulos seleccionados.
            DataTable dt = new DataTable();
            dt = obtenerid.ConsultaUltimoiddeNivel();
            this.Session["id_Level"] = dt.Rows[0]["id_Level"].ToString().Trim();
            dt = null;
        }
        private void comboNivelConsultanNivel()
        {
            DataTable dt = new DataTable();
            dt = obtenerid.ConsultallenarcomboNivel();
            //Se llena niveles en perfiles
            cmbBuscarNivel.DataSource = dt;
            cmbBuscarNivel.DataTextField = "Level_Description";
            cmbBuscarNivel.DataValueField = "id_Level";
            cmbBuscarNivel.DataBind();
            dt = null;

        }
        



        private void SavelimpiarControlesPerfil()
        {
            DdlSelRol.Text = "0";
            DdlSelMod.Text = "0";
            ddlchannel.Text = "0";
            
            cmbniveluser.Text = "0";
            TxtCodPerfil.Text = "";
            TxtNomPerfil.Text = "";
            TxtDescPerfil.Text = "";
            RBtnListStatusPerfil.Items[0].Selected = true;
            RBtnListStatusPerfil.Items[1].Selected = false;

            TxtBNomPerfil.Text = "";
            cmbBRolAs.Text = "0";
            cmbSniveluser.Text = "0";
        }
        private void activarControlesPerfil()
        {
            DdlSelRol.Enabled = true;
            DdlSelMod.Enabled = true;
            ddlchannel.Enabled = true;
            cmbniveluser.Enabled = true;
            RBtnListStatusPerfil.Enabled = false;
            TxtCodPerfil.Enabled = true;
            TxtNomPerfil.Enabled = true;
            TxtDescPerfil.Enabled = true;
            Panel_Roles.Enabled = false;
            Panel_Nivel.Enabled = false;
            Panel_Perfil.Enabled = true;
            Panel_Usuario.Enabled = false;
            //Panel_Asignación.Enabled = false;
        }
        private void desactivarControlesPerfil()
        {
            DdlSelRol.Enabled = false;
            DdlSelMod.Enabled = false;
            ddlchannel.Enabled = false;
            cmbniveluser.Enabled = false;
            RBtnListStatusPerfil.Enabled = false;
            TxtCodPerfil.Enabled = false;
            TxtNomPerfil.Enabled = false;
            TxtDescPerfil.Enabled = false;
            Panel_Roles.Enabled = true;
            Panel_Nivel.Enabled = true;
            Panel_Perfil.Enabled = true;
            Panel_Usuario.Enabled = true;
            //Panel_Asignación.Enabled = true;
        }

        private void crearActivarbotonesPerfil()
        {            
            btnCrearPer.Visible = false;
            BtnGuardarPer.Visible = true;
            btnConsultarPer.Visible = false;
            btnEditPerfil.Visible = false;
            btnActualizarPer.Visible = false;
            btnCancelPer.Visible = true;
            btnPreg1.Visible = false;
            btnAreg1.Visible = false;
            btnSreg1.Visible = false;
            btnUreg1.Visible = false;
        }

        private void saveActivarbotonesPerfil()
        {
            btnCrearPer.Visible = true;
            BtnGuardarPer.Visible = false;
            btnConsultarPer.Visible = true;
            btnEditPerfil.Visible = false;
            btnActualizarPer.Visible = false;
            btnCancelPer.Visible = true;
            btnPreg1.Visible = false;
            btnAreg1.Visible = false;
            btnSreg1.Visible = false;
            btnUreg1.Visible = false;
        }

        private void EditarActivarbotonesPerfil()
        {
            btnCrearPer.Visible = false;
            BtnGuardarPer.Visible = false;
            btnConsultarPer.Visible = true;
            btnEditPerfil.Visible = false;
            btnActualizarPer.Visible = true;
            btnCancelPer.Visible = true;
            btnPreg1.Visible = false;
            btnAreg1.Visible = false;
            btnSreg1.Visible = false;
            btnUreg1.Visible = false;
        }

        private void EditarActivarControlesPerfil()
        {
            DdlSelRol.Enabled = true;
            DdlSelMod.Enabled = true;
            ddlchannel.Enabled = true;
            cmbniveluser.Enabled = true;
            RBtnListStatusPerfil.Enabled = true;
            TxtCodPerfil.Enabled = false;
            TxtNomPerfil.Enabled = true;
            TxtDescPerfil.Enabled = true;
            Panel_Roles.Enabled = false;
            Panel_Nivel.Enabled = false;
            Panel_Perfil.Enabled = true;
            Panel_Usuario.Enabled = false;
            //Panel_Asignación.Enabled = false;
        }

        private void BuscarActivarbotnesPerfil()
        {
            btnCrearPer.Visible = false;
            BtnGuardarPer.Visible = false;
            btnConsultarPer.Visible = true;
            btnEditPerfil.Visible = true;
            btnActualizarPer.Visible = false;
            btnCancelPer.Visible = true;
        }

        private void ComboModuloenPerfil()
        {
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 5);
            DdlSelMod.DataSource = ds;
            DdlSelMod.DataTextField = "Modulo_Name";
            DdlSelMod.DataValueField = "Modulo_id";
            DdlSelMod.DataBind();
        }

        private void comboRolenPerfil()
        {
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 6);
            //se llena roles en perfiles
            DdlSelRol.DataSource = ds;
            DdlSelRol.DataTextField = "Rol_Name";
            DdlSelRol.DataValueField = "Rol_id";
            DdlSelRol.DataBind();
        }

        /** 
         * Se crea metodo para llenar combo de Canales
         * 24/05/2011  Angel Ortiz
         * **/
        private void comboCanalenPerfil()
        {
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 9);
            ddlchannel.DataSource = ds;
            ddlchannel.DataTextField = "Channel_Name";
            ddlchannel.DataValueField = "cod_Channel";
            ddlchannel.DataBind();
            ddlchannel.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            ds = null;
        }

        private void comboCanalenBuscarperfil()
        {
            //DataSet data = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 9);
            ddlchannelb.DataSource = ds;
            ddlchannelb.DataTextField = "Channel_Name";
            ddlchannelb.DataValueField = "cod_Channel";
            ddlchannelb.DataBind();
            ddlchannelb.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            ds = null;
        }

        private void comboRolenBuscarperfil() 
        {
            //se llena roles en buscar perfiles
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 30);
            cmbBRolAs.DataSource = ds;
            cmbBRolAs.DataTextField = "Rol_Name";
            cmbBRolAs.DataValueField = "Rol_id";
            cmbBRolAs.DataBind();
            ds = null;
        }
        private void comboNivelenPerfil()
        {
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 43);
            //Se llena niveles en perfiles
            cmbniveluser.DataSource = ds;
            cmbniveluser.DataTextField = "Level_Description";
            cmbniveluser.DataValueField = "id_Level";
            cmbniveluser.DataBind();
            ds = null;
            
        }
        private void comboNivelbuscarPerfil()
        {
            ds1 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 44);
            //Se llena niveles en buscar perfiles
            cmbSniveluser.DataSource = ds1;
            cmbSniveluser.DataTextField = "Level_Description";
            cmbSniveluser.DataValueField = "id_Level";
            cmbSniveluser.DataBind();
            ds1 = null;
        }
                  
       

        private void SavelimpiarControlesUsuario()
        {
            txtCodUsu.Text = "";
            TxtNumDoc.Text = "";
            TxtPNom.Text = "";
            TxtSNom.Text = "";
            TxtPApe.Text = "";
            TxtSApe.Text = "";
            TxtPalabra.Text = "";
            TxtUsu.Text = "";
            TxtPsw.Text = "";
            TxtMail.Text = "";
            TxtTel.Text = "";
            TxtDir.Text = "";
            cmbdoc.Text = "0";
            cmbmodul.Text = "0";
            cmbperfil.Items.Clear();
            cmbcompany.Text = "0";
            cmbcontry.Text = "0";
            RBtnListStatusUsu.Items[0].Selected = true;
            RBtnListStatusUsu.Items[1].Selected = false;

            txtdoc.Text = "";
            txtuser.Text = "0";
        }
        private void activarControlesUsuario()
        {
            cmbdoc.Enabled = true;
            cmbmodul.Enabled = true;
            cmbperfil.Enabled = true;
            cmbcompany.Enabled = true;
            cmbcontry.Enabled = true;
            RBtnListStatusUsu.Enabled = false;
            txtCodUsu.Enabled = false;
            TxtNumDoc.Enabled = true;
            TxtPNom.Enabled = true;
            TxtSNom.Enabled = true;
            TxtPApe.Enabled = true;
            TxtSApe.Enabled = true;
            TxtPalabra.Enabled = true;
            TxtUsu.Enabled = false;
            TxtPsw.Enabled = false;
            TxtMail.Enabled = true;
            TxtTel.Enabled = true;
            TxtDir.Enabled = true;
            Panel_Roles.Enabled = false;
            Panel_Nivel.Enabled = false;
            Panel_Perfil.Enabled = false;
            Panel_Usuario.Enabled = true;
            //Panel_Asignación.Enabled = false;
        }
        private void desactivarControlesUsuario()
        {
            cmbdoc.Enabled = false;
            cmbmodul.Enabled = false;
            cmbperfil.Enabled = false;
            cmbcompany.Enabled = false;
            cmbcontry.Enabled = false;
            RBtnListStatusUsu.Enabled = false;
            txtCodUsu.Enabled = false;
            TxtNumDoc.Enabled = false;
            TxtPNom.Enabled = false;
            TxtSNom.Enabled = false;
            TxtPApe.Enabled = false;
            TxtSApe.Enabled = false;
            TxtPalabra.Enabled = false;
            TxtUsu.Enabled = false;
            TxtPsw.Enabled = false;
            TxtMail.Enabled = false;
            TxtTel.Enabled = false;
            TxtDir.Enabled = false;
            Panel_Roles.Enabled = true;
            Panel_Nivel.Enabled = true;
            Panel_Perfil.Enabled = true;
            Panel_Usuario.Enabled = true;
            //Panel_Asignación.Enabled = true;
        }
        private void crearActivarbotonesUsuario()
        {
            btnCrearUsu.Visible = false;
            BtnGuardarUsu.Visible = true;
            btnConsultarUsu.Visible = false;
            btnEditAct.Visible = false;
            btnActua.Visible = false;
            btnCancelarUsu.Visible = true;
            btnPreg2.Visible = false;
            btnAreg2.Visible = false;
            btnSreg2.Visible = false;
            btnUreg2.Visible = false;

        }
        private void saveActivarbotonesUsuario()
        {
            btnCrearUsu.Visible = true;
            BtnGuardarUsu.Visible = false;
            btnConsultarUsu.Visible = true;
            btnEditAct.Visible = false;
            btnActua.Visible = false;
            btnCancelarUsu.Visible = true;
            btnPreg2.Visible = false;
            btnAreg2.Visible = false;
            btnSreg2.Visible = false;
            btnUreg2.Visible = false;
        }
        private void EditarActivarbotonesUsuario()
        {
            btnCrearUsu.Visible = false;
            BtnGuardarUsu.Visible = false;
            btnConsultarUsu.Visible = true;
            btnEditAct.Visible = false;
            btnActua.Visible = true;
            btnCancelarUsu.Visible = true;
            btnPreg2.Visible = false;
            btnAreg2.Visible = false;
            btnSreg2.Visible = false;
            btnUreg2.Visible = false;

         }
        private void EditarActivarControlesUsuario()
        {
            cmbdoc.Enabled = true;
            cmbmodul.Enabled = true;
            cmbperfil.Enabled = true;
            cmbcompany.Enabled = true;
            cmbcontry.Enabled = true;
            RBtnListStatusUsu.Enabled = true;
            txtCodUsu.Enabled = false;
            TxtNumDoc.Enabled = true;
            TxtPNom.Enabled = true;
            TxtSNom.Enabled = true;
            TxtPApe.Enabled = true;
            TxtSApe.Enabled = true;
            TxtPalabra.Enabled = true;
            TxtUsu.Enabled = false;
            TxtPsw.Enabled = false;
            TxtMail.Enabled = true;
            TxtTel.Enabled = true;
            TxtDir.Enabled = true;
            Panel_Roles.Enabled = false;
            Panel_Nivel.Enabled = false;
            Panel_Perfil.Enabled = false;
            Panel_Usuario.Enabled = true;
            //Panel_Asignación.Enabled = false;

        }
        private void BuscarActivarbotnesUsuario()
        {
            btnCrearUsu.Visible = false;
            BtnGuardarUsu.Visible = false;
            btnConsultarUsu.Visible = true;
            btnEditAct.Visible = true;
            btnActua.Visible = false;
            btnCancelarUsu.Visible = true;
        }
        private void combousuarios()
        {
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 23);
            txtuser.DataSource = ds;
            txtuser.DataTextField = "name_user";
            txtuser.DataValueField = "Person_id";
            txtuser.DataBind();
            //txtuser.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }

        private void comboperfilausuario()
        {
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOPERFILES", cmbmodul.SelectedValue);
            cmbperfil.DataSource = ds;
            cmbperfil.DataTextField = "Perfil_Name";
            cmbperfil.DataValueField = "Perfil_id";
            cmbperfil.DataBind();
            ds = null;
        }
        private void combomoduloenusuario()
        {
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 5);
            //se llena modulo en usuarios
            cmbmodul.DataSource = ds;
            cmbmodul.DataTextField = "Modulo_Name";
            cmbmodul.DataValueField = "Modulo_id";
            cmbmodul.DataBind();
        }
        private void comboclienteenusuario()
        {
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en Usuarios
            cmbcompany.DataSource = ds;
            cmbcompany.DataTextField = "Company_Name";
            cmbcompany.DataValueField = "Company_id";
            cmbcompany.DataBind();
        }
        private void combopaisenUsuario()
        {
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 2);
            //se llena paises en usuarios
            cmbcontry.DataSource = ds;
            cmbcontry.DataTextField = "Name_Country";
            cmbcontry.DataValueField = "cod_Country";
            cmbcontry.DataBind();
        }
        private void combotipoDocenUsuario()
        {

            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 1);
            cmbdoc.DataSource = ds;
            cmbdoc.DataTextField = "Type_documento";
            cmbdoc.DataValueField = "id_typeDocument";
            cmbdoc.DataBind();

            //ds = null;

        }

        //private void SavelimpiarControlesAsignación()
        //{
        //    CmbSelPaisDirCu.Text = "0";
        //    CmbSelDirCu.Text = "0";
        //    LstBoxPersonalSinAsing.Text = "0";
        //    LstBoxPersonalAsing.Text = "0";          
        //}
        //private void activarControlesAsignación()
        //{
        //    CmbSelPaisDirCu.Enabled = true;
        //    CmbSelDirCu.Enabled = true;
        //    LstBoxPersonalSinAsing.Enabled = true;
        //    LstBoxPersonalAsing.Enabled = true;
        //    BtnMasAsing.Enabled = true;
        //    BtnMenosAsing.Enabled = true;
        //    Panel_Roles.Enabled = false;
        //    Panel_Nivel.Enabled = false;
        //    Panel_Perfil.Enabled = false;
        //    Panel_Usuario.Enabled = false;
        //    Panel_Asignación.Enabled = true;
        //}
        //private void desactivarControlesAsignación()
        //{
        //    CmbSelPaisDirCu.Enabled = false;
        //    CmbSelDirCu.Enabled = false;
        //    LstBoxPersonalSinAsing.Enabled = false;
        //    LstBoxPersonalAsing.Enabled = false;
        //    BtnMasAsing.Enabled = false;
        //    BtnMenosAsing.Enabled = false;
        //    Panel_Roles.Enabled = true;
        //    Panel_Nivel.Enabled = true;
        //    Panel_Perfil.Enabled = true;
        //    Panel_Usuario.Enabled = true;
        //    Panel_Asignación.Enabled = true;
        //}
        //private void crearActivarbotonesAsignación()
        //{                     
        //    BtnCrearAsignEje.Visible = false;
        //    BtnSaveAsignEje.Visible = true;
        //    BtnSearchAsignEje.Visible = false;
        //    BtnEditAsignEje.Visible = false;
        //    BtnUpdateAsignEje.Visible = false;
        //    BtnCancelAsignEje.Visible = true;
           
        //}
        //private void saveActivarbotonesAsignación()
        //{
        //    BtnCrearAsignEje.Visible = true;
        //    BtnSaveAsignEje.Visible = false;
        //    BtnSearchAsignEje.Visible = true;
        //    BtnEditAsignEje.Visible = false;
        //    BtnUpdateAsignEje.Visible = false;
        //    BtnCancelAsignEje.Visible = true;
            
        //}
        //private void EditarActivarbotonesAsignación()
        //{
   
        //    BtnCrearAsignEje.Visible = false;
        //    BtnSaveAsignEje.Visible = false;
        //    BtnSearchAsignEje.Visible = true;
        //    BtnEditAsignEje.Visible = false;
        //    BtnUpdateAsignEje.Visible = true;
        //    BtnCancelAsignEje.Visible = true;

        // }
        //private void EditarActivarControlesAsignación()
        //{          
                
        //    CmbSelPaisDirCu.Enabled = true;
        //    CmbSelDirCu.Enabled = true;
        //    LstBoxPersonalSinAsing.Enabled = true;
        //    LstBoxPersonalAsing.Enabled = true;
        //    BtnMasAsing.Enabled = true;
        //    BtnMenosAsing.Enabled = true;
        //    Panel_Roles.Enabled = false;
        //    Panel_Nivel.Enabled = false;
        //    Panel_Perfil.Enabled = false;
        //    Panel_Usuario.Enabled = false;
        //    Panel_Asignación.Enabled = true;

        //}
        //private void BuscarActivarbotnesAsignación()
        //{
        //    BtnCrearAsignEje.Visible = false;
        //    BtnSaveAsignEje.Visible = false;
        //    BtnSearchAsignEje.Visible = true;
        //    BtnEditAsignEje.Visible = true;
        //    BtnUpdateAsignEje.Visible =false;
        //    BtnCancelAsignEje.Visible = true;
        //}
        //private void comboPaisAsignarDirCuenta()
        //{
        //    //se llena Paises en Maestro de asignación de ejecutivos a director de cuenta
        //    DataTable dt = new DataTable();
        //    dt = obtenerid.Get_SearchCountryDirCuenta();

        //    CmbSelPaisDirCu.DataSource = dt; 
        //    CmbSelPaisDirCu.DataTextField = "Name_Country";
        //    CmbSelPaisDirCu.DataValueField = "cod_Country";
        //    CmbSelPaisDirCu.DataBind();

        //    //se llena Paises en Buscar asignación de ejecutivos a director de cuenta
        //    cmbBSelPaisDirect.DataSource = dt;
        //    cmbBSelPaisDirect.DataTextField = "Name_Country";
        //    cmbBSelPaisDirect.DataValueField = "cod_Country";
        //    cmbBSelPaisDirect.DataBind();
        //}
        //private void comboAsignaDirCuenta()
        //{
        //    //se llena director de cuenta en maestro Asignación de ejecutivos a director de cuenta
        //    DataSet ds = new DataSet();
        //    ds = obtenerid.Get_SearchDirCuentaxPais(CmbSelPaisDirCu.SelectedValue);
        //    if (ds != null)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            CmbSelDirCu.DataSource = ds.Tables[0];
        //            CmbSelDirCu.DataTextField = "Person_Name";
        //            CmbSelDirCu.DataValueField = "Person_id";
        //            CmbSelDirCu.DataBind();

        //            if (ds.Tables[1].Rows.Count > 0)
        //            {
        //                for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
        //                {
        //                    for (int j = 0; j <= CmbSelDirCu.Items.Count - 1; j++)
        //                    {
        //                        if (ds.Tables[1].Rows[i]["Person_id_Director"].ToString().Trim() == CmbSelDirCu.Items[j].Value)
        //                        {
        //                            CmbSelDirCu.Items.Remove(CmbSelDirCu.Items[j]);
        //                        }
        //                    }
        //                }
        //            }
        //            if (CmbSelDirCu.Items.Count == 1)
        //            {
        //                Alertas.CssClass = "MensajesError";
        //                LblFaltantes.Text = "No existen Directores de Cuenta para realizar asignación o ya todos tienen asignación";
        //                MensajeAlerta();
        //            }
        //        }
        //    }
        //}
        //private void ListaEjeCuenta()
        //{
        //    //se llena ejecutivos de cuenta en maestro Asignación de ejecutivos a director de cuenta
        //    DataSet ds = new DataSet();
        //    ds = obtenerid.Get_SearchEjeCuentaxPais(CmbSelPaisDirCu.SelectedValue);
        //    if (ds != null)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            LstBoxPersonalSinAsing.DataSource = ds.Tables[0];
        //            LstBoxPersonalSinAsing.DataTextField = "Person_Name";
        //            LstBoxPersonalSinAsing.DataValueField = "Person_id";
        //            LstBoxPersonalSinAsing.DataBind();
        //            if (ds.Tables[1].Rows.Count > 0)
        //            {
        //                for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
        //                {
        //                    for (int j = 0; j <= LstBoxPersonalSinAsing.Items.Count - 1; j++)
        //                    {
        //                        if (ds.Tables[1].Rows[i]["Person_id_Ejecutive"].ToString().Trim() == LstBoxPersonalSinAsing.Items[j].Value)
        //                        {
        //                            LstBoxPersonalSinAsing.Items.Remove(LstBoxPersonalSinAsing.Items[j]);
        //                        }
        //                    }
        //                }
        //            }
        //            if (LstBoxPersonalSinAsing.Items.Count == 0)
        //            {
        //                Alertas.CssClass = "MensajesError";
        //                LblFaltantes.Text = "No existen Ejecutivos de Cuenta para realizar asignación o ya todos tienen asignación";
        //                MensajeAlerta();
        //            }
        //        }
        //    }
        //}
        //private void ListaEjeCuentaenBuscar()
        //{

        //    //se llena ejecutivos de cuenta en maestro Asignación de ejecutivos a director de cuenta
        //    DataSet ds = new DataSet();
        //    ds = obtenerid.Get_SearchEjeCuentaxPais(CmbSelPaisDirCu.Items[0].Value);
        //    if (ds != null)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            LstBoxPersonalSinAsing.DataSource = ds.Tables[0];
        //            LstBoxPersonalSinAsing.DataTextField = "Person_Name";
        //            LstBoxPersonalSinAsing.DataValueField = "Person_id";
        //            LstBoxPersonalSinAsing.DataBind();
        //            if (ds.Tables[1].Rows.Count > 0)
        //            {
        //                for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
        //                {
        //                    for (int j = 0; j <= LstBoxPersonalSinAsing.Items.Count - 1; j++)
        //                    {
        //                        if (ds.Tables[1].Rows[i]["Person_id_Ejecutive"].ToString().Trim() == LstBoxPersonalSinAsing.Items[j].Value)
        //                        {
        //                            LstBoxPersonalSinAsing.Items.Remove(LstBoxPersonalSinAsing.Items[j]);
        //                        }
        //                    }
        //                }
        //            }
        //            if (LstBoxPersonalSinAsing.Items.Count == 0)
        //            {
        //                Alertas.CssClass = "MensajesError";
        //                LblFaltantes.Text = "No existen Ejecutivos de Cuenta para realizar asignación o ya todos tienen asignación";
        //                MensajeAlerta();
        //            }
        //        }
        //    }
        //}
        //private void comboBAsignaDirCuenta()
        //{
        //    //se llena director de cuenta en Buscar Asignación de ejecutivos a director de cuenta
        //    DataTable dt = new DataTable();
        //    dt = obtenerid.Get_SearchDirCuentaconAsingacion(cmbBSelPaisDirect.SelectedValue);
        //    if (dt != null)
        //    {

        //        if (dt.Rows.Count > 1)
        //        {
        //            cmbBSelNameDirect.DataSource = dt;
        //            cmbBSelNameDirect.DataTextField = "Person_Name";
        //            cmbBSelNameDirect.DataValueField = "Person_id";
        //            cmbBSelNameDirect.DataBind();
        //        }
        //        else
        //        {
        //            ModalPopupExtender1.Hide();
        //            Alertas.CssClass = "MensajesError";
        //            LblFaltantes.Text = "No existen Directores de Cuenta con asignación realizada";
        //            MensajeAlerta();
        //        }
        //    }
        //}


        private void MensajeAlerta()
        {            
            ModalPopupAlertas.Show();
            BtnAceptarAlert.Focus();
            //ScriptManager.RegisterStartupScript(
            //    this, this.GetType(), "myscript", "alert('Debe ingresar todos los parametros con (*)');", true);
        }

        #endregion

        #region Rol
        protected void btnCrearRol_Click(object sender, EventArgs e)
        {
            activarControlesRol();
            crearActivarbotonesRol();

        }
        protected void btnsaveus_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomRol.Text = TxtNomRol.Text.TrimStart();
            TxtDescRol.Text = TxtDescRol.Text.TrimStart();
            TxtCodRol.Text = TxtCodRol.Text.TrimStart();
            if (TxtNomRol.Text == "" || TxtDescRol.Text == "" || TxtCodRol.Text == "")
            {
                if (TxtNomRol.Text == "")
                {
                    LblFaltantes.Text = ". " + "Nombre de Rol";
                }
                if (TxtDescRol.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                if (TxtCodRol.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Código de Rol";
                }

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;

            }
            //Se Coloca esta validacion para que no permita registros en 0 Ing. Carlos Hernandez 22/06/2009
            //Se quita la validacion para nombre de rol y descripción, esta validacion es controlada por el validator Ing. Mauricio Ortiz 23/06/2009
            else
            {
                if (TxtCodRol.Text == "0000")
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                    MensajeAlerta();
                    return;
                }
            }

            try
            {
                DAplicacion odconsuroles = new DAplicacion();
                DataTable dtconsulta = odconsuroles.ConsultaDuplicados(ConfigurationManager.AppSettings["Roles"], TxtNomRol.Text, null, null);
                if (dtconsulta == null)
                {
                    ERoles oeroles = oUsuario.RegistrarRoles(TxtCodRol.Text, TxtNomRol.Text.ToUpper(), TxtDescRol.Text, true, Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now), Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));
                    string sRol = "";
                    sRol = TxtNomRol.Text;
                    this.Session["sRol"] = TxtCodRol.Text;
                    this.Session["sRol"] = sRol;
                    //llenarcombos();
                    SavelimpiarControlesRol();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Rol " + this.Session["sRol"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesRol();
                    desactivarControlesRol();
                }
                else
                {
                    string sRol = "";
                    sRol = TxtNomRol.Text;
                    this.Session["sRol"] = sRol;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Rol " + this.Session["sRol"] + " Ya Existe";
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
        protected void BtnBRol_Click(object sender, EventArgs e)
        {
            desactivarControlesRol();
            LblFaltantes.Text = "";
            TxtBNomRol.Text = TxtBNomRol.Text.TrimStart();
            if (TxtBNomRol.Text == "")
            {
                this.Session["mensajealert"] = "Nombre de Rol";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnRol_ModalPopupExtender.Show();
                return;

            }
            //Se coloca esta validacion para no permitir consultas en cero Ing. Carlos Hernandez
            else
            {
                if (TxtBNomRol.Text == "0")
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                    MensajeAlerta();
                    IbtnRol_ModalPopupExtender.Show();
                    return;
                }
            }
            BuscarActivarbotnesRol();
            //btnCrearRol.Visible = true;
            sRolname = TxtBNomRol.Text.ToUpper();
            TxtBNomRol.Text = "";
            
                DataTable oeroles = oUsuario.BuscarRoles(sRolname);
                if (oeroles != null)
                {
                    if (oeroles.Rows.Count > 0)
                    {
                        for (int i = 0; i <= oeroles.Rows.Count - 1; i++)
                        {
                            TxtCodRol.Text = oeroles.Rows[0]["Rol_id"].ToString().Trim();
                            TxtNomRol.Text = oeroles.Rows[0]["Rol_Name"].ToString().Trim();
                            TxtDescRol.Text = oeroles.Rows[0]["Rol_Description"].ToString().Trim();
                            estado = Convert.ToBoolean(oeroles.Rows[0]["Rol_Status"].ToString().Trim());
                            if (estado == true)
                            {
                                RBtnListStatusRol.Items[0].Selected = true;
                                RBtnListStatusRol.Items[1].Selected = false;

                            }
                            else
                            {
                                RBtnListStatusRol.Items[0].Selected = false;
                                RBtnListStatusRol.Items[1].Selected = true;
                            }
                            this.Session["troles"] = oeroles;
                            this.Session["i"] = 0;

                        }
                        if (oeroles.Rows.Count == 1)
                        {
                            btnPreg0.Visible = false;
                            btnAreg0.Visible = false;
                            btnSreg0.Visible = false;
                            btnUreg0.Visible = false;
                        }
                        else
                        {
                            btnPreg0.Visible = true;
                            btnAreg0.Visible = true;
                            btnSreg0.Visible = true;
                            btnUreg0.Visible = true;
                        }
                    }
                    else
                    {
                        SavelimpiarControlesRol();
                        saveActivarbotonesRol();
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                        MensajeAlerta();
                        IbtnRol_ModalPopupExtender.Show();
                    }
                }
           
        }
        protected void btnEditRol_Click(object sender, EventArgs e)
        {
            btnEditRol.Visible = false;
            btnActuRol.Visible = true;
            activarControlesRol();
            TxtCodRol.Enabled = false;
            RBtnListStatusRol.Enabled = true;
            this.Session["rept"] = TxtNomRol.Text;
        }
        protected void btnActuRol_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomRol.Text = TxtNomRol.Text.TrimStart();
            TxtDescRol.Text = TxtDescRol.Text.TrimStart();
            TxtCodRol.Text = TxtCodRol.Text.TrimStart();
            if (TxtNomRol.Text == "" || TxtDescRol.Text == "" || TxtCodRol.Text == "")
            {
                if (TxtNomRol.Text == "")
                {
                    LblFaltantes.Text = ". " + "Nombre de Rol";
                }
                if (TxtDescRol.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                if (TxtCodRol.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Código de Rol";
                }

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
                if (RBtnListStatusRol.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabrol = new DAplicacion();
                    DataTable dt = oddeshabrol.PermitirDeshabilitar(ConfigurationManager.AppSettings["RolesProfiles"], TxtCodRol.Text);
                    if (dt != null)
                    {
                       
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Perfil, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }

                }
                repetido = Convert.ToString(this.Session["rept"]);
                if (repetido != TxtNomRol.Text)
                {

                    DAplicacion odconsuroles = new DAplicacion();
                    DataTable dtconsulta = odconsuroles.ConsultaDuplicados(ConfigurationManager.AppSettings["Roles"], TxtNomRol.Text, null, null);
                    if (dtconsulta == null)
                    {
                        ERoles oeaUsuario = oUsuario.ActualizaRol(TxtCodRol.Text, TxtNomRol.Text.ToUpper(), TxtDescRol.Text, estado, Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));
                        sNomRol = TxtNomRol.Text;
                        this.Session["sNomRol"] = sNomRol;
                        //llenarcombos();
                        SavelimpiarControlesRol();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Rol : " + this.Session["sNomRol"] + " fue Actualizado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesRol();
                        desactivarControlesRol();

                    }
                    else
                    {
                        sNomRol = TxtNomRol.Text;
                        this.Session["sNomRol"] = sNomRol;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Rol : " + this.Session["sNomRol"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    ERoles oeaUsuario = oUsuario.ActualizaRol(TxtCodRol.Text, TxtNomRol.Text.ToUpper(), TxtDescRol.Text, estado, Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));
                    sNomRol = TxtNomRol.Text;
                    this.Session["sNomRol"] = sNomRol;
                    //llenarcombos();
                    SavelimpiarControlesRol();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Rol : " + this.Session["sNomRol"] + " fue Actualizado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesRol();
                    desactivarControlesRol();

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
        protected void btnCancelRol_Click(object sender, EventArgs e)
        {
            SavelimpiarControlesRol();
            saveActivarbotonesRol();
            desactivarControlesRol();

        }
        private void MostrarDatosRol()
        {
            recorrido = (DataTable)this.Session["troles"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodRol.Text = recorrido.Rows[recsearch]["Rol_id"].ToString().Trim();
                    TxtNomRol.Text = recorrido.Rows[recsearch]["Rol_Name"].ToString().Trim();
                    TxtDescRol.Text = recorrido.Rows[recsearch]["Rol_Description"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Rol_Status"].ToString().Trim());
                    if (estado == true)
                    {
                        RBtnListStatusRol.Items[0].Selected = true;
                        RBtnListStatusRol.Items[1].Selected = false;
                    }
                    else
                    {
                        RBtnListStatusRol.Items[0].Selected = false;
                        RBtnListStatusRol.Items[1].Selected = true;
                    }


                }
            }
        }
        protected void btnPreg0_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["troles"];
            MostrarDatosRol();
        }
        protected void btnAreg0_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosRol();
            }

        }
        protected void btnSreg0_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["troles"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosRol();
            }
        }
        protected void btnUreg0_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["troles"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosRol();
        }
        #endregion

        #region Nivel
        protected void btnCrearNive_Click(object sender, EventArgs e)
        {
            LlenaCmbModuloNivel();
            activarControlesNivel();
            crearActivarbotonesNivel(); 
        }
        protected void btnSaveNive_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            txtcodnive.Text = txtcodnive.Text.TrimStart();
            TxtnomNivel.Text = TxtnomNivel.Text.TrimStart();
            if (txtcodnive.Text == "" || TxtnomNivel.Text == "")
            {
                if (txtcodnive.Text == "")
                {
                    LblFaltantes.Text = ". " + "Código Nivel";
                }
                if (TxtnomNivel.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Nivel";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            else
            {
                if (txtcodnive.Text == "0000")
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                    MensajeAlerta();
                    return;
                }
            }
            try
            {
                if (RblistEstnivel.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }
                DAplicacion odconsuNiveles = new DAplicacion();
                DataTable dtconsulta = odconsuNiveles.ConsultaDuplicados(ConfigurationManager.AppSettings["Niveles"], TxtnomNivel.Text, null, null);
                if (dtconsulta == null)
                {
                    EPersonLevel oePersonLevel = oUsuario.RegistrarNiveles(txtcodnive.Text, TxtnomNivel.Text.ToUpper(), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    //consultaUltimoIdNivel();
                    for (int i = 0; i <= CheckModulo.Items.Count - 1; i++)
                    {
                        if (CheckModulo.Items[i].Selected == true)
                        {
                            EPersonLevel oePersonLevelNivel = oUsuario.RegistrarNivelesModulo(txtcodnive.Text, CheckModulo.Items[i].Value, CheckModulo.Items[i].Text,true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        }
                    }                    
                    string sNivel = "";
                    sNivel = TxtnomNivel.Text;
                    this.Session["sNivel"] = sNivel;
                    SavelimpiarControlesNivel();
                    comboNivelConsultanNivel();
                    comboNivelenPerfil();
                    comboNivelbuscarPerfil();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Nivel de usuario " + this.Session["sNivel"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesNivel();
                    desactivarControlesNivel();
                }
                else
                {
                    string sNivel = "";
                    sNivel = TxtnomNivel.Text;
                    this.Session["sNivel"] = sNivel;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Nivel de usuario " + this.Session["sNivel"] + " Ya Existe";
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
        protected void BtnBNivel_Click(object sender, EventArgs e)
        {
            desactivarControlesNivel();
            LblFaltantes.Text = "";
            cmbBuscarNivel.Text = cmbBuscarNivel.Text;
            TxtBCodNivel.Text = TxtBCodNivel.Text.TrimStart();
           

            if (TxtBCodNivel.Text == "" && cmbBuscarNivel.Text == "0")
            {
                this.Session["mensajealert"] = "Código y/o Nombre del Nivel de usuario";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnNivel_ModalPopupExtender.Show();
                return;

            }
            BuscarActivarbotnesNivel();
            sCodNivel = TxtBCodNivel.Text;
            SNomNivel = cmbBuscarNivel.Text;
            TxtBCodNivel.Text = "";
            cmbBuscarNivel.Text = "0";
            DataTable oeNiveles = oUsuario.BuscarNiveles(sCodNivel, SNomNivel);

            if (oeNiveles != null)
            {
                if (oeNiveles.Rows.Count > 0)
                {
                    //for (int i = 0; i <= oeNiveles.Rows.Count - 1; i++)
                    //{
                        txtcodnive.Text = oeNiveles.Rows[0]["id_Level"].ToString().Trim();
                        TxtnomNivel.Text = oeNiveles.Rows[0]["Level_Description"].ToString().Trim();
                        LlenaCmbModuloNivel();
                        for (int y = 0; y <= oeNiveles.Rows.Count-1 ; y++)
                        {
                            for (int j = 0; j <= CheckModulo.Items.Count - 1; j++)
                            {
                                string tabla = oeNiveles.Rows[y][7].ToString().Trim();
                                string ch = CheckModulo.Items[j].Value;
                                string dato = oeNiveles.Rows[y][7].ToString().Trim();
                                if (oeNiveles.Rows[y][7].ToString().Trim() == CheckModulo.Items[j].Value)
                                {  ///compara los que se encuentran en el check y los de la tabla si son iguales los chulea si no los deja en false
                                    if (oeNiveles.Rows[y][9].ToString().Trim() == "True")
                                    {
                                        CheckModulo.Items[j].Selected = true;
                                    }
                                    else
                                    {
                                        CheckModulo.Items[j].Selected = false;
                                    }
                                }
                            }
                        }
                        estado = Convert.ToBoolean(oeNiveles.Rows[0]["Level_status"].ToString().Trim());

                        if (estado == true)
                        {
                            RblistEstnivel.Items[0].Selected = true;
                            RblistEstnivel.Items[1].Selected = false;
                        }
                        else
                        {
                            RblistEstnivel.Items[0].Selected = false;
                            RblistEstnivel.Items[1].Selected = true;
                        }
                        this.Session["tNiveles"] = oeNiveles;
                        this.Session["i"] = 0;
                    //}

                    if (oeNiveles.Rows.Count == 1)
                    {
                        btnPregNive.Visible = false;
                        btnAregNive.Visible = false;
                        btnSregNive.Visible = false;
                        btnUregNive.Visible = false;
                    }
                    else
                    {
                        btnPregNive.Visible = true;
                        btnAregNive.Visible = true;
                        btnSregNive.Visible = true;
                        btnUregNive.Visible = true;
                    }
                }
                else
                {
                    SavelimpiarControlesNivel();
                    saveActivarbotonesNivel();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnNivel_ModalPopupExtender.Show();
                }
            }
        }
        protected void btneditNive_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesNivel();
            EditarActivarControlesNivel();
            this.Session["rept"] = TxtnomNivel.Text;
           
        }
        protected void btnupdNive_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtnomNivel.Text = TxtnomNivel.Text.ToUpper();
            if (TxtnomNivel.Text == "")
            {
                LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Nivel";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {
                if (RblistEstnivel.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabNivel = new DAplicacion();
                    DataTable dt = oddeshabNivel.PermitirDeshabilitar(ConfigurationManager.AppSettings["NivelesappLucky"], txtcodnive.Text);
                    if (dt != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en otro maestro, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt1 = oddeshabNivel.PermitirDeshabilitar(ConfigurationManager.AppSettings["NivelesProfiles"], txtcodnive.Text);
                    if (dt1 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Perfil, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }

                }
                repetido = Convert.ToString(this.Session["rept"]);
                if (repetido != TxtnomNivel.Text)
                {
                    DAplicacion odconsuNiveles = new DAplicacion();
                    DataTable dtconsulta = odconsuNiveles.ConsultaDuplicados(ConfigurationManager.AppSettings["Niveles"], TxtnomNivel.Text, null, null);
                    if (dtconsulta == null)
                    {
                        EPersonLevel oeNiveles = oUsuario.Actualizar_Niveles(txtcodnive.Text, TxtnomNivel.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        DataTable dt = (DataTable)this.Session["tNiveles"];
                                for (int j = 0; j <= CheckModulo.Items.Count - 1; j++)
                    {

                        for (int y = 0; y <= dt.Rows.Count - 1; y++)
                        {
                            if (dt.Rows[y][7].ToString().Trim() == CheckModulo.Items[j].Value)
                            {
                                EPersonLevel oeNivelesModulos = oUsuario.Actualizar_NivelesModulos(txtcodnive.Text, CheckModulo.Items[j].Value, CheckModulo.Items[j].Selected, Convert.ToString(this.Session["sUser"]), DateTime.Now);


                                y = dt.Rows.Count - 1;

                            }
                        }
                    }
                    for (int j = 0; j <= CheckModulo.Items.Count - 1; j++)
                    {
                        if (CheckModulo.Items[j].Selected == true)
                        {
                            bContinuar = true;
                            for (int y = 0; y <= dt.Rows.Count - 1; y++)
                            {
                                if (dt.Rows[y][7].ToString().Trim() == CheckModulo.Items[j].Value)
                                {
                                    bContinuar = false;
                                    y = dt.Rows.Count - 1;
                                }                                
                            }
                            if (bContinuar)
                                {
                                    EPersonLevel oePersonLevelNivel = oUsuario.RegistrarNivelesModulo(txtcodnive.Text, CheckModulo.Items[j].Value, CheckModulo.Items[j].Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                }
                        }
                    
                  }


                        SNomNivel = TxtnomNivel.Text;
                        this.Session["sNomNivel"] = SNomNivel;
                        //llenarcombos();
                        SavelimpiarControlesNivel();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Nivel de usuario : " + this.Session["sNomNivel"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesNivel();
                        desactivarControlesNivel();
                    }
                    else
                    {
                        SNomNivel = TxtnomNivel.Text;
                        this.Session["sNomNivel"] = SNomNivel;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El nivel de usuario : " + this.Session["sNomNivel"] + " Ya Existe";
                        MensajeAlerta();
                    }
            }
                else
                {
                    EPersonLevel oeNiveles = oUsuario.Actualizar_Niveles(txtcodnive.Text, TxtnomNivel.Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    DataTable dt = (DataTable)this.Session["tNiveles"];
                    for (int j = 0; j <= CheckModulo.Items.Count - 1; j++)
                    {

                        for (int y = 0; y <= dt.Rows.Count - 1; y++)
                        {
                            if (dt.Rows[y][7].ToString().Trim() == CheckModulo.Items[j].Value)
                            {
                                EPersonLevel oeNivelesModulos = oUsuario.Actualizar_NivelesModulos(txtcodnive.Text,CheckModulo.Items[j].Value,  CheckModulo.Items[j].Selected, Convert.ToString(this.Session["sUser"]), DateTime.Now);


                                y = dt.Rows.Count - 1;

                            }
                        }
                    }
                    for (int j = 0; j <= CheckModulo.Items.Count - 1; j++)
                    {
                        if (CheckModulo.Items[j].Selected == true)
                        {
                            bContinuar = true;
                            for (int y = 0; y <= dt.Rows.Count - 1; y++)
                            {
                                if (dt.Rows[y][7].ToString().Trim() == CheckModulo.Items[j].Value)
                                {
                                    bContinuar = false;
                                    y = dt.Rows.Count - 1;
                                }                                
                            }
                            if (bContinuar)
                                {
                                    EPersonLevel oePersonLevelNivel = oUsuario.RegistrarNivelesModulo(txtcodnive.Text, CheckModulo.Items[j].Value, CheckModulo.Items[j].Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                }
                        }
                    }
                }

                SNomNivel = TxtnomNivel.Text;
                this.Session["sNomNivel"] = SNomNivel;
                //llenarcombos();
                SavelimpiarControlesNivel();
                Alertas.CssClass = "MensajesCorrecto";
                LblFaltantes.Text = "El Nivel de usuario : " + this.Session["sNomNivel"] + " Se Actualizo con Exito";
                MensajeAlerta();
                saveActivarbotonesNivel();
                desactivarControlesNivel();


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
        protected void btnCancelNive_Click(object sender, EventArgs e)
        {
            SavelimpiarControlesNivel();
            desactivarControlesNivel();
            saveActivarbotonesNivel();
        }
        private void MostrarDatosNivel()
        {
            recorrido = (DataTable)this.Session["tNiveles"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    txtcodnive.Text = recorrido.Rows[recsearch]["id_Level"].ToString().Trim();
                    TxtnomNivel.Text = recorrido.Rows[recsearch]["Level_Description"].ToString().Trim();
                    LlenaCmbModuloNivel();
                    for (int y = 0; y <= recorrido.Rows.Count - 1; y++)
                    {
                        for (int j = 0; j <= CheckModulo.Items.Count - 1; j++)
                        {
                            string tabla = recorrido.Rows[recsearch][7].ToString().Trim();
                            string ch = CheckModulo.Items[j].Value;
                            string dato = recorrido.Rows[recsearch][7].ToString().Trim();
                            if (recorrido.Rows[recsearch][7].ToString().Trim() == CheckModulo.Items[j].Value)
                            {  ///compara los que se encuentran en el check y los de la tabla si son iguales los chulea si no los deja en false
                                if (recorrido.Rows[recsearch][9].ToString().Trim() == "True")
                                {
                                    CheckModulo.Items[j].Selected = true;
                                }
                                else
                                {
                                    CheckModulo.Items[j].Selected = false;
                                }
                            }
                        }
                    }
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Level_status"].ToString().Trim());

                    if (estado == true)
                    {
                        RblistEstnivel.Items[0].Selected = true;
                        RblistEstnivel.Items[1].Selected = false;

                    }
                    else
                    {
                        RblistEstnivel.Items[0].Selected = false;
                        RblistEstnivel.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void btnPregNive_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tNiveles"];
            MostrarDatosNivel();
        }
        protected void btnAregNive_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosNivel();
            }
        }
        protected void btnSregNive_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tNiveles"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosNivel();
            }
        }
        protected void btnUregNive_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tNiveles"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosNivel();
        }
        #endregion

        #region Perfil
        protected void btnCrearPer_Click(object sender, EventArgs e)
        {
            //llena combos y activa controles para crear nuevo perfil
            ComboModuloenPerfil();
            comboRolenPerfil();
            comboNivelenPerfil();
            comboCanalenPerfil();
            activarControlesPerfil();
            crearActivarbotonesPerfil();
        }              
        protected void BtnGuardarPer_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtCodPerfil.Text = TxtCodPerfil.Text.TrimStart();
            TxtNomPerfil.Text = TxtNomPerfil.Text.TrimStart();
            TxtDescPerfil.Text = TxtDescPerfil.Text.TrimStart();
            if (TxtCodPerfil.Text == "" || TxtNomPerfil.Text == "" || TxtDescPerfil.Text == "" || DdlSelRol.Text == "0" || DdlSelMod.Text == "0" || cmbniveluser.Text == "0" || ddlchannel.Text == "0")
            {//genera mensaje de alerta al faltar ingresar compo requerido
                if (TxtCodPerfil.Text == "")
                {
                    LblFaltantes.Text = ". " + "Codigo Perfil";
                }
                if (TxtNomPerfil.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Perfil";
                }
                if (TxtDescPerfil.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                if (DdlSelRol.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Rol";
                }
                if (ddlchannel.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                if (DdlSelMod.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Módulo";
                }
                if (cmbniveluser.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nivel de Usuario";
                }

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            else
            {
                if (TxtCodPerfil.Text == "000")
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
                    MensajeAlerta();
                    return;
                }
            }
            try
            {
                if (RBtnListStatusPerfil.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }
                DAplicacion odconsuperfiles = new DAplicacion();
                DataTable dtconsulta = odconsuperfiles.ConsultaDuplicados(ConfigurationManager.AppSettings["Perfiles"], TxtNomPerfil.Text, null, null);
                if (dtconsulta == null)
                {
                    EProfiles oeperfiles = oUsuario.RegistrarProfiles(TxtCodPerfil.Text, DdlSelRol.SelectedValue, cmbniveluser.SelectedValue, TxtNomPerfil.Text.ToUpper(), DdlSelMod.SelectedValue.ToString(), TxtDescPerfil.Text, Convert.ToInt32(ddlchannel.SelectedValue), estado, Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now), Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));

                   
                    string sPerfil = "";
                    sPerfil = TxtNomPerfil.Text;
                    this.Session["sPerfil"] = sPerfil;
                    SavelimpiarControlesPerfil();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Perfil " + this.Session["sPerfil"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesPerfil();
                    desactivarControlesPerfil();
                }
                else
                {
                    string sPerfil = "";
                    sPerfil = TxtNomPerfil.Text;
                    this.Session["sPerfil"] = sPerfil;
                    this.Session["mensajealert"] = "El Perfil " + this.Session["sPerfil"];
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Nivel de usuario " + this.Session["sNivel"] + " Ya Existe";
                    MensajeAlerta();
                }

            }
            catch (Exception ex)
            {

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);


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
        protected void BtnBPerfil_Click(object sender, EventArgs e)
        {
            desactivarControlesPerfil();
            LblFaltantes.Text = "";
            TxtBNomPerfil.Text = TxtBNomPerfil.Text.TrimStart();
            if (TxtBNomPerfil.Text == "" && cmbBRolAs.Text == "0" && cmbSniveluser.Text == "0" && ddlchannelb.Text == "0")
            {
                this.Session["mensajealert"] = "Nombre de perfil, Rol y/o Nivel de usuario";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnPerfil_ModalPopupExtender.Show();
                return;
            }

            BuscarActivarbotnesPerfil();
            //btnCrearPer.Visible = true;
            sPerfilName = TxtBNomPerfil.Text.ToUpper();
            sRolid = cmbBRolAs.SelectedValue;
            idChannel = Convert.ToInt32(ddlchannelb.SelectedValue);
            sCodNivel = cmbSniveluser.SelectedValue;
            TxtBNomPerfil.Text = "";
            cmbBRolAs.Text = "0";
            cmbSniveluser.Text = "0";
            ddlchannelb.Text = "0";

            DataTable oeperfiles = oUsuario.BuscarPerfiles(sPerfilName, sRolid, idChannel, sCodNivel);

            if (oeperfiles != null)
            {
                if (oeperfiles.Rows.Count > 0)
                {
                    for (int i = 0; i <= oeperfiles.Rows.Count - 1; i++)
                    {                   
                        TxtCodPerfil.Text = oeperfiles.Rows[0]["Perfil_id"].ToString().Trim();
                        comboRolenPerfil();
                        DdlSelRol.Text = oeperfiles.Rows[0]["Rol_id"].ToString().Trim();                        
                        comboNivelenPerfil();
                        cmbniveluser.Text = oeperfiles.Rows[0]["id_Level"].ToString().Trim();
                        ComboModuloenPerfil();
                        DdlSelMod.Text = (oeperfiles.Rows[0]["Modulo_id"].ToString().Trim()); 
                        TxtNomPerfil.Text = oeperfiles.Rows[0]["Perfil_Name"].ToString().Trim();                        
                        TxtDescPerfil.Text = (oeperfiles.Rows[0]["Perfil_Description"].ToString().Trim());
                        comboCanalenPerfil(); 
                        ddlchannel.Text = oeperfiles.Rows[0]["cod_Channel"].ToString().Trim();                        
                        estado = Convert.ToBoolean(oeperfiles.Rows[0]["Perfil_Status"].ToString().Trim());

                        if (estado == true)
                        {
                            RBtnListStatusPerfil.Items[0].Selected = true;
                            RBtnListStatusPerfil.Items[1].Selected = false;
                        }
                        else
                        {
                            RBtnListStatusPerfil.Items[0].Selected = false;
                            RBtnListStatusPerfil.Items[1].Selected = true;
                        }
                        this.Session["tperfiles"] = oeperfiles;
                        this.Session["i"] = 0;
                    }

                    if (oeperfiles.Rows.Count == 1)
                    {
                        btnPreg1.Visible = false;
                        btnAreg1.Visible = false;
                        btnSreg1.Visible = false;
                        btnUreg1.Visible = false;
                    }
                    else
                    {
                        btnPreg1.Visible = true;
                        btnAreg1.Visible = true;
                        btnSreg1.Visible = true;
                        btnUreg1.Visible = true;
                    }
                }
                else
                {
                    SavelimpiarControlesPerfil();
                    saveActivarbotonesPerfil();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    IbtnPerfil_ModalPopupExtender.Show();
                }

            }
        }
        protected void btnEditPerfil_Click(object sender, EventArgs e)
        {
            EditarActivarbotonesPerfil();
            EditarActivarControlesPerfil();
            this.Session["rept"] = TxtNomPerfil.Text;
        }        
        protected void btnActualizarPer_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomPerfil.Text = TxtNomPerfil.Text.TrimStart();
            TxtDescPerfil.Text = TxtDescPerfil.Text.TrimStart();
            if (TxtNomPerfil.Text == "" || TxtDescPerfil.Text == "" || DdlSelRol.Text == "0" || DdlSelMod.Text == "0" || cmbniveluser.Text == "0" || ddlchannel.Text == "0")
            {

                if (TxtNomPerfil.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Perfil";
                }
                if (TxtDescPerfil.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                if (DdlSelRol.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Rol";
                }
                if (ddlchannel.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Canal";
                }
                if (DdlSelMod.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Módulo";
                }
                if (cmbniveluser.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nivel de usuario";
                }


                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {
                if (RBtnListStatusPerfil.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabperfil = new DAplicacion();
                    DataTable dt = oddeshabperfil.PermitirDeshabilitar(ConfigurationManager.AppSettings["ProfilesBudget"], TxtCodPerfil.Text);
                    if (dt != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en otro maestro, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }
                    DataTable dt1 = oddeshabperfil.PermitirDeshabilitar(ConfigurationManager.AppSettings["ProfilesPerson"], TxtCodPerfil.Text);
                    if (dt1 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en otro maestro, por favor Verifique";
                        MensajeAlerta();
                        return;
                    }

                }
                repetido = Convert.ToString(this.Session["rept"]);
                if (repetido != TxtNomPerfil.Text)
                {
                    DAplicacion odconsuperfiles = new DAplicacion();
                    DataTable dtconsulta = odconsuperfiles.ConsultaDuplicados(ConfigurationManager.AppSettings["Perfiles"], 
                        TxtNomPerfil.Text, null, null);
                    if (dtconsulta == null)
                    {
                        EProfiles oePerfiles = oUsuario.Actualizar_Perfiles(
                            TxtCodPerfil.Text, 
                            "1",
                            DdlSelRol.SelectedValue, 
                            cmbniveluser.SelectedValue, 
                            TxtNomPerfil.Text, 
                            DdlSelMod.SelectedValue.ToString(), 
                            TxtDescPerfil.Text, 
                            Convert.ToInt32(ddlchannel.SelectedValue), 
                            estado, 
                            Convert.ToString(this.Session["sUser"])
                            );
                        obtenerid.Get_Actualiza_ModuloPerson(TxtCodPerfil.Text, DdlSelMod.SelectedValue.ToString());
                        sNomPerfil = TxtNomPerfil.Text;
                        this.Session["sNomPerfil"] = sNomPerfil;
                        //llenarcombos();
                        SavelimpiarControlesPerfil();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Perfil : " + this.Session["sNomPerfil"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        saveActivarbotonesPerfil();
                        desactivarControlesPerfil();
                    }
                    else
                    {
                        sNomPerfil = TxtNomPerfil.Text;
                        this.Session["sNomPerfil"] = sNomPerfil;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Perfil : " + this.Session["sNomPerfil"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    EProfiles oePerfiles = oUsuario.Actualizar_Perfiles(
                        TxtCodPerfil.Text,
                        "1",
                        DdlSelRol.SelectedValue, 
                        cmbniveluser.SelectedValue, 
                        TxtNomPerfil.Text, 
                        DdlSelMod.SelectedValue.ToString(), 
                        TxtDescPerfil.Text, 
                        Convert.ToInt32(ddlchannel.SelectedValue), 
                        estado, 
                        Convert.ToString(this.Session["sUser"])
                        );
                    obtenerid.Get_Actualiza_ModuloPerson(TxtCodPerfil.Text, DdlSelMod.SelectedValue.ToString());
                    sNomPerfil = TxtNomPerfil.Text;
                    this.Session["sNomPerfil"] = sNomPerfil;
                    //llenarcombos();
                    SavelimpiarControlesPerfil();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Perfil : " + this.Session["sNomPerfil"] + " Se Actualizo con Exito";
                    MensajeAlerta();
                    saveActivarbotonesPerfil();
                    desactivarControlesPerfil();
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
        protected void btnCancelPer_Click(object sender, EventArgs e)
        {
            SavelimpiarControlesPerfil();
            saveActivarbotonesPerfil();
            desactivarControlesPerfil();
        }
        private void MostrarDatosPerfil()
        {
            recorrido = (DataTable)this.Session["tperfiles"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    TxtCodPerfil.Text = recorrido.Rows[recsearch]["Perfil_id"].ToString().Trim();
                    DdlSelRol.Text = recorrido.Rows[recsearch]["Rol_id"].ToString().Trim();
                    cmbniveluser.Text = recorrido.Rows[recsearch]["id_Level"].ToString().Trim();
                    TxtNomPerfil.Text = recorrido.Rows[recsearch]["Perfil_Name"].ToString().Trim();
                    DdlSelMod.Text = (recorrido.Rows[recsearch]["Modulo_id"].ToString().Trim());
                    TxtDescPerfil.Text = (recorrido.Rows[recsearch]["Perfil_Description"].ToString().Trim());
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Perfil_Status"].ToString().Trim());

                    if (estado == true)
                    {
                        RBtnListStatusPerfil.Items[0].Selected = true;
                        RBtnListStatusPerfil.Items[1].Selected = false;

                    }
                    else
                    {
                        RBtnListStatusPerfil.Items[0].Selected = false;
                        RBtnListStatusPerfil.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void btnPreg1_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tperfiles"];
            MostrarDatosPerfil();
        }
        protected void btnAreg1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosPerfil();
            }
        }
        protected void btnSreg1_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tperfiles"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosPerfil();
            }
        }
        protected void btnUreg1_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tperfiles"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosPerfil();
        }
        #endregion

        #region Usuario
        protected void btnCrearUsu_Click(object sender, EventArgs e)
        {
           
            combotipoDocenUsuario();
            combomoduloenusuario();
            comboclienteenusuario();
            combopaisenUsuario();
            activarControlesUsuario();
            crearActivarbotonesUsuario();
       
        }
        protected void BtnGuardarUsu_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNumDoc.Text = TxtNumDoc.Text.TrimStart();
            TxtPNom.Text = TxtPNom.Text.TrimStart();
            TxtPApe.Text = TxtPApe.Text.TrimStart();
            TxtUsu.Text = TxtUsu.Text.TrimStart();
            TxtPsw.Text = TxtPsw.Text.TrimStart();
            TxtMail.Text = TxtMail.Text.TrimStart();
            
            
                if (cmbdoc.Text == "0" || TxtNumDoc.Text == "" || TxtPNom.Text == "" || TxtPApe.Text == "" || cmbmodul.Text == "0" || cmbperfil.Text == "0" || cmbperfil.Text == "" || cmbcompany.Text == "0"
                    || cmbcontry.Text == "0" || TxtMail.Text == "")
                {
                    if (cmbdoc.Text == "0")
                    {
                        LblFaltantes.Text = LblFaltantes.Text + ". " + "Tipo de documento";
                    }
                    if (TxtNumDoc.Text == "")
                    {
                        LblFaltantes.Text = LblFaltantes.Text + ". " + "Número de documento";
                    }
                    if (TxtPNom.Text == "")
                    {
                        LblFaltantes.Text = LblFaltantes.Text + ". " + "Primer Nombre";
                    }
                    if (TxtPApe.Text == "")
                    {
                        LblFaltantes.Text = LblFaltantes.Text + ". " + "Primer Apellido";
                    }
                    if (cmbmodul.Text == "0")
                    {
                        LblFaltantes.Text = LblFaltantes.Text + ". " + "Módulo";
                    }
                    if (cmbperfil.Text == "0" || cmbperfil.Text == "")
                    {
                        LblFaltantes.Text = LblFaltantes.Text + ". " + "Perfil";
                    }
                    if (cmbcompany.Text == "0")
                    {
                        LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                    }
                    if (cmbcontry.Text == "0")
                    {
                        LblFaltantes.Text = LblFaltantes.Text + ". " + "País";
                    }
                    if (TxtMail.Text == "")
                    {
                        LblFaltantes.Text = LblFaltantes.Text + ". " + "Email";
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
                        if (Convert.ToInt64(TxtNumDoc.Text) <= 0)
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Sr. Usuario, no ingresar parametros con valor 0";
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



                try
                {
                    if (cmbmodul.SelectedValue != "OPE")
                    //if (cmbperfil.SelectedValue != "001" && cmbperfil.SelectedValue != "002" && cmbperfil.SelectedValue != "003" && cmbperfil.SelectedValue != "006" && cmbperfil.SelectedValue != "007" && cmbperfil.SelectedValue != "008" && cmbperfil.SelectedValue != "0017" && cmbperfil.SelectedValue != "0018" && cmbperfil.SelectedValue != "0019" && cmbperfil.SelectedValue != "0020" && cmbperfil.SelectedValue !="0022" && cmbperfil.SelectedValue !="0026")
                    {
                        DAplicacion odconsuusu = new DAplicacion();
                        DataTable dtconsulta = odconsuusu.ConsultaDuplicados(ConfigurationManager.AppSettings["Usuario"], TxtNumDoc.Text, TxtUsu.Text, TxtPsw.Text);
                        if (dtconsulta == null)
                        {
                        
                            string Key;
                            Key = ConfigurationManager.AppSettings["TamperProofKey"];
                           
                            EUsuario oerUsuario = oUsuario.Registrar(cmbdoc.SelectedValue.ToString(), TxtNumDoc.Text, TxtPNom.Text.ToUpper(), TxtSNom.Text.ToUpper(), TxtPApe.Text.ToUpper(),
                            TxtSApe.Text.ToUpper(), TxtMail.Text.ToLower(), TxtTel.Text, TxtDir.Text.ToUpper(), cmbcontry.SelectedValue.ToString().Trim(), TxtUsu.Text,Encriptacion.Codificar(TxtPsw.Text,Key), cmbperfil.SelectedValue,
                            cmbmodul.SelectedValue.ToString().Trim(), TxtPalabra.Text, Convert.ToInt32(cmbcompany.SelectedValue.ToString().Trim()), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            Convert.ToString(oerUsuario.Personid);
                            DataTable dtosusuariocredenciales = oUsuario.ConsultarCredenciales(TxtNumDoc.Text);
                            string sUsuario = "";
                            string sMail = "";
                            string spassw = "";
                            sUsuario = dtosusuariocredenciales.Rows[0]["name_user"].ToString().Trim();
                            sMail = TxtMail.Text;
                            spassw = dtosusuariocredenciales.Rows[0]["User_Password"].ToString().Trim();
                            this.Session["sUsuario"] = sUsuario;
                            this.Session["sMail"] = sMail;
                            this.Session["sPassw"] = spassw;
                            TxtMail.Text = sUsuario;
                            TxtPsw.Text = spassw;
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "El usuario " + this.Session["sUsuario"] + " fue creado con Exito";
                            MensajeAlerta();
                            SavelimpiarControlesUsuario();
                            //llenarcombos();
                            combousuarios();
                            comboperfilausuario();
                            saveActivarbotonesUsuario();
                            desactivarControlesUsuario();

                            EntrySeccion oSeccion = new EntrySeccion();
                            EEntrySeccion oeseccion = oSeccion.Register_PrimerSeccion(sUsuario, Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now), Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));
                            try
                            {
                                Enviomail oEnviomail = new Enviomail();
                                EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Solicitud_Clave");
                                Mails oMail = new Mails();
                                oMail.Server = oeEmail.MailServer;
                                oMail.Puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"]); //Se agrega Puerto Ing. CarlosH 30/11/2011
                                oMail.MCifrado = true; //Se agrega Cifrado  Ing. CarlosH 30/11/2011
                                oMail.DatosUsuario = new System.Net.NetworkCredential(); //Se Agrega Credenciales Ing. CarlosH 30/11/2011 
                                oMail.From = oeEmail.MailTo;
                                oMail.To = this.Session["sMail"].ToString().Trim();
                                oMail.Subject = "El usuario: " + sUsuario + ". Se ha registrado con éxito en Xplora";
                                string[] textArray1 = new string[] { "¡Bienvenido a Xplora!" , "<br>" ,
                            "Lo invitamos a acceder al sistema de consultas en línea para que esté al tanto de toda la información de la evaluación de ventas y otros datos para una mejor desición sobre sus productos", "<br><br>" ,
                            "Su información para conectarse está especificada a continuación, por favor guarde estos datos por si lo necesita:" , "<br><br>" ,
                            "Usuario:" + sUsuario , "<br>", "Contraseña:" + this.Session["sPassw"], "<br><br>", "Puede acceder a traves del siguiente link: " ,    
                        "<a href=" + "http://sige.lucky.com.pe" + ">..::Xplora</a>" ,"<br><br>" , 
                            "Atentamente", "<br>", "Administrador Xplora" };

                                oMail.Body = string.Concat(textArray1);
                                oMail.CC = "aortiz@lucky.com.pe";

                                oMail.BodyFormat = "HTML";
                                oMail.send();
                                oMail = null;
                                oeEmail = null;
                                oEnviomail = null;
                            }
                            catch
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El usuario " + this.Session["sUsuario"] + " se ha creado con éxito." + "<br><br>" + "No fue posible enviar Notificación por correo electrónico debido a problemas técnicos." + "<br>" + "Por favor notifique manualmente. Gracias";
                                MensajeAlerta();

                            }
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El usuario " + this.Session["sUsuario"] + " Ya Existe";
                            MensajeAlerta(); 
                        }
                    }

                    else
                    {
                        DAplicacion odconsuusu = new DAplicacion();
                        DataTable dtconsulta = odconsuusu.ConsultaDuplicados(ConfigurationManager.AppSettings["usuarioMovil"], TxtNumDoc.Text, null, null);
                        if (dtconsulta == null)
                        {
                            EUsuario oerUsuario = oUsuario.RegistrarMovil(cmbdoc.SelectedValue.ToString(), TxtNumDoc.Text, TxtPNom.Text.ToUpper(), TxtSNom.Text.ToUpper(), TxtPApe.Text.ToUpper(),
                            TxtSApe.Text.ToUpper(), TxtMail.Text.ToLower(), TxtTel.Text, TxtDir.Text.ToUpper(), cmbcontry.SelectedValue.ToString().Trim(), TxtUsu.Text, TxtPsw.Text, cmbperfil.SelectedValue,
                            cmbmodul.SelectedValue.ToString().Trim(), TxtPalabra.Text, Convert.ToInt32(cmbcompany.SelectedValue.ToString().Trim()), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            Convert.ToString(oerUsuario.Personid);

                            EUsuario oerUsuarioTMP = oUsuario.RegistrarMovilTMP(
                                oerUsuario.Personid,
                                oerUsuario.idtypeDocument,
                                oerUsuario.Personnd,oerUsuario.PersonFirtsname,oerUsuario.PersonLastName,
                                oerUsuario.PersonSurname,oerUsuario.PersonSeconName,oerUsuario.PersonEmail,
                                oerUsuario.PersonPhone,oerUsuario.PersonAddres,oerUsuario.codCountry,
                                oerUsuario.nameuser,oerUsuario.UserPassword,oerUsuario.Perfilid,
                                oerUsuario.Moduloid,oerUsuario.UserRecall,Convert.ToInt32(oerUsuario.companyid),oerUsuario.PersonStatus,
                                oerUsuario.PersonCreateBy,Convert.ToDateTime(oerUsuario.PersonDateBy),
                                oerUsuario.PersonModiBy,Convert.ToDateTime(oerUsuario.PersonDateModiBy)
                            );
                            //DataTable dtosusuariocredenciales = oUsuario.ConsultarCredenciales(TxtNumDoc.Text);
                            string sUsuario = "";
                            string sMail = "";
                            string spassw = "";
                            sUsuario = TxtUsu.Text;
                            sMail = TxtMail.Text;
                            spassw = TxtPsw.Text;
                            this.Session["sUsuario"] = sUsuario;
                            this.Session["sMail"] = sMail;
                            this.Session["sPassw"] = spassw;
                            //TxtMail.Text = sUsuario;
                            //TxtPsw.Text = spassw;
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "El usuario " + this.Session["sUsuario"] + ", fue creado con éxito";
                            MensajeAlerta();
                            SavelimpiarControlesUsuario();
                            combousuarios();
                            comboperfilausuario();
                            saveActivarbotonesUsuario();
                            desactivarControlesUsuario();

                            EntrySeccion oSeccion = new EntrySeccion();
                            EEntrySeccion oeseccion = oSeccion.Register_PrimerSeccion(sUsuario, Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now), Convert.ToString(this.Session["sUser"]), Convert.ToString(DateTime.Now));
                            try
                            {
                                Enviomail oEnviomail = new Enviomail();
                                EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Solicitud_Clave");
                                Mails oMail = new Mails();
                                oMail.Server = oeEmail.MailServer;
                                oMail.Puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"]); //Se agrega Puerto Ing. CarlosH 30/11/2011
                                oMail.MCifrado = true; //Se agrega Cifrado  Ing. CarlosH 30/11/2011
                                oMail.DatosUsuario = new System.Net.NetworkCredential(); //Se Agrega Credenciales Ing. CarlosH 30/11/2011 
                                oMail.From = oeEmail.MailTo;
                                oMail.To = this.Session["sMail"].ToString().Trim();
                                oMail.Subject = "El usuario: " + sUsuario + ". Se ha registrado con éxito en Xplora";
                                string[] textArray1 = new string[] { "¡Bienvenido a Xplora!" , "<br>" ,
                            "Lo invitamos a acceder al sistema de consultas en línea para que esté al tanto de toda la información de la evaluación de ventas y otros datos para una mejor desición sobre sus productos", "<br><br>" ,
                            "Su información para conectarse está especificada a continuación, por favor guarde estos datos por si lo necesita:" , "<br><br>" ,
                            "Usuario:" + sUsuario , "<br>", "Contraseña:" + this.Session["sPassw"], "<br><br>", "Puede acceder a traves del siguiente link: " ,    
                        "<a href=" + "http://sige.lucky.com.pe" + ">..::Xplora</a>" ,"<br><br>" , 
                            "Atentamente", "<br>", "Administrador Xplora" };

                                oMail.Body = string.Concat(textArray1);
                                //oMail.CC = "aortiz@lucky.com.pe";
                                oMail.CC = "davidgadea@lucky.com.pe";

                                oMail.BodyFormat = "HTML";
                                oMail.send();
                                oMail = null;
                                oeEmail = null;
                                oEnviomail = null;
                            }
                            catch
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El usuario " + this.Session["sUsuario"] + " se ha creado con éxito." + "<br><br>" + "No fue posible enviar Notificación por correo electrónico debido a problemas técnicos." + "<br>" + "Por favor notifique manualmente. Gracias";
                                MensajeAlerta();

                            }
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El usuario " + this.Session["sUsuario"] + " Ya Existe";
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
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            btndeco.Enabled = true;
            lblclave.Text = "";

            desactivarControlesUsuario();
            LblFaltantes.Text = "";
            txtdoc.Text = txtdoc.Text.TrimStart();
            txtuser.Text = txtuser.Text.TrimStart();
            if (txtdoc.Text == "" && txtuser.Text == "0")
            {
                this.Session["mensajealert"] = "Número de documento y/o nombre de usuario";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                Ibtnlogin_ModalPopupExtender.Show();
                return;

            }
            BuscarActivarbotnesUsuario();
            //btnCrearUsu.Visible = true;
            sdoc = txtdoc.Text;
            sUsuer = txtuser.Text;
            btnEditAct.Visible = true;

            DataTable dt = search.Search_User(txtdoc.Text, Convert.ToInt32(txtuser.Text));

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    txtdoc.Text = "";
                    txtuser.Text = "0";
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                       
                       
                        txtCodUsu.Text = dt.Rows[0]["Person_id"].ToString().Trim();
                        combotipoDocenUsuario();
                        cmbdoc.Text = dt.Rows[0]["id_typeDocument"].ToString().Trim();
                        TxtNumDoc.Text = dt.Rows[0]["Person_nd"].ToString().Trim();
                        TxtPNom.Text = dt.Rows[0]["Person_Firtsname"].ToString().Trim();
                        TxtSNom.Text = dt.Rows[0]["Person_LastName"].ToString().Trim();
                        TxtPApe.Text = dt.Rows[0]["Person_Surname"].ToString().Trim();
                        TxtSApe.Text = dt.Rows[0]["Person_SeconName"].ToString().Trim();
                        TxtPalabra.Text = dt.Rows[0]["User_Recall"].ToString().Trim();
                        combomoduloenusuario();
                        cmbmodul.Text = dt.Rows[0]["Modulo_id"].ToString().Trim();
                        comboperfilausuario();
                        cmbperfil.Text = dt.Rows[0]["Perfil_id"].ToString().Trim();
                        comboclienteenusuario();
                        cmbcompany.Text = dt.Rows[0]["Company_id"].ToString().Trim();
                        TxtUsu.Text = dt.Rows[0]["name_user"].ToString().Trim();
                        TxtPsw.Text = dt.Rows[0]["User_Password"].ToString().Trim();
                        TxtMail.Text = dt.Rows[0]["Person_Email"].ToString().Trim();
                        TxtTel.Text = dt.Rows[0]["Person_Phone"].ToString().Trim();
                        TxtDir.Text = dt.Rows[0]["Person_Addres"].ToString().Trim();
                        combopaisenUsuario();
                        cmbcontry.Text = dt.Rows[0]["cod_Country"].ToString().Trim();
                        estado = Convert.ToBoolean(dt.Rows[0]["Person_Status"].ToString().Trim());

                        if (estado == true)
                        {
                            RBtnListStatusUsu.Items[0].Selected = true;
                            RBtnListStatusUsu.Items[1].Selected = false;
                        }
                        else
                        {
                            RBtnListStatusUsu.Items[0].Selected = false;
                            RBtnListStatusUsu.Items[1].Selected = true;
                        }

                        this.Session["tusuarios"] = dt;
                        this.Session["i"] = 0;
                    }


                    if (dt.Rows.Count == 1)
                    {
                        btnPreg2.Visible = false;
                        btnAreg2.Visible = false;
                        btnSreg2.Visible = false;
                        btnUreg2.Visible = false;
                    }
                    else
                    {
                        btnPreg2.Visible = true;
                        btnAreg2.Visible = true;
                        btnSreg2.Visible = true;
                        btnUreg2.Visible = true;
                    }

                }
                else
                {
                    SavelimpiarControlesUsuario();
                    //llenarcombos();
                    comboperfilausuario();
                    saveActivarbotonesUsuario();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();

                    dt = null;
                }
            }
            else
            {
                SavelimpiarControlesUsuario();
                //llenarcombos();
                comboperfilausuario();
                saveActivarbotonesUsuario();
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                MensajeAlerta();
                Ibtnlogin_ModalPopupExtender.Show();
            }
        }
        protected void btnEditAct_Click(object sender, EventArgs e)
        {
            btnActua.Visible = true;
            btnEditAct.Visible = false;
            activarControlesUsuario();
            this.Session["rept"] = TxtNumDoc.Text;
            this.Session["rept1"] = TxtUsu.Text;
            this.Session["rept2"] = TxtPsw.Text;
            EditarActivarControlesUsuario();

        }
        protected void btnActua_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNumDoc.Text = TxtNumDoc.Text.TrimStart();
            TxtPNom.Text = TxtPNom.Text.TrimStart();
            TxtPApe.Text = TxtPApe.Text.TrimStart();
            TxtUsu.Text = TxtUsu.Text.TrimStart();
            TxtPsw.Text = TxtPsw.Text.TrimStart();
            TxtMail.Text = TxtMail.Text.TrimStart();
            if (cmbdoc.Text == "0" || TxtNumDoc.Text == "" || TxtPNom.Text == "" || TxtPApe.Text == "" || cmbmodul.Text == "0" || cmbperfil.Text == "0" || cmbcompany.Text == "0"
              || TxtUsu.Text == "" || TxtPsw.Text == "" || cmbcontry.Text == "0" || TxtMail.Text == "")
            {
                if (cmbdoc.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Tipo de documento";
                }
                if (TxtNumDoc.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Número de documento";
                }
                if (TxtPNom.Text == "")
                {
                    LblFaltantes.Text = ". " + "Primer Nombre";
                }
                if (TxtPApe.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Primer Apellido";
                }
                if (cmbmodul.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Módulo";
                }
                if (cmbperfil.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Perfil";
                }
                if (cmbcompany.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }
                if (TxtUsu.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Usuario";
                }
                if (TxtPsw.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Clave";
                }
                if (cmbcontry.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "País";
                }
                if (TxtMail.Text == "0")
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
                if (Convert.ToInt64(TxtNumDoc.Text) <= 0)
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
                if (RBtnListStatusUsu.Items[0].Selected == true)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    DAplicacion oddeshabusu = new DAplicacion();
                    DataTable dt = oddeshabusu.PermitirDeshabilitar(ConfigurationManager.AppSettings["PersonBudget"], txtCodUsu.Text);
                    DataTable dt1 = oddeshabusu.PermitirDeshabilitar(ConfigurationManager.AppSettings["PersonPerson_Asign_Ejec_Direct"], txtCodUsu.Text);
                    DataTable dt2 = oddeshabusu.PermitirDeshabilitar(ConfigurationManager.AppSettings["PersonDirPerson_Asign_Ejec_Direct"], txtCodUsu.Text);

                    if (dt != null || dt1 != null || dt2 != null)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No puede deshabilitar este registro. Ya que existe relación en otros maestro. Por favor verifique";
                        MensajeAlerta();
                        return;
                    }

                }
                repetido = Convert.ToString(this.Session["rept"]);
                repetido1 = Convert.ToString(this.Session["rept1"]);
                repetido2 = Convert.ToString(this.Session["rept2"]);
                if (repetido != TxtNumDoc.Text || repetido1 != TxtUsu.Text)
                {
                    DAplicacion odconsuroles = new DAplicacion();
                    DataTable dtconsulta = odconsuroles.ConsultaDuplicados(ConfigurationManager.AppSettings["Usuario"], TxtNumDoc.Text, null, null);
                    DataTable dtconsulta1 = odconsuroles.ConsultaDuplicados(ConfigurationManager.AppSettings["Usuario_name"],TxtUsu.Text, null , null);
                    if (dtconsulta == null && dtconsulta1 == null)
                    {

                        EUsuario oeaUsuario = oUsuario.Actualizar(Convert.ToInt32(txtCodUsu.Text), cmbdoc.SelectedValue.ToString().Trim(), TxtNumDoc.Text, TxtPNom.Text.ToUpper(), TxtSNom.Text.ToUpper(), TxtPApe.Text.ToUpper(),
                                TxtSApe.Text.ToUpper(), TxtMail.Text.ToLower(), TxtTel.Text, TxtDir.Text.ToUpper(), cmbcontry.SelectedValue.ToString().Trim(), TxtUsu.Text, TxtPsw.Text, cmbperfil.SelectedValue,
                                cmbmodul.SelectedValue.ToString().Trim(), TxtPalabra.Text, Convert.ToInt32(cmbcompany.SelectedValue.ToString().Trim()), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        EUsuario oeaUsuarioTMP = oUsuario.ActualizarTMP(Convert.ToInt32(txtCodUsu.Text), cmbdoc.SelectedValue.ToString().Trim(), TxtNumDoc.Text, TxtPNom.Text.ToUpper(), TxtSNom.Text.ToUpper(), TxtPApe.Text.ToUpper(),
                               TxtSApe.Text.ToUpper(), TxtMail.Text.ToLower(), TxtTel.Text, TxtDir.Text.ToUpper(), cmbcontry.SelectedValue.ToString().Trim(), TxtUsu.Text, TxtPsw.Text, cmbperfil.SelectedValue,
                               cmbmodul.SelectedValue.ToString().Trim(), TxtPalabra.Text, Convert.ToInt32(cmbcompany.SelectedValue.ToString().Trim()), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        sNomUsu = TxtUsu.Text;
                        this.Session["sNomUsu"] = sNomUsu;
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Usuario : " + this.Session["sNomUsu"] + " Se Actualizo con Exito";
                        MensajeAlerta();
                        SavelimpiarControlesUsuario();
                        //llenarcombos();
                        combousuarios();
                        comboperfilausuario();
                        saveActivarbotonesUsuario();
                        desactivarControlesUsuario();
                    }
                    else
                    {
                        this.Session["mensajealert"] = "El Número de documento, nombre de usuario y/o Clave ";
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Usuario : " + this.Session["sNomUsu"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    EUsuario oeaUsuario = oUsuario.Actualizar(Convert.ToInt32(txtCodUsu.Text), cmbdoc.SelectedValue.ToString().Trim(), TxtNumDoc.Text, TxtPNom.Text.ToUpper(), TxtSNom.Text.ToUpper(), TxtPApe.Text.ToUpper(),
                               TxtSApe.Text.ToUpper(), TxtMail.Text.ToLower(), TxtTel.Text, TxtDir.Text.ToUpper(), cmbcontry.SelectedValue.ToString().Trim(), TxtUsu.Text, TxtPsw.Text, cmbperfil.SelectedValue,
                               cmbmodul.SelectedValue.ToString().Trim(), TxtPalabra.Text, Convert.ToInt32(cmbcompany.SelectedValue.ToString().Trim()), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    EUsuario oeaUsuarioTMP = oUsuario.ActualizarTMP(Convert.ToInt32(txtCodUsu.Text), cmbdoc.SelectedValue.ToString().Trim(), TxtNumDoc.Text, TxtPNom.Text.ToUpper(), TxtSNom.Text.ToUpper(), TxtPApe.Text.ToUpper(),
                   TxtSApe.Text.ToUpper(), TxtMail.Text.ToLower(), TxtTel.Text, TxtDir.Text.ToUpper(), cmbcontry.SelectedValue.ToString().Trim(), TxtUsu.Text, TxtPsw.Text, cmbperfil.SelectedValue,
                   cmbmodul.SelectedValue.ToString().Trim(), TxtPalabra.Text, Convert.ToInt32(cmbcompany.SelectedValue.ToString().Trim()), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    sNomUsu = TxtUsu.Text;
                    this.Session["sNomUsu"] = sNomUsu;
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Usuario : " + this.Session["sNomUsu"] + " Se Actualizo con Exito";
                    MensajeAlerta();
                    SavelimpiarControlesUsuario();
                    //llenarcombos();
                    combousuarios();
                    comboperfilausuario();
                    saveActivarbotonesUsuario();
                    desactivarControlesUsuario();


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
        protected void btnCancelarUsu_Click(object sender, EventArgs e)
        {
            SavelimpiarControlesUsuario();
            saveActivarbotonesUsuario();
            desactivarControlesUsuario();
        }       
        protected void cmbmodul_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboperfilausuario();
        }
        protected void cmbperfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnActua.Visible == false)
            {
                if (cmbmodul.SelectedValue == "OPE")
                //if (cmbperfil.SelectedValue == "001" || cmbperfil.SelectedValue == "002" || cmbperfil.SelectedValue == "003" || cmbperfil.SelectedValue == "006" || cmbperfil.SelectedValue == "007" || cmbperfil.SelectedValue == "008" || cmbperfil.SelectedValue == "0017" || cmbperfil.SelectedValue == "0018" || cmbperfil.SelectedValue == "0019" || cmbperfil.SelectedValue == "0020" || cmbperfil.SelectedValue == "0022")
                {

                    string usuario = TxtPNom.Text;
                    TxtUsu.Text = usuario.Substring(0, 1) + TxtPApe.Text;
                    TxtUsu.Text = TxtUsu.Text.Replace(" ", "");
                    TxtPsw.Text = "1234";

                }
                else
                {
                    string usuario = TxtPNom.Text;
                    TxtUsu.Text = usuario.Substring(0, 1) + TxtPApe.Text + "**";
                    TxtUsu.Text = TxtUsu.Text.Replace(" ", "");
                    TxtPsw.Text = "*******";
                }
            }
            else
            {
                if (cmbperfil.SelectedValue == "001" || cmbperfil.SelectedValue == "002" || cmbperfil.SelectedValue == "003")
                { TxtPsw.Text = "1234"; }
            }
        }
        private void MostrarDatosUsu()
        {
            recorrido = (DataTable)this.Session["tusuarios"];
            if (recorrido != null)
            {
                if (recorrido.Rows.Count > 0)
                {
                    recsearch = Convert.ToInt32(this.Session["i"]);
                    txtCodUsu.Text = recorrido.Rows[recsearch]["Person_id"].ToString().Trim();
                    cmbdoc.Text = recorrido.Rows[recsearch]["id_typeDocument"].ToString().Trim();
                    TxtNumDoc.Text = recorrido.Rows[recsearch]["Person_nd"].ToString().Trim();
                    TxtPNom.Text = recorrido.Rows[recsearch]["Person_Firtsname"].ToString().Trim();
                    TxtSNom.Text = recorrido.Rows[recsearch]["Person_LastName"].ToString().Trim();
                    TxtPApe.Text = recorrido.Rows[recsearch]["Person_Surname"].ToString().Trim();
                    TxtSApe.Text = recorrido.Rows[recsearch]["Person_SeconName"].ToString().Trim();
                    TxtPalabra.Text = recorrido.Rows[recsearch]["User_Recall"].ToString().Trim();
                    cmbmodul.Text = recorrido.Rows[recsearch]["Modulo_id"].ToString().Trim();
                    comboperfilausuario(); 
                    cmbperfil.Text = recorrido.Rows[recsearch]["Perfil_id"].ToString().Trim();
                    cmbcompany.Text = recorrido.Rows[recsearch]["Company_id"].ToString().Trim();
                    TxtUsu.Text = recorrido.Rows[recsearch]["name_user"].ToString().Trim();
                    TxtPsw.Text = recorrido.Rows[recsearch]["User_Password"].ToString().Trim();
                    TxtMail.Text = recorrido.Rows[recsearch]["Person_Email"].ToString().Trim();
                    TxtTel.Text = recorrido.Rows[recsearch]["Person_Phone"].ToString().Trim();
                    TxtDir.Text = recorrido.Rows[recsearch]["Person_Addres"].ToString().Trim();
                    cmbcontry.Text = recorrido.Rows[recsearch]["cod_Country"].ToString().Trim();
                    estado = Convert.ToBoolean(recorrido.Rows[recsearch]["Person_Status"].ToString().Trim());

                    if (estado == true)
                    {
                        RBtnListStatusUsu.Items[0].Selected = true;
                        RBtnListStatusUsu.Items[1].Selected = false;
                    }
                    else
                    {
                        RBtnListStatusUsu.Items[0].Selected = false;
                        RBtnListStatusUsu.Items[1].Selected = true;
                    }
                }
            }
        }
        protected void btnPreg2_Click(object sender, EventArgs e)
        {
            recsearch = 0;
            this.Session["i"] = recsearch;
            recorrido = (DataTable)this.Session["tusuarios"];
            MostrarDatosUsu();
        }
        protected void btnAreg2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["i"]) > 0)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) - 1;
                this.Session["i"] = recsearch;
                MostrarDatosUsu();
            }
        }
        protected void btnSreg2_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tusuarios"];
            if (Convert.ToInt32(this.Session["i"]) < recorrido.Rows.Count - 1)
            {
                recsearch = Convert.ToInt32(this.Session["i"]) + 1;
                this.Session["i"] = recsearch;
                MostrarDatosUsu();
            }
        }

        protected void btnUreg2_Click(object sender, EventArgs e)
        {
            recorrido = (DataTable)this.Session["tusuarios"];
            recsearch = (recorrido.Rows.Count - 1);
            this.Session["i"] = recsearch;
            MostrarDatosUsu();
        }
        #endregion      

        //#region Asisgnación
        //protected void BtnCrearAsignEje_Click(object sender, EventArgs e)
        //{
        //    crearActivarbotonesAsignación();
        //    activarControlesAsignación();
        //}
        //protected void CmbSelPaisDirCu_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (CmbSelPaisDirCu.SelectedValue != "0")
        //    {
        //        comboAsignaDirCuenta();
        //        ListaEjeCuenta();
        //    }
        //    else
        //    {
        //        CmbSelDirCu.Items.Clear();
        //        LstBoxPersonalSinAsing.Items.Clear();
        //        LstBoxPersonalAsing.Items.Clear();
        //    }
        //}
        //protected void BtnMasAsing_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i <= LstBoxPersonalSinAsing.Items.Count - 1; i++)
        //    {
        //        if (LstBoxPersonalSinAsing.Items[i].Selected == true)
        //        {
        //            LstBoxPersonalAsing.Items.Insert(0, new ListItem(LstBoxPersonalSinAsing.Items[i].Text, LstBoxPersonalSinAsing.Items[i].Value));
        //            LstBoxPersonalSinAsing.Items.Remove(LstBoxPersonalSinAsing.Items[i]);
        //            i = i - 1;
        //        }
        //    }
        //}
        //protected void BtnMenosAsing_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i <= LstBoxPersonalAsing.Items.Count - 1; i++)
        //    {
        //        if (LstBoxPersonalAsing.Items[i].Selected == true)
        //        {
        //            LstBoxPersonalSinAsing.Items.Insert(0, new ListItem(LstBoxPersonalAsing.Items[i].Text, LstBoxPersonalAsing.Items[i].Value));
        //            LstBoxPersonalAsing.Items.Remove(LstBoxPersonalAsing.Items[i]);
        //            i = i - 1;
        //        }
        //    }
        //}
        //protected void BtnSaveAsignEje_Click(object sender, EventArgs e)
        //{
        //    LblFaltantes.Text = "";
        //    if (CmbSelPaisDirCu.SelectedValue == "0" || CmbSelPaisDirCu.SelectedValue == "" || CmbSelDirCu.SelectedValue == "0" || CmbSelDirCu.SelectedValue == ""
        //        || LstBoxPersonalAsing.Items.Count == 0)
        //    {
        //        if (CmbSelPaisDirCu.SelectedValue == "0" || CmbSelPaisDirCu.SelectedValue == "")
        //        {
        //            LblFaltantes.Text = ". " + "País";
        //        }
        //        if (CmbSelDirCu.SelectedValue == "0" || CmbSelDirCu.SelectedValue == "")
        //        {
        //            LblFaltantes.Text = LblFaltantes.Text + ". " + "Director de Cuenta";
        //        }
        //        if (LstBoxPersonalAsing.Items.Count == 0)
        //        {
        //            LblFaltantes.Text = LblFaltantes.Text + ". " + "ejecutivos de cuenta asignados";
        //        }

        //        Alertas.CssClass = "MensajesError";
        //        LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
        //        MensajeAlerta();
        //        return;
        //    }

        //    for (int i = 0; i <= LstBoxPersonalAsing.Items.Count - 1; i++)
        //    {
        //        EPerson_Asign_Ejec_Direct oerPerson_Asign_Ejec_Direct = oPerson_Asign_Ejec_Direct.RegisterAsign_Ejec_Direct(Convert.ToInt32(CmbSelDirCu.SelectedValue), Convert.ToInt32(LstBoxPersonalAsing.Items[i].Value), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
        //    }
        //    string sAsign = "";
        //    sAsign = CmbSelDirCu.SelectedItem.Text;
        //    this.Session["sAsign "] = sAsign;
        //    this.Session["mensajealert"] = "La asignación al Director de Cuenta " + this.Session["sAsign "];
        //    Alertas.CssClass = "MensajesCorrecto";
        //    LblFaltantes.Text = "El Rol " + this.Session["sRol"] + " fue creado con Exito";
        //    MensajeAlerta();
        //    saveActivarbotonesAsignación();
        //    comboPaisAsignarDirCuenta();
        //    CmbSelDirCu.Items.Clear();
        //    LstBoxPersonalSinAsing.Items.Clear();
        //    LstBoxPersonalAsing.Items.Clear();
        //}
        //protected void BtnSearchAsign_Click(object sender, EventArgs e)
        //{
        //    desactivarControlesAsignación();
        //    if (cmbBSelPaisDirect.SelectedValue == "0" || cmbBSelNameDirect.SelectedValue == "0" || cmbBSelNameDirect.SelectedValue == "")
        //    {
        //        this.Session["mensajealert"] = "País y Director de Cuenta";
        //        Alertas.CssClass = "MensajesError";
        //        LblFaltantes.Text = "Ingrese al menos un parámetro de consulta " + this.Session["mensajealert"];
        //        MensajeAlerta();
        //        ModalPopupExtender1.Show();
        //        return;
        //    }
        //    BuscarActivarbotnesAsignación();
        //    sCountryAsign = cmbBSelPaisDirect.SelectedValue;
        //    sNameDirCuenta = cmbBSelNameDirect.SelectedValue;
        //    cmbBSelPaisDirect.SelectedValue = "0";
        //    cmbBSelNameDirect.Items.Clear();

        //    DataSet oeSAsign_Ejec_Direct = oPerson_Asign_Ejec_Direct.SearchAsign_Ejec_Direct(Convert.ToInt32(sNameDirCuenta));
        //    if (oeSAsign_Ejec_Direct != null)
        //    {
        //        if (oeSAsign_Ejec_Direct.Tables[0].Rows.Count > 0)
        //        {
        //            CmbSelPaisDirCu.Items.Clear();
        //            CmbSelDirCu.Items.Clear();
        //            CmbSelPaisDirCu.Items.Insert(0, new ListItem(oeSAsign_Ejec_Direct.Tables[0].Rows[0]["Name_Country"].ToString().Trim(), oeSAsign_Ejec_Direct.Tables[0].Rows[0]["cod_Country"].ToString().Trim()));
        //            CmbSelDirCu.Items.Insert(0, new ListItem(oeSAsign_Ejec_Direct.Tables[0].Rows[0]["Person_Name"].ToString().Trim(), oeSAsign_Ejec_Direct.Tables[0].Rows[0]["Person_id_Director"].ToString().Trim()));
        //        }

        //        if (oeSAsign_Ejec_Direct.Tables[1].Rows.Count > 0)
        //        {
        //            this.Session["EjecAsignados"] = oeSAsign_Ejec_Direct.Tables[1];

        //            LstBoxPersonalAsing.Items.Clear();
        //            for (int i = 0; i <= oeSAsign_Ejec_Direct.Tables[1].Rows.Count - 1; i++)
        //            {
        //                LstBoxPersonalAsing.Items.Insert(0, new ListItem(oeSAsign_Ejec_Direct.Tables[1].Rows[i]["Person_Name"].ToString().Trim(), oeSAsign_Ejec_Direct.Tables[1].Rows[i]["Person_id_Ejecutive"].ToString().Trim()));
        //            }
        //        }
        //        ListaEjeCuentaenBuscar();
        //    }

        //}
        //protected void cmbBSelPaisDirect_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbBSelPaisDirect.SelectedValue != "0")
        //    {
        //        comboBAsignaDirCuenta();
        //        ModalPopupExtender1.Show();
        //    }
        //    else
        //    {
        //        ModalPopupExtender1.Show();
        //    }

        //}
        //protected void cmbBSelNameDirect_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (CmbSelPaisDirCu.SelectedValue != "0")
        //    {
        //        comboAsignaDirCuenta();
        //        ListaEjeCuenta();
        //    }
        //    else
        //    {
        //        CmbSelDirCu.Items.Clear();
        //        LstBoxPersonalSinAsing.Items.Clear();
        //        LstBoxPersonalAsing.Items.Clear();
        //    }
        //}      
        //protected void BtnEditAsignEje_Click(object sender, EventArgs e)
        //{
        //    BtnUpdateAsignEje.Visible = true;
        //    BtnEditAsignEje.Visible = false;
        //    activarControlesAsignación();
        //    CmbSelPaisDirCu.Enabled = false;
        //    CmbSelDirCu.Enabled = false;
        //}
        //#endregion

        #region Asignar Clientes a Usuarios

        private void cargar_ddl_cxu_usuario()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LISTAUSUARIOS");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    //se llena combo canales en Asignación de informe a Ususario por Cliente seleccionado
                    ddl_cxu_usuario.DataSource = dt;
                    ddl_cxu_usuario.DataTextField = "name_user";
                    ddl_cxu_usuario.DataValueField = "Person_id";
                    ddl_cxu_usuario.DataBind();
                    ddl_cxu_usuario.Items.Insert(0, new RadComboBoxItem("<Seleccione..>", "0"));
                    dt = null;
                }
                else
                {
                    ddl_cxu_usuario.Items.Clear();
                }
            }
        }

        protected void btn_cxu_crear_Click(object sender, EventArgs e)
        {
            btn_cxu_guardar.Visible = true;
            activarcontroles_cli_x_usu();
            cargar_ddl_cxu_usuario();
            //ddl_cxu_usuario.AllowCustomText = true;
            //ddl_cxu_usuario.EnableTextSelection = true;
            //ddl_cxu_usuario.MarkFirstMatch = true;
            btn_cxu_crear.Visible = false;
        }

        private void cargar_cbxl_cxu_cliente()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_Consultar_CLIENTES");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    cbxl_cxu_cliente.DataSource = dt;
                    cbxl_cxu_cliente.DataTextField = "Company_Name";
                    cbxl_cxu_cliente.DataValueField = "Company_id";
                    cbxl_cxu_cliente.DataBind();
                    dt = null;
                }
                else
                {
                    cbxl_cxu_cliente.Items.Clear();
                }
            }
        }

        private void activarcontroles_cli_x_usu()
        {
            ddl_cxu_usuario.Enabled = true;
            cbxl_cxu_cliente.Enabled = true;
        }

        protected void ddl_cxu_usuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable user_cliente = new DataTable();
                int codusuario = Convert.ToInt32(ddl_cxu_usuario.SelectedValue);
                user_cliente = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LISTA_CLIENTE_X_USUARIO", codusuario, 0);
                //cbxl_cxu_cliente.Items.Clear();
                int numrows = user_cliente.Rows.Count;
                if (user_cliente.Rows.Count > 0)
                {
                    foreach (ListItem item in cbxl_cxu_cliente.Items)
                    {
                        item.Selected = false;
                        for (int x = 0; x < numrows; x++)
                        {
                            if (item.Value == user_cliente.Rows[x]["company_id"].ToString())
                            {
                                item.Selected = Convert.ToBoolean(user_cliente.Rows[x]["Status"]);
                                x = numrows;
                            }
                        }
                    }
                    user_cliente = null;
                }
                else
                {
                    foreach (ListItem item in cbxl_cxu_cliente.Items)
                    {
                        item.Selected = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Lo sentimos, ha ocurrido un error en la consulta vuelva a intentarlo";
                MensajeAlerta();
            }
        }

        protected void btn_cxu_guardar_Click(object sender, EventArgs e)
        {
            if (ddl_cxu_usuario.SelectedIndex != 0 && cbxl_cxu_cliente.SelectedItem != null)
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
                int codusuario = Convert.ToInt32(ddl_cxu_usuario.SelectedValue);

                foreach (ListItem item in cbxl_cxu_cliente.Items)
                {
                    int codcliente = Convert.ToInt32(item.Value);
                    bool nodoxcanal_estado = item.Selected;
                    try
                    {
                        st = ouser.registrarClientesxUsuario(codusuario, codcliente, nodoxcanal_estado, username);
                    }
                    catch (Exception ex) { }
                }

                Alertas.CssClass = "MensajesCorrecto";
                LblFaltantes.Text = "Asignación Clientes a Usuario registrada correctamente.";
                MensajeAlerta();
                ddl_cxu_usuario.SelectedIndex = 0;
                foreach (ListItem item in cbxl_cxu_cliente.Items)
                {
                    item.Selected = false;
                }
            }
            else
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Seleccione el Usuario y los Clientes a asociar";
                MensajeAlerta();
            }
        }

        protected void btn_cxu_cancelar_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in cbxl_cxu_cliente.Items)
            {
                item.Selected = false;
            }
            ddl_cxu_usuario.SelectedIndex = 0;
            ddl_cxu_usuario.Enabled = false;
            cbxl_cxu_cliente.Enabled = false;
            btn_cxu_crear.Visible = true;
            btn_cxu_guardar.Visible = false;
        }

        protected void btn_cxu_buscar_Click(object sender, EventArgs e)
        {
          
            Pmensaclave.Visible = false;
            lblclave.Text = "";
            llenar_rgv_usuario_x_cliente();
            mp_ClientexUsuario.Hide();
            mp_consulta_rgv_clientxuser.Show();
        }

        private void llenar_rgv_usuario_x_cliente()
        {
            DataSet dtusers = new DataSet();
            Usuario ouser = new Usuario();
            int cliente = Convert.ToInt32(ddl_cxub_cliente.Text);
            dtusers = ouser.buscarClientesxUsuario(0, cliente);
            Session["dt_users"] = dtusers;
            rgv_cxu_users.DataSource = dtusers;
            rgv_cxu_users.DataBind();
        }

        private void llenar_ddl_cxub_cliente()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_CLIENTES_EXTERNOS");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddl_cxub_cliente.DataSource = dt;
                    ddl_cxub_cliente.DataTextField = "Company_Name";
                    ddl_cxub_cliente.DataValueField = "Company_id";
                    ddl_cxub_cliente.DataBind();
                    ddl_cxub_cliente.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
                    dt = null;
                }
                else
                {
                    ddl_cxub_cliente.Items.Clear();
                }
            }
            foreach (ListItem item in ddl_cxub_cliente.Items)
            {
                if (item.Text.Length > 30)
                    item.Text = item.Text.Substring(0, 30);
            }
        }

        protected void rgv_cxu_users_EditCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            mp_consulta_rgv_clientxuser.Show();
            editedItem.Edit = true;
            rgv_cxu_users.DataSource = (DataSet)(Session["dt_users"]);
            rgv_cxu_users.DataBind();
        }

        protected void rgv_cxu_users_UpdateCommand(object source, GridCommandEventArgs e)
        {
            Usuario ouser = new Usuario();
            GridEditableItem editedItem = e.Item as GridEditableItem;
            int codusuario = Convert.ToInt32((editedItem["Person_id"].Controls[0] as TextBox).Text);
            int codcliente = Convert.ToInt32((editedItem["company_id"].Controls[0] as TextBox).Text);
            CheckBox cbx_stado = editedItem["Status"].Controls[0] as CheckBox;
            bool nodoxcanal_estado = cbx_stado.Checked;
            string username = "";
            try
            {
                username = Convert.ToString(this.Session["sUser"]);
            }
            catch (Exception ex)
            {
            }
            int st = 0;
            st = ouser.registrarClientesxUsuario(codusuario, codcliente, nodoxcanal_estado, username);
            llenar_rgv_usuario_x_cliente();
            mp_consulta_rgv_clientxuser.Show();
        }

        protected void rgv_cxu_users_CancelCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            editedItem.Edit = false;
            rgv_cxu_users.DataSource = (DataSet)(Session["dt_users"]);
            mp_consulta_rgv_clientxuser.Show();
        }

        protected void btn_cxucancelar_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in cbxl_cxu_cliente.Items)
            {
                item.Selected = false;
            }
            ddl_cxu_usuario.SelectedIndex = 0;
            ddl_cxu_usuario.Enabled = false;
            cbxl_cxu_cliente.Enabled = false;
            btn_cxu_crear.Visible = true;
            btn_cxu_guardar.Visible = false;
        } 
        #endregion


        #region Tipo Reporte

        void ActivarControles()
        {
            btnTipoReporte_Crear.Visible = false;
            btnTipoReporte_Guardar.Visible = true;
            btnTipoReporte_Consultar.Visible = false;

            txtTipoReporte_Codigo.Enabled = true;
            txtTipoReporte_Descripcion.Enabled = true;
            txtTipoReporte_TipoLevantamiento.Enabled = true;

            ddlTipoReporte_Cliente.Enabled = true;
            ddlTipoReporte_Reporte.Enabled = true;
            //ddlTipoReporte_Canal.Enabled = true;

        }

        void DesactivarControles()
        {
            btnTipoReporte_Crear.Visible = true;
            btnTipoReporte_Guardar.Visible = false;
            btnTipoReporte_Consultar.Visible = true;

            txtTipoReporte_Codigo.Enabled = false;
            txtTipoReporte_Descripcion.Enabled = false;
            txtTipoReporte_TipoLevantamiento.Enabled = false;

            ddlTipoReporte_Cliente.Enabled = false;
            ddlTipoReporte_Reporte.Enabled = false;
            ddlTipoReporte_Canal.Enabled = false;
        }

        void limpiarControles()
        {


            txtTipoReporte_Codigo.Text = "";
            txtTipoReporte_Descripcion.Text = "";
            txtTipoReporte_TipoLevantamiento.Text = "";

            ddlTipoReporte_Cliente.Items.Clear();
            ddlTipoReporte_Reporte.Items.Clear();
            ddlTipoReporte_Canal.Items.Clear();
        }

        private void llenaTipoReporte_Canal()
        {



            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSREPORCHANNEL", Convert.ToInt32(ddlTipoReporte_Cliente.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlTipoReporte_Canal.DataSource = ds.Tables[1];
                ddlTipoReporte_Canal.DataTextField = "Channel_Name";
                ddlTipoReporte_Canal.DataValueField = "cod_Channel";
                ddlTipoReporte_Canal.DataBind();
                ddlTipoReporte_Canal.Enabled = true;
            }
            else
            {
                ddlTipoReporte_Canal.Items.Clear();
                ddlTipoReporte_Canal.Enabled = false;
            }


        }
        private void llenaTipoReporte_Reporte()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSREPORCHANNEL", 0);
            ddlTipoReporte_Reporte.DataSource = ds.Tables[2];
            ddlTipoReporte_Reporte.DataTextField = "Report_NameReport";
            ddlTipoReporte_Reporte.DataValueField = "Report_Id";
            ddlTipoReporte_Reporte.DataBind();


            ddlptipoperfil_Reporte.DataSource = ds.Tables[2];
            ddlptipoperfil_Reporte.DataTextField = "Report_NameReport";
            ddlptipoperfil_Reporte.DataValueField = "Report_Id";
            ddlptipoperfil_Reporte.DataBind();

            

        }

        private void llenaTipoReporte_ReporteB()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSREPORCHANNEL", 0);

            ddlptipoperfil_Reporte.DataSource = ds.Tables[2];
            ddlptipoperfil_Reporte.DataTextField = "Report_NameReport";
            ddlptipoperfil_Reporte.DataValueField = "Report_Id";
            ddlptipoperfil_Reporte.DataBind();



        }




        private void llenaTipoReporte_Cliente()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_Consultar_CLIENTES");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlTipoReporte_Cliente.DataSource = dt;
                    ddlTipoReporte_Cliente.DataTextField = "Company_Name";
                    ddlTipoReporte_Cliente.DataValueField = "Company_id";
                    ddlTipoReporte_Cliente.DataBind();
                    ddlTipoReporte_Cliente.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

                    
                    dt = null;
                }
                else
                {
                    ddlTipoReporte_Cliente.Items.Clear();
                }
            }
        }

        private void llenaTipoReporte_ClienteB()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_Consultar_CLIENTES");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                   
                    ddlptipoperfil_Cliente.DataSource = dt;
                    ddlptipoperfil_Cliente.DataTextField = "Company_Name";
                    ddlptipoperfil_Cliente.DataValueField = "Company_id";
                    ddlptipoperfil_Cliente.DataBind();
                    ddlptipoperfil_Cliente.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

                    dt = null;
                }
                else
                {
                    ddlptipoperfil_Cliente.Items.Clear();
                }
            }
        }



        



        protected void btnTipoReporte_Crear_Click(object sender, EventArgs e)
        {
            llenaTipoReporte_Cliente();
            
            llenaTipoReporte_Reporte();
            ActivarControles();
        }

        protected void btnTipoReporte_Guardar_Click(object sender, EventArgs e)
        {
            OPE_Tipo_Reporte oOPE_Tipo_Reporte = new OPE_Tipo_Reporte();
            try
            {

                if (txtTipoReporte_Codigo.Text == "" || txtTipoReporte_Descripcion.Text == "" || ddlTipoReporte_Cliente.SelectedValue == "0" || txtTipoReporte_TipoLevantamiento.Text == ""
                    || ddlTipoReporte_Reporte.SelectedValue == "0" || ddlTipoReporte_Canal.SelectedValue == "0")
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Por favor llene todos los campos";
                    MensajeAlerta();
                    return;
                }
                else
                {
                    DataTable dt = oOPE_Tipo_Reporte.ConsultarTipo_Reporte(txtTipoReporte_Descripcion.Text, ddlTipoReporte_Cliente.SelectedValue, ddlTipoReporte_Reporte.SelectedValue);
                    if(dt.Rows.Count==0)
                    {
                         oOPE_Tipo_Reporte.RegistrarTipo_Reporte(txtTipoReporte_Codigo.Text, txtTipoReporte_Descripcion.Text, ddlTipoReporte_Cliente.SelectedValue, txtTipoReporte_TipoLevantamiento.Text, ddlTipoReporte_Reporte.SelectedValue, ddlTipoReporte_Canal.SelectedValue);
                         Alertas.CssClass = "MensajesCorrecto";
                         LblFaltantes.Text = "El tipo de reporte fue creado.";
                         MensajeAlerta();
                    }
                    else
                    {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "ya existe ese tipo de reporte para el Cliente seleccionado";
                            MensajeAlerta();

                    }
 
                }

            }
            catch
            {
 
            }
        }

        protected void btnTipoReporte_Cancelar_Click(object sender, EventArgs e)
        {
            limpiarControles();
            DesactivarControles();
        }

        protected void ddlTipoReporte_Cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaTipoReporte_Canal();
        }

        protected void ptipoperfil_Consultar_Click(object sender, EventArgs e)
        {
            CargagrillaTipoPerfil();

        }

        #endregion





        protected void gv_TipoPerfil_CancelCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                CargagrillaTipoPerfil();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_TipoPerfil_EditCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                CargagrillaTipoPerfil();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_TipoPerfil_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                gv_TipoPerfil.CurrentPageIndex = e.NewPageIndex;
                CargagrillaTipoPerfil();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_TipoPerfil_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            try
            {
                CargagrillaTipoPerfil();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_TipoPerfil_UpdateCommand(object source, GridCommandEventArgs e)
        {
            OPE_Tipo_Reporte oOPE_Tipo_Reporte = new OPE_Tipo_Reporte();
            try
            {

                Conexion Ocoon = new Conexion();

                GridItem item = gv_TipoPerfil.Items[e.Item.ItemIndex];

                Label lbl_id_Tipo_Reporte = (Label)item.FindControl("lbl_id_Tipo_Reporte");
                string id_Tipo_Reporte = lbl_id_Tipo_Reporte.Text.Trim();


                Label lbl_TipoReporte_Descripcion = (Label)item.FindControl("lbl_TipoReporte_Descripcion");
                string TipoReporte_Descripcion = lbl_TipoReporte_Descripcion.Text.Trim();


                Label lbl_company_id = (Label)item.FindControl("lbl_company_id");
                string company_id = lbl_company_id.Text.Trim();


                Label lbl_report_id = (Label)item.FindControl("lbl_report_id");
                string report_id = lbl_report_id.Text.Trim();


                Label lblcod_channel = (Label)item.FindControl("lblcod_channel");
                string cod_channel = lblcod_channel.Text.Trim();


                List<object> ArrayEditorValue = new List<object>();

                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditManager editMan = editedItem.EditManager;

                foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
                {
                    if (column is IGridEditableColumn)
                    {
                        IGridEditableColumn editableCol = (column as IGridEditableColumn);
                        if (editableCol.IsEditable)
                        {
                            IGridColumnEditor editor = editMan.GetColumnEditor(editableCol);

                            string editorType = editor.ToString();
                            string editorText = "unknown";
                            object editorValue = null;

                            if (editor is GridTextColumnEditor)
                            {
                                editorText = (editor as GridTextColumnEditor).Text;
                                editorValue = (editor as GridTextColumnEditor).Text;
                                ArrayEditorValue.Add(editorValue);
                            }

                            if (editor is GridDateTimeColumnEditor)
                            {
                                editorText = (editor as GridDateTimeColumnEditor).Text;
                                editorValue = (editor as GridDateTimeColumnEditor).PickerControl;
                                ArrayEditorValue.Add(editorValue);
                            }
                            if (editor is GridDropDownColumnEditor)
                            {
                                editorText = (editor as GridDropDownColumnEditor).SelectedText;
                                editorValue = (editor as GridDropDownColumnEditor).SelectedValue;
                                ArrayEditorValue.Add(editorValue);
                            }
                        }
                    }
                }

                string tipolevantamiento = ArrayEditorValue[0].ToString();





                oOPE_Tipo_Reporte.ActualizarTipo_Reporte(id_Tipo_Reporte, TipoReporte_Descripcion, company_id, tipolevantamiento, report_id, cod_channel);
           

                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "Los datos fuero actualizados correctamente";
                    MensajeAlerta();



                CargagrillaTipoPerfil();
            }
            catch (Exception ex)
            {

                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        public void CargagrillaTipoPerfil()
        {
            OPE_Tipo_Reporte oOPE_Tipo_Reporte = new OPE_Tipo_Reporte();
            DataTable dt = oOPE_Tipo_Reporte.ConsultarTipo_Reporte(txtptipoperfil_descripcion.Text, ddlptipoperfil_Cliente.SelectedValue, ddlptipoperfil_Reporte.SelectedValue);
            Session["llena"] = dt;
            gv_TipoPerfil.DataSource = dt;
            gv_TipoPerfil.DataBind();
            btngv_TipoPerfilCancelar.Visible = true;
            panel_principal.Visible = false;
           // paneel.Visible = false;
            ActivarControles();

        }

        protected void btngv_TipoPerfilCancelar_Click(object sender, EventArgs e)
        {
            div.Visible = false;
            panel_principal.Visible = true;
            DesactivarControles();
        }
        /// <summary>
        /// Accion: Desencripta la Clave de Usuario
        /// Creado el : 22/03/2012
        /// Creado Por: Ing. Carlos Alberto Hernandez
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btndeco_Click(object sender, EventArgs e)
        {
            lblclave.Text = "";
            string Key, clave;
            Key = ConfigurationManager.AppSettings["TamperProofKey"];
            clave = TxtPsw.Text;

            try
            {
                if (clave != ConfigurationManager.AppSettings["ClaveOpe"])
                {

                    string spasEncriptado = Lucky.CFG.Util.Encriptacion.DesCodificar(clave, Key);
                    lblclave.Text = spasEncriptado;
                    Pmensaclave.Visible = true;
                    btndeco.Enabled = false;
                }
                else
                {
                    Pmensaclave.Visible = true;
                    lblclave.Text = "Clave no Encriptada";
                    btndeco.Enabled = false;



                }

               

           }
           

            catch (Exception ex) {

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);

                mesjerror.errorWebsite(ConfigurationManager.AppSettings["COUNTRY"]);
                Pmensaclave.Visible = true;
                lblclave.Text = "Clave no Encriptada";
                btndeco.Enabled = false;
               
            
            
            
            }
          
        }

       



    }
}