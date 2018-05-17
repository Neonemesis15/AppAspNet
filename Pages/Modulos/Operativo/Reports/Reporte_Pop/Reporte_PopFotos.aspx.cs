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

namespace SIGE.Pages.Modulos.Operativo.Reports.Reporte_Pop
{
    public partial class Reporte_PopFotos : System.Web.UI.Page
    {
        private int compañia;
        private string pais;
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Facd_ProcAdmin = new Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();

        Conexion oCon = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                CargarCombo_Channel();
            }
        }       
        protected void CargarCombo_Channel()
        {
            DataTable dt = null;
            Conexion Ocoon = new Conexion();

            compañia = Convert.ToInt32(this.Session["companyid"]);
            pais = this.Session["scountry"].ToString();


            dt = Ocoon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_CHANNEL", pais, compañia);

            cmbcanal.DataSource = dt;
            cmbcanal.DataValueField = "cod_Channel";
            cmbcanal.DataTextField = "Channel_Name";
            cmbcanal.DataBind();
            cmbcanal.Items.Insert(0, new ListItem("---Seleccione---", "0"));

        }
        protected void cmbcanal_SelectedIndexChanged(object sender, EventArgs e)
        {
 
            string channel = cmbcanal.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);
            DataTable dt = oCon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PLANNING", channel, compañia);

            cmbplanning.Items.Clear();

            cmbplanning.DataSource = dt;
            cmbplanning.DataValueField = "id_planning";
            cmbplanning.DataTextField = "Planning_Name";
            cmbplanning.DataBind();

            cmbplanning.Items.Insert(0, new ListItem("---Seleccione---", "0"));
            cmbplanning.Enabled = true;

        }

        protected void cmbplanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = null;
            string sidplanning = cmbplanning.SelectedValue;
            compañia = Convert.ToInt32(this.Session["companyid"]);

            if (cmbplanning.SelectedIndex != 0)
            {
                dt = oCon.ejecutarDataTable("UP_WEBXPLORA_OPE_COMBO_PERSON", sidplanning);

                //------llamado al metodo cargar categoria de producto
                cargarCombo_Oficina();
                cargarCombo_NodeComercial(sidplanning);
               
                //----------------------------------------------------
            }
            else
            {
                cmbOficina.Items.Clear();
                cmbOficina.Enabled = false;
            }
        }
        protected void cargarCombo_Oficina()
        {
            try
            {

                if (this.Session["companyid"] != null)
                {
                    compañia = Convert.ToInt32(this.Session["companyid"]);
                    DataTable dtofi = oCon.ejecutarDataTable("UP_WEBXPLORA_AD_OBTENEROFICINAS", compañia);

                    if (dtofi.Rows.Count > 0)
                    {
                        cmbOficina.Enabled = true;
                        cmbOficina.DataSource = dtofi;
                        cmbOficina.DataTextField = "Name_Oficina";
                        cmbOficina.DataValueField = "cod_Oficina";
                        cmbOficina.DataBind();

                        cmbOficina.Items.Insert(0, new ListItem("---Todas---", "0"));
                    }
                }
                else
                {
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void cargarCombo_NodeComercial(string sid_planning)
        {
            try
            {
                cmbNodeComercial.Items.Clear();
                Facade_Procesos_Administrativos.ENodeComercial[] oListNodeComercial = Facd_ProcAdmin.Get_NodeComercialBy_idPlanning(cmbplanning.SelectedValue);

                if (oListNodeComercial.Length > 0)
                {
                    cmbNodeComercial.Enabled = true;
                    cmbNodeComercial.DataSource = oListNodeComercial;
                    cmbNodeComercial.DataTextField = "commercialNodeName";
                    cmbNodeComercial.DataValueField = "NodeCommercial";
                    cmbNodeComercial.DataBind();

                    cmbNodeComercial.Items.Insert(0, new ListItem("---Todas---", "0"));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            llenarDataList();
        }
        public void llenarDataList()
        {
            try
            {
                string sidplanning = cmbplanning.SelectedValue;
                int icod_oficina = Convert.ToInt32(cmbOficina.SelectedValue);
                int iidNodeComercial = Convert.ToInt32(cmbNodeComercial.SelectedValue);

                DateTime dfecha_inicio;
                DateTime dfecha_fin;

                if (txt_fecha_inicio.SelectedDate.ToString() == "" || txt_fecha_inicio.SelectedDate.ToString() == "0" || txt_fecha_inicio.SelectedDate == null)
                    dfecha_inicio = txt_fecha_inicio.MinDate;
                else dfecha_inicio = txt_fecha_inicio.SelectedDate.Value;


                if (txt_fecha_fin.SelectedDate.ToString() == "" || txt_fecha_fin.SelectedDate.ToString() == "0" || txt_fecha_fin.SelectedDate == null)
                    dfecha_fin = txt_fecha_fin.MaxDate;
                else dfecha_fin = txt_fecha_fin.SelectedDate.Value;

                if (DateTime.Compare(dfecha_inicio, dfecha_fin) == 1)
                {
                    lblmensaje.Visible = true;
                    lblmensaje.Text = "La fecha de inicio debe ser menor o igual a la fecha fin";
                    lblmensaje.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    DataTable dtPop;

                    dtPop = oCon.ejecutarDataTable("UP_WEBXPLORA_OPE_CONSULTA_REPORTE_FOTOGRAFICO_POP",sidplanning,icod_oficina,iidNodeComercial,dfecha_inicio,dfecha_fin);

                    DataList_Pop.DataSource = dtPop;
                    DataList_Pop.DataBind();

                    lblmensaje.Text = "Se encontro " + dtPop.Rows.Count + " resultados";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblmensaje.Text = "";
                if (cmbplanning.SelectedIndex > 0)
                    lblmensaje.Text = "Ocurrió un error inesperado, Por favor intentelo más tarde o comuníquese con el área de Tecnología de Información.";
            }
        }


        protected void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            DataListItem item;
            for (int i = 0; i < DataList_Pop.Items.Count; i++)
            {
                item = DataList_Pop.Items[i];

                //if (ListItemType.Item == item.ItemType)
                //{
                CheckBox cb_validar = item.FindControl("cb_validar") as CheckBox;
                if (cb_all.Checked == true)
                {
                    cb_all.Text = "Deselecionar todo";
                    cb_validar.Checked = true;
                }
                else
                {
                    cb_validar.Checked = false;
                    cb_all.Text = "Selecionar todo";
                }

            }
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            string dataValidas = String.Empty;
            string dataInvalidas = String.Empty;

            DataListItem item;
            for (int i = 0; i < DataList_Pop.Items.Count; i++)
            {
                item = DataList_Pop.Items[i];

                //if (ListItemType.Item == item.ItemType)
                //{
                CheckBox cb_validar = item.FindControl("cb_validar") as CheckBox;
                Label id_rpPromocion = item.FindControl("id_rpPromocion") as Label;

                if (cb_validar.Checked == true)
                {
                    dataValidas = dataValidas + id_rpPromocion.Text + ",";
                }
                else
                {
                    dataInvalidas = dataInvalidas + id_rpPromocion.Text + ",";
                }

                oCon.ejecutarDataReader("UP_WEBXPLORA_OPE_ACTUALIZAR_REPORTE_POP_DETALLE_VALIDADO", dataValidas, dataInvalidas, this.Session["sUser"], DateTime.Now);
            }
        }
    }
}