using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using Lucky.Business.Common.Application;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Entity.Common.Application;
using System.Data.SqlClient;
using Lucky.Business.Common.Maestros.Lote;
using Lucky.Entity.Common.Maestros.Lote;


namespace SIGE.Pages.Modulos.Administrativo
{
    public partial class CargaMasivaGProductos : System.Web.UI.Page
    {

        private Product_Type oProductType;
        private SubCategoria oSubCategoria = new SubCategoria();
        private BProduct_Family oProductFamily = new BProduct_Family();
        private SubBrand oSubBrand = new SubBrand();
        private Brand oBrand = new Brand();
        private AD_ProductosAncla oPAncla = new AD_ProductosAncla();
        private Productos oProductos = new Productos();
        private Conexion oConn = new Lucky.Data.Conexion();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.oProductType = new Product_Type();
        }

        #region Funciones

        private void consultaUltimoIdmarca()
        {
            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_CONSULTA_ID_BRANDCOMPANYTMP");
            //llena el ultimo id de marca para insertar en bd intemedia  16/02/2011 Magaly Jiménez
            TxtCodBrand.Text = dt.Rows[0]["id_Brand"].ToString().Trim();
            dt = null;
        }

        private void consultaUltimoIdfamilia()
        {
            DataTable dt = null;
            dt = oConn.ejecutarDataTable("UP_WEBXPLORA_AD_CONSULTA_ID_FAMILYTMP");
            //llena el ultimo id de marca para insertar en bd intemedia  7/04/2011 Magaly Jiménez

            this.Session["codigofamily"] = dt.Rows[0]["id_ProductFamily"].ToString().Trim();
            dt = null;
        }

        private void MensajeAlerta()
        {
            ModalPopupAlertas.Show();
            BtnAceptarAlert.Focus();
            //ScriptManager.RegisterStartupScript(
            //    this, this.GetType(), "myscript", "alert('Debe ingresar todos los parametros con (*)');", true);
        }
        
        #endregion

        protected void btnCargaArchivo_Click(object sender, EventArgs e)
        {
            #region Cargar Categorias
            if (this.Session["TipoCarga"].ToString().Trim() == "Carga Categoria")
            {
                if ((FileUpCMasivaMArca.PostedFile != null) && 
                    (FileUpCMasivaMArca.PostedFile.ContentLength > 0)){

                    string fn = System.IO.Path.GetFileName(FileUpCMasivaMArca.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;

                    if (SaveLocation != string.Empty){
                        if (FileUpCMasivaMArca.FileName.ToLower().EndsWith(".xls")){
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();
                            DataTable dt = new DataTable();

                            FileUpCMasivaMArca.PostedFile.SaveAs(SaveLocation);

                            // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Categorias"];
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            try{
                                if (this.Session["scountry"].ToString() != null){
                                    oDa.Fill(oDs);
                                    dt = oDs.Tables[0];
                                    if (dt.Columns.Count == 2){

                                        dt.Columns[0].ColumnName = "Categoria";
                                        dt.Columns[1].ColumnName = "Grupo Categoria";
                                        dt.Columns[2].ColumnName = "Cliente";

                                        GvCargaArchivo.DataSource = dt;
                                        GvCargaArchivo.DataBind();

                                        foreach (GridViewRow gvr in GvCargaArchivo.Rows){
                                            for (int i = 0; i < 2; i++){
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&nbsp;", "");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("  ", " ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#160;", "");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#193;", "Á");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#201;", "É");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#205;", "Í");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#211;", "Ó");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#218;", "Ú");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#225;", "á");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#233;", "é");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#237;", "í");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#243;", "ó");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#250;", "ú");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#209;", "Ñ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#241;", "ñ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&amp;", "&");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#176;", "o");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#186;", "o");
                                            }
                                        }

                                        bool sigue = true;
                                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                        {

                                            DAplicacion odconsulProductCategory = new DAplicacion();
                                            DataTable dtconsulta = 
                                                odconsulProductCategory.ConsultaDuplicados(
                                                ConfigurationManager.AppSettings["ProductCategory"], 
                                                GvCargaArchivo.Rows[i].Cells[0].Text, 
                                                null, 
                                                null);

                                            if (dtconsulta != null)
                                            {
                                                Alertas.CssClass = "MensajesError";
                                                LblFaltantes.Text = "La Categoría de Producto " + 
                                                    dt.Rows[i][0].ToString().Trim() + " ya existe";
                                                MensajeAlerta();
                                                sigue = false;
                                                i = dt.Rows.Count - 1;
                                            }
                                        }
                                        if (sigue)
                                        {
                                            for (int i = 0; i <= GvCargaArchivo.Rows.Count - 1; i++)
                                            {
                                                try
                                                {
                                                    //EProduct_Type oeProductType = oProductType.RegistrarProductcategory(TxtCodProductType.Text, GvCargaArchivo.Rows[i].Cells[0].Text, GvCargaArchivo.Rows[i].Cells[1].Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                                    EProduct_Type oeProductType = oProductType.RegistrarProductcategory(TxtCodProductType.Text, GvCargaArchivo.Rows[i].Cells[0].Text, GvCargaArchivo.Rows[i].Cells[1].Text, true, GvCargaArchivo.Rows[i].Cells[2].Text, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                                    EProduct_Type oeProductTypeTMP = oProductType.RegistrarProductcategoryTMP(oeProductType.id_Product_Category, oeProductType.Product_Category, oeProductType.Product_Category_Status);
                                                }

                                                catch (Exception ex)
                                                {
                                                    string error = "";
                                                    string mensaje = "";
                                                    error = Convert.ToString(ex.Message);
                                                    mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                                    if (error == mensaje)
                                                    {
                                                        Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                                        string errMessage = "";
                                                        errMessage = mensaje;
                                                        errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                                        this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                                    }
                                                    else
                                                    {
                                                        this.Session.Abandon();
                                                        Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                                    }
                                                }
                                            }
                                            Alertas.CssClass = "MensajesCorrecto";
                                            LblFaltantes.Text = GvCargaArchivo.Rows.Count + "  Categorías  fueron cargadas con éxito" + this.Session["sBrand"] + ", provenientes del Archivo  " + FileUpCMasivaMArca.FileName.ToLower();
                                            MensajeAlerta();
                                        }
                                    }
                                    else
                                    {
                                        GvCargaArchivo.DataBind();
                                        Alertas.CssClass = "MensajesError";
                                        LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Categorías válido. Por favor verifique la estructura que fue enviada a su correo.";
                                        MensajeAlerta();

                                        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                        correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                        correo.To.Add(this.Session["smail"].ToString());
                                        correo.Subject = "Errores en archivo de creación de Categorias";
                                        correo.IsBodyHtml = true;
                                        correo.Priority = System.Net.Mail.MailPriority.Normal;
                                        string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                            this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                   
                                             
                                            "El archivo que usted seleccionó para la carga de Categorías no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga 2 columnas" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                            "Columna 1  : Categoria" + "<br/>" +
                                            "Columna 2  : Grupo de Categoria"+ "<br/>" + "<br/>" +
                                            "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente, usted puede personalizar los nombres de las columnas del archivo ." +
                                            "Pero tenga en cuenta que debe ingresar la información de las Categorias en ese orden." + "<br/>" + "<br/>" + "<br/>" +
                                            "Cordial Saludo" + "<br/>" + "Administrador Xplora" };

                                        correo.Body = string.Concat(txtbody);

                                        System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                        cliente.Host = "mail.lucky.com.pe";

                                        try
                                        {
                                            cliente.Send(correo);
                                        }
                                        catch (System.Net.Mail.SmtpException)
                                        {
                                        }
                                    }
                                }
                                else
                                {
                                    Alertas.CssClass = "MensajesError";
                                    LblFaltantes.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                                    MensajeAlerta();

                                }
                            }
                            catch (Exception ex)
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Categorias valido. Por favor verifique que el nombre de la hoja donde estan los datos sea Categoria";
                                MensajeAlerta();

                            }
                            oConn1.Close();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            MensajeAlerta();

                        }
                    }
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Es indispensable seleccionar un archivo.";
                    MensajeAlerta();

                }


            }
            #endregion
            #region Cargar SubCategoria
            if (this.Session["TipoCarga"].ToString().Trim() == "SubCategoria")
            {
                if ((FileUpCMasivaMArca.PostedFile != null) && (FileUpCMasivaMArca.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpCMasivaMArca.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpCMasivaMArca.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();
                            DataTable dt = new DataTable();

                            FileUpCMasivaMArca.PostedFile.SaveAs(SaveLocation);

                            // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_SubCategorias"]; ;
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            try
                            {
                                if (this.Session["scountry"].ToString() != null)
                                {
                                    oDa.Fill(oDs);
                                    dt = oDs.Tables[0];
                                    if (dt.Columns.Count == 2)
                                    {
                                        dt.Columns[0].ColumnName = "Categoria";
                                        dt.Columns[1].ColumnName = "SubCategoria";

                                        GvCargaArchivo.DataSource = dt;
                                        GvCargaArchivo.DataBind();

                                        foreach (GridViewRow gvr in GvCargaArchivo.Rows)
                                        {
                                            for (int i = 0; i < 2; i++)
                                            {
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&nbsp;", "");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("  ", " ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#160;", "");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#193;", "Á");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#201;", "É");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#205;", "Í");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#211;", "Ó");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#218;", "Ú");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#225;", "á");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#233;", "é");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#237;", "í");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#243;", "ó");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#250;", "ú");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#209;", "Ñ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#241;", "ñ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&amp;", "&");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#176;", "o");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#186;", "o");
                                            }
                                        }

                                        bool sigue = true;
                                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                        {
                                            DataSet oeSubCategoryid = oProductos.ObteneridsProductos(2, null, GvCargaArchivo.Rows[i].Cells[0].Text, null, null, null, null, null, null, null);
                                            if (oeSubCategoryid != null)
                                            {
                                                if (oeSubCategoryid.Tables[0].Rows.Count == 0)
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "La categoria " + GvCargaArchivo.Rows[i].Cells[0].Text + ". No es válido ";
                                                    MensajeAlerta();

                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                                else
                                                {
                                                    GvCargaArchivo.Rows[i].Cells[0].Text = oeSubCategoryid.Tables[0].Rows[0][0].ToString().Trim();
                                                }
                                            }

                                            DAplicacion odconsulSubCategoria = new DAplicacion();
                                            DataTable dtconsulta = odconsulSubCategoria.ConsultaDuplicados(ConfigurationManager.AppSettings["Product_SubCategory"], GvCargaArchivo.Rows[i].Cells[0].Text, GvCargaArchivo.Rows[i].Cells[1].Text, null);
                                            if (dtconsulta != null)
                                            {
                                                Alertas.CssClass = "MensajesError";
                                                LblFaltantes.Text = "La SubCategoria de Producto " + dt.Rows[i][1].ToString().Trim() + " Ya Existe";
                                                MensajeAlerta();

                                                sigue = false;
                                                i = dt.Rows.Count - 1;
                                            }

                                        }
                                        if (sigue)
                                        {
                                            for (int i = 0; i <= GvCargaArchivo.Rows.Count - 1; i++)
                                            {
                                                try
                                                {
                                                    ESubCategoria oeSubCategoria = oSubCategoria.RegistrarSubCategoria(GvCargaArchivo.Rows[i].Cells[0].Text, GvCargaArchivo.Rows[i].Cells[1].Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                                }
                                                catch (Exception ex)
                                                {
                                                    string error = "";
                                                    string mensaje = "";
                                                    error = Convert.ToString(ex.Message);
                                                    mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                                    if (error == mensaje)
                                                    {
                                                        Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                                        string errMessage = "";
                                                        errMessage = mensaje;
                                                        errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                                        this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                                    }
                                                    else
                                                    {
                                                        this.Session.Abandon();
                                                        Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                                    }
                                                }
                                            }
                                            Alertas.CssClass = "MensajesCorrecto";
                                            LblFaltantes.Text = GvCargaArchivo.Rows.Count + "  SubCategorias  fueron cargadas con exito" + this.Session["sBrand"] + ", provenientes del Archivo  " + FileUpCMasivaMArca.FileName.ToLower();
                                            MensajeAlerta();

                                        }
                                    }
                                    else
                                    {

                                        GvCargaArchivo.DataBind();
                                        Alertas.CssClass = "MensajesError";
                                        LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Categorias válido. Por favor verifique la estructura que fue enviada a su correo.";
                                        MensajeAlerta();


                                        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                        correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                        correo.To.Add(this.Session["smail"].ToString());
                                        correo.Subject = "Errores en archivo de creación de SubCategorias";
                                        correo.IsBodyHtml = true;
                                        correo.Priority = System.Net.Mail.MailPriority.Normal;
                                        string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                            this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                   
                                             

                                            "El archivo que usted seleccionó para la carga de SubCategorías no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga 2 columnas" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                            "Columna 1  : Categoria" + "<br/>" +
                                            "Columna 2  : SubCategoria"+ "<br/>" + "<br/>" +
                                            "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizar los nombres de las columnas del archivo ." +
                                            "Pero tenga en cuenta que debe ingresar la información de las SubCategorias en ese orden." + "<br/>" + "<br/>" + 
                                            "Cordial Saludo" + "<br/>" + "Administrador Xplora" };

                                        correo.Body = string.Concat(txtbody);

                                        System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                        cliente.Host = "mail.lucky.com.pe";

                                        try
                                        {
                                            cliente.Send(correo);
                                        }
                                        catch (System.Net.Mail.SmtpException)
                                        {
                                        }
                                    }
                                }
                                else
                                {
                                    Alertas.CssClass = "MensajesError";
                                    LblFaltantes.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                                    MensajeAlerta();

                                }
                            }
                            catch (Exception ex)
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de SubCategorias valido. Por favor verifique que el nombre de la hoja donde estan los datos sea SubCategoria";
                                MensajeAlerta();

                            }
                            oConn1.Close();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            MensajeAlerta();

                        }
                    }
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Es indispensable seleccionar un archivo.";
                    MensajeAlerta();

                }


            }
            #endregion
            #region Cargar Marcas
            if (this.Session["TipoCarga"].ToString().Trim() == "Carga Marcas")
            {
                if ((FileUpCMasivaMArca.PostedFile != null) && (FileUpCMasivaMArca.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpCMasivaMArca.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpCMasivaMArca.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();

                            DataTable dt = new DataTable();


                            FileUpCMasivaMArca.PostedFile.SaveAs(SaveLocation);

                            // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Marcas"];
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            try
                            {
                                if (this.Session["scountry"].ToString() != null)
                                {
                                    oDa.Fill(oDs);
                                    dt = oDs.Tables[0];
                                    if (dt.Columns.Count == 3)
                                    {

                                        dt.Columns[0].ColumnName = "Compañia";
                                        dt.Columns[1].ColumnName = "Categoria";
                                        dt.Columns[2].ColumnName = "Nombre de Marca";

                                        GvCargaArchivo.DataSource = dt;
                                        GvCargaArchivo.DataBind();

                                        foreach (GridViewRow gvr in GvCargaArchivo.Rows)
                                        {
                                            for (int i = 0; i < 3; i++)
                                            {
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&nbsp;", "");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("  ", " ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#160;", "");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#193;", "Á");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#201;", "É");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#205;", "Í");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#211;", "Ó");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#218;", "Ú");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#225;", "á");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#233;", "é");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#237;", "í");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#243;", "ó");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#250;", "ú");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#209;", "Ñ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#241;", "ñ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&amp;", "&");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#176;", "o");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#186;", "o");
                                            }
                                        }

                                        bool sigue = true;
                                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                        {
                                            DataSet oebranid = oBrand.Searchidcompanycategory(GvCargaArchivo.Rows[i].Cells[0].Text, GvCargaArchivo.Rows[i].Cells[1].Text);
                                            if (oebranid != null)
                                            {
                                                if (oebranid.Tables[0].Rows.Count == 0)
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "El Cliente " + GvCargaArchivo.Rows[i].Cells[0].Text + ". No es válido ";
                                                    MensajeAlerta();


                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                                else
                                                {
                                                    GvCargaArchivo.Rows[i].Cells[0].Text = oebranid.Tables[0].Rows[0][0].ToString().Trim();
                                                }

                                                if (oebranid.Tables[1].Rows.Count == 0)
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "La Categoria " + GvCargaArchivo.Rows[i].Cells[1].Text + ". No es válida ";
                                                    MensajeAlerta();


                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                                else
                                                {
                                                    GvCargaArchivo.Rows[i].Cells[1].Text = oebranid.Tables[1].Rows[0][0].ToString().Trim();
                                                }

                                            }
                                            DAplicacion odconsulBrand = new DAplicacion();
                                            DataTable dtconsulta = odconsulBrand.ConsultaDuplicados(ConfigurationManager.AppSettings["Brand"], GvCargaArchivo.Rows[i].Cells[2].Text, GvCargaArchivo.Rows[i].Cells[0].Text, GvCargaArchivo.Rows[i].Cells[1].Text);
                                            if (dtconsulta != null)
                                            {
                                                Alertas.CssClass = "MensajesError";
                                                LblFaltantes.Text = "La Marca de Producto " + dt.Rows[i][2].ToString().Trim() + " Ya Existe";
                                                MensajeAlerta();

                                                sigue = false;
                                                i = dt.Rows.Count - 1;

                                            }

                                        }
                                        if (sigue)
                                        {
                                            for (int i = 0; i <= GvCargaArchivo.Rows.Count - 1; i++)
                                            {
                                                try
                                                {

                                                    EBrand oeBrand = oBrand.RegistrarBrand(Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[0].Text), GvCargaArchivo.Rows[i].Cells[1].Text, GvCargaArchivo.Rows[i].Cells[2].Text, true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                                    EBrand oebrandtmp = oBrand.RegistrarBrandtmp(Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[0].Text), oeBrand.id_Brand, oeBrand.Name_Brand, oeBrand.Brand_Status);
                                                    consultaUltimoIdmarca();
                                                    EBrand oebrandCategorytmp = oBrand.RegistrarBrandCategorytmp(GvCargaArchivo.Rows[i].Cells[1].Text, TxtCodBrand.Text);

                                                }


                                                catch (Exception ex)
                                                {
                                                    string error = "";
                                                    string mensaje = "";
                                                    error = Convert.ToString(ex.Message);
                                                    mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                                    if (error == mensaje)
                                                    {
                                                        Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                                        string errMessage = "";
                                                        errMessage = mensaje;
                                                        errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                                        this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                                    }
                                                    else
                                                    {
                                                        this.Session.Abandon();
                                                        Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                                    }
                                                }
                                            }
                                            Alertas.CssClass = "MensajesCorrecto";
                                            LblFaltantes.Text = GvCargaArchivo.Rows.Count + "  Marcas  fueron cargadas con exito" + this.Session["sBrand"] + ", provenientes del Archivo  " + FileUpCMasivaMArca.FileName.ToLower();
                                            MensajeAlerta();
                                        }
                                    }
                                    else
                                    {
                                        GvCargaArchivo.DataBind();
                                        Alertas.CssClass = "MensajesError";
                                        LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de asignación de puntos de venta válido. Por favor verifique la estructura que fue enviada a su correo.";
                                        MensajeAlerta();

                                        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                        correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                        correo.To.Add(this.Session["smail"].ToString());
                                        correo.Subject = "Errores en archivo de creación de Marcas";
                                        correo.IsBodyHtml = true;
                                        correo.Priority = System.Net.Mail.MailPriority.Normal;
                                        string[] txtbody = new string[] { "Señor(a):" + "<br/>" +
                                            this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +

                                            "El archivo que usted seleccionó para la carga de Marcas no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga 3 columnas" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                            "Columna 1  : Compañia" + "<br/>" +
                                            "Columna 2  : Categoria"+ "<br/>" +
                                            "Columna 3  : Nombre de Marca" + "<br/>" + "<br/>" +
                                            "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizar los nombres de las columnas del archivo ." +
                                            "Pero tenga en cuenta que debe ingresar la información de las Marcas en ese orden." + "<br/>" + "<br/>" + 
                                            "Cordial Saludo" + "<br/>" + "Administrador Xplora" };

                                        correo.Body = string.Concat(txtbody);

                                        System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                        cliente.Host = "mail.lucky.com.pe";

                                        try
                                        {
                                            cliente.Send(correo);
                                        }
                                        catch (System.Net.Mail.SmtpException)
                                        {
                                        }
                                    }
                                }
                                else
                                {
                                    Alertas.CssClass = "MensajesError";
                                    LblFaltantes.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                                    MensajeAlerta();
                                }
                            }
                            catch (Exception ex)
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Marcas válido. Por favor verifique que el nombre de la hoja donde estan los datos sea Marcas";
                                MensajeAlerta();

                            }
                            oConn1.Close();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            MensajeAlerta();

                        }
                    }
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Es indispensable seleccionar un archivo.";
                    MensajeAlerta();
                }
            }
            #endregion
            #region Cargar Productos
            if (this.Session["TipoCarga"].ToString().Trim() == "Productos")
            {
                if ((FileUpCMasivaMArca.PostedFile != null) && (FileUpCMasivaMArca.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpCMasivaMArca.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpCMasivaMArca.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();
                            DataTable dt = new DataTable();

                            FileUpCMasivaMArca.PostedFile.SaveAs(SaveLocation);

                            // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Productos"];
                            //<add key="CargaMasiva_Productos" value="SELECT * FROM [Productos$]" />
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            oConn1.Close();
                            try
                            {

                                oDa.Fill(oDs);
                                dt = oDs.Tables[0];

                                BL_Lote oBL_Lote = new BL_Lote();
                                string nombre_archivo = FileUpCMasivaMArca.FileName.ToString();
                                oBL_Lote.Insert_Lote(dt, nombre_archivo);
                                
                                if (dt.Columns.Count == 14)
                                {
                                    #region
                                    /*
                                    dt.Columns[0].ColumnName = "SKU";
                                    dt.Columns[1].ColumnName = "Nombre Producto";
                                    dt.Columns[2].ColumnName = "Marca";
                                    dt.Columns[3].ColumnName = "SubMarca";
                                    dt.Columns[4].ColumnName = "Familia";
                                    dt.Columns[5].ColumnName = "Cliente";
                                    dt.Columns[6].ColumnName = "Categoria";
                                    dt.Columns[7].ColumnName = "Subcategoria";
                                    dt.Columns[8].ColumnName = "Presentación";
                                    dt.Columns[9].ColumnName = "Peso";
                                    dt.Columns[10].ColumnName = "Precio Lista";
                                    dt.Columns[11].ColumnName = "Precio PVP";
                                    dt.Columns[12].ColumnName = "Unidad de Medida";
                                    dt.Columns[13].ColumnName = "Subfamilia";

                                    GvCargaArchivo.DataSource = dt;
                                    GvCargaArchivo.DataBind();

                                    foreach (GridViewRow gvr in GvCargaArchivo.Rows)
                                    {
                                        for (int i = 0; i < 14; i++)
                                        {
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&nbsp;", "");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("  ", " ");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#160;", "");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#193;", "Á");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#201;", "É");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#205;", "Í");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#211;", "Ó");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#218;", "Ú");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#225;", "á");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#233;", "é");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#237;", "í");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#243;", "ó");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#250;", "ú");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#209;", "Ñ");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#241;", "ñ");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&amp;", "&");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#176;", "o");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#186;", "o");
                                        }
                                    }

                                    string usuario = this.Session["sUser"].ToString().Trim();
                                    ConnectionStringSettings settingconection;
                                    settingconection = ConfigurationManager.ConnectionStrings["ConectaDBLucky"];
                                    string oSqlConnIN = settingconection.ConnectionString;

                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oSqlConnIN))
                                    {
                                        bulkCopy.DestinationTableName = "Products_TMP";
                                        //
                                        bulkCopy.WriteToServer(dt);
                                    }
                                    */
                                    #endregion

                                    #region
                                    /*
                                    bool sigue = true;
                                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                    {
                                        if (GvCargaArchivo.Rows[i].Cells[0].Text == "")
                                        {
                                            Alertas.CssClass = "MensajesError";
                                            LblFaltantes.Text = "Debe ingresar SKU ";
                                            MensajeAlerta();
                                            sigue = false;
                                            i = dt.Rows.Count - 1;
                                        }

                                        if (GvCargaArchivo.Rows[i].Cells[1].Text == "")
                                        {
                                            Alertas.CssClass = "MensajesError";
                                            LblFaltantes.Text = "Debe ingresar Nombre de Producto";
                                            MensajeAlerta();
                                            sigue = false;
                                            i = dt.Rows.Count - 1;
                                        }

                                        if (GvCargaArchivo.Rows[i].Cells[2].Text == "")
                                        {
                                            Alertas.CssClass = "MensajesError";
                                            LblFaltantes.Text = "Debe ingresar Nombre de Marca";
                                            MensajeAlerta();
                                            sigue = false;
                                            i = dt.Rows.Count - 1;
                                        }

                                        if (GvCargaArchivo.Rows[i].Cells[5].Text == "")
                                        {
                                            Alertas.CssClass = "MensajesError";
                                            LblFaltantes.Text = "Debe ingresar Nombre de Cliente";
                                            MensajeAlerta();
                                            sigue = false;
                                            i = dt.Rows.Count - 1;
                                        }
                                        if (GvCargaArchivo.Rows[i].Cells[6].Text == "")
                                        {
                                            Alertas.CssClass = "MensajesError";
                                            LblFaltantes.Text = "Debe ingresar Nombre de Categoría";
                                            MensajeAlerta();
                                            sigue = false;
                                            i = dt.Rows.Count - 1;
                                        }

                                        if (sigue)
                                        {
                                            DataSet oeidsProduct = oProductos.ObteneridsProductos(1, GvCargaArchivo.Rows[i].Cells[5].Text, GvCargaArchivo.Rows[i].Cells[6].Text, GvCargaArchivo.Rows[i].Cells[7].Text, GvCargaArchivo.Rows[i].Cells[2].Text, GvCargaArchivo.Rows[i].Cells[3].Text, GvCargaArchivo.Rows[i].Cells[8].Text, GvCargaArchivo.Rows[i].Cells[4].Text, GvCargaArchivo.Rows[i].Cells[13].Text, GvCargaArchivo.Rows[i].Cells[12].Text);

                                            if (oeidsProduct != null)
                                            {
                                                if (oeidsProduct.Tables[0].Rows.Count == 0)
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "El Cliente " + GvCargaArchivo.Rows[i].Cells[5].Text + ". No es válido ";
                                                    MensajeAlerta();
                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                                else
                                                {
                                                    GvCargaArchivo.Rows[i].Cells[5].Text = oeidsProduct.Tables[0].Rows[0][0].ToString().Trim();
                                                }

                                                if (sigue)
                                                {
                                                    if (oeidsProduct.Tables[1].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "La Categoría " + GvCargaArchivo.Rows[i].Cells[6].Text + ". No es válida ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[6].Text = oeidsProduct.Tables[1].Rows[0][0].ToString().Trim();

                                                        if (oeidsProduct.Tables[8].Rows.Count == 0)
                                                        {
                                                            if (GvCargaArchivo.Rows[i].Cells[7].Text == "")
                                                            {
                                                                GvCargaArchivo.Rows[i].Cells[7].Text = "0";
                                                            }
                                                            else
                                                            {
                                                                Alertas.CssClass = "MensajesError";
                                                                LblFaltantes.Text = "La SubCategoría " + GvCargaArchivo.Rows[i].Cells[7].Text + ". No es válida para la Categoría " + GvCargaArchivo.Rows[i].Cells[6].Text;
                                                                MensajeAlerta();

                                                                sigue = false;
                                                                i = dt.Rows.Count - 1;
                                                            }
                                                        }

                                                        else
                                                        {
                                                            sigue = false;
                                                            for (int j = 0; j <= oeidsProduct.Tables[8].Rows.Count - 1; j++)
                                                            {
                                                                string datow;
                                                                datow = oeidsProduct.Tables[8].Rows[j][1].ToString().Trim();
                                                                if (GvCargaArchivo.Rows[i].Cells[7].Text == oeidsProduct.Tables[8].Rows[j][1].ToString().Trim())
                                                                {
                                                                    GvCargaArchivo.Rows[i].Cells[7].Text = oeidsProduct.Tables[8].Rows[j][0].ToString().Trim();
                                                                    j = oeidsProduct.Tables[8].Rows.Count - 1;
                                                                    sigue = true;

                                                                }

                                                                //else
                                                                //{
                                                                //    Alertas.CssClass = "MensajesError";
                                                                //    LblFaltantes.Text = "La SubCategoria " + GvCargaArchivo.Rows[i].Cells[7].Text + ". No es válida ";
                                                                //    MensajeAlerta();


                                                                //    sigue = false;
                                                                //    i = dt.Rows.Count - 1;
                                                                //    j = oeidsProduct.Tables[8].Rows.Count - 1;

                                                                //}
                                                            }
                                                        }
                                                    }
                                                }

                                                if (sigue)
                                                {
                                                    if (oeidsProduct.Tables[3].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "La Marca " + GvCargaArchivo.Rows[i].Cells[2].Text + ". No es válida ";
                                                        MensajeAlerta();


                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[2].Text = oeidsProduct.Tables[3].Rows[0][0].ToString().Trim();

                                                        if (oeidsProduct.Tables[7].Rows.Count == 0)
                                                        {
                                                            if (GvCargaArchivo.Rows[i].Cells[3].Text == "")
                                                            {
                                                                GvCargaArchivo.Rows[i].Cells[3].Text = "0";
                                                            }
                                                            else
                                                            {
                                                                Alertas.CssClass = "MensajesError";
                                                                LblFaltantes.Text = "La SubMarca " + GvCargaArchivo.Rows[i].Cells[3].Text + ". No es válida para la Marca " + GvCargaArchivo.Rows[i].Cells[2].Text;
                                                                MensajeAlerta();


                                                                sigue = false;
                                                                i = dt.Rows.Count - 1;
                                                            }

                                                        }
                                                        else
                                                        {
                                                            sigue = false;
                                                            for (int j = 0; j <= oeidsProduct.Tables[7].Rows.Count - 1; j++)
                                                            {
                                                                if (GvCargaArchivo.Rows[i].Cells[3].Text == oeidsProduct.Tables[7].Rows[j][1].ToString().Trim())
                                                                {
                                                                    GvCargaArchivo.Rows[i].Cells[3].Text = oeidsProduct.Tables[7].Rows[j][0].ToString().Trim();
                                                                    j = oeidsProduct.Tables[7].Rows.Count - 1;
                                                                    sigue = true;
                                                                }

                                                                //else
                                                                //{
                                                                //    Alertas.CssClass = "MensajesError";
                                                                //    LblFaltantes.Text = "La SubMarca " + GvCargaArchivo.Rows[i].Cells[3].Text + ". No es válida ";
                                                                //    MensajeAlerta();


                                                                //    sigue = false;
                                                                //    i = dt.Rows.Count - 1;
                                                                //    j = oeidsProduct.Tables[7].Rows.Count - 1;

                                                                //}
                                                            }

                                                        }
                                                    }
                                                }
                                                if (sigue)
                                                {
                                                    if (GvCargaArchivo.Rows[i].Cells[8].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[8].Text = "0";
                                                    }
                                                    else
                                                    {
                                                        if (oeidsProduct.Tables[5].Rows.Count == 0)
                                                        {
                                                            Alertas.CssClass = "MensajesError";
                                                            LblFaltantes.Text = "La Presentación " + GvCargaArchivo.Rows[i].Cells[8].Text + ". No es válida ";
                                                            MensajeAlerta();


                                                            sigue = false;
                                                            i = dt.Rows.Count - 1;
                                                        }
                                                        else
                                                        {
                                                            GvCargaArchivo.Rows[i].Cells[8].Text = oeidsProduct.Tables[5].Rows[0][0].ToString().Trim();
                                                        }
                                                    }
                                                }
                                                if (sigue)
                                                {
                                                    if (GvCargaArchivo.Rows[i].Cells[4].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[4].Text = "0";
                                                    }
                                                    else
                                                    {
                                                        if (oeidsProduct.Tables[6].Rows.Count == 0)
                                                        {
                                                            Alertas.CssClass = "MensajesError";
                                                            LblFaltantes.Text = "La Familia " + GvCargaArchivo.Rows[i].Cells[4].Text + ". No es válida ";
                                                            MensajeAlerta();


                                                            sigue = false;
                                                            i = dt.Rows.Count - 1;
                                                        }
                                                        else
                                                        {
                                                            GvCargaArchivo.Rows[i].Cells[4].Text = oeidsProduct.Tables[6].Rows[0][0].ToString().Trim();
                                                        }
                                                    }
                                                }
                                                //verifica el codigo de subfamilia
                                                if (sigue)
                                                {
                                                    if (GvCargaArchivo.Rows[i].Cells[13].Text == "")
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[13].Text = "0";
                                                    }
                                                    else
                                                    {
                                                        if (oeidsProduct.Tables[11].Rows.Count == 0)
                                                        {
                                                            Alertas.CssClass = "MensajesError";
                                                            LblFaltantes.Text = "La SubFamilia " + GvCargaArchivo.Rows[i].Cells[13].Text + ". No es válida ";
                                                            MensajeAlerta();


                                                            sigue = false;
                                                            i = dt.Rows.Count - 1;
                                                        }
                                                        else
                                                        {
                                                            GvCargaArchivo.Rows[i].Cells[13].Text = oeidsProduct.Tables[11].Rows[0][0].ToString().Trim();
                                                        }
                                                    }
                                                }

                                                if (sigue)
                                                {
                                                    if (GvCargaArchivo.Rows[i].Cells[12].Text != "")
                                                    {
                                                        if (oeidsProduct.Tables[10].Rows.Count == 0)
                                                        {
                                                            Alertas.CssClass = "MensajesError";
                                                            LblFaltantes.Text = "La Unidad de Medida " + GvCargaArchivo.Rows[i].Cells[12].Text + ". No es válida ";
                                                            MensajeAlerta();


                                                            sigue = false;
                                                            i = dt.Rows.Count - 1;
                                                        }
                                                        else
                                                        {
                                                            GvCargaArchivo.Rows[i].Cells[12].Text = oeidsProduct.Tables[10].Rows[0][0].ToString().Trim();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        {
                                                            GvCargaArchivo.Rows[i].Cells[12].Text = "0";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (sigue)
                                        {
                                            DAplicacion odconsuProductos = new DAplicacion();
                                            DataTable dtconsulta = odconsuProductos.ConsultaDuplicados(ConfigurationManager.AppSettings["Productos"], GvCargaArchivo.Rows[i].Cells[1].Text, GvCargaArchivo.Rows[i].Cells[0].Text, null);
                                            if (dtconsulta != null)
                                            {
                                                Alertas.CssClass = "MensajesError";
                                                LblFaltantes.Text = "El Producto " + dt.Rows[i][1].ToString().Trim() + " Ya Existe";
                                                MensajeAlerta();

                                                sigue = false;
                                                i = dt.Rows.Count - 1;

                                            }
                                        }

                                    }

                                    try
                                    {

                                        Conexion oCoon = new Conexion();

                                        DataTable dtCargar = oCoon.ejecutarDataTable("UP_WEBXPLORA_AD_CARGAMASIVA_PRODUCTOS_TMP", usuario, DateTime.Now,
                                        usuario, DateTime.Now);


                                        //EProductos oeProductos = oProductos.RegistrarProductos(GvCargaArchivo.Rows[i].Cells[0].Text, "0", GvCargaArchivo.Rows[i].Cells[1].Text, Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[2].Text), Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[3].Text), GvCargaArchivo.Rows[i].Cells[4].Text, GvCargaArchivo.Rows[i].Cells[13].Text,
                                        //Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[5].Text), GvCargaArchivo.Rows[i].Cells[6].Text, Convert.ToInt64(GvCargaArchivo.Rows[i].Cells[7].Text), GvCargaArchivo.Rows[i].Cells[8].Text, 0, Convert.ToDecimal(GvCargaArchivo.Rows[i].Cells[9].Text), Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[12].Text), 29,
                                        //0, 0, 0, 0, Convert.ToDecimal(GvCargaArchivo.Rows[i].Cells[10].Text), Convert.ToDecimal(GvCargaArchivo.Rows[i].Cells[11].Text), "0", "0", true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now, "");
                                        //EProductos oeProductostmp = oProductos.RegistrarProductostmp(Convert.ToInt32(oeProductos.id_Product),
                                        //                                    oeProductos.cod_Product.ToString(),
                                        //                                    oeProductos.Product_Name.ToString(),
                                        //                                    oeProductos.id_Brand.ToString(),
                                        //                                    oeProductos.id_SubBrand.ToString(),
                                        //                                    oeProductos.id_ProductFamily.ToString(),
                                        //                                    oeProductos.id_ProductSubFamily.ToString(),
                                        //                                    oeProductos.Company_id.ToString(),
                                        //                                    oeProductos.id_Product_Categ.ToString(),
                                        //                                    oeProductos.id_Product_Presentation.ToString(),
                                        //                                    oeProductos.Product_Status.ToString());

                                        Alertas.CssClass = "MensajesCorrecto";
                                        LblFaltantes.Text = dtCargar.Rows.Count + "  Productos  fueron cargados con éxito" + this.Session["sBrand"] + ", provenientes del Archivo  " + FileUpCMasivaMArca.FileName.ToLower();
                                        MensajeAlerta();
                                    }
                                    catch (Exception ex)
                                    {
                                        string error = "";
                                        string mensaje = "";
                                        error = Convert.ToString(ex.Message);
                                        mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                        if (error == mensaje)
                                        {
                                            Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                            string errMessage = "";
                                            errMessage = mensaje;
                                            errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                            this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                        }
                                        else
                                        {
                                            this.Session.Abandon();
                                            Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                        }
                                    }
                                    */
                                    #endregion
                                }
                                else
                                {
                                    GvCargaArchivo.DataBind();
                                    //Alertas.CssClass = "MensajesError";
                                    //LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Carga de Productos. Por favor verifique la estructura que fue enviada a su correo.";
                                    Alertas.CssClass = "MensajesCorrecto";
                                    LblFaltantes.Text = dt.Rows.Count + "  Productos  fueron cargados con éxito" + ", provenientes del Archivo  " + FileUpCMasivaMArca.FileName.ToLower();
                                    MensajeAlerta();
                                    #region
                                    /*
                                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                    correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                    correo.To.Add(this.Session["smail"].ToString());
                                    correo.Subject = "Errores en archivo de creación de Productos";
                                    correo.IsBodyHtml = true;
                                    correo.Priority = System.Net.Mail.MailPriority.Normal;
                                    string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                            this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                   
                                             
                                            "El archivo que usted seleccionó para la carga Productos no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga 14 columnas" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                            "Columna 0  : SKU" + "<br/>" +
                                            "Columna 1  : Nombre Producto"+ "<br/>" +
                                            "Columna 2  : Marca" + "<br/>" +   
                                            "Columna 3  : SubMarca" + "<br/>" +     
                                            "Columna 4  : Familia" + "<br/>" +     
                                            "Columna 5  : Cliente" + "<br/>" +     
                                            "Columna 6  : Categoria" + "<br/>" +     
                                            "Columna 7  : Subcategoria" + "<br/>" +     
                                            "Columna 8  : Presentación" + "<br/>" +     
                                            "Columna 9  : Peso" + "<br/>" +     
                                            "Columna 10 : Precio Lista" + "<br/>" +     
                                            "Columna 11 : Precio PVP" + "<br/>" +
                                            "Columna 12 : Unidad de Medida" + "<br/>" +  
                                            "Columna 13 : SubFamilia" + "<br/>" +  "<br/>" +
                                            "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizar los nombres de las columnas del archivo ." +
                                            "Pero tenga en cuenta que debe ingresar la información de los Productos en ese orden." + "<br/>" + "<br/>" + 
                                            "Cordial Saludo" + "<br/>" + "Administrador Xplora"};

                                    correo.Body = string.Concat(txtbody);

                                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                    cliente.Host = "mail.lucky.com.pe";

                                    try
                                    {
                                        cliente.Send(correo);
                                    }
                                    catch (System.Net.Mail.SmtpException)
                                    {
                                    }
                                    */
                                    #endregion
                                }

                            }
                            catch (Exception ex)
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Productos válido. Por favor verifique que el nombre de la hoja donde estan los datos sea Productos";
                                MensajeAlerta();

                            }
                            
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            MensajeAlerta();
                        }
                    }
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Es indispensable seleccionar un archivo.";
                    MensajeAlerta();
                }
            }
            #endregion
            #region Cargar Familias
            if (this.Session["TipoCarga"].ToString().Trim() == "Familia")
            {
                if ((FileUpCMasivaMArca.PostedFile != null) && (FileUpCMasivaMArca.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpCMasivaMArca.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpCMasivaMArca.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();
                            DataTable dt = new DataTable();

                            FileUpCMasivaMArca.PostedFile.SaveAs(SaveLocation);

                            // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Familia"];
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            try
                            {
                                oDa.Fill(oDs);
                                dt = oDs.Tables[0];
                                if (dt.Columns.Count == 8)
                                {
                                    dt.Columns[0].ColumnName = "Cliente";
                                    dt.Columns[1].ColumnName = "Categoria";
                                    dt.Columns[2].ColumnName = "Subcategoria";
                                    dt.Columns[3].ColumnName = "Marca";
                                    dt.Columns[4].ColumnName = "SubMarca";
                                    dt.Columns[5].ColumnName = "Nombre";
                                    dt.Columns[6].ColumnName = "Descripción";
                                    dt.Columns[7].ColumnName = "Peso";

                                    GvCargaArchivo.DataSource = dt;
                                    GvCargaArchivo.DataBind();

                                    #region MyRegion
                                    foreach (GridViewRow gvr in GvCargaArchivo.Rows)
                                    {
                                        for (int i = 0; i < 8; i++)
                                        {
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&nbsp;", "");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("  ", " ");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#160;", "");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#193;", "Á");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#201;", "É");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#205;", "Í");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#211;", "Ó");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#218;", "Ú");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#225;", "á");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#233;", "é");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#237;", "í");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#243;", "ó");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#250;", "ú");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#209;", "Ñ");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#241;", "ñ");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&amp;", "&");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#176;", "o");
                                            gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#186;", "o");
                                        }
                                    }

                                    #endregion

                                    bool sigue = true;
                                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                    {
                                        if (GvCargaArchivo.Rows[i].Cells[0].Text == "")
                                        {
                                            Alertas.CssClass = "MensajesError";
                                            LblFaltantes.Text = "Debe ingresar Cliente";
                                            MensajeAlerta();
                                            sigue = false;
                                            i = dt.Rows.Count - 1;
                                        }

                                        if (GvCargaArchivo.Rows[i].Cells[1].Text == "")
                                        {
                                            Alertas.CssClass = "MensajesError";
                                            LblFaltantes.Text = "Debe ingresar Categoria";
                                            MensajeAlerta();
                                            sigue = false;
                                            i = dt.Rows.Count - 1;
                                        }

                                        if (GvCargaArchivo.Rows[i].Cells[3].Text == "")
                                        {
                                            Alertas.CssClass = "MensajesError";
                                            LblFaltantes.Text = "Debe ingresar Marca";
                                            MensajeAlerta();
                                            sigue = false;
                                            i = dt.Rows.Count - 1;
                                        }

                                        if (GvCargaArchivo.Rows[i].Cells[5].Text == "")
                                        {
                                            Alertas.CssClass = "MensajesError";
                                            LblFaltantes.Text = "Debe ingresar Nombre";
                                            MensajeAlerta();
                                            sigue = false;
                                            i = dt.Rows.Count - 1;
                                        }

                                        if (GvCargaArchivo.Rows[i].Cells[6].Text == "")
                                        {
                                            Alertas.CssClass = "MensajesError";
                                            LblFaltantes.Text = "Debe ingresar Descripción";
                                            MensajeAlerta();

                                            sigue = false;
                                            i = dt.Rows.Count - 1;
                                        }

                                        if (GvCargaArchivo.Rows[i].Cells[7].Text == "")
                                        {
                                            Alertas.CssClass = "MensajesError";
                                            LblFaltantes.Text = "Debe ingresar peso";
                                            MensajeAlerta();

                                            sigue = false;
                                            i = dt.Rows.Count - 1;
                                        }

                                        if (sigue)
                                        {
                                            DataSet oeidsProduct = oProductos.ObteneridsProductos(1, GvCargaArchivo.Rows[i].Cells[0].Text, GvCargaArchivo.Rows[i].Cells[1].Text, GvCargaArchivo.Rows[i].Cells[2].Text, GvCargaArchivo.Rows[i].Cells[3].Text, GvCargaArchivo.Rows[i].Cells[4].Text, null, null, null, null);

                                            if (oeidsProduct != null)
                                            {
                                                if (oeidsProduct.Tables[0].Rows.Count == 0)
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "El Cliente " + GvCargaArchivo.Rows[i].Cells[0].Text + ", no es válido.";
                                                    MensajeAlerta();
                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                                else
                                                {
                                                    GvCargaArchivo.Rows[i].Cells[0].Text = oeidsProduct.Tables[0].Rows[0][0].ToString().Trim();
                                                }

                                                if (sigue)
                                                {
                                                    if (oeidsProduct.Tables[1].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "La Categoría " + GvCargaArchivo.Rows[i].Cells[1].Text + ", no es válida.";
                                                        MensajeAlerta();
                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[1].Text = oeidsProduct.Tables[1].Rows[0][0].ToString().Trim();

                                                        if (oeidsProduct.Tables[8].Rows.Count == 0)
                                                        {
                                                            if (GvCargaArchivo.Rows[i].Cells[2].Text == "")
                                                            {
                                                                GvCargaArchivo.Rows[i].Cells[2].Text = "0";
                                                            }
                                                            else
                                                            {
                                                                Alertas.CssClass = "MensajesError";
                                                                LblFaltantes.Text = "La SubCategoria " + GvCargaArchivo.Rows[i].Cells[2].Text + ", no es válida para la Categoría " + GvCargaArchivo.Rows[i].Cells[6].Text + ".";
                                                                MensajeAlerta();
                                                                sigue = false;
                                                                i = dt.Rows.Count - 1;
                                                            }
                                                        }

                                                        else
                                                        {
                                                            sigue = false;
                                                            for (int j = 0; j <= oeidsProduct.Tables[8].Rows.Count - 1; j++)
                                                            {
                                                                string datow;
                                                                datow = oeidsProduct.Tables[8].Rows[j][1].ToString().Trim();
                                                                if (GvCargaArchivo.Rows[i].Cells[2].Text == oeidsProduct.Tables[8].Rows[j][1].ToString().Trim())
                                                                {
                                                                    GvCargaArchivo.Rows[i].Cells[2].Text = oeidsProduct.Tables[8].Rows[j][0].ToString().Trim();
                                                                    j = oeidsProduct.Tables[8].Rows.Count - 1;
                                                                    sigue = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                                if (sigue)
                                                {
                                                    if (oeidsProduct.Tables[3].Rows.Count == 0)
                                                    {
                                                        Alertas.CssClass = "MensajesError";
                                                        LblFaltantes.Text = "La Marca " + GvCargaArchivo.Rows[i].Cells[3].Text + ", no es válida.";
                                                        MensajeAlerta();
                                                        sigue = false;
                                                        i = dt.Rows.Count - 1;
                                                    }
                                                    else
                                                    {
                                                        GvCargaArchivo.Rows[i].Cells[3].Text = oeidsProduct.Tables[3].Rows[0][0].ToString().Trim();

                                                        if (oeidsProduct.Tables[7].Rows.Count == 0)
                                                        {
                                                            if (GvCargaArchivo.Rows[i].Cells[4].Text == "")
                                                            {
                                                                GvCargaArchivo.Rows[i].Cells[4].Text = "0";
                                                            }
                                                            else
                                                            {
                                                                Alertas.CssClass = "MensajesError";
                                                                LblFaltantes.Text = "La SubMarca " + GvCargaArchivo.Rows[i].Cells[4].Text + ", no es válida para la Marca " + GvCargaArchivo.Rows[i].Cells[3].Text + ".";
                                                                MensajeAlerta();
                                                                sigue = false;
                                                                i = dt.Rows.Count - 1;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            sigue = false;
                                                            for (int j = 0; j <= oeidsProduct.Tables[7].Rows.Count - 1; j++)
                                                            {
                                                                string dato;
                                                                dato = oeidsProduct.Tables[7].Rows[j][1].ToString().Trim();
                                                                if (GvCargaArchivo.Rows[i].Cells[4].Text == oeidsProduct.Tables[7].Rows[j][1].ToString().Trim())
                                                                {
                                                                    GvCargaArchivo.Rows[i].Cells[4].Text = oeidsProduct.Tables[7].Rows[j][0].ToString().Trim();
                                                                    j = oeidsProduct.Tables[7].Rows.Count - 1;
                                                                    sigue = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        if (sigue)
                                        {
                                            try
                                            {
                                                DAplicacion odcConsuFamily = new DAplicacion();
                                                DataTable dtconsulta = odcConsuFamily.ConsultaDuplicados(ConfigurationManager.AppSettings["ProductFamily"], null, GvCargaArchivo.Rows[i].Cells[3].Text.ToUpper(), GvCargaArchivo.Rows[i].Cells[5].Text.ToUpper());

                                                if (dtconsulta != null)
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "La Familia " + dt.Rows[i][1].ToString().Trim() + " ya existe";
                                                    MensajeAlerta();
                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                            }
                                            catch (Exception ex) { }
                                        }
                                    }
                                    if (sigue)
                                    {
                                        for (int i = 0; i <= GvCargaArchivo.Rows.Count - 1; i++)
                                        {
                                            try
                                            {
                                                EProduct_Family oFamily = oProductFamily.RegistrarProductosFamily(null, GvCargaArchivo.Rows[i].Cells[1].Text.ToString().Trim(), Convert.ToInt64(GvCargaArchivo.Rows[i].Cells[2].Text.ToString().Trim()), Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[3].Text.ToString().Trim()), Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[4].Text), GvCargaArchivo.Rows[i].Cells[5].Text, GvCargaArchivo.Rows[i].Cells[6].Text, Convert.ToDecimal(GvCargaArchivo.Rows[i].Cells[7].Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                                consultaUltimoIdfamilia();
                                                EProduct_Family oFamilytmp = oProductFamily.RegistrarProductosFamilyTMP(this.Session["codigofamily"].ToString().Trim(), Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[3].Text.ToString().Trim()), Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[4].Text.ToString().Trim()), GvCargaArchivo.Rows[i].Cells[5].Text, true, GvCargaArchivo.Rows[i].Cells[1].Text);
                                            }
                                            catch (Exception ex)
                                            {
                                                string error = "";
                                                string mensaje = "";
                                                error = Convert.ToString(ex.Message);
                                                mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                                if (error == mensaje)
                                                {
                                                    Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                                    string errMessage = "";
                                                    errMessage = mensaje;
                                                    errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                                    this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                                }
                                                else
                                                {
                                                    this.Session.Abandon();
                                                    Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                                }
                                            }
                                        }
                                        Alertas.CssClass = "MensajesCorrecto";
                                        LblFaltantes.Text = GvCargaArchivo.Rows.Count + "  Familias fueron cargadas con exito" + this.Session["sBrand"] + ", provenientes del Archivo  " + FileUpCMasivaMArca.FileName.ToLower();
                                        MensajeAlerta();
                                    }
                                }
                                else
                                {

                                    GvCargaArchivo.DataBind();
                                    Alertas.CssClass = "MensajesError";
                                    LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Carga de Productos. Por favor verifique la estructura que fue enviada a su correo.";
                                    MensajeAlerta();


                                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                    correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                    correo.To.Add(this.Session["smail"].ToString());
                                    correo.Subject = "Errores en archivo de creación de Familias";
                                    correo.IsBodyHtml = true;
                                    correo.Priority = System.Net.Mail.MailPriority.Normal;
                                    string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                    this.Session["nameuser"].ToString() + "<br/>" + "<br/>" + 
                                    "El archivo que usted seleccionó para la carga de Familias no cumple con una estructura válida." + "<br/>" +
                                    "Por favor verifique que tenga 8 columnas" + "<br/>" +  "<br/>" +
                                    "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                    "Columna 0  : Cliente"+ "<br/>" +
                                    "Columna 1  : Categoria" + "<br/>" +   
                                    "Columna 2  : Subcategoria" + "<br/>" +     
                                    "Columna 3  : Marca" + "<br/>" +     
                                    "Columna 4  : SubMarca" + "<br/>" +     
                                    "Columna 5  : Nombre" + "<br/>" +     
                                    "Columna 6  : Descripción" + "<br/>" +     
                                    "Columna 7  : Peso" + "<br/>" + "<br/>" +                                                                       
                                    "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente, usted puede personalizar los nombres de las columnas del archivo ." +
                                    "Pero tenga en cuenta que debe ingresar la información de los Productos en ese orden." + "<br/>" + "<br/>" + 
                                    "Cordial Saludo" + "<br/>" + "Administrador Xplora"};
                                    correo.Body = string.Concat(txtbody);

                                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                    cliente.Host = "mail.lucky.com.pe";

                                    try
                                    {
                                        cliente.Send(correo);
                                    }
                                    catch (System.Net.Mail.SmtpException)
                                    {
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Productos válido. Por favor verifique que el nombre de la hoja donde estan los datos sea Familia";
                                MensajeAlerta();

                            }
                            oConn1.Close();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            MensajeAlerta();

                        }
                    }
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Es indispensable seleccionar un archivo.";
                    MensajeAlerta();

                }


            }
            #endregion
            #region Cargar Producto Ancla
            if (this.Session["TipoCarga"].ToString().Trim() == "Producto_Ancla")
            {
                if ((FileUpCMasivaMArca.PostedFile != null) && (FileUpCMasivaMArca.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpCMasivaMArca.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpCMasivaMArca.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();
                            DataTable dt = new DataTable();

                            FileUpCMasivaMArca.PostedFile.SaveAs(SaveLocation);

                            // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Pancla"];
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            try
                            {
                                if (this.Session["scountry"].ToString() != null)
                                {
                                    oDa.Fill(oDs);
                                    dt = oDs.Tables[0];
                                    if (dt.Columns.Count == 6)
                                    {

                                        dt.Columns[0].ColumnName = "Cliente";
                                        dt.Columns[1].ColumnName = "Oficina";
                                        dt.Columns[2].ColumnName = "Categoria";
                                        dt.Columns[3].ColumnName = "SubCategoria";
                                        dt.Columns[4].ColumnName = "Marca";
                                        dt.Columns[5].ColumnName = "Producto";


                                        GvCargaArchivo.DataSource = dt;
                                        GvCargaArchivo.DataBind();

                                        foreach (GridViewRow gvr in GvCargaArchivo.Rows)
                                        {
                                            for (int i = 0; i < 6; i++)
                                            {
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&nbsp;", "");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("  ", " ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#160;", "");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#193;", "Á");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#201;", "É");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#205;", "Í");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#211;", "Ó");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#218;", "Ú");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#225;", "á");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#233;", "é");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#237;", "í");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#243;", "ó");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#250;", "ú");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#209;", "Ñ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#241;", "ñ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&amp;", "&");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#176;", "o");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#186;", "o");
                                            }
                                        }

                                        bool sigue = true;
                                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                        {
                                            if (GvCargaArchivo.Rows[i].Cells[0].Text == "")
                                            {
                                                Alertas.CssClass = "MensajesError";
                                                LblFaltantes.Text = "Debe ingresar Cliente ";
                                                MensajeAlerta();

                                                sigue = false;
                                                i = dt.Rows.Count - 1;
                                            }

                                            if (GvCargaArchivo.Rows[i].Cells[1].Text == "")
                                            {
                                                Alertas.CssClass = "MensajesError";
                                                LblFaltantes.Text = "Debe ingresar Oficina";
                                                MensajeAlerta();

                                                sigue = false;
                                                i = dt.Rows.Count - 1;
                                            }

                                            if (GvCargaArchivo.Rows[i].Cells[2].Text == "")
                                            {
                                                Alertas.CssClass = "MensajesError";
                                                LblFaltantes.Text = "Debe ingresar Categoria";
                                                MensajeAlerta();

                                                sigue = false;
                                                i = dt.Rows.Count - 1;
                                            }

                                            if (GvCargaArchivo.Rows[i].Cells[4].Text == "")
                                            {
                                                Alertas.CssClass = "MensajesError";
                                                LblFaltantes.Text = "Debe ingresar Marca";
                                                MensajeAlerta();

                                                sigue = false;
                                                i = dt.Rows.Count - 1;
                                            }
                                            if (GvCargaArchivo.Rows[i].Cells[5].Text == "")
                                            {
                                                Alertas.CssClass = "MensajesError";
                                                LblFaltantes.Text = "Debe ingresar Producto";
                                                MensajeAlerta();

                                                sigue = false;
                                                i = dt.Rows.Count - 1;
                                            }

                                            DataSet oeidspancla = oPAncla.ObteneridProducAncla(GvCargaArchivo.Rows[i].Cells[0].Text, GvCargaArchivo.Rows[i].Cells[1].Text, GvCargaArchivo.Rows[i].Cells[2].Text, GvCargaArchivo.Rows[i].Cells[3].Text, GvCargaArchivo.Rows[i].Cells[4].Text, GvCargaArchivo.Rows[i].Cells[5].Text);

                                            if (oeidspancla != null)
                                            {
                                                if (oeidspancla.Tables[0].Rows.Count == 0)
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "El Cliente " + GvCargaArchivo.Rows[i].Cells[0].Text + ". No es válido ";
                                                    MensajeAlerta();
                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                                else
                                                {
                                                    GvCargaArchivo.Rows[i].Cells[0].Text = oeidspancla.Tables[0].Rows[0][0].ToString().Trim();
                                                }

                                                if (oeidspancla.Tables[1].Rows.Count == 0)
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "La Oficina " + GvCargaArchivo.Rows[i].Cells[1].Text + ". No es válido ";
                                                    MensajeAlerta();
                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                                else
                                                {
                                                    GvCargaArchivo.Rows[i].Cells[1].Text = oeidspancla.Tables[1].Rows[0][0].ToString().Trim();
                                                }

                                                if (oeidspancla.Tables[2].Rows.Count == 0)
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "La Categoria " + GvCargaArchivo.Rows[i].Cells[2].Text + ". No es válida ";
                                                    MensajeAlerta();
                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                                else
                                                {
                                                    GvCargaArchivo.Rows[i].Cells[2].Text = oeidspancla.Tables[2].Rows[0][0].ToString().Trim();

                                                    if (oeidspancla.Tables[3].Rows.Count == 0)
                                                    {
                                                        if (GvCargaArchivo.Rows[i].Cells[3].Text == "")
                                                        {
                                                            GvCargaArchivo.Rows[i].Cells[3].Text = "0";
                                                        }
                                                        else
                                                        {
                                                            Alertas.CssClass = "MensajesError";
                                                            LblFaltantes.Text = "La SubCategoria " + GvCargaArchivo.Rows[i].Cells[3].Text + ". No es válida para la Categoria " + GvCargaArchivo.Rows[i].Cells[2].Text;
                                                            MensajeAlerta();
                                                            sigue = false;
                                                            i = dt.Rows.Count - 1;
                                                        }
                                                    }

                                                    else
                                                    {
                                                        sigue = false;
                                                        for (int j = 0; j <= oeidspancla.Tables[3].Rows.Count - 1; j++)
                                                        {
                                                            string dato;
                                                            dato = oeidspancla.Tables[3].Rows[j][1].ToString().Trim();
                                                            if (GvCargaArchivo.Rows[i].Cells[3].Text == oeidspancla.Tables[3].Rows[j][1].ToString().Trim())
                                                            {
                                                                GvCargaArchivo.Rows[i].Cells[3].Text = oeidspancla.Tables[3].Rows[j][0].ToString().Trim();
                                                                j = oeidspancla.Tables[3].Rows.Count - 1;
                                                                sigue = true;
                                                            }
                                                        }
                                                    }
                                                }

                                                if (oeidspancla.Tables[4].Rows.Count == 0)
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "La Marca " + GvCargaArchivo.Rows[i].Cells[4].Text + ". No es válida ";
                                                    MensajeAlerta();
                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                                else
                                                {
                                                    GvCargaArchivo.Rows[i].Cells[4].Text = oeidspancla.Tables[4].Rows[0][0].ToString().Trim();
                                                }

                                                if (oeidspancla.Tables[5].Rows.Count == 0)
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "El Producto " + GvCargaArchivo.Rows[i].Cells[5].Text + ". No es válida ";
                                                    MensajeAlerta();
                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                                else
                                                {
                                                    GvCargaArchivo.Rows[i].Cells[5].Text = oeidspancla.Tables[5].Rows[0][0].ToString().Trim();
                                                }
                                            }
                                            DAplicacion odconsultaPAncla = new DAplicacion();
                                            DataTable dtconsulta = odconsultaPAncla.ConsultaDuplicadosPancla(ConfigurationManager.AppSettings["AD_ProductosAncla"], Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[0].Text), GvCargaArchivo.Rows[i].Cells[2].Text, Convert.ToInt64(GvCargaArchivo.Rows[i].Cells[3].Text), Convert.ToInt64(GvCargaArchivo.Rows[i].Cells[1].Text));
                                            if (dtconsulta != null)
                                            {
                                                Alertas.CssClass = "MensajesError";
                                                LblFaltantes.Text = "El Producto Ancla " + dt.Rows[i][1].ToString().Trim() + " Ya Existe";
                                                MensajeAlerta();
                                                sigue = false;
                                                i = dt.Rows.Count - 1;
                                            }
                                        }
                                        if (sigue)
                                        {
                                            for (int i = 0; i <= GvCargaArchivo.Rows.Count - 1; i++)
                                            {
                                                try
                                                {
                                                    EAD_ProductosAncla oePAncla = oPAncla.RegistrarPAncla(Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[0].Text), GvCargaArchivo.Rows[i].Cells[2].Text, Convert.ToInt64(GvCargaArchivo.Rows[i].Cells[3].Text), GvCargaArchivo.Rows[i].Cells[5].Text, Convert.ToInt64(GvCargaArchivo.Rows[i].Cells[1].Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                                }
                                                catch (Exception ex)
                                                {
                                                    string error = "";
                                                    string mensaje = "";
                                                    error = Convert.ToString(ex.Message);
                                                    mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                                    if (error == mensaje)
                                                    {
                                                        Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                                        string errMessage = "";
                                                        errMessage = mensaje;
                                                        errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                                        this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                                    }
                                                    else
                                                    {
                                                        this.Session.Abandon();
                                                        Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                                    }
                                                }
                                            }
                                            Alertas.CssClass = "MensajesCorrecto";
                                            LblFaltantes.Text = GvCargaArchivo.Rows.Count + "  Productos ancla fueron cargadas con exito " + this.Session["sBrand"] + ", provenientes del Archivo  " + FileUpCMasivaMArca.FileName.ToLower();
                                            MensajeAlerta();
                                        }
                                    }
                                    else
                                    {
                                        GvCargaArchivo.DataBind();
                                        Alertas.CssClass = "MensajesError";
                                        LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Carga de Productos Ancla. Por favor verifique la estructura que fue enviada a su correo.";
                                        MensajeAlerta();

                                        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                        correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                        correo.To.Add(this.Session["smail"].ToString());
                                        correo.Subject = "Errores en archivo de creación de Productos Ancla";
                                        correo.IsBodyHtml = true;
                                        correo.Priority = System.Net.Mail.MailPriority.Normal;
                                        string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                         this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                   
                                             

                                            "El archivo que usted seleccionó para la carga de Producto Ancla no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga 6 columnas" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                            "Columna 0  : Cliente" + "<br/>" +
                                            "Columna 1  : Oficina"+ "<br/>" +
                                            "Columna 2  : Categoria" + "<br/>" +   
                                            "Columna 3  : SubCategoria" + "<br/>" +     
                                            "Columna 4  : Marca" + "<br/>" +     
                                            "Columna 5  : Producto" + "<br/>" + "<br/>" +
                                            "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente  , usted puede personalizar los nombres de las columnas del archivo ." +
                                            "Pero tenga en cuenta que debe ingresar la información de los Productos en ese orden." + "<br/>" + "<br/>" + "<br/>" +
                                            "Cordial Saludo" + "<br/>" + "Administrador Xplora"};

                                        correo.Body = string.Concat(txtbody);
                                        System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                        cliente.Host = "mail.lucky.com.pe";

                                        try
                                        {
                                            cliente.Send(correo);
                                        }
                                        catch (System.Net.Mail.SmtpException)
                                        {
                                        }
                                    }
                                }
                                else
                                {
                                    Alertas.CssClass = "MensajesError";
                                    LblFaltantes.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                                    MensajeAlerta();
                                }
                            }
                            catch (Exception ex)
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Productos Ancla válido. Por favor verifique que el nombre de la hoja donde estan los datos sea Pancla";
                                MensajeAlerta();
                            }
                            oConn1.Close();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            MensajeAlerta();
                        }
                    }
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Es indispensable seleccionar un archivo.";
                    MensajeAlerta();
                }
            }

            #endregion
            #region Cargar SubMarcas
            if (this.Session["TipoCarga"].ToString().Trim() == "SubMarca")
            {
                if ((FileUpCMasivaMArca.PostedFile != null) && (FileUpCMasivaMArca.PostedFile.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(FileUpCMasivaMArca.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("Busquedas") + "\\" + fn;

                    if (SaveLocation != string.Empty)
                    {
                        if (FileUpCMasivaMArca.FileName.ToLower().EndsWith(".xls"))
                        {
                            OleDbConnection oConn1 = new OleDbConnection();
                            OleDbCommand oCmd = new OleDbCommand();
                            OleDbDataAdapter oDa = new OleDbDataAdapter();
                            DataSet oDs = new DataSet();

                            DataTable dt = new DataTable();



                            FileUpCMasivaMArca.PostedFile.SaveAs(SaveLocation);

                            // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                            oConn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 8.0; HDR=YES;\"";
                            oConn1.Open();
                            oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_SubMarca"]; ;
                            oCmd.Connection = oConn1;
                            oDa.SelectCommand = oCmd;
                            try
                            {
                                if (this.Session["scountry"].ToString() != null)
                                {
                                    oDa.Fill(oDs);
                                    dt = oDs.Tables[0];
                                    if (dt.Columns.Count == 3)
                                    {

                                        dt.Columns[0].ColumnName = "Categoria";
                                        dt.Columns[1].ColumnName = "Marca";
                                        dt.Columns[2].ColumnName = "SubMarca";

                                        GvCargaArchivo.DataSource = dt;
                                        GvCargaArchivo.DataBind();

                                        foreach (GridViewRow gvr in GvCargaArchivo.Rows)
                                        {
                                            for (int i = 0; i < 3; i++)
                                            {
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&nbsp;", "");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("  ", " ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#160;", "");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#193;", "Á");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#201;", "É");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#205;", "Í");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#211;", "Ó");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#218;", "Ú");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#225;", "á");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#233;", "é");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#237;", "í");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#243;", "ó");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#250;", "ú");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#209;", "Ñ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#241;", "ñ");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&amp;", "&");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#176;", "o");
                                                gvr.Cells[i].Text = gvr.Cells[i].Text.Replace("&#186;", "o");
                                            }
                                        }

                                        bool sigue = true;
                                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                        {
                                            if (sigue)
                                            {
                                                if (GvCargaArchivo.Rows[i].Cells[0].Text == "")
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "Debe ingresar Nombre de Categoria";
                                                    MensajeAlerta();

                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                                if (GvCargaArchivo.Rows[i].Cells[1].Text == "")
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "Debe ingresar Nombre de Marca";
                                                    MensajeAlerta();

                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                                if (GvCargaArchivo.Rows[i].Cells[2].Text == "")
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "Debe ingresar Nombre de Submarca";
                                                    MensajeAlerta();

                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                            }
                                            if (sigue)
                                            {
                                                DataSet oeSubMarcaid = oProductos.ObteneridsProductos(3, null, GvCargaArchivo.Rows[i].Cells[0].Text, null, GvCargaArchivo.Rows[i].Cells[1].Text, null, null, null, null, null);
                                                if (oeSubMarcaid != null)
                                                {
                                                    if (sigue)
                                                    {
                                                        if (oeSubMarcaid.Tables[0].Rows.Count == 0)
                                                        {
                                                            Alertas.CssClass = "MensajesError";
                                                            LblFaltantes.Text = "La categoria " + GvCargaArchivo.Rows[i].Cells[0].Text + ". No es válido ";
                                                            MensajeAlerta();
                                                            sigue = false;
                                                            i = dt.Rows.Count - 1;
                                                        }
                                                        else
                                                        {
                                                            GvCargaArchivo.Rows[i].Cells[0].Text = oeSubMarcaid.Tables[0].Rows[0][0].ToString().Trim();

                                                            if (oeSubMarcaid.Tables[1].Rows.Count == 0)
                                                            {
                                                                Alertas.CssClass = "MensajesError";
                                                                LblFaltantes.Text = "La Marca " + GvCargaArchivo.Rows[i].Cells[1].Text + ". No es válida para la Categoría " + GvCargaArchivo.Rows[i].Cells[2].Text;
                                                                MensajeAlerta();
                                                                sigue = false;
                                                                i = dt.Rows.Count - 1;
                                                            }

                                                            else
                                                            {
                                                                sigue = false;
                                                                for (int j = 0; j <= oeSubMarcaid.Tables[1].Rows.Count - 1; j++)
                                                                {
                                                                    string dato;
                                                                    dato = oeSubMarcaid.Tables[1].Rows[j][1].ToString().Trim();
                                                                    if (GvCargaArchivo.Rows[i].Cells[1].Text == oeSubMarcaid.Tables[1].Rows[j][1].ToString().Trim())
                                                                    {
                                                                        GvCargaArchivo.Rows[i].Cells[1].Text = oeSubMarcaid.Tables[1].Rows[j][0].ToString().Trim();
                                                                        j = oeSubMarcaid.Tables[1].Rows.Count - 1;
                                                                        sigue = true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            if (sigue)
                                            {
                                                DAplicacion odconsulSubBrand = new DAplicacion();
                                                DataTable dtconsulta = odconsulSubBrand.ConsultaDuplicados(ConfigurationManager.AppSettings["SubBrand"], GvCargaArchivo.Rows[i].Cells[2].Text, GvCargaArchivo.Rows[i].Cells[1].Text, null);

                                                if (dtconsulta != null)
                                                {
                                                    Alertas.CssClass = "MensajesError";
                                                    LblFaltantes.Text = "La SubMarca de Producto " + dt.Rows[i][1].ToString().Trim() + " ya existe";
                                                    MensajeAlerta();
                                                    sigue = false;
                                                    i = dt.Rows.Count - 1;
                                                }
                                            }
                                        }
                                        if (sigue)
                                        {
                                            for (int i = 0; i <= GvCargaArchivo.Rows.Count - 1; i++)
                                            {
                                                try
                                                {

                                                    ESubBrand oeSubBrand = oSubBrand.RegistrarSubBrand(GvCargaArchivo.Rows[i].Cells[2].Text, Convert.ToInt32(GvCargaArchivo.Rows[i].Cells[1].Text), true, Convert.ToString(this.Session["sUser"]), DateTime.Now, Convert.ToString(this.Session["sUser"]), DateTime.Now);
                                                    ESubBrand oeSubBrandtmp = oSubBrand.RegistrarSubBrandTMP(oeSubBrand.id_SubBrand.ToString(), oeSubBrand.Name_SubBrand, oeSubBrand.id_Brand, oeSubBrand.SubBrand_Status);
                                                }
                                                catch (Exception ex)
                                                {
                                                    string error = "";
                                                    string mensaje = "";
                                                    error = Convert.ToString(ex.Message);
                                                    mensaje = ConfigurationManager.AppSettings["ErrorConection"];
                                                    if (error == mensaje)
                                                    {
                                                        Lucky.CFG.Exceptions.Exceptions exs = new Lucky.CFG.Exceptions.Exceptions(ex);
                                                        string errMessage = "";
                                                        errMessage = mensaje;
                                                        errMessage = new Lucky.CFG.Util.Functions().preparaMsgError(ex.Message);
                                                        this.Response.Redirect("../../../err_mensaje.aspx?msg=" + errMessage, true);
                                                    }
                                                    else
                                                    {
                                                        this.Session.Abandon();
                                                        Response.Redirect("~/err_mensaje_seccion.aspx", true);
                                                    }
                                                }
                                            }
                                            Alertas.CssClass = "MensajesCorrecto";
                                            LblFaltantes.Text = GvCargaArchivo.Rows.Count + "  SubMarcas fueron cargadas con exito" + this.Session["sBrand"] + ", provenientes del Archivo  " + FileUpCMasivaMArca.FileName.ToLower();
                                            MensajeAlerta();
                                        }
                                    }
                                    else
                                    {

                                        GvCargaArchivo.DataBind();
                                        Alertas.CssClass = "MensajesError";
                                        LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de Categorias válido. Por favor verifique la estructura que fue enviada a su correo.";
                                        MensajeAlerta();


                                        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                                        correo.From = new System.Net.Mail.MailAddress("AdminXplora@lucky.com.pe");
                                        correo.To.Add(this.Session["smail"].ToString());
                                        correo.Subject = "Errores en archivo de creación de SubMarcas";
                                        correo.IsBodyHtml = true;
                                        correo.Priority = System.Net.Mail.MailPriority.Normal;
                                        string[] txtbody = new string[] { "Señor(a):" + "<br/>" + 
                                        this.Session["nameuser"].ToString() + "<br/>" + "<br/>" +                   
                                             

                                            "El archivo que usted seleccionó para la carga de asignación de puntos de venta no cumple con una estructura válida." + "<br/>" +
                                            "Por favor verifique que tenga 4 columnas" + "<br/>" +  "<br/>" +
                                            "Sugerencia : identifique las columnas de la siguiente manera para su mayor comprensión" + "<br/>" +  "<br/>" +
                                            "Columna 1  : Categoria" + "<br/>" +
                                            "Columna 2  : Marca"+ "<br/>" +
                                            "Columna 3  : SubMarca"+ "<br/>" +
                                            "Nota:  No es indispensable que las columnas se identifiquen de la misma manera como se describió anteriormente, usted puede personalizar los nombres de las columnas del archivo ." +
                                            "Pero tenga en cuenta que debe ingresar la información de las SubMarcas en ese orden." + "<br/>" + "<br/>" + "<br/>" +
                                            "Cordial Saludo" + "<br/>" + "Administrador Xplora" };

                                        correo.Body = string.Concat(txtbody);

                                        System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
                                        cliente.Host = "mail.lucky.com.pe";

                                        try
                                        {
                                            cliente.Send(correo);
                                        }
                                        catch (System.Net.Mail.SmtpException)
                                        {
                                        }
                                    }
                                }
                                else
                                {
                                    Alertas.CssClass = "MensajesError";
                                    LblFaltantes.Text = "Es indispensable que cierre sesión he inicie nuevamente. su sesión expiró.";
                                    MensajeAlerta();

                                }
                            }
                            catch (Exception ex)
                            {
                                Alertas.CssClass = "MensajesError";
                                LblFaltantes.Text = "El archivo seleccionado no corresponde a un archivo de SubCategorias valido. Por favor verifique que el nombre de la hoja donde estan los datos sea SubMarca";
                                MensajeAlerta();

                            }
                            oConn1.Close();
                        }
                        else
                        {
                            Alertas.CssClass = "MensajesError";
                            LblFaltantes.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                            MensajeAlerta();

                        }
                    }
                }
                else
                {
                    Alertas.CssClass = "MensajesError";
                    LblFaltantes.Text = "Es indispensable seleccionar un archivo.";
                    MensajeAlerta();

                }
            }
            #endregion
        }

    }
}
