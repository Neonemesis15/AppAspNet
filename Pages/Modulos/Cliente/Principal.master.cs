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


public partial class Principal : System.Web.UI.MasterPage
{
    #region zona Declaración de Objetos y Variables
    string sUser;
    string sPassw;
    string snamecompany;
    string scountry;
    int companyid;
    int codservice;
    bool bdpo, bcity;
    private Conexion oConn = new Conexion();
    Facade_Proceso_Planning Planning = new Facade_Proceso_Planning();
    Facade_Procesos_Administrativos PdAmon = new Facade_Procesos_Administrativos();

    Facade_Proceso_Cliente Carrusel = new Facade_Proceso_Cliente();
    LuckyWs LuckyWs = new LuckyWs();

    #endregion

    #region Zona de Funciones

    private void Obtener_Servicios_Canales()
    {
        DataTable dtservi = LuckyWs.ObtenerServiciosWS(companyid, scountry);
        this.ViewState["dt"] = dtservi;



        lblUsuario.Text = snamecompany;
        lvCarrusel.DataSource = LuckyWs.ObtenerServiciosWS(companyid,scountry);
        lvCarrusel.DataBind();

        

        for( int i=0; i<=dtservi.Rows.Count-1;i++){

            codservice = Convert.ToInt32(dtservi.Rows[i]["Codigo"]);
        
        
        
        
        }

         





    }

    private void Obtener_ActividadesComercio()
    {
       
        DataSet dsas = null;
        dsas = Carrusel.Get_Obtener_ActividadesComercio_Cliente(220, companyid, "1000");


        CmbActividadPComercio.DataSource = dsas;
        CmbActividadPComercio.DataValueField = "id_cinfo";
        CmbActividadPComercio.DataTextField = "cinfo_Name_Activity";
        CmbActividadPComercio.DataBind();
        CmbActividadPComercio.Items.Insert(0, new ListItem("<Seleccione...>", "0"));



    }
    private void ObtenerPaisesCliente()
    {
   
        DataSet dspais = null;
        dspais = Carrusel.Get_obtenerPaises_Cliente(companyid);
      
        this.Session["Paises"] = dspais;
        CmbPais.DataSource = dspais;
        CmbPais.DataValueField = "codpais";
        CmbPais.DataTextField = "namepais";
        CmbPais.DataBind();
        CmbPais.Items.Insert(0, new ListItem("<Seleccione...>", "0"));





    }
    private void ObtenerDepartamentos()
    {

        DataSet dspaisdto = null;

        dspaisdto = (DataSet)this.Session["Paises"];

        for (int i = 0; i <= dspaisdto.Tables[0].Rows.Count - 1; i++)
        {

            bdpo = Convert.ToBoolean(dspaisdto.Tables[0].Rows[i]["Country_Dpto"]);

            if (bdpo == true)
            {
                DataSet dspto = null;
                dspto = Carrusel.Get_ObtenerDepartamentos_Pais(CmbPais.SelectedValue);
                
                this.Session["dpto"] = dspto;
                CmbDepartamento.DataSource = dspto;
                CmbDepartamento.DataValueField = "cod_dpto";
                CmbDepartamento.DataTextField = "Name_dpto";
                CmbDepartamento.DataBind();
                CmbDepartamento.Items.Insert(0, new ListItem("<Seleccione...>", "0"));



            }
            else
            {
                CmbDepartamento.BackColor = System.Drawing.Color.Gray;
                CmbDepartamento.Enabled = false;



            }




        }



    }
    private void ObtenerCityCliente()
    {
    
         DataSet dsdpto = null;
         DataSet dspais = null;
         dspais = (DataSet)this.Session["Paises"];
         dsdpto = (DataSet)this.Session["dpto"];

        for (int i = 0; i <= dspais.Tables[0].Rows.Count - 1; i++)
        {
            bdpo = Convert.ToBoolean(dspais.Tables[0].Rows[i]["Country_Dpto"]);

            if (bdpo == true)
            {
             DataSet dscity = null;
                dscity=Carrusel.Get_ObtenerCiudadesxPais_Departamento(CmbPais.SelectedValue,CmbDepartamento.SelectedValue);
            
                this.Session["dtcity"] = dscity;
                cmbcity.DataSource = dscity;
                cmbcity.DataValueField = "cod_City";
                cmbcity.DataTextField = "Name_City";
                cmbcity.DataBind();
                cmbcity.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
            }
            else
            {
                DataSet dscity = null;
                dscity = Carrusel.Get_ObtenerCiudadesxPais_Departamento(CmbPais.SelectedValue, "");

           
                cmbcity.DataSource = dscity;
                cmbcity.DataValueField = "cod_City";
                cmbcity.DataTextField = "Name_City";
                cmbcity.DataBind();
                cmbcity.Items.Insert(0, new ListItem("<Seleccione...>", "0"));





            }




        }







    }

    private void ObtenerDistritosCliente()
    {

     DataSet dspais = null;


     dspais = (DataSet)this.Session["Paises"]; ;


        for (int i = 0; i <=dspais.Tables[0].Rows.Count - 1; i++)
        {

            bdpo = Convert.ToBoolean(dspais.Tables[0].Rows[i]["Country_Dpto"]);
            bcity = Convert.ToBoolean(dspais.Tables[0].Rows[i]["Country_Ciudad"]);

            if (bdpo == true && bcity == true)
            {
                DataSet dsdist = null;
                dsdist = Carrusel.Get_ObtenerDistritosCity(CmbPais.SelectedValue, CmbDepartamento.SelectedValue, cmbcity.SelectedValue);

                cmbDistrito.DataSource = dsdist;
                cmbDistrito.DataValueField = "cod_District";
                cmbDistrito.DataTextField = "Name_Local";
                cmbDistrito.DataBind();
                cmbDistrito.Items.Insert(0, new ListItem("<Seleccione...>", "0"));


            }
            else
            {
                if (bdpo == false && bcity == true)
                {
                    DataSet dsdist = null;
                    dsdist = Carrusel.Get_ObtenerDistritosCity(CmbPais.SelectedValue, "", cmbcity.SelectedValue);
                  
                    
                    cmbDistrito.DataSource = dsdist;
                    cmbDistrito.DataValueField = "cod_District";
                    cmbDistrito.DataTextField = "Name_Local";
                    cmbDistrito.DataBind();
                    cmbDistrito.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
                    CmbDepartamento.BackColor = System.Drawing.Color.Gray;
                    CmbDepartamento.Enabled = false;





                }
                else
                {
                    if (bdpo == true && bcity == false)
                    {
                        
                        DataSet dsdist = null;
                        dsdist = Carrusel.Get_ObtenerDistritosCity(CmbPais.SelectedValue, CmbDepartamento.SelectedValue, "");
                        
                        cmbDistrito.DataSource = dsdist;
                        cmbDistrito.DataValueField = "cod_District";
                        cmbDistrito.DataTextField = "Name_Local";
                        cmbDistrito.DataBind();
                        cmbDistrito.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
                        cmbcity.BackColor = System.Drawing.Color.Gray;
                        cmbcity.Enabled = false;




                    }

                    else
                    {

                        if (bdpo == false && bcity == false)
                        {
                            DataSet dsdist = null;
                            dsdist = Carrusel.Get_ObtenerDistritosCity(CmbPais.SelectedValue, "", "");
                            
                            cmbDistrito.DataSource = dsdist;
                            cmbDistrito.DataValueField = "cod_District";
                            cmbDistrito.DataTextField = "Name_Local";
                            cmbDistrito.DataBind();
                            cmbDistrito.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
                            CmbDepartamento.BackColor = System.Drawing.Color.Gray;
                            CmbDepartamento.Enabled = false;
                            cmbcity.BackColor = System.Drawing.Color.Gray;
                            cmbcity.Enabled = false;




                        }




                    }




                }




            }










        }









    }
    private void ObtenerSegmentosCliente()
    {

        DataSet dsseg= null;
        dsseg = Carrusel.Get_OtenerSegmentos();
    
        CmbSegmento.DataSource = dsseg;
        CmbSegmento.DataValueField = "id_Segment";
        CmbSegmento.DataTextField = "Segment_Name";
        CmbSegmento.DataBind();
        CmbSegmento.Items.Insert(0, new ListItem("<Seleccione...>", "0"));





    }

    private void ObtenerPDVCliente()
    {
        DataTable dtpdvcli = null;
        dtpdvcli = oConn.ejecutarDataTable("UP_WEBSIGE_CLIENTE_OBTENERPDVCLIENTE", 220, companyid, Convert.ToInt32(CmbSegmento.SelectedValue));
        cmbpdv.DataSource = dtpdvcli;
        cmbpdv.DataValueField = "id_PointOfsale";
        cmbpdv.DataTextField = "pdv_Name";
        cmbpdv.DataBind();
        cmbpdv.Items.Insert(0, new ListItem("<Seleccione...>", "0"));



    }

    private void Obtener_Menustipo()
    {
   
        DataSet dsment = null;
        dsment = Carrusel.Get_ObtenerInformes_tipo_cliente(220, companyid);
        


       // MenuItem item = new MenuItem();

        

        //MenuEmpresarial.Items.Add(item);
        for (int i = 0; i <= dsment.Tables[0].Rows.Count - 1; i++)
        {
            MenuItem item = new MenuItem();
            item.Text = dsment.Tables[0].Rows[i]["NameReport"].ToString();
            item.Value = dsment.Tables[0].Rows[i]["Report_Id"].ToString();
            

            MenuEmpresarial.Items.Add(item);


        }


        //MenuEmpresarial.DataBind();





    }

    private void Obtener_MenuDinamico()
    {
     
        DataSet dsmendi = null;
        dsmendi = Carrusel.Get_ObtenerInformes_Dinamico_cliente(220, companyid);
        


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

    private void ObtenerAgrupacionComercial() {
        DataSet dsag = null;
        dsag = Carrusel.Get_ObtenerTiposAgrupacionComercial(220, companyid);
        cmbTipoAgrupacion.DataSource = dsag;
        cmbTipoAgrupacion.DataValueField = "idNodeComType";
        cmbTipoAgrupacion.DataTextField = "NodeComType_name";
        cmbTipoAgrupacion.DataBind();
        cmbTipoAgrupacion.Items.Insert(0, new ListItem("<Seleccione...>", "0"));


      
    
    
    
    
    
    }


    private void obtenerAgrupacionComercial() {
        DataSet dsagrupa = null;
        dsagrupa = Carrusel.Get_ObtenerAgrupacionesComerciales(Convert.ToInt32(cmbTipoAgrupacion.SelectedValue));
        CmbAgrupacion.DataSource = dsagrupa;
        CmbAgrupacion.DataValueField = "NodeCommercial";
        CmbAgrupacion.DataTextField = "commercialNodeName";
        CmbAgrupacion.DataBind();
        CmbAgrupacion.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
    
    
    
    
    
    }

    private void Obtenercategoria() {

        DataSet dscatego = null;
        dscatego = Carrusel.Get_ObtenercategoriasCliente_Servicio(companyid, 220);
        CmbCategoria.DataSource = dscatego;
        CmbCategoria.DataValueField = "idcatego";
        CmbCategoria.DataTextField = "namecatego";
        CmbCategoria.DataBind();
        CmbCategoria.Items.Insert(0, new ListItem("<Seleccione...>", "0"));


    
    
    
    }

    private void ObtnerMarcaxCategoria() {
        DataSet dsmarca = null;
        dsmarca = Carrusel.Get_ObtenerMarcasCliente(Convert.ToInt32(CmbCategoria.SelectedValue));
        CmbMarca.DataSource = dsmarca;
        CmbMarca.DataValueField = "id_Brand";
        CmbMarca.DataTextField = "Name_Brand";
        CmbMarca.DataBind();
        CmbMarca.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
    
    
    
    
    }

    private void ObtenerSubmarcaxMarca() {
        DataSet dssbm = null;
        dssbm = Carrusel.Get_ObtenerSubmarcaCliente(CmbMarca.SelectedValue);
        CmbSubMarca.DataSource= dssbm;
        CmbSubMarca.DataValueField = "id_SubBrand";
        CmbSubMarca.DataTextField = "Name_SubBrand";
        CmbSubMarca.DataBind();
        CmbSubMarca.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
    
    
    
    
    }
    private void ObtenerPresentacionesCliente() {

        DataSet dspresenta = null;

        if (CmbMarca.SelectedValue == "0")
        {
            dspresenta = Carrusel.Get_ObtenerPresentacionesClientte(CmbCategoria.SelectedValue, 0, "0");
            CmbPresentacion.DataSource = dspresenta;
            CmbPresentacion.DataValueField = "id_Product";
            CmbPresentacion.DataTextField = "Product_Name";
            CmbPresentacion.DataBind();
            CmbPresentacion.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
        }
        else {

            dspresenta = Carrusel.Get_ObtenerPresentacionesClientte(CmbCategoria.SelectedValue, Convert.ToInt32(CmbMarca.SelectedValue), CmbSubMarca.SelectedValue);
            CmbPresentacion.DataSource = dspresenta;
            CmbPresentacion.DataValueField = "id_Product";
            CmbPresentacion.DataTextField = "Product_Name";
            CmbPresentacion.DataBind();
            CmbPresentacion.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
        
        
        
        }
        if (CmbSubMarca.SelectedValue == "0") {

            dspresenta = Carrusel.Get_ObtenerPresentacionesClientte(CmbCategoria.SelectedValue, Convert.ToInt32(CmbMarca.SelectedValue), "0");
            CmbPresentacion.DataSource = dspresenta;
            CmbPresentacion.DataValueField = "id_Product";
            CmbPresentacion.DataTextField = "Product_Name";
            CmbPresentacion.DataBind();
            CmbPresentacion.Items.Insert(0, new ListItem("<Seleccione...>", "0"));
        
        
        
        
        
        }
    
    }










    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            sUser = this.Session["sUser"].ToString();
            sPassw = this.Session["sPassw"].ToString();
            companyid = Convert.ToInt32(this.Session["companyid"]);
            scountry = this.Session["scountry"].ToString();
            snamecompany = this.Session["sNombre"].ToString();
            try
            {
                  
              
                Obtener_Servicios_Canales();
                Obtener_Menustipo();
                Obtener_MenuDinamico();
               
                Obtener_ActividadesComercio();//Esta funcion debe dispararse dede el selectchange del carrusel cambiarla
                ObtenerPaisesCliente();
                ObtenerSegmentosCliente();
                ObtenerAgrupacionComercial();
                Obtenercategoria();

              

     

                 


















            }


            catch (Exception ex)
            {
                this.Session.Abandon();
                string errmensajeseccion = Convert.ToString(ex);
                Response.Redirect("~/err_mensaje_seccion.aspx", true);

            }






            //if (!Page.User.Identity.IsAuthenticated) Response.Redirect("login.aspx");
            //if (Session["CompanyId"] == null) Response.Redirect("login.aspx");
            //if (!Page.IsPostBack)
            //{
            //lblUsuario.Text = string.Format("{0}", Session["NombreUsuario"].ToString());
            //    int CompanyId = Convert.ToInt32(Session["CompanyId"]);


            //    GridView1.CssClass = "tabla";
            //    GridView1.DataSource = new Lucky().ObtenerServiciosWS(CompanyId);
            //    GridView1.DataBind();
            //}
        }




    }

    protected void BtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        BtnRest.Visible = true;
        BtnAdd.Visible = false;
        MenuEmpresarial.Visible = true;
        BtnRestDina.Visible = false;
        BtnAddDina.Visible = true;
        MenuDinamico.Visible = false;
        BtnRestFav.Visible = false;
        BtnAddFav.Visible = true;
        MenuFavoritos.Visible = false;

    }

    protected void BtnRest_Click(object sender, ImageClickEventArgs e)
    {
        BtnAdd.Visible = true;
        BtnRest.Visible = false;
        MenuEmpresarial.Visible = false;
    }

    protected void BtnAddDina_Click(object sender, ImageClickEventArgs e)
    {
        BtnAdd.Visible = true;
        BtnRest.Visible = false;
        MenuEmpresarial.Visible = false;
        BtnRestDina.Visible = true;
        BtnAddDina.Visible = false;
        MenuDinamico.Visible = true;
        BtnRestFav.Visible = false;
        BtnAddFav.Visible = true;
        MenuFavoritos.Visible = false;


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
        Lbltitulo.Text = "Informe " + MenuEmpresarial.SelectedValue;
        
        Page.RegisterStartupScript("prueba","AjaxConsulta('Cliente_VGMAY.aspx','leftSide','idUsuario={2}&idService={3}&idCanal={1}','1')\" title=\"{0}\">{0}</span>");
     
        MenuEmpresarial.Visible = false;
    }

    protected void MenuDinamico_MenuItemClick(object sender, MenuEventArgs e)
    {
        Lbltitulo.Text = "Informe" +" "+ MenuDinamico.SelectedItem.Text;
        string url;
        //Response.Redirect("../Cliente/Informes/Cliente_VGMAY.aspx", true);
        StringBuilder sb = new StringBuilder();

        sb.Append("<script language=Javascript>\n");
        sb.Append("AjaxConsulta() { \n");
        sb.Append("....");
        sb.Append("</script>");
        Page.RegisterStartupScript("script", sb.ToString());

        sb.Append(string.Format("../Cliente/Informes/Cliente_VGMAY.aspx"));
        url = sb.ToString();
        Response.Redirect(url, true);
    


    }

    protected void btncloseseccion_Click(object sender, ImageClickEventArgs e)
    {

        PdAmon.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
        this.Session.RemoveAll();


        Response.Redirect("~/login.aspx");
    }

    protected void lvCarrusel_SelectedIndexChanged(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(string.Format("<span style=\"cursor:pointer;\" onclick=\"AjaxConsulta('prueba.html','leftSide','idUsuario={2}&idService={3}&idCanal={1}','1')\" title=\"{0}\">{0}</span>", companyid, 220));
      


        Obtener_ActividadesComercio();
    }

    protected void CmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerCityCliente();
        ObtenerDistritosCliente();
    }

    protected void CmbPais_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerDepartamentos();
        ObtenerCityCliente();
        ObtenerDistritosCliente();
    }

    protected void cmbcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerDistritosCliente();
    }

    protected void BtnFechaDesde_Click(object sender, EventArgs e)
    {
        MenuEmpresarial.Enabled = false;
    }

    protected void CmbSegmento_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerPDVCliente();
    }

    protected void lvCarrusel_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        Obtener_ActividadesComercio();
    }

    protected void cmbTipoAgrupacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        obtenerAgrupacionComercial();
    }

    protected void CmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtnerMarcaxCategoria();
    }

    protected void CmbMarca_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerSubmarcaxMarca();
    }


}


