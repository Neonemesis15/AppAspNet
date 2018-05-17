using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
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

namespace SIGE.Pages.Modulos.Cliente
{
    public partial class Mod_Cliente_Biblioteca : System.Web.UI.Page
    {
        int Table = 0;
        Facade_Proceso_Cliente.Facade_Proceso_Cliente Get_DataClientes = new SIGE.Facade_Proceso_Cliente.Facade_Proceso_Cliente();
        Facade_Procesos_Administrativos.Facade_Procesos_Administrativos Get_Administrativo = new SIGE.Facade_Procesos_Administrativos.Facade_Procesos_Administrativos();
        Conexion oCoon = new Conexion();

        MenuItem CreateMenuItem(String text, String Value, String toolTip)
        {

            // Create a new MenuItem object.
            MenuItem menuItem = new MenuItem();

            // Set the properties of the MenuItem object using
            // the specified parameters.
            menuItem.Text = text;
            menuItem.Value = Value;
            menuItem.ToolTip = toolTip;


            return menuItem;

        }

        private void Obtener_Categorias()
        {
            try
            {
                int scanal = 0;
                if (this.Session["Canal"].ToString().Trim() == "0")
                {
                    scanal = 0;
                }
                else
                {
                    scanal = 1;
                }
                DataSet dsCategorias = Get_DataClientes.Get_ObtenerCategoriasPOP(Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim());
                ListViewCanales.DataSource = dsCategorias.Tables[scanal];
                ListViewCanales.DataBind();
                //ImgCanalMayor.Attributes.Add("onMouseOver", "this.src = '/Pages/ImgBooom/botonmayoristaazul.png'");
                //ImgCanalMayor.Attributes.Add("onMouseOut", "this.src = '/Pages/ImgBooom/botonmayoristaazulchico.png'");
                //ImgCanalMemor.Attributes.Add("onMouseOver", "this.src = '/Pages/ImgBooom/botonminoristaazul.png'");
                //ImgCanalMemor.Attributes.Add("onMouseOut", "this.src = '/Pages/ImgBooom/botonminoristaazulchico.png'");
                //ImgCanalAASS.Attributes.Add("onMouseOver", "this.src = '/Pages/ImgBooom/botonaassazul.png'");
                //ImgCanalAASS.Attributes.Add("onMouseOut", "this.src = '/Pages/ImgBooom/botonaassazulchico.png'");
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void Obtener_MenusMarcas()
        {
            try
            {
                MenuMarcas.Items.Clear();
                if (this.Session["Canal"].ToString().Trim() == "0")
                {
                    Table = 0;
                }
                else
                {
                    Table = 1;
                }

                DataSet dsmarca = Get_DataClientes.Get_ObtenerMarcasPOP(this.Session["categoria"].ToString().Trim(), Convert.ToInt32(this.Session["companyid"]), this.Session["Canal"].ToString().Trim());
                for (int i = 0; i <= dsmarca.Tables[Table].Rows.Count - 1; i++)
                {
                    MenuItem item = new MenuItem();
                    //string s = "<SCRIPT language=\"javascript\">" +
                    //"alert ('\"MenuEmpresarial.SelectedValue\"'');</SCRIPT>";
                    //RegisterStartupScript("mensaje", s);
                    item.Text = dsmarca.Tables[Table].Rows[i][1].ToString();
                    item.Value = dsmarca.Tables[Table].Rows[i][0].ToString();
                    MenuMarcas.Items.Add(item);

                    //// Create the submenu items.
                    //MenuItem SubMenuItemPOP;
                    //DataSet dsmt = null;
                    //dsmt = Get_DataClientes.Get_ObtenerPOPxMarca(this.Session["categoria"].ToString().Trim(), Convert.ToInt32(MenuMarcas.Items[i].Value));
                    //for (int j = 0; j <= dsmt.Tables[0].Rows.Count - 1; j++)
                    //{
                    //    SubMenuItemPOP = CreateMenuItem(dsmt.Tables[0].Rows[j][1].ToString(), dsmt.Tables[0].Rows[j][0].ToString(), "prueba");
                    //    SubMenuItemPOP.ChildItems.Add(SubMenuItemPOP);
                    //    //  MenuMarcas.Items[0].ChildItems.Clear();
                    //    //  MenuMarcas.Items[0].Selected = true;
                    //    // MenuMarcas.ItemWrap.ToString();
                    //    MenuMarcas.Items[i].ChildItems.Add(SubMenuItemPOP);
                    //    //MenuMarcas.Items.Add(musicSubMenuItem);
                    //    //MenuMarcas.DataBind();
                    //}
                }
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void Obtener_MenusMaterial()
        {
            try
            {
                MenuMaterial.Items.Clear();
                DataSet dsMaterial = Get_DataClientes.Get_ObtenerPOPxMarca(this.Session["categoria"].ToString().Trim(), Convert.ToInt32(this.Session["companyid"]), Convert.ToInt32(MenuMarcas.SelectedValue), this.Session["Canal"].ToString().Trim());
                if (this.Session["Canal"].ToString().Trim() == "0")
                {
                    if (MenuMarcas.SelectedValue == "0")
                    {
                        Table = 0;
                    }
                    else
                    {
                        Table = 1;
                    }
                }
                else
                {
                    if (MenuMarcas.SelectedValue == "0")
                    {
                        Table = 2;
                    }
                    else
                    {
                        Table = 3;
                    }
                }

                for (int i = 0; i <= dsMaterial.Tables[Table].Rows.Count - 1; i++)
                {
                    MenuItem itemMaterial = new MenuItem();
                    //string s = "<SCRIPT language=\"javascript\">" +
                    //"alert ('\"MenuEmpresarial.SelectedValue\"'');</SCRIPT>";
                    //RegisterStartupScript("mensaje", s);
                    itemMaterial.Text = dsMaterial.Tables[Table].Rows[i][1].ToString();
                    itemMaterial.Value = dsMaterial.Tables[Table].Rows[i][0].ToString();
                    MenuMaterial.Items.Add(itemMaterial);
                }
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void BibliotecaAll()
        {
            // funcion inactiva , fuera de uso
            try
            {
                DataSet dsBiblioteca = oCoon.ejecutarDataSet("UP_WEBSIGE_CLIENTE_OBTENERFOTOSBIBLIOTECAPOP", Convert.ToInt32(this.Session["companyid"]), this.Session["categoria"].ToString().Trim(), Convert.ToInt32(this.Session["id_Brand"]), Convert.ToInt32(this.Session["id_material"]), this.Session["Canal"].ToString().Trim());
                if (this.Session["Canal"].ToString().Trim() == "0")
                {
                    Table = 0;
                }
                else
                {
                    Table = 1;
                }
                //ListViewBiblioteca.DataSource = dsBiblioteca.Tables[Table];
                //ListViewBiblioteca.DataBind();

                ListViewBibliotecaPOP.DataSource = dsBiblioteca.Tables[Table];
                ListViewBibliotecaPOP.DataBind();
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void BibliotecaXCategorias()
        {
            try
            {
                DataSet dsBiblioteca = oCoon.ejecutarDataSet("UP_WEBSIGE_CLIENTE_OBTENERFOTOSBIBLIOTECAPOP", Convert.ToInt32(this.Session["companyid"]), this.Session["categoria"].ToString().Trim(), Convert.ToInt32(this.Session["id_Brand"]), Convert.ToInt32(this.Session["id_material"]), this.Session["Canal"].ToString().Trim());
                if (this.Session["Canal"].ToString().Trim() == "0")
                {
                    Table = 2;
                }
                else
                {
                    Table = 3;
                }
                ListViewBiblioteca.DataSource = dsBiblioteca.Tables[Table];
                ListViewBiblioteca.DataBind();
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void BibliotecaXMarca()
        {
            try
            {
                DataSet dsBiblioteca = oCoon.ejecutarDataSet("UP_WEBSIGE_CLIENTE_OBTENERFOTOSBIBLIOTECAPOP", Convert.ToInt32(this.Session["companyid"]), this.Session["categoria"].ToString().Trim(), Convert.ToInt32(this.Session["id_Brand"]), Convert.ToInt32(this.Session["id_material"]), this.Session["Canal"].ToString().Trim());
                if (this.Session["Canal"].ToString().Trim() == "0")
                {
                    if (this.Session["id_Brand"].ToString().Trim() == "0")
                    {
                        Table = 2;
                    }
                    else
                    {
                        Table = 4;
                    }
                }
                else
                {
                    if (this.Session["id_Brand"].ToString().Trim() == "0")
                    {
                        Table = 3;
                    }
                    else
                    {
                        Table = 5;
                    }
                }
                ListViewBiblioteca.DataSource = dsBiblioteca.Tables[Table];
                ListViewBiblioteca.DataBind();
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void BibliotecaXMaterial()
        {
            try
            {
                DataSet dsBiblioteca = oCoon.ejecutarDataSet("UP_WEBSIGE_CLIENTE_OBTENERFOTOSBIBLIOTECAPOP", Convert.ToInt32(this.Session["companyid"]), this.Session["categoria"].ToString().Trim(), Convert.ToInt32(this.Session["id_Brand"]), Convert.ToInt32(this.Session["id_material"]), this.Session["Canal"].ToString().Trim());
                if (this.Session["Canal"].ToString().Trim() == "0")
                {
                    if (this.Session["id_Brand"].ToString().Trim() == "0")
                    {
                        if (this.Session["id_material"].ToString().Trim() == "0")
                        {
                            Table = 2;
                        }
                        else
                        {
                            Table = 8;
                        }
                    }
                    else
                    {
                        if (this.Session["id_material"].ToString().Trim() == "0")
                        {
                            Table = 4;
                        }
                        else
                        {
                            Table = 6;
                        }
                    }
                }
                else
                {
                    if (this.Session["id_Brand"].ToString().Trim() == "0")
                    {
                        if (this.Session["id_material"].ToString().Trim() == "0")
                        {
                            Table = 3;
                        }
                        else
                        {
                            Table = 9;
                        }
                    }
                    else
                    {
                        if (this.Session["id_material"].ToString().Trim() == "0")
                        {
                            Table = 5;
                        }
                        else
                        {
                            Table = 7;
                        }
                    }
                }
                ListViewBiblioteca.DataSource = dsBiblioteca.Tables[Table];
                ListViewBiblioteca.DataBind();
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
        }
        private void BibliotecaDetalle()
        {
            try
            {
                
                DataSet dsBibliotecaDetalle = Get_DataClientes.Get_ObtenerPOPDetallado(Convert.ToInt32(this.Session["Biblioteca"]));
                if (dsBibliotecaDetalle.Tables[0].Rows.Count > 0)
                {
                    ListViewBibliotecaPOP.DataSource = dsBibliotecaDetalle.Tables[0];
                    ListViewBibliotecaPOP.DataBind();
                    if (dsBibliotecaDetalle.Tables[0].Rows.Count > 1)
                    {
                        AfterListDataPager.Visible = true;
                        BeforeListDataPager.Visible = true;
                    }
                    else
                    {
                        AfterListDataPager.Visible = false;
                        BeforeListDataPager.Visible = false;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string sUser = this.Session["sUser"].ToString();
                    usersession.Text = sUser;
                    string sPassw = this.Session["sPassw"].ToString();
                    if (sUser != null && sPassw != null)
                    {
                        Obtener_Categorias();
                        this.Session["categoria"] = "0";
                        this.Session["id_Brand"] = "0";
                        this.Session["id_material"] = "0";
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

        protected void ListViewCanales_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)ListViewCanales.Items[e.NewSelectedIndex];
            this.Session["CategoriaSeleccionada"] = e.NewSelectedIndex;
        }
        protected void ListViewCanales_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            // Clear selection. 
            ListViewCanales.SelectedIndex = -1;
        }
        protected void ListViewCanales_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MarcaAgua.Style.Value = "position: absolute;top: 50%;left:50%; height: 390px; width:520px; margin-top: -165px; margin-left: -180px;  z-index: 2; background-position: center center;   background-repeat: no-repeat;  background-image: url('../ImgBooom/none.png');";
            LblMaterialSel.Visible = true;
            CarrouselBiblioteca.Style.Value = "display:none;";
            ListBiblioteca.Style.Value = "display:Block;  overflow: auto;  height: 420px; max-height: 420px;";
            ListViewBibliotecaPOP.DataBind();
            int item = Convert.ToInt32(this.Session["CategoriaSeleccionada"]);
            Label lbCodcategoria = new Label();
            lbCodcategoria = ((Label)ListViewCanales.Items[item].FindControl("idCategory"));
            this.Session["categoria"] = lbCodcategoria.Text;
            LblCategoriaSel.Text = ((Label)ListViewCanales.Items[item].FindControl("nameCategory")).Text.ToUpper().ToString().Trim();
            LblMarcaSel.Text = "Todas";
            LblMaterialSel.Text = "TODOS";
            Obtener_MenusMarcas();
            MenuMaterial.Items.Clear();
            divMaterial.Style.Value = "Display:none;";
            divMarcas.Style.Value = " width:188px;  max-height: 200px; overflow: auto;display:block;background-color:Transparent;";
            BibliotecaXCategorias();
        }
        protected void MenuMarcas_MenuItemClick(object sender, MenuEventArgs e)
        {
            CarrouselBiblioteca.Style.Value = "display:none;";
            ListBiblioteca.Style.Value = "display:Block;  overflow: auto;  height: 420px; max-height: 420px;";
            Obtener_MenusMaterial();            
            divMaterial.Style.Value = " width:188px;  max-height: 200px; overflow: auto;display:block;background-color:Transparent;";
            LblMarcaSel.Text = MenuMarcas.SelectedItem.Text.ToUpper();
            LblMaterialSel.Visible = true;
            LblMaterialSel.Text = "TODOS";
            this.Session["id_Brand"] = MenuMarcas.SelectedValue;
            BibliotecaXMarca();
           
           
            //POR FAVOR NO ELIMINAR NADA DE ESTE COMENTARIO . ING MAURICIO ORTIZ 24 DE JUNIO DE 2010

            //try
            //{
            //    MenuItem parent = e.Item.Parent;
            //    string prueba = "The " + e.Item.Text +
            //" menu item has an index of " +
            //parent.ChildItems.IndexOf(e.Item).ToString() +
            //" in the parent menu item's ChildItems collection.";

            //}
            //catch
            //{
            //}
            //string prueba2 = "The " + e.Item.Text +
            // " menu item has an index of " +
            // MenuMarcas.Items.IndexOf(e.Item).ToString() +
            // " in the Menu control's Items collection.";

            //// Create the submenu items.

            //MenuItem SubMenuItemPOP;
            //DataSet dsmt = null;
            //dsmt = Get_DataClientes.Get_ObtenerPOPxMarca(this.Session["categoria"].ToString().Trim(), Convert.ToInt32(MenuMarcas.SelectedValue));
            //for (int i = 0; i <= dsmt.Tables[0].Rows.Count - 1; i++)
            //{
            //    SubMenuItemPOP = CreateMenuItem(dsmt.Tables[0].Rows[i][1].ToString(), dsmt.Tables[0].Rows[i][0].ToString(), "prueba");
            //    SubMenuItemPOP.ChildItems.Add(SubMenuItemPOP);
            //    //  MenuMarcas.Items[0].ChildItems.Clear();


            //    //  MenuMarcas.Items[0].Selected = true;
            //    // MenuMarcas.ItemWrap.ToString();
            //    MenuMarcas.Items[MenuMarcas.Items.IndexOf(e.Item)].ChildItems.Add(SubMenuItemPOP);
            //    //MenuMarcas.Items.Add(musicSubMenuItem);
            //    //MenuMarcas.DataBind();
            //}



        }
        protected void MenuMaterial_MenuItemClick(object sender, MenuEventArgs e)
        {
            CarrouselBiblioteca.Style.Value = "display:none;";
            ListBiblioteca.Style.Value = "display:Block;  overflow: auto;  height: 420px; max-height: 420px;";
            LblMaterialSel.Visible = true;
            LblMaterialSel.Text = MenuMaterial.SelectedItem.Text.ToUpper();
            this.Session["id_material"] = MenuMaterial.SelectedValue;
            BibliotecaXMaterial();
        }
        protected void ListViewBiblioteca_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBiblioteca.Style.Value = "display:none;  overflow: auto; height: 420px; max-height: 420px;";
            CarrouselBiblioteca.Style.Value = "vertical-align: middle; display: block; width: 98%; min-width: 98%; max-width:98% ; height:99%; max-height:99%;";
            int item = Convert.ToInt32(this.Session["FotografiaSeleccionada"]);
            Label lblCod_Biblioteca = new Label();
            lblCod_Biblioteca = ((Label)ListViewBiblioteca.Items[item].FindControl("Cod_Biblioteca"));
            this.Session["Biblioteca"] = lblCod_Biblioteca.Text;
            LblMaterialSel.Text = ((Label)ListViewBiblioteca.Items[item].FindControl("NameMaterial")).Text.ToUpper();
            LblMaterialSel.Visible = false;            
            BibliotecaDetalle();

        }
        protected void ListViewBiblioteca_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            ListViewItem item = (ListViewItem)ListViewBiblioteca.Items[e.NewSelectedIndex];
            this.Session["FotografiaSeleccionada"] = e.NewSelectedIndex;
        }
        protected void ListViewBiblioteca_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            // Clear selection. 
            ListViewBiblioteca.SelectedIndex = -1;
        }
        protected void BeforeListDataPager_PreRender(object sender, EventArgs e)
        {            
            BibliotecaDetalle();
           // LblMaterialSel.Text = ((Label)ListViewBiblioteca.FindControl("infoPOPClientPhotos_nameLabel")).Text.ToUpper();
           
        }
        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                string canal = this.Session["Canal"].ToString().Trim();
            }
            catch (Exception ex)
            {
                string canal = ex.ToString().Trim();                 
                Get_Administrativo.Get_Delete_Sesion_User(usersession.Text);
                this.Session.Abandon();
                Response.Redirect("~/err_mensaje_seccion.aspx", true);
            }
            
            if (this.Session["Canal"].ToString().Trim() == "0")
            {
                Response.Redirect("~/Pages/Modulos/Cliente/Mod_Cliente_Canales.aspx", true);
            }
            else
            {
                Response.Redirect("~/Pages/Modulos/Cliente/Mod_Cliente.aspx", true);
            }
        }

        protected void ListViewBibliotecaPOP_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            // Clear selection. 
            ListViewBibliotecaPOP.SelectedIndex = -1;
        }
    }
}