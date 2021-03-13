using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Business.Common.Application;
using Lucky.CFG.Util;
using Lucky.Data;
using Telerik.Web.UI;
using Lucky.Business.Common.Maestros.Cliente;
using Lucky.Entity.Common.Maestros;
using System.Collections.Generic;


namespace SIGE.Pages.Modulos.Operativo.Reports
{
    /// <summary>
    /// Report_Precio.aspx.cs
    /// Developed by:
    /// - Ditmar Estrada Bernuy (DEB)
    /// Changes:
    /// - 2018-12-17 Pablo Salas Alvarez (PSA) Refactoring Button 'Buscar'
    /// </summary>
    public partial class Report_Precio : System.Web.UI.Page
    {
        #region Declaracion de Variables
        private int compañia;
        private string pais;
        private static string static_channel;

        ////////////
        int iidperson;
        string sidplanning;
        string sidchannel;
        int icod_oficina;
        int iidNodeComercial;
        string sidPDV;
        string sidcategoriaProducto;
        string sidmarca;
        string scodproducto;
        string sidproductSubcategory;
        // Variable String para guardar los Errores
        String message = "";
        ////////////
        #endregion

        #region Servicios Web
        Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        #endregion

        #region Invocacion a la Capa Business del Framework
        OPE_REPORTE_PRECIO oBLOpeReportePrecio = new OPE_REPORTE_PRECIO();
        #endregion

        /// <summary>
        /// Obtener el Mensaje de Error
        /// </summary>
        /// <returns></returns>
        public String getMessage() {
            return message;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack){
                
                fncLoginDummy();

                String idCountry =
                    this.Session["scountry"].ToString();
                String idCompany =
                    this.Session["companyid"].ToString();

                orqLoadDropDownListCanal(idCountry, idCompany);

                if (!message.Equals("")) {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "Ocurrió un Error: " + getMessage();
                }

                /*CargarCombo_Channel();

                if (message.Equals("")){
                    Cargarcanales();
                }
                else {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "Ocurrió un Error: " + getMessage();
                }*/
                

                //GetCanalesByUsuario("1");
            }
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "attachevent", "AttachHoverEvent();", true);
        }

        #region Filtros
        /// <summary>
        /// Cargar Combo de Canal para los Filtros del Reporte 
        /// de Precios
        /// </summary>
        protected void CargarCombo_Channel()
        {
            DataTable dt = new DataTable();
            //Conexion Ocoon = new Conexion();

            //compañia = Convert.ToInt32(this.Session["companyid"]);
            //pais = this.Session["scountry"].ToString();

            Canales oDCanales = new Canales();
            try{
                //dt = Ocoon.ejecutarDataTable(
                //    "UP_WEBXPLORA_OPE_COMBO_CHANNEL", 
                //    this.Session["scountry"].ToString(), 
                //    Convert.ToInt32(this.Session["companyid"]));

                dt = oDCanales.getCanalesByIdCountryAndIdCompany(
                    this.Session["scountry"].ToString(),
                    this.Session["companyid"].ToString()
                    );

            }catch (Exception ex){
                message = ex.ToString();
            }

            // Verificar que no existan errores
            if (message.Equals(""))
            {
                // Verificar que Existan Registros
                if (dt.Rows.Count > 0)
                {
                    cmbcanal.DataSource = dt;
                    cmbcanal.DataValueField = "cod_Channel";
                    cmbcanal.DataTextField = "channel_name";
                    cmbcanal.DataBind();
                    cmbcanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                }
                else
                {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "No se Encontraron Canales para el País y la Compañia indicada, ¡Por favor verifique...!";
                }
            }
            else
            {
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un Error: " + getMessage();
            }

        }
        /// <summary>
        /// Obtener Equipos (Planning) por idCanal (Filtros)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbcanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            static_channel = cmbcanal.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);
            try
            {
                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", static_channel, compañia);
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }

            #region Limpiar Filtros

            cmbplanning.Items.Clear();
            cmbcategoria_producto.Items.Clear();
            cmbcategoria_producto.Enabled = false;
            cmbsubcategoria.Items.Clear();
            cmbsubcategoria.Enabled = false;

            cmbmarca.Items.Clear();
            cmbmarca.Enabled = false;
            cmbsku.Items.Clear();
            cmbsku.Enabled = false;
            cmbperson.Items.Clear();
            cmbperson.Enabled = false;

            cmbOficina.Items.Clear();
            cmbOficina.Enabled = false;
            cmbNodeComercial.Items.Clear();
            cmbNodeComercial.Enabled = false;
            cmbPuntoDeVenta.Items.Clear();
            cmbPuntoDeVenta.Enabled = false;

            #endregion

            // Verificar que no existan errores
            if (message.Equals(""))
            {
                if (dt.Rows.Count > 0)
                {
                    cmbplanning.DataSource = dt;
                    cmbplanning.DataValueField = "id_planning";
                    cmbplanning.DataTextField = "Planning_Name";
                    cmbplanning.DataBind();
                    cmbplanning.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                    cmbplanning.Enabled = true;
                }
                else
                {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "No se Encontraron Planning para el Canal y la Compañia indicada (Filtros), ¡Por favor verifique...!";
                }

                #region Validación para el Canal 1593
                if (cmbcanal.SelectedValue == "1593")
                {
                    lblmensaje.Visible = true;
                    lblmensaje.Text = "No Hay registros asociados para el canal seleccionado.";
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                }
                #endregion
            }
            else
            {
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un Error: " + getMessage();
            }
        }       
        /// <summary>
        /// Llenar Personal por idPlanning (En Cadena también llena Oficina, NodoComercial y Categoría)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbplanning_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt = null;

            string sidplanning = cmbplanning.SelectedValue;

            if (cmbplanning.SelectedIndex != 0)
            {
                try
                {
                    Conexion Ocoon = new Conexion();
                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);
                }
                catch (Exception ex)
                {
                    message = ex.ToString();
                }

                #region Comentada
                /*
                // Verificar que no existan errores
                if (message.Equals(""))
                {
                    // Verificar que Existan Registros
                    if (dt.Rows.Count > 0){
                        cmbcanal.DataSource = dt;
                        cmbcanal.DataValueField = "cod_Channel";
                        cmbcanal.DataTextField = "Channel_Name";
                        cmbcanal.DataBind();
                        cmbcanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                    }
                    else {
                        lblmensaje.ForeColor = System.Drawing.Color.Red;
                        lblmensaje.Text = "No se Encontraron Canales para el País y la Compañia indicada, ¡Por favor verifique...!";   
                    }
                }
                else
                {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "Ocurrió un Error: " + getMessage();
                }
                */
                #endregion

                #region Filtrar y Limpiar Combos
                cmbcategoria_producto.Items.Clear();
                cmbmarca.Items.Clear();
                cmbmarca.Enabled = false;
                cmbsku.Items.Clear();
                cmbsku.Enabled = false;
                cmbperson.Items.Clear();
                cmbperson.Enabled = false;
                #endregion

                // Verificar que no existan errores
                if (message.Equals(""))
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmbperson.DataSource = dt;
                        cmbperson.DataValueField = "Person_id";
                        cmbperson.DataTextField = "Person_NameComplet";
                        cmbperson.DataBind();
                        cmbperson.Items.Insert(0, new ListItem("---Todos---", "0"));
                        cmbperson.Enabled = true;

                        //------llamado al metodo cargar categoria de producto

                        cargarCombo_Oficina();

                        if (message.Equals(""))
                        {
                            cargarCombo_NodeComercial(sidplanning);
                        }
                        else
                        {
                            lblmensaje.ForeColor = System.Drawing.Color.Red;
                            lblmensaje.Text = "Ocurrio un Error:" + getMessage();
                        }

                        if (message.Equals(""))
                        {
                            cargarCombo_CategoriaDeproducto(sidplanning);
                        }
                        else
                        {
                            lblmensaje.ForeColor = System.Drawing.Color.Red;
                            lblmensaje.Text = "Ocurrio un Error:" + getMessage();
                        }
                        //----------------------------------------------------

                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Red;
                        lblmensaje.Text = "No se Encontró Personal para el Planning indicada, ¡Por favor verifique...!";
                    }
                }
                else
                {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "Ocurrió un Error: " + getMessage();
                }
            }
            else
            {
                cmbcategoria_producto.Items.Clear();
                cmbcategoria_producto.Enabled = false;
                cmbmarca.Items.Clear();
                cmbmarca.Enabled = false;
                cmbsku.Items.Clear();
                cmbsku.Enabled = false;
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
        /// <summary>
        /// Obtener Oficinas por idCompañia
        /// </summary>
        protected void cargarCombo_Oficina()
        {

            //if (this.Session["companyid"] != null)
            //{

            compañia = Convert.ToInt32(this.Session["companyid"]);

            DataTable dtofi = new DataTable();
            try
            {
                Conexion Ocoon = new Conexion();
                dtofi = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENEROFICINAS", compañia);
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }

            // Verificar que no existan errores
            if (message.Equals(""))
            {
                if (dtofi.Rows.Count > 0)
                {
                    cmbOficina.Enabled = true;
                    cmbOficina.DataSource = dtofi;
                    cmbOficina.DataTextField = "Name_Oficina";
                    cmbOficina.DataValueField = "cod_Oficina";
                    cmbOficina.DataBind();

                    cmbOficina.Items.Insert(0, new ListItem("---Todas---", "0"));
                }
                else
                {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "No se Encontraron Oficinas para la Compañia indicada, ¡Por favor verifique...!";
                }
            }
            else
            {
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un Error: " + getMessage();
            }

            //}
            //else
            //{
            //    Response.Redirect("~/err_mensaje_seccion.aspx", true);
            //}

        }
        /// <summary>
        /// Obtener Nodos Comerciales por idPlanning
        /// </summary>
        /// <param name="sid_planning"></param>
        protected void cargarCombo_NodeComercial(string sid_planning)
        {
            cmbNodeComercial.Items.Clear();
            Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = null;
            try{
                oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(cmbplanning.SelectedValue);
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }

            // Verificar que no existan errores
            if (message.Equals(""))
            {

                if (oListNodeComercial.Length > 0)
                {
                    cmbNodeComercial.Enabled = true;
                    cmbNodeComercial.DataSource = oListNodeComercial;
                    cmbNodeComercial.DataTextField = "commercialNodeName";
                    cmbNodeComercial.DataValueField = "NodeCommercial";
                    cmbNodeComercial.DataBind();
                    cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                }
                else
                {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "No se Encontraron Nodos Comerciales para el Planning Indicado, ¡Por favor verifique...!";
                }
            }
            else
            {
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un Error: " + getMessage();
            }
        }
        /// <summary>
        /// Cargar Categorias por IdPlanning
        /// </summary>
        /// <param name="sidplanning"></param>
        protected void cargarCombo_CategoriaDeproducto(string sidplanning)
        {
            DataTable dt = null;
            
            try{
                Conexion Ocoon = new Conexion();
                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_PRECIO", sidplanning);
            }catch (Exception ex){
                message = ex.ToString();
            }

            cmbcategoria_producto.Enabled = false;

            // Verificar que no existan errores
            if (message.Equals("")){
                if (dt.Rows.Count > 0)
                {

                    cmbcategoria_producto.DataSource = dt;
                    cmbcategoria_producto.DataValueField = "id_ProductCategory";
                    cmbcategoria_producto.DataTextField = "Product_Category";
                    cmbcategoria_producto.DataBind();
                    cmbcategoria_producto.Items.Insert(0, new ListItem("---Todas---", "0"));
                    cmbcategoria_producto.Enabled = true;

                }
                else
                {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "No se Encontraron Categorías para el Planning indicado, ¡Por favor verifique...!";  
                }
            }
            else {
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un Error: " + getMessage();
            }
        }
        /// <summary>
        /// Cargar Puntos de Venta por idPlanning y idOficina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            Conexion Ocoon = new Conexion();

            cmbPuntoDeVenta.Items.Clear();
            cmbPuntoDeVenta.Enabled = false;
            
            // Si existen Planning y Oficina Seleccionados
            if (cmbplanning.SelectedIndex > 0 && cmbOficina.SelectedIndex > 0)
            {
                DataTable dtPdv = new DataTable();
                try
                {
                    dtPdv = Ocoon.ejecutarDataTable(
                        "UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina",
                        cmbplanning.SelectedValue,
                        Convert.ToInt32(cmbOficina.SelectedValue));
                }
                catch (Exception ex) {
                    message = ex.ToString();
                }

                if (message.Equals(""))
                {
                    if (dtPdv.Rows.Count > 0)
                    {
                        cmbPuntoDeVenta.DataSource = dtPdv;
                        cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
                        cmbPuntoDeVenta.DataTextField = "pdv_Name";
                        cmbPuntoDeVenta.DataBind();
                        cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));
                        cmbPuntoDeVenta.Enabled = true;
                    }
                    else {
                        lblmensaje.ForeColor = System.Drawing.Color.Red;
                        lblmensaje.Text = "No se Encontraron Puntos de Venta para el Planning y la Oficina indicadas, ¡Por favor verifique...!";   
                    }
                }
                else {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "Ocurrió un Error: " + getMessage();
                }

                DataTable dtNodeCom = new DataTable();
                try
                {
                    dtNodeCom = Ocoon.ejecutarDataTable(
                        "UP_WEBXPLORA_AD_OBTENER_NODECOMERCIAL_BY_idPlanning_and_codOficina",
                        cmbplanning.SelectedValue,
                        Convert.ToInt32(cmbOficina.SelectedValue));
                }
                catch (Exception ex) {
                    message = ex.ToString();
                }

                // Verificar que no existan errores
                if (message.Equals("")){
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
                        lblmensaje.ForeColor = System.Drawing.Color.Red;
                        lblmensaje.Text = "No se Encontraron Nodos Comerciales para el Planning y la Oficina indicadas, ¡Por favor verifique...!";  
                    }
                } 
                else {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "Ocurrió un Error: " + getMessage();
                }
            }

            // Si solo Existen Planning Seleccionado
            if (cmbOficina.SelectedIndex == 0 && cmbplanning.SelectedIndex > 0)
            {
                cargarCombo_NodeComercial(cmbplanning.SelectedValue);
            }
            
        }
        /// <summary>
        /// Obtener Puntos de Venta por IdPlanning y IdNodoComercial
        /// Obtener Puntos de Venta por IdPlanning y IdOficina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbNodeComercial_SelectedIndexChanged(object sender, EventArgs e)
        {

            Conexion Ocoon = new Conexion();
            // Si se ha seleccionado Planning y NodoComercial
            if (cmbplanning.SelectedIndex > 0 && cmbNodeComercial.SelectedIndex > 0){
                DataTable dtPdv = new DataTable();
                try
                {
                    dtPdv = Ocoon.ejecutarDataTable(
                         "UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_and_idNodeComercial",
                         cmbplanning.SelectedValue,
                         Convert.ToInt32(cmbNodeComercial.SelectedValue));
                }
                catch (Exception ex) {
                    message = ex.ToString();
                }

                if (message.Equals("")){
                    if (dtPdv.Rows.Count > 0)
                    {
                        cmbPuntoDeVenta.DataSource = dtPdv;
                        cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
                        cmbPuntoDeVenta.DataTextField = "pdv_Name";
                        cmbPuntoDeVenta.DataBind();
                        cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));
                        cmbPuntoDeVenta.Enabled = true;
                    }
                    else {
                        lblmensaje.ForeColor = System.Drawing.Color.Red;
                        lblmensaje.Text = "No se Encontraron Puntos de Venta para la Campaña, Nodo Comercial indicados, ¡Por favor verifique...!";  
                    }
                }else{
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "Ocurrió un Error: " + getMessage();
                }
                
            }
            
            // Si no se ha Seleccionado NodoComercial
            if (cmbNodeComercial.SelectedIndex == 0)
            {

                DataTable dtPdv = new DataTable();
                try
                {
                    dtPdv = Ocoon.ejecutarDataTable(
                         "UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_AND_codOficina",
                         cmbplanning.SelectedValue,
                         Convert.ToInt32(cmbOficina.SelectedValue));
                }
                catch (Exception ex) {
                    message = ex.ToString();
                }
                // Verificar que no existan errores
                if (message.Equals("")){
                    if (dtPdv.Rows.Count > 0)
                    {
                        cmbPuntoDeVenta.DataSource = dtPdv;
                        cmbPuntoDeVenta.DataValueField = "ClientPDV_Code";
                        cmbPuntoDeVenta.DataTextField = "pdv_Name";
                        cmbPuntoDeVenta.DataBind();
                        cmbPuntoDeVenta.Items.Insert(0, new ListItem("---Todos---", "0"));
                        cmbPuntoDeVenta.Enabled = true;
                    }
                    else {
                        lblmensaje.ForeColor = System.Drawing.Color.Red;
                        lblmensaje.Text = "No se Encontraron Puntos de Venta para el Planning y la Oficina indicadas, ¡Por favor verifique...!"; 
                    }
                }
                else {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "Ocurrió un Error: " + getMessage();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmbcategoria_producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            cmbsubcategoria.Items.Clear();
            cmbsubcategoria.Enabled = false;
            //string iischannel = cmbcanal.SelectedValue;
            //string sidplanning = cmbplanning.SelectedValue;
            string siproductCategory = cmbcategoria_producto.SelectedValue;
            
            try
            {
                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_REPORT_PRECIO", siproductCategory);
            }
            catch (Exception ex) {
                message = ex.ToString();
            }

            // Verificar que no existan errores
            if (message.Equals(""))
            {

                cmbmarca.Items.Clear();
                cmbmarca.Enabled = false;
                cmbsku.Items.Clear();
                cmbsku.Enabled = false;

                if (dt.Rows.Count > 0)
                {
                    cmbmarca.DataSource = dt;
                    cmbmarca.DataValueField = "id_Brand";
                    cmbmarca.DataTextField = "Name_Brand";
                    cmbmarca.DataBind();
                    cmbmarca.Items.Insert(0, new ListItem("---Todas---", "0"));
                    cmbmarca.Enabled = true;

                    cargarGrilla_Precio();
                }
                else {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "No se Encontraron Marcas para Categoría indicada, ¡Por favor verifique...!";  
                }
            }
            else {
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un Error: " + getMessage();
            }

            

            
            //Pablo Salas
            //22/08/2011
            //se agrega el metodo cargarsubcategorias
            //cargarsubcategorias(siproductCategory);
        }
        
        #endregion

        #region Crear Registro y Cargar Excel
        /// <summary>
        /// Cargar ComboBox para Registrar Reporte de Precios y Carga Masiva de Registro de Precios
        /// </summary>
        protected void Cargarcanales()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            pais = this.Session["scountry"].ToString();

            try
            {
                dt = Ocoon.ejecutarDataTable(
                    "UP_WEBXPLORA_OPE_COMBO_CHANNEL", 
                    pais, 
                    compañia);
            }
            catch (Exception ex)
            {
                message = ex.ToString();
            }

            if (message.Equals(""))
            {
                if (dt.Rows.Count > 0)
                {
                    // DropDownList para Registrar un nuevo Precio
                    ddlCanal.DataSource = dt;
                    ddlCanal.DataValueField = "cod_Channel";
                    ddlCanal.DataTextField = "Channel_Name";
                    ddlCanal.DataBind();
                    ddlCanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));

                    // DropDownList para Realizar Carga Masiva
                    // de Precios
                    ddlCanalCargaMasiva.DataSource = dt;
                    ddlCanalCargaMasiva.DataValueField = "cod_Channel";
                    ddlCanalCargaMasiva.DataTextField = "Channel_Name";
                    ddlCanalCargaMasiva.DataBind();
                    ddlCanalCargaMasiva.Items.Insert(0, new ListItem("---Seleccione---", "0"));

                }
                else
                {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "No se Encontraron Canales para el País y la Compañia indicada (Insertar Registros de Precios), ¡Por favor verifique...!";
                }
            }
            else
            {
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un Error: " + getMessage();
            }

        }

        /// <summary>
        /// Cargar todos los DropDownList relacionado a Canales
        /// </summary>
        public void orqLoadDropDownListCanal(String idCountry,
        String idCompany) { 
            Canales oDCanales = new Canales();
            
            DataTable dt =
                oDCanales.getCanalesByIdCountryAndIdCompanyDummy(
                    idCountry,
                    idCompany
                    );
            
            String dataValueField = "cod_channel";
            String dataTextField = "channel_name";
            
            // Cargar DropDownList de Canales para Find
            fncCargarDropDownListByDataTable(
                cmbcanal,
                dt,
                dataValueField,
                dataTextField);
            if (getMessage().Equals("")){
                // Cargar DropDownList de Canales para Insert
                fncCargarDropDownListByDataTable(
                    ddlCanal,
                    dt,
                    dataValueField,
                    dataTextField);
                if (getMessage().Equals("")){
                    // Cargar DropDownList de Canales para Export Excel
                    fncCargarDropDownListByDataTable(
                        ddlCanalCargaMasiva,
                        dt,
                        dataValueField,
                        dataTextField);
                }
            }
            
            if (!getMessage().Equals("")) {
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un Error: " + getMessage();
            }
        }

        

        /// <summary>
        /// Metodo para cargar DropDownList Pasando DataTable,
        /// dataValueField y dataTextField
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="dt"></param>
        /// <param name="dataValueField"></param>
        /// <param name="dataTextField"></param>
        public void fncCargarDropDownListByDataTable(
        DropDownList ddl, DataTable dt, String dataValueField,
        String dataTextField) {
            try
            {
                ddl.DataSource = dt;
                ddl.DataValueField = dataValueField;
                ddl.DataTextField = dataTextField;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }
            catch (Exception ex) {
                message = "Ocurrio un Error al Cargar DropDownList: " 
                    + ex.Message.ToString();
            }
        }

        #endregion

        #region Maestros
        #region Canal

        protected void ddlCanal_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            static_channel = ddlCanal.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", static_channel, compañia);

            ddlCampana.Items.Clear();
            ddlCategoria.Items.Clear();
            ddlCategoria.Enabled = false;

            ddlMarca.Items.Clear();
            ddlMarca.Enabled = false;

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
            if (ddlCanal.SelectedValue == "1000")
            {
                lblPrecioLista.Visible = true;
                txtPrecioListar.Visible = true;
                lblPrecioReventa.Visible = true;
                txtPrecioReventa.Visible = true;
                lblPVP.Visible = false;
                txtPVP.Visible = false;
                lblPrecioOferta.Visible = false;
                txtPrecioOferta.Visible = false;
                lblPrecioCosto.Visible = false;
                txtPrecioCosto.Visible = false;
            }
            if (ddlCanal.SelectedValue == "1241")
            {
                lblPrecioLista.Visible = false;
                txtPrecioListar.Visible = false;
                lblPrecioReventa.Visible = false;
                txtPrecioReventa.Visible = false;
                lblPVP.Visible = true;
                txtPVP.Visible = true;
                lblPrecioOferta.Visible = true;
                txtPrecioOferta.Visible = true;
                lblPrecioCosto.Visible = false;
                txtPrecioCosto.Visible = false;

            }
            if (ddlCanal.SelectedValue == "1023")
            {
                lblPrecioLista.Visible = false;
                txtPrecioListar.Visible = false;
                lblPrecioReventa.Visible = false;
                txtPrecioReventa.Visible = false;
                lblPVP.Visible = true;
                txtPVP.Visible = true;
                lblPrecioOferta.Visible = false;
                txtPrecioOferta.Visible = false;
                lblPrecioCosto.Visible = true;
                txtPrecioCosto.Visible = true;

            }
            MopoReporPrecio.Show();
        }
        protected void ddlCanalCargaMasiva_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            static_channel = ddlCanalCargaMasiva.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", static_channel, compañia);

            ddlCampañaCargaMasiva.Items.Clear();

            if (dt.Rows.Count > 0)
            {
                ddlCampañaCargaMasiva.DataSource = dt;
                ddlCampañaCargaMasiva.DataValueField = "id_planning";
                ddlCampañaCargaMasiva.DataTextField = "Planning_Name";
                ddlCampañaCargaMasiva.DataBind();
                ddlCampañaCargaMasiva.Items.Insert(0, new ListItem("---Seleccione---", "0"));
                ddlCampañaCargaMasiva.Enabled = true;
            }

            MopoReporPrecioMasiva.Show();
        }
        
        /// <summary>
        /// Metodo Dummy para cargar Canales
        /// </summary>
        /// <param name="codUsuario"></param>
        protected void GetCanalesByUsuario(String codUsuario)
        {
            try
            {
                BL_Canal oBL_Canal = new BL_Canal();
                List<MA_Canal> oListCanal = new List<MA_Canal>();
                oListCanal = oBL_Canal.Get_CanalesByCodUsuario(codUsuario);

                foreach (MA_Canal oMA_Canal in oListCanal) {
                    ListItem listItem = new ListItem(oMA_Canal.nombre, oMA_Canal.codigo);
                    cmbcanal.Items.Add(listItem);
                }
                cmbcanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Ocurrió un error inesperado..." + ex.Message;
                
            }

        }

        #endregion
        #region Planning
        protected void ddlCampana_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            string sidplanning = ddlCampana.SelectedValue;

            if (ddlCampana.SelectedIndex != 0)
            {
                dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);

                if (dt.Rows.Count > 0)
                {
                    ddlMercaderista.DataSource = dt;
                    ddlMercaderista.DataValueField = "Person_id";
                    ddlMercaderista.DataTextField = "Person_NameComplet";
                    ddlMercaderista.DataBind();
                    ddlMercaderista.Items.Insert(0, new ListItem("---Todos---", "0"));
                    ddlMercaderista.Enabled = true;
                }

                ddlCategoria.Items.Clear();
                ddlMarca.Items.Clear();
                ddlMarca.Enabled = false;

                //------llamado al metodo cargar categoria de producto
                cargarCategoria(sidplanning);
                cargarnodecomercial(sidplanning);
                //----------------------------------------------------
            }
            else
            {
                ddlCategoria.Items.Clear();
                ddlCategoria.Enabled = false;
                ddlMarca.Items.Clear();
                ddlMarca.Enabled = false;

                ddlPuntoVenta.Items.Clear();
                ddlPuntoVenta.Enabled = false;
            }
            MopoReporPrecio.Show();
        }
        protected void ddlCampañaCargaMasiva_SelectedIndexChanged(object sender, EventArgs e)
        {
            Conexion cn = new Conexion();
            DataSet ds = cn.ejecutarDataSet("OPE_CARGAMASIVA_REPORTE_PRECIO_DATOS", ddlCampañaCargaMasiva.SelectedValue, "19");
            CreaExcel(ds);
            Datos.Visible = true;
            MopoReporPrecioMasiva.Show();
        }
        #endregion

        #region PtoVenta
        void llenarPuntoVenta1(string campaña, int NodeComercial)
        {
            Conexion Ocoon = new Conexion();


            DataTable dtPdv = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_PUNTODEVENTA_BY_idPlanning_and_idNodeComercial", campaña, NodeComercial);

            if (dtPdv.Rows.Count > 0)
            {
                ddlPuntoVenta.DataSource = dtPdv;
                ddlPuntoVenta.DataValueField = "ClientPDV_Code";
                ddlPuntoVenta.DataTextField = "pdv_Name";
                ddlPuntoVenta.DataBind();

                ddlPuntoVenta.Items.Insert(0, new ListItem("---Todos---", "0"));

                ddlPuntoVenta.Enabled = true;
            }
        }

        #endregion
        #region NodeComercial
        protected void ddlNodeComercial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                llenarPuntoVenta1(ddlCampana.SelectedValue, Convert.ToInt32(ddlNodeComercial.SelectedValue));
                MopoReporPrecio.Show();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cargarnodecomercial(string sidplanning)
        {
            try
            {
                ddlNodeComercial.Items.Clear();
                //Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(sidplanning);

                //if (oListNodeComercial.Length > 0)
                //{
                //    ddlNodeComercial.Enabled = true;
                //    ddlNodeComercial.DataSource = oListNodeComercial;
                //    ddlNodeComercial.DataTextField = "commercialNodeName";
                //    ddlNodeComercial.DataValueField = "NodeCommercial";
                //    ddlNodeComercial.DataBind();

                //    ddlNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                //}
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #region Categorias
        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {


            //string iischannel = cmbcanal.SelectedValue;
            //string sidplanning = cmbplanning.SelectedValue;
            string siproductCategory = ddlCategoria.SelectedValue;
            llenarMarca1(siproductCategory);
            MopoReporPrecio.Show();
        }
        protected void cargarCategoria(string sidplanning)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA_DE_PRODUCTO_REPORT_PRECIO", sidplanning);
            ddlCategoria.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                ddlCategoria.DataSource = dt;
                ddlCategoria.DataValueField = "id_ProductCategory";
                ddlCategoria.DataTextField = "Product_Category";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("---Todas---", "0"));
                ddlCategoria.Enabled = true;
            }
        }
        #endregion
        #region SubCategoria
        public void cargarsubcategorias(string idproductcategory)
        {
            try
            {
                DataTable dt_sc = null;
                Conexion Ocoon = new Conexion();
                if (idproductcategory.ToString() != "0")
                {
                    dt_sc = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_CONSULTASUBCATEGORIA", idproductcategory, "");
                    if (dt_sc.Rows.Count > 0)
                    {
                        cmbsubcategoria.Enabled = true;
                        cmbsubcategoria.DataSource = dt_sc;
                        cmbsubcategoria.DataTextField = "Name_Subcategory";
                        cmbsubcategoria.DataValueField = "id_Subcategory";
                        cmbsubcategoria.DataBind();
                        cmbsubcategoria.Items.Insert(0, new ListItem("---Todas---", "0"));
                    }
                }
                else
                {
                    cmbsubcategoria.Items.Clear();
                    cmbsubcategoria.Enabled = false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
        #region Marca
        protected void cmbmarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            //string iischannel = cmbcanal.SelectedValue;
            //string sidplanning = cmbplanning.SelectedValue;
            string sidmarca = cmbmarca.SelectedValue;
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SKU_PRODUCTO_REPORT_PRECIO", sidmarca);

            cmbsku.Items.Clear();
            cmbsku.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                cmbsku.DataSource = dt;
                cmbsku.DataValueField = "cod_Product";
                cmbsku.DataTextField = "productoNombre";
                cmbsku.DataBind();
                cmbsku.Items.Insert(0, new ListItem("---Todos---", "0"));
                cmbsku.Enabled = true;
            }
            //cargarGrilla_Precio();
        }
        void llenarMarca1(string siproductCategory)
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();
            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_REPORT_PRECIO", siproductCategory);

            ddlMarca.Items.Clear();
            ddlMarca.Enabled = false;
            if (dt.Rows.Count > 0)
            {
                ddlMarca.DataSource = dt;
                ddlMarca.DataValueField = "id_Brand";
                ddlMarca.DataTextField = "Name_Brand";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("---Todas---", "0"));
                ddlMarca.Enabled = true;
            }
        }
        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarcombofamilia(ddlFamilia, Convert.ToInt32(this.Session["companyid"]), Convert.ToInt32(ddlMarca.SelectedValue), Convert.ToInt32(ddlCategoria.SelectedValue));
            MopoReporPrecio.Show();
            llenaProducto(Convert.ToInt32(this.Session["companyid"]), ddlCategoria.SelectedValue, Convert.ToInt32(ddlMarca.SelectedValue));
        }

        #endregion
        #region Familia
        private void llenarcombofamilia(DropDownList ddl, int cliente, int marca, int categoria)
        {
            Conexion Ocoon = new Conexion();

            DataTable dt1 = new DataTable();
            dt1 = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_CONSULTAFAMILY", cliente, marca, categoria, "");
            this.Session["dt_familias"] = dt1;
            //se llena familias
            if (dt1.Rows.Count > 0)
            {
                ddl.DataSource = dt1;
                ddl.DataTextField = "name_Family";
                ddl.DataValueField = "id_ProductFamily";
                ddl.DataBind();
                dt1 = null;

            }
            ddl.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
        }
        protected void ddlFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarsubfamilia(ddlSubfamilia, ddlFamilia.SelectedValue);
            //llenaProducto();
            MopoReporPrecio.Show();
        }

        #endregion
        #region SubFamilia
        private void llenarsubfamilia(DropDownList ddl, string familia)
        {
            Conexion Ocoon = new Conexion();
            DataTable ds = new DataTable();
            ds = Ocoon.ejecutarDataTable("UP_WEBXPLORA_AD_BUSCAR_SUBFAMILY", 0, 0, 0, familia, "");
            //se llena Combo de marca en buscar de maestro familia de producto
            ddl.DataSource = ds;
            ddl.DataTextField = "subfam_nombre";
            ddl.DataValueField = "id_ProductSubFamily";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            ds = null;
        }

        #endregion
        #region Producto
        private void llenaProducto(int companyid, string categoria, int marca)
        {
            Conexion Ocoon = new Conexion();
            DataTable ds = new DataTable();
            ds = Ocoon.ejecutarDataTable("UP_WEB_SEARCHPRODUCT", companyid, categoria, marca, "");
            //se llena Combo de marca en buscar de maestro familia de producto
            ddlProducto.DataSource = ds;
            ddlProducto.DataTextField = "Product_Name";
            ddlProducto.DataValueField = "id_Product";
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            ds = null;
        }
        #endregion
        #region Utilitario
        private void LimpiarControles()
        {
            txtPrecioListar.Text = "";
            txtPrecioReventa.Text = "";
            ddlProducto.Items.Clear();
            ddlCanal.SelectedValue = "0";
            ddlCampana.Items.Clear();
            ddlMercaderista.Items.Clear();
            ddlNodeComercial.Items.Clear();
            ddlPuntoVenta.Items.Clear();
            ddlCategoria.Items.Clear();
            ddlMarca.Items.Clear();

            ddlCanalCargaMasiva.Items.Clear();
            ddlCampañaCargaMasiva.Items.Clear();

            Cargarcanales();
        }

        #endregion
        #endregion

        #region Botones

        #region BtnCrear
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            Cargarcanales();
        }
        protected void btnGuardarReportPrecio_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlCanal.Text == "0" || ddlCampana.Text == "0" || ddlMercaderista.Text == "0"
    || ddlNodeComercial.Text == "0" || ddlPuntoVenta.Text == "0" || ddlCategoria.Text == "0"
    || ddlMarca.Text == "0" || ddlProducto.Text == "0")
                {
                    return;
                }

                OPE_REPORTE_PRECIO oOPE_REPORTE_PRECIO = new OPE_REPORTE_PRECIO();

                DataTable dt = null;
                DataTable dtl = null;
                Conexion Ocoon = new Conexion();
                Conexion con = new Conexion(2);
                dt = Ocoon.ejecutarDataTable("UP_WEB_SEARCH_USER", "", Convert.ToInt32(ddlMercaderista.SelectedValue));
                string idperfil = dt.Rows[0]["Perfil_id"].ToString();

                dtl = con.ejecutarDataTable("STP_JVM_LISTAR_CANALES", ddlCanal.SelectedValue);
                string tipocanal = dtl.Rows[0]["CAN_TIPO"].ToString();


                Lucky.Entity.Common.Application.EOPE_REPORTE_PRECIO oEOPE_REPORTE_PRECIO = oOPE_REPORTE_PRECIO.RegistrarReportePrecio(Convert.ToInt32(ddlMercaderista.SelectedValue),
                                                            idperfil, ddlCampana.SelectedValue, this.Session["companyid"].ToString(),
                                                            ddlPuntoVenta.SelectedValue, ddlCategoria.SelectedValue, ddlMarca.SelectedValue, ddlFamilia.SelectedValue,
                                                            ddlSubfamilia.SelectedValue, tipocanal, rdtpFechaRegistro.SelectedDate.Value.ToShortDateString(), "0", "0", Convert.ToChar("0"), txtObservacion.Text);




                oOPE_REPORTE_PRECIO.RegistrarReportePrecio_Detalle(Convert.ToInt32(oEOPE_REPORTE_PRECIO.ID), ddlProducto.SelectedValue, txtPrecioListar.Text, txtPrecioReventa.Text, txtPrecioOferta.Text, txtPVP.Text, txtPrecioCosto.Text,
                                                                    txtPrecioMin.Text, txtPrecioMax.Text, txtPrecioRegular.Text, Convert.ToChar("0"));

                LimpiarControles();
                lblmensaje.Visible = true;
                lblmensaje.Text = "Fue registrado con exito el reporte de precio";
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
        #endregion

        #region BtnCargaMasiva
        protected void btnCargaMasiva_Click(object sender, EventArgs e)
        {
            if ((FileUpCMasivaPrecio.PostedFile != null) && (FileUpCMasivaPrecio.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(FileUpCMasivaPrecio.PostedFile.FileName);
                string SaveLocation = Server.MapPath("masivo") + "\\" + fn;

                if (SaveLocation != string.Empty)
                {
                    if (FileUpCMasivaPrecio.FileName.ToLower().EndsWith(".xls"))
                    {
                        // string Destino = Server.MapPath(null) + "\\PDV_Planning\\" + Path.GetFileName(FileUpPDV.PostedFile.FileName);
                        OleDbConnection oConn1 = new OleDbConnection();
                        OleDbCommand oCmd = new OleDbCommand();
                        OleDbDataAdapter oDa = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();
                        DataTable dt = new DataTable();

                        FileUpCMasivaPrecio.PostedFile.SaveAs(SaveLocation);

                        // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                        oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                        oConn1.Open();
                        oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Ope_Precio"];
                        oCmd.Connection = oConn1;
                        oDa.SelectCommand = oCmd;

                        try
                        {

                            oDa.Fill(oDs);


                            dt = oDs.Tables[0];
                            int numcol = 14; //determina el número de columnas para el datatable
                            if (dt.Columns.Count == numcol)
                            {

                                dt.Columns[0].ColumnName = "id_ClientPDV";
                                dt.Columns[1].ColumnName = "id_Categoria";
                                dt.Columns[2].ColumnName = "id_Marca";
                                dt.Columns[3].ColumnName = "Observacion";
                                dt.Columns[4].ColumnName = "Fecha";
                                dt.Columns[5].ColumnName = "cod_Producto";
                                dt.Columns[6].ColumnName = "Precio_Lista";
                                dt.Columns[7].ColumnName = "Precio_Reventa";
                                dt.Columns[8].ColumnName = "Precio_Oferta";
                                dt.Columns[9].ColumnName = "PVP";
                                dt.Columns[10].ColumnName = "Precio_Costo";
                                dt.Columns[11].ColumnName = "Precio_Min";
                                dt.Columns[12].ColumnName = "Precio_max";
                                dt.Columns[13].ColumnName = "Precio_Regular";


                                string idPlanning = ddlCampañaCargaMasiva.SelectedValue;
                                string companyid = this.Session["companyid"].ToString();
                                string usuario = this.Session["sUser"].ToString().Trim();

                                cargarCategoria(idPlanning);
                                llenarPuntoVenta1(idPlanning, 0);

                                OPE_REPORTE_PRECIO oOPE_REPORTE_PRECIO = new OPE_REPORTE_PRECIO();
                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    string id_ClientPDV = dt.Rows[i]["id_ClientPDV"].ToString();
                                    string id_Categoria = dt.Rows[i]["id_Categoria"].ToString();
                                    string id_Marca = dt.Rows[i]["id_Marca"].ToString();
                                    string Observacion = dt.Rows[i]["Observacion"].ToString();
                                    string Fecha = dt.Rows[i]["Fecha"].ToString();

                                    string cod_Producto = dt.Rows[i]["cod_Producto"].ToString();
                                    string Precio_Lista = dt.Rows[i]["Precio_Lista"].ToString();
                                    string Precio_Reventa = dt.Rows[i]["Precio_Reventa"].ToString();

                                    string Precio_Oferta = dt.Rows[i]["Precio_Oferta"].ToString();
                                    string PVP = dt.Rows[i]["PVP"].ToString();
                                    string Precio_Costo = dt.Rows[i]["Precio_Costo"].ToString();

                                    string Precio_Min = dt.Rows[i]["Precio_Min"].ToString();
                                    string Precio_max = dt.Rows[i]["Precio_max"].ToString();
                                    string Precio_Regular = dt.Rows[i]["Precio_Regular"].ToString();

                                    llenarMarca1(id_Categoria);
                                    llenaProducto(Convert.ToInt32(companyid), id_Categoria, Convert.ToInt32(id_Marca));

                                    ddlPuntoVenta.Items.FindByValue(id_ClientPDV).Selected = true;
                                    ddlCategoria.Items.FindByValue(id_Categoria).Selected = true;
                                    ddlMarca.Items.FindByValue(id_Marca).Selected = true;



                                    DataTable dtl1 = null;
                                    DataTable dtl = null;
                                    Conexion Ocoon = new Conexion();
                                    Conexion con = new Conexion(2);
                                    dtl1 = Ocoon.ejecutarDataTable("UP_WEB_SEARCH_USER", "", Convert.ToInt32(this.Session["personid"].ToString()));
                                    string idperfil = dtl1.Rows[0]["Perfil_id"].ToString();

                                    dtl = con.ejecutarDataTable("STP_JVM_LISTAR_CANALES", ddlCanalCargaMasiva.SelectedValue);
                                    string tipocanal = dtl.Rows[0]["CAN_TIPO"].ToString();


                                    Lucky.Entity.Common.Application.EOPE_REPORTE_PRECIO oEOPE_REPORTE_PRECIO = 
                                    oOPE_REPORTE_PRECIO.RegistrarReportePrecio(Convert.ToInt32(this.Session["personid"].ToString()),
                                    idperfil, ddlCampañaCargaMasiva.SelectedValue, this.Session["companyid"].ToString(),
                                    id_ClientPDV, id_Categoria, id_Marca, "0",
                                    "", tipocanal, Fecha, "0", "0", Convert.ToChar("0"), Observacion);

                                    oOPE_REPORTE_PRECIO.RegistrarReportePrecio_Detalle(
                                        Convert.ToInt32(oEOPE_REPORTE_PRECIO.ID), cod_Producto, Precio_Lista, 
                                        Precio_Reventa, Precio_Oferta, PVP, Precio_Costo,
                                        Precio_Min, Precio_max, Precio_Regular, Convert.ToChar("0"));
                                }

                                LimpiarControles();
                                lblmensaje.Visible = true;
                                lblmensaje.Text = "Fue registrado con exito el reporte de precio";
                                lblmensaje.ForeColor = System.Drawing.Color.Blue;


                                //realiza las validaciones y carga los productos a planning.
                                //DataSet dsCargar = oCoon.ejecutarDataSet("UP_WEBXPLORA_PLA_CARGAMASIVA_PRECIO_PLANNING_TMP",
                                //idPlanning,
                                //Convert.ToInt32(id_ReportsPlanning),
                                //usuario, DateTime.Now,
                                //usuario, DateTime.Now);





                            }
                            //}
                        }
                        catch
                        {
                            lbl_mensaje_verifica.Text = "Por favor revise la consistencia de la información";
                        }
                    }
                }
            }
        }

        #endregion

        #region BtnBuscar
        /// <summary>
        /// Mostrar información correspondiente al Reporte de Precios
        /// en el AspControl GridView 'GridView1', la idea inicial era
        /// utilizar un solo AspControl GridView para los diferentes 
        /// Canales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            //cargarGrilla_Precio();
            fillGridViewPrecioDummy();
        }

        #endregion

        #region BtnExportarExcel
        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                gv_precioToExcel.Visible = true;
                GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Precio", this.gv_precioToExcel);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void btn_exportarexcel_Click(object sender, EventArgs e)
        {
            try
            {
                gv_precioToExcel.Visible = true;
                GridViewExportUtil.ExportToExcelMethodTwo("Reporte_Precio", this.gv_precioToExcel);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        private void CreaExcel(DataSet ds)
        {
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbooks oLibros = default(Microsoft.Office.Interop.Excel.Workbooks);
            Microsoft.Office.Interop.Excel.Workbook oLibro = default(Microsoft.Office.Interop.Excel.Workbook);

            Microsoft.Office.Interop.Excel.Sheets oHojas = default(Microsoft.Office.Interop.Excel.Sheets);

            Microsoft.Office.Interop.Excel.Worksheet oHoja = default(Microsoft.Office.Interop.Excel.Worksheet);


            Microsoft.Office.Interop.Excel.Range oCeldas = default(Microsoft.Office.Interop.Excel.Range);

            try
            {


                string sFile = null;
                string sTemplate = null;

                // Usamos una plantilla para crear el nuevo excel

                sFile = Server.MapPath("masivo") + "\\" + "DATOS_CARGA_REPORTE_PRECIO.xls";

                sTemplate = Server.MapPath("masivo") + "\\" + "Template.xls";

                oExcel.Visible = false;

                oExcel.DisplayAlerts = false;

                // Abrimos un nuevo libro

                oLibros = oExcel.Workbooks;

                oLibros.Open(sTemplate);

                oLibro = oLibros.Item[1];

                oHojas = oLibro.Worksheets;


                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    oHoja = (Microsoft.Office.Interop.Excel.Worksheet)oHojas.Item[i + 1];

                    oHoja.Name = "Hoja" + (i + 1);


                    string col = "";
                    int columnas = ds.Tables[i].Columns.Count;
                    switch (columnas)
                    {
                        case 1:
                            col = "A";
                            break;
                        case 2:
                            col = "B";
                            break;
                        case 3:
                            col = "C";
                            break;
                        case 4:
                            col = "D";
                            break;
                        case 5:
                            col = "E";
                            break;
                        case 6:
                            col = "F";
                            break;

                    }



                    oCeldas = oHoja.Cells;
                    //oHoja.Range["B2"].Interior.Color = 0;
                    //oHoja.Range["B2"].Font.Color = 16777215;
                    oHoja.Range["A2", col + "2"].Interior.Color = 0;
                    oHoja.Range["A2", col + "2"].Font.Color = 16777215;


                    oHoja.Range["A2", col + "2"].Font.Bold = true;
                    //oHoja.Range["A2"].Font.Bold = true;

                    oHoja.Range["A2", col + (ds.Tables[i].Rows.Count + 2).ToString()].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDash;
                    oHoja.Range["A2", col + (ds.Tables[i].Rows.Count + 2).ToString()].Borders.LineStyle = Microsoft.Office.Interop.Excel.XlBorderWeight.xlHairline;




                    VuelcaDatos(ds.Tables[i], oCeldas);


                }



                oHoja.SaveAs(sFile);

                oLibro.Close();

                // Eliminamos lo que hemos creado

                oExcel.Quit();

                oExcel = null;

                oLibros = null;

                oLibro = null;

                oHojas = null;

                oHoja = null;

                oCeldas = null;

                System.GC.Collect();

            }
            catch
            {
                oLibro.Close();
                oExcel.Quit();

                lblmensaje.Text = "se cargo correctamente";
                //Pmensaje.CssClass = "MensajesSupervisor";
                //lblencabezado.Text = "Sr. Usuario";
                //lblmensajegeneral.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                //Mensajes_Usuario();
            }

        }
        private void VuelcaDatos(DataTable tabla, Microsoft.Office.Interop.Excel.Range oCells)
        {

            DataRow dr = null;
            object[] ary = null;

            int iRow = 0;
            int iCol = 0;

            // Sacamos las cabeceras


            for (iCol = 0; iCol <= tabla.Columns.Count - 1; iCol++)
            {
                oCells[2, iCol + 1] = tabla.Columns[iCol].ToString();


            }


            // Sacamos los datos


            for (iRow = 0; iRow <= tabla.Rows.Count - 1; iRow++)
            {
                dr = tabla.Rows[iRow];

                ary = dr.ItemArray;


                for (iCol = 0; iCol <= ary.GetUpperBound(0); iCol++)
                {
                    oCells[iRow + 3, iCol + 1] = ary[iCol].ToString();

                }

            }

        }
        #endregion


        #region Validaciones

        #region BtnPrecioCero
        //boton para ejecutar metodo de verificacion
        protected void btn_verificar_Click(object sender, EventArgs e)
        {
            load_indicators();

        }
        //metodo de verificacion de registros con valores cero
        private void load_indicators()
        {
            DataTable registros = (DataTable)this.Session["dt_registros"];

            string celda1 = "";
            string celda2 = "";

            if (cmbcanal.SelectedValue.Equals("1000"))//mayorista
            {
                celda1 = "Precio lista"; //Precio de Lista
                celda2 = "Precio reventa"; //Precio de Reventa
            }
            else if (cmbcanal.SelectedValue.Equals("1241"))//autoservicios
            {
                celda1 = "Precio punto de venta"; //precios PDV
                celda2 = "precio de oferta"; //Precio oferta
            }

            for (int i = 0; i < registros.Rows.Count; i++)
            {
                string precio_1 = registros.Rows[i][celda1].ToString();
                string precio_2 = registros.Rows[i][celda2].ToString();

                if (precio_1.Equals("0") || precio_2.Equals("0"))
                {
                    //mantiene los registros con valores 0.
                }
                else
                    registros.Rows[i].Delete();
            }
            registros.AcceptChanges();

            for (int i = 0; i < registros.Rows.Count; i++)
            {
                registros.Rows[i]["ROWID"] = i + 1;
            }
            GridView1.DataSource = registros;
            GridView1.DataBind();
            GridView1.PageIndex = 0;
            GridView1.Visible = true;
            dgv_faltantes.Visible = false;
            lbl_mensaje_verifica.Text = "Se encontró " + registros.Rows.Count + " registros con valores cero.";
        }
        #endregion

        #region FueraPeriodo
        protected void btn_verfecha_Click(object sender, EventArgs e)
        {
            Conexion Ocoon = new Conexion();

            iidperson = 0;
            if (cmbperson.SelectedIndex >= 0)
                iidperson = Convert.ToInt32(cmbperson.SelectedValue);

            sidplanning = cmbplanning.SelectedValue;
            sidchannel = cmbcanal.SelectedValue;
            icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);
            iidNodeComercial = Convert.ToInt32(cmbNodeComercial.SelectedValue);
            sidPDV = cmbPuntoDeVenta.SelectedValue;
            if (sidPDV == "")
                sidPDV = "0";
            sidcategoriaProducto = cmbcategoria_producto.SelectedValue;
            sidmarca = cmbmarca.SelectedValue;
            if (sidcategoriaProducto == "")
                sidcategoriaProducto = "0";
            //se agrega el filtro para la subcategoria
            sidproductSubcategory = cmbsubcategoria.SelectedValue;
            if (sidproductSubcategory == "")
                sidproductSubcategory = "0";

            scodproducto = cmbsku.SelectedValue;
            if (scodproducto == "")
                scodproducto = "0";
            if (sidmarca == "")
                sidmarca = "0";


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
                DataTable registros = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_REPORTE_PRECIOS_FUERA_RANGO", iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto, sidproductSubcategory, sidmarca, scodproducto, dfecha_inicio, dfecha_fin);

                this.Session["dt_registros"] = registros;
                llenagrilla();
                lbl_mensaje_verifica.Text = "Se encontró " + registros.Rows.Count + " registros fuera de período.";
            }


        }
        #endregion

        #region Faltantes
        protected void btn_verfaltante_Click(object sender, EventArgs e)
        {
            Conexion Ocoon = new Conexion();
            DateTime fechaini;
            //DateTime fechafin;
            fechaini = txt_fecha_inicio.SelectedDate.Value;
            DataTable registros = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_LISTA_PROD_X_PDV_FALTANTES", cmbplanning.SelectedValue, fechaini.Year, fechaini.Month);

            //dgv_faltantes
            dgv_faltantes.DataSource = registros;
            dgv_faltantes.DataBind();
            dgv_faltantes.PageIndex = 1;
            dgv_faltantes.Visible = true;
            GridView1.Visible = false;
            lbl_mensaje_verifica.Text = "Se encontró " + registros.Rows.Count + " registros faltantes por relevar.";
        }
        protected void btn_validar_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = GridView1.Rows[i];
                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox cb_validar = (CheckBox)row.FindControl("cb_validar");

                    Label lbl_validar = (Label)row.FindControl("lbl_validar");
                    Label lbl_Id_Detalle_Precio = (Label)row.FindControl("lbl_Id_Detalle_Precio");


                    int id = Convert.ToInt32(lbl_Id_Detalle_Precio.Text);
                    bool validar = cb_validar.Checked;

                    update_precio_detalle_validado(id, validar);
                    if (validar == true)
                    {

                        lbl_validar.Text = "valido";
                        lbl_validar.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_validar.Text = "invalidado";
                        lbl_validar.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }

            //cargarGrilla_Competencias();
        }

        #endregion

        #endregion
        #endregion

        #region Grilla
        #region CargarGrilla
        /// <summary>
        /// Carga la información correspondiente al registro de Precios, 
        /// en el AspControl GridView 'GridView1', se utiliza este método
        /// para mostrar u ocultar columnas dependiendo el id del canal 
        /// y de la campaña.
        /// </summary>
        protected void cargarGrilla_Precio()
        {
            // Mostrar el AspControl GridView 'GridView1'
            GridView1.Visible = true;
            // Ocultar el AspControl GridView 'dgv_faltantes', que se 
            // utiliza como auxiliar para Exportar los registros a 
            // Excel. 
            dgv_faltantes.Visible = false;
            // Limpiar el AspControl Label 'lblmensaje', utilizado para
            // mostrar mensajes de error, se podría utilizar un modal
            // pop up, para mostrar los errores.
            lblmensaje.Text = "";

            //------------------------volver a deshabilitgar las columnas--------------------------
            GridView1.Columns[10].Visible = false;
            GridView1.Columns[11].Visible = false;
            GridView1.Columns[12].Visible = false;
            GridView1.Columns[13].Visible = false;
            GridView1.Columns[14].Visible = false;
            //GridView1.Columns[15].Visible = false;
            // ---------------------------------------------------

            //Facade_Proceso_Operativo.Facade_Proceso_Operativo obj_Facade_Proceso_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
            // DataTable 'dt' para Obtener la Consulta Tradicional para el Reporte de Precio
            DataTable dt = null;
            // DataTable 'dt02' para Obtener la Nueva Consulta para el Reporte de Precio.
            DataTable dt02 = null;

            #region Validación idParametros
            // Setear la variable idPersona a Cero(0), en caso no se haya
            // seleccionado
            iidperson = 0;
            // Verificar que el aspControl DropDownList 'cmbperson' tenga
            // un elemento seleccionado para setearlo a la variable 
            // 'iidperson'
            if (cmbperson.SelectedIndex >= 0){
                iidperson = Convert.ToInt32(cmbperson.SelectedValue);
            }

            // La variable 'sidplanning' es obligatoria, pero igualmente
            // se puede hacer la validación que el AspControl 
            // DropDownList 'cmbplanning' tenga un valor seleccionado
            if (cmbplanning.SelectedIndex >= 0){
                sidplanning = cmbplanning.SelectedValue;
            }

            // La variable 'sidchannel' es obligatorio, pero igualmente
            // se puede hacer la validación que el AspControl 
            // DropDownList 'cmbcanal' tenga un valor seleccionado
            if (cmbcanal.SelectedIndex >= 0){
                sidchannel = cmbcanal.SelectedValue;
            }

            // Asignación de la variable 'icod_oficina', para la gestión
            // el Id de la Oficina, previamente verificar que el 
            // AspControl DropDownList 'cmbOficina' tenga un valor
            // seleccionado.
            if (!cmbOficina.SelectedValue.Equals("")){
                icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);
            }
            
            // Asignación de la variable NodoComercial que esta definida
            // como 'iidNodeComercial', previamente se verifica que el 
            // AspControl DropDownList 'cmbNodeComercial' tenga un valor
            // seleccionado.
            if (!cmbNodeComercial.SelectedValue.Equals("")) {
                iidNodeComercial = Convert.ToInt32(cmbNodeComercial.SelectedValue);
            }

            // Setea el valor de la variable 'sidPDV' como el valor 
            // seleccionado por el AspControl DropDownList 
            // 'cmbPuntoDeVenta', se puede mejorar la implementación
            sidPDV = "0";
            if (!cmbPuntoDeVenta.SelectedValue.Equals("")){
                sidPDV = cmbPuntoDeVenta.SelectedValue;
            }
            //if (sidPDV == ""){ sidPDV = "0"; }

            // Setear el valor de la variable 'sidcategoriaProducto' a 0.
            sidcategoriaProducto = "0";
            // Verificar si el AspControl DropDownList 
            // 'cmbcategoria_producto', tiene productos seleccionados
            if (!cmbcategoria_producto.SelectedValue.Equals("")) {
                sidcategoriaProducto = cmbcategoria_producto.SelectedValue;
            }
            //if (sidcategoriaProducto == ""){ sidcategoriaProducto = "0";}

            // Setear el valor de la variable 'sidmarca' a 0.
            sidmarca = "0";
            // Verificar si el AspControl DropDownList 'cmbmarca'
            // tiene valor seleccionado.
            if (!cmbcategoria_producto.SelectedValue.Equals("")){
                sidmarca = cmbmarca.SelectedValue;
            }
            //if (sidmarca == ""){sidmarca = "0";}                
                
            // Setear el valor de la variable 'idSubCategoria' a 0.
            sidproductSubcategory = "0";
            // Verificar si el AspControl DropDownList 'cmbsubcategoria'
            // tiene valor seleccionado.
            if (!cmbsubcategoria.SelectedValue.Equals("")) {
                sidproductSubcategory = cmbsubcategoria.SelectedValue;
            }
            //if (sidproductSubcategory == ""){
            //    sidproductSubcategory = "0";
            //}

            // Setear el valor de la variable 'scodproducto' a 0.
            scodproducto = "0";
            // Verificar si el AspControl DropDownList 'cmbsku',
            // tiene valor seleccionado.
            if (!cmbsku.SelectedValue.Equals("")){
                scodproducto = cmbsku.SelectedValue;
            }
            //if (scodproducto == ""){ scodproducto = "0";}

            #endregion 

            // Variable para la fecha de Inicio
            DateTime dfecha_inicio;
            // Variable para la fecha de Fin
            DateTime dfecha_fin;

            // Validación Fecha Inicio (Asigna la mínima fecha cuando la fecha es Vacia)
            if (txt_fecha_inicio.SelectedDate.ToString() == ""
                || txt_fecha_inicio.SelectedDate.ToString() == "0"
                || txt_fecha_inicio.SelectedDate == null){
                dfecha_inicio = txt_fecha_inicio.MinDate;
            }
            else{
                dfecha_inicio = txt_fecha_inicio.SelectedDate.Value;
            }

            // Validación Fecha Fin (Asigna la máxima fecha permitida cuando la fecha es Vacia)
            if (txt_fecha_fin.SelectedDate.ToString() == ""
                || txt_fecha_fin.SelectedDate.ToString() == "0"
                || txt_fecha_fin.SelectedDate == null){
                dfecha_fin = txt_fecha_fin.MaxDate;
            }
            else{
                dfecha_fin = txt_fecha_fin.SelectedDate.Value;
            }

            // Validación de Fechas Iguales
            if (DateTime.Compare(dfecha_inicio, dfecha_fin) == 1){
                lblmensaje.Visible = true;
                lblmensaje.Text = "La fecha de inicio debe ser menor o igual a la fecha fin";
                lblmensaje.ForeColor = System.Drawing.Color.Red;
            }
            else{
                //Ing Ditmar Estrada 20/01/2011
                //dt = obj_Facade_Proceso_Operativo.Get_ReportePrecio(iidperson, sidplanning, sidchannel, sidcategoriaProducto, 
                //sidmarca, scodproducto, dfecha_inicio, dfecha_fin);
                //este metodo se suspendio temporalmente hasta corregir la data de caracteres especiales, por miestras 
                //se usara el metodo ejecutarDataTable que se muestra acontinuacion.
                Conexion Ocoon = new Conexion();
                //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_PRECIO", iidperson, sidplanning, sidchannel,
                //icod_oficina,iidNodeComercial,sidPDV, sidcategoriaProducto, sidmarca, scodproducto, dfecha_inicio, dfecha_fin);
                // Validar que la consulta a la base de datos, no tenga problemas de conectividad
                try{
                    
                    // Invocando a la Consulta de Precio Tradicional
                    dt = Ocoon.ejecutarDataTable(
                        "UP_WEBXPLORA_OPE_CONSULTA_PRECIO",
                        iidperson, sidplanning, sidchannel, icod_oficina, iidNodeComercial, sidPDV, sidcategoriaProducto,
                        sidproductSubcategory, sidmarca, scodproducto, dfecha_inicio, dfecha_fin);

                    // Invocando a la Nueva Consulta para el Reporte de Precio (falta Asignarle un dt)
                    dt02 = oBLOpeReportePrecio.find(sidplanning, 
                        sidchannel, icod_oficina, iidNodeComercial, 
                        sidPDV, sidcategoriaProducto, sidmarca,
                        sidcategoriaProducto, sidproductSubcategory, 
                        iidperson, dfecha_inicio, dfecha_fin);

                }catch (Exception ex) {
                    message = ex.ToString();
                }

                // Validar que no se haya presentando problemas en la consulta del precio.
                if (message.Equals("")){
                    // Validar que exista más de 1 registro en la consulta devuelta.
                    if (dt.Rows.Count > 0)
                    {
                        //Angel Ortiz 04/10/2011
                        // se guarda en variable de Session 'dt_registros' para utilizarlo en validacon para controles de alertas.
                        this.Session["dt_registros"] = dt;
                        this.Session["codigocanal"] = sidchannel;
                        
                        // Llenar el AspControl GridView 'GridView1' para el Reporte de Precios Tradicional.
                        llenagrilla();

                        lblmensaje.ForeColor = System.Drawing.Color.Green;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                        btn_verificar.Enabled = true;
                    }
                    else
                    {
                        lblmensaje.ForeColor = System.Drawing.Color.Blue;
                        lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
                        GridView1.DataBind();
                    }    
                
                    // Validar que exista más de 1 registro para la consulta de Precios (Nueva Versión)
                    if (dt02.Rows.Count > 0){
                        llenarGridViewPrice(dt02);
                        if (!getMessage().Equals("")) {
                            lblmensaje.ForeColor = System.Drawing.Color.Red;
                            lblmensaje.Text = "Ocurrió un Error: " + getMessage();                        
                        }
                    }
                    else {
                        lblmensaje.ForeColor = System.Drawing.Color.Red;
                        lblmensaje.Text = "Ocurrió un Error: " + "No se encontraron Registros, ¡Por favor Verifique!";
                    }
                }
                else {
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                    lblmensaje.Text = "Ocurrió un Error: " + getMessage();
                }
            }

            //}
            //catch (Exception ex)
            //{
                //ex.Message.ToString();
                //lblmensaje.ForeColor = System.Drawing.Color.Red;
                //lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
                //GridView1.DataBind();

                //System.Threading.Thread.Sleep(8000);
                //Response.Redirect("~/err_mensaje_seccion.aspx", true);

            //}
        }


        /// <summary>
        /// Llenar AspControl GridView 'GridView1'. Como mejora, se puede considerar, recibir como parametro las variable 'idGridView' y un DataTable
        /// que funcione como fuente de información.
        /// </summary>
        private void llenagrilla()
        {
            // Recuperar el valor de la consulta del Reporte de Precios almacenado en Session y guardarlo en la variable local 'dt'.
            DataTable dt = (DataTable)this.Session["dt_registros"];
            // Recuperar el CodCanal y asignarlo a la variable local 'sidchannel'.
            string sidchannel = this.Session["codigocanal"].ToString();

            //mayorista
            if (String.Equals(sidchannel, "1000")){
                //GridView1.Columns[11].Visible = true;//Precio de Oferta
                GridView1.Columns[12].Visible = true;//Precio de Lista
                GridView1.Columns[13].Visible = true;//Precio de Reventa
            }
            //minorista
            else if (String.Equals(sidchannel, "1023")){
                GridView1.Columns[10].Visible = true;   //precios PDV
                GridView1.Columns[14].Visible = true;   //Precio de Costo
                //GridView1.Columns[12].Visible = true;
            }
            //autoservicios
            else if (String.Equals(sidchannel, "1241")){
                //GridView1.Columns[9].Visible = true;
                //GridView1.Columns[13].Visible = true; //Precio Reventa                            
                GridView1.Columns[10].Visible = true; //precios PDV
                GridView1.Columns[11].Visible = true; //Precio oferta
                //GridView1.Columns[14].Visible = true; //precio costo
                //GridView1.Columns[15].Visible = true; //observacion
            }

            //this.Session["dt_registros"] = dt;//asignamos la varia

            // Validar que exista más de 1 registro en la consulta devuelta.
            if (dt.Rows.Count > 0) {

                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.PageIndex = 0;
                GridView1.Visible = true;

                // Ocultar AspControl GridView 'dgv_faltantes'
                dgv_faltantes.Visible = false;

                // Llenar el AspControl GridView 'gv_precioToExcel', para exportar a Excel
                // (Considero que no debería llamarse, porque satura la consulta, se dehabilitará temporalmente)
                //gv_precioToExcel.DataSource = dt;
                //gv_precioToExcel.DataBind();

                //lblmensaje.ForeColor = System.Drawing.Color.Green;
                //lblmensaje.Text = "Se encontró " + dt.Rows.Count + " resultados";
            }
            
        }

        /// <summary>
        /// Llenar el GridView Precio Dummy
        /// </summary>
        private void fillGridViewPrecioDummy() {
            DateTime dfecha = new DateTime();
            try{
                
                DataTable dt = oBLOpeReportePrecio.findDummy(
                sidplanning, sidchannel, icod_oficina, 
                iidNodeComercial, sidPDV, sidcategoriaProducto, 
                sidmarca, sidcategoriaProducto, 
                sidproductSubcategory, iidperson, dfecha, dfecha);

                llenarGridViewPrice(dt);

            }catch (Exception ex) {
                message = "Ocurrio un Error Al intentar llenar "
                    + "el GridView Dummy: " + ex.Message.ToString();
            }

        }

        /// <summary>
        /// Llenar Grilla de Precios (Cabeceras)
        /// </summary>
        /// <param name="dt"></param>
        private void llenarGridViewPrice(DataTable dt){

            // Validación de la cantidad de registros en DataTable 'dt'
            if (dt.Rows.Count > 0){
                
                gvReportePrecio.DataSource = dt;
                gvReportePrecio.DataBind();
                gvReportePrecio.PageIndex = 0;
                gvReportePrecio.Visible = true;

            }else{

                message = "No se han Encontrado registros para mostrar " +
                "para el reporte de precios, ¡Por favor Verifique!";

            }
        }

        #endregion

        #region OrdenarGrilla
        //Codigo para Ordenar y Paginar la grilla de "precios"------------------
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
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = GridView1.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                GridView1.DataSource = dataView;
                GridView1.DataBind();
            }

        }

        #endregion

        #region EventosGrilla
        protected void update_precio_detalle_validado(int id, bool validar)
        {

            try
            {
                Conexion Ocoon = new Conexion();

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_PRECIO_DETALLE_VALIDADO", id, validar);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void gv_precios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

            llenagrilla();

            GridViewRow row = GridView1.Rows[GridView1.EditIndex];

            Label lbl_fec_Reg = row.FindControl("lbl_fec_Reg") as Label;
            lbl_fec_Reg.Visible = false;
            RadDateTimePicker RadDateTimePicker_fec_reg = (RadDateTimePicker)row.FindControl("RadDateTimePicker_fec_reg");
            RadDateTimePicker_fec_reg.Visible = true;
            RadDateTimePicker_fec_reg.DbSelectedDate = Convert.ToDateTime(lbl_fec_Reg.Text);

            if (static_channel == "1000")
            {
                Label lbl_gvp_precio_lista = row.FindControl("lbl_gvp_precio_lista") as Label;
                Label lbl_gvp_precio_reventa = row.FindControl("lbl_gvp_precio_reventa") as Label;
                RadNumericTextBox txt_gvp_precio_lista = (RadNumericTextBox)row.FindControl("txt_gvp_precio_lista");
                RadNumericTextBox txt_gvp_precio_reventa = (RadNumericTextBox)row.FindControl("txt_gvp_precio_reventa");

                txt_gvp_precio_lista.DbValue = Convert.ToDouble(lbl_gvp_precio_lista.Text.Trim());
                txt_gvp_precio_reventa.DbValue = Convert.ToDouble(lbl_gvp_precio_reventa.Text.Trim());
                lbl_gvp_precio_lista.Visible = false;
                lbl_gvp_precio_reventa.Visible = false;

                txt_gvp_precio_lista.Visible = true;
                txt_gvp_precio_reventa.Visible = true;
            }
            else if (static_channel == "1023")
            {
                Label lbl_gvp_precio_pdv = row.FindControl("lbl_gvp_precio_pdv") as Label;
                Label lbl_gvp_precio_costo = row.FindControl("lbl_gvp_precio_costo") as Label;
                RadNumericTextBox txt_gvp_precio_pdv = (RadNumericTextBox)row.FindControl("txt_gvp_precio_pdv");
                RadNumericTextBox txt_gvp_precio_costo = (RadNumericTextBox)row.FindControl("txt_gvp_precio_costo");

                txt_gvp_precio_pdv.DbValue = Convert.ToDouble(lbl_gvp_precio_pdv.Text.Trim());
                txt_gvp_precio_costo.DbValue = Convert.ToDouble(lbl_gvp_precio_costo.Text.Trim());
                lbl_gvp_precio_pdv.Visible = false;
                lbl_gvp_precio_costo.Visible = false;

                txt_gvp_precio_pdv.Visible = true;
                txt_gvp_precio_costo.Visible = true;
            }
            else if (static_channel == "1241")
            {
                Label lbl_gvp_precio_pdv = row.FindControl("lbl_gvp_precio_pdv") as Label;
                Label lbl_gvp_precio_oferta = row.FindControl("lbl_gvp_precio_oferta") as Label;
                RadNumericTextBox txt_gvp_precio_pdv = (RadNumericTextBox)row.FindControl("txt_gvp_precio_pdv");
                RadNumericTextBox txt_gvp_precio_oferta = (RadNumericTextBox)row.FindControl("txt_gvp_precio_oferta");

                txt_gvp_precio_pdv.DbValue = Convert.ToDouble(lbl_gvp_precio_pdv.Text.Trim());
                txt_gvp_precio_oferta.DbValue = Convert.ToDouble(lbl_gvp_precio_oferta.Text.Trim());
                lbl_gvp_precio_pdv.Visible = false;
                lbl_gvp_precio_oferta.Visible = false;

                txt_gvp_precio_pdv.Visible = true;
                txt_gvp_precio_oferta.Visible = true;
            }

        }
        protected void gv_precios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Conexion Ocoon = new Conexion();

            try
            {
                GridViewRow row = GridView1.Rows[GridView1.EditIndex];

                Label lbl_Id_Detalle_Precio = (Label)row.FindControl("lbl_Id_Detalle_Precio");
                RadDateTimePicker RadDateTimePicker_fec_reg = (RadDateTimePicker)row.FindControl("RadDateTimePicker_fec_reg");

                int iid_det_prec = Convert.ToInt32(lbl_Id_Detalle_Precio.Text.Trim());

                string precio_lista = String.Empty;
                string precio_reventa = String.Empty;
                string precio_pdv = String.Empty;
                string precio_costo = String.Empty;
                string precio_oferta = String.Empty;
                if (static_channel == "1000")
                {
                    RadNumericTextBox txt_gvp_precio_lista = (RadNumericTextBox)row.FindControl("txt_gvp_precio_lista");
                    RadNumericTextBox txt_gvp_precio_reventa = (RadNumericTextBox)row.FindControl("txt_gvp_precio_reventa");

                    precio_lista = txt_gvp_precio_lista.DbValue.ToString();
                    precio_reventa = txt_gvp_precio_reventa.DbValue.ToString();
                    precio_pdv = "-1";
                    precio_costo = "-1";
                    precio_oferta = "-1";

                }
                else if (static_channel == "1023")
                {
                    RadNumericTextBox txt_gvp_precio_pdv = (RadNumericTextBox)row.FindControl("txt_gvp_precio_pdv");
                    RadNumericTextBox txt_gvp_precio_costo = (RadNumericTextBox)row.FindControl("txt_gvp_precio_costo");

                    precio_lista = "-1";
                    precio_reventa = "-1";
                    precio_pdv = txt_gvp_precio_pdv.DbValue.ToString();
                    precio_costo = txt_gvp_precio_costo.DbValue.ToString();
                    precio_oferta = "-1";

                }
                else if (static_channel == "1241")
                {
                    RadNumericTextBox txt_gvp_precio_pdv = (RadNumericTextBox)row.FindControl("txt_gvp_precio_pdv");
                    RadNumericTextBox txt_gvp_precio_oferta = (RadNumericTextBox)row.FindControl("txt_gvp_precio_oferta");


                    precio_lista = "-1";
                    precio_reventa = "-1";
                    try
                    {
                        precio_pdv = txt_gvp_precio_pdv.DbValue.ToString();
                    }
                    catch (Exception)
                    {
                        precio_pdv = "";
                    }
                    precio_costo = "-1";

                    try
                    {
                        precio_oferta = txt_gvp_precio_oferta.DbValue.ToString();
                    }
                    catch (Exception)
                    {
                        precio_oferta = "";
                    }
                }
                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_PRECIO_DETALLE_PRECIOS", iid_det_prec, precio_lista, precio_reventa, precio_oferta, precio_pdv, precio_costo, RadDateTimePicker_fec_reg.DbSelectedDate, Session["sUser"].ToString(), DateTime.Now);


                GridView1.EditIndex = -1;
                cargarGrilla_Precio();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();

                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        protected void gv_precios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            llenagrilla();
        }
        
        /// <summary>
        /// Ejecutar para cada fila del AspGridView 'gvReportePrecio'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                DataTable dt = new DataTable();
                String data = gvReportePrecio.DataKeys[e.Row.RowIndex].Value.ToString();
                String[] keys = data.Split('|');
                String day = keys[0];
                String idPuntoDeVenta = keys[1];

                GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;
                try{
                    dt = oBLOpeReportePrecio.findDetailsDummy(
                        idPuntoDeVenta,
                        DateTime.Parse(day));
                    if(oBLOpeReportePrecio.getMessages().Equals("")){
                        if(dt.Rows.Count > 0){
                            gvDetails.DataSource = dt;
                            gvDetails.DataBind();
                        }else{
                            message = "Error: No se encontraron detalles " +
                                " para la Cabecera indicada, " +
                                "!por favor verifique!";
                        }
                    }

                }catch(Exception ex){
                    message = "Error: " + ex.Message.ToString();
                }
                
            }
        }
        #endregion

        #region PaginadoGrilla
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            llenagrilla();
        }

        #endregion

        #region SeleccionarTodosLosElementos
        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb_all = (CheckBox)GridView1.HeaderRow.FindControl("cb_all");
            bool validar = cb_all.Checked;

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = GridView1.Rows[i];
                if (row.RowType == DataControlRowType.DataRow)
                {


                    CheckBox cb_validar = (CheckBox)row.FindControl("cb_validar");


                    if (validar == true && cb_validar.Enabled == true)
                    {
                        cb_validar.Checked = true;
                    }
                    else if (validar == false && cb_validar.Enabled == true)
                    {
                        cb_validar.Checked = false;
                    }

                }
            }
        }

        #endregion
        #endregion

        #region Dummy
        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Retorna todas la variables de Session que devuelve un Login
        /// </summary>
        private void fncLoginDummy() {
            try
            {
                this.Session["scountry"] = "550";
                this.Session["companyid"] = "1561";
                this.Session["Perfilid"] = "0090";
            }
            catch (Exception ex) {
                message = "Ocurrio un Error en Dummy Login: " + 
                    ex.Message.ToString(); 
            }
        }
        #endregion
    }
}