using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Lucky.Data;
using System.IO;
using Lucky.CFG.Util;
using Telerik.Web.UI;
using Lucky.Business.Common.Application;
using Lucky.Business.Common.Security;

using Lucky.Data.Common.Application;
using Lucky.Data.Common.Security;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Security;
using Lucky.Entity.Common.Application.Security;

using Lucky.CFG.Messenger;
using Lucky.CFG.Tools;
using System;
using System.Collections;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;

using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class LevantamientoMaterialPOP : System.Web.UI.Page
    {
        #region Declaracion de variables generales
        Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private int compañia;
        private string pais;
        private int iid_rptePbl;
        #endregion


        private OPE_REPORTE_MATERIAL_POP orepormaterialPOP = new OPE_REPORTE_MATERIAL_POP();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IfCargaMasivaGProductos.Attributes["src"] = "CargaMasivaLevantamiento.aspx";
                try
                {

                    llenaConsultaplanningAASS();
                    cargaruscarBRecogerPor();
                    //comboDistribuidora();
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


        private void activarControlesLP()
        {

            cmbcanal.Enabled = true;
            cmbplanning.Enabled = true;
            txt_fechaActual.Enabled = true;
            //cmbAño.Enabled = true;
            //cmbmes.Enabled = true;
            cmbRecoger.Enabled = true;


        }
        private void desactivarControleLP()
        {

            cmbcanal.Enabled = false;
            cmbplanning.Enabled = false;
            txt_fechaActual.Enabled = false;
            //cmbAño.Enabled = true;
            //cmbmes.Enabled = true;
            cmbRecoger.Enabled = false;
            GvLPublicaciones.Enabled = false;

        }
        private void crearActivarbotonesLP()
        {
            BtnCrearUsuInfo.Visible = false;
            BtnGuardarUsuInfo.Visible = true;
            BtnConsultarUsuInfo.Visible = false;
            //BtnEditarUsuInfo.Visible = false;
            //BtnActuUsuInfo.Visible = false;
            BtnCancelUsuInfo.Visible = true;
            BtnCargaMasivaLP.Visible = false;
        }
        private void ConsultarActivarbotonesLP()
        {
            BtnCrearUsuInfo.Visible = false;
            BtnGuardarUsuInfo.Visible = false;
            BtnConsultarUsuInfo.Visible = false;
            //BtnEditarUsuInfo.Visible = false;
            BtnActuUsuInfo.Visible = true;
            BtnCancelUsuInfo.Visible = true;

        }
        private void SavelimpiarcontrolesLP()
        {
            cmbcanal.Text = "0";
            cmbplanning.Text = "0";
            txt_fechaActual.Text = "";
            //cmbAño.Enabled = true;
            //cmbmes.Enabled = true;
            cmbRecoger.Text = "0";
            GvLPublicaciones.DataBind();




        }
        private void saveActivarbotonesLP()
        {
            BtnCrearUsuInfo.Visible = true;
            BtnGuardarUsuInfo.Visible = false;
            BtnConsultarUsuInfo.Visible = true;
            //BtnEditarUsuInfo.Visible = false;
            BtnActuUsuInfo.Visible = false;
            BtnCancelUsuInfo.Visible = true;

        }
        private void CargaMActivarbotonesLP()
        {
            BtnCrearUsuInfo.Visible = false;
            BtnGuardarUsuInfo.Visible = false;
            BtnConsultarUsuInfo.Visible = true;
            //BtnEditarUsuInfo.Visible = false;
            BtnActuUsuInfo.Visible = false;
            BtnCancelUsuInfo.Visible = true;
            BtnCargaMasivaLP.Visible = true;

        }

        //private void SavelimpiarcontrolesSector()
        //{
        //    TxtCodSector.Text = "";
        //    TxtNomSector.Text = "";
        //    CmbmallaSector.Text = "0";
        //    CmbCliente.Text = "0";
        //    RbtnStatusSector.Items[0].Selected = true;
        //    RbtnStatusSector.Items[1].Selected = false;

        //    CmbBmallaSector.Text = "0";
        //    TxtBNomSector.Text = "";



        //}
        //private void saveActivarbotonesSector()
        //{
        //    BtnCrearSector.Visible = true;
        //    BtnsaveSector.Visible = false;
        //    BtnSearchSector.Visible = true;
        //    BtnEditSector.Visible = false;
        //    BtnActualizaSector.Visible = false;
        //    BtnCancelSector.Visible = true;
        //}
        //private void EditarActivarbotonesSector()
        //{
        //    BtnCrearSector.Visible = false;
        //    BtnsaveSector.Visible = false;
        //    BtnSearchSector.Visible = true;
        //    BtnEditSector.Visible = false;
        //    BtnActualizaSector.Visible = true;
        //    BtnCancelSector.Visible = true;
        //    PregSector.Visible = false;
        //    AregSector.Visible = false;
        //    SregSector.Visible = false;
        //    UregSector.Visible = false;

        //}
        //private void EditarActivarControlesSector()
        //{
        //    TxtCodSector.Enabled = false;
        //    TxtNomSector.Enabled = true;
        //    CmbmallaSector.Enabled = true;
        //    CmbCliente.Enabled = true;
        //    RbtnStatusSector.Enabled = true;
        //    TabPanelSegmentos.Enabled = false;
        //    TabPanelTipoAgrupación.Enabled = false;
        //    TabPanelAgrupaciónComercial.Enabled = false;
        //    Panel_Mallas.Enabled = false;
        //    Panel_Sector.Enabled = true;
        //    TabPanelPDV.Enabled = false;
        //    PanelPDVCliente.Enabled = false;
        //    TabDistribuidora.Enabled = false;
        //}
        //private void BuscarActivarbotnesSector()
        //{
        //    BtnCrearSector.Visible = false;
        //    BtnsaveSector.Visible = false;
        //    BtnSearchSector.Visible = true;
        //    BtnEditSector.Visible = true;
        //    BtnActualizaSector.Visible = false;
        //    BtnCancelSector.Visible = true;

        //}
        //private void LlenacomboClienteSector()
        //{
        //    DataSet ds = new DataSet();
        //    ds = owsadministrativo.llenaCombosSector(0);
        //    //se llena cliente producto en Sector
        //    CmbCliente.DataSource = ds.Tables[2];
        //    CmbCliente.DataTextField = "Company_Name";
        //    CmbCliente.DataValueField = "Company_id";
        //    CmbCliente.DataBind();
        //    CmbCliente.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

        //}
        //private void LlenacomboBuscarClienteSector()
        //{
        //    DataSet ds = new DataSet();
        //    ds = owsadministrativo.llenaCombosSector(0);
        //    //se llena cliente producto en Sector
        //    cmbBClienteSector.DataSource = ds.Tables[2];
        //    cmbBClienteSector.DataTextField = "Company_Name";
        //    cmbBClienteSector.DataValueField = "Company_id";
        //    cmbBClienteSector.DataBind();
        //    cmbBClienteSector.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

        //}
        //private void LlenacomboMallasenSector()
        //{
        //    DataSet ds = new DataSet();
        //    ds = owsadministrativo.llenaCombosSector(Convert.ToInt32(CmbCliente.SelectedValue));
        //    //se llena mallas producto en Sector
        //    CmbmallaSector.DataSource = ds.Tables[1];
        //    CmbmallaSector.DataTextField = "malla";
        //    CmbmallaSector.DataValueField = "id_malla";
        //    CmbmallaSector.DataBind();
        //    CmbmallaSector.Items.Insert(0, new ListItem("<Seleccione..>", "0"));


        //}
        //private void LlenacomboBuscarMallasenSector()
        //{
        //    DataSet ds = new DataSet();
        //    ds = owsadministrativo.llenaCombosSector(Convert.ToInt32(cmbBClienteSector.SelectedValue));

        //    //se llena mallas producto en consultar Sector
        //    CmbBmallaSector.DataSource = ds.Tables[0];
        //    CmbBmallaSector.DataTextField = "malla";
        //    CmbBmallaSector.DataValueField = "id_malla";
        //    CmbBmallaSector.DataBind();
        //    // CmbBmallaSector.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
        //    ds = null;


        //}crearActivarbotonesSector()

        protected void CargarCombo_Channel()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            pais = this.Session["scountry"].ToString();


            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CHANNEL", pais, compañia);
            if (dt.Rows.Count > 0)
            {
                cmbcanal.DataSource = dt;
                cmbcanal.DataValueField = "cod_Channel";
                cmbcanal.DataTextField = "Channel_Name";
                cmbcanal.DataBind();
                cmbcanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }

        }
        //private void cargarAños()
        //{
        //    DataTable dty = null;
        //    dty = Get_Administrativo.Get_ObtenerYears();
        //    if (dty.Rows.Count > 0)
        //    {
        //        cmbAño.DataSource = dty;
        //        cmbAño.DataValueField = "Years_Number";
        //        cmbAño.DataTextField = "Years_Number";
        //        cmbAño.DataBind();


        //        cmbAño.DataSource = dty;
        //        cmbAño.DataValueField = "Years_Number";
        //        cmbAño.DataTextField = "Years_Number";
        //        cmbAño.DataBind();
        //        cmbAño.Items.Insert(0, new ListItem("--Todos--", "0"));
        //    }
        //    else
        //    {
        //        dty = null;
        //    }
        //}
        //private void cargarMes()
        //    {
        //        DataTable dtm = Get_Administrativo.Get_ObtenerMeses();

        //        if (dtm.Rows.Count > 0)
        //        {
        //            cmbmes.DataSource = dtm;
        //            cmbmes.DataValueField = "codmes";
        //            cmbmes.DataTextField = "namemes";
        //            cmbmes.DataBind();
        //            cmbmes.Items.Insert(0, new ListItem("--Todos--", "0"));

        //        }
        //    }
        private void cargarRecogerPor()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_RECOGERPOR");

            cmbRecoger.DataSource = dt;
            cmbRecoger.DataValueField = "id_RecogerPor";
            cmbRecoger.DataTextField = "recoger_por";
            cmbRecoger.DataBind();


        }
        private void cargaruscarBRecogerPor()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_RECOGERPOR");

            cmbBRecogidoPof.DataSource = dt;
            cmbBRecogidoPof.DataValueField = "id_RecogerPor";
            cmbBRecogidoPof.DataTextField = "recoger_por";
            cmbBRecogidoPof.DataBind();


        }
        private void llenarGrilla()
        {


            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENAGRILLALPUBLICACIONES");
            GvLPublicaciones.DataSource = dt;
            GvLPublicaciones.DataBind();

            llenarMarcagrilla();
            llenarpdvgrilla();
            llenarpromosiongrilla();
            llenartipoPOP();


        }
        //private void llenarGrillaXsku()
        //{
        //    DataSet ds = null;
        //    Conexion Ocoon = new Conexion();

        //    ds = Ocoon.ejecutarDataSet("UP_WEBXPLORA_OPE_LLENAPROMOMATERIALPOP", cmbplanning.SelectedValue);
        //    for (int i = 0; i <= GvLPublicaciones.Rows.Count - 1; i++)
        //    {

        //        ((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbsku")).DataSource = ds.Tables[0];
        //        ((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbsku")).DataTextField = "Product_Name";
        //        ((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbsku")).DataValueField = "id_Product";
        //        ((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbsku")).DataBind();
        //        ((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbsku")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));

        //    }
        //}
        //private void llenarGrillaXFamilia()
        //{
        //    DataSet ds = null;
        //    Conexion Ocoon = new Conexion();

        //    ds = Ocoon.ejecutarDataSet("UP_WEBXPLORA_AD_LLENAGRILLAXSKU", cmbplanning.SelectedValue);

        //    for (int i = 0; i <= GvLPublicaciones.Rows.Count - 1; i++)
        //    {

        //        ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbFamilia")).DataSource = ds.Tables[1];
        //        ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbFamilia")).DataTextField = "name_Family";
        //        ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbFamilia")).DataValueField = "id_ProductFamily";
        //        ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbFamilia")).DataBind();
        //        ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbFamilia")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));

        //    }
        //}
        private void llenarpdvgrilla()
        {
            DataSet ds = null;
            Conexion Ocoon = new Conexion();

            ds = Ocoon.ejecutarDataSet("UP_WEBXPLORA_AD_LLENAPDVTACTIVIDAD_EXHI_IMPULSO", cmbplanning.SelectedValue);
            for (int i = 0; i <= GvLPublicaciones.Rows.Count - 1; i++)
            {

                ((DropDownList)GvLPublicaciones.Rows[i].Cells[6].FindControl("cmbCadena")).DataSource = ds.Tables[0];
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[6].FindControl("cmbCadena")).DataTextField = "pdv_Name";
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[6].FindControl("cmbCadena")).DataValueField = "id_ClientPDV";
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[6].FindControl("cmbCadena")).DataBind();
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[6].FindControl("cmbCadena")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            }
        }
        private void llenarpromosiongrilla()
        {
            DataSet ds = null;
            Conexion Ocoon = new Conexion();

            ds = Ocoon.ejecutarDataSet("UP_WEBXPLORA_OPE_LLENAPROMOMATERIALPOP",0);
            for (int i = 0; i <= GvLPublicaciones.Rows.Count - 1; i++)
            {

                ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbPromocion")).DataSource = ds.Tables[1];
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbPromocion")).DataTextField = "nombre";
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbPromocion")).DataValueField = "id_promocion";
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbPromocion")).DataBind();
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbPromocion")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            }
        }
        private void llenarMarcagrilla()
        {
            DataSet ds = null;
            Conexion Ocoon = new Conexion();

            ds = Ocoon.ejecutarDataSet("UP_WEBXPLORA_OPE_LLENAPROMOMATERIALPOP", cmbplanning.SelectedValue);
            for (int i = 0; i <= GvLPublicaciones.Rows.Count - 1; i++)
            {

                ((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbMarca")).DataSource = ds.Tables[0];
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbMarca")).DataTextField = "Name_Brand";
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbMarca")).DataValueField = "id_Brand";
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbMarca")).DataBind();
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbMarca")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            }
        }
        private void llenartipoPOP()
        {
            DataSet ds = null;
            Conexion Ocoon = new Conexion();

            ds = Ocoon.ejecutarDataSet("UP_WEBXPLORA_OPE_LLENAPROMOMATERIALPOP", cmbplanning.SelectedValue);
            for (int i = 0; i <= GvLPublicaciones.Rows.Count - 1; i++)
            {

                ((DropDownList)GvLPublicaciones.Rows[i].Cells[3].FindControl("cmbtipoPOP")).DataSource = ds.Tables[2];
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[3].FindControl("cmbtipoPOP")).DataTextField = "POP_name";
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[3].FindControl("cmbtipoPOP")).DataValueField = "id_MPointOfPurchase";
                ((DropDownList)GvLPublicaciones.Rows[i].Cells[3].FindControl("cmbtipoPOP")).DataBind();
                //((DropDownList)GvLPublicaciones.Rows[i].Cells[8].FindControl("cmbTipo")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            }
        }
        private void llenaplanningAASS()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            compañia = Convert.ToInt32(this.Session["companyid"]);
            string sidchannel = cmbcanal.SelectedValue;

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", 1241, compañia);

            cmbplanning.Items.Clear();


            if (dt.Rows.Count > 0)
            {
                cmbplanning.DataSource = dt;
                cmbplanning.DataValueField = "id_planning";
                cmbplanning.DataTextField = "Planning_Name";
                cmbplanning.DataBind();
                cmbplanning.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                cmbplanning.Enabled = true;
            }
        }
        private void MensajeAlerta()
        {

            ModalPopupAlertas.Show();
            BtnAceptarAlert.Focus();
            //ScriptManager.RegisterStartupScript(
            //    this, this.GetType(), "myscript", "alert('Debe ingresar todos los parametros con (*)');", true);
        }
        private void ConsultaGrillaMarca()
        {
            try
            {
                if (TexBFecha.Text=="")
                {
                    TexBFecha.Text = "01/01/1900";
                }
                DataSet ds = orepormaterialPOP.ConsultarGrillaLevaMaterialPOP(cmbBCampaña.SelectedValue, Convert.ToDateTime(TexBFecha.Text));

                if (ds.Tables[0].Rows.Count > 0)
                {

                    GvLPublicaciones.Columns[7].Visible = true;
                    GvLPublicaciones.DataSource = ds.Tables[0];
                    GvLPublicaciones.DataBind();
                    llenarMarcagrilla();
                    llenarpdvgrilla();
                    llenarpromosiongrilla();
                    llenartipoPOP();
                    for (int i = 0; i <= GvLPublicaciones.Rows.Count - 1; i++)
                    {
                        ((Label)GvLPublicaciones.Rows[i].Cells[0].FindControl("lblcodregistro")).Text = ds.Tables[0].Rows[i]["id_rpteMatPOP"].ToString().Trim();
                        txt_fechaActual.Text = ds.Tables[0].Rows[i]["Fec_Reg_BD"].ToString().Trim();
                        ((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbMarca")).Text = ds.Tables[0].Rows[i]["Id_brand"].ToString().Trim();
                        ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbPromocion")).Text = ds.Tables[0].Rows[i]["id_Tipo_Prom"].ToString().Trim();
                        ((DropDownList)GvLPublicaciones.Rows[i].Cells[3].FindControl("cmbtipoPOP")).Text = ds.Tables[0].Rows[i]["id_MPointOfPurchase"].ToString().Trim();
                        ((TextBox)GvLPublicaciones.Rows[i].Cells[4].FindControl("txt_Inicio_Actividad")).Text = ds.Tables[0].Rows[i]["fec_ini_act"].ToString().Trim();
                        ((TextBox)GvLPublicaciones.Rows[i].Cells[5].FindControl("txt_Fin_Actividad")).Text = ds.Tables[0].Rows[i]["fec_fin_act"].ToString().Trim();
                        ((DropDownList)GvLPublicaciones.Rows[i].Cells[6].FindControl("cmbCadena")).Text = ds.Tables[0].Rows[i]["ClientPDV_code"].ToString().Trim();

                    }

                    ds = null;
                }
                else 
                {
                    SavelimpiarcontrolesLP();
                    saveActivarbotonesLP();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                }
            }
            catch (Exception ex)
            {

            }



        }      
        private void llenaConsultaplanningAASS()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_LLENACOMBOCONSULTAPLANNINGLP");

            cmbBCampaña.DataSource = dt;
            cmbBCampaña.DataValueField = "id_Plannig";
            cmbBCampaña.DataTextField = "Planning_Name";
            cmbBCampaña.DataBind();
            cmbBCampaña.Items.Insert(0, new ListItem("<Seleccione..>", "0"));



        }
        protected void cmbplanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BtnCargaMasivaLP.Visible == true)
            {

                this.Session["Fecha"] = txt_fechaActual.Text;
                this.Session["Planning"] = cmbplanning.Text;
                //this.Session["RecogerPor"] = cmbRecoger.Text;
                ModalCMasiva.Show();
            }
            else
            {
                GvLPublicaciones.Visible = true;
                llenarGrilla();
            }

        }
     
        protected void cmbRecoger_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarGrilla();
        }
        protected void cmbBCampaña_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            ModalPopupLevantaPublicacion.Show();
        }
        protected void BtnCrearUsuInfo_Click(object sender, EventArgs e)
        {
            activarControlesLP();
            crearActivarbotonesLP();
            llenaplanningAASS();

        }
        protected void BtnGuardarUsuInfo_Click(object sender, EventArgs e)
        {
            //bool continuar = false;
            LblFaltantes.Text = "";
            if (cmbplanning.Text == "0" || txt_fechaActual.Text == "" )
            {
                if (cmbplanning.Text == "0" || cmbplanning.Text == "")
                {
                    LblFaltantes.Text = ". " + "seleccione Campaña";
                }

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar información ";
                MensajeAlerta();
                return;
            }




            try
            {


                for (int i = 0; i <= GvLPublicaciones.Rows.Count - 1; i++)
                {

                    if (((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbMarca")).Text == "0")
                    { }
                    else
                    {
                        EOPE_REPORTE_MATERIAL_POP oeLevanPublicaciones = orepormaterialPOP.RegistrarINFOLevantaMaterialPOP(Convert.ToInt32(this.Session["personid"]), cmbplanning.Text, Convert.ToInt32(this.Session["companyid"]), ((DropDownList)GvLPublicaciones.Rows[i].Cells[6].FindControl("cmbCadena")).Text, Convert.ToInt32(((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbMarca")).Text), Convert.ToInt32(((DropDownList)GvLPublicaciones.Rows[i].Cells[3].FindControl("cmbtipoPOP")).Text),
                        ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbPromocion")).Text, Convert.ToDateTime(((TextBox)GvLPublicaciones.Rows[i].Cells[3].FindControl("txt_Inicio_Actividad")).Text), Convert.ToDateTime(((TextBox)GvLPublicaciones.Rows[i].Cells[4].FindControl("txt_Fin_Actividad")).Text), DateTime.Now, Convert.ToDateTime(txt_fechaActual.Text), Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now, true);
                    }
                }

                //SavelimpiarcontrolesLP();
                saveActivarbotonesLP();
                desactivarControleLP();
                Alertas.CssClass = "MensajesCorrecto";
                LblFaltantes.Text = "Registros de Levantamiento de Material POP  fueron creados  con Exito";
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
        protected void BtnBuscarLevaP_Click(object sender, EventArgs e)
        {

            LblFaltantes.Text = "";


            if (cmbBCampaña.Text == "0" )
            {
                if (cmbBCampaña.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Campaña";
                }
                //if ( TexBFecha.Text == "")
                //{
                //    LblFaltantes.Text = LblFaltantes.Text + ". " + "Fecha";
                //}

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "seleccione Campaña";
                MensajeAlerta();
                ModalPopupLevantaPublicacion.Show();
                return;
            }

            llenaplanningAASS();
            cmbplanning.Text = cmbBCampaña.Text;
            this.Session["planning"] = cmbplanning.Text;
            //cargarRecogerPor();
            //cmbRecoger.Text = cmbBRecogidoPof.Text;
            cmbplanning.Enabled = false;
            ConsultaGrillaMarca();

            GvLPublicaciones.Enabled = true;

            cmbBCampaña.Text = "0";
            TexBFecha.Text = "";
            cmbBRecogidoPof.Items.Clear();
            ConsultarActivarbotonesLP();





        }
        protected void BtnActuUsuInfo_Click(object sender, EventArgs e)
        {
            try
            {


                for (int i = 0; i <= GvLPublicaciones.Rows.Count - 1; i++)
                {

                    if (((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbMarca")).Text == "0" )
                    { }
                    else
                    {

                      EOPE_REPORTE_MATERIAL_POP oeLevanPublicaciones = orepormaterialPOP.Actualizar_levaMaterialPOP(Convert.ToInt32(((Label)GvLPublicaciones.Rows[i].Cells[0].FindControl("lblcodregistro")).Text), ((DropDownList)GvLPublicaciones.Rows[i].Cells[6].FindControl("cmbCadena")).Text, Convert.ToInt32(((DropDownList)GvLPublicaciones.Rows[i].Cells[1].FindControl("cmbMarca")).Text), Convert.ToInt32(((DropDownList)GvLPublicaciones.Rows[i].Cells[3].FindControl("cmbtipoPOP")).Text),
                       ((DropDownList)GvLPublicaciones.Rows[i].Cells[2].FindControl("cmbPromocion")).Text, Convert.ToDateTime(((TextBox)GvLPublicaciones.Rows[i].Cells[4].FindControl("txt_Inicio_Actividad")).Text), Convert.ToDateTime(((TextBox)GvLPublicaciones.Rows[i].Cells[5].FindControl("txt_Fin_Actividad")).Text), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now, true);
                    }
                }


                //SavelimpiarcontrolesLP();
                saveActivarbotonesLP();
                desactivarControleLP();
                Alertas.CssClass = "MensajesCorrecto";
                LblFaltantes.Text = "Registros de Levantamiento de Material POP  fueron Actualizados con Exito";
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
        protected void BtnCancelUsuInfo_Click(object sender, EventArgs e)
        {
            SavelimpiarcontrolesLP();
            saveActivarbotonesLP();
            desactivarControleLP();
            cmbplanning.Text = "0";
            txt_fechaActual.Text = "";
            cmbRecoger.Text = "0";
            GvLPublicaciones.DataBind();
            BtnCargaMasivaLP.Visible = true;

        }
        protected void GvLPublicaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["TipoProd"] = "Competidor";
            LblMensajeConfirProd.Text = "Realmente desea eliminar este Registro  ?";
            ModalConfirmaProd.Show();          
            
        }
        protected void BtnSiConfirmaProd_Click(object sender, EventArgs e)
        {
            DataSet ds = null;
            Conexion Ocoon = new Conexion();
            GridViewRow row = GvLPublicaciones.SelectedRow;
            ds = Ocoon.ejecutarDataSet("UP_WEBXPLORA_OPE_ELIMINARLEVAMATERIALPOP", Convert.ToInt32(((Label)GvLPublicaciones.Rows[row.RowIndex].Cells[0].FindControl("lblcodregistro")).Text));
            cmbBCampaña.Text = this.Session["planning"].ToString().Trim();
            ConsultaGrillaMarca();
        }
        protected void BtnNoConfirmaProd_Click(object sender, EventArgs e)
        {
            ModalConfirmaProd.Show();
        }
        protected void BtnCargaMasivaLP_Click(object sender, EventArgs e)
        {
            CargaMActivarbotonesLP();
            activarControlesLP();
            llenaplanningAASS();
            GvLPublicaciones.Visible = false;
            this.Session["TipoCarga"] = "Carga Levantamiento MaterialPOP";
            //this.Session["Fecha"]=txt_fechaActual.Text;
            //this.Session["Planning"]=cmbplanning.Text;
            //this.Session["RecogerPor"] = cmbRecoger.Text;
            //ModalCMasiva.Show();
        }
    }
}