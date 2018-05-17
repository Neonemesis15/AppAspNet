using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Lucky.Business.Common.Application;
using Lucky.Business.Common.Security;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Data.Common.Security;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Security;
using Lucky.Entity.Common.Application.Security;
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;
using Lucky.CFG.Tools;

namespace SIGE.Pages.Modulos.Administrativo
{
    public partial class pruebaexcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_img_exporttoexcel_Click(object sender, ImageClickEventArgs e)
        {

            if (this.Session["Exportar_Excel"].ToString().Trim() == "Exportar_Corporacion")
            {
                try
                {
                    DataTable dt;
                    dt = (this.Session["CExporCorp"] as DataTable);
                    GVExportaExcel.DataSource = dt;
                    GVExportaExcel.DataBind();
                    GVExportaExcel.Visible = true;
                    ExportToExcel("Corporación");
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            if (this.Session["Exportar_Excel"].ToString().Trim() == "Exportar_Agrup_Come")
            {
                try
                {
                    DataTable dt;
                    dt = (this.Session["CExporAgrup"] as DataTable);
                    GVExportaExcel.DataSource = dt;
                    GVExportaExcel.DataBind();
                    GVExportaExcel.Visible = true;
                    ExportToExcel("Agrupación");
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            if (this.Session["Exportar_Excel"].ToString().Trim() == "Exportar_Marcas")
            {
                try
                {
                    DataTable dt;
                    dt = (this.Session["CExporMarca"] as DataTable);
                    GVExportaExcel.DataSource = dt;
                    GVExportaExcel.DataBind();
                    GVExportaExcel.Visible = true;
                    ExportToExcel("Marca");
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }


            if (this.Session["Exportar_Excel"].ToString().Trim() == "Exportar_Categorias")
            {
                try
                {
                    DataTable dt2;
                    dt2 = (this.Session["CExporCategoria"] as DataTable);
                    GVExportaExcel.DataSource = dt2;
                    GVExportaExcel.DataBind();
                    GVExportaExcel.Visible = true;
                    ExportToExcel("Categoria");
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }

            if (this.Session["Exportar_Excel"].ToString().Trim() == "Exportar_Productos")
            {
               
                    DataTable dt1;
                    dt1 = (this.Session["CExporProducto"] as DataTable);
                    GVExportaExcel.DataSource = dt1;
                    GVExportaExcel.DataBind();
                    GVExportaExcel.Visible = true;
                    ExportToExcel("Producto");
               
            }
            
            
            if (this.Session["Exportar_Excel"].ToString().Trim() == "Exportar_SubCategorias")
            {
                try
                {
                    DataTable dt3;
                    dt3 = (this.Session["CExporSubCategoria"] as DataTable);
                    GVExportaExcel.DataSource = dt3;
                    GVExportaExcel.DataBind();
                    GVExportaExcel.Visible = true;
                    ExportToExcel("SubCategoria");
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }

            if (this.Session["Exportar_Excel"].ToString().Trim() == "Exportar_Pancla")
            {
                try
                {
                    DataTable dt3;
                    dt3 = (this.Session["CExporPancla"] as DataTable);
                    GVExportaExcel.DataSource = dt3;
                    GVExportaExcel.DataBind();
                    GVExportaExcel.Visible = true;
                    ExportToExcel("Pancla");
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }


            if (this.Session["Exportar_Excel"].ToString().Trim() == "Exportar_SubMarca")
            {
                try
                {
                    DataTable dt3;
                    dt3 = (this.Session["CExporSubMarca"] as DataTable);
                    GVExportaExcel.DataSource = dt3;
                    GVExportaExcel.DataBind();
                    GVExportaExcel.Visible = true;
                    ExportToExcel("SubMarca");
                }
                catch (Exception ex)   
                {
                    ex.Message.ToString();
                }
            }
            
            if (this.Session["Exportar_Excel"].ToString().Trim() == "Exportar_Familia")
            {
                try
                {
                    DataTable dt2;
                    dt2 = (this.Session["CExporFamilia"] as DataTable);
                    GVExportaExcel.DataSource = dt2;
                    GVExportaExcel.DataBind();
                    GVExportaExcel.Visible = true;
                    ExportToExcel("Familia");
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
        }
         
        private void ExportToExcel(string strFileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();

            //GridView1.EnableViewState = false;
            //GridView1.AllowPaging = false;
            //gv.DataBind();

            page.EnableEventValidation = false;
            page.DesignerInitialize();
            page.Controls.Add(form);
            form.Controls.Add(GVExportaExcel);
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
        
       
    }
}