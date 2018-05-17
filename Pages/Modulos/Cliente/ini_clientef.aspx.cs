using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Xml;
using System.Globalization;
using Lucky.Entity.Common.Security;
using Lucky.Entity.Common.Application.Security;
using Lucky.Business.Common.Security;
using Lucky.Business.Common.Application;
using Lucky.CFG;
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.CFG.Tools;
using SIGE.Facade_Procesos_Administrativos;
using SIGE.Facade_Proceso_Planning;
using SIGE.Facade_Proceso_Cliente;


namespace SIGE.Pages.Modulos.Cliente
{


    public partial class ini_clientef : System.Web.UI.Page
    {
        #region Propiedades
        public string Nivel
        {
            get
            {
                return cmbnivel.SelectedValue;
            }
        }

        #endregion
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

        #region Zona de Funciones



        //private void pruebagraficachar() {


        //    // Set Cylinder drawing style attribute
        //    charSales.Series["Series1"]["DrawingStyle"] = "Cylinder";
        //    charSales.Series["Series2"]["DrawingStyle"] = "Cylinder";
        //    charSales.Series["Series3"]["DrawingStyle"] = "Cylinder";

        //    // Set series chart type
        //    if (Bar.Checked)
        //    {
        //        charSales.Series["Series1"].ChartType = SeriesChartType.Bar;
        //        charSales.Series["Series2"].ChartType = SeriesChartType.Bar;
        //        charSales.Series["Series3"].ChartType = SeriesChartType.Bar;
        //        charSales.Titles[0].Text = "3D Bar Cylinder";
        //    }
        //    else if (Column.Checked)
        //    {
        //        charSales.Series["Series1"].ChartType = SeriesChartType.Column;
        //        charSales.Series["Series2"].ChartType = SeriesChartType.Column;
        //        charSales.Series["Series3"].ChartType = SeriesChartType.Column;
        //        charSales.Titles[0].Text = "3D Column Cylinder";
        //    }

        //    // Set clustered flag
        //    charSales.ChartAreas["ChartArea1"].Area3DStyle.IsClustered = !Clustered.Checked;

        //    // Set points depth
        //    if (pointDepth.SelectedItem.Text != "")
        //        charSales.ChartAreas["ChartArea1"].Area3DStyle.PointDepth = int.Parse(pointDepth.SelectedItem.Text);

        //    if (gapDepth.SelectedItem.Text != "")
        //        charSales.ChartAreas["ChartArea1"].Area3DStyle.PointGapDepth = int.Parse(gapDepth.SelectedItem.Text);


        //}

        private void ObtenerNivelesxCliente()
        {

            DataTable dtnivel = null;
            dtnivel = Carrusel.Get_ObtenerNivelesUSuario(sperfilid);
            bool sdetail;
            bool sgeneral;

            sdetail = Convert.ToBoolean(dtnivel.Rows[0]["Perfil_Detail"]);
            sgeneral = Convert.ToBoolean(dtnivel.Rows[0]["Perfil_General"]);

            if (sdetail == true && sgeneral == true)
            {
                cmbnivel.Items.Insert(0, new ListItem("General", "1"));
                cmbnivel.Items.Insert(0, new ListItem("Detallado", "2"));
                cmbnivel.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            }

            if (sdetail == false && sgeneral == true)
            {
                cmbnivel.Items.Insert(0, new ListItem("General", "1"));
                cmbnivel.Items.Insert(0, new ListItem("--Seleccione--", "0"));
            }
        }

        private void Obtener_Servicios_Canales()
        {
            DataTable dtservi = LuckyWs.ObtenerServiciosWS(companyid, scountry);

            /*Carlos Hernandez*/
            //this.ViewState["dt"] = dtservi;


            //lblUsuario.Text = snamecompany;
            lvCarrusel.DataSource = LuckyWs.ObtenerServiciosWS(companyid, scountry);
            lvCarrusel.DataBind();




            /*mauricio ortiz*/

            //for (int i = 0; i <= dtservi.Rows.Count - 1; i++)
            //{

            //    codservice = Convert.ToInt32(dtservi.Rows[i]["Codigo"]);




            //}







        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                sUser = this.Session["sUser"].ToString();
                sPassw = this.Session["sPassw"].ToString();
                sperfilid = this.Session["Perfilid"].ToString();


                servicio = Convert.ToInt32(Session["Service"]);
                companyid = Convert.ToInt32(this.Session["companyid"]);
                scountry = this.Session["scountry"].ToString();
                snamecompany = this.Session["sNombre"].ToString();
                lblUsuario.Text = snamecompany;
                //cmbnivel.SelectedValue = "1";

                try
                {
                    ObtenerNivelesxCliente();
                    Obtener_Servicios_Canales();

                    //ObtenerAñosIni();
                    //ObtenerMesIni();
                    //Charprueba();
                    //ObtenerAñosFin();
                    //ObtenerMesesFin();
                    //Obtener_Menustipo();
                    //Obtener_MenuDinamico();
                    //pruebagraficachar();


                    //Obtener_ActividadesComercio();//Esta funcion debe dispararse dede el selectchange del carrusel cambiarla
                    //ObtenerPaisesCliente();
                    //ObtenerSegmentosCliente();
                    //ObtenerAgrupacionComercial();
                    //Obtenercategoria();
                }

                catch (Exception ex)
                {
                    PdAmon.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                    this.Session.Abandon();
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);

                    //this.Session.Abandon();
                    //string errmensajeseccion = Convert.ToString(ex);
                    //Response.Redirect("~/err_mensaje_seccion.aspx", true);

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

        protected void btncloseseccion_Click(object sender, ImageClickEventArgs e)
        {
            PdAmon.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
            /* colocar label de session user */
            this.Session.RemoveAll();


            Response.Redirect("~/login.aspx", true);
        }

        protected void lvCarrusel_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("<span style=\"cursor:pointer;\" onclick=\"AjaxConsulta('prueba.html','leftSide','idUsuario={2}&idService={3}&idCanal={1}','1')\" title=\"{0}\">{0}</span>", companyid, 220));
            //Obtener_ActividadesComercio();
        }


        protected void lvCarrusel_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            //Obtener_ActividadesComercio();
        }

        protected void cmbaño_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BtnFechaDesde_ModalPopupExtender.Show();
        }

        protected void cmbañofin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BtnFehasHasta_ModalPopupExtender.Show();
        }

        protected void btnasignafein_Click(object sender, EventArgs e)
        {
            //BtnFechaDesde_ModalPopupExtender.Show();
            //lblDate1.Text = cmbaño.SelectedItem.Text + ' ' + cmbmes.SelectedItem.Text;
            //btnasignafein.Visible = false;
            //btncancelfecha.Text = "Agregar Fecha";
            //this.Session["date1año"] = cmbaño.SelectedValue;
            //this.Session["date1mes"] = cmbmes.SelectedValue;
        }

        protected void btnasigfehafin_Click(object sender, EventArgs e)
        {
            //BtnFehasHasta_ModalPopupExtender.Show();
            //lblDate2.Text = cmbañofin.SelectedItem.Text + ' ' + cmbmesfin.SelectedItem.Text;
            //btnasigfehafin.Visible = false;
            //btncancelarfechfin.Text = "Agregar Fecha";
            //this.Session["date2año"] = cmbañofin.SelectedValue;
            //this.Session["date2mes"] = cmbmesfin.SelectedValue;

        }

        protected void cmbmes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BtnFechaDesde_ModalPopupExtender.Show();
        }

        protected void BtnFechaHasta_Click(object sender, EventArgs e)
        {
            //MenuEmpresarial.Enabled = false;

            //cmbañofin.Items.Clear();
            //cmbmesfin.Items.Clear();
            //lblDate2.Text = "";

            //btnasigfehafin.Visible = true;

            //btncancelarfechfin.Text = "Cancelar";
            //ObtenerAñosFin();
            //ObtenerMesesFin();
            //BtnFehasHasta_ModalPopupExtender.Show();

        }

        protected void cmbmesfin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BtnFechaDesde_ModalPopupExtender.Show();
        }
        protected void cmbnivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ConstruirNivelGeneralservicio();
        }

        protected void cmbnivel_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
    }
}