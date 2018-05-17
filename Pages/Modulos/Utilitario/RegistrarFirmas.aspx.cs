using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lucky.CFG.Tools;
using Lucky.Business.Common.Application;
using Lucky.Entity.Common.Security;

namespace SIGE.Pages.Modulos.Utilitario
{
    public partial class RegistrarFirmas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtNombre.Attributes.Add("onkeypress", "javascript:return validar(event,'ctl00_SheetContentPlaceHolder_txtNombre');");
            txtApePaterno.Attributes.Add("onkeypress", "javascript:return validar(event,'ctl00_SheetContentPlaceHolder_txtApePaterno');");
            txtApeMaterno.Attributes.Add("onkeypress", "javascript:return validar(event,'ctl00_SheetContentPlaceHolder_txtApeMaterno');");
            txtCargo.Attributes.Add("onkeypress", "javascript:return validar(event,'ctl00_SheetContentPlaceHolder_txtCargo');");





            this.txtNombre.Attributes.Add("onblur", "txt1(event,'ctl00_SheetContentPlaceHolder_txtNombre');");
            this.txtApePaterno.Attributes.Add("onblur", "txt1(event,'ctl00_SheetContentPlaceHolder_txtApePaterno');");
            this.txtApeMaterno.Attributes.Add("onblur", "txt1(event,'ctl00_SheetContentPlaceHolder_txtApeMaterno');");
            this.txtCargo.Attributes.Add("onblur", "txt1(event,'ctl00_SheetContentPlaceHolder_txtCargo');");


            this.txtNombre.Attributes.Add("onKeyUp", "txt(event,'ctl00_SheetContentPlaceHolder_txtNombre');");
            this.txtApePaterno.Attributes.Add("onKeyUp", "txt(event,'ctl00_SheetContentPlaceHolder_txtApePaterno');");
            this.txtApeMaterno.Attributes.Add("onKeyUp", "txt(event,'ctl00_SheetContentPlaceHolder_txtApeMaterno');");
            this.txtCargo.Attributes.Add("onKeyUp", "txt(event,'ctl00_SheetContentPlaceHolder_txtCargo');");
         
            if (!Page.IsPostBack)
            {

                if (Session["mensaje"] == null)
                {
                    lblmensaje.Text = "";
                    lblmensaje.Visible = false;
                    iFirma.ImageUrl = "";
                    iFirma.Visible = false;
                }
                else
                {
                    if (Session["mensaje"].ToString() == "Se Registro correctamente" || Session["mensaje"].ToString() == "Se Actualizo correctamente")
                    {
                        lblmensaje.Text = Session["mensaje"].ToString();
                        lblmensaje.Visible = true;
                        iFirma.ImageUrl = "firma.png";
                        iFirma.Visible = true;
                        Session["mensaje"] = null;

                    }
                    else 
                    {
                        lblmensaje.Text = Session["mensaje"].ToString();
                        lblmensaje.Visible = true;
                        Session["mensaje"] = null;

                    }
                }
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

            if (txtNombre.Text == "" || txtApePaterno.Text == "" || txtApeMaterno.Text == "" || txtCargo.Text == "")
            {
                lblmensaje.Text = "llene los campos Nombre, Apellidos y Cargo para realizar el registro";
                lblmensaje.Visible = true;
                lblmensaje.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                if (btnRegistrar.Text == "Actualizar")
                {
                    Session["Actualizar"] = "Actualizar";
                }
                   
                

                Session["txtNombre"] = txtNombre.Text;
                Session["txtApePaterno"] = txtApePaterno.Text;
                Session["txtApeMaterno"] = txtApeMaterno.Text;
                Session["txtCargo"] = txtCargo.Text;
                Session["txtCelular"] = txtCelular.Text;
                Session["txtTelefono"] = txtTelefono.Text;
                Session["txtAnexo"] = txtAnexo.Text;
                Session["txtCelularRPM"] = txtCelularRPM.Text;
                Session["txtCelularNextel"] = txtCelularNextel.Text;

                Session["Registrar"] = "registrar";
                Session["Consulta"] = "";
                Response.Redirect("GeneradorFirma.aspx");
            }
        }
        
        Firma oMyImage = new Firma();

        public void consulta()
        {
            try
            {
                Session["Consulta"] = "consulta";
                DataTable dt = oMyImage.Consulta(Session["txtNombre"].ToString(), Session["txtApePaterno"].ToString(), Session["txtApeMaterno"].ToString(), Session["txtCargo"].ToString());

                txtNombre.Text = dt.Rows[0]["Nombre"].ToString();

                txtApePaterno.Text = dt.Rows[0]["ApellidoPaterno"].ToString();
                txtApeMaterno.Text = dt.Rows[0]["ApellidoMaterno"].ToString();


                txtCargo.Text = dt.Rows[0]["Cargo"].ToString();

                txtCelular.Text = dt.Rows[0]["Celular"].ToString();

                txtTelefono.Text = dt.Rows[0]["telefono"].ToString();

                txtAnexo.Text = dt.Rows[0]["anexo"].ToString();

                txtCelularRPM.Text = dt.Rows[0]["RPM"].ToString();
                txtCelularNextel.Text = dt.Rows[0]["Nextel"].ToString();

              

                oMyImage.MImage("", (byte[])dt.Rows[0]["imagen"]);
                iFirma.ImageUrl = "firma.png";
                iFirma.Visible = true;

                lblmensaje.Visible = false;
            }
            catch
            {
                lblmensaje.Text = "llene los campos Nombre, Apellido Paterno y Apellido Materno para realizar la consulta";
                lblmensaje.Visible = true;
                lblmensaje.ForeColor = System.Drawing.Color.Red;


            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Consulta"] = "consulta";
                DataTable dt = oMyImage.Consulta(txtNombre.Text, txtApePaterno.Text, txtApeMaterno.Text,txtCargo.Text);

                if (dt.Rows.Count > 0)
                {

                    txtNombre.Text = dt.Rows[0]["Nombre"].ToString();

                    txtApePaterno.Text = dt.Rows[0]["ApellidoPaterno"].ToString();
                    txtApeMaterno.Text = dt.Rows[0]["ApellidoMaterno"].ToString();


                    txtCargo.Text = dt.Rows[0]["Cargo"].ToString();

                    txtCelular.Text = dt.Rows[0]["Celular"].ToString();

                    txtTelefono.Text = dt.Rows[0]["telefono"].ToString();

                    txtAnexo.Text = dt.Rows[0]["anexo"].ToString();
                    txtCelularRPM.Text = dt.Rows[0]["RPM"].ToString();
                    txtCelularNextel.Text = dt.Rows[0]["Nextel"].ToString();

                    Session["idfirma"] = dt.Rows[0]["idFirma"].ToString();

                    oMyImage.MImage("", (byte[])dt.Rows[0]["imagen"]);
                    iFirma.ImageUrl = "firma.png";
                    iFirma.Visible = true;

                    lblmensaje.Visible = false;
                    btnCancelar.Visible = true;
                    btnRegistrar.Text = "Actualizar";
                    btnConsultar.Visible = false;
                }

                else
                {
                    lblmensaje.Text = "No existe una Firma con esos parametros";
                    lblmensaje.Visible = true;
                    lblmensaje.ForeColor = System.Drawing.Color.Red;

                    iFirma.ImageUrl = "";
                    iFirma.Visible = false;

                }
            }
            catch
            {
                lblmensaje.Text = "llene los campos Nombre, Apellido Paterno, Apellido Materno y Cargo para realizar la consulta";
                lblmensaje.Visible = true;
                lblmensaje.ForeColor = System.Drawing.Color.Red;


            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnConsultar.Visible = true;
            btnCancelar.Visible = false;
            btnRegistrar.Text = "Registrar";
        }
    }
}