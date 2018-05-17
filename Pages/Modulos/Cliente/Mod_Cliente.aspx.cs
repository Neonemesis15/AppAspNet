using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
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
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;
using Lucky.CFG.Tools;
using System.Configuration;

namespace SIGE.Pages.Modulos.Cliente
{
    public partial class Mod_Cliente : System.Web.UI.Page
    {

        Conexion oCoon = new Conexion();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();

        string sUser;
        string sPassw;
        string sNameUser;
        string url_foto;


        


        private bool Validar_fechas()
        {
            DateTime Fechaini, Fechafin;
            try
            {
                Fechaini = DateTime.Parse(txtfecha.Text);
                Fechafin = DateTime.Parse(txtfechafin.Text);
                if (Fechaini >= Fechafin)
                {

                    this.Session["encabemensa"] = "Señor Usuario";
                    this.Session["cssclass"] = "MensajesSupervisor";


                    //this.Session["mensaje"] = this.Session["mensaje"] + " " + "La Fecha de Inicio de Ejecucion no puede ser mayor y/o Igual que la de Finalización";
                    this.Session["mensaje"] = "La fecha de inicio de ejecución no puede ser mayor y/o igual que la de finalización";
                    Mensajes_Usuario();
                    return false;
                }
                return true;
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Es necesario que ingrese todas las fechas solicitadas";
                Mensajes_Usuario();
                return false;
            }
        }

        public void ValidaBotonActivaciones()
        {
            int iperonid;
            int company;
            company = Convert.ToInt32(this.Session["companyid"]);
            iperonid = Convert.ToInt32(this.Session["personid"]);
            DataTable dt = null;
            dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENERBOTACTIVACIONES", iperonid, company);

            if (company == 1572)
            {

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

        public void llenacanales()
        {
            try
            {
                DataTable dtcanales = null;

                dtcanales = Get_DataClientes.Get_ObtenerCanalesxCliente(Convert.ToInt32(this.Session["companyid"]));

                if (dtcanales.Rows.Count > 0)
                {
                    ListViewCanales.DataSource = dtcanales;
                    ListViewCanales.DataBind();
                }
                else
                {
                    dtcanales = null;
                    //ScriptManager.RegisterStartupScript(
                    //                                   this, this.GetType(), "myscript", "alert('Su Perfil no tiene Permisos para Visualizar reportes de este Canal');", true);
                    //return;
                    this.Session["encabemensa"] = "Envío de Solicitudes";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Su Perfil no tiene Permisos para Visualizar reportes de este Canal";
                    Mensajes_Usuario();
                    return; 
                }
            }
            catch (Exception ex)
            {
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

        public void llenareportes()
        {
            try
            {
                


                int company;

               

                string reporte;
                reporte = this.Session["reportbook"].ToString().Trim();
              
               
                company = Convert.ToInt32(this.Session["companyid"]);

              

                if (reporte == "52")
                {

                    DataTable dtb = null;
                    dtb = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_BOOKS_CLIENTE", this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["companyid"]), "52");

                    if (dtb.Rows.Count > 0)
                    {

                        if (dtb.Rows.Count < 2)
                        {
                            btnnext.Visible = false; //Se agrego al aspx un id a los botones del carrusel Ing. Carlos Hernandez 09/11/2010
                            btnprev.Visible = false;
                        }
                        else
                        {
                            btnnext.Visible = true; //Se agrego al aspx un id a los botones del carrusel Ing. Carlos Hernandez 09/11/2010
                            btnprev.Visible = true;
                        }

                        // se quita el if ya que hace exactamente lo mismo por los dos caminos. 16/11/2010 Ing. Mauricio Ortiz
                        //if (scity == "")
                        //{
                        ListViewReportes.DataSource = dtb;
                        ListViewReportes.DataBind();

                        this.Session["reportbook"] = "0";
                        //}
                        //else
                        //{
                        //    ListViewReportes.DataSource = dtReportes;
                        //    ListViewReportes.DataBind();

                        //}



                    }





                }

                else
                {

                    if (MenSNivel.Items.Count > 0)
                    {
                        string subcanal;
                        subcanal = this.Session["sucnal"].ToString().Trim();
                        DataTable dtsf;
                        dtsf = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_REPORTESXCANAL_SANFERNANDO", Convert.ToInt32(this.Session["personid"]), this.Session["Canal"].ToString().Trim(), subcanal, company, MenSNivel.SelectedValue);

                        if (dtsf.Rows.Count > 0)
                        {
                            if (dtsf.Rows.Count < 2)
                            {
                                btnnext.Visible = false; //Se agrego al aspx un id a los botones del carrusel Ing. Carlos Hernandez 09/11/2010
                                btnprev.Visible = false;
                            }
                            else
                            {
                                btnnext.Visible = true;
                                btnprev.Visible = true;
                            }

                            ListViewReportes.DataSource = dtsf;
                            ListViewReportes.DataBind();
                        }
                        else
                        {
                            dtsf = null;
                            ListViewReportes.DataBind();
                            btnnext.Visible = false;
                            btnprev.Visible = false;
                            this.Session["encabemensa"] = "Envío de Solicitudes";
                            this.Session["cssclass"] = "MensajesSupervisor";
                            this.Session["mensaje"] = "Señor Usuario: Su Perfil no tiene Permisos para visualizar Reportes en este Canal y subcanal seleccionado";
                            //Mensajes_Usuario();
                            return;
                        }
                    }
                    else
                    {

                        string scity = this.Session["scity"].ToString().Trim();
                        DataTable dtReportes = Get_DataClientes.Get_Obtener_reporteXCliente_Canal(Convert.ToInt32(this.Session["personid"]), this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["companyid"]));

                        if (dtReportes.Rows.Count > 0)
                        {


                            if (dtReportes.Rows.Count < 2)
                            {
                                btnnext.Visible = false; //Se agrego al aspx un id a los botones del carrusel Ing. Carlos Hernandez 09/11/2010
                                btnprev.Visible = false;
                            }
                            else
                            {
                                btnnext.Visible = true; //Se agrego al aspx un id a los botones del carrusel Ing. Carlos Hernandez 09/11/2010
                                btnprev.Visible = true;
                            }

                            // se quita el if ya que hace exactamente lo mismo por los dos caminos. 16/11/2010 Ing. Mauricio Ortiz
                            //if (scity == "")
                            //{
                            ListViewReportes.DataSource = dtReportes;
                            ListViewReportes.DataBind();
                            //}
                            //else
                            //{
                            //    ListViewReportes.DataSource = dtReportes;
                            //    ListViewReportes.DataBind();

                            //}



                        }


                        //Se vuelve a Colocar esta validación porque se elimino en el ultimo ajuste Ing. Carlos Hernandez 09/11/2010
                        else
                        {

                            ListViewReportes.DataBind();//Se agrega limpiado del control cuando no hay datos y mensaje Ing. Carlos Hernandez 
                            btnnext.Visible = false;
                            btnprev.Visible = false;
                            ScriptManager.RegisterStartupScript(
                                            this, this.GetType(), "myscript", "alert('Señor Usuario: Su Perfil no tiene Permisos para visualizar Reportes en este Canal');", true);

                        }
                    }
                }

        
                
                
                
                
                
                
               }
            
            catch (Exception ex)
            {
                Exception mensaje = ex;

                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

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

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    lblscanal.Text = this.Session["NameCanal"].ToString().Trim();
                    divscanal.Style.Value = " width:188px;  max-height: 200px; overflow: auto;display:block;background-color:Transparent;";
                    divscanal.Visible = true;
                    MenuItem item = new MenuItem();
                    //string s = "<SCRIPT language=\"javascript\">" +
                    //"alert ('\"MenuEmpresarial.SelectedValue\"'');</SCRIPT>";
                    //RegisterStartupScript("mensaje", s);
                    item.Text = ds.Tables[0].Rows[i][1].ToString();
                    item.Value = ds.Tables[0].Rows[i][0].ToString();
                    MenuScanal.Items.Add(item);
                   



                    //// Create the submenu items.
                    //MenuItem SubMenuItemPOP;
                    //DataSet dsmt = null;
                    //dsmt = Get_DataClientes.Get_ObtenerPOPxMarca(this.Session["categoria"].ToString().Trim(), Convert.ToInt32(MenuMarcas.Items[i].Value));
                    //for (int j = 0; j <= dsmt.Tables[0].Rows.Count - 1; j++)
                    //{
                    //    SubMenuItemPOP = CreateMenuItem(dsmt.Tables[0].Rows[j][1].ToString(), dsmt.Tables[0].Rows[j][0].ToString(), "prueba");
                    //    SubMenuItemPOP.ChildItems.Add(SubMenuItemPOP);
                    //    //  MenuMarcas.Items[0].ChildItems.Clear();
                    //    //  MenuMarcas.Items[0].Selected = true;
                    //    // MenuMarcas.ItemWrap.ToString();
                    //    MenuMarcas.Items[i].ChildItems.Add(SubMenuItemPOP);
                    //    //MenuMarcas.Items.Add(musicSubMenuItem);
                    //    //MenuMarcas.DataBind();
                    //}
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
                    lblSubnivel.Text = this.Session["NamesubCanal"].ToString().Trim();
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


        public void ValidaBotonInfGerencial()
        {
            int iperonid;
            iperonid = Convert.ToInt32(this.Session["personid"]);
            DataTable dt = null;
            dt = Get_DataClientes.Get_obtnerBotonINfGerencial(iperonid, Convert.ToInt32(this.Session["companyid"]));

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
            dt = Get_DataClientes.Get_obtnerBotonResumen_Ejecutivo(iperonid, Convert.ToInt32(this.Session["companyid"]));

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
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {
                try
                {
                    sUser = this.Session["sUser"].ToString();
                    sPassw = this.Session["sPassw"].ToString();
                    sNameUser = this.Session["nameuser"].ToString();
                    TxtSolicitante.Text = this.Session["smail"].ToString().Trim();
                    url_foto = this.Session["fotocomany"].ToString();
                    Imgcliente.ImageUrl = url_foto;
                    TxtEmail.Text = "mortiz.col@lucky.com.pe";
                    //TablaInformes.Style.Value = "display:none;";
                    PanelCorreos.Style.Value = "display:none;";
                    PanelInfo.Style.Value = "display:none;";
                    usersession.Text = sUser;
                    lblUsuario.Text = sNameUser;
                    lblcompany.Text = this.Session["sNombre"].ToString();
                    if (sUser != null && sPassw != null)
                    {
                        BotonPOP();
                        ValidaBotonInfGerencial();
                        ValidaBotonResumenEjecutivo();
                        ValidaBotonActivaciones();
                        llenacanales();
                        for (int i = 0; i <= ListViewCanales.Items.Count - 1; i++)
                        {
                            Label lbCodCanal = new Label();
                            lbCodCanal = ((Label)ListViewCanales.Items[i].FindControl("CodCanal"));
                            if (this.Session["Canal"].ToString().Trim() == lbCodCanal.Text)
                            {
                                string NameCanal = this.Session["NameCanal"].ToString().Trim();

                                ((LinkButton)ListViewCanales.Items[i].FindControl("SelectCanalesButton")).CssClass = "p" + NameCanal + "Seleccionado";
                                ((LinkButton)ListViewCanales.Items[i].FindControl("SelectCanalesButton")).Enabled = false;

                            }
                        } 

                        llenarsubcanles();
                       
                                       
                        
                        for (int i= 0 ; i <= MenuScanal.Items.Count -1 ; i++)
                        {
                            if (MenuScanal.Items[i].Value == this.Session["sucnal"].ToString().Trim())
                            {
                                MenuScanal.Items[i].Selected = true;
                                llenarsubNivel();
                                if (MenSNivel.Items.Count > 0)
                                {
                                    lbmarcas.Style.Value = "display:block;";
                                }
                                else 
                                {
                                    lbmarcas.Style.Value = "display:none;";
                                }

                                for (int j = 0; j <= MenSNivel.Items.Count - 1; j++)
                                {
                                    if (MenSNivel.Items[j].Value == this.Session["subnivel"].ToString().Trim())
                                    {
                                        MenSNivel.Items[j].Selected = true;
                                       
                                    }
                                }
                                
                               
                            }
                        }
                        llenareportes();
                                       
                    }
                }
                catch (Exception ex)
                {
                    Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                    Exception mensaje = ex;
                    this.Session.Abandon();
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
            iframeExcel.Attributes["src"] = "";
        }

      


        public void BotonPOP()
        {
            int iperonid;
            iperonid = Convert.ToInt32(this.Session["personid"]);
            DataTable dt = null;
            dt = Get_DataClientes.Get_obtnerBotonBlioteca(iperonid, Convert.ToInt32(this.Session["companyid"]));

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

        protected void MenuScanal_MenuItemClick(object sender, MenuEventArgs e)
        {
            lbmarcas.Style.Value = "display:block;";
            ListViewReportes.DataBind();
            btnnext.Visible = false;
            btnprev.Visible = false;

            Label lbNameSubCanal = new Label();
            lbNameSubCanal.Text = MenuScanal.SelectedItem.Text;
            this.Session["NameSubCanal"] = lbNameSubCanal.Text;

            lblSubnivel.Text = this.Session["NamesubCanal"].ToString().Trim();

            this.Session["sucnal"] = MenuScanal.SelectedValue;
            llenarsubNivel();

            if (MenSNivel.Items.Count == 0)
            {
                this.Session["sucnal"] = MenuScanal.SelectedValue;
                lbmarcas.Style.Value = "display:none;";
                //Response.Redirect("../Cliente/Mod_Cliente.aspx", true);
                int company;
                company = Convert.ToInt32(this.Session["companyid"]);
                int item = Convert.ToInt32(this.Session["CanalSeleccionado"]);
                if (MenuScanal.Items.Count > 0)
                {
                    divscanal.Visible = true;
                    DataTable dtsf;
                    dtsf = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_REPORTESXCANAL_CLIENTE", Convert.ToInt32(this.Session["personid"]), this.Session["Canal"].ToString().Trim(), MenuScanal.SelectedValue, company);

                    if (dtsf.Rows.Count > 0)
                    {
                        if (dtsf.Rows.Count < 4)
                        {
                            btnnext.Visible = false; //Se agrego al aspx un id a los botones del carrusel Ing. Carlos Hernandez 09/11/2010
                            btnprev.Visible = false;
                        }
                        else
                        {
                            btnnext.Visible = true; //Se agrego al aspx un id a los botones del carrusel Ing. Carlos Hernandez 09/11/2010
                            btnprev.Visible = true;
                        }
                        ListViewReportes.DataSource = dtsf;
                        ListViewReportes.DataBind();
                    }
                    else
                    {
                        dtsf = null;
                        ListViewReportes.DataBind();
                        btnnext.Visible = false;
                        btnprev.Visible = false;
                        this.Session["encabemensa"] = "Envío de Solicitudes";
                        this.Session["cssclass"] = "MensajesSupervisor";
                        this.Session["mensaje"] = "Señor Usuario: Su Perfil no tiene Permisos para visualizar Reportes en este Canal y subcanal seleccionado";
                        Mensajes_Usuario();
                        return;
                    }
                }

            }
           
        }




        protected void MenSNivel_MenuItemClick(object sender, MenuEventArgs e)
        {
            PanelInfo.Style.Value = "display:none;";
            gvLink_informes.DataBind();
            gvLink_informes_invisible.DataBind();

            int company;
            company = Convert.ToInt32(this.Session["companyid"]);
            int item = Convert.ToInt32(this.Session["CanalSeleccionado"]);
         
            //Label lbNameCanal = new Label();
            //lbNameCanal = ((Label)ListViewCanales.Items[item].FindControl("NameCanal"));
            //this.Session["NameCanal"] = lbNameCanal.Text;

            lblscanal.Text = this.Session["NameCanal"].ToString().Trim();
            divscanal.Visible = false;



            if (MenuScanal.Items.Count > 0)
            {
                divscanal.Visible = true;
                DataTable dtsf;
                dtsf = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_REPORTESXCANAL_SANFERNANDO", Convert.ToInt32(this.Session["personid"]), this.Session["Canal"].ToString().Trim(), MenuScanal.SelectedValue, company, MenSNivel.SelectedValue);

                if (dtsf.Rows.Count > 0)
                {
                    if (dtsf.Rows.Count < 4)
                    {
                        btnnext.Visible = false; //Se agrego al aspx un id a los botones del carrusel Ing. Carlos Hernandez 09/11/2010
                        btnprev.Visible = false;
                    }
                    else
                    {
                        btnnext.Visible = true; //Se agrego al aspx un id a los botones del carrusel Ing. Carlos Hernandez 09/11/2010
                        btnprev.Visible = true;
                    }
                    ListViewReportes.DataSource = dtsf;
                    ListViewReportes.DataBind();
                }
                else
                {                    
                    dtsf = null;
                    ListViewReportes.DataBind();
                    btnnext.Visible = false;
                    btnprev.Visible = false;
                    this.Session["encabemensa"] = "Envío de Solicitudes";
                    this.Session["cssclass"] = "MensajesSupervisor";
                    this.Session["mensaje"] = "Señor Usuario: Su Perfil no tiene Permisos para visualizar Reportes en este Canal y subcanal seleccionado";
                    Mensajes_Usuario();
                    return;
                }
            }
        }

        protected void ListViewCanales_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)ListViewCanales.Items[e.NewSelectedIndex];
            this.Session["CanalSeleccionado"] = e.NewSelectedIndex;
            this.Session["Items"] = item;

            

        }
        protected void ListViewCanales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lbmarcas.Style.Value = "display:none;";
                MenSNivel.Items.Clear();           
               
                PanelInfo.Style.Value = "display:none;";
                iframeExcel.Attributes["src"] = "";
                int company;
                company = Convert.ToInt32(this.Session["companyid"]);
                int item = Convert.ToInt32(this.Session["CanalSeleccionado"]);

                Label lbNamCanal = new Label();
                lbNamCanal = ((Label)ListViewCanales.Items[item].FindControl("NameCanal"));
                //lblscanal.Text = this.Session["NameCanal"].ToString().Trim(); ---Se deshabilita esto temporalmente habilitarlo cuano se use SF Ing.Carlos Hernandez 20/07/2011
                this.Session["NameCanal"] = lbNamCanal.Text;
                //lblscanal.Text = this.Session["NameCanal"].ToString().Trim();---Se deshabilita esto temporalmente habilitarlo cuano se use SF Ing.Carlos Hernandez 20/07/2011


                Label lbCodCanal = new Label();
                lbCodCanal = ((Label)ListViewCanales.Items[item].FindControl("CodCanal"));
                
                this.Session["Canal"] = lbCodCanal.Text;


            

                for (int i = 0; i <= ListViewCanales.Items.Count - 1; i++)
                {
                    Label lbCodCanalSel = new Label();
                    lbCodCanalSel = ((Label)ListViewCanales.Items[i].FindControl("CodCanal"));
                    if (this.Session["Canal"].ToString().Trim() == lbCodCanalSel.Text)
                    {
                        string NameCanal = this.Session["NameCanal"].ToString().Trim();

                        ((LinkButton)ListViewCanales.Items[i].FindControl("SelectCanalesButton")).CssClass = "p" + NameCanal + "Seleccionado";
                        ((LinkButton)ListViewCanales.Items[i].FindControl("SelectCanalesButton")).Enabled = false;

                    }
                    else
                    {
                        Label lbNameCanalNoSel = new Label();
                        lbNameCanalNoSel = ((Label)ListViewCanales.Items[i].FindControl("NameCanal"));
                        this.Session["NameCanalNosel"] = lbNameCanalNoSel.Text;

                        string NameCanalNoSel = this.Session["NameCanalNosel"].ToString().Trim();

                        ((LinkButton)ListViewCanales.Items[i].FindControl("SelectCanalesButton")).CssClass = "p" + NameCanalNoSel;
                        ((LinkButton)ListViewCanales.Items[i].FindControl("SelectCanalesButton")).Enabled = true;
                    }
                }

                llenarsubcanles();

                if (MenuScanal.Items.Count == 0)
                {
                    ListViewReportes.DataBind();
                    btnnext.Visible = false;
                    btnprev.Visible = false;
                    llenareportes();
                    DataTable dtcnalna = null;
                    dtcnalna = Get_DataClientes.Get_ObtenercanalNoactivoxCliente(this.Session["Canal"].ToString(), Convert.ToInt32(this.Session["companyid"]));

                    bool Estado;
                    Label lbNameCanal = new Label();
                    lbNameCanal = ((Label)ListViewCanales.Items[item].FindControl("NameCanal"));
                    lblscanal.Text = this.Session["NameCanal"].ToString().Trim();
                    this.Session["NameCanal"] = lbNameCanal.Text;
                    lblscanal.Text = this.Session["NameCanal"].ToString().Trim();




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



                    gvLink_informes.DataBind();
                    gvLink_informes_invisible.DataBind();

                  
                }                 
                else
                {
                    ListViewReportes.DataBind();
                    btnnext.Visible = false;
                    btnprev.Visible = false;                   
                }
            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }


        }
        protected void ListViewCanales_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            // Clear selection. 
            ListViewCanales.SelectedIndex = -1;
        }

        protected void ListViewReportes_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)ListViewReportes.Items[e.NewSelectedIndex];
            this.Session["ReporteSeleccionado"] = e.NewSelectedIndex;
            iframeExcel.Attributes["src"] = "";

        }
        protected void ListViewReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                int item = Convert.ToInt32(this.Session["ReporteSeleccionado"]);
                Label lbCodReporte = new Label();
                lbCodReporte = ((Label)ListViewReportes.Items[item].FindControl("CodReporte"));
                this.Session["Reporte"] = lbCodReporte.Text;

                if (Convert.ToInt32(lbCodReporte.Text) == 1 || Convert.ToInt32(lbCodReporte.Text) == 2)
                {

                    DataTable dtb = null;
                    dtb = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_URL_BOOK", Convert.ToInt32(this.Session["Reporte"]));

                    if (dtb.Rows.Count > 0)
                    {

                        PanelInfo.Style.Value = "display:Block;";
                        gvLink_informes.DataSource = dtb;
                        gvLink_informes.DataBind();

                        gvLink_informes_invisible.DataSource = dtb;
                        gvLink_informes_invisible.DataBind();

                        gvLink_informes.SelectedIndex = -1;

                    }
                    else
                    {
                        PanelInfo.Style.Value = "display:none;";
                        //ScriptManager.RegisterStartupScript(
                        //this, this.GetType(), "myscript", "alert('No existe información disponible para este tipo de reporte');", true);


                        this.Session["encabemensa"] = "Señor Usuario";
                        this.Session["cssclass"] = "MensajesSupervisor";

                        this.Session["mensaje"] = "No existe información disponible para este tipo de reporte";
                        Mensajes_Usuario();
                    }


                }
                else
                {
                    //Permite validar Reportes No COntratados por el Cliente Ing.Carlos Hernández
                    dt = Get_DataClientes.Get_ObtenerReportesNoActivos(Convert.ToInt32(lbCodReporte.Text), Convert.ToInt32(this.Session["companyid"]));
                    if (dt.Rows.Count > 0)
                    {
                        bool Estado = Convert.ToBoolean(dt.Rows[0]["ReportCanal_Status"]);
                        if (Estado == false)
                        {
                            ScriptManager.RegisterStartupScript(
                                  this, this.GetType(), "myscript", "alert('Reporte no Contratado');", true);
                        }
                    }
                    // TablaInformes.Style.Value = "display:block;";
                    PanelCorreos.Style.Value = "display:none;";
                    //divscanal.Style.Value = "display:none;";
                    iframeExcel.Attributes["src"] = "";
                    gvLink_informes.DataBind();
                    DataTable dturl = null;
                    int personinfor;
                    personinfor = Convert.ToInt32(this.Session["personid"]);

                    if (MenuScanal.Items.Count == 0)
                    {




                        dturl = Get_DataClientes.Get_ObtenerurlInformes(this.Session["scountry"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["Reporte"]), personinfor);
                        if (dturl != null)
                        {
                            if (dturl.Rows.Count > 0)
                            {
                                PanelInfo.Style.Value = "display:Block;";
                                gvLink_informes.DataSource = dturl;
                                gvLink_informes.DataBind();

                                gvLink_informes_invisible.DataSource = dturl;
                                gvLink_informes_invisible.DataBind();

                                gvLink_informes.SelectedIndex = -1;
                            }
                            else
                            {
                                PanelInfo.Style.Value = "display:none;";
                                //ScriptManager.RegisterStartupScript(
                                //this, this.GetType(), "myscript", "alert('No existe información disponible para este tipo de reporte');", true);


                                this.Session["encabemensa"] = "Señor Usuario";
                                this.Session["cssclass"] = "MensajesSupervisor";

                                this.Session["mensaje"] = "No existe información disponible para este tipo de reporte";
                                Mensajes_Usuario();
                            }
                        }
                    }
                    else
                    {
                        if (MenuScanal.SelectedValue != "")
                        {
                            dturl = Get_DataClientes.Get_ObtenerurlInformesconSubcanal(this.Session["scountry"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), MenuScanal.SelectedValue, Convert.ToInt32(this.Session["Reporte"]), personinfor);
                            if (dturl != null)
                            {
                                if (dturl.Rows.Count > 0)
                                {
                                    PanelInfo.Style.Value = "display:Block;";
                                    gvLink_informes.DataSource = dturl;
                                    gvLink_informes.DataBind();

                                    gvLink_informes_invisible.DataSource = dturl;
                                    gvLink_informes_invisible.DataBind();

                                    gvLink_informes.SelectedIndex = -1;
                                }
                                else
                                {
                                    PanelInfo.Style.Value = "display:none;";
                                    //ScriptManager.RegisterStartupScript(
                                    //this, this.GetType(), "myscript", "alert('No existe información disponible para este tipo de reporte');", true);


                                    this.Session["encabemensa"] = "Señor Usuario";
                                    this.Session["cssclass"] = "MensajesSupervisor";

                                    this.Session["mensaje"] = "No existe información disponible para este tipo de reporte";
                                    Mensajes_Usuario();
                                }
                            }
                        }
                        else
                        {
                            this.Session["encabemensa"] = "Señor Usuario";
                            this.Session["cssclass"] = "MensajesSupervisor";

                            this.Session["mensaje"] = "Es importante que seleccione un subcanal";
                            Mensajes_Usuario();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Exception mensaje = ex;
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

        }
        protected void ListViewReportes_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            // Clear selection. 
            iframeExcel.Attributes["src"] = "";
            ListViewReportes.SelectedIndex = -1;
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
            string prueba = this.Session["Canal"].ToString().Trim();

            int company;
            company = Convert.ToInt32(this.Session["companyid"]);
            if (company == 1562)
            {
                Response.Redirect("../Cliente/Mod_Cliente_Biblioteca.aspx", true);
            }

            if (company == 1561)
            {
                Response.Redirect("../Cliente/Mod_Cliente_Biblioteca_Tipo.aspx", true);



            }
            //Response.Redirect("../Cliente/Mod_Cliente_Biblioteca.aspx", true);
        }


        private void Mensajes_Usuario()
        {
            PCanal.CssClass = this.Session["cssclass"].ToString();
            lblencabezado.Text = this.Session["encabemensa"].ToString();
            lblmensajegeneral.Text = this.Session["mensaje"].ToString();
            ModalPopupCanal.Show();
        }
        //public void llenacanales()
        //{
        //    TablaInformes.Style.Value = "display:none;";

        //    iframeExcel.Attributes["src"] = "";
        //    gvLink_informes.DataBind();
        //    DataTable dtcanales = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENERCNALXSERVICIO", this.Session["Cod_ServicioSel"].ToString().Trim(), Convert.ToInt32(this.Session["companyid"]));

        //    MenuCanales.Items.Clear();
        //    if (dtcanales != null)
        //    {
        //        if (dtcanales.Rows.Count > 0)
        //        {
        //            for (int i = 0; i <= dtcanales.Rows.Count - 1; i++)
        //            {
        //                MenuItem item = new MenuItem();
        //                item.Text = dtcanales.Rows[i]["nombre_canal"].ToString().Trim();
        //                item.Value = dtcanales.Rows[i]["codigo_canal"].ToString().Trim();
        //                MenuCanales.Items.Add(item);
        //            }
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnAnfitrionaje_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnAnfitrionaje.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnBlitz_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnBlitz.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnBtl_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnBtl.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }


        //}

        //protected void BtnCanje_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnCanje.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnDegustacion_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnDegustacion.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }
        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnImpulso_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnImpulso.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }

        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnMercadersimo_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnMercadersimo.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }

        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnSampling_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnSampling.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }

        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void BtnSellSampling_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", BtnSellSampling.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }

        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void NtnVisibilidad_Click(object sender, ImageClickEventArgs e)
        //{
        //    PanelCorreos.Style.Value = "display:none;";
        //    DataTable dt = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_CODSTRATEGY", NtnVisibilidad.ImageUrl, this.Session["scountry"].ToString());
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            this.Session["Cod_ServicioSel"] = dt.Rows[0][0].ToString().Trim();
        //            llenacanales();
        //        }

        //        else
        //        {
        //            this.Session["encabemensa"] = "Señor Usuario";
        //            this.Session["cssclass"] = "MensajesSupervisor";

        //            this.Session["mensaje"] = "No existe información disponible para este servicio";
        //            Mensajes_Usuario();
        //        }
        //    }
        //}

        //protected void MenuCanales_MenuItemClick(object sender, MenuEventArgs e)
        //{
        //    TablaInformes.Style.Value = "display:block;";
        //    PanelCorreos.Style.Value = "display:none;";
        //    iframeExcel.Attributes["src"] = "";
        //    gvLink_informes.DataBind();
        //    DataTable dturl = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_URLINFORMES", this.Session["scountry"].ToString(), Convert.ToInt32(this.Session["companyid"]), Convert.ToInt32(this.Session["Cod_ServicioSel"].ToString().Trim()), MenuCanales.SelectedItem.Value);
        //    gvLink_informes.DataSource = dturl;
        //    gvLink_informes.DataBind();

        //    gvLink_informes_invisible.DataSource = dturl;
        //    gvLink_informes_invisible.DataBind();

        //    gvLink_informes.SelectedIndex = -1;
        //}

        protected void gvLink_informes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string RemoteHost = Request.ServerVariables["REMOTE_ADDR"];

            int reporte;
            reporte = Convert.ToInt32(this.Session["Reporte"]);
            string fn = System.IO.Path.GetFileName(gvLink_informes_invisible.Rows[gvLink_informes.SelectedRow.DataItemIndex].Cells[1].Text.ToString().Trim());
            if (reporte==Convert.ToInt32(ConfigurationManager.AppSettings["Manuales"]))
            {

               


                this.Response.Redirect("~/" + "/Pages/Modulos/Cliente/GaleryofBooksOnline/" + fn, true);
            }

            if (reporte == Convert.ToInt32(ConfigurationManager.AppSettings["Evaluaciones_Indicadores"]))
            {

                string url;
                iframeExcel.Attributes["src"] = "";
            url="Evaluaciones/" + fn;
            iframeExcel.Attributes["src"] = url;
            
            
            
            
            }
            else
            {

                iframeExcel.Attributes["src"] = "";
                iframeExcel.Attributes["src"] = "Informes/" + fn;





            }

           

            Sesion_Users su = new Sesion_Users();

            DataTable dtDuplicado = su.Duplicado_Auditoria_Download(this.Session["sUser"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["Reporte"]), RemoteHost, fn, DateTime.Now.Hour.ToString().Trim() + ":" + DateTime.Now.Minute.ToString().Trim());
            if (dtDuplicado != null)
            
                if (dtDuplicado.Rows.Count == 0)
                {
                    su.Registrar_Auditoria_Download(this.Session["sUser"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["Reporte"]), RemoteHost, fn, DateTime.Now, DateTime.Now.Hour.ToString().Trim() + ":" + DateTime.Now.Minute.ToString().Trim());
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

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //TablaInformes.Style.Value = "display:none;";
            PanelCorreos.Style.Value = "display:block;";
            //MenuCanales.Items.Clear();
            iframeExcel.Attributes["src"] = "";
        }

        protected void ImgCancelMail_Click(object sender, ImageClickEventArgs e)
        {
            PanelCorreos.Style.Value = "display:none;";
        }

        protected void ImgEnviarMail_Click(object sender, ImageClickEventArgs e)
        {
            if (TxtMotivo.Text == "" || TxtMensaje.Text == "")
            {
                this.Session["encabemensa"] = "Envío de Solicitudes";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Sr. Usuario, es necesario que ingrese información en el asunto y en el mensaje";
                Mensajes_Usuario();
                return;
            }
            try
            {
                Enviomail oEnviomail = new Enviomail();
                EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Solicitud_Operativo");
                Mails oMail = new Mails();
                oMail.Server = "mail.lucky.com.pe";
                //oeEmail.MailServer;
                oMail.From = TxtSolicitante.Text;
                oMail.To = TxtEmail.Text;
                oMail.Subject = TxtMotivo.Text;
                oMail.Body = TxtMensaje.Text;
                oMail.CC = "chernandez.col@lucky.com.pe";
                oMail.BodyFormat = "HTML";
                oMail.send();
                oMail = null;
                // oeEmail = null;
                oEnviomail = null;
                TxtMotivo.Text = "";
                TxtMensaje.Text = "";
                PanelCorreos.Style.Value = "display:none;";
                this.Session["encabemensa"] = "Envío de Solicitudes";
                this.Session["cssclass"] = "MensajesSupConfirm";
                this.Session["mensaje"] = "Sr. Usuario, el mensaje fue enviado correctamente";
                Mensajes_Usuario();
            }
            catch (Exception ex)
            {
                this.Session["encabemensa"] = "Envío de Solicitudes";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "Sr. Usuario, se presentó un error inesperado al momento de enviar el correo. Por favor intentelo nuevamente o consulte al Administrador de la aplicación";
                Mensajes_Usuario();
            }
        }  

        /// <summary>
        /// Se ajusta este Metodo agregando el metodo de WS y controlando la no existencia de retorno de Datos y Limoiando la grilla
        /// Modificado por: Ing. Carlos Hernandez
        /// Fecha: 11/11/2010
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLink_informes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                iframeExcel.Attributes["src"] = "";
                gvLink_informes.PageIndex = e.NewPageIndex;
                 DataTable dturl;
                 int personinfor; 
                personinfor = Convert.ToInt32(this.Session["personid"]);
                dturl = Get_DataClientes.Get_ObtenerurlInformes(this.Session["scountry"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["Reporte"]), personinfor);

                if (dturl.Rows.Count > 0)
                {

                    ///oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_URLINFORMES", this.Session["scountry"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), this.Session["Reporte"]);
                    gvLink_informes.DataSource = dturl;
                    gvLink_informes.DataBind();
                }
                else {

                    gvLink_informes.DataBind();
                
                }
            }
            catch
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void btnsearchr_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtsr = null;
            if (txtfecha.Text != "" && txtfechafin.Text != "")
            {
                dtsr = oCoon.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENER_URLINFORMES_TMP", this.Session["scountry"].ToString(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim(), Convert.ToInt32(this.Session["Reporte"]), txtfecha.Text, txtfechafin.Text);
                if (dtsr.Rows.Count > 0)
                {
                    gvLink_informes.DataSource = dtsr;
                    gvLink_informes.DataBind();
                    gvLink_informes_invisible.DataBind();
                    txtfecha.Text = "";
                    txtfechafin.Text = "";



                }
                else
                {

                    gvLink_informes.DataBind();
                    txtfecha.Text = "";
                    txtfechafin.Text = "";


                    Validar_fechas();

                }


            }
            else
            {
                PanelInfo.Style.Value = "display:none;";
                ScriptManager.RegisterStartupScript(
                this, this.GetType(), "myscript", "alert('Debe Seleccionar Fechas para Busqueda');", true);




            }
        }

        protected void txtfecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtfecha.Text != "__/__/____" && txtfecha.Text != "")
                {
                    DateTime t = Convert.ToDateTime(txtfecha.Text);
                }
                Validar_fechas();
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                txtfecha.Text = "";
                Mensajes_Usuario();
            }
        }

        protected void txtfechafin_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (txtfechafin.Text != "__/__/____" && txtfecha.Text != "")
                {
                    DateTime t = Convert.ToDateTime(txtfechafin.Text);
                }
                Validar_fechas();
            }
            catch
            {
                this.Session["encabemensa"] = "Señor Usuario";
                this.Session["cssclass"] = "MensajesSupervisor";
                this.Session["mensaje"] = "La fecha ingresada no tiene un formato valido . Por favor intente nuevamente";
                txtfechafin.Text = "";
                Mensajes_Usuario();
            }

        }

        protected void ImgBtnInfGerencial_Click(object sender, EventArgs e)
        {
            this.Session["reportbook"] = ConfigurationManager.AppSettings["Informacion_Gerencial"];
            llenareportes();
            //this.Response.Redirect("~/" + "/Pages/Modulos/Cliente/GaleryofBooksOnline/index.html", true);

        }

     
    }
}