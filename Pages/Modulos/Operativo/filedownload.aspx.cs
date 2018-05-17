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

namespace SIGE.Pages.Modulos.Operativo
{
    public partial class filedownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filename = this.Session["ArchivoCSV"].ToString().Trim();

            if (!String.IsNullOrEmpty(filename))
            {
                String dlDir = Server.MapPath("PictureComercio" + "\\" + this.Session["sUser"].ToString()) + "\\";
                //String dlDir = @"PictureComercio/";
                //String path = Server.MapPath(dlDir + filename);
                String path = this.Session["DIR"].ToString().Trim() + "\\" + filename;

                System.IO.FileInfo toDownload =
                             new System.IO.FileInfo(path);

                if (toDownload.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition",
                               "attachment; filename=" + toDownload.Name);
                    Response.AddHeader("Content-Length",
                               toDownload.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(dlDir + filename);
                    Response.End();
                }
            }
        }
    }
}