using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.CFG.Util;
using Lucky.Data;
using Microsoft.Reporting.WebForms;
using Telerik.Web.UI;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Web;
using Lucky.Business.Common.Application;
using System.IO;
using System.Threading;
using System.Text;



namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_v2_Stock_M_SF : System.Web.UI.Page
    {
        private Conexion oCoon = new Conexion(); 
        private Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        private Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();

        private string sUser;
        private string sPassw;
        private string sNameUser;
        private int icompany;
        private int iservicio;
        private string canal;
        private int Report;
        //private ReportViewer reporte1;
        //private ReportViewer reportedetalle;
        
        #region Funciones Especiales

        private string sidaño;
        private string sidmes;
        private string sidperiodo;
        private string siddia;
        private string sidciudad;
        private string sidcategoria;
        private string sidsub_categoria;
        private string sidmarca;
        private string sidsub_marca;
        private string sidsku;
        private string sidpuntoventa;
        private string sidfamilias;
        private string sidsubfamilias;



        protected void GetPeridForAnalist()
        {//se obtiene el estado de un Reporte en un Año, mes y periodo especifico.Y otros datos adicionales del periodo obtenido

            Report = Convert.ToInt32(this.Session["Reporte"]);
            DataTable dt = null;
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO", canal, Report, sidaño, sidmes, sidperiodo);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    div_Validar.Visible = true;
                    sidaño = dt.Rows[0]["id_Year"].ToString();
                    sidmes = dt.Rows[0]["id_Month"].ToString();
                    sidperiodo = dt.Rows[0]["periodo"].ToString();
                    bool valid_analist = Convert.ToBoolean(dt.Rows[0]["ReportsPlanning_ValidacionAnalista"]);

                    lbl_año_value.Text = sidaño;
                    lbl_mes_value.Text = sidmes;
                    lbl_periodo_value.Text = sidperiodo;
                    
                    if (valid_analist)
                        chkb_validar.Checked = valid_analist;
                    else
                        chkb_invalidar.Checked = true;

                    lbl_validacion.Text = sidaño + "-" + dt.Rows[0]["Month_name"].ToString() + " " + sidperiodo;
                }
            }
        }

        protected void GetPeriodForClient()
        { //se obtiene el ultimo años mes y perido validado por el analista, para que el cliente pueda ver dicho reporte
            DataTable dt = null;

            Report = Convert.ToInt32(this.Session["Reporte"]);
            //Add 02/02/2012 pSalas se adiciona los parametros id_cliente y id_channel para obtener el máximo periodo validado para el Cliente.
            icompany = Convert.ToInt32(this.Session["companyid"]);
            canal = this.Session["Canal"].ToString().Trim();
            //iservicio = Convert.ToInt32(this.Session["Service"]);

            //dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_MAX_PERIODO_VALIDADO", Report);
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_MAX_PERIODO_VALIDADO_SF",icompany,canal, Report);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    sidaño = dt.Rows[0]["id_Year"].ToString();//con estos datos se debe hacer la carga
                    sidmes = dt.Rows[0]["id_Month"].ToString();
                    sidperiodo = dt.Rows[0]["periodo"].ToString();
                }
            }
        }

        private void _AsignarVariables()
        {
            sidaño = cmb_año.SelectedValue;
            sidmes = cmb_mes.SelectedValue;
            sidperiodo = cmb_periodo.SelectedValue;
            //siddia = cmb_dia.SelectedValue;
            //sidcategoria = "9463";

            string sidperdil = this.Session["Perfilid"].ToString();
            if (cmb_año.SelectedValue == "0" && cmb_mes.SelectedValue == "0" && cmb_periodo.SelectedValue == "0")
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    //aca debe ir la carga inical para el analista
                    icompany = Convert.ToInt32(this.Session["companyid"]);
                    iservicio = Convert.ToInt32(this.Session["Service"]);
                    canal = this.Session["Canal"].ToString().Trim();
                    Report = Convert.ToInt32(this.Session["Reporte"]);
                    int cadena = Convert.ToInt32(cmb_nodocomercial.SelectedItem.Value);
                    string categoria = cmb_categoria.SelectedItem.Value;
                    //(int reportid, int icadena, string categoria, string canal, int cliente, int servicio)
                    //Periodo p = new Periodo(Report, cadena, categoria, canal, icompany, iservicio);
                    Periodo p = new Periodo(Report, sidciudad, sidcategoria, sidsub_categoria, sidmarca, sidsub_marca, sidsku, sidpuntoventa, canal, icompany, iservicio);
                    p.Set_PeriodoConDataValidada_New(); //23/12/2011 se quito el comentario en el método.
                    //p.Set_PeriodoConDataValidada();
                    //datos quemados temporalmente
                    sidaño = p.Año;
                    sidmes = p.Mes;
                    sidperiodo = p.PeriodoId;

                    GetPeridForAnalist();
                }
                else if (sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteGeneral"] || sidperdil == ConfigurationManager.AppSettings["PerfilClienteDetallado1"])
                {
                    GetPeriodForClient();
                }
            }
            else
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    GetPeridForAnalist();
                }
            }
        }

        #endregion


        private void iniciarcombos()
        {
            cmb_año.DataBind();
            cmb_año.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_mes.DataBind();
            cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_periodo.DataBind();
            cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            //cmb_dia.DataBind();
            //cmb_dia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            cmb_oficina.DataBind();
            cmb_oficina.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_corporacion.DataBind();
            cmb_corporacion.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_nodocomercial.DataBind();
            cmb_nodocomercial.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            //cmb_pventa.DataBind();
            //cmb_pventa.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            cmb_supervisor.DataBind();
            cmb_supervisor.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_fuerzav.DataBind();
            cmb_fuerzav.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            cmb_categoria.DataBind();
            cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            cmb_marca.DataBind();
            cmb_marca.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            //cmb_familia.DataBind();
            //cmb_familia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            ddl_Familia.DataBind();
            //ddl_Familia.Items.Insert(0, new ListItem("--Todos--","0"));

            ddl_PuntoDeVenta.DataBind();

            //cmb_subfamilia.DataBind();
            //cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0")); 
        }

        //private void UpdateProgressContext2()
        //{
        //    const int total = 100;
        //    RadProgressContext progress = RadProgressContext.Current;

        //    for (int i = 0; i < total; i++)
        //    {                
        //        progress.PrimaryTotal = 1;
        //        progress.PrimaryValue = 1;
        //        progress.PrimaryPercent = 100;
        //        progress.SecondaryTotal = total;
        //        progress.SecondaryValue = i;
        //        progress.SecondaryPercent = i;
        //        progress.CurrentOperationText = "Stock - Moderno";

        //        if (!Response.IsClientConnected)
        //        {
        //            //Cancel button was clicked or the browser was closed, so stop processing
        //            break;
        //        }
        //        progress.Speed = i;
        //        //Stall the current thread for 0.1 seconds
        //        System.Threading.Thread.Sleep(50);
        //    }
        //}

        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Llenar_Periodos();
        }

        protected void btn_ocultar_Click(object sender, EventArgs e)
        {
            if (Div_filtros.Visible == false)
            {
                Div_filtros.Visible = true;
                //btn_ocultar.Text = "Filtros";
                
                btngnerar.Visible = true;
                btnVentaXFamilia.Visible = true;
                btnQuiebreXTienda.Visible = true;
                btnVentaXTienda.Visible = true;
                btnVentaTotalXMes.Visible = true;
                //btnVentaMensualXFamilia.Visible = true;
                btnVentaAcumXProducto.Visible = true;
                btnEvoProdXSemana.Visible = true;
               

            }
            else if (Div_filtros.Visible == true)
            {
               
                Div_filtros.Visible = false;
                //btn_ocultar.Text = "Ocultar";
                btngnerar.Visible = false;
                btnVentaXFamilia.Visible = false;
                btnQuiebreXTienda.Visible = false;
                btnVentaXTienda.Visible = false;
                btnVentaTotalXMes.Visible = false;
                //btnVentaMensualXFamilia.Visible = false;
                btnVentaAcumXProducto.Visible = false;
                btnEvoProdXSemana.Visible = false; 

            }
        }

     

        protected void cargarParametrosdeXml()
        {
            string path = Server.MapPath("~/reporteSF_Moderno.xml");

            if (System.IO.File.Exists(path))
            {
                //Reportes_parametros oReportes_parametros = new Reportes_parametros();
                //Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

                //oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
                //oReportes_parametros.Id_user = this.Session["sUser"].ToString();
                //oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
                //oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
                //oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();
                
                //RadGrid_parametros.DataSource = oReportes_parametrosToXml.Get_Parametros(oReportes_parametros, path);
                //RadGrid_parametros.DataBind();

                Reportes_parametros_SF oReportes_parametros_SF = new Reportes_parametros_SF();
                Reportes_parametros_SF_ToXml oReportes_parametros_SF_ToXml = new Reportes_parametros_SF_ToXml();

                //parametros para guardar el contenido de los combo con checkBox. Add 24/02/2012
                string id_puntosDeVenta = "", id_familias = "", id_subfamilias = "", id_productos = "", id_dias = "";
                 

                oReportes_parametros_SF.Id_servicio = Convert.ToInt32(this.Session["Service"]);
                oReportes_parametros_SF.Id_canal = this.Session["Canal"].ToString().Trim();
                oReportes_parametros_SF.Id_compania = Convert.ToInt32(this.Session["companyid"]);
                oReportes_parametros_SF.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
                oReportes_parametros_SF.Id_user = this.Session["sUser"].ToString();
                oReportes_parametros_SF.Id_oficina = Convert.ToInt32(cmb_oficina.SelectedValue);
                oReportes_parametros_SF.Id_corporacion = cmb_corporacion.SelectedValue;
                oReportes_parametros_SF.Id_cadena = cmb_nodocomercial.SelectedValue;
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        id_puntosDeVenta = id_puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + ",";
                        //oReportes_parametros_SF.ListPuntoDeVenta.Add(ddl_PuntoDeVenta.Items[i].Value);
                    }
                }
                if(id_puntosDeVenta != "")
                    id_puntosDeVenta = id_puntosDeVenta.Substring(0, id_puntosDeVenta.Length - 1);

                if (id_puntosDeVenta == null) 
                    id_puntosDeVenta = "0";
                else if (id_puntosDeVenta == "") 
                    id_puntosDeVenta = "0";
                oReportes_parametros_SF.Id_puntoDeVenta = id_puntosDeVenta;

                oReportes_parametros_SF.Id_fuerzaVenta = cmb_fuerzav.SelectedValue;
                oReportes_parametros_SF.Id_supervisor = cmb_supervisor.SelectedValue;
                oReportes_parametros_SF.Id_categoria = cmb_categoria.SelectedValue;
                oReportes_parametros_SF.Id_marca = cmb_marca.SelectedValue;
                for (int i = 0; i < ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        id_familias = id_familias + ddl_Familia.Items[i].Value + ",";
                        //oReportes_parametros_SF.ListFamilia.Add(ddl_Familia.Items[i].Value);
                    }
                }
                if (id_familias != "")
                id_familias = id_familias.Substring(0, id_familias.Length - 1);

                if (id_familias == null)
                    id_familias = "0";
                else if (id_familias == "")
                    id_familias = "0";
                oReportes_parametros_SF.Id_familias = id_familias;


                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        id_subfamilias = id_subfamilias + ddl_Subfamilia.Items[i].Value + ",";
                        //oReportes_parametros_SF.ListSubfamilia.Add(ddl_Subfamilia.Items[i].Value);
                    }
                }
                if(id_subfamilias!="" )
                id_subfamilias = id_subfamilias.Substring(0, id_subfamilias.Length - 1);
               

                if (id_subfamilias == null)
                    id_subfamilias = "0";
                else if (id_subfamilias == "")
                    id_subfamilias = "0";
                oReportes_parametros_SF.Id_subfamilias = id_subfamilias;


                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        id_productos = id_productos + ddl_Producto.Items[i].Value + ",";
                        //oReportes_parametros_SF.ListProductos.Add(ddl_Producto.Items[i].Value);
                    }
                }
                if (id_productos != "")
                id_productos = id_productos.Substring(0, id_productos.Length - 1);

                if (id_productos == null)
                    id_productos = "0";
                else if(id_productos=="")
                    id_productos="0";
                oReportes_parametros_SF.Id_productos = id_productos;


                oReportes_parametros_SF.Id_year = cmb_año.SelectedValue;
                oReportes_parametros_SF.Id_month = cmb_mes.SelectedValue;
                oReportes_parametros_SF.Id_periodo = cmb_periodo.SelectedValue;
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        id_dias = id_dias + ddl_Dia.Items[i].Value + ",";
                        //oReportes_parametros_SF.ListDias.Add(ddl_Dia.Items[i].Value);
                    }
                }
                if (id_dias != "")
                id_dias = id_dias.Substring(0, id_dias.Length - 1);

                if (id_dias == null)
                    id_dias = "0";
                else if (id_dias == "")
                    id_dias = "0";
                oReportes_parametros_SF.Id_dias = id_dias;

                oReportes_parametros_SF.Descripcion = txt_descripcion_parametros.Text.Trim();


                RadGrid_parametros.DataSource = oReportes_parametros_SF_ToXml.Get_Parametros(oReportes_parametros_SF, path);
                RadGrid_parametros.DataBind();
            }
        }

        protected void btn_imb_tab_Click(object sender, ImageClickEventArgs e)
        {
            TabContainer_filtros.ActiveTabIndex = 0;
        }

        protected void RadGrid_parametros_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("BUSCAR"))
            {

                Label lbl_id_oficina = (Label)e.Item.FindControl("lbl_id_oficina");
                Label lbl_id_corporacion = (Label)e.Item.FindControl("lbl_id_corporacion");
                Label lbl_id_cadena = (Label)e.Item.FindControl("lbl_id_cadena");
                Label lbl_id_pdv = (Label)e.Item.FindControl("lbl_id_pdv");
                Label lbl_id_fuerzaVenta = (Label)e.Item.FindControl("lbl_id_fuerzaVenta");
                Label lbl_id_supervisor = (Label)e.Item.FindControl("");
                Label lbl_id_categoria = (Label)e.Item.FindControl("lbl_id_categoria");
                Label lbl_id_marca = (Label)e.Item.FindControl("lbl_id_marca");
                Label lbl_id_familias =(Label)e.Item.FindControl("lbl_id_familias");
                Label lbl_id_subfamilias = (Label)e.Item.FindControl("lbl_id_subfamilias");
                Label lbl_skuProducto = (Label)e.Item.FindControl("lbl_skuProducto");
                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_periodo = (Label)e.Item.FindControl("lbl_id_periodo");
                Label lbl_id_dias = (Label)e.Item.FindControl("lbl_id_dias");
                //Label lbl_id_subMarca = (Label)e.Item.FindControl("lbl_id_subMarca");
                //Label lbl_id_subCategoria = (Label)e.Item.FindControl("lbl_id_subCategoria");
                #region
                sidaño = lbl_id_año.Text.Trim();
                sidmes = lbl_id_mes.Text.Trim();
                siddia = lbl_id_dias.Text.Trim();
                sidperiodo = lbl_id_periodo.Text.Trim();
                sidciudad = lbl_id_oficina.Text.Trim();
                sidcategoria = lbl_id_categoria.Text.Trim();
                //sidsub_categoria = lbl_id_subCategoria.Text.Trim();
                sidmarca = lbl_id_marca.Text.Trim();
                //sidsub_marca = lbl_id_subMarca.Text.Trim();
                
                sidsku = lbl_skuProducto.Text.Trim();
                sidpuntoventa = lbl_id_pdv.Text.Trim();
                sidfamilias = lbl_id_familias.Text.Trim();
                #endregion
                //idOficina
                cmb_oficina.SelectedValue = lbl_id_oficina.Text;
                //idCorporacion
                cmb_corporacion.SelectedValue = lbl_id_corporacion.Text;
                //idCadena
                cmb_nodocomercial.SelectedValue = lbl_id_cadena.Text;
                //puntoDeVenta
                if (lbl_id_pdv.Text != "" && lbl_id_pdv.Text != "0") {
                    string cad_pdv = lbl_id_pdv.Text;
                    string[] arr_pdv = cad_pdv.Split(',');
                    for (int i = 0; i < arr_pdv.Length; i++) {
                        ddl_PuntoDeVenta.SelectedValue = ddl_PuntoDeVenta.Items.FindByValue(arr_pdv[i]).Value;
                    }
                }
                //fuerzaDeVenta
                cmb_fuerzav.SelectedValue = lbl_id_fuerzaVenta.Text;
                
                //familia
                if (lbl_id_familias.Text != "" && lbl_id_familias.Text != "0") {
                    string cad_familias = lbl_id_familias.Text;
                    string[] arr_familias = cad_familias.Split(',');
                    for (int i = 0; i < arr_familias.Length; i++) {
                        ddl_Familia.SelectedValue = ddl_Familia.Items.FindByValue(arr_familias[i]).Value;
                    }
                }

                //subfamilia
                if (lbl_id_subfamilias.Text != "" && lbl_id_subfamilias.Text != "0")
                {
                    string cad_subfamilias = lbl_id_subfamilias.Text;
                    string[] arr_subfamilias = cad_subfamilias.Split(',');
                    for (int i = 0; i < arr_subfamilias.Length; i++)
                    {
                        ddl_Subfamilia.SelectedValue = ddl_Subfamilia.Items.FindByValue(arr_subfamilias[i]).Value;
                    }
                }
                //productos
                if (lbl_skuProducto.Text != "" && lbl_skuProducto.Text != "0")
                {
                    string cad_sku = lbl_skuProducto.Text;
                    string[] arr_sku = cad_sku.Split(',');
                    for (int i = 0; i < arr_sku.Length; i++)
                    {
                        ddl_Producto.SelectedValue = ddl_Producto.Items.FindByValue(arr_sku[i]).Value;
                        //cmbCheckBox_skuProducto.SelectedValue = cmbCheckBox_skuProducto.Items.FindByValue(arr_sku[i]).Value;
                    }
                }
                //id_years
                cmb_año.SelectedValue = lbl_id_año.Text;
                //id_months
                cmb_mes.SelectedValue = lbl_id_mes.Text;
                //id_periodo
                cmb_periodo.SelectedValue = lbl_id_periodo.Text;
                //id_dias
                if (lbl_id_dias.Text != "" && lbl_id_dias.Text != "0")
                {
                    string cad_idDias = lbl_id_dias.Text;
                    string[] arr_idDias = cad_idDias.Split(',');
                    for (int i = 0; i < arr_idDias.Length; i++)
                    {
                        ddl_Dia.SelectedValue = ddl_Dia.Items.FindByValue(arr_idDias[i]).Value;
                        //cmbCheckBox_skuProducto.SelectedValue = cmbCheckBox_skuProducto.Items.FindByValue(arr_sku[i]).Value;
                    }
                }

                UpdatePanel_filtros.Update();
                //llenarreporteInicial();
                //llenarreporteCompa();
                //llenarreporteCompaciuda();
            }
            if (e.CommandName == "ELIMINAR")
            {
                Label lbl_id = (Label)e.Item.FindControl("lbl_id");

                string path = Server.MapPath("~/parametros.xml");
                Reportes_parametros oReportes_parametros = new Reportes_parametros();
                oReportes_parametros.Id = Convert.ToInt32(lbl_id.Text);

                Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

                oReportes_parametrosToXml.DeleteElement(oReportes_parametros, path);
                cargarParametrosdeXml();
            }
            if (e.CommandName == "EDITAR")
            {
                Label lbl_id = (Label)e.Item.FindControl("lbl_id");
                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_periodo = (Label)e.Item.FindControl("lbl_id_periodo");
                Label lbl_id_oficina = (Label)e.Item.FindControl("lbl_id_oficina");
                Label lbl_id_pdv = (Label)e.Item.FindControl("lbl_id_pdv");
                Label lbl_id_categoria = (Label)e.Item.FindControl("lbl_id_categoria");
                Label lbl_id_subCategoria = (Label)e.Item.FindControl("lbl_id_subCategoria");
                Label lbl_id_marca = (Label)e.Item.FindControl("lbl_id_marca");
                Label lbl_id_subMarca = (Label)e.Item.FindControl("lbl_id_subMarca");
                Label lbl_skuProducto = (Label)e.Item.FindControl("lbl_skuProducto");

                Session["idxml"] = lbl_id.Text.Trim();
                cmb_año.SelectedIndex = cmb_año.Items.FindItemByValue(lbl_id_año.Text).Index;
                cmb_mes.SelectedIndex = cmb_mes.Items.FindItemByValue(lbl_id_mes.Text).Index;
                Llenar_Periodos();
                cmb_periodo.SelectedIndex = cmb_periodo.FindItemByValue(lbl_id_periodo.Text).Index;
                //cmb_ciudad.SelectedIndex = cmb_ciudad.Items.FindItemByValue(lbl_id_oficina.Text).Index;
                //cmb_punto_de_venta.SelectedIndex = cmb_punto_de_venta.Items.FindItemByValue(lbl_id_pdv.Text).Index;
                //cmb_categoria.SelectedIndex = cmb_categoria.Items.FindItemByValue(lbl_id_categoria.Text).Index;
                //Subcategorias();
                //cmb_subCategoria.SelectedIndex=cmb_subCategoria.Items.FindItemByValue(lbl_id_subCategoria.Text).Index;
                //cmb_marca.SelectedIndex = cmb_marca.Items.FindItemByValue(lbl_id_marca.Text).Index;
                //cmb_subMarca.SelectedIndex = cmb_subMarca.Items.FindItemByValue(lbl_id_subMarca.Text).Index;
                //cmb_skuProducto.SelectedIndex = cmb_skuProducto.Items.FindItemByValue(lbl_skuProducto.Text).Index;

                TabContainer_filtros.ActiveTabIndex = 0;
                lbl_updateconsulta.Visible = true;
                btn_img_actualizar.Visible = true;
                lbl_saveconsulta.Visible = false;
                btn_img_add.Visible = false;
            }
        }

        #region Logica de la Validación del Reporte

        protected void chkb_validar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkb_validar.Checked)
            {
                chkb_invalidar.Checked = false;
            }
            lbl_msj_validar.Text = "¿ Esta seguro que desea continuar?";
            btn_aceptar2.Visible = false;
            btn_aceptar.Visible = true;
            btn_cancelar.Visible = true;

            ModalPopupExtender_ValidationAnalyst.Show();
        }
        protected void chkb_invalidar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkb_invalidar.Checked)
            {
                chkb_validar.Checked = false;
            }
            lbl_msj_validar.Text = "¿ Esta seguro que desea continuar?";
            btn_aceptar2.Visible = false;
            btn_aceptar.Visible = true;
            btn_cancelar.Visible = true;

            ModalPopupExtender_ValidationAnalyst.Show();
        }
        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            btn_aceptar2.Visible = true;
            btn_aceptar.Visible = false;
            btn_cancelar.Visible = false;
            try
            {
                Report = Convert.ToInt32(this.Session["Reporte"]);
                oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_REPORT_PLANNING_ACTUALIZAR_VALIDACION", Report, lbl_año_value.Text.Trim(), lbl_mes_value.Text.Trim(), lbl_periodo_value.Text.Trim(), chkb_validar.Checked, Session["sUser"].ToString(), DateTime.Now);

                ModalPopupExtender_ValidationAnalyst.Show();
                lbl_msj_validar.Text = "El cambio se realizo con exito.";
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            if (chkb_validar.Checked == true)
            {
                chkb_validar.Checked = false;
                chkb_invalidar.Checked = true;
            }
            else
            {
                chkb_validar.Checked = true;
                chkb_invalidar.Checked = false;
            }
        }
#endregion 

        protected void buttonGuardar_Click(object sender, EventArgs e)
        {
            #region
            //string path = Server.MapPath("~/parametros.xml");
            //Reportes_parametros oReportes_parametros = new Reportes_parametros();

            //oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
            //oReportes_parametros.Id_user = this.Session["sUser"].ToString();
            //oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
            //oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
            //oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

            //oReportes_parametros.Id_año = cmb_año.SelectedValue;
            //oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            //oReportes_parametros.Id_periodo = Convert.ToInt32(cmb_periodo.SelectedValue);
            //oReportes_parametros.Descripcion = txt_descripcion_parametros.Text.Trim();

            //Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

            //if (!System.IO.File.Exists(path))
            //    oReportes_parametrosToXml.createXml(oReportes_parametros, path);
            //else
            //    oReportes_parametrosToXml.addToXml(oReportes_parametros, path);
            
            //cargarParametrosdeXml();
            //txt_descripcion_parametros.Text = "";
            //TabContainer_filtros.ActiveTabIndex = 1;

            #endregion

            string path = Server.MapPath("~/reporteSF_Moderno.xml");
            //Reportes_parametros oReportes_parametros = new Reportes_parametros();
            Reportes_parametros_SF oReportes_parametros_SF = new Reportes_parametros_SF();
            string id_puntosDeVenta = "", id_familias = "", id_subfamilias = "", id_productos = "", id_dias = "";


            oReportes_parametros_SF.Id_servicio = Convert.ToInt32(this.Session["Service"]);
            oReportes_parametros_SF.Id_canal = this.Session["Canal"].ToString().Trim();
            oReportes_parametros_SF.Id_compania = Convert.ToInt32(this.Session["companyid"]);
            oReportes_parametros_SF.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
            oReportes_parametros_SF.Id_user = this.Session["sUser"].ToString();
            oReportes_parametros_SF.Id_oficina = Convert.ToInt32(cmb_oficina.SelectedValue);
            oReportes_parametros_SF.Id_corporacion = cmb_corporacion.SelectedValue;
            oReportes_parametros_SF.Id_cadena = cmb_nodocomercial.SelectedValue;
            //Puntos de Venta
            for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
            {
                if (ddl_PuntoDeVenta.Items[i].Selected) {
                    id_puntosDeVenta = id_puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + ",";
                    //oReportes_parametros_SF.ListPuntoDeVenta.Add(ddl_PuntoDeVenta.Items[i].Value);
                }
            }
            if (id_puntosDeVenta != "")
            {
                id_puntosDeVenta = id_puntosDeVenta.Substring(0, id_puntosDeVenta.Length - 1);
            }
            if (id_puntosDeVenta == "")
                id_puntosDeVenta = "0";
            else if (id_puntosDeVenta==null)
                id_puntosDeVenta="0";
            oReportes_parametros_SF.Id_puntoDeVenta = id_puntosDeVenta;


            oReportes_parametros_SF.Id_fuerzaVenta = cmb_fuerzav.SelectedValue;
            oReportes_parametros_SF.Id_supervisor = cmb_supervisor.SelectedValue;
            oReportes_parametros_SF.Id_categoria = cmb_categoria.SelectedValue;
            oReportes_parametros_SF.Id_marca = cmb_marca.SelectedValue;
            
            //Familias
            for (int i = 0; i < ddl_Familia.Items.Count - 1; i++) {
                if (ddl_Familia.Items[i].Selected) {
                    //oReportes_parametros_SF.ListFamilia.Add(ddl_Familia.Items[i].Value);
                    id_familias = id_familias + ddl_Familia.Items[i].Value + ",";
                }
            }
            if (id_familias != "") { 
            id_familias = id_familias.Substring(0, id_familias.Length - 1);
            }

            if (id_familias == null)
            oReportes_parametros_SF.Id_familias = id_familias;
            else if (id_familias == "")
                oReportes_parametros_SF.Id_familias = id_familias;
            oReportes_parametros_SF.Id_familias = id_familias;
            
            //Subfamilias
            for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++) {
                if (ddl_Subfamilia.Items[i].Selected) {
                    //oReportes_parametros_SF.ListSubfamilia.Add(ddl_Subfamilia.Items[i].Value);
                    id_subfamilias = id_subfamilias + ddl_Subfamilia.Items[i].Value + ",";
                }
            }
            if (id_subfamilias != "") {
                id_subfamilias = id_subfamilias.Substring(0, id_subfamilias.Length - 1);
            }
            
            if (id_subfamilias == null)
                id_subfamilias = "0";
            else if (id_subfamilias == "")
                id_subfamilias = "0";
            oReportes_parametros_SF.Id_subfamilias = id_subfamilias;
            
            //Productos
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        //oReportes_parametros_SF.ListProductos.Add(ddl_Producto.Items[i].Value);
                        id_productos = id_productos + ddl_Producto.Items[i].Value + ",";
                    }
                }
                if (id_productos != "")
                {
                    id_productos = id_productos.Substring(0, id_productos.Length - 1);
                }
            if (id_productos == "")
                id_productos = "0";
            else if (id_productos == null)
                id_productos = "0";
            oReportes_parametros_SF.Id_productos = id_productos;


            oReportes_parametros_SF.Id_year = cmb_año.SelectedValue;
            oReportes_parametros_SF.Id_month = cmb_mes.SelectedValue;
           
            //Dias
            oReportes_parametros_SF.Id_periodo = cmb_periodo.SelectedValue;
            for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++) {
                if (ddl_Dia.Items[i].Selected) {
                    id_dias = id_dias + ddl_Dia.Items[i].Value + ",";
                    //oReportes_parametros_SF.ListDias.Add(ddl_Dia.Items[i].Value);
                }
            }
            if (id_dias != "")
            {
                id_dias = id_dias.Substring(0, id_dias.Length - 1);
            }
            if (id_dias == null)
                id_dias = "0";
            else if (id_dias == null)
                id_dias = "0";
            oReportes_parametros_SF.Id_dias = id_dias;

            oReportes_parametros_SF.Descripcion = txt_descripcion_parametros.Text.Trim();

            //Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();
            Reportes_parametros_SF_ToXml oReportes_parametros_SF_ToXml = new Reportes_parametros_SF_ToXml();

            if (!System.IO.File.Exists(path))
                oReportes_parametros_SF_ToXml.createXml(oReportes_parametros_SF, path);
            else
                oReportes_parametros_SF_ToXml.addToXml(oReportes_parametros_SF, path);

            cargarParametrosdeXml();
            txt_descripcion_parametros.Text = "";
            TabContainer_filtros.ActiveTabIndex = 1;



        }
        protected void btn_actualizar_Click(object sender, EventArgs e)
        { 
        }
        


        protected void cmb_oficina_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenacorporacion();
        }
        protected void cmb_corporacion_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenacadena();
        }
        protected void cmb_nodocomercial_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //llenapuntoventa();
            llenapuntoventa_2();
        }
        protected void cmb_categoria_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenamarca();
        }
        protected void cmb_marca_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //llenafamilia();
            llenafamilia_2();
        }
        protected void cmb_familia_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            llenasubfamilia();
        }
        protected void ddl_Producto_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }


        protected void btnVentaXFamilia_Click(object sender, EventArgs e)
        {
            TabContainer_Reporte_Stock_SF_M.ActiveTab.TabIndex = 0;
            _AsignarVariables();
            llenarReporteVentasXFamilia();
        }
        protected void btnQuiebreXTienda_Click(object sender, EventArgs e)
        {
            TabContainer_Reporte_Stock_SF_M.ActiveTab.TabIndex = 0;
            _AsignarVariables();
            llenarReporteQuiebreXTienda();
        }
        protected void btnVentaXTienda_Click(object sender, EventArgs e)
        {
            TabContainer_Reporte_Stock_SF_M.ActiveTab.TabIndex = 0;
            _AsignarVariables();
            llenarVentaXTienda();
        }
        protected void btnVentaTotalXMes_Click(object sender, EventArgs e)
        {
            TabContainer_Reporte_Stock_SF_M.ActiveTab.TabIndex = 0;
            _AsignarVariables();
            llenarVentasTotalesXMes();
        }
        protected void btnVentaMensualXFamilia_Click(object sender, EventArgs e)
        {
            TabContainer_Reporte_Stock_SF_M.ActiveTab.TabIndex = 0;
            _AsignarVariables();
            //llenarVentasMensuales_X_Familia();
        }
        protected void btnVentaAcumXProducto_Click(object sender, EventArgs e)
        {
            TabContainer_Reporte_Stock_SF_M.ActiveTab.TabIndex = 0;
            _AsignarVariables();
            llenarVentasAcumuladas_X_Prod();
        }
        protected void btnEvoProdXSemana_Click(object sender, EventArgs e)
        {
            TabContainer_Reporte_Stock_SF_M.ActiveTab.TabIndex = 0;
            _AsignarVariables();
            llenarEvolucion_X_Productos_X_Semana();
        }

        //private void llenarreporteInicial()
        //{
        //    icompany = Convert.ToInt32(this.Session["companyid"]);
        //    iservicio = Convert.ToInt32(this.Session["Service"]);
        //    canal = this.Session["Canal"].ToString().Trim();
        //    Report = Convert.ToInt32(this.Session["Reporte"]);

        //    try
        //    {
        //        //Stock Moderno
        //        rpt_stock_m.Visible = true;
        //        rpt_stock_m.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;

        //        rpt_stock_m.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_Stock_Ingreso_SF_M";

        //        String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

        //        rpt_stock_m.ServerReport.ReportServerUrl = new Uri(strConnection);
        //        rpt_stock_m.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
        //        List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductcategory", cmb_categoria.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idbrand", cmb_marca.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductfamily", cmb_familia.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", cmb_subfamilia.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idperson", cmb_supervisor.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idffvv", cmb_fuerzav.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));

        //        rpt_stock_m.ServerReport.SetParameters(parametros);

        //        #region masreportes
        //        ////ventas por nivel
        //        //rpt_nivel.Visible = true;
        //        //rpt_nivel.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;

        //        //rpt_nivel.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_VENTAS_NIVEL_SF_AAVV";

        //        //String strConnection1 = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

        //        //rpt_nivel.ServerReport.ReportServerUrl = new Uri(strConnection1);
        //        //rpt_nivel.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
        //        //List<Microsoft.Reporting.WebForms.ReportParameter> parametros1 = new List<Microsoft.Reporting.WebForms.ReportParameter>();

        //        //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
        //        //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
        //        //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("category", sidcategoria));
        //        //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
        //        //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
        //        //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", siddia));
        //        //parametros1.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", sidperiodo));

        //        //rpt_nivel.ServerReport.SetParameters(parametros1);

        //        ////ventas por zona
        //        //rpt_zona.Visible = true;
        //        //rpt_zona.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;

        //        //rpt_zona.ServerReport.ReportPath = "/Reporte_Precios_V1/RPT_VENTAS_ZONAS_SF_AAVV";

        //        //String strConnection2 = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

        //        //rpt_zona.ServerReport.ReportServerUrl = new Uri(strConnection2);
        //        //rpt_zona.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
        //        //List<Microsoft.Reporting.WebForms.ReportParameter> parametros2 = new List<Microsoft.Reporting.WebForms.ReportParameter>();

        //        //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("channel", canal));
        //        //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("strategy", iservicio.ToString()));
        //        //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("category", sidcategoria));
        //        //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
        //        //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
        //        //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", siddia));
        //        //parametros2.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", sidperiodo));

        //        //rpt_zona.ServerReport.SetParameters(parametros2); 
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        Exception mensaje = ex;
        //        Label mensajeusu = new Label();
        //        mensajeusu.Visible = true;
        //        mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
        //    }
        //}

        private void llenarReporteIngresosStock()
        {
            
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }


            string iiaño = cmb_año.SelectedValue;
            if (iiaño == "")
                iiaño = "0";

            string iimes = cmb_mes.SelectedValue;
            if (iimes == "")
                iimes = "0";

            string iiperiodo = cmb_periodo.SelectedValue;
            if (iiperiodo == "")
                iiperiodo = "0";


            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }


            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }



            try
            {
                //Stock Moderno
                rpt_stock_m.Visible = true;
                rpt_stock_m.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_stock_m.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_Stock_Ingreso_SF_M";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_stock_m.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_stock_m.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductcategory", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idbrand", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductfamily", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductfamily", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", id_subfamilias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idperson", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSku", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idffvv", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));
                rpt_stock_m.ServerReport.SetParameters(parametros);

            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }
        }

        private void llenarReporteIngresosStock_inicial()
        {
            TabContainer_Reporte_Stock_SF_M.ActiveTab.TabIndex = 0;
            
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }

                  string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }


                string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }

            try
            {
                //Stock Moderno
                rpt_stock_m.Visible = true;
                rpt_stock_m.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_stock_m.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_Stock_Ingreso_SF_M";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_stock_m.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_stock_m.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductcategory", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idbrand", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductfamily", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductfamily", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idproductsubfamily", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idperson", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSku", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idffvv", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_stock_m.ServerReport.SetParameters(parametros);

            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";
            }
        }
        private void llenarReporteQuiebresXPtoVenta()
        {
           
            //string agrupaciónComercial, string ciudad, string nodocomercial, string ptoVenta, string marca, string categoria, string supervisor, string ffvv, string familias, string subfamilias, int year, int month, int periodo) {
            MyAccordion.SelectedIndex = 1;
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }

            string iiaño = cmb_año.SelectedValue;
            if (iiaño == "")
                iiaño = "0";

            string iimes = cmb_mes.SelectedValue;
            if (iimes == "")
                iimes = "0";

            string iiperiodo = cmb_periodo.SelectedValue;
            if (iiperiodo == "")
                iiperiodo = "0";

            try
            {

                //Stock Moderno
                rpt_Quiebre_PtoVenta.Visible = true;
                rpt_Quiebre_PtoVenta.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Quiebre_PtoVenta.ServerReport.ReportPath = "/Reporte_Precios_V1/Report_Stock_SF_Quiebre";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Quiebre_PtoVenta.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Quiebre_PtoVenta.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                // cmb_fuerzav.SelectedItem.Value
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("companyid", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Codchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("codstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Cadena", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ciudad", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Cliente", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ptoventa", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ptoventa", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Marca", cmb_marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Supervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("ffvv", cmb_fuerzav.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familias", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familias", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Subfamilias", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Subfamilias", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", iiperiodo));

                rpt_Quiebre_PtoVenta.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }


        }
        private void llenarReporteQuiebresXPtoVenta_inicial()
        {
            //string agrupaciónComercial, string ciudad, string nodocomercial, string ptoVenta, string marca, string categoria, string supervisor, string ffvv, string familias, string subfamilias, int year, int month, int periodo) {
            

            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }


            try
            {

                //Stock Moderno
                rpt_Quiebre_PtoVenta.Visible = true;
                rpt_Quiebre_PtoVenta.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Quiebre_PtoVenta.ServerReport.ReportPath = "/Reporte_Precios_V1/Report_Stock_SF_Quiebre";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Quiebre_PtoVenta.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Quiebre_PtoVenta.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                // cmb_fuerzav.SelectedItem.Value
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("companyid", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Codchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("codstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Cadena", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ciudad", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Cliente", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ptoventa", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ptoventa", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Marca", cmb_marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Supervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("ffvv", cmb_fuerzav.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familias", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familias", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Subfamilias", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Subfamilias", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", sidperiodo));

                rpt_Quiebre_PtoVenta.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }


        }
        private void llenarReporteQuiebresXProducto()
        {
            MyAccordion.SelectedIndex = 1;
            //string agrupaciónComercial, string ciudad, string nodocomercial, string ptoVenta, string marca, string categoria, string supervisor, string ffvv, string familias, string subfamilias, int year, int month, int periodo) {
            TabContainer_Reporte_Stock_SF_M.ActiveTab.TabIndex = 2;
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }


            string iiaño = cmb_año.SelectedValue;
            if (iiaño == "")
                iiaño = "0";

            string iimes = cmb_mes.SelectedValue;
            if (iimes == "")
                iimes = "0";

            string iiperiodo = cmb_periodo.SelectedValue;
            if (iiperiodo == "")
                iiperiodo = "0";

            try
            {

                //QuiebreXProducto Moderno
                rpt_Quiebre_Producto.Visible = true;
                rpt_Quiebre_Producto.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Quiebre_Producto.ServerReport.ReportPath = "/Reporte_Precios_V1/Report_Stock_SF_QuiebreXProducto";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Quiebre_Producto.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Quiebre_Producto.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                // cmb_fuerzav.SelectedItem.Value
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("companyid", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Codchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("codstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Cadena", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ciudad", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Cliente", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ptoventa", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ptoventa", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Marca", cmb_marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Supervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("ffvv", cmb_fuerzav.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familias", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familias", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Subfamilias", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Subfamilias", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", iiperiodo));

                rpt_Quiebre_Producto.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }


        }
        private void llenarReporteQuiebresXProducto_inicial()
        {
            //string agrupaciónComercial, string ciudad, string nodocomercial, string ptoVenta, string marca, string categoria, string supervisor, string ffvv, string familias, string subfamilias, int year, int month, int periodo) {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            canal = this.Session["Canal"].ToString().Trim();
            Report = Convert.ToInt32(this.Session["Reporte"]);

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }

            try
            {

                //QuiebreXProducto Moderno
                rpt_Quiebre_Producto.Visible = true;
                rpt_Quiebre_Producto.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Quiebre_Producto.ServerReport.ReportPath = "/Reporte_Precios_V1/Report_Stock_SF_QuiebreXProducto";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Quiebre_Producto.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Quiebre_Producto.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                // cmb_fuerzav.SelectedItem.Value
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("companyid", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Codchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("codstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Cadena", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ciudad", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Cliente", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ptoventa", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Ptoventa", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Marca", cmb_marca.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Categoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Supervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("ffvv", cmb_fuerzav.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familias", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Familias", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Subfamilias", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("Subfamilias", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("period", sidperiodo));

                rpt_Quiebre_Producto.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }


        }

        private void llenarReporteVentasXFamilia()
        {
            MyAccordion.SelectedIndex = 1;
            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0) {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++) {
                    if (ddl_Subfamilia.Items[i].Selected) {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }


            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }


            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }

            string iiaño = cmb_año.SelectedValue;
            if (iiaño == "")
                iiaño = "0";

            string iimes = cmb_mes.SelectedValue;
            if (iimes == "")
                iimes = "0";

            string iiperiodo = cmb_periodo.SelectedValue;
            if (iiperiodo == "")
                iiperiodo = "0";


            try
            {
                rpt_Ventas_X_Familia.Visible = true;
                rpt_Ventas_X_Familia.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Ventas_X_Familia.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Ventas_X_Familia";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Ventas_X_Familia.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Ventas_X_Familia.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSku", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", "0"));

                rpt_Ventas_X_Familia.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }
        }
        private void llenarReporteVentasXFamilia_inicial()
        {

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }


            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }


            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }



            try
            {
                rpt_Ventas_X_Familia.Visible = true;
                rpt_Ventas_X_Familia.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Ventas_X_Familia.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Ventas_X_Familia";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Ventas_X_Familia.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Ventas_X_Familia.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSku", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", "0"));

                rpt_Ventas_X_Familia.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }
        }
        private void llenarReporteQuiebreXTienda()
        {
            MyAccordion.SelectedIndex = 1;
            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }


            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }


            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }

            string iiaño = cmb_año.SelectedValue;
            if (iiaño == "")
                iiaño = "0";

            string iimes = cmb_mes.SelectedValue;
            if (iimes == "")
                iimes = "0";

            string iiperiodo = cmb_periodo.SelectedValue;
            if (iiperiodo == "")
                iiperiodo = "0";

            try
            {
                rpt_Quiebre_X_Tienda.Visible = true;
                rpt_Quiebre_X_Tienda.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Quiebre_X_Tienda.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Quiebres_X_PtoVenta";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Quiebre_X_Tienda.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Quiebre_X_Tienda.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSku", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_Quiebre_X_Tienda.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }
        }
        private void llenarReporteQuiebreXTienda_inicial()
        {

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }


            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }


            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }



            try
            {
                rpt_Quiebre_X_Tienda.Visible = true;
                rpt_Quiebre_X_Tienda.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Quiebre_X_Tienda.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Quiebres_X_PtoVenta";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Quiebre_X_Tienda.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Quiebre_X_Tienda.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSku", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_Quiebre_X_Tienda.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }
        }
        private void llenarVentaXTienda()
        {
            MyAccordion.SelectedIndex = 1;
            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }


            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }


            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }

            string iiaño = cmb_año.SelectedValue;
            if (iiaño == "")
                iiaño = "0";

            string iimes = cmb_mes.SelectedValue;
            if (iimes == "")
                iimes = "0";

            string iiperiodo = cmb_periodo.SelectedValue;
            if (iiperiodo == "")
                iiperiodo = "0";

            try
            {
                rpt_Venta_X_Tienda.Visible = true;
                rpt_Venta_X_Tienda.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Venta_X_Tienda.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Ventas_X_PtoVenta";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Venta_X_Tienda.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Venta_X_Tienda.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSku", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_Venta_X_Tienda.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }
        }
        private void llenarVentaXTienda_inicial()
        {
            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }


            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }


            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }


            try
            {
                rpt_Venta_X_Tienda.Visible = true;
                rpt_Venta_X_Tienda.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Venta_X_Tienda.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Ventas_X_PtoVenta";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Venta_X_Tienda.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Venta_X_Tienda.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSku", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_Venta_X_Tienda.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }
        }
        private void llenarVentasTotalesXMes()
        {
            MyAccordion.SelectedIndex = 1;
            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }

            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }

            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }

            string iiaño = cmb_año.SelectedValue;
            if (iiaño == "")
                iiaño = "0";

            string iimes = cmb_mes.SelectedValue;
            if (iimes == "")
                iimes = "0";

            string iiperiodo = cmb_periodo.SelectedValue;
            if (iiperiodo == "")
                iiperiodo = "0";

            try
            {
                rpt_VentasTotales_X_Mes.Visible = true;
                rpt_VentasTotales_X_Mes.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_VentasTotales_X_Mes.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Ventas_X_Mes";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_VentasTotales_X_Mes.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_VentasTotales_X_Mes.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSku", id_productos));

                rpt_VentasTotales_X_Mes.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }
        }
        private void llenarVentasTotalesXMes_inicial()
        {

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }

            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }

            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }



            try
            {
                rpt_VentasTotales_X_Mes.Visible = true;
                rpt_VentasTotales_X_Mes.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_VentasTotales_X_Mes.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Ventas_X_Mes";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_VentasTotales_X_Mes.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_VentasTotales_X_Mes.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSku", id_productos));

                rpt_VentasTotales_X_Mes.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }
        }
        //private void llenarVentasMensuales_X_Familia()
        //{
        //    MyAccordion.SelectedIndex = 1;
        //    string id_PuntosDeVenta = "0";
        //    string puntosDeVenta = "";
        //    if (ddl_PuntoDeVenta.SelectedIndex >= 0)
        //    {
        //        for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
        //        {
        //            if (ddl_PuntoDeVenta.Items[i].Selected)
        //            {
        //                puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
        //            }
        //        }
        //        id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
        //    }

        //    string id_familias = "0";
        //    string familias = "";
        //    if (ddl_Familia.SelectedIndex >= 0)
        //    {
        //        for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
        //        {
        //            if (ddl_Familia.Items[i].Selected)
        //            {
        //                familias = familias + ddl_Familia.Items[i].Value + " ,";
        //            }
        //        }
        //        id_familias = familias.Substring(0, familias.Length - 1);
        //    }

        //    string id_subfamilias = "0";
        //    string subfamilias = "";
        //    if (ddl_Subfamilia.SelectedIndex >= 0)
        //    {
        //        for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
        //        {
        //            if (ddl_Subfamilia.Items[i].Selected)
        //            {
        //                subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
        //            }
        //        }
        //        id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
        //    }


        //    string id_productos = "0";
        //    string productos = "";
        //    if (ddl_Producto.SelectedIndex >= 0)
        //    {
        //        for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
        //        {
        //            if (ddl_Producto.Items[i].Selected)
        //            {
        //                productos = productos + ddl_Producto.Items[i].Value + " ,";
        //            }
        //        }
        //        id_productos = productos.Substring(0, productos.Length - 1);
        //    }


        //    string id_dias = "0";
        //    string dias = "";
        //    if (ddl_Dia.SelectedIndex >= 0)
        //    {
        //        for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
        //        {
        //            if (ddl_Dia.Items[i].Selected)
        //            {
        //                dias = dias + ddl_Dia.Items[i].Value + " ,";
        //            }
        //        }
        //        id_dias = dias.Substring(0, dias.Length - 1);
        //    }

        //    string iiaño = cmb_año.SelectedValue;
        //    if (iiaño == "")
        //        iiaño = "0";

        //    string iimes = cmb_mes.SelectedValue;
        //    if (iimes == "")
        //        iimes = "0";

        //    string iiperiodo = cmb_periodo.SelectedValue;
        //    if (iiperiodo == "")
        //        iiperiodo = "0";

        //    try
        //    {
        //        rpt_VentasMensuales_X_Familia.Visible = true;
        //        rpt_VentasMensuales_X_Familia.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;

        //        rpt_VentasMensuales_X_Familia.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Ventas_X_Mes_X_Familia";

        //        String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

        //        rpt_VentasMensuales_X_Familia.ServerReport.ReportServerUrl = new Uri(strConnection);
        //        rpt_VentasMensuales_X_Familia.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
        //        List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
        //        //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
        //        //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
        //        //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", cmb_subfamilia.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSku", id_productos));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

        //        rpt_VentasMensuales_X_Familia.ServerReport.SetParameters(parametros);
        //    }
        //    catch (Exception ex)
        //    {

        //        Exception mensaje = ex;
        //        Label mensajeusu = new Label();
        //        mensajeusu.Visible = true;
        //        mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

        //    }
        //}
        //private void llenarVentasMensuales_X_Familia_inicial()
        //{
        //    string id_PuntosDeVenta = "0";
        //    string puntosDeVenta = "";
        //    if (ddl_PuntoDeVenta.SelectedIndex >= 0)
        //    {
        //        for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
        //        {
        //            if (ddl_PuntoDeVenta.Items[i].Selected)
        //            {
        //                puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
        //            }
        //        }
        //        id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
        //    }

        //    string id_familias = "0";
        //    string familias = "";
        //    if (ddl_Familia.SelectedIndex >= 0)
        //    {
        //        for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
        //        {
        //            if (ddl_Familia.Items[i].Selected)
        //            {
        //                familias = familias + ddl_Familia.Items[i].Value + " ,";
        //            }
        //        }
        //        id_familias = familias.Substring(0, familias.Length - 1);
        //    }

        //    string id_subfamilias = "0";
        //    string subfamilias = "";
        //    if (ddl_Subfamilia.SelectedIndex >= 0)
        //    {
        //        for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
        //        {
        //            if (ddl_Subfamilia.Items[i].Selected)
        //            {
        //                subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
        //            }
        //        }
        //        id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
        //    }


        //    string id_productos = "0";
        //    string productos = "";
        //    if (ddl_Producto.SelectedIndex >= 0)
        //    {
        //        for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
        //        {
        //            if (ddl_Producto.Items[i].Selected)
        //            {
        //                productos = productos + ddl_Producto.Items[i].Value + " ,";
        //            }
        //        }
        //        id_productos = productos.Substring(0, productos.Length - 1);
        //    }


        //    string id_dias = "0";
        //    string dias = "";
        //    if (ddl_Dia.SelectedIndex >= 0)
        //    {
        //        for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
        //        {
        //            if (ddl_Dia.Items[i].Selected)
        //            {
        //                dias = dias + ddl_Dia.Items[i].Value + " ,";
        //            }
        //        }
        //        id_dias = dias.Substring(0, dias.Length - 1);
        //    }

        //    try
        //    {
        //        rpt_VentasMensuales_X_Familia.Visible = true;
        //        rpt_VentasMensuales_X_Familia.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;

        //        rpt_VentasMensuales_X_Familia.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Ventas_X_Mes_X_Familia";

        //        String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

        //        rpt_VentasMensuales_X_Familia.ServerReport.ReportServerUrl = new Uri(strConnection);
        //        rpt_VentasMensuales_X_Familia.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
        //        List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
        //        //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
        //        //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
        //        //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", cmb_subfamilia.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSku", id_productos));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
        //        parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

        //        rpt_VentasMensuales_X_Familia.ServerReport.SetParameters(parametros);
        //    }
        //    catch (Exception ex)
        //    {

        //        Exception mensaje = ex;
        //        Label mensajeusu = new Label();
        //        mensajeusu.Visible = true;
        //        mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

        //    }
        //}
        private void llenarVentasAcumuladas_X_Prod()
        {
            MyAccordion.SelectedIndex = 1;
            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }


            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }


            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }

            string iiaño = cmb_año.SelectedValue;
            if (iiaño == "")
                iiaño = "0";

            string iimes = cmb_mes.SelectedValue;
            if (iimes == "")
                iimes = "0";

            string iiperiodo = cmb_periodo.SelectedValue;
            if (iiperiodo == "")
                iiperiodo = "0";

            try
            {
                rpt_VentasAcumuladas_X_Prod.Visible = true;
                rpt_VentasAcumuladas_X_Prod.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_VentasAcumuladas_X_Prod.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Ventas_X_Producto";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_VentasAcumuladas_X_Prod.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_VentasAcumuladas_X_Prod.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSKU", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_VentasAcumuladas_X_Prod.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }
        }
        private void llenarVentasAcumuladas_X_Prod_inicial()
        {

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }


            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }


            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }


            try
            {
                rpt_VentasAcumuladas_X_Prod.Visible = true;
                rpt_VentasAcumuladas_X_Prod.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_VentasAcumuladas_X_Prod.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Ventas_X_Producto";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_VentasAcumuladas_X_Prod.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_VentasAcumuladas_X_Prod.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSKU", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_VentasAcumuladas_X_Prod.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }
        }
        private void llenarEvolucion_X_Productos_X_Semana()
        {
            MyAccordion.SelectedIndex = 1;
            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }


            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }


            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }

            string iiaño = cmb_año.SelectedValue;
            if (iiaño == "")
                iiaño = "0";

            string iimes = cmb_mes.SelectedValue;
            if (iimes == "")
                iimes = "0";

            string iiperiodo = cmb_periodo.SelectedValue;
            if (iiperiodo == "")
                iiperiodo = "0";


            try
            {
                rpt_Evo_Prod_Sem.Visible = true;
                rpt_Evo_Prod_Sem.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Evo_Prod_Sem.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Evolucion_X_Productos_X_Semana";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Evo_Prod_Sem.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Evo_Prod_Sem.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSKU", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", iiaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", iimes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", iiperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_Evo_Prod_Sem.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }
        }
        private void llenarEvolucion_X_Productos_X_Semana_inicial()
        {

            string id_PuntosDeVenta = "0";
            string puntosDeVenta = "";
            if (ddl_PuntoDeVenta.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_PuntoDeVenta.Items.Count - 1; i++)
                {
                    if (ddl_PuntoDeVenta.Items[i].Selected)
                    {
                        puntosDeVenta = puntosDeVenta + ddl_PuntoDeVenta.Items[i].Value + " ,";
                    }
                }
                id_PuntosDeVenta = puntosDeVenta.Substring(0, puntosDeVenta.Length - 1);
            }

            string id_familias = "0";
            string familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Familia.Items.Count - 1; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        familias = familias + ddl_Familia.Items[i].Value + " ,";
                    }
                }
                id_familias = familias.Substring(0, familias.Length - 1);
            }

            string id_subfamilias = "0";
            string subfamilias = "";
            if (ddl_Subfamilia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Subfamilia.Items.Count - 1; i++)
                {
                    if (ddl_Subfamilia.Items[i].Selected)
                    {
                        subfamilias = subfamilias + ddl_Subfamilia.Items[i].Value + " ,";
                    }
                }
                id_subfamilias = subfamilias.Substring(0, subfamilias.Length - 1);
            }




            string id_productos = "0";
            string productos = "";
            if (ddl_Producto.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Producto.Items.Count - 1; i++)
                {
                    if (ddl_Producto.Items[i].Selected)
                    {
                        productos = productos + ddl_Producto.Items[i].Value + " ,";
                    }
                }
                id_productos = productos.Substring(0, productos.Length - 1);
            }


            string id_dias = "0";
            string dias = "";
            if (ddl_Dia.SelectedIndex >= 0)
            {
                for (int i = 0; i <= ddl_Dia.Items.Count - 1; i++)
                {
                    if (ddl_Dia.Items[i].Selected)
                    {
                        dias = dias + ddl_Dia.Items[i].Value + " ,";
                    }
                }
                id_dias = dias.Substring(0, dias.Length - 1);
            }



            try
            {
                rpt_Evo_Prod_Sem.Visible = true;
                rpt_Evo_Prod_Sem.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent;

                rpt_Evo_Prod_Sem.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_SF_M_Stock_Evolucion_X_Productos_X_Semana";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];

                rpt_Evo_Prod_Sem.ServerReport.ReportServerUrl = new Uri(strConnection);
                rpt_Evo_Prod_Sem.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();

                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcompany", icompany.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idstrategy", iservicio.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idchannel", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idcorporacion", cmb_corporacion.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idoficina", cmb_oficina.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idnodecomercial", cmb_nodocomercial.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", cmb_pventa.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idclientpdvcode", id_PuntosDeVenta));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idCategoria", cmb_categoria.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idMarca", cmb_marca.SelectedItem.Value));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", cmb_familia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFamilia", id_familias));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", cmb_subfamilia.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSubfamilia", id_subfamilias));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSKU", id_productos));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idSupervisor", cmb_supervisor.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("idFuerzaDeVenta", cmb_fuerzav.SelectedItem.Value));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("year", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("month", sidmes));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("periodo", sidperiodo));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("day", id_dias));

                rpt_Evo_Prod_Sem.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                Label mensajeusu = new Label();
                mensajeusu.Visible = true;
                mensajeusu.Text = "Se ha perdido la comunicación con nuestro servidor. Disculpe las molestias";

            }
        }

        protected void btngnerar_Click(object sender, EventArgs e)
        {
            //TabContainer_Reporte_Stock_SF_M.ActiveTab.TabIndex = 0;
            MyAccordion.SelectedIndex = 1;
            _AsignarVariables();
            llenarReporteIngresosStock();
            llenarReporteQuiebresXPtoVenta();
            llenarReporteQuiebresXProducto();
            
            llenarReporteVentasXFamilia();
            llenarReporteQuiebreXTienda();
            llenarVentaXTienda();
            llenarVentasTotalesXMes();
            //llenarVentasMensuales_X_Familia();
            llenarVentasAcumuladas_X_Prod();
            llenarEvolucion_X_Productos_X_Semana();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            sUser = this.Session["sUser"].ToString();
            sPassw = this.Session["sPassw"].ToString();
            sNameUser = this.Session["nameuser"].ToString();
            icompany = Convert.ToInt32(this.Session["companyid"]);
            iservicio = Convert.ToInt32(this.Session["Service"]);
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();

            MyAccordion.SelectedIndex = 1;
            //TabContainer_filtros.ActiveTabIndex = 1;
            TabContainer_Reporte_Stock_SF_M.ActiveTabIndex = 0;
            //UpdateProgressContext2();
            

            
            
            if (!IsPostBack)
            {
                try
                {
                    //UpdateProgressContext2();
                    iniciarcombos();

                    _AsignarVariables();
                    cargarParametrosdeXml();
                    llenarReporteIngresosStock_inicial();
                    llenarReporteQuiebresXPtoVenta_inicial();
                    llenarReporteQuiebresXProducto_inicial();
                    llenarReporteVentasXFamilia_inicial();
                    llenarReporteQuiebreXTienda_inicial();
                    llenarVentaXTienda_inicial();
                    llenarVentasTotalesXMes_inicial();
                    //llenarVentasMensuales_X_Familia_inicial();
                    llenarVentasAcumuladas_X_Prod_inicial();
                    llenarEvolucion_X_Productos_X_Semana_inicial();


                    Años();
                    Llena_Meses();
                    llenaoficina();
                    llenacategoria();
                    llenafuerzav();
                    llenasupervisores();

                    llenaCorporacion_ini();
                    llenaNodoComercial_ini();
                    llenaPuntoDeVenta_ini();
                    llenaFamilia_ini();
                    llenaSubFamilia_ini();
                    llenaproductos_ini();

                    //llenaMarca_ini();
                    //llenasubFamilia_ini();
                    

                    

                    //llenafamilia_2();
                    //llenarreporteInicial();
                    //llenarReporteQuiebresXPtoVenta();
                    //llenarReporteQuiebresXProducto();

                }
                catch (Exception ex)
                {
                    Exception mensaje = ex;
                    this.Session.Abandon();
                    //Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }

        }


        protected void ddl_Familia_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Panel_Familia.Controls.Clear();
            //Panel_Familia.Controls.Add(new Literal() { Text = "<b>Las Familias seleccionadas son:</b>" + "<br/>" });
            //foreach (ListItem item in (sender as ListControl).Items)
            //{
            //    if (item.Selected)
            //    {

            //        Panel_Familia.Controls.Add(new Literal() { Text = item.Text + "<br/>" });

            //    }
            //}

            string id_Familias = "";
            if (ddl_Familia.SelectedIndex >= 0)
            {
                
                string cadenaIdFamilia = "";

                for (int i = 0; i < ddl_Familia.Items.Count; i++)
                {
                    if (ddl_Familia.Items[i].Selected)
                    {
                        cadenaIdFamilia = cadenaIdFamilia + ddl_Familia.Items[i].Value + ",";
                    }
                }
                
                id_Familias = cadenaIdFamilia.Substring(0, cadenaIdFamilia.Length - 1);
            }

            llenasubfamilia_2(id_Familias);

        }
        protected void ddl_PuntoDeVenta_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Panel_PuntoDeVenta.Controls.Clear();
            Panel_PuntoDeVenta.Controls.Add(new Literal() { Text = "<b>Las Punto de Venta seleccionadas son:</b>" + "<br/>" });
            foreach (ListItem item in (sender as ListControl).Items)
            {
                if (item.Selected)
                {

                    Panel_PuntoDeVenta.Controls.Add(new Literal() { Text = item.Text + "<br/>" });

                }
            }
        }
        protected void ddl_Subfamilia_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string id_SubFamilias = "";
            try
            {
                if (ddl_Subfamilia.SelectedIndex >= 0)
                {

                    string cadenaIdSubFamilia = "";

                    for (int i = 0; i < ddl_Subfamilia.Items.Count; i++)
                    {
                        if (ddl_Subfamilia.Items[i].Selected)
                        {
                            cadenaIdSubFamilia = cadenaIdSubFamilia + ddl_Subfamilia.Items[i].Value + ",";
                        }
                    }

                    id_SubFamilias = cadenaIdSubFamilia.Substring(0, cadenaIdSubFamilia.Length - 1);
                }

                llenaproductos_2(id_SubFamilias);
            }
            catch (Exception ex)
            {

            }
        }
        protected void cmb_periodo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LLenarDiasxPerido();
            LLenarDiasxPeriodo_2();
        }
        //icompany, canal, 28, cmb_año.SelectedValue.ToString(), cmb_mes.SelectedValue.ToString(), cmb_periodo.SelectedValue.ToString()


        private void llenasubfamilia_2(String id_familias)
        {
            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", sidproductFamily);
            try
            {
                DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK_CHECK", id_familias);

                if (dtfamilia.Rows.Count > 0)
                {
                    ddl_Subfamilia.Enabled = true;
                    ddl_Subfamilia.DataSource = dtfamilia;
                    ddl_Subfamilia.DataTextField = "subfam_nombre";
                    ddl_Subfamilia.DataValueField = "id_ProductSubFamily";
                    ddl_Subfamilia.DataBind();

                    //cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenafamilia_2()
        {
            //UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK
            try
            {
                DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK", cmb_categoria.SelectedItem.Value);

                if (dtfamilia.Rows.Count > 0)
                {
                    ddl_Familia.Enabled = true;
                    ddl_Familia.DataSource = dtfamilia;
                    ddl_Familia.DataTextField = "name_Family";
                    ddl_Familia.DataValueField = "id_ProductFamily";
                    ddl_Familia.DataBind();

                    //ddl_Familia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
                else
                {
                    //ddl_Familia.Items.Insert(0, new RadComboBoxItem("---No tiene familias asociadas---", "0"));
                    //ddl_Subfamilia.Items.Insert(0, new RadComboBoxItem("---No tiene subfamilias asociadas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenapuntoventa_2() {
            //UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA
            try
            {
                DataTable dtpventa = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA", cmb_nodocomercial.SelectedItem.Value);

                if (dtpventa.Rows.Count > 0)
                {
                    ddl_PuntoDeVenta.Enabled = true;
                    ddl_PuntoDeVenta.DataSource = dtpventa;
                    ddl_PuntoDeVenta.DataTextField = "pdv_Name";
                    ddl_PuntoDeVenta.DataValueField = "id_PointOfsale";
                    ddl_PuntoDeVenta.DataBind();

                    //cmb_pventa.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        //private void llenapuntoventa()
        //{
        //    //UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA
        //    try
        //    {
        //        DataTable dtpventa = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA", cmb_nodocomercial.SelectedItem.Value);

        //        if (dtpventa.Rows.Count > 0)
        //        {
        //            cmb_pventa.Enabled = true;
        //            cmb_pventa.DataSource = dtpventa;
        //            cmb_pventa.DataTextField = "pdv_Name";
        //            cmb_pventa.DataValueField = "id_PointOfsale";
        //            cmb_pventa.DataBind();

        //            cmb_pventa.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Redirect("~/err_mensaje_seccion.aspx", true);
        //    }
        //}
        private void llenasubfamilia()
        {
            ////dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", sidproductFamily);
            //try
            //{
            //    DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", cmb_familia.SelectedItem.Value);

            //    if (dtfamilia.Rows.Count > 0)
            //    {
            //        cmb_subfamilia.Enabled = true;
            //        cmb_subfamilia.DataSource = dtfamilia;
            //        cmb_subfamilia.DataTextField = "subfam_nombre";
            //        cmb_subfamilia.DataValueField = "id_ProductSubFamily";
            //        cmb_subfamilia.DataBind();

            //        cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("~/err_mensaje_seccion.aspx", true);
            //}
        }
        private void llenaproductos_2(String id_SubFamilias)
        {
            try
            {
                DataTable dtprodutos = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SKU_PRODUCTO_REPORT_STOCK", id_SubFamilias);
                if (dtprodutos.Rows.Count > 0)
                {
                    ddl_Producto.Enabled = true;
                    ddl_Producto.DataSource = dtprodutos;
                    ddl_Producto.DataTextField = "productoNombre";
                    ddl_Producto.DataValueField = "cod_Product";
                    ddl_Producto.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void LLenarDiasxPeriodo_2()
        {

            //DataTable dtDias = oCoon.ejecutarDataTable("UP_WEBXPLORA_OBTENERDIASXPERIODO_SF_MODERNO", idCompany, idCanal, idReporte, idYear, idMonth, idPeriodo);
            DataTable dtDias = oCoon.ejecutarDataTable("UP_WEBXPLORA_OBTENERDIASXPERIODO_SF_MODERNO", icompany, canal, 28, cmb_año.SelectedValue, cmb_mes.SelectedValue, cmb_periodo.SelectedValue);


            if (dtDias.Rows.Count > 0)
            {
                ddl_Dia.DataSource = dtDias;
                ddl_Dia.DataValueField = "dia";
                ddl_Dia.DataTextField = "dia";
                ddl_Dia.DataBind();
                //ddl_Dia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                ddl_Dia.Enabled = true;
            }
            else
            {
                dtDias = null;
            }
        }
        
        //llenar Combos  --  27/02/2012  pSalas
        private void Años()
        {
            DataTable dty = null;
            dty = Get_Administrativo.Get_ObtenerYears();
            if (dty.Rows.Count > 0)
            {
                cmb_año.DataSource = dty;
                cmb_año.DataValueField = "Years_Number";
                cmb_año.DataTextField = "Years_Number";
                cmb_año.DataBind();
                cmb_año.DataSource = dty;
                cmb_año.DataValueField = "Years_Number";
                cmb_año.DataTextField = "Years_Number";
                cmb_año.DataBind();
                cmb_año.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            }
            else
            {
                dty = null;
            }
        }
        private void Llena_Meses()
        {
            DataTable dtm = Get_Administrativo.Get_ObtenerMeses();

            if (dtm.Rows.Count > 0)
            {
                cmb_mes.DataSource = dtm;
                cmb_mes.DataValueField = "codmes";
                cmb_mes.DataTextField = "namemes";
                cmb_mes.DataBind();

                cmb_mes.DataSource = dtm;
                cmb_mes.DataValueField = "codmes";
                cmb_mes.DataTextField = "namemes";
                cmb_mes.DataBind();
                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            }
            else
            {
                dtm = null;
            }
        }
        private void Llenar_Periodos()
        {
            DataTable dtp = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            canal = this.Session["Canal"].ToString().Trim();
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", canal, icompany, Report, cmb_mes.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                cmb_periodo.DataSource = dtp;
                cmb_periodo.DataValueField = "id_periodo";
                cmb_periodo.DataTextField = "Periodo";
                cmb_periodo.DataBind();

                //cmb_periodo.DataSource = dtp;
                //cmb_periodo.DataValueField = "id_periodo";
                //cmb_periodo.DataTextField = "Periodo";
                //cmb_periodo.DataBind();
                cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            }
            else
            {
                dtp = null;
            }
        }
        private void LLenarDiasxPerido()
        {
            //DataTable dtp = null;
            //dtp = Get_Administrativo.Get_obtener_Dias_Periodo(icompany, canal, Report, cmb_año.SelectedValue, cmb_mes.SelectedValue, Convert.ToInt32(cmb_periodo.SelectedValue));
            //if (dtp.Rows.Count > 0)
            //{
            //    cmb_dia.DataSource = dtp;
            //    cmb_dia.DataValueField = "id_periodo";
            //    cmb_dia.DataTextField = "Dias";
            //    cmb_dia.DataBind();

            //    cmb_dia.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
            //    cmb_dia.Enabled = true;
            //}
            //else
            //{
            //    dtp = null;
            //}
        }
        //métodos para llenar combos para filtros
        // Angel Ortiz
        // 14/10/2011
        private void llenacorporacion()
        {
            //Corporacion.corp_id, Corporacion.corp_name
            try
            {
                DataTable dtcorp = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CORPORACION", cmb_oficina.SelectedItem.Value);

                if (dtcorp.Rows.Count > 0)
                {
                    cmb_corporacion.Enabled = true;
                    cmb_corporacion.DataSource = dtcorp;
                    cmb_corporacion.DataTextField = "corp_name";
                    cmb_corporacion.DataValueField = "corp_id";
                    cmb_corporacion.DataBind();

                    cmb_corporacion.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        //1
        private void llenaoficina()
        {
            if (this.Session["companyid"] != null)
            //if (this.compañia != null)
            {
                int compañia = Convert.ToInt32(this.Session["companyid"]);
                DataTable dtofi = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENEROFICINAS", compañia);

                if (dtofi.Rows.Count > 0)
                {
                    cmb_oficina.Enabled = true;
                    cmb_oficina.DataSource = dtofi;
                    cmb_oficina.DataTextField = "Name_Oficina";
                    cmb_oficina.DataValueField = "cod_Oficina";
                    cmb_oficina.DataBind();

                    cmb_oficina.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            else
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        
        private void llenacadena()
        {
            //UP_WEBXPLORA_OPE_COMBO_CADENA
            try
            {
                DataTable dtcad = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CADENA", cmb_corporacion.SelectedItem.Value);

                if (dtcad.Rows.Count > 0)
                {
                    cmb_nodocomercial.Enabled = true;
                    cmb_nodocomercial.DataSource = dtcad;
                    cmb_nodocomercial.DataTextField = "commercialNodeName";
                    cmb_nodocomercial.DataValueField = "NodeCommercial";
                    cmb_nodocomercial.DataBind();

                    cmb_nodocomercial.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenacategoria()
        {
            //UP_WEBXPLORA_OPE_COMBO_CATEGORIA
            try
            {
                DataTable dtcategoria = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CATEGORIA", icompany, canal);

                if (dtcategoria.Rows.Count > 0)
                {
                    cmb_categoria.Enabled = true;
                    cmb_categoria.DataSource = dtcategoria;
                    cmb_categoria.DataTextField = "Product_Category";
                    cmb_categoria.DataValueField = "id_ProductCategory";
                    cmb_categoria.DataBind();

                    cmb_categoria.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenamarca()
        {
            //UP_WEBXPLORA_OPE_COMBO_CATEGORIA
            try
            {
                DataTable dtmarca = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_MARCA_BY_CATEGORIA_ID", cmb_categoria.SelectedItem.Value);

                if (dtmarca.Rows.Count > 0)
                {
                    cmb_marca.Enabled = true;
                    cmb_marca.DataSource = dtmarca;
                    cmb_marca.DataTextField = "Name_Brand";
                    cmb_marca.DataValueField = "id_Brand";
                    cmb_marca.DataBind();

                    cmb_marca.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
                else
                {
                    cmb_marca.Items.Insert(0, new RadComboBoxItem("---No tiene marcas asociadas---", "0"));
                    //cmb_familia.Items.Insert(0, new RadComboBoxItem("---No tiene familias asociadas---", "0"));
                    //cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---No tiene subfamilias asociadas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        //private void llenafamilia()
        //{
        //    //UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK
        //    try
        //    {
        //        DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK", cmb_categoria.SelectedItem.Value);

        //        if (dtfamilia.Rows.Count > 0)
        //        {
        //            cmb_familia.Enabled = true;
        //            cmb_familia.DataSource = dtfamilia;
        //            cmb_familia.DataTextField = "name_Family";
        //            cmb_familia.DataValueField = "id_ProductFamily";
        //            cmb_familia.DataBind();

        //            cmb_familia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
        //        }
        //        else 
        //        {
        //            cmb_familia.Items.Insert(0, new RadComboBoxItem("---No tiene familias asociadas---", "0"));
        //            cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---No tiene subfamilias asociadas---", "0"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Redirect("~/err_mensaje_seccion.aspx", true);
        //    }
        //}
        private void llenasupervisores()
        {
            //UP_WEBXPLORA_OPE_COMBO_SUPERVISOR_CANAL            
            try
            {
                DataTable dtsupervisor = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUPERVISOR_CANAL", icompany, canal);

                if (dtsupervisor.Rows.Count > 0)
                {
                    cmb_supervisor.Enabled = true;
                    cmb_supervisor.DataSource = dtsupervisor;
                    cmb_supervisor.DataTextField = "Person_NameComplet";
                    cmb_supervisor.DataValueField = "Person_id";
                    cmb_supervisor.DataBind();

                    cmb_supervisor.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenafuerzav()
        {
            //UP_WEBXPLORA_OPE_COMBO_FUERZA_VENTA
            try
            {
                DataTable dtfuerzav = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_FUERZA_VENTA", icompany, canal);

                if (dtfuerzav.Rows.Count > 0)
                {
                    cmb_fuerzav.Enabled = true;
                    cmb_fuerzav.DataSource = dtfuerzav;
                    cmb_fuerzav.DataTextField = "pdv_contact_name";
                    cmb_fuerzav.DataValueField = "id_PointOfSale_Contact";
                    cmb_fuerzav.DataBind();

                    cmb_fuerzav.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        //llenar por Compañia y Canal
        private void llenaCorporacion_ini()
        {
            //Corporacion.corp_id, Corporacion.corp_name
            try
            {
                DataTable dtcorp = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CORPORACION_INI");

                if (dtcorp.Rows.Count > 0)
                {
                    cmb_corporacion.Enabled = true;
                    cmb_corporacion.DataSource = dtcorp;
                    cmb_corporacion.DataTextField = "corp_name";
                    cmb_corporacion.DataValueField = "corp_id";
                    cmb_corporacion.DataBind();

                    cmb_corporacion.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenaPuntoDeVenta_ini()
        {
            //UP_WEBXPLORA_OPE_COMBO_PUNTOVENTA
            try
            {
                DataTable dtpventa = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_PuntoDeVenta_INI", icompany, canal);

                if (dtpventa.Rows.Count > 0)
                {
                    ddl_PuntoDeVenta.Enabled = true;
                    ddl_PuntoDeVenta.DataSource = dtpventa;
                    ddl_PuntoDeVenta.DataTextField = "pdv_Name";
                    ddl_PuntoDeVenta.DataValueField = "id_PointOfsale";
                    ddl_PuntoDeVenta.DataBind();

                    //cmb_pventa.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenaNodoComercial_ini()
        {
            //UP_WEBXPLORA_OPE_COMBO_CADENA
            try
            {
                DataTable dtcad = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_NodoComercial_INI", icompany, canal);

                if (dtcad.Rows.Count > 0)
                {
                    cmb_nodocomercial.Enabled = true;
                    cmb_nodocomercial.DataSource = dtcad;
                    cmb_nodocomercial.DataTextField = "commercialNodeName";
                    cmb_nodocomercial.DataValueField = "NodeCommercial";
                    cmb_nodocomercial.DataBind();

                    cmb_nodocomercial.Items.Insert(0, new RadComboBoxItem("---Todas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenaFamilia_ini()
        {
            //UP_WEBXPLORA_OPE_COMBO_FAMILIA_PRODUCTO_REPORT_STOCK
            try
            {
                DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_FAMILIA_INI", icompany, canal, Report);

                if (dtfamilia.Rows.Count > 0)
                {
                    ddl_Familia.Enabled = true;
                    ddl_Familia.DataSource = dtfamilia;
                    ddl_Familia.DataTextField = "name_Family";
                    ddl_Familia.DataValueField = "id_ProductFamily";
                    ddl_Familia.DataBind();

                    //ddl_Familia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
                else
                {
                    //ddl_Familia.Items.Insert(0, new RadComboBoxItem("---No tiene familias asociadas---", "0"));
                    //ddl_Subfamilia.Items.Insert(0, new RadComboBoxItem("---No tiene subfamilias asociadas---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenaSubFamilia_ini()
        {
            //dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_SUBFAMILIA_PRODUCTO_REPORT_STOCK", sidproductFamily);
            try
            {
                DataTable dtfamilia = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_SUBFAMILIA_INI", icompany, canal, Report);

                if (dtfamilia.Rows.Count > 0)
                {
                    ddl_Subfamilia.Enabled = true;
                    ddl_Subfamilia.DataSource = dtfamilia;
                    ddl_Subfamilia.DataTextField = "subfam_nombre";
                    ddl_Subfamilia.DataValueField = "id_ProductSubFamily";
                    ddl_Subfamilia.DataBind();

                    //cmb_subfamilia.Items.Insert(0, new RadComboBoxItem("---Todos---", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void llenaproductos_ini()
        {
            try
            {
                DataTable dtprodutos = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_SF_M_COMBO_PRODUCTOS_INI", icompany, iservicio, canal, Report);
                if (dtprodutos.Rows.Count > 0)
                {
                    ddl_Producto.Enabled = true;
                    ddl_Producto.DataSource = dtprodutos;
                    ddl_Producto.DataTextField = "productoNombre";
                    ddl_Producto.DataValueField = "cod_Product";
                    ddl_Producto.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        //nuevos


      

    }
}
