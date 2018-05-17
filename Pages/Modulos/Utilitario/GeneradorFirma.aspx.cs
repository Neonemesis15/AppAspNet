using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;
using WebsitesScreenshot.SupportClasses;
using WebsitesScreenshot;
using Lucky.CFG.Tools;
using Lucky.Business.Common.Application;
using Lucky.Entity.Common.Security;


namespace SIGE.Pages.Modulos.Utilitario
{
    public partial class GeneradorFirma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //imgLucky.ImageUrl = "lucky.jpg";//Server.MapPath("lucky.JPG");
            //imgHorizontal.ImageUrl = "lineahorizontal.jpg";//Server.MapPath("lineahorizontal.JPG");
            //imgVertical.ImageUrl = "lineavertical.jpg";// Server.MapPath("lineavertical.JPG");
            //image.ImageUrl = "1.jpg";//Server.MapPath("1.JPG");
            ////imgPunto.ImageUrl = "punto.png";// Server.MapPath("punto.png");
            if ("registrar" == Session["Registrar"].ToString())
            {
                Consulta2();
                generar2();
            }
            else
            {
                if (!Page.IsPostBack)
                {

                    Consulta();
                    generar();
                }
            }
        }

        Firma oFirma = new Firma();

        public void Consulta()
        {
            if (Session["consulta"] == null)
            {
                Session["for"] = "0";
                Session["consulta"] = "1";
                Session["tabla"] = oFirma.Consultatodos();

                DataTable dt = (DataTable)Session["tabla"];
                Session["cont"] = dt.Rows.Count;

                int i = Convert.ToInt32(Session["for"]);


                Session["id"] = dt.Rows[i]["idFirma"].ToString();
                lblNombreApellido.Text = dt.Rows[i]["Nombre"].ToString() + " " + dt.Rows[i]["ApellidoPaterno"].ToString() + " " + dt.Rows[i]["ApellidoMaterno"].ToString().Substring(0, 1) + ".";

                lblCargo.Text = dt.Rows[i]["Cargo"].ToString();

                lblNumeros.Text = "C : " + dt.Rows[i]["Celular"].ToString() + " T : " + dt.Rows[i]["telefono"].ToString() + " A : " + dt.Rows[i]["anexo"].ToString();
                Session["Celular"] = dt.Rows[i]["Celular"].ToString();
                Session["telefono"] = dt.Rows[i]["telefono"].ToString();
                Session["anexo"] = dt.Rows[i]["anexo"].ToString();

                Session["for"] = i + 1;
            }
            else
            {
                DataTable dt = (DataTable)Session["tabla"];

                if (Convert.ToInt32(Session["cont"]) > Convert.ToInt32(Session["for"]))
                {

                    int i = Convert.ToInt32(Session["for"]);
                    Session["id"] = dt.Rows[i]["idFirma"].ToString();
                    lblNombreApellido.Text = dt.Rows[i]["Nombre"].ToString() + " " + dt.Rows[i]["ApellidoPaterno"].ToString() + " " + dt.Rows[i]["ApellidoMaterno"].ToString().Substring(0, 1) + ".";

                    lblCargo.Text = dt.Rows[i]["Cargo"].ToString();

                    lblNumeros.Text = "C : " + dt.Rows[i]["Celular"].ToString() + " T : " + dt.Rows[i]["telefono"].ToString() + " A : " + dt.Rows[i]["anexo"].ToString();
                    Session["Celular"] = dt.Rows[i]["Celular"].ToString();
                    Session["telefono"] = dt.Rows[i]["telefono"].ToString();
                    Session["anexo"] = dt.Rows[i]["anexo"].ToString();

                    Session["for"] = i + 1;
                }
                else
                {
                    Response.Redirect("http://www.google.com.pe/");
                }

            }



        }

        public void Consulta2()
        {



            string RPC="";
            string TELEFONO="";
            string ANEXO="";
            string RPM="";
            string NEXTEL="";




            lblNombreApellido.Text = Session["txtNombre"].ToString() + " " + Session["txtApePaterno"].ToString() + " " + Session["txtApeMaterno"].ToString().Substring(0, 1) + ".";

            lblCargo.Text = Session["txtCargo"].ToString();

            if (Session["txtCelular"].ToString() != "")
            {
                RPC = "RPC : " + Session["txtCelular"].ToString();
            }
            if (Session["txtTelefono"].ToString() != "")
            {
                TELEFONO = " T : " + Session["txtTelefono"].ToString();
            }
            if (Session["txtAnexo"].ToString() != "")
            {
                ANEXO = " A : " + Session["txtAnexo"].ToString();
            }

            if (Session["txtCelularRPM"].ToString() != "")
            {
                RPM = "RPM : " + Session["txtCelularRPM"].ToString();
            }

            if (Session["txtCelularNextel"].ToString() != "")
            {
                NEXTEL = " NEXTEL : " + Session["txtCelularNextel"].ToString();
            }

            lblNumeros.Text = RPC + TELEFONO + ANEXO;

            lblNumerosAlt.Text = RPM + NEXTEL;




        }

        public void PrintWebControl(Control ctrl)
        {

            Random r = new Random();
            int a = r.Next(0, 100);
            StringWriter stringWrite = new StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            if (ctrl is WebControl)
            {
                Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
            }
            Page pg = new Page();
            pg.EnableEventValidation = false;


            HtmlForm frm = new HtmlForm();
            pg.Controls.Add(frm);
            frm.Attributes.Add("runat", "server");
            frm.Controls.Add(ctrl);
            pg.DesignerInitialize();
            pg.RenderControl(htmlWrite);
            string strHTML = stringWrite.ToString();




            WebsitesScreenshot.WebsitesScreenshot _Obj = new WebsitesScreenshot.WebsitesScreenshot();
            WebsitesScreenshot.WebsitesScreenshot.Result _Result;
            //Capture web page for the specified url
            _Result = _Obj.CaptureHTML(strHTML);
            if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
            {
                string aa= Server.MapPath("WebsitesScreenshot.png");
                _Obj.ImageFormat = WebsitesScreenshot.WebsitesScreenshot.ImageFormats.PNG;
                _Obj.SaveImage(Server.MapPath("WebsitesScreenshot.png"));

            }
            //Convert local web page to image
            _Result = _Obj.CaptureWebpage("C:\\test.html");
            if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
            {
                _Obj.ImageFormat = WebsitesScreenshot.WebsitesScreenshot.ImageFormats.PNG;
                _Obj.SaveImage("c:\\test.png");
            }
            _Obj.Dispose();



        }

        public void generar2()
        {

            // WritePdf();
            PrintWebControl(divv);
            //asss();

            Image2.ImageUrl = "~/WebsitesScreenshot.png";
            Image2.Visible = true;


            byte[] ar = oFirma.imageToByteArray(oFirma.urlimagenToImagen("WebsitesScreenshot.png"));

            byte[] b = oFirma.CropImageFile(ar, 780, 295, 878, 410);

            oFirma.MImage("", b);

            
            Response.Redirect("RegistrarFirmas.aspx");



        }

        public void generar()
        {

            // WritePdf();
            PrintWebControl(divv);
            //asss();

            Image2.ImageUrl = "~/WebsitesScreenshot.png";
            Image2.Visible = true;


            byte[] ar = oFirma.imageToByteArray(oFirma.urlimagenToImagen("WebsitesScreenshot.png"));

            byte[] b = oFirma.CropImageFile(ar, 850, 280, 878, 400);

            oFirma.MImage("", b);



            Response.Redirect("RegistrarFirmas.aspx");
        }
    }
}