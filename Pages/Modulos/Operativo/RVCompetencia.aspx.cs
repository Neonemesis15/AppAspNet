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
    public partial class RVCompetencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
                                                       
                ReportViewer1.ServerReport.ReportPath="/ActividadesComercio/ActividadesComercio";
                String strConnection = ConfigurationSettings.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(strConnection);
        }
    }
}
