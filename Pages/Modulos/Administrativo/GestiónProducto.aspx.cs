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
        private int icodBrand;
        private string sBrand = "";
        private string repetido = "";
        private string repetido1 = "";
        private string repetido2 = "";
        private string repetido3 = "";
        private string sSubBrand = "";
        private string scodSubBrand = "";
        private string sNode = "";
        private string scodProductType = "";
        private string sproductType = "";
        private string sSubCategoria = "";
        private string sCodCategoria = "";
        private string sPresent = "";
        private int iCompanyId;
        private int iid_Brand;
        private string SKU_Producto, CategoriaProduc;
        private string sNomProd = "";
        private string sbcliente = "";
        private string soficina = "";
        private int sMarca;
        private string sCategoriaFamily;
        private string sNombreFamilia = "";
        private string sCategoria = "";
        private string planningADM;
        private string Cliente;
        private string scompany_id;
        private DataTable dt = null;
        private long iid_Product;
        private long iid_pancla;
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
                   
                    IfCargaMasivaGProductos.Attributes["src"] = "CargaMasivaGProductos.aspx";
                    IfCargaMCategoria.Attributes["src"] = "CargaMasivaGProductos.aspx";
                    IfCMSubcategoria.Attributes["src"] = "CargaMasivaGProductos.aspx";
                    IframeCargarProduct.Attributes["src"] = "CargaMasivaGProductos.aspx";
                    iframepancla.Attributes["src"] = "CargaMasivaGProductos.aspx";
                    IframeCMSubmarca.Attributes["src"] = "CargaMasivaGProductos.aspx";
                    iframeCMFamilia.Attributes["src"] = "CargaMasivaGProductos.aspx";
                    
                    if (this.planningADM == "SI"){

                        LLenacomboCategoriaBuscarMarcaporplanning();
                        llenar_comboBCategopresentPlanning();

                        comboclienteBuscarproductoporplanning();
                        comboclienteenBuscarPanclaporplanning();
                        comboclienteBuscarenFamilyporplanning();

                        //LLenacomboCategoriaBuscarMarcaporplanning();
                        LLenacomboCategoriaBuscarSubMarcaporplanning();
                        comboclienteenPanclaporplanning();

                        LlenacomboCategProducto(cmbBCategoriaProduct);
                        LlenacomboMarcaProduct(cmbBBrand);

                        //Guardar en sesiones los maestros de: Categoria, SubCategoria, Familia, Marca, Tipo, Formato, 
                        BL_Categoria oBL_Categoria = new BL_Categoria();
                        BL_SubCategoria oBL_SubCategoria = new BL_SubCategoria();
                        BL_Familia oBL_Familia = new BL_Familia();
                        BL_Marca oBL_Marca = new BL_Marca();
                        BL_Formato oBL_Formato = new BL_Formato();
                        BL_Tipo oBL_Tipo = new BL_Tipo();

                        List<MA_Categoria> oListMA_Categoria = new List<MA_Categoria>();
                        List<MA_SubCategoria> oListMA_SubCategoria = new List<MA_SubCategoria>();
                        List<MA_Familia> oListMA_Familia = new List<MA_Familia>();
                        List<MA_Marca> oListMA_Marca = new List<MA_Marca>();
                        List<MA_Formato> oListMA_Formato = new List<MA_Formato>();
                        List<MA_Tipo> oListMA_Tipo = new List<MA_Tipo>();

                        oListMA_Categoria = oBL_Categoria.Get_Categorias();
                        oListMA_SubCategoria = oBL_SubCategoria.Get_SubCategoriasByCodCategoria("%");
                        oListMA_Familia = oBL_Familia.Get_FamiliasByCodSubCategoria("%");
                        oListMA_Marca = oBL_Marca.Get_Marcas();
                        oListMA_Formato = oBL_Formato.Get_Formatos();
                        oListMA_Tipo = oBL_Tipo.Get_Tipos();

                        this.Session["CCategoria"] = oListMA_Categoria;
                        this.Session["CSubCategoria"] = oListMA_SubCategoria;
                        this.Session["CFamilia"] = oListMA_Familia;
                        this.Session["CMarca"] = oListMA_Marca;
                        this.Session["CFormato"] = oListMA_Formato;
                        this.Session["CTipo"] = oListMA_Tipo;

                    }
                    else
                    {                        
                        llenar_comboBCategopresent();
                        llenaComboCategoriaBuscarMarca();
                        //comboBproducCompany();
                        LlenacomboBuscarClienteProductAncla();
                        LlenacomboBuscarClienteProductFamily();                        
                        LlenacomboBuscarCategSubMarca();
                        LlenacomboClienteProductAncla();
                        llenarcomboclienteencrearsubfam(ddl_bsf_cliente);
                    }
                    LlenacomboCategoConsulta();
                    llenarcombocliente();
                    LlenaCategory_CompanyCatego();
                    cargar_cbxl_cxu_cliente();
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
        
        #region FUNCIONES
        private void SavelimpiarControlesMarca()
        {
            TxtCodBrand.Text = "";
            TxtNomBrand.Text = "";
            txtAlias.Text = "";
            //cmbCategoryMarca.Text= "0";
            //cmbClienteMarca.Text = "0";
            cmbCategoryMarca.Items.Clear();
            cmbClienteMarca.Items.Clear();
            TxtBCodBrand.Text="";
            cmbBuscarCategoryM.SelectedIndex = 0;
            //cmbBuscarCategoryM.Items.Clear();
            TxtBNomBrand.Text = "";
        }
        private void activarControlesMarca()
        {
            TxtCodBrand.Enabled = false;
            TxtNomBrand.Enabled = true;
            cmbCategoryMarca.Enabled = true;
            cmbClienteMarca.Enabled = true;
            Panel_Marcas.Enabled = true;
            Panel_Submarcas.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = false;
            TabProducAncla.Enabled = false;
            PanelSubFamilia.Enabled = false;
        }
        private void desactivarControlesMarca()
        {
            TxtCodBrand.Enabled = false;
            TxtNomBrand.Enabled = false;
            cmbCategoryMarca.Enabled = false;
            cmbClienteMarca.Enabled = false;
            Panel_Marcas.Enabled = true;
            Panel_Submarcas.Enabled = true;
            Panel_CategProduct.Enabled = true;
            Panel_ProductFamily.Enabled = true;
            Panel_SubCategoria.Enabled = true;
            Panel_Presentación.Enabled = true;
            PanelProducto.Enabled = true;
            TabProducAncla.Enabled = true;
            PanelSubFamilia.Enabled = true;
        }
        private void crearActivarbotonesMarca()
        {            
            BtnCrearBrand.Visible = false;
            BtnSaveBrand.Visible = true;
            BtnConsultaBrand.Visible = false;            
            BtnCancelBrand.Visible = true;
        }
        private void saveActivarbotonesMarca()
        {
            BtnCrearBrand.Visible = true;
            BtnSaveBrand.Visible = false;
            BtnConsultaBrand.Visible = true;            
            BtnCancelBrand.Visible = true;
        }
        private void EditarActivarbotonesMarca()
        {            
            BtnCrearBrand.Visible = false;
            BtnSaveBrand.Visible = false;
            BtnConsultaBrand.Visible = true;
            BtnCancelBrand.Visible = true;
        }
        private void EditarActivarControlesMarca()
        {
            TxtCodBrand.Enabled = false;
            TxtNomBrand.Enabled = true;
            cmbCategoryMarca.Enabled = true;
            cmbClienteMarca.Enabled = false;
            Panel_Marcas.Enabled = true;
            Panel_Submarcas.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = false;
            TabProducAncla.Enabled = false;
        }
        private void BuscarActivarbotnesMarca()
        {
            BtnCrearBrand.Visible = false;
            BtnSaveBrand.Visible = false;
            BtnConsultaBrand.Visible = true;            
            BtnCancelBrand.Visible = true;
        }
        private void comboclienteenMarca()
        {
            cmbClienteMarca.DataSource = null;
            DataSet dtclien = null;
            dtclien = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en Usuarios
            cmbClienteMarca.DataSource = dtclien;
            cmbClienteMarca.DataTextField = "Company_Name";
            cmbClienteMarca.DataValueField = "Company_id";
            cmbClienteMarca.DataBind();
            dtclien = null;
        }
        private void comboclienteenMarcaporplanning()
        {
            cmbClienteMarca.DataSource = null;
            DataTable dtclipla = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            dtclipla = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_CONSULTACLIENTE_PROPIOYCOPETIDORES", Cliente);
            //se llena cliente en Usuarios
            cmbClienteMarca.DataSource = dtclipla;
            cmbClienteMarca.DataTextField = "Company_Name";
            cmbClienteMarca.DataValueField = "Company_id";
            cmbClienteMarca.DataBind();
            dtclipla = null;
        }
        private void LlenacomboCategMarca()
        {
            cmbCategoryMarca.DataSource = null;
            DataTable categ = null;
            if (this.planningADM == "SI")
            {
                Cliente = Convert.ToString(this.Session["companyid"]);
                categ = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOBUSCARCATEGORIAPLANNING", Cliente);
            }
            else 
            {
                categ = oConn.ejecutarDataTable("UP_WEB_LLENACOMBOS", 13);
            }
            cmbCategoryMarca.DataSource = categ;
            cmbCategoryMarca.DataTextField = "Product_Category";
            cmbCategoryMarca.DataValueField = "id_ProductCategory";
            cmbCategoryMarca.DataBind();
            categ = null;
        }
        private void llenaComboCategoriaBuscarMarca()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_CATEGORIA_WITH_PRODUCTS");
            //Se llena categoria en consulta del maestro de Marca
            cmbBuscarCategoryM.DataSource = dt;
            cmbBuscarCategoryM.DataTextField = "Product_Category";
            cmbBuscarCategoryM.DataValueField = "id_ProductCategory";
            cmbBuscarCategoryM.DataBind();
            cmbBuscarCategoryM.Items.Insert(0,new ListItem("<Seleccione...>","0"));
            dt = null;
        }
        private void LLenacomboCategoriaconsultarMarcaplanning()
        {
            DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOBUSCARCATEGORIAPLANNING", Cliente);
            //se llena cliente en Usuarios
            cmbBuscarCategoryM.DataSource = ds;
            cmbBuscarCategoryM.DataTextField = "Product_Category";
            cmbBuscarCategoryM.DataValueField = "id_ProductCategory";
            cmbBuscarCategoryM.DataBind();
        }  
        /// <summary>
        /// Llena el AspControl DropDownList 'cmbBuscarCategoryM' con las Categorias Disponibles por idCliente
        /// </summary>
        private void LLenacomboCategoriaBuscarMarcaporplanning()
        {
            DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            try{
                // Obtener las Categorias por idCliente
                ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOBUSCARCATEGORIAPLANNING", Cliente);
            }catch (Exception ex) {
                messages = "Ocurrio un Error: " + ex.ToString();
            }

            // Verificar que no existan Errores
            if (messages.Equals("")){

                // Verificar que exista al menos un Elemento para poder llenar el AspControl DropDowList 'cmbBuscarCategoryM' con las Categorias correspondientes
                
                if (ds.Tables[0].Rows.Count > 0){
                    //se llena cliente en Usuarios
                    cmbBuscarCategoryM.DataSource = ds;
                    cmbBuscarCategoryM.DataTextField = "Product_Category";
                    cmbBuscarCategoryM.DataValueField = "id_ProductCategory";
                    cmbBuscarCategoryM.DataBind();
                }
                else{

                    // Mostrar PopUp Mensaje Usuario
                    messages = "Error: No Existen Categorías Disponibles para el Cliente indicado, ¡por favor Verificar...!";
                }
            }else {

                // Mostrar PopUp Mensaje Usuario
            }
            
            #region Data Dummy
            /*
            ListItem listItem0 = new ListItem("Categoria00", "0");
            ListItem listItem1 = new ListItem("Categoria01", "1");
            ListItem listItem2 = new ListItem("Categoria02", "2");
            ListItem listItem3 = new ListItem("Categoria03", "3");

            cmbBuscarCategoryM.Items.Add(listItem0);
            cmbBuscarCategoryM.Items.Add(listItem1);
            cmbBuscarCategoryM.Items.Add(listItem2);
            cmbBuscarCategoryM.Items.Add(listItem3);
            */
            #endregion

        }  
        private void InicializarPaneles()
        {
            CargaMasiva.Style.Value = "Display:none;";
            CosultaGVMarca.Style.Value = "Display:none;";
        }
        private void LLenaConsultaMarca()
        {
            try
            {
                DataTable dt = oBrand.SearchBrandPlanning(icodBrand, cmbBuscarCategoryM.Text, sBrand, Convert.ToInt32(Cliente));
                GVConsultaMarca.DataSource = dt;
                GVConsultaMarca.DataBind();
                dt = null;
            }
            catch (Exception ex)
            {
            }
        }
        private void LlenacomboConsultaClienteMarca()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena mallas PDVC


            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).DataSource = ds;
                ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).DataTextField = "Company_Name";
                ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).DataValueField = "Company_id";
                ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).DataBind();               
            }
            else
            {                
            }
        }
        private void LlenacomboConsultaClienteMarcaPlanning()
        {
            DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_CONSULTACLIENTE_PROPIOYCOPETIDORES", Cliente);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).DataSource = ds;
                ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).DataTextField = "Company_Name";
                ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).DataValueField = "Company_id";
                ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).DataBind();                
            }
            else
            {               
            }
        }
        private void LlenacomboConsultaCategoriaMarca()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 13);
            //se llena mallas PDVC

            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[2].FindControl("cmbCategoryMarca")).DataSource = ds;
                ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[2].FindControl("cmbCategoryMarca")).DataTextField = "Product_Category";
                ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[2].FindControl("cmbCategoryMarca")).DataValueField = "id_ProductCategory";
                ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[2].FindControl("cmbCategoryMarca")).DataBind();                
            }
            else
            {               
            }
        }
        private void SavelimpiarControlesSubMarca()
        {
            TxtCodSubMarca.Text = "";
            TxtNomSubMarca.Text = "";
            cmbCategoriaSubmarca.Text = "0";
            CmbSelBrand.Text = "0";
            RbtnStatusSubBrand.Items[0].Selected = true;
            RbtnStatusSubBrand.Items[1].Selected = false;

            cmbCategorySubmarca.Text = "0";
            TxtBNomSubBrand.Text = "";
            CmbBSelBrand.Text = "0";
        }
        private void activarControlesSubMarca()
        {
            TxtCodSubMarca.Enabled = false;
            TxtNomSubMarca.Enabled = true;
            CmbSelBrand.Enabled = true;
            cmbCategoriaSubmarca.Enabled = true;
            RbtnStatusSubBrand.Enabled = false;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = true;
            Panel_CategProduct.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            PanelSubFamilia.Enabled = false;
            TabProducAncla.Enabled = false;
        }
        private void desactivarControlesSubMarca()
        {
            TxtCodSubMarca.Enabled = false;
            TxtNomSubMarca.Enabled = false;
            CmbSelBrand.Enabled = false;
            cmbCategoriaSubmarca.Enabled = false;
            RbtnStatusSubBrand.Enabled = false;
            Panel_Marcas.Enabled = true;
            Panel_Submarcas.Enabled = true;
            Panel_CategProduct.Enabled = true;
            Panel_SubCategoria.Enabled = true;
            Panel_Presentación.Enabled = true;
            PanelProducto.Enabled = true;
            Panel_ProductFamily.Enabled = true;
            PanelSubFamilia.Enabled = true;
            TabProducAncla.Enabled = true;
        }
        private void crearActivarbotonesSubMarca()
        {

            BtnCrearSubBrand.Visible = false;
            BtnsaveSubBrand.Visible = true;
            BtnSearchSubBrand.Visible = false;           
            BtnCMSubmarca.Visible = false;
            BtnCancelSubBrand.Visible = true;
        }
        private void saveActivarbotonesSubMarca()
        {
            BtnCrearSubBrand.Visible = true;
            BtnsaveSubBrand.Visible = false;
            BtnSearchSubBrand.Visible = true;            
            BtnCMSubmarca.Visible = true;
            BtnCancelSubBrand.Visible = true;
        }
        private void EditarActivarbotonesSubMarca()
        {
            BtnCrearSubBrand.Visible = false;
            BtnsaveSubBrand.Visible = false;
            BtnSearchSubBrand.Visible = true;
            BtnCancelSubBrand.Visible = true;
        }
        private void EditarActivarControlesSubMarca()
        {
            TxtCodSubMarca.Enabled = false;
            TxtNomSubMarca.Enabled = true;
            CmbSelBrand.Enabled = true;
            cmbCategoriaSubmarca.Enabled = true;
            RbtnStatusSubBrand.Enabled = true;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = true;
            Panel_CategProduct.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = false;
            PanelSubFamilia.Enabled = false;
            TabProducAncla.Enabled = false;
        }
        private void BuscarActivarbotnesSubMarca()
        {
            BtnCrearSubBrand.Visible = false;
            BtnsaveSubBrand.Visible = false;
            BtnSearchSubBrand.Visible = true;
            BtnCMSubmarca.Visible = true;
            BtnCancelSubBrand.Visible = true;
        }
        private void LlenacomboCategSubMarca()
        {
            DataTable dt = null;
            cmbCategoriaSubmarca.DataSource = null;
            dt = oConn.ejecutarDataTable("UP_WEB_LLENACOMBOS", 13);
            //se llena categorias en tipo de producto
            cmbCategoriaSubmarca.DataSource = dt;
            cmbCategoriaSubmarca.DataTextField = "Product_Category";
            cmbCategoriaSubmarca.DataValueField = "id_ProductCategory";
            cmbCategoriaSubmarca.DataBind();
            dt = null;
        }
        private void LlenacomboGVCategSubMarca(int i)
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 13);
        
            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[1].FindControl("cmbCateSubMarca")).DataSource = ds;
                ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[1].FindControl("cmbCateSubMarca")).DataTextField = "Product_Category";
                ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[1].FindControl("cmbCateSubMarca")).DataValueField = "id_ProductCategory";
                ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[1].FindControl("cmbCateSubMarca")).DataBind();
            }
            else
            {
            }
            ds = null;
        }
       private void LlenacomboBuscarCategSubMarca()
        {    
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOBUSCARCATEGORIA");
            //Se llena categoria en consulta del maestro de Marca
            cmbCategorySubmarca.DataSource = dt;
            cmbCategorySubmarca.DataTextField = "Product_Category";
            cmbCategorySubmarca.DataValueField = "id_ProductCategory";
            cmbCategorySubmarca.DataBind();
            dt = null;
        }
       private void llenamarcaconsultaSubMarca()
       {
           DataTable dt = new DataTable();
           CmbBSelBrand.DataSource = null;
           dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOMARCASEGUNCATEGORIA", cmbCategorySubmarca.SelectedValue);
           //se llena Combo de marca en buscar de maestro familia de producto
           CmbBSelBrand.DataSource = dt;
           CmbBSelBrand.DataTextField = "Name_Brand";
           CmbBSelBrand.DataValueField = "id_Brand";
           CmbBSelBrand.DataBind();
           CmbBSelBrand.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
           dt = null;
       }
       private void LlenacomboMarcaensubMarca()
        {
            DataTable dt = new DataTable();
            CmbSelBrand.DataSource = null;
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOMARCASEGUNCATEGORIA",cmbCategoriaSubmarca.SelectedValue);
            //se llena Combo de marca en buscar de maestro familia de producto
            CmbSelBrand.DataSource = dt;
            CmbSelBrand.DataTextField = "Name_Brand";
            CmbSelBrand.DataValueField = "id_Brand";
            CmbSelBrand.DataBind();
            CmbSelBrand.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            dt = null;
        }
       private void LlenacomboGVMarcaensubMarca(int i)
       {
           DataTable dt = new DataTable();
           dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOMARCASEGUNCATEGORIA", ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[1].FindControl("cmbCateSubMarca")).SelectedValue);     

           if (dt.Rows.Count > 0)
           {
               ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[2].FindControl("cmbMarcaSubMarca")).DataSource = dt;
               ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[2].FindControl("cmbMarcaSubMarca")).DataTextField = "Name_Brand";
               ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[2].FindControl("cmbMarcaSubMarca")).DataValueField = "id_Brand";
               ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[2].FindControl("cmbMarcaSubMarca")).DataBind();
               ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[2].FindControl("cmbMarcaSubMarca")).Items.Insert(0, new ListItem("<Seleccione...>", "0"));
           }
           else
           {

           }
           dt = null;
       }
       private void combocomboMarcaenSubarcaporplanning()
       {
           DataTable dt = null;
           Cliente = Convert.ToString(this.Session["companyid"]);
           dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOMARCAENSUBMARCAPLANNING", cmbCategoriaSubmarca.SelectedValue, Cliente);
           //se llena cliente en Usuarios
           CmbSelBrand.DataSource = dt;
           CmbSelBrand.DataTextField = "Name_Brand";
           CmbSelBrand.DataValueField = "id_Brand";
           CmbSelBrand.DataBind();
           dt = null;
       }
       private void LLenacomboCategoriaBuscarSubMarcaporplanning()
       {
           /*DataSet ds = null;
           Cliente = Convert.ToString(this.Session["companyid"]);
           ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOBUSCARCATEGORIAPLANNING", Cliente);
           //se llena cliente en Usuarios
           cmbCategorySubmarca.DataSource = ds;
           cmbCategorySubmarca.DataTextField = "Product_Category";
           cmbCategorySubmarca.DataValueField = "id_ProductCategory";
           cmbCategorySubmarca.DataBind();*/

           ListItem listItem0 = new ListItem("Categoria00", "0");
           ListItem listItem1 = new ListItem("Categoria01", "1");
           ListItem listItem2 = new ListItem("Categoria02", "2");
           ListItem listItem3 = new ListItem("Categoria03", "3");

           cmbCategorySubmarca.Items.Add(listItem0);
           cmbCategorySubmarca.Items.Add(listItem1);
           cmbCategorySubmarca.Items.Add(listItem2);
           cmbCategorySubmarca.Items.Add(listItem3);
       }
       private void LLenacomboCategoriaSubMarcasplanning()
       {
           DataTable dt = null;
           cmbCategoriaSubmarca.DataSource = null;
           Cliente = Convert.ToString(this.Session["companyid"]);
           dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOBUSCARCATEGORIAPLANNING", Cliente);
           //se llena cliente en Usuarios
           cmbCategoriaSubmarca.DataSource = dt;
           cmbCategoriaSubmarca.DataTextField = "Product_Category";
           cmbCategoriaSubmarca.DataValueField = "id_ProductCategory";
           cmbCategoriaSubmarca.DataBind();
           dt = null;
       }
        private void SavelimpiarControlesCategoria()
        {
            TxtCodProductType.Text = "";
            TxtNomProductType.Text = "";
            TxtgroupCategory.Text = "";
            cmb_categorias_cliente.Text = "0";
            TxtBCodTypeProduct.Text = "";
            TxtBNomTypeProduct.Text = "";
        }
        private void activarControlesCategoria()
        {
            TxtCodProductType.Enabled = false;
            TxtNomProductType.Enabled = true;
            TxtgroupCategory.Enabled = true;
            cmb_categorias_cliente.Enabled = true;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_CategProduct.Enabled = true;
            Panel_ProductFamily.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = false;
            PanelSubFamilia.Enabled = false;
            TabProducAncla.Enabled = false;
        }
        private void desactivarControlesCategoria()
        {
            TxtCodProductType.Enabled = false;
            TxtNomProductType.Enabled = false;
            TxtgroupCategory.Enabled = false;
            cmb_categorias_cliente.Enabled = false;
            Panel_Marcas.Enabled = true;
            Panel_Submarcas.Enabled = true;
            Panel_CategProduct.Enabled = true;
            Panel_ProductFamily.Enabled = true;
            Panel_SubCategoria.Enabled = true;
            Panel_Presentación.Enabled = true;
            PanelProducto.Enabled = true;
            PanelSubFamilia.Enabled = true;
            TabProducAncla.Enabled = true;
        }
        private void crearActivarbotonesCategoria()
        {            
            BtnCrearProductType.Visible = false;
            BtnSaveProductType.Visible = true;
            BtnConsultaProductType.Visible = false;  
            BtnCancelProductType.Visible = true;
        }
        private void saveActivarbotonesCategoria()
        {
            BtnCrearProductType.Visible = true;
            BtnSaveProductType.Visible = false;
            BtnConsultaProductType.Visible = true;   
            BtnCancelProductType.Visible = true;
        }
        private void EditarActivarbotonesCategoria()
        {
            BtnCrearProductType.Visible = false;
            BtnSaveProductType.Visible = false;
            BtnConsultaProductType.Visible = true;   
            BtnCancelProductType.Visible = true;
        }
        private void EditarActivarControlesCategoria()
        {
            TxtCodProductType.Enabled = false;
            TxtNomProductType.Enabled = true;
            TxtgroupCategory.Enabled = true;
            cmb_categorias_cliente.Enabled = true;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_CategProduct.Enabled = true;
            Panel_SubCategoria.Enabled = false;
            Panel_Presentación.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            PanelProducto.Enabled = false;
            PanelSubFamilia.Enabled = false;
            TabProducAncla.Enabled = false;
        }
        private void BuscarActivarbotnesCategoria()
        {
            BtnCrearProductType.Visible = false;
            BtnSaveProductType.Visible = false;
            BtnConsultaProductType.Visible = true;
            BtnCancelProductType.Visible = true;
            btnCCategoria.Visible = true;
        }
        private void SavelimpiarControlesSubCategoria()
        {
            TxtCodSubCategoria.Text = "";
            TxtNomSubCategory.Text = "";
            cmbCateSubCategoria.Text = "0";
            RBtSubCategoy.Items[0].Selected = true;
            RBtSubCategoy.Items[1].Selected = false;
            TxtBNomSubCategory.Text = "";
            cmbBCategoriaSC.Text = "0";
        }
        private void activarControlesSubCategoria()
        {
            TxtCodSubCategoria.Enabled = false;
            TxtNomSubCategory.Enabled = true;
            cmbCateSubCategoria.Enabled = true;
            RBtSubCategoy.Enabled = false;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            Panel_SubCategoria.Enabled = true;
            Panel_Presentación.Enabled = false;
            PanelSubFamilia.Enabled = false;
            PanelProducto.Enabled = false;
            TabProducAncla.Enabled = false;            
        }
        private void desactivarControlesSubCategoria()
        {
            TxtCodSubCategoria.Enabled = false;
            TxtNomSubCategory.Enabled = false;
            cmbCateSubCategoria.Enabled = false;
            RBtSubCategoy.Enabled = false;
            Panel_Marcas.Enabled = true;
            Panel_Submarcas.Enabled = true;
            Panel_ProductFamily.Enabled = true;
            Panel_CategProduct.Enabled = true;
            Panel_SubCategoria.Enabled = true;
            Panel_Presentación.Enabled = true;
            PanelSubFamilia.Enabled = true;
            PanelProducto.Enabled = true;
            TabProducAncla.Enabled = true;
        }
        private void crearActivarbotonesSubCategoria()
        {
            BtnCrearSubCategory.Visible = false;
            BtnGuardarSubCategory.Visible = true;
            BtnConsultarSubCategory.Visible = false;
            BtnCargaMasuSubcategoria.Visible = false;
            BtnCancelarSubCategory.Visible = true;
         }
        private void saveActivarbotonesSubCategoria()
        {
            
            BtnCrearSubCategory.Visible = true;
            BtnGuardarSubCategory.Visible = false;
            BtnConsultarSubCategory.Visible = true;
            BtnCargaMasuSubcategoria.Visible = true;
            BtnCancelarSubCategory.Visible = true;
        }
        private void EditarActivarbotonesSubCategoria()
        {

            BtnCrearSubCategory.Visible = false;
            BtnGuardarSubCategory.Visible = false;
            BtnConsultarSubCategory.Visible = true;
            BtnCancelarSubCategory.Visible = true;
        }
        private void EditarActivarControlesSubCategoria()
        {
            TxtCodSubCategoria.Enabled = false;
            TxtNomSubCategory.Enabled = true;
            cmbCateSubCategoria.Enabled = true;
            RBtSubCategoy.Enabled = true;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_SubCategoria.Enabled = true;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = false;
            PanelSubFamilia.Enabled = false;
            TabProducAncla.Enabled = false;
        }
        private void BuscarActivarbotnesSubCategoria()
        {
            
            BtnCrearSubCategory.Visible = false;
            BtnGuardarSubCategory.Visible = false;
            BtnConsultarSubCategory.Visible = true;
            BtnCancelarSubCategory.Visible = true;
        }
        private void comboCategSubCategoria()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 13);
            //se llena categorias en tipo de producto
            cmbCateSubCategoria.DataSource = ds;
            cmbCateSubCategoria.DataTextField = "Product_Category";
            cmbCateSubCategoria.DataValueField = "id_ProductCategory";
            cmbCateSubCategoria.DataBind();
            ds = null;
        }
        private void LlenacomboCategoConsulta()
        {
            /*DataSet ds1 = new DataSet();
            ds1 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 59);
            //se llena Categorias producto en BuscarSubCategorias
            cmbBCategoriaSC.DataSource = ds1;
            cmbBCategoriaSC.DataTextField = "Product_Category";
            cmbBCategoriaSC.DataValueField = "id_ProductCategory";
            cmbBCategoriaSC.DataBind();
            ds1 = null;*/

            ListItem listItem0 = new ListItem("Categoria00", "0");
            ListItem listItem1 = new ListItem("Categoria01", "1");
            ListItem listItem2 = new ListItem("Categoria02", "2");
            ListItem listItem3 = new ListItem("Categoria03", "3");

            cmbBCategoriaSC.Items.Add(listItem0);
            cmbBCategoriaSC.Items.Add(listItem1);
            cmbBCategoriaSC.Items.Add(listItem2);
            cmbBCategoriaSC.Items.Add(listItem3);

        }
        private void LlenacomboConsultaCategoriaSubcategoria()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 13);
            //se llena mallas PDVC
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).DataSource = ds;
                ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).DataTextField = "Product_Category";
                ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).DataValueField = "id_ProductCategory";
                ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).DataBind();
            }
            else
            {
            }
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
        private void SavelimpiarControlesPresent()
        {
            TxtCodPresen.Text = "";
            cmbCategoryPresent.Text = "0";
            cmbSubCategoryPresent.Text= "0";
            cmbMarcaPresent.Text = "0";
            TxtNomPresen.Text = "";
            TxtConteNeto.Text = "";
            TexEmpPresent.Text = "";
            TexUnidadEpresent.Text = "";
            CmbUnidNeto.Text = "0";
            RbtnPresenStatus.Items[0].Selected = true;
            RbtnPresenStatus.Items[1].Selected = false;
            cmbBCategoriaPresent.Text = "0";
            cmbBMarcaPresent.Text = "0";
            TxtBNomPresen.Text = "";                
        }
        private void activarControlesPresent()
        {
            TxtCodPresen.Enabled = false;
            cmbCategoryPresent.Enabled = true;
            cmbSubCategoryPresent.Enabled = true;
            cmbMarcaPresent.Enabled = true;
            TxtConteNeto.Enabled = true;
            CmbUnidNeto.Enabled = true;
            TexEmpPresent.Enabled = true;
            TexUnidadEpresent.Enabled = true;
            TxtNomPresen.Enabled = true;
            RbtnPresenStatus.Enabled = false;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_Presentación.Enabled = true;
            PanelProducto.Enabled = false;
            TabProducAncla.Enabled = false;           
        }
        private void desactivarControlesPresent()
        {
            TxtCodPresen.Enabled = false;
            cmbCategoryPresent.Enabled = false;
            cmbSubCategoryPresent.Enabled = false;
            cmbMarcaPresent.Enabled = false;
            TxtConteNeto.Enabled = false;
            CmbUnidNeto.Enabled = false;
            TexEmpPresent.Enabled = false;
            TexUnidadEpresent.Enabled = false;
            TxtNomPresen.Enabled = false;
            RbtnPresenStatus.Enabled = false;
            Panel_Marcas.Enabled = true;
            Panel_Submarcas.Enabled = true;
            Panel_ProductFamily.Enabled = true;
            Panel_CategProduct.Enabled = true;
            Panel_SubCategoria.Enabled = true;
            Panel_Presentación.Enabled = true;
            PanelProducto.Enabled = true;
            TabProducAncla.Enabled = true;
        }
        private void crearActivarbotonesPresent()
        {            
            BtnCrearPresen.Visible = false;
            BtnSavePresen.Visible = true;
            BtnConsultaPresen.Visible = false;
            BtnCancelPresent.Visible = true;
        }
        private void saveActivarbotonesPresent()
        {
            BtnCrearPresen.Visible = true;
            BtnSavePresen.Visible = false;
            BtnConsultaPresen.Visible = true;
            BtnCancelPresent.Visible = true;
        }
        private void EditarActivarbotonesPresent()
        {
            BtnCrearPresen.Visible = false;
            BtnSavePresen.Visible = false;
            BtnConsultaPresen.Visible = true;
            BtnCancelPresent.Visible = true;
        }
        private void EditarActivarControlesPresent()
        {
            TxtCodPresen.Enabled = false;
            cmbCategoryPresent.Enabled = true;
            cmbSubCategoryPresent.Enabled = true;
            cmbMarcaPresent.Enabled = true;
            TxtConteNeto.Enabled = true;
            CmbUnidNeto.Enabled = true;
            TexEmpPresent.Enabled = true;
            TexUnidadEpresent.Enabled = true;
            TxtNomPresen.Enabled = true;
            RbtnPresenStatus.Enabled = true;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_Presentación.Enabled = true;
            PanelProducto.Enabled = false;
            TabProducAncla.Enabled = false;
        }
        private void BuscarActivarbotnesPresent()
        {
            BtnCrearPresen.Visible = false;
            BtnSavePresen.Visible = false;
            BtnConsultaPresen.Visible = true;
            BtnCancelPresent.Visible = true;
        }
        private void LlenacomboCategPresent()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 13);
            //se llena categorias en tipo de producto
            cmbCategoryPresent.DataSource = ds;
            cmbCategoryPresent.DataTextField = "Product_Category";
            cmbCategoryPresent.DataValueField = "id_ProductCategory";
            cmbCategoryPresent.DataBind();
            ds = null;
        }
        private void LlenaSubporCategoPresent()
        {           
            DataTable dt1 = new DataTable();
            dt1 = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOSUBCATEGORIAPRESENT", cmbCategoryPresent.SelectedValue);
            //se llena Combo de servicio segun usuario seleccionado
            if (dt1.Rows.Count > 0)
            {
                cmbSubCategoryPresent.DataSource = dt1;
                cmbSubCategoryPresent.DataTextField = "Name_Subcategory";
                cmbSubCategoryPresent.DataValueField = "id_Subcategory";
                cmbSubCategoryPresent.DataBind();
            }
            // se modifica sp para q muestre el item seleccione desde sql para evitar error en consulta. Ing. Mauricio Ortiz 29/09/2010
            //cmbSubCategoryPresent.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            dt1 = null;            
        }
        private void LlenacomboMarcaPresent()
        {
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOMARCASEGUNCATEGORIA", cmbCategoryPresent.SelectedValue);
            //se llena Combo de marca en buscar de maestro familia de producto
            cmbMarcaPresent.DataSource = dt;
            cmbMarcaPresent.DataTextField = "Name_Brand";
            cmbMarcaPresent.DataValueField = "id_Brand";
            cmbMarcaPresent.DataBind();
            cmbMarcaPresent.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            dt = null; 
        }
        private void combocomboMarcaenPresentaplanning()
        {
            DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOMARCAENSUBMARCAPLANNING", cmbCategoryPresent.SelectedValue, Cliente);
            //se llena cliente en Usuarios
            cmbMarcaPresent.DataSource = ds;
            cmbMarcaPresent.DataTextField = "Name_Brand";
            cmbMarcaPresent.DataValueField = "id_Brand";
            cmbMarcaPresent.DataBind();
            cmbMarcaPresent.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void llenar_comboBCategopresent()
        {
            DataSet ds5 = new DataSet();
            ds5 = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOCATEGORIACONSULTARPRESENTACIÓN");

            //se llena categira en busqueda de presentación
            cmbBCategoriaPresent.DataSource = ds5;
            cmbBCategoriaPresent.DataTextField = "Product_Category";
            cmbBCategoriaPresent.DataValueField = "id_ProductCategory";
            cmbBCategoriaPresent.DataBind();
            ds5 = null;
        }
        /// <summary>
        /// 
        /// </summary>
        private void llenar_comboBCategopresentPlanning()
        {            
            /*
            DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOBUSCARCATEGORIAPLANNING", Cliente);
            //se llena cliente en Usuarios
            cmbBCategoriaPresent.DataSource = ds;
            cmbBCategoriaPresent.DataTextField = "Product_Category";
            cmbBCategoriaPresent.DataValueField = "id_ProductCategory";
            cmbBCategoriaPresent.DataBind();
            */


            ListItem listItem0 = new ListItem("Categoria00", "0");
            ListItem listItem1 = new ListItem("Categoria01", "1");
            ListItem listItem2 = new ListItem("Categoria02", "2");
            ListItem listItem3 = new ListItem("Categoria03", "3");

            cmbBCategoriaPresent.Items.Add(listItem0);
            cmbBCategoriaPresent.Items.Add(listItem1);
            cmbBCategoriaPresent.Items.Add(listItem2);
            cmbBCategoriaPresent.Items.Add(listItem3);

        }
        private void llenar_comboBCategoSubCategoPlanning()
        {
            DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOBUSCARCATEGORIAPLANNING", Cliente);
            //se llena cliente en Usuarios
            cmbCateSubCategoria.DataSource = ds;
            cmbCateSubCategoria.DataTextField = "Product_Category";
            cmbCateSubCategoria.DataValueField = "id_ProductCategory";
            cmbCateSubCategoria.DataBind();
        }
        private void llenar_comboBMarcapresent()
        {
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOMARCASEGUNCATEGORIA",cmbBCategoriaPresent.SelectedValue);
            //se llena Combo de marca en buscar de maestro familia de producto
            cmbBMarcaPresent.DataSource = dt;
            cmbBMarcaPresent.DataTextField = "Name_Brand";
            cmbBMarcaPresent.DataValueField = "id_Brand";
            cmbBMarcaPresent.DataBind();
            cmbBMarcaPresent.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            dt = null; 
           
        }
        private void combocomboConsultaMarcaenPresentaplanning()
        {
            DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOMARCASEGUNCATEGORIAPLANNING", cmbBCategoriaPresent.SelectedValue, Cliente);
            //se llena cliente en Usuarios
            cmbBMarcaPresent.DataSource = ds;
            cmbBMarcaPresent.DataTextField = "Name_Brand";
            cmbBMarcaPresent.DataValueField = "id_Brand";
            cmbBMarcaPresent.DataBind();
            cmbBMarcaPresent.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;

        }        
        private void Llenacom_UnidadMedida()
        {
            DataSet ds1 = new DataSet();
            ds1 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 46);
            //se llena unidades de medida en maestro presentacion de productos
            CmbUnidNeto.DataSource = ds1;
            CmbUnidNeto.DataTextField = "UnitOfMeasure_Name";
            CmbUnidNeto.DataValueField = "id_UnitOfMeasure";
            CmbUnidNeto.DataBind();
            ds1 = null;
        }
        private void LLenacomboCategoriaPresentacionplanning()
        {
            DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOBUSCARCATEGORIAPLANNING", Cliente);
            //se llena cliente en Usuarios
            cmbCategoryPresent.DataSource = ds;
            cmbCategoryPresent.DataTextField = "Product_Category";
            cmbCategoryPresent.DataValueField = "id_ProductCategory";
            cmbCategoryPresent.DataBind();

        }  
        private void SavelimpiarControlesFamily()
        {
            TxtCodFamily.Text="";
            cmbClienteFamily.Text = "0";
            CmbMarcaFamily.Text="0";
            CmbSubmarcaFamily.Text="0";
            TxtNomFamily.Text="";
            TxtDescripcionFamily.Text = "";
            txtPesoFamily.Text = "";
            cmbCategoryFamily.Text = "0";
            CmbSubCategoryFamily.Text = "0";
            cmbCategoryBFamily.Text = "0";
            CmbBMarcaFamily.Text="0";
            TxtBNomFamily.Text = "";
        }
        private void activarControlesFamily()
        {

            TxtCodFamily.Enabled = false;
            cmbClienteFamily.Enabled = true;
            CmbMarcaFamily.Enabled = true;
            CmbSubmarcaFamily.Enabled = true;
            cmbCategoryFamily.Enabled = true;
            CmbSubCategoryFamily.Enabled = true;
            TxtNomFamily.Enabled = true;
            TxtDescripcionFamily.Enabled = true;
            txtPesoFamily.Enabled = true;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_ProductFamily.Enabled = true;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = false;
            TabProducAncla.Enabled = false;

        }
        private void desactivarControlesFamily()
        {
            TxtCodFamily.Enabled = false;
            cmbClienteFamily.Enabled = false;
            CmbMarcaFamily.Enabled = false;
            CmbSubmarcaFamily.Enabled = false;
            cmbCategoryFamily.Enabled = false;
            CmbSubCategoryFamily.Enabled = false;
            TxtNomFamily.Enabled = false;
            TxtDescripcionFamily.Enabled = false;
            txtPesoFamily.Enabled = false;
            Panel_Marcas.Enabled = true;
            Panel_Submarcas.Enabled = true;
            Panel_CategProduct.Enabled = true;
            Panel_SubCategoria.Enabled = true;
            Panel_Presentación.Enabled = true;
            Panel_ProductFamily.Enabled = true;
            PanelProducto.Enabled = true;
            TabProducAncla.Enabled = true;
        }
        private void crearActivarbotonesFamily()
        {
            BtnCrearFamily.Visible = false;
            BtnsaveFamily.Visible = true;
            BtnSearchFamily.Visible = false;
            BtnCarMasivaFamilia.Visible = false;
            BtnCancelFamily.Visible = true;
       
        }
        private void saveActivarbotonesFamily()
        {
            BtnCrearFamily.Visible =true;
            BtnsaveFamily.Visible = false;
            BtnSearchFamily.Visible = true;
            BtnCancelFamily.Visible = true;
            BtnCarMasivaFamilia.Visible = true;  
        }
        private void EditarActivarbotonesFamily()
        {
            BtnCrearFamily.Visible = false;
            BtnsaveFamily.Visible = false;
            BtnSearchFamily.Visible = true;
            BtnCancelFamily.Visible = true;
         
        }
        private void EditarActivarControlesFamily()
        {
            TxtCodFamily.Enabled = false;
            cmbClienteFamily.Enabled = true;
            CmbMarcaFamily.Enabled = true;
            CmbSubmarcaFamily.Enabled = true;
            cmbCategoryFamily.Enabled = true;
            CmbSubCategoryFamily.Enabled = true;
            TxtNomFamily.Enabled = true;
            TxtDescripcionFamily.Enabled = true;
            txtPesoFamily.Enabled = true;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_ProductFamily.Enabled = true;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = false;
            TabProducAncla.Enabled = false;
        }
        private void BuscarActivarbotnesFamily()
        {

            BtnCrearFamily.Visible = false;
            BtnsaveFamily.Visible = false;
            BtnSearchFamily.Visible = true;
            BtnCancelFamily.Visible = true;      

        }
        private void LlenacomboClienteProductFamily()
        {

            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en Usuarios
            cmbClienteFamily.DataSource = ds;
            cmbClienteFamily.DataTextField = "Company_Name";
            cmbClienteFamily.DataValueField = "Company_id";
            cmbClienteFamily.DataBind();


        }
        private void comboclienteenFamilyporplanning()
        {
            DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_CONSULTACLIENTE_PROPIOYCOPETIDORES", Cliente);
            //se llena cliente en Usuarios
            cmbClienteFamily.DataSource = ds;
            cmbClienteFamily.DataTextField = "Company_Name";
            cmbClienteFamily.DataValueField = "Company_id";
            cmbClienteFamily.DataBind();

        }
        private void LlenacomboGVClienteProductFamily(int i)
        {

            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en Usuarios
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).DataSource = ds;
                ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).DataTextField = "Company_Name";
                ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).DataValueField = "Company_id";
                ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).DataBind();
            }
            else
            {
            }
        }
        private void comboclienteGVenFamilyporplanning(int i)
        {
            DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_CONSULTACLIENTE_PROPIOYCOPETIDORES", Cliente);
            //se llena cliente en Usuarios
            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).DataSource = ds;
                ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).DataTextField = "Company_Name";
                ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).DataValueField = "Company_id";
                ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).DataBind();
            }
            else
            {
            }
        }
        private void LlenaMarcaFamily()
        {
           DataTable ds = new DataTable();
           dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOMARCABUSCARFAMILY", Convert.ToInt32(cmbClienteFamily.SelectedValue), cmbCategoryFamily.SelectedValue);
            //se llena Combo de marca en buscar de maestro familia de producto
            CmbMarcaFamily.DataSource = dt;
            CmbMarcaFamily.DataTextField = "Name_Brand";
            CmbMarcaFamily.DataValueField = "id_Brand";
            CmbMarcaFamily.DataBind();
            CmbMarcaFamily.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void LlenacomboBuscarClienteProductFamily()
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en Usuarios
            CmbBClientFamily.DataSource = ds;
            CmbBClientFamily.DataTextField = "Company_Name";
            CmbBClientFamily.DataValueField = "Company_id";
            CmbBClientFamily.DataBind();         
        }
        private void comboclienteBuscarenFamilyporplanning()
        {
            /*DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_CONSULTACLIENTE_PROPIOYCOPETIDORES", Cliente);
            //se llena cliente en Usuarios
            CmbBClientFamily.DataSource = ds;
            CmbBClientFamily.DataTextField = "Company_Name";
            CmbBClientFamily.DataValueField = "Company_id";
            CmbBClientFamily.DataBind();*/

            ListItem listItem0 = new ListItem("Compania00", "0");
            ListItem listItem1 = new ListItem("Compania01", "1");
            ListItem listItem2 = new ListItem("Compania02", "2");
            ListItem listItem3 = new ListItem("Compania03", "3");

            CmbBClientFamily.Items.Add(listItem0);
            CmbBClientFamily.Items.Add(listItem1);
            CmbBClientFamily.Items.Add(listItem2);
            CmbBClientFamily.Items.Add(listItem3);

        }  
        private void comboSubmarcaFamily()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEB_LLENACOMBOSUBMARCAPRODFAMILY", CmbMarcaFamily.SelectedValue);
            //Se llena submarcas de productos en productos
            CmbSubmarcaFamily.DataSource = dt;
            CmbSubmarcaFamily.DataTextField = "Name_SubBrand";
            CmbSubmarcaFamily.DataValueField = "id_SubBrand";
            CmbSubmarcaFamily.DataBind();
            dt = null;
        }
        private void LlenaMarcaBuscarFamily()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOMARCAENBPRODUC", Convert.ToInt32(CmbBClientFamily.SelectedValue), cmbCategoryBFamily.SelectedValue);
            //se llena Combo de marca en buscar de maestro familia de producto
            CmbBMarcaFamily.DataSource = ds.Tables[1];
            CmbBMarcaFamily.DataTextField = "Name_Brand";
            CmbBMarcaFamily.DataValueField = "id_Brand";
            CmbBMarcaFamily.DataBind();
            CmbBMarcaFamily.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void LlenaCategoriaBuscarFamily()
        {
            DataTable dt = new DataTable();

            if (this.planningADM == "SI")
                dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOBUSCARCATEGORIAPLANNING", Convert.ToString(this.Session["companyid"]));
            else
                dt = oConn.ejecutarDataTable("UP_WEB_LLENACOMBOS", 13);

            //se llena Combo de marca en buscar de maestro familia de producto
            cmbCategoryBFamily.DataSource = dt;
            cmbCategoryBFamily.DataTextField = "Product_Category";
            cmbCategoryBFamily.DataValueField = "id_ProductCategory";
            cmbCategoryBFamily.DataBind();
            dt = null;
        }
        private void LlenacomboCategFamily(int cliente)
        {
            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEXPLORA_CLIEN_V2_LLENACOMBOS", cliente,"","",2);
            //se llena categorias en tipo de producto
            cmbCategoryFamily.DataSource = dt;
            cmbCategoryFamily.DataTextField = "Name_Catego";
            cmbCategoryFamily.DataValueField = "cod_catego";
            cmbCategoryFamily.DataBind();
            cmbCategoryFamily.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            dt = null;
        }
        private void LlenaSubporCategoFamily()
        {

            DataTable dt1 = new DataTable();
            dt1 = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOSUBCATEGORIAPRESENT", cmbCategoryFamily.SelectedValue);
            //se llena Combo subcategoria segun categoria
            CmbSubCategoryFamily.DataSource = dt1;
            CmbSubCategoryFamily.DataTextField = "Name_Subcategory";
            CmbSubCategoryFamily.DataValueField = "id_Subcategory";
            CmbSubCategoryFamily.DataBind();
            // se modifica sp para q muestre el item seleccione desde sql para evitar error en consulta. Ing. Mauricio Ortiz 29/09/2010
            //CmbSubCategoriaProduct.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            if (CmbSubCategoryFamily.Items.Count == 1)
           
            {
                CmbSubCategoryFamily.Items.Clear();
                CmbSubCategoryFamily.Items.Insert(0, new ListItem("<No Aplica>", "n"));
                CmbSubCategoryFamily.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
        

            dt1 = null;


        }
        private void llenacomboMarcaaxCategoriaenBusFamily()
        {
            DataSet ds_catego = new DataSet();

            ds_catego = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOMARCAENBPRODUC", CmbBClientFamily.SelectedValue, cmbCategoryBFamily.SelectedValue);
            //se llena marca por Categoria cliente en buscar Productos
            CmbBMarcaFamily.DataSource = ds_catego.Tables[1];
            CmbBMarcaFamily.DataTextField = "Name_Brand";
            CmbBMarcaFamily.DataValueField = "id_Brand";
            CmbBMarcaFamily.DataBind();
            CmbBMarcaFamily.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            
            ds_catego = null;
        }
        private void LlenacomboGVCategFamily(int i)
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 13);
            //se llena categorias en tipo de producto
        
            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GvConsFamilia.Rows[i].Cells[2].FindControl("cmbcatefamilia")).DataSource = ds;
                ((DropDownList)GvConsFamilia.Rows[i].Cells[2].FindControl("cmbcatefamilia")).DataTextField = "Product_Category";
                ((DropDownList)GvConsFamilia.Rows[i].Cells[2].FindControl("cmbcatefamilia")).DataValueField = "id_ProductCategory";
                ((DropDownList)GvConsFamilia.Rows[i].Cells[2].FindControl("cmbcatefamilia")).DataBind();
            }
            else
            {
            }
            ds = null;
        }
        private void LlenaGVSubporCategoFamily(int i)
        {

            DataTable dt1 = new DataTable();
            dt1 = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOSUBCATEGORIAPRESENT", ((DropDownList)GvConsFamilia.Rows[i].Cells[2].FindControl("cmbcatefamilia")).SelectedValue);
            //se llena Combo subcategoria segun categoria
     
            // se modifica sp para q muestre el item seleccione desde sql para evitar error en consulta. Ing. Mauricio Ortiz 29/09/2010
            //CmbSubCategoriaProduct.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            if (dt1.Rows.Count > 0)
            {
                ((DropDownList)GvConsFamilia.Rows[i].Cells[3].FindControl("cmbsubcatefamilia")).DataSource = dt1;
                ((DropDownList)GvConsFamilia.Rows[i].Cells[3].FindControl("cmbsubcatefamilia")).DataTextField = "Name_Subcategory";
                ((DropDownList)GvConsFamilia.Rows[i].Cells[3].FindControl("cmbsubcatefamilia")).DataValueField = "id_Subcategory";
                ((DropDownList)GvConsFamilia.Rows[i].Cells[3].FindControl("cmbsubcatefamilia")).DataBind();


            }
            else
            {

            }
            dt1 = null;
        }
        private void LlenaGVMarcaFamily(int i)
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOMARCAGVFAMILY", Convert.ToInt32(((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).SelectedValue), ((DropDownList)GvConsFamilia.Rows[i].Cells[2].FindControl("cmbcatefamilia")).SelectedValue);
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GvConsFamilia.Rows[i].Cells[4].FindControl("cmbmarcafamilia")).DataSource = ds.Tables[0];
                ((DropDownList)GvConsFamilia.Rows[i].Cells[4].FindControl("cmbmarcafamilia")).DataTextField = "Name_Brand";
                ((DropDownList)GvConsFamilia.Rows[i].Cells[4].FindControl("cmbmarcafamilia")).DataValueField = "id_Brand";
                ((DropDownList)GvConsFamilia.Rows[i].Cells[4].FindControl("cmbmarcafamilia")).DataBind();
            }
            else
            {

            }

            ds = null;


        }
        private void comboGVSubmarcaFamily(int i)
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOSUBMARCAPRODFAMILY", ((DropDownList)GvConsFamilia.Rows[i].Cells[4].FindControl("cmbmarcafamilia")).SelectedValue);
            //Se llena submarcas de productos en productos
     
            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GvConsFamilia.Rows[i].Cells[5].FindControl("cmbSubmarcafamilia")).DataSource = ds;
                ((DropDownList)GvConsFamilia.Rows[i].Cells[5].FindControl("cmbSubmarcafamilia")).DataTextField = "Name_SubBrand";
                ((DropDownList)GvConsFamilia.Rows[i].Cells[5].FindControl("cmbSubmarcafamilia")).DataValueField = "id_SubBrand";
                ((DropDownList)GvConsFamilia.Rows[i].Cells[5].FindControl("cmbSubmarcafamilia")).DataBind();            
            }
            else
            {
            }
            ds = null;
        }
        private void consultaUltimoIdfamilia()
        {
            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_CONSULTA_ID_FAMILYTMP");
            //llena el ultimo id de marca para insertar en bd intemedia  7/04/2011 Magaly Jiménez
            TxtCodFamily.Text = dt.Rows[0]["id_ProductFamily"].ToString().Trim();
            dt = null;
        }
        private void SavelimpiarControlesProducto()
        {
            cmbTipoCateg.Text="0";
            CmbSubCategoriaProduct.Text="0";
            cmbFabricante.Text="0";
            cmbPFamily.Text = "0";
            cmbTipoProducto.Text = "0";
            cmbFormatoProducto.Text = "0";
            //cmbSelSubBrand.Text="0";
            //cmbPres.Text="0";
            //TxtFactor.Text="";
            //TxtPeso.Text="";
            //cmbUMedida.Text = "0";
            TxtCodProducto.Text="";
            TxtNomProducto.Text="";
            txtAlias.Text = "";
            RadNumericTxtInfPrecioOferta.Text = "";
            RadNumericTxtInfPrecioVenta.Text = "";
            //TxtInfStock.Text = "";
            CheckBoxInfStock.Checked = false;
            TxtInfPromocion.Text = "";
            //TxtCompan.Text = "";
            //TxtPrecioPDV.Text="";
            //TxtPrecioReventa.Text="";
            //TxtCaracteristicas.Text="";
            //TxtBeneficios.Text = "";
            RBtnListStatusProducto.Items[0].Selected = true;
            RBtnListStatusProducto.Items[1].Selected = false;
            //cbmbcompañia.Text="0";
            cmbBBrand.Text="0";
            //TxtSKUProducto.Text = "";
        }
        private void activarControlesProducto()
        {
            #region Clasificación
            cmbTipoCateg.Enabled = true;
            CmbSubCategoriaProduct.Enabled = true;
            cmbPFamily.Enabled = true;
            cmbFabricante.Enabled = true;   //Marca
            #endregion

            #region Especificacion
            cmbTipoProducto.Enabled = true;
            cmbFormatoProducto.Enabled = true;
            #endregion

            #region Información Basica
            TxtCodProducto.Enabled = true;
            TxtNomProducto.Enabled = true;
            txtAlias.Enabled = true;
            RadNumericTxtInfPrecioVenta.Enabled = true;
            RadNumericTxtInfPrecioOferta.Enabled = true;
            //TxtInfStock.Enabled = true;
            CheckBoxInfStock.Enabled = true;
            TxtInfPromocion.Enabled = true;
            #endregion

            #region Paneles ON/OFF
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = true;
            #endregion

            #region RadioButton Status
            RBtnListStatusProducto.Enabled = false;
            #endregion

            #region Tabs
            TabProducAncla.Enabled = false;
            #endregion

            //ddl_psubfamily.Enabled = true;
            ////cmbSelSubBrand.Enabled = true;
            ////cmbPres.Enabled = true;
            ////TxtFactor.Enabled = false;
            //TxtPeso.Enabled = true;
            //TxtCompan.Enabled = true;
            //TxtPrecioPDV.Enabled = true;
            //cmbUMedida.Enabled = true;
            //TxtPrecioReventa.Enabled = true;
            //TxtCaracteristicas.Enabled = true;
            //TxtBeneficios.Enabled = true; 
            
        }
        private void desactivarControlesProducto()
        {
            cmbTipoCateg.Enabled = false;
            CmbSubCategoriaProduct.Enabled = false;
            cmbFabricante.Enabled = false;
            //cmbSelSubBrand.Enabled = false;
            //cmbPres.Enabled = false;
            ////TxtFactor.Enabled = false;
            cmbPFamily.Enabled = false;
            cmbTipoProducto.Enabled = false;
            cmbFormatoProducto.Enabled = false;
            //TxtPeso.Enabled = false;
            TxtCodProducto.Enabled = false;
            TxtNomProducto.Enabled = false;
            //TxtInfStock.Enabled = false;
            CheckBoxInfStock.Enabled = false;
            TxtInfPromocion.Enabled = false;
            //TxtCompan.Enabled = false;
            //cmbUMedida.Enabled = false;
            //TxtPrecioPDV.Enabled = false;
            //TxtPrecioReventa.Enabled = false;
            //TxtCaracteristicas.Enabled = false;
            //TxtBeneficios.Enabled = false;
            RBtnListStatusProducto.Enabled = false;
            Panel_Marcas.Enabled = true;
            Panel_ProductFamily.Enabled = true;
            Panel_Submarcas.Enabled = true;
            Panel_CategProduct.Enabled = true;
            Panel_SubCategoria.Enabled = true;
            Panel_Presentación.Enabled = true;
            PanelProducto.Enabled = true;
            TabProducAncla.Enabled = true;
            //ddl_psubfamily.Enabled = false;
            txtAlias.Enabled = false;
            RadNumericTxtInfPrecioOferta.Enabled = false;
            RadNumericTxtInfPrecioVenta.Enabled = false;
        }
        private void crearActivarbotonesProducto()
        {
            btnCrearProducto.Visible = false;
            BtnSaveProd.Visible = true;
            btnConsultarProducto.Visible = false;
            btnCancelarProducto.Visible = true;
            btncMasivaProducto.Visible = false;
        }
        private void saveActivarbotonesProducto()
        {
            btnCrearProducto.Visible = true;
            BtnSaveProd.Visible = false;
            btnConsultarProducto.Visible = true;
            btnCancelarProducto.Visible = true;
            btncMasivaProducto.Visible = true;
        }
        private void EditarActivarbotonesProducto()
        {

            btnCrearProducto.Visible = false;
            BtnSaveProd.Visible = false;
            btnConsultarProducto.Visible = true;
            btnCancelarProducto.Visible = true;
            btncMasivaProducto.Visible = false;
        }
        private void EditarActivarControlesProducto()
        {
            cmbTipoCateg.Enabled = true;
            CmbSubCategoriaProduct.Enabled = true;
            cmbFabricante.Enabled = true;
            //cmbSelSubBrand.Enabled = true;
            cmbPFamily.Enabled = true;
            //cmbPres.Enabled = true;
            //TxtFactor.Enabled = false;
            //TxtPeso.Enabled = true;
            //cmbUMedida.Enabled = true;
            TxtCodProducto.Enabled = true;
            TxtNomProducto.Enabled = false;
            //TxtCompan.Enabled = true;
            //TxtPrecioPDV.Enabled = true;
            //TxtPrecioReventa.Enabled = true;
            //TxtCaracteristicas.Enabled = true;
            //TxtBeneficios.Enabled = true;
            RBtnListStatusProducto.Enabled = true;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = true;
            TabProducAncla.Enabled = false;
        }
        private void BuscarActivarbotnesProducto()
        {

            btnCrearProducto.Visible = false;
            BtnSaveProd.Visible = false;
            btnConsultarProducto.Visible = true;
            btnCancelarProducto.Visible = true;
            btncMasivaProducto.Visible = false;

        }
        private void LlenacomboTipoProducto(DropDownList oDropDownList)
        {
            /*
             * Ver. 1.0     -   PSalas  -   Se obtiene las Tipos utilizando el Framework
             */

            ////////////////////////////////////////////////////////////////////
            // Ver. 1.0     -   PSalas  -   11 Nov. 2016  cmbTipoProducto
            ////////////////////////////////////////////////////////////////////
            try
            {
                BL_Tipo oBL_Tipo = new BL_Tipo();
                List<MA_Tipo> oListTipo = new List<MA_Tipo>();
                oListTipo = oBL_Tipo.Get_Tipos();

                oDropDownList.Items.Clear();
                foreach (MA_Tipo oMA_Tipo in oListTipo)
                {
                    ListItem listItem = new ListItem(oMA_Tipo.nombre, oMA_Tipo.codigo);
                    oDropDownList.Items.Add(listItem);
                }
                oDropDownList.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        private void LlenacomboFormatoProducto(DropDownList oDropDownList)
        {
            /*
             * Ver. 1.0     -   PSalas  -   Se obtiene las Formatos utilizando el Framework
             */

            ////////////////////////////////////////////////////////////////////
            // Ver. 1.0     -   PSalas  -   11 Nov. 2016 cmbFormatoProducto
            ////////////////////////////////////////////////////////////////////
            try
            {
                BL_Formato oBL_Formato = new BL_Formato();
                List<MA_Formato> oListFormato = new List<MA_Formato>();
                oListFormato = oBL_Formato.Get_Formatos();

                oDropDownList.Items.Clear();
                foreach (MA_Formato oMA_Formato in oListFormato)
                {
                    ListItem listItem = new ListItem(oMA_Formato.nombre, oMA_Formato.codigo);
                    oDropDownList.Items.Add(listItem);
                }
                oDropDownList.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        private void LlenacomboCategProducto(DropDownList oDropDownList)
        {
            /*
             * Ver. 1.0     -   PSalas  -   Se obtiene las categorias utilizando el Framework
             * Ver. 0.1     -   Anomino -   Obtiene las categorias usando el Procedimiento Almacenado (PA) UP_WEB_LLENACOMBOS
             */
            ////////////////////////////////////////////////////////////////////
            // Ver. 1.0     -   PSalas  -   22 Oct. 2016  
            ////////////////////////////////////////////////////////////////////
            try
            {
                BL_Categoria oBL_Categoria = new BL_Categoria();
                List<MA_Categoria> oListCategoria = new List<MA_Categoria>();
                oListCategoria = oBL_Categoria.Get_Categorias();

                oDropDownList.Items.Clear();
                foreach (MA_Categoria oMA_Categoria in oListCategoria)
                {
                    ListItem listItem = new ListItem(oMA_Categoria.nombre, oMA_Categoria.codigo);
                    oDropDownList.Items.Add(listItem);
                }
                oDropDownList.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //lblmensaje.ForeColor = System.Drawing.Color.Red;
                //lblmensaje.Text = "Ocurrió un error inesperado..." + ex.Message;

            }

            //////////////////////////////////////////////////////////////
            // Ver. 0.1     -   Anomino -   ??????
            //////////////////////////////////////////////////////////////
            //DataSet ds = null;
            //ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 13);
            ////se llena categorias en tipo de producto
            //cmbTipoCateg.DataSource = ds;
            //cmbTipoCateg.DataTextField = "Product_Category";
            //cmbTipoCateg.DataValueField = "id_ProductCategory";
            //cmbTipoCateg.DataBind();
            //ds = null;

        }
        private void LlenaSubporCategoProduct(string codCategoria)
        {

            /*
             * Ver. 1.0     -   PSalas  -   Se obtiene las categorias utilizando el Framework
             * Ver. 0.1     -   Anomino -   Obtiene las categorias usando el Procedimiento Almacenado (PA) UP_WEB_LLENACOMBOS
             */

            ////////////////////////////////////////////////////////////////////
            // Ver. 1.0     -   PSalas  -   22 Oct. 2016
            ////////////////////////////////////////////////////////////////////
            try
            {
                BL_SubCategoria oBL_SubCategoria = new BL_SubCategoria();
                List<MA_SubCategoria> oListSubCategoria = new List<MA_SubCategoria>();
                oListSubCategoria = oBL_SubCategoria.Get_SubCategoriasByCodCategoria(codCategoria);

                CmbSubCategoriaProduct.Items.Clear();
                foreach (MA_SubCategoria oMA_SubCategoria in oListSubCategoria)
                {
                    ListItem listItem = new ListItem(oMA_SubCategoria.nombre, oMA_SubCategoria.codigo);
                    CmbSubCategoriaProduct.Items.Add(listItem);
                }
                CmbSubCategoriaProduct.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //lblmensaje.ForeColor = System.Drawing.Color.Red;
                //lblmensaje.Text = "Ocurrió un error inesperado..." + ex.Message;

            }


            //////////////////////////////////////////////////////////////
            // Ver. 0.1     -   Anomino -   ??????
            //////////////////////////////////////////////////////////////
            //DataTable dt1 = new DataTable();
            //dt1 = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOSUBCATEGORIAPRESENT", cmbTipoCateg.SelectedValue);
            ////se llena Combo subcategoria segun categoria
            //CmbSubCategoriaProduct.DataSource = dt1;
            //CmbSubCategoriaProduct.DataTextField = "Name_Subcategory";
            //CmbSubCategoriaProduct.DataValueField = "id_Subcategory";
            //CmbSubCategoriaProduct.DataBind();
            //dt1 = null;
        }
        private void LlenaFamiliaporSubCategoria(string codSubCategoria)
        {

            /*
             * Ver. 1.0     -   PSalas  -   Se obtiene las familias utilizando el Framework
             */

            ////////////////////////////////////////////////////////////////////
            // Ver. 1.0     -   PSalas  -   29 Oct. 2016
            ////////////////////////////////////////////////////////////////////
            try
            {
                BL_Familia oBL_Familia = new BL_Familia();
                List<MA_Familia> oListFamilia = new List<MA_Familia>();
                oListFamilia = oBL_Familia.Get_FamiliasByCodSubCategoria(codSubCategoria);

                cmbPFamily.Items.Clear();
                foreach (MA_Familia oMA_Familia in oListFamilia)
                {
                    ListItem listItem = new ListItem(oMA_Familia.nombre, oMA_Familia.codigo);
                    cmbPFamily.Items.Add(listItem);
                }
                cmbPFamily.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //lblmensaje.ForeColor = System.Drawing.Color.Red;
                //lblmensaje.Text = "Ocurrió un error inesperado..." + ex.Message;

            }

        }
        private void LlenacomboMarcaProduct(DropDownList oDropDownList)
        {

            /*
             * Ver. 1.0     -   PSalas  -   Se obtiene las categorias utilizando el Framework
             * Ver. 0.1     -   Anomino -   Obtiene las categorias usando el Procedimiento Almacenado (PA) UP_WEB_LLENACOMBOS
             */

            ////////////////////////////////////////////////////////////////////
            // Ver. 1.0     -   PSalas  -   29 Oct. 2016 
            ////////////////////////////////////////////////////////////////////
            try
            {
                BL_Marca oBL_Marca = new BL_Marca();
                List<MA_Marca> oListMarca = new List<MA_Marca>();
                oListMarca = oBL_Marca.Get_Marcas();

                oDropDownList.Items.Clear();
                foreach (MA_Marca oMA_Marca in oListMarca)
                {
                    ListItem listItem = new ListItem(oMA_Marca.nombre, oMA_Marca.codigo);
                    oDropDownList.Items.Add(listItem);
                }
                oDropDownList.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //lblmensaje.ForeColor = System.Drawing.Color.Red;
                //lblmensaje.Text = "Ocurrió un error inesperado..." + ex.Message;

            }

            //////////////////////////////////////////////////////////////
            // Ver. 0.1     -   Anomino -   ??????
            //////////////////////////////////////////////////////////////
            /*
             Obtener las marcas de los todos los productos
             */
            /*
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOMARCASEGUNCATEGORIA", cmbTipoCateg.SelectedValue);
            //se llena Combo de marca en buscar de maestro familia de producto
            cmbFabricante.DataSource = dt;
            cmbFabricante.DataTextField = "Name_Brand";
            cmbFabricante.DataValueField = "id_Brand";
            cmbFabricante.DataBind();
            cmbFabricante.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            dt = null;
            */

        }
        private void comboSubmarcaProd()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOSUBMARCAPROD", cmbFabricante.SelectedValue);
            //Se llena submarcas de productos en productos
            //cmbSelSubBrand.DataSource = ds;
            //cmbSelSubBrand.DataTextField = "Name_SubBrand";
            //cmbSelSubBrand.DataValueField = "id_SubBrand";
            //cmbSelSubBrand.DataBind();
            ds = null;
        }
        private void comboCompanyProducto()
        {
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOCLIENTEPRODUCT", Convert.ToInt32(cmbFabricante.SelectedValue));
             //se llena compañia en Productos
            //TxtCompan.Text = dt.Rows[0]["Company_Name"].ToString().Trim();
            this.Session["Company_id"] = dt.Rows[0]["Company_id"].ToString().Trim();
            dt = null;
        }
        private void comboPresentacionProd()
        {
            DataTable dt = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOPRESENTPRODUCT", cmbTipoCateg.SelectedValue, Convert.ToInt32(cmbFabricante.SelectedValue));
           
            //Se llena presentacion de productos en productos
            //cmbPres.DataSource = dt;
            //cmbPres.DataTextField = "ProductPresentationName";
            //cmbPres.DataValueField = "id_ProductPresentation";
            //cmbPres.DataBind();
            // se modifica sp para q muestre el item seleccione desde sql para evitar error en consulta. Ing. Magaly Jiménez 29/09/2010
            //cmbPres.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            dt = null;

        }
        private void comboPFamiliaProd()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOFAMILYPRODUCT", cmbFabricante.SelectedValue);

            //Se llena presentacion de productos en productos
            cmbPFamily.DataSource = dt;
            cmbPFamily.DataTextField = "name_Family";
            cmbPFamily.DataValueField = "id_ProductFamily";
            cmbPFamily.DataBind();
            dt = null;
        }    
        private void tooltipnomPresent()
        {
            //try
            //{
            //    if (cmbPres.SelectedItem.Text != "0")
            //    {
            //        cmbPres.SelectedItem.Text = cmbPres.SelectedItem.Text.TrimStart();
            //        cmbPres.ToolTip = cmbPres.SelectedItem.Text;
            //    }
            //}
            //catch
            //{
            //}
        }
        //private void comboBproducCompany()
        //{
        //    DataSet ds1 = new DataSet();
        //    ds1 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 33);
        //    //se llena compañia en buscar Productos
        //    cbmbcompañia.DataSource = ds1;
        //    cbmbcompañia.DataTextField = "Company_Name";
        //    cbmbcompañia.DataValueField = "Company_id";
        //    cbmbcompañia.DataBind();
        //    ds1 = null;
        //}
        private void comboclienteBuscarproductoporplanning()
        {
            /*DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_CONSULTACLIENTE_PROPIOYCOPETIDORES", Cliente);
            //se llena cliente en Usuarios
            cbmbcompañia.DataSource = ds;
            cbmbcompañia.DataTextField = "Company_Name";
            cbmbcompañia.DataValueField = "Company_id";
            cbmbcompañia.DataBind();*/

            ListItem listItem0 = new ListItem("Compania00", "0");
            ListItem listItem1 = new ListItem("Compania01", "1");
            ListItem listItem2 = new ListItem("Compania02", "2");
            ListItem listItem3 = new ListItem("Compania03", "3");

            //cbmbcompañia.Items.Add(listItem0);
            //cbmbcompañia.Items.Add(listItem1);
            //cbmbcompañia.Items.Add(listItem2);
            //cbmbcompañia.Items.Add(listItem3);


        }  
        private void llenacomboCategoriaxClienteenBusProduct()
        {
            //DataSet ds1 = new DataSet();
            //ds1 = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOMARCAENBPRODUC", cbmbcompañia.SelectedValue,"0");
            ////se llena marca sefun cliente en buscar Productos
            //cmbBCategoriaProduct.DataSource = ds1.Tables[0];
            //cmbBCategoriaProduct.DataTextField = "Product_Category";
            //cmbBCategoriaProduct.DataValueField = "id_ProductCategory";
            //cmbBCategoriaProduct.DataBind();
            //cmbBCategoriaProduct.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            //IbtnProductos_ModalPopupExtender.Show();
            //ds1 = null;
        }
        //private void llenacomboMarcaaxCategoriaenBusProduct()
        //{
        //    DataSet ds1 = new DataSet();
        //    ds1 = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOMARCAENBPRODUC", cbmbcompañia.SelectedValue, cmbBCategoriaProduct.SelectedValue);
        //    //se llena marca por Categoria cliente en buscar Productos
        //    cmbBBrand.DataSource = ds1.Tables[1];
        //    cmbBBrand.DataTextField = "Name_Brand";
        //    cmbBBrand.DataValueField = "id_Brand";
        //    cmbBBrand.DataBind();
        //    cmbBBrand.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
        //    IbtnProductos_ModalPopupExtender.Show();
        //    ds1 = null;
        //}
        private void combosubcategoriaProducto()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOTIPOPROD", cmbTipoCateg.SelectedValue);
            //Se llena Tipo de productos en productos
            CmbSubCategoriaProduct.DataSource = ds;
            CmbSubCategoriaProduct.DataTextField = "Name_Subcategory";
            CmbSubCategoriaProduct.DataValueField = "id_Subcategory";
            CmbSubCategoriaProduct.DataBind();
            CmbSubCategoriaProduct.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;     
        }
        private void comboConsultaCompanyProducto()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOCLIENTEPRODUCT", 
                    Convert.ToInt32(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbFabricante")).Text));
                //se llena compañia en Productos
                string
                dato = dt.Rows[0]["Company_Name"].ToString().Trim();
                ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[8].FindControl("TxtCompan")).Text = dt.Rows[0]["Company_Name"].ToString().Trim();
                this.Session["Company_id"] = dt.Rows[0]["Company_id"].ToString().Trim();
                dt = null;
            }
            catch (Exception ex)
            { 
            }
        }
        private void comboConsultaPresentacionProd()
        {
            DataTable dt = new DataTable();
            dt = owsadministrativo.LlenaComboPresentProduct(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).SelectedValue, Convert.ToInt32(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbFabricante")).SelectedValue));

            if (dt.Rows.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[4].FindControl("cmbPres")).DataSource = dt;
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[4].FindControl("cmbPres")).DataTextField = "ProductPresentationName";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[4].FindControl("cmbPres")).DataValueField = "id_ProductPresentation";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[4].FindControl("cmbPres")).DataBind();
            }
            else
            {
            }           
            dt = null;
        }
        private void comboConsultaPFamiliaProd()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOFAMILYPRODUCT", ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbFabricante")).SelectedValue);

            if (dt.Rows.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[5].FindControl("cmbPFamily")).DataSource = dt;
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[5].FindControl("cmbPFamily")).DataTextField = "name_Family";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[5].FindControl("cmbPFamily")).DataValueField = "id_ProductFamily";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[5].FindControl("cmbPFamily")).DataBind();
            }
            else
            {
            }
            dt = null;
        }
        private void LLenaConsultaProductos()
        {
            try
            {
                DataTable oeProducto = oProductos.BuscarProductos(iCompanyId, CategoriaProduc, iid_Brand, SKU_Producto);
                GVConsulProduct.DataSource = oeProducto;
                GVConsulProduct.DataBind();
                oeProducto = null;
            }
            catch (Exception ex)
            {

            }
        }
        private void llenacomboUnidadMedida()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPOLORA_LLENACOMBOUMEDIDA");
            //se llena marca sefun cliente en buscar Productos

            //cmbUMedida.DataSource = dt;
            //cmbUMedida.DataTextField = "UnitOfMeasure_Name";
            //cmbUMedida.DataValueField = "id_UnitOfMeasure";
            //cmbUMedida.DataBind();
            //cmbUMedida.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            
            dt = null;
        }
 
        private void SavelimpiarControlesProductAncla()
        {
            cmbClienteAncla.Text = "0";
            cmbOficinaPancla.Text = "0";
            cmbCategoryAncla.Text = "0";
            cmbSubcateAncla.Text = "0";
            cmbproductAncla.Text="0";
            cmbMarcaAncla.Text = "0";
            TxtPrecioprodAncla.Text = "";
            TxtPesoPancla.Text = "";
            RBTproductoAncla.Items[0].Selected = true;
            RBTproductoAncla.Items[1].Selected = false;
           
            CmbBClientePAncla.Text="0";
            CmbBCategoriaPAncla.Text = "0";
        }
        private void activarControlesProductAncla()
        {
            cmbClienteAncla.Enabled = true;
            cmbOficinaPancla.Enabled = true;
            cmbCategoryAncla.Enabled = true;
            cmbSubcateAncla.Enabled = true;
            cmbproductAncla.Enabled = true;
            cmbMarcaAncla.Enabled = true;
            TxtPrecioprodAncla.Enabled = true;
            RBTproductoAncla.Enabled = false;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = false;
            TabProducAncla.Enabled = true;
            PanelSubFamilia.Enabled = false;
           

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
            RBTproductoAncla.Enabled = false;
            Panel_Marcas.Enabled = true;
            Panel_Submarcas.Enabled = true;
            Panel_ProductFamily.Enabled = true;
            Panel_CategProduct.Enabled = true;
            Panel_SubCategoria.Enabled = true;
            Panel_Presentación.Enabled = true;
            PanelProducto.Enabled = true;
            TabProducAncla.Enabled = true;
            PanelSubFamilia.Enabled = true;
        }
        private void crearActivarbotonesProductAncla()
        {
         
            BtnCrearAncla.Visible = false;
            BtnGuardarAncla.Visible = true;
            BtnConsultarAncla.Visible = false;
            BtnCaMasivaPancla.Visible = false;
            BtnCancelarAncla.Visible = true;
            BtnCarMasivaFamilia.Visible = false;
        }
        private void saveActivarbotonesProductAncla()
        {
            BtnCrearAncla.Visible = true;
            BtnGuardarAncla.Visible = false;
            BtnConsultarAncla.Visible = true;
            BtnCaMasivaPancla.Visible = true;
            BtnCancelarAncla.Visible = true;
            BtnCarMasivaFamilia.Visible = true;            
        }
        private void EditarActivarbotonesProductAncla()
        {

            BtnCrearAncla.Visible = false;
            BtnGuardarAncla.Visible = false;
            BtnConsultarAncla.Visible = true;
            BtnCancelarAncla.Visible = true;
          
        }
        private void EditarActivarControlesProductAncla()
        {
            cmbClienteAncla.Enabled = false;
            cmbOficinaPancla.Enabled = false;
            cmbCategoryAncla.Enabled = false;
            cmbSubcateAncla.Enabled = true;
            cmbproductAncla.Enabled = true;
            cmbMarcaAncla.Enabled = true;
            TxtPrecioprodAncla.Enabled = true;
            RBTproductoAncla.Enabled = true;
            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = false;
            TabProducAncla.Enabled = true;
        }
        private void BuscarActivarbotnesProductAncla()
        {
            BtnCrearAncla.Visible = false;
            BtnGuardarAncla.Visible = false;
            BtnConsultarAncla.Visible = true;
            BtnCancelarAncla.Visible = true;

        }
        private void LlenacomboClienteProductAncla()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPAncla(0, "0", 0, 0,"0");
           
            //se llena cliente en producto Ancla
            cmbClienteAncla.DataSource = ds.Tables[0];
            cmbClienteAncla.DataTextField = "Company_Name";
            cmbClienteAncla.DataValueField = "Company_id";
            cmbClienteAncla.DataBind();
            cmbClienteAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
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
        private void comboclienteenPanclaporplanning()
        {
            /*DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_CONSULTACLIENTE_PROPIOYCOPETIDORES", Cliente);
            //se llena cliente en Usuarios
            cmbClienteAncla.DataSource = ds;
            cmbClienteAncla.DataTextField = "Company_Name";
            cmbClienteAncla.DataValueField = "Company_id";
            cmbClienteAncla.DataBind();*/

            ListItem listItem0 = new ListItem("Compania00", "0");
            ListItem listItem1 = new ListItem("Compania01", "1");
            ListItem listItem2 = new ListItem("Compania02", "2");
            ListItem listItem3 = new ListItem("Compania03", "3");

            cmbClienteAncla.Items.Add(listItem0);
            cmbClienteAncla.Items.Add(listItem1);
            cmbClienteAncla.Items.Add(listItem2);
            cmbClienteAncla.Items.Add(listItem3);

        }
        private void comboclienteenGVPanclaporplanning(int i)
        {
            DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_CONSULTACLIENTE_PROPIOYCOPETIDORES", Cliente);
            //se llena cliente en Usuarios

            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).DataSource = ds;
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).DataTextField = "Company_Name";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).DataValueField = "Company_id";
                ((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).DataBind();
            }
            else
            {

            }
            ds = null;
        }  
        private void LlenacomboBuscarClienteProductAncla()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaConsultaCombosPAncla(0);
            //se llena cliente enconsulta en productos Ancla
            CmbBClientePAncla.DataSource = ds.Tables[0];
            CmbBClientePAncla.DataTextField = "Company_Name";
            CmbBClientePAncla.DataValueField = "Company_id";
            CmbBClientePAncla.DataBind();
            CmbBClientePAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void comboclienteenBuscarPanclaporplanning()
        {
            /*
            DataSet ds = null;
            Cliente = Convert.ToString(this.Session["companyid"]);
            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_CONSULTACLIENTE_PROPIOYCOPETIDORES", Cliente);
            //se llena cliente en Usuarios
            CmbBClientePAncla.DataSource = ds;
            CmbBClientePAncla.DataTextField = "Company_Name";
            CmbBClientePAncla.DataValueField = "Company_id";
            CmbBClientePAncla.DataBind();
            */

            ListItem listItem0 = new ListItem("CompaniaAncla00", "0");
            ListItem listItem1 = new ListItem("CompaniaAncla01", "1");
            ListItem listItem2 = new ListItem("CompaniaAncla02", "2");
            ListItem listItem3 = new ListItem("CompaniaAncla03", "3");

            CmbBClientePAncla.Items.Add(listItem0);
            CmbBClientePAncla.Items.Add(listItem1);
            CmbBClientePAncla.Items.Add(listItem2);
            CmbBClientePAncla.Items.Add(listItem3);

        }  
        private void LlenacomboBuscarCategoryProductAncla()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaConsultaCombosPAncla(Convert.ToInt32(CmbBClientePAncla.SelectedValue));
            //se llena categorya en busqueda enconsulta en productos Ancla
            CmbBCategoriaPAncla.DataSource = ds.Tables[1];
            CmbBCategoriaPAncla.DataTextField = "Product_Category";
            CmbBCategoriaPAncla.DataValueField = "id_ProductCategory";
            CmbBCategoriaPAncla.DataBind();
            CmbBCategoriaPAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
        }
        private void LlenacomboCategoProductAncla()
        {
            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPAncla(Convert.ToInt32(cmbClienteAncla.SelectedValue), "0", 0, 0, "0");
            //llena categoria segun cliente
            cmbCategoryAncla.DataSource = ds.Tables[1];
            cmbCategoryAncla.DataTextField = "Product_Category";
            cmbCategoryAncla.DataValueField = "id_ProductCategory";
            cmbCategoryAncla.DataBind();
            cmbCategoryAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            ds = null;
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
        private void LlenaSubporCategoProductAncla()
        {
            DataSet ds = null;
           
            ds = owsadministrativo.llenaCombosPAncla(Convert.ToInt32(cmbClienteAncla.SelectedValue), cmbCategoryAncla.SelectedValue, 0, 0, "0");
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
        private void tooltipnomProducAncla()
        {
            try
            {
                if (cmbproductAncla.SelectedItem.Text != "0")
                {
                    cmbproductAncla.SelectedItem.Text = cmbproductAncla.SelectedItem.Text.TrimStart();
                    cmbproductAncla.ToolTip = cmbproductAncla.SelectedItem.Text;
                }
            }
            catch
            {
            }
        }
        private void tooltiGVpnomProducAncla(int i)
        {
            try
            {
                if (((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).SelectedItem.Text != "0")
                {
                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).SelectedItem.Text = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).SelectedItem.Text.TrimStart();
                    ((DropDownList)GVConsultaPancla.Rows[i].Cells[5].FindControl("cmbprodupancla")).ToolTip = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).SelectedItem.Text;
                }
            }
            catch
            {
            }
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
            ds = owsadministrativo.llenaCombosPAncla(Convert.ToInt32(cmbClienteAncla.SelectedValue), cmbCategoryAncla.SelectedValue, Convert.ToInt64(ssubCategoryP), 0, "0");
            //se llena marcas segun subcategoria y cliente
            cmbMarcaAncla.DataSource = ds.Tables[4];
            cmbMarcaAncla.DataTextField = "Name_Brand";
            cmbMarcaAncla.DataValueField = "id_Brand";
            cmbMarcaAncla.DataBind();
            cmbMarcaAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            
            ds = null;

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
            ds = owsadministrativo.llenaCombosPAncla(Convert.ToInt32(cmbClienteAncla.SelectedValue), cmbCategoryAncla.SelectedValue, Convert.ToInt64(ssubCategoryP), Convert.ToInt32(cmbMarcaAncla.SelectedValue), "0");
                        
            cmbproductAncla.DataSource = ds.Tables[5];
            cmbproductAncla.DataTextField = "Product_Name";
            cmbproductAncla.DataValueField = "cod_Product";
            cmbproductAncla.DataBind();
            cmbproductAncla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
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
        private void LlenaPrecioPAncla()
        {

            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPAncla(Convert.ToInt32(cmbClienteAncla.SelectedValue), "0", 0, 0, cmbproductAncla.SelectedValue);
            TxtPrecioprodAncla.Text = ds.Tables[6].Rows[0][0].ToString().Trim();
            //this.Session["Company_id"] = ds.Tables[6][""].ToString().Trim();
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
        private void LlenaPesoPAncla()
        {

            DataSet ds = null;
            ds = owsadministrativo.llenaCombosPAncla(Convert.ToInt32(cmbClienteAncla.SelectedValue), "0", 0, 0, cmbproductAncla.SelectedValue);
            TxtPesoPancla.Text = ds.Tables[7].Rows[0][0].ToString().Trim();
            //this.Session["Company_id"] = ds.Tables[6][""].ToString().Trim();
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
        private void comboOficinaXclienteenPancla()
        {
            DataSet ds = null;

            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOOFICINAPANCLA", cmbClienteAncla.SelectedValue);
            //se llena cliente en Usuarios
            cmbOficinaPancla.DataSource = ds;
            cmbOficinaPancla.DataTextField = "Name_Oficina";
            cmbOficinaPancla.DataValueField = "cod_Oficina";
            cmbOficinaPancla.DataBind();

        }
        private void comboOficinaXGVclienteenPancla(int i)
        {
            DataSet ds = null;

            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOOFICINAPANCLA", Convert.ToInt32(((DropDownList)GVConsultaPancla.Rows[i].Cells[0].FindControl("cmbcliepancla")).SelectedValue));
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
        private void llenaComboOficinaConsultaPancla()
        {
            DataSet ds = null;

            ds = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_LLENACOMBOOFICINACONSULTARPANCLA", CmbBClientePAncla.SelectedValue, CmbBCategoriaPAncla.SelectedValue);
            //se llena cliente en Usuarios
            cmbOficinaBPancla.DataSource = ds;
            cmbOficinaBPancla.DataTextField = "Name_Oficina";
            cmbOficinaBPancla.DataValueField = "cod_Oficina";
            cmbOficinaBPancla.DataBind();
            cmbOficinaBPancla.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
        }
        private void consultaGVMarca(EBrand dtmarca)
        {
            icodBrand = dtmarca.id_Brand;
            string pcategory = dtmarca.id_ProductCategory;
            sBrand = dtmarca.Name_Brand;

            try
            {
                this.planningADM = this.Session["AdmProd"].ToString().Trim();
            }
            catch
            {
            }

            if (this.planningADM == "SI")
            {
                DataTable dt = oBrand.SearchBrandPlanning(icodBrand, cmbBuscarCategoryM.Text, sBrand, Convert.ToInt32(Cliente));
                gridbuscarmarcas(dt);
                dt = null;
                LLenacomboCategoriaBuscarMarcaporplanning();
            }
            else
            {
                DataTable dt = oBrand.SearchBrand(icodBrand, pcategory, sBrand);
                gridbuscarmarcas(dt);
                dt = null;
                llenaComboCategoriaBuscarMarca();
            }

            Alertas.CssClass = "MensajesError";
            LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
            MensajeAlerta();
            IbtnBrand.Show();


        }      
        private void gridbuscarmarcas(DataTable oeBrand)
        {
            GVConsultaMarca.DataSource = oeBrand;
            GVConsultaMarca.DataBind();
            ModalGVConsultaMarca.Show();
            for (int i = 0; i <= oeBrand.Rows.Count - 1; i++)
            {
                String idclient = oeBrand.Rows[i][1].ToString().Trim();
                String idcategory = oeBrand.Rows[i][2].ToString().Trim();
                ((Label)GVConsultaMarca.Rows[i].FindControl("lblClientecMarca")).Text = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_CONSULTACLIENTE_PROPIOYCOPETIDORES", idclient).Rows[1][1].ToString();
                ((Label)GVConsultaMarca.Rows[i].FindControl("lblidcategoria")).Text = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_CATEGORIA_BY_ID", idcategory).Rows[0][0].ToString();
            }
        }
        /** Metodos para el Panel de Sub Familias **/
        private void habilitarControlesSubFamily()
        {
            btncreasubfam.Visible = false;
            btnguardasubfam.Visible = true;
            btnconsultasubfam.Visible = false;    
            btncancelsubfam.Visible = true;
            btncmasivasubfam.Visible = false;
            txtcodigosubfamilia.Enabled = false;
            txtnomsubfamilia.Enabled = true;
            ddlfamilias.Enabled = true;
            ddl_sf_categoria.Enabled = true;
            ddl_sf_subcategoria.Enabled = true;
            ddl_sf_marca.Enabled = true;
            ddl_sf_submarca.Enabled = true;
            ddl_sf_cliente.Enabled = true;
        }
        private void deshabilitarControlesSubFamily()
        {
            btncreasubfam.Visible = true;
            btnguardasubfam.Visible = false;
            btnconsultasubfam.Visible = true;
            btncancelsubfam.Visible = true;
            btncmasivasubfam.Visible = true;
            txtcodigosubfamilia.Enabled = false;
            txtnomsubfamilia.Enabled = false;
            ddlfamilias.Enabled = false;
            ddl_sf_categoria.Enabled = false;
            ddl_sf_subcategoria.Enabled = false;
            ddl_sf_marca.Enabled = false;
            ddl_sf_submarca.Enabled = false;
            ddl_sf_cliente.Enabled = false;
        }
        private void MensajeAlerta()
        {            
            ModalPopupAlertas.Show();
            BtnAceptarAlert.Focus();
            //ScriptManager.RegisterStartupScript(
            //    this, this.GetType(), "myscript", "alert('Debe ingresar todos los parametros con (*)');", true);
        }
        private void llenarcombocliente()
        {
            /*DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_CLIENTES_EXTERNOS");
            //se llena cliente 
            cmb_categorias_cliente.DataSource = dt;
            cmb_categorias_cliente.DataTextField = "Company_Name";
            cmb_categorias_cliente.DataValueField = "Company_id";
            cmb_categorias_cliente.DataBind();
            cmb_categorias_cliente.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            foreach (ListItem li in cmb_categorias_cliente.Items)
            {
                cmb_Cliente.Items.Add(li);
            }*/

            ListItem listItem0 = new ListItem("Compania00", "0");
            ListItem listItem1 = new ListItem("Compania01", "1");
            ListItem listItem2 = new ListItem("Compania02", "2");
            ListItem listItem3 = new ListItem("Compania03", "3");

            cmb_categorias_cliente.Items.Add(listItem0);
            cmb_categorias_cliente.Items.Add(listItem1);
            cmb_categorias_cliente.Items.Add(listItem2);
            cmb_categorias_cliente.Items.Add(listItem3);
            cmb_categorias_cliente.Items.Insert(0, new ListItem("<Seleccione..>", "99"));

        }
        #endregion

        #region Marcas

        protected void BtnCrearBrand_Click(object sender, EventArgs e)
        {
            try
            {
                this.planningADM = this.Session["AdmProd"].ToString().Trim();
            }
            catch
            {
            }

            if (this.planningADM == "SI")
                comboclienteenMarcaporplanning();
            else
                comboclienteenMarca();

            LlenacomboCategMarca();
            crearActivarbotonesMarca();
            activarControlesMarca();
            BtnCargaMasiva.Visible = false;            
        }
        protected void cmbClienteMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable categ;
            DataTable dt;
            dt = oConn.ejecutarDataTable("UP_WEB_SEARCH_COMPANYXID", cmbClienteMarca.SelectedValue);

            if (dt.Rows[0]["Type_Company"].ToString() == "3")
            {
                categ = oConn.ejecutarDataTable("UP_WEBXPLORA_CONSULTA_CATEGORIA_COPETIDORA", cmbClienteMarca.SelectedValue);
                cmbCategoryMarca.DataSource = categ;
                cmbCategoryMarca.DataTextField = "Product_Category";
                cmbCategoryMarca.DataValueField = "id_ProductCategory";
                cmbCategoryMarca.DataBind();
                categ = null;
            }
            else
            {



                categ = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOBUSCARCATEGORIAXCOMPANY", cmbClienteMarca.SelectedValue);
                cmbCategoryMarca.DataSource = categ;
                cmbCategoryMarca.DataTextField = "Product_Category";
                cmbCategoryMarca.DataValueField = "id_ProductCategory";
                cmbCategoryMarca.DataBind();
                categ = null;
            }

        }
        protected void BtnSaveBrand_Click(object sender, EventArgs e)
        {
            BtnCargaMasiva.Visible = true;
            LblFaltantes.Text = "";
            TxtNomBrand.Text = TxtNomBrand.Text.TrimStart();
            TxtNomBrand.Text = TxtNomBrand.Text.TrimEnd();
            if (TxtNomBrand.Text == "" || cmbClienteMarca.Text == "0" || cmbCategoryMarca.Text == "0")
            {               
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {
               
                DAplicacion odconsulBrand = new DAplicacion();
                DataTable dtconsulta = odconsulBrand.ConsultaDuplicados(ConfigurationManager.AppSettings["Brand"], TxtNomBrand.Text, cmbClienteMarca.Text, cmbCategoryMarca.Text);
                if (dtconsulta == null)
                {                
                    EBrand oeBrand = oBrand.RegistrarBrand(Convert.ToInt32(cmbClienteMarca.Text), cmbCategoryMarca.Text,  TxtNomBrand.Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    EBrand oebrandtmp = oBrand.RegistrarBrandtmp(Convert.ToInt32(cmbClienteMarca.Text), oeBrand.id_Brand,oeBrand.Name_Brand,oeBrand.Brand_Status);
                    //consultaUltimoIdmarca();
                    EBrand oebrandCategorytmp = oBrand.RegistrarBrandCategorytmp( cmbCategoryMarca.Text, oeBrand.id_Brand.ToString());
                    string sBrand = "";
                    sBrand = TxtNomBrand.Text;
                    this.Session["sBrand"] = sBrand;
                    SavelimpiarControlesMarca();
                    try
                    {
                        this.planningADM = this.Session["AdmProd"].ToString().Trim();
                    }
                    catch
                    {
                    }

                    if (this.planningADM == "SI")
                    {
                        LLenacomboCategoriaBuscarMarcaporplanning();
                        combocomboMarcaenSubarcaporplanning();
                    }
                    else
                    {
                        llenaComboCategoriaBuscarMarca();
                        LlenacomboMarcaensubMarca();
                    }               
                   
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Marca de Producto " + this.Session["sBrand"] + " fue creada con éxito";
                    MensajeAlerta();
                    saveActivarbotonesMarca();
                    desactivarControlesMarca();                  
                }
                else
                {
                    string sBrand = "";
                    sBrand = TxtNomBrand.Text;
                    this.Session["sBrand"] = sBrand;
                    this.Session["mensajealert"] = "La Marca de Producto " + this.Session["sBrand"];
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La Marca de Producto " + this.Session["sBrand"] + " ya existe";
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
        protected void BtnBBrand_Click(object sender, EventArgs e)
        {
            BtnCargaMasiva.Visible = true;
            desactivarControlesMarca();
            LblFaltantes.Text = "";
            TxtBCodBrand.Text = TxtBCodBrand.Text.TrimStart().TrimEnd();
            
            if (TxtBCodBrand.Text == "" && TxtBNomBrand.Text == "" && cmbBuscarCategoryM.Text == "0")
            {
                this.Session["mensajealert"] = "Código y/o Nombre de Marca de producto";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parámetro de consulta";
                MensajeAlerta();
                IbtnBrand.Show();
                return;
            }

            BuscarActivarbotnesMarca();

            try
            {
                icodBrand = Convert.ToInt32(TxtBCodBrand.Text);
            }
            catch
            {
            }

            sBrand = TxtBNomBrand.Text;
            TxtBCodBrand.Text = "";
            TxtBNomBrand.Text = "";
            cmbClienteMarca.Text = "0";
                        
            try
            {
                this.planningADM = this.Session["AdmProd"].ToString().Trim();
            }
            catch
            {
            }

            if (this.planningADM == "SI") // si el perfil es adm
            {
                Cliente = Convert.ToString(this.Session["companyid"]);
                DataTable oebrandpla = oBrand.SearchBrandPlanning(icodBrand, cmbBuscarCategoryM.Text, sBrand, Convert.ToInt32(Cliente));
                this.Session["CMarca"] = oebrandpla;
                if (oebrandpla != null)
                {
                    if (oebrandpla.Rows.Count > 0)
                    {
                        GVConsultaMarca.DataSource = null;
                        GVConsultaMarca.DataSource = oebrandpla;
                        GVConsultaMarca.DataBind();
                        ModalGVConsultaMarca.Show();

                        for (int i = 0; i < oebrandpla.Rows.Count; i++)
                        {                            
                            String idclient = oebrandpla.Rows[i][1].ToString().Trim();
                            String idcategory = oebrandpla.Rows[i][2].ToString().Trim();
                            ((Label)GVConsultaMarca.Rows[i].FindControl("lblClientecMarca")).Text = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_CONSULTACLIENTE_PROPIOYCOPETIDORES", idclient).Rows[1][1].ToString();
                            ((Label)GVConsultaMarca.Rows[i].FindControl("lblidcategoria")).Text = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_CATEGORIA_BY_ID", idcategory).Rows[0][0].ToString();                                                        
                            //Cliente = ((Label)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[0].FindControl("lblClientecMarca")).Text;
                            //LlenacomboConsultaClienteMarca();
                            //((Label)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[0].FindControl("cmbClienteMarca")).Text = oebrandpla.Rows[i]["Company_id"].ToString().Trim();
                            //LlenacomboConsultaCategoriaMarca();
                            //((Label)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[0].FindControl("cmbCategoryMarca")).Text = oebrandpla.Rows[i]["id_ProductCategory"].ToString().Trim();
                            //((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).Text = Cliente;                        
                            //((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[2].FindControl("cmbCategoryMarca")).Text = Categoria;  
                            ModalGVConsultaMarca.Show();
                            //((TextBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[3].FindControl("TxtNomBrand")).Text = Marca;
                            //((CheckBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[4].FindControl("CheckEMarca")).Checked = estado;
                            this.Session["rept"] = ((Label)GVConsultaMarca.Rows[i].FindControl("lblcMArca")).Text;
                        }
                    }
                    else
                    {
                        SavelimpiarControlesMarca();
                        saveActivarbotonesMarca();
                        comboclienteenMarca();
                        try
                        {
                            this.planningADM = this.Session["AdmProd"].ToString().Trim();
                        }
                        catch
                        {
                        }

                        if (this.planningADM == "SI")
                        {
                            LLenacomboCategoriaBuscarMarcaporplanning();
                        }
                        else
                        {
                            llenaComboCategoriaBuscarMarca();
                        }

                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La consulta realizada no devolvió ninguna respuesta";
                        MensajeAlerta();
                        IbtnBrand.Show();
                    }
                }
            }
            else // si el perfil no es adm
            {
                DataTable oeBrand = oBrand.SearchBrand(icodBrand, cmbBuscarCategoryM.Text, sBrand);
                this.Session["CMarca"] = oeBrand;
                if (oeBrand != null)
                {
                    if (oeBrand.Rows.Count > 0)
                    {
                       //metodo de carga
                        gridbuscarmarcas(oeBrand);
                    }
                    else
                    {
                        SavelimpiarControlesMarca();
                        saveActivarbotonesMarca();
                        comboclienteenMarca();
                        try
                        {
                            this.planningADM = this.Session["AdmProd"].ToString().Trim();
                        }
                        catch
                        {
                        }

                        if (this.planningADM == "SI")
                        {
                            LLenacomboCategoriaBuscarMarcaporplanning();
                        }
                        else
                        {
                            llenaComboCategoriaBuscarMarca();
                        }

                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                        MensajeAlerta();
                        IbtnBrand.Show();
                    }
                }
            }

            this.Session["icodBrand"]=icodBrand;
            this.Session["cmbBuscarCategoryM.Text"]=cmbBuscarCategoryM.Text;
            this.Session["sBrand"] = sBrand;
            this.Session["Exportar_Excel"] = "Exportar_Marcas";
            
            DataTable dtnameCombosMarca = new DataTable();
            dtnameCombosMarca.Columns.Add("Código", typeof(String));
            dtnameCombosMarca.Columns.Add("Cliente", typeof(String));
            dtnameCombosMarca.Columns.Add("Categoria", typeof(String));
            dtnameCombosMarca.Columns.Add("Nombre Marca", typeof(String));
            dtnameCombosMarca.Columns.Add("Estado", typeof(String));

            for (int i = 0; i <= GVConsultaMarca.Rows.Count - 1; i++)
            {
                DataRow dr = dtnameCombosMarca.NewRow();
                dr["Código"] = ((Label)GVConsultaMarca.Rows[i].Cells[0].FindControl("LblCodBrand2")).Text;
                dr["Cliente"] = ((Label)GVConsultaMarca.Rows[i].Cells[1].FindControl("lblClientecMarca")).Text;
                dr["Categoria"] = ((Label)GVConsultaMarca.Rows[i].Cells[2].FindControl("lblidcategoria")).Text;
                dr["Nombre Marca"] = ((Label)GVConsultaMarca.Rows[i].Cells[3].FindControl("lblcMArca")).Text;
                dr["Estado"] = ((CheckBox)GVConsultaMarca.Rows[i].Cells[4].FindControl("CheckEMarca")).Checked;

                dtnameCombosMarca.Rows.Add(dr);
            }

            this.Session["CExporMarca"] = dtnameCombosMarca;
        }
        protected void BtnCancelBrand_Click(object sender, EventArgs e)
        {
            saveActivarbotonesMarca();
            desactivarControlesMarca();
            SavelimpiarControlesMarca();
            BtnCargaMasiva.Visible = true;
            ModalGVConsultaMarca.Hide();
            CargaMasiva.Visible = false;
        }
        protected void BtnCargaMAsivaMarca_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            InicializarPaneles();
            CargaMasiva.Style.Value = "Display:Block;";
        }
        protected void GVConsultaMarca_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            GVConsultaMarca.PageIndex = e.NewPageIndex;
            GVConsultaMarca.DataSource = (DataTable)this.Session["CMarca"];
            GVConsultaMarca.DataBind();
            ModalGVConsultaMarca.Show();
        }
        protected void btnCancelar_Click(object sender, System.EventArgs e)
        {
            InicializarPaneles();            
            CosultaGVMarca.Style.Value = "Display:Block;";
            saveActivarbotonesProducto();
            TxtPrecioprodAncla.Text = "";
        }
        protected void GVConsultaMarca_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            btnCancelar.Visible = false;
            ModalGVConsultaMarca.Show();
            GVConsultaMarca.EditIndex = e.NewEditIndex;
            string  Cliente, Categoria, Marca;
            bool estado;
            Cliente = ((Label)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[0].FindControl("lblClientecMarca")).Text;
            Categoria = ((Label)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[0].FindControl("lblidcategoria")).Text;
            Marca = ((Label)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[0].FindControl("lblcMArca")).Text;
            estado =((CheckBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[0].FindControl("CheckEMarca")).Checked;
            GVConsultaMarca.DataSource = (DataTable)this.Session["CMarca"];
            GVConsultaMarca.DataBind();
            try
            {
                this.planningADM = this.Session["AdmProd"].ToString().Trim();
            }
            catch
            {
            }
            if (this.planningADM == "SI")
            {
                LlenacomboConsultaClienteMarcaPlanning();
            }
            else
            {
                LlenacomboConsultaClienteMarca();
            }
            LlenacomboConsultaClienteMarca();
            ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).Items.FindByText(Cliente).Selected = true;
            LlenacomboConsultaCategoriaMarca();
            ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[2].FindControl("cmbCategoryMarca")).Items.FindByText(Categoria).Selected = true; 
            ((TextBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[3].FindControl("TxtNomBrand")).Text = Marca;
            ((CheckBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[4].FindControl("CheckEMarca")).Checked = estado;
            this.Session["rept"] = ((TextBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[3].FindControl("TxtNomBrand")).Text;                      
        }
        protected void GVConsultaMarca_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            GVConsultaMarca.EditIndex = -1;
            gridbuscarmarcas((DataTable)this.Session["CMarca"]);            
            btnCancelar.Visible = true;
            ModalGVConsultaMarca.Show();
        }
        protected void GVConsultaMarca_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomBrand.Text = TxtNomBrand.Text.TrimStart();            

            if (((CheckBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[4].FindControl("CheckEMarca")).Checked != false)
            {
                estado = true;
            }
            else
            {
                estado = false;
                DAplicacion oddeshabBrand = new DAplicacion();
                DataTable dt = oddeshabBrand.PermitirDeshabilitar(ConfigurationManager.AppSettings["BrandProducts_Planning"], TxtCodBrand.Text);
                DataTable dt1 = oddeshabBrand.PermitirDeshabilitar(ConfigurationManager.AppSettings["BrandProducts"], TxtCodBrand.Text);
                DataTable dt2 = oddeshabBrand.PermitirDeshabilitar(ConfigurationManager.AppSettings["Brand_SubBrand"], TxtCodBrand.Text);

                if (dt != null || dt1 != null || dt2 != null)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No puede deshabilitar este registro, ya que existe relación en otro maestro. Por favor verifique.";
                    MensajeAlerta();
                    return;
                }
            }

            if (((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).Text == "0" || ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[2].FindControl("cmbCategoryMarca")).Text == "0" || ((TextBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[3].FindControl("TxtNomBrand")).Text == "")
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
                repetido = Convert.ToString(this.Session["rept"]);
                this.Session["sBrand"] = ((TextBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[3].FindControl("TxtNomBrand")).Text;
                if (repetido != ((TextBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[3].FindControl("TxtNomBrand")).Text) //si el nombre de la marca ha cambiado
                {
                    DAplicacion odconsulBrand = new DAplicacion();
                    DataTable dtconsulta = odconsulBrand.ConsultaDuplicados(ConfigurationManager.AppSettings["Brand"], ((TextBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[3].FindControl("TxtNomBrand")).Text, ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).Text, ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[2].FindControl("cmbCategoryMarca")).Text); //consulta si existen duplicados
                    if (dtconsulta == null) //si el cambio a realizar no se encuentra ya registrado en la DB
                    {
                        EBrand oeBrand = oBrand.Actualizar_Brand(Convert.ToInt32(((Label)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[0].FindControl("TxtCodBrand")).Text), Convert.ToInt32(((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).Text), ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[2].FindControl("cmbCategoryMarca")).Text, ((TextBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[3].FindControl("TxtNomBrand")).Text, ((CheckBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[4].FindControl("CheckEMarca")).Checked, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        EBrand oeBrandtmp = oBrand.Actualizar_BrandTMP(Convert.ToInt32(((Label)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[0].FindControl("TxtCodBrand")).Text), ((TextBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[3].FindControl("TxtNomBrand")).Text, ((CheckBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[4].FindControl("CheckEMarca")).Checked, ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[2].FindControl("cmbCategoryMarca")).Text);
                        GVConsultaMarca.EditIndex = -1;
                        consultaGVMarca(oeBrand);
                        IbtnSubBrand.Hide();
                        ModalGVConsultaMarca.Show();
                        btnCancelar.Visible = true;
                        SavelimpiarControlesMarca();

                        if (this.planningADM == "SI")
                        {
                            combocomboMarcaenSubarcaporplanning();
                        }
                        else
                        {
                            LlenacomboMarcaensubMarca();
                        }
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Marca de Producto : " + this.Session["sBrand"] + " se actualizó correctamente";
                        MensajeAlerta();
                        //saveActivarbotonesMarca();
                        //desactivarControlesMarca();
                    }
                    else
                    {
                        sBrand = TxtNomBrand.Text;
                        this.Session["sBrand"] = sBrand;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La Marca de Producto : " + this.Session["sBrand"] + " ya existe";
                        MensajeAlerta();
                    }
                    //BuscarBrand.Visible = false;
                }
                else // si el nombre de la marca no ha cambiado
                {

                    EBrand oeBrand = oBrand.Actualizar_Brand(Convert.ToInt32(((Label)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[0].FindControl("TxtCodBrand")).Text), Convert.ToInt32(((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[1].FindControl("cmbClienteMarca")).Text), ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[2].FindControl("cmbCategoryMarca")).Text, ((TextBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[3].FindControl("TxtNomBrand")).Text, ((CheckBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[4].FindControl("CheckEMarca")).Checked, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    EBrand oeBrandtmp = oBrand.Actualizar_BrandTMP(Convert.ToInt32(((Label)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[0].FindControl("TxtCodBrand")).Text), ((TextBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[3].FindControl("TxtNomBrand")).Text, ((CheckBox)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[4].FindControl("CheckEMarca")).Checked, ((DropDownList)GVConsultaMarca.Rows[GVConsultaMarca.EditIndex].Cells[2].FindControl("cmbCategoryMarca")).Text);                   
                    //SavelimpiarControlesMarca();
                    GVConsultaMarca.EditIndex = -1;
                    consultaGVMarca(oeBrand);
                    IbtnSubBrand.Hide();
                    ModalGVConsultaMarca.Show();
                    btnCancelar.Visible = true;
                    //btn_img_exporttoexcel.Visible = true;

                    if (this.planningADM == "SI")
                    {
                        combocomboMarcaenSubarcaporplanning();
                    }
                    else
                    {
                        LlenacomboMarcaensubMarca();
                    }
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Marca de Producto : " + this.Session["sBrand"] + " se actualizó correctamente";
                    MensajeAlerta();
                    IbtnSubBrand.Hide();
                    
                    //saveActivarbotonesMarca();
                    //desactivarControlesMarca();
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

        #region SubMarcas
        protected void BtnCrearSubBrand_Click(object sender, EventArgs e)
        {
            if (this.planningADM == "SI")
                LLenacomboCategoriaSubMarcasplanning();
            else
                LlenacomboCategSubMarca();          
            crearActivarbotonesSubMarca();
            activarControlesSubMarca();
        }
        protected void cmbCategoriaSubmarca_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.planningADM == "SI")
            {
                combocomboMarcaenSubarcaporplanning();
            }
            else
            {
                LlenacomboMarcaensubMarca();
            }       
        }
        protected void cmbCategorySubmarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenamarcaconsultaSubMarca();
            IbtnSubBrand.Show();
        }
        protected void BtnsaveSubBrand_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomSubMarca.Text = TxtNomSubMarca.Text.TrimStart();
            TxtNomSubMarca.Text = TxtNomSubMarca.Text.TrimEnd();
            string sidBrand = CmbSelBrand.Text;
            if (TxtNomSubMarca.Text == "" || CmbSelBrand.Text == "0" || cmbCategoriaSubmarca.Text=="0")
            {
                if (TxtNomSubMarca.Text == "")
                {
                    LblFaltantes.Text = ". " + "SubMarca de producto";
                }
                if (CmbSelBrand.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Marca de producto";
                }
                if (cmbCategoriaSubmarca.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Categoría de producto";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {
               
                DAplicacion odconsulSubBrand = new DAplicacion();
                DataTable dtconsulta = odconsulSubBrand.ConsultaDuplicados(ConfigurationManager.AppSettings["SubBrand"], TxtNomSubMarca.Text, CmbSelBrand.Text, null);
                if (dtconsulta == null)
                {
                    ESubBrand oeSubBrand = oSubBrand.RegistrarSubBrand(TxtNomSubMarca.Text, Convert.ToInt32(CmbSelBrand.Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    ESubBrand oeSubBrandtmp = oSubBrand.RegistrarSubBrandTMP(oeSubBrand.id_SubBrand.ToString(), oeSubBrand.Name_SubBrand, oeSubBrand.id_Brand, oeSubBrand.SubBrand_Status);
                    string sSubBrand = "";
                    sSubBrand = CmbSelBrand.SelectedItem.Text + " " + TxtNomSubMarca.Text;
                    this.Session["sSubBrand"] = sSubBrand;
                    SavelimpiarControlesSubMarca();
                    LlenacomboCategSubMarca();
                   // LlenacomboMarcaensubMarca();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La SubMarca de Producto " + this.Session["sSubBrand"] + " fue creada con éxito";
                    MensajeAlerta();
                    saveActivarbotonesSubMarca();
                    desactivarControlesSubMarca();
                }
                else
                {
                    string sSubBrand = "";
                    sSubBrand = CmbSelBrand.SelectedItem.Text + " " + TxtNomSubMarca.Text;
                    this.Session["sSubBrand"] = sSubBrand;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La SubMarca de Producto " + this.Session["sSubBrand"] + " ya existe";
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
        protected void BtnBSubBrand_Click(object sender, EventArgs e)
        {
            IbtnSubBrand.Hide();
            desactivarControlesSubMarca();
            LblFaltantes.Text = "";
            TxtBNomSubBrand.Text = TxtBNomSubBrand.Text.TrimStart();

            if (cmbCategorySubmarca.Text == "0" && TxtBNomSubBrand.Text == "" && CmbBSelBrand.Text == "0")
            {
                this.Session["mensajealert"] = "Código , Nombre de SubMarca y/o Marca de Producto";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parámetro de consulta";
                MensajeAlerta();
                IbtnSubBrand.Show();
                return;
            }

            BuscarActivarbotnesSubMarca();
            try
            {
                scodSubBrand =cmbCategorySubmarca.Text;
            }
            catch { } 
            sSubBrand = TxtBNomSubBrand.Text;
            if (CmbBSelBrand.Text == "")
            {
                icodBrand = 0;
            }
            else
            {

                icodBrand = Convert.ToInt32(CmbBSelBrand.Text);
            }
            //icodBrand = Convert.ToInt32(CmbBSelBrand.Text);
            cmbCategorySubmarca.Text = "0";
            TxtBNomSubBrand.Text = "";
            CmbBSelBrand.Text = "0";

            this.Session["scodSubBrand"] = scodSubBrand;
            this.Session["icodBrand"] = icodBrand;
            this.Session["sSubBrand"] = sSubBrand;

            DataTable oeSubBrand = oSubBrand.SearchSubBrand(scodSubBrand, icodBrand, sSubBrand );
            this.Session["CSubMarca"] = oeSubBrand;
            if (oeSubBrand != null)
            {
                if (oeSubBrand.Rows.Count > 0)
                {
                    GvConsultaSubmarca.DataSource = oeSubBrand;
                    GvConsultaSubmarca.DataBind();
                    ModalpConsGVSubmarca.Show();
                    IbtnSubBrand.Hide();

                    for (int i = 0; i <= oeSubBrand.Rows.Count - 1; i++)
                    {
                        try
                        {
                            LlenacomboGVCategSubMarca(i);
                            ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[1].FindControl("cmbCateSubMarca")).Text = oeSubBrand.Rows[i][8].ToString().Trim();
                            LlenacomboGVMarcaensubMarca(i);
                            ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[2].FindControl("cmbMarcaSubMarca")).Text = oeSubBrand.Rows[i][2].ToString().Trim();                           
                        }
                        catch (Exception ex) { }
                    }

                    this.Session["Exportar_Excel"] = "Exportar_SubMarca";

                    DataTable dtnameCombosSubmarca = new DataTable();
                    dtnameCombosSubmarca.Columns.Add("Categoria", typeof(String));
                    dtnameCombosSubmarca.Columns.Add("Marca", typeof(String));
                    dtnameCombosSubmarca.Columns.Add("Submarca", typeof(String));
                    dtnameCombosSubmarca.Columns.Add("Estado", typeof(String));

                    for (int i = 0; i <= GvConsultaSubmarca.Rows.Count - 1; i++)
                    {
                        try
                        {
                            DataRow dr = dtnameCombosSubmarca.NewRow();
                            dr["Categoria"] = ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[1].FindControl("cmbCateSubMarca")).SelectedItem.Text;
                            dr["Marca"] = ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[2].FindControl("cmbMarcaSubMarca")).SelectedItem.Text;
                            dr["Submarca"] = ((Label)GvConsultaSubmarca.Rows[i].Cells[3].FindControl("lblSubMarca")).Text;
                            dr["Estado"] = ((CheckBox)GvConsultaSubmarca.Rows[i].Cells[4].FindControl("ChecksubMarca")).Checked;
                            dtnameCombosSubmarca.Rows.Add(dr);
                        }
                        catch (Exception ex) { }
                    }

                    this.Session["CExporSubMarca"] = dtnameCombosSubmarca;
                    
                    //TxtCodSubMarca.Text = oeSubBrand.Rows[0]["id_SubBrand"].ToString().Trim();
                    //LlenacomboCategSubMarca();
                    //cmbCategoriaSubmarca.Text = oeSubBrand.Rows[0]["id_ProductCategory"].ToString().Trim();
                    //if (planningADM == "SI")
                    //{
                    //    combocomboMarcaenSubarcaporplanning();
                    //}
                    //else
                    //{
                    //    LlenacomboMarcaensubMarca();
                    //}                           
                }
                else
                {
                    SavelimpiarControlesSubMarca();
                    saveActivarbotonesSubMarca();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La consulta realizada no devolvió ninguna respuesta";
                    MensajeAlerta();
                    IbtnSubBrand.Show();
                }
            }

        }
        protected void BtnCancelSubBrand_Click(object sender, EventArgs e)
        {
            saveActivarbotonesSubMarca();
            desactivarControlesSubMarca();
            SavelimpiarControlesSubMarca();
        }
        protected void cmbCateSubMarca_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LlenacomboGVMarcaensubMarca(GvConsultaSubmarca.EditIndex);
            ModalpConsGVSubmarca.Show();
        }
        protected void GvConsultaSubmarca_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            ModalpConsGVSubmarca.Show();
            btncgvCSubmarca.Visible = false;
            GvConsultaSubmarca.EditIndex = e.NewEditIndex;
            string Categoria, Marca, SubMarca;
            bool estado;
            
            Categoria = ((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[1].FindControl("cmbCateSubMarca")).Text;
            SubMarca = ((Label)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("lblSubMarca")).Text;
            Marca = ((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).Text;
            estado = ((CheckBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[4].FindControl("ChecksubMarca")).Checked;

            DataTable oecSubcategoria = (DataTable)this.Session["CSubMarca"];
            GvConsultaSubmarca.DataSource = oecSubcategoria;
            GvConsultaSubmarca.DataBind();

            LlenacomboGVCategSubMarca(GvConsultaSubmarca.EditIndex);
            ((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[1].FindControl("cmbCateSubMarca")).Text = oecSubcategoria.Rows[GvConsultaSubmarca.EditIndex][8].ToString().Trim();
            LlenacomboGVMarcaensubMarca(GvConsultaSubmarca.EditIndex);
            ((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).Text = oecSubcategoria.Rows[GvConsultaSubmarca.EditIndex][2].ToString().Trim();

            LlenacomboGVCategSubMarca(GvConsultaSubmarca.EditIndex);
            ((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[1].FindControl("cmbCateSubMarca")).Items.FindByValue(Categoria).Selected = true;
            LlenacomboGVMarcaensubMarca(GvConsultaSubmarca.EditIndex);
            ((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).Items.FindByValue(Marca).Selected = true;
            ((TextBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("txtSubMarca")).Text = SubMarca;
            ((CheckBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[4].FindControl("ChecksubMarca")).Checked = estado;

            this.Session["rept"] = ((TextBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("txtSubMarca")).Text;
            this.Session["rept1"] = ((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).Text;
        }

        protected void GvConsultaSubmarca_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            btncgvCSubmarca.Visible = true;
            GvConsultaSubmarca.EditIndex = -1;
            DataTable oeSubBrand = oSubBrand.SearchSubBrand(this.Session["scodSubBrand"].ToString().Trim(), Convert.ToInt32(this.Session["icodBrand"].ToString().Trim()), this.Session["sSubBrand"].ToString().Trim());
            this.Session["CSubMarca"] = oeSubBrand;
            if (oeSubBrand != null)
            {
                if (oeSubBrand.Rows.Count > 0)
                {
                    GvConsultaSubmarca.DataSource = oeSubBrand;
                    GvConsultaSubmarca.DataBind();
                    ModalpConsGVSubmarca.Show();

                    for (int i = 0; i <= oeSubBrand.Rows.Count - 1; i++)
                    {
                        LlenacomboGVCategSubMarca(i);
                        ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[1].FindControl("cmbCateSubMarca")).Text = oeSubBrand.Rows[i][8].ToString().Trim();
                        LlenacomboGVMarcaensubMarca(i);
                        ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[2].FindControl("cmbMarcaSubMarca")).Text = oeSubBrand.Rows[i][2].ToString().Trim();
                    }
                }
            }
        }

        protected void GvConsultaSubmarca_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            //ModalpConsGVSubmarca.Show();
            btncgvCSubmarca.Visible = true;
            if (((CheckBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[4].FindControl("ChecksubMarca")).Checked != false)
            {
                estado = true;
            }
            else
            {
                estado = false;
                DAplicacion oddeshabSubBrand = new DAplicacion();
                DataTable dt = oddeshabSubBrand.PermitirDeshabilitar(ConfigurationManager.AppSettings["SubBrandProducts"], ((Label)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[0].FindControl("LblsubCategory")).Text);
                if (dt != null)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Perfil, por favor Verifique";
                    MensajeAlerta();
                    return;
                }
            }
            LblFaltantes.Text = "";
            //TxtNomSubMarca.Text = TxtNomSubMarca.Text.TrimStart();
            if (((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[1].FindControl("cmbCateSubMarca")).Text == "0" || ((TextBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("txtSubMarca")).Text == "" || ((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).Text == "0")
            {
                if (((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[1].FindControl("cmbCateSubMarca")).Text == "")
                {
                    LblFaltantes.Text = ". " + "Categoria";
                }
                if (((TextBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("txtSubMarca")).Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Submarca";
                }
                if (((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).Text == "0")
                {
                    LblFaltantes.Text = ". " + "Marca";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {

                repetido = Convert.ToString(this.Session["rept"]);
                repetido1 = Convert.ToString(this.Session["rept1"]);
                if (repetido != ((TextBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("txtSubMarca")).Text || repetido1 != ((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).Text)
                {
                    DAplicacion odconsulSubBrand = new DAplicacion();
                    DataTable dtconsulta = odconsulSubBrand.ConsultaDuplicados(ConfigurationManager.AppSettings["SubBrand"], ((TextBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("txtSubMarca")).Text, ((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).Text, null);
                    if (dtconsulta == null)
                    {

                        ESubBrand oeaSubBrand = oSubBrand.Actualizar_SubBrand(Convert.ToInt32(((Label)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[0].FindControl("LblsubCategory")).Text), ((TextBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("txtSubMarca")).Text, Convert.ToInt32(((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).Text), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        ESubBrand oeSubBrandtmp = oSubBrand.Actualizar_SubBrandtmp(Convert.ToInt32(((Label)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[0].FindControl("LblsubCategory")).Text), ((TextBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("txtSubMarca")).Text, Convert.ToInt32(((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).Text), estado);
                        //sSubBrand = ((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).SelectedItem.Text + " " + ((TextBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("txtSubMarca")).Text;
                        //this.Session["sSubBrand"] = sSubBrand;
                        SavelimpiarControlesSubMarca();
                        GvConsultaSubmarca.EditIndex = -1;
                        //DataTable oeSubBrand = oSubBrand.SearchSubBrand(this.Session["scodSubBrand"].ToString().Trim(), Convert.ToInt32(this.Session["icodBrand"].ToString().Trim()), this.Session["sSubBrand"].ToString().Trim());
                        DataTable oeSubBrand = (DataTable)this.Session["CSubMarca"];
                        if (oeSubBrand != null)
                        {
                            if (oeSubBrand.Rows.Count > 0)
                            {
                                GvConsultaSubmarca.DataSource = oeSubBrand;
                                GvConsultaSubmarca.DataBind();
                                ModalpConsGVSubmarca.Show();

                                for (int i = 0; i <= oeSubBrand.Rows.Count - 1; i++)
                                {
                                    LlenacomboGVCategSubMarca(i);
                                    ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[1].FindControl("cmbCateSubMarca")).Text = oeSubBrand.Rows[i][8].ToString().Trim();
                                    LlenacomboGVMarcaensubMarca(i);
                                    ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[2].FindControl("cmbMarcaSubMarca")).Text = oeSubBrand.Rows[i][2].ToString().Trim();
                                }
                            }
                        }
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La SubMarca de Producto : " + this.Session["sSubBrand"] + " fue Actualizado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesSubMarca();
                        desactivarControlesSubMarca();
                    }
                    else
                    {
                        //sSubBrand = CmbSelBrand.SelectedItem.Text + " " + TxtNomSubMarca.Text;
                        //this.Session["sSubBrand"] = sSubBrand;
                        GvConsultaSubmarca.EditIndex = -1;
                        //DataTable oeSubBrand = oSubBrand.SearchSubBrand(this.Session["scodSubBrand"].ToString().Trim(), Convert.ToInt32(this.Session["icodBrand"].ToString().Trim()), this.Session["sSubBrand"].ToString().Trim());
                        //this.Session["CSubMarca"] = oeSubBrand;
                        DataTable oeSubBrand = (DataTable)this.Session["CSubMarca"];
                        if (oeSubBrand != null)
                        {
                            if (oeSubBrand.Rows.Count > 0)
                            {
                                GvConsultaSubmarca.DataSource = oeSubBrand;
                                GvConsultaSubmarca.DataBind();
                                ModalpConsGVSubmarca.Show();

                                for (int i = 0; i <= oeSubBrand.Rows.Count - 1; i++)
                                {
                                    LlenacomboGVCategSubMarca(i);
                                    ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[1].FindControl("cmbCateSubMarca")).Text = oeSubBrand.Rows[i][8].ToString().Trim();
                                    LlenacomboGVMarcaensubMarca(i);
                                    ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[2].FindControl("cmbMarcaSubMarca")).Text = oeSubBrand.Rows[i][2].ToString().Trim();
                                }
                            }
                        }
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La SubMarca de Producto : " + this.Session["sSubBrand"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {
                    ESubBrand oeaSubBrand = oSubBrand.Actualizar_SubBrand(Convert.ToInt32(((Label)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[0].FindControl("LblsubCategory")).Text), ((TextBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("txtSubMarca")).Text, Convert.ToInt32(((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).Text), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    ESubBrand oeSubBrandtmp = oSubBrand.Actualizar_SubBrandtmp(Convert.ToInt32(((Label)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[0].FindControl("LblsubCategory")).Text), ((TextBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("txtSubMarca")).Text, Convert.ToInt32(((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).Text), estado);
                    //scSubBrand = ((DropDownList)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[2].FindControl("cmbMarcaSubMarca")).SelectedItem.Text + " " + ((TextBox)GvConsultaSubmarca.Rows[GvConsultaSubmarca.EditIndex].Cells[3].FindControl("txtSubMarca")).Text;
                    //this.Session["scSubBrand"] = scSubBrand;
                    SavelimpiarControlesSubMarca();
                    GvConsultaSubmarca.EditIndex = -1;
                    //DataTable oeSubBrand = oSubBrand.SearchSubBrand(this.Session["scodSubBrand"].ToString().Trim(), Convert.ToInt32(this.Session["icodBrand"].ToString().Trim()), this.Session["sSubBrand"].ToString().Trim());
                    //this.Session["CSubMarca"] = oeSubBrand;
                    DataTable oeSubBrand = (DataTable)this.Session["CSubMarca"];
                    if (oeSubBrand != null)
                    {
                        if (oeSubBrand.Rows.Count > 0)
                        {
                            GvConsultaSubmarca.DataSource = oeSubBrand;
                            GvConsultaSubmarca.DataBind();
                            ModalpConsGVSubmarca.Show();

                            for (int i = 0; i <= oeSubBrand.Rows.Count - 1; i++)
                            {
                                LlenacomboGVCategSubMarca(i);
                                ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[1].FindControl("cmbCateSubMarca")).Text = oeSubBrand.Rows[i][8].ToString().Trim();
                                LlenacomboGVMarcaensubMarca(i);
                                ((DropDownList)GvConsultaSubmarca.Rows[i].Cells[2].FindControl("cmbMarcaSubMarca")).Text = oeSubBrand.Rows[i][2].ToString().Trim();
                            }
                        }
                    }
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La SubMarca de Producto : " + this.Session["sSubBrand"] + " fue actualizada con éxito";
                    MensajeAlerta();
                    saveActivarbotonesSubMarca();
                    desactivarControlesSubMarca();
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

        #region Categoria
        protected void BtnCrearProductType_Click(object sender, EventArgs e)
        {
            SavelimpiarControlesCategoria();
            crearActivarbotonesCategoria();
            activarControlesCategoria();
            BtnCargaMasivaCate.Visible = false;
        }

        private void cancelarCat()
        {
            saveActivarbotonesCategoria();
            desactivarControlesCategoria();
            SavelimpiarControlesCategoria();
            BtnCargaMasivaCate.Visible = true;
        }

        protected void BtnSaveProductType_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomProductType.Text = TxtNomProductType.Text.Trim();
            if (cmb_categorias_cliente.Text == "0" || TxtNomProductType.Text == "")
            {
                LblFaltantes.Text = "Debe ingresar los campos con: ";
                if (cmb_categorias_cliente.Text == "0")
                {
                    LblFaltantes.Text += ("Cliente" + " . ");
                }
                if (TxtNomProductType.Text == "")
                {
                    LblFaltantes.Text += ("Categoría de producto" + " . ");                    
                }
                Alertas.CssClass = "MensajesError";
                MensajeAlerta();
                return;
            }

            try
            {               
                DAplicacion odconsulProductCategory = new DAplicacion();
                DataTable dtconsulta = odconsulProductCategory.ConsultaDuplicados(ConfigurationManager.AppSettings["ProductCategory"], TxtNomProductType.Text, cmb_categorias_cliente.SelectedValue.ToString(), null);
                if (dtconsulta == null)
                {
                    EProduct_Type oeProductType = oProductType.RegistrarProductcategory(TxtCodProductType.Text, TxtNomProductType.Text, TxtgroupCategory.Text, true, cmb_categorias_cliente.SelectedValue.ToString().Trim(), Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    EProduct_Type oeProductTypeTMP = oProductType.RegistrarProductcategoryTMP(oeProductType.id_Product_Category, oeProductType.Product_Category, oeProductType.Product_Category_Status);
                    this.Session["sProductType"] = TxtNomProductType.Text;  
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Categoría de producto " + TxtNomProductType.Text + ", fue creada con éxito";
                    cancelarCat();
                    MensajeAlerta();
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La Categoría  de producto " + TxtNomProductType.Text + ", ya existe";
                    cancelarCat();
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

        protected void BtnBTypeProduct_Click(object sender, EventArgs e)
        {
            desactivarControlesCategoria();
            LblFaltantes.Text = "";
            TxtBCodTypeProduct.Text = TxtBCodTypeProduct.Text.Trim();
            TxtBNomTypeProduct.Text = TxtBNomTypeProduct.Text.Trim();
            scompany_id = cmb_Cliente.SelectedValue.ToString().Trim();
            
            if (TxtBCodTypeProduct.Text == "" && TxtBNomTypeProduct.Text == "" && cmb_Cliente.Text == "0")
            {
                this.Session["mensajealert"] = "Código y/o nombre de Categoría de producto y/o Cliente";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parámetro de consulta";
                cancelarCat();
                MensajeAlerta();
                IbtnProductType.Show();
                return;
            }
            BuscarActivarbotnesCategoria();
            scodProductType = TxtBCodTypeProduct.Text;
            sproductType = TxtBNomTypeProduct.Text;
            TxtBCodTypeProduct.Text = "";
            TxtBNomTypeProduct.Text = "";
            cmb_Cliente.Text = "0";
            this.Session["scodProductType"] = scodProductType;
            this.Session["sproductType"] = sproductType;
            this.Session["scompany_id"] = scompany_id;
            DataTable oeProductType = oProductType.SearchProductCategory(scodProductType, sproductType, scompany_id);
            this.Session["tProductType"] = oeProductType;
            if (oeProductType != null)
            {
                if (oeProductType.Rows.Count > 0)
                {
                    gridbuscarCategoria(oeProductType);
                }
                       
                else
                {
                    CancelarCat();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La consulta realizada no devolvió ninguna respuesta";
                    MensajeAlerta();
                    IbtnProductType.Show();
                }               
            }
            this.Session["Exportar_Excel"] = "Exportar_Categorias";

            DataTable dtnameCaategoria = new DataTable();
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

        private void gridbuscarCategoria(DataTable oeProductType)
        {
            GVConsultaCategoria.EditIndex = -1;
            GVConsultaCategoria.DataSource = null;
            GVConsultaCategoria.DataSource = oeProductType;
            GVConsultaCategoria.DataBind();
            MopopConsulCate.Show();
        }
        
        protected void BtnCancelProductType_Click(object sender, EventArgs e)
        {
            saveActivarbotonesCategoria();
            desactivarControlesCategoria();
            SavelimpiarControlesCategoria();
            BtnCargaMasivaCate.Visible = true;
        }

        private void CancelarCat()
        {
            saveActivarbotonesCategoria();
            desactivarControlesCategoria();
            SavelimpiarControlesCategoria();
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
        
        protected void GVConsultaCategoria_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            LblFaltantes.Text = "";

            ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("TxtNomProductType")).Text = ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("TxtNomProductType")).Text.Trim();
            ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[2].FindControl("TxtgroupCategory")).Text = ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[2].FindControl("TxtgroupCategory")).Text.Trim();
            this.Session["sProductType"] = ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("TxtNomProductType")).Text;
            this.Session["scompany_id"] = ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[3].FindControl("cmbCliente_Edit")).Text.Trim();
            if (((CheckBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[3].FindControl("CheckECategoria")).Checked != false)
            {
                estado = true;
            }
            else
            {
                estado = false;
                DAplicacion oddeshabProductType = new DAplicacion();
                DataTable dt = oddeshabProductType.PermitirDeshabilitar(ConfigurationManager.AppSettings["ProductCategoryProduct_Tipo"], ((Label)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("LblCodProductType")).Text);
                if (dt != null)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No se puede deshabilitar este registro ya que existe relación en el maestro de Perfil, por favor verifique";
                    MensajeAlerta();
                    return;
                }
            }

            if (((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("TxtNomProductType")).Text == "" || ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[3].FindControl("cmbCliente_Edit")).Text == "0")
            {
                LblFaltantes.Text = "Debe ingresar los campos: ";
                if (((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("TxtNomProductType")).Text == "")
                {
                    LblFaltantes.Text += ("Nombre de producto" + " . " );
                }
                if (((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[3].FindControl("cmbCliente_Edit")).Text == "0")
                {
                    LblFaltantes.Text += ("Cliente" + " . " );
                }
                Alertas.CssClass = "MensajesError";                
                CancelarCat();
                MensajeAlerta();
                return;
            }
            try
            {
                repetido = Convert.ToString(this.Session["rept"]);
                repetido1 = Convert.ToString(this.Session["rept1"]);
                repetido2 = Convert.ToString(this.Session["rept2"]);
                if (repetido != ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("TxtNomProductType")).Text || repetido1 != ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[2].FindControl("TxtgroupCategory")).Text || repetido2 != ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[3].FindControl("cmbCliente_Edit")).Text )
                {
                    DAplicacion odconsulProductType = new DAplicacion();
                    DataTable dtconsulta = odconsulProductType.ConsultaDuplicados(ConfigurationManager.AppSettings["ProductCategory"], ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("TxtNomProductType")).Text, ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[3].FindControl("cmbCliente_Edit")).Text, ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[2].FindControl("TxtgroupCategory")).Text);
                    if (dtconsulta == null)
                    {
                        EProduct_Type oeaProductType = oProductType.Actualizar_ProductCategory(((Label)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("LblCodProductType")).Text, ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("TxtNomProductType")).Text, ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("TxtgroupCategory")).Text, ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("cmbCliente_Edit")).Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        EProduct_Type oeaProductTypetmp = oProductType.Actualizar_ProductCategorytmp(((Label)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("LblCodProductType")).Text, ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("TxtNomProductType")).Text, estado);
                        GVConsultaCategoria.EditIndex = -1;
                        DataTable oeProductType = oProductType.SearchProductCategory(this.Session["scodProductType"].ToString().Trim(), this.Session["sproductType"].ToString().Trim(), this.Session["scompany_id"].ToString().Trim());                     
                        this.Session["tProductType"] = oeProductType;
                        if (oeProductType != null)
                        {
                            if (oeProductType.Rows.Count > 0)
                            {
                                gridbuscarCategoria(oeProductType);
                            }                          
                        }
                        IbtnProductType.Hide();
                        btnCCategoria.Visible = true;
                        MopopConsulCate.Show();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La categoría de Producto : " + this.Session["sProductType"] + " fue actualizada con éxito";
                        MensajeAlerta();
                        saveActivarbotonesCategoria();
                    }
                    else
                    {
                        this.Session["tProductType"] = ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[1].FindControl("TxtNomProductType")).Text;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La categoria de Producto : " + this.Session["tProductType"] + " Ya Existe";
                        cancelarCat();
                        MensajeAlerta();
                    }
                }
                else
                {
                    EProduct_Type oeaProductType = oProductType.Actualizar_ProductCategory(((Label)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("LblCodProductType")).Text, ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("TxtNomProductType")).Text, ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("TxtgroupCategory")).Text, ((DropDownList)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("cmbCliente_Edit")).Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    EProduct_Type oeaProductTypetmp = oProductType.Actualizar_ProductCategorytmp(((Label)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("LblCodProductType")).Text, ((TextBox)GVConsultaCategoria.Rows[GVConsultaCategoria.EditIndex].Cells[0].FindControl("TxtNomProductType")).Text, estado);
                    SavelimpiarControlesCategoria();
                    GVConsultaCategoria.EditIndex = -1;
                    DataTable oeProductType = oProductType.SearchProductCategory(this.Session["scodProductType"].ToString().Trim(), this.Session["sproductType"].ToString().Trim(), this.Session["scompany_id"].ToString().Trim());
                    this.Session["tProductType"] = oeProductType;
                    if (oeProductType != null)
                    {
                        if (oeProductType.Rows.Count > 0)
                        {
                            GVConsultaCategoria.DataSource = oeProductType;
                            GVConsultaCategoria.DataBind();
                            MopopConsulCate.Show();

                        }
                    }

                    try
                    { 
                        this.planningADM = this.Session["AdmProd"].ToString().Trim();
                    }
                    catch
                    {
                    }

                    if (this.planningADM == "SI")
                    {
                        LLenacomboCategoriaBuscarMarcaporplanning();
                    }
                    else
                    {
                        llenaComboCategoriaBuscarMarca();
                    }
            
                    btnCCategoria.Visible = true;
                    MopopConsulCate.Show();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La categoría de Producto : " + this.Session["sProductType"] + " fue actualizada con éxito";
                    MensajeAlerta();
                    IbtnProductType.Hide();
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

        #region SubCategoria

        protected void BtnCrearSubCategory_Click(object sender, EventArgs e)
        {
            try
            {
                this.planningADM = this.Session["AdmProd"].ToString().Trim();
            }
            catch
            {
            }

            if (this.planningADM == "SI")
                llenar_comboBCategoSubCategoPlanning();
            else
                comboCategSubCategoria();

            crearActivarbotonesSubCategoria();
            activarControlesSubCategoria();
        }

        protected void BtnGuardarSubCategory_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomSubCategory.Text = TxtNomSubCategory.Text.TrimStart().TrimEnd();
            this.Session["sSubCategoria"] = TxtNomSubCategory.Text;

            string sidCategoira = cmbCateSubCategoria.Text;
            if (TxtNomSubCategory.Text == "" || cmbCateSubCategoria.Text == "0")
            {
                if (TxtNomSubCategory.Text == "")
                {
                    LblFaltantes.Text = ". " + "SubCategoría de producto";
                }
                if (cmbCateSubCategoria.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Categoría de producto";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {

                DAplicacion odconsulSubCategoria = new DAplicacion();
                DataTable dtconsulta = odconsulSubCategoria.ConsultaDuplicados(ConfigurationManager.AppSettings["Product_SubCategory"], TxtNomSubCategory.Text, cmbCateSubCategoria.Text, null);
                if (dtconsulta == null)
                {
                    ESubCategoria oeSubCategoria = oSubCategoria.RegistrarSubCategoria(cmbCateSubCategoria.Text, TxtNomSubCategory.Text,  true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sSubCategoria = "";
                    //sSubCategoria = cmbCateSubCategoria.SelectedItem.Text + " " + TxtNomSubCategory.Text;
                    sSubCategoria = TxtNomSubCategory.Text;
                    this.Session["sSubCategoria"] = sSubCategoria;
                    SavelimpiarControlesSubCategoria();
                    LlenacomboCategoConsulta();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La SubCategoría de Producto " + this.Session["sSubCategoria"] + " fue creada con éxito";
                    MensajeAlerta();
                    saveActivarbotonesSubCategoria();
                    desactivarControlesSubCategoria();                    
                }
                else
                {
                    string sSubCategoria = "";
                    sSubCategoria = cmbCateSubCategoria.SelectedItem.Text + " " + TxtNomSubCategory.Text;
                    this.Session["sSubCategoria"] = sSubCategoria;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La SubCategoría de Producto " + this.Session["sSubCategoria"] + " ya existe";
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

        protected void BtnBusSubCategory_Click(object sender, EventArgs e)
        {
            desactivarControlesSubCategoria();
            LblFaltantes.Text = "";
            TxtBNomSubCategory.Text = TxtBNomSubCategory.Text.TrimStart();

            if (TxtBNomSubCategory.Text == "" && cmbBCategoriaSC.Text == "0")
            {
                this.Session["mensajealert"] = "Nombre de SubSybCategoria y/o Categoria de Producto";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parámetro de consulta";
                MensajeAlerta();
                ModalPopupSubCategoria.Show();
                return;
            }

            BuscarActivarbotnesSubCategoria();
            sSubCategoria = TxtBNomSubCategory.Text;
            sCodCategoria = cmbBCategoriaSC.Text;
            TxtBNomSubCategory.Text = "";
            cmbBCategoriaSC.Text = "0";

            this.Session["sCodCategoria"] = sCodCategoria;
            this.Session["sSubCategoria"] = sSubCategoria;

            DataTable oecateSubCategoria = oSubCategoria.ConsultarSubCategoria(sCodCategoria, sSubCategoria);
            this.Session["tSubcategoria"] = oecateSubCategoria;
            if (oecateSubCategoria != null)
            {
                if (oecateSubCategoria.Rows.Count > 0)
                {
                    GvConsultaSubcategoria.DataSource = oecateSubCategoria;
                    GvConsultaSubcategoria.DataBind();
                    ModalCoSub.Show();
                }
                else
                {
                    SavelimpiarControlesSubCategoria();
                    saveActivarbotonesSubCategoria();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    ModalPopupSubCategoria.Show();
                }

                this.Session["Exportar_Excel"] = "Exportar_SubCategorias";

                DataTable dtnameSubCaategoria = new DataTable();
                dtnameSubCaategoria.Columns.Add("Código", typeof(String));
                dtnameSubCaategoria.Columns.Add("Categoria", typeof(String));
                dtnameSubCaategoria.Columns.Add("SubCategoria", typeof(String));
                dtnameSubCaategoria.Columns.Add("Estado", typeof(String));

                for (int i = 0; i <= GvConsultaSubcategoria.Rows.Count - 1; i++)
                {

                    DataRow dr = dtnameSubCaategoria.NewRow();
                    dr["Código"] = ((Label)GvConsultaSubcategoria.Rows[i].Cells[0].FindControl("LblsubCategory")).Text;
                    dr["Categoria"] = ((Label)GvConsultaSubcategoria.Rows[i].Cells[1].FindControl("LblCatsubcategoria")).Text;
                    dr["Subcategoria"] = ((Label)GvConsultaSubcategoria.Rows[i].Cells[2].FindControl("lblSubcategoria")).Text;
                    dr["Estado"] = ((CheckBox)GvConsultaSubcategoria.Rows[i].Cells[3].FindControl("Chesubcategoria")).Checked;
                    dtnameSubCaategoria.Rows.Add(dr);
                }

                this.Session["CExporSubCategoria"] = dtnameSubCaategoria;  
             }
            
        }       
        protected void BtnCancelarSubCategory_Click(object sender, EventArgs e)
        {
            saveActivarbotonesSubCategoria();
            desactivarControlesSubCategoria();
            SavelimpiarControlesSubCategoria();
        }

        protected void GvConsultaSubcategoria_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            BtncancelsubC.Visible = false;
            //btn_img_exporttoexcel.Visible = false;
            ModalCoSub.Show();
            GvConsultaSubcategoria.EditIndex = e.NewEditIndex;
            string Codigo, Categoria, Subcategoria;
            bool estado;
            Codigo = ((Label)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[0].FindControl("LblsubCategory")).Text;
            Categoria = ((Label)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[0].FindControl("LblCatsubcategoria")).Text;
            Subcategoria = ((Label)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[0].FindControl("lblSubcategoria")).Text;
            estado = ((CheckBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[0].FindControl("Chesubcategoria")).Checked;
            GvConsultaSubcategoria.DataSource = (DataTable)this.Session["tSubcategoria"];
            GvConsultaSubcategoria.DataBind();

            ((Label)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[0].FindControl("LblsubCategory")).Text = Codigo;
            LlenacomboConsultaCategoriaSubcategoria();
            ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).Items.FindByText(Categoria).Selected = true;
            ((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[2].FindControl("TxtSubcategoria")).Text = Subcategoria;
            ((CheckBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[3].FindControl("Chesubcategoria")).Checked = estado;
            this.Session["rept"] = ((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[0].FindControl("TxtSubcategoria")).Text;
            this.Session["rept1"] = ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[0].FindControl("cmbCateSubCategory")).Text;
        }

        protected void GvConsultaSubcategoria_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            GvConsultaSubcategoria.EditIndex = -1;
            GvConsultaSubcategoria.DataSource = (DataTable)this.Session["tSubcategoria"];
            GvConsultaSubcategoria.DataBind();
            BtncancelsubC.Visible = true;
            ModalCoSub.Show();
        }

        protected void GvConsultaSubcategoria_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            LblFaltantes.Text = "";
            ((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[2].FindControl("TxtSubcategoria")).Text = ((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[2].FindControl("TxtSubcategoria")).Text.TrimStart();
            ((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[2].FindControl("TxtSubcategoria")).Text = ((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[2].FindControl("TxtSubcategoria")).Text.TrimEnd();
            this.Session["sSubCategoria"] = ((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[2].FindControl("TxtSubcategoria")).Text;
            TxtNomSubCategory.Text = TxtNomSubCategory.Text.TrimStart();
            BtncancelsubC.Visible = true;

            if (((CheckBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[3].FindControl("Chesubcategoria")).Checked != false)
            {
                estado = true;
            }
            else
            {
                estado = false;
                DAplicacion odconsulSubCategoria = new DAplicacion();
                DataTable dtconsulta = odconsulSubCategoria.PermitirDeshabilitar(ConfigurationManager.AppSettings["Product_SubCategory"], ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[0].FindControl("cmbCateSubCategory")).Text);
                if (dtconsulta != null)
                {

                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Perfil, por favor Verifique";
                    MensajeAlerta();
                    return;
                }
            }

            if (((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[2].FindControl("TxtSubcategoria")).Text == "" || ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).Text == "0")
            {
                if (((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[2].FindControl("TxtSubcategoria")).Text == "")
                {
                    LblFaltantes.Text = ". " + "SubCategoria de producto";
                }
                if (((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Categoria de producto";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {               
                repetido = Convert.ToString(this.Session["rept"]);
                repetido1 = Convert.ToString(this.Session["rept1"]);
                if (repetido != ((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[2].FindControl("TxtSubcategoria")).Text || repetido1 != ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).Text)
                {
                    DAplicacion odconsulSubCategoria = new DAplicacion();
                    DataTable dtconsulta = odconsulSubCategoria.ConsultaDuplicados(ConfigurationManager.AppSettings["Product_SubCategory"], ((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[2].FindControl("TxtSubcategoria")).Text, ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).Text, null);
                    if (dtconsulta == null)
                    {
                        
                        ESubCategoria oeSubCategoria = oSubCategoria.Actualizar_SubCategoria(Convert.ToInt64(((Label)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[0].FindControl("LblsubCategory")).Text), ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).Text, ((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[2].FindControl("TxtSubcategoria")).Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        //sSubCategoria = cmbCateSubCategoria.SelectedItem.Text + " " + TxtNomSubCategory.Text;
                        //this.Session["sSubCategoria"] = sSubCategoria;
                        SavelimpiarControlesSubCategoria();
                        LlenacomboCategoConsulta();
                        GvConsultaSubcategoria.EditIndex = -1;
                        DataTable oecateSubCategoria = oSubCategoria.ConsultarSubCategoria(this.Session["sCodCategoria"].ToString().Trim(), this.Session["sSubCategoria"].ToString().Trim());
                        this.Session["tSubcategoria"] = oecateSubCategoria;
                        if (oecateSubCategoria != null)
                        {
                            if (oecateSubCategoria.Rows.Count > 0)
                            {
                                GvConsultaSubcategoria.DataSource = oecateSubCategoria;
                              GvConsultaSubcategoria.DataBind();
                              ModalCoSub.Show();
                            }
                        }                     
                   
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La SubCategoria de Producto : " + this.Session["sSubCategoria"] + " fue Actualizado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesSubCategoria();
                        desactivarControlesSubCategoria();



                    }
                    else
                    {
                        sSubCategoria = cmbCateSubCategoria.SelectedItem.Text + " " + TxtNomSubCategory.Text;
                        this.Session["sSubCategoria"] = sSubCategoria;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La SubCategoria de Producto : " + this.Session["sSubCategoria"] + " Ya Existe";
                        MensajeAlerta();
                    }
                }
                else
                {

                    ESubCategoria oeSubCategoria = oSubCategoria.Actualizar_SubCategoria(Convert.ToInt64(((Label)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[0].FindControl("LblsubCategory")).Text), ((DropDownList)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[1].FindControl("cmbCateSubCategory")).Text, ((TextBox)GvConsultaSubcategoria.Rows[GvConsultaSubcategoria.EditIndex].Cells[2].FindControl("TxtSubcategoria")).Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    //sSubCategoria = cmbCateSubCategoria.SelectedItem.Text + " " + TxtNomSubCategory.Text;
                    //this.Session["sSubCategoria"] = sSubCategoria;
                    SavelimpiarControlesSubCategoria();
                    LlenacomboCategoConsulta();
                    GvConsultaSubcategoria.EditIndex = -1;
                    DataTable oecateSubCategoria = oSubCategoria.ConsultarSubCategoria(this.Session["sCodCategoria"].ToString().Trim(), this.Session["sSubCategoria"].ToString().Trim());
                    this.Session["tSubcategoria"] = oecateSubCategoria;
                    if (oecateSubCategoria != null)
                    {
                        if (oecateSubCategoria.Rows.Count > 0)
                        {
                            GvConsultaSubcategoria.DataSource = oecateSubCategoria;
                            GvConsultaSubcategoria.DataBind();
                            ModalCoSub.Show();
                        }
                    }   
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La SubCategoria de Producto : " + this.Session["sSubCategoria"] + " fue Actualizado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesSubCategoria();
                    desactivarControlesSubCategoria();

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

        #region Presentación de Producto
        protected void BtnCrearPresen_Click(object sender, EventArgs e)
        {
            try
            {
                this.planningADM = this.Session["AdmProd"].ToString().Trim();
            }
            catch
            {
            }

            if (this.planningADM == "SI")
                LLenacomboCategoriaPresentacionplanning();                   
            else 
                LlenacomboCategPresent();                   

            Llenacom_UnidadMedida();
            crearActivarbotonesPresent();
            activarControlesPresent();
       
        }
        protected void cmbCategoryPresent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.planningADM = this.Session["AdmProd"].ToString().Trim();
            }
            catch
            {
            }

            if (this.planningADM == "SI")
                combocomboMarcaenPresentaplanning();
            else
                LlenacomboMarcaPresent();
            
            LlenaSubporCategoPresent();
        }

        protected void cmbBCategoriaPresent_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.planningADM = this.Session["AdmProd"].ToString().Trim();
            }
            catch
            {
            }

            if (this.planningADM == "SI")
                combocomboConsultaMarcaenPresentaplanning();
            else
                llenar_comboBMarcapresent();

            IbtnPresen_ModalPopupExtender.Show();
         }

        protected void BtnSavePresen_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";
            TxtNomPresen.Text = TxtNomPresen.Text.TrimStart();

            if (cmbCategoryPresent.Text == "0" || TexEmpPresent.Text == "0" || TexUnidadEpresent.Text == "0" || cmbMarcaPresent.Text == "0" || TxtNomPresen.Text == "" || TxtConteNeto.Text == "" || CmbUnidNeto.Text == "0")
            {
               
                if (cmbCategoryPresent.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Categoria";
                }

                if (TexEmpPresent.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Empaquetamiento";
                }

                if (TexUnidadEpresent.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Unidades por empaque";
                } 
              
                if (cmbMarcaPresent.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Marca";
                }
                if (TxtNomPresen.Text == "")
                {
                    LblFaltantes.Text = ". " + "Presentación de producto";
                }
                if (TxtConteNeto.Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Contenido Neto";
                }
                if (CmbUnidNeto.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Unidad de medida";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {

                DAplicacion odconsulPresen = new DAplicacion();
                DataTable dtconsulta = odconsulPresen.ConsultaDuplicadosPresent(ConfigurationManager.AppSettings["Presentaciones"], cmbCategoryPresent.Text, Convert.ToInt64(cmbSubCategoryPresent.Text), Convert.ToInt32(cmbMarcaPresent.Text), TxtNomPresen.Text );
                if (dtconsulta == null)
                {
                    EProduct_Presentations oePresen = oProdPresent.RegistrarPresentation(TxtCodPresen.Text, cmbCategoryPresent.Text, Convert.ToInt64(cmbSubCategoryPresent.Text), Convert.ToInt32(cmbMarcaPresent.Text), TexEmpPresent.Text, TexUnidadEpresent.Text, TxtNomPresen.Text, Convert.ToDecimal(TxtConteNeto.Text), Convert.ToInt32(CmbUnidNeto.Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                   // EProduct_Presentations oePresentmp = oProdPresent.RegistrarPresentationTMP(TxtCodPresen.Text, TxtNomPresen.Text, Convert.ToDecimal(TxtConteNeto.Text), Convert.ToInt32(CmbUnidNeto.Text), true);
                    sPresent = TxtNomPresen.Text + " para " + cmbCategoryPresent.SelectedItem.Text;
                    this.Session["sPresent"] = sPresent;
                    SavelimpiarControlesPresent();
                    try
                    {
                        this.planningADM = this.Session["AdmProd"].ToString().Trim();
                    }
                    catch
                    {
                    }

                    if (this.planningADM == "SI")
                    {
                        llenar_comboBCategopresentPlanning();
                    }
                    else
                    {
                        llenar_comboBCategopresent();
                    }
                  
                  
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "La Presentación de Producto " + this.Session["sPresent"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesPresent();
                    desactivarControlesPresent();
                }
                else
                {
                    sPresent = TxtNomPresen.Text + " de " + TxtConteNeto.Text + " " + CmbUnidNeto.SelectedItem.Text;
                    this.Session["sPresent"] = sPresent;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "La Presentación de Producto " + this.Session["sPresent"] + " Ya Existe";
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
        protected void BtnBPresent_Click(object sender, EventArgs e)
        {   
            desactivarControlesPresent();
            LblFaltantes.Text = "";
            TxtBNomPresen.Text = TxtBNomPresen.Text.TrimStart();
          
            if (cmbBCategoriaPresent.Text == "0" &&  cmbBMarcaPresent.Text == "0" && TxtBNomPresen.Text == "")
            {
                this.Session["mensajealert"] = "Código y/o Nombre de Presentación de producto";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                IbtnPresen_ModalPopupExtender.Show();
                return;
            }

            BuscarActivarbotnesPresent();
            sPresent = TxtBNomPresen.Text;
            TxtBNomPresen.Text = "";
            string cmbmarca = "";
            if (cmbBMarcaPresent.Text == "")
            {
                cmbmarca = "0";
            }
            else
            {
                cmbmarca = cmbBMarcaPresent.Text;
            }
            DataTable oePresen = oProdPresent.BuscarPresentation(cmbBCategoriaPresent.Text, Convert.ToInt32(cmbmarca), sPresent);

            if (oePresen.Rows.Count > 0)
            {
                Session["Obj_Presentacion"] = oePresen;
                llena_grid_presentacion(oePresen);
                mpe_grid_presentacion.Show();
            }
            else 
            {
                this.Session["mensajealert"] = "No se encontraron registros";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "No se encontraron registros";
                MensajeAlerta();
                IbtnPresen_ModalPopupExtender.Show();
                return;
            }            
        }

        private void llena_grid_presentacion(DataTable oePresen) 
        {
            grid_bpresentacion.DataSource = oePresen;
            grid_bpresentacion.DataBind();
            //mpe_grid_presentacion.Show();
            //this.grid_bpresentacion.EditIndex = -1;
            int i = 0;
            foreach (GridViewRow dr in grid_bpresentacion.Rows)
            {
                String idcategory = oePresen.Rows[i][1].ToString().Trim();
                String idsubcategory = oePresen.Rows[i][2].ToString().Trim();
                int idbrand = Convert.ToInt32(oePresen.Rows[i][3].ToString().Trim());
                String idumeasure = oePresen.Rows[i][8].ToString().Trim();
                ((Label)dr.FindControl("lbl_bcategoria")).Text = oConn.ejecutarDataTable("UP_WEB_SEARCHPRODUCTCATEGORY", idcategory, "").Rows[0][1].ToString();
                ((Label)dr.FindControl("lbl_bsubcategoria")).Text = oConn.ejecutarDataTable("UP_WEBXPLORA_SEARCHSUBCATEGORY_BY_ID", idsubcategory).Rows[0][0].ToString();
                ((Label)dr.FindControl("lbl_bmarca")).Text = oConn.ejecutarDataTable("UP_WEB_SEARCHBRAND", idbrand, "0", "").Rows[0][3].ToString();
                ((Label)dr.FindControl("lbl_bumedida")).Text = oConn.ejecutarDataTable("UP_WEBXPLORA_SEARCHUMEDIDA_BY_ID", idumeasure).Rows[0][0].ToString();
                i++;
            }
        }

        //private void llenagridsubfamilias(DataTable data)
        //{
        //    grid_subfamily.DataSource = data;
        //    grid_subfamily.DataBind();
        //    foreach (GridViewRow dr in grid_subfamily.Rows)
        //    {
        //        DataTable nomfam = oConn.ejecutarDataTable("UP_WEBXPLORA_OBTENER_NOMBREFAMILIA", ((Label)dr.FindControl("lbl_bsubfam_family")).Text);
        //        ((Label)dr.FindControl("lbl_bsubfam_family")).Text = nomfam.Rows[0][5].ToString();
        //    }
        //}

        protected void grid_bpresentacion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            mpe_grid_presentacion.Show();
            btncancelgvpresent.Visible = false;
            grid_bpresentacion.EditIndex = e.NewEditIndex;
            int editado = grid_bpresentacion.EditIndex;
            string v_categoria, v_subcategoria, v_marca, v_umedida, v_empaque, v_uempaque, v_nombre, v_cneto;
            //bool estado;

            v_categoria = ((Label)grid_bpresentacion.Rows[editado].FindControl("lbl_bcategoria")).Text;
            v_subcategoria = ((Label)grid_bpresentacion.Rows[editado].FindControl("lbl_bsubcategoria")).Text;
            v_marca = ((Label)grid_bpresentacion.Rows[editado].FindControl("lbl_bmarca")).Text;

            v_empaque = ((Label)grid_bpresentacion.Rows[editado].FindControl("lbl_bempaque")).Text;
            v_uempaque = ((Label)grid_bpresentacion.Rows[editado].FindControl("lbl_buniemp")).Text;
            v_nombre = ((Label)grid_bpresentacion.Rows[editado].FindControl("lbl_bnpresent")).Text;
            v_cneto = ((Label)grid_bpresentacion.Rows[editado].FindControl("lbl_bcneto")).Text;
            v_umedida = ((Label)grid_bpresentacion.Rows[editado].FindControl("lbl_bumedida")).Text;
            grid_bpresentacion.DataSource = (DataTable)Session["Obj_Presentacion"];
            grid_bpresentacion.DataBind();
            llenacombo_bprecategoria(editado);
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bcategoria")).Items.FindByText(v_categoria).Selected = true;
            String cat = ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bcategoria")).Text;
            llenacombo_bpresubcategoria(editado,cat);
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bsubcategoria")).Items.FindByText(v_subcategoria).Selected = true;
            llenacombo_bpremarca(editado, cat);
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bmarca")).Items.FindByText(v_marca).Selected = true;

            ((TextBox)grid_bpresentacion.Rows[editado].FindControl("txt_bempaque")).Text = v_empaque;
            ((TextBox)grid_bpresentacion.Rows[editado].FindControl("txt_buniemp")).Text = v_uempaque;
            ((TextBox)grid_bpresentacion.Rows[editado].FindControl("txt_bnpresent")).Text = v_nombre;
            ((TextBox)grid_bpresentacion.Rows[editado].FindControl("txt_bcneto")).Text = v_cneto;
            llenacombo_bumedida(editado);
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bumedida")).Items.FindByText(v_umedida).Selected = true;


            this.Session["rep_pres"] = v_nombre;
            //this.Session["rept1"] = ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).Text;
            //this.Session["rept2"] = ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text;
        }

        private void llenacombo_bpremarca(int editado, string catego)
        {
            DataTable dt1 = new DataTable();
            dt1 = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOMARCASEGUNCATEGORIA", catego);
            //se llena Combo de marca en buscar de maestro familia de producto
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bmarca")).DataSource = dt1;
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bmarca")).DataTextField = "Name_Brand";
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bmarca")).DataValueField = "id_Brand";
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bmarca")).DataBind();
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bmarca")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            dt1 = null; 
        }

        private void llenacombo_bpresubcategoria(int editado, string catego)
        {
            DataTable dt1 = new DataTable();
            dt1 = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOSUBCATEGORIAPRESENT", catego);
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bsubcategoria")).DataSource = dt1;
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bsubcategoria")).DataTextField = "Name_Subcategory";
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bsubcategoria")).DataValueField = "id_Subcategory";
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bsubcategoria")).DataBind();
            // se modifica sp para q muestre el item seleccione desde sql para evitar error en consulta. Ing. Mauricio Ortiz 29/09/2010
            //cmbSubCategoryPresent.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            dt1 = null;      
        }

        private void llenacombo_bprecategoria(int editado)
        {
            DataTable dt1 = new DataTable();
            dt1 = oConn.ejecutarDataTable("UP_WEB_LLENACOMBOS", 13);
            //se llena categorias en tipo de producto
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bcategoria")).DataSource = dt1;
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bcategoria")).DataTextField = "Product_Category";
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bcategoria")).DataValueField = "id_ProductCategory";
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bcategoria")).DataBind();            
            dt1 = null;
        }

        private void llenacombo_bumedida(int editado)
        {
            DataTable dt1 = new DataTable();
            dt1 = oConn.ejecutarDataTable("UP_WEBXPOLORA_LLENACOMBOUMEDIDA");
            //se llena categorias en tipo de producto
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bumedida")).DataSource = dt1;
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bumedida")).DataTextField = "UnitOfMeasure_Name";
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bumedida")).DataValueField = "id_UnitOfMeasure";
            ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bumedida")).DataBind();
            dt1 = null;
        }

        protected void grid_bpresentacion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            LblFaltantes.Text = "";
            TxtBNomPresen.Text = TxtBNomPresen.Text.TrimStart();
            TxtBNomPresen.Text = TxtBNomPresen.Text.TrimEnd();

            try
            {
                //repetido = Convert.ToString(this.Session["rep_pres"]);
                int editado = grid_bpresentacion.EditIndex;
                string idcategoria = ((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bcategoria")).Text;                
                int idmarca = Convert.ToInt32(((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bmarca")).Text);
                string nompres = ((TextBox)grid_bpresentacion.Rows[editado].FindControl("txt_bnpresent")).Text;
                int duplicados = oProdPresent.BuscarPresentation("0", idmarca, nompres).Rows.Count;
                if (duplicados <= 1) //si no hay registros repetidos
                {
                    Product_Presentations product = new Product_Presentations();
                    EProduct_Presentations up_product = new EProduct_Presentations();
                    
                    up_product = product.ActualizarPresentation(            grid_bpresentacion.Rows[editado].Cells[0].Text,
                                                                            idcategoria,
                                                                            Convert.ToInt32(((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bsubcategoria")).Text),
                                                                            idmarca,
                                                                            ((TextBox)grid_bpresentacion.Rows[editado].FindControl("txt_bempaque")).Text,
                                                                            ((TextBox)grid_bpresentacion.Rows[editado].FindControl("txt_buniemp")).Text,
                                                                            nompres,
                                                                            Convert.ToDecimal(((TextBox)grid_bpresentacion.Rows[editado].FindControl("txt_bcneto")).Text),
                                                                            Convert.ToInt32(((DropDownList)grid_bpresentacion.Rows[editado].FindControl("ddl_bumedida")).Text),
                                                                            ((CheckBox)grid_bpresentacion.Rows[editado].FindControl("cbx_estado")).Checked,
                                                                             Convert.ToString(this.Session["sUser"]),
                                                                             DateTime.Now
                                                                            );
                        
                        //consultaGVMarca(oeBrand);
                        DataTable dt = (DataTable)Session["Obj_Presentacion"];
                        dt.Rows[editado][0] = up_product.id_ProductPresentation;
                        dt.Rows[editado][1] = up_product.id_ProductCategory;
                        dt.Rows[editado][2] = up_product.id_Subcategory;
                        dt.Rows[editado][3] = up_product.id_Brand;
                        dt.Rows[editado][4] = up_product.Empaquetamiento;
                        dt.Rows[editado][5] = up_product.Unidad_Empaque;
                        dt.Rows[editado][6] = up_product.ProductPresentationName;
                        dt.Rows[editado][7] = up_product.ProductPresentation_Neto;
                        dt.Rows[editado][8] = up_product.id_UnitOfMeasure;
                        dt.Rows[editado][9] = up_product.ProductPresentation_Status;

                        grid_bpresentacion.EditIndex = -1;
                        llena_grid_presentacion(dt);
                        mpe_grid_presentacion.Show();
                        btncancelgvpresent.Visible = true;
                        SavelimpiarControlesMarca();

                        if (this.planningADM == "SI")
                        {
                            combocomboMarcaenSubarcaporplanning();
                        }
                        else
                        {
                            LlenacomboMarcaensubMarca();
                        }
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Presentación de Producto : " + nompres + " se actualizó correctamente";
                        MensajeAlerta();
                    
                    //BuscarBrand.Visible = false;
                }
                else // si el nombre de la marca no ha cambiado
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No puede actualizar ya que existe una presentación con el nuevo nombre asignado. Por favor verifique";
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

        protected void grid_bpresentacion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grid_bpresentacion.EditIndex = -1;
            grid_bpresentacion.DataSource = ((DataTable)Session["Obj_Presentacion"]);
            btncancelgvpresent.Visible = true;
            mpe_grid_presentacion.Show();
        }

        protected void grid_bpresentacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void BtnCancelPresent_Click(object sender, EventArgs e)
        {
            saveActivarbotonesPresent();
            desactivarControlesPresent();
            SavelimpiarControlesPresent();
        }

        #endregion

        #region Productos
        protected void btnCrearProducto_Click(object sender, EventArgs e)
        {
            LlenacomboCategProducto(cmbTipoCateg);
            LlenacomboMarcaProduct(cmbFabricante);
            LlenacomboTipoProducto(cmbTipoProducto);
            LlenacomboFormatoProducto(cmbFormatoProducto);
            crearActivarbotonesProducto();
            activarControlesProducto();
            //llenacomboUnidadMedida();
            
        }
        protected void cmbTipoCateg_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaSubporCategoProduct(cmbTipoCateg.SelectedValue);
            //LlenacomboMarcaProduct();           
        }
        /*protected void cmbFabricante_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboCompanyProducto();
            comboSubmarcaProd();
            comboPresentacionProd();
            comboPFamiliaProd();
            //Nombreproducto();
            //llenarddl_gsubfamilia(ddl_psubfamily, "0");
            tooltipnomPresent();


            competencia_codAuto(cmbFabricante.SelectedValue);

        }*/
        public void competencia_codAuto(string idBrand)
        {
            DataSet dst = null;
            dst = oConn.ejecutarDataSet("UP_WEBXPLORA_AD_COD_COMPENTIA_ALICORP",idBrand);

            if (dst.Tables[0].Rows.Count>0)
            {
                TxtCodProducto.Enabled = false;

                string cod=dst.Tables[1].Rows[0][0].ToString();

                TxtCodProducto.Text = cod;
            }

        }
        protected void CmbSubCategoriaProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Nombreproducto();
            //tooltipnomPresent();
            LlenaFamiliaporSubCategoria(CmbSubCategoriaProduct.SelectedValue);
        }      
        protected void cmbPres_SelectedIndexChanged(object sender, EventArgs e)
        {
            tooltipnomPresent();
            ///se comentarea ya que se decide ingresar el peso y nombre del producto por el maestro y no al seleccionar la presentación.
            ///09/03/2011 Magaly Jiménez
            //try
            //{
            //    dt = oConn.ejecutarDataTable("UP_WEB_CONSULTAFACTOR", cmbPres.SelectedValue);
            //    //TxtFactor.Text = dt.Rows[0]["Factor"].ToString().Trim();
            //    if (TxtFactor.Text != "")
            //    {
            //        ////TxtFactor.Enabled = false;
            //        //// Calcula en peso del producto de acuerdo al factor de conversión a toneladas . Ing. Mauricio Ortiz  
            //        ////TxtPeso.Text = Convert.ToString(Convert.ToDecimal(TxtFactor.Text) * Convert.ToDecimal(dt.Rows[0]["Neto"].ToString().Trim()));
            //        ////Se agrega el campo unidad de Empaque para calcular el Peso Ing. Carlos Hernandez 13/10/2010
            //        //TxtPeso.Text = Convert.ToString(Convert.ToDecimal(dt.Rows[0]["Neto"].ToString().Trim()) * Convert.ToDecimal(dt.Rows[0]["Unidad_Empaque"].ToString().Trim()));
            //        /////  se quita división / Convert.ToDecimal(TxtFactor.Text)
            //        //TxtPeso.Enabled = false;
            //    }
            //    else
            //    {
            //        //TxtPeso.Text = "";
            //        //TxtFactor.Enabled = true;
            //        ////Activa el campo de factor de conversión debido a q no existe un factor comun de conversión para este producto . Ing. Mauricio Ortiz
            //        //TxtPeso.Enabled = false;
            //    }
            //    dt = null;
            //    //Nombreproducto();
            //    tooltipnomPresent();

            //}
            //catch
            //{
            //    TxtFactor.Text = "0";
            //    TxtPeso.Text = "0";
            //}
         
        }
        protected void BtnSaveProd_Click(object sender, EventArgs e)
        {
            if (cmbTipoCateg.SelectedValue == "0" 
                || cmbFabricante.SelectedValue == "0" 
                || TxtCodProducto.Text == "" 
                || TxtNomProducto.Text == "" 
                )
                {
                    LblFaltantes.Text = "";
                    if (cmbTipoCateg.Text == "0" ){
                        LblFaltantes.Text = LblFaltantes.Text + "Categoría" + Environment.NewLine;
                    }
                    if (cmbFabricante.Text == "0"){
                        LblFaltantes.Text = LblFaltantes.Text + "Marca" + Environment.NewLine;
                    }
                    if (TxtCodProducto.Text == ""){
                        LblFaltantes.Text = LblFaltantes.Text + "Código" + Environment.NewLine;
                    }
                    if (TxtNomProducto.Text == ""){
                        LblFaltantes.Text = LblFaltantes.Text + "Nombre de producto" + Environment.NewLine;
                    }
                    //Validación del Precio de Venta y Precio de Oferta
                    if (RadNumericTxtInfPrecioVenta.Value > 0 || RadNumericTxtInfPrecioOferta.Value > 0){
                        if (RadNumericTxtInfPrecioOferta.Value > RadNumericTxtInfPrecioVenta.Value){
                            LblFaltantes.Text = LblFaltantes.Text + "El precio de Oferta no puede ser mayor que el precio de Venta";
                        }
                    }

                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Debe ingresar todos los campos con (*) \n" + LblFaltantes.Text;
                    MensajeAlerta();
                    return;
                    
                }

                try
                {
                    MA_Producto oMA_Producto = new MA_Producto();
                    oMA_Producto.cod_categoria = cmbTipoCateg.SelectedValue;
                    oMA_Producto.cod_subcategoria = CmbSubCategoriaProduct.SelectedValue;
                    oMA_Producto.cod_familia = cmbPFamily.SelectedValue;
                    oMA_Producto.cod_marca = cmbFabricante.SelectedValue;
                    oMA_Producto.cod_tipo = cmbTipoProducto.SelectedValue;
                    oMA_Producto.cod_formato = cmbFormatoProducto.SelectedValue;
                    oMA_Producto.codigoint = TxtCodProducto.Text;
                    oMA_Producto.nombre = TxtNomProducto.Text;
                    oMA_Producto.alias = txtAlias.Text;
                    oMA_Producto.precio_venta = RadNumericTxtInfPrecioVenta.Text;
                    oMA_Producto.precio_oferta = RadNumericTxtInfPrecioOferta.Text;
                    oMA_Producto.stock = CheckBoxInfStock.Checked ? "1":"0";
                    oMA_Producto.promocion = TxtInfPromocion.Text;
                    
                    BL_Producto oBL_Producto = new BL_Producto();
                    string codProducto = oBL_Producto.Insert_Producto(oMA_Producto);

                    //string sProducto = "";
                    //sProducto = TxtNomProducto.Text;
                    //this.Session["sProducto"] = sProducto;
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Producto " + oMA_Producto.nombre + " fue creado con Exito/n" + "El identificador asignado fue: " + codProducto;
                    MensajeAlerta();
                    btncMasivaProducto.Visible = true;
                    saveActivarbotonesProducto();
                    SavelimpiarControlesProducto();
                    desactivarControlesProducto();

                    #region Código Comentado
                    //DAplicacion odconsuProductos = new DAplicacion();
                    //DataTable dtconsulta = odconsuProductos.ConsultaDuplicados(ConfigurationManager.AppSettings["Productos"], TxtNomProducto.Text.ToUpper(), TxtCodProducto.Text, this.Session["Company_id"].ToString());
                    //DataTable dtconsulta1 = odconsuProductos.ConsultaDuplicados(ConfigurationManager.AppSettings["ProductosEAN"], TxtCodEAN.Text.ToUpper(), null, null);
                    /*try
                    {
                        if (dtconsulta == null)
                        {
                            //string compañia;
                            //compañia = this.Session["Company_id"].ToString().Trim();
                            //if (TxtPrecioPDV.Text == "")
                            //{
                            //    TxtPrecioPDV.Text = "0";
                            //}
                            //if (TxtPrecioReventa.Text == "")
                            //{
                            //    TxtPrecioReventa.Text = "0";
                            //}

                            //EProductos oeProductos = oProductos.RegistrarProductos( TxtCodProducto.Text, 
                            //                                                        "0", 
                            //                                                        TxtNomProducto.Text, 
                            //                                                        Convert.ToInt32(cmbFabricante.Text), 
                            //                                                        Convert.ToInt32(cmbSelSubBrand.Text), 
                            //                                                        cmbPFamily.Text, ddl_psubfamily.Text,
                            //                                                        Convert.ToInt32(this.Session["Company_id"].ToString().Trim()), 
                            //                                                        cmbTipoCateg.Text, 
                            //                                                        Convert.ToInt64(CmbSubCategoriaProduct.Text), 
                            //                                                        cmbPres.Text, 0, Convert.ToDecimal(TxtPeso.Text), 
                            //                                                        Convert.ToInt32(cmbUMedida.Text), 
                            //                                                        29,
                            //                                                        0, 
                            //                                                        0, 
                            //                                                        0, 
                            //                                                        0, 
                            //                                                        Convert.ToDecimal(TxtPrecioPDV.Text), 
                            //                                                        Convert.ToDecimal(TxtPrecioReventa.Text), 
                            //                                                        TxtCaracteristicas.Text, 
                            //                                                        TxtBeneficios.Text, 
                            //                                                        true, 
                            //                                                        Convert.ToString(this.Session["sUser"]), 
                            //                                                        DateTime.Now, 
                            //                                                        Convert.ToString(this.Session["sUser"]), 
                            //                                                        DateTime.Now,txtAlias.Text);
                                
                            //EProductos oeProductostmp = oProductos.RegistrarProductostmp(   Convert.ToInt32(oeProductos.id_Product), 
                            //                                                                oeProductos.cod_Product.ToString(),
                            //                                                                oeProductos.Product_Name.ToString(),
                            //                                                                oeProductos.id_Brand.ToString(),
                            //                                                                oeProductos.id_SubBrand.ToString(),
                            //                                                                oeProductos.id_ProductFamily.ToString(),
                            //                                                                oeProductos.id_ProductSubFamily.ToString(),
                            //                                                                oeProductos.Company_id.ToString(),
                            //                                                                oeProductos.id_Product_Categ.ToString(),
                            //                                                                oeProductos.id_Product_Presentation.ToString(),
                            //                                                                oeProductos.Product_Status.ToString());

                            //DataTable dtidproduct = obtenerid.Get_ObtenerIdProduct(TxtNomProducto.Text, Convert.ToString(this.Session["sUser"]));
                            //iid_Product = Convert.ToInt32(dtidproduct.Rows[0]["id_Product"].ToString().Trim());


                            string sProducto = "";
                            sProducto = TxtNomProducto.Text;
                            this.Session["sProducto"] = sProducto;
                            //llenarcombos();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "El Producto " + this.Session["sProducto"] + " fue creado con Exito";
                            MensajeAlerta();
                            btncMasivaProducto.Visible = true;
                            saveActivarbotonesProducto();
                            SavelimpiarControlesProducto();
                            desactivarControlesProducto();

                        }
                        else
                        {
                            string sProducto = "";
                            if (dtconsulta != null)
                            {
                                sProducto = TxtNomProducto.Text;
                            }
                            //if (dtconsulta1 != null)
                            //{
                            //    sProducto = "con Código EAN : " + TxtCodEAN.Text;
                            //}

                            this.Session["sProducto"] = sProducto;
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "El Producto " + this.Session["sProducto"] + " Ya Existe";
                            MensajeAlerta();
                        }
                    }
                    catch (Exception ex)
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = this.Session["sProducto"] + " Ya Existe un Producto con ese SKU";
                        MensajeAlerta();
                    }
                    */
                    #endregion
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
        private void llenacomboConsultaUnidadMedida(int i)
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPOLORA_LLENACOMBOUMEDIDA");
            //se llena marca sefun cliente en buscar Productos

            if (dt.Rows.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[i].Cells[7].FindControl("cmbUMedida")).DataSource = dt;
                ((DropDownList)GVConsulProduct.Rows[i].Cells[7].FindControl("cmbUMedida")).DataTextField = "UnitOfMeasure_Name";
                ((DropDownList)GVConsulProduct.Rows[i].Cells[7].FindControl("cmbUMedida")).DataValueField = "id_UnitOfMeasure";
                ((DropDownList)GVConsulProduct.Rows[i].Cells[7].FindControl("cmbUMedida")).DataBind();
            }
            else
            {
            }
            dt = null;
        }
        //19/11/2016 PSA Metodo Inservible
        //private void LlenaComboConsultaProduct_Categoria(int i)
        //{
        //    try
        //    {
        //        BL_Categoria oBL_Categoria = new BL_Categoria();
        //        List<MA_Categoria> oListCategoria = new List<MA_Categoria>();
        //        oListCategoria = oBL_Categoria.Get_Categorias();

        //        ((DropDownList)GVConsulProduct.Rows[i].Cells[0].FindControl("cmbTipoCateg")).DataSource = oListCategoria;
        //        ((DropDownList)GVConsulProduct.Rows[i].Cells[0].FindControl("cmbTipoCateg")).DataTextField = "nombre";
        //        ((DropDownList)GVConsulProduct.Rows[i].Cells[0].FindControl("cmbTipoCateg")).DataValueField = "codigo";
        //        ((DropDownList)GVConsulProduct.Rows[i].Cells[0].FindControl("cmbTipoCateg")).DataBind();
                
        //        /*oDropDownList.Items.Clear();
        //        foreach (MA_Categoria oMA_Categoria in oListCategoria)
        //        {
        //            ListItem listItem = new ListItem(oMA_Categoria.nombre, oMA_Categoria.codigo);
        //            //oDropDownList.Items.Add(listItem);
        //            ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).Items.Add(listItem);
        //        }

        //        oDropDownList.Items.Insert(0, new ListItem("---Seleccione---", "0"));*/
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //        //lblmensaje.ForeColor = System.Drawing.Color.Red;
        //        //lblmensaje.Text = "Ocurrió un error inesperado..." + ex.Message;

        //    }
        //}
        private void LlenacomboConsultaCategProducto()
        {
            //Llenar Categoria del control de edición
            List<MA_Categoria> oListCategoria = (List<MA_Categoria>)this.Session["CCategoria"]; //new List<MA_Categoria>();
            if (oListCategoria.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).DataSource = oListCategoria;
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).DataTextField = "nombre";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).DataValueField = "codigo";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).DataBind();
            }
            /*DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 13);
            //se llena mallas PDVC

            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).DataSource = ds;
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).DataTextField = "Product_Category";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).DataValueField = "id_ProductCategory";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).DataBind();
            }
            else
            {
            }*/
        }
        private void LlenacomboConsultaSubcateProducto()
        {
            BL_SubCategoria oBL_SubCategoria = new BL_SubCategoria();
            List<MA_SubCategoria> oListMA_SubCategoria = new List<MA_SubCategoria>();
            oListMA_SubCategoria = oBL_SubCategoria.Get_SubCategoriasByCodCategoria(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).SelectedValue);
            if (oListMA_SubCategoria.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[1].FindControl("CmbSubCategoriaProduct")).DataSource = oListMA_SubCategoria;
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[1].FindControl("CmbSubCategoriaProduct")).DataTextField = "nombre";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[1].FindControl("CmbSubCategoriaProduct")).DataValueField = "codigo";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[1].FindControl("CmbSubCategoriaProduct")).DataBind();
            }
            #region
            /*DataTable dt1 = new DataTable();
            dt1 = owsadministrativo.LlenaComboSubCategoriaPresent(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).SelectedValue);

            if (dt1.Rows.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[1].FindControl("CmbSubCategoriaProduct")).DataSource = dt1;
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[1].FindControl("CmbSubCategoriaProduct")).DataTextField = "Name_Subcategory";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[1].FindControl("CmbSubCategoriaProduct")).DataValueField = "id_Subcategory";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[1].FindControl("CmbSubCategoriaProduct")).DataBind();
            }
            else
            {
            }*/
            #endregion
        }
        private void LlenacomboConsultaFamiliaProducto()
        {
            BL_Familia oBL_Familia = new BL_Familia();
            List<MA_Familia> oListMA_Familia = new List<MA_Familia>();
            oListMA_Familia = oBL_Familia.Get_FamiliasByCodSubCategoria(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("CmbSubCategoriaProduct")).SelectedValue);
            if (oListMA_Familia.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbPFamily")).DataSource = oListMA_Familia;
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbPFamily")).DataTextField = "nombre";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbPFamily")).DataValueField = "codigo";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbPFamily")).DataBind();
            }
        }        
        private void LlenacomboConsultaMarcaProducto()
        {
            List<MA_Marca> oListMarca = (List<MA_Marca>)this.Session["CMarca"];
            if (oListMarca.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[3].FindControl("cmbFabricante")).DataSource = oListMarca;
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[3].FindControl("cmbFabricante")).DataTextField = "nombre";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[3].FindControl("cmbFabricante")).DataValueField = "codigo";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[3].FindControl("cmbFabricante")).DataBind();
            }
            /*
            DataTable dt = new DataTable();
            //dt = owsadministrativo.LLenacomboMarcaporCategoria(((DropDownList)GVConsulProduct.Rows[i].Cells[0].FindControl("cmbTipoCateg")).Text); 
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOMARCASEGUNCATEGORIA", ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).Text);

            if (dt.Rows.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbFabricante")).DataSource = dt;
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbFabricante")).DataTextField = "Name_Brand";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbFabricante")).DataValueField = "id_Brand";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbFabricante")).DataBind();
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbFabricante")).Items.Insert(0, new ListItem("<Seleccione..>", "0"));
            }
            else
            {
            }
            dt = null;
            */
        }
        private void LlenacomboConsultaSubMarcaProducto()
        {
            DataSet ds = new DataSet();
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOSUBMARCAPROD", ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("cmbFabricante")).Text);
            //se llena mallas PDVC

            if (ds.Tables[0].Rows.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[3].FindControl("cmbSelSubBrand")).DataSource = ds;
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[3].FindControl("cmbSelSubBrand")).DataTextField = "Name_SubBrand";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[3].FindControl("cmbSelSubBrand")).DataValueField = "id_SubBrand";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[3].FindControl("cmbSelSubBrand")).DataBind();
            }
            else
            {
            }
        }
        private void LlenacomboConsultaTipoProducto() {
            List<MA_Tipo> oListTipo = (List<MA_Tipo>)this.Session["CTipo"];
            if (oListTipo.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[8].FindControl("cmb_TipProducto")).DataSource = oListTipo;
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[8].FindControl("cmb_TipProducto")).DataTextField = "nombre";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[8].FindControl("cmb_TipProducto")).DataValueField = "codigo";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[8].FindControl("cmb_TipProducto")).DataBind();
            }
        }
        private void LlenacomboConsultaFormatoProducto()
        {
            List<MA_Formato> oListFormato = (List<MA_Formato>)this.Session["CFormato"];
            if (oListFormato.Count > 0)
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[9].FindControl("cmb_ForProducto")).DataSource = oListFormato;
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[9].FindControl("cmb_ForProducto")).DataTextField = "nombre";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[9].FindControl("cmb_ForProducto")).DataValueField = "codigo";
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[9].FindControl("cmb_ForProducto")).DataBind();
            }
        }
        protected void BtnBProductos_Click(object sender, EventArgs e)
        {
            IbtnProductos_ModalPopupExtender.Hide();
            desactivarControlesProducto();
            LblFaltantes.Text = "";
            if ((cmbBCategoriaProduct.Text == "0" || cmbBCategoriaProduct.Text == "") && 
                (cmbBBrand.Text == "0" || cmbBBrand.Text == "")
                ){
                this.Session["mensajealert"] = "Categoria y/o Marca";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parametro de consulta";
                MensajeAlerta();
                SavelimpiarControlesProducto();
                IbtnProductos_ModalPopupExtender.Show();
                return;
                }
            BuscarActivarbotnesProducto();

            //Obtener de las sessiones los maestros de: Categoria, SubCategoria, Familia, Marca, Tipo, Formato, 
            /*List<MA_Categoria> oListMA_Categoria = (List<MA_Categoria>)this.Session["CCategoria"];
            List<MA_SubCategoria> oListMA_SubCategoria = (List<MA_SubCategoria>)this.Session["CSubCategoria"];
            List<MA_Familia> oListMA_Familia = (List<MA_Familia>)this.Session["CFamilia"];
            List<MA_Marca> oListMA_Marca = (List<MA_Marca>)this.Session["CMarca"];
            List<MA_Formato> oListMA_Formato = (List<MA_Formato>)this.Session["CFormato"];
            List<MA_Tipo> oListMA_Tipo = (List<MA_Tipo>)this.Session["CTipo"];*/
            
            BL_Producto oBL_Producto = new BL_Producto();
            List<MA_Producto> oListSR_Producto = new List<MA_Producto>();
            oListSR_Producto = oBL_Producto.Get_Productos(cmbBCategoriaProduct.SelectedValue, cmbBBrand.SelectedValue);
            this.Session["CBPCategoria"] = cmbBCategoriaProduct.SelectedValue;
            this.Session["CBPMarca"] = cmbBBrand.SelectedValue;
            
            //Reemplazar los codigos de Categoria, Marca, Etc, con sus valores
            /*foreach (MA_Producto oMA_Producto in oListSR_Producto) {
                foreach (MA_Categoria oMA_Categoria in oListMA_Categoria) {
                    if (oMA_Producto.cod_categoria.Equals(oMA_Categoria.codigo)) {
                        oMA_Producto.categoria = oMA_Categoria.nombre;
                    }
                }
                foreach (MA_SubCategoria oMA_SubCategoria in oListMA_SubCategoria)
                {
                    if (oMA_Producto.cod_subcategoria.Equals(oMA_SubCategoria.codigo))
                    {
                        oMA_Producto.subcategoria = oMA_SubCategoria.nombre;
                    }
                }
                foreach (MA_Familia oMA_Familia in oListMA_Familia)
                {
                    if (oMA_Producto.cod_familia.Equals(oMA_Familia.codigo))
                    {
                        oMA_Producto.familia = oMA_Familia.nombre;
                    }
                }
                foreach (MA_Marca oMA_Marca in oListMA_Marca) {
                    if (oMA_Producto.cod_marca.Equals(oMA_Marca.codigo))
                    {
                        oMA_Producto.marca = oMA_Marca.nombre;
                    }                
                }
                foreach (MA_Tipo oMA_Tipo in oListMA_Tipo)
                {
                    if (oMA_Producto.cod_tipo.Equals(oMA_Tipo.codigo))
                    {
                        oMA_Producto.tipo = oMA_Tipo.nombre;
                    }
                }
                foreach (MA_Formato oMA_Formato in oListMA_Formato)
                {
                    if (oMA_Producto.cod_formato.Equals(oMA_Formato.codigo))
                    {
                        oMA_Producto.formato = oMA_Formato.nombre;
                    }
                }
            }
            */
            this.Session["CProducto"] = oListSR_Producto;

            if (oListSR_Producto.Count > 0)
            {
                GVConsulProduct.DataSource = oListSR_Producto;
                GVConsulProduct.DataBind();
                ModalPCProducto.Show();
                IbtnProductos_ModalPopupExtender.Hide();
            }
            else
            {
                SavelimpiarControlesProducto();
                saveActivarbotonesProducto();
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                MensajeAlerta();
                IbtnProductos_ModalPopupExtender.Show();
            }

        }
        protected void GVConsulProduct_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            Button1.Visible = true;
            #region Validaciones
            //Validaciones
            /*if (((CheckBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[12].FindControl("CheckEProducto")).Checked != false)
                estado = true;
            else
            {
                estado = false;
                DAplicacion oddeshabproduct = new DAplicacion();
                DataTable dt = oddeshabproduct.PermitirDeshabilitar(ConfigurationManager.AppSettings["ProductsProducts_Planning"], Convert.ToString(this.Session["idProduct"]));
                if (dt != null)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No se puede Deshabilitar este registro ya que existe relación en el maestro de Products_Planning, por favor verifique";
                    MensajeAlerta();
                    return;
                }
            }*/

            /*try
            {
                if (Convert.ToDecimal(((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPeso")).Text) == 0)
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Sr. Usuario, no ingresar peso con valor 0";
                    MensajeAlerta();
                    return;
                }
            }
            catch
            {
            }*/

            /*try
            {
                if (((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text == "0")
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Sr. Usuario, no ingresar código de producto con valor 0";
                    MensajeAlerta();
                    return;
                }
            }
            catch
            {
            }*/


            /*if (((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).SelectedValue == "0" || 
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbFabricante")).SelectedValue == "0" || 
                ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPeso")).Text == "" || 
                ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text == "" ||
                ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text == "" || 
                ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCompan")).Text == "0")
            {
                if (((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Categoría";
                }
                if (((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbFabricante")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Marca";
                }
                if (((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPeso")).Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Peso";
                }
                if (((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Código";
                }
                if (((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de producto";
                }
                if (((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCompan")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }
                if (((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPrecioPDV")).Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Precio de Lista";
                }
                if (((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPrecioReventa")).Text == "")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "PVP";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }*/
            #endregion
            try
            {
                #region
                /*repetido1 = Convert.ToString(this.Session["rept1"]);
                repetido2 = Convert.ToString(this.Session["rept2"]);
                if (repetido1 != ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text || 
                    repetido2 != ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text)
                {*/
                    /*DAplicacion odconsuProductos = new DAplicacion();
                    DataTable dtconsulta = new DataTable();
                    DataTable dtconsulta1 = new DataTable();*/

                /*if (repetido1 != ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text || 
                    repetido2 != ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text)
                {
                    dtconsulta1 = odconsuProductos.ConsultaDuplicados(ConfigurationManager.AppSettings["Productos"], 
                        ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text, 
                        ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text, 
                        null);
                }
                else
                {
                    dtconsulta1 = null;
                }*/
                #endregion
                #region
                /*if (dtconsulta1 == null)
                        //-- =============================================
                        //-- Author:		    <Angel Ortiz>
                        //-- Create date:       <20/05/2011>
                        //-- Description:       <Corregido bug al actualizar producto, se cambió GVConsulProduct.EditIndex].Cells[0].FindControl("CmbSubCategoriaProduct").Text - no obtenía el valor seleccionado>                        
                //-- =============================================
                {*/
                #endregion
                #region
                /*EProductos oeaProductos = oProductos.Actualizar_Producto(
                Convert.ToInt64(this.Session["idProduct"]), 
                ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text, 
                "0", 
                ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text, 
                Convert.ToInt32(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbFabricante")).Text), 
                Convert.ToInt32(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbSelSubBrand")).Text), 
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPFamily")).Text,
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("ddl_pgsubfamily")).Text, 
                Convert.ToInt32(this.Session["iCompanyId"].ToString().Trim()), 
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).Text, 
                Convert.ToInt64(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("CmbSubCategoriaProduct")).Text), 
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPres")).Text, 
                0, 
                Convert.ToDecimal(((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPeso")).Text), 
                Convert.ToInt32(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbUMedida")).Text), 
                29,
                0, 
                0, 
                0, 
                0, 
                Convert.ToDecimal(((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPrecioPDV")).Text), 
                Convert.ToDecimal(((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPrecioReventa")).Text), 
                "0", 
                "0", 
                estado, 
                Convert.ToString(this.Session["sUser"]), 
                DateTime.Now, 
                ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtAliasProducto")).Text
                );*/
                #endregion
                
                MA_Producto oMA_Producto = new MA_Producto();
                oMA_Producto.cod_categoria = ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).SelectedValue;
                oMA_Producto.cod_subcategoria = ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("CmbSubCategoriaProduct")).SelectedValue;
                oMA_Producto.cod_familia = ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPFamily")).SelectedValue;
                oMA_Producto.cod_marca = ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbFabricante")).SelectedValue;
                oMA_Producto.codigo = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodigoProducto")).Text;
                oMA_Producto.codigoint = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text;
                oMA_Producto.nombre = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text;
                oMA_Producto.alias = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtAliasProducto")).Text;
                oMA_Producto.cod_tipo = ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmb_TipProducto")).SelectedValue;
                oMA_Producto.cod_formato = ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmb_ForProducto")).SelectedValue;
                oMA_Producto.precio_venta = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPrecioPDV")).Text;
                oMA_Producto.precio_oferta = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPrecioOferta")).Text;
                oMA_Producto.stock = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtStockProducto")).Text;
                oMA_Producto.promocion = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPromoProducto")).Text;

                oMA_Producto.cod_categoria = (oMA_Producto.cod_categoria == "" ? null : oMA_Producto.cod_categoria);
                oMA_Producto.cod_subcategoria = (oMA_Producto.cod_subcategoria == "" ? null : oMA_Producto.cod_subcategoria);
                oMA_Producto.cod_familia = (oMA_Producto.cod_familia == "" ? null : oMA_Producto.cod_familia);
                oMA_Producto.cod_marca = (oMA_Producto.cod_marca == "" ? null : oMA_Producto.cod_marca);
                oMA_Producto.codigo = (oMA_Producto.codigo == "" ? null : oMA_Producto.codigo);
                oMA_Producto.codigoint = (oMA_Producto.codigoint == "" ? null : oMA_Producto.codigoint);
                oMA_Producto.nombre = (oMA_Producto.nombre == "" ? null : oMA_Producto.nombre);
                oMA_Producto.alias = (oMA_Producto.alias == "" ? null : oMA_Producto.alias);
                oMA_Producto.cod_tipo = (oMA_Producto.cod_tipo == "" ? null : oMA_Producto.cod_tipo);
                oMA_Producto.cod_formato = (oMA_Producto.cod_formato == "" ? null : oMA_Producto.cod_formato);
                oMA_Producto.precio_venta = (oMA_Producto.precio_venta == "" ? null : oMA_Producto.precio_venta);
                oMA_Producto.precio_oferta = (oMA_Producto.precio_oferta == "" ? null : oMA_Producto.precio_oferta);
                oMA_Producto.stock = (oMA_Producto.stock == "" ? null : oMA_Producto.stock);
                oMA_Producto.promocion = (oMA_Producto.promocion == "" ? null : oMA_Producto.promocion);


                BL_Producto oBL_Producto = new BL_Producto();
                oBL_Producto.Update_Producto(oMA_Producto);
                

                #region
                        /*EProductos oeaProductostmp = oProductos.Actualizar_ProductoTMP(
                            Convert.ToInt64(this.Session["idProduct"]), 
                            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text, 
                            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text, 
                            Convert.ToInt32(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbFabricante")).Text), 
                            Convert.ToInt32(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbSelSubBrand")).Text), 
                            ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPFamily")).Text,
                            ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("ddl_pgsubfamily")).Text, 
                            ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).Text, 
                            ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPres")).Text, 
                            estado);
                        

                        sNomProd = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text;
                        this.Session["sNomProd"] = sNomProd;
                        */
                        #endregion

                SavelimpiarControlesProducto();
                GVConsulProduct.EditIndex = -1;

                List<MA_Producto> oListSR_Producto = new List<MA_Producto>();
                oListSR_Producto = oBL_Producto.Get_Productos(this.Session["CBPCategoria"].ToString(), this.Session["CBPMarca"].ToString());
                this.Session["CProducto"] = oListSR_Producto;
                if (oListSR_Producto != null)
                {
                    if (oListSR_Producto.Count > 0)
                    {
                        Button1.Visible = true;
                        GVConsulProduct.DataSource = oListSR_Producto;
                        GVConsulProduct.DataBind();
                        ModalPCProducto.Show();
                    }
                    ModalPCProducto.Show();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Producto : " + oMA_Producto.nombre + " fue Actualizada con Exito";
                    MensajeAlerta();
                    saveActivarbotonesProducto();
                    desactivarControlesProducto();
                }

                #region
                /*DataTable oeProducto = oProductos.BuscarProductos(
                Convert.ToInt32(this.Session["iCompanyId"].ToString().Trim()), 
                this.Session["CategoriaProduc"].ToString().Trim(), 
                Convert.ToInt32(this.Session["iid_Brand"].ToString().Trim()), 
                this.Session["SKU_Producto"].ToString().Trim());*/

                /*this.Session["CProducto"] = oeProducto;
                if (oeProducto != null)
                {
                    if (oeProducto.Rows.Count > 0)
                    {
                        Button1.Visible = true;
                        GVConsulProduct.DataSource = oeProducto;
                        GVConsulProduct.DataBind();
                        ModalPCProducto.Show();*/
                        /*for (int i = 0; i <= oeProducto.Rows.Count - 1; i++)
                        {
                            // LlenacomboConsultaCategProducto(i);
                            ((DropDownList)GVConsulProduct.Rows[i].Cells[0].FindControl("cmbTipoCateg")).Text = oeProducto.Rows[i][7].ToString().Trim();
                            LlenacomboConsultaSubcateProducto();
                            ((DropDownList)GVConsulProduct.Rows[i].Cells[1].FindControl("CmbSubCategoriaProduct")).Text = oeProducto.Rows[i][8].ToString().Trim();
                            LlenacomboConsultaMarcaProducto();
                            ((DropDownList)GVConsulProduct.Rows[i].Cells[2].FindControl("cmbFabricante")).Text = oeProducto.Rows[i][4].ToString().Trim();
                            LlenacomboConsultaSubMarcaProducto();
                            ((DropDownList)GVConsulProduct.Rows[i].Cells[3].FindControl("cmbSelSubBrand")).Text = oeProducto.Rows[i][5].ToString().Trim();
                            comboConsultaCompanyProducto();
                            ((Label)GVConsulProduct.Rows[i].Cells[3].FindControl("TxtCompan")).Text = oeProducto.Rows[i][27].ToString().Trim();
                            comboConsultaPresentacionProd();
                            ((DropDownList)GVConsulProduct.Rows[i].Cells[4].FindControl("cmbPres")).Text = oeProducto.Rows[i][9].ToString().Trim();
                            comboConsultaPFamiliaProd();
                            ((DropDownList)GVConsulProduct.Rows[i].Cells[5].FindControl("cmbPFamily")).Text = oeProducto.Rows[i][26].ToString().Trim();
                            DropDownList ddl = ((DropDownList)GVConsulProduct.Rows[i].Cells[6].FindControl("ddl_pgsubfamily"));
                            llenarddl_gsubfamilia(ddl, ((DropDownList)GVConsulProduct.Rows[i].Cells[5].FindControl("cmbPFamily")).Text);
                            ((DropDownList)GVConsulProduct.Rows[i].Cells[6].FindControl("ddl_pgsubfamily")).Text = oeProducto.Rows[i][29].ToString().Trim();
                        }*/
                /*}
                ModalPCProducto.Show();
                //llenarcombos();
                Alertas.CssClass = "MensajesCorrecto";
                LblFaltantes.Text = "El Producto : " + this.Session["sNomProd"] + " fue Actualizada con Exito";
                MensajeAlerta();
                saveActivarbotonesProducto();
                desactivarControlesProducto();
                }*/

                /*}
                else
                {
                    sNomProd = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text;
                    this.Session["sNomProd"] = sNomProd;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Producto : " + this.Session["sNomProd"] + " Ya Existe";
                    MensajeAlerta();
                }*/
                //}
#endregion
                #region
                /*else{
                    EProductos oeaProductos = oProductos.Actualizar_Producto(Convert.ToInt64(this.Session["idProduct"].ToString().Trim()),
                    ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text,
                    "0",
                    ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text,
                    Convert.ToInt32(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbFabricante")).Text),
                    Convert.ToInt32(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbSelSubBrand")).Text),
                    ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPFamily")).Text,
                    ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("ddl_pgsubfamily")).Text,
                    Convert.ToInt32(this.Session["iCompanyId"].ToString().Trim()),
                    ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).Text,
                    Convert.ToInt64(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("CmbSubCategoriaProduct")).Text),
                    ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPres")).Text,
                    0, Convert.ToDecimal(((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPeso")).Text),
                    Convert.ToInt32(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbUMedida")).Text),
                    29,
                    0, 0, 0, 0,
                    Convert.ToDecimal(((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPrecioPDV")).Text),
                    Convert.ToDecimal(((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPrecioReventa")).Text),
                    "0", "0", estado, Convert.ToString(this.Session["sUser"]), DateTime.Now, ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtAliasProducto")).Text);

                    EProductos oeaProductostmp = oProductos.Actualizar_ProductoTMP(
                    Convert.ToInt64(this.Session["idProduct"]),
                    ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text,
                    ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text,
                    Convert.ToInt32(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbFabricante")).Text),
                    Convert.ToInt32(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbSelSubBrand")).Text),
                    ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPFamily")).Text,
                    ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("ddl_pgsubfamily")).Text,
                    ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).Text,
                    ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPres")).Text, estado);

                    sNomProd = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text;
                    this.Session["sNomProd"] = sNomProd;
                    SavelimpiarControlesProducto();
                    GVConsulProduct.EditIndex = -1;
                    DataTable oeProducto = oProductos.BuscarProductos(Convert.ToInt32(this.Session["iCompanyId"].ToString().Trim()), this.Session["CategoriaProduc"].ToString().Trim(), Convert.ToInt32(this.Session["iid_Brand"].ToString().Trim()), this.Session["SKU_Producto"].ToString().Trim());
                    this.Session["CProducto"] = oeProducto;
                    if (oeProducto != null)
                    {
                        if (oeProducto.Rows.Count > 0)
                        {

                            GVConsulProduct.DataSource = oeProducto;
                            GVConsulProduct.DataBind();
                            ModalPCProducto.Show();

                            //for (int i = 0; i <= oeProducto.Rows.Count - 1; i++)
                            //{
                            //   // LlenacomboConsultaCategProducto(i);
                            //    ((DropDownList)GVConsulProduct.Rows[i].Cells[0].FindControl("cmbTipoCateg")).Text = oeProducto.Rows[i][7].ToString().Trim();
                            //    LlenacomboConsultaSubcateProducto();
                            //    ((DropDownList)GVConsulProduct.Rows[i].Cells[1].FindControl("CmbSubCategoriaProduct")).Text = oeProducto.Rows[i][8].ToString().Trim();
                            //    LlenacomboConsultaMarcaProducto();
                            //    ((DropDownList)GVConsulProduct.Rows[i].Cells[2].FindControl("cmbFabricante")).Text = oeProducto.Rows[i][4].ToString().Trim();
                            //    LlenacomboConsultaSubMarcaProducto();
                            //    ((DropDownList)GVConsulProduct.Rows[i].Cells[3].FindControl("cmbSelSubBrand")).Text = oeProducto.Rows[i][5].ToString().Trim();
                            //    comboConsultaCompanyProducto();
                            //    ((Label)GVConsulProduct.Rows[i].Cells[3].FindControl("TxtCompan")).Text = oeProducto.Rows[i][27].ToString().Trim();
                            //    comboConsultaPresentacionProd();
                            //    ((DropDownList)GVConsulProduct.Rows[i].Cells[4].FindControl("cmbPres")).Text = oeProducto.Rows[i][9].ToString().Trim();
                            //    comboConsultaPFamiliaProd();
                            //    ((DropDownList)GVConsulProduct.Rows[i].Cells[5].FindControl("cmbPFamily")).Text = oeProducto.Rows[i][26].ToString().Trim();
                            //    DropDownList ddl = ((DropDownList)GVConsulProduct.Rows[i].Cells[6].FindControl("ddl_pgsubfamily"));
                            //    llenarddl_gsubfamilia(ddl, ((DropDownList)GVConsulProduct.Rows[i].Cells[5].FindControl("cmbPFamily")).Text);
                            //    ((DropDownList)GVConsulProduct.Rows[i].Cells[6].FindControl("ddl_pgsubfamily")).Text = oeProducto.Rows[i][29].ToString().Trim();
                            //}
                        }
                        ModalPCProducto.Show();
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Producto : " + this.Session["sNomProd"] + " fue actualizado con éxito";
                        MensajeAlerta();
                        saveActivarbotonesProducto();
                        desactivarControlesProducto();
                    }
                }*/
                #endregion
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
        protected void GVConsulProduct_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            Button1.Visible = true;
            GVConsulProduct.EditIndex = -1;
            //DataTable oeProducto = oProductos.BuscarProductos(Convert.ToInt32(this.Session["iCompanyId"].ToString().Trim()), this.Session["CategoriaProduc"].ToString().Trim(), Convert.ToInt32(this.Session["iid_Brand"].ToString().Trim()), this.Session["SKU_Producto"].ToString().Trim());
            BL_Producto oBL_Producto = new BL_Producto();
            List<MA_Producto> oListSR_Producto = new List<MA_Producto>();
            oListSR_Producto = oBL_Producto.Get_Productos(this.Session["CBPCategoria"].ToString(), this.Session["CBPMarca"].ToString());
            this.Session["CProducto"] = oListSR_Producto;
            if (oListSR_Producto != null)
            {
                if (oListSR_Producto.Count > 0)
                {
                    GVConsulProduct.DataSource = oListSR_Producto;
                    GVConsulProduct.DataBind();
                    ModalPCProducto.Show();

                    //for (int i = 0; i <= oeProducto.Rows.Count - 1; i++)
                    //{
                    //    LlenacomboConsultaCategProducto(i);
                    //    ((DropDownList)GVConsulProduct.Rows[i].Cells[0].FindControl("cmbTipoCateg")).Text = oeProducto.Rows[i][7].ToString().Trim();
                    //    LlenacomboConsultaSubcateProducto(i);
                    //    ((DropDownList)GVConsulProduct.Rows[i].Cells[1].FindControl("CmbSubCategoriaProduct")).Text = oeProducto.Rows[i][8].ToString().Trim();
                    //    LlenacomboConsultaMarcaProducto(i);
                    //    ((DropDownList)GVConsulProduct.Rows[i].Cells[2].FindControl("cmbFabricante")).Text = oeProducto.Rows[i][4].ToString().Trim();
                    //    LlenacomboConsultaSubMarcaProducto(i);
                    //    ((DropDownList)GVConsulProduct.Rows[i].Cells[3].FindControl("cmbSelSubBrand")).Text = oeProducto.Rows[i][5].ToString().Trim();
                    //    comboConsultaCompanyProducto(i);
                    //    ((Label)GVConsulProduct.Rows[i].Cells[3].FindControl("TxtCompan")).Text = oeProducto.Rows[i][27].ToString().Trim();
                    //    comboConsultaPresentacionProd(i);
                    //    ((DropDownList)GVConsulProduct.Rows[i].Cells[4].FindControl("cmbPres")).Text = oeProducto.Rows[i][9].ToString().Trim();
                    //    comboConsultaPFamiliaProd(i);
                    //    ((DropDownList)GVConsulProduct.Rows[i].Cells[5].FindControl("cmbPFamily")).Text = oeProducto.Rows[i][26].ToString().Trim();
                    //}
                }
            }
        }
        protected void GVConsulProduct_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Button1.Visible = false;
            ModalPCProducto.Show();
            
            GVConsulProduct.EditIndex = e.NewEditIndex;

            MA_Producto oMA_Producto = new MA_Producto();
            oMA_Producto.cod_categoria = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("lblTipoCategCod")).Text;
            oMA_Producto.cod_subcategoria = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[1].FindControl("lblSubCategoriaProductCod")).Text;
            oMA_Producto.cod_familia = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("lblPFamilyCod")).Text;
            oMA_Producto.cod_marca = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[3].FindControl("lblFabricanteCod")).Text;
            oMA_Producto.codigo = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[4].FindControl("lblCodigoProducto")).Text;
            oMA_Producto.codigoint = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[5].FindControl("lblCodProducto")).Text;
            oMA_Producto.nombre = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[6].FindControl("lblNomProducto")).Text;
            oMA_Producto.alias = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[7].FindControl("lblAliasProducto")).Text;
            oMA_Producto.cod_tipo = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[8].FindControl("lbl_TipProductoCod")).Text;
            oMA_Producto.cod_formato = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[9].FindControl("lbl_ForProductoCod")).Text;
            oMA_Producto.precio_venta = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[10].FindControl("lblPrecioPDV")).Text;
            oMA_Producto.precio_oferta = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[11].FindControl("lblPrecioOferta")).Text;
            oMA_Producto.stock = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[12].FindControl("LblStockProducto")).Text;
            oMA_Producto.promocion = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[13].FindControl("LblPromoProducto")).Text;

            #region
            /*
            string Categoria, Subcategoria, Marca, subMarca, Presentacion,Codigo,Tipo,Formato,PrecioVenta,PrecioOferta,Stock,Promocion, Codigo_int, Familia, SubFamilia, Peso, SKU, Cliente, Nombre, PrecioL, PrecioPVP, umedida, Alias;
            bool estado;
            
            Categoria = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("lblTipoCategCod")).Text;
            Subcategoria = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[1].FindControl("lblSubCategoriaProductCod")).Text;
            Familia = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[2].FindControl("lblPFamilyCod")).Text;
            Marca = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[3].FindControl("lblFabricanteCod")).Text;
            Codigo = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[4].FindControl("lblCodigoProducto")).Text;
            Codigo_int = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[5].FindControl("lblCodProducto")).Text;
            Nombre = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[6].FindControl("lblNomProducto")).Text;
            Alias = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[7].FindControl("lblAliasProducto")).Text;
            Tipo = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[8].FindControl("lbl_TipProductoCod")).Text;
            Formato = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[9].FindControl("lbl_ForProductoCod")).Text;
            PrecioVenta = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[10].FindControl("lblPrecioPDV")).Text;
            PrecioOferta = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[11].FindControl("lblPrecioOferta")).Text;
            Stock = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[12].FindControl("LblStockProducto")).Text;
            Promocion = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[13].FindControl("LblPromoProducto")).Text;
            */
            //subMarca = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[3].FindControl("lblSelSubBrand")).Text;
            //Presentacion = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[4].FindControl("lblPres")).Text;
            //SubFamilia = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[6].FindControl("lbl_pgsubfamily")).Text;
            //Peso = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[6].FindControl("TxtPeso")).Text;
            //SKU = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[7].FindControl("TxtCodProducto")).Text;
            //Cliente = ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[8].FindControl("TxtCompan")).Text;
            //umedida = ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[7].FindControl("cmbUMedida")).Text;
            //estado = ((CheckBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[12].FindControl("CheckEProducto")).Checked;

            //DataTable ecproducto = (DataTable)this.Session["CProducto"];
            //GVConsulProduct.DataSource = ecproducto;
            //GVConsulProduct.DataBind();
            //this.Session["idProduct"] = Convert.ToInt64(ecproducto.Rows[GVConsulProduct.EditIndex]["id_Product"].ToString().Trim());
            //iid_Product = Convert.ToInt64(this.Session["idProduct"]);
#endregion
            
            List<MA_Producto> oListSR_Producto = (List<MA_Producto>)this.Session["CProducto"];
            GVConsulProduct.DataSource = oListSR_Producto;
            GVConsulProduct.DataBind();

            
            LlenacomboConsultaCategProducto();
            if (oMA_Producto.cod_categoria != "")
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).Items.FindByValue(oMA_Producto.cod_categoria).Selected = true;
            }
            LlenacomboConsultaSubcateProducto();
            if (oMA_Producto.cod_subcategoria != "")
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("CmbSubCategoriaProduct")).Items.FindByValue(oMA_Producto.cod_subcategoria).Selected = true;
            }
            LlenacomboConsultaFamiliaProducto();
            if (oMA_Producto.cod_familia != "")
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPFamily")).Items.FindByValue(oMA_Producto.cod_familia).Selected = true;
            }
            LlenacomboConsultaMarcaProducto();
            if (oMA_Producto.cod_marca != "")
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbFabricante")).Items.FindByValue(oMA_Producto.cod_marca).Selected = true;
            }
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodigoProducto")).Text = oMA_Producto.codigo;
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text = oMA_Producto.codigoint;
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text =oMA_Producto.nombre;
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtAliasProducto")).Text = oMA_Producto.alias;
            LlenacomboConsultaTipoProducto();
            if (oMA_Producto.cod_tipo != "")
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmb_TipProducto")).Items.FindByValue(oMA_Producto.cod_tipo).Selected = true;
            }
            LlenacomboConsultaFormatoProducto();
            if (oMA_Producto.cod_formato != "")
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmb_ForProducto")).Items.FindByValue(oMA_Producto.cod_formato).Selected = true;
            }
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPrecioPDV")).Text = oMA_Producto.precio_venta;
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPrecioOferta")).Text = oMA_Producto.precio_oferta;
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtStockProducto")).Text = oMA_Producto.stock;
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPromoProducto")).Text = oMA_Producto.promocion;
            
            //Setear los valores 
            /*
             * Comentado 20Nov2016 Psalas
            LlenacomboConsultaCategProducto();
            if (Categoria != "")
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbTipoCateg")).Items.FindByText(Categoria).Selected = true;
            }

            LlenacomboConsultaSubcateProducto();
            if (Subcategoria != "")
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("CmbSubCategoriaProduct")).Items.FindByText(Subcategoria).Selected = true;
            }


            LlenacomboConsultaMarcaProducto();
            if (Marca != "")
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbFabricante")).Items.FindByText(Marca).Selected = true;
            }

            LlenacomboConsultaSubMarcaProducto();
            if (subMarca != "")
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbSelSubBrand")).Items.FindByText(subMarca).Selected = true;
            }
            comboConsultaPresentacionProd();
            if (Presentacion != "")
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPres")).Items.FindByText(Presentacion).Selected = true;
            }
            comboConsultaPFamiliaProd();
            if (Familia != "")
            {
                ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPFamily")).Items.FindByText(Familia).Selected = true;
            }

            llenarddl_gsubfamilia(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[6].FindControl("ddl_pgsubfamily")), ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPFamily")).Text);
            ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("ddl_pgsubfamily")).Text = ecproducto.Rows[GVConsulProduct.EditIndex][29].ToString().Trim();

            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPeso")).Text = Peso;
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text = SKU;
            comboConsultaCompanyProducto();
            ((Label)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCompan")).Text = Cliente;
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text = Nombre;
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtAliasProducto")).Text = Alias;
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPrecioPDV")).Text = PrecioL;
            ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtPrecioReventa")).Text = PrecioPVP;
            llenacomboConsultaUnidadMedida(GVConsulProduct.EditIndex);
            ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbUMedida")).Text = umedida;
            estado = ((CheckBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("CheckEProducto")).Checked;
            this.Session["rept1"] = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtNomProducto")).Text;
            this.Session["rept2"] = ((TextBox)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("TxtCodProducto")).Text;
            */

        }
        /*protected void cmbPFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            //llenarddl_gsubfamilia(ddl_psubfamily, cmbPFamily.Text.ToString());
        }*/
        private void llenarddl_gsubfamilia(DropDownList ddl, string familia)
        {
            DataTable ds = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_BUSCAR_SUBFAMILY", 0, 0, 0, familia,"");
            //se llena Combo de marca en buscar de maestro familia de producto
            ddl.DataSource = dt;
            ddl.DataTextField = "subfam_nombre";
            ddl.DataValueField = "id_ProductSubFamily";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            ds = null;            
        }
        protected void btnCancelarProducto_Click(object sender, EventArgs e)
        {
            saveActivarbotonesProducto();
            desactivarControlesProducto();
            SavelimpiarControlesProducto();
            btncMasivaProducto.Visible = true;
        }
        protected void cbmbcompañia_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenacomboCategoriaxClienteenBusProduct();
        }
        //protected void cmbBCategoriaProduct_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    llenacomboMarcaaxCategoriaenBusProduct();
        //}
        protected void cmbPFamily_SelectedIndexChanged1(object sender, EventArgs e)
        {
            llenarddl_gsubfamilia(((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("ddl_pgsubfamily")), ((DropDownList)GVConsulProduct.Rows[GVConsulProduct.EditIndex].Cells[0].FindControl("cmbPFamily")).Text);
            ModalPCProducto.Show();
        }
        protected void cmbTipoCateg_SelectedIndexChanged1(object sender, System.EventArgs e)
        {
            LlenacomboConsultaSubcateProducto();
            LlenacomboConsultaMarcaProducto();
            ModalPCProducto.Show();
        }
        protected void cmbFabricante_SelectedIndexChanged1(object sender, System.EventArgs e)
        {
            LlenacomboConsultaSubMarcaProducto();
            comboConsultaCompanyProducto();
            comboConsultaPresentacionProd();
            comboConsultaPFamiliaProd();
            ModalPCProducto.Show();
        }     

        #endregion

        #region Producto Ancla
        protected void BtnCrearAncla_Click(object sender, EventArgs e)
        {
            try
            {
                this.planningADM = this.Session["AdmProd"].ToString().Trim();
            }
            catch
            {
            }
            if (this.planningADM == "SI")
            {
              
                comboclienteenPanclaporplanning();
                crearActivarbotonesProductAncla();
                activarControlesProductAncla();
            }
            else
            {               
                LlenacomboClienteProductAncla();
                crearActivarbotonesProductAncla();
                activarControlesProductAncla();
               
            }
           
        }
        protected void cmbClienteAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboOficinaXclienteenPancla();          
        }
        protected void cmbOficinaPancla_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenacomboCategoProductAncla();
        }
        protected void cmbCategoryAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaSubporCategoProductAncla();
            
        }
        protected void cmbSubcateAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaMarcaenAnclaconSubcategoria();
        }
        protected void cmbMarcaAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaProductoenAncla();
        }
        protected void cmbproductAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaPrecioPAncla();
            LlenaPesoPAncla();
            tooltipnomProducAncla();
        }
        protected void CmbBClientePAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenacomboBuscarCategoryProductAncla();
            ModalPopup_BPAncla.Show();
        }
        protected void CmbBCategoriaPAncla_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaComboOficinaConsultaPancla();
            ModalPopup_BPAncla.Show();
        }
        protected void BtnGuardarAncla_Click(object sender, EventArgs e)
        {
            LblFaltantes.Text = "";

            if (cmbClienteAncla.Text == "0" || cmbCategoryAncla.Text == "0" || cmbSubcateAncla.Text == "0" || cmbMarcaAncla.Text == "0" || cmbproductAncla.Text == "0" || cmbOficinaPancla.Text =="0")
            {
                if (cmbClienteAncla.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Cliente";
                }
                if (cmbCategoryAncla.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Categoria";
                }
                if (cmbSubcateAncla.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Categoria";
                }
                if (cmbMarcaAncla.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Marca";
                }
                if (cmbproductAncla.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Producto";
                }
                if (cmbOficinaPancla.Text == "0")
                {
                    LblFaltantes.Text = ". " + "Oficina";
                }
                
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
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
                DataTable dtconsulta = odconsultaPAncla.ConsultaDuplicadosPancla(ConfigurationManager.AppSettings["AD_ProductosAncla"], Convert.ToInt32(cmbClienteAncla.Text), cmbCategoryAncla.Text, Convert.ToInt64(ssubCategoryP2), Convert.ToInt64(cmbOficinaPancla.Text));
                if (dtconsulta == null)
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
                    EAD_ProductosAncla oePAncla = oPAncla.RegistrarPAncla(Convert.ToInt32(cmbClienteAncla.Text), cmbCategoryAncla.Text, Convert.ToInt64(ssubCategoryP), cmbproductAncla.Text, Convert.ToInt64( cmbOficinaPancla.Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    EProductos oeaPAncla = oPAncla.Actualizar_PrecioLista(cmbproductAncla.Text, Convert.ToDecimal(TxtPrecioprodAncla.Text), Convert.ToString(this.Session["sUser"]), DateTime.Now);     
                    string sCodProduct = "";
                    sCodProduct =  cmbproductAncla.SelectedItem.Text;
                    this.Session["sCodProduct"] = sCodProduct;
                    SavelimpiarControlesProductAncla();
                    //llenarcombos();
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Producto Ancla " + this.Session["sCodProduct"] + " fue creado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesProductAncla();
                    desactivarControlesProductAncla();

                }
                else
                {
                    string sCodProduct = "";
                    sCodProduct = cmbproductAncla.SelectedItem.Text;
                    this.Session["sCodProduct"] = sSubCategoria;
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Producto Ancla " + this.Session["sCodProduct"] + " Ya Existe";
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
        protected void btnBProductAncla_Click(object sender, EventArgs e)
        {
            ModalPopup_BPAncla.Hide();
            desactivarControlesProductAncla();
            LblFaltantes.Text = "";

            if (CmbBClientePAncla.Text == "0" || CmbBCategoriaPAncla.Text == "0" )
            {
                this.Session["mensajealert"] = "Nombre de Cliente y Categoria de Producto";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese parametros de consulta minimo Cliente y Categoria";
                MensajeAlerta();
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

            DataTable oePancla = oPAncla.ConsultarPancla(Convert.ToInt32(sbcliente), sCategoria, Convert.ToInt64(soficina));
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
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    ModalPopup_BPAncla.Show();
                }
            }

        }

        protected void BtnCancelarAncla_Click(object sender, EventArgs e)
        {
            saveActivarbotonesProductAncla();
            desactivarControlesProductAncla();
            SavelimpiarControlesProductAncla();
 
        }              
        protected void cmbcliepancla_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            comboOficinaXGVclienteenPancla(GVConsultaPancla.EditIndex);
            ModalPopancla.Show();
            ModalPopup_BPAncla.Show();
        }
        protected void cmboficipancla_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LlenacomboGVCategoProductAncla(GVConsultaPancla.EditIndex);
            ModalPopancla.Show();
            ModalPopup_BPAncla.Show();
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
            tooltiGVpnomProducAncla(GVConsultaPancla.EditIndex);
            LlenaGVPrecioPAncla(GVConsultaPancla.EditIndex);
            LlenaGVPesoPAncla(GVConsultaPancla.EditIndex);
            ModalPopancla.Show();
        }
        protected void GVConsultaPancla_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            ModalPopancla.Show();
            Button2.Visible=false;
            GVConsultaPancla.EditIndex = e.NewEditIndex;
            string Cliente, Oficina, Categoria, Subcategoria, Marca, Producto, Precio, Peso;
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
            ((TextBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[7].FindControl("cmbsubpesopancla")).Text=Peso;
            ((CheckBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[8].FindControl("Checkpancla")).Checked = estado;
          
            this.Session["rept"] = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[0].FindControl("cmbcliepancla")).Text;
            this.Session["rept1"] = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[2].FindControl("cmbcatepancla")).Text;
            this.Session["rept2"] = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text;
            this.Session["rept3"] = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[1].FindControl("cmboficipancla")).Text;
        }
        protected void GVConsultaPancla_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            Button2.Visible = true;
            GVConsultaPancla.EditIndex = -1;
            DataTable oePancla = oPAncla.ConsultarPancla(Convert.ToInt32(this.Session["sbcliente"].ToString().Trim()), this.Session["sCategoria"].ToString().Trim(), Convert.ToInt64(this.Session["soficina"].ToString().Trim()));
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
        protected void GVConsultaPancla_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
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
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Sr. Usuario, no ingresar Precio con valor 0";
                    MensajeAlerta();
                    return;
                }
            }
            catch
            {
            }

            if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[0].FindControl("cmbcliepancla")).Text == "0" || ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[1].FindControl("cmboficipancla")).Text == "0" || ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[2].FindControl("cmbcatepancla")).Text == "0" || ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text == "0" || ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[4].FindControl("cmbmarcapancla")).Text == "0" || ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text == "0")
            {
                if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[0].FindControl("cmbcliepancla")).Text == "0")
                {
                    LblFaltantes.Text = ". " + "Cliente";
                }
                if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[1].FindControl("cmboficipancla")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Oficina";
                }
                if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[2].FindControl("cmbcatepancla")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Categoria";
                }
                if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "SubCategoria";
                }
                if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[4].FindControl("cmbmarcapancla")).Text == "0")
                {
                    LblFaltantes.Text = ". " + "Marca";
                }
                if (((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text == "0")
                {
                    LblFaltantes.Text = ". " + "Producto";
                }
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }
            try
            {
                
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
                        dato= Convert.ToInt64(this.Session["id_pancla"]);
                        EAD_ProductosAncla oeacPancla = oPAncla.Actualizar_Pancla(Convert.ToInt64(this.Session["id_pancla"]), Convert.ToInt64(((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text), ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        EProductos oeaPAncla = oPAncla.Actualizar_PrecioLista(((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text, Convert.ToDecimal(((TextBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[6].FindControl("cmbsubpreciopancla")).Text), Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        string sCodProduct = "";
                        sCodProduct = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).SelectedItem.Text;
                        this.Session["sCodProduct"] = sCodProduct;
                        SavelimpiarControlesProductAncla();
                        GVConsultaPancla.EditIndex = -1;
                        DataTable oePancla = oPAncla.ConsultarPancla(Convert.ToInt32(this.Session["sbcliente"].ToString().Trim()), this.Session["sCategoria"].ToString().Trim(), Convert.ToInt64(this.Session["soficina"].ToString().Trim()));
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
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "El Producto Ancla " + this.Session["sCodProduct"] + " fue Actualizado con Exito";
                        MensajeAlerta();
                        saveActivarbotonesProductAncla();
                        desactivarControlesProductAncla();
                    }
                    else
                    {
                        GVConsultaPancla.EditIndex = -1;
                        DataTable oePancla = oPAncla.ConsultarPancla(Convert.ToInt32(this.Session["sbcliente"].ToString().Trim()), this.Session["sCategoria"].ToString().Trim(), Convert.ToInt64(this.Session["soficina"].ToString().Trim()));
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
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Producto Ancla " + this.Session["sCodProduct"] + " Ya Existe";
                        MensajeAlerta();
                       
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
                    EAD_ProductosAncla oeacPancla = oPAncla.Actualizar_Pancla(Convert.ToInt64(this.Session["id_pancla"]), Convert.ToInt64(((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[3].FindControl("cmbsubcatepancla")).Text), ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text, estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    EProductos oeaPAncla = oPAncla.Actualizar_PrecioLista(((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).Text, Convert.ToDecimal(((TextBox)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[6].FindControl("cmbsubpreciopancla")).Text), Convert.ToString(this.Session["sUser"]), DateTime.Now);
                    string sCodProduct = "";
                    sCodProduct = ((DropDownList)GVConsultaPancla.Rows[GVConsultaPancla.EditIndex].Cells[5].FindControl("cmbprodupancla")).SelectedItem.Text;
                    this.Session["sCodProduct"] = sCodProduct;
                    SavelimpiarControlesProductAncla();
                    GVConsultaPancla.EditIndex = -1;
                    DataTable oePancla = oPAncla.ConsultarPancla(Convert.ToInt32(this.Session["sbcliente"].ToString().Trim()), this.Session["sCategoria"].ToString().Trim(), Convert.ToInt64(this.Session["soficina"].ToString().Trim()));
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
                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "El Producto Ancla " + this.Session["sCodProduct"] + " fue Actualizado con Exito";
                    MensajeAlerta();
                    saveActivarbotonesProductAncla();
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


        #endregion

        #region Familias
        protected void CmbMarcaFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboSubmarcaFamily();
        }
        protected void cmbCategoryFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaSubporCategoFamily();
        }
        protected void BtnCrearFamily_Click(object sender, EventArgs e)
        {
            try
            {
                this.planningADM = this.Session["AdmProd"].ToString().Trim();
            }
            catch
            {
            }
            if (this.planningADM == "SI")
            {
                comboclienteenFamilyporplanning();
                crearActivarbotonesFamily();
                activarControlesFamily();
            }
            else
            {
                LlenacomboClienteProductFamily();
                crearActivarbotonesFamily();
                activarControlesFamily();
            }
           
        }
        protected void BtnsaveFamily_Click(object sender, EventArgs e)
        {

            TxtNomFamily.Text=TxtNomFamily.Text.TrimStart();
            TxtDescripcionFamily.Text = TxtDescripcionFamily.Text.TrimStart();
            TxtNomFamily.Text = TxtNomFamily.Text.TrimEnd();
            TxtDescripcionFamily.Text = TxtDescripcionFamily.Text.TrimEnd();
            if ( cmbClienteFamily.Text == "0" || CmbMarcaFamily.SelectedValue == "0" || cmbCategoryFamily.SelectedValue == "0" || TxtNomFamily.Text == "" || TxtDescripcionFamily.Text=="" ||txtPesoFamily.Text=="" )
            {
                //if (TxtCodFamily.Text == "0")
                //{
                //    LblFaltantes.Text = LblFaltantes.Text + ". " + "Código de Familia";
                //}
                if (cmbClienteFamily.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }
                if (CmbMarcaFamily.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Marca";
                }
                //if (CmbSubmarcaFamily.Text == "0")
                //{
                //    LblFaltantes.Text = LblFaltantes.Text + ". " + "SubMarca";
                //}
                if (cmbCategoryFamily.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Categoria";
                }
                //if (CmbSubCategoryFamily.Text == "0")
                //{
                //    LblFaltantes.Text = LblFaltantes.Text + ". " + "SubCategoria";
                //}
                if (TxtNomFamily.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Familia";
                }
                if (TxtDescripcionFamily.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                if (txtPesoFamily.Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Peso";
                }
               
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                return;
            }

            try
            {
       

                DAplicacion odcConsuFamily = new DAplicacion();
                DataTable dtconsulta = odcConsuFamily.ConsultaDuplicados(ConfigurationManager.AppSettings["ProductFamily"], TxtCodFamily.Text.ToUpper(), CmbMarcaFamily.Text.ToUpper(), TxtNomFamily.Text.ToUpper());

              
                    if (dtconsulta == null)
                    {
                        string ssubmarcaf="";
                        if (CmbSubmarcaFamily.Text == "n")
                        {
                            ssubmarcaf = "0";
                        }
                        else
                        {
                            ssubmarcaf = CmbSubmarcaFamily.Text;
                        }

                        string ssubCategoriaf = "";
                        if (CmbSubCategoryFamily.Text == "n")
                        {
                            ssubCategoriaf = "0";
                        }
                        else
                        {
                            ssubCategoriaf = CmbSubCategoryFamily.Text;
                        }
                        try
                        {
                        EProduct_Family oFamily = oProductFamily.RegistrarProductosFamily(TxtCodFamily.Text.ToUpper(), cmbCategoryFamily.SelectedValue.ToString().Trim(), Convert.ToInt64(ssubCategoriaf), Convert.ToInt32(CmbMarcaFamily.SelectedValue.ToString().Trim()), Convert.ToInt32(ssubmarcaf), TxtNomFamily.Text.ToUpper(), TxtDescripcionFamily.Text, Convert.ToDecimal( txtPesoFamily.Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        consultaUltimoIdfamilia();
                        EProduct_Family oFamilytmp = oProductFamily.RegistrarProductosFamilyTMP(TxtCodFamily.Text.ToUpper(), Convert.ToInt32(CmbMarcaFamily.SelectedValue.ToString().Trim()), Convert.ToInt32(ssubmarcaf), TxtNomFamily.Text.ToUpper(), true, cmbCategoryFamily.SelectedValue.ToString().Trim());
                        string sProductFamily = "";
                        sProductFamily = TxtNomFamily.Text;
                        this.Session["sProductFamily"] = sProductFamily;
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La familia de producto " + this.Session["sProductFamily"] + " fue creada con éxito";
                        MensajeAlerta();
                        saveActivarbotonesFamily();
                        SavelimpiarControlesFamily();
                        desactivarControlesFamily();
                        }
                        catch(Exception ex)
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "La familia de producto " + this.Session["sProductFamily"] + " ya existe";
                            MensajeAlerta();
                        }

                    }
                    else
                    {
                        string sProductFamily = "";
                        if (dtconsulta != null)
                        {
                            sProductFamily = TxtNomFamily.Text;
                        }
                     
                        this.Session["sProductFamily"] = sProductFamily;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La familia de producto " + this.Session["sProductFamily"] + " ya existe";
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
        protected void cmbClienteFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenacomboCategFamily(Convert.ToInt32(cmbClienteFamily.Text));
                   
        }
        protected void CmbSubCategoryFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaMarcaFamily();
        }
        protected void CmbBClientFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            LlenaCategoriaBuscarFamily();
            IbtnFamily.Show();

        }
        protected void cmbCategoryBFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.planningADM = this.Session["AdmProd"].ToString().Trim();
            }
            catch
            {
            }
            if (this.planningADM == "SI")
            {
                llenacomboMarcaaxCategoriaenBusFamily();
                IbtnFamily.Show();
            }
            else
            {
                LlenaMarcaBuscarFamily();
                IbtnFamily.Show();
            }
           
        }
        protected void BtnBFamily_Click(object sender, EventArgs e)
        {
            IbtnFamily.Hide();
            desactivarControlesFamily();
            LblFaltantes.Text = "";

            if (CmbBClientFamily.Text == "0" && CmbBMarcaFamily.Text == "0" && cmbCategoryBFamily.Text == "0" && TxtBNomFamily.Text == "")
            {
                this.Session["mensajealert"] = "Cliente y/o Marcar y/o Nombre de Familia";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parámetro de consulta";
                MensajeAlerta();
                ModalPopup_BPAncla.Show();
                return;
            }

            BuscarActivarbotnesFamily();
            sbcliente = CmbBClientFamily.Text;
            if (CmbBMarcaFamily.Items.Count > 0)
            {
                sMarca = Convert.ToInt32(CmbBMarcaFamily.Text);
            }
            else
            {
                sMarca = 0;
            }
            if (cmbCategoryBFamily.Items.Count > 0)
            {
                sCategoriaFamily = cmbCategoryBFamily.Text;
            }
            else
            {
                sCategoriaFamily = "0";
            }
            sNombreFamilia = TxtBNomFamily.Text;
            CmbBClientFamily.Text = "0";
            CmbBMarcaFamily.Text = "0";
            cmbCategoryBFamily.Text = "0";
            TxtBNomFamily.Text = "";

            this.Session["sbcliente"] = sbcliente;
            this.Session["sMarca"] = sMarca;
            this.Session["sCategoriaFamily"] = sCategoriaFamily;
            this.Session["sNombreFamilia"] = sNombreFamilia;

            DataTable oPFamily = oProductFamily.ConsultarFamily(Convert.ToInt32(sbcliente), Convert.ToInt32(sMarca), sCategoriaFamily, sNombreFamilia);
            this.Session["cFamily"] = oPFamily;
            
            if (oPFamily != null)
            {
                if (oPFamily.Rows.Count > 0)
                {
                    GvConsFamilia.DataSource = oPFamily;
                    GvConsFamilia.DataBind();
                    ModalPOPFamilia.Show();

                    for (int i = 0; i <= oPFamily.Rows.Count - 1; i++)
                    {
                        try
                        {
                            LlenacomboGVClienteProductFamily(i);
                            ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).Text = oPFamily.Rows[i][0].ToString().Trim();
                            LlenacomboGVCategFamily(i);
                            ((DropDownList)GvConsFamilia.Rows[i].Cells[2].FindControl("cmbcatefamilia")).Text = oPFamily.Rows[i][5].ToString().Trim();
                            LlenaGVSubporCategoFamily(i);
                            ((DropDownList)GvConsFamilia.Rows[i].Cells[3].FindControl("cmbsubcatefamilia")).Text = oPFamily.Rows[i][6].ToString().Trim();
                            LlenaGVMarcaFamily(i);
                            ((DropDownList)GvConsFamilia.Rows[i].Cells[4].FindControl("cmbmarcafamilia")).Text = oPFamily.Rows[i][2].ToString().Trim();
                            comboGVSubmarcaFamily(i);
                            ((DropDownList)GvConsFamilia.Rows[i].Cells[5].FindControl("cmbSubmarcafamilia")).Text = oPFamily.Rows[i][3].ToString().Trim();                           
                        }
                        catch (Exception ex) { }
                    }

                    this.Session["Exportar_Excel"] = "Exportar_Familia";

                    DataTable dtnameCombosfamilia = new DataTable();
                    dtnameCombosfamilia.Columns.Add("Código", typeof(String));
                    dtnameCombosfamilia.Columns.Add("Cliente", typeof(String));
                    dtnameCombosfamilia.Columns.Add("Categoria", typeof(String));
                    dtnameCombosfamilia.Columns.Add("Subcategoria", typeof(String));
                    dtnameCombosfamilia.Columns.Add("Marca", typeof(String));
                    dtnameCombosfamilia.Columns.Add("SubMarca", typeof(String));
                    dtnameCombosfamilia.Columns.Add("Nombre", typeof(String));
                    dtnameCombosfamilia.Columns.Add("Descripción", typeof(String));
                    dtnameCombosfamilia.Columns.Add("Peso", typeof(String));
                    dtnameCombosfamilia.Columns.Add("Estado", typeof(String));
                    
                    for (int i = 0; i <= GvConsFamilia.Rows.Count - 1; i++)
                    {
                        try
                        {
                            DataRow dr = dtnameCombosfamilia.NewRow();
                            dr["Código"] = ((Label)GvConsFamilia.Rows[i].Cells[0].FindControl("txtCodigo")).Text;
                            dr["Cliente"] = ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).SelectedItem.Text;
                            dr["Categoria"] = ((DropDownList)GvConsFamilia.Rows[i].Cells[2].FindControl("cmbcatefamilia")).SelectedItem.Text;
                            dr["Subcategoria"] = ((DropDownList)GvConsFamilia.Rows[i].Cells[3].FindControl("cmbsubcatefamilia")).SelectedItem.Text;
                            dr["Marca"] = ((DropDownList)GvConsFamilia.Rows[i].Cells[4].FindControl("cmbmarcafamilia")).SelectedItem.Text;
                            dr["SubMarca"] = ((DropDownList)GvConsFamilia.Rows[i].Cells[5].FindControl("cmbSubmarcafamilia")).SelectedItem.Text;
                            dr["Nombre"] = ((Label)GvConsFamilia.Rows[i].Cells[6].FindControl("txtNomFamilia")).Text;
                            dr["Descripción"] = ((Label)GvConsFamilia.Rows[i].Cells[7].FindControl("txtDescripFamilia")).Text;
                            dr["Peso"] = ((Label)GvConsFamilia.Rows[i].Cells[8].FindControl("cmbsubpesofamilia")).Text;
                            dr["Estado"] = ((CheckBox)GvConsFamilia.Rows[i].Cells[9].FindControl("CheckFamilia")).Checked;

                            dtnameCombosfamilia.Rows.Add(dr);
                        }
                        catch (Exception ex) { }
                    }

                    this.Session["CExporFamilia"] = dtnameCombosfamilia;                    
                }
                else
                {
                    SavelimpiarControlesFamily();
                    saveActivarbotonesFamily();
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = " la consulta realizada no arrojo ninguna respuesta";
                    MensajeAlerta();
                    ModalPopup_BPAncla.Show();
                }
            }
        }
        protected void BtnCancelFamily_Click(object sender, EventArgs e)
        {
            saveActivarbotonesFamily();
            desactivarControlesFamily();
            SavelimpiarControlesFamily();
        }       
        protected void cmbcliefamilia_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LlenacomboGVCategFamily(GvConsFamilia.EditIndex);
            ModalPOPFamilia.Show();
        }
        protected void cmbcatefamilia_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            LlenaGVSubporCategoFamily(GvConsFamilia.EditIndex);
            LlenaGVMarcaFamily(GvConsFamilia.EditIndex);
            ModalPOPFamilia.Show();
        }
        protected void cmbsubcatefamilia_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //LlenaGVMarcaFamily(GvConsFamilia.EditIndex);
             ModalPOPFamilia.Show();
        }
        protected void cmbSubmarcafamilia_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ModalPOPFamilia.Show();
        }        
        protected void cmbmarcafamilia_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            comboGVSubmarcaFamily(GvConsFamilia.EditIndex);
            ModalPOPFamilia.Show();
        }
        protected void GvConsFamilia_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            ModalPOPFamilia.Show();
            BtnGVCfamilia.Visible = false;
            GvConsFamilia.EditIndex = e.NewEditIndex;
            string Código, Cliente,  Categoria, Subcategoria, Marca, SubMarca, Nombre, Descripción, Peso;
            bool estado;

            Código = ((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[0].FindControl("txtCodigo")).Text;
            Cliente = ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[1].FindControl("cmbcliefamilia")).Text;
            Categoria = ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[2].FindControl("cmbcatefamilia")).Text;
            Subcategoria = ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[3].FindControl("cmbsubcatefamilia")).Text;
            Marca = ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).Text;
            SubMarca = ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[5].FindControl("cmbSubmarcafamilia")).Text;
            Nombre = ((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text;
            Descripción = ((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[7].FindControl("txtDescripFamilia")).Text;
            Peso = ((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[8].FindControl("cmbsubpesofamilia")).Text;
            estado = ((CheckBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[9].FindControl("CheckFamilia")).Checked;
            DataTable oPFamily = (DataTable)this.Session["cFamily"];
            GvConsFamilia.DataSource = oPFamily;
            GvConsFamilia.DataBind();
            
            LlenacomboGVClienteProductFamily(GvConsFamilia.EditIndex);
            ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[1].FindControl("cmbcliefamilia")).Text = oPFamily.Rows[GvConsFamilia.EditIndex][0].ToString().Trim();
            LlenacomboGVCategFamily(GvConsFamilia.EditIndex);
            ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[2].FindControl("cmbcatefamilia")).Text = oPFamily.Rows[GvConsFamilia.EditIndex][5].ToString().Trim();
            LlenaGVSubporCategoFamily(GvConsFamilia.EditIndex);
            ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[3].FindControl("cmbsubcatefamilia")).Text = oPFamily.Rows[GvConsFamilia.EditIndex][6].ToString().Trim();
            LlenaGVMarcaFamily(GvConsFamilia.EditIndex);
            ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).Text = oPFamily.Rows[GvConsFamilia.EditIndex][2].ToString().Trim();
            comboGVSubmarcaFamily(GvConsFamilia.EditIndex);
            ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[5].FindControl("cmbSubmarcafamilia")).Text = oPFamily.Rows[GvConsFamilia.EditIndex][3].ToString().Trim();
            
            ((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[0].FindControl("txtCodigo")).Text = Código;
            LlenacomboGVClienteProductFamily(GvConsFamilia.EditIndex);
            ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[1].FindControl("cmbcliefamilia")).Items.FindByValue(Cliente).Selected = true;
            LlenacomboGVCategFamily(GvConsFamilia.EditIndex);
            ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[2].FindControl("cmbcatefamilia")).Items.FindByValue(Categoria).Selected = true;
            LlenaGVSubporCategoFamily(GvConsFamilia.EditIndex);
            ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[3].FindControl("cmbsubcatefamilia")).Items.FindByValue(Subcategoria).Selected = true;
            LlenaGVMarcaFamily(GvConsFamilia.EditIndex);
            ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).Items.FindByValue(Marca).Selected = true;
            comboGVSubmarcaFamily(GvConsFamilia.EditIndex);
            ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[5].FindControl("cmbSubmarcafamilia")).Items.FindByValue(SubMarca).Selected = true;
            ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text = Nombre;
            ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[7].FindControl("txtDescripFamilia")).Text = Descripción;
            ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[8].FindControl("cmbsubpesofamilia")).Text = Peso;
            ((CheckBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[9].FindControl("CheckFamilia")).Checked = estado;


            this.Session["rept"] = ((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[0].FindControl("txtCodigo")).Text;
            this.Session["rept1"] = ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).Text;
            this.Session["rept2"] = ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text;
           
        }
        protected void GvConsFamilia_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            BtnGVCfamilia.Visible = true;
            GvConsFamilia.EditIndex = -1;
            DataTable oPFamily = oProductFamily.ConsultarFamily(Convert.ToInt32(this.Session["sbcliente"].ToString().Trim()), Convert.ToInt32(this.Session["sMarca"].ToString().Trim()), this.Session["sCategoriaFamily"].ToString().Trim(), this.Session["sNombreFamilia"].ToString().Trim());
            this.Session["cFamily"] = oPFamily;

            if (oPFamily != null)
            {
                if (oPFamily.Rows.Count > 0)
                {

                    GvConsFamilia.DataSource = oPFamily;
                    GvConsFamilia.DataBind();
                    ModalPOPFamilia.Show();

                    for (int i = 0; i <= oPFamily.Rows.Count - 1; i++)
                    {
                        try
                        {
                            LlenacomboGVClienteProductFamily(i);
                            ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).Text = oPFamily.Rows[i][0].ToString().Trim();
                            LlenacomboGVCategFamily(i);
                            ((DropDownList)GvConsFamilia.Rows[i].Cells[2].FindControl("cmbcatefamilia")).Text = oPFamily.Rows[i][5].ToString().Trim();
                            LlenaGVSubporCategoFamily(i);
                            ((DropDownList)GvConsFamilia.Rows[i].Cells[3].FindControl("cmbsubcatefamilia")).Text = oPFamily.Rows[i][6].ToString().Trim();
                            LlenaGVMarcaFamily(i);
                            ((DropDownList)GvConsFamilia.Rows[i].Cells[4].FindControl("cmbmarcafamilia")).Text = oPFamily.Rows[i][2].ToString().Trim();
                            comboGVSubmarcaFamily(i);
                            ((DropDownList)GvConsFamilia.Rows[i].Cells[5].FindControl("cmbSubmarcafamilia")).Text = oPFamily.Rows[i][3].ToString().Trim();

                        }
                        catch (Exception ex) { }

                    }
                }
            }

        }
        protected void GvConsFamilia_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {              

            BtnGVCfamilia.Visible = true;
            if (((CheckBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[9].FindControl("CheckFamilia")).Checked != false)
            {
                estado = true;
            }
            else
            {
                estado = false;
            }


            if (((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[0].FindControl("txtCodigo")).Text == "" || ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[1].FindControl("cmbcliefamilia")).Text == "0" || ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).SelectedValue == "0" || ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[2].FindControl("cmbcatefamilia")).SelectedValue == "0" || ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text == "" || ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[7].FindControl("txtDescripFamilia")).Text == "" || ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[8].FindControl("cmbsubpesofamilia")).Text == "")
            {
                if (((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[0].FindControl("txtCodigo")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Código de Familia";
                }
                if (((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[1].FindControl("cmbcliefamilia")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Cliente";
                }
                if (((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Marca";
                }

                if (((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[2].FindControl("cmbcatefamilia")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Categoria";
                }

                if (((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Nombre de Familia";
                }
                if (((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[7].FindControl("txtDescripFamilia")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Descripción";
                }
                if (((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[8].FindControl("cmbsubpesofamilia")).Text == "0")
                {
                    LblFaltantes.Text = LblFaltantes.Text + ". " + "Peso";
                }

                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar todos los campos con (*)";
                MensajeAlerta();
                ModalPOPFamilia.Show();
                return;
            }
            try
            {

                repetido = Convert.ToString(this.Session["rept"]);
                repetido1 = Convert.ToString(this.Session["rept1"]);
                repetido2 = Convert.ToString(this.Session["rept2"]);
                if (repetido != ((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[0].FindControl("txtCodigo")).Text || repetido1 != ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).Text || repetido2 != ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text)
                {
                    DAplicacion odcConsuFamily = new DAplicacion();
                    DataTable dtconsulta = odcConsuFamily.ConsultaDuplicados(ConfigurationManager.AppSettings["ProductFamily"], ((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[0].FindControl("txtCodigo")).Text.ToUpper(), ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).Text.ToUpper(), ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text.ToUpper());
                    if (dtconsulta == null)
                    {
                        //string ssubmarcaf = "";
                        //if (CmbSubmarcaFamily.Text == "n")
                        //{
                        //    ssubmarcaf = "0";
                        //}
                        //else
                        //{
                        //    ssubmarcaf = CmbSubmarcaFamily.Text;
                        //}
                        //string ssubCategoriaf = "";
                        //if (CmbSubCategoryFamily.Text == "n")
                        //{
                        //    ssubCategoriaf = "0";
                        //}
                        //else
                        //{
                        //    ssubCategoriaf = CmbSubCategoryFamily.Text;
                        //}

                        try
                        {
                            EProduct_Family oPfamily = oProductFamily.Actualizar_PFamily(((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[0].FindControl("txtCodigo")).Text, ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[2].FindControl("cmbcatefamilia")).Text, Convert.ToInt64(((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[3].FindControl("cmbsubcatefamilia")).Text), Convert.ToInt32(((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).Text), Convert.ToInt32(((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[5].FindControl("cmbSubmarcafamilia")).Text), ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text, ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[7].FindControl("txtDescripFamilia")).Text, Convert.ToDecimal(((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[8].FindControl("cmbsubpesofamilia")).Text), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            EProduct_Family oPfamilytmp = oProductFamily.Actualizar_PFamilyTMP(((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[0].FindControl("txtCodigo")).Text, Convert.ToInt32(((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).Text), Convert.ToInt32(((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[5].FindControl("cmbSubmarcafamilia")).Text), ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text, estado);
                            //string sProductFamily = "";
                            //sProductFamily = ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text;
                            //this.Session["sProductFamily"] = sProductFamily;
                            SavelimpiarControlesFamily();
                            GvConsFamilia.EditIndex = -1;
                            DataTable oPFamily = oProductFamily.ConsultarFamily(Convert.ToInt32(this.Session["sbcliente"].ToString().Trim()), Convert.ToInt32(this.Session["sMarca"].ToString().Trim()), this.Session["sCategoriaFamily"].ToString().Trim(), this.Session["sNombreFamilia"].ToString().Trim());
                            this.Session["cFamily"] = oPFamily;

                            if (oPFamily != null)
                            {
                                if (oPFamily.Rows.Count > 0)
                                {

                                    GvConsFamilia.DataSource = oPFamily;
                                    GvConsFamilia.DataBind();
                                    ModalPOPFamilia.Show();

                                    for (int i = 0; i <= oPFamily.Rows.Count - 1; i++)
                                    {
                                        try
                                        {
                                            LlenacomboGVClienteProductFamily(i);
                                            ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).Text = oPFamily.Rows[i][0].ToString().Trim();
                                            LlenacomboGVCategFamily(i);
                                            ((DropDownList)GvConsFamilia.Rows[i].Cells[2].FindControl("cmbcatefamilia")).Text = oPFamily.Rows[i][5].ToString().Trim();
                                            LlenaGVSubporCategoFamily(i);
                                            ((DropDownList)GvConsFamilia.Rows[i].Cells[3].FindControl("cmbsubcatefamilia")).Text = oPFamily.Rows[i][6].ToString().Trim();
                                            LlenaGVMarcaFamily(i);
                                            ((DropDownList)GvConsFamilia.Rows[i].Cells[4].FindControl("cmbmarcafamilia")).Text = oPFamily.Rows[i][2].ToString().Trim();
                                            comboGVSubmarcaFamily(i);
                                            ((DropDownList)GvConsFamilia.Rows[i].Cells[5].FindControl("cmbSubmarcafamilia")).Text = oPFamily.Rows[i][3].ToString().Trim();

                                        }
                                        catch (Exception ex) { }

                                    }
                                }
                            }

                            // LlenacombofamiliaProducto();
                            Alertas.CssClass = "MensajesCorrecto";
                            LblFaltantes.Text = "La Familia de Producto : " + this.Session["sProductFamily"] + " Se Actualizo Corecctamente";
                            MensajeAlerta();
                            saveActivarbotonesFamily();
                            desactivarControlesFamily();
                        }
                     
                        catch
                    {
                        ModalPOPFamilia.Show();
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Peso de la Familia debe ser Númerico";
                        MensajeAlerta();
                    }
                    }

                    else
                    {
                        GvConsFamilia.EditIndex = -1;
                        DataTable oPFamily = oProductFamily.ConsultarFamily(Convert.ToInt32(this.Session["sbcliente"].ToString().Trim()), Convert.ToInt32(this.Session["sMarca"].ToString().Trim()), this.Session["sCategoriaFamily"].ToString().Trim(), this.Session["sNombreFamilia"].ToString().Trim());
                        this.Session["cFamily"] = oPFamily;

                        if (oPFamily != null)
                        {
                            if (oPFamily.Rows.Count > 0)
                            {

                                GvConsFamilia.DataSource = oPFamily;
                                GvConsFamilia.DataBind();
                                ModalPOPFamilia.Show();

                                for (int i = 0; i <= oPFamily.Rows.Count - 1; i++)
                                {
                                    try
                                    {
                                        LlenacomboGVClienteProductFamily(i);
                                        ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).Text = oPFamily.Rows[i][0].ToString().Trim();
                                        LlenacomboGVCategFamily(i);
                                        ((DropDownList)GvConsFamilia.Rows[i].Cells[2].FindControl("cmbcatefamilia")).Text = oPFamily.Rows[i][5].ToString().Trim();
                                        LlenaGVSubporCategoFamily(i);
                                        ((DropDownList)GvConsFamilia.Rows[i].Cells[3].FindControl("cmbsubcatefamilia")).Text = oPFamily.Rows[i][6].ToString().Trim();
                                        LlenaGVMarcaFamily(i);
                                        ((DropDownList)GvConsFamilia.Rows[i].Cells[4].FindControl("cmbmarcafamilia")).Text = oPFamily.Rows[i][2].ToString().Trim();
                                        comboGVSubmarcaFamily(i);
                                        ((DropDownList)GvConsFamilia.Rows[i].Cells[5].FindControl("cmbSubmarcafamilia")).Text = oPFamily.Rows[i][3].ToString().Trim();

                                    }
                                    catch (Exception ex) { }

                                }
                            }
                        }

                        //string sProductFamily = "";
                        //sProductFamily = ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text;
                        //this.Session["sProductFamily"] = sProductFamily;
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "La Familia de Producto : " + this.Session["sProductFamily"] + " Ya Existe";
                        MensajeAlerta();
                    }

                }
                else
                {
                    //string ssubmarcaf = "";
                    //if (CmbSubmarcaFamily.Text == "n")
                    //{
                    //    ssubmarcaf = "0";
                    //}
                    //else
                    //{
                    //    ssubmarcaf = CmbSubmarcaFamily.Text;
                    //}
                    //string ssubCategoriaf = "";
                    //if (CmbSubCategoryFamily.Text == "n")
                    //{
                    //    ssubCategoriaf = "0";
                    //}
                    //else
                    //{
                    //    ssubCategoriaf = CmbSubCategoryFamily.Text;
                    //}
                    try
                    {
                        EProduct_Family oPfamily = oProductFamily.Actualizar_PFamily(((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[0].FindControl("txtCodigo")).Text, ((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[2].FindControl("cmbcatefamilia")).Text, Convert.ToInt64(((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[3].FindControl("cmbsubcatefamilia")).Text), Convert.ToInt32(((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).Text), Convert.ToInt32(((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[5].FindControl("cmbSubmarcafamilia")).Text), ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text, ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[7].FindControl("txtDescripFamilia")).Text, Convert.ToDecimal(((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[8].FindControl("cmbsubpesofamilia")).Text), estado, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        EProduct_Family oPfamilytmp = oProductFamily.Actualizar_PFamilyTMP(((Label)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[0].FindControl("txtCodigo")).Text, Convert.ToInt32(((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[4].FindControl("cmbmarcafamilia")).Text), Convert.ToInt32(((DropDownList)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[5].FindControl("cmbSubmarcafamilia")).Text), ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text, estado);
                        string sProductFamily = "";
                        sProductFamily = ((TextBox)GvConsFamilia.Rows[GvConsFamilia.EditIndex].Cells[6].FindControl("txtNomFamilia")).Text;
                        this.Session["sProductFamily"] = sProductFamily;
                        SavelimpiarControlesFamily();
                        GvConsFamilia.EditIndex = -1;
                        DataTable oPFamily = oProductFamily.ConsultarFamily(Convert.ToInt32(this.Session["sbcliente"].ToString().Trim()), Convert.ToInt32(this.Session["sMarca"].ToString().Trim()), this.Session["sCategoriaFamily"].ToString().Trim(), this.Session["sNombreFamilia"].ToString().Trim());
                        this.Session["cFamily"] = oPFamily;

                        if (oPFamily != null)
                        {
                            if (oPFamily.Rows.Count > 0)
                            {
                                GvConsFamilia.DataSource = oPFamily;
                                GvConsFamilia.DataBind();
                                ModalPOPFamilia.Show();

                                for (int i = 0; i <= oPFamily.Rows.Count - 1; i++)
                                {
                                    try
                                    {
                                        LlenacomboGVClienteProductFamily(i);
                                        ((DropDownList)GvConsFamilia.Rows[i].Cells[1].FindControl("cmbcliefamilia")).Text = oPFamily.Rows[i][0].ToString().Trim();
                                        LlenacomboGVCategFamily(i);
                                        ((DropDownList)GvConsFamilia.Rows[i].Cells[2].FindControl("cmbcatefamilia")).Text = oPFamily.Rows[i][5].ToString().Trim();
                                        LlenaGVSubporCategoFamily(i);
                                        ((DropDownList)GvConsFamilia.Rows[i].Cells[3].FindControl("cmbsubcatefamilia")).Text = oPFamily.Rows[i][6].ToString().Trim();
                                        LlenaGVMarcaFamily(i);
                                        ((DropDownList)GvConsFamilia.Rows[i].Cells[4].FindControl("cmbmarcafamilia")).Text = oPFamily.Rows[i][2].ToString().Trim();
                                        comboGVSubmarcaFamily(i);
                                        ((DropDownList)GvConsFamilia.Rows[i].Cells[5].FindControl("cmbSubmarcafamilia")).Text = oPFamily.Rows[i][3].ToString().Trim();
                                    }
                                    catch (Exception ex) { }
                                }
                            }
                        }

                        //LlenacombofamiliaProducto();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Marca de Producto : " + this.Session["sProductFamily"] + " se actualizó correctamente";
                        MensajeAlerta();
                        saveActivarbotonesFamily();
                        desactivarControlesFamily();
                    }

                    catch
                    {
                        ModalPOPFamilia.Show();
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "El Peso de la Familia debe ser numérico";
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
        #endregion         

        #region Sub Familias
        protected void btncreasubfam_Click(object sender, EventArgs e)
        {
            habilitarControlesSubFamily();
            llenarcomboclienteencrearsubfam(ddl_sf_cliente);
        }

        private void llenarcomboclienteencrearsubfam(DropDownList ddl)
        {
            DataSet ds = null;
            ds = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 4);
            //se llena cliente en Usuarios
            ddl.DataSource = ds;
            ddl.DataTextField = "Company_Name";
            ddl.DataValueField = "Company_id";
            ddl.DataBind();
        }

        private void llenarcombocategoriaencrearsubfam(DropDownList ddl, int cliente)
        {
            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEXPLORA_CLIEN_V2_LLENACOMBOS", cliente, "", "", 2);
            //se llena categorias en tipo de producto
            ddl.DataSource = dt;
            ddl.DataTextField = "Name_Catego";
            ddl.DataValueField = "cod_catego";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            dt = null;
        }

        protected void ddl_sf_categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarcombosubcategoriaencrearsubfam();
            llenarcombomarcaencrearsubfam(ddl_sf_marca,Convert.ToInt32(ddl_sf_cliente.SelectedValue), ddl_sf_categoria.SelectedValue.ToString());
           
        }

        protected void ddl_sf_cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarcombocategoriaencrearsubfam(ddl_sf_categoria,Convert.ToInt32(ddl_sf_cliente.Text));
        }

        private void llenarcombosubcategoriaencrearsubfam()
        {
            DataTable dt1 = new DataTable();
            dt1 = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOSUBCATEGORIAPRESENT", ddl_sf_categoria.SelectedValue);
            //se llena Combo subcategoria segun categoria
            ddl_sf_subcategoria.DataSource = dt1;
            ddl_sf_subcategoria.DataTextField = "Name_Subcategory";
            ddl_sf_subcategoria.DataValueField = "id_Subcategory";
            ddl_sf_subcategoria.DataBind();
            // se modifica sp para q muestre el item seleccione desde sql para evitar error en consulta. Ing. Mauricio Ortiz 29/09/2010
            //CmbSubCategoriaProduct.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            if (CmbSubCategoryFamily.Items.Count == 1)
            {
                ddl_sf_subcategoria.Items.Clear();
                ddl_sf_subcategoria.Items.Insert(0, new ListItem("<No Aplica>", "n"));
                ddl_sf_subcategoria.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            }


            dt1 = null;
        }

        private void llenarcombomarcaencrearsubfam(DropDownList ddl, int cliente, string categoria)
        {
            DataTable ds = new DataTable();
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_LLENACOMBOMARCABUSCARFAMILY", cliente,categoria);
            //se llena Combo de marca en buscar de maestro familia de producto
            ddl.DataSource = dt;
            ddl.DataTextField = "Name_Brand";
            ddl.DataValueField = "id_Brand";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            ds = null;
        }

        protected void ddl_sf_marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarcombosubmarcencrearsubfam();
            llenarcombofamiliaencrearsubfam(ddlfamilias, Convert.ToInt32(ddl_sf_cliente.Text), Convert.ToInt32(ddl_sf_marca.Text), Convert.ToInt32(ddl_sf_categoria.Text));
        }

        private void llenarcombosubmarcencrearsubfam()
        {
            DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEB_LLENACOMBOSUBMARCAPRODFAMILY", ddl_sf_marca.SelectedValue);

                //Se llena submarcas de productos en productos
                ddl_sf_submarca.DataSource = dt;
                ddl_sf_submarca.DataTextField = "Name_SubBrand";
                ddl_sf_submarca.DataValueField = "id_SubBrand";
                ddl_sf_submarca.DataBind();
                dt = null;
        }

        private void llenarcombofamiliaencrearsubfam(DropDownList ddl, int cliente, int marca, int categoria)
        {
            
            DataTable dt1 = new DataTable();
            dt1 = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_CONSULTAFAMILY", cliente, marca, categoria, "");
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

        protected void btncancelsubfam_Click(object sender, EventArgs e)
        {
            deshabilitarControlesSubFamily();
        }

        protected void btnbuscarsubfam_Click(object sender, EventArgs e)
        {
            if (ddl_bsf_cliente.SelectedIndex != 0 && ddl_bsf_categoria.SelectedIndex != 0)
            {
                int codcatego = Convert.ToInt32(ddl_bsf_categoria.Text);
                int codcliente = Convert.ToInt32(ddl_bsf_cliente.Text);
                int codmarca = Convert.ToInt32(ddl_bsf_marca.Text);
                string codfam = ddl_bsubfam.Text;                
                string nomsubfam = txt_bsubfam.Text;

                DataTable dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_BUSCAR_SUBFAMILY", codcliente, codcatego, codmarca, codfam, nomsubfam);
                if (dt != null)
                {
                    popup_grid_consultasubfam.Show();
                    this.Session["dt_subfamilias"] = dt;
                    grid_subfamily.EditIndex = -1;
                    llenagridsubfamilias(dt);
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "No se encontraron registros para esta consulta.";
                    MensajeAlerta();
                }

            }
            else
            {                
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe seleccionar Cliente y Categoría.";
                MensajeAlerta();                
            }
        }

        protected void btnguardasubfam_Click(object sender, EventArgs e)
        {
            string codfam = ddlfamilias.Text;
            string nomsubfam = txtnomsubfamilia.Text;
            bool estado = true;
            if (!txtnomsubfamilia.Text.Equals("") && ddlfamilias.Items.Count != 0)
            {
                DataTable dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_BUSCAR_SUBFAMILY", 0, 0, 0, codfam, nomsubfam);
                //this.Session["ObjSubFamily"] = dt;
                if (dt.Rows.Count == 0)
                {
                    try
                    {
                        DataTable dtl =oConn.ejecutarDataTable("UP_WEBXPLORA_AD_REGISTERSUBFAMILY", codfam, nomsubfam, estado, Convert.ToString(this.Session["sUser"]));


                        string id_Brand,id_SubBrand,id_ProductFamily,id_ProductSubFamily,subfam_nombre,subfam_status;
                        

                        id_Brand=dtl.Rows[0]["id_Brand"].ToString();
                        if(dtl.Rows[0]["id_SubBrand"].ToString() != "")
                        {
                            id_SubBrand=dtl.Rows[0]["id_SubBrand"].ToString();
                        }
                        else
                        {
                            id_SubBrand="0";
                        }

                        
                        id_ProductFamily=dtl.Rows[0]["id_ProductFamily"].ToString();
                        id_ProductSubFamily=dtl.Rows[0]["id_ProductSubFamily"].ToString();
                        subfam_nombre=dtl.Rows[0]["subfam_nombre"].ToString();
                        subfam_status =Convert.ToInt32(Convert.ToBoolean(dtl.Rows[0]["subfam_status"])).ToString();


                        Conexion cn = new Conexion(2);
                        cn.ejecutarDataTable("STP_JVM_INSERTAR_TBL_SUBFAMILIA", id_ProductSubFamily, id_Brand, id_SubBrand, id_ProductFamily, subfam_nombre, subfam_status);

                        string sSubFamilia = "";
                        sSubFamilia = txtnomsubfamilia.Text;
                        this.Session["sSubFamilia"] = sSubFamilia;
                        //llenarcombos();
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Sub Familia " + txtnomsubfamilia.Text + " fue creada con éxito";
                        MensajeAlerta();
                        limpiarcontroles();
                        deshabilitarControlesSubFamily();
                        //habilitarControlesSubFamily();
                    }
                    catch (Exception ex)
                    {
                        Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                        string errMessage = "";
                        errMessage = exs.ToString();
                        errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                        this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                    }
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = txtnomsubfamilia.Text + ": Ya existe esta subfamilia";
                    MensajeAlerta();
                }
            }
            else 
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Debe ingresar los parámetros requeridos";
                MensajeAlerta();
            } 
        }

        private void llenacombofam_bsubfam(int editado)
        {
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)this.Session["dt_familias"];
            //se llena familias

            ((DropDownList)grid_subfamily.Rows[editado].FindControl("ddl_bsubfam_family")).DataSource = dt1;
            ((DropDownList)grid_subfamily.Rows[editado].FindControl("ddl_bsubfam_family")).DataTextField = "name_Family";
            ((DropDownList)grid_subfamily.Rows[editado].FindControl("ddl_bsubfam_family")).DataValueField = "id_ProductFamily";
            ((DropDownList)grid_subfamily.Rows[editado].FindControl("ddl_bsubfam_family")).DataBind();
            dt1 = null;
        }



        protected void ddl_bsf_cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarcombocategoriaencrearsubfam(ddl_bsf_categoria, Convert.ToInt32(ddl_bsf_cliente.Text));
            ModalBuscarSubFamily.Show();
        }

        protected void ddl_bsf_categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarcombomarcaencrearsubfam(ddl_bsf_marca, Convert.ToInt32(ddl_bsf_cliente.SelectedValue), ddl_bsf_categoria.SelectedValue.ToString());
            llenarcombofamiliaencrearsubfam(ddl_bsubfam, Convert.ToInt32(ddl_bsf_cliente.Text), Convert.ToInt32(ddl_bsf_marca.Text), Convert.ToInt32(ddl_bsf_categoria.Text));
            ModalBuscarSubFamily.Show();
        }

        protected void ddl_bsf_marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarcombofamiliaencrearsubfam(ddl_bsubfam, Convert.ToInt32(ddl_bsf_cliente.Text), Convert.ToInt32(ddl_bsf_marca.Text), Convert.ToInt32(ddl_bsf_categoria.Text));
            ModalBuscarSubFamily.Show();
        }
        
        protected void grid_subfamily_RowEditing(object sender, GridViewEditEventArgs e)
        {
            popup_grid_consultasubfam.Show();
            btncancelgvsubfam.Visible = false;
            grid_subfamily.EditIndex = e.NewEditIndex;
            int editado = grid_subfamily.EditIndex;
            string v_familia, v_nomsubfam;
            bool estado;

            v_familia = ((Label)grid_subfamily.Rows[editado].FindControl("lbl_bsubfam_family")).Text;
            v_nomsubfam = ((Label)grid_subfamily.Rows[editado].FindControl("lbl_bsubfam_nom")).Text;
            estado = ((CheckBox)grid_subfamily.Rows[editado].FindControl("cbx_bsubfam_status")).Checked;
            grid_subfamily.DataSource = (DataTable)this.Session["dt_subfamilias"];
            grid_subfamily.DataBind();
            llenacombofam_bsubfam(editado);
            ((DropDownList)grid_subfamily.Rows[editado].FindControl("ddl_bsubfam_family")).Items.FindByText(v_familia).Selected = true;
            ((TextBox)grid_subfamily.Rows[editado].FindControl("txt_bsubfam_nom")).Text = v_nomsubfam;
            ((CheckBox)grid_subfamily.Rows[editado].FindControl("cbx_bsubfam_status")).Checked = estado;
            this.Session["rep_familia"] = ((DropDownList)grid_subfamily.Rows[editado].FindControl("ddl_bsubfam_family")).Text;
            this.Session["rep_nomsubfam"] = v_nomsubfam;
        }

        protected void grid_subfamily_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            btncancelgvsubfam.Visible = true;
            int editado = grid_subfamily.EditIndex;
            string v_idsubfamilia, v_familia, v_nomsubfam;
            bool estado;

            v_idsubfamilia = ((Label)grid_subfamily.Rows[editado].FindControl("lbl_bsubfam_cod")).Text;
            v_familia = ((DropDownList)grid_subfamily.Rows[editado].FindControl("ddl_bsubfam_family")).Text;
            v_nomsubfam = ((TextBox)grid_subfamily.Rows[editado].FindControl("txt_bsubfam_nom")).Text;
            estado = ((CheckBox)grid_subfamily.Rows[editado].FindControl("cbx_bsubfam_status")).Checked;
                        
            try
            {
                repetido = Convert.ToString(this.Session["rep_familia"]);
                repetido1 = Convert.ToString(this.Session["rep_nomsubfam"]);
                if (repetido != v_familia || repetido1 != v_nomsubfam)
                {
                    if (oConn.ejecutarDataTable("UP_WEBXPLORA_AD_BUSCARDUPLICADO_SUBFAMILY", v_familia, v_nomsubfam).Rows.Count == 0)
                    {
                        DataTable update = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_ACTUALIZARSUBFAMILIAS", v_idsubfamilia, v_familia, v_nomsubfam, estado, Convert.ToString(this.Session["sUser"]));

                        DataTable dt = (DataTable)this.Session["dt_subfamilias"];
                        dt.Rows[editado][0] = v_idsubfamilia;
                        dt.Rows[editado][1] = v_familia;
                        dt.Rows[editado][2] = v_nomsubfam;
                        dt.Rows[editado][3] = estado;

                        grid_subfamily.EditIndex = -1;
                        llenagridsubfamilias(dt);
                        //this.grid_bpresentacion.DataSource = dt;
                        //this.grid_bpresentacion.DataBind();
                        btncancelgvsubfam.Visible = true;
                        popup_grid_consultasubfam.Show();
                        
                        Alertas.CssClass = "MensajesCorrecto";
                        LblFaltantes.Text = "La Subfamilia de Producto : " + v_nomsubfam + " se actualizó correctamente";
                        MensajeAlerta();
                    }
                    else
                    {
                        Alertas.CssClass = "MensajesError";
                        LblFaltantes.Text = "No puede actualizar ya que existe una subfamilia con estos datos. Por favor verificar.";
                        MensajeAlerta();
                        return;
                    }
                }
                else
                {
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

        protected void grid_subfamily_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grid_subfamily.EditIndex = -1;
            llenagridsubfamilias((DataTable)this.Session["dt_subfamilias"]);
            btncancelgvsubfam.Visible = true;
            popup_grid_consultasubfam.Show();
        }

        protected void grid_subfamily_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        private void llenagridsubfamilias(DataTable data) 
        {
            grid_subfamily.DataSource = data;
            grid_subfamily.DataBind();
            foreach (GridViewRow dr in grid_subfamily.Rows)
            {
                DataTable nomfam = oConn.ejecutarDataTable("UP_WEBXPLORA_OBTENER_NOMBREFAMILIA", ((Label)dr.FindControl("lbl_bsubfam_family")).Text);
                ((Label)dr.FindControl("lbl_bsubfam_family")).Text = nomfam.Rows[0][5].ToString();
            }
        }

        private void limpiarcontroles() 
        {
            txtcodigosubfamilia.Text = "";
            txtnomsubfamilia.Text = "";
            ddlfamilias.Items.Clear();
            ddl_sf_categoria.Items.Clear();
            ddl_sf_subcategoria.Items.Clear();
            ddl_sf_marca.Items.Clear();
            ddl_sf_submarca.Items.Clear();
            ddl_sf_cliente.Items.Clear();
        }
                
        #endregion

        #region Cargas Masiva
        protected void BtnCargaMasiva_Click(object sender, System.EventArgs e)
        {
            this.Session["TipoCarga"] = "Carga Marcas";
            ModalCMasiva.Show();
        }
        protected void BtnCargaMasivaCate_Click(object sender, System.EventArgs e)
        {
            this.Session["TipoCarga"] = "Carga Categoria";
            ModalCMasivaCategoria.Show();
        }
        protected void BtnCargaMasuSubcategoria_Click(object sender, System.EventArgs e)
        {
            this.Session["TipoCarga"] = "SubCategoria";
            ModalCMSubcategoria.Show();
        }
        protected void btncMasivaProducto_Click(object sender, System.EventArgs e)
        {
            this.Session["TipoCarga"] = "Productos";
            ModalPCargaMProducto.Show();
        }
        protected void BtnCaMasivaPancla_Click(object sender, System.EventArgs e)
        {
            this.Session["TipoCarga"] = "Producto_Ancla";
            ModalPpancla.Show();
        }
        protected void BtnCMSubmarca_Click(object sender, System.EventArgs e)
        {
            this.Session["TipoCarga"] = "SubMarca";
            ModalCaMaPOSubmarca.Show();
        }
        protected void BtnCarMasivaFamilia_Click(object sender, System.EventArgs e)
        {
            this.Session["TipoCarga"] = "Familia";
            ModalPoCMFamilia.Show();
        } 
        #endregion

        #region Category_Company

        private void activarControlesCategoryCompany()
        {

            Panel_Marcas.Enabled = false;
            Panel_Submarcas.Enabled = false;
            Panel_ProductFamily.Enabled = false;
            Panel_CategProduct.Enabled = false;
            Panel_SubCategoria.Enabled = false;
            Panel_Presentación.Enabled = false;
            PanelProducto.Enabled = false;
            TabProducAncla.Enabled = false;
            PanelSubFamilia.Enabled = false;
            panel_Category_Copany.Enabled = true;

        }
        private void desactivarControlesCategoryCompany()
        {

            Panel_Marcas.Enabled = true;
            Panel_Submarcas.Enabled = true;
            Panel_ProductFamily.Enabled = true;
            Panel_CategProduct.Enabled = true;
            Panel_SubCategoria.Enabled = true;
            Panel_Presentación.Enabled = true;
            PanelProducto.Enabled = true;
            TabProducAncla.Enabled = true;
            PanelSubFamilia.Enabled = true;
            panel_Category_Copany.Enabled = true;

        }



        private void cargar_cbxl_cxu_cliente()
        {
            /*DataTable dt = new DataTable();
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_CLIENTES");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    cbxl_cxu_cliente.DataSource = dt;
                    cbxl_cxu_cliente.DataTextField = "Company_Name";
                    cbxl_cxu_cliente.DataValueField = "Company_id";
                    cbxl_cxu_cliente.DataBind();


                    ddlPCategoryCompany_Cliente.DataSource = dt;
                    ddlPCategoryCompany_Cliente.DataTextField = "Company_Name";
                    ddlPCategoryCompany_Cliente.DataValueField = "Company_id";
                    ddlPCategoryCompany_Cliente.DataBind();

                    ddlPCategoryCompany_Cliente.Items.Insert(0, new ListItem("<Seleccione..>", "0"));


                    dt = null;
                }
                else
                {
                    cbxl_cxu_cliente.Items.Clear();
                }
            }*/

            ListItem listItem0 = new ListItem("Compania00", "0");
            ListItem listItem1 = new ListItem("Compania01", "1");
            ListItem listItem2 = new ListItem("Compania02", "2");
            ListItem listItem3 = new ListItem("Compania03", "3");

            cbxl_cxu_cliente.Items.Add(listItem0);
            cbxl_cxu_cliente.Items.Add(listItem1);
            cbxl_cxu_cliente.Items.Add(listItem2);
            cbxl_cxu_cliente.Items.Add(listItem3);

            ddlPCategoryCompany_Cliente.Items.Add(listItem0);
            ddlPCategoryCompany_Cliente.Items.Add(listItem1);
            ddlPCategoryCompany_Cliente.Items.Add(listItem2);
            ddlPCategoryCompany_Cliente.Items.Add(listItem3);
            ddlPCategoryCompany_Cliente.Items.Insert(0, new ListItem("<Seleccione..>", "99"));
        }

        private void LlenaCategory_CompanyCatego()
        {
            /*DataSet ds1 = new DataSet();
            ds1 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 59);
            //se llena Categorias producto en BuscarSubCategorias
            ddlCategoryCompany_Categoria.DataSource = ds1;
            ddlCategoryCompany_Categoria.DataTextField = "Product_Category";
            ddlCategoryCompany_Categoria.DataValueField = "id_ProductCategory";
            ddlCategoryCompany_Categoria.DataBind();

            ddlCategoryCompany_Categoria.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            //llenando el panel de consultar
            ddlPCategoryCompany_Categoria.DataSource = ds1;
            ddlPCategoryCompany_Categoria.DataTextField = "Product_Category";
            ddlPCategoryCompany_Categoria.DataValueField = "id_ProductCategory";
            ddlPCategoryCompany_Categoria.DataBind();

            ddlPCategoryCompany_Categoria.Items.Insert(0, new ListItem("<Seleccione..>", "0"));

            ds1 = null;*/

            ListItem listItem0 = new ListItem("Categoria00", "0");
            ListItem listItem1 = new ListItem("Categoria01", "1");
            ListItem listItem2 = new ListItem("Categoria02", "2");
            ListItem listItem3 = new ListItem("Categoria03", "3");

            ddlCategoryCompany_Categoria.Items.Add(listItem0);
            ddlCategoryCompany_Categoria.Items.Add(listItem1);
            ddlCategoryCompany_Categoria.Items.Add(listItem2);
            ddlCategoryCompany_Categoria.Items.Add(listItem3);
            ddlCategoryCompany_Categoria.Items.Insert(0, new ListItem("<Seleccione..>", "99"));

            ddlPCategoryCompany_Categoria.Items.Add(listItem0);
            ddlPCategoryCompany_Categoria.Items.Add(listItem1);
            ddlPCategoryCompany_Categoria.Items.Add(listItem2);
            ddlPCategoryCompany_Categoria.Items.Add(listItem3);
            ddlPCategoryCompany_Categoria.Items.Insert(0, new ListItem("<Seleccione..>", "99"));
        }

 
 
        protected void btnCategoryCompany_Crear_Click(object sender, EventArgs e)
        {
            ddlCategoryCompany_Categoria.Enabled = true;
            cbxl_cxu_cliente.Enabled = true;
            btnCategoryCompany_Guardar.Visible = true;
            activarControlesCategoryCompany();
            btnCategoryCompany_Guardar.Visible = true;
            btnCategoryCompany_Crear.Visible = false;
            btnCategoryCompany_Consultar.Visible = false;
        }



        AD_Category_Company oAD_Category_Company = new AD_Category_Company();
        protected void btnCategoryCompany_Guardar_Click(object sender, EventArgs e)
        {
           

                        if (ddlCategoryCompany_Categoria.SelectedIndex != 0 && cbxl_cxu_cliente.SelectedItem != null)
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
                string categoria = ddlCategoryCompany_Categoria.SelectedValue;

                foreach (ListItem item in cbxl_cxu_cliente.Items)
                {
                    string codcliente = item.Value;
                    bool nodoxcanal_estado = item.Selected;
                    try
                    {
                     
                        if (nodoxcanal_estado==true)
                        {
                            DataTable dt =oAD_Category_Company.ConsultarAD_Category_Company(categoria, codcliente);

                            if (dt.Rows.Count == 0)
                            {
                                oAD_Category_Company.Category_Company(categoria, codcliente, true, username, DateTime.Now, username, DateTime.Now);
                            }
                            else
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El Cliente " + dt.Rows[0][4].ToString() + " ya esta asignado a la Categoria " + dt.Rows[0][2].ToString();
                                MensajeAlerta();

                                return;

                            }
                        }
                       
                        
                    }
                    catch (Exception ex) { }
                }

                Alertas.CssClass = "MensajesCorrecto";
                LblFaltantes.Text = "Asignación Clientes a Categoria registrada correctamente.";
                MensajeAlerta();
                ddlCategoryCompany_Categoria.SelectedIndex = 0;
                foreach (ListItem item in cbxl_cxu_cliente.Items)
                {
                    item.Selected = false;
                }
            }
            else
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Seleccione la Categoria y los Clientes a asociar";
                MensajeAlerta();
            }
        }

        public void CargagrillaCategoryCompany()
        {
            DataTable dt = oAD_Category_Company.ConsultarAD_Category_Company(ddlPCategoryCompany_Categoria.SelectedValue, ddlPCategoryCompany_Cliente.SelectedValue);
            gv_CategoryCompany.DataSource = dt;
            gv_CategoryCompany.DataBind();
            paneel.Visible = false;
            activarControlesCategoryCompany();

        }

        protected void btnCategoryCompany_Buscar_Click(object sender, EventArgs e)
        {
            if (ddlPCategoryCompany_Cliente.SelectedValue == "0")
            {
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Seleccione un Cliente";
                MensajeAlerta();
            }
            try
            {


                CargagrillaCategoryCompany();

               
            }
            catch (Exception ex) 
            { 
            }
        }

        protected void gv_CategoryCompany_ItemDataBound(object sender, GridItemEventArgs e)
        {
            string tableName = e.Item.OwnerTableView.Name.ToString();
            if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditManager editMan = editedItem.EditManager;

                DataSet ds = null;
                Conexion Ocoon = new Conexion();

                DataTable dt = new DataTable();
                dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_CLIENTES");

                GridDropDownListColumnEditor gddlCliente = editMan.GetColumnEditor("gddlCliente") as GridDropDownListColumnEditor;

                gddlCliente.DataSource = dt;
                gddlCliente.DataValueField = "Company_id";
                gddlCliente.DataTextField = "Company_Name";
                gddlCliente.DataBind();
                gddlCliente.Visible = true;

                DataSet ds1 = new DataSet();
                ds1 = oConn.ejecutarDataSet("UP_WEB_LLENACOMBOS", 59);

                GridDropDownListColumnEditor gddlCategoria = editMan.GetColumnEditor("gddlCategoria") as GridDropDownListColumnEditor;
                //gddlCategoria.Visible = true;
                gddlCategoria.DataSource = ds1;
                gddlCategoria.DataValueField = "id_ProductCategory";
                gddlCategoria.DataTextField = "Product_Category";
                gddlCategoria.DataBind();


            }
        }

        protected void gv_CategoryCompany_CancelCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                CargagrillaCategoryCompany();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_CategoryCompany_EditCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                CargagrillaCategoryCompany();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_CategoryCompany_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                gv_CategoryCompany.CurrentPageIndex = e.NewPageIndex;
                CargagrillaCategoryCompany();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_CategoryCompany_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            try
            {
                CargagrillaCategoryCompany();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        protected void btnGrillaCategoryCompany_Click(object sender, EventArgs e)
        {
            div.Visible = false;
            paneel.Visible = true;
        }


        protected void gv_CategoryCompany_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
               
                Conexion Ocoon = new Conexion();

                GridItem item = gv_CategoryCompany.Items[e.Item.ItemIndex];

                Label lbl_Id = (Label)item.FindControl("lbl_Id");
                int id = Convert.ToInt32(lbl_Id.Text.Trim());



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

                string Cliente = ArrayEditorValue[0].ToString();
                string Categoria = ArrayEditorValue[1].ToString();



                //  DataTable dt = Ocoon.ejecutarDataTable("ValidarProducto", Producto, Convert.ToInt32(this.Session["companyid"]));

                DataTable dt = oAD_Category_Company.ConsultarAD_Category_Company(Categoria, Cliente);

                if (dt.Rows.Count == 0)
                {
                    Ocoon.ejecutarDataReader("UP_WEBXPLORA_AD_UPDATE_AD_Category_Company", id, Cliente, Categoria, cb_validar.Checked, Session["sUser"].ToString(), DateTime.Now);

                    Alertas.CssClass = "MensajesCorrecto";
                    LblFaltantes.Text = "Los datos fuero actualizados correctamente";
                    MensajeAlerta();
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "El Cliente " + dt.Rows[0][4].ToString() + " ya esta asignado a la Categoria " + dt.Rows[0][2].ToString();
                    MensajeAlerta();

                    CargagrillaCategoryCompany();

                    return;

                }


                CargagrillaCategoryCompany();
            }
            catch (Exception ex)
            {
               
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

        }

        protected void btnCategoryCompany_Cancelar_Click(object sender, EventArgs e)
        {
            btnCategoryCompany_Guardar.Visible = false;
            btnCategoryCompany_Crear.Visible = true;
            btnCategoryCompany_Consultar.Visible = true;
        }

        #endregion

        protected void btnBuscarLote_Click(object sender, EventArgs e)
        { 
        
        }

    }
}