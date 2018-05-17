using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Lucky.Data;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;
using Lucky.CFG.Util;
using System.Net;
//using LuckyMer.Contracts.DataContract;
using Lucky.CFG.JavaMovil;

namespace SIGE.Pages.Modulos.Cliente.Reportes
{
    public partial class Reporte_v2_CompetenciaColgatePromociones : System.Web.UI.Page
    {
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
       public static DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                RadComboBox cmb_periodo = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_periodo") as RadComboBox;
                RadComboBox cmb_año = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_año") as RadComboBox;
                RadComboBox cmb_mes = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_mes") as RadComboBox;

                cmb_año.DataBind();
                cmb_año.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                cmb_mes.DataBind();
                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                cmb_periodo.DataBind();
                cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

                cargarMenu();
                llenar_Años();
                llenar_Meses();
                llenargrilla();
            }
                
        }


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


        public void llenargrilla()
        {

            RadComboBox cmb_año = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_año") as RadComboBox;
            RadComboBox cmb_periodo = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_periodo") as RadComboBox;
            RadComboBox cmb_mes = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_mes") as RadComboBox;


            int icompany = Convert.ToInt32(this.Session["companyid"]);
            int iservicio = Convert.ToInt32(this.Session["Service"]);
            string canal = this.Session["Canal"].ToString().Trim();
            int Report = Convert.ToInt32(this.Session["Reporte"]);

            Conexion cn = new Conexion();
            ds = cn.ejecutarDataSet("UP_WEBXPLORA_CLIE_V2_REPORTE_PROMOCIONES_COLGATE", canal, iservicio, icompany,cmb_año.SelectedValue,cmb_mes.SelectedValue,cmb_periodo.SelectedValue);
        }
        public void grabarimg(string nombre,byte[] byteArrayIn)
        {

            FileStream fc;


            fc = File.Create(Server.MapPath("Colgate_"+nombre + ".Png"));
            fc.Write(byteArrayIn, 0, byteArrayIn.Length);
            fc.Flush();
            fc.Close();

            imag.Src=("Colgate_"+nombre + ".Png");
           
            link.HRef = ("Colgate_"+nombre + ".Png");
        }

        public void grabarimgcompetencia(string nombre, byte[] byteArrayIn)
        {

            FileStream fc;

            fc = File.Create(Server.MapPath("Competencia_"+nombre + ".Png"));
            fc.Write(byteArrayIn, 0, byteArrayIn.Length);
            fc.Flush();
            fc.Close();

            Img2.Src = ("Competencia_"+nombre + ".Png");

            linkCompe.HRef = ("Competencia_"+nombre + ".Png");
        }

        #region llena Datos

        private void llenar_Años()
        {
            RadComboBox cmb_año = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_año") as RadComboBox;



            DataTable dty = null;
            dty = Get_Administrativo.Get_ObtenerYears();
            if (dty.Rows.Count > 0)
            {
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

        private void llenar_Meses()
        {

            RadComboBox cmb_mes = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_mes") as RadComboBox;

            DataTable dtm = Get_Administrativo.Get_ObtenerMeses();

            if (dtm.Rows.Count > 0)
            {


                cmb_mes.DataSource = dtm;
                cmb_mes.DataValueField = "codmes";
                cmb_mes.DataTextField = "namemes";
                cmb_mes.DataBind();
                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));
                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "-1"));
            }
            else
            {
                dtm = null;

            }

        }
        
        private void llenar_Periodos()
        {
            RadComboBox cmb_año = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_año") as RadComboBox;
            RadComboBox cmb_periodo = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_periodo") as RadComboBox;
            RadComboBox cmb_mes = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_mes") as RadComboBox;


            Conexion oCoon = new Conexion();
            DataTable dtp = null;
           int Report = Convert.ToInt32(this.Session["Reporte"]);
           string canal = this.Session["Canal"].ToString().Trim();
          int icompany = Convert.ToInt32(this.Session["companyid"]);
          dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS_2", canal, icompany, Report, cmb_mes.SelectedValue,cmb_año.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                cmb_periodo.DataSource = dtp;
                cmb_periodo.DataValueField = "id_periodo";
                cmb_periodo.DataTextField = "Periodo";
                cmb_periodo.DataBind();

                cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));


            }
            else
            {

                dtp = null;
                cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Todos--", "0"));

            }
        }



        #endregion



        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox cmb_periodo = RadPanelBar_menu.FindChildByValue<RadPanelItem>("filtro").FindControl("cmb_periodo") as RadComboBox;


            cmb_periodo.Items.Clear();
            llenar_Periodos();
        }

        protected void btngnerar_Click(object sender, EventArgs e)
        {
            llenargrilla();
        }


    }
}