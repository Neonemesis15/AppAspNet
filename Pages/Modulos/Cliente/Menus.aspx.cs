using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Xml;
using Lucky.Entity.Common.Security;
using Lucky.Entity.Common.Application.Security;
using Lucky.Business.Common.Security;
using Lucky.Business.Common.Application;
using Lucky.CFG;
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;
using Lucky.Data;
using Lucky.CFG.Tools;
using SIGE.Facade_Procesos_Administrativos;
using SIGE.Facade_Proceso_Planning;
using SIGE.Facade_Proceso_Cliente;

namespace SIGE.Pages.Modulos.Cliente
{
    public partial class Menus : System.Web.UI.Page
    {
        #region zona Declaración de Objetos y Variables
        string sUser;
        string sPassw;
        string snamecompany;
        string scountry;
        int companyid;
        string canal;
        string sperfilid;
        int codservice;
        bool bdpo, bcity;
        int servicio;
        private Conexion oConn = new Conexion();
        Facade_Proceso_Planning.Facade_Proceso_Planning Planning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();

        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos PdAmon = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Carrusel = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();




        LuckyWs LuckyWs = new LuckyWs();

        #endregion
        private void Obtener_Menustipo()
        {


            DataSet dsment = null;
            MenuEmpresarial.Items.Clear();
            companyid = Convert.ToInt32(this.Session["companyid"]);
            servicio = Convert.ToInt32(Session["Service"]);
            dsment = Carrusel.Get_ObtenerInformes_tipo_cliente(servicio, companyid);



           
            for (int i = 0; i <= dsment.Tables[0].Rows.Count - 1; i++)
            {
                MenuItem item = new MenuItem();
                string s = "<SCRIPT language=\"javascript\">" +
                "alert ('\"MenuEmpresarial.SelectedValue\"'');</SCRIPT>";
                RegisterStartupScript("mensaje", s);
               
               
                item.Text = dsment.Tables[0].Rows[i]["NameReport"].ToString();
                item.Value = dsment.Tables[0].Rows[i]["Report_Id"].ToString();

                
                
                MenuEmpresarial.Items.Add(item);


            }


            //MenuEmpresarial.DataBind();





        }

        private void Obtener_MenuDinamico()
        {

            DataSet dsmendi = null;
            MenuDinamico.Items.Clear();
            companyid = Convert.ToInt32(this.Session["companyid"]);
            servicio = Convert.ToInt32(Session["Service"]);
            dsmendi = Carrusel.Get_ObtenerInformes_Dinamico_cliente(servicio, companyid);



            // MenuItem item = new MenuItem();



            //MenuEmpresarial.Items.Add(item);
            for (int i = 0; i <= dsmendi.Tables[0].Rows.Count - 1; i++)
            {
                MenuItem item = new MenuItem();
                item.Text = dsmendi.Tables[0].Rows[i]["NameReport"].ToString();
                item.Value = dsmendi.Tables[0].Rows[i]["Report_Id"].ToString();


                MenuDinamico.Items.Add(item);


            }


            //MenuEmpresarial.DataBind();





        }

        protected void Page_Load(object sender, EventArgs e)
        {
            


            if (!IsPostBack)
            { }

        }
        protected void BtnAdd_Click(object sender, ImageClickEventArgs e)
        {
            Obtener_Menustipo();
            BtnRest.Visible = true;
            BtnAdd.Visible = false;
            MenuEmpresarial.Visible = true;
            BtnRestDina.Visible = false;
            BtnAddDina.Visible = true;
            MenuDinamico.Visible = false;
            BtnRestFav.Visible = false;
            BtnAddFav.Visible = true;
            MenuFavoritos.Visible = false;
            //DatabinPLanVentas();
            //DatabinContriPlan();
        }

        protected void BtnRest_Click(object sender, ImageClickEventArgs e)
        {
            BtnAdd.Visible = true;
            BtnRest.Visible = false;
            MenuEmpresarial.Visible = false;
        }

        protected void BtnAddDina_Click(object sender, ImageClickEventArgs e)
        {
            Obtener_MenuDinamico();
            BtnAdd.Visible = true;
            BtnRest.Visible = false;
            MenuEmpresarial.Visible = false;
            BtnRestDina.Visible = true;
            BtnAddDina.Visible = false;
            MenuDinamico.Visible = true;
            BtnRestFav.Visible = false;
            BtnAddFav.Visible = true;
            MenuFavoritos.Visible = false;
            //charSales.Visible = true;
            //charcoPla.Visible = true;


        }

        protected void BtnRestDina_Click(object sender, ImageClickEventArgs e)
        {
            BtnRestDina.Visible = false;
            BtnAddDina.Visible = true;
            MenuDinamico.Visible = false;

        }

        protected void BtnAddFav_Click(object sender, ImageClickEventArgs e)
        {
            BtnAdd.Visible = true;
            BtnRest.Visible = false;
            MenuEmpresarial.Visible = false;
            BtnRestFav.Visible = true;
            BtnAddFav.Visible = false;
            MenuFavoritos.Visible = true;
            BtnRestDina.Visible = false;
            BtnAddDina.Visible = true;
            MenuDinamico.Visible = false;

        }

        protected void BtnRestFav_Click(object sender, ImageClickEventArgs e)
        {
            BtnRestFav.Visible = false;
            BtnAddFav.Visible = true;
            MenuFavoritos.Visible = false;
            BtnRestDina.Visible = false;
            BtnAddDina.Visible = true;
            MenuDinamico.Visible = false;


        }

        protected void MenuEmpresarial_MenuItemClick(object sender, MenuEventArgs e)
        {
            string menutipo;
            string s = "<SCRIPT language=\"javascript\">" +
               "alert ('\"MenuEmpresarial.SelectedValue\"');</SCRIPT>";
            RegisterStartupScript("mensaje", s);
            menutipo= MenuEmpresarial.SelectedItem.Text;
            this.Session["menutipo"] = Request.QueryString["menutipo"];
        }

        protected void MenuDinamico_MenuItemClick(object sender, MenuEventArgs e)
        {
            //Lbltitulo.Text = "Informe" + " " + MenuDinamico.SelectedItem.Text;




        }

       
    }
}
