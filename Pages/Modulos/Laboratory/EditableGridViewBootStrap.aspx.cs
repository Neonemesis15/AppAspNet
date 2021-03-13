using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SIGE.Pages.Modulos.Laboratory
{
    public partial class EditableGridViewBootStrap : System.Web.UI.Page
    {
        // Variables Globales
        // Crear el Objeto DataTable Global
        DataTable dt; 

        protected void Page_Load(object sender, EventArgs e)
        {
            dataLoad();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("detail")) {
                int ID = Convert.ToInt32(grdvCrudOperation.DataKeys[index].Value.ToString());
                IEnumerable<DataRow> query = from i in dt.AsEnumerable()
                                             where i.Field<int>("ID").Equals(ID)
                                             select i;
                DataTable detailTable = query.CopyToDataTable<DataRow>();
                DetailsView1.DataSource = detailTable;
                DetailsView1.DataBind();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#detailModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DetailModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("editRecord")) {
                GridViewRow gvrow = grdvCrudOperation.Rows[index];
                HfUpdateID.Value = HttpUtility.HtmlDecode(gvrow.Cells[3].Text).ToString();
                txtNameUpdate.Text = HttpUtility.HtmlDecode(gvrow.Cells[4].Text);
                txtEmailIDUpdate.Text = HttpUtility.HtmlDecode(gvrow.Cells[5].Text);
                txtAddressUpdate.Text = HttpUtility.HtmlDecode(gvrow.Cells[6].Text);
                txtContactUpdate.Text = HttpUtility.HtmlDecode(gvrow.Cells[7].Text);
                lblResult.Visible = false;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#editModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
            }
            else if (e.CommandName.Equals("deleteRecord")) {
                string code = grdvCrudOperation.DataKeys[index].Value.ToString();
                HfDeleteID.Value = code;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
            }
        }

        /// <summary>
        /// Mostrar el Modal para Agregar Nuevos Registros.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }

        /// <summary>
        /// Guardar los Cambios efectuados sobre un registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            try {
                int row = updateDummy(HfUpdateID.Value, 
                    txtNameAdd.Text.Trim(),
                    txtEmailIDAdd.Text.Trim(), 
                    txtAddressAdd.Text.Trim(),
                    txtContactAdd.Text.Trim());
            }
            catch (Exception ex) {
                message = ex.Message;
            }

            dataLoad();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("alert('Records Updated Successfully');");
            sb.Append("$('#editModal').modal('hide');");
            sb.Append(@"</script>");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

        }

        /// <summary>
        /// Evento Click para agregar un nuevo producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            try {
                int row = insertDummy(txtNameAdd.Text.Trim(),
                    txtEmailIDAdd.Text.Trim(),
                    txtAddressAdd.Text.Trim(),
                    txtContactAdd.Text.Trim());
            }
            catch (Exception ex) {
                message = ex.Message;
            }

            dataLoad();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("alert('Record Added Successfully');");
            sb.Append("$('#addModal').modal('hide');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);

        }

        /// <summary>
        /// Evento para Confirmar el Evento Borrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            try{
                int row = deleteDummy(HfDeleteID.Value);
            }
            catch (Exception ex) {
                message = ex.Message;
            }
            dataLoad();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("alert('Record deleted Successfully');");
            sb.Append("$('#deleteModal').modal('hide');");
            sb.Append(@"</script>");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);


        }

        /// <summary>
        /// Cargar en el AspControl GridView 'grdvCrudOperation'
        /// La información a Mostrar en la Grilla
        /// </summary>
        public void dataLoad() {
            grdvCrudOperation.DataSource = dataDummy();
            grdvCrudOperation.DataBind();
            //Ocultar el ID de los registros
            grdvCrudOperation.Columns[3].Visible = false;
        }

        /// <summary>
        /// Retorna un DataTable con Data de Prueba para el Ejemplo 
        /// de GridView BootStrap
        /// </summary>
        public DataTable dataDummy()
        {
            //Inicializar DataTable dt
            dt = new DataTable();
            // Definir las Columnas
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("EmailID", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Contact", typeof(string));

            //Llenar información
            dt.Rows.Add(1, "Lucas", "lucas@3ositos.com", "Mi Casa 111",
            "977419421");
            dt.Rows.Add(2, "Mateo", "mateo@3ositos.com", "Mi Casa 222",
            "977419422");
            dt.Rows.Add(3, "Fiorela", "fiorela@3ositos.com", "Mi Casa 333",
            "977419423");
            dt.Rows.Add(4, "Catalina", "catalina@3ositos.com", "Mi Casa 444",
            "977419424");
            dt.Rows.Add(5, "Juan", "juan@3ositos.com", "Mi Casa 555",
            "977419425");
            dt.Rows.Add(6, "Mery", "mery@3ositos.com", "Mi Casa 666",
            "977419426");
            dt.Rows.Add(7, "Gis", "gis@3ositos.com", "Mi Casa 777",
            "977419427");
            dt.Rows.Add(8, "Tefi", "tefi@3ositos.com", "Mi Casa 888",
            "977419428");
            dt.Rows.Add(9, "Kathy", "Kathy@3ositos.com", "Mi Casa 999",
            "977419429");
            dt.Rows.Add(10, "Pablo", "pablo@3ositos.com", "Mi Casa 000",
            "977419420");

            return dt;
        }

        /// <summary>
        /// Update Dummy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="contactNumber"></param>
        /// <returns></returns>
        public int updateDummy(String id, String name, String email,
            String address, String contactNumber) {
                return 1;
        }

        /// <summary>
        /// Delete Dummy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int deleteDummy(String id) {
            return 1;
        }

        /// <summary>
        /// Dummy insert
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        public int insertDummy(String name, String email,
            String address, String contact) {
                return 1;
        }
    }
}