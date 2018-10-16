using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Business.Common.Application;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;
using Lucky.CFG.Util;
using System.Collections.Generic;

namespace SIGE.Pages.Modulos.Planning
{
    /// <summary>
    /// Seguimiento Planning.
    /// Developed by: 
    /// - Pablo Salas Alvarez (PSA)
    /// Changes:
    /// - 2018-10-15 (PSA) Refactoring.
    /// </summary>
    public partial class Seguimiento_Planning : System.Web.UI.Page
    {
        private DateTime fechaSolicitudP;
        private DateTime fechaFinalP;
        private DateTime fechaIniPreP;
        private DateTime fechaIniPre;
        private DateTime fechaFinPreP;
        private DateTime fechaIniPlaP;
        private DateTime fechaPlaFinP;
        private bool Postback = true;
        
        private Staff_Planning Staff_Planning = new Staff_Planning();
        private PuntosDV PuntosDV = new PuntosDV();
        private Conexion oCoon = new Conexion();
        private PointOfSale_PlanningOper PointOfSale_PlanningOper = new PointOfSale_PlanningOper();

        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos PAdmin = 
            new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private Facade_Proceso_Planning.Facade_Proceso_Planning PPlanning = 
            new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        private Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE Presupuesto = 
            new SIGE.Facade_Interface_EasyWin.Facade_Info_EasyWin_for_SIGE();
        private Facade_Menu_strategy.Facade_MPlanning menu = 
            new SIGE.Facade_Menu_strategy.Facade_MPlanning();
        private Facade_Proceso_Cliente.Facade_Proceso_Cliente wsCliente = 
            new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();
                
        private int level_carga;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                try
                {
                    /*pstring sUser = this.Session["sUser"].ToString();
                    string sPassw = this.Session["sPassw"].ToString();
                    string sNameUser = this.Session["nameuser"].ToString();
                    lblUsuario.Text = sNameUser;
                    if (sUser != null && sPassw != null)*/
                        llena_planning();
                    /*
                    if(this.Session["companyid"].ToString().Trim()=="1562" || this.Session["companyid"].ToString().Trim()=="1561")
                    {

                        MenuProductoAncla.Visible = true;
                    }*/
                }
                catch (Exception ex)
                {
                    string error = "";
                    string mensaje = "";
                    error = Convert.ToString(ex.Message);
                    mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                    if (error.Equals(mensaje))
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

        /// <summary>
        /// Ocultar todos los ModalPanel
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private void InicializarPaneles()
        {
            // Modal para Asignar Presupuesto
            ModalPanelASignaPresupuesto.Hide();
            // Modal para Descripción de Campania
            ModalPanelDescCampaña.Hide();
            // Modal para Responsables de Campania
            ModalPanelResponsablesCampaña.Hide();
            // Modal para Asignar Personal
            ModalPanelAsignaPersonal.Hide();
            // Modal para Asignar Puntos de Venta
            ModalPanelPDV.Hide();
            // Modal para Asignar Paneles
            ModalPanelPaneles.Hide();
            // Modal para Asignar Pdv por Usuario
            ModalPanelAsignacionPDVaoper.Hide();
            // Modal para Asignar Productos
            ModalPanelProductos.Hide();
            // Modal para Asignar Reportes
            ModalPanelReportPlan.Hide();
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
            txtobj.Text = "";
            txtmanda.Text = "";
            Txtmeca.Text = "";
            txtcontacto.Text = "";
            txtarea.Text = "";
        }
        private void Limpiar_InformacionAsignaPDVOPE()
        {
            Button1.Visible = false;
            BtnAllAsigPDV.Visible = false;
            BtnNoneasigPDV.Visible = false;
            GvAsignaPDVOPE.DataBind();
            GvNewAsignaPDVOPE.DataBind();
            ChkListPDV.Items.Clear();
            TxtF_iniPDVOPE.Text = "";
            TxtF_finPDVOPE.Text = "";
            CmbSelCityRutas.Items.Clear();
            CmbSelTipoAgrupRutas.Items.Clear();
            CmbSelAgrupRutas.Items.Clear();
            CmbSelOficinaRutas.Items.Clear();
            CmbSelMallasRutas.Items.Clear();
            CmbSelSectorRutas.Items.Clear();
            ChkListPDV.Items.Clear();
            ConsultaPDVCampañaRutas();
            llenaOperativosAsignaPDVOPE();
            CmbSelOpePlanning.Enabled = false;
            CmbSelCityRutas.Enabled = false;
            CmbSelTipoAgrupRutas.Enabled = false;
            CmbSelAgrupRutas.Enabled = false;
            CmbSelOficinaRutas.Enabled = false;
            CmbSelMallasRutas.Enabled = false;
            CmbSelSectorRutas.Enabled = false;
            ChkListPDV.Enabled = false;
            TxtF_iniPDVOPE.Enabled = false;
            TxtF_finPDVOPE.Enabled = false;
            BtnCalF_iniPDVOPE.Enabled = false;
            BtnCalF_finPDVOPE.Enabled = false;
            BtnAsigPDVOPE.Enabled = false;

            BtnEditAsigPDVOPE.Visible = true;
            BtnUpdateAsigPDVOPE.Visible = false;
        }

        private void Mensajes_Seguimiento()
        {
            MensajeSeguimiento.CssClass = this.Session["cssclass"].ToString();
            lblencabezadoSeguimiento.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneralSeguimiento.Text = this.Session["mensaje"].ToString();
            MPMensajeSeguimiento.Show();
        }
        private void Mensajes_AsignacionPresupuesto()
        {
            MensajeAsignacionPresupuesto.CssClass = this.Session["cssclass"].ToString();
            lblencabezado.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneral.Text = this.Session["mensaje"].ToString();
            MPMensajeAsignacionPresupuesto.Show();
        }

        /// <summary>
        /// Muestra el ModalPopUp Correspondiente a las validaciones de la Campaña
        /// </summary>
        /// <param name=""> None </param>
        /// <returns>None</returns>
        private void Mensajes_DescripcionCampaña()
        {
            MensajeDescripcionCampaña.CssClass = this.Session["cssclass"].ToString();
            lblencabezadoDesc.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneralDesc.Text = this.Session["mensaje"].ToString();
            ModalPanelDescCampaña.Show();
            MPMensajeDescripcionCampaña.Show();
        }

        private void Mensajes_PuntosdeVenta()
        {
            MensajePuntosDV.CssClass = this.Session["cssclass"].ToString();
            lblencabezadoMPDV.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneralMPDV.Text = this.Session["mensaje"].ToString();
            ModalPanelPDV.Show();
            MPMensajeMPDV.Show();
        }

        private void Mensajes_ResponsablesCampaña()
        {
            MensajeResponsablesCampaña.CssClass = this.Session["cssclass"].ToString();
            lblencabezadoResp.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneralResp.Text = this.Session["mensaje"].ToString();
            ModalPanelResponsablesCampaña.Show();
            MPMensajeResponsablesCampaña.Show();
        }
        private void Mensajes_AsigPDVOPE()
        {
            MensajeAsignaPDVOPE.CssClass = this.Session["cssclass"].ToString();
            lblencabezadoPDVOPE.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneralPDVOPE.Text = this.Session["mensaje"].ToString();
            ModalPanelAsignacionPDVaoper.Show();
            MPMensajeAsignaPDVOPE.Show();
        }
        private void Mensajes_Productos()
        {
            MensajeProductos.CssClass = this.Session["cssclass"].ToString();
            lblencabezadoProductos.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneralProductos.Text = this.Session["mensaje"].ToString();
            MPMensajeProductos.Show();
        }
        private void Mensajes_SeguimientoValidacionVistas()
        {
            PMensajeVista.CssClass = this.Session["cssclass"].ToString();
            LblTitMensajeVista.Text = this.Session["encabemensa"].ToString();
            LblMsjMensajeVista.Text = this.Session["mensaje"].ToString();
            ModalMensajeVista.Show();
        }
        private void Mensajes_Reportes()
        {
            MensajeAsignaReports.CssClass = this.Session["cssclass"].ToString();
            lblencabezadoReports.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneralReports.Text = this.Session["mensaje"].ToString();
            ModalPanelReportes.Show();
            MPMensajeAsignaReports.Show();
        }
        private void Mensajes_ReportesDel()
        {
            MensajeAsignaRepDel.CssClass = this.Session["cssclass"].ToString();
            lblencabezadoRepDel.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneralRepDel.Text = this.Session["mensaje"].ToString();

            MPMensajeAsignaRepDel.Show();
        }
        private void Mensajes_Paneles()
        {
            MensajePaneles.CssClass = this.Session["cssclass"].ToString();
            lblencabezadoPaneles.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneralPaneles.Text = this.Session["mensaje"].ToString();
            ModalPanelPaneles.Show();
            MPMensajePaneles.Show();
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
                        Mensajes_AsignacionPresupuesto();
                    }
                    if (fechaSolicitudP > fechaIniPlaP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La Fecha de solicitud de la campaña  no puede ser mayor a la fecha de inicio de ejecución";
                        Mensajes_AsignacionPresupuesto();

                    }
                    else if (fechaSolicitudP > fechaFinalP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de solicitud de la campaña  no puede ser mayor a la fecha de entrega final";
                        Mensajes_AsignacionPresupuesto();
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
                Mensajes_AsignacionPresupuesto();
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

             
                if (fechaFinalP < fechaSolicitudP)
                {
                    valido = false;
                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "La fecha de entrega final no pude ser menor a la fecha de solicitud";
                    Mensajes_AsignacionPresupuesto();

                }
                else if (fechaFinalP < fechaIniPreP)
                {
                    valido = false;
                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";

                    this.Session["mensaje"] = "La fecha de entrega final no pude ser menor a la fecha de inicio de Preproducción";
                    Mensajes_AsignacionPresupuesto();

                }
                else
                    if (fechaFinalP < fechaFinPreP)
                    {
                        valido = false;
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de entrega final no pude ser menor a la fecha de fin de Preproducción";
                        Mensajes_AsignacionPresupuesto();

                    }
                    else if (fechaFinalP < fechaIniPlaP)
                    {
                        valido = false;
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de entrega final no pude ser menor a la fecha de inicio de ejecución";
                        Mensajes_AsignacionPresupuesto();
                    }
                    else
                        if (fechaFinalP < fechaPlaFinP)
                        {
                            valido = false;
                            this.Session["encabemensa"] = "Señor Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";

                            this.Session["mensaje"] = "La fecha de entrega final no pude ser menor a la fecha de fin de ejecución";
                            Mensajes_AsignacionPresupuesto();
                        }
                return valido;
            }
            catch
            {
                valido = false;
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
                Mensajes_AsignacionPresupuesto();
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

                    if (fechaIniPreP > fechaFinPreP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La fecha de inicio de Preproducción no puede ser mayor que la fecha de fin de Preproducción";
                        Mensajes_AsignacionPresupuesto();
                        return false;
                    }

                    if (fechaIniPreP > fechaIniPlaP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La Fecha de inicio de Preproducción no puede ser mayor que la fecha de inicio del Plannning";
                        Mensajes_AsignacionPresupuesto();
                        return false;

                    }
                    if (fechaIniPreP > fechaFinalP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de inicio de Preproducción no puede ser mayor que la fecha de entrega final";
                        Mensajes_AsignacionPresupuesto();
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
                Mensajes_AsignacionPresupuesto();
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

                    if (fechaFinPreP < fechaSolicitudP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser mayor que la fecha de solicitud";
                        Mensajes_AsignacionPresupuesto();
                        return false;
                    }

                    if (fechaFinPreP > fechaIniPlaP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser mayor que la fecha de inicio del Plannning";
                        Mensajes_AsignacionPresupuesto();
                        return false;

                    }
                    if (fechaFinPreP > fechaFinalP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser mayor quela fecha de entrega final";
                        Mensajes_AsignacionPresupuesto();
                        return false;

                    }

                    if (fechaFinPreP > fechaPlaFinP)
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "La fecha de fin de Preproducción no puede ser mayor quela fecha fin de ejecución";
                        Mensajes_AsignacionPresupuesto();
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
                Mensajes_AsignacionPresupuesto();
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
                    Mensajes_AsignacionPresupuesto();
                    return false;
                }
                return true;
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
                Mensajes_AsignacionPresupuesto();
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
                    Mensajes_AsignacionPresupuesto();
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
                Mensajes_AsignacionPresupuesto();
                return false;
            }
        }

        // datos completos 
        private bool datoscompletosInformacionBasica()
        {
            if (txt_FechaSolicitud.Text == "" ||
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

                Mensajes_AsignacionPresupuesto();
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Verificar que los datos obligatorios para Actualizar la Descripción de las Campañas estén completos
        /// </summary>
        /// <param name=""> None </param>
        /// <returns>Bool</returns>
        private bool datoscompletosDescripcionCampaña()
        {
            // Obtener los valores del Formulario
            txtobj.Text = txtobj.Text.TrimStart();
            txtmanda.Text = txtmanda.Text.TrimStart();
            Txtmeca.Text = Txtmeca.Text.TrimStart();
            txtcontacto.Text = txtcontacto.Text.TrimStart();
            txtarea.Text = txtarea.Text.TrimStart();

            // Validar si cualquiera de los valores que se han obtenido por Formulario son diferentes de vacio
            if (txtobj.Text == "" 
                || txtmanda.Text == "" 
                || Txtmeca.Text == "" 
                || txtcontacto.Text == "" 
                || txtarea.Text == ""){

                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";

                // Almacenar en una session el mensaje de usuario correspondiente al Error
                if (txtarea.Text == "") this.Session["mensaje"] = "Debe ingresar el área involucrada";
                if (txtcontacto.Text == "") this.Session["mensaje"] = "Debe ingresar el nombre del contacto"; 
                if (Txtmeca.Text == "") this.Session["mensaje"] = "Debe ingresar la mecanica de la campaña";
                if (txtmanda.Text == "") this.Session["mensaje"] = "Debe ingresar los mandatorios de la campaña";
                if (txtobj.Text == "") this.Session["mensaje"] = "Debe ingresar el objetivo de la campaña"; 
                
                // Mostrar el ModalPopUp correspondiente al mensaje de error producido
                Mensajes_DescripcionCampaña();

                return false;

            }else{
                return true;
            }
        }
        private bool datoscompletosProductos()
        {
            bool sigue = true;
            string vista = this.Session["vista_final"].ToString().Trim();
           
            if (sigue)
            {
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

                if (vista.Equals("SubFamilia"))
                {
                    if (ChkListSubFamilias.SelectedIndex == -1)
                    {
                        this.Session["mensaje"] = "Debe seleccionar por lo menos una subfamilia";
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

        //Llenar objetos
        
        private void ConsultaAsignacionBudget()
        {
            
            #region Data de Prueba ( )
            //***********************
            /*
            LblTxtPresupuesto.Text = "111111";
            txtnumpla.Text = "1111111";
            Rblisstatus.Items[0].Selected = true;
            Rblisstatus.Items[1].Selected = false;
            txtnamepresu.Text = "22222222";
            txt_FechaSolicitud.Text = "01-01-2000";
            txt_FechaEntrega.Text = "01-02-2000";
            txtcliente.Text = "33333";
            txtcliente.ToolTip = "33333";

            txt_FechainiPre.Text = "01-01-2000";
            txt_Fechafinpre.Text = "01-02-2000";

            txtservice.Text = "Mercaderismo";
            txtservice.ToolTip = "Mercaderismo02";
            txt_FechainiPla.Text = "01-01-2000";
            txt_FechaPlafin.Text = "01-02-2000";
            TxtDuracion.Text = "Mercaderismo02";

            LlenaCanales();
            */
            //***********************
            #endregion

            DataSet ds = new DataSet();
            /// Retorna un DataSet con la información del Planning Seleccionado
            /// como se muestra a continuación:
            /// DataTable[0] - Obtiene información para llenar combo de Planning
            /// DataTable[1] - Obtiene información de los objetivos para la Campaña
            /// DataTable[2] - Obtiene información de los Productos con SKU Mandatorio
            /// DataTable[3] - Obtiene información de la Mecánica de las Actividades
            /// DataTable[4] - Obtiene información del Personal Asignado a la Campaña
            /// DataTable[5] - Obtiene información de la asginación de Mercaderista y Supervisores
            /// DataTable[6] - Obtiene información de los PDVs de la Campania
            /// DataTable[7] - Obtiene información de los Productos Asignados por Campania 
            /// DataTable[8] - Obtiene información de los Puntos de Venta Asignados a los Mercaderistas
            /// DataTable[9] - Obtiene información del Planning Seleccionado
            /// DataTable[10]- Obtiene información de los Reportes asignados a la Campania
            /// DataTable[11]- Obtiene información de los Paneles Asignados a la Campania
            /// DataTable[12]- Obtiene información de las Marcas Asignadas a la Campania
            /// DataTable[12]- Obtiene información de las Familias Asignadas a la Campania
            /// DataTable[13]- Obtiene información de las Familias Asignadas a la Categorias
            ds = PPlanning.Get_PlanningCreados(CmbSelCampaña.SelectedValue);

            //Se cambia el objeto ds por el datatable generado anteriormente. Consulta volvia a repetir proceso.
            DataTable dtplanning = ((DataSet)this.ViewState["planning_creados"]).Tables[9];

            if (dtplanning.Rows.Count > 0)
            {
                LblTxtPresupuesto.Text = dtplanning.Rows[0]["Planning_Name"].ToString().Trim();
                txtnumpla.Text = dtplanning.Rows[0]["id_planning"].ToString().Trim();

                if (dtplanning.Rows[0]["Planning_Status"].ToString().Trim() == "True")
                {
                    Rblisstatus.Items[0].Selected = true;
                    Rblisstatus.Items[1].Selected = false;
                }
                else
                {
                    Rblisstatus.Items[0].Selected = false;
                    Rblisstatus.Items[1].Selected = true;
                }

                txtnamepresu.Text = dtplanning.Rows[0]["Planning_Name"].ToString().Trim();
                txt_FechaSolicitud.Text = dtplanning.Rows[0]["Planning_DateRepSoli"].ToString().Trim();
                txt_FechaEntrega.Text = dtplanning.Rows[0]["Planning_DateFinreport"].ToString().Trim();

                //se llena el control con el nombre del cliente asociado al presupuesto seleccionado - Ing. Mauricio Ortiz
                DataTable dtCliente = Presupuesto.Get_ObtenerClientes(dtplanning.Rows[0]["Planning_Budget"].ToString().Trim(), 1);
                this.Session["company_id"] = dtCliente.Rows[0]["Company_id"].ToString().Trim();
                txtcliente.Text = dtCliente.Rows[0]["Company_Name"].ToString().ToUpperInvariant();
                txtcliente.ToolTip = dtCliente.Rows[0]["Company_Name"].ToString().ToUpperInvariant();


                txt_FechainiPre.Text = dtplanning.Rows[0]["Planning_PreproduDateini"].ToString().Trim();
                txt_Fechafinpre.Text = dtplanning.Rows[0]["Planning_PreproduDateEnd"].ToString().Trim();

                txtservice.Text = dtplanning.Rows[0]["Strategy_Name"].ToString().Trim();
                txtservice.ToolTip = dtplanning.Rows[0]["Strategy_Name"].ToString().Trim();
                txt_FechainiPla.Text = dtplanning.Rows[0]["Planning_StartActivity"].ToString().Trim();
                txt_FechaPlafin.Text = dtplanning.Rows[0]["Planning_EndActivity"].ToString().Trim();
                TxtDuracion.Text = dtplanning.Rows[0]["Planning_ProjectDuration"].ToString().Trim();
                LlenaCanales();
                RbtnCanal.SelectedValue = dtplanning.Rows[0]["Planning_CodChannel"].ToString().Trim();
                dtCliente = null;
             
            }
            dtplanning = null;
            
        }

        /// <summary>
        /// Setea en el aspControl Panel 'PanelDescCampaña', la información de la Campaña.
        /// </summary>
        /// <param name=""> None </param>
        /// <returns>None</returns>
        /// DataTable[0] - Obtiene información para llenar combo de Planning
        /// DataTable[1] - Obtiene información de los objetivos para la Campaña
        /// DataTable[2] - Obtiene información de los Productos con SKU Mandatorio
        /// DataTable[3] - Obtiene información de la Mecánica de las Actividades
        /// DataTable[4] - Obtiene información del Personal Asignado a la Campaña
        /// DataTable[5] - Obtiene información de la asginación de Mercaderista y Supervisores
        /// DataTable[6] - Obtiene información de los PDVs de la Campania
        /// DataTable[7] - Obtiene información de los Productos Asignados por Campania 
        /// DataTable[8] - Obtiene información de los Puntos de Venta Asignados a los Mercaderistas
        /// DataTable[9] - Obtiene información del Planning Seleccionado
        /// DataTable[10]- Obtiene información de los Reportes asignados a la Campania
        /// DataTable[11]- Obtiene información de los Paneles Asignados a la Campania
        /// DataTable[12]- Obtiene información de las Marcas Asignadas a la Campania
        /// DataTable[13]- Obtiene información de las Familias Asignadas a la Campania
        /// DataTable[14]- Obtiene información de las Categorias Asignadas a la Campania
        private void ConsultadescripcionCampaña()
        {

            #region Codigo para Test
            /*
            TxtCodPlanningDesc.Text = "111111";
            LblTxtPresupuestoDesc.Text = "111111";
            txtobj.Text = "111111";
            txtmanda.Text = "111111";
            Txtmeca.Text = "111111";
            txtcontacto.Text = "111111";
            txtarea.Text = "111111";
            */
            #endregion

            DataSet ds = (DataSet)this.ViewState["planning_creados"];

            // Verifica si tiene más de un registro:
            /// DataTable[1] - Obtiene información de los objetivos para la Campaña
            /// DataTable[2] - Obtiene información de los Productos con SKU Mandatorio
            /// DataTable[3] - Obtiene información de la Mecánica de las Actividades
            if (ds.Tables[1].Rows.Count > 0 
                && ds.Tables[2].Rows.Count > 0 
                && ds.Tables[3].Rows.Count > 0){
                
                /// DataTable[9] - Obtiene información del Planning Seleccionado
                TxtCodPlanningDesc.Text = ds.Tables[9].Rows[0]["id_planning"].ToString().Trim();
                LblTxtPresupuestoDesc.Text = ds.Tables[9].Rows[0]["Planning_Name"].ToString().Trim();
                txtcontacto.Text = ds.Tables[9].Rows[0]["Name_Contact"].ToString().Trim();
                txtarea.Text = ds.Tables[9].Rows[0]["Planning_AreaInvolved"].ToString().Trim();
                
                /// DataTable[1] - Obtiene información de los objetivos para la Campaña
                txtobj.Text = ds.Tables[1].Rows[0]["objPlaDescription"].ToString().Trim();

                /// DataTable[2] - Obtiene información de los Productos con SKU Mandatorio
                txtmanda.Text = ds.Tables[2].Rows[0]["MandtoryDescription"].ToString().Trim();

                /// DataTable[3] - Obtiene información de la Mecánica de las Actividades
                Txtmeca.Text = ds.Tables[3].Rows[0]["MechanicalActivity_Description"].ToString().Trim();

            }
            ds = null;

        } //revisado

        class TestObject {
            public string Person_id { get; set; }
            public string name_user { get; set; }
        }
        
        
        private void ConsultaResponsablesCamapaña()
        {

            TestObject oTestObject01 = new TestObject() { Person_id = "01", name_user = "Ejecutivos01" };
            TestObject oTestObject02 = new TestObject() { Person_id = "02", name_user = "Ejecutivos02" };
            TestObject oTestObject03 = new TestObject() { Person_id = "03", name_user = "Ejecutivos03" };
            TestObject oTestObject04 = new TestObject() { Person_id = "04", name_user = "Ejecutivos04" };
            TestObject oTestObject05 = new TestObject() { Person_id = "05", name_user = "Ejecutivos05" };
            TestObject oTestObject06 = new TestObject() { Person_id = "06", name_user = "Ejecutivos06" };
            TestObject oTestObject07 = new TestObject() { Person_id = "07", name_user = "Ejecutivos07" };
            TestObject oTestObject08 = new TestObject() { Person_id = "08", name_user = "Ejecutivos08" };
            TestObject oTestObject09 = new TestObject() { Person_id = "09", name_user = "Ejecutivos09" };
            TestObject oTestObject10 = new TestObject() { Person_id = "10", name_user = "Ejecutivos10" };
            TestObject oTestObject11 = new TestObject() { Person_id = "11", name_user = "Ejecutivos11" };
            TestObject oTestObject12 = new TestObject() { Person_id = "12", name_user = "Ejecutivos12" };
            List<TestObject> oList = new List<TestObject>();
            oList.Add(oTestObject01);
            oList.Add(oTestObject02);
            oList.Add(oTestObject03);
            oList.Add(oTestObject04);
            oList.Add(oTestObject05);
            oList.Add(oTestObject06);
            oList.Add(oTestObject07);
            oList.Add(oTestObject08);
            oList.Add(oTestObject09);
            oList.Add(oTestObject10);
            oList.Add(oTestObject11);
            oList.Add(oTestObject12);

            TestObject oTestObject101 = new TestObject() { Person_id = "01", name_user = "Mercaderista01" };
            TestObject oTestObject102 = new TestObject() { Person_id = "02", name_user = "Mercaderista02" };
            TestObject oTestObject103 = new TestObject() { Person_id = "03", name_user = "Mercaderista03" };
            TestObject oTestObject104 = new TestObject() { Person_id = "04", name_user = "Mercaderista04" };
            TestObject oTestObject105 = new TestObject() { Person_id = "05", name_user = "Mercaderista05" };
            TestObject oTestObject106 = new TestObject() { Person_id = "06", name_user = "Mercaderista06" };
            TestObject oTestObject107 = new TestObject() { Person_id = "07", name_user = "Mercaderista07" };
            TestObject oTestObject108 = new TestObject() { Person_id = "08", name_user = "Mercaderista08" };
            TestObject oTestObject109 = new TestObject() { Person_id = "09", name_user = "Mercaderista09" };
            TestObject oTestObject110 = new TestObject() { Person_id = "10", name_user = "Mercaderista10" };
            TestObject oTestObject111 = new TestObject() { Person_id = "11", name_user = "Mercaderista11" };
            TestObject oTestObject112 = new TestObject() { Person_id = "12", name_user = "Mercaderista12" };
            List<TestObject> oList2 = new List<TestObject>();
            oList2.Add(oTestObject101);
            oList2.Add(oTestObject102);
            oList2.Add(oTestObject103);
            oList2.Add(oTestObject104);
            oList2.Add(oTestObject105);
            oList2.Add(oTestObject106);
            oList2.Add(oTestObject107);
            oList2.Add(oTestObject108);
            oList2.Add(oTestObject109);
            oList2.Add(oTestObject110);
            oList2.Add(oTestObject111);
            oList2.Add(oTestObject112);

            TestObject oTestObject201 = new TestObject() { Person_id = "01", name_user = "Supervisor01" };
            TestObject oTestObject202 = new TestObject() { Person_id = "02", name_user = "Supervisor02" };
            TestObject oTestObject203 = new TestObject() { Person_id = "03", name_user = "Supervisor03" };
            TestObject oTestObject204 = new TestObject() { Person_id = "04", name_user = "Supervisor04" };
            TestObject oTestObject205 = new TestObject() { Person_id = "05", name_user = "Supervisor05" };
            TestObject oTestObject206 = new TestObject() { Person_id = "06", name_user = "Supervisor06" };
            TestObject oTestObject207 = new TestObject() { Person_id = "07", name_user = "Supervisor07" };
            TestObject oTestObject208 = new TestObject() { Person_id = "08", name_user = "Supervisor08" };
            TestObject oTestObject209 = new TestObject() { Person_id = "09", name_user = "Supervisor09" };
            TestObject oTestObject210 = new TestObject() { Person_id = "10", name_user = "Supervisor10" };
            TestObject oTestObject211 = new TestObject() { Person_id = "11", name_user = "Supervisor11" };
            TestObject oTestObject212 = new TestObject() { Person_id = "12", name_user = "Supervisor12" };
            List<TestObject> oList3 = new List<TestObject>();
            oList3.Add(oTestObject201);
            oList3.Add(oTestObject202);
            oList3.Add(oTestObject203);
            oList3.Add(oTestObject204);
            oList3.Add(oTestObject205);
            oList3.Add(oTestObject206);
            oList3.Add(oTestObject207);
            oList3.Add(oTestObject208);
            oList3.Add(oTestObject209);
            oList3.Add(oTestObject210);
            oList3.Add(oTestObject211);
            oList3.Add(oTestObject212);


            GvEjecutivosAsignados.DataSource = oList;
            GvEjecutivosAsignados.DataBind();

            GvMercaderistasAsignados.DataSource = oList2;
            GvMercaderistasAsignados.DataBind();
            
            GvSupervisoresAsignados.DataSource = oList3;
            GvSupervisoresAsignados.DataBind();


            /*
            DataSet ds = (DataSet)this.ViewState["planning_creados"];
            if (ds.Tables[4].Rows.Count > 0)
            {
                TxtPlanningRes.Text = ds.Tables[9].Rows[0]["id_planning"].ToString().Trim();
                LblTxtPresupuestoRes.Text = ds.Tables[9].Rows[0]["Planning_Name"].ToString().Trim();

                DataSet dsPersonal = PPlanning.Get_Staff_Planning(TxtPlanningRes.Text);
                GvEjecutivosAsignados.DataSource = dsPersonal.Tables[2];
                GvEjecutivosAsignados.DataBind();

                GvMercaderistasAsignados.DataSource = dsPersonal.Tables[1];
                GvMercaderistasAsignados.DataBind();

                GvSupervisoresAsignados.DataSource = dsPersonal.Tables[0];
                GvSupervisoresAsignados.DataBind();
                dsPersonal = null;
            }
            ds = null;
            */
        } //revisado

        private void ConsultaPDVCampaña()
        {
            DataSet ds = (DataSet)this.ViewState["planning_creados"];
            if (ds.Tables[6].Rows.Count > 0)
            {
                TxtPlanningPDV.Text = ds.Tables[9].Rows[0]["id_planning"].ToString().Trim();
                LblTxtPresupuestoPDV.Text = ds.Tables[9].Rows[0]["Planning_Name"].ToString().Trim();
                DataTable dtCliente = Presupuesto.Get_ObtenerClientes(ds.Tables[9].Rows[0]["Planning_Budget"].ToString().Trim(), 1);
                this.Session["company_id"] = dtCliente.Rows[0]["Company_id"].ToString().Trim();
                this.Session["Planning_CodChannel"] = ds.Tables[9].Rows[0]["Planning_CodChannel"].ToString().Trim();
                llenaciudades();
                GvPDV.DataBind();
                GVPDVDelete.DataBind();
                dtCliente = null;
                this.Session["InsertaConsultaPDV"] = "true";
                this.Session["InsertaConsultaPDVPresupuesto"] = ds.Tables[9].Rows[0]["Planning_Budget"].ToString().Trim();
            }
            ds = null;
        } //revisado
        private void ConsultaPDVCampañaRutas()
        {
            DataSet ds = (DataSet)this.ViewState["planning_creados"];
            if (ds.Tables[6].Rows.Count > 0)
            {
                TxtPlanningAsigPDVOPE.Text = ds.Tables[9].Rows[0]["id_planning"].ToString().Trim();
                LblTxtPresupuestoAsigPDVOPE.Text = ds.Tables[9].Rows[0]["Planning_Name"].ToString().Trim();
                DataTable dtCliente = Presupuesto.Get_ObtenerClientes(ds.Tables[9].Rows[0]["Planning_Budget"].ToString().Trim(), 1);
                this.Session["company_id"] = dtCliente.Rows[0]["Company_id"].ToString().Trim();
                this.Session["Planning_CodChannel"] = ds.Tables[9].Rows[0]["Planning_CodChannel"].ToString().Trim();
                //llenaciudadesRutas();
                //CmbSelTipoAgrupRutas.Items.Clear();
                //CmbSelAgrupRutas.Items.Clear();
                //CmbSelOficinaRutas.Items.Clear();
                //CmbSelMallasRutas.Items.Clear();
                //CmbSelSectorRutas.Items.Clear();
                //ChkListPDV.Items.Clear();
                
                DataTable dtAsigna_PDV_A_OPE_Temp = new DataTable();
                dtAsigna_PDV_A_OPE_Temp.Columns.Add("Cod_", typeof(Int32));
                dtAsigna_PDV_A_OPE_Temp.Columns.Add("Mercaderista", typeof(String));
                dtAsigna_PDV_A_OPE_Temp.Columns.Add("Cod.", typeof(Int32));
                dtAsigna_PDV_A_OPE_Temp.Columns.Add("Punto_de_Venta", typeof(String));
                dtAsigna_PDV_A_OPE_Temp.Columns.Add("Desde", typeof(String));
                dtAsigna_PDV_A_OPE_Temp.Columns.Add("Hasta", typeof(String));
                GvNewAsignaPDVOPE.DataSource = dtAsigna_PDV_A_OPE_Temp;
                GvNewAsignaPDVOPE.DataBind();

                this.Session["dtAsigna_PDV_A_OPE_Temp"] = dtAsigna_PDV_A_OPE_Temp;
                DataTable dt = PPlanning.ObtenerIdPlanning(ds.Tables[9].Rows[0]["Planning_Budget"].ToString().Trim());
                
                this.Session["Fechainicial"] = dt.Rows[0]["Planning_StartActivity"].ToString().Trim();
                this.Session["Fechafinal"] = dt.Rows[0]["Planning_EndActivity"].ToString().Trim();
                
                dtCliente = null;
                dt = null;
            }
            ds = null;
        } //revisado
        private void ConsultaPDVXoperativo()
        {
            DataTable DTConsulta = PointOfSale_PlanningOper.Consultar_PDVPlanningXoperativo(TxtPlanningAsigPDVOPE.Text, Convert.ToInt32(CmbSelOpePlanning.Text));
            GvAsignaPDVOPE.DataSource = DTConsulta;
            GvAsignaPDVOPE.DataBind();
            DTConsulta = null;
        } //revisado
        private void ConsultaProductosCampaña()
        {
            DataSet ds = (DataSet)this.ViewState["planning_creados"];
            if (ds.Tables[7].Rows.Count > 0 || ds.Tables[12].Rows.Count > 0 || ds.Tables[13].Rows.Count > 0 || ds.Tables[14].Rows.Count > 0)
            {
                TxtPlanningAsigProd.Text = ds.Tables[9].Rows[0]["id_planning"].ToString().Trim();
                LblTxtPresupuestoAsigProd.Text = ds.Tables[9].Rows[0]["Planning_Name"].ToString().Trim();
                DataTable dtCliente = Presupuesto.Get_ObtenerClientes(ds.Tables[9].Rows[0]["Planning_Budget"].ToString().Trim(), 1);
                this.Session["company_id"] = dtCliente.Rows[0]["Company_id"].ToString().Trim();
                this.Session["Planning_CodChannel"] = ds.Tables[9].Rows[0]["Planning_CodChannel"].ToString().Trim();

                this.Session["id_planningProductos"] = TxtPlanningAsigProd.Text;
                this.Session["PresupuestoProductos"] = LblTxtPresupuestoAsigProd.Text;

                dtCliente = null;
                DataSet dsProductos = PPlanning.Get_ObtenerProductosPlanning(TxtPlanningAsigProd.Text, Convert.ToInt32(this.Session["company_id"].ToString().Trim()));

                gv_stockToExcel.DataSource = dsProductos.Tables[0];
                gv_stockToExcel.DataBind();
                gv_stockToExcel2.DataSource = dsProductos.Tables[1];
                gv_stockToExcel2.DataBind();
                gv_stockToExcel3.DataSource = dsProductos.Tables[2];
                gv_stockToExcel3.DataBind();
                gv_stockToExcel4.DataSource = dsProductos.Tables[3];
                gv_stockToExcel4.DataBind();
                gv_stockToExcel5.DataSource = dsProductos.Tables[4];
                gv_stockToExcel5.DataBind();
                gv_stockToExcel6.DataSource = dsProductos.Tables[6];
                gv_stockToExcel6.DataBind();


                gvproductospropios.DataSource = dsProductos.Tables[0];
                gvproductospropios.DataBind();
                if (this.Session["company_id"].ToString() == "1561")
                {
                 gvproductospropios.Columns[8].Visible = true;
                 gvproductospropios.Columns[7].Visible = true;
                }
                gvproductospropiosDEL.DataSource = dsProductos.Tables[0];
                gvproductospropiosDEL.DataBind();
       

                Gvproductoscompetidor.DataSource = dsProductos.Tables[1];
                Gvproductoscompetidor.DataBind();
                GvproductoscompetidorDEL.DataSource = dsProductos.Tables[1];
                GvproductoscompetidorDEL.DataBind();

                gvmarcaspropias.DataSource = dsProductos.Tables[2];
                gvmarcaspropias.DataBind();
                gvmarcaspropiasDEL.DataSource = dsProductos.Tables[2];
                gvmarcaspropiasDEL.DataBind();

                GvMarcascompetidor.DataSource = dsProductos.Tables[3];
                GvMarcascompetidor.DataBind();
                GvmarcascompetidorDEL.DataSource = dsProductos.Tables[3];
                GvmarcascompetidorDEL.DataBind();

                gvFamiliaspropias.DataSource = dsProductos.Tables[4];
                gvFamiliaspropias.DataBind();
                gvFamiliaspropiasDEL.DataSource = dsProductos.Tables[4];
                gvFamiliaspropiasDEL.DataBind();

                GvFamiliascompetidor.DataSource = dsProductos.Tables[5];
                GvFamiliascompetidor.DataBind();
                GvFamiliascompetidorDEL.DataSource = dsProductos.Tables[5];
                GvFamiliascompetidorDEL.DataBind();

                gvcategoriaspropias.DataSource = dsProductos.Tables[6];
                gvcategoriaspropias.DataBind();
                gvcategoriaspropiasDEL.DataSource = dsProductos.Tables[6];
                gvcategoriaspropiasDEL.DataBind();

                GvCategoriascompetidor.DataSource = dsProductos.Tables[7];
                GvCategoriascompetidor.DataBind();
                GvCategoriascompetidorDEL.DataSource = dsProductos.Tables[7];
                GvCategoriascompetidorDEL.DataBind();

                gvMatPOP.DataSource = dsProductos.Tables[8];
                gvMatPOP.DataBind();

                gvObservaciones.DataSource = dsProductos.Tables[9];
                gvObservaciones.DataBind();

                dsProductos = null;
            }
            ds = null;

        } //revisado
        private void ConsultaReportesCampaña()
        {
            BtnCarga20.Visible = false;
            BtnCarga80.Visible = false;
            DataSet ds = (DataSet)this.ViewState["planning_creados"];
            if (ds.Tables[10].Rows.Count > 0)
            {
                TxtPlanningAsigReportes.Text = ds.Tables[9].Rows[0]["id_planning"].ToString().Trim();
                TxtPlanningAsigReports.Text = ds.Tables[9].Rows[0]["id_planning"].ToString().Trim();
                LblTxtPresupuestoAsigReportes.Text = ds.Tables[9].Rows[0]["Planning_Name"].ToString().Trim();
                LblTxtPresupuestoAsigReports.Text = ds.Tables[9].Rows[0]["Planning_Name"].ToString().Trim();
                DataTable dtCliente = Presupuesto.Get_ObtenerClientes(ds.Tables[9].Rows[0]["Planning_Budget"].ToString().Trim(), 1);
                this.Session["company_id"] = dtCliente.Rows[0]["Company_id"].ToString().Trim();
                this.Session["Planning_CodChannel"] = ds.Tables[9].Rows[0]["Planning_CodChannel"].ToString().Trim();

                this.Session["id_planningReportes"] = TxtPlanningAsigReports.Text;
                this.Session["Presupuestoreportes"] = LblTxtPresupuestoAsigReports.Text;

                if (this.Session["company_id"].ToString().Trim() == "1562" && this.Session["Planning_CodChannel"].ToString().Trim() == "1000")
                {
                    BtnCarga20.Visible = true;
                    BtnCarga80.Visible = true;
                }

                dtCliente = null;
                DataTable dtReportes = PPlanning.Get_ConsultarReportesPlanning(TxtPlanningAsigReportes.Text);

                GVReportesAsignados.DataSource = dtReportes;
                GVReportesAsignados.DataBind();
                GVReportesAsignadosDEL.DataSource = dtReportes;
                GVReportesAsignadosDEL.DataBind();
                dtReportes = null;
                //llenameses(); esta función de dispara ahora desde la seleccion de un año. Ing. Mauricio Ortiz
                llenaaños();
                llenainformes();
            }
            ds = null;

        } //revisado
        private void ConsultaGvFrecuencias()
        {
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
        } //revisado


        protected void gvproductospropios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Conexion cn = new Conexion();
                string idproduct = gvproductospropios.Rows[e.RowIndex].Cells[1].Text;
                string flag = gvproductospropios.Rows[e.RowIndex].Cells[7].Text;

                if (flag == "M")
                {
                    cn.ejecutarDataTable("PLA_ACTUALIZAR_FLAGMANDATORIO", null, idproduct);
                }
                else
                {
                    cn.ejecutarDataTable("PLA_ACTUALIZAR_FLAGMANDATORIO", "M", idproduct);
                }

                ModalPanelProductos.Show();
                ConsultaProductosCampaña();
        

        }

        /// <summary>
        /// Obtiene los Planning por CompanyId
        /// </summary>
        /// <param name="iCompany_id">Id de la Compañia</param>
        /// <returns></returns>
        private void llena_planning(){
            
            DataTable dt = new DataTable(); ;
            // Metodo para Obtener los Presupustos Asignados
            dt = Presupuesto.Presupuesto_Search(Convert.ToInt32(this.Session["companyid"].ToString().Trim()));

            if (dt != null)
            {
                if (dt.Rows.Count > 1)
                {
                    CmbSelCampaña.DataSource = dt;
                    CmbSelCampaña.DataTextField = "name";
                    CmbSelCampaña.DataValueField = "id_planning";
                    CmbSelCampaña.DataBind();
                }
                else
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Usted está asignado a una Compañía que no tiene campañas creadas";
                    Mensajes_Seguimiento();
                }
            }

            #region Codigo de Pruebas
            /*
            ListItem listItem0 = new ListItem("PlanningPrueba00", "1");
            ListItem listItem1 = new ListItem("PlanningPrueba01", "1");
            ListItem listItem2 = new ListItem("PlanningPrueba02", "1");
            ListItem listItem3 = new ListItem("PlanningPrueba03", "1");
            ListItem listItem4 = new ListItem("PlanningPrueba04", "1");
            ListItem listItem5 = new ListItem("PlanningPrueba05", "1");
            ListItem listItem6 = new ListItem("PlanningPrueba06", "1");

            CmbSelCampaña.Items.Add(listItem0);
            CmbSelCampaña.Items.Add(listItem1);
            CmbSelCampaña.Items.Add(listItem2);
            CmbSelCampaña.Items.Add(listItem3);
            CmbSelCampaña.Items.Add(listItem4);
            CmbSelCampaña.Items.Add(listItem5);
            CmbSelCampaña.Items.Add(listItem6);

            CmbSelCampaña.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            */
            #endregion

        }
        private void LlenaCanales()
        {
            ListItem listItem1 = new ListItem("Mayorista", "1");
            ListItem listItem2 = new ListItem("Minorista", "2");
            ListItem listItem3 = new ListItem("Autoservicios", "3");
            RbtnCanal.Items.Add(listItem1);
            RbtnCanal.Items.Add(listItem2);
            RbtnCanal.Items.Add(listItem3);
            /*
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
            */
        }
        private void llenaciudades()
        {
            DataSet ds = PPlanning.Get_cityPointofsalePlanning(TxtPlanningPDV.Text, "0", 0, "0", 0, 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                CmbSelCity.DataSource = ds.Tables[0];

                CmbSelCity.DataValueField = "cod_city";
                CmbSelCity.DataTextField = "name_city";
                CmbSelCity.DataBind();
                CmbSelCity.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "No se han creado puntos de venta para la campaña: " + LblTxtPresupuestoPDV.Text.ToUpper();
                // falta mensaje de usuario en pdv Mensajes_SeguimientoValidacionVistas();

            }
            ds = null;
            CmbSelTipoAgrup.Items.Clear();
            CmbSelAgrup.Items.Clear();
            CmbSelOficina.Items.Clear();
            CmbSelMalla.Items.Clear();
            GvPDV.DataBind();
            GVPDVDelete.DataBind();


        }
        private void llenaTipoAgrup()
        {
            DataSet ds = PPlanning.Get_cityPointofsalePlanning(TxtPlanningPDV.Text, CmbSelCity.Text, 0, "0", 0, 0);

            CmbSelTipoAgrup.DataSource = ds.Tables[1];

            CmbSelTipoAgrup.DataValueField = "idNodeComType";
            CmbSelTipoAgrup.DataTextField = "NodeComType_name";
            CmbSelTipoAgrup.DataBind();
            CmbSelTipoAgrup.Items.Insert(0, new ListItem("<Seleccione..>", "0"));


            ds = null;
            CmbSelAgrup.Items.Clear();
            CmbSelOficina.Items.Clear();
            CmbSelMalla.Items.Clear();
            CmbSelSector.Items.Clear();
            GvPDV.DataBind();
            GVPDVDelete.DataBind();

        }
        private void llenaAgrup()
        {
            DataSet ds = PPlanning.Get_cityPointofsalePlanning(TxtPlanningPDV.Text, CmbSelCity.Text, Convert.ToInt32(CmbSelTipoAgrup.Text), "0", 0, 0);

            CmbSelAgrup.DataSource = ds.Tables[2];

            CmbSelAgrup.DataValueField = "NodeCommercial";
            CmbSelAgrup.DataTextField = "commercialNodeName";
            CmbSelAgrup.DataBind();
            CmbSelAgrup.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
            CmbSelOficina.Items.Clear();
            CmbSelMalla.Items.Clear();
            CmbSelSector.Items.Clear();
            GvPDV.DataBind();
            GVPDVDelete.DataBind();
        }
        private void llenaOficinas()
        {
            DataSet ds = PPlanning.Get_cityPointofsalePlanning(TxtPlanningPDV.Text, CmbSelCity.Text, Convert.ToInt32(CmbSelTipoAgrup.Text), CmbSelAgrup.Text, 0, 0);

            CmbSelOficina.DataSource = ds.Tables[3];

            CmbSelOficina.DataValueField = "cod_Oficina";
            CmbSelOficina.DataTextField = "Name_Oficina";
            CmbSelOficina.DataBind();
            CmbSelOficina.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
            CmbSelMalla.Items.Clear();
            CmbSelSector.Items.Clear();
            GvPDV.DataBind();
            GVPDVDelete.DataBind();
        }
        private void Llenamallas()
        {

            DataSet ds = PPlanning.Get_cityPointofsalePlanning(TxtPlanningPDV.Text, CmbSelCity.Text, Convert.ToInt32(CmbSelTipoAgrup.Text), CmbSelAgrup.Text, Convert.ToInt64(CmbSelOficina.Text), 0);

            CmbSelMalla.DataSource = ds.Tables[4];

            CmbSelMalla.DataValueField = "id_malla";
            CmbSelMalla.DataTextField = "malla";
            CmbSelMalla.DataBind();
            CmbSelMalla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
            CmbSelSector.Items.Clear();

        }
        private void Llenasector()
        {
            DataSet ds = PPlanning.Get_cityPointofsalePlanning(TxtPlanningPDV.Text, CmbSelCity.Text, Convert.ToInt32(CmbSelTipoAgrup.Text), CmbSelAgrup.Text, Convert.ToInt64(CmbSelOficina.Text), Convert.ToInt32(CmbSelMalla.Text));

            CmbSelSector.DataSource = ds.Tables[5];

            CmbSelSector.DataValueField = "id_sector";
            CmbSelSector.DataTextField = "Sector";
            CmbSelSector.DataBind();
            CmbSelSector.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void llenaGrillaPDV()
        {
            DataTable dtpdvplanning = null;
            dtpdvplanning = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_OBTENERPDVPLA", TxtPlanningPDV.Text, CmbSelCity.Text, Convert.ToInt32(CmbSelTipoAgrup.Text), CmbSelAgrup.Text, Convert.ToInt64(CmbSelOficina.Text), Convert.ToInt32(CmbSelMalla.Text), Convert.ToInt32(CmbSelSector.Text));

            // pendiente cambiar por este metodo
            //DataTable dtpdvplanning = PuntosDV.Consultar_PDVPlanningGeneral(TxtPlanningPDV.Text, Convert.ToInt32(CmbSelMalla.Text), Convert.ToInt32(CmbSelSector.Text));
            GvPDV.DataSource = dtpdvplanning;
            GvPDV.DataBind();
            GVPDVDelete.DataSource = dtpdvplanning;
            GVPDVDelete.DataBind();
            dtpdvplanning = null;
        }
        private void llenaOperativosAsignaPDVOPE()
        {
            DataSet dsStaffPlanning = PPlanning.Get_Staff_Planning(TxtPlanningAsigPDVOPE.Text);
            if (dsStaffPlanning != null)
            {
                if (dsStaffPlanning.Tables[1].Rows.Count > 0)
                {
                    CmbSelOpePlanning.DataSource = dsStaffPlanning.Tables[1];
                    CmbSelOpePlanning.DataTextField = "name_user";
                    CmbSelOpePlanning.DataValueField = "Person_id";
                    CmbSelOpePlanning.DataBind();
                    CmbSelOpePlanning.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                }
                else
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "No se ha seleccionado el personal de la campaña: " + LblTxtPresupuestoAsigPDVOPE.Text.ToUpper();
                    Mensajes_AsigPDVOPE();

                }
            }
            dsStaffPlanning = null;
        }
        private void llenaciudadesRutas()
        {
            DataSet ds = PPlanning.Get_cityPointofsalePlanning(TxtPlanningAsigPDVOPE.Text, "0", 0, "0", 0, 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                CmbSelCityRutas.DataSource = ds.Tables[0];
                CmbSelCityRutas.DataValueField = "cod_city";
                CmbSelCityRutas.DataTextField = "name_city";
                CmbSelCityRutas.DataBind();
                CmbSelCityRutas.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "No se han creado puntos de venta para la campaña: " + LblTxtPresupuestoPDV.Text.ToUpper();
                // falta mensaje de usuario en pdv Mensajes_SeguimientoValidacionVistas();
            }
            ds = null;
            CmbSelTipoAgrupRutas.Items.Clear();
            CmbSelAgrupRutas.Items.Clear();
            CmbSelOficinaRutas.Items.Clear();
            CmbSelMallasRutas.Items.Clear();
            ChkListPDV.Items.Clear();

        }
        private void llenaTipoAgrupRutas()
        {
            DataSet ds = PPlanning.Get_cityPointofsalePlanning(TxtPlanningAsigPDVOPE.Text, CmbSelCityRutas.Text, 0, "0", 0, 0);

            CmbSelTipoAgrupRutas.DataSource = ds.Tables[1];

            CmbSelTipoAgrupRutas.DataValueField = "idNodeComType";
            CmbSelTipoAgrupRutas.DataTextField = "NodeComType_name";
            CmbSelTipoAgrupRutas.DataBind();
            CmbSelTipoAgrupRutas.Items.Insert(0, new ListItem("<Seleccione..>", "0"));



            ds = null;
            CmbSelAgrupRutas.Items.Clear();
            CmbSelOficinaRutas.Items.Clear();
            CmbSelMallasRutas.Items.Clear();
            CmbSelSectorRutas.Items.Clear();
            ChkListPDV.Items.Clear();

        }
        private void llenaAgrupRutas()
        {
            DataSet ds = PPlanning.Get_cityPointofsalePlanning(TxtPlanningAsigPDVOPE.Text, CmbSelCityRutas.Text, Convert.ToInt32(CmbSelTipoAgrupRutas.Text), "0", 0, 0);

            CmbSelAgrupRutas.DataSource = ds.Tables[2];

            CmbSelAgrupRutas.DataValueField = "NodeCommercial";
            CmbSelAgrupRutas.DataTextField = "commercialNodeName";
            CmbSelAgrupRutas.DataBind();
            CmbSelAgrupRutas.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
            CmbSelOficinaRutas.Items.Clear();
            CmbSelMallasRutas.Items.Clear();
            CmbSelSectorRutas.Items.Clear();
            ChkListPDV.Items.Clear();
        }
        private void llenaOficinasRutas()
        {
            DataSet ds = PPlanning.Get_cityPointofsalePlanning(TxtPlanningAsigPDVOPE.Text, CmbSelCityRutas.Text, Convert.ToInt32(CmbSelTipoAgrupRutas.Text), CmbSelAgrupRutas.Text, 0, 0);

            CmbSelOficinaRutas.DataSource = ds.Tables[3];

            CmbSelOficinaRutas.DataValueField = "cod_Oficina";
            CmbSelOficinaRutas.DataTextField = "Name_Oficina";
            CmbSelOficinaRutas.DataBind();
            CmbSelOficinaRutas.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
            CmbSelMallasRutas.Items.Clear();
            CmbSelSectorRutas.Items.Clear();
            ChkListPDV.Items.Clear();
        }
        private void LlenamallasRutas()
        {
            DataSet ds = PPlanning.Get_cityPointofsalePlanning(TxtPlanningAsigPDVOPE.Text, CmbSelCityRutas.Text, Convert.ToInt32(CmbSelTipoAgrupRutas.Text), CmbSelAgrupRutas.Text, Convert.ToInt64(CmbSelOficinaRutas.Text), 0);

            CmbSelMallasRutas.DataSource = ds.Tables[4];

            CmbSelMallasRutas.DataValueField = "id_malla";
            CmbSelMallasRutas.DataTextField = "malla";
            CmbSelMallasRutas.DataBind();
            CmbSelMallasRutas.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
            CmbSelSectorRutas.Items.Clear();

        }
        private void LlenasectorRutas()
        {
            DataSet ds = PPlanning.Get_cityPointofsalePlanning(TxtPlanningAsigPDVOPE.Text, CmbSelCityRutas.Text, Convert.ToInt32(CmbSelTipoAgrupRutas.Text), CmbSelAgrupRutas.Text, Convert.ToInt64(CmbSelOficinaRutas.Text), Convert.ToInt32(CmbSelMallasRutas.Text));
            CmbSelSectorRutas.DataSource = ds.Tables[5];

            CmbSelSectorRutas.DataValueField = "id_sector";
            CmbSelSectorRutas.DataTextField = "Sector";
            CmbSelSectorRutas.DataBind();
            CmbSelSectorRutas.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void LlenaPDVPlanning()
        {
            // se coloca provisionalemnte con esto para seguir en crear ing. mauricio ortiz
            DataTable dt = PointOfSale_PlanningOper.Consultar_PDVPlanning(TxtPlanningAsigPDVOPE.Text, CmbSelCityRutas.Text, Convert.ToInt32(CmbSelTipoAgrupRutas.Text), CmbSelAgrupRutas.Text, Convert.ToInt64(CmbSelOficinaRutas.Text), Convert.ToInt32(CmbSelMallasRutas.Text), Convert.ToInt32(CmbSelSectorRutas.Text));
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
                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "No existe información de puntos de venta con este filtro. ";
                    Mensajes_AsigPDVOPE();
                    ChkListPDV.Items.Clear();
                    BtnAllPDV.Visible = false;
                    BtnNonePDV.Visible = false;
                    LlenamallasRutas();
                    LlenasectorRutas();
                }
            }
            dt = null;
        }
        private void llenacompetidores()
        {
            DataTable dtCompetidores = PPlanning.Get_ObtenerCompetidoresCliente(Convert.ToInt32(this.Session["company_id"]));
            CmbCompetidores.DataSource = dtCompetidores;
            CmbCompetidores.DataTextField = "Company_Name";
            CmbCompetidores.DataValueField = "Compay_idCompe";
            CmbCompetidores.DataBind();
            dtCompetidores = null;
        }

        private void llenaEjecutivos()
        {

            TestObject oTestObject401 = new TestObject() { Person_id = "01", name_user = "Ejecutivo01" };
            TestObject oTestObject402 = new TestObject() { Person_id = "02", name_user = "Ejecutivo02" };
            TestObject oTestObject403 = new TestObject() { Person_id = "03", name_user = "Ejecutivo03" };
            TestObject oTestObject404 = new TestObject() { Person_id = "04", name_user = "Ejecutivo04" };
            TestObject oTestObject405 = new TestObject() { Person_id = "05", name_user = "Ejecutivo05" };
            TestObject oTestObject406 = new TestObject() { Person_id = "06", name_user = "Ejecutivo06" };
            TestObject oTestObject407 = new TestObject() { Person_id = "07", name_user = "Ejecutivo07" };
            TestObject oTestObject408 = new TestObject() { Person_id = "08", name_user = "Ejecutivo08" };
            TestObject oTestObject409 = new TestObject() { Person_id = "09", name_user = "Ejecutivo09" };
            TestObject oTestObject410 = new TestObject() { Person_id = "10", name_user = "Ejecutivo10" };
            TestObject oTestObject411 = new TestObject() { Person_id = "11", name_user = "Ejecutivo11" };
            TestObject oTestObject412 = new TestObject() { Person_id = "12", name_user = "Ejecutivo12" };
            List<TestObject> oList4 = new List<TestObject>();
            oList4.Add(oTestObject401);
            oList4.Add(oTestObject402);
            oList4.Add(oTestObject403);
            oList4.Add(oTestObject404);
            oList4.Add(oTestObject405);
            oList4.Add(oTestObject406);
            oList4.Add(oTestObject407);
            oList4.Add(oTestObject408);
            oList4.Add(oTestObject409);
            oList4.Add(oTestObject410);
            oList4.Add(oTestObject411);
            oList4.Add(oTestObject412);

            LstNewejecutivo.DataSource = oList4;
            LstNewejecutivo.DataTextField = "name_user";
            LstNewejecutivo.DataValueField = "Person_id";
            LstNewejecutivo.DataBind();

            /*ListItem listItem1 = new ListItem("Ejecutivo01", "01");
            ListItem listItem2 = new ListItem("Ejecutivo02", "02");
            ListItem listItem3 = new ListItem("Ejecutivo03", "03");
            ListItem listItem4 = new ListItem("Ejecutivo04", "04");
            ListItem listItem5 = new ListItem("Ejecutivo05", "05");
            
            LstNewejecutivo.Items.Add(listItem1);
            LstNewejecutivo.Items.Add(listItem2);
            LstNewejecutivo.Items.Add(listItem3);
            LstNewejecutivo.Items.Add(listItem4);
            LstNewejecutivo.Items.Add(listItem5);*/

            /*DataTable dt = PPlanning.ObtenerEjecutivos(Convert.ToInt32(this.Session["company_id"]));           
            LstNewejecutivo.DataSource = dt;
            LstNewejecutivo.DataTextField = "name_user";
            LstNewejecutivo.DataValueField = "Person_id";
            LstNewejecutivo.DataBind();
            LstNewejecutivo.Items.Remove(LstNewejecutivo.Items[0]);
            dt = null;
            */

        }
        private void llenaNuevosSupervisores()
        {

            TestObject oTestObject201 = new TestObject() { Person_id = "01", name_user = "Supervisor01" };
            TestObject oTestObject202 = new TestObject() { Person_id = "02", name_user = "Supervisor02" };
            TestObject oTestObject203 = new TestObject() { Person_id = "03", name_user = "Supervisor03" };
            TestObject oTestObject204 = new TestObject() { Person_id = "04", name_user = "Supervisor04" };
            TestObject oTestObject205 = new TestObject() { Person_id = "05", name_user = "Supervisor05" };
            TestObject oTestObject206 = new TestObject() { Person_id = "06", name_user = "Supervisor06" };
            TestObject oTestObject207 = new TestObject() { Person_id = "07", name_user = "Supervisor07" };
            TestObject oTestObject208 = new TestObject() { Person_id = "08", name_user = "Supervisor08" };
            TestObject oTestObject209 = new TestObject() { Person_id = "09", name_user = "Supervisor09" };
            TestObject oTestObject210 = new TestObject() { Person_id = "10", name_user = "Supervisor10" };
            TestObject oTestObject211 = new TestObject() { Person_id = "11", name_user = "Supervisor11" };
            TestObject oTestObject212 = new TestObject() { Person_id = "12", name_user = "Supervisor12" };
            List<TestObject> oList3 = new List<TestObject>();
            oList3.Add(oTestObject201);
            oList3.Add(oTestObject202);
            oList3.Add(oTestObject203);
            oList3.Add(oTestObject204);
            oList3.Add(oTestObject205);
            oList3.Add(oTestObject206);
            oList3.Add(oTestObject207);
            oList3.Add(oTestObject208);
            oList3.Add(oTestObject209);
            oList3.Add(oTestObject210);
            oList3.Add(oTestObject211);
            oList3.Add(oTestObject212);

            LstNewSupervisor.DataSource = oList3;
            LstNewSupervisor.DataTextField = "name_user";
            LstNewSupervisor.DataValueField = "Person_id";
            LstNewSupervisor.DataBind();


            /*
            DataSet dsSupervisor = PPlanning.ObtenerPersonal(TxtPlanningRes.Text);
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
            dsSupervisor = null;*/
        } 
        private void llenaNuevosMercaderistas()
        {


            TestObject oTestObject301 = new TestObject() { Person_id = "01", name_user = "Mercaderistas01" };
            TestObject oTestObject302 = new TestObject() { Person_id = "02", name_user = "Mercaderistas02" };
            TestObject oTestObject303 = new TestObject() { Person_id = "03", name_user = "Mercaderistas03" };
            TestObject oTestObject304 = new TestObject() { Person_id = "04", name_user = "Mercaderistas04" };
            TestObject oTestObject305 = new TestObject() { Person_id = "05", name_user = "Mercaderistas05" };
            TestObject oTestObject306 = new TestObject() { Person_id = "06", name_user = "Mercaderistas06" };
            TestObject oTestObject307 = new TestObject() { Person_id = "07", name_user = "Mercaderistas07" };
            TestObject oTestObject308 = new TestObject() { Person_id = "08", name_user = "Mercaderistas08" };
            TestObject oTestObject309 = new TestObject() { Person_id = "09", name_user = "Mercaderistas09" };
            TestObject oTestObject310 = new TestObject() { Person_id = "10", name_user = "Mercaderistas10" };
            TestObject oTestObject311 = new TestObject() { Person_id = "11", name_user = "Mercaderistas11" };
            TestObject oTestObject312 = new TestObject() { Person_id = "12", name_user = "Mercaderistas12" };
            List<TestObject> oList4 = new List<TestObject>();
            oList4.Add(oTestObject301);
            oList4.Add(oTestObject302);
            oList4.Add(oTestObject303);
            oList4.Add(oTestObject304);
            oList4.Add(oTestObject305);
            oList4.Add(oTestObject306);
            oList4.Add(oTestObject307);
            oList4.Add(oTestObject308);
            oList4.Add(oTestObject309);
            oList4.Add(oTestObject310);
            oList4.Add(oTestObject311);
            oList4.Add(oTestObject312);

            LstNewMercaderista.DataSource = oList4;
            LstNewMercaderista.DataTextField = "name_user";
            LstNewMercaderista.DataValueField = "Person_id";
            LstNewMercaderista.DataBind();
            /*
            DataSet dsMercaderista = PPlanning.ObtenerPersonal(TxtPlanningRes.Text);
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
            if (LstNewMercaderista.Items.Count <= 0)
            {
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "No existen mercaderistas disponibles con un perfil apropiado para la campaña " + LblTxtPresupuestoRes.Text.ToUpper();
                Mensajes_ResponsablesCampaña();
                return;
            }*/
        } 
        private void llenaStaffplanning()
        {
            DataSet dsStaffPlanning = PPlanning.Get_Staff_Planning(CmbSelCampaña.SelectedValue);
            if (dsStaffPlanning != null)
            {
                if (dsStaffPlanning.Tables[0].Rows.Count > 0 && dsStaffPlanning.Tables[1].Rows.Count > 0)
                {
                    CmbSelSupervisoresAsig.DataSource = dsStaffPlanning.Tables[0];
                    CmbSelSupervisoresAsig.DataTextField = "name_user";
                    CmbSelSupervisoresAsig.DataValueField = "Person_id";
                    CmbSelSupervisoresAsig.DataBind();
                    CmbSelSupervisoresAsig.Items.Insert(0, new ListItem("--Seleccione--", "0"));

                    LstBoxMercaderistas.DataSource = dsStaffPlanning.Tables[1];
                    LstBoxMercaderistas.DataTextField = "name_user";
                    LstBoxMercaderistas.DataValueField = "Person_id";
                    LstBoxMercaderistas.DataBind();

                    for (int i = 0; i <= LstBoxMercaderistas.Items.Count - 1; i++)
                    {
                        DataTable dtverificacion = PPlanning.Get_VerficaAsignaOperativo(TxtPlanningAsig.Text, Convert.ToInt32(LstBoxMercaderistas.Items[i].Value));
                        if (dtverificacion.Rows.Count > 0)
                        {
                            LstBoxMercaderistas.Items.Remove(LstBoxMercaderistas.Items[i]);
                            i--;
                        }
                        dtverificacion = null;
                    }
                }
            }
            dsStaffPlanning = null;

        }
        private void LlenaCategorias()
        {
            //Llena las Categorias          
            DataTable dtcatego = new DataTable();
            dtcatego = PPlanning.Get_ObtenerCategoriasPlanning(Convert.ToInt32(this.Session["company_id"]));

            if (this.Session["vista_final"].ToString().Trim().Equals("Categoria"))
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
                dtcatego = null;
            }
        }
        private void LlenaCategoriasCompe()
        {
            //Llena las Categorias
            int company = Convert.ToInt32(CmbCompetidores.SelectedItem.Value);

            DataTable dtcatego = new DataTable();
            dtcatego = PPlanning.Get_ObtenerCategoriasPlanning(company);
            if (dtcatego != null)
            {
                if (dtcatego.Rows.Count > 0)
                {
                    if (this.Session["vista_final"].ToString().Trim().Equals("Categoria"))
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
                else
                {
                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "No existen productos para el competidor seleccionado";                   
                }
            }
            dtcatego = null;
        }
        private void LlenaPeriodos(int iaño, int imes)
        {
            DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENERPREIODOS", iaño, imes);
            ChklstFrecuencia.DataSource = dt;
            ChklstFrecuencia.DataTextField = "dia";
            ChklstFrecuencia.DataValueField = "dia";
            ChklstFrecuencia.DataBind();
            ModalPanelReportes.Show();

        }

        private void llenameses()
        {
            DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CONSULTA_INFORMESYMESES_PLANNING", TxtPlanningAsigReports.Text, 0, "NONE", Convert.ToInt32(CmbSelAñoCampaña.SelectedItem.Text));
            RbtnListmeses.DataSource = ds.Tables[0];
            RbtnListmeses.DataValueField = "id_Month";
            RbtnListmeses.DataTextField = "Month_name";
            RbtnListmeses.DataBind();
            //ChkListMeses.DataSource = ds.Tables[0];
            //ChkListMeses.DataValueField = "id_Month";
            //ChkListMeses.DataTextField = "Month_name";
            //ChkListMeses.DataBind();
            ds = null;
            ModalPanelReportes.Show();
        }
      
        private void llenaaños()
        {
            DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CONSULTA_INFORMESYMESES_PLANNING", TxtPlanningAsigReports.Text, 0, "NONE", 0);
            CmbSelAñoCampaña.DataSource = ds.Tables[2];
            CmbSelAñoCampaña.DataValueField = "Years_id";
            CmbSelAñoCampaña.DataTextField = "Years_Number";
            CmbSelAñoCampaña.DataBind();
            CmbSelAñoCampaña.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void llenainformes()
        {
            DataSet ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CONSULTA_INFORMESYMESES_PLANNING", TxtPlanningAsigReports.Text, Convert.ToInt32(this.Session["company_id"]), this.Session["Planning_CodChannel"].ToString().Trim(), 0);
            RBtnListInformes.DataSource = ds.Tables[1];
            RBtnListInformes.DataValueField = "Report_id";
            RBtnListInformes.DataTextField = "Report_NameReport";
            RBtnListInformes.DataBind();
            ds = null;
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
            dtpdvplanning = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTARPDVPLANNING_PANELES", TxtCodPlanningPanel.Text, RbtnListReportPanel.SelectedItem.Value,"0","0");

            GvPDVPaneles.DataSource = dtpdvplanning;
            GvPDVPaneles.DataBind();
            dtpdvplanning = null;

            GvPDVPaneles.BorderColor = System.Drawing.Color.Red;
            GvPDVPaneles.BorderStyle = BorderStyle.Double;
            GvPDVPaneles.BorderWidth = Unit.Pixel(3);
            GvPDVPaneles.GridLines = GridLines.Horizontal;
            GvPDVPaneles.HeaderStyle.BackColor = System.Drawing.Color.FromName("#333333");
            GvPDVPaneles.HeaderStyle.Font.Bold = true;
            GvPDVPaneles.HeaderStyle.ForeColor = System.Drawing.Color.White;
            GvPDVPaneles.RowStyle.BackColor = System.Drawing.Color.White;
            GvPDVPaneles.RowStyle.ForeColor = System.Drawing.Color.FromName("#336666");
            this.GvPDVPaneles.Columns[5].Visible = false; 
            
        }

        private void StiloBotonTipoProdSeleccionado()
        {
            BtnProdPropio.CssClass = "buttonNewPlan";
            BtnProdCompe.CssClass = "buttonNewPlan";
        }
        private void HabilitabotonesTipoProd()
        {
            BtnProdPropio.Enabled = true;
            BtnProdCompe.Enabled = true;
        }

        private void ActivaControlesAsignacionBudget()
        {
            Rblisstatus.Enabled = true;
            TxtDuracion.Enabled = true;
            RbtnCanal.Enabled = true;
            txt_FechaSolicitud.Enabled = true;
            txt_FechainiPre.Enabled = true;
            txt_FechainiPla.Enabled = true;
            ImageButtonCal.Enabled = true;
            ImageButtonCal3.Enabled = true;
            ImageButtonCal5.Enabled = true;
            txt_FechaEntrega.Enabled = true;
            txt_Fechafinpre.Enabled = true;
            txt_FechaPlafin.Enabled = true;
            ImageButtonCal2.Enabled = true;
            ImageButtonCal4.Enabled = true;
            ImageButtonCal6.Enabled = true;
        }
        /// <summary>
        /// Activa Controles Input Txt para Editar la Descripción de una Campania.
        /// </summary>
        /// <param name=""> None </param>
        /// <returns>None</returns>
        private void ActivaControlesDescripcionCampaña()
        {
            txtobj.Enabled = true;
            txtmanda.Enabled = true;
            Txtmeca.Enabled = true;
            txtcontacto.Enabled = true;
            txtarea.Enabled = true;
        }
        private void ActivaControlesResponsablesCampaña()
        {
            GvEjecutivosAsignados.Enabled = true;
            GvSupervisoresAsignados.Enabled = true;
            GvMercaderistasAsignados.Enabled = true;
            ImgButtonAddEjecutivos.Enabled = true;
            ImgButtonAddSupervisores.Enabled = true;
            ImgButtonAddMercaderistas.Enabled = true;
        }
        private void ActivaControlesAsignaPersonal()
        {
            CmbSelSupervisoresAsig.Enabled = true;
            LstBoxMercaderistas.Enabled = true;
            BtnMasAsing.Enabled = true;
            GvAsignados.Enabled = true;
        }

        private void DesactivarControlesASignacionBudget()
        {
            Rblisstatus.Enabled = false;
            TxtDuracion.Enabled = false;
            RbtnCanal.Enabled = false;
            txt_FechaSolicitud.Enabled = false;
            txt_FechainiPre.Enabled = false;
            txt_FechainiPla.Enabled = false;
            ImageButtonCal.Enabled = false;
            ImageButtonCal3.Enabled = false;
            ImageButtonCal5.Enabled = false;
            txt_FechaEntrega.Enabled = false;
            txt_Fechafinpre.Enabled = false;
            txt_FechaPlafin.Enabled = false;
            ImageButtonCal2.Enabled = false;
            ImageButtonCal4.Enabled = false;
            ImageButtonCal6.Enabled = false;
        }
        private void DesactivarControlesDescripcionCampaña()
        {
            txtobj.Enabled = false;
            txtmanda.Enabled = false;
            Txtmeca.Enabled = false;
            txtcontacto.Enabled = false;
            txtarea.Enabled = false;
        }
        private void DesactivarControlesResponsablesCampaña()
        {
            GvEjecutivosAsignados.Enabled = false;
            GvSupervisoresAsignados.Enabled = false;
            GvMercaderistasAsignados.Enabled = false;
            ImgButtonAddEjecutivos.Enabled = false;
            ImgButtonAddSupervisores.Enabled = false;
            ImgButtonAddMercaderistas.Enabled = false;
        }
        private void DesactivarControlesAsignaPersonal()
        {
            CmbSelSupervisoresAsig.Enabled = false;
            LstBoxMercaderistas.Enabled = false;
            BtnMasAsing.Enabled = false;
            GvAsignados.Enabled = false;
        }

        

        protected void ImgCloseSession_Click(object sender, ImageClickEventArgs e)
        {
            this.Session.Abandon();
            Response.Redirect("~/login.aspx");
        }
        /// <summary>
        /// Redireccionar al Menú Planning
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect(
                "~/Pages/Modulos/Planning/Menu_Planning.aspx", true);
        }
        protected void BtnCOlv_Click(object sender, ImageClickEventArgs e)
        {
            InicializarPaneles();
            botonregresar.Visible = true;
            GVReportesAsignados.EditIndex = -1;
        }

        /// <summary>
        /// Evento onChange del ComboBox 'CmbSelCampaña'
        /// </summary>
        /// <param name=""></param>
        /// <returns>DataTable</returns>
        protected void CmbSelCampaña_SelectedIndexChanged(object sender, EventArgs e)
        {
            Postback = false;
            this.Session["Postback"] = false;
            // Verificar si el valor seleccionado para el Control AspControl DropDownList 'CmbSelCampaña' es diferente de vacio.
            if (!CmbSelCampaña.SelectedValue.Equals("")){

                #region Codigo para saltarse validaciones 
                // Botón para mostrar la Información General del:
                // - Planning
                // - Puntos de Venta
                // - Personal Disponible
                /*
                ImgBtnInformeTotal.Visible = true;

                // AspControl Image: 'ImgAsigBudget'
                ImgAsigBudget.ImageUrl = "~/Pages/images/Terminado.png";
                // 
                ImgIrABudget.Visible = true;

                ImgDescCamp.ImageUrl = "~/Pages/images/Terminado.png";
                ImgIrADesc.Visible = true;

                ImgResponsables.ImageUrl = "~/Pages/images/Terminado.png";
                ImgIrAResponsables.Visible = true;

                ImgAsigPersonal.ImageUrl = "~/Pages/images/Terminado.png";
                ImgIrAASigPersonal.Visible = true;

                ImgPDV.ImageUrl = "~/Pages/images/Terminado.png";
                ImgIrAPDV.Visible = true;

                ImgProductos.ImageUrl = "~/Pages/images/Terminado.png";
                ImgIrAProductos.Visible = true;

                ImgAsignaPDV.ImageUrl = "~/Pages/images/Terminado.png";
                ImgIrAAsignapdv.Visible = true;

                ImgReportes.ImageUrl = "~/Pages/images/Terminado.png";
                ImgIrAReportesCampaña.Visible = true;

                ImgPaneles.ImageUrl = "~/Pages/images/Terminado.png";
                ImgIrAPaneles.Visible = true;

                ImgBreaf.ImageUrl = "~/Pages/images/Terminado.png";
                ImgIrABreaf.Visible = true;

                Image4.ImageUrl = "~/Pages/images/Terminado.png";
                MenuObjetivoSODMay.Visible = true;
                
                ImgProdAncla.Visible = true;
                */
                #endregion

                DataSet ds = new DataSet();

                /// <summary>
                /// Obtener los Planning creados
                /// Retorna 14 Objetos que muestra la información del Planning
                /// Ing. Mauricio Ortiz 14 de julio de 2010
                /// </summary>
                /// <param name="sid_planning">Id del Planning</param>
                /// <returns>DataSet</returns>
                /// DataTable[0] - Obtiene información para llenar combo de Planning
                /// DataTable[1] - Obtiene información de los objetivos para la Campaña
                /// DataTable[2] - Obtiene información de los Productos con SKU Mandatorio
                /// DataTable[3] - Obtiene información de la Mecánica de las Actividades
                /// DataTable[4] - Obtiene información del Personal Asignado a la Campaña
                /// DataTable[5] - Obtiene información de la asginación de Mercaderista y Supervisores
                /// DataTable[6] - Obtiene información de los PDVs de la Campania
                /// DataTable[7] - Obtiene información de los Productos Asignados por Campania 
                /// DataTable[8] - Obtiene información de los Puntos de Venta Asignados a los Mercaderistas
                /// DataTable[9] - Obtiene información del Planning Seleccionado
                /// DataTable[10]- Obtiene información de los Reportes asignados a la Campania
                /// DataTable[11]- Obtiene información de los Paneles Asignados a la Campania
                /// DataTable[12]- Obtiene información de las Marcas Asignadas a la Campania
                /// DataTable[13]- Obtiene información de las Familias Asignadas a la Campania
                /// DataTable[14]- Obtiene información de las Categorias Asignadas a la Campania
                ds = oCoon.ejecutarDataSet("UP_WEBSIGE_PLANNIG_CREADOS", CmbSelCampaña.SelectedValue);

                // Guarda en un ViewState 'planning_creados' la información del Planning (14 DataTables)
                this.ViewState["planning_creados"] = ds;
                
                // Guarda en un DataTable los Clientes asociados a un Planning.
                DataTable dtCliente = 
                    Presupuesto.Get_ObtenerClientes(ds.Tables[9].Rows[0]["Planning_Budget"].ToString().Trim(), 
                    1);
                this.Session["Numbudget"] = ds.Tables[9].Rows[0]["Planning_Budget"].ToString().Trim();
                this.Session["company_id"] = dtCliente.Rows[0]["Company_id"].ToString().Trim();
                
                DataSet dtniveles = new DataSet();

                // Verifica si los DataTables tienes registros en:
                // DataTable[1] - Obtiene información de los objetivos para la Campaña
                // DataTable[2] - Obtiene información de los Productos con SKU Mandatorio
                // DataTable[3] - Obtiene información de la Mecánica de las Actividades
                if (ds.Tables[1].Rows.Count > 0 
                    && ds.Tables[2].Rows.Count > 0 
                    && ds.Tables[3].Rows.Count > 0){
                    ImgDescCamp.ImageUrl = "~/Pages/images/Terminado.png";
                    ImgIrADesc.Visible = true;
                }else{
                    ImgDescCamp.ImageUrl = "~/Pages/images/Pendiente.png";
                    ImgIrADesc.Visible = false;
                }

                // Verifica si los DataTables tienes registros en:
                /// DataTable[4] - Obtiene información del Personal Asignado a la Campaña
                if (ds.Tables[4].Rows.Count > 0){
                    ImgResponsables.ImageUrl = "~/Pages/images/Terminado.png";
                    ImgIrAResponsables.Visible = true;
                }else{
                    ImgResponsables.ImageUrl = "~/Pages/images/Pendiente.png";
                    ImgIrAResponsables.Visible = false;
                }
                // Verifica si los DataTables tienes registros en:
                /// DataTable[5] - Obtiene información de la asginación de Mercaderista y Supervisores                
                if (ds.Tables[5].Rows.Count > 0){
                    ImgAsigPersonal.ImageUrl = "~/Pages/images/Terminado.png";
                    ImgIrAASigPersonal.Visible = true;
                }else{
                    ImgAsigPersonal.ImageUrl = "~/Pages/images/Pendiente.png";
                    ImgIrAASigPersonal.Visible = false;
                }
                // Verifica si los DataTables tienes registros en:
                /// DataTable[6] - Obtiene información de los PDVs de la Campania
                if (ds.Tables[6].Rows.Count > 0){
                    ImgPDV.ImageUrl = "~/Pages/images/Terminado.png";
                    ImgIrAPDV.Visible = true;
                }else{
                    ImgPDV.ImageUrl = "~/Pages/images/Pendiente.png";
                    ImgIrAPDV.Visible = false;
                }
                
                // Verifica si los DataTables tienes registros en:
                /// DataTable[7] - Obtiene información de los Productos Asignados por Campania 
                /// DataTable[12]- Obtiene información de las Marcas Asignadas a la Campania
                /// DataTable[13]- Obtiene información de las Familias Asignadas a la Campania
                /// DataTable[14]- Obtiene información de las Categorias Asignadas a la Campania
                if (ds.Tables[7].Rows.Count > 0 
                    || ds.Tables[12].Rows.Count > 0 
                    || ds.Tables[13].Rows.Count > 0 
                    || ds.Tables[14].Rows.Count > 0){
                    ImgProductos.ImageUrl = "~/Pages/images/Terminado.png";
                    ImgIrAProductos.Visible = true;
                }else{
                    ImgProductos.ImageUrl = "~/Pages/images/Pendiente.png";
                    ImgIrAProductos.Visible = false;
                }

                // Verifica si los DataTables tienes registros en:
                /// DataTable[8] - Obtiene información de los Puntos de Venta Asignados a los Mercaderistas
                if (ds.Tables[8].Rows.Count > 0){
                    ImgAsignaPDV.ImageUrl = "~/Pages/images/Terminado.png";
                    ImgIrAAsignapdv.Visible = true;
                }else{
                    ImgAsignaPDV.ImageUrl = "~/Pages/images/Pendiente.png";
                    ImgIrAAsignapdv.Visible = false;
                }

                // Verifica si los DataTables tienes registros en:
                /// DataTable[10]- Obtiene información de los Reportes asignados a la Campania
                if (ds.Tables[10].Rows.Count > 0){
                    ImgReportes.ImageUrl = "~/Pages/images/Terminado.png";
                    ImgIrAReportesCampaña.Visible = true;
                }else{
                    ImgReportes.ImageUrl = "~/Pages/images/Pendiente.png";
                    ImgIrAReportesCampaña.Visible = false;
                }

                // Verifica si los DataTables tienes registros en:
                /// DataTable[11]- Obtiene información de los Paneles Asignados a la Campania
                if (ds.Tables[11].Rows.Count > 0){
                    ImgPaneles.ImageUrl = "~/Pages/images/Terminado.png";
                    ImgIrAPaneles.Visible = true;
                }else{
                    ImgPaneles.ImageUrl = "~/Pages/images/Pendiente.png";
                    ImgIrAPaneles.Visible = false;
                }

                if (ImgAsigBudget.ImageUrl.Equals("~/Pages/images/Terminado.png")
                    && ImgDescCamp.ImageUrl.Equals("~/Pages/images/Terminado.png")
                    && ImgResponsables.ImageUrl.Equals("~/Pages/images/Terminado.png")
                    && ImgAsigPersonal.ImageUrl.Equals("~/Pages/images/Terminado.png")
                    && ImgPDV.ImageUrl.Equals("~/Pages/images/Terminado.png")
                    && ImgProductos.ImageUrl.Equals("~/Pages/images/Terminado.png")
                    && ImgAsignaPDV.ImageUrl.Equals("~/Pages/images/Terminado.png")
                    && ImgReportes.ImageUrl.Equals("~/Pages/images/Terminado.png")
                    && ImgPaneles.ImageUrl.Equals("~/Pages/images/Terminado.png")
                    ){
                    ImgBreaf.ImageUrl = "~/Pages/images/Terminado.png";
                    ImgIrABreaf.Visible = true;
                }else{
                    ImgBreaf.ImageUrl = "~/Pages/images/Pendiente.png";
                    ImgIrABreaf.Visible = false;
                }

                // Verifica si es Canal mayorista, para mostrar el menú de 'Objetivos SOD':
                /// DataTable[9] - Obtiene información del Planning Seleccionado
                if (ds.Tables[9].Rows[0]["Planning_CodChannel"].ToString() == "1000"){
                    MenuObjetivoSODMay.Visible = true;
                }else{
                    MenuObjetivoSODMay.Visible = false;
                }
                
                ImgProdAncla.Visible = true;
                
                //metodo temporal para la visualizacion de Niveles en AA_VV

                if (CmbSelCampaña.SelectedValue.Equals("101131122011"))//si la campaña es AA_VV
                {
                    this.Session["id_planning"] = CmbSelCampaña.SelectedValue;
                    OpcionGestionNiveles.Style.Value = "display: block";
                    ImgGestionNiveles.ImageUrl = "../../images/Terminado.png";
                    ImgIrAGestionNiveles.Visible = true;

                    OpcionGestionFuerzaVenta.Style.Value = "display: block";
                    ImgGestionFuerzaVenta.ImageUrl = "../../images/Terminado.png";
                    ImgIrAGestionFuerzaVenta.Visible = true;
                }else{
                    OpcionGestionNiveles.Style.Value = "display: none";
                    OpcionGestionFuerzaVenta.Style.Value = "display: none";
                }
                ds = null;
                dtCliente = null;
                
            }
            else{
                ImgBtnInformeTotal.Visible = false;
                ImgAsigBudget.ImageUrl = "~/Pages/images/Esperando.png";
                ImgDescCamp.ImageUrl = "~/Pages/images/Esperando.png";
                ImgResponsables.ImageUrl = "~/Pages/images/Esperando.png";
                ImgAsigPersonal.ImageUrl = "~/Pages/images/Esperando.png";
                ImgPDV.ImageUrl = "~/Pages/images/Esperando.png";
                ImgPaneles.ImageUrl = "~/Pages/images/Esperando.png";
                ImgProductos.ImageUrl = "~/Pages/images/Esperando.png";
                ImgAsignaPDV.ImageUrl = "~/Pages/images/Esperando.png";
                ImgReportes.ImageUrl = "~/Pages/images/Esperando.png";
                ImgBreaf.ImageUrl = "~/Pages/images/Esperando.png";
                ImgIrABudget.Visible = false;
                ImgIrADesc.Visible = false;
                ImgIrAResponsables.Visible = false;
                ImgIrAASigPersonal.Visible = false;
                ImgIrAPDV.Visible = false;
                ImgIrAProductos.Visible = false;
                ImgIrAAsignapdv.Visible = false;
                ImgIrAReportesCampaña.Visible = false;
                ImgIrAPaneles.Visible = false;
                ImgIrABreaf.Visible = false;
            }
        }

        #region Presupuesto
        
        /// <summary>
        /// Evento Click del AspControl ImageButton: 'ImgIrABudget'
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        protected void ImgIrABudget_Click(object sender, ImageClickEventArgs e)
        {
            // Hace visible el DIV Regresar: 'botonregresar'
            botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            // Oculta todos los Panels
            InicializarPaneles();
            // Hace visible el Panel ModalPanelASignaPresupuesto
            ModalPanelASignaPresupuesto.Show();
            ConsultaAsignacionBudget();
        }
        protected void BtnEditPlanning_Click(object sender, EventArgs e)
        {
            BtnEditPlanning.Visible = false;
            Postback = true;
            this.Session["Postback"] = true;
            BtnUpdatePlanning.Visible = true;
            ActivaControlesAsignacionBudget();
            ModalPanelASignaPresupuesto.Show();
        }
        protected void btncancelcara_Click(object sender, EventArgs e)
        {
            this.Session["Postback"] = false;
            BtnEditPlanning.Visible = true;
            BtnUpdatePlanning.Visible = false;
            ConsultaAsignacionBudget();
            DesactivarControlesASignacionBudget();
            ModalPanelASignaPresupuesto.Show();
        }
        protected void BtnUpdatePlanning_Click(object sender, EventArgs e)
        {
            this.Session["Postback"] = false;
            Boolean Continuar = datoscompletosInformacionBasica();
            if (Continuar)
            {
                ModalPanelASignaPresupuesto.Show();
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
                                        PPlanning.Get_Update_Planning(txtnumpla.Text, RbtnCanal.SelectedValue, Convert.ToDateTime(txt_FechainiPla.Text), Convert.ToDateTime(txt_FechaPlafin.Text),
                                        Convert.ToDateTime(txt_FechaSolicitud.Text), Convert.ToDateTime(txt_FechainiPre.Text), Convert.ToDateTime(txt_Fechafinpre.Text), TxtDuracion.Text, Convert.ToDateTime(txt_FechaEntrega.Text),
                                        TxtDuracion.Text, Convert.ToBoolean(Rblisstatus.SelectedValue), 1, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                                        string Estado = "0";
                                        if (Rblisstatus.SelectedValue.Equals("True"))
                                            Estado = "1";

                                        PPlanning.Get_Update_PlanningTBL_EQUIPO(txtnumpla.Text,  RbtnCanal.SelectedValue, Estado);
                                        ModalPanelASignaPresupuesto.Show();
                                        DesactivarControlesASignacionBudget();
                                        BtnEditPlanning.Visible = true;
                                        BtnUpdatePlanning.Visible = false;
                                        this.Session["encabemensa"] = "Sr. Usuario";
                                        this.Session["cssclass"] = "MensajesSupConfirm";
                                        this.Session["mensaje"] = "La campaña : " + txtnamepresu.Text.ToUpper() + ", se ha actualizado con éxito";
                                        Mensajes_AsignacionPresupuesto();                                       
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void txt_FechaEntrega_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ModalPanelASignaPresupuesto.Show();
                Postback = Convert.ToBoolean(this.Session["Postback"].ToString().Trim());
                if (Postback)
                {
                    try
                    {
                        
                        if (txt_FechaEntrega.Text != "__/__/____" && txt_FechaEntrega.Text != "")
                        {
                            DateTime t = Convert.ToDateTime(txt_FechaEntrega.Text);
                            this.Session["Postback"] = false;
                        }
                    }
                    catch
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                        txt_FechaEntrega.Text = "";
                        Mensajes_AsignacionPresupuesto();
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
        protected void txt_FechaPlafin_TextChanged(object sender, EventArgs e)
        {
            try
            {                
                ModalPanelASignaPresupuesto.Show();
                Postback = Convert.ToBoolean(this.Session["Postback"].ToString().Trim());
                if (Postback)
                {
                    try
                    {
                        
                        if (txt_FechaPlafin.Text != "__/__/____" && txt_FechaPlafin.Text != "")
                        {
                            DateTime t = Convert.ToDateTime(txt_FechaPlafin.Text);
                            txt_Fechafinpre.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPla.Text).AddDays(-1));
                            txt_FechaEntrega.Text = Convert.ToString(Convert.ToDateTime(txt_FechaPlafin.Text).AddDays(1));
                            this.Session["Postback"] = false;
                        }
                    }
                    catch
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                        txt_FechaPlafin.Text = "";
                        Mensajes_AsignacionPresupuesto();
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
        protected void txt_FechainiPla_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ModalPanelASignaPresupuesto.Show();
                Postback = Convert.ToBoolean(this.Session["Postback"].ToString().Trim());
                if (Postback)
                {
                    try
                    {
                        
                        if (txt_FechainiPla.Text != "__/__/____" && txt_FechainiPla.Text != "")
                        {
                            DateTime t = Convert.ToDateTime(txt_FechainiPla.Text);
                            txt_FechaSolicitud.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPla.Text).AddDays(-2));
                            txt_FechainiPre.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPla.Text).AddDays(-1));
                            txt_Fechafinpre.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPla.Text).AddDays(-1));
                            txt_FechaEntrega.Text = Convert.ToString(Convert.ToDateTime(txt_FechaPlafin.Text).AddDays(1));
                            this.Session["Postback"] = false;
                        }
                    }
                    catch
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                        txt_FechainiPla.Text = "";
                        Mensajes_AsignacionPresupuesto();
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
        protected void txt_Fechafinpre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ModalPanelASignaPresupuesto.Show();
                Postback = Convert.ToBoolean(this.Session["Postback"].ToString().Trim());
                if (Postback)
                {
                    try
                    {                        
                        if (txt_Fechafinpre.Text != "__/__/____" && txt_Fechafinpre.Text != "")
                        {
                            DateTime t = Convert.ToDateTime(txt_Fechafinpre.Text);
                            txt_FechainiPla.Text = Convert.ToString(Convert.ToDateTime(txt_Fechafinpre.Text).AddDays(1));
                            this.Session["Postback"] = false;
                        }
                    }
                    catch
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                        txt_Fechafinpre.Text = "";
                        Mensajes_AsignacionPresupuesto();
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
        protected void txt_FechainiPre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ModalPanelASignaPresupuesto.Show();
                Postback = Convert.ToBoolean(this.Session["Postback"].ToString().Trim());
                if (Postback)
                {
                    try
                    {                        
                        if (txt_FechainiPre.Text != "__/__/____" && txt_FechainiPre.Text != "")
                        {
                            DateTime t = Convert.ToDateTime(txt_FechainiPre.Text);

                            txt_Fechafinpre.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPre.Text));
                            txt_FechaSolicitud.Text = Convert.ToString(Convert.ToDateTime(txt_FechainiPre.Text).AddDays(-1));
                            this.Session["Postback"] = false;
                        }
                    }
                    catch
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                        txt_FechainiPre.Text = "";
                        Mensajes_AsignacionPresupuesto();
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
        protected void txt_FechaSolicitud_TextChanged(object sender, EventArgs e)
        {
            try
            { 
                ModalPanelASignaPresupuesto.Show();
                Postback = Convert.ToBoolean(this.Session["Postback"].ToString().Trim());
                if (Postback)
                {
                    try
                    {                       
                        if (txt_FechaSolicitud.Text != "__/__/____" && txt_FechaSolicitud.Text != "")
                        {
                            DateTime t = Convert.ToDateTime(txt_FechaSolicitud.Text);                                                 
                            this.Session["Postback"] = false;

                        }
                    }
                    catch
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La fecha ingresada no tiene un formato válido. Por favor intente nuevamente";
                        txt_FechaSolicitud.Text = "";
                        Mensajes_AsignacionPresupuesto();
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

        protected void btnaceptar_Click(object sender, EventArgs e)
        {
            ModalPanelASignaPresupuesto.Show();
        }
        #endregion

        #region Descripcion campaña
        /// <summary>
        /// Evento Click para el AspControl: ImgIrADesc
        /// Que retorna la descripción de la Campaña
        /// </summary>
        /// <param name=""> None </param>
        /// <returns>None</returns>
        protected void ImgIrADesc_Click(object sender, ImageClickEventArgs e)
        {
            botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();
            ModalPanelDescCampaña.Show();
            ConsultadescripcionCampaña();

        }
        protected void BtnClearDescrip_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            BtnEditDescripcion.Visible = true;
            BtnUpdateDescripcion.Visible = false;
            ConsultadescripcionCampaña();
            DesactivarControlesDescripcionCampaña();
            ModalPanelDescCampaña.Show();

        }
        
        /// <summary>
        /// Evento Click del AspControl - BtnEditDescripcion, para Setear los valores de
        /// la Campaña en los controladores input txt y Editar la Información
        /// Para Editar la descripción de la Campania
        /// </summary>
        /// <param name=""> None </param>
        /// <returns>None</returns>
        protected void BtnEditDescripcion_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            BtnEditDescripcion.Visible = false;
            BtnUpdateDescripcion.Visible = true;
            ActivaControlesDescripcionCampaña();
            ModalPanelDescCampaña.Show();
        }

        /// <summary>
        /// Evento Click del AspControl - BtnUpdateDescripcion, para confirmar los 
        /// cambios en la información de la Campaña
        /// Para Editar la descripción de la Campania
        /// </summary>
        /// <param name=""> None </param>
        /// <returns>None</returns>
        protected void BtnUpdateDescripcion_Click(object sender, EventArgs e)
        {
            Boolean Continuar = datoscompletosDescripcionCampaña();
            if (Continuar)
            {
                //Ejecutar Método para actualizar los objetivos de la Campaña. Ing. Mauricio Ortiz
                PPlanning.Get_Update_Objetives_Planning(txtobj.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now, TxtCodPlanningDesc.Text);

                //Ejecutar Método para almacenar los Mandatorios de la Campaña. Ing. Mauricio Ortiz
                PPlanning.Get_Update_Mandatorios_Planning(txtmanda.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now, TxtCodPlanningDesc.Text);

                //Ejecutar Método para almacenar la Mecanica de la Actividad. Ing. Mauricio Ortiz

                PPlanning.Get_Update_Mecanica_Planning(Txtmeca.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now, TxtCodPlanningDesc.Text);

                //Ejecutar Método para actualizar contacto del planning y area involucrada del planning
                PPlanning.ActualizaContactoyarea(TxtCodPlanningDesc.Text, txtarea.Text, txtcontacto.Text, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                DesactivarControlesDescripcionCampaña();
                BtnEditDescripcion.Visible = true;
                BtnUpdateDescripcion.Visible = false;

                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupConfirm";
                this.Session["mensaje"] = "Se ha Actualizado con éxito la descripción para la campaña : " + LblTxtPresupuestoDesc.Text.ToUpper();
                Mensajes_DescripcionCampaña();

            }
        }
        protected void btnaceptarDesc_Click(object sender, EventArgs e)
        {
            ModalPanelDescCampaña.Show();
        }
        #endregion

        #region Reponsables
        protected void ImgIrAResponsables_Click(object sender, ImageClickEventArgs e)
        {
            botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();

            DataTable dtEliminarejecutivoTemp = new DataTable();
            dtEliminarejecutivoTemp.Columns.Add("Person_id", typeof(Int32));
            dtEliminarejecutivoTemp.Columns.Add("name_user", typeof(String));
            this.Session["dtEliminarejecutivoTemp"] = dtEliminarejecutivoTemp;

            DataTable dtEliminarsupervisorTemp = new DataTable();
            dtEliminarsupervisorTemp.Columns.Add("Person_id", typeof(Int32));
            dtEliminarsupervisorTemp.Columns.Add("name_user", typeof(String));
            this.Session["dtEliminarsupervisorTemp"] = dtEliminarsupervisorTemp;

            DataTable dtEliminarmercaderistaTemp = new DataTable();
            dtEliminarmercaderistaTemp.Columns.Add("Person_id", typeof(Int32));
            dtEliminarmercaderistaTemp.Columns.Add("name_user", typeof(String));
            this.Session["dtEliminarmercaderistaTemp"] = dtEliminarmercaderistaTemp;

            llenaEjecutivos();

            ModalPanelResponsablesCampaña.Show();

            ConsultaResponsablesCamapaña();

            GvEliminaEjecutivos.DataBind();
            GvEliminaSupervisor.DataBind();
            GvEliminaMercaderista.DataBind();
        }
        protected void GvEjecutivosAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtEliminarAsignadosTemp = (DataTable)this.Session["dtEliminarejecutivoTemp"];
            DataRow drDelete = dtEliminarAsignadosTemp.NewRow();
            drDelete["Person_id"] = Convert.ToInt32(GvEjecutivosAsignados.SelectedRow.Cells[1].Text);
            drDelete["name_user"] = GvEjecutivosAsignados.SelectedRow.Cells[2].Text;
            dtEliminarAsignadosTemp.Rows.Add(drDelete);
            this.Session["dtEliminarejecutivoTemp"] = dtEliminarAsignadosTemp;
            GvEliminaEjecutivos.DataSource = dtEliminarAsignadosTemp;
            GvEliminaEjecutivos.DataBind();

            DataTable dtAsignadosTemp = new DataTable();
            dtAsignadosTemp.Columns.Add("Person_id", typeof(Int32));
            dtAsignadosTemp.Columns.Add("name_user", typeof(String));
            for (int i = 0; i <= GvEjecutivosAsignados.Rows.Count - 1; i++)
            {
                DataRow dr = dtAsignadosTemp.NewRow();
                dr["Person_id"] = Convert.ToInt32(GvEjecutivosAsignados.Rows[i].Cells[1].Text);
                dr["name_user"] = GvEjecutivosAsignados.Rows[i].Cells[2].Text;
                dr["name_user"] = dr["name_user"].ToString().Replace("&amp;", "&");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#193;", "á");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#201;", "é");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#205;", "í");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#211;", "ó");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#218;", "ú");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#225;", "á");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#233;", "é");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#237;", "í");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#243;", "ó");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#250;", "ú");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#209;", "ñ");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#241;", "ñ");

                dtAsignadosTemp.Rows.Add(dr);
            }

            dtAsignadosTemp.Rows[GvEjecutivosAsignados.SelectedRow.RowIndex].Delete();
            GvEjecutivosAsignados.DataSource = dtAsignadosTemp;
            GvEjecutivosAsignados.DataBind();
            ModalPanelResponsablesCampaña.Show();
        }
        protected void GvSupervisoresAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtEliminarAsignadosSupTemp = (DataTable)this.Session["dtEliminarsupervisorTemp"];
            DataRow drDelete = dtEliminarAsignadosSupTemp.NewRow();
            drDelete["Person_id"] = Convert.ToInt32(GvSupervisoresAsignados.SelectedRow.Cells[1].Text);
            drDelete["name_user"] = GvSupervisoresAsignados.SelectedRow.Cells[2].Text;
            dtEliminarAsignadosSupTemp.Rows.Add(drDelete);
            this.Session["dtEliminarsupervisorTemp"] = dtEliminarAsignadosSupTemp;
            GvEliminaSupervisor.DataSource = dtEliminarAsignadosSupTemp;
            GvEliminaSupervisor.DataBind();

            DataTable dtAsignadosSupTemp = new DataTable();
            dtAsignadosSupTemp.Columns.Add("Person_id", typeof(Int32));
            dtAsignadosSupTemp.Columns.Add("name_user", typeof(String));
            for (int i = 0; i <= GvSupervisoresAsignados.Rows.Count - 1; i++)
            {
                DataRow dr = dtAsignadosSupTemp.NewRow();
                dr["Person_id"] = Convert.ToInt32(GvSupervisoresAsignados.Rows[i].Cells[1].Text);
                dr["name_user"] = GvSupervisoresAsignados.Rows[i].Cells[2].Text;
                dr["name_user"] = dr["name_user"].ToString().Replace("&amp;", "&");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#193;", "á");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#201;", "é");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#205;", "í");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#211;", "ó");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#218;", "ú");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#225;", "á");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#233;", "é");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#237;", "í");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#243;", "ó");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#250;", "ú");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#209;", "ñ");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#241;", "ñ");

                dtAsignadosSupTemp.Rows.Add(dr);
            }

            dtAsignadosSupTemp.Rows[GvSupervisoresAsignados.SelectedRow.RowIndex].Delete();
            GvSupervisoresAsignados.DataSource = dtAsignadosSupTemp;
            GvSupervisoresAsignados.DataBind();
            ModalPanelResponsablesCampaña.Show();

        }
        protected void GvMercaderistasAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtEliminarMercaderistasTemp = (DataTable)this.Session["dtEliminarmercaderistaTemp"];
            DataRow drDelete = dtEliminarMercaderistasTemp.NewRow();
            drDelete["Person_id"] = Convert.ToInt32(GvMercaderistasAsignados.SelectedRow.Cells[1].Text);
            drDelete["name_user"] = GvMercaderistasAsignados.SelectedRow.Cells[2].Text;
            dtEliminarMercaderistasTemp.Rows.Add(drDelete);
            this.Session["dtEliminarmercaderistaTemp"] = dtEliminarMercaderistasTemp;
            GvEliminaMercaderista.DataSource = dtEliminarMercaderistasTemp;
            GvEliminaMercaderista.DataBind();

            DataTable dtAsignadosMerTemp = new DataTable();
            dtAsignadosMerTemp.Columns.Add("Person_id", typeof(Int32));
            dtAsignadosMerTemp.Columns.Add("name_user", typeof(String));
            for (int i = 0; i <= GvMercaderistasAsignados.Rows.Count - 1; i++)
            {
                DataRow dr = dtAsignadosMerTemp.NewRow();
                dr["Person_id"] = Convert.ToInt32(GvMercaderistasAsignados.Rows[i].Cells[1].Text);
                dr["name_user"] = GvMercaderistasAsignados.Rows[i].Cells[2].Text;
                dr["name_user"] = dr["name_user"].ToString().Replace("&amp;", "&");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#193;", "á");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#201;", "é");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#205;", "í");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#211;", "ó");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#218;", "ú");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#225;", "á");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#233;", "é");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#237;", "í");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#243;", "ó");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#250;", "ú");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#209;", "ñ");
                dr["name_user"] = dr["name_user"].ToString().Replace("&#241;", "ñ");

                dtAsignadosMerTemp.Rows.Add(dr);
            }

            dtAsignadosMerTemp.Rows[GvMercaderistasAsignados.SelectedRow.RowIndex].Delete();
            GvMercaderistasAsignados.DataSource = dtAsignadosMerTemp;
            GvMercaderistasAsignados.DataBind();
            ModalPanelResponsablesCampaña.Show();
        }
        protected void btnAddEjecutivos_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= LstNewejecutivo.Items.Count - 1; i++)
            {
                if (LstNewejecutivo.Items[i].Selected == true)
                {

                    bool continuar = true;
                    for (int j = 0; j <= GvEjecutivosAsignados.Rows.Count - 1; j++)
                    {
                        if (GvEjecutivosAsignados.Rows[j].Cells[1].Text == LstNewejecutivo.Items[i].Value)
                        {
                            continuar = false;
                            j = GvEjecutivosAsignados.Rows.Count - 1;
                        }
                    }
                    if (continuar)
                    {
                        DataTable dtAsignadosTemp = new DataTable();
                        dtAsignadosTemp.Columns.Add("Person_id", typeof(Int32));
                        dtAsignadosTemp.Columns.Add("name_user", typeof(String));
                        for (int k = 0; k <= GvEjecutivosAsignados.Rows.Count - 1; k++)
                        {
                            DataRow dr = dtAsignadosTemp.NewRow();
                            dr["Person_id"] = Convert.ToInt32(GvEjecutivosAsignados.Rows[k].Cells[1].Text);
                            dr["name_user"] = GvEjecutivosAsignados.Rows[k].Cells[2].Text;
                            dr["name_user"] = dr["name_user"].ToString().Replace("&amp;", "&");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#193;", "á");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#201;", "é");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#205;", "í");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#211;", "ó");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#218;", "ú");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#225;", "á");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#233;", "é");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#237;", "í");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#243;", "ó");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#250;", "ú");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#209;", "ñ");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#241;", "ñ");

                            dtAsignadosTemp.Rows.Add(dr);
                        }
                        DataRow drnew = dtAsignadosTemp.NewRow();
                        drnew["Person_id"] = Convert.ToInt32(LstNewejecutivo.Items[i].Value);
                        drnew["name_user"] = LstNewejecutivo.Items[i].Text;
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&amp;", "&");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#193;", "á");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#201;", "é");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#205;", "í");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#211;", "ó");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#218;", "ú");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#225;", "á");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#233;", "é");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#237;", "í");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#243;", "ó");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#250;", "ú");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#209;", "ñ");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#241;", "ñ");


                        dtAsignadosTemp.Rows.Add(drnew);
                        GvEjecutivosAsignados.DataSource = dtAsignadosTemp;
                        GvEjecutivosAsignados.DataBind();
                        //GvEjecutivosAsignados.Ro ChkListSupervisores.Items.Insert(0, new ListItem(LstNewSupervisor.Items[i].Text, LstNewSupervisor.Items[i].Value));
                    }
                    LstNewejecutivo.Items[i].Selected = false;
                }
            }
            for (int i = 0; i <= GvEjecutivosAsignados.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= GvEliminaEjecutivos.Rows.Count - 1; j++)
                {
                    if (GvEjecutivosAsignados.Rows[i].Cells[1].Text == GvEliminaEjecutivos.Rows[j].Cells[0].Text)
                    {
                        DataTable dtEliminaAsignadosTemp = new DataTable();
                        dtEliminaAsignadosTemp.Columns.Add("Person_id", typeof(Int32));
                        dtEliminaAsignadosTemp.Columns.Add("name_user", typeof(String));
                        for (int k = 0; k <= GvEliminaEjecutivos.Rows.Count - 1; k++)
                        {
                            DataRow dr = dtEliminaAsignadosTemp.NewRow();
                            dr["Person_id"] = Convert.ToInt32(GvEliminaEjecutivos.Rows[k].Cells[0].Text);
                            dr["name_user"] = GvEliminaEjecutivos.Rows[k].Cells[1].Text;
                            dtEliminaAsignadosTemp.Rows.Add(dr);

                        }
                        DataRow drnew = dtEliminaAsignadosTemp.NewRow();
                        dtEliminaAsignadosTemp.Rows[GvEliminaEjecutivos.Rows[j].RowIndex].Delete();
                        this.Session["dtEliminarejecutivoTemp"] = dtEliminaAsignadosTemp;
                        GvEliminaEjecutivos.DataSource = dtEliminaAsignadosTemp;
                        GvEliminaEjecutivos.DataBind();
                        j = GvEliminaEjecutivos.Rows.Count - 1;
                    }
                }
            }
            ModalPanelResponsablesCampaña.Show();
        }
        protected void btnAddSupervisores_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= LstNewSupervisor.Items.Count - 1; i++)
            {
                if (LstNewSupervisor.Items[i].Selected == true)
                {
                    bool continuar = true;
                    for (int j = 0; j <= GvSupervisoresAsignados.Rows.Count - 1; j++)
                    {
                        if (GvSupervisoresAsignados.Rows[j].Cells[1].Text == LstNewSupervisor.Items[i].Value)
                        {
                            continuar = false;
                            j = GvSupervisoresAsignados.Rows.Count - 1;
                        }
                    }
                    if (continuar)
                    {
                        DataTable dtAsignadosTemp = new DataTable();
                        dtAsignadosTemp.Columns.Add("Person_id", typeof(Int32));
                        dtAsignadosTemp.Columns.Add("name_user", typeof(String));
                        for (int k = 0; k <= GvSupervisoresAsignados.Rows.Count - 1; k++)
                        {
                            DataRow dr = dtAsignadosTemp.NewRow();
                            dr["Person_id"] = Convert.ToInt32(GvSupervisoresAsignados.Rows[k].Cells[1].Text);
                            dr["name_user"] = GvSupervisoresAsignados.Rows[k].Cells[2].Text;
                            dr["name_user"] = dr["name_user"].ToString().Replace("&amp;", "&");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#193;", "á");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#201;", "é");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#205;", "í");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#211;", "ó");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#218;", "ú");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#225;", "á");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#233;", "é");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#237;", "í");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#243;", "ó");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#250;", "ú");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#209;", "ñ");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#241;", "ñ");

                            dtAsignadosTemp.Rows.Add(dr);
                        }
                        DataRow drnew = dtAsignadosTemp.NewRow();
                        drnew["Person_id"] = Convert.ToInt32(LstNewSupervisor.Items[i].Value);
                        drnew["name_user"] = LstNewSupervisor.Items[i].Text;
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&amp;", "&");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#193;", "á");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#201;", "é");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#205;", "í");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#211;", "ó");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#218;", "ú");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#225;", "á");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#233;", "é");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#237;", "í");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#243;", "ó");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#250;", "ú");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#209;", "ñ");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#241;", "ñ");


                        dtAsignadosTemp.Rows.Add(drnew);
                        GvSupervisoresAsignados.DataSource = dtAsignadosTemp;
                        GvSupervisoresAsignados.DataBind();
                    }
                    LstNewSupervisor.Items[i].Selected = false;
                }
            }
            for (int i = 0; i <= GvSupervisoresAsignados.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= GvEliminaSupervisor.Rows.Count - 1; j++)
                {
                    if (GvSupervisoresAsignados.Rows[i].Cells[1].Text == GvEliminaSupervisor.Rows[j].Cells[0].Text)
                    {
                        DataTable dtEliminaAsignadosTemp = new DataTable();
                        dtEliminaAsignadosTemp.Columns.Add("Person_id", typeof(Int32));
                        dtEliminaAsignadosTemp.Columns.Add("name_user", typeof(String));
                        for (int k = 0; k <= GvEliminaSupervisor.Rows.Count - 1; k++)
                        {
                            DataRow dr = dtEliminaAsignadosTemp.NewRow();
                            dr["Person_id"] = Convert.ToInt32(GvEliminaSupervisor.Rows[k].Cells[0].Text);
                            dr["name_user"] = GvEliminaSupervisor.Rows[k].Cells[1].Text;
                            dtEliminaAsignadosTemp.Rows.Add(dr);

                        }
                        DataRow drnew = dtEliminaAsignadosTemp.NewRow();
                        dtEliminaAsignadosTemp.Rows[GvEliminaSupervisor.Rows[j].RowIndex].Delete();
                        this.Session["dtEliminarsupervisorTemp"] = dtEliminaAsignadosTemp;
                        GvEliminaSupervisor.DataSource = dtEliminaAsignadosTemp;
                        GvEliminaSupervisor.DataBind();
                        j = GvEliminaSupervisor.Rows.Count - 1;
                    }
                }
            }
            ModalPanelResponsablesCampaña.Show();
        }
        protected void btnAddMercaderistas_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= LstNewMercaderista.Items.Count - 1; i++)
            {
                if (LstNewMercaderista.Items[i].Selected == true)
                {
                    bool continuar = true;
                    for (int j = 0; j <= GvMercaderistasAsignados.Rows.Count - 1; j++)
                    {
                        if (GvMercaderistasAsignados.Rows[j].Cells[1].Text == LstNewMercaderista.Items[i].Value)
                        {
                            continuar = false;
                            j = GvMercaderistasAsignados.Rows.Count - 1;
                        }
                    }
                    if (continuar)
                    {
                        DataTable dtAsignadosTemp = new DataTable();
                        dtAsignadosTemp.Columns.Add("Person_id", typeof(Int32));
                        dtAsignadosTemp.Columns.Add("name_user", typeof(String));
                        for (int k = 0; k <= GvMercaderistasAsignados.Rows.Count - 1; k++)
                        {
                            DataRow dr = dtAsignadosTemp.NewRow();
                            dr["Person_id"] = Convert.ToInt32(GvMercaderistasAsignados.Rows[k].Cells[1].Text);
                            dr["name_user"] = GvMercaderistasAsignados.Rows[k].Cells[2].Text;
                            dr["name_user"] = dr["name_user"].ToString().Replace("&amp;", "&");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#193;", "á");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#201;", "é");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#205;", "í");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#211;", "ó");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#218;", "ú");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#225;", "á");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#233;", "é");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#237;", "í");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#243;", "ó");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#250;", "ú");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#209;", "ñ");
                            dr["name_user"] = dr["name_user"].ToString().Replace("&#241;", "ñ");

                            dtAsignadosTemp.Rows.Add(dr);
                        }
                        DataRow drnew = dtAsignadosTemp.NewRow();
                        drnew["Person_id"] = Convert.ToInt32(LstNewMercaderista.Items[i].Value);
                        drnew["name_user"] = LstNewMercaderista.Items[i].Text;
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&amp;", "&");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#193;", "á");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#201;", "é");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#205;", "í");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#211;", "ó");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#218;", "ú");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#225;", "á");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#233;", "é");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#237;", "í");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#243;", "ó");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#250;", "ú");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#209;", "ñ");
                        drnew["name_user"] = drnew["name_user"].ToString().Replace("&#241;", "ñ");

                        dtAsignadosTemp.Rows.Add(drnew);
                        GvMercaderistasAsignados.DataSource = dtAsignadosTemp;
                        GvMercaderistasAsignados.DataBind();
                    }
                    LstNewMercaderista.Items[i].Selected = false;
                }
            }
            for (int i = 0; i <= GvMercaderistasAsignados.Rows.Count - 1; i++)
            {
                for (int j = 0; j <= GvEliminaMercaderista.Rows.Count - 1; j++)
                {
                    if (GvMercaderistasAsignados.Rows[i].Cells[1].Text == GvEliminaMercaderista.Rows[j].Cells[0].Text)
                    {
                        DataTable dtEliminaAsignadosTemp = new DataTable();
                        dtEliminaAsignadosTemp.Columns.Add("Person_id", typeof(Int32));
                        dtEliminaAsignadosTemp.Columns.Add("name_user", typeof(String));
                        for (int k = 0; k <= GvEliminaMercaderista.Rows.Count - 1; k++)
                        {
                            DataRow dr = dtEliminaAsignadosTemp.NewRow();
                            dr["Person_id"] = Convert.ToInt32(GvEliminaMercaderista.Rows[k].Cells[0].Text);
                            dr["name_user"] = GvEliminaMercaderista.Rows[k].Cells[1].Text;
                            dtEliminaAsignadosTemp.Rows.Add(dr);
                        }
                        DataRow drnew = dtEliminaAsignadosTemp.NewRow();
                        dtEliminaAsignadosTemp.Rows[GvEliminaMercaderista.Rows[j].RowIndex].Delete();
                        this.Session["dtEliminarmercaderistaTemp"] = dtEliminaAsignadosTemp;
                        GvEliminaMercaderista.DataSource = dtEliminaAsignadosTemp;
                        GvEliminaMercaderista.DataBind();
                        j = GvEliminaMercaderista.Rows.Count - 1;
                    }
                }
            }
            ModalPanelResponsablesCampaña.Show();
        }
        protected void BtnclosePanelejecutivo_Click(object sender, ImageClickEventArgs e)
        {
            ModalPanelResponsablesCampaña.Show();
        }
        protected void BtnclosePanelSupervisor_Click(object sender, ImageClickEventArgs e)
        {
            ModalPanelResponsablesCampaña.Show();
        }
        protected void BtnclosePanelMercaderista_Click(object sender, ImageClickEventArgs e)
        {
            ModalPanelResponsablesCampaña.Show();
        }
        protected void BtnEditResponsables_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            BtnEditResponsables.Visible = false;
            BtnUpdateResponsables.Visible = true;
            ActivaControlesResponsablesCampaña();
            ModalPanelResponsablesCampaña.Show();
            
            DataTable dtEliminarejecutivoTemp = new DataTable();
            dtEliminarejecutivoTemp.Columns.Add("Person_id", typeof(Int32));
            dtEliminarejecutivoTemp.Columns.Add("name_user", typeof(String));
            this.Session["dtEliminarejecutivoTemp"] = dtEliminarejecutivoTemp;
            llenaNuevosSupervisores();
            llenaNuevosMercaderistas();
        }
        protected void BtnUpdateResponsables_Click(object sender, EventArgs e)
        {
            // registrar el ejecutivo de cuenta
            DAplicacion odDuplicado = new DAplicacion();
            for (int i = 0; i <= GvEjecutivosAsignados.Rows.Count - 1; i++)
            {
                DataTable dtDuplicadoEjecutivo = odDuplicado.ConsultaDuplicados(ConfigurationManager.AppSettings["Staff_Planning"], TxtPlanningRes.Text, GvEjecutivosAsignados.Rows[i].Cells[1].Text, null);
                if (dtDuplicadoEjecutivo == null)
                {
                    Staff_Planning.RegistrarPersonal(TxtPlanningRes.Text, Convert.ToInt32(GvEjecutivosAsignados.Rows[i].Cells[1].Text), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                }
                else
                {
                    Staff_Planning.ActualizaPersonal(TxtPlanningRes.Text, Convert.ToInt32(GvEjecutivosAsignados.Rows[i].Cells[1].Text), true, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                }
                dtDuplicadoEjecutivo = null;
            }
            // registrar supervisores seleccionados
            for (int i = 0; i <= GvSupervisoresAsignados.Rows.Count - 1; i++)
            {
                DataTable dtDuplicadoSupervisor = odDuplicado.ConsultaDuplicados(ConfigurationManager.AppSettings["Staff_Planning"], TxtPlanningRes.Text, GvSupervisoresAsignados.Rows[i].Cells[1].Text, null);
                if (dtDuplicadoSupervisor == null)
                {
                    Staff_Planning.RegistrarPersonal(TxtPlanningRes.Text, Convert.ToInt32(GvSupervisoresAsignados.Rows[i].Cells[1].Text), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                    try
                    {
                        Staff_Planning.RegistrarTBL_PERFIL(Convert.ToInt32(GvSupervisoresAsignados.Rows[i].Cells[1].Text), TxtPlanningRes.Text, this.Session["company_id"].ToString().Trim());
                    }
                    catch (Exception ex)
                    {
                        // el planning no esta registrado en la base de datos intermedia por lo cual no insertará el perfil en tbl_perfil
                    }
                }
                else
                {
                    Staff_Planning.ActualizaPersonal(TxtPlanningRes.Text, Convert.ToInt32(GvSupervisoresAsignados.Rows[i].Cells[1].Text), true, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                }
                dtDuplicadoSupervisor = null;

            }

            // registrar mercaderistas seleccionados
            for (int i = 0; i <= GvMercaderistasAsignados.Rows.Count - 1; i++)
            {
                DataTable dtDuplicadoMercaderista = odDuplicado.ConsultaDuplicados(ConfigurationManager.AppSettings["Staff_Planning"], TxtPlanningRes.Text, GvMercaderistasAsignados.Rows[i].Cells[1].Text, null);
                if (dtDuplicadoMercaderista == null)
                {
                    Staff_Planning.RegistrarPersonal(TxtPlanningRes.Text, Convert.ToInt32(GvMercaderistasAsignados.Rows[i].Cells[1].Text), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                }
                else
                {
                    Staff_Planning.ActualizaPersonal(TxtPlanningRes.Text, Convert.ToInt32(GvMercaderistasAsignados.Rows[i].Cells[1].Text), true, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                }
                dtDuplicadoMercaderista = null;
            }

            //deshabilita ejecutivos 
            for (int i = 0; i <= GvEliminaEjecutivos.Rows.Count - 1; i++)
            {
                Staff_Planning.ActualizaPersonal(TxtPlanningRes.Text, Convert.ToInt32(GvEliminaEjecutivos.Rows[i].Cells[0].Text), false, this.Session["sUser"].ToString().Trim(), DateTime.Now);
            }
            //deshabilita supervisores 
            Conexion cn = new Conexion();
            DataTable dt;
            for (int i = 0; i <= GvEliminaSupervisor.Rows.Count - 1; i++)
            {
                dt = cn.ejecutarDataTable("UP_WEBSIGE_PLANNING_CONSULTARMERCADERISTASXSUPERVISOR", TxtPlanningRes.Text, Convert.ToInt32(GvEliminaSupervisor.Rows[i].Cells[0].Text));
                if (dt.Rows.Count == 0)
                {
                    Staff_Planning.ActualizaPersonal(TxtPlanningRes.Text, Convert.ToInt32(GvEliminaSupervisor.Rows[i].Cells[0].Text), false, this.Session["sUser"].ToString().Trim(), DateTime.Now);
                }
                else
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "El supervisor con el codigo : " + Convert.ToInt32(GvEliminaSupervisor.Rows[i].Cells[0].Text) + " no se puede eliminar por tener mercaderistas asignados";
                    Mensajes_ResponsablesCampaña();
                    return;
                }
            }
            //deshabilita mercaderistas 
            for (int i = 0; i <= GvEliminaMercaderista.Rows.Count - 1; i++)
            {
                Staff_Planning.ActualizaPersonal(TxtPlanningRes.Text, Convert.ToInt32(GvEliminaMercaderista.Rows[i].Cells[0].Text), false, this.Session["sUser"].ToString().Trim(), DateTime.Now);
            }
            DesactivarControlesResponsablesCampaña();
            BtnEditResponsables.Visible = true;
            BtnUpdateResponsables.Visible = false;
            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupConfirm";
            this.Session["mensaje"] = "Se ha Actualizado con éxito los responsables para la campaña : " + LblTxtPresupuestoRes.Text.ToUpper();
            Mensajes_ResponsablesCampaña();
        }
        protected void BbtnaceptarResp_Click(object sender, EventArgs e)
        {
            ModalPanelResponsablesCampaña.Show();
        }
        protected void BtnClearRespons_Click(object sender, EventArgs e)
        {
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();
            BtnEditResponsables.Visible = true;
            BtnUpdateResponsables.Visible = false;
            DesactivarControlesResponsablesCampaña();

            DataTable dtEliminarejecutivoTemp = new DataTable();
            dtEliminarejecutivoTemp.Columns.Add("Person_id", typeof(Int32));
            dtEliminarejecutivoTemp.Columns.Add("name_user", typeof(String));
            this.Session["dtEliminarejecutivoTemp"] = dtEliminarejecutivoTemp;

            DataTable dtEliminarsupervisorTemp = new DataTable();
            dtEliminarsupervisorTemp.Columns.Add("Person_id", typeof(Int32));
            dtEliminarsupervisorTemp.Columns.Add("name_user", typeof(String));
            this.Session["dtEliminarsupervisorTemp"] = dtEliminarsupervisorTemp;

            DataTable dtEliminarmercaderistaTemp = new DataTable();
            dtEliminarmercaderistaTemp.Columns.Add("Person_id", typeof(Int32));
            dtEliminarmercaderistaTemp.Columns.Add("name_user", typeof(String));
            this.Session["dtEliminarmercaderistaTemp"] = dtEliminarmercaderistaTemp;
            llenaEjecutivos();
            llenaNuevosSupervisores();
            llenaNuevosMercaderistas();
            ModalPanelResponsablesCampaña.Show();
            ConsultaResponsablesCamapaña();
            GvEliminaEjecutivos.DataBind();
            GvEliminaSupervisor.DataBind();
            GvEliminaMercaderista.DataBind();
        }
        #endregion

        #region Asignacion de personal
        protected void ImgIrAASigPersonal_Click(object sender, ImageClickEventArgs e)
        {
            botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();

            ModalPanelAsignaPersonal.Show();
            GvAsignados.DataBind();

            DataSet ds = (DataSet)this.ViewState["planning_creados"];
            if (ds.Tables[5].Rows.Count > 0)
            {
                TxtPlanningAsig.Text = ds.Tables[9].Rows[0]["id_planning"].ToString().Trim();
                LblTxtPresupuestoAsig.Text = ds.Tables[9].Rows[0]["Planning_Name"].ToString().Trim();
            }
            llenaStaffplanning();
            ds = null;
        }
        protected void CmbSelSupervisoresAsig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbSelSupervisoresAsig.Text != "0")
            {
                BtnMasAsing.Enabled = true;
                DataTable dt = PPlanning.Get_ObtenerMercaderistasxSupervisor(Convert.ToInt32(CmbSelSupervisoresAsig.Text), TxtPlanningAsig.Text);
                GvAsignados.DataSource = dt;
                GvAsignados.DataBind();
                dt = null;
            }
            else
            {
                GvAsignados.DataBind();
                BtnMasAsing.Enabled = false;
            }
            ModalPanelAsignaPersonal.Show();
        }
        protected void GvAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbSelSupervisoresAsig.Enabled = false;
            BtnSaveAsig.Enabled = true;

            DataTable dtAsigna_Temp = new DataTable();
            dtAsigna_Temp.Columns.Add("Person_id", typeof(String));
            dtAsigna_Temp.Columns.Add("Nombre", typeof(String));

            for (int i = 0; i <= GvAsignados.Rows.Count - 1; i++)
            {
                DataRow dr = dtAsigna_Temp.NewRow();
                dr["Person_id"] = GvAsignados.Rows[i].Cells[1].Text;
                dr["Nombre"] = GvAsignados.Rows[i].Cells[2].Text;
                dtAsigna_Temp.Rows.Add(dr);
            }
            ListItem newList = new ListItem();
            newList.Value = GvAsignados.SelectedRow.Cells[1].Text.ToString().Trim();
            newList.Text = GvAsignados.SelectedRow.Cells[2].Text.ToString().Trim();

            newList.Text = newList.Text.Replace("&amp;", "&");
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
            GridViewRow row = GvAsignados.SelectedRow;
            dtAsigna_Temp.Rows[row.RowIndex].Delete();
            GvAsignados.DataSource = dtAsigna_Temp;
            GvAsignados.DataBind();
            dtAsigna_Temp = null;
            ModalPanelAsignaPersonal.Show();
        }
        protected void BtnMasAsing_Click(object sender, EventArgs e)
        {
            if (LstBoxMercaderistas.SelectedIndex != -1)
            {
                CmbSelSupervisoresAsig.Enabled = false;
                BtnSaveAsig.Enabled = true;

                DataTable dtAsigna_Temp = new DataTable();
                dtAsigna_Temp.Columns.Add("Person_id", typeof(String));
                dtAsigna_Temp.Columns.Add("Nombre", typeof(String));

                for (int i = 0; i <= GvAsignados.Rows.Count - 1; i++)
                {
                    DataRow dr = dtAsigna_Temp.NewRow();
                    dr["Person_id"] = GvAsignados.Rows[i].Cells[1].Text;
                    dr["Nombre"] = GvAsignados.Rows[i].Cells[2].Text;
                    dtAsigna_Temp.Rows.Add(dr);

                }

                for (int i = 0; i <= LstBoxMercaderistas.Items.Count - 1; i++)
                {
                    if (LstBoxMercaderistas.Items[i].Selected == true)
                    {
                        DataRow dr = dtAsigna_Temp.NewRow();
                        dr["Person_id"] = LstBoxMercaderistas.Items[i].Value;
                        dr["Nombre"] = LstBoxMercaderistas.Items[i].Text;
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&amp;", "&");
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&#193;", "á");
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&#201;", "é");
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&#205;", "í");
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&#211;", "ó");
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&#218;", "ú");
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&#225;", "á");
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&#233;", "é");
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&#237;", "í");
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&#243;", "ó");
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&#250;", "ú");
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&#209;", "ñ");
                        dr["Nombre"] = dr["Nombre"].ToString().Replace("&#241;", "ñ");
                        dtAsigna_Temp.Rows.Add(dr);
                        LstBoxMercaderistas.Items.Remove(LstBoxMercaderistas.Items[i]);
                        i--;
                    }
                }
                GvAsignados.DataSource = dtAsigna_Temp;
                GvAsignados.DataBind();
                dtAsigna_Temp = null;
                ModalPanelAsignaPersonal.Show();
            }
            else
            {
                ModalPanelAsignaPersonal.Show();
            }
        }
        protected void BtnEditAsigna_Click(object sender, EventArgs e)
        {
            InicializarPaneles();
            BtnEditAsigna.Visible = false;
            BtnSaveAsig.Visible = true;
            ActivaControlesAsignaPersonal();
            ModalPanelAsignaPersonal.Show();
        }
        protected void BtnSaveAsig_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= LstBoxMercaderistas.Items.Count - 1; i++)
            {
                DataTable dt = PPlanning.Get_VerficaAsignaOperativo(TxtPlanningAsig.Text, Convert.ToInt32(LstBoxMercaderistas.Items[i].Value));
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        // delete a la tabla de asignacion de personal asignacion de operativos a supervisores
                        Conexion oConn = new Conexion();
                        PPlanning.Get_EliminarOperativosxSupervisor(TxtPlanningAsig.Text, Convert.ToInt32(LstBoxMercaderistas.Items[i].Value));
                    }
                }
                dt = null;
            }
            for (int i = 0; i <= GvAsignados.Rows.Count - 1; i++)
            {
                DataTable dt = PPlanning.Get_VerficaAsignaOperativo(TxtPlanningAsig.Text, Convert.ToInt32(GvAsignados.Rows[i].Cells[1].Text));
                if (dt != null)
                {
                    if (dt.Rows.Count <= 0)
                    {
                        // insertar  a la tabla de asignacion de personal asignacion de operativos a supervisores
                        PPlanning.Get_Register_OperativosxSupervisor(TxtPlanningAsig.Text, Convert.ToInt32(CmbSelSupervisoresAsig.Text), Convert.ToInt32(GvAsignados.Rows[i].Cells[1].Text), true, this.Session["sUser"].ToString(), DateTime.Now, this.Session["sUser"].ToString(), DateTime.Now);
                    }
                }
                dt = null;
            }
            BtnSaveAsig.Enabled = false;
            CmbSelSupervisoresAsig.Enabled = true;
            llenaStaffplanning();
            GvAsignados.DataBind();
            ModalPanelAsignaPersonal.Show();

        }
        protected void BtnClearAsig_Click(object sender, EventArgs e)
        {
            GvAsignados.DataBind();
            //pasar a una funcion 
            DataSet ds = new DataSet();
            //ds = PPlanning.Get_PlanningCreados(CmbSelCampaña.SelectedValue);
            ds = (DataSet)this.ViewState["planning_creados"];
            if (ds.Tables[5].Rows.Count > 0)
            {
                TxtPlanningAsig.Text = ds.Tables[9].Rows[0]["id_planning"].ToString().Trim();
                LblTxtPresupuestoAsig.Text = ds.Tables[9].Rows[0]["Planning_Name"].ToString().Trim();
            }
            llenaStaffplanning();
            ds = null;
            BtnEditAsigna.Visible = true;
            BtnSaveAsig.Visible = false;
            BtnSaveAsig.Enabled = false;
            DesactivarControlesAsignaPersonal();
            ModalPanelAsignaPersonal.Show();
        }
        #endregion

        #region Puntos de Venta
        protected void ImgIrAPDV_Click(object sender, ImageClickEventArgs e)
        {
            botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();

            ConsultaPDVCampaña();
            ModalPanelPDV.Show();
            ifcarga.Attributes["src"] = "carga_PDV.aspx";
        }
        protected void CmbSelCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaTipoAgrup();
            ModalPanelPDV.Show();
        }
        protected void CmbSelTipoAgrup_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaAgrup();
            ModalPanelPDV.Show();
        }
        protected void CmbSelAgrup_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaOficinas();
            ModalPanelPDV.Show();
        }
        protected void CmbSelOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            Llenamallas();
            Llenasector();
            llenaGrillaPDV();
            ModalPanelPDV.Show();
        }
        protected void CmbSelMalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            Llenasector();
            llenaGrillaPDV();
            ModalPanelPDV.Show();
        }
        protected void CmbSelSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaGrillaPDV();
            ModalPanelPDV.Show();
        }
        protected void GvPDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            // eliminar registros de la tabla puntos de venta planning
            try
            {
                PPlanning.Get_EliminarPDVPlanning(Convert.ToInt32(GVPDVDelete.Rows[GvPDV.SelectedIndex].Cells[5].Text));
                llenaGrillaPDV();
                ModalPanelPDV.Show();
            }
            catch (Exception ex)
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "No es posible eliminar el punto de venta ya que está asignado a un Mercaderista";
                Mensajes_PuntosdeVenta();
            }           
        }
        protected void btnaceptarMPDV_Click(object sender, EventArgs e)
        {
            ModalPanelPDV.Show();
        }
        #endregion

        #region Paneles
        protected void ImgIrAPaneles_Click(object sender, ImageClickEventArgs e)
        {
            LblTitCountReg.Visible = false;
            lblcountreg.Visible = false;
            lblcountreg.Text = "";
            BtnAddPanel.Visible = false;
            BtnEliminarRegpanel.Visible = false;
            botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();
            ModalPanelPaneles.Show();
            PanelPaneles.Style.Value = "display:block";
            DataSet ds = new DataSet();
            //ds = PPlanning.Get_PlanningCreados(CmbSelCampaña.SelectedValue);
            ds = (DataSet)this.ViewState["planning_creados"];
            if (ds.Tables[11].Rows.Count > 0)
            {
                TxtCodPlanningPanel.Text = ds.Tables[9].Rows[0]["id_planning"].ToString().Trim();
                LblSelPresupuestoPanel.Text = ds.Tables[9].Rows[0]["Planning_Name"].ToString().Trim();
                DataTable dtCliente = Presupuesto.Get_ObtenerClientes(ds.Tables[9].Rows[0]["Planning_Budget"].ToString().Trim(), 1);
                this.Session["company_id"] = dtCliente.Rows[0]["Company_id"].ToString().Trim();
                this.Session["Planning_CodChannel"] = ds.Tables[9].Rows[0]["Planning_CodChannel"].ToString().Trim();
                dtCliente = null;
            }
            llenainformesPaneles();
            ds = null;
        }

        public void llenarPeriodos(DropDownList ddl,string planning, int report, string año, string mes)
        {

            // string a = Session["idPlanning"].ToString();
            //string b = Session["id_report"].ToString();
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBSIGE_PLANNING_OBTENERPERIODOS_xPlanningReporteAñoMes", planning, report, año, mes);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataValueField = "id_ReportsPlanning";
                    ddl.DataTextField = "Perido";
                    ddl.DataBind();
                }
                else
                {
                    ddl.Items.Clear();
                }
            }

            dt = null;

        }
        protected void ddlmes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RbtnListReportPanel.SelectedItem == null)
            {
                ModalPanelPaneles.Show();
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Debe seleccionar un reporte";
                Mensajes_Paneles();

            }
            else
            {
                llenarPeriodos(ddlPeriodo, TxtCodPlanningPanel.Text, Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value), ddlAño.SelectedValue, ddlmes.SelectedValue);
                ModalPanelPaneles.Show();
            }
        }



        public void llenarpanelesXperido()
        {
              DPlanning odplanning = new DPlanning();
            DataTable dt = odplanning.Consulta_Pla_paneles(TxtCodPlanningPanel.Text, Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value),0);

           // DataTable dt = PPlanning.Consulta_Pla_paneles(TxtCodPlanningPanel.Text, Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value));
            GvPDVPaneles.DataSource = dt;
            GvPDVPaneles.DataBind();
            GvPDVPaneles.BorderColor = System.Drawing.Color.Blue;
            GvPDVPaneles.BorderStyle = BorderStyle.Double;
            GvPDVPaneles.BorderWidth = Unit.Pixel(3);
            GvPDVPaneles.GridLines = GridLines.Horizontal;
            GvPDVPaneles.HeaderStyle.BackColor = System.Drawing.Color.FromName("#336666");
            GvPDVPaneles.HeaderStyle.Font.Bold = true;
            GvPDVPaneles.HeaderStyle.ForeColor = System.Drawing.Color.White;
            GvPDVPaneles.RowStyle.BackColor = System.Drawing.Color.White;
            GvPDVPaneles.RowStyle.ForeColor = System.Drawing.Color.FromName("#333333");
            this.GvPDVPaneles.Columns[5].Visible = true;
            if (this.Session["company_id"].ToString()=="1561")
            {
                this.GvPDVPaneles.Columns[6].Visible = true;
                llenaTipoPanel();
            }
            LblTitCountReg.Visible = true;
            lblcountreg.Visible = true;
            lblcountreg.Text = dt.Rows.Count.ToString().Trim();
            ModalPanelPaneles.Show();
            if (dt.Rows.Count > 0)
            {
                BtnEliminarRegpanel.Visible = true;
                btnActualizarPanel.Visible = true;
            }
            else
            {
                BtnEliminarRegpanel.Visible = false;
            }
            dt = null;
            BtnAddPanel.Visible = true;
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
        protected void RbtnListReportPanel_SelectedIndexChanged(object sender, EventArgs e)
        {

            llenarpanelesXperido();
        }
        protected void BtnEliminarRegpanel_Click(object sender, EventArgs e)
        {
            DPlanning odplanning = new DPlanning();
            bool sigue = false;
            ModalPanelPaneles.Show();
            for (int i = 0; i <= GvPDVPaneles.Rows.Count - 1; i++)
            {
                if (((CheckBox)GvPDVPaneles.Rows[i].Cells[0].FindControl("CheckBox1")).Checked == true)
                {
                    i = GvPDVPaneles.Rows.Count - 1;
                    sigue = true;
                }
            }
            if (sigue)
            {
                for (int i = 0; i <= GvPDVPaneles.Rows.Count - 1; i++)
                {
                    if (((CheckBox)GvPDVPaneles.Rows[i].Cells[0].FindControl("CheckBox1")).Checked == true)
                    {
                        PPlanning.Eliminar_Pla_paneles(Convert.ToInt64(((Label)GvPDVPaneles.Rows[i].FindControl("LblNo")).Text));

                    }
                }
                DataTable dt = odplanning.Consulta_Pla_paneles(TxtCodPlanningPanel.Text, Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value),0);
                GvPDVPaneles.DataSource = dt;
                GvPDVPaneles.DataBind();

                GvPDVPaneles.BorderColor = System.Drawing.Color.Blue;
                GvPDVPaneles.BorderStyle = BorderStyle.Double;
                GvPDVPaneles.BorderWidth = Unit.Pixel(3);
                GvPDVPaneles.GridLines = GridLines.Horizontal;
                GvPDVPaneles.HeaderStyle.BackColor = System.Drawing.Color.FromName("#336666");
                GvPDVPaneles.HeaderStyle.Font.Bold = true;
                GvPDVPaneles.HeaderStyle.ForeColor = System.Drawing.Color.White;
                GvPDVPaneles.RowStyle.BackColor = System.Drawing.Color.White;
                GvPDVPaneles.RowStyle.ForeColor = System.Drawing.Color.FromName("#333333");
                LblTitCountReg.Visible = true;
                lblcountreg.Visible = true;
                lblcountreg.Text = dt.Rows.Count.ToString().Trim();
                if (dt.Rows.Count > 0)
                {
                    BtnEliminarRegpanel.Visible = true;
                }
                else
                {
                    BtnEliminarRegpanel.Visible = false;
                }
                dt = null;
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupConfirm";
                this.Session["mensaje"] = "Se han eliminado los puntos de venta seleccionados.";
                Mensajes_Paneles();
            }
            else
            {
                // mensaje para que el usuario seleccione al menos un punto de venta y asi ejecute el borrado de la bd
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Debe seleccionar al menos un punto de venta";
                Mensajes_Paneles();
            }
        }
        protected void BtnAddPanel_Click(object sender, EventArgs e)
        {
            ModalPanelPaneles.Show();
            llenaGrillaPDVPaneles();
            BtnSavePaneles.Visible = true;
            BtnCancelPaneles.Visible = true;
            BtnEliminarRegpanel.Visible = false;
            BtnAddPanel.Visible = false;
            btnActualizarPanel.Visible = false;
            RbtnListReportPanel.Enabled = false;
            LblTitCountReg.Visible = false;
            lblcountreg.Visible = false;
            lblcountreg.Text = "";

        }

        protected void btnActualizarPanel_Click(object sender, EventArgs e)
        {

            if (ddlPeriodo.SelectedValue == "")
            {

                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Selecione un Periodo";
                Mensajes_Paneles();

                return;
            }

            for (int i = 0; i <= GvPDVPaneles.Rows.Count - 1; i++)
            {
                if (((CheckBox)GvPDVPaneles.Rows[i].Cells[0].FindControl("CheckBox1")).Checked == true)
                {

                    DataTable dt = new DataTable();
                    dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_ACTUALIZAR_PANELES_PLANNING", Convert.ToInt32(((Label)GvPDVPaneles.Rows[i].Cells[0].FindControl("LblNo")).Text), Convert.ToInt32(ddlPeriodo.SelectedValue));

                }

            }

            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupConfirm";
            this.Session["mensaje"] = "Se ha actualizado correctamente los puntos de venta para el panel del reporte " + RbtnListReportPanel.SelectedItem.Text;
            Mensajes_Paneles();

            llenarpanelesXperido();
            ModalPanelPaneles.Show();

        }

        protected void BtnSavePaneles_Click(object sender, EventArgs e)
        {

            if (ddlPeriodo.SelectedValue == "")
            {

                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Selecione un Periodo";
                Mensajes_Paneles();

                return;
            }

     

            ModalPanelPaneles.Show();
            // metodo para agregar nuevos puntos de venta a panel del reporte seleccionado
            bool sigue = false;
            DAplicacion odDuplicado = new DAplicacion();
            for (int i = 0; i <= GvPDVPaneles.Rows.Count - 1; i++)
            {
                if (((CheckBox)GvPDVPaneles.Rows[i].Cells[0].FindControl("CheckBox1")).Checked == true)
                {
                    sigue = true;
                    i = GvPDVPaneles.Rows.Count - 1;
                }
            }

            if (sigue)
            {
                for (int i = 0; i <= GvPDVPaneles.Rows.Count - 1; i++)
                {
                    if (((CheckBox)GvPDVPaneles.Rows[i].Cells[0].FindControl("CheckBox1")).Checked == true)
                    {
                        DataTable dtDuplicado = odDuplicado.ConsultaDuplicados(ConfigurationManager.AppSettings["PLA_Panel_Planning"], TxtCodPlanningPanel.Text,
                                  ((Label)GvPDVPaneles.Rows[i].Cells[0].FindControl("LblNo")).Text,
                                  RbtnListReportPanel.SelectedItem.Value);
                        if (dtDuplicado == null)
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
                            Mensajes_Paneles();
                            sigue = false;
                            i = GvPDVPaneles.Rows.Count - 1;
                        }
                        dtDuplicado = null;
                    }

                }

                DPlanning dp= new DPlanning();
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
                           
                            ((CheckBox)GvPDVPaneles.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = false;
                        }
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se ha configurado correctamente los puntos de venta para el panel del reporte " + RbtnListReportPanel.SelectedItem.Text;
                    Mensajes_Paneles();

                    BtnSavePaneles.Visible = false;

                    RbtnListReportPanel.Enabled = true;
                    DPlanning oDPlanning = new DPlanning();
                    // consulta nuevamente la asignacion al panel del reporte seleccionado
                    DataTable dt = oDPlanning.Consulta_Pla_paneles(TxtCodPlanningPanel.Text, Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value), 0);
                    GvPDVPaneles.DataSource = dt;
                    GvPDVPaneles.DataBind();

                    GvPDVPaneles.BorderColor = System.Drawing.Color.Blue;
                    GvPDVPaneles.BorderStyle = BorderStyle.Double;
                    GvPDVPaneles.BorderWidth = Unit.Pixel(3);
                    GvPDVPaneles.GridLines = GridLines.Horizontal;
                    GvPDVPaneles.HeaderStyle.BackColor = System.Drawing.Color.FromName("#336666");
                    GvPDVPaneles.HeaderStyle.Font.Bold = true;
                    GvPDVPaneles.HeaderStyle.ForeColor = System.Drawing.Color.White;
                    GvPDVPaneles.RowStyle.BackColor = System.Drawing.Color.White;
                    GvPDVPaneles.RowStyle.ForeColor = System.Drawing.Color.FromName("#333333");

                    LblTitCountReg.Visible = true;
                    lblcountreg.Visible = true;
                    lblcountreg.Text = dt.Rows.Count.ToString().Trim();
                    if (dt.Rows.Count > 0)
                    {
                        BtnEliminarRegpanel.Visible = true;
                        btnActualizarPanel.Visible = true;
                    }
                    else
                    {
                        BtnEliminarRegpanel.Visible = false;
                        btnActualizarPanel.Visible = false;
                    }
                    BtnAddPanel.Visible = true;
                    BtnCancelPaneles.Visible = false;
                    dt = null;
                }
            }
            else
            {
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Debe seleccionar al menos un punto de venta";
                Mensajes_Paneles();
            }

        }
        protected void BtnaceptaPaneles_Click(object sender, EventArgs e)
        {
            ModalPanelPaneles.Show();
        }
        protected void BtnCancelPaneles_Click(object sender, EventArgs e)
        {
            /*
            ModalPanelPaneles.Show();

            BtnSavePaneles.Visible = false;
            BtnCancelPaneles.Visible = false;
            RbtnListReportPanel.Enabled = true;

            DataTable dt = PPlanning.Consulta_Pla_paneles(TxtCodPlanningPanel.Text, Convert.ToInt32(RbtnListReportPanel.SelectedItem.Value));
            GvPDVPaneles.DataSource = dt;
            GvPDVPaneles.DataBind();

            GvPDVPaneles.BorderColor = System.Drawing.Color.Blue;
            GvPDVPaneles.BorderStyle = BorderStyle.Double;
            GvPDVPaneles.BorderWidth = Unit.Pixel(3);
            GvPDVPaneles.GridLines = GridLines.Horizontal;
            GvPDVPaneles.HeaderStyle.BackColor = System.Drawing.Color.FromName("#336666");
            GvPDVPaneles.HeaderStyle.Font.Bold = true;
            GvPDVPaneles.HeaderStyle.ForeColor = System.Drawing.Color.White;
            GvPDVPaneles.RowStyle.BackColor = System.Drawing.Color.White;
            GvPDVPaneles.RowStyle.ForeColor = System.Drawing.Color.FromName("#333333");

            LblTitCountReg.Visible = true;
            lblcountreg.Visible = true;
            lblcountreg.Text = dt.Rows.Count.ToString().Trim();
            if (dt.Rows.Count > 0)
            {
                BtnEliminarRegpanel.Visible = true;
            }
            else
            {
                BtnEliminarRegpanel.Visible = false;
            }
            BtnAddPanel.Visible = true;
            dt = null;
             */
        }

        #endregion

        #region Rutas
        protected void ImgIrAAsignapdv_Click(object sender, ImageClickEventArgs e)
        {
            botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();

            ModalPanelAsignacionPDVaoper.Show();
            GvAsignaPDVOPE.DataBind();
            ConsultaPDVCampañaRutas();
            llenaOperativosAsignaPDVOPE();


        }
        protected void BtnEditAsigPDVOPE_Click(object sender, EventArgs e)
        {
            CmbSelOpePlanning.Enabled = true;
            BtnEditAsigPDVOPE.Visible = false;
            BtnUpdateAsigPDVOPE.Visible = true;
            ModalPanelAsignacionPDVaoper.Show();
        }
        protected void BtnClearAsigPDVOPE_Click(object sender, EventArgs e)
        {
            Limpiar_InformacionAsignaPDVOPE();
            ModalPanelAsignacionPDVaoper.Show();

        }
        protected void CmbSelOpePlanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModalPanelAsignacionPDVaoper.Show();
            if (CmbSelOpePlanning.Text != "0")
            {
                Button1.Visible = true;
                BtnAllAsigPDV.Visible = true;
                BtnNoneasigPDV.Visible = true;
                ConsultaPDVXoperativo();
                llenaciudadesRutas();
                CmbSelTipoAgrupRutas.Items.Clear();
                CmbSelAgrupRutas.Items.Clear();
                CmbSelOficinaRutas.Items.Clear();
                CmbSelMallasRutas.Items.Clear();
                CmbSelSectorRutas.Items.Clear();
                ChkListPDV.Items.Clear();
                CmbSelCityRutas.Enabled = true;
                CmbSelTipoAgrupRutas.Enabled = true;
                CmbSelAgrupRutas.Enabled = true;
                CmbSelOficinaRutas.Enabled = true;
                CmbSelMallasRutas.Enabled = true;
                CmbSelSectorRutas.Enabled = true;
                ChkListPDV.Enabled = true;
                TxtF_iniPDVOPE.Enabled = true;
                TxtF_finPDVOPE.Enabled = true;
                BtnCalF_iniPDVOPE.Enabled = true;
                BtnCalF_finPDVOPE.Enabled = true;
                BtnAsigPDVOPE.Enabled = true;
            }
            else
            {
                Limpiar_InformacionAsignaPDVOPE();

            }
        }
        protected void CmbSelCityRutas_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaTipoAgrupRutas();
            ModalPanelAsignacionPDVaoper.Show();
        }
        protected void CmbSelTipoAgrupRutas_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaAgrupRutas();
            ModalPanelAsignacionPDVaoper.Show();
        }
        protected void CmbSelAgrupRutas_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaOficinasRutas();
            ModalPanelAsignacionPDVaoper.Show();
        }
        protected void CmbSelOficinaRutas_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenamallasRutas();
            LlenasectorRutas();
            LlenaPDVPlanning();
            ModalPanelAsignacionPDVaoper.Show();
        }
        protected void CmbSelMallasRutas_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenasectorRutas();
            LlenaPDVPlanning();
            ModalPanelAsignacionPDVaoper.Show();
        }
        protected void CmbSelSectorRutas_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaPDVPlanning();
            ModalPanelAsignacionPDVaoper.Show();
        }
        protected void BtnAllPDV_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= ChkListPDV.Items.Count - 1; i++)
            {
                ChkListPDV.Items[i].Selected = true;
            }
            ModalPanelAsignacionPDVaoper.Show();
        }
        protected void BtnNonePDV_Click(object sender, EventArgs e)
        {
            ChkListPDV.ClearSelection();
            ModalPanelAsignacionPDVaoper.Show();
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
                Mensajes_AsigPDVOPE();
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
                        Mensajes_AsigPDVOPE();
                        seguir = false;
                    }

                    if (FechaInicial < FechainicialPlanning || Fechafinal > FechafinalPlanning)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "Las fechas deben estar dentro del rango : " + FechainicialPlanning.ToShortDateString() + " y " + FechafinalPlanning.ToShortDateString() + " que corresponden a las fechas de ejecución de la Campaña";
                        Mensajes_AsigPDVOPE();
                        seguir = false;
                    }
                    if (FechaInicial < DateTime.Today)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La fecha inicial debe ser igual o superior a la fecha actual";
                        Mensajes_AsigPDVOPE();
                        seguir = false;
                    }

                }
                catch
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Formato de fecha no valido. Por favor verifique (dd/mm/aaaa)";
                    Mensajes_AsigPDVOPE();
                }
            }

            if (seguir)
            {
                for (int i = 0; i <= ChkListPDV.Items.Count - 1; i++)
                {
                    if (ChkListPDV.Items[i].Selected == true)
                    {
                        DataTable dtconsulta = PPlanning.Get_AsignacionDuplicadaPDV(Convert.ToInt32(ChkListPDV.Items[i].Value), Convert.ToInt32(CmbSelOpePlanning.SelectedItem.Value), TxtPlanningAsigPDVOPE.Text);
                        if (dtconsulta != null)
                        {
                            if (dtconsulta.Rows.Count == 0)
                            {
                                seguir = true;
                            }
                            // Se elimina funcionalidad : solo debe permitir guardar una vez un punto de venta por operativo sin importar la fecha. 
                            // 29/01/2011 Ing. Mauricio Ortiz 
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
                                Mensajes_AsigPDVOPE();
                                //k = dtconsulta.Rows.Count - 1;
                                i = ChkListPDV.Items.Count - 1;

                                seguir = false;
                                //}
                                //else
                                //{
                                //    seguir = true;
                                //}
                                //}
                            }
                        }
                        dtconsulta = null;
                        if (seguir)
                        {
                            #region paso
                            if (GvNewAsignaPDVOPE.Rows.Count > 0)
                            {
                                for (int j = 0; j <= GvNewAsignaPDVOPE.Rows.Count - 1; j++)
                                {
                                    if (CmbSelOpePlanning.SelectedItem.Value == GvNewAsignaPDVOPE.Rows[j].Cells[1].Text &&
                                        ChkListPDV.Items[i].Value == GvNewAsignaPDVOPE.Rows[j].Cells[3].Text)
                                    //&& (Convert.ToDateTime(TxtF_iniPDVOPE.Text) >= Convert.ToDateTime(GvNewAsignaPDVOPE.Rows[j].Cells[5].Text) && Convert.ToDateTime(TxtF_iniPDVOPE.Text) <= Convert.ToDateTime(GvNewAsignaPDVOPE.Rows[j].Cells[6].Text))
                                    {
                                        this.Session["encabemensa"] = "Sr. Usuario";
                                        this.Session["cssclass"] = "MensajesSupervisor";
                                        this.Session["mensaje"] = "El punto de venta : " + ChkListPDV.Items[i].Text + " ya esta en la lista de nueva asignacion al Mercaderista seleccionado. Por favor verifique";
                                        Mensajes_AsigPDVOPE();
                                        j = GvNewAsignaPDVOPE.Rows.Count - 1;
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
                GvNewAsignaPDVOPE.DataSource = dtAsigna_PDV_A_OPE_Temp;
                GvNewAsignaPDVOPE.DataBind();
                dtAsigna_PDV_A_OPE_Temp = null;
                ChkListPDV.ClearSelection();
            }

            ModalPanelAsignacionPDVaoper.Show();
        }
        protected void BtnaceptaPDVOPE_Click(object sender, EventArgs e)
        {
            ModalPanelAsignacionPDVaoper.Show();
        }
        protected void BtnUpdateAsigPDVOPE_Click(object sender, EventArgs e)
        {
            ModalPanelAsignacionPDVaoper.Show();


            for (int i = 0; i <= GvNewAsignaPDVOPE.Rows.Count - 1; i++)
            {
                EPointOfSale_PlanningOper RegistrarPointOfSale_PlanningOper = PointOfSale_PlanningOper.RegistrarAsignPDVaOperativo(Convert.ToInt32(GvNewAsignaPDVOPE.Rows[i].Cells[3].Text), TxtPlanningAsigPDVOPE.Text, Convert.ToInt32(GvNewAsignaPDVOPE.Rows[i].Cells[1].Text), Convert.ToDateTime(GvNewAsignaPDVOPE.Rows[i].Cells[5].Text + " 01:00:00.000"), Convert.ToDateTime(GvNewAsignaPDVOPE.Rows[i].Cells[6].Text + " 23:59:00.000"),0, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                PointOfSale_PlanningOper RegistrarTBL_EQUIPO_PTO_VENTA = new PointOfSale_PlanningOper();
                RegistrarTBL_EQUIPO_PTO_VENTA.RegistrarTBL_EQUIPO_PTO_VENTA(Convert.ToInt32(GvNewAsignaPDVOPE.Rows[i].Cells[3].Text), TxtPlanningAsigPDVOPE.Text, Convert.ToInt32(GvNewAsignaPDVOPE.Rows[i].Cells[1].Text), Convert.ToDateTime(GvNewAsignaPDVOPE.Rows[i].Cells[5].Text + " 01:00:00.000"), Convert.ToDateTime(GvNewAsignaPDVOPE.Rows[i].Cells[6].Text + " 23:59:00.000"));

            }
            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupConfirm";
            this.Session["mensaje"] = "Se ha asignado con éxito los puntos de venta para la campaña : " + LblTxtPresupuestoAsigPDVOPE.Text.ToUpper();
            Mensajes_AsigPDVOPE();
            Limpiar_InformacionAsignaPDVOPE();

        }
        protected void GvNewAsignaPDVOPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GvNewAsignaPDVOPE.SelectedRow;
            DataTable dtdel = (DataTable)this.Session["dtAsigna_PDV_A_OPE_Temp"];
            dtdel.Rows[row.RowIndex].Delete();
            this.Session["dtAsigna_PDV_A_OPE_Temp"] = dtdel;
            GvNewAsignaPDVOPE.DataSource = (DataTable)this.Session["dtAsigna_PDV_A_OPE_Temp"];
            GvNewAsignaPDVOPE.DataBind();
            dtdel = null;
            ModalPanelAsignacionPDVaoper.Show();

        }
        //protected void GvAsignaPDVOPE_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LblMensajeConfirm.Text = "Realmente desea eliminar el punto de venta " + GvAsignaPDVOPE.SelectedRow.Cells[3].Text + " Para el usuario " + CmbSelOpePlanning.SelectedItem.Text + " ?";
        //    ModalConfirmacion.Show();
        //    ModalPanelAsignacionPDVaoper.Show();
        //}
        protected void BtnNoConfirma_Click(object sender, EventArgs e)
        {
            ModalPanelAsignacionPDVaoper.Show();
        }
        protected void BtnSiConfirma_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= GvAsignaPDVOPE.Rows.Count - 1; i++)
            {
                if (((CheckBox)GvAsignaPDVOPE.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    int prueba2 = Convert.ToInt32(((Label)GvAsignaPDVOPE.Rows[i].FindControl("LblNo")).Text);
                    DataTable dt1 = PointOfSale_PlanningOper.EliminarPuntosVentaXoperativo_TBL_EQUIPO_PTO_VENTA(TxtPlanningAsigPDVOPE.Text, Convert.ToInt32(CmbSelOpePlanning.Text), Convert.ToInt32(((Label)GvAsignaPDVOPE.Rows[i].FindControl("LblNo")).Text));
                    DataTable dt = PointOfSale_PlanningOper.EliminarPuntosVentaXoperativo(Convert.ToInt32(((Label)GvAsignaPDVOPE.Rows[i].FindControl("LblNo")).Text));
                    dt = null;
                    dt1 = null;
                }
            }
            ModalPanelAsignacionPDVaoper.Show();
            ConsultaPDVXoperativo();
            if (GvAsignaPDVOPE.Rows.Count == 0)
            {
                Button1.Visible = false;
                BtnAllAsigPDV.Visible = false;
                BtnNoneasigPDV.Visible = false;
            }

        }
        protected void GvAsignaPDVOPE_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ModalPanelAsignacionPDVaoper.Show();
            GvAsignaPDVOPE.EditIndex = e.NewEditIndex;
            ConsultaPDVXoperativo();
        }
        protected void GvAsignaPDVOPE_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ModalPanelAsignacionPDVaoper.Show();
            GvAsignaPDVOPE.EditIndex = -1;
            ConsultaPDVXoperativo();
        }
        protected void GvAsignaPDVOPE_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            bool seguir = true;
            ModalPanelAsignacionPDVaoper.Show();

            DateTime FechaInicial = Convert.ToDateTime(((TextBox)GvAsignaPDVOPE.Rows[GvAsignaPDVOPE.EditIndex].Cells[0].FindControl("TxtFechaini")).Text);
            DateTime Fechafinal = Convert.ToDateTime(((TextBox)GvAsignaPDVOPE.Rows[GvAsignaPDVOPE.EditIndex].Cells[0].FindControl("TxtFechafin")).Text);

            if (FechaInicial > Fechafinal)
            {
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "La fecha inicial no puede ser mayor a la fecha final";
                Mensajes_AsigPDVOPE();
                seguir = false;
            }

            if (seguir)
            {
                EPointOfSale_PlanningOper ActualizarPointOfSale_PlanningOper = PointOfSale_PlanningOper.ActualizarAsignPDVaOperativo(
                    Convert.ToInt32(((Label)GvAsignaPDVOPE.Rows[GvAsignaPDVOPE.EditIndex].Cells[0].FindControl("LblNo")).Text),
                   Convert.ToDateTime(((TextBox)GvAsignaPDVOPE.Rows[GvAsignaPDVOPE.EditIndex].Cells[0].FindControl("TxtFechaini")).Text + " 07:00:00.000"),
                   Convert.ToDateTime(((TextBox)GvAsignaPDVOPE.Rows[GvAsignaPDVOPE.EditIndex].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000"),
                      Convert.ToString(this.Session["sUser"]),
                      DateTime.Now);

                PointOfSale_PlanningOper ActualizarTBL_EQUIPO_PTO_VENTA = new PointOfSale_PlanningOper();
                ActualizarTBL_EQUIPO_PTO_VENTA.ActualizarTBL_EQUIPO_PTO_VENTA(((Label)GvAsignaPDVOPE.Rows[GvAsignaPDVOPE.EditIndex].Cells[0].FindControl("Label2")).Text,
                    TxtPlanningAsigPDVOPE.Text, Convert.ToInt32(CmbSelOpePlanning.Text),
                     Convert.ToDateTime(((TextBox)GvAsignaPDVOPE.Rows[GvAsignaPDVOPE.EditIndex].Cells[0].FindControl("TxtFechaini")).Text + " 07:00:00.000"),
                   Convert.ToDateTime(((TextBox)GvAsignaPDVOPE.Rows[GvAsignaPDVOPE.EditIndex].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000"));


                GvAsignaPDVOPE.EditIndex = -1;
                ConsultaPDVXoperativo();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            LblMensajeConfirm.Text = "Realmente desea eliminar los puntos de venta seleccionados para el usuario " + CmbSelOpePlanning.SelectedItem.Text + " ?";
            ModalConfirmacion.Show();
            ModalPanelAsignacionPDVaoper.Show();
        }
        protected void BtnCargaPDVOPE_Click(object sender, EventArgs e)
        {
            ModalPanelAsignacionPDVaoper.Show();
            ModalPopupPanelCargaMasivaPDVOPE.Show();
            this.Session["PresupuestoPDVOPE"] = this.Session["Numbudget"].ToString().Trim() + " " + LblTxtPresupuestoAsigPDVOPE.Text;
            this.Session["id_planningPDVOPE"] = TxtPlanningAsigPDVOPE.Text;
            IframeMasivaPDVOpe.Attributes["src"] = "Carga_PDVOPE.aspx";


        }
        protected void BtnAllAsigPDV_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= GvAsignaPDVOPE.Rows.Count - 1; i++)
            {
                ((CheckBox)GvAsignaPDVOPE.Rows[i].FindControl("CheckBox1")).Checked = true;

            }
            ModalPanelAsignacionPDVaoper.Show();

        }
        protected void BtnNoneasigPDV_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= GvAsignaPDVOPE.Rows.Count - 1; i++)
            {
                ((CheckBox)GvAsignaPDVOPE.Rows[i].FindControl("CheckBox1")).Checked = false;

            }
            ModalPanelAsignacionPDVaoper.Show();

        }
        #endregion

        #region Levantamiento de informacion
        protected void ImgIrAProductos_Click(object sender, ImageClickEventArgs e)
        {
            botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();
            ModalPanelProductos.Show();
            ConsultaProductosCampaña();
            llenacompetidores();
            llenainformesProd();

            llenaLevantamiento_Categoria();
            llenaLevantamiento_Marca();
        }
        void llenaLevantamiento_Categoria()
        {
           int company_id = Convert.ToInt32(this.Session["company_id"].ToString().Trim());
            DataTable dtcatego = PPlanning.Get_ObtenerCategoriasPlanning(company_id);

            ddlLevantamiento_Categoria.DataSource = dtcatego;
            ddlLevantamiento_Categoria.DataValueField = "id_ProductCategory";
            ddlLevantamiento_Categoria.DataTextField = "Product_Category";
            ddlLevantamiento_Categoria.DataBind();

        }

        protected void ddlLevantamiento_Marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsProductos = cn.ejecutarDataSet("UP_WEBXPLORA_PLA_OBTENERPRODUCTOSPLANNING_FILTROSXCATEGORIA_MARCA", TxtPlanningAsigProd.Text, Convert.ToInt32(this.Session["company_id"].ToString().Trim()), ddlLevantamiento_Categoria.SelectedValue, ddlLevantamiento_Marca.SelectedValue);

            gvproductospropios.DataSource = dsProductos.Tables[0];
            gvproductospropios.DataBind();
            gv_stockToExcel.DataSource = dsProductos.Tables[0];
            gv_stockToExcel.DataBind();
            gvproductospropiosDEL.DataSource = dsProductos.Tables[0];
            gvproductospropiosDEL.DataBind();

            Gvproductoscompetidor.DataSource = dsProductos.Tables[1];
            Gvproductoscompetidor.DataBind();
            gv_stockToExcel2.DataSource = dsProductos.Tables[1];
            gv_stockToExcel2.DataBind();
            GvproductoscompetidorDEL.DataSource = dsProductos.Tables[1];
            GvproductoscompetidorDEL.DataBind();

            gvmarcaspropias.DataSource = dsProductos.Tables[2];
            gvmarcaspropias.DataBind();

            gv_stockToExcel3.DataSource = dsProductos.Tables[2];
            gv_stockToExcel3.DataBind();

            gvmarcaspropiasDEL.DataSource = dsProductos.Tables[2];
            gvmarcaspropiasDEL.DataBind();


            GvMarcascompetidor.DataSource = dsProductos.Tables[3];
            GvMarcascompetidor.DataBind();
            gv_stockToExcel4.DataSource = dsProductos.Tables[3];
            gv_stockToExcel4.DataBind();
            GvmarcascompetidorDEL.DataSource = dsProductos.Tables[3];
            GvmarcascompetidorDEL.DataBind();

            gvFamiliaspropias.DataSource = dsProductos.Tables[4];
            gvFamiliaspropias.DataBind();
            gv_stockToExcel5.DataSource = dsProductos.Tables[4];
            gv_stockToExcel5.DataBind();
            gvFamiliaspropiasDEL.DataSource = dsProductos.Tables[4];
            gvFamiliaspropiasDEL.DataBind();

            GvFamiliascompetidor.DataSource = dsProductos.Tables[5];
            GvFamiliascompetidor.DataBind();
            GvFamiliascompetidorDEL.DataSource = dsProductos.Tables[5];
            GvFamiliascompetidorDEL.DataBind();

            gvcategoriaspropias.DataSource = dsProductos.Tables[6];
            gvcategoriaspropias.DataBind();

            gv_stockToExcel6.DataSource = dsProductos.Tables[6];
            gv_stockToExcel6.DataBind();

            gvcategoriaspropiasDEL.DataSource = dsProductos.Tables[6];
            gvcategoriaspropiasDEL.DataBind();

            GvCategoriascompetidor.DataSource = dsProductos.Tables[7];
            GvCategoriascompetidor.DataBind();
            GvCategoriascompetidorDEL.DataSource = dsProductos.Tables[7];
            GvCategoriascompetidorDEL.DataBind();

            gvMatPOP.DataSource = dsProductos.Tables[8];
            gvMatPOP.DataBind();


            gvObservaciones.DataSource= dsProductos.Tables[9];
            gvObservaciones.DataBind();


            dsProductos = null;

            ModalPanelProductos.Show();
        }
        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                gv_stockToExcel.Visible = true;
                gv_stockToExcel2.Visible = true;
                gv_stockToExcel3.Visible = true;
                gv_stockToExcel4.Visible = true;
                gv_stockToExcel5.Visible = true;
                gv_stockToExcel6.Visible = true;

                GridViewExportUtil.ExportToExcelMethodsix("Informe", this.gv_stockToExcel, this.gv_stockToExcel2, this.gv_stockToExcel3, this.gv_stockToExcel4, this.gv_stockToExcel5, this.gv_stockToExcel6);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void llenaLevantamiento_Categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaLevantamiento_Marca();

            DataSet dsProductos = cn.ejecutarDataSet("UP_WEBXPLORA_PLA_OBTENERPRODUCTOSPLANNING_FILTROSXCATEGORIA_MARCA", TxtPlanningAsigProd.Text, Convert.ToInt32(this.Session["company_id"].ToString().Trim()), ddlLevantamiento_Categoria.SelectedValue, ddlLevantamiento_Marca.SelectedValue);

            gv_stockToExcel.DataSource = dsProductos.Tables[0];
            gv_stockToExcel.DataBind();
            gv_stockToExcel2.DataSource = dsProductos.Tables[1];
            gv_stockToExcel2.DataBind();
            gv_stockToExcel3.DataSource = dsProductos.Tables[2];
            gv_stockToExcel3.DataBind();
            gv_stockToExcel4.DataSource = dsProductos.Tables[3];
            gv_stockToExcel4.DataBind();
            gv_stockToExcel5.DataSource = dsProductos.Tables[4];
            gv_stockToExcel5.DataBind();
            gv_stockToExcel6.DataSource = dsProductos.Tables[6];
            gv_stockToExcel6.DataBind();



            gvproductospropios.DataSource = dsProductos.Tables[0];
            gvproductospropios.DataBind();
            gvproductospropiosDEL.DataSource = dsProductos.Tables[0];
            gvproductospropiosDEL.DataBind();

            Gvproductoscompetidor.DataSource = dsProductos.Tables[1];
            Gvproductoscompetidor.DataBind();
            GvproductoscompetidorDEL.DataSource = dsProductos.Tables[1];
            GvproductoscompetidorDEL.DataBind();

            gvmarcaspropias.DataSource = dsProductos.Tables[2];
            gvmarcaspropias.DataBind();
            gvmarcaspropiasDEL.DataSource = dsProductos.Tables[2];
            gvmarcaspropiasDEL.DataBind();

            GvMarcascompetidor.DataSource = dsProductos.Tables[3];
            GvMarcascompetidor.DataBind();
            GvmarcascompetidorDEL.DataSource = dsProductos.Tables[3];
            GvmarcascompetidorDEL.DataBind();

            gvFamiliaspropias.DataSource = dsProductos.Tables[4];
            gvFamiliaspropias.DataBind();
            gvFamiliaspropiasDEL.DataSource = dsProductos.Tables[4];
            gvFamiliaspropiasDEL.DataBind();

            GvFamiliascompetidor.DataSource = dsProductos.Tables[5];
            GvFamiliascompetidor.DataBind();
            GvFamiliascompetidorDEL.DataSource = dsProductos.Tables[5];
            GvFamiliascompetidorDEL.DataBind();

            gvcategoriaspropias.DataSource = dsProductos.Tables[6];
            gvcategoriaspropias.DataBind();
            gvcategoriaspropiasDEL.DataSource = dsProductos.Tables[6];
            gvcategoriaspropiasDEL.DataBind();

            GvCategoriascompetidor.DataSource = dsProductos.Tables[7];
            GvCategoriascompetidor.DataBind();
            GvCategoriascompetidorDEL.DataSource = dsProductos.Tables[7];
            GvCategoriascompetidorDEL.DataBind();

            gvMatPOP.DataSource = dsProductos.Tables[8];
            gvMatPOP.DataBind();


            gvObservaciones.DataSource = dsProductos.Tables[9];
            gvObservaciones.DataBind();

            dsProductos = null;

            ModalPanelProductos.Show();
        }

        void llenaLevantamiento_Marca()
        {
            int company_id = Convert.ToInt32(this.Session["company_id"].ToString().Trim());
            DataTable dtmarca = PPlanning.Get_ObtenerMarcas(Convert.ToInt32(ddlLevantamiento_Categoria.SelectedValue), company_id);

            ddlLevantamiento_Marca.DataSource = dtmarca;
            ddlLevantamiento_Marca.DataValueField = "id_Brand";
            ddlLevantamiento_Marca.DataTextField = "Name_Brand";
            ddlLevantamiento_Marca.DataBind();
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
            ModalPanelAsignaProductos.Show();
        }
        protected void BtnProdCompe_Click(object sender, EventArgs e)
        {
            if (CmbCompetidores.Items.Count == 1)
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Actualmente el cliente no tiene ningun competidor asignado. Por favor solicite esta información al administrador de Xplora";
                Mensajes_Productos();
            }
            else
            {
                this.Session["TipoProducto"] = "C";
                StiloBotonTipoProdSeleccionado();
                BtnProdCompe.CssClass = "buttonNewPlanSel";
                HabilitabotonesTipoProd();
                BtnProdCompe.Enabled = false;
                BtnSaveProd.Enabled = false;
                CmbCompetidores.Visible = true;
                LblSelCompetidor.Visible = true;
                CmbCompetidores.Text = "0";
                limpiarlistas();
                //level_carga = 0;
                //cargarlistas();
                ModalPanelAsignaProductos.Show();
            }
        }

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
            ModalPanelAsignaProductos.Show();
        }

        private void showpanels()
        {
            //bool[] s_vistas = new bool[6];
            bool[] s_vistas = (bool[])(this.Session["s_vistas"]);

            categorias.Visible = s_vistas[0];
            Marcas.Visible = s_vistas[1];
            submarcas.Visible = s_vistas[2];
            Familias.Visible = s_vistas[3];
            SubFamilias.Visible = s_vistas[4];
            productos.Visible = s_vistas[5];
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
            if (s_vistas[0] == true && level_carga == 0)//Si categorias esta habilitado
            {
                DataTable dtcatego = PPlanning.Get_ObtenerCategoriasPlanning(company_id);
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
            if (s_vistas[1] == true)
            {
                if (level_carga > 5 || level_carga == 0)//Si marcas esta habilitado
                {
                    DataTable dtmarca = PPlanning.Get_ObtenerMarcas(id_categoria, company_id);

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

            if (s_vistas[2] == true)
            {

                if (level_carga > 4 || level_carga == 0)//Si submarcas esta habilitado
                {
                    int id_marca = 0;

                    if (rblmarca.Items.Count > 0 && rblmarca.SelectedIndex != -1)// si existen marcas y se seleccionó alguna
                        id_marca = Convert.ToInt32(rblmarca.SelectedValue);

                    DataTable dtsubmarca = PPlanning.Get_ObtenerSubMarcas(id_categoria.ToString(), id_marca, company_id);
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
            if (s_vistas[3] == true)
            {

                if (level_carga > 3 || level_carga == 0)//si familias está habilitado
                {
                    int id_marca = 0;
                    int id_submarca = 0;

                    if (rblmarca.Items.Count != 0 && rblmarca.SelectedIndex != -1)//si existen marcas y se ha seleccionado alguna
                        id_marca = Convert.ToInt32(rblmarca.SelectedValue);
                    if (rblsubmarca.Items.Count != 0 && rblsubmarca.SelectedIndex != -1)//si existen submarcas y se ha seleccionado alguna
                        id_submarca = Convert.ToInt32(rblsubmarca.SelectedValue);

                    DataTable dtFamilias = PPlanning.Get_Obtener_Familias(id_marca, id_submarca, company_id);

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
                                Mensajes_Productos();
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
                                Mensajes_Productos();
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

                    DataTable dtSubFamilias = PPlanning.Get_Obtener_SubFamilias(id_familia, company_id);

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
                                Mensajes_Productos();
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
                                Mensajes_Productos();
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
                    dtproducto = PPlanning.Get_Obtener_Productos(id_categoria.ToString(), id_marca.ToString(), id_familia, id_subfamilia, company_id);


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
                        Mensajes_Productos();
                    }
                }
                dtproducto = null;
            }
            #endregion

            level_carga = 0;
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
                ModalPanelAsignaProductos.Show();
            }
            catch
            {
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
                ModalPanelAsignaProductos.Show();
            }
            catch
            {
                PAdmin.Get_Delete_Sesion_User(usersession.Text);
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
                ModalPanelAsignaProductos.Show();
            }
            catch
            {
                PAdmin.Get_Delete_Sesion_User(usersession.Text);
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
                ModalPanelAsignaProductos.Show();
            }
            catch (Exception ex)
            {
                PAdmin.Get_Delete_Sesion_User(usersession.Text);
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
                ModalPanelAsignaProductos.Show();
            }
            catch (Exception ex)
            {
                PAdmin.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        protected void ImgCloseAddProd_Click(object sender, ImageClickEventArgs e)
        {
            StiloBotonTipoProdSeleccionado();
            BtnProdPropio.Enabled = true;
            BtnProdCompe.Enabled = true;
            BtnSaveProd.Enabled = false;
            //submarcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
            CmbCompetidores.Visible = false;
            LblSelCompetidor.Visible = false;
            CmbCompetidores.Text = "0";
            limpiarlistas();
            ModalPanelProductos.Show();
            ModalPanelAsignaProductos.Hide();
            llenainformesProd();
        }
        protected void BtnSaveProd_Click(object sender, EventArgs e)
        {
            Boolean continuar = datoscompletosProductos();
            DAplicacion Dduplicidad = new DAplicacion();
            string sTIPO_LEVANTAMIENTO;

            #region Codigo backup de levantamiento
            //if (continuar)
            //{
            //    bool sigue = true;
            //    sTIPO_LEVANTAMIENTO = this.Session["vista_final"].ToString().Trim();
            //    #region Levantamiento por Producto
            //    if (sTIPO_LEVANTAMIENTO.Equals("Producto"))
            //    {
            //        for (int i = 0; i <= ChkProductos.Items.Count - 1; i++)
            //        {
            //            if (ChkProductos.Items[i].Selected == true)
            //            {
            //                DataTable dtconsulta = Dduplicidad.ConsultaDuplicados(ConfigurationManager.AppSettings["Product_planning"], TxtPlanningAsigProd.Text, ChkProductos.Items[i].Value, null);
            //                if (dtconsulta != null)
            //                {
            //                    this.Session["encabemensa"] = "Sr. Usuario";
            //                    this.Session["cssclass"] = "MensajesSupervisor";
            //                    this.Session["mensaje"] = "El producto " + ChkProductos.Items[i].Text + " Ya exite para esta Campaña";
            //                    Mensajes_Productos();
            //                    sigue = false;
            //                    i = ChkProductos.Items.Count - 1;
            //                }
            //            }
            //        }

            //        if (sigue)
            //        {
            //            for (int i = 0; i <= ChkProductos.Items.Count - 1; i++)
            //            {
            //                if (ChkProductos.Items[i].Selected == true)
            //                {
            //                    DataTable dtbrandysubbrand = PPlanning.Get_Obtener_BrandySubbrandxProducto(Convert.ToInt32(ChkProductos.Items[i].Value));
            //                    if (dtbrandysubbrand != null)
            //                    {
            //                        if (dtbrandysubbrand.Rows.Count > 0)
            //                        {
            //                            this.Session["brand"] = dtbrandysubbrand.Rows[0]["id_Brand"].ToString().Trim();
            //                            this.Session["sub_brand"] = dtbrandysubbrand.Rows[0]["id_SubBrand"].ToString().Trim();
            //                        }
            //                    }
            //                    DPlanning PermitirGuardar = new DPlanning();
            //                    DataTable dtpermitir = PermitirGuardar.Permitir_GuardarLevantamiento(sTIPO_LEVANTAMIENTO, Convert.ToInt64(ChkProductos.Items[i].Value), rbliscatego.SelectedItem.Value, Convert.ToInt32(this.Session["brand"].ToString().Trim()), null);
            //                    if (dtpermitir != null)
            //                    {
            //                        if (dtpermitir.Rows[0]["CONTINUAR"].ToString().Trim() == "CONTINUAR")
            //                        {
            //                            sigue = true;
            //                        }
            //                        else
            //                        {
            //                            this.Session["encabemensa"] = "Sr. Usuario";
            //                            this.Session["cssclass"] = "MensajesSupervisor";
            //                            this.Session["mensaje"] = "El producto " + ChkProductos.Items[i].Text + " No se permite almacenar por falta de información en Categorías , Marcas y Productos. Consulte con el Administrador Xplora";
            //                            Mensajes_Productos();
            //                            sigue = false;
            //                            i = ChkProductos.Items.Count - 1;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        if (sigue)
            //        {
            //            //Ejecutar Método para almacenar los productos seleccionados para el planning. Ing. Mauricio Ortiz  
            //            for (int i = 0; i <= ChkProductos.Items.Count - 1; i++)
            //            {
            //                if (ChkProductos.Items[i].Selected == true)
            //                {
            //                    //ejecutar metodo para consultar datos de producto 
            //                    DataTable dtbrandysubbrand = PPlanning.Get_Obtener_BrandySubbrandxProducto(Convert.ToInt32(ChkProductos.Items[i].Value));
            //                    if (dtbrandysubbrand != null)
            //                    {
            //                        if (dtbrandysubbrand.Rows.Count > 0)
            //                        {
            //                            this.Session["brand"] = dtbrandysubbrand.Rows[0]["id_Brand"].ToString().Trim();
            //                            this.Session["sub_brand"] = dtbrandysubbrand.Rows[0]["id_SubBrand"].ToString().Trim();
            //                        }
            //                    }

            //                    DataTable dtconsulta = Dduplicidad.ConsultaDuplicados(ConfigurationManager.AppSettings["Product_planning"], TxtPlanningAsigProd.Text, ChkProductos.Items[i].Value, null);
            //                    if (dtconsulta == null)
            //                    {
            //                        PPlanning.Get_Regitration_ProductosPlanning(TxtPlanningAsigProd.Text, Convert.ToInt64(ChkProductos.Items[i].Value), rbliscatego.SelectedItem.Value, Convert.ToInt32(this.Session["brand"].ToString().Trim()), this.Session["sub_brand"].ToString().Trim(), "", "", 0, Convert.ToInt32(RbtnListInfProd.SelectedItem.Value), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
            //                        DPlanning Dplanning = new DPlanning();

            //                        string sRbtMasopciones;
            //                        try
            //                        {
            //                            sRbtMasopciones = RbtMasopciones.SelectedItem.Text;
            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            sRbtMasopciones = "";
            //                        }

            //                        //Dplanning.Registrar_TBL_EQUIPO_PRODUCTOS(this.Session["TipoProducto"].ToString().Trim(), sRbtMasopciones);

            //                        try
            //                        {
            //                            DataTable dtRegsitrarTBLPROD = Dplanning.Registrar_TBL_PRODUCTO_CADENA("1", this.Session["TipoProducto"].ToString().Trim(),
            //                                   Convert.ToInt64(ChkProductos.Items[i].Value), TxtPlanningAsigProd.Text, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0");

            //                            if (dtRegsitrarTBLPROD != null)
            //                            {
            //                                if (dtRegsitrarTBLPROD.Rows.Count > 0)
            //                                {
            //                                    for (int ir = 0; ir <= dtRegsitrarTBLPROD.Rows.Count - 1; ir++)
            //                                    {
            //                                        try
            //                                        {
            //                                            DataTable dtRegsitrarTBLPRODUCTOS = Dplanning.Registrar_TBL_PRODUCTO_CADENA("2", this.Session["TipoProducto"].ToString().Trim(),
            //                                                                                       Convert.ToInt64(ChkProductos.Items[i].Value), TxtPlanningAsigProd.Text,
            //                                                                                       dtRegsitrarTBLPROD.Rows[ir][0].ToString().Trim(),
            //                                             dtRegsitrarTBLPROD.Rows[ir][1].ToString().Trim(),
            //                                             dtRegsitrarTBLPROD.Rows[ir][2].ToString().Trim(),
            //                                             dtRegsitrarTBLPROD.Rows[ir][3].ToString().Trim(),
            //                                             dtRegsitrarTBLPROD.Rows[ir][4].ToString().Trim(),
            //                                             dtRegsitrarTBLPROD.Rows[ir][5].ToString().Trim(),
            //                                             dtRegsitrarTBLPROD.Rows[ir][6].ToString().Trim(),
            //                                             dtRegsitrarTBLPROD.Rows[ir][7].ToString().Trim(),
            //                                             dtRegsitrarTBLPROD.Rows[ir][8].ToString().Trim(),
            //                                             dtRegsitrarTBLPROD.Rows[ir][9].ToString().Trim());
            //                                            dtRegsitrarTBLPRODUCTOS = null;
            //                                        }
            //                                        catch (Exception ex)
            //                                        {
            //                                            // no inserta en tbl_producto_cadena nada porq ya existe en esa tabla
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                            dtRegsitrarTBLPROD = null;
            //                        }
            //                        catch
            //                        {

            //                        }

            //                        this.Session["encabemensa"] = "Sr. Usuario";
            //                        this.Session["cssclass"] = "MensajesSupConfirm";
            //                        this.Session["mensaje"] = "Se ha creado con éxito los productos para la campaña : " + LblTxtPresupuestoAsigProd.Text.ToUpper();
            //                        Mensajes_Productos();
            //                    }
            //                    dtconsulta = null;
            //                    dtbrandysubbrand = null;
            //                }
            //            }

            //            if (CmbCompetidores.Text != "0")
            //            {
            //                LlenaCategoriasCompe();
            //            }
            //            else
            //            {
            //                LlenaCategorias();
            //            }
            //            BtnSaveProd.Enabled = false;
            //            Chklistmarca.Items.Clear();
            //            rblmarca.Items.Clear();
            //            Chklistmarca.Items.Clear();
            //            rblsubmarca.Items.Clear();
            //            ChkListFamilias.Items.Clear();
            //            ChkProductos.Items.Clear();
            //        }
            //    } 
            //    #endregion
            //    else
            //    {
            //        #region Levantamiento por Marca
            //        if (sTIPO_LEVANTAMIENTO == "Marca")
            //        {
            //            for (int i = 0; i <= Chklistmarca.Items.Count - 1; i++)
            //            {
            //                if (Chklistmarca.Items[i].Selected == true)
            //                {
            //                    DataTable dtconsulta = Dduplicidad.ConsultaDuplicados(ConfigurationManager.AppSettings["PLA_Brand_Planning"], TxtPlanningAsigProd.Text, Chklistmarca.Items[i].Value, null);
            //                    if (dtconsulta != null)
            //                    {
            //                        this.Session["encabemensa"] = "Sr. Usuario";
            //                        this.Session["cssclass"] = "MensajesSupervisor";
            //                        this.Session["mensaje"] = "La marca  " + Chklistmarca.Items[i].Text + " Ya exite para esta Campaña";
            //                        Mensajes_Productos();
            //                        sigue = false;
            //                        i = Chklistmarca.Items.Count - 1;
            //                    }
            //                }
            //            }

            //            if (sigue)
            //            {
            //                for (int i = 0; i <= Chklistmarca.Items.Count - 1; i++)
            //                {
            //                    if (Chklistmarca.Items[i].Selected == true)
            //                    {
            //                        DPlanning PermitirGuardar = new DPlanning();
            //                        DataTable dtpermitir = PermitirGuardar.Permitir_GuardarLevantamiento(sTIPO_LEVANTAMIENTO, 0, rbliscatego.SelectedItem.Value, Convert.ToInt32(Chklistmarca.Items[i].Value), null);
            //                        if (dtpermitir != null)
            //                        {
            //                            if (dtpermitir.Rows[0]["CONTINUAR"].ToString().Trim() == "CONTINUAR")
            //                            {
            //                                sigue = true;
            //                            }
            //                            else
            //                            {
            //                                this.Session["encabemensa"] = "Sr. Usuario";
            //                                this.Session["cssclass"] = "MensajesSupervisor";
            //                                this.Session["mensaje"] = "La Marca " + Chklistmarca.Items[i].Text + " No se permite almacenar por falta de información en Categorías , Marcas. Consulte con el Administrador Xplora";
            //                                Mensajes_Productos();
            //                                sigue = false;
            //                                i = Chklistmarca.Items.Count - 1;

            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            if (sigue)
            //            {
            //                //Ejecutar Método para almacenar las marcas seleccionados para el planning. Ing. Mauricio Ortiz  
            //                for (int i = 0; i <= Chklistmarca.Items.Count - 1; i++)
            //                {
            //                    if (Chklistmarca.Items[i].Selected == true)
            //                    {

            //                        DataTable dtconsulta = Dduplicidad.ConsultaDuplicados(ConfigurationManager.AppSettings["PLA_Brand_Planning"], TxtPlanningAsigProd.Text, Chklistmarca.Items[i].Value, null);
            //                        if (dtconsulta == null)
            //                        {
            //                            PPlanning.Get_Registrar_MarcasPlanning(TxtPlanningAsigProd.Text, rbliscatego.SelectedItem.Value, Convert.ToInt32(Chklistmarca.Items[i].Value), Convert.ToInt32(RbtnListInfProd.SelectedItem.Value), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
            //                            DPlanning Dplanning = new DPlanning();
            //                            try
            //                            {
            //                                //Dplanning.Registrar_TBL_EQUIPO_MARCAS();
            //                            }
            //                            catch (Exception ex)
            //                            {
            //                            }

            //                            this.Session["encabemensa"] = "Sr. Usuario";
            //                            this.Session["cssclass"] = "MensajesSupConfirm";
            //                            this.Session["mensaje"] = "Se ha creado con éxito las marcas para la campaña : " + LblTxtPresupuestoAsigProd.Text.ToUpper();
            //                            Mensajes_Productos();
            //                        }
            //                        dtconsulta = null;
            //                    }
            //                }
            //                if (CmbCompetidores.Text != "0")
            //                {
            //                    LlenaCategoriasCompe();
            //                }
            //                else
            //                {
            //                    LlenaCategorias();
            //                }
            //                BtnSaveProd.Enabled = false;
            //                Chklistmarca.Items.Clear();
            //                rblmarca.Items.Clear();
            //                Chklistmarca.Items.Clear();
            //                rblsubmarca.Items.Clear();
            //                ChkListFamilias.Items.Clear();
            //                ChkProductos.Items.Clear();
            //            }
            //        } 
            //        #endregion
            //        else
            //        {
            //            #region Levantamiento por Familia
            //            if (sTIPO_LEVANTAMIENTO == "Familia")
            //            {
            //                for (int i = 0; i <= ChkListFamilias.Items.Count - 1; i++)
            //                {
            //                    if (ChkListFamilias.Items[i].Selected == true)
            //                    {
            //                        DataTable dtconsulta = Dduplicidad.ConsultaDuplicados(ConfigurationManager.AppSettings["PLA_Family_Planning"], TxtPlanningAsigProd.Text, ChkListFamilias.Items[i].Value, RbtnListInfProd.SelectedItem.Value);
            //                        if (dtconsulta != null)
            //                        {
            //                            this.Session["encabemensa"] = "Sr. Usuario";
            //                            this.Session["cssclass"] = "MensajesSupervisor";
            //                            this.Session["mensaje"] = "La Familia  " + ChkListFamilias.Items[i].Text + " Ya exite para esta Campaña";
            //                            Mensajes_Productos();
            //                            sigue = false;
            //                            i = ChkListFamilias.Items.Count - 1;
            //                        }
            //                    }
            //                }

            //                if (sigue)
            //                {
            //                    for (int i = 0; i <= ChkListFamilias.Items.Count - 1; i++)
            //                    {
            //                        if (ChkListFamilias.Items[i].Selected == true)
            //                        {
            //                            DPlanning PermitirGuardar = new DPlanning();
            //                            DataTable dtpermitir = PermitirGuardar.Permitir_GuardarLevantamiento(sTIPO_LEVANTAMIENTO, 0, rbliscatego.SelectedItem.Value, 0, ChkListFamilias.Items[i].Value);
            //                            if (dtpermitir != null)
            //                            {
            //                                if (dtpermitir.Rows[0]["CONTINUAR"].ToString().Trim() == "CONTINUAR")
            //                                {
            //                                    sigue = true;
            //                                }
            //                                else
            //                                {
            //                                    this.Session["encabemensa"] = "Sr. Usuario";
            //                                    this.Session["cssclass"] = "MensajesSupervisor";
            //                                    this.Session["mensaje"] = "La familia " + ChkListFamilias.Items[i].Text + " No se permite almacenar por falta de información en Categorías , Marcas y Familias . Consulte con el Administrador Xplora";
            //                                    Mensajes_Productos();
            //                                    sigue = false;
            //                                    i = ChkListFamilias.Items.Count - 1;

            //                                }
            //                            }
            //                        }
            //                    }
            //                }

            //                if (sigue)
            //                {
            //                    //Ejecutar Método para almacenar las familias seleccionadas para el planning. Ing. Mauricio Ortiz  
            //                    for (int i = 0; i <= ChkListFamilias.Items.Count - 1; i++)
            //                    {
            //                        if (ChkListFamilias.Items[i].Selected == true)
            //                        {

            //                            DataTable dtconsulta = Dduplicidad.ConsultaDuplicados(ConfigurationManager.AppSettings["PLA_Family_Planning"], TxtPlanningAsigProd.Text, ChkListFamilias.Items[i].Value, RbtnListInfProd.SelectedItem.Value);
            //                            if (dtconsulta == null)
            //                            {
            //                                PPlanning.Get_Registrar_FamiliasPlanning(TxtPlanningAsigProd.Text, rbliscatego.SelectedItem.Value, Convert.ToInt32(rblmarca.SelectedValue), ChkListFamilias.Items[i].Value, Convert.ToInt32(RbtnListInfProd.SelectedItem.Value), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
            //                                DPlanning Dplanning = new DPlanning();
            //                                try
            //                                {
            //                                    DataTable dtRegsitrarTBL_EQUIPO_FAMILIAS = null;// Dplanning.Registrar_TBL_EQUIPO_FAMILIAS();
            //                                }
            //                                catch (Exception ex)
            //                                {

            //                                }

            //                                this.Session["encabemensa"] = "Sr. Usuario";
            //                                this.Session["cssclass"] = "MensajesSupConfirm";
            //                                this.Session["mensaje"] = "Se ha creado con éxito las familias para la campaña : " + LblTxtPresupuestoAsigProd.Text.ToUpper();
            //                                Mensajes_Productos();
            //                            }
            //                        }
            //                    }
            //                    if (CmbCompetidores.Text != "0")
            //                    {
            //                        LlenaCategoriasCompe();
            //                    }
            //                    else
            //                    {
            //                        LlenaCategorias();
            //                    }
            //                    BtnSaveProd.Enabled = false;
            //                    Chklistmarca.Items.Clear();
            //                    rblmarca.Items.Clear();
            //                    Chklistmarca.Items.Clear();
            //                    rblsubmarca.Items.Clear();
            //                    ChkListFamilias.Items.Clear();
            //                    ChkProductos.Items.Clear();
            //                }
            //            } 
            //            #endregion
            //            else
            //            {
            //                #region Levantamiento por Categoria
            //                if (sTIPO_LEVANTAMIENTO == "Categoria")
            //                {
            //                    for (int i = 0; i <= Chklistcatego.Items.Count - 1; i++)
            //                    {
            //                        if (Chklistcatego.Items[i].Selected == true)
            //                        {
            //                            DataTable dtconsulta = Dduplicidad.DuplicadosCategoriasPlanning(ConfigurationManager.AppSettings["PLA_Category_Planning"], TxtPlanningAsigProd.Text, Chklistcatego.Items[i].Value, RbtnListInfProd.SelectedItem.Value);
            //                            if (dtconsulta != null)
            //                            {
            //                                this.Session["encabemensa"] = "Sr. Usuario";
            //                                this.Session["cssclass"] = "MensajesSupervisor";
            //                                this.Session["mensaje"] = "La Categoría  " + Chklistcatego.Items[i].Text + " Ya exite para esta Campaña";
            //                                Mensajes_Productos();
            //                                sigue = false;
            //                                i = Chklistcatego.Items.Count - 1;
            //                            }
            //                        }
            //                    }

            //                    if (sigue)
            //                    {
            //                        for (int i = 0; i <= Chklistcatego.Items.Count - 1; i++)
            //                        {
            //                            if (Chklistcatego.Items[i].Selected == true)
            //                            {
            //                                DPlanning PermitirGuardar = new DPlanning();
            //                                DataTable dtpermitir = PermitirGuardar.Permitir_GuardarLevantamiento(sTIPO_LEVANTAMIENTO, 0, Chklistcatego.Items[i].Value, 0, null);
            //                                if (dtpermitir != null)
            //                                {
            //                                    if (dtpermitir.Rows[0]["CONTINUAR"].ToString().Trim() == "CONTINUAR")
            //                                    {
            //                                        sigue = true;
            //                                    }
            //                                    else
            //                                    {
            //                                        this.Session["encabemensa"] = "Sr. Usuario";
            //                                        this.Session["cssclass"] = "MensajesSupervisor";
            //                                        this.Session["mensaje"] = "La Categoría " + Chklistcatego.Items[i].Text + " No se permite almacenar por falta de información en Categorías. Consulte con el Administrador Xplora";
            //                                        Mensajes_Productos();
            //                                        sigue = false;
            //                                        i = Chklistcatego.Items.Count - 1;
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }

            //                    if (sigue)
            //                    {
            //                        //Ejecutar Método para almacenar las categorias seleccionadas para el planning. Ing. Mauricio Ortiz  
            //                        for (int i = 0; i <= Chklistcatego.Items.Count - 1; i++)
            //                        {
            //                            if (Chklistcatego.Items[i].Selected == true)
            //                            {
            //                                DataTable dtconsulta = Dduplicidad.DuplicadosCategoriasPlanning(ConfigurationManager.AppSettings["PLA_Category_Planning"], TxtPlanningAsigProd.Text, Chklistcatego.Items[i].Value, RbtnListInfProd.SelectedItem.Value);
            //                                if (dtconsulta == null)
            //                                {
            //                                    DPlanning dregistrarCateg = new DPlanning();
            //                                    DataTable dtRegistrarCategorias = dregistrarCateg.Crear_CategoriasPlanning(TxtPlanningAsigProd.Text,
            //                                      Chklistcatego.Items[i].Value, Convert.ToInt32(RbtnListInfProd.SelectedItem.Value), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);
            //                                    try
            //                                    {
            //                                        DataTable dtRegistrarTBL_EQUIPO_CATEGORIAS = null;// dregistrarCateg.Registrar_TBL_EQUIPO_CATEGORIAS();
            //                                    }
            //                                    catch (Exception ex)
            //                                    {

            //                                    }

            //                                    this.Session["encabemensa"] = "Sr. Usuario";
            //                                    this.Session["cssclass"] = "MensajesSupConfirm";
            //                                    this.Session["mensaje"] = "Se ha creado con éxito las categorías para la campaña : " + LblTxtPresupuestoAsigProd.Text.ToUpper();
            //                                    Mensajes_Productos();
            //                                }
            //                            }
            //                        }
            //                        if (CmbCompetidores.Text != "0")
            //                        {
            //                            LlenaCategoriasCompe();
            //                        }
            //                        else
            //                        {
            //                            LlenaCategorias();
            //                        }
            //                        Chklistmarca.Items.Clear();
            //                        rblmarca.Items.Clear();
            //                        rblsubmarca.Items.Clear();
            //                        ChkListFamilias.Items.Clear();
            //                        ChkProductos.Items.Clear();
            //                    }
            //                } 
            //                #endregion
            //                else
            //                {
            //                    this.Session["encabemensa"] = "Señor Usuario";
            //                    this.Session["cssclass"] = "MensajesSupervisor";
            //                    this.Session["mensaje"] = "Este reporte seleccionado no tiene disponible esta funcionalidad. Consulte con el Administrador Xplora";
            //                    Mensajes_Productos();
            //                }
            //            }
            //        }
            //    }
            //} 
            #endregion
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
                    foreach (ListItem producto in listaproductos)
                    {
                        DataTable dtconsulta = cn.ejecutarDataTable("PLA_CONSULTA_PRODUCT_PLANNING", producto.Value, TxtPlanningAsigProd.Text, RbtnListInfProd.SelectedItem.Value);
                        if (dtconsulta.Rows.Count != 0)
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "El producto " + producto.Text + ", ya existe para esta Campaña";
                            Mensajes_Productos();
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
                                Mensajes_Productos();
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
                                int idMarca = Convert.ToInt32(dt.Rows[0]["id_Brand"].ToString().Trim());
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
                        this.Session["mensaje"] = "Se ha creado con éxito los productos para la campaña: " + LblTxtPresupuestoAsigProd.Text.ToUpper();
                        Mensajes_Productos();


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
                                Mensajes_SeguimientoValidacionVistas();
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
                                        Mensajes_SeguimientoValidacionVistas();
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
                                        string id_brand_pla = dt_marcas.Rows[0]["ID"].ToString();
                                        string categoria = id_ProductCategory;
                                        string id_eq_cat = dt_marcas.Rows[0]["id_CategoryPlanning"].ToString(); ;
                                        Dplanning.Registrar_TBL_EQUIPO_MARCAS(id_brand_pla, id_planning, id_ProductCategory, categoria, id_eq_cat, id_Brand.ToString(), Report_Id.ToString(), "1");

                                        Dplanning.RegistrarTBL_MARCA_COMPETIDORA(this.Session["company_id"].ToString().Trim(), CmbCompetidores.SelectedValue, id_brand_pla, "1");
                                        



                                    }
                                    catch (Exception ex)
                                    {
                                    }

                                    this.Session["encabemensa"] = "Sr. Usuario";
                                    this.Session["cssclass"] = "MensajesSupConfirm";
                                    this.Session["mensaje"] = "Se ha creado con éxito las marcas para la campaña : " + LblTxtPresupuestoAsigProd.Text.ToUpper();
                                    Mensajes_SeguimientoValidacionVistas();
                                }
                                dtconsulta = null;
                            }
                            if (!CmbCompetidores.Text.Equals("0"))
                            {
                                LlenaCategoriasCompe();
                            }
                            else
                            {
                                LlenaCategorias();
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
                                    Mensajes_SeguimientoValidacionVistas();
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
                                            Mensajes_SeguimientoValidacionVistas();
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
                                        string usuario = this.Session["sUser"].ToString().Trim();

                                        DataTable dt_levfamilias = PPlanning.Get_Registrar_FamiliasPlanning(id_equipo, id_categoria, id_marca, id_familia, id_reporte, true, usuario, DateTime.Now, usuario, DateTime.Now);
                                        DPlanning Dplanning = new DPlanning();

                                        string id_eqcategoria = dt_levfamilias.Rows[0]["id_eqcategoria"].ToString();
                                        string id_eqfamilia = dt_levfamilias.Rows[0]["id_eqfamilia"].ToString();
                                        string id_eqmarca = dt_levfamilias.Rows[0]["id_eqmarca"].ToString();
                                        try
                                        {
                                            DataTable dtRegsitrarTBL_EQUIPO_FAMILIAS = Dplanning.Registrar_TBL_EQUIPO_FAMILIAS(id_eqfamilia, id_equipo, id_categoria,
                                                id_eqcategoria, id_marca.ToString(), id_eqmarca, id_familia, id_reporte.ToString());
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                        this.Session["encabemensa"] = "Sr. Usuario";
                                        this.Session["cssclass"] = "MensajesSupConfirm";
                                        this.Session["mensaje"] = "Se ha creado con éxito las familias para la campaña : " + LblTxtPresupuestoAsigProd.Text.ToUpper();
                                        Mensajes_SeguimientoValidacionVistas();
                                    }
                                }
                                if (!CmbCompetidores.Text.Equals("0"))
                                {
                                    LlenaCategoriasCompe();
                                }
                                else
                                {
                                    LlenaCategorias();
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
                                        Mensajes_SeguimientoValidacionVistas();
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
                                                Mensajes_SeguimientoValidacionVistas();
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
                                            DataTable dtcategorias = dregistrarCateg.Crear_CategoriasPlanning(id_planning, id_categoria, report_id, status, usuario, DateTime.Now, usuario, DateTime.Now);

                                            try
                                            {
                                                DataTable dtRegistrarTBL_EQUIPO_CATEGORIAS = dregistrarCateg.Registrar_TBL_EQUIPO_CATEGORIAS(dtcategorias.Rows[0]["id_CategoryPlanning"].ToString(), id_planning, id_categoria, report_id.ToString(), "1");
                                            }
                                            catch (Exception ex)
                                            {

                                            }

                                            this.Session["encabemensa"] = "Sr. Usuario";
                                            this.Session["cssclass"] = "MensajesSupConfirm";
                                            this.Session["mensaje"] = "Se ha creado con éxito las categorías para la campaña : " + LblTxtPresupuestoAsigProd.Text.ToUpper();
                                            Mensajes_SeguimientoValidacionVistas();
                                        }
                                    }
                                    if (!CmbCompetidores.Text.Equals("0"))
                                    {
                                        LlenaCategoriasCompe();
                                    }
                                    else
                                    {
                                        LlenaCategorias();
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
                                Mensajes_SeguimientoValidacionVistas();
                            }
                        }
                    }
                }
            }
            else
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                Mensajes_Productos();
            }
            ConsultaProductosCampaña();
            ModalPanelProductos.Hide();
            ModalPanelAsignaProductos.Show();
        }
        protected void BtnClearProd_Click(object sender, EventArgs e)
        {
            StiloBotonTipoProdSeleccionado();
            BtnProdPropio.Enabled = true;
            BtnProdCompe.Enabled = true;
            BtnSaveProd.Enabled = false;
            submarcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
            CmbCompetidores.Visible = false;
            LblSelCompetidor.Visible = false;
            CmbCompetidores.Text = "0";
            rbliscatego.Items.Clear();
            Chklistcatego.Items.Clear();
            rblmarca.Items.Clear();
            Chklistmarca.Items.Clear();
            rblsubmarca.Items.Clear();
            ChkListFamilias.Items.Clear();
            ChkProductos.Items.Clear();
            //ModalPanelProductos.Show();
            //ModalPanelAsignaProductos.Show();

            ModalPanelreporteproducto.Show();
            llenainformesProd();

        }
        protected void gvproductospropios_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["TipoProd"] = "Propio";
            LblMensajeConfirProd.Text = "Realmente desea eliminar el producto " + gvproductospropios.SelectedRow.Cells[4].Text + " del planning " + LblTxtPresupuestoAsigProd.Text + " ?";
            ModalPanelProductos.Show();
            ModalConfirmaProd.Show();
        }
        protected void Gvproductoscompetidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["TipoProd"] = "Competidor";
            LblMensajeConfirProd.Text = "Realmente desea eliminar el producto " + Gvproductoscompetidor.SelectedRow.Cells[4].Text + " del planning " + LblTxtPresupuestoAsigProd.Text + " ?";
            ModalPanelProductos.Show();
            ModalConfirmaProd.Show();
        }
        protected void gvmarcaspropias_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["TipoProd"] = "Propio";
            LblMensajeConfirProd.Text = "Realmente desea eliminar la marca " + gvmarcaspropias.SelectedRow.Cells[2].Text + " del planning " + LblTxtPresupuestoAsigProd.Text + " ?";
            ModalPanelProductos.Show();
            ModalConfirmaProd.Show();
        }
        protected void GvMarcascompetidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["TipoProd"] = "Competidor";
            LblMensajeConfirProd.Text = "Realmente desea eliminar la marca " + GvMarcascompetidor.SelectedRow.Cells[2].Text + " del planning " + LblTxtPresupuestoAsigProd.Text + " ?";
            ModalPanelProductos.Show();
            ModalConfirmaProd.Show();
        }
        protected void GvCategoriascompetidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["TipoProd"] = "Competidor";
            LblMensajeConfirProd.Text = "Realmente desea eliminar la marca " + GvCategoriascompetidor.SelectedRow.Cells[2].Text + " del planning " + LblTxtPresupuestoAsigProd.Text + " ?";
            ModalPanelProductos.Show();
            ModalConfirmaProd.Show();
        }
        protected void gvcategoriaspropias_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["TipoProd"] = "Propio";
            LblMensajeConfirProd.Text = "Realmente desea eliminar la Categoría " + gvcategoriaspropias.SelectedRow.Cells[2].Text + " del planning " + LblTxtPresupuestoAsigProd.Text + " ?";
            ModalPanelProductos.Show();
            ModalConfirmaProd.Show();
        }
        protected void gvFamiliaspropias_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["TipoProd"] = "Propio";
            LblMensajeConfirProd.Text = "Realmente desea eliminar la familia " + gvFamiliaspropias.SelectedRow.Cells[3].Text + " del planning " + LblTxtPresupuestoAsigProd.Text + " ?";
            ModalPanelProductos.Show();
            ModalConfirmaProd.Show();
        }
        protected void gvMatPOP_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["TipoProd"] = "MatePOP";
            LblMensajeConfirProd.Text = "Realmente desea eliminar el material POP " + gvMatPOP.SelectedRow.Cells[2].Text + " del planning " + LblTxtPresupuestoAsigProd.Text + " ?";
            ModalPanelProductos.Show();
            ModalConfirmaProd.Show();
        }
        protected void gvObservaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["TipoProd"] = "Observaciones";
            LblMensajeConfirProd.Text = "Realmente desea eliminar la observacion " + gvObservaciones.SelectedRow.Cells[2].Text + " del planning " + LblTxtPresupuestoAsigProd.Text + " ?";
            ModalPanelProductos.Show();
            ModalConfirmaProd.Show();
        }


        protected void GvFamiliascompetidor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["TipoProd"] = "Competidor";
            LblMensajeConfirProd.Text = "Realmente desea eliminar la familia " + GvFamiliascompetidor.SelectedRow.Cells[3].Text + " del planning " + LblTxtPresupuestoAsigProd.Text + " ?";
            ModalPanelProductos.Show();
            ModalConfirmaProd.Show();
        }
        protected void BtnSiConfirmaProd_Click(object sender, EventArgs e)
        {
            string TipoProducto = this.Session["TipoProd"].ToString().Trim();
            int company_id = Convert.ToInt32(this.Session["company_id"]);

            if (TipoProducto.Equals("Propio"))
            {
                if (gvproductospropios.SelectedRow != null)
                {
                    GridViewRow row = gvproductospropios.SelectedRow;
                    //PPlanning.Get_EliminarProductosTBL_PRODUCTO_CADENA(Convert.ToInt64(gvproductospropiosDEL.Rows[row.RowIndex].Cells[0].Text));
                    DPlanning EliminarProductTBL_EQUIPO_PRODUCTOS = new DPlanning();
                    string a=gvproductospropiosDEL.Rows[row.RowIndex].Cells[0].Text;
                    string b = gvproductospropiosDEL.Rows[row.RowIndex].Cells[0].Text;
                    string nomReporte = gvproductospropiosDEL.Rows[row.RowIndex].Cells[6].Text;

                    EliminarProductTBL_EQUIPO_PRODUCTOS.Eliminar_ProductTBL_EQUIPO_PRODUCTOS(gvproductospropiosDEL.Rows[row.RowIndex].Cells[2].Text, nomReporte, company_id, TxtPlanningAsigProd.Text);
                    EliminarProductTBL_EQUIPO_PRODUCTOS.Eliminar_ProductPlanning(Convert.ToInt32(b));
                    //PPlanning.Get_EliminarProductosPlanning(Convert.ToInt64(gvproductospropiosDEL.Rows[row.RowIndex].Cells[0].Text));
                }
                if (gvmarcaspropias.SelectedRow != null)
                {
                    GridViewRow row = gvmarcaspropias.SelectedRow;
                    PPlanning.Get_EliminarMarcasTBL_EQUIPO_MARCAS(gvmarcaspropiasDEL.Rows[row.RowIndex].Cells[0].Text);
                    PPlanning.Get_EliminarMarcasPlanning(Convert.ToInt64(gvmarcaspropiasDEL.Rows[row.RowIndex].Cells[0].Text));
                }

                if (gvFamiliaspropias.SelectedRow != null)
                {
                    GridViewRow row = gvFamiliaspropias.SelectedRow;
                    PPlanning.Get_EliminarFamiliasTBL_EQUIPO_FAMILIAS(gvFamiliaspropiasDEL.Rows[row.RowIndex].Cells[0].Text);
                    PPlanning.Get_EliminarFamiliasPlanning(Convert.ToInt64(gvFamiliaspropiasDEL.Rows[row.RowIndex].Cells[0].Text));
                }

                if (gvcategoriaspropias.SelectedRow != null)
                {
                    GridViewRow row = gvcategoriaspropias.SelectedRow;
                    DPlanning DEliminarCateg = new DPlanning();
                    DEliminarCateg.Eliminar_CategoriasTBL_EQUIPO_CATEGORIAS(gvcategoriaspropiasDEL.Rows[row.RowIndex].Cells[0].Text);
                    DEliminarCateg.Eliminar_CategoriasPlanning(Convert.ToInt64(gvcategoriaspropiasDEL.Rows[row.RowIndex].Cells[0].Text));                   
                }
            }
            if (TipoProducto.Equals("Competidor"))
            {
                if (Gvproductoscompetidor.SelectedRow != null)
                {
                    GridViewRow row = Gvproductoscompetidor.SelectedRow;
                    string nomReporte = Gvproductoscompetidor.Rows[row.RowIndex].Cells[6].Text;
                    //PPlanning.Get_EliminarProductosTBL_PRODUCTO_CADENA(Convert.ToInt64(GvproductoscompetidorDEL.Rows[row.RowIndex].Cells[0].Text));
                    DPlanning EliminarProductTBL_EQUIPO_PRODUCTOS = new DPlanning();
                    EliminarProductTBL_EQUIPO_PRODUCTOS.Eliminar_ProductTBL_EQUIPO_PRODUCTOS(GvproductoscompetidorDEL.Rows[row.RowIndex].Cells[2].Text, nomReporte, company_id, TxtPlanningAsigProd.Text);
                    PPlanning.Get_EliminarProductosPlanning(Convert.ToInt64(GvproductoscompetidorDEL.Rows[row.RowIndex].Cells[0].Text));
                }
                if (GvMarcascompetidor.SelectedRow != null)
                {
                    GridViewRow row = GvMarcascompetidor.SelectedRow;
                    PPlanning.Get_EliminarMarcasTBL_EQUIPO_MARCAS(GvmarcascompetidorDEL.Rows[row.RowIndex].Cells[0].Text);
                    PPlanning.Get_EliminarMarcasPlanning(Convert.ToInt64(GvmarcascompetidorDEL.Rows[row.RowIndex].Cells[0].Text));
                }
                if (GvFamiliascompetidor.SelectedRow != null)
                {
                    GridViewRow row = GvFamiliascompetidor.SelectedRow;
                    PPlanning.Get_EliminarFamiliasTBL_EQUIPO_FAMILIAS(GvFamiliascompetidorDEL.Rows[row.RowIndex].Cells[0].Text);
                    PPlanning.Get_EliminarFamiliasPlanning(Convert.ToInt64(GvFamiliascompetidorDEL.Rows[row.RowIndex].Cells[0].Text));

                }

                if (GvCategoriascompetidor.SelectedRow != null)
                {
                    GridViewRow row = GvCategoriascompetidor.SelectedRow;
                    DPlanning DEliminarCateg = new DPlanning();
                    DEliminarCateg.Eliminar_CategoriasTBL_EQUIPO_CATEGORIAS(GvCategoriascompetidorDEL.Rows[row.RowIndex].Cells[0].Text);
                    DEliminarCateg.Eliminar_CategoriasPlanning(Convert.ToInt64(gvcategoriaspropiasDEL.Rows[row.RowIndex].Cells[0].Text));
                }
            }

            if (TipoProducto.Equals("MatePOP"))
            {
                Conexion cn = new Conexion();
                cn.ejecutarDataTable("PLA_ELIMINAR_MATERIALPOPDELPLANNING", TxtPlanningAsigProd.Text, gvMatPOP.SelectedRow.Cells[1].Text);

                Conexion con = new Conexion(2);
                con.ejecutarDataTable("PLA_ELIMINAR_TBL_POP", TxtPlanningAsigProd.Text, gvMatPOP.SelectedRow.Cells[1].Text);
            }

            if (TipoProducto.Equals("Observaciones"))
            {
                Conexion cn = new Conexion();
                cn.ejecutarDataTable("PLA_ELIMINAR_PLANNING_OBSERVACIONES", TxtPlanningAsigProd.Text, gvObservaciones.SelectedRow.Cells[1].Text);

                string obser = "";

                if (gvObservaciones.SelectedRow.Cells[1].Text.Length == 1)
                {
                    obser = "0" + gvObservaciones.SelectedRow.Cells[1].Text;
                }
                else
                {
                    obser = gvObservaciones.SelectedRow.Cells[1].Text; 
                }


                Conexion con = new Conexion(2);
                con.ejecutarDataTable("PLA_ELIMINAR_TBL_EQUIPO_OBSERVACIONES", TxtPlanningAsigProd.Text, obser);
            }

            ConsultaProductosCampaña();
            ModalPanelProductos.Show();
        }

        protected void BtnNoConfirmaProd_Click(object sender, EventArgs e)
        {
            ModalPanelProductos.Show();
        }
        protected void BtnaceptaProductos_Click(object sender, EventArgs e)
        {
            ModalPanelProductos.Show();
            ModalPanelAsignaProductos.Show();
        }
        protected void BtnSelInfProd_Click(object sender, EventArgs e)
        {
            try
            {
                ModalPanelAsignaProductos.Show();
                llenacompetidores();
                StiloBotonTipoProdSeleccionado();
                BtnProdCompe.Enabled = true;
                BtnProdPropio.Enabled = true;                
                CmbCompetidores.Visible = false;
                LblSelCompetidor.Visible = false;
                BtnSaveProd.Enabled = false;
                BtnClearProd.Enabled = true;
                RbtMasopciones.Items.Clear();
                limpiarlistas();

                BtnSaveProd.Visible = true;
                BtnClearProd.Visible = true;
                BtnCargaLevanInform.Visible = true;
                #region backup codigo previo de visualizacion de paneles
                //DPlanning dplanning = new DPlanning();
                //DataTable dtValidacion = dplanning.ValidaTipoGestion(Convert.ToInt32(this.Session["company_id"].ToString().Trim()),
                //     this.Session["Planning_CodChannel"].ToString().Trim(), Convert.ToInt32(RbtnListInfProd.SelectedItem.Value));
                //if (dtValidacion != null)
                //{
                //    if (dtValidacion.Rows.Count > 0)
                //    {
                //        if (dtValidacion.Rows[0]["id_Tipo_Reporte"].ToString().Trim() == "")
                //        {
                //            divselproductos.Style.Value = "display:block;";
                //            div_masopciones.Style.Value = "display:none;";
                //            if (dtValidacion.Rows[0]["vista_final"].ToString().Trim() == "Categoria")
                //            {
                //                categorias.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                Marcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
                //                submarcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
                //                Familias.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
                //                productos.Style.Value = "width: 390px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
                //                BtnProdPropio.Text = "Categorías Propias";
                //                BtnProdCompe.Text = "Categorias Competidor";

                //                BtnProdPropio.Visible = false;
                //                BtnProdCompe.Visible = false;
                //                this.Session["vista_final"] = dtValidacion.Rows[0]["vista_final"].ToString().Trim();
                //                LlenaCategorias();

                //            }
                //            if (dtValidacion.Rows[0]["vista_final"].ToString().Trim() == "Marca")
                //            {
                //                categorias.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                Marcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                submarcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
                //                Familias.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
                //                productos.Style.Value = "width: 390px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
                //                BtnProdPropio.Text = "Marcas Propias";
                //                BtnProdCompe.Text = "Marcas Competidor";
                //                BtnProdPropio.Visible = true;
                //                BtnProdCompe.Visible = true;
                //            }
                //            if (dtValidacion.Rows[0]["vista_final"].ToString().Trim() == "SubMarca")
                //            {
                //                categorias.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                Marcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                submarcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                Familias.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
                //                productos.Style.Value = "width: 390px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
                //                BtnProdPropio.Visible = true;
                //                BtnProdCompe.Visible = true;
                //            }
                //            if (dtValidacion.Rows[0]["vista_final"].ToString().Trim() == "Familia")
                //            {
                //                categorias.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                Marcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                submarcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
                //                Familias.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                productos.Style.Value = "width: 390px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
                //                BtnProdPropio.Text = "Familias Propias";
                //                BtnProdCompe.Text = "Familias Competidor";
                //                BtnProdPropio.Visible = true;
                //                BtnProdCompe.Visible = true;
                //            }
                //            if (dtValidacion.Rows[0]["vista_final"].ToString().Trim() == "Producto")
                //            {
                //                categorias.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                Marcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                submarcas.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                Familias.Style.Value = "width: 260px; border-color: Black; border-width: 1px; border-style: solid; display:none;";
                //                productos.Style.Value = "width: 390px; border-color: Black; border-width: 1px; border-style: solid; display:Block;";
                //                BtnProdPropio.Text = "Productos Propios";
                //                BtnProdCompe.Text = "Productos Competidor";
                //                BtnProdPropio.Visible = true;
                //                BtnProdCompe.Visible = true;

                //            }
                //            if (dtValidacion.Rows[0]["vista_final"].ToString().Trim() == "")
                //            {
                //                divselproductos.Style.Value = "display:none;";
                //                div_masopciones.Style.Value = "display:none;";
                //                this.Session["encabemensa"] = "Señor Usuario";
                //                this.Session["cssclass"] = "MensajesSupervisor";
                //                this.Session["mensaje"] = "No existe parametrización de esta vista . Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente";
                //                BtnProdPropio.Visible = false;
                //                BtnProdCompe.Visible = false;
                //                ModalPanelAsignaProductos.Hide();
                //                ModalPanelreporteproducto.Show();
                //                Mensajes_SeguimientoValidacionVistas();

                //            }
                //            this.Session["vista_final"] = dtValidacion.Rows[0]["vista_final"].ToString().Trim();
                //        }
                //        else
                //        {
                //            divselproductos.Style.Value = "display:none;";
                //            div_masopciones.Style.Value = "display:block;";
                //            RbtMasopciones.DataSource = dtValidacion;
                //            RbtMasopciones.DataTextField = "TipoReporte_Descripcion";
                //            RbtMasopciones.DataValueField = "id_Tipo_Reporte";
                //            RbtMasopciones.DataBind();

                //            BtnSaveProd.Visible = false;
                //            BtnClearProd.Visible = false;
                //            BtnCargaLevanInform.Visible = false;

                //        }
                //    }
                //    else
                //    {
                //        divselproductos.Style.Value = "display:none;";
                //        div_masopciones.Style.Value = "display:none;";
                //        this.Session["encabemensa"] = "Señor Usuario";
                //        this.Session["cssclass"] = "MensajesSupervisor";
                //        this.Session["mensaje"] = "No existe parametrización de esta vista . Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente";

                //        ModalPanelAsignaProductos.Hide();
                //        ModalPanelreporteproducto.Show(); 

                //        Mensajes_SeguimientoValidacionVistas();
                //    }

                //}
                //else
                //{
                //    divselproductos.Style.Value = "display:none;";
                //    div_masopciones.Style.Value = "display:none;";
                //    this.Session["encabemensa"] = "Señor Usuario";
                //    this.Session["cssclass"] = "MensajesSupervisor";
                //    this.Session["mensaje"] = "No existe parametrización de esta vista . Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente";
                //    ModalPanelAsignaProductos.Hide();
                //    ModalPanelreporteproducto.Show();
                //    Mensajes_SeguimientoValidacionVistas();

                //}
                //dtValidacion = null; 
                #endregion

                LblReporteAsociado.Visible = true;
                LblReporteAsociado.Text = "Reporte " + RbtnListInfProd.SelectedItem.Text;                
                this.Session["RbtnListInfProd"] = RbtnListInfProd.SelectedItem.Value;
                
                int id_company = Convert.ToInt32(this.Session["company_id"].ToString().Trim());
                string cod_channel = this.Session["Planning_CodChannel"].ToString().Trim();
                int id_report = Convert.ToInt32(RbtnListInfProd.SelectedItem.Value);

                this.Session["id_report"] = id_report;

                DPlanning dplanning = new DPlanning();
                DataTable vistas = dplanning.ValidaTipoGestion(id_company, cod_channel, id_report);
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
                if (id_report.ToString()=="25")
                {
                    BtnProdPropio.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ModalPanelAsignaProductos.Hide();
                ModalPanelreporteproducto.Show();
                divselproductos.Style.Value = "display:none;";
                div_masopciones.Style.Value = "display:none;";               
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

            if (tipo_rep == "03" && rep == "58" && id_company == 1561)//elementos de visibilidad....Carlos Marin
            {

                div_Elementos.Style.Value = "display:block;";
                div_masopciones.Style.Value = "display:none;";


                Conexion cn = new Conexion();
                DataTable dt = cn.ejecutarDataTable("PLA_LISTAR_MPointOfPurchaseXTipoMaterial");


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
                        Mensajes_Productos();
                    }
                }
                else
                {
                    divselproductos.Style.Value = "display:none;";
                    div_masopciones.Style.Value = "display:none;";
                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "No existe parametrización de esta vista. Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente.";
                    Mensajes_SeguimientoValidacionVistas();
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
        protected void RbtMasopciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblReporteAsociado.Text = LblReporteAsociado.Text + "_" + RbtMasopciones.SelectedItem.Text;
            opcionesvistareporte();
            //divselproductos.Style.Value = "display:block;";
            //div_masopciones.Style.Value = "display:none;";
            if (this.Session["RbtnListInfProd"].ToString() == "25")
            {
                BtnProdPropio.Visible = false;
            }
            ModalPanelAsignaProductos.Show();
            #region backup anterior
		
            //LblReporteAsociado.Text = LblReporteAsociado.Text + "_" + RbtMasopciones.SelectedItem.Text;

            //BtnSaveProd.Visible = true;
            //BtnClearProd.Visible = true;
            //BtnCargaLevanInform.Visible = true;
            //DPlanning dplanning = new DPlanning();

            //DataTable validacion = (DataTable)this.Session["validacion"];

            //if (validacion != null)
            //{
            //    if (validacion.Rows.Count > 0)
            //    {
            //        for (int i = 0; i <= validacion.Rows.Count - 1; i++)
            //        {
            //            if (validacion.Rows[i]["TipoReporte_Descripcion"].ToString().Trim() == RbtMasopciones.SelectedItem.Text)
            //            {
            //                divselproductos.Style.Value = "display:block;";
            //                div_masopciones.Style.Value = "display:none;";
            //                string rep = this.Session["RbtnListInfProd"].ToString();

            //                DataTable vistas = (DataTable)this.Session["vistas"];
            //                string tipo;
            //                string vista_final="";
            //                int row = 0;

            //                if (rep.Equals("23"))//FOTOGRAFICO
            //                {
            //                    if (RbtMasopciones.Text.Equals("02"))//tipo exhib. visib.
            //                        row = 1;
            //                }
            //                else if (rep.Equals("58"))//PRESENCIA
            //                {
            //                    if (RbtMasopciones.Text.Equals("04"))//tipo exhib. visib.
            //                        row = 1;
            //                    if (RbtMasopciones.Text.Equals("05"))//tipo exhib. visib.
            //                        row = 2;
            //                    if (RbtMasopciones.Text.Equals("06"))//tipo exhib. visib.
            //                        row = 3;
            //                    if (RbtMasopciones.Text.Equals("07"))//tipo exhib. visib.
            //                        row = 4;
            //                    if (RbtMasopciones.Text.Equals("08"))//tipo exhib. visib.
            //                        row = 5;
            //                }

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

                            

            //                if (vista_final.Equals("Categoria"))
            //                {
            //                    showpanels();
            //                    BtnProdPropio.Text = "Categorías Propias";
            //                    BtnProdCompe.Text = "Categorias Competidor";

            //                }
            //                if (vista_final.Equals("Marca"))
            //                {
            //                    showpanels();
            //                    BtnProdPropio.Text = "Marcas Propias";
            //                    BtnProdCompe.Text = "Marcas Competidor";
            //                }
            //                if (vista_final.Equals("SubMarca"))
            //                {
            //                    showpanels();

            //                }
            //                if (vista_final.Equals("Familia"))
            //                {
            //                    showpanels();
            //                    BtnProdPropio.Text = "Familias Propias";
            //                    BtnProdCompe.Text = "Familias Competidor";

            //                }
            //                if (vista_final.Equals("SubFamilia"))
            //                {
            //                    BtnProdPropio.Text = "SubFamilias Propias";
            //                    BtnProdCompe.Text = "SubFamilias Competidor";
            //                    BtnProdPropio.Visible = true;
            //                    BtnProdCompe.Visible = true;
            //                    showpanels();
            //                }
            //                if (vista_final.Equals("Producto"))
            //                {
            //                    showpanels();
            //                    BtnProdPropio.Text = "Productos Propios";
            //                    BtnProdCompe.Text = "Productos Competidor";

            //                }
            //                if (vista_final.Equals(""))
            //                {
            //                    divselproductos.Style.Value = "display:none;";
            //                    div_masopciones.Style.Value = "display:none;";
            //                    this.Session["encabemensa"] = "Señor Usuario";
            //                    this.Session["cssclass"] = "MensajesSupervisor";
            //                    this.Session["mensaje"] = "No existe parametrización de esta vista. Por favor informe al Administrador para parametrizar vista para el reporte seleccionado del cliente y canal correspondiente";
            //                    Mensajes_SeguimientoValidacionVistas();

            //                }
            //                this.Session["vista_final"] = validacion.Rows[i]["vista_final"].ToString().Trim();
            //                i = validacion.Rows.Count - 1;

            //            }
            //        }
            //    }
            //}
            //ModalPanelAsignaProductos.Show(); 
	#endregion
        }
        protected void ImgCloseVistas_Click(object sender, ImageClickEventArgs e)
        {
            llenainformesProd();
            ModalPanelreporteproducto.Hide();
            ModalPanelProductos.Show();

        }
        protected void BtnAceptaMensajeVista_Click(object sender, EventArgs e)
        {
            ModalPanelAsignaProductos.Hide();
            ModalPanelreporteproducto.Show();
            divselproductos.Style.Value = "display:none;";
            div_masopciones.Style.Value = "display:none;";
        }

        #endregion

        #region Reportes
        protected void ImgIrAReportesCampaña_Click(object sender, ImageClickEventArgs e)
        {
            botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();

            ModalPanelReportPlan.Show();
            ConsultaReportesCampaña();
            ConsultaGvFrecuencias();

        }
        protected void GVReportesAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblMensajeConfirReport.Text = "Realmente desea eliminar el informe : " + GVReportesAsignados.SelectedRow.Cells[2].Text + ", " + GVReportesAsignados.SelectedRow.Cells[4].Text + ", Periodo : " + GVReportesAsignados.SelectedRow.Cells[5].Text + " de la campaña : " + LblTxtPresupuestoAsigReportes.Text + " ?";
            ModalPanelReportPlan.Show();
            ModalConfirmaReport.Show();
        }
        protected void BtnSiConfirmaReport_Click(object sender, EventArgs e)
        {
            ModalPanelReportPlan.Show();
            try
            {
                GridViewRow row = GVReportesAsignados.SelectedRow;
                PPlanning.Get_EliminarReportesPlanning(Convert.ToInt32(GVReportesAsignadosDEL.Rows[row.RowIndex].Cells[0].Text));
                ConsultaReportesCampaña();

            }
            catch (Exception ex)
            {
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "No es posible eliminar este registro ya que existe relación en la entidad categorías por cliente";
                Mensajes_ReportesDel();
            }

        }
        protected void BtnNoConfirmaReport_Click(object sender, EventArgs e)
        {
            ModalPanelReportPlan.Show();
        }
        protected void BtnAddFrecuencia_Click(object sender, EventArgs e)
        {
            this.Session["ActualizaReportesPlanning"] = 0;

            DAplicacion odDuplicado = new DAplicacion();
            DataTable dtAsigna_reportplanning = (DataTable)this.Session["dtAsigna_reportplanning"];

            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupervisor";

            if (RBtnListInformes.SelectedValue == "")
            {
                this.Session["mensaje"] = "Debe Seleccionar un Informe";
                Mensajes_Reportes();
                return;
            }

            if (RbtnListmeses.SelectedValue == "")
          //  if (ChkListMeses.SelectedValue == "")
            {
                this.Session["mensaje"] = "Debe Seleccionar el mes";
                Mensajes_Reportes();
                return;
            }

            if (ChklstFrecuencia.SelectedValue == "")
            {
                this.Session["mensaje"] = "Debe Seleccionar el periodo";
                Mensajes_Reportes();
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
                                        Mensajes_Reportes();
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
                                    DataTable dtDuplicado = odDuplicado.ConsultaDuplicadoReportPlanning(ConfigurationManager.AppSettings["Reports_Planning"], TxtPlanningAsigReports.Text, RBtnListInformes.SelectedItem.Value, RbtnListmeses.SelectedValue, ChklstFrecuencia.Items[frecuencias].Value);
                                    if (dtDuplicado != null)
                                    {
                                        this.Session["mensaje"] = "El informe " + RBtnListInformes.SelectedItem.Text + " para el mes " + RbtnListmeses.SelectedItem.Text + " y periodo " + ChklstFrecuencia.Items[frecuencias].Value + " Ya esta creado en la base de datos";
                                        Mensajes_Reportes();
                                        //i = ChkListMeses.Items.Count - 1;
                                        frecuencias = ChklstFrecuencia.Items.Count - 1;
                                        continuar = false;
                                    }
                                    dtDuplicado = null;
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
                        dtAsigna_reportplanning = null;
                        RBtnListInformes.SelectedIndex = -1;                        
                        //ChkListMeses.SelectedIndex = -1;
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
                        ModalPanelReportes.Show();
                    }
                }
            }
            catch
            {
                PAdmin.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

        }
        protected void BtnaceptaReports_Click(object sender, EventArgs e)
        {
            if (this.Session["ActualizaReportesPlanning"].ToString().Trim() == "1")
            {
                ModalPanelReportes.Hide();
                ModalPanelReportPlan.Show();
            }
            else
            {
                ModalPanelReportes.Show();
            }
        }
        protected void BtnaceptaReportsDel_Click(object sender, EventArgs e)
        {

            ModalPanelReportes.Hide();
            ModalPanelReportPlan.Show();

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
                ModalPanelReportes.Show();
                dtdel = null;
            }
            catch
            {
                PAdmin.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        protected void BtnSaveReportes_Click(object sender, EventArgs e)
        {
            //DAplicacion odDuplicado = new DAplicacion();
            //if (GVFrecuencias.Rows.Count > 0)
            //{
            //    for (int i = 0; i <= GVFrecuencias.Rows.Count - 1; i++)
            //    {
            //        DataTable dtDuplicado = odDuplicado.ConsultaDuplicadoReportPlanning(ConfigurationManager.AppSettings["Reports_Planning"], TxtPlanningAsigReports.Text, GVFrecuencias.Rows[i].Cells[1].Text, GVFrecuencias.Rows[i].Cells[3].Text, GVFrecuencias.Rows[i].Cells[5].Text);
            //        if (dtDuplicado == null)
            //        {
            //            PPlanning.Get_InsertaReportesPlanning(TxtPlanningAsigReports.Text, Convert.ToInt32(GVFrecuencias.Rows[i].Cells[1].Text), Convert.ToInt32(GVFrecuencias.Rows[i].Cells[5].Text), GVFrecuencias.Rows[i].Cells[3].Text, Convert.ToInt32(GVFrecuencias.Rows[i].Cells[5].Text), Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechaini")).Text), Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechafin")).Text), true, this.Session["sUser"].ToString(), DateTime.Now, this.Session["sUser"].ToString(), DateTime.Now);
            //        }
            //    }
            //    this.Session["encabemensa"] = "Sr. Usuario";
            //    this.Session["cssclass"] = "MensajesSupConfirm";
            //    this.Session["mensaje"] = "Se ha creado con éxito los Informes de la campaña : " + LblTxtPresupuestoAsigReports.Text.ToUpper();
            //    Mensajes_Reportes();
            //    RBtnListInformes.SelectedIndex = -1;
            //    ChkListMeses.SelectedIndex = -1;
            //    ChklstFrecuencia.SelectedIndex = -1;
            //    ConsultaGvFrecuencias();

            //    DataTable dtReportes = PPlanning.Get_ConsultarReportesPlanning(TxtPlanningAsigReportes.Text);

            //    GVReportesAsignados.DataSource = dtReportes;
            //    GVReportesAsignados.DataBind();
            //    GVReportesAsignadosDEL.DataSource = dtReportes;
            //    GVReportesAsignadosDEL.DataBind();
            //    dtReportes = null;
            //}
            //else
            //{
            //    this.Session["encabemensa"] = "Sr. Usuario";
            //    this.Session["cssclass"] = "MensajesSupervisor";
            //    this.Session["mensaje"] = "Debe realizar al menos una Asignación";
            //    Mensajes_Reportes();
            //    return;

            //}
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
                        DateTime FechaInicial = Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechaini")).Text);
                        DateTime Fechafinal = Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000");
                        if (FechaInicial > Fechafinal)
                        {
                            this.Session["mensaje"] = "La fecha inicial no puede ser mayor a la fecha final.";
                            Mensajes_Reportes();
                            seguir = false;
                            i = GVFrecuencias.Rows.Count;
                            return;
                        }

                        if (seguir)
                        {
                            if (Convert.ToString(FechaInicial.Year) != GVFrecuencias.Rows[i].Cells[5].Text ||
                                Convert.ToString(Fechafinal.Year) != GVFrecuencias.Rows[i].Cells[5].Text)
                                // || Convert.ToInt32(Fechafinal.Month) != Convert.ToInt32(GVFrecuencias.Rows[i].Cells[3].Text))  -- Comentado para habilitar final de periodo en el mes siguiente
                            // || Convert.ToInt32(FechaInicial.Month) != Convert.ToInt32(GVFrecuencias.Rows[i].Cells[3].Text) )
                            {
                                this.Session["mensaje"] = "El reporte : " + GVFrecuencias.Rows[i].Cells[2].Text +
                                    " es de " + GVFrecuencias.Rows[i].Cells[4].Text + " de " +
                                     GVFrecuencias.Rows[i].Cells[5].Text + ". Deben ser del mismo año, por favor verifique";

                                Mensajes_Reportes();
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
                                    if (Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[k].Cells[0].FindControl("TxtFechaini")).Text) >= Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechaini")).Text)
                                        && Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[k].Cells[0].FindControl("TxtFechaini")).Text) <= Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000")
                                         )
                                    {
                                        cuenta = cuenta + 1;
                                    }
                                    if (cuenta > 1)
                                    {

                                        this.Session["mensaje"] = "Los rangos de fechas por reporte y periodo no deben coincidir. Por favor verifique";
                                        Mensajes_Reportes();
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
                        Mensajes_Reportes();
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
                    DataTable dtvalidarango = odDuplicado.ConsultaRangos(TxtPlanningAsigReportes.Text, Convert.ToInt32(GVFrecuencias.Rows[j].Cells[1].Text),
                        Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[j].Cells[0].FindControl("TxtFechaini")).Text),
                        Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[j].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000"),
                        Convert.ToInt32(GVFrecuencias.Rows[j].Cells[5].Text));
                    if (dtvalidarango.Rows.Count > 0)
                    {
                        seguir = false;
                        j = GVFrecuencias.Rows.Count;
                        this.Session["mensaje"] = "Ya existe un periodo para ese reporte el cual contiene el Rango de fechas ingresado.";
                        Mensajes_Reportes();
                        return;

                    }
                    dtvalidarango = null;

                }

                if (seguir)
                {
                    for (int i = 0; i <= GVFrecuencias.Rows.Count - 1; i++)
                    {
                        DataTable dtDuplicado = odDuplicado.ConsultaDuplicadoReportPlanning(ConfigurationManager.AppSettings["Reports_Planning"], TxtPlanningAsigReportes.Text, GVFrecuencias.Rows[i].Cells[1].Text, GVFrecuencias.Rows[i].Cells[3].Text, GVFrecuencias.Rows[i].Cells[5].Text);
                        if (dtDuplicado == null)
                        {
                            PPlanning.Get_InsertaReportesPlanning(TxtPlanningAsigReportes.Text, Convert.ToInt32(GVFrecuencias.Rows[i].Cells[1].Text), Convert.ToInt32(GVFrecuencias.Rows[i].Cells[5].Text), GVFrecuencias.Rows[i].Cells[3].Text, Convert.ToInt32(GVFrecuencias.Rows[i].Cells[6].Text), Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechaini")).Text), Convert.ToDateTime(((TextBox)GVFrecuencias.Rows[i].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000"), true, this.Session["sUser"].ToString(), DateTime.Now, this.Session["sUser"].ToString(), DateTime.Now);
                        }
                        dtDuplicado = null;
                    }
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "Se ha creado con éxito los Informes de la campaña : " + LblTxtPresupuestoAsigReports.Text.ToUpper();
                    Mensajes_Reportes();
                    RBtnListInformes.SelectedIndex = -1;
                    CmbSelAñoCampaña.SelectedIndex = -1;
                   // ChkListMeses.Items.Clear();
                    RbtnListmeses.Items.Clear();
                    ChklstFrecuencia.Items.Clear();

                    ChklstFrecuencia.SelectedIndex = -1;
                    ConsultaGvFrecuencias();

                    DataTable dtReportes = PPlanning.Get_ConsultarReportesPlanning(TxtPlanningAsigReportes.Text);

                    GVReportesAsignados.DataSource = dtReportes;
                    GVReportesAsignados.DataBind();
                    GVReportesAsignadosDEL.DataSource = dtReportes;
                    GVReportesAsignadosDEL.DataBind();
                    dtReportes = null;

                }
            }
            else
            {
                this.Session["mensaje"] = "Ingrese las fechas de inicio y fin por cada asignación.";
                Mensajes_Reportes();
            }
        }
        protected void BtnClearReportes_Click(object sender, EventArgs e)
        {
            RBtnListInformes.SelectedIndex = -1;
            //ChkListMeses.SelectedIndex = -1;
            RbtnListmeses.SelectedIndex = -1;
            ChklstFrecuencia.Items.Clear();
            ConsultaGvFrecuencias();
            ModalPanelReportPlan.Show();
            ModalPanelReportes.Show();
        }
        protected void CmbSelAñoCampaña_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbSelAñoCampaña.Text != "0")
            {
                llenameses();
            }
            else
            {
               // ChkListMeses.Items.Clear();
                RbtnListmeses.Items.Clear();
                ChklstFrecuencia.Items.Clear();
            }
        }
        //protected void BtnActualizarReportPlanning_Click(object sender, EventArgs e)
        //{
        //    this.Session["ActualizaReportesPlanning"] = 1;
        //    bool seguir = true;
        //    for (int i = 0; i <= GVReportesAsignados.Rows.Count - 1; i++)
        //    {
        //        if (((TextBox)GVReportesAsignados.Rows[i].Cells[0].FindControl("TxtFechaini")).Text == "" ||
        //            ((TextBox)GVReportesAsignados.Rows[i].Cells[0].FindControl("TxtFechafin")).Text == "")
        //        {
        //            seguir = false;
        //            i = GVReportesAsignados.Rows.Count;
        //        }

        //        if (seguir)
        //        {
        //            try
        //            {
        //                DateTime FechaInicial = Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[i].Cells[0].FindControl("TxtFechaini")).Text);
        //                DateTime Fechafinal = Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[i].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000");
        //                if (FechaInicial > Fechafinal)
        //                {
        //                    this.Session["encabemensa"] = "Sr. Usuario";
        //                    this.Session["cssclass"] = "MensajesSupervisor";
        //                    this.Session["mensaje"] = "La fecha inicial no puede ser mayor a la fecha final.";
        //                    Mensajes_Reportes();
        //                    seguir = false;
        //                    i = GVReportesAsignados.Rows.Count;
        //                    ModalPanelReportes.Hide();
        //                    ModalPanelReportPlan.Show();
        //                    return;
        //                }

        //                if (seguir)
        //                {
        //                    if (Convert.ToString(FechaInicial.Year) != GVReportesAsignados.Rows[i].Cells[3].Text ||
        //                        Convert.ToString(Fechafinal.Year) != GVReportesAsignados.Rows[i].Cells[3].Text || Convert.ToInt32(Fechafinal.Month) != Convert.ToInt32(GVReportesAsignados.Rows[i].Cells[4].Text))
        //                    {
        //                        this.Session["encabemensa"] = "Sr. Usuario";
        //                        this.Session["cssclass"] = "MensajesSupervisor";
        //                        this.Session["mensaje"] = "El reporte : " + GVReportesAsignados.Rows[i].Cells[2].Text +
        //                            " es de " + GVReportesAsignados.Rows[i].Cells[5].Text + " de " +
        //                             GVReportesAsignados.Rows[i].Cells[3].Text + ". deben ser del mismo año y la fecha fin debe ser del mes " + GVReportesAsignados.Rows[i].Cells[5].Text + " Por favor verifique";
        //                        Mensajes_Reportes();
        //                        seguir = false;
        //                        i = GVReportesAsignados.Rows.Count;
        //                        ModalPanelReportes.Hide();
        //                        ModalPanelReportPlan.Show();
        //                        return;
        //                    }
        //                    if (seguir)
        //                    {
        //                        int cuenta = 0;
        //                        for (int k = 0; k <= GVReportesAsignados.Rows.Count - 1; k++)
        //                        {

        //                            if (Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[k].Cells[0].FindControl("TxtFechaini")).Text) >= Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[i].Cells[0].FindControl("TxtFechaini")).Text)
        //                                && Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[k].Cells[0].FindControl("TxtFechaini")).Text) <= Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[i].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000")
        //                                 )
        //                            {
        //                                cuenta = cuenta + 1;
        //                            }
        //                            if (cuenta > 1)
        //                            {
        //                                this.Session["encabemensa"] = "Sr. Usuario";
        //                                this.Session["cssclass"] = "MensajesSupervisor";
        //                                this.Session["mensaje"] = "Los rangos de fechas por reporte y periodo no deben coincidir. Por favor verifique";
        //                                Mensajes_Reportes();
        //                                seguir = false;
        //                                k = GVReportesAsignados.Rows.Count;
        //                                i = GVReportesAsignados.Rows.Count;
        //                                ModalPanelReportes.Hide();
        //                                ModalPanelReportPlan.Show();
        //                                return;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            catch
        //            {
        //                this.Session["encabemensa"] = "Sr. Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";
        //                this.Session["mensaje"] = "Debe incluir todas las fechas con formato (dd/mm/aaaa).";
        //                Mensajes_Reportes();
        //                seguir = false;
        //                i = GVReportesAsignados.Rows.Count;
        //                ModalPanelReportes.Hide();
        //                ModalPanelReportPlan.Show();
        //                return;
        //            }
        //        }

        //    }
        //    if (seguir)
        //    {

        //        DAplicacion odDuplicado = new DAplicacion();
        //        for (int j = 0; j <= GVReportesAsignados.Rows.Count - 1; j++)
        //        {
        //            DataTable dtvalidarango = odDuplicado.ConsultaRangos(TxtPlanningAsigReportes.Text, Convert.ToInt32(GVReportesAsignados.Rows[j].Cells[1].Text),
        //                Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[j].Cells[0].FindControl("TxtFechaini")).Text),
        //                Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[j].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000"),
        //                Convert.ToInt32(GVReportesAsignados.Rows[j].Cells[5].Text));
        //            if (dtvalidarango.Rows.Count > 0)
        //            {
        //                seguir = false;
        //                j = GVReportesAsignados.Rows.Count;
        //                this.Session["encabemensa"] = "Sr. Usuario";
        //                this.Session["cssclass"] = "MensajesSupervisor";
        //                this.Session["mensaje"] = "Ya existe un periodo para ese reporte el cual contiene el Rango de fechas ingresado.";
        //                Mensajes_Reportes();
        //                ModalPanelReportes.Hide();
        //                ModalPanelReportPlan.Show();

        //                return;

        //            }
        //            dtvalidarango = null;

        //        }

        //        if (seguir)
        //        {
        //            for (int i = 0; i <= GVReportesAsignados.Rows.Count - 1; i++)
        //            {
        //                // metodo de actualizacion de registros.
        //            }
        //            this.Session["encabemensa"] = "Sr. Usuario";
        //            this.Session["cssclass"] = "MensajesSupConfirm";
        //            this.Session["mensaje"] = "Se ha Actualizó con éxito los Informes de la campaña : " + LblTxtPresupuestoAsigReportes.Text.ToUpper();
        //            Mensajes_Reportes();
        //            ModalPanelReportes.Hide();
        //            ModalPanelReportPlan.Show();

        //        }
        //    }
        //    else
        //    {
        //        this.Session["encabemensa"] = "Sr. Usuario";
        //        this.Session["cssclass"] = "MensajesSupervisor";
        //        this.Session["mensaje"] = "Ingrese las fechas de inicio y fin por cada asignación.";
        //        Mensajes_Reportes();
        //        ModalPanelReportes.Hide();
        //        ModalPanelReportPlan.Show();
        //    }

        //}
        protected void ImgCloseReportes_Click(object sender, ImageClickEventArgs e)
        {
            ModalPanelReportPlan.Show();
        }

        protected void GVReportesAsignados_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ModalPanelReportPlan.Show();
            GVReportesAsignados.EditIndex = e.NewEditIndex;
            ConsultaReportesCampaña();
        }

        protected void GVReportesAsignados_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ModalPanelReportPlan.Show();
            GVReportesAsignados.EditIndex = -1;
            ConsultaReportesCampaña();
        }

        protected void GVReportesAsignados_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.Session["ActualizaReportesPlanning"] = 1;
            bool seguir = true;

            if (((TextBox)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("TxtFechaini")).Text == "" ||
                    ((TextBox)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("TxtFechafin")).Text == "")
            {
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Debe ingresar las fechas de inicio y fin";
                Mensajes_Reportes();
                seguir = false;

            }

            if (seguir)
            {
                try
                {
                    DateTime FechaInicial = Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("TxtFechaini")).Text + " 01:00:00.000");
                    DateTime Fechafinal = Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000");
                    if (FechaInicial > Fechafinal)
                    {
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La fecha inicial no puede ser mayor a la fecha final.";
                        Mensajes_Reportes();
                        seguir = false;
                        ModalPanelReportes.Hide();
                        ModalPanelReportPlan.Show();
                        return;
                    }

                    if (seguir)
                    {
                        if (Convert.ToString(FechaInicial.Year) != ((Label)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("LblAño")).Text
                            ||
                            Convert.ToString(Fechafinal.Year) != ((Label)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("LblAño")).Text)
                            //||
                            //Convert.ToInt32(Fechafinal.Month) != Convert.ToInt32(((Label)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("LblMes")).Text))
                        {
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "Las fechas del reporte : " + ((Label)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("LblReporte")).Text +
                                " deben ser del mismo año. Por favor verifique"; //y la fecha fin debe ser del mes de " + ((Label)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("LblMes")).Text + " Por favor verifique";
                            Mensajes_Reportes();
                            seguir = false;
                            ModalPanelReportes.Hide();
                            ModalPanelReportPlan.Show();
                            return;
                        }
                        if (seguir)
                        {
                            DAplicacion odValidacion = new DAplicacion();
                            DataTable dtValidaactualizacion = odValidacion.PermitirUpdateReportsPlanning(
                                TxtPlanningAsigReportes.Text,
                                 ((Label)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("LblReporte")).Text,
                                  ((Label)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("LblMes")).Text,
                                 Convert.ToInt32(((Label)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("LblAño")).Text),
                                 Convert.ToInt32(((Label)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("LblPeriodo")).Text));

                            if (dtValidaactualizacion != null)
                            {
                                if (dtValidaactualizacion.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= dtValidaactualizacion.Rows.Count - 1; i++)
                                    {
                                        if (Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("TxtFechaini")).Text + " 01:00:00.000") >= Convert.ToDateTime(dtValidaactualizacion.Rows[i]["ReportsPlanning_RecogerDesde"].ToString().Trim()) &&
                                            Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("TxtFechaini")).Text + " 01:00:00.000") <= Convert.ToDateTime(dtValidaactualizacion.Rows[i]["ReportsPlanning_RecogerHasta"].ToString().Trim()))
                                        {
                                            this.Session["encabemensa"] = "Sr. Usuario";
                                            this.Session["cssclass"] = "MensajesSupervisor";
                                            this.Session["mensaje"] = "Los rangos de fechas por reporte y periodo no deben coincidir. Por favor verifique";
                                            Mensajes_Reportes();
                                            seguir = false;

                                            ModalPanelReportes.Hide();
                                            ModalPanelReportPlan.Show();
                                            return;
                                        }
                                    }
                                }
                            }
                            dtValidaactualizacion = null;
                        }
                    }
                }
                catch(Exception ex)
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Debe incluir todas las fechas con formato (dd/mm/aaaa).";
                    Mensajes_Reportes();
                    seguir = false;

                    ModalPanelReportes.Hide();
                    ModalPanelReportPlan.Show();
                    return;
                }
            }


            if (seguir)
            {
                // metodo de actualizacion de registros. 

                PPlanning.Get_ActualizarReportesPlanning(Convert.ToInt32(GVReportesAsignadosDEL.Rows[GVReportesAsignados.EditIndex].Cells[0].Text),
                    Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("TxtFechaini")).Text + " 01:00:00.000"),
                    Convert.ToDateTime(((TextBox)GVReportesAsignados.Rows[GVReportesAsignados.EditIndex].Cells[0].FindControl("TxtFechafin")).Text + " 23:59:00.000"),
                    this.Session["sUser"].ToString(), DateTime.Now);



                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupConfirm";
                this.Session["mensaje"] = "Se ha Actualizó con éxito las fechas del informe seleccionado de la campaña : " + LblTxtPresupuestoAsigReportes.Text.ToUpper();
                Mensajes_Reportes();
                ModalPanelReportes.Hide();
                ModalPanelReportPlan.Show();
                GVReportesAsignados.EditIndex = -1;
                ConsultaReportesCampaña();
            }

        }

        protected void BtnCarga20_Click(object sender, ImageClickEventArgs e)
        {
            botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();

            this.Session["2080"] = "20";
            ModalPopupCarga2080.Show();
            IframeMasiva2080.Attributes["src"] = "Carga2080.aspx";
        }
        protected void BtnCarga80_Click(object sender, ImageClickEventArgs e)
        {

            botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();

            this.Session["2080"] = "80";
            ModalPopupCarga2080.Show();
            IframeMasiva2080.Attributes["src"] = "Carga2080.aspx";
        }

        protected void BtnCloseMasiva2080_Click(object sender, ImageClickEventArgs e)
        {
            botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();
            ModalPopupCarga2080.Hide();
            ModalPanelReportPlan.Show();

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
            ModalPanelReportes.Show();
        }
        protected void BtnNonePer_Click(object sender, EventArgs e)
        {
            ChklstFrecuencia.ClearSelection();
            ModalPanelReportes.Show();
        }


        #endregion

        #region en proceso de creación - Breaf
        protected void ImgIrABreaf_Click(object sender, ImageClickEventArgs e)
        {
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();
        }

        
        #endregion


        /// <summary>
        /// Evento Click del AspControl ImageButton: 'ImgBtnInformeTotal'
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        protected void ImgBtnInformeTotal_Click(object sender, ImageClickEventArgs e)
        {
            // Obtener el Valor Seleccionado del AspControl DropDownList 'CmbSelCampaña'
            // Para Obtener la 
            // Información General del Planning Seleccionado
            // Tables[0] Información General del Planning
            // Tables[1] Información General del Personal 
            // Tables[2] Información General de los Puntos de Venta
            DataSet ds = oCoon.ejecutarDataSet(
                "UP_WEBXPLORA_PLA_INFORMACIONGENERALPLANNING", 
                CmbSelCampaña.SelectedValue, 
                Convert.ToInt32(this.Session["company_id"]));

            // Llena la Grilla de la Información General del Planning
            GvInformacionPlanning.DataSource = ds.Tables[0];
            GvInformacionPlanning.DataBind();
            
            // Llena la Grilla del Staff
            ((GridView)GvInformacionPlanning.Rows[0].Cells[0]
                .FindControl("GVInfogenStaff")).DataSource = ds.Tables[1];
            ((GridView)GvInformacionPlanning.Rows[0].Cells[0]
                .FindControl("GVInfogenStaff")).DataBind();
            
            // Llena la Grilla de los Puntos de Venta
            ((GridView)GvInformacionPlanning.Rows[0].Cells[0]
                .FindControl("GVInfopdv")).DataSource = ds.Tables[2];
            ((GridView)GvInformacionPlanning.Rows[0].Cells[0]
                .FindControl("GVInfopdv")).DataBind();
        }

        protected void BtnCloseMasivaProd_Click(object sender, ImageClickEventArgs e)
        {
            ModalPanelAsignaProductos.Show();
            ModalPanelMasivaProd.Hide();
        }


        #region Gestión de niveles
        protected void ImgIrAGestionNiveles_Click(object sender, ImageClickEventArgs e)
        {
            //botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();
            string planning = this.Session["id_planning"].ToString();
            obtenerfechasplanning(planning);
            obtener_nodocomercial(planning);
            obtenerniveles();
            cargarfrecuencias();
            MP_AgregarPDVNivel.Show();
            //ConsultaReportesCampaña();
            //ConsultaGvFrecuencias();
        }

        protected void btn_gestionniveles_listacomp_Click(object sender, EventArgs e)
        {
            DataSet pdv_nivel = new DataSet();
            pdv_nivel = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_LISTA_NIVELES_PDV");
            rgv_lista_pdv_nivel.DataSource = pdv_nivel.Tables[0];
            rgv_lista_pdv_nivel.DataBind();
            pintarceldas();
            MP_GestionNiveles.Show();
        }

        protected void btn_gestionniveles_listanuev_Click(object sender, EventArgs e)
        {
            DataSet pdv_nivel = new DataSet();
            pdv_nivel = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_LISTA_NIVELES_PDV");
            rgv_lista_pdv_nivel.DataSource = pdv_nivel.Tables[1];
            rgv_lista_pdv_nivel.DataBind();
            pintarceldas();
            MP_GestionNiveles.Show();
        }

        private void obtenerfechasplanning(string planning)
        {
            if (ddl_gestionniveles_mesini.Items.Count == 0)
            {
                DataSet fplanning = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_OBTENER_FECHASPLANNING", planning);
                DateTime fec_ini = new DateTime();
                DateTime fec_fin = new DateTime();
                fec_ini = Convert.ToDateTime(fplanning.Tables[0].Rows[0]["fecini"]);//obtenemos la fecha inicial del planning
                fec_fin = Convert.ToDateTime(fplanning.Tables[0].Rows[0]["fecfin"]);//obtenemos la fecha final del planning

                if (fec_ini.Year <= DateTime.Now.Year && DateTime.Now.Year <= fec_fin.Year)// si el año actual esta dentro del periodo.
                {
                    ddl_gestionniveles_mesini.Items.Insert(0, new ListItem("Seleccione...", "0"));
                    ddl_gestionniveles_mesfin.Items.Insert(0, new ListItem("Seleccione...", "0"));
                    int indice = 1;
                    for (int x = fec_ini.Month; x <= fec_fin.Month; x++)
                    {
                        string mes = fplanning.Tables[1].Rows[x - 1]["Month_name"].ToString();
                        ddl_gestionniveles_mesini.Items.Insert(indice, new ListItem(mes, x.ToString()));
                        ddl_gestionniveles_mesfin.Items.Insert(indice, new ListItem(mes, x.ToString()));
                        indice++;
                    }
                }
            }
        }

        private void obtener_nodocomercial(string id_planning)
        {
            DataTable nodocomercial = new DataTable();
            nodocomercial = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_AGRUPACIONCOMMERCIAL_PLANNING", id_planning);
            ddl_gestionniveles_nodo.DataSource = nodocomercial;
            ddl_gestionniveles_nodo.DataTextField = "commercialNodeName";
            ddl_gestionniveles_nodo.DataValueField = "NodeCommercial";
            ddl_gestionniveles_nodo.DataBind();
            ddl_gestionniveles_nodo.Items.Insert(0, new ListItem("Seleccione...", "0"));
            MP_AgregarPDVNivel.Show();
        }

        private void obtener_pdvniveles(string id_planning, int nodo)
        {
            DataTable pdv_nivel = new DataTable();
            pdv_nivel = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_PTOVENTA_PLANNING", id_planning, nodo);
            ddl_gestionniveles_pdv.DataSource = pdv_nivel;
            ddl_gestionniveles_pdv.DataTextField = "pdv_Name";
            ddl_gestionniveles_pdv.DataValueField = "id_MPOSPlanning";
            ddl_gestionniveles_pdv.DataBind();
            ddl_gestionniveles_pdv.Items.Insert(0, new ListItem("Seleccione...", "0"));
            MP_AgregarPDVNivel.Show();
        }

        protected void ddl_gestionniveles_nodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string planning = this.Session["id_planning"].ToString();
            obtener_pdvniveles(planning, Convert.ToInt32(ddl_gestionniveles_nodo.Text));
            MP_AgregarPDVNivel.Show();
        }

        private void obtenerniveles()
        {
            DataTable pdv_nivel = new DataTable();
            pdv_nivel = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENERNIVELES");
            ddl_gestionniveles_nivel.DataSource = pdv_nivel;
            ddl_gestionniveles_nivel.DataTextField = "Segment_Name";
            ddl_gestionniveles_nivel.DataValueField = "id_Segment";
            ddl_gestionniveles_nivel.DataBind();
            ddl_gestionniveles_nivel.Items.Insert(0, new ListItem("Seleccione...", "0"));
            MP_AgregarPDVNivel.Show();
        }

        protected void btn_gestionniveles_crear_Click(object sender, EventArgs e)
        {
            ddl_gestionniveles_nivel.Enabled = true;
            ddl_gestionniveles_pdv.Enabled = true;
            ddl_gestionniveles_nodo.Enabled = true;
            ddl_gestionniveles_mesini.Enabled = true;
            ddl_gestionniveles_mesfin.Enabled = true;
            ddl_gestionniveles_frecuencia.Enabled = true;
            btn_gestionniveles_guardar.Visible = true;
            MP_AgregarPDVNivel.Show();
        }

        protected void btn_salir_gestionniveles_Click(object sender, EventArgs e)
        {
            MP_AgregarPDVNivel.Show();
        }
        protected void btn_gestionniveles_salir_Click(object sender, EventArgs e)
        {
            ddl_gestionniveles_nivel.Items.Clear();
            ddl_gestionniveles_pdv.Items.Clear();
            ddl_gestionniveles_nodo.Items.Clear();
            ddl_gestionniveles_mesini.Items.Clear();
            ddl_gestionniveles_mesfin.Items.Clear();
            ddl_gestionniveles_frecuencia.Items.Clear();
            btn_gestionniveles_guardar.Visible = false;
        }

        protected void btn_gestionniveles_guardar_Click(object sender, EventArgs e)
        {
            string id_nivel = ddl_gestionniveles_nivel.Text;
            int MPOPlanning = Convert.ToInt32(ddl_gestionniveles_pdv.Text);
            string mes_ini = ddl_gestionniveles_mesini.Text;
            string mes_fin = ddl_gestionniveles_mesfin.Text;
            string frecuencia = ddl_gestionniveles_frecuencia.Text;
            string usuario = this.Session["sUser"].ToString().Trim();
            try
            {
                oCoon.ejecutarDataTable("UP_WEBXPLORA_REGISTRAR_PDV_NIVEL", id_nivel, MPOPlanning, mes_ini, mes_fin, frecuencia, usuario);
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupConfirm";
                this.Session["mensaje"] = "Se ha guardado con éxito el registro para el Pto. de Venta: " + ddl_gestionniveles_pdv.SelectedValue.ToUpper();
                Mensajes_Seguimiento();
            }
            catch
            {
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Error no se pudo registrar el nivel para el Pto. de Venta seleccionado.";
                Mensajes_ReportesDel();
            }
        }

        private void cargarfrecuencias()
        {
            //ddl_gestionniveles_frecuencia
            DataTable pdv_nivel = new DataTable();
            pdv_nivel = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTIENEFRECUENCIAS");
            ddl_gestionniveles_frecuencia.DataSource = pdv_nivel;
            ddl_gestionniveles_frecuencia.DataTextField = "Txt_Frecuencia";
            ddl_gestionniveles_frecuencia.DataValueField = "id_Frecuencia";
            ddl_gestionniveles_frecuencia.DataBind();
            ddl_gestionniveles_frecuencia.Items.Insert(0, new ListItem("Seleccione...", "0"));
            MP_AgregarPDVNivel.Show();
        }

        protected void pintarceldas()
        {
            foreach (GridViewRow gvr in rgv_lista_pdv_nivel.Rows)
            {
                TableCell evaluacion = gvr.Cells[8];
                if (evaluacion.Text == "BAJA NIVEL")
                    evaluacion.BackColor = System.Drawing.Color.Red; // This will make row back color red

                if (evaluacion.Text == "SUBE NIVEL")
                    evaluacion.BackColor = System.Drawing.Color.Green; // This will make row back color green

                if (evaluacion.Text == "MANTIENE NIVEL")
                    evaluacion.BackColor = System.Drawing.Color.Yellow; // This will make row back color yellow            
            }
        }

        protected void rgv_lista_pdv_nivel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = rgv_lista_pdv_nivel.Rows[index];

                if (e.CommandName == "DETALLE")
                {
                    int cod_mopsplanning = Convert.ToInt32(row.Cells[1].Text);
                    if (cod_mopsplanning != 0)
                    {
                        //id_planivel, id_MPOSPlanning, Mes_ini, Mes_Fin, id_Segment, id_Evaluacion, id_Frecuencia
                        DataTable detalle = null;
                        Conexion Ocoon = new Conexion();
                        detalle = Ocoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENERDETALLEPDVNIVEL", cod_mopsplanning);
                        gv_detalle_pdv_nivel.DataSource = detalle;
                        gv_detalle_pdv_nivel.DataBind();
                        detalle = null;
                        Ocoon = null;
                    }
                    MP_GestionNiveles.Show();
                    MP_Detalle.Show();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void btn_detalleniveles_salir_Click(object sender, EventArgs e)
        {
            MP_GestionNiveles.Show();
            MP_AgregarPDVNivel.Show();
        }


        #endregion

        


        protected void ImgIrAGestionFuerzaVenta_Click(object sender, ImageClickEventArgs e)
        {
            //botonregresar.Visible = false;
            Postback = false;
            this.Session["Postback"] = false;
            InicializarPaneles();
            string planning = this.Session["id_planning"].ToString();
            cargar_tipo_contacto();
            cargar_fuerza_venta();
            MP_P_FuerzaVenta.Show();
        }

        private void cargar_tipo_contacto() 
        {
            //UP_WEBXPLORA_PLA_OBTENER_TIPO_CONTACTO_PDV
            DataTable contacto_tipo = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_TIPO_CONTACTO_PDV");
            if (contacto_tipo.Rows.Count > 0)
            {
                ddl_fuerzaventa_tipo.DataSource = contacto_tipo;
                ddl_fuerzaventa_tipo.DataValueField = "id_PointOfSale_Contact_Type";
                ddl_fuerzaventa_tipo.DataTextField = "pdv_contact_type_description";
                ddl_fuerzaventa_tipo.DataBind();
                ddl_fuerzaventa_tipo.Items.Insert(0, new ListItem("Seleccione...", "0"));
            }
        }

        private void cargar_fuerza_venta() 
        {
            DataTable dt_fuerza_venta = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_LISTA_FUERZAVENTA");
            if (dt_fuerza_venta.Rows.Count > 0)
            {
                gv_gestion_fuerzaventa.DataSource = dt_fuerza_venta;
                gv_gestion_fuerzaventa.DataBind();
            }
        }

        //btn_fuerzaventa_guardar
 
        protected void btn_fuerzaventa_guardar_Click1(object sender, EventArgs e)
        {
            if (txt_fuerzaventa_nombre.Text.Equals("") || ddl_fuerzaventa_tipo.SelectedIndex == 0)
            {
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Debe ingresar un nombre y seleccionar el tipo de contacto.";
                Mensajes_ReportesDel();
            }
            else
            {
                int tipo_contacto = Convert.ToInt32(ddl_fuerzaventa_tipo.Text);
                string nombre = txt_fuerzaventa_nombre.Text;
                try
                {
                    DataTable registra = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_REGISTRAR_CONTACT_PDV", tipo_contacto, nombre);
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "El Contacto de PDV se ha registrado correctamente.";
                    Mensajes_ReportesDel();
                }
                catch (Exception ex)
                {
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Error no se pudo registrar el Contacto de PDV.";
                    Mensajes_ReportesDel();
                }
                cargar_fuerza_venta();
                txt_fuerzaventa_nombre.Text = "";
                ddl_fuerzaventa_tipo.SelectedIndex = 0;
                MP_P_FuerzaVenta.Show();
            }
        }


            #region Producto Ancla

        protected void ImgProdAncla_Click(object sender, ImageClickEventArgs e)
        {

           
            DataSet ds = new DataSet();
            //ds = PPlanning.Get_PlanningCreados(CmbSelCampaña.SelectedValue);
            ds = (DataSet)this.ViewState["planning_creados"];
            if (ds.Tables[11].Rows.Count > 0)
            {
                this.Session["id_planning"] = ds.Tables[9].Rows[0]["id_planning"].ToString().Trim();
                this.Session["Planning_Name"] = ds.Tables[9].Rows[0]["Planning_Name"].ToString().Trim();
                DataTable dtCliente = Presupuesto.Get_ObtenerClientes(ds.Tables[9].Rows[0]["Planning_Budget"].ToString().Trim(), 1);
                this.Session["company_id"] = dtCliente.Rows[0]["Company_id"].ToString().Trim();
                this.Session["Planning_CodChannel"] = ds.Tables[9].Rows[0]["Planning_CodChannel"].ToString().Trim();
                dtCliente = null;
            }
            llenaReportes();
            LlenacomboBuscarClienteProductAncla();
            ModalPopancla.Show();
       

        }
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos owsadministrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Conexion cn = new Conexion();
        private void LlenacomboClienteProductAncla()
        {
            DataSet ds = null;
           ds =cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", 0, "0", 0, 0, "0");
            //se llena cliente en producto Ancla
            
            cmbClienteAncla.DataSource = ds.Tables[0];
            cmbClienteAncla.DataTextField = "Company_Name";
            cmbClienteAncla.DataValueField = "Company_id";
            cmbClienteAncla.DataBind();
            cmbClienteAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;

            cmbClienteAncla.SelectedValue = this.Session["company_id"].ToString();
            comboOficinaXclienteenPancla();
            cmbClienteAncla.Enabled = false;
            ModalPopancla.Show();
        }

        private void comboOficinaXclienteenPancla()
        {
            DataSet ds = null;

            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOOFICINAPANCLA", cmbClienteAncla.SelectedValue);
            //se llena cliente en Usuarios
            cmbOficinaPancla.DataSource = ds;
            cmbOficinaPancla.DataTextField = "Name_Oficina";
            cmbOficinaPancla.DataValueField = "cod_Oficina";
            cmbOficinaPancla.DataBind();
            ModalPopancla.Show();
        }

        private void LlenacomboCategoProductAncla()
        {
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(cmbClienteAncla.SelectedValue), "0", 0, 0, "0");
            //llena categoria segun cliente
            cmbCategoryAncla.DataSource = ds.Tables[1];
            cmbCategoryAncla.DataTextField = "Product_Category";
            cmbCategoryAncla.DataValueField = "id_ProductCategory";
            cmbCategoryAncla.DataBind();
            cmbCategoryAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
            ModalPopancla.Show();
        }

        private void LlenaSubporCategoProductAncla()
        {
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(cmbClienteAncla.SelectedValue), cmbCategoryAncla.SelectedValue, 0, 0, "0");
            //se llena Combo subcategoria segun categoria y cliente
            cmbSubcateAncla.DataSource = ds.Tables[2];
            cmbSubcateAncla.DataTextField = "Name_Subcategory";
            cmbSubcateAncla.DataValueField = "id_Subcategory";
            cmbSubcateAncla.DataBind();

            if (cmbSubcateAncla.Items.Count > 0)
            {
                cmbSubcateAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {
                cmbSubcateAncla.Items.Clear();
                cmbSubcateAncla.Items.Insert(0, new ListItem("<No Aplica>", "n"));
                cmbSubcateAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            ModalPopancla.Show();
        }
        private void LlenaMarcaenAnclaconSubcategoria()
        {
            string ssubCategoryP = "";
            if (cmbSubcateAncla.Text == "n")
            {
                ssubCategoryP = "0";
            }
            else
            {
                ssubCategoryP = cmbSubcateAncla.Text;
            }
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(cmbClienteAncla.SelectedValue), cmbCategoryAncla.SelectedValue, Convert.ToInt64(ssubCategoryP), 0, "0");
            //se llena marcas segun subcategoria y cliente
            cmbMarcaAncla.DataSource = ds.Tables[4];
            cmbMarcaAncla.DataTextField = "Name_Brand";
            cmbMarcaAncla.DataValueField = "id_Brand";
            cmbMarcaAncla.DataBind();
            cmbMarcaAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            ds = null;
            ModalPopancla.Show();
        }

        private void LlenaProductoenAncla()
        {
            string ssubCategoryP = "";
            if (cmbSubcateAncla.Text == "n")
            {
                ssubCategoryP = "0";
            }
            else
            {
                ssubCategoryP = cmbSubcateAncla.Text;
            }
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(cmbClienteAncla.SelectedValue), cmbCategoryAncla.SelectedValue, Convert.ToInt64(ssubCategoryP), Convert.ToInt32(cmbMarcaAncla.SelectedValue), "0");

            cmbproductAncla.DataSource = ds.Tables[5];
            cmbproductAncla.DataTextField = "Product_Name";
            cmbproductAncla.DataValueField = "cod_Product";
            cmbproductAncla.DataBind();
            cmbproductAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
            ModalPopancla.Show();
        }


        protected void cmbClienteAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = null;

            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOOFICINAPANCLA", cmbClienteAncla.SelectedValue);
            //se llena cliente en Usuarios
            cmbOficinaPancla.DataSource = ds;
            cmbOficinaPancla.DataTextField = "Name_Oficina";
            cmbOficinaPancla.DataValueField = "cod_Oficina";
            cmbOficinaPancla.DataBind();
            ModalPopancla.Show();
        }

        protected void cmbOficinaPancla_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(cmbClienteAncla.SelectedValue), "0", 0, 0, "0");
            //llena categoria segun cliente
            cmbCategoryAncla.DataSource = ds.Tables[1];
            cmbCategoryAncla.DataTextField = "Product_Category";
            cmbCategoryAncla.DataValueField = "id_ProductCategory";
            cmbCategoryAncla.DataBind();
            cmbCategoryAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
            ModalPopancla.Show();
        }

        protected void cmbCategoryAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(cmbClienteAncla.SelectedValue), cmbCategoryAncla.SelectedValue, 0, 0, "0");
            //se llena Combo subcategoria segun categoria y cliente
            cmbSubcateAncla.DataSource = ds.Tables[2];
            cmbSubcateAncla.DataTextField = "Name_Subcategory";
            cmbSubcateAncla.DataValueField = "id_Subcategory";
            cmbSubcateAncla.DataBind();

            if (cmbSubcateAncla.Items.Count > 0)
            {
                cmbSubcateAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {
                cmbSubcateAncla.Items.Clear();
                cmbSubcateAncla.Items.Insert(0, new ListItem("<No Aplica>", "n"));
                cmbSubcateAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            ModalPopancla.Show();
        }

        protected void cmbSubcateAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ssubCategoryP = "";
            if (cmbSubcateAncla.Text == "n")
            {
                ssubCategoryP = "0";
            }
            else
            {
                ssubCategoryP = cmbSubcateAncla.Text;
            }
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(cmbClienteAncla.SelectedValue), cmbCategoryAncla.SelectedValue, Convert.ToInt64(ssubCategoryP), 0, "0");
            //se llena marcas segun subcategoria y cliente
            cmbMarcaAncla.DataSource = ds.Tables[4];
            cmbMarcaAncla.DataTextField = "Name_Brand";
            cmbMarcaAncla.DataValueField = "id_Brand";
            cmbMarcaAncla.DataBind();
            cmbMarcaAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            ds = null;
            ModalPopancla.Show();
        }

        protected void cmbMarcaAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ssubCategoryP = "";
            if (cmbSubcateAncla.Text == "n")
            {
                ssubCategoryP = "0";
            }
            else
            {
                ssubCategoryP = cmbSubcateAncla.Text;
            }
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(cmbClienteAncla.SelectedValue), cmbCategoryAncla.SelectedValue, Convert.ToInt64(ssubCategoryP), Convert.ToInt32(cmbMarcaAncla.SelectedValue), "0");

            cmbproductAncla.DataSource = ds.Tables[5];
            cmbproductAncla.DataTextField = "Product_Name";
            cmbproductAncla.DataValueField = "cod_Product";
            cmbproductAncla.DataBind();
            cmbproductAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
            ModalPopancla.Show();
        }

        protected void cmbproductAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(cmbClienteAncla.SelectedValue), "0", 0, 0, cmbproductAncla.SelectedValue);
            TxtPrecioprodAncla.Text = ds.Tables[6].Rows[0][0].ToString().Trim();
            //this.Session["Company_id"] = ds.Tables[6][""].ToString().Trim();
           // ds = null;
            //DataSet dst = null;
            //ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(cmbClienteAncla.SelectedValue), "0", 0, 0, cmbproductAncla.SelectedValue);
            TxtPesoPancla.Text = ds.Tables[7].Rows[0][0].ToString().Trim();
            //this.Session["Company_id"] = ds.Tables[6][""].ToString().Trim();
            ds = null;
            ModalPopancla.Show();
        }

        PLA_ProductosAncla oPLA_ProductosAncla = new PLA_ProductosAncla();

        protected void BtnGuardarAncla_Click(object sender, EventArgs e)
        {

            string faltantes = "";

            if (cmbClienteAncla.Text == "0" || cmbCategoryAncla.Text == "0" || cmbSubcateAncla.Text == "0" || cmbMarcaAncla.Text == "0" || cmbproductAncla.Text == "0" || cmbOficinaPancla.Text == "0")
            {
                if (cmbClienteAncla.Text == "0")
                {
                    faltantes  = ". " + "Cliente";
                }
                if (cmbCategoryAncla.Text == "0")
                {
                    faltantes = faltantes + ". " + "Categoria";
                }
                if (cmbSubcateAncla.Text == "0")
                {
                    faltantes = faltantes + ". " + "Categoria";
                }
                if (cmbMarcaAncla.Text == "0")
                {
                    faltantes = ". " + "Marca";
                }
                if (cmbproductAncla.Text == "0")
                {
                    faltantes = ". " + "Producto";
                }
                if (cmbOficinaPancla.Text == "0")
                {
                    faltantes = ". " + "Oficina";
                }
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Debe ingresar todos los campos con (*): " + faltantes;
                Mensajes_ProductoAncla();

                return;
            }

            try
            {
                string ssubCategoryP2 = "";
                if (cmbSubcateAncla.Text == "n")
                {
                    ssubCategoryP2 = "0";
                }
                else
                {
                    ssubCategoryP2 = cmbSubcateAncla.Text;
                }

                

                DAplicacion odconsultaPAncla = new DAplicacion();
                DataTable dtconsulta = oPLA_ProductosAncla.ConsultarPancla(Convert.ToInt32(cmbClienteAncla.Text), cmbCategoryAncla.Text, Convert.ToInt64(cmbOficinaPancla.Text),cmbSubcateAncla.SelectedValue,Convert.ToInt32(ddlPeriodoProdAncla.SelectedValue));
                if (dtconsulta.Rows.Count == 0 )
                {
                    string ssubCategoryP = "";
                    if (cmbSubcateAncla.Text == "n")
                    {
                        ssubCategoryP = "0";
                    }
                    else
                    {
                        ssubCategoryP = cmbSubcateAncla.Text;
                    }
                    EPLA_ProductosAncla oePAncla = oPLA_ProductosAncla.RegistrarPAncla(Convert.ToInt32(cmbClienteAncla.Text),Convert.ToInt32(ddlPeriodoProdAncla.SelectedValue), cmbCategoryAncla.Text, Convert.ToInt64(ssubCategoryP), cmbproductAncla.Text, Convert.ToInt64(cmbOficinaPancla.Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    EProductos oeaPAncla = oPLA_ProductosAncla.Actualizar_PrecioLista(cmbproductAncla.Text, Convert.ToDecimal(TxtPrecioprodAncla.Text), Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sCodProduct = "";
                    sCodProduct = cmbproductAncla.SelectedItem.Text;
                    this.Session["sCodProduct"] = sCodProduct;
                    SavelimpiarControlesProductAncla();
                    //llenarcombos();


                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "El Producto Ancla " + this.Session["sCodProduct"] + " fue creado con Exito";
                    Mensajes_ProductoAncla();


                    saveActivarbotonesProductAncla();
                    desactivarControlesProductAncla();

                }
                else
                {
                    string sCodProduct = "";
                    sCodProduct = cmbproductAncla.SelectedItem.Text;
                    this.Session["sCodProduct"] = sCodProduct;

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "El Producto Ancla " + this.Session["sCodProduct"] + " Ya Existe";
                    Mensajes_ProductoAncla();

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

        private void desactivarControlesProductAncla()
        {
            cmbClienteAncla.Enabled = false;
            cmbOficinaPancla.Enabled = false;
            cmbCategoryAncla.Enabled = false;
            cmbSubcateAncla.Enabled = false;
            cmbproductAncla.Enabled = false;
            cmbMarcaAncla.Enabled = false;
            TxtPrecioprodAncla.Enabled = false;

        }

        private void saveActivarbotonesProductAncla()
        {
            BtnCrearAncla.Visible = true;
            BtnGuardarAncla.Visible = false;
            BtnConsultarAncla.Visible = true;
            BtnCancelarAncla.Visible = true;

        }

        private void SavelimpiarControlesProductAncla()
        {
            cmbClienteAncla.Text = "0";
            cmbOficinaPancla.Text = "0";
            cmbCategoryAncla.Text = "0";
            cmbSubcateAncla.Text = "0";
            cmbproductAncla.Text = "0";
            cmbMarcaAncla.Text = "0";
            TxtPrecioprodAncla.Text = "";
            TxtPesoPancla.Text = "";

        }

        protected void BtnCrearAncla_Click(object sender, EventArgs e)
        {
          
                LlenacomboClienteProductAncla();
                crearActivarbotonesProductAncla();
                activarControlesProductAncla();
                ModalPopancla.Show();

            
        }
        private void crearActivarbotonesProductAncla()
        {

            BtnCrearAncla.Visible = false;
            BtnGuardarAncla.Visible = true;
            BtnConsultarAncla.Visible = false;
            BtnCancelarAncla.Visible = true;
        }
        private void activarControlesProductAncla()
        {
           // cmbClienteAncla.Enabled = true;
            cmbOficinaPancla.Enabled = true;
            cmbCategoryAncla.Enabled = true;
            cmbSubcateAncla.Enabled = true;
            cmbproductAncla.Enabled = true;
            cmbMarcaAncla.Enabled = true;
            TxtPrecioprodAncla.Enabled = true;


        }

        private void llenaReportes()
        {
            DataSet ds = cn.ejecutarDataSet("UP_WEBXPLORA_PLA_CONSULTA_INFORMESYMESES_PLANNING", this.Session["id_planning"].ToString().Trim(), Convert.ToInt32(this.Session["company_id"]), this.Session["Planning_CodChannel"].ToString().Trim(), 0);
            ddlReporteProdAncla.DataSource = ds.Tables[1];
            ddlReporteProdAncla.DataValueField = "Report_id";
            ddlReporteProdAncla.DataTextField = "Report_NameReport";
            ddlReporteProdAncla.DataBind();
            ds = null;
        }

        protected void ddlmesProdAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarPeriodos(ddlPeriodoProdAncla, this.Session["id_planning"].ToString(), Convert.ToInt32(ddlReporteProdAncla.SelectedValue), ddlAño.SelectedValue, ddlmes.SelectedValue);
            ModalPopancla.Show();
        }

        private void LlenacomboBuscarClienteProductAncla()
        {
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_PLA_LLENACOMBOSCONSULTASPRODUCTANCLA", 0);
            
            //se llena cliente enconsulta en productos Ancla
            CmbBClientePAncla.DataSource = ds.Tables[0];
            CmbBClientePAncla.DataTextField = "Company_Name";
            CmbBClientePAncla.DataValueField = "Company_id";
            CmbBClientePAncla.DataBind();
            CmbBClientePAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));


            CmbBClientePAncla.SelectedValue = this.Session["company_id"].ToString();

            LlenacomboBuscarCategoryProductAncla();

            ds = null;
        }

        private void LlenacomboBuscarCategoryProductAncla()
        {
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_PLA_LLENACOMBOSCONSULTASPRODUCTANCLA", Convert.ToInt32(CmbBClientePAncla.SelectedValue));
            //se llena categorya en busqueda enconsulta en productos Ancla
            CmbBCategoriaPAncla.DataSource = ds.Tables[1];
            CmbBCategoriaPAncla.DataTextField = "Product_Category";
            CmbBCategoriaPAncla.DataValueField = "id_ProductCategory";
            CmbBCategoriaPAncla.DataBind();
            CmbBCategoriaPAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
           // ModalPopancla.Show();
            //ModalPopup_BPAncla.Show();
        }
        private void llenaComboOficinaConsultaPancla()
        {
            DataSet ds = null;

            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOOFICINACONSULTARPANCLA", CmbBClientePAncla.SelectedValue, CmbBCategoriaPAncla.SelectedValue);
            //se llena cliente en Usuarios
            cmbOficinaBPancla.DataSource = ds;
            cmbOficinaBPancla.DataTextField = "Name_Oficina";
            cmbOficinaBPancla.DataValueField = "cod_Oficina";
            cmbOficinaBPancla.DataBind();
            cmbOficinaBPancla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ModalPopancla.Show();
            ModalPopup_BPAncla.Show();
        }

        protected void CmbBClientePAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenacomboBuscarCategoryProductAncla();
           // ModalPopup_BPAncla.Show();
        }

        protected void CmbBCategoriaPAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaComboOficinaConsultaPancla();
            //ModalPopup_BPAncla.Show();
        }

        protected void btnaceptarProductoAncla_Click(object sender, EventArgs e)
        {

            MensajemodalPProductoAncla.Hide();
            ModalPopancla.Show();

        }


        private void BuscarActivarbotnesProductAncla()
        {
            BtnCrearAncla.Visible = false;
            BtnGuardarAncla.Visible = false;
            BtnConsultarAncla.Visible = true;
            BtnCancelarAncla.Visible = true;

        }

        private void LlenacomboGVClienteProductAncla(int i)
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPAncla(0, "0", 0, 0, "0");

            //se llena cliente en producto Ancla
            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).DataSource = ds;
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).DataTextField = "Company_Name";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).DataValueField = "Company_id";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).DataBind();
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {

            }
            ds = null;
        }

        private void comboOficinaXGVclienteenPancla(int i)
        {
            DataSet ds = null;

            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOOFICINAPANCLA", Convert.ToInt32(((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).SelectedValue));
            //se llena cliente en Usuarios


            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[1].FindControl("cmboficipancla")).DataSource = ds;
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[1].FindControl("cmboficipancla")).DataTextField = "Name_Oficina";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[1].FindControl("cmboficipancla")).DataValueField = "cod_Oficina";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[1].FindControl("cmboficipancla")).DataBind();

            }
            else
            {

            }

        }
        private void LlenacomboGVCategoProductAncla(int i)
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPAncla(Convert.ToInt32(((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).SelectedValue), "0", 0, 0, "0");
            //llena categoria segun cliente
            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).DataSource = ds.Tables[1];
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).DataTextField = "Product_Category";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).DataValueField = "id_ProductCategory";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).DataBind();
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {

            }
            ds = null;
        }

        private void LlenaGVSubporCategoProductAncla(int i)
        {
            DataSet ds = null;

            ds = owsadministrativo.llenaCombosPAncla(Convert.ToInt32(((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).SelectedValue), ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).SelectedValue, 0, 0, "0");
            //se llena Combo subcategoria segun categoria y cliente

            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).DataSource = ds.Tables[2];
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).DataTextField = "Name_Subcategory";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).DataValueField = "id_Subcategory";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).DataBind();
                //((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {

            }

            if (((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Items.Count > 0)
            {
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Items.Clear();
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Items.Insert(0, new ListItem("<No Aplica>", "n"));
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
        }
        private void LlenaMarcaGVenAnclaconSubcategoria(int i)
        {
            string ssubCategoryP = "";
            if (((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Text == "n")
            {
                ssubCategoryP = "0";
            }
            else
            {
                ssubCategoryP = ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Text;
            }
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPAncla(Convert.ToInt32(((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).SelectedValue), ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).SelectedValue, Convert.ToInt64(ssubCategoryP), 0, "0");
            //se llena marcas segun subcategoria y cliente
            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[4].FindControl("cmbmarcapancla")).DataSource = ds.Tables[4];
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[4].FindControl("cmbmarcapancla")).DataTextField = "Name_Brand";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[4].FindControl("cmbmarcapancla")).DataValueField = "id_Brand";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[4].FindControl("cmbmarcapancla")).DataBind();
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[4].FindControl("cmbmarcapancla")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {

            }

            ds = null;

        }
        private void LlenaGVProductoenAncla(int i)
        {
            string ssubCategoryP = "";
            if (((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Text == "n")
            {
                ssubCategoryP = "0";
            }
            else
            {
                ssubCategoryP = ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Text;
            }
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPAncla(Convert.ToInt32(((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).SelectedValue), ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).SelectedValue, Convert.ToInt64(ssubCategoryP), Convert.ToInt32(((DropDownList)GVConsultaPancla.Rows[i].Cells[4].FindControl("cmbmarcapancla")).SelectedValue), "0");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).DataSource = ds.Tables[5];
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).DataTextField = "Product_Name";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).DataValueField = "cod_Product";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).DataBind();
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {

            }


            ds = null;

        }
        private void LlenaGVPrecioPAncla(int i)
        {

            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPAncla(Convert.ToInt32(((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).SelectedValue), "0", 0, 0, ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).SelectedValue);
            TxtPrecioprodAncla.Text = ds.Tables[6].Rows[0][0].ToString().Trim();
            //this.Session["Company_id"] = ds.Tables[6][""].ToString().Trim();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ((TextBox)GVConsultaPancla.Rows[i].Cells[6].FindControl("cmbsubpreciopancla")).Text = ds.Tables[6].Rows[0][0].ToString().Trim();

            }
            else
            {

            }
            ds = null;


        }

        private void LlenaGVPesoPAncla(int i)
        {

            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPAncla(Convert.ToInt32(((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).SelectedValue), "0", 0, 0, ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).SelectedValue);
            //TxtPesoPancla.Text = ds.Tables[7].Rows[0][0].ToString().Trim();
            //this.Session["Company_id"] = ds.Tables[6][""].ToString().Trim();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ((TextBox)GVConsultaPancla.Rows[i].Cells[7].FindControl("cmbsubpesopancla")).Text = ds.Tables[7].Rows[0][0].ToString().Trim();

            }
            else
            {

            }
            ds = null;

        }
        protected void btnBProductAncla_Click(object sender, EventArgs e)
        {
            ModalPopup_BPAncla.Hide();
            desactivarControlesProductAncla();

           string sbcliente ;
           string sCategoria ;
           string soficina ;
           long iid_pancla;

            if (CmbBClientePAncla.Text == "0" || CmbBCategoriaPAncla.Text == "0")
            {

                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Ingrese parametros de consulta minimo Cliente y Categoria";
                Mensajes_ProductoAncla();

                ModalPopup_BPAncla.Show();
                return;
            }

            BuscarActivarbotnesProductAncla();
            sbcliente = CmbBClientePAncla.Text;
            sCategoria = CmbBCategoriaPAncla.Text;
            soficina = cmbOficinaBPancla.Text;
            CmbBClientePAncla.Text = "0";
            CmbBCategoriaPAncla.Text = "0";


            this.Session["sbcliente"] = sbcliente;
            this.Session["sCategoria"] = sCategoria;
            this.Session["soficina"] = soficina;

            DataTable oePancla = oPLA_ProductosAncla.ConsultarPancla(Convert.ToInt32(sbcliente), sCategoria, Convert.ToInt64(soficina),"0",0);
            this.Session["tPancla"] = oePancla;
            if (oePancla != null)
            {
                if (oePancla.Rows.Count > 0)
                {
                    GVConsultaPancla.DataSource = oePancla;
                    GVConsultaPancla.DataBind();
                    ModalPopancla.Show();
                    ProdAnclaConsultar.Visible = true;

                    ProdAnclaCrear.Visible = false;
                    BtnCrearAncla.Visible = false;
                    BtnGuardarAncla.Visible = false;
                    BtnConsultarAncla.Visible = false;
                    BtnCancelarAncla.Visible = false;
                  

                    for (int i = 0; i <= oePancla.Rows.Count - 1; i++)
                    {
                        try
                        {
                            this.Session["id_pancla"] = Convert.ToInt64(oePancla.Rows[0]["id_pancla"].ToString().Trim());
                            iid_pancla = Convert.ToInt64(this.Session["id_pancla"]);
                            LlenacomboGVClienteProductAncla(i);
                            ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).Text = oePancla.Rows[i][1].ToString().Trim();
                            comboOficinaXGVclienteenPancla(i);
                            ((DropDownList)GVConsultaPancla.Rows[i].Cells[1].FindControl("cmboficipancla")).Text = oePancla.Rows[i][12].ToString().Trim();
                            LlenacomboGVCategoProductAncla(i);
                            ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).Text = oePancla.Rows[i][2].ToString().Trim();
                            LlenaGVSubporCategoProductAncla(i);
                            ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Text = oePancla.Rows[i][3].ToString().Trim();
                            LlenaMarcaGVenAnclaconSubcategoria(i);
                            ((DropDownList)GVConsultaPancla.Rows[i].Cells[4].FindControl("cmbmarcapancla")).Text = oePancla.Rows[i][11].ToString().Trim();
                            LlenaGVProductoenAncla(i);
                            ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).Text = oePancla.Rows[i][4].ToString().Trim();
                            LlenaGVPrecioPAncla(i);
                            LlenaGVPesoPAncla(i);
                        }
                        catch (Exception ex) { }

                    }
                    this.Session["Exportar_Excel"] = "Exportar_Pancla";

                    DataTable dtnameCombosPancla = new DataTable();
                    dtnameCombosPancla.Columns.Add("Cliente", typeof(String));
                    dtnameCombosPancla.Columns.Add("Oficina", typeof(String));
                    dtnameCombosPancla.Columns.Add("Categoria", typeof(String));
                    dtnameCombosPancla.Columns.Add("Subcategoria", typeof(String));
                    dtnameCombosPancla.Columns.Add("Marca", typeof(String));
                    dtnameCombosPancla.Columns.Add("Producto", typeof(String));
                    dtnameCombosPancla.Columns.Add("Precio", typeof(String));
                    dtnameCombosPancla.Columns.Add("Peso", typeof(String));
                    dtnameCombosPancla.Columns.Add("Estado", typeof(String));



                    for (int i = 0; i <= GVConsultaPancla.Rows.Count - 1; i++)
                    {
                        try
                        {
                            DataRow dr = dtnameCombosPancla.NewRow();
                            dr["Cliente"] = ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).SelectedItem.Text;
                            dr["Oficina"] = ((DropDownList)GVConsultaPancla.Rows[i].Cells[1].FindControl("cmboficipancla")).SelectedItem.Text;
                            dr["Categoria"] = ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).SelectedItem.Text;
                            dr["Subcategoria"] = ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).SelectedItem.Text;
                            dr["Marca"] = ((DropDownList)GVConsultaPancla.Rows[i].Cells[4].FindControl("cmbmarcapancla")).SelectedItem.Text;
                            dr["Producto"] = ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).SelectedItem.Text;
                            dr["Precio"] = ((TextBox)GVConsultaPancla.Rows[i].Cells[6].FindControl("cmbsubpreciopancla")).Text;
                            dr["Peso"] = ((TextBox)GVConsultaPancla.Rows[i].Cells[7].FindControl("cmbsubpesopancla")).Text;
                            dr["Estado"] = ((CheckBox)GVConsultaPancla.Rows[i].Cells[8].FindControl("Checkpancla")).Checked;

                            dtnameCombosPancla.Rows.Add(dr);
                        }
                        catch (Exception ex) { }
                    }

                    this.Session["CExporPancla"] = dtnameCombosPancla;

                    //this.Session["id_pancla"] = Convert.ToInt64(oePancla.Rows[0]["id_pancla"].ToString().Trim());
                    //iid_pancla = Convert.ToInt64(this.Session["id_pancla"]);
                    //LlenacomboClienteProductAncla();
                    //cmbClienteAncla.Text=oePancla.Rows[0]["Company_id"].ToString().Trim();
                    //LlenacomboCategoProductAncla();                
                    //cmbCategoryAncla.Text=oePancla.Rows[0]["id_ProductCategory"].ToString().Trim();
                    //LlenaSubporCategoProductAncla();
                    //if (cmbSubcateAncla.Items[1].Text == "<No Aplica>")
                    //{
                    //    cmbSubcateAncla.SelectedIndex = 1;

                    //}
                    //else
                    //{
                    //    cmbSubcateAncla.Text = oePancla.Rows[0]["id_Subcategory"].ToString().Trim();

                    //}
                    //LlenaMarcaenAnclaconSubcategoria();
                    //cmbMarcaAncla.Text = oePancla.Rows[0]["id_Brand"].ToString().Trim();
                    //LlenaProductoenAncla();
                    //cmbproductAncla.Text=oePancla.Rows[0]["cod_Product"].ToString().Trim();
                    //comboOficinaXclienteenPancla();
                    //cmbOficinaPancla.Text = oePancla.Rows[0]["cod_Oficina"].ToString().Trim();
                    //LlenaPrecioPAncla();
                    //LlenaPesoPAncla();

                    //estado = Convert.ToBoolean(oePancla.Rows[0]["pancla_Status"].ToString().Trim());

                    //if (estado == true)
                    //{
                    //    RBTproductoAncla.Items[0].Selected = true;
                    //    RBTproductoAncla.Items[1].Selected = false;
                    //}
                    //else
                    //{
                    //    RBTproductoAncla.Items[0].Selected = false;
                    //    RBTproductoAncla.Items[1].Selected = true;
                    //}
                    //this.Session["tPancla"] = oePancla;
                    //this.Session["i"] = 0;
                    //if (oePancla.Rows.Count == 1)
                    //{
                    //    BtnPriPancla.Visible = false;
                    //    BtnAntPancla.Visible = false;
                    //    BtnSigPancla.Visible = false;
                    //    BtnUltPancla.Visible = false;
                    //}
                    //else
                    //{
                    //    BtnPriPancla.Visible = true;
                    //    BtnAntPancla.Visible = true;
                    //    BtnSigPancla.Visible = true;
                    //    BtnUltPancla.Visible = true;
                    //}


                }
                else
                {
                    SavelimpiarControlesProductAncla();
                    saveActivarbotonesProductAncla();
                    ModalPopup_BPAncla.Hide();

                    ModalPopup_BPAncla.Show();

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "la consulta realizada no arrojo ninguna respuesta";
                    Mensajes_ProductoAncla();


                }
            }

        }

        protected void GVConsultaPancla_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            Button2.Visible = true;
            GVConsultaPancla.EditIndex = -1;
            DataTable oePancla = oPLA_ProductosAncla.ConsultarPancla(Convert.ToInt32(this.Session["sbcliente"].ToString().Trim()), this.Session["sCategoria"].ToString().Trim(), Convert.ToInt64(this.Session["soficina"].ToString().Trim()),"0",0);
            this.Session["tPancla"] = oePancla;
            if (oePancla != null)
            {
                if (oePancla.Rows.Count > 0)
                {
                    GVConsultaPancla.DataSource = oePancla;
                    GVConsultaPancla.DataBind();
                    ModalPopancla.Show();

                    for (int i = 0; i <= oePancla.Rows.Count - 1; i++)
                    {

                        LlenacomboGVClienteProductAncla(i);
                        ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).Text = oePancla.Rows[i][1].ToString().Trim();
                        comboOficinaXGVclienteenPancla(i);
                        ((DropDownList)GVConsultaPancla.Rows[i].Cells[1].FindControl("cmboficipancla")).Text = oePancla.Rows[i][12].ToString().Trim();
                        LlenacomboGVCategoProductAncla(i);
                        ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).Text = oePancla.Rows[i][2].ToString().Trim();
                        LlenaGVSubporCategoProductAncla(i);
                        ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Text = oePancla.Rows[i][3].ToString().Trim();
                        LlenaMarcaGVenAnclaconSubcategoria(i);
                        ((DropDownList)GVConsultaPancla.Rows[i].Cells[4].FindControl("cmbmarcapancla")).Text = oePancla.Rows[i][11].ToString().Trim();
                        LlenaGVProductoenAncla(i);
                        ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).Text = oePancla.Rows[i][4].ToString().Trim();
                        LlenaGVPrecioPAncla(i);
                        LlenaGVPesoPAncla(i);
                    }

                }
            }



        }

        protected void GVConsultaPancla_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            ModalPopancla.Show();
            Button2.Visible = false;
            GVConsultaPancla.EditIndex = e.NewEditIndex;
            string Cliente, Oficina, Categoria, Subcategoria, Marca, Producto, Precio, Peso;
            long iid_pancla;
            bool estado;

            Cliente = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[0].FindControl("cmbcliepancla")).Text;
            Oficina = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[1].FindControl("cmboficipancla")).Text;
            Categoria = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[2].FindControl("cmbcatepancla")).Text;
            Subcategoria = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text;
            Marca = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[4].FindControl("cmbmarcapancla")).Text;
            Producto = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text;
            Precio = ((TextBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[6].FindControl("cmbsubpreciopancla")).Text;
            Peso = ((TextBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[7].FindControl("cmbsubpesopancla")).Text;
            estado = ((CheckBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[8].FindControl("Checkpancla")).Checked;
            DataTable oePancla = (DataTable)this.Session["tPancla"];
            GVConsultaPancla.DataSource = oePancla;
            GVConsultaPancla.DataBind();
            //this.Session["idPancla"] = Convert.ToInt64(ecpancla.Rows[GVConsultaPancla.EditIndex]["id_Product"].ToString().Trim());
            //iid_Product = Convert.ToInt64(this.Session["idPancla"]);
            //for (int i = 0; i <= oePancla.Rows.Count - 1; i++) 
            //        {
            LlenacomboGVClienteProductAncla(GVConsultaPancla.EditIndex);
            ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[0].FindControl("cmbcliepancla")).Text = oePancla.Rows[GVConsultaPancla.EditIndex][1].ToString().Trim();
            comboOficinaXGVclienteenPancla(GVConsultaPancla.EditIndex);
            ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[1].FindControl("cmboficipancla")).Text = oePancla.Rows[GVConsultaPancla.EditIndex][12].ToString().Trim();
            LlenacomboGVCategoProductAncla(GVConsultaPancla.EditIndex);
            ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[2].FindControl("cmbcatepancla")).Text = oePancla.Rows[GVConsultaPancla.EditIndex][2].ToString().Trim();
            LlenaGVSubporCategoProductAncla(GVConsultaPancla.EditIndex);
            ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text = oePancla.Rows[GVConsultaPancla.EditIndex][3].ToString().Trim();
            LlenaMarcaGVenAnclaconSubcategoria(GVConsultaPancla.EditIndex);
            ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[4].FindControl("cmbmarcapancla")).Text = oePancla.Rows[GVConsultaPancla.EditIndex][11].ToString().Trim();
            LlenaGVProductoenAncla(GVConsultaPancla.EditIndex);
            ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text = oePancla.Rows[GVConsultaPancla.EditIndex][4].ToString().Trim();
            LlenaGVPrecioPAncla(GVConsultaPancla.EditIndex);
            LlenaGVPesoPAncla(GVConsultaPancla.EditIndex);

            //}
            this.Session["id_pancla"] = Convert.ToInt64(oePancla.Rows[GVConsultaPancla.EditIndex]["id_pancla"].ToString().Trim());
            iid_pancla = Convert.ToInt64(this.Session["id_pancla"]);
            LlenacomboGVClienteProductAncla(GVConsultaPancla.EditIndex);
            ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[0].FindControl("cmbcliepancla")).Items.FindByValue(Cliente).Selected = true;
            comboOficinaXGVclienteenPancla(GVConsultaPancla.EditIndex);
            ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[1].FindControl("cmboficipancla")).Items.FindByValue(Oficina).Selected = true;
            LlenacomboGVCategoProductAncla(GVConsultaPancla.EditIndex);
            ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[2].FindControl("cmbcatepancla")).Items.FindByValue(Categoria).Selected = true;
            LlenaGVSubporCategoProductAncla(GVConsultaPancla.EditIndex);
            ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Items.FindByValue(Subcategoria).Selected = true;
            LlenaMarcaGVenAnclaconSubcategoria(GVConsultaPancla.EditIndex);
            ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[4].FindControl("cmbmarcapancla")).Items.FindByValue(Marca).Selected = true;
            LlenaGVProductoenAncla(GVConsultaPancla.EditIndex);
            ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Items.FindByValue(Producto).Selected = true;
            LlenaGVPrecioPAncla(GVConsultaPancla.EditIndex);
            ((TextBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[6].FindControl("cmbsubpreciopancla")).Text = Precio;
            LlenaGVPesoPAncla(GVConsultaPancla.EditIndex);
            ((TextBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[7].FindControl("cmbsubpesopancla")).Text = Peso;
            ((CheckBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[8].FindControl("Checkpancla")).Checked = estado;

            this.Session["rept"] = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[0].FindControl("cmbcliepancla")).Text;
            this.Session["rept1"] = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[2].FindControl("cmbcatepancla")).Text;
            this.Session["rept2"] = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text;
            this.Session["rept3"] = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[1].FindControl("cmboficipancla")).Text;
        }

        protected void GVConsultaPancla_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            bool estado;

            Button2.Visible = true;
            if (((CheckBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[8].FindControl("Checkpancla")).Checked != false)
            {
                estado = true;
            }
            else
            {
                estado = false;

            }

            try
            {
                if (Convert.ToDecimal(((TextBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[6].FindControl("cmbsubpreciopancla")).Text) == 0)
                {

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "no ingresar Precio con valor 0";
                    ModalPopancla.Show();
                    Mensajes_ProductoAncla();
                    return;
                }
            }
            catch
            {
            }

            string LblFaltantes="";

            if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[0].FindControl("cmbcliepancla")).Text == "0" || ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[1].FindControl("cmboficipancla")).Text == "0" || ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[2].FindControl("cmbcatepancla")).Text == "0" || ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text == "0" || ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[4].FindControl("cmbmarcapancla")).Text == "0" || ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text == "0")
            {
                if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[0].FindControl("cmbcliepancla")).Text == "0")
                {
                   LblFaltantes = ". " + "Cliente";
                }
                if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[1].FindControl("cmboficipancla")).Text == "0")
                {
                    LblFaltantes = LblFaltantes + ". " + "Oficina";
                }
                if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[2].FindControl("cmbcatepancla")).Text == "0")
                {
                   LblFaltantes = LblFaltantes + ". " + "Categoria";
                }
                if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text == "0")
                {
                   LblFaltantes= LblFaltantes + ". " + "SubCategoria";
                }
                if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[4].FindControl("cmbmarcapancla")).Text == "0")
                {
                    LblFaltantes = ". " + "Marca";
                }
                if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text == "0")
                {
                    LblFaltantes = ". " + "Producto";
                }

                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Debe ingresar todos los campos con (*): " + LblFaltantes;
                Mensajes_ProductoAncla();
                return;
            }
            try
            {

                string repetido, repetido1, repetido2, repetido3;


                repetido = Convert.ToString(this.Session["rept"]);
                repetido1 = Convert.ToString(this.Session["rept1"]);
                repetido2 = Convert.ToString(this.Session["rept2"]);
                repetido3 = Convert.ToString(this.Session["rept3"]);
                if (repetido != ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[0].FindControl("cmbcliepancla")).Text || repetido1 != ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[2].FindControl("cmbcatepancla")).Text || repetido2 != ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text || repetido3 != ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[1].FindControl("cmboficipancla")).Text)
                {
                    DAplicacion odconsultaPAncla = new DAplicacion();
                    DataTable dtconsulta = odconsultaPAncla.ConsultaDuplicadosPancla(ConfigurationManager.AppSettings["AD_ProductosAncla"], Convert.ToInt32(((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[0].FindControl("cmbcliepancla")).Text), ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[2].FindControl("cmbcatepancla")).Text, Convert.ToInt64(((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text), Convert.ToInt64(((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[1].FindControl("cmboficipancla")).Text));
                    if (dtconsulta == null)
                    {
                        string ssubCategoryP = "";
                        if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text == "n")
                        {
                            ssubCategoryP = "0";
                        }
                        else
                        {
                            ssubCategoryP = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text;
                        }
                        long dato;
                        dato = Convert.ToInt64(this.Session["id_pancla"]);
                        EPLA_ProductosAncla oeacPancla = oPLA_ProductosAncla.Actualizar_Pancla(Convert.ToInt64(this.Session["id_pancla"]), Convert.ToInt32(((Label)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[10].FindControl("lblid_ReportsPlanning")).Text), Convert.ToInt64(((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text), ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        EProductos oeaPAncla = oPLA_ProductosAncla.Actualizar_PrecioLista(((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text, Convert.ToDecimal(((TextBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[6].FindControl("cmbsubpreciopancla")).Text), Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        string sCodProduct = "";
                        sCodProduct = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).SelectedItem.Text;
                        this.Session["sCodProduct"] = sCodProduct;
                        SavelimpiarControlesProductAncla();
                        GVConsultaPancla.EditIndex = -1;
                        DataTable oePancla = oPLA_ProductosAncla.ConsultarPancla(Convert.ToInt32(this.Session["sbcliente"].ToString().Trim()), this.Session["sCategoria"].ToString().Trim(), Convert.ToInt64(this.Session["soficina"].ToString().Trim()),"0",0);
                        this.Session["tPancla"] = oePancla;
                        long iid_pancla;
                        if (oePancla != null)
                        {
                            if (oePancla.Rows.Count > 0)
                            {
                                GVConsultaPancla.DataSource = oePancla;
                                GVConsultaPancla.DataBind();
                                ModalPopancla.Show();

                                for (int i = 0; i <= oePancla.Rows.Count - 1; i++)
                                {
                                    this.Session["id_pancla"] = Convert.ToInt64(oePancla.Rows[0]["id_pancla"].ToString().Trim());
                                    iid_pancla = Convert.ToInt64(this.Session["id_pancla"]);
                                    LlenacomboGVClienteProductAncla(i);
                                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).Text = oePancla.Rows[i][1].ToString().Trim();
                                    comboOficinaXGVclienteenPancla(i);
                                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[1].FindControl("cmboficipancla")).Text = oePancla.Rows[i][12].ToString().Trim();
                                    LlenacomboGVCategoProductAncla(i);
                                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).Text = oePancla.Rows[i][2].ToString().Trim();
                                    LlenaGVSubporCategoProductAncla(i);
                                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Text = oePancla.Rows[i][3].ToString().Trim();
                                    LlenaMarcaGVenAnclaconSubcategoria(i);
                                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[4].FindControl("cmbmarcapancla")).Text = oePancla.Rows[i][11].ToString().Trim();
                                    LlenaGVProductoenAncla(i);
                                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).Text = oePancla.Rows[i][4].ToString().Trim();
                                    LlenaGVPrecioPAncla(i);
                                    LlenaGVPesoPAncla(i);
                                }

                            }
                        }
                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupConfirm";
                        this.Session["mensaje"] = "El Producto Ancla " + this.Session["sCodProduct"] + " fue Actualizado con Exito";
                        Mensajes_ProductoAncla();

                        //saveActivarbotonesProductAncla();
                        desactivarControlesProductAncla();
                    }
                    else
                    {
                        GVConsultaPancla.EditIndex = -1;
                        DataTable oePancla = oPLA_ProductosAncla.ConsultarPancla(Convert.ToInt32(this.Session["sbcliente"].ToString().Trim()), this.Session["sCategoria"].ToString().Trim(), Convert.ToInt64(this.Session["soficina"].ToString().Trim()),"0",0 );
                        this.Session["tPancla"] = oePancla;
                        if (oePancla != null)
                        {
                            if (oePancla.Rows.Count > 0)
                            {
                                GVConsultaPancla.DataSource = oePancla;
                                GVConsultaPancla.DataBind();
                                ModalPopancla.Show();

                                for (int i = 0; i <= oePancla.Rows.Count - 1; i++)
                                {

                                    LlenacomboGVClienteProductAncla(i);
                                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).Text = oePancla.Rows[i][1].ToString().Trim();
                                    comboOficinaXGVclienteenPancla(i);
                                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[1].FindControl("cmboficipancla")).Text = oePancla.Rows[i][12].ToString().Trim();
                                    LlenacomboGVCategoProductAncla(i);
                                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).Text = oePancla.Rows[i][2].ToString().Trim();
                                    LlenaGVSubporCategoProductAncla(i);
                                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Text = oePancla.Rows[i][3].ToString().Trim();
                                    LlenaMarcaGVenAnclaconSubcategoria(i);
                                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[4].FindControl("cmbmarcapancla")).Text = oePancla.Rows[i][11].ToString().Trim();
                                    LlenaGVProductoenAncla(i);
                                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).Text = oePancla.Rows[i][4].ToString().Trim();
                                    LlenaGVPrecioPAncla(i);
                                    LlenaGVPesoPAncla(i);
                                }

                            }
                        }
                        //string sCodProduct = "";
                        //sCodProduct =((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).SelectedItem.Text;
                        //this.Session["sCodProduct"] = sCodProduct;

                        this.Session["encabemensa"] = "Sr. Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "El Producto Ancla " + this.Session["sCodProduct"] + " Ya Existe";
                        Mensajes_ProductoAncla();

                    }
                }
                else
                {
                    string ssubCategoryP = "";
                    if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text == "n")
                    {
                        ssubCategoryP = "0";
                    }
                    else
                    {
                        ssubCategoryP = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text;
                    }
                    long dato;
                    dato = Convert.ToInt64(this.Session["id_pancla"]);
                    EPLA_ProductosAncla oeacPancla = oPLA_ProductosAncla.Actualizar_Pancla(Convert.ToInt64(this.Session["id_pancla"]), Convert.ToInt32(((Label)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[10].FindControl("lblid_ReportsPlanning")).Text), Convert.ToInt64(((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text), ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    EProductos oeaPAncla = oPLA_ProductosAncla.Actualizar_PrecioLista(((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text, Convert.ToDecimal(((TextBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[6].FindControl("cmbsubpreciopancla")).Text), Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sCodProduct = "";
                    sCodProduct = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).SelectedItem.Text;
                    this.Session["sCodProduct"] = sCodProduct;
                    SavelimpiarControlesProductAncla();
                    GVConsultaPancla.EditIndex = -1;
                    DataTable oePancla = oPLA_ProductosAncla.ConsultarPancla(Convert.ToInt32(this.Session["sbcliente"].ToString().Trim()), this.Session["sCategoria"].ToString().Trim(), Convert.ToInt64(this.Session["soficina"].ToString().Trim()),"0",0);
                    this.Session["tPancla"] = oePancla;
                    if (oePancla != null)
                    {
                        if (oePancla.Rows.Count > 0)
                        {
                            GVConsultaPancla.DataSource = oePancla;
                            GVConsultaPancla.DataBind();
                            ModalPopancla.Show();

                            for (int i = 0; i <= oePancla.Rows.Count - 1; i++)
                            {

                                LlenacomboGVClienteProductAncla(i);
                                ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).Text = oePancla.Rows[i][1].ToString().Trim();
                                comboOficinaXGVclienteenPancla(i);
                                ((DropDownList)GVConsultaPancla.Rows[i].Cells[1].FindControl("cmboficipancla")).Text = oePancla.Rows[i][12].ToString().Trim();
                                LlenacomboGVCategoProductAncla(i);
                                ((DropDownList)GVConsultaPancla.Rows[i].Cells[2].FindControl("cmbcatepancla")).Text = oePancla.Rows[i][2].ToString().Trim();
                                LlenaGVSubporCategoProductAncla(i);
                                ((DropDownList)GVConsultaPancla.Rows[i].Cells[3].FindControl("cmbsubcatepancla")).Text = oePancla.Rows[i][3].ToString().Trim();
                                LlenaMarcaGVenAnclaconSubcategoria(i);
                                ((DropDownList)GVConsultaPancla.Rows[i].Cells[4].FindControl("cmbmarcapancla")).Text = oePancla.Rows[i][11].ToString().Trim();
                                LlenaGVProductoenAncla(i);
                                ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).Text = oePancla.Rows[i][4].ToString().Trim();
                                LlenaGVPrecioPAncla(i);
                                LlenaGVPesoPAncla(i);
                            }

                        }
                    }
                    //llenarcombos();

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "El Producto Ancla " + this.Session["sCodProduct"] + " fue Actualizado con Exito";
                    Mensajes_ProductoAncla();

                    //saveActivarbotonesProductAncla();
                    desactivarControlesProductAncla();

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

        protected void cmbcliepancla_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            comboOficinaXGVclienteenPancla(GVConsultaPancla.EditIndex);
            ModalPopancla.Show();
        }
        protected void cmboficipancla_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LlenacomboGVCategoProductAncla(GVConsultaPancla.EditIndex);
            ModalPopancla.Show();
        }
        protected void cmbcatepancla_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LlenaGVSubporCategoProductAncla(GVConsultaPancla.EditIndex);
            ModalPopancla.Show();
        }
        protected void cmbsubcatepancla_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LlenaMarcaGVenAnclaconSubcategoria(GVConsultaPancla.EditIndex);
            ModalPopancla.Show();
        }
        protected void cmbmarcapancla_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LlenaGVProductoenAncla(GVConsultaPancla.EditIndex);
            ModalPopancla.Show();
        }
        protected void cmbprodupancla_SelectedIndexChanged(object sender, System.EventArgs e)
        {
           
            LlenaGVPrecioPAncla(GVConsultaPancla.EditIndex);
            LlenaGVPesoPAncla(GVConsultaPancla.EditIndex);
            ModalPopancla.Show();
        }
        protected void btnCancelar_Click(object sender, System.EventArgs e)
        {
            ProdAnclaCrear.Visible = true;
            ProdAnclaConsultar.Visible = false;
            BtnCrearAncla.Visible = true;
            BtnGuardarAncla.Visible = false;
            BtnConsultarAncla.Visible = true;
            BtnCancelarAncla.Visible = true;
            TxtPrecioprodAncla.Text = "";

            ModalPopancla.Show();
            
        }

        protected void BtnCancelarAncla_Click(object sender, System.EventArgs e)
        {
            desactivarControlesProductAncla();

            ProdAnclaCrear.Visible = true;
            ProdAnclaConsultar.Visible = false;
            BtnCrearAncla.Visible = true;
            BtnGuardarAncla.Visible = false;
            BtnConsultarAncla.Visible = true;
            BtnCancelarAncla.Visible = true;
            TxtPrecioprodAncla.Text = "";

            ModalPopancla.Show();
            
        }

        

        private void Mensajes_ProductoAncla()
        {
            PMensajeProdAncla.CssClass = this.Session["cssclass"].ToString();
            lblEncabezadoProdAncla.Text = this.Session["encabemensa"].ToString();
            lblMensajeProdAncla.Text = this.Session["mensaje"].ToString();
            MensajemodalPProductoAncla.Show();
        }
            #endregion


        #region Objetivo SOD MAY


        protected void ImgObjetivoSODMay_Click(object sender, ImageClickEventArgs e)
        {


            DataSet ds = new DataSet();
            //ds = PPlanning.Get_PlanningCreados(CmbSelCampaña.SelectedValue);
            ds = (DataSet)this.ViewState["planning_creados"];
            if (ds.Tables[11].Rows.Count > 0)
            {
                this.Session["id_planning"] = ds.Tables[9].Rows[0]["id_planning"].ToString().Trim();
                this.Session["Planning_Name"] = ds.Tables[9].Rows[0]["Planning_Name"].ToString().Trim();
                DataTable dtCliente = Presupuesto.Get_ObtenerClientes(ds.Tables[9].Rows[0]["Planning_Budget"].ToString().Trim(), 1);
                this.Session["company_id"] = dtCliente.Rows[0]["Company_id"].ToString().Trim();
                this.Session["Planning_CodChannel"] = ds.Tables[9].Rows[0]["Planning_CodChannel"].ToString().Trim();
                dtCliente = null;
            }
            llenaReportesObjetivoSODMAY();
            LlenacomboClienteObjetivosSODMAY();
            LlenacomboClienteObjetivosSODMAYbuscar();
            ObjetivosSODMAY_Inicio();
            modalPObjetivoSODMAY.Show();


        }



        private void llenaReportesObjetivoSODMAY()
        {
            DataSet ds = cn.ejecutarDataSet("UP_WEBXPLORA_PLA_CONSULTA_INFORMESYMESES_PLANNING", this.Session["id_planning"].ToString().Trim(), Convert.ToInt32(this.Session["company_id"]), this.Session["Planning_CodChannel"].ToString().Trim(), 0);
            ddlReporteObjetivoSODMAY.DataSource = ds.Tables[1];
            ddlReporteObjetivoSODMAY.DataValueField = "Report_id";
            ddlReporteObjetivoSODMAY.DataTextField = "Report_NameReport";
            ddlReporteObjetivoSODMAY.DataBind();
            ds = null;
        }

        protected void ddlMesObjetivosSODMAY_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarPeriodos(dllPeridoObjetivosSODMAY, this.Session["id_planning"].ToString().Trim(), Convert.ToInt32(ddlReporteObjetivoSODMAY.SelectedValue), ddlAñoObjetivosSODMAY.SelectedValue, ddlMesObjetivosSODMAY.SelectedValue);
            modalPObjetivoSODMAY.Show();
        }

        private void LlenacomboClienteObjetivosSODMAY()
        {
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", 0, "0", 0, 0, "0");

            ddlClienteObjetivosSODMAY.DataSource = ds.Tables[0];
            ddlClienteObjetivosSODMAY.DataTextField = "Company_Name";
            ddlClienteObjetivosSODMAY.DataValueField = "Company_id";
            ddlClienteObjetivosSODMAY.DataBind();
            ddlClienteObjetivosSODMAY.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;

            ddlClienteObjetivosSODMAY.SelectedValue = this.Session["company_id"].ToString();
            llenacomoMalla(ddlMallaObjetivosSODMAY, ddlClienteObjetivosSODMAY.SelectedValue);
            ddlClienteObjetivosSODMAY.Enabled = false;


            DataSet dst = null;
            dst = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(ddlClienteObjetivosSODMAY.SelectedValue), "0", 0, 0, "0");
            //llena categoria segun cliente
            ddlCategoriaObjetivosSODMAY.DataSource = dst.Tables[1];
            ddlCategoriaObjetivosSODMAY.DataTextField = "Product_Category";
            ddlCategoriaObjetivosSODMAY.DataValueField = "id_ProductCategory";
            ddlCategoriaObjetivosSODMAY.DataBind();
            ddlCategoriaObjetivosSODMAY.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            dst = null;



            modalPObjetivoSODMAY.Show();
        }

        private void llenacomoMalla( DropDownList ddl,  string idcompany)
        {
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_PLA_LLENACOMBOSMALLLAXCOMPANY", Convert.ToInt32(idcompany));

            ddl.DataSource = ds.Tables[0];
            ddl.DataTextField = "malla";
            ddl.DataValueField = "id_malla";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;

        }




        protected void ddlCategoriaObjetivosSODMAY_SelectedIndexChanged(object sender, System.EventArgs e)
        {


            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(ddlClienteObjetivosSODMAY.SelectedValue), ddlCategoriaObjetivosSODMAY.SelectedValue, 0, 0, "0");
            //se llena marcas segun categoria y cliente

            ddlMarcaObjetivosSODMAY.DataSource = ds.Tables[3];
            ddlMarcaObjetivosSODMAY.DataTextField = "Name_Brand";
            ddlMarcaObjetivosSODMAY.DataValueField = "id_Brand";
            ddlMarcaObjetivosSODMAY.DataBind();
            ddlMarcaObjetivosSODMAY.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            ds = null;
            modalPObjetivoSODMAY.Show();
            
        }


        protected void btnCrearObjetivosSODMAY_Click(object sender, System.EventArgs e)
        {
            //ddlClienteObjetivosSODMAY.Enabled = true;
            ddlMallaObjetivosSODMAY.Enabled = true;
            ddlCategoriaObjetivosSODMAY.Enabled = true;
            ddlMarcaObjetivosSODMAY.Enabled = true;
            btnCrearObjetivosSODMAY.Visible = false;
            btnCargaMasisvaObjetivosSODMAY.Enabled = false;
            btnGuardarObjetivosSODMAY.Visible = true;
            btnGuardarObjetivosSODMAY.Enabled = true;
            txtObjetivoCategoria.Enabled = true;
            txtObjetivoMarca.Enabled = true;
            modalPObjetivoSODMAY.Show();

        }



        protected void btnCancelarObjetivosSODMAY_Click(object sender, System.EventArgs e)
        {
            tblcrear.Visible = true;
            tblConsultar.Visible = false;
            btnCrearObjetivosSODMAY.Visible = true;
            btnGuardarObjetivosSODMAY.Visible = false;
            btnConsultarObjetivosSODMAY.Visible = true;
            btnCancelarObjetivosSODMAY.Visible = true;
            btnCargaMasisvaObjetivosSODMAY.Enabled = true;
            txtObjetivoCategoria.Text = "";
            txtObjetivoMarca.Text = "";

            modalPObjetivoSODMAY.Show();

        }

        PLA_Supe_Objetivos_Sod_May oPLA_Supe_Objetivos_Sod_May = new PLA_Supe_Objetivos_Sod_May();

        protected void btnGuardarObjetivosSODMAY_Click(object sender, System.EventArgs e)
        {

            string faltantes = "";

            if (ddlClienteObjetivosSODMAY.Text == "0" || ddlCategoriaObjetivosSODMAY.Text == "0" || ddlMarcaObjetivosSODMAY.Text == "0" || ddlMallaObjetivosSODMAY.Text == "0" || txtObjetivoCategoria.Text == "" || txtObjetivoMarca.Text == "" || dllPeridoObjetivosSODMAY.SelectedValue == "")
            {
                if (ddlClienteObjetivosSODMAY.Text == "0")
                {
                    faltantes = ". " + "Cliente";
                }
                if (ddlCategoriaObjetivosSODMAY.Text == "0")
                {
                    faltantes = faltantes + ". " + "Categoria";
                }
                if (ddlMarcaObjetivosSODMAY.Text == "0")
                {
                    faltantes = ". " + "Marca";
                }
                if (ddlMallaObjetivosSODMAY.Text == "0")
                {
                    faltantes = ". " + "Malla";
                }
                if (txtObjetivoMarca.Text == "")
                {
                    faltantes = ". " + "Objetivo Marca";
                }
                
                if (txtObjetivoCategoria.Text == "0")
                {
                    faltantes = ". " + "Objetivo Categoria";
                }
                if (dllPeridoObjetivosSODMAY.SelectedValue == "")
                {
                    faltantes = ". " + "Periodo";
                }
                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Debe ingresar todos los campos con (*): " + faltantes;
                Mensajes_ObjetivosSODMAY();

                return;
            }

            try
            {

                DataTable dtconsulta = oPLA_Supe_Objetivos_Sod_May.ConsultarSupe_Objetivos_Sod_May(Convert.ToInt32(ddlClienteObjetivosSODMAY.SelectedValue), this.Session["Planning_CodChannel"].ToString().Trim(), Convert.ToInt32(ddlMallaObjetivosSODMAY.SelectedValue), ddlCategoriaObjetivosSODMAY.SelectedValue, Convert.ToInt32(ddlMarcaObjetivosSODMAY.SelectedValue), Convert.ToInt32(dllPeridoObjetivosSODMAY.SelectedValue));
                if (dtconsulta.Rows.Count == 0)
                {

                    EPLA_Supe_Objetivos_Sod_May oEPLA_Supe_Objetivos_Sod_May = oPLA_Supe_Objetivos_Sod_May.RegistrarSupe_Objetivos_Sod_May(Convert.ToInt32(ddlClienteObjetivosSODMAY.SelectedValue), this.Session["Planning_CodChannel"].ToString().Trim(), Convert.ToInt32(ddlMallaObjetivosSODMAY.SelectedValue), ddlCategoriaObjetivosSODMAY.SelectedValue, Convert.ToInt32(ddlMarcaObjetivosSODMAY.SelectedValue), Convert.ToInt32(dllPeridoObjetivosSODMAY.SelectedValue), Convert.ToDouble(txtObjetivoMarca.Text), Convert.ToDouble(txtObjetivoCategoria.Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                   

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupConfirm";
                    this.Session["mensaje"] = "El Objetivo fue creado con Exito";
                    Mensajes_ObjetivosSODMAY();


                    ObjetivosSODMAY_Limpiar();
                    ObjetivosSODMAY_Inicio();

                }
                else
                {
                   
                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "El Objetivo Ya Existe";
                    Mensajes_ObjetivosSODMAY();

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


        public void ObjetivosSODMAY_Inicio()
        {

            tblcrear.Visible = true;
            tblConsultar.Visible = false;
            pObjetivosSODMAY.Style.Value = "display:block";
            pCargamasivaObjetivosSODMAY.Style.Value = "display:block";
            btnCrearObjetivosSODMAY.Enabled = true;
            btnGuardarObjetivosSODMAY.Enabled = false;
            btnCancelarObjetivosSODMAY.Enabled = true;
            btnConsultarObjetivosSODMAY.Enabled = true;
            btnCargaMasisvaObjetivosSODMAY.Enabled = true;


            ddlClienteObjetivosSODMAY.Enabled = false;
            ddlMallaObjetivosSODMAY.Enabled = false;
            ddlMarcaObjetivosSODMAY.Enabled = false;
            ddlCategoriaObjetivosSODMAY.Enabled = false;
            txtObjetivoMarca.Enabled = false;
            txtObjetivoCategoria.Enabled = false;


        }

        public void ObjetivosSODMAY_Limpiar()
        {

            ddlMallaObjetivosSODMAY.SelectedValue="0";
            ddlMarcaObjetivosSODMAY.Items.Clear();
            ddlCategoriaObjetivosSODMAY.SelectedValue = "0";
            txtObjetivoMarca.Text = "";
            txtObjetivoCategoria.Text = "";


        }

        private void Mensajes_ObjetivosSODMAY()
        {
          
            PMensajeObjetivosSODMAY.CssClass = this.Session["cssclass"].ToString();
            lbltituloObjetivosSODMAY.Text = this.Session["encabemensa"].ToString();
            lblMensajeObjetivosSODMAY.Text = this.Session["mensaje"].ToString();
            MensajemodalObjetivosSODMAY.Show();
        }

        protected void btnaceptarObjetivosSODMAY_Click(object sender, EventArgs e)
        {

            MensajemodalObjetivosSODMAY.Hide();
            modalPObjetivoSODMAY.Show();

        }

        private void LlenacomboClienteObjetivosSODMAYbuscar()
        {
            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", 0, "0", 0, 0, "0");

            ddlClienteObjetivosSODMAYbuscar.DataSource = ds.Tables[0];
            ddlClienteObjetivosSODMAYbuscar.DataTextField = "Company_Name";
            ddlClienteObjetivosSODMAYbuscar.DataValueField = "Company_id";
            ddlClienteObjetivosSODMAYbuscar.DataBind();
            ddlClienteObjetivosSODMAYbuscar.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;

            ddlClienteObjetivosSODMAYbuscar.SelectedValue = this.Session["company_id"].ToString();
            llenacomoMalla(ddlMallaObjetivosSODMAYbuscar,ddlClienteObjetivosSODMAY.SelectedValue);
            ddlClienteObjetivosSODMAY.Enabled = false;


            DataSet dst = null;
            dst = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(ddlClienteObjetivosSODMAYbuscar.SelectedValue), "0", 0, 0, "0");
            //llena categoria segun cliente
            ddlCategoriaObjetivosSODMAYbuscar.DataSource = dst.Tables[1];
            ddlCategoriaObjetivosSODMAYbuscar.DataTextField = "Product_Category";
            ddlCategoriaObjetivosSODMAYbuscar.DataValueField = "id_ProductCategory";
            ddlCategoriaObjetivosSODMAYbuscar.DataBind();
            ddlCategoriaObjetivosSODMAYbuscar.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            dst = null;



           // modalPObjetivoSODMAYbuscar.Show();
        }


        protected void ddlCategoriaObjetivosSODMAYbuscar_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet ds = null;
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOSPRODUCANCLA", Convert.ToInt32(ddlClienteObjetivosSODMAYbuscar.SelectedValue), ddlCategoriaObjetivosSODMAYbuscar.SelectedValue, 0, 0, "0");
            //se llena marcas segun categoria y cliente

            ddlMarcaObjetivosSODMAYbuscar.DataSource = ds.Tables[3];
            ddlMarcaObjetivosSODMAYbuscar.DataTextField = "Name_Brand";
            ddlMarcaObjetivosSODMAYbuscar.DataValueField = "id_Brand";
            ddlMarcaObjetivosSODMAYbuscar.DataBind();
            ddlMarcaObjetivosSODMAYbuscar.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            ds = null;
            modalPObjetivoSODMAY.Show();
            modalPObjetivoSODMAYbuscar.Show();

        }



        protected void btnBuscarObjetivosSODMAYbuscar_Click(object sender, EventArgs e)
        {

            modalPObjetivoSODMAYbuscar.Hide();
            desactivarControlesProductAncla();

            string sbcliente;
            string sCategoria;
            string soficina;
            long iid_pancla;

            if (CmbBClientePAncla.Text == "0" || CmbBCategoriaPAncla.Text == "0")
            {

                this.Session["encabemensa"] = "Sr. Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Ingrese parametros de consulta minimo Cliente y Categoria";
                Mensajes_ObjetivosSODMAY();

                modalPObjetivoSODMAYbuscar.Show();
                return;
            }

            BuscarActivarbotnesProductAncla();
            sbcliente = CmbBClientePAncla.Text;
            sCategoria = CmbBCategoriaPAncla.Text;
            soficina = cmbOficinaBPancla.Text;
            CmbBClientePAncla.Text = "0";
            CmbBCategoriaPAncla.Text = "0";


            this.Session["sbcliente"] = sbcliente;
            this.Session["sCategoria"] = sCategoria;
            this.Session["soficina"] = soficina;

            DataTable dtconsulta = oPLA_Supe_Objetivos_Sod_May.ConsultarSupe_Objetivos_Sod_May(Convert.ToInt32(ddlClienteObjetivosSODMAYbuscar.SelectedValue), this.Session["Planning_CodChannel"].ToString().Trim(), Convert.ToInt32(ddlMallaObjetivosSODMAYbuscar.SelectedValue), ddlCategoriaObjetivosSODMAYbuscar.SelectedValue, Convert.ToInt32(ddlMarcaObjetivosSODMAYbuscar.SelectedValue),0);
            Session["tObjMAY"] = dtconsulta;
            if (dtconsulta != null)
            {
                if (dtconsulta.Rows.Count != 0)
                {
                    gvObjetivosSODMAY.DataSource = dtconsulta;
                    gvObjetivosSODMAY.DataBind();
                    modalPObjetivoSODMAY.Show();
                    tblConsultar.Visible = true;

                    tblcrear.Visible = false;
                    btnCrearObjetivosSODMAY.Visible = false;
                    btnGuardarObjetivosSODMAY.Visible = false;
                    btnConsultarObjetivosSODMAY.Visible = false;
                    btnCancelarObjetivosSODMAY.Visible = false;
                    btnCargaMasisvaObjetivosSODMAY.Enabled = false;
                    btnCargaMasisvaObjetivosSODMAY.Visible = false;

                    
                }
                else
                {


                    modalPObjetivoSODMAYbuscar.Show();

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "la consulta realizada no arrojo ninguna respuesta";
                    Mensajes_ObjetivosSODMAY();


                }
            }

        }

        protected void gvObjetivosSODMAY_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            modalPObjetivoSODMAY.Show();
            Button2.Visible = false;
            gvObjetivosSODMAY.EditIndex = e.NewEditIndex;
            string Cliente, ValorMarca, ValorCategoria, Subcategoria, Marca, Producto, Precio, Peso;
            long iid_pancla;
            bool estado;


             ValorCategoria= ((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[5].FindControl("txtObjCategoria")).Text;
             ValorMarca = ((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[6].FindControl("txtObjMarca")).Text;
             estado = ((CheckBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[8].FindControl("CheckObjetivosSODMAY")).Checked;
            DataTable dt = (DataTable)this.Session["tObjMAY"];
            gvObjetivosSODMAY.DataSource = dt;
            gvObjetivosSODMAY.DataBind();


            ((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[5].FindControl("txtObjCategoria")).Text = ValorCategoria;
            ((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[6].FindControl("txtObjMarca")).Text = ValorMarca;
            ((CheckBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[8].FindControl("CheckObjetivosSODMAY")).Checked = estado;

            this.Session["rept"] = ((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[5].FindControl("txtObjCategoria")).Text;
            this.Session["rept1"] = ((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[6].FindControl("txtObjMarca")).Text;
            this.Session["rept2"] = ((CheckBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[8].FindControl("CheckObjetivosSODMAY")).Text;

        }


        protected void gvObjetivosSODMAY_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            Button2.Visible = true;
            gvObjetivosSODMAY.EditIndex = -1;
            DataTable dtconsulta = oPLA_Supe_Objetivos_Sod_May.ConsultarSupe_Objetivos_Sod_May(Convert.ToInt32(ddlClienteObjetivosSODMAYbuscar.SelectedValue), this.Session["Planning_CodChannel"].ToString().Trim(), Convert.ToInt32(ddlMallaObjetivosSODMAYbuscar.SelectedValue), ddlCategoriaObjetivosSODMAYbuscar.SelectedValue, Convert.ToInt32(ddlMarcaObjetivosSODMAYbuscar.SelectedValue), 0);
            this.Session["tObjMAY"] = dtconsulta;
            if (dtconsulta != null)
            {
                if (dtconsulta.Rows.Count > 0)
                {
                    gvObjetivosSODMAY.DataSource = dtconsulta;
                    gvObjetivosSODMAY.DataBind();
                    modalPObjetivoSODMAY.Show();


                }
            }



        }


        protected void gvObjetivosSODMAY_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            bool estado;

            Button2.Visible = true;
            if (((CheckBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[8].FindControl("CheckObjetivosSODMAY")).Checked != false)
            {
                estado = true;
            }
            else
            {
                estado = false;

            }

            try
            {
                if (((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[6].FindControl("txtObjMarca")).Text == "" || ((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[5].FindControl("txtObjCategoria")).Text == "")
                {

                    this.Session["encabemensa"] = "Sr. Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "no ingresar Valores con 0";
                    modalPObjetivoSODMAY.Show();
                    Mensajes_ObjetivosSODMAY();
                    return;
                }

            }
            catch
            {

            }

            try
            {

                string repetido, repetido1, repetido2;


                repetido = Convert.ToString(this.Session["rept"]);
                repetido1 = Convert.ToString(this.Session["rept1"]);
                repetido2 = Convert.ToString(this.Session["rept2"]);
                string dfg =((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[5].FindControl("txtObjCategoria")).Text;
                string fg = ((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[6].FindControl("txtObjMarca")).Text;
                if (repetido == ((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[5].FindControl("txtObjCategoria")).Text && repetido1 == ((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[6].FindControl("txtObjMarca")).Text)
                {
                    DataTable dtconsulta = oPLA_Supe_Objetivos_Sod_May.ConsultarSupe_Objetivos_Sod_May(Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[9].FindControl("lblid_ClienteObjetivosSODMAY")).Text), ((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[10].FindControl("lblcod_ChannelObjetivosSODMAY")).Text, Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[11].FindControl("lblid_mallaObjetivosSODMAYY")).Text), ((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[12].FindControl("lblid_ProductCategoryObjetivosSODMAY")).Text, Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[13].FindControl("lblid_BrandObjetivosSODMAY")).Text), Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[14].FindControl("lblid_ReportsPlanning")).Text));
                    if (dtconsulta.Rows.Count == 0)
                    {

                        EPLA_Supe_Objetivos_Sod_May oEPLA_Supe_Objetivos_Sod_May = oPLA_Supe_Objetivos_Sod_May.ActualizarSupe_Objetivos_Sod_May(Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[15].FindControl("lblid_objMay")).Text), Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[9].FindControl("lblid_ClienteObjetivosSODMAY")).Text), ((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[10].FindControl("lblcod_ChannelObjetivosSODMAY")).Text, Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[11].FindControl("lblid_mallaObjetivosSODMAYY")).Text), ((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[12].FindControl("lblid_ProductCategoryObjetivosSODMAY")).Text, Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[13].FindControl("lblid_BrandObjetivosSODMAY")).Text), Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[14].FindControl("lblid_ReportsPlanning")).Text), Convert.ToDouble(((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[6].FindControl("txtObjMarca")).Text), Convert.ToDouble(((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[5].FindControl("txtObjCategoria")).Text), ((CheckBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[8].FindControl("CheckObjetivosSODMAY")).Checked, Convert.ToString(this.Session["sUser"]), DateTime.Now);


                        gvObjetivosSODMAY.EditIndex = -1;
                        DataTable dt = oPLA_Supe_Objetivos_Sod_May.ConsultarSupe_Objetivos_Sod_May(Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[9].FindControl("lblid_ClienteObjetivosSODMAY")).Text), ((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[10].FindControl("lblcod_ChannelObjetivosSODMAY")).Text, Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[11].FindControl("lblid_mallaObjetivosSODMAYY")).Text), ((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[12].FindControl("lblid_ProductCategoryObjetivosSODMAY")).Text, Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[13].FindControl("lblid_BrandObjetivosSODMAY")).Text), Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[14].FindControl("lblid_ReportsPlanning")).Text));
                        this.Session["tObjMAY"] = dt;

                            if (dt.Rows.Count == 0)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupConfirm";
                                this.Session["mensaje"] = "El Objetivo fue Actualizado con Exito";
                                Mensajes_ObjetivosSODMAY();
                                gvObjetivosSODMAY.DataSource = dt;
                                gvObjetivosSODMAY.DataBind();
                               // modalPObjetivoSODMAY.Show();

                            }
                        
                     

                    }
                    else
                    {
                       
                        DataTable dt = oPLA_Supe_Objetivos_Sod_May.ConsultarSupe_Objetivos_Sod_May(Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[9].FindControl("lblid_ClienteObjetivosSODMAY")).Text), ((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[10].FindControl("lblcod_ChannelObjetivosSODMAY")).Text, Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[11].FindControl("lblid_mallaObjetivosSODMAYY")).Text), ((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[12].FindControl("lblid_ProductCategoryObjetivosSODMAY")).Text, Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[13].FindControl("lblid_BrandObjetivosSODMAY")).Text), Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[14].FindControl("lblid_ReportsPlanning")).Text));
                        gvObjetivosSODMAY.EditIndex = -1;
                        this.Session["tObjMAY"] = dt;
                       
                            if (dt.Rows.Count > 0)
                            {
                                this.Session["encabemensa"] = "Sr. Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";
                                this.Session["mensaje"] = "El Objetivo Ya Existe";
                                Mensajes_ObjetivosSODMAY();
                                gvObjetivosSODMAY.DataSource = dt;
                                gvObjetivosSODMAY.DataBind();
                               // modalPObjetivoSODMAY.Show();


                            }
                        



                    }
                }
                else
                {


                    EPLA_Supe_Objetivos_Sod_May oEPLA_Supe_Objetivos_Sod_May = oPLA_Supe_Objetivos_Sod_May.ActualizarSupe_Objetivos_Sod_May(Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[15].FindControl("lblid_objMay")).Text), Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[9].FindControl("lblid_ClienteObjetivosSODMAY")).Text), ((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[10].FindControl("lblcod_ChannelObjetivosSODMAY")).Text, Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[11].FindControl("lblid_mallaObjetivosSODMAYY")).Text), ((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[12].FindControl("lblid_ProductCategoryObjetivosSODMAY")).Text, Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[13].FindControl("lblid_BrandObjetivosSODMAY")).Text), Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[14].FindControl("lblid_ReportsPlanning")).Text), Convert.ToDouble(((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[6].FindControl("txtObjMarca")).Text), Convert.ToDouble(((TextBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[5].FindControl("txtObjCategoria")).Text), ((CheckBox)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[8].FindControl("CheckObjetivosSODMAY")).Checked, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    
                   
                    DataTable dt = oPLA_Supe_Objetivos_Sod_May.ConsultarSupe_Objetivos_Sod_May(Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[9].FindControl("lblid_ClienteObjetivosSODMAY")).Text), ((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[10].FindControl("lblcod_ChannelObjetivosSODMAY")).Text, Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[11].FindControl("lblid_mallaObjetivosSODMAYY")).Text), ((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[12].FindControl("lblid_ProductCategoryObjetivosSODMAY")).Text, Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[13].FindControl("lblid_BrandObjetivosSODMAY")).Text), Convert.ToInt32(((Label)gvObjetivosSODMAY.Rows[gvObjetivosSODMAY.EditIndex].Cells[14].FindControl("lblid_ReportsPlanning")).Text));
                    gvObjetivosSODMAY.EditIndex = -1;
                    this.Session["tObjMAY"] = dt;

                        if (dt.Rows.Count > 0)
                        {

                           



                            gvObjetivosSODMAY.DataSource = dt;
                            gvObjetivosSODMAY.DataBind();
                            this.Session["encabemensa"] = "Sr. Usuario";
                            this.Session["cssclass"] = "MensajesSupConfirm";
                            this.Session["mensaje"] = "El Objetivo fue Actualizado con Exito";
                            Mensajes_ObjetivosSODMAY();
                           // modalPObjetivoSODMAY.Show();



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

        protected void btnCancelarObjetivosSODMAYgrilla_Click(object sender, System.EventArgs e)
        {
            tblcrear.Visible = true;
            tblConsultar.Visible = false;
            btnCrearObjetivosSODMAY.Visible = true;
            BtnGuardarAncla.Visible = false;
            btnCrearObjetivosSODMAY.Visible = true;
            btnCancelarObjetivosSODMAY.Visible = true;
            btnCargaMasisvaObjetivosSODMAY.Visible = true;
            btnCargaMasisvaObjetivosSODMAY.Enabled = true;
            btnConsultarObjetivosSODMAY.Visible = true;
            btnConsultarObjetivosSODMAY.Enabled = true;
            txtObjetivoMarca.Text = "";
            txtObjetivoCategoria.Text = "";

            modalPObjetivoSODMAY.Show();

        }

        protected void btnCargaMasisvaObjetivosSODMAY_Click(object sender, System.EventArgs e)
        {
            divCargaMasivaObjetivosSODMAY.Style.Value = "display:block";

            pObjetivosSODMAY.Style.Value ="display:none";

            pCargamasivaObjetivosSODMAY.Style.Value = "display:block";

            IframeCargaMasivaObjetivosSODMAY.Attributes["src"] = "Carga_ObjetivosMAY.aspx";
            modalpCargamasiva.Show();
        }

        protected void BtnCerrarCargaMasiva_Click(object sender, System.EventArgs e)
        {
            divCargaMasivaObjetivosSODMAY.Style.Value = "display:none";

            pObjetivosSODMAY.Style.Value ="display:block";

            pCargamasivaObjetivosSODMAY.Style.Value = "display:none";
            IframeCargaMasivaObjetivosSODMAY.Attributes["src"] = "";

            ObjetivosSODMAY_Inicio();
            ObjetivosSODMAY_Limpiar();

            modalPObjetivoSODMAY.Show();
        }

        


        #endregion


        #region Elementos de visibilidad
        protected void btnsave_Elemento_Click(object sender, EventArgs e)
        {
            DataTable dt;
            DataTable dtl;

            Conexion cn = new Conexion();
            Conexion con = new Conexion(2);
            foreach (ListItem ELEM_VISI in chklist.Items)
            {
                if (ELEM_VISI.Selected)
                {
                    dtl = cn.ejecutarDataTable("UP_PLA_CONSULTAR_MPointOfPurchase_Planning", TxtPlanningAsigProd.Text, ELEM_VISI.Value);

                    if (dtl.Rows.Count == 0)
                    {
                        cn.ejecutarDataTable("UP_PLA_CREARPLANNINGXMPOP", TxtPlanningAsigProd.Text, ELEM_VISI.Value, true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now);

                        //dt = con.ejecutarDataTable("STP_JVM_CONSULTAR_TBL_TIPMATERIAL", this.Session["Planning"].ToString(), "02");
                        //if (dt.Rows.Count == 0)
                        //{
                        //    con.ejecutarDataTable("STP_JVM_INSERTAR_TBL_TIPMATERIAL", this.Session["Planning"].ToString(), TIPOMATPOP.Value, TIPOMATPOP.Text);
                        //}
                        con.ejecutarDataTable("STP_JVM_INSERTAR_TBL_EQUIPO_POP", TxtPlanningAsigProd.Text, null, ELEM_VISI.Value, null, true, null);

                    }
                    else
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "El Material POP " + ELEM_VISI.Text + " Ya ha sido agregado a la campaña.";
                        Mensajes_Productos();
                        return;
                    }

                }
            }

            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupConfirm";
            this.Session["mensaje"] = "Se ha creado con éxito el Material POP a la campaña ";
            Mensajes_Productos();

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
                    string pla = TxtPlanningAsigProd.Text;
                    string ass=this.Session["RbtnListInfProd"].ToString();



                    dtl = cn.ejecutarDataTable("PLA_CONSULTAR_PLANNING_OBSERVACION", TxtPlanningAsigProd.Text, Convert.ToInt32(OBSERVACION.Value), Convert.ToInt32(this.Session["RbtnListInfProd"].ToString()));

                    if (dtl.Rows.Count == 0)
                    {
                        cn.ejecutarDataTable("PLA_INSERTAR_PLANNING_OBSERVACION", TxtPlanningAsigProd.Text, Convert.ToInt32(OBSERVACION.Value), true, this.Session["sUser"].ToString().Trim(), DateTime.Now, this.Session["sUser"].ToString().Trim(), DateTime.Now, Convert.ToInt32(this.Session["RbtnListInfProd"].ToString()));

                        //dt = con.ejecutarDataTable("STP_JVM_CONSULTAR_TBL_TIPMATERIAL", this.Session["Planning"].ToString(), "02");
                        //if (dt.Rows.Count == 0)
                        //{
                        //    con.ejecutarDataTable("STP_JVM_INSERTAR_TBL_TIPMATERIAL", this.Session["Planning"].ToString(), TIPOMATPOP.Value, TIPOMATPOP.Text);
                        //}


                        if (OBSERVACION.Value.ToString().Length == 1)
                        {
                            OBSERVACION.Value = "0" + OBSERVACION.Value;
                        }

                        con.ejecutarDataTable("STP_JVM_INSERTAR_TBL_EQUIPO_OBSERVACION", TxtPlanningAsigProd.Text, OBSERVACION.Value, true);

                    }
                    else
                    {
                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "La Observación " + OBSERVACION.Text + " Ya ha sido agregado a la campaña.";
                        Mensajes_Productos();
                        return;
                    }

                }
            }

            this.Session["encabemensa"] = "Sr. Usuario";
            this.Session["cssclass"] = "MensajesSupConfirm";
            this.Session["mensaje"] = "Se ha creado con éxito la Observacion a la campaña ";
            Mensajes_Productos();

            chklist.Items.Clear();
            div_Elementos.Style.Value = "display:none";
        }
        #endregion





    }
}






