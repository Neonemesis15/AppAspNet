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
using System.Drawing.Imaging;
using System.Drawing;
using Lucky.CFG.Tools;
using Lucky.Business.Common.Application;
using Lucky.Entity.Common.Security;

namespace SIGE.Pages.Modulos.Utilitario
{
    public partial class Auditoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarGrilla_ReporteFotografico();
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            cargarGrilla_ReporteFotografico();
        }


        public DataTable Get_ReporteFotografico(int iidperson, string sidplanning, string sidchanel, int cod_oficina, int id_NodeComercial, string ClientPDV_Code, string sidtiporeporte, string sid_categoriaproducto, string sidbrand, string stipreport, DateTime dfecha_inicio, DateTime dfecha_fin, int icompanyid)
        {
            Conexion oCoon = new Conexion();
            DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REPORTE_FOTOGRAFICO", iidperson, sidplanning, sidchanel, cod_oficina, id_NodeComercial, ClientPDV_Code, sidtiporeporte, sid_categoriaproducto, sidbrand, stipreport, dfecha_inicio, dfecha_fin, icompanyid);
            return dt;
        }
        protected void cargarGrilla_ReporteFotografico()
        {
            try
            {
                //------------------------volver a deshabilitgar las columnas--------------------------
                //gv_Foto.Columns[6].Visible = false;
                

                //-----------------//-----------------//-----------------//-----------------//-----------------

               DataTable dt_Foto;
              
                    Conexion oCoon = new Conexion(2);
                   
                        //es momentaneo cambiar el store para que sea uno solo
                        dt_Foto = oCoon.ejecutarDataTable("UP_WEBXPLORA_UTILITARIO_CONSULTA_REPORTE_AUDITORIA");
                        //gv_Foto.Columns[2].Visible = false; //Distribuidora
                        //gv_Foto.Columns[3].Visible = false; //Tipo
                        //gv_Foto.Columns[4].Visible = false; //Distrito
                        //gv_Foto.Columns[5].Visible = false; //Direccion
                        //gv_Foto.Columns[6].Visible = false; //Supervisor

                        //gv_Foto.Columns[7].Visible = true;//Zona
                        //gv_Foto.Columns[8].Visible = true;//Nivel
                        //gv_Foto.Columns[9].Visible = true;//CodigoPtoVenta
                        ////gv_Foto.Columns[10].Visible = false;//Punto de venta
                        //gv_Foto.Columns[11].Visible = true;//Categoria
                        //gv_Foto.Columns[12].Visible = true;//Marca
                        //gv_Foto.Columns[13].Visible = true;//Tipo de Reporte
                        //gv_Foto.Columns[14].Visible = true;//Comentario

                        gv_Foto.DataSource = dt_Foto;
                        gv_Foto.DataBind();
                
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
             
            }

        }

        //Codigo para Ordenar y Paginar la grilla "grid_detalle_foto"------------------
        public string SortExpression
        {
            get { return (ViewState["SortExpression"] == null ? string.Empty : ViewState["SortExpression"].ToString()); }
            set { ViewState["SortExpression"] = value; }
        }

        public string SortDirection
        {
            get { return (ViewState["SortDirection"] == null ? string.Empty : ViewState["SortDirection"].ToString()); }
            set { ViewState["SortDirection"] = value; }
        }

        private string GetSortDirection(string sortExpression)
        {
            if (SortExpression == sortExpression)
            {
                if (SortDirection == "ASC")
                    SortDirection = "DESC";
                else if (SortDirection == "DESC")
                    SortDirection = "ASC";
                return SortDirection;
            }
            else
            {
                SortExpression = sortExpression;
                SortDirection = "ASC";
                return SortDirection;
            }
        }
        protected void gv_Foto_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = gv_Foto.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                gv_Foto.DataSource = dataView;
                gv_Foto.DataBind();
            }
        }
       
     

        protected void gv_Foto_CancelCommand(object source, GridCommandEventArgs e)
        {

            try
            {
                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_EditCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            try
            {
                gv_Foto.CurrentPageIndex = e.NewPageIndex;
                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        protected void gv_Foto_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            try
            {
                cargarGrilla_ReporteFotografico();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }


     





        protected void imgbtn_cancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //RadBinaryImage_fotoBig.DataBind();
                //RadBinaryImage_fotoBig.Visible = false;
                //ModalPopup_Edit.Hide();
            }
            catch (Exception ex)
            {
                //comentario
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

        /// <summary>
        /// Verificar este metodo Ing. Carlo  Hernandez ---Preguntar a Ditmar Estrada
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void gv_Foto_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                //panelEdit.DataBind();
                //RadBinaryImage_fotoBig.DataBind();
                //RadBinaryImage_fotoBig.Visible = false;
                //GridItem item = gv_Foto.Items[e.Item.ItemIndex];
                GridItem item = gv_Foto.Items[e.Item.ItemIndex];


                Label lbl_id_reg_foto = (Label)item.FindControl("lbl_id_reg_foto");

                
                if (e.CommandName == "VERFOTO")
                {

                    Session["iidregft"] = Convert.ToInt32(lbl_id_reg_foto.Text);
                   

                    DataTable dt = null;
                    Conexion Ocoon = new Conexion();
                    //string sidregft = ((LinkButton)sender).CommandArgument;
                    int iidregft = Convert.ToInt32(lbl_id_reg_foto.Text);

                    dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REG_FOTO", iidregft);
                    byte[] byteArrayIn = (byte[])dt.Rows[0]["foto"];

                    Session["array"] = byteArrayIn;
                    RadBinaryImage_viewFoto.DataValue = byteArrayIn;
                    ModalPopupExtender_viewfoto.Show();

                    //Response.Redirect("verFoto.aspx?iidregft=" + iidregft, true);
                }
                else if (e.CommandName == "EDITFOTO")
                {

                    //string sidregft = ((LinkButton)senser).CommandArgument;
                    int iidregft = Convert.ToInt32(lbl_id_reg_foto.Text);
                    //int Id_repft = Convert.ToInt32(lbl_Id_Reg_Fotogr.Text);
                    if (Int32.Equals(iidregft, 0))
                    {
                        Response.Redirect("/login.aspx");
                    }
                    else
                    {
                        RadBinaryImage imageBinary = (RadBinaryImage)item.FindControl("RadBinaryImage_foto");

                        //RadBinaryImage_fotoBig.DataValue = imageBinary.DataValue;
                       // RadBinaryImage_fotoBig.Visible = false;
                        Session["iidregft"] = iidregft;
                        //Session["Id_repft"] = Id_repft;
                    }
                    //ModalPopup_Edit.Show();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }





        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string a = Server.MapPath("temp.jpg");
            Session["path"] = a;
            MyImage FImage;
            FImage = new MyImage(RadBinaryImage_viewFoto.ImageUrl, (byte[])Session["array"]);
            FImage.Rotate("90", Server.MapPath("temp.jpg"));
            //Image1.ImageUrl = "temp.jpg";
            RadBinaryImage_viewFoto.DataValue = FImage.imageToByteArray((System.Drawing.Image)Session["imagenabyte"]);


            FImage = null;

            ModalPopupExtender_viewfoto.Show();
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            string a = Server.MapPath("temp.jpg");
            Session["path"] = a;
            MyImage FImage;
            FImage = new MyImage(RadBinaryImage_viewFoto.ImageUrl, (byte[])Session["array"]);
            FImage.Rotate("270", Server.MapPath("temp.jpg"));
            //Image1.ImageUrl = "temp.jpg";
            RadBinaryImage_viewFoto.DataValue = FImage.imageToByteArray((System.Drawing.Image)Session["imagenabyte"]);


            FImage = null;

            ModalPopupExtender_viewfoto.Show();
        }

        protected void ibtnGuardarImagen_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Int32 iidregft = Convert.ToInt32(Session["iidregft"]);
                Int32 Id_repft = Convert.ToInt32(Session["Id_repft"]);

                Conexion Ocoon = new Conexion();

                byte[] foto = (byte[])Session["array"];

                Ocoon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REG_FOTO", iidregft, Id_repft, foto, Session["sUser"].ToString(), DateTime.Now);
                //RadBinaryImage_fotoBig.Visible = false;
                cargarGrilla_ReporteFotografico();

            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }


    }
}