using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lucky.CFG.Util;

namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class ExportPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string file_name = Request.QueryString["file_name"] == null ? "0" : Request.QueryString["file_name"];

            if (String.Equals(file_name,"0"))
            {
                Response.Redirect("/login.aspx");
            }
            else
            {
                DataTable dt = null;

                dt = Session["DataTableRangosStock"] as DataTable;

                GridViewExportUtil.ExportDataTableToExcel(dt,file_name);

                Session["DataTableRangosStock"] = null; 
            }
        }
    }
}