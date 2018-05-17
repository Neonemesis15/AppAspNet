using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Business.Common.Application;
using Lucky.Business.Common.Security;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Data.Common.Security;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Security;
using Lucky.Entity.Common.Application.Security;
using SIGE.Facade_Proceso_Cliente;
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;
using Lucky.CFG.Tools;
using System.Configuration;

namespace SIGE.Pages.Modulos.Cliente
{
    public partial class Mod_Cliente_Canales : System.Web.UI.Page
    {
        Conexion oCoon = new Conexion();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente DataCliente = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();
        string sNameUser;        
        string url_foto;

        public void llenacanales()
        {
            try
            {
                DataTable dtcanales = null;

                dtcanales = DataCliente.Get_ObtenerCanalesxCliente(Convert.ToInt32(this.Session["companyid"]));

                if (dtcanales.Rows.Count > 0)
                {
                    derecha.Visible = true;
                    izquierda.Visible = true;
                    ListViewCanales.DataSource = dtcanales;
                    ListViewCanales.DataBind();
                }
                else
                {
                    derecha.Visible = false;
                    izquierda.Visible = false;
                    dtcanales = null;                    
                    ScriptManager.RegisterStartupScript(
                                    this, this.GetType(), "myscript", "alert('Canales no disponibles para el Cliente');", true);


                    return;

                }
            }
            catch (Exception ex) {
                Exception mensaje = ex;
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            
            
            
            }

       

            

            //ImgCanalMayor.Attributes.Add("onMouseOver", "this.src = '/Pages/ImgBooom/botonmayoristaazul.png'");
            //ImgCanalMayor.Attributes.Add("onMouseOut", "this.src = '/Pages/ImgBooom/botonmayoristaazulchico.png'");
            //ImgCanalMemor.Attributes.Add("onMouseOver", "this.src = '/Pages/ImgBooom/botonminoristaazul.png'");
            //ImgCanalMemor.Attributes.Add("onMouseOut", "this.src = '/Pages/ImgBooom/botonminoristaazulchico.png'");
            //ImgCanalAASS.Attributes.Add("onMouseOver", "this.src = '/Pages/ImgBooom/botonaassazul.png'");
            //ImgCanalAASS.Attributes.Add("onMouseOut", "this.src = '/Pages/ImgBooom/botonaassazulchico.png'");
        }


        private void llenarsubcanles()
        {
            DataSet ds;
            int company;
            string canal;
            canal = this.Session["Canal"].ToString().Trim();
            company = Convert.ToInt32(this.Session["companyid"]);

            ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_OBTENERSUBCANALES", canal, company);
            MenuScanal.Items.Clear();

            if (ds.Tables[0].Rows.Count>0)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    divscanal.Style.Value = " width:500px;  max-height: 200px; overflow: auto;display:block;background-color:Transparent;";
                    MenuItem item = new MenuItem();                   
                    item.Text = ds.Tables[0].Rows[i][1].ToString();
                    item.Value = ds.Tables[0].Rows[i][0].ToString();
                    MenuScanal.Items.Add(item);
                }
            }
            else
            {
               ds = null;
            }
        }

        private void llenarsubNivel()
        {
            DataSet ds;           
            string Subcanal;
            Subcanal = this.Session["sucnal"].ToString().Trim();
            //company = Convert.ToInt32(this.Session["companyid"]);

            ds = oCoon.ejecutarDataSet("UP_WEBXPLORA_OBTENERSUBNIVELES", Subcanal);
            MenSNivel.Items.Clear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    divscanal.Style.Value = " width:500px;  max-height: 200px; overflow: auto;display:block;background-color:Transparent;";
                    MenuItem item = new MenuItem();
                    item.Text = ds.Tables[0].Rows[i][1].ToString();
                    item.Value = ds.Tables[0].Rows[i][0].ToString();
                    MenSNivel.Items.Add(item);
                }
            }
            else
            {
                ds = null;
            }
        }

        public void BotonPOP()
        {
            int iperonid;
            iperonid = Convert.ToInt32(this.Session["personid"]);
            DataTable dt = null;
            dt = DataCliente.Get_obtnerBotonBlioteca(iperonid, Convert.ToInt32(this.Session["companyid"]));
            
            if (dt.Rows.Count > 0)
            {
                Biblioteca.DataSource = dt;
                Biblioteca.DataBind();


            }
            else {

                dt = null;
                Biblioteca.Visible = false;
              }

           
        }
        public void ValidaBotonInfGerencial()
        {
            int iperonid;
            iperonid = Convert.ToInt32(this.Session["personid"]);
            DataTable dt = null;
            dt = DataCliente.Get_obtnerBotonINfGerencial(iperonid, Convert.ToInt32(this.Session["companyid"]));

            if (dt.Rows.Count > 0)
            {
                ImgBtnInfGerencial.Visible = true;
            }
            else
            {
                ImgBtnInfGerencial.Visible = false;                
            }
            dt = null;

        }
        public void ValidaBotonResumenEjecutivo()
        {
            int iperonid;
            iperonid = Convert.ToInt32(this.Session["personid"]);
            DataTable dt = null;
            dt = DataCliente.Get_obtnerBotonResumen_Ejecutivo(iperonid, Convert.ToInt32(this.Session["companyid"]));

            if (dt.Rows.Count > 0)
            {
                ImgBtnResumen_Ejecutivo.Visible = true;
            }
            else
            {
                ImgBtnResumen_Ejecutivo.Visible = false;
            }
            dt = null;

        }
        public void ValidaBotonActivaciones()
        {
            int iperonid;
            int company;
            company=Convert.ToInt32(this.Session["companyid"]);
            iperonid = Convert.ToInt32(this.Session["personid"]);
            DataTable dt = null;
            dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENERBOTACTIVACIONES", iperonid,company );

            if (company == 1572) {

                ImgBtnInfGerencial.Visible = false;
                Biblioteca.Visible = false;
            
            
            }
           

            if (dt.Rows.Count > 0)
            {
                btnActivapromo.Visible = true;
               
                
            }
            else
            {
                btnActivapromo.Visible = false;
            }
            dt = null;

        }


        protected void ImgCloseSession_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Get_Administrativo.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                this.Session.Abandon();
                Response.Redirect("~/login.aspx", true);
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/login.aspx", true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string sUser = this.Session["sUser"].ToString();
                    sNameUser = this.Session["nameuser"].ToString();
                    url_foto = this.Session["fotocomany"].ToString();
                    Imgcliente.ImageUrl =url_foto;
                    usersession.Text = sUser;
                    lblUsuario.Text = sNameUser;
                    lblcompany.Text = this.Session["sNombre"].ToString();
                    string sPassw = this.Session["sPassw"].ToString();
                    this.Session["reportbook"] = "0";
                    if (sUser != null && sPassw != null)
                    {
                        llenacanales();
                        BotonPOP();
                        ValidaBotonInfGerencial();
                        ValidaBotonResumenEjecutivo();
                        ValidaBotonActivaciones();
                         DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_MODULOSALTERNOS",this.Session["idnivel"].ToString().Trim());

                         if (dt != null)
                         {
                             if (dt.Rows.Count > 0)
                             {
                                 SelModulo.Visible = true;
                                 cmbselModulos.Visible = true;
                                 SelCliente.Visible = false;
                                 cmbcliente.Visible = false;
                                 GO.Visible = true;
                                 cmbcliente.Enabled = false;
                                 cmbselModulos.DataSource = dt;
                                 cmbselModulos.DataTextField = "Modulo_Name";
                                 cmbselModulos.DataValueField = "Modulo_id";
                                 cmbselModulos.DataBind();
                                 cmbselModulos.Items.Insert(0, new ListItem("--Seleccione--", "0"));
                                 if (cmbselModulos.Items.Count == 2)
                                 {
                                     if (cmbselModulos.Items[1].Value == "CLIE")
                                     {
                                         cmbselModulos.Text = "CLIE";
                                         SelModulo.Visible = false;
                                         cmbselModulos.Visible = false;
                                         SelCliente.Visible = true;
                                         cmbcliente.Visible = true;
                                         cmbcliente.Enabled = true;

                                         DataTable dtCliente = oCoon.ejecutarDataTable("UP_WEBXPLORA_GEN_CONSULTACLIENTES", Convert.ToInt32(this.Session["personid"].ToString().Trim()));
                                         cmbcliente.DataSource = dtCliente;
                                         cmbcliente.DataTextField = "Company_Name";
                                         cmbcliente.DataValueField = "Company_id";
                                         cmbcliente.DataBind();
                                         
                                         if (dtCliente.Rows.Count == 1)
                                         {
                                             cmbcliente.Visible = false;
                                             SelCliente.Visible = false;
                                             ScriptManager.RegisterStartupScript(
                                             this, this.GetType(), "myscript", "alert('No tiene permisos para visualizar información de ningun Cliente. Por favor solicite al Administrador los permisos necesarios.');", true);
                                         }
                                         else
                                         {                                         
                                             if (dtCliente.Rows.Count == 2)
                                             {
                                                 cmbcliente.SelectedIndex = 1;
                                             }
                                             else
                                             {
                                                 cmbcliente.Text = "0";
                                                 // se comentarea para mostrar el cliente propio del usuario. 28/01/2011 Magaly Jiménez. 
                                                 //cmbcliente.Items.Remove(cmbcliente.Items.FindByValue(this.Session["companyid"].ToString().Trim()));
                                                 cmbcliente.Enabled = true;
                                                 cmbcliente.Visible = true;
                                                 SelCliente.Visible = true;
                                             }
                                         }
                                     }
                                 }

                                 DataTable dtClientes = oCoon.ejecutarDataTable("UP_WEBXPLORA_GEN_CONSULTACLIENTES", Convert.ToInt32(this.Session["personid"].ToString().Trim()));
                                 if (dtClientes.Rows.Count == 2)
                                 {
                                     cmbselModulos.Items.Remove(cmbselModulos.Items.FindByValue("CLIE"));
                                 }

                                 if (cmbselModulos.Items.Count == 1)
                                 {
                                     SelModulo.Visible = false;
                                     cmbselModulos.Visible = false;
                                     SelCliente.Visible = false;
                                     cmbcliente.Visible = false;
                                     GO.Visible = false;
                                     cmbcliente.Enabled = false;
                                 }
                             }
                             else
                             {
                                 SelModulo.Visible = false;
                                 cmbselModulos.Visible = false;
                                 SelCliente.Visible = false;
                                 cmbcliente.Visible = false;
                                 GO.Visible = false;
                                 cmbselModulos.Items.Clear();
                                 cmbcliente.Items.Clear();
                             }
                         }
                    }
                }
                catch (Exception ex)
                {
                    Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                    this.Session.Abandon();
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
        }

        protected void ImgCanalMayor_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Cliente/Mod_Cliente.aspx", true);
        }

        protected void MenuScanal_MenuItemClick(object sender, MenuEventArgs e)
        {

            lbmarcas.Style.Value = "display:block;";
            Label lbNameSubCanal = new Label();
            lbNameSubCanal.Text = MenuScanal.SelectedItem.Text;
            this.Session["NameSubCanal"] = lbNameSubCanal.Text;

            lblSubnivel.Text = this.Session["NamesubCanal"].ToString().Trim();

            this.Session["sucnal"] = MenuScanal.SelectedValue;
            llenarsubNivel();

            if (MenSNivel.Items.Count == 0)
            {
                lbmarcas.Style.Value = "display:none;";
                this.Session["sucnal"] = MenuScanal.SelectedValue;             
                Response.Redirect("../Cliente/Mod_Cliente.aspx", true);              

            }
       
            
        
        }

        protected void MenSNivel_MenuItemClick(object sender, MenuEventArgs e)
        {
            this.Session["subnivel"] = MenSNivel.SelectedValue;

            Response.Redirect("../Cliente/Mod_Cliente.aspx", true);
        
        }
        

        protected void ListViewCanales_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbmarcas.Style.Value = "display:none;";
            int company;
            company = Convert.ToInt32(this.Session["companyid"]);
            int item = Convert.ToInt32(this.Session["CanalSeleccionado"]);

            Label lbNameCanal = new Label();
            lbNameCanal = ((Label)ListViewCanales.Items[item].FindControl("NameCanal"));
            this.Session["NameCanal"] = lbNameCanal.Text;

            lblscanal.Text = this.Session["NameCanal"].ToString().Trim();


            DataTable dtcnalna = null;

            bool Estado;
            Label lbCodCanal = new Label();
            lbCodCanal = ((Label)ListViewCanales.Items[item].FindControl("CodCanal"));
         

            
            this.Session["Canal"] = lbCodCanal.Text;

            
                llenarsubcanles();
                MenSNivel.Items.Clear();
            

           if( MenuScanal.Items.Count == 0)
            {              
                dtcnalna = DataCliente.Get_ObtenercanalNoactivoxCliente(this.Session["Canal"].ToString(), Convert.ToInt32(this.Session["companyid"]));
                if (dtcnalna.Rows.Count > 0)
                {
                    Estado = Convert.ToBoolean(dtcnalna.Rows[0]["Channel_Status"]);

                    if (Estado == false)
                    {

                        ScriptManager.RegisterStartupScript(
                                  this, this.GetType(), "myscript", "alert('Canal No Disponible');", true);
                        return;
                    }
                }
                Response.Redirect("../Cliente/Mod_Cliente.aspx", true);
            }            
        }
                       
        protected void ListViewCanales_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)ListViewCanales.Items[e.NewSelectedIndex];
            this.Session["CanalSeleccionado"] = e.NewSelectedIndex;           
        }

        protected void ListViewCanales_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            // Clear selection. 
            ListViewCanales.SelectedIndex = -1;
        }

        protected void Biblioteca_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)Biblioteca.Items[e.NewSelectedIndex];
            this.Session["Biblioteca"] = e.NewSelectedIndex;
        }

        protected void Biblioteca_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            // Clear selection. 
            Biblioteca.SelectedIndex = -1;
        }

        protected void Biblioteca_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int item = Convert.ToInt32(this.Session["Biblioteca"]);
            //Label lbCodREporte = new Label();
            //lbCodREporte = ((Label)Biblioteca.Items[item].FindControl("CodReporte"));
            //this.Session["CodReporte"] = lbCodREporte.Text;

            //Label lbNameReporte = new Label();
            //lbNameReporte = ((Label)Biblioteca.Items[item].FindControl("NameReporte"));
            //this.Session["NameReporte"] = lbNameReporte.Text;
            this.Session["Canal"] = "0";
            int company;
            company = Convert.ToInt32(this.Session["companyid"]);
            if (company == 1562)
            {
                Response.Redirect("../Cliente/Mod_Cliente_Biblioteca.aspx", true);
            }

            if (company == 1561) {
                Response.Redirect("../Cliente/Mod_Cliente_Biblioteca_Tipo.aspx", true);
            
            
            
            }
        }

        protected void cmbselModulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbselModulos.SelectedItem.Value == "0")
            {
                cmbcliente.Enabled = false;
                SelCliente.Visible = false;
                cmbcliente.Visible = false;
            }
            else
            {
                DataTable dtClientes = oCoon.ejecutarDataTable("UP_WEBXPLORA_GEN_CONSULTACLIENTES", Convert.ToInt32(this.Session["personid"].ToString().Trim()));
                cmbcliente.DataSource = dtClientes;
                cmbcliente.DataTextField = "Company_Name";
                cmbcliente.DataValueField = "Company_id";
                cmbcliente.DataBind();
                cmbcliente.Items.Insert(cmbcliente.Items.Count, new ListItem("Lucky SAC", "1478"));

                if (dtClientes.Rows.Count == 1)
                {
                    cmbcliente.Visible = false;
                    SelCliente.Visible = false;
                    ScriptManager.RegisterStartupScript(
                    this, this.GetType(), "myscript", "alert('No tiene permisos para visualizar información de ningun Cliente. Por favor solicite al Administrador los permisos necesarios.');", true);
                }
                else
                {
                    if (cmbselModulos.SelectedItem.Value == "AD" || cmbselModulos.SelectedItem.Value == "PLA")
                    {
                        cmbcliente.Text = "1478";
                        cmbcliente.Visible = false;
                        SelCliente.Visible = false;
                    }
                    else
                    {
                        cmbcliente.Items.Remove(cmbcliente.Items.FindByValue("1478"));
                        if (dtClientes.Rows.Count == 2)
                        {
                            cmbcliente.SelectedIndex = 1;
                        }
                        else
                        {
                            cmbcliente.Text = "0";
                            //se comentarea para permitir ver cliente propio del usuario. 28/01/2011 Magaly jiménez.
                            //cmbcliente.Items.Remove(cmbcliente.Items.FindByValue(this.Session["companyid"].ToString().Trim()));
                            cmbcliente.Enabled = true;
                            cmbcliente.Visible = true;
                            SelCliente.Visible = true;
                        }
                    }
                }
            }
        }

        protected void GO_Click(object sender, EventArgs e)
        {
            
                if (cmbselModulos.Text != "0" && cmbcliente.Text != "0")
                {
                    AplicacionWeb oAplicacionWeb = new AplicacionWeb();                   
                    
                    EAplicacionWeb oeAplicacionWeb = oAplicacionWeb.obtenerAplicacion(this.Session["scountry"].ToString().Trim(), cmbselModulos.SelectedItem.Value);
                    
                    this.Session["oeAplicacionWeb"] = oeAplicacionWeb;
                    this.Session["cod_applucky"] = oeAplicacionWeb.codapplucky;
                    this.Session["abr_app"] = oeAplicacionWeb.abrapp;
                    this.Session["app_url"] = oeAplicacionWeb.appurl;
                    this.Session["companyid"] = cmbcliente.SelectedItem.Value;
                    DataTable dturllogo = oCoon.ejecutarDataTable("UP_WEBXPLORA_GEN_LOGOCLIENT", Convert.ToInt32(cmbcliente.Text));

                    this.Session["fotocomany"] = dturllogo.Rows[0][0].ToString().Trim();
                    this.Session["sNombre"] = cmbcliente.SelectedItem.Text;
                    string sPagina = oeAplicacionWeb.HomePage;
                    oeAplicacionWeb = null;
                    oAplicacionWeb = null;
                    this.Response.Redirect("~/" + sPagina, true);
                }
            
           
        }

        protected void ImgBtnInfGerencial_Click(object sender, EventArgs e)
        {
            this.Session["Canal"] = "0";
            int company;
            company = Convert.ToInt32(this.Session["companyid"]);
            if (company == 1562)
            {
                this.Session["reportbook"] = ConfigurationManager.AppSettings["Informacion_Gerencial"];
                Response.Redirect("../Cliente/Mod_Cliente.aspx", true);
            }

          





            ///this.Response.Redirect("~/" + "/Pages/Modulos/Cliente/GaleryofBooksOnline/index.html" , true);
        }                

        //protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        //{ 
        //    string prueba = ListView1.SelectedItemTemplate.ToString();
        //    Response.Redirect("../Cliente/Mod_Cliente.aspx", true);           
        //}

        //protected void ListView1_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        //{
        //    string id = ListView1.DataKeys[e.NewSelectedIndex].Value.ToString();
        //    string mensaje = "ListView Selected ID : " + id;

        //}

      

        //protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        //{
          
          
        //}


    
    }
}
