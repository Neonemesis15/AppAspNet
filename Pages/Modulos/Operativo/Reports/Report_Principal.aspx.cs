using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Text;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Application.Interfaces;
using Lucky.Business;
using Lucky.Business.Common.Application;
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;
using Lucky.CFG.Tools;


namespace SIGE.Pages.Modulos.Operativo.Reports
{
    public partial class Report_Principal1 : System.Web.UI.Page
    {
        #region Zona de Declaración de Variables Generales
        private Competition__Information oCompetition__Information = new Competition__Information();
        Conexion oConn = new Lucky.Data.Conexion();
    
        Facade_Proceso_Operativo.Facade_Proceso_Operativo Get_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Facade_Proceso_Planning.Facade_Proceso_Planning Get_Planning = new SIGE.Facade_Proceso_Planning.Facade_Proceso_Planning();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_Cliente = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();

        int compañia;
        //int person;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                string sUser = this.Session["sUser"].ToString();
                //usersession.Text = sUser;
                
                string sPassw = this.Session["sPassw"].ToString();
                if (sUser != null && sPassw != null)
                {
                    mostrar_opciones();
                    
                }
            }
            catch (Exception ex)
            {
                //Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

        }
        //método provisional
        protected void mostrar_opciones() 
        {
            LinkButton1.Visible = false;//competencia
            LinkButton2.Visible = false;//fotografico
            LinkButton4.Visible = false;//quiebre
            LinkButton6.Visible = false;//stock
            LinkButton8.Visible = false;//SOD
            LinkButton7.Visible = false;//layout
            LinkButton9.Visible = false;//exibiciones
            LinkButton3.Visible = false;//precios
          

            DataTable dt = null;
            compañia = Convert.ToInt32(this.Session["companyid"]);
            dt = Get_Cliente.Get_ObtenerCanalesxCliente(compañia);
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Equals(dt.Rows[i]["codigo_canal"].ToString(),"1000"))
                {
                    //mayoristas
                    LinkButton1.Visible = true;//competencia
                    LinkButton2.Visible = true;//fotografico
                    LinkButton4.Visible = true;//quiebre
                    LinkButton6.Visible = true;//stock
                    LinkButton8.Visible = true;//SOD
                    LinkButton3.Visible = true;//precios
                    LinkSegmento.Visible = false; //Seguimiento segmentacion
                    Likrptsegmen.Visible = false; //Consolidado Segmentacion
                    Likrptsegnv.Visible = true; //No visitas
                    liksgingr.Visible = true;
                    
                }
                if (Equals(dt.Rows[i]["codigo_canal"].ToString() , "1023"))
                {
                    //minoristas
                    LinkButton1.Visible = true;//competencia
                    LinkButton2.Visible = true;//fotografico
                    LinkButton4.Visible = true;//quiebre
                    LinkButton8.Visible = true;//SOD
                    LinkButton3.Visible = true;//precios
                    LinkSegmento.Visible = false; //Seguimiento segmentacion
                    Likrptsegmen.Visible = false; //Consolidado Segmentacion
                    Likrptsegnv.Visible = true; //No visitas
                    liksgingr.Visible = true;

                }
                if (Equals(dt.Rows[i]["codigo_canal"].ToString(), "1241"))
                {
                    //autoservicios
                    LinkButton1.Visible = true;//competencia
                    LinkButton2.Visible = true;//fotografico
                    LinkButton4.Visible = true;//quiebre
                    LinkButton7.Visible = true;//layout
                    LinkButton9.Visible = true;//exibiciones
                    LinkButton3.Visible = true;//precios
                    LinkSegmento.Visible = false; //Seguimiento segmentacion
                    Likrptsegmen.Visible = false; //Consolidado Segmentacion
                    Likrptsegnv.Visible = true; //No visitas
                    liksgingr.Visible = true;
                }
                if (Equals(dt.Rows[i]["codigo_canal"].ToString(), "1002"))
                {
                    //TRADICIONAL SF
                     
                   
                    LinkSegmento.Visible = true; //Seguimiento segmentacion
                    LinkButton2.Visible = true;//fotografico
                    Likrptsegmen.Visible = true; //Consolidado Segmentacion
                    Likrptsegnv.Visible = true; //No visitas
                    liksgingr.Visible = true;
                  
                }

            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportFotografico.aspx");
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Competencia.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Precio.aspx");
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_SOD.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Quiebre.aspx");
        }
        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Stock.aspx");
        }
        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Layout.aspx");
        }
        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Exhibicion.aspx");
        }

        protected void LinkSegmento_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report_Segmentacion.aspx");
        }

        protected void Likrptsegmen_Click(object sender, EventArgs e)
        {
            Response.Redirect("Rpt_Segmentacion.aspx");
        }

        protected void Likrptsegnv_Click(object sender, EventArgs e)
        {
            Response.Redirect("Rpt_SegNov.aspx");
        }

        protected void liksgingr_Click(object sender, EventArgs e)
        {
            Response.Redirect("Rpt_SegIngre.aspx");
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reporte_Pablo.aspx");
        }
    }
}
