using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.IO;
using Lucky.Data;
using Lucky.CFG.Util;

namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_v2_Competencia : System.Web.UI.Page
    {
        private int iidcompany;
        private string sidcanal;
        private int iservicio;

        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
         Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();
        Conexion oCoon = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarCobertura();
                cargarAños();
                cargarMes();
                cargarActividades();
                cargarCategorias();


                //Parametros inicializados para la carga--------------
                string sidtipoactividad = cmb_actividad.SelectedValue;
                string sidano = cmb_año.SelectedValue;
                string sidmes = cmb_mes.SelectedValue;
                if (sidmes == "")
                    sidmes = "0";
                string sidoficina = cmb_oficina.SelectedValue;
                string sidcategoria = cmb_categoria.SelectedValue;


                if (cmb_año.SelectedIndex <=0 && cmb_mes.SelectedIndex == 0)
                {
                    iidcompany = Convert.ToInt32(this.Session["companyid"]);
                    sidcanal = this.Session["Canal"].ToString().Trim();
                    int Report = Convert.ToInt32(this.Session["Reporte"]);
                    Periodo p = new Periodo();

                    p.Reportid = Report;
                    p.Cliente = iidcompany;
                    p.Canal = sidcanal;
                    

                    p.Set_PeriodoConDataValidada();

                    sidano = p.Año;
                    sidmes = p.Mes;
                   
                }
                
                //----fin parametros---------------------------

                try
                {
                    cmb_año.SelectedValue = cmb_año.Items.FindByValue(sidano).Value;

                    cmb_mes.SelectedValue = cmb_mes.Items.FindByValue(sidmes).Value;
                }
                catch(Exception ex)
                {
                    ex.Message.ToString();
                    cmb_año.Items.Clear();
                    cmb_mes.Items.Clear();

                    cargarAños();
                    cargarMes();
                }
                cmb_mes.Visible = true;

                cargar_gvCompetencia(sidtipoactividad, sidano, sidmes, sidoficina, sidcategoria);

                cargarParametrosdeXml();
            }
           
        }

        //protected void btn_ocultar_Click(object sender, EventArgs e)
        //{
        //    if (Div_filtros.Visible == true)
        //    {

        //        Div_filtros.Visible = false;
        //        btn_ocultar.Text = "Filtros";
        //    }
        //    else if (Div_filtros.Visible == false)
        //    {
        //        Div_filtros.Visible = true;
        //        btn_ocultar.Text = "Ocultar";
        //    }
        //}
        protected void cmb_año_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_año.SelectedValue == "0")
            {
                lbl_Mes.Visible = false;
                cmb_mes.Items.Clear();
                cmb_mes.Visible = false;

              
                cmb_mes.Items.Clear();
        

            }
            else
            {
               
                lbl_Mes.Visible = true;
                cmb_mes.Visible = true;
            }

            //cargar_gvCompetencia();
        }
    
        protected void cargarCobertura()
        {
            DataTable dt = null;
            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();  
            dt = oCoon.ejecutarDataTable("UP_WEXPLORA_CLIEN_V2_LLENACOMBOS", iidcompany, sidcanal, "", 1);

            if (dt.Rows.Count > 0)
            {
                cmb_oficina.DataSource = dt;
                cmb_oficina.DataValueField = "cod_city";
                cmb_oficina.DataTextField = "name_city";
                cmb_oficina.DataBind();

                cmb_oficina.Items.Insert(0, new ListItem("--Todos--", "0"));
            }

        }
        private void cargarAños()
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
                cmb_año.Items.Insert(0, new ListItem("--Todos--", "0"));
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
                cmb_mes.DataSource = dtm;
                cmb_mes.DataValueField = "codmes";
                cmb_mes.DataTextField = "namemes";
                cmb_mes.DataBind();
                cmb_mes.Items.Insert(0, new ListItem("--Todos--", "0"));

            }
        }
        private void cargarCategorias()
        {
             DataSet dtcatego = null;
            //dtcatego = Get_DataClientes.Get_Obtenerinfocombos(iidcompany,sidcanal,"",2);
             iidcompany = Convert.ToInt32(this.Session["companyid"]);
              iservicio = Convert.ToInt32(this.Session["Service"]);
              dtcatego = Get_DataClientes.Get_ObtenercategoriasCliente_Servicio(iidcompany, iservicio);
            if (dtcatego.Tables[0].Rows.Count > 0)
            {
                cmb_categoria.DataSource = dtcatego;
                cmb_categoria.DataValueField = "idcatego";
                cmb_categoria.DataTextField = "namecatego";
                cmb_categoria.DataBind();
                cmb_categoria.Items.Insert(0, new ListItem("--Seleccione--", "0"));

            }
            else
            {
                dtcatego = null;
            }
        }
        private void cargarActividades()
        {
            DataTable dt = null;
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_COMBO_TIPO_ACTIVIDAD");
            if (dt.Rows.Count > 0)
            {

                cmb_actividad.DataSource = dt;
                cmb_actividad.DataValueField = "id_Tipo_Act";
                cmb_actividad.DataTextField = "descripcion";
                cmb_actividad.DataBind();
                cmb_actividad.Items.Insert(0, new ListItem("--Todas--", "0"));

            }
            else
            {
                cmb_categoria.DataSource = dt;
            }
        }
        public static DataTable dt_static = new DataTable();
        protected void cargar_gvCompetencia(string sidtipoactividad,string sidano,string sidmes,string sidoficina,string sidcategoria)
        {

            lbl_msj.Visible = false;
            lbl_categoria.Visible = false;

            DataTable dt = null;

            iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
            sidcanal = this.Session["Canal"].ToString();

            try
            {
                dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_REPORTE_COMPETENCIA", iidcompany, sidcanal, sidtipoactividad, sidano, sidmes,sidoficina, sidcategoria);

                if (dt.Rows.Count > 0)
                {

                    gv_competenciaV2.DataSource = dt;
                    gv_competenciaV2.DataBind();

                    dt_static = dt;

                    gv_competencia_to_excel.DataSource = dt;
                    gv_competencia_to_excel.DataBind();

                    lbl_categoria.Visible = true;

                    //lbl_msj.Text = dt.Rows.Count+" Resultados";
                    //lbl_msj.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    gv_competenciaV2.DataBind();

                    //lbl_msj.Visible = true;
                    //lbl_msj.Text = dt.Rows.Count + " Resultados";
                    //lbl_msj.ForeColor = System.Drawing.Color.Green;
                }
            }
            catch (Exception ex)
            {
                lbl_msj.Text = ex.Message.ToString();
            }
        }
        
        protected void btn_img_buscar_detalle_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{

                string sidcompetencia = ((ImageButton)sender).CommandArgument;
                int iidcompetencia = Convert.ToInt32(sidcompetencia);


                DataTable dt = null;
                dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_REPORTE_COMPETENCIA_DETALLE", iidcompetencia);

                lbl_foto_comentario.Visible = false;
                if (dt.Rows.Count == 0)
                {
                    lbl_foto_comentario.Visible = true;
                    lbl_foto_comentario.ForeColor = System.Drawing.Color.Red;
                    lbl_foto_comentario.Text = "No hay detalle";
                    foto_url.Visible = false;
                    gv_detalle_competencia.DataBind();
                }
                else
                {
                    foto_url.Visible = true;
                    //gv_detalle_competencia.Visible = true;
                    gv_detalle_competencia.DataSource = dt;
                    gv_detalle_competencia.DataBind();

                    DataTable dt2 = null;
                    dt2 = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_REPORTE_COMPETENCIA_FOTO", iidcompetencia);

                    if (dt2.Rows.Count > 0)
                    {
                        foto_url.ImageUrl = "Informe_de_Competencia/Fotos/" + dt2.Rows[0]["Url_foto"].ToString();
                        lbl_foto_comentario.Text = "Comentario: " + dt2.Rows[0]["Comentario"].ToString();
                        lbl_foto_fecha.Text = "Fecha de toma: " + dt2.Rows[0]["Fec_Reg_Cel"].ToString();
                        lbl_foto_comentario.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        foto_url.ImageUrl = "Informe_de_Competencia/Fotos/sin_url_imagen.jpg";
                        lbl_foto_comentario.Text = "No se tomó la foto para este registro.";
                        lbl_foto_comentario.ForeColor = System.Drawing.Color.Red;
                        lbl_foto_fecha.Text = "";
                    } 
                } 
                ModalPopupExtender_detalle.Show();
            //}
            //catch (Exception ex)
            //{ ex.Message.ToString(); }
        }
        protected void cmb_categoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Parametros inicializados para la carga--------------
            string sidtipoactividad = cmb_actividad.SelectedValue;
            string sidano = cmb_año.SelectedValue;
            string sidmes = cmb_mes.SelectedValue;
            if (sidmes == "")
                sidmes = "0";
            string sidoficina = cmb_oficina.SelectedValue;
            string sidcategoria = cmb_categoria.SelectedValue;

            //----fin parametros---------------------------
            lbl_msj.Visible = false;
            if (cmb_categoria.SelectedValue != "0")
            {
                lbl_categoria.Text = cmb_categoria.SelectedItem.ToString();
                cargar_gvCompetencia(sidtipoactividad, sidano, sidmes, sidoficina, sidcategoria);

            }
            else
            {
                lbl_msj.Visible = true;
                lbl_msj.Text = "Selecciones almenos una categoria";
                lbl_msj.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            //Parametros inicializados para la carga--------------
            string sidtipoactividad = cmb_actividad.SelectedValue;
            string sidano = cmb_año.SelectedValue;
            string sidmes = cmb_mes.SelectedValue;
            if (sidmes == "")
                sidmes = "0";
            string sidoficina = cmb_oficina.SelectedValue;
            string sidcategoria = cmb_categoria.SelectedValue;

            
            //----fin parametros---------------------------

            lbl_msj.Visible = false;

            if (cmb_categoria.SelectedValue != "0")
            {
                cargar_gvCompetencia(sidtipoactividad, sidano, sidmes,sidoficina, sidcategoria);
            }
            else
            {
                lbl_msj.Visible = true;
                lbl_msj.Text = "Selecciones almenos una categoria";
                lbl_msj.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btn_img_toExcel_Click(object sender, ImageClickEventArgs e)
        {
            gv_competencia_to_excel.Visible = true;
            ExportToExcel("Reporte_Comeptencia");
        }
        private void ExportToExcel(string strFileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();

            //gv_competencia.EnableViewState = false;
            //gv_competencia.AllowPaging = false;
            //gv.DataBind();

            page.EnableEventValidation = false;
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(gv_competencia_to_excel);
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";// vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName + ".xls");
            Response.Charset = "UTF-8";

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void btn_cerrar_popup_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupExtender_detalle.Hide();
        }

        protected void gv_competenciaV2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Parametros inicializados para la carga--------------
            string sidtipoactividad = cmb_actividad.SelectedValue;
            string sidano = cmb_año.SelectedValue;
            string sidmes = cmb_mes.SelectedValue;
            if (sidmes == "")
                sidmes = "0";
            string sidoficina = cmb_oficina.SelectedValue;
            string sidcategoria = cmb_categoria.SelectedValue;

            
            //----fin parametros---------------------------
            gv_competenciaV2.PageIndex = e.NewPageIndex;
            cargar_gvCompetencia(sidtipoactividad, sidano, sidmes,sidoficina, sidcategoria);
        }
        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

        protected void gv_competenciaV2_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = dt_static;//gv_competenciaV2.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                gv_competenciaV2.DataSource = dataView;
                gv_competenciaV2.DataBind();
            }
        }

        protected void buttonGuardar_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/parametros.xml");
            Reportes_parametros oReportes_parametros = new Reportes_parametros();


            oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
            oReportes_parametros.Id_user = this.Session["sUser"].ToString();
            oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
            oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
            oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

            oReportes_parametros.Id_oficina = Convert.ToInt32(cmb_oficina.SelectedValue);
            //oReportes_parametros.Id_punto_venta = cmb_punto_de_venta.SelectedValue;
            oReportes_parametros.Id_producto_categoria = cmb_categoria.SelectedValue;
            //string sidmarca = cmb_marca.SelectedValue;
            //if (sidmarca == "")
            //    sidmarca = "0";
            //oReportes_parametros.Id_producto_marca = sidmarca;
            //oReportes_parametros.Id_producto_familia = cmb_familia.SelectedValue;
            oReportes_parametros.Id_año = cmb_año.SelectedValue;
            oReportes_parametros.Id_mes = cmb_mes.SelectedValue;
            oReportes_parametros.Id_tipoactividad = cmb_actividad.SelectedValue;
            
            //----------->
            oReportes_parametros.Descripcion = txt_descripcion_parametros.Text.Trim();

            Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

            if (!System.IO.File.Exists(path))
                oReportes_parametrosToXml.createXml(oReportes_parametros, path);
            else
                oReportes_parametrosToXml.addToXml(oReportes_parametros, path);


            cargarParametrosdeXml();
            txt_descripcion_parametros.Text = "";
            TabContainer_filtros.ActiveTabIndex = 1;
        }

        protected void cargarParametrosdeXml()
        {
            string path = Server.MapPath("~/parametros.xml");

            if (System.IO.File.Exists(path))
            {
                Reportes_parametros oReportes_parametros = new Reportes_parametros();


                oReportes_parametros.Id_reporte = Convert.ToInt32(this.Session["Reporte"]);
                oReportes_parametros.Id_user = this.Session["sUser"].ToString();
                oReportes_parametros.Id_compañia = Convert.ToInt32(this.Session["companyid"]);
                oReportes_parametros.Id_servicio = Convert.ToInt32(this.Session["Service"]);
                oReportes_parametros.Id_canal = this.Session["Canal"].ToString().Trim();

                Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

                RadGrid_parametros.DataSource = oReportes_parametrosToXml.Get_Parametros(oReportes_parametros, path);
                RadGrid_parametros.DataBind();
            }
        }

        protected void btn_img_buscar_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void btn_imb_tab_Click(object sender, ImageClickEventArgs e)
        {
            TabContainer_filtros.ActiveTabIndex = 0;
        }

        protected void RadGrid_parametros_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "BUSCAR")
            {
                Label lbl_id_año = (Label)e.Item.FindControl("lbl_id_año");
                Label lbl_id_mes = (Label)e.Item.FindControl("lbl_id_mes");
                Label lbl_id_periodo = (Label)e.Item.FindControl("lbl_id_periodo");
                Label lbl_id_oficina = (Label)e.Item.FindControl("lbl_id_oficina");
                Label lbl_id_categoria = (Label)e.Item.FindControl("lbl_id_categoria");
                Label lbl_id_tipoactividad = (Label)e.Item.FindControl("lbl_id_tipoactividad");


                cargar_gvCompetencia(lbl_id_tipoactividad.Text.Trim(), lbl_id_año.Text.Trim(), lbl_id_mes.Text.Trim(),lbl_id_oficina.Text.Trim(), lbl_id_categoria.Text.Trim());
            }
            if (e.CommandName == "ELIMINAR")
            {
                Label lbl_id = (Label)e.Item.FindControl("lbl_id");

                string path = Server.MapPath("~/parametros.xml");
                Reportes_parametros oReportes_parametros = new Reportes_parametros();
                oReportes_parametros.Id = Convert.ToInt32(lbl_id.Text);



                Reportes_parametrosToXml oReportes_parametrosToXml = new Reportes_parametrosToXml();

                oReportes_parametrosToXml.DeleteElement(oReportes_parametros, path);
            }
        }

    }
}