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

namespace SIGE.Pages.Modulos.Administrativo.ProductUserControls
{
    public partial class CategoryInsertUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                llenarComboCliente();
            }
        }


        private Conexion oConn = new Lucky.Data.Conexion();
        private String messages = "";


        private void cleanControlsPinputDf()
        {
            TxtCodProductType.Text = "";
            TxtNomProductType.Text = "";
            TxtgroupCategory.Text = "";
            cmb_categorias_cliente.Text = "0";
        }

        /// <summary>
        /// Funcion para validar que no se registren categorias Duplicadas
        /// </summary>
        public bool validarPrerequisitosCategoriasInsertarActualizar()
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
                AlertasInsertPanel.CssClass = "MensajesError";
                MensajeAlerta();
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool validarPrerequisitosCategoriasInsertarActualizar_Lab01(
            string nombreProducto,
            string nombreTipoProducto) {

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
                AlertasInsertPanel.CssClass = "MensajesError";
                MensajeAlerta();
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Validate preconditions for Products Insert, using a boolean value as an answer.
        /// </summary>
        /// <param name="productName">string value of product name.</param>
        /// <param name="categoryId">string value of category id.</param>
        /// <returns>boolean value for determine if the record is duplicate or not.</returns>
        public bool validatePreconditionsForProductInsert(string productName, string categoryId)
        {
            string message = "";

            productName = productName.Trim();

            if (categoryId == "0" || productName == "") {
                
                message = "Debe ingresar los campos con: ";

                if (categoryId == "0") message += ("Cliente" + " . ");
                if (productName == "") message += ("Categoría de producto" + " . ");
                
                AlertasInsertPanel.CssClass = "MensajesError";
                MensajeAlerta();
                
                return false;

            } else {
                
                return true;
            }
        }


        /// <summary>
        /// Validar Categorías Duplicadas
        /// </summary>
        /// <returns></returns>
        private DataTable validarCategoriasDuplicadas()
        {
            DAplicacion odconsulProductCategory = new DAplicacion();
            DataTable dtconsulta =
                        odconsulProductCategory.ConsultaDuplicados(
                        ConfigurationManager.AppSettings["ProductCategory"],
                        TxtNomProductType.Text,
                        cmb_categorias_cliente.SelectedValue.ToString(),
                        null);
            return dtconsulta;
        }

        /// <summary>
        /// Registrar Categorias en la Base de Datos
        /// </summary>
        /// <returns></returns>
        public EProduct_Type registrarCategorias()
        {
            Product_Type oProductTypeBusinessLogic = new Product_Type();

            EProduct_Type oeProductType =
                            oProductTypeBusinessLogic.RegistrarProductcategory(
                            TxtCodProductType.Text,
                            TxtNomProductType.Text,
                            TxtgroupCategory.Text,
                            true,
                            cmb_categorias_cliente.SelectedValue.ToString().Trim(),
                            Convert.ToString(this.Session["sUser"]),
                            DateTime.Now,
                            Convert.ToString(this.Session["sUser"]),
                            DateTime.Now);
            return oeProductType;
        }

        public EProduct_Type registrarCategorias_Test(
            string codigoProducto, 
            string nombreProducto,
            string codigoGrupoCategoriaProducto, 
            string codigoCategoriaProducto,
            string codigoUsuario,
            bool estadoProducto,
            DateTime fechaRegistro)
        {
            Product_Type oProductTypeBusinessLogic = new Product_Type();

            EProduct_Type oeProductType =
                            oProductTypeBusinessLogic.RegistrarProductcategory(
                            TxtCodProductType.Text,
                            TxtNomProductType.Text,
                            TxtgroupCategory.Text,
                            estadoProducto,
                            cmb_categorias_cliente.SelectedValue.ToString().Trim(),
                            Convert.ToString(this.Session["sUser"]),
                            DateTime.Now,
                            null,
                            DateTime.MinValue);
            return oeProductType;
        }

        /// <summary>
        /// Funcionalidad para Guardar Cambios de Categoría
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSaveProductType_Click(object sender, EventArgs e)
        {
            try {

                // Lectura de Componentes en el formulario
                string codigoProducto               = TxtCodProductType.Text;
                string nombreProducto               = TxtNomProductType.Text;
                string codigoGrupoCategoriaProducto = TxtgroupCategory.Text;
                bool estadoProducto                 = true;
                string codigoCategoriaProducto      = cmb_categorias_cliente.SelectedValue.ToString().Trim();
                string nombreUsuario                = Convert.ToString(this.Session["sUser"]);
                DateTime fechaRegistro              = DateTime.Now;

                if (validarPrerequisitosCategoriasInsertarActualizar())
                {
                    DataTable dtconsulta = validarCategoriasDuplicadas();

                    if (dtconsulta == null)
                    {

                        EProduct_Type oeProductType = registrarCategorias_Test(
                            codigoProducto,
                            nombreProducto, 
                            codigoGrupoCategoriaProducto,
                            codigoCategoriaProducto,
                            nombreUsuario,
                            estadoProducto,
                            fechaRegistro);
                        //EProduct_Type oeProductType = registrarCategorias();
                        //EProduct_Type oeProductTypeTMP = registrarCategoriasMobile(oeProductType);

                        messageCreateSucessCategory();
                        cleanControlsPinputDf();

                    }
                    else
                    {
                        messageCreateFailedCategory();
                    }
                }

            }

            catch (Exception ex)
            {
                errorMessage(ex);
            }
        }

        /// <summary>
        /// Función para limpiar los InputInsert
        /// </summary>
        private void limpiarInputInsert() {
            TxtCodProductType.Text = "";
            TxtNomProductType.Text = "";
            TxtgroupCategory.Text = "";
            cmb_categorias_cliente.Text = "0";
        }

        /// <summary>
        /// Mensaje cuando la categoria fue creada con éxito
        /// </summary>
        private void messageCreateSucessCategory()
        {

            this.Session["sProductType"] = TxtNomProductType.Text;
            AlertasInsertPanel.CssClass = "MensajesCorrecto";
            LblFaltantes.Text = "La Categoría de producto " + TxtNomProductType.Text + ", fue creada con éxito";
            //cancelarCat();
            MensajeAlerta();
        }

        /// <summary>
        /// Mensaje de Alertas
        /// </summary>
        private void MensajeAlerta()
        {
            ModalPopupAlertasInsert.Show();
            //BtndisparaalertasInsert.Focus();
            //ScriptManager.RegisterStartupScript(
            //    this, this.GetType(), "myscript", "alert('Debe ingresar todos los parametros con (*)');", true);
        }

        /// <summary>
        /// Mensaje cuando la categoria Falló por algún motivo, al momento de crearla
        /// </summary>
        private void messageCreateFailedCategory()
        {
            AlertasInsertPanel.CssClass = "MensajesError";
            LblFaltantes.Text = "La Categoría  de producto " + TxtNomProductType.Text + ", ya existe";
            //cancelarCat();
            MensajeAlerta();
        }


        /// <summary>
        /// Management Error Messages
        /// </summary>
        /// <param name="ex"></param>
        private void errorMessage(Exception ex)
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

        /// <summary>
        /// Llenar Compañias, en el AspControl DropDownList 'cmb_categorias_cliente' y 'cmb_Cliente', para la Gestión de Categorias
        /// </summary>
        private void llenarComboCliente()
        {
            
            messages = "";
            DataTable dt = null;
            try
            {
                dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENER_CLIENTES_EXTERNOS");
            }
            catch (Exception ex)
            {
                messages = "Ocurrio un Error: " + ex.ToString();
            }

            // Verificar que no existan Errores
            if (messages.Equals(""))
            {
                if (dt.Rows.Count > 0)
                {
                    //se llena cliente 
                    cmb_categorias_cliente.DataSource = dt;
                    cmb_categorias_cliente.DataTextField = "Company_Name";
                    cmb_categorias_cliente.DataValueField = "Company_id";
                    cmb_categorias_cliente.DataBind();
                    cmb_categorias_cliente.Items.Insert(0, new ListItem("<Seleccione..>", "0"));
                    foreach (ListItem li in cmb_categorias_cliente.Items)
                    {
                        //cmb_Cliente.Items.Add(li);
                    }
                }
                else
                {
                    // Mostrar PopUp Mensaje Usuario
                    messages = "Error: No Existen Compañias Disponibles, ¡por favor Verificar...!";
                }
            }
            else
            {
                // Mostrar PopUp Mensaje Usuario
            }


            #region Data Dummy
            /*
            ListItem listItem0 = new ListItem("Compania00", "0");
            ListItem listItem1 = new ListItem("Compania01", "1");
            ListItem listItem2 = new ListItem("Compania02", "2");
            ListItem listItem3 = new ListItem("Compania03", "3");

            cmb_categorias_cliente.Items.Add(listItem0);
            cmb_categorias_cliente.Items.Add(listItem1);
            cmb_categorias_cliente.Items.Add(listItem2);
            cmb_categorias_cliente.Items.Add(listItem3);
            cmb_categorias_cliente.Items.Insert(0, new ListItem("<Seleccione..>", "99"));
            */
            #endregion
        }
 
    }
}