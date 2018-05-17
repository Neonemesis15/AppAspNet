using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Lucky.Business.Common.Application;
using Lucky.Business.Common.Security;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Data.Common.Security;
using Lucky.Entity.Common.Application;
using Lucky.Entity.Common.Security;
using Lucky.Entity.Common.Application.Security;
using Lucky.CFG.Util;
using Lucky.CFG.Messenger;
using Lucky.CFG.Tools;

namespace SIGE.Pages.Modulos.Supervision
{
    public partial class ini_Supervisor : System.Web.UI.Page
    {
        #region Zona de Declaración de Variables Generales
        Conexion oConn = new Conexion();
        private Competition__Information oCompetition__Information = new Competition__Information();
        private Photographs_Service oPhotographs_Service = new Photographs_Service();
        private PointOfSale_PlanningOper PointOfSale_PlanningOper = new PointOfSale_PlanningOper();
        private Products_PlanningOpe Products_PlanningOpe = new Products_PlanningOpe();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Facade_Proceso_Operativo.Facade_Proceso_Operativo Get_Operativo = new SIGE.Facade_Proceso_Operativo.Facade_Proceso_Operativo();
        Facade_Proceso_Supervisor.Facade_Proceso_Supervisor Get_Supervisión = new SIGE.Facade_Proceso_Supervisor.Facade_Proceso_Supervisor();



        int budget = 0;
        int planning = 0;
        int SelOperativos = 0;
        int SelOperativosProd = 0;
        int SelPDV = 0;
        int SelPRODUCTO = 0;
        int Duplicados = 0;
        int DuplicadosProd = 0;
        int iOperativo = 0;
        int QueryPDV = 0;
        int ValidaSignaPDVOpe = 0;
        int ValidaSignaPDV = 0;
        string planningName = "";
        string Query = "";
        string QueryJoin = "";
        string sTxtEmail = "";


        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();

        #endregion

        #region Funciones Comunes

        private void Mensajes()
        {
            LblAlert.Text = this.Session["Encabezado"].ToString().Trim();
            LblFaltantes.Text = this.Session["alertas"].ToString().Trim();
            ModalPopupAlertas.Show();
        }
        private void llenargrilla()
        {
            dt = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARPLANNINGSUPERVISOR", this.Session["sUser"].ToString());
            if (dt.Rows.Count == 0)
            {
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Asignación de Servicio";
                this.Session["alertas"] = "Sr. Usuario. Actualmente no tiene ninguna asignación de servicio. Por favor intentelo más tarde";
                Mensajes();
                MenuSupervisor.Items[0].Enabled = false;
                MenuSupervisor.Items[1].Enabled = true;
            }
            else
            {
                //se llena grilla plannings
                gvplanning.DataSource = dt;
                gvplanning.DataBind();


            }
            dt = null;
        }
        private void llenargrillaAnalisis()
        {

            dt2 = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARPLANNINGSUPERVISORANALISIS", this.Session["sUser"].ToString());
            if (dt2.Rows.Count == 0)
            {
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Asignación de Servicio";
                this.Session["alertas"] = "Sr. Usuario. Actualmente no existen Campañas creadas para realizar auditoria. Por favor intentelo más adelante";
                Mensajes();
                MenuSupervisor.Items[0].Enabled = true;
                MenuSupervisor.Items[1].Enabled = false;

            }
            else
            {
                //se llena grilla plannings

                gvplanningAnalisis.DataSource = dt2;
                gvplanningAnalisis.DataBind();
            }
            dt2 = null;


        }

        private void llenaAsignacionPDV()
        {
            DataTable dt = Get_Supervisión.Get_ConsultarAsignacionPDV(Convert.ToInt32(gvplanning.SelectedRow.Cells[1].Text), Convert.ToInt32(LstBoxSelBuscarOperativos.SelectedItem.Value));
            GvBuscarAsignados.DataSource = dt;
            GvBuscarAsignados.DataBind();
        }
        private void llenaAsignacionPDVAnalisis()
        {
            RbtnLstPDVAnalisis.Items.Clear();

            DataTable dt = null;

            for (int i = 0; i <= ChkLstOpeAnalisis.Items.Count - 1; i++)
            {
                if (ChkLstOpeAnalisis.Items[i].Selected == true)
                {
                    dt = Get_Supervisión.Get_ConsultarAsignacionPDV(Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text), Convert.ToInt32(ChkLstOpeAnalisis.Items[i].Value));
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int j = 0; j <= dt.Rows.Count - 1; j++)
                            {
                                RbtnLstPDVAnalisis.Items.Insert(0, new ListItem(dt.Rows[j][1].ToString(), dt.Rows[j][0].ToString()));
                                RbtnLstPDVAnalisis.Enabled = true;
                                ChkAllPDV.Visible = true;
                                ChkAllPDV.Checked = false;
                            }
                        }
                        else
                        {
                            ChkAllPDV.Visible = false;
                            ChkAllPDV.Checked = false;
                        }
                    }
                }


            }
        }
        private void llenaAsignacionPRODUCTO()
        {
            DataTable dt = Get_Supervisión.Get_ConsultarAsignacionPRODUCTO(Convert.ToInt32(gvplanning.SelectedRow.Cells[1].Text), Convert.ToInt32(LstBoxSelBuscarOperativosProd.SelectedItem.Value));
            GvBuscarAsignadosProd.DataSource = dt;
            GvBuscarAsignadosProd.DataBind();
        }
        private void llenaAsignacionPRODUCTOAnalisis()
        {
            ChkLstProdAnalisis.Items.Clear();
            DataTable dt = null;

            for (int i = 0; i <= ChkLstOpeAnalisis.Items.Count - 1; i++)
            {
                if (ChkLstOpeAnalisis.Items[i].Selected == true)
                {
                    for (int j = 0; j <= RbtnLstPDVAnalisis.Items.Count - 1; j++)
                    {
                        if (RbtnLstPDVAnalisis.Items[j].Selected == true)
                        {
                            dt = Get_Supervisión.Get_ConsultarAsignacionPRODUCTOPDV_XINFORME(Convert.ToInt32(MenuDinamico.SelectedValue), Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text), Convert.ToInt32(ChkLstOpeAnalisis.Items[i].Value), Convert.ToInt32(RbtnLstPDVAnalisis.Items[j].Value));
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    for (int k = 0; k <= dt.Rows.Count - 1; k++)
                                    {
                                        ChkLstProdAnalisis.Items.Insert(0, new ListItem(dt.Rows[k][1].ToString(), dt.Rows[k][0].ToString()));
                                        ChkLstProdAnalisis.Enabled = true;
                                        ChkAllPRODUCTO.Visible = true;
                                        ChkAllPRODUCTO.Checked = false;
                                        ChkAllPRODUCTO.Enabled = true;
                                    }
                                }
                                else
                                {
                                    ChkAllPRODUCTO.Visible = false;
                                    ChkAllPRODUCTO.Checked = false;
                                    ChkAllPRODUCTO.Enabled = false;
                                    Alertas.CssClass = "MensajesSupervisor";
                                    this.Session["Encabezado"] = "Sr. Usuario";
                                    this.Session["alertas"] = "Al punto de venta : " + RbtnLstPDVAnalisis.Items[j].Text + " no se le ha ingresado información de " + MenuDinamico.SelectedItem.Text;
                                    Mensajes();
                                    return;
                                }
                            }
                            j = RbtnLstPDVAnalisis.Items.Count;
                        }
                    }
                }
            }
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string sUser = this.Session["sUser"].ToString();
                    string sPassw = this.Session["sPassw"].ToString();
                    usersession.Text = sUser;

                    if (sUser != null && sPassw != null)
                    {
                        TxtSolicitante.Text = this.Session["smail"].ToString();
                        MenuSupervisor.Items[0].Enabled = false;
                        MenuSupervisor.Items[1].Enabled = true;
                        llenargrilla();
                        TbCSupervisor.Visible = false;

                        Pasignaciones.Style.Value = "display:block;";
                        PAnalisis.Style.Value = "display:none;";

                        panelAsignaPDV.Style.Value = "display:none;";
                        panelConsultaPDV.Style.Value = "display:none;";
                        panelAsignaProductos.Style.Value = "display:none;";
                        panelConsultaPRODUCTOS.Style.Value = "display:none;";

                        PanelVistaAnalisis.Style.Value = "display:none;";
                        panelcontenido.Style.Value = "display:none;";
                    }
                }
                catch (Exception ex)
                {
                    Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                    this.Session.Abandon();
                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                }
            }
        }

        protected void ImgCloseSession_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Get_Administrativo.Get_Delete_Sesion_User(this.Session["sUser"].ToString());
                this.Session.Abandon();
                Response.Redirect("~/login.aspx", true);
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/login.aspx", true);
            }
        }

        protected void MenuSupervisor_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (MenuSupervisor.Items[0].Selected == true)
            {
                MenuSupervisor.Items[0].Enabled = false;
                MenuSupervisor.Items[1].Enabled = true;

                Pasignaciones.Style.Value = "display:block;";
                PAnalisis.Style.Value = "display:none;";

                panelAsignaPDV.Style.Value = "display:none;";
                panelConsultaPDV.Style.Value = "display:none;";
                panelAsignaProductos.Style.Value = "display:none;";
                panelConsultaPRODUCTOS.Style.Value = "display:none;";

                NewAsign.Visible = true;
                BtnAsignarPDV.Visible = false;
                NewAsignProd.Visible = true;
                BtnAsignarProd.Visible = false;
                LstBoxSelOperativos.SelectedIndex = -1;
                LstBoxPdv.SelectedIndex = -1;
                GvAsignados.DataBind();
                LstBoxSelOperativoProd.SelectedIndex = -1;
                LstBoxProd.SelectedIndex = -1;
                GvAsignadosProd.DataBind();

                PanelVistaAnalisis.Style.Value = "display:none;";
                panelcontenido.Style.Value = "display:none;";

                Lbltitulo.Text = "";
                TxtFechaEjec.Text = "";
                TxtFechaFinEjec.Text = "";
                ChkAllPDV.Visible = false;
                ChkAllPRODUCTO.Visible = false;
                ChkLstOpeAnalisis.Items.Clear();
                RbtnLstPDVAnalisis.Items.Clear();
                ChkLstProdAnalisis.Items.Clear();
                GvDatosAnalisis.Visible = false;


                MenuDinamico.Items.Clear();

            }
            else
            {
                MenuSupervisor.Items[0].Enabled = true;
                MenuSupervisor.Items[1].Enabled = false;
                Pasignaciones.Style.Value = "display:none;";
                PAnalisis.Style.Value = "display:block;";
                llenargrillaAnalisis();
            }
        }

        protected void gvplanning_SelectedIndexChanged(object sender, EventArgs e)
        {
            TbCSupervisor.Visible = true;

            budget = Convert.ToInt32(gvplanning.SelectedRow.Cells[5].Text);
            planning = Convert.ToInt32(gvplanning.SelectedRow.Cells[1].Text);
            this.Session["Planning"] = planning;
            planningName = gvplanning.SelectedRow.Cells[2].Text;
            this.Session["alertas"] = "El Planning " + gvplanning.SelectedRow.Cells[2].Text;
            this.Session["PlanningName"] = planningName;
            this.Session["budget"] = budget;
            LstBoxSelOperativos.Items.Clear();
            LstBoxPdv.Items.Clear();

            LstBoxSelOperativoProd.Items.Clear();
            LstBoxProd.Items.Clear();

            LstBoxSelBuscarOperativos.Items.Clear();
            LstBoxSelBuscarOperativosProd.Items.Clear();

            //se llena combo supervisores    
            dt = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARSTAFF", Convert.ToInt32(this.Session["personid"]), Convert.ToInt32(gvplanning.SelectedRow.Cells[1].Text));

            //se llena combo puntos de venta
            dt2 = oConn.ejecutarDataTable("UP_WEBXPLORA_PLA_CONSULTARPDVPLANNING", planning);

            //se llena combo productos
            dt3 = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARPRODUCTOS", planning);

            if (dt.Rows.Count == 0)
            {
                this.Session["Encabezado"] = "Sr Usuario:";
                this.Session["alertas"] = this.Session["alertas"] + " No tiene personal operativo asignado. ";
                LblSeloperativo.Visible = false;
            }
            else
            {
                LstBoxSelOperativos.DataSource = dt;
                LstBoxSelOperativos.DataTextField = "Nombre";
                LstBoxSelOperativos.DataValueField = "Person_id";
                LstBoxSelOperativos.DataBind();
                LblSeloperativo.Visible = true;

                LstBoxSelOperativoProd.DataSource = dt;
                LstBoxSelOperativoProd.DataTextField = "Nombre";
                LstBoxSelOperativoProd.DataValueField = "Person_id";
                LstBoxSelOperativoProd.DataBind();
                LblSeloperativoProd.Visible = true;

                LstBoxSelBuscarOperativos.DataSource = dt;
                LstBoxSelBuscarOperativos.DataTextField = "Nombre";
                LstBoxSelBuscarOperativos.DataValueField = "Person_id";
                LstBoxSelBuscarOperativos.DataBind();
                LblSelBuscaroperativo.Visible = true;

                LstBoxSelBuscarOperativosProd.DataSource = dt;
                LstBoxSelBuscarOperativosProd.DataTextField = "Nombre";
                LstBoxSelBuscarOperativosProd.DataValueField = "Person_id";
                LstBoxSelBuscarOperativosProd.DataBind();
                LblSelBuscaroperativoProd.Visible = true;

            }

            if (dt2.Rows.Count == 0)
            {
                this.Session["Encabezado"] = "Sr Usuario:";
                this.Session["alertas"] = this.Session["alertas"] + " Fue creado sin Puntos de Venta. ";
                LblSelpdv.Visible = false;
            }
            else
            {
                //se llena combo puntos de venta
                LstBoxPdv.DataSource = dt2;
                LstBoxPdv.DataTextField = "Nombre";
                LstBoxPdv.DataValueField = "Codigo";
                LstBoxPdv.DataBind();
                LblSelpdv.Visible = true;
            }

            if (dt3.Rows.Count == 0)
            {
                this.Session["Encabezado"] = "Sr Usuario:";
                this.Session["alertas"] = this.Session["alertas"] + " Fue creado sin productos. ";
                LblSelProd.Visible = false;
            }
            else
            {
                //se llena combo productos
                LstBoxProd.DataSource = dt3;
                LstBoxProd.DataTextField = "Nombre";
                LstBoxProd.DataValueField = "Codigo";
                LstBoxProd.DataBind();
                LblSelProd.Visible = true;
            }

            if (this.Session["alertas"].ToString() != "El Planning " + gvplanning.SelectedRow.Cells[2].Text)
            {
                Alertas.CssClass = "MensajesSupervisor";
                Mensajes();
            }
            dt = null;
            dt2 = null;
            dt3 = null;

            DataTable dtAsignadosTemp = new DataTable();
            dtAsignadosTemp.Columns.Add("Cod_Operativo", typeof(Int32));
            dtAsignadosTemp.Columns.Add("Nombre_Operativo", typeof(String));
            dtAsignadosTemp.Columns.Add("Cod_PDV", typeof(String));
            dtAsignadosTemp.Columns.Add("Punto de Venta", typeof(String));
            this.Session["dtasignadosTemp"] = dtAsignadosTemp;

            DataTable dtAsignadosProdTemp = new DataTable();
            dtAsignadosProdTemp.Columns.Add("Cod_Operativo", typeof(Int32));
            dtAsignadosProdTemp.Columns.Add("Nombre_Operativo", typeof(String));
            dtAsignadosProdTemp.Columns.Add("Cod_Prod", typeof(String));
            dtAsignadosProdTemp.Columns.Add("Producto", typeof(String));
            this.Session["dtasignadosProdTemp"] = dtAsignadosProdTemp;

        }
        protected void LstBoxSelBuscarOperativos_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaAsignacionPDV();
        }
        protected void LstBoxSelBuscarOperativosProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaAsignacionPRODUCTO();

        }


        protected void BtnMasAsing_Click(object sender, EventArgs e)
        {

            DataTable dtAsignados = (DataTable)this.Session["dtasignadosTemp"];
            for (int j = 0; j <= LstBoxSelOperativos.Items.Count - 1; j++)
            {
                if (LstBoxSelOperativos.Items[j].Selected == true)
                {
                    SelOperativos = 1;
                    for (int i = 0; i <= LstBoxPdv.Items.Count - 1; i++)
                    {
                        if (LstBoxPdv.Items[i].Selected == true)
                        {
                            // se deja comentariado codigo para no generar errores debido a cambio en planning. Ing. Mauricio Ortiz 08/09/2010
                            //DataTable dt = Get_Supervisión.Get_PuntoVentaDuplicado(Convert.ToInt32(LstBoxPdv.Items[i].Value), Convert.ToInt32(LstBoxSelOperativos.Items[j].Value), Convert.ToInt32(gvplanning.SelectedRow.Cells[1].Text));
                            //if (dt != null)
                            //{
                            //    if (dt.Rows.Count > 0)
                            //    {
                            //        Alertas.CssClass = "MensajesSupervisor";
                            //        this.Session["Encabezado"] = "Asignación de Servicio";
                            //        this.Session["alertas"] = "Sr. Usuario. El punto de venta : " + LstBoxPdv.Items[i].Text + " ya se encuentrá asignado al operativo : " + LstBoxSelOperativos.Items[j].Text;
                            //        Mensajes();
                            //        return;
                            //    }
                            //    else
                            //    {
                            //        SelPDV = 1;
                            //        try
                            //        {
                            //            DataRow dr = dtAsignados.NewRow();
                            //            dr["Cod_Operativo"] = Convert.ToInt32(LstBoxSelOperativos.Items[j].Value);
                            //            dr["Nombre_Operativo"] = LstBoxSelOperativos.Items[j].Text;
                            //            dr["Cod_PDV"] = Convert.ToInt32(LstBoxPdv.Items[i].Value);
                            //            dr["Punto de Venta"] = LstBoxPdv.Items[i].Text;
                            //            dtAsignados.Rows.Add(dr);
                            //            this.Session["dtasignadosTemp"] = dtAsignados;
                            //        }
                            //        catch
                            //        {
                            //            Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                            //            this.Session.Abandon();
                            //            Response.Redirect("~/err_mensaje_seccion.aspx", true);
                            //        }
                            //    }
                            //}
                        }
                    }
                }
            }

            if (SelOperativos == 0)
            {
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Asignación de Servicio";
                this.Session["alertas"] = "Sr. Usuario. Debe seleccionar un Operativo de la lista.";
                Mensajes();
                return;
            }

            if (SelPDV == 0)
            {
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Asignación de Servicio";
                this.Session["alertas"] = "Sr. Usuario. Debe seleccionar por lo menos un punto de venta de la lista.";
                Mensajes();
                return;
            }

            for (int i = 0; i <= dtAsignados.Rows.Count - 1; i++)
            {
                string codoper = dtAsignados.Rows[i]["Cod_Operativo"].ToString();
                string codpdv = dtAsignados.Rows[i]["Cod_PDV"].ToString();
                for (int j = 0; j <= dtAsignados.Rows.Count - 1; j++)
                {
                    if ((dtAsignados.Rows[j]["Cod_Operativo"].ToString() == codoper) && (dtAsignados.Rows[j]["Cod_PDV"].ToString() == codpdv))
                    {
                        Duplicados = Duplicados + 1;
                    }
                    if (Duplicados >= 2)
                    {
                        dtAsignados.Rows[j].Delete();
                    }
                }
                Duplicados = 0;
            }

            this.Session["dtasignadosTemp"] = dtAsignados;
            LstBoxSelOperativos.SelectedIndex = -1;
            LstBoxPdv.SelectedIndex = -1;
            GvAsignados.DataSource = (DataTable)this.Session["dtasignadosTemp"];
            GvAsignados.DataBind();
        }
        protected void BtnMasAsingProd_Click(object sender, EventArgs e)
        {
            DataTable dtAsignadosProd = (DataTable)this.Session["dtasignadosProdTemp"];
            for (int j = 0; j <= LstBoxSelOperativoProd.Items.Count - 1; j++)
            {
                if (LstBoxSelOperativoProd.Items[j].Selected == true)
                {
                    SelOperativosProd = 1;
                    for (int i = 0; i <= LstBoxProd.Items.Count - 1; i++)
                    {
                        if (LstBoxProd.Items[i].Selected == true)
                        {
                            DataTable dt = Get_Supervisión.Get_ProductoDuplicado(Convert.ToInt32(LstBoxProd.Items[i].Value), Convert.ToInt32(LstBoxSelOperativoProd.Items[j].Value), Convert.ToInt32(gvplanning.SelectedRow.Cells[1].Text));
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    Alertas.CssClass = "MensajesSupervisor";
                                    this.Session["Encabezado"] = "Asignación de Servicio";
                                    this.Session["alertas"] = "Sr. Usuario. El producto : " + LstBoxProd.Items[i].Text + " ya se encuentrá asignado al operativo : " + LstBoxSelOperativoProd.Items[j].Text;
                                    Mensajes();
                                    return;
                                }
                                else
                                {

                                    SelPRODUCTO = 1;
                                    try
                                    {
                                        DataRow dr = dtAsignadosProd.NewRow();
                                        dr["Cod_Operativo"] = Convert.ToInt32(LstBoxSelOperativoProd.Items[j].Value);
                                        dr["Nombre_Operativo"] = LstBoxSelOperativoProd.Items[j].Text;
                                        dr["Cod_Prod"] = Convert.ToInt32(LstBoxProd.Items[i].Value);
                                        dr["Producto"] = LstBoxProd.Items[i].Text;
                                        dtAsignadosProd.Rows.Add(dr);
                                        this.Session["dtasignadosProdTemp"] = dtAsignadosProd;
                                    }
                                    catch
                                    {
                                        Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                                        this.Session.Abandon();
                                        Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                    }
                                }
                            }
                        }
                    }
                }
            }


            if (SelOperativosProd == 0)
            {
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Asignación de Servicio";
                this.Session["alertas"] = "Sr. Usuario. Debe seleccionar un Operativo de la lista.";
                Mensajes();
                return;
            }

            if (SelPRODUCTO == 0)
            {
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Asignación de Servicio";
                this.Session["alertas"] = "Sr. Usuario. Debe seleccionar por lo menos un producto de la lista.";
                Mensajes();
                return;
            }

            for (int i = 0; i <= dtAsignadosProd.Rows.Count - 1; i++)
            {
                string codoper = dtAsignadosProd.Rows[i]["Cod_Operativo"].ToString();
                string codproducto = dtAsignadosProd.Rows[i]["Cod_Prod"].ToString();
                for (int j = 0; j <= dtAsignadosProd.Rows.Count - 1; j++)
                {
                    if ((dtAsignadosProd.Rows[j]["Cod_Operativo"].ToString() == codoper) && (dtAsignadosProd.Rows[j]["Cod_Prod"].ToString() == codproducto))
                    {
                        DuplicadosProd = DuplicadosProd + 1;
                    }
                    if (DuplicadosProd >= 2)
                    {
                        dtAsignadosProd.Rows[j].Delete();
                    }
                }
                DuplicadosProd = 0;
            }

            this.Session["dtasignadosProdTemp"] = dtAsignadosProd;
            LstBoxSelOperativoProd.SelectedIndex = -1;
            LstBoxProd.SelectedIndex = -1;
            GvAsignadosProd.DataSource = (DataTable)this.Session["dtasignadosProdTemp"];
            GvAsignadosProd.DataBind();

        }

        protected void GvAsignados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvAsignados.PageIndex = e.NewPageIndex;
            GvAsignados.DataSource = (DataTable)this.Session["dtasignadosTemp"];
            GvAsignados.DataBind();
        }
        protected void GvAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GvAsignados.SelectedRow;
            DataTable dtdel = (DataTable)this.Session["dtasignadosTemp"];
            dtdel.Rows[row.RowIndex].Delete();
            this.Session["dtasignadosTemp"] = dtdel;

            GvAsignados.DataSource = (DataTable)this.Session["dtasignadosTemp"];
            GvAsignados.DataBind();


        }

        protected void GvAsignadosProd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvAsignadosProd.PageIndex = e.NewPageIndex;
            GvAsignadosProd.DataSource = (DataTable)this.Session["dtasignadosProdTemp"];
            GvAsignadosProd.DataBind();
        }
        protected void GvBuscarAsignados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvBuscarAsignados.PageIndex = e.NewPageIndex;
            llenaAsignacionPDV();
        }
        protected void GvBuscarAsignadosProd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvBuscarAsignadosProd.PageIndex = e.NewPageIndex;
            llenaAsignacionPRODUCTO();
        }

        protected void GvAsignadosProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GvAsignadosProd.SelectedRow;
            DataTable dtdelp = (DataTable)this.Session["dtasignadosProdTemp"];
            dtdelp.Rows[row.RowIndex].Delete();
            this.Session["dtasignadosProdTemp"] = dtdelp;

            GvAsignadosProd.DataSource = (DataTable)this.Session["dtasignadosProdTemp"];
            GvAsignadosProd.DataBind();

        }
        protected void GvBuscarAsignados_SelectedIndexChanged(object sender, EventArgs e)
        {
           // debido a cambios realizados en modulo planning se deja comentariada esta linea para evitar errores de compilacion . Ing. Mauricio Ortiz 31/01/2011
           // EPointOfSale_PlanningOper ActualizaAsignPDV = PointOfSale_PlanningOper.ActualizarAsignPDVaOperativo(Convert.ToInt32(GvBuscarAsignados.SelectedRow.Cells[1].Text), gvplanning.SelectedRow.Cells[1].Text, Convert.ToInt32(LstBoxSelBuscarOperativos.SelectedValue), false, Convert.ToString(this.Session["sUser"]), DateTime.Now);
           // llenaAsignacionPDV();
        }
        protected void GvBuscarAsignadosProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            EProducts_PlanningOpe ActualizaAsignPRODUCTO = Products_PlanningOpe.ActualizarAsignPRODUCTOaOperativo(Convert.ToInt32(GvBuscarAsignadosProd.SelectedRow.Cells[1].Text), gvplanning.SelectedRow.Cells[1].Text, Convert.ToInt32(LstBoxSelBuscarOperativosProd.SelectedValue), false, Convert.ToString(this.Session["sUser"]), DateTime.Now);
            llenaAsignacionPRODUCTO();
        }

        protected void BtnAsignarPDV_Click(object sender, EventArgs e)
        {
            if (GvAsignados.PageIndex != 0)
            {
                GvAsignados.PageIndex = 0;
                GvAsignados.DataSource = (DataTable)this.Session["dtasignadosTemp"];
                GvAsignados.DataBind();
            }
            if (GvAsignados.Rows.Count > 0)
            {
                for (int j = 0; j <= GvAsignados.PageCount; j++)
                {
                    for (int i = 0; i <= GvAsignados.Rows.Count - 1; i++)
                    {
                        // se deja comentariado codigo para no generar errores debido a cambio en planning. Ing. Mauricio Ortiz 08/09/2010
                        //DataTable dt = Get_Supervisión.Get_PuntoVentaDuplicado(Convert.ToInt32(GvAsignados.Rows[i].Cells[3].Text), Convert.ToInt32(GvAsignados.Rows[i].Cells[1].Text), Convert.ToInt32(gvplanning.SelectedRow.Cells[1].Text));
                        //if (dt != null)
                        //{
                        //    if (dt.Rows.Count == 0)
                        //    {    
                        //        // 06/09/2010 Ing. Mauricio Ortiz
                        //        //se coloca fecha actual en fecha inicio y fin para no generar error en la aplicacion.
                        //        // esto ya no se hace en este modulo , se hace en el modulo planning 
                        //        EPointOfSale_PlanningOper RegistrarPointOfSale_PlanningOper = PointOfSale_PlanningOper.RegistrarAsignPDVaOperativo(Convert.ToInt32(GvAsignados.Rows[i].Cells[3].Text),gvplanning.SelectedRow.Cells[1].Text, Convert.ToInt32(GvAsignados.Rows[i].Cells[1].Text),DateTime.Now,DateTime.Now, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                        //    }
                        //}
                    }
                    GvAsignados.PageIndex = j + 1;
                    GvAsignados.DataSource = (DataTable)this.Session["dtasignadosTemp"];
                    GvAsignados.DataBind();
                }
                DataTable dtAsignadosTemp = new DataTable();
                dtAsignadosTemp.Columns.Add("Cod_Operativo", typeof(Int32));
                dtAsignadosTemp.Columns.Add("Nombre_Operativo", typeof(String));
                dtAsignadosTemp.Columns.Add("Cod_PDV", typeof(String));
                dtAsignadosTemp.Columns.Add("Punto de Venta", typeof(String));
                this.Session["dtasignadosTemp"] = dtAsignadosTemp;

                GvAsignados.DataSource = (DataTable)this.Session["dtasignadosTemp"];
                GvAsignados.DataBind();
                Alertas.CssClass = "MensajesSupConfirm";
                this.Session["Encabezado"] = "Asignación de Puntos de Venta";
                this.Session["alertas"] = "Sr. Usuario. Asignación de Puntos de venta realizada con éxito.";
                Mensajes();
            }
            else
            {
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Asignación de Puntos de Venta";
                this.Session["alertas"] = "Sr. Usuario. no ha seleccionado para realizar la asignación.";
                Mensajes();
            }

            for (int i = 0; i <= LstBoxSelOperativos.Items.Count - 1; i++)
            {
                DataTable dt = Get_Supervisión.Get_ConsultarAsignacionPDV(Convert.ToInt32(gvplanning.SelectedRow.Cells[1].Text), Convert.ToInt32(LstBoxSelOperativos.Items[i].Value));

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ValidaSignaPDVOpe = 1;

                    }
                    else
                    {
                        ValidaSignaPDVOpe = 0;
                        i = LstBoxSelOperativos.Items.Count;
                    }
                }
            }
            if (ValidaSignaPDVOpe == 0)
            {
                ImgAdverirPDV.Visible = true;
                LblAdvertirPDV.Visible = true;
                ImgOkPdv.Visible = false;
                LblOkPdv.Visible = false;
            }
            else
            {
                ImgAdverirPDV.Visible = false;
                LblAdvertirPDV.Visible = false;
                ImgOkPdv.Visible = true;
                LblOkPdv.Visible = true;
            }

            for (int i = 0; i <= LstBoxPdv.Items.Count - 1; i++)
            {
                //falta publicar webservices-... DataTable dt = Get_Supervisión.Get_PuntoVentaAsignado(Convert.ToInt32(LstBoxPdv.Items[i].Value), Convert.ToInt32(gvplanning.SelectedRow.Cells[1].Text));
                DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_PDVASIGNADO", Convert.ToInt32(LstBoxPdv.Items[i].Value), Convert.ToInt32(gvplanning.SelectedRow.Cells[1].Text));

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ValidaSignaPDV = 1;

                    }
                    else
                    {
                        ValidaSignaPDV = 0;
                        i = LstBoxPdv.Items.Count;
                    }
                }
            }
            if (ValidaSignaPDV == 0)
            {
                ImgAdverirPDV0.Visible = true;
                LblAdvertirPDV0.Visible = true;
                ImgOkPdv0.Visible = false;
                LblOkPdv0.Visible = false;
            }
            else
            {
                ImgAdverirPDV0.Visible = false;
                LblAdvertirPDV0.Visible = false;
                ImgOkPdv0.Visible = true;
                LblOkPdv0.Visible = true;
            }



        }
        protected void BtnAsignarProd_Click(object sender, EventArgs e)
        {
            if (GvAsignadosProd.PageIndex != 0)
            {
                GvAsignadosProd.PageIndex = 0;
                GvAsignadosProd.DataSource = (DataTable)this.Session["dtasignadosProdTemp"];
                GvAsignadosProd.DataBind();
            }
            if (GvAsignadosProd.Rows.Count > 0)
            {
                for (int j = 0; j <= GvAsignadosProd.PageCount; j++)
                {
                    for (int i = 0; i <= GvAsignadosProd.Rows.Count - 1; i++)
                    {
                        DataTable dt = Get_Supervisión.Get_ProductoDuplicado(Convert.ToInt32(GvAsignadosProd.Rows[i].Cells[1].Text), Convert.ToInt32(GvAsignadosProd.Rows[i].Cells[1].Text), Convert.ToInt32(gvplanning.SelectedRow.Cells[1].Text));
                        if (dt != null)
                        {
                            if (dt.Rows.Count == 0)
                            {
                                EProducts_PlanningOpe RegistrarProducts_PlanningOpe = Products_PlanningOpe.RegistrarAsignPRODUCTOSaOperativo(Convert.ToInt32(GvAsignadosProd.Rows[i].Cells[3].Text), gvplanning.SelectedRow.Cells[1].Text, Convert.ToInt32(GvAsignadosProd.Rows[i].Cells[1].Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                            }
                        }
                    }
                    GvAsignadosProd.PageIndex = j + 1;
                    GvAsignadosProd.DataSource = (DataTable)this.Session["dtasignadosProdTemp"];
                    GvAsignadosProd.DataBind();
                }

                DataTable dtAsignadosProdTemp = new DataTable();
                dtAsignadosProdTemp.Columns.Add("Cod_Operativo", typeof(Int32));
                dtAsignadosProdTemp.Columns.Add("Nombre_Operativo", typeof(String));
                dtAsignadosProdTemp.Columns.Add("Cod_Prod", typeof(String));
                dtAsignadosProdTemp.Columns.Add("Producto", typeof(String));
                this.Session["dtasignadosProdTemp"] = dtAsignadosProdTemp;

                GvAsignadosProd.DataSource = (DataTable)this.Session["dtasignadosProdTemp"];
                GvAsignadosProd.DataBind();

                Alertas.CssClass = "MensajesSupConfirm";
                this.Session["Encabezado"] = "Asignación de Productos";
                this.Session["alertas"] = "Sr. Usuario. Asignación de Productos realizada con éxito.";
                Mensajes();
            }
            else
            {
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Asignación de Productos";
                this.Session["alertas"] = "Sr. Usuario. no ha seleccionado para realizar la asignación.";
                Mensajes();

            }
        }

        protected void NewAsign_Click(object sender, EventArgs e)
        {
            panelAsignaPDV.Style.Value = "display:Block;";
            BtnAsignarPDV.Visible = true;
            NewAsign.Visible = false;
            BtnBuscarAsignacionPDV.Visible = false;
            panelConsultaPDV.Style.Value = "display:none;";
            BtnCancelVistaPDV.Visible = true;

        }
        protected void NewAsignProd_Click(object sender, EventArgs e)
        {
            panelAsignaProductos.Style.Value = "display:Block;";
            BtnAsignarProd.Visible = true;
            NewAsignProd.Visible = false;
            BtnBuscarAsignacionPRODUCTO.Visible = false;
            panelConsultaPRODUCTOS.Style.Value = "display:none;";
            BtnCancelVistaPRODUCTO.Visible = true;
        }

        protected void BtnBuscarAsignacionPDV_Click(object sender, EventArgs e)
        {
            panelAsignaPDV.Style.Value = "display:none;";
            BtnAsignarPDV.Visible = false;
            NewAsign.Visible = false;
            panelConsultaPDV.Style.Value = "display:Block;";
            BtnBuscarAsignacionPDV.Visible = false;
            BtnCancelVistaPDV.Visible = true;
        }
        protected void BtnBuscarAsignacionPRODUCTO_Click(object sender, EventArgs e)
        {
            panelAsignaProductos.Style.Value = "display:none;";
            BtnAsignarProd.Visible = false;
            NewAsignProd.Visible = false;
            panelConsultaPRODUCTOS.Style.Value = "display:Block;";
            BtnBuscarAsignacionPRODUCTO.Visible = false;
            BtnCancelVistaPRODUCTO.Visible = true;
        }

        protected void BtnCancelVistaPDV_Click(object sender, EventArgs e)
        {
            panelAsignaPDV.Style.Value = "display:none;";
            BtnAsignarPDV.Visible = false;
            NewAsign.Visible = true;
            panelConsultaPDV.Style.Value = "display:none;";
            BtnBuscarAsignacionPDV.Visible = true;
            BtnCancelVistaPDV.Visible = false;
            LstBoxSelBuscarOperativos.SelectedIndex = -1;
            GvBuscarAsignados.DataBind();

            DataTable dtAsignadosTemp = new DataTable();
            dtAsignadosTemp.Columns.Add("Cod_Operativo", typeof(Int32));
            dtAsignadosTemp.Columns.Add("Nombre_Operativo", typeof(String));
            dtAsignadosTemp.Columns.Add("Cod_PDV", typeof(String));
            dtAsignadosTemp.Columns.Add("Punto de Venta", typeof(String));
            this.Session["dtasignadosTemp"] = dtAsignadosTemp;

            GvAsignados.DataSource = (DataTable)this.Session["dtasignadosTemp"];
            GvAsignados.DataBind();

            DataTable dtAsignadosProdTemp = new DataTable();
            dtAsignadosProdTemp.Columns.Add("Cod_Operativo", typeof(Int32));
            dtAsignadosProdTemp.Columns.Add("Nombre_Operativo", typeof(String));
            dtAsignadosProdTemp.Columns.Add("Cod_Prod", typeof(String));
            dtAsignadosProdTemp.Columns.Add("Producto", typeof(String));
            this.Session["dtasignadosProdTemp"] = dtAsignadosProdTemp;

            GvAsignadosProd.DataSource = (DataTable)this.Session["dtasignadosProdTemp"];
            GvAsignadosProd.DataBind();

        }
        protected void BtnCancelVistaPRODUCTO_Click(object sender, EventArgs e)
        {
            panelAsignaProductos.Style.Value = "display:none;";
            BtnAsignarProd.Visible = false;
            NewAsignProd.Visible = true;
            panelConsultaPRODUCTOS.Style.Value = "display:none;";
            BtnBuscarAsignacionPRODUCTO.Visible = true;
            BtnCancelVistaPRODUCTO.Visible = false;
            LstBoxSelBuscarOperativosProd.SelectedIndex = -1;
            GvBuscarAsignadosProd.DataBind();

            DataTable dtAsignadosTemp = new DataTable();
            dtAsignadosTemp.Columns.Add("Cod_Operativo", typeof(Int32));
            dtAsignadosTemp.Columns.Add("Nombre_Operativo", typeof(String));
            dtAsignadosTemp.Columns.Add("Cod_PDV", typeof(String));
            dtAsignadosTemp.Columns.Add("Punto de Venta", typeof(String));
            this.Session["dtasignadosTemp"] = dtAsignadosTemp;

            GvAsignados.DataSource = (DataTable)this.Session["dtasignadosTemp"];
            GvAsignados.DataBind();

            DataTable dtAsignadosProdTemp = new DataTable();
            dtAsignadosProdTemp.Columns.Add("Cod_Operativo", typeof(Int32));
            dtAsignadosProdTemp.Columns.Add("Nombre_Operativo", typeof(String));
            dtAsignadosProdTemp.Columns.Add("Cod_Prod", typeof(String));
            dtAsignadosProdTemp.Columns.Add("Producto", typeof(String));
            this.Session["dtasignadosProdTemp"] = dtAsignadosProdTemp;

            GvAsignadosProd.DataSource = (DataTable)this.Session["dtasignadosProdTemp"];
            GvAsignadosProd.DataBind();

        }

        protected void gvplanningAnalisis_SelectedIndexChanged(object sender, EventArgs e)
        {
            MenuDinamico.Items.Clear();
            this.Session["alertas"] = "";
            PanelVistaAnalisis.Style.Value = "display:block;";

            DataTable dtActCom = Get_Supervisión.Get_ConsultarInfoActividadComercio(Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text));
            GVActComercio.DataSource = dtActCom;
            GVActComercio.DataBind();

            DataTable dtActPropia = Get_Supervisión.Get_ConsultarInfoActividadPropia(Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text));
            GVPhotoActPropia.DataSource = dtActPropia;
            GVPhotoActPropia.DataBind();


            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARINFORMES", Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text));

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                MenuItem item = new MenuItem();
                item.Text = dt.Rows[i]["NameReport"].ToString();
                item.Value = dt.Rows[i]["Report_Id"].ToString();

                MenuDinamico.Items.Add(item);
            }

            DataTable dt2 = null;
            dt2 = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARSTAFF", Convert.ToInt32(this.Session["personid"]), Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text));

            if (dt2.Rows.Count == 0)
            {
                this.Session["Encabezado"] = "Sr Usuario:";
                this.Session["alertas"] = this.Session["alertas"] + "No tiene personal operativo asignado. ";
            }
            else
            {
                ChkLstOpeAnalisis.DataSource = dt2;
                ChkLstOpeAnalisis.DataTextField = "Nombre";
                ChkLstOpeAnalisis.DataValueField = "Person_id";
                ChkLstOpeAnalisis.DataBind();
            }

            if (this.Session["alertas"].ToString() != "")
            {
                Alertas.CssClass = "MensajesSupervisor";
                Mensajes();
            }
        }

        protected void BtnAdd_Click(object sender, ImageClickEventArgs e)
        {
            BtnRest.Visible = true;
            BtnAdd.Visible = false;
            MenuDinamico.Visible = true;
        }

        protected void BtnRest_Click(object sender, ImageClickEventArgs e)
        {
            BtnRest.Visible = false;
            BtnAdd.Visible = true;
            MenuDinamico.Visible = false;

        }

        protected void MenuDinamico_MenuItemClick(object sender, MenuEventArgs e)
        {
            Lbltitulo.Text = "Informe " + MenuDinamico.SelectedItem.Text;
            GvDatosAnalisis.Visible = false;
            panelcontenido.Style.Value = "display:block;";
            if (MenuDinamico.SelectedValue != "0" && MenuDinamico.SelectedValue != "1")
            {
                panelActPropia.Style.Value = "display:block;";
                panelActComercio.Style.Value = "display:none;";
                panelActPhotoPropia.Style.Value = "display:none;";
            }
            if (MenuDinamico.SelectedValue == "0")
            {
                panelActPropia.Style.Value = "display:none;";
                panelActComercio.Style.Value = "display:block;";
                panelActPhotoPropia.Style.Value = "display:none;";
            }

            if (MenuDinamico.SelectedValue == "1")
            {
                panelActPropia.Style.Value = "display:none;";
                panelActComercio.Style.Value = "display:none;";
                panelActPhotoPropia.Style.Value = "display:Block;";
            }
        }

        protected void ChkLstOpeAnalisis_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaAsignacionPDVAnalisis();
            ChkAllPDV.Checked = false;
            ChkAllPDV.Enabled = true;
            ChkLstProdAnalisis.Items.Clear();
            ChkAllPRODUCTO.Visible = false;
            ChkAllPRODUCTO.Checked = false;

        }

        protected void ChkAllPDV_CheckedChanged(object sender, EventArgs e)
        {

            if (ChkAllPDV.Checked == true)
            {
                RbtnLstPDVAnalisis.Enabled = false;
                ChkAllPRODUCTO.Visible = true;
                ChkAllPRODUCTO.Checked = true;
                ChkAllPRODUCTO.Enabled = false;
                ChkLstProdAnalisis.Items.Clear();
            }
            else
            {
                RbtnLstPDVAnalisis.Enabled = true;
                ChkAllPRODUCTO.Visible = false;
                ChkAllPRODUCTO.Checked = false;
                ChkAllPRODUCTO.Enabled = true;
                ChkLstProdAnalisis.Items.Clear();
            }
            for (int i = 0; i <= RbtnLstPDVAnalisis.Items.Count - 1; i++)
            {
                RbtnLstPDVAnalisis.Items[i].Selected = false;
            }
        }
        protected void ChkAllOperativos_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllOperativos.Checked == true)
            {
                ChkLstOpeAnalisis.Enabled = false;
                ChkAllPDV.Visible = true;
                ChkAllPDV.Checked = true;
                ChkAllPDV.Enabled = false;
                RbtnLstPDVAnalisis.Items.Clear();

                ChkAllPRODUCTO.Visible = true;
                ChkAllPRODUCTO.Checked = true;
                ChkAllPRODUCTO.Enabled = false;
                ChkLstProdAnalisis.Items.Clear();
            }
            else
            {
                ChkLstOpeAnalisis.Enabled = true;
                ChkAllPDV.Visible = false;
                ChkAllPDV.Checked = false;
                ChkAllPDV.Enabled = true;
                RbtnLstPDVAnalisis.Items.Clear();

                ChkAllPRODUCTO.Visible = false;
                ChkAllPRODUCTO.Checked = false;
                ChkAllPRODUCTO.Enabled = true;
                ChkLstProdAnalisis.Items.Clear();
            }
            for (int i = 0; i <= ChkLstOpeAnalisis.Items.Count - 1; i++)
            {
                ChkLstOpeAnalisis.Items[i].Selected = false;
            }
        }
        protected void ChkAllPRODUCTO_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllPRODUCTO.Checked == true)
            {
                for (int i = 0; i <= ChkLstProdAnalisis.Items.Count - 1; i++)
                {
                    ChkLstProdAnalisis.Items[i].Selected = true;
                }
            }
            else
            {
                for (int i = 0; i <= ChkLstProdAnalisis.Items.Count - 1; i++)
                {
                    ChkLstProdAnalisis.Items[i].Selected = false;
                }
            }
        }

        protected void BtnConsultarAnalisis_Click(object sender, EventArgs e)
        {
            if (TxtFechaEjec.Text == "" || TxtFechaFinEjec.Text == "" ||
                (ChkAllOperativos.Checked == false && ChkLstOpeAnalisis.SelectedValue == "")
                || (ChkAllPDV.Checked == false && RbtnLstPDVAnalisis.SelectedValue == "")
                || (ChkAllPRODUCTO.Checked == false && ChkLstProdAnalisis.SelectedValue == ""))
            {
                this.Session["Encabezado"] = "Sr Usuario:";
                this.Session["alertas"] = "Es necesario que ingrese todos los campos marcados con (*).";
                Alertas.CssClass = "MensajesSupervisor";
                GvDatosAnalisis.Visible = false;
                Mensajes();
                return;
            }
            GvDatosAnalisis.Visible = true;
            DataTable dt = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARDINAMICA", Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text), Convert.ToInt32(MenuDinamico.SelectedValue));
            #region Información para Ventas
            if (MenuDinamico.SelectedValue == "17")
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            Query = Query + "Sales_Planning." + dt.Rows[i]["name"] + " AS " + "[" + dt.Rows[i]["Symbol_Name"] + "]" + ",";
                            QueryJoin = QueryJoin + "Sales_Planning_1." + dt.Rows[i]["name"] + " AS " + "[" + dt.Rows[i]["Symbol_Name"] + "]" + ",";
                        }
                        Query = Query.Remove(Query.Length - 1, 1);
                        QueryJoin = QueryJoin.Remove(QueryJoin.Length - 1, 1);
                        try
                        {
                            iOperativo = Convert.ToInt32(ChkLstOpeAnalisis.SelectedValue);
                        }
                        catch
                        {
                            iOperativo = 0;
                        }

                        try
                        {
                            QueryPDV = Convert.ToInt32(RbtnLstPDVAnalisis.SelectedValue);
                        }
                        catch
                        {
                            QueryPDV = 0;
                        }

                        DataTable dtResultado = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARDINAMICAxVENTAS", MenuDinamico.SelectedValue, Query, QueryJoin, Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text), TxtFechaEjec.Text, TxtFechaFinEjec.Text, ChkAllOperativos.Checked, iOperativo, QueryPDV, ChkAllPDV.Checked, ChkAllPRODUCTO.Checked);
                        GvDatosAnalisis.Visible = true;
                        GvDatosAnalisis.DataSource = dtResultado;
                        GvDatosAnalisis.DataBind();
                    }
                    else
                    {
                        this.Session["Encabezado"] = "Sr Usuario:";
                        this.Session["alertas"] = "El Planning seleccionado no posee registros para presentar en " + MenuDinamico.SelectedItem.Text;
                        Alertas.CssClass = "MensajesSupervisor";
                        GvDatosAnalisis.Visible = false;
                        Mensajes();
                        return;
                    }
                }
            }
            #endregion
            #region Información para Precios
            if (MenuDinamico.SelectedValue == "19")
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            Query = Query + "Prices_Planning." + dt.Rows[i]["name"] + " AS " + "[" + dt.Rows[i]["Symbol_Name"] + "]" + ",";
                            QueryJoin = QueryJoin + "Prices_Planning_1." + dt.Rows[i]["name"] + " AS " + "[" + dt.Rows[i]["Symbol_Name"] + "]" + ",";
                        }
                        Query = Query.Remove(Query.Length - 1, 1);
                        QueryJoin = QueryJoin.Remove(QueryJoin.Length - 1, 1);
                        try
                        {
                            iOperativo = Convert.ToInt32(ChkLstOpeAnalisis.SelectedValue);
                        }
                        catch
                        {
                            iOperativo = 0;
                        }

                        try
                        {
                            QueryPDV = Convert.ToInt32(RbtnLstPDVAnalisis.SelectedValue);
                        }
                        catch
                        {
                            QueryPDV = 0;
                        }

                        DataTable dtResultado = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARDINAMICAxPRECIOS", MenuDinamico.SelectedValue, Query, QueryJoin, Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text), TxtFechaEjec.Text, TxtFechaFinEjec.Text, ChkAllOperativos.Checked, iOperativo, QueryPDV, ChkAllPDV.Checked, ChkAllPRODUCTO.Checked);
                        GvDatosAnalisis.Visible = true;
                        GvDatosAnalisis.DataSource = dtResultado;
                        GvDatosAnalisis.DataBind();
                    }
                    else
                    {
                        this.Session["Encabezado"] = "Sr Usuario:";
                        this.Session["alertas"] = "El Planning seleccionado no posee registros para presentar en " + MenuDinamico.SelectedItem.Text;
                        Alertas.CssClass = "MensajesSupervisor";
                        GvDatosAnalisis.Visible = false;
                        Mensajes();
                        return;

                    }
                }
            }
            #endregion
            #region Información para ShareofDisplay
            if (MenuDinamico.SelectedValue == "21")
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            Query = Query + "SpaceMeasurement_Planning." + dt.Rows[i]["name"] + " AS " + "[" + dt.Rows[i]["Symbol_Name"] + "]" + ",";
                            QueryJoin = QueryJoin + "SpaceMeasurement_Planning_1." + dt.Rows[i]["name"] + " AS " + "[" + dt.Rows[i]["Symbol_Name"] + "]" + ",";
                        }
                        Query = Query.Remove(Query.Length - 1, 1);
                        QueryJoin = QueryJoin.Remove(QueryJoin.Length - 1, 1);
                        try
                        {
                            iOperativo = Convert.ToInt32(ChkLstOpeAnalisis.SelectedValue);
                        }
                        catch
                        {
                            iOperativo = 0;
                        }

                        try
                        {
                            QueryPDV = Convert.ToInt32(RbtnLstPDVAnalisis.SelectedValue);
                        }
                        catch
                        {
                            QueryPDV = 0;
                        }

                        DataTable dtResultado = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARDINAMICAxSOD", MenuDinamico.SelectedValue, Query, QueryJoin, Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text), TxtFechaEjec.Text, TxtFechaFinEjec.Text, ChkAllOperativos.Checked, iOperativo, QueryPDV, ChkAllPDV.Checked, ChkAllPRODUCTO.Checked);
                        GvDatosAnalisis.Visible = true;
                        GvDatosAnalisis.DataSource = dtResultado;
                        GvDatosAnalisis.DataBind();
                    }
                    else
                    {
                        this.Session["Encabezado"] = "Sr Usuario:";
                        this.Session["alertas"] = "El Planning seleccionado no posee registros para presentar en " + MenuDinamico.SelectedItem.Text;
                        Alertas.CssClass = "MensajesSupervisor";
                        GvDatosAnalisis.Visible = false;
                        Mensajes();
                        return;

                    }
                }
            }
            #endregion
            #region Información para Disponibilidad
            if (MenuDinamico.SelectedValue == "22")
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            Query = Query + "Coverage_Planning." + dt.Rows[i]["name"] + " AS " + "[" + dt.Rows[i]["Symbol_Name"] + "]" + ",";
                            QueryJoin = QueryJoin + "Coverage_Planning_1." + dt.Rows[i]["name"] + " AS " + "[" + dt.Rows[i]["Symbol_Name"] + "]" + ",";
                        }
                        Query = Query.Remove(Query.Length - 1, 1);
                        QueryJoin = QueryJoin.Remove(QueryJoin.Length - 1, 1);
                        try
                        {
                            iOperativo = Convert.ToInt32(ChkLstOpeAnalisis.SelectedValue);
                        }
                        catch
                        {
                            iOperativo = 0;
                        }

                        try
                        {
                            QueryPDV = Convert.ToInt32(RbtnLstPDVAnalisis.SelectedValue);
                        }
                        catch
                        {
                            QueryPDV = 0;
                        }

                        DataTable dtResultado = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARDINAMICAxDISPONIBILIDAD", MenuDinamico.SelectedValue, Query, QueryJoin, Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text), TxtFechaEjec.Text, TxtFechaFinEjec.Text, ChkAllOperativos.Checked, iOperativo, QueryPDV, ChkAllPDV.Checked, ChkAllPRODUCTO.Checked);
                        GvDatosAnalisis.Visible = true;
                        GvDatosAnalisis.DataSource = dtResultado;
                        GvDatosAnalisis.DataBind();
                    }
                    else
                    {
                        this.Session["Encabezado"] = "Sr Usuario:";
                        this.Session["alertas"] = "El Planning seleccionado no posee registros para presentar en " + MenuDinamico.SelectedItem.Text;
                        Alertas.CssClass = "MensajesSupervisor";
                        GvDatosAnalisis.Visible = false;
                        Mensajes();
                        return;

                    }
                }
            }
            #endregion

        }

        protected void RbtnLstPDVAnalisis_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaAsignacionPRODUCTOAnalisis();

        }

        protected void TxtFechaEjec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TxtFechaEjec.Text != "")
                {
                    if (Convert.ToDateTime(TxtFechaEjec.Text.ToString()) > DateTime.Today || Convert.ToDateTime(TxtFechaEjec.Text.ToString()) < Convert.ToDateTime(gvplanningAnalisis.SelectedRow.Cells[7].Text))
                    {
                        TxtFechaFinEjec.Enabled = false;
                        ImgCalendar2.Enabled = false;
                        ChkAllOperativos.Checked = false;
                        ChkAllPDV.Visible = false;
                        ChkAllPRODUCTO.Visible = false;
                        ChkLstOpeAnalisis.SelectedIndex = -1;
                        ChkLstOpeAnalisis.Enabled = true;
                        RbtnLstPDVAnalisis.Items.Clear();
                        ChkLstProdAnalisis.Items.Clear();
                        GvDatosAnalisis.Visible = false;

                        TxtFechaEjec.Focus();
                        TxtFechaEjec.Text = "";
                        Alertas.CssClass = "MensajesSupervisor";
                        this.Session["Encabezado"] = "Parametros Incorrectos";
                        this.Session["alertas"] = "Sr. Usuario, la fecha ingresada no puede ser mayor a la fecha actual ni menor a la fecha inicial del planning. Por favor verifiquelo";
                        Mensajes();
                        return;
                    }
                    else
                    {
                        TxtFechaFinEjec.Enabled = true;
                        ImgCalendar2.Enabled = true;
                        TxtFechaFinEjec.Focus();
                    }
                }
            }
            catch
            {
                ChkAllOperativos.Checked = false;
                ChkAllPDV.Visible = false;
                ChkAllPRODUCTO.Visible = false;
                ChkLstOpeAnalisis.SelectedIndex = -1;
                ChkLstOpeAnalisis.Enabled = true;
                RbtnLstPDVAnalisis.Items.Clear();
                ChkLstProdAnalisis.Items.Clear();
                GvDatosAnalisis.Visible = false;

                TxtFechaFinEjec.Enabled = false;
                ImgCalendar2.Enabled = false;
                TxtFechaEjec.Focus();
                TxtFechaEjec.Text = "";
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Parametros Incorrectos";
                this.Session["alertas"] = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
                Mensajes();
                return;
            }
        }

        protected void TxtFechaFinEjec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TxtFechaFinEjec.Text.ToString().Trim() != "")
                {
                    if (Convert.ToDateTime(TxtFechaFinEjec.Text.ToString().Trim()) < Convert.ToDateTime(TxtFechaEjec.Text.ToString().Trim()) || Convert.ToDateTime(TxtFechaFinEjec.Text.ToString().Trim()) > DateTime.Today)
                    {
                        ChkAllOperativos.Checked = false;
                        ChkAllPDV.Visible = false;
                        ChkAllPRODUCTO.Visible = false;
                        ChkLstOpeAnalisis.SelectedIndex = -1;
                        ChkLstOpeAnalisis.Enabled = true;
                        RbtnLstPDVAnalisis.Items.Clear();
                        ChkLstProdAnalisis.Items.Clear();
                        GvDatosAnalisis.Visible = false;

                        TxtFechaFinEjec.Focus();
                        TxtFechaFinEjec.Text = "";
                        Alertas.CssClass = "MensajesSupervisor";
                        this.Session["Encabezado"] = "Parametros Incorrectos";
                        this.Session["alertas"] = "Sr. Usuario, la fecha final no puede ser menor a la fecha inicial ni superior a la fecha actual. Por favor verifiquelo";
                        Mensajes();
                        return;
                    }
                }
            }

            catch
            {
                ChkAllOperativos.Checked = false;
                ChkAllPDV.Visible = false;
                ChkAllPRODUCTO.Visible = false;
                ChkLstOpeAnalisis.SelectedIndex = -1;
                ChkLstOpeAnalisis.Enabled = true;
                RbtnLstPDVAnalisis.Items.Clear();
                ChkLstProdAnalisis.Items.Clear();
                GvDatosAnalisis.Visible = false;

                TxtFechaFinEjec.Focus();
                TxtFechaFinEjec.Text = "";
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Parametros Incorrectos";
                this.Session["alertas"] = "Sr. Usuario, la fecha ingresada tiene un formato invalido. Por favor verifiquelo";
                Mensajes();
                return;
            }

        }

        protected void GVActComercio_SelectedIndexChanged(object sender, EventArgs e)
        {
            PhotoactividadesCom();
            ModalPopupFotosComercio.Show();
        }

        private void PhotoactividadesCom()
        {
            DataTable dtactivi = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_OBTENERFOTOSACTIVIDAD", Convert.ToInt32(GVActComercio.SelectedRow.Cells[1].Text.ToString()));

            gvactivi.DataSource = dtactivi;
            gvactivi.DataBind();
            if (dtactivi != null)
            {
                if (dtactivi.Rows.Count > 0)
                {
                    for (int i = 0; i <= gvactivi.Rows.Count - 1; i++)
                    {
                        string fn = System.IO.Path.GetFileName(dtactivi.Rows[i]["PhotoCI_PathName"].ToString().Trim());
                        ((Image)gvactivi.Rows[i].Cells[0].FindControl("ImgPhotoa")).ImageUrl = "~/Pages/Modulos/Operativo/PictureComercio/" + fn;
                        ((TextBox)gvactivi.Rows[i].Cells[0].FindControl("txtcomentario")).Text = dtactivi.Rows[i]["PhotoCI_Observacion"].ToString().Trim();
                        ((Label)gvactivi.Rows[i].Cells[0].FindControl("LblNumFoto")).Text = dtactivi.Rows[i]["id_PhotoCI"].ToString().Trim();
                    }
                }
            }
        }

        protected void BtnUpdateComment_Click(object sender, EventArgs e)
        {
            for (int j = 0; j <= gvactivi.Rows.Count - 1; j++)
            {
                ECompetition__Information ObsFotoActCom = oCompetition__Information.ActualizarObsFotoActividadCom(Convert.ToInt32(((Label)gvactivi.Rows[j].Cells[0].FindControl("LblNumFoto")).Text), Convert.ToInt32(GVActComercio.SelectedRow.Cells[1].Text), ((TextBox)gvactivi.Rows[j].Cells[0].FindControl("txtcomentario")).Text, this.Session["sUser"].ToString().Trim(), DateTime.Now);
            }
            Alertas.CssClass = "MensajesSupConfirm";
            this.Session["Encabezado"] = "Actualización Fotografías del Comercio";
            this.Session["alertas"] = "Sr. Usuario. Se han Actualizado correctamente las observaciones de las fotográfias de la Actividad .";
            Mensajes();

        }

        protected void GVPhotoActPropia_SelectedIndexChanged(object sender, EventArgs e)
        {
            PhotoactividadesPropia();
            ModalPopupFotosPropia.Show();
        }

        private void PhotoactividadesPropia()
        {
            DataTable dtactivipropia = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_OBTENERFOTOSACTIVIDADPROPIA", Convert.ToInt32(GVPhotoActPropia.SelectedRow.Cells[1].Text.ToString()));

            gvactivPropia.DataSource = dtactivipropia;
            gvactivPropia.DataBind();
            if (dtactivipropia != null)
            {
                if (dtactivipropia.Rows.Count > 0)
                {
                    for (int i = 0; i <= gvactivPropia.Rows.Count - 1; i++)
                    {
                        string fn = System.IO.Path.GetFileName(dtactivipropia.Rows[i]["Photo_Directory"].ToString().Trim());
                        ((Image)gvactivPropia.Rows[i].Cells[0].FindControl("ImgPhotoap")).ImageUrl = "~/Pages/Modulos/Operativo/PictureActividad/" + fn;
                        ((TextBox)gvactivPropia.Rows[i].Cells[0].FindControl("txtcomentario")).Text = dtactivipropia.Rows[i]["Photo_Comment_Observa"].ToString().Trim();
                        ((Label)gvactivPropia.Rows[i].Cells[0].FindControl("LblNumFoto")).Text = dtactivipropia.Rows[i]["id_Photographs"].ToString().Trim();
                    }
                }
            }
        }

        protected void BtnUpdateCommenPropia_Click(object sender, EventArgs e)
        {
            for (int j = 0; j <= gvactivPropia.Rows.Count - 1; j++)
            {
                EPhotographs_Service PhotographsServices = oPhotographs_Service.Actualizar_ComentarioFotoActividadPropia(Convert.ToInt32(((Label)gvactivPropia.Rows[j].Cells[0].FindControl("LblNumFoto")).Text), ((TextBox)gvactivPropia.Rows[j].Cells[0].FindControl("txtcomentario")).Text,
                    Convert.ToString(this.Session["sUser"]), DateTime.Now);
            }

            DataTable dtActPropia = Get_Supervisión.Get_ConsultarInfoActividadPropia(Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text));
            GVPhotoActPropia.DataSource = dtActPropia;
            GVPhotoActPropia.DataBind();

            Alertas.CssClass = "MensajesSupConfirm";
            this.Session["Encabezado"] = "Actualización Observación Fotografía Actividad";
            this.Session["alertas"] = "Sr. Usuario. Se han Actualizado correctamente la observación de la fotografía de la Actividad.";
            Mensajes();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ModalPopupCorreos.Show();
        }

        protected void RbtnSelPara_SelectedIndexChanged(object sender, EventArgs e)
        {
            sTxtEmail = "";
            this.Session["TxtEmail"] = sTxtEmail;
            TxtEmail.Text = this.Session["TxtEmail"].ToString().Trim();

            if (RbtnSelPara.SelectedValue == "0")
            {
                DataTable dtPersonlMailTo = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTAROPEACCESOSIGE");
                if (dtPersonlMailTo != null)
                {
                    if (dtPersonlMailTo.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtPersonlMailTo.Rows.Count - 1; i++)
                        {

                            sTxtEmail = sTxtEmail + dtPersonlMailTo.Rows[i]["Person_Email"].ToString().Trim();
                            if (i < dtPersonlMailTo.Rows.Count - 1)
                            {
                                sTxtEmail = sTxtEmail + ";";
                            }
                        }
                        this.Session["TxtEmail"] = sTxtEmail;
                        TxtEmail.Text = this.Session["TxtEmail"].ToString().Trim();
                    }
                }
            }

            if (RbtnSelPara.SelectedValue == "1")
            {
                DataTable dtPersonlMailTo = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTAREJECUTIVOCUENTA", Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text));
                if (dtPersonlMailTo != null)
                {
                    if (dtPersonlMailTo.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtPersonlMailTo.Rows.Count - 1; i++)
                        {

                            sTxtEmail = sTxtEmail + dtPersonlMailTo.Rows[i]["Person_Email"].ToString().Trim();
                            if (i < dtPersonlMailTo.Rows.Count - 1)
                            {
                                sTxtEmail = sTxtEmail + ";";
                            }
                        }
                        this.Session["TxtEmail"] = sTxtEmail;
                        TxtEmail.Text = this.Session["TxtEmail"].ToString().Trim();
                    }
                }
            }

            if (RbtnSelPara.SelectedValue == "2")
            {
                DataTable dtPersonlMailTo = oConn.ejecutarDataTable("UP_WEBSIGE_SUPERVISOR_CONSULTARDIRECTORCUENTA", Convert.ToInt32(gvplanningAnalisis.SelectedRow.Cells[1].Text));
                if (dtPersonlMailTo != null)
                {
                    if (dtPersonlMailTo.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtPersonlMailTo.Rows.Count - 1; i++)
                        {
                            sTxtEmail = sTxtEmail + dtPersonlMailTo.Rows[i]["Person_Email"].ToString().Trim();
                            if (i < dtPersonlMailTo.Rows.Count - 1)
                            {
                                sTxtEmail = sTxtEmail + ";";
                            }
                        }
                        this.Session["TxtEmail"] = sTxtEmail;
                        TxtEmail.Text = this.Session["TxtEmail"].ToString().Trim();
                    }
                }
            }

            ModalPopupCorreos.Show();
        }

        protected void ImgEnviarMail_Click(object sender, ImageClickEventArgs e)
        {
            if (TxtEmail.Text == "")
            {
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Envio Solicitudes"; ;
                this.Session["alertas"] = "Sr. Usuario, por favor seleccione a quien va dirigido el correo";
                Mensajes();
                ModalPopupCorreos.Show();
                return;
            }
            if (TxtMotivo.Text == "" || TxtMensaje.Text == "")
            {
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Envio Solicitudes"; ;
                this.Session["alertas"] = "Sr. Usuario, es necesario que ingrese información en el asunto y en el mensaje ";
                Mensajes();
                ModalPopupCorreos.Show();
                return;

            }

            try
            {
                Enviomail oEnviomail = new Enviomail();
                EEnviomail oeEmail = oEnviomail.Envio_mails(this.Session["scountry"].ToString().Trim(), "Solicitud_Operativo");
                Mails oMail = new Mails();
                oMail.Server = "mail.lucky.com.pe";
                //oeEmail.MailServer;
                oMail.From = TxtSolicitante.Text;
                oMail.To = this.Session["TxtEmail"].ToString().Trim();
                oMail.Subject = TxtMotivo.Text;
                oMail.Body = TxtMensaje.Text;
                oMail.CC = "mortiz.col@lucky.com.pe";
                oMail.BodyFormat = "HTML";
                oMail.send();
                oMail = null;
                // oeEmail = null;
                oEnviomail = null;
                TxtMotivo.Text = "";
                TxtMensaje.Text = "";
                RbtnSelPara.SelectedIndex = -1;
                TxtEmail.Text = "";
                Alertas.CssClass = "MensajesSupConfirm";
                this.Session["Encabezado"] = "Envio Solicitudes"; ;
                this.Session["alertas"] = "Sr. Usuario, el mensaje fue enviado correctamente";
                Mensajes();
            }
            catch (Exception ex)
            {
                Alertas.CssClass = "MensajesSupervisor";
                this.Session["Encabezado"] = "Envio Solicitudes"; ;
                this.Session["alertas"] = "Sr. Usuario, se presentó un error inesperado al momento de enviar el correo. Por favor intentelo nuevamente o consulte al Administrador de la aplicación";
                Mensajes();
                return;
            }
        }
    }
}