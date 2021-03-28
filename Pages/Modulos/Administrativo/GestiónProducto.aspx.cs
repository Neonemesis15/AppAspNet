using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Telerik.Web.UI;
using Lucky.Business.Common.Application;
using Lucky.Business.Common.Maestros.Producto;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Maestros.Producto;

namespace SIGE.Pages.Modulos.Administrativo
{ 
    /// <summary>
    /// Permite al actor Administrador de SIGE realizar todos los procesos para la administracion de Gestión De Productos
    /// Developed by: 
    /// - Ing. Magaly Jiménez (MJ)
    /// Changes:
    /// - 08-11-2018 Pablo Salas Alvarez (PSA)  Refactoring Productos
    /// - 11-08-2011 Angel Ortiz (AO)           Se añade cliente por cada categoria
    /// - 11-07-2011 Angel Ortiz (AO)           Se añade Panel para la gestion de Sub Familias, se cambia vistas en Navegacion 
    ///                                         para edicion de datos en Presentaciones.
    ///                                         Se limpia codigo con lineas innecesarias y se protegen los atributos (variables globales) con private.
    /// - 13-08-2010 Magaly Jiménez (MJ)        Creación de la Clase
    /// </summary>
    public partial class GestiónProducto : System.Web.UI.Page
    {
        #region VARIABLES
        // Variable para guardar los mensajes de Error en la Pagina
        private String messages = "";

        private bool estado;

        private string repetido = "";
        private string repetido1 = "";
        private string repetido2 = "";
        private string planningADM;
        private string Cliente;
        private string scompany_id;
    
        #endregion 

        #region CONEXION A CAPA BUSINESS LOGIC
        private Brand oBrand = new Brand();
        private SubBrand oSubBrand = new SubBrand();
        private SubCategoria oSubCategoria = new SubCategoria();
        private AD_ProductosAncla oPAncla = new AD_ProductosAncla();
        private BProduct_Family oProductFamily = new BProduct_Family();
        private Product_Presentations oProdPresent = new Product_Presentations();
        private Productos oProductos = new Productos();
        #endregion 

        #region CONEXION A LA BASE DE DATOS
        private Conexion oConn = new Lucky.Data.Conexion();
        private Conexion oCoon = new Conexion();
        #endregion

        #region CONEXION A LOS WEB SERVICES
        private Product_Type oProductType = new Product_Type();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos owsadministrativo = 
            new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos obtenerid = 
            new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        #endregion

        /// <summary>
        /// Carga Inicial de la Página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try{
                this.planningADM = this.Session["AdmProd"].ToString().Trim(); // "SI"; 
            }catch{

            }

            if (!IsPostBack){
                try{
                   
           
                    IfCargaMCategoria.Attributes["src"] = "CargaMasivaGProductos.aspx";
             
                    if (this.planningADM == "SI")
                    {


                    }
                    else
                    {
                      
                    }


                       //llenarcombocliente();



               }catch (Exception ex){

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
        
        #region FUNCIONES


        /// <summary>
        /// Llena Compañias en el AspControl que pasan como parametro 'ddl', para la Gestión de Sub Familias
        /// </summary>
        /// <param name="ddl"></param>
        private void llenarcomboclienteencrearsubfam(DropDownList ddl)
        {
            messages = "";
            DataSet ds = null;
            try
            {
                ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            }
            catch (Exception ex)
            {
                messages = "Ocurrio un Error: " + ex.ToString();
            }

            // Verificar que no existan Errores
            if (messages.Equals(""))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //se llena cliente en Usuarios
                    ddl.DataSource = ds;
                    ddl.DataTextField = "Company_Name";
                    ddl.DataValueField = "Company_id";
                    ddl.DataBind();
                }
                else
                {
                    // Mostrar PopUp Mensaje Usuario
                    messages = "Error: No Existen Categorías Disponibles para el Cliente indicado, ¡por favor Verificar...!";
                }
            }
            else
            {
                // Mostrar PopUp Mensaje Usuario
            }

        }
        
        /// <summary>
        /// Mensaje de Alertas
        /// </summary>
        private void MensajeAlerta()
        {
            ModalPopupAlertas.Show();
            BtnAceptarAlert.Focus();
            //ScriptManager.RegisterStartupScript(
            //    this, this.GetType(), "myscript", "alert('Debe ingresar todos los parametros con (*)');", true);
        }
        
        #endregion


        #region Controls Enabled / Disabled

        /// <summary>
        /// Gestion las Opciones de Categoría para deshabilitarlos en caso encuentre un Error.
        /// </summary>
        private void CancelarCat()
        {
            //saveActivarbotonesCategoria();
            //desactivarControlesCategoria();
            //SavelimpiarControlesCategoria();
        }

      


        /// <summary>
        /// Deshabilitar Opciones de Crear y Guardar, para la Gestión de Categorias
        /// </summary>
        private void buscarActivarbotnesCategoria()
        {
            //BtnCrearProductType.Visible = false;
            //BtnSaveProductType.Visible = false;
            //BtnConsultaProductType.Visible = true;
            BtnCancelProductType.Visible = true;
            btnCCategoria.Visible = true;
        }




        private void crearActivarbotonesCategoria()
        {
            //BtnCrearProductType.Visible = false;
            //BtnSaveProductType.Visible = true;
            //BtnConsultaProductType.Visible = false;
            BtnCancelProductType.Visible = true;
        }

        private void activarControlesCategoria()
        {
            /*
            TxtCodProductType.Enabled = false;
            TxtNomProductType.Enabled = true;
            TxtgroupCategory.Enabled = true;
            cmb_categorias_cliente.Enabled = true;
            */
            Panel_CategProduct.Enabled = true;

        }
        #endregion

        #region Categoria


        protected void BtnCancelProductType_Click(object sender, EventArgs e)
        {
            //saveActivarbotonesCategoria();
            //desactivarControlesCategoria();
            //SavelimpiarControlesCategoria();
            //BtnCargaMasivaCate.Visible = true;
        }




        /// <summary>
        /// Registrar en base de datos Mobile
        /// </summary>
        /// <returns></returns>
        public EProduct_Type registrarCategoriasMobile(EProduct_Type oeProductType)
        {
            EProduct_Type oeProductTypeTMP =
                           oProductType.RegistrarProductcategoryTMP(
                           oeProductType.id_Product_Category,
                           oeProductType.Product_Category,
                           oeProductType.Product_Category_Status);
            return oeProductTypeTMP;
        }






      

        /// <summary>
        /// Remover los Espacios en Blanco de la cadenas de texto.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string trimText(String text) {
            return text.Trim();
        }

       



        /// <summary>
        /// Guarda en sessión las variables utilizadas en los filtros de Category Search
        /// </summary>
        private void saveSessionInCategorySearch(string productId, string productName, string companyId) {
            this.Session["scodProductType"] = productId;
            this.Session["sproductType"] = productName;
            this.Session["scompany_id"] = scompany_id;
        }

        /// <summary>
        /// Guarda en sessión el resultado de la busqueda aplicando los filtros de Category Search
        /// </summary>
        /// <param name="categoryDt"></param>
        private void saveSessionResultCategorySearch(DataTable categoryDt) {
            this.Session["tProductType"] = categoryDt;
        }

        /// <summary>
        /// Retornar un DataTable con las Categorías que cumplen con los filtros indicados.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productName"></param>
        /// <param name="companyId"></param>
        /// <returns>DataTable con las Categorias que cumplen con los filtros aplicados</returns>
        private DataTable getProductCategoryInCategorySearch(string productId, string productName, string companyId) {
            DataTable oeProductType = new DataTable();
            try
            {
                // Buscar las Categorias según los Filtros
                oeProductType = oProductType.SearchProductCategory(productId, productName, companyId);
            }
            catch (Exception ex)
            {
                messages = "Ocurrio un Error: " + ex.ToString();
            }
            return oeProductType;
        }

        /// <summary>
        /// Poblar el Gridview en base al resultado de la busqueda de Category Search
        /// </summary>
        /// <param name="categorySearchResultDt"></param>
        /// <returns></returns>
        private bool populateGridViewForCategoryResultSearch(DataTable categorySearchResultDt) {

            bool result = false;

            if (categorySearchResultDt != null)
            {
                if (categorySearchResultDt.Rows.Count > 0)
                {
                    gridbuscarCategoria(categorySearchResultDt);
                    result = true;
                }

                else
                {
                    CancelarCat();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La consulta realizada no devolvió ninguna respuesta";
                    MensajeAlerta();
                    //IbtnProductType.Show();
                }
            }

            return result;
        }
        

        /// <summary>
        /// Guardar en sessión el resultado de la busqueda de Category Search para exportarlo a Excel.
        /// </summary>
        private void saveSessionToExportExcelByCategorySearchResult() {

            // Setea la Session para Exportar a Excel.
            this.Session["Exportar_Excel"] = "Exportar_Categorias";

            // Crear un DataTable 'dtnameCaategoria'
            DataTable dtnameCaategoria = new DataTable();
            // Agregar Columnas al DataTable 'dtnameCaategoria'
            dtnameCaategoria.Columns.Add("Código", typeof(String));
            dtnameCaategoria.Columns.Add("Categoria", typeof(String));
            dtnameCaategoria.Columns.Add("Grupo Categoria", typeof(String));
            //dtnameCaategoria.Columns.Add("Cliente", typeof(String));
            dtnameCaategoria.Columns.Add("Cliente", typeof(String));
            dtnameCaategoria.Columns.Add("Estado", typeof(String));


            for (int i = 0; i <= GVConsultaCategoria.Rows.Count - 1; i++)
            {
                DataRow dr = dtnameCaategoria.NewRow();
                dr["Código"] = ((Label)GVConsultaCategoria.Rows[i].Cells[0].FindControl("LblCodProductType")).Text;
                dr["Categoria"] = ((Label)GVConsultaCategoria.Rows[i].Cells[0].FindControl("LblNomProductType")).Text;
                dr["Grupo Categoria"] = ((Label)GVConsultaCategoria.Rows[i].Cells[0].FindControl("Lblgroupcategory")).Text;
                //dr["Cliente"] = ((Label)GVConsultaCategoria.Rows[i].Cells[0].FindControl("LblCodClie")).Text;
                dr["Cliente"] = ((Label)GVConsultaCategoria.Rows[i].Cells[0].FindControl("LblClienteID")).Text;
                dr["Estado"] = ((CheckBox)GVConsultaCategoria.Rows[i].Cells[0].FindControl("CheckECategoria")).Checked;
                dtnameCaategoria.Rows.Add(dr);
            }

            this.Session["CExporCategoria"] = dtnameCaategoria;
        
        }

     
        
        /// <summary>
        /// Llenar la información del AspControl GridView 'GVConsultaCategoria' y Hacer Visible el Panel 'CosultaGVCategoria'
        /// </summary>
        /// <param name="oeProductType"></param>
        private void gridbuscarCategoria(DataTable oeProductType)
        {
            GVConsultaCategoria.EditIndex = -1;
            GVConsultaCategoria.DataSource = null;
            GVConsultaCategoria.DataSource = oeProductType;
            GVConsultaCategoria.DataBind();
            MopopConsulCate.Show();
        }




        protected void GVConsultaCategoria_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            GVConsultaCategoria.PageIndex = e.NewPageIndex;
            GVConsultaCategoria.DataSource = (DataTable)this.Session["tProductType"];
            GVConsultaCategoria.DataBind();
            MopopConsulCate.Show();
        }

        protected void GVConsultaCategoria_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            btnCCategoria.Visible = false;
            
            MopopConsulCate.Show();
            
            GVConsultaCategoria.EditIndex = e.NewEditIndex;
            
            string Codigo, Categoria, grupo, cliente;
            bool estado;
            
            Codigo = ((Label)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("LblCodProductType")).Text;
            Categoria = ((Label)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("LblNomProductType")).Text;
            grupo = ((Label)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("Lblgroupcategory")).Text;
            cliente = ((Label)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("LblClienteID")).Text;
            estado = ((CheckBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("CheckECategoria")).Checked;
            
            GVConsultaCategoria.DataSource = (DataTable)this.Session["tProductType"];
            GVConsultaCategoria.DataBind();
            
            if (cliente.Equals(""))
                cliente = "<Seleccione...>";
            
            ((Label)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("LblCodProductType")).Text = Codigo;
            ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("TxtNomProductType")).Text = Categoria;
            ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[2].FindControl("TxtgroupCategory")).Text = grupo;
            
            LlenacomboConsultaCliente();

            ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[3].FindControl("cmbCliente_Edit")).Items.FindByText(cliente).Selected = true;
            ((CheckBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[4].FindControl("CheckECategoria")).Checked = estado;
            this.Session["rept"] = ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("TxtNomProductType")).Text;
            this.Session["rept1"] = ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[2].FindControl("TxtgroupCategory")).Text;
            this.Session["rept2"] = ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[3].FindControl("cmbCliente_Edit")).Text;
        }

        protected void GVConsultaCategoria_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            GVConsultaCategoria.EditIndex = -1;
            gridbuscarCategoria((DataTable)this.Session["tProductType"]);
            btnCCategoria.Visible = true;
            MopopConsulCate.Show();
        }

        private void LlenacomboConsultaCliente()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 65);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("cmbCliente_Edit")).DataSource = ds;
                ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("cmbCliente_Edit")).DataTextField = "Company_Name";
                ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("cmbCliente_Edit")).DataValueField = "Company_id";
                ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("cmbCliente_Edit")).DataBind();
            }
        }

        /// <summary>
        /// Preguntar si esta permitido deshabilitar Categoria
        /// </summary>
        /// <returns></returns>
        private bool permitirDeshabilitarCategoria() {
            bool continuar = false;
            if (((CheckBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[3].FindControl("CheckECategoria")).Checked != false)
            {
                continuar = true;
            }
            else
            {
                continuar = false;
                DAplicacion oddeshabProductType = new DAplicacion();
                DataTable dt = oddeshabProductType.PermitirDeshabilitar(
                    ConfigurationManager.AppSettings["ProductCategoryProduct_Tipo"],
                    ((Label)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("LblCodProductType")).Text);
                if (dt != null)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No se puede deshabilitar este registro ya que existe relación en el maestro de Perfil, por favor verifique";
                    MensajeAlerta();
                    //return;
                }
            }
            return continuar;
        }


        /// <summary>
        /// Validar Campos en Editar Categorias
        /// </summary>
        /// <param name="txtNomProductyType"></param>
        /// <param name="sCompanyId"></param>
        /// <param name="sProductType"></param>
        /// <returns></returns>
        private bool validarCamposEditarCategoria(string txtNomProductyType, string sCompanyId, string sProductType)
        {
            bool continuar = true;
            if (txtNomProductyType == "" || sCompanyId == "0")
            {

                LblFaltantes.Text = "Debe ingresar los campos: ";
                if (sProductType == "")
                {
                    LblFaltantes.Text += ("Nombre de producto" + " . ");
                }
                if (sCompanyId == "0")
                {
                    LblFaltantes.Text += ("Cliente" + " . ");
                }
                Alertas.CssClass = "MensajesError";
                CancelarCat();
                MensajeAlerta();
                continuar = false;
            }
            return continuar;
        }

        /// <summary>
        /// Consultar si existen duplicados al Editar Categorias
        /// </summary>
        /// <returns></returns>
        private DataTable consultarDuplicadosEditarCategoria(string sProductType, string sCompanyId, string txtGroupCategory)
        {
            DAplicacion odconsulProductType = new DAplicacion();
            DataTable dtconsulta =
                odconsulProductType.ConsultaDuplicados(
                ConfigurationManager.AppSettings["ProductCategory"],
                sProductType,
                sCompanyId,
                txtGroupCategory);
            return dtconsulta;
        }

        /// <summary>
        /// Actualizar Categoria en Base de Datos
        /// </summary>
        private EProduct_Type actualizarCategoria(string lblCodProductTypeInsert, string txtNomProductTypeInsert, string txtGroupCategoryInsert,
            string cmbClienteEditInsert, bool estadoInsert)
        {
            EProduct_Type oeaProductType =
                                oProductType.Actualizar_ProductCategory(
                                lblCodProductTypeInsert,
                                txtNomProductTypeInsert,
                                txtGroupCategoryInsert,
                                cmbClienteEditInsert,
                                estadoInsert,
                                Convert.ToString(this.Session["sUser"]), DateTime.Now);

            return oeaProductType;
        }

        /// <summary>
        /// Actualizar Categoria en Base de Datos Mobile
        /// </summary>
        private EProduct_Type actualizarCategoriaMobile(string lblCodProductTypeInsert, string txtNomProductTypeInsert, bool estadoInsert)
        {
            EProduct_Type oeaProductTypetmp = oProductType.Actualizar_ProductCategorytmp(
                                lblCodProductTypeInsert,
                                txtNomProductTypeInsert,
                                estadoInsert);

            return oeaProductTypetmp;
        }

        /// <summary>
        /// mensaje indicando que el registro se encuentra duplicado
        /// </summary>
        private void messageRegistroDuplicado() {
            this.Session["tProductType"] = ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("TxtNomProductType")).Text;
            Alertas.CssClass = "MensajesError";
            LblFaltantes.Text = "La categoria de Producto : " + this.Session["tProductType"] + " Ya Existe";
            //cancelarCat();
            MensajeAlerta();
        }

        /// <summary>
        /// mensaje de Actualización exitosa
        /// </summary>
        private void messageSucessfullActualizacionCategoria() {

            btnCCategoria.Visible = true;
            MopopConsulCate.Show();
            Alertas.CssClass = "MensajesCorrecto";
            LblFaltantes.Text = "La categoría de Producto : " + this.Session["sProductType"] + " fue actualizada con éxito";
            MensajeAlerta();
        }

        protected void GVConsultaCategoria_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            LblFaltantes.Text = "";
            string txtNomProductyType = ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("TxtNomProductType")).Text.Trim();
            string txtGroupCategory = ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[2].FindControl("TxtgroupCategory")).Text.Trim();
            string sProductType = ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("TxtNomProductType")).Text;
            string sCompanyId = ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[3].FindControl("cmbCliente_Edit")).Text.Trim();
            
            this.Session["sProductType"] = sProductType;
            this.Session["scompany_id"] = sCompanyId;

            if ( permitirDeshabilitarCategoria() ){

                if ( validarCamposEditarCategoria(txtNomProductyType, sCompanyId, sProductType) ) {

                    try
                    {
                        DataTable dtconsulta = consultarDuplicadosEditarCategoria(sProductType, sCompanyId, txtGroupCategory);

                        if (dtconsulta == null)
                        {
                            string lblCodProductTypeInsert = ((Label)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("LblCodProductType")).Text;
                            string txtNomProductTypeInsert = ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("TxtNomProductType")).Text;
                            string txtGroupCategoryInsert = ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("TxtgroupCategory")).Text;
                            string cmbClienteEditInsert = ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("cmbCliente_Edit")).Text;
                            bool estadoInsert = estado;

                            EProduct_Type oeaProductType =
                                actualizarCategoria(
                                lblCodProductTypeInsert,
                                txtNomProductTypeInsert,
                                txtGroupCategoryInsert,
                                cmbClienteEditInsert,
                                estadoInsert);

                            EProduct_Type oeaProductTypetmp =
                                actualizarCategoriaMobile(
                                lblCodProductTypeInsert,
                                txtNomProductTypeInsert,
                                estadoInsert);

                            GVConsultaCategoria.EditIndex = -1;

                            DataTable oeProductType = oProductType.SearchProductCategory(
                                this.Session["scodProductType"].ToString().Trim(),
                                this.Session["sproductType"].ToString().Trim(),
                                this.Session["scompany_id"].ToString().Trim());

                            this.Session["tProductType"] = oeProductType;

                            if (oeProductType != null)
                            {
                                if (oeProductType.Rows.Count > 0)
                                {
                                    gridbuscarCategoria(oeProductType);
                                }
                            }

                            //IbtnProductType.Hide();

                            messageSucessfullActualizacionCategoria();

                            //saveActivarbotonesCategoria();
                        }
                        else
                        {
                            messageRegistroDuplicado();
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        messageErrorActualizarCategoria(ex);
                    }
                }
               
            }

            
        }

        /// <summary>
        /// Gestion de errores cuando la actualizacion de categoria falla
        /// </summary>
        /// <param name="ex"></param>
        public void messageErrorActualizarCategoria(Exception ex)
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

        #endregion

    }
}