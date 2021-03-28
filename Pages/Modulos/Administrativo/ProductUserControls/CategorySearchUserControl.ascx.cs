using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.Pages.Modulos.Administrativo.ProductUserControls
{
    public partial class CategorySearchUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Limpia los Filtros para próximas consultas
        /// </summary>
        private void cleanFilterInCategorySearch()
        {
            TxtBCodTypeProduct.Text = "";
            TxtBNomTypeProduct.Text = "";
            cmb_Cliente.Text = "0";
        }

        /// <summary>
        /// Limpiar los Input, para la Gestión de Categorías
        /// </summary>
        private void SavelimpiarControlesCategoria()
        {
            /*
            TxtCodProductType.Text = "";
            TxtNomProductType.Text = "";
            TxtgroupCategory.Text = "";
            cmb_categorias_cliente.Text = "0";
            */
            TxtBCodTypeProduct.Text = "";
            TxtBNomTypeProduct.Text = "";
        }

        /// <summary>
        /// Evento Click del Panel de Busqueda de Categorias, 
        /// para filtrar la información de las Categorías, aplicando los filtros, para la Gestión de Categorías.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnBTypeProduct_Click(object sender, EventArgs e)
        {
            /*
            desactivarControlesCategoria();

            LblFaltantes.Text = "";

            string productId = trimText(TxtBCodTypeProduct.Text);
            string productName = trimText(TxtBNomTypeProduct.Text);
            string companyId = trimText(cmb_Cliente.SelectedValue.ToString());

            bool continuar = validateFilterInCategorySearch(productId, productName, companyId);

            if (continuar)
            {

                buscarActivarbotnesCategoria();

                cleanFilterInCategorySearch();

                saveSessionInCategorySearch(productId, productName, companyId);

                DataTable categoriesDt = getProductCategoryInCategorySearch(productId, productName, companyId);

                continuar = populateGridViewForCategoryResultSearch(categoriesDt);

                if (continuar)
                {

                    saveSessionResultCategorySearch(categoriesDt);
                    saveSessionToExportExcelByCategorySearchResult();

                }
            }
            */

        }
        /// <summary>
        /// Opcion de Cancelar, en la Gestión de Categorias
        /// </summary>
        private void cancelarCat()
        {
            saveActivarbotonesCategoria();
            desactivarControlesCategoria();
            SavelimpiarControlesCategoria();
            //BtnCargaMasivaCate.Visible = true;
        }

        /// <summary>
        /// Desactivar Controles, en la Gestión de Categorías
        /// </summary>
        private void desactivarControlesCategoria()
        {

        }

       

        /// <summary>
        /// Desactivar la Opción de Save, para la Opción de Gestión de Categorías
        /// </summary>
        private void saveActivarbotonesCategoria()
        {
            //BtnCrearProductType.Visible = true;
            //BtnSaveProductType.Visible = false;
            //BtnConsultaProductType.Visible = true;
            //BtnCancelProductType.Visible = true;
        }

        /// <summary>
        /// Validar que los filtros aplicados devuelvan una consulta.
        /// </summary>
        /// <returns></returns>
        private bool validateFilterInCategorySearch(string productId, string productName, string companyId)
        {

            // Validación que los Filtros o Criterios de Busqueda se encuentren especificados
            if (productId == "" && productName == "" && cmb_Cliente.Text == "0")
            {
                /*
                this.Session["mensajealert"] = "Código y/o nombre de Categoría de producto y/o Cliente";
                Alertas.CssClass = "MensajesError";
                LblFaltantes.Text = "Ingrese por lo menos un parámetro de consulta";
                cancelarCat();
                MensajeAlerta();
                // Mostrar el AspControl ModalPopUpExtender 'IbtnProductType' relacionado al AspControl Panel 'BuscarProductCateg'
                IbtnProductType.Show();
                return false;
                */
            }
            return true;
        }

    }
}