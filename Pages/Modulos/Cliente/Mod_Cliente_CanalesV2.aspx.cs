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

namespace SIGE.Pages.Modulos.Cliente
{
    public partial class Mod_Cliente_CanalesV2 : System.Web.UI.Page
    {
        Conexion oCoon = new Conexion();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente DataCliente = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();
        string sNameUser;
        string namecomapny;
        string url_foto;

        public void llenacanales()
        {
            try
            {
                DataTable dtcanales = null;

                dtcanales = DataCliente.Get_ObtenerCanalesxCliente(Convert.ToInt32(this.Session["companyid"]));

                if (dtcanales.Rows.Count > 0)
                {


                    ListViewCanales.DataSource = dtcanales;
                    ListViewCanales.DataBind();
                }
                else
                {
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
                //Response.Redirect("~/err_mensaje_seccion.aspx", true);
            
            
            
            }

            

            //ImgCanalMayor.Attributes.Add("onMouseOver", "this.src = '/Pages/ImgBooom/botonmayoristaazul.png'");
            //ImgCanalMayor.Attributes.Add("onMouseOut", "this.src = '/Pages/ImgBooom/botonmayoristaazulchico.png'");
            //ImgCanalMemor.Attributes.Add("onMouseOver", "this.src = '/Pages/ImgBooom/botonminoristaazul.png'");
            //ImgCanalMemor.Attributes.Add("onMouseOut", "this.src = '/Pages/ImgBooom/botonminoristaazulchico.png'");
            //ImgCanalAASS.Attributes.Add("onMouseOver", "this.src = '/Pages/ImgBooom/botonaassazul.png'");
            //ImgCanalAASS.Attributes.Add("onMouseOut", "this.src = '/Pages/ImgBooom/botonaassazulchico.png'");
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
            else
            {

                dt = null;
                Biblioteca.Visible = false;
            }


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
                    if (sUser != null && sPassw != null)
                    {
                        llenacanales();
                        BotonPOP();
                    }
                }
                catch (Exception ex)
                {
                    Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                    this.Session.Abandon();
                    //Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
        }

        protected void ImgCanalMayor_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Cliente/Mod_ClienteV2.aspx", true);
        }

        protected void ListViewCanales_SelectedIndexChanged(object sender, EventArgs e)
        {
            int item = Convert.ToInt32(this.Session["CanalSeleccionado"]);

            DataTable dtcnalna = null;

            bool Estado;
                Label lbCodCanal = new Label();
                lbCodCanal = ((Label)ListViewCanales.Items[item].FindControl("CodCanal"));
                this.Session["Canal"] = lbCodCanal.Text;
                dtcnalna = DataCliente.Get_ObtenercanalNoactivoxCliente(this.Session["Canal"].ToString(), Convert.ToInt32(this.Session["companyid"]));
                if (dtcnalna.Rows.Count > 0) {

                    Estado = Convert.ToBoolean(dtcnalna.Rows[0]["Channel_Status"]);

                    if (Estado == false) {

                        ScriptManager.RegisterStartupScript(
                                  this, this.GetType(), "myscript", "alert('Canal No Disponible');", true);


                        return;
                    
                    
                    
                    }
                
                
                
                
                }

                


                Label lbNameCanal = new Label();
                lbNameCanal = ((Label)ListViewCanales.Items[item].FindControl("NameCanal"));
                this.Session["NameCanal"] = lbNameCanal.Text;


                Response.Redirect("../Cliente/Mod_ClienteV2.aspx", true);
            
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
