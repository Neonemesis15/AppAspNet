using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Data;
using System.Data;
using System.Configuration;
using Telerik.Web.UI;

namespace SIGE.Pages.Modulos.Cliente.Reportes.Informes_de_Stock
{
    public partial class Form_Stock_RangoDiasGiro : System.Web.UI.UserControl
    {

        Conexion oCoon = new Conexion();
        int icompany;
        string canal;
        int Report;
        string sUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            string sidperdil = this.Session["Perfilid"].ToString();
            if (!Page.IsPostBack)
            {
                if (sidperdil == ConfigurationManager.AppSettings["PerfilAnalista"])
                {
                    string Año = DateTime.Now.Year.ToString();
                    string Mes = DateTime.Now.Month.ToString();
                    if (Mes.Length == 1)
                        Mes = "0" + Mes;
                    cmb_año.SelectedIndex = cmb_año.Items.FindItemIndexByValue(Año);
                    cmb_mes.SelectedIndex = cmb_mes.Items.FindItemIndexByValue(Mes);
                    Llenar_Periodos();
                    cmb_periodo.SelectedIndex = cmb_periodo.FindItemIndexByValue("1");

                    Cargar_gv_RangoDiasGiro();

                    lbl_cliente.Text = this.Session["sNombre"].ToString();
                }
            }
        }
        public void Categorias(DataTable dtcatego)
        {

            if (dtcatego.Rows.Count > 0)
            {

                cmb_categoria.DataSource = dtcatego;
                cmb_categoria.DataValueField = "cod_catego";
                cmb_categoria.DataTextField = "Name_Catego";
                cmb_categoria.DataBind();

                cmb_categoria.Items.Insert(0, new RadComboBoxItem("--Seleccione--", "0"));
            }
            else
            {
                cmb_categoria.DataBind();
            }

        }
        public void Años(DataTable dty)
        {

            if (dty.Rows.Count > 0)
            {
                cmb_año.DataSource = dty;
                cmb_año.DataValueField = "Years_Number";
                cmb_año.DataTextField = "Years_Number";
                cmb_año.DataBind();

                cmb_año.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));

                cmb_año2.DataSource = dty;
                cmb_año2.DataValueField = "Years_Number";
                cmb_año2.DataTextField = "Years_Number";
                cmb_año2.DataBind();

                cmb_año2.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));
            }
            else
            {
                cmb_año.DataBind();
                cmb_año2.DataBind();
            }
        }

        public void Llena_Meses(DataTable dtm)
        {

            if (dtm.Rows.Count > 0)
            {
                cmb_mes.DataSource = dtm;
                cmb_mes.DataValueField = "codmes";
                cmb_mes.DataTextField = "namemes";
                cmb_mes.DataBind();

                cmb_mes.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));


                cmb_mes2.DataSource = dtm;
                cmb_mes2.DataValueField = "codmes";
                cmb_mes2.DataTextField = "namemes";
                cmb_mes2.DataBind();

                cmb_mes2.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));
            }
            else
            {
                cmb_mes.DataBind();
                cmb_periodo.Enabled = false;

                cmb_mes2.DataBind();
                cmb_periodo2.Enabled = false;
            }
        }

        private void Llenar_Periodos()
        {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            canal = this.Session["Canal"].ToString().Trim();

            cmb_periodo.Items.Clear();
            cmb_periodo.Enabled = true;
            DataTable dtp = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", canal, icompany, Report, cmb_mes.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                cmb_periodo.DataSource = dtp;
                cmb_periodo.DataValueField = "id_periodo";
                cmb_periodo.DataTextField = "Periodo";
                cmb_periodo.DataBind();

                cmb_periodo.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));

            }
            else
            {
                cmb_periodo.DataBind();
            }
        }
        protected void cmb_mes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmb_periodo.Items.Clear();
            cmb_periodo.Enabled = true;
            Llenar_Periodos();
        }
        private void Llenar_Periodos2()
        {

            icompany = Convert.ToInt32(this.Session["companyid"]);
            canal = this.Session["Canal"].ToString().Trim();

            cmb_periodo2.Items.Clear();
            cmb_periodo2.Enabled = true;
            DataTable dtp = null;
            Report = Convert.ToInt32(this.Session["Reporte"]);
            dtp = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_OBTENERPERIODOS", canal, icompany, Report, cmb_mes.SelectedValue);
            if (dtp.Rows.Count > 0)
            {
                cmb_periodo2.DataSource = dtp;
                cmb_periodo2.DataValueField = "id_periodo";
                cmb_periodo2.DataTextField = "Periodo";
                cmb_periodo2.DataBind();

                cmb_periodo2.Items.Insert(0, new RadComboBoxItem("--Selecione--", "0"));

            }
            else
            {
                cmb_periodo2.DataBind();
            }
        }
        protected void cmb_mes2_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmb_periodo2.Items.Clear();
            cmb_periodo2.Enabled = true;
            Llenar_Periodos2();
            ModalPopupExtender_addRangDG.Show();
        }

        protected void Cargar_gv_RangoDiasGiro()
        {
            try
            {
                lbl_msj.Text = "";
                if (cmb_año.SelectedIndex > 0 && cmb_mes.SelectedIndex > 0 && cmb_periodo.SelectedIndex > 0)
                {
                    icompany = Convert.ToInt32(this.Session["companyid"]);
                    DataTable dtRangDG = oCoon.ejecutarDataTable("UP_WEBXPLORA_CLIE_V2_GET_RANGO_DIASGIRO_BYPERIODO", icompany,cmb_año.SelectedValue, cmb_mes.SelectedValue,cmb_periodo.SelectedValue);

                    gv_RangoDG.DataSource = dtRangDG;
                    gv_RangoDG.DataBind();
                }
                else
                {
                    lbl_msj.ForeColor = System.Drawing.Color.Red;
                    lbl_msj.Text = "Por favor seleccione: Año, mes y periodo";
                    
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }

 

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            Cargar_gv_RangoDiasGiro();
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            lbl_msj.Text = "";
            try
            {
                icompany = Convert.ToInt32(this.Session["companyid"]);
                sUser = this.Session["sUser"].ToString();
                for (int i = 0; i < gv_RangoDG.Items.Count; i++)
                {
                    GridItem item = gv_RangoDG.Items[i];

                    RadNumericTextBox txt_min_dg_xplora = item.FindControl("txt_min_dg_xplora") as RadNumericTextBox;
                    RadNumericTextBox txt_max_dg_xplora = item.FindControl("txt_max_dg_xplora") as RadNumericTextBox;
                    RadNumericTextBox txt_min_dg_cliente = item.FindControl("txt_min_dg_cliente") as RadNumericTextBox;
                    RadNumericTextBox txt_max_dg_cliente = item.FindControl("txt_max_dg_cliente") as RadNumericTextBox;
                    Label lbl_id_ProductCategory=item.FindControl("lbl_id_ProductCategory") as Label;
                    Label lbl_id_ReportsPlanning=item.FindControl("lbl_id_ReportsPlanning") as Label;


                    oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_INSERT_UPDATE_RANGO_DIASGIRO_BYPERIODO", icompany, lbl_id_ProductCategory.Text.Trim(), lbl_id_ReportsPlanning.Text.Trim(),
                        txt_min_dg_xplora.DbValue, txt_max_dg_xplora.DbValue, txt_min_dg_cliente.DbValue, txt_max_dg_cliente.DbValue, sUser, DateTime.Now, sUser, DateTime.Now);

                    lbl_msj.ForeColor = System.Drawing.Color.Green;
                    lbl_msj.Text = "El cambio se guardo con exito";
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();

                lbl_msj.ForeColor = System.Drawing.Color.Red;
                lbl_msj.Text = "Ocurrió un error, intentelo más tarde. Gracias";
            }
        }


        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
             Cargar_gv_RangoDiasGiro();
        }
        protected void btn_nuevo_Click(object sender, EventArgs e)
        {
            lbl_msj.Text = "";
            Label lbl_msjpopup=Panel_addRangDG.FindControl("lbl_msjpopup") as Label;
            lbl_msjpopup.Text = "";
            try
            {
                icompany = Convert.ToInt32(this.Session["companyid"]);
                sUser = this.Session["sUser"].ToString();
                canal = this.Session["Canal"].ToString().Trim();

                if (cmb_categoria.SelectedIndex>0 && cmb_año2.SelectedIndex > 0 && cmb_mes2.SelectedIndex > 0 && cmb_periodo2.SelectedIndex > 0)
                {
                    Report = Convert.ToInt32(this.Session["Reporte"]);
                    int id_ReportsPlanning = Convert.ToInt32(oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO", canal,Report, cmb_año2.SelectedValue, cmb_mes2.SelectedValue, cmb_periodo2.SelectedValue).Rows[0]["id_ReportsPlanning"]);
                    oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_INSERT_UPDATE_RANGO_DIASGIRO_BYPERIODO", icompany, cmb_categoria.SelectedValue, id_ReportsPlanning,
                            txt_min2_dg_xplora.DbValue, txt_max2_dg_xplora.DbValue, txt_min2_dg_cliente.DbValue, txt_max2_dg_cliente.DbValue, sUser, DateTime.Now, sUser, DateTime.Now);

                    cmb_año.SelectedIndex = cmb_año.Items.FindItemIndexByValue(cmb_año2.SelectedValue);
                    cmb_mes.SelectedIndex = cmb_mes.Items.FindItemIndexByValue(cmb_mes2.SelectedValue);
                    Llenar_Periodos();
                    cmb_periodo.SelectedIndex = cmb_periodo.FindItemIndexByValue(cmb_periodo2.SelectedValue);
                    Cargar_gv_RangoDiasGiro();

                    lbl_msj.ForeColor = System.Drawing.Color.Green;
                    lbl_msj.Text = "El cambio se guardo con exito";
                }
                else
                {
                    lbl_msjpopup.ForeColor = System.Drawing.Color.Red;
                    lbl_msjpopup.Text = "* Por favor seleccione: Categoria, Año, mes y periodo";
                    ModalPopupExtender_addRangDG.Show();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();

                lbl_msjpopup.ForeColor = System.Drawing.Color.Red;
                lbl_msjpopup.Text = "Ocurrió un error, intentelo más tarde. Gracias";
                ModalPopupExtender_addRangDG.Show();
            }
        }

        protected void gv_RangoDG_ItemCommand(object source, GridCommandEventArgs e)
        {
            lbl_msj.Text = "";
            icompany = Convert.ToInt32(this.Session["companyid"]);
            try
            {
                if (e.CommandName == "ELIMINAR")
                {

                    Label lbl_id_ProductCategory = e.Item.FindControl("lbl_id_ProductCategory") as Label;
                    Label lbl_id_ReportsPlanning = e.Item.FindControl("lbl_id_ReportsPlanning") as Label;

                    int iid_ReportsPlanning = Convert.ToInt32(lbl_id_ReportsPlanning.Text);

                    oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_DELETE_RANGO_DIASGIRO_BYPERIODO", icompany, lbl_id_ProductCategory.Text.Trim(), iid_ReportsPlanning);

                    Cargar_gv_RangoDiasGiro();
                    lbl_msj.ForeColor = System.Drawing.Color.Green;
                    lbl_msj.Text = "Eliminacón exitosa.";
                }


            }
            catch (Exception ex)
            {
                ex.Message.ToString();

                lbl_msj.ForeColor = System.Drawing.Color.Red;
                lbl_msj.Text = "Ocurrió un error, intentelo más tarde. Gracias";
              
            }
        }
    }
}