using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lucky.Data;
using Telerik.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Stock
{
    public partial class Reporte_v2_Stock_DiasGiroPorMarcaYFamilia : System.Web.UI.UserControl
    {
        Conexion oCoon = new Conexion();

        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();


        int icompany;
        int Report;
        string canal;
        private string year = String.Empty;
        private string month = String.Empty;
        private int iperiodo = 0;
        private string sidmarca = String.Empty;
        private string id_ProductCategory = String.Empty;
        private string cod_oficina = String.Empty;
        private string id_family = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        # region Llenar Datos

        public void Años(DataTable dty)
        {

            if (dty.Rows.Count > 0)
            {
                cmb_año.DataSource = dty;
                cmb_año.DataValueField = "Years_Number";
                cmb_año.DataTextField = "Years_Number";
                cmb_año.DataBind();

                cmb_año.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));
            }
            else
            {
                cmb_año.DataBind();
            }
        }

        public void Llena_Meses(DataTable dtm)
        {

            if (dtm.Rows.Count > 0)
            {
                cmb_mes.DataSource = dtm;
                cmb_mes.DataValueField = "codmes";
                cmb_mes.DataTextField = "namemes";
                cmb_mes.DataBind();

                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));
            }
            else
            {
                cmb_mes.DataBind();
            }
        }

        private void Llenar_Periodos()
        {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            canal = this.Session["Canal"].ToString().Trim();

            cmb_periodo.Items.Clear();
            cmb_periodo.Enabled = true;
            DataTable dtp = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", canal, icompany, Report, cmb_mes.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                cmb_periodo.DataSource = dtp;
                cmb_periodo.DataValueField = "id_periodo";
                cmb_periodo.DataTextField = "Periodo";
                cmb_periodo.DataBind();

                cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));

            }
            else
            {
                cmb_periodo.DataBind();
            }
        }

        public void Cobertura(DataTable dtc)
        {

            if (dtc.Rows.Count > 0)
            {

                cmb_oficina.DataSource = dtc;
                cmb_oficina.DataValueField = "cod_city";
                cmb_oficina.DataTextField = "name_city";
                cmb_oficina.DataBind();

                cmb_oficina.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));

            }
            else
            {
                cmb_oficina.DataBind();
            }

        }

        public void Categorias(DataTable dtcatego)
        {
            if (dtcatego.Rows.Count > 0)
            {

                cmb_categoria.DataSource = dtcatego;
                cmb_categoria.DataValueField = "cod_catego";
                cmb_categoria.DataTextField = "Name_Catego";
                cmb_categoria.DataBind();

                cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));

            }
            else
            {
                cmb_categoria.DataBind();
            }

        }

        private void Subcategorias()
        {
            DataTable dtscat = null;
            dtscat = Get_DataClientes.Get_Obtenerinfocombos(0, "", cmb_categoria.SelectedValue, 3);
            if (dtscat.Rows.Count > 0)
            {
                cmb_subCategoria.DataSource = dtscat;
                cmb_subCategoria.DataValueField = "cod_subcatego";
                cmb_subCategoria.DataTextField = "Name_Subcatego";
                cmb_subCategoria.DataBind();


                cmb_subCategoria.Items.Insert(0, new ListItem("--Todas--", "0"));

            }
            else
            {
                cmb_subCategoria.DataBind();
                cmb_subCategoria.Enabled = false;
            }

        }

        private void Marcas()
        {
            DataTable dtm = null;
            icompany = Convert.ToInt32(this.Session["companyid"]);
            if (cmb_subCategoria.SelectedValue == "")
            {

                dtm = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIEN_V2_LLENARMARCAS_PRECIOS", cmb_categoria.SelectedValue, 0, icompany);



            }
            else
            {

                dtm = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIEN_V2_LLENARMARCAS_PRECIOS", cmb_categoria.SelectedValue, cmb_subCategoria.SelectedValue, icompany);

            }
            if (dtm.Rows.Count > 0)
            {

                cmb_marca.DataSource = dtm;
                cmb_marca.DataValueField = "id_Brand";
                cmb_marca.DataTextField = "Name_Brand";
                cmb_marca.DataBind();

                cmb_marca.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));

            }
            else
            {

                dtm = null;
                cmb_marca.Enabled = false;
                //cmb_subCategoria.Items.Clear();
            }
        }

        private void Submarcas()
        {
            DataTable dtsm = null;
            dtsm = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTNERSUBMARCAS", Convert.ToInt32(cmb_marca.SelectedValue));
            if (dtsm.Rows.Count > 0)
            {

                cmb_subMarca.DataSource = dtsm;
                cmb_subMarca.DataValueField = "id_SubBrand";
                cmb_subMarca.DataTextField = "Name_SubBrand";
                cmb_subMarca.DataBind();

                cmb_subMarca.Items.Insert(0, new ListItem("--Todas--", "0"));

            }
            else
            {
                dtsm = null;
                cmb_subMarca.Enabled = false;
                cmb_subMarca.Items.Clear();

            }

        }
        private void Puntos_Venta()
        {

            DataTable dtpdv = null;
            dtpdv = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PDV_PRECIOS", Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), cmb_oficina.SelectedValue);
            if (dtpdv.Rows.Count > 0)
            {
                cmb_punto_de_venta.DataSource = dtpdv;
                cmb_punto_de_venta.DataValueField = "pdv_code";
                cmb_punto_de_venta.DataTextField = "pdv_Name";
                cmb_punto_de_venta.DataBind();

                cmb_punto_de_venta.Items.Insert(0, new ListItem("--Todos--", "0"));

            }
            else
            {
                dtpdv = null;
            }
        }
        private void Familias()
        {

            DataTable dtf = null;
            dtf = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_COMBO_FAMILIA_BY_IDCATEGORIA", cmb_categoria.SelectedValue.Trim());

            if (dtf.Rows.Count > 0)
            {
                cmb_familia.Enabled = true;
                cmb_familia.DataSource = dtf;
                cmb_familia.DataValueField = "id_ProductFamily";
                cmb_familia.DataTextField = "name_Family";
                cmb_familia.DataBind();

                cmb_familia.Items.Insert(0, new RadComboBoxItem("--Todas--", "0"));
            }
            else
            {
                cmb_familia.DataBind();
                cmb_familia.Enabled = false;
            }
        }
        private void Productos()
        {
            icompany = Convert.ToInt32(this.Session["companyid"]);
            DataTable dtpdt = null;


            if (cmb_subCategoria.SelectedValue == "" && cmb_marca.SelectedValue == "" && cmb_subMarca.SelectedValue == "")
            {

                dtpdt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PRODUCTOS_PRECIOS", icompany, cmb_categoria.SelectedValue, "0", 0, 0);

            }
            else
            {
                if (cmb_subMarca.SelectedValue == "")
                {



                    dtpdt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PRODUCTOS_PRECIOS", icompany, cmb_categoria.SelectedValue, cmb_subCategoria.SelectedValue, cmb_marca.SelectedValue, 0);
                }
                else
                {

                    dtpdt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PRODUCTOS_PRECIOS", icompany, cmb_categoria.SelectedValue, cmb_subCategoria.SelectedValue, cmb_marca.SelectedValue, cmb_subMarca.SelectedValue);
                }
            }
            if (dtpdt.Rows.Count > 0)
            {
                cmb_skuProducto.DataSource = dtpdt;
                cmb_skuProducto.DataValueField = "cod_Product";
                cmb_skuProducto.DataTextField = "Name_Product";
                cmb_skuProducto.DataBind();

                cmb_skuProducto.Items.Insert(0, new ListItem("--Todas--", "0"));

            }
            else
            {
                dtpdt = null;
                cmb_subCategoria.Items.Clear();
                cmb_skuProducto.Items.Clear();
            }

        }


        # endregion
        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmb_periodo.Items.Clear();
            Llenar_Periodos();
            ModalPopupExtender_mostrarFiltros.Show();
        }
        protected void cmb_oficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Puntos_Venta();

        }
        protected void cmb_categoria_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            cmb_marca.Items.Clear();
            cmb_subMarca.Items.Clear();
            cmb_subCategoria.Items.Clear();
            cmb_subCategoria.Enabled = true;
            Subcategorias();
            cmb_marca.Enabled = true;
            Marcas();
            Familias();
            Productos();
            ModalPopupExtender_mostrarFiltros.Show();
        }

        protected void cmb_marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_subMarca.Enabled = true;
            //Submarcas();
            Productos();

            //cargarInforme_DiasGiroOficina();
            //cargarInforme_DiasGiroCategoria();
            //cargarInforme_DiasGiroMarcaFamilia();
            //cargarInforme_DiasGiroPDV();
            ModalPopupExtender_mostrarFiltros.Show();
        }

        protected void cmb_subMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            Productos();
            ModalPopupExtender_mostrarFiltros.Show();
        }

        protected void btngnerar_Click(object sender, EventArgs e)
        {
            sidmarca = cmb_marca.SelectedValue;
            if (sidmarca == "")
                sidmarca = "0";

            year = cmb_año.SelectedValue;
            month = cmb_mes.SelectedValue;

            iperiodo = Convert.ToInt32(cmb_periodo.SelectedValue);

            id_ProductCategory = cmb_categoria.SelectedValue;
            if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0" && id_ProductCategory == "0")
                id_ProductCategory = "12";


            cod_oficina = cmb_oficina.SelectedValue.Trim();
            if (cod_oficina == "")
                cod_oficina = "0";

            id_family = cmb_familia.SelectedValue.Trim();
            if (id_family == "")
                id_family = "0";
            //--end cargar parametros iniciales

            icompany = Convert.ToInt32(this.Session["companyid"]);
            int iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            try
            {


                ReportstockdiagiroMarcaFamilia.Visible = true;
                ReportstockdiagiroMarcaFamilia.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                ReportstockdiagiroMarcaFamilia.ServerReport.ReportPath = "/Reporte_Precios_V1/ReporteStock_TotalDiasGiroPorMarcaYFamilia";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                ReportstockdiagiroMarcaFamilia.ServerReport.ReportServerUrl = new Uri(strConnection);
                ReportstockdiagiroMarcaFamilia.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();

                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Company_id", Convert.ToString(icompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Strategy", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Planning_CodChannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("cod_Oficina", cod_oficina));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductCategory", id_ProductCategory));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_Brand", sidmarca));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("id_ProductFamily", id_family));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", year));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", month));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iperiodo.ToString()));

                ReportstockdiagiroMarcaFamilia.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";

            }
        }
    }
}