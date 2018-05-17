using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Data;
using Lucky.CFG.Util;
using Telerik.Web.UI;
using System.Data;
//using LuckyMer.Contracts.DataContract;
using Lucky.CFG.JavaMovil;
using System.Configuration;

namespace SIGE.Pages.Modulos.Cliente.Reportes.Informe_de_Presencia
{
    public partial class Reporte_v2_PresenciaConfiguracion : System.Web.UI.Page
    {
        private int iidcompany;
        private string sidcanal;
        private int iservicio;
        private int Report;
        
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();
        Conexion oCoon = new Conexion();
        Periodo P = new Periodo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarMenu();
                cargarAños();
                cargarMes();
                ConfigurarControles();
            }
        }

        #region LLenar Combos
        private void cargarAños()
        {
            DataTable dty = null;
            dty = Get_Administrativo.Get_ObtenerYears();
            if (dty.Rows.Count > 0)
            {
                string sidperdil = this.Session["Perfilid"].ToString();
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    Form_Presencia_Objetivos1.llenaAñosUC(dty);
                    Form_Presencia_PrecSugeridos1.llenaAñosUC(dty);
                    UC_Form_TextEditor1.llenaAñosUC(dty);
                }
            }
            else
            {
                dty = null;
            }
        }
        private void cargarMes()
        {
            DataTable dtm = Get_Administrativo.Get_ObtenerMeses();

            if (dtm.Rows.Count > 0)
            {
                string sidperdil = this.Session["Perfilid"].ToString();
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    Form_Presencia_Objetivos1.llenaMesUC(dtm);
                    Form_Presencia_PrecSugeridos1.llenaMesUC(dtm);
                    UC_Form_TextEditor1.llenaMesUC(dtm);
                }
            }
        }
        #endregion

        #region Cargar Menu
        public void cargarMenu()
        {
            MenuService1.MenuServiceClient client = new MenuService1.MenuServiceClient("BasicHttpBinding_IMenuService");


            RadMenu rad_menu = RadPanelBar_menu.FindChildByValue<RadPanelItem>("menu").FindControl("rad_menu") as RadMenu;

            string dataJson;
            string request;

            request = "{'i':'" + Session["id_menu"].ToString() + "'}";
            dataJson = client.ObtenerMenuDetalle(request);
            //MenuServiceResponse menuServiceResponse = HelperJson.Deserialize<MenuServiceResponse>(dataJson);

            MenuLoadUtil oLoadMenu = new MenuLoadUtil();
            //rad_menu = oLoadMenu.LoadRadMenu(rad_menu, menuServiceResponse);

        }
        #endregion 

        private void ConfigurarControles()
        {
            string sidperdil = this.Session["Perfilid"].ToString();        
            sidcanal = this.Session["Canal"].ToString();

            if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
            {
                RadPanelBar_menu.FindChildByValue<RadPanelItem>("configuracion").Visible = true;
                if (sidcanal == "1023")
                {
                    RadPageView_Parametrizacion.Visible = true;
                    cargarCategorias();
                    cargarListaGraficos();
                    cargarProductos();
                }
            }
            else
            {
                RadPanelBar_menu.FindChildByValue<RadPanelItem>("configuracion").Visible = false;
            }
        }
        private void cargarCategorias()
        {
            string sidperdil = this.Session["Perfilid"].ToString();

            if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
            {
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();
                Report = Convert.ToInt32(this.Session["Reporte"]);
                iservicio = Convert.ToInt32(this.Session["Service"]);

                DataTable dtcatego = Get_DataClientes.Get_Obtenerinfocombos_F(iidcompany, sidcanal, Report, "", 2);

                rlb_category.DataSource = dtcatego;
                rlb_category.DataValueField = "cod_catego";
                rlb_category.DataTextField = "Name_Catego";
                rlb_category.DataBind();
                rlb_category.Items.Insert(0, new RadListBoxItem("--Todos--", "0"));
            }
        }
        private static DataTable sdtProduct;
        protected void rlb_category_SelectedIndexChanged(object o, EventArgs e)
        {
            if (rlb_category.SelectedValue != "0")
            {
                DataTable dtFiltrado = new DataTable();

                dtFiltrado.Columns.Add("cod_Product");
                dtFiltrado.Columns.Add("Product_Name");

                string expression = "id_ProductCategory=" + rlb_category.SelectedValue;
                string sortOrder = "Product_Name ASC";

                DataRow[] foundRows;

                foundRows = sdtProduct.Select(expression, sortOrder);

                for (int i = 0; i < foundRows.Length; i++)
                {
                    DataRow dr = dtFiltrado.NewRow();

                    dr["cod_Product"] = foundRows[i]["cod_Product"];
                    dr["Product_Name"] = foundRows[i]["Product_Name"];

                    dtFiltrado.Rows.Add(dr);
                }

                rlb_productos.DataSource = dtFiltrado;
                rlb_productos.DataValueField = "cod_Product";
                rlb_productos.DataTextField = "Product_Name";
                rlb_productos.DataBind();
            }
            else
            {
                rlb_productos.DataSource = sdtProduct;
                rlb_productos.DataValueField = "cod_Product";
                rlb_productos.DataTextField = "Product_Name";
                rlb_productos.DataBind();
            }

        }
        private void cargarProductos()
        {
            string sidperdil = this.Session["Perfilid"].ToString();
            if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
            {
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();
                Report = Convert.ToInt32(this.Session["Reporte"]);
                iservicio = Convert.ToInt32(this.Session["Service"]);

                //cambiar paso del código de planning
                //string año = rcmb_añoPr.SelectedValue.ToString(); Se quito debido a k la gestión de gráficos ya no sera por año y mes 
                //string mes = rcmb_mesPr.SelectedValue.ToString();
                string cod_grafico = rcmb_grafico.SelectedValue.ToString();
                lbl_mensaje.ForeColor = System.Drawing.Color.Red;
                DataSet dtm = oCoon.ejecutarDataSet("UP_WEBXPLORA_PRESENCIA_OBTENER_PRODUCTOS_X_PLANNING", sidcanal, iidcompany, Report, iservicio, cod_grafico);

                sdtProduct = dtm.Tables[0];



                if (!cod_grafico.Equals("0"))
                {

                    rlb_productos.DataSource = dtm.Tables[0];
                    rlb_productos.DataValueField = "cod_Product";
                    rlb_productos.DataTextField = "Product_Name";
                    rlb_productos.DataBind();

                    rlb_category.SelectedIndex = 0;

                    rlb_lstproductos.DataSource = dtm.Tables[1];
                    rlb_lstproductos.DataValueField = "cod_Product";
                    rlb_lstproductos.DataTextField = "Product_Name";
                    rlb_lstproductos.DataBind();

                    lbl_grafico_text.Text = "Gráfico " + rcmb_grafico.SelectedItem.Text;
                    lbl_grafico_text.ForeColor = System.Drawing.Color.Green;
                    lbl_mensaje.Text = "";

                }
                else
                {
                    lbl_mensaje.Text = "Sr. usuario, debe seleccionar año, mes y gráfico.";
                }
            }
        }

        private void cargarListaGraficos()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?> " +
            "<Items>" +
                "<Item Text=\"--Seleccione--\" Value=\"0\" />" +
                "<Item Text=\"Executive Summary - 2\" Value=\"graf01\" />" +
                "<Item Text=\"Executive Summary - 2.1\" Value=\"graf02\" />" +
                "<Item Text=\"Executive Summary - 2.2\" Value=\"graf03\" />" +
                "<Item Text=\"Executive Summary - 2.3\" Value=\"graf04\" />" +
                "<Item Text=\"Executive Summary - 2.4\" Value=\"graf05\" />" +
                "<Item Text=\"Index Price - 1\" Value=\"graf11\" />" +
                "<Item Text=\"Index Price - 2\" Value=\"graf06\" />" +
                "<Item Text=\"Index Price - 2.1\" Value=\"graf07\" />" +
                "<Item Text=\"Index Price - 2.2\" Value=\"graf08\" />" +
                "<Item Text=\"Index Price - 3\" Value=\"graf09\" />" +
                "<Item Text=\"Index Price - 4\" Value=\"graf10\" />" +

            "</Items>";
            rcmb_grafico.LoadXml(xml);
        }
        protected void btn_guardar_params_Click(object sender, EventArgs e)
        {
            sidcanal = this.Session["Canal"].ToString();
            //string año = rcmb_añoPr.SelectedValue.ToString();
            // string mes = rcmb_mesPr.SelectedValue.ToString();
            string cod_grafico = rcmb_grafico.SelectedValue.ToString();
            string grafico = rcmb_grafico.Text.ToString();
            string cod_productos = "";

            lbl_mensaje.ForeColor = System.Drawing.Color.Red;

            if (!cod_grafico.Equals("0"))
            {
                if (rlb_lstproductos.Items.Count != 0)
                {
                    foreach (RadListBoxItem item in rlb_lstproductos.Items)
                        cod_productos = cod_productos + item.Value + ",";
                    //cod_productos = cod_productos + "'" + item.Value + "',";

                    cod_productos = cod_productos.Substring(0, cod_productos.Length - 1);

                    try
                    {
                        DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PRESENCIA_REGISTRAR_PANEL_PROD", sidcanal, cod_grafico, grafico, cod_productos);


                        rlb_productos.Items.Clear();
                        rlb_lstproductos.Items.Clear();
                        cargarProductos();

                        lbl_mensaje.Text = "Registro efectuado correctamente.";
                        lbl_mensaje.ForeColor = System.Drawing.Color.Green;
                    }
                    catch (Exception ex)
                    {
                        lbl_mensaje.Text = "Lo sentimos, no se pudo completar el registro.";
                    }
                }
                else
                {
                    lbl_mensaje.Text = "Sr. usuario, no ha seleccionado productos.";
                }
            }
            else
            {
                lbl_mensaje.Text = "Sr. usuario, debe seleccionar año, mes y gráfico.";
            }
        }

        protected void rcmb_grafico_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cargarProductos();
        }
      
    }
}