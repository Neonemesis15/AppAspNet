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
    public partial class Form_Presencia_Objetivos : System.Web.UI.UserControl
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
            rcmb_año.DataSource = dty;
            rcmb_año.DataValueField = "Years_Number";
            rcmb_año.DataTextField = "Years_Number";
            rcmb_año.DataBind();
            rcmb_año.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));

            rcmb_añoNew.DataSource = dty;
            rcmb_añoNew.DataValueField = "Years_Number";
            rcmb_añoNew.DataTextField = "Years_Number";
            rcmb_añoNew.DataBind();
            rcmb_añoNew.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
        }
        public void llenaMesUC(DataTable dtm)
        {

            rcmb_mes.DataSource = dtm;
            rcmb_mes.DataValueField = "codmes";
            rcmb_mes.DataTextField = "namemes";
            rcmb_mes.DataBind();
            rcmb_mes.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));


            rcmb_mesNewDesde.DataSource = dtm;
            rcmb_mesNewDesde.DataValueField = "codmes";
            rcmb_mesNewDesde.DataTextField = "namemes";
            rcmb_mesNewDesde.DataBind();
            rcmb_mesNewDesde.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));


            rcmb_mesNewHasta.DataSource = dtm;
            rcmb_mesNewHasta.DataValueField = "codmes";
            rcmb_mesNewHasta.DataTextField = "namemes";
            rcmb_mesNewHasta.DataBind();
            rcmb_mesNewHasta.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));

        }
        protected void cargar_gv_objetivosPresenciaPorTipoCiudad()
        {
            try
            {
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();

                DataTable dtObjPresencia = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_OBJETIVOS_DE_PRESENCIA_POR_TIPOCIUDAD", sidcanal, iidcompany,rcmb_año.SelectedValue, rcmb_mes.SelectedValue);


                RadGrid_objetivos.DataSource = dtObjPresencia;
                RadGrid_objetivos.DataBind();

            }
            catch(Exception ex)
            {
                
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(System.Configuration.ConfigurationManager.AppSettings["COUNTRY"]);
 
            }
        }
        protected void cargar_gv_objetivosPresenciaPorOficina()
        {
            try
            {
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();

                DataTable dtObjPresencia = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_OBJETIVOS_DE_PRESENCIA_POR_OFICINA", sidcanal, iidcompany, rcmb_año.SelectedValue, rcmb_mes.SelectedValue);


                RadGrid_objetivos.DataSource = dtObjPresencia;
                RadGrid_objetivos.DataBind();

            }
            catch (Exception ex)
            {

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(System.Configuration.ConfigurationManager.AppSettings["COUNTRY"]);

            }

        }
        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            lbl_msj_validation.Text = "";
            lbl_info.Text = "";
            //btn_guardar.Visible = false;
            if (rcmb_tipoObjetivos.SelectedIndex > 0 && rcmb_año.SelectedIndex > 0 && rcmb_mes.SelectedIndex >0)
            {
                if (rcmb_tipoObjetivos.SelectedValue == "1")
                {
                    cargar_gv_objetivosPresenciaPorTipoCiudad();
                }
                else if (rcmb_tipoObjetivos.SelectedValue == "2")
                {
                    cargar_gv_objetivosPresenciaPorOficina();
                }
               
                if (RadGrid_objetivos.Items.Count > 0)
                {
                    lbl_info.Text = "Objetivos del mes de: " + rcmb_mes.SelectedItem.Text + " del año : " + rcmb_año.SelectedItem.Text;

                }
                //UpdatePanel_gridObjetivo.Update();
            }
            else
            {
                ModalPopupExtender_validationMessage.Show();
                lbl_msj_validation.Text = "Por favor, termine de seleccionar las opciones.";
               // UpdatePanel_validationMessage.Update();

            }
        }
 
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                sidcanal = this.Session["Canal"].ToString();
                siduser = this.Session["sUser"].ToString();
                for (int i = 0; i < RadGrid_objetivos.Items.Count; i++)
                {

                    GridDataItem ditem = RadGrid_objetivos.Items[i];

                    string idElemento = (ditem.FindControl("lbl_id_elemento") as Label).Text;
                    string cod_cobertura = (ditem.FindControl("lbl_id_cobertura") as Label).Text;
                    string id_year = (ditem.FindControl("lbl_id_year") as Label).Text;
                    string id_month = (ditem.FindControl("lbl_id_month") as Label).Text;
                    object objetivo = (ditem.FindControl("rtxt_objetive") as RadNumericTextBox).DbValue;

                    if (rcmb_tipoObjetivos.SelectedValue == "1")
                    {
                        oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_ACTUALIZAR_OBJETIVOS_DE_PRESENCIA_POR_TIPOCIUDAD", sidcanal, iidcompany, Convert.ToInt32(cod_cobertura), idElemento, id_year, id_month, Convert.ToInt32(objetivo), siduser, DateTime.Now);
                    }
                    else if (rcmb_tipoObjetivos.SelectedValue == "2")
                    {
                        oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_ACTUALIZAR_OBJETIVOS_DE_PRESENCIA_POR_CUIDAD", sidcanal, iidcompany, Convert.ToInt32(cod_cobertura), idElemento, id_year, id_month, Convert.ToInt32(objetivo), siduser, DateTime.Now);
                    }

                }

                lbl_msj_validation.Text = "Registro exitoso.";
                lbl_msj_validation.ForeColor = System.Drawing.Color.Green;
                ModalPopupExtender_validationMessage.Show();
            }
            catch (Exception ex)
            {
                lbl_msj_validation.Text = "Error, intentelo nuevamente.";
                lbl_msj_validation.ForeColor = System.Drawing.Color.Red;
                ModalPopupExtender_validationMessage.Show();

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(System.Configuration.ConfigurationManager.AppSettings["COUNTRY"]);

            }
        }
        protected void rcmb_newObj_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (rcmb_newObj.SelectedValue == "1")
            {
                cargar_gv_newObjetivosPresenciaPorTipoCiudad();
            }
            else if (rcmb_newObj.SelectedValue == "2")
            {
                cargar_gv_newObjetivosPresenciaPorOficina();
            }
            else
            {
                RadGrid_newObjtives.DataBind();
            }
            ModalPopupExtender_add.Show();
            //UpdatePanel_newObjtives.Update();
        }
        protected void cargar_gv_newObjetivosPresenciaPorTipoCiudad()
        {
            try
            {
                iservicio = Convert.ToInt32(this.Session["Service"]);
                sidcanal = this.Session["Canal"].ToString();
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());

                DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_POP_Y_TIPOCIUDAD", iservicio, sidcanal, iidcompany);
                RadGrid_newObjtives.DataSource = dt;
                RadGrid_newObjtives.DataBind();
            }
            catch (Exception ex)
            {
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(System.Configuration.ConfigurationManager.AppSettings["COUNTRY"]);
            }
        }
        protected void cargar_gv_newObjetivosPresenciaPorOficina()
        {
            try
            {
                iservicio = Convert.ToInt32(this.Session["Service"]);
                sidcanal = this.Session["Canal"].ToString();
                iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());

                DataTable dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENER_POP_Y_OFICINA", iservicio, sidcanal, iidcompany);
                RadGrid_newObjtives.DataSource = dt;
                RadGrid_newObjtives.DataBind();
            }
            catch (Exception ex)
            {
                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(System.Configuration.ConfigurationManager.AppSettings["COUNTRY"]);
            }
        }

        protected void btn_guardarNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (rcmb_newObj.SelectedIndex > 0 && rcmb_mesNewDesde.SelectedIndex > 0 && rcmb_mesNewHasta.SelectedIndex > 0)
                {
                    iservicio = Convert.ToInt32(this.Session["Service"]);
                    Report = Convert.ToInt32(this.Session["Reporte"]);
                    iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                    sidcanal = this.Session["Canal"].ToString();
                    siduser = this.Session["sUser"].ToString();
                    for (int i = 0; i < RadGrid_newObjtives.Items.Count; i++)
                    {

                        GridDataItem ditem = RadGrid_newObjtives.Items[i];

                        string idElemento = (ditem.FindControl("lbl_id_elemento") as Label).Text;
                        string cod_cobertura = (ditem.FindControl("lbl_id_cobertura") as Label).Text;

                        object objetivo = (ditem.FindControl("rtxt_objetive") as RadNumericTextBox).DbValue;

                        if ((ditem.FindControl("rtxt_objetive") as RadNumericTextBox).Text.Length != 0)
                        {
                            if (rcmb_newObj.SelectedValue == "1")
                            {
                                oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_INSERTAR_OBJETIVOS_DE_PRESENCIA_POR_TIPOCIUDAD", iservicio, sidcanal, iidcompany, Report, Convert.ToInt32(cod_cobertura), idElemento,rcmb_añoNew.SelectedValue, rcmb_mesNewDesde.SelectedValue, rcmb_mesNewHasta.SelectedValue, Convert.ToInt32(objetivo), siduser, DateTime.Now, siduser, DateTime.Now);
                            }
                            else if (rcmb_newObj.SelectedValue == "2")
                            {
                                oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_INSERTAR_OBJETIVOS_DE_PRESENCIA_POR_OFICINA", iservicio, sidcanal, iidcompany, Report, Convert.ToInt32(cod_cobertura), idElemento,rcmb_añoNew.SelectedValue, rcmb_mesNewDesde.SelectedValue, rcmb_mesNewHasta.SelectedValue, Convert.ToInt32(objetivo), siduser, DateTime.Now, siduser, DateTime.Now);
                            }
                        }
                    }
                    ModalPopupExtender_add.Show();

                    lbl_msj_validation.Text = "Registro exitoso.";
                    lbl_msj_validation.ForeColor = System.Drawing.Color.Green;
                    ModalPopupExtender_validationMessage.Show();
                }
                else
                {
                    //ModalPopupExtender_add.Hide();

                    ModalPopupExtender_validationMessage.Show();
                    lbl_msj_validation.Text = "Por favor, termine de seleccionar las opciones.";
                    //UpdatePanel_validationMessage.Update();
                }
            }
            catch (Exception ex)
            {
                lbl_msj_validation.Text = "Error, intentalo nuevamente.";
                lbl_msj_validation.ForeColor = System.Drawing.Color.Red;
                ModalPopupExtender_validationMessage.Show();

                Lucky.CFG.Exceptions.Exceptions mesjerror = new Lucky.CFG.Exceptions.Exceptions(ex);
                mesjerror.errorWebsite(System.Configuration.ConfigurationManager.AppSettings["COUNTRY"]);
            }
        }
    }
}