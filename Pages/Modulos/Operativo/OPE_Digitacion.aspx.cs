using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.IO;
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

namespace SIGE.Pages.Modulos.Operativo
{
    public partial class Ope_Digitacion : System.Web.UI.Page
    {
        #region WEB SERVICES - INICIO
        Facade_Proceso_Planning.Facade_Proceso_Planning wsPlanning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE Presupuesto = new SIGE.Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE();
        #endregion WEB SERVICES - FIN

        Conexion oCoon = new Conexion();
        
        #region 3 CAPAS - INICIO
        OPE_DigFORMATOFINALSOD oOPE_DigFORMATOFINALSOD = new OPE_DigFORMATOFINALSOD();
        OPE_DigFORMATOFINALESPRECIOS oOPE_DigFORMATOFINALESPRECIOS = new OPE_DigFORMATOFINALESPRECIOS();
        OPE_DigFORMATOFINALESDG oOPE_DigFORMATOFINALESDG = new OPE_DigFORMATOFINALESDG();
        OPE_DigFORMATOAGCOCINA oOPE_DigFORMATOAGCOCINA = new OPE_DigFORMATOAGCOCINA();
        #endregion 3 CAPAS - FIN 

        #region VARIABLES DE SESSION - INICIO
        string sUser;
        string sPassw;
        string sNameUser;
        int Company_id;
        #endregion VARIABLES DE SESSION - FIN

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    #region CREA VARIABLES DE SESSION - INICIO
                    sUser = this.Session["sUser"].ToString();
                    sPassw = this.Session["sPassw"].ToString();
                    sNameUser = this.Session["nameuser"].ToString();
                    #endregion CREA VARIABLES DE SESSION - FIN
                    usersession.Text = sUser;
                    lblUsuario.Text = sNameUser;

                    if (sUser != null && sPassw != null)
                    {
                        LlenaPresupuestosAsignados();
                        LlenaFormatos();
                        InicializarFiltros();
                        
                    }
                }
                catch (Exception ex)
                {
                    this.Session.Abandon();
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
        }

        protected void ImgCloseSession_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.Session.Abandon();
                Response.Redirect("~/login.aspx", true);
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/login.aspx", true);
            }
        }

        
        #region MENSAJE DE USUARIO - MODALPOPPUP CANAL
        /// <summary>
        /// Muestra los mensajes de usuario 
        /// </summary>
        private void Mensajes_Usuario()
        {
            string CssClass = this.Session["cssclass"].ToString();
            string TxtEncabezado = this.Session["encabemensa"].ToString();
            string TxtMensaje = this.Session["mensaje"].ToString();
            Panel_Mensaje.CssClass = CssClass;
            lblencabezado.Text = TxtEncabezado;
            lblmensajegeneral.Text = TxtMensaje;
            ModalPopupCanal.Show();
        }
        #endregion MENSAJE DE USUARIO - MODALPOPPUP CANAL

        #region INICIALIZA FILTROS - INICIO
        /// <summary>
        /// 1.- Filtro.Visible=false.
        /// 2.- FiltroDetalle.Visible=false.
        /// 3.- CmbFormato.Enabled=true;
        /// 4.- CmbCampaña.Enabled=true;
        /// 5.- Todo lo demás Visible=false;
        /// </summary>
        private void InicializarFiltros()
        {
            #region FILTROS - INI
            Filtros.Visible = false;
            FiltrosDetalle.Visible = false;
            #endregion FILTROS - FIN

            #region GRILLAS
            GvDigitacion.DataBind();
            GVDigitacionPrecios.DataBind();
            GvDigitacionDG.DataBind();
            GvDigitacionAGCocina.DataBind();
            GvDigitacionMimaskot.DataBind();
            GvDigitacionPromGalletas.DataBind();
            GvDigitacionHojaControl.DataBind();

            #region hoja de control
            GvDigitacionHojaControlAbarrotes.DataBind();
            GvDigitacionHojaControlAbarrotes_Premios.DataBind();
            GvIntermedia.DataBind();
            #endregion hoja de control

            #endregion GRILLAS

            #region combos
            CmbFormato.Enabled = true;
            CmbCampaña.Enabled = true;
            #endregion combos

            #region LBL Y TXT - INICIO
            #region FILTRO PRINCIPAL
            #region fecha ini
            LblFecha.Visible = false;
            TxtFecha.Visible = false;
            #endregion fecha ini
            #region fecha fin
            LblFechaFin.Visible = false;
            TxtFechaFin.Visible = false;
            #endregion fecha fin
            #region imagenes calendario
            ImageButtonCal.Visible = false;
            ImageButtonCalFin.Visible = false;
            #endregion imagenes calendario
            #region mercadersita
            LblOperativo.Visible = false;
            CmbOperativo.Visible = false;
            #endregion
            #region cliente
            LblCodCliente.Visible = false;
            TxtCliente.Visible = false;
            LblCliente.Visible = false;
            #endregion cliente
            #region ciudad
            LblCiudad.Visible = false;
            CmbCiudad.Visible = false;
            #endregion ciudad
            #region zona mayorista
            LblZonaMayor.Visible = false;
            CmbZonaMayor.Visible = false;
            #endregion zona mayorista
            #region lugar impulso
            LblLugarImpulso.Visible = false;
            TxtLugarDireccion.Visible = false;
            #endregion lugar impulso
            #region punto de venta
            LblPDV.Visible = false;
            TxtPDV.Visible = false;
            #endregion punto de venta
            #region mercado
            LblMercado.Visible = false;
            CmbMercado.Visible = false;
            #endregion mercado
            #region Nombre cliente mayorista
            LblNombreClieMayorista.Visible = false;
            TxtNombreClieMayorista.Visible = false;
            #endregion Nombre cliente mayorista
            #endregion FILTRO PRINCIPAL

            #region FILTRO DETALLES
            #region Cod ClienteDetalle - Punto de Venta
            LblCodClienteDetalle.Visible = false;
            TxtClienteDetalle.Visible = false;
            LblClienteDetalle.Visible = false;
            #endregion Cod ClienteDetalle - Punto de Venta
            #region factura / boleta
            LblFacturaBoleta.Visible = false;
            TxtFacturaBoleta.Visible = false;
            #endregion factura / boleta
            #region DNI
            LblDNI.Visible = false;
            TxtDNI.Visible = false;
            #endregion DNI
            #region nombre cliente minorista
            LblNombreClienteMinorista.Visible = false;
            TxtNombreClienteMinorista.Visible = false;
            #endregion nombre cliente minorista
            #region categoria productos
            LblCategoria.Visible = false;
            CmbCategoria.Visible = false;
            #endregion categoria productos
            #region telefono
            LblTelefono.Visible = false;
            TxtTelefono.Visible = false;
            #endregion telefono
            #region ruc
            LblRuc.Visible = false;
            TxtRuc.Visible = false;
            #endregion ruc
            #region monto en soles
            LblMontoSoles.Visible = false;
            TxtMontoSoles.Visible = false;
            #endregion
            #endregion FILTRO DETALLES

            #endregion LBL Y TXT - FIN


        }
        #endregion INICIALIZA FILTROS - FIN
       
        #region OCULTAR BOTONES
        /// <summary>
        /// 1.-SuperCabecera:Nuevo.Enabled=true; 
        /// 2.-SuperCabecera:Consultar.Enabled=true;
        /// 3.-FiltroPrincipal.Visible=False.
        /// 4.-FiltroDetalle.Visible=False.
        /// 5.-Planning inicializar en 0.
        /// 6.-Formatos inicializar en 0.
        /// </summary>
        private void cancelarActivarbotones()
        {

            #region BOTONES SUPERCABECERA

            ImgSearchDigitacion.Visible = true;
            ImgNewDigitacion.Visible = true;

            ImageButton1.Visible = false;
            ImgCancelDigitacion.Visible = false;

            #endregion BOTONES SUPERCABECERA

            #region BOTONES FILTRO VISIBLE - TRUE/FALSE
            ImgEditDigitacion.Visible = false;
            ImgHabEditDigitacion.Visible = false;
            ImgUpdateDigitacion.Visible = false;
            ImgCancelEditDigitacion.Visible = false;
            ImgGuardarCabeceraOpeDigitacion.Visible = false;
            #endregion BOTONES FILTRO VISIBLE - TRUE/FALSE

            #region BOTONES DETALLES: VISIBLE - TRUE/FALSE
            ImgGuardarDetalle.Visible = false;
            ImgCancelarDetalle.Visible = false;
            #endregion BOTONES DETALLES : VISIBLE -TRUE/FALSE

            #region INICIALIZAR PLANNING EN <SELECCIONAR>
            Lbl_Campaña.Visible = false;
            CmbCampaña.Text = "0";
            CmbCampaña.Visible = false;
            #endregion INICIALIZAR PLANNING EN <SELECCIONAR>

            #region INICIALIZAR FORMATO EN <SELECCIONAR>
            Lbl_Formato.Visible = false;
            CmbFormato.Text = "0";
            CmbFormato.Visible = false;
            #endregion INICIALIZAR FORMATO EN <SELECCIONAR>
            
        }
        #endregion OCULTAR BOTONES
        #region ACTIVAR BOTONES
        /// <summary>
        /// 1.-SuperCabecera:Cancelar.Enabled=true;
        /// 2.-FiltroPrincipal:Consultar.Enabled=true;
        /// </summary>
        private void consultarActivarbotones()
        {
            #region BOTONES SUPERCABECERA: VISIBLE:TRUE/FALSE
            ImgNewDigitacion.Visible = false;
            ImageButton1.Visible = false; //BtnGuardar
            ImgSearchDigitacion.Visible = false;
            ImgCancelDigitacion.Visible = true;
            #endregion BOTONES SUPERCABECERA: VISIBLE:TRUE/FALSE

            #region BOTONES FILTRO PRINCIPAL
            ImgEditDigitacion.Visible = true;//BtnConsultar
            ImgHabEditDigitacion.Visible = false;//BtnEditar
            ImgUpdateDigitacion.Visible = false;
            #endregion BOTONES FILTRO PRINCIPAL

        }
        #endregion ACTIVAR BOTONES

        #region INHABILITAR FILTROS PRINCIPALES
        /// <summary>
        /// 1.-SuperCabecera:CmbPlanning.Enabled=false.
        /// 2.-SuperCabecera:CmbFormato.Enabled=false.
        /// 2.-FiltroPrincipal.Enabled=false.
        /// </summary>
        private void InhabilitarObjetos()
        {
            #region INHABILITAR CMB PRINCIPALES
            CmbCampaña.Enabled = false;
            CmbFormato.Enabled = false;
            #endregion INHABILITAR CMB PRINCIPALES

            #region INHABILITAR FECHA INI Y FIN
            TxtFecha.Enabled = false;
            TxtFechaFin.Enabled = false;
            #endregion INHABILITAR FECHA INI Y FIN
            #region INHABILITAR BOTONES DE FECHA INI - FIN
            ImageButtonCal.Enabled = false;
            ImageButtonCalFin.Enabled = false;
            #endregion INHABILITAR BOTONES DE FECHA INI - FIN
            #region INHABILITAR CMB MERCADERISTA
            CmbOperativo.Enabled = false;
            #endregion INHABILITAR CMB MERCADERISTA
            #region INHABILITAR ZONA MAYORISTA - NODO COMERCIAL
            CmbZonaMayor.Enabled = false;
            #endregion INHABILITAR ZONA MAYORISTA - NODO COMERCIAL
            #region INHABILITAR MERCADO - NODO COMERCIAL
            CmbMercado.Enabled = false;
            #endregion INHABILITAR MERCADO - NODO COMERCIAL
            #region INHABILITAR CLIENTE - PUNTO DE VENTA
            TxtCliente.Enabled = false;
            #endregion INHABILITAR CLIENTE - PUNTO DE VENTA
            #region INHABILITAR CIUDAD
            CmbCiudad.Enabled = false;
            #endregion INHABILITAR CIUDAD
            #region INHABILITAR DIRECCION / LUGAR IMPULSO
            TxtLugarDireccion.Enabled = false;
            #endregion INHABILITAR DIRECCION / LUGAR IMPULSO
            #region INHABILITAR CATEGORIA
            CmbCategoria.Enabled = false;
            #endregion INHABILITAR CATEGORIA
            #region INHABILITAR PUNTO DE VENTA - PDV - (OBSOLETO)
            TxtPDV.Enabled = false;
            #endregion INHABILITAR PUNTO DE VENTA - PDV - (OBSOLETO)
            #region INHABILITAR NOMBRE CLIENTE MAYORISTA
            TxtNombreClieMayorista.Enabled = false;
            #endregion INHABILITAR NOMBRE CLIENTE MAYORISTA

        }
        #endregion INHABILITAR FILTROS PRINCIPALES
        #region HABILITAR FILTRO PRINCIPALES
        /// <summary>
        /// 1.-Habilitar CMBs Principales.
        /// 2.-Habilitar FiltroPrincipal.
        /// </summary>
        private void HabilitarObjetos()
        {
            #region HABILITAR CMBs PRINCIPALES
            CmbCampaña.Enabled = true;
            CmbFormato.Enabled = true;
            #endregion HABILITAR CMBs PRINCIPALES

            #region HABILITAR FECHA INI
            TxtFecha.Enabled = true;
            ImageButtonCal.Enabled = true;
            #endregion HABILITAR FECHA INI
            #region HABILITAR FECHA FIN
            TxtFechaFin.Enabled = true;
            ImageButtonCalFin.Enabled = true;
            #endregion HABILITAR FECHA FIN
            #region HABILITAR MERCADERISTA
            CmbOperativo.Enabled = true;
            #endregion HABILITAR MERCADERISTA
            #region HABILITAR ZONA MAYOR
            CmbZonaMayor.Enabled = true;
            #endregion HABILITAR ZONA MAYOR
            #region HABILITAR MERCADO - NODO COMERCIAL
            CmbMercado.Enabled = true;
            #endregion HABILITAR MERCADO - NODO COMERCIAL
            #region HABILITAR CLIENTE - PUNTO DE VENTA
            TxtCliente.Enabled = true;
            #endregion HABILITAR CLIENTE - PUNTO DE VENTA
            #region HABILITAR CIUDAD
            CmbCiudad.Enabled = true;
            #endregion HABILITAR CIUDAD
            #region HABILITAR LUGAR DE IMPULSO - DIRECCION
            TxtLugarDireccion.Enabled = true;
            #endregion HABILITAR LUGAR DE IMPULSO - DIRECCION
            #region HABILITAR CATEGORIA
            CmbCategoria.Enabled = true;
            #endregion HABILITAR CATEGORIA

        }
        #endregion HABILITAR FILTRO PRINCIPALES

        #region IDENTIFICA PARAMETROS PARA GRILLA EN HOJA DE CONTROL
        private void IdentificarParametros(int iformato)
        {
            DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_IDENTIFICA_PARAMETROS", iformato);
        }
        #endregion IDENTIFICA PARAMETROS PARA GRILLA EN HOJA DE CONTROL
        #region IDENTIFICA FILTROS POR FORMATO
        private void IdentificarFiltros(int iformato)
        {
            InicializarFiltros();
            DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_FILTROS", iformato);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Filtros.Disabled = false;
                    Filtros.Visible = true;

                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        #region IDENTIFICA: FILTROS PRINCIPALES
                        #region IDENTIFICA FECHA INI
                        if (dt.Rows[i][0].ToString().Trim() == "Fecha")
                        {
                            LblFecha.Visible = true;
                            TxtFecha.Visible = true;
                            ImageButtonCal.Visible = true;
                        }
                        #endregion IDENTIFICA FECHA INI
                        #region IDENTIFICA FECHA FIN
                        if (dt.Rows[i][0].ToString().Trim() == "Fecha Fin")
                        {
                            LblFechaFin.Visible = true;
                            TxtFechaFin.Visible = true;
                            ImageButtonCalFin.Visible = true;
                        }
                        #endregion IDENTIFICA FECHA FIN
                        #region IDENTIFICA MERCADERISTA
                        if (dt.Rows[i][0].ToString().Trim() == "Promotor / Mercaderista")
                        {
                            LblOperativo.Visible = true;
                            CmbOperativo.Visible = true;
                        }
                        #endregion IDENTIFICA MERCADERISTA
                        #region IDENTIFICA ZONA MAYORISTA
                        if (dt.Rows[i][0].ToString().Trim() == "Zona Mayorista")
                        {
                            LblZonaMayor.Visible = true;
                            CmbZonaMayor.Visible = true;
                        }
                        #endregion IDENTIFICA MERCADO
                        #region IDENTIFICA MERCADO
                        if (dt.Rows[i][0].ToString().Trim() == "Mercado")
                        {
                            LblMercado.Visible = true;
                            CmbMercado.Visible = true;
                        }
                        #endregion IDENTIFICA MERCADO
                        #region IDENTIFICA CODIGO CLIENTE - PUNTO DE VENTA
                        if (dt.Rows[i][0].ToString().Trim() == "Codigo De Cliente")
                        {
                            LblCodCliente.Visible = true;
                            TxtCliente.Visible = true;
                            LblCliente.Visible = true;
                        }
                        #endregion IDENTIFICA CODIGO CLIENTE - PUNTO DE VENTA
                        #region IDENTIFICA CIUDAD
                        if (dt.Rows[i][0].ToString().Trim() == "Ciudad")
                        {
                            LblCiudad.Visible = true;
                            CmbCiudad.Visible = true;
                        }
                        #endregion IDENTIFICA CIUDAD
                        #region IDENTIFICA LUGAR IMPULSO / DIRECCION
                        if (dt.Rows[i][0].ToString().Trim() == "Lugar De Impulso / Dirección")
                        {
                            LblLugarImpulso.Visible = true;
                            TxtLugarDireccion.Visible = true;
                        }
                        #endregion IDENTIFICA LUGAR IMPULSO / DIRECCION
                        #region IDENTIFICA PUNTO DE VENTA - OBSOLETO
                        if (dt.Rows[i][0].ToString().Trim() == "Pdv")
                        {
                            LblPDV.Visible = true;
                            TxtPDV.Visible = true;
                        }
                        #endregion IDENTIFICA PUNTO DE VENTA - OBSOLETO
                        #region IDENTIFICA NOMBRE CLIENTE MAYORISTA - OBSOLETO
                        if (dt.Rows[i][0].ToString().Trim() == "Cliente Mayorista")
                        {
                            LblNombreClieMayorista.Visible = true;
                            TxtNombreClieMayorista.Visible = true;
                        }
                        #endregion IDENTIFICA NOMBRE CLIENTE MAYORISTA - OBSOLETO
                        #region IDENTIFICA CATEGORIA DE PRODUCTO
                        if (dt.Rows[i][0].ToString().Trim() == "Categoria")
                        {
                            LblCategoria.Visible = true;
                            CmbCategoria.Visible = true;
                        }
                        #endregion IDENTIFICA CATEGORIA DE PRODUCTO
                        #endregion IDENTIFICA FILTROS PRINCIPALES

                        #region IDENTIFICA:FILTROS DETALLES
                        #region IDENTIFICA CODIGO CLIENTE - PUNTO DE VENTA
                        if (dt.Rows[i][0].ToString().Trim() == "Codigo De Cliente Detalle")
                        {
                            LblCodClienteDetalle.Visible = true;
                            TxtClienteDetalle.Visible = true;
                            LblClienteDetalle.Visible = true;
                        }
                        #endregion IDENTIFICA CODIGO CLIENTE - PUNTO DE VENTA
                        #region IDENTIFICA FACTURA / BOLETA
                        if (dt.Rows[i][0].ToString().Trim() == "Factura / Boleta")
                        {
                            LblFacturaBoleta.Visible = true;
                            TxtFacturaBoleta.Visible = true;
                        }
                        #endregion IDENTIFICA FACTURA / BOLETA
                        #region IDENTIFICA CLIENTE MINORISTA
                        if (dt.Rows[i][0].ToString().Trim() == "Cliente Minorista")
                        {
                            LblNombreClienteMinorista.Visible = true;
                            TxtNombreClienteMinorista.Visible = true;
                        }
                        #endregion IDENTIFICA CLIENTE MINORISTA
                        #region IDENTIFICA DNI
                        if (dt.Rows[i][0].ToString().Trim() == "Le / Dni")
                        {
                            LblDNI.Visible = true;
                            TxtDNI.Visible = true;
                        }
                        #endregion IDENTIFICA DNI
                        #region IDENTIFICA RUC
                        if (dt.Rows[i][0].ToString().Trim() == "Ruc")
                        {
                            LblCategoria.Visible = true;
                            CmbCategoria.Visible = true;

                        }
                        #endregion IDENTIFICA RUC
                        #region IDENTIFICA TELEFONO
                        if (dt.Rows[i][0].ToString().Trim() == "Telefono")
                        {
                            LblCategoria.Visible = true;
                            CmbCategoria.Visible = true;

                        }
                        #endregion IDENTIFICA TELEFONO
                        #region IDENTIFICA MONTO EN SOLES
                        if (dt.Rows[i][0].ToString().Trim() == "Monto Soles")
                        {
                            LblMontoSoles.Visible = true;
                            TxtMontoSoles.Visible = true;

                        }
                        #endregion IDENTIFICA MONTO EN SOLES
                        #endregion IDENTIFICA: FILTROS DETALLES
                    }
                }
                dt = null;
            }
        }
        #endregion IDENTIFICA FILTROS POR FORMATO
        
        #region VALIDA INFORMACION EN FILTROS PRINCIPAL
        private Boolean ValidainfoFiltros(bool continuar)
        {
            #region VALIDA FECHA INI
            if (TxtFecha.Visible == true)
            {
                if (TxtFecha.Text == "")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario ingresar la fecha ";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA FECHA INI
            #region VALIDA FECHA FIN
            if (TxtFechaFin.Visible == true)
            {
                if (TxtFechaFin.Text == "")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario ingresar la fecha final ";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA FECHA FIN
            #region VALIDA MERCADERISTA
            if (CmbOperativo.Visible == true)
            {
                if (CmbOperativo.Text == "0")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario seleccionar Mercaderista / Promotor";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA MERCADERISTA
            #region VALIDA ZONA MAYORISTA
            if (CmbZonaMayor.Visible == true)
            {
                if (CmbZonaMayor.Text == "0")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario seleccionar la Zona Mayorista";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA ZONA MAYORISTA
            #region VALIDA MERCADO
            if (CmbMercado.Visible == true)
            {
                if (CmbMercado.Text == "0")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario seleccionar el Mercado";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA MERCADO
            #region VALIDA CLIENTE - PUNTO DE VENTA
            if (TxtCliente.Visible == true)
            {
                if (LblCliente.Text == "")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario seleccionar punto de venta";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA CLIENTE - PUNTO DE VENTA
            #region VALIDA CIUDAD
            if (CmbCiudad.Visible == true)
            {
                if (CmbCiudad.Text == "0")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario seleccionar la Ciudad";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA CIUDAD
            #region VALIDA DIRECCION
            if (TxtLugarDireccion.Visible == true)
            {
                if (TxtLugarDireccion.Text.Trim() == "")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario ingresar Dirección";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA DIRECCION
            #region VALIDA NOMBRE DEL CLIENTE MAYORISTA
            if (TxtNombreClieMayorista.Visible == true)
            {
                if (TxtNombreClieMayorista.Text.Trim() == "")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario ingresar el Nombre del Cliente Mayorista";
                    Mensajes_Usuario();
                }
            }
            #endregion
            return continuar;
        }
        #endregion VALIDA INFORMACION EN FILTROS PRINCIPAL
        #region VALIDA INFORMACION EN FILTROS DETALLE
        private Boolean ValidaInfoFiltrosDetalles(bool continuar)
        {
            #region VALIDA FACTURA BOLETA
            if (TxtFacturaBoleta.Visible == true)
            {
                if (TxtFacturaBoleta.Text == "")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario ingresar el número de la Factura o Boleta ";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA FACTURA BOLETA
            #region VALIDA NOMBRE CLIENTE MAYORISTA
            if (TxtNombreClienteMinorista.Visible == true)
            {
                if (TxtNombreClienteMinorista.Text == "")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario ingresar el Nombre del Cliente Minorista ";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA NOMBRE CLIENTE MAYORISTA
            #region VALIDA DNI
            if (TxtDNI.Visible == true)
            {
                if (TxtDNI.Text == "")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario ingresar el número de la L.E. o D.N.I. ";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA DNI
            #region VALIDA RUC
            if (TxtRuc.Visible == true)
            {
                if (TxtRuc.Text == "")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario ingresar el numero de RUC ";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA RUC
            #region VALIDA TELEFONO
            if (TxtTelefono.Visible == true)
            {
                if (TxtTelefono.Text == "")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario ingresar la Telefono ";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA TELEFONO
            #region VALIDA MONTO EN SOLES
            if (TxtMontoSoles.Visible == true)
            {
                if (TxtMontoSoles.Text == "")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario ingresar el Monto ";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA FACTURA BOLETA
            #region VALIDA ID_CLIENTE DETALLE
            if (TxtClienteDetalle.Visible == true)
            {
                if (TxtClienteDetalle.Text == "")
                {
                    continuar = false;
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario seleccionar el Punto de Venta  ";
                    Mensajes_Usuario();
                }
            }
            #endregion VALIDA ID_CLIENTE DETALLE
            return continuar;
        }
        #endregion VALIDA INFORMACION EN FILTROS DETALLE

        #region LIMPIAR FILTROS PRINCIPAL Y FILTRO DETALLES (METODOS SEPARADOS)
        #region LIMPIAR FILTRO PRINCIPAL
        /// <summary>
        /// 1.-Limpia Todos los Datos que se encuentran en FiltroPrincipal
        /// </summary>
        private void LimpiarFiltros()
        {
            #region LIMPIAR FECHA
            TxtFecha.Text = "";
            TxtFechaFin.Text = "";
            #endregion LIMPIAR FECHA
            #region LIMPIAR MERCADERISTA
            try
            {
                CmbOperativo.SelectedIndex = 0;
            }
            catch
            { // no afecta el desempeño de la aplicación . es solo para controlar cuando aún no este lleno el objeto
            }
            #endregion LIMPIAR MERCADERISTA
            #region LIMPIAR CLIENTE
            TxtCliente.Text = "";
            LblCliente.Text = "";
            #endregion LIMPIAR CLIENTE
            #region LIMPIAR CMB CIUDAD
            try
            {
                CmbCiudad.SelectedIndex = 0;
            }
            catch
            { // no afecta el desepeño de la aplicación . es solo para controlar cuando aún no este lleno el objeto
            }
            #endregion LIMPIAR CMB CIUDAD
            #region LIMPIAR CMB ZONA MAYORISTA
            try
            {
                CmbZonaMayor.SelectedIndex = 0;
            }
            catch
            { // no afecta el desepeño de la aplicación . es solo para controlar cuando aún no este lleno el objeto
            }
            #endregion LIMPIAR CMB ZONA MAYORISTA
            #region LIMPIAR CMB MERCADO
            try
            {
                CmbMercado.SelectedIndex = 0;
            }
            catch
            { // no afecta el desepeño de la aplicación . es solo para controlar cuando aún no este lleno el objeto
            }
            #endregion LIMPIAR CMB MERCADO
            #region LIMPIAR CMB CATEGORIA
            try
            {
                CmbCategoria.SelectedIndex = 0;
            }
            catch
            { // no afecta el desepeño de la aplicación . es solo para controlar cuando aún no este lleno el objeto
            }
            #endregion LIMPIAR CMB CATEGORIA
            #region LIMPIAR LUGAR IMPULSO
            TxtLugarDireccion.Text = "";
            #endregion LIMPIAR LUGAR IMPULSO
            #region LIMPIAR PUNTO DE VENTA - PDV
            TxtPDV.Text = "";
            #endregion LIMPIAR PUNTO DE VENTA - PDV
        }
        #endregion LIMPIAR FILTRO PRINCIPAL

        #region LIMPIAR FILTRO DETALLES
        /// <summary>
        /// Limpia Todos los Datos que se encuentran en FiltroDetalle
        /// </summary>
        private void LimpiarFiltrosDetalles()
        {
            #region LIMPIAR FACTURA BOLETA
            TxtFacturaBoleta.Text = "";
            #endregion LIMPIAR FACTURA BOLETA
            #region LIMPIAR CLIENTE MINORISTA
            TxtNombreClienteMinorista.Text = "";
            #endregion  LIMPIAR CLIENTE MINORISTA
            #region LIMPIAR  DNI
            TxtDNI.Text = "";
            #endregion LIMPIAR  DNI
            #region LIMPIAR RUC
            TxtRuc.Text = "";
            #endregion LIMPIAR RUC
            #region LIMPIAR TELEFONO
            TxtTelefono.Text = "";
            #endregion LIMPIAR TELEFONO
            #region LIMPIAR MONTO SOLES
            TxtMontoSoles.Text = "";
            #endregion LIMPIAR MONTO SOLES
            #region LIMPIAR CLIENTE DETALLE - PUNTO DE VENTA
            TxtClienteDetalle.Text = "";
            LblClienteDetalle.Text = "";
            #endregion LIMPIAR CLIENTE DETALLE - PUNTO DE VENTA
        }
        #endregion LIMPIAR FILTRO DETALLES
        #endregion LIMPIAR FILTROS PRINCIPAL Y FILTRO DETALLES (METODOS SEPARADOS)
        #region ACTIVAR COMBOS SUPERCABECERA, BOTON: GUARDAR Y CANCELAR(SUPERCABECERA)
        /// <summary>
        /// 1.-CmbCampaña.Visible = true.
        /// 2.-CmbFormato.Visible = true.
        /// 3.-BtnSuperCabecera:Guardar.Visible=true;
        /// 4.-BtnSuperCabecera:Cancelar.Visible=true;
        /// 5.-FiltrosPrincipal.Visible=false;
        /// 6.-FiltrosDetalle.Visible=false;
        /// </summary>
        private void crearActivarbotones()
        {
            #region CMB SUPERCABECERA
            Lbl_Campaña.Visible = true;
            CmbCampaña.Visible = true;
            Lbl_Formato.Visible = true;
            CmbFormato.Visible = true;
            #endregion CMB SUPERCABECERA

            #region BOTONES SUPERCABECERA - VISIBLE: FALSE/TRUE
            ImgNewDigitacion.Visible = false;
            ImgSearchDigitacion.Visible = false; //CONSULTAR
            ImageButton1.Visible = true; //GUARDAR
            ImgCancelDigitacion.Visible = true;
            #endregion BOTONES SUPERCABECERA
            #region BOTONES FILTRO PRINCIPAL
            ImgEditDigitacion.Visible = false;
            ImgHabEditDigitacion.Visible = false;
            ImgUpdateDigitacion.Visible = false;
            ImgCancelEditDigitacion.Visible = false;
            ImgGuardarCabeceraOpeDigitacion.Visible = false;
            #endregion BOTONES FILTRO PRINCIPAL
            #region BOTONES FILTRO DETALLE
            ImgGuardarDetalle.Visible = false;
            ImgCancelarDetalle.Visible = false;
            #endregion BOTONES FILTRO DETALLE

        }
        #endregion ACTIVAR COMBOS SUPERCABECERA, BOTON: GUARDAR Y CANCELAR(SUPERCABECERA)


        #region LLENAR CBM PLANNING - INICIO
        /// <summary>
        /// LLENA COMBO PLANNING
        /// </summary>
        private void LlenaPresupuestosAsignados()
        {
            try
            {
                DataTable dt = new DataTable();
                Company_id = Convert.ToInt32(this.Session["companyid"].ToString().Trim());
                dt = Presupuesto.Presupuesto_Search(Company_id);

                if (dt != null)
                {
                    if (dt.Rows.Count > 1)
                    {
                        CmbCampaña.DataSource = dt;
                        CmbCampaña.DataValueField = "Numero_Presupuesto";
                        CmbCampaña.DataTextField = "Nombre";
                        CmbCampaña.DataBind();
                    }
                    else
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "Usted pertenece a una compañia que no tiene campañas creadas. Por favor consulte con el Administrador Xplora";
                        Mensajes_Usuario();
                    }
                }
                dt = null;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        #endregion LLENAR CBM PLANNING - FIN
        #region LLENAR CBM FORMATOS - INICIO
        /// <summary>
        /// Muestra los formatos disponibles 
        /// </summary>
        private void LlenaFormatos()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_FORMATOS");

                if (dt != null)
                {
                    if (dt.Rows.Count > 1)
                    {
                        CmbFormato.DataSource = dt;
                        CmbFormato.DataValueField = "Cod_Formato";
                        CmbFormato.DataTextField = "Formato";
                        CmbFormato.DataBind();
                    }
                    else
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No se ha parametrizado ningun formato. Consulte con el Administrador";
                        Mensajes_Usuario();
                    }
                }
                dt = null;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        #endregion LLENAR CBM FORMATOS - FIN
        #region LLENAR CMB CATEGORIAS POR FORMATO - INICIO
        
       
        private void llenarCategorias()
        {
            try
            {
                Company_id = Convert.ToInt32(this.Session["companyid"].ToString().Trim());
                DataTable dtCategory = null;
                dtCategory = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_OBTENERCATEGORIAS_PARAPLA", Company_id);
                if (dtCategory.Rows.Count > 0)
                {
                    
                    CmbCategoria.DataSource = dtCategory;
                    //CmbCategoria.Items.Insert(0, new ListItem("Seleccione", "0"));
                    CmbCategoria.DataValueField = "id_ProductCategory";
                    CmbCategoria.DataTextField = "Product_Category";
                    CmbCategoria.DataBind();
                    
                }
                dtCategory = null;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenarCategorias_FORMATO_CANJE_ANUA()
        {
            try
            {
                Company_id = Convert.ToInt32(this.Session["companyid"].ToString().Trim());
                DataTable dtCategory = null;
                dtCategory = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_PLANNING_OBTENERCATEGORIAS_PARAPLA", Company_id,5,13);
                if (dtCategory.Rows.Count > 0)
                {

                    CmbCategoria.DataSource = dtCategory;
                    //CmbCategoria.Items.Insert(0, new ListItem("Seleccione", "0"));
                    CmbCategoria.DataValueField = "id_ProductCategory";
                    CmbCategoria.DataTextField = "Product_Category";
                    CmbCategoria.DataBind();

                }
                dtCategory = null;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenarCategorias_ALIMENTO_PARA_MASCOTAS()
        {
            try
            {
                Company_id = Convert.ToInt32(this.Session["companyid"].ToString().Trim());
                DataTable dtCategory = null;
                dtCategory = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_PLANNING_OBTENERCATEGORIAS_PARAPLA", Company_id, 29, 40);
                if (dtCategory.Rows.Count > 0)
                {

                    CmbCategoria.DataSource = dtCategory;
                    //CmbCategoria.Items.Insert(0, new ListItem("Seleccione", "0"));
                    CmbCategoria.DataValueField = "id_ProductCategory";
                    CmbCategoria.DataTextField = "Product_Category";
                    CmbCategoria.DataBind();

                }
                dtCategory = null;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        #endregion LLENAR CMB CATEGORIAS POR FORMATO - FIN
        #region LLENAR CMB CIUDADES POR COMPAÑIA Y REPORTE
        /// <summary>
        /// llena las oficinas disponibles para un cliente 
        /// Pendiente confirmar si se debe pasar como parámetro , el código del reportes 
        /// </summary>
        private void llenarCiudades(int iReport_Id)
        {
            try
            {
                Company_id = Convert.ToInt32(this.Session["companyid"].ToString().Trim());
                DataTable dtcity = null;
                dtcity = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENERCITY", Company_id, iReport_Id);
                if (dtcity.Rows.Count > 0)
                {
                    CmbCiudad.DataSource = dtcity;
                    CmbCiudad.DataValueField = "cod_Oficina";
                    CmbCiudad.DataTextField = "Name_Oficina";
                    CmbCiudad.DataBind();
                }
                dtcity = null;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        #endregion LLENAR CMB CIUDADES POR COMPAÑIA Y REPORTE
        #region LLENAR CMB MERCADERISTA POR PLANNING
        private void llenaStaffplanning(string splanning)
        {
            try
            {
                DataSet dsStaffPlanning = wsPlanning.Get_Staff_Planning(splanning);
                if (dsStaffPlanning != null)
                {
                    if (dsStaffPlanning.Tables[1].Rows.Count > 0)
                    {
                        CmbOperativo.DataSource = dsStaffPlanning.Tables[1];
                        CmbOperativo.DataTextField = "name_user";
                        CmbOperativo.DataValueField = "Person_id";
                        CmbOperativo.DataBind();
                    }
                }
                dsStaffPlanning = null;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        #endregion LLENAR CMB MERCADERISTA POR PLANNING
        #region LLENAR CMB NODO COMERCIAL - CADENAS, MERCADOS
        #region LLENAR CMB CADENAS - NODO COMERCIAL
        private void combocadenas()
        {
            DataSet ds = new DataSet();
            ds = oCoon.ejecutarDataSet("UP_WEB_COMBONODOS", "25");
            CmbZonaMayor.DataSource = ds;
            CmbZonaMayor.DataTextField = "commercialNodeName";
            CmbZonaMayor.DataValueField = "NodeCommercial";
            CmbZonaMayor.DataBind();
            ds = null;
        }
        #endregion LLENAR CMB CADENAS - NODO COMERCIAL
        #region LLENAR CMB MERCADOS - NODO COMERCIAL
        private void comboMercados()
        {
            DataSet ds = new DataSet();
            ds = oCoon.ejecutarDataSet("UP_WEB_COMBONODOS", "26");
            CmbMercado.DataSource = ds;
            CmbMercado.DataTextField = "commercialNodeName";
            CmbMercado.DataValueField = "NodeCommercial";
            CmbMercado.DataBind();
            ds = null;
        }
        #endregion LLENAR CMB MERCADOS - NODO COMERCIAL
        #endregion LLENAR CMB NODO COMERCIAL - CADENAS, MERCADOS
        #region LLENAR CMB PLANNING
        protected void CmbCampaña_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaFormatos();
            InicializarFiltros();
            LimpiarFiltros();
        }
        #endregion LLENAR CMB PLANNING

        #region LLENAR PUNTO DE VENTA - PDV - CLIENTE
        /// <summary>
        /// Visualizar el nombre del punto de venta seleccionado de acuerdo a un codigo de pdv.
        /// </summary>
        /// <param name="scodpdv"></param>
        private void llenaPDV(string scodpdv)
        {
            try
            {
                DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_PDVCODEXCLIENTEYCANAL", this.Session["company_id"].ToString().Trim(), this.Session["CodChannel"].ToString().Trim(), scodpdv);
                if (dt.Rows.Count > 0)
                {
                    LblCliente.Text = dt.Rows[0]["pdv_Name"].ToString().Trim();
                }
                else
                {
                    LblCliente.Text = "";
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Codigo de cliente inexistente para este cliente y canal";
                    Mensajes_Usuario();
                }
                dt = null;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        #endregion LLENAR PUNTO DE VENTA - PDV

        #region LLENAR PUNTO DE VENTA DETALLE - PDV - CLIENTE
        private void llenaPDV_Detalle(string scodpdv)
        {
            try
            {
                DataTable dt1 = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_PDVCODEXCLIENTEYCANAL", this.Session["company_id"].ToString().Trim(), this.Session["CodChannel"].ToString().Trim(), scodpdv);
                if (dt1.Rows.Count > 0)
                {
                    LblClienteDetalle.Text = dt1.Rows[0]["pdv_Name"].ToString().Trim();
                }
                else
                {
                    LblClienteDetalle.Text = "";
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Codigo de cliente inexistente para este cliente y canal";
                    Mensajes_Usuario();
                }
                dt1 = null;
            }
            catch (Exception ex)
            {
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        #endregion LLENAR PUNTO DE VENTA DETALLE - PDV - CLIENTE

        #region EVENTO: SELECT INDEX: CMB CATEGORIA
        /// <summary>
        /// Muestra todos los productos que pertenecen a una categoria, un  planning y una compañia
        /// Pablo Salas
        /// 02-07-2011
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //MOSTRAR EN LA GV EL LISTADO DE PRODUCTOS FILTRADO POR CATEGORIA PARA INSERTAR LAS CANTIDADES EN DETALLE
                #region FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA - SELECCIONAR CATEGORIA
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
                {

                    DataSet dsHojaControlCanjeAbarrotesMulticategoria = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIGITACION_FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA", CmbCategoria.SelectedValue, TxtCodPlanning.Text,Convert.ToInt32(CmbFormato.SelectedValue));
                    if (this.Session["Opcion"].ToString().Trim() == "New")
                    {

                        if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[0].Rows.Count > 0)
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupConfirm";
                            this.Session["mensaje"] = "Correcto, Si hay informacion para digitar";
                            Mensajes_Usuario();

                            GvDigitacionHojaControlAbarrotes.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[0];
                            GvDigitacionHojaControlAbarrotes.DataBind();
                            GvDigitacionHojaControlAbarrotes.Visible = false;
                            ImgGuardarDetalle.Visible = true;
                            ImgCancelarDetalle.Visible = true;
                            FiltrosDetalle.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = true;


                            //GvDigitacionHojaControlAbarrotes.Columns[0].Visible = false;
                            //GvDigitacionHojaControlAbarrotes.Columns[1].Visible = false;
                            //GvDigitacionHojaControlAbarrotes.Columns[2].Visible = false;
                            //GvDigitacionHojaControlAbarrotes.Columns[3].Visible = false;
                            //GvDigitacionHojaControlAbarrotes.Columns[4].Visible = false;
                            //GvDigitacionHojaControlAbarrotes.Columns[5].Visible = false;
                        }
                        else
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No hay información para digitar";
                            Mensajes_Usuario();
                            GvDigitacionHojaControlAbarrotes.Visible = false;
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            //ImgGuardarDetalle
                        }


                    }
                    //else
                    //{
                    //    if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[0].Rows.Count > 0)
                    //    {

                    //        GvDigitacionHojaControlAbarrotes.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[0];
                    //        GvDigitacionHojaControlAbarrotes.DataBind();
                    //        GvDigitacionHojaControlAbarrotes.Visible = true;
                    //        ImgGuardarDetalle.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        this.Session["encabemensa"] = "Sr. Usuario";
                    //        this.Session["cssclass"] = "MensajesSupervisor";
                    //        this.Session["mensaje"] = "No hay información para digitar";
                    //        Mensajes_Usuario();
                    //        GvDigitacionHojaControlAbarrotes.Visible = false;
                    //        ImgGuardarDetalle.Visible = false;
                    //        //ImgGuardarDetalle
                    //    }
                    //}


                    if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[1].Rows.Count > 0)
                    {
                        GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[1];
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();
                        //GvDigitacionHojaControlAbarrotes_Premios.Visible = true;

                    }
                    else
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No hay información para digitar";
                        Mensajes_Usuario();
                        GvDigitacionHojaControlAbarrotes_Premios.Visible = false;

                        //ImgGuardarDetalle.Visible = false;
                        //ImgGuardarDetalle
                    }

                    GvDigitacionHojaControlAbarrotes_Premios.Visible = false;
                    dsHojaControlCanjeAbarrotesMulticategoria = null;
                }
                #endregion

                #region FORMATO_CANJE_ANUA
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
                {
                    DataSet dsHojaControlCanjeAbarrotesMulticategoria = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIGITACION_FORMATO_CANJE_ANUA", CmbCategoria.SelectedValue, TxtCodPlanning.Text, 13);
                    //UP_WEBXPLORA_OPE_DIGITACION_PLANNING_OBTENERCATEGORIAS_PARAPLA
                    //MODIFICAR PARA FORMATO CANJE ANUA


                }
                #endregion
            }
            catch (Exception ex) { }
        }
        #endregion EVENTO: SELECT INDEX: CMB CATEGORIA
        #region EVENTO: SELECT INDEX: CMB FORMATO
        /// <summary>
        /// FILTRA FORMATOS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CmbFormato_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LimpiarFiltros();
                IdentificarFiltros(Convert.ToInt32(CmbFormato.SelectedValue));
                if (CmbCampaña.SelectedValue != "0" && CmbFormato.SelectedValue != "0")
                {
                    DataTable dt = wsPlanning.ObtenerIdPlanning(CmbCampaña.SelectedValue);
                    TxtCodPlanning.Text = dt.Rows[0]["Planning"].ToString().Trim();
                    this.Session["CodChannel"] = dt.Rows[0]["Planning_CodChannel"].ToString().Trim();
                    this.Session["company_id"] = dt.Rows[0]["Company_id"].ToString().Trim();
                    llenaStaffplanning(dt.Rows[0]["Planning"].ToString().Trim());

                    if (this.Session["Opcion"].ToString().Trim() == "New")
                    {
                        DataTable dtColumnasDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_COLUMNAS_A_MOSTRAR", Convert.ToInt32(CmbFormato.SelectedValue));

                        #region FORMATO FINAL SOD
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINAL_SOD"])
                        {
                            llenarCiudades(21);
                            DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_FORMATOFINALSOD", dt.Rows[0]["Planning"].ToString().Trim());
                            if (dtDigitacion != null)
                            {
                                if (dtDigitacion.Rows.Count > 0)
                                {
                                    GvDigitacion.Enabled = true;
                                    GvDigitacion.DataSource = dtDigitacion;
                                    GvDigitacion.DataBind();
                                    if (dtColumnasDigitacion != null)
                                    {
                                        if (dtColumnasDigitacion.Rows.Count > 0)
                                        {
                                            GvDigitacion.Columns[0].Visible = false;
                                            GvDigitacion.Columns[5].Visible = false;

                                            if (dtColumnasDigitacion.Rows[0]["Exhib_Primaria"].ToString().Trim() == "False")
                                            {
                                                GvDigitacion.Columns[3].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacion.Columns[3].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Exhib_Secundaria"].ToString().Trim() == "False")
                                            {
                                                GvDigitacion.Columns[4].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacion.Columns[4].Visible = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    InicializarFiltros();
                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "No hay información para digitar";
                                    Mensajes_Usuario();
                                }
                            }
                            dt = null;
                            dtDigitacion = null;
                        }
                        #endregion
                        #region FORMATO FINALES PRECIOS
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_PRECIOS"])
                        {
                            combocadenas();
                            DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_FORMATOFINALESPRECIOS", dt.Rows[0]["Planning"].ToString().Trim());
                            if (dtDigitacion != null)
                            {
                                if (dtDigitacion.Rows.Count > 0)
                                {
                                    GVDigitacionPrecios.Enabled = true;
                                    GVDigitacionPrecios.DataSource = dtDigitacion;
                                    GVDigitacionPrecios.DataBind();
                                    if (dtColumnasDigitacion != null)
                                    {
                                        if (dtColumnasDigitacion.Rows.Count > 0)
                                        {
                                            GVDigitacionPrecios.Columns[0].Visible = false;
                                            GVDigitacionPrecios.Columns[8].Visible = false;

                                            if (dtColumnasDigitacion.Rows[0]["Precio_Costo"].ToString().Trim() == "False")
                                            {
                                                GVDigitacionPrecios.Columns[4].Visible = false;
                                            }
                                            else
                                            {
                                                GVDigitacionPrecios.Columns[4].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Precio_Reventa"].ToString().Trim() == "False")
                                            {
                                                GVDigitacionPrecios.Columns[5].Visible = false;
                                            }
                                            else
                                            {
                                                GVDigitacionPrecios.Columns[5].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Precio_Reventa_Unid"].ToString().Trim() == "False")
                                            {
                                                GVDigitacionPrecios.Columns[6].Visible = false;
                                            }
                                            else
                                            {
                                                GVDigitacionPrecios.Columns[6].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Observaciones"].ToString().Trim() == "False")
                                            {
                                                GVDigitacionPrecios.Columns[7].Visible = false;
                                            }
                                            else
                                            {
                                                GVDigitacionPrecios.Columns[7].Visible = true;
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    InicializarFiltros();
                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "No hay información para digitar";
                                    Mensajes_Usuario();
                                }
                            }
                            dt = null;
                            dtDigitacion = null;
                        }
                        #endregion
                        #region FORMATOS FINALES DG
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_DG"])
                        {
                            combocadenas();
                            DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_FORMATOSFINALESDG", dt.Rows[0]["Planning"].ToString().Trim());
                            if (dtDigitacion != null)
                            {
                                if (dtDigitacion.Rows.Count > 0)
                                {
                                    GvDigitacionDG.Enabled = true;
                                    GvDigitacionDG.DataSource = dtDigitacion;
                                    GvDigitacionDG.DataBind();
                                    if (dtColumnasDigitacion != null)
                                    {
                                        if (dtColumnasDigitacion.Rows.Count > 0)
                                        {
                                            GvDigitacionDG.Columns[0].Visible = false;
                                            GvDigitacionDG.Columns[9].Visible = false;

                                            if (dtColumnasDigitacion.Rows[0]["Local_1"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionDG.Columns[3].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionDG.Columns[3].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Local_2"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionDG.Columns[4].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionDG.Columns[4].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Local_3"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionDG.Columns[5].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionDG.Columns[5].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Local_4"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionDG.Columns[6].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionDG.Columns[6].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Local_5"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionDG.Columns[7].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionDG.Columns[7].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Total"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionDG.Columns[8].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionDG.Columns[8].Visible = true;
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    InicializarFiltros();
                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "No hay información para digitar";
                                    Mensajes_Usuario();
                                }
                            }
                            dt = null;
                            dtDigitacion = null;
                        }
                        #endregion
                        #region FORMATOS AG COCINA
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_AGCOCINA"])
                        {
                            combocadenas();
                            DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_FORMATOAGCOCINA", dt.Rows[0]["Planning"].ToString().Trim());
                            if (dtDigitacion != null)
                            {
                                if (dtDigitacion.Rows.Count > 0)
                                {

                                    GvDigitacionAGCocina.Enabled = true;
                                    GvDigitacionAGCocina.DataSource = dtDigitacion;
                                    GvDigitacionAGCocina.DataBind();
                                    if (dtColumnasDigitacion != null)
                                    {
                                        if (dtColumnasDigitacion.Rows.Count > 0)
                                        {
                                            GvDigitacionAGCocina.Columns[0].Visible = false;
                                            GvDigitacionAGCocina.Columns[9].Visible = false;

                                            if (dtColumnasDigitacion.Rows[0]["Precio"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionAGCocina.Columns[3].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionAGCocina.Columns[3].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Stock_inicial"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionAGCocina.Columns[4].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionAGCocina.Columns[4].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ingresos"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionAGCocina.Columns[5].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionAGCocina.Columns[5].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Total_Stock"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionAGCocina.Columns[6].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionAGCocina.Columns[6].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Stock_Final"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionAGCocina.Columns[7].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionAGCocina.Columns[7].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ventas"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionAGCocina.Columns[8].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionAGCocina.Columns[8].Visible = true;
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    InicializarFiltros();
                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "No hay información para digitar";
                                    Mensajes_Usuario();
                                }
                            }
                            dt = null;
                            dtDigitacion = null;
                        }
                        #endregion
                        #region FORMATOS MIMASKOT
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_MIMASKOT"])
                        {
                            DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_FORMATOMIMASKOT", dt.Rows[0]["Planning"].ToString().Trim());
                            if (dtDigitacion != null)
                            {
                                if (dtDigitacion.Rows.Count > 0)
                                {

                                    GvDigitacionMimaskot.Enabled = true;
                                    GvDigitacionMimaskot.DataSource = dtDigitacion;
                                    GvDigitacionMimaskot.DataBind();
                                    if (dtColumnasDigitacion != null)
                                    {
                                        if (dtColumnasDigitacion.Rows.Count > 0)
                                        {
                                            GvDigitacionMimaskot.Columns[0].Visible = false;
                                            GvDigitacionMimaskot.Columns[11].Visible = false;

                                            if (dtColumnasDigitacion.Rows[0]["Stock_inicial"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[3].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[3].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ingresos"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[4].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[4].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Total_Stock"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[5].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[5].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Stock_Final"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[6].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[6].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ventas"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[7].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[7].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Precio_Reventa"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[8].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[8].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Mejor_Precio_Reventa"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[9].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[9].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Precio_Costo"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[10].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[10].Visible = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    InicializarFiltros();
                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "No hay información para digitar";
                                    Mensajes_Usuario();
                                }
                            }
                            dt = null;
                            dtDigitacion = null;
                        }
                        #endregion
                        #region FORMATO PRECIOS CUIDADO DEL CABELLO
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PRECIOS_CUIDADOCABELLO"])
                        {
                            combocadenas();
                            DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_FORMATOFINALESPRECIOS", dt.Rows[0]["Planning"].ToString().Trim());
                            if (dtDigitacion != null)
                            {
                                if (dtDigitacion.Rows.Count > 0)
                                {
                                    GVDigitacionPrecios.Enabled = true;
                                    GVDigitacionPrecios.DataSource = dtDigitacion;
                                    GVDigitacionPrecios.DataBind();
                                    if (dtColumnasDigitacion != null)
                                    {
                                        if (dtColumnasDigitacion.Rows.Count > 0)
                                        {
                                            GVDigitacionPrecios.Columns[0].Visible = false;
                                            GVDigitacionPrecios.Columns[8].Visible = false;

                                            if (dtColumnasDigitacion.Rows[0]["Precio_Costo"].ToString().Trim() == "False")
                                            {
                                                GVDigitacionPrecios.Columns[4].Visible = false;
                                            }
                                            else
                                            {
                                                GVDigitacionPrecios.Columns[4].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Precio_Reventa"].ToString().Trim() == "False")
                                            {
                                                GVDigitacionPrecios.Columns[5].Visible = false;
                                            }
                                            else
                                            {
                                                GVDigitacionPrecios.Columns[5].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Precio_Reventa_Unid"].ToString().Trim() == "False")
                                            {
                                                GVDigitacionPrecios.Columns[6].Visible = false;
                                            }
                                            else
                                            {
                                                GVDigitacionPrecios.Columns[6].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Observaciones"].ToString().Trim() == "False")
                                            {
                                                GVDigitacionPrecios.Columns[7].Visible = false;
                                            }
                                            else
                                            {
                                                GVDigitacionPrecios.Columns[7].Visible = true;
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    InicializarFiltros();
                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "No hay información para digitar";
                                    Mensajes_Usuario();
                                }
                            }
                            dt = null;
                            dtDigitacion = null;
                        }
                        #endregion
                        #region FORMATOS DIAS GIRO CUIDADO DEL CABELLO
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_DIASGIRO"])
                        {
                            combocadenas();
                            DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_FORMATOAGCOCINA", dt.Rows[0]["Planning"].ToString().Trim());
                            if (dtDigitacion != null)
                            {
                                if (dtDigitacion.Rows.Count > 0)
                                {

                                    GvDigitacionAGCocina.Enabled = true;
                                    GvDigitacionAGCocina.DataSource = dtDigitacion;
                                    GvDigitacionAGCocina.DataBind();
                                    if (dtColumnasDigitacion != null)
                                    {
                                        if (dtColumnasDigitacion.Rows.Count > 0)
                                        {
                                            GvDigitacionAGCocina.Columns[0].Visible = false;
                                            GvDigitacionAGCocina.Columns[9].Visible = false;

                                            if (dtColumnasDigitacion.Rows[0]["Precio"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionAGCocina.Columns[3].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionAGCocina.Columns[3].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Stock_inicial"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionAGCocina.Columns[4].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionAGCocina.Columns[4].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ingresos"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionAGCocina.Columns[5].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionAGCocina.Columns[5].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Total_Stock"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionAGCocina.Columns[6].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionAGCocina.Columns[6].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Stock_Final"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionAGCocina.Columns[7].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionAGCocina.Columns[7].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ventas"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionAGCocina.Columns[8].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionAGCocina.Columns[8].Visible = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    InicializarFiltros();
                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "No hay información para digitar";
                                    Mensajes_Usuario();
                                }
                            }
                            dt = null;
                            dtDigitacion = null;
                        }
                        #endregion
                        #region FORMATOS NUTRICAN
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_NUTRICAN"])
                        {
                            DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_FORMATONUTRICAN", dt.Rows[0]["Planning"].ToString().Trim());
                            if (dtDigitacion != null)
                            {
                                if (dtDigitacion.Rows.Count > 0)
                                {

                                    GvDigitacionMimaskot.Enabled = true;
                                    GvDigitacionMimaskot.DataSource = dtDigitacion;
                                    GvDigitacionMimaskot.DataBind();
                                    if (dtColumnasDigitacion != null)
                                    {
                                        if (dtColumnasDigitacion.Rows.Count > 0)
                                        {
                                            GvDigitacionMimaskot.Columns[0].Visible = false;
                                            GvDigitacionMimaskot.Columns[11].Visible = false;

                                            if (dtColumnasDigitacion.Rows[0]["Stock_inicial"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[3].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[3].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ingresos"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[4].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[4].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Total_Stock"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[5].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[5].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Stock_Final"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[6].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[6].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ventas"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[7].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[7].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Precio_Reventa"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[8].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[8].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Mejor_Precio_Reventa"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[9].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[9].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Precio_Costo"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[10].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[10].Visible = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    InicializarFiltros();
                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "No hay información para digitar";
                                    Mensajes_Usuario();
                                }
                            }
                            dt = null;
                            dtDigitacion = null;
                        }
                        #endregion
                        #region FORMATOS PROMOCION GALLETAS
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PROMOCION_GALLETAS"])
                        {
                            comboMercados();
                            llenarCiudades(21);
                            DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_FORMATOPROMGALLETAS", dt.Rows[0]["Planning"].ToString().Trim());
                            if (dtDigitacion != null)
                            {
                                if (dtDigitacion.Rows.Count > 0)
                                {
                                    GvDigitacionPromGalletas.Enabled = true;
                                    GvDigitacionPromGalletas.DataSource = dtDigitacion;
                                    GvDigitacionPromGalletas.DataBind();
                                    if (dtColumnasDigitacion != null)
                                    {
                                        if (dtColumnasDigitacion.Rows.Count > 0)
                                        {
                                            GvDigitacionPromGalletas.Columns[0].Visible = false;
                                            GvDigitacionPromGalletas.Columns[11].Visible = false;

                                            if (dtColumnasDigitacion.Rows[0]["Stock_inicial"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionPromGalletas.Columns[3].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionPromGalletas.Columns[3].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ingresos_Semana1"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionPromGalletas.Columns[4].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[4].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ingresos_Semana2"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionPromGalletas.Columns[5].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionPromGalletas.Columns[5].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Total_Ingresos"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionPromGalletas.Columns[6].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionPromGalletas.Columns[6].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Total_Stock"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionPromGalletas.Columns[7].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionPromGalletas.Columns[7].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Stock_Final"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionPromGalletas.Columns[8].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionPromGalletas.Columns[8].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ventas"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionPromGalletas.Columns[9].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionPromGalletas.Columns[9].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Precio"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionPromGalletas.Columns[10].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionPromGalletas.Columns[10].Visible = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    InicializarFiltros();
                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "No hay información para digitar";
                                    Mensajes_Usuario();
                                }
                            }
                            dt = null;
                            dtDigitacion = null;
                        }
                        #endregion
                        #region FORMATOS AB MASCOTAS - INFORME SEMANAL
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASINFSEM"])
                        {
                            DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_FORMATOMIMASKOT", dt.Rows[0]["Planning"].ToString().Trim());
                            if (dtDigitacion != null)
                            {
                                if (dtDigitacion.Rows.Count > 0)
                                {

                                    GvDigitacionMimaskot.Enabled = true;
                                    GvDigitacionMimaskot.DataSource = dtDigitacion;
                                    GvDigitacionMimaskot.DataBind();
                                    if (dtColumnasDigitacion != null)
                                    {
                                        if (dtColumnasDigitacion.Rows.Count > 0)
                                        {
                                            GvDigitacionMimaskot.Columns[0].Visible = false;
                                            GvDigitacionMimaskot.Columns[11].Visible = false;

                                            if (dtColumnasDigitacion.Rows[0]["Stock_inicial"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[3].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[3].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ingresos"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[4].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[4].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Total_Stock"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[5].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[5].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Stock_Final"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[6].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[6].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Ventas"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[7].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[7].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Precio_Reventa"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[8].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[8].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Mejor_Precio_Reventa"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[9].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[9].Visible = true;
                                            }
                                            if (dtColumnasDigitacion.Rows[0]["Precio_Costo"].ToString().Trim() == "False")
                                            {
                                                GvDigitacionMimaskot.Columns[10].Visible = false;
                                            }
                                            else
                                            {
                                                GvDigitacionMimaskot.Columns[10].Visible = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    InicializarFiltros();
                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";
                                    this.Session["mensaje"] = "No hay información para digitar";
                                    Mensajes_Usuario();
                                }
                            }
                            dt = null;
                            dtDigitacion = null;
                        }
                        #endregion
                        #region FORMATOS AB MASCOTAS - HOJA CONTROL
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASHOJACONTROL"])
                        {

                            combocadenas();
                            llenarCategorias_ALIMENTO_PARA_MASCOTAS();

                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = false;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = true;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE


                        }
                        #endregion
                        #region FORMATOS CANJE MULTICATEGORIAS - HOJA CONTROL
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
                        {

                            combocadenas();
                            llenarCategorias();

                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = false;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = true;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE
                            

                         }
                        #endregion
                        #region FORMATO CANJE_ANUA
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_CANJE_ANUA"])
                        {
                            combocadenas();
                            llenarCategorias_FORMATO_CANJE_ANUA();

                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = false;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = true;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE

                        }
                        #endregion FORMATO CANJE_ANUA
                        #region FORMATO RESUMEN_CANJE_ANUA
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_ANUA"])
                        {
                            combocadenas();
                            llenarCategorias_FORMATO_CANJE_ANUA();

                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = false;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = true;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE
                            
                        }
                        #endregion FORMATO RESUMEN_CANJE_ANUA
                        #region FORMATO HDC_CANJE
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE"])
                        {
                            combocadenas();
                            llenarCategorias_FORMATO_CANJE_ANUA();
                            #region CABECERA
                            CmbCampaña.Enabled = true;
                            CmbFormato.Enabled = true;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = true;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = false;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = true;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE

                        }
                        #endregion FORMATO HDC_CANJE
                        #region FORMATO FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar"])
                        {
                            combocadenas();
                            llenarCategorias_FORMATO_CANJE_ANUA();

                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = true;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = false;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = true;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE

                        }
                        #endregion FORMATO FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar
                        #region FORMATO RESUMEN_CANJE_PLUSBELLE
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_PLUSBELLE"])
                        {
                            combocadenas();
                            llenarCategorias_FORMATO_CANJE_ANUA();

                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = false;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = true;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE

                        }
                        #endregion FORMATO RESUMEN_CANJE_PLUSBELLE

                        #region FORMATO COMPETENCIA_ANUA
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_ANUA"])
                        {


                        }
                        #endregion FORMATO COMPETENCIA_ANUA
                        #region FORMATO COMPETENCIA_MAYORISTA
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_MAYORISTA"])
                        {
                      

                        }
                        #endregion FORMATO COMPETENCIA_MAYORISTA

                    }
                    if (this.Session["Opcion"].ToString().Trim() == "Search")
                    {
                        #region FORMATO FINAL SOD
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINAL_SOD"])
                        {
                            llenarCiudades(21);
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;//BtnGuardar
                            ImgCancelDigitacion.Visible = true;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                        }
                        #endregion
                        #region FORMATO FINALES PRECIOS
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_PRECIOS"])
                        {
                            combocadenas();


                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;//BtnGuardar
                            ImgCancelDigitacion.Visible = true;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR

                        }
                        #endregion
                        #region FORMATOS FINALES DG
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_DG"])
                        {
                            combocadenas();
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;//BtnGuardar
                            ImgCancelDigitacion.Visible = true;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                        }
                        #endregion
                        #region FORMATOS AG COCINA
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_AGCOCINA"])
                        {
                            combocadenas();
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;//BtnGuardar
                            ImgCancelDigitacion.Visible = true;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                        }
                        #endregion
                        #region FORMATOS MIMASKOT
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_MIMASKOT"])
                        {
                            // no necesita ingresar a ninguan función
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;//BtnGuardar
                            ImgCancelDigitacion.Visible = true;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                        }
                        #endregion
                        #region FORMATO PRECIOS CUIDADO DEL CABELLO
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PRECIOS_CUIDADOCABELLO"])
                        {
                            combocadenas();
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;//BtnGuardar
                            ImgCancelDigitacion.Visible = true;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                        }
                        #endregion
                        #region FORMATOS DIAS GIRO CUIDADO DEL CABELLO
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_DIASGIRO"])
                        {
                            combocadenas();
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;//BtnGuardar
                            ImgCancelDigitacion.Visible = true;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                        }
                        #endregion
                        #region FORMATOS NUTRICAN
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_NUTRICAN"])
                        {
                            // no necesita ingresar a ninguan función
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;//BtnGuardar
                            ImgCancelDigitacion.Visible = true;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                        }
                        #endregion
                        #region FORMATOS PROMOCION GALLETAS
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PROMOCION_GALLETAS"])
                        {
                            comboMercados();
                            llenarCiudades(21);
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;//BtnGuardar
                            ImgCancelDigitacion.Visible = true;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                        }
                        #endregion
                        #region FORMATOS AB MASCOTAS - INFORME SEMANAL
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASINFSEM"])
                        {
                            // no necesita ingresar a ninguan función
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;//BtnGuardar
                            ImgCancelDigitacion.Visible = true;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                        }
                        #endregion
                        #region FORMATOS AB MASCOTAS - HOJA DE CONTROL
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASHOJACONTROL"])
                        {
                            combocadenas();
                            llenarCategorias_ALIMENTO_PARA_MASCOTAS();

                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE
                            //// no necesita ingresar a ninguan función
                            //#region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            //ImgNewDigitacion.Visible = false;
                            //ImgSearchDigitacion.Visible = false;
                            //ImageButton1.Visible = false;//BtnGuardar
                            //ImgCancelDigitacion.Visible = true;
                            //#endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            //#region BOTONES CABECERA MOSTRAR/OCULTAR
                            //ImgEditDigitacion.Visible = true;//BtnConsultar
                            //ImgHabEditDigitacion.Visible = false;
                            //ImgUpdateDigitacion.Visible = false;
                            //ImgCancelEditDigitacion.Visible = false;
                            //ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            //#endregion BOTONES CABECERA MOSTRAR/OCULTAR
                        }
                        #endregion
                        #region FORMATOS CANJE MULTICATEGORIAS - HOJA CONTROL
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
                        {
                            combocadenas();
                            llenarCategorias();

                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR 
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR 
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR 
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE

                        }
                        #endregion
                        #region FORMATO CANJE_ANUA
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_CANJE_ANUA"])
                        {
                            combocadenas();
                            llenarCategorias_FORMATO_CANJE_ANUA();
                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE
                        }
                        #endregion FORMATO CANJE_ANUA
                        #region FORMATO RESUMEN_CANJE_ANUA
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_ANUA"])
                        {
                            combocadenas();
                            llenarCategorias_FORMATO_CANJE_ANUA();
                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE
                        }
                        #endregion FORMATO RESUMEN_CANJE_ANUA
                        #region FORMATO HDC_CANJE
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE"])
                        {
                            combocadenas();
                            llenarCategorias();
                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE

                        }
                        #endregion FORMATO HDC_CANJE
                        #region FORMATO FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar"])
                        {
                            combocadenas();
                            llenarCategorias();
                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE

                        }
                        #endregion FORMATO FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar
                        #region FORMATO RESUMEN_CANJE_PLUSBELLE
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_PLUSBELLE"])
                        {
                            combocadenas();
                            llenarCategorias_FORMATO_CANJE_ANUA();
                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE

                        }
                        #endregion FORMATO RESUMEN_CANJE_PLUSBELLE
                        #region FORMATO COMPETENCIA_ANUA
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_ANUA"])
                        {
                            combocadenas();
                            llenarCategorias_FORMATO_CANJE_ANUA();
                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE

                        }
                        #endregion FORMATO COMPETENCIA_ANUA
                        #region FORMATO COMPETENCIA_MAYORISTA
                        if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_MAYORISTA"])
                        {
                            combocadenas();
                            llenarCategorias_FORMATO_CANJE_ANUA();
                            #region CABECERA
                            CmbCampaña.Enabled = false;
                            CmbFormato.Enabled = false;
                            #endregion CABECERA
                            #region FILTROS
                            Filtros.Disabled = false;
                            FiltrosDetalle.Visible = false;
                            #endregion FILTROS
                            #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR
                            ImgNewDigitacion.Visible = false;
                            ImgSearchDigitacion.Visible = false;
                            ImageButton1.Visible = false;
                            ImgCancelDigitacion.Visible = false;
                            #endregion BOTONES SUPERCABECERA MOSTRAR/OCULTAR
                            #region BOTONES CABECERA MOSTRAR/OCULTAR
                            ImgEditDigitacion.Visible = true;//BtnConsultar
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            #endregion BOTONES CABECERA MOSTRAR/OCULTAR
                            #region BOTONES DETALLE :MOSTRAR/OCULTAR
                            ImgGuardarDetalle.Visible = false;
                            ImgCancelarDetalle.Visible = false;
                            #endregion BOTONES DETALLE

                        }
                        #endregion FORMATO COMPETENCIA_MAYORISTA
                    }
                    dt = null;
                }
                else
                {
                    InicializarFiltros();
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Es necesario seleccionar un presupuesto y un formato ";
                    Mensajes_Usuario();
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
        #endregion EVENTO: SELECT INDEX: CMB FORMATO

        #region EVENTO KEYPRESS - MOSTRAR EL CLIENTE - PUNTO DE VENTA
        protected void TxtCliente_TextChanged(object sender, EventArgs e)
        {
            TxtCliente.Focus();
            if (TxtCliente.Text != "")
            {
                llenaPDV(TxtCliente.Text);
            }
            else
            {
                LblCliente.Text = "";
            }
        }
        #endregion EVENTO KEYPRESS - MOSTRAR EL CLIENTE - PUNTO DE VENTA
        #region EVENTO CLICK: IMAGEN : MOSTRAR EL CLIENTE - PUNTO DE VENTA
        protected void ImgSelCliente_Click(object sender, ImageClickEventArgs e)
        {
            TxtCliente.Focus();
            if (TxtCliente.Text != "")
            {
                llenaPDV(TxtCliente.Text);
            }
            else
            {
                LblCliente.Text = "";
            }
        }
        #endregion EVENTO CLICK: IMAGEN : MOSTRAR EL CLIENTE - PUNTO DE VENTA


        #region EVENTO KEYPRESS - MOSTRAR EL CLIENTE DETALLE - PUNTO DE VENTA
        protected void TxtClienteDetalle_TextChanged(object sender, EventArgs e)
        {
            TxtClienteDetalle.Focus();
            if (TxtClienteDetalle.Text != "")
            {
                llenaPDV_Detalle(TxtClienteDetalle.Text);
            }
            else
            {
                LblClienteDetalle.Text = "";
            }
        }

        #endregion EVENTO KEYPRESS - MOSTRAR EL CLIENTE DETALLE - PUNTO DE VENTA
        #region EVENTO CLICK: IMAGEN : MOSTRAR EL CLIENTE DETALLE - PUNTO DE VENTA
        protected void ImgSelClienteDetalle_Click(object sender, ImageClickEventArgs e)
        {
            TxtCliente.Focus();
            if (TxtClienteDetalle.Text != "")
            {
                llenaPDV_Detalle(TxtClienteDetalle.Text);
            }
            else
            {
                LblClienteDetalle.Text = "";
            }
        }
        #endregion EVENTO CLICK: IMAGEN : MOSTRAR EL CLIENTE DETALLE - PUNTO DE VENTA

        #region EVENTOS CLICK: SUPERCABECERA:  BOTONES

        #region EVENTO CLICK: BOTON GUARDAR : SUPERCABECERA
        /// <summary>
        /// FUNCION GUARDAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            bool continuar = true;
            bool continuar1 = false;


            if (ValidainfoFiltros(continuar))
            {
                continuar1 = false;

                //guardar para : FORMATO_FINAL_SOD
                #region FORMATO_FINAL_SOD
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINAL_SOD"])
                {
                    if (GvDigitacion.Rows.Count > 0)
                    {
                        for (int i = 0; i <= GvDigitacion.Rows.Count - 1; i++)
                        {
                            if (GvDigitacion.Columns[3].Visible == true || GvDigitacion.Columns[4].Visible == true)
                            {
                                if (((TextBox)GvDigitacion.Rows[i].Cells[0].FindControl("TxtExhPrim")).Text != "" ||
                                    ((TextBox)GvDigitacion.Rows[i].Cells[0].FindControl("TxtExhSec")).Text != "")
                                {
                                    continuar1 = true;
                                    EOPE_DigFORMATOFINALSOD oeOPE_DigFORMATOFINALSOD = oOPE_DigFORMATOFINALSOD.RegistrarFormatoFinalSod(TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                                        Convert.ToInt32(CmbOperativo.SelectedValue), TxtCliente.Text, LblCliente.Text,
                                        Convert.ToInt64(CmbCiudad.SelectedValue),
                                        ((Label)GvDigitacion.Rows[i].Cells[0].FindControl("LblCateg")).Text,
                                        ((Label)GvDigitacion.Rows[i].Cells[0].FindControl("LblMarca")).Text,
                                        ((TextBox)GvDigitacion.Rows[i].Cells[0].FindControl("TxtExhPrim")).Text,
                                        ((TextBox)GvDigitacion.Rows[i].Cells[0].FindControl("TxtExhSec")).Text,
                                        Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                }
                            }
                        }
                    }

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }
                    if (continuar == true && continuar1 == true)
                    {
                        CmbCampaña.Text = "0";
                        CmbFormato.Text = "0";
                        InicializarFiltros();
                        cancelarActivarbotones();

                        LimpiarFiltros();

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                    }
                }
                #endregion

                //guardar para : FORMATO_FINALES_PRECIOS
                #region FORMATO FINALES PRECIOS
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_PRECIOS"])
                {
                    if (GVDigitacionPrecios.Rows.Count > 0)
                    {
                        for (int i = 0; i <= GVDigitacionPrecios.Rows.Count - 1; i++)
                        {
                            if (GVDigitacionPrecios.Columns[4].Visible == true || GVDigitacionPrecios.Columns[5].Visible == true
                                || GVDigitacionPrecios.Columns[6].Visible == true || GVDigitacionPrecios.Columns[7].Visible == true)
                            {
                                if (((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtCosto")).Text != "" ||
                                    ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventa")).Text != "" ||
                                    ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventaUnid")).Text != "" ||
                                    ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtObservaciones")).Text != "")
                                {
                                    continuar1 = true;
                                    EOPE_DigFORMATOFINALESPRECIOS oeOPE_DigFORMATOFINALESPRECIOS = oOPE_DigFORMATOFINALESPRECIOS.RegistrarFormatoFinalesPrecios(TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                                        Convert.ToInt32(CmbOperativo.SelectedValue), Convert.ToInt32(CmbZonaMayor.SelectedValue), TxtCliente.Text, LblCliente.Text,
                                        ((Label)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("LblSubCatg")).Text,
                                       ((Label)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("LblSku")).Text,
                                       ((Label)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("LblProd")).Text,
                                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtCosto")).Text,
                                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventa")).Text,
                                         ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtObservaciones")).Text,
                                         ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventaUnid")).Text,
                                        Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                }
                            }
                        }
                    }

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }
                    if (continuar == true && continuar1 == true)
                    {
                        CmbCampaña.Text = "0";
                        CmbFormato.Text = "0";
                        InicializarFiltros();
                        cancelarActivarbotones();

                        LimpiarFiltros();

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                    }
                }
                #endregion

                //guardar para : FORMATOS FINALES DG
                #region FORMATOS FINALES DG
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_DG"])
                {
                    if (GvDigitacionDG.Rows.Count > 0)
                    {
                        for (int i = 0; i <= GvDigitacionDG.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionDG.Columns[3].Visible == true || GvDigitacionDG.Columns[4].Visible == true
                                || GvDigitacionDG.Columns[5].Visible == true || GvDigitacionDG.Columns[6].Visible == true
                                || GvDigitacionDG.Columns[7].Visible == true || GvDigitacionDG.Columns[8].Visible == true)
                            {
                                if (((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal1")).Text != "" ||
                                    ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal2")).Text != "" ||
                                    ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal3")).Text != "" ||
                                    ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal4")).Text != "" ||
                                    ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal5")).Text != "" ||
                                    ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtTotal")).Text != "")
                                {
                                    continuar1 = true;


                                    EOPE_DigFORMATOFINALESDG oeOPE_DigFORMATOFINALESDG = oOPE_DigFORMATOFINALESDG.RegistrarFormatoFinalesDG(TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                                        Convert.ToInt32(CmbOperativo.SelectedValue), Convert.ToInt32(CmbZonaMayor.SelectedValue), TxtCliente.Text, LblCliente.Text,
                                      ((Label)GvDigitacionDG.Rows[i].Cells[0].FindControl("LblSku")).Text,
                                      ((Label)GvDigitacionDG.Rows[i].Cells[0].FindControl("LblProd")).Text,
                                      ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal1")).Text,
                                      ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal2")).Text,
                                      ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal3")).Text,
                                      ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal4")).Text,
                                      ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal5")).Text,
                                      ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtTotal")).Text,
                                      Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                }
                            }
                        }
                    }

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }
                    if (continuar == true && continuar1 == true)
                    {
                        CmbCampaña.Text = "0";
                        CmbFormato.Text = "0";
                        InicializarFiltros();
                        cancelarActivarbotones();

                        LimpiarFiltros();

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                    }
                }
                #endregion

                //guardar para : FORMATOS FINALES AG COCINA
                #region FORMATOS AG COCINA
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_AGCOCINA"])
                {
                    if (GvDigitacionAGCocina.Rows.Count > 0)
                    {
                        for (int i = 0; i <= GvDigitacionAGCocina.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionAGCocina.Columns[3].Visible == true || GvDigitacionAGCocina.Columns[4].Visible == true
                                || GvDigitacionAGCocina.Columns[5].Visible == true || GvDigitacionAGCocina.Columns[6].Visible == true
                                || GvDigitacionAGCocina.Columns[7].Visible == true || GvDigitacionAGCocina.Columns[8].Visible == true)
                            {
                                if (((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtPrecio")).Text != "" ||
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockini")).Text != "" ||
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtIngresos")).Text != "" ||
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockTot")).Text != "" ||
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockFin")).Text != "" ||
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("Txtventas")).Text != "")
                                {
                                    continuar1 = true;

                                    EOPE_DigFORMATOAGCOCINA oeOPE_DigFORMATOAGCOCINA = oOPE_DigFORMATOAGCOCINA.RegistrarFormatoAGCOCINA
                                        (TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text), Convert.ToDateTime(TxtFechaFin.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                                        Convert.ToInt32(CmbZonaMayor.SelectedValue), TxtCliente.Text, LblCliente.Text,
                                        ((Label)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("LblSku")).Text,
                                        ((Label)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("LblProd")).Text,
                                        ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtPrecio")).Text,
                                        ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockini")).Text,
                                        ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtIngresos")).Text,
                                        ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockTot")).Text,
                                        ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockFin")).Text,
                                        ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("Txtventas")).Text,
                                        Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                }
                            }
                        }
                    }

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }
                    if (continuar == true && continuar1 == true)
                    {
                        CmbCampaña.Text = "0";
                        CmbFormato.Text = "0";
                        InicializarFiltros();
                        cancelarActivarbotones();

                        LimpiarFiltros();

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                    }
                }
                #endregion

                //guardar para : FORMATOS MIMASKOT
                #region FORMATOS MIMASKOT
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_MIMASKOT"])
                {
                    if (GvDigitacionMimaskot.Rows.Count > 0)
                    {
                        for (int i = 0; i <= GvDigitacionMimaskot.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionMimaskot.Columns[3].Visible == true || GvDigitacionMimaskot.Columns[4].Visible == true
                                || GvDigitacionMimaskot.Columns[5].Visible == true || GvDigitacionMimaskot.Columns[6].Visible == true
                                || GvDigitacionMimaskot.Columns[7].Visible == true || GvDigitacionMimaskot.Columns[8].Visible == true
                                || GvDigitacionMimaskot.Columns[9].Visible == true || GvDigitacionMimaskot.Columns[10].Visible == true)
                            {
                                if (((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockini")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtIngresos")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockTot")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockFin")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("Txtventas")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPreReventa")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtMejorpreRev")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPrecioCsto")).Text != "")
                                {
                                    continuar1 = true;

                                    // CAMBIAR POR METODO PARA REGISTRAR EN MIMASKOT
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_MIMASKOT",
                                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                                    Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                                    TxtLugarDireccion.Text,
                                    ((Label)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("LblSku")).Text,
                                    ((Label)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("LblProd")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockini")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtIngresos")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockTot")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockFin")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("Txtventas")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPreReventa")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtMejorpreRev")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPrecioCsto")).Text,
                                    Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                }
                            }
                        }
                    }

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }
                    if (continuar == true && continuar1 == true)
                    {
                        CmbCampaña.Text = "0";
                        CmbFormato.Text = "0";
                        InicializarFiltros();
                        cancelarActivarbotones();

                        LimpiarFiltros();

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                    }
                }
                #endregion

                //guardar para : FORMATO_FINALES_PRECIOS
                #region FORMATO PRECIOS CUIDADO DEL CABELLO
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PRECIOS_CUIDADOCABELLO"])
                {
                    if (GVDigitacionPrecios.Rows.Count > 0)
                    {
                        for (int i = 0; i <= GVDigitacionPrecios.Rows.Count - 1; i++)
                        {
                            if (GVDigitacionPrecios.Columns[4].Visible == true || GVDigitacionPrecios.Columns[5].Visible == true
                                || GVDigitacionPrecios.Columns[6].Visible == true || GVDigitacionPrecios.Columns[7].Visible == true)
                            {
                                if (((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtCosto")).Text != "" ||
                                    ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventa")).Text != "" ||
                                    ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventaUnid")).Text != "" ||
                                    ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtObservaciones")).Text != "")
                                {
                                    continuar1 = true;
                                    EOPE_DigFORMATOFINALESPRECIOS oeOPE_DigFORMATOFINALESPRECIOS = oOPE_DigFORMATOFINALESPRECIOS.RegistrarFormatoFinalesPrecios(TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                                        Convert.ToInt32(CmbOperativo.SelectedValue), Convert.ToInt32(CmbZonaMayor.SelectedValue), TxtCliente.Text, LblCliente.Text,
                                        ((Label)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("LblSubCatg")).Text,
                                       ((Label)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("LblSku")).Text,
                                       ((Label)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("LblProd")).Text,
                                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtCosto")).Text,
                                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventa")).Text,
                                         ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtObservaciones")).Text,
                                         ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventaUnid")).Text,
                                        Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                }
                            }
                        }
                    }

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }
                    if (continuar == true && continuar1 == true)
                    {
                        CmbCampaña.Text = "0";
                        CmbFormato.Text = "0";
                        InicializarFiltros();
                        cancelarActivarbotones();

                        LimpiarFiltros();

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                    }
                }
                #endregion

                //guardar para : FORMATOS DIAS GIRO CUIDADO DEL CABELLO
                #region FORMATOS DIAS GIRO CUIDADO DEL CABELLO
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_DIASGIRO"])
                {
                    if (GvDigitacionAGCocina.Rows.Count > 0)
                    {
                        for (int i = 0; i <= GvDigitacionAGCocina.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionAGCocina.Columns[3].Visible == true || GvDigitacionAGCocina.Columns[4].Visible == true
                                || GvDigitacionAGCocina.Columns[5].Visible == true || GvDigitacionAGCocina.Columns[6].Visible == true
                                || GvDigitacionAGCocina.Columns[7].Visible == true || GvDigitacionAGCocina.Columns[8].Visible == true)
                            {
                                if (((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtPrecio")).Text != "" ||
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockini")).Text != "" ||
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtIngresos")).Text != "" ||
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockTot")).Text != "" ||
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockFin")).Text != "" ||
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("Txtventas")).Text != "")
                                {
                                    continuar1 = true;

                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_DIASGIRO",
                                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                                        Convert.ToInt32(CmbZonaMayor.SelectedValue), TxtCliente.Text, LblCliente.Text,
                                        ((Label)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("LblSku")).Text,
                                        ((Label)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("LblProd")).Text,
                                        ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtPrecio")).Text,
                                        ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockini")).Text,
                                        ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtIngresos")).Text,
                                        ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockTot")).Text,
                                        ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockFin")).Text,
                                        ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("Txtventas")).Text,
                                        Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                }
                            }
                        }
                    }

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }
                    if (continuar == true && continuar1 == true)
                    {
                        CmbCampaña.Text = "0";
                        CmbFormato.Text = "0";
                        InicializarFiltros();
                        cancelarActivarbotones();

                        LimpiarFiltros();

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                    }
                }
                #endregion

                //guardar para : FORMATOS NUTRICAN
                #region FORMATOS NUTRICAN
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_NUTRICAN"])
                {
                    if (GvDigitacionMimaskot.Rows.Count > 0)
                    {
                        for (int i = 0; i <= GvDigitacionMimaskot.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionMimaskot.Columns[3].Visible == true || GvDigitacionMimaskot.Columns[4].Visible == true
                                || GvDigitacionMimaskot.Columns[5].Visible == true || GvDigitacionMimaskot.Columns[6].Visible == true
                                || GvDigitacionMimaskot.Columns[7].Visible == true || GvDigitacionMimaskot.Columns[8].Visible == true
                                || GvDigitacionMimaskot.Columns[9].Visible == true || GvDigitacionMimaskot.Columns[10].Visible == true)
                            {
                                if (((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockini")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtIngresos")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockTot")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockFin")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("Txtventas")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPreReventa")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtMejorpreRev")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPrecioCsto")).Text != "")
                                {
                                    continuar1 = true;

                                    // CAMBIAR POR METODO PARA REGISTRAR EN MIMASKOT
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_MIMASKOT",
                                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                                    Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                                    TxtLugarDireccion.Text,
                                    ((Label)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("LblSku")).Text,
                                    ((Label)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("LblProd")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockini")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtIngresos")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockTot")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockFin")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("Txtventas")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPreReventa")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtMejorpreRev")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPrecioCsto")).Text,
                                    Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                }
                            }
                        }
                    }

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }
                    if (continuar == true && continuar1 == true)
                    {
                        CmbCampaña.Text = "0";
                        CmbFormato.Text = "0";
                        InicializarFiltros();
                        cancelarActivarbotones();

                        LimpiarFiltros();

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                    }
                }
                #endregion

                //guardar para : FORMATO PROMOCION GALLETAS
                #region FORMATOS PROMOCION GALLETAS
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PROMOCION_GALLETAS"])
                {
                    if (GvDigitacionPromGalletas.Rows.Count > 0)
                    {
                        for (int i = 0; i <= GvDigitacionPromGalletas.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionPromGalletas.Columns[3].Visible == true || GvDigitacionPromGalletas.Columns[4].Visible == true
                                || GvDigitacionPromGalletas.Columns[5].Visible == true || GvDigitacionPromGalletas.Columns[6].Visible == true
                                || GvDigitacionPromGalletas.Columns[7].Visible == true || GvDigitacionPromGalletas.Columns[8].Visible == true
                                || GvDigitacionPromGalletas.Columns[9].Visible == true || GvDigitacionPromGalletas.Columns[10].Visible == true)
                            {
                                if (((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtStockini")).Text != "" ||
                                    ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtIngresos_1sem")).Text != "" ||
                                    ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtIngresos_2sem")).Text != "" ||
                                    ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtTotIngresos")).Text != "" ||
                                    ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtStockTot")).Text != "" ||
                                    ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtStockFin")).Text != "" ||
                                    ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("Txtventas")).Text != "" ||
                                    ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtPrecio")).Text != "")
                                {
                                    continuar1 = true;

                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_PROMGALLETAS",
                                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                                        Convert.ToDateTime(TxtFechaFin.Text), Convert.ToInt32(CmbOperativo.SelectedValue), CmbMercado.Text,
                                        TxtCliente.Text, LblCliente.Text, Convert.ToInt64(CmbCiudad.SelectedValue),
                                        ((Label)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("LblSku")).Text,
                                        ((Label)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("LblProd")).Text,
                                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtStockini")).Text,
                                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtIngresos_1sem")).Text,
                                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtIngresos_2sem")).Text,
                                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtTotIngresos")).Text,
                                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtStockTot")).Text,
                                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtStockFin")).Text,
                                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("Txtventas")).Text,
                                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtPrecio")).Text,
                                       Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                                }
                            }
                        }
                    }

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }
                    if (continuar == true && continuar1 == true)
                    {
                        CmbCampaña.Text = "0";
                        CmbFormato.Text = "0";
                        InicializarFiltros();
                        cancelarActivarbotones();

                        LimpiarFiltros();

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                    }
                }
                #endregion

                //guardar para : FORMATOS AB MASCOTAS - INFORME SEMANAL
                #region FORMATOS AB MASCOTAS - INFORME SEMANAL
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASINFSEM"])
                {
                    if (GvDigitacionMimaskot.Rows.Count > 0)
                    {
                        for (int i = 0; i <= GvDigitacionMimaskot.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionMimaskot.Columns[3].Visible == true || GvDigitacionMimaskot.Columns[4].Visible == true
                                || GvDigitacionMimaskot.Columns[5].Visible == true || GvDigitacionMimaskot.Columns[6].Visible == true
                                || GvDigitacionMimaskot.Columns[7].Visible == true || GvDigitacionMimaskot.Columns[8].Visible == true
                                || GvDigitacionMimaskot.Columns[9].Visible == true || GvDigitacionMimaskot.Columns[10].Visible == true)
                            {
                                if (((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockini")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtIngresos")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockTot")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockFin")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("Txtventas")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPreReventa")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtMejorpreRev")).Text != "" ||
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPrecioCsto")).Text != "")
                                {
                                    continuar1 = true;

                                    // CAMBIAR POR METODO PARA REGISTRAR EN ABMASCOTAS
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_ABMASCOTAS",
                                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                                    Convert.ToDateTime(TxtFecha.Text), Convert.ToDateTime(TxtFechaFin.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                                    TxtLugarDireccion.Text, TxtCliente.Text, LblCliente.Text,
                                    ((Label)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("LblSku")).Text,
                                    ((Label)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("LblProd")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockini")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtIngresos")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockTot")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockFin")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("Txtventas")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPreReventa")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtMejorpreRev")).Text,
                                    ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPrecioCsto")).Text,
                                    Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                }
                            }
                        }
                    }

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }
                    if (continuar == true && continuar1 == true)
                    {
                        CmbCampaña.Text = "0";
                        CmbFormato.Text = "0";
                        InicializarFiltros();
                        cancelarActivarbotones();

                        LimpiarFiltros();

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                    }
                }
                #endregion

                //////////////////////////////////////////////////////////////////////////////////////
                //guardar para : FORMATOS AB MASCOTAS - HOJA DE CONTROL
                #region FORMATOS AB MASCOTAS - HOJA DE CONTROL
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASHOJACONTROL"])
                {
                    if (GvDigitacionHojaControl.Rows.Count >= 0)
                    {
                        for (int i = 0; i <= GvDigitacionHojaControl.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionHojaControl.Columns[1].Visible == true || GvDigitacionHojaControl.Columns[2].Visible == true
                                || GvDigitacionHojaControl.Columns[3].Visible == true || GvDigitacionHojaControl.Columns[4].Visible == true
                                || GvDigitacionHojaControl.Columns[5].Visible == true || GvDigitacionHojaControl.Columns[6].Visible == true
                                || GvDigitacionHojaControl.Columns[7].Visible == true || GvDigitacionHojaControl.Columns[8].Visible == true
                                || GvDigitacionHojaControl.Columns[9].Visible == true || GvDigitacionHojaControl.Columns[10].Visible == true
                                || GvDigitacionHojaControl.Columns[11].Visible == true || GvDigitacionHojaControl.Columns[12].Visible == true
                                || GvDigitacionHojaControl.Columns[13].Visible == true || GvDigitacionHojaControl.Columns[14].Visible == true
                                || GvDigitacionHojaControl.Columns[15].Visible == true || GvDigitacionHojaControl.Columns[16].Visible == true
                                || GvDigitacionHojaControl.Columns[17].Visible == true || GvDigitacionHojaControl.Columns[18].Visible == true
                                || GvDigitacionHojaControl.Columns[19].Visible == true )
                            {
                                if (((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtFact")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtClieMay")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtDNI")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTel")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCarne_MM")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtPollo_MM")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCachorro_MM")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCordero_MM")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTotales_MM")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtAdulto_NC")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCachorro_NC")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTotales_NC")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtMonto_Soles")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtPlatos_MM")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtPlatos_NC")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTaza_MM")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTaza_NC")).Text != "" ||
                                ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtFirma")).Text != "" )

                                {
                                    continuar1 = true;

                                    // CAMBIAR POR METODO PARA REGISTRAR EN ABMASCOTAS
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_ABMASCOTAS_HOJA_CONTROL",
                                        TxtCodPlanning.Text, 
                                        Convert.ToInt32(CmbFormato.Text),
                                        123,
                                        "zona_mayorista_01",
                                        456,
                                    Convert.ToDateTime(TxtFecha.Text), 
                                    Convert.ToDateTime(TxtFechaFin.Text),
                                    789,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtFact")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtClieMay")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtDNI")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTel")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCarne_MM")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtRazas_Pequeñas_MM")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtPollo_MM")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCachorro_MM")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCordero_MM")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTotales_MM")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtAdulto_NC")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCachorro_NC")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTotales_NC")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtMonto_Soles")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtPlatos_MM")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtPlatos_NC")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTaza_MM")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTaza_NC")).Text,
                                    ((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtFirma")).Text,
                                    Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                                }
                            }
                        }
                    }

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }
                    if (continuar == true && continuar1 == true)
                    {
                        CmbCampaña.Text = "0";
                        CmbFormato.Text = "0";
                        InicializarFiltros();
                        cancelarActivarbotones();

                        LimpiarFiltros();

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                    }
                }
                #endregion
                ///////////////////////////////////////////////////////////////////////////////////////

                ///Modificando:02 jul 2011
                //guardar para : FORMATOS HOJA DE CONTROL- ABARROTES_ROTACION_MULTICATEGORIA CABECERA
                #region FORMATOS HOJA DE CONTROL- ABARROTES_ROTACION_MULTICATEGORIA
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
                {
                    /////////////////////////////////////////////////////////////////////////////////////////

                    //if (GvDigitacionHojaControlAbarrotes.Rows.Count >= 0)
                    //{
                    
                    //continuar1 = true;

                    //// CREANDO... METODO PARA REGISTRA HOJA DE CONTROL ABARROTES_ROTACIONMULTICATEGORIA
                    //oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_CAB",
                    //    TxtCodPlanning.Text,
                    //    Convert.ToInt32(CmbFormato.Text),
                    //    Convert.ToDateTime(TxtFecha.Text),
                    //    Convert.ToInt32(CmbOperativo.Text),
                    //    TxtNombreClieMayorista.Text,
                    //    Convert.ToInt32(CmbZonaMayor.Text),
                    //    CmbCategoria.Text,
                    //    Convert.ToString(this.Session["sUser"]), 
                    //    DateTime.Now, 
                    //    Convert.ToString(this.Session["sUser"]), 
                    //    DateTime.Now,
                    //    "");

                    //}
                   
                    //if (continuar == true && continuar1 == false)
                    //{
                    //    this.Session["encabemensa"] = "Sr. Usuario";
                    //    this.Session["cssclass"] = "MensajesSupervisor";
                    //    this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                    //    Mensajes_Usuario();
                    //}
                    //if (continuar == true && continuar1 == true)
                    //{
                    //    CmbCampaña.Text = "0";
                    //    CmbFormato.Text = "0";
                    //    InicializarFiltros();
                    //    cancelarActivarbotones();

                    //    LimpiarFiltros();

                    //    this.Session["encabemensa"] = "Sr. Usuario";
                    //    this.Session["cssclass"] = "MensajesSupConfirm";
                    //    this.Session["mensaje"] = "Se almacenó la información registrada";
                    //    Mensajes_Usuario();
                        
                    //    IdentificarFiltros(Convert.ToInt32(12));
                    //    Filtros.EnableViewState = true;

                    //}



                    ////////////////////////////////////////////////////////////////////////////////////////

                    //////////////////////////////////////////////////////////////////////////////////////
                    //if (GvDigitacionHojaControlAbarrotes.Rows.Count >= 0)
                    //{
                    //    for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                    //    {
                            //if (GvDigitacionHojaControl.Columns[1].Visible == true || GvDigitacionHojaControl.Columns[2].Visible == true
                            //    || GvDigitacionHojaControl.Columns[3].Visible == true || GvDigitacionHojaControl.Columns[4].Visible == true
                            //    || GvDigitacionHojaControl.Columns[5].Visible == true || GvDigitacionHojaControl.Columns[6].Visible == true
                            //    || GvDigitacionHojaControl.Columns[7].Visible == true || GvDigitacionHojaControl.Columns[8].Visible == true
                            //    || GvDigitacionHojaControl.Columns[9].Visible == true || GvDigitacionHojaControl.Columns[10].Visible == true
                            //    || GvDigitacionHojaControl.Columns[11].Visible == true || GvDigitacionHojaControl.Columns[12].Visible == true
                            //    || GvDigitacionHojaControl.Columns[13].Visible == true || GvDigitacionHojaControl.Columns[14].Visible == true
                            //    || GvDigitacionHojaControl.Columns[15].Visible == true || GvDigitacionHojaControl.Columns[16].Visible == true
                            //    || GvDigitacionHojaControl.Columns[17].Visible == true || GvDigitacionHojaControl.Columns[18].Visible == true
                            //    || GvDigitacionHojaControl.Columns[19].Visible == true)
                            //{
                                //if (((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtFact")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtClieMay")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtDNI")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTel")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCarne_MM")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtPollo_MM")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCachorro_MM")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCordero_MM")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTotales_MM")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtAdulto_NC")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCachorro_NC")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTotales_NC")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtMonto_Soles")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtPlatos_MM")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtPlatos_NC")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTaza_MM")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTaza_NC")).Text != "" ||
                                //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtFirma")).Text != "")
                                //{
                                    //continuar1 = true;

                                    // CREANDO... METODO PARA REGISTRA HOJA DE CONTROL ABARROTES_ROTACIONMULTICATEGORIA
                                    //oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_ABMASCOTAS_HOJA_CONTROL",
                                    //    TxtCodPlanning.Text,
                                    //    Convert.ToInt32(CmbFormato.Text),
                                    //    123,
                                    //    "zona_mayorista_01",
                                    //    456,
                                    //Convert.ToDateTime(TxtFecha.Text),
                                    //Convert.ToDateTime(TxtFechaFin.Text),
                                    //789,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtFact")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtClieMay")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtDNI")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTel")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCarne_MM")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtRazas_Pequeñas_MM")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtPollo_MM")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCachorro_MM")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCordero_MM")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTotales_MM")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtAdulto_NC")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtCachorro_NC")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTotales_NC")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtMonto_Soles")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtPlatos_MM")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtPlatos_NC")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTaza_MM")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtTaza_NC")).Text,
                                    //((TextBox)GvDigitacionHojaControl.Rows[i].Cells[0].FindControl("TxtFirma")).Text,
                                    //Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);

                                //}
                            //}
                    //    }
                    //}

                    //if (continuar == true && continuar1 == false)
                    //{
                    //    this.Session["encabemensa"] = "Sr. Usuario";
                    //    this.Session["cssclass"] = "MensajesSupervisor";
                    //    this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                    //    Mensajes_Usuario();
                    //}
                    //if (continuar == true && continuar1 == true)
                    //{
                    //    CmbCampaña.Text = "0";
                    //    CmbFormato.Text = "0";
                    //    InicializarFiltros();
                    //    cancelarActivarbotones();

                    //    LimpiarFiltros();

                    //    this.Session["encabemensa"] = "Sr. Usuario";
                    //    this.Session["cssclass"] = "MensajesSupConfirm";
                    //    this.Session["mensaje"] = "Se almacenó la información registrada";
                    //    Mensajes_Usuario();
                    //}
                    ////////////////////////////////////////////////////////////////////////////////////
                }
                #endregion

                // Continuan el resto de formatos para guardar cada uno en un region
            }
        }
        #endregion EVENTO CLICK: BOTON GUARDAR : SUPERCABECERA
        #region EVENTO CLICK : BOTON NUEVO: SUPERCABECERA (Crea una Session[Option]="New")
        protected void ImgNewDigitacion_Click(object sender, ImageClickEventArgs e)
        {
            crearActivarbotones();
            this.Session["Opcion"] = "New";
        }
        #endregion EVENTO CLICK : BOTON NUEVO: SUPERCABECERA  (Crea una Session[Option]="New")
        #region EVENTO CLICK: BOTON CANCELAR : SUPERCABECERA
        protected void ImgCancelDigitacion_Click(object sender, ImageClickEventArgs e)
        {
            cancelarActivarbotones();
            InicializarFiltros();
            HabilitarObjetos();
        }
        #endregion EVENTO CLICK: BOTON CANCELAR : SUPERCABECERA
        #region EVENTO CLICK : BOTON CONSULTAR : SUPERCABECERA
        protected void ImgSearchDigitacion_Click(object sender, ImageClickEventArgs e)
        {
            consultarActivarbotones();
            crearActivarbotones();
            this.Session["Opcion"] = "Search";
            ImageButton1.Visible = false;
        }
        #endregion EVENTO CLICK : BOTON CONSULTAR : SUPERCABECERA

        #endregion EVENTOS CLICK: SUPERCABECERA BOTONES
        #region EVENTOS CLICK: FILTRO PRINCIPAL : BOTONES

        #region EVENTO CLICK: BOTON EDITAR : FILTRO PRINCIPAL
        protected void ImgHabEditDigitacion_Click(object sender, ImageClickEventArgs e)
        {
            GvDigitacion.Enabled = true;
            GVDigitacionPrecios.Enabled = true;
            GvDigitacionDG.Enabled = true;
            GvDigitacionAGCocina.Enabled = true;
            GvDigitacionMimaskot.Enabled = true;
            GvDigitacionPromGalletas.Enabled = true;
            GvDigitacionHojaControlAbarrotes.Enabled = true;
            GvDigitacionHojaControlAbarrotes_Premios.Enabled = true;

            ImgEditDigitacion.Visible = false;//BtnConsultar - cab
            ImgUpdateDigitacion.Visible = true;//BtnActualizar-Cab
            ImgHabEditDigitacion.Visible = false;//BtnEditar - Cab
            ImgGuardarCabeceraOpeDigitacion.Visible = false;
            ImgCancelEditDigitacion.Visible = true;//BtnCancelar - Cab
            
         }
        #endregion EVENTO CLICK: BOTON EDITAR : FILTRO PRINCIPAL
        #region EVENTO CLICK: BOTON CONSULTAR : FILTRO PRINCIPAL
        protected void ImgEditDigitacion_Click(object sender, ImageClickEventArgs e)
        {
            bool continuar = true;
            if (ValidainfoFiltros(continuar))
            {
                ///PARA FORMATOS DE VENTAS
                DataTable dtColumnasDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_COLUMNAS_A_MOSTRAR", Convert.ToInt32(CmbFormato.SelectedValue));

                ///PARA FORMATOS DE HOJAS DE CONTROL
                DataTable dtParametrosDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIGITACION_IDENTIFICA_PARAMETROS", Convert.ToInt32(CmbFormato.SelectedValue));

                DataTable dtDigitacion = new DataTable();
                #region FORMATO_FINAL_SOD
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINAL_SOD"])
                {
                    dtDigitacion = oOPE_DigFORMATOFINALSOD.ConsultarOPE_DigFORMATOFINALSOD(
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                       Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                      TxtCliente.Text, Convert.ToInt64(CmbCiudad.SelectedValue));

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacion.DataSource = dtDigitacion;
                            GvDigitacion.DataBind();
                            GvDigitacion.Enabled = false;
                            GvDigitacion.Columns[0].Visible = true;
                            GvDigitacion.Columns[5].Visible = true;
                            InhabilitarObjetos();

                            if (dtColumnasDigitacion != null)
                            {
                                if (dtColumnasDigitacion.Rows.Count > 0)
                                {
                                    if (dtColumnasDigitacion.Rows[0]["Exhib_Primaria"].ToString().Trim() == "False")
                                    {
                                        GvDigitacion.Columns[3].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacion.Columns[3].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Exhib_Secundaria"].ToString().Trim() == "False")
                                    {
                                        GvDigitacion.Columns[4].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacion.Columns[4].Visible = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        HabilitarObjetos();
                        GvDigitacion.DataBind();
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                        Mensajes_Usuario();

                    }
                }
                #endregion
                #region FORMATO_FINALES_PRECIOS
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_PRECIOS"])
                {
                    dtDigitacion = oOPE_DigFORMATOFINALESPRECIOS.ConsultarOPE_DigFORMATOFINALESPRECIOS(
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                      Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                     Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GVDigitacionPrecios.DataSource = dtDigitacion;
                            GVDigitacionPrecios.DataBind();
                            GVDigitacionPrecios.Enabled = false;
                            GVDigitacionPrecios.Columns[0].Visible = true;
                            GVDigitacionPrecios.Columns[8].Visible = true;
                            InhabilitarObjetos();
                            if (dtColumnasDigitacion != null)
                            {
                                if (dtColumnasDigitacion.Rows.Count > 0)
                                {
                                    if (dtColumnasDigitacion.Rows[0]["Precio_Costo"].ToString().Trim() == "False")
                                    {
                                        GVDigitacionPrecios.Columns[4].Visible = false;
                                    }
                                    else
                                    {
                                        GVDigitacionPrecios.Columns[4].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Precio_Reventa"].ToString().Trim() == "False")
                                    {
                                        GVDigitacionPrecios.Columns[5].Visible = false;
                                    }
                                    else
                                    {
                                        GVDigitacionPrecios.Columns[5].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Precio_Reventa_Unid"].ToString().Trim() == "False")
                                    {
                                        GVDigitacionPrecios.Columns[6].Visible = false;
                                    }
                                    else
                                    {
                                        GVDigitacionPrecios.Columns[6].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Observaciones"].ToString().Trim() == "False")
                                    {
                                        GVDigitacionPrecios.Columns[7].Visible = false;
                                    }
                                    else
                                    {
                                        GVDigitacionPrecios.Columns[7].Visible = true;
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        HabilitarObjetos();
                        GVDigitacionPrecios.DataBind();
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                        Mensajes_Usuario();

                    }
                }
                #endregion
                #region FORMATOS FINALES DG
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_DG"])
                {
                    dtDigitacion = oOPE_DigFORMATOFINALESDG.ConsultarFormatoFinalesDG(
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                      Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                     Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionDG.DataSource = dtDigitacion;
                            GvDigitacionDG.DataBind();
                            GvDigitacionDG.Enabled = false;
                            GvDigitacionDG.Columns[0].Visible = true;
                            GvDigitacionDG.Columns[9].Visible = true;
                            InhabilitarObjetos();
                            if (dtColumnasDigitacion != null)
                            {
                                if (dtColumnasDigitacion.Rows.Count > 0)
                                {
                                    if (dtColumnasDigitacion.Rows[0]["Local_1"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionDG.Columns[3].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionDG.Columns[3].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Local_2"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionDG.Columns[4].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionDG.Columns[4].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Local_3"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionDG.Columns[5].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionDG.Columns[5].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Local_4"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionDG.Columns[6].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionDG.Columns[6].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Local_5"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionDG.Columns[7].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionDG.Columns[7].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Total"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionDG.Columns[8].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionDG.Columns[8].Visible = true;
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        HabilitarObjetos();
                        GvDigitacionDG.DataBind();
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                        Mensajes_Usuario();
                    }
                }
                #endregion
                #region FORMATOS AG COCINA
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_AGCOCINA"])
                {
                    dtDigitacion = oOPE_DigFORMATOAGCOCINA.ConsultarFormatoAGCOCINA(
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                      Convert.ToDateTime(TxtFecha.Text), Convert.ToDateTime(TxtFechaFin.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                     Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionAGCocina.DataSource = dtDigitacion;
                            GvDigitacionAGCocina.DataBind();
                            GvDigitacionAGCocina.Enabled = false;
                            GvDigitacionAGCocina.Columns[0].Visible = true;
                            GvDigitacionAGCocina.Columns[9].Visible = true;
                            InhabilitarObjetos();
                            if (dtColumnasDigitacion != null)
                            {
                                if (dtColumnasDigitacion.Rows.Count > 0)
                                {
                                    if (dtColumnasDigitacion.Rows[0]["Precio"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionAGCocina.Columns[3].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionAGCocina.Columns[3].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Stock_inicial"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionAGCocina.Columns[4].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionAGCocina.Columns[4].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ingresos"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionAGCocina.Columns[5].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionAGCocina.Columns[5].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Total_Stock"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionAGCocina.Columns[6].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionAGCocina.Columns[6].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Stock_Final"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionAGCocina.Columns[7].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionAGCocina.Columns[7].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ventas"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionAGCocina.Columns[8].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionAGCocina.Columns[8].Visible = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        HabilitarObjetos();
                        GvDigitacionAGCocina.DataBind();
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                        Mensajes_Usuario();
                    }
                }
                #endregion
                #region FORMATOS MIMASKOT
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_MIMASKOT"])
                {
                    //CAMBIAR POR METODO PARA CONSULTAR EN MIMASKOT
                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_MIMASKOT",
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                       Convert.ToInt32(CmbOperativo.SelectedValue), TxtLugarDireccion.Text);



                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionMimaskot.DataSource = dtDigitacion;
                            GvDigitacionMimaskot.DataBind();
                            GvDigitacionMimaskot.Enabled = false;
                            GvDigitacionMimaskot.Columns[0].Visible = true;
                            GvDigitacionMimaskot.Columns[11].Visible = true;
                            InhabilitarObjetos();
                            if (dtColumnasDigitacion != null)
                            {
                                if (dtColumnasDigitacion.Rows.Count > 0)
                                {
                                    if (dtColumnasDigitacion.Rows[0]["Stock_inicial"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[3].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[3].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ingresos"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[4].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[4].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Total_Stock"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[5].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[5].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Stock_Final"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[6].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[6].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ventas"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[7].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[7].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Precio_Reventa"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[8].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[8].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Mejor_Precio_Reventa"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[9].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[9].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Precio_Costo"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[10].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[10].Visible = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            HabilitarObjetos();
                            GvDigitacionMimaskot.DataBind();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion
                #region FORMATO PRECIOS CUIDADO DEL CABELLO
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PRECIOS_CUIDADOCABELLO"])
                {
                    dtDigitacion = oOPE_DigFORMATOFINALESPRECIOS.ConsultarOPE_DigFORMATOFINALESPRECIOS(
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                      Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                     Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GVDigitacionPrecios.DataSource = dtDigitacion;
                            GVDigitacionPrecios.DataBind();
                            GVDigitacionPrecios.Enabled = false;
                            GVDigitacionPrecios.Columns[0].Visible = true;
                            GVDigitacionPrecios.Columns[8].Visible = true;
                            InhabilitarObjetos();
                            if (dtColumnasDigitacion != null)
                            {
                                if (dtColumnasDigitacion.Rows.Count > 0)
                                {
                                    if (dtColumnasDigitacion.Rows[0]["Precio_Costo"].ToString().Trim() == "False")
                                    {
                                        GVDigitacionPrecios.Columns[4].Visible = false;
                                    }
                                    else
                                    {
                                        GVDigitacionPrecios.Columns[4].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Precio_Reventa"].ToString().Trim() == "False")
                                    {
                                        GVDigitacionPrecios.Columns[5].Visible = false;
                                    }
                                    else
                                    {
                                        GVDigitacionPrecios.Columns[5].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Precio_Reventa_Unid"].ToString().Trim() == "False")
                                    {
                                        GVDigitacionPrecios.Columns[6].Visible = false;
                                    }
                                    else
                                    {
                                        GVDigitacionPrecios.Columns[6].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Observaciones"].ToString().Trim() == "False")
                                    {
                                        GVDigitacionPrecios.Columns[7].Visible = false;
                                    }
                                    else
                                    {
                                        GVDigitacionPrecios.Columns[7].Visible = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        HabilitarObjetos();
                        GVDigitacionPrecios.DataBind();
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                        Mensajes_Usuario();

                    }
                }
                #endregion
                #region FORMATOS DIAS GIRO CUIDADO DEL CABELLO
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_DIASGIRO"])
                {
                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_DIASGIRO",
                      TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                      Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                     Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionAGCocina.DataSource = dtDigitacion;
                            GvDigitacionAGCocina.DataBind();
                            GvDigitacionAGCocina.Enabled = false;
                            GvDigitacionAGCocina.Columns[0].Visible = true;
                            GvDigitacionAGCocina.Columns[9].Visible = true;
                            InhabilitarObjetos();
                            if (dtColumnasDigitacion != null)
                            {
                                if (dtColumnasDigitacion.Rows.Count > 0)
                                {
                                    if (dtColumnasDigitacion.Rows[0]["Precio"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionAGCocina.Columns[3].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionAGCocina.Columns[3].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Stock_inicial"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionAGCocina.Columns[4].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionAGCocina.Columns[4].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ingresos"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionAGCocina.Columns[5].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionAGCocina.Columns[5].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Total_Stock"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionAGCocina.Columns[6].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionAGCocina.Columns[6].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Stock_Final"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionAGCocina.Columns[7].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionAGCocina.Columns[7].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ventas"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionAGCocina.Columns[8].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionAGCocina.Columns[8].Visible = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        HabilitarObjetos();
                        GvDigitacionAGCocina.DataBind();
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                        Mensajes_Usuario();
                    }
                }
                #endregion
                #region FORMATOS NUTRICAN
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_NUTRICAN"])
                {
                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_MIMASKOT",
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                       Convert.ToInt32(CmbOperativo.SelectedValue), TxtLugarDireccion.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionMimaskot.DataSource = dtDigitacion;
                            GvDigitacionMimaskot.DataBind();
                            GvDigitacionMimaskot.Enabled = false;
                            GvDigitacionMimaskot.Columns[0].Visible = true;
                            GvDigitacionMimaskot.Columns[11].Visible = true;
                            InhabilitarObjetos();
                            if (dtColumnasDigitacion != null)
                            {
                                if (dtColumnasDigitacion.Rows.Count > 0)
                                {
                                    if (dtColumnasDigitacion.Rows[0]["Stock_inicial"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[3].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[3].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ingresos"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[4].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[4].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Total_Stock"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[5].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[5].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Stock_Final"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[6].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[6].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ventas"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[7].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[7].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Precio_Reventa"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[8].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[8].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Mejor_Precio_Reventa"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[9].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[9].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Precio_Costo"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[10].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[10].Visible = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            HabilitarObjetos();
                            GvDigitacionMimaskot.DataBind();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion
                #region FORMATOS PROMOCION GALLETAS
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PROMOCION_GALLETAS"])
                {

                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_PROMGALLETAS",
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                        Convert.ToDateTime(TxtFechaFin.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                        CmbMercado.Text, Convert.ToInt64(CmbCiudad.SelectedValue), TxtCliente.Text);



                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionPromGalletas.DataSource = dtDigitacion;
                            GvDigitacionPromGalletas.DataBind();
                            GvDigitacionPromGalletas.Enabled = false;
                            GvDigitacionPromGalletas.Columns[0].Visible = true;
                            GvDigitacionPromGalletas.Columns[11].Visible = true;
                            InhabilitarObjetos();
                            if (dtColumnasDigitacion != null)
                            {
                                if (dtColumnasDigitacion.Rows.Count > 0)
                                {
                                    if (dtColumnasDigitacion.Rows[0]["Stock_inicial"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionPromGalletas.Columns[3].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionPromGalletas.Columns[3].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ingresos_Semana1"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionPromGalletas.Columns[4].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[4].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ingresos_Semana2"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionPromGalletas.Columns[5].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionPromGalletas.Columns[5].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Total_Ingresos"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionPromGalletas.Columns[6].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionPromGalletas.Columns[6].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Total_Stock"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionPromGalletas.Columns[7].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionPromGalletas.Columns[7].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Stock_Final"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionPromGalletas.Columns[8].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionPromGalletas.Columns[8].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ventas"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionPromGalletas.Columns[9].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionPromGalletas.Columns[9].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Precio"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionPromGalletas.Columns[10].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionPromGalletas.Columns[10].Visible = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            HabilitarObjetos();
                            GvDigitacionPromGalletas.DataBind();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion
                #region FORMATOS AB MASCOTAS - INFORME SEMANAL
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASINFSEM"])
                {


                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_ABMASCOTAS",
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                        Convert.ToDateTime(TxtFechaFin.Text), Convert.ToInt32(CmbOperativo.SelectedValue), TxtLugarDireccion.Text, TxtCliente.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionMimaskot.DataSource = dtDigitacion;
                            GvDigitacionMimaskot.DataBind();
                            GvDigitacionMimaskot.Enabled = false;
                            GvDigitacionMimaskot.Columns[0].Visible = true;
                            GvDigitacionMimaskot.Columns[11].Visible = true;
                            InhabilitarObjetos();
                            if (dtColumnasDigitacion != null)
                            {
                                if (dtColumnasDigitacion.Rows.Count > 0)
                                {
                                    if (dtColumnasDigitacion.Rows[0]["Stock_inicial"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[3].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[3].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ingresos"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[4].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[4].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Total_Stock"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[5].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[5].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Stock_Final"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[6].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[6].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Ventas"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[7].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[7].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Precio_Reventa"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[8].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[8].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Mejor_Precio_Reventa"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[9].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[9].Visible = true;
                                    }
                                    if (dtColumnasDigitacion.Rows[0]["Precio_Costo"].ToString().Trim() == "False")
                                    {
                                        GvDigitacionMimaskot.Columns[10].Visible = false;
                                    }
                                    else
                                    {
                                        GvDigitacionMimaskot.Columns[10].Visible = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            HabilitarObjetos();
                            GvDigitacionMimaskot.DataBind();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion

                #region FORMATO_ABMASCOTASHOJACONTROL
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASHOJACONTROL"])
                {

                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_ABMASCOTASHOJACONTROL_INTER",
                        TxtCodPlanning.Text,
                        Convert.ToInt32(CmbFormato.Text),
                        Convert.ToInt32(CmbOperativo.SelectedValue),
                        TxtCliente.Text, 
                        CmbCategoria.SelectedValue,
                        Convert.ToDateTime(TxtFecha.Text),
                        Convert.ToDateTime(TxtFechaFin.Text));

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;

                            #region OCULTAR COLUMNAS
                            Mostrar_Todas_Columnas_GVIntermedia();
                            GvIntermedia.Columns[5].Visible = false;//TELEFONO
                            GvIntermedia.Columns[6].Visible = false;//PDV
                            #endregion

                            GvIntermedia.DataSource = dtDigitacion;
                            GvIntermedia.DataBind();
                            GvIntermedia.Visible = true;

                            InhabilitarObjetos();

                        }
                        else
                        {
                            HabilitarObjetos();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion
                
                #region FORMATOS HCCANJEMULTICATEGORIA -
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
                {

                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_INTER",
                        TxtCodPlanning.Text, 
                        Convert.ToInt32(CmbFormato.Text),
                        Convert.ToInt32(CmbOperativo.SelectedValue),
                        TxtCliente.Text, CmbCategoria.SelectedValue,
                        Convert.ToDateTime(TxtFecha.Text));

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;

                            #region OCULTAR COLUMNAS
                            Mostrar_Todas_Columnas_GVIntermedia();
                            GvIntermedia.Columns[5].Visible=false;//TELEFONO
                            GvIntermedia.Columns[6].Visible = false;//PDV
                            #endregion

                            GvIntermedia.DataSource = dtDigitacion;
                            GvIntermedia.DataBind();
                            GvIntermedia.Visible = true;

                            InhabilitarObjetos();
 
                        }
                        else
                        {
                            HabilitarObjetos();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion

                #region FORMATO_CANJE_ANUA - ID = 14
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_CANJE_ANUA"])
                {
                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_CANJE_ANUA_INTER",
                        TxtCodPlanning.Text, 
                        Convert.ToInt32(CmbFormato.Text),
                        Convert.ToInt32(CmbOperativo.SelectedValue),
                        Convert.ToInt32(CmbZonaMayor.SelectedValue),
                        TxtCliente.Text, 
                        CmbCategoria.SelectedValue, 
                        Convert.ToDateTime(TxtFecha.Text));

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            //ImgEditDigitacion.Visible = false;
                            ////ImgHabEditDigitacion.Visible = true;
                            //ImgHabEditDigitacion.Visible = false;
                            //ImgUpdateDigitacion.Visible = true;
                            //ImgCancelEditDigitacion.Visible = true;
                            //ImgCancelDigitacion.Visible = false;
                            //ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            //ImgEditDigitacion.Visible = false;
                            //ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            //InhabilitarObjetos();



                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;

                            #region OCULTAR COLUMNAS
                            Mostrar_Todas_Columnas_GVIntermedia();
                            GvIntermedia.Columns[6].Visible = false;//PUNTO DE VENTA
                            #endregion

                            GvIntermedia.DataSource = dtDigitacion;
                            GvIntermedia.DataBind();
                            GvIntermedia.Visible = true;

                            InhabilitarObjetos();

                        }
                        else
                        {
                            HabilitarObjetos();
                            //GvDigitacionMimaskot.DataBind();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion
                
                #region FORMATO RESUMEN_CANJE_ANUA - F, ID=13
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_ANUA"])
                {
                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_RESUMEN_CANJE_ANUA_INTER",
                            TxtCodPlanning.Text, 
                            Convert.ToInt32(CmbFormato.Text),
                            Convert.ToInt32(CmbOperativo.SelectedValue),
                            Convert.ToInt32(CmbZonaMayor.SelectedValue),
                            Convert.ToInt32(CmbCategoria.SelectedValue), 
                            Convert.ToDateTime(TxtFecha.Text),
                            Convert.ToDateTime(TxtFechaFin.Text)
                            );

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            //ImgEditDigitacion.Visible = false;
                            ////ImgHabEditDigitacion.Visible = true;
                            //ImgHabEditDigitacion.Visible = false;
                            //ImgUpdateDigitacion.Visible = true;
                            //ImgCancelEditDigitacion.Visible = true;
                            //ImgCancelDigitacion.Visible = false;
                            //ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            //ImgEditDigitacion.Visible = false;
                            //ImgGuardarCabeceraOpeDigitacion.Visible = false;

                            //InhabilitarObjetos();
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;

                            #region OCULTAR COLUMNAS
                            Mostrar_Todas_Columnas_GVIntermedia();
                            GvIntermedia.Columns[2].Visible = false;
                            GvIntermedia.Columns[3].Visible = false;
                            GvIntermedia.Columns[4].Visible = false;
                            GvIntermedia.Columns[5].Visible = false;
                            #endregion



                            GvIntermedia.DataSource = dtDigitacion;
                            GvIntermedia.DataBind();
                            GvIntermedia.Visible = true;

                            InhabilitarObjetos();

                        }
                        else
                        {
                            HabilitarObjetos();
                            //GvDigitacionMimaskot.DataBind();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion

                #region FORMATO HDC_CANJE - F, ID=15 - MODIFICAR SEGUN FORMATO
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE"])
                {
                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_INTER",
                            TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                            TxtNombreClieMayorista.Text, CmbCategoria.SelectedValue, Convert.ToDateTime(TxtFecha.Text));

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            //ImgEditDigitacion.Visible = false;
                            ////ImgHabEditDigitacion.Visible = true;
                            //ImgHabEditDigitacion.Visible = false;
                            //ImgUpdateDigitacion.Visible = true;
                            //ImgCancelEditDigitacion.Visible = true;
                            //ImgCancelDigitacion.Visible = false;
                            //ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            //ImgEditDigitacion.Visible = false;
                            //ImgGuardarCabeceraOpeDigitacion.Visible = false;


                            //InhabilitarObjetos();
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;

                            GvIntermedia.DataSource = dtDigitacion;
                            GvIntermedia.DataBind();
                            GvIntermedia.Visible = true;

                            InhabilitarObjetos();
                        }
                        else
                        {
                            HabilitarObjetos();
                            //GvDigitacionMimaskot.DataBind();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion

                #region FORMATO HDC_CANJE-STOCK_PREMIOS_TIRA_1_millar - F, ID=16 - MODIFICAR SEGUN FORMATO
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar"])
                {
                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_INTER",
                            TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                            TxtNombreClieMayorista.Text, CmbCategoria.SelectedValue, Convert.ToDateTime(TxtFecha.Text));

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            //ImgEditDigitacion.Visible = false;
                            ////ImgHabEditDigitacion.Visible = true;
                            //ImgHabEditDigitacion.Visible = false;
                            //ImgUpdateDigitacion.Visible = true;
                            //ImgCancelEditDigitacion.Visible = true;
                            //ImgCancelDigitacion.Visible = false;
                            //ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            //ImgEditDigitacion.Visible = false;
                            //ImgGuardarCabeceraOpeDigitacion.Visible = false;


                            //InhabilitarObjetos();
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;

                            GvIntermedia.DataSource = dtDigitacion;
                            GvIntermedia.DataBind();
                            GvIntermedia.Visible = true;

                            InhabilitarObjetos();

                        }
                        else
                        {
                            HabilitarObjetos();
                            //GvDigitacionMimaskot.DataBind();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion

                #region FORMATO RESUMEN_CANJE_PLUSBELLE - F, ID=17
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_PLUSBELLE"])
                {
                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_RESUMEN_CANJE_PLUSBELLE_INTER",
                            TxtCodPlanning.Text,
                            Convert.ToInt32(CmbFormato.Text),
                            Convert.ToInt32(CmbOperativo.SelectedValue),
                            Convert.ToInt32(CmbZonaMayor.SelectedValue),
                            Convert.ToInt32(CmbCategoria.SelectedValue), 
                            Convert.ToDateTime(TxtFecha.Text),
                            Convert.ToDateTime(TxtFechaFin.Text));

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            //ImgEditDigitacion.Visible = false;
                            ////ImgHabEditDigitacion.Visible = true;
                            //ImgHabEditDigitacion.Visible = false;
                            //ImgUpdateDigitacion.Visible = true;
                            //ImgCancelEditDigitacion.Visible = true;
                            //ImgCancelDigitacion.Visible = false;
                            //ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            //ImgEditDigitacion.Visible = false;
                            //ImgGuardarCabeceraOpeDigitacion.Visible = false;


                            //InhabilitarObjetos();
                            ImgEditDigitacion.Visible = false;
                            ImgHabEditDigitacion.Visible = true;
                            ImgUpdateDigitacion.Visible = false;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;

                            #region OCULTAR COLUMNAS
                            Mostrar_Todas_Columnas_GVIntermedia();
                            GvIntermedia.Columns[2].Visible = false;
                            GvIntermedia.Columns[3].Visible = false;
                            GvIntermedia.Columns[4].Visible = false;
                            GvIntermedia.Columns[5].Visible = false;
                            #endregion


                            GvIntermedia.DataSource = dtDigitacion;
                            GvIntermedia.DataBind();
                            GvIntermedia.Visible = true;

                            InhabilitarObjetos();
                        }
                        else
                        {
                            HabilitarObjetos();
                            //GvDigitacionMimaskot.DataBind();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion

                #region FORMATO COMPETENCIA_ANUA- F, ID=18
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_ANUA"])
                {
                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_INTER",
                            TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                            TxtNombreClieMayorista.Text, CmbCategoria.SelectedValue, Convert.ToDateTime(TxtFecha.Text));

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            //ImgHabEditDigitacion.Visible = true;
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            ImgEditDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;


                            InhabilitarObjetos();

                        }
                        else
                        {
                            HabilitarObjetos();
                            //GvDigitacionMimaskot.DataBind();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion

                #region FORMATO COMPETENCIA_MAYORISTA- F, ID=19
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_MAYORISTA"])
                {
                    dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_INTER",
                            TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                            TxtNombreClieMayorista.Text, CmbCategoria.SelectedValue, Convert.ToDateTime(TxtFecha.Text));

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            //ImgHabEditDigitacion.Visible = true;
                            ImgHabEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;
                            ImgEditDigitacion.Visible = false;
                            ImgGuardarCabeceraOpeDigitacion.Visible = false;


                            InhabilitarObjetos();

                        }
                        else
                        {
                            HabilitarObjetos();
                            //GvDigitacionMimaskot.DataBind();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No existe información registrada con estos filtros.";
                            Mensajes_Usuario();
                        }
                    }
                }
                #endregion

            }
        }
        #endregion EVENTO CLICK: BOTON CONSULTAR : FILTRO PRINCIPAL
        #region EVENTO CLICK: BOTON CANCELAR : FILTRO PRINCIPAL
        protected void ImgCancelEditDigitacion_Click1(object sender, ImageClickEventArgs e)
        {
            cancelarActivarbotones();
            InicializarFiltros();
            HabilitarObjetos();
        }
        //protected void ImgCancelEditDigitacion_Click(object sender, ImageClickEventArgs e)
        //{
        //    ImgCancelEditDigitacion.Visible = false;
        //    ImgEditDigitacion.Visible = true;
        //    ImgHabEditDigitacion.Visible = false;
        //    ImgUpdateDigitacion.Visible = false;
        //    ImgCancelDigitacion.Visible = true;
        //    HabilitarObjetos();
        //    GvDigitacion.DataBind();
        //    GVDigitacionPrecios.DataBind();
        //    GvDigitacionDG.DataBind();
        //    GvDigitacionAGCocina.DataBind();
        //    GvDigitacionMimaskot.DataBind();
        //    GvDigitacionPromGalletas.DataBind();
        //}
        #endregion EVENTO CLICK: BOTON CANCELAR : FILTRO PRINCIPAL
        #region EVENTO CLICK: BOTON ACTUALIZAR: FILTRO PRINCIPAL
        protected void ImgUpdateDigitacion_Click(object sender, ImageClickEventArgs e)
        {
            bool continuar = true;

            #region FORMATO_FINAL_SOD
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINAL_SOD"])
            {
                for (int i = 0; i <= GvDigitacion.Rows.Count - 1; i++)
                {
                    if (((TextBox)GvDigitacion.Rows[i].Cells[0].FindControl("TxtExhPrim")).Text == "" &&
                                   ((TextBox)GvDigitacion.Rows[i].Cells[0].FindControl("TxtExhSec")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {
                        EOPE_DigFORMATOFINALSOD oeaOPE_DigFORMATOFINALSOD = oOPE_DigFORMATOFINALSOD.ActualizarOPE_DigFORMATOFINALSOD(
                       Convert.ToInt64(((Label)GvDigitacion.Rows[i].Cells[0].FindControl("LblNo")).Text),
                        ((TextBox)GvDigitacion.Rows[i].Cells[0].FindControl("TxtExhPrim")).Text,
                        ((TextBox)GvDigitacion.Rows[i].Cells[0].FindControl("TxtExhSec")).Text,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    }
                }
                if (continuar == true)
                {
                    DataTable dtDigitacion = oOPE_DigFORMATOFINALSOD.ConsultarOPE_DigFORMATOFINALSOD(
                       TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                   Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                  TxtCliente.Text, Convert.ToInt64(CmbCiudad.SelectedValue));

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacion.DataSource = dtDigitacion;
                            GvDigitacion.DataBind();
                            GvDigitacion.SelectedIndex = -1;
                        }
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                }
            }
            #endregion

            #region FORMATO_FINALES_PRECIOS
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_PRECIOS"])
            {
                for (int i = 0; i <= GVDigitacionPrecios.Rows.Count - 1; i++)
                {
                    if (((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtCosto")).Text == "" &&
                                   ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventa")).Text == "" &&
                         ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtObservaciones")).Text == "" &&
                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventaUnid")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {
                        EOPE_DigFORMATOFINALESPRECIOS oeaOPE_DigFORMATOFINALESPRECIOS = oOPE_DigFORMATOFINALESPRECIOS.ActualizarOPE_DigFORMATOFINALESPRECIOS(
                        Convert.ToInt64(((Label)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("LblNo")).Text),
                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtCosto")).Text,
                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventa")).Text,
                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtObservaciones")).Text,
                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventaUnid")).Text,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    }
                }
                if (continuar == true)
                {
                    DataTable dtDigitacion = oOPE_DigFORMATOFINALESPRECIOS.ConsultarOPE_DigFORMATOFINALESPRECIOS(
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                         Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                        Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GVDigitacionPrecios.DataSource = dtDigitacion;
                            GVDigitacionPrecios.DataBind();
                            GVDigitacionPrecios.SelectedIndex = -1;
                        }
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                }
            }
            #endregion

            #region FORMATOS FINALES DG
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_DG"])
            {
                for (int i = 0; i <= GvDigitacionDG.Rows.Count - 1; i++)
                {
                    if (((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal1")).Text == "" &&
                                    ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal2")).Text == "" &&
                                    ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal3")).Text == "" &&
                                    ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal4")).Text == "" &&
                                    ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal5")).Text == "" &&
                                    ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtTotal")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {

                        EOPE_DigFORMATOFINALESDG oeaOPE_DigFORMATOFINALESDG = oOPE_DigFORMATOFINALESDG.ActualizarFormatoFinalesDG
                            (Convert.ToInt64(((Label)GvDigitacionDG.Rows[i].Cells[0].FindControl("LblNo")).Text),
                             ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal1")).Text,
                                      ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal2")).Text,
                                      ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal3")).Text,
                                      ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal4")).Text,
                                      ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtLocal5")).Text,
                                      ((TextBox)GvDigitacionDG.Rows[i].Cells[0].FindControl("TxtTotal")).Text,
                                        Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    }
                }
                if (continuar == true)
                {
                    DataTable dtDigitacion = oOPE_DigFORMATOFINALESDG.ConsultarFormatoFinalesDG(
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                         Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                        Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionDG.DataSource = dtDigitacion;
                            GvDigitacionDG.DataBind();
                            GvDigitacionDG.SelectedIndex = -1;
                        }
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                }
            }
            #endregion

            #region FORMATOS AG COCINA
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_AGCOCINA"])
            {
                for (int i = 0; i <= GvDigitacionAGCocina.Rows.Count - 1; i++)
                {
                    if (((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtPrecio")).Text == "" &&
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockini")).Text == "" &&
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtIngresos")).Text == "" &&
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockTot")).Text == "" &&
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockFin")).Text == "" &&
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("Txtventas")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {

                        EOPE_DigFORMATOAGCOCINA oeaEOPE_DigFORMATOAGCOCINA = oOPE_DigFORMATOAGCOCINA.ActualizarFormatoAGCOCINA(
                            Convert.ToInt64(((Label)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("LblNo")).Text),
                             ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtPrecio")).Text,
                                      ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockini")).Text,
                                      ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtIngresos")).Text,
                                      ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockTot")).Text,
                                      ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockFin")).Text,
                                      ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("Txtventas")).Text,
                                        Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    }
                }
                if (continuar == true)
                {


                    DataTable dtDigitacion = oOPE_DigFORMATOAGCOCINA.ConsultarFormatoAGCOCINA(
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                    Convert.ToDateTime(TxtFecha.Text), Convert.ToDateTime(TxtFechaFin.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                   Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);



                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionAGCocina.DataSource = dtDigitacion;
                            GvDigitacionAGCocina.DataBind();
                            GvDigitacionAGCocina.SelectedIndex = -1;
                        }
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                }
            }
            #endregion

            #region FORMATOS MIMASKOT
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_MIMASKOT"])
            {
                for (int i = 0; i <= GvDigitacionMimaskot.Rows.Count - 1; i++)
                {
                    if (((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockini")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtIngresos")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockTot")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockFin")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("Txtventas")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPreReventa")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtMejorpreRev")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPrecioCsto")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {

                        //CAMBIAR POR METODO PARA ACTUALIZAR EN MIMASKOT

                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_MIMASKOT",
                            Convert.ToInt64(((Label)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("LblNo")).Text),
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockini")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtIngresos")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockTot")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockFin")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("Txtventas")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPreReventa")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtMejorpreRev")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPrecioCsto")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    }
                }
                if (continuar == true)
                {

                    // INVOCAR METODO DE CONSULTA DE MIMASKOT Y ELIMINAR COMENTARIO ABAJO 

                    DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_MIMASKOT",
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                    Convert.ToInt32(CmbOperativo.SelectedValue), TxtLugarDireccion.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionMimaskot.DataSource = dtDigitacion;
                            GvDigitacionMimaskot.DataBind();
                            GvDigitacionMimaskot.SelectedIndex = -1;
                        }
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                }
            }
            #endregion

            #region FORMATO PRECIOS CUIDADO DEL CABELLO
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PRECIOS_CUIDADOCABELLO"])
            {
                for (int i = 0; i <= GVDigitacionPrecios.Rows.Count - 1; i++)
                {
                    if (((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtCosto")).Text == "" &&
                            ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventa")).Text == "" &&
                            ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventaUnid")).Text == "" &&
                            ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtObservaciones")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {
                        EOPE_DigFORMATOFINALESPRECIOS oeaOPE_DigFORMATOFINALESPRECIOS = oOPE_DigFORMATOFINALESPRECIOS.ActualizarOPE_DigFORMATOFINALESPRECIOS(
                        Convert.ToInt64(((Label)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("LblNo")).Text),
                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtCosto")).Text,
                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventa")).Text,
                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtObservaciones")).Text,
                        ((TextBox)GVDigitacionPrecios.Rows[i].Cells[0].FindControl("TxtReventaUnid")).Text,
                        Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    }
                }
                if (continuar == true)
                {
                    DataTable dtDigitacion = oOPE_DigFORMATOFINALESPRECIOS.ConsultarOPE_DigFORMATOFINALESPRECIOS(
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                         Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                        Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GVDigitacionPrecios.DataSource = dtDigitacion;
                            GVDigitacionPrecios.DataBind();
                            GVDigitacionPrecios.SelectedIndex = -1;
                        }
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                }
            }
            #endregion

            #region FORMATOS DIAS GIRO CUIDADO DEL CABELLO
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_DIASGIRO"])
            {
                for (int i = 0; i <= GvDigitacionAGCocina.Rows.Count - 1; i++)
                {
                    if (((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtPrecio")).Text == "" &&
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockini")).Text == "" &&
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtIngresos")).Text == "" &&
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockTot")).Text == "" &&
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockFin")).Text == "" &&
                                    ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("Txtventas")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {

                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_DIASGIRO",
                            Convert.ToInt64(((Label)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("LblNo")).Text),
                             ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtPrecio")).Text,
                                      ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockini")).Text,
                                      ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtIngresos")).Text,
                                      ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockTot")).Text,
                                      ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("TxtStockFin")).Text,
                                      ((TextBox)GvDigitacionAGCocina.Rows[i].Cells[0].FindControl("Txtventas")).Text,
                                        Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    }
                }
                if (continuar == true)
                {


                    DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_DIASGIRO",
                      TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                      Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                     Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);



                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionAGCocina.DataSource = dtDigitacion;
                            GvDigitacionAGCocina.DataBind();
                            GvDigitacionAGCocina.SelectedIndex = -1;
                        }
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                }
            }
            #endregion

            #region FORMATOS NUTRICAN
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_NUTRICAN"])
            {
                for (int i = 0; i <= GvDigitacionMimaskot.Rows.Count - 1; i++)
                {
                    if (((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockini")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtIngresos")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockTot")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockFin")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("Txtventas")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPreReventa")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtMejorpreRev")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPrecioCsto")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_MIMASKOT",
                            Convert.ToInt64(((Label)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("LblNo")).Text),
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockini")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtIngresos")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockTot")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockFin")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("Txtventas")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPreReventa")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtMejorpreRev")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPrecioCsto")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    }
                }
                if (continuar == true)
                {
                    DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_MIMASKOT",
                        TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                    Convert.ToInt32(CmbOperativo.SelectedValue), TxtLugarDireccion.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionMimaskot.DataSource = dtDigitacion;
                            GvDigitacionMimaskot.DataBind();
                            GvDigitacionMimaskot.SelectedIndex = -1;
                        }
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                }
            }
            #endregion

            #region FORMATOS PROMOCION GALLETAS
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PROMOCION_GALLETAS"])
            {
                for (int i = 0; i <= GvDigitacionPromGalletas.Rows.Count - 1; i++)
                {
                    if (((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtStockini")).Text == "" &&
                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtIngresos_1sem")).Text == "" &&
                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtIngresos_2sem")).Text == "" &&
                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtTotIngresos")).Text == "" &&
                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtStockTot")).Text == "" &&
                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtStockFin")).Text == "" &&
                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("Txtventas")).Text == "" &&
                        ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtPrecio")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_PROMGALLETAS",
                            Convert.ToInt64(((Label)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("LblNo")).Text),
                            ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtStockini")).Text,
                            ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtIngresos_1sem")).Text,
                            ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtIngresos_2sem")).Text,
                            ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtTotIngresos")).Text,
                            ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtStockTot")).Text,
                            ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtStockFin")).Text,
                            ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("Txtventas")).Text,
                            ((TextBox)GvDigitacionPromGalletas.Rows[i].Cells[0].FindControl("TxtPrecio")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    }
                }
                if (continuar == true)
                {

                    DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_PROMGALLETAS",
                       TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                       Convert.ToDateTime(TxtFechaFin.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                       CmbMercado.Text, Convert.ToInt64(CmbCiudad.SelectedValue), TxtCliente.Text);

                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionPromGalletas.DataSource = dtDigitacion;
                            GvDigitacionPromGalletas.DataBind();
                            GvDigitacionPromGalletas.SelectedIndex = -1;
                        }
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                }
            }
            #endregion

            #region FORMATOS AB MASCOTAS - INFORME SEMANAL
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASINFSEM"])
            {
                for (int i = 0; i <= GvDigitacionMimaskot.Rows.Count - 1; i++)
                {
                    if (((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockini")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtIngresos")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockTot")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockFin")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("Txtventas")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPreReventa")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtMejorpreRev")).Text == "" &&
                        ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPrecioCsto")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_ABMASCOTAS",
                            Convert.ToInt64(((Label)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("LblNo")).Text),
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockini")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtIngresos")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockTot")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtStockFin")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("Txtventas")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPreReventa")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtMejorpreRev")).Text,
                            ((TextBox)GvDigitacionMimaskot.Rows[i].Cells[0].FindControl("TxtPrecioCsto")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    }
                }
                if (continuar == true)
                {

                    DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_ABMASCOTAS",
                     TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                     Convert.ToDateTime(TxtFechaFin.Text), Convert.ToInt32(CmbOperativo.SelectedValue), TxtLugarDireccion.Text, TxtCliente.Text);


                    if (dtDigitacion != null)
                    {
                        if (dtDigitacion.Rows.Count > 0)
                        {
                            ImgEditDigitacion.Visible = false;
                            ImgUpdateDigitacion.Visible = true;
                            ImgCancelEditDigitacion.Visible = true;
                            ImgCancelDigitacion.Visible = false;
                            GvDigitacionMimaskot.DataSource = dtDigitacion;
                            GvDigitacionMimaskot.DataBind();
                            GvDigitacionMimaskot.SelectedIndex = -1;
                        }
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                }
            }
            #endregion

            #region FORMATO_ABMASCOTASHOJACONTROL
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASHOJACONTROL"])
            {

                #region ACTUALIZAR CANTIDADES EN GRILLA(GvDigitacionHojaControlAbarrotes) PRODUCTOS - CUANDO PRESIONA BOTON ACTUALIZAR
                for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                {

                    if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro donde se muestran los Productos debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }

                    else
                    {
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_HCCANJEMULTICATEGORIA",
                            Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNumDet")).Text),
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    }

                }
                #endregion ACTUALIZAR CANTIDADES EN GRILLA(GvDigitacionHojaControlAbarrotes) PRODUCTOS - CUANDO PRESIONA BOTON ACTUALIZAR

                #region ACTUALIZAR CANTIDADES EN PREMIOS
                for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                {
                    if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro donde se muestran los Premios debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {//CAMBIAR POR EL METODO ACTUALIZAR DE HCCCANJEMULTICATEGORIA PREMIO
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_HCCANJEMULTICATEGORIA_PREMIO",
                            Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_ID_HCCCABxPremio")).Text),
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    }

                }
                #endregion ACTUALIZAR CANTIDADES EN PREMIOS

                #region MENSAJE ACTUALIZACION EXITOSA
                if (continuar == true)
                {

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                    GvDigitacionHojaControlAbarrotes.Enabled = false;
                    GvDigitacionHojaControlAbarrotes_Premios.Enabled = false;
                    ImgHabEditDigitacion.Visible = true;
                    ImgUpdateDigitacion.Visible = false;
                }
                #endregion MENSAJE ACTUALIZACION EXITOSA
            }
            #endregion

            #region FORMATOS HDCCANJEMULTICATEGORIA - CREADO 07/07/2011 psalas
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
            {

                #region ACTUALIZAR CANTIDADES EN GRILLA(GvDigitacionHojaControlAbarrotes) PRODUCTOS - CUANDO PRESIONA BOTON ACTUALIZAR
                for (int i = 0; i <=GvDigitacionHojaControlAbarrotes.Rows.Count -1; i++)
                {
                   
                    if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text == "" )
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro donde se muestran los Productos debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                   
                    else
                    {
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_HCCANJEMULTICATEGORIA",
                            Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNumDet")).Text),
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    }

                }
                #endregion ACTUALIZAR CANTIDADES EN GRILLA(GvDigitacionHojaControlAbarrotes) PRODUCTOS - CUANDO PRESIONA BOTON ACTUALIZAR

                #region ACTUALIZAR CANTIDADES EN PREMIOS
                for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++) {
                    if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro donde se muestran los Premios debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {//CAMBIAR POR EL METODO ACTUALIZAR DE HCCCANJEMULTICATEGORIA PREMIO
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_HCCANJEMULTICATEGORIA_PREMIO",
                            Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_ID_HCCCABxPremio")).Text),
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    }
                
                }
                #endregion ACTUALIZAR CANTIDADES EN PREMIOS

                #region MENSAJE ACTUALIZACION EXITOSA
                if (continuar == true)
                    {

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se Actualizó la información correctamente";
                        Mensajes_Usuario();
                        GvDigitacionHojaControlAbarrotes.Enabled = false;
                        GvDigitacionHojaControlAbarrotes_Premios.Enabled = false;
                        ImgHabEditDigitacion.Visible = true;
                        ImgUpdateDigitacion.Visible = false;
                    }
                #endregion MENSAJE ACTUALIZACION EXITOSA
            }
            #endregion

            #region FORMATO_RESUMEN_CANJE_ANUA
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_ANUA"])
            {
                #region ACTUALIZAR CANTIDADES EN GRILLA(GvDigitacionHojaControlAbarrotes) PRODUCTOS - CUANDO PRESIONA BOTON ACTUALIZAR
                for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                {

                    if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro donde se muestran los Productos debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }

                    else
                    {
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_HCCANJEMULTICATEGORIA",
                            Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNumDet")).Text),
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    }

                }
                #endregion ACTUALIZAR CANTIDADES EN GRILLA(GvDigitacionHojaControlAbarrotes) PRODUCTOS - CUANDO PRESIONA BOTON ACTUALIZAR

                #region ACTUALIZAR CANTIDADES EN PREMIOS
                for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                {
                    if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro donde se muestran los Premios debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {//CAMBIAR POR EL METODO ACTUALIZAR DE HCCCANJEMULTICATEGORIA PREMIO
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_HCCANJEMULTICATEGORIA_PREMIO",
                            Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_ID_HCCCABxPremio")).Text),
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    }

                }
                #endregion ACTUALIZAR CANTIDADES EN PREMIOS

                #region MENSAJE ACTUALIZACION EXITOSA
                if (continuar == true)
                {

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                    GvDigitacionHojaControlAbarrotes.Enabled = false;
                    GvDigitacionHojaControlAbarrotes_Premios.Enabled = false;
                    ImgHabEditDigitacion.Visible = true;
                    ImgUpdateDigitacion.Visible = false;
                }
                #endregion MENSAJE ACTUALIZACION EXITOSA
            }
            #endregion

            #region FORMATO_CANJE_ANUA
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_CANJE_ANUA"])
            {
                #region ACTUALIZAR CANTIDADES EN GRILLA(GvDigitacionHojaControlAbarrotes) PRODUCTOS - CUANDO PRESIONA BOTON ACTUALIZAR
                for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                {

                    if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro donde se muestran los Productos debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }

                    else
                    {
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_HCCANJEMULTICATEGORIA",
                            Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNumDet")).Text),
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    }

                }
                #endregion ACTUALIZAR CANTIDADES EN GRILLA(GvDigitacionHojaControlAbarrotes) PRODUCTOS - CUANDO PRESIONA BOTON ACTUALIZAR

                #region ACTUALIZAR CANTIDADES EN PREMIOS
                for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                {
                    if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro donde se muestran los Premios debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {//CAMBIAR POR EL METODO ACTUALIZAR DE HCCCANJEMULTICATEGORIA PREMIO
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_HCCANJEMULTICATEGORIA_PREMIO",
                            Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_ID_HCCCABxPremio")).Text),
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    }

                }
                #endregion ACTUALIZAR CANTIDADES EN PREMIOS

                #region MENSAJE ACTUALIZACION EXITOSA
                if (continuar == true)
                {

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                    GvDigitacionHojaControlAbarrotes.Enabled = false;
                    GvDigitacionHojaControlAbarrotes_Premios.Enabled = false;
                    ImgHabEditDigitacion.Visible = true;
                    ImgUpdateDigitacion.Visible = false;
                }
                #endregion MENSAJE ACTUALIZACION EXITOSA
            }
            #endregion

            #region FORMATO_HDC_CANJE -- MODIFICAR SEGUN FORMATO
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE"])
            {

            }
            #endregion

            #region FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar -- MODIFICAR SEGUN FORMATO
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar"])
            {

            }
            #endregion

            #region FORMATO_RESUMEN_CANJE_PLUSBELLE
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_PLUSBELLE"])
            {
                #region ACTUALIZAR CANTIDADES EN GRILLA(GvDigitacionHojaControlAbarrotes) PRODUCTOS - CUANDO PRESIONA BOTON ACTUALIZAR
                for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                {

                    if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro donde se muestran los Productos debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }

                    else
                    {
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_HCCANJEMULTICATEGORIA",
                            Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNumDet")).Text),
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    }

                }
                #endregion ACTUALIZAR CANTIDADES EN GRILLA(GvDigitacionHojaControlAbarrotes) PRODUCTOS - CUANDO PRESIONA BOTON ACTUALIZAR

                #region ACTUALIZAR CANTIDADES EN PREMIOS
                for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                {
                    if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text == "")
                    {
                        continuar = false;
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "En cada registro donde se muestran los Premios debe aparecer por lo menos un valor en los campos editables del formulario. Por favor verifique";
                        Mensajes_Usuario();
                    }
                    else
                    {//CAMBIAR POR EL METODO ACTUALIZAR DE HCCCANJEMULTICATEGORIA PREMIO
                        oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ACTUALIZAR_FORMATO_HCCANJEMULTICATEGORIA_PREMIO",
                            Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_ID_HCCCABxPremio")).Text),
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text,
                            Convert.ToString(this.Session["sUser"]), DateTime.Now);

                    }

                }
                #endregion ACTUALIZAR CANTIDADES EN PREMIOS

                #region MENSAJE ACTUALIZACION EXITOSA
                if (continuar == true)
                {

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se Actualizó la información correctamente";
                    Mensajes_Usuario();
                    GvDigitacionHojaControlAbarrotes.Enabled = false;
                    GvDigitacionHojaControlAbarrotes_Premios.Enabled = false;
                    ImgHabEditDigitacion.Visible = true;
                    ImgUpdateDigitacion.Visible = false;
                }
                #endregion MENSAJE ACTUALIZACION EXITOSA
            }
            #endregion

            #region FORMATO COMPETENCIA_ANUA -- MODIFICAR SEGUN FORMATO
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_ANUA"])
            {

            }
            #endregion

            #region FORMATO COMPETENCIA_MAYORISTA -- MODIFICAR SEGUN FORMATO
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_MAYORISTA"])
            {
            }
            #endregion
        }
        #endregion EVENTO CLICK: BOTON ACTUALIZAR: FILTRO PRINCIPAL
        #region EVENTO CLICK: BOTON GUARDAR: FILTRO PRINCIPAL
        protected void ImgGuardarCabeceraOpeDigitacion_Click(object sender, ImageClickEventArgs e)
        {
            bool continuar = true;
            bool continuar1 = false;

            if (ValidainfoFiltros(continuar))
            {


                #region FORMATO_ABMASCOTASHOJACONTROL

                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASHOJACONTROL"])
                {
                    continuar1 = true;

                    #region INSERTANDO CABECERA
                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_ABMASCOTASHOJACONTROL_CAB",
                        TxtCodPlanning.Text,
                        Convert.ToInt32(CmbFormato.Text),
                        Convert.ToInt32(CmbOperativo.Text),
                        Convert.ToInt32(CmbZonaMayor.Text),
                        TxtCliente.Text,
                        Convert.ToDateTime(TxtFecha.Text),
                        Convert.ToDateTime(TxtFechaFin.Text),
                        CmbCategoria.Text,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        "");
                    #endregion

                    if (continuar == true && continuar1 == false)
                    {
                        #region MENSAJE DE REGISTRO DE CABECERA FALLIDO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                        #endregion
                    }

                    if (continuar == true && continuar1 == true)
                    {
                        #region MENSAJE DE REGISTRO DE CABECERA EXITOSO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                        #endregion

                        #region FILTROS SUPERCABECERA: ENABLED
                        CmbCampaña.Enabled = false;
                        CmbFormato.Enabled = false;
                        #endregion FILTROS SUPERCABECERA: ENABLED

                        #region FILTROS CABECERA : ENABLED - INICIO
                        Filtros.Disabled = true;
                        #endregion FILTROS CONTENDORES:ENABLED - CABECERA - FIN

                        #region FILTROS DETALLE - INICIO
                        FiltrosDetalle.Visible = true;
                        #endregion FILTROS DETALLE - FIN

                        #region VISIBILIDAD DE BOTONES TRUE/FALSE - INICIO
                        #region MOSTRAR/OCULTAR - BOTONES SUPERCABECERA - INICIO
                        ImgNewDigitacion.Visible = false;
                        ImgSearchDigitacion.Visible = false;
                        ImageButton1.Visible = false;
                        ImgCancelDigitacion.Visible = false;
                        #endregion MOSTRAR/OCULTAR - BOTONES SUPERCABECERA - FIN

                        #region MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - INICIO
                        ImgEditDigitacion.Visible = false;
                        ImgHabEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = false;
                        ImgCancelEditDigitacion.Visible = false;
                        ImgGuardarCabeceraOpeDigitacion.Visible = false;
                        #endregion MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - FIN

                        #region MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - INICIO

                        ImgGuardarDetalle.Visible = true;
                        ImgCancelarDetalle.Visible = true;

                        #endregion BOTONES GUARDAR Y CANCELAR - DETALLE - FIN

                        #endregion VISIBILIDAD DE BOTONES TRUE/FALSE - FIN

                        #region METODOS AUXILIARES - INICIO
                        #region OBTENER Y COLOCAR ID DE CABECERA EN TEXTBOX OCULTO PARA GUARDAR EL DETALLE MAS ADELANTE
                        DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_OBTENER_CABECERA_HCCANJEMULTI");
                        Txt_ID_HDCanjeMulticategoriaCAB.Text = dt.Rows[0]["CAB_ID"].ToString().Trim();
                        #endregion

                        #region GENERAR U OBTENER EL NUMERO DE REGISTRO EN EL DETALLE(NO ES EL ID) EN TEXTBOX OCULTO PARA GUARDAR MAS ADELANTE
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        if (ds2.Tables[0].Rows.Count > 1)
                        {
                            string aux = ds2.Tables[0].Rows[0]["DET_ID"].ToString().Trim();
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        else
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        #endregion
                        #endregion METODOS AUXILIARES - FIN

                        #region MUESTRA GRILLA DE PRODUCTOS - INICIO

                        DataSet dsHojaControlCanjeAbarrotesMulticategoria = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIGITACION_FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA",
                        CmbCategoria.SelectedValue,
                        TxtCodPlanning.Text,
                        Convert.ToInt32(CmbFormato.SelectedValue));
                        
                        if (this.Session["Opcion"].ToString().Trim() == "New")
                        {

                            if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[0].Rows.Count > 0)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupConfirm";
                                this.Session["mensaje"] = "Correcto, Si hay informacion para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR GRILLA - PRODUCTOS - INICIO

                                GvDigitacionHojaControlAbarrotes.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[0];
                                GvDigitacionHojaControlAbarrotes.DataBind();
                                GvDigitacionHojaControlAbarrotes.Visible = true;
                                GvDigitacionHojaControlAbarrotes.Enabled = true;
                                #region COLUMNAS VISIBLES - INICIO
                                GvDigitacionHojaControlAbarrotes.Columns[8].Visible = false;
                                #endregion COLUMNAS VISIBLES - FIN

                                #endregion MOSTRAR GRILLA - PRODUCTOS - FIN


                            }
                            else
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "No hay información para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - INICIO
                                GvDigitacionHojaControlAbarrotes.Visible = false;
                                #endregion MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - FIN

                                #region MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - INICIO
                                ImgGuardarDetalle.Visible = false;
                                ImgCancelarDetalle.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - FIN

                                #region MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - INICIO
                                ImgGuardarCabeceraOpeDigitacion.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - FIN
                                //ImgGuardarDetalle
                            }


                        }
                        #endregion MUESTRA GRILLA DE PRODUCTOS - FIN

                        #region MUESTRA GRILLA DE PREMIOS - INICIO
                        if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[1].Rows.Count > 0)
                        {
                            GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[1];
                            GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                            #region GRIDVIEW VISIBLE:TRUE - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                            GvDigitacionHojaControlAbarrotes_Premios.Enabled = true;
                            #endregion GRIDVIEW VISIBLE:FALSE - FIN

                            #region COLUMNAS A MOSTRAR EN GRILLA [4] - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = false;
                            #endregion COLUMNAS A MOSTRAR EN GRILLA - FIN

                        }
                        else
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No hay información para digitar";
                            Mensajes_Usuario();

                            GvDigitacionHojaControlAbarrotes_Premios.Visible = false;

                            //ImgGuardarDetalle.Visible = false;
                            //ImgGuardarDetalle
                        }
                        #endregion MUESTRA SI HDCCANJEMULTICATEGORIA TIENE PREMIOS - FIN

                        dsHojaControlCanjeAbarrotesMulticategoria = null;

                    }
                }

                #endregion FORMATO_ABMASCOTASHOJACONTROL
                #region FORMATO HDC_ABARROTES_ROTACIONMULTICATEGORIA, ID=12 - INSERTAR CABECERA - INICIO

                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
                {
                    continuar1 = true;
                    
                    #region INSERTANDO CABECERA
                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_CAB",
                        TxtCodPlanning.Text,
                        Convert.ToInt32(CmbFormato.Text),
                        Convert.ToInt32(CmbOperativo.Text),
                        Convert.ToInt32(CmbZonaMayor.Text),
                        TxtCliente.Text,
                        Convert.ToDateTime(TxtFecha.Text),
                        CmbCategoria.Text,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        "");
                    #endregion

                    if (continuar == true && continuar1 == false)
                    {
                        #region MENSAJE DE REGISTRO DE CABECERA FALLIDO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                        #endregion
                    }

                    if (continuar == true && continuar1 == true)
                    {
                        #region MENSAJE DE REGISTRO DE CABECERA EXITOSO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                        #endregion

                        #region FILTROS SUPERCABECERA: ENABLED
                        CmbCampaña.Enabled = false;
                        CmbFormato.Enabled = false;
                        #endregion FILTROS SUPERCABECERA: ENABLED

                        #region FILTROS CABECERA : ENABLED - INICIO
                        Filtros.Disabled = true;
                        #endregion FILTROS CONTENDORES:ENABLED - CABECERA - FIN

                        #region FILTROS DETALLE - INICIO
                        FiltrosDetalle.Visible = true;
                        #endregion FILTROS DETALLE - FIN

                        #region VISIBILIDAD DE BOTONES TRUE/FALSE - INICIO
                        #region MOSTRAR/OCULTAR - BOTONES SUPERCABECERA - INICIO
                        ImgNewDigitacion.Visible = false;
                        ImgSearchDigitacion.Visible = false;
                        ImageButton1.Visible = false;
                        ImgCancelDigitacion.Visible = false;
                        #endregion MOSTRAR/OCULTAR - BOTONES SUPERCABECERA - FIN

                        #region MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - INICIO
                        ImgEditDigitacion.Visible = false;
                        ImgHabEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = false;
                        ImgCancelEditDigitacion.Visible = false;
                        ImgGuardarCabeceraOpeDigitacion.Visible = false;
                        #endregion MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - FIN

                        #region MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - INICIO

                        ImgGuardarDetalle.Visible = true;
                        ImgCancelarDetalle.Visible = true;

                        #endregion BOTONES GUARDAR Y CANCELAR - DETALLE - FIN

                        #endregion VISIBILIDAD DE BOTONES TRUE/FALSE - FIN

                        #region METODOS AUXILIARES - INICIO
                        #region OBTENER Y COLOCAR ID DE CABECERA EN TEXTBOX OCULTO PARA GUARDAR EL DETALLE MAS ADELANTE
                        DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_OBTENER_CABECERA_HCCANJEMULTI");
                        Txt_ID_HDCanjeMulticategoriaCAB.Text = dt.Rows[0]["CAB_ID"].ToString().Trim();
                        #endregion

                        #region GENERAR U OBTENER EL NUMERO DE REGISTRO EN EL DETALLE(NO ES EL ID) EN TEXTBOX OCULTO PARA GUARDAR MAS ADELANTE
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        if (ds2.Tables[0].Rows.Count > 1)
                        {
                            string aux = ds2.Tables[0].Rows[0]["DET_ID"].ToString().Trim();
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        else
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        #endregion
                        #endregion METODOS AUXILIARES - FIN

                        #region MUESTRA GRILLA DE PRODUCTOS - INICIO

                        DataSet dsHojaControlCanjeAbarrotesMulticategoria = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIGITACION_FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA", 
                            CmbCategoria.SelectedValue, 
                            TxtCodPlanning.Text, 
                            Convert.ToInt32(CmbFormato.SelectedValue));

                        if (this.Session["Opcion"].ToString().Trim() == "New")
                        {

                            if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[0].Rows.Count > 0)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupConfirm";
                                this.Session["mensaje"] = "Correcto, Si hay informacion para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR GRILLA - PRODUCTOS - INICIO

                                GvDigitacionHojaControlAbarrotes.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[0];
                                GvDigitacionHojaControlAbarrotes.DataBind();
                                GvDigitacionHojaControlAbarrotes.Visible = true;
                                GvDigitacionHojaControlAbarrotes.Enabled = true;
                                #region COLUMNAS VISIBLES - INICIO
                                GvDigitacionHojaControlAbarrotes.Columns[8].Visible = false;
                                #endregion COLUMNAS VISIBLES - FIN

                                #endregion MOSTRAR GRILLA - PRODUCTOS - FIN


                            }
                            else
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "No hay información para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - INICIO
                                GvDigitacionHojaControlAbarrotes.Visible = false;
                                #endregion MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - FIN

                                #region MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - INICIO
                                ImgGuardarDetalle.Visible = false;
                                ImgCancelarDetalle.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - FIN

                                #region MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - INICIO
                                ImgGuardarCabeceraOpeDigitacion.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - FIN
                                //ImgGuardarDetalle
                            }


                        }
                        #endregion MUESTRA GRILLA DE PRODUCTOS - FIN

                        #region MUESTRA GRILLA DE PREMIOS - INICIO
                        if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[1].Rows.Count > 0)
                        {
                            GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[1];
                            GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                            #region GRIDVIEW VISIBLE:TRUE - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                            GvDigitacionHojaControlAbarrotes_Premios.Enabled = true;
                            #endregion GRIDVIEW VISIBLE:FALSE - FIN

                            #region COLUMNAS A MOSTRAR EN GRILLA [4] - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = false;
                            #endregion COLUMNAS A MOSTRAR EN GRILLA - FIN

                        }
                        else
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No hay información para digitar";
                            Mensajes_Usuario();

                            GvDigitacionHojaControlAbarrotes_Premios.Visible = false;

                            //ImgGuardarDetalle.Visible = false;
                            //ImgGuardarDetalle
                        }
                        #endregion MUESTRA SI HDCCANJEMULTICATEGORIA TIENE PREMIOS - FIN

                        dsHojaControlCanjeAbarrotesMulticategoria = null;

                    }
                }

                #endregion FORMATO HDC_ABARROTES_ROTACIONMULTICATEGORIA - GUARDAR CABECERA - INICIO
                #region FORMATO RESUMEN DE CANJE ANUA, ID=13 - OK

                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_ANUA"])
                {
                    continuar1 = true;

                    #region INSERTANDO CABECERA
                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_RESUMEN_CANJE_ANUA",
                        TxtCodPlanning.Text,
                        Convert.ToInt32(CmbFormato.Text),
                        Convert.ToDateTime(TxtFecha.Text),
                        Convert.ToDateTime(TxtFechaFin.Text),
                        Convert.ToInt32(CmbOperativo.Text),
                        Convert.ToInt32(CmbZonaMayor.Text),
                        CmbCategoria.Text,                        
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        "");
                    #endregion INSERTANDO CABECERA

                    if (continuar == true && continuar1 == false)
                    {
                        #region MENSAJE DE REGISTRO DE CABECERA FALLIDO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA FALLIDO
                    }
                    if (continuar == true && continuar1 == true)
                    {

                        #region MENSAJE DE REGISTRO DE CABECERA EXITOSO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA EXITOSO

                        #region FILTROS SUPERCABECERA: ENABLED
                        CmbCampaña.Enabled = false;
                        CmbFormato.Enabled = false;
                        #endregion FILTROS SUPERCABECERA: ENABLED
                        #region FILTROS CABECERA : ENABLED - INICIO
                        Filtros.Disabled = true;
                        #endregion FILTROS CONTENDORES:ENABLED - CABECERA - FIN
                        #region FILTROS DETALLE - INICIO
                        FiltrosDetalle.Visible = true;
                        #endregion FILTROS DETALLE - FIN

                        #region BOTONES TRUE/FALSE - INICIO

                        #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - - INICIO
                        ImgNewDigitacion.Visible = false;
                        ImgSearchDigitacion.Visible = false;
                        ImageButton1.Visible = false;
                        ImgCancelDigitacion.Visible = false;
                        #endregion BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - FIN
                        #region BOTONES CABECERA  -MOSTRAR/OCULTAR - : GUARDAR,  - INICIO
                        ImgEditDigitacion.Visible = false;
                        ImgHabEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = false;
                        ImgCancelEditDigitacion.Visible = false;
                        ImgGuardarCabeceraOpeDigitacion.Visible = false;
                        #endregion BOTONES CABECERA  - MOSTRAR/OCULTAR - : GUARDAR,  - FIN
                        #region BOTONES DETALLE - MOSTRAR/OCULTAR - : GUARDAR, CANCELAR - INICIO

                        ImgGuardarDetalle.Visible = true;
                        ImgCancelarDetalle.Visible = true;

                        #endregion BOTONES DETALLE - GUARDAR Y CANCELAR - FIN

                        #endregion BOTONES TRUE/FALSE - FIN

                        #region METODOS AUXILIARES - INICIO
                        #region OBTENER Y COLOCAR ID DE CABECERA EN TEXTBOX OCULTO PARA GUARDAR EL DETALLE MAS ADELANTE
                        DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_OBTENER_CABECERA_HCCANJEMULTI");
                        Txt_ID_HDCanjeMulticategoriaCAB.Text = dt.Rows[0]["CAB_ID"].ToString().Trim();
                        #endregion

                        #region GENERAR U OBTENER EL NUMERO DE REGISTRO EN EL DETALLE(NO ES EL ID) EN TEXTBOX OCULTO PARA GUARDAR MAS ADELANTE
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        if (ds2.Tables[0].Rows.Count > 1)
                        {
                            string aux = ds2.Tables[0].Rows[0]["DET_ID"].ToString().Trim();
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        else
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        #endregion
                        #endregion METODOS AUXILIARES - FIN

                        #region MUESTRA GRILLA DE PRODUCTOS - INICIO

                        DataSet dsHojaControlCanjeAbarrotesMulticategoria = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIGITACION_FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA", 
                            CmbCategoria.SelectedValue,
                            TxtCodPlanning.Text, 
                            Convert.ToInt32(CmbFormato.SelectedValue));

                        if (this.Session["Opcion"].ToString().Trim() == "New")
                        {

                            if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[0].Rows.Count > 0)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupConfirm";
                                this.Session["mensaje"] = "Correcto, Si hay informacion para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR GRILLA - PRODUCTOS - INICIO

                                GvDigitacionHojaControlAbarrotes.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[0];
                                GvDigitacionHojaControlAbarrotes.DataBind();
                                GvDigitacionHojaControlAbarrotes.Visible = true;
                                GvDigitacionHojaControlAbarrotes.Enabled = true;

                                #region COLUMNAS VISIBLES - INICIO
                                GvDigitacionHojaControlAbarrotes.Columns[8].Visible = false;
                                #endregion COLUMNAS VISIBLES - FIN

                                #endregion MOSTRAR GRILLA - PRODUCTOS - FIN


                            }
                            else
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "No hay información para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - INICIO
                                GvDigitacionHojaControlAbarrotes.Visible = false;
                                #endregion MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - FIN
                                GvDigitacionHojaControlAbarrotes_Premios.Enabled = true;

                                #region MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - INICIO
                                ImgGuardarDetalle.Visible = false;
                                ImgCancelarDetalle.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - FIN

                                #region MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - INICIO
                                ImgGuardarCabeceraOpeDigitacion.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - FIN
                                //ImgGuardarDetalle
                            }


                        }
                        #endregion MUESTRA GRILLA DE PRODUCTOS - FIN
                        #region MUESTRA GRILLA DE PREMIOS - INICIO
                        if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[1].Rows.Count > 0)
                        {
                            GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[1];
                            GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                            #region GRIDVIEW VISIBLE:TRUE - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                            #endregion GRIDVIEW VISIBLE:FALSE - FIN
                            GvDigitacionHojaControlAbarrotes_Premios.Enabled = true;
                            #region COLUMNAS A MOSTRAR EN GRILLA [4] - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = false;
                            #endregion COLUMNAS A MOSTRAR EN GRILLA - FIN

                        }
                        else
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No hay información para digitar";
                            Mensajes_Usuario();

                            GvDigitacionHojaControlAbarrotes_Premios.Visible = false;

                            //ImgGuardarDetalle.Visible = false;
                            //ImgGuardarDetalle
                        }
                        #endregion MUESTRA SI HDCCANJEMULTICATEGORIA TIENE PREMIOS - FIN

                        dsHojaControlCanjeAbarrotesMulticategoria = null;

                    }
                }

                #endregion FORMATO RESUMEN DE CANJE ANUA, ID=13
                #region  FORMATO CANJE_ANUA - GUARDAR CABECERA, ID=14 - OK


                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_CANJE_ANUA"])
                {
                    continuar1 = true;

                    #region INSERTANDO CABECERA
                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_CANJE_ANUA",
                        TxtCodPlanning.Text,
                        Convert.ToInt32(CmbFormato.Text),
                        Convert.ToDateTime(TxtFecha.Text),
                        Convert.ToInt32(CmbOperativo.Text),
                        Convert.ToInt32(CmbZonaMayor.Text),
                        TxtCliente.Text,
                        CmbCategoria.Text,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        "");
                    #endregion INSERTANDO CABECERA

                    if (continuar == true && continuar1 == false)
                    {
                        #region MENSAJE DE REGISTRO DE CABECERA FALLIDO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA FALLIDO
                    }
                    if (continuar == true && continuar1 == true)
                    {

                        #region MENSAJE DE REGISTRO DE CABECERA EXITOSO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA EXITOSO

                        #region FILTROS SUPERCABECERA: ENABLED
                        CmbCampaña.Enabled = false;
                        CmbFormato.Enabled = false;
                        #endregion FILTROS SUPERCABECERA: ENABLED
                        #region FILTROS CABECERA : ENABLED - INICIO
                        Filtros.Disabled = true;
                        #endregion FILTROS CONTENDORES:ENABLED - CABECERA - FIN
                        #region FILTROS DETALLE - INICIO
                        FiltrosDetalle.Visible = true;
                        #endregion FILTROS DETALLE - FIN

                        #region BOTONES TRUE/FALSE - INICIO

                        #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - - INICIO
                        ImgNewDigitacion.Visible = false;
                        ImgSearchDigitacion.Visible = false;
                        ImageButton1.Visible = false;
                        ImgCancelDigitacion.Visible = false;
                        #endregion BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - FIN
                        #region BOTONES CABECERA  -MOSTRAR/OCULTAR - : GUARDAR,  - INICIO
                        ImgEditDigitacion.Visible = false;
                        ImgHabEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = false;
                        ImgCancelEditDigitacion.Visible = false;
                        ImgGuardarCabeceraOpeDigitacion.Visible = false;
                        #endregion BOTONES CABECERA  - MOSTRAR/OCULTAR - : GUARDAR,  - FIN
                        #region BOTONES DETALLE - MOSTRAR/OCULTAR - : GUARDAR, CANCELAR - INICIO

                        ImgGuardarDetalle.Visible = true;
                        ImgCancelarDetalle.Visible = true;

                        #endregion BOTONES DETALLE - GUARDAR Y CANCELAR - FIN

                        #endregion BOTONES TRUE/FALSE - FIN

                        #region METODOS AUXILIARES - INICIO
                        #region OBTENER Y COLOCAR ID DE CABECERA EN TEXTBOX OCULTO PARA GUARDAR EL DETALLE MAS ADELANTE
                        DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_OBTENER_CABECERA_HCCANJEMULTI");
                        Txt_ID_HDCanjeMulticategoriaCAB.Text = dt.Rows[0]["CAB_ID"].ToString().Trim();
                        #endregion

                        #region GENERAR U OBTENER EL NUMERO DE REGISTRO EN EL DETALLE(NO ES EL ID) EN TEXTBOX OCULTO PARA GUARDAR MAS ADELANTE
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        if (ds2.Tables[0].Rows.Count > 1)
                        {
                            string aux = ds2.Tables[0].Rows[0]["DET_ID"].ToString().Trim();
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        else
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        #endregion
                        #endregion METODOS AUXILIARES - FIN

                        #region MUESTRA GRILLA DE PRODUCTOS - INICIO

                        DataSet dsHojaControlCanjeAbarrotesMulticategoria = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIGITACION_FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA",
                            CmbCategoria.SelectedValue, 
                            TxtCodPlanning.Text,
                            Convert.ToInt32(CmbFormato.SelectedValue)
                            );

                        if (this.Session["Opcion"].ToString().Trim() == "New")
                        {

                            if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[0].Rows.Count > 0)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupConfirm";
                                this.Session["mensaje"] = "Correcto, Si hay informacion para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR GRILLA - PRODUCTOS - INICIO

                                GvDigitacionHojaControlAbarrotes.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[0];
                                GvDigitacionHojaControlAbarrotes.DataBind();
                                GvDigitacionHojaControlAbarrotes.Visible = true;
                                GvDigitacionHojaControlAbarrotes.Enabled = true;
                                #region COLUMNAS VISIBLES - INICIO
                                GvDigitacionHojaControlAbarrotes.Columns[8].Visible = false;
                                #endregion COLUMNAS VISIBLES - FIN

                                #endregion MOSTRAR GRILLA - PRODUCTOS - FIN


                            }
                            else
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "No hay información para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - INICIO
                                GvDigitacionHojaControlAbarrotes.Visible = false;
                                #endregion MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - FIN

                                #region MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - INICIO
                                ImgGuardarDetalle.Visible = false;
                                ImgCancelarDetalle.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - FIN

                                #region MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - INICIO
                                ImgGuardarCabeceraOpeDigitacion.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - FIN
                                //ImgGuardarDetalle
                            }


                        }
                        #endregion MUESTRA GRILLA DE PRODUCTOS - FIN
                        #region MUESTRA GRILLA DE PREMIOS - INICIO
                        if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[1].Rows.Count > 0)
                        {
                            GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[1];
                            GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                            #region GRIDVIEW VISIBLE:TRUE - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                            #endregion GRIDVIEW VISIBLE:FALSE - FIN
                            GvDigitacionHojaControlAbarrotes_Premios.Enabled = true;
                            #region COLUMNAS A MOSTRAR EN GRILLA [4] - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = false;
                            #endregion COLUMNAS A MOSTRAR EN GRILLA - FIN

                        }
                        else
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No hay información para digitar";
                            Mensajes_Usuario();

                            GvDigitacionHojaControlAbarrotes_Premios.Visible = false;

                            //ImgGuardarDetalle.Visible = false;
                            //ImgGuardarDetalle
                        }
                        #endregion MUESTRA SI HDCCANJEMULTICATEGORIA TIENE PREMIOS - FIN

                        dsHojaControlCanjeAbarrotesMulticategoria = null;

                    }
                }

                #endregion
                #region FORMATO HDC CANJE, ID=15
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE"])
                {
                    continuar1 = true;

                    #region INSERTANDO CABECERA
                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HDC_CANJE_CAB",
                        TxtCodPlanning.Text,
                        Convert.ToInt32(CmbFormato.Text),
                        Convert.ToInt32(CmbOperativo.Text),
                        Convert.ToDateTime(TxtFecha.Text),
                        CmbCategoria.Text,
                        TxtCliente.Text,
                        Convert.ToInt32(CmbZonaMayor.Text),
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        "");
                    #endregion INSERTANDO CABECERA

                    if (continuar == true && continuar1 == false)
                    {
                        #region MENSAJE DE REGISTRO DE CABECERA FALLIDO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA FALLIDO
                    }
                    if (continuar == true && continuar1 == true)
                    {

                        #region MENSAJE DE REGISTRO DE CABECERA EXITOSO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA EXITOSO

                        #region FILTROS SUPERCABECERA: ENABLED
                        CmbCampaña.Enabled = false;
                        CmbFormato.Enabled = false;
                        #endregion FILTROS SUPERCABECERA: ENABLED
                        #region FILTROS CABECERA : ENABLED - INICIO
                        Filtros.Disabled = true;
                        #endregion FILTROS CONTENDORES:ENABLED - CABECERA - FIN
                        #region FILTROS DETALLE - INICIO
                        FiltrosDetalle.Visible = true;
                        #endregion FILTROS DETALLE - FIN

                        #region BOTONES TRUE/FALSE - INICIO

                        #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - - INICIO
                        ImgNewDigitacion.Visible = false;
                        ImgSearchDigitacion.Visible = false;
                        ImageButton1.Visible = false;
                        ImgCancelDigitacion.Visible = false;
                        #endregion BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - FIN
                        #region BOTONES CABECERA  -MOSTRAR/OCULTAR - : GUARDAR,  - INICIO
                        ImgEditDigitacion.Visible = false;
                        ImgHabEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = false;
                        ImgCancelEditDigitacion.Visible = false;
                        ImgGuardarCabeceraOpeDigitacion.Visible = false;
                        #endregion BOTONES CABECERA  - MOSTRAR/OCULTAR - : GUARDAR,  - FIN
                        #region BOTONES DETALLE - MOSTRAR/OCULTAR - : GUARDAR, CANCELAR - INICIO

                        ImgGuardarDetalle.Visible = true;
                        ImgCancelarDetalle.Visible = true;

                        #endregion BOTONES DETALLE - GUARDAR Y CANCELAR - FIN

                        #endregion BOTONES TRUE/FALSE - FIN

                        #region METODOS AUXILIARES - INICIO
                        #region OBTENER Y COLOCAR ID DE CABECERA EN TEXTBOX OCULTO PARA GUARDAR EL DETALLE MAS ADELANTE
                        DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_OBTENER_CABECERA_HCCANJEMULTI");
                        Txt_ID_HDCanjeMulticategoriaCAB.Text = dt.Rows[0]["CAB_ID"].ToString().Trim();
                        #endregion

                        #region GENERAR U OBTENER EL NUMERO DE REGISTRO EN EL DETALLE(NO ES EL ID) EN TEXTBOX OCULTO PARA GUARDAR MAS ADELANTE
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        if (ds2.Tables[0].Rows.Count > 1)
                        {
                            string aux = ds2.Tables[0].Rows[0]["DET_ID"].ToString().Trim();
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        else
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        #endregion
                        #endregion METODOS AUXILIARES - FIN

                        #region MUESTRA GRILLA DE PRODUCTOS - INICIO

                        DataSet dsHojaControlCanjeAbarrotesMulticategoria = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIGITACION_FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA", 
                            CmbCategoria.SelectedValue, 
                            TxtCodPlanning.Text,
                            Convert.ToInt32(CmbFormato.SelectedValue)
                            );

                        if (this.Session["Opcion"].ToString().Trim() == "New")
                        {

                            if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[0].Rows.Count > 0)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupConfirm";
                                this.Session["mensaje"] = "Correcto, Si hay informacion para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR GRILLA - PRODUCTOS - INICIO

                                GvDigitacionHojaControlAbarrotes.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[0];
                                GvDigitacionHojaControlAbarrotes.DataBind();
                                GvDigitacionHojaControlAbarrotes.Visible = true;
                                GvDigitacionHojaControlAbarrotes.Enabled = true;
                                #region COLUMNAS VISIBLES - INICIO
                                GvDigitacionHojaControlAbarrotes.Columns[8].Visible = false;
                                #endregion COLUMNAS VISIBLES - FIN

                                #endregion MOSTRAR GRILLA - PRODUCTOS - FIN


                            }
                            else
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "No hay información para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - INICIO
                                GvDigitacionHojaControlAbarrotes.Visible = false;
                                #endregion MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - FIN

                                #region MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - INICIO
                                ImgGuardarDetalle.Visible = false;
                                ImgCancelarDetalle.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - FIN

                                #region MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - INICIO
                                ImgGuardarCabeceraOpeDigitacion.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - FIN
                                //ImgGuardarDetalle
                            }


                        }
                        #endregion MUESTRA GRILLA DE PRODUCTOS - FIN
                        #region MUESTRA GRILLA DE PREMIOS - INICIO
                        if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[1].Rows.Count > 0)
                        {
                            GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[1];
                            GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                            #region GRIDVIEW VISIBLE:TRUE - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                            #endregion GRIDVIEW VISIBLE:FALSE - FIN
                            GvDigitacionHojaControlAbarrotes_Premios.Enabled = true;
                            #region COLUMNAS A MOSTRAR EN GRILLA [4] - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = false;
                            #endregion COLUMNAS A MOSTRAR EN GRILLA - FIN

                        }
                        else
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No hay información para digitar";
                            Mensajes_Usuario();

                            GvDigitacionHojaControlAbarrotes_Premios.Visible = false;

                            //ImgGuardarDetalle.Visible = false;
                            //ImgGuardarDetalle
                        }
                        #endregion MUESTRA SI HDCCANJEMULTICATEGORIA TIENE PREMIOS - FIN

                        dsHojaControlCanjeAbarrotesMulticategoria = null;

                    }
                }
                #endregion FORMATO HDC CANJE
                #region FORMATO HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar, ID=16 //MODIFICAR
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar"])
                {
                    continuar1 = true;

                    #region INSERTANDO CABECERA
                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_CAB",
                        TxtCodPlanning.Text,
                        Convert.ToInt32(CmbFormato.Text),
                        Convert.ToDateTime(TxtFecha.Text),
                        "",
                        Convert.ToInt32(CmbOperativo.Text),
                        TxtCliente.Text,
                        Convert.ToInt32(CmbZonaMayor.Text),
                        CmbCategoria.Text,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        "");
                    #endregion INSERTANDO CABECERA

                    if (continuar == true && continuar1 == false)
                    {
                        #region MENSAJE DE REGISTRO DE CABECERA FALLIDO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA FALLIDO
                    }
                    if (continuar == true && continuar1 == true)
                    {

                        #region MENSAJE DE REGISTRO DE CABECERA EXITOSO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA EXITOSO

                        #region FILTROS SUPERCABECERA: ENABLED
                        CmbCampaña.Enabled = false;
                        CmbFormato.Enabled = false;
                        #endregion FILTROS SUPERCABECERA: ENABLED
                        #region FILTROS CABECERA : ENABLED - INICIO
                        Filtros.Disabled = true;
                        #endregion FILTROS CONTENDORES:ENABLED - CABECERA - FIN
                        #region FILTROS DETALLE - INICIO
                        FiltrosDetalle.Visible = true;
                        #endregion FILTROS DETALLE - FIN

                        #region BOTONES TRUE/FALSE - INICIO

                        #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - - INICIO
                        ImgNewDigitacion.Visible = false;
                        ImgSearchDigitacion.Visible = false;
                        ImageButton1.Visible = false;
                        ImgCancelDigitacion.Visible = false;
                        #endregion BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - FIN
                        #region BOTONES CABECERA  -MOSTRAR/OCULTAR - : GUARDAR,  - INICIO
                        ImgEditDigitacion.Visible = false;
                        ImgHabEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = false;
                        ImgCancelEditDigitacion.Visible = false;
                        ImgGuardarCabeceraOpeDigitacion.Visible = false;
                        #endregion BOTONES CABECERA  - MOSTRAR/OCULTAR - : GUARDAR,  - FIN
                        #region BOTONES DETALLE - MOSTRAR/OCULTAR - : GUARDAR, CANCELAR - INICIO

                        ImgGuardarDetalle.Visible = true;
                        ImgCancelarDetalle.Visible = true;

                        #endregion BOTONES DETALLE - GUARDAR Y CANCELAR - FIN

                        #endregion BOTONES TRUE/FALSE - FIN

                        #region METODOS AUXILIARES - INICIO
                        #region OBTENER Y COLOCAR ID DE CABECERA EN TEXTBOX OCULTO PARA GUARDAR EL DETALLE MAS ADELANTE
                        DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_OBTENER_CABECERA_HCCANJEMULTI");
                        Txt_ID_HDCanjeMulticategoriaCAB.Text = dt.Rows[0]["CAB_ID"].ToString().Trim();
                        #endregion

                        #region GENERAR U OBTENER EL NUMERO DE REGISTRO EN EL DETALLE(NO ES EL ID) EN TEXTBOX OCULTO PARA GUARDAR MAS ADELANTE
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        if (ds2.Tables[0].Rows.Count > 1)
                        {
                            string aux = ds2.Tables[0].Rows[0]["DET_ID"].ToString().Trim();
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        else
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        #endregion
                        #endregion METODOS AUXILIARES - FIN

                        #region MUESTRA GRILLA DE PRODUCTOS - INICIO

                        DataSet dsHojaControlCanjeAbarrotesMulticategoria = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIGITACION_FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA", 
                            CmbCategoria.SelectedValue, 
                            TxtCodPlanning.Text,
                            Convert.ToInt32(CmbFormato.SelectedValue)
                            );

                        if (this.Session["Opcion"].ToString().Trim() == "New")
                        {

                            if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[0].Rows.Count > 0)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupConfirm";
                                this.Session["mensaje"] = "Correcto, Si hay informacion para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR GRILLA - PRODUCTOS - INICIO

                                GvDigitacionHojaControlAbarrotes.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[0];
                                GvDigitacionHojaControlAbarrotes.DataBind();
                                GvDigitacionHojaControlAbarrotes.Visible = true;
                                GvDigitacionHojaControlAbarrotes.Enabled = true;

                                #region COLUMNAS VISIBLES - INICIO
                                GvDigitacionHojaControlAbarrotes.Columns[8].Visible = false;
                                #endregion COLUMNAS VISIBLES - FIN

                                #endregion MOSTRAR GRILLA - PRODUCTOS - FIN


                            }
                            else
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "No hay información para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - INICIO
                                GvDigitacionHojaControlAbarrotes.Visible = false;
                                #endregion MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - FIN

                                #region MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - INICIO
                                ImgGuardarDetalle.Visible = false;
                                ImgCancelarDetalle.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - FIN

                                #region MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - INICIO
                                ImgGuardarCabeceraOpeDigitacion.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - FIN
                                //ImgGuardarDetalle
                            }


                        }
                        #endregion MUESTRA GRILLA DE PRODUCTOS - FIN
                        #region MUESTRA GRILLA DE PREMIOS - INICIO
                        if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[1].Rows.Count > 0)
                        {
                            GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[1];
                            GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                            #region GRIDVIEW VISIBLE:TRUE - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                            #endregion GRIDVIEW VISIBLE:FALSE - FIN
                            GvDigitacionHojaControlAbarrotes_Premios.Enabled = true;

                            #region COLUMNAS A MOSTRAR EN GRILLA [4] - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = false;
                            #endregion COLUMNAS A MOSTRAR EN GRILLA - FIN

                        }
                        else
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No hay información para digitar";
                            Mensajes_Usuario();

                            GvDigitacionHojaControlAbarrotes_Premios.Visible = false;

                            //ImgGuardarDetalle.Visible = false;
                            //ImgGuardarDetalle
                        }
                        #endregion MUESTRA SI HDCCANJEMULTICATEGORIA TIENE PREMIOS - FIN

                        dsHojaControlCanjeAbarrotesMulticategoria = null;

                    }
                }
                #endregion FORMATO HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar
                #region FORMATO_RESUMEN_CANJE_PLUSBELLE, ID=17
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_PLUSBELLE"])
                {
                    continuar1 = true;

                    #region INSERTANDO CABECERA
                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HDC_CANJE_CAB",
                        TxtCodPlanning.Text,
                        Convert.ToInt32(CmbFormato.Text),
                        Convert.ToInt32(CmbOperativo.Text),
                        Convert.ToInt32(CmbZonaMayor.Text),
                        Convert.ToDateTime(TxtFecha.Text),
                        Convert.ToDateTime(TxtFechaFin.Text),
                        CmbCategoria.Text,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        "");
                    #endregion INSERTANDO CABECERA

                    if (continuar == true && continuar1 == false)
                    {
                        #region MENSAJE DE REGISTRO DE CABECERA FALLIDO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA FALLIDO
                    }
                    if (continuar == true && continuar1 == true)
                    {

                        #region MENSAJE DE REGISTRO DE CABECERA EXITOSO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA EXITOSO

                        #region FILTROS SUPERCABECERA: ENABLED
                        CmbCampaña.Enabled = false;
                        CmbFormato.Enabled = false;
                        #endregion FILTROS SUPERCABECERA: ENABLED
                        #region FILTROS CABECERA : ENABLED - INICIO
                        Filtros.Disabled = true;
                        #endregion FILTROS CONTENDORES:ENABLED - CABECERA - FIN
                        #region FILTROS DETALLE - INICIO
                        FiltrosDetalle.Visible = true;
                        #endregion FILTROS DETALLE - FIN

                        #region BOTONES TRUE/FALSE - INICIO

                        #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - - INICIO
                        ImgNewDigitacion.Visible = false;
                        ImgSearchDigitacion.Visible = false;
                        ImageButton1.Visible = false;
                        ImgCancelDigitacion.Visible = false;
                        #endregion BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - FIN
                        #region BOTONES CABECERA  -MOSTRAR/OCULTAR - : GUARDAR,  - INICIO
                        ImgEditDigitacion.Visible = false;
                        ImgHabEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = false;
                        ImgCancelEditDigitacion.Visible = false;
                        ImgGuardarCabeceraOpeDigitacion.Visible = false;
                        #endregion BOTONES CABECERA  - MOSTRAR/OCULTAR - : GUARDAR,  - FIN
                        #region BOTONES DETALLE - MOSTRAR/OCULTAR - : GUARDAR, CANCELAR - INICIO

                        ImgGuardarDetalle.Visible = true;
                        ImgCancelarDetalle.Visible = true;

                        #endregion BOTONES DETALLE - GUARDAR Y CANCELAR - FIN

                        #endregion BOTONES TRUE/FALSE - FIN

                        #region METODOS AUXILIARES - INICIO
                        #region OBTENER Y COLOCAR ID DE CABECERA EN TEXTBOX OCULTO PARA GUARDAR EL DETALLE MAS ADELANTE
                        DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_OBTENER_CABECERA_HCCANJEMULTI");
                        Txt_ID_HDCanjeMulticategoriaCAB.Text = dt.Rows[0]["CAB_ID"].ToString().Trim();
                        #endregion

                        #region GENERAR U OBTENER EL NUMERO DE REGISTRO EN EL DETALLE(NO ES EL ID) EN TEXTBOX OCULTO PARA GUARDAR MAS ADELANTE
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        if (ds2.Tables[0].Rows.Count > 1)
                        {
                            string aux = ds2.Tables[0].Rows[0]["DET_ID"].ToString().Trim();
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        else
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        #endregion
                        #endregion METODOS AUXILIARES - FIN

                        #region MUESTRA GRILLA DE PRODUCTOS - INICIO

                        DataSet dsHojaControlCanjeAbarrotesMulticategoria = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIGITACION_FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA", 
                            CmbCategoria.SelectedValue, 
                            TxtCodPlanning.Text,
                            Convert.ToInt32(CmbFormato.SelectedValue)
                            );

                        if (this.Session["Opcion"].ToString().Trim() == "New")
                        {

                            if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[0].Rows.Count > 0)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupConfirm";
                                this.Session["mensaje"] = "Correcto, Si hay informacion para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR GRILLA - PRODUCTOS - INICIO

                                GvDigitacionHojaControlAbarrotes.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[0];
                                GvDigitacionHojaControlAbarrotes.DataBind();
                                GvDigitacionHojaControlAbarrotes.Visible = true;
                                GvDigitacionHojaControlAbarrotes.Enabled = true;
                                #region COLUMNAS VISIBLES - INICIO
                                GvDigitacionHojaControlAbarrotes.Columns[8].Visible = false;
                                #endregion COLUMNAS VISIBLES - FIN

                                #endregion MOSTRAR GRILLA - PRODUCTOS - FIN


                            }
                            else
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "No hay información para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - INICIO
                                GvDigitacionHojaControlAbarrotes.Visible = false;
                                #endregion MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - FIN

                                #region MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - INICIO
                                ImgGuardarDetalle.Visible = false;
                                ImgCancelarDetalle.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - FIN

                                #region MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - INICIO
                                ImgGuardarCabeceraOpeDigitacion.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - FIN
                                //ImgGuardarDetalle
                            }


                        }
                        #endregion MUESTRA GRILLA DE PRODUCTOS - FIN
                        #region MUESTRA GRILLA DE PREMIOS - INICIO
                        if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[1].Rows.Count > 0)
                        {
                            GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[1];
                            GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                            #region GRIDVIEW VISIBLE:TRUE - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                            #endregion GRIDVIEW VISIBLE:FALSE - FIN
                            GvDigitacionHojaControlAbarrotes_Premios.Enabled = true;
                            #region COLUMNAS A MOSTRAR EN GRILLA [4] - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = false;
                            #endregion COLUMNAS A MOSTRAR EN GRILLA - FIN

                        }
                        else
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No hay información para digitar";
                            Mensajes_Usuario();

                            GvDigitacionHojaControlAbarrotes_Premios.Visible = false;

                            //ImgGuardarDetalle.Visible = false;
                            //ImgGuardarDetalle
                        }
                        #endregion MUESTRA SI HDCCANJEMULTICATEGORIA TIENE PREMIOS - FIN

                        dsHojaControlCanjeAbarrotesMulticategoria = null;

                    }
                }
                #endregion FORMATO_RESUMEN_CANJE_PLUSBELLE 
                #region FORMATO COMPETENCIA_ANUA, ID=18 //MODIFICAR
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_ANUA"])
                {
                    continuar1 = true;

                    #region INSERTANDO CABECERA
                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_CAB",
                        TxtCodPlanning.Text,
                        Convert.ToInt32(CmbFormato.Text),
                        Convert.ToDateTime(TxtFecha.Text),
                        Convert.ToDateTime(TxtFechaFin.Text),
                        Convert.ToInt32(CmbOperativo.Text),
                        "",//TxtCliente.text
                        Convert.ToInt32(CmbZonaMayor.Text),
                        CmbCategoria.Text,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        "");
                    #endregion INSERTANDO CABECERA

                    if (continuar == true && continuar1 == false)
                    {
                        #region MENSAJE DE REGISTRO DE CABECERA FALLIDO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA FALLIDO
                    }
                    if (continuar == true && continuar1 == true)
                    {

                        #region MENSAJE DE REGISTRO DE CABECERA EXITOSO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA EXITOSO

                        #region FILTROS SUPERCABECERA: ENABLED
                        CmbCampaña.Enabled = false;
                        CmbFormato.Enabled = false;
                        #endregion FILTROS SUPERCABECERA: ENABLED
                        #region FILTROS CABECERA : ENABLED - INICIO
                        Filtros.Disabled = true;
                        #endregion FILTROS CONTENDORES:ENABLED - CABECERA - FIN
                        #region FILTROS DETALLE - INICIO
                        FiltrosDetalle.Visible = true;
                        #endregion FILTROS DETALLE - FIN

                        #region BOTONES TRUE/FALSE - INICIO

                        #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - - INICIO
                        ImgNewDigitacion.Visible = false;
                        ImgSearchDigitacion.Visible = false;
                        ImageButton1.Visible = false;
                        ImgCancelDigitacion.Visible = false;
                        #endregion BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - FIN
                        #region BOTONES CABECERA  -MOSTRAR/OCULTAR - : GUARDAR,  - INICIO
                        ImgEditDigitacion.Visible = false;
                        ImgHabEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = false;
                        ImgCancelEditDigitacion.Visible = false;
                        ImgGuardarCabeceraOpeDigitacion.Visible = false;
                        #endregion BOTONES CABECERA  - MOSTRAR/OCULTAR - : GUARDAR,  - FIN
                        #region BOTONES DETALLE - MOSTRAR/OCULTAR - : GUARDAR, CANCELAR - INICIO

                        ImgGuardarDetalle.Visible = true;
                        ImgCancelarDetalle.Visible = true;

                        #endregion BOTONES DETALLE - GUARDAR Y CANCELAR - FIN

                        #endregion BOTONES TRUE/FALSE - FIN

                        #region METODOS AUXILIARES - INICIO
                        #region OBTENER Y COLOCAR ID DE CABECERA EN TEXTBOX OCULTO PARA GUARDAR EL DETALLE MAS ADELANTE
                        DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_OBTENER_CABECERA_HCCANJEMULTI");
                        Txt_ID_HDCanjeMulticategoriaCAB.Text = dt.Rows[0]["CAB_ID"].ToString().Trim();
                        #endregion

                        #region GENERAR U OBTENER EL NUMERO DE REGISTRO EN EL DETALLE(NO ES EL ID) EN TEXTBOX OCULTO PARA GUARDAR MAS ADELANTE
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        if (ds2.Tables[0].Rows.Count > 1)
                        {
                            string aux = ds2.Tables[0].Rows[0]["DET_ID"].ToString().Trim();
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        else
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        #endregion
                        #endregion METODOS AUXILIARES - FIN

                        #region MUESTRA GRILLA DE PRODUCTOS - INICIO

                        DataSet dsHojaControlCanjeAbarrotesMulticategoria = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIGITACION_FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA", 
                            CmbCategoria.SelectedValue, 
                            TxtCodPlanning.Text,
                            Convert.ToInt32(CmbFormato.SelectedValue)
                            );

                        if (this.Session["Opcion"].ToString().Trim() == "New")
                        {

                            if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[0].Rows.Count > 0)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupConfirm";
                                this.Session["mensaje"] = "Correcto, Si hay informacion para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR GRILLA - PRODUCTOS - INICIO

                                GvDigitacionHojaControlAbarrotes.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[0];
                                GvDigitacionHojaControlAbarrotes.DataBind();
                                GvDigitacionHojaControlAbarrotes.Visible = true;
                                GvDigitacionHojaControlAbarrotes.Enabled = true;
                                #region COLUMNAS VISIBLES - INICIO
                                GvDigitacionHojaControlAbarrotes.Columns[8].Visible = false;
                                #endregion COLUMNAS VISIBLES - FIN

                                #endregion MOSTRAR GRILLA - PRODUCTOS - FIN


                            }
                            else
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "No hay información para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - INICIO
                                GvDigitacionHojaControlAbarrotes.Visible = false;
                                #endregion MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - FIN

                                #region MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - INICIO
                                ImgGuardarDetalle.Visible = false;
                                ImgCancelarDetalle.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - FIN

                                #region MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - INICIO
                                ImgGuardarCabeceraOpeDigitacion.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - FIN
                                //ImgGuardarDetalle
                            }


                        }
                        #endregion MUESTRA GRILLA DE PRODUCTOS - FIN
                        #region MUESTRA GRILLA DE PREMIOS - INICIO
                        if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[1].Rows.Count > 0)
                        {
                            GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[1];
                            GvDigitacionHojaControlAbarrotes_Premios.DataBind();
                            GvDigitacionHojaControlAbarrotes_Premios.Enabled = true;
                            #region GRIDVIEW VISIBLE:TRUE - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                            #endregion GRIDVIEW VISIBLE:FALSE - FIN

                            #region COLUMNAS A MOSTRAR EN GRILLA [4] - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = false;
                            #endregion COLUMNAS A MOSTRAR EN GRILLA - FIN

                        }
                        else
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No hay información para digitar";
                            Mensajes_Usuario();

                            GvDigitacionHojaControlAbarrotes_Premios.Visible = false;

                            //ImgGuardarDetalle.Visible = false;
                            //ImgGuardarDetalle
                        }
                        #endregion MUESTRA SI HDCCANJEMULTICATEGORIA TIENE PREMIOS - FIN

                        dsHojaControlCanjeAbarrotesMulticategoria = null;

                    }
                }
                #endregion FORMATO COMPETENCIA_ANUA
                #region FORMATO COMPETENCIA_MAYORISTA, ID=19 //MODIFICAR
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_MAYORISTA"])
                {
                    continuar1 = true;

                    #region INSERTANDO CABECERA
                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_CAB",
                        TxtCodPlanning.Text,
                        Convert.ToInt32(CmbFormato.Text),
                        Convert.ToDateTime(TxtFecha.Text),
                        Convert.ToDateTime(TxtFechaFin.Text),
                        Convert.ToInt32(CmbOperativo.Text),
                        "",//TxtCliente.text
                        Convert.ToInt32(CmbZonaMayor.Text),
                        CmbCategoria.Text,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        Convert.ToString(this.Session["sUser"]),
                        DateTime.Now,
                        "");
                    #endregion INSERTANDO CABECERA

                    if (continuar == true && continuar1 == false)
                    {
                        #region MENSAJE DE REGISTRO DE CABECERA FALLIDO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA FALLIDO
                    }
                    if (continuar == true && continuar1 == true)
                    {

                        #region MENSAJE DE REGISTRO DE CABECERA EXITOSO
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();
                        #endregion MENSAJE DE REGISTRO DE CABECERA EXITOSO

                        #region FILTROS SUPERCABECERA: ENABLED
                        CmbCampaña.Enabled = false;
                        CmbFormato.Enabled = false;
                        #endregion FILTROS SUPERCABECERA: ENABLED
                        #region FILTROS CABECERA : ENABLED - INICIO
                        Filtros.Disabled = true;
                        #endregion FILTROS CONTENDORES:ENABLED - CABECERA - FIN
                        #region FILTROS DETALLE - INICIO
                        FiltrosDetalle.Visible = true;
                        #endregion FILTROS DETALLE - FIN

                        #region BOTONES TRUE/FALSE - INICIO

                        #region BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - - INICIO
                        ImgNewDigitacion.Visible = false;
                        ImgSearchDigitacion.Visible = false;
                        ImageButton1.Visible = false;
                        ImgCancelDigitacion.Visible = false;
                        #endregion BOTONES SUPERCABECERA - MOSTRAR/OCULTAR - FIN
                        #region BOTONES CABECERA  -MOSTRAR/OCULTAR - : GUARDAR,  - INICIO
                        ImgEditDigitacion.Visible = false;
                        ImgHabEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = false;
                        ImgCancelEditDigitacion.Visible = false;
                        ImgGuardarCabeceraOpeDigitacion.Visible = false;
                        #endregion BOTONES CABECERA  - MOSTRAR/OCULTAR - : GUARDAR,  - FIN
                        #region BOTONES DETALLE - MOSTRAR/OCULTAR - : GUARDAR, CANCELAR - INICIO

                        ImgGuardarDetalle.Visible = true;
                        ImgCancelarDetalle.Visible = true;

                        #endregion BOTONES DETALLE - GUARDAR Y CANCELAR - FIN

                        #endregion BOTONES TRUE/FALSE - FIN

                        #region METODOS AUXILIARES - INICIO
                        #region OBTENER Y COLOCAR ID DE CABECERA EN TEXTBOX OCULTO PARA GUARDAR EL DETALLE MAS ADELANTE
                        DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_OBTENER_CABECERA_HCCANJEMULTI");
                        Txt_ID_HDCanjeMulticategoriaCAB.Text = dt.Rows[0]["CAB_ID"].ToString().Trim();
                        #endregion

                        #region GENERAR U OBTENER EL NUMERO DE REGISTRO EN EL DETALLE(NO ES EL ID) EN TEXTBOX OCULTO PARA GUARDAR MAS ADELANTE
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        if (ds2.Tables[0].Rows.Count > 1)
                        {
                            string aux = ds2.Tables[0].Rows[0]["DET_ID"].ToString().Trim();
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        else
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        #endregion
                        #endregion METODOS AUXILIARES - FIN

                        #region MUESTRA GRILLA DE PRODUCTOS - INICIO

                        DataSet dsHojaControlCanjeAbarrotesMulticategoria = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIGITACION_FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA", CmbCategoria.SelectedValue, TxtCodPlanning.Text);

                        if (this.Session["Opcion"].ToString().Trim() == "New")
                        {

                            if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[0].Rows.Count > 0)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupConfirm";
                                this.Session["mensaje"] = "Correcto, Si hay informacion para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR GRILLA - PRODUCTOS - INICIO

                                GvDigitacionHojaControlAbarrotes.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[0];
                                GvDigitacionHojaControlAbarrotes.DataBind();
                                GvDigitacionHojaControlAbarrotes.Visible = true;
                                GvDigitacionHojaControlAbarrotes.Enabled = true;
                                #region COLUMNAS VISIBLES - INICIO
                                GvDigitacionHojaControlAbarrotes.Columns[8].Visible = false;
                                #endregion COLUMNAS VISIBLES - FIN

                                #endregion MOSTRAR GRILLA - PRODUCTOS - FIN


                            }
                            else
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "No hay información para digitar";
                                Mensajes_Usuario();

                                #region MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - INICIO
                                GvDigitacionHojaControlAbarrotes.Visible = false;
                                #endregion MOSTRAR/OCULTAR : GRILLA -PRODUCTOS - FIN

                                #region MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - INICIO
                                ImgGuardarDetalle.Visible = false;
                                ImgCancelarDetalle.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES DETALLE : GUARDAR, CANCELAR - FIN

                                #region MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - INICIO
                                ImgGuardarCabeceraOpeDigitacion.Visible = false;
                                #endregion MOSTRAR/OCULTAR - BOTONES CABECERA : GUARDAR,  - FIN
                                //ImgGuardarDetalle
                            }


                        }
                        #endregion MUESTRA GRILLA DE PRODUCTOS - FIN
                        #region MUESTRA GRILLA DE PREMIOS - INICIO
                        if (dsHojaControlCanjeAbarrotesMulticategoria.Tables[1].Rows.Count > 0)
                        {
                            GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsHojaControlCanjeAbarrotesMulticategoria.Tables[1];
                            GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                            #region GRIDVIEW VISIBLE:TRUE - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                            #endregion GRIDVIEW VISIBLE:FALSE - FIN
                            GvDigitacionHojaControlAbarrotes_Premios.Enabled = true;
                            #region COLUMNAS A MOSTRAR EN GRILLA [4] - INICIO
                            GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = false;
                            #endregion COLUMNAS A MOSTRAR EN GRILLA - FIN

                        }
                        else
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "No hay información para digitar";
                            Mensajes_Usuario();

                            GvDigitacionHojaControlAbarrotes_Premios.Visible = false;

                            //ImgGuardarDetalle.Visible = false;
                            //ImgGuardarDetalle
                        }
                        #endregion MUESTRA SI HDCCANJEMULTICATEGORIA TIENE PREMIOS - FIN

                        dsHojaControlCanjeAbarrotesMulticategoria = null;

                    }
                }
                #endregion FORMATO COMPETENCIA_MAYORISTA

            }
        }
        #endregion EVENTO CLICK: BOTON GUARDAR: FILTRO PRINCIPAL

        #endregion EVENTOS CLICK: FILTRO PRINCIPAL : BOTONES
        #region EVENTOS CLICK: FILTRO DETALLES : BOTONES

        #region EVENTO CLICK: BOTON GUARDAR DETALLE: FILTRO DETALLE
        protected void ImgGuardarHDCCanjeDetalle_Click(object sender, ImageClickEventArgs e)
        {
            bool continuar = true;
            bool continuar1 = false;

            if (ValidaInfoFiltrosDetalles(continuar))
            {

                #region FORMATO_ABMASCOTASHOJACONTROL
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASHOJACONTROL"])
                {

                    #region INSERTAR DETALLES

                    if (GvDigitacionHojaControlAbarrotes.Rows.Count > 0)
                    {
                        #region METODO AUXILIARES
                        #region OBTENER NUMERO DE REGISTRO MAX
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        string aux = ds2.Tables[0].Rows[0]["NUM_REG"].ToString().Trim();
                        if (aux == "")
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        else if (Convert.ToInt32(aux) >= 1)
                        {
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        #endregion OBTENER NUMERO DE REGISTRO MAX
                        #endregion METODO AUXILIARES

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {

                            if (GvDigitacionHojaControlAbarrotes.Columns[6].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL DETALLES
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_DET",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        TxtFacturaBoleta.Text,
                                        TxtNombreClienteMinorista.Text,
                                        TxtDNI.Text,
                                        TxtMontoSoles.Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblEmpaque")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblGrupo")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblPresentacion")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblMarca")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblSKU")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNombreProducto")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL DETALLES
                                }
                            }

                        }
                    }
                    #endregion INSERTAR DETALLES
                    #region INSERTAR PREMIOS
                    if (GvDigitacionHojaControlAbarrotes_Premios.Rows.Count > 0)
                    {

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionHojaControlAbarrotes_Premios.Columns[2].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL PREMIOS
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_PREMIO",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        ((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_Id_Premio")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL PREMIOS
                                }
                            }
                        }

                    }
                    #endregion INSERTAR PREMIOS

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }

                    if (continuar == true && continuar1 == true)
                    {

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();


                        LimpiarFiltrosDetalles();

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes_Premios
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes_Premios

                    }
                }
                #endregion 
                //guardar para : FORMATO HDC_ABARROTES_ROTACIONMULTICATEGORIA DETALLE, ID=12  y PREMIO
                #region FORMATO HDC_ABARROTES_ROTACIONMULTICATEGORIA, ID=12  y PREMIO
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
                {
                    
                    #region INSERTAR DETALLES

                    if (GvDigitacionHojaControlAbarrotes.Rows.Count > 0)
                    {
                        #region METODO AUXILIARES
                        #region OBTENER NUMERO DE REGISTRO MAX
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        string aux = ds2.Tables[0].Rows[0]["NUM_REG"].ToString().Trim();
                        if (aux == "")
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        else if (Convert.ToInt32(aux) >= 1)
                        {
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        #endregion OBTENER NUMERO DE REGISTRO MAX
                        #endregion METODO AUXILIARES

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {

                            if (GvDigitacionHojaControlAbarrotes.Columns[6].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL DETALLES
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_DET",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        TxtFacturaBoleta.Text,
                                        TxtNombreClienteMinorista.Text,
                                        TxtDNI.Text,
                                        TxtMontoSoles.Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblEmpaque")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblGrupo")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblPresentacion")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblMarca")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblSKU")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNombreProducto")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL DETALLES
                                }
                            }

                        }
                    }
                    #endregion INSERTAR DETALLES
                    #region INSERTAR PREMIOS
                    if (GvDigitacionHojaControlAbarrotes_Premios.Rows.Count > 0)
                    {

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionHojaControlAbarrotes_Premios.Columns[2].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL PREMIOS
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_PREMIO",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        ((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_Id_Premio")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL PREMIOS
                                }
                            }
                        }

                    }
                    #endregion INSERTAR PREMIOS

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }

                    if (continuar == true && continuar1 == true)
                    {

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();

                         
                        LimpiarFiltrosDetalles();

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++) {
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes_Premios
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes_Premios

                    }
                }
                #endregion FORMATO HDC_ABARROTES_ROTACIONMULTICATEGORIA, ID=12  y PREMIO

                #region FORMATO_CANJE_ANUA
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_CANJE_ANUA"])
                {
                    
                    #region INSERTAR DETALLES
                    if (GvDigitacionHojaControlAbarrotes.Rows.Count > 0)
                    {
                        #region METODO AUXILIARES
                        #region OBTENER NUMERO DE REGISTRO MAX
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        string aux = ds2.Tables[0].Rows[0]["NUM_REG"].ToString().Trim();
                        if (aux == "")
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        else if (Convert.ToInt32(aux) >= 1)
                        {
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        #endregion OBTENER NUMERO DE REGISTRO MAX
                        #endregion METODO AUXILIARES

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {

                            if (GvDigitacionHojaControlAbarrotes.Columns[6].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL DETALLES
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_CANJE_ANUA_DET",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        TxtFacturaBoleta.Text,
                                        TxtNombreClienteMinorista.Text,
                                        TxtDNI.Text,
                                        TxtTelefono.Text,
                                        TxtMontoSoles.Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblEmpaque")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblGrupo")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblPresentacion")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblMarca")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblSKU")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNombreProducto")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL DETALLES
                                }
                            }

                        }
                    }
                    #endregion INSERTAR DETALLES
                    #region INSERTAR PREMIOS
                    if (GvDigitacionHojaControlAbarrotes_Premios.Rows.Count > 0)
                    {

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionHojaControlAbarrotes_Premios.Columns[2].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL PREMIOS
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_PREMIO",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        ((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_Id_Premio")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL PREMIOS
                                }
                            }
                        }

                    }
                    #endregion INSERTAR PREMIOS

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }

                    if (continuar == true && continuar1 == true)
                    {

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();


                        LimpiarFiltrosDetalles();

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes_Premios
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes_Premios

                    }
                }
                #endregion FORMATO RESUMEN_CANJE_ANUA, ID=13

                #region FORMATO_RESUMEN_CANJE_ANUA
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_ANUA"])
                {
                    #region INSERTAR DETALLES
                    if (GvDigitacionHojaControlAbarrotes.Rows.Count > 0)
                    {
                        #region METODO AUXILIARES
                        #region OBTENER NUMERO DE REGISTRO MAX
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        string aux = ds2.Tables[0].Rows[0]["NUM_REG"].ToString().Trim();
                        if (aux == "")
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        else if (Convert.ToInt32(aux) >= 1)
                        {
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        #endregion OBTENER NUMERO DE REGISTRO MAX
                        #endregion METODO AUXILIARES

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {

                            if (GvDigitacionHojaControlAbarrotes.Columns[6].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL DETALLES
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_RESUMEN_CANJE_ANUA_DET",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        TxtClienteDetalle.Text,
                                        TxtMontoSoles.Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblEmpaque")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblGrupo")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblPresentacion")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblMarca")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblSKU")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNombreProducto")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL DETALLES
                                }
                            }

                        }
                    }
                    #endregion INSERTAR DETALLES
                    #region INSERTAR PREMIOS
                    if (GvDigitacionHojaControlAbarrotes_Premios.Rows.Count > 0)
                    {

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionHojaControlAbarrotes_Premios.Columns[2].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL PREMIOS
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_PREMIO",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        ((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_Id_Premio")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL PREMIOS
                                }
                            }
                        }

                    }
                    #endregion INSERTAR PREMIOS

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }

                    if (continuar == true && continuar1 == true)
                    {

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();


                        LimpiarFiltrosDetalles();

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes_Premios
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes_Premios

                    }
                }
                #endregion FORMATO CANJE_ANUA, ID=14

                #region FORMATO HDC_CANJE, ID=15
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE"])
                {
                    #region INSERTAR DETALLES
                    if (GvDigitacionHojaControlAbarrotes.Rows.Count > 0)
                    {
                        #region METODO AUXILIARES
                        #region OBTENER NUMERO DE REGISTRO MAX
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        string aux = ds2.Tables[0].Rows[0]["NUM_REG"].ToString().Trim();
                        if (aux == "")
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        else if (Convert.ToInt32(aux) >= 1)
                        {
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        #endregion OBTENER NUMERO DE REGISTRO MAX
                        #endregion METODO AUXILIARES

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {

                            if (GvDigitacionHojaControlAbarrotes.Columns[6].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL DETALLES
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HDC_CANJE_DET",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        TxtFacturaBoleta.Text,
                                        TxtNombreClienteMinorista.Text,
                                        TxtDNI.Text,
                                        TxtTelefono.Text,
                                        TxtMontoSoles.Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblEmpaque")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblGrupo")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblPresentacion")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblMarca")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblSKU")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNombreProducto")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL DETALLES
                                }
                            }

                        }
                    }
                    #endregion INSERTAR DETALLES
                    #region INSERTAR PREMIOS
                    if (GvDigitacionHojaControlAbarrotes_Premios.Rows.Count > 0)
                    {

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionHojaControlAbarrotes_Premios.Columns[2].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL PREMIOS
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_PREMIO",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        ((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_Id_Premio")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL PREMIOS
                                }
                            }
                        }

                    }
                    #endregion INSERTAR PREMIOS
                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }

                    if (continuar == true && continuar1 == true)
                    {

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();


                        LimpiarFiltrosDetalles();

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes_Premios
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes_Premios

                    }
                }
                #endregion FORMATO HDC_CANJE, ID=15

                #region FORMATO HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar, ID=16 - CAMBIAR SEGUN FORMATO
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar"])
                {
                    #region INSERTAR DETALLES
                    if (GvDigitacionHojaControlAbarrotes.Rows.Count > 0)
                    {
                        #region METODO AUXILIARES
                        #region OBTENER NUMERO DE REGISTRO MAX
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        string aux = ds2.Tables[0].Rows[0]["NUM_REG"].ToString().Trim();
                        if (aux == "")
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        else if (Convert.ToInt32(aux) >= 1)
                        {
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        #endregion OBTENER NUMERO DE REGISTRO MAX
                        #endregion METODO AUXILIARES

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {

                            if (GvDigitacionHojaControlAbarrotes.Columns[6].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL DETALLES
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_DET",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        TxtFacturaBoleta.Text,
                                        TxtNombreClienteMinorista.Text,
                                        TxtDNI.Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblEmpaque")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblGrupo")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblPresentacion")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblMarca")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblSKU")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNombreProducto")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL DETALLES
                                }
                            }

                        }
                    }
                    #endregion INSERTAR DETALLES
                    #region INSERTAR PREMIOS
                    if (GvDigitacionHojaControlAbarrotes_Premios.Rows.Count > 0)
                    {

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionHojaControlAbarrotes_Premios.Columns[2].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL PREMIOS
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_PREMIO",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        ((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_Id_Premio")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL PREMIOS
                                }
                            }
                        }

                    }
                    #endregion INSERTAR PREMIOS
                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }

                    if (continuar == true && continuar1 == true)
                    {

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();


                        LimpiarFiltrosDetalles();

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes_Premios
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes_Premios

                    }
                }
                #endregion FORMATO HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar, ID=16

                #region FORMATO RESUMEN_CANJE_PLUSBELLE, ID=17
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_PLUSBELLE"])
                {
                    #region INSERTAR DETALLES
                    if (GvDigitacionHojaControlAbarrotes.Rows.Count > 0)
                    {
                        #region METODO AUXILIARES
                        #region OBTENER NUMERO DE REGISTRO MAX
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        string aux = ds2.Tables[0].Rows[0]["NUM_REG"].ToString().Trim();
                        if (aux == "")
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        else if (Convert.ToInt32(aux) >= 1)
                        {
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        #endregion OBTENER NUMERO DE REGISTRO MAX
                        #endregion METODO AUXILIARES

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionHojaControlAbarrotes.Columns[6].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL DETALLES
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_RESUMEN_CANJE_PLUSBELLE_DET",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        TxtClienteDetalle.Text,
                                        TxtMontoSoles.Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblEmpaque")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblGrupo")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblPresentacion")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblMarca")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblSKU")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNombreProducto")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL DETALLES
                                }
                            }

                        }
                    }
                    #endregion INSERTAR DETALLES
                    #region INSERTAR PREMIOS
                    if (GvDigitacionHojaControlAbarrotes_Premios.Rows.Count > 0)
                    {

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionHojaControlAbarrotes_Premios.Columns[2].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL PREMIOS
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_PREMIO",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        ((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_Id_Premio")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL PREMIOS
                                }
                            }
                        }

                    }
                    #endregion INSERTAR PREMIOS
                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }

                    if (continuar == true && continuar1 == true)
                    {

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();


                        LimpiarFiltrosDetalles();

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes_Premios
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes_Premios

                    }
                }
                #endregion FORMATO RESUMEN_CANJE_PLUSBELLE, ID=17

                #region FORMATO COMPETENCIA_ANUA, ID=18 -- CAMBIAR SEGUN FORMATO
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_ANUA"])
                {
                    #region INSERTAR DETALLES
                    if (GvDigitacionHojaControlAbarrotes.Rows.Count > 0)
                    {
                        #region METODO AUXILIARES
                        #region OBTENER NUMERO DE REGISTRO MAX
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        string aux = ds2.Tables[0].Rows[0]["NUM_REG"].ToString().Trim();
                        if (aux == "")
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        else if (Convert.ToInt32(aux) >= 1)
                        {
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        #endregion OBTENER NUMERO DE REGISTRO MAX
                        #endregion METODO AUXILIARES

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {

                            if (GvDigitacionHojaControlAbarrotes.Columns[6].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL DETALLES
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_DET",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        TxtFacturaBoleta.Text,
                                        TxtNombreClienteMinorista.Text,
                                        TxtDNI.Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblEmpaque")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblGrupo")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblPresentacion")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblMarca")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblSKU")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNombreProducto")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL DETALLES
                                }
                            }

                        }
                    }
                    #endregion INSERTAR DETALLES
                    #region INSERTAR PREMIOS
                    if (GvDigitacionHojaControlAbarrotes_Premios.Rows.Count > 0)
                    {

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionHojaControlAbarrotes_Premios.Columns[2].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL PREMIOS
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_PREMIO",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        ((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_Id_Premio")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL PREMIOS
                                }
                            }
                        }

                    }
                    #endregion INSERTAR PREMIOS

                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }

                    if (continuar == true && continuar1 == true)
                    {

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();


                        LimpiarFiltrosDetalles();

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes_Premios
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes_Premios

                    }
                }
                #endregion FORMATO RESUMEN_CANJE_PLUSBELLE, ID=18

                #region FORMATO COMPETENCIA_MAYORISTA, ID=19 -- CAMBIAR SEGUN FORMATO
                if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_MAYORISTA"])
                {
                    #region INSERTAR DETALLES
                    if (GvDigitacionHojaControlAbarrotes.Rows.Count > 0)
                    {
                        #region METODO AUXILIARES
                        #region OBTENER NUMERO DE REGISTRO MAX
                        DataSet ds2 = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_OBTENER_DETALLE_HCCANJEMULTI", Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text));
                        string aux = ds2.Tables[0].Rows[0]["NUM_REG"].ToString().Trim();
                        if (aux == "")
                        {
                            TxtNumRegistro_DET.Text = "1";
                        }
                        else if (Convert.ToInt32(aux) >= 1)
                        {
                            int aux2 = Convert.ToInt32(aux) + 1;
                            TxtNumRegistro_DET.Text = Convert.ToString(aux2);
                        }
                        #endregion OBTENER NUMERO DE REGISTRO MAX
                        #endregion METODO AUXILIARES

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {

                            if (GvDigitacionHojaControlAbarrotes.Columns[6].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL DETALLES
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_DET",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        TxtFacturaBoleta.Text,
                                        TxtNombreClienteMinorista.Text,
                                        TxtDNI.Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblEmpaque")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblGrupo")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblPresentacion")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblMarca")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblSKU")).Text,
                                        ((Label)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("LblNombreProducto")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL DETALLES
                                }
                            }

                        }
                    }
                    #endregion INSERTAR DETALLES
                    #region INSERTAR PREMIOS
                    if (GvDigitacionHojaControlAbarrotes_Premios.Rows.Count > 0)
                    {

                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            if (GvDigitacionHojaControlAbarrotes_Premios.Columns[2].Visible == true)
                            {
                                if (((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text != "")
                                {
                                    continuar1 = true;
                                    #region INSERTAR HOJA DE CONTROL PREMIOS
                                    oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_INSERTAR_FORMATO_HOJACONTROL_CANJE_MULTICATEGORIA_PREMIO",
                                        Convert.ToInt32(Txt_ID_HDCanjeMulticategoriaCAB.Text),
                                        Convert.ToInt32(TxtNumRegistro_DET.Text),
                                        ((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("Lbl_Id_Premio")).Text,
                                        ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text
                                        );
                                    #endregion INSERTAR HOJA DE CONTROL PREMIOS
                                }
                            }
                        }

                    }
                    #endregion INSERTAR PREMIOS
                    if (continuar == true && continuar1 == false)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "No ha registrado ningún dato para almacenar según formulario";
                        Mensajes_Usuario();
                    }

                    if (continuar == true && continuar1 == true)
                    {

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "Se almacenó la información registrada";
                        Mensajes_Usuario();


                        LimpiarFiltrosDetalles();

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes.Rows[i].Cells[0].FindControl("TxtN1")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes

                        #region LIMPIAR GvDigitacionHojaControlAbarrotes_Premios
                        for (int i = 0; i <= GvDigitacionHojaControlAbarrotes_Premios.Rows.Count - 1; i++)
                        {
                            ((TextBox)GvDigitacionHojaControlAbarrotes_Premios.Rows[i].Cells[0].FindControl("TxtCantidad_Premio")).Text = "";
                        }
                        #endregion LIMPIAR GvDigitacionHojaControlAbarrotes_Premios

                    }
                }
                #endregion FORMATO COMPETENCIA_MAYORISTA, ID=19

            }
         
        }
        #endregion EVENTO CLICK: BOTON GUARDAR DETALLE: FILTRO DETALLE
        #region EVENTO CLICK: BOTON CANCELAR DETALLE: FILTRO DETALLE
        protected void ImgCancelarDetalle_Click(object sender, ImageClickEventArgs e)
        {
            cancelarActivarbotones();
            InicializarFiltros();
            HabilitarObjetos();
        }
        #endregion EVENTO CLICK: BOTON CANCELAR DETALLE: FILTRO DETALLE

        #endregion EVENTOS CLICK: FILTRO DETALLES : BOTONES


        #region EVENTO : SELECT INDEX: ELIMINAR GVDigitacion
        protected void GvDigitacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblMensajeConfirm.Text = "Realmente desea eliminar el registro No. " + ((Label)GvDigitacion.Rows[GvDigitacion.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text + "?";
            ModalConfirmacion.Show();
        }
        #endregion EVENTO : SELECT INDEX: ELIMINAR GVDigitacion
        #region EVENTO : SELECT INDEX: ELIMINAR GVDigitacionPrecios
        protected void GVDigitacionPrecios_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblMensajeConfirm.Text = "Realmente desea eliminar el registro No. " + ((Label)GVDigitacionPrecios.Rows[GVDigitacionPrecios.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text + "?";
            ModalConfirmacion.Show();
        }
        #endregion EVENTO : SELECT INDEX: ELIMINAR GVDigitacionPrecios
        #region EVENTO : SELECT INDEX: ELIMINAR GvDigitacionDG
        protected void GvDigitacionDG_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblMensajeConfirm.Text = "Realmente desea eliminar el registro No. " + ((Label)GvDigitacionDG.Rows[GvDigitacionDG.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text + "?";
            ModalConfirmacion.Show();
        }
        #endregion EVENTO : SELECT INDEX: ELIMINAR GvDigitacionDG
        #region EVENTO : SELECT INDEX: ELIMINAR GvDigitacionAGCocina
        protected void GvDigitacionAGCocina_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblMensajeConfirm.Text = "Realmente desea eliminar el registro No. " + ((Label)GvDigitacionAGCocina.Rows[GvDigitacionAGCocina.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text + "?";
            ModalConfirmacion.Show();
        }
        #endregion EVENTO : SELECT INDEX: ELIMINAR GvDigitacionAGCocina
        #region EVENTO : SELECT INDEX: ELIMINAR GvDigitacionMimaskot
        protected void GvDigitacionMimaskot_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblMensajeConfirm.Text = "Realmente desea eliminar el registro No. " + ((Label)GvDigitacionMimaskot.Rows[GvDigitacionMimaskot.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text + "?";
            ModalConfirmacion.Show();
        }
        #endregion EVENTO : SELECT INDEX: ELIMINAR GvDigitacionMimaskot
        #region EVENTO : SELECT INDEX: ELIMINAR GvDigitacionPromGalletas
        protected void GvDigitacionPromGalletas_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblMensajeConfirm.Text = "Realmente desea eliminar el registro No. " + ((Label)GvDigitacionPromGalletas.Rows[GvDigitacionPromGalletas.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text + "?";
            ModalConfirmacion.Show();
        }
        #endregion EVENTO : SELECT INDEX: ELIMINAR GvDigitacionPromGalletas       
        #region EVENTO : SELECT INDEX: ELIMINAR GvDigitacionHojaControlAbarrotes
        protected void GvDigitacionHojaControlAbarrotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblMensajeConfirm.Text = "¿Desea eliminar el registro?";
            ModalConfirmacion.Show();
        }
        #endregion EVENTO : SELECT INDEX: ELIMINAR GvDigitacionHojaControlAbarrotes
        #region EVENTO : SELECT INDEX: ELIMINAR GvDigitacionHojaControlAbarrotes_Premios
        protected void GvDigitacionHojaControlAbarrotes_Premios_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblMensajeConfirm.Text = "¿Desea eliminar el registro?";
            ModalPopupConfirmacion3.Show();

        }
        #endregion EVENTO : SELECT INDEX: ELIMINAR GvDigitacionHojaControlAbarrotes_Premios
        #region EVENTO : SELECT INDEX: CONSULTAR GvIntermedia
        protected void GvIntermedia_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblMensajeConfirmaConsulta.Text = "¿Desea Consultar el registro seleccionado?";
            ModalConfirmacion2.Show();
        }
        #endregion EVENTO : SELECT INDEX: CONSULTAR GvIntermedia

        #region POPUP: ModalConfirmacion - BOTON ELIMINAR: SI / NO
        protected void BtnSiConfirma_Click(object sender, EventArgs e)
        {
            #region FORMATO_FINAL_SOD
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINAL_SOD"])
            {
                DataTable dt = oOPE_DigFORMATOFINALSOD.EliminarOPE_DigFORMATOFINALSOD(Convert.ToInt64(((Label)GvDigitacion.Rows[GvDigitacion.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));

                DataTable dtDigitacion = oOPE_DigFORMATOFINALSOD.ConsultarOPE_DigFORMATOFINALSOD(
                    TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                     Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                    TxtCliente.Text, Convert.ToInt64(CmbCiudad.SelectedValue));

                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacion.DataSource = dtDigitacion;
                        GvDigitacion.DataBind();
                        GvDigitacion.SelectedIndex = -1;
                    }
                }
                else
                {
                    GvDigitacion.DataBind();
                    consultarActivarbotones();
                    ImgCancelEditDigitacion.Visible = true;
                    ImgCancelDigitacion.Visible = false;
                    GvDigitacion.SelectedIndex = -1;

                }
            }
            #endregion
            #region FORMATO_FINALES_PRECIOS
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_PRECIOS"])
            {
                DataTable dt = oOPE_DigFORMATOFINALESPRECIOS.EliminarOPE_DigFORMATOFINALESPRECIOS(Convert.ToInt64(((Label)GVDigitacionPrecios.Rows[GVDigitacionPrecios.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));

                DataTable dtDigitacion = oOPE_DigFORMATOFINALESPRECIOS.ConsultarOPE_DigFORMATOFINALESPRECIOS(
                    TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                    Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                   Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GVDigitacionPrecios.DataSource = dtDigitacion;
                        GVDigitacionPrecios.DataBind();
                        GVDigitacionPrecios.SelectedIndex = -1;
                    }
                }
                else
                {
                    GVDigitacionPrecios.DataBind();
                    consultarActivarbotones();
                    ImgCancelEditDigitacion.Visible = true;
                    ImgCancelDigitacion.Visible = false;
                    GVDigitacionPrecios.SelectedIndex = -1;
                }
            }
            #endregion
            #region FORMATOS FINALES DG
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_DG"])
            {
                DataTable dt = oOPE_DigFORMATOFINALESDG.EliminarFormatoFinalesDG(Convert.ToInt64(((Label)GvDigitacionDG.Rows[GvDigitacionDG.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));

                DataTable dtDigitacion = oOPE_DigFORMATOFINALESDG.ConsultarFormatoFinalesDG(
                    TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                    Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                   Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionDG.DataSource = dtDigitacion;
                        GvDigitacionDG.DataBind();
                        GvDigitacionDG.SelectedIndex = -1;
                    }
                }
                else
                {
                    GvDigitacionDG.DataBind();
                    consultarActivarbotones();
                    ImgCancelEditDigitacion.Visible = true;
                    ImgCancelDigitacion.Visible = false;
                    GvDigitacionDG.SelectedIndex = -1;
                }
            }
            #endregion
            #region FORMATOS AG COCINA
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_AGCOCINA"])
            {
                DataTable dt = oOPE_DigFORMATOAGCOCINA.EliminarFormatoAGCOCINA(Convert.ToInt64(((Label)GvDigitacionAGCocina.Rows[GvDigitacionAGCocina.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));
                DataTable dtDigitacion = oOPE_DigFORMATOAGCOCINA.ConsultarFormatoAGCOCINA(
                    TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                    Convert.ToDateTime(TxtFecha.Text), Convert.ToDateTime(TxtFechaFin.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                   Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionAGCocina.DataSource = dtDigitacion;
                        GvDigitacionAGCocina.DataBind();
                        GvDigitacionAGCocina.SelectedIndex = -1;
                    }
                }
                else
                {
                    GvDigitacionAGCocina.DataBind();
                    consultarActivarbotones();
                    ImgCancelEditDigitacion.Visible = true;
                    ImgCancelDigitacion.Visible = false;
                    GvDigitacionAGCocina.SelectedIndex = -1;
                }
            }
            #endregion
            #region FORMATOS MIMASKOT
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_MIMASKOT"])
            {

                // CREAR METODO PARA ELIMINAR MIMASKOT Y ELIMINAR COMENTARIO DE ABAJO E INCVOCAR METODO DE CONSULTA MIMASKOT
                DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_MIMASKOT", Convert.ToInt64(((Label)GvDigitacionMimaskot.Rows[GvDigitacionMimaskot.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_MIMASKOT",
                    TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                   Convert.ToInt32(CmbOperativo.SelectedValue), TxtLugarDireccion.Text);

                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionMimaskot.DataSource = dtDigitacion;
                        GvDigitacionMimaskot.DataBind();
                        GvDigitacionMimaskot.SelectedIndex = -1;
                    }
                    else
                    {
                        GvDigitacionMimaskot.DataBind();
                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionMimaskot.SelectedIndex = -1;
                    }
                }
            }
            #endregion
            #region FORMATO PRECIOS CUIDADO DEL CABELLO
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PRECIOS_CUIDADOCABELLO"])
            {
                DataTable dt = oOPE_DigFORMATOFINALESPRECIOS.EliminarOPE_DigFORMATOFINALESPRECIOS(Convert.ToInt64(((Label)GVDigitacionPrecios.Rows[GVDigitacionPrecios.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));

                DataTable dtDigitacion = oOPE_DigFORMATOFINALESPRECIOS.ConsultarOPE_DigFORMATOFINALESPRECIOS(
                    TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                    Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                   Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GVDigitacionPrecios.DataSource = dtDigitacion;
                        GVDigitacionPrecios.DataBind();
                        GVDigitacionPrecios.SelectedIndex = -1;
                    }
                }
                else
                {
                    GVDigitacionPrecios.DataBind();
                    consultarActivarbotones();
                    ImgCancelEditDigitacion.Visible = true;
                    ImgCancelDigitacion.Visible = false;
                    GVDigitacionPrecios.SelectedIndex = -1;
                }
            }
            #endregion
            #region FORMATOS DIAS GIRO CUIDADO DEL CABELLO
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_DIASGIRO"])
            {
                DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_DIASGIRO",
                    Convert.ToInt64(((Label)GvDigitacionAGCocina.Rows[GvDigitacionAGCocina.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));

                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_DIASGIRO",
                       TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text),
                       Convert.ToDateTime(TxtFecha.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                      Convert.ToInt32(CmbZonaMayor.Text), TxtCliente.Text);

                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionAGCocina.DataSource = dtDigitacion;
                        GvDigitacionAGCocina.DataBind();
                        GvDigitacionAGCocina.SelectedIndex = -1;
                    }
                }
                else
                {
                    GvDigitacionAGCocina.DataBind();
                    consultarActivarbotones();
                    ImgCancelEditDigitacion.Visible = true;
                    ImgCancelDigitacion.Visible = false;
                    GvDigitacionAGCocina.SelectedIndex = -1;
                }
            }
            #endregion
            #region FORMATOS NUTRICAN
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_NUTRICAN"])
            {
                DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_MIMASKOT", Convert.ToInt64(((Label)GvDigitacionMimaskot.Rows[GvDigitacionMimaskot.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_MIMASKOT",
                    TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                   Convert.ToInt32(CmbOperativo.SelectedValue), TxtLugarDireccion.Text);

                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionMimaskot.DataSource = dtDigitacion;
                        GvDigitacionMimaskot.DataBind();
                        GvDigitacionMimaskot.SelectedIndex = -1;
                    }
                    else
                    {
                        GvDigitacionMimaskot.DataBind();
                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionMimaskot.SelectedIndex = -1;
                    }
                }
            }
            #endregion
            #region FORMATOS PROMOCION GALLETAS
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PROMOCION_GALLETAS"])
            {
                DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_PROMGALLETAS", Convert.ToInt64(((Label)GvDigitacionPromGalletas.Rows[GvDigitacionPromGalletas.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));

                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_PROMGALLETAS",
                      TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                      Convert.ToDateTime(TxtFechaFin.Text), Convert.ToInt32(CmbOperativo.SelectedValue),
                      CmbMercado.Text, Convert.ToInt64(CmbCiudad.SelectedValue), TxtCliente.Text);

                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionPromGalletas.DataSource = dtDigitacion;
                        GvDigitacionPromGalletas.DataBind();
                        GvDigitacionPromGalletas.SelectedIndex = -1;
                    }
                    else
                    {
                        GvDigitacionPromGalletas.DataBind();
                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionPromGalletas.SelectedIndex = -1;
                    }
                }
            }
            #endregion
            #region FORMATOS AB MASCOTAS - INFORME SEMANAL
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASINFSEM"])
            {
                DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_ABMASCOTAS", Convert.ToInt64(((Label)GvDigitacionMimaskot.Rows[GvDigitacionMimaskot.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));

                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_ABMASCOTAS",
                    TxtCodPlanning.Text, Convert.ToInt32(CmbFormato.Text), Convert.ToDateTime(TxtFecha.Text),
                    Convert.ToDateTime(TxtFechaFin.Text), Convert.ToInt32(CmbOperativo.SelectedValue), TxtLugarDireccion.Text, TxtCliente.Text);


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionMimaskot.DataSource = dtDigitacion;
                        GvDigitacionMimaskot.DataBind();
                        GvDigitacionMimaskot.SelectedIndex = -1;
                    }
                    else
                    {
                        GvDigitacionMimaskot.DataBind();
                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionMimaskot.SelectedIndex = -1;
                    }
                }
            }
            #endregion
          
            #region FORMATOS HCCANJEMULTICATEGORIA 
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
            {
                //DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_ABMASCOTAS", Convert.ToInt64(((Label)GvDigitacionMimaskot.Rows[GvDigitacionMimaskot.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));
                DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_HCCANJEMULTICATEGORIA_DET", Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes.Rows[GvDigitacionHojaControlAbarrotes.SelectedRow.RowIndex].Cells[0].FindControl("LblNumDet")).Text));                
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        #region SUP CABECERA - BOTONES
                        ImgCancelDigitacion.Visible = false;
                        #endregion
                        
                        #region CABECERA - BOTONES
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = false;//BtnEditar
                        ImgUpdateDigitacion.Visible = true;//BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false;//BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion
                        
                        
                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;


                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        consultarActivarbotones();


                        #region SUP CABECERA - BOTONES
                        ImgCancelDigitacion.Visible = true;
                        #endregion

                        #region CABECERA - BOTONES
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false;//BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false;//BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                        
                    }
                }

            }
            #endregion

            #region FORMATOS ABMASCOTASHOJACONTROL
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASHOJACONTROL"])
            {
                //DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_ABMASCOTAS", Convert.ToInt64(((Label)GvDigitacionMimaskot.Rows[GvDigitacionMimaskot.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));
                DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_HCCANJEMULTICATEGORIA_DET", Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes.Rows[GvDigitacionHojaControlAbarrotes.SelectedRow.RowIndex].Cells[0].FindControl("LblNumDet")).Text));
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;


                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;
                    }
                }
            }
            #endregion FORMATOS ABMASCOTASHOJACONTROL

            #region FORMATO RESUMEN_CANJE_ANUA
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_ANUA"])
            {
                //DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_ABMASCOTAS", Convert.ToInt64(((Label)GvDigitacionMimaskot.Rows[GvDigitacionMimaskot.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));
                DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_HCCANJEMULTICATEGORIA_DET", Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes.Rows[GvDigitacionHojaControlAbarrotes.SelectedRow.RowIndex].Cells[0].FindControl("LblNumDet")).Text));
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;


                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;
                    }
                }
            }
            #endregion  FORMATO RESUMEN_CANJE_ANUA

            #region FORMATO CANJE_ANUA
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_CANJE_ANUA"])
            {
                //DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_ABMASCOTAS", Convert.ToInt64(((Label)GvDigitacionMimaskot.Rows[GvDigitacionMimaskot.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));
                DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_HCCANJEMULTICATEGORIA_DET", Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes.Rows[GvDigitacionHojaControlAbarrotes.SelectedRow.RowIndex].Cells[0].FindControl("LblNumDet")).Text));
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;


                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;
                    }
                }
            }
            #endregion FORMATO CANJE_ANUA

            #region FORMATO HDC_CANJE
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
            { }
            #endregion

            #region FORMATO HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar"])
            {
                //DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_ABMASCOTAS", Convert.ToInt64(((Label)GvDigitacionMimaskot.Rows[GvDigitacionMimaskot.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));
                DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_HCCANJEMULTICATEGORIA_DET", Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes.Rows[GvDigitacionHojaControlAbarrotes.SelectedRow.RowIndex].Cells[0].FindControl("LblNumDet")).Text));
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;


                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;
                    }
                }
            }
            #endregion FORMATO HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar

            #region FORMATO_RESUMEN_CANJE_PLUSBELLE
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_PLUSBELLE"])
            {
                //DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_ABMASCOTAS", Convert.ToInt64(((Label)GvDigitacionMimaskot.Rows[GvDigitacionMimaskot.SelectedRow.RowIndex].Cells[0].FindControl("LblNo")).Text));
                DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_HCCANJEMULTICATEGORIA_DET", Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes.Rows[GvDigitacionHojaControlAbarrotes.SelectedRow.RowIndex].Cells[0].FindControl("LblNumDet")).Text));
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgEditDigitacion.Visible = false;
                        ImgUpdateDigitacion.Visible = true;
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;


                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;
                    }
                }
            }
            #endregion FORMATO_RESUMEN_CANJE_PLUSBELLE

            #region FORMATO COMPETENCIA_ANUA
         
            #endregion FORMATO COMPETENCIA_ANUA

            #region FORMATO COMPETENCIA_MAYORISTA
          
            #endregion FORMATO COMPETENCIA_MAYORISTA

            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupConfirm";
            this.Session["mensaje"] = "Se Eliminó el registro correctamente";
            Mensajes_Usuario();
        }
        protected void BtnNoConfirma_Click(object sender, EventArgs e)
        {
            ModalConfirmacion.Hide();
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINAL_SOD"])
            {
                GvDigitacion.SelectedIndex = -1;
            }
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_PRECIOS"])
            {
                GVDigitacionPrecios.SelectedIndex = -1;
            }
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_FINALES_DG"])
            {
                GvDigitacionDG.SelectedIndex = -1;
            }
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_AGCOCINA"])
            {
                GvDigitacionAGCocina.SelectedIndex = -1;
            }
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_MIMASKOT"])
            {
                GvDigitacionMimaskot.SelectedIndex = -1;
            }
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PRECIOS_CUIDADOCABELLO"])
            {
                GVDigitacionPrecios.SelectedIndex = -1;
            }
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_DIASGIRO"])
            {
                GvDigitacionAGCocina.SelectedIndex = -1;
            }
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_NUTRICAN"])
            {
                GvDigitacionMimaskot.SelectedIndex = -1;
            }

            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_PROMOCION_GALLETAS"])
            {
                GvDigitacionPromGalletas.SelectedIndex = -1;
            }
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASINFSEM"])
            {
                GvDigitacionMimaskot.SelectedIndex = -1;
            }
        }
        #endregion POPUP: ModalConfirmacion - BOTON ELIMINAR: SI / NO
        
        #region POPUP: ModalConfirmacion2 - SELECCIONAR DESDE GRILLA gvIntermedia : BOTON : SI / NO - VALIDO SOLO PARA [GvIntermedia]
        protected void BtnNoConfirmaConsulta_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// BtnSiConfirmaConsulta para HDCCANJEMULTICATEGORIA ModalPopupConsulta ModalPopup2
        /// MOSTRAR LA INFORMACION DE LA HDCCANJEMULTICATEGORIA TANTO DE GvDigitacionHojaControlAbarrotes y de GvDigitacionHojaControlAbarrotes_Premios
        /// psalas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSiConfirmaConsulta_Click(object sender, EventArgs e)
        {

            #region FORMATO_ABMASCOTASHOJACONTROL
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASHOJACONTROL"])
            {
                #region CONSULTAR LOS PRODUCTOS DE ACUERDO A LA SELECCION
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgHabEditDigitacion.Visible = true;

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion


                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes.Columns[8].Visible = true;

                        GvDigitacionHojaControlAbarrotes.Enabled = false;
                        GvDigitacionHojaControlAbarrotes.Visible = true;


                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        consultarActivarbotones();

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                    }
                }
                #endregion

                #region CONSULTAR LOS PREMIOS DE ACUERDO A LA SELECCION
                DataSet dsDigitacion = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));

                GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                if (dsDigitacion != null)
                {
                    if (dsDigitacion.Tables[1].Rows.Count > 0)
                    {

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                        GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = true;

                        GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                        GvDigitacionHojaControlAbarrotes_Premios.Enabled = false;

                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                    }
                }
                #endregion
            }
            #endregion

            #region FORMATOS HCCANJEMULTICATEGORIA -
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
            {
                #region CONSULTAR LOS PRODUCTOS DE ACUERDO A LA SELECCION
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgHabEditDigitacion.Visible = true;

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion


                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes.Columns[8].Visible = true;
                        
                        GvDigitacionHojaControlAbarrotes.Enabled = false;
                        GvDigitacionHojaControlAbarrotes.Visible = true;

                        
                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        consultarActivarbotones();

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                    }
                }
                #endregion 

                #region CONSULTAR LOS PREMIOS DE ACUERDO A LA SELECCION
                DataSet dsDigitacion = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));
                
                GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                GvDigitacionHojaControlAbarrotes_Premios.DataBind();
             
                if (dsDigitacion != null)
                {
                    if (dsDigitacion.Tables[1].Rows.Count > 0)
                    {

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion 

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                        GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = true;

                        GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                        GvDigitacionHojaControlAbarrotes_Premios.Enabled = false;
 
                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                    }
                }
                #endregion
            }
            #endregion

            #region FORMATO RESUMEN DE CANJE ANUA
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_ANUA"])
            {
                #region CONSULTAR LOS PRODUCTOS DE ACUERDO A LA SELECCION 
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgHabEditDigitacion.Visible = true;

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion


                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes.Columns[8].Visible = true;

                        GvDigitacionHojaControlAbarrotes.Enabled = false;
                        GvDigitacionHojaControlAbarrotes.Visible = true;


                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        consultarActivarbotones();

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                    }
                }
                #endregion

                #region CONSULTAR LOS PREMIOS DE ACUERDO A LA SELECCION
                DataSet dsDigitacion = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));

                GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                if (dsDigitacion != null)
                {
                    if (dsDigitacion.Tables[1].Rows.Count > 0)
                    {

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                        GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = true;

                        GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                        GvDigitacionHojaControlAbarrotes_Premios.Enabled = false;

                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                    }
                }
                #endregion
            }
            #endregion

            #region FORMATO DE CANJE ANUA
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_CANJE_ANUA"])
            {
                #region CONSULTAR LOS PRODUCTOS DE ACUERDO A LA SELECCION 
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgHabEditDigitacion.Visible = true;

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion


                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes.Columns[8].Visible = true;

                        GvDigitacionHojaControlAbarrotes.Enabled = false;
                        GvDigitacionHojaControlAbarrotes.Visible = true;


                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        consultarActivarbotones();

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                    }
                }
                #endregion

                #region CONSULTAR LOS PREMIOS DE ACUERDO A LA SELECCION
                DataSet dsDigitacion = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));

                GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                if (dsDigitacion != null)
                {
                    if (dsDigitacion.Tables[1].Rows.Count > 0)
                    {

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                        GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = true;

                        GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                        GvDigitacionHojaControlAbarrotes_Premios.Enabled = false;

                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                    }
                }
                #endregion
            }
            #endregion

            #region FORMATO HDC CANJE - IMPLEMENTAR SEGUN MODELO
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE"])
            {

                #region CONSULTAR LOS PRODUCTOS DE ACUERDO A LA SELECCION
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgHabEditDigitacion.Visible = true;

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion


                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes.Columns[8].Visible = true;

                        GvDigitacionHojaControlAbarrotes.Enabled = false;
                        GvDigitacionHojaControlAbarrotes.Visible = true;


                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        consultarActivarbotones();

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                    }
                }
                #endregion

                #region CONSULTAR LOS PREMIOS DE ACUERDO A LA SELECCION
                DataSet dsDigitacion = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));

                GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                if (dsDigitacion != null)
                {
                    if (dsDigitacion.Tables[1].Rows.Count > 0)
                    {

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                        GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = true;

                        GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                        GvDigitacionHojaControlAbarrotes_Premios.Enabled = false;

                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                    }
                }
                #endregion
            }
            #endregion

            #region FORMATO HDC CANJE - STOCK DE PREMIOS TIRA 1 millar--IMPLEMENTAR SEGUN MODELO
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar"])
            {
                #region CONSULTAR LOS PRODUCTOS DE ACUERDO A LA SELECCION
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgHabEditDigitacion.Visible = true;

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion


                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes.Columns[8].Visible = true;

                        GvDigitacionHojaControlAbarrotes.Enabled = false;
                        GvDigitacionHojaControlAbarrotes.Visible = true;


                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        consultarActivarbotones();

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                    }
                }
                #endregion

                #region CONSULTAR LOS PREMIOS DE ACUERDO A LA SELECCION
                DataSet dsDigitacion = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));

                GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                if (dsDigitacion != null)
                {
                    if (dsDigitacion.Tables[1].Rows.Count > 0)
                    {

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                        GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = true;

                        GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                        GvDigitacionHojaControlAbarrotes_Premios.Enabled = false;

                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                    }
                }
                #endregion
            }
            #endregion

            #region FORMATO_RESUMEN_CANJE_PLUSBELLE
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_PLUSBELLE"])
            {
                #region CONSULTAR LOS PRODUCTOS DE ACUERDO A LA SELECCION
                DataTable dtDigitacion = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));


                if (dtDigitacion != null)
                {
                    if (dtDigitacion.Rows.Count > 0)
                    {
                        ImgHabEditDigitacion.Visible = true;

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion


                        GvDigitacionHojaControlAbarrotes.DataSource = dtDigitacion;
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes.Columns[8].Visible = true;

                        GvDigitacionHojaControlAbarrotes.Enabled = false;
                        GvDigitacionHojaControlAbarrotes.Visible = true;


                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes.DataBind();
                        GvDigitacionHojaControlAbarrotes.SelectedIndex = -1;

                        consultarActivarbotones();

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                    }
                }
                #endregion

                #region CONSULTAR LOS PREMIOS DE ACUERDO A LA SELECCION
                DataSet dsDigitacion = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
                Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));

                GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                if (dsDigitacion != null)
                {
                    if (dsDigitacion.Tables[1].Rows.Count > 0)
                    {

                        #region SUP
                        ImgCancelDigitacion.Visible = false;
                        #endregion

                        #region CAB
                        ImgEditDigitacion.Visible = false;//BtnConsultar
                        ImgHabEditDigitacion.Visible = true;//BtnEditar
                        ImgUpdateDigitacion.Visible = false; //BtnActualizar
                        ImgGuardarCabeceraOpeDigitacion.Visible = false; //BtnGuardar
                        ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                        #endregion

                        GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                        GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = true;

                        GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                        GvDigitacionHojaControlAbarrotes_Premios.Enabled = false;

                    }
                    else
                    {
                        GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                        consultarActivarbotones();
                        ImgCancelEditDigitacion.Visible = true;
                        ImgCancelDigitacion.Visible = false;
                        GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                    }
                }
                #endregion
            }
            #endregion

            #region FORMATO COMPETENCIA ANUA
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_ANUA"])
            { 
            
            }
            #endregion

            #region FORMATO COMPETENCIA_MAYORISTA
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_MAYORISTA"])
            { }
            #endregion
            

            //this.Session["encabemensa"] = "Sr. Usuario";
            //this.Session["cssclass"] = "MensajesSupConfirm";
            //this.Session["mensaje"] = "Se Hizo la consulta del registro correctamente";
 
            //Mensajes_Usuario();
        }
        #endregion POPUP: ModalConfirmacion2 - BOTON CONSULTAR : SI / NO - VALIDO SOLO PARA [GvIntermedia]
        
        #region POPUP: ModalPopupConfirmacion3 - BOTON ELIMINAR: SI/NO - VALIDO PARA [GvDigitacionHojaControlAbarrotes_Premios]
        
//METODO ELIMINAR REGISTRO DE PREMIOS DE LA Grilla
        private void Eliminar_Premios_GvPremos() {
            DataTable DT = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DIG_ELIMINAR_FORMATO_HCCANJEMULTICATEGORIA_PREMIO", Convert.ToInt32(((Label)GvDigitacionHojaControlAbarrotes_Premios.Rows[GvDigitacionHojaControlAbarrotes_Premios.SelectedRow.RowIndex].Cells[0].FindControl("Lbl_ID_HCCCABxPremio")).Text));

            DataSet dsDigitacion = oCoon.ejecutarDataSet("UP_WEBXPLORA_OPE_DIG_CONSULTAR_FORMATO_HCCANJEMULTICATEGORIA_ULTIMO",
           Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumCab")).Text),
           Convert.ToInt32(((Label)GvIntermedia.Rows[GvIntermedia.SelectedRow.RowIndex].Cells[0].FindControl("LblNumReg")).Text));

            GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
            GvDigitacionHojaControlAbarrotes_Premios.DataBind();

            if (dsDigitacion != null)
            {
                if (dsDigitacion.Tables[1].Rows.Count > 0)
                {

                    ImgCancelDigitacion.Visible = false;

                    #region CABECERA - BOTONES
                    ImgHabEditDigitacion.Visible = true;//BtnEditar
                    ImgUpdateDigitacion.Visible = false;//BtnActualizar
                    ImgCancelEditDigitacion.Visible = true;//BtnCancelar
                    ImgGuardarCabeceraOpeDigitacion.Visible = false;//BtnGuardar
                    ImgEditDigitacion.Visible = false;//BtnConsultar
                    #endregion


                    GvDigitacionHojaControlAbarrotes_Premios.Columns[4].Visible = true;

                    GvDigitacionHojaControlAbarrotes_Premios.DataSource = dsDigitacion.Tables[1];
                    GvDigitacionHojaControlAbarrotes_Premios.DataBind();
                    GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;
                    GvDigitacionHojaControlAbarrotes_Premios.Visible = true;
                    GvDigitacionHojaControlAbarrotes_Premios.Enabled = true;


                }
                else
                {
                    GvDigitacionHojaControlAbarrotes_Premios.DataBind();

                    consultarActivarbotones();
                    ImgCancelEditDigitacion.Visible = true;
                    ImgCancelDigitacion.Visible = false;
                    GvDigitacionHojaControlAbarrotes_Premios.SelectedIndex = -1;

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "No hay Detalles para este registro";
                    Mensajes_Usuario();


                }
            }

        } 

        protected void BtnSiConfirmaConfirmacion3_Click(object sender, EventArgs e)
        {

            #region FORMATO_ABMASCOTASHOJACONTROL
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_ABMASCOTASHOJACONTROL"])
            {
                Eliminar_Premios_GvPremos();
            }
            #endregion

            #region FORMATOS HCCANJEMULTICATEGORIA -
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATOHDCABARROTES_ROTACIONMULTICATEGORIA"])
            {
                Eliminar_Premios_GvPremos();
            }
             #endregion
            #region FORMATO_RESUMEN_CANJE_ANUA -
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_ANUA"])
            {
                Eliminar_Premios_GvPremos();
            }
            #endregion
            #region FORMATO_CANJE_ANUA -
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_CANJE_ANUA"])
            {
                Eliminar_Premios_GvPremos();
            }
            #endregion
            #region FORMATO_HDC_CANJE -
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE"])
            {
                Eliminar_Premios_GvPremos();
            }
            #endregion
            #region FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar -
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_HDC_CANJE_STOCK_PREMIOS_TIRA_1_millar"])
            {
                Eliminar_Premios_GvPremos();
            }
            #endregion
            #region FORMATO_RESUMEN_CANJE_PLUSBELLE
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["FORMATO_RESUMEN_CANJE_PLUSBELLE"])
            {
                Eliminar_Premios_GvPremos();
            }
            #endregion
            #region COMPETENCIA_ANUA
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_ANUA"])
            {
            }
            #endregion
            #region COMPETENCIA_MAYORISTA
            if (CmbFormato.SelectedItem.Text == ConfigurationManager.AppSettings["COMPETENCIA_MAYORISTA"])
            { }
            #endregion
        }

        protected void BtnNoConfirmaConfirmacion3_Click(object sender, EventArgs e)
        {

        }
        #endregion POPUP: ModalPopupConfirmacion3 - BOTON ELIMINAR: SI/NO - VALIDO PARA [GvDigitacionHojaControlAbarrotes_Premios]
        

        #region SIN CONTENIDO
        protected void CmbZonaMayor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion SIN CONTENIDO

        

        ///
        private void Mostrar_Todas_Columnas_GVIntermedia() {
            GvIntermedia.Columns[1].Visible = true;
            GvIntermedia.Columns[2].Visible = true;
            GvIntermedia.Columns[3].Visible = true;
            GvIntermedia.Columns[4].Visible = true;
            GvIntermedia.Columns[5].Visible = true;
            GvIntermedia.Columns[6].Visible = true;
            GvIntermedia.Columns[7].Visible = true;
            GvIntermedia.Columns[8].Visible = true;
        }
    }
}