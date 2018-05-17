using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Lucky.Data;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;
using System.Drawing.Imaging;
using System.Drawing;
using Lucky.CFG.Tools;
using Lucky.Business.Common.Application;
using Lucky.Entity.Common.Security;
using System.Data.OleDb;
using Lucky.Entity.Common.Application.JavaMovil;

namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Reporte_Pablo : System.Web.UI.Page
    {

        #region Declaracion de Campañas
        private int compañia = 1572;
        private string pais="589";
        private static string static_channel;
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        #endregion 

        protected void Page_Load(object sender, EventArgs e)
        {
            //RegistrarPresencia();
            RegistrarRegistroPDV();
        }

        public void RegistrarPresencia()
        {
            #region 
            //Lucky.Business.Common.JavaMovil.BL_Registar_Presencia oBL_Registar_Presencia = new Lucky.Business.Common.JavaMovil.BL_Registar_Presencia();
            /////Elementos de Visibilidad//// - opcion 03
            //List<E_Reporte_Presencia> temp = new List<E_Reporte_Presencia>();
            //E_Reporte_Presencia a = new E_Reporte_Presencia();
            //E_Reporte_Presencia_Detalle det = new E_Reporte_Presencia_Detalle();
            //List<E_Reporte_Presencia_Detalle> listdetalle = new List<E_Reporte_Presencia_Detalle>();
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "01", ValorDetalle = "1" });
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "02", ValorDetalle = "1" });
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "03", ValorDetalle = "1" });
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "04", ValorDetalle = "1" });
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "05", ValorDetalle = "1" });
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "06", ValorDetalle = "1" });
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "07", ValorDetalle = "1" });
            //temp.Add(new E_Reporte_Presencia() { Person_id = "9087", Perfil_id = "0017", Equipo_id = "813622482010", Cliente_id = "1561", ClientPDV_Code = "LM025-084", Categoria_id = "", Marca_id = "", OpcionReporte_id = "03", FechaRegistro = "27/10/2011 17:32:28", Latitud = "-72660464", Longitud = "-462304096", OrigenCoordenada = "C", TipoCanal = "MY", Comentario = "", PresenciaDetalle = listdetalle });
            //oBL_Registar_Presencia.registrarpresencia(temp);
            #endregion

            //Lucky.Business.Common.JavaMovil.BL_ReportesColgate_May oBL_ReportesColgate_May = new Lucky.Business.Common.JavaMovil.BL_ReportesColgate_May();
            
            //List<E_Reporte_Presencia> temp = new List<E_Reporte_Presencia>();
            
            //E_Reporte_Presencia a = new E_Reporte_Presencia();
            //E_Reporte_Presencia_Detalle det = new E_Reporte_Presencia_Detalle();

            ///Reporte Presencia
            
            //List<E_Reporte_Presencia_Detalle> listDetalle = new List<E_Reporte_Presencia_Detalle>();
            //listDetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "01", ValorDetalle = "1" });
            //listDetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "02", ValorDetalle = "1" });
            //listDetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "02", ValorDetalle = "1" });
            //temp.Add(new E_Reporte_Presencia() { Person_id = "9087", Perfil_id = "0017", Equipo_id = "813622482010", Cliente_id = "1561", ClientPDV_Code = "LM025-084", Categoria_id = "", Marca_id = "", OpcionReporte_id = "03", FechaRegistro = "27/10/2011 17:32:28", Latitud = "-72660464", Longitud = "-462304096", OrigenCoordenada = "C", TipoCanal = "MY", Comentario = "", PresenciaDetalle = listDetalle });
            
            ////Reporte Fotográfico

            //List<E_Reporte_Fotografico> tempFoto = new List<E_Reporte_Fotografico>();
            //E_Reporte_Fotografico oE_Reporte_Fotografico = new E_Reporte_Fotografico();
            //tempFoto.Add(new E_Reporte_Fotografico() { Person_id = "9087", Perfil_id = "0017", Equipo_id = "813622482010", Cliente_id = "1561", ClientePDV_Code = "LM025-084", TipoReporte_id = "23", Categoria_id = "9994", Marca_id = "", FechaRegistro = "27/10/2011 17:32:28", Latitud = "-72660464", Longitud = "-462304096", OrigenCoordenada = "C", Comentario = "", Foto1= "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" });

            /////Registro de Visita
            //E_Visita oE_VisitaFin = new E_Visita();
            //oE_VisitaFin.PersonId = 9087;
            //oE_VisitaFin.PerfilId = "0017";
            //oE_VisitaFin.EquipoId = "813622482010";
            //oE_VisitaFin.ClienteId = "1561";
            //oE_VisitaFin.ClientPDV_Code = "LM025-084";
            //oE_VisitaFin.NoVisitaId = "";
            //oE_VisitaFin.FechaIni = "12/01/2012 18:32:29";
            //oE_VisitaFin.LatitudInicio = "-72660464";
            //oE_VisitaFin.LongitudInicio = "-462304096";
            //oE_VisitaFin.OrigenInicio = "C";
            //oE_VisitaFin.FechaFin = "12/01/2012 18:32:39";
            //oE_VisitaFin.LatitudFin = "-72660464";
            //oE_VisitaFin.LongitudFin = "-462304096";
            //oE_VisitaFin.OrigenFin = "C";


            //oBL_ReportesColgate_May.registrarPresencia_May(temp, tempFoto, oE_VisitaFin);

            //oBL_ReportesColgate_May.registrarPresencia_May(temp, "");


            #region Ini - Fin de Día, Visita
            //Lucky.Business.Common.JavaMovil.BL_Marcacion oBL_Marcacion = new Lucky.Business.Common.JavaMovil.BL_Marcacion();
             
            //Conexion Ocoon = new Conexion();
            //Conexion con = new Conexion(2);

            //INSETAR MARCACION INPUT = <lr><p id="1" cm="9087" cp="0017" 
            //ce="813622482010" cc="1561" e="1" m="" fi="12/01/2012 18:31:53" 
            //lai="-72660464" loi="-462304096" oci="C" ff="" laf="" lof="" ocf="" ee="I" /></lr>

            ////Marcación de Inicio y fin de Día

            ///////////////////////////////
            ///////Inicio de Día
            ///////////////////////////////

            ///////////////INICIO
            //E_Marcacion oE_Marcacion = new E_Marcacion();
            //oE_Marcacion.PersonId = 9087;
            //oE_Marcacion.PerfilId = "0017";
            //oE_Marcacion.EquipoId = "813622482010";
            //oE_Marcacion.ClienteId = "1561";
            //oE_Marcacion.EstadoId = "1";
            //oE_Marcacion.MotivoId = "";
            //oE_Marcacion.FechaIni = "12/01/2012 18:31:53";
            //oE_Marcacion.LatitudInicio = "-72660464";
            //oE_Marcacion.LongitudInicio = "-462304096";
            //oE_Marcacion.OrigenInicio = "C";
            //oE_Marcacion.FechaFin = "";
            //oE_Marcacion.LatitudFin = "";
            //oE_Marcacion.LongitudFin = "";
            //oE_Marcacion.OrigenFin = "";

            //oBL_Marcacion.registrarMarcacion(oE_Marcacion);

            /////////////FIN


            ////////////////////////////////
            /////////Fin de Día
            ////////////////////////////////
            //:INSETAR MARCACION INPUT = <lr><p id="1" cm="9087" cp="0017" ce="813622482010" 
            //cc="1561" e="1" m="" fi="12/01/2012 18:31:53" lai="-72660464" loi="-462304096" 
            //oci="C" ff="12/01/2012 18:32:08" laf="-72660464" lof="-462304096" ocf="C" ee="F" /></lr>


            ///////////////INICIO

            //E_Marcacion oE_MarcacionFin = new E_Marcacion();
            //oE_MarcacionFin.PersonId = 9087;
            //oE_MarcacionFin.PerfilId = "0017";
            //oE_MarcacionFin.EquipoId = "813622482010";
            //oE_MarcacionFin.ClienteId = "1561";
            //oE_MarcacionFin.EstadoId = "1";
            //oE_MarcacionFin.MotivoId = "";
            //oE_MarcacionFin.FechaIni = "12/01/2012 18:31:53";
            //oE_MarcacionFin.LatitudInicio = "-72660464";
            //oE_MarcacionFin.LongitudInicio = "-462304096";
            //oE_MarcacionFin.OrigenInicio = "C";
            //oE_MarcacionFin.FechaFin = "12/01/2012 18:32:08";
            //oE_MarcacionFin.LatitudFin = "-72660464";
            //oE_MarcacionFin.LongitudFin = "462304096";
            //oE_MarcacionFin.OrigenFin = "C";
            
            //oBL_Marcacion.registrarMarcacion(oE_MarcacionFin);
            ////////////FIN





            //////////////////////////
            ////////////Inicio de Visita -- Se guarda en la tabla -- tbl_reg_visita
            ////////////////////////
            /////
            //:INSETAR VISITA INPUT = <lr><p id="1" cm="9087" cp="0017" 
            //ce="813622482010" cc="1561" pv="LM025-084" fi="12/01/2012 18:32:29" lai="-72660464" 
            //loi="-462304096" oci="C" ff="" laf="" lof="" ocf="" e="I" cnv="" /></lr>
            ///////

            ///INICIO

            //Lucky.Business.Common.JavaMovil.BL_Visita oBL_Visita = new Lucky.Business.Common.JavaMovil.BL_Visita();
            //E_Visita oE_Visita = new E_Visita();
            //oE_Visita.PersonId = 9087;
            //oE_Visita.PerfilId = "0017";
            //oE_Visita.EquipoId = "813622482010";
            //oE_Visita.ClienteId = "1561";
            //oE_Visita.ClientPDV_Code = "LM025-084";
            //oE_Visita.NoVisitaId = "";
            //oE_Visita.FechaIni = "12/01/2012 18:32:29";
            //oE_Visita.LatitudInicio = "-72660464";
            //oE_Visita.LongitudInicio = "-462304096";
            //oE_Visita.OrigenInicio = "C";
            //oE_Visita.FechaFin = "";
            //oE_Visita.LatitudFin = "";
            //oE_Visita.LongitudFin = "";
            //oE_Visita.OrigenFin = "";


            //oBL_Visita.registrarVisita(oE_Visita);

            ///FIN


            //////////////////////////
            ////////////Fin de Visita
            ////////////////////////
            /////
            //:INSETAR VISITA INPUT = <lr><p id="1" cm="9087" cp="0017" ce="813622482010" 
            //cc="1561" pv="LM025-084" fi="12/01/2012 18:32:29" lai="-72660464" 
            //loi="-462304096" oci="C" ff="12/01/2012 18:32:39" laf="-72660464" 
            //lof="-462304096" ocf="C" e="F" cnv="" /></lr>
            ///////

            ////INICIO
            //E_Visita oE_VisitaFin = new E_Visita();
            //oE_VisitaFin.PersonId = 9087;
            //oE_VisitaFin.PerfilId = "0017";
            //oE_VisitaFin.EquipoId = "813622482010";
            //oE_VisitaFin.ClienteId = "1561";
            //oE_VisitaFin.ClientPDV_Code = "LM025-084";
            //oE_VisitaFin.NoVisitaId = "";
            //oE_VisitaFin.FechaIni = "12/01/2012 18:32:29";
            //oE_VisitaFin.LatitudInicio = "-72660464";
            //oE_VisitaFin.LongitudInicio = "-462304096";
            //oE_VisitaFin.OrigenInicio = "C";
            //oE_VisitaFin.FechaFin = "12/01/2012 18:32:39";
            //oE_VisitaFin.LatitudFin = "-72660464";
            //oE_VisitaFin.LongitudFin = "-462304096";
            //oE_VisitaFin.OrigenFin = "C";


            //oBL_Visita.registrarVisita(oE_VisitaFin);
            //FIN
            #endregion

            #region Pruebas Registro Cab y Det - Presencia Colgate pSalas 12/01/2012

            //Lucky.Business.Common.JavaMovil.BL_Registar_Presencia oBL_Registar_Presencia = new Lucky.Business.Common.JavaMovil.BL_Registar_Presencia();
            
            
            /////Elementos de Visibilidad//// - opcion 03
            
            //List<E_Reporte_Presencia> temp = new List<E_Reporte_Presencia>();

            //E_Reporte_Presencia a = new E_Reporte_Presencia();
            //E_Reporte_Presencia_Detalle det = new E_Reporte_Presencia_Detalle();
            //List<E_Reporte_Presencia_Detalle> listdetalle = new List<E_Reporte_Presencia_Detalle>();
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "01", ValorDetalle = "1" });
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "02", ValorDetalle = "1" });
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "03", ValorDetalle = "1" });
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "04", ValorDetalle = "1" });
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "05", ValorDetalle = "1" });
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "06", ValorDetalle = "1" });
            //listdetalle.Add(new E_Reporte_Presencia_Detalle() { Codigo = "07", ValorDetalle = "1" });
            
            //temp.Add(new E_Reporte_Presencia() { Person_id = "9087", Perfil_id = "0017", Equipo_id = "813622482010", Cliente_id = "1561", ClientPDV_Code = "LM025-084", Categoria_id = "", Marca_id = "", OpcionReporte_id = "03", FechaRegistro = "27/10/2011 17:32:28", Latitud = "-72660464", Longitud = "-462304096", OrigenCoordenada = "C", TipoCanal = "MY", Comentario = "", PresenciaDetalle = listdetalle });
            

            //oBL_Registar_Presencia.registrarpresencia(temp);

            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////


            /////Presencia Colgate//// - Opcion 04
            /////
            //List<E_Reporte_Presencia> tempb = new List<E_Reporte_Presencia>();

            //E_Reporte_Presencia b = new E_Reporte_Presencia();
            //E_Reporte_Presencia_Detalle detb = new E_Reporte_Presencia_Detalle();
            //List<E_Reporte_Presencia_Detalle> listdetalleb = new List<E_Reporte_Presencia_Detalle>();
            //listdetalleb.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COL0001", ValorDetalle = "20" });
            //listdetalleb.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COL0002", ValorDetalle = "21" });
            //listdetalleb.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COL0003", ValorDetalle = "22" });
            //listdetalleb.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COL0004", ValorDetalle = "23" });
            //listdetalleb.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COL0005", ValorDetalle = "24" });
            //listdetalleb.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COL0006", ValorDetalle = "25" });
            //listdetalleb.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COL0007", ValorDetalle = "26" });
            
            //tempb.Add(new E_Reporte_Presencia() { Person_id = "9087", Perfil_id = "0017", Equipo_id = "813622482010", Cliente_id = "1561", ClientPDV_Code = "LM025-084", Categoria_id = "", Marca_id = "", OpcionReporte_id = "04", FechaRegistro = "27/10/2011 17:32:28", Latitud = "-72660464", Longitud = "-462304096", OrigenCoordenada = "C", TipoCanal = "MY", Comentario = "", PresenciaDetalle = listdetalleb });
            
            //oBL_Registar_Presencia.registrarpresencia(tempb);
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////


            /////Presencia Competidora//// - Opcion 05
            //List<E_Reporte_Presencia> tempc = new List<E_Reporte_Presencia>();

            //E_Reporte_Presencia c = new E_Reporte_Presencia();
            //E_Reporte_Presencia_Detalle detc = new E_Reporte_Presencia_Detalle();
            //List<E_Reporte_Presencia_Detalle> listdetallec = new List<E_Reporte_Presencia_Detalle>();
            //listdetallec.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COM0001", ValorDetalle = "27" });
            //listdetallec.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COM0002", ValorDetalle = "28" });
            //listdetallec.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COM0003", ValorDetalle = "29" });
            //listdetallec.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COM0004", ValorDetalle = "30" });
            //listdetallec.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COM0005", ValorDetalle = "31" });
            //listdetallec.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COM0006", ValorDetalle = "32" });
            //listdetallec.Add(new E_Reporte_Presencia_Detalle() { Codigo = "COM0007", ValorDetalle = "33" });
            
            //tempc.Add(new E_Reporte_Presencia() { Person_id = "9087", Perfil_id = "0017", Equipo_id = "813622482010", Cliente_id = "1561", ClientPDV_Code = "LM025-084", Categoria_id = "", Marca_id = "", OpcionReporte_id = "05", FechaRegistro = "27/10/2011 17:32:28", Latitud = "-72660464", Longitud = "-462304096", OrigenCoordenada = "C", TipoCanal = "MY", Comentario = "", PresenciaDetalle = listdetallec });
            // oBL_Registar_Presencia.registrarpresencia(tempc);

            // //////////////////////////////////////////////////////////////////////////////////////
            // //////////////////////////////////////////////////////////////////////////////////////
            // //////////////////////////////////////////////////////////////////////////////////////
            // //////////////////////////////////////////////////////////////////////////////////////
            // //////////////////////////////////////////////////////////////////////////////////////

            /////Presencia Exhibidor//// - Opcion 06
            //List<E_Reporte_Presencia> tempd = new List<E_Reporte_Presencia>();

            //E_Reporte_Presencia d = new E_Reporte_Presencia();
            //E_Reporte_Presencia_Detalle detd = new E_Reporte_Presencia_Detalle();
            //List<E_Reporte_Presencia_Detalle> listdetalled = new List<E_Reporte_Presencia_Detalle>();
            //listdetalled.Add(new E_Reporte_Presencia_Detalle() { Codigo = "MISIL001", ValorDetalle = "34" });
            //listdetalled.Add(new E_Reporte_Presencia_Detalle() { Codigo = "MISIL002", ValorDetalle = "35" });
            
           
            //tempd.Add(new E_Reporte_Presencia() { Person_id = "9087", Perfil_id = "0017", Equipo_id = "813622482010", Cliente_id = "1561", ClientPDV_Code = "LM025-084", Categoria_id = "", Marca_id = "", OpcionReporte_id = "06", FechaRegistro = "27/10/2011 17:32:28", Latitud = "-72660464", Longitud = "-462304096", OrigenCoordenada = "C", TipoCanal = "MY", Comentario = "", PresenciaDetalle = listdetalled });
            //oBL_Registar_Presencia.registrarpresencia(tempd);

            //List<E_Reporte_Presencia> tempe = new List<E_Reporte_Presencia>();

            //E_Reporte_Presencia e = new E_Reporte_Presencia();
            //E_Reporte_Presencia_Detalle dete = new E_Reporte_Presencia_Detalle();
            //List<E_Reporte_Presencia_Detalle> listdetallee = new List<E_Reporte_Presencia_Detalle>();
            //listdetallee.Add(new E_Reporte_Presencia_Detalle() { Codigo = "01", ValorDetalle = "X" });
            //listdetallee.Add(new E_Reporte_Presencia_Detalle() { Codigo = "02", ValorDetalle = "X" });

            
            //tempe.Add(new E_Reporte_Presencia() { Person_id = "9087", Perfil_id = "0017", Equipo_id = "813622482010", Cliente_id = "1561", ClientPDV_Code = "LM025-084", Categoria_id = "", Marca_id = "", OpcionReporte_id = "07", FechaRegistro = "27/10/2011 17:32:28", Latitud = "-72660464", Longitud = "-462304096", OrigenCoordenada = "C", TipoCanal = "MY", Comentario = "", PresenciaDetalle = listdetallee });
            //oBL_Registar_Presencia.registrarpresencia(tempe);

            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////


            /////Presencia Observaciones//// - Opcion 08
            //List<E_Reporte_Presencia> tempf = new List<E_Reporte_Presencia>();

            //E_Reporte_Presencia f = new E_Reporte_Presencia();
            //E_Reporte_Presencia_Detalle detf = new E_Reporte_Presencia_Detalle();
            //List<E_Reporte_Presencia_Detalle> listdetallef = new List<E_Reporte_Presencia_Detalle>();
            //listdetallef.Add(new E_Reporte_Presencia_Detalle() { Codigo = "", ValorDetalle = "" });
           
            //tempf.Add(new E_Reporte_Presencia() { Person_id = "9087", Perfil_id = "0017", Equipo_id = "813622482010", Cliente_id = "1561", ClientPDV_Code = "LM025-084", Categoria_id = "", Marca_id = "", OpcionReporte_id = "08", FechaRegistro = "27/10/2011 17:32:28", Latitud = "-72660464", Longitud = "-462304096", OrigenCoordenada = "C", TipoCanal = "MY", Comentario = "Comentario Ene 2011", PresenciaDetalle = listdetallef });
            
            //oBL_Registar_Presencia.registrarpresencia(tempf);

            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            #endregion



        }

        public void RegistrarRegistroPDV() {

            Lucky.Business.Common.JavaMovil.BL_ReportesColgate_Bodega oBL_ReportesColgate_Bodega = new Lucky.Business.Common.JavaMovil.BL_ReportesColgate_Bodega();
            List<Lucky.Entity.Common.Application.JavaMovil.E_Reporte_RegistroPDV> temp = new List<Lucky.Entity.Common.Application.JavaMovil.E_Reporte_RegistroPDV>();
            E_Reporte_RegistroPDV a = new E_Reporte_RegistroPDV();
            E_Reporte_RegistroPDV_Detalle b = new E_Reporte_RegistroPDV_Detalle();

            List<E_Reporte_RegistroPDV_Detalle_Opcion> listOpcion = new List<E_Reporte_RegistroPDV_Detalle_Opcion>();
            listOpcion.Add(new E_Reporte_RegistroPDV_Detalle_Opcion {  id_Opcion="02"});
            listOpcion.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "03" });
            listOpcion.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "04" });
            listOpcion.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "05" });


            List<E_Reporte_RegistroPDV_Detalle_Opcion> listOpcion_02 = new List<E_Reporte_RegistroPDV_Detalle_Opcion>();
            listOpcion_02.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "20" });
            listOpcion_02.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "21" });
            listOpcion_02.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "22" });
            listOpcion_02.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "23" });

            List<E_Reporte_RegistroPDV_Detalle_Opcion> listOpcion_03 = new List<E_Reporte_RegistroPDV_Detalle_Opcion>();
            listOpcion_03.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "30" });
            listOpcion_03.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "31" });
            listOpcion_03.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "32" });
            listOpcion_03.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "33" });

            List<E_Reporte_RegistroPDV_Detalle_Opcion> listOpcion_04 = new List<E_Reporte_RegistroPDV_Detalle_Opcion>();
            listOpcion_04.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "40" });
            listOpcion_04.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "41" });
            listOpcion_04.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "42" });
            listOpcion_04.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "43" });
            listOpcion_04.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "44" });
            listOpcion_04.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "45" });

            List<E_Reporte_RegistroPDV_Detalle_Opcion> listOpcion_05 = new List<E_Reporte_RegistroPDV_Detalle_Opcion>();
            listOpcion_05.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "50" });
            listOpcion_05.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "51" });
            listOpcion_05.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "52" });
            listOpcion_05.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "53" });
            listOpcion_05.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "54" });

            List<E_Reporte_RegistroPDV_Detalle_Opcion> listOpcion_06 = new List<E_Reporte_RegistroPDV_Detalle_Opcion>();
            listOpcion_06.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "60" });
            listOpcion_06.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "61" });
            listOpcion_06.Add(new E_Reporte_RegistroPDV_Detalle_Opcion { id_Opcion = "62" });



            List<E_Reporte_RegistroPDV_Detalle> listDetalle = new List<E_Reporte_RegistroPDV_Detalle>();
            listDetalle.Add(new E_Reporte_RegistroPDV_Detalle() { id_Pregunta = "01", Id_Opcion = listOpcion });
            listDetalle.Add(new E_Reporte_RegistroPDV_Detalle() { id_Pregunta = "02", Id_Opcion = listOpcion_02 });
            listDetalle.Add(new E_Reporte_RegistroPDV_Detalle() { id_Pregunta = "03", Id_Opcion = listOpcion_03 });

            List<E_Reporte_RegistroPDV_Detalle> listDetalle_02 = new List<E_Reporte_RegistroPDV_Detalle>();
            listDetalle_02.Add(new E_Reporte_RegistroPDV_Detalle() { id_Pregunta = "01", Id_Opcion = listOpcion_04 });
            listDetalle_02.Add(new E_Reporte_RegistroPDV_Detalle() { id_Pregunta = "02", Id_Opcion = listOpcion_05 });
            listDetalle_02.Add(new E_Reporte_RegistroPDV_Detalle() { id_Pregunta = "04", Id_Opcion = listOpcion_06 });


            temp.Add(new E_Reporte_RegistroPDV() { Person_id = "uaqp2", 
                Equipo_id = "012011092692011", Cliente_id = "1561", 
                ClientePDV_Code = "30986", Nombre = "Nombre BodegaPrueba", 
                Telefono = "56423453", FechaRegistro = "17/02/2012 15:34:12", 
                Latitud = "-72660464", Longitud = "-462304096", OrigenCoordenada = "C", 
                IdImplementaPDV = "01", RegistroPDV_Detalle = listDetalle });

            //temp.Add(new E_Reporte_RegistroPDV() { Person_id = "uaqp2", Equipo_id = "012011092692011", 
            //    Cliente_id = "1561", ClientePDV_Code = "32256", Nombre = "Nombre BodegaPrueba_02", 
            //    Telefono = "99899876", FechaRegistro = "15/02/2012 15:52:28", Latitud = "-72660464", 
            //    Longitud = "-462304096", OrigenCoordenada = "C", IdImplementaPDV = "00", 
            //    RegistroPDV_Detalle = listDetalle_02 });
            
            

            oBL_ReportesColgate_Bodega.registrarRegistroPDV_Bodega(temp);


        }
 


      }

       
}
