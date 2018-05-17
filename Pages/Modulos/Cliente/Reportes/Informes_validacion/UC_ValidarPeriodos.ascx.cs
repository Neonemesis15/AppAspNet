using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucky.Data;
using System.Data;

namespace SIGE.Pages.Modulos.Cliente.Reportes.Informes_validacion
{
    public partial class UC_ValidarPeriodos : System.Web.UI.UserControl
    {
        private static string año;
        private static string mes;
        private static string periodo;

        Conexion oCoon = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void SetValidacion(int servicio, string canal, int cliente,int reporte, string _año, string _mes, string _periodo)
        {
            año = _año;
            mes = _mes;
            periodo = _periodo;

            DataTable dt = null;

            //falta un detalle de parametros
            dt = oCoon.ejecutarDataTable("UP_WEBXPLORA_PLA_OBTENER_REPORTPLANNING_BY_REPORTID_AND_ANO_MES_AND_PERIODO_NEW",servicio,canal,cliente, reporte, año, mes, periodo);

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    lbl_periodo.Text = "Periodo : " + periodo + ", Mes: " + mes + ", Año: " + año;
                    lbl_periodo.ForeColor = System.Drawing.Color.Green;
                    bool valid_analist = Convert.ToBoolean(dt.Rows[0]["ReportsPlanning_ValidacionAnalista"]);

                    if (valid_analist == true)
                    {
                        rb_valido.Checked = true;
                        rb_invalido.Checked = false;
                    }
                    else
                    {
                        rb_invalido.Checked = true;
                        rb_valido.Checked = false;
                    }
                }
            }
        }

        protected void rb_valido_CheckedChanged(object sender, EventArgs e)
        {
            lbl_msj_validar.Text = "¿ Esta seguro que desea validar?";
            lbl_msj_validar.ForeColor = System.Drawing.Color.Green;
            ModalPopupExtender_ValidationAnalyst.Show();
        }

        protected void rb_invalido_CheckedChanged(object sender, EventArgs e)
        {
            lbl_msj_validar.Text = "¿ Esta seguro que desea invalidar?";
            lbl_msj_validar.ForeColor = System.Drawing.Color.Red;
            ModalPopupExtender_ValidationAnalyst.Show();
        }
        protected void btn_aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                int iidcompany = Convert.ToInt32(this.Session["companyid"].ToString());
                string sidcanal = this.Session["Canal"].ToString();
                int Report = Convert.ToInt32(this.Session["Reporte"]);
                int iservicio = Convert.ToInt32(this.Session["Service"]);

                bool valido;
                valido = rb_valido.Checked;

                oCoon.ejecutarDataReader("UP_WEBXPLORA_CLIE_V2_REPORT_PLANNING_GUARDAR_VALIDACION", iservicio, sidcanal, iidcompany, Report, año, mes, periodo, valido, Session["sUser"].ToString(), DateTime.Now);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            if (rb_valido.Checked == true)
            {
                rb_valido.Checked = false;
                rb_invalido.Checked = true;
            }
            else
            {
                rb_valido.Checked = true;
                rb_invalido.Checked = false;
            }
        }
    }
}