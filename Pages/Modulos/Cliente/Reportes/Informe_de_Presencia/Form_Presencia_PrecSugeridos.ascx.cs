using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Data;
using Telerik.Web.UI;
using System.Data;

namespace SIGE.Pages.Modulos.Cliente.Reportes.Informe_de_Presencia
{
    public partial class Form_Presencia_PrecSugeridos : System.Web.UI.UserControl
    {

        private Int32 iidcompany;
        private String sidcanal;
        private String siduser;
        private Int32 Report;
        private Int32 iservicio;
        Conexion oCoon = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        public void llenaAñosUC(DataTable dty)
        {
            rcmb_añoPS.DataSource = dty;
            rcmb_añoPS.DataValueField = "Years_Number";
            rcmb_añoPS.DataTextField = "Years_Number";
            rcmb_añoPS.DataBind();
            rcmb_añoPS.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));

            rcmb_añoNewPS.DataSource = dty;
            rcmb_añoNewPS.DataValueField = "Years_Number";
            rcmb_añoNewPS.DataTextField = "Years_Number";
            rcmb_añoNewPS.DataBind();
            rcmb_añoNewPS.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
        }
        public void llenaMesUC(DataTable dtm)
        {

            rcmb_mesPS.DataSource = dtm;
            rcmb_mesPS.DataValueField = "codmes";
            rcmb_mesPS.DataTextField = "namemes";
            rcmb_mesPS.DataBind();
            rcmb_mesPS.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));


            rcmb_mesNewDesdePS.DataSource = dtm;
            rcmb_mesNewDesdePS.DataValueField = "codmes";
            rcmb_mesNewDesdePS.DataTextField = "namemes";
            rcmb_mesNewDesdePS.DataBind();
            rcmb_mesNewDesdePS.Items.Insert(0, new RadComboBoxItem("--Desde--", "0"));


            rcmb_mesNewHastaPS.DataSource = dtm;
            rcmb_mesNewHastaPS.DataValueField = "codmes";
            rcmb_mesNewHastaPS.DataTextField = "namemes";
            rcmb_mesNewHastaPS.DataBind();
            rcmb_mesNewHastaPS.Items.Insert(0, new RadComboBoxItem("--Hasta--", "0"));
        }

        
        protected void btn_buscarPS_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_info.Text = "";
                lbl_msj_validation.Text = "";
                if (rcmb_añoPS.SelectedIndex > 0 && rcmb_mesPS.SelectedIndex > 0 )
                {

                    iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                    sidcanal = this.Session["Canal"].ToString();

                    DataTable dtprec = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_PRECIOS_SUGERIDOS_SKU_MANDATORIOS", sidcanal, iidcompany, rcmb_añoPS.SelectedValue, rcmb_mesPS.SelectedValue);


                    RadGrid_PSugeridosPS.DataSource = dtprec;
                    RadGrid_PSugeridosPS.DataBind();

                    if (RadGrid_PSugeridosPS.Items.Count > 0)
                    {
                        lbl_info.Text = "Precios del mes de: " + rcmb_mesPS.SelectedItem.Text + " del año : " + rcmb_añoPS.SelectedItem.Text;

                    }
                    //UpdatePanel_gridPSugeridoPS.Update();

                }
                else
                {
                    ModalPopupExtender_validationMessagePS.Show();
                    lbl_msj_validation.Text = "Por favor, termine de seleccionar las opciones.";
                    //UpdatePanel_validationMessagePS.Update();

                }
            }
            catch (Exception ex)
            {

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(System.Configuration.ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }

       
        protected void btn_guardarPS_Click(object sender, EventArgs e)
        {
            try
            {
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();
                siduser = this.Session["sUser"].ToString();
                for (int i = 0; i < RadGrid_PSugeridosPS.Items.Count; i++)
                {

                    GridDataItem ditem = RadGrid_PSugeridosPS.Items[i];

                    string cod_product = ditem["cod_Product"].Text;
                    object PrecSugerido = (ditem.FindControl("rtxt_precio") as RadNumericTextBox).DbValue;


                    string id_year = (ditem.FindControl("lbl_id_año") as Label).Text;
                    string id_month = (ditem.FindControl("lbl_id_mes") as Label).Text;

                    if (PrecSugerido != null)
                    {
                        oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_ACTUALIZAR_PRECIO_SUGERIDO", sidcanal, iidcompany, cod_product, id_year, id_month, PrecSugerido, siduser, DateTime.Now);
                    }
                }

                lbl_msj_validation.Text = "Registro exitoso.";
                lbl_msj_validation.ForeColor = System.Drawing.Color.Green;
                ModalPopupExtender_validationMessagePS.Show();
            }
            catch (Exception ex)
            {
                lbl_msj_validation.Text = "Error, intentalo nuevamente.";
                lbl_msj_validation.ForeColor = System.Drawing.Color.Red;
                ModalPopupExtender_validationMessagePS.Show();

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(System.Configuration.ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }
        protected void RadGrid_newPSugeridoPS_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                iservicio = Convert.ToInt32(this.Session["Service"]);
                sidcanal = this.Session["Canal"].ToString();
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());

                DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_SKU_MANDATORIOS", iservicio, sidcanal, iidcompany);

                RadGrid_newPSugeridoPS.DataSource = dt;
                //En este evento no se nececita llamar el DataBind()

                //UpdatePanel_newPSugeridoPS.Update();
            }
            catch (Exception ex)
            {
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(System.Configuration.ConfigurationManager.AppSettings["COUNTRY"]);
            }
        }
        protected void btn_guardarPSNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (rcmb_añoNewPS.SelectedIndex > 0 && rcmb_mesNewDesdePS.SelectedIndex > 0 && rcmb_mesNewHastaPS.SelectedIndex > 0)
                {

                    iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                    sidcanal = this.Session["Canal"].ToString();
                    siduser = this.Session["sUser"].ToString();
                    for (int i = 0; i < RadGrid_newPSugeridoPS.Items.Count; i++)
                    {

                        GridDataItem ditem = RadGrid_newPSugeridoPS.Items[i];

                        string cod_product = ditem["cod_Product"].Text;
                        object PrecSugerido = (ditem.FindControl("rtxt_precio") as RadNumericTextBox).DbValue;


                        if (PrecSugerido != null)
                        {
                            oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_INSERTAR_PRECIO_SUGERIDO_SKU_MADATORIO", sidcanal, iidcompany, rcmb_añoNewPS.SelectedValue, rcmb_mesNewDesdePS.SelectedValue, rcmb_mesNewHastaPS.SelectedValue, cod_product, PrecSugerido, siduser, DateTime.Now, siduser, DateTime.Now);
                        }
                    }

                    ModalPopupExtender_addPS.Show();

                    lbl_msj_validation.Text = "Registro exitoso.";
                    lbl_msj_validation.ForeColor = System.Drawing.Color.Green;
                    ModalPopupExtender_validationMessagePS.Show();
                }
                else
                {
                    //ModalPopupExtender_addPS.Hide();

                    ModalPopupExtender_validationMessagePS.Show();
                    lbl_msj_validation.Text = "Por favor, termine de seleccionar las opciones.";
                    //UpdatePanel_validationMessagePS.Update();
                }
            }
            catch (Exception ex)
            {
                lbl_msj_validation.Text = "Error, intentalo nuevamente.";
                lbl_msj_validation.ForeColor = System.Drawing.Color.Red;
                ModalPopupExtender_validationMessagePS.Show();

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(System.Configuration.ConfigurationManager.AppSettings["COUNTRY"]);
            }

        }

       
     
    }
}