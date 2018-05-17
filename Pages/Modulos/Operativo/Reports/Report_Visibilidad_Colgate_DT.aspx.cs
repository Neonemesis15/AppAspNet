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

namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_Visibilidad_Colgate_DT : System.Web.UI.Page
    {

        #region Declaracion de variables generales
        private int compañia;
        private string pais;
        Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private static DataTable dt_Foto;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.Session["companyid"]) == 1592)
            {
                cmbComentario.Visible = true;
                lblComentario.Visible = true;
            }


            if (!Page.IsPostBack)
            {
                CargarCombo_Channel();
                CargarCanal();
                CargarTipoReporte();
                CargarCombo_TipoReporte();
                RadProgressArea1.ProgressIndicators &= ~ProgressIndicators.SelectedFilesCount;


            }
        }

        protected void CargarCombo_TipoReporte()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            compañia = Convert.ToInt32(this.Session["companyid"]);

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_TIPO_REPORTE_CLIENTE", compañia);
            if (dt.Rows.Count > 0)
            {
                cmbtiporeporte.DataSource = dt;
                cmbtiporeporte.DataValueField = "id_Tipo_Reporte";
                cmbtiporeporte.DataTextField = "TipoReporte_Descripcion";
                cmbtiporeporte.DataBind();
                cmbtiporeporte.Items.Insert(0, new ListItem("---Todos---", "0"));
            }

        }
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

        public void habilitarCombos()
        {
            cmbNodeComercial.Enabled = false;
            cmbPuntoDeVenta.Enabled = false;
            cmbcategoria_producto.Enabled = false;
            cmbmarca.Enabled = false;
            cmbOficina.Enabled = false;
        }


        protected void cmbcanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            //tdcampaña.Visible = true;
            cmbplanning.Visible = true;
            string sidchannel = cmbcanal.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);

            if (sidchannel == "1002" && compañia == 1572)
            {
                cmbNodeComercial.Enabled = false;
                cmbPuntoDeVenta.Enabled = false;
                cmbcategoria_producto.Enabled = false;
                cmbmarca.Enabled = false;
                cmbOficina.Enabled = false;

                lbl_supervisor.Visible = true;
                lbl_distribuidor.Visible = true;
                lbl_distrito.Visible = true;

                cmbSupervisor.Visible = true;
                cmbDistribuidor.Visible = true;
                cmbDistrito.Visible = true;
            }
            else
            {
                cmbNodeComercial.Enabled = true;
                cmbPuntoDeVenta.Enabled = true;
                cmbcategoria_producto.Enabled = true;
                cmbmarca.Enabled = true;
                cmbOficina.Enabled = true;

                lbl_supervisor.Visible = false;
                lbl_distribuidor.Visible = false;
                lbl_distrito.Visible = false;

                cmbSupervisor.Visible = false;
                cmbDistribuidor.Visible = false;
                cmbDistrito.Visible = false;
            }


            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);


            cmbplanning.Items.Clear();
            cmbcategoria_producto.Items.Clear();
            cmbcategoria_producto.Enabled = false;
            cmbmarca.Items.Clear();
            cmbmarca.Enabled = false;
            cmbperson.Items.Clear();
            cmbperson.Enabled = false;

            cmbOficina.Items.Clear();
            cmbOficina.Enabled = false;
            cmbNodeComercial.Items.Clear();
            cmbNodeComercial.Enabled = false;
            cmbPuntoDeVenta.Items.Clear();
            cmbPuntoDeVenta.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                cmbplanning.DataSource = dt;
                cmbplanning.DataValueField = "id_planning";
                cmbplanning.DataTextField = "Planning_Name";
                cmbplanning.DataBind();
                cmbplanning.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                cmbplanning.Enabled = true;
                lblmensaje.Text = "";
            }
        }

        protected void cmbplanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            string sidplanning = cmbplanning.SelectedValue;

            if (cmbplanning.SelectedIndex != 0)
            {
                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);

                cmbcategoria_producto.Items.Clear();
                cmbmarca.Items.Clear();
                cmbmarca.Enabled = false;
                cmbperson.Items.Clear();
                cmbperson.Enabled = false;

                if (dt.Rows.Count > 0)
                {
                    cmbperson.DataSource = dt;
                    cmbperson.DataValueField = "Person_id";
                    cmbperson.DataTextField = "Person_NameComplet";
                    cmbperson.DataBind();
                    cmbperson.Items.Insert(0, new ListItem("---Todos---", "0"));
                    cmbperson.Enabled = true;
                }
                else
                {
                    lblmensaje.Text = "No se encontraron resultados.";
                    gv_Foto.DataBind();
                }
                //------llamado al metodo cargar categoria de producto
                //string iischannel = cmbcanal.SelectedValue;

                //Add pSalas 27/01/2012
                string sidchannel = cmbcanal.SelectedValue;
                compañia = Convert.ToInt32(this.Session["companyid"]);
                if (sidchannel == "1002" && compañia == 1572)
                {
                    cargarComboSupervidoresByPlanning();
                    cargarComboDistribuidorByPlanning();
                    cargarComboDistritoByPlanning();
                    cargarComboPointOfSaleByPlanning();
                }
                else
                {
                    cargarCombo_Oficina();
                    cargarCombo_NodeComercial(sidplanning);
                    cargarCombo_CategoriaDeproducto(sidplanning);
                }

                //----------------------------------------------------
            }
            else
            {
                cmbcategoria_producto.Items.Clear();
                cmbcategoria_producto.Enabled = false;
                cmbmarca.Items.Clear();
                cmbmarca.Enabled = false;


                cmbperson.Items.Clear();
                cmbperson.Enabled = false;

                cmbOficina.Items.Clear();
                cmbOficina.Enabled = false;
                cmbNodeComercial.Items.Clear();
                cmbNodeComercial.Enabled = false;
                cmbPuntoDeVenta.Items.Clear();
                cmbPuntoDeVenta.Enabled = false;
            }
        }
        protected void cargarCombo_Oficina()
        {
            try
            {
                Conexion Ocoon = new Conexion();

                if (this.Session["companyid"] != null)
                {
                    compañia = Convert.ToInt32(this.Session["companyid"]);
                    DataTable dtofi = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENEROFICINAS", compañia);

                    if (dtofi.Rows.Count > 0)
                    {
                        cmbOficina.Enabled = true;
                        cmbOficina.DataSource = dtofi;
                        cmbOficina.DataTextField = "Name_Oficina";
                        cmbOficina.DataValueField = "cod_Oficina";
                        cmbOficina.DataBind();

                        cmbOficina.Items.Insert(0, new ListItem("---Todas---", "0"));
                    }
                }
                else
                {
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void cmbOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Conexion Ocoon = new Conexion();

                cmbPuntoDeVenta.Items.Clear();
                cmbPuntoDeVenta.Enabled = false;
                if (cmbplanning.SelectedIndex > 0 && cmbOficina.SelectedIndex > 0)
                {
                    DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", cmbplanning.SelectedValue, Convert.ToInt32(cmbOficina.SelectedValue));

                    if (dtPdv.Rows.Count > 0)
                    {
                        cmbPuntoDeVenta.DataSource = dtPdv;
                        cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
                        cmbPuntoDeVenta.DataTextField = "pdv_Name";
                        cmbPuntoDeVenta.DataBind();

                        cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));

                        cmbPuntoDeVenta.Enabled = true;
                    }

                    compañia = Convert.ToInt32(this.Session["companyid"]);
                    if (compañia == 1572)
                    {
                        DataTable dtNodeCom = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_MALLA_BY_idPlanning_and_codOficina", cmbplanning.SelectedValue, Convert.ToInt32(cmbOficina.SelectedValue));

                        if (dtNodeCom.Rows.Count > 0)
                        {
                            cmbNodeComercial.Enabled = true;
                            cmbNodeComercial.Items.Clear();
                            cmbNodeComercial.DataSource = dtNodeCom;
                            cmbNodeComercial.DataTextField = "malla";
                            cmbNodeComercial.DataValueField = "id_malla";
                            cmbNodeComercial.DataBind();

                            cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                        }
                        else
                        {
                            cmbNodeComercial.Enabled = false;
                            cmbNodeComercial.Items.Clear();
                        }
                    }
                    else
                    {
                        DataTable dtNodeCom = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_NODECOMERCIAL_BY_idPlanning_and_codOficina", cmbplanning.SelectedValue, Convert.ToInt32(cmbOficina.SelectedValue));

                        if (dtNodeCom.Rows.Count > 0)
                        {
                            cmbNodeComercial.Enabled = true;
                            cmbNodeComercial.Items.Clear();
                            cmbNodeComercial.DataSource = dtNodeCom;
                            cmbNodeComercial.DataTextField = "commercialNodeName";
                            cmbNodeComercial.DataValueField = "id_NodeCommercial";
                            cmbNodeComercial.DataBind();

                            cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                        }
                        else
                        {
                            cmbNodeComercial.Enabled = false;
                            cmbNodeComercial.Items.Clear();
                        }
                    }

                }
                if (cmbOficina.SelectedIndex == 0 && cmbplanning.SelectedIndex > 0)
                {
                    cargarCombo_NodeComercial(cmbplanning.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cargarCombo_NodeComercial(string sid_planning)
        {

            try
            {
                Conexion Ocoon = new Conexion();
                compañia = Convert.ToInt32(this.Session["companyid"]);
                if (compañia == 1572)
                {
                    cmbNodeComercial.Items.Clear();
                    DataTable dtNodeCom = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_MALLAS_POR_CLIENTE", compañia);

                    if (dtNodeCom.Rows.Count > 0)
                    {
                        cmbNodeComercial.Enabled = true;
                        cmbNodeComercial.DataSource = dtNodeCom;
                        cmbNodeComercial.DataTextField = "malla";
                        cmbNodeComercial.DataValueField = "id_malla";
                        cmbNodeComercial.DataBind();

                        cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                    }
                }
                else
                {
                    cmbNodeComercial.Items.Clear();
                    Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(cmbplanning.SelectedValue);

                    if (oListNodeComercial.Length > 0)
                    {
                        cmbNodeComercial.Enabled = true;
                        cmbNodeComercial.DataSource = oListNodeComercial;
                        cmbNodeComercial.DataTextField = "commercialNodeName";
                        cmbNodeComercial.DataValueField = "NodeCommercial";
                        cmbNodeComercial.DataBind();

                        cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                    }
                }


            }
            catch (Exception e)
            {
                throw e;
            }




            //try
            //{
            //    cmbNodeComercial.Items.Clear();
            //    Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(cmbplanning.SelectedValue);

            //    if (oListNodeComercial.Length > 0)
            //    {
            //        cmbNodeComercial.Enabled = true;
            //        cmbNodeComercial.DataSource = oListNodeComercial;
            //        cmbNodeComercial.DataTextField = "commercialNodeName";
            //        cmbNodeComercial.DataValueField = "NodeCommercial";
            //        cmbNodeComercial.DataBind();

            //        cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }

        protected void cmbNodeComercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Conexion Ocoon = new Conexion();

                compañia = Convert.ToInt32(this.Session["companyid"]);
                if (compañia == 1572)
                {
                    if (cmbplanning.SelectedIndex > 0 && cmbNodeComercial.SelectedIndex > 0)
                    {
                        DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_and_idmalla", cmbplanning.SelectedValue, Convert.ToInt32(cmbNodeComercial.SelectedValue));

                        if (dtPdv.Rows.Count > 0)
                        {
                            cmbPuntoDeVenta.DataSource = dtPdv;
                            cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
                            cmbPuntoDeVenta.DataTextField = "pdv_Name";
                            cmbPuntoDeVenta.DataBind();

                            cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));

                            cmbPuntoDeVenta.Enabled = true;
                        }
                    }
                }
                else
                {
                    if (cmbplanning.SelectedIndex > 0 && cmbNodeComercial.SelectedIndex > 0)
                    {
                        DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_and_idNodeComercial", cmbplanning.SelectedValue, Convert.ToInt32(cmbNodeComercial.SelectedValue));

                        if (dtPdv.Rows.Count > 0)
                        {
                            cmbPuntoDeVenta.DataSource = dtPdv;
                            cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
                            cmbPuntoDeVenta.DataTextField = "pdv_Name";
                            cmbPuntoDeVenta.DataBind();

                            cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));

                            cmbPuntoDeVenta.Enabled = true;
                        }
                    }
                }


                if (cmbNodeComercial.SelectedIndex == 0)
                {
                    DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", cmbplanning.SelectedValue, Convert.ToInt32(cmbOficina.SelectedValue));

                    if (dtPdv.Rows.Count > 0)
                    {
                        cmbPuntoDeVenta.DataSource = dtPdv;
                        cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
                        cmbPuntoDeVenta.DataTextField = "pdv_Name";
                        cmbPuntoDeVenta.DataBind();

                        cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));

                        cmbPuntoDeVenta.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cargarCombo_CategoriaDeproducto(string sidplanning)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_FOTOGRFICO", sidplanning);
            cmbcategoria_producto.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                cmbcategoria_producto.DataSource = dt;
                cmbcategoria_producto.DataValueField = "id_ProductCategory";
                cmbcategoria_producto.DataTextField = "Product_Category";
                cmbcategoria_producto.DataBind();
                cmbcategoria_producto.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbcategoria_producto.Enabled = true;
            }
        }
        protected void cmbcategoria_producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();


            string sidcategoriaproducto = cmbcategoria_producto.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_REPORT_FOTOGRAFICO", sidcategoriaproducto);

            cmbmarca.Items.Clear();
            cmbmarca.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                cmbmarca.DataSource = dt;
                cmbmarca.DataValueField = "id_Brand";
                cmbmarca.DataTextField = "Name_Brand";
                cmbmarca.DataBind();
                cmbmarca.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbmarca.Enabled = true;
            }
            // cargarGrilla_ReporteFotografico();
        }

        protected void cmbtiporeporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbtiporeporte.SelectedValue == "01")
            {
                cmbmarca.Visible = false;
                lbl_marca.Visible = false;
            }
            else if (cmbtiporeporte.SelectedValue == "02")
            {
                cmbmarca.Visible = true;
                lbl_marca.Visible = true;
            }
        }
        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            cargarGrilla_ReporteFotografico();
        }
        public DataTable Get_ReporteFotografico(int iidperson, string sidplanning, string sidchanel, int cod_oficina, int id_NodeComercial, string ClientPDV_Code, string sidtiporeporte, string sid_categoriaproducto, string sidbrand, string stipreport, DateTime dfecha_inicio, DateTime dfecha_fin, int icompanyid)
        {
            Conexion oCoon = new Conexion();
            DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REPORTE_FOTOGRAFICO", iidperson, sidplanning, sidchanel, cod_oficina, id_NodeComercial, ClientPDV_Code, sidtiporeporte, sid_categoriaproducto, sidbrand, stipreport, dfecha_inicio, dfecha_fin, icompanyid);
            return dt;
        }
        protected void cargarGrilla_ReporteFotografico()
        {
            try
            {
                //------------------------volver a deshabilitgar las columnas--------------------------
                //gv_Foto.Columns[6].Visible = false;
                gv_Foto.Columns[2].Visible = false; //Distribuidora
                gv_Foto.Columns[3].Visible = false; //Tipo
                gv_Foto.Columns[4].Visible = false; //Distrito
                gv_Foto.Columns[5].Visible = false; //Direccion
                gv_Foto.Columns[6].Visible = false; //Supervisor
                gv_Foto.Columns[11].Visible = false;//Categoria

                //-----------------//-----------------//-----------------//-----------------//-----------------

                string sidtipoReport = cmbtiporeporte.SelectedValue;
                string iidpersonstr = cmbperson.SelectedValue;
                if (iidpersonstr == "")
                    iidpersonstr = "0";
                int iidperson = Convert.ToInt32(iidpersonstr);
                string sidoficina = cmbOficina.SelectedValue;
                if (sidoficina == "")
                    sidoficina = "0";
                int icod_oficina = Convert.ToInt32(sidoficina);
                string sidplanning = cmbplanning.SelectedValue;
                string sidchannel = cmbcanal.SelectedValue;
                string iidnodecomercialstr = cmbNodeComercial.SelectedValue;
                if (iidnodecomercialstr == "")
                    iidnodecomercialstr = "0";
                int iidNodeComercial = Convert.ToInt32(iidnodecomercialstr);
                string sidPDV = cmbPuntoDeVenta.SelectedValue;
                if (sidPDV == "")
                    sidPDV = "0";
                string sidcategoriaProducto = cmbcategoria_producto.SelectedValue;
                if (sidcategoriaProducto == "")
                    sidcategoriaProducto = "0";
                string sidmarca = cmbmarca.SelectedValue;
                if (sidmarca == "")
                    sidmarca = "0";
                //Add 28/01/2012
                string siddistribuidor = cmbDistribuidor.SelectedValue;
                if (siddistribuidor == "")
                    siddistribuidor = "0";
                string siddistrito = cmbDistrito.SelectedValue;
                if (siddistrito == "")
                    siddistrito = "0";
                string sidsupervisor = cmbSupervisor.SelectedValue;
                if (sidsupervisor == "")
                    sidsupervisor = "0";

                DateTime dfecha_inicio;
                DateTime dfecha_fin;

                if (txt_fecha_inicio.SelectedDate.ToString() == "" || txt_fecha_inicio.SelectedDate.ToString() == "0" || txt_fecha_inicio.SelectedDate == null)
                    dfecha_inicio = txt_fecha_inicio.MinDate;
                else dfecha_inicio = txt_fecha_inicio.SelectedDate.Value;


                if (txt_fecha_fin.SelectedDate.ToString() == "" || txt_fecha_fin.SelectedDate.ToString() == "0" || txt_fecha_fin.SelectedDate == null)
                    dfecha_fin = txt_fecha_fin.MaxDate;
                else dfecha_fin = txt_fecha_fin.SelectedDate.Value;

                if (DateTime.Compare(dfecha_inicio, dfecha_fin) == 1)
                {
                    lblmensaje.Visible = true;
                    lblmensaje.Text = "La fecha de inicio debe ser menor o igual a la fecha fin";
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                }
                else
                {

                    compañia = Convert.ToInt32(this.Session["companyid"]);
                    //dt_Foto = Get_ReporteFotografico(iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, "R", sidcategoriaProducto, sidmarca, sidtipoReport, dfecha_inicio, dfecha_fin, compañia);
                    Conexion oCoon = new Conexion();
                    if (compañia == 1572 && sidchannel == "1025")//para sanfernando AAVV
                    {
                        //es momentaneo cambiar el store para que sea uno solo
                        dt_Foto = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REPORTE_FOTOGRAFICO_BORRAR", iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, "R", sidcategoriaProducto, sidmarca, sidtipoReport, dfecha_inicio, dfecha_fin, compañia);
                        gv_Foto.Columns[2].Visible = false; //Distribuidora
                        gv_Foto.Columns[3].Visible = false; //Tipo
                        gv_Foto.Columns[4].Visible = false; //Distrito
                        gv_Foto.Columns[5].Visible = false; //Direccion
                        gv_Foto.Columns[6].Visible = false; //Supervisor

                        gv_Foto.Columns[7].Visible = true;//Zona
                        gv_Foto.Columns[8].Visible = true;//Nivel
                        gv_Foto.Columns[9].Visible = true;//CodigoPtoVenta
                        //gv_Foto.Columns[10].Visible = false;//Punto de venta
                        gv_Foto.Columns[11].Visible = true;//Categoria
                        gv_Foto.Columns[12].Visible = true;//Marca
                        gv_Foto.Columns[13].Visible = true;//Tipo de Reporte
                        gv_Foto.Columns[14].Visible = true;//Comentario
                    }
                    //psalas 27/01/2012
                    else if (compañia == 1572 && sidchannel == "1002")// para sanFernando Tradicional
                    {
                        dt_Foto = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REPORTE_FOTOGRAFICO_SF_TRADICIONAL", iidperson, sidplanning, sidchannel, sidPDV, siddistribuidor, siddistrito, sidsupervisor, dfecha_inicio, dfecha_fin, compañia);

                        gv_Foto.Columns[2].Visible = true; //Distribuidora
                        gv_Foto.Columns[3].Visible = true; //Tipo
                        gv_Foto.Columns[4].Visible = true; //Distrito
                        gv_Foto.Columns[5].Visible = true; //Direccion
                        gv_Foto.Columns[6].Visible = true; //Supervisor

                        gv_Foto.Columns[7].Visible = false;//Zona
                        gv_Foto.Columns[8].Visible = false;//Nivel
                        gv_Foto.Columns[9].Visible = false;//CodigoPtoVenta
                        //gv_Foto.Columns[10].Visible = false;//Punto de venta
                        gv_Foto.Columns[11].Visible = false;//Categoria
                        gv_Foto.Columns[12].Visible = false;//Marca
                        gv_Foto.Columns[13].Visible = false;//Tipo de Reporte
                        gv_Foto.Columns[14].Visible = false;//Comentario


                    }
                    else if (compañia == 1592) // para 3M
                    {

                        dt_Foto = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REPORTE_FOTOGRAFICO_3M", iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, "R", sidcategoriaProducto, sidmarca, sidtipoReport, dfecha_inicio, dfecha_fin, compañia, cmbComentario.SelectedValue);
                        gv_Foto.Columns[2].Visible = false; //Distribuidora
                        gv_Foto.Columns[3].Visible = false; //Tipo
                        gv_Foto.Columns[4].Visible = false; //Distrito
                        gv_Foto.Columns[5].Visible = false; //Direccion
                        gv_Foto.Columns[6].Visible = false; //Supervisor

                        gv_Foto.Columns[7].Visible = true;//Zona
                        gv_Foto.Columns[8].Visible = true;//Nivel
                        gv_Foto.Columns[9].Visible = true;//CodigoPtoVenta
                        //gv_Foto.Columns[10].Visible = false;//Punto de venta
                        gv_Foto.Columns[11].Visible = true;//Categoria
                        gv_Foto.Columns[12].Visible = true;//Marca
                        gv_Foto.Columns[13].Visible = true;//Tipo de Reporte
                        gv_Foto.Columns[14].Visible = true;//Comentario

                    }
                    else
                    {
                        dt_Foto = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REPORTE_FOTOGRAFICO", iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, "R", sidcategoriaProducto, sidmarca, sidtipoReport, dfecha_inicio, dfecha_fin, compañia);
                        gv_Foto.Columns[2].Visible = false; //Distribuidora
                        gv_Foto.Columns[3].Visible = false; //Tipo
                        gv_Foto.Columns[4].Visible = false; //Distrito
                        gv_Foto.Columns[5].Visible = false; //Direccion
                        gv_Foto.Columns[6].Visible = false; //Supervisor

                        gv_Foto.Columns[7].Visible = true;//Zona
                        gv_Foto.Columns[8].Visible = true;//Nivel
                        gv_Foto.Columns[9].Visible = true;//CodigoPtoVenta
                        //gv_Foto.Columns[10].Visible = false;//Punto de venta
                        gv_Foto.Columns[11].Visible = true;//Categoria
                        gv_Foto.Columns[12].Visible = true;//Marca
                        gv_Foto.Columns[13].Visible = true;//Tipo de Reporte
                        gv_Foto.Columns[14].Visible = true;//Comentario
                    }



                    if (String.Equals(sidtipoReport, "01"))//Exibicion 
                    {
                        gv_Foto.Columns[11].Visible = false;//Categoria

                    }
                    else  // 02 Visibilidad
                    {
                        gv_Foto.Columns[11].Visible = true;//Categoria

                    }

                    gv_Foto.DataSource = dt_Foto;
                    gv_Foto.DataBind();

                    lblmensaje.Text = "Se encontro " + dt_Foto.Rows.Count + " resultados";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.Text = "";
                if (cmbplanning.SelectedIndex > 0)
                    lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
            }

        }

        //Codigo para Ordenar y Paginar la grilla "grid_detalle_foto"------------------
        public string SortExpression
        {
            get { return (ViewState["SortExpression"] == null ? string.Empty : ViewState["SortExpression"].ToString()); }
            set { ViewState["SortExpression"] = value; }
        }

        public string SortDirection
        {
            get { return (ViewState["SortDirection"] == null ? string.Empty : ViewState["SortDirection"].ToString()); }
            set { ViewState["SortDirection"] = value; }
        }

        private string GetSortDirection(string sortExpression)
        {
            if (SortExpression == sortExpression)
            {
                if (SortDirection == "ASC")
                    SortDirection = "DESC";
                else if (SortDirection == "DESC")
                    SortDirection = "ASC";
                return SortDirection;
            }
            else
            {
                SortExpression = sortExpression;
                SortDirection = "ASC";
                return SortDirection;
            }
        }
        protected void gv_Foto_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = gv_Foto.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                gv_Foto.DataSource = dataView;
                gv_Foto.DataBind();
            }
        }
        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            // GridHeaderItem header = "";

            //CheckBox cb_all = (CheckBox)gv_Foto.FindControl("cb_all");
            bool validar = cb_all.Checked;

            for (int i = 0; i < gv_Foto.Items.Count; i++)
            {
                GridItem item = gv_Foto.Items[i];
                //if (item.ItemType == GridItemType.Item)
                //{

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");

                if (cb_validar.Enabled == true)
                {
                    if (validar == true)
                    {
                        cb_validar.Checked = true;
                    }
                    else if (validar == false)
                    {
                        cb_validar.Checked = false;
                    }
                }

            }
        }
        protected void btn_validar_Click(object sender, EventArgs e)
        {

            string dataValidas = String.Empty;
            string dataInvalidas = String.Empty;
            string fotosValidas = String.Empty;

            for (int i = 0; i < gv_Foto.Items.Count; i++)
            {
                GridItem item = gv_Foto.Items[i];


                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");
                Label lbl_validar = (Label)item.FindControl("lbl_validar");
                Label lbl_Id_Reg_Fotogr = (Label)item.FindControl("lbl_Id_Reg_Fotogr");

                int id = Convert.ToInt32(lbl_Id_Reg_Fotogr.Text.Trim());
                bool validar = cb_validar.Checked;

                //update_fotografico_detalle_validado(id, validar);
                if (validar == true)
                {


                    lbl_validar.Text = "validado";
                    lbl_validar.ForeColor = System.Drawing.Color.Green;

                    dataValidas = dataValidas + id + ",";

                    Label oLabel_id_reg_foto = (Label)item.FindControl("lbl_id_reg_foto");

                    int iidregft = Convert.ToInt32(oLabel_id_reg_foto.Text);
                    // update_urlFoto(iidregft);
                    fotosValidas = fotosValidas + iidregft + ",";
                }
                else
                {
                    dataInvalidas = dataInvalidas + id + ",";

                    lbl_validar.Text = "sin validar";
                    lbl_validar.ForeColor = System.Drawing.Color.Red;
                }

            }


            fotosValidas = fotosValidas.Substring(0, fotosValidas.Length - 1);
            dataValidas = dataValidas.Substring(0, dataValidas.Length - 1);
            dataInvalidas = dataInvalidas.Substring(0, dataInvalidas.Length - 1);

            update_urlFoto(fotosValidas);
            update_fotografico_detalle_validado(dataValidas, dataInvalidas);
            //cargarGrilla_Competencias();
        }
        protected void update_fotografico_detalle_validado(string dataValida, string dataInvalida)
        {
            try
            {
                Conexion Ocoon = new Conexion();
                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_FOTOGRAFICO_VALIDADO_2", dataValida, dataInvalida, this.Session["sUser"], DateTime.Now);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void update_urlFoto(string fotosValidas)
        {
            Conexion Ocoon = new Conexion();
            try
            {

                DataRow[] drs = dt_Foto.Select("Id_regft in(" + fotosValidas + ")");

                System.Drawing.Image newImage;
                foreach (DataRow item in drs)
                {

                    string PhotoNameAsUrlInDb = item["Id_regft"].ToString() + ".jpg";
                    byte[] byteArrayIn = (byte[])item["foto"];

                    string strFileName = Server.MapPath(@"~\Pages\Modulos\Cliente\Reportes\Galeria_fotografica\Fotos") + "\\" + PhotoNameAsUrlInDb;
                    if (byteArrayIn != null && !File.Exists(strFileName))
                    {
                        using (MemoryStream stream = new MemoryStream(byteArrayIn))
                        {
                            newImage = System.Drawing.Image.FromStream(stream);
                            newImage.Save(strFileName);

                        }
                    }

                }

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REG_FOTO_FOTOGRAFICO_URLFOTO_2", fotosValidas, this.Session["sUser"], DateTime.Now);

            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }
        }
        protected void gv_Foto_CancelCommand(object source, GridCommandEventArgs e)
        {

            try
            {
                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_EditCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                gv_Foto.CurrentPageIndex = e.NewPageIndex;
                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            try
            {
                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                lblmensaje.Text = "";
                Conexion Ocoon = new Conexion();

                GridItem item = gv_Foto.Items[e.Item.ItemIndex];

                Label lbl_Id_Reg_Fotogr = (Label)item.FindControl("lbl_Id_Reg_Fotogr");
                int iid_ReportFotog = Convert.ToInt32(lbl_Id_Reg_Fotogr.Text.Trim());

                CheckBox cb_validar = (CheckBox)item.FindControl("cb_validar");



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
                            if (editor is GridNumericColumnEditor)
                            {
                                editorText = (editor as GridNumericColumnEditor).Text;
                                editorValue = (editor as GridNumericColumnEditor).NumericTextBox.DbValue;
                                ArrayEditorValue.Add(editorValue);
                            }

                            if (editor is GridDateTimeColumnEditor)
                            {
                                editorText = (editor as GridDateTimeColumnEditor).Text;
                                editorValue = (editor as GridDateTimeColumnEditor).PickerControl;
                                ArrayEditorValue.Add(editorValue);
                            }
                        }
                    }
                }

                string comentario = ArrayEditorValue[0].ToString();
                DateTime Fec_reg_bd = Convert.ToDateTime((ArrayEditorValue[2] as RadDateTimePicker).SelectedDate);

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_FOTOGRAFICO", cb_validar.Checked, comentario, iid_ReportFotog, Fec_reg_bd, Session["sUser"].ToString(), DateTime.Now);

                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                lblmensaje.Text = ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_DataBound(object sender, EventArgs e)
        {

            if (gv_Foto.Items.Count > 0)
            {
                cb_all.Visible = true;
                lbl_cb_all.Visible = true;

            }
            else
            {
                cb_all.Visible = false;
                lbl_cb_all.Visible = false;
            }
        }
        protected void buttonSubmit_Click(object sender, System.EventArgs e)
        {


            UploadedFile file = UploadedFile.FromHttpPostedFile(Request.Files[inputFile.UniqueID]);

            if (!Object.Equals(file, null))
            {
                LooongMethodWhichUpdatesTheProgressContext(file);
                RadBinaryImage_fotoBig.Visible = true;

                Int32 tamaño = Convert.ToInt32(file.InputStream.Length);
                byte[] byteArrayIn = new byte[tamaño + 1];

                file.InputStream.Read(byteArrayIn, 0, tamaño);
                RadBinaryImage_fotoBig.DataValue = byteArrayIn;
                Session["byteArrayIn"] = byteArrayIn;
            }

            ModalPopup_Edit.Show();
        }

        private void LooongMethodWhichUpdatesTheProgressContext(UploadedFile file)
        {
            const int total = 100;

            RadProgressContext progress = RadProgressContext.Current;

            for (int i = 0; i < total; i++)
            {
                progress.PrimaryTotal = 1;
                progress.PrimaryValue = 1;
                progress.PrimaryPercent = 100;

                progress.SecondaryTotal = total;
                progress.SecondaryValue = i;
                progress.SecondaryPercent = i;

                progress.CurrentOperationText = file.GetName() + " Empezando a procesar...";

                if (!Response.IsClientConnected)
                {
                    //Cancel button was clicked or the browser was closed, so stop processing
                    break;
                }

                //Stall the current thread for 0.1 seconds
                System.Threading.Thread.Sleep(100);
            }
        }

        protected void imgbtn_save_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                Int32 iidregft = Convert.ToInt32(Session["iidregft"]);
                Int32 Id_repft = Convert.ToInt32(Session["Id_repft"]);

                Conexion Ocoon = new Conexion();

                byte[] foto = (byte[])Session["byteArrayIn"];

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REG_FOTO", iidregft, Id_repft, foto, Session["sUser"].ToString(), DateTime.Now);
                RadBinaryImage_fotoBig.Visible = false;
                cargarGrilla_ReporteFotografico();

            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void imgbtn_cancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RadBinaryImage_fotoBig.DataBind();
                RadBinaryImage_fotoBig.Visible = false;
                ModalPopup_Edit.Hide();
            }
            catch (Exception ex)
            {
                //comentario
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        /// <summary>
        /// Verificar este metodo Ing. Carlo  Hernandez ---Preguntar a Ditmar Estrada
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void gv_Foto_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                panelEdit.DataBind();
                RadBinaryImage_fotoBig.DataBind();
                RadBinaryImage_fotoBig.Visible = false;
                //GridItem item = gv_Foto.Items[e.Item.ItemIndex];
                GridItem item = gv_Foto.Items[e.Item.ItemIndex];






                Label lbl_id_reg_foto = (Label)item.FindControl("lbl_id_reg_foto");

                Label lbl_Id_Reg_Fotogr = (Label)item.FindControl("lbl_Id_Reg_Fotogr");
                if (e.CommandName == "VERFOTO")
                {
                    Session["iidregft"] = Convert.ToInt32(lbl_id_reg_foto.Text);
                    Session["Id_repft"] = Convert.ToInt32(lbl_Id_Reg_Fotogr.Text);

                    DataTable dt = null;
                    Conexion Ocoon = new Conexion();
                    //string sidregft = ((LinkButton)sender).CommandArgument;
                    int iidregft = Convert.ToInt32(lbl_id_reg_foto.Text);

                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REG_FOTO", iidregft);
                    byte[] byteArrayIn = (byte[])dt.Rows[0]["foto"];

                    Session["array"] = byteArrayIn;
                    RadBinaryImage_viewFoto.DataValue = byteArrayIn;
                    ModalPopupExtender_viewfoto.Show();

                    //Response.Redirect("verFoto.aspx?iidregft=" + iidregft, true);
                }
                else if (e.CommandName == "EDITFOTO")
                {

                    //string sidregft = ((LinkButton)senser).CommandArgument;
                    int iidregft = Convert.ToInt32(lbl_id_reg_foto.Text);
                    int Id_repft = Convert.ToInt32(lbl_Id_Reg_Fotogr.Text);
                    if (Int32.Equals(iidregft, 0))
                    {
                        Response.Redirect("/login.aspx");
                    }
                    else
                    {
                        RadBinaryImage imageBinary = (RadBinaryImage)item.FindControl("RadBinaryImage_foto");

                        RadBinaryImage_fotoBig.DataValue = imageBinary.DataValue;
                        RadBinaryImage_fotoBig.Visible = false;
                        Session["iidregft"] = iidregft;
                        Session["Id_repft"] = Id_repft;
                    }
                    ModalPopup_Edit.Show();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void BtnclosePanel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                RadBinaryImage_fotoBig.DataBind();
                RadBinaryImage_fotoBig.Visible = false;
                ModalPopup_Edit.Hide();
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

        }



        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string a = Server.MapPath("temp.jpg");
            Session["path"] = a;
            MyImage FImage;
            FImage = new MyImage(RadBinaryImage_viewFoto.ImageUrl, (byte[])Session["array"]);
            FImage.Rotate("90", Server.MapPath("temp.jpg"));
            //Image1.ImageUrl = "temp.jpg";
            RadBinaryImage_viewFoto.DataValue = FImage.imageToByteArray((System.Drawing.Image)Session["imagenabyte"]);


            FImage = null;

            ModalPopupExtender_viewfoto.Show();
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            string a = Server.MapPath("temp.jpg");
            Session["path"] = a;
            MyImage FImage;
            FImage = new MyImage(RadBinaryImage_viewFoto.ImageUrl, (byte[])Session["array"]);
            FImage.Rotate("270", Server.MapPath("temp.jpg"));
            //Image1.ImageUrl = "temp.jpg";
            RadBinaryImage_viewFoto.DataValue = FImage.imageToByteArray((System.Drawing.Image)Session["imagenabyte"]);


            FImage = null;

            ModalPopupExtender_viewfoto.Show();
        }

        protected void ibtnGuardarImagen_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Int32 iidregft = Convert.ToInt32(Session["iidregft"]);
                Int32 Id_repft = Convert.ToInt32(Session["Id_repft"]);

                Conexion Ocoon = new Conexion();

                byte[] foto = (byte[])Session["array"];

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REG_FOTO", iidregft, Id_repft, foto, Session["sUser"].ToString(), DateTime.Now);
                RadBinaryImage_fotoBig.Visible = false;
                cargarGrilla_ReporteFotografico();

            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }



        protected void CargarTipoReporte()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            compañia = Convert.ToInt32(this.Session["companyid"]);

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_TIPO_REPORTE_CLIENTE", compañia);
            if (dt.Rows.Count > 0)
            {
                ddlTipoReporte.DataSource = dt;
                ddlTipoReporte.DataValueField = "id_Tipo_Reporte";
                ddlTipoReporte.DataTextField = "TipoReporte_Descripcion";
                ddlTipoReporte.DataBind();
                ddlTipoReporte.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }

        }
        protected void CargarCanal()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            pais = this.Session["scountry"].ToString();


            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CHANNEL", pais, compañia);
            if (dt.Rows.Count > 0)
            {
                ddlCanal.DataSource = dt;
                ddlCanal.DataValueField = "cod_Channel";
                ddlCanal.DataTextField = "Channel_Name";
                ddlCanal.DataBind();
                ddlCanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }

        }

        protected void ddlCanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            string sidchannel = ddlCanal.SelectedValue;

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", sidchannel, compañia);

            ddlCampana.Items.Clear();
            ddlCategoria.Items.Clear();
            ddlCategoria.Enabled = false;
            ddlMarca.Items.Clear();
            ddlMarca.Enabled = false;
            ddlMercaderista.Items.Clear();
            ddlMercaderista.Enabled = false;

            ddlOficina.Items.Clear();
            ddlOficina.Enabled = false;
            ddlNodeComercial.Items.Clear();
            ddlNodeComercial.Enabled = false;
            ddlPuntoVenta.Items.Clear();
            ddlPuntoVenta.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                ddlCampana.DataSource = dt;
                ddlCampana.DataValueField = "id_planning";
                ddlCampana.DataTextField = "Planning_Name";
                ddlCampana.DataBind();
                ddlCampana.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlCampana.Enabled = true;
            }

            MopoReporFotografico.Show();
        }

        protected void cargarOficina()
        {
            try
            {
                Conexion Ocoon = new Conexion();

                if (this.Session["companyid"] != null)
                {
                    compañia = Convert.ToInt32(this.Session["companyid"]);
                    DataTable dtofi = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENEROFICINAS", compañia);

                    if (dtofi.Rows.Count > 0)
                    {
                        ddlOficina.Enabled = true;
                        ddlOficina.DataSource = dtofi;
                        ddlOficina.DataTextField = "Name_Oficina";
                        ddlOficina.DataValueField = "cod_Oficina";
                        ddlOficina.DataBind();

                        ddlOficina.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                    }
                }
                else
                {
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void cargarNodeComercial(string sid_planning)
        {
            try
            {
                ddlNodeComercial.Items.Clear();
                Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(sid_planning);

                if (oListNodeComercial.Length > 0)
                {
                    ddlNodeComercial.Enabled = true;
                    ddlNodeComercial.DataSource = oListNodeComercial;
                    ddlNodeComercial.DataTextField = "commercialNodeName";
                    ddlNodeComercial.DataValueField = "NodeCommercial";
                    ddlNodeComercial.DataBind();

                    ddlNodeComercial.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void cargarCategoria(string sidplanning)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_FOTOGRFICO", sidplanning);
            ddlCategoria.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                ddlCategoria.DataSource = dt;
                ddlCategoria.DataValueField = "id_ProductCategory";
                ddlCategoria.DataTextField = "Product_Category";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlCategoria.Enabled = true;
            }
        }

        protected void ddlCampana_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            string sidplanning = ddlCampana.SelectedValue;

            if (ddlCampana.SelectedIndex != 0)
            {
                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);

                ddlCategoria.Items.Clear();
                ddlMarca.Enabled = false;
                ddlMercaderista.Items.Clear();
                ddlMercaderista.Enabled = false;

                if (dt.Rows.Count > 0)
                {
                    ddlMercaderista.DataSource = dt;
                    ddlMercaderista.DataValueField = "Person_id";
                    ddlMercaderista.DataTextField = "Person_NameComplet";
                    ddlMercaderista.DataBind();
                    ddlMercaderista.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                    ddlMercaderista.Enabled = true;
                }

                //------llamado al metodo cargar categoria de producto
                cargarOficina();
                cargarNodeComercial(sidplanning);
                cargarCategoria(sidplanning);
                //----------------------------------------------------
            }
            else
            {
                ddlCategoria.Items.Clear();
                ddlCategoria.Enabled = false;
                ddlMarca.Items.Clear();
                ddlMarca.Enabled = false;



                ddlMercaderista.Items.Clear();
                ddlMercaderista.Enabled = false;

                ddlOficina.Items.Clear();
                ddlOficina.Enabled = false;
                ddlNodeComercial.Items.Clear();
                ddlNodeComercial.Enabled = false;
                ddlPuntoVenta.Items.Clear();
                ddlPuntoVenta.Enabled = false;
            }

            MopoReporFotografico.Show();
        }

        protected void cargarNodeComercial(DropDownList ddlplanning)
        {
            try
            {
                cmbNodeComercial.Items.Clear();
                Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(ddlplanning.SelectedValue);

                if (oListNodeComercial.Length > 0)
                {
                    ddlNodeComercial.Enabled = true;
                    ddlNodeComercial.DataSource = oListNodeComercial;
                    ddlNodeComercial.DataTextField = "commercialNodeName";
                    ddlNodeComercial.DataValueField = "NodeCommercial";
                    ddlNodeComercial.DataBind();

                    ddlNodeComercial.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Conexion Ocoon = new Conexion();

                ddlPuntoVenta.Items.Clear();
                ddlPuntoVenta.Enabled = false;
                if (ddlCampana.SelectedIndex > 0 && ddlOficina.SelectedIndex > 0)
                {
                    DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", ddlCampana.SelectedValue, Convert.ToInt32(ddlOficina.SelectedValue));

                    if (dtPdv.Rows.Count > 0)
                    {
                        ddlPuntoVenta.DataSource = dtPdv;
                        ddlPuntoVenta.DataValueField = "ClientPDV_Code";
                        ddlPuntoVenta.DataTextField = "pdv_Name";
                        ddlPuntoVenta.DataBind();

                        ddlPuntoVenta.Items.Insert(0, new ListItem("---Seleccione---", "0"));

                        ddlPuntoVenta.Enabled = true;
                    }

                    DataTable dtNodeCom = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_NODECOMERCIAL_BY_idPlanning_and_codOficina", ddlCampana.SelectedValue, Convert.ToInt32(ddlOficina.SelectedValue));

                    if (dtNodeCom.Rows.Count > 0)
                    {
                        ddlNodeComercial.Enabled = true;
                        ddlNodeComercial.Items.Clear();
                        ddlNodeComercial.DataSource = dtNodeCom;
                        ddlNodeComercial.DataTextField = "commercialNodeName";
                        ddlNodeComercial.DataValueField = "id_NodeCommercial";
                        ddlNodeComercial.DataBind();

                        ddlNodeComercial.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                    }
                    else
                    {
                        ddlNodeComercial.Enabled = false;
                        ddlNodeComercial.Items.Clear();
                    }
                }
                if (ddlOficina.SelectedIndex == 0 && ddlCampana.SelectedIndex > 0)
                {
                    cargarNodeComercial(ddlCampana.SelectedValue);
                }

                MopoReporFotografico.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlNodeComercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Conexion Ocoon = new Conexion();


                if (ddlCampana.SelectedIndex > 0 && ddlNodeComercial.SelectedIndex > 0)
                {
                    DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_and_idNodeComercial", ddlCampana.SelectedValue, Convert.ToInt32(ddlNodeComercial.SelectedValue));

                    if (dtPdv.Rows.Count > 0)
                    {
                        ddlPuntoVenta.DataSource = dtPdv;
                        ddlPuntoVenta.DataValueField = "ClientPDV_Code";
                        ddlPuntoVenta.DataTextField = "pdv_Name";
                        ddlPuntoVenta.DataBind();

                        ddlPuntoVenta.Items.Insert(0, new ListItem("---Seleccione---", "0"));

                        ddlPuntoVenta.Enabled = true;
                    }
                }
                if (ddlNodeComercial.SelectedIndex == 0)
                {
                    DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina", ddlCampana.SelectedValue, Convert.ToInt32(ddlOficina.SelectedValue));

                    if (dtPdv.Rows.Count > 0)
                    {
                        ddlPuntoVenta.DataSource = dtPdv;
                        ddlPuntoVenta.DataValueField = "ClientPDV_Code";
                        ddlPuntoVenta.DataTextField = "pdv_Name";
                        ddlPuntoVenta.DataBind();

                        ddlPuntoVenta.Items.Insert(0, new ListItem("---Seleccione---", "0"));

                        ddlPuntoVenta.Enabled = true;
                    }
                }

                MopoReporFotografico.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            string sidcategoriaproducto = ddlCategoria.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_REPORT_SOD", sidcategoriaproducto);

            ddlMarca.Items.Clear();
            ddlMarca.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                ddlMarca.DataSource = dt;
                ddlMarca.DataValueField = "id_Brand";
                ddlMarca.DataTextField = "Name_Brand";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlMarca.Enabled = true;
            }
            MopoReporFotografico.Show();
        }

        void LimpiarControles()
        {
            ddlCanal.SelectedValue = "0";
            ddlCampana.Items.Clear();
            ddlCategoria.Items.Clear();
            ddlMarca.Items.Clear();
            ddlMercaderista.Items.Clear();
            ddlNodeComercial.Items.Clear();
            ddlOficina.Items.Clear();
            ddlPuntoVenta.Items.Clear();


        }

        protected void btnGuardarReporteFotografico_Click(object sender, EventArgs e)
        {

            if (fileImagen.HasFile)
            {
                string contentType = fileImagen.PostedFile.ContentType;

                // Get the bytes from the uploaded file
                Int32 tamaño = Convert.ToInt32(fileImagen.PostedFile.InputStream.Length);
                byte[] byteArrayIn = new byte[tamaño + 1];
                fileImagen.PostedFile.InputStream.Read(byteArrayIn, 0, tamaño);
                Session["byteArrayIn"] = byteArrayIn;



                try
                {
                    if (ddlCanal.Text == "0" || ddlCampana.Text == "0" || ddlMercaderista.Text == "0"
        || ddlNodeComercial.Text == "0" || ddlPuntoVenta.Text == "0" || ddlTipoReporte.Text == "0")
                    {
                        lblmensaje.Visible = true;
                        lblmensaje.Text = "No se pudo registrar faltan datos";
                        lblmensaje.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    OPE_Reporte_Fotografico oOPE_Reporte_Fotografico = new OPE_Reporte_Fotografico();

                    DataTable dt = null;
                    Conexion Ocoon = new Conexion();
                    dt = Ocoon.ejecutarDataTable("UP_WEB_SEARCH_USER", "", Convert.ToInt32(ddlMercaderista.SelectedValue));
                    string idperfil = dt.Rows[0]["Perfil_id"].ToString();


                    oOPE_Reporte_Fotografico.RegistrarFoto(Convert.ToInt32(ddlMercaderista.SelectedValue),
                                                                idperfil, ddlCampana.SelectedValue, this.Session["companyid"].ToString(),
                                                                ddlPuntoVenta.SelectedValue, "1", DateTime.Now.ToShortDateString(), txtComentario.Text, (byte[])Session["byteArrayIn"], fileImagen.FileName);


                    oOPE_Reporte_Fotografico.RegistrarReporteFotografico(Convert.ToInt32(ddlMercaderista.SelectedValue),
                                                                idperfil, ddlCampana.SelectedValue, this.Session["companyid"].ToString(),
                                                                ddlPuntoVenta.SelectedValue, ddlTipoReporte.SelectedValue, ddlCategoria.SelectedValue, ddlMarca.SelectedValue, DateTime.Now.ToShortDateString(), "0", "0", "n", null);


                    LimpiarControles();
                    lblmensaje.Visible = true;
                    lblmensaje.Text = "Fue registrado con exito el reporte Fotografico";
                    lblmensaje.ForeColor = System.Drawing.Color.Blue;
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
                lblmensaje.Visible = true;
                lblmensaje.Text = "No se pudo registrar ingrese una foto";
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                return;
            }



        }

        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            MopoReporFotografico.Show();
        }

        //pSalas 27/01/2012 
        public void cargarComboDistribuidorByPlanning()
        {
            //DistribuidoraBy_Planning

            cmbDistribuidor.Items.Clear();
            DataTable dt = new DataTable();
            Conexion Ocoon = new Conexion();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_DISTRIBUIDORA_BY_ID_PLANNING", cmbplanning.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbDistribuidor.DataSource = dt;
                cmbDistribuidor.DataValueField = "Id_Dex";
                cmbDistribuidor.DataTextField = "Dex_Name";
                cmbDistribuidor.DataBind();
                cmbDistribuidor.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbDistribuidor.Enabled = true;
            }
        }
        public void cargarComboDistritoByPlanning()
        {
            //DistritosBy_Planning
            Conexion Ocoon = new Conexion();
            cmbDistrito.Items.Clear();
            DataTable dt = new DataTable();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_DISTRITOS_BY_ID_PLANNING", cmbplanning.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbDistrito.DataSource = dt;
                cmbDistrito.DataValueField = "cod_District";
                cmbDistrito.DataTextField = "Name_Local";
                cmbDistrito.DataBind();
                cmbDistrito.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbDistrito.Enabled = true;
            }

        }
        public void cargarComboSupervidoresByPlanning()
        {

            //SupervisoresByPlanning
            Conexion Ocoon = new Conexion();
            cmbSupervisor.Items.Clear();
            DataTable dt = new DataTable();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_SUPERVISORES_BY_ID_PLANNING", cmbplanning.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbSupervisor.DataSource = dt;
                cmbSupervisor.DataValueField = "Person_idSupe";
                cmbSupervisor.DataTextField = "Supervisor";
                cmbSupervisor.DataBind();
                cmbSupervisor.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbSupervisor.Enabled = true;
            }
        }

        public void cargarComboPointOfSaleByPlanning()
        {

            //SupervisoresByPlanning
            Conexion Ocoon = new Conexion();
            cmbPuntoDeVenta.Items.Clear();
            DataTable dt = new DataTable();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_POINT_OF_SALE_BY_ID_PLANNING", cmbplanning.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                cmbPuntoDeVenta.DataSource = dt;
                cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
                cmbPuntoDeVenta.DataTextField = "pdv_Name";
                cmbPuntoDeVenta.DataBind();
                cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todas---", "0"));
                cmbPuntoDeVenta.Enabled = true;
            }
        }
    }
}