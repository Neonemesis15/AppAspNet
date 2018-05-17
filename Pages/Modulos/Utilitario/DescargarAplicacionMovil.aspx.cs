using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lucky.CFG.Tools;
using Lucky.Business.Common.Application;
using Lucky.Entity.Common.Security;
using Lucky.Entity.Common.Application;

namespace SIGE.Pages.Modulos.Utilitario
{
    public partial class DescargarAplicacionMovil : System.Web.UI.Page
    {
        public static BLPLA_VersionAppMovil blPLA_VersionAppMovil = new BLPLA_VersionAppMovil();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            List<EPLA_VersionAppMovil> listaVersion = blPLA_VersionAppMovil.listarAppMovil();
            RadGridControlVersion.DataSource = listaVersion;
            RadGridControlVersion.DataBind();
        }
    }
}