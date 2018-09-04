using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Facade_Procesos_Administrativos;

namespace SIGE.UsersControls
{
    

    public partial class Calendar : System.Web.UI.UserControl
    {

        #region Declaracion de WS
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos FAdmin = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();



        #endregion

        #region CargaData
        private void cmboaño() {
            DataTable dtaño = null;
            dtaño = FAdmin.Get_ObtenerYears();

            this.Session["Año"] = dtaño;

            
            
            cmbaño.DataSource = dtaño;
            cmbaño.DataValueField = "Years_id";
            cmbaño.DataTextField = "Years_Number";
            cmbaño.DataBind();
        
        
        
        
        
        }


        #endregion 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                cmboaño();
                
            
            
            
            }

        }

        protected void cmbaño_SelectedIndexChanged(object sender, EventArgs e)
        {
      
            
            DataTable dtaño = null;
            dtaño = (DataTable)this.Session["Año"];
            DataTable dtmes = null;
            dtmes = FAdmin.Get_ObtenerMeses();
            lbaño.Text = "";
            lblmes.Text = "";

            for (int i = 0; i <= dtmes.Rows.Count - 1; i++)
            {


                lbaño.Text = dtmes.Rows[i]["Año"].ToString();
                
            }


            this.Session["mes"] = dtmes;
            
          


            cmbmes.DataSource = dtmes;
            cmbmes.DataValueField = "codmes";
            cmbmes.DataTextField = "namemes";
            cmbmes.DataBind();

        }

        protected void cmbmes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtmes = null;

            dtmes = FAdmin.Get_obtener_Mes_Selecionado(Convert.ToInt32(cmbmes.SelectedValue));
            lblmes.Text = "";

                lblmes.Text = dtmes.Rows[0]["namemes"].ToString();
            


            
        }


    }
}